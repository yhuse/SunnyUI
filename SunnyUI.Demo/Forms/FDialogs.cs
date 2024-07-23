using System;
using System.Collections.Generic;
using System.Threading;

namespace Sunny.UI.Demo
{
    public partial class FDialogs : UIPage, ITranslate
    {
        public FDialogs()
        {
            InitializeComponent();
        }

        public override void Init()
        {
            base.Init();
            btnCH.PerformClick();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            this.ShowInfoDialog("默认信息提示框");
            this.ShowInfoDialog2("默认信息提示框");
        }

        private void btnAsk_Click(object sender, EventArgs e)
        {
            if (this.ShowAskDialog("确认信息提示框"))
            {
                this.ShowSuccessTip("您点击了确定按钮");
            }
            else
            {
                this.ShowErrorTip("您点击了取消按钮");
            }

            if (this.ShowAskDialog2("确认信息提示框"))
            {
                this.ShowSuccessTip("您点击了确定按钮");
            }
            else
            {
                this.ShowErrorTip("您点击了取消按钮");
            }
        }

        private void btnSuccess_Click(object sender, EventArgs e)
        {
            this.ShowSuccessDialog("正确信息提示框", false, 3000);
            this.ShowSuccessDialog2("正确信息提示框", false, 3000);
        }

        private void btnWarn_Click(object sender, EventArgs e)
        {
            this.ShowWarningDialog("警告信息提示框");
            this.ShowWarningDialog2("警告信息提示框");
        }

        private void btnError_Click(object sender, EventArgs e)
        {
            this.ShowErrorDialog("错误信息提示框");
            this.ShowErrorDialog2("错误信息提示框");
        }

        private void btnStatus2_Click(object sender, EventArgs e)
        {
            this.ShowStatusForm(100, "数据加载中......", 0);
            for (int i = 0; i < 88; i++)
            {
                SystemEx.Delay(50);
                this.SetStatusFormDescription("数据加载中(" + i + "%)......");
                this.SetStatusFormStepIt();
            }

            this.HideStatusForm();
        }

        private void btnStringInput_Click(object sender, EventArgs e)
        {
            string value = "请输入字符串";
            if (this.ShowInputStringDialog(ref value, true, "请输入字符串：", true))
            {
                this.ShowInfoDialog(value);
            }
        }

        private void btnIntInput_Click(object sender, EventArgs e)
        {
            int value = 0;
            if (this.ShowInputIntegerDialog(ref value))
            {
                this.ShowInfoDialog(value.ToString());
            }
        }

        private void btnDoubleInput_Click(object sender, EventArgs e)
        {
            double value = 0;
            if (this.ShowInputDoubleDialog(ref value))
            {
                this.ShowInfoDialog(value.ToString("F2"));
            }
        }

        private void btnPasswordInput_Click(object sender, EventArgs e)
        {
            string value = "";
            if (this.ShowInputPasswordDialog(ref value))
            {
                this.ShowInfoDialog(value);
            }
        }

        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            List<string> items = new List<string>() { "0", "1", "2", "3", "4" };
            int index = 2;
            if (this.ShowSelectDialog(ref index, items))
            {
                this.ShowInfoDialog(index.ToString());
            }
        }

        private void uiSymbolButton2_Click(object sender, EventArgs e)
        {
            this.ShowInfoNotifier("Info", InfoNotifierClick);
        }

        private void InfoNotifierClick(object sender, EventArgs e)
        {
            this.ShowInfoTip("嗨，你点击了右下角的弹窗消息");
        }

        private void uiSymbolButton6_Click(object sender, EventArgs e)
        {
            this.ShowSuccessNotifier("Success");
        }

        private void uiSymbolButton5_Click(object sender, EventArgs e)
        {
            this.ShowWarningNotifier("Warning");
        }

        private void uiSymbolButton4_Click(object sender, EventArgs e)
        {
            this.ShowErrorNotifier("Error");
        }

        private void uiSymbolButton9_Click(object sender, EventArgs e)
        {
            this.ShowSuccessTip("轻便消息提示框 - 成功");
        }

        private void uiSymbolButton8_Click(object sender, EventArgs e)
        {
            this.ShowWarningTip("轻便消息提示框 - 警告");
        }

        private void uiSymbolButton7_Click(object sender, EventArgs e)
        {
            this.ShowErrorTip("轻便消息提示框 - 错误");
        }

        private void uiSymbolButton10_Click(object sender, EventArgs e)
        {
            FLogin frm = new FLogin();
            frm.ShowDialog();
            if (frm.IsLogin)
            {
                UIMessageTip.ShowOk("登录成功");
            }

            frm.Dispose();
        }

        private void uiSymbolButton3_Click(object sender, EventArgs e)
        {
            UILoginForm frm = new UILoginForm
            {
                ShowInTaskbar = true,
                Text = "Login",
                Title = "SunnyUI.Net Login Form",
                SubText = Version
            };

            frm.OnLogin += Frm_OnLogin;
            frm.LoginImage = UILoginForm.UILoginImage.Login2;
            frm.ShowDialog();
            if (frm.IsLogin)
            {
                UIMessageTip.ShowOk("登录成功");
            }

            frm.Dispose();
        }

        private bool Frm_OnLogin(string userName, string password)
        {
            return userName == "admin" && password == "admin";
        }

        private void uiSymbolButton11_Click(object sender, EventArgs e)
        {
            this.ShowWaitForm("准备开始...");
            Thread.Sleep(1000);
            this.SetWaitFormDescription(UILocalize.SystemProcessing + "20%");
            Thread.Sleep(1000);
            this.SetWaitFormDescription(UILocalize.SystemProcessing + "40%");
            Thread.Sleep(1000);
            this.SetWaitFormDescription(UILocalize.SystemProcessing + "60%");
            Thread.Sleep(1000);
            this.SetWaitFormDescription(UILocalize.SystemProcessing + "80%");
            Thread.Sleep(1000);
            this.SetWaitFormDescription(UILocalize.SystemProcessing + "100%");
            this.HideWaitForm();
        }

        private void uiSymbolButton13_Click(object sender, EventArgs e)
        {
            string dir = "";
            if (DirEx.SelectDirEx("扩展打开文件夹", ref dir))
            {
                UIMessageTip.ShowOk(dir);
            }
        }

        private void uiSymbolButton12_Click(object sender, EventArgs e)
        {
            this.ShowProcessForm(200);
            Thread.Sleep(2000);
            this.HideProcessForm();
        }

        private void btnCH_Click(object sender, EventArgs e)
        {
            UILocalizeHelper.SetCH();
            LoginText = "登录";
            UIStyles.Translate();
        }

        private void btnEN_Click(object sender, EventArgs e)
        {
            UILocalizeHelper.SetEN();
            LoginText = "Login";
            UIStyles.Translate();
        }

        private string LoginText = "登录";

        public void Translate()
        {
            uiSymbolButton6.Text = btnSuccess.Text = uiSymbolButton9.Text = UILocalize.SuccessTitle;
            uiSymbolButton5.Text = btnWarn.Text = uiSymbolButton8.Text = UILocalize.WarningTitle;
            uiSymbolButton4.Text = btnError.Text = uiSymbolButton7.Text = UILocalize.ErrorTitle;
            uiSymbolButton2.Text = btnInfo.Text = UILocalize.InfoTitle;
            btnAsk.Text = UILocalize.AskTitle;

            uiSymbolButton3.Text = uiSymbolButton10.Text = LoginText;
        }
    }
}