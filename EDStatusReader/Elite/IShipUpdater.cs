using EDStatusReader.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Elite
{
    public interface IShipUpdater
    {
        void Update(ShipStatus ship);
    }
}
