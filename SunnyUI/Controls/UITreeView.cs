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
 * 文件名称: UITreeView.cs
 * 文件说明: 树形列表
 * 当前版本: V3.1
 * 创建日期: 2020-05-05
 *
 * 2020-05-05: V2.2.5 增加文件
 * 2020-07-07: V2.2.6 全部重写，增加圆角，CheckBoxes等
 * 2020-08-12: V2.2.7 更新可设置背景色
 * 2021-07-19: V3.0.5 调整了显示CheckBoxes时图片位置
 * 2021-08-26: V3.0.6 CheckBoxes增加三态，感谢群友：笑口常开 
 * 2022-01-05: V3.0.9 TreeNodeStateSync: 节点点击时同步父节点和子节点的状态
 * 2022-03-19: V3.1.1 重构主题配色
 * 2022-04-01: V3.1.2 增加水平滚动条
 * 2022-04-01: V3.1.2 自定义行颜色，可通过代码给颜色值，SetNodePainter
 * 2022-05-15: V3.1.8 修复了一个设计期显示错误
 * 2022-05-15: V3.1.8 增加了点击文字改变CheckBox状态的NodeClickChangeCheckBoxes
 * 2022-10-28: V3.2.6 TreeNode支持imagekey绑定图标
 * 2022-11-03: V3.2.6 增加了可设置垂直滚动条宽度的属性
 * 2022-12-06: V3.3.0 增加了可自定义行的颜色
 * 2023-03-13: V3.3.3 增加MouseDoubleClick和MouseClick事件
 * 2023-03-26: V3.3.4 修改LabelEdit属性
 * 2023-05-13: V3.3.6 重构DrawString函数
 * 2023-07-02: V3.3.9 屏蔽DrawMode属性，默认为OwnerDrawAll
 * 2023-11-13: V3.5.2 重构主题
 * 2024-01-01: V3.6.2 增加可修改滚动条颜色
 * 2024-01-20: V3.6.3 自定义行颜色，可通过代码给颜色值，SetNodePainter，增加选中颜色
