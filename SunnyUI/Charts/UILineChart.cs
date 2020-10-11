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
            if (LineOption == null || LineOption.Series == null || LineOption.Series.Count == 0) return;

            DrawOrigin = new Point(LineOption.Grid.Left, Height - LineOption.Grid.Bottom);
            DrawSize = new Size(Width - LineOption.Grid.Left - LineOption.Grid.Right,
                Height - LineOption.Grid.Top - LineOption.Grid.Bottom);

            if (DrawSize.Width <= 0 || DrawSize.Height <= 0) return;
            CalcAxises();

            foreach (var series in LineOption.Series.Values)
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
            if (LineOption.XAxisType == UIAxisType.DateTime)
                XScale = new UIDateScale();
            else
                XScale = new UILinearScale();

            YScale = new UILinearScale();

            //Y轴
            {
                LineOption.GetAllDataYRange(out double min, out double max);
                if (min > 0 && max > 0 && !LineOption.YAxis.Scale) min = 0;
                if (min < 0 && max < 0 && !LineOption.YAxis.Scale) max = 0;
                YScale.SetRange(min, max);
                YScale.AxisChange();
                if (!LineOption.YAxis.MaxAuto) YScale.Max = LineOption.YAxis.Max;
                if (!LineOption.YAxis.MinAuto) YScale.Min = LineOption.YAxis.Min;
                YLabels = YScale.CalcLabels();
            }

            //X轴
            {
                LineOption.GetAllDataXRange(out double min, out double max);
                XScale.SetRange(min, max);
                XScale.AxisChange();
                if (!LineOption.XAxis.MaxAuto) XScale.Max = LineOption.XAxis.Max;
                if (!LineOption.XAxis.MinAuto) XScale.Min = LineOption.XAxis.Min;
                XLabels = XScale.CalcLabels();
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
            if (LineOption == null) return;
            if (!NeedDraw) return;

            // if (BarOption.ToolTip != null && BarOption.ToolTip.AxisPointer.Type == UIAxisPointerType.Shadow) DrawToolTip(g);
            DrawAxis(g);
            DrawTitle(g, LineOption.Title);
            DrawAxisScales(g);
            DrawSeries(g);
            // if (BarOption.ToolTip != null && BarOption.ToolTip.AxisPointer.Type == UIAxisPointerType.Line) DrawToolTip(g);
            DrawLegend(g, LineOption.Legend);
        }

        private void DrawAxis(Graphics g)
        {
            g.DrawRectangle(ChartStyle.ForeColor, LineOption.Grid.Left, LineOption.Grid.Top, DrawSize.Width, DrawSize.Height);
            if (XScale == null || YScale == null) return;

            //X Tick
            if (LineOption.XAxis.AxisTick.Show)
            {
                float[] xlabels = XScale.CalcXPixels(XLabels, DrawOrigin.X, DrawSize.Width);
                for (int i = 0; i < xlabels.Length; i++)
                {
                    float x = xlabels[i];
                    if (LineOption.XAxis.AxisLabel.Show)
                    {
                        string label;
                        if (LineOption.XAxisType == UIAxisType.DateTime)
                            label = new DateTimeInt64(XLabels[i]).ToString(XScale.Format);
                        else
                            label = XLabels[i].ToString(XScale.Format);

                        SizeF sf = g.MeasureString(label, SubFont);
                        g.DrawString(label, SubFont, ChartStyle.ForeColor, x - sf.Width / 2.0f, DrawOrigin.Y + LineOption.XAxis.AxisTick.Length);
                    }

                    if (x.Equals(DrawOrigin.X)) continue;
                    if (x.Equals(DrawOrigin.X + DrawSize.Width)) continue;

                    using (Pen pn = new Pen(ChartStyle.ForeColor))
                    {
                        pn.DashStyle = DashStyle.Dash;
                        pn.DashPattern = new float[] { 3, 3 };
                        g.DrawLine(pn, x, DrawOrigin.Y, x, LineOption.Grid.Top);
                    }
                }

                SizeF sfname = g.MeasureString(LineOption.XAxis.Name, SubFont);
                g.DrawString(LineOption.XAxis.Name, SubFont, ChartStyle.ForeColor,
                    DrawOrigin.X + (DrawSize.Width - sfname.Width) / 2.0f,
                    DrawOrigin.Y + LineOption.XAxis.AxisTick.Length + sfname.Height);
            }

            //Y Tick
            if (LineOption.YAxis.AxisTick.Show)
            {
                float[] ylabels = YScale.CalcYPixels(YLabels, DrawOrigin.Y, DrawSize.Height);
                float wmax = 0;
                for (int i = 0; i < ylabels.Length; i++)
                {
                    float y = ylabels[i];
                    if (LineOption.YAxis.AxisLabel.Show)
                    {
                        string label = YLabels[i].ToString(YScale.Format);
                        SizeF sf = g.MeasureString(label, SubFont);
                        wmax = Math.Max(wmax, sf.Width);
                        g.DrawString(label, SubFont, ChartStyle.ForeColor, DrawOrigin.X - LineOption.YAxis.AxisTick.Length - sf.Width, y - sf.Height / 2.0f);
                    }

                    if (y.Equals(DrawOrigin.Y)) continue;
                    if (y.Equals(DrawOrigin.X - DrawSize.Height)) continue;

                    using (Pen pn = new Pen(ChartStyle.ForeColor))
                    {
                        pn.DashStyle = DashStyle.Dash;
                        pn.DashPattern = new float[] { 3, 3 };
                        g.DrawLine(pn, DrawOrigin.X, y, Width - LineOption.Grid.Right, y);
                    }
                }


                SizeF sfname = g.MeasureString(LineOption.YAxis.Name, SubFont);
                int xx = (int)(DrawOrigin.X - LineOption.YAxis.AxisTick.Length - wmax - sfname.Height);
                int yy = (int)(LineOption.Grid.Top + (DrawSize.Height - sfname.Width) / 2);
                g.DrawString(LineOption.YAxis.Name, SubFont, ChartStyle.ForeColor, new Point(xx, yy),
                    new StringFormat() { Alignment = StringAlignment.Center }, 270);
            }
        }

        private void DrawSeries(Graphics g)
        {
            if (YScale == null) return;

            int idx = 0;
            if (LineOption.GreaterWarningArea == null && LineOption.LessWarningArea == null)
            {
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

                foreach (var series in LineOption.Series.Values)
                {
                    Color color = series.Color;
                    if (!series.CustomColor) color = ChartStyle.GetColor(idx);

                    using (Pen pen = new Pen(color, series.Width))
                    {
                        Graphics graphics = bmp.Graphics();
                        graphics.SetHighQuality();
                        if (series.Smooth)
                            graphics.DrawCurve(pen, series.Points.ToArray());
                        else
                            graphics.DrawLines(pen, series.Points.ToArray());
                        graphics.SetDefaultQuality();
                    }

                    if (LineOption.GreaterWarningArea != null)
                    {
                        using (Pen pen = new Pen(LineOption.GreaterWarningArea.Color, series.Width))
                        {
                            Graphics graphics = bmpGreater.Graphics();
                            graphics.SetHighQuality();
                            if (series.Smooth)
                                graphics.DrawCurve(pen, series.Points.ToArray());
                            else
                                graphics.DrawLines(pen, series.Points.ToArray());
                            graphics.SetDefaultQuality();
                        }
                    }

                    if (LineOption.LessWarningArea != null)
                    {
                        using (Pen pen = new Pen(LineOption.LessWarningArea.Color, series.Width))
                        {
                            Graphics graphics = bmpLess.Graphics();
                            graphics.SetHighQuality();
                            if (series.Smooth)
                                graphics.DrawCurve(pen, series.Points.ToArray());
                            else
                                graphics.DrawLines(pen, series.Points.ToArray());
                            graphics.SetDefaultQuality();
                        }
                    }

                    idx++;
                }

                if (LineOption.GreaterWarningArea != null)
                {
                    wTop = YScale.CalcYPixel(LineOption.GreaterWarningArea.Value, DrawOrigin.Y, DrawSize.Height);
                    wTop = DrawOrigin.Y - wTop;
                    g.DrawImage(bmpGreater, new Rectangle(0, 0, Width, (int)wTop),
                        new Rectangle(0, 0, Width, (int)wTop), GraphicsUnit.Pixel);
                }

                if (LineOption.LessWarningArea != null)
                {
                    wBottom = YScale.CalcYPixel(LineOption.LessWarningArea.Value, DrawOrigin.Y, DrawSize.Height);
                    wBottom = DrawOrigin.Y - wBottom;
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
            foreach (var series in LineOption.Series.Values)
            {
                Color color = series.Color;
                if (!series.CustomColor) color = ChartStyle.GetColor(idx);

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
            if (YScale == null) return;

            foreach (var line in LineOption.YAxisScaleLines)
            {
                float pos = YScale.CalcYPixel(line.Value, DrawOrigin.Y, DrawSize.Height);
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
                    sb.Append(LineOption.XAxis.Name + ": ");
                    if (LineOption.XAxisType == UIAxisType.DateTime)
                        sb.Append(new DateTimeInt64(point.X).ToString(LineOption.XAxis.AxisLabel.DateTimeFormat));
                    else
                        sb.Append(point.X.ToString("F" + LineOption.XAxis.AxisLabel.DecimalCount));
                    sb.Append('\n');
                    sb.Append(LineOption.YAxis.Name + ": " + point.Y.ToString("F" + LineOption.YAxis.AxisLabel.DecimalCount));
                    idx++;
                }

                if (LineOption.ToolTip.Visible)
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
                        if (e.Location.X + 15 + tip.Width > Width - LineOption.Grid.Right)
                            x = e.Location.X - tip.Width - 2;
                        if (e.Location.Y + 20 + tip.Height > Height - LineOption.Grid.Bottom)
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
