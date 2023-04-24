using System;
using System.Drawing;
using System.Text;

namespace Sunny.UI.Demo
{
    public partial class FLineChart : UIPage
    {
        public FLineChart()
        {
            InitializeComponent();
        }

        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            timer1.Stop();

            DateTime dt = new DateTime(2020, 10, 4);

            UILineOption option = new UILineOption();
            option.ToolTip.Visible = true;
            option.Title = new UITitle();
            option.Title.Text = "SunnyUI";
            option.Title.SubText = "LineChart";

            option.XAxisType = UIAxisType.DateTime;

            var series = option.AddSeries(new UILineSeries("Line1"));
            series.Add(dt.AddHours(0), 1.2);
            series.Add(dt.AddHours(1), 2.2);
            series.Add(dt.AddHours(2), 3.2);
            series.Add(dt.AddHours(3), cbContainsNan.Checked ? double.NaN : 4.2);
            series.Add(dt.AddHours(4), 3.2);
            series.Add(dt.AddHours(5), 2.2);
            series.Symbol = UILinePointSymbol.Square;
            series.SymbolSize = 4;
            series.SymbolLineWidth = 2;
            series.SymbolColor = Color.Red;
            series.ShowLine = !cbPoints.Checked;
            //数据点显示小数位数
            series.YAxisDecimalPlaces = 2;

            series = option.AddSeries(new UILineSeries("Line2", Color.Lime));
            series.Add(dt.AddHours(3), 3.3);
            series.Add(dt.AddHours(4), 2.3);
            series.Add(dt.AddHours(5), 2.3);
            series.Add(dt.AddHours(6), 1.3);
            series.Add(dt.AddHours(7), 2.3);
            series.Add(dt.AddHours(8), 4.3);
            series.Symbol = UILinePointSymbol.Star;
            series.SymbolSize = 4;
            series.SymbolLineWidth = 2;
            series.ShowLine = !cbPoints.Checked;
            //数据点显示小数位数
            series.YAxisDecimalPlaces = 1;
            series.Smooth = true;

            option.GreaterWarningArea = new UILineWarningArea(3.5);
            option.LessWarningArea = new UILineWarningArea(2.2, Color.Gold);

            option.YAxisScaleLines.Add(new UIScaleLine("上限", 3.5, Color.Red));
            option.YAxisScaleLines.Add(new UIScaleLine("下限", 2.2, Color.Gold));

            option.XAxis.Name = "日期";
            option.YAxis.Name = "数值";
            //X轴坐标轴显示格式化
            option.XAxis.AxisLabel.DateTimeFormat = "yyyy-MM-dd HH:mm";

            //Y轴坐标轴显示小数位数
            option.YAxis.AxisLabel.DecimalPlaces = 1;

            option.XAxisScaleLines.Add(new UIScaleLine(dt.AddHours(3).DateTimeString(), dt.AddHours(3), Color.Red));
            option.XAxisScaleLines.Add(new UIScaleLine(dt.AddHours(6).DateTimeString(), dt.AddHours(6), Color.Red));

            //设置X轴显示范围
            option.XAxis.SetRange(dt, dt.AddHours(8));
            LineChart.SetOption(option);
        }

        private void uiImageButton1_Click(object sender, EventArgs e)
        {
            LineChart.ChartStyleType = UIChartStyleType.Default;
        }

        private void uiImageButton2_Click(object sender, EventArgs e)
        {
            LineChart.ChartStyleType = UIChartStyleType.Plain;
        }

        private void uiImageButton3_Click(object sender, EventArgs e)
        {
            LineChart.ChartStyleType = UIChartStyleType.Dark;
        }

