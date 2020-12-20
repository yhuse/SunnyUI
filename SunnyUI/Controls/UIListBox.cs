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
 * 文件名称: UIListBox.cs
 * 文件说明: 列表框
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2020-05-21: V2.2.5 增加鼠标滑过高亮
 *                    开发日志：https://www.cnblogs.com/yhuse/p/12933885.html
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("ItemClick")]
    [DefaultProperty("Items")]
    public sealed partial class UIListBox : UIPanel
    {
        private readonly ListBoxEx listbox = new ListBoxEx();
        private readonly UIScrollBar bar = new UIScrollBar();
        private readonly Timer timer = new Timer();

        public UIListBox()
        {
            InitializeComponent();
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
            listbox.DoubleClick += Listbox_DoubleClick;
            listbox.BeforeDrawItem += Listbox_BeforeDrawItem;
            listbox.MouseDown += Listbox_MouseDown;
            listbox.MouseUp += Listbox_MouseUp;
            listbox.MouseMove += Listbox_MouseMove;

            timer.Tick += Timer_Tick;
            timer.Start();
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

        ~UIListBox()
        {
            timer.Stop();
            timer.Dispose();
        }

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
            listbox.Font = Font;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            bar.Top = 2;
            bar.Height = Height - 4;
            bar.Left = Width - bar.Width - 2;
        }

        private void Listbox_BeforeDrawItem(object sender, ListBox.ObjectCollection items, DrawItemEventArgs e)
        {
            if (Items.Count != LastCount)
            {
                listbox.SetScrollInfo();
                LastCount = Items.Count;
                ItemsCountChange?.Invoke(this, null);
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
            if (uiColor.IsCustom()) return;

            if (bar != null)
            {
                bar.ForeColor = uiColor.PrimaryColor;
                bar.HoverColor = uiColor.ButtonFillHoverColor;
                bar.PressColor = uiColor.ButtonFillPressColor;
                bar.FillColor = Color.White;
            }

            hoverColor = uiColor.TreeViewHoverColor;
            if (listbox != null)
            {
                listbox.HoverColor = hoverColor;
                listbox.SetStyleColor(uiColor);
                listbox.BackColor = Color.White;
            }

            fillColor = Color.White;
        }

        protected override void AfterSetFillColor(Color color)
        {
            base.AfterSetFillColor(color);
            if (listbox != null)
            {
                listbox.BackColor = color;
            }

            if (bar != null)
            {
                bar.FillColor = color;
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
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        [Description("列表项"), Category("SunnyUI")]
        public ListBox.ObjectCollection Items => listbox.Items;

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

        [DefaultValue(typeof(Color), "White")]
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

        [DefaultValue(typeof(Color), "155, 200, 255")]
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
    }

    /// <summary>
    /// ListBox
    /// </summary>
    [ToolboxItem(false)]
    public sealed class ListBoxEx : ListBox, IStyleInterface
    {
        private UIScrollBar bar;

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

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
            Font = UIFontColor.Font;
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
            SetStyleColor(UIStyles.GetStyleColor(style));
            _style = style;
        }

        public void SetStyleColor(UIBaseStyle uiColor)
        {
            if (uiColor.IsCustom()) return;

            ItemSelectBackColor = uiColor.ListItemSelectBackColor;
            ItemSelectForeColor = uiColor.ListItemSelectForeColor;
            Invalidate();
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

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

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

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Color backColor = isSelected ? ItemSelectBackColor : BackColor;
            Color foreColor = isSelected ? ItemSelectForeColor : ForeColor;

            Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
            if (!otherState)
            {
                e.Graphics.FillRectangle(BackColor, e.Bounds);
                e.Graphics.FillRoundRectangle(backColor, rect, 5);
                e.Graphics.DrawString(Items[e.Index].ToString(), e.Font, foreColor, e.Bounds, sStringFormat);
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
                e.Graphics.FillRoundRectangle(backColor, rect, 5);
                e.Graphics.DrawString(Items[e.Index].ToString(), e.Font, foreColor, e.Bounds, sStringFormat);
            }

            AfterDrawItem?.Invoke(this, Items, e);
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
            MouseIndex = IndexFromPoint(e.Location);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            MouseIndex = -1;
        }
    }
}