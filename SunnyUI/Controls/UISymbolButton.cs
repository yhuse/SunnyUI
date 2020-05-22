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
    public sealed class UISymbolButton : UIButton
    {
        private int _symbolSize = 24;
        private int _imageInterval = 2;

        [DefaultValue(24)]
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

        [DefaultValue(2)]
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

        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            Padding = new Padding(_symbolSize + _imageInterval * 2, Padding.Top, Padding.Right, Padding.Bottom);
            //填充文字
            Color color = GetForeColor();
            g.DrawString(Text, Font, color, Size, Padding, TextAlign);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //重绘父类
            base.OnPaint(e);

            //字体图标
            Color color = GetForeColor();

            float left = 0;
            float top = 0;
            SizeF ImageSize = e.Graphics.GetFontImageSize(Symbol, SymbolSize);
            SizeF TextSize = e.Graphics.MeasureString(Text, Font);

            if (TextAlign == ContentAlignment.TopCenter || TextAlign == ContentAlignment.TopLeft || TextAlign == ContentAlignment.TopRight)
            {
                top = Padding.Top;
            }

            if (TextAlign == ContentAlignment.MiddleCenter || TextAlign == ContentAlignment.MiddleLeft || TextAlign == ContentAlignment.MiddleRight)
            {
                top = Padding.Top + (Height - Padding.Top - Padding.Bottom - ImageSize.Height) / 2.0f;
            }

            if (TextAlign == ContentAlignment.BottomCenter || TextAlign == ContentAlignment.BottomLeft || TextAlign == ContentAlignment.BottomRight)
            {
                top = Height - Padding.Bottom - ImageSize.Height;
            }

            if (TextAlign == ContentAlignment.TopCenter || TextAlign == ContentAlignment.MiddleCenter || TextAlign == ContentAlignment.BottomCenter)
            {
                left = Padding.Left + (Width - TextSize.Width - Padding.Left - Padding.Right) / 2.0f;
                left = left - ImageInterval - ImageSize.Width;
            }

            if (TextAlign == ContentAlignment.TopLeft || TextAlign == ContentAlignment.MiddleLeft || TextAlign == ContentAlignment.BottomLeft)
            {
                left = ImageInterval;
            }

            if (TextAlign == ContentAlignment.TopRight || TextAlign == ContentAlignment.MiddleRight || TextAlign == ContentAlignment.BottomRight)
            {
                left = Width - Padding.Right - TextSize.Width - ImageInterval - ImageSize.Width;
            }

            if (Text.IsNullOrEmpty())
                e.Graphics.DrawFontImage(Symbol, SymbolSize, color, ImageInterval + (Width - ImageSize.Width) / 2.0f, (Height - ImageSize.Height) / 2.0f);
            else
                e.Graphics.DrawFontImage(Symbol, SymbolSize, color, left, top);
        }
    }
}