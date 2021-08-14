using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    public partial class FCombobox : UIPage
    {
        public FCombobox()
        {
            InitializeComponent();

            IList<Info> infoList = new List<Info>();
            Info info1 = new Info() { Id = "1", Name = "张三" };
            Info info2 = new Info() { Id = "2", Name = "李四" };
            Info info3 = new Info() { Id = "3", Name = "王五" };
            infoList.Add(info1);
            infoList.Add(info2);
            infoList.Add(info3);

            uiComboBox2.ValueMember = "Id";
            uiComboBox2.DisplayMember = "Name";
            uiComboBox2.DataSource = infoList;
        }

        public class Info
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        private void uiComboBox1_DropDown(object sender, System.EventArgs e)
        {
            uiComboBox1.Items.Clear();
            uiComboBox1.Items.Add("100");
            uiComboBox1.Items.Add("101");
            uiComboBox1.Items.Add("102");
            uiComboBox1.Items.Add("103");
        }

        private void uiDatePicker1_ValueChanged(object sender, System.DateTime value)
        {
            value.ConsoleWriteLine();
        }

        private void uiTimePicker1_ValueChanged(object sender, System.DateTime value)
        {
            value.ConsoleWriteLine();
        }

        private void uiDatetimePicker1_ValueChanged(object sender, System.DateTime value)
        {
            value.ConsoleWriteLine();
        }

        private void uiColorPicker1_Click(object sender, System.EventArgs e)
        {
            Console.WriteLine(uiColorPicker1.Value.ToString());
        }

        private void uiColorPicker1_ValueChanged(object sender, System.Drawing.Color value)
        {
            Console.WriteLine(value.ToString());
        }

        private void uiComboTreeView2_NodesSelected(object sender, System.Windows.Forms.TreeNodeCollection nodes)
        {
            //返回的nodes为TreeView的所有节点，需循环判断
            foreach (TreeNode item in nodes)
            {
                if (item.Checked)
                    Console.WriteLine(item.ToString());
            }
        }

        private void uiComboTreeView3_NodesSelected(object sender, TreeNodeCollection nodes)
        {

        }
    }
}
