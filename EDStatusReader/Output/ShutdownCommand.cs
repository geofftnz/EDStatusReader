using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Output
{
    public class ShutdownCommand : PanelCommand
    {
        public ShutdownCommand() : base(CMD_SHUTDOWN)
        {
            CommandData = new byte[] { 0 };
        }
    }
}
