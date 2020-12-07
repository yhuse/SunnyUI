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
 * 文件名称: UILineChart.cs
 * 文件说明: 曲线图
 * 当前版本: V2.2
 * 创建日期: 2020-10-01
 *
 * 2020-10-01: V2.2.8 完成曲线图表
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public sealed class UILineChart : UIChart
    {
        private bool NeedDraw;

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CalcData();
        }

        private Point DrawOrigin;
        private Size DrawSize;

        protected override void CalcData()
        {
            NeedDraw = false;
            if (Option?.Series == null || Option.Series.Count == 0) return;

            DrawOrigin = new Point(Option.Grid.Left, Height - Option.Grid.Bottom);
            DrawSize = new Size(Width - Option.Grid.Left - Option.Grid.Right,
                Height - Option.Grid.Top - Option.Grid.Bottom);

            if (DrawSize.Width <= 0 || DrawSize.Height <= 0) return;
            CalcAxises();

            foreach (var series in Option.Series.Values)
            {
                series.ClearPoints();
                float[] x = XScale.CalcXPixels(series.XData.ToArray(), DrawOrigin.X, DrawSize.Width);
                float[] y = YScale.CalcYPixels(series.YData.ToArray(), DrawOrigin.Y, DrawSize.Height);
                series.AddPoints(x, y);
            }

            NeedDraw = true;
        }

        private UIScale XScale;
        private UIScale YScale;
        private double[] YLabels;
        private double[] XLabels;

        private void CalcAxises()
        {
            if (Option.XAxisType == UIAxisType.DateTime)
                XScale = new UIDateScale();
            else
                XScale = new UILinearScale();

            YScale = new UILinearScale();

            //Y轴
            {
                Option.GetAllDataYRange(out double min, out double max);
                if (min > 0 && max > 0 && !Option.YAxis.Scale) min = 0;
                if (min < 0 && max < 0 && !Option.YAxis.Scale) max = 0;
                YScale.SetRange(min, max);
                if (!Option.YAxis.MaxAuto) YScale.Max = Option.YAxis.Max;
                if (!Option.YAxis.MinAuto) YScale.Min = Option.YAxis.Min;
                YScale.AxisChange();
                YLabels = YScale.CalcLabels();
            }

            //X轴
            {
                Option.GetAllDataXRange(out double min, out double max);
                XScale.SetRange(min, max);
                if (!Option.XAxis.MaxAuto) XScale.Max = Option.XAxis.Max;
                if (!Option.XAxis.MinAuto) XScale.Min = Option.XAxis.Min;
                XScale.AxisChange();
                XLabels = XScale.CalcLabels();
            }
        }

        [Browsable(false), DefaultValue(null)]
        public UILineOption Option
        {
            get
            {
                UIOption option = BaseOption ?? EmptyOption;
                return (UILineOption)option;
            }

            // set
            // {
            //     SetOption(value);
            // }
        }

        protected override void CreateEmptyOption()
        {
            if (emptyOption != null) return;

            UILineOption option = new UILineOption();
            option.Title = new UITitle();
            option.Title.Text = "SunnyUI";
            option.Title.SubText = "LineChart";

            var series = option.AddSeries(new UILineSeries("Line1"));
            for (int i = 0; i < 200; i++)
            {
                series.Add(i, 6 * Math.Sin(i * 3 * Math.PI / 180));
            }

            series = option.AddSeries(new UILineSeries("Line2"));
            for (int i = 0; i < 150; i++)
            {
                series.Add(i, 6 * Math.Cos(i * 5 * Math.PI / 180));
            }

            series.Smooth = true;

            option.XAxis.Name = "数值";
            option.YAxis.Name = "数值";

            emptyOption = option;
        }

        protected override void DrawOption(Graphics g)
        {
            if (Option == null) return;
            if (!NeedDraw) return;

            // if (BarOption.ToolTip != null && BarOption.ToolTip.AxisPointer.Type == UIAxisPointerType.Shadow) DrawToolTip(g);
            DrawAxis(g);
            DrawTitle(g, Option.Title);
            DrawAxisScales(g);
            DrawSeries(g);
            // if (BarOption.ToolTip != null && BarOption.ToolTip.AxisPointer.Type == UIAxisPointerType.Line) DrawToolTip(g);
            DrawLegend(g, Option.Legend);
        }

        private void DrawAxis(Graphics g)
        {
            g.DrawRectangle(ChartStyle.ForeColor, Option.Grid.Left, Option.Grid.Top, DrawSize.Width, DrawSize.Height);
            float zeroPos = YScale.CalcYPixel(0, DrawOrigin.Y, DrawSize.Height);
            g.DrawLine(ChartStyle.ForeColor, DrawOrigin.X, zeroPos, DrawOrigin.X + DrawSize.Width, zeroPos);
            if (XScale == null || YScale == null) return;

            //X Tick
            if (Option.XAxis.AxisTick.Show)
            {
                float[] labels = XScale.CalcXPixels(XLabels, DrawOrigin.X, DrawSize.Width);
                for (int i = 0; i < labels.Length; i++)
                {
                    float x = labels[i];
                    if (Option.XAxis.AxisLabel.Show)
                    {
                        string label;
                        if (Option.XAxisType == UIAxisType.DateTime)
                        {
                            if (Option.XAxis.AxisLabel.AutoFormat)
                                label = new DateTimeInt64(XLabels[i]).ToString(XScale.Format);
                            else
                                label = new DateTimeInt64(XLabels[i]).ToString(Option.XAxis.AxisLabel.DateTimeFormat);
                        }
                        else
                        {
                            if (Option.XAxis.AxisLabel.AutoFormat)
                                label = XLabels[i].ToString(XScale.Format);
                            else
                                label = new DateTimeInt64(XLabels[i]).ToString("F" + Option.XAxis.AxisLabel.DecimalCount);
                        }

                        SizeF sf = g.MeasureString(label, SubFont);
                        g.DrawString(label, SubFont, ChartStyle.ForeColor, x - sf.Width / 2.0f, DrawOrigin.Y + Option.XAxis.AxisTick.Length);
                    }

                    if (x.Equals(DrawOrigin.X)) continue;
                    if (x.Equals(DrawOrigin.X + DrawSize.Width)) continue;

                    using (Pen pn = new Pen(ChartStyle.ForeColor))
                    {
                        pn.DashStyle = DashStyle.Dash;
                        pn.DashPattern = new float[] { 3, 3 };
                        g.DrawLine(pn, x, DrawOrigin.Y, x, Option.Grid.Top);
                    }
                }

                SizeF sfName = g.MeasureString(Option.XAxis.Name, SubFont);
                g.DrawString(Option.XAxis.Name, SubFont, ChartStyle.ForeColor,
                    DrawOrigin.X + (DrawSize.Width - sfName.Width) / 2.0f,
                    DrawOrigin.Y + Option.XAxis.AxisTick.Length + sfName.Height);
            }

            //Y Tick
            if (Option.YAxis.AxisTick.Show)
            {
                float[] labels = YScale.CalcYPixels(YLabels, DrawOrigin.Y, DrawSize.Height);
                float widthMax = 0;
                for (int i = 0; i < labels.Length; i++)
                {
                    float y = labels[i];
                    if (Option.YAxis.AxisLabel.Show)
                    {
                        string label = YLabels[i].ToString(YScale.Format);
                        SizeF sf = g.MeasureString(label, SubFont);
                        widthMax = Math.Max(widthMax, sf.Width);
                        g.DrawString(label, SubFont, ChartStyle.ForeColor, DrawOrigin.X - Option.YAxis.AxisTick.Length - sf.Width, y - sf.Height / 2.0f);
                    }

                    if (y.Equals(DrawOrigin.Y)) continue;
                    if (y.Equals(DrawOrigin.X - DrawSize.Height)) continue;

                    using (Pen pn = new Pen(ChartStyle.ForeColor))
                    {
                        pn.DashStyle = DashStyle.Dash;
                        pn.DashPattern = new float[] { 3, 3 };
                        g.DrawLine(pn, DrawOrigin.X, y, Width - Option.Grid.Right, y);
                    }
                }


                SizeF sfName = g.MeasureString(Option.YAxis.Name, SubFont);
                int xx = (int)(DrawOrigin.X - Option.YAxis.AxisTick.Length - widthMax - sfName.Height);
                int yy = (int)(Option.Grid.Top + (DrawSize.Height - sfName.Width) / 2);
                g.DrawString(Option.YAxis.Name, SubFont, ChartStyle.ForeColor, new Point(xx, yy),
                    new StringFormat() { Alignment = StringAlignment.Center }, 270);
            }
        }

        private void DrawSeries(Graphics g, Color color, UILineSeries series)
        {
            if (series.Points.Count == 0)
            {
                return;
            }

            if (series.Points.Count == 1 && series.Symbol == UILinePointSymbol.None)
            {
                g.DrawPoint(color, series.Points[0], 4);
                return;
            }

            using (Pen pen = new Pen(color, series.Width))
            {
                g.SetHighQuality();
                if (series.Smooth)
                    g.DrawCurve(pen, series.Points.ToArray());
                else
                    g.DrawLines(pen, series.Points.ToArray());
                g.SetDefaultQuality();
            }
        }

        private void DrawSeries(Graphics g)
        {
            if (YScale == null) return;

            int idx = 0;
            if (Option.GreaterWarningArea == null && Option.LessWarningArea == null)
            {
                foreach (var series in Option.Series.Values)
                {
                    Color color = series.Color;
                    if (!series.CustomColor) color = ChartStyle.GetColor(idx);
                    DrawSeries(g, color, series);
                    idx++;
                }
            }
            else
            {
                Bitmap bmp = new Bitmap(Width, Height);
                Bitmap bmpGreater = new Bitmap(Width, Height);
                Bitmap bmpLess = new Bitmap(Width, Height);
                float wTop = 0;
                float wBottom = Height;

                foreach (var series in Option.Series.Values)
                {
                    Color color = series.Color;
                    if (!series.CustomColor) color = ChartStyle.GetColor(idx);

                    using (Graphics graphics = bmp.Graphics())
                    {
                        DrawSeries(graphics, color, series);
                    }

                    if (Option.GreaterWarningArea != null)
                    {
                        using (Graphics graphics = bmpGreater.Graphics())
                        {
                            DrawSeries(graphics, Option.GreaterWarningArea.Color, series);
                        }
                    }

                    if (Option.LessWarningArea != null)
                    {
                        using (Graphics graphics = bmpLess.Graphics())
                        {
                            DrawSeries(graphics, Option.LessWarningArea.Color, series);
                        }
                    }

                    idx++;
                }

                if (Option.GreaterWarningArea != null)
                {
                    wTop = YScale.CalcYPixel(Option.GreaterWarningArea.Value, DrawOrigin.Y, DrawSize.Height);
                    g.DrawImage(bmpGreater, new Rectangle(0, 0, Width, (int)wTop),
                        new Rectangle(0, 0, Width, (int)wTop), GraphicsUnit.Pixel);
                }

                if (Option.LessWarningArea != null)
                {
                    wBottom = YScale.CalcYPixel(Option.LessWarningArea.Value, DrawOrigin.Y, DrawSize.Height);
                    g.DrawImage(bmpLess, new Rectangle(0, (int)wBottom, Width, Height - (int)wBottom),
                        new Rectangle(0, (int)wBottom, Width, Height - (int)wBottom), GraphicsUnit.Pixel);
                }

                g.DrawImage(bmp, new Rectangle(0, (int)wTop, Width, (int)wBottom - (int)wTop),
                    new Rectangle(0, (int)wTop, Width, (int)wBottom - (int)wTop), GraphicsUnit.Pixel);

                bmpGreater.Dispose();
                bmpLess.Dispose();
                bmp.Dispose();
            }

            idx = 0;
            foreach (var series in Option.Series.Values)
            {
                Color color = series.Color;
                if (!series.CustomColor) color = ChartStyle.GetColor(idx);

                if (series.Symbol != UILinePointSymbol.None)
                {
                    using (Brush br = new SolidBrush(ChartStyle.BackColor))
                    using (Pen pn = new Pen(color, series.SymbolLineWidth))
                    {
                        foreach (var p in series.Points)
                        {
                            switch (series.Symbol)
                            {
                                case UILinePointSymbol.Square:
                                    g.FillRectangle(br, p.X - series.SymbolSize, p.Y - series.SymbolSize, series.SymbolSize * 2, series.SymbolSize * 2);
                                    g.DrawRectangle(pn, p.X - series.SymbolSize, p.Y - series.SymbolSize, series.SymbolSize * 2, series.SymbolSize * 2);
                                    break;
                                case UILinePointSymbol.Diamond:
                                    {
                                        PointF pt1 = new PointF(p.X - series.SymbolSize, p.Y);
                                        PointF pt2 = new PointF(p.X, p.Y - series.SymbolSize);
                                        PointF pt3 = new PointF(p.X + series.SymbolSize, p.Y);
                                        PointF pt4 = new PointF(p.X, p.Y + series.SymbolSize);
                                        PointF[] pts = { pt1, pt2, pt3, pt4, pt1 };
                                        g.SetHighQuality();
                                        GraphicsPath path = pts.Path();
                                        g.FillPath(br, path);
                                        g.DrawPath(pn, path);
                                        path.Dispose();
                                    }
                                    break;
                                case UILinePointSymbol.Triangle:
                                    {
                                        PointF pt1 = new PointF(p.X, p.Y - series.SymbolSize);
                                        PointF pt2 = new PointF(p.X - series.SymbolSize * 0.866f, p.Y + series.SymbolSize * 0.5f);
                                        PointF pt3 = new PointF(p.X + series.SymbolSize * 0.866f, p.Y + series.SymbolSize * 0.5f);
                                        PointF[] pts = { pt1, pt2, pt3, pt1 };
                                        g.SetHighQuality();
                                        GraphicsPath path = pts.Path();
                                        g.FillPath(br, path);
                                        g.DrawPath(pn, path);
                                        path.Dispose();
                                    }
                                    break;
                                case UILinePointSymbol.Circle:
                                    g.SetHighQuality();
                                    g.FillEllipse(br, p.X - series.SymbolSize, p.Y - series.SymbolSize, series.SymbolSize * 2, series.SymbolSize * 2);
                                    g.DrawEllipse(pn, p.X - series.SymbolSize, p.Y - series.SymbolSize, series.SymbolSize * 2, series.SymbolSize * 2);
                                    break;
                                case UILinePointSymbol.Plus:
                                    g.DrawLine(pn, p.X - series.SymbolSize, p.Y, p.X + series.SymbolSize, p.Y);
                                    g.DrawLine(pn, p.X, p.Y - series.SymbolSize, p.X, p.Y + series.SymbolSize);
                                    break;
                                case UILinePointSymbol.Star:
                                    g.SetHighQuality();
                                    g.DrawLine(pn, p.X, p.Y - series.SymbolSize, p.X, p.Y + series.SymbolSize);
                                    g.DrawLine(pn, p.X - series.SymbolSize * 0.866f, p.Y + series.SymbolSize * 0.5f,
                                        p.X + series.SymbolSize * 0.866f, p.Y - series.SymbolSize * 0.5f);
                                    g.DrawLine(pn, p.X - series.SymbolSize * 0.866f, p.Y - series.SymbolSize * 0.5f,
                                        p.X + series.SymbolSize * 0.866f, p.Y + series.SymbolSize * 0.5f);
                                    break;
                            }
                        }
                    }

                    g.SetDefaultQuality();
                }

                idx++;
            }
        }

        private void DrawAxisScales(Graphics g)
        {
            if (YScale == null) return;

            foreach (var line in Option.YAxisScaleLines)
            {
                float pos = YScale.CalcYPixel(line.Value, DrawOrigin.Y, DrawSize.Height);
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

            int idx = 0;
            foreach (var line in Option.XAxisScaleLines)
            {
                float pos = XScale.CalcXPixel(line.Value, DrawOrigin.X, DrawSize.Width);
                using (Pen pn = new Pen(line.Color, line.Size))
                {
                    g.DrawLine(pn, pos, DrawOrigin.Y, pos, Option.Grid.Top);
                }

                SizeF sf = g.MeasureString(line.Name, SubFont);
                float x = pos - sf.Width;
                if (x < Option.Grid.Left) x = pos + 2;
                float y = Option.Grid.Top + 4 + sf.Height * idx;
                if (y > Height - Option.Grid.Bottom)
                {
                    idx = 0;
                    y = Option.Grid.Top + 4 + sf.Height * idx;
                }

                idx++;
                g.DrawString(line.Name, SubFont, line.Color, x, y);
            }
        }

        private readonly List<UILineSelectPoint> selectPoints = new List<UILineSelectPoint>();
        private readonly List<UILineSelectPoint> selectPointsTemp = new List<UILineSelectPoint>();

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!NeedDraw) return;

            selectPointsTemp.Clear();
            foreach (var series in Option.Series.Values)
            {
                if (series.DataCount == 0) continue;
                if (series.GetNearestPoint(e.Location, 4, out double x, out double y, out int index))
                {
                    UILineSelectPoint point = new UILineSelectPoint();
                    point.SeriesIndex = series.Index;
                    point.Name = series.Name;
                    point.Index = index;
                    point.X = x;
                    point.Y = y;
                    point.Location = new Point((int)series.Points[index].X, (int)series.Points[index].Y);
                    selectPointsTemp.Add(point);
                }
            }

            bool isNew = false;
            if (selectPointsTemp.Count != selectPoints.Count)
            {
                isNew = true;
            }
            else
            {
                Dictionary<string, UILineSelectPoint> points = selectPoints.ToDictionary(p => p.Name);
                foreach (var point in selectPointsTemp)
                {
                    if (!points.ContainsKey(point.Name))
                    {
                        isNew = true;
                        break;
                    }

                    if (points[point.Name].Index != point.Index)
                    {
                        isNew = true;
                        break;
                    }
                }
            }

            if (isNew)
            {
                selectPoints.Clear();
                StringBuilder sb = new StringBuilder();
                int idx = 0;
                Dictionary<int, UILineSelectPoint> dictionary = selectPointsTemp.ToDictionary(p => p.SeriesIndex);
                List<UILineSelectPoint> points = dictionary.SortedValues();
                foreach (var point in points)
                {
                    selectPoints.Add(point);

                    if (idx > 0) sb.Append('\n');

                    sb.Append(point.Name);
                    sb.Append('\n');
                    sb.Append(Option.XAxis.Name + ": ");
                    if (Option.XAxisType == UIAxisType.DateTime)
                        sb.Append(new DateTimeInt64(point.X).ToString(Option.XAxis.AxisLabel.DateTimeFormat));
                    else
                        sb.Append(point.X.ToString("F" + Option.XAxis.AxisLabel.DecimalCount));
                    sb.Append('\n');
                    sb.Append(Option.YAxis.Name + ": " + point.Y.ToString("F" + Option.YAxis.AxisLabel.DecimalCount));
                    idx++;
                }

                if (Option.ToolTip.Visible)
                {
                    if (sb.ToString().IsNullOrEmpty())
                    {
                        tip.Visible = false;
                    }
                    else
                    {
                        using (Graphics g = this.CreateGraphics())
                        {
                            SizeF sf = g.MeasureString(sb.ToString(), SubFont);
                            tip.Size = new Size((int)sf.Width + 4, (int)sf.Height + 4);
                        }

                        int x = e.Location.X + 15;
                        int y = e.Location.Y + 20;
                        if (e.Location.X + 15 + tip.Width > Width - Option.Grid.Right)
                            x = e.Location.X - tip.Width - 2;
                        if (e.Location.Y + 20 + tip.Height > Height - Option.Grid.Bottom)
                            y = e.Location.Y - tip.Height - 2;

                        tip.Left = x;
                        tip.Top = y;

                        tip.Text = sb.ToString();
                        if (!tip.Visible) tip.Visible = true;
                    }
                }

                PointValue?.Invoke(this, selectPoints.ToArray());
            }
        }

        public delegate void OnPointValue(object sender, UILineSelectPoint[] points);

        public event OnPointValue PointValue;
    }
}
