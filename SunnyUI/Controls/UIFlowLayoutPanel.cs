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
 * 文件名称: UIFlowLayoutPanel.cs
 * 文件说明: FlowLayoutPanel
 * 当前版本: V2.2
 * 创建日期: 2020-09-29
 *
 * 2020-09-29: V2.2.8 增加文件说明
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class UIFlowLayoutPanel : UIPanel
    {
        private UIVerScrollBarEx VBar;
        private UIHorScrollBarEx HBar;
        private FlowLayoutPanel flowLayoutPanel;

        public UIFlowLayoutPanel()
        {
            InitializeComponent();

            ShowText = false;

            Panel.AutoScroll = true;
            Panel.ControlAdded += Panel_ControlAdded;
            Panel.ControlRemoved += Panel_ControlRemoved;
            Panel.Scroll += Panel_Scroll;
            Panel.MouseWheel += Panel_MouseWheel;
            Panel.MouseEnter += Panel_MouseEnter;
            Panel.MouseClick += Panel_MouseClick;

            VBar.ValueChanged += VBar_ValueChanged;
            HBar.ValueChanged += HBar_ValueChanged;

            SizeChanged += Panel_SizeChanged;
        }

        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
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

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            Panel.BackColor = uiColor.PlainColor;
        }

        protected override void AfterSetFillColor(Color color)
        {
            base.AfterSetFillColor(color);
            Panel.BackColor = color;
            VBar.FillColor = color;
            HBar.FillColor = color;
        }

        public void AddControl(Control ctrl)
        {
            Panel.Controls.Add(ctrl);
        }

        public void RemoveControl(Control ctrl)
        {
            Panel.Controls.Remove(ctrl);
        }

        public void Clear()
        {
            Panel.Controls.Clear();
        }

        private void Panel_MouseClick(object sender, MouseEventArgs e)
        {
            Panel.Focus();
        }

        private void Panel_MouseEnter(object sender, EventArgs e)
        {
            Panel.Focus();
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
            Panel.VerticalScroll.Value = VBar.Value;
        }

        private void HBar_ValueChanged(object sender, EventArgs e)
        {
            Panel.HorizontalScroll.Value = HBar.Value;
        }

        private void Panel_Scroll(object sender, ScrollEventArgs e)
        {
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

        private void SetScrollInfo()
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
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.VBar = new Sunny.UI.UIVerScrollBarEx();
            this.HBar = new Sunny.UI.UIHorScrollBarEx();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.Location = new System.Drawing.Point(2, 2);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(429, 383);
            this.flowLayoutPanel.TabIndex = 0;
            // 
            // VBar
            // 
            this.VBar.BoundsHeight = 10;
            this.VBar.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.VBar.LargeChange = 10;
            this.VBar.Location = new System.Drawing.Point(410, 5);
            this.VBar.Maximum = 100;
            this.VBar.MinimumSize = new System.Drawing.Size(1, 1);
            this.VBar.Name = "VBar";
            this.VBar.Size = new System.Drawing.Size(18, 377);
            this.VBar.TabIndex = 1;
            this.VBar.Text = "uiVerScrollBarEx1";
            this.VBar.Value = 0;
            this.VBar.Visible = false;
            // 
            // HBar
            // 
            this.HBar.BoundsWidth = 10;
            this.HBar.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.HBar.LargeChange = 10;
            this.HBar.Location = new System.Drawing.Point(5, 364);
            this.HBar.Maximum = 100;
            this.HBar.MinimumSize = new System.Drawing.Size(1, 1);
            this.HBar.Name = "HBar";
            this.HBar.Size = new System.Drawing.Size(399, 18);
            this.HBar.TabIndex = 2;
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
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(433, 387);
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

                VBar.Left = Width - VBar.Width - added;
                VBar.Top = added;
                VBar.Height = Height - added * 2;

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
