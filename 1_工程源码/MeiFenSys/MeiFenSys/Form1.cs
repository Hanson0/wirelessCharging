using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraEditors;

using JiaHao.DX.FormBuild;
using JiaHao.MySqlHelp;
using Windows;
using System.Reflection;
using System.Threading;

namespace MeiFenSys
{
    public delegate void Handled1();//定义一个委托
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        CreateForm creatForm = new CreateForm();

        StringBuilder tempStringBuilder = new StringBuilder();      //全局可变字符串实例，用于读取配置文件内容
        private string strCurrentDirectory;                         //应用程序路径


        //private string strServer;
        //private string strUser;
        //private string strPassword;
        //private string strDatabaseName;

        public Form1()
        {
            InitializeComponent();

            Thread.Sleep(2000);


            strCurrentDirectory = System.Environment.CurrentDirectory + "\\SetUp.ini";
            Win32API.GetPrivateProfileString("Info", "Station", "", tempStringBuilder, 256, strCurrentDirectory);
            labStation.Text = tempStringBuilder.ToString();

            DataBaseInit();
            
            //DXTypeIn a = new DXTypeIn();
            //newForm.ShowDialog();
            //#region
            //DXTypeIn typeInForm = new DXTypeIn()
            //{
            //    Dock = DockStyle.Fill,
            //    FormBorderStyle = FormBorderStyle.None,
            //    TopLevel = false,
            //    Visible = true

            //};
            //xtraTabPage1.Controls.Add(typeInForm);
            //xtraTabPage1.Text = typeInForm.Text;
            //xtraTabControl1.TabPages.Add(xtraTabPage1);



            //DXPackCheck packCheckForm = new DXPackCheck()
            //{

            //    Visible = true,
            //    Dock = DockStyle.Fill,
            //    FormBorderStyle = FormBorderStyle.None,
            //    TopLevel = false
            //};
            //xtraTabPage2.Controls.Add(packCheckForm);
            //xtraTabPage2.Text = packCheckForm.Text;
            //xtraTabControl1.TabPages.Add(xtraTabPage2);

            //#endregion

            //xtraTabControl1.CloseButtonClick += xtraTabControl1_CloseButtonClick;
        }
        private void DataBaseInit()
        {
            //Win32API.GetPrivateProfileString("DataBase", "Server", "", tempStringBuilder, 256, strCurrentDirectory);
            //if (tempStringBuilder.ToString() == "")
            //{
            //    Win32API.WritePrivateProfileString("DataBase", "Server", "localhost", strCurrentDirectory);
            //}
            //else
            //{
            //    strServer = tempStringBuilder.ToString();
            //}

            //Win32API.GetPrivateProfileString("DataBase", "User", "", tempStringBuilder, 256, strCurrentDirectory);
            //if (tempStringBuilder.ToString() == "")
            //{
            //    Win32API.WritePrivateProfileString("DataBase", "User", "ailink", strCurrentDirectory);
            //}
            //else
            //{
            //    strUser = tempStringBuilder.ToString();
            //}

            //Win32API.GetPrivateProfileString("DataBase", "Password", "", tempStringBuilder, 256, strCurrentDirectory);
            //if (tempStringBuilder.ToString() == "")
            //{
            //    Win32API.WritePrivateProfileString("DataBase", "Password", "ailink2017", strCurrentDirectory);
            //}
            //else
            //{
            //    strPassword = tempStringBuilder.ToString();
            //}

            //Win32API.GetPrivateProfileString("DataBase", "DatabaseName", "", tempStringBuilder, 256, strCurrentDirectory);
            //if (tempStringBuilder.ToString() == "")
            //{
            //    Win32API.WritePrivateProfileString("DataBase", "DatabaseName", "hikvision_customization", strCurrentDirectory);
            //}
            //else
            //{
            //    strDatabaseName = tempStringBuilder.ToString();
            //}
            //string strConn = "server={0};user={1};password={2};database={3};charset=utf8;";
            //strConn = string.Format(strConn, strServer, strUser, strPassword, strDatabaseName);

            //ClassMySqlHelp.strConn = strConn;//"server=localhost;user=ailink;password=ailink2017;database=hikvision_customization;charset=utf8;"
            try
            {
                ClassMySqlHelp mySql = new ClassMySqlHelp();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                Environment.Exit(0);
            }

        }

