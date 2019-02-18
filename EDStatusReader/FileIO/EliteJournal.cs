using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.FileIO
{
    public class EliteJournal
    {
        public string Filename { get; set; }
        public long FilePosition { get; private set; } = 0;
        public DateTime LastWriteTime { get; set; } = DateTime.UtcNow;

        public EliteJournal(string filename)
        {
            Filename = filename;
        }

        /// <summary>
        /// Get lines since last time
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetLines()
        {
            var fi = new FileInfo(Filename);

            if (fi.LastWriteTimeUtc == LastWriteTime)
                yield break;

            using (var fs = File.Open(Filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fs.Seek(FilePosition, SeekOrigin.Begin);

                var sb = new StringBuilder(4096);

                while (fs.CanRead)
                {
                    int b = fs.ReadByte();
                    FilePosition = fs.Position;

                    if (b == 10)
                    {
                        yield return sb.ToString();
                        sb.Clear();
                    }
                    else if (b >= 32)
                    {
                        sb.Append((char)b);
                    }
                }

                /*
                using (var sr = new StreamReader(fs,Encoding.UTF8,false,1))
                {
                    while (!sr.EndOfStream)
                    {
                        LastWriteTime = fi.LastWriteTimeUtc;
                        FilePosition = fs.Position;
                        yield return sr.ReadLine();
                    }
                    sr.Close();
                }*/
            }

        }
    }
}
