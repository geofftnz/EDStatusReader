using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Elite
{
    [Flags]
    public enum StatusFlags
    {
        Docked = 1,
        Landed = 2,
        LandingGear = 4,
        ShieldsUp = 8,
        Supercruise = 16,
        FAOff = 32,
        Hardpoints = 64,
        InWing = 128,
        Lights = 256,
        CargoScoop = 512,
        Silent = 1024,
        FuelScoop = 2048,
        SrvHandbrake = 4096,
        SrvTurret = 8192,
        SrvUnderShip = 16384,
        SrvDriveAssist = 32768,
        MassLock = 65536,
        FsdCharging = 131072,
        FsdCooldown = 262144,
        LowFuel  = 524288  ,
        OverHeating = 1048576 ,
        HasLatLong = 2097152,
        IsInDanger = 4194304,
        Interdiction = 8388608,
        InMainShip = 16777216,
        InFighter = 33554432,
        InSRV = 67108864,
        Analysis = 134217728,
        F28 = 268435456,
        F29 = 536870912,
        F30 = 1073741824
    }
}
