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
 * 文件名称: UICheckBoxGroup.cs
 * 文件说明: 多选框组
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-04-19: V2.2.3 增加单元
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2020-07-03: V2.2.6 修正调整ItemSize无效的Bug
 * 2020-07-04: V2.2.6 可以设置初始选中值
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
    /// 多选框组
    /// </summary>
    [DefaultProperty("Items")]
    [DefaultEvent("ValueChanged")]
    public class UICheckBoxGroup : UIGroupBox
    {
        /// <summary>
        /// 值切换事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="index">索引</param>
        /// <param name="text">文字</param>
        /// <param name="isChecked">是否选中</param>
        public delegate void OnValueChanged(object sender, int index, string text, bool isChecked);

        /// <summary>
        /// 值切换事件
        /// </summary>
        public event OnValueChanged ValueChanged;

        /// <summary>
        /// 析构事件
        /// </summary>
        ~UICheckBoxGroup()
        {
            listbox?.Dispose();
            ClearBoxes();
        }

        private void ClearBoxes()
        {
            foreach (var box in boxes)
            {
                box.Hide();
                box.Dispose();
            }

            boxes.Clear();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        [Description("获取该多选框组中项的集合"), Category("SunnyUI")]
        public ListBox.ObjectCollection Items => ListBox.Items;

        private CheckedListBoxEx listbox;

        private CheckedListBoxEx ListBox
        {
            get
            {
                if (listbox == null)
                {
                    listbox = new CheckedListBoxEx();
                    listbox.OnItemsCountChange += Listbox_OnItemsCountChange;
                }

                return listbox;
            }
        }

        private void Listbox_OnItemsCountChange(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void CreateBoxes()
        {
            if (Items.Count == 0) return;
            if (Items.Count != boxes.Count)
            {
                ClearBoxes();

                for (int i = 0; i < Items.Count; i++)
                {
                    UICheckBox box = new UICheckBox
                    {
                        BackColor = Color.Transparent,
                        Font = Font,
                        Parent = this,
                        Tag = i,
                        Style = Style
                    };

                    box.ValueChanged += Box_ValueChanged;
                    boxes.Add(box);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            CreateBoxes();

            if (Items.Count == 0) return;
            int startX = StartPos.X;
            int startY = TitleTop + StartPos.Y;
            for (int i = 0; i < Items.Count; i++)
            {
                boxes[i].Text = Items[i].ToString();

                int rowIndex = i / ColumnCount;
                int columnIndex = i % ColumnCount;

                boxes[i].Left = startX + ItemSize.Width * columnIndex + ColumnInterval * columnIndex;
                boxes[i].Top = startY + ItemSize.Height * rowIndex + RowInterval * rowIndex;
                boxes[i].Size = ItemSize;
                boxes[i].Show();
            }
        }

        private void Box_ValueChanged(object sender, bool value)
        {
            UICheckBox checkBox = (UICheckBox)sender;

            if (!multiChange)
            {
                ValueChanged?.Invoke(this, checkBox.Tag.ToString().ToInt(), checkBox.Text, checkBox.Checked);
            }
        }

        [Browsable(false)]
        public List<int> SelectedIndexes
        {
            get
            {
                List<int> indexes = new List<int>();

                for (int i = 0; i < boxes.Count; i++)
                {
                    if (boxes[i].Checked)
                        indexes.Add(i);
                }

                return indexes;
            }
            set
            {
                if (boxes.Count != Items.Count)
                {
                    CreateBoxes();
                }

                foreach (int i in value)
                {
                    if (i >= 0 && i < boxes.Count)
                    {
                        boxes[i].Checked = true;
                    }
                }
            }
        }

        //[Browsable(false)]
        //public List<string> SelectedItems
        //{
        //    get
        //    {
        //        List<string> items = new List<string>();

        //        foreach (var checkBox in boxes)
        //        {
        //            if (checkBox.Checked)
        //                items.Add(checkBox.Text);
        //        }

        //        return items;
        //    }
        //}

        [Browsable(false)]
        public List<object> SelectedItems
        {
            get
            {
                List<object> items = new List<object>();

                for (int i = 0; i < boxes.Count; i++)
                {
                    if (boxes[i].Checked)
                        items.Add(Items[i]);
                }

                return items;
            }
        }

        private readonly List<UICheckBox> boxes = new List<UICheckBox>();

        private int columnCount = 1;

        [DefaultValue(1)]
        [Description("显示列的个数"), Category("SunnyUI")]
        public int ColumnCount
        {
            get => columnCount;
            set
            {
                columnCount = value;
                Invalidate();
            }
        }

        private Size _itemSize = new Size(150, 35);

        [DefaultValue(typeof(Size), "150, 35")]
        [Description("显示项的大小"), Category("SunnyUI")]
        public Size ItemSize
        {
            get => _itemSize;
            set
            {
                _itemSize = value;
                Invalidate();
            }
        }

        private Point startPos = new Point(12, 12);

        [DefaultValue(typeof(Point), "12, 12")]
        [Description("显示项的起始位置"), Category("SunnyUI")]
        public Point StartPos
        {
            get => startPos;
            set
            {
                startPos = value;
                Invalidate();
            }
        }

        public int columnInterval;

        [DefaultValue(0)]
        [Description("显示项列之间的间隔"), Category("SunnyUI")]
        public int ColumnInterval
        {
            get => columnInterval;
            set
            {
                columnInterval = value;
                Invalidate();
            }
        }

        private int rowInterval;

        [DefaultValue(0)]
        [Description("显示项行之间的间隔"), Category("SunnyUI")]
        public int RowInterval
        {
            get => rowInterval;
            set
            {
                rowInterval = value;
                Invalidate();
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            foreach (var box in boxes)
            {
                box.Font = Font;
            }
        }

        /// <summary>
        /// 全部选择
        /// </summary>
        public void SelectAll()
        {
            multiChange = true;
            foreach (var box in boxes)
            {
                box.Checked = true;
            }

            multiChange = false;
        }

        /// <summary>
        /// 全部不选
        /// </summary>
        public void UnSelectAll()
        {
            multiChange = true;
            foreach (var box in boxes)
            {
                box.Checked = false;
            }

            multiChange = false;
        }

        /// <summary>
        /// 反转选择
        /// </summary>
        public void ReverseSelected()
        {
            multiChange = true;
            foreach (var box in boxes)
            {
                box.Checked = !box.Checked;
            }

            multiChange = false;
        }

        private bool multiChange;

        [ToolboxItem(false)]
        internal class CheckedListBoxEx : CheckedListBox
        {
            public event EventHandler OnItemsCountChange;

            private int count = -1;

            protected override void OnDrawItem(DrawItemEventArgs e)
            {
                base.OnDrawItem(e);

                if (count != Items.Count)
                {
                    OnItemsCountChange?.Invoke(this, e);
                    count = Items.Count;
                }
            }
        }
    }
}