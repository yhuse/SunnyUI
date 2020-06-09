namespace Sunny.UI
{
    partial class UITitlePage
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
            this.PageTitle = new Sunny.UI.UITitlePage.UITitle();
            this.PagePanel = new Sunny.UI.UIPanel();
            this.SuspendLayout();
            // 
            // PageTitle
            // 
            this.PageTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.PageTitle.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.PageTitle.Location = new System.Drawing.Point(0, 0);
            this.PageTitle.Name = "PageTitle";
            this.PageTitle.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.PageTitle.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.PageTitle.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.PageTitle.Size = new System.Drawing.Size(800, 35);
            this.PageTitle.Symbol = 0;
            this.PageTitle.SymbolSize = 24;
            this.PageTitle.TabIndex = 0;
            this.PageTitle.Text = "UITitlePage";
            this.PageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PagePanel
            // 
            this.PagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PagePanel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.PagePanel.Location = new System.Drawing.Point(0, 35);
            this.PagePanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PagePanel.Name = "PagePanel";
            this.PagePanel.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.PagePanel.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.PagePanel.Size = new System.Drawing.Size(800, 415);
            this.PagePanel.TabIndex = 1;
            this.PagePanel.Text = null;
            // 
            // UITitlePage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PagePanel);
            this.Controls.Add(this.PageTitle);
            this.Name = "UITitlePage";
            this.Text = "UITitlePage";
            this.ResumeLayout(false);

        }

        #endregion

        private UITitle PageTitle;
        protected UIPanel PagePanel;
    }
}