using System;
using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    public partial class FContextMenuStrip : UIPage
    {
        public FContextMenuStrip()
        {
            InitializeComponent();

            var styles = UIStyles.PopularStyles();
            foreach (UIStyle style in styles)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(style.DisplayText()) { Tag = style };
                item.Click += Item_Click;
                uiContextMenuStrip1.Items.Add(item);
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.Tag != null && item.Tag is UIStyle)
            {
                UIStyle style = (UIStyle)item.Tag;
                this.Style = style;
            }
        }

        private void uiButton2_Click(object sender, System.EventArgs e)
        {
            uiButton2.ShowContextMenuStrip(uiContextMenuStrip1, 0, uiButton2.Height);
        }
    }
}
