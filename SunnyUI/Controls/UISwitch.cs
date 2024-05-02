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
 * 文件名称: UISwitch.cs
 * 文件说明: 开关
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2021-05-06: V3.0.3 更新Active状态改变时触发ValueChanged事件
 * 2021-09-14: V3.0.7 增加Disabled颜色
 * 2022-01-02: V3.0.9 增加是否只读属性
 * 2022-03-19: V3.1.1 重构主题配色
 * 2022-09-26: V3.2.4 修复了Readonly时，双击还可以改变值的问题
 * 2022-04-23: V3.3.5 增加ActiveChanging事件，在状态改变前可以进行判断
 * 2023-05-13: V3.3.6 重构DrawString函数
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sunny.UI
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Active")]
    [ToolboxItem(true)]
    public sealed class UISwitch : UIControl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="value">开关值</param>
        public delegate void OnValueChanged(object sender, bool value);

        public enum UISwitchShape
        {
            Round,
            Square
        }

        public UISwitch()
        {
            SetStyleFlags();
            Height = 29;
            Width = 75;
            ShowText = false;
            ShowRect = false;

            inActiveColor = Color.Gray;
            fillColor = Color.White;

            rectColor = UIStyles.Blue.SwitchActiveColor;
            fillColor = UIStyles.Blue.SwitchFillColor;
            inActiveColor = UIStyles.Blue.SwitchInActiveColor;
            rectDisableColor = UIStyles.Blue.SwitchRectDisableColor;
        }

        [DefaultValue(false)]
        [Description("是否只读"), Category("SunnyUI")]
        public bool ReadOnly { get; set; }

        private UISwitchShape switchShape = UISwitchShape.Round;

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

        public event EventHandler ActiveChanged;

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
                if (!ReadOnly && activeValue != value)
                {
                    activeValue = value;
                    ValueChanged?.Invoke(this, value);
                    ActiveChanged?.Invoke(this, new EventArgs());
                    Invalidate();
                }
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

        [DefaultValue(typeof(Color), "Gray")]
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

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnClick(EventArgs e)
        {
            ActiveChange();
            base.OnClick(e);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            if (!UseDoubleClick)
            {
                ActiveChange();
                base.OnClick(e);
            }
            else
            {
                base.OnDoubleClick(e);
            }
        }

        public event OnCancelEventArgs ActiveChanging;

        private void ActiveChange()
        {
            CancelEventArgs e = new CancelEventArgs();
            if (ActiveChanging != null)
            {
                ActiveChanging?.Invoke(this, e);
            }

            if (!e.Cancel)
            {
                Active = !Active;
            }
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);

            rectColor = uiColor.SwitchActiveColor;
            fillColor = uiColor.SwitchFillColor;
            inActiveColor = uiColor.SwitchInActiveColor;
            rectDisableColor = uiColor.SwitchRectDisableColor;
        }

        [Description("不可用颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "173, 178, 181")]
        public Color DisabledColor
        {
            get => rectDisableColor;
            set => SetRectDisableColor(value);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            Color color = Active ? ActiveColor : InActiveColor;
            if (!Enabled) color = rectDisableColor;

            if (SwitchShape == UISwitchShape.Round)
            {
                Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
                using GraphicsPath path1 = rect.CreateTrueRoundedRectanglePath(rect.Height);
                g.FillPath(color, path1, true);

                int width = Width - 3 - 1 - 3 - (rect.Height - 6);
                if (!Active)
                {
                    g.FillEllipse(fillColor.IsValid() ? fillColor : Color.White, 3, 3, rect.Height - 6, rect.Height - 6);
                    g.DrawString(InActiveText, Font, fillColor.IsValid() ? fillColor : Color.White, new Rectangle(3 + rect.Height - 6, 0, width, rect.Height), ContentAlignment.MiddleCenter);
                }
                else
                {
                    g.FillEllipse(fillColor.IsValid() ? fillColor : Color.White, Width - 3 - 1 - (rect.Height - 6), 3, rect.Height - 6, rect.Height - 6);
                    g.DrawString(ActiveText, Font, fillColor.IsValid() ? fillColor : Color.White, new Rectangle(3, 0, width, rect.Height), ContentAlignment.MiddleCenter);
                }
            }

            if (SwitchShape == UISwitchShape.Square)
            {
                Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
                g.FillRoundRectangle(color, rect, Radius);

                int width = Width - 3 - 1 - 3 - (rect.Height - 6);
                if (!Active)
                {
                    g.FillRoundRectangle(fillColor.IsValid() ? fillColor : Color.White, 3, 3, rect.Height - 6, rect.Height - 6, Radius);
                    g.DrawString(InActiveText, Font, fillColor.IsValid() ? fillColor : Color.White, new Rectangle(3 + rect.Height - 6, 0, width, rect.Height), ContentAlignment.MiddleCenter);
                }
                else
                {
                    g.FillRoundRectangle(fillColor.IsValid() ? fillColor : Color.White, Width - 3 - 1 - (rect.Height - 6), 3, rect.Height - 6, rect.Height - 6, Radius);
                    g.DrawString(ActiveText, Font, fillColor.IsValid() ? fillColor : Color.White, new Rectangle(3, 0, width, rect.Height), ContentAlignment.MiddleCenter);
                }
            }
        }
    }
}