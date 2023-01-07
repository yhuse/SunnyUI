/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2023 ShenYongHua(沈永华).
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
 * 文件名称: UISymbolButton.cs
 * 文件说明: 字体图标按钮
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-07-26: V2.2.6 增加Image属性，增加图片和文字的摆放位置
 * 2022-01-05: V3.0.9 字体图标增加颜色设置
 * 2022-03-19: V3.1.1 重构主题配色
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    public class UISymbolButton : UIButton, ISymbol
    {
        private int _symbolSize = 24;
        private int _imageInterval = 2;

        public UISymbolButton()
        {
            ShowText = false;
        }

        private bool autoSize;

        [Browsable(false)]
        [Description("自动大小"), Category("SunnyUI")]
        public override bool AutoSize
        {
            get => autoSize;
            set => autoSize = false;
        }

        /// <summary>
        /// 字体图标大小
        /// </summary>
        [DefaultValue(24)]
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

        private Color symbolColor = Color.White;

        /// <summary>
        /// 字体图标颜色
        /// </summary>
        [Description("图标颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public Color SymbolColor
        {
            get => symbolColor;
            set
            {
                if (symbolColor != value)
                {
                    symbolColor = value;
                    SetStyleCustom();
                }
            }
        }

        private Color symbolHoverColor = Color.White;
        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
        [Description("图标鼠标移上时字体颜色")]
        public Color SymbolHoverColor
        {
            get => symbolHoverColor;
            set
            {
                if (symbolHoverColor != value)
                {
                    symbolHoverColor = value;
                    SetStyleCustom(false);
                }
            }
        }

        private Color symbolPressColor = Color.White;
        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
        [Description("图标鼠标按下时字体颜色")]
        public Color SymbolPressColor
        {
            get => symbolPressColor;
            set
            {
                if (symbolPressColor != value)
                {
                    symbolPressColor = value;
                    SetStyleCustom(false);
                }
            }
        }

        private Color symbolDisableColor = Color.FromArgb(109, 109, 103);
        [DefaultValue(typeof(Color), "109, 109, 103"), Category("SunnyUI")]
        [Description("图标不可用时字体颜色")]
        public Color SymbolDisableColor
        {
            get => symbolDisableColor;
            set
            {
                if (symbolDisableColor != value)
                {
                    symbolDisableColor = value;
                    SetStyleCustom();
                }
            }
        }

        private Color symbolSelectedColor = Color.White;
        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
        [Description("图标选中时字体颜色")]
        public Color SymbolSelectedColor
        {
            get => symbolSelectedColor;
            set
            {
                if (symbolSelectedColor != value)
                {
                    symbolSelectedColor = value;
                    SetStyleCustom();
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

            symbolColor = uiColor.ButtonForeColor;
            symbolHoverColor = uiColor.ButtonForeHoverColor;
            symbolPressColor = uiColor.ButtonForePressColor;
            symbolSelectedColor = uiColor.ButtonForeSelectedColor;
            symbolDisableColor = uiColor.ForeDisableColor;
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

        private ContentAlignment imageAlign = ContentAlignment.MiddleCenter;

        [DefaultValue(ContentAlignment.MiddleCenter)]
        [Description("图片放置位置"), Category("SunnyUI")]
        public ContentAlignment ImageAlign
        {
            get => imageAlign;
            set
            {
                imageAlign = value;
                Invalidate();
            }
        }

        [DefaultValue(2)]
        [Description("图片文字间间隔"), Category("SunnyUI")]
        public int ImageInterval
        {
            get => _imageInterval;
            set
            {
                _imageInterval = Math.Max(0, value);
                Invalidate();
            }
        }

        private bool _isCircle;

        [DefaultValue(false)]
        [Description("是否是圆形"), Category("SunnyUI")]
        public bool IsCircle
        {
            get => _isCircle;
            set
            {
                _isCircle = value;
                if (value)
                {
                    Text = "";
                }
                else
                {
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

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            if (IsCircle)
            {
                int size = Math.Min(Width, Height) - 2 - CircleRectWidth;
                g.FillEllipse(GetFillColor(), (Width - size) / 2.0f, (Height - size) / 2.0f, size, size);
            }
            else
            {
                base.OnPaintFill(g, path);
            }
        }

        private int circleRectWidth = 1;

        [DefaultValue(1)]
        [Description("圆形边框大小"), Category("SunnyUI")]
        public int CircleRectWidth
        {
            get => circleRectWidth;
            set
            {
                circleRectWidth = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 绘制边框颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintRect(Graphics g, GraphicsPath path)
        {
            if (IsCircle)
            {
                int size = Math.Min(Width, Height) - 2 - CircleRectWidth;
                using var pn = new Pen(GetRectColor(), CircleRectWidth);
                g.SetHighQuality();
                g.DrawEllipse(pn, (Width - size) / 2.0f, (Height - size) / 2.0f, size, size);
                g.SetDefaultQuality();
            }
            else
            {
                base.OnPaintRect(g, path);
            }
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            Invalidate();
        }

        /// <summary>
        /// 获取字体颜色
        /// </summary>
        /// <returns>颜色</returns>
        protected Color GetSymbolForeColor()
        {
            //文字
            Color color = lightStyle ? _style.Colors().ButtonForeLightColor : symbolColor;
            if (IsHover)
                color = symbolHoverColor;
            if (IsPress)
                color = symbolPressColor;
            if (selected)
                color = symbolSelectedColor;
            if (ShowFocusColor && Focused)
                color = symbolPressColor;
            return Enabled ? color : symbolDisableColor;
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //重绘父类
            base.OnPaint(e);

            SizeF ImageSize = new SizeF(0, 0);
            if (Symbol > 0)
                ImageSize = e.Graphics.GetFontImageSize(Symbol, SymbolSize);
            if (Image != null)
                ImageSize = Image.Size;

            //字体图标
            Color color = GetForeColor();
            SizeF TextSize = e.Graphics.MeasureString(Text, Font);

            if (ImageAlign == ContentAlignment.MiddleCenter && TextAlign == ContentAlignment.MiddleCenter)
            {
                if (ImageSize.Width.Equals(0))
                {
                    if (TextSize.Width > 0)
                        e.Graphics.DrawString(Text, Font, color, (Width - TextSize.Width) / 2.0f, (Height - TextSize.Height) / 2.0f);
                }
                else if (TextSize.Width.Equals(0))
                {
                    if (ImageSize.Width > 0)
                    {
                        if (Symbol > 0 && Image == null)
                        {
                            e.Graphics.DrawFontImage(Symbol, SymbolSize, GetSymbolForeColor(),
                                new RectangleF(
                                    (Width - ImageSize.Width) / 2.0f,
                                    Padding.Top + (Height - ImageSize.Height - Padding.Top - Padding.Bottom) / 2.0f,
                                      ImageSize.Width, ImageSize.Height), SymbolOffset.X, SymbolOffset.Y);
                        }

                        if (Image != null)
                        {
                            e.Graphics.DrawImage(Image,
                                (Width - ImageSize.Width) / 2.0f,
                                Padding.Top + (Height - ImageSize.Height - Padding.Top - Padding.Bottom) / 2.0f,
                                  ImageSize.Width, ImageSize.Height);
                        }
                    }
                }
                else
                {
                    float allWidth = ImageSize.Width + ImageInterval + TextSize.Width;

                    if (Symbol > 0 && Image == null)
                    {
                        e.Graphics.DrawFontImage(Symbol, SymbolSize, GetSymbolForeColor(),
                            new RectangleF((Width - allWidth) / 2.0f, (Height - ImageSize.Height) / 2.0f, ImageSize.Width, ImageSize.Height), SymbolOffset.X, SymbolOffset.Y);
                    }

                    if (Image != null)
                    {
                        e.Graphics.DrawImage(Image, (Width - allWidth) / 2.0f, (Height - ImageSize.Height) / 2.0f,
                            ImageSize.Width, ImageSize.Height);
                    }

                    e.Graphics.DrawString(Text, Font, color, (Width - allWidth) / 2.0f + ImageSize.Width + ImageInterval,
                        (Height - TextSize.Height) / 2.0f);
                }
            }
            else
            {
                float left = 0;
                float top = 0;

                if (ImageSize.Width > 0)
                {
                    switch (ImageAlign)
                    {
                        case ContentAlignment.TopLeft:
                            left = Padding.Left;
                            top = Padding.Top;
                            break;

                        case ContentAlignment.TopCenter:
                            left = (Width - ImageSize.Width) / 2.0f;
                            top = Padding.Top;
                            break;

                        case ContentAlignment.TopRight:
                            left = Width - Padding.Right - ImageSize.Width;
                            top = Padding.Top;
                            break;

                        case ContentAlignment.MiddleLeft:
                            left = Padding.Left;
                            top = (Height - ImageSize.Height) / 2.0f;
                            break;

                        case ContentAlignment.MiddleCenter:
                            left = (Width - ImageSize.Width) / 2.0f;
                            top = (Height - ImageSize.Height) / 2.0f;
                            break;

                        case ContentAlignment.MiddleRight:
                            left = Width - Padding.Right - ImageSize.Width;
                            top = (Height - ImageSize.Height) / 2.0f;
                            break;

                        case ContentAlignment.BottomLeft:
                            left = Padding.Left;
                            top = Height - Padding.Bottom - ImageSize.Height;
                            break;

                        case ContentAlignment.BottomCenter:
                            left = (Width - ImageSize.Width) / 2.0f;
                            top = Height - Padding.Bottom - ImageSize.Height;
                            break;

                        case ContentAlignment.BottomRight:
                            left = Width - Padding.Right - ImageSize.Width;
                            top = Height - Padding.Bottom - ImageSize.Height;
                            break;
                    }

                    if (Symbol > 0 && Image == null)
                    {
                        e.Graphics.DrawFontImage(Symbol, SymbolSize, GetSymbolForeColor(),
                            new RectangleF(left, top, ImageSize.Width, ImageSize.Height), SymbolOffset.X, SymbolOffset.Y);
                    }

                    if (Image != null)
                    {
                        e.Graphics.DrawImage(Image, left, top, ImageSize.Width, ImageSize.Height);
                    }
                }

                left = 0;
                top = 0;
                if (TextSize.Width > 0)
                {
                    switch (TextAlign)
                    {
                        case ContentAlignment.TopLeft:
                            left = Padding.Left;
                            top = Padding.Top;
                            break;

                        case ContentAlignment.TopCenter:
                            left = (Width - TextSize.Width) / 2.0f;
                            top = Padding.Top;
                            break;

                        case ContentAlignment.TopRight:
                            left = Width - Padding.Right - TextSize.Width;
                            top = Padding.Top;
                            break;

                        case ContentAlignment.MiddleLeft:
                            left = Padding.Left;
                            top = (Height - TextSize.Height) / 2.0f;
                            break;

                        case ContentAlignment.MiddleCenter:
                            left = (Width - TextSize.Width) / 2.0f;
                            top = (Height - TextSize.Height) / 2.0f;
                            break;

                        case ContentAlignment.MiddleRight:
                            left = Width - Padding.Right - TextSize.Width;
                            top = (Height - TextSize.Height) / 2.0f;
                            break;

                        case ContentAlignment.BottomLeft:
                            left = Padding.Left;
                            top = Height - Padding.Bottom - TextSize.Height;
                            break;

                        case ContentAlignment.BottomCenter:
                            left = (Width - TextSize.Width) / 2.0f;
                            top = Height - Padding.Bottom - TextSize.Height;
                            break;

                        case ContentAlignment.BottomRight:
                            left = Width - Padding.Right - TextSize.Width;
                            top = Height - Padding.Bottom - TextSize.Height;
                            break;
                    }

                    e.Graphics.DrawString(Text, Font, color, left, top);
                }
            }
        }
    }
}