namespace Sunny.UI.Demo
{
    partial class FAsideHeaderMainFooter
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
            // Header
            // 
            this.Header.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.Header.ForeColor = System.Drawing.Color.White;
            this.Header.Size = new System.Drawing.Size(774, 57);
            this.Header.Style = Sunny.UI.UIStyle.Custom;
            this.Header.StyleCustomMode = true;
            // 
            // Aside
            // 
            this.Aside.Size = new System.Drawing.Size(250, 685);
            this.Aside.MenuItemClick += new Sunny.UI.UINavMenu.OnMenuItemClick(this.Aside_MenuItemClick);
            // 
            // FAsideHeaderMainFooter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1024, 720);
            this.Name = "FAsideHeaderMainFooter";
            this.Text = "FAsideHeaderMainFooter";
            this.ResumeLayout(false);

        }

        #endregion
    }
}