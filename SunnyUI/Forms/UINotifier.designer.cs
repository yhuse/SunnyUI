namespace Sunny.UI
{
    partial class UINotifier
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
                if (timerResetEvent != null)
                    timerResetEvent.Close();

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
            this.components = new System.ComponentModel.Container();
            this.noteContent = new System.Windows.Forms.Label();
            this.noteDate = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.PictureBox();
            this.buttonMenu = new System.Windows.Forms.PictureBox();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noteTitle = new System.Windows.Forms.Label();
            this.idLabel = new System.Windows.Forms.Label();
            this.icon = new Sunny.UI.UISymbolLabel();
            ((System.ComponentModel.ISupportInitialize)(this.buttonClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonMenu)).BeginInit();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // noteContent
            // 
            this.noteContent.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.noteContent.Image = global::Sunny.UI.Properties.Resources.notifier;
            this.noteContent.Location = new System.Drawing.Point(43, 30);
            this.noteContent.Name = "noteContent";
            this.noteContent.Size = new System.Drawing.Size(270, 73);
            this.noteContent.TabIndex = 3;
            this.noteContent.Text = "Description";
            this.noteContent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.noteContent.Click += new System.EventHandler(this.noteContent_Click);
            // 
            // noteDate
            // 
            this.noteDate.AutoSize = true;
            this.noteDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.noteDate.Image = global::Sunny.UI.Properties.Resources.notifier;
            this.noteDate.Location = new System.Drawing.Point(11, 97);
            this.noteDate.Name = "noteDate";
            this.noteDate.Size = new System.Drawing.Size(13, 9);
            this.noteDate.TabIndex = 4;
            this.noteDate.Text = "- -";
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(140)))), ((int)(((byte)(230)))));
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonClose.ForeColor = System.Drawing.Color.White;
            this.buttonClose.Location = new System.Drawing.Point(256, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(66, 24);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.TabStop = false;
            this.buttonClose.Text = " Calibrator";
            this.buttonClose.Click += new System.EventHandler(this.onCloseClick);
            this.buttonClose.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            // 
            // buttonMenu
            // 
            this.buttonMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(140)))), ((int)(((byte)(230)))));
            this.buttonMenu.BackgroundImage = global::Sunny.UI.Properties.Resources.menu;
            this.buttonMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonMenu.ContextMenuStrip = this.menu;
            this.buttonMenu.Location = new System.Drawing.Point(275, 2);
            this.buttonMenu.Name = "buttonMenu";
            this.buttonMenu.Size = new System.Drawing.Size(24, 24);
            this.buttonMenu.TabIndex = 5;
            this.buttonMenu.TabStop = false;
            this.buttonMenu.Click += new System.EventHandler(this.onMenuClick);
            // 
            // menu
            // 
            this.menu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeAllToolStripMenuItem});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(120, 26);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.closeAllToolStripMenuItem.Text = "Close All";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.onMenuCloseAllClick);
            // 
            // noteTitle
            // 
            this.noteTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(140)))), ((int)(((byte)(230)))));
            this.noteTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.noteTitle.ForeColor = System.Drawing.Color.White;
            this.noteTitle.Location = new System.Drawing.Point(2, 2);
            this.noteTitle.Name = "noteTitle";
            this.noteTitle.Size = new System.Drawing.Size(270, 24);
            this.noteTitle.TabIndex = 6;
            this.noteTitle.Text = "Note";
            this.noteTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.idLabel.Image = global::Sunny.UI.Properties.Resources.notifier;
            this.idLabel.Location = new System.Drawing.Point(296, 103);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(21, 9);
            this.idLabel.TabIndex = 7;
            this.idLabel.Text = "0000";
            this.idLabel.Visible = false;
            // 
            // icon
            // 
            this.icon.BackColor = System.Drawing.Color.White;
            this.icon.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.icon.Location = new System.Drawing.Point(10, 51);
            this.icon.MinimumSize = new System.Drawing.Size(1, 1);
            this.icon.Name = "icon";
            this.icon.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.icon.Size = new System.Drawing.Size(32, 30);
            this.icon.Style = Sunny.UI.UIStyle.Custom;
            this.icon.StyleCustomMode = true;
            this.icon.Symbol = 61528;
            this.icon.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(140)))), ((int)(((byte)(230)))));
            this.icon.SymbolSize = 36;
            this.icon.TabIndex = 8;
            this.icon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UINotifier
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Sunny.UI.Properties.Resources.notifier;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(324, 117);
            this.Controls.Add(this.icon);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.buttonMenu);
            this.Controls.Add(this.noteTitle);
            this.Controls.Add(this.noteDate);
            this.Controls.Add(this.noteContent);
            this.Controls.Add(this.buttonClose);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UINotifier";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Toast";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OnLoad);
            this.Shown += new System.EventHandler(this.UINotifier_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.buttonClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonMenu)).EndInit();
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox buttonClose;
        private System.Windows.Forms.PictureBox buttonMenu;
        private System.Windows.Forms.Label noteContent;
        private System.Windows.Forms.Label noteDate;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.Label noteTitle;
        private System.Windows.Forms.Label idLabel;
        private UISymbolLabel icon;
    }
}