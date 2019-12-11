using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Output
{
    public class LCDLineCommand : PanelCommand
    {
        public LCDLineCommand(byte line, string text) : base((byte)(CMD_LCDLINE1 + line))
        {
            CommandData = Encoding.ASCII.GetBytes(text.PadRight(20)).Concat(new byte[] { 0 }).ToArray();
        }

    }
}
