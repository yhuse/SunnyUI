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
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultProperty("Items")]
    [DefaultEvent("SelectedIndexChanged")]
    [ToolboxItem(true)]
    public sealed partial class UIComboBox : UIDropControl
    {
        public UIComboBox()
        {
            InitializeComponent();
        }

        public event EventHandler SelectedIndexChanged;

        protected override void ItemForm_ValueChanged(object sender, object value)
        {
            selectedItem = ListBox.SelectedItem;
            selectedIndex = ListBox.SelectedIndex;
            Text = ListBox.Text;
            Invalidate();
            SelectedIndexChanged?.Invoke(this, null);
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        public ListBox.ObjectCollection Items => ListBox?.Items;

        [DefaultValue(25)]
        public int ItemHeight
        {
            get => ListBox.ItemHeight;
            set => ListBox.ItemHeight = value;
        }

        [DefaultValue(8)]
        public int MaxDropDownItems { get; set; } = 8;

        private int selectedIndex = -1;

        [Browsable(false)]
        [DefaultValue(-1)]
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                if (value.InRange(-1, ListBox.Items.Count - 1))
                {
                    ListBox.SelectedIndex = value;
                    selectedIndex = value;

                    if (value >= 0)
                    {
                        Text = ListBox.Items[value].ToString();
                    }
                }
            }
        }

        private object selectedItem;

        [Browsable(false)]
        [DefaultValue(null)]
        public object SelectedItem
        {
            get => selectedItem;
            set
            {
                if (value != null)
                {
                    int idx = ListBox.Items.IndexOf(value);
                    SelectedIndex = idx;
                    selectedItem = idx >= 0 ? value : null;
                }
            }
        }

        private void UIComboBox_ButtonClick(object sender, EventArgs e)
        {
            if (Items.Count == 0 || ItemForm.Visible)
            {
                return;
            }

            ItemForm.Show(this, new Size(Width, CalcItemFormHeight()));
        }

        private void UIComboBox_FontChanged(object sender, EventArgs e)
        {
            if (ItemForm != null)
            {
                ListBox.Font = Font;
            }
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            ListBox.SetStyleColor(uiColor);
        }
    }
}