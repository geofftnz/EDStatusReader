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
            //CommandData = Encoding.ASCII.GetBytes(text).Concat(new byte[] { 0 }).ToArray();
        }

        public static byte[] IntTo7Seg4(int value)
        {
            var b = new byte[4];

            for (int i = 0; i < 4; i++)
            {

            }
            return b;

        }

    }
}
