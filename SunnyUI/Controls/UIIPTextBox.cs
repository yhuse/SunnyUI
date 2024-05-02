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
 * 文件名称: UIIPTextBox.cs
 * 文件说明: IP地址输入框
 * 当前版本: V3.1
 * 创建日期: 2022-01-29
 *
 * 2022-01-29: V3.1.0 增加文件说明
 * 2022-11-02: V3.2.6 增加TextChanged事件
 * 2022-12-02: V3.3.0 删除TextChanged事件，增加ValueChanged事件
 * 2023-05-10: V3.3.6 修复Enabled切换时背景色不一致的问题
 * 2023-05-13: V3.3.6 重构DrawString函数
 * 2023-11-13: V3.5.2 重构主题
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultProperty("Text")]
    [DefaultEvent("ValueChanged")]
    public sealed partial class UIIPTextBox : UIPanel
    {
        private IPAddress _value;

        public UIIPTextBox()
        {
            InitializeComponent();
            InitializeComponentEnd = true;

            SetStyleFlags();
            ShowText = false;
            Width = 150;
            Height = 29;

            UIIPTextBox_SizeChanged(null, null);
            foreach (TextBox txt in Controls.OfType<TextBox>())
            {
                txt.AutoSize = false;
                txt.PreviewKeyDown += Txt_PreviewKeyDown;
                txt.KeyPress += Txt_KeyPress;
                txt.TextChanged += Txt_TextChanged;
                txt.Leave += Txt_Leave;
            }

            fillColor = UIStyles.Blue.EditorBackColor;
        }

        public event EventHandler ValueChanged;

        private void Txt_Leave(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.Text.IsNullOrEmpty()) return;
            if (t.Text.ToInt() > 255) t.Text = "255";
            if (t.Text.ToInt() < 0) t.Text = "0";
        }

        private void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8)
            {
                e.Handled = true;
                return;
            }

            if (t.SelectionStart == 2 && t.Text.Length == 2 && t.Text.ToInt() >= 26 && e.KeyChar != 8)
            {
                e.Handled = true;
                return;
            }

            if (t.SelectionStart == 2 && t.Text.Length == 2 && t.Text.ToInt() == 25 && e.KeyChar != 8)
            {
                if (e.KeyChar > '5')
                {
                    e.Handled = true;
                    return;
                }
            }

            if (e.KeyChar == 8) return;
            if (t.SelectionStart == 2) NextTextBox(t)?.Focus();
        }

        private void Txt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            TextBox t = (TextBox)sender;
            switch (e.KeyCode)
            {
                case Keys.OemPeriod:
                case Keys.Decimal:
                case Keys.Right:
                    if (t.Text.IsValid() && t.SelectionStart == t.TextLength) NextTextBox(t)?.Focus();
                    break;
                case Keys.Left:
                    if (t.SelectionStart == 0) PrevTextBox(t)?.Focus();
                    break;
                case Keys.Back:
                    if (t.SelectionStart == 0)
                    {
                        TextBox txtPrev = PrevTextBox(t);
                        if (txtPrev == null) return;
                        txtPrev.Focus();
                        if (!ReadOnly && txtPrev.TextLength > 0)
                            txtPrev.Text = txtPrev.Text.Remove(txtPrev.TextLength - 1, 1);

                        txtPrev.SelectionStart = txtPrev.TextLength;
                    }
                    break;
                case Keys.Tab:
                    SelectNextControl(this, true, true, false, true);
                    break;
            }
        }

        private TextBox PrevTextBox(TextBox box)
        {
            if (box == txt2) return txt1;
            if (box == txt3) return txt2;
            if (box == txt4) return txt3;

            return null;
        }

        private TextBox NextTextBox(TextBox box)
        {
            if (box == txt1) return txt2;
            if (box == txt2) return txt3;
            if (box == txt3) return txt4;

            return null;
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色，当值为背景色或透明色或空值则不填充"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public new Color FillColor
        {
            get
            {
                return fillColor;
            }
            set
            {
                if (fillColor != value)
                {
                    fillColor = value;
                    Invalidate();
                }

                AfterSetFillColor(value);
            }
        }

        public new void Focus()
        {
            base.Focus();
            txt1.Focus();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            txt1.Focus();
        }

        public void Clear()
        {
            txt1.Clear();
            txt2.Clear();
            txt3.Clear();
            txt4.Clear();
        }

        private void Txt_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.Text.IsNullOrEmpty()) return;
            if (t.Text.ToInt() > 255) t.Text = "255";
            if (t.Text.ToInt() < 0) t.Text = "0";

            string strIp = $"{txt1.Text}.{txt2.Text}.{txt3.Text}.{txt4.Text}";

            if (IPAddress.TryParse(strIp, out IPAddress ip))
            {
                _value = ip;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                _value = null;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        [Browsable(true)]
        public new string Text
        {
            get => Value == null ? string.Empty : Value.ToString();
            set
            {
                if (!IPAddress.TryParse(value, out IPAddress ip))
                    return;

                Value = ip;
            }
        }

        /// <summary>
        /// 获取输入的IP地址
        /// </summary>
        [DefaultValue(null)]
        public IPAddress Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
                if (value == null) return;

                byte[] bytes = value.GetAddressBytes();
                txt1.Text = bytes[0].ToString();
                txt2.Text = bytes[1].ToString();
                txt3.Text = bytes[2].ToString();
                txt4.Text = bytes[3].ToString();
            }
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);

            fillColor = uiColor.EditorBackColor;
            foreColor = UIFontColor.Primary;

            txt1.BackColor = Enabled ? fillColor : FillDisableColor;
            txt1.ForeColor = UIFontColor.Primary;
            txt2.BackColor = Enabled ? fillColor : FillDisableColor;
            txt2.ForeColor = UIFontColor.Primary;
            txt3.BackColor = Enabled ? fillColor : FillDisableColor;
            txt3.ForeColor = UIFontColor.Primary;
            txt4.BackColor = Enabled ? fillColor : FillDisableColor;
            txt4.ForeColor = UIFontColor.Primary;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            txt1.BackColor = Enabled ? fillColor : FillDisableColor;
            txt1.ForeColor = UIFontColor.Primary;
            txt2.BackColor = Enabled ? fillColor : FillDisableColor;
            txt2.ForeColor = UIFontColor.Primary;
            txt3.BackColor = Enabled ? fillColor : FillDisableColor;
            txt3.ForeColor = UIFontColor.Primary;
            txt4.BackColor = Enabled ? fillColor : FillDisableColor;
            txt4.ForeColor = UIFontColor.Primary;
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (TextBox txt in Controls.OfType<TextBox>().Where(t => t != txt1))
            {
                e.Graphics.DrawString(".", Font, ForeColor, new Rectangle(txt.Left - 5, 0, 5, Height), ContentAlignment.MiddleCenter, 2);
            }
        }

        protected override void AfterSetForeColor(Color color)
        {
            base.AfterSetForeColor(color);
            txt1.ForeColor = color;
            txt2.ForeColor = color;
            txt3.ForeColor = color;
            txt4.ForeColor = color;
        }

        protected override void AfterSetFillColor(Color color)
        {
            base.AfterSetFillColor(color);
            txt1.BackColor = Enabled ? color : FillDisableColor;
            txt2.BackColor = Enabled ? color : FillDisableColor;
            txt3.BackColor = Enabled ? color : FillDisableColor;
            txt4.BackColor = Enabled ? color : FillDisableColor;
        }

        [DefaultValue(false)]
        [Description("是否只读"), Category("SunnyUI")]
        public bool ReadOnly
        {
            get => txt1.ReadOnly;
            set
            {
                txt1.ReadOnly = value;
                txt1.BackColor = Enabled ? FillColor : FillDisableColor;
                txt2.ReadOnly = value;
                txt2.BackColor = Enabled ? FillColor : FillDisableColor;
                txt3.ReadOnly = value;
                txt3.BackColor = Enabled ? FillColor : FillDisableColor;
                txt4.ReadOnly = value;
                txt4.BackColor = Enabled ? FillColor : FillDisableColor;
            }
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            UIIPTextBox_SizeChanged(null, null);
        }

        /// <summary>
        /// 重载鼠标按下事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            ActiveControl = txt1;
        }

        private void InitializeComponent()
        {
            txt1 = new TextBox();
            txt2 = new TextBox();
            txt3 = new TextBox();
            txt4 = new TextBox();
            SuspendLayout();
            // 
            // txt1
            // 
            txt1.BackColor = Color.White;
            txt1.BorderStyle = BorderStyle.None;
            txt1.ImeMode = ImeMode.Disable;
            txt1.Location = new Point(6, 4);
            txt1.Margin = new Padding(3, 3, 10, 3);
            txt1.MaxLength = 3;
            txt1.Name = "txt1";
            txt1.Size = new Size(40, 22);
            txt1.TabIndex = 0;
            txt1.TextAlign = HorizontalAlignment.Center;
            // 
            // txt2
            // 
            txt2.BackColor = Color.White;
            txt2.BorderStyle = BorderStyle.None;
            txt2.ImeMode = ImeMode.Disable;
            txt2.Location = new Point(58, 3);
            txt2.Margin = new Padding(3, 3, 10, 3);
            txt2.MaxLength = 3;
            txt2.Name = "txt2";
            txt2.Size = new Size(40, 22);
            txt2.TabIndex = 0;
            txt2.TabStop = false;
            txt2.TextAlign = HorizontalAlignment.Center;
            // 
            // txt3
            // 
            txt3.BackColor = Color.White;
            txt3.BorderStyle = BorderStyle.None;
            txt3.ImeMode = ImeMode.Disable;
            txt3.Location = new Point(115, 3);
            txt3.Margin = new Padding(3, 3, 10, 3);
            txt3.MaxLength = 3;
            txt3.Name = "txt3";
            txt3.Size = new Size(40, 22);
            txt3.TabIndex = 0;
            txt3.TabStop = false;
            txt3.TextAlign = HorizontalAlignment.Center;
            // 
            // txt4
            // 
            txt4.BackColor = Color.White;
            txt4.BorderStyle = BorderStyle.None;
            txt4.ImeMode = ImeMode.Disable;
            txt4.Location = new Point(163, 3);
            txt4.Margin = new Padding(3, 3, 10, 3);
            txt4.MaxLength = 3;
            txt4.Name = "txt4";
            txt4.Size = new Size(40, 22);
            txt4.TabIndex = 0;
            txt4.TabStop = false;
            txt4.TextAlign = HorizontalAlignment.Center;
            // 
            // UIIPTextBox
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(txt4);
            Controls.Add(txt3);
            Controls.Add(txt2);
            Controls.Add(txt1);
            FillColor2 = Color.FromArgb(235, 243, 255);
            Name = "UIIPTextBox";
            Padding = new Padding(1);
            Size = new Size(219, 29);
            SizeChanged += UIIPTextBox_SizeChanged;
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.TextBox txt2;
        private System.Windows.Forms.TextBox txt3;
        private System.Windows.Forms.TextBox txt4;

        private void UIIPTextBox_SizeChanged(object sender, EventArgs e)
        {
            int w = (Width - RectSize * 2 - 28) / 4;
            txt1.Width = txt2.Width = txt3.Width = txt4.Width = w;
            txt1.Left = 8;
            txt2.Left = txt1.Right + 5;
            txt3.Left = txt2.Right + 5;
            txt4.Left = txt3.Right + 5;

            if (Height < UIGlobal.EditorMinHeight) Height = UIGlobal.EditorMinHeight;
            if (Height > UIGlobal.EditorMaxHeight) Height = UIGlobal.EditorMaxHeight;

            txt1.Height = Math.Min(Height - RectSize * 2, txt1.PreferredHeight);
            txt2.Height = Math.Min(Height - RectSize * 2, txt2.PreferredHeight);
            txt3.Height = Math.Min(Height - RectSize * 2, txt3.PreferredHeight);
            txt4.Height = Math.Min(Height - RectSize * 2, txt4.PreferredHeight);

            txt1.Top = txt2.Top = txt3.Top = txt4.Top = (Height - txt1.Height) / 2;
        }

        /// <summary>
        /// 重载字体变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (!InitializeComponentEnd) return;

            if (DefaultFontSize < 0 && txt1 != null) txt1.Font = this.Font;
            if (DefaultFontSize < 0 && txt2 != null) txt2.Font = this.Font;
            if (DefaultFontSize < 0 && txt3 != null) txt3.Font = this.Font;
            if (DefaultFontSize < 0 && txt4 != null) txt4.Font = this.Font;

            UIIPTextBox_SizeChanged(null, null);
            Invalidate();
        }
    }
}
