using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDStatusReader.Ship;

namespace EDStatusReader.Elite.Journal
{
    public class FSDTarget : JournalHeader
    {
        public string Name { get; set; }
        public int RemainingJumpsInRoute { get; set; }

        public override void Update(ShipStatus ship)
        {
            ship.FSDTarget = Name;
            ship.RemainingJumpsInRoute = RemainingJumpsInRoute;
        }
    }
}

