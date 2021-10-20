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
}
