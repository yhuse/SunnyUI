/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2023 ShenYongHua(沈永华).
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
 * 2022-06-30: V3.2.0 设置条目状态前判断是否创建
 * 2022-11-21: V3.2.9 修复未显示时切换节点文本为空的问题
 * 2023-04-19: V3.3.5 设置选择项ForeColor
 * 2023-06-27: V3.3.9 内置条目关联值由Tag改为TagString
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
        /// <param name="sender">控件</param>
        /// <param name="index">索引</param>
        /// <param name="text">文字</param>
        /// <param name="isChecked">是否选中</param>
        public delegate void OnValueChanged(object sender, int index, string text, bool isChecked);

        /// <summary>
        /// 值切换事件
        /// </summary>
        public event OnValueChanged ValueChanged;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UICheckBoxGroup()
        {
            items.CountChange += Items_CountChange;
            StyleCustomModeChanged += UICheckBoxGroup_StyleCustomModeChanged;
        }

        private void UICheckBoxGroup_StyleCustomModeChanged(object sender, EventArgs e)
        {
            foreach (var item in boxes)
            {
                item.StyleCustomMode = styleCustomMode;
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);

            if (DefaultFontSize < 0)
            {
                foreach (var item in boxes)
                {
                    item.Font = Font;
                }
            }
        }

        private void Items_CountChange(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// 获取和设置条目值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 清除所有条目
        /// </summary>
        public void Clear()
        {
            Items.Clear();
            ClearBoxes();
            Invalidate();
        }

        /// <summary>
        /// 条目列表
        /// </summary>
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
                    box.TagString = i.ToString();
                    box.Style = Style;
                    //box.IsScaled = IsScaled;
                    box.ValueChanged += Box_ValueChanged;
                    box.Text = Items[i]?.ToString();
                    box.StyleCustomMode = StyleCustomMode;
                    box.ForeColor = ForeColor;
                    boxes.Add(box);
                }
            }
        }

        protected override void AfterSetForeColor(Color color)
        {
            base.AfterSetForeColor(color);
            foreach (var item in boxes)
            {
                item.ForeColor = color;
            }
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
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
                ValueChanged?.Invoke(this, checkBox.TagString.ToInt(), checkBox.Text, checkBox.Checked);
            }
        }

        /// <summary>
        /// 选中状态列表
        /// </summary>
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

        /// <summary>
        /// 设置条目状态
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="isChecked">是否选中</param>
        public void SetItemCheckState(int index, bool isChecked)
        {
            CreateBoxes();
            if (index >= 0 && index < boxes.Count)
            {
                boxes[index].Checked = isChecked;
            }
        }

        /// <summary>
        /// 获取条目状态
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>是否选中</returns>
        public bool GetItemCheckState(int index)
        {
            if (index >= 0 && index < items.Count)
                return boxes[index].Checked;

            return false;
        }

        /// <summary>
        /// 所有选中条目列表
        /// </summary>
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

        /// <summary>
        /// 显示列的个数
        /// </summary>
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

        /// <summary>
        /// 显示项的大小
        /// </summary>
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

        /// <summary>
        /// 显示项的起始位置
        /// </summary>
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


        private int columnInterval;

        /// <summary>
        /// 显示项列之间的间隔
        /// </summary>
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

        /// <summary>
        /// 显示项行之间的间隔
        /// </summary>
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