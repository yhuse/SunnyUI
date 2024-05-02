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
 * 文件名称: ListBoxEx.cs
 * 文件说明: 列表框基类
 * 当前版本: V3.1
 * 创建日期: 2022-05-12
 *
 * 2023-05-12: V3.3.6 增加文件说明
 * 2023-05-12: V3.3.6 重构DrawString函数
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// ListBox
    /// </summary>
    [ToolboxItem(false)]
    internal sealed class ListBoxEx : ListBox, IStyleInterface
    {
        private UIScrollBar bar;

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

        private float DefaultFontSize = -1;

        public void SetDPIScale()
        {
            if (!UIDPIScale.NeedSetDPIFont()) return;
            if (DefaultFontSize < 0) DefaultFontSize = this.Font.Size;
            this.SetDPIScaleFont(DefaultFontSize);
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

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
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
        [DefaultValue(false), Browsable(false)]
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
            base.Font = UIStyles.Font();
            IntegralHeight = false;
            ItemHeight = 25;
            Size = new Size(150, 200);
            ResumeLayout(false);
        }

        #endregion 组件设计器生成的代码

        private UIStyle _style = UIStyle.Inherited;
        private Color _itemSelectBackColor = UIColor.Blue;
        private Color _itemSelectForeColor = Color.White;

        /// <summary>
        /// 主题样式
        /// </summary>
        [DefaultValue(UIStyle.Inherited), Description("主题样式"), Category("SunnyUI")]
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

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="style">主题样式</param>
        private void SetStyle(UIStyle style)
        {
            if (!style.IsCustom())
            {
                SetStyleColor(style.Colors());
                Invalidate();
            }

            _style = style == UIStyle.Inherited ? UIStyle.Inherited : UIStyle.Custom;
        }

        public void SetInheritedStyle(UIStyle style)
        {
            SetStyle(style);
            _style = UIStyle.Inherited;
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
                    if (DesignMode) Invalidate();
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
                    if (DesignMode) Invalidate();
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

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Color backColor = isSelected ? ItemSelectBackColor : BackColor;
            Color foreColor = isSelected ? ItemSelectForeColor : ForeColor;

            Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
            string showText = DisplayMember.IsNullOrEmpty() ? Items[e.Index].ToString() : GetItemText(Items[e.Index]);

            if (!otherState)
            {
                e.Graphics.FillRectangle(BackColor, e.Bounds);
                e.Graphics.FillRectangle(backColor, rect);
                e.Graphics.DrawString(showText, e.Font, foreColor, e.Bounds, textAlignment, StringAlignment.Center);
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
                e.Graphics.DrawString(showText, e.Font, foreColor, e.Bounds, textAlignment, StringAlignment.Center);
            }

            AfterDrawItem?.Invoke(this, Items, e);

            base.OnDrawItem(e);
        }

        private Color hoverColor = Color.FromArgb(155, 200, 255);

        [DefaultValue(typeof(Color), "155, 200, 255")]
        public Color HoverColor
        {
            get => hoverColor;
            set => hoverColor = value;

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
                        using var g = CreateGraphics();
                        OnDrawItem(new DrawItemEventArgs(g, Font, GetItemRectangle(lastIndex), lastIndex, DrawItemState.Grayed));
                    }

                    mouseIndex = value;
                    if (mouseIndex >= 0 && mouseIndex >= 0 && mouseIndex < Items.Count && mouseIndex != SelectedIndex)
                    {
                        using var g = CreateGraphics();
                        OnDrawItem(new DrawItemEventArgs(g, Font, GetItemRectangle(value), value, DrawItemState.HotLight));
                    }

                    lastIndex = mouseIndex;
                }
            }
        }

        /// <summary>
        /// 重载鼠标移动事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (SelectionMode != SelectionMode.One) return;
            MouseIndex = IndexFromPoint(e.Location);
        }

        /// <summary>
        /// 重载鼠标离开事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (SelectionMode != SelectionMode.One) return;
            MouseIndex = -1;
        }
    }
}
