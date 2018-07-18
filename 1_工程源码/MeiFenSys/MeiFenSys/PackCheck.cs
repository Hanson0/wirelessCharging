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
using JiaHao.MySqlHelp;
//using JiaHao.ExcelHelp;
//using JiaHao.RegexHelp;

namespace MeiFenSys
{
    public partial class PackCheck : DevExpress.XtraEditors.XtraForm
    {
        private bool UIDOk;
        StringBuilder tempStringBuilder = new StringBuilder();      //全局可变字符串实例，用于读取配置文件内容
        private string strCurrentDirectory;                         //应用程序路径
        //Infomation
        private string strLogPath;                      //log路径
        //Result
        private int iPass;
        private int iFail;
        //station
        string station;



        public PackCheck()
        {
            InitializeComponent();

            InitGlobleVariable();



        }
        public void InitGlobleVariable()
        {
            //!!!!!!!!!!!!!!上线后该地址要更改为：获取当前系统地址，再拼剪为formain的地址
            strCurrentDirectory = System.Environment.CurrentDirectory + "\\SetUp.ini";

            strLogPath = System.Environment.CurrentDirectory + "\\PackLog\\";
            if (!Directory.Exists(strLogPath))
            {
                Directory.CreateDirectory(strLogPath);
            }


            //Information


            //Result
            Win32API.GetPrivateProfileString("Result", "PackPass", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Result", "PackPass", "6", strCurrentDirectory);
            }
            else
            {
                textPass.Text = tempStringBuilder.ToString();
                iPass = int.Parse(tempStringBuilder.ToString());
            }

            Win32API.GetPrivateProfileString("Result", "PackFail", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Result", "PackFail", "6", strCurrentDirectory);

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

        }

        private void textUID_EditValueChanged(object sender, EventArgs e)
        {
            //textUID.SelectAll();
            if (textUID.Text.Length > 11)
            {
                textUID.ReadOnly = true;

                textSn.ReadOnly = false;
                textSn.Focus();
                textSn.SelectAll();
                UIDOk = true;

                textLog.Text = "";
                textLog.BackColor = Color.White;
            }
        }

        private void textSn_EditValueChanged(object sender, EventArgs e)
        {
            //textSn
            if (textSn.Text.Length == 12)//设置成活动的
            {
                if (UIDOk == true)
                {
                    UIDOk = false;
                    //查询Mysql,查询sn（查询不到报错-不存在）,查到对应的sn-uid，比对uid（对比不一致，报错）；
                    ClassMySqlHelp mySql = new ClassMySqlHelp();
                    string errLog = "";
                    Win32API.GetPrivateProfileString("Info", "Station", "", tempStringBuilder, 256, strCurrentDirectory);
                    station = tempStringBuilder.ToString();
                    int ret = mySql.SelectAndInsert(textSn.Text, textUID.Text,station, out errLog);
                    if (ret != 0)
                    {
                        PutResult(false, errLog);
                        //XtraMessageBox.Show(errLog);
                    }
                    else
                    {
                        string passLog = string.Format("SN:{0} 和 UID:{1}是对应关系", textSn.Text, textUID.Text);
                        PutResult(true, passLog);
                        //XtraMessageBox.Show(passLog);
                    }

                }
            }
            if (textSn.Text.Length > 11)/*长度大于等于6后调转光标并清空text*/
            {

                textSn.ReadOnly = true;
                textSn.Text = "";

                textUID.ReadOnly = false;
                textUID.Focus();
                textUID.SelectAll();
            }

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
                Win32API.WritePrivateProfileString("Result", "PackPass", iPass.ToString(), strCurrentDirectory);
                textTotal.Text = (iPass + iFail).ToString();
                textRate.Text = Math.Round((double)iPass*100 / (iPass + iFail), 2).ToString()+"%";

                WritePassResult(textSn.Text, textLog.Text, strLogPath, true);
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
                Win32API.WritePrivateProfileString("Result", "PackFail", iFail.ToString(), strCurrentDirectory);
                textTotal.Text = (iPass + iFail).ToString();
                textRate.Text = Math.Round((double)iPass * 100 / (iPass + iFail), 2).ToString() + "%";

                WritePassResult(textSn.Text, textLog.Text, strLogPath, false);
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
                    Sn = "FFFFFFFFF";
                    //updateUI = delegate {
                    textSn.Text = Sn;
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            iPass = 0;
            iFail = 0;
            textFail.Text = "0";
            textPass.Text = "0";
            textTotal.Text = "0";
            textRate.Text = "0.00";
            Win32API.WritePrivateProfileString("Result", "PackFail", "0", strCurrentDirectory);
            Win32API.WritePrivateProfileString("Result", "PackPass", "0", strCurrentDirectory);
            textUID.Focus();
            textUID.SelectAll();
        }



    }
}