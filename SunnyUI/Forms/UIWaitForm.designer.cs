namespace Sunny.UI
{
    partial class UIWaitForm
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
            labelDescription = new UILabel();
            Bar = new UIWaitingBar();
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
            labelDescription.TabIndex = 4;
            labelDescription.Text = "系统正在处理中，请稍候...";
            labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Bar
            // 
            Bar.FillColor = System.Drawing.Color.FromArgb(243, 249, 255);
            Bar.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Bar.ForeColor = System.Drawing.Color.FromArgb(80, 160, 255);
            Bar.Location = new System.Drawing.Point(32, 91);
            Bar.MinimumSize = new System.Drawing.Size(70, 23);
            Bar.Name = "Bar";
            Bar.Size = new System.Drawing.Size(409, 29);
            Bar.TabIndex = 6;
            Bar.Text = "uiWaitingBar1";
            Bar.Tick += Bar_Tick;
            // 
            // UIWaitForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(473, 153);
            ControlBox = false;
            Controls.Add(labelDescription);
            Controls.Add(Bar);
            IsForbidAltF4 = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UIWaitForm";
            ShowIcon = false;
            Text = "提示";
            TopMost = true;
            ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 473, 153);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private UILabel labelDescription;
        private UIWaitingBar Bar;
    }
}