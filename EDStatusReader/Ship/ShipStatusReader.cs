using EDStatusReader.FileIO;
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
            while (true)
            {
                InitJournalReader();
                InitStatusReader();

                var status = statusFile?.GetStatus()?.FirstOrDefault();
                if (status != null)
                    Console.WriteLine(status.ToString());

                if (journalFile != null)
                {
                    foreach (var line in journalFile.GetLines())
                    {
                        Console.WriteLine(line);
                    }
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
