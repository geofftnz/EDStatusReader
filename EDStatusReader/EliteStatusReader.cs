using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EDStatusReader.Elite;


namespace EDStatusReader
{
    public class EliteStatusReader
    {
        public string FileName { get; set; }
        private DateTime prevWriteTime = DateTime.UtcNow;

        public EliteStatusReader()
        {
            FileName = Environment.ExpandEnvironmentVariables(@"%UserProfile%\Saved Games\Frontier Developments\Elite Dangerous\Status.json");
        }

        public void Run()
        {
            while (true)
            {

                if (PollFile(out Status status))
                {
                    if (status != null)
                    {
                        Console.WriteLine($"{status.Timestamp} {status.Flags} {status.Pips[0]}{status.Pips[1]}{status.Pips[2]} W:{status.FireGroup} G:{status.GuiFocus} C:{status.Cargo:0} F:{(status?.Fuel?.Total ?? 0.0):0.000}t");
                    }
                }

                Thread.Sleep(250);
            }
        }

        private Status lastResult = new Status();
        private bool PollFile(out Status status)
        {
            bool changed = false;
            status = null;
            var lastWriteTime = File.GetLastWriteTimeUtc(FileName);

            if (lastWriteTime != prevWriteTime)
            {
                prevWriteTime = lastWriteTime;
                //Console.WriteLine("write");

                //string json = File.ReadAllText(FileName);

                using (var fs = File.Open(FileName,FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        string json = sr.ReadToEnd();

                        
                        var newstatus = JsonConvert.DeserializeObject<Status>(json);
                        lastResult = newstatus ?? lastResult;
                        status = lastResult;
                        changed = true;
                        

                        Console.WriteLine(json);
                        sr.Close();
                    }
                    fs.Close();
                }
            }

            return changed;
        }

    }
}
