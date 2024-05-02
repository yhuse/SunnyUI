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
 * 文件名称: UIContextMenuStrip.cs
 * 文件说明: 上下文菜单
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2022-03-19: V3.1.1 重构主题配色
 * 2023-10-17: V3.5.1 修正文字显示垂直居中
 * 2023-10-17: V3.5.1 当右键菜单未绑定ImageList，并且ImageIndex>0时，将ImageIndex绑定为Symbol绘制
 * 2024-02-21: V3.6.3 修复显示快捷键文本位置
 * 2024-02-22: V3.6.3 节点AutoSize时不重绘，重绘时考虑Enabled为False时颜色显示
******************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public sealed class UIContextMenuStrip : ContextMenuStrip, IStyleInterface, IZoomScale
    {
        private ContextMenuColorTable ColorTable = new ContextMenuColorTable();

        public UIContextMenuStrip()
        {
            Font = UIStyles.Font();
            //RenderMode = ToolStripRenderMode.Custom;
            Renderer = new UIToolStripRenderer(ColorTable);
            Version = UIGlobal.Version;

            ColorTable.SetStyleColor(UIStyles.Blue);
            BackColor = UIStyles.Blue.ContextMenuColor;
        }

        /// <summary>
        /// 禁止控件跟随窗体缩放
        /// </summary>
        [DefaultValue(false), Category("SunnyUI"), Description("禁止控件跟随窗体缩放")]
        public bool ZoomScaleDisabled { get; set; }

        /// <summary>
        /// 控件缩放前在其容器里的位置
        /// </summary>
        [Browsable(false), DefaultValue(typeof(Rectangle), "0, 0, 0, 0")]
        public Rectangle ZoomScaleRect { get; set; }

        /// <summary>
        /// 设置控件缩放比例
        /// </summary>
        /// <param name="scale">缩放比例</param>
        public void SetZoomScale(float scale)
        {

        }

        private float DefaultFontSize = -1;

        public void SetDPIScale()
        {
            if (!UIDPIScale.NeedSetDPIFont()) return;
            if (DefaultFontSize < 0) DefaultFontSize = this.Font.Size;
            this.SetDPIScaleFont(DefaultFontSize);
        }

        protected override void OnOpening(CancelEventArgs e)
        {
            base.OnOpening(e);
            SetDPIScale();
        }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false), Browsable(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="style">主题样式</param>
        private void SetStyle(UIStyle style)
        {
            if (!style.IsCustom())
            {
                SetStyleColor(style.Colors());
                Invalidate();
            }

            _style = style == UIStyle.Inherited ? UIStyle.Inherited : UIStyle.Custom;
        }

        public void SetInheritedStyle(UIStyle style)
        {
            SetStyle(style);
            _style = UIStyle.Inherited;
        }

        public void SetStyleColor(UIBaseStyle uiColor)
        {
            ColorTable.SetStyleColor(uiColor);
            BackColor = uiColor.ContextMenuColor;
            ForeColor = uiColor.ContextMenuForeColor;
        }

        public string Version { get; }

        private UIStyle _style = UIStyle.Inherited;

        /// <summary>
        /// 主题样式
        /// </summary>
        [DefaultValue(UIStyle.Inherited), Description("主题样式"), Category("SunnyUI")]
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

    internal class UIToolStripRenderer : ToolStripProfessionalRenderer
    {
        public UIToolStripRenderer() { }

        public UIToolStripRenderer(ProfessionalColorTable professionalColorTable) : base(professionalColorTable) { }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e.Item.AutoSize)
            {
                base.OnRenderItemText(e);
                return;
            }

            //调整文本区域的位置和大小以实现垂直居中
            Rectangle textRect = new Rectangle(e.TextRectangle.Left, e.Item.ContentRectangle.Top, e.TextRectangle.Width, e.Item.ContentRectangle.Height);
            ToolStripMenuItem stripItem = (ToolStripMenuItem)e.Item;
            Rectangle backRect = new Rectangle(e.Item.Bounds.Left + 2, e.Item.Bounds.Top - 2, e.Item.Bounds.Width - 4, e.Item.Bounds.Height);

            if (e.Item.Enabled)
            {
                if (e.Item.Selected)
                {
                    e.Graphics.FillRectangle(ColorTable.MenuItemSelected, backRect);
                }
                else
                {
                    e.Graphics.FillRectangle(ColorTable.ImageMarginGradientBegin, backRect);
                }
            }
            else
            {
                e.Graphics.FillRectangle(ColorTable.ImageMarginGradientBegin, backRect);
            }

            Color textColor = e.TextColor;
            if (!e.Item.Enabled) textColor = Color.Gray;
            //设置文本绘制格式
            TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.Left;
            TextRenderer.DrawText(e.Graphics, e.Item.Text, e.TextFont, textRect, textColor, flags);

            if (stripItem.ShowShortcutKeys)
            {
                flags = TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.Right;
                TextRenderer.DrawText(e.Graphics, ShortcutToText(stripItem.ShortcutKeys, stripItem.ShortcutKeyDisplayString), e.TextFont, textRect, textColor, flags);
            }

            //当右键菜单未绑定ImageList，并且ImageIndex>0时，将ImageIndex绑定为Symbol绘制
            ToolStripItem item = e.Item;
            while (!(item.Owner is ContextMenuStrip))
            {
                if (item.Owner is ToolStripDropDownMenu)
                    item = item.OwnerItem;
            }

            ContextMenuStrip ownerContextMenu = item.Owner as ContextMenuStrip;
            if (ownerContextMenu.ImageList != null) return;
            if (e.Item.ImageIndex <= 0) return;
            if (e.Item.Image != null) return;
            Rectangle imageRect = new Rectangle(0, e.Item.ContentRectangle.Top, e.TextRectangle.Left, e.Item.ContentRectangle.Height);
            e.Graphics.DrawFontImage(e.Item.ImageIndex, 24, e.TextColor, imageRect);
        }

        internal static string ShortcutToText(Keys shortcutKeys, string shortcutKeyDisplayString)
        {
            if (!string.IsNullOrEmpty(shortcutKeyDisplayString))
            {
                return shortcutKeyDisplayString;
            }

            if (shortcutKeys == Keys.None)
            {
                return string.Empty;
            }

            //KeysConverter kc = new KeysConverter();
            //kc.ConvertToString(item1.ShortcutKeys).WriteConsole();

            return TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString(shortcutKeys);
        }
    }

    internal class ContextMenuColorTable : ProfessionalColorTable
    {
        private UIBaseStyle StyleColor = UIStyles.GetStyleColor(UIStyle.Blue);

        public void SetStyleColor(UIBaseStyle color)
        {
            StyleColor = color;
        }

        public override Color MenuItemSelected => StyleColor.ContextMenuSelectedColor;

        public override Color MenuItemPressedGradientBegin => StyleColor.ButtonFillPressColor;

        public override Color MenuItemPressedGradientMiddle => StyleColor.ButtonFillPressColor;

        public override Color MenuItemPressedGradientEnd => StyleColor.ButtonFillPressColor;

        public override Color MenuBorder => StyleColor.ButtonRectColor;

        public override Color MenuItemBorder => StyleColor.PrimaryColor;

        public override Color ImageMarginGradientBegin => StyleColor.ContextMenuColor;

        public override Color ImageMarginGradientEnd => StyleColor.ContextMenuColor;

        public override Color ImageMarginGradientMiddle => StyleColor.ContextMenuColor;
    }
}