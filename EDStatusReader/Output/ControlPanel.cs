using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;


namespace EDStatusReader.Output
{
    public class ControlPanel : IDisposable
    {
        public string PortName { get; private set; }
        private SerialPort serialPort;

        public ControlPanel(string portName, int baud = 9600)
        {
            PortName = portName;
            serialPort = new SerialPort(portName, baud, Parity.None, 8, StopBits.One);

            serialPort.Open();

        }

        public void SendCommand(PanelCommand command)
        {
            var bs = command.GetBuffer();
            serialPort.Write(bs, 0, bs.Length);
        }
        public void DebugWriteByte(byte b)
        {
            byte[] bs = new byte[1];
            bs[0] = b;
            serialPort.Write(bs, 0, bs.Length);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    serialPort.Close();
                    serialPort.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion



    }
}
