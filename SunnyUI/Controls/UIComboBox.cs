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
 * 文件名称: UIComboBox.cs
 * 文件说明: 组合框
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-06-11: V2.2.5 增加DataSource，支持数据绑定
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using static System.Windows.Forms.ComboBox;

namespace Sunny.UI
{
    [DefaultProperty("Items")]
    [DefaultEvent("SelectedIndexChanged")]
    [ToolboxItem(true)]
    [LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")]
    public sealed partial class UIComboBox : UIDropControl
    {
        public UIComboBox()
        {
            InitializeComponent();

            box.SelectedIndexChanged += Box_SelectedIndexChanged;
            box.DataSourceChanged += Box_DataSourceChanged;
            box.DisplayMemberChanged += Box_DisplayMemberChanged;
            box.ValueMemberChanged += Box_ValueMemberChanged;
        }

        private void Box_ValueMemberChanged(object sender, EventArgs e)
        {
            ValueMemberChanged?.Invoke(sender, e);
        }

        private void Box_DisplayMemberChanged(object sender, EventArgs e)
        {
            DisplayMemberChanged?.Invoke(sender, e);
        }

        private void Box_DataSourceChanged(object sender, EventArgs e)
        {
            DataSourceChanged?.Invoke(sender, e);
        }

        private void Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            Text = box.GetItemText(box.SelectedItem);
            GetSelectedValue();
            SelectedValueChanged?.Invoke(this, e);
            SelectedIndexChanged?.Invoke(this, e);
        }

        public event EventHandler SelectedIndexChanged;

        public event EventHandler DataSourceChanged;

        public event EventHandler DisplayMemberChanged;

        public event EventHandler ValueMemberChanged;

        public event EventHandler SelectedValueChanged;

        public readonly ComboBox box = new ComboBox();

        protected override void ItemForm_ValueChanged(object sender, object value)
        {
            SelectedIndex = ListBox.SelectedIndex;
            Box_SelectedIndexChanged(null, null);
            Invalidate();
        }

        private readonly UIComboBoxItem dropForm = new UIComboBoxItem();

        protected override void CreateInstance()
        {
            ItemForm = new UIDropDown(dropForm);
        }

        protected override int CalcItemFormHeight()
        {
            int interval = ItemForm.Height - ItemForm.ClientRectangle.Height;
            return 4 + Math.Min(ListBox.Items.Count, MaxDropDownItems) * ItemHeight + interval;
        }

        private UIListBox ListBox
        {
            get => dropForm.ListBox;
        }

        [DefaultValue(25)]
        [Description("列表项高度"), Category("SunnyUI")]
        public int ItemHeight
        {
            get => ListBox.ItemHeight;
            set => ListBox.ItemHeight = value;
        }

        [DefaultValue(8)]
        [Description("列表下拉最大个数"), Category("SunnyUI")]
        public int MaxDropDownItems { get; set; } = 8;

        private void UIComboBox_FontChanged(object sender, EventArgs e)
        {
            if (ItemForm != null)
            {
                ListBox.Font = Font;
            }
        }

        private void UIComboBox_ButtonClick(object sender, EventArgs e)
        {
            ListBox.Items.Clear();
            if (Items.Count == 0 || ItemForm.Visible)
            {
                return;
            }

            foreach (var data in Items)
            {
                ListBox.Items.Add(GetItemText(data));
            }

            ListBox.SelectedIndex = SelectedIndex;
            ItemForm.Show(this, new Size(Width, CalcItemFormHeight()));
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            ListBox.SetStyleColor(uiColor);
        }

        private object dataSource;

        [DefaultValue(null), RefreshProperties(RefreshProperties.Repaint), AttributeProvider(typeof(IListSource))]
        [Description("数据源"), Category("SunnyUI")]
        public object DataSource
        {
            get => dataSource;
            set
            {
                SetDataConnection(value, new BindingMemberInfo(DisplayMember));
                dataSource = value;
                box.Items.Clear();

                if (dataManager != null)
                {
                    foreach (var obj in dataManager.List)
                    {
                        box.Items.Add(obj);
                    }
                }
            }
        }

        private bool inSetDataConnection;
        private CurrencyManager dataManager;

        private void SetDataConnection(object newDataSource, BindingMemberInfo newDisplayMember)
        {
            bool dataSourceChanged = dataSource != newDataSource;
            bool displayMemberChanged = !DisplayMember.Equals(newDisplayMember.BindingPath);

            if (inSetDataConnection)
            {
                return;
            }

            try
            {
                inSetDataConnection = true;
                if (dataSourceChanged || displayMemberChanged)
                {
                    CurrencyManager newDataManager = null;
                    if (newDataSource != null && newDataSource != Convert.DBNull)
                    {
                        newDataManager = (CurrencyManager)BindingContext[newDataSource, newDisplayMember.BindingPath];
                    }

                    dataManager = newDataManager;
                }
            }
            finally
            {
                inSetDataConnection = false;
            }
        }

        //public int DropDownWidth { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        [MergableProperty(false)]
        [Description("下拉显示项"), Category("SunnyUI")]
        public ObjectCollection Items
        {
            get => box.Items;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("选中索引"), Category("SunnyUI")]
        public int SelectedIndex
        {
            get => box.SelectedIndex;
            set => box.SelectedIndex = value;
        }

        [Browsable(false), Bindable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("选中项"), Category("SunnyUI")]
        public object SelectedItem
        {
            get => box.SelectedItem;
            set => box.SelectedItem = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("选中文字"), Category("SunnyUI")]
        public string SelectedText
        {
            get
            {
                if (DropDownStyle == UIDropDownStyle.DropDown)
                {
                    return edit.SelectedText;
                }
                else
                {
                    return Text;
                }
            }
        }

        public override void ResetText()
        {
            Clear();
        }

        [Description("获取或设置要为此列表框显示的属性。"), Category("SunnyUI")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string DisplayMember
        {
            get => box.DisplayMember;
            set
            {
                SetDataConnection(dataSource, new BindingMemberInfo(value));
                box.DisplayMember = value;
            }
        }

        [Description("获取或设置指示显示值的方式的格式说明符字符。"), Category("SunnyUI")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.FormatStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        public string FormatString
        {
            get => box.FormatString;
            set => box.FormatString = value;
        }

        [Description("获取或设置指示显示值是否可以进行格式化操作。"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool FormattingEnabled
        {
            get => box.FormattingEnabled;
            set => box.FormattingEnabled = value;
        }

        [Description("获取或设置要为此列表框实际值的属性。"), Category("SunnyUI")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string ValueMember
        {
            get => box.ValueMember;
            set => box.ValueMember = value;
        }

        private object selectedValue;

        private void GetSelectedValue()
        {
            if (SelectedIndex != -1 && dataManager != null)
            {
                selectedValue = FilterItemOnProperty(SelectedItem, ValueMember);
            }
            else
            {
                selectedValue = null;
            }
        }

        [
            DefaultValue(null),
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Bindable(true)
        ]
        public object SelectedValue
        {
            get
            {
                GetSelectedValue();
                return selectedValue;
            }
            set
            {
                selectedValue = value;

                if (value == null)
                {
                    return;
                }

                if (dataManager != null)
                {
                    int index = DataManagerFind(value);
                    SelectedIndex = index;
                }
            }
        }

        private int DataManagerFind(object value)
        {
            if (dataManager == null) return -1;
            for (int i = 0; i < dataManager.List.Count; i++)
            {
                var item = dataManager.List[i];
                if (FilterItemOnProperty(item, ValueMember).Equals(value))
                {
                    return i;
                }
            }

            return -1;
        }

        private object FilterItemOnProperty(object item, string field)
        {
            if (item != null && field.Length > 0)
            {
                try
                {
                    // if we have a dataSource, then use that to display the string
                    PropertyDescriptor descriptor;
                    if (dataManager != null)
                        descriptor = dataManager.GetItemProperties().Find(field, true);
                    else
                        descriptor = TypeDescriptor.GetProperties(item).Find(field, true);

                    if (descriptor != null)
                    {
                        item = descriptor.GetValue(item);
                    }
                }
                catch
                {
                    return null;
                }
            }

            return item;
        }

        public string GetItemText(object item)
        {
            return box.GetItemText(item);
        }
    }
}