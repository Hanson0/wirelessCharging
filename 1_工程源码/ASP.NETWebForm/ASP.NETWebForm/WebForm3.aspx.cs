using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP.NETWebForm
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink link = new HyperLink();
            link.NavigateUrl = "~/WebForm2.aspx";//"http://www.youku.com";
            link.Text = "优酷";
            link.ToolTip = "进入优酷网";
            link.CssClass = "a2";
            Panel1.Controls.Add(link);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            //TextBox1.Text = "";
            //foreach (ListItem item in ListBox1.Items)//foreach不能用在改变的遍历
            //{
            //    if (item.Selected)
            //    {
            //        //ListBox1.Items.Remove(item);
            //        TextBox1.Text += item.Value + "\r\n";
            //    }
            //}
            //正向遍历删除
            //for (int i = 0; i < ListBox1.Items.Count; i++)
            //{
            //    if (ListBox1.Items[i].Selected)
            //    {
            //        ListBox1.Items.Remove(ListBox1.Items[i]);//删除后list的结构已经改变，删出i=2的，那么原来的3就是现在的2——所以要用反向遍历
            //        TextBox1.Text += ListBox1.Items[i].Text + "\r\n";
            //    }
            //}
            //反向遍历删除
            for (int i = ListBox1.Items.Count-1; i >=0; i--)
            {
                if (ListBox1.Items[i].Selected)
                {
                    //
                    TextBox1.Text += ListBox1.Items[i].Text + "\r\n";
                    //移动到新list
                    ListBox2.Items.Add(ListBox1.Items[i]);
                    ListBox1.Items.Remove(ListBox1.Items[i]);//删除后list的结构已经改变，删出i=2的，那么原来的3就是现在的2——所以要用反向遍历

                }
                
            }


        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //反向遍历删除
            for (int i = ListBox2.Items.Count - 1; i >= 0; i--)
            {
                if (ListBox2.Items[i].Selected)
                {
                    //
                    TextBox1.Text += ListBox2.Items[i].Text + "\r\n";
                    //移动到新list
                    ListBox1.Items.Add(ListBox2.Items[i]);
                    ListBox2.Items.Remove(ListBox2.Items[i]);//删除后list的结构已经改变，删出i=2的，那么原来的3就是现在的2——所以要用反向遍历

                }

            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            HyperLink hyper = new HyperLink();
            hyper.NavigateUrl = "http://www.360.com";
            hyper.Text = "360网站导航大全";
            Panel1.Controls.Add(hyper);

            HyperLink hyper2 = new HyperLink();
            hyper2.NavigateUrl = "http://www.youku.com";
            hyper2.Text = "优酷";
            Panel1.Controls.Add(hyper);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            HyperLink link = new HyperLink();
            link.NavigateUrl = "~/WebForm2.aspx";//"http://www.youku.com";
            link.Text = "优酷";
            link.ToolTip = "进入优酷网";
            link.CssClass = "a2";
            Panel1.Controls.Add(link);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            RectangleHotSpot hs = new RectangleHotSpot();


            hs.Left = 0; hs.Right = 200; hs.Top = 300; hs.Bottom = 400;
            hs.NavigateUrl = "http://www.taobao.com";
            hs.AlternateText = "进入淘宝网";
            //ImageMap1.im
            ImageMap1.HotSpots.Add(hs);
        }
    }
}