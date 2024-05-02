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
 * 文件名称: UINavBar.cs
 * 文件说明: 导航栏
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-08-28: V2.2.7 增加节点的Image绘制
 * 2021-03-20: V3.0.2 增加可设置背景图片
 * 2021-06-08: V3.0.4 标题选中高亮颜色增加可调整高度
 * 2021-08-07: V3.0.5 显示/隐藏子节点提示箭头，增加选中项圆角
 * 2022-03-19: V3.1.1 重构主题配色
 * 2022-04-14: V3.1.3 重构扩展函数
 * 2022-07-28: V3.2.2 删除界面此控件的编辑器 
 * 2023-02-22: V3.3.2 去除下拉菜单宽度调整
 * 2023-05-12: V3.3.6 重构DrawString函数
 * 2023-05-16: V3.3.6 重构DrawFontImage函数
 * 2023-10-17: V3.5.1 修正下拉菜单文字显示垂直居中
 * 2023-10-17: V3.5.1 ImageList为空时，下拉菜单增加Symbol绘制
 * 2023-11-16: V3.5.2 重构主题
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("MenuItemClick")]
    [DefaultProperty("Nodes")]
    public sealed partial class UINavBar : ScrollableControl, IStyleInterface, IZoomScale
    {
        public readonly TreeView Menu = new TreeView();

        private readonly UIContextMenuStrip NavBarMenu = new UIContextMenuStrip();

        public UINavBar()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            DoubleBuffered = true;
            UpdateStyles();
            base.Font = UIStyles.Font();

            NavBarMenu.VisibleChanged += NavBarMenu_VisibleChanged;
            Dock = DockStyle.Top;
            Width = 500;
            Height = 110;
            Version = UIGlobal.Version;

            selectedForeColor = UIStyles.Blue.NavBarMenuSelectedColor;
            selectedHighColor = UIStyles.Blue.NavBarMenuSelectedColor;
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
            _nodeInterval = UIZoomScale.Calc(baseNodeInterval, scale);
            nodeSize = UIZoomScale.Calc(baseNodeSize, scale);
            Invalidate();
        }

        private float DefaultFontSize = -1;

        public void SetDPIScale()
        {
            if (!UIDPIScale.NeedSetDPIFont()) return;
            if (DefaultFontSize < 0) DefaultFontSize = this.Font.Size;
            this.SetDPIScaleFont(DefaultFontSize);
        }

        private int radius;

        [DefaultValue(0)]
        [Description("显示选择项圆角"), Category("SunnyUI")]
        public int Radius
        {
            get => radius;
            set
            {
                radius = Math.Max(0, value);
                Invalidate();
            }
        }

        private bool showItemsArrow = true;

        [DefaultValue(true)]
        [Description("显示子节点提示箭头"), Category("SunnyUI")]
        public bool ShowItemsArrow
        {
            get => showItemsArrow;
            set
            {
                showItemsArrow = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        public void ClearAll()
        {
            Nodes.Clear();
            MenuHelper.Clear();
        }

        [DefaultValue(null)]
        [Description("关联的TabControl"), Category("SunnyUI")]
        public UITabControl TabControl { get; set; }

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

        private UIMenuStyle _menuStyle = UIMenuStyle.Black;

        [DefaultValue(UIMenuStyle.Black)]
        [Description("导航栏主题风格"), Category("SunnyUI")]
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

        private Color foreColor = Color.Silver;

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "Silver")]
        public override Color ForeColor
        {
            get => foreColor;
            set
            {
                if (foreColor != value)
                {
                    foreColor = value;
                    _menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }

        private Color backColor = Color.FromArgb(56, 56, 56);

        [DefaultValue(typeof(Color), "56, 56, 56")]
        [Description("背景颜色"), Category("SunnyUI")]
        public override Color BackColor
        {
            get => backColor;
            set
            {
                if (backColor != value)
                {
                    backColor = value;
                    _menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }

        private void SetMenuStyle(UIMenuColor uiColor)
        {
            foreColor = uiColor.UnSelectedForeColor;
            backColor = uiColor.BackColor;
            menuHoverColor = uiColor.HoverColor;
            menuSelectedColor = uiColor.SelectedColor;
            Invalidate();
        }

        private Color menuSelectedColor = Color.FromArgb(36, 36, 36);

        [DefaultValue(typeof(Color), "36, 36, 36")]
        [Description("菜单栏选中背景颜色"), Category("SunnyUI")]
        public Color MenuSelectedColor
        {
            get => menuSelectedColor;
            set
            {
                if (menuSelectedColor != value)
                {
                    menuSelectedColor = value;
                    _menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }

        [Description("选中使用菜单栏选中背景颜色MenuSelectedColor，不选用则使用背景色BackColor"), Category("SunnyUI"), DefaultValue(false)]
        public bool MenuSelectedColorUsed { get; set; }

        private Color menuHoverColor = Color.FromArgb(76, 76, 76);

        [DefaultValue(typeof(Color), "76, 76, 76")]
        [Description("菜单栏鼠标移上颜色"), Category("SunnyUI")]
        public Color MenuHoverColor
        {
            get => menuHoverColor;
            set
            {
                if (menuHoverColor != value)
                {
                    menuHoverColor = value;
                    _menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }

        private Color selectedHighColor = UIColor.Blue;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("选中Tab页高亮"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color SelectedHighColor

        {
            get => selectedHighColor;
            set
            {
                if (selectedHighColor != value)
                {
                    selectedHighColor = value;
                    Invalidate();
                }
            }
        }

        private int selectedHighColorSize = 4;

        /// <summary>
        /// 选中页高亮高度
        /// </summary>
        [Description("选中Tab页高亮高度"), Category("SunnyUI")]
        [DefaultValue(4)]
        public int SelectedHighColorSize

        {
            get => selectedHighColorSize;
            set
            {
                value = Math.Max(value, 0);
                value = Math.Min(value, 8);
                selectedHighColorSize = value;
                Invalidate();
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

        private void NavBarMenu_VisibleChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [MergableProperty(false)]
        [Description("菜单栏显示节点集合"), Category("SunnyUI")]
        //[Editor("System.Windows.Forms.Design.TreeNodeCollectionEditor", typeof(UITypeEditor))]
        public TreeNodeCollection Nodes => Menu.Nodes;

        [DefaultValue(null)]
        [Description("图片列表"), Category("SunnyUI")]
        public ImageList ImageList
        {
            get => Menu.ImageList;
            set => Menu.ImageList = value;
        }

        [DefaultValue(null)]
        [Browsable(false)]
        [Description("下拉菜单图片列表"), Category("SunnyUI")]
        public ImageList DropMenuImageList
        {
            get => NavBarMenu.ImageList;
            set => NavBarMenu.ImageList = value;
        }

        private Font dropMenuFont = UIStyles.Font();

        /// <summary>
        /// 标题字体
        /// </summary>
        [Description("标题字体"), Category("SunnyUI")]
        [DefaultValue(typeof(Font), "宋体, 12pt")]
        public Font DropMenuFont
        {
            get => dropMenuFont;
            set
            {
                dropMenuFont = value;
                Invalidate();
            }
        }

        private StringAlignment nodeAlignment = StringAlignment.Far;

        [DefaultValue(StringAlignment.Far)]
        [Description("显示节点位置"), Category("SunnyUI")]
        public StringAlignment NodeAlignment
        {
            get => nodeAlignment;
            set
            {
                nodeAlignment = value;
                Invalidate();
            }
        }

        private int NodeX;
        private int NodeY;
        private int ActiveIndex = -1;

        private int selectedIndex = -1;

        private readonly NavMenuHelper MenuHelper = new NavMenuHelper();

        [DefaultValue(-1)]
        [Description("选中菜单索引"), Category("SunnyUI")]
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                if (Nodes.Count > 0 && value >= 0 && value < Nodes.Count)
                {
                    selectedIndex = value;
                    NodeMouseClick?.Invoke(Nodes[SelectedIndex], selectedIndex, GetPageIndex(Nodes[SelectedIndex]));

                    if (Nodes[value].Nodes.Count == 0)
                    {
                        MenuItemClick?.Invoke(Nodes[SelectedIndex].Text, selectedIndex, GetPageIndex(Nodes[SelectedIndex]));
                        TabControl?.SelectPage(GetPageIndex(Nodes[SelectedIndex]));
                    }

                    Invalidate();
                }
            }
        }

        private Color selectedForeColor = UIColor.Blue;

        [DefaultValue(typeof(Color), "80, 160, 255")]
        [Description("选中菜单字体颜色"), Category("SunnyUI")]
        public Color SelectedForeColor
        {
            get => selectedForeColor;
            set
            {
                if (selectedForeColor != value)
                {
                    selectedForeColor = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (BackgroundImage == null)
            {
                e.Graphics.Clear(BackColor);
            }

            NodeX = 0;
            NodeY = Height - NodeSize.Height;

            switch (NodeAlignment)
            {
                case StringAlignment.Near:
                    NodeX = NodeInterval;
                    break;

                case StringAlignment.Center:
                    NodeX = (Width - Nodes.Count * NodeSize.Width) / 2;
                    break;

                case StringAlignment.Far:
                    NodeX = Width - Nodes.Count * NodeSize.Width - NodeInterval;
                    break;
            }

            for (int i = 0; i < Nodes.Count; i++)
            {
                Rectangle rect = new Rectangle(NodeX + i * NodeSize.Width, NodeY, NodeSize.Width, NodeSize.Height);

                TreeNode node = Nodes[i];

                int symbolSize = 0;
                if (ImageList != null && ImageList.Images.Count > 0 && node.ImageIndex >= 0 && node.ImageIndex >= 0 && node.ImageIndex < ImageList.Images.Count)
                    symbolSize = ImageList.ImageSize.Width;

                int symbol = MenuHelper.GetSymbol(node);
                if (symbol > 0)
                    symbolSize = MenuHelper.GetSymbolSize(node);

                Point symbolOffset = MenuHelper.GetSymbolOffset(node);

                SizeF sf = TextRenderer.MeasureText(node.Text, Font);
                Color textColor = ForeColor;

                if (i == ActiveIndex)
                {
                    if (radius == 0)
                    {
                        e.Graphics.FillRectangle(MenuHoverColor, rect);
                    }
                    else
                    {
                        using var path = rect.CreateRoundedRectanglePath(Radius, UICornerRadiusSides.LeftTop | UICornerRadiusSides.RightTop);
                        e.Graphics.FillPath(MenuHoverColor, path);
                    }

                    textColor = SelectedForeColor;
                }

                if (i == SelectedIndex)
                {
                    if (MenuSelectedColorUsed)
                    {
                        if (radius == 0)
                        {
                            e.Graphics.FillRectangle(MenuSelectedColor, rect.X, Height - NodeSize.Height, rect.Width, NodeSize.Height);
                        }
                        else
                        {
                            using var path = new Rectangle(rect.X, Height - NodeSize.Height, rect.Width, NodeSize.Height).CreateRoundedRectanglePath(Radius, UICornerRadiusSides.LeftTop | UICornerRadiusSides.RightTop);
                            e.Graphics.FillPath(MenuSelectedColor, path);
                        }
                    }

                    if (!NavBarMenu.Visible && SelectedHighColorSize > 0)
                    {
                        e.Graphics.FillRectangle(SelectedHighColor, rect.X, Height - SelectedHighColorSize, rect.Width, SelectedHighColorSize);
                    }

                    textColor = SelectedForeColor;
                }

                if (symbolSize > 0)
                {
                    if (symbol > 0)
                    {
                        e.Graphics.DrawFontImage(symbol, symbolSize, textColor, new Rectangle(NodeX + i * NodeSize.Width + (int)(NodeSize.Width - sf.Width - symbolSize) / 2, NodeY, symbolSize, NodeSize.Height), symbolOffset.X, symbolOffset.Y, MenuHelper.GetSymbolRotate(node));
                    }
                    else
                    {
                        if (ImageList != null)
                            e.Graphics.DrawImage((Bitmap)ImageList.Images[node.ImageIndex], NodeX + i * NodeSize.Width + (NodeSize.Width - sf.Width - symbolSize) / 2.0f, NodeY + (NodeSize.Height - ImageList.ImageSize.Height) / 2);
                    }

                    e.Graphics.DrawString(node.Text, Font, textColor, new Rectangle(NodeX + i * NodeSize.Width + symbolSize / 2, NodeY, NodeSize.Width, NodeSize.Height), ContentAlignment.MiddleCenter);
                }
                else
                {
                    e.Graphics.DrawString(node.Text, Font, textColor, new Rectangle(NodeX + i * NodeSize.Width, NodeY, NodeSize.Width, NodeSize.Height), ContentAlignment.MiddleCenter);
                }

                if (ShowItemsArrow && node.Nodes.Count > 0)
                {
                    if (i != SelectedIndex)
                    {
                        e.Graphics.DrawFontImage(61703, 24, textColor, new Rectangle(NodeX + i * NodeSize.Width + rect.Width - 24 - 3, rect.Top, 24, rect.Height));
                    }
                    else
                    {
                        e.Graphics.DrawFontImage(NavBarMenu.Visible ? 61702 : 61703, 24, textColor, new Rectangle(NodeX + i * NodeSize.Width + rect.Width - 24 - 3, rect.Top, 24, rect.Height));
                    }
                }
            }
        }

        private int _nodeInterval = 100;
        private int baseNodeInterval = 100;

        [DefaultValue(100)]
        [Description("显示菜单边距"), Category("SunnyUI")]
        public int NodeInterval
        {
            get => _nodeInterval;
            set
            {
                if (_nodeInterval != value)
                {
                    baseNodeInterval = _nodeInterval = value;
                    Invalidate();
                }
            }
        }

        private Size nodeSize = new Size(130, 45);
        private Size baseNodeSize = new Size(130, 45);

        [DefaultValue(typeof(Size), "130, 45")]
        [Description("显示菜单大小"), Category("SunnyUI")]
        public Size NodeSize
        {
            get => nodeSize;
            set
            {
                if (nodeSize != value)
                {
                    baseNodeSize = nodeSize = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 重载鼠标离开事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            ActiveIndex = -1;
            Invalidate();
        }

        /// <summary>
        /// 重载鼠标移动事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.X < NodeX || e.X > NodeX + Nodes.Count * NodeSize.Width || e.Y < NodeY)
            {
                if (ActiveIndex != -1)
                {
                    ActiveIndex = -1;
                    Invalidate();
                }

                return;
            }

            int index = (e.X - NodeX) / NodeSize.Width;
            if (ActiveIndex != index)
            {
                ActiveIndex = index;
                Invalidate();
            }
        }

        private int NodeMenuLeft(int index)
        {
            return NodeX + index * NodeSize.Width;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (ActiveIndex == -1) return;
            SelectedIndex = ActiveIndex;
            Invalidate();
            if (SelectedIndex < 0) return;
            if (SelectedIndex >= Nodes.Count) return;
            if (Nodes[selectedIndex].Nodes.Count == 0)
            {
                return;
            }

            NavBarMenu.Style = UIStyles.Style;
            NavBarMenu.Items.Clear();
            NavBarMenu.ImageList = ImageList;
            NavBarMenu.Font = DropMenuFont;
            foreach (TreeNode node in Nodes[SelectedIndex].Nodes)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(node.Text) { Tag = node };
                item.Click += Item_Click;
                if (ImageList != null)
                {
                    item.ImageIndex = node.ImageIndex;
                }
                else
                {
                    int symbol = MenuHelper.GetSymbol(node);
                    if (symbol > 0) item.ImageIndex = symbol;
                }

                NavBarMenu.Items.Add(item);

                if (node.Nodes.Count > 0)
                {
                    AddMenu(item, node);
                }
            }

            //NavBarMenu.AutoSize = true;
            //if (NavBarMenu.Width < NodeSize.Width)
            //{
            //    NavBarMenu.AutoSize = false;
            //    NavBarMenu.Width = NodeSize.Width;
            //}

            foreach (ToolStripMenuItem item in NavBarMenu.Items)
            {
                item.AutoSize = false;
                item.Width = NavBarMenu.Width + 3;

                if (!DropDownItemAutoHeight)
                {
                    item.Height = DropDownItemHeight;
                }
            }

            NavBarMenu.CalcHeight();
            NavBarMenu.Show(this, NodeMenuLeft(SelectedIndex), Height);
        }

        [DefaultValue(30)]
        [Description("下拉菜单节点高度"), Category("SunnyUI")]
        public int DropDownItemHeight { get; set; } = 30;

        [DefaultValue(false)]
        [Description("下拉菜单节点自动高度"), Category("SunnyUI")]
        public bool DropDownItemAutoHeight { get; set; } = false;

        private void Item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.Tag != null && item.Tag is TreeNode node)
            {
                TabControl?.SelectPage(GetPageIndex(node));
                MenuItemClick?.Invoke(item.Text, selectedIndex, GetPageIndex(node));
                NodeMouseClick?.Invoke(node, selectedIndex, GetPageIndex(node));
            }
        }

        public delegate void OnMenuItemClick(string itemText, int menuIndex, int pageIndex);

        public event OnMenuItemClick MenuItemClick;

        public delegate void OnNodeMouseClick(TreeNode node, int menuIndex, int pageIndex);

        public event OnNodeMouseClick NodeMouseClick;

        private void AddMenu(ToolStripMenuItem item, TreeNode node)
        {
            foreach (TreeNode childNode in node.Nodes)
            {
                ToolStripMenuItem childItem = new ToolStripMenuItem(childNode.Text) { Tag = childNode };
                if (ImageList != null)
                {
                    childItem.ImageIndex = childNode.ImageIndex;
                }
                else
                {
                    int symbol = MenuHelper.GetSymbol(childNode);
                    if (symbol > 0) childItem.ImageIndex = symbol;
                }

                childItem.Click += Item_Click;
                item.DropDownItems.Add(childItem);

                if (childNode.Nodes.Count > 0)
                {
                    AddMenu(childItem, childNode);
                }
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
            selectedForeColor = uiColor.NavBarMenuSelectedColor;
            selectedHighColor = uiColor.NavBarMenuSelectedColor;
        }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false), Browsable(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        public string Version { get; }

        #region 扩展函数

        public UINavBar SetNodePageIndex(TreeNode node, int pageIndex)
        {
            MenuHelper.SetPageIndex(node, pageIndex);
            return this;
        }

        public UINavBar SetNodeSymbol(TreeNode node, int symbol, int symbolSize = 24, int symbolRotate = 0)
        {
            MenuHelper.SetSymbol(node, symbol, symbolSize, symbolRotate);
            return this;
        }

        public UINavBar SetNodeImageIndex(TreeNode node, int imageIndex)
        {
            node.ImageIndex = imageIndex;
            return this;
        }

        public TreeNode CreateNode(string text, int pageIndex)
        {
            return CreateNode(new NavMenuItem(text, pageIndex));
        }

        public TreeNode CreateNode(string text, Guid pageGuid)
        {
            return CreateNode(new NavMenuItem(text, pageGuid));
        }

        public TreeNode CreateNode(UIPage page)
        {
            return CreateNode(new NavMenuItem(page));
        }

        public TreeNode CreateNode(string text, int symbol, int symbolSize, int pageIndex, int symbolRotate = 0)
        {
            var node = CreateNode(text, pageIndex);
            SetNodeSymbol(node, symbol, symbolSize, symbolRotate);
            return node;
        }

        public TreeNode CreateChildNode(TreeNode parent, string text, Guid pageGuid)
        {
            return CreateChildNode(parent, new NavMenuItem(text, pageGuid));
        }

        public int GetPageIndex(TreeNode node)
        {
            return MenuHelper.GetPageIndex(node);
        }

        private void SetNodeItem(TreeNode node, NavMenuItem item)
        {
            MenuHelper.Add(node, item);
        }

        private TreeNode CreateNode(NavMenuItem item)
        {
            TreeNode node = new TreeNode(item.Text);
            Nodes.Add(node);
            SetNodeItem(node, item);
            return node;
        }

        public TreeNode CreateChildNode(TreeNode parent, string text, int pageIndex)
        {
            return CreateChildNode(parent, new NavMenuItem(text, pageIndex));
        }

        public TreeNode CreateChildNode(TreeNode parent, UIPage page)
        {
            return CreateChildNode(parent, new NavMenuItem(page));
        }

        public TreeNode CreateChildNode(TreeNode parent, string text, int symbol, int symbolSize, int pageIndex, int symbolRotate = 0)
        {
            var node = CreateChildNode(parent, text, pageIndex);
            SetNodeSymbol(node, symbol, symbolSize, symbolRotate);
            return node;
        }

        private TreeNode CreateChildNode(TreeNode parent, NavMenuItem item)
        {
            TreeNode childNode = new TreeNode(item.Text);
            parent.Nodes.Add(childNode);
            SetNodeItem(childNode, item);
            return childNode;
        }

        #endregion 扩展函数
    }
}