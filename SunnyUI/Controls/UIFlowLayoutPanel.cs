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
 * 文件名称: UIFlowLayoutPanel.cs
 * 文件说明: FlowLayoutPanel
 * 当前版本: V3.1
 * 创建日期: 2020-09-29
 *
 * 2020-09-29: V2.2.8 增加文件说明
 * 2021-07-10: V3.0.4 增加滚动条颜色属性 
 * 2021-07-31: V3.0.5 可像原生控件一样通过Controls.Add增加
 * 2021-08-11: V3.0.5 删除点击的Focus事件
 * 2021-10-18: V3.0.8 增加Scroll事件
 * 2021-11-05: V3.0.8 修改不同DPI缩放滚动条未覆盖的问题
 * 2022-11-03: V3.2.6 增加了可设置垂直滚动条宽度的属性
 * 2022-11-13: V3.2.8 增加滚动条背景色调整
 * 2022-11-13: V3.2.8 删除AddControl、RemoveControl方法
 * 2022-11-25: V3.2.9 增加Get方法以获取控件
 * 2023-01-11: V3.3.1 增加AutoScroll属性
 * 2023-01-11: V3.3.1 修复只显示水平滚动条时，鼠标滚轮滚动水平滚动条不动的问题
 * 2023-11-05: V3.5.2 重构主题
 * 2024-01-17: V3.6.3 重写ScrollControlIntoView函数
 * 2024-04-28: V3.6.5 增加Render方法，尝试解决点击状态栏恢复窗体后右侧滚动条未消失的问题
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class UIFlowLayoutPanel : UIPanel, IToolTip
    {
        private UIVerScrollBarEx VBar;
        private UIHorScrollBarEx HBar;
        private FlowLayoutPanel flowLayoutPanel;
        private readonly Timer timer;

        public UIFlowLayoutPanel()
        {
            InitializeComponent();
            SetStyleFlags(true, false);
            ShowText = false;

            Panel.AutoScroll = true;
            Panel.ControlAdded += Panel_ControlAdded;
            Panel.ControlRemoved += Panel_ControlRemoved;
            Panel.Scroll += Panel_Scroll;
            Panel.MouseWheel += Panel_MouseWheel;
            Panel.ClientSizeChanged += Panel_ClientSizeChanged;
            Panel.BackColor = UIStyles.Blue.PlainColor;

            VBar.ValueChanged += VBar_ValueChanged;
            HBar.ValueChanged += HBar_ValueChanged;

            SizeChanged += Panel_SizeChanged;
            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public void Render()
        {
            if (Panel == null) return;
            int height = Panel.Height;
            Panel.Height = height + 1;
            Panel.Height = height;
        }

        [DefaultValue(true)]
        [Browsable(true)]
        public new bool AutoScroll
        {
            get => Panel.AutoScroll;
            set => Panel.AutoScroll = value;
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
                if (VBar != null) VBar.FillWidth = value;
            }
        }

        protected override void OnContextMenuStripChanged(EventArgs e)
        {
            base.OnContextMenuStripChanged(e);
            if (Panel != null) Panel.ContextMenuStrip = ContextMenuStrip;
        }

        /// <summary>
        /// 重载字体变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (flowLayoutPanel != null) flowLayoutPanel.Font = Font;
            if (VBar != null) VBar.Font = Font;
            if (HBar != null) HBar.Font = Font;
        }

        public new event ScrollEventHandler Scroll;

        /// <summary>
        /// 需要额外设置ToolTip的控件
        /// </summary>
        /// <returns>控件</returns>
        public Control ExToolTipControl()
        {
            return Panel;
        }

        public new void Focus()
        {
            base.Focus();
            Panel.Focus();
        }

        public override bool Focused
        {
            get => Panel.Focused;
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            Panel.Focus();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            timer?.Stop();
            timer?.Dispose();
        }

        [DefaultValue(FlowDirection.LeftToRight)]
        [Localizable(true)]
        public FlowDirection FlowDirection
        {
            get => Panel.FlowDirection;
            set => Panel.FlowDirection = value;
        }

        [DefaultValue(true)]
        [Localizable(true)]
        public bool WrapContents
        {
            get => Panel.WrapContents;
            set => Panel.WrapContents = value;
        }

        [DefaultValue(false)]
        [DisplayName("FlowBreak")]
        public bool GetFlowBreak(Control control)
        {
            return Panel.GetFlowBreak(control);
        }

        [DisplayName("FlowBreak")]
        public void SetFlowBreak(Control control, bool value)
        {
            Panel.SetFlowBreak(control, value);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (e.Control is UIHorScrollBarEx bar1)
            {
                if (bar1.TagString == "79E1E7DD-3E4D-916B-C8F1-F45B579C290C")
                {
                    base.OnControlAdded(e);
                    return;
                }
            }

            if (e.Control is UIVerScrollBarEx bar2)
            {
                if (bar2.TagString == "63FD1249-41D3-E08A-F8F5-CC41CC30FD03")
                {
                    base.OnControlAdded(e);
                    return;
                }
            }

            if (e.Control is FlowLayoutPanel panel)
            {
                if (panel.Tag.ToString() == "69605093-6397-AD32-9F69-3C29F642F87E")
                {
                    base.OnControlAdded(e);
                    return;
                }
            }

            if (Panel != null && !DesignMode)
            {
                Add(e.Control);
            }
            else
            {
                base.OnControlAdded(e);
                if (Panel != null) Panel.SendToBack();
            }
        }

        public void Remove(Control control)
        {
            if (Panel != null)
            {
                if (Panel.Controls.Contains(control))
                    Panel.Controls.Remove(control);
            }
        }

        public void Add(Control control)
        {
            Panel?.Controls.Add(control);
        }

        public void Clear()
        {
            foreach (Control control in Panel.Controls)
            {
                control.Dispose();
            }

            Panel.Controls.Clear();
        }

        [Browsable(false)]
        public ControlCollection AllControls => Panel.Controls;

        public T Get<T>(string controlName) where T : Control
        {
            return Panel.GetControls<T>()?.Where(p => p.Name == controlName).FirstOrDefault();
        }

        public Control Get(string controlName)
        {
            return Panel.Controls.ContainsKey(controlName) ? Panel.Controls[controlName] : null;
        }

        public Control Get(int index)
        {
            return (index >= 0 && index < Panel.Controls.Count) ? Panel.Controls[index] : null;
        }

        [Browsable(false)]
        public int ControlCount => AllControls.Count;

        [Browsable(false)]
        public Control this[string controlName]
        {
            get => Get(controlName);
        }

        [Browsable(false)]
        public Control this[int index]
        {
            get => Get(index);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (VBar.Maximum != Panel.VerticalScroll.Maximum ||
                VBar.Visible != Panel.VerticalScroll.Visible ||
                HBar.Maximum != Panel.HorizontalScroll.Maximum ||
                HBar.Visible != Panel.HorizontalScroll.Visible)
            {
                SetScrollInfo();
            }
        }

        private void Panel_ClientSizeChanged(object sender, EventArgs e)
        {
            SetScrollInfo();
        }

        [Browsable(false)]
        public FlowLayoutPanel FlowLayoutPanel => flowLayoutPanel;

        /// <summary>
        /// 绘制前景颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
        }

        //public override void SetInheritedStyle(UIStyle style)
        //{
        //    UIStyleHelper.SetChildUIStyle(this, style);
        //    base.SetInheritedStyle(style);
        //}

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            Panel.BackColor = uiColor.PlainColor;

            if (HBar != null && HBar.Style == UIStyle.Inherited)
            {
                HBar.ForeColor = uiColor.GridBarForeColor;
                HBar.HoverColor = uiColor.ButtonFillHoverColor;
                HBar.PressColor = uiColor.ButtonFillPressColor;
                HBar.FillColor = uiColor.GridBarFillColor;
                scrollBarColor = uiColor.GridBarForeColor;
                scrollBarBackColor = uiColor.GridBarFillColor;
            }

            if (VBar != null && VBar.Style == UIStyle.Inherited)
            {
                VBar.ForeColor = uiColor.GridBarForeColor;
                VBar.HoverColor = uiColor.ButtonFillHoverColor;
                VBar.PressColor = uiColor.ButtonFillPressColor;
                VBar.FillColor = uiColor.GridBarFillColor;
                scrollBarColor = uiColor.GridBarForeColor;
                scrollBarBackColor = uiColor.GridBarFillColor;
            }
        }

        protected override void AfterSetFillColor(Color color)
        {
            base.AfterSetFillColor(color);
            Panel.BackColor = color;
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
                HBar.HoverColor = HBar.PressColor = HBar.ForeColor = value;
                VBar.HoverColor = VBar.PressColor = VBar.ForeColor = value;
                HBar.Style = VBar.Style = UIStyle.Custom;
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
                HBar.FillColor = value;
                VBar.FillColor = value;
                HBar.Style = VBar.Style = UIStyle.Custom;
                Invalidate();
            }
        }

        /// <summary>
        /// 滚动条主题样式
        /// </summary>
        [DefaultValue(true), Description("滚动条主题样式"), Category("SunnyUI")]
        public bool ScrollBarStyleInherited
        {
            get => HBar != null && HBar.Style == UIStyle.Inherited;
            set
            {
                if (value)
                {
                    if (HBar != null) HBar.Style = UIStyle.Inherited;
                    if (VBar != null) VBar.Style = UIStyle.Inherited;

                    scrollBarColor = UIStyles.Blue.GridBarForeColor;
                    scrollBarBackColor = UIStyles.Blue.GridBarFillColor;
                }

            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Panel.Focus();
        }

        /// <summary>
        /// 重载鼠标进入事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Panel.Focus();
        }

        private void Panel_MouseWheel(object sender, MouseEventArgs e)
        {
            //if (e.Delta < 0)
            //{
            //    if (Panel.VerticalScroll.Maximum > Panel.VerticalScroll.Value + 50)
            //        Panel.VerticalScroll.Value += 50;
            //    else
            //        Panel.VerticalScroll.Value = Panel.VerticalScroll.Maximum;
            //}
            //else
            //{
            //    if (Panel.VerticalScroll.Value > 50)
            //        Panel.VerticalScroll.Value -= 50;
            //    else
            //        Panel.VerticalScroll.Value = 0;
            //}

            VBar.Value = Panel.VerticalScroll.Value;
            HBar.Value = Panel.HorizontalScroll.Value;
        }

        private void VBar_ValueChanged(object sender, EventArgs e)
        {
            if (VBar.Value.InRange(0, Panel.VerticalScroll.Maximum))
                Panel.VerticalScroll.Value = VBar.Value;
        }

        private void HBar_ValueChanged(object sender, EventArgs e)
        {
            Panel.HorizontalScroll.Value = HBar.Value;
        }

        private void Panel_Scroll(object sender, ScrollEventArgs e)
        {
            Scroll?.Invoke(this, e);
            VBar.Value = Panel.VerticalScroll.Value;
        }

        private void Panel_SizeChanged(object sender, EventArgs e)
        {
            SetScrollInfo();
        }

        private void Panel_ControlRemoved(object sender, ControlEventArgs e)
        {
            SetScrollInfo();
        }

        private void Panel_ControlAdded(object sender, ControlEventArgs e)
        {
            SetScrollInfo();
        }

        public void SetScrollInfo()
        {
            VBar.Visible = Panel.VerticalScroll.Visible;
            VBar.Maximum = Panel.VerticalScroll.Maximum;
            VBar.Value = Panel.VerticalScroll.Value;
            VBar.LargeChange = Panel.VerticalScroll.LargeChange;
            VBar.BoundsHeight = Panel.VerticalScroll.LargeChange;

            HBar.Visible = Panel.HorizontalScroll.Visible;
            HBar.Maximum = Panel.HorizontalScroll.Maximum;
            HBar.Value = Panel.HorizontalScroll.Value;
            HBar.LargeChange = Panel.HorizontalScroll.LargeChange;
            HBar.BoundsWidth = Panel.HorizontalScroll.LargeChange;

            SetScrollPos();
        }
        public FlowLayoutPanel Panel => flowLayoutPanel;

        private void InitializeComponent()
        {
            this.flowLayoutPanel = new FlowLayoutPanel();
            this.VBar = new UIVerScrollBarEx();
            this.HBar = new UIHorScrollBarEx();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.BackColor = Color.FromArgb(235, 243, 255);
            this.flowLayoutPanel.Dock = DockStyle.Fill;
            this.flowLayoutPanel.Location = new Point(2, 2);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new Size(429, 383);
            this.flowLayoutPanel.TabIndex = 0;
            this.flowLayoutPanel.Tag = "69605093-6397-AD32-9F69-3C29F642F87E";
            // 
            // VBar
            // 
            this.VBar.BoundsHeight = 10;
            this.VBar.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            this.VBar.LargeChange = 10;
            this.VBar.Location = new Point(410, 5);
            this.VBar.Maximum = 100;
            this.VBar.MinimumSize = new Size(1, 1);
            this.VBar.Name = "VBar";
            this.VBar.Size = new Size(18, 377);
            this.VBar.TabIndex = 1;
            this.VBar.TagString = "63FD1249-41D3-E08A-F8F5-CC41CC30FD03";
            this.VBar.Text = "uiVerScrollBarEx1";
            this.VBar.Value = 0;
            this.VBar.Visible = false;
            // 
            // HBar
            // 
            this.HBar.BoundsWidth = 10;
            this.HBar.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            this.HBar.LargeChange = 10;
            this.HBar.Location = new Point(5, 364);
            this.HBar.Maximum = 100;
            this.HBar.MinimumSize = new Size(1, 1);
            this.HBar.Name = "HBar";
            this.HBar.Size = new Size(399, 18);
            this.HBar.TabIndex = 2;
            this.HBar.TagString = "79E1E7DD-3E4D-916B-C8F1-F45B579C290C";
            this.HBar.Text = "uiHorScrollBarEx1";
            this.HBar.Value = 0;
            this.HBar.Visible = false;
            // 
            // UIFlowLayoutPanel
            // 
            this.Controls.Add(this.HBar);
            this.Controls.Add(this.VBar);
            this.Controls.Add(this.flowLayoutPanel);
            this.Name = "UIFlowLayoutPanel";
            this.Padding = new Padding(2);
            this.Size = new Size(433, 387);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetScrollPos();
        }

        private void SetScrollPos()
        {
            if (VBar != null && HBar != null)
            {
                int added = 1;
                if (RadiusSides != UICornerRadiusSides.None)
                {
                    added = Radius / 2;
                }

                int barWidth = Math.Max(ScrollBarInfo.VerticalScrollBarWidth(), ScrollBarWidth);
                VBar.Width = barWidth;
                VBar.Left = Width - barWidth - added;
                VBar.Top = added;
                VBar.Height = Height - added * 2;

                HBar.Height = ScrollBarInfo.HorizontalScrollBarHeight();
                HBar.Left = added;
                HBar.Top = Height - HBar.Height - added;

                if (VBar.Visible)
                    HBar.Width = VBar.Left - 1 - added;
                else
                    HBar.Width = Width - added * 2;
            }
        }

        public new void ScrollControlIntoView(Control activeControl)
        {
            Panel.ScrollControlIntoView(activeControl);
        }
    }
}
