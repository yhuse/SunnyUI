using System.Drawing;

namespace Sunny.UI.Demo.Charts
{
    public partial class FBarChart : UITitlePage
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
            series.AddData(1.1);
            series.AddData(1.1);
            series.AddData(1.1);
            series.AddData(1.1);
            series.AddData(1.1);
            option.Series.Add(series);

            series = new UIBarSeries();
            series.Name = "Bar2";
            series.AddData(1.1);
            series.AddData(1.1);
            series.AddData(1.1);
            series.AddData(1.1);
            series.AddData(1.1);
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

            option.YAxisScaleLines.Add(new UIScaleLine() { Color = Color.Red, Name = "上限", Value = 12 });
            option.YAxisScaleLines.Add(new UIScaleLine() { Color = Color.Gold, Name = "下限", Value = -20 });

            BarChart.SetOption(option);
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
    }
}