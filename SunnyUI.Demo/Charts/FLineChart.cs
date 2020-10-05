using System;
using System.Drawing;
using System.Text;

namespace Sunny.UI.Demo.Charts
{
    public partial class FLineChart : UITitlePage
    {
        public FLineChart()
        {
            InitializeComponent();
        }

        private void uiSymbolButton1_Click(object sender, System.EventArgs e)
        {
            UILineOption option = new UILineOption();
            option.Title = new UITitle();
            option.Title.Text = "SunnyUI";
            option.Title.SubText = "LineChart";

            option.XAxisType = UIAxisType.Time;

            var series = option.AddSeries(new UILineSeries("Line1"));
            DateTime dt = new DateTime(2020, 10, 4);
            series.Add(dt.AddHours(0), 1.2);
            series.Add(dt.AddHours(0.1), 2.2);
            series.Add(dt.AddHours(0.2), 3.2);
            series.Add(dt.AddHours(0.3), 4.2);
            series.Add(dt.AddHours(0.4), 3.2);
            series.Add(dt.AddHours(0.5), 2.2);
            series.Symbol = UILinePointSymbol.Square;
            series.SymbolSize = 4;
            series.SymbolLineWidth = 2;
            series.SymbolColor = Color.Red;

            series = option.AddSeries(new UILineSeries("Line2"));
            series.Add(dt.AddHours(0.3), 3.3);
            series.Add(dt.AddHours(0.4), 2.3);
            series.Add(dt.AddHours(0.5), 2.3);
            series.Add(dt.AddHours(0.6), 1.3);
            series.Add(dt.AddHours(0.7), 2.3);
            series.Add(dt.AddHours(0.8), 4.3);
            series.Symbol = UILinePointSymbol.Star;
            series.SymbolSize = 4;
            series.SymbolLineWidth = 2;
            series.SymbolColor = Color.Red;
            series.Smooth = true;

            // option.XAxis.Min = new DateTimeInt64(dt.AddDays(-1));
            // option.XAxis.Max = new DateTimeInt64(dt.AddDays(1));
            // option.XAxis.MaxAuto = false;
            // option.XAxis.MinAuto = false;

            option.XAxis.Name = "数值";
            option.YAxis.Name = "数值";

            LineChart.SetOption(option);
        }

        private void LineChart_PointValue(object sender, System.Collections.Generic.List<UILineSelectPoint> points)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var point in points)
            {
                sb.Append(point.Name + ", " + point.Index + ", " + point.X + ", " + point.Y);
                sb.Append('\n');
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
