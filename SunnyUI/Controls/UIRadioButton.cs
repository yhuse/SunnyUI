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
 * 文件名称: UIRadioButton.cs
 * 文件说明: 单选框
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-16: V2.2.1 增加ReadOnly属性
 * 2020-04-25: V2.2.4 更新主题配置类
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Text")]
    [ToolboxItem(true)]
    public sealed class UIRadioButton : UIControl
    {
        public delegate void OnValueChanged(object sender, bool value);

        public event OnValueChanged ValueChanged;

        public UIRadioButton()
        {
            Cursor = Cursors.Hand;
            ShowRect = false;
            Size = new Size(150, 29);
            foreColor = UIStyles.Blue.CheckBoxForeColor;
            fillColor = UIStyles.Blue.CheckBoxColor;
            PaintOther += UIRadioButton_PaintOther;
        }

        private void UIRadioButton_PaintOther(object sender, PaintEventArgs e)
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
                UIRadioButton_PaintOther(this, null);
            }
        }

        [DefaultValue(false)]
        [Description("是否只读"), Category("SunnyUI")]
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

        private int _imageSize = 16;
        private int _imageInterval = 3;

        [DefaultValue(16)]
        [Description("按钮图片大小"), Category("SunnyUI")]
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

        [DefaultValue(3)]
        [Description("按钮图片文字间间隔"), Category("SunnyUI")]
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

        [DefaultValue(false)]
        [Description("是否选中"), Category("SunnyUI")]
        public bool Checked
        {
            get => _checked;
            set
            {
                _checked = value;

                if (value)
                {
                    try
                    {
                        if (Parent == null) return;
                        List<UIRadioButton> buttons = Parent.GetControls<UIRadioButton>();
                        foreach (var box in buttons)
                        {
                            if (box == this) continue;
                            if (box.GroupIndex != GroupIndex) continue;
                            if (box.Checked) box.Checked = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(@"UIRadioBox click error." + ex.Message);
                    }
                }

                ValueChanged?.Invoke(this, _checked);
                Invalidate();
            }
        }

        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            //设置按钮标题位置
            Padding = new Padding(_imageSize + _imageInterval * 2, Padding.Top, Padding.Right, Padding.Bottom);

            //填充文字
            Color color = ForeColor;
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
                g.FillEllipse(color, left, top, ImageSize, ImageSize);
                float pointSize = ImageSize - 4;
                g.FillEllipse(BackColor.IsValid() ? BackColor : Color.White,
                    left + ImageSize / 2.0f - pointSize / 2.0f,
                    top + ImageSize / 2.0f - pointSize / 2.0f,
                    pointSize, pointSize);

                pointSize = ImageSize - 8;
                g.FillEllipse(color,
                    left + ImageSize / 2.0f - pointSize / 2.0f,
                    top + ImageSize / 2.0f - pointSize / 2.0f,
                    pointSize, pointSize);
            }
            else
            {
                using (Pen pn = new Pen(color, 2))
                {
                    g.SetHighQuality();
                    g.DrawEllipse(pn, left + 1, top + 1, ImageSize - 2, ImageSize - 2);
                    g.SetDefaultQuality();
                }
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            if (!ReadOnly)
            {
                Checked = true;
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

        [DefaultValue(0)]
        public int GroupIndex { get; set; }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color RadioButtonColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }
    }
}