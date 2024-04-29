namespace Sunny.UI
{
    partial class UIMessageForm
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
            btnCancel = new UIButton();
            btnOK = new UIButton();
            lbMsg = new UIRichTextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.BackColor = System.Drawing.Color.Transparent;
            btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnCancel.Location = new System.Drawing.Point(224, 220);
            btnCancel.Margin = new System.Windows.Forms.Padding(0);
            btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(224, 48);
            btnCancel.Style = UIStyle.Custom;
            btnCancel.TabIndex = 6;
            btnCancel.Text = "取消";
            btnCancel.TipsText = null;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOK
            // 
            btnOK.BackColor = System.Drawing.Color.Transparent;
            btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            btnOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnOK.Location = new System.Drawing.Point(2, 220);
            btnOK.Margin = new System.Windows.Forms.Padding(0);
            btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(223, 48);
            btnOK.Style = UIStyle.Custom;
            btnOK.TabIndex = 5;
            btnOK.Text = "确定";
            btnOK.TipsText = null;
            btnOK.Click += btnOK_Click;
            // 
            // lbMsg
            // 
            lbMsg.BackColor = System.Drawing.Color.FromArgb(235, 243, 255);
            lbMsg.FillColor = System.Drawing.Color.White;
            lbMsg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lbMsg.Location = new System.Drawing.Point(14, 50);
            lbMsg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lbMsg.MinimumSize = new System.Drawing.Size(1, 1);
            lbMsg.Name = "lbMsg";
            lbMsg.Padding = new System.Windows.Forms.Padding(2);
            lbMsg.RadiusSides = UICornerRadiusSides.None;
            lbMsg.ReadOnly = true;
            lbMsg.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            lbMsg.ScrollBarStyleInherited = false;
            lbMsg.ShowText = false;
            lbMsg.Size = new System.Drawing.Size(422, 158);
            lbMsg.Style = UIStyle.Custom;
            lbMsg.TabIndex = 7;
            lbMsg.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // UIMessageForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(450, 270);
            Controls.Add(lbMsg);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            EscClose = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UIMessageForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "UIMsgBox";
            ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 450, 270);
            FormClosed += UIMessageForm_FormClosed;
            Shown += UIMessageForm_Shown;
            ResumeLayout(false);
        }

        #endregion

        private UIButton btnCancel;
        private UIButton btnOK;
        private UIRichTextBox lbMsg;
        private System.Windows.Forms.Timer timer1;
    }
}