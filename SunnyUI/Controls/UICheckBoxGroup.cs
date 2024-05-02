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
 * 2023-11-07: V3.5.2 重写UICheckBoxGroup
 * 2023-12-04: V3.6.1 增加属性可修改图标大小
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
        public delegate void OnValueChanged(object sender, CheckBoxGroupEventArgs e);

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
            ForeColor = UIStyles.Blue.CheckBoxForeColor;
            checkBoxColor = UIStyles.Blue.CheckBoxColor;
            hoverColor = UIStyles.Blue.ListItemHoverColor;
        }

        private Color checkBoxColor;

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
            set
            {
                SetItemCheckState(index, value);
                ValueChanged?.Invoke(this, new CheckBoxGroupEventArgs(index, Items[index].ToString(), this[index], SelectedIndexes.ToArray()));
                Invalidate();
            }
        }

        private Dictionary<int, bool> CheckStates = new Dictionary<int, bool>();
        private Dictionary<int, Rectangle> CheckBoxRects = new Dictionary<int, Rectangle>();
        private int _imageSize = 16;

        [DefaultValue(16)]
        [Description("图标大小"), Category("SunnyUI")]
        [Browsable(true)]
        public int CheckBoxSize
        {
            get => _imageSize;
            set
            {
                _imageSize = Math.Max(value, 16);
                _imageSize = Math.Min(value, 64);
                Invalidate();
            }
        }

        /// <summary>
        /// 清除所有条目
        /// </summary>
        public void Clear()
        {
            Items.Clear();
            CheckStates.Clear();
            CheckBoxRects.Clear();
            Invalidate();
            ValueChanged?.Invoke(this, new CheckBoxGroupEventArgs(-1, "", false, SelectedIndexes.ToArray()));
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

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            checkBoxColor = uiColor.CheckBoxColor;
            ForeColor = uiColor.CheckBoxForeColor;
            hoverColor = uiColor.ListItemHoverColor;
        }

        private Color hoverColor;

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("鼠标滑过填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "155, 200, 255")]
        public Color HoverColor
        {
            get => hoverColor;
            set
            {
                hoverColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color CheckBoxColor
        {
            get => checkBoxColor;
            set
            {
                checkBoxColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Items.Count == 0) return;

            if (activeIndex >= 0 && CheckBoxRects.ContainsKey(activeIndex))
            {
                e.Graphics.FillRectangle(hoverColor, CheckBoxRects[activeIndex]);
            }

            int startX = StartPos.X;
            int startY = TitleTop + StartPos.Y;

            for (int i = 0; i < Items.Count; i++)
            {
                string text = Items[i].ToString();
                int rowIndex = i / ColumnCount;
                int columnIndex = i % ColumnCount;
                int left = startX + ItemSize.Width * columnIndex + ColumnInterval * columnIndex;
                int top = startY + ItemSize.Height * rowIndex + RowInterval * rowIndex;
                Rectangle rect = new Rectangle(left, top, ItemSize.Width, ItemSize.Height);
                if (CheckBoxRects.NotContainsKey(i))
                    CheckBoxRects.Add(i, rect);
                else
                    CheckBoxRects[i] = rect;

                int ImageSize = CheckBoxSize;

                //图标
                top = rect.Top + (rect.Height - ImageSize) / 2;
                left = rect.Left + 6;
                Color color = Enabled ? checkBoxColor : foreDisableColor;

                if (this[i])
                {
                    e.Graphics.FillRoundRectangle(color, new Rectangle((int)left, (int)top, ImageSize, ImageSize), 1);
                    color = BackColor.IsValid() ? BackColor : Color.White;
                    Point pt2 = new Point((int)(left + ImageSize * 2 / 5.0f), (int)(top + ImageSize * 3 / 4.0f) - (ImageSize.Div(10)));
                    Point pt1 = new Point((int)left + 2 + ImageSize.Div(10), pt2.Y - (pt2.X - 2 - ImageSize.Div(10) - (int)left));
                    Point pt3 = new Point((int)left + ImageSize - 2 - ImageSize.Div(10), pt2.Y - (ImageSize - pt2.X - 2 - ImageSize.Div(10)) - (int)left);

                    PointF[] CheckMarkLine = { pt1, pt2, pt3 };
                    using Pen pn = new Pen(color, 2);
                    e.Graphics.SetHighQuality();
                    e.Graphics.DrawLines(pn, CheckMarkLine);
                    e.Graphics.SetDefaultQuality();
                }
                else
                {
                    using Pen pn = new Pen(color, 1);
                    e.Graphics.DrawRoundRectangle(pn, new Rectangle((int)left + 1, (int)top + 1, ImageSize - 2, ImageSize - 2), 1);
                    e.Graphics.DrawRectangle(pn, new Rectangle((int)left + 2, (int)top + 2, ImageSize - 4, ImageSize - 4));
                }

                e.Graphics.DrawString(text, Font, ForeColor, rect, ContentAlignment.MiddleLeft, ImageSize + 10, 0);

            }
        }

        int activeIndex = -1;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            int index = -1;
            foreach (var item in CheckBoxRects)
            {
                if (e.Location.InRect(item.Value))
                {
                    index = item.Key;
                    break;
                }
            }

            if (activeIndex != index)
            {
                activeIndex = index;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            activeIndex = -1;
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            foreach (var pair in CheckBoxRects)
            {
                if (e.Location.InRect(pair.Value) && pair.Key >= 0 && pair.Key < items.Count)
                {
                    this[pair.Key] = !this[pair.Key];
                    Invalidate();
                }
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
                for (int i = 0; i < Items.Count; i++)
                {
                    if (this[i]) indexes.Add(i);
                }

                return indexes;
            }
            set
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    SetItemCheckState(i, false);
                }

                foreach (int i in value)
                {
                    if (i >= 0 && i < Items.Count)
                    {
                        SetItemCheckState(i, true);
                    }
                }

                ValueChanged?.Invoke(this, new CheckBoxGroupEventArgs(-1, "", false, SelectedIndexes.ToArray()));
                Invalidate();
            }
        }

        /// <summary>
        /// 设置条目状态
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="isChecked">是否选中</param>
        private void SetItemCheckState(int index, bool isChecked)
        {
            if (index >= 0 && index < Items.Count && CheckStates.NotContainsKey(index))
            {
                CheckStates.Add(index, isChecked);
            }

            CheckStates[index] = isChecked;
        }

        /// <summary>
        /// 获取条目状态
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>是否选中</returns>
        private bool GetItemCheckState(int index)
        {
            if (index >= 0 && index < items.Count && CheckStates.ContainsKey(index))
                return CheckStates[index];

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

                for (int i = 0; i < Items.Count; i++)
                {
                    if (this[i]) objects.Add(Items[i]);
                }

                return objects;
            }
        }

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

        private Size _itemSize = new Size(150, 29);

        /// <summary>
        /// 显示项的大小
        /// </summary>
        [DefaultValue(typeof(Size), "150, 29")]
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


        private int columnInterval = 6;

        /// <summary>
        /// 显示项列之间的间隔
        /// </summary>
        [DefaultValue(6)]
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

        private int rowInterval = 2;

        /// <summary>
        /// 显示项行之间的间隔
        /// </summary>
        [DefaultValue(2)]
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
            for (int i = 0; i < Items.Count; i++)
            {
                SetItemCheckState(i, true);
            }

            ValueChanged?.Invoke(this, new CheckBoxGroupEventArgs(-1, "", false, SelectedIndexes.ToArray()));
            Invalidate();
        }

        /// <summary>
        /// 全部不选
        /// </summary>
        public void UnSelectAll()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                SetItemCheckState(i, false);
            }

            ValueChanged?.Invoke(this, new CheckBoxGroupEventArgs(-1, "", false, SelectedIndexes.ToArray()));
            Invalidate();
        }

        /// <summary>
        /// 反转选择
        /// </summary>
        public void ReverseSelected()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                SetItemCheckState(i, !this[i]);
            }

            ValueChanged?.Invoke(this, new CheckBoxGroupEventArgs(-1, "", false, SelectedIndexes.ToArray()));
            Invalidate();
        }
    }

    public class CheckBoxGroupEventArgs : EventArgs
    {
        public int Index { get; set; }

        public string Text { get; set; }
        public bool Checked { get; set; }

        public int[] SelectedIndexes { get; set; }

        public CheckBoxGroupEventArgs(int index, string text, bool isChecked, int[] indexes)
        {
            Index = index;
            Text = text;
            Checked = isChecked;
            SelectedIndexes = indexes;
        }
    }
}