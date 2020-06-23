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
    }
}
