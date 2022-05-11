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
            this.uiAvatar1 = new Sunny.UI.UIAvatar();
            this.uiLine1 = new Sunny.UI.UILine();
            this.edtUser = new Sunny.UI.UITextBox();
            this.edtPassword = new Sunny.UI.UITextBox();
            this.btnLogin = new Sunny.UI.UISymbolButton();
            this.btnCancel = new Sunny.UI.UISymbolButton();
            this.lblTitle = new Sunny.UI.UILabel();
            this.lblSubText = new Sunny.UI.UILabel();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.uiPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiAvatar1
            // 
            this.uiAvatar1.BackColor = System.Drawing.Color.Transparent;
            this.uiAvatar1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiAvatar1.Location = new System.Drawing.Point(65, 16);
            this.uiAvatar1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiAvatar1.Name = "uiAvatar1";
            this.uiAvatar1.Size = new System.Drawing.Size(60, 60);
            this.uiAvatar1.Style = Sunny.UI.UIStyle.Custom;
            this.uiAvatar1.TabIndex = 4;
            this.uiAvatar1.Text = "uiAvatar1";
            this.uiAvatar1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLine1
            // 
            this.uiLine1.BackColor = System.Drawing.Color.White;
            this.uiLine1.FillColor = System.Drawing.Color.White;
            this.uiLine1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiLine1.Location = new System.Drawing.Point(4, 85);
            this.uiLine1.MinimumSize = new System.Drawing.Size(2, 2);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiLine1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiLine1.Size = new System.Drawing.Size(182, 28);
            this.uiLine1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLine1.StyleCustomMode = true;
            this.uiLine1.TabIndex = 5;
            this.uiLine1.Text = "用户登录";
            this.uiLine1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // edtUser
            // 
            this.edtUser.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edtUser.EnterAsTab = true;
            this.edtUser.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.edtUser.Location = new System.Drawing.Point(4, 121);
            this.edtUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtUser.MinimumSize = new System.Drawing.Size(1, 1);
            this.edtUser.Name = "edtUser";
            this.edtUser.Padding = new System.Windows.Forms.Padding(5);
            this.edtUser.ShowText = false;
            this.edtUser.Size = new System.Drawing.Size(182, 29);
            this.edtUser.Style = Sunny.UI.UIStyle.Custom;
            this.edtUser.Symbol = 61447;
            this.edtUser.SymbolSize = 22;
            this.edtUser.TabIndex = 0;
            this.edtUser.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.edtUser.Watermark = "请输入账号";
            this.edtUser.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // edtPassword
            // 
            this.edtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edtPassword.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.edtPassword.Location = new System.Drawing.Point(4, 162);
            this.edtPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtPassword.MinimumSize = new System.Drawing.Size(1, 1);
            this.edtPassword.Name = "edtPassword";
            this.edtPassword.Padding = new System.Windows.Forms.Padding(5);
            this.edtPassword.PasswordChar = '*';
            this.edtPassword.ShowText = false;
            this.edtPassword.Size = new System.Drawing.Size(182, 29);
            this.edtPassword.Style = Sunny.UI.UIStyle.Custom;
            this.edtPassword.Symbol = 61475;
            this.edtPassword.SymbolSize = 22;
            this.edtPassword.TabIndex = 1;
            this.edtPassword.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.edtPassword.Watermark = "请输入密码";
            this.edtPassword.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.edtPassword.DoEnter += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLogin.Location = new System.Drawing.Point(4, 206);
            this.btnLogin.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.btnLogin.ShowFocusColor = true;
            this.btnLogin.Size = new System.Drawing.Size(86, 29);
            this.btnLogin.Style = Sunny.UI.UIStyle.Custom;
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "登录";
            this.btnLogin.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnCancel.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.Location = new System.Drawing.Point(100, 206);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.btnCancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnCancel.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.ShowFocusColor = true;
            this.btnCancel.Size = new System.Drawing.Size(86, 29);
            this.btnCancel.Style = Sunny.UI.UIStyle.Red;
            this.btnCancel.StyleCustomMode = true;
            this.btnCancel.Symbol = 61453;
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.Navy;
            this.lblTitle.Location = new System.Drawing.Point(44, 35);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(694, 32);
            this.lblTitle.Style = Sunny.UI.UIStyle.Custom;
            this.lblTitle.StyleCustomMode = true;
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "SunnyUI.Net";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // lblSubText
            // 
            this.lblSubText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubText.BackColor = System.Drawing.Color.Transparent;
            this.lblSubText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubText.Location = new System.Drawing.Point(426, 421);
            this.lblSubText.Name = "lblSubText";
            this.lblSubText.Size = new System.Drawing.Size(310, 26);
            this.lblSubText.TabIndex = 7;
            this.lblSubText.Text = "SunnyUI";
            this.lblSubText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSubText.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.uiAvatar1);
            this.uiPanel1.Controls.Add(this.uiLine1);
            this.uiPanel1.Controls.Add(this.edtUser);
            this.uiPanel1.Controls.Add(this.edtPassword);
            this.uiPanel1.Controls.Add(this.btnCancel);
            this.uiPanel1.Controls.Add(this.btnLogin);
            this.uiPanel1.FillColor = System.Drawing.Color.White;
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiPanel1.Location = new System.Drawing.Point(433, 126);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiPanel1.Size = new System.Drawing.Size(190, 245);
            this.uiPanel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiPanel1.StyleCustomMode = true;
            this.uiPanel1.TabIndex = 9;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // UILoginForm
            // 
            this.AllowShowTitle = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::Sunny.UI.Properties.Resources.Login1;
            this.ClientSize = new System.Drawing.Size(750, 450);
            this.Controls.Add(this.uiPanel1);
            this.Controls.Add(this.lblSubText);
            this.Controls.Add(this.lblTitle);
            this.EscClose = true;
            this.MaximumSize = new System.Drawing.Size(750, 450);
            this.MinimumSize = new System.Drawing.Size(750, 450);
            this.Name = "UILoginForm";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.ShowInTaskbar = false;
            this.ShowTitle = false;
            this.Text = "UILogin";
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 750, 450);
            this.uiPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

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