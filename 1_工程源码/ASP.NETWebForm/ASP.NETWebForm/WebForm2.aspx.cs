using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP.NETWebForm
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            foreach (Control control in chkslist.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox check = (CheckBox)control;
                    if (check.Checked)
                    {
                        TextBox1.Text += check.Text+"，";
                    }
                }
            }

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            TextBox2.Text = (CheckBox1.Checked) ? "选中" : "未选中";
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            BulletedList1.CssClass = "style1";
            BulletedList1.DisplayMode = BulletedListDisplayMode.Text;

        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            BulletedList1.CssClass = "style2";
            BulletedList1.DisplayMode = BulletedListDisplayMode.Text;

        }

        protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            BulletedList1.CssClass = "style3";
            BulletedList1.DisplayMode = BulletedListDisplayMode.HyperLink;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取选中项 
            TextBox3.Text = DropDownList1.SelectedValue;//Value
            //TextBox3.Text = DropDownList1.SelectedItem.Text;//Text
            //TextBox3.Text = DropDownList1.SelectedIndex.ToString();//索引
            //TextBox3.Text = DropDownList1.Items[DropDownList1.SelectedIndex].Text;//Text


            //获取其他项
            //TextBox3.Text = DropDownList1.Items[0].Text;
            //TextBox3.Text = DropDownList1.Items[0].Value;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DropDownList1.SelectedItem.Selected = false;

            DropDownList1.Items[2].Selected = true;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            DropDownList1.Items.Clear();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect(DropDownList1.SelectedValue);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            DropDownList1.Items[0].Text = "京东";
            DropDownList1.Items[0].Value = "http://www.jingdong.com";
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            ListItem newListItem = new ListItem();
            newListItem.Text = "新浪";
            newListItem.Value = "http://www.sina.com";
            DropDownList1.Items.Add(newListItem);
        }

    }
}