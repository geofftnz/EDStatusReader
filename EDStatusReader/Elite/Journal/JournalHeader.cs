using EDStatusReader.Ship;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Elite.Journal
{
    public abstract class JournalHeader : IShipUpdater
    {
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonProperty(PropertyName = "event")]
        public string EventName { get; set; }

        public abstract void Update(ShipStatus ship);
    }
}
