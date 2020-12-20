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