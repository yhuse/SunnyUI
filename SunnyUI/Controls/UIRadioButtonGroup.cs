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
            StyleCustomModeChanged += UICheckBoxGroup_StyleCustomModeChanged;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);

            if (DefaultFontSize < 0)
            {
                foreach (var item in buttons)
                {
                    item.Font = Font;
                }
            }
        }

        private void UICheckBoxGroup_StyleCustomModeChanged(object sender, EventArgs e)
        {
            foreach (var item in buttons)
            {
                item.StyleCustomMode = styleCustomMode;
            }
        }

        private void Items_CountChange(object sender, EventArgs e)
        {
            Invalidate();
        }

        ~UIRadioButtonGroup()
        {
            ClearButtons();
        }

        private void ClearButtons()
        {
            foreach (var button in buttons)
            {
                button.Hide();
                button.Dispose();
            }

            buttons.Clear();
        }

        public void Clear()
        {
            Items.Clear();
            ClearButtons();
            SelectedIndex = -1;
            Invalidate();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        [Description("列表项"), Category("SunnyUI")]
        public UIObjectCollection Items => items;

        private readonly UIObjectCollection items = new UIObjectCollection();

        private void CreateBoxes()
        {
            if (Items.Count == 0) return;
            if (Items.Count != buttons.Count)
            {
                ClearButtons();

                for (int i = 0; i < Items.Count; i++)
                {
                    UIRadioButton button = new UIRadioButton
                    {
                        BackColor = Color.Transparent,
                        Font = Font,
                        Parent = this,
                        TagString = i.ToString(),
                        Style = Style,
                        Text = Items[i]?.ToString(),
                        StyleCustomMode = StyleCustomMode,
                        ForeColor = ForeColor
                    };

                    button.ValueChanged += Button_ValueChanged;
                    buttons.Add(button);
                }
            }
        }

        protected override void AfterSetForeColor(Color color)
        {
            base.AfterSetForeColor(color);
            foreach (var item in buttons)
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

            int startX = StartPos.X;
            int startY = TitleTop + StartPos.Y;
            for (int i = 0; i < Items.Count; i++)
            {
                buttons[i].Text = Items[i].ToString();

                int rowIndex = i / ColumnCount;
                int columnIndex = i % ColumnCount;

                buttons[i].Left = startX + ItemSize.Width * columnIndex + ColumnInterval * columnIndex;
                buttons[i].Top = startY + ItemSize.Height * rowIndex + RowInterval * rowIndex;
                buttons[i].Size = ItemSize;
                buttons[i].Show();
            }
        }

        private void Button_ValueChanged(object sender, bool value)
        {
            UIRadioButton button = (UIRadioButton)sender;
            if (value)
            {
                SelectedIndex = button.TagString.ToInt();
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
                if (buttons.Count != Items.Count)
                {
                    CreateBoxes();
                }

                if (Items.Count == 0)
                {
                    selectedIndex = -1;
                    return;
                }

                if (SelectedIndex != value)
                {
                    if (value >= 0 && value < buttons.Count)
                    {
                        selectedIndex = value;
                        buttons[value].Checked = true;
                        ValueChanged?.Invoke(this, value, buttons[value].Text);
                    }
                }
            }
        }

        public void SelectedNone()
        {
            foreach (var button in buttons)
            {
                button.Checked = false;
            }

            selectedIndex = -1;
        }

        private readonly List<UIRadioButton> buttons = new List<UIRadioButton>();

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

        private Size itemSize = new Size(150, 35);

        [DefaultValue(typeof(Size), "150, 35")]
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

        public int columnInterval;

        [DefaultValue(0)]
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

        private int rowInterval;

        [DefaultValue(0)]
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