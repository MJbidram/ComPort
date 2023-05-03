using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ComPort
{

    public partial class Form1 : Form
    {
        PortController portController;
        DataManagement dataManagement;
        string dataOut;
        string reciveData;
        List<string> reciveDataList = new List<string>();
        StreamWriter objStreamWriter;
        String pathFile;
        bool isSaveToTxtFile;
        bool isNotSaveAndReplaceToTxtFile;


        #region GUI
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataManagement = new DataManagement();
            portController = new PortController();
            String[] ports = SerialPort.GetPortNames();
            cBoxComPort.Items.AddRange(ports);
            chBoxDtrEnable.Checked = false;
            serialPort1.DtrEnable = false;
            chBoxRtsEnable.Checked = false;
            serialPort1.RtsEnable = false;
            btnOpen.Enabled = true;
            btnClose.Enabled = false;
            lblStatus.Text = "OFF";
            isSaveToTxtFile = false;
            isNotSaveAndReplaceToTxtFile = false;
            cBoxReciveFormat.Text = "Char";


        }

        private void btnOpen_Click(object sender, EventArgs e)
        {

            try
            {
                portController.OpenPort(
                    serialPort1,
                    cBoxComPort.Text,
                    cBoxBaudRate.Text,
                    cBoxDatabits.Text,
                    cBoxStopBits.Text,
                    cBoxParityBits.Text);

                progressBar1.Value = 100;
                btnOpen.Enabled = false;
                btnClose.Enabled = true;
                lblStatus.Text = "ON";
            }
            catch (Exception err)
            {
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "OFF";

            }



        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {

                portController.ClosePort(serialPort1);
                progressBar1.Value = 0;
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
                lblStatus.Text = "OFF";
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            dataOut = txtBoxDataOut.Text;

            dataManagement.SendData(serialPort1, dataOut);


        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void chBoxDtrEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (chBoxDtrEnable.Checked)
            {
                serialPort1.DtrEnable = true;
                MessageBox.Show("DTR Enable", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                serialPort1.DtrEnable = false;
            }
        }

        private void chBoxRtsEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (chBoxRtsEnable.Checked)
            {
                MessageBox.Show("RTS Enable", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                serialPort1.RtsEnable = true;
            }
            else
            {
                serialPort1.RtsEnable = false;
            }

        }

        private void btnClearDataOut_Click(object sender, EventArgs e)
        {
            if (txtBoxDataOut.Text != "")
            {
                txtBoxDataOut.Text = "";
            }
        }

        private void txtBoxDataOut_TextChanged(object sender, EventArgs e)
        {
            int outDataLength = txtBoxDataOut.Text.Length;
            lblDataOutLength.Text = string.Format("{0:00}", outDataLength);
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            reciveData = dataManagement.ReciveData(serialPort1);

            this.Invoke(new EventHandler(showData));
        }

        private void showData(object sender, EventArgs e)
        {
            reciveData = dataManagement.DataConvertor(reciveData, cBoxReciveFormat.Text);
            reciveDataList.Add(reciveData);
            lblDataRecLength.Text = string.Format("{0:00}", reciveData.Length);
            recListBox.Items.Add(reciveData);

        }

        private void btnClearRecData_Click(object sender, EventArgs e)
        {

            recListBox.Items.Clear();
            reciveDataList.Clear();
        }

        private void cBoxComPort_DropDown(object sender, EventArgs e)
        {
            String[] ports = SerialPort.GetPortNames();
            cBoxComPort.Items.Clear();
            cBoxComPort.Items.AddRange(ports);
        }

        private void txtBoxFilter_TextChanged(object sender, EventArgs e)
        {
            recListBox.Items.Clear();
          foreach(string str in dataManagement.filteredData(reciveDataList, txtBoxFilter.Text))
            {
                recListBox.Items.Add(str);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            dataManagement.SaveData(reciveDataList);
        }
        #endregion


    }
}
