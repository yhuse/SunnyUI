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
 * 文件名称: UIEditForm.cs
 * 文件说明: 编辑窗体基类
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
    public partial class UIEditForm : UIForm
    {
        public UIEditForm()
        {
            InitializeComponent();

            btnOK.Text = UILocalize.OK;
            btnCancel.Text = UILocalize.Cancel;
        }

        public bool IsOK { get; private set; }

        [Category("SunnyUI"), Description("确定按钮点击事件")]
        public event EventHandler ButtonOkClick;

        [Category("SunnyUI"), Description("取消按钮点击事件")]
        public event EventHandler ButtonCancelClick;

        [Description("确定按钮可用状态"), Category("SunnyUI")]
        [DefaultValue(true)]
        public bool ButtonOKEnabled
        {
            get => btnOK.Enabled;
            set => btnOK.Enabled = value;
        }

        [Description("取消按钮可用状态"), Category("SunnyUI")]
        [DefaultValue(true)]
        public bool ButtonCancelEnabled
        {
            get => btnCancel.Enabled;
            set => btnCancel.Enabled = value;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (!CheckData())
            {
                return;
            }

            if (ButtonOkClick != null)
            {
                ButtonOkClick.Invoke(sender, e);
            }
            else
            {
                DialogResult = DialogResult.OK;
                IsOK = true;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (ButtonCancelClick != null)
            {
                ButtonCancelClick.Invoke(sender, e);
            }
            else
            {
                DialogResult = DialogResult.Cancel;
                IsOK = false;
                Close();
            }
        }

        protected virtual bool CheckData()
        {
            return true;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (btnOK != null && btnCancel != null)
            {
                btnCancel.Left = Width - 130;
                btnOK.Left = Width - 245;
            }
        }

        protected bool CheckEmpty(UITextBox edit, string desc)
        {
            bool result = edit.Text.IsValid();
            if (!result)
            {
                this.ShowWarningDialog(desc);
                edit.Focus();
            }

            return result;
        }

        protected bool CheckRange(UITextBox edit, int min, int max, string desc)
        {
            bool result = edit.IntValue >= min && edit.IntValue <= max;
            if (!result)
            {
                this.ShowWarningDialog(desc);
                edit.Focus();
            }

            return result;
        }

        protected bool CheckRange(UITextBox edit, double min, double max, string desc)
        {
            bool result = edit.DoubleValue >= min && edit.IntValue <= max;
            if (!result)
            {
                this.ShowWarningDialog(desc);
                edit.Focus();
            }

            return result;
        }

        protected bool CheckEmpty(UIComboBox edit, string desc)
        {
            bool result = edit.Text.IsValid();
            if (!result)
            {
                this.ShowWarningDialog(desc);
                edit.Focus();
            }

            return result;
        }

        protected bool CheckEmpty(UIDatePicker edit, string desc)
        {
            bool result = edit.Text.IsValid();
            if (!result)
            {
                this.ShowWarningDialog(desc);
                edit.Focus();
            }

            return result;
        }
    }
}