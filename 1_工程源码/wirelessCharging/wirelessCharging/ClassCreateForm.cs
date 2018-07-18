using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraTab;

namespace JiaHao.DX.FormBuild
{
    class CreateForm
    {
        private static Dictionary<string, XtraTabPage> TabPageDic = new Dictionary<string, XtraTabPage>();

        private static CreateForm form = null;
        public CreateForm() { }

        static CreateForm()
        {
            form = new CreateForm();
        }
        public static CreateForm FormOper
        {
            get
            {
                return form;
            }
        }

        /// <summary>
        /// 新增选项卡页
        /// </summary>
        /// <param name="tabControl">选项卡控件</param>
        /// <param name="tabPageName">当期选项卡页name名称</param>
        /// <param name="tabText">当前选项卡页Text标题</param>
        /// <param name="newFormName">当前选项卡中的新窗体</param>
        public void AddTabpage(XtraTabControl tabControl, string tabPageName, string tabText, string newFormName)
        {
            try
            {
                if (IsTabpageExsit(tabControl, tabPageName))
                {
                    return;
                }
                XtraTabPage newPage = new XtraTabPage();
                newPage.Name = tabPageName;
                newPage.Text = tabText;
                newPage.Tooltip = tabPageName;
                newPage.Controls.Add(AddNewForm(newFormName));
                tabControl.TabPages.Add(newPage);
                TabPageDic.Add(tabPageName, newPage);
                tabControl.SelectedTabPage = newPage;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        /// <summary>
        /// 移除选项卡页
        /// </summary>
        /// <param name="tabControl"></param>
        /// <param name="tabPageName"></param>
        /// <param name="e"></param>
        public void RemoveTabPage(XtraTabControl tabControl, EventArgs e)
        {
            DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs args = (DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs)e;
            string name = args.Page.Tooltip;
            foreach (XtraTabPage item in tabControl.TabPages)
            {
                if (item.Name == name)
                {
                    tabControl.TabPages.Remove(item);
                    item.Dispose();
                    TabPageDic.Remove(name);

                    return;
                }
            }
        }

        /// <summary>
        /// 判断选项卡是否已经存在
        /// </summary>
        /// <param name="tabControl">选项卡控件</param>
        /// <param name="tabPageName">选项卡名称</param>
        /// <returns></returns>
        private bool IsTabpageExsit(XtraTabControl tabControl, string tabPageName)
        {
            foreach (var item in TabPageDic)
            {
                if (item.Key == tabPageName)
                {
                    tabControl.SelectedTabPage = item.Value;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 在选项卡中生成窗体
        /// </summary>
        /// <param name="form">窗体名称</param>
        private Form AddNewForm(string formName)
        {
            try
            {
                Form newForm = (Form)Assembly.GetExecutingAssembly().CreateInstance(formName);
                newForm.FormBorderStyle = FormBorderStyle.None;
                newForm.TopLevel = false;
                //newForm.Parent = ((XtraTabControl)sender).SelectedTabPage;
                newForm.ControlBox = false;
                newForm.Dock = DockStyle.Fill;
                newForm.Visible = true;
                return newForm;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}


