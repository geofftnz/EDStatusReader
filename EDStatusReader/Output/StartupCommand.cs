using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Output
{
    public class StartupCommand : PanelCommand
    {
        public StartupCommand() : base(CMD_STARTUP)
        {
            CommandData = new byte[] { 0 };
        }
    }
}
