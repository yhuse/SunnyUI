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
            bar.Style = UIStyle.Custom;
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

            timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Start();
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
            listbox.ContextMenuStrip = ContextMenuStrip;
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

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            listbox.IsScaled = true;
            listbox.Font = Font;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            bar.Top = 2;
            bar.Height = Height - 4;
            bar.Left = Width - bar.Width - 2;
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
            if (SelectedItem != null)
                ItemDoubleClick?.Invoke(this, e);
        }

        private void Listbox_Click(object sender, EventArgs e)
        {
            if (SelectedItem != null)
                ItemClick?.Invoke(this, e);
        }

        public event EventHandler ItemClick;

        public event EventHandler ItemDoubleClick;

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

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (bar != null)
            {
                bar.ForeColor = uiColor.ListBarForeColor;
                bar.HoverColor = uiColor.ButtonFillHoverColor;
                bar.PressColor = uiColor.ButtonFillPressColor;
                bar.FillColor = uiColor.ListBarFillColor;
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
            set => listbox.SelectedIndex = value;
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
                _style = UIStyle.Custom;
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

    /// <summary>
    /// ListBox
    /// </summary>
    [ToolboxItem(false)]
    public sealed class ListBoxEx : ListBox
    {
        private UIScrollBar bar;

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

        [Browsable(false), DefaultValue(false)]
        public bool IsScaled { get; set; }

        public void SetDPIScale()
        {
            if (!IsScaled)
            {
                this.SetDPIScaleFont();
                IsScaled = true;
            }
        }

        public UIScrollBar Bar
        {
            get => bar;
            set
            {
                bar = value;
                SetScrollInfo();
            }
        }

        public int Count => Items.Count;

        public ListBoxEx()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            UpdateStyles();
            this.DoubleBuffered();

            BorderStyle = BorderStyle.None;
            ForeColor = UIFontColor.Primary;
            IntegralHeight = false;
            Version = UIGlobal.Version;
            SetScrollInfo();
        }

        public event EventHandler ItemsClear;
        public event EventHandler ItemsAdd;
        public event EventHandler ItemsRemove;
        public event EventHandler ItemsInsert;

        protected override void WndProc(ref Message m)
        {
            if (IsDisposed || Disposing) return;
            if (IsHandleCreated)
            {
                const int LB_ADDSTRING = 0x0180;
                const int LB_INSERTSTRING = 0x0181;
                const int LB_DELETESTRING = 0x0182;
                const int LB_RESETCONTENT = 0x0184;
                if (m.Msg == LB_RESETCONTENT)
                {
                    ItemsClear?.Invoke(this, EventArgs.Empty);
                }

                if (m.Msg == LB_DELETESTRING)
                {
                    ItemsRemove?.Invoke(this, EventArgs.Empty);
                }

                if (m.Msg == LB_ADDSTRING)
                {
                    ItemsAdd?.Invoke(this, EventArgs.Empty);
                }

                if (m.Msg == LB_INSERTSTRING)
                {
                    ItemsInsert?.Invoke(this, EventArgs.Empty);
                }

                //if (m.Msg == Win32.User.WM_ERASEBKGND)
                //{
                //    m.Result = IntPtr.Zero;
                //    return;
                //}
            }

            base.WndProc(ref m);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (Bar != null && Bar.Visible)
            {
                if (Bar.Value != 0)
                {
                    ScrollBarInfo.SetScrollValue(Handle, Bar.Value);
                }
            }
            //SetScrollInfo();
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            SetScrollInfo();
        }

        public void SetScrollInfo()
        {
            if (Bar == null)
            {
                return;
            }

            var si = ScrollBarInfo.GetInfo(Handle);
            if (si.ScrollMax > 0)
            {
                Bar.Maximum = si.ScrollMax;
                Bar.Visible = si.ScrollMax > 0 && si.nMax > 0 && si.nPage > 0;
                Bar.Value = si.nPos;
            }
            else
            {
                Bar.Visible = false;
            }
        }

        public string Version { get; }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        #region 组件设计器生成的代码

        /// <summary>
        ///     设计器支持所需的方法 - 不要
        ///     使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            SuspendLayout();
            //
            // UIListBox
            //
            BorderStyle = BorderStyle.FixedSingle;
            DrawMode = DrawMode.OwnerDrawFixed;
            Font = UIFontColor.Font();
            IntegralHeight = false;
            ItemHeight = 25;
            Size = new Size(150, 200);
            ResumeLayout(false);
        }

        #endregion 组件设计器生成的代码

        private UIStyle _style = UIStyle.Blue;
        private Color _itemSelectBackColor = UIColor.Blue;
        private Color _itemSelectForeColor = Color.White;

        /// <summary>
        /// 主题样式
        /// </summary>
        [DefaultValue(UIStyle.Blue), Description("主题样式"), Category("SunnyUI")]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (Bar != null && Bar.Visible)
            {
                var si = ScrollBarInfo.GetInfo(Handle);
                int temp = Math.Abs(e.Delta / 120);
                if (e.Delta > 10)
                {
                    int nposnum = si.nPos - temp * SystemInformation.MouseWheelScrollLines;
                    ScrollBarInfo.SetScrollValue(Handle, nposnum >= si.nMin ? nposnum : 0);
                }
                else if (e.Delta < -10)
                {
                    int nposnum = si.nPos + temp * SystemInformation.MouseWheelScrollLines;
                    ScrollBarInfo.SetScrollValue(Handle, nposnum <= si.ScrollMax ? nposnum : si.ScrollMax);
                }
                SetScrollInfo();
            }
        }

        public void SetStyle(UIStyle style)
        {
            if (!style.IsCustom())
            {
                SetStyleColor(style.Colors());
                Invalidate();
            }

            _style = style;
        }

        public void SetStyleColor(UIBaseStyle uiColor)
        {
            ItemSelectBackColor = uiColor.ListItemSelectBackColor;
            ItemSelectForeColor = uiColor.ListItemSelectForeColor;
        }

        [Category("SunnyUI"), Description("The border color used to paint the control.")]
        public Color ItemSelectBackColor
        {
            get => _itemSelectBackColor;
            set
            {
                if (_itemSelectBackColor != value)
                {
                    _itemSelectBackColor = value;
                    _style = UIStyle.Custom;
                    if (DesignMode)
                        Invalidate();
                }
            }
        }

        [Category("SunnyUI"), Description("The border color used to paint the control.")]
        public Color ItemSelectForeColor
        {
            get => _itemSelectForeColor;
            set
            {
                if (_itemSelectForeColor != value)
                {
                    _itemSelectForeColor = value;
                    _style = UIStyle.Custom;
                    if (DesignMode)
                        Invalidate();
                }
            }
        }

        public delegate void OnBeforeDrawItem(object sender, ObjectCollection items, DrawItemEventArgs e);

        public event OnBeforeDrawItem BeforeDrawItem;

        public event OnBeforeDrawItem AfterDrawItem;

        private StringAlignment textAlignment = StringAlignment.Near;

        public StringAlignment TextAlignment
        {
            get => textAlignment;
            set
            {
                textAlignment = value;
                Invalidate();
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            BeforeDrawItem?.Invoke(this, Items, e);
            if (Items.Count == 0)
            {
                return;
            }

            bool otherState = e.State == DrawItemState.Grayed || e.State == DrawItemState.HotLight;
            if (!otherState)
            {
                e.DrawBackground();
            }

            if (e.Index < 0 || e.Index >= Items.Count)
            {
                return;
            }

            StringFormat sStringFormat = new StringFormat();
            sStringFormat.LineAlignment = StringAlignment.Center;
            sStringFormat.Alignment = textAlignment;

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Color backColor = isSelected ? ItemSelectBackColor : BackColor;
            Color foreColor = isSelected ? ItemSelectForeColor : ForeColor;

            Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
            string showText = DisplayMember.IsNullOrEmpty() ? Items[e.Index].ToString() : GetItemText(Items[e.Index]);

            if (!otherState)
            {
                e.Graphics.FillRectangle(BackColor, e.Bounds);
                e.Graphics.FillRectangle(backColor, rect);
                e.Graphics.DrawString(showText, e.Font, foreColor, e.Bounds, sStringFormat);
            }
            else
            {
                if (e.State == DrawItemState.Grayed)
                {
                    backColor = BackColor;
                    foreColor = ForeColor;
                }

                if (e.State == DrawItemState.HotLight)
                {
                    backColor = HoverColor;
                    foreColor = ForeColor;
                }

                e.Graphics.FillRectangle(BackColor, e.Bounds);
                e.Graphics.FillRectangle(backColor, rect);
                e.Graphics.DrawString(showText, e.Font, foreColor, e.Bounds, sStringFormat);
            }

            AfterDrawItem?.Invoke(this, Items, e);

            base.OnDrawItem(e);
        }

        private Color hoverColor = Color.FromArgb(155, 200, 255);

        [DefaultValue(typeof(Color), "155, 200, 255")]
        public Color HoverColor
        {
            get => hoverColor;
            set
            {
                hoverColor = value;
                _style = UIStyle.Custom;
            }
        }

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            base.OnMeasureItem(e);
            e.ItemHeight += ItemHeight;
        }

        public void SelectedFirst()
        {
            if (Items.Count > 0 && SelectedIndex < 0)
            {
                SelectedIndex = 0;
            }
        }

        private int lastIndex = -1;
        private int mouseIndex = -1;

        [Browsable(false)]
        public int MouseIndex
        {
            get => mouseIndex;
            set
            {
                if (mouseIndex != value)
                {
                    if (lastIndex >= 0 && lastIndex >= 0 && lastIndex < Items.Count && lastIndex != SelectedIndex)
                    {
                        OnDrawItem(new DrawItemEventArgs(CreateGraphics(), Font, GetItemRectangle(lastIndex), lastIndex, DrawItemState.Grayed));
                    }

                    mouseIndex = value;
                    if (mouseIndex >= 0 && mouseIndex >= 0 && mouseIndex < Items.Count && mouseIndex != SelectedIndex)
                    {
                        OnDrawItem(new DrawItemEventArgs(CreateGraphics(), Font, GetItemRectangle(value), value, DrawItemState.HotLight));
                    }

                    lastIndex = mouseIndex;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (SelectionMode != SelectionMode.One) return;
            MouseIndex = IndexFromPoint(e.Location);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (SelectionMode != SelectionMode.One) return;
            MouseIndex = -1;
        }
    }
}