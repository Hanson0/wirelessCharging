using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using System.IO;
using Windows;
using DevComponents.DotNetBar;
using System.Threading;
using MyCRC16;
using System.IO.Ports;
using JiaHao.RegexHelp;
using ClassMyLoad;
using IMEISNPrint;
using Newtonsoft.Json;
namespace wirelessCharging
{
    public partial class WpcTest : DevExpress.XtraEditors.XtraForm
    {
        public delegate void UpdateUI();                            //创建委托
        static UpdateUI updateUI;

        //testSn的存储
        private string strSn;

        //startMode
        private int startMode;

        //Load Port
        private SerialPort ComDevice = new SerialPort();
        //Multi Port
        private SerialPort MultiCom = new SerialPort();
        //Multi2 Port
        private SerialPort Multi2Com = new SerialPort();


        StringBuilder tempStringBuilder = new StringBuilder();      //全局可变字符串实例，用于读取配置文件内容
        private string strCurrentDirectory;                         //应用程序路径
        private string strParamDirectory;                         //应用程序路径
        //Infomation
        private string strLogPath;                      //log路径
        //Result
        private int iPass;
        private int iFail;

        private string SelectedI;
        private string[] SelectedIArry;
        private int Icount;
        //private float[][] intArray;
        private float[,] floatArray;
        private string SelectParam;

        public static bool updateSelectedI = true;

        //负载仪
        private string LoadMetercom;
        private string LoadMeterBaudRate;
        private string LoadMeterDataBit;
        private string LoadMeterStopBit;
        private string LoadMeterParity;
        private int LoadMeterComMode;

        private byte LoadAddr;
        //万用表1
        private string MultiMetercom;
        private string MultiMeterBaudRate;
        private string MultiMeterDataBit;
        private string MultiMeterStopBit;
        private string MultiMeterParity;
        private int MultiMeterComMode;
        //万用表2
        private string MultiMeter2com;
        private string MultiMeter2BaudRate;
        private string MultiMeter2DataBit;
        private string MultiMeter2StopBit;
        private string MultiMeter2Parity;
        private int MultiMeter2ComMode;

        //接收一帧正确的标志,设置电流成功
        private bool flag;
        //CC模式设置成功标志
        private bool flagCCMode;

        //测试项
        private int testIin;
        private int testVout;
        private int testEff;
        private int testFreq;
        //Length
        private int snLength;//Sn
        private int productNameLength;//productName

        //url
        private const string urlInquire = "/wirelesscharging/test/verify/";
        private const string urlUpload = "/wirelesscharging/test";

        //Iin
        private string strReadIin;//万用表测试的电流和频率用的 
        private bool isReadIin;

        private string strReadC;//模块测试的电流
        private bool isReadC;
        //Vin
        private string strReadVin;
        private bool isReadVin;
        //Vout
        private string strReadVout;
        private bool isReadVout;


        //Freq
        //private string strReadF;
        //private bool isReadF;

        //AdjustFactor
        private float AdjustFactor50mA;
        private float AdjustFactor100mA;
        private float AdjustFactor150mA;
        private float AdjustFactor200mA;
        private float AdjustFactor300mA;
        private float AdjustFactor400mA;
        private float AdjustFactor500mA;
        private float AdjustFactor600mA;
        private float AdjustFactor1000mA;

        private float defaultAdjustFactor = 1;
        //一项测试不通过 更新红点后，跳出for循环 
        private bool isBreak;
        //采样次数
        private int IinSamplingTime = 10;
        enum MyStartMode
        {
            DisConServer = 0,
            ConServer = 1
        }
        //扫SN第一次触发，注册Com口接受事件
        private static int btnCnt;
        private static int freqReceivedEventCnt;


