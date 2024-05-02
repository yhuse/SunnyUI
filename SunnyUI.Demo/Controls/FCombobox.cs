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
            for (int i = 0; i < 120; i++)
            {
                infoList.Add(new Info() { Id = i.ToString(), Name = "节点" + i });
            }

            uiComboBox2.ValueMember = "Id";
            uiComboBox2.DisplayMember = "Name";
            uiComboBox2.DataSource = infoList;

            uiComboBox3.ValueMember = "Id";
            uiComboBox3.DisplayMember = "Name";
            uiComboBox3.DataSource = infoList;

            dt.Columns.Add("Column1", typeof(string));
            dt.Columns.Add("Column2", typeof(string));
            dt.Columns.Add("Column3", typeof(string));

            for (int i = 0; i < 100; i++)
            {
                dt.Rows.Add("A" + i.ToString("D2"), "B" + (i + 1).ToString("D2"), "C" + (i + 2).ToString("D2"));
            }

            uiComboDataGridView1.DataGridView.Init();
            uiComboDataGridView1.ItemSize = new System.Drawing.Size(360, 240);
            uiComboDataGridView1.DataGridView.AddColumn("Column1", "Column1");
            uiComboDataGridView1.DataGridView.AddColumn("Column2", "Column2");
            uiComboDataGridView1.DataGridView.AddColumn("Column3", "Column3");
            uiComboDataGridView1.DataGridView.ReadOnly = true;
            uiComboDataGridView1.SelectIndexChange += UiComboDataGridView1_SelectIndexChange;
            uiComboDataGridView1.ShowFilter = true;
            uiComboDataGridView1.DataGridView.DataSource = dt;
            uiComboDataGridView1.FilterColumnName = "Column1"; //不设置则全部列过滤

            uiComboDataGridView2.DataGridView.Init();
            uiComboDataGridView2.DataGridView.MultiSelect = true;//设置可多选
            uiComboDataGridView2.ItemSize = new System.Drawing.Size(360, 240);
            uiComboDataGridView2.DataGridView.AddColumn("Column1", "Column1");
            uiComboDataGridView2.DataGridView.AddColumn("Column2", "Column2");
            uiComboDataGridView2.DataGridView.AddColumn("Column3", "Column3");
            uiComboDataGridView2.DataGridView.ReadOnly = true;
            uiComboDataGridView2.ShowFilter = true;
            uiComboDataGridView2.DataGridView.DataSource = dt;
            uiComboDataGridView2.FilterColumnName = "Column1"; //不设置则全部列过滤

            uiComboBox1.SetTipsText(uiToolTip1, "Hello World.");

            //日期选择框文本设置为空
            uiDatePicker3.CanEmpty = true;
            uiDatePicker3.Text = "";
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

            public override string ToString()
            {
                return "ID: " + Id + ", Name: " + Name;
            }
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
            uiComboDataGridView1.Text = "";
            if (value != null && value is DataGridViewRow)
            {
                DataGridViewRow row = (DataGridViewRow)value;
                uiComboDataGridView1.Text = row.Cells["Column1"].Value.ToString();
            }
        }

        private void uiComboDataGridView1_SelectIndexChange_1(object sender, int index)
        {
            uiComboDataGridView1.Text = dt.Rows[index]["Column1"].ToString();
        }

        private void uiDatePicker3_ValueChanged(object sender, DateTime value)
        {
            this.ShowInfoTip(uiDatePicker3.Value.DateString());
            Console.WriteLine(uiDatePicker3.Value);
        }

        private void uiComboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            uiComboBox3.SelectedValue.WriteConsole();
            uiComboBox3.SelectedItem.WriteConsole();
            uiComboBox3.Text.WriteConsole();
        }

        private void uiComboBox4_SelectedValueChanged(object sender, EventArgs e)
        {
            uiComboBox4.SelectedValue.WriteConsole();
            uiComboBox4.SelectedItem.WriteConsole();
            uiComboBox4.Text.WriteConsole();
        }

        private void uiComboDataGridView2_ValueChanged(object sender, object value)
        {
            uiComboDataGridView2.Text = "";
            if (value != null && value is DataGridViewSelectedRowCollection)
            {
                DataGridViewSelectedRowCollection collection = (DataGridViewSelectedRowCollection)value;
                foreach (var item in collection)
                {
                    DataGridViewRow row = (DataGridViewRow)item;
                    uiComboDataGridView2.Text += row.Cells["Column1"].Value.ToString();
                    uiComboDataGridView2.Text += "; ";
                }
            }
        }

        private void uiComboBox1_TipsClick(object sender, EventArgs e)
        {
            this.ShowInfoTip("Hello world.");
        }
    }
}
