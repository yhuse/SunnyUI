using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace Sunny.UI
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    public sealed partial class UIStringUpDown : UIPanel
    {
        private readonly UITextBox edit = new UITextBox();

        private string _value;

        private int valuesSelectedIndex;
        private Color pnlColor;
        private List<string> _values = new List<string>();

        public UIStringUpDown()
        {
            InitializeComponent();
            ShowText = false;
            edit.Type = UITextBox.UIEditType.String;
            edit.Parent = pnlValue;
            edit.Visible = false;
            edit.TextChanged += Edit_TextChanged;
            edit.Leave += Edit_Leave;
        }

        [Description("选中项")]
        [Category("SunnyUI")]
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                pnlValue.Text = _value?.Trim();
                ValueChanged?.Invoke(this, new ValueChangedEventArgs(_value));
            }
        }

        [Description("可选项")]
        [Category("SunnyUI")]
        public List<string> Values
        {
            set
            {
                _values = value;
                Value = _values.FirstOrDefault();
            }
            get => _values;
        }


        private void Edit_Leave(object sender, EventArgs e)
        {
            if (edit.Visible)
            {
                edit.Visible = false;
                pnlValue.FillColor = pnlColor;
            }
        }

        private void Edit_TextChanged(object sender, EventArgs e)
        {
            if (edit != null && edit.Visible) Value = edit.Text;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (pnlValue != null) pnlValue.Font = Font;
            if (edit != null) edit.Font = Font;
        }

        public event EventHandler ValueChanged;

        private string NextValue()
        {
            if (!Values.Any())
                return string.Empty;
            valuesSelectedIndex++;
            if (valuesSelectedIndex > Values.Count - 1)
                valuesSelectedIndex = 0;
            return Values[valuesSelectedIndex];
        }

        private string PreviousValue()
        {
            if (!Values.Any())
                return string.Empty;
            valuesSelectedIndex--;
            if (valuesSelectedIndex < 0)
                valuesSelectedIndex = Value.Length - 1;
            return Values[valuesSelectedIndex];
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Value = NextValue();
            if (edit.Visible)
            {
                edit.Visible = false;
                pnlValue.FillColor = pnlColor;
            }
        }

        private void btnDec_Click(object sender, EventArgs e)
        {
            Value = PreviousValue();
            if (edit.Visible)
            {
                edit.Visible = false;
                pnlValue.FillColor = pnlColor;
            }
        }

        private void pnlValue_DoubleClick(object sender, EventArgs e)
        {
            edit.Left = 1;
            edit.Top = (pnlValue.Height - edit.Height) / 2;
            edit.Width = pnlValue.Width - 2;
            pnlColor = pnlValue.FillColor;
            pnlValue.FillColor = Color.White;
            edit.TextAlignment = ContentAlignment.MiddleCenter;
            edit.Text = pnlValue.Text;
            edit.Visible = true;
            edit.BringToFront();
        }

        public class ValueChangedEventArgs : EventArgs
        {
            public ValueChangedEventArgs()
            {
            }

            public ValueChangedEventArgs(string value)
            {
                Value = value;
            }

            public string Value { get; set; }
        }
    }
}