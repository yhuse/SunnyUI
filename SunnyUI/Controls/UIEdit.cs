﻿/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2025 ShenYongHua(沈永华).
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
 * 文件名称: UIEdit.cs
 * 文件说明: 文本输入框
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2022-12-18: V3.3.0 修复了一个最小值大于0是，显示类型为字符串Text为空仍有显示的问题
 * 2023-03-07: V3.3.3 修复了删除为空时小数位数和设置值不一致的问题
 * 2023-04-19: V3.3.5 修复了最大值最小值范围判断的问题
 * 2023-05-12: V3.3.6 重构DrawString函数
 * 2023-06-14: V3.3.8 修复输入范围判断的问题
 * 2025-06-10: V3.8.4 多行时水印文字在左上角显示
 * 2025-07-16: V3.8.6 重写水印文字的绘制逻辑
 ******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(false)]
    public class UIEdit : TextBox
    {
        private bool canEmpty;
        private int decLength = 2;
        private UITextBox.UIEditType _uiEditType = UITextBox.UIEditType.String;

        public UIEdit()
        {
            //设置为单选边框
            BorderStyle = BorderStyle.FixedSingle;
            base.Font = UIStyles.Font();
            base.ForeColor = UIFontColor.Primary;
            Width = 150;
            base.MaxLength = 32767;
            this.Leave += new EventHandler(ThisWasLeaved);
            SetUserPaintStyle(false);
        }

        private void WaterMarkContainer_DoubleClick(object sender, EventArgs e)
        {
            this.Focus();
            base.OnDoubleClick(EventArgs.Empty);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            SetUserPaintStyle(NeedUserPaint());
        }

        private void ThisWasLeaved(object sender, EventArgs e)
        {
            CheckMaxMin();
        }

        public bool TouchPressClick { get; set; } = false;

        private int textLength = 0;

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            SetUserPaintStyle(NeedUserPaint());

            if (Text.IsValid())
            {
                if (Text.Length > textLength && Type == UITextBox.UIEditType.Integer || Type == UITextBox.UIEditType.Double)
                {
                    CheckMaxMin(true);
                }
            }

            textLength = Text.Length;
        }

        private bool _userPaint = true;
        private void SetUserPaintStyle(bool needPaint)
        {
            if (needPaint != _userPaint)
            {
                _userPaint = needPaint;
                SetStyle(ControlStyles.UserPaint, needPaint);
                Invalidate();
            }
        }

        public bool NeedUserPaint()
        {
            return (Text.IsNullOrEmpty() && Watermark.IsValid()) || (Text.IsValid() && !Enabled);
        }

        private void waterMarkContainer_Click(object sender, EventArgs e)
        {
            this.Focus();
            base.OnClick(EventArgs.Empty);
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Text.IsNullOrEmpty() && Watermark.IsValid())
            {
                e.Graphics.DrawString(Watermark, Font, WaterMarkColor, this.ClientRectangle, ContentAlignment.TopLeft);
            }

            if ((Text.IsValid() && !Enabled))
            {
                e.Graphics.DrawString(Text, Font, ForeDisableColor, this.ClientRectangle, ContentAlignment.TopLeft);
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            SetUserPaintStyle(NeedUserPaint());
        }

        public virtual Color ForeDisableColor { get; set; } = Color.FromArgb(109, 109, 103);

        private float DefaultFontSize = -1;

        public void SetDPIScale()
        {
            if (!UIDPIScale.NeedSetDPIFont()) return;
            if (DefaultFontSize < 0) DefaultFontSize = this.Font.Size;
            Font = UIDPIScale.DPIScaleFont(Font, DefaultFontSize);
        }

        private string _waterMarkText = "";

        [DefaultValue(null)]
        public string Watermark
        {
            get => _waterMarkText;
            set
            {
                _waterMarkText = value;
                //SetUserPaintStyle(NeedUserPaint());
                Invalidate();
            }
        }

        private Color _waterMarkColor = Color.Gray;
        public Color WaterMarkColor
        {
            get => _waterMarkColor;
            set
            {
                _waterMarkColor = value;
                Invalidate();
            }
        }

        private Color _waterMarkActiveColor = Color.Gray;

        public Color WaterMarkActiveForeColor
        {
            get => _waterMarkActiveColor;
            set
            {
                _waterMarkActiveColor = value;
                Invalidate();
            }
        }

        public event OnSelectionChanged SelectionChanged;

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            SelectionChanged?.Invoke(this, new UITextBoxSelectionArgs() { SelectionStart = this.SelectionStart, Text = this.Text });
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            SelectionChanged?.Invoke(this, new UITextBoxSelectionArgs() { SelectionStart = this.SelectionStart, Text = this.Text });
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!Multiline)
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
                                Text = 0.ToString("f" + decLength);
                        break;

                    case UITextBox.UIEditType.Integer:
                        if (!CanEmpty)
                            if (!Text.IsInt32())
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
                        Text = DoubleValue.ToString("f" + decLength);
                        Invalidate();
                    }
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
                if (Type == UITextBox.UIEditType.Double)
                {
                    Text = value.ToString("f" + decLength);
                    CheckMaxMin();
                }
            }
        }

        [DefaultValue(0)]
        public int IntValue
        {
            get
            {
                if (Text == "" && CanEmpty) return 0;
                return Text.ToInt32();
            }
            set
            {
                if (Type == UITextBox.UIEditType.Integer)
                {
                    Text = value.ToString();
                    CheckMaxMin();
                }
            }
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

        public double CheckMaxMin(double value)
        {
            if (_uiEditType == UITextBox.UIEditType.Integer)
            {
                if (value > MaxValue)
                    value = (int)MaxValue;
                if (value < MinValue)
                    value = (int)MinValue;

                Text = value.ToString();
                SelectionStart = Text.Length;
                return value;
            }

            if (_uiEditType == UITextBox.UIEditType.Double)
            {
                if (value > MaxValue)
                    value = MaxValue;

                if (value < MinValue)
                    value = MinValue;

                Text = value.ToString("f" + decLength);
                SelectionStart = Text.Length;
                return value;
            }

            return value;
        }

        public void CheckMaxMin(bool checkLen = false)
        {
            if (_uiEditType == UITextBox.UIEditType.Integer)
            {
                if (Text == "" && CanEmpty) return;
                if (!int.TryParse(Text, out var a)) return;

                int tlen = Text.Replace("+", "").Replace("-", "").Length;
                int maxlen = MaxValue.ToString().Replace("+", "").Replace("-", "").Length;
                int minlen = MinValue.ToString().Replace("+", "").Replace("-", "").Length;
                int mlen = Math.Max(maxlen, minlen);

                if (a > MaxValue)
                {
                    if (!checkLen || (checkLen && tlen >= mlen))
                    {
                        a = (int)MaxValue;
                        Text = a.ToString();
                        SelectionStart = Text.Length;
                    }
                }

                if (a < MinValue)
                {
                    if (!checkLen || (checkLen && tlen >= mlen))
                    {
                        a = (int)MinValue;
                        Text = a.ToString();
                        SelectionStart = Text.Length;
                    }
                }

                if (!checkLen)
                {
                    Text = a.ToString();
                }
            }

            if (_uiEditType == UITextBox.UIEditType.Double)
            {
                if (Text == "" && CanEmpty) return;
                if (!double.TryParse(Text, out var a)) return;

                int tlen = Text.Replace("+", "").Replace("-", "").Length;
                int maxlen = MaxValue.ToString("f" + decLength).Replace("+", "").Replace("-", "").Length;
                int minlen = MinValue.ToString("f" + decLength).Replace("+", "").Replace("-", "").Length;
                int mlen = Math.Max(maxlen, minlen);

                if (a > MaxValue)
                {
                    if (!checkLen || (checkLen && tlen >= mlen))
                    {
                        a = MaxValue;
                        Text = a.ToString("f" + decLength);
                        SelectionStart = Text.Length;
                    }
                }

                mlen = MinValue.ToString("f" + decLength).Replace("+", "").Replace("-", "").Length;
                if (a < MinValue)
                {
                    if (!checkLen || (checkLen && tlen >= mlen))
                    {
                        a = MinValue;
                        Text = a.ToString("f" + decLength);
                        SelectionStart = Text.Length;
                    }
                }

                if (!checkLen)
                {
                    Text = a.ToString("f" + decLength);
                }
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!Multiline)
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

            //如果为整型,为空时自动为0
            if (_uiEditType == UITextBox.UIEditType.Integer)
            {
                if (Text == "" && CanEmpty) return;

                if (Text == "")
                    Text = "0";

                if (!Text.IsInt32())
                    Text = "0";
            }

            //如果为浮点,检查.前后是否为空,为空加0
            if (_uiEditType == UITextBox.UIEditType.Double)
            {
                if (Text == "" && CanEmpty) return;

                //if (StringIndexIsChar(Text, 0, '.'))
                //    Text = @"0" + Text;
                //
                //if (StringIndexIsChar(Text, Text.Length - 1, '.'))
                //    Text = Text + @"0";
                //
                //if (StringIndexIsChar(Text, 0, '+') || StringIndexIsChar(Text, 0, '+'))
                //    if (StringIndexIsChar(Text, 1, '.'))
                //        Text = Text.Insert(1, @"0");

                if (!double.TryParse(Text, out var doubleValue))
                    Text = 0.ToString("f" + decLength);
                else
                    Text = doubleValue.ToString("f" + decLength);
            }

            //CheckMaxMin();
            //Invalidate();
        }

        private double max = int.MaxValue;
        private double min = int.MinValue;

        static internal double EffectiveMax(double _max)
        {
            double maxSupported = double.MaxValue;
            if (_max > maxSupported)
            {
                return maxSupported;
            }

            return _max;
        }

        static internal double EffectiveMin(double _min)
        {
            double minSupported = double.MinValue;
            if (_min < minSupported)
            {
                return minSupported;
            }

            return _min;
        }

        [DefaultValue(int.MaxValue)]
        [Description("最大日期"), Category("SunnyUI")]
        public double MaxValue
        {
            get
            {
                return EffectiveMax(max);
            }
            set
            {
                //if (value != max)
                {
                    if (value < EffectiveMin(min))
                    {
                        value = EffectiveMin(min);
                    }

                    // If trying to set the maximum greater than Max, throw.
                    if (value > double.MaxValue)
                    {
                        value = double.MaxValue;
                    }

                    max = value;
                    if (Type == UITextBox.UIEditType.Integer)
                        max = Math.Min(max, int.MaxValue);

                    //If Value (which was once valid) is suddenly greater than the max (since we just set it)
                    //then adjust this...
                    if (IntValue > max)
                    {
                        IntValue = (int)max;
                    }

                    if (DoubleValue > max)
                    {
                        DoubleValue = max;
                    }
                }
            }
        }

        [DefaultValue(int.MinValue)]
        [Description("最小日期"), Category("SunnyUI")]
        public double MinValue
        {
            get
            {
                return EffectiveMin(min);
            }
            set
            {
                if (value != min)
                {
                    if (value > EffectiveMax(max))
                    {
                        value = EffectiveMax(max);
                    }

                    // If trying to set the minimum less than Min, throw.
                    if (value < double.MinValue)
                    {
                        value = double.MinValue;
                    }

                    min = value;
                    if (Type == UITextBox.UIEditType.Integer)
                        min = Math.Max(min, int.MinValue);

                    //If Value (which was once valid) is suddenly less than the min (since we just set it)
                    //then adjust this...
                    if (IntValue < min)
                    {
                        IntValue = (int)min;
                    }

                    if (DoubleValue < min)
                    {
                        min = value;
                        DoubleValue = min;
                    }
                }
            }
        }
    }
}