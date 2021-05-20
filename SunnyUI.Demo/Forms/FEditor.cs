using System;
using System.Collections.Generic;

namespace Sunny.UI.Demo.Forms
{
    public partial class FEditor : UITitlePage
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

            UIEditOption option = new UIEditOption();
            option.AutoLabelWidth = true;
            option.Text = "增加";
            option.AddText("Name", "姓名", null, true);
            option.AddInteger("Age", "年龄", 16);
            option.AddDate("Birthday", "生日", DateTime.Now);
            option.AddCombobox("Sex", "性别", sex, 1, true, true);
            option.AddCombobox("Info", "关联", infoList, "Name", "Id", "2");
            option.AddSwitch("Switch", "选择", false);

            UIEditForm frm = new UIEditForm(option);
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
            }

            frm.Dispose();
        }

        private bool Frm_CheckedData(object sender, UIEditForm.EditFormEventArgs e)
        {
            if (e.Form["Age"].ToString().ToInt() < 18 || e.Form["Age"].ToString().ToInt() > 60)
            {
                e.Form.SetEditorFocus("Age");
                ShowWarningTip("年龄范围为18到60岁");
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
