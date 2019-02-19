using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Elite
{
    public class Status
    {
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonProperty(PropertyName = "event")]
        public string EventName { get; set; }

        public StatusFlags Flags { get; set; }

        public int[] Pips { get; set; }

        public int FireGroup { get; set; }

        public GuiElement GuiFocus { get; set; }

        public FuelStore Fuel { get; set; }

        public float Cargo { get; set; }


        public class FuelStore
        {
            //FuelMain":33.316231, "FuelReservoir
            [JsonProperty(PropertyName = "FuelMain")]
            public float Main { get; set; } = 0.0f;

            [JsonProperty(PropertyName = "FuelReservoir")]
            public float Reservoir { get; set; } = 0.0f;

            public float Total => Main + Reservoir;
        }

        public override string ToString()
        {
            return $"{Timestamp} {Flags} {Pips[0]}{Pips[1]}{Pips[2]} W:{FireGroup} G:{GuiFocus} C:{Cargo:0} F:{(Fuel?.Total ?? 0.0):0.000}t";
        }
    }
}
