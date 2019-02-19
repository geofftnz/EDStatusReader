using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //var app = new EliteStatusReader();
            //var app = new EliteJournalReader();
            //app.Run();

            using (var app = new Ship.ShipStatusReader())
            {
                app.Run();
            }
        }
    }
}