        private void xtraTabControl1_CloseButtonClick_1(object sender, EventArgs e)
        {
            //ClosePageButtonEventArgs a = (ClosePageButtonEventArgs)e;
            //string tabpagename = a.Page.Text;
            //foreach (Control xtp in xtraTabControl1.TabPages)
            //{
            //    if (xtp.Text == tabpagename)
            //    {
            //        xtp.Dispose();
            //        return;
            //    }
            //}

            creatForm.RemoveTabPage(xtraTabControl1, e);

            //#region
            //DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs a = (DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs)e;
            //string tabpagename = a.Page.Text;
            //foreach (XtraTabPage xtp in xtraTabControl1.TabPages)
            //{
            //    //if (xtp.ShowCloseButton.Equals(DevExpress.Utils.DefaultBoolean.True))
            //    //{
            //    if (xtp.Text == tabpagename)
            //    {
            //        DevExpress.XtraEditors.XtraForm form = xtp.Controls[0] as DevExpress.XtraEditors.XtraForm;
            //        form.Close();
            //        form.Dispose();
            //        xtraTabControl1.TabPages.Remove((a.Page as XtraTabPage));
            //        xtp.Dispose();
            //        return;
            //    }
            //    //}
            //}
            //#endregion
        }

        private void btnPlanIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                string planInFormName = typeof(PlanIn).ToString();
                creatForm.AddTabpage(xtraTabControl1, "TabPagePlanIn", "订单导入", planInFormName);
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
                
            }
        }

        private void btnPack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string packFormName = typeof(PackCheck).ToString();
                creatForm.AddTabpage(xtraTabControl1, "TabPagePack", "包装检查", packFormName);
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btnBox_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void navBarItemStartFind_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //显示等待进度条；
            //splashScreenManager1.ShowWaitForm();
            //从MySql中读取数据显示from MySqldataBase to show
            ClassMySqlHelp mySql = new ClassMySqlHelp();

            DataTable dt = mySql.GetSnTab();
            Excel.dataTable = dt;
            //Excel excel = new Excel(dt);
            //excel.ShowDialog();


            try
            {
                string excelFormName = typeof(Excel).ToString();
                creatForm.AddTabpage(xtraTabControl1, "TabPageSNExcel", "录入查询", excelFormName);
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void navBarItemFindLog_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //显示等待进度条；
            //splashScreenManager1.ShowWaitForm();
            //从MySql中读取数据显示from MySqldataBase to show
            ClassMySqlHelp mySql = new ClassMySqlHelp();

            DataTable dt = mySql.GetLogTab();
            Excel.dataTable = dt;
            //Excel excel = new Excel(dt);
            //excel.ShowDialog();

            try
            {
                string excelFormName = typeof(Excel).ToString();
                creatForm.AddTabpage(xtraTabControl1, "TabPageLOGExcel", "LOG查询", excelFormName);
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }
        }



        private void btnStaSet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetStation stationSet = new SetStation(updateLabStation);

            stationSet.ShowDialog();
        }
        private void updateLabStation()
        {
            Win32API.GetPrivateProfileString("Info", "Station", "", tempStringBuilder, 256, strCurrentDirectory);
            labStation.Text = tempStringBuilder.ToString();
            //labStation.Update();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //try
            //{
            //    string excelFormName = typeof(Excel).ToString();
            //    creatForm.AddTabpage(xtraTabControl1, "TabPageExcel", "数据查询", "Excel");
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClassMySqlHelp mySql = new ClassMySqlHelp();
            try
            {
                mySql.CloseConn();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }


    }
}
