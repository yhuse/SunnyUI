using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    public partial class FSplitContainer : UIPage
    {
        public FSplitContainer()
        {
            InitializeComponent();

            uiNavMenuEx1.AddNodeRightSymbol(uiNavMenuEx1.Nodes[0], 61452);
            uiNavMenuEx1.AddNodeRightSymbol(uiNavMenuEx1.Nodes[0], 61453);

            uiNavMenuEx1.AddNodeRightSymbol(uiNavMenuEx1.Nodes[1], 61454);
            uiNavMenuEx1.AddNodeRightSymbol(uiNavMenuEx1.Nodes[1], 61450);

            uiNavMenuEx1.AddNodeRightSymbol(uiNavMenuEx1.Nodes[0].Nodes[0], 61452);
            uiNavMenuEx1.AddNodeRightSymbol(uiNavMenuEx1.Nodes[0].Nodes[0], 61453);

            uiNavMenuEx1.AddNodeRightSymbol(uiNavMenuEx1.Nodes[0].Nodes[2], 61452);
            uiNavMenuEx1.AddNodeRightSymbol(uiNavMenuEx1.Nodes[0].Nodes[2], 61453);

            uiNavMenuEx1.AddNodeRightSymbol(uiNavMenuEx1.Nodes[3], 61450);

            //uiNavMenuEx1.SetNodeSymbol(uiNavMenuEx1.Nodes[0], 61450);
            uiNavMenuEx1.SetNodeImageIndex(uiNavMenuEx1.Nodes[0], 1);
        }

        private void uiSplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void uiSymbolButton1_Click(object sender, System.EventArgs e)
        {

        }

        private void uiNavMenuEx1_NodeRightSymbolClick(object sender, TreeNode node, int index, int symbol)
        {
            uiListBox1.Items.Add(node.Text + ", " + index + ", " + symbol);
        }

        private void uiButton1_Click(object sender, System.EventArgs e)
        {
            uiListBox1.Items.Clear();
        }
    }
}
