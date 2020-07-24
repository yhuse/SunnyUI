using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    public partial class FScrollBar : UITitlePage
    {
        public FScrollBar()
        {
            InitializeComponent();
        }

        public override void Init()
        {
            value = 0;
            timer1.ReStart();
        }

        private int value;
        private void timer1_Tick(object sender, EventArgs e)
        {
            value++;
            uiScrollBar1.Value = value;
            uiHorScrollBar1.Value = value;
            uiScrollBar2.Value = value/3;
            uiHorScrollBar2.Value = value/3;
            uiScrollBar3.Value = value / 10;
            uiHorScrollBar3.Value = value / 10;
        }
    }
}
