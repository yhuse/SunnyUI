namespace Sunny.UI.Demo
{
    partial class FEditor
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
            this.uiLine2 = new Sunny.UI.UILine();
            this.uiSymbolButton1 = new Sunny.UI.UISymbolButton();
            this.btnAdd = new Sunny.UI.UISymbolButton();
            this.uiLine1 = new Sunny.UI.UILine();
            this.btnEdit = new Sunny.UI.UISymbolButton();
            this.SuspendLayout();
            // 
            // uiLine2
            // 
            this.uiLine2.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLine2.Location = new System.Drawing.Point(30, 149);
            this.uiLine2.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine2.Name = "uiLine2";
            this.uiLine2.Size = new System.Drawing.Size(670, 20);
            this.uiLine2.TabIndex = 27;
            this.uiLine2.Text = "UIEditForm 代码生成";
            this.uiLine2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiSymbolButton1
            // 
            this.uiSymbolButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiSymbolButton1.Location = new System.Drawing.Point(30, 184);
            this.uiSymbolButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton1.Name = "uiSymbolButton1";
            this.uiSymbolButton1.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.uiSymbolButton1.Size = new System.Drawing.Size(100, 35);
            this.uiSymbolButton1.Symbol = 61543;
            this.uiSymbolButton1.TabIndex = 26;
            this.uiSymbolButton1.Text = "增加";
            this.uiSymbolButton1.Click += new System.EventHandler(this.uiSymbolButton1_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Font = new System.Drawing.Font("宋体", 12F);
            this.btnAdd.Location = new System.Drawing.Point(30, 92);
            this.btnAdd.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.btnAdd.Size = new System.Drawing.Size(100, 35);
            this.btnAdd.Symbol = 61543;
            this.btnAdd.TabIndex = 25;
            this.btnAdd.Text = "增加";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // uiLine1
            // 
            this.uiLine1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLine1.Location = new System.Drawing.Point(30, 55);
            this.uiLine1.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(670, 20);
            this.uiLine1.TabIndex = 24;
            this.uiLine1.Text = "UIEditForm";
            this.uiLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnEdit
            // 
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.Font = new System.Drawing.Font("宋体", 12F);
            this.btnEdit.Location = new System.Drawing.Point(152, 92);
            this.btnEdit.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.btnEdit.Size = new System.Drawing.Size(100, 35);
            this.btnEdit.Symbol = 61508;
            this.btnEdit.TabIndex = 23;
            this.btnEdit.Text = "编辑";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // FEditor
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.uiLine2);
            this.Controls.Add(this.uiSymbolButton1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.uiLine1);
            this.Controls.Add(this.btnEdit);
            this.Name = "FEditor";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.ShowTitle = true;
            this.Symbol = 61508;
            this.Text = "Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private UILine uiLine2;
        private UISymbolButton uiSymbolButton1;
        private UISymbolButton btnAdd;
        private UILine uiLine1;
        private UISymbolButton btnEdit;
    }
}