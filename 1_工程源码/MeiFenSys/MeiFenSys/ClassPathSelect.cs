using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeiFenSys
{
    class ClassPathSelect
    {
        // 选择文件：
        public string SelectPath(int model)
        {
            string path = string.Empty;

            if (model==1)//文件路径
            {
                var openFileDialog = new System.Windows.Forms.OpenFileDialog()
                {
                    //Files (*.*)|
                    Filter = "(*.xlsx)|*.xlsx|(*.xls)|*.xls|(*.*)|*.*"//如果需要筛选txt文件（"Files (*.txt)|*.txt"）
                };
                var result = openFileDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    path = openFileDialog.FileName;
                }
            }
            else if (model==2)//文件夹路径
            {
                System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    path = fbd.SelectedPath;
                }
            }
            else
            {
                path=null;
            }
            return path;
        }
    }
}
