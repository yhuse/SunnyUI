using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    [DefaultProperty("Active")]
    [DefaultEvent("ActiveChanged")]
    public class UIValve : Control
    {
        public UIValve()
        {
            SetStyleFlags();
            Width = Height = 60;
            rectColor = Color.Silver;
            fillColor = Color.White;
            valveColor = UIColor.Blue;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Active = !Active;
        }

        private bool active;

        [DefaultValue(false), Description("是否滚动"), Category("SunnyUI")]
        public bool Active
        {
            get => active;
            set
            {
                if (active != value)
                {
                    active = value;
                    ActiveChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler ActiveChanged;

        private void SetStyleFlags(bool supportTransparent = true, bool selectable = true, bool resizeRedraw = false)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            if (supportTransparent) SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            if (selectable) SetStyle(ControlStyles.Selectable, true);
            if (resizeRedraw) SetStyle(ControlStyles.ResizeRedraw, true);
            base.DoubleBuffered = true;
            UpdateStyles();
        }

        public enum UIValveDirection
        {
            Left,
            Top,
            Right,
            Bottom
        }

        private UIValveDirection direction = UIValveDirection.Left;
        public UIValveDirection Direction
        {
            get => direction;
            set
            {
                direction = value;
                Invalidate();
            }
        }

        private Color rectColor;
        private Color fillColor;
        private Color valveColor;

        /// <summary>
        /// 阀门颜色
        /// </summary>
        [Description("阀门颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color ValveColor
        {
            get => valveColor;
            set => valveColor = value;
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "Silver")]
        public Color RectColor
        {
            get => rectColor;
            set => rectColor = value;
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public Color FillColor
        {
            get => fillColor;
            set => fillColor = value;
        }

        [Description("管道尺寸"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public int PipeSize
        {
            get => pipeSize;
            set => pipeSize = value;
        }

        int pipeSize = 24;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            switch (direction)
            {
                case UIValveDirection.Left:
                    int w = pipeSize / 2;
                    using (Bitmap bmp = new Bitmap(Width, Height))
                    using (Graphics g1 = bmp.Graphics())
                    using (LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, 0),
                        new Point(w, 0),
                        rectColor,
                        fillColor))
                    {
                        g1.SetHighQuality();
                        g1.FillRectangle(lgb, new Rectangle(0, 0, w, Height * 2));
                        g1.SetDefaultQuality();
                        e.Graphics.DrawImage(bmp, new Rectangle(Width - w * 2 - 8, -5, w, Height + 50), new Rectangle(0, 5, w, Height + 20), GraphicsUnit.Pixel);
                    }

                    using (Bitmap bmp = new Bitmap(Width, Height))
                    using (Graphics g1 = bmp.Graphics())
                    using (LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, 0),
                        new Point(w, 0),
                           fillColor,
                        rectColor))
                    {
                        g1.SetHighQuality();
                        g1.FillRectangle(lgb, new Rectangle(0, 0, w, Height * 2));
                        g1.SetDefaultQuality();

                        e.Graphics.DrawImage(bmp, new Rectangle(Width - w - 8, -5, w, Height + 50), new Rectangle(0, 5, w, Height + 20), GraphicsUnit.Pixel);
                    }

                    e.Graphics.DrawRectangle(RectColor, new Rectangle(Width - pipeSize - 8, 0, pipeSize - 1, Height - 1));

                    Rectangle rect = new Rectangle(Width - pipeSize - 8 - 2, 4, pipeSize + 4, 6);
                    e.Graphics.FillRectangle(rectColor, rect);

                    rect = new Rectangle(Width - pipeSize - 8 - 2, Height - 4 - 6, pipeSize + 4, 6);
                    e.Graphics.FillRectangle(rectColor, rect);

                    rect = new Rectangle(Width - pipeSize - 8 - 14, Height / 2 - 2, 14, 4);
                    e.Graphics.FillRectangle(rectColor, rect);

                    rect = new Rectangle(Width - pipeSize - 8 - 14 - 10, Height / 2 - 14, 10, 27);
                    e.Graphics.FillRectangle(valveColor, rect);

                    Color[] colors = GDIEx.GradientColors(Color.White, valveColor, 14);
                    rect = new Rectangle(Width - pipeSize - 8 - 14 - 10, Height / 2 - 14 + 4, 10, 4);
                    e.Graphics.FillRectangle(colors[4], rect);
                    rect = new Rectangle(Width - pipeSize - 8 - 14 - 10, Height / 2 - 14 + 12, 10, 4);
                    e.Graphics.FillRectangle(colors[4], rect);
                    rect = new Rectangle(Width - pipeSize - 8 - 14 - 10, Height / 2 - 14 + 20, 10, 4);
                    e.Graphics.FillRectangle(colors[4], rect);

                    rect = new Rectangle(Width - pipeSize - 8 - 14 - 10, Height / 2 - 14, 10, 27);
                    e.Graphics.DrawRectangle(valveColor, rect);

                    Point pt1 = new Point(Width - pipeSize - 8 - 7, Height / 2 - 5);
                    Point pt2 = new Point(Width - pipeSize - 8 + 2, Height / 2 - 5 - 5);
                    Point pt3 = new Point(Width - pipeSize - 8 + 2, Height / 2 + 4 + 5);
                    Point pt4 = new Point(Width - pipeSize - 8 - 7, Height / 2 + 4);
                    e.Graphics.FillPolygon(rectColor, new PointF[] { pt1, pt2, pt3, pt4, pt1 });

                    break;
                case UIValveDirection.Top:
                    break;
                case UIValveDirection.Right:
                    break;
                case UIValveDirection.Bottom:
                    break;
                default:
                    break;
            }
        }
    }
}
