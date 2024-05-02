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
 * 文件名称: UIComboBox.cs
 * 文件说明: 组合框
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-06-11: V2.2.5 增加DataSource，支持数据绑定
 * 2021-05-06: V3.0.3 解决鼠标下拉选择，触发SelectedIndexChanged两次的问题
 * 2021-06-03: V3.0.4 更新了数据绑定相关代码
 * 2021-08-03: V3.0.5 Items.Clear后清除显示
 * 2021-08-15: V3.0.6 重写了水印文字的画法，并增加水印文字颜色
 * 2022-01-16: V3.1.0 增加了下拉框颜色设置
 * 2022-04-13: V3.1.3 根据Text自动选中SelectIndex
 * 2022-04-15: V3.1.3 增加过滤
 * 2022-04-16: V3.1.3 过滤下拉控跟随主题配色
 * 2022-04-20: V3.1.5 过滤文字为空时，下拉框显示所有数据列表
 * 2022-05-04: V3.1.8 过滤时修复ValueMember绑定值的显示
 * 2022-05-24: V3.1.9 Selceted=-1，清除文本
 * 2022-08-25: V3.2.3 下拉框边框可设置颜色
 * 2022-11-03: V3.2.6 过滤时删除字符串前面、后面的空格
 * 2022-11-13: V3.2.8 增加不显示过滤可以自动调整下拉框宽度
 * 2022-11-30: V3.3.0 增加Clear方法
 * 2023-02-04: V3.3.1 增加清除按钮
 * 2023-03-15: V3.3.3 修改失去焦点自动关闭过滤下拉框
 * 2023-06-28: V3.3.9 增加过滤时忽略大小写
 * 2023-07-03: V3.3.9 修改了几个对象的释放
 * 2023-08-11: V3.4.1 Items.Clear后，DropDownStyle为DropDown时，不清空Text
 * 2023-12-26: V3.6.2 增加下拉界面的滚动条设置
 * 2024-01-27: V3.6.3 修复在窗体构造函数设置SelectedIndex报错
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 组合框
    /// </summary>
    [DefaultProperty("Items")]
    [DefaultEvent("SelectedIndexChanged")]
    [ToolboxItem(true)]
    [LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")]
    public sealed partial class UIComboBox : UIDropControl, IToolTip, IHideDropDown
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UIComboBox()
        {
            InitializeComponent();
            ListBox.SelectedIndexChanged += Box_SelectedIndexChanged;
            ListBox.ValueMemberChanged += Box_ValueMemberChanged;
            ListBox.SelectedValueChanged += ListBox_SelectedValueChanged;
            ListBox.ItemsClear += ListBox_ItemsClear;
            ListBox.ItemsRemove += ListBox_ItemsRemove;

            filterForm.BeforeListClick += ListBox_Click;

            edit.TextChanged += Edit_TextChanged;
            edit.KeyDown += Edit_KeyDown;
            DropDownWidth = 150;
            fullControlSelect = true;

            CreateInstance();
        }

        [DefaultValue(0), Category("SunnyUI"), Description("垂直滚动条宽度，最小为原生滚动条宽度")]
        public int ScrollBarWidth
        {
            get => ListBox.ScrollBarWidth;
            set => ListBox.ScrollBarWidth = value;
        }

        [DefaultValue(6), Category("SunnyUI"), Description("垂直滚动条滑块宽度，最小为原生滚动条宽度")]
        public int ScrollBarHandleWidth
        {
            get => ListBox.ScrollBarHandleWidth;
            set => ListBox.ScrollBarHandleWidth = value;
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("滚动条填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color ScrollBarColor
        {
            get => ListBox.ScrollBarColor;
            set => ListBox.ScrollBarColor = value;
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("滚动条背景颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "243, 249, 255")]
        public Color ScrollBarBackColor
        {
            get => ListBox.ScrollBarBackColor;
            set => ListBox.ScrollBarBackColor = value;
        }

        /// <summary>
        /// 滚动条主题样式
        /// </summary>
        [DefaultValue(true), Description("滚动条主题样式"), Category("SunnyUI")]
        public bool ScrollBarStyleInherited
        {
            get => ListBox.ScrollBarStyleInherited;
            set => ListBox.ScrollBarStyleInherited = value;
        }

        [DefaultValue(false)]
        [Description("显示清除按钮"), Category("SunnyUI")]
        public bool ShowClearButton
        {
            get => showClearButton;
            set => showClearButton = value;
        }

        public override void Clear()
        {
            base.Clear();
            if (DataSource != null)
            {
                DataSource = null;
            }
            else
            {
                ListBox.Items.Clear();
            }
        }

        private void ListBox_Click(object sender, EventArgs e)
        {
            SelectTextChange = true;
            filterSelectedItem = filterList[(int)sender];
            filterSelectedValue = GetItemValue(filterSelectedItem);
            Text = GetItemText(filterSelectedItem).ToString();
            edit.SelectionStart = Text.Length;
            SelectedValueChanged?.Invoke(this, EventArgs.Empty);
            SelectTextChange = false;
        }

        private void ShowDropDownFilter()
        {
            if (Text.IsNullOrEmpty() && ShowFilter)
                FillFilterTextEmpty();

            FilterItemForm.AutoClose = false;
            if (!FilterItemForm.Visible)
            {
                FilterItemForm.Show(this, new Size(DropDownWidth < Width ? Width : DropDownWidth, CalcItemFormHeight()));
                edit.Focus();
            }
        }

        private void Edit_KeyDown(object sender, KeyEventArgs e)
        {
            if (ShowFilter)
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                {
                    if (!FilterItemForm.Visible)
                        ShowDropDownFilter();
                    int cnt = filterForm.ListBox.Items.Count;
                    int idx = filterForm.ListBox.SelectedIndex;

                    if (cnt > 0)
                    {
                        if (e.KeyCode == Keys.Down)
                        {
                            if (idx < cnt - 1)
                                filterForm.ListBox.SelectedIndex++;
                        }

                        if (e.KeyCode == Keys.Up)
                        {
                            if (idx > 0)
                                filterForm.ListBox.SelectedIndex--;
                        }
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    FilterItemForm.Close();
                }
                else if (e.KeyCode == Keys.Return)
                {
                    if (FilterItemForm.Visible)
                    {
                        int cnt = filterForm.ListBox.Items.Count;
                        int idx = filterForm.ListBox.SelectedIndex;

                        if (cnt > 0 && idx >= 0 && idx < cnt)
                        {
                            SelectTextChange = true;
                            filterSelectedItem = filterList[idx];
                            filterSelectedValue = GetItemValue(filterSelectedItem);
                            Text = GetItemText(filterSelectedItem).ToString();
                            edit.SelectionStart = Text.Length;
                            SelectedValueChanged?.Invoke(this, EventArgs.Empty);
                            SelectTextChange = false;
                        }

                        FilterItemForm.Close();
                    }
                    else
                    {
                        ShowDropDownFilter();
                    }
                }
                else
                {
                    base.OnKeyDown(e);
                }
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ShowDropDown();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    ItemForm.Close();
                }
                else
                {
                    base.OnKeyDown(e);
                }
            }
        }

        private object filterSelectedItem;
        private object filterSelectedValue;
        private bool showFilter;

        /// <summary>
        /// 显示过滤
        /// </summary>
        [DefaultValue(false)]
        [Description("显示过滤"), Category("SunnyUI")]
        public bool ShowFilter
        {
            get => showFilter;
            set
            {
                showFilter = value;
                if (value)
                {
                    DropDownStyle = UIDropDownStyle.DropDown;
                }
            }
        }

        /// <summary>
        /// 过滤显示最大条目数
        /// </summary>
        [DefaultValue(100)]
        [Description("过滤显示最大条目数"), Category("SunnyUI")]
        public int FilterMaxCount { get; set; } = 100;

        /// <summary>
        /// 下拉状态改变事件
        /// </summary>
        protected override void DropDownStyleChanged()
        {
            if (DropDownStyle == UIDropDownStyle.DropDownList)
            {
                showFilter = false;
            }
        }

        CurrencyManager dataManager;

        private void SetDataConnection()
        {
            if (!ShowFilter) return;

            if (DropDownStyle == UIDropDownStyle.DropDown && DataSource != null && DisplayMember.IsValid())
            {
                dataManager = (CurrencyManager)BindingContext[DataSource, new BindingMemberInfo(DisplayMember).BindingPath];
            }
        }

        private object GetItemValue(object item)
        {
            if (dataManager == null)
                return item;

            if (ValueMember.IsNullOrWhiteSpace())
                return null;

            PropertyDescriptor descriptor = dataManager.GetItemProperties().Find(ValueMemberBindingMemberInfo.BindingField, true);
            if (descriptor != null)
            {
                return descriptor.GetValue(item);
            }

            return null;
        }

        /// <summary>
        /// 需要额外设置ToolTip的控件
        /// </summary>
        /// <returns>控件</returns>
        public Control ExToolTipControl()
        {
            return edit;
        }

        [DefaultValue(false)]
        public bool Sorted
        {
            get => ListBox.Sorted;
            set => ListBox.Sorted = value;
        }

        public int FindString(string s)
        {
            return ListBox.FindString(s);
        }

        public int FindString(string s, int startIndex)
        {
            return ListBox.FindString(s, startIndex);
        }

        public int FindStringExact(string s)
        {
            return ListBox.FindStringExact(s);
        }

        public int FindStringExact(string s, int startIndex)
        {
            return ListBox.FindStringExact(s, startIndex);
        }

        private void ListBox_ItemsRemove(object sender, EventArgs e)
        {
            if (ListBox.Count == 0 && DropDownStyle == UIDropDownStyle.DropDownList)
            {
                Text = "";
                edit.Text = "";
            }
        }

        private void ListBox_ItemsClear(object sender, EventArgs e)
        {
            if (DropDownStyle == UIDropDownStyle.DropDownList)
            {
                Text = "";
                edit.Text = "";
            }
        }

        public new event EventHandler TextChanged;

        private void Edit_TextChanged(object sender, EventArgs e)
        {
            TextChanged?.Invoke(this, e);

            if (!ShowFilter)
            {
                if (SelectTextChange) return;
                if (Text.IsValid())
                {
                    ListBox.ListBox.Text = Text;
                }
                else
                {
                    SelectTextChange = true;
                    SelectedIndex = -1;
                    edit.Text = "";
                    SelectTextChange = false;
                }
            }
            else
            {
                if (DropDownStyle == UIDropDownStyle.DropDownList) return;
                if (edit.Focused && Text.IsValid())
                {
                    ShowDropDownFilter();
                }

                if (Text.IsValid())
                {
                    string filterText = Text;
                    if (TrimFilter)
                        filterText = filterText.Trim();

                    filterForm.ListBox.Items.Clear();
                    filterList.Clear();

                    if (DataSource == null)
                    {
                        foreach (var item in Items)
                        {
                            if (FilterIgnoreCase)
                            {
                                if (item.ToString().ToUpper().Contains(filterText.ToUpper()))
                                {
                                    filterList.Add(item.ToString());
                                    if (filterList.Count > FilterMaxCount) break;
                                }
                            }
                            else
                            {
                                if (item.ToString().Contains(filterText))
                                {
                                    filterList.Add(item.ToString());
                                    if (filterList.Count > FilterMaxCount) break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (dataManager != null)
                        {
                            for (int i = 0; i < Items.Count; i++)
                            {
                                if (FilterIgnoreCase)
                                {
                                    if (GetItemText(dataManager.List[i]).ToString().ToUpper().Contains(filterText.ToUpper()))
                                    {
                                        filterList.Add(dataManager.List[i]);
                                        if (filterList.Count > FilterMaxCount) break;
                                    }
                                }
                                else
                                {
                                    if (GetItemText(dataManager.List[i]).ToString().Contains(filterText))
                                    {
                                        filterList.Add(dataManager.List[i]);
                                        if (filterList.Count > FilterMaxCount) break;
                                    }
                                }
                            }
                        }
                    }

                    foreach (var item in filterList)
                    {
                        filterForm.ListBox.Items.Add(GetItemText(item));
                    }
                }
                else
                {
                    FillFilterTextEmpty();
                    filterSelectedItem = null;
                    filterSelectedValue = null;
                    SelectedValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        [DefaultValue(false)]
        [Description("过滤时删除字符串前面、后面的空格"), Category("SunnyUI")]
        public bool TrimFilter { get; set; }

        [DefaultValue(false)]
        [Description("过滤时忽略大小写"), Category("SunnyUI")]
        public bool FilterIgnoreCase { get; set; }

        private void FillFilterTextEmpty()
        {
            filterForm.ListBox.Items.Clear();
            filterList.Clear();

            if (DataSource == null)
            {
                foreach (var item in Items)
                {
                    filterList.Add(item.ToString());
                }
            }
            else
            {
                if (dataManager != null)
                {
                    for (int i = 0; i < Items.Count; i++)
                    {
                        filterList.Add(dataManager.List[i]);
                    }
                }
            }

            foreach (var item in filterList)
            {
                filterForm.ListBox.Items.Add(GetItemText(item));
            }
        }

        List<object> filterList = new List<object>();

        private void ListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!ShowFilter)
                SelectedValueChanged?.Invoke(this, e);
        }

        private void Box_ValueMemberChanged(object sender, EventArgs e)
        {
            ValueMemberChanged?.Invoke(this, e);
        }

        private void Box_DisplayMemberChanged(object sender, EventArgs e)
        {
            DisplayMemberChanged?.Invoke(this, e);
            SetDataConnection();
        }

        private void Box_DataSourceChanged(object sender, EventArgs e)
        {
            DataSourceChanged?.Invoke(this, e);
            SetDataConnection();
        }

        private bool SelectTextChange;

        private void Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectTextChange = true;
            if (!ShowFilter)
            {
                if (ListBox.SelectedItem != null)
                    Text = ListBox.GetItemText(ListBox.SelectedItem);
                else
                    Text = "";
            }

            SelectTextChange = false;

            if (!Wana_1)
                SelectedIndexChanged?.Invoke(this, e);
        }

        public event EventHandler SelectedIndexChanged;

        public event EventHandler DataSourceChanged;

        public event EventHandler DisplayMemberChanged;

        public event EventHandler ValueMemberChanged;

        public event EventHandler SelectedValueChanged;

        /// <summary>
        /// 值改变事件
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="value">值</param>
        protected override void ItemForm_ValueChanged(object sender, object value)
        {
            Invalidate();
        }

        private readonly UIComboBoxItem dropForm = new UIComboBoxItem();
        private readonly UIComboBoxItem filterForm = new UIComboBoxItem();

        private UIDropDown filterItemForm;

        private UIDropDown FilterItemForm
        {
            get
            {
                if (filterItemForm == null)
                {
                    filterItemForm = new UIDropDown(filterForm);

                    if (filterItemForm != null)
                    {
                        filterItemForm.VisibleChanged += FilterItemForm_VisibleChanged;
                    }
                }

                return filterItemForm;
            }
        }

        private void FilterItemForm_VisibleChanged(object sender, EventArgs e)
        {
            dropSymbol = SymbolNormal;
            if (filterItemForm.Visible)
            {
                dropSymbol = SymbolDropDown;
            }

            Invalidate();
        }

        /// <summary>
        /// 创建对象
        /// </summary>
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

        private UIListBox FilterListBox
        {
            get => filterForm.ListBox;
        }

        [DefaultValue(25)]
        [Description("列表项高度"), Category("SunnyUI")]
        public int ItemHeight
        {
            get => ListBox.ItemHeight;
            set => FilterListBox.ItemHeight = ListBox.ItemHeight = value;
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

            if (filterForm != null)
            {
                filterForm.ListBox.Font = Font;
            }
        }

        public void ShowDropDown()
        {
            UIComboBox_ButtonClick(this, EventArgs.Empty);
        }

        public void HideDropDown()
        {
            try
            {
                if (!ShowFilter)
                {
                    if (ItemForm != null && ItemForm.Visible)
                        ItemForm.Close();
                }
                else
                {
                    if (FilterItemForm != null && FilterItemForm.Visible)
                        FilterItemForm.Close();
                }
            }
            catch
            {
            }
        }

        [DefaultValue(false)]
        [Description("不显示过滤可以自动调整下拉框宽度"), Category("SunnyUI")]
        public bool DropDownAutoWidth { get; set; }

        private void UIComboBox_ButtonClick(object sender, EventArgs e)
        {
            if (NeedDrawClearButton)
            {
                NeedDrawClearButton = false;
                Text = "";

                if (!showFilter)
                {
                    while (dropForm.ListBox.SelectedIndex != -1)
                        dropForm.ListBox.SelectedIndex = -1;
                }
                else
                {
                    while (filterForm.ListBox.SelectedIndex != -1)
                        filterForm.ListBox.SelectedIndex = -1;
                }

                Invalidate();
                return;
            }

            if (!ShowFilter)
            {
                if (Items.Count > 0)
                {
                    int dropWidth = Width;

                    if (DropDownAutoWidth)
                    {
                        if (DataSource == null)
                        {
                            for (int i = 0; i < Items.Count; i++)
                            {
                                Size sf = TextRenderer.MeasureText(Items[i].ToString(), Font);
                                dropWidth = Math.Max((int)sf.Width + ScrollBarInfo.VerticalScrollBarWidth() + 6, dropWidth);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < Items.Count; i++)
                            {
                                Size sf = TextRenderer.MeasureText(dropForm.ListBox.GetItemText(Items[i]), Font);
                                dropWidth = Math.Max((int)sf.Width + ScrollBarInfo.VerticalScrollBarWidth() + 6, dropWidth);
                            }
                        }
                    }
                    else
                    {
                        dropWidth = Math.Max(DropDownWidth, dropWidth);
                    }

                    ItemForm.Show(this, new Size(dropWidth, CalcItemFormHeight()));
                }
            }
            else
            {
                if (FilterItemForm.Visible)
                {
                    FilterItemForm.Close();
                }
                else
                {
                    ShowDropDownFilter();
                }
            }
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            ListBox.SetStyleColor(uiColor.DropDownStyle);
            FilterListBox.SetStyleColor(uiColor.DropDownStyle);
        }

        public object DataSource
        {
            get => ListBox.DataSource;
            set
            {
                ListBox.DataSource = value;
                Box_DataSourceChanged(this, EventArgs.Empty);
            }
        }

        [DefaultValue(150)]
        [Description("下拉框宽度"), Category("SunnyUI")]
        public int DropDownWidth { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        [Description("列表项"), Category("SunnyUI")]
        public ListBox.ObjectCollection Items => ListBox.Items;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("选中索引"), Category("SunnyUI")]
        public int SelectedIndex
        {
            get => ShowFilter ? -1 : ListBox.SelectedIndex;
            set
            {
                if (!ShowFilter)
                {
                    if (DataSource != null && value == -1 && SelectedIndex > 0)
                    {
                        Wana_1 = true;
                        SelectedIndex = 0;
                        Wana_1 = false;
                    }

                    ListBox.SelectedIndex = value;
                }
            }
        }

        private bool Wana_1;

        [Browsable(false), Bindable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("选中项"), Category("SunnyUI")]
        public object SelectedItem
        {
            get => ShowFilter ? filterSelectedItem : ListBox.SelectedItem;
            set
            {
                if (!ShowFilter)
                {
                    ListBox.SelectedItem = value;
                }
            }
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
            get => ListBox.DisplayMember;
            set
            {
                ListBox.DisplayMember = value;
                Box_DisplayMemberChanged(this, EventArgs.Empty);
            }
        }

        [Description("获取或设置指示显示值的方式的格式说明符字符。"), Category("SunnyUI")]
        [DefaultValue("")]
        [MergableProperty(false)]
        public string FormatString
        {
            get => ListBox.FormatString;
            set => FilterListBox.FormatString = ListBox.FormatString = value;
        }

        [Description("获取或设置指示显示值是否可以进行格式化操作。"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool FormattingEnabled
        {
            get => ListBox.FormattingEnabled;
            set => FilterListBox.FormattingEnabled = ListBox.FormattingEnabled = value;
        }

        [Description("获取或设置要为此列表框实际值的属性。"), Category("SunnyUI")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string ValueMember
        {
            get => ListBox.ValueMember;
            set
            {
                ListBox.ValueMember = value;
                ValueMemberBindingMemberInfo = new BindingMemberInfo(value);
            }
        }

        BindingMemberInfo ValueMemberBindingMemberInfo;

        [
            DefaultValue(null),
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Bindable(true)
        ]
        public object SelectedValue
        {
            get => ShowFilter ? filterSelectedValue : ListBox.SelectedValue;
            set
            {
                if (!ShowFilter)
                    ListBox.SelectedValue = value;
            }
        }

        public string GetItemText(object item)
        {
            return ListBox.GetItemText(item);
        }

        private void UIComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !ShowFilter)
            {
                ShowDropDown();
            }
        }

        private void edit_Leave(object sender, EventArgs e)
        {
            HideDropDown();
        }

        [DefaultValue(typeof(Color), "White")]
        public Color ItemFillColor
        {
            get => ListBox.FillColor;
            set => FilterListBox.FillColor = ListBox.FillColor = value;
        }

        [DefaultValue(typeof(Color), "48, 48, 48")]
        public Color ItemForeColor
        {
            get => ListBox.ForeColor;
            set => FilterListBox.ForeColor = ListBox.ForeColor = value;
        }

        [DefaultValue(typeof(Color), "243, 249, 255")]
        public Color ItemSelectForeColor
        {
            get => ListBox.ItemSelectForeColor;
            set => FilterListBox.ItemSelectForeColor = ListBox.ItemSelectForeColor = value;
        }

        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color ItemSelectBackColor
        {
            get => ListBox.ItemSelectBackColor;
            set => FilterListBox.ItemSelectBackColor = ListBox.ItemSelectBackColor = value;
        }

        [DefaultValue(typeof(Color), "220, 236, 255")]
        public Color ItemHoverColor
        {
            get => ListBox.HoverColor;
            set => FilterListBox.HoverColor = ListBox.HoverColor = value;
        }

        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color ItemRectColor
        {
            get => ListBox.RectColor;
            set => ListBox.RectColor = value;
        }
    }
}