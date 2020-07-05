using Sunny.UI.Demo.Forms;
using System;
using System.Collections.Generic;

namespace Sunny.UI.Demo
{
    public partial class FDialogs : UITitlePage
    {
        public FDialogs()
        {
            InitializeComponent();
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
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            this.ShowInfoDialog("跟随界面主题风格信息提示框", Style);
        }

        private void btnSuccess_Click(object sender, EventArgs e)
        {
            this.ShowSuccessDialog("正确信息提示框");
        }

        private void btnWarn_Click(object sender, EventArgs e)
        {
            this.ShowWarningDialog("警告信息提示框");
        }

        private void btnError_Click(object sender, EventArgs e)
        {
            this.ShowErrorDialog("错误信息提示框");
        }

        private void btnStatus2_Click(object sender, EventArgs e)
        {
            ShowStatus("提示", "数据加载中......");
            for (int i = 0; i < 100; i++)
            {
                SystemEx.Delay(50);
                StatusDescription = "数据加载中(" + i + "%)......";
                StatusStepIt();
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            this.ShowInfoDialog("默认信息提示框");
        }

        private void btnStringInput_Click(object sender, EventArgs e)
        {
            string value = "请输入字符串";
            if (this.InputStringDialog(ref value))
            {
                this.ShowInfoDialog(value);
            }
        }

        private void btnIntInput_Click(object sender, EventArgs e)
        {
            int value = 0;
            if (this.InputIntegerDialog(ref value))
            {
                this.ShowInfoDialog(value.ToString());
            }
        }

        private void btnDoubleInput_Click(object sender, EventArgs e)
        {
            double value = 0;
            if (this.InputDoubleDialog(ref value))
            {
                this.ShowInfoDialog(value.ToString("F2"));
            }
        }

        private void btnPasswordInput_Click(object sender, EventArgs e)
        {
            string value = "";
            if (this.InputPasswordDialog(ref value))
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
            this.ShowInfoNotifier("Info");
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

        private void btnCH_Click(object sender, EventArgs e)
        {
            UILocalizeHelper.SetCH();
        }

        private void btnEN_Click(object sender, EventArgs e)
        {
            UILocalizeHelper.SetEN();
        }

        private void uiSymbolButton9_Click(object sender, EventArgs e)
        {
            UIMessageTip.ShowOk("轻便消息提示框 - 成功");
        }

        private void uiSymbolButton8_Click(object sender, EventArgs e)
        {
            UIMessageTip.ShowWarning("轻便消息提示框 - 警告", 1000, true);
        }

        private void uiSymbolButton7_Click(object sender, EventArgs e)
        {
            UIMessageTip.ShowError("轻便消息提示框 - 错误");
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
            UILoginForm frm = new UILoginForm();
            frm.Text = "SunnyUI.Net Login Form";
            frm.SubText = "SunnyUI.Net V2.2.5";
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
    }
}