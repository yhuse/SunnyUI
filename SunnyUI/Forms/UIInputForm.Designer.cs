namespace Sunny.UI
{
    partial class UIInputForm
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
            label = new UILabel();
            edit = new UITextBox();
            pnlBtm.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBtm
            // 
            pnlBtm.Location = new System.Drawing.Point(0, 135);
            pnlBtm.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            pnlBtm.Size = new System.Drawing.Size(473, 55);
            pnlBtm.TabIndex = 2;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(343, 4);
            // 
            // btnOK
            // 
            btnOK.Location = new System.Drawing.Point(228, 4);
            // 
            // label
            // 
            label.AutoSize = true;
            label.BackColor = System.Drawing.Color.Transparent;
            label.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            label.Location = new System.Drawing.Point(28, 57);
            label.Name = "label";
            label.Size = new System.Drawing.Size(215, 16);
            label.TabIndex = 1;
            label.Text = "请在下方编辑框中输入数值：";
            label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edit
            // 
            edit.Cursor = System.Windows.Forms.Cursors.IBeam;
            edit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            edit.Location = new System.Drawing.Point(29, 92);
            edit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            edit.MinimumSize = new System.Drawing.Size(1, 1);
            edit.Name = "edit";
            edit.Padding = new System.Windows.Forms.Padding(5);
            edit.RadiusSides = UICornerRadiusSides.None;
            edit.ShowText = false;
            edit.Size = new System.Drawing.Size(415, 29);
            edit.TabIndex = 0;
            edit.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            edit.Watermark = "";
            // 
            // UIInputForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(473, 190);
            Controls.Add(edit);
            Controls.Add(label);
            Name = "UIInputForm";
            Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            ShowIcon = false;
            Text = "输入";
            ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 473, 182);
            Shown += UIInputForm_Shown;
            Controls.SetChildIndex(pnlBtm, 0);
            Controls.SetChildIndex(label, 0);
            Controls.SetChildIndex(edit, 0);
            pnlBtm.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private UILabel label;
        private UITextBox edit;
    }
}