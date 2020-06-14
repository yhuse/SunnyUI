using System;
using System.Collections.Generic;

namespace Sunny.UI.Charts
{
    public class UIBarOption : UIOption, IDisposable
    {
        public UITitle Title;

        public UICategoryAxis XAxis { get; set; } = new UICategoryAxis();

        public UIValueAxis YAxis { get; set; } = new UIValueAxis();

        public List<UIBarSeries> Series = new List<UIBarSeries>();

        public UIChartGrid Grid = new UIChartGrid();

        //public UIPieLegend Legend;

        //public UIPieToolTip ToolTip;
        

        public void AddSeries(UIBarSeries series)
        {
            Series.Add(series);
        }

        public void Dispose()
        {
            foreach (var series in Series)
            {
                series?.Dispose();
            }

            Series.Clear();
        }

        public int SeriesCount => Series.Count;
    }

    public class UIAxis
    {
        public string Name { get; set; }

        public UIAxisType Type { get; set; }

        /// <summary>
        /// 坐标轴的分割段数，需要注意的是这个分割段数只是个预估值
        /// 最后实际显示的段数会在这个基础上根据分割后坐标轴刻度显示的易读程度作调整。
        /// 在类目轴中无效。
        /// </summary>
        public int SplitNumber { get; set; } = 5;
    }

    public class UICategoryAxis : UIAxis
    {
        public UICategoryAxis()
        {
            Type = UIAxisType.Category;
        }

        public List<string> Data = new List<string>();

        public void Clear()
        {
            Data.Clear();
        }
    }

    public class UIValueAxis : UIAxis
    {
        public UIValueAxis()
        {
            Type = UIAxisType.Value;
        }
    }

    public class UIBarSeries : IDisposable
    {
        public string Name { get; set; }

        public UISeriesType Type => UISeriesType.Bar;
        
        public List<double> Data = new List<double>();

        public void AddData(double value)
        {
            Data.Add(value);
        }

        public void Dispose()
        {
            Data.Clear();
        }
    }
}