using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Output
{
    public class PanelCommand
    {
        public const byte CMD_ESC = 0xff;
        public const byte CMD_END = 0xfe;
        
        public const byte CMD_SHIFTREG = 0x01;

        public const byte CMD_STARTUP = 0x0E;
        public const byte CMD_SHUTDOWN = 0x0F;

        public const byte CMD_LCDLINE = 0x10;
        public const byte CMD_LCDCMASK = 0xF0;
        public const byte CMD_LCDDMASK = 0x0F;
        public const byte CMD_LCDLINE1 = 0x10;
        public const byte CMD_LCDLINE2 = 0x11;
        public const byte CMD_LCDLINE3 = 0x12;
        public const byte CMD_LCDLINE4 = 0x13;
        public const byte CMD_LCDLINE5 = 0x14;
        public const byte CMD_LCDLINE6 = 0x15;
        public const byte CMD_LCDLINE7 = 0x16;
        public const byte CMD_LCDLINE8 = 0x17;
        public const byte CMD_7SEG1 = 0x21;
        public const byte CMD_7SEG2 = 0x22;
        public const byte CMD_7SEGINT1 = 0x23;
        public const byte CMD_7SEGINT2 = 0x24;

        public const int MAX_COMMAND_DATA = 64;


        public byte Command { get; set; }
        public byte[] CommandData { get; set; }


        public PanelCommand(byte command)
        {
            Command = command;
        }

        public virtual byte[] GetBuffer()
        {
            var buf = new List<byte>();

            buf.Add(CMD_ESC);
            buf.Add(Command);

            if (CommandData != null)
            {
                buf.AddRange(CommandData.Take(MAX_COMMAND_DATA));
            }

            buf.Add(CMD_ESC);
            buf.Add(CMD_END);

            return buf.ToArray();
        }
    }
}
