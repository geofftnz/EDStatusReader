using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Output
{
    public class LED7SegIntCommand : PanelCommand
    {
        public LED7SegIntCommand(byte display, int value) : base((byte)(CMD_7SEGINT1 + display))
        {
            CommandData = IntTo7Seg4(value);
        }

        public static byte[] IntTo7Seg4(int value)
        {
            var b = new byte[4];

            b[0] = (byte)((value >> 24) & 0xff);
            b[1] = (byte)((value >> 16) & 0xff);
            b[2] = (byte)((value >> 8) & 0xff);
            b[3] = (byte)((value) & 0xff);

            return b;
        }

    }
}
