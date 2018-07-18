using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP.NETWebForm
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label3.Text = TextBox1.Text + "今年"+TextBox2.Text+"岁";
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            Label3.Text += "。";
            
            //if (TextBox1.Text == "123" && TextBox2.Text == "123")
            //{
            //    Label3.Text = "登录成功";
            //}
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (rbtn1.Checked)
            {
                TextBox3.Text = "你选择了："+rbtn1.Text;
            }
            if (rbtn2.Checked)
            {
                TextBox3.Text = "你选择了：" + rbtn2.Text;
            }
            if (rbtn3.Checked)
            {
                TextBox3.Text = "你选择了：" + rbtn3.Text;
            }
            if (rbtn4.Checked)
            {
                TextBox3.Text = "你选择了：" + rbtn4.Text;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            rbtn5.Checked = rbtn6.Checked =rbtn7.Checked =rbtn8.Checked =false;
            double dbGrade;
            try
            {
                 dbGrade = double.Parse(TextBox4.Text);
            }
            catch (Exception ex)
            {

                TextBox4.Text = ex.Message;
                return;
            }
            if (dbGrade<0|| dbGrade>100)
            {
                TextBox4.Text = "数值超出范围";
                return;
            }

            TextBox4.Text = "";
            if (dbGrade<60)
            {
                rbtn5.Checked = true;
                return;
            }
            if (dbGrade < 70)
            {
                rbtn6.Checked = true;
                return;
            }
            if (dbGrade < 90)
            {
                rbtn7.Checked = true;
                return;
            }
            if (dbGrade <= 100)
            {
                rbtn8.Checked = true;
                return;
            }

        
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //TextBox5.Text += ".";
            TextBox5.Text = (CheckBox1.Checked) ? "选中" : "未选中";
        }
    }
}