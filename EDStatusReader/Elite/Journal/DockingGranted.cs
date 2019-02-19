using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Elite.Journal
{
    public class DockingGranted : JournalHeader
    {
        public string StarSystem { get; set; }
        public int LandingPad { get; set; }
    }
}

