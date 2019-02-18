using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.FileIO
{
    public static class FileUtils
    {
        public static IEnumerable<JournalFileName> GetJournalFilenames(string path)
        {
            var files = Directory.EnumerateFiles(path, @"Journal*.log").Select(x=>Path.GetFileName(x));

            return files.Select(x => JournalFileName.Parse(x, path)).Where(x => x != null);
        }
    }
}
