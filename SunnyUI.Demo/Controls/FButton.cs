using System;

namespace Sunny.UI.Demo
{
    public partial class FButton : UITitlePage
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
        /// 放在 [窗体Load、重载OnLoad、重载Init] 的内容每次页面切换都会执行。
        /// 这三个选一个用就行了。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FButton_Load(object sender, EventArgs e)
        {
            uiSwitch1.Active = uiSwitch4.Active = true;
            uiSwitch2.Active = uiSwitch3.Active = false;
        }

        //放在 [窗体Load、重载OnLoad、重载Init] 的内容每次页面切换都会执行。
        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);
        //    uiSwitch1.Active = uiSwitch4.Active = true;
        //    uiSwitch2.Active = uiSwitch3.Active = false;
        //}

        //放在 [窗体Load、重载OnLoad、重载Init] 的内容每次页面切换都会执行。
        //public override void Init()
        //{
        //    base.Init();
        //    uiSwitch1.Active = uiSwitch4.Active = true;
        //    uiSwitch2.Active = uiSwitch3.Active = false;
        //}

        private void uiButton10_Click(object sender, EventArgs e)
        {
            uiButton10.Selected = !uiButton10.Selected;
        }

        private void uiSwitch1_ValueChanged(object sender, bool value)
        {
            Console.WriteLine(uiSwitch1.Active);
        }

        private void uiSwitch1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(uiSwitch1.Active);
        }

        private void uiButton13_Click(object sender, EventArgs e)
        {
            Frame.SelectPage(1004);
        }
    }
}