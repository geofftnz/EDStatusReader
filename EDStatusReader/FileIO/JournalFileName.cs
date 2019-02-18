using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.FileIO
{
    public class JournalFileName
    {
        public string Fullpath { get; set; }
        public string Filename { get; set; }
        public DateTime StartDate { get; set; }
        public int FileNumber { get; set; }

        public static JournalFileName Parse(string s, string path = "")
        {
            if (string.IsNullOrWhiteSpace(s))
                return null;

            if (!s.StartsWith("Journal."))
                return null;

            if (!s.EndsWith(".log"))
                return null;

            var parts = s.Split('.');

            if (parts.Length != 4)
                return null;

            // extract date parts
            if (parts[1].Length != 12)
                return null;

            int[] dateparts = new int[6];

            for (int i = 0; i < 6; i++)
            {
                if (!int.TryParse(parts[1].Substring(i * 2, 2), out dateparts[i]))
                    return null;
            }

            // extract file number
            if (!int.TryParse(parts[2], out int FileNumber))
                return null;

            return new JournalFileName
            {
                Fullpath = Path.Combine(path, s),
                Filename = s,
                StartDate = new DateTime(2000 + dateparts[0], dateparts[1], dateparts[2], dateparts[3], dateparts[4], dateparts[5]),
                FileNumber = FileNumber
            };
        }
    }
}
