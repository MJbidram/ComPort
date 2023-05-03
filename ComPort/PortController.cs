using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComPort
{
    internal class PortController : IPortController
    {
        public void ClosePort(SerialPort serialPort)
        {

            serialPort.Close();




        }

        public void OpenPort(SerialPort serialPort, string portName, string BaudRate, string DataBits, string stopBits, string parity)
        {

            serialPort.PortName = portName;
            serialPort.BaudRate = Convert.ToInt32(BaudRate);
            serialPort.DataBits = Convert.ToInt32(DataBits);
            serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBits);
            serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), parity);
            serialPort.Open();




        }
    }
}
