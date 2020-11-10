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
 * 2020-07-01: V2.2.6 解决引发事件所有结点重绘导致闪烁；解决滚轮失效问题。
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
            SetStyle(ControlStyles.DoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);

            base.UpdateStyles();

            BorderStyle = BorderStyle.None;
            //HideSelection = false;
            DrawMode = TreeViewDrawMode.OwnerDrawAll;
            FullRowSelect = true;
            ShowLines = false;
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

        [DefaultValue(false)]
        [Description("只显示一个打开的节点"), Category("SunnyUI")]
        public bool ShowOneNode { get; set; }

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

        public void ClearAll()
        {
            Nodes.Clear();
            MenuHelper.Clear();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            menuStyle = UIMenuStyle.Custom;
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
                menuStyle = UIMenuStyle.Custom;
                _style = UIStyle.Custom;
                Invalidate();
            }
        }

        private Color fillColor = Color.FromArgb(56, 56, 56);

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
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
                    menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }
        /// <summary>
        /// SizeChange导致treeNode闪屏
        /// </summary>
        const int TVM_SETEXTENDEDSTYLE = 0x112C;
        const int TVS_EX_DOUBLEBUFFER = 0x0004;

        private void UpdateExtendedStyles()
        {
            int Style = 0;

            if (DoubleBuffered)
                Style |= TVS_EX_DOUBLEBUFFER;

            if (Style != 0)
                Win32.User.SendMessage(Handle, TVM_SETEXTENDEDSTYLE, new IntPtr(TVS_EX_DOUBLEBUFFER), new IntPtr(Style));
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateExtendedStyles();
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

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        [DefaultValue(null)]
        [Description("关联的TabControl"), Category("SunnyUI")]
        public UITabControl TabControl { get; set; }

        private Color selectedColor = Color.FromArgb(36, 36, 36);

        private bool showTips;

        [Description("是否显示角标"), Category("SunnyUI")]
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

        [Description("角标文字字体"), Category("SunnyUI")]
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
        [Description("选中节点颜色"), Category("SunnyUI")]
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
        [Description("选中节点高亮颜色"), Category("SunnyUI")]
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
        [Description("鼠标移上颜色"), Category("SunnyUI")]
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

        /// <summary>
        /// 主题样式
        /// </summary>
        [DefaultValue(UIStyle.Blue), Description("主题样式"), Category("SunnyUI")]
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
        [Description("导航菜单主题风格"), Category("SunnyUI")]
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
        [Description("选中节点字体颜色"), Category("SunnyUI")]
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

        public int GetPageIndex(TreeNode node)
        {
            return MenuHelper.GetPageIndex(node);
        }

        public void SetNodeItem(TreeNode node, NavMenuItem item)
        {
            MenuHelper.Add(node, item);
        }

        public void SetNodePageIndex(TreeNode node, int pageIndex)
        {
            MenuHelper.SetPageIndex(node, pageIndex);
        }

        public void SetNodeTipsText(TreeNode node, string tipsText)
        {
            MenuHelper.SetTipsText(node, tipsText);
        }

        public void SetNodeSymbol(TreeNode node, int symbol, int symbolSize = 24)
        {
            MenuHelper.SetSymbol(node, symbol, symbolSize);
        }

        public void SetNodeImageIndex(TreeNode node, int imageIndex)
        {
            node.ImageIndex = imageIndex;
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
                        Color color = e.Node == SelectedNode ? SelectedForeColor : ForeColor;
                        e.Graphics.DrawFontImage(MenuHelper.GetSymbol(e.Node), MenuHelper.GetSymbolSize(e.Node), color, imageLeft + (MenuHelper.GetSymbolSize(e.Node) - fiSize.Width) / 2.0f, e.Bounds.Y + (e.Bounds.Height - fiSize.Height) / 2);
                    }
                    else
                    {
                        if (e.Node == SelectedNode && e.Node.SelectedImageIndex >= 0 && e.Node.SelectedImageIndex < ImageList.Images.Count)
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

                    e.Graphics.FillEllipse(TipsColor, tipsLeft, tipsTop, sfMax, sfMax);
                    e.Graphics.DrawString(MenuHelper.GetTipsText(e.Node), TipsFont, Color.White, tipsLeft + sfMax / 2.0f - tipsSize.Width / 2.0f, tipsTop + 1 + sfMax / 2.0f - tipsSize.Height / 2.0f);
                }
            }
        }

        private Color tipsColor = Color.Red;

        [DefaultValue(typeof(Color), "Red")]
        public Color TipsColor
        {
            get => tipsColor;
            set
            {
                tipsColor = value;
                Invalidate();
            }
        }

        [Description("展开节点后选中第一个子节点"), DefaultValue(true), Category("SunnyUI")]
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
                    AllNodes.TryAddOrUpdate(MenuHelper.GetPageIndex(node), node);
                }

                GetAllNodes(node.Nodes);
            }
        }

        private void ShowSelectedNode()
        {
            NavMenuItem item = MenuHelper[SelectedNode];
            if (item != null)
            {
                if (item.PageGuid != Guid.Empty)
                {
                    TabControl?.SelectPage(item.PageGuid);
                }
                else
                {
                    if (item.PageIndex >= 0)
                    {
                        TabControl?.SelectPage(item.PageIndex);
                    }
                }
            }

            MenuItemClick?.Invoke(SelectedNode, MenuHelper[SelectedNode], MenuHelper.GetPageIndex(SelectedNode));
        }

        public TreeNode GetTreeNode(int pageIndex)
        {
            return MenuHelper.GetTreeNode(pageIndex);
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

            if (ShowOneNode)
            {
                TreeNode node = e.Node.PrevNode;
                while (node != null)
                {
                    if (node.IsExpanded)
                    {
                        node.Collapse();
                    }
                    node = node.PrevNode;
                }

                node = e.Node.NextNode;
                while (node != null)
                {
                    if (node.IsExpanded)
                    {
                        node.Collapse();
                    }
                    node = node.NextNode;
                }
            }

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
            if (IsDisposed || Disposing) return;
            Win32.User.ShowScrollBar(Handle, 3, false);
        }

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
            var childNode = CreateChildNode(parent, new NavMenuItem(page));
            if (page.Symbol > 0)
            {
                MenuHelper.SetSymbol(childNode, page.Symbol, page.SymbolSize);
            }

            return childNode;
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