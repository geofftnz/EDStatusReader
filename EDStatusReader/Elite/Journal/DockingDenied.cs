using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Elite.Journal
{
    public class DockingDenied : JournalHeader
    {
        public string Reason { get; set; }
        public string StationName { get; set; }
    }
}
