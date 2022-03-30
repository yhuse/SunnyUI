using System;
using System.Collections.Generic;
using System.Data;
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

            uiComboDataGridView1.DataGridView.Init();
            uiComboDataGridView1.ItemSize = new System.Drawing.Size(360, 240);
            uiComboDataGridView1.DataGridView.AddColumn("Column1", "Column1");
            uiComboDataGridView1.DataGridView.AddColumn("Column2", "Column2");
            uiComboDataGridView1.DataGridView.AddColumn("Column3", "Column3");
            uiComboDataGridView1.DataGridView.ReadOnly = true;
            uiComboDataGridView1.SelectIndexChange += UiComboDataGridView1_SelectIndexChange;


            dt.Columns.Add("Column1", typeof(string));
            dt.Columns.Add("Column2", typeof(string));
            dt.Columns.Add("Column3", typeof(string));

            for (int i = 0; i < 100; i++)
            {
                dt.Rows.Add("A" + i.ToString("D2"),
                    "B" + (i + 1).ToString("D2"),
                    "C" + (i + 2).ToString("D2"));
            }

            uiComboDataGridView1.ShowFilter = true;
            uiComboDataGridView1.DataGridView.DataSource = dt;
            uiComboDataGridView1.FilterColumnName = "Column1"; //不设置则全部列过滤
        }

        private void UiComboDataGridView1_SelectIndexChange(object sender, int index)
        {
            uiComboDataGridView1.Text = dt.Rows[index]["Column1"].ToString();
        }

        DataTable dt = new DataTable();

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
            Console.WriteLine(value);
        }

        private void uiTimePicker1_ValueChanged(object sender, System.DateTime value)
        {
            Console.WriteLine(value);
        }

        private void uiDatetimePicker1_ValueChanged(object sender, System.DateTime value)
        {
            Console.WriteLine(value);
            uiDatePicker3.Value = value;
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

        private void uiComboDataGridView1_ValueChanged(object sender, object value)
        {
            if (value == null)
            {
                uiComboDataGridView1.Text = "";
            }
            else
            {
                if (value is DataGridViewRow)
                {
                    DataGridViewRow row = (DataGridViewRow)value;
                    uiComboDataGridView1.Text = row.Cells["Column1"].Value.ToString();
                }
                else
                {
                    uiComboDataGridView1.Text = "";
                }
            }
        }

        private void uiComboDataGridView1_SelectIndexChange_1(object sender, int index)
        {
            uiComboDataGridView1.Text = dt.Rows[index]["Column1"].ToString();
        }

        private void uiDatePicker3_ValueChanged(object sender, DateTime value)
        {
            ShowInfoTip(uiDatePicker3.Value.DateString());
            Console.WriteLine(uiDatePicker3.Value);
        }
    }
}
