namespace Sunny.UI
{
    partial class UISelectForm
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
            this.ComboBox = new Sunny.UI.UIComboBox();
            this.pnlBtm.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBtm
            // 
            this.pnlBtm.Location = new System.Drawing.Point(1, 124);
            this.pnlBtm.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.pnlBtm.Size = new System.Drawing.Size(471, 55);
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
            this.label.Size = new System.Drawing.Size(186, 21);
            this.label.TabIndex = 3;
            this.label.Text = "请在下方编辑框中选择：";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ComboBox
            // 
            this.ComboBox.DataSource = null;
            this.ComboBox.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.ComboBox.FillColor = System.Drawing.Color.White;
            this.ComboBox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ComboBox.Location = new System.Drawing.Point(29, 92);
            this.ComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ComboBox.MinimumSize = new System.Drawing.Size(63, 0);
            this.ComboBox.Name = "ComboBox";
            this.ComboBox.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.ComboBox.Size = new System.Drawing.Size(415, 29);
            this.ComboBox.TabIndex = 4;
            this.ComboBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UISelectForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(473, 182);
            this.Controls.Add(this.label);
            this.Controls.Add(this.ComboBox);
            this.EscClose = true;
            this.Name = "UISelectForm";
            this.Text = "选择";
            this.Controls.SetChildIndex(this.ComboBox, 0);
            this.Controls.SetChildIndex(this.pnlBtm, 0);
            this.Controls.SetChildIndex(this.label, 0);
            this.pnlBtm.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private UILabel label;
        private UIComboBox ComboBox;
    }
}