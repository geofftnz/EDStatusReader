using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDStatusReader.Ship;

namespace EDStatusReader.Elite.Journal
{
    public class StartJump : JournalHeader
    {
        public string JumpType { get; set; }
        public string StarSystem { get; set; }
        public string StarClass { get; set; }

        public override void Update(ShipStatus ship)
        {
            ship.FSDJump = true;
            ship.FsdCharging = false;
            if (JumpType.Equals("Hyperspace", StringComparison.InvariantCultureIgnoreCase))
            {
                ship.FSDJumpType = FSDJumpType.Hyperspace;
                ship.FSDTarget = StarSystem;
                ship.FSDTargetStarClass = StarClass;
                ship.InHyperspace = true;
            }
            else  // supercruise
            {
                ship.FSDJumpType = FSDJumpType.Supercruise;
                ship.InHyperspace = false;
            }
        }
    }
}
