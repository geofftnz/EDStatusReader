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
            //RegisterParser<Status>();
            Parsers.Add("Status", a => (IShipUpdater)a);

            RegisterParser<DockingDenied>();
            RegisterParser<DockingGranted>();
            RegisterParser<DockingRequested>();
            RegisterParser<Docked>();
            RegisterParser<FSDJump>();
            RegisterParser<FSDTarget>();
            RegisterParser<Location>();
            RegisterParser<ShipTargeted>();
            RegisterParser<StartJump>();
            RegisterParser<JetConeBoost>();
            RegisterParser<SupercruiseExit>();
        }

        private void RegisterParser<T>() where T : IShipUpdater, new()
        {
            T a = new T();
            string name = a.GetType().Name;
            Parsers.Add(name, h => JsonConvert.DeserializeObject<T>(h._InputLine));
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
