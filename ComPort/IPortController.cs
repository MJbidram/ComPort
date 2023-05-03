using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComPort
{
    internal interface IPortController
    {
        void OpenPort(SerialPort serialPort, string portName, string BaudRate, string DataBits, string stopBits, string parity);
        void ClosePort(SerialPort serialPort);


    }
}
