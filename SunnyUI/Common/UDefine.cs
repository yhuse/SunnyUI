using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public enum UIDateType
    {
        YearMonthDay,
        YearMonth,
        Year
    }

    public interface IToolTip
    {
        Control ExToolTipControl();
    }

    public enum UILineCap
    {
        None,
        Square,
        Diamond,
        Triangle,
        Circle
    }

    public enum UILineDashStyle
    {
        Solid,
        Dash,
        Dot,
        DashDot,
        DashDotDot,
        Custom,
        None
    }

    public class UITreeNodePainter
    {
        public Color BackColor;
        public Color ForeColor;
    }
}
