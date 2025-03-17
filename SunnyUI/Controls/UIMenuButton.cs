/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2025 ShenYongHua(沈永华).
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
 * 文件名称: UIMenuButton.cs
 * 文件说明: 下拉菜单按钮
 * 当前版本: V3.8
 * 创建日期: 2024-12-16
 *
 * 2024-12-16: V3.8.0 增加文件说明
 * 2025-01-15: V3.8.1 更改属性描述
******************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [Description("下拉菜单按钮控件")]
    public class UIMenuButton : UISymbolButton
    {
        private bool _showDropArrow = true;

        [Description("当用户左键点击按钮时，显示的快捷菜单"), Category("SunnyUI")]
        public UIContextMenuStrip Menu { get; set; }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Left)
            {
                Menu?.Show(this, 0, Height);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (ShowDropArrow)
            {
                e.Graphics.DrawFontImage(61703, SymbolSize, GetForeColor(), new Rectangle(Width - SymbolSize - 4, 0, SymbolSize, Height));
            }
        }

        /// <summary>
        /// 字体图标
        /// </summary>
        [DefaultValue(true)]
        [Description("显示下拉按钮"), Category("SunnyUI")]
        public bool ShowDropArrow
        {
            get => _showDropArrow;
            set
            {
                _showDropArrow = value;
                Invalidate();
            }
        }
    }
}
