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
 * 文件名称: UIFormHelper.cs
 * 文件说明: 窗体帮助类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-05-05: V2.2.5 增加下拉选择框，进度提升窗体
******************************************************************************/

using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
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
            frm.TopMost = form.TopMost;
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
        /// <returns>结果</returns>
        public static bool ShowMessageDialog(string message, string title, bool showCancelButton, UIStyle style, bool showMask = true)
        {
            Point pt = SystemEx.GetCursorPos();
            Rectangle screen = Screen.GetBounds(pt);

            Form mask = null;
            if (showMask)
            {
                mask = new Form();
                mask.FormBorderStyle = FormBorderStyle.None;
                mask.BackColor = Color.FromArgb(0, 0, 0);
                mask.Opacity = 0.5;
                mask.ShowInTaskbar = false;
                mask.StartPosition = FormStartPosition.Manual;
                mask.Bounds = screen;
                mask.Show();
                mask.TopMost = true;
                Application.DoEvents();
            }

            UIMessageForm frm = new UIMessageForm();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowMessage(message, title, showCancelButton, style);
            frm.ShowInTaskbar = false;
            frm.TopMost = showMask;
            frm.ShowDialog();
            bool isOk = frm.IsOK;
            frm.Dispose();

            mask?.Close();
            return isOk;
        }
    }

    public static class UIMessageBox
    {
        public static void Show(string text, bool showMask = true)
        {
            Show(text, UILocalize.InfoTitle, UIStyle.Blue, UIMessageBoxButtons.OK, showMask);
        }

        public static void ShowInfo(string text, bool showMask = true)
        {
            Show(text, UILocalize.InfoTitle, UIStyle.Gray, UIMessageBoxButtons.OK, showMask);
        }

        public static void ShowSuccess(string text, bool showMask = true)
        {
            Show(text, UILocalize.SuccessTitle, UIStyle.Green, UIMessageBoxButtons.OK, showMask);
        }

        public static void ShowWarning(string text, bool showMask = true)
        {
            Show(text, UILocalize.WarningTitle, UIStyle.Orange, UIMessageBoxButtons.OK, showMask);
        }

        public static void ShowError(string text, bool showMask = true)
        {
            Show(text, UILocalize.ErrorTitle, UIStyle.Red, UIMessageBoxButtons.OK, showMask);
        }

        public static bool ShowAsk(string text, bool showMask = true)
        {
            return Show(text, UILocalize.AskTitle, UIStyle.Blue, UIMessageBoxButtons.OKCancel, showMask);
        }

        public static bool Show(string text, string caption, UIStyle style = UIStyle.Blue, UIMessageBoxButtons buttons = UIMessageBoxButtons.OK, bool showMask = true)
        {
            return UIMessageDialog.ShowMessageDialog(text, caption, buttons == UIMessageBoxButtons.OKCancel, style, showMask);
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
        private static bool InputStringDialog(ref string value, bool checkEmpty = true, string desc = "请输入字符串：", UIStyle style = UIStyle.Blue, bool topMost = false)
        {
            UIInputForm frm = new UIInputForm();
            frm.TopMost = topMost;
            frm.Style = style;
            frm.Editor.Text = value;
            frm.Text = UILocalize.InputTitle;
            frm.Label.Text = desc;
            frm.CheckInputEmpty = checkEmpty;
            frm.ShowDialog();
            if (frm.IsOK)
            {
                value = frm.Editor.Text;
                return true;
            }

            return false;
        }

        public static bool InputStringDialog(this UIForm form, ref string value, bool checkEmpty = true, string desc = "请输入字符串：")
        {
            return InputStringDialog(ref value, checkEmpty, desc, form.Style, form.TopMost);
        }

        public static bool InputStringDialog(this UIPage form, ref string value, bool checkEmpty = true, string desc = "请输入字符串：")
        {
            return InputStringDialog(ref value, checkEmpty, desc, form.Style, form.TopMost);
        }

        public static bool InputStringDialog(this Form form, ref string value, bool checkEmpty = true, string desc = "请输入字符串：", UIStyle style = UIStyle.Blue)
        {
            return InputStringDialog(ref value, checkEmpty, desc, style, form.TopMost);
        }

        private static bool InputPasswordDialog(ref string value, bool checkEmpty = true, string desc = "请输入密码：", UIStyle style = UIStyle.Blue, bool topMost = false)
        {
            UIInputForm frm = new UIInputForm();
            frm.TopMost = topMost;
            frm.Style = style;
            frm.Text = UILocalize.InputTitle;
            frm.Label.Text = desc;
            frm.Editor.PasswordChar = '*';
            frm.CheckInputEmpty = checkEmpty;
            frm.ShowDialog();
            if (frm.IsOK)
            {
                value = frm.Editor.Text;
                return true;
            }

            return false;
        }

        public static bool InputPasswordDialog(this UIForm form, ref string value, bool checkEmpty = true, string desc = "请输入密码：")
        {
            return InputPasswordDialog(ref value, checkEmpty, desc, form.Style, form.TopMost);
        }

        public static bool InputPasswordDialog(this UIPage form, ref string value, bool checkEmpty = true, string desc = "请输入密码：")
        {
            return InputPasswordDialog(ref value, checkEmpty, desc, form.Style, form.TopMost);
        }

        public static bool InputPasswordDialog(this Form form, ref string value, bool checkEmpty = true, string desc = "请输入密码：", UIStyle style = UIStyle.Blue)
        {
            return InputPasswordDialog(ref value, checkEmpty, desc, style, form.TopMost);
        }

        private static bool InputIntegerDialog(ref int value, bool checkEmpty = true, string desc = "请输入数字：", UIStyle style = UIStyle.Blue, bool topMost = false)
        {
            UIInputForm frm = new UIInputForm();
            frm.TopMost = topMost;
            frm.Style = style;
            frm.Editor.Type = UITextBox.UIEditType.Integer;
            frm.Editor.IntValue = value;
            frm.Text = UILocalize.InputTitle;
            frm.Label.Text = desc;
            frm.CheckInputEmpty = checkEmpty;
            frm.ShowDialog();
            if (frm.IsOK)
            {
                value = frm.Editor.IntValue;
                return true;
            }

            return false;
        }

        private static bool InputIntegerDialog(ref int value, int minimum, int maximum, bool checkEmpty = true, string desc = "请输入数字：", UIStyle style = UIStyle.Blue, bool topMost = false)
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
            frm.ShowDialog();
            if (frm.IsOK)
            {
                value = frm.Editor.IntValue;
                return true;
            }

            return false;
        }

        public static bool InputIntegerDialog(this UIForm form, ref int value, bool checkEmpty = true, string desc = "请输入数字：")
        {
            return InputIntegerDialog(ref value, checkEmpty, desc, form.Style, form.TopMost);
        }

        public static bool InputIntegerDialog(this UIPage form, ref int value, bool checkEmpty = true, string desc = "请输入数字：")
        {
            return InputIntegerDialog(ref value, checkEmpty, desc, form.Style, form.TopMost);
        }

        public static bool InputIntegerDialog(this Form form, ref int value, bool checkEmpty = true, string desc = "请输入数字：", UIStyle style = UIStyle.Blue)
        {
            return InputIntegerDialog(ref value, checkEmpty, desc, style, form.TopMost);
        }

        public static bool InputIntegerDialog(this UIForm form, ref int value, int minimum, int maximum, bool checkEmpty = true, string desc = "请输入数字：")
        {
            return InputIntegerDialog(ref value, minimum, maximum, checkEmpty, desc, form.Style, form.TopMost);
        }

        public static bool InputIntegerDialog(this UIPage form, ref int value, int minimum, int maximum, bool checkEmpty = true, string desc = "请输入数字：")
        {
            return InputIntegerDialog(ref value, minimum, maximum, checkEmpty, desc, form.Style, form.TopMost);
        }

        public static bool InputIntegerDialog(this Form form, ref int value, int minimum, int maximum, bool checkEmpty = true, string desc = "请输入数字：", UIStyle style = UIStyle.Blue)
        {
            return InputIntegerDialog(ref value, minimum, maximum, checkEmpty, desc, style, form.TopMost);
        }

        private static bool InputDoubleDialog(ref double value, int decimals = 2, bool checkEmpty = true, string desc = "请输入数字：", UIStyle style = UIStyle.Blue, bool topMost = false)
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
            frm.ShowDialog();
            if (frm.IsOK)
            {
                value = frm.Editor.DoubleValue;
                return true;
            }

            return false;
        }

        private static bool InputDoubleDialog(ref double value, double minimum, double maximum, int decimals = 2, bool checkEmpty = true, string desc = "请输入数字：", UIStyle style = UIStyle.Blue, bool topMost = false)
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
            frm.ShowDialog();
            if (frm.IsOK)
            {
                value = frm.Editor.DoubleValue;
                return true;
            }

            return false;
        }

        public static bool InputDoubleDialog(this UIForm form, ref double value, int decimals = 2, bool checkEmpty = true, string desc = "请输入数字：")
        {
            return InputDoubleDialog(ref value, decimals, checkEmpty, desc, form.Style, form.TopMost);
        }

        public static bool InputDoubleDialog(this UIPage form, ref double value, int decimals = 2, bool checkEmpty = true, string desc = "请输入数字：")
        {
            return InputDoubleDialog(ref value, decimals, checkEmpty, desc, form.Style, form.TopMost);
        }

        public static bool InputDoubleDialog(this Form form, ref double value, int decimals = 2, bool checkEmpty = true, string desc = "请输入数字：", UIStyle style = UIStyle.Blue)
        {
            return InputDoubleDialog(ref value, decimals, checkEmpty, desc, style, form.TopMost);
        }

        public static bool InputDoubleDialog(this UIForm form, ref double value, double minimum, double maximum, int decimals = 2, bool checkEmpty = true, string desc = "请输入数字：")
        {
            return InputDoubleDialog(ref value, minimum, maximum, decimals, checkEmpty, desc, form.Style, form.TopMost);
        }

        public static bool InputDoubleDialog(this UIPage form, ref double value, double minimum, double maximum, int decimals = 2, bool checkEmpty = true, string desc = "请输入数字：")
        {
            return InputDoubleDialog(ref value, minimum, maximum, decimals, checkEmpty, desc, form.Style, form.TopMost);
        }

        public static bool InputDoubleDialog(this Form form, ref double value, double minimum, double maximum, int decimals = 2, bool checkEmpty = true, string desc = "请输入数字：", UIStyle style = UIStyle.Blue)
        {
            return InputDoubleDialog(ref value, minimum, maximum, decimals, checkEmpty, desc, style, form.TopMost);
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
            return form.ShowSelectDialog(ref selectIndex, items, UILocalize.SelectTitle, "", style, form.TopMost);
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
            return form.ShowSelectDialog(ref selectIndex, items, title, description, form.Style, form.TopMost);
        }

        public static bool ShowSelectDialog(this UIPage form, ref int selectIndex, IList items, string title, string description)
        {
            return form.ShowSelectDialog(ref selectIndex, items, title, description, form.Style, form.TopMost);
        }
    }

    public static class UINotifierHelper
    {
        public static void ShowNotifier(string desc, UINotifierType type = UINotifierType.INFO, string title = "Notifier", bool isDialog = false, int timeout = 0, Form inApp = null)
        {
            UINotifier.Show(desc, type, title, isDialog, timeout, inApp);
        }
    }

    public static class FormEx
    {
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

        public static Form ShowControlMask(this Control control)
        {
            bool topmost = false;
            Form baseForm = control.RootForm();
            if (baseForm != null)
            {
                topmost = baseForm.TopMost;
                baseForm.TopMost = true;
                Application.DoEvents();
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

            var pt = control.LocationOnScreen();
            mask.Left = pt.X;
            mask.Top = pt.Y;
            mask.Show();
            mask.TopMost = true;
            return mask;
        }

        public static void EndShow(this Form maskForm)
        {
            if (maskForm.Tag is Form form)
            {
                form.TopMost = maskForm.Text.ToBoolean();
            }
        }

        public static void ShowInMask(this Form frm, Form maskForm)
        {
            frm.StartPosition = FormStartPosition.Manual;
            frm.Left = maskForm.Left + (maskForm.Width - frm.Width) / 2;
            frm.Top = maskForm.Top + (maskForm.Height - frm.Height) / 2;
            frm.ShowInTaskbar = false;
            frm.TopMost = true;
        }
    }
}