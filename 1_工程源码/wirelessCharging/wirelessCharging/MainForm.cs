using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using JiaHao.DX.FormBuild;
using DevExpress.XtraEditors;
using System.IO.Ports;
using Windows;
using System.Threading;
namespace wirelessCharging
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        CreateForm creatForm = new CreateForm();
        //万用表2
        private SerialPort Multi2Com = new SerialPort();
        private string MultiMeter2com;
        private string MultiMeter2BaudRate;
        private string MultiMeter2DataBit;
        private string MultiMeter2StopBit;
        private string MultiMeter2Parity;
        private int MultiMeter2ComMode;
        StringBuilder tempStringBuilder = new StringBuilder();      //全局可变字符串实例，用于读取配置文件内容
        private string strCurrentDirectory;                         //应用程序路径

        private static int btnCnt;
        public MainForm()
        {
            InitializeComponent();
            InitGlobleVariable();
        }
        public void InitGlobleVariable()
        {
            strCurrentDirectory = System.Environment.CurrentDirectory + "\\SetUp.ini";
            #region//测量模块( 万用表2 )
            //COM
            Win32API.GetPrivateProfileString("MultiMeter2", "COM", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("MultiMeter2", "COM", "COM1", strCurrentDirectory);
            }
            else
            {
                MultiMeter2com = tempStringBuilder.ToString();
            }
            Win32API.GetPrivateProfileString("MultiMeter2", "ComMode", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("MultiMeter2", "ComMode", "1", strCurrentDirectory);
            }
            else
            {
                MultiMeter2ComMode = Convert.ToInt16(tempStringBuilder.ToString());
            }

            Win32API.GetPrivateProfileString("MultiMeter2", "BaudRate", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("MultiMeter2", "BaudRate", "9600", strCurrentDirectory);
            }
            else
            {
                MultiMeter2BaudRate = tempStringBuilder.ToString();
            }
            Win32API.GetPrivateProfileString("MultiMeter2", "DataBit", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("MultiMeter2", "DataBit", "8", strCurrentDirectory);
            }
            else
            {
                MultiMeter2DataBit = tempStringBuilder.ToString();
            }
            Win32API.GetPrivateProfileString("MultiMeter2", "StopBit", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("MultiMeter2", "StopBit", "1", strCurrentDirectory);
            }
            else
            {
                MultiMeter2StopBit = tempStringBuilder.ToString();
            }
            Win32API.GetPrivateProfileString("MultiMeter2", "Parity", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("MultiMeter2", "Parity", "None", strCurrentDirectory);
            }
            else
            {
                MultiMeter2Parity = tempStringBuilder.ToString();
                switch (MultiMeter2Parity)
                {
                    case "None":
                        MultiMeter2Parity = "0";
                        break;
                    case "Odd":
                        MultiMeter2Parity = "1";
                        break;
                    case "Even":
                        MultiMeter2Parity = "2";
                        break;
                    case "Mark":
                        MultiMeter2Parity = "3";
                        break;
                    case "Space":
                        MultiMeter2Parity = "4";
                        break;

                    default:
                        MultiMeter2Parity = "0";
                        break;
                }
            }
            //配置并打开串口
            //SetSerialPort(Multi2Com, MultiMeter2com, MultiMeter2BaudRate, MultiMeter2DataBit, MultiMeter2StopBit, MultiMeter2Parity, MultiMeter2ComMode);
            //try
            //{
            //    Multi2Com.Open();
            //    //确保打开了万用表1的COM口后，初始化
            //    //string strConfM1V = "SYST:REM;CONF:VOLT:DC 5;CONF:AUTO 1;SENS:DET:RATE SLOW;SENS:TEMP:TCO:TYPE J;SENS:UNIT C;SENS:TEMP:RJUN:SIM 23;SENS:FREQ:INP 0;SENS:CONT:THR 10INP:IMP:AUTO 0;SYST:BEEP:STAT 2;TRIG:SOUR INT;TRIG:COUN 1;SAMP:COUN 1;CALC:FUNC OFF;CALC:STAT 0;CALC:MATH:MMF 1;CALC:MATH:MBF 0;CALC:MATH:PERC 1;CALC:REL:REF -.00001;CALC:DB:REF 0;CALC:DBM:REF 600;CALC:LIM:LOW -1;CALC:LIM:UPP 1;";//这句可ini配置
            //    //byte[] byteCmdConf = System.Text.Encoding.Default.GetBytes(strConfM1V);
            //    //MultiCom.Write(byteCmdConf, 0, byteCmdConf.Length);

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("测量模块 " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Environment.Exit(0);
            //    return;
            //}
            //Multi2Com.DataReceived += Multi2Com_DataReceived;
            #endregion
        }
        void Multi2Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(100);
            byte[] ReDatas = new byte[Multi2Com.BytesToRead];
            int ReLen = Multi2Com.Read(ReDatas, 0, ReDatas.Length);

            string strRead = new ASCIIEncoding().GetString(ReDatas);
            if (strRead.Contains("OK"))
            {
                XtraMessageBox.Show("测量模块自检成功");
            }
            else
            {
                XtraMessageBox.Show("测量模块自检失败");
            }


        }

        private void SetSerialPort(SerialPort comDevice, string com, string BaudRate, string DataBit, string StopBit, string Parity, int ComMode)
        {
            comDevice.PortName = com;
            comDevice.BaudRate = Convert.ToInt32(BaudRate);
            comDevice.DataBits = Convert.ToInt32(DataBit);
            comDevice.StopBits = (StopBits)Convert.ToInt32(StopBit);
            //ComDevice.Parity = (Parity)cbbParity.SelectedIndex;LoadMeterParity
            comDevice.Parity = (Parity)Convert.ToInt32(Parity);
            if (ComMode == 1)
            {
                comDevice.RtsEnable = true;
                comDevice.DtrEnable = true;
            }
            comDevice.ReadTimeout = 100;

        }
        private void btnTest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string testFormName = typeof(WpcTest).ToString();
                creatForm.AddTabpage(xtraTabControl1, "TabPageTest", "性能测试", testFormName);
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            creatForm.RemoveTabPage(xtraTabControl1, e);
        }

        private void btnPack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             try
            {
                string InboxFormName = typeof(InBox).ToString();
                creatForm.AddTabpage(xtraTabControl1, "TabPageInBox", "装箱检查", InboxFormName);
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             try
            {
                string SearchFormName = typeof(Search).ToString();
                creatForm.AddTabpage(xtraTabControl1, "TabPageSearch", "Log查询", SearchFormName);
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }            
        }

        private void btnSet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
             try
            {
                string SettingFormName = typeof(Setting).ToString();
                creatForm.AddTabpage(xtraTabControl1, "TabPageSetting", "参数设置", SettingFormName);
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }            
        }

        private void navBtnBarChart_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                string BarChartFormName = typeof(BarChart).ToString();
                creatForm.AddTabpage(xtraTabControl1, "TabPageBarChart", "直方图", BarChartFormName);
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }
        private void btnAdjust_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string AdjustFormName = typeof(Adjust).ToString();
                creatForm.AddTabpage(xtraTabControl1, "TabPageAdjust", "模块校准", AdjustFormName);
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }
        private void btnSelfChecking_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnCnt++;
            if (!Multi2Com.IsOpen)
            {
                try
                {
                    SetSerialPort(Multi2Com, MultiMeter2com, MultiMeter2BaudRate, MultiMeter2DataBit, MultiMeter2StopBit, MultiMeter2Parity, MultiMeter2ComMode);
                    Multi2Com.Open();
                    if (btnCnt==1)
                    {
                        Multi2Com.DataReceived += Multi2Com_DataReceived;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            string strCmdCheck = "AT\r\n";

            //string strCmdReadIin = "MEAS:VOLT:DC?;"; //"MEAS:CURR:DC?";//"MEAS:VOLT:DC?;";
            byte[] byteCmdCheck = System.Text.Encoding.Default.GetBytes(strCmdCheck);
            Multi2Com.Write(byteCmdCheck, 0, byteCmdCheck.Length);
            Thread.Sleep(1000);
            if (Multi2Com.IsOpen)
            {
                Multi2Com.Close();
            }

        }

    }
}
