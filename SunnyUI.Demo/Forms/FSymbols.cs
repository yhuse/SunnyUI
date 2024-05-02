using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    public partial class FSymbols : UIPage
    {
        private UISymbolPanel fontAwesomeV4;
        private UISymbolPanel elegantIcons;
        private UISymbolPanel fontAweSomeV6Regular;
        private UISymbolPanel fontAweSomeV6Solid;
        private UISymbolPanel fontAweSomeV6Brands;
        private UISymbolPanel materialIcons;
        private UISymbolPanel searchSymbolPanel;

        public FSymbols()
        {
            InitializeComponent();

            fontAwesomeV4 = new UISymbolPanel(typeof(FontAwesomeIcons), UISymbolType.FontAwesomeV4);
            elegantIcons = new UISymbolPanel(typeof(FontElegantIcons), UISymbolType.FontAwesomeV4);
            fontAweSomeV6Regular = new UISymbolPanel(typeof(FontAweSomeV6Regular), UISymbolType.FontAwesomeV6Regular);
            fontAweSomeV6Solid = new UISymbolPanel(typeof(FontAweSomeV6Solid), UISymbolType.FontAwesomeV6Solid);
            fontAweSomeV6Brands = new UISymbolPanel(typeof(FontAweSomeV6Brands), UISymbolType.FontAwesomeV6Brands);
            materialIcons = new UISymbolPanel(typeof(MaterialIcons), UISymbolType.MaterialIcons);
            searchSymbolPanel = new UISymbolPanel();

            lpV6Solid.Add(fontAweSomeV6Solid);
            lpV6Regular.Add(fontAweSomeV6Regular);
            lpV6Brands.Add(fontAweSomeV6Brands);
            lpElegant.Add(elegantIcons);
            lpMaterialIcons.Add(materialIcons);
            lblResult.Add(searchSymbolPanel);
            lpAwesome.Add(fontAwesomeV4);

            fontAwesomeV4.ValueChanged += CustomSymbolPanel_ValueChanged;
            elegantIcons.ValueChanged += CustomSymbolPanel_ValueChanged;
            fontAweSomeV6Brands.ValueChanged += CustomSymbolPanel_ValueChanged;
            fontAweSomeV6Regular.ValueChanged += CustomSymbolPanel_ValueChanged;
            fontAweSomeV6Solid.ValueChanged += CustomSymbolPanel_ValueChanged;
            materialIcons.ValueChanged += CustomSymbolPanel_ValueChanged;
            searchSymbolPanel.ValueChanged += CustomSymbolPanel_ValueChanged;
        }

        private void CustomSymbolPanel_ValueChanged(object sender, SymbolValue symbol)
        {
            uiTextBox2.IntValue = symbol.Value;
            uiSymbolLabel1.Symbol = symbol.Value;
            this.ShowInfoTip(symbol.ToString());
        }

        private void uiSymbolButton1_Click(object sender, System.EventArgs e)
        {
            string filter = uiTextBox1.Text.Trim();
            fontAwesomeV4.Filter = filter;
            elegantIcons.Filter = filter;
            fontAweSomeV6Regular.Filter = filter;
            fontAweSomeV6Solid.Filter = filter;
            fontAweSomeV6Brands.Filter = filter;
            materialIcons.Filter = filter;
            searchSymbolPanel.Clear();
            if (filter.IsNullOrEmpty()) return;

            for (int i = 0; i < fontAwesomeV4.SymbolCount; i++)
            {
                var value = fontAwesomeV4.Get(i);
                if (value.Name.ToUpper().Contains(filter.ToUpper()))
                    searchSymbolPanel.Add(value);
            }

            for (int i = 0; i < elegantIcons.SymbolCount; i++)
            {
                var value = elegantIcons.Get(i);
                if (value.Name.ToUpper().Contains(filter.ToUpper()))
                    searchSymbolPanel.Add(value);
            }

            for (int i = 0; i < fontAweSomeV6Regular.SymbolCount; i++)
            {
                var value = fontAweSomeV6Regular.Get(i);
                if (value.Name.ToUpper().Contains(filter.ToUpper()))
                    searchSymbolPanel.Add(value);
            }

            for (int i = 0; i < fontAweSomeV6Solid.SymbolCount; i++)
            {
                var value = fontAweSomeV6Solid.Get(i);
                if (value.Name.ToUpper().Contains(filter.ToUpper()))
                    searchSymbolPanel.Add(value);
            }

            for (int i = 0; i < fontAweSomeV6Brands.SymbolCount; i++)
            {
                var value = fontAweSomeV6Brands.Get(i);
                if (value.Name.ToUpper().Contains(filter.ToUpper()))
                    searchSymbolPanel.Add(value);
            }

            for (int i = 0; i < materialIcons.SymbolCount; i++)
            {
                var value = materialIcons.Get(i);
                if (value.Name.ToUpper().Contains(filter.ToUpper()))
                    searchSymbolPanel.Add(value);
            }

            searchSymbolPanel.Invalidate();
            uiTabControl1.SelectedTab = tabPage7;
        }

        private void FSymbols_Shown(object sender, System.EventArgs e)
        {
            uiTabControl1.SelectedTab = tabPage1;
            uiTextBox1.Focus();
        }

        public override void Init()
        {
            base.Init();
            uiTextBox1.Focus();
            searchSymbolPanel.Clear();
        }

        private void uiSymbolButton2_Click(object sender, System.EventArgs e)
        {
            Clipboard.SetText(uiTextBox2.Text);
        }

        private void uiTextBox1_ButtonClick(object sender, System.EventArgs e)
        {
            uiTextBox1.Text = "";
            uiSymbolButton1.PerformClick();
            uiTabControl1.SelectedTab = tabPage1;
        }

        private void uiTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                uiSymbolButton1.PerformClick();
            }
        }
    }
}
