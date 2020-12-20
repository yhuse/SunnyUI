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
 * 文件名称: UISymbolButton.cs
 * 文件说明: 字体图标按钮
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-07-26: V2.2.6 增加Image属性，增加图片和文字的摆放位置
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
    public class UISymbolButton : UIButton
    {
        private int _symbolSize = 24;
        private int _imageInterval = 2;

        public UISymbolButton()
        {
            ShowText = false;
        }

        [DefaultValue(24)]
        [Description("字体图标大小"), Category("SunnyUI")]
        public int SymbolSize
        {
            get => _symbolSize;
            set
            {
                _symbolSize = Math.Max(value, 16);
                _symbolSize = Math.Min(value, 64);
                Invalidate();
            }
        }

        [DefaultValue(null)]
        [Description("图片"), Category("SunnyUI")]
        public Image Image { get; set; }

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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor(typeof(UIImagePropertyEditor), typeof(UITypeEditor))]
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

        protected override void OnPaintRect(Graphics g, GraphicsPath path)
        {
            if (IsCircle)
            {
                int size = Math.Min(Width, Height) - 2 - CircleRectWidth;
                using (Pen pn = new Pen(GetRectColor(), CircleRectWidth))
                {
                    g.SetHighQuality();
                    g.DrawEllipse(pn, (Width - size) / 2.0f, (Height - size) / 2.0f, size, size);
                    g.SetDefaultQuality();
                }
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
                            e.Graphics.DrawFontImage(Symbol, SymbolSize, color,
                                new RectangleF(
                                    (Width - ImageSize.Width) / 2.0f,
                                    Padding.Top + (Height - ImageSize.Height - Padding.Top - Padding.Bottom) / 2.0f,
                                      ImageSize.Width, ImageSize.Height));
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
                        e.Graphics.DrawFontImage(Symbol, SymbolSize, color,
                            new RectangleF((Width - allWidth) / 2.0f, (Height - ImageSize.Height) / 2.0f, ImageSize.Width, ImageSize.Height));
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
                        e.Graphics.DrawFontImage(Symbol, SymbolSize, color,
                            new RectangleF(left, top, ImageSize.Width, ImageSize.Height));
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