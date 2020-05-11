using System.Drawing;

namespace Sunny.UI
{
    /// <summary>
    /// 菜单主题样色
    /// </summary>
    public enum UIMenuStyle
    {
        /// <summary>
        /// 自定义
        /// </summary>
        [DisplayText("Custom")]
        Custom,

        /// <summary>
        /// 黑
        /// </summary>
        [DisplayText("Black")]
        Black,

        /// <summary>
        /// 白
        /// </summary>
        [DisplayText("White")]
        White
    }

    public abstract class UIMenuColor
    {
        public abstract UIMenuStyle Style { get; }
        public virtual Color BackColor => Color.FromArgb(56, 56, 56);
        public virtual Color SelectedColor => Color.FromArgb(36, 36, 36);
        public virtual Color UnSelectedForeColor => Color.Silver;
        public virtual Color HoverColor => Color.FromArgb(76, 76, 76);

        public override string ToString()
        {
            return Style.DisplayText();
        }
    }

    public class UIMenuCustomColor : UIMenuColor
    {
        public override UIMenuStyle Style => UIMenuStyle.Custom;
    }

    public class UIMenuBlackColor : UIMenuColor
    {
        public override UIMenuStyle Style => UIMenuStyle.Black;
    }

    public class UIMenuWhiteColor : UIMenuColor
    {
        public override UIMenuStyle Style => UIMenuStyle.White;
        public override Color BackColor => Color.FromArgb(250, 250, 250);
        public override Color SelectedColor => Color.FromArgb(240, 240, 240);
        public override Color UnSelectedForeColor => UIFontColor.Primary;
        public override Color HoverColor => Color.FromArgb(230, 230, 230);
    }
}
