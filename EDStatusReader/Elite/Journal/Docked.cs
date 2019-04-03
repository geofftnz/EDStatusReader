using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDStatusReader.Ship;

namespace EDStatusReader.Elite.Journal
{
    public class Docked : JournalHeader
    {
        public string StationName { get; set; }

        public override void Update(ShipStatus ship)
        {
            ship.DockingRequested = false;
            ship.RequestedDockingStation = StationName;
            ship.DockingGranted = false;
            ship.DockingDeniedReason = string.Empty;
        }
    }
}

