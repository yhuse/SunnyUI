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
 * 文件名称: UIBarChart.cs
 * 文件说明: 柱状图
 * 当前版本: V2.2
 * 创建日期: 2020-06-06
 *
 * 2020-06-06: V2.2.5 增加文件说明
 * 2020-08-21: V2.2.7 可设置柱状图最小宽度
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public class UIBarChart : UIChart
    {
        protected bool NeedDraw;

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CalcData();
        }

        /// <summary>
        /// 计算刻度
        /// 起始值必须小于结束值
        /// </summary>
        /// <param name="start">起始值</param>
        /// <param name="end">结束值</param>
        /// <param name="expect_num">期望刻度数量，实际数接近此数</param>
        /// <param name="degree_start">刻度起始值，须乘以间隔使用</param>
        /// <param name="degree_end">刻度结束值，须乘以间隔使用</param>
        /// <param name="degree_gap">刻度间隔</param>
        public void CalcDegreeScale(double start, double end, int expect_num,
            out int degree_start, out int degree_end, out double degree_gap, out int decimalCount)
        {
            if (start >= end)
            {
                throw new Exception("起始值必须小于结束值");
            }

            double differ = end - start;
            double differ_gap = differ / (expect_num - 1); //35, 4.6, 0.27

            double exponent = Math.Log10(differ_gap) - 1; //0.54, -0.34, -1.57
            int _exponent = (int)exponent; //0, 0=>-1, -1=>-2
            if (exponent < 0 && Math.Abs(exponent) > 1e-8)
            {
                _exponent--;
            }

            int step = (int)(differ_gap / Math.Pow(10, _exponent)); //35, 46, 27
            int[] fix_steps = new int[] { 10, 20, 25, 50, 100 };
            int fix_step = 10; //25, 50, 25
            for (int i = fix_steps.Length - 1; i >= 1; i--)
            {
                if (step > (fix_steps[i] + fix_steps[i - 1]) / 2)
                {
                    fix_step = fix_steps[i];
                    break;
                }
            }

            degree_gap = fix_step * Math.Pow(10, _exponent); //25, 5, 0.25

            double start1 = start / degree_gap;
            int start2 = (int)start1;
            if (start1 < 0 && Math.Abs(start1 - start2) > 1e-8)
            {
                start2--;
            }

            degree_start = start2;

            double end1 = end / degree_gap;
            int end2 = (int)end1;
            if (end1 >= 0 && Math.Abs(end1 - end2) > 1e-8)
            {
                end2++;
            }

            degree_end = end2;
            _exponent = Math.Abs(_exponent);
            if (fix_step.IsEven()) _exponent--;
            if (_exponent < 0) _exponent = 0;
            decimalCount = _exponent;
        }

        protected override void CalcData()
        {
            Bars.Clear();
            NeedDraw = false;
            if (Option == null || Option.Series == null || Option.SeriesCount == 0) return;

            DrawOrigin = new Point(Option.Grid.Left, Height - Option.Grid.Bottom);
            DrawSize = new Size(Width - Option.Grid.Left - Option.Grid.Right,
                Height - Option.Grid.Top - Option.Grid.Bottom);

            if (DrawSize.Width <= 0 || DrawSize.Height <= 0) return;
            if (Option.XAxis.Data.Count == 0) return;

            NeedDraw = true;
            DrawBarWidth = DrawSize.Width * 1.0f / Option.XAxis.Data.Count;

            double min = double.MaxValue;
            double max = double.MinValue;
            foreach (var series in Option.Series)
            {
                if (series.Data.Count > 0)
                {
                    min = Math.Min(min, series.Data.Min());
                    max = Math.Max(max, series.Data.Max());
                }
            }

            if (min > 0 && max > 0 && !Option.YAxis.Scale) min = 0;
            if (min < 0 && max < 0 && !Option.YAxis.Scale) max = 0;
            if (!Option.YAxis.MaxAuto) max = Option.YAxis.Max;
            if (!Option.YAxis.MinAuto) min = Option.YAxis.Min;

            if ((max - min).IsZero() && min.IsZero())
            {
                max = 100;
                min = 0;
            }
            else
            {
                if (max > 0) min = 0;
                else max = 0;
            }

            CalcDegreeScale(min, max, Option.YAxis.SplitNumber,
                out int start, out int end, out double interval, out int decimalCount);

            YAxisStart = start;
            YAxisEnd = end;
            YAxisInterval = interval;
            YAxisDecimalCount = decimalCount;

            float x1 = DrawBarWidth / (Option.SeriesCount * 2 + Option.SeriesCount + 1);
            float x2 = x1 * 2;

            for (int i = 0; i < Option.SeriesCount; i++)
            {
                float barX = DrawOrigin.X;
                var series = Option.Series[i];
                Bars.TryAdd(i, new List<BarInfo>());
                for (int j = 0; j < series.Data.Count; j++)
                {
                    Color color = ChartStyle.GetColor(i);
                    if (series.Colors.Count > 0 && j >= 0 && j < series.Colors.Count)
                        color = series.Colors[j];

                    float xx = barX + x1 * (i + 1) + x2 * i + x1;
                    float ww = Math.Min(x2, series.MaxWidth);
                    xx -= ww / 2.0f;

                    if (YAxisStart >= 0)
                    {
                        float h = Math.Abs((float)(DrawSize.Height * (series.Data[j] - start * interval) / ((end - start) * interval)));

                        Bars[i].Add(new BarInfo()
                        {
                            Rect = new RectangleF(xx, DrawOrigin.Y - h, ww, h),
                            Color = color
                        });
                    }
                    else if (YAxisEnd <= 0)
                    {
                        float h = Math.Abs((float)(DrawSize.Height * (end * interval - series.Data[j]) / ((end - start) * interval)));
                        Bars[i].Add(new BarInfo()
                        {
                            Rect = new RectangleF(xx, Option.Grid.Top + 1, ww, h - 1),
                            Color = color
                        });
                    }
                    else
                    {
                        float lowH = 0;
                        float highH = 0;
                        float DrawBarHeight = DrawSize.Height * 1.0f / (YAxisEnd - YAxisStart);
                        float lowV = 0;
                        float highV = 0;
                        for (int k = YAxisStart; k <= YAxisEnd; k++)
                        {
                            if (k < 0) lowH += DrawBarHeight;
                            if (k > 0) highH += DrawBarHeight;
                            if (k < 0) lowV += (float)YAxisInterval;
                            if (k > 0) highV += (float)YAxisInterval;
                        }

                        // lowH.ConsoleWriteLine();
                        // highH.ConsoleWriteLine();

                        if (series.Data[j] >= 0)
                        {
                            float h = Math.Abs((float)(highH * series.Data[j] / highV));
                            Bars[i].Add(new BarInfo()
                            {
                                Rect = new RectangleF(xx, DrawOrigin.Y - lowH - h, ww, h),
                                Color = color
                            });
                        }
                        else
                        {
                            float h = Math.Abs((float)(lowH * series.Data[j] / lowV));
                            Bars[i].Add(new BarInfo()
                            {
                                Rect = new RectangleF(xx, DrawOrigin.Y - lowH + 1, ww, h - 1),
                                Color = color
                            });
                        }
                    }

                    barX += DrawBarWidth;
                }
            }

            if (Option.ToolTip != null)
            {
                for (int i = 0; i < Option.XAxis.Data.Count; i++)
                {
                    string str = Option.XAxis.Data[i];
                    foreach (var series in Option.Series)
                    {
                        str += '\n';
                        str += series.Name + " : " + series.Data[i].ToString(Option.ToolTip.ValueFormat);
                    }

                    Bars[0][i].Tips = str;
                }
            }
        }

        protected int selectIndex = -1;
        protected Point DrawOrigin;
        protected Size DrawSize;
        protected float DrawBarWidth;
        protected int YAxisStart;
        protected int YAxisEnd;
        protected double YAxisInterval;
        protected int YAxisDecimalCount;
        protected readonly ConcurrentDictionary<int, List<BarInfo>> Bars = new ConcurrentDictionary<int, List<BarInfo>>();

        [DefaultValue(-1), Browsable(false)]
        protected int SelectIndex
        {
            get => selectIndex;
            set
            {
                if (Option.ToolTip != null && selectIndex != value)
                {
                    selectIndex = value;
                    Invalidate();
                }

                if (selectIndex < 0) tip.Visible = false;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            try
            {
                if (!Option.ToolTip.Visible) return;
                if (e.Location.X > Option.Grid.Left && e.Location.X < Width - Option.Grid.Right
                                                       && e.Location.Y > Option.Grid.Top &&
                                                       e.Location.Y < Height - Option.Grid.Bottom)
                {
                    SelectIndex = (int)((e.Location.X - Option.Grid.Left) / DrawBarWidth);
                }
                else
                {
                    SelectIndex = -1;
                }

                if (SelectIndex >= 0 && Bars.Count > 0)
                {
                    if (tip.Text != Bars[0][selectIndex].Tips)
                    {
                        tip.Text = Bars[0][selectIndex].Tips;
                        tip.Size = new Size((int)Bars[0][selectIndex].Size.Width + 4, (int)Bars[0][selectIndex].Size.Height + 4);
                    }

                    int x = e.Location.X + 15;
                    int y = e.Location.Y + 20;
                    if (e.Location.X + 15 + tip.Width > Width - Option.Grid.Right)
                        x = e.Location.X - tip.Width - 2;
                    if (e.Location.Y + 20 + tip.Height > Height - Option.Grid.Bottom)
                        y = e.Location.Y - tip.Height - 2;

                    tip.Left = x;
                    tip.Top = y;
                    if (!tip.Visible) tip.Visible = Bars[0][selectIndex].Tips.IsValid();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        [Browsable(false), DefaultValue(null)]
        public UIBarOption Option
        {
            get
            {
                UIOption option = BaseOption ?? EmptyOption;
                return (UIBarOption)option;
            }

            // set
            // {
            //     SetOption(value);
            // }
        }

        protected override void CreateEmptyOption()
        {
            if (emptyOption != null) return;

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
            series.AddData(1);
            series.AddData(5);
            series.AddData(2);
            series.AddData(4);
            series.AddData(3);
            option.Series.Add(series);

            series = new UIBarSeries();
            series.Name = "Bar2";
            series.AddData(2);
            series.AddData(1);
            series.AddData(5);
            series.AddData(3);
            series.AddData(4);
            option.Series.Add(series);

            option.XAxis.Data.Add("Mon");
            option.XAxis.Data.Add("Tue");
            option.XAxis.Data.Add("Wed");
            option.XAxis.Data.Add("Thu");
            option.XAxis.Data.Add("Fri");

            option.ToolTip = new UIBarToolTip();
            option.ToolTip.AxisPointer.Type = UIAxisPointerType.Shadow;

            emptyOption = option;
        }

        protected override void DrawOption(Graphics g)
        {
            if (Option == null) return;
            if (!NeedDraw) return;

            if (Option.ToolTip != null && Option.ToolTip.AxisPointer.Type == UIAxisPointerType.Shadow) DrawToolTip(g);
            DrawAxis(g);
            DrawTitle(g, Option.Title);
            DrawSeries(g, Option.Series);
            if (Option.ToolTip != null && Option.ToolTip.AxisPointer.Type == UIAxisPointerType.Line) DrawToolTip(g);
            DrawLegend(g, Option.Legend);
            DrawAxisScales(g);
        }

        protected virtual void DrawToolTip(Graphics g)
        {
            if (selectIndex < 0) return;
            if (Option.ToolTip.AxisPointer.Type == UIAxisPointerType.Line)
            {
                float x = DrawOrigin.X + SelectIndex * DrawBarWidth + DrawBarWidth / 2.0f;
                g.DrawLine(ChartStyle.ToolTipShadowColor, x, DrawOrigin.Y, x, Option.Grid.Top);
            }

            if (Option.ToolTip.AxisPointer.Type == UIAxisPointerType.Shadow)
            {
                float x = DrawOrigin.X + SelectIndex * DrawBarWidth;
                g.FillRectangle(ChartStyle.ToolTipShadowColor, x, Option.Grid.Top, DrawBarWidth, Height - Option.Grid.Top - Option.Grid.Bottom);
            }
        }

        protected virtual void DrawAxis(Graphics g)
        {
            if (YAxisStart >= 0) g.DrawLine(ChartStyle.ForeColor, DrawOrigin, new Point(DrawOrigin.X + DrawSize.Width, DrawOrigin.Y));
            if (YAxisEnd <= 0) g.DrawLine(ChartStyle.ForeColor, new Point(DrawOrigin.X, Option.Grid.Top), new Point(DrawOrigin.X + DrawSize.Width, Option.Grid.Top));

            g.DrawLine(ChartStyle.ForeColor, DrawOrigin, new Point(DrawOrigin.X, DrawOrigin.Y - DrawSize.Height));

            if (Option.XAxis.AxisTick.Show)
            {
                float start;

                if (Option.XAxis.AxisTick.AlignWithLabel)
                {
                    start = DrawOrigin.X + DrawBarWidth / 2.0f;
                    for (int i = 0; i < Option.XAxis.Data.Count; i++)
                    {
                        g.DrawLine(ChartStyle.ForeColor, start, DrawOrigin.Y, start, DrawOrigin.Y + Option.XAxis.AxisTick.Length);
                        start += DrawBarWidth;
                    }
                }
                else
                {
                    bool haveZero = false;
                    for (int i = YAxisStart; i <= YAxisEnd; i++)
                    {
                        if (i == 0)
                        {
                            haveZero = true;
                            break;
                        }
                    }

                    if (!haveZero)
                    {
                        start = DrawOrigin.X;
                        for (int i = 0; i <= Option.XAxis.Data.Count; i++)
                        {
                            g.DrawLine(ChartStyle.ForeColor, start, DrawOrigin.Y, start, DrawOrigin.Y + Option.XAxis.AxisTick.Length);
                            start += DrawBarWidth;
                        }
                    }
                }
            }

            if (Option.XAxis.AxisLabel.Show)
            {
                float start = DrawOrigin.X + DrawBarWidth / 2.0f;
                foreach (var data in Option.XAxis.Data)
                {
                    SizeF sf = g.MeasureString(data, SubFont);
                    g.DrawString(data, SubFont, ChartStyle.ForeColor, start - sf.Width / 2.0f, DrawOrigin.Y + Option.XAxis.AxisTick.Length);
                    start += DrawBarWidth;
                }

                SizeF sfname = g.MeasureString(Option.XAxis.Name, SubFont);
                g.DrawString(Option.XAxis.Name, SubFont, ChartStyle.ForeColor, DrawOrigin.X + (DrawSize.Width - sfname.Width) / 2.0f, DrawOrigin.Y + Option.XAxis.AxisTick.Length + sfname.Height);
            }

            if (Option.YAxis.AxisTick.Show)
            {
                float start = DrawOrigin.Y;
                float DrawBarHeight = DrawSize.Height * 1.0f / (YAxisEnd - YAxisStart);
                for (int i = YAxisStart; i <= YAxisEnd; i++)
                {
                    g.DrawLine(ChartStyle.ForeColor, DrawOrigin.X, start, DrawOrigin.X - Option.YAxis.AxisTick.Length, start);

                    if (i != 0)
                    {
                        using (Pen pn = new Pen(ChartStyle.ForeColor))
                        {
                            pn.DashStyle = DashStyle.Dash;
                            pn.DashPattern = new float[] { 3, 3 };
                            g.DrawLine(pn, DrawOrigin.X, start, Width - Option.Grid.Right, start);
                        }
                    }
                    else
                    {
                        g.DrawLine(ChartStyle.ForeColor, DrawOrigin.X, start, Width - Option.Grid.Right, start);

                        float lineStart = DrawOrigin.X;
                        for (int j = 0; j <= Option.XAxis.Data.Count; j++)
                        {
                            g.DrawLine(ChartStyle.ForeColor, lineStart, start, lineStart, start + Option.XAxis.AxisTick.Length);
                            lineStart += DrawBarWidth;
                        }
                    }

                    start -= DrawBarHeight;
                }
            }

            if (Option.YAxis.AxisLabel.Show)
            {
                float start = DrawOrigin.Y;
                float DrawBarHeight = DrawSize.Height * 1.0f / (YAxisEnd - YAxisStart);
                int idx = 0;
                float wmax = 0;

                if (Option.YAxis.AxisLabel.AutoFormat)
                    Option.YAxis.AxisLabel.DecimalCount = YAxisDecimalCount;

                for (int i = YAxisStart; i <= YAxisEnd; i++)
                {
                    string label = Option.YAxis.AxisLabel.GetLabel(i * YAxisInterval, idx);
                    SizeF sf = g.MeasureString(label, SubFont);
                    wmax = Math.Max(wmax, sf.Width);
                    g.DrawString(label, SubFont, ChartStyle.ForeColor, DrawOrigin.X - Option.YAxis.AxisTick.Length - sf.Width, start - sf.Height / 2.0f);
                    start -= DrawBarHeight;
                }

                SizeF sfname = g.MeasureString(Option.YAxis.Name, SubFont);
                int x = (int)(DrawOrigin.X - Option.YAxis.AxisTick.Length - wmax - sfname.Height);
                int y = (int)(Option.Grid.Top + (DrawSize.Height - sfname.Width) / 2);
                g.DrawString(Option.YAxis.Name, SubFont, ChartStyle.ForeColor, new Point(x, y),
                    new StringFormat() { Alignment = StringAlignment.Center }, 270);
            }
        }

        private void DrawAxisScales(Graphics g)
        {
            foreach (var line in Option.YAxisScaleLines)
            {
                double ymin = YAxisStart * YAxisInterval;
                double ymax = YAxisEnd * YAxisInterval;
                float pos = (float)((line.Value - ymin) * (Height - Option.Grid.Top - Option.Grid.Bottom) / (ymax - ymin));
                pos = (Height - Option.Grid.Bottom - pos);
                using (Pen pn = new Pen(line.Color, line.Size))
                {
                    g.DrawLine(pn, DrawOrigin.X, pos, Width - Option.Grid.Right, pos);
                }

                SizeF sf = g.MeasureString(line.Name, SubFont);

                if (line.Left == UILeftAlignment.Left)
                    g.DrawString(line.Name, SubFont, line.Color, DrawOrigin.X + 4, pos - 2 - sf.Height);
                if (line.Left == UILeftAlignment.Center)
                    g.DrawString(line.Name, SubFont, line.Color, DrawOrigin.X + (Width - Option.Grid.Left - Option.Grid.Right - sf.Width) / 2, pos - 2 - sf.Height);
                if (line.Left == UILeftAlignment.Right)
                    g.DrawString(line.Name, SubFont, line.Color, Width - sf.Width - 4 - Option.Grid.Right, pos - 2 - sf.Height);
            }
        }

        protected virtual void DrawSeries(Graphics g, List<UIBarSeries> series)
        {
            if (series == null || series.Count == 0) return;

            for (int i = 0; i < Bars.Count; i++)
            {
                var bars = Bars[i];
                foreach (var info in bars)
                {
                    g.FillRectangle(info.Color, info.Rect);
                }
            }

            for (int i = 0; i < Option.XAxis.Data.Count; i++)
            {
                Bars[0][i].Size = g.MeasureString(Bars[0][i].Tips, SubFont);
            }
        }

        protected class BarInfo
        {
            public RectangleF Rect { get; set; }

            public string Tips { get; set; }

            public SizeF Size { get; set; }

            public Color Color { get; set; }
        }
    }
}