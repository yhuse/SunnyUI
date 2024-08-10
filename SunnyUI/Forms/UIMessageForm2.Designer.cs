namespace Sunny.UI
{
    partial class UIMessageForm2
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
            components = new System.ComponentModel.Container();
            btnCancel = new UISymbolButton();
            btnOK = new UISymbolButton();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnCancel.BackColor = System.Drawing.Color.Transparent;
            btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Font = new System.Drawing.Font("宋体", 12F);
            btnCancel.Location = new System.Drawing.Point(378, 164);
            btnCancel.Margin = new System.Windows.Forms.Padding(0);
            btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            btnCancel.ShowFocusLine = true;
            btnCancel.Size = new System.Drawing.Size(100, 35);
            btnCancel.Symbol = 361453;
            btnCancel.TabIndex = 10;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            btnCancel.TipsText = null;
            // 
            // btnOK
            // 
            btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnOK.BackColor = System.Drawing.Color.Transparent;
            btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnOK.Font = new System.Drawing.Font("宋体", 12F);
            btnOK.Location = new System.Drawing.Point(263, 164);
            btnOK.Margin = new System.Windows.Forms.Padding(0);
            btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            btnOK.ShowFocusLine = true;
            btnOK.Size = new System.Drawing.Size(100, 35);
            btnOK.TabIndex = 9;
            btnOK.Text = "确定";
            btnOK.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            btnOK.TipsText = null;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // UIMessageForm2
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(516, 220);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            EscClose = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UIMessageForm2";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            Text = "UIMessageForm2";
            ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 500, 180);
            FormClosed += UIMessageForm2_FormClosed;
            Shown += UIMessageForm2_Shown;
            Paint += UIMessageForm2_Paint;
            ResumeLayout(false);
        }

        #endregion
        protected UISymbolButton btnCancel;
        protected UISymbolButton btnOK;
        private System.Windows.Forms.Timer timer1;
    }
}