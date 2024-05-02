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
 * 文件名称: UIThermometer.cs
 * 文件说明: 温度计
 * 当前版本: V3.6.1
 * 创建日期: 2023-11-30
 *
 * 2023-11-30: V3.6.1 增加文件说明
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sunny.UI
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    [ToolboxItem(true)]
    public class UIThermometer : UIControl
    {
        public event EventHandler ValueChanged;

        public UIThermometer()
        {
            SetStyleFlags();
            Width = 32;
            Height = 150;

            ShowText = false;
            ShowRect = false;

            rectDisableColor = UIStyles.Blue.TrackDisableColor;
            fillColor = UIStyles.Blue.TrackBarFillColor;
            rectColor = UIStyles.Blue.TrackBarForeColor;
        }

        private int _maximum = 100;
        private int _minimum;
        private int thermometerValue;

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
            get => thermometerValue;
            set
            {
                int v = Math.Min(Math.Max(Minimum, value), Maximum);
                if (thermometerValue != v)
                {
                    thermometerValue = v;
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
            fillColor = uiColor.TrackBarFillColor;
            rectColor = uiColor.TrackBarForeColor;
        }

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            g.Clear(fillColor);
            g.FillRoundRectangle(rectDisableColor, new Rectangle(Width / 2 - LineSize / 2, 5, LineSize, Height - 1 - 10), LineSize);

            int len = (int)((Value - Minimum) * 1.0 * (Height - 1 - 5 - BallSize) / (Maximum - Minimum));
            if (len > 0)
            {
                g.FillRoundRectangle(rectColor, new Rectangle(Width / 2 - LineSize / 2, Height - len - ballSize, LineSize, len), LineSize);
            }

            g.FillEllipse(rectColor, new Rectangle(Width / 2 - BallSize / 2, Height - BallSize - 1, BallSize, BallSize));
            g.FillRectangle(rectColor, new Rectangle(Width / 2 - LineSize / 2, Height - len - ballSize + LineSize / 2, LineSize, len + 2), true);

        }

        private int lineSize = 6;

        [DefaultValue(6)]
        [Description("温度计管大小"), Category("SunnyUI")]
        public int LineSize
        {
            get => lineSize;
            set
            {
                lineSize = Math.Max(6, value);
                Invalidate();
            }
        }

        private int ballSize = 20;

        [DefaultValue(20)]
        [Description("温度计球大小"), Category("SunnyUI")]
        public int BallSize
        {
            get => ballSize;
            set
            {
                ballSize = Math.Max(16, value);
                Invalidate();
            }
        }

        /// <summary>/// 重载鼠标抬起事件
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
        /// 边框颜色
        /// </summary>
        [Description("温度计颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color ThermometerColor
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
