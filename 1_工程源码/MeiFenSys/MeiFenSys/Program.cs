using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using System.Threading;
using DevExpress.XtraEditors;
namespace MeiFenSys
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //BonusSkins.Register();
            //SkinManager.EnableFormSkins();
            ////System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
            //Application.Run(new Form1());

            #region 方法一:使用互斥量
            bool createNew;

            //  createdNew:
            // 在此方法返回时，如果创建了局部互斥体（即，如果 name 为 null 或空字符串）或指定的命名系统互斥体，则包含布尔值 true；
            // 如果指定的命名系统互斥体已存在，则为false
            using (Mutex mutex = new Mutex(true, Application.ProductName, out createNew))
            {
                if (createNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    BonusSkins.Register();
                    SkinManager.EnableFormSkins();
                    Application.Run(new Form1());
                }
                // 程序已经运行的情况，则弹出消息提示并终止此次运行
                else
                {
                    XtraMessageBox.Show("主应用程序已经在运行中...");
                    System.Threading.Thread.Sleep(1000);

                    //  终止此进程并为基础操作系统提供指定的退出代码。
                    System.Environment.Exit(1);
                }
            }

            #endregion

        }
    }
}
