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
 * 2020-06-11: V2.2.5 增加DataSource
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
            box.SelectionChangeCommitted += Box_SelectionChangeCommitted;
            box.TextUpdate += Box_TextUpdate;
            box.DataSourceChanged += Box_DataSourceChanged;
            box.DisplayMemberChanged += Box_DisplayMemberChanged;
            box.Format += Box_Format;
            box.FormatInfoChanged += Box_FormatInfoChanged;
            box.FormatStringChanged += Box_FormatStringChanged;
            box.FormattingEnabledChanged += Box_FormattingEnabledChanged;
            box.ValueMemberChanged += Box_ValueMemberChanged;
            box.SelectedValueChanged += Box_SelectedValueChanged;
        }

        private void Box_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedValueChanged?.Invoke(sender, e);
        }

        private void Box_ValueMemberChanged(object sender, EventArgs e)
        {
            ValueMemberChanged?.Invoke(sender, e);
        }

        private void Box_FormattingEnabledChanged(object sender, EventArgs e)
        {
            FormattingEnabledChanged?.Invoke(sender, e);
        }

        private void Box_FormatStringChanged(object sender, EventArgs e)
        {
            FormatStringChanged?.Invoke(sender, e);
        }

        private void Box_FormatInfoChanged(object sender, EventArgs e)
        {
            FormatInfoChanged?.Invoke(sender, e);
        }

        private void Box_Format(object sender, ListControlConvertEventArgs e)
        {
            Format?.Invoke(sender, e);
        }

        private void Box_DisplayMemberChanged(object sender, EventArgs e)
        {
            DisplayMemberChanged?.Invoke(sender, e);
        }

        private void Box_DataSourceChanged(object sender, EventArgs e)
        {
            DataSourceChanged?.Invoke(sender, e);
        }

        private void Box_TextUpdate(object sender, EventArgs e)
        {
            TextUpdate?.Invoke(sender, e);
        }

        private void Box_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SelectionChangeCommitted?.Invoke(sender, e);
        }

        private void Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged?.Invoke(sender, e);
        }

        public event EventHandler TextUpdate;

        public event EventHandler SelectedIndexChanged;

        public event EventHandler SelectionChangeCommitted;

        public event EventHandler DataSourceChanged;

        public event EventHandler DisplayMemberChanged;

        public event ListControlConvertEventHandler Format;

        public event EventHandler FormatStringChanged;

        public event EventHandler FormattingEnabledChanged;

        public event EventHandler ValueMemberChanged;

        public event EventHandler SelectedValueChanged;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public event EventHandler FormatInfoChanged;

        private readonly ComboBox box = new ComboBox();

        protected override void ItemForm_ValueChanged(object sender, object value)
        {
            //selectedItem = ListBox.SelectedItem;
            //selectedIndex = ListBox.SelectedIndex;
            box.SelectedIndex = ListBox.SelectedIndex;
            Text = ListBox.Text;
            Invalidate();
            //SelectedIndexChanged?.Invoke(this, null);
        }

        private readonly UIComboBoxItem item = new UIComboBoxItem();

        protected override void CreateInstance()
        {
            ItemForm = new UIDropDown(item);
        }

        protected override int CalcItemFormHeight()
        {
            int interval = ItemForm.Height - ItemForm.ClientRectangle.Height;
            return 4 + Math.Min(Items.Count, MaxDropDownItems) * ItemHeight + interval;
        }

        private UIListBox ListBox
        {
            get => item.ListBox;
        }

        [DefaultValue(25)]
        public int ItemHeight
        {
            get => ListBox.ItemHeight;
            set => ListBox.ItemHeight = value;
        }

        [DefaultValue(8)]
        public int MaxDropDownItems { get; set; } = 8;

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //[Localizable(true)]
        //[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        //[MergableProperty(false)]
        //public ListBox.ObjectCollection Items => ListBox?.Items;

        private void UIComboBox_FontChanged(object sender, EventArgs e)
        {
            if (ItemForm != null)
            {
                ListBox.Font = Font;
            }
        }

        private void UIComboBox_ButtonClick(object sender, EventArgs e)
        {
            if (Items.Count == 0 || ItemForm.Visible)
            {
                return;
            }

            ListBox.Items.Clear();

            foreach (var item in Items)
            {
                ListBox.Items.Add(GetItemText(item));                
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

        [
        DefaultValue(AutoCompleteMode.None),
        Browsable(true), EditorBrowsable(EditorBrowsableState.Always)
        ]
        public AutoCompleteMode AutoCompleteMode
        {
            get => box.AutoCompleteMode;
            set => box.AutoCompleteMode = value;
        }

        [
       DefaultValue(AutoCompleteSource.None),
       Browsable(true), EditorBrowsable(EditorBrowsableState.Always)
       ]
        public AutoCompleteSource AutoCompleteSource
        {
            get => box.AutoCompleteSource;
            set => box.AutoCompleteSource = value;
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get => box.AutoCompleteCustomSource;
            set => box.AutoCompleteCustomSource = value;
        }

        [DefaultValue(null), RefreshProperties(RefreshProperties.Repaint), AttributeProvider(typeof(IListSource))]
        public object DataSource
        {
            get => box.DataSource;
            set => box.DataSource = value;
        }

        [DefaultValue(DrawMode.Normal), RefreshProperties(RefreshProperties.Repaint)]
        public DrawMode DrawMode
        {
            get => box.DrawMode;
            set => box.DrawMode = value;
        }

        public int DropDownWidth { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        [MergableProperty(false)]
        public ObjectCollection Items
        {
            get => box.Items;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get => box.SelectedIndex;
            set => box.SelectedIndex = value;
        }

        [Browsable(false), Bindable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem
        {
            get => box.SelectedItem;
            set => box.SelectedItem = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText
        {
            get => box.SelectedText;
            set => box.SelectedText = value;
        }

        [DefaultValue(false)]
        public bool Sorted
        {
            get => box.Sorted;
            set => box.Sorted = value;
        }

        public void BeginUpdate()
        {
            box.BeginUpdate();
        }

        public void EndUpdate()
        {
            box.EndUpdate();
        }

        public int FindString(string s)
        {
            return box.FindString(s, -1);
        }

        public int FindString(string s, int startIndex)
        {
            return box.FindString(s, startIndex);
        }

        public int FindStringExact(string s)
        {
            return box.FindStringExact(s);
        }

        public int FindStringExact(string s, int startIndex)
        {
            return box.FindStringExact(s, startIndex);
        }

        public override void ResetText()
        {
            box.ResetText();
        }

        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string DisplayMember
        {
            get => box.DisplayMember;
            set => box.DisplayMember = value;
        }

        [
         Browsable(false),
         EditorBrowsable(EditorBrowsableState.Advanced),
         DefaultValue(null)
     ]
        public IFormatProvider FormatInfo
        {
            get => box.FormatInfo;
            set => box.FormatInfo = value;
        }

        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.FormatStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        /// <include file='doc\ListControl.uex' path='docs/doc[@for="ListControl.FormatString"]/*' />
        public string FormatString
        {
            get => box.FormatString;
            set => box.FormatString = value;
        }

        [DefaultValue(false)]
        public bool FormattingEnabled
        {
            get => box.FormattingEnabled;
            set => box.FormattingEnabled = value;
        }

        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string ValueMember
        {
            get => box.ValueMember;
            set => box.ValueMember = value;
        }

        [
DefaultValue(null),
Browsable(false),
DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
Bindable(true)
]
        public object SelectedValue
        {
            get => box.SelectedValue;
            set => box.SelectedValue = value;
        }

        public string GetItemText(object item)
        {
            return box.GetItemText(item);
        }

        //private int selectedIndex = -1;

        //[Browsable(false)]
        //[DefaultValue(-1)]
        //public int SelectedIndex
        //{
        //    get => selectedIndex;
        //    set
        //    {
        //        if (value.InRange(-1, ListBox.Items.Count - 1))
        //        {
        //            ListBox.SelectedIndex = value;
        //            selectedIndex = value;

        //            if (value >= 0)
        //            {
        //                Text = ListBox.Items[value].ToString();
        //            }
        //        }
        //    }
        //}

        //private object selectedItem;

        //[Browsable(false)]
        //[DefaultValue(null)]
        //public object SelectedItem
        //{
        //    get => selectedItem;
        //    set
        //    {
        //        if (value != null)
        //        {
        //            int idx = ListBox.Items.IndexOf(value);
        //            SelectedIndex = idx;
        //            selectedItem = idx >= 0 ? value : null;
        //        }
        //    }
        //}
    }
}