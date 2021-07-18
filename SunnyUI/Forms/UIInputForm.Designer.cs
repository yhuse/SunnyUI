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
            this.label = new Sunny.UI.UILabel();
            this.edit = new Sunny.UI.UITextBox();
            this.pnlBtm.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBtm
            // 
            this.pnlBtm.Location = new System.Drawing.Point(1, 124);
            this.pnlBtm.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.pnlBtm.Size = new System.Drawing.Size(471, 55);
            this.pnlBtm.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(343, 12);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(228, 12);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.BackColor = System.Drawing.Color.Transparent;
            this.label.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label.Location = new System.Drawing.Point(29, 57);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(218, 21);
            this.label.TabIndex = 1;
            this.label.Text = "请在下方编辑框中输入数值：";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edit
            // 
            this.edit.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edit.FillColor = System.Drawing.Color.White;
            this.edit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.edit.Location = new System.Drawing.Point(29, 92);
            this.edit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edit.Maximum = 2147483647D;
            this.edit.Minimum = -2147483648D;
            this.edit.MinimumSize = new System.Drawing.Size(1, 1);
            this.edit.Name = "edit";
            this.edit.Padding = new System.Windows.Forms.Padding(5);
            this.edit.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.edit.Size = new System.Drawing.Size(415, 29);
            this.edit.TabIndex = 0;
            this.edit.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UIInputForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(473, 182);
            this.Controls.Add(this.edit);
            this.Controls.Add(this.label);
            this.Name = "UIInputForm";
            this.Text = "输入";
            this.Shown += new System.EventHandler(this.UIInputForm_Shown);
            this.Controls.SetChildIndex(this.pnlBtm, 0);
            this.Controls.SetChildIndex(this.label, 0);
            this.Controls.SetChildIndex(this.edit, 0);
            this.pnlBtm.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UILabel label;
        private UITextBox edit;
    }
}