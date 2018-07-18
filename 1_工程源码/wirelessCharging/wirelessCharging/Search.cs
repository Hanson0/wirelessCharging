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
using IMEISNPrint;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace wirelessCharging
{
    public partial class Search : DevExpress.XtraEditors.XtraForm
    {
        private const string excelName = "数据库导出数据";
        public static DataTable dataTable;
        SaveFileDialog saveFileDialog;

        StringBuilder tempStringBuilder = new StringBuilder();      //全局可变字符串实例，用于读取配置文件内容
        private string strCurrentDirectory;                         //应用程序路径

        //url
        //url
        private const string urlSearchBySn = "/wirelesscharging/test/sn/";
        private string urlSearchByTime = "/wirelesscharging/test/testTime/{0}/{1}";
        private const string urlSearchByBox = "/wirelesscharging/package/";

        public Search()
        {
            InitializeComponent();
            InitGlobleVariable();

        }
        public void InitGlobleVariable()
        {
            //!!!!!!!!!!!!!!上线后该地址要更改为：获取当前系统地址，再拼剪为formain的地址
            strCurrentDirectory = System.Environment.CurrentDirectory + "\\SetUp.ini";
            //strLogPath = System.Environment.CurrentDirectory + "\\TestLog\\";
            //if (!Directory.Exists(strLogPath))
            //{
            //    Directory.CreateDirectory(strLogPath);
            //}
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
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //textDateS.Text = dtPStart.Value.Year.ToString();
            //dtPStart.Value.
            //textDateS.Text = dtPStart.Value.ToString();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            //textDateE.Text = dtPEnd.Value.ToString("yyyy-MM-dd");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //组织发送字符串
            //textDateE.Text = dtPEnd.Value.ToString("yyyy-MM-dd");

            StringBuilder tempJson = new StringBuilder();
            if (rabtnCheckByDate.Checked)
            {
                //按日期查询
                string startTime = dtPStart.Value.ToString("yyyy-MM-dd") + " 00:00:00";
                string endTime = dtPEnd.Value.ToString("yyyy-MM-dd") + " 23:59:59";
                string urlByTime = string.Format(urlSearchByTime, startTime, endTime);
                SearchBySnOrTime(urlByTime);
            }
            else if (rabtnCheckBySn.Checked)
            {
                //按条码查询
                tempJson.Append(urlSearchBySn);
                tempJson.Append(textSn.Text);
                SearchBySnOrTime(tempJson.ToString());
            }
            else if (rabtnCheckByBox.Checked)
            {
                //按箱号查询
                tempJson.Append(urlSearchByBox);
                tempJson.Append(textBox.Text);
                SearchByBox(tempJson.ToString());
            }
            else
            {
                XtraMessageBox.Show("请选择查询方式");
            }



        }

        private void SearchBySnOrTime(string url)
        {
            #region SN / 日期 搜索
            string msg;
            RpSearchSnTimeInfo rpSearchSnTimeInfo;

            try
            {
                int result = HttpServer.GetSearchSnTimeInfo(url, out rpSearchSnTimeInfo, out msg);
                if (result != 0)
                {
                    XtraMessageBox.Show(msg);
                }
                else
                {
                    if (rpSearchSnTimeInfo.code != 1000)
                    {
                        XtraMessageBox.Show(msg);
                    }
                    else
                    {
                        XtraMessageBox.Show("查询成功");
                        //解析数据 
                        foreach (var item in rpSearchSnTimeInfo.data)
                        {
                            if (item.result == 0)
                            {
                                item.Result = "FAIL";
                            }
                            else
                            {
                                item.Result = "PASS";
                            }
                        }
                        for (int i = 0; i < rpSearchSnTimeInfo.data.Count; i++)
                        {
                            
                        //}

                        //foreach (var itemData in rpSearchSnTimeInfo.data)//list
                        //{
                            //从新给字符串itemData.data赋值 反序列化,判断每项的值是否为空，为空
                            string strItemDatadata="";
                            string[] strArryData = rpSearchSnTimeInfo.data[i].data.Split(',');//每一个List，中的data是一个字符串
                            
                            foreach (string item in strArryData)
                            {
                                int startIndex = item.IndexOf(':');
                                int endIndex = item.LastIndexOf('"');
                                string value =item.Substring(startIndex+1, endIndex-startIndex-1);
                                if (value.Length>1)//有值,添加这一项
                                {
                                    strItemDatadata += item+",";
                                } 
                                else if (value.Length == 1)//只包含:""中间的"
                                {
                                    if (item.Contains("{"))//第一项
                                    {
                                        strItemDatadata += "{";
                                    }
                                    if (item.Contains("}"))//第一项
                                    {
                                        strItemDatadata += "}";
                                    }
                                }

                            }
                            rpSearchSnTimeInfo.data[i].data = strItemDatadata.Remove(strItemDatadata.LastIndexOf(','), 1);
                            
                            //itemData.data = strItemDatadata;
	                    }
                        
                        string strRp = JsonConvert.SerializeObject(rpSearchSnTimeInfo);
                        dataTable = MyJsonConvert.ToDataTable(strRp);


                        for (int i = 0; i < dataTable.Columns.Count - 1; i++)
                        {
                            if (dataTable.Columns[i].ColumnName == "result")
                            {
                                dataTable.Columns.Remove("result");
                            }
                        }
                        dataTable.Columns.Remove("code");
                        dataTable.Columns.Remove("message");
                        //for (int i = 0; i < dataTable.Rows.Count; i++)
                        //{
                        //    if ((int)dataTable.Rows[i][2] ==0)
                        //    {
                        //            dataTable.Rows[i][2]="FAIL";
                        //    }
                        //                            else if((int)dataTable.Rows[i][3] ==1)
                        //    {
                        //            dataTable.Rows[i][2]="PASS";
                        //    }
			                 
                        //}

                        //ConvertSearchSnTimeInfo convertSearchSnTimeInfo;

                        //更新gridView

                        gridView1.Columns.Clear();
                        gridControl1.DataSource = dataTable;
                        gridControl1.Refresh();

                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            #endregion
        }

        private void SearchByBox(string url)
        {
            #region 根据箱号搜索
            string msg;
            RpSearchBoxInfo rpSearchBoxInfo;//rpSearchBoxInfo

            try
            {
                int result = HttpServer.GetSearchBoxInfo(url, out rpSearchBoxInfo, out msg);
                if (result != 0)
                {
                    XtraMessageBox.Show(msg);
                }
                else
                {
                    if (rpSearchBoxInfo.code != 1000)
                    {
                        XtraMessageBox.Show(msg);
                    }
                    else
                    {

                        XtraMessageBox.Show("查询成功");
                        //解析数据 
                        string strRp = JsonConvert.SerializeObject(rpSearchBoxInfo);
                        dataTable = MyJsonConvert.ToDataTable(strRp);
                        dataTable.Columns.Remove("code");
                        dataTable.Columns.Remove("message");

                        //更新gridView
                        //gridView1.OptionsFind.AlwaysVisible = true;
                        //gridView1.OptionsBehavior.Editable = true;
                        //gridView1.OptionsView.ColumnAutoWidth = true;

                        gridView1.Columns.Clear();

                        gridControl1.DataSource = dataTable;
                        //gridControl1.Refresh();

                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            #endregion
        }
        private void Search_Load(object sender, EventArgs e)
        {
            gridView1.OptionsFind.AlwaysVisible = true;
            gridView1.OptionsBehavior.Editable = true;
            gridView1.OptionsView.ColumnAutoWidth = true;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            #region
            try
            {
                saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Execl files(*.xls)|*.xls";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true; //保存对话框是否记忆上次打开的目录 
                //saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "导出Excel文件到";
                DateTime now = DateTime.Now;//.PadLeft(2)
                saveFileDialog.FileName = "查询记录表" + now.Year.ToString().PadLeft(2) + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0') + "-" + now.Hour.ToString().PadLeft(2, '0') + now.Minute.ToString().PadLeft(2, '0') + now.Second.ToString().PadLeft(2, '0');
                //saveFileDialog.FileName = "维修记录报表" + now.ToString();//文件不能用/命名
                //点了保存按钮进入   
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FileName.Trim() == "")
                    {
                        XtraMessageBox.Show("请输入要保存的文件名", "提示");
                        return;
                    }
                    splashScreenManager1.ShowWaitForm();//开始显示
                    ExportTOExcel(saveFileDialog.FileName);//数据导入完成
                    Thread.Sleep(3000);
                    splashScreenManager1.CloseWaitForm();//关闭显示

                    Thread.Sleep(500);
                    XtraMessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
            #endregion
        }

        public void ExportTOExcel(string path)
        {
            CompositeLink complink = new CompositeLink(new PrintingSystem());//复合链路
            PrintableComponentLink link = new PrintableComponentLink();//可打印组件链接
            link.Component = gridControl1;
            complink.Links.Add(link);//Links:获取一个合成基对象的链接的集合;Add:添加指定的DevExpress.XtraPrinting对象到集合


            FileInfo fileInfo;


            complink.ExportToXls(path, new XlsExportOptions() { ExportMode = XlsExportMode.SingleFile, ShowGridLines = true });
            fileInfo = new FileInfo(path);
            //fileInfo.IsReadOnly = true;//只读文件

            //Close();
        }



    }
}