using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDStatusReader.Ship;

namespace EDStatusReader.Elite.Journal
{
    public class SupercruiseExit : JournalHeader
    {
        public string StarSystem { get; set; }
        public string Body { get; set; }
        public string BodyType { get; set; }

        public override void Update(ShipStatus ship)
        {
            ship.Location = StarSystem;
        }
    }
}

