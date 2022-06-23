using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(false)]
    public class UITransparentPanel : UIPanel
    {
        private int _opacity = 150;

        public UITransparentPanel()
        {
            SetStyleFlags(true, false);
        }

        [Bindable(true), Category("Custom"), DefaultValue(150), Description("背景的透明度. 有效值0-255")]
        public int Opacity
        {
            get { return _opacity; }
            set
            {
                if (value > 255) value = 255;
                else if (value < 0) value = 0;
                _opacity = value;
                Invalidate();
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected override void AfterSetFillColor(Color color)
        {
            base.AfterSetFillColor(color);
            BackColor = Color.FromArgb(Opacity, FillColor);
        }

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
        }
    }
}