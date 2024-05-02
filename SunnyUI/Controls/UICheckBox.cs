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
 * 文件名称: UICheckBox.cs
 * 文件说明: 复选框
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-16: V2.2.1 增加ReadOnly属性
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2021-04-26: V3.0.3 增加默认事件CheckedChanged
 * 2022-03-19: V3.1.1 重构主题配色
 * 2023-05-12: V3.3.6 重构DrawString函数
 * 2023-11-07: V3.5.2 增加修改图标大小
 * 2023-12-04: V3.6.1 增加属性可修改图标大小
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 复选框
    /// </summary>
    [DefaultEvent("CheckedChanged")]
    [DefaultProperty("Checked")]
    [ToolboxItem(true)]
    public class UICheckBox : UIControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UICheckBox()
        {
            SetStyleFlags();
            base.Cursor = Cursors.Hand;
            ShowRect = false;
            Size = new Size(150, 29);
            SetStyle(ControlStyles.StandardDoubleClick, UseDoubleClick);

            ForeColor = UIStyles.Blue.CheckBoxForeColor;
            fillColor = UIStyles.Blue.CheckBoxColor;
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (AutoSize && Dock == DockStyle.None)
            {
                Size sf = TextRenderer.MeasureText(Text, Font);
                int w = sf.Width + CheckBoxSize + 3;
                int h = Math.Max(CheckBoxSize, sf.Height) + 2;
                if (Width != w) Width = w;
                if (Height != h) Height = h;
            }
        }

        private bool autoSize;

        /// <summary>
        /// 自动大小
        /// </summary>
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
        /// 值改变事件
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="value">值</param>
        public delegate void OnValueChanged(object sender, bool value);

        /// <summary>
        /// 值改变事件
        /// </summary>
        public event OnValueChanged ValueChanged;

        private int _imageSize = 16;
        private int _imageInterval = 3;

        [DefaultValue(16)]
        [Description("图标大小"), Category("SunnyUI")]
        [Browsable(true)]
        public int CheckBoxSize
        {
            get => _imageSize;
            set
            {
                _imageSize = Math.Max(value, 16);
                _imageSize = Math.Min(value, 64);
                Invalidate();
            }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        [DefaultValue(false)]
        [Description("是否只读"), Category("SunnyUI")]
        public bool ReadOnly { get; set; }

        /// <summary>
        /// 图标与文字之间间隔
        /// </summary>
        [DefaultValue(3)]
        [Description("图标与文字之间间隔"), Category("SunnyUI")]
        public int ImageInterval
        {
            get => _imageInterval;
            set
            {
                _imageInterval = Math.Max(1, value);
                Invalidate();
            }
        }

        private bool _checked;

        /// <summary>
        /// 是否选中
        /// </summary>
        [Description("是否选中"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool Checked
        {
            get => _checked;
            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    ValueChanged?.Invoke(this, _checked);
                    CheckedChanged?.Invoke(this, new EventArgs());
                }

                Invalidate();
            }
        }

        /// <summary>
        /// 值改变事件
        /// </summary>
        public event EventHandler CheckedChanged;

        /// <summary>
        /// 绘制前景颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            //填充文字
            Color color = ForeColor;
            color = Enabled ? color : UIDisableColor.Fore;
            Rectangle rect = new Rectangle(_imageSize + _imageInterval * 2, 0, Width - _imageSize + _imageInterval * 2, Height);
            g.DrawString(Text, Font, color, rect, ContentAlignment.MiddleLeft);
        }

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            int ImageSize = CheckBoxSize;
            //图标
            float top = (Height - ImageSize) / 2.0f;
            float left = Text.IsValid() ? ImageInterval : (Width - ImageSize) / 2.0f;
            Color color = Enabled ? fillColor : foreDisableColor;
            if (Checked)
            {
                g.FillRoundRectangle(color, new Rectangle((int)left, (int)top, ImageSize, ImageSize), 1);
                color = BackColor.IsValid() ? BackColor : Color.White;
                Point pt2 = new Point((int)(left + ImageSize * 2 / 5.0f), (int)(top + ImageSize * 3 / 4.0f) - (ImageSize.Div(10)));
                Point pt1 = new Point((int)left + 2 + ImageSize.Div(10), pt2.Y - (pt2.X - 2 - ImageSize.Div(10) - (int)left));
                Point pt3 = new Point((int)left + ImageSize - 2 - ImageSize.Div(10), pt2.Y - (ImageSize - pt2.X - 2 - ImageSize.Div(10)) - (int)left);

                PointF[] CheckMarkLine = { pt1, pt2, pt3 };
                using Pen pn = new Pen(color, 2);
                g.SetHighQuality();
                g.DrawLines(pn, CheckMarkLine);
                g.SetDefaultQuality();
            }
            else
            {
                using Pen pn = new Pen(color, 1);
                g.DrawRoundRectangle(pn, new Rectangle((int)left + 1, (int)top + 1, ImageSize - 2, ImageSize - 2), 1);
                g.DrawRectangle(pn, new Rectangle((int)left + 2, (int)top + 2, ImageSize - 4, ImageSize - 4));
            }
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnClick(EventArgs e)
        {
            if (!ReadOnly)
            {
                Checked = !Checked;
            }

            base.OnClick(e);
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            fillColor = uiColor.CheckBoxColor;
            ForeColor = uiColor.CheckBoxForeColor;
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color CheckBoxColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }
    }
}