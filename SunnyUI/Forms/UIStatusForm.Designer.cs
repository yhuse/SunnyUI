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
            components = new System.ComponentModel.Container();
            labelDescription = new UILabel();
            processBar = new UIProcessBar();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // labelDescription
            // 
            labelDescription.AutoSize = true;
            labelDescription.BackColor = System.Drawing.Color.Transparent;
            labelDescription.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelDescription.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            labelDescription.Location = new System.Drawing.Point(32, 55);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new System.Drawing.Size(207, 16);
            labelDescription.TabIndex = 1;
            labelDescription.Text = "系统正在处理中，请稍候...";
            labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // processBar
            // 
            processBar.FillColor = System.Drawing.Color.FromArgb(235, 243, 255);
            processBar.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            processBar.Location = new System.Drawing.Point(32, 91);
            processBar.MinimumSize = new System.Drawing.Size(70, 23);
            processBar.Name = "processBar";
            processBar.RadiusSides = UICornerRadiusSides.None;
            processBar.Size = new System.Drawing.Size(409, 29);
            processBar.TabIndex = 3;
            processBar.Text = "0.0%";
            processBar.ValueChanged += processBar_ValueChanged;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // UIStatusForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(473, 153);
            ControlBox = false;
            Controls.Add(processBar);
            Controls.Add(labelDescription);
            IsForbidAltF4 = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UIStatusForm";
            ShowIcon = false;
            Text = "提示";
            TopMost = true;
            ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 473, 153);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private UILabel labelDescription;
        private UIProcessBar processBar;
        private System.Windows.Forms.Timer timer1;
    }
}