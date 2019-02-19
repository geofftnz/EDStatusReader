using EDStatusReader.Elite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Ship
{
    public class ShipStatus
    {
        public DateTime LastUpdateUTC { get; set; }

        public Status Status { get; set; }

        public string Location { get; set; }

        public bool DockingRequested { get; set; }
        public bool? DockingGranted { get; set; }
        public int LandingPad { get; set; }
        public string DockingDeniedReason { get; set; }

        public string FSDJumpType { get; set; }
        public string FSDTarget { get; set; }
        public bool InHyperspace { get; set; }

        public bool TargetLocked { get; set; }
        public string TargetName { get; set; }
        public int TargetScanStage { get; set; }
        public bool? TargetWanted { get; set; }


    }
}
