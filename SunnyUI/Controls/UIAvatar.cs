/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2022 ShenYongHua(沈永华).
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
 * 文件名称: UIAvatar.cs
 * 文件说明: 头像
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
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
    /// <summary>
    /// 头像
    /// </summary>
    [DefaultEvent("Click")]
    [DefaultProperty("Symbol")]
    [ToolboxItem(true)]
    public sealed class UIAvatar : UIControl, ISymbol, IZoomScale
    {
        /// <summary>
        /// 头像图标类型
        /// </summary>
        public enum UIIcon
        {
            /// <summary>
            /// 图像
            /// </summary>
            Image,

            /// <summary>
            /// 符号
            /// </summary>
            Symbol,

            /// <summary>
            /// 文字
            /// </summary>
            Text
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public UIAvatar()
        {
            SetStyleFlags(true, false);
            Width = Height = 60;
            ShowText = false;
            ShowRect = false;

            fillColor = UIStyles.Blue.AvatarFillColor;
            foreColor = UIStyles.Blue.AvatarForeColor;
        }

        private int avatarSize = 60;
        private int baseAvatorSize = 60;

        [DefaultValue(60), Description("头像大小"), Category("SunnyUI")]
        public int AvatarSize
        {
            get => avatarSize;
            set
            {
                baseAvatorSize = value;
                avatarSize = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "Silver")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("前景颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);

            fillColor = uiColor.AvatarFillColor;
            foreColor = uiColor.AvatarForeColor;
        }

        private UIIcon icon = UIIcon.Symbol;

        /// <summary>
        /// 显示方式：图像（Image）、符号（Symbol）、文字（Text）
        /// </summary>
        [DefaultValue(UIIcon.Symbol), Description("显示方式：图像（Image）、符号（Symbol）、文字（Text）"), Category("SunnyUI")]
        public UIIcon Icon
        {
            get => icon;
            set
            {
                if (icon != value)
                {
                    icon = value;
                    Invalidate();
                }
            }
        }

        private UIShape sharpType = UIShape.Circle;

        /// <summary>
        /// 显示形状：圆形，正方形
        /// </summary>
        [DefaultValue(UIShape.Circle), Description("显示形状：圆形，正方形"), Category("SunnyUI")]
        public UIShape Shape
        {
            get => sharpType;
            set
            {
                if (sharpType != value)
                {
                    sharpType = value;
                    Invalidate();
                }
            }
        }

        private Image image;

        /// <summary>
        /// 图片
        /// </summary>
        [DefaultValue(typeof(Image), "null"), Description("图片"), Category("SunnyUI")]
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

        private int symbolSize = 45;
        private int baseSymbolSize = 45;

        /// <summary>
        /// 图标大小
        /// </summary>
        [DefaultValue(45), Description("图标大小"), Category("SunnyUI")]
        public int SymbolSize
        {
            get => symbolSize;
            set
            {
                if (symbolSize != value)
                {
                    symbolSize = Math.Max(value, 16);
                    symbolSize = Math.Min(value, 128);
                    baseSymbolSize = symbolSize;
                    Invalidate();
                }
            }
        }

        private int symbol = 61447;

        /// <summary>
        /// 图标字符
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor("Sunny.UI.UIImagePropertyEditor, " + AssemblyRefEx.SystemDesign, typeof(UITypeEditor))]
        [DefaultValue(61447), Description("图标"), Category("SunnyUI")]
        public int Symbol
        {
            get => symbol;
            set
            {
                if (symbol != value)
                {
                    symbol = value;
                    Invalidate();
                }
            }
        }

        private Point symbolOffset = new Point(0, 0);

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

        private Point textOffset = new Point(0, 0);

        [DefaultValue(typeof(Point), "0, 0")]
        [Description("文字的偏移位置"), Category("SunnyUI")]
        public Point TextOffset
        {
            get => textOffset;
            set
            {
                textOffset = value;
                Invalidate();
            }
        }

        private Point imageOffset = new Point(0, 0);

        [DefaultValue(typeof(Point), "0, 0")]
        [Description("文字的偏移位置"), Category("SunnyUI")]
        public Point ImageOffset
        {
            get => imageOffset;
            set
            {
                imageOffset = value;
                Invalidate();
            }
        }

        /// <summary>
        /// OnPaintFill
        /// </summary>
        /// <param name="g">g</param>
        /// <param name="path">path</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            Rectangle rect = new Rectangle((Width - avatarSize) / 2, (Height - avatarSize) / 2, avatarSize, avatarSize);

            switch (Shape)
            {
                case UIShape.Circle:
                    g.FillEllipse(fillColor, rect);
                    break;

                case UIShape.Square:
                    g.FillRoundRectangle(fillColor, rect, 5);
                    break;
            }
        }

        [Browsable(false), DefaultValue(0), Description("水平偏移"), Category("SunnyUI")]
        public int OffsetX { get; set; } = 0;

        [Browsable(false), DefaultValue(0), Description("垂直偏移"), Category("SunnyUI")]
        public int OffsetY { get; set; } = 0;

        public event PaintEventHandler PaintAgain;

        /// <summary>
        /// OnPaint
        /// </summary>
        /// <param name="e">e</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Width <= 0 || Height <= 0)
            {
                return;
            }

            if (Icon == UIIcon.Image)
            {
                int size = avatarSize;
                if (Image == null)
                {
                    return;
                }

                float sc1 = Image.Width * 1.0f / size;
                float sc2 = Image.Height * 1.0f / size;

                Bitmap scaleImage = ((Bitmap)Image).ResizeImage((int)(Image.Width * 1.0 / Math.Min(sc1, sc2) + 0.5),
                    (int)(Image.Height * 1.0 / Math.Min(sc1, sc2) + 0.5));

                Bitmap bmp = scaleImage.Split(size, Shape);
                e.Graphics.DrawImage(bmp, (Width - avatarSize) / 2 + 1 + ImageOffset.X, (Height - avatarSize) / 2 + 1 + ImageOffset.Y);
                bmp.Dispose();
                scaleImage.Dispose();
                e.Graphics.SetHighQuality();

                using (Pen pn = new Pen(BackColor, 4))
                {
                    if (Shape == UIShape.Circle)
                    {
                        e.Graphics.DrawEllipse(pn, (Width - avatarSize) / 2 + 1 + ImageOffset.X, (Height - avatarSize) / 2 + 1 + ImageOffset.Y, size, size);
                    }

                    if (Shape == UIShape.Square)
                    {
                        e.Graphics.DrawRoundRectangle(pn, (Width - avatarSize) / 2 + 1 + ImageOffset.X, (Height - avatarSize) / 2 + 1 + ImageOffset.Y, size, size, 5);
                    }
                }

                e.Graphics.SetDefaultQuality();
            }

            if (Icon == UIIcon.Symbol)
            {
                e.Graphics.DrawFontImage(symbol, symbolSize, ForeColor, new Rectangle((Width - avatarSize) / 2 + 1 + SymbolOffset.X,
                    (Height - avatarSize) / 2 + 1 + SymbolOffset.Y, avatarSize, avatarSize));
            }

            if (Icon == UIIcon.Text)
            {
                SizeF sf = e.Graphics.MeasureString(Text, Font);
                e.Graphics.DrawString(Text, Font, foreColor, (Width - sf.Width) / 2.0f + TextOffset.X, (Height - sf.Height) / 2.0f + 1 + TextOffset.Y);
            }

            PaintAgain?.Invoke(this, e);
        }

        public override void SetZoomScale(float scale)
        {
            base.SetZoomScale(scale);
            avatarSize = UIZoomScale.Calc(baseAvatorSize, scale);
            symbolSize = UIZoomScale.Calc(baseSymbolSize, scale);
            Invalidate();
        }
    }
}