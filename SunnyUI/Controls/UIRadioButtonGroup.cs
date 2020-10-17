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
 * 文件说明: 单选框组
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
    [DefaultProperty("Items")]
    [DefaultEvent("ValueChanged")]
    public class UIRadioButtonGroup : UIGroupBox
    {
        public delegate void OnValueChanged(object sender, int index, string text);

        public event OnValueChanged ValueChanged;

        ~UIRadioButtonGroup()
        {
            listbox?.Dispose();
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        [Description("列表项"), Category("SunnyUI")]
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
                        Tag = i,
                        Style = Style
                    };

                    button.ValueChanged += Button_ValueChanged;
                    buttons.Add(button);
                }
            }
        }

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
                SelectedIndex = button.Tag.ToString().ToInt();
            }
        }

        [Browsable(false)]
        [DefaultValue(-1)]
        public int SelectedIndex
        {
            get => ListBox.SelectedIndex;
            set
            {
                if (buttons.Count != Items.Count)
                {
                    CreateBoxes();
                }

                if (Items.Count == 0)
                {
                    ListBox.SelectedIndex = -1;
                    return;
                }

                if (SelectedIndex != value)
                {
                    if (value >= 0 && value < buttons.Count)
                    {
                        ListBox.SelectedIndex = value;
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

            ListBox.SelectedIndex = -1;
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

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            foreach (var button in buttons)
            {
                button.Font = Font;
            }
        }

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