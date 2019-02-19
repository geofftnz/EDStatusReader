using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.FileIO
{
    public class EliteJournal : IDisposable
    {
        public string Filename { get; set; }
        public long FilePosition { get; private set; } = 0;
        public DateTime LastWriteTime { get; set; } = DateTime.UtcNow;

        private StringBuilder lineBuilder = new StringBuilder(4096);
        private FileStream fs;

        public EliteJournal(string filename)
        {
            Filename = filename;

            fs = File.Open(Filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }

        public IEnumerable<string> GetLines()
        {
            int b;

            do
            {
                b = fs.ReadByte();
                if (b == 10)
                {
                    yield return lineBuilder.ToString();
                    lineBuilder.Clear();
                }
                else if (b >= 32)
                {
                    lineBuilder.Append((char)b);
                }
                FilePosition = fs.Position;
            } while (b != -1);

        }


        /// <summary>
        /// Get lines since last time
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetLines2()
        {
            var fi = new FileInfo(Filename);
            if (fi.LastWriteTimeUtc == LastWriteTime)
                yield break;

            using (var fs = File.Open(Filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fs.Seek(FilePosition, SeekOrigin.Begin);



                while (fs.CanRead)
                {
                    int b = fs.ReadByte();
                    if (b < 0)
                        break;
                    FilePosition = fs.Position;

                    if (b == 10)
                    {
                        yield return lineBuilder.ToString();
                        lineBuilder.Clear();
                    }
                    else if (b >= 32)
                    {
                        lineBuilder.Append((char)b);
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

        public void Dispose()
        {
            fs?.Dispose();
        }
    }
}
