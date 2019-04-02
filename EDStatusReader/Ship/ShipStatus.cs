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

        // status flags
        public bool Docked { get; set; }
        public bool Landed { get; set; }
        public bool LandingGearDeployed { get; set; }
        public bool ShieldsUp { get; set; }
        public bool FlightAssistDisabled { get; set; }
        public bool HardPointsDeployed { get; set; }
        public bool Supercruise { get; set; }
        public bool InWing { get; set; }
        public bool LightsOn { get; set; }
        public bool CargoScoopDeployed { get; set; }
        public bool SilentRunning { get; set; }
        public bool FuelScoop { get; set; }
        public bool SrvHandbrake { get; set; }
        public bool SrvTurret { get; set; }
        public bool SrvUnderShip { get; set; }
        public bool SrvDriveAssist { get; set; }
        public bool MassLock { get; set; }
        public bool FsdCharging { get; set; }
        public bool FsdCooldown { get; set; }
        public bool LowFuel { get; set; }
        public bool OverHeating { get; set; }
        public bool HasLatLong { get; set; }
        public bool IsInDanger { get; set; }
        public bool Interdiction { get; set; }
        public bool InMainShip { get; set; }
        public bool InFighter { get; set; }
        public bool InSRV { get; set; }
        public bool AnalysisMode { get; set; }
        public bool NightVisionOn { get; set; }

        // flags derived from journal
        public bool FSDSupercharged { get; set; }
        public float FSDSuperchargeAmount { get; set; }

        /// <summary>
        /// Currently in a jump (hyperspace or supercruise entry)
        /// </summary>
        public bool FSDJump { get; set; }
        public bool InHyperspace { get; set; }


        public SpaceType SpaceType
        {
            get
            {
                if (InHyperspace) return SpaceType.Hyperspace;
                if (Supercruise) return SpaceType.Supercruise;
                return SpaceType.NormalSpace;
            }
        }

        // location
        public string Location { get; set; }

        // docking
        public string RequestedDockingStation { get; set; }
        public bool DockingRequested { get; set; }
        public bool? DockingGranted { get; set; }
        public int LandingPad { get; set; }
        public string DockingDeniedReason { get; set; }

        // FSD
        public FSDJumpType FSDJumpType { get; set; }
        public string FSDTarget { get; set; }
        public string FSDTargetStarClass { get; set; }

        // Target
        public bool TargetLocked { get; set; }
        public string TargetName { get; set; }
        public int TargetScanStage { get; set; }
        public bool? TargetWanted { get; set; }

        // Hardpoints/Weapons
        public int FireGroup { get; set; }

        // Fuel
        public float TotalFuel { get; set; }
        public int TotalFuelKg { get; set; }

        // Cargo
        public int Cargo { get; set; }

        // GUI
        public GuiElement GuiFocus { get; set; }

    }
}
