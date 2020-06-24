using System;
using System.Text;
using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    public partial class FCheckBox : UITitlePage
    {
        public FCheckBox()
        {
            InitializeComponent();
        }

        private void uiCheckBoxGroup1_ValueChanged(object sender, int index, string text, bool isChecked)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SelectedIndexes: ");
            foreach (var selectedIndex in uiCheckBoxGroup1.SelectedIndexes)
            {
                sb.Append(selectedIndex);
                sb.Append(", ");
            }

           this.ShowSuccessTip("SelectedIndex: " + index + ", SelectedText: " + text + "\n" + sb.ToString());
        }

        private void uiButton1_Click(object sender, System.EventArgs e)
        {
            uiCheckBoxGroup1.SelectAll();
        }

        private void uiButton2_Click(object sender, System.EventArgs e)
        {
            uiCheckBoxGroup1.UnSelectAll();
        }

        private void uiButton3_Click(object sender, System.EventArgs e)
        {
            uiCheckBoxGroup1.ReverseSelected();
        }
    }
}