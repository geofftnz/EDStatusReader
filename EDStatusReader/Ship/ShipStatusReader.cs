using EDStatusReader.Elite;
using EDStatusReader.Elite.Journal;
using EDStatusReader.FileIO;
using EDStatusReader.Output;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDStatusReader.Ship
{
    public class ShipStatusReader : IDisposable
    {
        public string EliteSavePath { get; private set; }
        private EliteJournal journalFile;
        private EliteStatus statusFile;
        private ShipStatus ship = new ShipStatus();
        private JournalParser parser = new JournalParser();
        private ControlPanel controlPanel = new ControlPanel("COM6");

        private uint shiftreg = 0x1;

        public ShipStatusReader()
        {
            EliteSavePath = Environment.ExpandEnvironmentVariables(@"%UserProfile%\Saved Games\Frontier Developments\Elite Dangerous");
        }

        private void InitJournalReader()
        {
            if (journalFile == null)
            {
                var journalFileName = FileUtils.GetJournalFilenames(EliteSavePath).OrderByDescending(x => x.StartDate).FirstOrDefault();
                if (journalFileName == null)
                    return;
                journalFile = new EliteJournal(journalFileName.Fullpath);
            }
        }

        private void InitStatusReader()
        {
            if (statusFile == null)
            {
                var filename = Path.Combine(EliteSavePath, "Status.json");
                statusFile = new EliteStatus(filename);
            }
        }


        public void Run()
        {
            // TODO: exit condition
            while (true)
            {
                InitJournalReader();
                InitStatusReader();

                ship.JournalFileName = journalFile.Filename;

                var items = new List<IEliteEventHeader>();

                if (journalFile != null)
                    items.AddRange(journalFile.GetLines().Select(s => { var a = JsonConvert.DeserializeObject<JournalHeader>(s); a._InputLine = s; return a; }));

                var status = statusFile?.GetStatus()?.FirstOrDefault();
                if (status != null)
                    items.Add(status);

                bool update = false;
                bool clear = true;
                foreach (var item in items.OrderBy(i => i.Timestamp))
                {
                    if (parser.Parse(item, ship))
                    {

                        if (!item.EventName.Equals("status", StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (clear)
                                ship.LastJournalLines.Clear();
                            clear = false;
                            ship.LastJournalLines.Add(item._InputLine);
                        }

                        update = true;
                    }
                }

                // DEBUG
                /*
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.LeftArrow)
                        shiftreg >>= 1;
                    if (key.Key == ConsoleKey.RightArrow)
                        shiftreg <<= 1;

                    if (shiftreg == 0)
                        shiftreg = 1;

                    update = true;
                }*/

                if (update)
                {
                    ship.RenderToConsole();
                    
                    controlPanel.SendCommand(new LCDLineCommand(0, $"@ {ship.Location}"));

                    controlPanel.SendCommand(new LCDLineCommand(1, $"{ship.RemainingJumpsInRoute}> {ship.FSDTarget} ({ship.FSDTargetStarClass})"));

                    controlPanel.SendCommand(new LCDLineCommand(2, $"* {ship.TargetName}"));
                    
                    if (!ship.TargetLocked)
                    {
                        controlPanel.SendCommand(new LCDLineCommand(3, $"?"));
                    }
                    if (ship.TargetLocked && ship.TargetScanStage < 3)
                    {
                        string scantext = "***";
                        controlPanel.SendCommand(new LCDLineCommand(3, $"! {ship.TargetName} {scantext.Substring(0,ship.TargetScanStage+1)}"));
                    }
                    if (ship.TargetLocked && ship.TargetScanStage == 3)
                    {
                        controlPanel.SendCommand(new LCDLineCommand(3, $"! {ship.TargetName} {((ship.TargetWanted ?? false) ? "W!":"C")}"));
                    }


                        //controlPanel.SendCommand(new LCDLineCommand(3, $"{shiftreg:X8}"));
                        controlPanel.SendCommand(new ShiftRegisterCommand(ship));
                    
                    controlPanel.SendCommand(new LED7SegCommand(0, ship.Cargo));

                    int tempFuel = ship.TotalFuelKg;
                    while (tempFuel > 9999) tempFuel /= 10;
                    controlPanel.SendCommand(new LED7SegCommand(1, tempFuel));
                }


                Thread.Sleep(100);
            }
        }



        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    journalFile?.Dispose();
                    statusFile?.Dispose();
                    controlPanel.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion



    }
}
