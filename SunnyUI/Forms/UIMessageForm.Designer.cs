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
            this.btnCancel = new Sunny.UI.UIButton();
            this.btnOK = new Sunny.UI.UIButton();
            this.lbMsg = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.Location = new System.Drawing.Point(224, 220);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.btnCancel.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)));
            this.btnCancel.Size = new System.Drawing.Size(224, 48);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.TipsText = null;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.btnOK_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.btnOK_MouseLeave);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOK.Location = new System.Drawing.Point(2, 220);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.btnOK.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.btnOK.Size = new System.Drawing.Size(223, 48);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            this.btnOK.TipsText = null;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnOK.MouseEnter += new System.EventHandler(this.btnOK_MouseEnter);
            this.btnOK.MouseLeave += new System.EventHandler(this.btnOK_MouseLeave);
            // 
            // lbMsg
            // 
            this.lbMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.lbMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbMsg.Location = new System.Drawing.Point(14, 50);
            this.lbMsg.Name = "lbMsg";
            this.lbMsg.ReadOnly = true;
            this.lbMsg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.lbMsg.Size = new System.Drawing.Size(422, 158);
            this.lbMsg.TabIndex = 7;
            this.lbMsg.Text = "";
            // 
            // UIMessageForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(450, 270);
            this.Controls.Add(this.lbMsg);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.EscClose = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIMessageForm";
            this.Padding = new System.Windows.Forms.Padding(1, 35, 1, 3);
            this.ShowInTaskbar = false;
            this.Text = "UIMsgBox";
            this.ResumeLayout(false);

        }

        #endregion

        private UIButton btnCancel;
        private UIButton btnOK;
        private System.Windows.Forms.RichTextBox lbMsg;
    }
}