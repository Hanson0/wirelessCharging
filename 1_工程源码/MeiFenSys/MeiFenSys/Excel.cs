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
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;
using System.IO;

using System.Threading;

namespace MeiFenSys
{
    public partial class Excel : DevExpress.XtraEditors.XtraForm
    {

        private const string excelName = "数据库导出数据";
        public static DataTable dataTable;
        SaveFileDialog saveFileDialog;

        public Excel()//DataTable dt
        {
            InitializeComponent();
            //dataTable = dt;
        }

        public void SaveDataToExcel()
        {
            CompositeLink complink = new CompositeLink(new PrintingSystem());//复合链路
            PrintableComponentLink link = new PrintableComponentLink();//可打印组件链接
            link.Component = gridControl1;
            complink.Links.Add(link);//Links:获取一个合成基对象的链接的集合;Add:添加指定的DevExpress.XtraPrinting对象到集合
            //complink.CreatePageForEachLink();

            string path = "./excel\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            FileInfo fileInfo;

            if (File.Exists(path + excelName + ".xls"))
            {
                //fileInfo = new FileInfo(path + excelName + ".xls");
                //fileInfo.IsReadOnly = false;
                //fileInfo.Delete();
            }

            complink.ExportToXls(path + excelName + ".xls", new XlsExportOptions() { ExportMode = XlsExportMode.SingleFile, ShowGridLines = true });
            //complink.ExportToXlsx(path + casenumber + ".xlsx", new XlsxExportOptions() { ExportMode = XlsExportMode.SingleFile, ShowGridLines = true });
            fileInfo = new FileInfo(path + excelName + ".xls");
            //fileInfo.IsReadOnly = true;//只读文件
            XtraMessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Close();
        }

        private void Excel_Load(object sender, EventArgs e)
        {
            gridView1.OptionsFind.AlwaysVisible = true;

            gridView1.OptionsBehavior.Editable = true;

            gridView1.OptionsView.ColumnAutoWidth = true;

            gridControl1.DataSource = dataTable;
            gridControl1.Refresh();
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
                saveFileDialog.FileName = "美分产品记录表" + now.Year.ToString().PadLeft(2) + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0') + "-" + now.Hour.ToString().PadLeft(2, '0') + now.Minute.ToString().PadLeft(2, '0') + now.Second.ToString().PadLeft(2, '0');
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