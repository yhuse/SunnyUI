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
 * 文件名称: UISymbolLabel.cs
 * 文件说明: 带字体图标的标签
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-04-23: V2.2.4 增加UISymbolLabel
 * 2021-12-24: V3.0.9 修复Dock和AutoSize同时设置的Bug
 * 2022-03-19: V3.1.1 重构主题配色
 * 2023-05-12: V3.3.6 重构DrawString函数
 * 2023-05-16: V3.3.6 重构DrawFontImage函数
 * 2023-10-26: V3.5.1 字体图标增加旋转角度参数SymbolRotate
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
        private int _imageInterval = 0;

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

        [DefaultValue(0)]
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
            Size TextSize = TextRenderer.MeasureText(Text, Font);

            int height = Math.Max(SymbolSize, TextSize.Height);
            int width = SymbolSize + ImageInterval + TextSize.Width;

            if (Dock == DockStyle.None && autoSize)
            {
                TextAlign = ContentAlignment.MiddleCenter;
                if (Width != width + 4) Width = width + 4;
                if (Height != height + 4) Height = height + 4;
            }

            Rectangle rect;
            switch (TextAlign)
            {
                case ContentAlignment.TopLeft:
                    rect = new Rectangle(Padding.Left, Padding.Top, width, height);
                    break;
                case ContentAlignment.TopCenter:
                    rect = new Rectangle((Width - width) / 2, Padding.Top, width, height);
                    break;
                case ContentAlignment.TopRight:
                    rect = new Rectangle(Width - width - Padding.Right, Padding.Top, width, height);
                    break;
                case ContentAlignment.MiddleLeft:
                    rect = new Rectangle(Padding.Left, (Height - height) / 2, width, height);
                    break;
                case ContentAlignment.MiddleCenter:
                    rect = new Rectangle((Width - width) / 2, (Height - height) / 2, width, height);
                    break;
                case ContentAlignment.MiddleRight:
                    rect = new Rectangle(Width - width - Padding.Right, (Height - height) / 2, width, height);
                    break;
                case ContentAlignment.BottomLeft:
                    rect = new Rectangle(Padding.Left, Height - Padding.Bottom - height, width, height);
                    break;
                case ContentAlignment.BottomCenter:
                    rect = new Rectangle((Width - width) / 2, Height - Padding.Bottom - height, width, height);
                    break;
                case ContentAlignment.BottomRight:
                    rect = new Rectangle(Width - width - Padding.Right, Height - Padding.Bottom - height, width, height);
                    break;
                default:
                    rect = new Rectangle((Width - width) / 2, (Height - height) / 2, width, height);
                    break;
            }

            if (Text.IsNullOrEmpty())
                e.Graphics.DrawFontImage(Symbol, SymbolSize, symbolColor, new RectangleF((Width - SymbolSize) / 2.0f, (Height - SymbolSize) / 2.0f, SymbolSize, SymbolSize), SymbolOffset.X, SymbolOffset.Y, SymbolRotate);
            else
                e.Graphics.DrawFontImage(Symbol, SymbolSize, symbolColor, new Rectangle(rect.Left, rect.Top, SymbolSize, rect.Height), SymbolOffset.X, SymbolOffset.Y, SymbolRotate);

            e.Graphics.DrawString(Text, Font, ForeColor, rect, ContentAlignment.MiddleRight);
        }

        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        { }
    }
}
