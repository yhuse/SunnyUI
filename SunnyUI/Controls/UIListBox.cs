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
 * 文件名称: UIListBox.cs
 * 文件说明: 列表框
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2020-05-21: V2.2.5 增加鼠标滑过高亮
 *                    开发日志：https://www.cnblogs.com/yhuse/p/12933885.html
 * 2021-06-03: V3.0.4 修改对象绑定的显示问题
 * 2021-07-29: V3.0.5 增加多选行
 * 2021-07-30: V3.0.5 选中项显示方角
 * 2021-08-04: V3.0.5 增加Items变更的事件
 * 2021-12-29: V3.0.9 增加修改文字颜色
 * 2022-02-23: V3.1.1 按键上下移动选择项目时，滚动条跟随
 * 2022-03-08: V3.1.1 修复在选中某一项后，清除选中项需要两次操作
 * 2022-03-19: V3.1.1 重构主题配色
 * 2022-05-15: V3.1.8 增加滚动条颜色设置
 * 2022-09-05: V3.2.3 修复Click，DoubleClick事件
 * 2022-11-03: V3.2.6 增加了可设置垂直滚动条宽度的属性
 * 2023-11-16: V3.5.2 重构主题
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using static System.Windows.Forms.ListBox;

namespace Sunny.UI
{
    [DefaultEvent("ItemClick")]
    [DefaultProperty("Items")]
    public sealed class UIListBox : UIPanel, IToolTip
    {
        private readonly ListBoxEx listbox = new ListBoxEx();
        private readonly UIScrollBar bar = new UIScrollBar();
        private readonly Timer timer;

        public UIListBox()
        {
            SetStyleFlags(true, false);
            ShowText = false;
            Padding = new Padding(2);

            bar.ValueChanged += Bar_ValueChanged;
            bar.Width = SystemInformation.VerticalScrollBarWidth + 2;
            bar.Parent = this;
            bar.Dock = DockStyle.None;
            bar.Visible = false;

            listbox.Parent = this;
            listbox.Dock = DockStyle.Fill;
            listbox.Show();
            listbox.Bar = bar;

            listbox.SelectedIndexChanged += Listbox_SelectedIndexChanged;
            listbox.SelectedValueChanged += Listbox_SelectedValueChanged;
            listbox.Click += Listbox_Click;
            listbox.MouseClick += Listbox_MouseClick;
            listbox.DoubleClick += Listbox_DoubleClick;
            listbox.BeforeDrawItem += Listbox_BeforeDrawItem;
            listbox.MouseDown += Listbox_MouseDown;
            listbox.MouseUp += Listbox_MouseUp;
            listbox.MouseMove += Listbox_MouseMove;
            listbox.DataSourceChanged += Listbox_DataSourceChanged;
            listbox.DisplayMemberChanged += Listbox_DisplayMemberChanged;
            listbox.ValueMemberChanged += Listbox_ValueMemberChanged;
            listbox.ItemsClear += Listbox_ItemsClear;
            listbox.ItemsAdd += Listbox_ItemsAdd;
            listbox.ItemsRemove += Listbox_ItemsRemove;
            listbox.ItemsInsert += Listbox_ItemsInsert;
            listbox.KeyPress += Listbox_KeyPress;
            listbox.KeyDown += Listbox_KeyDown;
            listbox.KeyUp += Listbox_KeyUp;
            listbox.MouseEnter += Listbox_MouseEnter;
            listbox.MouseLeave += Listbox_MouseLeave;
            listbox.DrawItem += Listbox_DrawItem;
            listbox.MouseDoubleClick += Listbox_MouseDoubleClick;

            timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public override void SetDPIScale()
        {
            base.SetDPIScale();
            listbox.SetDPIScale();
        }

        private int scrollBarWidth = 0;

        [DefaultValue(0), Category("SunnyUI"), Description("垂直滚动条宽度，最小为原生滚动条宽度")]
        public int ScrollBarWidth
        {
            get => scrollBarWidth;
            set
            {
                scrollBarWidth = value;
                SetScrollInfo();
            }
        }

        private int scrollBarHandleWidth = 6;

        [DefaultValue(6), Category("SunnyUI"), Description("垂直滚动条滑块宽度，最小为原生滚动条宽度")]
        public int ScrollBarHandleWidth
        {
            get => scrollBarHandleWidth;
            set
            {
                scrollBarHandleWidth = value;
                if (bar != null) bar.FillWidth = value;
            }
        }

        public new event MouseEventHandler MouseDoubleClick;

        private void Listbox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MouseDoubleClick?.Invoke(this, e);
        }

