using EDStatusReader.Ship;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Elite
{
    public class Status : IShipUpdater
    {
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonProperty(PropertyName = "event")]
        public string EventName { get; set; }

        public StatusFlags Flags { get; set; }

        public int[] Pips { get; set; }

        public int FireGroup { get; set; }

        public GuiElement GuiFocus { get; set; }

        public FuelStore Fuel { get; set; }

        public float Cargo { get; set; }


        public class FuelStore
        {
            //FuelMain":33.316231, "FuelReservoir
            [JsonProperty(PropertyName = "FuelMain")]
            public float Main { get; set; } = 0.0f;

            [JsonProperty(PropertyName = "FuelReservoir")]
            public float Reservoir { get; set; } = 0.0f;

            public float Total => Main + Reservoir;
        }

        public override string ToString()
        {
            return $"{Timestamp} {Flags} {Pips?[0] ?? 0}{Pips?[1] ?? 0}{Pips?[2] ?? 0} W:{FireGroup} G:{GuiFocus} C:{Cargo:0} F:{(Fuel?.Total ?? 0.0):0.000}t";
        }

        public void Update(ShipStatus ship)
        {
            if (ship == null)
                throw new ArgumentNullException("ship");

            #region flags
            ship.Docked = Flags.HasFlag(StatusFlags.Docked);
            ship.Landed = Flags.HasFlag(StatusFlags.Landed);
            ship.LandingGearDeployed = Flags.HasFlag(StatusFlags.LandingGear);
            ship.ShieldsUp = Flags.HasFlag(StatusFlags.ShieldsUp);
            ship.FlightAssistDisabled = Flags.HasFlag(StatusFlags.FAOff);
            ship.HardPointsDeployed = Flags.HasFlag(StatusFlags.Hardpoints);
            ship.Supercruise = Flags.HasFlag(StatusFlags.Supercruise);
            ship.InWing = Flags.HasFlag(StatusFlags.InWing);
            ship.LightsOn = Flags.HasFlag(StatusFlags.Lights);
            ship.CargoScoopDeployed = Flags.HasFlag(StatusFlags.CargoScoop);
            ship.SilentRunning = Flags.HasFlag(StatusFlags.Silent);
            ship.FuelScoop = Flags.HasFlag(StatusFlags.FuelScoop);
            ship.SrvHandbrake = Flags.HasFlag(StatusFlags.SrvHandbrake);
            ship.SrvTurret = Flags.HasFlag(StatusFlags.SrvTurret);
            ship.SrvUnderShip = Flags.HasFlag(StatusFlags.SrvUnderShip);
            ship.SrvDriveAssist = Flags.HasFlag(StatusFlags.SrvDriveAssist);
            ship.MassLock = Flags.HasFlag(StatusFlags.MassLock);
            ship.FsdCharging = Flags.HasFlag(StatusFlags.FsdCharging);
            ship.FsdCooldown = Flags.HasFlag(StatusFlags.FsdCooldown);
            ship.LowFuel = Flags.HasFlag(StatusFlags.LowFuel);
            ship.OverHeating = Flags.HasFlag(StatusFlags.OverHeating);
            ship.HasLatLong = Flags.HasFlag(StatusFlags.HasLatLong);
            ship.IsInDanger = Flags.HasFlag(StatusFlags.IsInDanger);
            ship.Interdiction = Flags.HasFlag(StatusFlags.Interdiction);
            ship.InMainShip = Flags.HasFlag(StatusFlags.InMainShip);
            ship.InFighter = Flags.HasFlag(StatusFlags.InFighter);
            ship.InSRV = Flags.HasFlag(StatusFlags.InSRV);
            ship.AnalysisMode = Flags.HasFlag(StatusFlags.Analysis);
            ship.NightVisionOn = Flags.HasFlag(StatusFlags.NightVis);
            #endregion

            ship.TotalFuel = Fuel.Total;
            ship.TotalFuelKg = (int)(Fuel.Total * 1000);

            ship.Cargo = (int)Cargo;

            ship.GuiFocus = GuiFocus;
        }
    }
}
