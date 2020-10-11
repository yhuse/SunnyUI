/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2020 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@qq.com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UIPieChartOption.cs
 * 文件说明: 饼状图配置类
 * 当前版本: V2.2
 * 创建日期: 2020-06-06
 *
 * 2020-06-06: V2.2.5 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sunny.UI
{
    public sealed class UIPieOption : UIOption, IDisposable
    {
        public List<UIPieSeries> Series = new List<UIPieSeries>();

        public UIPieToolTip ToolTip { get; set; } = new UIPieToolTip();

        public void AddSeries(UIPieSeries series)
        {
            Series.Clear();
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

    public class UIDoughnutOption : UIOption, IDisposable
    {
        public List<UIDoughnutSeries> Series = new List<UIDoughnutSeries>();

        public UIPieToolTip ToolTip { get; set; } = new UIPieToolTip();

        public void AddSeries(UIDoughnutSeries series)
        {
            Series.Clear();
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

    public class UIPieToolTip : UIChartToolTip
    {
        public UIPieToolTip()
        {
            Formatter = "{{a}}" + '\n' + "{{b}} : {{c}} ({{d}}%)";
            ValueFormat = "F0";
            Visible = true;
        }
    }

    public class UIPieSeries : IDisposable
    {
        public string Name { get; set; }

        public UISeriesType Type => UISeriesType.Pie;

        public int Radius { get; set; } = 70;

        public UICenter Center { get; set; } = new UICenter(50, 50);

        public readonly List<UIPieSeriesData> Data = new List<UIPieSeriesData>();

        public UIPieSeriesLabel Label = new UIPieSeriesLabel();

        public delegate Color OnDataColorChangeEventHandler(double data);

        public event OnDataColorChangeEventHandler DataColorChange;

        public void AddData(string name, double value)
        {
            if (DataColorChange != null)
            {
                Color color = DataColorChange.Invoke(value);
                Data.Add(new UIPieSeriesData(name, value, color));
            }
            else
            {
                Data.Add(new UIPieSeriesData(name, value));
            }
        }

        public void AddData(string name, double value, Color color)
        {
            Data.Add(new UIPieSeriesData(name, value, color));
        }

        public void Dispose()
        {
            Data.Clear();
        }
    }

    public class RadiusInOut
    {
        public int Inner { get; set; }

        public int Outer { get; set; }

        public RadiusInOut(int inner, int outer)
        {
            Inner = inner;
            Outer = outer;
        }
    }

    public class UIDoughnutSeries : IDisposable
    {
        public string Name { get; set; }

        public UISeriesType Type { get; set; }

        public RadiusInOut Radius { get; set; } = new RadiusInOut(50, 70);

        public UICenter Center { get; set; } = new UICenter(50, 50);

        public readonly List<UIPieSeriesData> Data = new List<UIPieSeriesData>();

        public UIPieSeriesLabel Label = new UIPieSeriesLabel();

        public void AddData(string name, double value)
        {
            Data.Add(new UIPieSeriesData(name, value));
        }

        public void Dispose()
        {
            Data.Clear();
        }
    }

    public class UIPieSeriesData
    {
        public string Name { get; set; }

        public double Value { get; set; }

        public Color Color { get; set; }

        public bool StyleCustomMode { get; private set; }

        public UIPieSeriesData()
        {
        }

        public UIPieSeriesData(string name, double value)
        {
            Name = name;
            Value = value;
            StyleCustomMode = false;
        }

        public UIPieSeriesData(string name, double value, Color color)
        {
            Name = name;
            Value = value;
            Color = color;
            StyleCustomMode = true;
        }
    }

    public class UIPieSeriesLabel
    {
        public bool Show { get; set; } = false;

        public UIPieSeriesLabelPosition Position { get; set; } = UIPieSeriesLabelPosition.Center;

        public string Formatter { get; set; } = "{{b}}";
    }

    public enum UIPieSeriesLabelPosition
    {
        Outside,
        Inside,
        Center
    }
}