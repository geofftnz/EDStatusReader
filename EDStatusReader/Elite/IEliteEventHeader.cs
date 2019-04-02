using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Elite
{
    public interface IEliteEventHeader
    {
        DateTime Timestamp { get; set; }
        string EventName { get; set; }
        string _InputLine { get; set; }
    }
}
