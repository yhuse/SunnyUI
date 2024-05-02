using System;

namespace Sunny.UI.Demo
{
    public partial class FButton : UIPage
    {
        public FButton()
        {
            InitializeComponent();
            uiToolTip1.SetToolTip(uiButton1, uiButton1.Text);
            uiToolTip1.SetToolTip(uiSymbolButton1, uiSymbolButton1.Text, "SunnyUI");
            uiToolTip1.SetToolTip(uiSymbolButton2, uiSymbolButton2.Text, "SunnyUI",
                uiSymbolButton2.Symbol, 32, UIColor.Red);
        }

        /// <summary>
        /// 放在 [窗体Load (NeedReload = true)] 的内容每次页面切换，进入页面都会执行。
        /// 这三个选一个用就行了。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FButton_Load(object sender, EventArgs e)
        {
            Console.WriteLine("1. FButton_Load");

        }

        //放在 [窗体Load (NeedReload = true)] 的内容每次页面切换，进入页面都会执行。
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Console.WriteLine("3. FButton_OnLoad");
        }

        //放在 [重载Init] 的内容每次页面切换，进入页面都会执行。
        public override void Init()
        {
            base.Init();
            uiSwitch1.Active = uiSwitch4.Active = true;
            uiSwitch2.Active = uiSwitch3.Active = false;

            Console.WriteLine("2. FButton_Init");
        }

        //放在 [Final] 的内容每次页面切换，退出页面都会执行
        public override void Final()
        {
            base.Final();
            Console.WriteLine("4. FButton_Final");
        }

        private void uiSwitch1_ValueChanged(object sender, bool value)
        {
            Console.WriteLine(uiSwitch1.Active);
        }

        private void uiSymbolButton25_Click(object sender, EventArgs e)
        {
            Frame.SelectPage(5000);
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            //传值给页面1001
            //设置FAvatar的Label文字
            SendParamToPage(1001, "你好");
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            //传值给框架
            SendParamToFrame("传值给框架");
        }

        private void uiSwitch1_ActiveChanging(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !this.ShowAskDialog("您确认要改变当前开关的状态吗？");
        }
    }
}
