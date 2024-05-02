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
 * 文件名称: UITabControlMenu.cs
 * 文件说明: 标签菜单控件
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2022-08-11: V3.0.2 重写ItemSize，将宽、高调整为正常显示
 * 2023-05-12: V3.3.6 重构DrawString函数
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public sealed class UITabControlMenu : TabControl, IStyleInterface, IZoomScale
    {
        public UITabControlMenu()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            UpdateStyles();

            base.ItemSize = new Size(40, 200);
            DrawMode = TabDrawMode.OwnerDrawFixed;
            base.Font = UIStyles.Font();
            AfterSetFillColor(FillColor);
            Size = new Size(450, 270);
            Version = UIGlobal.Version;

            tabSelectedForeColor = UIStyles.Blue.TabControlTabSelectedColor;
            tabSelectedHighColor = UIStyles.Blue.TabControlTabSelectedColor;
            _fillColor = UIStyles.Blue.TabControlBackColor;
        }

        [DefaultValue(typeof(Size), "200, 40")]
        public new Size ItemSize
        {
            get => new Size(base.ItemSize.Height, base.ItemSize.Width);
            set
            {
                base.ItemSize = new Size(value.Height, value.Width);
                Invalidate();
            }
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
            if (DesignMode) return;
            if (!UIDPIScale.NeedSetDPIFont()) return;
            if (DefaultFontSize < 0) DefaultFontSize = this.Font.Size;
            this.SetDPIScaleFont(DefaultFontSize);
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

        public string Version { get; }

        private HorizontalAlignment textAlignment = HorizontalAlignment.Center;

        [Description("文字显示方向"), Category("SunnyUI")]
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

        private Color _fillColor = UIColor.LightBlue;
        private Color tabBackColor = Color.FromArgb(56, 56, 56);

        /// <summary>
        /// 当使用边框时填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("当使用边框时填充颜色，当值为背景色或透明色或空值则不填充"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "243, 249, 255")]
        public Color FillColor
        {
            get => _fillColor;
            set
            {
                if (_fillColor != value)
                {
                    _fillColor = value;
                    AfterSetFillColor(value);
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "56, 56, 56")]
        public Color TabBackColor
        {
            get => tabBackColor;
            set
            {
                if (tabBackColor != value)
                {
                    tabBackColor = value;
                    _menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }

        private Color tabSelectedColor = Color.FromArgb(36, 36, 36);

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("选中Tab页背景色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "36, 36, 36")]
        public Color TabSelectedColor
        {
            get => tabSelectedColor;
            set
            {
                if (tabSelectedColor != value)
                {
                    tabSelectedColor = value;
                    _menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }

        private Color tabSelectedForeColor = UIColor.Blue;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("选中Tab页字体色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color TabSelectedForeColor
        {
            get => tabSelectedForeColor;
            set
            {
                if (tabSelectedForeColor != value)
                {
                    tabSelectedForeColor = value;
                    Invalidate();
                }
            }
        }

        private Color tabUnSelectedForeColor = Color.Silver;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("未选中Tab页字体色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "Silver")]
        public Color TabUnSelectedForeColor
        {
            get => tabUnSelectedForeColor;
            set
            {
                if (tabUnSelectedForeColor != value)
                {
                    tabUnSelectedForeColor = value;
                    _menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }

        private Color tabSelectedHighColor = UIColor.Blue;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("选中Tab页高亮"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color TabSelectedHighColor

        {
            get => tabSelectedHighColor;
            set
            {
                if (tabSelectedHighColor != value)
                {
                    tabSelectedHighColor = value;
                    Invalidate();
                }
            }
        }

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

        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rect = base.DisplayRectangle;
                return new Rectangle(rect.Left - 3, rect.Top - 4, rect.Width + 7, rect.Height + 8);
            }
        }

        private void AfterSetFillColor(Color color)
        {
            foreach (TabPage page in TabPages)
            {
                page.BackColor = color;
            }
        }

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
            tabSelectedForeColor = uiColor.TabControlTabSelectedColor;
            tabSelectedHighColor = uiColor.TabControlTabSelectedColor;
            _fillColor = uiColor.TabControlBackColor;
        }

        private UIMenuStyle _menuStyle = UIMenuStyle.Black;

        [DefaultValue(UIMenuStyle.Black)]
        [Description("主题风格"), Category("SunnyUI")]
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
            TabBackColor = uiColor.BackColor;
            TabSelectedColor = uiColor.SelectedColor;
            TabUnSelectedForeColor = uiColor.UnSelectedForeColor;
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            DoubleBuffered = true;
            SizeMode = TabSizeMode.Fixed;
            Appearance = TabAppearance.Normal;
            Alignment = TabAlignment.Left;
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

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // 绘制背景色
            e.Graphics.Clear(TabBackColor);
            for (int index = 0; index <= TabCount - 1; index++)
            {
                Rectangle TabRect = new Rectangle(GetTabRect(index).Location.X - 2, GetTabRect(index).Location.Y - 2, base.ItemSize.Height + 4, base.ItemSize.Width);
                Size sf = TextRenderer.MeasureText(TabPages[index].Text, Font);
                int textLeft = 4 + 6 + 4 + (ImageList?.ImageSize.Width ?? 0);
                if (TextAlignment == HorizontalAlignment.Right)
                    textLeft = TabRect.Width - 4 - sf.Width;
                if (TextAlignment == HorizontalAlignment.Center)
                    textLeft = textLeft + (int)((TabRect.Width - textLeft - sf.Width) / 2.0f);

                if (index == SelectedIndex)
                {
                    // 绘制Tab选中背景色
                    e.Graphics.FillRectangle(TabSelectedColor, TabRect.X, TabRect.Y, TabRect.Width, TabRect.Height + 4);

                    // 绘制Tab选中高亮
                    e.Graphics.FillRectangle(TabSelectedHighColor, TabRect.X, TabRect.Y, 4, TabRect.Height + 3);
                }

                // 绘制标题
                Color textColor = index == SelectedIndex ? tabSelectedForeColor : TabUnSelectedForeColor;
                e.Graphics.DrawString(TabPages[index].Text, Font, textColor, new Rectangle(textLeft, TabRect.Top, TabRect.Width, TabRect.Height), ContentAlignment.MiddleLeft);

                // 绘制图标
                if (ImageList != null)
                {
                    int imageIndex = TabPages[index].ImageIndex;
                    if (imageIndex >= 0 && imageIndex < ImageList.Images.Count)
                    {
                        e.Graphics.DrawImage(ImageList.Images[imageIndex], TabRect.X + 4 + 6, TabRect.Y + 2 + (TabRect.Height - ImageList.ImageSize.Height) / 2.0f, ImageList.ImageSize.Width, ImageList.ImageSize.Height);
                    }
                }
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