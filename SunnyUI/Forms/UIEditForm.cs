/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2021 ShenYongHua(沈永华).
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
 * 文件名称: UIEditForm.cs
 * 文件说明: 编辑窗体基类
 * 当前版本: V3.0
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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

        private readonly UIEditOption Option;

        public UIEditForm(UIEditOption option)
        {
            InitializeComponent();

            btnOK.Text = UILocalize.OK;
            btnCancel.Text = UILocalize.Cancel;

            Option = option;
            if (option == null || option.Infos.Count == 0) return;

            base.Text = option.Text;
            int top = 55;

            List<Control> ctrls = new List<Control>();

            if (option.AutoLabelWidth)
            {
                float size = 0;
                foreach (var info in option.Infos)
                {
                    SizeF sf = info.Text.MeasureString(UIFontColor.Font);
                    size = Math.Max(sf.Width, size);
                }

                option.LabelWidth = (int)size + 1 + 50;
            }

            Width = option.LabelWidth + option.ValueWidth + 28;

            foreach (var info in option.Infos)
            {
                UILabel label = new UILabel();
                label.Text = info.Text;
                label.AutoSize = false;
                label.Left = 5;
                label.Width = option.LabelWidth - 25;
                label.Height = 29;
                label.Top = top;
                label.TextAlign = ContentAlignment.MiddleRight;
                label.Parent = this;

                if (info.EditType == EditType.Text)
                {
                    UITextBox edit = new UITextBox();
                    edit.Left = option.LabelWidth;
                    edit.Width = option.ValueWidth;
                    edit.Top = top;
                    edit.Text = info.Value?.ToString();
                    edit.Parent = this;
                    edit.Name = "Edit_" + info.DataPropertyName;
                    edit.EnterAsTab = true;
                    edit.Enabled = info.Enabled;
                    ctrls.Add(edit);
                }

                if (info.EditType == EditType.Password)
                {
                    UITextBox edit = new UITextBox();
                    edit.Left = option.LabelWidth;
                    edit.Width = option.ValueWidth;
                    edit.Top = top;
                    edit.Text = info.Value?.ToString();
                    edit.Parent = this;
                    edit.PasswordChar = '*';
                    edit.Name = "Edit_" + info.DataPropertyName;
                    edit.EnterAsTab = true;
                    edit.Enabled = info.Enabled;
                    ctrls.Add(edit);
                }

                if (info.EditType == EditType.Integer)
                {
                    UITextBox edit = new UITextBox();
                    edit.Type = UITextBox.UIEditType.Integer;
                    edit.Left = option.LabelWidth;
                    edit.Width = info.HalfWidth ? option.ValueWidth / 2 : option.ValueWidth;
                    edit.Top = top;
                    edit.IntValue = info.Value.ToString().ToInt();
                    edit.Parent = this;
                    edit.Name = "Edit_" + info.DataPropertyName;
                    edit.Enabled = info.Enabled;
                    ctrls.Add(edit);
                }

                if (info.EditType == EditType.Double)
                {
                    UITextBox edit = new UITextBox();
                    edit.Type = UITextBox.UIEditType.Double;
                    edit.Left = option.LabelWidth;
                    edit.Width = info.HalfWidth ? option.ValueWidth / 2 : option.ValueWidth;
                    edit.Top = top;
                    edit.DoubleValue = info.Value.ToString().ToDouble();
                    edit.Parent = this;
                    edit.Name = "Edit_" + info.DataPropertyName;
                    edit.EnterAsTab = true;
                    edit.Enabled = info.Enabled;
                    ctrls.Add(edit);
                }

                if (info.EditType == EditType.Date)
                {
                    UIDatePicker edit = new UIDatePicker();
                    edit.Left = option.LabelWidth;
                    edit.Width = info.HalfWidth ? option.ValueWidth / 2 : option.ValueWidth;
                    edit.Top = top;
                    edit.Value = (DateTime)info.Value;
                    edit.Parent = this;
                    edit.Name = "Edit_" + info.DataPropertyName;
                    edit.Enabled = info.Enabled;
                    ctrls.Add(edit);
                }

                if (info.EditType == EditType.DateTime)
                {
                    UIDatetimePicker edit = new UIDatetimePicker();
                    edit.Left = option.LabelWidth;
                    edit.Width = info.HalfWidth ? option.ValueWidth / 2 : option.ValueWidth;
                    edit.Top = top;
                    edit.Value = (DateTime)info.Value;
                    edit.Parent = this;
                    edit.Name = "Edit_" + info.DataPropertyName;
                    edit.Enabled = info.Enabled;
                    ctrls.Add(edit);
                }

                if (info.EditType == EditType.Switch)
                {
                    UISwitch edit = new UISwitch();
                    edit.SwitchShape = UISwitch.UISwitchShape.Square;
                    edit.Left = option.LabelWidth - 1;
                    edit.Width = 75;
                    edit.Height = 29;
                    edit.Top = top;
                    edit.Active = (bool)info.Value;
                    edit.Parent = this;
                    edit.Name = "Edit_" + info.DataPropertyName;
                    edit.Enabled = info.Enabled;
                    ctrls.Add(edit);
                }

                if (info.EditType == EditType.Combobox)
                {
                    UIComboBox edit = new UIComboBox();
                    edit.DropDownStyle = UIDropDownStyle.DropDownList;
                    edit.Left = option.LabelWidth;
                    edit.Width = info.HalfWidth ? option.ValueWidth / 2 : option.ValueWidth;
                    edit.Top = top;
                    edit.Parent = this;
                    edit.Name = "Edit_" + info.DataPropertyName;
                    edit.Enabled = info.Enabled;

                    if (info.DisplayMember.IsNullOrEmpty())
                    {
                        object[] items = (object[])info.DataSource;
                        if (items != null)
                        {
                            edit.Items.AddRange(items);
                            int index = info.Value.ToString().ToInt();
                            if (index < items.Length)
                                edit.SelectedIndex = index;
                        }
                    }
                    else
                    {
                        edit.DisplayMember = info.DisplayMember;
                        edit.ValueMember = info.ValueMember;
                        edit.DataSource = info.DataSource;
                        edit.SelectedValue = info.Value;
                    }


                    ctrls.Add(edit);
                }

                top += 29 + 10;
            }

            pnlBtm.BringToFront();
            Height = top + 10 + 55;

            int tabIndex = 0;
            foreach (var ctrl in ctrls)
            {
                ctrl.TabIndex = tabIndex;
                tabIndex++;
            }

            pnlBtm.TabIndex = tabIndex;
            tabIndex++;
            btnOK.TabIndex = tabIndex;
            tabIndex++;
            btnCancel.TabIndex = tabIndex;
            btnOK.ShowFocusLine = btnCancel.ShowFocusLine = true;
        }

        public object this[string dataPropertyName]
        {
            get
            {
                if (Option == null)
                {
                    throw new ArgumentNullException();
                }

                if (!Option.Dictionary.ContainsKey(dataPropertyName))
                {
                    throw new ArgumentOutOfRangeException();
                }

                return Option.Dictionary[dataPropertyName].Value;
            }
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

            if (CheckedData != null)
            {
                if (!CheckedData.Invoke(this, new EditFormEventArgs(this)))
                {
                    return;
                }
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

        public void SetEditorFocus(string dataPropertyName)
        {
            Control editor = this.GetControl<UITextBox>("Edit_" + dataPropertyName);
            if (editor != null)
                editor.Focus();
        }

        protected virtual bool CheckData()
        {
            if (Option != null)
            {
                foreach (var info in Option.Infos)
                {
                    if (info.EditType == EditType.Text || info.EditType == EditType.Password)
                    {
                        UITextBox edit = this.GetControl<UITextBox>("Edit_" + info.DataPropertyName);
                        if (edit == null) continue;

                        if (info.CheckEmpty && edit.Text.IsNullOrEmpty())
                        {
                            ShowWarningTip(edit, info.Text + "不能为空");
                            edit.Focus();
                            return false;
                        }

                        info.Value = edit.Text;
                    }

                    if (info.EditType == EditType.Integer)
                    {
                        UITextBox edit = this.GetControl<UITextBox>("Edit_" + info.DataPropertyName);
                        if (edit == null) continue;
                        info.Value = edit.IntValue;
                    }

                    if (info.EditType == EditType.Double)
                    {
                        UITextBox edit = this.GetControl<UITextBox>("Edit_" + info.DataPropertyName);
                        if (edit == null) continue;
                        info.Value = edit.DoubleValue;
                    }

                    if (info.EditType == EditType.Date)
                    {
                        UIDatePicker edit = this.GetControl<UIDatePicker>("Edit_" + info.DataPropertyName);
                        if (edit == null) continue;
                        info.Value = edit.Value.Date;
                    }

                    if (info.EditType == EditType.DateTime)
                    {
                        UIDatetimePicker edit = this.GetControl<UIDatetimePicker>("Edit_" + info.DataPropertyName);
                        if (edit == null) continue;
                        info.Value = edit.Value;
                    }

                    if (info.EditType == EditType.Combobox)
                    {
                        UIComboBox edit = this.GetControl<UIComboBox>("Edit_" + info.DataPropertyName);
                        if (edit == null) continue;
                        info.Value = edit.ValueMember.IsValid() ? edit.SelectedValue : edit.SelectedIndex;
                    }

                    if (info.EditType == EditType.Switch)
                    {
                        UISwitch edit = this.GetControl<UISwitch>("Edit_" + info.DataPropertyName);
                        if (edit == null) continue;
                        info.Value = edit.Active;
                    }
                }
            }

            return true;
        }

        public delegate bool OnCheckedData(object sender, EditFormEventArgs e);

        public event OnCheckedData CheckedData;

        public class EditFormEventArgs : EventArgs
        {
            public EditFormEventArgs()
            {

            }

            public EditFormEventArgs(UIEditForm editor)
            {
                Form = editor;
            }

            public UIEditForm Form { get; set; }
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