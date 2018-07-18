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

using JiaHao.RegexHelp;
using Windows;
using System.IO;
using System.Collections;
using IMEISNPrint;
namespace wirelessCharging
{
    public partial class InBox : DevExpress.XtraEditors.XtraForm
    {
        //private static int cntEqual = 0;
        //private static int cntGreater = 0;


        StringBuilder tempStringBuilder = new StringBuilder();      //全局可变字符串实例，用于读取配置文件内容
        private string strCurrentDirectory;                         //应用程序路径
        //Length
        private int snLength;//Sn
        private int boxLength;//Box
        
        //Infomation
        private string strLogPath;                      //log路径
        //Result
        private int iPass;
        private int iFail;
        //url
        private const string urlInquire = "/wirelesscharging/package/count/";
        private const string urlUpload = "/wirelesscharging/package";


        //MaxCount
        private int MaxCount = 240;
        //CurrentSnCount
        private int CurrentSnCount;
        
        public InBox()
        {
            InitializeComponent();
            InitGlobleVariable();
        }

        public void InitGlobleVariable()
        {
            //!!!!!!!!!!!!!!上线后该地址要更改为：获取当前系统地址，再拼剪为formain的地址
            strCurrentDirectory = System.Environment.CurrentDirectory + "\\SetUp.ini";

            strLogPath = System.Environment.CurrentDirectory + "\\InBoxLog\\";
            if (!Directory.Exists(strLogPath))
            {
                Directory.CreateDirectory(strLogPath);
            }
            //Length  sn
            Win32API.GetPrivateProfileString("Sn", "length", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Sn", "length", "30", strCurrentDirectory);
            }
            else
            {
                snLength = Convert.ToInt16(tempStringBuilder.ToString());
            }
                       //Box
            Win32API.GetPrivateProfileString("Box", "length", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Box", "length", "23", strCurrentDirectory);
            }
            else
            {
                boxLength = Convert.ToInt16(tempStringBuilder.ToString());
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

            //Result
            #region
            Win32API.GetPrivateProfileString("Result", "InBoxPass", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Result", "InBoxPass", "6", strCurrentDirectory);
            }
            else
            {
                textPass.Text = tempStringBuilder.ToString();
                iPass = int.Parse(tempStringBuilder.ToString());
            }

            Win32API.GetPrivateProfileString("Result", "InBoxFail", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Result", "InBoxFail", "6", strCurrentDirectory);

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
            #region 装箱数量
            //CurrentSnCountInBox
            //Win32API.GetPrivateProfileString("Set", "CurrentSnCountInBox", "", tempStringBuilder, 256, strCurrentDirectory);
            //if (tempStringBuilder.ToString() == "")
            //{
            //    Win32API.WritePrivateProfileString("Set", "CurrentSnCountInBox", "0", strCurrentDirectory);
            //}
            //else
            //{
            //    try
            //    {
            //        labHasInBox.Text = tempStringBuilder.ToString();
            //        CurrentSnCount = Convert.ToInt32(labHasInBox.Text);
            //    }
            //    catch (Exception ex)
            //    {
            //        XtraMessageBox.Show("箱中当前产品数量 " + ex.Message);
            //    }
            //}
            //MaxSnCoutInBox
            Win32API.GetPrivateProfileString("Set", "MaxSnCoutInBox", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Set", "MaxSnCoutInBox", "240", strCurrentDirectory);
            }
            else
            {
                try
                {
                   labMaxCount.Text = tempStringBuilder.ToString();
                   MaxCount = Convert.ToInt32(labMaxCount.Text);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("箱中产品的最大数量 " + ex.Message);
                }
            }
            //smallBoxCount
            //Win32API.GetPrivateProfileString("Set", "smallBoxCount", "", tempStringBuilder, 256, strCurrentDirectory);
            //if (tempStringBuilder.ToString() == "")
            //{
            //    Win32API.WritePrivateProfileString("Set", "smallBoxCount", "6", strCurrentDirectory);
            //}
            //else
            //{
            //    textSmalBoxCount.Text = tempStringBuilder.ToString();
            //}
            #endregion
        }
        private void btnChangeSnCount_Click(object sender, EventArgs e)
        {
            //#region
            //string smallBoxCount = textSmalBoxCount.Text.Trim();
            //if (btnChangeSnCount.Text == "更改")
            //{
            //    btnChangeSnCount.Text = "确定";
            //    btnChangeSnCount.ForeColor = Color.Red;
            //    textSmalBoxCount.ReadOnly = false;
            //    textSmalBoxCount.Focus();
            //    textSmalBoxCount.SelectAll();
            //    return;
            //}
            //if (btnChangeSnCount.Text == "确定")
            //{
            //    if (string.IsNullOrEmpty(smallBoxCount))
            //    {
            //        XtraMessageBox.Show("请输入一箱装 几盒！");
            //        textSmalBoxCount.Focus();
            //        textSmalBoxCount.SelectAll();
            //        return;
            //    }
            //    //数量的正整数规范
            //    ClassMyRegex regex = new ClassMyRegex();
            //    if (regex.RegexIsPosIntMatch(smallBoxCount) == false)
            //    {
            //        XtraMessageBox.Show("请输入正整数！");
            //        textSmalBoxCount.Focus();
            //        textSmalBoxCount.SelectAll();
            //        return;
            //    }


            //    btnChangeSnCount.Text = "更改";
            //    btnChangeSnCount.ForeColor = Color.Black;
            //    Win32API.WritePrivateProfileString("Set", "smallBoxCount", smallBoxCount, strCurrentDirectory);
            //    XtraMessageBox.Show("修改成功");
            //    textSmalBoxCount.ReadOnly = true;
            //}
            //#endregion
        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {
            #region
            if (textBoxID.Text.Length > boxLength-1)//==23
            {


                //textBoxID.ReadOnly = true;

                //textSn1.ReadOnly = false;
                //textSn1.Focus();
                //textSn1.Text = "";

                //textLog.Text = "";
                //textLog.BackColor = Color.White;


                //开始查询
                #region
                string msg;
                StringBuilder tempJson = new StringBuilder();
                ResponseSimpleInfo responseSimpleInfo;

                try
                {
                    tempJson.Append(urlInquire);
                    tempJson.Append(textBoxID.Text);

                    int result = HttpServer.GetSimpleInfo(tempJson.ToString(), out responseSimpleInfo, out msg);
                    if (result != 0)
                    {
                        PutResult(false, msg);
                        textLog.BackColor = Color.OrangeRed;
                        textBoxID.ReadOnly = false;
                        textSn1.ReadOnly = true;
                        textBoxID.Text = "";
                        textBoxID.Focus();
                    }
                    else
                    {
                        if (responseSimpleInfo.code != 1000)
                        {
                            PutResult(false, msg);
                            textLog.BackColor = Color.OrangeRed;
                            textBoxID.ReadOnly = false;
                            textSn1.ReadOnly = true;
                            textBoxID.Text = "";
                            textBoxID.Focus();
                        }
                        else
                        {
                            //更新当前数量label的显示,
                            CurrentSnCount = responseSimpleInfo.data.count;
                            labHasInBox.Text = CurrentSnCount.ToString();

                            textBoxID.ReadOnly = true;
                            textSn1.ReadOnly = false;
                            textSn1.Focus();
                            textSn1.Text = "";

                            textLog.Text = "";
                            textLog.BackColor = Color.White;
                        }
                    }
                }
                catch (Exception ex)
                {
                    PutResult(false, ex.Message);
                    textLog.BackColor = Color.OrangeRed;

                    textBoxID.ReadOnly = false;
                    textSn1.ReadOnly = true;
                    textBoxID.Text = "";
                    textBoxID.Focus();
                }
                #endregion
            }
            #endregion

            //#region
            //if (textBoxID.Text.Length > 22)//==23
            //{

            //    textBoxID.ReadOnly = true;
            //    textSn1.ReadOnly = false;
            //    textSn1.Focus();
            //    textSn1.SelectAll();

            //    textLog.Text = "";
            //    textLog.BackColor = Color.White;
            //}
            //#endregion
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            iPass = 0;
            iFail = 0;
            textFail.Text = "0";
            textPass.Text = "0";
            textTotal.Text = "0";
            textRate.Text = "0.00";
            Win32API.WritePrivateProfileString("Result", "InBoxFail", "0", strCurrentDirectory);
            Win32API.WritePrivateProfileString("Result", "InBoxPass", "0", strCurrentDirectory);
        }
        public void PutResult(bool result, string log)
        {
            string logStr;

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
                textLog.Text = logStr;
                textLog.Text += "====================================\r\n";
                textLog.Text += "====================================\r\n";
                textLog.Text += "LOG\r\n";
                textLog.Text += log;
                textLog.BackColor = Color.Green;

                //结果显示： 
                textPass.Text = iPass.ToString();
                Win32API.WritePrivateProfileString("Result", "InBoxPass", iPass.ToString(), strCurrentDirectory);
                textTotal.Text = (iPass + iFail).ToString();
                textRate.Text = Math.Round((double)iPass * 100 / (iPass + iFail), 2).ToString() + "%";

                WritePassResult(textBoxID.Text + "_" + textSn1.Text, textLog.Text, strLogPath, true);
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

                textLog.Text = logStr;
                textLog.Text += "====================================\r\n";
                textLog.Text += "====================================\r\n";
                textLog.Text += "LOG\r\n";
                textLog.Text += log;
                textLog.BackColor = Color.Red;

                //结果显示： 
                textFail.Text = iFail.ToString();
                Win32API.WritePrivateProfileString("Result", "InBoxFail", iFail.ToString(), strCurrentDirectory);
                textTotal.Text = (iPass + iFail).ToString();
                textRate.Text = Math.Round((double)iPass * 100 / (iPass + iFail), 2).ToString() + "%";

                WritePassResult(textBoxID.Text + "_" + textSn1.Text, textLog.Text, strLogPath, false);
            }
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
                    Sn = "FFFFFFFFF...";
                    //updateUI = delegate {
                    textBoxID.Text = Sn;
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

        private void textSn1_TextChanged(object sender, EventArgs e)
        {
            #region 多个sn一起查询
            //Win32API.GetPrivateProfileString("Set", "smallBoxCount", "", tempStringBuilder, 256, strCurrentDirectory);
            //int smallBoxCount = Convert.ToInt32(tempStringBuilder.ToString());
            //for (int i = 1; i <= smallBoxCount; i++)
            //{
            //    if (textSn1.Text.Length == 31 * i - 1)
            //    {
            //        textSn1.Text += "|";
            //        //让文本框获取焦点 
            //        this.textSn1.Focus();
            //        //设置光标的位置到文本尾 
            //        this.textSn1.Select(this.textSn1.TextLength, 0);
            //        break;
            //    }
            //}

            ////if (textSn1.Text.Length == 30
            ////    || textSn1.Text.Length == 61
            ////    || textSn1.Text.Length == 92
            ////    || textSn1.Text.Length == 123
            ////    || textSn1.Text.Length == 154
            ////    || textSn1.Text.Length == 185
            ////    || textSn1.Text.Length == 216
            ////    || textSn1.Text.Length == 247
            ////   )
            ////{
            ////    textSn1.Text += "|";

            ////    //让文本框获取焦点 
            ////    this.textSn1.Focus();
            ////    //设置光标的位置到文本尾 
            ////    this.textSn1.Select(this.textSn1.TextLength, 0);

            ////}
            //if (textSn1.Text.Length == (31 * smallBoxCount))
            //{
            //    cntEqual++;
            //    if (cntEqual == 2)
            //    {
            //        cntEqual = 0;

            //        string allSn = textSn1.Text.Substring(0, textSn1.Text.LastIndexOf('|'));
            //        string[] snArry = allSn.Split('|');
            //        string strRepeat;
            //        if (IsRepeat(snArry, out strRepeat))//包含重复的
            //        {
            //            strRepeat = string.Format("扫描中具有相同的SN号：{0}", strRepeat);
            //            PutResult(false, strRepeat);
            //        }
            //        else
            //        {
            //            //一一查重(sn表——查sn看回复的pakage有没有),查漏(Log表——查sn和result为1的是否存在)
            //            foreach (var item in snArry)
            //            {
            //                textLog.Text = "开始查重查漏:";
            //            }

            //            //ClassMySqlHelp mySql = new ClassMySqlHelp();
            //            //string errLog = "";
            //            //Win32API.GetPrivateProfileString("Info", "Station", "", tempStringBuilder, 256, strCurrentDirectory);
            //            //station = tempStringBuilder.ToString();

            //            //int ret = mySql.InBoxSelectAndInsert(snArry, textBoxID.Text, station, out errLog);
            //            //if (ret != 0)
            //            //{
            //            //    PutResult(false, errLog);

            //            //}
            //            //else
            //            //{
            //            //    PutResult(true, errLog);
            //            //}
            //        }



            //    }

            //}
            //if (textSn1.Text.Length > (31 * smallBoxCount) - 1)
            //{
            //    cntGreater++;

            //    if (cntGreater == 2)
            //    {
            //        cntGreater = 0;
            //        textSn1.Text = "";
            //        textSn1.ReadOnly = true;

            //        textBoxID.ReadOnly = false;
            //        textBoxID.Focus();
            //        textBoxID.SelectAll();
            //    }

            //}
            #endregion
            if (textSn1.Text.Length > snLength - 1)
            {
                if (Convert.ToInt32(labHasInBox.Text) >= MaxCount)
                {
                    XtraMessageBox.Show("本箱 已装满,请换箱");
                    textSn1.Text = "";
                    return;
                }
                //开始绑定
                #region 扫一个SN绑定一个箱号
                #region 上报服务器
                string msg;
                StringBuilder tempJson = new StringBuilder();
                ResponseSimpleInfo rpSimpleInfo;

                try
                {
                    tempJson.Append("{\"sn\":\"");
                    tempJson.Append(textSn1.Text);
                    tempJson.Append("\",");
                    tempJson.Append("\"packageNumber\":\"");
                    tempJson.Append(textBoxID.Text);
                    tempJson.Append("\"}");

                    int ret = HttpServer.PostSimpleInfo(urlUpload, out rpSimpleInfo, out msg, tempJson.ToString());
                    if (ret != 0)
                    {
                        if (rpSimpleInfo.code == 2002)
                        {
                            PutResult(false, textSn1.Text+"已经装箱");
                            textSn1.Focus();
                            textSn1.Text = "";
                        }
                        else
                        {
                            PutResult(false, textSn1.Text+ " 性能"+msg);
                            textSn1.Focus();
                            textSn1.Text = "";
                        }
                        return;

                    }
                    if (rpSimpleInfo.code != 1000)
                    {
                        //MessageBox.Show(msg);
                        PutResult(false, msg);
                    }
                    else
                    {
                        PutResult(true, textSn1.Text+" 装箱成功");

                        //当前数量变量更新
                        CurrentSnCount = rpSimpleInfo.data.count;
                        //更新当前数量label的显示,
                        labHasInBox.Text = CurrentSnCount.ToString();

                        //如果当前数量已经240,提示换箱
                        if (CurrentSnCount >= MaxCount)
                        {
                            XtraMessageBox.Show("本箱 已装满" + MaxCount.ToString() + ",下只产品请换箱");
                        }

                    }
                    textSn1.Focus();
                    textSn1.Text = "";
                }
                catch (Exception ex)
                {
                    PutResult(false, ex.Message);
                    textSn1.Focus();
                    textSn1.Text = "";
                }

                #endregion
                #endregion

            }

        }
        public bool IsRepeat(string[] array, out string strRepeat)
        {
            strRepeat = "";
            Hashtable ht = new Hashtable();
            for (int i = 0; i < array.Length; i++)
            {
                if (ht.Contains(array[i]))
                {
                    strRepeat = array[i];
                    return true;
                }
                else
                {
                    ht.Add(array[i], array[i]);
                }
            }
            return false;
        }

        private void btnChangeBox_Click(object sender, EventArgs e)
        {
            textBoxID.ReadOnly = false;
            textBoxID.Focus();
            textBoxID.Text = "";

            textSn1.ReadOnly = true;
            textSn1.Text = "";

            //刷新已装箱 数量
            labHasInBox.Text = "";
            CurrentSnCount = 0;
            Win32API.WritePrivateProfileString("Set", "CurrentSnCountInBox", "0", strCurrentDirectory);
        }




    }
}