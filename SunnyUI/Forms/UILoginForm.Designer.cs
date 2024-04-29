namespace Sunny.UI
{
    partial class UILoginForm
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
            uiAvatar1 = new UIAvatar();
            uiLine1 = new UILine();
            edtUser = new UITextBox();
            edtPassword = new UITextBox();
            btnLogin = new UISymbolButton();
            btnCancel = new UISymbolButton();
            lblTitle = new UILabel();
            lblSubText = new UILabel();
            uiPanel1 = new UIPanel();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // uiAvatar1
            // 
            uiAvatar1.BackColor = System.Drawing.Color.Transparent;
            uiAvatar1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiAvatar1.Location = new System.Drawing.Point(65, 16);
            uiAvatar1.MinimumSize = new System.Drawing.Size(1, 1);
            uiAvatar1.Name = "uiAvatar1";
            uiAvatar1.Size = new System.Drawing.Size(60, 60);
            uiAvatar1.TabIndex = 4;
            uiAvatar1.Text = "uiAvatar1";
            // 
            // uiLine1
            // 
            uiLine1.BackColor = System.Drawing.Color.Transparent;
            uiLine1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiLine1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            uiLine1.Location = new System.Drawing.Point(4, 85);
            uiLine1.MinimumSize = new System.Drawing.Size(2, 2);
            uiLine1.Name = "uiLine1";
            uiLine1.RadiusSides = UICornerRadiusSides.None;
            uiLine1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            uiLine1.Size = new System.Drawing.Size(182, 28);
            uiLine1.StyleCustomMode = true;
            uiLine1.TabIndex = 5;
            uiLine1.Text = "用户登录";
            // 
            // edtUser
            // 
            edtUser.Cursor = System.Windows.Forms.Cursors.IBeam;
            edtUser.EnterAsTab = true;
            edtUser.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            edtUser.Location = new System.Drawing.Point(4, 121);
            edtUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            edtUser.MinimumSize = new System.Drawing.Size(1, 1);
            edtUser.Name = "edtUser";
            edtUser.Padding = new System.Windows.Forms.Padding(5);
            edtUser.ShowText = false;
            edtUser.Size = new System.Drawing.Size(182, 29);
            edtUser.Symbol = 61447;
            edtUser.SymbolSize = 22;
            edtUser.TabIndex = 0;
            edtUser.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            edtUser.Watermark = "请输入账号";
            // 
            // edtPassword
            // 
            edtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            edtPassword.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            edtPassword.Location = new System.Drawing.Point(4, 162);
            edtPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            edtPassword.MinimumSize = new System.Drawing.Size(1, 1);
            edtPassword.Name = "edtPassword";
            edtPassword.Padding = new System.Windows.Forms.Padding(5);
            edtPassword.PasswordChar = '*';
            edtPassword.ShowText = false;
            edtPassword.Size = new System.Drawing.Size(182, 29);
            edtPassword.Symbol = 61475;
            edtPassword.SymbolSize = 22;
            edtPassword.TabIndex = 1;
            edtPassword.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            edtPassword.Watermark = "请输入密码";
            edtPassword.DoEnter += btnLogin_Click;
            // 
            // btnLogin
            // 
            btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            btnLogin.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnLogin.Location = new System.Drawing.Point(4, 206);
            btnLogin.MinimumSize = new System.Drawing.Size(1, 1);
            btnLogin.Name = "btnLogin";
            btnLogin.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            btnLogin.ShowFocusColor = true;
            btnLogin.Size = new System.Drawing.Size(86, 29);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "登录";
            btnLogin.Click += btnLogin_Click;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCancel.FillColor = System.Drawing.Color.FromArgb(230, 80, 80);
            btnCancel.FillColor2 = System.Drawing.Color.FromArgb(230, 80, 80);
            btnCancel.FillHoverColor = System.Drawing.Color.FromArgb(235, 115, 115);
            btnCancel.FillPressColor = System.Drawing.Color.FromArgb(184, 64, 64);
            btnCancel.FillSelectedColor = System.Drawing.Color.FromArgb(184, 64, 64);
            btnCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnCancel.Location = new System.Drawing.Point(100, 206);
            btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            btnCancel.RectColor = System.Drawing.Color.FromArgb(230, 80, 80);
            btnCancel.RectHoverColor = System.Drawing.Color.FromArgb(235, 115, 115);
            btnCancel.RectPressColor = System.Drawing.Color.FromArgb(184, 64, 64);
            btnCancel.RectSelectedColor = System.Drawing.Color.FromArgb(184, 64, 64);
            btnCancel.ShowFocusColor = true;
            btnCancel.Size = new System.Drawing.Size(86, 29);
            btnCancel.Style = UIStyle.Custom;
            btnCancel.StyleCustomMode = true;
            btnCancel.Symbol = 61453;
            btnCancel.TabIndex = 3;
            btnCancel.Text = "取消";
            btnCancel.Click += btnCancel_Click;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = System.Drawing.Color.Transparent;
            lblTitle.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblTitle.ForeColor = System.Drawing.Color.Navy;
            lblTitle.Location = new System.Drawing.Point(44, 35);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(694, 32);
            lblTitle.Style = UIStyle.Custom;
            lblTitle.StyleCustomMode = true;
            lblTitle.TabIndex = 6;
            lblTitle.Text = "SunnyUI.Net";
            lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubText
            // 
            lblSubText.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblSubText.BackColor = System.Drawing.Color.Transparent;
            lblSubText.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblSubText.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lblSubText.Location = new System.Drawing.Point(426, 421);
            lblSubText.Name = "lblSubText";
            lblSubText.Size = new System.Drawing.Size(310, 26);
            lblSubText.TabIndex = 7;
            lblSubText.Text = "SunnyUI";
            lblSubText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(uiAvatar1);
            uiPanel1.Controls.Add(uiLine1);
            uiPanel1.Controls.Add(edtUser);
            uiPanel1.Controls.Add(edtPassword);
            uiPanel1.Controls.Add(btnCancel);
            uiPanel1.Controls.Add(btnLogin);
            uiPanel1.FillColor = System.Drawing.Color.White;
            uiPanel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiPanel1.Location = new System.Drawing.Point(433, 126);
            uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.RadiusSides = UICornerRadiusSides.None;
            uiPanel1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            uiPanel1.Size = new System.Drawing.Size(190, 245);
            uiPanel1.Style = UIStyle.Custom;
            uiPanel1.StyleCustomMode = true;
            uiPanel1.TabIndex = 9;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UILoginForm
            // 
            AllowShowTitle = false;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackgroundImage = Properties.Resources.Login1;
            ClientSize = new System.Drawing.Size(750, 450);
            Controls.Add(uiPanel1);
            Controls.Add(lblSubText);
            Controls.Add(lblTitle);
            EscClose = true;
            MaximumSize = new System.Drawing.Size(750, 450);
            MinimumSize = new System.Drawing.Size(750, 450);
            Name = "UILoginForm";
            Padding = new System.Windows.Forms.Padding(0);
            ShowIcon = false;
            ShowInTaskbar = false;
            ShowTitle = false;
            Text = "UILogin";
            ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 750, 450);
            Shown += UILoginForm_Shown;
            uiPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private UIAvatar uiAvatar1;
        private UILine uiLine1;
        private UITextBox edtUser;
        private UITextBox edtPassword;
        protected UILabel lblTitle;
        private UISymbolButton btnLogin;
        private UISymbolButton btnCancel;
        protected UIPanel uiPanel1;
        protected UILabel lblSubText;
    }
}