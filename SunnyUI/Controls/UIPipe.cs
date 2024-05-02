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
 * 文件名称: UIPipe.cs
 * 文件说明: 管道
 * 当前版本: V3.1
 * 创建日期: 2021-07-26
 *
 * 2020-07-26: V3.0.5 增加管道控件
 * 2021-07-29: V3.0.5 优化管道连接
 * 2023-02-24: V3.3.2 修复了管道宽度调大后水流不显示的问题
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public class UIPipe : UIControl
    {
        public UIPipe()
        {
            SetStyleFlags();
            ShowText = false;
            rectColor = Color.Silver;
            fillColor = Color.White;
            StyleCustomMode = true;
            Style = UIStyle.Custom;
            Width = 200;
            Height = 16;
            ZoomScaleDisabled = true;
        }

        private ConcurrentDictionary<UIPipe, Bitmap> linked = new ConcurrentDictionary<UIPipe, Bitmap>();

        public void Link(UIPipe pipe)
        {
            if (linked.NotContainsKey(pipe))
            {
                linked.TryAdd(pipe, null);
            }
        }

        private UILine.LineDirection direction = UILine.LineDirection.Horizontal;

        [DefaultValue(UILine.LineDirection.Horizontal)]
        [Description("线条方向"), Category("SunnyUI")]
        public UILine.LineDirection Direction
        {
            get => direction;
            set
            {
                if (direction != value)
                {
                    Radius = 0;
                    direction = value;

                    if (direction == UILine.LineDirection.Horizontal)
                    {
                        Width = 200;
                        Height = 16;
                    }

                    if (direction == UILine.LineDirection.Vertical)
                    {
                        Width = 16;
                        Height = 200;
                    }

                    Invalidate();
                }
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
            set => SetRectColor(value);
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        [Description("流动开启"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool Active { get; set; }

        [Description("流动方向"), Category("SunnyUI")]
        [DefaultValue(UIFlowDirection.Forward)]
        public UIFlowDirection FlowDirection
        {
            get;
            set;
        }

        public int flowSpeed = 6;
        [Description("流动速度"), Category("SunnyUI")]
        [DefaultValue(6)]
        public int FlowSpeed
        {
            get => flowSpeed;
            set => flowSpeed = Math.Max(value, 1);
        }

        [Description("流动填充块颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "Purple")]
        public Color FlowColor { get; set; } = Color.Purple;

        private int flowColorAlpha = 200;
        [Description("流动填充块颜色透明度"), Category("SunnyUI")]
        [DefaultValue(200)]
        public int FlowColorAlpha
        {
            get => flowColorAlpha;
            set => flowColorAlpha = Math.Min(Math.Max(0, value), 255);
        }

        public int flowSize = 35;
        [Description("流动填充块大小"), Category("SunnyUI")]
        [DefaultValue(35)]
        public int FlowSize
        {
            get => flowSize;
            set => flowSize = Math.Max(value, Math.Min(Width, Height) + 2);
        }

        public int flowInterval = 22;
        [Description("流动填充块间隔"), Category("SunnyUI")]
        [DefaultValue(22)]
        public int FlowInterval
        {
            get => flowInterval;
            set => flowInterval = Math.Max(value, 10);
        }

        public enum UIFlowDirection
        {
            /// <summary>
            /// 正向
            /// </summary>
            Forward,

            /// <summary>
            /// 反向
            /// </summary>
            Reverse
        }

        Color[] colors;

        /// <summary>
        /// 绘制边框颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintRect(Graphics g, GraphicsPath path)
        {
            base.OnPaintRect(g, path);

            if (Direction == UILine.LineDirection.Horizontal) return;
            if (Height < Width * 2 + 4) return;

            if (RadiusSides.HasFlag(UICornerRadiusSides.LeftTop) &&
                    !RadiusSides.HasFlag(UICornerRadiusSides.RightTop))
            {
                for (int i = 0; i < Width; i++)
                {
                    g.DrawLine(colors[i], Width - 1, i, Width, i);
                }
            }

            if (!RadiusSides.HasFlag(UICornerRadiusSides.LeftTop) &&
                RadiusSides.HasFlag(UICornerRadiusSides.RightTop))
            {
                for (int i = 0; i < Width; i++)
                {
                    int idx = Width - i;
                    if (idx >= 0 && idx < Width)
                        g.DrawLine(colors[idx], 0, i - 1, 1, i - 1);
                }
            }

            if (RadiusSides.HasFlag(UICornerRadiusSides.LeftBottom) &&
                !RadiusSides.HasFlag(UICornerRadiusSides.RightBottom))
            {
                for (int i = 0; i < Width; i++)
                {
                    g.DrawLine(colors[i], Width - 1, Height - i - 1, Width, Height - i - 1);
                }
            }

            if (!RadiusSides.HasFlag(UICornerRadiusSides.LeftBottom) &&
                RadiusSides.HasFlag(UICornerRadiusSides.RightBottom))
            {
                for (int i = 0; i < Width; i++)
                {
                    int idx = Width - i;
                    if (idx >= 0 && idx < Width)
                        g.DrawLine(colors[idx], 0, Height - i, 1, Height - i);
                }
            }

            PaintLinkedRect(g);
        }

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            base.OnPaintFill(g, path);
            if (colors == null || colors.Length != Width)
                colors = new Color[Width];

            //水平的
            if (Direction == UILine.LineDirection.Horizontal)
            {
                if (Width > Height)
                {
                    if (Radius != Height)
                        Radius = Height;
                }
                else
                {
                    Radius = 0;
                }

                int h = Height.Div(2) + Height.Mod(2);
                using (Bitmap bmp = new Bitmap(Width, Height))
                using (Graphics g1 = bmp.Graphics())
                using (LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, 0),
                    new Point(0, h),
                    rectColor,
                    fillColor))
                {
                    g1.SetHighQuality();
                    g1.FillPath(lgb, path);
                    g1.SetDefaultQuality();
                    g.DrawImage(bmp, new Rectangle(0, 0, Width, h), new Rectangle(0, 0, Width, h), GraphicsUnit.Pixel);
                }

                using (Bitmap bmp = new Bitmap(Width, Height))
                using (Graphics g1 = bmp.Graphics())
                using (LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, h - 1),
                    new Point(0, Height),
                    fillColor,
                    rectColor))
                {
                    g1.SetHighQuality();
                    g1.FillPath(lgb, path);
                    g1.SetDefaultQuality();

                    g.DrawImage(bmp, new Rectangle(0, h, Width, Height - h), new Rectangle(0, h, Width, Height - h), GraphicsUnit.Pixel);
                }
            }

            //垂直的
            if (Direction == UILine.LineDirection.Vertical)
            {
                if (Height > Width)
                {
                    if (Radius != Width)
                        Radius = Width;
                }
                else
                {
                    Radius = 0;
                }

                int w = Width.Div(2) + Width.Mod(2);
                using (Bitmap bmp = new Bitmap(Width, Height))
                using (Graphics g1 = bmp.Graphics())
                using (LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, 0),
                    new Point(w, 0),
                    rectColor,
                    fillColor))
                {
                    g1.SetHighQuality();
                    g1.FillPath(lgb, path);
                    g1.SetDefaultQuality();
                    g.DrawImage(bmp, new Rectangle(0, 0, w, Height), new Rectangle(0, 0, w, Height), GraphicsUnit.Pixel);
                    if (Height >= Width * 2 + 4)
                        for (int i = 0; i < w; i++)
                        {

                            colors[i] = bmp.GetPixel(i, Width + 2);
                        }
                }

                using (Bitmap bmp = new Bitmap(Width, Height))
                using (Graphics g1 = bmp.Graphics())
                using (LinearGradientBrush lgb = new LinearGradientBrush(new Point(w - 1, 0),
                    new Point(Width, 0),
                       fillColor,
                    rectColor))
                {
                    g1.SetHighQuality();
                    g1.FillPath(lgb, path);
                    g1.SetDefaultQuality();

                    g.DrawImage(bmp, new Rectangle(w, 0, Width - w, Height), new Rectangle(w, 0, Width - w, Height), GraphicsUnit.Pixel);
                    if (Height >= Width * 2 + 4)
                        for (int i = w; i < Width; i++)
                        {
                            colors[i] = bmp.GetPixel(i, Height - Width - 2);
                        }
                }

                if (Height < Width * 2 + 4) return;
                w = Width.Div(2) + Width.Mod(2);

                if (RadiusSides.HasFlag(UICornerRadiusSides.LeftTop) && !RadiusSides.HasFlag(UICornerRadiusSides.RightTop))
                {
                    for (int i = 1; i < w; i++)
                    {
                        using Pen pen = new Pen(colors[i], 2);
                        g.DrawArc(pen, new Rectangle(i, i, Width - i * 2, Width - i * 2), 180, 90);
                    }

                    for (int i = 0; i < w; i++)
                    {
                        g.DrawLine(colors[i], w, i, Width, i);
                    }

                    for (int i = w; i < Width; i++)
                    {
                        g.DrawLine(colors[i], w + (i - w), i, Width, i);
                    }
                }

                if (!RadiusSides.HasFlag(UICornerRadiusSides.LeftTop) && RadiusSides.HasFlag(UICornerRadiusSides.RightTop))
                {
                    for (int i = 1; i < w; i++)
                    {
                        using Pen pen = new Pen(colors[i], 2);
                        g.DrawArc(pen, new Rectangle(i - 1, i, Width - i * 2, Width - i * 2), 270, 90);
                    }

                    for (int i = 0; i < w; i++)
                    {
                        int idx = Width - i;
                        if (idx >= 0 && idx < Width)
                            g.DrawLine(colors[idx], 0, i - 1, w, i - 1);
                    }

                    for (int i = w; i < Width; i++)
                    {
                        int idx = Width - i;
                        if (idx >= 0 && idx < Width)
                            g.DrawLine(colors[idx], 0, i - 1, Width - i, i - 1);
                    }
                }

                if (RadiusSides.HasFlag(UICornerRadiusSides.LeftBottom) && !RadiusSides.HasFlag(UICornerRadiusSides.RightBottom))
                {
                    for (int i = 1; i < w; i++)
                    {
                        using Pen pen = new Pen(colors[i], 2);
                        g.DrawArc(pen, new Rectangle(i, Height - Width + i - 1, Width - i * 2, Width - i * 2), 90, 90);
                    }

                    for (int i = 0; i < w; i++)
                    {
                        g.DrawLine(colors[i], w, Height - i - 1, Width, Height - i - 1);
                    }

                    for (int i = w; i < Width; i++)
                    {
                        g.DrawLine(colors[i], w + (i - w), Height - i - 1, Width, Height - i - 1);
                    }
                }

                if (!RadiusSides.HasFlag(UICornerRadiusSides.LeftBottom) && RadiusSides.HasFlag(UICornerRadiusSides.RightBottom))
                {
                    for (int i = 1; i < w; i++)
                    {
                        using Pen pen = new Pen(colors[i], 2);
                        g.DrawArc(pen, new Rectangle(i - 1, Height - Width - 1 + i, Width - i * 2, Width - i * 2), 0, 90);
                    }

                    for (int i = 0; i < w; i++)
                    {
                        int idx = Width - i;
                        if (idx >= 0 && idx < Width)
                            g.DrawLine(colors[idx], 0, Height - i, w, Height - i);
                    }

                    for (int i = w; i < Width; i++)
                    {
                        int idx = Width - i;
                        if (idx >= 0 && idx < Width)
                            g.DrawLine(colors[idx], 0, Height - i, Width - i, Height - i);
                    }
                }
            }

            PaintLinked(g);
            PaintFlow(g);
        }

        private void PaintLinked(Graphics g)
        {
            foreach (var pipe in linked.Keys)
            {
                if (Direction == UILine.LineDirection.Horizontal)
                {

                }

                if (Direction == UILine.LineDirection.Vertical)
                {
                    if (pipe.Direction == UILine.LineDirection.Vertical) continue;
                    if (pipe.Parent != this.Parent) continue;
                    if (pipe.Width < 5) continue;

                    if (linked[pipe] == null || linked[pipe].Size != pipe.Size)
                    {
                        linked[pipe]?.Dispose();
                        linked[pipe] = CreatePipeBack(pipe);
                    }

                    if (pipe.Left > Left && pipe.Left < Right && pipe.Right > Right)
                    {
                        int h = pipe.Height / 2;
                        int w = Width / 2 + Width.Mod(2) - 1;
                        for (int i = 0; i < h; i++)
                        {
                            int ww = i;
                            if (ww >= w) ww = w;
                            g.DrawLine(linked[pipe].GetPixel(2, i), Width - ww, pipe.Top + i - this.Top, Width, pipe.Top + i - this.Top);
                        }

                        for (int i = h; i < pipe.Height; i++)
                        {
                            int ww = pipe.Height - i - 1;
                            if (ww >= w) ww = w;
                            g.DrawLine(linked[pipe].GetPixel(2, i), Width - ww, pipe.Top + i - this.Top, Width, pipe.Top + i - this.Top);
                        }
                    }

                    if (pipe.Left < Left && pipe.Right > Left && pipe.Right < Right)
                    {
                        int h = pipe.Height / 2;
                        int w = Width / 2 + Width.Mod(2) - 1;
                        for (int i = 0; i < h; i++)
                        {
                            int ww = i;
                            if (ww >= w) ww = w;
                            g.DrawLine(linked[pipe].GetPixel(2, i), 0, pipe.Top + i - this.Top, ww - 1, pipe.Top + i - this.Top);
                        }

                        for (int i = h; i < pipe.Height; i++)
                        {
                            int ww = pipe.Height - i - 1;
                            if (ww >= w) ww = w;
                            g.DrawLine(linked[pipe].GetPixel(2, i), 0, pipe.Top + i - this.Top, ww - 1, pipe.Top + i - this.Top);
                        }
                    }
                }
            }
        }

        private void PaintLinkedRect(Graphics g)
        {
            foreach (var pipe in linked.Keys)
            {
                if (Direction == UILine.LineDirection.Horizontal)
                {

                }

                if (Direction == UILine.LineDirection.Vertical)
                {
                    if (pipe.Direction == UILine.LineDirection.Vertical) continue;
                    if (pipe.Parent != this.Parent) continue;
                    if (pipe.Width < 5) continue;

                    if (linked[pipe] == null || linked[pipe].Size != pipe.Size)
                    {
                        linked[pipe]?.Dispose();
                        linked[pipe] = CreatePipeBack(pipe);
                    }

                    if (pipe.Left > Left && pipe.Left < Right && pipe.Right > Right)
                    {
                        for (int i = 0; i < pipe.Height; i++)
                        {
                            g.DrawLine(linked[pipe].GetPixel(2, i), Width - 1, pipe.Top + i - this.Top, Width + 1, pipe.Top + i - this.Top);
                        }
                    }

                    if (pipe.Left < Left && pipe.Right > Left && pipe.Right < Right)
                    {
                        for (int i = 0; i < pipe.Height; i++)
                        {
                            g.DrawLine(linked[pipe].GetPixel(2, i), -1, pipe.Top + i - this.Top, 0, pipe.Top + i - this.Top);
                        }
                    }
                }
            }
        }

        private Bitmap CreatePipeBack(UIPipe pipe)
        {
            Bitmap result = new Bitmap(pipe.Width, pipe.Height);
            using Graphics g = result.Graphics();
            using var path = result.Bounds().CreateRoundedRectanglePath(5, UICornerRadiusSides.None);

            int h = pipe.Height.Div(2) + pipe.Height.Mod(2);
            using (Bitmap bmp = new Bitmap(pipe.Width, pipe.Height))
            using (Graphics g1 = bmp.Graphics())
            using (LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, 0),
                new Point(0, h),
                rectColor,
                fillColor))
            {
                g1.SetHighQuality();
                g1.FillPath(lgb, path);
                g1.SetDefaultQuality();
                g.DrawImage(bmp, new Rectangle(0, 0, pipe.Width, h), new Rectangle(0, 0, pipe.Width, h), GraphicsUnit.Pixel);
            }

            using (Bitmap bmp = new Bitmap(pipe.Width, pipe.Height))
            using (Graphics g1 = bmp.Graphics())
            using (LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, h - 1),
                new Point(0, pipe.Height),
                fillColor,
                rectColor))
            {
                g1.SetHighQuality();
                g1.FillPath(lgb, path);
                g1.SetDefaultQuality();

                g.DrawImage(bmp, new Rectangle(0, h, pipe.Width, pipe.Height - h), new Rectangle(0, h, pipe.Width, pipe.Height - h), GraphicsUnit.Pixel);
            }

            return result;
        }

        private int FlowPos;

        private void PaintFlow(Graphics g)
        {
            if (IsDesignMode) return;
            if (!Active) return;
            Color color = Color.FromArgb(FlowColorAlpha, FlowColor);
            if (Direction == UILine.LineDirection.Horizontal)
            {
                int pos = FlowPos.Mod(FlowSize + FlowInterval);
                for (int i = 0; i < int.MaxValue; i++)
                {
                    Rectangle rect = new Rectangle(pos - FlowSize - FlowInterval, 2, FlowSize, Height - 5);
                    if (rect.Left > Width) break;
                    bool isShow = rect.Left >= 0 & rect.Right <= Width;

                    if (!isShow)
                    {
                        if (rect.Left < 0 && rect.Right > 0)
                        {
                            rect = new Rectangle(0, rect.Top, rect.Width + rect.Left, rect.Height);
                            isShow = true;
                        }
                    }

                    if (!isShow)
                    {
                        if (rect.Left < Width && rect.Right > Width)
                        {
                            rect = new Rectangle(rect.Left, rect.Top, Width - rect.Left, rect.Height);
                            isShow = true;
                        }
                    }

                    if (isShow)
                    {
                        g.FillRoundRectangle(color, rect, Math.Min(rect.Width, rect.Height));
                    }

                    pos += FlowSize;
                    pos += flowInterval;
                }

                if (FlowDirection == UIFlowDirection.Forward)
                    FlowPos += FlowSpeed;
                if (FlowDirection == UIFlowDirection.Reverse)
                    FlowPos -= FlowSpeed;
            }

            if (Direction == UILine.LineDirection.Vertical)
            {
                int pos = FlowPos.Mod(FlowSize + FlowInterval);

                int top = 0;
                if (RadiusSides.HasFlag(UICornerRadiusSides.LeftTop) &&
                    !RadiusSides.HasFlag(UICornerRadiusSides.RightTop)) top = Width;
                if (!RadiusSides.HasFlag(UICornerRadiusSides.LeftTop) &&
                    RadiusSides.HasFlag(UICornerRadiusSides.RightTop)) top = Width;
                int bottom = Height - 1;
                if (RadiusSides.HasFlag(UICornerRadiusSides.LeftBottom) &&
                    !RadiusSides.HasFlag(UICornerRadiusSides.RightBottom)) bottom = Height - Width;
                if (!RadiusSides.HasFlag(UICornerRadiusSides.LeftBottom) &&
                    RadiusSides.HasFlag(UICornerRadiusSides.RightBottom)) bottom = Height - Width;

                for (int i = 0; i < int.MaxValue; i++)
                {
                    Rectangle rect = new Rectangle(2, pos - FlowSize - FlowInterval, Width - 5, FlowSize);
                    if (rect.Top > Height) break;
                    bool isShow = rect.Top >= top & rect.Bottom <= bottom;

                    if (!isShow)
                    {
                        if (rect.Top < top && rect.Bottom > top)
                        {
                            rect = new Rectangle(rect.Left, top, rect.Width, rect.Height + rect.Top - top);
                            isShow = true;
                        }
                    }

                    if (!isShow)
                    {
                        if (rect.Top < bottom && rect.Bottom >= bottom)
                        {
                            rect = new Rectangle(rect.Left, rect.Top, rect.Width, bottom - rect.Top);
                            isShow = true;
                        }
                    }

                    if (isShow)
                    {
                        g.FillRoundRectangle(color, rect, Math.Min(rect.Width, rect.Height));
                    }

                    pos += FlowSize;
                    pos += flowInterval;
                }

                if (FlowDirection == UIFlowDirection.Forward)
                    FlowPos += FlowSpeed;
                if (FlowDirection == UIFlowDirection.Reverse)
                    FlowPos -= FlowSpeed;
            }
        }
    }
}
