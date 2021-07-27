/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2021 ShenYongHua(沈永华).
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
 * 当前版本: V3.0
 * 创建日期: 2021-07-26
 *
 * 2020-07-26: V3.0.5 增加管道控件
******************************************************************************/

using System;
using System.Collections.Generic;
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
        }

        private List<UIPipe> linked = new List<UIPipe>();

        public void Link(UIPipe pipe)
        {
            if (linked.IndexOf(pipe) < 0)
            {
                linked.Add(pipe);
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
        }

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

                if (RadiusSides.HasFlag(UICornerRadiusSides.LeftTop) &&
                    !RadiusSides.HasFlag(UICornerRadiusSides.RightTop))
                {
                    for (int i = 1; i < w; i++)
                    {
                        g.DrawArc(new Pen(colors[i], 2), new Rectangle(i, i, Width - i * 2, Width - i * 2), 180, 90);
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

                if (!RadiusSides.HasFlag(UICornerRadiusSides.LeftTop) &&
                    RadiusSides.HasFlag(UICornerRadiusSides.RightTop))
                {
                    for (int i = 1; i < w; i++)
                    {
                        g.DrawArc(new Pen(colors[i], 2), new Rectangle(i - 1, i, Width - i * 2, Width - i * 2), 270, 90);
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

                if (RadiusSides.HasFlag(UICornerRadiusSides.LeftBottom) &&
                    !RadiusSides.HasFlag(UICornerRadiusSides.RightBottom))
                {
                    for (int i = 1; i < w; i++)
                    {
                        g.DrawArc(new Pen(colors[i], 2), new Rectangle(i, Height - Width + i - 1, Width - i * 2, Width - i * 2), 90, 90);
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

                if (!RadiusSides.HasFlag(UICornerRadiusSides.LeftBottom) &&
                    RadiusSides.HasFlag(UICornerRadiusSides.RightBottom))
                {
                    for (int i = 1; i < w; i++)
                    {
                        g.DrawArc(new Pen(colors[i], 2), new Rectangle(i - 1, Height - Width - 1 + i, Width - i * 2, Width - i * 2), 0, 90);
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

            PaintFlow(g);
        }

        private int FlowPos = 0;

        private void PaintFlow(Graphics g)
        {
            if (IsDesignMode) return;
            if (!Active) return;
            Color color = Color.FromArgb(150, FlowColor);
            if (Direction == UILine.LineDirection.Horizontal)
            {
                int pos = FlowPos.Mod(FlowSize + FlowInterval);
                for (int i = 0; i < int.MaxValue; i++)
                {
                    Rectangle rect = new Rectangle(pos - FlowSize - FlowInterval, 1, FlowSize, Height - 3);
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

                    if (rect.Width >= rect.Height && isShow)
                    {
                        g.FillRoundRectangle(color, rect, Radius - 2);
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
                    Rectangle rect = new Rectangle(1, pos - FlowSize - FlowInterval, Width - 3, FlowSize);
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

                    if (rect.Height >= rect.Width && isShow)
                    {
                        g.FillRoundRectangle(color, rect, Radius - 2);
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
