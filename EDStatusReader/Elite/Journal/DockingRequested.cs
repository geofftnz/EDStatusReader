using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDStatusReader.Ship;

namespace EDStatusReader.Elite.Journal
{
    public class DockingRequested : JournalHeader
    {
        public string StationName { get; set; }

        public override void Update(ShipStatus ship)
        {
            ship.DockingRequested = true;
            ship.RequestedDockingStation = StationName;
            ship.DockingGranted = null;
            ship.DockingDeniedReason = string.Empty;
            ship.LandingPad = 0;
        }
    }
}

