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

using System.Collections;

using JiaHao.RegexHelp;
using JiaHao.MySqlHelp;
namespace MeiFenSys
{
    public partial class InBox : DevExpress.XtraEditors.XtraForm
    {
        private static int cntEqual = 0;
        private static int cntGreater = 0;


        StringBuilder tempStringBuilder = new StringBuilder();      //全局可变字符串实例，用于读取配置文件内容
        private string strCurrentDirectory;                         //应用程序路径
        //Infomation
        private string strLogPath;                      //log路径
        //Result
        private int iPass;
        private int iFail;
        //station
        string station;

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
            //smallBoxCount
            Win32API.GetPrivateProfileString("Set", "smallBoxCount", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Set", "smallBoxCount", "6", strCurrentDirectory);
            }
            else
            {
                textSmalBoxCount.Text = tempStringBuilder.ToString();
            }

        }
        //textBoxID
        //private void textBoxID_TextChanged(object sender, EventArgs e)
        //{
        //    if (textBoxID.Text.Length > 22)//==23
        //    {
        //        textBoxID.ReadOnly = true;

        //        textSn1.ReadOnly = false;
        //        textSn1.Focus();
        //        textSn1.SelectAll();

        //        textLog.Text = "";
        //        textLog.BackColor = Color.White;
        //    }

        //}


        private void btnChangeSnCount_Click(object sender, EventArgs e)
        {
            string smallBoxCount = textSmalBoxCount.Text.Trim();
            if (btnChangeSnCount.Text == "更改")
            {
                btnChangeSnCount.Text = "确定";
                btnChangeSnCount.ForeColor = Color.Red;
                textSmalBoxCount.ReadOnly = false;
                textSmalBoxCount.Focus();
                textSmalBoxCount.SelectAll();
                return;
            }
            if (btnChangeSnCount.Text == "确定")
            {
                if (string.IsNullOrEmpty(smallBoxCount))
                {
                    XtraMessageBox.Show("请输入一箱装 几盒！");
                    textSmalBoxCount.Focus();
                    textSmalBoxCount.SelectAll();
                    return;
                }
                //数量的正整数规范
                ClassMyRegex regex = new ClassMyRegex();
                if (regex.RegexIsPosIntMatch(smallBoxCount) == false)
                {
                    XtraMessageBox.Show("请输入正整数！");
                    textSmalBoxCount.Focus();
                    textSmalBoxCount.SelectAll();
                    return;
                }


                btnChangeSnCount.Text = "更改";
                btnChangeSnCount.ForeColor = Color.Black;
                Win32API.WritePrivateProfileString("Set", "smallBoxCount", smallBoxCount, strCurrentDirectory);
                XtraMessageBox.Show("修改成功");
                textSmalBoxCount.ReadOnly = true;
            }


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
            //textUID.Focus();
            //textUID.SelectAll();
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

                WritePassResult(textBoxID.Text, textLog.Text, strLogPath, true);
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

                WritePassResult(textBoxID.Text, textLog.Text, strLogPath, false);
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
            Win32API.GetPrivateProfileString("Set", "smallBoxCount", "", tempStringBuilder, 256, strCurrentDirectory);
            int smallBoxCount = Convert.ToInt32(tempStringBuilder.ToString())*2;

            if (textSn1.Text.Length == 12
                || textSn1.Text.Length == 25
                || textSn1.Text.Length == 38
                || textSn1.Text.Length == 51
                || textSn1.Text.Length == 64
                || textSn1.Text.Length == 77
                || textSn1.Text.Length == 90
                || textSn1.Text.Length == 103
               )
            {
                textSn1.Text += "|";

                //让文本框获取焦点 
                this.textSn1.Focus();
                //设置光标的位置到文本尾 
                this.textSn1.Select(this.textSn1.TextLength, 0);

            }
            if (textSn1.Text.Length == (13 * smallBoxCount))
            {
                cntEqual++;
                if (cntEqual == 2)
                {
                    cntEqual = 0;

                    string allSn = textSn1.Text.Substring(0, textSn1.Text.LastIndexOf('|'));
                    string[] snArry = allSn.Split('|');
                    string strRepeat;
                    if (IsRepeat(snArry,out strRepeat))//包含重复的
                    {
                        strRepeat = string.Format("扫描中具有相同的SN号：{0}", strRepeat);
                        PutResult(false, strRepeat);
                    }
                    else
                    {
                        //一一查重(sn表——查sn看回复的pakage有没有),查漏(Log表——查sn和result为1的是否存在)
                        ClassMySqlHelp mySql = new ClassMySqlHelp();
                        string errLog = "";
                        Win32API.GetPrivateProfileString("Info", "Station", "", tempStringBuilder, 256, strCurrentDirectory);
                        station = tempStringBuilder.ToString();

                        int ret = mySql.InBoxSelectAndInsert(snArry, textBoxID.Text, station, out errLog);
                        if (ret != 0)
                        {
                            PutResult(false, errLog);

                        }
                        else
                        {
                            PutResult(true, errLog);
                        }
                    }



                }

            }
            if (textSn1.Text.Length > (13 * smallBoxCount) - 1)
            {
                cntGreater++;

                if (cntGreater == 2)
                {
                    cntGreater = 0;
                    textSn1.Text = "";
                    textSn1.ReadOnly = true;

                    textBoxID.ReadOnly = false;
                    textBoxID.Focus();
                    textBoxID.SelectAll();
                }

            }
        }

        /// <summary>
        /// Hashtable 方法
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public bool IsRepeat(string[] array,out string strRepeat)
        {
            strRepeat="";
            Hashtable ht = new Hashtable();
            for (int i = 0; i < array.Length; i++)
            {
                if (ht.Contains(array[i]))
                {
                    strRepeat=array[i];
                    return true;
                }
                else
                {
                    ht.Add(array[i], array[i]);
                }
            }
            return false;
        }

        private void textBoxID_TextChanged_1(object sender, EventArgs e)
        {
            if (textBoxID.Text.Length > 22)//==23
            {
                textBoxID.ReadOnly = true;

                textSn1.ReadOnly = false;
                textSn1.Focus();
                textSn1.SelectAll();

                textLog.Text = "";
                textLog.BackColor = Color.White;
            }
        }




    }
}