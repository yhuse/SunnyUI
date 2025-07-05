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
            this.ShowInfoDialog2(CodeTranslator.Current.Default);
            this.ShowInfoDialog(CodeTranslator.Current.Default);
        }

        private void btnAsk_Click(object sender, EventArgs e)
        {
            if (this.ShowAskDialog2(CodeTranslator.Current.Confirm, true))
            {
                this.ShowSuccessTip(CodeTranslator.Current.PressedOK);
            }
            else
            {
                this.ShowErrorTip(CodeTranslator.Current.PressedCancel);
            }

            if (this.ShowAskDialog(CodeTranslator.Current.Confirm))
            {
                this.ShowSuccessTip(CodeTranslator.Current.PressedOK);
            }
            else
            {
                this.ShowErrorTip(CodeTranslator.Current.PressedCancel);
            }
        }

        private void btnSuccess_Click(object sender, EventArgs e)
        {
            this.ShowSuccessDialog2(CodeTranslator.Current.Success, false, 3000);
            this.ShowSuccessDialog(CodeTranslator.Current.Success, false, 3000);
        }

        private void btnWarn_Click(object sender, EventArgs e)
        {
            this.ShowWarningDialog2(CodeTranslator.Current.Warning);
            this.ShowWarningDialog(CodeTranslator.Current.Warning);
        }

        private void btnError_Click(object sender, EventArgs e)
        {
            this.ShowErrorDialog2(CodeTranslator.Current.Error);
            this.ShowErrorDialog(CodeTranslator.Current.Error);
        }

        private void btnStatus2_Click(object sender, EventArgs e)
        {
            this.ShowStatusForm(100, CodeTranslator.Current.Loading + "......", 0);
            for (int i = 0; i < 88; i++)
            {
                SystemEx.Delay(50);
                this.SetStatusFormDescription(CodeTranslator.Current.Loading + "(" + i + "%)......");
                this.SetStatusFormStepIt();
            }

            this.HideStatusForm();
        }

        private void btnStringInput_Click(object sender, EventArgs e)
        {
            string value = CodeTranslator.Current.InputString;
            if (this.ShowInputStringDialog(ref value, true, CodeTranslator.Current.InputString, true))
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
            this.ShowInfoNotifier("Hello SunnyUI!", InfoNotifierClick);
        }

        private void InfoNotifierClick(object sender, DescriptionEventArgs e)
        {
            this.ShowInfoTip(e.Description);
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
            this.ShowSuccessTip(CodeTranslator.Current.Success);
        }

        private void uiSymbolButton8_Click(object sender, EventArgs e)
        {
            this.ShowWarningTip(CodeTranslator.Current.Warning);
        }

        private void uiSymbolButton7_Click(object sender, EventArgs e)
        {
            this.ShowErrorTip(CodeTranslator.Current.Error);
        }

        private void uiSymbolButton10_Click(object sender, EventArgs e)
        {
            FLogin frm = new FLogin();
            frm.ShowDialog();
            if (frm.IsLogin)
            {
                UIMessageTip.ShowOk(CodeTranslator.Current.LoginSuccess);
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
                UIMessageTip.ShowOk(CodeTranslator.Current.LoginSuccess);
            }

            frm.Dispose();
        }

        private bool Frm_OnLogin(string userName, string password)
        {
            return userName == "admin" && password == "admin";
        }

        private void uiSymbolButton11_Click(object sender, EventArgs e)
        {
            this.ShowWaitForm(CodeTranslator.Current.Prepare);
            Thread.Sleep(1000);
            this.SetWaitFormDescription(UIStyles.CurrentResources.SystemProcessing + "20%");
            Thread.Sleep(1000);
            this.SetWaitFormDescription(UIStyles.CurrentResources.SystemProcessing + "40%");
            Thread.Sleep(1000);
            this.SetWaitFormDescription(UIStyles.CurrentResources.SystemProcessing + "60%");
            Thread.Sleep(1000);
            this.SetWaitFormDescription(UIStyles.CurrentResources.SystemProcessing + "80%");
            Thread.Sleep(1000);
            this.SetWaitFormDescription(UIStyles.CurrentResources.SystemProcessing + "100%");
            this.HideWaitForm();
        }

        private void uiSymbolButton13_Click(object sender, EventArgs e)
        {
            string dir = "";
            if (Dialogs.SelectDirEx(CodeTranslator.Current.OpenDir, ref dir))
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
            UIStyles.CultureInfo = CultureInfos.zh_CN;
        }

        private void btnEN_Click(object sender, EventArgs e)
        {
            UIStyles.CultureInfo = CultureInfos.en_US;
        }

        /// <summary>
        /// 重载多语翻译
        /// </summary>
        public override void Translate()
        {
            //必须保留
            base.Translate();
            //读取翻译代码中的多语资源
            CodeTranslator.Load(this);
        }

        private class CodeTranslator : IniCodeTranslator<CodeTranslator>
        {
            public string Default { get; set; } = "默认信息提示框";
            public string Confirm { get; set; } = "确认信息提示框";
            public string PressedOK { get; set; } = "您点击了确定按钮";
            public string PressedCancel { get; set; } = "您点击了取消按钮";
            public string Success { get; set; } = "正确信息提示框";
            public string Warning { get; set; } = "警告信息提示框";
            public string Error { get; set; } = "错误信息提示框";
            public string Loading { get; set; } = "数据加载中";
            public string InputString { get; set; } = "请输入字符串";
            public string PressedNotifier { get; set; } = "嗨，你点击了右下角的弹窗消息";
            public string LoginSuccess { get; set; } = "登录成功";
            public string Prepare { get; set; } = "准备开始";
            public string OpenDir { get; set; } = "打开文件夹";
        }
    }
}