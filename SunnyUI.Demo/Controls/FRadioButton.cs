using System;

namespace Sunny.UI.Demo
{
    public partial class FRadioButton : UITitlePage
    {
        public FRadioButton()
        {
            InitializeComponent();
            uiRadioButtonGroup1.SelectedIndex = 2;
        }

        private void uiRadioButtonGroup1_ValueChanged(object sender, int index, string text)
        {
           Console.WriteLine("SelectedIndex: " + index + ", SelectedText: " + text);
        }
    }
}