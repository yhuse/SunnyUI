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
    }
}