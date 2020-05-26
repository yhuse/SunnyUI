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
            this.SuspendLayout();
            // 
            // pnlBtm
            // 
            this.pnlBtm.Location = new System.Drawing.Point(1, 124);
            this.pnlBtm.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.pnlBtm.Size = new System.Drawing.Size(471, 55);
            this.pnlBtm.TabIndex = 2;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.BackColor = System.Drawing.Color.Transparent;
            this.label.Font = new System.Drawing.Font("微软雅黑", 12F);
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
            this.edit.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.edit.Location = new System.Drawing.Point(29, 92);
            this.edit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edit.Name = "edit";
            this.edit.Padding = new System.Windows.Forms.Padding(5);
            this.edit.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.edit.Size = new System.Drawing.Size(415, 29);
            this.edit.TabIndex = 0;
            // 
            // UIInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(473, 182);
            this.Controls.Add(this.edit);
            this.Controls.Add(this.label);
            this.Name = "UIInputForm";
            this.ShowInTaskbar = false;
            this.Text = "输入";
            this.Shown += new System.EventHandler(this.UIInputForm_Shown);
            this.Controls.SetChildIndex(this.pnlBtm, 0);
            this.Controls.SetChildIndex(this.label, 0);
            this.Controls.SetChildIndex(this.edit, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UILabel label;
        private UITextBox edit;
    }
}