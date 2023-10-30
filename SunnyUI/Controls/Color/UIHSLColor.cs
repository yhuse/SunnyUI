using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public struct HSLColor
    {
        private double m_hue;
        private double m_saturation;
        private double m_lightness;
        // http://en.wikipedia.org/wiki/HSL_color_space

        public double Hue
        {
            get { return m_hue; }
            set { m_hue = value; }
        }

        public double Saturation
        {
            get { return m_saturation; }
            set { m_saturation = value; }
        }

        public double Lightness
        {
            get { return m_lightness; }
            set
            {
                m_lightness = value;
                if (m_lightness < 0)
                    m_lightness = 0;
                if (m_lightness > 1)
                    m_lightness = 1;
            }
        }

        public HSLColor(double hue, double saturation, double lightness)
        {
            m_hue = Math.Min(360, hue);
            m_saturation = Math.Min(1, saturation);
            m_lightness = Math.Min(1, lightness);
        }

        public HSLColor(Color color)
        {
            m_hue = 0;
            m_saturation = 1;
            m_lightness = 1;
            FromRGB(color);
        }

        public Color Color
        {
            get { return ToRGB(); }
            set { FromRGB(value); }
        }

        private void FromRGB(Color cc)
        {
            double r = (double)cc.R / 255d;
            double g = (double)cc.G / 255d;
            double b = (double)cc.B / 255d;

            double min = Math.Min(Math.Min(r, g), b);
            double max = Math.Max(Math.Max(r, g), b);
            // calulate hue according formula given in
            // "Conversion from RGB to HSL or HSV"
            m_hue = 0;
            if (min != max)
            {
                if (r == max && g >= b)
                {
                    m_hue = 60 * ((g - b) / (max - min)) + 0;
                }
                else
                if (r == max && g < b)
                {
                    m_hue = 60 * ((g - b) / (max - min)) + 360;
                }
                else
                if (g == max)
                {
                    m_hue = 60 * ((b - r) / (max - min)) + 120;
                }
                else
                if (b == max)
                {
                    m_hue = 60 * ((r - g) / (max - min)) + 240;
                }
            }
            // find lightness
            m_lightness = (min + max) / 2;

            // find saturation
            if (m_lightness == 0 || min == max)
                m_saturation = 0;
            else
            if (m_lightness > 0 && m_lightness <= 0.5)
                m_saturation = (max - min) / (2 * m_lightness);
            else
            if (m_lightness > 0.5)
                m_saturation = (max - min) / (2 - 2 * m_lightness);
        }

        private Color ToRGB()
        {
            // convert to RGB according to
            // "Conversion from HSL to RGB"

            double r = m_lightness;
            double g = m_lightness;
            double b = m_lightness;
            if (m_saturation == 0)
                return Color.FromArgb(255, (int)(r * 255), (int)(g * 255), (int)(b * 255));

            double q = 0;
            if (m_lightness < 0.5)
                q = m_lightness * (1 + m_saturation);
            else
                q = m_lightness + m_saturation - (m_lightness * m_saturation);
            double p = 2 * m_lightness - q;
            double hk = m_hue / 360;

            // r,g,b colors
            double[] tc = new double[3] { hk + (1d / 3d), hk, hk - (1d / 3d) };
            double[] colors = new double[3] { 0, 0, 0 };

            for (int color = 0; color < colors.Length; color++)
            {
                if (tc[color] < 0)
                    tc[color] += 1;
                if (tc[color] > 1)
                    tc[color] -= 1;

                if (tc[color] < (1d / 6d))
                    colors[color] = p + ((q - p) * 6 * tc[color]);
                else
                if (tc[color] >= (1d / 6d) && tc[color] < (1d / 2d))
                    colors[color] = q;
                else
                if (tc[color] >= (1d / 2d) && tc[color] < (2d / 3d))
                    colors[color] = p + ((q - p) * 6 * (2d / 3d - tc[color]));
                else
                    colors[color] = p;

                colors[color] *= 255; // convert to value expected by Color
            }
            return Color.FromArgb(255, (int)colors[0], (int)colors[1], (int)colors[2]);
        }

        public bool Equals(HSLColor color)
        {
            return Hue == color.Hue && Lightness == color.Lightness && Saturation == color.Saturation;
        }

        public override string ToString()
        {
            string s = string.Format("HSL({0:f2}, {1:f2}, {2:f2})", Hue, Saturation, Lightness);
            return s;
        }
    }

    public static class UIColorUtil
    {
        public static Rectangle Rect(RectangleF rf)
        {
            Rectangle r = new Rectangle();
            r.X = (int)rf.X;
            r.Y = (int)rf.Y;
            r.Width = (int)rf.Width;
            r.Height = (int)rf.Height;
            return r;
        }

        public static RectangleF Rect(Rectangle r)
        {
            RectangleF rf = new RectangleF();
            rf.X = r.X;
            rf.Y = r.Y;
            rf.Width = r.Width;
            rf.Height = r.Height;
            return rf;
        }

        public static Point Point(PointF pf)
        {
            return new Point((int)pf.X, (int)pf.Y);
        }

        public static PointF Center(RectangleF r)
        {
            PointF center = r.Location;
            center.X += r.Width / 2;
            center.Y += r.Height / 2;
            return center;
        }

        public static void DrawFrame(Graphics dc, RectangleF r, float cornerRadius, Color color)
        {
            using Pen pen = new Pen(color);
            if (cornerRadius <= 0)
            {
                dc.DrawRectangle(pen, Rect(r));
                return;
            }

            cornerRadius = (float)Math.Min(cornerRadius, Math.Floor(r.Width) - 2);
            cornerRadius = (float)Math.Min(cornerRadius, Math.Floor(r.Height) - 2);

            using GraphicsPath path = new GraphicsPath();
            path.AddArc(r.X, r.Y, cornerRadius, cornerRadius, 180, 90);
            path.AddArc(r.Right - cornerRadius, r.Y, cornerRadius, cornerRadius, 270, 90);
            path.AddArc(r.Right - cornerRadius, r.Bottom - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path.AddArc(r.X, r.Bottom - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path.CloseAllFigures();
            dc.DrawPath(pen, path);
        }

        public static void Draw2ColorBar(Graphics dc, RectangleF r, Orientation orientation, Color c1, Color c2)
        {
            RectangleF lr1 = r;
            float angle = 0;

            if (orientation == Orientation.Vertical)
                angle = 270;
            if (orientation == Orientation.Horizontal)
                angle = 0;

            if (lr1.Height > 0 && lr1.Width > 0)
            {
                using LinearGradientBrush lb1 = new LinearGradientBrush(lr1, c1, c2, angle, false);
                dc.FillRectangle(lb1, lr1);
            }
        }

        public static void Draw3ColorBar(Graphics dc, RectangleF r, Orientation orientation, Color c1, Color c2, Color c3)
        {
            // to draw a 3 color bar 2 gradient brushes are needed
            // one from c1 - c2 and c2 - c3
            RectangleF lr1 = r;
            RectangleF lr2 = r;
            float angle = 0;

            if (orientation == Orientation.Vertical)
            {
                angle = 270;

                lr1.Height = lr1.Height / 2;
                lr2.Height = r.Height - lr1.Height;
                lr2.Y += lr1.Height;
            }
            if (orientation == Orientation.Horizontal)
            {
                angle = 0;

                lr1.Width = lr1.Width / 2;
                lr2.Width = r.Width - lr1.Width;
                lr1.X = lr2.Right;
            }

            if (lr1.Height > 0 && lr1.Width > 0)
            {
                using LinearGradientBrush lb2 = new LinearGradientBrush(lr2, c1, c2, angle, false);
                using LinearGradientBrush lb1 = new LinearGradientBrush(lr1, c2, c3, angle, false);

                dc.FillRectangle(lb1, lr1);
                dc.FillRectangle(lb2, lr2);
            }

            // with some sizes the first pixel in the gradient rectangle shows the opposite color
            // this is a workaround for that problem
            if (orientation == Orientation.Vertical)
            {
                using Pen pc2 = new Pen(c2, 1);
                using Pen pc3 = new Pen(c3, 1);
                dc.DrawLine(pc3, lr1.Left, lr1.Top, lr1.Right - 1, lr1.Top);
                dc.DrawLine(pc2, lr2.Left, lr2.Top, lr2.Right - 1, lr2.Top);
            }

            if (orientation == Orientation.Horizontal)
            {
                using Pen pc1 = new Pen(c1, 1);
                using Pen pc2 = new Pen(c2, 1);
                using Pen pc3 = new Pen(c3, 1);
                dc.DrawLine(pc1, lr2.Left, lr2.Top, lr2.Left, lr2.Bottom - 1);
                dc.DrawLine(pc2, lr2.Right, lr2.Top, lr2.Right, lr2.Bottom - 1);
                dc.DrawLine(pc3, lr1.Right, lr1.Top, lr1.Right, lr1.Bottom - 1);
            }
        }
    }

    internal class SelectorImages
    {
        public enum eIndexes
        {
            Right,
            Left,
            Up,
            Down,
            Donut,
        }

        private static ImageList m_imageList;

        public static ImageList ImageList()
        {
            if (m_imageList == null)
                m_imageList = ImageEx.GetToolbarImageList(Properties.Resources.colorbarIndicators, new Size(12, 12), Color.Magenta);
            return m_imageList;
        }

        public static Image Image(eIndexes index)
        {
            return ImageList().Images[(int)index];
        }
    }

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}