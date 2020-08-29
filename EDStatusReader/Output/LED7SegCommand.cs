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

            // decimal point position (for thousands)
            int dp = -1;
            if (value >= 1000) dp = 3;
            if (value >= 10000) dp = 2;
            if (value >= 100000) dp = 1;
            if (value >= 1000000) dp = 0;

            // keep in range
            while (value > 9999) 
                value /= 10;

            for (int i = 0; i < 4; i++)
            {
                int digit = value % 10;

                // supress leading zeros
                if (value == 0 && i > 0)
                    digit = 10;

                b[i] = (byte)(digit);

                if (i == dp)
                    b[i] += 11;

                value /= 10;
            }
            return b;
        }

    }
}
