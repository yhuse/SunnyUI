/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2022 ShenYongHua(沈永华).
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
 * 文件名称: UIFormHelper.cs
 * 文件说明: 窗体帮助类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-05-05: V2.2.5 增加下拉选择框，进度提升窗体
 * 2021-05-09: V3.0.3 增加RemovePage接口，更改GetTopMost为原生接口TopMost
 * 2021-06-27: V3.0.4 增加一个反馈的接口，Feedback，Page可将对象反馈给Frame
 * 2021-12-13: V3.0.9 增加全屏遮罩，Form的ShowDialogWithMask()扩展方法
******************************************************************************/

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public delegate void OnZoomScaleChanged(object sender, float scale);

    public delegate void OnZoomScaleRectChanged(object sender, Rectangle info);

    public static class UIMessageDialog
    {
        /// <summary>
        /// 正确信息提示框
        /// </summary>
        /// <param name="form">窗体</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        public static void ShowSuccessDialog(this Form form, string msg, UIStyle style = UIStyle.Green)
        {
            form.ShowMessageDialog(msg, UILocalize.SuccessTitle, false, style);
        }

        /// <summary>
        /// 信息提示框
        /// </summary>
        /// <param name="form">窗体</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        public static void ShowInfoDialog(this Form form, string msg, UIStyle style = UIStyle.Gray)
        {
            form.ShowMessageDialog(msg, UILocalize.InfoTitle, false, style);
        }

        /// <summary>
        /// 警告信息提示框
        /// </summary>
        /// <param name="form">窗体</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        public static void ShowWarningDialog(this Form form, string msg, UIStyle style = UIStyle.Orange)
        {
            form.ShowMessageDialog(msg, UILocalize.WarningTitle, false, style);
        }

        /// <summary>
        /// 错误信息提示框
        /// </summary>
        /// <param name="form">窗体</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        public static void ShowErrorDialog(this Form form, string msg, UIStyle style = UIStyle.Red)
        {
            form.ShowMessageDialog(msg, UILocalize.ErrorTitle, false, style);
        }

        /// <summary>
        /// 确认信息提示框
        /// </summary>
        /// <param name="form">窗体</param>
        /// <param name="msg">信息</param>
        /// <param name="style"></param>
        /// <returns>结果</returns>
        public static bool ShowAskDialog(this Form form, string msg, UIStyle style = UIStyle.Blue)
        {
            return form.ShowMessageDialog(msg, UILocalize.AskTitle, true, style);
        }

        /// <summary>
        /// 正确信息提示框
        /// </summary>
        /// <param name="form">窗体</param>
        /// <param name="title">标题</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        public static void ShowSuccessDialog(this Form form, string title, string msg, UIStyle style = UIStyle.Green)
        {
            form.ShowMessageDialog(msg, title, false, style);
        }

        /// <summary>
        /// 信息提示框
        /// </summary>
        /// <param name="form">窗体</param>
        /// <param name="title">标题</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        public static void ShowInfoDialog(this Form form, string title, string msg, UIStyle style = UIStyle.Gray)
        {
            form.ShowMessageDialog(msg, title, false, style);
        }

        /// <summary>
        /// 警告信息提示框
        /// </summary>
        /// <param name="form">窗体</param>
        /// <param name="title">标题</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        public static void ShowWarningDialog(this Form form, string title, string msg, UIStyle style = UIStyle.Orange)
        {
            form.ShowMessageDialog(msg, title, false, style);
        }

        /// <summary>
        /// 错误信息提示框
        /// </summary>
        /// <param name="form">窗体</param>
        /// <param name="title">标题</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        public static void ShowErrorDialog(this Form form, string title, string msg, UIStyle style = UIStyle.Red)
        {
            form.ShowMessageDialog(msg, title, false, style);
        }

        /// <summary>
        /// 确认信息提示框
        /// </summary>
        /// <param name="form">窗体</param>
        /// <param name="title">标题</param>
        /// <param name="msg">信息</param>
        /// <param name="style"></param>
        /// <returns>结果</returns>
        public static bool ShowAskDialog(this Form form, string title, string msg, UIStyle style = UIStyle.Blue)
        {
            return form.ShowMessageDialog(msg, title, true, style);
        }

        public static bool ShowMessageDialog(this Form form, string message, string title, bool isShowCancel, UIStyle style)
        {
            UIMessageForm frm = new UIMessageForm();
            frm.TopMost = form != null && form.TopMost;
            frm.ShowMessage(message, title, isShowCancel, style);
            frm.ShowDialog();
            bool isOk = frm.IsOK;
            frm.Dispose();
            return isOk;
        }

        /// <summary>
        /// 确认信息提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">信息</param>
        /// <param name="showCancelButton">显示取消按钮</param>
        /// <param name="style">主题</param>
        /// <param name="showMask">显示遮罩层</param>
        /// <param name="topMost">置顶</param>
        /// <returns>结果</returns>
        public static bool ShowMessageDialog(string message, string title, bool showCancelButton, UIStyle style, bool showMask = true, bool topMost = false)
        {
            Point pt = SystemEx.GetCursorPos();
            Rectangle screen = Screen.GetBounds(pt);
            UIMessageForm frm = new UIMessageForm();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowMessage(message, title, showCancelButton, style);
            frm.ShowInTaskbar = false;
            frm.TopMost = topMost;

            if (showMask)
                frm.ShowDialogWithMask();
            else
                frm.ShowDialog();

            bool isOk = frm.IsOK;
            frm.Dispose();
            return isOk;
        }
    }

    public static class UIMessageBox
    {
        public static void Show(string text, bool showMask = true, bool topMost = false)
        {
            Show(text, UILocalize.InfoTitle, UIStyle.Blue, UIMessageBoxButtons.OK, showMask, topMost);
        }

        public static void ShowInfo(string text, bool showMask = true, bool topMost = false)
        {
            Show(text, UILocalize.InfoTitle, UIStyle.Gray, UIMessageBoxButtons.OK, showMask, topMost);
        }

        public static void ShowSuccess(string text, bool showMask = true, bool topMost = false)
        {
            Show(text, UILocalize.SuccessTitle, UIStyle.Green, UIMessageBoxButtons.OK, showMask, topMost);
        }

        public static void ShowWarning(string text, bool showMask = true, bool topMost = false)
        {
            Show(text, UILocalize.WarningTitle, UIStyle.Orange, UIMessageBoxButtons.OK, showMask, topMost);
        }

        public static void ShowError(string text, bool showMask = true, bool topMost = false)
        {
            Show(text, UILocalize.ErrorTitle, UIStyle.Red, UIMessageBoxButtons.OK, showMask, topMost);
        }

        public static bool ShowAsk(string text, bool showMask = true, bool topMost = false)
        {
            return Show(text, UILocalize.AskTitle, UIStyle.Blue, UIMessageBoxButtons.OKCancel, showMask, topMost);
        }

        public static bool Show(string text, string caption, UIStyle style = UIStyle.Blue, UIMessageBoxButtons buttons = UIMessageBoxButtons.OK, bool showMask = true, bool topMost = false)
        {
            return UIMessageDialog.ShowMessageDialog(text, caption, buttons == UIMessageBoxButtons.OKCancel, style, showMask, topMost);
        }
    }

    public enum UIMessageBoxButtons
    {
        /// <summary>
        /// 确定
        /// </summary>
        OK = 0,

        /// <summary>
        /// 确定、取消
        /// </summary>
        OKCancel = 1
    }

    public static class UIInputDialog
    {
        public static bool InputStringDialog(ref string value, bool checkEmpty = true,
            string desc = "请输入字符串：", UIStyle style = UIStyle.Blue, bool topMost = false, bool showMask = false)
        {
            UIInputForm frm = new UIInputForm();
            frm.TopMost = topMost;
            frm.Style = style;
            frm.Editor.Text = value;
            frm.Text = UILocalize.InputTitle;
            frm.Label.Text = desc;
            frm.CheckInputEmpty = checkEmpty;
            if (showMask)
                frm.ShowDialogWithMask();
            else
                frm.ShowDialog();
            if (frm.IsOK)
            {
                value = frm.Editor.Text;
                return true;
            }

            return false;
        }

        public static bool InputStringDialog(this UIForm form, ref string value, bool checkEmpty = true,
            string desc = "请输入字符串：", bool showMask = false)
        {
            return InputStringDialog(ref value, checkEmpty, desc, form != null ? form.Style : UIStyle.Blue, form != null && form.TopMost, showMask);
        }

        public static bool InputStringDialog(this UIPage form, ref string value, bool checkEmpty = true,
            string desc = "请输入字符串：", bool showMask = false)
        {
            return InputStringDialog(ref value, checkEmpty, desc, form != null ? form.Style : UIStyle.Blue, form != null && form.TopMost, showMask);
        }

        public static bool InputStringDialog(this Form form, ref string value, bool checkEmpty = true,
            string desc = "请输入字符串：", UIStyle style = UIStyle.Blue, bool showMask = false)
        {
            return InputStringDialog(ref value, checkEmpty, desc, style, form != null && form.TopMost, showMask);
        }

        public static bool InputPasswordDialog(ref string value, bool checkEmpty = true,
            string desc = "请输入密码：", UIStyle style = UIStyle.Blue, bool topMost = false, bool showMask = false)
        {
            UIInputForm frm = new UIInputForm();
            frm.TopMost = topMost;
            frm.Style = style;
            frm.Text = UILocalize.InputTitle;
            frm.Label.Text = desc;
            frm.Editor.PasswordChar = '*';
            frm.CheckInputEmpty = checkEmpty;
            if (showMask)
                frm.ShowDialogWithMask();
            else
                frm.ShowDialog();
            if (frm.IsOK)
            {
                value = frm.Editor.Text;
                return true;
            }

            return false;
        }

        public static bool InputPasswordDialog(this UIForm form, ref string value, bool checkEmpty = true,
            string desc = "请输入密码：", bool showMask = false)
        {
            return InputPasswordDialog(ref value, checkEmpty, desc, form != null ? form.Style : UIStyle.Blue, form != null && form.TopMost, showMask);
        }

        public static bool InputPasswordDialog(this UIPage form, ref string value, bool checkEmpty = true,
            string desc = "请输入密码：", bool showMask = false)
        {
            return InputPasswordDialog(ref value, checkEmpty, desc, form != null ? form.Style : UIStyle.Blue, form != null && form.TopMost, showMask);
        }

        public static bool InputPasswordDialog(this Form form, ref string value, bool checkEmpty = true,
            string desc = "请输入密码：", UIStyle style = UIStyle.Blue, bool showMask = false)
        {
            return InputPasswordDialog(ref value, checkEmpty, desc, style, form != null && form.TopMost, showMask);
        }

        public static bool InputIntegerDialog(ref int value, bool checkEmpty = true,
            string desc = "请输入数字：", UIStyle style = UIStyle.Blue, bool topMost = false, bool showMask = false)
        {
            UIInputForm frm = new UIInputForm();
            frm.TopMost = topMost;
            frm.Style = style;
            frm.Editor.Type = UITextBox.UIEditType.Integer;
            frm.Editor.IntValue = value;
            frm.Text = UILocalize.InputTitle;
            frm.Label.Text = desc;
            frm.CheckInputEmpty = checkEmpty;
            if (showMask)
                frm.ShowDialogWithMask();
            else
                frm.ShowDialog();
            if (frm.IsOK)
            {
                value = frm.Editor.IntValue;
                return true;
            }

            return false;
        }

        public static bool InputIntegerDialog(ref int value, int minimum, int maximum, bool checkEmpty = true,
            string desc = "请输入数字：", UIStyle style = UIStyle.Blue, bool topMost = false, bool showMask = false)
        {
            UIInputForm frm = new UIInputForm();
            frm.TopMost = topMost;
            frm.Style = style;
            frm.Editor.Type = UITextBox.UIEditType.Integer;
            frm.Editor.IntValue = value;
            frm.Text = UILocalize.InputTitle;
            frm.Label.Text = desc;
            frm.CheckInputEmpty = checkEmpty;
            frm.Editor.MaxLength = 11;
            frm.Editor.Minimum = minimum;
            frm.Editor.Maximum = maximum;
            frm.Editor.HasMaximum = true;
            frm.Editor.HasMinimum = true;
            if (showMask)
                frm.ShowDialogWithMask();
            else
                frm.ShowDialog();
            if (frm.IsOK)
            {
                value = frm.Editor.IntValue;
                return true;
            }

            return false;
        }

        public static bool InputIntegerDialog(this UIForm form, ref int value, bool checkEmpty = true,
            string desc = "请输入数字：", bool showMask = false)
        {
            return InputIntegerDialog(ref value, checkEmpty, desc, form != null ? form.Style : UIStyle.Blue, form != null && form.TopMost, showMask);
        }

        public static bool InputIntegerDialog(this UIPage form, ref int value, bool checkEmpty = true,
            string desc = "请输入数字：", bool showMask = false)
        {
            return InputIntegerDialog(ref value, checkEmpty, desc, form != null ? form.Style : UIStyle.Blue, form != null && form.TopMost, showMask);
        }

        public static bool InputIntegerDialog(this Form form, ref int value, bool checkEmpty = true,
            string desc = "请输入数字：", UIStyle style = UIStyle.Blue, bool showMask = false)
        {
            return InputIntegerDialog(ref value, checkEmpty, desc, style, form != null && form.TopMost, showMask);
        }

        public static bool InputIntegerDialog(this UIForm form, ref int value, int minimum, int maximum, bool checkEmpty = true,
            string desc = "请输入数字：", bool showMask = false)
        {
            return InputIntegerDialog(ref value, minimum, maximum, checkEmpty, desc, form != null ? form.Style : UIStyle.Blue, form != null && form.TopMost, showMask);
        }

        public static bool InputIntegerDialog(this UIPage form, ref int value, int minimum, int maximum, bool checkEmpty = true,
            string desc = "请输入数字：", bool showMask = false)
        {
            return InputIntegerDialog(ref value, minimum, maximum, checkEmpty, desc, form != null ? form.Style : UIStyle.Blue, form != null && form.TopMost, showMask);
        }

        public static bool InputIntegerDialog(this Form form, ref int value, int minimum, int maximum, bool checkEmpty = true,
            string desc = "请输入数字：", UIStyle style = UIStyle.Blue, bool showMask = false)
        {
            return InputIntegerDialog(ref value, minimum, maximum, checkEmpty, desc, style, form != null && form.TopMost, showMask);
        }

        public static bool InputDoubleDialog(ref double value, int decimals = 2, bool checkEmpty = true,
            string desc = "请输入数字：", UIStyle style = UIStyle.Blue, bool topMost = false, bool showMask = false)
        {
            UIInputForm frm = new UIInputForm();
            frm.TopMost = topMost;
            frm.Style = style;
            frm.Editor.Type = UITextBox.UIEditType.Double;
            frm.Editor.DecLength = decimals;
            frm.Editor.DoubleValue = value;
            frm.Text = UILocalize.InputTitle;
            frm.Label.Text = desc;
            frm.CheckInputEmpty = checkEmpty;
            if (showMask)
                frm.ShowDialogWithMask();
            else
                frm.ShowDialog();
            if (frm.IsOK)
            {
                value = frm.Editor.DoubleValue;
                return true;
            }

            return false;
        }

        public static bool InputDoubleDialog(ref double value, double minimum, double maximum, int decimals = 2, bool checkEmpty = true,
            string desc = "请输入数字：", UIStyle style = UIStyle.Blue, bool topMost = false, bool showMask = false)
        {
            UIInputForm frm = new UIInputForm();
            frm.TopMost = topMost;
            frm.Style = style;
            frm.Editor.Type = UITextBox.UIEditType.Double;
            frm.Editor.DecLength = decimals;
            frm.Editor.DoubleValue = value;
            frm.Text = UILocalize.InputTitle;
            frm.Label.Text = desc;
            frm.CheckInputEmpty = checkEmpty;
            frm.Editor.Minimum = minimum;
            frm.Editor.Maximum = maximum;
            frm.Editor.HasMaximum = true;
            frm.Editor.HasMinimum = true;
            if (showMask)
                frm.ShowDialogWithMask();
            else
                frm.ShowDialog();
            if (frm.IsOK)
            {
                value = frm.Editor.DoubleValue;
                return true;
            }

            return false;
        }

        public static bool InputDoubleDialog(this UIForm form, ref double value, int decimals = 2, bool checkEmpty = true,
            string desc = "请输入数字：", bool showMask = false)
        {
            return InputDoubleDialog(ref value, decimals, checkEmpty, desc, form != null ? form.Style : UIStyle.Blue, form != null && form.TopMost, showMask);
        }

        public static bool InputDoubleDialog(this UIPage form, ref double value, int decimals = 2, bool checkEmpty = true,
            string desc = "请输入数字：", bool showMask = false)
        {
            return InputDoubleDialog(ref value, decimals, checkEmpty, desc, form != null ? form.Style : UIStyle.Blue, form != null && form.TopMost, showMask);
        }

        public static bool InputDoubleDialog(this Form form, ref double value, int decimals = 2, bool checkEmpty = true,
            string desc = "请输入数字：", UIStyle style = UIStyle.Blue, bool showMask = false)
        {
            return InputDoubleDialog(ref value, decimals, checkEmpty, desc, style, form != null && form.TopMost, showMask);
        }

        public static bool InputDoubleDialog(this UIForm form, ref double value, double minimum, double maximum, int decimals = 2, bool checkEmpty = true,
            string desc = "请输入数字：", bool showMask = false)
        {
            return InputDoubleDialog(ref value, minimum, maximum, decimals, checkEmpty, desc, form != null ? form.Style : UIStyle.Blue, form != null && form.TopMost, showMask);
        }

        public static bool InputDoubleDialog(this UIPage form, ref double value, double minimum, double maximum, int decimals = 2, bool checkEmpty = true,
            string desc = "请输入数字：", bool showMask = false)
        {
            return InputDoubleDialog(ref value, minimum, maximum, decimals, checkEmpty, desc, form != null ? form.Style : UIStyle.Blue, form != null && form.TopMost, showMask);
        }

        public static bool InputDoubleDialog(this Form form, ref double value, double minimum, double maximum, int decimals = 2, bool checkEmpty = true,
            string desc = "请输入数字：", UIStyle style = UIStyle.Blue, bool showMask = false)
        {
            return InputDoubleDialog(ref value, minimum, maximum, decimals, checkEmpty, desc, style, form != null && form.TopMost, showMask);
        }
    }

    public static class UISelectDialog
    {
        public static bool ShowSelectDialog(this Form form, ref int selectIndex, IList items, string title, string description, UIStyle style = UIStyle.Blue, bool topMost = false)
        {
            UISelectForm frm = new UISelectForm();
            frm.TopMost = topMost;
            frm.Style = style;
            frm.SetItems(items);
            frm.SelectedIndex = selectIndex;
            if (title.IsValid()) frm.Title = title;
            if (description.IsValid()) frm.Description = description;
            frm.ShowDialog();

            bool result = frm.IsOK;
            if (frm.IsOK)
            {
                selectIndex = frm.SelectedIndex;
            }

            frm.Dispose();
            return result;
        }

        public static bool ShowSelectDialog(this Form form, ref int selectIndex, IList items, UIStyle style = UIStyle.Blue)
        {
            return form.ShowSelectDialog(ref selectIndex, items, UILocalize.SelectTitle, "", style, form != null && form.TopMost);
        }

        public static bool ShowSelectDialog(this UIForm form, ref int selectIndex, IList items)
        {
            return form.ShowSelectDialog(ref selectIndex, items, form.Style);
        }

        public static bool ShowSelectDialog(this UIPage form, ref int selectIndex, IList items)
        {
            return form.ShowSelectDialog(ref selectIndex, items, form.Style);
        }

        public static bool ShowSelectDialog(this UIForm form, ref int selectIndex, IList items, string title, string description)
        {
            return form.ShowSelectDialog(ref selectIndex, items, title, description, form != null ? form.Style : UIStyle.Blue, form != null && form.TopMost);
        }

        public static bool ShowSelectDialog(this UIPage form, ref int selectIndex, IList items, string title, string description)
        {
            return form.ShowSelectDialog(ref selectIndex, items, title, description, form != null ? form.Style : UIStyle.Blue, form != null && form.TopMost);
        }
    }

    public static class UINotifierHelper
    {
        public static void ShowNotifier(string desc, UINotifierType type = UINotifierType.INFO, string title = "Notifier", bool isDialog = false, int timeout = 0, Form inApp = null)
        {
            UINotifier.Show(desc, type, title, isDialog, timeout, inApp);
        }

        public static void ShowNotifier(string desc, EventHandler clickEvent, UINotifierType type = UINotifierType.INFO, string title = "Notifier", int timeout = 0)
        {
            UINotifier.Show(desc, type, title, false, timeout, null, clickEvent);
        }
    }

    public static class FormEx
    {
        internal class FMask : Form
        {
            public FMask()
            {
                this.SuspendLayout();
                this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
                this.BackColor = Color.Black;
                this.ClientSize = new System.Drawing.Size(800, 450);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Name = "FMask";
                this.Opacity = 0.5D;
                this.ShowInTaskbar = false;
                this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                this.Text = "FMask";
                this.TopMost = true;
                this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FMask_MouseClick);
                this.ResumeLayout(false);
            }

            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                int num = 256;
                int num2 = 260;
                if (msg.Msg == num | msg.Msg == num2)
                {
                    if (keyData == Keys.Escape)
                    {
                        Close();
                    }
                }

                return base.ProcessCmdKey(ref msg, keyData);
            }

            private void FMask_MouseClick(object sender, MouseEventArgs e)
            {
                Close();
            }

            private System.ComponentModel.IContainer components = null;

            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }

                base.Dispose(disposing);
            }
        }

        public static DialogResult ShowDialogWithMask(this Form form)
        {
            Point pt = SystemEx.GetCursorPos();
            Rectangle screen = Screen.GetBounds(pt);
            FMask mask = new FMask();
            mask.Bounds = screen;
            mask.Show();

            form.ShowInTaskbar = false;
            form.TopMost = true;
            DialogResult dr = form.ShowDialog();
            mask.Dispose();

            return dr;
        }

        [Obsolete("已弃用，直接调用Form的ShowDialogWithMask扩展方法")]
        public static Form ShowFullMask(this Form form)
        {
            Point pt = SystemEx.GetCursorPos();
            Rectangle screen = Screen.GetBounds(pt);

            Form mask = new Form();
            mask.FormBorderStyle = FormBorderStyle.None;
            mask.BackColor = Color.FromArgb(0, 0, 0);
            mask.Opacity = 0.5;
            mask.ShowInTaskbar = false;
            mask.StartPosition = FormStartPosition.Manual;
            mask.Bounds = screen;
            mask.TopMost = true;
            mask.Show();
            return mask;
        }

        [Obsolete("已弃用，直接调用Form的ShowDialogWithMask扩展方法")]
        public static Form ShowControlMask(this Control control)
        {
            bool topmost = false;
            Form baseForm = control.RootForm();
            if (baseForm != null)
            {
                topmost = baseForm.TopMost;
                baseForm.TopMost = true;
            }

            Form mask = new Form();
            mask.FormBorderStyle = FormBorderStyle.None;
            mask.BackColor = Color.FromArgb(0, 0, 0);
            mask.Opacity = 0.5;
            mask.ShowInTaskbar = false;
            mask.StartPosition = FormStartPosition.Manual;
            mask.Bounds = control.Bounds;
            mask.Tag = baseForm;
            mask.Text = topmost.ToString();

            var pt = control.ScreenLocation();
            mask.Left = pt.X;
            mask.Top = pt.Y;
            mask.Show();
            mask.TopMost = true;
            return mask;
        }

        [Obsolete("已弃用，直接调用Form的ShowDialogWithMask扩展方法")]
        public static void EndShow(this Form maskForm)
        {
            if (maskForm.Tag is Form form)
            {
                form.TopMost = maskForm.Text.ToBoolean();
            }
        }

        [Obsolete("已弃用，直接调用Form的ShowDialogWithMask扩展方法")]
        public static void ShowInMask(this Form frm, Form maskForm)
        {
            frm.StartPosition = FormStartPosition.Manual;
            frm.Left = maskForm.Left + (maskForm.Width - frm.Width) / 2;
            frm.Top = maskForm.Top + (maskForm.Height - frm.Height) / 2;
            frm.ShowInTaskbar = false;
            frm.TopMost = true;
        }

        public static UIEditForm CreateForm(this UIEditOption option)
        {
            return new UIEditForm(option);
        }

        /// <summary>
        /// 设置窗体的圆角矩形
        /// </summary>
        /// <param name="form">需要设置的窗体</param>
        /// <param name="rgnRadius">圆角矩形的半径</param>
        public static void SetFormRoundRectRegion(Form form, int rgnRadius)
        {
            if (form != null && form.FormBorderStyle == FormBorderStyle.None)
            {
                int region = Win32.GDI.CreateRoundRectRgn(0, 0, form.Width + 1, form.Height + 1, rgnRadius, rgnRadius);
                Win32.User.SetWindowRgn(form.Handle, region, true);
                Win32.GDI.DeleteObject(region);
            }
        }
    }
}