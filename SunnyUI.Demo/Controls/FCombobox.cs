using System;

namespace Sunny.UI.Demo
{
    public partial class FCombobox : UITitlePage
    {
        public FCombobox()
        {
            InitializeComponent();
        }

        private void uiDatePicker1_ValueChanged(object sender, DateTime value)
        {
            uiDatePicker1.Value.ConsoleWriteLine();
        }

        private void uiDatetimePicker1_ValueChanged(object sender, DateTime value)
        {
            uiDatetimePicker1.Value.ConsoleWriteLine();
        }

        private void uiTimePicker1_ValueChanged(object sender, DateTime value)
        {
            uiTimePicker1.Value.ConsoleWriteLine();
        }

        private void uiComboBox1_DropDown(object sender, EventArgs e)
        {
            uiComboBox1.Items.Clear();
            uiComboBox1.Items.Add("100");
            uiComboBox1.Items.Add("101");
            uiComboBox1.Items.Add("102");
            uiComboBox1.Items.Add("103");
        }

        private void uiComboTreeView1_NodeSelected(object sender, System.Windows.Forms.TreeNode node)
        {
            ShowInfoTip(node.Text);
        }

        private void uiComboTreeView2_NodesSelected(object sender, System.Windows.Forms.TreeNodeCollection node)
        {
            ShowInfoTip(uiComboTreeView2.Text);
        }
    }
}
