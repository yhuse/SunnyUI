namespace Sunny.UI.Demo
{
    partial class FEdit
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
            this.edtName = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.rbMale = new Sunny.UI.UIRadioButton();
            this.rbFemale = new Sunny.UI.UIRadioButton();
            this.edtAge = new Sunny.UI.UITextBox();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.cbDepartment = new Sunny.UI.UIComboBox();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.uiLabel6 = new Sunny.UI.UILabel();
            this.edtDate = new Sunny.UI.UIDatePicker();
            this.edtAddress = new Sunny.UI.UITextBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.SuspendLayout();
            // 
            // pnlBtm
            // 
            this.pnlBtm.Location = new System.Drawing.Point(1, 304);
            this.pnlBtm.Size = new System.Drawing.Size(518, 55);
            this.pnlBtm.TabIndex = 7;
            // 
            // edtName
            // 
            this.edtName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edtName.EnterAsTab = true;
            this.edtName.FillColor = System.Drawing.Color.White;
            this.edtName.Font = new System.Drawing.Font("宋体", 12F);
            this.edtName.Location = new System.Drawing.Point(150, 55);
            this.edtName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtName.Maximum = 2147483647D;
            this.edtName.Minimum = -2147483648D;
            this.edtName.Name = "edtName";
            this.edtName.Padding = new System.Windows.Forms.Padding(5);
            this.edtName.Size = new System.Drawing.Size(340, 29);
            this.edtName.TabIndex = 0;
            // 
            // uiLabel2
            // 
            this.uiLabel2.AutoSize = true;
            this.uiLabel2.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLabel2.Location = new System.Drawing.Point(56, 59);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(42, 21);
            this.uiLabel2.TabIndex = 4;
            this.uiLabel2.Text = "姓名";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel3
            // 
            this.uiLabel3.AutoSize = true;
            this.uiLabel3.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLabel3.Location = new System.Drawing.Point(56, 99);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(42, 21);
            this.uiLabel3.TabIndex = 6;
            this.uiLabel3.Text = "性别";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbMale
            // 
            this.rbMale.Checked = true;
            this.rbMale.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbMale.Font = new System.Drawing.Font("宋体", 12F);
            this.rbMale.Location = new System.Drawing.Point(150, 95);
            this.rbMale.Name = "rbMale";
            this.rbMale.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rbMale.Size = new System.Drawing.Size(82, 29);
            this.rbMale.TabIndex = 1;
            this.rbMale.Text = "男";
            // 
            // rbFemale
            // 
            this.rbFemale.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbFemale.Font = new System.Drawing.Font("宋体", 12F);
            this.rbFemale.Location = new System.Drawing.Point(238, 95);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rbFemale.Size = new System.Drawing.Size(82, 29);
            this.rbFemale.TabIndex = 2;
            this.rbFemale.Text = "女";
            // 
            // edtAge
            // 
            this.edtAge.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edtAge.FillColor = System.Drawing.Color.White;
            this.edtAge.Font = new System.Drawing.Font("宋体", 12F);
            this.edtAge.Location = new System.Drawing.Point(150, 135);
            this.edtAge.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtAge.Maximum = 2147483647D;
            this.edtAge.Minimum = -2147483648D;
            this.edtAge.Name = "edtAge";
            this.edtAge.Padding = new System.Windows.Forms.Padding(5);
            this.edtAge.Size = new System.Drawing.Size(170, 29);
            this.edtAge.TabIndex = 3;
            this.edtAge.Text = "0";
            this.edtAge.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            // 
            // uiLabel4
            // 
            this.uiLabel4.AutoSize = true;
            this.uiLabel4.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLabel4.Location = new System.Drawing.Point(56, 139);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(42, 21);
            this.uiLabel4.TabIndex = 10;
            this.uiLabel4.Text = "年龄";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbDepartment
            // 
            this.cbDepartment.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cbDepartment.FillColor = System.Drawing.Color.White;
            this.cbDepartment.Font = new System.Drawing.Font("宋体", 12F);
            this.cbDepartment.Items.AddRange(new object[] {
            "研发部",
            "采购部",
            "生产部",
            "销售部",
            "人事部",
            "财务部",
            "行政部",
            "其他"});
            this.cbDepartment.Location = new System.Drawing.Point(150, 175);
            this.cbDepartment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDepartment.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbDepartment.Name = "cbDepartment";
            this.cbDepartment.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.cbDepartment.Size = new System.Drawing.Size(170, 29);
            this.cbDepartment.TabIndex = 4;
            this.cbDepartment.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel5
            // 
            this.uiLabel5.AutoSize = true;
            this.uiLabel5.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLabel5.Location = new System.Drawing.Point(56, 179);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(42, 21);
            this.uiLabel5.TabIndex = 12;
            this.uiLabel5.Text = "部门";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel6
            // 
            this.uiLabel6.AutoSize = true;
            this.uiLabel6.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLabel6.Location = new System.Drawing.Point(56, 219);
            this.uiLabel6.Name = "uiLabel6";
            this.uiLabel6.Size = new System.Drawing.Size(42, 21);
            this.uiLabel6.TabIndex = 13;
            this.uiLabel6.Text = "生日";
            this.uiLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edtDate
            // 
            this.edtDate.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.edtDate.FillColor = System.Drawing.Color.White;
            this.edtDate.Font = new System.Drawing.Font("宋体", 12F);
            this.edtDate.Location = new System.Drawing.Point(150, 215);
            this.edtDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtDate.MaxLength = 10;
            this.edtDate.MinimumSize = new System.Drawing.Size(63, 0);
            this.edtDate.Name = "edtDate";
            this.edtDate.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.edtDate.Size = new System.Drawing.Size(170, 29);
            this.edtDate.SymbolDropDown = 61555;
            this.edtDate.SymbolNormal = 61555;
            this.edtDate.TabIndex = 5;
            this.edtDate.Text = "2020-05-08";
            this.edtDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.edtDate.Value = new System.DateTime(2020, 5, 8, 0, 0, 0, 0);
            // 
            // edtAddress
            // 
            this.edtAddress.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edtAddress.FillColor = System.Drawing.Color.White;
            this.edtAddress.Font = new System.Drawing.Font("宋体", 12F);
            this.edtAddress.Location = new System.Drawing.Point(150, 254);
            this.edtAddress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtAddress.Maximum = 2147483647D;
            this.edtAddress.Minimum = -2147483648D;
            this.edtAddress.Name = "edtAddress";
            this.edtAddress.Padding = new System.Windows.Forms.Padding(5);
            this.edtAddress.Size = new System.Drawing.Size(340, 29);
            this.edtAddress.TabIndex = 6;
            // 
            // uiLabel1
            // 
            this.uiLabel1.AutoSize = true;
            this.uiLabel1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLabel1.Location = new System.Drawing.Point(56, 258);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(42, 21);
            this.uiLabel1.TabIndex = 15;
            this.uiLabel1.Text = "住址";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(520, 362);
            this.Controls.Add(this.edtAddress);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.edtDate);
            this.Controls.Add(this.uiLabel6);
            this.Controls.Add(this.uiLabel5);
            this.Controls.Add(this.cbDepartment);
            this.Controls.Add(this.uiLabel4);
            this.Controls.Add(this.edtAge);
            this.Controls.Add(this.rbFemale);
            this.Controls.Add(this.rbMale);
            this.Controls.Add(this.uiLabel3);
            this.Controls.Add(this.edtName);
            this.Controls.Add(this.uiLabel2);
            this.Name = "FEdit";
            this.Text = "UIEditFrom";
            this.Controls.SetChildIndex(this.pnlBtm, 0);
            this.Controls.SetChildIndex(this.uiLabel2, 0);
            this.Controls.SetChildIndex(this.edtName, 0);
            this.Controls.SetChildIndex(this.uiLabel3, 0);
            this.Controls.SetChildIndex(this.rbMale, 0);
            this.Controls.SetChildIndex(this.rbFemale, 0);
            this.Controls.SetChildIndex(this.edtAge, 0);
            this.Controls.SetChildIndex(this.uiLabel4, 0);
            this.Controls.SetChildIndex(this.cbDepartment, 0);
            this.Controls.SetChildIndex(this.uiLabel5, 0);
            this.Controls.SetChildIndex(this.uiLabel6, 0);
            this.Controls.SetChildIndex(this.edtDate, 0);
            this.Controls.SetChildIndex(this.uiLabel1, 0);
            this.Controls.SetChildIndex(this.edtAddress, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private UITextBox edtName;
        private UILabel uiLabel2;
        private UILabel uiLabel3;
        private UIRadioButton rbMale;
        private UIRadioButton rbFemale;
        private UITextBox edtAge;
        private UILabel uiLabel4;
        private UIComboBox cbDepartment;
        private UILabel uiLabel5;
        private UILabel uiLabel6;
        private UIDatePicker edtDate;
        private UITextBox edtAddress;
        private UILabel uiLabel1;
    }
}