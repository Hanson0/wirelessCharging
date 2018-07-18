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
using System.Threading;
using Windows;

using JiaHao.MySqlHelp;
using JiaHao.ExcelHelp;
using JiaHao.RegexHelp;

namespace MeiFenSys
{
    public partial class PlanIn : DevExpress.XtraEditors.XtraForm
    {
        private const string eachErrorTip = "此项必填";
        private const string Tip = "温馨提示：您有未填信息";
        private const string totalErrorTip = "温馨提示：您输入的信息有误";

        StringBuilder tempStringBuilder = new StringBuilder();      //全局可变字符串实例，用于读取配置文件内容
        private string strCurrentDirectory;                         //应用程序路径
        //private string xlsxCurrentDirectory;                        //xlsx路径

        public PlanIn()
        {
            InitializeComponent();
            InitGlobleVariable();

        }
        public void InitGlobleVariable()
        {
            strCurrentDirectory = System.Environment.CurrentDirectory + "\\SetUp.ini";

            //strLogPath = System.Environment.CurrentDirectory + "\\SNUnBindLog\\";
            //if (!Directory.Exists(strLogPath))
            //{
            //    Directory.CreateDirectory(strLogPath);
            //}

            ////HTTP
            //Win32API.GetPrivateProfileString("Http", "IP", "", tempStringBuilder, 256, strCurrentDirectory);
            //HttpServer.Ip = tempStringBuilder.ToString();
            //Win32API.GetPrivateProfileString("Http", "Port", "", tempStringBuilder, 256, strCurrentDirectory);
            //HttpServer.Port = tempStringBuilder.ToString();

            //Result
            Win32API.GetPrivateProfileString("Info", "SnData", "", tempStringBuilder, 256, strCurrentDirectory);
            if (tempStringBuilder.ToString() == "")
            {
                Win32API.WritePrivateProfileString("Info", "SnData", "1804", strCurrentDirectory);
            }
            else
            {
                textSnFormat.Text = tempStringBuilder.ToString();
                //iPass = int.Parse(tempStringBuilder.ToString());
            }



        }


        private void btnSureInput_Click(object sender, EventArgs e)
        {

            #region 是否填写判断
            bool planCodeIsNull = string.IsNullOrEmpty(textPlancode.Text);
            bool quantityIsNull = string.IsNullOrEmpty(textQuantity.Text);
            bool excelPathIsNull = string.IsNullOrEmpty(textExcelPath.Text);
            bool textSnFormatNull = string.IsNullOrEmpty(textSnFormat.Text);


            if (planCodeIsNull == false && quantityIsNull == false && excelPathIsNull == false && textSnFormatNull == false)
            {
                string order = textPlancode.Text.Trim();//订单
                string data = textSnFormat.Text.Trim();//日期

                labPlanCodeErrorTip.Text = "";
                labQuantityErrorTip.Text = "";
                labExcelPathErrorTip.Text = "";
                labSnFormatErrorTip.Text = "";

                labTips.Text = "";
                //此处加入数据库查询与插入
                //ClassMySqlHelp mySql = new ClassMySqlHelp();
                //从MySql中读取数据显示from MySqldataBase to show
                //DataTable dt = mySql.Test();
                //Excel excel = new Excel(dt);
                //excel.ShowDialog();

                //订单信息(测试数据)导入MySql   Inport to MySqldataBase
                //mySql.Import();

                //订单号位数
                if (order.Length != 10)
                {
                    labTips.Text = totalErrorTip;
                    labPlanCodeErrorTip.Text = "请输入10位订单号";
                    return;
                }

                //数量的正整数规范
                ClassMyRegex regex = new ClassMyRegex();
                if (regex.RegexIsPosIntMatch(textQuantity.Text) == false)
                {
                    labTips.Text = totalErrorTip;
                    labQuantityErrorTip.Text = "请输入正整数";
                    return;
                }
                //SN规则中日期的长度
                if (data.Length != 4)
                {
                    labTips.Text = totalErrorTip;
                    labSnFormatErrorTip.Text = "请输入4位日期（年月），如：1804";
                    return;
                }
                //SN规则中日期的正整数规范
                if (regex.RegexIsPosIntMatch(data) == false)
                {
                    labTips.Text = totalErrorTip;
                    labSnFormatErrorTip.Text = "请输入正整数的4位日期（年月），如：1804";
                    return;
                }
                //textSnFormat日期通过,就记忆
                Win32API.WritePrivateProfileString("Info", "SnData", data, strCurrentDirectory);
                //从excel读取数据到ds
                try
                {
                    ClassExcelAndDt excelAndDt = new ClassExcelAndDt();
                    DataSet dsFromExcel = new DataSet();
                    DataTable dbFromExcel = new DataTable();
                    
                    dbFromExcel = excelAndDt.ExcelToDS(textExcelPath.Text, order);
                    if (dbFromExcel == null)
                    {
                        XtraMessageBox.Show("Excel中未找到订单号的表\r\n提示:请检查订单号和Excel及其路径");
                        return;
                    }
                    //dbFromExcel = dsFromExcel.Tables[0];


                    //数量比较
                    int quantity = Convert.ToInt32(textQuantity.Text);//int.Parse(textQuantity.Text);
                    if (quantity != dbFromExcel.Rows.Count)
                    {
                        labQuantityErrorTip.Text = "数量与Excel表格不一致";
                        return;
                    }
                    //显示等待进度条；
                    splashScreenManager1.ShowWaitForm();
                    //开始操作mySql
                    ClassMySqlHelp mySql = new ClassMySqlHelp();
                    string returnSn = "";
                    int ret = mySql.Import(dbFromExcel, order, data,out returnSn);
                    if (ret == -1)//插入失败了
                    {
                        if (string.IsNullOrEmpty(returnSn))
                        {
                            Thread.Sleep(3000);
                            splashScreenManager1.CloseWaitForm();
                            XtraMessageBox.Show("数据库操作失败");

                            return;
                        }
                        Thread.Sleep(3000);
                        splashScreenManager1.CloseWaitForm();
                        XtraMessageBox.Show(string.Format("导入失败，检测到SN:{0}已存在", returnSn));
                        return;
                    }
                    Thread.Sleep(3000);
                    splashScreenManager1.CloseWaitForm();
                    XtraMessageBox.Show("导入完成");
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                    return;
                }

                return;
            }

            if (planCodeIsNull || quantityIsNull || excelPathIsNull || textSnFormatNull)
            {
                labTips.Text = Tip;
            }

            if (planCodeIsNull)
            {
                labPlanCodeErrorTip.Text = eachErrorTip;
            }
            else
            {
                labPlanCodeErrorTip.Text = "";
            }

            if (quantityIsNull)
            {
                labQuantityErrorTip.Text = eachErrorTip;
            }
            else
            {
                labQuantityErrorTip.Text = "";
            }

            if (textSnFormatNull)
            {
                labSnFormatErrorTip.Text = eachErrorTip;
            }
            else
            {
                labSnFormatErrorTip.Text = "";
            }

            if (excelPathIsNull)
            {
                labExcelPathErrorTip.Text = eachErrorTip;
            }
            else
            {
                labExcelPathErrorTip.Text = "";
            }
            #endregion


        }

        private void btnSelectExcelPath_Click(object sender, EventArgs e)
        {
            ClassPathSelect excelPath = new ClassPathSelect();

            string path = excelPath.SelectPath(1);
            if (path == null)
            {
                XtraMessageBox.Show("提示：模式错误，路径不存在");
                return;
            }
            textExcelPath.Text = path;
        }
    }
}