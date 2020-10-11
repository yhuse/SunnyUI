namespace Sunny.UI.Demo
{
    partial class FListBox
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
            this.uiImageListBox1 = new Sunny.UI.UIImageListBox();
            this.uiLine1 = new Sunny.UI.UILine();
            this.uiLine2 = new Sunny.UI.UILine();
            this.uiListBox1 = new Sunny.UI.UIListBox();
            this.uiCheckBox1 = new Sunny.UI.UICheckBox();
            this.uiButton1 = new Sunny.UI.UIButton();
            this.PagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PagePanel
            // 
            this.PagePanel.Controls.Add(this.uiButton1);
            this.PagePanel.Controls.Add(this.uiCheckBox1);
            this.PagePanel.Controls.Add(this.uiListBox1);
            this.PagePanel.Controls.Add(this.uiLine2);
            this.PagePanel.Controls.Add(this.uiLine1);
            this.PagePanel.Controls.Add(this.uiImageListBox1);
            this.PagePanel.Size = new System.Drawing.Size(800, 465);
            // 
            // uiImageListBox1
            // 
            this.uiImageListBox1.FillColor = System.Drawing.Color.White;
            this.uiImageListBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiImageListBox1.ItemHeight = 80;
            this.uiImageListBox1.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiImageListBox1.Location = new System.Drawing.Point(355, 48);
            this.uiImageListBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiImageListBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiImageListBox1.Name = "uiImageListBox1";
            this.uiImageListBox1.Padding = new System.Windows.Forms.Padding(2);
            this.uiImageListBox1.Size = new System.Drawing.Size(266, 343);
            this.uiImageListBox1.TabIndex = 1;
            this.uiImageListBox1.Text = "uiImageListBox1";
            this.uiImageListBox1.ItemDoubleClick += new System.EventHandler(this.uiImageListBox1_ItemDoubleClick);
            // 
            // uiLine1
            // 
            this.uiLine1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine1.Location = new System.Drawing.Point(33, 20);
            this.uiLine1.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(266, 18);
            this.uiLine1.TabIndex = 19;
            this.uiLine1.Text = "uiListBox";
            this.uiLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLine2
            // 
            this.uiLine2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine2.Location = new System.Drawing.Point(355, 20);
            this.uiLine2.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine2.Name = "uiLine2";
            this.uiLine2.Size = new System.Drawing.Size(266, 18);
            this.uiLine2.TabIndex = 20;
            this.uiLine2.Text = "uiImageListBox";
            this.uiLine2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiListBox1
            // 
            this.uiListBox1.FillColor = System.Drawing.Color.White;
            this.uiListBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiListBox1.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiListBox1.Location = new System.Drawing.Point(33, 48);
            this.uiListBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiListBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiListBox1.Name = "uiListBox1";
            this.uiListBox1.Padding = new System.Windows.Forms.Padding(2);
            this.uiListBox1.Size = new System.Drawing.Size(270, 343);
            this.uiListBox1.TabIndex = 21;
            this.uiListBox1.Text = "uiListBox1";
            this.uiListBox1.ItemDoubleClick += new System.EventHandler(this.uiListBox1_ItemDoubleClick);
            // 
            // uiCheckBox1
            // 
            this.uiCheckBox1.Checked = true;
            this.uiCheckBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiCheckBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiCheckBox1.Location = new System.Drawing.Point(355, 399);
            this.uiCheckBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiCheckBox1.Name = "uiCheckBox1";
            this.uiCheckBox1.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.uiCheckBox1.Size = new System.Drawing.Size(266, 35);
            this.uiCheckBox1.TabIndex = 22;
            this.uiCheckBox1.Text = "ShowDescription";
            this.uiCheckBox1.ValueChanged += new Sunny.UI.UICheckBox.OnValueChanged(this.uiCheckBox1_ValueChanged);
            // 
            // uiButton1
            // 
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton1.Location = new System.Drawing.Point(33, 400);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(100, 35);
            this.uiButton1.TabIndex = 23;
            this.uiButton1.Text = "Add Item";
            this.uiButton1.Click += new System.EventHandler(this.uiButton1_Click);
            this.uiButton1.DoubleClick += new System.EventHandler(this.uiButton1_DoubleClick);
            // 
            // FListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Name = "FListBox";
            this.Symbol = 61474;
            this.Text = "ListBox";
            this.PagePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private UIImageListBox uiImageListBox1;
        private UILine uiLine2;
        private UILine uiLine1;
        private UIListBox uiListBox1;
        private UICheckBox uiCheckBox1;
        private UIButton uiButton1;
    }
}