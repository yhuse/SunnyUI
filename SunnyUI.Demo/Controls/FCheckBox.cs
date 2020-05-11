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

        private void uiRadioButtonGroup1_ValueChanged(object sender, int index, string text)
        {
            MessageBox.Show(index.ToString());
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

            this.ShowInfoDialog("SelectedIndex: " + index + ", SelectedText: " + text + "\n" + sb.ToString());
        }
    }
}