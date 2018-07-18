using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace floatConvertToHex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region
            float f = Convert.ToSingle(textBoxX1.Text);
            //float f = 2.3f;
            //Convert.ToByte(f, 16);
            byte[] b = BitConverter.GetBytes(f);
            Array.Reverse(b);
            foreach (var item in b)
            {
                textBoxX2.Text += item.ToString();
            }
            #endregion
            //调用CRC16 得到的CRC字节数组是 [0]高8  [1]低8 ={0x23,0xFC} ，转字符串时反转了的,所以 才有了规范的 低8在前高8在后 "FC23"
            //textBoxX2.Text = CRC.ToModbusCRC16(textBoxX1.Text, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] bytes= new byte[4]{0x41,0x20,0x00,0x2A};
            Array.Reverse(bytes);

            textBoxX3.Text = BitConverter.ToSingle(bytes, 0).ToString();

        }
    }
}
