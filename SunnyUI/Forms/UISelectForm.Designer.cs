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
            label = new UILabel();
            ComboBox = new UIComboBox();
            pnlBtm.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBtm
            // 
            pnlBtm.Location = new System.Drawing.Point(1, 134);
            pnlBtm.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            pnlBtm.Size = new System.Drawing.Size(471, 55);
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
            label.Location = new System.Drawing.Point(29, 57);
            label.Name = "label";
            label.Size = new System.Drawing.Size(183, 16);
            label.TabIndex = 3;
            label.Text = "请在下方编辑框中选择：";
            label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ComboBox
            // 
            ComboBox.DataSource = null;
            ComboBox.DropDownStyle = UIDropDownStyle.DropDownList;
            ComboBox.FillColor = System.Drawing.Color.White;
            ComboBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ComboBox.ItemHoverColor = System.Drawing.Color.FromArgb(155, 200, 255);
            ComboBox.ItemSelectForeColor = System.Drawing.Color.FromArgb(235, 243, 255);
            ComboBox.Location = new System.Drawing.Point(29, 92);
            ComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            ComboBox.MinimumSize = new System.Drawing.Size(63, 0);
            ComboBox.Name = "ComboBox";
            ComboBox.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            ComboBox.Size = new System.Drawing.Size(415, 29);
            ComboBox.SymbolSize = 24;
            ComboBox.TabIndex = 4;
            ComboBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            ComboBox.Watermark = "";
            // 
            // UISelectForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(473, 190);
            Controls.Add(label);
            Controls.Add(ComboBox);
            Name = "UISelectForm";
            Padding = new System.Windows.Forms.Padding(1, 35, 1, 1);
            ShowIcon = false;
            Text = "选择";
            ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 473, 182);
            Controls.SetChildIndex(ComboBox, 0);
            Controls.SetChildIndex(pnlBtm, 0);
            Controls.SetChildIndex(label, 0);
            pnlBtm.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private UILabel label;
        private UIComboBox ComboBox;
    }
}