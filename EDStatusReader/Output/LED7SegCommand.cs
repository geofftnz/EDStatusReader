using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Output
{
    public class LED7SegCommand : PanelCommand
    {
        public LED7SegCommand(byte display, int value) : base((byte)(CMD_7SEG1 + display))
        {
            CommandData = IntTo7Seg4(value);
        }

        public static byte[] IntTo7Seg4(int value)
        {
            var b = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                int digit = value % 10;

                // supress leading zeros
                if (value == 0 && i > 0)
                    digit = 10;

                b[i] = (byte)(digit);
                value /= 10;
            }
            return b;

        }

    }
}
