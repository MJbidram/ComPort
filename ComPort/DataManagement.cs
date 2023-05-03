using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ComPort
{

    internal class DataManagement : IDataManagement
    {
        string reciveData;
        List<string> reciveDataList = new List<string>();
        StreamWriter objStreamWriter;
        String pathFile;
        public string DataConvertor(string data, string type)
        {
            string strOut = "";
            if (type == "Hex")
            {
                foreach (int element in data)
                {
                    strOut += Convert.ToString(element, 16) + "\t";
                }
            }
            else if (type == "Decimal")
            {
                foreach (int element in data)
                {
                    strOut += Convert.ToString(element) + "\t";
                }
            }
            else if (type == "Binary")
            {
                foreach (int element in data)
                {
                    strOut += Convert.ToString(element, 2) + "\t";
                }
            }
            else if (type == "Char")
            {
                foreach (int element in data)
                {
                    strOut += Convert.ToChar(element);
                }
            }

            return strOut;
        }

        public string ReciveData(SerialPort serialPort)
        {
            byte[] buffer = new byte[2048];
            while (serialPort.BytesToRead > 0)
            {
                try
                {
                    reciveData = serialPort.ReadLine();

                    Debug.WriteLine(reciveData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return reciveData;

        }

        public void SaveData(List<string> data)
        {
            //pathFile = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            //pathFile += @"\_mySourceFiles\serialData.txt";
            //try
            //{
            //    objStreamWriter = new StreamWriter(pathFile, true);
            //    objStreamWriter.WriteLine(reciveData);
            //    objStreamWriter.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream sm = File.Open(saveFileDialog.FileName, FileMode.Create)) 
                using (StreamWriter sw  = new StreamWriter(sm))
                {
                    foreach (var item in data)
                    {
                        sw.WriteLine(item);
                    }
                    sw.Close();
                    sm.Close();
                }

            }
            saveFileDialog.Dispose();
        }

        public void SendData(SerialPort serialPort, string data)
        {
            serialPort.WriteLine(data);
        }

        public List<string> filteredData(List<string> data, string item)
        {
            List<string> result = new List<string>();
            if (!string.IsNullOrEmpty(item))
            {

                foreach (String str in data)
                {
                    if (str.StartsWith(item))
                    {

                        result.Add(str);
                    }

                }
            }
            else if (item == "")
            {

                foreach (string str in data)
                {

                    result.Add(str);
                }
            }
            return result;
        }
    }
}
