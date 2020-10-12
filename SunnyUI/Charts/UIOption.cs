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
 * 文件名称: UIOption.cs
 * 文件说明: 图表设置类
 * 当前版本: V2.2
 * 创建日期: 2020-06-06
 *
 * 2020-06-06: V2.2.5 增加文件说明
******************************************************************************/

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

        public string ValueFormat { get; set; }
    }

    public class UIScaleLine
    {
        public double Value { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }
        public UILeftAlignment Left { get; set; } = UILeftAlignment.Left;

        public float Size { get; set; } = 1.0f;
    }

    public class UILegend
    {
        public UILeftAlignment Left { get; set; } = UILeftAlignment.Center;

        public UITopAlignment Top { get; set; } = UITopAlignment.Top;

        public UIOrient Orient { get; set; } = UIOrient.Vertical;

        public readonly List<string> Data = new List<string>();

        public readonly List<Color> Colors = new List<Color>();

        public int DataCount => Data.Count;

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

    public class UIChartGrid
    {
        public int Left { get; set; } = 60;
        public int Right { get; set; } = 60;
        public int Top { get; set; } = 60;
        public int Bottom { get; set; } = 60;
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


}