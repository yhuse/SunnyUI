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
 * 文件名称: UIHeaderButton.cs
 * 文件说明: 顶部图标按钮
 * 当前版本: V3.1
 * 创建日期: 2021-02-10
 *
 * 2021-02-10: V3.0.1 增加文件说明
 * 2021-03-27: V3.0.2 增加字体图标背景时鼠标移上背景色
 * 2021-06-01: V3.0.4 增加图片与文字的位置
 * 2021-06-22: V3.0.4 增加ShowSelected，是否显示选中状态
 * 2021-09-21: V3.0.7 增加Disabled颜色
 * 2021-12-07: V3.0.9 更改图片自动刷新
 * 2022-01-02: V3.0.9 增加角标
 * 2022-03-19: V3.1.1 重构主题配色
 * 2023-05-13: V3.3.6 重构DrawString函数
 * 2023-05-16: V3.3.6 重构DrawFontImage函数
 * 2023-10-26: V3.5.1 字体图标增加旋转角度参数SymbolRotate
 * 2024-01-21: V3.6.3 增加分组编号
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [ToolboxItem(true)]
    public class UIHeaderButton : UIControl, IButtonControl, ISymbol
    {
        public UIHeaderButton()
        {
            SetStyleFlags();
            Size = new Size(100, 88);
            ShowText = false;
            ShowRect = false;
            Radius = 0;
            RadiusSides = UICornerRadiusSides.None;
            RectSides = ToolStripStatusLabelBorderSides.None;
            Padding = new Padding(0, 8, 0, 3);

            SetStyle(ControlStyles.StandardDoubleClick, UseDoubleClick);

            foreHoverColor = UIStyles.Blue.ButtonForeHoverColor;
            forePressColor = UIStyles.Blue.ButtonForePressColor;
            foreSelectedColor = UIStyles.Blue.ButtonForeSelectedColor;

            fillHoverColor = UIStyles.Blue.ButtonFillHoverColor;
            fillPressColor = UIStyles.Blue.ButtonFillPressColor;
            fillSelectedColor = UIStyles.Blue.ButtonFillSelectedColor;

            fillDisableColor = fillColor;
            foreDisableColor = foreColor;
            rectDisableColor = UIStyles.Blue.RectDisableColor;
        }

        /// <summary>
        /// 设置控件缩放比例
        /// </summary>
        /// <param name="scale">缩放比例</param>
        public override void SetZoomScale(float scale)
        {
            base.SetZoomScale(scale);
            circleSize = UIZoomScale.Calc(baseCircleSize, scale);
        }

        private bool showTips = false;

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

        private Color tipsColor = Color.Red;

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

        private Font tipsFont = UIStyles.SubFont();

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

        private bool isClick;

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
        /// 点击事件
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnClick(EventArgs e)
        {
            Focus();

            List<UIHeaderButton> buttons = Parent.GetControls<UIHeaderButton>();
            foreach (var button in buttons)
            {
                if (button.GroupIndex == GroupIndex)
                    button.Selected = false;
            }

            if (ShowSelected)
            {
                Selected = true;
            }

            TabControl?.SelectPage(PageIndex);

            base.OnClick(e);
        }

        [DefaultValue(0)]
        [Description("分组编号"), Category("SunnyUI")]
        public int GroupIndex { get; set; }

        [DefaultValue(true)]
        [Description("显示选中状态"), Category("SunnyUI")]
        public bool ShowSelected { get; set; } = true;

        public event EventHandler SelectedChanged;

        private int _symbolSize = 48;

        /// <summary>
        /// 字体图标大小
        /// </summary>
        [DefaultValue(48)]
        [Description("字体图标大小"), Category("SunnyUI")]
        public int SymbolSize
        {
            get => _symbolSize;
            set
            {
                _symbolSize = Math.Max(value, 16);
                _symbolSize = Math.Min(value, 128);
                Invalidate();
            }
        }

        [DefaultValue(0)]
        [Description("多页面框架的页面索引"), Category("SunnyUI")]
        public int PageIndex { get; set; }

        private Color symbolColor = Color.White;

        /// <summary>
        /// 字体图标颜色
        /// </summary>
        [DefaultValue(typeof(Color), "White")]
        [Description("字体图标颜色"), Category("SunnyUI")]
        public Color SymbolColor
        {
            get => symbolColor;
            set
            {
                symbolColor = value;
                Invalidate();
            }
        }

        private int _symbolRotate = 0;

        /// <summary>
        /// 字体图标旋转角度
        /// </summary>
        [DefaultValue(0)]
        [Description("字体图标旋转角度"), Category("SunnyUI")]
        public int SymbolRotate
        {
            get => _symbolRotate;
            set
            {
                if (_symbolRotate != value)
                {
                    _symbolRotate = value;
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

            fillHoverColor = uiColor.ButtonFillHoverColor;
            foreHoverColor = uiColor.ButtonForeHoverColor;

            fillPressColor = uiColor.ButtonFillPressColor;
            forePressColor = uiColor.ButtonForePressColor;

            fillSelectedColor = uiColor.ButtonFillSelectedColor;
            foreSelectedColor = uiColor.ButtonForeSelectedColor;

            rectDisableColor = uiColor.RectDisableColor;
            fillDisableColor = fillColor;
            foreDisableColor = foreColor;
        }

        [Description("不可用颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "173, 178, 181")]
        public Color CircleDisabledColor
        {
            get => rectDisableColor;
            set => SetRectDisableColor(value);
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

        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color FillDisableColor
        {
            get => fillDisableColor;
            set => SetFillDisableColor(value);
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

        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
        [Description("不可用时字体颜色")]
        public Color ForeDisableColor
        {
            get => foreDisableColor;
            set => SetForeDisableColor(value);
        }

        [DefaultValue(typeof(Color), "115, 179, 255"), Category("SunnyUI")]
        [Description("鼠标移上时填充颜色")]
        public Color FillHoverColor
        {
            get => fillHoverColor;
            set => SetFillHoverColor(value);
        }

        [DefaultValue(typeof(Color), "64, 128, 204"), Category("SunnyUI")]
        [Description("鼠标按下时填充颜色")]
        public Color FillPressColor
        {
            get => fillPressColor;
            set => SetFillPressColor(value);
        }

        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
        [Description("鼠标移上时字体颜色")]
        public Color ForeHoverColor
        {
            get => foreHoverColor;
            set => SetForeHoverColor(value);
        }

        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
        [Description("鼠标按下时字体颜色")]
        public Color ForePressColor
        {
            get => forePressColor;
            set => SetForePressColor(value);
        }

        [DefaultValue(typeof(Color), "64, 128, 204"), Category("SunnyUI")]
        [Description("选中时填充颜色")]
        public Color FillSelectedColor
        {
            get => fillSelectedColor;
            set => SetFillSelectedColor(value);
        }

        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
        [Description("选中时字体颜色")]
        public Color ForeSelectedColor
        {
            get => foreSelectedColor;
            set => SetForeSelectedColor(value);
        }

        private Image image;

        [DefaultValue(null)]
        [Description("图片"), Category("SunnyUI")]
        public Image Image
        {
            get => image;
            set
            {
                if (image != value)
                {
                    image = value;
                    Invalidate();
                }
            }
        }

        private int _symbol = FontAwesomeIcons.fa_check;

        /// <summary>
        /// 字体图标
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor("Sunny.UI.UIImagePropertyEditor, " + AssemblyRefEx.SystemDesign, typeof(UITypeEditor))]
        [DefaultValue(61452)]
        [Description("字体图标"), Category("SunnyUI")]
        public int Symbol
        {
            get => _symbol;
            set
            {
                _symbol = value;
                Invalidate();
            }
        }

        private int imageTop;

        [DefaultValue(0)]
        [Description("图片距离顶部距离"), Category("SunnyUI")]
        public int ImageTop
        {
            get => imageTop;
            set
            {
                imageTop = value;
                Invalidate();
            }
        }

        private Point symbolOffset = new Point(0, 0);

        /// <summary>
        /// 字体图标的偏移位置
        /// </summary>
        [DefaultValue(typeof(Point), "0, 0")]
        [Description("字体图标的偏移位置"), Category("SunnyUI")]
        public Point SymbolOffset
        {
            get => symbolOffset;
            set
            {
                symbolOffset = value;
                Invalidate();
            }
        }

        private Color circleColor = Color.Bisque;

        [DefaultValue(typeof(Color), "Bisque")]
        [Description("字体图标背景颜色"), Category("SunnyUI")]
        public Color CircleColor
        {
            get => circleColor;
            set
            {
                circleColor = value;
                Invalidate();
            }
        }

        [DefaultValue(false)]
        [Description("是否显示字体图标鼠标移上背景颜色"), Category("SunnyUI")]
        public bool ShowCircleHoverColor { get; set; }

        private Color circleHoverColor = Color.Bisque;

        [DefaultValue(typeof(Color), "Bisque")]
        [Description("字体图标鼠标移上背景颜色"), Category("SunnyUI")]
        public Color CircleHoverColor
        {
            get => circleHoverColor;
            set
            {
                circleHoverColor = value;
                Invalidate();
            }
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            Invalidate();
        }

        private int circleSize = 50;
        private int baseCircleSize = 50;

        [DefaultValue(50)]
        [Description("字体图标背景大小"), Category("SunnyUI")]
        public int CircleSize
        {
            get => circleSize;
            set
            {
                baseCircleSize = circleSize = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            if (!selected)
            {
                Color color = GetFillColor();
                g.Clear(color);
            }
            else
            {
                g.Clear(fillSelectedColor);
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
                    SelectedChanged?.Invoke(this, new EventArgs());
                    Invalidate();
                }
            }
        }

        private TextImageRelation textImageRelation = TextImageRelation.ImageAboveText;

        [DefaultValue(TextImageRelation.ImageAboveText)]
        [Description("指定图像与文本的相对位置"), Category("SunnyUI")]
        public TextImageRelation TextImageRelation
        {
            get => textImageRelation;
            set
            {
                textImageRelation = value;
                Invalidate();
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

        public void NotifyDefault(bool value)
        {
        }

        [DefaultValue(DialogResult.None)]
        [Description("指定标识符以指示对话框的返回值"), Category("SunnyUI")]
        public DialogResult DialogResult { get; set; } = DialogResult.None;

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

        protected override void OnKeyUp(KeyEventArgs e)
        {
            IsPress = false;
            Invalidate();

            base.OnKeyUp(e);
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //重绘父类
            base.OnPaint(e);
            Size ImageSize = new Size(0, 0);
            if (Symbol > 0)
                ImageSize = new Size(SymbolSize, SymbolSize);
            if (Image != null)
                ImageSize = Image.Size;

            Color color = GetForeColor();
            switch (textImageRelation)
            {
                case TextImageRelation.TextAboveImage:
                    {
                        #region  文本在上
                        e.Graphics.DrawString(Text, Font, color, new Rectangle(0, Padding.Top, Width, Height), ContentAlignment.TopCenter);

                        //字体图标
                        if (Symbol > 0 && Image == null)
                        {
                            Color bcColor = CircleColor;
                            if (!Enabled) bcColor = CircleDisabledColor;
                            if (ShowCircleHoverColor && IsHover)
                            {
                                bcColor = CircleHoverColor;
                            }

                            e.Graphics.FillEllipse(bcColor, (Width - CircleSize) / 2.0f - 1, Height - Padding.Bottom - CircleSize - 1, CircleSize, CircleSize);
                            e.Graphics.DrawFontImage(Symbol, SymbolSize, SymbolColor, new Rectangle(0, Height - Padding.Bottom - CircleSize, Width, CircleSize), symbolOffset.X, symbolOffset.Y, SymbolRotate);
                        }
                        else if (Image != null)
                        {
                            e.Graphics.DrawImage(Image, (Width - ImageSize.Width) / 2.0f, Height - Padding.Bottom - ImageSize.Height + imageTop, ImageSize.Width, ImageSize.Height);
                        }
                        #endregion
                    }
                    break;
                case TextImageRelation.ImageBeforeText:
                    {
                        #region  图片在前   
                        //字体图标
                        if (Symbol > 0 && Image == null)
                        {
                            Color bcColor = CircleColor;
                            if (!Enabled) bcColor = CircleDisabledColor;
                            if (ShowCircleHoverColor && IsHover)
                            {
                                bcColor = CircleHoverColor;
                            }

                            e.Graphics.FillEllipse(bcColor, Padding.Left - 1, (Height - CircleSize) / 2.0f - 1, CircleSize, CircleSize);
                            e.Graphics.DrawFontImage(Symbol, SymbolSize, SymbolColor, new Rectangle(Padding.Left, 0, CircleSize, Height), symbolOffset.X, symbolOffset.Y, SymbolRotate);
                        }
                        else if (Image != null)
                        {
                            e.Graphics.DrawImage(Image, ImageTop, (Height - ImageSize.Height) / 2.0f, ImageSize.Width, ImageSize.Height);
                        }

                        e.Graphics.DrawString(Text, Font, color, new Rectangle(0, 0, Width - Padding.Right, Height), ContentAlignment.MiddleRight);
                        #endregion
                    }
                    break;
                case TextImageRelation.TextBeforeImage:
                    {
                        #region  文本在前
                        e.Graphics.DrawString(Text, Font, color, new Rectangle(Padding.Left, 0, Width, Height), ContentAlignment.MiddleLeft);

                        //字体图标
                        if (Symbol > 0 && Image == null)
                        {
                            Color bcColor = CircleColor;
                            if (!Enabled) bcColor = CircleDisabledColor;
                            if (ShowCircleHoverColor && IsHover)
                            {
                                bcColor = CircleHoverColor;
                            }

                            e.Graphics.FillEllipse(bcColor, Width - Padding.Right - CircleSize - 1, (Height - CircleSize) / 2.0f - 1, CircleSize, CircleSize);
                            e.Graphics.DrawFontImage(Symbol, SymbolSize, SymbolColor, new Rectangle(Width - Padding.Right - CircleSize, 0, CircleSize, Height), symbolOffset.X, symbolOffset.Y, SymbolRotate);
                        }
                        else if (Image != null)
                        {
                            e.Graphics.DrawImage(Image, Width - Padding.Right - ImageSize.Width + imageTop, (Height - ImageSize.Height) / 2.0f, ImageSize.Width, ImageSize.Height);
                        }
                        #endregion
                    }
                    break;
                default:
                    {
                        #region  图片在上
                        //字体图标
                        if (Symbol > 0 && Image == null)
                        {
                            Color bcColor = CircleColor;
                            if (!Enabled) bcColor = CircleDisabledColor;
                            if (ShowCircleHoverColor && IsHover)
                            {
                                bcColor = CircleHoverColor;
                            }

                            e.Graphics.FillEllipse(bcColor, (Width - CircleSize) / 2.0f - 1, Padding.Top - 1, CircleSize, CircleSize);
                            e.Graphics.DrawFontImage(Symbol, SymbolSize, SymbolColor, new Rectangle(0, Padding.Top, Width, CircleSize), symbolOffset.X, symbolOffset.Y, SymbolRotate);
                        }
                        else if (Image != null)
                        {
                            e.Graphics.DrawImage(Image, (Width - ImageSize.Width) / 2.0f, ImageTop, ImageSize.Width, ImageSize.Height);
                        }

                        e.Graphics.DrawString(Text, Font, color, new Rectangle(0, 0, Width, Height - Padding.Bottom), ContentAlignment.BottomCenter);
                        #endregion
                    }
                    break;
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
        }

        [DefaultValue(null)]
        [Description("关联的TabControl"), Category("SunnyUI")]
        public UITabControl TabControl { get; set; }
    }
}