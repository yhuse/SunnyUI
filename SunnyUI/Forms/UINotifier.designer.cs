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
            buttonMenu = new UISymbolButton();
            menu = new System.Windows.Forms.ContextMenuStrip(components);
            closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            noteTitle = new UIPanel();
            idLabel = new System.Windows.Forms.Label();
            icon = new UISymbolLabel();
            buttonClose = new UISymbolButton();
            menu.SuspendLayout();
            SuspendLayout();
            // 
            // noteContent
            // 
            noteContent.BackColor = System.Drawing.Color.Transparent;
            noteContent.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            noteContent.Location = new System.Drawing.Point(69, 48);
            noteContent.MaximumSize = new System.Drawing.Size(288, 100);
            noteContent.Name = "noteContent";
            noteContent.Size = new System.Drawing.Size(288, 100);
            noteContent.TabIndex = 3;
            noteContent.Text = "Desc";
            noteContent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            noteContent.Click += noteContent_Click;
            // 
            // noteDate
            // 
            noteDate.AutoSize = true;
            noteDate.Font = new System.Drawing.Font("宋体", 9F);
            noteDate.Image = Properties.Resources.notifier;
            noteDate.Location = new System.Drawing.Point(21, 168);
            noteDate.Name = "noteDate";
            noteDate.Size = new System.Drawing.Size(65, 12);
            noteDate.TabIndex = 4;
            noteDate.Text = "2024-01-01";
            // 
            // buttonMenu
            // 
            buttonMenu.BackColor = System.Drawing.Color.FromArgb(90, 140, 230);
            buttonMenu.BackgroundImage = Properties.Resources.menu;
            buttonMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            buttonMenu.ContextMenuStrip = menu;
            buttonMenu.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            buttonMenu.Location = new System.Drawing.Point(295, 2);
            buttonMenu.MinimumSize = new System.Drawing.Size(1, 1);
            buttonMenu.Name = "buttonMenu";
            buttonMenu.RadiusSides = UICornerRadiusSides.None;
            buttonMenu.Size = new System.Drawing.Size(40, 35);
            buttonMenu.Style = UIStyle.Custom;
            buttonMenu.Symbol = 361641;
            buttonMenu.SymbolOffset = new System.Drawing.Point(-1, 1);
            buttonMenu.TabIndex = 5;
            buttonMenu.TabStop = false;
            buttonMenu.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            buttonMenu.Click += onMenuClick;
            // 
            // menu
            // 
            menu.ImageScalingSize = new System.Drawing.Size(24, 24);
            menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { closeAllToolStripMenuItem });
            menu.Name = "menu";
            menu.Size = new System.Drawing.Size(148, 26);
            // 
            // closeAllToolStripMenuItem
            // 
            closeAllToolStripMenuItem.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            closeAllToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            closeAllToolStripMenuItem.Text = "Close All";
            closeAllToolStripMenuItem.Click += onMenuCloseAllClick;
            // 
            // noteTitle
            // 
            noteTitle.BackColor = System.Drawing.Color.FromArgb(90, 140, 230);
            noteTitle.FillColor = System.Drawing.Color.FromArgb(80, 160, 255);
            noteTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            noteTitle.ForeColor = System.Drawing.Color.White;
            noteTitle.Location = new System.Drawing.Point(2, 2);
            noteTitle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            noteTitle.MinimumSize = new System.Drawing.Size(1, 1);
            noteTitle.Name = "noteTitle";
            noteTitle.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            noteTitle.RadiusSides = UICornerRadiusSides.None;
            noteTitle.Size = new System.Drawing.Size(374, 35);
            noteTitle.Style = UIStyle.Custom;
            noteTitle.TabIndex = 6;
            noteTitle.Text = "Note";
            noteTitle.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Font = new System.Drawing.Font("宋体", 9F);
            idLabel.Image = Properties.Resources.notifier;
            idLabel.Location = new System.Drawing.Point(331, 168);
            idLabel.Name = "idLabel";
            idLabel.Size = new System.Drawing.Size(29, 12);
            idLabel.TabIndex = 7;
            idLabel.Text = "0000";
            idLabel.Visible = false;
            // 
            // icon
            // 
            icon.BackColor = System.Drawing.Color.White;
            icon.Font = new System.Drawing.Font("宋体", 12F);
            icon.Location = new System.Drawing.Point(13, 79);
            icon.MinimumSize = new System.Drawing.Size(1, 1);
            icon.Name = "icon";
            icon.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            icon.Size = new System.Drawing.Size(40, 40);
            icon.Style = UIStyle.Custom;
            icon.StyleCustomMode = true;
            icon.Symbol = 361528;
            icon.SymbolColor = System.Drawing.Color.FromArgb(90, 140, 230);
            icon.SymbolSize = 45;
            icon.TabIndex = 8;
            icon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonClose
            // 
            buttonClose.BackColor = System.Drawing.Color.FromArgb(90, 140, 230);
            buttonClose.Font = new System.Drawing.Font("宋体", 12F);
            buttonClose.Location = new System.Drawing.Point(335, 2);
            buttonClose.MinimumSize = new System.Drawing.Size(1, 1);
            buttonClose.Name = "buttonClose";
            buttonClose.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            buttonClose.RadiusSides = UICornerRadiusSides.None;
            buttonClose.Size = new System.Drawing.Size(40, 35);
            buttonClose.Style = UIStyle.Custom;
            buttonClose.StyleCustomMode = true;
            buttonClose.Symbol = 361453;
            buttonClose.SymbolOffset = new System.Drawing.Point(0, 1);
            buttonClose.SymbolSize = 26;
            buttonClose.TabIndex = 9;
            buttonClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonClose.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            buttonClose.Click += onCloseClick;
            // 
            // UINotifier
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.Color.White;
            BackgroundImage = Properties.Resources.notifier;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(378, 204);
            Controls.Add(buttonClose);
            Controls.Add(icon);
            Controls.Add(idLabel);
            Controls.Add(buttonMenu);
            Controls.Add(noteDate);
            Controls.Add(noteContent);
            Controls.Add(noteTitle);
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
            menu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Sunny.UI.UISymbolButton buttonMenu;
        private System.Windows.Forms.Label noteContent;
        private System.Windows.Forms.Label noteDate;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private Sunny.UI.UIPanel noteTitle;
        private System.Windows.Forms.Label idLabel;
        private UISymbolLabel icon;
        private Sunny.UI.UISymbolButton buttonClose;
    }
}