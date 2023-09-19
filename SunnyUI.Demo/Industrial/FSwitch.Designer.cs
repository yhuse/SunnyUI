namespace Sunny.UI.Demo
{
    partial class FSwitch
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
            this.uiLine1 = new Sunny.UI.UILine();
            this.uiTurnSwitch1 = new Sunny.UI.UITurnSwitch();
            this.uiTurnSwitch2 = new Sunny.UI.UITurnSwitch();
            this.SuspendLayout();
            // 
            // uiLine1
            // 
            this.uiLine1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLine1.Location = new System.Drawing.Point(30, 55);
            this.uiLine1.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(670, 20);
            this.uiLine1.TabIndex = 78;
            this.uiLine1.Text = "UITurnSwitch";
            this.uiLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiTurnSwitch1
            // 
            this.uiTurnSwitch1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTurnSwitch1.Location = new System.Drawing.Point(33, 96);
            this.uiTurnSwitch1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTurnSwitch1.Name = "uiTurnSwitch1";
            this.uiTurnSwitch1.Size = new System.Drawing.Size(160, 160);
            this.uiTurnSwitch1.TabIndex = 79;
            this.uiTurnSwitch1.Text = "uiTurnSwitch1";
            this.uiTurnSwitch1.ValueChanged += new Sunny.UI.UITurnSwitch.OnValueChanged(this.uiTurnSwitch1_ValueChanged);
            // 
            // uiTurnSwitch2
            // 
            this.uiTurnSwitch2.ActiveAngle = 0;
            this.uiTurnSwitch2.BackInnerSize = 60;
            this.uiTurnSwitch2.BackSize = 80;
            this.uiTurnSwitch2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTurnSwitch2.InActiveAngle = 90;
            this.uiTurnSwitch2.InActiveColor = System.Drawing.Color.Fuchsia;
            this.uiTurnSwitch2.Location = new System.Drawing.Point(216, 96);
            this.uiTurnSwitch2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTurnSwitch2.Name = "uiTurnSwitch2";
            this.uiTurnSwitch2.Size = new System.Drawing.Size(160, 160);
            this.uiTurnSwitch2.TabIndex = 80;
            this.uiTurnSwitch2.Text = "uiTurnSwitch2";
            // 
            // FSwitch
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.uiTurnSwitch2);
            this.Controls.Add(this.uiTurnSwitch1);
            this.Controls.Add(this.uiLine1);
            this.Name = "FSwitch";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.ShowTitle = true;
            this.Symbol = 361956;
            this.Text = "Switch";
            this.ResumeLayout(false);

        }

        #endregion

        private UILine uiLine1;
        private UITurnSwitch uiTurnSwitch1;
        private UITurnSwitch uiTurnSwitch2;
    }
}