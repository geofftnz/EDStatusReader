using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Elite.Journal
{
    public class StartJump : JournalHeader
    {
        public string JumpType { get; set; }
        public string StarSystem { get; set; }
        public string StarClass { get; set; }
    }
}
