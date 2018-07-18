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

using Windows;
using System.IO;
namespace wirelessCharging
{
    public partial class Setting : DevExpress.XtraEditors.XtraForm
    {
        public static string selectI;
        public static string selectProduct;
        public static string selectParam;
        public static bool isLocked;
        public string allParam;

        //测试项
        public static int testIin;
        public static int testVout;
        public static int testEff;
        public static int testFreq;

        StringBuilder tempStringBuilder = new StringBuilder();      //全局可变字符串实例，用于读取配置文件内容
        private string strCurrentDirectory;                         //应用程序路径
        private string strParamDirectory;                         //应用程序路径
        public Setting()
        {
            InitializeComponent();
            InitGlobleVariable();
        }
        public void InitGlobleVariable()
        {
            //!!!!!!!!!!!!!!上线后该地址要更改为：获取当前系统地址，再拼剪为formain的地址
            strCurrentDirectory = System.Environment.CurrentDirectory + "\\SetUp.ini";
            strParamDirectory = System.Environment.CurrentDirectory + "\\Param.txt";
            //strLogPath = System.Environment.CurrentDirectory + "\\Log\\";
            //if (!Directory.Exists(strLogPath))
            //{
            //    Directory.CreateDirectory(strLogPath);
            //}

            //界面恢复
            //Set-Param
            #region
            Win32API.GetPrivateProfileString("Set", "Param50", "", tempStringBuilder, 1024, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Set", "Param50", "120;1.3;1.4;1.4", strCurrentDirectory);
            }
            else
            {
                allParam = tempStringBuilder.ToString();
            }

            Win32API.GetPrivateProfileString("Set", "Param100", "", tempStringBuilder, 1024, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Set", "Param100", "120;1.3;1.4;1.4", strCurrentDirectory);
            }
            else
            {
                allParam += tempStringBuilder.ToString();
            }

            Win32API.GetPrivateProfileString("Set", "Param150", "", tempStringBuilder, 1024, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Set", "Param150", "120;1.3;1.4;1.4", strCurrentDirectory);
            }
            else
            {
                allParam += tempStringBuilder.ToString();
            }

            Win32API.GetPrivateProfileString("Set", "Param200", "", tempStringBuilder, 1024, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Set", "Param200", "120;1.3;1.4;1.4", strCurrentDirectory);
            }
            else
            {
                allParam += tempStringBuilder.ToString();
            }

            Win32API.GetPrivateProfileString("Set", "Param300", "", tempStringBuilder, 1024, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Set", "Param300", "120;1.3;1.4;1.4", strCurrentDirectory);
            }
            else
            {
                allParam += tempStringBuilder.ToString();
            }

            Win32API.GetPrivateProfileString("Set", "Param400", "", tempStringBuilder, 1024, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Set", "Param400", "120;1.3;1.4;1.4", strCurrentDirectory);
            }
            else
            {
                allParam += tempStringBuilder.ToString();
            }
            Win32API.GetPrivateProfileString("Set", "Param500", "", tempStringBuilder, 1024, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Set", "Param500", "120;1.3;1.4;1.4", strCurrentDirectory);
            }
            else
            {
                allParam += tempStringBuilder.ToString();
            }

            Win32API.GetPrivateProfileString("Set", "Param600", "", tempStringBuilder, 1024, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Set", "Param600", "120;1.3;1.4;1.4", strCurrentDirectory);
            }
            else
            {
                allParam += tempStringBuilder.ToString();

            }
            Win32API.GetPrivateProfileString("Set", "Param1000", "", tempStringBuilder, 1024, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Set", "Param1000", "100;1.3;1.4;1.4;", strCurrentDirectory);
            }
            else
            {
                allParam += tempStringBuilder.ToString();
                string[] paramArry = allParam.Split(';');
                Control[] panelArry = { panelParam, panelParam100mA, panelParam150mA, panelParam200mA, panelParam300mA, panelParam400mA, panelParam500mA, panelParam600mA,panelParam1000mA };
                SetValueToControls(panelArry, paramArry);
            }
            #endregion

            //Set-ProductName
            Win32API.GetPrivateProfileString("Set", "ProductName1", "", tempStringBuilder, 1024, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Set", "ProductName1", "WPC VT65", strCurrentDirectory);
            }
            else
            {
                checkProName1 .Text= tempStringBuilder.ToString();
            }
            Win32API.GetPrivateProfileString("Set", "ProductName2", "", tempStringBuilder, 1024, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Set", "ProductName2", "WPC VG20", strCurrentDirectory);
            }
            else
            {
                checkProName2.Text = tempStringBuilder.ToString();
            }
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
                    if (testIin == 1)
                    {
                        labelControl1.BackColor = Color.Green;
                    }
                    else if (testIin == 0)
                    {
                        labelControl1.BackColor = Color.OrangeRed;
                    }
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
                    if (testVout == 1)
                    {
                        labelControl2.BackColor = Color.Green;
                    }
                    else if (testVout == 0)
                    {
                        labelControl2.BackColor = Color.OrangeRed;
                    }
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
                    if (testEff == 1)
                    {
                        labelControl3.BackColor = Color.Green;
                    }
                    else if (testEff == 0)
                    {
                        labelControl3.BackColor = Color.OrangeRed;
                    }
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
                    if (testFreq == 1)
                    {
                        labelControl4.BackColor = Color.Green;
                    }
                    else if (testFreq == 0)
                    {
                        labelControl4.BackColor = Color.OrangeRed;
                    }
            }
            #endregion
            //Information


        }
        //单选框事件
        private void checkProName1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkProName1.Checked)
            {
                //清除所有勾选
                ClearCheckBox();
                Control[] panelArry = { panelParam, panelParam100mA, panelParam150mA, panelParam200mA, panelParam300mA, panelParam400mA, panelParam500mA, panelParam600mA };
                ClearColorFromControls(panelArry);
                //勾选电流值，设置对应的参数范围
                check400mA.Checked = true;
                //check150mA.Checked = true;
                //textI300mAIS.Text = "700";
                //textI300mAIE.Text = "650";
                textI400mAIE.BackColor = Color.AliceBlue;
                textI400mAIS.BackColor = Color.AliceBlue;
                //Vout
                //textI300mAVS.Text = "4.00";
                //textI300mAVE.Text = "4.50";
                textI400mAVS.BackColor = Color.AliceBlue;
                textI400mAVE.BackColor = Color.AliceBlue;
                //Eff
                //textI300mAES.Text = "0.9";
                //textI300mAEE.Text = "1.00";
                textI400mAES.BackColor = Color.AliceBlue;
                textI400mAEE.BackColor = Color.AliceBlue;
                //Freq
                //textI300mAFS.Text = "120";
                //textI300mAFE.Text = "130";
                textI400mAFS.BackColor = Color.AliceBlue;
                textI400mAFE.BackColor = Color.AliceBlue;
                //清空参数
                //ClearText();
                //设置对应参数

            }

        }
        private void checkProName2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkProName2.Checked)
            {
                //清除所有勾选
                ClearCheckBox();
                //清除所有颜色
                Control[] panelArry = { panelParam, panelParam100mA, panelParam150mA, panelParam200mA, panelParam300mA, panelParam400mA, panelParam500mA, panelParam600mA };
                ClearColorFromControls(panelArry);
                //勾选电流值，设置对应的参数范围
                //check400mA.Checked = true;
                check50mA.Checked = true;

                //清空参数
                //ClearText();
                //100mA
                //Iin
                //textI200mAIS.Text = "700";
                //textI200mAIE.Text = "650";
                textI50mAIE.BackColor = Color.AliceBlue;
                textI50mAIS.BackColor = Color.AliceBlue;
                //Vout
                //textI200mAVS.Text = "4.00";
                //textI200mAVE.Text = "4.50";
                textI50mAVS.BackColor = Color.AliceBlue;
                textI50mAVE.BackColor = Color.AliceBlue;
                //Eff
                //textI200mAES.Text = "0.9";
                //textI200mAEE.Text = "1.00";
                textI50mAES.BackColor = Color.AliceBlue;
                textI50mAEE.BackColor = Color.AliceBlue;
                //Freq
                //textI200mAFS.Text = "120";
                //textI200mAFE.Text = "130";
                textI50mAFS.BackColor = Color.AliceBlue;
                textI50mAFE.BackColor = Color.AliceBlue;
            }

        }


        //清空CheckBOX
        private void ClearCheckBox()
        {
            check50mA.Checked = false;
            check400mA.Checked = false;
            check500mA.Checked = false;
            check600mA.Checked = false;
            check300mA.Checked = false;
            check200mA.Checked = false;
            check150mA.Checked = false;
            check100mA.Checked = false;
            check1000mA.Checked = false;
        }
        //private void ClearText()
        //{
        //    foreach(Control c in this.Controls)
        //    {
        //        if (c is TextEdit)//判断是否是textBox控件，是则清空
        //        {
        //            c.Text = "";
        //        }
        //    }
        //}
        //清空ParamText内容和颜色
        public void ClearTextFromControls(Control groupControl)
        {
            ////获取Group中每个控件
            var a = groupControl.Controls;
            //将a集合中的每一个元素（每个控件）依次赋给b
            foreach (var b in a)
            {
                //判断控件类型为RadioButton
                if (b.GetType() == typeof(TextEdit))
                {
                    //强制转换成RadioButton类型
                    var c = (TextEdit)b;
                    c.Text = "";
                    c.BackColor = Color.White;
                }

            }
        }
        //只清空颜色
        public void ClearColorFromControls(Control[] group)
        {
            ////获取Group中每个控件
            foreach (var groupControl in group)
            {
                var a = groupControl.Controls;
                //将a集合中的每一个元素（每个控件）依次赋给b
                foreach (var b in a)
                {
                    //判断控件类型为RadioButton
                    if (b.GetType() == typeof(TextEdit))
                    {
                        //强制转换成RadioButton类型
                        var c = (TextEdit)b;
                        c.BackColor = Color.White;
                    }

                }
            }
        }
        //获取有颜色的（即:选中的）参数值
        public string GetColorValueFromControls(Control groupControl)
        {
            ////获取Group中每个控件
            string s = null;

            var a = groupControl.Controls;
            //将a集合中的每一个元素（每个控件）依次赋给b
            foreach (var b in a)
            {
                //判断控件类型为RadioButton
                if (b.GetType() == typeof(TextEdit))
                {
                    //强制转换成RadioButton类型
                    var c = (TextEdit)b;
                    if (c.BackColor == Color.AliceBlue)
                    {
                        //if (string.IsNullOrEmpty(c.Text)==false)
                        //{
                            //s = s + c.Text + ";";
                        //}
                            s = s + c.Text + ";";

                    }
                }

            }
            return s;
        }
        public string GetAllValueFromControls(Control groupControl)
        {
            ////获取Group中每个控件
            string s = null;

            var a = groupControl.Controls;
            //将a集合中的每一个元素（每个控件）依次赋给b
            foreach (var b in a)
            {
                //判断控件类型为RadioButton
                if (b.GetType() == typeof(TextEdit))
                {
                    //强制转换成RadioButton类型
                    var c = (TextEdit)b;
                    s = s + c.Text + ";";
                }

            }
            return s;
        }
        public void SetValueToControls(Control[] group,string[] value)
        {
            int i = 0;

            foreach (var groupControl in group)
            {
                var a = groupControl.Controls;
                //将a集合中的每一个元素（每个控件）依次赋给b
                foreach (var b in a)
                {
                    //判断控件类型为RadioButton
                    if (b.GetType() == typeof(TextEdit))
                    {

                        //强制转换成RadioButton类型
                        var c = (TextEdit)b;
                        c.Text = value[i];
                        i++;
                    }

                }
            }
        }
        //清空radio
        private void ClearRadio()
        {
            checkProName1.Checked = false;
            checkProName2.Checked = false;
        }

        //全体使能
        private void EnAbleAll()
        {
            panelI.Enabled = true;
            groupControl1.Enabled = true;
            EnAbleParam();
        }
        private void EnAbleParam()
        {
            panelParam.Enabled = true;
            panelParam100mA.Enabled = true;
            panelParam150mA.Enabled = true;
            panelParam200mA.Enabled = true;
            panelParam300mA.Enabled = true;
            panelParam400mA.Enabled = true;
            panelParam500mA.Enabled = true;
            panelParam600mA.Enabled = true;
            panelParam1000mA.Enabled = true;
        }
        private void DisAbleParam()
        {
            panelParam.Enabled = false;
            panelParam100mA.Enabled = false;
            panelParam150mA.Enabled = false;
            panelParam200mA.Enabled = false;
            panelParam300mA.Enabled = false;
            panelParam400mA.Enabled = false;
            panelParam500mA.Enabled = false;
            panelParam600mA.Enabled = false;
            panelParam1000mA.Enabled = false;
        }
        //全体不使能
        private void DisableAll()
        {
            panelI.Enabled = false;
            groupControl1.Enabled = false;
            DisAbleParam();
        }
        //按键事件
        private void btnClearText_Click(object sender, EventArgs e)
        {
            ClearRadio();
            ClearCheckBox();
            ClearTextFromControls(panelParam);
        }

        public static string GetValueFromControls(Control groupControl)
        {
            ////获取Group中每个控件
            string s = null;

            var a = groupControl.Controls;
            //将a集合中的每一个元素（每个控件）依次赋给b
            foreach (var b in a)
            {
                //判断控件类型为RadioButton
                if (b.GetType() == typeof(CheckEdit))
                {
                    //强制转换成RadioButton类型
                    var c = (CheckEdit)b;
                    if (c.Checked == true)
                    {
                        s = s + c.Text + ";";

                    }

                }

            }
            return s;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            if (!checkProName1.Checked && !checkProName2.Checked)
            {
                XtraMessageBox.Show("请选择产品型号");
                return;
            }
            if (!check50mA.Checked && !check400mA.Checked && !check500mA.Checked && !check600mA.Checked && !check300mA.Checked && !check200mA.Checked && !check150mA.Checked && !check100mA.Checked && !check1000mA.Checked)
            {
                XtraMessageBox.Show("请选择测试电流值");
                return;
            }
            //设置
            //取型号
            if (checkProName1.Checked)
            {
                selectProduct = checkProName1.Text;
            }
            else
            {
                selectProduct = checkProName2.Text;
            }
            //取测试电流
            selectI = GetValueFromControls(panelI);

            //取选中的参数范围
            string eachSelectParam = GetColorValueFromControls(panelParam);
            if (File.Exists(strParamDirectory))
            {
                File.Delete(strParamDirectory);
            }

            WriteTxt(eachSelectParam);

            selectParam = eachSelectParam;

            eachSelectParam = GetColorValueFromControls(panelParam100mA);
            WriteTxt(eachSelectParam);
            selectParam += eachSelectParam;

            eachSelectParam = GetColorValueFromControls(panelParam150mA);
            WriteTxt(eachSelectParam);
            selectParam += eachSelectParam;

            eachSelectParam = GetColorValueFromControls(panelParam200mA);
            WriteTxt(eachSelectParam);
            selectParam += eachSelectParam;

            eachSelectParam = GetColorValueFromControls(panelParam300mA);
            WriteTxt(eachSelectParam);
            selectParam += eachSelectParam;

            eachSelectParam = GetColorValueFromControls(panelParam400mA);
            WriteTxt(eachSelectParam);
            selectParam += eachSelectParam;

            eachSelectParam = GetColorValueFromControls(panelParam500mA);
            WriteTxt(eachSelectParam);
            selectParam += eachSelectParam;

            eachSelectParam = GetColorValueFromControls(panelParam600mA);
            WriteTxt(eachSelectParam);
            selectParam += eachSelectParam;

            eachSelectParam = GetColorValueFromControls(panelParam1000mA);
            WriteTxt(eachSelectParam);
            selectParam += eachSelectParam;

            //选择的参数写入txt文件
            //WriteTxt(selectParam);
            //全部参数写入INI文件....
            writeParamToIni();
            //写测试项目
            WriteTestItem();

            DisableAll();
            isLocked = true;
            WpcTest.updateSelectedI = true;
        }
        //写入INI文件，保存参数设置
        private string writeParamToIni()
        {
            string strParam=null;
            strParam = GetAllValueFromControls(panelParam);
            Win32API.WritePrivateProfileString("Set", "Param50", strParam, strCurrentDirectory);

            strParam = GetAllValueFromControls(panelParam100mA);
            Win32API.WritePrivateProfileString("Set", "Param100", strParam, strCurrentDirectory);

            strParam = GetAllValueFromControls(panelParam150mA);
            Win32API.WritePrivateProfileString("Set", "Param150", strParam, strCurrentDirectory);

            strParam = GetAllValueFromControls(panelParam200mA);
            Win32API.WritePrivateProfileString("Set", "Param200", strParam, strCurrentDirectory);

            strParam = GetAllValueFromControls(panelParam300mA);
            Win32API.WritePrivateProfileString("Set", "Param300", strParam, strCurrentDirectory);

            strParam = GetAllValueFromControls(panelParam400mA);
            Win32API.WritePrivateProfileString("Set", "Param400", strParam, strCurrentDirectory);

            strParam = GetAllValueFromControls(panelParam500mA);
            Win32API.WritePrivateProfileString("Set", "Param500", strParam, strCurrentDirectory);

            strParam = GetAllValueFromControls(panelParam600mA);
            Win32API.WritePrivateProfileString("Set", "Param600", strParam, strCurrentDirectory);

            strParam = GetAllValueFromControls(panelParam1000mA);
            Win32API.WritePrivateProfileString("Set", "Param1000", strParam, strCurrentDirectory);
            //选择的产品名称
            Win32API.WritePrivateProfileString("Set", "UsedProductName", selectProduct, strCurrentDirectory);
            //选择的测试电流
            Win32API.WritePrivateProfileString("Set", "SelectedI", selectI, strCurrentDirectory);
            return strParam;
        }

        public void WriteTxt(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return;
            }
            if (!File.Exists(strParamDirectory))
            {
                FileStream fs = new FileStream(strParamDirectory, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                long fl = fs.Length;
                fs.Seek(fl, SeekOrigin.End);
                sw.WriteLine(content);//开始写入值
                sw.Close();
                fs.Close();


            }
            else
            {
                FileStream fs = new FileStream(strParamDirectory, FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                long fl = fs.Length;
                fs.Seek(fl, SeekOrigin.Begin);

                sw.WriteLine(content);//开始写入值
                sw.Close();
                fs.Close();
            }

            //FileStream fs = new FileStream(strParamDirectory, FileMode.Create);
            ////获得字节数组
            //byte[] data = System.Text.Encoding.Default.GetBytes(content);
            ////开始写入
            //fs.Write(data, 0, data.Length);
            ////清空缓冲区、关闭流
            //fs.Flush();
            //fs.Close();
        }

        private void WriteTestItem()
        {
            if (labelControl1.BackColor==Color.Green)
            {
                Win32API.WritePrivateProfileString("TestItem", "TestIin", "1", strCurrentDirectory);
                testIin = 1;
            }
            else if (labelControl1.BackColor == Color.OrangeRed)
            {
                Win32API.WritePrivateProfileString("TestItem", "TestIin", "0", strCurrentDirectory);
                testIin = 0;
            }

            if (labelControl2.BackColor == Color.Green)
            {
                Win32API.WritePrivateProfileString("TestItem", "TestVout", "1", strCurrentDirectory);
                testVout = 1;
            }
            else if (labelControl2.BackColor == Color.OrangeRed)
            {
                Win32API.WritePrivateProfileString("TestItem", "TestVout", "0", strCurrentDirectory);
                testVout = 0;
            }

            if (labelControl3.BackColor == Color.Green)
            {
                Win32API.WritePrivateProfileString("TestItem", "TestEff", "1", strCurrentDirectory);
                testEff = 1;
            }
            else if (labelControl3.BackColor == Color.OrangeRed)
            {
                Win32API.WritePrivateProfileString("TestItem", "TestEff", "0", strCurrentDirectory);
                testEff = 0;
            }

            if (labelControl4.BackColor == Color.Green)
            {
                Win32API.WritePrivateProfileString("TestItem", "TestFreq", "1", strCurrentDirectory);
                testFreq = 1;
            }
            else if (labelControl4.BackColor == Color.OrangeRed)
            {
                Win32API.WritePrivateProfileString("TestItem", "TestFreq", "0", strCurrentDirectory);
                testFreq = 0;
            }

        }

        //解锁
        private void btnCancel_Click(object sender, EventArgs e)
        {
            EnAbleAll();
            isLocked = false;
        }




        private void check50mA_CheckedChanged(object sender, EventArgs e)
        {
            if (check50mA.Checked)
            {
                SetColor("50", Color.AliceBlue);
                //string[] iValue = { "50", "100", "150", "200", "300", "400", "500", "600" };
                //foreach (var eachIValue in iValue)
                //{
                //    SetColor(eachIValue, Color.AliceBlue);
                //}
            }
            else
            {
                SetColor("50", Color.White);
                //string[] iValue = { "50", "100", "150", "200", "300", "400", "500", "600" };
                //foreach (var eachIValue in iValue)
                //{
                //    SetColor(eachIValue, Color.White);
                //}
            }

        }


        private void check100mA_CheckedChanged(object sender, EventArgs e)
        {
            if (check100mA.Checked)
            {
                SetColor("100", Color.AliceBlue);
            }
            else
            {
                SetColor("100", Color.White);
            }
        }

        private void check150mA_CheckedChanged(object sender, EventArgs e)
        {
            if (check150mA.Checked)
            {
                SetColor("150", Color.AliceBlue);
            }
            else
            {
                SetColor("150", Color.White);
            }
        }

        private void check200mA_CheckedChanged(object sender, EventArgs e)
        {
            if (check200mA.Checked)
            {
                SetColor("200", Color.AliceBlue);
            }
            else
            {
                SetColor("200", Color.White);
            }
        }

        private void check300mA_CheckedChanged(object sender, EventArgs e)
        {
            if (check300mA.Checked)
            {
                SetColor("300", Color.AliceBlue);
            }
            else
            {
                SetColor("300", Color.White);
            }
        }

        private void check400mA_CheckedChanged(object sender, EventArgs e)
        {
            if (check400mA.Checked)
            {
                SetColor("400", Color.AliceBlue);
            }
            else
            {
                SetColor("400", Color.White);
            }
        }

        private void check500mA_CheckedChanged(object sender, EventArgs e)
        {
            if (check500mA.Checked)
            {
                SetColor("500", Color.AliceBlue);
            }
            else
            {
                SetColor("500", Color.White);
            }
        }

        private void check600mA_CheckedChanged(object sender, EventArgs e)
        {
            if (check600mA.Checked)
            {
                SetColor("600", Color.AliceBlue);
            }
            else
            {
                SetColor("600", Color.White);
            }
        }
        private void check1000mA_CheckedChanged(object sender, EventArgs e)
        {
            if (check1000mA.Checked)
            {
                SetColor("1000", Color.AliceBlue);
            }
            else
            {
                SetColor("1000", Color.White);
            }
        }

        private void SetColor(string mA, Color color)
        {
            //50mA的每项起始参数
            string[] paramFrame = { "textI" + mA + "mAIS", "textI" + mA + "mAIE", "textI" + mA + "mAVS", "textI" + mA + "mAVE", "textI" + mA + "mAES", "textI" + mA + "mAEE", "textI" + mA + "mAFS", "textI" + mA + "mAFE" };

            foreach (var name in paramFrame)
            {
                object o = this.GetType().GetField(name, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                ((TextEdit)o).BackColor = color;//SkyBlue;
            }

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            if (labelControl1.BackColor==Color.Green)
            {
                labelControl1.BackColor = Color.OrangeRed;
            }
            else if (labelControl1.BackColor == Color.OrangeRed)
            {
                labelControl1.BackColor = Color.Green;
            }
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {
            if (labelControl2.BackColor == Color.Green)
            {
                labelControl2.BackColor = Color.OrangeRed;
            }
            else if (labelControl2.BackColor == Color.OrangeRed)
            {
                labelControl2.BackColor = Color.Green;
            }
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {
            if (labelControl3.BackColor == Color.Green)
            {
                labelControl3.BackColor = Color.OrangeRed;
            }
            else if (labelControl3.BackColor == Color.OrangeRed)
            {
                labelControl3.BackColor = Color.Green;
            }
        }

        private void labelControl4_Click(object sender, EventArgs e)
        {
            if (labelControl4.BackColor == Color.Green)
            {
                labelControl4.BackColor = Color.OrangeRed;
            }
            else if (labelControl4.BackColor == Color.OrangeRed)
            {
                labelControl4.BackColor = Color.Green;
            }
        }


    }
}