using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComPort
{
    internal interface IDataManagement
    {
        void SendData(SerialPort serialPort,String data);
        string ReciveData(SerialPort serialPort);
        string DataConvertor(string data, string type);
        List<string> filteredData(List<string> data,string item);
        void SaveData(List<string> data);
    }
}
