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
 * 文件名称: UIEdit.cs
 * 文件说明: 文本输入框
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(false)]
    public sealed class UIEdit : TextBox
    {
        private bool canEmpty;
        private int decLength = 2;
        private bool hasMaxValue;
        private bool hasMinValue;
        private string mask = "0.00";
        private double maxValue = int.MaxValue;
        private double minValue = int.MinValue;
        private UITextBox.UIEditType _uiEditType = UITextBox.UIEditType.String;

        public UIEdit()
        {
            //设置为单选边框
            BorderStyle = BorderStyle.FixedSingle;
            Font = UIFontColor.Font;
            ForeColor = UIFontColor.Primary;
            Width = 150;
            MaxLength = 32767;
        }

        private string watermark;

        [DefaultValue(null)]
        public string Watermark
        {
            get => watermark;
            set
            {
                watermark = value;
                Win32.User.SendMessage(Handle, 0x1501, (int)IntPtr.Zero, value);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Down)
            {
                if (e.KeyData == Keys.Enter)
                {
                    EnterKeyPress?.Invoke(this, e);
                }

                if (EnterAsTab)
                {
                    SendKeys.Send("{tab}");
                }

                e.Handled = true;
            }

            if (e.KeyData == Keys.Up)
            {
                if (EnterAsTab)
                {
                    SendKeys.Send("+{TAB}");
                }

                e.Handled = true;
            }

            if (e.Control && e.KeyCode == Keys.A)
            {
                SelectAll();
                e.SuppressKeyPress = true;
            }

            if (e.Control && e.KeyCode == Keys.C)
            {
                Copy();
                e.SuppressKeyPress = true;
            }

            if (e.Control && e.KeyCode == Keys.V)
            {
                Paste();
                e.SuppressKeyPress = true;
            }

            if (e.Control && e.KeyCode == Keys.X)
            {
                Cut();
                e.SuppressKeyPress = true;
            }

            base.OnKeyDown(e);
        }

        public bool EnterAsTab { get; set; }

        public event EventHandler EnterKeyPress;

        [DefaultValue(false), Category("SunnyUI"), Description("整型、浮点型可以为空")]
        public bool CanEmpty
        {
            get => canEmpty;
            set
            {
                if (canEmpty != value)
                {
                    canEmpty = value;
                    Invalidate();
                }
            }
        }

        [
            DefaultValue(UITextBox.UIEditType.String),
            Category("SunnyUI"),
            Description("设置编辑框输入内容属性")
        ]
        public UITextBox.UIEditType Type
        {
            get => _uiEditType;
            set
            {
                if (_uiEditType == value) return;
                _uiEditType = value;

                switch (_uiEditType)
                {
                    case UITextBox.UIEditType.Double:
                        if (!CanEmpty)
                            if (!Text.IsDouble())
                                Text = mask;
                        break;

                    case UITextBox.UIEditType.Integer:
                        if (!CanEmpty)
                            if (!Text.IsInt())
                                Text = @"0";
                        break;

                    case UITextBox.UIEditType.String:
                        Text = "";
                        break;
                }

                CheckMaxMin();
                if (DesignMode)
                    Invalidate();
            }
        }

        [DefaultValue(2)]
        public int DecLength
        {
            get => decLength;
            set
            {
                if (decLength != value)
                {
                    decLength = value;
                    if (decLength < 0)
                    {
                        decLength = 0;
                    }

                    if (_uiEditType == UITextBox.UIEditType.Double)
                    {
                        mask = DecimalToMask(decLength);
                        Text = mask;
                        Invalidate();
                    }
                }
            }
        }

        [DefaultValue(false)]
        public bool HasMaxValue
        {
            get => hasMaxValue;
            set
            {
                if (hasMaxValue != value)
                {
                    hasMaxValue = value;
                    CheckMaxMin();
                    Invalidate();
                }
            }
        }

        [DefaultValue(false)]
        public bool HasMinValue
        {
            get => hasMinValue;
            set
            {
                if (hasMinValue != value)
                {
                    hasMinValue = value;
                    CheckMaxMin();
                    Invalidate();
                }
            }
        }

        [DefaultValue(0)]
        public double DoubleValue
        {
            get
            {
                if (Text == "" && CanEmpty) return 0;
                return Text.ToDouble();
            }
            set
            {
                CheckMaxMin();
                Text = value.ToString("f" + decLength);
            }
        }

        [DefaultValue(0)]
        public int IntValue
        {
            get
            {
                if (Text == "" && CanEmpty) return 0;
                return Text.ToInt();
            }
            set
            {
                CheckMaxMin();
                Text = value.ToString();
            }
        }

        [DefaultValue(int.MaxValue)]
        public double MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = value;
                if (maxValue < minValue)
                    minValue = maxValue;
                CheckMaxMin();
                Invalidate();
            }
        }

        [DefaultValue(int.MinValue)]
        public double MinValue
        {
            get => minValue;
            set
            {
                minValue = value;
                if (minValue > maxValue)
                    maxValue = minValue;
                CheckMaxMin();
                Invalidate();
            }
        }

        private string DecimalToMask(int iDecimal)
        {
            if (iDecimal == 0)
                return "0";
            var str = "0.";
            for (int i = 1; i <= iDecimal; i++)
                str = str + "0";
            return str;
        }

        private int SubCharCount(string str, char subChar)
        {
            string[] IDList = str.Split(subChar);
            return IDList.Length - 1;
        }

        private bool StringIndexIsChar(string str, int idx, char inChar)
        {
            if (str == "")
                return false;
            if (idx >= str.Length)
                return false;
            char[] cl = str.ToCharArray();
            return cl[idx] == inChar;
        }

        private bool IsValidChar(string str, char KeyChar, int pos)
        {
            bool b;
            if (_uiEditType == UITextBox.UIEditType.Integer)
            {
                b = char.IsDigit(KeyChar);
                if (b)
                {
                    if ((str.IndexOf('+') >= 0) | (str.IndexOf('-') >= 0))
                    {
                        if ((pos == 1) & (SelectionLength > 0))
                            return true;

                        b = pos > 1;
                    }
                    return b;
                }

                b = KeyChar.Equals('+');
                if (b)
                {
                    if (str == "") return true;
                    if (pos != 1) return false;
                    b = ((str.IndexOf('+') == -1) & (str.IndexOf('-') == -1)) | (SelectionLength > 0);
                    return b;
                }

                b = KeyChar.Equals('-');
                if (b)
                {
                    if (str == "") return true;
                    if (pos != 1) return false;
                    b = ((str.IndexOf('+') == -1) & (str.IndexOf('-') == -1)) | (SelectionLength > 0);
                    return b;
                }

                return false;
            }

            if (_uiEditType == UITextBox.UIEditType.Double)
            {
                b = char.IsDigit(KeyChar);
                if (b)
                {
                    if ((str.IndexOf('+') >= 0) | (str.IndexOf('-') >= 0))
                    {
                        if ((pos == 1) & (SelectionLength > 0))
                            return true;

                        b = pos > 1;
                    }
                    return b;
                }

                b = KeyChar.Equals('.');
                if (b)
                {
                    if (str == "") return true;
                    if (SubCharCount(str, KeyChar) != 0) return false;
                    if ((str.IndexOf('+') >= 0) | (str.IndexOf('-') >= 0))
                    {
                        if ((pos == 1) & (SelectionLength > 0))
                            return true;

                        b = pos > 1;
                    }
                    return b;
                }

                b = KeyChar.Equals('+');
                if (b)
                {
                    if (str == "") return true;
                    if (pos != 1) return false;
                    b = ((str.IndexOf('+') == -1) & (str.IndexOf('-') == -1)) | (SelectionLength > 0);
                    return b;
                }

                b = KeyChar.Equals('-');
                if (b)
                {
                    if (str == "") return true;
                    if (pos != 1) return false;
                    b = ((str.IndexOf('+') == -1) & (str.IndexOf('-') == -1)) | (SelectionLength > 0);
                    return b;
                }

                return false;
            }

            return true;
        }

        public void CheckMaxMin()
        {
            if (_uiEditType == UITextBox.UIEditType.Integer)
            {
                if (Text == "" && CanEmpty) return;

                if (!int.TryParse(Text, out var a))
                    Text = @"0";

                if (hasMaxValue)
                {
                    var m = (int)Math.Floor(maxValue);
                    if (a > m)
                        a = m;
                }
                if (hasMinValue)
                {
                    var m = (int)Math.Ceiling(minValue);
                    if (a < m)
                        a = m;
                }

                Text = a.ToString();
            }

            if (_uiEditType == UITextBox.UIEditType.Double)
            {
                if (Text == "" && CanEmpty) return;

                if (!double.TryParse(Text, out var a))
                    Text = a.ToString("f" + decLength);

                if (hasMaxValue)
                {
                    if (a > maxValue)
                        a = maxValue;
                }

                if (hasMinValue)
                {
                    if (a < minValue)
                        a = minValue;
                }

                Text = a.ToString("f" + decLength);
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            //以下代码  取消按下回车或esc的“叮”声
            if (e.KeyChar == Convert.ToChar(13) || e.KeyChar == Convert.ToChar(27))
            {
                e.Handled = true;
            }
            else if (e.KeyChar == 8)
            {
            }
            else if (!(IsValidChar(Text, e.KeyChar, SelectionStart + 1) & (e.KeyChar >= 32)))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        /// <summary>
        ///     在得到焦点时修改文体框的背景色
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            if (FocusedSelectAll)
            {
                SelectAll();
            }
            else
            {
                SelectionStart = Text.Length;
                SelectionLength = 0;
            }
        }

        [DefaultValue(false)]
        public bool FocusedSelectAll { get; set; }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);

            //如果为整形,为空时自动为0
            if (_uiEditType == UITextBox.UIEditType.Integer)
            {
                if (Text == "" && CanEmpty) return;

                if (Text == "")
                    Text = "0";

                if (!Text.IsInt())
                    Text = "0";
            }

            //如果为浮点,检查.前后是否为空,为空加0
            if (_uiEditType == UITextBox.UIEditType.Double)
            {
                if (Text == "" && CanEmpty) return;

                if (StringIndexIsChar(Text, 0, '.'))
                    Text = @"0" + Text;

                if (StringIndexIsChar(Text, Text.Length - 1, '.'))
                    Text = Text + @"0";

                if (StringIndexIsChar(Text, 0, '+') || StringIndexIsChar(Text, 0, '+'))
                    if (StringIndexIsChar(Text, 1, '.'))
                        Text = Text.Insert(1, @"0");

                if (!double.TryParse(Text, out var doubleValue))
                    Text = mask;

                Text = doubleValue.ToString("f" + decLength);
            }

            CheckMaxMin();
            //Invalidate();
        }
    }
}