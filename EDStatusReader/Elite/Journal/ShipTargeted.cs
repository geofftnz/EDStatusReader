using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Elite.Journal
{
    public class ShipTargeted
    {
        public bool TargetLocked { get; set; }
        public string Ship { get; set; }
        public string Ship_Localised { get; set; }
        public int ScanStage { get; set; }
        public string PilotRank { get; set; }
        public string LegalStatus { get; set; }
    }
}
