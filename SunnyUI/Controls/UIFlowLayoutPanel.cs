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
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            Panel.MouseEnter += Panel_MouseEnter;
            Panel.MouseClick += Panel_MouseClick;
            Panel.ClientSizeChanged += Panel_ClientSizeChanged;

            VBar.ValueChanged += VBar_ValueChanged;
            HBar.ValueChanged += HBar_ValueChanged;

            SizeChanged += Panel_SizeChanged;
            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (flowLayoutPanel != null) flowLayoutPanel.Font = Font;
            if (VBar != null) VBar.Font = Font;
            if (HBar != null) HBar.Font = Font;
        }

        public new event ScrollEventHandler Scroll;

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

            if (Panel != null && !IsDesignMode)
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
            if (control is IStyleInterface ctrl)
            {
                if (!ctrl.StyleCustomMode) ctrl.Style = Style;
            }

            if (Panel != null)
            {
                Panel.Controls.Add(control);
            }
        }

        [Obsolete("此方法已优化，用Add代替")]
        public void AddControl(Control control)
        {
            if (control is IStyleInterface ctrl)
            {
                if (!ctrl.StyleCustomMode) ctrl.Style = Style;
            }

            if (Panel != null)
            {
                Panel.Controls.Add(control);
            }
        }

        [Obsolete("此方法已优化，用Remove代替")]
        public void RemoveControl(Control control)
        {
            if (Panel != null)
            {
                if (Panel.Controls.Contains(control))
                    Panel.Controls.Remove(control);
            }
        }

        public void Clear()
        {
            foreach (Control control in Panel.Controls)
            {
                control.Dispose();
            }

            Panel.Controls.Clear();
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

        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            Panel.BackColor = uiColor.PlainColor;

            HBar.FillColor = VBar.FillColor = uiColor.FlowLayoutPanelBarFillColor;
            scrollBarColor = HBar.ForeColor = VBar.ForeColor = uiColor.FlowLayoutPanelBarForeColor;
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
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color ScrollBarColor
        {
            get => scrollBarColor;
            set
            {
                scrollBarColor = value;
                VBar.ForeColor = value;
                HBar.ForeColor = value;
                Invalidate();
            }
        }

        private void Panel_MouseClick(object sender, MouseEventArgs e)
        {
            //Panel.Focus();
        }

        private void Panel_MouseEnter(object sender, EventArgs e)
        {
            //Panel.Focus();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Panel.Focus();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Panel.Focus();
        }

        private void Panel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (Panel.VerticalScroll.Maximum > Panel.VerticalScroll.Value + 50)
                    Panel.VerticalScroll.Value += 50;
                else
                    Panel.VerticalScroll.Value = Panel.VerticalScroll.Maximum;
            }
            else
            {
                if (Panel.VerticalScroll.Value > 50)
                    Panel.VerticalScroll.Value -= 50;
                else
                    Panel.VerticalScroll.Value = 0;
            }

            VBar.Value = Panel.VerticalScroll.Value;
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
            this.VBar.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
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
            this.HBar.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
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

                VBar.Width = ScrollBarInfo.VerticalScrollBarWidth();
                VBar.Left = Width - VBar.Width - added;
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
    }
}
