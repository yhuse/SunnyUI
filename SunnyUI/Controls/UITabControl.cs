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
 * 文件名称: UITabControl.cs
 * 文件说明: 标签控件
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public sealed class UITabControl : TabControl, IStyleInterface
    {
        public UITabControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);

            ItemSize = new Size(150, 40);
            DrawMode = TabDrawMode.OwnerDrawFixed;
            Font = UIFontColor.Font;
            AfterSetFillColor(FillColor);
            Size = new Size(450, 270);
            Version = UIGlobal.Version;
        }

        public string Version { get; }

        private Color _fillColor = UIColor.LightBlue;
        private Color tabBackColor = Color.FromArgb(56, 56, 56);

        [DefaultValue(null)]
        public string TagString { get; set; }

        [DefaultValue(false)]
        public bool StyleCustomMode { get; set; }

        private HorizontalAlignment textAlignment = HorizontalAlignment.Center;

        [DefaultValue(HorizontalAlignment.Center)]
        public HorizontalAlignment TextAlignment
        {
            get => textAlignment;
            set
            {
                textAlignment = value;
                Invalidate();
            }
        }

        private bool tabVisible = true;

        [DefaultValue(true)]
        public bool TabVisible
        {
            get => tabVisible;
            set
            {
                tabVisible = value;
                if (!tabVisible)
                {
                    ItemSize = new Size(0, 1);
                }
                else
                {
                    if (ItemSize == new Size(0, 1))
                    {
                        ItemSize = new Size(150, 40);
                    }
                }

                Invalidate();
            }
        }

        /// <summary>
        /// 当使用边框时填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("当使用边框时填充颜色，当值为背景色或透明色或空值则不填充"), Category("自定义")]
        [DefaultValue(typeof(Color), "235, 243, 255")]
        public Color FillColor
        {
            get => _fillColor;
            set
            {
                _fillColor = value;
                AfterSetFillColor(value);
                _style = UIStyle.Custom;
                Invalidate();
            }
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("自定义")]
        [DefaultValue(typeof(Color), "56, 56, 56")]
        public Color TabBackColor
        {
            get => tabBackColor;
            set
            {
                tabBackColor = value;
                _menuStyle = UIMenuStyle.Custom;
                Invalidate();
            }
        }

        private Color tabSelectedColor = Color.FromArgb(36, 36, 36);

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("选中Tab页背景色"), Category("自定义")]
        [DefaultValue(typeof(Color), "36, 36, 36")]
        public Color TabSelectedColor
        {
            get => tabSelectedColor;
            set
            {
                tabSelectedColor = value;
                _menuStyle = UIMenuStyle.Custom;
                Invalidate();
            }
        }

        private Color tabSelectedForeColor = UIColor.Blue;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("选中Tab页字体色"), Category("自定义")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color TabSelectedForeColor
        {
            get => tabSelectedForeColor;
            set
            {
                tabSelectedForeColor = value;
                _style = UIStyle.Custom;
                Invalidate();
            }
        }

        private Color tabUnSelectedForeColor = Color.Silver;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("未选中Tab页字体色"), Category("自定义")]
        [DefaultValue(typeof(Color), "Silver")]
        public Color TabUnSelectedForeColor
        {
            get => tabUnSelectedForeColor;
            set
            {
                tabUnSelectedForeColor = value;
                _menuStyle = UIMenuStyle.Custom;
                Invalidate();
            }
        }

        private Color tabSelectedHighColor = UIColor.Blue;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("选中Tab页高亮"), Category("自定义")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color TabSelectedHighColor

        {
            get => tabSelectedHighColor;
            set
            {
                tabSelectedHighColor = value;
                _style = UIStyle.Custom;
                Invalidate();
            }
        }

        private UIStyle _style = UIStyle.Blue;

        [DefaultValue(UIStyle.Blue)]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rect = base.DisplayRectangle;
                if (tabVisible)
                {
                    return new Rectangle(rect.Left - 4, rect.Top - 4, rect.Width + 8, rect.Height + 8);
                }
                else
                {
                    return new Rectangle(rect.Left - 4, rect.Top - 5, rect.Width + 8, rect.Height + 9);
                }
            }
        }

        private void AfterSetFillColor(Color color)
        {
            foreach (TabPage page in TabPages)
            {
                page.BackColor = color;
            }
        }

        public void SetStyle(UIStyle style)
        {
            SetStyleColor(UIStyles.GetStyleColor(style));
            _style = style;
        }

        public void SetStyleColor(UIBaseStyle uiColor)
        {
            if (uiColor.IsCustom()) return;

            tabSelectedForeColor = tabSelectedHighColor = uiColor.MenuSelectedColor;
            _fillColor = uiColor.PlainColor;
            Invalidate();
        }

        private UIMenuStyle _menuStyle = UIMenuStyle.Black;

        [DefaultValue(UIMenuStyle.Black)]
        public UIMenuStyle MenuStyle
        {
            get => _menuStyle;
            set
            {
                if (value != UIMenuStyle.Custom)
                {
                    SetMenuStyle(UIStyles.MenuColors[value]);
                }

                _menuStyle = value;
            }
        }

        private void SetMenuStyle(UIMenuColor uiColor)
        {
            tabBackColor = uiColor.BackColor;
            tabSelectedColor = uiColor.SelectedColor;
            tabUnSelectedForeColor = uiColor.UnSelectedForeColor;
            Invalidate();
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            DoubleBuffered = true;
            SizeMode = TabSizeMode.Fixed;
            Appearance = TabAppearance.Normal;
            Alignment = TabAlignment.Top;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (e.Control is TabPage)
            {
                //e.Control.BackColor = FillColor;
                e.Control.Padding = new Padding(0);
            }
        }

        private bool showCloseButton;

        [DefaultValue(false)]
        public bool ShowCloseButton
        {
            get => showCloseButton;
            set
            {
                showCloseButton = value;
                Invalidate();
            }
        }

        private bool showActiveCloseButton;

        [DefaultValue(false)]
        public bool ShowActiveCloseButton
        {
            get => showActiveCloseButton;
            set
            {
                showActiveCloseButton = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // 绘制背景色
            e.Graphics.Clear(TabBackColor);

            if (!TabVisible)
            {
                return;
            }

            for (int index = 0; index <= TabCount - 1; index++)
            {
                Rectangle TabRect = new Rectangle(GetTabRect(index).Location.X - 2, GetTabRect(index).Location.Y - 2, ItemSize.Width, ItemSize.Height);

                Bitmap bmp = new Bitmap(TabRect.Width, TabRect.Height);
                Graphics g = Graphics.FromImage(bmp);

                SizeF sf = e.Graphics.MeasureString(TabPages[index].Text, Font);
                int textLeft = ImageList?.ImageSize.Width ?? 0;
                if (ImageList != null) textLeft += 4 + 4 + 6;
                if (TextAlignment == HorizontalAlignment.Right)
                    textLeft = (int)(TabRect.Width - 4 - sf.Width);
                if (TextAlignment == HorizontalAlignment.Center)
                    textLeft = textLeft + (int)((TabRect.Width - textLeft - sf.Width) / 2.0f);

                // 绘制标题
                g.Clear(TabBackColor);
                if (index == SelectedIndex)
                {
                    g.Clear(TabSelectedColor);
                    g.FillRectangle(TabSelectedHighColor, 0, bmp.Height - 4, bmp.Width, 4);
                }

                g.DrawString(TabPages[index].Text, Font, index == SelectedIndex ? tabSelectedForeColor : TabUnSelectedForeColor, textLeft, TabRect.Top + 2 + (TabRect.Height - sf.Height) / 2.0f);
                if (ShowCloseButton || (ShowActiveCloseButton && index == SelectedIndex))
                {
                    g.DrawFontImage(61453, 20, index == SelectedIndex ? tabSelectedForeColor : TabUnSelectedForeColor, new Rectangle(TabRect.Width - 28, TabRect.Top, 24, TabRect.Height));
                }

                // 绘制图标
                if (ImageList != null)
                {
                    int imageIndex = TabPages[index].ImageIndex;
                    if (imageIndex >= 0 && imageIndex < ImageList.Images.Count)
                    {
                        g.DrawImage(ImageList.Images[imageIndex], 4 + 6, TabRect.Y + (TabRect.Height - ImageList.ImageSize.Height) / 2.0f, ImageList.ImageSize.Width, ImageList.ImageSize.Height);
                    }
                }

                if (RightToLeftLayout && RightToLeft == RightToLeft.Yes)
                {
                    bmp = bmp.HorizontalFlip();
                }

                e.Graphics.DrawImage(bmp, TabRect.Left, TabRect.Top);
                bmp.Dispose();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            int removeIndex = -1;
            for (int index = 0; index <= TabCount - 1; index++)
            {
                Rectangle TabRect = new Rectangle(GetTabRect(index).Location.X - 2, GetTabRect(index).Location.Y - 2, ItemSize.Width, ItemSize.Height);
                Rectangle rect = new Rectangle(TabRect.Right - 28, TabRect.Top, 24, TabRect.Height);
                if (e.Location.InRect(rect))
                {
                    removeIndex = index;
                    break;
                }
            }

            if (removeIndex < 0 || removeIndex >= TabCount)
            {
                return;
            }

            if (ShowCloseButton || (ShowActiveCloseButton && removeIndex == SelectedIndex))
            {
                if (BeforeRemoveTabPage == null || (BeforeRemoveTabPage != null && BeforeRemoveTabPage.Invoke(this, removeIndex)))
                {
                    RemoveTabPage(removeIndex);
                }
            }
        }

        public delegate bool OnBeforeRemoveTabPage(object sender, int index);
        public delegate void OnAfterRemoveTabPage(object sender, int index);

        public event OnBeforeRemoveTabPage BeforeRemoveTabPage;
        public event OnAfterRemoveTabPage AfterRemoveTabPage;

        internal void RemoveTabPage(int index)
        {
            if (index < 0 || index >= TabCount)
            {
                return;
            }

            TabPages.Remove(TabPages[index]);
            AfterRemoveTabPage?.Invoke(this, index);

            if (TabCount == 0) return;
            if (index == 0) SelectedIndex = 0;
            if (index > 0) SelectedIndex = index - 1;
        }

        public enum UITabPosition
        {
            Left,
            Right
        }

        [DefaultValue(UITabPosition.Left)]
        public UITabPosition TabPosition
        {
            get => (RightToLeftLayout && RightToLeft == RightToLeft.Yes)
                ? UITabPosition.Right
                : UITabPosition.Left;
            set
            {
                RightToLeftLayout = value == UITabPosition.Right;
                RightToLeft = (value == UITabPosition.Right) ? RightToLeft.Yes : RightToLeft.No;
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            Init(SelectedIndex);
        }

        public void Init(int index = 0)
        {
            if (index < 0 || index >= TabPages.Count)
            {
                return;
            }

            if (SelectedIndex != index)
                SelectedIndex = index;

            List<UIPage> pages = TabPages[SelectedIndex].GetControls<UIPage>();
            foreach (var page in pages)
            {
                page.Init();
            }

            List<UITabControlMenu> leftTabControls = TabPages[SelectedIndex].GetControls<UITabControlMenu>();
            foreach (var tabControl in leftTabControls)
            {
                tabControl.Init();
            }

            List<UITabControl> topTabControls = TabPages[SelectedIndex].GetControls<UITabControl>();
            foreach (var tabControl in topTabControls)
            {
                tabControl.Init();
            }
        }
    }
}