/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UIOption.cs
 * 文件说明: 图表设置类
 * 当前版本: V3.1
 * 创建日期: 2020-06-06
 *
 * 2020-06-06: V2.2.5 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sunny.UI
{
    public abstract class UIOption
    {
        public UITitle Title = new UITitle();
        public UILegend Legend;
    }

    public class UIChartToolTip
    {
        public bool Visible { get; set; }

        public string Formatter { get; set; }
    }

    public class UIAxis
    {
        public UIAxis(UIAxisType axisType)
        {
            Type = axisType;
        }

        public string Name { get; set; }

        public UIAxisType Type { get; }

        /// <summary>
        /// 是否是脱离 0 值比例。设置成 true 后坐标刻度不会强制包含零刻度。在双数值轴的散点图中比较有用。
        /// </summary>
        public bool Scale { get; set; }

        /// <summary>
        /// 坐标轴刻度
        /// </summary>
        public UIAxisTick AxisTick = new UIAxisTick();

        /// <summary>
        /// 坐标轴标签
        /// </summary>
        public UIAxisLabel AxisLabel = new UIAxisLabel();

        public bool MaxAuto { get; set; } = true;
        public bool MinAuto { get; set; } = true;

        public double Max { get; set; } = 100;
        public double Min { get; set; } = 0;

        public List<string> Data = new List<string>();

        public bool ShowGridLine { get; set; } = true;

        public void Clear()
        {
            Data.Clear();
        }

        public CustomLabels CustomLabels;

        public bool HaveCustomLabels
        {
            get => CustomLabels != null && CustomLabels.Count > 0;
        }

        public void SetMinValue(double min)
        {
            Min = min;
            MinAuto = false;
        }

        public void SetMaxValue(double max)
        {
            Max = max;
            MaxAuto = false;
        }

        public void SetRange(double min, double max)
        {
            SetMinValue(min);
            SetMaxValue(max);
        }

        public void SetMinValue(DateTime min)
        {
            Min = new DateTimeInt64(min);
            MinAuto = false;
        }

        public void SetMaxValue(DateTime max)
        {
            Max = new DateTimeInt64(max);
            MaxAuto = false;
        }

        public void SetRange(DateTime min, DateTime max)
        {
            SetMinValue(min);
            SetMaxValue(max);
        }

        public bool ShowArrow { get; set; } = false;

        public int ShowArrowSize { get; set; } = 30;

        public string Unit { get; set; } = string.Empty;
    }

    public class CustomLabels
    {
        public double Start { get; set; }

        public double Interval { get; set; }

        public int Count { get; set; }

        public double IntervalMilliseconds { get; set; }

        public UIAxisType AxisType { get; set; }

        public List<string> Labels = new List<string>();

        public double[] LabelValues()
        {
            double[] values = new double[Count + 1];

            for (int i = 0; i <= Count; i++)
            {
                if (AxisType == UIAxisType.DateTime)
                {
                    DateTimeInt64 dateTime = new DateTimeInt64(Start);
                    dateTime.AddMilliseconds(IntervalMilliseconds * i);
                    values[i] = dateTime.DoubleValue;
                }
                else
                {
                    values[i] = Start + Interval * i;
                }
            }

            return values;
        }

        public double Stop
        {
            get
            {
                if (AxisType == UIAxisType.DateTime)
                {
                    DateTimeInt64 dateTime = new DateTimeInt64(Start);
                    dateTime.AddMilliseconds(IntervalMilliseconds * Count);
                    return dateTime.DoubleValue;
                }
                else
                {
                    return Start + Interval * Count;
                }
            }
        }

        public CustomLabels(double start, double interval, int count)
        {
            Start = start;
            Interval = Math.Abs(interval);
            Count = Math.Max(2, count);
            AxisType = UIAxisType.Value;
        }

        public CustomLabels(DateTime start, int intervalMilliseconds, int count)
        {
            Start = new DateTimeInt64(start);
            IntervalMilliseconds = Math.Abs(intervalMilliseconds);
            Count = Math.Max(2, count);
            AxisType = UIAxisType.DateTime;
        }

        public void SetLabels(string[] labels)
        {
            Labels.Clear();
            Labels.AddRange(labels);
        }

        public void AddLabel(string label)
        {
            Labels.Add(label);
        }

        public void ClearLabels()
        {
            Labels.Clear();
        }

        public string GetLabel(int i)
        {
            if (i >= 0 && i < Labels.Count) return Labels[i];
            else return string.Empty;
        }
    }

    public class UIAxisLabel
    {
        /// <summary>
        /// 是否显示刻度标签。
        /// </summary>
        public bool Show { get; set; } = true;

        public int Angle { get; set; } = 0;

        private int decimalPlaces = -1;

        /// <summary>
        /// 小数位个数
        /// </summary>
        public int DecimalPlaces
        {
            get => decimalPlaces;
            set => decimalPlaces = Math.Max(0, value);
        }

        private string dateTimeFormat = "";

        /// <summary>
        /// 日期格式化字符串
        /// </summary>
        public string DateTimeFormat
        {
            get => dateTimeFormat;
            set
            {
                try
                {
                    DateTime.Now.ToString(value);
                    dateTimeFormat = value;
                }
                catch
                {
                    dateTimeFormat = "";
                }
            }
        }

        public void ClearFormat()
        {
            dateTimeFormat = "";
            decimalPlaces = -1;
        }
    }

    public class UIAxisTick
    {
        /// <summary>
        /// 是否显示坐标轴刻度。
        /// </summary>
        public bool Show { get; set; } = true;

        /// <summary>
        /// 坐标轴刻度的长度。
        /// </summary>
        public int Length { get; set; } = 5;
    }

    public class UIScaleLine
    {
        public double Value { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }
        public UILeftAlignment Left { get; set; } = UILeftAlignment.Left;

        public float Size { get; set; } = 1.0f;

        public bool DashDot { get; set; }

        public UIScaleLine()
        {

        }

        public UIScaleLine(string name, double value, Color color)
        {
            Name = name;
            Color = color;
            Value = value;
        }

        public UIScaleLine(string name, DateTime value, Color color)
        {
            Name = name;
            Color = color;
            Value = new DateTimeInt64(value);
        }
    }

    public class UILegend
    {
        public UILeftAlignment Left { get; set; } = UILeftAlignment.Center;

        public UITopAlignment Top { get; set; } = UITopAlignment.Top;

        public UIOrient Orient { get; set; } = UIOrient.Vertical;

        public readonly List<string> Data = new List<string>();

        public readonly List<Color> Colors = new List<Color>();

        public int DataCount => Data.Count;

        public UILegendStyle Style { get; set; } = UILegendStyle.Rectangle;

        public void AddData(string data)
        {
            Data.Add(data);
        }

        public void AddData(string data, Color color)
        {
            Data.Add(data);
            Colors.Add(color);
        }

        public bool HaveCustomColor(int index)
        {
            return Colors.Count > 0 && index >= 0 && index < Colors.Count;
        }

        public void Clear()
        {
            Data.Clear();
            Colors.Clear();
        }
    }

    public enum UILegendStyle
    {
        Rectangle,
        Line
    }

    public class UIChartGrid
    {
        public int Left { get; set; } = 60;
        public int Right { get; set; } = 60;
        public int Top { get; set; } = 60;
        public int Bottom { get; set; } = 60;

        public bool LeftShow { get; set; } = true;
        public bool RightShow { get; set; } = true;
        public bool TopShow { get; set; } = true;
        public bool BottomShow { get; set; } = true;
    }

    public enum UIOrient
    {
        Vertical,
        Horizontal
    }

    public enum UIAxisType
    {
        Value,
        Category,
        DateTime
    }

    public class UITitle
    {
        public string Text { get; set; } = "SunnyUI Chart";

        public string SubText { get; set; } = "";

        public UILeftAlignment Left { get; set; } = UILeftAlignment.Center;

        public UITopAlignment Top { get; set; } = UITopAlignment.Top;
    }

    public enum UILeftAlignment
    {
        Left,
        Center,
        Right
    }

    public enum UITopAlignment
    {
        Top,
        Center,
        Bottom
    }

    public class UICenter
    {
        public int Left { get; set; }

        public int Top { get; set; }

        public UICenter() : this(50, 50)
        {
        }

        public UICenter(int left, int top)
        {
            Left = left;
            Top = top;
        }
    }

    public enum UISeriesType
    {
        Pie,
        Line,
        Bar
    }

    public enum UIChartStyleType
    {
        Default,
        Plain,
        Dark,
        LiveChart
    }

    public static class UIChartStyles
    {
        public static UIChartStyle Default = new UIDefaultChartStyle();

        public static UIChartStyle Plain = new UILightChartStyle();

        public static UIChartStyle Dark = new UIDarkChartStyle();

        public static UIChartStyle LiveChart = new UILiveChartStyle();

        public static UIChartStyle GetChartStyle(UIChartStyleType style)
        {
            if (style == UIChartStyleType.Default) return Default;
            if (style == UIChartStyleType.Dark) return Dark;
            if (style == UIChartStyleType.LiveChart) return LiveChart;
            return Plain;
        }
    }

    public class ZoomArea
    {
        public bool XMinAuto { get; set; }
        public bool XMaxAuto { get; set; }
        public bool YMinAuto { get; set; }
        public bool YMaxAuto { get; set; }
        public bool Y2MinAuto { get; set; }
        public bool Y2MaxAuto { get; set; }
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
        public double Y2Min { get; set; }
        public double Y2Max { get; set; }

        public double XRange => XMax - XMin;
        public double YRange => YMax - YMin;
        public double Y2Range => Y2Max - Y2Min;

    }
}