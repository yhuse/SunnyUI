namespace Sunny.UI
{
    partial class UIEditForm
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
            this.pnlBtm = new Sunny.UI.UIPanel();
            this.btnCancel = new Sunny.UI.UISymbolButton();
            this.btnOK = new Sunny.UI.UISymbolButton();
            this.pnlBtm.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBtm
            // 
            this.pnlBtm.BackColor = System.Drawing.Color.Transparent;
            this.pnlBtm.Controls.Add(this.btnCancel);
            this.pnlBtm.Controls.Add(this.btnOK);
            this.pnlBtm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBtm.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pnlBtm.Location = new System.Drawing.Point(1, 392);
            this.pnlBtm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlBtm.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlBtm.Name = "pnlBtm";
            this.pnlBtm.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.pnlBtm.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
            this.pnlBtm.Size = new System.Drawing.Size(598, 55);
            this.pnlBtm.TabIndex = 1;
            this.pnlBtm.Text = null;
            this.pnlBtm.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.Location = new System.Drawing.Point(470, 12);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.btnCancel.ShowFocusColor = true;
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.Symbol = 61453;
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.TipsText = null;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOK.Location = new System.Drawing.Point(355, 12);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.btnOK.ShowFocusColor = true;
            this.btnOK.Size = new System.Drawing.Size(100, 35);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.TipsText = null;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // UIEditForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.pnlBtm);
            this.EscClose = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIEditForm";
            this.Padding = new System.Windows.Forms.Padding(1, 35, 1, 3);
            this.ShowInTaskbar = false;
            this.Text = "UIEditForm";
            this.pnlBtm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        protected UIPanel pnlBtm;
        protected UISymbolButton btnCancel;
        protected UISymbolButton btnOK;
    }
}