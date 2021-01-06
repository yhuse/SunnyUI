namespace Sunny.UI.Demo
{
    partial class FAvatar
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
            this.uiAvatar1 = new Sunny.UI.UIAvatar();
            this.uiAvatar2 = new Sunny.UI.UIAvatar();
            this.uiAvatar3 = new Sunny.UI.UIAvatar();
            this.uiAvatar4 = new Sunny.UI.UIAvatar();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.uiContextMenuStrip1 = new Sunny.UI.UIContextMenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更改密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uiLine1 = new Sunny.UI.UILine();
            this.PagePanel.SuspendLayout();
            this.uiContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PagePanel
            // 
            this.PagePanel.Controls.Add(this.uiLine1);
            this.PagePanel.Controls.Add(this.uiLabel4);
            this.PagePanel.Controls.Add(this.uiLabel3);
            this.PagePanel.Controls.Add(this.uiLabel2);
            this.PagePanel.Controls.Add(this.uiLabel1);
            this.PagePanel.Controls.Add(this.uiAvatar4);
            this.PagePanel.Controls.Add(this.uiAvatar3);
            this.PagePanel.Controls.Add(this.uiAvatar2);
            this.PagePanel.Controls.Add(this.uiAvatar1);
            this.PagePanel.Text = "";
            // 
            // uiAvatar1
            // 
            this.uiAvatar1.AvatarSize = 55;
            this.uiAvatar1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiAvatar1.Location = new System.Drawing.Point(102, 50);
            this.uiAvatar1.Name = "uiAvatar1";
            this.uiAvatar1.Size = new System.Drawing.Size(60, 60);
            this.uiAvatar1.SymbolSize = 48;
            this.uiAvatar1.TabIndex = 0;
            this.uiAvatar1.Text = "uiAvatar1";
            // 
            // uiAvatar2
            // 
            this.uiAvatar2.AvatarSize = 55;
            this.uiAvatar2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiAvatar2.Icon = Sunny.UI.UIAvatar.UIIcon.Image;
            this.uiAvatar2.Image = global::Sunny.UI.Demo.Properties.Resources.SunnyUISmall;
            this.uiAvatar2.Location = new System.Drawing.Point(30, 50);
            this.uiAvatar2.Name = "uiAvatar2";
            this.uiAvatar2.Size = new System.Drawing.Size(60, 60);
            this.uiAvatar2.TabIndex = 1;
            this.uiAvatar2.Text = "uiAvatar2";
            // 
            // uiAvatar3
            // 
            this.uiAvatar3.AvatarSize = 55;
            this.uiAvatar3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiAvatar3.Icon = Sunny.UI.UIAvatar.UIIcon.Text;
            this.uiAvatar3.Location = new System.Drawing.Point(174, 50);
            this.uiAvatar3.Name = "uiAvatar3";
            this.uiAvatar3.Size = new System.Drawing.Size(60, 60);
            this.uiAvatar3.TabIndex = 2;
            this.uiAvatar3.Text = "Avatar";
            // 
            // uiAvatar4
            // 
            this.uiAvatar4.AvatarSize = 55;
            this.uiAvatar4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiAvatar4.Location = new System.Drawing.Point(362, 50);
            this.uiAvatar4.Name = "uiAvatar4";
            this.uiAvatar4.Size = new System.Drawing.Size(60, 60);
            this.uiAvatar4.Symbol = 61715;
            this.uiAvatar4.TabIndex = 3;
            this.uiAvatar4.Text = "uiAvatar4";
            this.uiAvatar4.Click += new System.EventHandler(this.uiAvatar4_Click);
            // 
            // uiLabel1
            // 
            this.uiLabel1.AutoSize = true;
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel1.Location = new System.Drawing.Point(39, 117);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(42, 21);
            this.uiLabel1.TabIndex = 4;
            this.uiLabel1.Text = "图片";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel2
            // 
            this.uiLabel2.AutoSize = true;
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel2.Location = new System.Drawing.Point(111, 117);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(42, 21);
            this.uiLabel2.TabIndex = 5;
            this.uiLabel2.Text = "图标";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel3
            // 
            this.uiLabel3.AutoSize = true;
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel3.Location = new System.Drawing.Point(183, 117);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(42, 21);
            this.uiLabel3.TabIndex = 6;
            this.uiLabel3.Text = "文字";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel4
            // 
            this.uiLabel4.AutoSize = true;
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel4.Location = new System.Drawing.Point(355, 117);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(74, 21);
            this.uiLabel4.TabIndex = 7;
            this.uiLabel4.Text = "左键菜单";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiContextMenuStrip1
            // 
            this.uiContextMenuStrip1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.更改密码ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.退出ToolStripMenuItem});
            this.uiContextMenuStrip1.Name = "uiContextMenuStrip1";
            this.uiContextMenuStrip1.Size = new System.Drawing.Size(113, 88);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(112, 26);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 更改密码ToolStripMenuItem
            // 
            this.更改密码ToolStripMenuItem.Name = "更改密码ToolStripMenuItem";
            this.更改密码ToolStripMenuItem.Size = new System.Drawing.Size(112, 26);
            this.更改密码ToolStripMenuItem.Text = "密码";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(109, 6);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(112, 26);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // uiLine1
            // 
            this.uiLine1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine1.Location = new System.Drawing.Point(30, 20);
            this.uiLine1.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(670, 20);
            this.uiLine1.TabIndex = 19;
            this.uiLine1.Text = "UIAvatar";
            this.uiLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FAvatar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "FAvatar";
            this.Symbol = 61447;
            this.Text = "Avatar";
            this.Controls.SetChildIndex(this.PagePanel, 0);
            this.PagePanel.ResumeLayout(false);
            this.PagePanel.PerformLayout();
            this.uiContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UILabel uiLabel4;
        private UILabel uiLabel3;
        private UILabel uiLabel2;
        private UILabel uiLabel1;
        private UIAvatar uiAvatar4;
        private UIAvatar uiAvatar3;
        private UIAvatar uiAvatar2;
        private UIAvatar uiAvatar1;
        private UIContextMenuStrip uiContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更改密码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private UILine uiLine1;
    }
}