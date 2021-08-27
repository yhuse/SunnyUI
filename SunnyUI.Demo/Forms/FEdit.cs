namespace Sunny.UI.Demo
{
    public partial class FEdit : UIEditForm
    {
        public FEdit()
        {
            InitializeComponent();
        }

        protected override bool CheckData()
        {
            return CheckEmpty(edtName, "请输入姓名")
                   && CheckEmpty(edtAge, "请输入年龄")
                   && CheckRange(edtAge, 18, 60, "输入年龄范围18~60")
                   && CheckEmpty(cbDepartment, "请选择部门")
                   && CheckEmpty(edtDate, "请选择生日");
        }

        private Person person;

        public Person Person
        {
            get
            {
                if (person == null)
                {
                    person = new Person();
                }

                person.Name = edtName.Text;
                person.Age = edtAge.IntValue;
                person.Birthday = edtDate.Value;
                person.Address = edtAddress.Text;
                if (rbMale.Checked)
                    person.Sex = Sex.Male;
                if (rbFemale.Checked)
                    person.Sex = Sex.Female;
                person.Department = cbDepartment.Text;
                return person;
            }

            set
            {
                person = value;
                edtName.Text = value.Name;
                edtAge.IntValue = value.Age;
                edtDate.Value = value.Birthday;
                edtAddress.Text = value.Address;
                cbDepartment.SelectedIndex = cbDepartment.Items.IndexOf(value.Department);
                rbMale.Checked = value.Sex == Sex.Male;
                rbFemale.Checked = value.Sex == Sex.Female;
            }
        }
    }
}