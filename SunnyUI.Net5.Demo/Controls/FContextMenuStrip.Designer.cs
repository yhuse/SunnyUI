namespace Sunny.UI.Demo
{
    partial class FContextMenuStrip
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
            this.uiButton1 = new Sunny.UI.UIButton();
            this.uiContextMenuStrip1 = new Sunny.UI.UIContextMenuStrip();
            this.uiButton2 = new Sunny.UI.UIButton();
            this.uiLine1 = new Sunny.UI.UILine();
            this.PagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PagePanel
            // 
            this.PagePanel.Controls.Add(this.uiLine1);
            this.PagePanel.Controls.Add(this.uiButton2);
            this.PagePanel.Controls.Add(this.uiButton1);
            // 
            // uiButton1
            // 
            this.uiButton1.ContextMenuStrip = this.uiContextMenuStrip1;
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton1.Location = new System.Drawing.Point(30, 57);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(232, 35);
            this.uiButton1.TabIndex = 0;
            this.uiButton1.Text = "右键菜单";
            // 
            // uiContextMenuStrip1
            // 
            this.uiContextMenuStrip1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiContextMenuStrip1.Name = "uiContextMenuStrip1";
            this.uiContextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // uiButton2
            // 
            this.uiButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton2.Location = new System.Drawing.Point(294, 57);
            this.uiButton2.Name = "uiButton2";
            this.uiButton2.Size = new System.Drawing.Size(232, 35);
            this.uiButton2.TabIndex = 1;
            this.uiButton2.Text = "左键菜单";
            this.uiButton2.Click += new System.EventHandler(this.uiButton2_Click);
            // 
            // uiLine1
            // 
            this.uiLine1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine1.Location = new System.Drawing.Point(30, 20);
            this.uiLine1.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(670, 20);
            this.uiLine1.TabIndex = 19;
            this.uiLine1.Text = "UIContextMenuStrip";
            this.uiLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FContextMenuStrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "FContextMenuStrip";
            this.Symbol = 62104;
            this.Text = "ContextMenuStrip";
            this.Controls.SetChildIndex(this.PagePanel, 0);
            this.PagePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UIButton uiButton1;
        private UIButton uiButton2;
        private UILine uiLine1;
        private UIContextMenuStrip uiContextMenuStrip1;
    }
}