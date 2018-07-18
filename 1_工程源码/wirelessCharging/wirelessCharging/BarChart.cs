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
using DevExpress.XtraCharts;

namespace wirelessCharging
{
    public partial class BarChart : DevExpress.XtraEditors.XtraForm
    {
        public BarChart()
        {
            InitializeComponent();
        }

        private void createLineBaseItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region 创建折线图
            //ChartControl lineChart = new ChartControl();

            //Series series1 = new Series("Series 1", ViewType.Line);

            //series1.Points.Add(new SeriesPoint(1, 2));
            //series1.Points.Add(new SeriesPoint(2, 12));
            //series1.Points.Add(new SeriesPoint(3, 14));
            //series1.Points.Add(new SeriesPoint(4, 17));

            //lineChart.Series.Add(series1);

            //series1.ArgumentScaleType = ScaleType.Numerical;

            //XYDiagram diagram = (XYDiagram)lineChart.Diagram;
            //diagram.AxisX.Title.Visible = true;
            //diagram.AxisX.Title.Alignment = StringAlignment.Center;
            //diagram.AxisX.Title.Text = "横轴标题";
            //diagram.AxisX.Title.Antialiasing = true;
            //diagram.AxisX.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);

            //diagram.AxisY.Title.Visible = true;
            //diagram.AxisY.Title.Alignment = StringAlignment.Center;
            //diagram.AxisY.Title.Text = "纵轴标题";
            //diagram.AxisY.Title.Antialiasing = true;
            //diagram.AxisY.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);

            //((XYDiagram)lineChart.Diagram).EnableAxisXZooming = true;

            //lineChart.Legend.Visible = false;

            //lineChart.Titles.Add(new ChartTitle());
            //lineChart.Titles[0].Text = "主标题";


            //lineChart.Dock = DockStyle.Fill;
            //this.panel1.Controls.Add(lineChart);
            #endregion
        }
    }
}