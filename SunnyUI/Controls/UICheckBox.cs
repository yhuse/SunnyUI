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
* 文件名称: UICheckBox.cs
* 文件说明: 复选框
* 当前版本: V2.2
* 创建日期: 2020-01-01
*
* 2020-01-01: V2.2.0 增加文件说明
* 2020-04-16: V2.2.1 增加ReadOnly属性
* 2020-04-25: V2.2.4 更新主题配置类
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Text")]
    [ToolboxItem(true)]
    public class UICheckBox : UIControl
    {
        public UICheckBox()
        {
            Cursor = Cursors.Hand;
            ShowRect = false;
            Size = new Size(150, 29);
            foreColor = UIStyles.Blue.CheckBoxForeColor;
            fillColor = UIStyles.Blue.CheckBoxColor;
            SetStyle(ControlStyles.StandardDoubleClick, UseDoubleClick);
            PaintOther += UICheckBox_PaintOther;
        }

        private void UICheckBox_PaintOther(object sender, PaintEventArgs e)
        {
            if (AutoSize)
            {
                SizeF sf = Text.MeasureString(Font);
                int w = (int)sf.Width + ImageSize + 3;
                int h = Math.Max(ImageSize, (int)sf.Height) + 2;
                if (Width != w) Width = w;
                if (Height != h) Height = h;
            }
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
                UICheckBox_PaintOther(this, null);
            }
        }

        public delegate void OnValueChanged(object sender, bool value);

        public event OnValueChanged ValueChanged;

        private int _imageSize = 16;
        private int _imageInterval = 3;

        [DefaultValue(16)]
        [Description("图标大小"), Category("SunnyUI")]
        public int ImageSize
        {
            get => _imageSize;
            set
            {
                _imageSize = Math.Max(value, 16);
                _imageSize = Math.Min(value, 64);
                Invalidate();
            }
        }

        [DefaultValue(false)]
        public bool ReadOnly { get; set; }

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

        [Description("是否选中"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool Checked
        {
            get => _checked;
            set
            {
                _checked = value;
                ValueChanged?.Invoke(this, _checked);
                Invalidate();
            }
        }

        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            //设置按钮标题位置
            Padding = new Padding(_imageSize + _imageInterval * 2, Padding.Top, Padding.Right, Padding.Bottom);

            //填充文字
            Color color = foreColor;
            color = Enabled ? color : UIDisableColor.Fore;

            g.DrawString(Text, Font, color, Size, Padding, ContentAlignment.MiddleLeft);
        }

        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            //图标
            float top = Padding.Top - 1 + (Height - Padding.Top - Padding.Bottom - ImageSize) / 2.0f;
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
                using (Pen pn = new Pen(color, 2))
                {
                    g.SetHighQuality();
                    g.DrawLines(pn, CheckMarkLine);
                    g.SetDefaultQuality();
                }
            }
            else
            {
                using (Pen pn = new Pen(color, 1))
                {
                    g.DrawRoundRectangle(pn, new Rectangle((int)left + 1, (int)top + 1, ImageSize - 2, ImageSize - 2), 1);
                    g.DrawRectangle(pn, new Rectangle((int)left + 2, (int)top + 2, ImageSize - 4, ImageSize - 4));
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if (!ReadOnly)
            {
                Checked = !Checked;
            }

            base.OnClick(e);
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            fillColor = uiColor.CheckBoxColor;
            foreColor = uiColor.CheckBoxForeColor;
            Invalidate();
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