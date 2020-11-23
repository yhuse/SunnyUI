using System.Drawing;

namespace Sunny.UI.Demo
{
    public partial class FBarChartEx : UITitlePage
    {
        public FBarChartEx()
        {
            InitializeComponent();
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

        private void uiSymbolButton1_Click(object sender, System.EventArgs e)
        {
            UIBarOption option = new UIBarOption();

            option.Grid.Bottom = 60;

            option.Title = new UITitle();
            option.Title.Text = "SunnyUI";
            option.Title.SubText = "BarChartEx";

            //设置Legend
            option.Legend = new UILegend();
            option.Legend.Orient = UIOrient.Horizontal;
            option.Legend.Top = UITopAlignment.Top;
            option.Legend.Left = UILeftAlignment.Left;
            option.Legend.AddData("Bar1");
            option.Legend.AddData("Bar2");
            option.Legend.AddData("Bar3");

            var series = new UIBarSeries();
            series.Name = "Bar1";
            series.ShowBarName = true;
            series.ShowValue = true;
            series.AddData("通道1", 1.1);
            series.AddData("通道2", 1.5);
            option.Series.Add(series);

            series = new UIBarSeries();
            series.Name = "Bar2";
            series.ShowBarName = true;
            series.ShowValue = true;
            series.AddData("通道1", 2.2);
            series.AddData("通道2", 2.8);
            series.AddData("通道3", 2.5);
            option.Series.Add(series);

            series = new UIBarSeries();
            series.Name = "Bar3";
            series.ShowBarName = true;
            series.ShowValue = true;
            series.AddData("通道1", 0.7);
            option.Series.Add(series);

            option.XAxis.Data.Add("Mon");
            option.XAxis.Data.Add("Tue");
            option.XAxis.Data.Add("Wed");
            option.XAxis.Data.Add("Thu");
            option.XAxis.Data.Add("Fri");

            option.ToolTip.Visible = true;
            option.YAxis.Scale = true;

            option.XAxis.Name = "日期";
            option.YAxis.Name = "数值";

            option.XAxis.AxisTick.Distance = 14;

            option.FixedSeriesCount = 3;
            option.AutoSizeBars = true;
            option.AutoSizeBarsCompact = true;
            option.AutoSizeBarsCompactValue = 0.8f;

            option.YAxisScaleLines.Add(new UIScaleLine() { Color = Color.Red, Name = "上限", Value = 12 });
            option.YAxisScaleLines.Add(new UIScaleLine() { Color = Color.Gold, Name = "下限", Value = -20 });

            option.ToolTip.AxisPointer.Type = UIAxisPointerType.Shadow;
            BarChart.SetOption(option);
        }
    }
}
