namespace Sunny.UI.Demo
{
    partial class FHeaderAsideMainFooter
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
            this.uiLogo1 = new Sunny.UI.UILogo();
            this.Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(178)))), ((int)(((byte)(181)))));
            this.Footer.Location = new System.Drawing.Point(250, 664);
            this.Footer.Size = new System.Drawing.Size(774, 56);
            this.Footer.Style = Sunny.UI.UIStyle.Custom;
            this.Footer.StyleCustomMode = true;
            // 
            // Aside
            // 
            this.Aside.Size = new System.Drawing.Size(250, 575);
            this.Aside.Style = Sunny.UI.UIStyle.Blue;
            this.Aside.MenuItemClick += new Sunny.UI.UINavMenu.OnMenuItemClick(this.Aside_MenuItemClick);
            // 
            // Header
            // 
            this.Header.Controls.Add(this.uiLogo1);
            this.Header.Size = new System.Drawing.Size(1024, 110);
            this.Header.Style = Sunny.UI.UIStyle.Blue;
            // 
            // uiLogo1
            // 
            this.uiLogo1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLogo1.Location = new System.Drawing.Point(15, 15);
            this.uiLogo1.MaximumSize = new System.Drawing.Size(300, 80);
            this.uiLogo1.MinimumSize = new System.Drawing.Size(300, 80);
            this.uiLogo1.Name = "uiLogo1";
            this.uiLogo1.Size = new System.Drawing.Size(300, 80);
            this.uiLogo1.TabIndex = 2;
            this.uiLogo1.Text = "uiLogo1";
            // 
            // FHeaderAsideMainFooter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1024, 720);
            this.Name = "FHeaderAsideMainFooter";
            this.Style = Sunny.UI.UIStyle.Blue;
            this.Text = "FHeaderAsideMainFooter";
            this.Header.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UILogo uiLogo1;
    }
}