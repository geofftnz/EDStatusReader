using EDStatusReader.Elite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.FileIO
{
    public class EliteStatus : IDisposable
    {
        public string Filename { get; set; }

        private DateTime prevWriteTime = DateTime.UtcNow;
        private StringBuilder lineBuilder = new StringBuilder(4096);
        private FileStream fs;

        public EliteStatus(string filename)
        {
            Filename = filename;

            fs = File.Open(Filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }

        public IEnumerable<Status> GetStatus()
        {
            var lastWriteTime = File.GetLastWriteTimeUtc(Filename);

            if (prevWriteTime == lastWriteTime)
                yield break;

            prevWriteTime = lastWriteTime;
            fs.Seek(0, SeekOrigin.Begin);

            lineBuilder.Clear();
            int b;
            do
            {
                b = fs.ReadByte();
                if (b >= 32)
                {
                    lineBuilder.Append((char)b);
                }

            } while (b != -1 && b != 10);

            // see if we end in a }
            var line = lineBuilder.ToString();
            if (line.EndsWith("}"))
            {
                yield return JsonConvert.DeserializeObject<Status>(line);
            }
        }

        public void Dispose()
        {
            fs?.Dispose();
        }
    }
}
