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
 * 文件名称: UISwitch.cs
 * 文件说明: 开关
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sunny.UI
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("ActiveValue")]
    [ToolboxItem(true)]
    public sealed class UISwitch : UIControl
    {
        public delegate void OnValueChanged(object sender, bool value);

        public enum UISwitchShape
        {
            Round,
            Square
        }

        public UISwitch()
        {
            Height = 29;
            Width = 75;
            ShowText = false;
            ShowRect = false;
            foreColor = Color.White;
            inActiveColor = Color.Silver;
            fillColor = Color.White;
        }

        public UISwitchShape switchShape = UISwitchShape.Round;

        [Description("开关形状"), Category("SunnyUI")]
        [DefaultValue(UISwitchShape.Round)]
        public UISwitchShape SwitchShape
        {
            get => switchShape;
            set
            {
                switchShape = value;
                Invalidate();
            }
        }

        public event OnValueChanged ValueChanged;

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }

        private bool activeValue;

        [DefaultValue(false)]
        [Description("是否打开"), Category("SunnyUI")]
        public bool Active
        {
            get => activeValue;
            set
            {
                activeValue = value;
                ValueChanged?.Invoke(this, value);
                Invalidate();
            }
        }

        private string activeText = "开";

        [DefaultValue("开")]
        [Description("打开文字"), Category("SunnyUI")]
        public string ActiveText
        {
            get => activeText;
            set
            {
                activeText = value;
                Invalidate();
            }
        }

        private string inActiveText = "关";

        [DefaultValue("关")]
        [Description("关闭文字"), Category("SunnyUI")]
        public string InActiveText
        {
            get => inActiveText;
            set
            {
                inActiveText = value;
                Invalidate();
            }
        }

        private Color inActiveColor;

        [DefaultValue(typeof(Color), "Silver")]
        [Description("关闭颜色"), Category("SunnyUI")]
        public Color InActiveColor
        {
            get => inActiveColor;
            set
            {
                inActiveColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public Color ButtonColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("打开颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color ActiveColor
        {
            get => rectColor;
            set => SetRectColor(value);
        }

        protected override void OnClick(EventArgs e)
        {
            Active = !Active;
            base.OnClick(e);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            if (!UseDoubleClick)
            {
                Active = !Active;
                base.OnClick(e);
            }
            else
            {
                base.OnDoubleClick(e);
            }
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            rectColor = uiColor.SwitchActiveColor;
            fillColor = uiColor.SwitchFillColor;
            inActiveColor = uiColor.SwitchInActiveColor;
            Invalidate();
        }

        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            if (SwitchShape == UISwitchShape.Round)
            {
                Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
                g.FillRoundRectangle(Active ? ActiveColor : InActiveColor, rect, rect.Height);

                int width = Width - 3 - 1 - 3 - (rect.Height - 6);
                if (!Active)
                {
                    g.FillEllipse(fillColor.IsValid() ? fillColor : Color.White, 3, 3, rect.Height - 6, rect.Height - 6);
                    SizeF sf = g.MeasureString(InActiveText, Font);
                    g.DrawString(InActiveText, Font, fillColor.IsValid() ? fillColor : Color.White, 3 + rect.Height - 6 + (width - sf.Width) / 2, 3 + (rect.Height - 6 - sf.Height) / 2);
                }
                else
                {
                    g.FillEllipse(fillColor.IsValid() ? fillColor : Color.White, Width - 3 - 1 - (rect.Height - 6), 3, rect.Height - 6, rect.Height - 6);
                    SizeF sf = g.MeasureString(ActiveText, Font);
                    g.DrawString(ActiveText, Font, fillColor.IsValid() ? fillColor : Color.White, 3 + (width - sf.Width) / 2, 3 + (rect.Height - 6 - sf.Height) / 2);
                }
            }

            if (SwitchShape == UISwitchShape.Square)
            {
                Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
                g.FillRoundRectangle(Active ? ActiveColor : InActiveColor, rect, Radius);

                int width = Width - 3 - 1 - 3 - (rect.Height - 6);
                if (!Active)
                {
                    g.FillRoundRectangle(fillColor.IsValid() ? fillColor : Color.White, 3, 3, rect.Height - 6, rect.Height - 6, Radius);
                    SizeF sf = g.MeasureString(InActiveText, Font);
                    g.DrawString(InActiveText, Font, fillColor.IsValid() ? fillColor : Color.White, 3 + rect.Height - 6 + (width - sf.Width) / 2, 3 + (rect.Height - 6 - sf.Height) / 2);
                }
                else
                {
                    g.FillRoundRectangle(fillColor.IsValid() ? fillColor : Color.White, Width - 3 - 1 - (rect.Height - 6), 3, rect.Height - 6, rect.Height - 6, Radius);
                    SizeF sf = g.MeasureString(ActiveText, Font);
                    g.DrawString(ActiveText, Font, fillColor.IsValid() ? fillColor : Color.White, 3 + (width - sf.Width) / 2, 3 + (rect.Height - 6 - sf.Height) / 2);
                }
            }
        }
    }
}