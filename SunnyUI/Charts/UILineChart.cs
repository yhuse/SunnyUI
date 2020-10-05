using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
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

        private int YAxisStart;
        private int YAxisEnd;
        private double YAxisInterval;
        private int XAxisStart;
        private int XAxisEnd;
        private double XAxisInterval;

        protected override void CalcData()
        {
            NeedDraw = false;
            if (LineOption == null || LineOption.Series == null || LineOption.Series.Count == 0) return;

            DrawOrigin = new Point(LineOption.Grid.Left, Height - LineOption.Grid.Bottom);
            DrawSize = new Size(Width - LineOption.Grid.Left - LineOption.Grid.Right,
                Height - LineOption.Grid.Top - LineOption.Grid.Bottom);

            if (DrawSize.Width <= 0 || DrawSize.Height <= 0) return;
            CalcAxises();

            foreach (var series in LineOption.Series.Values)
            {
                series.ClearPoints();

                for (int i = 0; i < series.XData.Count; i++)
                {
                    float x = (float)((series.XData[i] - XAxisStart * XAxisInterval) * 1.0f * DrawSize.Width / XAxisInterval / (XAxisEnd - XAxisStart));
                    float y = (float)((series.YData[i] - YAxisStart * YAxisInterval) * 1.0f * DrawSize.Height / YAxisInterval / (YAxisEnd - YAxisStart));
                    series.AddPoint(new PointF(DrawOrigin.X + x, DrawOrigin.Y - y));
                }
            }

            NeedDraw = true;
        }

        private void CalcAxises()
        {
            //Y轴
            double min = double.MaxValue;
            double max = double.MinValue;
            foreach (var series in LineOption.Series.Values)
            {
                if (series.DataCount > 0)
                {
                    min = Math.Min(min, series.YData.Min());
                    max = Math.Max(max, series.YData.Max());
                }
            }

            if (min > 0 && max > 0 && !LineOption.YAxis.Scale) min = 0;
            if (min < 0 && max < 0 && !LineOption.YAxis.Scale) max = 0;
            if (!LineOption.YAxis.MaxAuto) max = LineOption.YAxis.Max;
            if (!LineOption.YAxis.MinAuto) min = LineOption.YAxis.Min;

            if ((max - min).IsZero())
            {
                max = 100;
                min = 0;
            }

            UIChartHelper.CalcDegreeScale(min, max, LineOption.YAxis.SplitNumber,
                out int startY, out int endY, out double intervalY);

            YAxisStart = startY;
            YAxisEnd = endY;
            YAxisInterval = intervalY;

            //X轴
            min = double.MaxValue;
            max = double.MinValue;
            foreach (var series in LineOption.Series.Values)
            {
                min = Math.Min(min, series.XData.Min());
                max = Math.Max(max, series.XData.Max());
            }

            if (min > 0 && max > 0 && !LineOption.XAxis.Scale && LineOption.XAxisType == UIAxisType.Value) min = 0;
            if (min < 0 && max < 0 && !LineOption.XAxis.Scale && LineOption.XAxisType == UIAxisType.Value) max = 0;
            if (!LineOption.XAxis.MaxAuto) max = LineOption.XAxis.Max;
            if (!LineOption.XAxis.MinAuto) min = LineOption.XAxis.Min;

            if ((max - min).IsZero())
            {
                max = 100;
                min = 0;
            }

            if (LineOption.XAxisType == UIAxisType.Value || LineOption.XAxisType == UIAxisType.Category)
            {
                UIChartHelper.CalcDegreeScale(min, max, LineOption.XAxis.SplitNumber,
                   out int startX, out int endX, out double intervalX);
                XAxisStart = startX;
                XAxisEnd = endX;
                XAxisInterval = intervalX;
            }

            if (LineOption.XAxisType == UIAxisType.Time)
            {
                UIChartHelper.CalcDateTimeDegreeScale(min, max, LineOption.XAxis.SplitNumber,
                    out int startX, out int endX, out double intervalX);
                XAxisStart = startX;
                XAxisEnd = endX;
                XAxisInterval = intervalX;
            }
        }

        [Browsable(false)]
        private UILineOption LineOption
        {
            get
            {
                UIOption option = Option ?? EmptyOption;
                return (UILineOption)option;
            }
        }

        protected override void CreateEmptyOption()
        {
            if (emptyOption != null) return;

            UILineOption option = new UILineOption();
            option.Title = new UITitle();
            option.Title.Text = "SunnyUI";
            option.Title.SubText = "LineChart";

            var series = option.AddSeries(new UILineSeries("Line1"));
            series.Add(0, 1.2);
            series.Add(1.1, 2.2);
            series.Add(2.2, 3.2);
            series.Add(3.3, 4.2);
            series.Add(4.4, 3.2);
            series.Add(5.5, 2.2);
            series.Symbol = UILinePointSymbol.Square;
            series.SymbolSize = 4;
            series.SymbolLineWidth = 1;
            series.SymbolColor = Color.Red;

            series = option.AddSeries(new UILineSeries("Line2"));
            series.Add(0.3, 3.3);
            series.Add(1.3, 2.3);
            series.Add(2.3, 2.3);
            series.Add(3.3, 1.3);
            series.Add(4.3, 2.3);
            series.Add(5.3, 4.3);
            series.Symbol = UILinePointSymbol.Plus;
            series.SymbolSize = 4;
            series.SymbolLineWidth = 1;
            series.SymbolColor = Color.Red;

            option.XAxis.Name = "数值";
            option.YAxis.Name = "数值";

            emptyOption = option;
        }

        protected override void DrawOption(Graphics g)
        {
            if (LineOption == null) return;
            if (!NeedDraw) return;

            // if (BarOption.ToolTip != null && BarOption.ToolTip.AxisPointer.Type == UIAxisPointerType.Shadow) DrawToolTip(g);
            DrawAxis(g);
            DrawTitle(g, LineOption.Title);
            DrawSeries(g);
            // if (BarOption.ToolTip != null && BarOption.ToolTip.AxisPointer.Type == UIAxisPointerType.Line) DrawToolTip(g);
            DrawLegend(g, LineOption.Legend);
            DrawAxisScales(g);
        }

        private void DrawAxis(Graphics g)
        {
            if (YAxisStart >= 0) g.DrawLine(ChartStyle.ForeColor, DrawOrigin,
                new Point(DrawOrigin.X + DrawSize.Width, DrawOrigin.Y));
            if (YAxisEnd <= 0) g.DrawLine(ChartStyle.ForeColor, new Point(DrawOrigin.X, LineOption.Grid.Top),
                new Point(DrawOrigin.X + DrawSize.Width, LineOption.Grid.Top));

            g.DrawLine(ChartStyle.ForeColor, DrawOrigin, new Point(DrawOrigin.X, DrawOrigin.Y - DrawSize.Height));

            //X Tick
            if (LineOption.XAxis.AxisTick.Show)
            {
                float start = DrawOrigin.X;
                float DrawBarWidth = DrawSize.Width * 1.0f / (XAxisEnd - XAxisStart);
                for (int i = XAxisStart; i <= XAxisEnd; i++)
                {
                    g.DrawLine(ChartStyle.ForeColor, start, DrawOrigin.Y, start, DrawOrigin.Y + LineOption.XAxis.AxisTick.Length);

                    if (i != 0)
                    {
                        using (Pen pn = new Pen(ChartStyle.ForeColor))
                        {
                            pn.DashStyle = DashStyle.Dash;
                            pn.DashPattern = new float[] { 3, 3 };
                            g.DrawLine(pn, start, DrawOrigin.Y, start, LineOption.Grid.Top);
                        }
                    }
                    else
                    {
                        g.DrawLine(ChartStyle.ForeColor, start, DrawOrigin.Y, start, LineOption.Grid.Top);
                    }

                    start += DrawBarWidth;
                }
            }

            //X Label
            if (LineOption.XAxis.AxisLabel.Show)
            {
                float start = DrawOrigin.X;
                float DrawBarWidth = DrawSize.Width * 1.0f / (XAxisEnd - XAxisStart);
                int idx = 0;
                float wmax = 0;
                for (int i = XAxisStart; i <= XAxisEnd; i++)
                {
                    string label = LineOption.XAxis.AxisLabel.GetLabel(i * XAxisInterval, idx, LineOption.XAxisType);
                    SizeF sf = g.MeasureString(label, SubFont);
                    wmax = Math.Max(wmax, sf.Width);
                    g.DrawString(label, SubFont, ChartStyle.ForeColor, start - sf.Width / 2.0f,
                        DrawOrigin.Y + LineOption.XAxis.AxisTick.Length);
                    start += DrawBarWidth;
                }

                SizeF sfname = g.MeasureString(LineOption.XAxis.Name, SubFont);
                g.DrawString(LineOption.XAxis.Name, SubFont, ChartStyle.ForeColor,
                    DrawOrigin.X + (DrawSize.Width - sfname.Width) / 2.0f,
                    DrawOrigin.Y + LineOption.XAxis.AxisTick.Length + sfname.Height);
            }

            //Y Tick
            if (LineOption.YAxis.AxisTick.Show)
            {
                float start = DrawOrigin.Y;
                float DrawBarHeight = DrawSize.Height * 1.0f / (YAxisEnd - YAxisStart);
                for (int i = YAxisStart; i <= YAxisEnd; i++)
                {
                    g.DrawLine(ChartStyle.ForeColor, DrawOrigin.X, start, DrawOrigin.X - LineOption.YAxis.AxisTick.Length, start);

                    if (i != 0)
                    {
                        using (Pen pn = new Pen(ChartStyle.ForeColor))
                        {
                            pn.DashStyle = DashStyle.Dash;
                            pn.DashPattern = new float[] { 3, 3 };
                            g.DrawLine(pn, DrawOrigin.X, start, Width - LineOption.Grid.Right, start);
                        }
                    }
                    else
                    {
                        g.DrawLine(ChartStyle.ForeColor, DrawOrigin.X, start, Width - LineOption.Grid.Right, start);
                    }

                    start -= DrawBarHeight;
                }
            }

            //Y Label
            if (LineOption.YAxis.AxisLabel.Show)
            {
                float start = DrawOrigin.Y;
                float DrawBarHeight = DrawSize.Height * 1.0f / (YAxisEnd - YAxisStart);
                int idx = 0;
                float wmax = 0;
                for (int i = YAxisStart; i <= YAxisEnd; i++)
                {
                    string label = LineOption.YAxis.AxisLabel.GetLabel(i * YAxisInterval, idx);
                    SizeF sf = g.MeasureString(label, SubFont);
                    wmax = Math.Max(wmax, sf.Width);
                    g.DrawString(label, SubFont, ChartStyle.ForeColor, DrawOrigin.X - LineOption.YAxis.AxisTick.Length - sf.Width, start - sf.Height / 2.0f);
                    start -= DrawBarHeight;
                }

                SizeF sfname = g.MeasureString(LineOption.YAxis.Name, SubFont);
                int x = (int)(DrawOrigin.X - LineOption.YAxis.AxisTick.Length - wmax - sfname.Height);
                int y = (int)(LineOption.Grid.Top + (DrawSize.Height - sfname.Width) / 2);
                g.DrawString(LineOption.YAxis.Name, SubFont, ChartStyle.ForeColor, new Point(x, y),
                    new StringFormat() { Alignment = StringAlignment.Center }, 270);
            }
        }

        private void DrawSeries(Graphics g)
        {
            int idx = 0;
            foreach (var series in LineOption.Series.Values)
            {
                Color color = series.Color;
                if (!series.CustomColor) color = ChartStyle.GetColor(idx);

                using (Pen pen = new Pen(color, series.Width))
                {
                    g.SetHighQuality();
                    if (series.Smooth)
                        g.DrawCurve(pen, series.Points.ToArray());
                    else
                        g.DrawLines(pen, series.Points.ToArray());
                    g.SetDefaultQuality();
                }


                if (series.Symbol != UILinePointSymbol.None)
                {
                    using (Brush br = new SolidBrush(ChartStyle.BackColor))
                    using (Pen pn = new Pen(series.SymbolColor, series.SymbolLineWidth))
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
            foreach (var line in LineOption.YAxisScaleLines)
            {
                double ymin = YAxisStart * YAxisInterval;
                double ymax = YAxisEnd * YAxisInterval;
                float pos = (float)((line.Value - ymin) * (Height - LineOption.Grid.Top - LineOption.Grid.Bottom) / (ymax - ymin));
                pos = (Height - LineOption.Grid.Bottom - pos);
                using (Pen pn = new Pen(line.Color, line.Size))
                {
                    g.DrawLine(pn, DrawOrigin.X, pos, Width - LineOption.Grid.Right, pos);
                }

                SizeF sf = g.MeasureString(line.Name, SubFont);

                if (line.Left == UILeftAlignment.Left)
                    g.DrawString(line.Name, SubFont, line.Color, DrawOrigin.X + 4, pos - 2 - sf.Height);
                if (line.Left == UILeftAlignment.Center)
                    g.DrawString(line.Name, SubFont, line.Color, DrawOrigin.X + (Width - LineOption.Grid.Left - LineOption.Grid.Right - sf.Width) / 2, pos - 2 - sf.Height);
                if (line.Left == UILeftAlignment.Right)
                    g.DrawString(line.Name, SubFont, line.Color, Width - sf.Width - 4 - LineOption.Grid.Right, pos - 2 - sf.Height);
            }
        }

        private readonly List<UILineSelectPoint> selectPoints = new List<UILineSelectPoint>();
        private readonly List<UILineSelectPoint> selectPointsTemp = new List<UILineSelectPoint>();

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!NeedDraw) return;

            selectPointsTemp.Clear();
            foreach (var series in LineOption.Series.Values)
            {
                if (series.GetNearestPoint(e.Location, 4, out double x, out double y, out int index))
                {
                    UILineSelectPoint point = new UILineSelectPoint();
                    point.Name = series.Name;
                    point.Index = index;
                    point.X = x;
                    point.Y = y;
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
                foreach (var point in selectPointsTemp)
                {
                    selectPoints.Add(point);
                }

                PointValue?.Invoke(this, selectPoints);
            }
        }

        public delegate void OnPointValue(object sender, List<UILineSelectPoint> points);

        public event OnPointValue PointValue;
    }
}
