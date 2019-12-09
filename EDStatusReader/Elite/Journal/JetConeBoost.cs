using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDStatusReader.Ship;

namespace EDStatusReader.Elite.Journal
{
    public class JetConeBoost : JournalHeader
    {
        public float BoostValue { get; set; }

        public override void Update(ShipStatus ship)
        {
            ship.FSDSupercharged = true;
            ship.FSDSuperchargeAmount = BoostValue;
        }
    }
}

