
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
            this.uiLine1 = new Sunny.UI.UILine();
            this.uiButton2 = new Sunny.UI.UIButton();
            this.uiButton1 = new Sunny.UI.UIButton();
            this.uiContextMenuStrip1 = new Sunny.UI.UIContextMenuStrip();
            this.SuspendLayout();
            // 
            // uiLine1
            // 
            this.uiLine1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLine1.Location = new System.Drawing.Point(30, 55);
            this.uiLine1.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(670, 20);
            this.uiLine1.TabIndex = 22;
            this.uiLine1.Text = "UIContextMenuStrip";
            this.uiLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiButton2
            // 
            this.uiButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton2.Font = new System.Drawing.Font("宋体", 12F);
            this.uiButton2.Location = new System.Drawing.Point(294, 92);
            this.uiButton2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton2.Name = "uiButton2";
            this.uiButton2.Size = new System.Drawing.Size(232, 35);
            this.uiButton2.TabIndex = 21;
            this.uiButton2.Text = "左键菜单";
            this.uiButton2.Click += new System.EventHandler(this.uiButton2_Click);
            // 
            // uiButton1
            // 
            this.uiButton1.ContextMenuStrip = this.uiContextMenuStrip1;
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiButton1.Location = new System.Drawing.Point(30, 92);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(232, 35);
            this.uiButton1.TabIndex = 20;
            this.uiButton1.Text = "右键菜单";
            // 
            // uiContextMenuStrip1
            // 
            this.uiContextMenuStrip1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiContextMenuStrip1.Name = "uiContextMenuStrip1";
            this.uiContextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // FContextMenuStrip1
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.uiLine1);
            this.Controls.Add(this.uiButton2);
            this.Controls.Add(this.uiButton1);
            this.Name = "FContextMenuStrip1";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.ShowTitle = true;
            this.Symbol = 62104;
            this.Text = "ContextMenuStrip";
            this.ResumeLayout(false);

        }

        #endregion

        private UILine uiLine1;
        private UIButton uiButton2;
        private UIButton uiButton1;
        private UIContextMenuStrip uiContextMenuStrip1;
    }
}