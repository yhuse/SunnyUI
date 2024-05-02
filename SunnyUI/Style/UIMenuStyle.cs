/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UIMenuStyle.cs
 * 文件说明: 控件样式定义类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

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
        public virtual Color SelectedColor2 => Color.FromArgb(36, 36, 36);
        public virtual Color UnSelectedForeColor => Color.FromArgb(240, 240, 240);
        public virtual Color HoverColor => Color.FromArgb(76, 76, 76);
        public virtual Color SecondBackColor => Color.FromArgb(66, 66, 66);

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
        public override Color BackColor => Color.FromArgb(240, 240, 240);
        public override Color SelectedColor => Color.FromArgb(250, 250, 250);
        public override Color SelectedColor2 => Color.FromArgb(250, 250, 250);
        public override Color UnSelectedForeColor => UIFontColor.Primary;
        public override Color HoverColor => Color.FromArgb(230, 230, 230);
        public override Color SecondBackColor => Color.FromArgb(235, 235, 235);
    }
}