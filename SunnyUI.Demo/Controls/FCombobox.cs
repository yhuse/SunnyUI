using System;
using System.Collections.Generic;

namespace Sunny.UI.Demo
{
    public partial class FCombobox : UITitlePage
    {
        public FCombobox()
        {
            InitializeComponent();

            IList<Info> infoList = new List<Info>();
            Info info1 = new Info() { Id = " 1 ", Name = " 张三 " };
            Info info2 = new Info() { Id = " 2 ", Name = " 李四 " };
            Info info3 = new Info() { Id = " 3 ", Name = " 王五 " };
            infoList.Add(info1);
            infoList.Add(info2);
            infoList.Add(info3);

            uiComboBox2.ValueMember = "Id";
            uiComboBox2.DisplayMember = "Name";
            uiComboBox2.DataSource = infoList;

            uiComboboxEx2.ValueMember = "Id";
            uiComboboxEx2.DisplayMember = "Name";
            uiComboboxEx2.DataSource = infoList;
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

        public class Info
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        private void uiComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowInfoTip(uiComboBox2.SelectedValue.ToString());
        }

        private void uiColorPicker1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(uiColorPicker1.Value.ToString());
        }

        private void uiColorPicker1_ValueChanged(object sender, System.Drawing.Color value)
        {
            Console.WriteLine(uiColorPicker1.Value.ToString());
        }
    }
}
