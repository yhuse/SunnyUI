
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
            this.uiLine6 = new Sunny.UI.UILine();
            this.uiRoundMeter2 = new Sunny.UI.UIRoundMeter();
            this.uiRoundMeter1 = new Sunny.UI.UIRoundMeter();
            this.uiMillisecondTimer1 = new Sunny.UI.UIMillisecondTimer(this.components);
            this.SuspendLayout();
            // 
            // uiAnalogMeter1
            // 
            this.uiAnalogMeter1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiAnalogMeter1.Location = new System.Drawing.Point(381, 96);
            this.uiAnalogMeter1.MaxValue = 100D;
            this.uiAnalogMeter1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiAnalogMeter1.MinValue = 0D;
            this.uiAnalogMeter1.Name = "uiAnalogMeter1";
            this.uiAnalogMeter1.Renderer = null;
            this.uiAnalogMeter1.Size = new System.Drawing.Size(140, 140);
            this.uiAnalogMeter1.TabIndex = 68;
            this.uiAnalogMeter1.Text = "uiAnalogMeter1";
            this.uiAnalogMeter1.Value = 0D;
            // 
            // uiLine9
            // 
            this.uiLine9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine9.Location = new System.Drawing.Point(381, 55);
            this.uiLine9.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine9.Name = "uiLine9";
            this.uiLine9.Size = new System.Drawing.Size(319, 20);
            this.uiLine9.TabIndex = 67;
            this.uiLine9.Text = "UIAnalogMeter";
            this.uiLine9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLine6
            // 
            this.uiLine6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine6.Location = new System.Drawing.Point(30, 55);
            this.uiLine6.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine6.Name = "uiLine6";
            this.uiLine6.Size = new System.Drawing.Size(319, 20);
            this.uiLine6.TabIndex = 64;
            this.uiLine6.Text = "UIRoundMeter";
            this.uiLine6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiRoundMeter2
            // 
            this.uiRoundMeter2.Angle = 0D;
            this.uiRoundMeter2.AngleImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter2.AngleImage")));
            this.uiRoundMeter2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter2.BackgroundImage")));
            this.uiRoundMeter2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.uiRoundMeter2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiRoundMeter2.Location = new System.Drawing.Point(186, 91);
            this.uiRoundMeter2.MeterType = Sunny.UI.UIRoundMeter.TMeterType.Wind;
            this.uiRoundMeter2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRoundMeter2.Name = "uiRoundMeter2";
            this.uiRoundMeter2.Size = new System.Drawing.Size(140, 140);
            this.uiRoundMeter2.TabIndex = 65;
            this.uiRoundMeter2.Text = "uiRoundMeter2";
            // 
            // uiRoundMeter1
            // 
            this.uiRoundMeter1.Angle = 0D;
            this.uiRoundMeter1.AngleImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter1.AngleImage")));
            this.uiRoundMeter1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter1.BackgroundImage")));
            this.uiRoundMeter1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.uiRoundMeter1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiRoundMeter1.Location = new System.Drawing.Point(30, 86);
            this.uiRoundMeter1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRoundMeter1.Name = "uiRoundMeter1";
            this.uiRoundMeter1.Size = new System.Drawing.Size(150, 150);
            this.uiRoundMeter1.TabIndex = 63;
            this.uiRoundMeter1.Text = "uiRoundMeter1";
            // 
            // uiMillisecondTimer1
            // 
            this.uiMillisecondTimer1.Interval = 100;
            this.uiMillisecondTimer1.TagString = null;
            this.uiMillisecondTimer1.Tick += new System.EventHandler(this.uiMillisecondTimer1_Tick);
            // 
            // FMeter
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.uiAnalogMeter1);
            this.Controls.Add(this.uiLine9);
            this.Controls.Add(this.uiRoundMeter2);
            this.Controls.Add(this.uiLine6);
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
        private UIMillisecondTimer uiMillisecondTimer1;
    }
}