        public WpcTest()
        {
            InitializeComponent();
            InitGlobleVariable();
        }
        public void InitGlobleVariable()
        {
            //!!!!!!!!!!!!!!上线后该地址要更改为：获取当前系统地址，再拼剪为formain的地址
            strCurrentDirectory = System.Environment.CurrentDirectory + "\\SetUp.ini";
            strParamDirectory = System.Environment.CurrentDirectory + "\\Param.txt";
            strLogPath = System.Environment.CurrentDirectory + "\\TestLog\\";
            if (!Directory.Exists(strLogPath))
            {
                Directory.CreateDirectory(strLogPath);
            }
            //HTTP
            Win32API.GetPrivateProfileString("Http", "IP", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Http", "IP", "192.168.1.25", strCurrentDirectory);
            }
            else
            {
                HttpServer.Ip = tempStringBuilder.ToString();
            }

            Win32API.GetPrivateProfileString("Http", "Port", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Http", "Port", "8088", strCurrentDirectory);
            }
            else
            {
                HttpServer.Port = tempStringBuilder.ToString();
            }
            //StartMode
            Win32API.GetPrivateProfileString("Porcess", "StartMode", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Porcess", "StartMode", "1", strCurrentDirectory);
            }
            else
            {
                startMode = int.Parse(tempStringBuilder.ToString());
                if (startMode == (int)MyStartMode.ConServer)
                {
                    //#region// 服务器连接 测试
                    //string msg;
                    //StringBuilder tempJson = new StringBuilder();
                    //ResponseInfo1 dataInfo1;

                    //try
                    //{
                    //    tempJson.Append(urlInquire);
                    //    tempJson.Append("123");

                    //    int result = HttpServer.GetInfo(tempJson.ToString(), out dataInfo1, out msg);
                    //    if (result != 0)
                    //    {
                    //        XtraMessageBox.Show("服务器连接 " + msg);
                    //        Environment.Exit(1);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    XtraMessageBox.Show(ex.Message);
                    //    Environment.Exit(1);
                    //}
                    //#endregion
                }
            }

            //校准系数 AdjustFactor
            #region
            //50mA
            Win32API.GetPrivateProfileString("AdjustFactor", "ADJ50mA", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("AdjustFactor", "ADJ50mA", "0.538", strCurrentDirectory);
            }
            else
            {
                AdjustFactor50mA = float.Parse(tempStringBuilder.ToString());
            }
            //100mA
            Win32API.GetPrivateProfileString("AdjustFactor", "ADJ100mA", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("AdjustFactor", "ADJ100mA", "0.677", strCurrentDirectory);
            }
            else
            {
                AdjustFactor100mA = float.Parse(tempStringBuilder.ToString());
            }
            //150mA
            Win32API.GetPrivateProfileString("AdjustFactor", "ADJ150mA", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("AdjustFactor", "ADJ150mA", "0.914", strCurrentDirectory);
            }
            else
            {
                AdjustFactor150mA = float.Parse(tempStringBuilder.ToString());
            }
            //200mA
            Win32API.GetPrivateProfileString("AdjustFactor", "ADJ200mA", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("AdjustFactor", "ADJ200mA", "0.956", strCurrentDirectory);
            }
            else
            {
                AdjustFactor200mA = float.Parse(tempStringBuilder.ToString());
            }
            //300mA
            Win32API.GetPrivateProfileString("AdjustFactor", "ADJ300mA", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("AdjustFactor", "ADJ300mA", "0.972", strCurrentDirectory);
            }
            else
            {
                AdjustFactor300mA = float.Parse(tempStringBuilder.ToString());
            }
            //400mA
            Win32API.GetPrivateProfileString("AdjustFactor", "ADJ400mA", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("AdjustFactor", "ADJ400mA", "1.009", strCurrentDirectory);
            }
            else
            {
                AdjustFactor400mA = float.Parse(tempStringBuilder.ToString());
            }
            //500mA
            Win32API.GetPrivateProfileString("AdjustFactor", "ADJ500mA", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("AdjustFactor", "ADJ500mA", "0.956", strCurrentDirectory);
            }
            else
            {
                AdjustFactor500mA = float.Parse(tempStringBuilder.ToString());
            }
            //600mA
            Win32API.GetPrivateProfileString("AdjustFactor", "ADJ600mA", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("AdjustFactor", "ADJ600mA", "0.94", strCurrentDirectory);
            }
            else
            {
                AdjustFactor600mA = float.Parse(tempStringBuilder.ToString());
            }
            //1000mA
            Win32API.GetPrivateProfileString("AdjustFactor", "ADJ1000mA", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("AdjustFactor", "ADJ1000mA", "0.965", strCurrentDirectory);
            }
            else
            {
                AdjustFactor1000mA = float.Parse(tempStringBuilder.ToString());
            }
            #endregion
            //测试项目
            #region
            //Iin
            Win32API.GetPrivateProfileString("TestItem", "TestIin", "", tempStringBuilder, 1024, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("TestItem", "TestIin", "0", strCurrentDirectory);
            }
            else
            {
                testIin = int.Parse(tempStringBuilder.ToString());
            }
            //Vout
            Win32API.GetPrivateProfileString("TestItem", "TestVout", "", tempStringBuilder, 1024, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("TestItem", "TestVout", "1", strCurrentDirectory);
            }
            else
            {
                testVout = int.Parse(tempStringBuilder.ToString());
            }
            //Eff
            Win32API.GetPrivateProfileString("TestItem", "TestEff", "", tempStringBuilder, 1024, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("TestItem", "TestEff", "1", strCurrentDirectory);
            }
            else
            {
                testEff = int.Parse(tempStringBuilder.ToString());
            }
            //Frq
            Win32API.GetPrivateProfileString("TestItem", "TestFreq", "", tempStringBuilder, 1024, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("TestItem", "TestFreq", "1", strCurrentDirectory);
            }
            else
            {
                testFreq = int.Parse(tempStringBuilder.ToString());
            }
            #endregion

            #region 仪表信息

            #region //负载仪
            //COM 读取
            Win32API.GetPrivateProfileString("LoadMeter", "COM", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("LoadMeter", "COM", "COM1", strCurrentDirectory);
            }
            else
            {
                LoadMetercom = tempStringBuilder.ToString();
            }
            Win32API.GetPrivateProfileString("LoadMeter", "ComMode", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("LoadMeter", "ComMode", "1", strCurrentDirectory);
            }
            else
            {
                LoadMeterComMode = Convert.ToInt16(tempStringBuilder.ToString());
            }

            Win32API.GetPrivateProfileString("LoadMeter", "BaudRate", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("LoadMeter", "BaudRate", "9600", strCurrentDirectory);
            }
            else
            {
                LoadMeterBaudRate = tempStringBuilder.ToString();
            }
            Win32API.GetPrivateProfileString("LoadMeter", "DataBit", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("LoadMeter", "DataBit", "8", strCurrentDirectory);
            }
            else
            {
                LoadMeterDataBit = tempStringBuilder.ToString();
            }
            Win32API.GetPrivateProfileString("LoadMeter", "StopBit", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("LoadMeter", "StopBit", "1", strCurrentDirectory);
            }
            else
            {
                LoadMeterStopBit = tempStringBuilder.ToString();
            }
            Win32API.GetPrivateProfileString("LoadMeter", "Parity", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("LoadMeter", "Parity", "None", strCurrentDirectory);
            }
            else
            {
                LoadMeterParity = tempStringBuilder.ToString();
                switch (LoadMeterParity)
                {
                    case "None":
                        LoadMeterParity = "0";
                        break;
                    case "Odd":
                        LoadMeterParity = "1";
                        break;
                    case "Even":
                        LoadMeterParity = "2";
                        break;
                    case "Mark":
                        LoadMeterParity = "3";
                        break;
                    case "Space":
                        LoadMeterParity = "4";
                        break;

                    default:
                        LoadMeterParity = "0";
                        break;
                }
            }
            //附加地址 读取
            Win32API.GetPrivateProfileString("LoadMeter", "Address", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("LoadMeter", "Address", "1", strCurrentDirectory);
            }
            else
            {
                LoadAddr = Convert.ToByte(tempStringBuilder.ToString(), 16);
            }

            //配置并打开串口
            SetSerialPort(ComDevice, LoadMetercom, LoadMeterBaudRate, LoadMeterDataBit, LoadMeterStopBit, LoadMeterParity, LoadMeterComMode);
            try
            {
                ComDevice.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("负载仪" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return;
            }
            ComDevice.DataReceived += ComDevice_DataReceived;
            #endregion

            #region//万用表1
            //COM
            Win32API.GetPrivateProfileString("MultiMeter", "COM", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("MultiMeter", "COM", "COM1", strCurrentDirectory);
            }
            else
            {
                MultiMetercom = tempStringBuilder.ToString();
            }
            Win32API.GetPrivateProfileString("MultiMeter", "ComMode", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("MultiMeter", "ComMode", "1", strCurrentDirectory);
            }
            else
            {
                MultiMeterComMode = Convert.ToInt16(tempStringBuilder.ToString());
            }

            Win32API.GetPrivateProfileString("MultiMeter", "BaudRate", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("MultiMeter", "BaudRate", "9600", strCurrentDirectory);
            }
            else
            {
                MultiMeterBaudRate = tempStringBuilder.ToString();
            }
            Win32API.GetPrivateProfileString("MultiMeter", "DataBit", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("MultiMeter", "DataBit", "8", strCurrentDirectory);
            }
            else
            {
                MultiMeterDataBit = tempStringBuilder.ToString();
            }
            Win32API.GetPrivateProfileString("MultiMeter", "StopBit", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("MultiMeter", "StopBit", "1", strCurrentDirectory);
            }
            else
            {
                MultiMeterStopBit = tempStringBuilder.ToString();
            }
            Win32API.GetPrivateProfileString("MultiMeter", "Parity", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("MultiMeter", "Parity", "None", strCurrentDirectory);
            }
            else
            {
                MultiMeterParity = tempStringBuilder.ToString();
                switch (MultiMeterParity)
                {
                    case "None":
                        MultiMeterParity = "0";
                        break;
                    case "Odd":
                        MultiMeterParity = "1";
                        break;
                    case "Even":
                        MultiMeterParity = "2";
                        break;
                    case "Mark":
                        MultiMeterParity = "3";
                        break;
                    case "Space":
                        MultiMeterParity = "4";
                        break;

                    default:
                        MultiMeterParity = "0";
                        break;
                }
            }
            if (testFreq == 1)
            {

                //配置并打开串口
                SetSerialPort(MultiCom, MultiMetercom, MultiMeterBaudRate, MultiMeterDataBit, MultiMeterStopBit, MultiMeterParity, MultiMeterComMode);
                try
                {
                    MultiCom.Open();
                    //确保打开了万用表1的COM口后，初始化
                    //string strConfM1V = "SYST:REM;CONF:VOLT:DC 5;CONF:AUTO 1;SENS:DET:RATE SLOW;SENS:TEMP:TCO:TYPE J;SENS:UNIT C;SENS:TEMP:RJUN:SIM 23;SENS:FREQ:INP 0;SENS:CONT:THR 10INP:IMP:AUTO 0;SYST:BEEP:STAT 2;TRIG:SOUR INT;TRIG:COUN 1;SAMP:COUN 1;CALC:FUNC OFF;CALC:STAT 0;CALC:MATH:MMF 1;CALC:MATH:MBF 0;CALC:MATH:PERC 1;CALC:REL:REF -.00001;CALC:DB:REF 0;CALC:DBM:REF 600;CALC:LIM:LOW -1;CALC:LIM:UPP 1;";//这句可ini配置
                    //byte[] byteCmdConf = System.Text.Encoding.Default.GetBytes(strConfM1V);
                    //MultiCom.Write(byteCmdConf, 0, byteCmdConf.Length);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("万用表1 " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                    return;
                }
                MultiCom.Close();//测试一下看能否正常打开
            }
            #endregion

            #region//万用表2
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
            if (testIin == 1)
            {

                //配置并打开串口
                SetSerialPort(Multi2Com, MultiMeter2com, MultiMeter2BaudRate, MultiMeter2DataBit, MultiMeter2StopBit, MultiMeter2Parity, MultiMeter2ComMode);
                try
                {
                    Multi2Com.Open();
                    //确保打开了万用表1的COM口后，初始化
                    //string strConfM1V = "SYST:REM;CONF:VOLT:DC 5;CONF:AUTO 1;SENS:DET:RATE SLOW;SENS:TEMP:TCO:TYPE J;SENS:UNIT C;SENS:TEMP:RJUN:SIM 23;SENS:FREQ:INP 0;SENS:CONT:THR 10INP:IMP:AUTO 0;SYST:BEEP:STAT 2;TRIG:SOUR INT;TRIG:COUN 1;SAMP:COUN 1;CALC:FUNC OFF;CALC:STAT 0;CALC:MATH:MMF 1;CALC:MATH:MBF 0;CALC:MATH:PERC 1;CALC:REL:REF -.00001;CALC:DB:REF 0;CALC:DBM:REF 600;CALC:LIM:LOW -1;CALC:LIM:UPP 1;";//这句可ini配置
                    //byte[] byteCmdConf = System.Text.Encoding.Default.GetBytes(strConfM1V);
                    //MultiCom.Write(byteCmdConf, 0, byteCmdConf.Length);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("测量模块 " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                    return;
                }
                Multi2Com.Close();
                //Multi2Com.DataReceived += Multi2Com_DataReceived;
            }
            #endregion

            #endregion

            //Set
            //选择的产品名
            Win32API.GetPrivateProfileString("Set", "UsedProductName", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Set", "UsedProductName", "pro1", strCurrentDirectory);
            }
            else
            {
                textProName.Text = tempStringBuilder.ToString();
                productNameLength = tempStringBuilder.ToString().Length;
            }
            ////产品名称长度
            //Win32API.GetPrivateProfileString("Set", "UsedProductName", "", tempStringBuilder, 256, strCurrentDirectory);
            //if (tempStringBuilder.ToString() == "")
            //{
            //    Win32API.WritePrivateProfileString("Set", "UsedProductName", "pro1", strCurrentDirectory);
            //}
            //else
            //{
            //    textProName.Text = tempStringBuilder.ToString();
            //}

            //选择的电流
            Win32API.GetPrivateProfileString("Set", "SelectedI", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Set", "SelectedI", "10mA", strCurrentDirectory);
            }
            else
            {
                SelectedI = tempStringBuilder.ToString();
                SelectedIArry = SelectedI.Split(';');
                Icount = SelectedIArry.Length - 1;
                CreatLabelIInGroup();

                //选择的参数范围
                #region
                try
                {
                    SelectParam = ReadTxt(strParamDirectory);
                    //textShowSelectParam.Text = SelectParam;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

                string[] strArrySP = SelectParam.Split('\n');

                string[][] strArray = new string[Icount][];
                for (int i = 0; i < Icount; i++)
                {
                    strArray[i] = strArrySP[i].Split(';');
                    //string str = "";
                    //for (int j = (i == 0 ? 0 : 8 * i-1); j < 8 * (i+1); j++)
                    //{
                    //    j
                    //    strArray[i][j] = strArrySP[j];
                    //}
                    //strArray[i] = new string[] { str };

                }
                floatArray = new float[Icount, 8];
                for (int i = 0; i < Icount; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (string.IsNullOrEmpty(strArray[i][j]))
                        {
                            XtraMessageBox.Show("您所选测试项，有参数规定范围未设置");
                            System.Environment.Exit(1);
                        }
                        floatArray[i, j] = Convert.ToSingle(strArray[i][j]);
                    }
                }
                #endregion

            }
            //Result
            #region
            Win32API.GetPrivateProfileString("Result", "TestPass", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Result", "TestPass", "3", strCurrentDirectory);
            }
            else
            {
                textPass.Text = tempStringBuilder.ToString();
                iPass = int.Parse(tempStringBuilder.ToString());
            }
            Win32API.GetPrivateProfileString("Result", "TestFail", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Result", "TestFail", "6", strCurrentDirectory);

            }
            else
            {
                textFail.Text = tempStringBuilder.ToString();
                iFail = int.Parse(tempStringBuilder.ToString());
                textTotal.Text = (iPass + iFail).ToString();
                if (iPass + iFail == 0)
                {
                    textRate.Text = "0.00%";
                }
                else
                {
                    textRate.Text = Math.Round((double)iPass * 100 / (iPass + iFail), 2).ToString() + "%";
                }
            }
            #endregion
            //Info
            Win32API.GetPrivateProfileString("Info", "StationNum", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Info", "StationNum", "2", strCurrentDirectory);
            }
            else
            {
                textWorkNum.Text = tempStringBuilder.ToString();
            }
            //Set
            Win32API.GetPrivateProfileString("Set", "AutoTestTime", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Set", "AutoTestTime", "7", strCurrentDirectory);
            }
            else
            {
                textDelayTime.Text = tempStringBuilder.ToString();
            }
            //Sn
            Win32API.GetPrivateProfileString("Sn", "length", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Sn", "length", "30", strCurrentDirectory);
            }
            else
            {
                snLength = Convert.ToInt16(tempStringBuilder.ToString());
            }

            //平均次数
            //取输入电流的平均次数
            Win32API.GetPrivateProfileString("Sampling", "IinSamplingTime", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Sampling", "IinSamplingTime", "10", strCurrentDirectory);
            }
            else
            {
                IinSamplingTime = Convert.ToInt16(tempStringBuilder.ToString());
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
        private void btnManual_Click(object sender, EventArgs e)
        {
            //关闭自动测试模式
            timer2.Enabled = false;
            //手动测试按键事件
            ManualEvent();
        }
        private void ManualEvent()
        {
            ////先判断SN号是否为空
            //if (string.IsNullOrEmpty(textSn.Text))
            //{
            //    //PutResult(false, "请输入15位的PCBA号");
            //    XtraMessageBox.Show("请扫描SN号");
            //    textSn.Focus();
            //    textSn.SelectAll();
            //    return;
            //}

            //产品型号是否对应
            //#region 判断SN的型号
            //if (textSn.Text.Length != snLength)
            //{
            //    JustUpdateResult(false, "SN:" + textSn.Text + " 的长度达不到预设长度：" + snLength + ",请检查");
            //    textSn.Focus();
            //    textSn.SelectAll();
            //    return;
            //}
            //string productName = textSn.Text.Substring(0, productNameLength);//13
            //if (textProName.Text.Trim() != productName)
            //{
            //    JustUpdateResult(false, "SN:" + textSn.Text + "与型号：" + textProName.Text + "不符,请检查");
            //    textSn.Focus();
            //    textSn.SelectAll();
            //    return;
            //}
            //#endregion

            //写入工号
            Win32API.WritePrivateProfileString("Info", "StationNum", textWorkNum.Text, strCurrentDirectory);

            SetPictureColorFromControls(groupTestRet);

            //Log清空
            txtShowData.Text = "";
            txtShowData.BackColor = Color.White;
            //最终结果清零
            //labResult.BackColor = Color.Transparent;//Color.White;
            //labResult.Text = "";

            Thread Thread = new Thread(new ThreadStart(testThread));
            Thread.IsBackground = true;
            Thread.Start();
        }


        private void testThread()
        {
            //串口发送数据
            //foreach (var item in SelectedIArry)
            //{

            //}
            //int result;
            string[] strSelectedIArry = new string[Icount];
            float[] floatIArry = new float[Icount];
            for (int i = 0; i < Icount; i++)
            {

                try
                {
                    strSelectedIArry[i] = SelectedIArry[i].Replace("mA", "");
                    floatIArry[i] = Convert.ToSingle(strSelectedIArry[i]);
                    //XtraMessageBox.Show(strSelectedIArry[i]);

                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                    System.Environment.Exit(1);
                }
            }
            //flag = true;
            TestData testData = new TestData()
            {
                #region
                Lin50mA = "",
                Vout50mA = "",
                Fre50mA = "",
                Eff50mA = "",

                Lin100mA = "",
                Vout100mA = "",
                Fre100mA = "",
                Eff100mA = "",

                Lin150mA = "",
                Vout150mA = "",
                Fre150mA = "",
                Eff150mA = "",

                Lin200mA = "",
                Vout200mA = "",
                Fre200mA = "",
                Eff200mA = "",

                Lin300mA = "",
                Vout300mA = "",
                Fre300mA = "",
                Eff300mA = "",

                Lin400mA = "",
                Vout400mA = "",
                Fre400mA = "",
                Eff400mA = "",

                Lin500mA = "",
                Vout500mA = "",
                Fre500mA = "",
                Eff500mA = "",

                Lin600mA = "",
                Vout600mA = "",
                Fre600mA = "",
                Eff600mA = "",

                Lin1000mA = "",
                Vout1000mA = "",
                Fre1000mA = "",
                Eff1000mA = ""
                #endregion
            };
            //循环设置负载仪电流 进行 测试
            //for (int i = 0; i < floatIArry.Length; i++)
            //{
            for (int i = floatIArry.Length - 1; i >= 0; i--)
            {
                #region
                //组合负载仪固定部分
                //byte[] frameSetI = new byte[11];//LoadAddr
                //byte[] fixedI = { LoadAddr, 0x10, 0x0A, 0x01, 0x00, 0x02 };
                //Buffer.BlockCopy(fixedI, 0, frameSetI, 0, fixedI.Length);

                ////计算 Ilen 、 HexItemI、CRC

                ////每一项电流值对应的16进制;300mA?就是300?或0.3?
                //byte[] HexItemI = FloatToHex(floatIArry[i] / 1000.0f);
                ////获取2.3转换成Hex后有几个字节
                //byte[] frameIlen = BitConverter.GetBytes(HexItemI.Length);
                //byte Ilen = frameIlen[0];
                //Buffer.BlockCopy(frameIlen, 0, frameSetI, 6, 1);
                //Buffer.BlockCopy(HexItemI, 0, frameSetI, 7, HexItemI.Length);
                //#region List的方式更高效
                ////List<byte> byteSource = new List<byte>();  
                ////byteSource.Add(11);  
                ////Stopwatch sw = new Stopwatch();  
                ////sw.Start();  
                ////for (int i = 0; i < RunCount; i++)  
                ////{  
                ////    byte[] newData = new byte[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };  
                ////    byteSource.AddRange(newData);  
                //#endregion

                ////重合后计算CRC
                //byte[] bytesCrc = CRC.CRC16(frameSetI, true);
                //byte[] bytesSetI = new byte[13];
                ////重合前面(除CRC)给新byte[]
                //Buffer.BlockCopy(frameSetI, 0, bytesSetI, 0, frameSetI.Length);
                ////重合CRC
                //Buffer.BlockCopy(bytesCrc, 0, bytesSetI, 11, bytesCrc.Length);
                #endregion
                //获取设置电流的请求帧
                byte[] bytesSetI = MyLoad.SetIRequest(LoadAddr, floatIArry[i]);

                //串口发送:设置电流的字节数组

                //负载仪
                if (!ComDevice.IsOpen)
                {
                    try
                    {
                        SetSerialPort(ComDevice, LoadMetercom, LoadMeterBaudRate, LoadMeterDataBit, LoadMeterStopBit, LoadMeterParity, LoadMeterComMode);
                        ComDevice.Open();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (testFreq == 1)
                {
                    //万用表1 com检验和初始化通道
                    if (!MultiCom.IsOpen)
                    {
                        freqReceivedEventCnt++;
                        try
                        {
                            SetSerialPort(MultiCom, MultiMetercom, MultiMeterBaudRate, MultiMeterDataBit, MultiMeterStopBit, MultiMeterParity, MultiMeterComMode);
                            MultiCom.Open();
                            if (freqReceivedEventCnt == 1)
                            {
                                MultiCom.DataReceived += MultiCom_DataReceived;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("万用表1" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                if (testIin == 1)
                {
                    //万用表2 Com检验
                    if (!Multi2Com.IsOpen)
                    {
                        btnCnt++;
                        try
                        {
                            SetSerialPort(Multi2Com, MultiMeter2com, MultiMeter2BaudRate, MultiMeter2DataBit, MultiMeter2StopBit, MultiMeter2Parity, MultiMeter2ComMode);

                            Multi2Com.Open();
                            if (btnCnt == 1)
                            {
                                Multi2Com.DataReceived += Multi2Com_DataReceived;
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("测量模块" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                //发送
                //ComDevice.RtsEnable = true;
                //ComDevice.DtrEnable = false;

                int retry = 5;
            REDO:
                AddContent("\r\n负载仪设置电流：" + floatIArry[i].ToString() + "mA");//显示Iin
            try
            {
                ComDevice.Write(bytesSetI, 0, bytesSetI.Length);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }

                Thread.Sleep(500);
                if (flag)
                {
                    flag = false;
                    int ret = 0;
                    //接收到正确回复帧才读取万用表，显示该项测试结果
                    //读取万用表1的值 ——值显示到Log中
                    //输入电流
                    #region 负载仪定电流模式设置
                    int retryCC = 2;

                REDOCC:
                    AddContent("负载仪定电流模式设置...");//显示Iin
                    byte[] byteCC = { 0x01, 0x10, 0x0A, 0x00, 0x00, 0x01, 0x02, 0x00, 0x01, 0xCD, 0x90 };
                    ComDevice.Write(byteCC, 0, byteCC.Length);
                    Thread.Sleep(500);
                    if (flagCCMode)
                    {
                        flagCCMode = false;

                    }
                    else
                    {
                        retryCC--;
                        if (retryCC >= 0)
                        {
                            goto REDOCC;
                        }
                    }
                    #endregion

                    #region 无 一直等待读取到万用表Iin数据,没有读出来卡在这里,负载仪也不会设置电流
#if MULTIMETER
                    string strCmdReadIin = "CONF:CURR:DC 0.0005;CONF:AUTO 1;SENS:DET:RATE SLOW;SENS:TEMP:TCO:TYPE J;SENS:UNIT C;SENS:TEMP:RJUN:SIM 23;SENS:FREQ:INP 0;SENS:CONT:THR 10;INP:IMP:AUTO 0;SYST:BEEP:STAT 2;TRIG:SOUR INT;TRIG:COUN 1;SAMP:COUN 1;CALC:FUNC OFF;CALC:STAT 0;CALC:MATH:MMF 1;CALC:MATH:MBF 0;CALC:MATH:PERC 1;CALC:REL:REF -.00001;CALC:DB:REF 0;CALC:DBM:REF 600;CALC:LIM:LOW -1;CALC:LIM:UPP 1;";
                    string TEST = "MEAS:CURR:DC?;";
                    //string strCmdReadIin = "MEAS:VOLT:DC?;"; //"MEAS:CURR:DC?";//"MEAS:VOLT:DC?;";
                    byte[] byteCmdRIi = System.Text.Encoding.Default.GetBytes(strCmdReadIin);
                    byte[] byteTEST = System.Text.Encoding.Default.GetBytes(TEST);

                    MultiCom.Write(byteCmdRIi, 0, byteCmdRIi.Length);
                    AddContent("万用表1 输入电流Iin测量中...");//显示Iin
                    Thread.Sleep(1000);
                    MultiCom.Write(byteTEST, 0, byteTEST.Length);
                    while (!isReadIin)
                    {
                        ;
                    }
                    //出循环后说明isReadIin==true,strReadIin 读到了输入电流值
                    isReadIin = false;

                    //解析 +1.0e-4 和设定范围做比较 得出ret=?
                    float floatIin = float.Parse(strReadIin);
                    float minIin = floatArray[i, 0];
                    float maxIin = floatArray[i, 1];

                    if (floatIin < minIin || floatIin > maxIin)
                    {
                        ret = -1;
                        AddContent("FAIL 输入电流(" + minIin.ToString() + "~" + maxIin.ToString() + ")：" + strReadIin.Replace("\r\n", ""));//显示Iin
                    }
                    else
                    {
                        AddContent("PASS 输入电流(" + minIin.ToString() + "~" + maxIin.ToString() + ")：" + strReadIin.Replace("\r\n", ""));//显示Iin
                    }

                    //给testData赋Iin值，目的拼接data字符串
                    SetStrData(SelectedIArry[i], 1, testData, floatIin);
#endif
                    #endregion

                    if (testFreq == 1)
                    {
                        #region 等待万用表1读 f
                        string strCmdReadF = "CONF:FREQ 0.5;CONF:AUTO 1;SENS:DET:RATE SLOW;SENS:TEMP:TCO:TYPE J;SENS:UNIT C;SENS:TEMP:RJUN:SIM 23;SENS:FREQ:INP 0;SENS:CONT:THR 10;INP:IMP:AUTO 0;SYST:BEEP:STAT 2;TRIG:SOUR INT;TRIG:COUN 1;SAMP:COUN 1;CALC:FUNC OFF;CALC:STAT 0;CALC:MATH:MMF 1;CALC:MATH:MBF 0;CALC:MATH:PERC 1;CALC:REL:REF -.00001;CALC:DB:REF 0;CALC:DBM:REF 600;CALC:LIM:LOW -1;CALC:LIM:UPP 1;MEAS:FREQ?;";
                        //string strCmdReadIin = "MEAS:VOLT:DC?;"; //"MEAS:CURR:DC?";//"MEAS:VOLT:DC?;";
                        byte[] byteCmdRF = System.Text.Encoding.Default.GetBytes(strCmdReadF);

                        MultiCom.Write(byteCmdRF, 0, byteCmdRF.Length);
                        AddContent("万用表1 频率测量中...");//显示Iin
                        while (!isReadIin)
                        {
                            ;
                        }
                        //出循环后说明isReadIin==true,strReadIin 读到了输入电流值
                        isReadIin = false;

                        //解析 +1.0e-4 和设定范围做比较 得出ret=?
                        float floatF = float.Parse(strReadIin);//共用strReadIin,因为同一个COM口接收
                        float minF = floatArray[i, 6];
                        float maxF = floatArray[i, 7];
                        //给testData赋Fre值，目的拼接data字符串
                        SetStrData(SelectedIArry[i], 4, testData, floatF);//频率赋值

                        if (floatF < minF || floatF > maxF)
                        {
                            ret = -1;
                            AddContent("频率(" + minF.ToString() + "~" + maxF.ToString() + ")：" + strReadIin.Replace("\r\n", "Hz\tFAIL"));//显示Iin
                            //result = 0;
                            isBreak = true;
                            goto END;
                        }
                        else
                        {
                            AddContent("频率(" + minF.ToString() + "~" + maxF.ToString() + ")：" + strReadIin.Replace("\r\n", "Hz\tPASS"));//显示Iin
                        }

                        #endregion
                    }


                    #region 等取到万用表2 Vout 无
#if MULTIMETER
                    string strCmdReadV = "SYST:REM;CONF:VOLT:DC 5;CONF:AUTO 1;SENS:DET:RATE SLOW;SENS:TEMP:TCO:TYPE J;SENS:UNIT C;SENS:TEMP:RJUN:SIM 23;SENS:FREQ:INP 0;SENS:CONT:THR 10INP:IMP:AUTO 0;SYST:BEEP:STAT 2;TRIG:SOUR INT;TRIG:COUN 1;SAMP:COUN 1;CALC:FUNC OFF;CALC:STAT 0;CALC:MATH:MMF 1;CALC:MATH:MBF 0;CALC:MATH:PERC 1;CALC:REL:REF -.00001;CALC:DB:REF 0;CALC:DBM:REF 600;CALC:LIM:LOW -1;CALC:LIM:UPP 1;MEAS:VOLT:DC?;";//这句可ini配置
                    byte[] byteCmdRV = System.Text.Encoding.Default.GetBytes(strCmdReadV);

                    Multi2Com.Write(byteCmdRV, 0, byteCmdRV.Length);
                    AddContent("万用表2 输出电压Vout测量中...");//显示Iin
                    while (!isReadVout)
                    {
                        ;
                    }
                    //出循环后说明isReadIin==true,strReadIin 读到了输入电流值
                    isReadVout = false;

                    //解析 +1.0e-4 和设定范围做比较 得出ret=?
                    float floatVout = float.Parse(strReadVout);
                    float minVout = floatArray[i, 2];
                    float maxVout = floatArray[i, 3];

                    if (floatVout < minVout || floatVout > maxVout)
                    {
                        ret = -1;
                        AddContent("FAIL 输出电压(" + minVout.ToString() + "~" + maxVout.ToString() + ")：" + strReadVout.Replace("\r\n", ""));//显示Vout
                    }
                    else
                    {
                        AddContent("PASS 输出电压(" + minVout.ToString() + "~" + maxVout.ToString() + ")：" + strReadVout.Replace("\r\n", ""));//显示Vout
                    }

                    //给testData赋Iin值，目的拼接data字符串
                    SetStrData(SelectedIArry[i], 2, testData, floatVout);//电压赋值
#endif
                    #endregion



                    float floatVout = 0;
                    if (testVout == 1)
                    {
                        #region 负载仪 读取Vout输出电压

                        int retryVout = 2;
                        //获取电压的请求帧
                        byte[] bytesCmdReadV = { LoadAddr, 0x03, 0x0B, 0x00, 0x00, 0x02, 0xC6, 0x2F };
                    REDOVOUT:
                        AddContent("负载仪读取输出电压Vout中...");//显示Iin
                        ComDevice.Write(bytesCmdReadV, 0, bytesCmdReadV.Length);

                        Thread.Sleep(800);//500
                        if (isReadVout)
                        {
                            isReadVout = false;
                            try
                            {
                                floatVout = float.Parse(strReadVout);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message);
                                goto REDOVOUT;
                            }
                            float minVout = floatArray[i, 2];
                            float maxVout = floatArray[i, 3];

                            //给testData赋Iin值，目的拼接data字符串
                            SetStrData(SelectedIArry[i], 2, testData, floatVout);//电压赋值

                            if (floatVout < minVout || floatVout > maxVout)
                            {
                                ret = -1;
                                AddContent("输出电压(" + minVout.ToString() + "~" + maxVout.ToString() + ")：" + strReadVout + "V\tFAIL");//显示Vout
                                //result = 0;
                                isBreak = true;
                                goto END;
                            }
                            else
                            {
                                AddContent("输出电压(" + minVout.ToString() + "~" + maxVout.ToString() + ")：" + strReadVout + "V\tPASS");//显示Vout
                            }

                        }
                        else
                        {
                            retryVout--;
                            if (retryVout >= 0)
                            {
                                goto REDOVOUT;
                            }
                        }
                        #endregion
                    }

                    float floatIin = 0;
                    float floatVin = 0;
                    if (testIin == 1)
                    {
                        #region 测试模块 输入电压—相当于万2
#if MULTIMETER
#else

                        string strCmdReadV = "AT+V\r\n";//这句可ini配置
                        byte[] byteCmdRV = System.Text.Encoding.Default.GetBytes(strCmdReadV);

                        Multi2Com.Write(byteCmdRV, 0, byteCmdRV.Length);
                        AddContent("测量模块 输入电压Vin测量中...");//显示Iin
                        while (!isReadVin)
                        {
                            ;
                        }
                        //出循环后说明isReadIin==true,strReadIin 读到了输入电流值
                        isReadVin = false;

                        //解析 +1.0e-4 和设定范围做比较 得出ret=?
                        floatVin = float.Parse(strReadVin);
                        //float minVout = floatArray[i, 2];
                        //float maxVout = floatArray[i, 3];
                        AddContent("输入电压：" + strReadVin + "V");//显示Vout
                        //if (floatVin < minVout || floatVin > maxVout)
                        //{
                        //    ret = -1;
                        //    AddContent("输入电压(" + minVout.ToString() + "~" + maxVout.ToString() + ")：" + floatVin.ToString() + "V\tFAIL");//显示Vout
                        //}
                        //else
                        //{
                        //    AddContent("输入电压(" + minVout.ToString() + "~" + maxVout.ToString() + ")：" + floatVin.ToString() + "V\tPASS");//显示Vout
                        //}

                        ////给testData赋Iin值，目的拼接data字符串
                        //SetStrData(SelectedIArry[i], 2, testData, floatVin);//电压赋值
#endif
                        #endregion


                        #region 测试模块 测试电流—相当于万2
#if MULTIMETER
#else
                        string strCmdRIi = "AT+C\r\n";

                        //string strCmdReadIin = "MEAS:VOLT:DC?;"; //"MEAS:CURR:DC?";//"MEAS:VOLT:DC?;";
                        byte[] byteCmdRIi = System.Text.Encoding.Default.GetBytes(strCmdRIi);
                        AddContent("测量模块 输入电流Iin测量中...");//显示Iin
                        #region 校准系数设置
                        if (floatIArry[i] == 50)
                        {
                            defaultAdjustFactor = AdjustFactor50mA;
                        }
                        else if (floatIArry[i] == 100)
                        {
                            defaultAdjustFactor = AdjustFactor100mA;
                        }
                        else if (floatIArry[i] == 150)
                        {
                            defaultAdjustFactor = AdjustFactor150mA;
                        }
                        else if (floatIArry[i] == 200)
                        {
                            defaultAdjustFactor = AdjustFactor200mA;
                        }
                        else if (floatIArry[i] == 300)
                        {
                            defaultAdjustFactor = AdjustFactor300mA;
                        }
                        else if (floatIArry[i] == 400)
                        {
                            defaultAdjustFactor = AdjustFactor400mA;
                        }
                        else if (floatIArry[i] == 500)
                        {
                            defaultAdjustFactor = AdjustFactor500mA;
                        }
                        else if (floatIArry[i] == 600)
                        {
                            defaultAdjustFactor = AdjustFactor600mA;
                        }
                        else if (floatIArry[i] == 1000)
                        {
                            defaultAdjustFactor = AdjustFactor1000mA;
                        }
                        else
                        {
                            defaultAdjustFactor = 1;
                        }
                        #endregion

                        float[] floatIinArry = new float[IinSamplingTime];
                        //取电流的平均值
                        for (int j = 0; j < IinSamplingTime; j++)
                        {
                            Multi2Com.Write(byteCmdRIi, 0, byteCmdRIi.Length);
                            while (!isReadC)
                            {
                                ;
                            }
                            //出循环后说明isReadIin==true,strReadIin 读到了输入电流值
                            isReadC = false;
                            floatIinArry[j] = float.Parse(strReadC) * defaultAdjustFactor;

                        }
                        floatIin = CRC.GetAverage(floatIinArry, IinSamplingTime);
                        //解析 +1.0e-4 和设定范围做比较 得出ret=?

                        float minIin = floatArray[i, 0];
                        float maxIin = floatArray[i, 1];
                        //给testData赋Iin值，目的拼接data字符串
                        SetStrData(SelectedIArry[i], 1, testData, floatIin);

                        if (floatIin < minIin || floatIin > maxIin)
                        {
                            ret = -1;
                            AddContent("输入电流(" + minIin.ToString() + "~" + maxIin.ToString() + ")：" + (floatIin * 1000).ToString() + "mA\tFAIL");//显示Iin
                            //result = 0;
                            isBreak = true;

                            goto END;
                        }
                        else
                        {
                            AddContent("输入电流(" + minIin.ToString() + "~" + maxIin.ToString() + ")：" + (floatIin * 1000).ToString() + "mA\tPASS");//显示Iin
                        }

#endif
                        #endregion
                    }

                    if (testEff == 1)
                    {
                        #region //计算EFF值
                        float floatEff = (floatVout * floatIArry[i] / 1000) / (floatVin * floatIin);
                        float minEff = floatArray[i, 4];
                        float maxEff = floatArray[i, 5];

                        SetStrData(SelectedIArry[i], 3, testData, floatEff);//EFF Json赋值

                        if (floatEff < minEff || floatEff > maxEff)
                        {
                            ret = -1;
                            AddContent("转换效率EFF(" + minEff.ToString() + "~" + maxEff.ToString() + ")：" + floatEff + "\tFAIL \r\n");//显示Vout
                            //result = 0;
                            isBreak = true;
                            goto END;
                        }
                        else
                        {
                            AddContent("转换效率EFF(" + minEff.ToString() + "~" + maxEff.ToString() + ")：" + floatEff + "\tPASS \r\n");//显示Vout
                        }
                        #endregion
                    }


                    //AddContent(Value1);
                //在范围外
                //if(Value<Is && Value>Ie)
                //{
                //   ret=-1;
                //}
                ////读取万用表2的值  ——值显示到Log中 
                //AddContent(Value2);
                //if(>Vs&&<Ve&&...)
                //{
                //   ret=-1;
                //}
                ////计算EFF值,
                //if(>Es&&<Ee&&...)
                //{
                //   ret=-1;
                //}

                END:
                    #region 根据万用表结果显示对应颜色结果
                    //在Group中查询出与名字相同的控件  //因 第一次 i=5 picture5
                    PictureBox pb = GetPictureControlFromGroup("picture" + (floatIArry.Length - 1 - i).ToString(), groupTestRet);
                    if (pb == null)
                    {
                        XtraMessageBox.Show("无法设置" + floatIArry[i].ToString() + "mA测试项结果，原因：未找到" + "picture" + (floatIArry.Length - i).ToString());
                        continue;
                    }
                    //Control control = GetControl("picture" + i.toString(), pictureGroup);
                    if (ret != 0)
                    {
                        //在Group中查询
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            pb.Image = Properties.Resources.红点中;
                            pb.BackColor = Color.Gainsboro;
                        }));
                        if (isBreak)
                        {
                            isBreak = false;
                            break;
                        }
                        //"picture"+i.toString()
                    }
                    else
                    {
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            pb.Image = Properties.Resources.绿点中;
                            pb.BackColor = Color.Transparent;
                        }));


                    }
                    #endregion

                }
                else
                {
                    retry--;
                    if (retry >= 0)
                    {
                        goto REDO;
                    }
                }

                //                #region 发送
                //                int tryCnt = 0;
                //                //尝试2*1000ms
                //                while (tryCnt++ < 3)
                //                {
                //                    if (flag)
                //                    {
                //                        //接收到了正确的回复帧,确定设置电流成功了,开始读取万用表,并判断数据显示结果，然后进行下一项设置
                //                        flag = false;
                //                        int ret = -1;
                //                        //ComDevice.Write(bytesSetI, 0, bytesSetI.Length);
                //                        //读取万用表1的值 ——要不要用表格的形式显示出来？可以先不显示
                //                        //在范围外
                //                        //if(Value<Is && Value>Ie)
                //                        //{
                //                        //   ret=-1;
                //                        //}
                //                        ////读取万用表2的值   
                //                        //if(>Vs&&<Ve&&...)
                //                        //{
                //                        //   ret=-1;
                //                        //}
                //                        ////计算EFF值,
                //                        //if(>Es&&<Ee&&...)
                //                        //{
                //                        //   ret=-1;
                //                        //}
                //                        #region
                //                        //在Group中查询出与名字相同的控件
                //                        PictureBox pb = GetPictureControlFromGroup("picture" + i.ToString(), groupTestRet);
                //                        if (pb == null)
                //                        {
                //                            XtraMessageBox.Show("无法设置" + floatIArry[i].ToString() + "mA测试项结果，原因：未找到" + "picture" + i.ToString());
                //                            continue;
                //                        }
                //                        //Control control = GetControl("picture" + i.toString(), pictureGroup);
                //                        if (ret != 0)
                //                        {
                //                            //在Group中查询
                //                            //this.BeginInvoke(new MethodInvoker(delegate
                //                            //{
                //                            //    pb.Image = Properties.Resources.红点中;
                //                            //    pb.BackColor = Color.Gainsboro;
                //                            //}));
                //                            Action action = delegate
                //                            {
                //                                pb.Image = Properties.Resources.红点中;
                //                                pb.BackColor = Color.Gainsboro;
                //                            };
                //                            pb.Invoke(action);


                //                            //"picture"+i.toString()
                //                        }
                //                        else
                //                        {
                //                            this.BeginInvoke(new MethodInvoker(delegate
                //                            {
                //                                pb.Image = Properties.Resources.绿点中;
                //                                pb.BackColor = Color.Transparent;
                //                            }));


                //                        }
                //                        #endregion
                //                        break;
                //                    }
                //                    ComDevice.Write(bytesSetI, 0, bytesSetI.Length);
                //                    Thread.Sleep(1000);
                //                }
                //#endregion




                //#region
                //if (flag)
                //{
                //    flag = false;
                //    int ret = -1;
                //    ComDevice.Write(bytesSetI, 0, bytesSetI.Length);
                //    //读取万用表1的值 ——要不要用表格的形式显示出来？可以先不显示
                //    //在范围外
                //    //if(Value<Is && Value>Ie)
                //    //{
                //    //   ret=-1;
                //    //}
                //    ////读取万用表2的值   
                //    //if(>Vs&&<Ve&&...)
                //    //{
                //    //   ret=-1;
                //    //}
                //    ////计算EFF值,
                //    //if(>Es&&<Ee&&...)
                //    //{
                //    //   ret=-1;
                //    //}
                //    #region
                //    //在Group中查询出与名字相同的控件
                //    PictureBox pb = GetPictureControlFromGroup("picture" + i.ToString(), groupTestRet);
                //    if (pb == null)
                //    {
                //        XtraMessageBox.Show("无法设置" + floatIArry[i].ToString() + "mA测试项结果，原因：未找到" + "picture" + i.ToString());
                //        continue;
                //    }
                //    //Control control = GetControl("picture" + i.toString(), pictureGroup);
                //    if (ret != 0)
                //    {
                //        //在Group中查询
                //        this.BeginInvoke(new MethodInvoker(delegate
                //        {
                //            pb.Image = Properties.Resources.红点中;
                //            pb.BackColor = Color.Gainsboro;
                //        }));

                //        //"picture"+i.toString()
                //    }
                //    else
                //    {
                //        this.BeginInvoke(new MethodInvoker(delegate
                //        {
                //            pb.Image = Properties.Resources.绿点中;
                //            pb.BackColor = Color.Transparent;
                //        }));


                //    }
                //    #endregion
                //}
                //else
                //{
                //    //尝试2*1000ms
                //    while (tryCnt++ < 3)
                //    {
                //        if (flag)
                //        {
                //            flag = false;
                //            break;
                //        }
                //        ComDevice.Write(bytesSetI, 0, bytesSetI.Length);
                //        Thread.Sleep(500);
                //    }

                //}
                //#endregion

                //Thread.Sleep(2000);

            }
            //获取所有pictureBox中的控件,判断图片是否相等，是否存在红中点或白中点（没有接收到正确帧,有异常帧），如果存在，则最终结果为Fail
            Thread.Sleep(300);
            int result = GetRetFromGroup(groupTestRet);

            if (startMode == (int)MyStartMode.ConServer)
            {
                #region 含有服务器的情况
                if (result == -1)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        //labResult.BackColor = Color.Orange;
                        //labResult.Text = "WARN";
                        AddContent("WARN 警告 有测试项电流设置失败");
                        txtShowData.BackColor = Color.Orange;
                    }));
                }
                else
                {
                    #region 上报服务器
                    string msg;
                    StringBuilder tempJson = new StringBuilder();
                    ResponseSimpleInfo rpSimpleInfo;


                    try
                    {
                        string strData = JsonConvert.SerializeObject(testData);

                        PostTestInfo postTestInfo = new PostTestInfo()
                        {
                            sn = strSn,
                            data = strData,
                            result = result
                        };

                        //额外的接口
                        int ret = HttpServer.PostSimpleInfo(urlUpload, out rpSimpleInfo, out msg, postTestInfo);
                        if (ret != 0)
                        {
                            //MessageBox.Show(msg);
                            this.BeginInvoke(new MethodInvoker(delegate
                            {

                                //labResult.BackColor = Color.Red;
                                //labResult.Text = "FAIL";
                                PutResult(false, msg);
                                textSn.Enabled = true;
                            }));
                            return;
                        }
                        if (rpSimpleInfo.code != 1000)
                        {
                            //MessageBox.Show(msg);
                            this.BeginInvoke(new MethodInvoker(delegate
                            {

                                //labResult.BackColor = Color.Red;
                                //labResult.Text = "FAIL";
                                PutResult(false, msg);
                            }));

                        }
                        else
                        {
                            //数据上报肯定成功了，但是测试结果有可能是Fail
                            if (result == 1)
                            {
                                this.BeginInvoke(new MethodInvoker(delegate
                                {
                                    //labResult.BackColor = Color.Green;
                                    //labResult.Text = "PASS";
                                    //AddContent("数据上报成功");
                                    PutResult(true, "数据上报成功");
                                }));
                            }
                            else if (result == 0)
                            {
                                this.BeginInvoke(new MethodInvoker(delegate
                                {

                                    //labResult.BackColor = Color.Red;
                                    //labResult.Text = "FAIL";
                                    PutResult(false, "FAIL");
                                }));

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        JustUpdateResult(false, ex.Message);
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            textSn.Focus();
                            textSn.Text = "";
                        }));
                    }
                    #endregion
                }
                #endregion
            }
            else if (startMode == (int)MyStartMode.DisConServer)
            {
                #region 不含服务器
                if (result == 0)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        //labResult.BackColor = Color.Red;
                        //labResult.Text = "FAIL";
                        //txtShowData.AppendText("FAIL");
                        //txtShowData.BackColor = Color.Red;
                        PutResult(false, "FAIL");
                    }));
                }
                else if (result == -1)
                {

                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        //labResult.BackColor = Color.Orange;
                        //labResult.Text = "WARN";
                        AddContent("WARN 警告 有测试项电流设置失败");
                        //txtShowData.AppendText("有测试项电流设置失败");
                        txtShowData.BackColor = Color.Orange;
                    }));