        private Color scrollBarColor = Color.FromArgb(80, 160, 255);

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("滚动条填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color ScrollBarColor
        {
            get => scrollBarColor;
            set
            {
                scrollBarColor = value;
                bar.HoverColor = bar.PressColor = bar.ForeColor = value;
                bar.Style = UIStyle.Custom;
                Invalidate();
            }
        }

        private Color scrollBarBackColor = Color.FromArgb(243, 249, 255);

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("滚动条背景颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "243, 249, 255")]
        public Color ScrollBarBackColor
        {
            get => scrollBarBackColor;
            set
            {
                scrollBarBackColor = value;
                bar.FillColor = value;
                bar.Style = UIStyle.Custom;
                Invalidate();
            }
        }

        /// <summary>
        /// 滚动条主题样式
        /// </summary>
        [DefaultValue(true), Description("滚动条主题样式"), Category("SunnyUI")]
        public bool ScrollBarStyleInherited
        {
            get => bar != null && bar.Style == UIStyle.Inherited;
            set
            {
                if (value)
                {
                    if (bar != null) bar.Style = UIStyle.Inherited;

                    scrollBarColor = UIStyles.Blue.ListBarForeColor;
                    scrollBarBackColor = UIStyles.Blue.ListBarFillColor;
                }
            }
        }

        private void Listbox_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItem?.Invoke(sender, e);
        }

        public event DrawItemEventHandler DrawItem;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            timer?.Stop();
            timer?.Dispose();

            bar?.Dispose();
            listbox?.Dispose();
        }

        public new event EventHandler MouseLeave;
        public new event EventHandler MouseEnter;
        public new event KeyPressEventHandler KeyPress;
        public new event KeyEventHandler KeyDown;
        public new event KeyEventHandler KeyUp;

        private void Listbox_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave?.Invoke(this, e);
        }

        private void Listbox_MouseEnter(object sender, EventArgs e)
        {
            MouseEnter?.Invoke(this, e);
        }

        private void Listbox_KeyUp(object sender, KeyEventArgs e)
        {
            KeyUp?.Invoke(this, e);
        }

