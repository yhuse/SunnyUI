using System;
using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    public partial class FFlowLayoutPanel : UIPage
    {
        public FFlowLayoutPanel()
        {
            InitializeComponent();
        }

        public override void Init()
        {
            base.Init();
            uiFlowLayoutPanel1.Clear();
            index = 0;

            for (int i = 0; i < 10; i++)
            {
                uiButton1.PerformClick();
            }
        }

        private int index;
        UIButton btn;
        private void uiButton1_Click(object sender, System.EventArgs e)
        {
            btn = new UIButton();
            btn.SetDPIScale();
            btn.Text = "Button" + index++.ToString("D2");
            btn.Name = btn.Text;
            btn.Click += Btn_Click;

            //建议用封装的方法Add
            uiFlowLayoutPanel1.Add(btn);

            //或者Panel.Controls.Add
            //uiFlowLayoutPanel1.Panel.Controls.Add(btn);

            //不能用原生方法Controls.Add
            //uiFlowLayoutPanel1.Controls.Add(btn);    

            var box = new UIGroupBox();
            box.Width = 100;
            box.Height = uiFlowLayoutPanel2.Height - 32;
            box.SetDPIScale();
            box.Text = "Button" + index++.ToString("D2");
            box.Name = box.Text;
            uiFlowLayoutPanel2.Add(box);

            uiButton3.Enabled = true;
        }

        private void Btn_Click(object sender, System.EventArgs e)
        {
            var button = (UIButton)sender;
            ShowInfoTip(button.Text);
        }

        private void uiButton2_Click(object sender, System.EventArgs e)
        {
            //清除用Clear方法
            uiFlowLayoutPanel1.Clear();
            uiFlowLayoutPanel2.Clear();
            //或者用
            //uiFlowLayoutPanel1.Panel.Controls.Clear();

            //不能用原生方法Controls.Clear
            //uiFlowLayoutPanel1.Controls.Clear();

            uiButton3.Enabled = false;
        }

        private void uiButton3_Click(object sender, System.EventArgs e)
        {
            if (btn != null)
            {
                //移除用Remove方法
                uiFlowLayoutPanel1.Remove(btn);
                //或者用
                //uiFlowLayoutPanel1.Panel.Controls.Remove(btn);

                //不能用原生方法Controls.Remove
                //uiFlowLayoutPanel1.Controls.Remove(btn);

                btn = null;
            }

            uiButton3.Enabled = false;
        }

        private void uiButton4_Click(object sender, System.EventArgs e)
        {
            //根据名称获取
            var btn = uiFlowLayoutPanel1.Get("Button01");

            //通过控件名称索引获取
            btn = uiFlowLayoutPanel1["Button01"];

            //通过名称和类型获取
            UIButton button = uiFlowLayoutPanel1.Get<UIButton>("Button01");

            //遍历，与原生方法不同，UIFlowLayoutPanel是组合控件，从其Panel控件遍历
            foreach (Control item in uiFlowLayoutPanel1.Panel.Controls)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
