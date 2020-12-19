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
 * 文件名称: UIDatePicker.cs
 * 文件说明: 时间选择框
 * 当前版本: V2.2
 * 创建日期: 2020-05-29
 *
 * 2020-05-29: V2.2.5 增加文件
 * 2020-08-07: V2.2.7 可编辑输入
 * 2020-09-16: V2.2.7 更改滚轮选择时间的方向
******************************************************************************/

using System;
using System.ComponentModel;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    [DefaultProperty("Value")]
    [DefaultEvent("ValueChanged")]
    public sealed partial class UITimePicker : UIDropControl
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // UITimePicker
            //
            this.Name = "UITimePicker";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.SymbolDropDown = 61555;
            this.SymbolNormal = 61555;
            this.ButtonClick += new System.EventHandler(this.UITimePicker_ButtonClick);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public UITimePicker()
        {
            InitializeComponent();
            Value = DateTime.Now;

            EditorLostFocus += UIDatePicker_LostFocus;
            TextChanged += UIDatePicker_TextChanged;
            MaxLength = 8;
        }

        [DefaultValue(false)]
        [Description("日期输入时，是否可空显示"), Category("SunnyUI")]
        public bool CanEmpty { get; set; }

        private void UIDatePicker_TextChanged(object sender, EventArgs e)
        {
            if (Text.Length == MaxLength)
            {
                try
                {
                    DateTime dt = (DateTime.Now.DateString() + " " + Text).ToDateTime(DateTimeEx.DateFormat + " " + timeFormat);
                    Value = dt;
                }
                catch
                {
                    Value = DateTime.Now.Date;
                }
            }
        }

        private void UIDatePicker_LostFocus(object sender, EventArgs e)
        {
            if (Text.IsNullOrEmpty())
            {
                if (CanEmpty) return;
            }

            try
            {
                DateTime dt = (DateTime.Now.DateString() + " " + Text).ToDateTime(DateTimeEx.DateFormat + " " + timeFormat);
                Value = dt;
            }
            catch
            {
                Value = DateTime.Now.Date;
            }
        }

        public delegate void OnDateTimeChanged(object sender, DateTime value);

        public event OnDateTimeChanged ValueChanged;

        protected override void ItemForm_ValueChanged(object sender, object value)
        {
            Value = (DateTime)value;
            Text = Value.ToString(timeFormat);
            Invalidate();
            ValueChanged?.Invoke(this, Value);
        }

        private readonly UITimeItem item = new UITimeItem();

        protected override void CreateInstance()
        {
            ItemForm = new UIDropDown(item);
        }

        [Description("选中时间"), Category("SunnyUI")]
        public DateTime Value
        {
            get => item.Time;
            set
            {
                Text = value.ToString(timeFormat);
                item.Time = value;
            }
        }

        private string timeFormat = "HH:mm:ss";

        [Description("时间格式化掩码"), Category("SunnyUI")]
        [DefaultValue("HH:mm:ss")]
        public string TimeFormat
        {
            get => timeFormat;
            set
            {
                timeFormat = value;
                Text = Value.ToString(timeFormat);
                MaxLength = timeFormat.Length;
            }
        }

        private void UITimePicker_ButtonClick(object sender, EventArgs e)
        {
            item.Time = Value;
            ItemForm.Show(this);
        }
    }
}