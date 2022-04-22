
namespace Sunny.UI.Demo
{
    partial class FMeter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMeter));
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.uiAnalogMeter1 = new Sunny.UI.UIAnalogMeter();
            this.uiLine9 = new Sunny.UI.UILine();
            this.uiRoundMeter2 = new Sunny.UI.UIRoundMeter();
            this.uiLine6 = new Sunny.UI.UILine();
            this.uiRoundMeter1 = new Sunny.UI.UIRoundMeter();
            this.uiPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.uiAnalogMeter1);
            this.uiPanel1.Controls.Add(this.uiLine9);
            this.uiPanel1.Controls.Add(this.uiRoundMeter2);
            this.uiPanel1.Controls.Add(this.uiLine6);
            this.uiPanel1.Controls.Add(this.uiRoundMeter1);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel1.FillColor2 = System.Drawing.Color.CornflowerBlue;
            this.uiPanel1.FillColorGradient = true;
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(0, 35);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiPanel1.Size = new System.Drawing.Size(800, 415);
            this.uiPanel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiPanel1.StyleCustomMode = true;
            this.uiPanel1.TabIndex = 69;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiAnalogMeter1
            // 
            this.uiAnalogMeter1.BackColor = System.Drawing.Color.Transparent;
            this.uiAnalogMeter1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiAnalogMeter1.Location = new System.Drawing.Point(381, 61);
            this.uiAnalogMeter1.MaxValue = 100D;
            this.uiAnalogMeter1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiAnalogMeter1.MinValue = 0D;
            this.uiAnalogMeter1.Name = "uiAnalogMeter1";
            this.uiAnalogMeter1.Renderer = null;
            this.uiAnalogMeter1.Size = new System.Drawing.Size(140, 140);
            this.uiAnalogMeter1.Style = Sunny.UI.UIStyle.Custom;
            this.uiAnalogMeter1.TabIndex = 73;
            this.uiAnalogMeter1.Text = "uiAnalogMeter1";
            this.uiAnalogMeter1.Value = 0D;
            this.uiAnalogMeter1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLine9
            // 
            this.uiLine9.BackColor = System.Drawing.Color.Transparent;
            this.uiLine9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine9.Location = new System.Drawing.Point(381, 20);
            this.uiLine9.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine9.Name = "uiLine9";
            this.uiLine9.Size = new System.Drawing.Size(319, 20);
            this.uiLine9.Style = Sunny.UI.UIStyle.Custom;
            this.uiLine9.TabIndex = 72;
            this.uiLine9.Text = "UIAnalogMeter";
            this.uiLine9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLine9.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiRoundMeter2
            // 
            this.uiRoundMeter2.AngleImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter2.AngleImage")));
            this.uiRoundMeter2.BackColor = System.Drawing.Color.Transparent;
            this.uiRoundMeter2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter2.BackgroundImage")));
            this.uiRoundMeter2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.uiRoundMeter2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiRoundMeter2.Location = new System.Drawing.Point(186, 56);
            this.uiRoundMeter2.MeterType = Sunny.UI.UIRoundMeter.TMeterType.Wind;
            this.uiRoundMeter2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRoundMeter2.Name = "uiRoundMeter2";
            this.uiRoundMeter2.Size = new System.Drawing.Size(140, 140);
            this.uiRoundMeter2.Style = Sunny.UI.UIStyle.Custom;
            this.uiRoundMeter2.TabIndex = 71;
            this.uiRoundMeter2.Text = "uiRoundMeter2";
            this.uiRoundMeter2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLine6
            // 
            this.uiLine6.BackColor = System.Drawing.Color.Transparent;
            this.uiLine6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine6.Location = new System.Drawing.Point(30, 20);
            this.uiLine6.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine6.Name = "uiLine6";
            this.uiLine6.Size = new System.Drawing.Size(319, 20);
            this.uiLine6.Style = Sunny.UI.UIStyle.Custom;
            this.uiLine6.TabIndex = 70;
            this.uiLine6.Text = "UIRoundMeter";
            this.uiLine6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLine6.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiRoundMeter1
            // 
            this.uiRoundMeter1.AngleImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter1.AngleImage")));
            this.uiRoundMeter1.BackColor = System.Drawing.Color.Transparent;
            this.uiRoundMeter1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter1.BackgroundImage")));
            this.uiRoundMeter1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.uiRoundMeter1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiRoundMeter1.Location = new System.Drawing.Point(30, 56);
            this.uiRoundMeter1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRoundMeter1.Name = "uiRoundMeter1";
            this.uiRoundMeter1.Size = new System.Drawing.Size(140, 140);
            this.uiRoundMeter1.Style = Sunny.UI.UIStyle.Custom;
            this.uiRoundMeter1.TabIndex = 69;
            this.uiRoundMeter1.Text = "uiRoundMeter1";
            this.uiRoundMeter1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // FMeter
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.uiPanel1);
            this.Name = "FMeter";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.ShowTitle = true;
            this.Symbol = 61668;
            this.Text = "Meter";
            this.uiPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private UIPanel uiPanel1;
        private UIAnalogMeter uiAnalogMeter1;
        private UILine uiLine9;
        private UIRoundMeter uiRoundMeter2;
        private UILine uiLine6;
        private UIRoundMeter uiRoundMeter1;
    }
}