******************************************************************************/

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("AfterSelect")]
    [DefaultProperty("Nodes")]
    public sealed class UITreeView : UIPanel, IToolTip
    {
        private UIScrollBar Bar;
        private UIHorScrollBar HBar;

        private bool ScrollBarVisible;
        private bool HScrollBarVisible;
        private TreeViewEx view;

        public UITreeView()
        {
            InitializeComponent();
            SetStyleFlags(true, false);
            ShowText = false;
            view.HBar = HBar;
            view.Bar = Bar;
            SetScrollInfo();

            view.BeforeCheck += View_BeforeCheck;
            view.AfterCheck += View_AfterCheck;
            view.BeforeCollapse += View_BeforeCollapse;
            view.AfterCollapse += View_AfterCollapse;
            view.BeforeExpand += View_BeforeExpand;
            view.AfterExpand += View_AfterExpand;
            view.DrawNode += View_DrawNode;
            view.ItemDrag += View_ItemDrag;
            view.NodeMouseHover += View_NodeMouseHover;
            view.BeforeSelect += View_BeforeSelect;
            view.AfterSelect += View_AfterSelect;
            view.NodeMouseClick += View_NodeMouseClick;
            view.NodeMouseDoubleClick += View_NodeMouseDoubleClick;
            view.MouseUp += View_MouseUp;
            view.MouseDown += View_MouseDown;
            view.MouseMove += View_MouseMove;
            view.MouseEnter += View_MouseEnter;
            view.MouseLeave += View_MouseLeave;
            view.KeyPress += View_KeyPress;
            view.KeyDown += View_KeyDown;
            view.KeyUp += View_KeyUp;
            view.AfterLabelEdit += View_AfterLabelEdit;
            view.MouseDoubleClick += View_MouseDoubleClick;
            view.MouseClick += View_MouseClick;
        }

        private Color scrollBarColor = Color.FromArgb(80, 160, 255);

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("滚动条填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color ScrollBarColor
        {
            get => scrollBarColor;
            set
            {
                scrollBarColor = value;
                HBar.HoverColor = HBar.PressColor = HBar.ForeColor = value;
                Bar.HoverColor = Bar.PressColor = Bar.ForeColor = value;
                HBar.Style = Bar.Style = UIStyle.Custom;
                Invalidate();
            }
        }

        private Color scrollBarRectColor = Color.FromArgb(80, 160, 255);

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("滚动条边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color ScrollBarRectColor
        {
            get => scrollBarRectColor;
            set
            {
                scrollBarRectColor = value;
                Bar.RectColor = value;
                HBar.Style = Bar.Style = UIStyle.Custom;
                Invalidate();
            }
        }

        private Color scrollBarBackColor = Color.FromArgb(243, 249, 255);

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("滚动条背景颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "243, 249, 255")]
        public Color ScrollBarBackColor
        {
            get => scrollBarBackColor;
            set
            {
                scrollBarBackColor = value;
                HBar.FillColor = value;
                Bar.FillColor = value;
                HBar.Style = Bar.Style = UIStyle.Custom;
                Invalidate();
            }
        }

        /// <summary>
        /// 滚动条主题样式
        /// </summary>
        [DefaultValue(true), Description("滚动条主题样式"), Category("SunnyUI")]
        public bool ScrollBarStyleInherited
        {
            get => HBar != null && HBar.Style == UIStyle.Inherited;
            set
            {
                if (value)
                {
                    if (HBar != null) HBar.Style = UIStyle.Inherited;
                    if (Bar != null) Bar.Style = UIStyle.Inherited;

                    scrollBarColor = UIStyles.Blue.GridBarForeColor;
                    scrollBarBackColor = UIStyles.Blue.GridBarFillColor;
                    scrollBarRectColor = Bar.RectColor = UIStyles.Blue.RectColor;
                }

            }
        }

        public override void SetDPIScale()
        {
            base.SetDPIScale();
            view.SetDPIScale();
        }

        public void CheckedAll()
        {
            view.CheckedAll();
        }

        public void UnCheckedAll()
        {
            view.UnCheckedAll();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            view?.Dispose();
            Bar?.Dispose();
            HBar?.Dispose();
        }

        public event NodeLabelEditEventHandler AfterLabelEdit;
        public new event EventHandler MouseLeave;
        public new event EventHandler MouseEnter;
        public new event MouseEventHandler MouseMove;
        public new event MouseEventHandler MouseDown;
        public new event MouseEventHandler MouseUp;
        public new event KeyPressEventHandler KeyPress;
        public new event KeyEventHandler KeyDown;
        public new event KeyEventHandler KeyUp;
        public new event MouseEventHandler MouseDoubleClick;
        public new event MouseEventHandler MouseClick;

        private void View_MouseClick(object sender, MouseEventArgs e)
        {
            MouseClick?.Invoke(this, e);
        }

        private void View_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MouseDoubleClick?.Invoke(this, e);
        }

        private int scrollBarWidth = 0;

        [DefaultValue(0), Category("SunnyUI"), Description("垂直滚动条宽度，最小为原生滚动条宽度")]
        public int ScrollBarWidth
        {
            get => scrollBarWidth;
            set
            {
                scrollBarWidth = value;
                SetScrollInfo();
            }
        }

        private int scrollBarHandleWidth = 6;

        [DefaultValue(6), Category("SunnyUI"), Description("垂直滚动条滑块宽度，最小为原生滚动条宽度")]
        public int ScrollBarHandleWidth
        {
            get => scrollBarHandleWidth;
            set
            {
                scrollBarHandleWidth = value;
                if (Bar != null) Bar.FillWidth = value;
            }
        }

        protected override void OnContextMenuStripChanged(EventArgs e)
        {
            base.OnContextMenuStripChanged(e);
            if (view != null) view.ContextMenuStrip = ContextMenuStrip;
        }

        public int DrawLeft(TreeNode node)
        {
            if (view == null || node == null) return 0;
            return view.DrawLeft(node);
        }

        public void SetNodePainter(TreeNode node, Color backColor, Color foreColor)
        {
            if (view.IsNull()) return;
            if (view.Painter.NotContainsKey(node))
            {
                view.Painter.TryAdd(node, new UITreeNodePainter());
            }

            view.Painter[node].BackColor = backColor;
            view.Painter[node].ForeColor = foreColor;
            view.Painter[node].HaveHoveColor = false;
            view.Invalidate();
        }

        public void SetNodePainter(TreeNode node, Color backColor, Color foreColor, Color hoverColor, Color selectedColor, Color selectedForeColor)
        {
            if (view.IsNull()) return;
            if (view.Painter.NotContainsKey(node))
            {
                view.Painter.TryAdd(node, new UITreeNodePainter());
            }

            view.Painter[node].BackColor = backColor;
            view.Painter[node].ForeColor = foreColor;
            view.Painter[node].HoverColor = hoverColor;
            view.Painter[node].SelectedColor = selectedColor;
            view.Painter[node].SelectedForeColor = selectedForeColor;
            view.Painter[node].HaveHoveColor = true;
            view.Invalidate();
        }

        public void ClearNodePainter(TreeNode node)
        {
            if (view.IsNull()) return;
            if (view.Painter.ContainsKey(node))
            {
                view.Painter.TryRemove(node, out _);
                view.Invalidate();
            }
        }

        public void ClearAllNodePainter(TreeNode node)
        {
            if (view.IsNull()) return;
            view.Painter.Clear();
            view.Invalidate();
        }

        [Description("节点点击时同步父节点和子节点的状态"), Category("SunnyUI")]
        [DefaultValue(true)]
        public bool TreeNodeStateSync
        {
            get => view.TreeNodeStateSync;
            set => view.TreeNodeStateSync = value;
        }

        [Description("点击文字改变CheckBox状态"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool NodeClickChangeCheckBoxes
        {
            get => view.NodeClickChangeCheckBoxes;
            set => view.NodeClickChangeCheckBoxes = value;
        }

        private void View_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            AfterLabelEdit?.Invoke(this, e);
        }

        /// <summary>
        /// 需要额外设置ToolTip的控件
        /// </summary>
        /// <returns>控件</returns>
        public Control ExToolTipControl()
        {
            return view;
        }

        [DefaultValue(false)]
        public bool LabelEdit
        {
            get => view.LabelEdit;
            set => view.LabelEdit = value;
        }

        private void View_KeyUp(object sender, KeyEventArgs e)
        {
            KeyUp?.Invoke(this, e);
        }

        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown?.Invoke(this, e);
        }

        private void View_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress?.Invoke(this, e);
        }

        private void View_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave?.Invoke(this, e);
        }

        private void View_MouseEnter(object sender, EventArgs e)
        {
            MouseEnter?.Invoke(this, e);
        }

        private void View_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMove?.Invoke(this, e);
        }

        private void View_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDown?.Invoke(this, e);
        }

        private void View_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUp?.Invoke(this, e);
        }

        [Browsable(false)]
        public TreeView TreeView => view;

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (view != null)
            {
                selectedForeColor = view.SelectedForeColor = uiColor.TreeViewSelectedForeColor;
                view.FillColor = view.BackColor = fillColor = uiColor.TreeViewBackColor;

                rectColor = uiColor.RectColor;
                view.SelectedColor = selectedColor = uiColor.TreeViewSelectedColor;
                view.ForeColor = foreColor = uiColor.TreeViewForeColor;
                hoverColor = view.HoverColor = uiColor.TreeViewHoverColor;
                LineColor = uiColor.TreeViewLineColor;
            }

            if (Bar != null)
            {
                Bar.FillColor = uiColor.TreeViewBarFillColor;
                Bar.ForeColor = uiColor.TreeViewBarForeColor;
                Bar.HoverColor = uiColor.ButtonFillHoverColor;
                Bar.PressColor = uiColor.ButtonFillPressColor;

                scrollBarRectColor = Bar.RectColor = uiColor.RectColor;
                scrollBarColor = uiColor.GridBarForeColor;
                scrollBarBackColor = uiColor.GridBarFillColor;
            }

            if (HBar != null)
            {
                HBar.FillColor = uiColor.TreeViewBarFillColor;
                HBar.ForeColor = uiColor.TreeViewBarForeColor;
                HBar.HoverColor = uiColor.ButtonFillHoverColor;
                HBar.PressColor = uiColor.ButtonFillPressColor;
                scrollBarColor = uiColor.GridBarForeColor;
                scrollBarBackColor = uiColor.GridBarFillColor;
            }
        }

        private Color hoverColor = Color.FromArgb(220, 236, 255);
        [DefaultValue(typeof(Color), "220, 236, 255")]
        public Color HoverColor
        {
            get => hoverColor;
            set
            {
                view.HoverColor = hoverColor = value;
                Invalidate();
            }
        }

        private Color selectedColor = Color.FromArgb(80, 160, 255);
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color SelectedColor
        {
            get => selectedColor;
            set
            {
                view.SelectedColor = selectedColor = value;
                Invalidate();
            }
        }

        public Color selectedForeColor = Color.White;
        [DefaultValue(typeof(Color), "White")]
        public Color SelectedForeColor
        {
            get => selectedForeColor;
            set
            {
                view.SelectedForeColor = selectedForeColor = value;
                Invalidate();
            }
        }

        protected override void AfterSetFillColor(Color color)
        {
            base.AfterSetFillColor(color);
            if (view != null)
            {
                view.FillColor = color;
                view.BackColor = color;
            }

            if (Bar != null)
            {
                Bar.FillColor = color;
            }

            if (HBar != null)
            {
                HBar.FillColor = color;
            }
        }

        protected override void AfterSetForeColor(Color color)
        {
            base.AfterSetForeColor(color);
            view.ForeColor = color;
        }

        [Browsable(false)]
        [DefaultValue(TreeViewDrawMode.OwnerDrawAll)]
        public TreeViewDrawMode DrawMode
        {
            get => view.DrawMode;
            set => view.DrawMode = value;
        }

        [DefaultValue(false)]
        public bool CheckBoxes
        {
            get => view.CheckBoxes;
            set => view.CheckBoxes = value;
        }

        [DefaultValue(false)]
        public bool ShowLines
        {
            get => view.ShowLinesEx;
            set => view.ShowLinesEx = value;
        }

        [DefaultValue(28)]
        public int ItemHeight
        {
            get => view.ItemHeight;
            set => view.ItemHeight = value;
        }

        [Browsable(false)]
        [DefaultValue(null)]
        public TreeNode SelectedNode
        {
            get => view.SelectedNode;
            set => view.SelectedNode = value;
        }

        [DefaultValue(true)]
        public bool HideSelection
        {
            get => view.HideSelection;
            set => view.HideSelection = value;
        }

        [DefaultValue(-1)]
        [Localizable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [TypeConverter(typeof(NoneExcludedImageIndexConverter))]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            typeof(UITypeEditor))]
        [RelatedImageList("ImageList")]
        public int ImageIndex
        {
            get => view.ImageIndex;
            set => view.ImageIndex = value;
        }

        [Localizable(true)]
        [TypeConverter(typeof(ImageKeyConverter))]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            typeof(UITypeEditor))]
        [DefaultValue("")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [RelatedImageList("ImageList")]
        public string ImageKey
        {
            get => view.ImageKey;
            set => view.ImageKey = value;
        }

        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public ImageList ImageList
        {
            get => view.ImageList;
            set => view.ImageList = value;
        }

        [DefaultValue(null)]
        public ImageList StateImageList
        {
            get => view.StateImageList;
            set => view.StateImageList = value;
        }

        [Localizable(true)]
        [DefaultValue(19)]
        public int Indent
        {
            get => view.Indent;
            set => view.Indent = value;
        }

        [DefaultValue(typeof(Color), "Black")]
        public Color LineColor
        {
            get => view.LineColor;
            set => view.LineColor = value;
        }

        [DefaultValue("\\")]
        public string PathSeparator
        {
            get => view.PathSeparator;
            set => view.PathSeparator = value;
        }

        [DefaultValue(-1)]
        [TypeConverter(typeof(NoneExcludedImageIndexConverter))]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            typeof(UITypeEditor))]
        [RelatedImageList("ImageList")]
        public int SelectedImageIndex
        {
            get => view.SelectedImageIndex;
            set => view.SelectedImageIndex = value;
        }

        [Localizable(true)]
        [TypeConverter(typeof(ImageKeyConverter))]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            typeof(UITypeEditor))]
        [DefaultValue("")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [RelatedImageList("ImageList")]
        public string SelectedImageKey
        {
            get => view.SelectedImageKey;
            set => view.SelectedImageKey = value;
        }

        [DefaultValue(false)]
        public bool ShowNodeToolTips
        {
            get => view.ShowNodeToolTips;
            set => view.ShowNodeToolTips = value;
        }

        [DefaultValue(true)]
        public bool ShowPlusMinus
        {
            get => view.ShowPlusMinus;
            set => view.ShowPlusMinus = value;
        }

        [DefaultValue(false)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool Sorted
        {
            get => view.Sorted;
            set => view.Sorted = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IComparer TreeViewNodeSorter
        {
            get => view.TreeViewNodeSorter;
            set => view.TreeViewNodeSorter = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TreeNode TopNode
        {
            get => view.TopNode;
            set => view.TopNode = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int VisibleCount => view.VisibleCount;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [MergableProperty(false)]
        public TreeNodeCollection Nodes => view.Nodes;

        public event TreeViewCancelEventHandler BeforeCheck;

        public event TreeViewEventHandler AfterCheck;

        public event TreeViewCancelEventHandler BeforeCollapse;

        public event TreeViewEventHandler AfterCollapse;

        public event TreeViewCancelEventHandler BeforeExpand;

        public event TreeViewEventHandler AfterExpand;

        public event DrawTreeNodeEventHandler DrawNode;

        public event ItemDragEventHandler ItemDrag;

        public event TreeNodeMouseHoverEventHandler NodeMouseHover;

        public event TreeViewCancelEventHandler BeforeSelect;

        public event TreeViewEventHandler AfterSelect;

        public event TreeNodeMouseClickEventHandler NodeMouseClick;

        public event TreeNodeMouseClickEventHandler NodeMouseDoubleClick;

        private void View_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            NodeMouseDoubleClick?.Invoke(this, e);
        }

        private void View_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            NodeMouseClick?.Invoke(this, e);
        }

        private void View_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AfterSelect?.Invoke(this, e);
        }

        private void View_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            BeforeSelect?.Invoke(this, e);
        }

        private void View_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            NodeMouseHover?.Invoke(this, e);
        }

        private void View_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ItemDrag?.Invoke(this, e);
        }

        private void View_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            DrawNode?.Invoke(this, e);
        }

        private void View_AfterExpand(object sender, TreeViewEventArgs e)
        {
            AfterExpand?.Invoke(this, e);
        }

        private void View_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            BeforeExpand?.Invoke(this, e);
        }

        private void View_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            AfterCollapse?.Invoke(this, e);
        }

        private void View_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            BeforeCollapse?.Invoke(this, e);
        }

        private void View_AfterCheck(object sender, TreeViewEventArgs e)
        {
            AfterCheck?.Invoke(this, e);
        }

        private void View_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            BeforeCheck?.Invoke(this, e);
        }

        public void BeginUpdate()
        {
            view.BeginUpdate();
        }

        public void CollapseAll()
        {
            view.CollapseAll();
        }

        public void EndUpdate()
        {
            view.EndUpdate();
        }

        public void ExpandAll()
        {
            view.ExpandAll();
        }

        public TreeViewHitTestInfo HitTest(Point pt)
        {
            return view.HitTest(pt);
        }

        public TreeViewHitTestInfo HitTest(int x, int y)
        {
            return view.HitTest(x, y);
        }

        public int GetNodeCount(bool includeSubTrees)
        {
            return view.GetNodeCount(includeSubTrees);
        }

        public TreeNode GetNodeAt(Point pt)
        {
            return view.GetNodeAt(pt);
        }

        public TreeNode GetNodeAt(int x, int y)
        {
            return view.GetNodeAt(x, y);
        }

        public override string ToString()
        {
            return view.ToString();
        }

        public void Sort()
        {
            view.Sort();
        }

        private void SetPos()
        {
            if (view == null) return;
            view.Left = 2;
            view.Top = 2;
            view.Width = Width - 4;
            view.Height = Height - 4;

            int barWidth = Math.Max(ScrollBarInfo.VerticalScrollBarWidth(), ScrollBarWidth);

            if (Bar != null)
            {
                Bar.Top = 2;
                Bar.Left = Width - barWidth - 2;
                Bar.Width = barWidth;
                Bar.Height = Height - 4;
            }

            if (HBar != null)
            {
                HBar.Left = 2;
                HBar.Top = Height - ScrollBarInfo.HorizontalScrollBarHeight() - 2;
                HBar.Width = Width - (ScrollBarVisible ? ScrollBarInfo.VerticalScrollBarWidth() : 0) - 2 - 2;
                HBar.Height = ScrollBarInfo.HorizontalScrollBarHeight();
            }
        }

        /// <summary>
        /// 重载字体变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (DefaultFontSize < 0 && view != null) view.Font = this.Font;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (!ScrollBarVisible || !HScrollBarVisible) return;
            base.OnMouseWheel(e);

            if (e.Delta > 10)
                ScrollBarInfo.ScrollUp(view.Handle);
            else if (e.Delta < -10)
                ScrollBarInfo.ScrollDown(view.Handle);

            SetScrollInfo();
        }

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetScrollInfo();
            SetPos();
        }

        public void SetScrollInfo()
        {
            if (view == null || Bar == null || HBar == null) return;

            if (Nodes.Count == 0)
            {
                Bar.Visible = false;
                return;
            }

            var si = ScrollBarInfo.GetInfo(view.Handle);
            var si1 = ScrollBarInfo.GetHorInfo(view.Handle);

            SetPos();

            Bar.Maximum = si.ScrollMax;
            Bar.Visible = si.ScrollMax > 0 && si.nMax > 0 && si.nPage > 0;
            Bar.Value = si.nPos;
            Bar.BringToFront();

            if (ScrollBarVisible != Bar.Visible)
            {
                ScrollBarVisible = Bar.Visible;
                Invalidate();
            }

            HBar.Maximum = si1.ScrollMax;
            HBar.Visible = si1.ScrollMax > 0 && si1.nMax > 0 && si1.nPage > 0;
            HBar.Value = si1.nPos;
            HBar.BringToFront();

            if (HScrollBarVisible != HBar.Visible)
            {
                HScrollBarVisible = HBar.Visible;
                Invalidate();
            }
        }

        private void InitializeComponent()
        {
            view = new TreeViewEx();
            Bar = new UIScrollBar();
            HBar = new UIHorScrollBar();
            SuspendLayout();
            //
            // view
            //
            view.BackColor = Color.White;
            view.BorderStyle = BorderStyle.None;
            view.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            view.ForeColor = Color.FromArgb(48, 48, 48);
            view.FullRowSelect = true;
            view.ItemHeight = 28;
            view.Location = new Point(2, 2);
            view.Name = "view";
            view.ShowLines = false;
            view.Size = new Size(266, 176);
            view.TabIndex = 0;
            view.AfterCollapse += view_AfterCollapse;
            view.AfterExpand += view_AfterExpand;
            view.DrawNode += view_DrawNode;
            //
            // Bar
            //
            Bar.Font = new Font("宋体", 12F);
            Bar.Location = new Point(247, 3);
            Bar.Name = "Bar";
            Bar.Size = new Size(19, 173);
            Bar.Style = UIStyle.Custom;
            Bar.StyleCustomMode = true;
            Bar.TabIndex = 2;
            Bar.Visible = false;
            Bar.ValueChanged += Bar_ValueChanged;
            //
            // HBar
            //
            HBar.Font = new Font("宋体", 12F);
            HBar.Location = new Point(247, 3);
            HBar.Name = "HBar";
            HBar.Size = new Size(173, 19);
            HBar.Style = UIStyle.Custom;
            HBar.StyleCustomMode = true;
            HBar.TabIndex = 3;
            HBar.Visible = false;
            HBar.ValueChanged += HBar_ValueChanged;
            //
            // UITreeViewEx
            //
            Controls.Add(Bar);
            Controls.Add(HBar);
            Controls.Add(view);
            FillColor = Color.White;
            ResumeLayout(false);
        }

        private void Bar_ValueChanged(object sender, EventArgs e)
        {
            ScrollBarInfo.SetScrollValue(view.Handle, Bar.Value);
        }

        private void HBar_ValueChanged(object sender, EventArgs e)
        {
            ScrollBarInfo.SetHorScrollValue(view.Handle, HBar.Value);
        }

        private void view_AfterExpand(object sender, TreeViewEventArgs e)
        {
            SetScrollInfo();
        }

        private void view_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            SetScrollInfo();
        }

        private void view_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            SetScrollInfo();
        }

        internal sealed class NoneExcludedImageIndexConverter : ImageIndexConverter
        {
            protected override bool IncludeNoneAsStandardValue => false;
        }

        internal class TreeViewEx : TreeView
        {
            public ConcurrentDictionary<TreeNode, UITreeNodePainter> Painter = new ConcurrentDictionary<TreeNode, UITreeNodePainter>();

            public UIHorScrollBar HBar;
            public UIScrollBar Bar;

            private TreeNode CurrentNode;

            private bool showLines;

            public TreeViewEx()
            {
                DrawMode = TreeViewDrawMode.OwnerDrawAll;
                base.DoubleBuffered = true;
            }

            private float DefaultFontSize = -1;

            public void SetDPIScale()
            {
                if (DesignMode) return;
                if (!UIDPIScale.NeedSetDPIFont()) return;
                if (DefaultFontSize < 0) DefaultFontSize = this.Font.Size;
                this.Font = UIDPIScale.DPIScaleFont(this.Font, DefaultFontSize);
            }

            [DefaultValue(typeof(Color), "155, 200, 255")]
            public Color HoverColor { get; set; } = Color.FromArgb(155, 200, 255);

            public Color SelectedColor { get; set; } = Color.FromArgb(80, 160, 255);

            public Color SelectedForeColor { get; set; } = Color.White;

            public Color FillColor { get; set; } = Color.White;

            public bool ShowLinesEx
            {
                get => showLines;
                set
                {
                    showLines = value;
                    Invalidate();
                }
            }

            /// <summary>
            /// 重载鼠标移动事件
            /// </summary>
            /// <param name="e">鼠标参数</param>
            protected override void OnMouseMove(MouseEventArgs e)
            {
                base.OnMouseMove(e);
                var node = GetNodeAt(e.Location);
                if (node == null || CurrentNode == node) return;

                using var g = CreateGraphics();
                if (CurrentNode != null && CurrentNode != SelectedNode)
                {
                    ClearCurrentNode(g);
                }

                if (node != SelectedNode)
                {
                    CurrentNode = node;
                    OnDrawNode(new DrawTreeNodeEventArgs(g, CurrentNode, new Rectangle(0, CurrentNode.Bounds.Y, Width, CurrentNode.Bounds.Height), TreeNodeStates.Hot));
                }
            }

            /// <summary>
            /// 重载鼠标离开事件
            /// </summary>
            /// <param name="e">鼠标参数</param>
            protected override void OnMouseLeave(EventArgs e)
            {
                using var g = CreateGraphics();
                ClearCurrentNode(g);
            }

            private void ClearCurrentNode(Graphics g)
            {
                if (CurrentNode != null && CurrentNode != SelectedNode)
                {
                    OnDrawNode(new DrawTreeNodeEventArgs(g, CurrentNode, new Rectangle(0, CurrentNode.Bounds.Y, Width, CurrentNode.Bounds.Height), TreeNodeStates.Default));
                    OnDrawNode(new DrawTreeNodeEventArgs(g, CurrentNode, new Rectangle(0, CurrentNode.Bounds.Y, Width, CurrentNode.Bounds.Height), TreeNodeStates.Default));
                    CurrentNode = null;
                }
            }

            public int DrawLeft(TreeNode node)
            {
                int drawLeft;
                if (!HBar.Visible)
                    drawLeft = (node.Level + 1) * Indent + 3;
                else
                    drawLeft = -(int)(Width * HBar.Value * 1.0 / HBar.Maximum) + (node.Level + 1) * Indent + 3;

                return drawLeft;
            }

            protected override void OnDrawNode(DrawTreeNodeEventArgs e)
            {
                base.OnDrawNode(e);

                if (e.Node == null || Nodes.Count == 0) return;

                try
                {
                    if (!DicNodeStatus.ContainsKey(e.Node.GetHashCode()))
                    {
                        DicNodeStatus.Add(e.Node.GetHashCode(), false);
                    }

                    if (CheckBoxes)
                    {
                        if (TreeNodeStateSync && e.Node.Parent != null && DicNodeStatus.ContainsKey(e.Node.Parent.GetHashCode()) && !DicNodeStatus[e.Node.Parent.GetHashCode()])
                        {
                            SetParentNodeCheckedState(e.Node);
                        }
                    }

                    if (BorderStyle == BorderStyle.Fixed3D)
                    {
                        BorderStyle = BorderStyle.FixedSingle;
                    }

                    if (e.Node == null || e.Node.Bounds.Width <= 0 && e.Node.Bounds.Height <= 0 && e.Node.Bounds.X <= 0 && e.Node.Bounds.Y <= 0)
                    {
                        e.DrawDefault = true;
                    }
                    else
                    {
                        int drawLeft;
                        if (!HBar.Visible)
                            drawLeft = e.Bounds.X + (e.Node.Level + 1) * Indent + 3;
                        else
                            drawLeft = -(int)(Width * HBar.Value * 1.0 / HBar.Maximum) + (e.Node.Level + 1) * Indent + 3;

                        var checkBoxLeft = drawLeft - 2;
                        var imageLeft = drawLeft;
                        var haveImage = false;
                        var sf = TextRenderer.MeasureText(e.Node.Text, Font);

                        if (CheckBoxes)
                        {
                            drawLeft += 16;
                            imageLeft += 16;
                        }

                        if (ImageList != null && ImageList.Images.Count > 0 && ImageList.Images.ContainsIndex(e.Node.ImageIndex))
                        {
                            haveImage = true;
                            drawLeft += ImageList.ImageSize.Width + 6;
                        }

                        if (!haveImage && ImageList != null && ImageList.Images.Count > 0 && ImageList.Images.ContainsKey(e.Node.ImageKey))
                        {
                            haveImage = true;
                            drawLeft += ImageList.ImageSize.Width + 6;
                        }

                        var checkboxColor = ForeColor;
                        if (e.Node != null)
                        {
                            if (e.Node == SelectedNode)
                            {
                                Color sc = SelectedColor;
                                Color scf = SelectedForeColor;
                                if (Painter.ContainsKey(e.Node) && Painter[e.Node].HaveHoveColor)
                                {
                                    sc = Painter[e.Node].SelectedColor;
                                    scf = Painter[e.Node].SelectedForeColor;
                                }

                                e.Graphics.FillRectangle(sc, new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(Width, e.Node.Bounds.Height)));
                                e.Graphics.DrawString(e.Node.Text, Font, scf, new Rectangle(drawLeft, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), ContentAlignment.MiddleLeft);
                                checkboxColor = SelectedForeColor;
                            }
                            else if (e.Node == CurrentNode && (e.State & TreeNodeStates.Hot) != 0)
                            {
                                Color hc = HoverColor;
                                if (Painter.ContainsKey(e.Node) && Painter[e.Node].HaveHoveColor)
                                {
                                    hc = Painter[e.Node].HoverColor;
                                }

                                e.Graphics.FillRectangle(hc, new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(Width, e.Node.Bounds.Height)));
                                e.Graphics.DrawString(e.Node.Text, Font, ForeColor, new Rectangle(drawLeft, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), ContentAlignment.MiddleLeft);
                            }
                            else
                            {
                                Color fc = FillColor;
                                Color fcf = ForeColor;
                                if (Painter.ContainsKey(e.Node))
                                {
                                    fc = Painter[e.Node].BackColor;
                                    fcf = Painter[e.Node].ForeColor;
                                }

                                e.Graphics.FillRectangle(fc, new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(Width, e.Node.Bounds.Height)));
                                e.Graphics.DrawString(e.Node.Text, Font, fcf, new Rectangle(drawLeft, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), ContentAlignment.MiddleLeft);
                            }

                            if (haveImage)
                            {
                                Image image = null;
                                if (ImageList.Images.ContainsIndex(e.Node.ImageIndex))
                                    image = ImageList.Images[e.Node.ImageIndex];
                                if (image == null && ImageList.Images.ContainsKey(e.Node.ImageKey))
                                    image = ImageList.Images[e.Node.ImageKey];

                                if (e.Node == SelectedNode)
                                {
                                    if (ImageList.Images.ContainsIndex(e.Node.SelectedImageIndex))
                                        image = ImageList.Images[e.Node.SelectedImageIndex];
                                    if (image == null && ImageList.Images.ContainsKey(e.Node.SelectedImageKey))
                                        image = ImageList.Images[e.Node.SelectedImageKey];
                                }

                                if (image != null)
                                    e.Graphics.DrawImage(image, imageLeft, e.Bounds.Y + (e.Bounds.Height - ImageList.ImageSize.Height) / 2);
                            }

                            if (CheckBoxes)
                            {
                                if (!e.Node.Checked)
                                {
                                    e.Graphics.DrawRectangle(checkboxColor,
                                        new Rectangle(checkBoxLeft + 2, e.Bounds.Y + (ItemHeight - 12) / 2 - 1, 12, 12));
                                }
                                else
                                {
                                    using var pn = new Pen(checkboxColor, 2);
                                    var pt1 = new Point(checkBoxLeft + 2 + 2, e.Bounds.Y + (ItemHeight - 12) / 2 - 1 + 5);
                                    var pt2 = new Point(pt1.X + 3, pt1.Y + 3);
                                    var pt3 = new Point(pt2.X + 5, pt2.Y - 5);

                                    PointF[] CheckMarkLine = { pt1, pt2, pt3 };

                                    e.Graphics.SetHighQuality();
                                    e.Graphics.DrawLines(pn, CheckMarkLine);
                                    e.Graphics.SetDefaultQuality();
                                    e.Graphics.DrawRectangle(checkboxColor, new Rectangle(checkBoxLeft + 2, e.Bounds.Y + (ItemHeight - 12) / 2 - 1, 12, 12));
                                }

                                if (DicNodeStatus[e.Node.GetHashCode()])
                                {
                                    //var location = e.Node.Bounds.Location;
                                    //location.Offset(-29, 10);
                                    var location = new Point(checkBoxLeft + 5, e.Bounds.Y + (ItemHeight - 12) / 2 + 2);
                                    var size = new Size(7, 7);
                                    e.Graphics.FillRectangle(checkboxColor, new Rectangle(location, size)); //这里绘制的是正方形

                                }
                            }
                        }

                        var lineY = e.Bounds.Y + e.Node.Bounds.Height / 2 - 1;
                        int lineX;
                        if (!HBar.Visible)
                            lineX = 3 + e.Node.Level * Indent + 9;
                        else
                            lineX = -(int)(Width * HBar.Value * 1.0 / HBar.Maximum) + 3 + e.Node.Level * Indent + 9;

                        if (ShowLinesEx)
                        {
                            try
                            {
                                //绘制虚线
                                using var pn = new Pen(LineColor);
                                pn.DashStyle = DashStyle.Dot;
                                e.Graphics.DrawLine(pn, lineX, lineY, lineX + 10, lineY);

                                if (e.Node.Level >= 1)
                                {
                                    e.Graphics.DrawLine(pn, lineX, lineY, lineX, e.Bounds.Top);
                                    if (e.Node.NextNode != null)
                                        e.Graphics.DrawLine(pn, lineX, lineY, lineX, e.Node.Bounds.Bottom);

                                    var pNode = e.Node.Parent;
                                    while (pNode != null)
                                    {
                                        lineX -= Indent;

                                        if (Nodes.Count > 0)
                                        {
                                            if (pNode.NextNode != null)
                                                e.Graphics.DrawLine(pn, lineX, lineY, lineX, e.Node.Bounds.Top);

                                            if (pNode.NextNode != null)
                                                e.Graphics.DrawLine(pn, lineX, lineY, lineX, e.Node.Bounds.Bottom);
                                        }

                                        pNode = pNode.Parent;
                                    }
                                }
                                else
                                {
                                    if (e.Node != null && Nodes.Count > 0)
                                    {
                                        if (e.Node.PrevNode != null)
                                            e.Graphics.DrawLine(pn, lineX, lineY, lineX, e.Node.Bounds.Top);

                                        if (e.Node.NextNode != null)
                                            e.Graphics.DrawLine(pn, lineX, lineY, lineX, e.Node.Bounds.Bottom);
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception);
                            }
                        }

                        if (!HBar.Visible)
                            lineX = 3 + e.Node.Level * Indent + 9;
                        else
                            lineX = -(int)(Width * HBar.Value * 1.0 / HBar.Maximum) + 3 + e.Node.Level * Indent + 9;

                        //绘制左侧+号
                        if (ShowPlusMinus && e.Node.Nodes.Count > 0)
                        {
                            e.Graphics.FillRectangle(Color.White, new Rectangle(lineX - 4, lineY - 4, 8, 8));
                            e.Graphics.DrawRectangle(UIFontColor.Primary, new Rectangle(lineX - 4, lineY - 4, 8, 8));
                            e.Graphics.DrawLine(UIFontColor.Primary, lineX - 2, lineY, lineX + 2, lineY);
                            if (!e.Node.IsExpanded)
                                e.Graphics.DrawLine(UIFontColor.Primary, lineX, lineY - 2, lineX, lineY + 2);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            public bool TreeNodeSelected(DrawTreeNodeEventArgs e)
            {
                return e.State == TreeNodeStates.Selected || e.State == TreeNodeStates.Focused ||
                       e.State == (TreeNodeStates.Focused | TreeNodeStates.Selected);
            }

            protected override void WndProc(ref Message m)
            {
                if (IsDisposed || Disposing) return;
                if (m.Msg == Win32.User.WM_ERASEBKGND)
                {
                    m.Result = IntPtr.Zero;
                    return;
                }

                if (Bar != null && !Bar.Visible)
                {
                    if (m.Msg == 522)
                    {
                        m.Result = IntPtr.Zero;
                        return;
                    }
                }

                base.WndProc(ref m);
            }

            private Dictionary<int, bool> DicNodeStatus = new Dictionary<int, bool>();

            protected override void OnAfterCheck(TreeViewEventArgs e)
            {
                base.OnAfterCheck(e);
                if (e.Action == TreeViewAction.ByMouse && TreeNodeStateSync) //鼠标点击
                {
                    DicNodeStatus[e.Node.GetHashCode()] = false;

                    SetChildNodeCheckedState(e.Node, e.Node.Checked);
                    SetParentNodeCheckedState(e.Node, true);
                }
            }

            public bool TreeNodeStateSync { get; set; } = true;

            private void SetParentNodeCheckedState(TreeNode currNode, bool ByMouse = false)
            {
                if (currNode.Parent == null)
                {
                    return;
                }

                TreeNode parentNode = currNode.Parent; //获得当前节点的父节点
                var count = parentNode.Nodes.Cast<TreeNode>().Where(n => n.Checked).ToList().Count;

                //判断节点Checked是否改变，只有改变时才赋值，否则不变更，以防止频繁触发OnAfterCheck事件
                bool bChecked = count == parentNode.Nodes.Count;
                if (parentNode.Checked != bChecked)
                {
                    parentNode.Checked = bChecked;
                }

                var half = parentNode.Nodes.Cast<TreeNode>().Where(n => (DicNodeStatus.ContainsKey(n.GetHashCode()) ? DicNodeStatus[n.GetHashCode()] : false)).ToList().Count;

                if ((count > 0 && count < parentNode.Nodes.Count) || half > 0)
                {
                    DicNodeStatus[parentNode.GetHashCode()] = true;
                }
                else
                {
                    DicNodeStatus[parentNode.GetHashCode()] = false;
                }

                if (ByMouse)
                {
                    using var g = CreateGraphics();
                    OnDrawNode(new DrawTreeNodeEventArgs(g, parentNode, new Rectangle(0, parentNode.Bounds.Y, Width, parentNode.Bounds.Height), TreeNodeStates.Hot));

                    if (parentNode.Parent != null) //如果父节点之上还有父节点
                    {
                        SetParentNodeCheckedState(parentNode, true); //递归调用
                    }
                }
            }

            //选中节点之后，选中节点的所有子节点
            private void SetChildNodeCheckedState(TreeNode currNode, bool state)
            {
                TreeNodeCollection nodes = currNode.Nodes; //获取所有子节点
                if (nodes.Count > 0) //存在子节点
                {
                    foreach (TreeNode tn in nodes)
                    {
                        DicNodeStatus[tn.GetHashCode()] = false;
                        tn.Checked = state;
                        SetChildNodeCheckedState(tn, state);//递归调用子节点的子节点
                    }
                }
            }

            public bool NodeClickChangeCheckBoxes { get; set; }

            protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
            {
                base.OnNodeMouseClick(e);
                if (CheckBoxes && NodeClickChangeCheckBoxes)
                {
                    int drawLeft = e.Node.Bounds.X;
                    if (ImageList != null)
                        drawLeft -= ImageList.ImageSize.Width;

                    if (e.Location.X > drawLeft)
                    {
                        e.Node.Checked = !e.Node.Checked;
                        DicNodeStatus[e.Node.GetHashCode()] = false;
                        SetChildNodeCheckedState(e.Node, e.Node.Checked);
                        SetParentNodeCheckedState(e.Node, true);
                    }
                }
            }

            public void CheckedAll()
            {
                foreach (TreeNode node in Nodes)
                {
                    node.Checked = true;
                    SetChildNodeCheckedState(node, true);
                }
            }

            public void UnCheckedAll()
            {
                foreach (TreeNode node in Nodes)
                {
                    node.Checked = false;
                    SetChildNodeCheckedState(node, false);
                }
            }
        }
    }
}