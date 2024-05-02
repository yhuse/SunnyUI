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
 * 文件名称: UIRadioButtonGroup.cs
 * 文件说明: 单选框组
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-04-19: V2.2.3 增加单元
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2020-07-03: V2.2.6 修正调整ItemSize无效的Bug
 * 2020-07-04: V2.2.6 可以设置初始选中值
 * 2022-11-21: V3.2.9 修复未显示时切换节点文本为空的问题
 * 2023-04-22: V3.3.5 设置选择项ForeColor
 * 2023-06-27: V3.3.9 内置条目关联值由Tag改为TagString
 * 2023-11-09: V3.5.2 重写UIRadioButtonGroup
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
    [DefaultProperty("Items")]
    [DefaultEvent("ValueChanged")]
    public class UIRadioButtonGroup : UIGroupBox
    {
        public delegate void OnValueChanged(object sender, int index, string text);

        public event OnValueChanged ValueChanged;

        public UIRadioButtonGroup()
        {
            items.CountChange += Items_CountChange;
            ForeColor = UIStyles.Blue.CheckBoxForeColor;
            checkBoxColor = UIStyles.Blue.CheckBoxColor;
            hoverColor = UIStyles.Blue.ListItemHoverColor;
        }

        private Color checkBoxColor;
        private Color hoverColor;

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

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color RadioButtonColor
        {
            get => checkBoxColor;
            set
            {
                checkBoxColor = value;
                Invalidate();
            }
        }

        private void Items_CountChange(object sender, EventArgs e)
        {
            Invalidate();
        }

        public void Clear()
        {
            Items.Clear();
            SelectedIndex = -1;
            Invalidate();
            ValueChanged(this, -1, "");
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        [Description("列表项"), Category("SunnyUI")]
        public UIObjectCollection Items => items;

        private readonly UIObjectCollection items = new UIObjectCollection();

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

                int ImageSize = RadioButtonSize;

                //图标
                top = rect.Top + (rect.Height - ImageSize) / 2;
                left = rect.Left + 6;
                Color color = Enabled ? checkBoxColor : foreDisableColor;

                if (SelectedIndex == i)
                {
                    e.Graphics.FillEllipse(color, left, top, ImageSize, ImageSize);
                    float pointSize = ImageSize - 4;
                    e.Graphics.FillEllipse(BackColor.IsValid() ? BackColor : Color.White,
                        left + ImageSize / 2.0f - pointSize / 2.0f,
                        top + ImageSize / 2.0f - pointSize / 2.0f,
                        pointSize, pointSize);

                    pointSize = ImageSize - 8;
                    e.Graphics.FillEllipse(color,
                        left + ImageSize / 2.0f - pointSize / 2.0f,
                        top + ImageSize / 2.0f - pointSize / 2.0f,
                        pointSize, pointSize);
                }
                else
                {
                    using Pen pn = new Pen(color, 2);
                    e.Graphics.SetHighQuality();
                    e.Graphics.DrawEllipse(pn, left + 1, top + 1, ImageSize - 2, ImageSize - 2);
                    e.Graphics.SetDefaultQuality();
                }

                e.Graphics.DrawString(text, Font, ForeColor, rect, ContentAlignment.MiddleLeft, ImageSize + 10, 0);

            }
        }

        private Dictionary<int, bool> CheckStates = new Dictionary<int, bool>();
        private Dictionary<int, Rectangle> CheckBoxRects = new Dictionary<int, Rectangle>();

        int activeIndex = -1;
        private int _imageSize = 16;

        [DefaultValue(16)]
        [Description("图标大小"), Category("SunnyUI")]
        [Browsable(true)]
        public int RadioButtonSize
        {
            get => _imageSize;
            set
            {
                _imageSize = Math.Max(value, 16);
                _imageSize = Math.Min(value, 64);
                Invalidate();
            }
        }

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
                    SelectedIndex = pair.Key;
                    Invalidate();
                }
            }
        }

        private int selectedIndex = -1;

        [Browsable(false)]
        [DefaultValue(-1)]
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                if (Items.Count == 0)
                {
                    selectedIndex = -1;
                    return;
                }

                if (SelectedIndex != value)
                {
                    selectedIndex = value;
                    Invalidate();
                    ValueChanged?.Invoke(this, value, items.ContainsIndex(value) ? items[value].ToString() : "");
                }
            }
        }

        private int columnCount = 1;

        [DefaultValue(1)]
        [Description("显示列个数"), Category("SunnyUI")]
        public int ColumnCount
        {
            get => columnCount;
            set
            {
                columnCount = value;
                Invalidate();
            }
        }

        private Size itemSize = new Size(150, 29);

        [DefaultValue(typeof(Size), "150, 29")]
        [Description("列表项大小"), Category("SunnyUI")]
        public Size ItemSize
        {
            get => itemSize;
            set
            {
                itemSize = value;
                Invalidate();
            }
        }

        private Point startPos = new Point(12, 12);

        [DefaultValue(typeof(Point), "12, 12")]
        [Description("列表项起始位置"), Category("SunnyUI")]
        public Point StartPos
        {
            get => startPos;
            set
            {
                startPos = value;
                Invalidate();
            }
        }

        public int columnInterval = 6;

        [DefaultValue(6)]
        [Description("显示列间隔"), Category("SunnyUI")]
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

        [DefaultValue(2)]
        [Description("显示行间隔"), Category("SunnyUI")]
        public int RowInterval
        {
            get => rowInterval;
            set
            {
                rowInterval = value;
                Invalidate();
            }
        }
    }
}