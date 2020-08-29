/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2020 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@qq.com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UIContextMenuStrip.cs
 * 文件说明: 上下文菜单
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
******************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public sealed class UIContextMenuStrip : ContextMenuStrip, IStyleInterface
    {
        public ContextMenuColorTable ColorTable = new ContextMenuColorTable();

        public UIContextMenuStrip()
        {
            Font = UIFontColor.Font;
            RenderMode = ToolStripRenderMode.Professional;
            Renderer = new ToolStripProfessionalRenderer(ColorTable);
            Version = UIGlobal.Version;
        }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

        public void SetStyle(UIStyle style)
        {
            SetStyleColor(UIStyles.GetStyleColor(style));
            _style = style;
        }

        public void SetStyleColor(UIBaseStyle uiColor)
        {
            if (uiColor.IsCustom()) return;

            ColorTable.SetStyleColor(uiColor);
            BackColor = uiColor.ContextMenuColor;
            Invalidate();
        }

        public string Version { get; }

        private UIStyle _style = UIStyle.Blue;

        /// <summary>
        /// 主题样式
        /// </summary>
        [DefaultValue(UIStyle.Blue), Description("主题样式"), Category("SunnyUI")]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        public void CalcHeight()
        {
            if (Items.Count > 0 && !AutoSize)
            {
                int height = 0;
                foreach (ToolStripItem item in Items)
                {
                    height += item.Height;
                }

                Height = height + 4;
            }
        }
    }

    public class ContextMenuColorTable : ProfessionalColorTable
    {
        private UIBaseStyle StyleColor = UIStyles.Blue;

        public void SetStyleColor(UIBaseStyle color)
        {
            StyleColor = color;
        }

        public override Color MenuItemSelected => StyleColor.MenuSelectedColor;

        public override Color MenuItemPressedGradientBegin => StyleColor.ButtonFillPressColor;

        public override Color MenuItemPressedGradientMiddle => StyleColor.ButtonFillPressColor;

        public override Color MenuItemPressedGradientEnd => StyleColor.ButtonFillPressColor;

        public override Color MenuBorder => StyleColor.RectColor;

        public override Color MenuItemBorder => StyleColor.PrimaryColor;

        public override Color ImageMarginGradientBegin => StyleColor.ContextMenuColor;

        public override Color ImageMarginGradientEnd => StyleColor.ContextMenuColor;

        public override Color ImageMarginGradientMiddle => StyleColor.ContextMenuColor;
    }
}