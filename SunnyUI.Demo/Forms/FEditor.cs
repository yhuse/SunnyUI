using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    public partial class FEditor : UIPage
    {
        public FEditor()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            Person person = new Person();
            person.Name = "SunnyUI";
            person.Sex = Sex.Male;
            person.Age = 18;
            person.Department = "研发部";
            person.Birthday = new DateTime(2002, 1, 1);

            FEdit frm = new FEdit();
            frm.Render();
            frm.Person = person;
            frm.ShowDialog();
            if (frm.IsOK)
            {
                this.ShowSuccessDialog(frm.Person.ToString());
            }

            frm.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FEdit frm = new FEdit();
            frm.Render();
            frm.ShowDialog();
            if (frm.IsOK)
            {
                this.ShowSuccessDialog(frm.Person.ToString());
            }

            frm.Dispose();
        }

        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            List<FCombobox.Info> infoList = new List<FCombobox.Info>();
            FCombobox.Info info1 = new FCombobox.Info() { Id = "1", Name = "张三" };
            FCombobox.Info info2 = new FCombobox.Info() { Id = "2", Name = "李四" };
            FCombobox.Info info3 = new FCombobox.Info() { Id = "3", Name = "王五" };
            infoList.Add(info1);
            infoList.Add(info2);
            infoList.Add(info3);

            string[] sex = new[] { "男", "女" };
            ComboCheckedListBoxItem[] checkedItems = new ComboCheckedListBoxItem[4]
            {
                new ComboCheckedListBoxItem("AAA",false),
                       new ComboCheckedListBoxItem("BBB",false),
                              new ComboCheckedListBoxItem("CCC",true),
                                     new ComboCheckedListBoxItem("DDD",false)
            };

            TreeNode[] nodes = new TreeNode[3];
            nodes[0] = new TreeNode("AA");
            nodes[1] = new TreeNode("BB");
            nodes[2] = new TreeNode("CC");
            nodes[0].Nodes.Add("AA11");
            nodes[0].Nodes.Add("AA22");
            nodes[0].Nodes.Add("AA33");
            nodes[1].Nodes.Add("BB11");
            nodes[1].Nodes.Add("BB22");

            UIEditOption option = new UIEditOption();
            option.AutoLabelWidth = true;
            option.Text = "增加";
            option.AddText("Name", "姓名", null, true);
            option.AddInteger("Age", "年龄", 16);
            option.AddDate("Birthday", "生日", DateTime.Now);
            option.AddCombobox("Sex", "性别", sex, 1, true, true);
            option.AddCombobox("Info", "关联", infoList, "Name", "Id", "2");
            option.AddSwitch("Switch", "选择", false, "打开", "关闭");
            option.AddComboTreeView("ComboTree", "选择", nodes, nodes[1].Nodes[1]);
            option.AddComboCheckedListBox("checkedList", "选择", checkedItems, "CCC");

            UIEditForm frm = new UIEditForm(option);
            frm.Render();
            frm.CheckedData += Frm_CheckedData;
            frm.ShowDialog();

            if (frm.IsOK)
            {
                Console.WriteLine("姓名: " + frm["Name"]);
                Console.WriteLine("年龄: " + frm["Age"]);
                Console.WriteLine("生日: " + frm["Birthday"]);
                Console.WriteLine("性别: " + sex[(int)frm["Sex"]]);
                Console.WriteLine("关联: " + frm["Info"]);
                Console.WriteLine("选择: " + frm["Switch"]);
                Console.WriteLine("选择: " + frm["ComboTree"]);

                var outCheckedItems = (ComboCheckedListBoxItem[])frm["checkedList"];
                foreach (var item in outCheckedItems)
                {
                    Console.WriteLine(item.Text, item.Checked);
                }
            }

            frm.Dispose();
        }

        private bool Frm_CheckedData(object sender, UIEditForm.EditFormEventArgs e)
        {
            if (e.Form["Age"].ToString().ToInt() < 18 || e.Form["Age"].ToString().ToInt() > 60)
            {
                e.Form.SetEditorFocus("Age");
                this.ShowWarningTip("年龄范围为18到60岁");
                return false;
            }

            return true;
        }
    }

    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Sex Sex { get; set; }

        public string Department { get; set; }

        public DateTime Birthday { get; set; }

        public string Address { get; set; }

        public override string ToString()
        {
            return Name + ", " + Age + ", " + Department;
        }
    }

    public enum Sex
    {
        Male,
        Female
    }
}
