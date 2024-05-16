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
            components = new System.ComponentModel.Container();
            noteContent = new System.Windows.Forms.Label();
            noteDate = new System.Windows.Forms.Label();
            buttonClose = new System.Windows.Forms.PictureBox();
            buttonMenu = new System.Windows.Forms.PictureBox();
            menu = new System.Windows.Forms.ContextMenuStrip(components);
            closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            noteTitle = new System.Windows.Forms.Label();
            idLabel = new System.Windows.Forms.Label();
            icon = new UISymbolLabel();
            ((System.ComponentModel.ISupportInitialize)buttonClose).BeginInit();
            ((System.ComponentModel.ISupportInitialize)buttonMenu).BeginInit();
            menu.SuspendLayout();
            SuspendLayout();
            // 
            // noteContent
            // 
            noteContent.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            noteContent.Image = Properties.Resources.notifier;
            noteContent.Location = new System.Drawing.Point(43, 30);
            noteContent.Name = "noteContent";
            noteContent.Size = new System.Drawing.Size(270, 73);
            noteContent.TabIndex = 3;
            noteContent.Text = "Description";
            noteContent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            noteContent.Click += noteContent_Click;
            // 
            // noteDate
            // 
            noteDate.AutoSize = true;
            noteDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            noteDate.Image = Properties.Resources.notifier;
            noteDate.Location = new System.Drawing.Point(11, 97);
            noteDate.Name = "noteDate";
            noteDate.Size = new System.Drawing.Size(13, 9);
            noteDate.TabIndex = 4;
            noteDate.Text = "- -";
            // 
            // buttonClose
            // 
            buttonClose.BackColor = System.Drawing.Color.FromArgb(90, 140, 230);
            buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            buttonClose.ForeColor = System.Drawing.Color.White;
            buttonClose.Location = new System.Drawing.Point(256, 2);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new System.Drawing.Size(66, 24);
            buttonClose.TabIndex = 1;
            buttonClose.TabStop = false;
            buttonClose.Text = " Calibrator";
            buttonClose.Click += onCloseClick;
            buttonClose.Paint += OnPaint;
            // 
            // buttonMenu
            // 
            buttonMenu.BackColor = System.Drawing.Color.FromArgb(90, 140, 230);
            buttonMenu.BackgroundImage = Properties.Resources.menu;
            buttonMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            buttonMenu.ContextMenuStrip = menu;
            buttonMenu.Location = new System.Drawing.Point(275, 2);
            buttonMenu.Name = "buttonMenu";
            buttonMenu.Size = new System.Drawing.Size(24, 24);
            buttonMenu.TabIndex = 5;
            buttonMenu.TabStop = false;
            buttonMenu.Click += onMenuClick;
            // 
            // menu
            // 
            menu.ImageScalingSize = new System.Drawing.Size(24, 24);
            menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { closeAllToolStripMenuItem });
            menu.Name = "menu";
            menu.Size = new System.Drawing.Size(120, 26);
            // 
            // closeAllToolStripMenuItem
            // 
            closeAllToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            closeAllToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            closeAllToolStripMenuItem.Text = "Close All";
            closeAllToolStripMenuItem.Click += onMenuCloseAllClick;
            // 
            // noteTitle
            // 
            noteTitle.BackColor = System.Drawing.Color.FromArgb(90, 140, 230);
            noteTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            noteTitle.ForeColor = System.Drawing.Color.White;
            noteTitle.Location = new System.Drawing.Point(2, 2);
            noteTitle.Name = "noteTitle";
            noteTitle.Size = new System.Drawing.Size(270, 24);
            noteTitle.TabIndex = 6;
            noteTitle.Text = "Note";
            noteTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            idLabel.Image = Properties.Resources.notifier;
            idLabel.Location = new System.Drawing.Point(296, 103);
            idLabel.Name = "idLabel";
            idLabel.Size = new System.Drawing.Size(21, 9);
            idLabel.TabIndex = 7;
            idLabel.Text = "0000";
            idLabel.Visible = false;
            // 
            // icon
            // 
            icon.BackColor = System.Drawing.Color.White;
            icon.Font = new System.Drawing.Font("宋体", 12F);
            icon.Location = new System.Drawing.Point(10, 51);
            icon.MinimumSize = new System.Drawing.Size(1, 1);
            icon.Name = "icon";
            icon.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            icon.Size = new System.Drawing.Size(32, 30);
            icon.Style = UIStyle.Custom;
            icon.StyleCustomMode = true;
            icon.Symbol = 361528;
            icon.SymbolColor = System.Drawing.Color.FromArgb(90, 140, 230);
            icon.SymbolSize = 36;
            icon.TabIndex = 8;
            icon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UINotifier
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.Color.White;
            BackgroundImage = Properties.Resources.notifier;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(324, 117);
            Controls.Add(icon);
            Controls.Add(idLabel);
            Controls.Add(buttonMenu);
            Controls.Add(noteTitle);
            Controls.Add(noteDate);
            Controls.Add(noteContent);
            Controls.Add(buttonClose);
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "UINotifier";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Toast";
            TopMost = true;
            Load += OnLoad;
            Shown += UINotifier_Shown;
            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;
            ((System.ComponentModel.ISupportInitialize)buttonClose).EndInit();
            ((System.ComponentModel.ISupportInitialize)buttonMenu).EndInit();
            menu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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