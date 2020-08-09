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
 * 文件名称: UITrackBar.cs
 * 文件说明: 进度指示条
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 *
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
            Width = 150;
            Height = 29;

            ShowText = false;
            ShowRect = false;
            fillColor = UIColor.LightBlue;
            foreColor = UIColor.Blue;
            rectColor = UIColor.Blue;
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
                    Invalidate();
                }

                ValueChanged?.Invoke(this, null);
            }
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            rectDisableColor = uiColor.TrackDisableColor;
            rectColor = uiColor.TrackBarRectColor;
            fillColor = uiColor.TrackBarFillColor;
            foreColor = uiColor.TrackBarForeColor;
            Invalidate();
        }

        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            g.Clear(fillColor);
            g.FillRoundRectangle(rectDisableColor, new Rectangle(5, Height / 2 - 3, Width - 1 - 10, 6), 6);

            int len = (int)((Value - Minimum) * 1.0 * (Width - 1 - 10) / (Maximum - Minimum));
            if (len > 0)
            {
                g.FillRoundRectangle(foreColor, new Rectangle(5, Height / 2 - 3, len, 6), 6);
            }

            g.FillRoundRectangle(fillColor.IsValid() ? fillColor : Color.White, new Rectangle(len, Height / 2 - 10, 10, 20), 5);

            using (Pen pen = new Pen(rectColor, 2))
            {
                g.SetHighQuality();
                g.DrawRoundRectangle(pen, new Rectangle(len + 1, Height / 2 - 9, 8, 18), 5);
                g.SetDefaultQuality();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (!ReadOnly)
            {
                int len = e.X - 5;
                int value = (len * 1.0 * (Maximum - Minimum) / (Width - 1 - 10)).RoundEx() + Minimum;
                Value = Math.Min(Math.Max(Minimum, value), Maximum);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (IsPress && !ReadOnly)
            {
                int len = e.X - 5;
                int value = (len * 1.0 * (Maximum - Minimum) / (Width - 1 - 10)).RoundEx() + Minimum;
                Value = Math.Min(Math.Max(Minimum, value), Maximum);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IsPress = true;
            Invalidate();
        }

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
        [DefaultValue(typeof(Color), "235, 243, 255")]
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

        [DefaultValue(typeof(Color), "173, 178, 181")]
        [Description("不可用时颜色"), Category("SunnyUI")]
        public Color DisableColor
        {
            get => rectDisableColor;
            set => SetRectDisableColor(value);
        }
    }
}