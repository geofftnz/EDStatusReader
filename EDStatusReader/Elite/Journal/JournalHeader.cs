using EDStatusReader.Ship;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Elite.Journal
{
    public class JournalHeader : IShipUpdater, IEliteEventHeader
    {
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonProperty(PropertyName = "event")]
        public string EventName { get; set; }

        public string _InputLine { get; set; }

        public virtual void Update(ShipStatus ship)
        {
        }
    }
}
