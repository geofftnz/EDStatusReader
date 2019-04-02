using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDStatusReader.Ship;

namespace EDStatusReader.Elite.Journal
{
    public class ShipTargeted : JournalHeader
    {
        public bool TargetLocked { get; set; }
        public string Ship { get; set; }
        public string Ship_Localised { get; set; }
        public int ScanStage { get; set; }
        public string PilotRank { get; set; }
        public string LegalStatus { get; set; }

        public override void Update(ShipStatus ship)
        {
            ship.TargetLocked = TargetLocked;
            ship.TargetName = TargetLocked ? Ship_Localised : string.Empty;
            ship.TargetScanStage = TargetLocked ? ScanStage : 0;
            ship.TargetWanted = LegalStatus?.Equals("Wanted", StringComparison.InvariantCultureIgnoreCase) ?? false;
        }
    }
}
