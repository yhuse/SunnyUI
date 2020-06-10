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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMeter));
            this.uiAnalogMeter1 = new Sunny.UI.UIAnalogMeter();
            this.uiLine9 = new Sunny.UI.UILine();
            this.uiLine7 = new Sunny.UI.UILine();
            this.uiLine6 = new Sunny.UI.UILine();
            this.uiLine1 = new Sunny.UI.UILine();
            this.uiLedStopwatch1 = new Sunny.UI.UILedStopwatch();
            this.uiLedDisplay1 = new Sunny.UI.UILedDisplay();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.uiRoundMeter2 = new Sunny.UI.UIRoundMeter();
            this.uiRoundMeter1 = new Sunny.UI.UIRoundMeter();
            this.uiLine2 = new Sunny.UI.UILine();
            this.uiBattery1 = new Sunny.UI.UIBattery();
            this.uiBattery2 = new Sunny.UI.UIBattery();
            this.uiBattery3 = new Sunny.UI.UIBattery();
            this.uiBattery4 = new Sunny.UI.UIBattery();
            this.uiBattery5 = new Sunny.UI.UIBattery();
            this.uiBattery6 = new Sunny.UI.UIBattery();
            this.PagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PagePanel
            // 
            this.PagePanel.Controls.Add(this.uiBattery6);
            this.PagePanel.Controls.Add(this.uiBattery5);
            this.PagePanel.Controls.Add(this.uiBattery4);
            this.PagePanel.Controls.Add(this.uiBattery3);
            this.PagePanel.Controls.Add(this.uiBattery2);
            this.PagePanel.Controls.Add(this.uiBattery1);
            this.PagePanel.Controls.Add(this.uiLine2);
            this.PagePanel.Controls.Add(this.uiAnalogMeter1);
            this.PagePanel.Controls.Add(this.uiLine9);
            this.PagePanel.Controls.Add(this.uiLine7);
            this.PagePanel.Controls.Add(this.uiRoundMeter2);
            this.PagePanel.Controls.Add(this.uiLine6);
            this.PagePanel.Controls.Add(this.uiRoundMeter1);
            this.PagePanel.Controls.Add(this.uiLine1);
            this.PagePanel.Controls.Add(this.uiLedStopwatch1);
            this.PagePanel.Controls.Add(this.uiLedDisplay1);
            // 
            // uiAnalogMeter1
            // 
            this.uiAnalogMeter1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiAnalogMeter1.Location = new System.Drawing.Point(381, 146);
            this.uiAnalogMeter1.MaxValue = 100D;
            this.uiAnalogMeter1.MinValue = 0D;
            this.uiAnalogMeter1.Name = "uiAnalogMeter1";
            this.uiAnalogMeter1.Renderer = null;
            this.uiAnalogMeter1.Size = new System.Drawing.Size(140, 140);
            this.uiAnalogMeter1.TabIndex = 52;
            this.uiAnalogMeter1.Text = "uiAnalogMeter1";
            this.uiAnalogMeter1.Value = 0D;
            // 
            // uiLine9
            // 
            this.uiLine9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine9.Location = new System.Drawing.Point(381, 105);
            this.uiLine9.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine9.Name = "uiLine9";
            this.uiLine9.Size = new System.Drawing.Size(319, 20);
            this.uiLine9.TabIndex = 51;
            this.uiLine9.Text = "UIAnalogMeter";
            this.uiLine9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLine7
            // 
            this.uiLine7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine7.Location = new System.Drawing.Point(381, 20);
            this.uiLine7.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine7.Name = "uiLine7";
            this.uiLine7.Size = new System.Drawing.Size(319, 20);
            this.uiLine7.TabIndex = 50;
            this.uiLine7.Text = "UILedStopwatch";
            this.uiLine7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLine6
            // 
            this.uiLine6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine6.Location = new System.Drawing.Point(30, 105);
            this.uiLine6.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine6.Name = "uiLine6";
            this.uiLine6.Size = new System.Drawing.Size(319, 20);
            this.uiLine6.TabIndex = 48;
            this.uiLine6.Text = "UIRoundMeter";
            this.uiLine6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLine1
            // 
            this.uiLine1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine1.Location = new System.Drawing.Point(30, 20);
            this.uiLine1.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(319, 20);
            this.uiLine1.TabIndex = 46;
            this.uiLine1.Text = "UILedDisplay";
            this.uiLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLedStopwatch1
            // 
            this.uiLedStopwatch1.Active = true;
            this.uiLedStopwatch1.BackColor = System.Drawing.Color.Black;
            this.uiLedStopwatch1.ForeColor = System.Drawing.Color.Lime;
            this.uiLedStopwatch1.Location = new System.Drawing.Point(381, 52);
            this.uiLedStopwatch1.Name = "uiLedStopwatch1";
            this.uiLedStopwatch1.Size = new System.Drawing.Size(190, 34);
            this.uiLedStopwatch1.TabIndex = 45;
            this.uiLedStopwatch1.Text = "00:06";
            // 
            // uiLedDisplay1
            // 
            this.uiLedDisplay1.BackColor = System.Drawing.Color.Black;
            this.uiLedDisplay1.ForeColor = System.Drawing.Color.Lime;
            this.uiLedDisplay1.Location = new System.Drawing.Point(30, 52);
            this.uiLedDisplay1.Name = "uiLedDisplay1";
            this.uiLedDisplay1.Size = new System.Drawing.Size(190, 34);
            this.uiLedDisplay1.TabIndex = 44;
            this.uiLedDisplay1.Text = "999.9 Ω";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // uiRoundMeter2
            // 
            this.uiRoundMeter2.Angle = 0D;
            this.uiRoundMeter2.AngleImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter2.AngleImage")));
            this.uiRoundMeter2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter2.BackgroundImage")));
            this.uiRoundMeter2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.uiRoundMeter2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiRoundMeter2.Location = new System.Drawing.Point(186, 141);
            this.uiRoundMeter2.MeterType = Sunny.UI.UIRoundMeter.TMeterType.Wind;
            this.uiRoundMeter2.Name = "uiRoundMeter2";
            this.uiRoundMeter2.Size = new System.Drawing.Size(140, 140);
            this.uiRoundMeter2.TabIndex = 49;
            this.uiRoundMeter2.Text = "uiRoundMeter2";
            // 
            // uiRoundMeter1
            // 
            this.uiRoundMeter1.Angle = 0D;
            this.uiRoundMeter1.AngleImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter1.AngleImage")));
            this.uiRoundMeter1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter1.BackgroundImage")));
            this.uiRoundMeter1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.uiRoundMeter1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiRoundMeter1.Location = new System.Drawing.Point(30, 136);
            this.uiRoundMeter1.Name = "uiRoundMeter1";
            this.uiRoundMeter1.Size = new System.Drawing.Size(150, 150);
            this.uiRoundMeter1.TabIndex = 47;
            this.uiRoundMeter1.Text = "uiRoundMeter1";
            // 
            // uiLine2
            // 
            this.uiLine2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine2.Location = new System.Drawing.Point(30, 292);
            this.uiLine2.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine2.Name = "uiLine2";
            this.uiLine2.Size = new System.Drawing.Size(319, 20);
            this.uiLine2.TabIndex = 53;
            this.uiLine2.Text = "UIBattery";
            this.uiLine2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiBattery1
            // 
            this.uiBattery1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiBattery1.Location = new System.Drawing.Point(30, 328);
            this.uiBattery1.Name = "uiBattery1";
            this.uiBattery1.Size = new System.Drawing.Size(48, 24);
            this.uiBattery1.TabIndex = 54;
            this.uiBattery1.Text = "uiBattery1";
            // 
            // uiBattery2
            // 
            this.uiBattery2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiBattery2.Location = new System.Drawing.Point(84, 328);
            this.uiBattery2.Name = "uiBattery2";
            this.uiBattery2.Power = 1;
            this.uiBattery2.Size = new System.Drawing.Size(48, 24);
            this.uiBattery2.TabIndex = 55;
            this.uiBattery2.Text = "uiBattery2";
            // 
            // uiBattery3
            // 
            this.uiBattery3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiBattery3.Location = new System.Drawing.Point(138, 328);
            this.uiBattery3.Name = "uiBattery3";
            this.uiBattery3.Power = 25;
            this.uiBattery3.Size = new System.Drawing.Size(48, 24);
            this.uiBattery3.TabIndex = 56;
            this.uiBattery3.Text = "uiBattery3";
            // 
            // uiBattery4
            // 
            this.uiBattery4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiBattery4.Location = new System.Drawing.Point(192, 328);
            this.uiBattery4.Name = "uiBattery4";
            this.uiBattery4.Power = 50;
            this.uiBattery4.Size = new System.Drawing.Size(48, 24);
            this.uiBattery4.TabIndex = 57;
            this.uiBattery4.Text = "uiBattery4";
            // 
            // uiBattery5
            // 
            this.uiBattery5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiBattery5.Location = new System.Drawing.Point(246, 328);
            this.uiBattery5.Name = "uiBattery5";
            this.uiBattery5.Power = 75;
            this.uiBattery5.Size = new System.Drawing.Size(48, 24);
            this.uiBattery5.TabIndex = 58;
            this.uiBattery5.Text = "uiBattery5";
            // 
            // uiBattery6
            // 
            this.uiBattery6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiBattery6.Location = new System.Drawing.Point(301, 328);
            this.uiBattery6.Name = "uiBattery6";
            this.uiBattery6.Size = new System.Drawing.Size(48, 24);
            this.uiBattery6.TabIndex = 59;
            this.uiBattery6.Text = "uiBattery6";
            // 
            // FMeter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "FMeter";
            this.Symbol = 61668;
            this.Text = "Meter";
            this.PagePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UIAnalogMeter uiAnalogMeter1;
        private UILine uiLine9;
        private UILine uiLine7;
        private UIRoundMeter uiRoundMeter2;
        private UILine uiLine6;
        private UIRoundMeter uiRoundMeter1;
        private UILine uiLine1;
        private UILedStopwatch uiLedStopwatch1;
        private UILedDisplay uiLedDisplay1;
        private System.Windows.Forms.Timer timer1;
        private UIBattery uiBattery6;
        private UIBattery uiBattery5;
        private UIBattery uiBattery4;
        private UIBattery uiBattery3;
        private UIBattery uiBattery2;
        private UIBattery uiBattery1;
        private UILine uiLine2;
    }
}