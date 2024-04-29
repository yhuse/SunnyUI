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
            pnlBtm = new UIPanel();
            btnCancel = new UISymbolButton();
            btnOK = new UISymbolButton();
            pnlBtm.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBtm
            // 
            pnlBtm.BackColor = System.Drawing.Color.Transparent;
            pnlBtm.Controls.Add(btnCancel);
            pnlBtm.Controls.Add(btnOK);
            pnlBtm.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlBtm.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            pnlBtm.Location = new System.Drawing.Point(1, 392);
            pnlBtm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pnlBtm.MinimumSize = new System.Drawing.Size(1, 1);
            pnlBtm.Name = "pnlBtm";
            pnlBtm.RadiusSides = UICornerRadiusSides.None;
            pnlBtm.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
            pnlBtm.Size = new System.Drawing.Size(598, 55);
            pnlBtm.TabIndex = 1;
            pnlBtm.Text = null;
            pnlBtm.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = System.Drawing.Color.Transparent;
            btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnCancel.Location = new System.Drawing.Point(470, 12);
            btnCancel.Margin = new System.Windows.Forms.Padding(0);
            btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            btnCancel.ShowFocusColor = true;
            btnCancel.Size = new System.Drawing.Size(100, 35);
            btnCancel.Symbol = 61453;
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnCancel.TipsText = null;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOK
            // 
            btnOK.BackColor = System.Drawing.Color.Transparent;
            btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            btnOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnOK.Location = new System.Drawing.Point(355, 12);
            btnOK.Margin = new System.Windows.Forms.Padding(0);
            btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            btnOK.ShowFocusColor = true;
            btnOK.Size = new System.Drawing.Size(100, 35);
            btnOK.TabIndex = 0;
            btnOK.Text = "确定";
            btnOK.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnOK.TipsText = null;
            btnOK.Click += btnOK_Click;
            // 
            // UIEditForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(600, 450);
            Controls.Add(pnlBtm);
            EscClose = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UIEditForm";
            Padding = new System.Windows.Forms.Padding(1, 35, 1, 3);
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "UIEditForm";
            ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 600, 450);
            pnlBtm.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        protected UIPanel pnlBtm;
        protected UISymbolButton btnCancel;
        protected UISymbolButton btnOK;
    }
}