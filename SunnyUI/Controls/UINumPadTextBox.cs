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
 * 文件名称: UINumPadTextBox.cs
 * 文件说明: 模拟数字键盘输入框
 * 当前版本: V3.3
 * 创建日期: 2023-03-18
 *
 * 2023-03-18: V3.3.3 增加文件说明
 * 2023-03-26: V3.3.3 增加默认事件ValueChanged，下键盘Enter事件相应此事件
 * 2023-03-26: V3.3.4 增加了最大值、最小值等属性
******************************************************************************/

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    [DefaultEvent("ValueChanged")]
    public class UINumPadTextBox : UIDropControl, IToolTip, IHideDropDown
    {
        public UINumPadTextBox()
        {
            InitializeComponent();
            edit.KeyDown += Edit_KeyDown;
            edit.CanEmpty = true;
            fullControlSelect = true;
        }

        public delegate void OnValueChanged(object sender, string value);
        public event OnValueChanged ValueChanged;
        private NumPadType numPadType = NumPadType.Text;

        [DefaultValue(NumPadType.Text)]
        [Description("小键盘类型"), Category("SunnyUI")]
        public NumPadType NumPadType
        {
            get => numPadType;
            set
            {
                numPadType = value;
                edit.MaxLength = 32767;
                switch (numPadType)
                {
                    case NumPadType.Text:
                        edit.Type = UITextBox.UIEditType.String;
                        break;
                    case NumPadType.Integer:
                        edit.Type = UITextBox.UIEditType.Integer;
                        break;
                    case NumPadType.Double:
                        edit.Type = UITextBox.UIEditType.Double;
                        break;
                    case NumPadType.IDNumber:
                        edit.Type = UITextBox.UIEditType.String;
                        edit.MaxLength = 18;
                        break;
                    default:
                        edit.Type = UITextBox.UIEditType.String;
                        break;
                }

                edit.Text = "";
            }
        }

        private void Edit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                if (!NumPadForm.Visible)
                    ShowDropDown();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                NumPadForm.Close();
            }
            else if (e.KeyCode == Keys.Return)
            {
                if (NumPadForm.Visible)
                {
                    NumPadForm.Close();
                }
                else
                {
                    ShowDropDown();
                }
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        public Control ExToolTipControl()
        {
            return edit;
        }

        private readonly UINumPadItem numPad = new UINumPadItem();

        private UIDropDown numPadForm;

        private UIDropDown NumPadForm
        {
            get
            {
                if (numPadForm == null)
                {
                    numPadForm = new UIDropDown(numPad);

                    if (numPadForm != null)
                    {
                        numPadForm.VisibleChanged += NumBoardForm_VisibleChanged;
                        numPadForm.ValueChanged += NumBoardForm_ValueChanged;
                    }
                }

                return numPadForm;
            }
        }

        [DllImport("user32.dll", EntryPoint = "PostMessageA", SetLastError = true)]
        public static extern int PostMessage(IntPtr hWnd, int Msg, Keys wParam, int lParam);
        public const int WM_CHAR = 256;
        private void NumBoardForm_ValueChanged(object sender, object value)
        {
            int start = edit.SelectionStart;
            switch ((int)value)
            {
                case 88:
                    if (Text.Length == 17)
                    {
                        Win32.User.PostMessage(edit.Handle, WM_CHAR, (int)value, 0);
                        edit.SelectionStart = start;
                        edit.Select(start, 0);
                        //this.Focus();
                    }
                    break;
                case 13:
                    ValueChanged?.Invoke(this, Text);
                    break;
                default:
                    Win32.User.PostMessage(edit.Handle, WM_CHAR, (int)value, 0);
                    edit.SelectionStart = start;
                    edit.Select(start, 0);
                    //this.Focus();
                    break;
            }
        }

        const uint KEYEVENTF_EXTENDEDKEY = 0x1;
        const uint KEYEVENTF_KEYUP = 0x2;

        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtKey);
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        public enum VirtualKeys : byte
        {
            VK_NUMLOCK = 0x90, //数字锁定键
            VK_SCROLL = 0x91,  //滚动锁定
            VK_CAPITAL = 0x14, //大小写锁定
            VK_A = 62
        }

        public bool CapsState;

        public static bool GetState(VirtualKeys Key)
        {
            return (GetKeyState((int)Key) == 1);
        }

        public static void SetState(VirtualKeys Key, bool State)
        {
            if (State != GetState(Key))
            {
                keybd_event((byte)Key, 0x45, KEYEVENTF_EXTENDEDKEY | 0, 0);
                keybd_event((byte)Key, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            }
        }

        private void NumBoardForm_VisibleChanged(object sender, System.EventArgs e)
        {
            dropSymbol = SymbolNormal;
            if (NumPadForm.Visible)
            {
                dropSymbol = SymbolDropDown;
            }

            if (!NumPadForm.Visible)
            {
                SetState(VirtualKeys.VK_CAPITAL, CapsState);
            }

            Invalidate();
        }

        private void UIKeyBoardTextBox_ButtonClick(object sender, System.EventArgs e)
        {
            if (NumPadForm.Visible)
            {
                NumPadForm.Close();
            }
            else
            {
                ShowDropDown();
            }
        }

        private void ShowDropDown()
        {
            NumPadForm.AutoClose = false;
            numPad.NumPadType = NumPadType;
            numPad.SetDPIScale();
            numPad.SetStyleColor(UIStyles.ActiveStyleColor);

            if (numPadType == NumPadType.IDNumber)
            {
                CapsState = GetState(VirtualKeys.VK_CAPITAL);
                SetState(VirtualKeys.VK_CAPITAL, true);
            }

            if (!NumPadForm.Visible)
            {
                NumPadForm.Show(this, NumPadForm.Size);
            }

            edit.Focus();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // edit
            // 
            edit.Leave += edit_Leave;
            // 
            // UIKeyBoardTextBox
            // 
            Name = "UIKeyBoardTextBox";
            ButtonClick += UIKeyBoardTextBox_ButtonClick;
            ResumeLayout(false);
            PerformLayout();
        }

        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            numPad?.Dispose();
            numPadForm?.Dispose();
            base.Dispose(disposing);
        }

        private void edit_Leave(object sender, EventArgs e)
        {
            HideDropDown();
        }

        public void HideDropDown()
        {
            try
            {
                if (NumPadForm != null && NumPadForm.Visible)
                    NumPadForm.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 当InputType为数字类型时，能输入的最大值
        /// </summary>
        [Description("当InputType为数字类型时，能输入的最大值。"), Category("SunnyUI")]
        [DefaultValue(2147483647D)]
        public double Maximum
        {
            get => edit.MaxValue;
            set => edit.MaxValue = value;
        }

        /// <summary>
        /// 当InputType为数字类型时，能输入的最小值
        /// </summary>
        [Description("当InputType为数字类型时，能输入的最小值。"), Category("SunnyUI")]
        [DefaultValue(-2147483648D)]
        public double Minimum
        {
            get => edit.MinValue;
            set => edit.MinValue = value;
        }

        [Description("浮点数，显示文字小数位数"), Category("SunnyUI")]
        [DefaultValue(2)]
        public int DecimalPlaces
        {
            get => edit.DecLength;
            set => edit.DecLength = Math.Max(value, 0);
        }
    }
}
