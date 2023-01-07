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
 * 文件名称: UISymbolLabel.cs
 * 文件说明: 带字体图标的标签
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-04-23: V2.2.4 增加UISymbolLabel
 * 2021-12-24: V3.0.9 修复Dock和AutoSize同时设置的Bug
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
    [ToolboxItem(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    public sealed class UISymbolLabel : UIControl, ISymbol
    {
        private int _symbolSize = 24;
        private int _imageInterval = 2;

        private Color symbolColor;

        public UISymbolLabel()
        {
            SetStyleFlags(true, false);
            ShowRect = false;
            symbolColor = UIStyles.Blue.LabelForeColor;
            foreColor = UIStyles.Blue.LabelForeColor;
            Width = 170;
            Height = 35;
        }

        private bool autoSize;

        [Browsable(true)]
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

        /// <summary>
        /// 字体图标颜色
        /// </summary>
        [Description("图标颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "48, 48, 48")]
        public Color SymbolColor
        {
            get => symbolColor;
            set
            {
                symbolColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "48, 48, 48")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
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

        [DefaultValue(2)]
        [Description("图标和文字间间隔"), Category("SunnyUI")]
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
        [Description("显示为圆形"), Category("SunnyUI")]
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
            g.FillRectangle(BackColor, Bounds);
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

        /// <summary>
        /// 绘制前景颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            Padding = new Padding(_symbolSize + _imageInterval * 2, Padding.Top, Padding.Right, Padding.Bottom);
            //填充文字
            g.DrawString(Text, Font, foreColor, Size, Padding, TextAlign);
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            symbolColor = uiColor.LabelForeColor;
            foreColor = uiColor.LabelForeColor;
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //重绘父类
            base.OnPaint(e);

            float left = 0;
            float top = 0;
            SizeF ImageSize = e.Graphics.GetFontImageSize(Symbol, SymbolSize);
            SizeF TextSize = e.Graphics.MeasureString(Text, Font);

            if (Dock == DockStyle.None && autoSize)
            {
                int width = (int)(SymbolSize + ImageInterval * 3 + TextSize.Width);
                int height = (int)Math.Max(SymbolSize, TextSize.Height);
                if (Width != width) Width = width;
                if (Height != height) Height = height;
            }

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
                e.Graphics.DrawFontImage(Symbol, SymbolSize, symbolColor, ImageInterval + (Width - ImageSize.Width) / 2.0f, (Height - ImageSize.Height) / 2.0f, SymbolOffset.X, SymbolOffset.Y);
            else
                e.Graphics.DrawFontImage(Symbol, SymbolSize, symbolColor, left, top, SymbolOffset.X, SymbolOffset.Y);
        }
    }
}
