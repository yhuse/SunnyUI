namespace Sunny.UI
{
    partial class UIAsideHeaderMainFooterFrame
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
            this.Footer = new Sunny.UI.UIPanel();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.Font = new System.Drawing.Font("宋体", 12F);
            this.Footer.Location = new System.Drawing.Point(250, 394);
            this.Footer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Footer.Name = "Footer";
            this.Footer.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.Footer.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.Footer.Size = new System.Drawing.Size(550, 56);
            this.Footer.TabIndex = 3;
            this.Footer.Text = null;
            // 
            // UIAsideHeaderMainFooterFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Footer);
            this.Name = "UIAsideHeaderMainFooterFrame";
            this.Text = "UIAsideHeaderMainFooterFrame";
            this.Controls.SetChildIndex(this.Aside, 0);
            this.Controls.SetChildIndex(this.Header, 0);
            this.Controls.SetChildIndex(this.Footer, 0);
            this.ResumeLayout(false);

        }

        #endregion

        protected UIPanel Footer;
    }
}