                    //XtraMessageBox.Show("有测试项电流设置失败");
                }
                else if (result == 1)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        //labResult.BackColor = Color.Green;
                        //labResult.Text = "PASS";

                        PutResult(true, "PASS");
                        //txtShowData.BackColor = Color.Green;
                    }));

                }
                else
                {
                    XtraMessageBox.Show("从pictureGroup中获取最终结果异常");
                }
                #endregion
            }

            this.BeginInvoke(new MethodInvoker(delegate
            {
                textSn.Enabled = true;
                textSn.Focus();
                textSn.Text = "";
            }));
            if (Multi2Com.IsOpen)
            {
                Multi2Com.Close();//最后测试完成后，关闭串口

            }

        }
        /// <summary>
        /// data对象赋值
        /// </summary>
        /// <param name="selectedI"></param>
        /// <param name="testItem"></param>
        /// <param name="testData"></param>
        /// <param name="value"></param>
        private void SetStrData(string selectedI, int testItem, TestData testData, float value)
        {
            if (testItem == 1)//Iin
            {
                switch (selectedI)
                {
                    #region
                    case "50mA":
                        testData.Lin50mA = value.ToString();
                        break;

                    case "100mA":
                        testData.Lin100mA = value.ToString();
                        break;
                    case "150mA":
                        testData.Lin150mA = value.ToString();
                        break;

                    case "200mA":
                        testData.Lin200mA = value.ToString();
                        break;
                    case "300mA":
                        testData.Lin300mA = value.ToString();
                        break;
                    case "400mA":
                        testData.Lin400mA = value.ToString();
                        break;
                    case "500mA":
                        testData.Lin500mA = value.ToString();
                        break;
                    case "600mA":
                        testData.Lin600mA = value.ToString();
                        break;
                    case "1000mA":
                        testData.Lin1000mA = value.ToString();
                        break;

                    default:
                        break;
                    #endregion
                }
            }
            else if (testItem == 2)//Vout
            {
                switch (selectedI)
                {
                    #region
                    case "50mA":
                        testData.Vout50mA = value.ToString();
                        break;

                    case "100mA":
                        testData.Vout100mA = value.ToString();
                        break;
                    case "150mA":
                        testData.Vout150mA = value.ToString();
                        break;

                    case "200mA":
                        testData.Vout200mA = value.ToString();
                        break;
                    case "300mA":
                        testData.Vout300mA = value.ToString();
                        break;
                    case "400mA":
                        testData.Vout400mA = value.ToString();
                        break;
                    case "500mA":
                        testData.Vout500mA = value.ToString();
                        break;
                    case "600mA":
                        testData.Vout600mA = value.ToString();
                        break;
                    case "1000mA":
                        testData.Vout1000mA = value.ToString();
                        break;

                    default:
                        break;
                    #endregion
                }
            }
            else if (testItem == 3)//eff
            {
                switch (selectedI)
                {
                    #region
                    case "50mA":
                        testData.Eff50mA = value.ToString();
                        break;

                    case "100mA":
                        testData.Eff100mA = value.ToString();
                        break;
                    case "150mA":
                        testData.Eff150mA = value.ToString();
                        break;

                    case "200mA":
                        testData.Eff200mA = value.ToString();
                        break;
                    case "300mA":
                        testData.Eff300mA = value.ToString();
                        break;
                    case "400mA":
                        testData.Eff400mA = value.ToString();
                        break;
                    case "500mA":
                        testData.Eff500mA = value.ToString();
                        break;
                    case "600mA":
                        testData.Eff600mA = value.ToString();
                        break;
                    case "1000mA":
                        testData.Eff1000mA = value.ToString();
                        break;

                    default:
                        break;
                    #endregion
                }
            }

            else if (testItem == 4)//Freq
            {
                switch (selectedI)
                {
                    #region
                    case "50mA":
                        testData.Fre50mA = value.ToString();
                        break;

                    case "100mA":
                        testData.Fre100mA = value.ToString();
                        break;
                    case "150mA":
                        testData.Fre150mA = value.ToString();
                        break;

                    case "200mA":
                        testData.Fre200mA = value.ToString();
                        break;
                    case "300mA":
                        testData.Fre300mA = value.ToString();
                        break;
                    case "400mA":
                        testData.Fre400mA = value.ToString();
                        break;
                    case "500mA":
                        testData.Fre500mA = value.ToString();
                        break;
                    case "600mA":
                        testData.Fre600mA = value.ToString();

                        break;
                    case "1000mA":
                        testData.Fre1000mA = value.ToString();
                        break;
                    default:
                        break;
                    #endregion
                }
            }
        }

        void ComDevice_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] ReDatas = new byte[ComDevice.BytesToRead];
            int ReLen = ComDevice.Read(ReDatas, 0, ReDatas.Length);
            //MessageBox.Show(ReLen.ToString());
            //flag = true;

            #region
            if (ReLen > 2)
            {
                this.AddData(ReDatas);//将新接收到的数据加到后面
                //string strReDatas = System.Text.Encoding.Default.GetString(ReDatas);
                StringBuilder sb = new StringBuilder();
                foreach (var itemData in ReDatas)
                {
                    sb.AppendFormat("{0:x2}" + "", itemData);
                }
                string strReDatas = sb.ToString().ToUpper();
                string a = LoadAddr.ToString();
                if (strReDatas.Contains("0A01"))
                {
                    AddContent("负载仪设置成功");
                    flag = true;
                }
                else if (strReDatas.Contains("100A00"))
                {
                    flagCCMode = true;
                    AddContent("负载仪CC模式设置成功");
                }
                else if (strReDatas.Contains(a + "03"))
                {
                    isReadVout = true;
                    AddContent("负载仪读取输出电压成功");
                    if (strReDatas.Contains("FF"))
                    {
                        strReDatas=strReDatas.Replace("FF", "");
                    }
                    if (strReDatas.Contains("FE"))
                    {
                        strReDatas=strReDatas.Replace("FE", "");
                    }
                    ReDatas = strToHexByte(strReDatas);//StringToByte(strReDatas);
                    //ReDatas = System.Text.Encoding.Default.GetBytes(strReDatas);
                    int valueLen = Convert.ToInt32(ReDatas[2]);
                    byte[] bytesValue = new byte[valueLen];
                    for (int i = 0; i < valueLen; i++)
                    {
                        bytesValue[i] = ReDatas[i + 3];
                    }
                    Array.Reverse(bytesValue);

                    float floatReadVout = BitConverter.ToSingle(bytesValue, 0);
                    strReadVout = floatReadVout.ToString();
                }
            }
            #endregion
        }

        void MultiCom_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(100);
            byte[] ReDatas = new byte[MultiCom.BytesToRead];
            int ReLen = MultiCom.Read(ReDatas, 0, ReDatas.Length);

            strReadIin = new ASCIIEncoding().GetString(ReDatas);
            //string strReadIin =Encoding.Default.GetString(ReDatas);
            if (strReadIin.Contains("+"))
            {
                isReadIin = true;
                //isReadF = true;
            }

        }
        void Multi2Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(100);
            byte[] ReDatas = new byte[Multi2Com.BytesToRead];
            int ReLen = Multi2Com.Read(ReDatas, 0, ReDatas.Length);

