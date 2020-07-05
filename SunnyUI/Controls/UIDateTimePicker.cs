using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    [DefaultProperty("Value")]
    [DefaultEvent("ValueChanged")]
    public sealed partial class UIDatetimePicker : UIDropControl
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UIDateTimePicker
            // 
            this.Name = "UIDatetimePicker";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.ButtonClick += new System.EventHandler(this.UIDatetimePicker_ButtonClick);
            this.ResumeLayout(false);
            this.PerformLayout();

            DropDownStyle = UIDropDownStyle.DropDownList;
        }

        public UIDatetimePicker()
        {
            InitializeComponent();
            Value = DateTime.Now;
        }

        public delegate void OnDateTimeChanged(object sender, DateTime value);


        public event OnDateTimeChanged ValueChanged;

        protected override void ItemForm_ValueChanged(object sender, object value)
        {
            Value = (DateTime)value;
            Text = Value.ToString(dateFormat);
            Invalidate();
            ValueChanged?.Invoke(this, Value);
        }

        private readonly UIDateTimeItem item = new UIDateTimeItem();

        protected override void CreateInstance()
        {
            ItemForm = new UIDropDown(item);
        }

        public DateTime Value
        {
            get => item.Date;
            set
            {
                Text = value.ToString(dateFormat);
                item.Date = value;
            }
        }

        private void UIDatetimePicker_ButtonClick(object sender, EventArgs e)
        {
            item.Date = Value;
            ItemForm.Show(this);
        }

        private string dateFormat = "yyyy-MM-dd HH:mm:ss";

        [Description("日期格式化掩码"), Category("自定义")]
        [DefaultValue("yyyy-MM-dd HH:mm:ss")]
        public string DateFormat
        {
            get => dateFormat;
            set
            {
                dateFormat = value;
                Text = Value.ToString(dateFormat);
            }
        }
    }
}
