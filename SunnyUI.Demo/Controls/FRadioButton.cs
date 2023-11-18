using System;

namespace Sunny.UI.Demo
{
    public partial class FRadioButton : UIPage
    {
        public FRadioButton()
        {
            InitializeComponent();
            uiRadioButtonGroup1.SelectedIndex = 2;
        }

        public override void Init()
        {
            base.Init();
            uiRadioButtonGroup1.SelectedIndex = 1;
        }

        private void uiRadioButtonGroup1_ValueChanged(object sender, int index, string text)
        {
            Console.WriteLine("SelectedIndex: " + index + ", SelectedText: " + text);
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            uiRadioButtonGroup1.SelectedIndex = -1;
        }

        private void uiButton4_Click(object sender, EventArgs e)
        {
            uiRadioButtonGroup1.SelectedIndex = 6;
        }

        private void uiRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine(uiRadioButton1.Checked);
        }
    }
}
