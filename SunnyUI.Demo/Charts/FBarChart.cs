using System;
using System.Drawing;

namespace Sunny.UI.Demo
{
    public partial class FBarChart : UIPage
    {
        public FBarChart()
        {
            InitializeComponent();
        }

        private void uiSymbolButton1_Click(object sender, System.EventArgs e)
        {
            UIBarOption option = new UIBarOption();
            option.Title = new UITitle();
            option.Title.Text = "SunnyUI";
            option.Title.SubText = "BarChart";

            //设置Legend
            option.Legend = new UILegend();
            option.Legend.Orient = UIOrient.Horizontal;
            option.Legend.Top = UITopAlignment.Top;
            option.Legend.Left = UILeftAlignment.Left;
            option.Legend.AddData("Bar1");
            option.Legend.AddData("Bar2");

            var series = new UIBarSeries();
            series.Name = "Bar1";
            series.AddData(-1.1);
            series.AddData(-1.1);
            series.AddData(-1.1);
            series.AddData(-1.1);
            series.AddData(-1.1);
            series.AddData(-1.1);
            series.AddData(-1.1);
            series.AddData(-1.1);

            //数据显示小数位数
            series.DecimalPlaces = 1;
            option.Series.Add(series);

            series = new UIBarSeries();
            series.Name = "Bar2";
            series.AddData(-1.1);
            series.AddData(-1.1);
            series.AddData(-1.1);
            series.AddData(-1.1);
            series.AddData(-1.1);
            series.AddData(-1.1);
            series.AddData(-1.1);
            series.AddData(-1.1);
            option.Series.Add(series);

            option.XAxis.Data.Add("D1");
            option.XAxis.Data.Add("D2");
            option.XAxis.Data.Add("D223");
            option.XAxis.Data.Add("D4");
            option.XAxis.Data.Add("D5");
            option.XAxis.Data.Add("D6");
            option.XAxis.Data.Add("D7333");
            option.XAxis.Data.Add("D8");

            option.ToolTip.Visible = true;
            option.YAxis.Scale = true;

            option.XAxis.Name = "日期";
            option.XAxis.AxisLabel.Angle = 60;//(0° ~ 90°)

            option.YAxis.Name = "数值";

            //坐标轴显示小数位数
            option.YAxis.AxisLabel.DecimalPlaces = 1;

            option.YAxisScaleLines.Add(new UIScaleLine() { Color = Color.Red, Name = "上限", Value = 12 });
            option.YAxisScaleLines.Add(new UIScaleLine() { Color = Color.Gold, Name = "下限", Value = -20 });

            option.ToolTip.AxisPointer.Type = UIAxisPointerType.Shadow;

            option.ShowValue = true;

            BarChart.SetOption(option);

            uiSymbolButton2.Enabled = true;
        }

        private void uiImageButton1_Click(object sender, System.EventArgs e)
        {
            BarChart.ChartStyleType = UIChartStyleType.Default;
        }

        private void uiImageButton2_Click(object sender, System.EventArgs e)
        {
            BarChart.ChartStyleType = UIChartStyleType.Plain;
        }

        private void uiImageButton3_Click(object sender, System.EventArgs e)
        {
            BarChart.ChartStyleType = UIChartStyleType.Dark;
        }

        private void uiSymbolButton2_Click(object sender, System.EventArgs e)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            BarChart.Update("Bar1", 0, random.Next(10));
            BarChart.Update("Bar1", 1, random.Next(10));
            BarChart.Update("Bar1", 2, random.Next(10));
            BarChart.Update("Bar1", 3, random.Next(10));
            BarChart.Update("Bar1", 4, random.Next(10));
            BarChart.Update("Bar1", 5, random.Next(10));
            BarChart.Update("Bar1", 6, random.Next(10));
            BarChart.Update("Bar1", 7, random.Next(10));

            BarChart.Update("Bar2", 0, random.Next(10));
            BarChart.Update("Bar2", 1, random.Next(10));
            BarChart.Update("Bar2", 2, random.Next(10));
            BarChart.Update("Bar2", 3, random.Next(10));
            BarChart.Update("Bar2", 4, random.Next(10));
            BarChart.Update("Bar2", 5, random.Next(10));
            BarChart.Update("Bar2", 6, random.Next(10));
            BarChart.Update("Bar2", 7, random.Next(10));

            BarChart.Refresh();
        }
    }
}
