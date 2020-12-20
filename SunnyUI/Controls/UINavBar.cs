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
 * 文件名称: UINavBar.cs
 * 文件说明: 导航栏
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-08-28: V2.2.7 增加节点的Image绘制
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("MenuItemClick")]
    [DefaultProperty("Nodes")]
    public sealed partial class UINavBar : ScrollableControl, IStyleInterface
    {
        public readonly UINavMenu Menu = new UINavMenu();

        private readonly UIContextMenuStrip NavBarMenu = new UIContextMenuStrip();

        public UINavBar()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            UpdateStyles();
            Font = UIFontColor.Font;

            NavBarMenu.VisibleChanged += NavBarMenu_VisibleChanged;
            Dock = DockStyle.Top;
            Width = 500;
            Height = 110;
            Version = UIGlobal.Version;
        }

        public void ClearAll()
        {
            Nodes.Clear();
            MenuHelper.Clear();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (NavBarMenu != null) NavBarMenu.Font = Font;
        }

        [DefaultValue(null)]
        [Description("关联的TabControl"), Category("SunnyUI")]
        public UITabControl TabControl { get; set; }

        public void SetNodeItem(TreeNode node, NavMenuItem item)
        {
            MenuHelper.Add(node, item);
        }

        public void SetNodePageIndex(TreeNode node, int pageIndex)
        {
            MenuHelper.SetPageIndex(node, pageIndex);
        }

        public int GetPageIndex(TreeNode node)
        {
            return MenuHelper.GetPageIndex(node);
        }

        public void SetNodeSymbol(TreeNode node, int symbol, int symbolSize = 24)
        {
            MenuHelper.SetSymbol(node, symbol, symbolSize);
        }

        public void SetNodeImageIndex(TreeNode node, int imageIndex)
        {
            node.ImageIndex = imageIndex;
        }

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
                foreColor = value;
                _menuStyle = UIMenuStyle.Custom;
                _style = UIStyle.Custom;
                Invalidate();
            }
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            _menuStyle = UIMenuStyle.Custom;
            _style = UIStyle.Custom;
        }

        private Color backColor = Color.FromArgb(56, 56, 56);

        [DefaultValue(typeof(Color), "56, 56, 56")]
        [Description("背景颜色"), Category("SunnyUI")]
        public override Color BackColor
        {
            get => backColor;
            set
            {
                backColor = value;
                _menuStyle = UIMenuStyle.Custom;
                _style = UIStyle.Custom;
                Invalidate();
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
                menuSelectedColor = value;
                _menuStyle = UIMenuStyle.Custom;
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
                menuHoverColor = value;
                _menuStyle = UIMenuStyle.Custom;
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
                selectedHighColor = value;
                _style = UIStyle.Custom;
                Invalidate();
            }
        }

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

        private void NavBarMenu_VisibleChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [MergableProperty(false)]
        [Description("菜单栏显示节点集合"), Category("SunnyUI")]
        public TreeNodeCollection Nodes => Menu.Nodes;

        [DefaultValue(null)]
        [Description("图片列表"), Category("SunnyUI")]
        public ImageList ImageList
        {
            get => Menu.ImageList;
            set => Menu.ImageList = value;
        }

        [DefaultValue(null)]
        [Description("下拉菜单图片列表"), Category("SunnyUI")]
        public ImageList DropMenuImageList
        {
            get => NavBarMenu.ImageList;
            set => NavBarMenu.ImageList = value;
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
                    NodeMouseClick?.Invoke(Nodes[SelectedIndex], selectedIndex, MenuHelper.GetPageIndex(Nodes[SelectedIndex]));

                    if (Nodes[value].Nodes.Count == 0)
                    {
                        MenuItemClick?.Invoke(Nodes[SelectedIndex].Text, selectedIndex, MenuHelper.GetPageIndex(Nodes[SelectedIndex]));
                        TabControl?.SelectPage(MenuHelper.GetPageIndex(Nodes[SelectedIndex]));
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
                    _style = UIStyle.Custom;
                    Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.Clear(BackColor);
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

                SizeF sf = e.Graphics.MeasureString(node.Text, Font);
                Color textColor = ForeColor;

                if (i == ActiveIndex)
                {
                    e.Graphics.FillRectangle(MenuHoverColor, rect);
                    textColor = SelectedForeColor;
                }

                if (i == SelectedIndex)
                {
                    if (MenuSelectedColorUsed)
                    {
                        e.Graphics.FillRectangle(MenuSelectedColor, rect.X, Height - NodeSize.Height, rect.Width, NodeSize.Height);
                    }

                    if (!NavBarMenu.Visible)
                    {
                        e.Graphics.FillRectangle(SelectedHighColor, rect.X, Height - 4, rect.Width, 4);
                    }

                    textColor = SelectedForeColor;
                }

                if (symbolSize > 0)
                {
                    if (symbol > 0)
                    {
                        e.Graphics.DrawFontImage(symbol, symbolSize, textColor, new RectangleF(NodeX + i * NodeSize.Width + (NodeSize.Width - sf.Width - symbolSize) / 2.0f, NodeY, symbolSize, NodeSize.Height));

                    }
                    else
                    {
                        if (ImageList != null)
                            e.Graphics.DrawImage((Bitmap)ImageList.Images[node.ImageIndex], NodeX + i * NodeSize.Width + (NodeSize.Width - sf.Width - symbolSize) / 2.0f, NodeY + (NodeSize.Height - ImageList.ImageSize.Height) / 2);
                    }

                    e.Graphics.DrawString(node.Text, Font, textColor, NodeX + i * NodeSize.Width + (NodeSize.Width - sf.Width + symbolSize) / 2.0f, NodeY + (NodeSize.Height - sf.Height) / 2);
                }
                else
                {
                    e.Graphics.DrawString(node.Text, Font, textColor, NodeX + i * NodeSize.Width + (NodeSize.Width - sf.Width) / 2.0f, NodeY + (NodeSize.Height - sf.Height) / 2);
                }

                if (node.Nodes.Count > 0)
                {
                    SizeF imageSize = e.Graphics.GetFontImageSize(61703, 24);
                    if (i != SelectedIndex)
                    {
                        e.Graphics.DrawFontImage(61703, 24, textColor, NodeX + i * NodeSize.Width + rect.Width - 24, rect.Top + (rect.Height - imageSize.Height) / 2);
                    }
                    else
                    {
                        e.Graphics.DrawFontImage(NavBarMenu.Visible ? 61702 : 61703, 24, textColor, NodeX + i * NodeSize.Width + rect.Width - 24, rect.Top + (rect.Height - imageSize.Height) / 2);
                    }
                }
            }
        }

        private int _nodeInterval = 100;

        [DefaultValue(100)]
        [Description("显示菜单边距"), Category("SunnyUI")]
        public int NodeInterval
        {
            get => _nodeInterval;
            set
            {
                if (_nodeInterval != value)
                {
                    _nodeInterval = value;
                    Invalidate();
                }
            }
        }

        private Size nodeSize = new Size(130, 45);

        [DefaultValue(typeof(Size), "130, 45")]
        [Description("显示菜单大小"), Category("SunnyUI")]
        public Size NodeSize
        {
            get => nodeSize;
            set
            {
                if (nodeSize != value)
                {
                    nodeSize = value;
                    Invalidate();
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            ActiveIndex = -1;
            Invalidate();
        }

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

            if (Nodes[selectedIndex].Nodes.Count == 0)
            {
                return;
            }

            NavBarMenu.Style = UIStyles.Style;
            NavBarMenu.Items.Clear();
            foreach (TreeNode node in Nodes[SelectedIndex].Nodes)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(node.Text) { Tag = node };
                item.Click += Item_Click;
                NavBarMenu.Items.Add(item);

                if (node.Nodes.Count > 0)
                {
                    AddMenu(item, node);
                }
            }

            NavBarMenu.AutoSize = true;
            if (NavBarMenu.Width < NodeSize.Width)
            {
                NavBarMenu.AutoSize = false;
                NavBarMenu.Width = NodeSize.Width;
            }

            foreach (ToolStripItem item in NavBarMenu.Items)
            {
                item.AutoSize = false;
                item.Width = NavBarMenu.Width - 1;
                item.Height = 30;
            }

            NavBarMenu.CalcHeight();
            NavBarMenu.Show(this, NodeMenuLeft(SelectedIndex), Height);
        }

        private void Item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.Tag != null && item.Tag is TreeNode node)
            {
                TabControl?.SelectPage(MenuHelper.GetPageIndex(node));
                MenuItemClick?.Invoke(item.Text, selectedIndex, MenuHelper.GetPageIndex(node));
                NodeMouseClick?.Invoke(node, selectedIndex, MenuHelper.GetPageIndex(node));
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
                childItem.Click += Item_Click;
                item.DropDownItems.Add(childItem);

                if (childNode.Nodes.Count > 0)
                {
                    AddMenu(childItem, childNode);
                }
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

            selectedForeColor = selectedHighColor = uiColor.MenuSelectedColor;
            Invalidate();
        }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        public string Version { get; }

        public TreeNode CreateNode(string text, int pageIndex)
        {
            return CreateNode(new NavMenuItem(text, pageIndex));
        }

        public TreeNode CreateNode(UIPage page)
        {
            return CreateNode(new NavMenuItem(page));
        }

        public TreeNode CreateNode(NavMenuItem item)
        {
            TreeNode node = new TreeNode(item.Text);
            Nodes.Add(node);
            SetNodeItem(node, item);
            return node;
        }

        public TreeNode CreateNode(string text, int imageIndex, int pageIndex)
        {
            return CreateNode(new NavMenuItem(text, pageIndex), imageIndex);
        }

        public TreeNode CreateNode(UIPage page, int imageIndex)
        {
            return CreateNode(new NavMenuItem(page), imageIndex);
        }

        public TreeNode CreateNode(NavMenuItem item, int imageIndex)
        {
            TreeNode node = new TreeNode(item.Text);
            Nodes.Add(node);
            SetNodeItem(node, item);
            node.ImageIndex = imageIndex;
            return node;
        }

        public TreeNode CreateNode(string text, int symbol, int symbolSize, int pageIndex)
        {
            return CreateNode(new NavMenuItem(text, pageIndex), symbol, symbolSize);
        }

        public TreeNode CreateNode(UIPage page, int symbol, int symbolSize)
        {
            return CreateNode(new NavMenuItem(page), symbol, symbolSize);
        }

        public TreeNode CreateNode(NavMenuItem item, int symbol, int symbolSize)
        {
            TreeNode node = new TreeNode(item.Text);
            Nodes.Add(node);
            SetNodeItem(node, item);
            MenuHelper.SetSymbol(node, symbol, symbolSize);
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

        public TreeNode CreateChildNode(TreeNode parent, NavMenuItem item)
        {
            TreeNode childNode = new TreeNode(item.Text);
            parent.Nodes.Add(childNode);
            SetNodeItem(childNode, item);
            return childNode;
        }

        public TreeNode CreateChildNode(TreeNode parent, int imageIndex, string text, int pageIndex)
        {
            return CreateChildNode(parent, imageIndex, new NavMenuItem(text, pageIndex));
        }

        public TreeNode CreateChildNode(TreeNode parent, int imageIndex, UIPage page)
        {
            return CreateChildNode(parent, imageIndex, new NavMenuItem(page));
        }

        public TreeNode CreateChildNode(TreeNode parent, int imageIndex, NavMenuItem item)
        {
            TreeNode childNode = new TreeNode(item.Text);
            parent.Nodes.Add(childNode);
            SetNodeItem(childNode, item);
            childNode.ImageIndex = imageIndex;
            return childNode;
        }

        public TreeNode CreateChildNode(TreeNode parent, int symbol, int symbolSize, string text, int pageIndex)
        {
            return CreateChildNode(parent, symbol, symbolSize, new NavMenuItem(text, pageIndex));
        }

        public TreeNode CreateChildNode(TreeNode parent, int symbol, int symbolSize, UIPage page)
        {
            return CreateChildNode(parent, symbol, symbolSize, new NavMenuItem(page));
        }

        public TreeNode CreateChildNode(TreeNode parent, int symbol, int symbolSize, NavMenuItem item)
        {
            TreeNode childNode = new TreeNode(item.Text);
            parent.Nodes.Add(childNode);
            SetNodeItem(childNode, item);
            MenuHelper.SetSymbol(childNode, symbol, symbolSize);
            return childNode;
        }
    }
}