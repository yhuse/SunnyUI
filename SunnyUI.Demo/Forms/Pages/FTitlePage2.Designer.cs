namespace Sunny.UI.Demo
{
    partial class FTitlePage2
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
            this.uiSymbolLabel1 = new Sunny.UI.UISymbolLabel();
            this.SuspendLayout();
            // 
            // uiSymbolLabel1
            // 
            this.uiSymbolLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiSymbolLabel1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.uiSymbolLabel1.Location = new System.Drawing.Point(0, 35);
            this.uiSymbolLabel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolLabel1.Name = "uiSymbolLabel1";
            this.uiSymbolLabel1.Padding = new System.Windows.Forms.Padding(68, 0, 0, 0);
            this.uiSymbolLabel1.Size = new System.Drawing.Size(800, 415);
            this.uiSymbolLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolLabel1.StyleCustomMode = true;
            this.uiSymbolLabel1.Symbol = 61528;
            this.uiSymbolLabel1.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.uiSymbolLabel1.SymbolSize = 64;
            this.uiSymbolLabel1.TabIndex = 3;
            this.uiSymbolLabel1.Text = "This is page2 with title";
            // 
            // FTitlePage2
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.uiSymbolLabel1);
            this.Name = "FTitlePage2";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.PageIndex = 1002;
            this.ShowTitle = true;
            this.Text = "FPage2";
            this.ResumeLayout(false);

        }

        #endregion

        private UISymbolLabel uiSymbolLabel1;
    }
}