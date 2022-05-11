using Sunny.UI.Properties;
using System;
using System.ComponentModel;

namespace Sunny.UI
{
    [DefaultProperty("Text")]
    [DefaultEvent("OnLogin")]
    public partial class UILoginForm : UIForm
    {
        public UILoginForm()
        {
            InitializeComponent();
            lblSubText.Text = lblSubText.Version;
        }

        [Description("顶部标题"), Category("SunnyUI")]
        [DefaultValue("SunnyUI.Net")]
        public string Title
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }

        [Description("底部说明"), Category("SunnyUI")]
        [DefaultValue("SunnyUI")]
        public string SubText
        {
            get => lblSubText.Text;
            set => lblSubText.Text = value;
        }

        [Description("用户登录"), Category("SunnyUI")]
        [DefaultValue("用户登录")]
        public string LoginText
        {
            get => uiLine1.Text;
            set => uiLine1.Text = value;
        }

        private UILoginImage loginImage;

        [DefaultValue(UILoginImage.Login1)]
        [Description("背景图片"), Category("SunnyUI")]
        public UILoginImage LoginImage
        {
            get => loginImage;
            set
            {
                if (loginImage != value)
                {
                    loginImage = value;

                    if (loginImage == UILoginImage.Login1) BackgroundImage = Resources.Login1;
                    if (loginImage == UILoginImage.Login2) BackgroundImage = Resources.Login2;
                    if (loginImage == UILoginImage.Login3) BackgroundImage = Resources.Login3;
                    if (loginImage == UILoginImage.Login4) BackgroundImage = Resources.Login4;
                    if (loginImage == UILoginImage.Login5) BackgroundImage = Resources.Login5;
                    if (loginImage == UILoginImage.Login6) BackgroundImage = Resources.Login6;
                }
            }
        }

        public enum UILoginImage
        {
            Login1,
            Login2,
            Login3,
            Login4,
            Login5,
            Login6
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (ButtonCancelClick != null)
                ButtonCancelClick?.Invoke(this, e);
            else
                Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (ButtonLoginClick != null)
            {
                ButtonLoginClick?.Invoke(this, e);
            }
            else
            {
                IsLogin = OnLogin != null && OnLogin(edtUser.Text, edtPassword.Text);
                if (IsLogin) Close();
            }
        }

        [Description("确认按钮事件"), Category("SunnyUI")]
        public event EventHandler ButtonLoginClick;

        [Description("取消按钮事件"), Category("SunnyUI")]
        public event EventHandler ButtonCancelClick;

        [DefaultValue(false), Browsable(false)]
        public bool IsLogin { get; protected set; }

        public delegate bool OnLoginHandle(string userName, string password);

        [Description("登录校验事件"), Category("SunnyUI")]
        public event OnLoginHandle OnLogin;

        [DefaultValue(null)]
        [Description("账号"), Category("SunnyUI")]
        public string UserName
        {
            get => edtUser.Text;
            set => edtUser.Text = value;
        }

        [DefaultValue(null)]
        [Description("密码"), Category("SunnyUI")]
        public string Password
        {
            get => edtPassword.Text;
            set => edtPassword.Text = value;
        }

        [DefaultValue("请输入账号")]
        [Description("账号水印文字"), Category("SunnyUI")]
        public string UserNameWatermark
        {
            get => edtUser.Watermark;
            set => edtUser.Watermark = value;
        }

        [DefaultValue(null)]
        [Description("请输入密码"), Category("SunnyUI")]
        public string PasswordWatermark
        {
            get => edtPassword.Watermark;
            set => edtPassword.Watermark = value;
        }

        [DefaultValue(null)]
        [Description("登录"), Category("SunnyUI")]
        public string ButtonLoginText
        {
            get => btnLogin.Text;
            set => btnLogin.Text = value;
        }

        [DefaultValue(null)]
        [Description("取消"), Category("SunnyUI")]
        public string ButtonCancelText
        {
            get => btnCancel.Text;
            set => btnCancel.Text = value;
        }
    }
}