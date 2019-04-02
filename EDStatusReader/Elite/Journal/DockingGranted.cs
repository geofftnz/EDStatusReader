using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDStatusReader.Ship;

namespace EDStatusReader.Elite.Journal
{
    public class DockingGranted : JournalHeader
    {
        public string StationName { get; set; }
        public int LandingPad { get; set; }

        public override void Update(ShipStatus ship)
        {
            ship.DockingRequested = false;
            ship.RequestedDockingStation = StationName;
            ship.DockingGranted = true;
            ship.DockingDeniedReason = string.Empty;
            ship.LandingPad = LandingPad;
        }
    }
}