        private void Listbox_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown?.Invoke(this, e);
        }

        private void Listbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress?.Invoke(this, e);
        }

        /// <summary>
        /// 需要额外设置ToolTip的控件
        /// </summary>
        /// <returns>控件</returns>
        public Control ExToolTipControl()
        {
            return listbox;
        }

        private void Listbox_MouseClick(object sender, MouseEventArgs e)
        {
            MouseClick?.Invoke(this, e);
        }

        public new event MouseEventHandler MouseClick;
        public event EventHandler ItemsClear;
        public event EventHandler ItemsAdd;
        public event EventHandler ItemsRemove;
        public event EventHandler ItemsInsert;

        public void BeginUpdate()
        {
            listbox.BeginUpdate();
        }

        public void EndUpdate()
        {
            listbox.EndUpdate();
        }

        public void ClearSelected()
        {
            listbox.SelectedIndex = -1;
            listbox.ClearSelected();
        }

        public int FindString(string s, int startIndex)
        {
            return listbox.FindString(s, startIndex);
        }

        public int FindStringExact(string s, int startIndex)
        {
            return listbox.FindStringExact(s, startIndex);
        }

        public Rectangle GetItemRectangle(int index)
        {
            return listbox.GetItemRectangle(index);
        }

        public bool GetSelected(int index)
        {
            return listbox.GetSelected(index);
        }

        public void SetSelected(int index, bool value)
        {
            listbox.SetSelected(index, value);
        }

        private void Listbox_ItemsInsert(object sender, EventArgs e)
        {
            ItemsInsert?.Invoke(this, e);
        }

        private void Listbox_ItemsRemove(object sender, EventArgs e)
        {
            ItemsRemove?.Invoke(this, e);
        }

        private void Listbox_ItemsAdd(object sender, EventArgs e)
        {
            ItemsAdd?.Invoke(this, e);
        }

        private void Listbox_ItemsClear(object sender, EventArgs e)
        {
            ItemsClear?.Invoke(this, e);
        }

        public int FindString(string s)
        {
            return listbox.FindString(s);
        }

        public int FindStringExact(string s)
        {
            return listbox.FindStringExact(s);
        }

        [Browsable(false)]
        public ListBox ListBox => listbox;

        public int IndexFromPoint(Point p)
        {
            return listbox.IndexFromPoint(p);
        }

        public int IndexFromPoint(int x, int y)
        {
            return listbox.IndexFromPoint(x, y);
        }

        [DefaultValue(StringAlignment.Near)]
        [Description("列表项高度"), Category("SunnyUI")]
        public new StringAlignment TextAlignment
        {
            get => listbox.TextAlignment;
            set => listbox.TextAlignment = value;
        }

        [DefaultValue(SelectionMode.One)]
        [Description("选择项所用方法"), Category("SunnyUI")]
        public SelectionMode SelectionMode
        {
            get => listbox.SelectionMode;
            set => listbox.SelectionMode = value;
        }

        [DefaultValue(SelectionMode.One)]
        [Description("选择项所用方法"), Category("SunnyUI")]
        public SelectedIndexCollection SelectedIndices
        {
            get => listbox.SelectedIndices;
        }

        [DefaultValue(false)]
        public bool Sorted
        {
            get => listbox.Sorted;
            set => listbox.Sorted = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TopIndex
        {
            get => listbox.TopIndex;
            set => listbox.TopIndex = value;
        }

        [DefaultValue(true)]
        public bool UseTabStops
        {
            get => listbox.UseTabStops;
            set => listbox.UseTabStops = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SelectedObjectCollection SelectedItems
        {
            get => listbox.SelectedItems;
        }

        protected override void OnContextMenuStripChanged(EventArgs e)
        {
            base.OnContextMenuStripChanged(e);
            if (listbox != null) listbox.ContextMenuStrip = ContextMenuStrip;
        }

        private void Listbox_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMove?.Invoke(this, e);
        }

        private void Listbox_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUp?.Invoke(this, e);
        }

        private void Listbox_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDown?.Invoke(this, e);
        }

        public new event MouseEventHandler MouseDown;
        public new event MouseEventHandler MouseUp;
        public new event MouseEventHandler MouseMove;

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Items.Count == 0 && LastCount != 0)
            {
                LastCount = 0;
                timer.Stop();
                ItemsCountChange?.Invoke(this, e);
                timer.Start();
            }
        }

        /// <summary>
        /// 重载字体变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (DefaultFontSize < 0 && listbox != null) listbox.Font = this.Font;
        }

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetScrollInfo();
        }

        private void SetScrollInfo()
        {
            bar.Top = 2;
            bar.Height = Height - 4;
            int barWidth = Math.Max(ScrollBarInfo.VerticalScrollBarWidth() + Padding.Right, ScrollBarWidth);
            bar.Width = barWidth + 1;
            bar.Left = Width - barWidth - 3;
        }

        private void Listbox_BeforeDrawItem(object sender, ObjectCollection items, DrawItemEventArgs e)
        {
            if (Items.Count != LastCount)
            {
                listbox.SetScrollInfo();
                LastCount = Items.Count;
                ItemsCountChange?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Listbox_DoubleClick(object sender, EventArgs e)
        {
            DoubleClick?.Invoke(this, e);
        }

        private void Listbox_Click(object sender, EventArgs e)
        {
            Click?.Invoke(this, e);
        }

        public new event EventHandler Click;

        public new event EventHandler DoubleClick;

        public event EventHandler ItemsCountChange;

        public event EventHandler SelectedIndexChanged;

        public event EventHandler SelectedValueChanged;

        private void Listbox_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedValueChanged?.Invoke(this, e);
            Text = listbox.SelectedItem?.ToString();
        }

        private void Listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged?.Invoke(this, e);
        }

        [DefaultValue(25)]
        [Description("列表项高度"), Category("SunnyUI")]
        public int ItemHeight
        {
            get => listbox.ItemHeight;
            set => listbox.ItemHeight = value;
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);

            if (bar != null && bar.Style == UIStyle.Inherited)
            {
                bar.ForeColor = uiColor.ListBarForeColor;
                bar.HoverColor = uiColor.ButtonFillHoverColor;
                bar.PressColor = uiColor.ButtonFillPressColor;
                bar.FillColor = uiColor.ListBarFillColor;

                scrollBarColor = uiColor.ListBarForeColor;
                scrollBarBackColor = uiColor.ListBarFillColor;
            }

            hoverColor = uiColor.ListItemHoverColor;
            if (listbox != null)
            {
                listbox.HoverColor = hoverColor;
                listbox.SetStyleColor(uiColor);
                listbox.BackColor = uiColor.ListBackColor;
                listbox.ForeColor = uiColor.ListForeColor;
            }

            fillColor = uiColor.ListBackColor;
        }

        protected override void AfterSetFillColor(Color color)
        {
            base.AfterSetFillColor(color);
            if (listbox != null)
            {
                listbox.BackColor = color;
            }
        }

        protected override void AfterSetForeColor(Color color)
        {
            base.AfterSetForeColor(color);
            if (listbox != null)
            {
                listbox.ForeColor = color;
            }
        }

        private int LastCount;

        private int lastBarValue = -1;

        private void Bar_ValueChanged(object sender, EventArgs e)
        {
            if (listbox != null)
            {
                if (bar.Value != lastBarValue)
                {
                    ScrollBarInfo.SetScrollValue(listbox.Handle, bar.Value);
                    lastBarValue = bar.Value;
                }
            }
        }

        protected override void OnRadiusChanged(int value)
        {
            base.OnRadiusChanged(value);
            Padding = new Padding(Math.Max(2, value / 2));
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor", typeof(UITypeEditor))]
        [MergableProperty(false)]
        [Description("列表项"), Category("SunnyUI")]
        public ObjectCollection Items => listbox.Items;

        [Browsable(false)]
        public int Count => Items.Count;

        public void SelectedFirst()
        {
            listbox.SelectedFirst();
        }

        [DefaultValue(typeof(Color), "80, 160, 255")]
        [Description("列表项选中背景颜色"), Category("SunnyUI")]
        public Color ItemSelectBackColor
        {
            get => listbox.ItemSelectBackColor;
            set => listbox.ItemSelectBackColor = value;
        }

        [DefaultValue(typeof(Color), "243, 249, 255")]
        [Description("列表项选中字体颜色"), Category("SunnyUI")]
        public Color ItemSelectForeColor
        {
            get => listbox.ItemSelectForeColor;
            set => listbox.ItemSelectForeColor = value;
        }

        [Browsable(false)]
        [DefaultValue(-1)]
        public int SelectedIndex
        {
            get => listbox.SelectedIndex;
            set
            {
                if (value >= 0)
                {
                    if (listbox != null && listbox.Items != null && listbox.Items.ContainsIndex(value))
                        listbox.SelectedIndex = value;
                }
                else
                {
                    listbox.SelectedIndex = value;
                }
            }
        }


        [Browsable(false)]
        [DefaultValue(null)]
        public object SelectedItem
        {
            get => listbox.SelectedItem;
            set => listbox.SelectedItem = value;
        }

        [Browsable(false)]
        [DefaultValue(null)]
        public object SelectedValue
        {
            get => listbox.SelectedValue;
            set => listbox.SelectedValue = value;
        }

        private Color hoverColor = Color.FromArgb(155, 200, 255);

        [DefaultValue(typeof(Color), "220, 236, 255")]
        [Description("列表项鼠标移上颜色"), Category("SunnyUI")]
        public Color HoverColor
        {
            get => hoverColor;
            set
            {
                hoverColor = value;
                listbox.HoverColor = hoverColor;
            }
        }

        [DefaultValue("")]
        [Description("指示要为此控件中的项显示的属性")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string DisplayMember
        {
            get => listbox.DisplayMember;
            set => listbox.DisplayMember = value;
        }

        [DefaultValue("")]
        [Description("指示用作控件中项的实际值的属性")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string ValueMember
        {
            get => listbox.ValueMember;
            set => listbox.ValueMember = value;
        }

        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [AttributeProvider(typeof(IListSource))]
        [Description("指示此控件将用来获取其项的列表")]
        public object DataSource
        {
            get => listbox.DataSource;
            set => listbox.DataSource = value;
        }

        private void Listbox_ValueMemberChanged(object sender, EventArgs e)
        {
            ValueMemberChanged?.Invoke(this, e);
        }

        private void Listbox_DisplayMemberChanged(object sender, EventArgs e)
        {
            DisplayMemberChanged?.Invoke(this, e);
        }

        private void Listbox_DataSourceChanged(object sender, EventArgs e)
        {
            DataSourceChanged?.Invoke(this, e);
        }

        public event EventHandler DataSourceChanged;

        public event EventHandler DisplayMemberChanged;

        public event EventHandler ValueMemberChanged;

        [DefaultValue("")]
        [Description("格式说明符，指示显示值的方式")]
        public string FormatString
        {
            get => listbox.FormatString;
            set => listbox.FormatString = value;
        }

        [Description("获取或设置指示显示值是否可以进行格式化操作。"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool FormattingEnabled
        {
            get => listbox.FormattingEnabled;
            set => listbox.FormattingEnabled = value;
        }

        public string GetItemText(object item)
        {
            return listbox.GetItemText(item);
        }

        public string GetItemText(int index)
        {
            if (index < 0 || index >= Items.Count) return string.Empty;
            return GetItemText(Items[index]);
        }
    }
}