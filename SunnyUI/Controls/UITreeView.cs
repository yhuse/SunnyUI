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
 * 文件名称: UITreeView.cs
 * 文件说明: 树形列表
 * 当前版本: V2.2
 * 创建日期: 2020-05-05
 *
 * 2020-05-05: V2.2.5 增加文件
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    public sealed class UITreeView : TreeView, IStyleInterface
    {
        private readonly UIScrollBar Bar = new UIScrollBar();

        public UITreeView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DrawMode = TreeViewDrawMode.OwnerDrawAll;
            DoubleBuffered = true;
            Font = UIFontColor.Font;

            ItemHeight = 28;
            BackColor = Color.White;

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

        private Color fillColor = Color.White;

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("背景颜色"), Category("自定义")]
        [DefaultValue(typeof(Color), "White")]
        public Color FillColor
        {
            get => fillColor;
            set
            {
                if (fillColor != value)
                {
                    fillColor = value;
                    _style = UIStyle.Custom;
                    Invalidate();
                }
            }
        }

        private Color foreColor = UIFontColor.Primary;

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("背景颜色"), Category("自定义")]
        [DefaultValue(typeof(Color), "48, 48, 48")]
        public override Color ForeColor
        {
            get => foreColor;
            set
            {
                if (foreColor != value)
                {
                    foreColor = value;
                    _style = UIStyle.Custom;
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

        private Color selectedColor = Color.FromArgb(80, 160, 255);

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

        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color SelectedColor
        {
            get => selectedColor;
            set
            {
                if (selectedColor != value)
                {
                    selectedColor = value;
                    _style = UIStyle.Custom;
                    Invalidate();
                }
            }
        }

        private Color selectedHighColor = UIColor.White;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("选中Tab页高亮"), Category("自定义")]
        [DefaultValue(typeof(Color), "White")]
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

        private Color hoverColor = Color.FromArgb(155, 200, 255);

        [DefaultValue(typeof(Color), "155, 200, 255")]
        public Color HoverColor
        {
            get => hoverColor;
            set
            {
                hoverColor = value;
                _style = UIStyle.Custom;
            }
        }

        private UIStyle _style = UIStyle.Blue;

        [DefaultValue(UIStyle.Blue)]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        private Color rectColor = UIStyles.Blue.RectColor;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("自定义")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color RectColor
        {
            get => rectColor;
            set => SetRectColor(value);
        }

        /// <summary>
        /// 设置边框颜色
        /// </summary>
        /// <param name="value">颜色</param>
        private void SetRectColor(Color value)
        {
            if (rectColor != value)
            {
                rectColor = value;
                _style = UIStyle.Custom;
                Invalidate();
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

            selectedForeColor = selectedHighColor = UIColor.White;

            rectColor = uiColor.RectColor;
            fillColor = UIColor.White;
            selectedColor = uiColor.TreeViewSelectedColor;
            foreColor = UIFontColor.Primary;
            hoverColor = uiColor.TreeViewHoverColor;

            if (Bar != null)
            {
                Bar.FillColor = UIColor.White;
                Bar.ForeColor = uiColor.PrimaryColor;
                Bar.HoverColor = uiColor.ButtonFillHoverColor;
                Bar.PressColor = uiColor.ButtonFillPressColor;
            }

            Invalidate();

            Invalidate();
        }

        private Color selectedForeColor = UIColor.White;

        [DefaultValue(typeof(Color), "White")]
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

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            if (BorderStyle == BorderStyle.Fixed3D)
            {
                BorderStyle = BorderStyle.FixedSingle;
            }

            SetScrollInfo();
            CheckBoxes = false;

            if (e.Node == null || (e.Node.Bounds.Width <= 0 && e.Node.Bounds.Height <= 0 && e.Node.Bounds.X <= 0 && e.Node.Bounds.Y <= 0))
            {
                e.DrawDefault = true;
            }
            else
            {
                int drawLeft = (e.Node.Level + 1) * 19 + 3;
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
                        e.Graphics.DrawFontImage(MenuHelper.GetSymbol(e.Node), MenuHelper.GetSymbolSize(e.Node), Color.White,
                            imageLeft + (MenuHelper.GetSymbolSize(e.Node) - fiSize.Width) / 2.0f, e.Bounds.Y + (e.Bounds.Height - fiSize.Height) / 2);
                    }
                    else
                    {
                        if (TreeNodeSelected(e) && e.Node.SelectedImageIndex >= 0 && e.Node.SelectedImageIndex < ImageList.Images.Count)
                            e.Graphics.DrawImage(ImageList.Images[e.Node.SelectedImageIndex], imageLeft, e.Bounds.Y + (e.Bounds.Height - ImageList.ImageSize.Height) / 2);
                        else
                            e.Graphics.DrawImage(ImageList.Images[e.Node.ImageIndex], imageLeft, e.Bounds.Y + (e.Bounds.Height - ImageList.ImageSize.Height) / 2);
                    }
                }

                int lineY = e.Bounds.Y + e.Node.Bounds.Height / 2 - 1;
                int lineX = 3 + e.Node.Level * 19 + 9;

                try
                {
                    //绘制虚线
                    Pen pn = new Pen(UIFontColor.Primary);
                    pn.DashStyle = DashStyle.Dot;
                    e.Graphics.DrawLine(pn, lineX, lineY, lineX + 10, lineY);

                    if (e.Node.Level >= 1)
                    {
                        e.Graphics.DrawLine(pn, lineX, lineY, lineX, e.Bounds.Top);
                        if (e.Node.NextNode != null)
                        {
                            e.Graphics.DrawLine(pn, lineX, lineY, lineX, e.Node.Bounds.Bottom);
                        }

                        TreeNode pNode = e.Node.Parent;
                        while (pNode != null)
                        {
                            lineX -= 19;

                            if (pNode.Level == 0 && pNode.NextNode != null)
                            {
                                e.Graphics.DrawLine(pn, lineX, lineY, lineX, e.Node.Bounds.Top);
                            }

                            if (pNode.NextNode != null)
                            {
                                e.Graphics.DrawLine(pn, lineX, lineY, lineX, e.Node.Bounds.Bottom);
                            }

                            pNode = pNode.Parent;
                        }
                    }
                    else
                    {
                        if (e.Node.PrevNode != null)
                        {
                            e.Graphics.DrawLine(pn, lineX, lineY, lineX, e.Node.Bounds.Top);
                        }

                        if (e.Node.NextNode != null)
                        {
                            e.Graphics.DrawLine(pn, lineX, lineY, lineX, e.Node.Bounds.Bottom);
                        }
                    }

                    pn.Dispose();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }

                lineX = 3 + e.Node.Level * 19 + 9;
                //绘制左侧+号
                if (e.Node.Nodes.Count > 0)
                {
                    e.Graphics.FillRectangle(Color.White, new Rectangle(lineX - 4, lineY - 4, 8, 8));
                    e.Graphics.DrawRectangle(UIFontColor.Primary, new Rectangle(lineX - 4, lineY - 4, 8, 8));
                    e.Graphics.DrawLine(UIFontColor.Primary, lineX - 2, lineY, lineX + 2, lineY);
                    if (!e.Node.IsExpanded)
                    {
                        e.Graphics.DrawLine(UIFontColor.Primary, lineX, lineY - 2, lineX, lineY + 2);
                    }
                }

                if (ShowTips && MenuHelper.GetTipsText(e.Node).IsValid())
                {
                    SizeF tipsSize = e.Graphics.MeasureString(MenuHelper.GetTipsText(e.Node), TipsFont);
                    float sfMax = Math.Max(tipsSize.Width, tipsSize.Height) + 1;
                    float tipsLeft = Width - (ScrollBarVisible ? 50 : 30) - 6 - sfMax;
                    float tipsTop = e.Bounds.Y + (ItemHeight - sfMax) / 2;

                    e.Graphics.FillEllipse(Color.Red,  tipsLeft, tipsTop, sfMax, sfMax);
                    e.Graphics.DrawString(MenuHelper.GetTipsText(e.Node), TipsFont, Color.White, tipsLeft + sfMax / 2.0f - tipsSize.Width / 2.0f, tipsTop + sfMax / 2.0f - tipsSize.Height / 2.0f);
                }
            }
        }

        private bool TreeNodeSelected(DrawTreeNodeEventArgs e)
        {
            return e.State == TreeNodeStates.Selected || e.State == TreeNodeStates.Focused ||
                   e.State == (TreeNodeStates.Focused | TreeNodeStates.Selected);
        }

        public string Version { get; }

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);

            if (e.Node != null && e.Node.Nodes.Count > 0)
            {
                if (e.X >= (e.Node.Level + 1) * 19 + 3)
                {
                    if (e.Node.IsExpanded)
                    {
                        e.Node.Collapse();
                    }
                    else
                    {
                        e.Node.Expand();
                    }
                }
            }

            SelectedNode = e.Node;
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
            ScrollBarInfo.ShowScrollBar(Handle, 3, false);
            if (m.Msg == 0xf || m.Msg == 0x133)
            {
                if (BorderStyle == BorderStyle.FixedSingle)
                {
                    ControlEx.ResetBorderColor(m, this, 1, Enabled ? RectColor : UIStyles.Blue.RectDisableColor);
                }
            }
        }
    }
}