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
        public bool? DockingGranted { get; set; } = null;
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

        private void T(int x, int y, string text, ConsoleColor fg = ConsoleColor.Gray, ConsoleColor bg = ConsoleColor.Black)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;
            Console.Write(text);
        }

        public void RenderToConsole()
        {
            int x = 0, y = 0;

            ConsoleColor bg = ConsoleColor.Black;
            Console.BackgroundColor = bg;
            Console.Clear();

            // Cargo/Fuel panel
            // Cargo
            T(x + 3, y++, "Cargo", CargoScoopDeployed ? ConsoleColor.White : ConsoleColor.DarkGray);
            T(x + 4, y++, $"{Cargo:0000}", ConsoleColor.Green);
            y++;
            // Fuel
            T(x + 4, y++, "Fuel", ConsoleColor.Gray);
            T(x + 4, y++, $"{TotalFuelKg:0000}", ConsoleColor.Green);
            T(x + 2, y++, "Scooping", FuelScoop ? ConsoleColor.Yellow : ConsoleColor.DarkGray);
            T(x + 2, y++, "Low Fuel", FuelScoop ? ConsoleColor.Red : ConsoleColor.DarkGray);
            y++;

            // Docking panel
            T(x + 3, y++, "DOCKING");
            T(x + 2, y++, "Requested", DockingRequested ? ConsoleColor.Cyan : ConsoleColor.DarkGray);

            if (DockingGranted.HasValue && !Docked)
                T(x + 3, y, ((DockingGranted ?? false) || Docked) ? "Granted" : "Denied", ((DockingGranted ?? false) || Docked) ? ConsoleColor.Green : ConsoleColor.Red);
            y++;
            if (!(DockingGranted ?? true) && !Docked)
                T(x + 1, y, DockingDeniedReason, ConsoleColor.Red);
            y++;
            T(x + 2, y++, "Docked", Docked ? ConsoleColor.Green : ConsoleColor.DarkGray);
            T(x + 2, y++, $"Pad: {LandingPad}", ((DockingGranted ?? false) || Docked) ? ConsoleColor.Green : ConsoleColor.DarkGray);
            y++;

            // Misc panel
            T(x + 2, y++, "  HEAT");
            T(x + 2, y++, "OverHeat", OverHeating ? ConsoleColor.Red : ConsoleColor.DarkGray);
            T(x + 2, y++, " Silent", SilentRunning ? ConsoleColor.Green : ConsoleColor.DarkGray);
            y++;

            // Misc panel
            T(x + 2, y++, "  MISC");
            T(x + 2, y++, " Lights", LightsOn ? ConsoleColor.Green : ConsoleColor.DarkGray);
            T(x + 2, y++, "NightVis", NightVisionOn ? ConsoleColor.Green : ConsoleColor.DarkGray);
            T(x + 2, y++, "  Wing", InWing ? ConsoleColor.Cyan : ConsoleColor.DarkGray);
            y++;



            x = 15;
            y = 0;

            // Nav / FSD
            T(x, y++, $"Loc: {Location}");
            T(x, y++, $"Tgt: {FSDTarget} ({FSDTargetStarClass})");
            y++;
            T(x, y++, "Interlocks:");
            int y2 = y;
            T(x, y++, "Masslock", MassLock ? ConsoleColor.Red : ConsoleColor.Green);
            T(x, y++, "Hardpoints", HardPointsDeployed ? ConsoleColor.Red : ConsoleColor.Green);
            T(x, y++, "CargoScoop", CargoScoopDeployed ? ConsoleColor.Red : ConsoleColor.Green);
            T(x, y++, "LandingGear", LandingGearDeployed ? ConsoleColor.Red : ConsoleColor.Green);
            T(x, y++, "Cooldown", FsdCooldown ? ConsoleColor.Red : ConsoleColor.Green);

            y = y2;
            x += 15;
            T(x, y++, "JetBoost", FSDSupercharged ? ConsoleColor.Cyan : ConsoleColor.DarkGray);
            T(x, y++, "Charging", FsdCharging ? ConsoleColor.Yellow : ConsoleColor.DarkGray);
            T(x, y++, "Supercruise", Supercruise ? ConsoleColor.Yellow : ConsoleColor.DarkGray);
            T(x, y++, "Hyperspace", InHyperspace ? ConsoleColor.Yellow : ConsoleColor.DarkGray);


            x = 44;
            y = 0;
            // Target / weapons / combat
            T(x, y++, "COMBAT MODE", (!AnalysisMode) ? ConsoleColor.Yellow : ConsoleColor.DarkGray);
            T(x, y++, "!DANGER!", (IsInDanger) ? ConsoleColor.Red : ConsoleColor.DarkGray);
            if (TargetLocked)
            {
                T(x, y++, $"Target: {TargetName}");
                T(x, y++, $"Scan: {TargetScanStage}");

                T(x, y++, TargetWanted.HasValue ? (TargetWanted.Value ? "WANTED" : "Clean") : "Scanning...", TargetWanted.HasValue ? (TargetWanted.Value ? ConsoleColor.Red : ConsoleColor.Green) : ConsoleColor.DarkGray);
            }
            else
            {
                T(x, y++, $"No Target");
                y += 2;
            }
            y++;
            T(x, y++, "Hardpoints", HardPointsDeployed ? ConsoleColor.Green : ConsoleColor.DarkGray);
            T(x, y++, $"Firegroup {FireGroup}");
            T(x, y++, "Interdiction", Interdiction ? ConsoleColor.Red : ConsoleColor.DarkGray);
            T(x, y++, "ShieldsDown", (!ShieldsUp) ? ConsoleColor.Red : ConsoleColor.DarkGray);


        }

    }
}
