namespace Sunny.UI
{
    partial class UIStatusForm
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
            this.labelDescription = new Sunny.UI.UILabel();
            this.processBar = new Sunny.UI.UIProcessBar();
            this.SuspendLayout();
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.BackColor = System.Drawing.Color.Transparent;
            this.labelDescription.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.labelDescription.Location = new System.Drawing.Point(32, 55);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(178, 21);
            this.labelDescription.TabIndex = 1;
            this.labelDescription.Text = "系统正在处理中，请稍候...";
            this.labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // processBar
            // 
            this.processBar.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.processBar.Location = new System.Drawing.Point(32, 91);
            this.processBar.MinimumSize = new System.Drawing.Size(70, 23);
            this.processBar.Name = "processBar";
            this.processBar.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.processBar.Size = new System.Drawing.Size(409, 29);
            this.processBar.TabIndex = 3;
            this.processBar.Text = "0.0%";
            this.processBar.ValueChanged += new Sunny.UI.UIProcessBar.OnValueChanged(this.processBar_ValueChanged);
            // 
            // UIStatusForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(473, 153);
            this.ControlBox = false;
            this.Controls.Add(this.processBar);
            this.Controls.Add(this.labelDescription);
            this.IsForbidAltF4 = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIStatusForm";
            this.Text = "提示";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private UILabel labelDescription;
        private UIProcessBar processBar;
    }
}