        private void LineChart_PointValue(object sender, UILineSelectPoint[] points)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var point in points)
            {
                sb.Append(point.Series.Name + ", " + point.Index + ", " + point.X + ", " + point.Y);
            }

            Console.WriteLine(sb.ToString());
        }

        private void uiSymbolButton2_Click(object sender, EventArgs e)
        {
            index = 0;
            UILineOption option = new UILineOption();
            option.ToolTip.Visible = true;
            option.Title = new UITitle();
            option.Title.Text = "SunnyUI";
            option.Title.SubText = "LineChart";
            var series = option.AddSeries(new UILineSeries("Line1"));

            //设置曲线显示最大点数，超过后自动清理
            series.SetMaxCount(50);

            //坐标轴显示小数位数
            option.XAxis.AxisLabel.DecimalPlaces = 1;
            option.YAxis.AxisLabel.DecimalPlaces = 1;
            LineChart.SetOption(option);
            timer1.Start();
        }

        int index = 0;
        Random random = new Random();

        private void timer1_Tick(object sender, EventArgs e)
        {
            LineChart.Option.AddData("Line1", index, random.NextDouble() * 10);
            index++;

            if (index > 50)
            {
                LineChart.Option.XAxis.SetRange(index - 50, index + 20);
            }

            LineChart.Refresh();
        }

        private void uiCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            uiSymbolButton1.PerformClick();
        }

        private void uiSymbolButton3_Click(object sender, EventArgs e)
        {
            timer1.Stop();

            UILineOption option = new UILineOption();
            option.ToolTip.Visible = true;
            option.Title = new UITitle();
            option.Title.Text = "SunnyUI";
            option.Title.SubText = "LineChart";

            var series = option.AddSeries(new UILineSeries("Line1"));
            series.Add(0, 1.2);
            series.Add(1, 2.2);
            series.Add(2, double.NaN);
            series.Add(3, 4.2);
            series.Add(4, 3.2);
            series.Add(5, 2.2);
            series.Symbol = UILinePointSymbol.Square;
            series.SymbolSize = 4;
            series.SymbolLineWidth = 2;
            series.SymbolColor = Color.Red;

            option.XAxis.Name = "日期";
            option.YAxis.Name = "数值";
            option.XAxis.AxisLabel.DateTimeFormat = "yyyy-MM-dd HH:mm";
            option.YAxis.AxisLabel.DecimalPlaces = 1;

            LineChart.SetOption(option);
        }

        private void uiCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            uiSymbolButton1.PerformClick();
        }

        private void uiSymbolButton3_Click_1(object sender, EventArgs e)
        {
            timer1.Stop();

            UILineOption option = new UILineOption();
            option.ToolTip.Visible = true;
            option.Title = new UITitle();
            option.Title.Text = "SunnyUI";
            option.Title.SubText = "LineChart";

            var series = option.AddSeries(new UILineSeries("Line1"));
            series.Add(0, 1.2);
            series.Add(1, 2.2);
            series.Add(2, 3.2);
            series.Add(3, 4.2);
            series.Add(4, 3.2);
            series.Add(5, 2.2);
            series.Symbol = UILinePointSymbol.Square;
            series.SymbolSize = 4;
            series.SymbolLineWidth = 2;
            series.SymbolColor = Color.Red;

            series = option.AddSeries(new UILineSeries("Line2", Color.Lime, true));
            series.Add(3, 13.3);
            series.Add(4, 12.3);
            series.Add(5, 12.3);
            series.Add(6, 11.3);
            series.Add(7, 12.3);
            series.Add(8, 14.3);
            series.Symbol = UILinePointSymbol.Star;
            series.SymbolSize = 4;
            series.SymbolLineWidth = 2;

            option.XAxis.Name = "日期";
            option.YAxis.Name = "数值";
            option.Y2Axis.Name = "数值";

            option.YAxisScaleLines.Add(new UIScaleLine() { Color = Color.Red, Name = "上限", Value = 3.5 });
            option.Y2AxisScaleLines.Add(new UIScaleLine() { Color = Color.Gold, Name = "下限", Value = 12, DashDot = true });

            option.XAxisScaleLines.Add(new UIScaleLine() { Color = Color.Lime, Name = "3", Value = 3 });
            option.XAxisScaleLines.Add(new UIScaleLine() { Color = Color.Gold, Name = "6", Value = 6 });

            //设置坐标轴为自定义标签
            option.XAxis.CustomLabels = new CustomLabels(1, 1, 11);
            for (int i = 1; i <= 12; i++)
            {
                option.XAxis.CustomLabels.AddLabel(i + "月");
            }

            LineChart.SetOption(option);
        }
    }
}
