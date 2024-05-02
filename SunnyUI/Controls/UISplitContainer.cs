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
 * 文件名称: UISplitContainer.cs
 * 文件说明: 分割容器
 * 当前版本: V3.1
 * 创建日期: 2021-10-30
 *
 * 2021-10-30: V3.0.8 增加文件说明
 * 2022-04-03: V3.1.3 增加主题样式
 * 2022-04-20: V3.1.5 修复调用Collapse()后，展开/收回操作失效
 * 2022-12-06: V3.3.0 去掉SplitterWidth限制
 * 2022-12-06: V3.3.0 SplitterWidth值小的时不绘制箭头
******************************************************************************/
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public sealed class UISplitContainer : SplitContainer, IStyleInterface, IZoomScale
    {
        private enum UIMouseType
        {
            None,
            Button,
            Split
        }

        private enum UISplitPanelState
        {
            Collapsed = 0,
            Expanded = 1,
        }

        private enum UIControlState
        {
            /// <summary>
            ///  正常。
            /// </summary>
            Normal,
            /// <summary>
            /// 鼠标进入。
            /// </summary>
            Hover,
        }

        public enum UICollapsePanel
        {
            None = 0,
            Panel1 = 1,
            Panel2 = 2,
        }

        private UICollapsePanel _collapsePanel = UICollapsePanel.Panel1;
        private UISplitPanelState _splitPanelState = UISplitPanelState.Expanded;
        private UIControlState _uiControlState;
        private int _lastDistance;
        private int _minSize;
        private UIMouseType _uiMouseType;
        private readonly object EventCollapseClick = new object();

        public UISplitContainer()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
            _lastDistance = SplitterDistance;
            base.SplitterWidth = 11;
            MinimumSize = new Size(20, 20);
            Version = UIGlobal.Version;
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

        private Color barColor = Color.FromArgb(56, 56, 56);

        [DefaultValue(typeof(Color), "56, 56, 56"), Category("SunnyUI")]
        [Description("分割栏背景色")]
        public Color BarColor
        {
            get => barColor;
            set
            {
                barColor = value;
                Invalidate();
            }
        }

        private Color handleColor = Color.FromArgb(106, 106, 106);

        [DefaultValue(typeof(Color), "106, 106, 106"), Category("SunnyUI")]
        [Description("分割栏按钮背景色")]
        public Color HandleColor
        {
            get => handleColor;
            set
            {
                handleColor = value;
                Invalidate();
            }
        }

        private Color handleHoverColor = Color.FromArgb(186, 186, 186);

        [DefaultValue(typeof(Color), "186, 186, 186"), Category("SunnyUI")]
        [Description("分割栏按钮鼠标移上背景色")]
        public Color HandleHoverColor
        {
            get => handleHoverColor;
            set
            {
                handleHoverColor = value;
                Invalidate();
            }
        }

        private Color arrowColor = Color.FromArgb(80, 160, 255);

        [DefaultValue(typeof(Color), "80, 160, 255"), Category("SunnyUI")]
        [Description("分割栏按钮箭头背景色")]
        public Color ArrowColor
        {
            get => arrowColor;
            set
            {
                arrowColor = value;
                Invalidate();
            }
        }

        public event EventHandler CollapseClick
        {
            add => Events.AddHandler(EventCollapseClick, value);
            remove => Events.RemoveHandler(EventCollapseClick, value);
        }

        [DefaultValue(UICollapsePanel.Panel1), Category("SunnyUI")]
        [Description("点击后收起的Panel")]
        public UICollapsePanel CollapsePanel
        {
            get => _collapsePanel;
            set
            {
                if (_collapsePanel != value)
                {
                    Expand();
                    _collapsePanel = value;
                    Invalidate();
                }
            }
        }

        private int DefaultCollapseWidth => 80;

        private int DefaultArrowWidth => 24;

        private Rectangle CollapseRect
        {
            get
            {
                if (_collapsePanel == UICollapsePanel.None)
                {
                    return Rectangle.Empty;
                }

                Rectangle rect = SplitterRectangle;
                if (Orientation == Orientation.Horizontal)
                {
                    rect.X = (Width - DefaultCollapseWidth) / 2;
                    rect.Width = DefaultCollapseWidth;
                }
                else
                {
                    rect.Y = (Height - DefaultCollapseWidth) / 2;
                    rect.Height = DefaultCollapseWidth;
                }

                return rect;
            }
        }

        private UISplitPanelState SplitPanelState
        {
            get => _splitPanelState;
            set
            {
                if (_splitPanelState != value)
                {
                    switch (value)
                    {
                        case UISplitPanelState.Expanded:
                            Expand();
                            break;
                        case UISplitPanelState.Collapsed:
                            Collapse();
                            break;

                    }

                    _splitPanelState = value;
                }
            }
        }

        private UIControlState ControlState
        {
            set
            {
                if (_uiControlState != value)
                {
                    _uiControlState = value;
                    Invalidate(CollapseRect);
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

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false), Browsable(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get;
        }

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString
        {
            get; set;
        }

        public void Collapse()
        {
            if (_collapsePanel != UICollapsePanel.None && SplitPanelState == UISplitPanelState.Expanded)
            {
                _lastDistance = SplitterDistance;
                if (_collapsePanel == UICollapsePanel.Panel1)
                {
                    _minSize = Panel1MinSize;
                    Panel1MinSize = 0;
                    SplitterDistance = 0;
                }
                else
                {
                    int width = Orientation == Orientation.Horizontal ?
                        Height : Width;
                    _minSize = Panel2MinSize;
                    Panel2MinSize = 0;
                    SplitterDistance = width - SplitterWidth - Padding.Vertical;
                }

                _splitPanelState = UISplitPanelState.Collapsed;
                Invalidate(SplitterRectangle);
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
            Invalidate(SplitterRectangle);
        }

        public void Expand()
        {
            if (_collapsePanel != UICollapsePanel.None && SplitPanelState == UISplitPanelState.Collapsed)
            {
                if (_collapsePanel == UICollapsePanel.Panel1)
                {
                    Panel1MinSize = _minSize;
                }
                else
                {
                    Panel2MinSize = _minSize;
                }

                SplitterDistance = _lastDistance;
                _splitPanelState = UISplitPanelState.Expanded;
                Invalidate(SplitterRectangle);
            }
        }

        private void OnCollapseClick(EventArgs e)
        {
            SplitPanelState = SplitPanelState == UISplitPanelState.Collapsed ?
                UISplitPanelState.Expanded : UISplitPanelState.Collapsed;
            EventHandler handler = Events[EventCollapseClick] as EventHandler;
            handler?.Invoke(this, e);
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Panel1Collapsed || Panel2Collapsed)
            {
                return;
            }

            Rectangle rect = SplitterRectangle;
            bool bHorizontal = Orientation == Orientation.Horizontal;

            e.Graphics.FillRectangle(BarColor, rect);
            if (_collapsePanel == UICollapsePanel.None)
            {
                return;
            }

            //if (SplitterWidth < 11) SplitterWidth = 11;

            Rectangle arrowRect = CalcArrowRect(CollapseRect);
            Color handleRectColor = _uiControlState == UIControlState.Hover ? handleHoverColor : HandleColor;
            Point[] points = GetHandlePoints();
            using Brush br = new SolidBrush(handleRectColor);
            e.Graphics.SetHighQuality();
            e.Graphics.FillPolygon(br, points);
            e.Graphics.SetDefaultQuality();

            if (SplitterWidth >= 9)
            {
                switch (_collapsePanel)
                {
                    case UICollapsePanel.Panel1:
                        if (bHorizontal)
                        {
                            e.Graphics.DrawFontImage(SplitPanelState == UISplitPanelState.Collapsed ? 61703 : 61702,
                                22, arrowColor, arrowRect);
                        }
                        else
                        {
                            e.Graphics.DrawFontImage(SplitPanelState == UISplitPanelState.Collapsed ? 61701 : 61700,
                                22, arrowColor, arrowRect);
                        }
                        break;
                    case UICollapsePanel.Panel2:
                        if (bHorizontal)
                        {
                            e.Graphics.DrawFontImage(SplitPanelState == UISplitPanelState.Collapsed ? 61702 : 61703,
                                22, arrowColor, arrowRect);
                        }
                        else
                        {
                            e.Graphics.DrawFontImage(SplitPanelState == UISplitPanelState.Collapsed ? 61700 : 61701,
                                22, arrowColor, arrowRect);
                        }
                        break;
                }
            }
        }

        private Point[] GetHandlePoints()
        {
            bool bCollapsed = SplitPanelState == UISplitPanelState.Collapsed;

            if (Orientation == Orientation.Horizontal)
            {
                if ((CollapsePanel == UICollapsePanel.Panel1 && !bCollapsed) ||
                    (CollapsePanel == UICollapsePanel.Panel2 && bCollapsed))
                {
                    return new[]
                    {
                        new Point(CollapseRect.Left + 2, CollapseRect.Top),
                        new Point(CollapseRect.Right - 2, CollapseRect.Top),
                        new Point(CollapseRect.Right, CollapseRect.Bottom),
                        new Point(CollapseRect.Left, CollapseRect.Bottom),
                        new Point(CollapseRect.Left + 2, CollapseRect.Top)
                    };
                }

                if ((CollapsePanel == UICollapsePanel.Panel1 && bCollapsed) ||
                    (CollapsePanel == UICollapsePanel.Panel2 && !bCollapsed))
                {
                    return new[]
                    {
                        new Point(CollapseRect.Left, CollapseRect.Top),
                        new Point(CollapseRect.Right, CollapseRect.Top),
                        new Point(CollapseRect.Right - 2, CollapseRect.Bottom),
                        new Point(CollapseRect.Left + 2, CollapseRect.Bottom),
                        new Point(CollapseRect.Left, CollapseRect.Top)
                    };
                }
            }

            if (Orientation == Orientation.Vertical)
            {
                if ((CollapsePanel == UICollapsePanel.Panel1 && !bCollapsed) ||
                    (CollapsePanel == UICollapsePanel.Panel2 && bCollapsed))
                {
                    return new[]
                    {
                        new Point(CollapseRect.Left, CollapseRect.Top + 2),
                        new Point(CollapseRect.Right, CollapseRect.Top),
                        new Point(CollapseRect.Right, CollapseRect.Bottom),
                        new Point(CollapseRect.Left, CollapseRect.Bottom - 2),
                        new Point(CollapseRect.Left, CollapseRect.Top + 2)
                    };
                }

                if ((CollapsePanel == UICollapsePanel.Panel1 && bCollapsed) ||
                    (CollapsePanel == UICollapsePanel.Panel2 && !bCollapsed))
                {
                    return new[]
                    {
                        new Point(CollapseRect.Left,CollapseRect.Top),
                        new Point(CollapseRect.Right,CollapseRect.Top+2),
                        new Point(CollapseRect.Right,CollapseRect.Bottom-2),
                        new Point(CollapseRect.Left,CollapseRect.Bottom),
                        new Point(CollapseRect.Left,CollapseRect.Top)
                    };
                }
            }

            return new Point[0];
        }

        /// <summary>
        /// 重载鼠标移动事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //如果鼠标的左键没有按下，重置鼠标状态
            if (e.Button != MouseButtons.Left)
            {
                _uiMouseType = UIMouseType.None;
            }

            Rectangle collapseRect = CollapseRect;
            Point mousePoint = e.Location;

            //鼠标在Button矩形里，并且不是在拖动
            if (collapseRect.Contains(mousePoint) && _uiMouseType != UIMouseType.Split)
            {
                Capture = false;
                SetCursor(Cursors.Hand);
                ControlState = UIControlState.Hover;
                return;
            }

            //鼠标在分隔栏矩形里
            if (SplitterRectangle.Contains(mousePoint))
            {
                ControlState = UIControlState.Normal;

                //如果已经在按钮按下了鼠标或者已经收缩，就不允许拖动了
                if (_uiMouseType == UIMouseType.Button ||
                    (_collapsePanel != UICollapsePanel.None && SplitPanelState == UISplitPanelState.Collapsed))
                {
                    Capture = false;
                    base.Cursor = Cursors.Default;
                    return;
                }

                //鼠标没有按下，设置Split光标
                if (_uiMouseType == UIMouseType.None && !IsSplitterFixed)
                {
                    SetCursor(Orientation == Orientation.Horizontal ? Cursors.HSplit : Cursors.VSplit);
                    return;
                }
            }

            ControlState = UIControlState.Normal;

            //正在拖动分隔栏
            if (_uiMouseType == UIMouseType.Split && !IsSplitterFixed)
            {
                SetCursor(Orientation == Orientation.Horizontal ? Cursors.HSplit : Cursors.VSplit);
                base.OnMouseMove(e);
                return;
            }

            base.Cursor = Cursors.Default;
            base.OnMouseMove(e);
        }

        /// <summary>
        /// 重载鼠标离开事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.Cursor = Cursors.Default;
            ControlState = UIControlState.Normal;
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// 重载鼠标按下事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Rectangle collapseRect = CollapseRect;
            Point mousePoint = e.Location;

            if (collapseRect.Contains(mousePoint) ||
                (_collapsePanel != UICollapsePanel.None &&
                 SplitPanelState == UISplitPanelState.Collapsed))
            {
                _uiMouseType = UIMouseType.Button;
                return;
            }

            if (SplitterRectangle.Contains(mousePoint))
            {
                _uiMouseType = UIMouseType.Split;
            }

            base.OnMouseDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            Invalidate(SplitterRectangle);
        }

        /// <summary>
        /// 重载鼠标抬起事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Invalidate(SplitterRectangle);

            Rectangle collapseRect = CollapseRect;
            Point mousePoint = e.Location;

            if (_uiMouseType == UIMouseType.Button && e.Button == MouseButtons.Left && collapseRect.Contains(mousePoint))
            {
                OnCollapseClick(EventArgs.Empty);
            }

            _uiMouseType = UIMouseType.None;
        }

        private void SetCursor(Cursor cursor)
        {
            if (base.Cursor != cursor)
            {
                base.Cursor = cursor;
            }
        }

        private Rectangle CalcArrowRect(Rectangle collapseRect)
        {
            if (Orientation == Orientation.Horizontal)
            {
                int width = (collapseRect.Width - DefaultArrowWidth) / 2;
                return new Rectangle(
                    collapseRect.X + width,
                    collapseRect.Y - 1,
                    DefaultArrowWidth,
                    collapseRect.Height);
            }
            else
            {
                int width = (collapseRect.Height - DefaultArrowWidth) / 2;
                return new Rectangle(
                    collapseRect.X - 1,
                    collapseRect.Y + width,
                    collapseRect.Width,
                    DefaultArrowWidth);
            }
        }

        public void SetStyleColor(UIBaseStyle uiColor)
        {
            arrowColor = uiColor.SplitContainerArrowColor;
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

        public void SetDPIScale()
        {
            //
        }
    }
}
