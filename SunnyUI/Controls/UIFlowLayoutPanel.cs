using System;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class UIFlowLayoutPanel : UIPanel
    {
        private UIVerScrollBarEx Bar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private Timer timer = new Timer();

        public UIFlowLayoutPanel()
        {
            InitializeComponent();

            Panel.AutoScroll = true;
            Panel.ControlAdded += Panel_ControlAdded;
            Panel.ControlRemoved += Panel_ControlRemoved;
            Panel.SizeChanged += Panel_SizeChanged;
            Panel.Scroll += Panel_Scroll;
            Panel.MouseWheel += Panel_MouseWheel;
            Panel.MouseEnter += Panel_MouseEnter;
            Panel.MouseClick += Panel_MouseClick;
            Bar.ValueChanged += Bar_ValueChanged;
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            Panel.BackColor = uiColor.PlainColor;
        }

        public void AddControl(Control ctrl)
        {
            Panel.Controls.Add(ctrl);
        }

        public void Clear()
        {
            foreach (Control control in Panel.Controls)
            {
                control.Dispose();
            }

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

            Bar.Value = Panel.VerticalScroll.Value;
        }

        private void Bar_ValueChanged(object sender, EventArgs e)
        {
            Panel.VerticalScroll.Value = Bar.Value;
        }

        private void Panel_Scroll(object sender, ScrollEventArgs e)
        {
            Bar.Value = Panel.VerticalScroll.Value;
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
            Bar.Visible = Panel.VerticalScroll.Visible;
            Bar.Maximum = Panel.VerticalScroll.Maximum;
            Bar.Value = Panel.VerticalScroll.Value;
            Bar.LargeChange = Panel.VerticalScroll.LargeChange;
            Bar.BoundsHeight = Panel.VerticalScroll.LargeChange;
        }

        public FlowLayoutPanel Panel => flowLayoutPanel;

        private void InitializeComponent()
        {
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.Bar = new Sunny.UI.UIVerScrollBarEx();
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
            // Bar
            // 
            this.Bar.BoundsHeight = 10;
            this.Bar.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Bar.LargeChange = 10;
            this.Bar.Location = new System.Drawing.Point(410, 5);
            this.Bar.Maximum = 100;
            this.Bar.MinimumSize = new System.Drawing.Size(1, 1);
            this.Bar.Name = "Bar";
            this.Bar.Size = new System.Drawing.Size(18, 377);
            this.Bar.TabIndex = 1;
            this.Bar.Text = "uiVerScrollBarEx1";
            this.Bar.Value = 0;
            // 
            // UIFlowLayoutPanel
            // 
            this.Controls.Add(this.Bar);
            this.Controls.Add(this.flowLayoutPanel);
            this.Name = "UIFlowLayoutPanel";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(433, 387);
            this.ResumeLayout(false);

        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (Bar != null)
            {
                if (RadiusSides != UICornerRadiusSides.None)
                {
                    int added = Radius / 2;
                    Bar.Left = Width - Bar.Width - added;
                    Bar.Top = added;
                    Bar.Height = Height - added * 2;
                }
                else
                {
                    Bar.Left = Width - Bar.Width - 1;
                    Bar.Top = 1;
                    Bar.Height = Height - 2;
                }
            }
        }
    }
}
