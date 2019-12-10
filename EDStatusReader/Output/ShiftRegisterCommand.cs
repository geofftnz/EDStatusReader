using EDStatusReader.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Output
{
    public class ShiftRegisterCommand : PanelCommand
    {
        public enum Output
        {
            LED1R = 0x00000200,
            LED1G = 0x00000400,
            LED2R = 0x00000800,
            LED2G = 0x00001000,
            LED3R = 0x00002000,
            LED3G = 0x00004000,
            LED4R = 0x00008000,
            LED4G = 0x00000002,
            LED5R = 0x00000004,
            LED5G = 0x00000008,

            LED6Y = 0x00000100,
            LED7B = 0x00000001,

            SW1 = 0x00000020,
            SW2 = 0x00000040,
            // SW3 unlit
            SW4 = 0x00000010,
            SW5 = 0x00000080,
        };



        public ShiftRegisterCommand(uint data) : base(CMD_SHIFTREG)
        {
            CommandData = BitConverter.GetBytes((ushort)data);  // HACK: ushort for now until we expand the shift registers on the display.
        }

        public ShiftRegisterCommand(ShipStatus ship) : base(CMD_SHIFTREG)
        {
            uint reg = 0x0;

            reg |= ship.MassLock ? (uint)Output.LED1R : (uint)Output.LED1G;
            reg |= ship.HardPointsDeployed ? (uint)Output.LED2R : (uint)Output.LED2G;
            reg |= ship.CargoScoopDeployed ? (uint)Output.LED3R : (uint)Output.LED3G;
            reg |= ship.LandingGearDeployed ? (uint)Output.LED4R : (uint)Output.LED4G;
            reg |= ship.FsdCooldown ? (uint)Output.LED5R : (uint)Output.LED5G;

            reg |= ship.FsdCharging ? (uint)Output.LED6Y : 0u;
            reg |= ship.FSDSupercharged ? (uint)Output.LED7B : 0u;

            reg |= ship.GuiFocus == Elite.GuiElement.GalaxyMap ? (uint)Output.SW1 : 0u;
            reg |= ship.GuiFocus == Elite.GuiElement.SystemMap ? (uint)Output.SW2 : 0u;
            reg |= ship.Supercruise ? (uint)Output.SW4 : 0u;
            reg |= ship.InHyperspace ? (uint)Output.SW5 : 0u;

            CommandData = BitConverter.GetBytes((ushort)reg);  // HACK: ushort for now until we expand the shift registers on the display.
        }

    }
}
