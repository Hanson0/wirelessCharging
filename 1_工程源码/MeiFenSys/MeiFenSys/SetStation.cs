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

namespace MeiFenSys
{
    public partial class SetStation : DevExpress.XtraEditors.XtraForm
    {
        private const string tip = "*必填";

        StringBuilder tempStringBuilder = new StringBuilder();      //全局可变字符串实例，用于读取配置文件内容
        private string strCurrentDirectory;                         //应用程序路径
        Handled1 _hand = null;
        public SetStation(Handled1 hand)
        {
            InitializeComponent();
            InitGlobleVariable();
            _hand = hand;
        }

        public void InitGlobleVariable()
        {
            strCurrentDirectory = System.Environment.CurrentDirectory + "\\SetUp.ini";
            //Win32API.GetPrivateProfileString("Info", "SnData", "", tempStringBuilder, 256, strCurrentDirectory);
            //if (tempStringBuilder.ToString() == "")
            //{
            //    Win32API.WritePrivateProfileString("Info", "SnData", "1804", strCurrentDirectory);
            //}
            //else
            //{
            //    textSnFormat.Text = tempStringBuilder.ToString();
            //}



        }

        private void btnSureSet_Click(object sender, EventArgs e)
        {

            bool textStationIsNull = string.IsNullOrEmpty(textStation.Text);
            if (textStationIsNull)
            {
                labPassTip.Text = "";
                labStationErrorTip.Text = tip;
                return;
            }
            labStationErrorTip.Text = "";
            try
            {
                Win32API.WritePrivateProfileString("Info", "Station", textStation.Text, strCurrentDirectory);
            }
            catch (Exception ex)
            {
                labPassTip.Text = "";
                XtraMessageBox.Show(ex.Message);
                return;
            }
            labPassTip.Text = "设置成功";
            _hand();

        }
    }
}