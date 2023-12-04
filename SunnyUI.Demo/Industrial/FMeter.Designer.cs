
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
            this.uiAnalogMeter1 = new Sunny.UI.UIAnalogMeter();
            this.uiLine9 = new Sunny.UI.UILine();
            this.uiRoundMeter2 = new Sunny.UI.UIRoundMeter();
            this.uiLine6 = new Sunny.UI.UILine();
            this.uiRoundMeter1 = new Sunny.UI.UIRoundMeter();
            this.uiRuler3 = new Sunny.UI.UIRuler();
            this.uiThermometer1 = new Sunny.UI.UIThermometer();
            this.uiLine1 = new Sunny.UI.UILine();
            this.uiRuler1 = new Sunny.UI.UIRuler();
            this.uiThermometer2 = new Sunny.UI.UIThermometer();
            this.SuspendLayout();
            // 
            // uiAnalogMeter1
            // 
            this.uiAnalogMeter1.BackColor = System.Drawing.Color.Transparent;
            this.uiAnalogMeter1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiAnalogMeter1.Location = new System.Drawing.Point(381, 96);
            this.uiAnalogMeter1.MaxValue = 100D;
            this.uiAnalogMeter1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiAnalogMeter1.MinValue = 0D;
            this.uiAnalogMeter1.Name = "uiAnalogMeter1";
            this.uiAnalogMeter1.Renderer = null;
            this.uiAnalogMeter1.Size = new System.Drawing.Size(140, 140);
            this.uiAnalogMeter1.TabIndex = 73;
            this.uiAnalogMeter1.Text = "uiAnalogMeter1";
            this.uiAnalogMeter1.Value = 0D;
            // 
            // uiLine9
            // 
            this.uiLine9.BackColor = System.Drawing.Color.Transparent;
            this.uiLine9.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLine9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLine9.Location = new System.Drawing.Point(381, 55);
            this.uiLine9.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine9.Name = "uiLine9";
            this.uiLine9.Size = new System.Drawing.Size(319, 20);
            this.uiLine9.TabIndex = 72;
            this.uiLine9.Text = "UIAnalogMeter";
            this.uiLine9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiRoundMeter2
            // 
            this.uiRoundMeter2.AngleImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter2.AngleImage")));
            this.uiRoundMeter2.BackColor = System.Drawing.Color.Transparent;
            this.uiRoundMeter2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter2.BackgroundImage")));
            this.uiRoundMeter2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.uiRoundMeter2.Font = new System.Drawing.Font("宋体", 12F);
            this.uiRoundMeter2.Location = new System.Drawing.Point(186, 91);
            this.uiRoundMeter2.MeterType = Sunny.UI.UIRoundMeter.TMeterType.Wind;
            this.uiRoundMeter2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRoundMeter2.Name = "uiRoundMeter2";
            this.uiRoundMeter2.Size = new System.Drawing.Size(140, 140);
            this.uiRoundMeter2.TabIndex = 71;
            this.uiRoundMeter2.Text = "uiRoundMeter2";
            // 
            // uiLine6
            // 
            this.uiLine6.BackColor = System.Drawing.Color.Transparent;
            this.uiLine6.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLine6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLine6.Location = new System.Drawing.Point(30, 55);
            this.uiLine6.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine6.Name = "uiLine6";
            this.uiLine6.Size = new System.Drawing.Size(319, 20);
            this.uiLine6.TabIndex = 70;
            this.uiLine6.Text = "UIRoundMeter";
            this.uiLine6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiRoundMeter1
            // 
            this.uiRoundMeter1.AngleImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter1.AngleImage")));
            this.uiRoundMeter1.BackColor = System.Drawing.Color.Transparent;
            this.uiRoundMeter1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter1.BackgroundImage")));
            this.uiRoundMeter1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.uiRoundMeter1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiRoundMeter1.Location = new System.Drawing.Point(30, 91);
            this.uiRoundMeter1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRoundMeter1.Name = "uiRoundMeter1";
            this.uiRoundMeter1.Size = new System.Drawing.Size(140, 140);
            this.uiRoundMeter1.TabIndex = 69;
            this.uiRoundMeter1.Text = "uiRoundMeter1";
            // 
            // uiRuler3
            // 
            this.uiRuler3.BackColor = System.Drawing.Color.Transparent;
            this.uiRuler3.Direction = Sunny.UI.UITrackBar.BarDirection.Vertical;
            this.uiRuler3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiRuler3.Location = new System.Drawing.Point(56, 287);
            this.uiRuler3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRuler3.MinorCount = 4;
            this.uiRuler3.Name = "uiRuler3";
            this.uiRuler3.Size = new System.Drawing.Size(35, 196);
            this.uiRuler3.TabIndex = 113;
            this.uiRuler3.Text = "uiRuler3";
            // 
            // uiThermometer1
            // 
            this.uiThermometer1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiThermometer1.Location = new System.Drawing.Point(78, 294);
            this.uiThermometer1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiThermometer1.Name = "uiThermometer1";
            this.uiThermometer1.Size = new System.Drawing.Size(49, 194);
            this.uiThermometer1.TabIndex = 112;
            this.uiThermometer1.Text = "uiThermometer1";
            this.uiThermometer1.Value = 30;
            // 
            // uiLine1
            // 
            this.uiLine1.BackColor = System.Drawing.Color.Transparent;
            this.uiLine1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLine1.LineColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiLine1.Location = new System.Drawing.Point(30, 261);
            this.uiLine1.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(319, 20);
            this.uiLine1.TabIndex = 111;
            this.uiLine1.Text = "UIThermometer";
            this.uiLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiRuler1
            // 
            this.uiRuler1.BackColor = System.Drawing.Color.Transparent;
            this.uiRuler1.Direction = Sunny.UI.UITrackBar.BarDirection.Vertical;
            this.uiRuler1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiRuler1.Location = new System.Drawing.Point(213, 287);
            this.uiRuler1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRuler1.MinorCount = 4;
            this.uiRuler1.Name = "uiRuler1";
            this.uiRuler1.Size = new System.Drawing.Size(35, 196);
            this.uiRuler1.StartValue = -20D;
            this.uiRuler1.StepCount = 6;
            this.uiRuler1.TabIndex = 115;
            this.uiRuler1.Text = "uiRuler1";
            // 
            // uiThermometer2
            // 
            this.uiThermometer2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiThermometer2.Location = new System.Drawing.Point(235, 294);
            this.uiThermometer2.Minimum = -20;
            this.uiThermometer2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiThermometer2.Name = "uiThermometer2";
            this.uiThermometer2.Size = new System.Drawing.Size(49, 194);
            this.uiThermometer2.TabIndex = 114;
            this.uiThermometer2.Text = "uiThermometer2";
            this.uiThermometer2.Value = 30;
            // 
            // FMeter
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 539);
            this.Controls.Add(this.uiRuler1);
            this.Controls.Add(this.uiThermometer2);
            this.Controls.Add(this.uiRuler3);
            this.Controls.Add(this.uiThermometer1);
            this.Controls.Add(this.uiLine1);
            this.Controls.Add(this.uiAnalogMeter1);
            this.Controls.Add(this.uiLine9);
            this.Controls.Add(this.uiLine6);
            this.Controls.Add(this.uiRoundMeter2);
            this.Controls.Add(this.uiRoundMeter1);
            this.Name = "FMeter";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.ShowTitle = true;
            this.Symbol = 61668;
            this.Text = "Meter";
            this.ResumeLayout(false);

        }

        #endregion
        private UIAnalogMeter uiAnalogMeter1;
        private UILine uiLine9;
        private UIRoundMeter uiRoundMeter2;
        private UILine uiLine6;
        private UIRoundMeter uiRoundMeter1;
        private UIRuler uiRuler3;
        private UIThermometer uiThermometer1;
        private UILine uiLine1;
        private UIRuler uiRuler1;
        private UIThermometer uiThermometer2;
    }
}