#if MULTIMETER 
            strReadVout=new ASCIIEncoding().GetString(ReDatas);
            if (strReadIin.Contains("+"))
            {
                isReadVout = true;
            }
#else
            string strRead = new ASCIIEncoding().GetString(ReDatas);
            if (strRead.Contains("OK"))
            {
                if (strRead.Contains("+V"))//电压
                {
                    strReadVin = strRead.Replace("+V=", "");
                    strReadVin = strReadVin.Replace("\r\nOK\r\n", "");
                    isReadVin = true;
                }
                else if (strRead.Contains("+C"))//电流
                {
                    strReadC = strRead.Replace("+C=", "");
                    strReadC = strReadC.Replace("\r\nOK\r\n", "");
                    isReadC = true;
                }
            }

#endif

        }


        private void AddData(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var itemData in data)
            {
                sb.AppendFormat("{0:x2}" + "", itemData);
            }
            try
            {
                //AddContent("负载仪返回:" + sb.ToString().ToUpper());
            }
            catch (Exception)
            {
                return;
            }
        }
        private void AddContent(string content)
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    if (txtShowData.Text.Length > 0)//至少有一个字节存在,才换行
                    {
                        txtShowData.AppendText("\r\n");
                    }
                    txtShowData.AppendText(System.DateTime.Now.ToString("(yyyy-MM-dd hh:mm:ss)   ") + content);
                }));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void PutResult(bool result, string log)
        {
            string logStr;

            updateUI = delegate
            {
                if (result)
                {
                    iPass++;

                    logStr = "\r\n";
                    logStr += "########     ###     ######   ######\r\n";
                    logStr += "##     ##   ## ##   ##    ## ##    ##\r\n";
                    logStr += "##     ##  ##   ##  ##       ##\r\n";
                    logStr += "########  ##     ##  ######   ######\r\n";
                    logStr += "##        #########       ##       ##\r\n";
                    logStr += "##        ##     ## ##    ## ##    ##\r\n";
                    logStr += "##        ##     ##  ######   ######\r\n";
                    txtShowData.AppendText("\r\n" + log);
                    txtShowData.AppendText("\r\n====================================\r\n");
                    txtShowData.AppendText("====================================\r\n");
                    txtShowData.AppendText(logStr);

                    //txtShowData.AppendText("LOG\r\n");
                    txtShowData.BackColor = Color.Green;

                    //结果显示： 
                    textPass.Text = iPass.ToString();
                    Win32API.WritePrivateProfileString("Result", "TestPass", iPass.ToString(), strCurrentDirectory);
                    textTotal.Text = (iPass + iFail).ToString();
                    textRate.Text = Math.Round((double)iPass * 100 / (iPass + iFail), 2).ToString() + "%";

                    WritePassResult(strSn, txtShowData.Text, strLogPath, true);

                }
                else
                {
                    iFail++;
                    logStr = "\r\n";
                    logStr += "########    ###     ####  ##\r\n";
                    logStr += "##         ## ##     ##   ##\r\n";
                    logStr += "##        ##   ##    ##   ##\r\n";
                    logStr += "######   ##     ##   ##   ##\r\n";
                    logStr += "##       #########   ##   ##\r\n";
                    logStr += "##       ##     ##   ##   ##\r\n";
                    logStr += "##       ##     ##  ####  ########\r\n";

                    txtShowData.AppendText("\r\n" + log);
                    txtShowData.AppendText("\r\n====================================\r\n");
                    txtShowData.AppendText("====================================\r\n");
                    txtShowData.AppendText(logStr);
                    txtShowData.BackColor = Color.Red;

                    //结果显示： 
                    textFail.Text = iFail.ToString();
                    Win32API.WritePrivateProfileString("Result", "TestFail", iFail.ToString(), strCurrentDirectory);
                    textTotal.Text = (iPass + iFail).ToString();
                    textRate.Text = Math.Round((double)iPass * 100 / (iPass + iFail), 2).ToString() + "%";

                    WritePassResult(strSn, txtShowData.Text, strLogPath, false);
                }
            };
            this.Invoke(updateUI);
        }
        public void JustUpdateResult(bool result, string log)
        {
            string logStr;

            updateUI = delegate
            {
                if (result)
                {
                    iPass++;
                    logStr = "\r\n";
                    logStr += "########     ###     ######   ######\r\n";
                    logStr += "##     ##   ## ##   ##    ## ##    ##\r\n";
                    logStr += "##     ##  ##   ##  ##       ##\r\n";
                    logStr += "########  ##     ##  ######   ######\r\n";
                    logStr += "##        #########       ##       ##\r\n";
                    logStr += "##        ##     ## ##    ## ##    ##\r\n";
                    logStr += "##        ##     ##  ######   ######\r\n";
                    txtShowData.Text = logStr;
                    txtShowData.AppendText("====================================\r\n");
                    txtShowData.AppendText("====================================\r\n");
                    txtShowData.AppendText("LOG\r\n");
                    txtShowData.AppendText(log);
                    txtShowData.BackColor = Color.Green;

                    //结果显示： 
                    //textBox_pass.Text = iPass.ToString();
                    //Win32API.WritePrivateProfileString("SNUnBindResult", "Pass", iPass.ToString(), str_CurrentDirectory);
                    //textBox_total.Text = (iPass + iFail).ToString();

                    //WritePassResult(SN_text.Text, textBox_log.Text, strLogPath, true);
                }
                else
                {
                    iFail++;
                    logStr = "\r\n";
                    logStr += "########    ###     ####  ##\r\n";
                    logStr += "##         ## ##     ##   ##\r\n";
                    logStr += "##        ##   ##    ##   ##\r\n";
                    logStr += "######   ##     ##   ##   ##\r\n";
                    logStr += "##       #########   ##   ##\r\n";
                    logStr += "##       ##     ##   ##   ##\r\n";
                    logStr += "##       ##     ##  ####  ########\r\n";

                    txtShowData.Text = logStr;
                    txtShowData.AppendText("====================================\r\n");
                    txtShowData.AppendText("====================================\r\n");
                    txtShowData.AppendText("LOG\r\n");
                    txtShowData.AppendText(log);
                    txtShowData.BackColor = Color.Crimson;

                    //结果显示： 
                    //textBox_fail.Text = iFail.ToString();
                    //Win32API.WritePrivateProfileString("SNUnBindResult", "Fail", iFail.ToString(), str_CurrentDirectory);
                    //textBox_total.Text = (iPass + iFail).ToString();
                    ////textBox_networkmac.Text = "FFFFFFFFFFFF";

                    //WritePassResult(SN_text.Text, textBox_log.Text, strLogPath, false);
                }
            };
            this.Invoke(updateUI);
        }
        public void WritePassResult(string Sn, string content, string path, bool IsPassOrFail)
        {
            string strTempName = null;
            string strTimeNow = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            if (IsPassOrFail)
            {
                strTempName = path + Sn.ToUpper() + "_" + strTimeNow + "_PASS.LOG";//文件夹的路径+文件
            }
            else
            {
                if (Sn == null)
                {
                    Sn = "FFFFFFFFF";
                    //updateUI = delegate {
                    //SN_text.Text = Sn;
                    //};
                    //SN_text.Invoke(updateUI);
                }
                strTempName = path + Sn.ToUpper() + "_" + strTimeNow + "_FAIL.LOG";
            }

            FileStream fs = new FileStream(strTempName, FileMode.OpenOrCreate, FileAccess.Write);
            byte[] byteWrite = Encoding.Default.GetBytes(content);
            fs.Write(byteWrite, 0, byteWrite.Length);
            fs.Close();
        }


        private byte[] FloatToHex(float f)
        {
            byte[] b = BitConverter.GetBytes(f);
            Array.Reverse(b);
            return b;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            textTestDate.Text = System.DateTime.Now.ToString();//"yy-MM-dd"
            if (Setting.isLocked)
            {
                textProName.Text = Setting.selectProduct;
            }
            if (Setting.isLocked && updateSelectedI)
            {
                updateSelectedI = false;//按一次锁定按钮只更新一次gruop
                SelectedI = Setting.selectI;
                SelectedIArry = SelectedI.Split(';');
                CreatLabelIInGroup();
                //同理,按一次锁定按钮只更新一次param参数范围-读txt
                #region
                //更新电流项数
                Icount = SelectedIArry.Length - 1;
                try
                {
                    SelectParam = ReadTxt(strParamDirectory);
                    //textShowSelectParam.Text = SelectParam;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                string[] strArrySP = SelectParam.Split('\n');

                string[][] strArray = new string[Icount][];
                for (int i = 0; i < Icount; i++)
                {
                    strArray[i] = strArrySP[i].Split(';');
                }
                floatArray = new float[Icount, 8];
                for (int i = 0; i < Icount; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (string.IsNullOrEmpty(strArray[i][j]))
                        {
                            XtraMessageBox.Show("您所选测试项，有参数规定范围未设置");
                            System.Environment.Exit(1);
                        }
                        floatArray[i, j] = Convert.ToSingle(strArray[i][j]);
                    }
                }
                #endregion
                //XtraMessageBox.Show((floatArray.Length / 8).ToString());

                SelectParam = Setting.selectParam;

                //textShowSelectParam.Text = SelectParam;

                //更新测试项目(如:Iin、Vout)
                testIin = Setting.testIin;
                testVout = Setting.testVout;
                testEff = Setting.testEff;
                testFreq = Setting.testFreq;
            }
        }

        private void CreatLabelIInGroup()
        {
            groupTestI.Controls.Clear();
            groupTestRet.Controls.Clear();

            //foreach (var item in collection)
            //{

            //}
            string[] tempSelectedIArry = new string[SelectedIArry.Length - 1];
            for (int i = 0; i < SelectedIArry.Length - 1; i++)
            {
                tempSelectedIArry[i] = SelectedIArry[i];
            }
            Array.Reverse(tempSelectedIArry);

            //for (int i = SelectedIArry.Length - 2; i >= 0; i--)//用的是SelectedIArry，不翻转
            //{
            for (int i = tempSelectedIArry.Length - 1; i >= 0; i--)
            {

                LabelX lable = new LabelX();
                //LabelControl lable = new LabelControl();
                lable.Name = "lableI" + i.ToString();
                lable.Text = tempSelectedIArry[i];
                //lable.Dock = DockStyle.Top;
                //lable.Size = new Size(196, 30);
                lable.Location = new Point(30, 40 * (i + 1));

                lable.AutoSize = true;
                groupTestI.Controls.Add(lable);

                //创建结果显示
                PictureBox pictureBox = new PictureBox();
                pictureBox.Name = "picture" + i.ToString();

                pictureBox.Image = Properties.Resources.白点中;
                //pictureBox.Dock = DockStyle.Top;
                pictureBox.Location = new Point(25, 40 * (i + 1));
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                //groupTestRet.Controls.Add(pictureBox);
                groupTestRet.Controls.Add(pictureBox);

            }

        }
        /// <summary>
        /// 设置图片为白点
        /// </summary>
        /// <param name="groupControl"></param>
        public void SetPictureColorFromControls(Control groupControl)
        {
            ////获取Group中每个控件

            var a = groupControl.Controls;
            //将a集合中的每一个元素（每个控件）依次赋给b
            foreach (var b in a)
            {
                //判断控件类型为RadioButton
                if (b.GetType() == typeof(PictureBox))
                {
                    //强制转换成RadioButton类型
                    var c = (PictureBox)b;
                    c.Image = Properties.Resources.白点中;
                    c.BackColor = Color.WhiteSmoke;
                }

            }
        }

        public PictureBox GetPictureControlFromGroup(string name, Control groupControl)
        {
            ////获取Group中每个控件
            PictureBox c = null;
            var a = groupControl.Controls;
            //将a集合中的每一个元素（每个控件）依次赋给b
            foreach (var b in a)
            {
                //判断控件类型为RadioButton
                if (b.GetType() == typeof(PictureBox))
                {
                    //强制转换成RadioButton类型
                    c = (PictureBox)b;
                    if (c.Name == name)
                    {
                        return c;
                    }
                }

            }
            return c;
        }
        /// <summary>
        /// //从Group中获取最终结果
        /// </summary>
        /// <param name="groupControl"></param>
        /// <returns></returns>
        public int GetRetFromGroup(Control groupControl)
        {
            ////获取Group中每个控件
            var a = groupControl.Controls;
            //将a集合中的每一个元素（每个控件）依次赋给b

            for (int i = a.Count - 1; i >= 0; i--)
            {
                if (a[i].GetType() == typeof(PictureBox))
                {
                    //强制转换成RadioButton类型
                    var c = (PictureBox)a[i];
                    if (c.BackColor == Color.Gainsboro)
                    {
                        return 0;
                    }
                    if (c.BackColor == Color.WhiteSmoke)
                    {
                        return -1;
                    }
                }
            }
            //foreach (var b in a)
            //{
            //    //判断控件类型为RadioButton
            //    if (b.GetType() == typeof(PictureBox))
            //    {
            //        //强制转换成RadioButton类型
            //        var c = (PictureBox)b;
            //        if (c.BackColor == Color.Gainsboro)
            //        {
            //            return 0;
            //        }
            //        if (c.BackColor == Color.WhiteSmoke)
            //        {
            //            return -1;
            //        }
            //        //if (c.BackColor == Color.Transparent)
            //        //{
            //        //    return 1;
            //        //}
            //    }

            //}
            return 1;
        }
        public string ReadTxt(string path)
        {
            string content = "";
            string line;
            try
            {
                StreamReader sr = new StreamReader(path, Encoding.Default);
                while ((line = sr.ReadLine()) != null)
                {
                    content += line + "\n";
                }
                sr.Dispose();
                sr.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //while ((line = sr.ReadLine()) != null)
            return content;
        }

        private void textSn_TextChanged(object sender, EventArgs e)
        {
            if (textSn.Text.Length > snLength - 1)
            {
                Win32API.GetPrivateProfileString("Set", "UsedProductName", "", tempStringBuilder, 256, strCurrentDirectory);
                productNameLength = tempStringBuilder.ToString().Length;
                string productName = textSn.Text.Substring(0, productNameLength);//13
                if (textProName.Text.Trim() != productName)
                {
                    JustUpdateResult(false, "SN的型号:" + productName + " 与 设定型号:" + textProName.Text + "不符,请检查");
                    textSn.Focus();
                    textSn.Text = "";
                    return;
                }
                //SnCheck = true;
                if (startMode == (int)MyStartMode.ConServer)
                {
                    //开始查询
                    #region
                    string msg;
                    StringBuilder tempJson = new StringBuilder();
                    ResponseSimpleInfo responseSimpleInfo;

                    try
                    {
                        tempJson.Append(urlInquire);
                        tempJson.Append(textSn.Text);

                        int result = HttpServer.GetSimpleInfo(tempJson.ToString(), out responseSimpleInfo, out msg);
                        if (result != 0)
                        {
                            if (responseSimpleInfo.code == 2002)
                            {
                                JustUpdateResult(false, textSn.Text + " 重复性能测试");
                                textSn.Focus();
                                textSn.Text = "";
                            }
                            else
                            {
                                JustUpdateResult(false, msg);
                                textSn.Focus();
                                textSn.Text = "";
                            }
                        }
                        else
                        {
                            if (responseSimpleInfo.code != 1000)
                            {
                                JustUpdateResult(false, msg);
                                textSn.Focus();
                                textSn.Text = "";
                            }
                            else
                            {
                                //JustUpdateResult(true, msg);
                                AddContent(msg + " SN：" + textSn.Text + " 查询 PASS\r\n");
                                /*****************************************/
                                /*****************************************/
                                /*SN查询PASS后，存Sn值，清空，使不能(测试中不能再扫码)*/
                                strSn = textSn.Text;
                                textSn.Focus();
                                textSn.Text = "";
                                textSn.Enabled = false;
                                //关闭自动测试模式
                                timer2.Enabled = false;
                                //手动测试按键事件
                                ManualEvent();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        JustUpdateResult(false, ex.Message);
                        textSn.Focus();
                        textSn.SelectAll();

                        //MessageBox.Show(ex.Message);
                        //SN_text.Focus();
                        //SN_text.SelectAll();
                    }
                    #endregion
                }
                else if (startMode == (int)MyStartMode.DisConServer)
                {
                    strSn = textSn.Text;
                    textSn.Focus();
                    textSn.Text = "";
                    textSn.Enabled = false;
                    //关闭自动测试模式
                    timer2.Enabled = false;
                    //手动测试按键事件
                    ManualEvent();
                }

            }
        }

        private void txtShowData_TextChanged(object sender, EventArgs e)
        {
            if (txtShowData.Text.Length > 65536)
            {
                txtShowData.Text = "";
            }
        }

        private void btnAutoTest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textDelayTime.Text))
            {
                XtraMessageBox.Show("请输入延迟时间");
                textDelayTime.Focus();
                textDelayTime.SelectAll();
                return;
            }
            //数量的正整数规范
            ClassMyRegex regex = new ClassMyRegex();
            if (regex.RegexIsPosIntMatch(textDelayTime.Text) == false)
            {
                XtraMessageBox.Show("请输入正整数!");
                textDelayTime.Focus();
                textDelayTime.SelectAll();
                return;
            }
            //写入自动测试时间
            Win32API.WritePrivateProfileString("Set", "AutoTestTime", textDelayTime.Text, strCurrentDirectory);

            timer2.Interval = Convert.ToInt16(textDelayTime.Text.ToString()) * 1000;
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ManualEvent();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            iPass = 0;
            iFail = 0;
            textFail.Text = "0";
            textPass.Text = "0";
            textTotal.Text = "0";
            textRate.Text = "0.00";
            Win32API.WritePrivateProfileString("Result", "TestFail", "0", strCurrentDirectory);
            Win32API.WritePrivateProfileString("Result", "TestPass", "0", strCurrentDirectory);
        }

        private void btnStopTest_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txtShowData.BackColor = Color.White;
        }



        // 把字节型转换成十六进制字符串

        public static string ByteToString(byte[] InBytes)
        {

            string StringOut = "";

            foreach (byte InByte in InBytes)
            {

                StringOut = StringOut + String.Format("{0:X2} ", InByte);

            }

            return StringOut;

        }

        public string ByteToString(byte[] InBytes, int len)
        {

            string StringOut = "";

            for (int i = 0; i < len; i++)
            {

                StringOut = StringOut + String.Format("{0:X2} ", InBytes[i]);

            }

            return StringOut;

        }

        // 把十六进制字符串转换成字节型

        public static byte[] StringToByte(string InString)
        {

            string[] ByteStrings;

            ByteStrings = InString.Split(" ".ToCharArray());

            byte[] ByteOut;

            ByteOut = new byte[ByteStrings.Length - 1];

            for (int i = 0; i == ByteStrings.Length - 1; i++)
            {

                ByteOut[i] = Convert.ToByte(("0x" + ByteStrings[i]));

            }

            return ByteOut;

        }

        private byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
            {
                hexString += " ";
            }
            byte[] returnBytes = new byte[hexString.Length / 2];

            for (int i = 0; i < hexString.Length / 2; i++)
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Replace(" ", ""), 16);
            }
            return returnBytes;
        }


    }
}