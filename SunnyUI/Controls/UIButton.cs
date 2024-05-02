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
 * 文件名称: UIButton.cs
 * 文件说明: 按钮
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2020-07-26: V2.2.6 增加Selected及选中颜色配置
 * 2020-08-22: V2.2.7 空格键按下press背景效果，添加双击事件，解决因快速点击导致过慢问题
 * 2020-09-14: V2.2.7 Tips颜色可设置
 * 2021-07-18: V3.0.5 增加ShowFocusColor，用来显示Focus状态
 * 2021-12-11: V3.0.9 增加了渐变色
 * 2022-02-26: V3.1.1 增加了AutoSize属性
 * 2022-03-19: V3.1.1 重构主题配色
 * 2022-03-31: V3.1.2 是否显示浅色背景
 * 2022-08-25: V3.2.3 增加同一个容器的相同GroupIndex的按钮控件的Selected单选
 * 2023-05-12: V3.3.6 重构DrawString函数
 * 2023-07-02: V3.3.9 渐变色增加方向选择
 * 2023-11-24: V3.6.2 修复LightStyle的文字颜色
 * 2023-12-06: V3.6.2 修复LightStyle的背景颜色
 * 2024-02-22: V3.6.3 增加按钮的&字符的Alt快捷键功能
 * 2024-02-23: V3.6.3 增加Text的属性编辑器
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 按钮
    /// </summary>
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [ToolboxItem(true)]
    public class UIButton : UIControl, IButtonControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UIButton()
        {
            SetStyleFlags();
            TabStop = true;
            Width = 100;
            Height = 35;
            Cursor = Cursors.Hand;

            plainColor = UIStyles.Blue.PlainColor;

            foreHoverColor = UIStyles.Blue.ButtonForeHoverColor;
            forePressColor = UIStyles.Blue.ButtonForePressColor;
            foreSelectedColor = UIStyles.Blue.ButtonForeSelectedColor;

            rectHoverColor = UIStyles.Blue.ButtonRectHoverColor;
            rectPressColor = UIStyles.Blue.ButtonRectPressColor;
            rectSelectedColor = UIStyles.Blue.ButtonRectSelectedColor;

            fillHoverColor = UIStyles.Blue.ButtonFillHoverColor;
            fillPressColor = UIStyles.Blue.ButtonFillPressColor;
            fillSelectedColor = UIStyles.Blue.ButtonFillSelectedColor;
            SetStyle(ControlStyles.StandardDoubleClick, UseDoubleClick);

            UseMnemonic = true;
        }

        [DefaultValue(true)]
        [Description("如果为true，&符号后面的第一次字符将做按钮的助记键"), Category("SunnyUI")]
        public bool UseMnemonic { get; set; }

        protected override bool ProcessMnemonic(char charCode)
        {
            if (UseMnemonic && CanProcessMnemonic() && IsMnemonic(charCode, Text))
            {
                PerformClick();
                return true;
            }

            return base.ProcessMnemonic(charCode);
        }

        private bool CanProcessMnemonic()
        {
            return UseMnemonic && CanSelect && Enabled && Visible && Parent != null;
        }

        public string TextWithoutMnemonics(string text)
        {
            if (text == null)
            {
                return null;
            }

            int index = text.IndexOf('&');

            if (index == -1)
            {
                return text;
            }

            StringBuilder str = new StringBuilder(text.Substring(0, index));
            for (; index < text.Length; ++index)
            {
                if (text[index] == '&')
                {
                    index++;    // Skip this & and copy the next character instead
                }
                if (index < text.Length)
                {
                    str.Append(text[index]);
                }
            }

            return str.ToString();
        }

        /// <summary>
        /// 是否显示浅色背景
        /// </summary>
        [DefaultValue(false)]
        [Description("是否显示浅色背景"), Category("SunnyUI")]
        public bool LightStyle
        {
            get => lightStyle;
            set
            {
                if (lightStyle != value)
                {
                    lightStyle = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "243, 249, 255")]
        [Description("浅色背景"), Category("SunnyUI")]
        public Color LightColor
        {
            get => plainColor;
            set => SetPlainColor(value);
        }

        private bool autoSize;

        /// <summary>
        /// 自动大小
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Description("自动大小"), Category("SunnyUI")]
        public override bool AutoSize
        {
            get => autoSize;
            set
            {
                autoSize = value;
                Invalidate();
            }
        }

        private bool isClick;

        /// <summary>
        /// 调用点击事件
        /// </summary>
        public void PerformClick()
        {
            if (isClick) return;
            if (CanSelect && Enabled)
            {
                isClick = true;
                OnClick(EventArgs.Empty);
                isClick = false;
            }
        }

        /// <summary>
        /// 重载点击事件
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnClick(EventArgs e)
        {
            Form form = FindFormInternal();
            if (form != null) form.DialogResult = DialogResult;

            Focus();
            base.OnClick(e);
        }

        internal Form FindFormInternal()
        {
            Control cur = this;
            while (cur != null && !(cur is Form))
            {
                cur = cur.Parent;
            }

            return (Form)cur;
        }

        private bool showTips = false;

        /// <summary>
        /// 是否显示角标
        /// </summary>
        [Description("是否显示角标"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool ShowTips
        {
            get
            {
                return showTips;
            }
            set
            {
                if (showTips != value)
                {
                    showTips = value;
                    Invalidate();
                }
            }
        }

        private string tipsText = "";

        /// <summary>
        /// 角标文字
        /// </summary>
        [Description("角标文字"), Category("SunnyUI")]
        [DefaultValue("")]
        public string TipsText
        {
            get { return tipsText; }
            set
            {
                if (tipsText != value)
                {
                    tipsText = value;
                    if (ShowTips)
                    {
                        Invalidate();
                    }
                }
            }
        }

        private Font tipsFont = UIStyles.SubFont();

        /// <summary>
        /// 角标文字字体
        /// </summary>
        [Description("角标文字字体"), Category("SunnyUI")]
        [DefaultValue(typeof(Font), "宋体, 9pt")]
        public Font TipsFont
        {
            get { return tipsFont; }
            set
            {
                if (!tipsFont.Equals(value))
                {
                    tipsFont = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            if (FillColorGradient)
            {
                if (IsHover || IsPress || Selected || Disabled)
                {
                    base.OnPaintFill(g, path);
                }
                else
                {
                    LinearGradientBrush br;
                    switch (fillColorGradientDirection)
                    {
                        case FlowDirection.LeftToRight:
                            br = new LinearGradientBrush(new Point(0, 0), new Point(Width, y: 0), FillColor, FillColor2);
                            break;
                        case FlowDirection.TopDown:
                            br = new LinearGradientBrush(new Point(0, 0), new Point(0, Height), FillColor, FillColor2);
                            break;
                        case FlowDirection.RightToLeft:
                            br = new LinearGradientBrush(new Point(Width, 0), new Point(0, y: 0), FillColor, FillColor2);
                            break;
                        case FlowDirection.BottomUp:
                            br = new LinearGradientBrush(new Point(0, Height), new Point(0, 0), FillColor, FillColor2);
                            break;
                        default:
                            br = new LinearGradientBrush(new Point(0, 0), new Point(0, Height), FillColor, FillColor2);
                            break;
                    }

                    br.GammaCorrection = true;
                    g.FillPath(br, path);
                    br?.Dispose();
                }
            }
            else
            {
                base.OnPaintFill(g, path);
            }
        }

        private Color tipsColor = Color.Red;

        /// <summary>
        /// 角标背景颜色
        /// </summary>
        [Description("角标背景颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "Red")]
        public Color TipsColor
        {
            get => tipsColor;
            set
            {
                tipsColor = value;
                Invalidate();
            }
        }

        private Color tipsForeColor = Color.White;

        /// <summary>
        /// 角标文字颜色
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("SunnyUI"), Description("角标文字颜色")]
        public Color TipsForeColor
        {
            get => tipsForeColor;
            set
            {
                tipsForeColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (autoSize && Dock == DockStyle.None)
            {
                Size sf = TextRenderer.MeasureText(Text, Font);
                if (Width != sf.Width + 6) Width = sf.Width + 6;
                if (Height != sf.Height + 6) Height = sf.Height + 6;
            }

            if (Enabled && ShowTips && !string.IsNullOrEmpty(TipsText))
            {
                e.Graphics.SetHighQuality();
                using var TempFont = TipsFont.DPIScaleFont(TipsFont.Size);
                Size sf = TextRenderer.MeasureText(TipsText, TempFont);
                int sfMax = Math.Max(sf.Width, sf.Height);
                int x = Width - 1 - 2 - sfMax;
                int y = 1 + 1;
                e.Graphics.FillEllipse(TipsColor, x - 1, y, sfMax, sfMax);
                e.Graphics.DrawString(TipsText, TempFont, TipsForeColor, new Rectangle(x, y, sfMax, sfMax), ContentAlignment.MiddleCenter);
            }

            if (Focused && ShowFocusLine)
            {
                Rectangle rect = new Rectangle(2, 2, Width - 5, Height - 5);
                using var path = rect.CreateRoundedRectanglePath(Radius);
                using Pen pn = new Pen(ForeColor);
                pn.DashStyle = DashStyle.Dot;
                e.Graphics.DrawPath(pn, path);
            }
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        [DefaultValue(false)]
        [Description("是否选中"), Category("SunnyUI")]
        public bool Selected
        {
            get => selected;
            set
            {
                if (selected != value)
                {
                    selected = value;
                    Invalidate();

                    if (value && Parent != null)
                    {
                        if (this is UISymbolButton)
                        {
                            List<UISymbolButton> buttons = Parent.GetControls<UISymbolButton>();

                            foreach (var box in buttons)
                            {
                                if (box == this) continue;
                                if (box.GroupIndex != GroupIndex) continue;
                                if (box.Selected) box.Selected = false;
                            }

                            return;
                        }

                        if (this is UIButton)
                        {
                            List<UIButton> buttons = Parent.GetControls<UIButton>();

                            foreach (var box in buttons)
                            {
                                if (box is UISymbolButton) continue;
                                if (box == this) continue;
                                if (box.GroupIndex != GroupIndex) continue;
                                if (box.Selected) box.Selected = false;
                            }

                            return;
                        }
                    }
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

            plainColor = uiColor.PlainColor;

            fillHoverColor = uiColor.ButtonFillHoverColor;
            rectHoverColor = uiColor.ButtonRectHoverColor;
            foreHoverColor = uiColor.ButtonForeHoverColor;

            fillPressColor = uiColor.ButtonFillPressColor;
            rectPressColor = uiColor.ButtonRectPressColor;
            forePressColor = uiColor.ButtonForePressColor;

            fillSelectedColor = uiColor.ButtonFillSelectedColor;
            foreSelectedColor = uiColor.ButtonForeSelectedColor;
            rectSelectedColor = uiColor.ButtonRectSelectedColor;
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color FillColor2
        {
            get => fillColor2;
            set => SetFillColor2(value);
        }

        /// <summary>
        /// 填充颜色渐变
        /// </summary>
        [Description("填充颜色渐变"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool FillColorGradient
        {
            get => fillColorGradient;
            set
            {
                if (fillColorGradient != value)
                {
                    fillColorGradient = value;
                    Invalidate();
                }
            }
        }

        private FlowDirection fillColorGradientDirection = FlowDirection.TopDown;

        [Description("填充颜色渐变方向"), Category("SunnyUI")]
        [DefaultValue(FlowDirection.TopDown)]
        public FlowDirection FillColorGradientDirection
        {
            get => fillColorGradientDirection;
            set
            {
                if (fillColorGradientDirection != value)
                {
                    fillColorGradientDirection = value;
                    Invalidate();
                }
            }
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

        /// <summary>
        /// 不可用时填充颜色
        /// </summary>
        [DefaultValue(typeof(Color), "244, 244, 244"), Category("SunnyUI")]
        [Description("不可用时填充颜色")]
        public Color FillDisableColor
        {
            get => fillDisableColor;
            set => SetFillDisableColor(value);
        }

        /// <summary>
        /// 不可用时边框颜色
        /// </summary>
        [DefaultValue(typeof(Color), "173, 178, 181"), Category("SunnyUI")]
        [Description("不可用时边框颜色")]
        public Color RectDisableColor
        {
            get => rectDisableColor;
            set => SetRectDisableColor(value);
        }

        /// <summary>
        /// 不可用时字体颜色
        /// </summary>
        [DefaultValue(typeof(Color), "109, 109, 103"), Category("SunnyUI")]
        [Description("不可用时字体颜色")]
        public Color ForeDisableColor
        {
            get => foreDisableColor;
            set => SetForeDisableColor(value);
        }

        /// <summary>
        /// 鼠标移上时填充颜色
        /// </summary>
        [DefaultValue(typeof(Color), "115, 179, 255"), Category("SunnyUI")]
        [Description("鼠标移上时填充颜色")]
        public Color FillHoverColor
        {
            get => fillHoverColor;
            set => SetFillHoverColor(value);
        }

        /// <summary>
        /// 鼠标按下时填充颜色
        /// </summary>
        [DefaultValue(typeof(Color), "64, 128, 204"), Category("SunnyUI")]
        [Description("鼠标按下时填充颜色")]
        public Color FillPressColor
        {
            get => fillPressColor;
            set => SetFillPressColor(value);
        }

        /// <summary>
        /// 鼠标移上时字体颜色
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
        [Description("鼠标移上时字体颜色")]
        public Color ForeHoverColor
        {
            get => foreHoverColor;
            set => SetForeHoverColor(value);
        }

        /// <summary>
        /// 鼠标按下时字体颜色
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
        [Description("鼠标按下时字体颜色")]
        public Color ForePressColor
        {
            get => forePressColor;
            set => SetForePressColor(value);
        }

        /// <summary>
        /// 鼠标移上时边框颜色
        /// </summary>
        [DefaultValue(typeof(Color), "115, 179, 255"), Category("SunnyUI")]
        [Description("鼠标移上时边框颜色")]
        public Color RectHoverColor
        {
            get => rectHoverColor;
            set => SetRectHoverColor(value);
        }

        /// <summary>
        /// 鼠标按下时边框颜色
        /// </summary>
        [DefaultValue(typeof(Color), "64, 128, 204"), Category("SunnyUI")]
        [Description("鼠标按下时边框颜色")]
        public Color RectPressColor
        {
            get => rectPressColor;
            set => SetRectPressColor(value);
        }

        /// <summary>
        /// 选中时填充颜色
        /// </summary>
        [DefaultValue(typeof(Color), "64, 128, 204"), Category("SunnyUI")]
        [Description("选中时填充颜色")]
        public Color FillSelectedColor
        {
            get => fillSelectedColor;
            set => SetFillSelectedColor(value);
        }

        /// <summary>
        /// 选中时字体颜色
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
        [Description("选中时字体颜色")]
        public Color ForeSelectedColor
        {
            get => foreSelectedColor;
            set => SetForeSelectedColor(value);
        }

        /// <summary>
        /// 选中时边框颜色
        /// </summary>
        [DefaultValue(typeof(Color), "64, 128, 204"), Category("SunnyUI")]
        [Description("选中时边框颜色")]
        public Color RectSelectedColor
        {
            get => rectSelectedColor;
            set => SetRectSelectedColor(value);
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
        /// 重载鼠标离开事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            IsPress = false;
            IsHover = false;
            Invalidate();
        }

        /// <summary>
        /// 重载鼠标进入事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            IsHover = true;
            Invalidate();
        }

        /// <summary>
        /// 通知控件它是默认按钮
        /// </summary>
        /// <param name="value"></param>
        public void NotifyDefault(bool value)
        {
        }

        /// <summary>
        /// 指定标识符以指示对话框的返回值
        /// </summary>
        [DefaultValue(DialogResult.None)]
        [Description("指定标识符以指示对话框的返回值"), Category("SunnyUI")]
        public DialogResult DialogResult { get; set; } = DialogResult.None;

        /// <summary>
        /// 键盘按下事件
        /// </summary>
        /// <param name="e">按键参数</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Focused && e.KeyCode == Keys.Space)
            {
                IsPress = true;
                Invalidate();
                PerformClick();
            }

            base.OnKeyDown(e);
        }

        /// <summary>
        /// 键盘抬起事件
        /// </summary>
        /// <param name="e">按键参数</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            IsPress = false;
            Invalidate();

            base.OnKeyUp(e);
        }

        /// <summary>
        /// 显示激活时边框线
        /// </summary>
        [DefaultValue(false)]
        [Description("显示激活时边框线"), Category("SunnyUI")]
        public bool ShowFocusLine { get; set; }

        [DefaultValue(0)]
        [Description("分组编号"), Category("SunnyUI")]
        public int GroupIndex { get; set; }

        [Description("文本返回值"), Category("SunnyUI")]
        [Browsable(true)]
        [DefaultValue("")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }
    }
}