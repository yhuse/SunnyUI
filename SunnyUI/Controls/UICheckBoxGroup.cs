/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2022 ShenYongHua(沈永华).
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
 * 文件名称: UICheckBoxGroup.cs
 * 文件说明: 多选框组
 * 当前版本: V3.1
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

        public UICheckBoxGroup()
        {
            items.CountChange += Items_CountChange;
        }

        private void Items_CountChange(object sender, EventArgs e)
        {
            Invalidate();
        }

        public bool this[int index]
        {
            get => GetItemCheckState(index);
            set => SetItemCheckState(index, value);
        }

        /// <summary>
        /// 析构事件
        /// </summary>
        ~UICheckBoxGroup()
        {
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

        public void Clear()
        {
            Items.Clear();
            ClearBoxes();
            Invalidate();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        [Description("获取该多选框组中项的集合"), Category("SunnyUI")]
        public UIObjectCollection Items => items;

        private readonly UIObjectCollection items = new UIObjectCollection();

        private void CreateBoxes()
        {
            if (Items.Count != boxes.Count)
            {
                ClearBoxes();

                for (int i = 0; i < Items.Count; i++)
                {
                    UICheckBox box = new UICheckBox();
                    box.BackColor = Color.Transparent;
                    box.Font = Font;
                    box.Parent = this;
                    box.Tag = i;
                    box.Style = Style;
                    box.IsScaled = IsScaled;
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

        public void SetItemCheckState(int index, bool isChecked)
        {
            if (index >= 0 && index < boxes.Count)
            {
                boxes[index].Checked = isChecked;
            }
        }

        public bool GetItemCheckState(int index)
        {
            if (index >= 0 && index < items.Count)
                return boxes[index].Checked;

            return false;
        }

        [Browsable(false)]
        public List<object> SelectedItems
        {
            get
            {
                List<object> objects = new List<object>();

                for (int i = 0; i < boxes.Count; i++)
                {
                    if (boxes[i].Checked)
                        objects.Add(Items[i]);
                }

                return objects;
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
    }
}