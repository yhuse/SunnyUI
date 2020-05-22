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
 * 文件名称: UINavMenu.cs
 * 文件说明: 导航菜单
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("MenuItemClick")]
    [DefaultProperty("Nodes")]
    public sealed class UINavMenu : TreeView, IStyleInterface
    {
        public delegate void OnMenuItemClick(TreeNode node, NavMenuItem item, int pageIndex);

        public event OnMenuItemClick MenuItemClick;

        private readonly UIScrollBar Bar = new UIScrollBar();

        public UINavMenu()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            BorderStyle = BorderStyle.None;
            //HideSelection = false;
            DrawMode = TreeViewDrawMode.OwnerDrawAll;
            //FullRowSelect = true;
            //ShowLines = false;
            //ShowPlusMinus = false;
            //ShowRootLines = false;

            DoubleBuffered = true;
            Font = UIFontColor.Font;
            //CheckBoxes = false;
            ItemHeight = 50;
            BackColor = Color.FromArgb(56, 56, 56);

            Bar.ValueChanged += Bar_ValueChanged;
            Bar.Dock = DockStyle.Right;
            Bar.Visible = false;
            Bar.Style = UIStyle.Custom;
            Bar.StyleCustomMode = true;
            Bar.FillColor = fillColor;

            Bar.ForeColor = Color.Silver;
            Bar.HoverColor = Color.Silver;
            Bar.PressColor = Color.Silver;

            Controls.Add(Bar);
            Version = UIGlobal.Version;
            SetScrollInfo();
        }

        [DefaultValue(null)]
        public string TagString { get; set; }

        private Color fillColor = Color.FromArgb(56, 56, 56);

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("背景颜色"), Category("自定义")]
        [DefaultValue(typeof(Color), "56, 56, 56")]
        public Color FillColor
        {
            get => fillColor;
            set
            {
                if (fillColor != value)
                {
                    fillColor = value;
                    menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }

        private Color foreColor = Color.Silver;

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("背景颜色"), Category("自定义")]
        [DefaultValue(typeof(Color), "Silver")]
        public override Color ForeColor
        {
            get => foreColor;
            set
            {
                if (foreColor != value)
                {
                    foreColor = value;
                    menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetScrollInfo();
        }

        private void Bar_ValueChanged(object sender, EventArgs e)
        {
            ScrollBarInfo.SetScrollValue(Handle, Bar.Value);
        }

        [DefaultValue(false)]
        public bool StyleCustomMode { get; set; }

        [DefaultValue(null)]
        public TabControl TabControl { get; set; }

        private Color selectedColor = Color.FromArgb(36, 36, 36);

        private bool showTips;

        [Description("是否显示角标"), Category("自定义")]
        [DefaultValue(false)]
        public bool ShowTips
        {
            get => showTips;
            set
            {
                if (showTips != value)
                {
                    showTips = value;
                    Invalidate();
                }
            }
        }

        private Font tipsFont = new Font("Microsoft Sans Serif", 9);

        [Description("角标文字字体"), Category("自定义")]
        [DefaultValue(typeof(Font), "Microsoft Sans Serif, 9pt")]
        public Font TipsFont
        {
            get => tipsFont;
            set
            {
                if (!tipsFont.Equals(value))
                {
                    tipsFont = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "36, 36, 36")]
        public Color SelectedColor
        {
            get => selectedColor;
            set
            {
                if (selectedColor != value)
                {
                    selectedColor = value;
                    menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }

        private Color selectedHighColor = UIColor.Blue;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("选中Tab页高亮"), Category("自定义")]
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

        private Color hoverColor = Color.FromArgb(76, 76, 76);

        [DefaultValue(typeof(Color), "76, 76, 76")]
        public Color HoverColor
        {
            get => hoverColor;
            set
            {
                hoverColor = value;
                menuStyle = UIMenuStyle.Custom;
            }
        }

        private UIStyle _style = UIStyle.Blue;

        [DefaultValue(UIStyle.Blue)]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
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

        private UIMenuStyle menuStyle = UIMenuStyle.Black;

        [DefaultValue(UIMenuStyle.Black)]
        public UIMenuStyle MenuStyle
        {
            get => menuStyle;
            set
            {
                if (value != UIMenuStyle.Custom)
                {
                    SetMenuStyle(UIStyles.MenuColors[value]);
                }

                menuStyle = value;
            }
        }

        private void SetMenuStyle(UIMenuColor uiColor)
        {
            BackColor = uiColor.BackColor;
            fillColor = uiColor.BackColor;
            selectedColor = uiColor.SelectedColor;
            foreColor = uiColor.UnSelectedForeColor;
            hoverColor = uiColor.HoverColor;

            if (Bar != null)
            {
                Bar.FillColor = uiColor.BackColor;
                Bar.ForeColor = uiColor.UnSelectedForeColor;
                Bar.HoverColor = uiColor.UnSelectedForeColor;
                Bar.PressColor = uiColor.UnSelectedForeColor;
            }

            Invalidate();
        }

        private Color selectedForeColor = UIColor.Blue;

        [DefaultValue(typeof(Color), "80, 160, 255")]
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

        private bool ScrollBarVisible;

        private TreeNode CurrentNode;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            TreeNode node = GetNodeAt(e.Location);
            if (node == null || CurrentNode == node)
            {
                return;
            }

            Graphics g = CreateGraphics();
            if (CurrentNode != null)
            {
                OnDrawNode(new DrawTreeNodeEventArgs(g, CurrentNode, new Rectangle(0, CurrentNode.Bounds.Y, Width, CurrentNode.Bounds.Height), TreeNodeStates.Default));
            }

            CurrentNode = node;
            OnDrawNode(new DrawTreeNodeEventArgs(g, CurrentNode, new Rectangle(0, CurrentNode.Bounds.Y, Width, CurrentNode.Bounds.Height), TreeNodeStates.Hot));
            g.Dispose();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Graphics g = CreateGraphics();
            if (CurrentNode != null)
            {
                OnDrawNode(new DrawTreeNodeEventArgs(g, CurrentNode, new Rectangle(0, CurrentNode.Bounds.Y, Width, CurrentNode.Bounds.Height), TreeNodeStates.Default));
                CurrentNode = null;
            }

            g.Dispose();
        }

        private bool checkBoxes;

        [Browsable(false)]
        public new bool CheckBoxes
        {
            get => checkBoxes;
            set => checkBoxes = false;
        }

        private readonly NavMenuHelper MenuHelper = new NavMenuHelper();

        public void SetNodeItem(TreeNode node, NavMenuItem item)
        {
            MenuHelper.Add(node, item);
        }

        public void SetNodePageIndex(TreeNode node, int pageIndex)
        {
            MenuHelper.SetPageIndex(node, pageIndex);
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            if (BorderStyle != BorderStyle.None)
            {
                BorderStyle = BorderStyle.None;
            }

            SetScrollInfo();
            CheckBoxes = false;

            if (e.Node == null || (e.Node.Bounds.Width <= 0 && e.Node.Bounds.Height <= 0 && e.Node.Bounds.X <= 0 && e.Node.Bounds.Y <= 0))
            {
                e.DrawDefault = true;
            }
            else
            {
                int drawLeft = e.Node.Level * 16 + 16 + 4;
                int imageLeft = drawLeft;
                bool haveImage = false;

                if (MenuHelper.GetSymbol(e.Node) > 0)
                {
                    haveImage = true;
                    drawLeft += MenuHelper.GetSymbolSize(e.Node) + 6;
                }
                else
                {
                    if (ImageList != null && ImageList.Images.Count > 0 && e.Node.ImageIndex >= 0 && e.Node.ImageIndex < ImageList.Images.Count)
                    {
                        haveImage = true;
                        drawLeft += ImageList.ImageSize.Width + 6;
                    }
                }

                SizeF sf = e.Graphics.MeasureString(e.Node.Text, Font);
                if (e.Node == SelectedNode)
                {
                    e.Graphics.FillRectangle((e.State & TreeNodeStates.Hot) != 0 ? HoverColor : SelectedColor,
                        new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(Width, e.Node.Bounds.Height)));

                    e.Graphics.DrawString(e.Node.Text, Font, SelectedForeColor, drawLeft, e.Bounds.Y + (ItemHeight - sf.Height) / 2.0f);
                    e.Graphics.FillRectangle(SelectedHighColor, new Rectangle(0, e.Bounds.Y, 4, e.Bounds.Height));
                }
                else if (e.Node == CurrentNode && (e.State & TreeNodeStates.Hot) != 0)
                {
                    e.Graphics.FillRectangle(HoverColor, new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(Width, e.Node.Bounds.Height)));
                    e.Graphics.DrawString(e.Node.Text, Font, ForeColor, drawLeft, e.Bounds.Y + (ItemHeight - sf.Height) / 2.0f);
                }
                else
                {
                    e.Graphics.FillRectangle(fillColor, new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(Width, e.Node.Bounds.Height)));
                    e.Graphics.DrawString(e.Node.Text, Font, ForeColor, drawLeft, e.Bounds.Y + (ItemHeight - sf.Height) / 2.0f);
                }

                if (haveImage)
                {
                    if (MenuHelper.GetSymbol(e.Node) > 0)
                    {
                        SizeF fiSize = e.Graphics.GetFontImageSize(MenuHelper.GetSymbol(e.Node), MenuHelper.GetSymbolSize(e.Node));
                        e.Graphics.DrawFontImage(MenuHelper.GetSymbol(e.Node), MenuHelper.GetSymbolSize(e.Node), Color.White, imageLeft + (MenuHelper.GetSymbolSize(e.Node) - fiSize.Width) / 2.0f, e.Bounds.Y + (e.Bounds.Height - fiSize.Height) / 2);
                    }
                    else
                    {
                        if (e.Selected() && e.Node.SelectedImageIndex >= 0 && e.Node.SelectedImageIndex < ImageList.Images.Count)
                            e.Graphics.DrawImage(ImageList.Images[e.Node.SelectedImageIndex], imageLeft, e.Bounds.Y + (e.Bounds.Height - ImageList.ImageSize.Height) / 2);
                        else
                            e.Graphics.DrawImage(ImageList.Images[e.Node.ImageIndex], imageLeft, e.Bounds.Y + (e.Bounds.Height - ImageList.ImageSize.Height) / 2);
                    }
                }

                if (e.Node.Nodes.Count > 0)
                {
                    e.Graphics.DrawFontImage(e.Node.IsExpanded ? 61702 : 61703, 24, ForeColor, Width - (Bar.Visible ? 50 : 30), e.Bounds.Y + (ItemHeight - 24) / 2);
                }

                if (ShowTips && MenuHelper.GetTipsText(e.Node).IsValid())
                {
                    SizeF tipsSize = e.Graphics.MeasureString(MenuHelper.GetTipsText(e.Node), TipsFont);
                    float sfMax = Math.Max(tipsSize.Width, tipsSize.Height) + 1;
                    float tipsLeft = Width - (ScrollBarVisible ? 50 : 30) - 6 - sfMax;
                    float tipsTop = e.Bounds.Y + (ItemHeight - sfMax) / 2;

                    e.Graphics.FillEllipse(Brushes.Red, tipsLeft, tipsTop, sfMax, sfMax);
                    e.Graphics.DrawString(MenuHelper.GetTipsText(e.Node), TipsFont, Brushes.White, tipsLeft + sfMax / 2.0f - tipsSize.Width / 2.0f, tipsTop + sfMax / 2.0f - tipsSize.Height / 2.0f);
                }
            }
        }

        [Description("展开节点后选中第一个子节点"), DefaultValue(true)]
        public bool ExpandSelectFirst { get; set; } = true;

        public string Version { get; }

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);

            if (e.Node != null && e.Node.Nodes.Count > 0)
            {
                if (e.Node.IsExpanded)
                {
                    e.Node.Collapse();
                }
                else
                {
                    e.Node.Expand();
                }

                if (SelectedNode != null && SelectedNode == e.Node && e.Node.IsExpanded && ExpandSelectFirst && e.Node.Nodes.Count > 0)
                {
                    SelectedNode = e.Node.Nodes[0];
                }
                else
                {
                    SelectedNode = e.Node;
                }
            }
            else
            {
                SelectedNode = e.Node;
            }

            ShowSelectedNode();
        }

        public void SelectFirst()
        {
            if (Nodes.Count > 0)
            {
                if (Nodes[0].Nodes.Count > 0 && ExpandSelectFirst)
                {
                    Nodes[0].Expand();
                    SelectedNode = Nodes[0].Nodes[0];
                }
                else
                {
                    SelectedNode = Nodes[0];
                }

                Nodes[0].EnsureVisible();
            }

            ShowSelectedNode();
        }

        public void SelectPage(int pageIndex)
        {
            AllNodes.Clear();
            GetAllNodes(Nodes);
            if (AllNodes.ContainsKey(pageIndex))
            {
                SelectedNode = AllNodes[pageIndex];
                ShowSelectedNode();
            }
        }

        private readonly ConcurrentDictionary<int, TreeNode> AllNodes = new ConcurrentDictionary<int, TreeNode>();

        private void GetAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (MenuHelper.GetPageIndex(node) >= 0)
                {
                    AllNodes.AddOrUpdate(MenuHelper.GetPageIndex(node), node);
                }

                GetAllNodes(node.Nodes);
            }
        }

        private void ShowSelectedNode()
        {
            int index = MenuHelper.GetPageIndex(SelectedNode);
            TabControl?.SelectPage(index);
            MenuItemClick?.Invoke(SelectedNode, MenuHelper[SelectedNode], index);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (e.Delta > 10)
            {
                ScrollBarInfo.ScrollUp(Handle);
            }
            else if (e.Delta < -10)
            {
                ScrollBarInfo.ScrollDown(Handle);
            }

            SetScrollInfo();
        }

        public void SetScrollInfo()
        {
            if (Nodes.Count == 0)
            {
                Bar.Visible = false;
                return;
            }

            var si = ScrollBarInfo.GetInfo(Handle);
            Bar.Maximum = si.ScrollMax;
            Bar.Visible = si.ScrollMax > 0 && si.nMax > 0 && si.nPage > 0;
            Bar.Value = si.nPos;
            Bar.BringToFront();

            if (ScrollBarVisible != Bar.Visible)
            {
                ScrollBarVisible = Bar.Visible;
                Invalidate();
            }
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);

            if (e.Node != null && ExpandSelectFirst && e.Node.Nodes.Count > 0)
            {
                e.Node.Expand();
                SelectedNode = e.Node.Nodes[0];
            }
            else
            {
                SelectedNode = e.Node;
            }
        }

        protected override void OnAfterExpand(TreeViewEventArgs e)
        {
            base.OnAfterExpand(e);
            SetScrollInfo();
        }

        protected override void OnAfterCollapse(TreeViewEventArgs e)
        {
            base.OnAfterCollapse(e);
            SetScrollInfo();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            //隐藏滚动条
            ScrollBarInfo.ShowScrollBar(Handle, 3, false);//0:horizontal,1:vertical,3:both
        }
    }

    public static class TreeViewHelper
    {
        public static bool Selected(this DrawTreeNodeEventArgs e)
        {
            return e.State == TreeNodeStates.Selected || e.State == TreeNodeStates.Focused ||
                   e.State == (TreeNodeStates.Focused | TreeNodeStates.Selected);
        }

        public static TreeNode CreateNode(this UINavMenu navMenu, string text, int pageIndex)
        {
            return navMenu.CreateNode(new NavMenuItem(text, pageIndex));
        }

        public static TreeNode CreateNode(this UINavMenu navMenu, UIPage page)
        {
            return navMenu.CreateNode(new NavMenuItem(page.Text, page.PageIndex));
        }

        public static TreeNode CreateNode(this UINavMenu navMenu, NavMenuItem item)
        {
            TreeNode node = new TreeNode(item.Text);
            navMenu.Nodes.Add(node);
            navMenu.SetNodeItem(node, item);
            return node;
        }

        public static TreeNode CreateNode(this UINavBar navBar, string text, int pageIndex)
        {
            return navBar.CreateNode(new NavMenuItem(text, pageIndex));
        }

        public static TreeNode CreateNode(this UINavBar navBar, UIPage page)
        {
            return navBar.CreateNode(new NavMenuItem(page.Text, page.PageIndex));
        }

        public static TreeNode CreateNode(this UINavBar navBar, NavMenuItem item)
        {
            TreeNode node = new TreeNode(item.Text);
            navBar.Nodes.Add(node);
            navBar.SetNodeItem(node, item);
            return node;
        }

        public static TreeNode CreateChildNode(this UINavMenu navMenu, TreeNode parent, string text, int pageIndex)
        {
            return navMenu.CreateChildNode(parent, new NavMenuItem(text, pageIndex));
        }

        public static TreeNode CreateChildNode(this UINavMenu navMenu, TreeNode parent, UIPage page)
        {
            return navMenu.CreateChildNode(parent, new NavMenuItem(page.Text, page.PageIndex));
        }

        public static TreeNode CreateChildNode(this UINavMenu navMenu, TreeNode parent, NavMenuItem item)
        {
            TreeNode childNode = new TreeNode(item.Text);
            parent.Nodes.Add(childNode);
            navMenu.SetNodeItem(childNode, item);
            return childNode;
        }

        public static TreeNode CreateChildNode(this UINavBar navBar, TreeNode parent, string text, int pageIndex)
        {
            return navBar.CreateChildNode(parent, new NavMenuItem(text, pageIndex));
        }

        public static TreeNode CreateChildNode(this UINavBar navBar, TreeNode parent, UIPage page)
        {
            return navBar.CreateChildNode(parent, new NavMenuItem(page.Text, page.PageIndex));
        }

        public static TreeNode CreateChildNode(this UINavBar navBar, TreeNode parent, NavMenuItem item)
        {
            TreeNode childNode = new TreeNode(item.Text);
            parent.Nodes.Add(childNode);
            navBar.SetNodeItem(childNode, item);
            return childNode;
        }
    }

    public class NavMenuHelper
    {
        public NavMenuItem this[TreeNode node]
        {
            get
            {
                if (node == null) return null;

                if (Items.ContainsKey(node))
                    return Items[node];
                else
                    return null;
            }
        }

        private readonly ConcurrentDictionary<TreeNode, NavMenuItem> Items = new ConcurrentDictionary<TreeNode, NavMenuItem>();

        public string GetTipsText(TreeNode node)
        {
            return this[node] == null ? string.Empty : Items[node].TipsText;
        }

        public int GetSymbol(TreeNode node)
        {
            return this[node] == null ? 0 : Items[node].Symbol;
        }

        public int GetSymbolSize(TreeNode node)
        {
            return this[node] == null ? 0 : Items[node].SymbolSize;
        }

        public int GetPageIndex(TreeNode node)
        {
            return this[node] == null ? -1 : Items[node].PageIndex;
        }

        public object GetTag(TreeNode node)
        {
            return this[node] == null ? null : Items[node].Tag;
        }

        public void SetTipsText(TreeNode node, string tips)
        {
            if (node == null) return;

            CreateIfNotExist(node);
            Items[node].TipsText = tips;
            node.TreeView.Invalidate();
        }

        public void SetPageIndex(TreeNode node, int index)
        {
            if (node == null) return;

            CreateIfNotExist(node);
            Items[node].PageIndex = index;
        }

        public void SetSymbol(TreeNode node, int symbol, int symbolSize = 32)
        {
            if (node == null) return;

            CreateIfNotExist(node);
            Items[node].Symbol = symbol;
            Items[node].SymbolSize = symbolSize;
            node.TreeView.Invalidate();
        }

        private void CreateIfNotExist(TreeNode node)
        {
            if (node == null) return;

            if (!Items.ContainsKey(node))
            {
                NavMenuItem menuItem = new NavMenuItem();
                Items.TryAdd(node, menuItem);
            }
        }

        public void Add(TreeNode node, NavMenuItem item)
        {
            if (node == null || item == null) return;

            if (this[node] == null)
            {
                Items.TryAdd(node, item);
            }
            else
            {
                Items[node] = item;
            }

            node.ImageIndex = item.ImageIndex;
            node.SelectedImageIndex = item.SelectedImageIndex;
            node.Tag = item;
        }
    }

    public class NavMenuItem
    {
        public string Text { get; set; }

        public int ImageIndex { get; set; } = -1;

        public int SelectedImageIndex { get; set; } = -1;

        public int Symbol { get; set; }

        public int SymbolSize { get; set; } = 24;

        public int PageIndex { get; set; }

        public string TipsText { get; set; }

        public bool Enabled { get; set; } = true;

        public object Tag { get; set; }

        public NavMenuItem()
        {
        }

        public NavMenuItem(UIPage page)
        {
            Text = page.Text;
            PageIndex = page.PageIndex;
        }

        public NavMenuItem(string text, int pageIndex)
        {
            PageIndex = pageIndex;
            Text = text;
        }

        public NavMenuItem(int pageIndex)
        {
            PageIndex = pageIndex;
        }
    }
}