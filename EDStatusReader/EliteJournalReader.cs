using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EDStatusReader.FileIO;

namespace EDStatusReader
{
    public class EliteJournalReader
    {
        public string JournalPath { get; set; }

        public EliteJournalReader()
        {
            JournalPath = Environment.ExpandEnvironmentVariables(@"%UserProfile%\Saved Games\Frontier Developments\Elite Dangerous");
        }

        public void Run()
        {
            Console.WriteLine(JournalPath);
            var journalFileName = FileUtils.GetJournalFilenames(JournalPath).OrderByDescending(x => x.StartDate).FirstOrDefault();

            if (journalFileName == null)
            {
                Console.WriteLine("Could not find a journal file to open");
                return;
            }
            Console.WriteLine($"Using journal file {journalFileName.Fullpath}");

            var journalFile = new EliteJournal(journalFileName.Fullpath);
            
            while (true)
            {
                foreach(var line in journalFile.GetLines())
                {
                    Console.WriteLine(journalFile.FilePosition.ToString() + " " + line);
                }

                Thread.Sleep(500);
            }
        }

    }
}
