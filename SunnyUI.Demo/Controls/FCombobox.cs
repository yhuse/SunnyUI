using System;

namespace Sunny.UI.Demo
{
    public partial class FCombobox : UITitlePage
    {
        public FCombobox()
        {
            InitializeComponent();
            uiDatePicker1.Value = DateTime.Today;
            uiTimePicker1.Value = DateTime.Now;
            uiDatetimePicker1.Value = DateTime.Now;
        }
    }
}
