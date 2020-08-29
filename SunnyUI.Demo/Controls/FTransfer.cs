using System;

namespace Sunny.UI.Demo
{
    public partial class FTransfer : UITitlePage
    {
        public FTransfer()
        {
            InitializeComponent();
        }

        private void uiTransfer1_ItemsLeftCountChange(object sender, System.EventArgs e)
        {
            Console.WriteLine("Left: " + uiTransfer1.ItemsLeft.Count);
        }

        private void uiTransfer1_ItemsRightCountChange(object sender, System.EventArgs e)
        {
            Console.WriteLine("Right: " + uiTransfer1.ItemsRight.Count);
        }
    }
}
