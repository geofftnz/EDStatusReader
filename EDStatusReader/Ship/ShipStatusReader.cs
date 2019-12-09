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

                if (update)
                {
                    ship.RenderToConsole();
                    controlPanel.DebugWriteByte(ship.Debug1);
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
