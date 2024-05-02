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
 * 文件名称: UIValve.cs
 * 文件说明: 阀门
 * 当前版本: V3.1
 * 创建日期: 2021-08-08
 *
 * 2021-08-07: V3.0.5 增加阀门控件
 * 2021-08-08: V3.0.5 完成四个方向的阀门
******************************************************************************/

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
    public sealed class UIValve : Control, IZoomScale
    {
        public UIValve()
        {
            SetStyleFlags();
            Width = Height = 60;
            rectColor = Color.Silver;
            fillColor = Color.White;
            valveColor = UIColor.Blue;
            Version = UIGlobal.Version;
            ZoomScaleDisabled = true;
        }

        /// <summary>
        /// 禁止控件跟随窗体缩放
        /// </summary>
        [DefaultValue(false), Category("SunnyUI"), Description("禁止控件跟随窗体缩放")]
        public bool ZoomScaleDisabled { get; set; }

        /// <summary>
        /// 控件缩放前在其容器里的位置
        /// </summary>
        [Browsable(false), DefaultValue(typeof(Rectangle), "0, 0, 0, 0")]
        public Rectangle ZoomScaleRect { get; set; }

        /// <summary>
        /// 设置控件缩放比例
        /// </summary>
        /// <param name="scale">缩放比例</param>
        public void SetZoomScale(float scale)
        {

        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Active = !Active;
        }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get;
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
        [DefaultValue(UIValveDirection.Left), Description("阀门方向"), Category("SunnyUI")]
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
            set
            {
                valveColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "Silver")]
        public Color RectColor
        {
            get => rectColor;
            set
            {
                rectColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public Color FillColor
        {
            get => fillColor;
            set
            {
                fillColor = value;
                Invalidate();
            }
        }

        [Description("管道尺寸"), Category("SunnyUI")]
        [DefaultValue(20)]
        public int PipeSize
        {
            get => pipeSize;
            set
            {
                pipeSize = value;
                Invalidate();
            }
        }

        int pipeSize = 20;

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int w = pipeSize / 2;
            Rectangle rect;
            Color[] colors;
            Point pt1, pt2, pt3, pt4;

            switch (direction)
            {
                case UIValveDirection.Left:
                    using (Bitmap bmp = new Bitmap(Width, Height))
                    using (Graphics g1 = bmp.Graphics())
                    using (LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, 0), new Point(w, 0), rectColor, fillColor))
                    {
                        g1.SetHighQuality();
                        g1.FillRectangle(lgb, new Rectangle(0, 0, w, Height * 2));
                        g1.SetDefaultQuality();
                        e.Graphics.DrawImage(bmp, new Rectangle(Width - w * 2 - 8, -5, w, Height + 50), new Rectangle(0, 5, w, Height + 20), GraphicsUnit.Pixel);
                    }

                    using (Bitmap bmp = new Bitmap(Width, Height))
                    using (Graphics g1 = bmp.Graphics())
                    using (LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, 0), new Point(w, 0), fillColor, rectColor))
                    {
                        g1.SetHighQuality();
                        g1.FillRectangle(lgb, new Rectangle(0, 0, w, Height * 2));
                        g1.SetDefaultQuality();
                        e.Graphics.DrawImage(bmp, new Rectangle(Width - w - 8, -5, w, Height + 50), new Rectangle(0, 5, w, Height + 20), GraphicsUnit.Pixel);
                    }

                    e.Graphics.DrawRectangle(RectColor, new Rectangle(Width - pipeSize - 8, 0, pipeSize - 1, Height - 1));

                    rect = new Rectangle(Width - pipeSize - 8 - 2, 4, pipeSize + 4, 6);
                    e.Graphics.FillRectangle(rectColor, rect);

                    rect = new Rectangle(Width - pipeSize - 8 - 2, Height - 4 - 6, pipeSize + 4, 6);
                    e.Graphics.FillRectangle(rectColor, rect);

                    rect = new Rectangle(Width - pipeSize - 8 - 14, Height / 2 - 2, 14, 4);
                    e.Graphics.FillRectangle(rectColor, rect);

                    rect = new Rectangle(Width - pipeSize - 8 - 14 - 10, Height / 2 - 14, 10, 27);
                    e.Graphics.FillRectangle(valveColor, rect);

                    colors = Color.White.GradientColors(valveColor, 14);
                    rect = new Rectangle(Width - pipeSize - 8 - 14 - 10, Height / 2 - 14 + 4, 10, 4);
                    e.Graphics.FillRectangle(colors[4], rect);
                    rect = new Rectangle(Width - pipeSize - 8 - 14 - 10, Height / 2 - 14 + 12, 10, 4);
                    e.Graphics.FillRectangle(colors[4], rect);
                    rect = new Rectangle(Width - pipeSize - 8 - 14 - 10, Height / 2 - 14 + 20, 10, 4);
                    e.Graphics.FillRectangle(colors[4], rect);

                    rect = new Rectangle(Width - pipeSize - 8 - 14 - 10, Height / 2 - 14, 10, 27);
                    e.Graphics.DrawRectangle(valveColor, rect);

                    pt1 = new Point(Width - pipeSize - 8 - 7, Height / 2 - 5);
                    pt2 = new Point(Width - pipeSize - 8 + 2, Height / 2 - 5 - 5);
                    pt3 = new Point(Width - pipeSize - 8 + 2, Height / 2 + 4 + 5);
                    pt4 = new Point(Width - pipeSize - 8 - 7, Height / 2 + 4);
                    e.Graphics.FillPolygon(rectColor, new PointF[] { pt1, pt2, pt3, pt4, pt1 });

                    break;
                case UIValveDirection.Bottom:
                    using (Bitmap bmp = new Bitmap(Width, Height))
                    using (Graphics g1 = bmp.Graphics())
                    using (LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, 0), new Point(0, w), rectColor, fillColor))
                    {
                        g1.SetHighQuality();
                        g1.FillRectangle(lgb, new Rectangle(0, 0, Width * 2, w));
                        g1.SetDefaultQuality();
                        e.Graphics.DrawImage(bmp, new Rectangle(-5, 8, Width + 50, w), new Rectangle(5, 0, Width + 20, w), GraphicsUnit.Pixel);
                    }

                    using (Bitmap bmp = new Bitmap(Width, Height))
                    using (Graphics g1 = bmp.Graphics())
                    using (LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, 0), new Point(0, w), fillColor, rectColor))
                    {
                        g1.SetHighQuality();
                        g1.FillRectangle(lgb, new Rectangle(0, 0, Width * 2, w));
                        g1.SetDefaultQuality();
                        e.Graphics.DrawImage(bmp, new Rectangle(-5, w + 8, Width + 50, w), new Rectangle(5, 0, Width + 20, w), GraphicsUnit.Pixel);
                    }

                    e.Graphics.DrawRectangle(RectColor, new Rectangle(0, 8, Width - 1, pipeSize - 1));

                    rect = new Rectangle(4, 8 - 2, 6, pipeSize + 4);
                    e.Graphics.FillRectangle(rectColor, rect);

                    rect = new Rectangle(Width - 4 - 6, 8 - 2, 6, pipeSize + 4);
                    e.Graphics.FillRectangle(rectColor, rect);

                    rect = new Rectangle(Width / 2 - 2, pipeSize + 8, 4, 14);
                    e.Graphics.FillRectangle(rectColor, rect);

                    rect = new Rectangle(Width / 2 - 14, pipeSize + 8 + 10 + 4, 27, 10);
                    e.Graphics.FillRectangle(valveColor, rect);

                    colors = Color.White.GradientColors(valveColor, 14);
                    rect = new Rectangle(Width / 2 - 14 + 4, pipeSize + 8 + 10 + 4, 4, 10);
                    e.Graphics.FillRectangle(colors[4], rect);
                    rect = new Rectangle(Width / 2 - 14 + 12, pipeSize + 8 + 10 + 4, 4, 10);
                    e.Graphics.FillRectangle(colors[4], rect);
                    rect = new Rectangle(Width / 2 - 14 + 20, pipeSize + 8 + 10 + 4, 4, 10);
                    e.Graphics.FillRectangle(colors[4], rect);

                    rect = new Rectangle(Width / 2 - 14, pipeSize + 8 + 10 + 4, 27, 10);
                    e.Graphics.DrawRectangle(valveColor, rect);

                    pt1 = new Point(Width / 2 - 5, pipeSize + 8 + 7);
                    pt2 = new Point(Width / 2 - 5 - 5, pipeSize + 8 - 2);
                    pt3 = new Point(Width / 2 + 4 + 5, pipeSize + 8 - 2);
                    pt4 = new Point(Width / 2 + 4, pipeSize + 8 + 7);
                    e.Graphics.FillPolygon(rectColor, new PointF[] { pt1, pt2, pt3, pt4, pt1 });
                    break;
                case UIValveDirection.Right:
                    using (Bitmap bmp = new Bitmap(Width, Height))
                    using (Graphics g1 = bmp.Graphics())
                    using (LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, 0), new Point(w, 0), rectColor, fillColor))
                    {
                        g1.SetHighQuality();
                        g1.FillRectangle(lgb, new Rectangle(0, 0, w, Height * 2));
                        g1.SetDefaultQuality();
                        e.Graphics.DrawImage(bmp, new Rectangle(8, -5, w, Height + 50), new Rectangle(0, 5, w, Height + 20), GraphicsUnit.Pixel);
                    }

                    using (Bitmap bmp = new Bitmap(Width, Height))
                    using (Graphics g1 = bmp.Graphics())
                    using (LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, 0), new Point(w, 0), fillColor, rectColor))
                    {
                        g1.SetHighQuality();
                        g1.FillRectangle(lgb, new Rectangle(0, 0, w, Height * 2));
                        g1.SetDefaultQuality();
                        e.Graphics.DrawImage(bmp, new Rectangle(w + 8, -5, w, Height + 50), new Rectangle(0, 5, w, Height + 20), GraphicsUnit.Pixel);
                    }

                    e.Graphics.DrawRectangle(RectColor, new Rectangle(8, 0, pipeSize - 1, Height - 1));

                    rect = new Rectangle(8 - 2, 4, pipeSize + 4, 6);
                    e.Graphics.FillRectangle(rectColor, rect);

                    rect = new Rectangle(8 - 2, Height - 4 - 6, pipeSize + 4, 6);
                    e.Graphics.FillRectangle(rectColor, rect);

                    rect = new Rectangle(pipeSize + 8, Height / 2 - 2, 14, 4);
                    e.Graphics.FillRectangle(rectColor, rect);

                    rect = new Rectangle(pipeSize + 8 + 10 + 4, Height / 2 - 14, 10, 27);
                    e.Graphics.FillRectangle(valveColor, rect);

                    colors = Color.White.GradientColors(valveColor, 14);
                    rect = new Rectangle(pipeSize + 8 + 10 + 4, Height / 2 - 14 + 4, 10, 4);
                    e.Graphics.FillRectangle(colors[4], rect);
                    rect = new Rectangle(pipeSize + 8 + 10 + 4, Height / 2 - 14 + 12, 10, 4);
                    e.Graphics.FillRectangle(colors[4], rect);
                    rect = new Rectangle(pipeSize + 8 + 10 + 4, Height / 2 - 14 + 20, 10, 4);
                    e.Graphics.FillRectangle(colors[4], rect);

                    rect = new Rectangle(pipeSize + 8 + 10 + 4, Height / 2 - 14, 10, 27);
                    e.Graphics.DrawRectangle(valveColor, rect);

                    pt1 = new Point(pipeSize + 8 + 7, Height / 2 - 5);
                    pt2 = new Point(pipeSize + 8 - 2, Height / 2 - 5 - 5);
                    pt3 = new Point(pipeSize + 8 - 2, Height / 2 + 4 + 5);
                    pt4 = new Point(pipeSize + 8 + 7, Height / 2 + 4);
                    e.Graphics.FillPolygon(rectColor, new PointF[] { pt1, pt2, pt3, pt4, pt1 });
                    break;
                case UIValveDirection.Top:
                    using (Bitmap bmp = new Bitmap(Width, Height))
                    using (Graphics g1 = bmp.Graphics())
                    using (LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, 0), new Point(0, w), rectColor, fillColor))
                    {
                        g1.SetHighQuality();
                        g1.FillRectangle(lgb, new Rectangle(0, 0, Width * 2, w));
                        g1.SetDefaultQuality();
                        e.Graphics.DrawImage(bmp, new Rectangle(-5, Height - w * 2 - 8, Width + 50, w), new Rectangle(5, 0, Width + 20, w), GraphicsUnit.Pixel);
                    }

                    using (Bitmap bmp = new Bitmap(Width, Height))
                    using (Graphics g1 = bmp.Graphics())
                    using (LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, 0), new Point(0, w), fillColor, rectColor))
                    {
                        g1.SetHighQuality();
                        g1.FillRectangle(lgb, new Rectangle(0, 0, Width * 2, w));
                        g1.SetDefaultQuality();
                        e.Graphics.DrawImage(bmp, new Rectangle(-5, Height - w - 8, Width + 50, w), new Rectangle(5, 0, Width + 20, w), GraphicsUnit.Pixel);
                    }

                    e.Graphics.DrawRectangle(RectColor, new Rectangle(0, Height - pipeSize - 8, Width - 1, pipeSize - 1));

                    rect = new Rectangle(4, Height - pipeSize - 8 - 2, 6, pipeSize + 4);
                    e.Graphics.FillRectangle(rectColor, rect);

                    rect = new Rectangle(Width - 4 - 6, Height - pipeSize - 8 - 2, 6, pipeSize + 4);
                    e.Graphics.FillRectangle(rectColor, rect);

                    rect = new Rectangle(Width / 2 - 2, Height - pipeSize - 8 - 14, 4, 14);
                    e.Graphics.FillRectangle(rectColor, rect);

                    rect = new Rectangle(Width / 2 - 14, Height - pipeSize - 8 - 14 - 10, 27, 10);
                    e.Graphics.FillRectangle(valveColor, rect);

                    colors = Color.White.GradientColors(valveColor, 14);
                    rect = new Rectangle(Width / 2 - 14 + 4, Height - pipeSize - 8 - 14 - 10, 4, 10);
                    e.Graphics.FillRectangle(colors[4], rect);
                    rect = new Rectangle(Width / 2 - 14 + 12, Height - pipeSize - 8 - 14 - 10, 4, 10);
                    e.Graphics.FillRectangle(colors[4], rect);
                    rect = new Rectangle(Width / 2 - 14 + 20, Height - pipeSize - 8 - 14 - 10, 4, 10);
                    e.Graphics.FillRectangle(colors[4], rect);

                    rect = new Rectangle(Width / 2 - 14, Height - pipeSize - 8 - 14 - 10, 27, 10);
                    e.Graphics.DrawRectangle(valveColor, rect);

                    pt1 = new Point(Width / 2 - 5, Height - pipeSize - 8 - 7);
                    pt2 = new Point(Width / 2 - 5 - 5, Height - pipeSize - 8 + 2);
                    pt3 = new Point(Width / 2 + 4 + 5, Height - pipeSize - 8 + 2);
                    pt4 = new Point(Width / 2 + 4, Height - pipeSize - 8 - 7);
                    e.Graphics.FillPolygon(rectColor, new PointF[] { pt1, pt2, pt3, pt4, pt1 });
                    break;
            }
        }
    }
}
