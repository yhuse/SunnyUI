using System;

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
