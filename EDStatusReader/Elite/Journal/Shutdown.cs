using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDStatusReader.Ship;

namespace EDStatusReader.Elite.Journal
{
    public class Shutdown : JournalHeader
    {

        public override void Update(ShipStatus ship)
        {
            ship.Shutdown = true;
        }
    }
}

