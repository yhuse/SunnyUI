/******************************************************************************
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
 * 文件名称: UIDatePicker.cs
 * 文件说明: 时间选择框
 * 当前版本: V3.1
 * 创建日期: 2020-05-29
 *
 * 2020-05-29: V2.2.5 增加文件
 * 2020-08-07: V2.2.7 可编辑输入
 * 2020-09-16: V2.2.7 更改滚轮选择时间的方向
 * 2024-06-09: V3.6.6 下拉框可选放大倍数为2
 * 2024-11-10: V3.7.2 增加StyleDropDown属性，手动修改Style时设置此属性以修改下拉框主题
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    [DefaultProperty("Value")]
    [DefaultEvent("ValueChanged")]
    [Description("时间选择框控件")]
    public sealed class UITimePicker : UIDropControl, IToolTip
    {
        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // UITimePicker
            // 
            Name = "UITimePicker";
            SymbolDropDown = 61555;
            SymbolNormal = 61555;
            ButtonClick += UITimePicker_ButtonClick;
            ResumeLayout(false);
            PerformLayout();
        }

        [Browsable(false)]
        public override string[] FormTranslatorProperties => ["TimeFormat"];

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

            item?.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// 需要额外设置ToolTip的控件
        /// </summary>
        /// <returns>控件</returns>
        public Control ExToolTipControl()
        {
            return edit;
        }

        public UITimePicker()
        {
            InitializeComponent();
            Value = DateTime.Now;

            EditorLostFocus += UIDatePicker_LostFocus;
            TextChanged += UIDatePicker_TextChanged;
            MaxLength = 8;

            CreateInstance();
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
                    if (Value != dt) Value = dt;
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
                if (Value != dt) Value = dt;
            }
            catch
            {
                Value = DateTime.Now.Date;
            }
        }

        public delegate void OnDateTimeChanged(object sender, DateTime value);

        public event OnDateTimeChanged ValueChanged;

        /// <summary>
        /// 值改变事件
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="value">值</param>
        protected override void ItemForm_ValueChanged(object sender, object value)
        {
            Value = (DateTime)value;
            Invalidate();
        }

        private readonly UITimeItem item = new UITimeItem();

        [DefaultValue(1)]
        [Description("弹窗放大倍数，可以1或者2"), Category("SunnyUI")]
        public int SizeMultiple { get => item.SizeMultiple; set => item.SizeMultiple = value; }

        /// <summary>
        /// 创建对象
        /// </summary>
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
                if (item.Time != value)
                {
                    item.Time = value;
                }

                ValueChanged?.Invoke(this, Value);
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
            item.Translate();
            item.SetDPIScale();
            item.SetStyleColor(UIStyles.ActiveStyleColor);
            if (StyleDropDown != UIStyle.Inherited) item.Style = StyleDropDown;
            Size size = SizeMultiple == 1 ? new Size(168, 200) : new Size(336, 400);
            ItemForm.Show(this, size);
        }
    }
}