using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 日期控件选择类型
    /// </summary>
    public enum UIDateType
    {
        /// <summary>
        /// 年月日
        /// </summary>
        YearMonthDay,

        /// <summary>
        /// 年月
        /// </summary>
        YearMonth,

        /// <summary>
        /// 年
        /// </summary>
        Year
    }

    /// <summary>
    /// 接口
    /// </summary>
    public interface IToolTip
    {
        /// <summary>
        /// 需要额外设置工具提示的控件
        /// </summary>
        /// <returns>控件</returns>
        Control ExToolTipControl();
    }

    /// <summary>
    /// 线头部类型
    /// </summary>
    public enum UILineCap
    {
        /// <summary>
        /// 无
        /// </summary>
        None,

        /// <summary>
        /// 正方形
        /// </summary>
        Square,

        /// <summary>
        /// 菱形
        /// </summary>
        Diamond,

        /// <summary>
        /// 三角形
        /// </summary>
        Triangle,

        /// <summary>
        /// 圆形
        /// </summary>
        Circle
    }

    /// <summary>
    /// 线型
    /// </summary>
    public enum UILineDashStyle
    {
        /// <summary>
        /// 实线
        /// </summary>
        Solid,

        /// <summary>
        /// 虚线
        /// </summary>
        Dash,

        /// <summary>
        /// 由重复的点图案构成的直线
        /// </summary>
        Dot,

        /// <summary>
        /// 由重复的点划线图案构成的直线
        /// </summary>
        DashDot,

        /// <summary>
        /// 由重复的双点划线图案构成的直线
        /// </summary>
        DashDotDot,

        /// <summary>
        /// 自定义
        /// </summary>
        Custom,

        /// <summary>
        /// 不设置线型
        /// </summary>
        None
    }

    /// <summary>
    /// 树节点绘制
    /// </summary>
    public class UITreeNodePainter
    {
        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// 前景色
        /// </summary>
        public Color ForeColor { get; set; }

        public Color HoverColor { get; set; }

        public Color SelectedColor { get; set; }

        public Color SelectedForeColor { get; set; }

        public bool HaveHoveColor { get; set; } = false;
    }
}
