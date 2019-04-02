using EDStatusReader.Elite.Journal;
using EDStatusReader.Ship;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Elite
{
    /// <summary>
    /// Turns lines of text into ship updates
    /// </summary>
    public class JournalParser
    {
        public Dictionary<string, Func<IEliteEventHeader, IShipUpdater>> Parsers { get; private set; } = new Dictionary<string, Func<IEliteEventHeader, IShipUpdater>>();

        public JournalParser()
        {
            RegisterParsers();
        }

        private void RegisterParsers()
        {
            RegisterParser<DockingDenied>();
            RegisterParser<DockingGranted>();
            RegisterParser<DockingRequested>();
            RegisterParser<FSDJump>();
            RegisterParser<FSDTarget>();
            RegisterParser<Location>();
            RegisterParser<ShipTargeted>();
            RegisterParser<StartJump>();
        }

        private void RegisterParser<T>() where T : IShipUpdater
        {
            Parsers.Add(typeof(T).GetType().Name, h => JsonConvert.DeserializeObject<T>(h._InputLine));
        }

        public bool Parse(IEliteEventHeader ev, ShipStatus ship)
        {
            if (Parsers.TryGetValue(ev.EventName, out var parser))
            {
                parser(ev).Update(ship);
                return true;
            }

            return false;
        }


    }
}
