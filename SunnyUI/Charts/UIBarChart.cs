using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Sunny.UI.Charts;

namespace Sunny.UI
{
    [ToolboxItem(false)]
    public class UIBarChart : UIChart
    {
        protected override void CalcData(UIOption option)
        {
            Bars.Clear();
            UIBarOption o = (UIBarOption)option;
            if (o == null || o.Series == null || o.Series.Count == 0) return;

            DrawOrigin = new Point(PieOption.Grid.Left,Height - PieOption.Grid.Bottom);
            DrawSize = new Size(Width- PieOption.Grid.Left- PieOption.Grid.Right,
                Height -PieOption.Grid.Top-PieOption.Grid.Bottom);

            if (DrawSize.Width<=0 || DrawSize.Height<=0) return;
            if (o.XAxis.Data.Count==0) return;

            DrawBarWidth = DrawSize.Width / o.XAxis.Data.Count;
        }

        private Point DrawOrigin;
        private Size DrawSize;
        private int DrawBarWidth;
        private readonly ConcurrentDictionary<int, BarInfo> Bars = new ConcurrentDictionary<int, BarInfo>();

        [Browsable(false)]
        private UIBarOption PieOption
        {
            get
            {
                UIOption option = Option ?? EmptyOption;
                UIBarOption o = (UIBarOption)option;
                return o;
            }
        }

        protected override void CreateEmptyOption()
        {
            if (emptyOption != null) return;

            UIBarOption option = new UIBarOption();
            option.Title = new UITitle();
            option.Title = new UITitle();
            option.Title.Text = "SunnyUI";
            option.Title.SubText = "BarChart";
            
            var series = new UIBarSeries();
            series.Name = "柱状图";
            series.AddData(1);
            series.AddData(5);
            series.AddData(2);
            series.AddData(4);
            series.AddData(3);

            option.XAxis.Data.Add("Mon");
            option.XAxis.Data.Add("Tue");
            option.XAxis.Data.Add("Wed");
            option.XAxis.Data.Add("Thu");
            option.XAxis.Data.Add("Fri");

            option.Series.Add(series);
            emptyOption = option;
        }

        private void DrawTitle(Graphics g, UITitle title)
        {
        }

        private void DrawSeries(Graphics g, UIPieOption o, List<UIPieSeries> series)
        {
        }

        private void DrawLegend(Graphics g, UIPieLegend legend)
        {
        }

        internal class BarInfo
        {
            public Rectangle Rect { get; set; }
        }
    }
}