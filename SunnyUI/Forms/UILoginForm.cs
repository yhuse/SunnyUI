/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UILoginForm.cs
 * 文件说明: 登录窗体基类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2023-04-19: V3.3.5 增加可选择显示时激活的控件
******************************************************************************/

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

        [Description("显示时激活的控件"), Category("SunnyUI")]
        [DefaultValue(UILoginFormFocusControl.UserName)]
        public UILoginFormFocusControl FocusControl { get; set; } = UILoginFormFocusControl.UserName;

        private void UILoginForm_Shown(object sender, EventArgs e)
        {
            switch (FocusControl)
            {
                case UILoginFormFocusControl.UserName:
                    edtUser.Focus();
                    break;
                case UILoginFormFocusControl.Password:
                    edtPassword.Focus();
                    break;
                case UILoginFormFocusControl.ButtonLogin:
                    btnLogin.Focus();
                    break;
                case UILoginFormFocusControl.ButtonCancel:
                    btnCancel.Focus();
                    break;
            }
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

        protected override void SetStyle(UIStyle style)
        {
            base.SetStyle(style);
            uiLine1.SetStyleColor(style.Colors());
            uiLine1.FillColor = UIColor.White;
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