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
 * 文件名称: UITrackBar.cs
 * 文件说明: 进度指示条
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2021-04-11: V3.0.2 增加垂直显示方式
 * 2022-03-19: V3.1.1 重构主题配色
 * 2023-11-28: V3.6.1 增加一种从上到下的进度显示方式
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    [ToolboxItem(true)]
    public sealed class UITrackBar : UIControl
    {
        public event EventHandler ValueChanged;

        public UITrackBar()
        {
            SetStyleFlags();
            Width = 150;
            Height = 29;

            ShowText = false;
            ShowRect = false;

            rectDisableColor = UIStyles.Blue.TrackDisableColor;
            rectColor = UIStyles.Blue.TrackBarRectColor;
            fillColor = UIStyles.Blue.TrackBarFillColor;
            foreColor = UIStyles.Blue.TrackBarForeColor;
        }

        public enum BarDirection
        {
            /// <summary>
            /// 水平的
            /// </summary>
            Horizontal,

            /// <summary>
            /// 竖直上升
            /// </summary>
            Vertical,

            /// <summary>
            /// 竖直下降
            /// </summary>
            VerticalDown
        }

        private BarDirection direction = BarDirection.Horizontal;

        [DefaultValue(BarDirection.Horizontal)]
        [Description("线条方向"), Category("SunnyUI")]
        public BarDirection Direction
        {
            get => direction;
            set
            {
                direction = value;
                Invalidate();
            }
        }

        private int _maximum = 100;
        private int _minimum;
        private int trackBarValue;

        [DefaultValue(false)]
        [Description("是否只读"), Category("SunnyUI")]
        public bool ReadOnly { get; set; }

        [DefaultValue(100)]
        [Description("最大值"), Category("SunnyUI")]
        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                if (_maximum <= _minimum)
                    _minimum = _maximum - 1;

                Invalidate();
            }
        }

        [DefaultValue(0)]
        [Description("最小值"), Category("SunnyUI")]
        public int Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                if (_minimum >= _maximum)
                    _maximum = _minimum + 1;

                Invalidate();
            }
        }

        [DefaultValue(0)]
        [Description("当前值"), Category("SunnyUI")]
        public int Value
        {
            get => trackBarValue;
            set
            {
                int v = Math.Min(Math.Max(Minimum, value), Maximum);
                if (trackBarValue != v)
                {
                    trackBarValue = v;
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);

            rectDisableColor = uiColor.TrackDisableColor;
            rectColor = uiColor.TrackBarRectColor;
            fillColor = uiColor.TrackBarFillColor;
            foreColor = uiColor.TrackBarForeColor;
        }

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            g.Clear(fillColor);

            if (Direction == BarDirection.Horizontal)
            {
                g.FillRoundRectangle(rectDisableColor,
                    new Rectangle(5, Height / 2 - 3, Width - 1 - 10, 6), 6);

                int len = (int)((Value - Minimum) * 1.0 * (Width - 1 - 10) / (Maximum - Minimum));
                if (len > 0)
                {
                    g.FillRoundRectangle(foreColor, new Rectangle(5, Height / 2 - 3, len, 6), 6);
                }

                g.FillRoundRectangle(fillColor.IsValid() ? fillColor : Color.White,
                    new Rectangle(len, (Height - BarSize) / 2, 10, BarSize), 5);

                using Pen pen = new Pen(rectColor, 2);
                g.SetHighQuality();
                g.DrawRoundRectangle(pen, new Rectangle(len + 1, (Height - BarSize) / 2 + 1, 8, BarSize - 2), 5);
                g.SetDefaultQuality();
            }

            if (Direction == BarDirection.Vertical)
            {
                g.FillRoundRectangle(rectDisableColor, new Rectangle(Width / 2 - 3, 5, 6, Height - 1 - 10), 6);

                int len = (int)((Value - Minimum) * 1.0 * (Height - 1 - 10) / (Maximum - Minimum));
                if (len > 0)
                {
                    g.FillRoundRectangle(foreColor, new Rectangle(Width / 2 - 3, Height - len - 5, 6, len), 6);
                }

                g.FillRoundRectangle(fillColor.IsValid() ? fillColor : Color.White, new Rectangle((Width - BarSize) / 2, Height - len - 10 - 1, BarSize, 10), 5);

                using Pen pen = new Pen(rectColor, 2);
                g.SetHighQuality();
                g.DrawRoundRectangle(pen, new Rectangle((Width - BarSize) / 2 + 1, Height - len - 10, BarSize - 2, 8), 5);
                g.SetDefaultQuality();
            }

            if (Direction == BarDirection.VerticalDown)
            {
                g.FillRoundRectangle(rectDisableColor, new Rectangle(Width / 2 - 3, 5, 6, Height - 10), 6);

                int len = (int)((Value - Minimum) * 1.0 * (Height - 1 - 10) / (Maximum - Minimum));
                if (len > 0)
                {
                    g.FillRoundRectangle(foreColor, new Rectangle(Width / 2 - 3, 5, 6, len), 6);
                }

                g.FillRoundRectangle(fillColor.IsValid() ? fillColor : Color.White, new Rectangle((Width - BarSize) / 2, len, BarSize, 10), 5);

                using Pen pen = new Pen(rectColor, 2);
                g.SetHighQuality();
                g.DrawRoundRectangle(pen, new Rectangle((Width - BarSize) / 2 + 1, len + 1, BarSize - 2, 8), 5);
                g.SetDefaultQuality();
            }
        }

        private int trackBarSize = 20;

        [DefaultValue(20)]
        [Description("按钮大小"), Category("SunnyUI")]
        public int BarSize
        {
            get => trackBarSize;
            set
            {
                trackBarSize = Math.Max(12, value);
                Invalidate();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (!ReadOnly)
            {
                if (Direction == BarDirection.Horizontal)
                {
                    int len = e.X - 5;
                    int value = (len * 1.0 * (Maximum - Minimum) / (Width - 10)).RoundEx() + Minimum;
                    Value = Math.Min(Math.Max(Minimum, value), Maximum);
                }

                if (Direction == BarDirection.Vertical)
                {
                    int len = Height - 10 - e.Y;
                    int value = (len * 1.0 * (Maximum - Minimum) / (Height - 10)).RoundEx() + Minimum;
                    Value = Math.Min(Math.Max(Minimum, value), Maximum);
                }

                if (Direction == BarDirection.VerticalDown)
                {
                    int len = e.Y - 5;
                    int value = (len * 1.0 * (Maximum - Minimum) / (Height - 10)).RoundEx() + Minimum;
                    Value = Math.Min(Math.Max(Minimum, value), Maximum);
                }
            }
        }

        /// <summary>
        /// 重载鼠标移动事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (IsPress && !ReadOnly)
            {
                if (Direction == BarDirection.Horizontal)
                {
                    int len = e.X - 5;
                    int value = (len * 1.0 * (Maximum - Minimum) / (Width - 10)).RoundEx() + Minimum;
                    Value = Math.Min(Math.Max(Minimum, value), Maximum);
                }

                if (Direction == BarDirection.Vertical)
                {
                    int len = Height - 10 - e.Y;
                    int value = (len * 1.0 * (Maximum - Minimum) / (Height - 10)).RoundEx() + Minimum;
                    Value = Math.Min(Math.Max(Minimum, value), Maximum);
                }

                if (Direction == BarDirection.VerticalDown)
                {
                    int len = e.Y - 5;
                    int value = (len * 1.0 * (Maximum - Minimum) / (Height - 10)).RoundEx() + Minimum;
                    Value = Math.Min(Math.Max(Minimum, value), Maximum);
                }
            }
        }

        /// <summary>
        /// 重载鼠标按下事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IsPress = true;
            Invalidate();
        }

        /// <summary>
        /// 重载鼠标抬起事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            IsPress = false;
            Invalidate();
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "243, 249, 255")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color RectColor
        {
            get => rectColor;
            set => SetRectColor(value);
        }

        [DefaultValue(typeof(Color), "Silver")]
        [Description("不可用时颜色"), Category("SunnyUI")]
        public Color DisableColor
        {
            get => rectDisableColor;
            set => SetRectDisableColor(value);
        }
    }
}