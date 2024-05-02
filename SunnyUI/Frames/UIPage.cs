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
 * 文件名称: UIPage.cs
 * 文件说明: 页面基类，从Form继承，可放置于容器内
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2021-05-21: V3.0.4 更改了下页面切换重复执行的Init事件调用
 * 2021-06-20: V3.0.4 增加标题行，替代UITitlePage
 * 2021-07-18: V3.0.5 修复OnLoad在加载时重复加载两次的问题，增加Final函数，每次页面切换，退出页面都会执行
 * 2021-08-17: V3.0.6 增加TitleFont属性
 * 2021-08-24: V3.0.6 修复OnLoad在加载时重复加载两次的问题
 * 2021-12-01: V3.0.9 增加FeedBack和SetParam函数，用于多页面传值
 * 2021-12-30: V3.0.9 增加NeedReload，页面切换是否需要重载Load
 * 2022-04-02: V3.1.2 默认设置AutoScaleMode为None
 * 2022-04-26: V3.1.8 屏蔽一些属性
 * 2022-05-11: V3.1.8 ShowTitle时，可调整Padding
 * 2022-06-11: V3.1.9 弹窗默认关闭半透明遮罩
 * 2022-08-25: V3.2.3 重构多页面框架传值删除SetParam，FeedbackToFrame
 * 2022-08-25: V3.2.3 重构多页面框架传值：页面发送给框架 SendParamToFrame 函数
 * 2022-08-25: V3.2.3 重构多页面框架传值：页面发送给框架 SendParamToPage 函数
 * 2022-08-25: V3.2.3 重构多页面框架传值：接收框架、页面传值 ReceiveParams 事件
 * 2022-10-28: V3.2.6 标题栏增加扩展按钮
 * 2023-02-24: V3.3.2 增加PageDeselecting，取消页面选择时增加判断
 * 2023-02-24: V3.3.2 取消设计期的Dock.Fill，改为运行时设置
 * 2023-03-15: V3.3.3 重新梳理页面加载顺序
 * 2023-05-12: V3.3.6 重构DrawString函数
 * 2023-07-27: V3.4.1 默认提示弹窗TopMost为true
 * 2023-10-09: V3.5.0 增加一个在窗体显示后延时执行的事件
 * 2023-10-26: V3.5.1 字体图标增加旋转角度参数SymbolRotate
 * 2023-11-06: V3.5.2 重构主题
 * 2023-12-04: V3.6.1 修复修改Style后，BackColor未保存的问题
 * 2023-12-20: V3.6.2 调整AfterShow事件位置及逻辑
 * 2024-04-28: V3.6.5 增加WindowStateChanged事件
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("Initialize")]
    public partial class UIPage : Form, IStyleInterface, ISymbol, IZoomScale
    {
        public UIPage()
        {
            InitializeComponent();

            TopLevel = false;
            if (this.Register()) SetStyle(UIStyles.Style);

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            UpdateStyles();

            //if (!IsDesignMode) base.Dock = DockStyle.Fill;

            Version = UIGlobal.Version;
            SetDPIScale();

            _rectColor = UIStyles.Blue.PageRectColor;
            ForeColor = UIStyles.Blue.PageForeColor;
            titleFillColor = UIStyles.Blue.PageTitleFillColor;
            titleForeColor = UIStyles.Blue.PageTitleForeColor;
            base.WindowState = FormWindowState.Normal;
            base.TopMost = false;
            base.FormBorderStyle = FormBorderStyle.None;
            base.AutoScroll = false;
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            base.SizeGripStyle = SizeGripStyle.Hide;
        }

        public readonly Guid Guid = Guid.NewGuid();
        private Color _rectColor = UIColor.Blue;

        private ToolStripStatusLabelBorderSides _rectSides = ToolStripStatusLabelBorderSides.None;

        protected UIStyle _style = UIStyle.Inherited;

        [Browsable(false)]
        public IFrame Frame
        {
            get; set;
        }

        private bool extendBox;

        [DefaultValue(false)]
        [Description("显示扩展按钮"), Category("SunnyUI")]
        public bool ExtendBox
        {
            get => extendBox;
            set
            {
                extendBox = showTitle && value;
                CalcSystemBoxPos();
                Invalidate();
            }
        }

        public event OnWindowStateChanged WindowStateChanged;

        internal void DoWindowStateChanged(FormWindowState thisState, FormWindowState lastState)
        {
            WindowStateChanged?.Invoke(this, thisState, lastState);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.HideComboDropDown();
        }

        public event PageDeselectingEventHandler PageDeselecting;

        internal bool OnPageDeselecting()
        {
            PageDeselectingEventArgs e = new PageDeselectingEventArgs(false, string.Empty);
            PageDeselecting?.Invoke(this, e);
            return e.Cancel;
        }

        [Browsable(false)]
        public new IButtonControl AcceptButton
        {
            get => base.AcceptButton;
            set => base.AcceptButton = value;
        }

        [Browsable(false)]
        public new IButtonControl CancelButton
        {
            get => base.CancelButton;
            set => base.CancelButton = value;
        }

        [Browsable(false)]
        public new SizeGripStyle SizeGripStyle
        {
            get => base.SizeGripStyle;
            set => base.SizeGripStyle = SizeGripStyle.Hide;
        }

        [Browsable(false)]
        public new FormStartPosition StartPosition
        {
            get => base.StartPosition;
            set => base.StartPosition = FormStartPosition.Manual;
        }

        [Browsable(false)]
        public new bool AutoScroll
        {
            get => base.AutoScroll;
            set => base.AutoScroll = false;
        }

        [Browsable(false)]
        public new bool ShowIcon
        {
            get => base.ShowIcon;
            set => base.ShowIcon = false;
        }

        [Browsable(false)]
        public new bool ShowInTaskbar
        {
            get => base.ShowInTaskbar;
            set => base.ShowInTaskbar = false;
        }

        [Browsable(false)]
        public new bool IsMdiContainer
        {
            get => base.IsMdiContainer;
            set => base.IsMdiContainer = false;
        }

        //不显示FormBorderStyle属性
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FormBorderStyle FormBorderStyle
        {
            get
            {
                return base.FormBorderStyle;
            }
            set
            {
                if (!Enum.IsDefined(typeof(FormBorderStyle), value))
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(FormBorderStyle));
                base.FormBorderStyle = FormBorderStyle.None;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool TopMost
        {
            get => base.TopMost;
            set => base.TopMost = false;
        }

        /// <summary>
        /// 不显示WindowState属性
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FormWindowState WindowState
        {
            get
            {
                return base.WindowState;
            }
            set
            {
                base.WindowState = FormWindowState.Normal;
            }
        }

        public UIPage SetPageIndex(int pageIndex)
        {
            PageIndex = pageIndex;
            return this;
        }

        public UIPage SetPageGuid(Guid pageGuid)
        {
            PageGuid = pageGuid;
            return this;
        }

        public UIPage SetText(string text)
        {
            Text = text;
            return this;
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
        public virtual void SetZoomScale(float scale)
        {

        }

        private float DefaultFontSize = -1;
        private float TitleFontSize = -1;

        public void SetDPIScale()
        {
            if (DesignMode) return;
            if (!UIDPIScale.NeedSetDPIFont()) return;

            if (DefaultFontSize < 0) DefaultFontSize = this.Font.Size;
            if (TitleFontSize < 0) TitleFontSize = this.TitleFont.Size;

            this.SetDPIScaleFont(DefaultFontSize);
            TitleFont = TitleFont.DPIScaleFont(TitleFontSize);
            foreach (var control in this.GetAllDPIScaleControls())
            {
                control.SetDPIScale();
            }
        }

        public void Render()
        {
            if (!DesignMode && UIStyles.Style.IsValid())
            {
                SetInheritedStyle(UIStyles.Style);
            }
        }

        private int _symbolSize = 24;

        /// <summary>
        /// 字体图标大小
        /// </summary>
        [DefaultValue(24)]
        [Description("字体图标大小"), Category("SunnyUI")]
        public int SymbolSize
        {
            get => _symbolSize;
            set
            {
                _symbolSize = Math.Max(value, 16);
                _symbolSize = Math.Min(value, 128);
                SymbolChange();
                Invalidate();
            }
        }

        private int _symbol;

        /// <summary>
        /// 字体图标
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor("Sunny.UI.UIImagePropertyEditor, " + AssemblyRefEx.SystemDesign, typeof(UITypeEditor))]
        [DefaultValue(0)]
        [Description("字体图标"), Category("SunnyUI")]
        public int Symbol
        {
            get => _symbol;
            set
            {
                _symbol = value;
                SymbolChange();
                Invalidate();
            }
        }

        private Point symbolOffset = new Point(0, 0);

        /// <summary>
        /// 字体图标的偏移位置
        /// </summary>
        [DefaultValue(typeof(Point), "0, 0")]
        [Description("字体图标的偏移位置"), Category("SunnyUI")]
        public Point SymbolOffset
        {
            get => symbolOffset;
            set
            {
                symbolOffset = value;
                Invalidate();
            }
        }

        private int _symbolRotate = 0;

        /// <summary>
        /// 字体图标旋转角度
        /// </summary>
        [DefaultValue(0)]
        [Description("字体图标旋转角度"), Category("SunnyUI")]
        public int SymbolRotate
        {
            get => _symbolRotate;
            set
            {
                if (_symbolRotate != value)
                {
                    _symbolRotate = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(false), Description("在Frame框架中不被关闭"), Category("SunnyUI")]
        public bool AlwaysOpen
        {
            get; set;
        }

        protected virtual void SymbolChange()
        {
        }

        [DefaultValue(-1)]
        public int PageIndex { get; set; } = -1;

        [Browsable(false)]
        public Guid PageGuid { get; set; } = Guid.Empty;

        [Browsable(false), DefaultValue(null)]
        public TabPage TabPage { get; set; } = null;

        /// <summary>
        ///     边框颜色
        /// </summary>
        /// <value>The color of the border style.</value>
        [Description("边框颜色"), Category("SunnyUI")]
        public Color RectColor
        {
            get => _rectColor;
            set
            {
                _rectColor = value;
                AfterSetRectColor(value);
                Invalidate();
            }
        }

        [DefaultValue(ToolStripStatusLabelBorderSides.None)]
        [Description("边框显示位置"), Category("SunnyUI")]
        public ToolStripStatusLabelBorderSides RectSides
        {
            get => _rectSides;
            set
            {
                _rectSides = value;
                Invalidate();
            }
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

        public string Version
        {
            get;
        }

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

        public event EventHandler Initialize;

        public event EventHandler Finalize;

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (AllowShowTitle && !AllowAddControlOnTitle && e.Control.Top < TitleHeight)
            {
                e.Control.Top = Padding.Top;
            }
        }

        [DefaultValue(false)]
        [Description("允许在标题栏放置控件"), Category("SunnyUI")]
        public bool AllowAddControlOnTitle
        {
            get; set;
        }

        public virtual void Init()
        {
            Initialize?.Invoke(this, new EventArgs());
            if (AfterShown != null)
            {
                AfterShownTimer = new System.Windows.Forms.Timer();
                AfterShownTimer.Tick += AfterShownTimer_Tick;
                AfterShownTimer.Start();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Init();
        }

        [Description("背景颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "Control")]
        public override Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        private bool IsShown;
        private System.Windows.Forms.Timer AfterShownTimer;
        public event EventHandler AfterShown;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (AutoScaleMode == AutoScaleMode.Font) AutoScaleMode = AutoScaleMode.None;
            if (base.BackColor == SystemColors.Control) base.BackColor = UIStyles.Blue.PageBackColor;

            IsShown = true;
        }

        private void AfterShownTimer_Tick(object sender, EventArgs e)
        {
            AfterShownTimer.Stop();
            AfterShownTimer.Tick -= AfterShownTimer_Tick;
            AfterShownTimer?.Dispose();
            AfterShownTimer = null;

            AfterShown?.Invoke(this, EventArgs.Empty);
        }

        internal void ReLoad()
        {
            if (IsShown)
            {
                if (NeedReload)
                    OnLoad(EventArgs.Empty);
                else
                    Init();
            }
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("页面切换是否需要重载Load"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool NeedReload { get; set; }

        public virtual void Final()
        {
            Finalize?.Invoke(this, new EventArgs());
        }

        public virtual void SetInheritedStyle(UIStyle style)
        {
            if (!DesignMode)
            {
                this.SuspendLayout();
                UIStyleHelper.SetChildUIStyle(this, style);

                if (_style == UIStyle.Inherited && style.IsValid())
                {
                    SetStyleColor(style.Colors());
                    Invalidate();
                    _style = UIStyle.Inherited;
                }

                UIStyleChanged?.Invoke(this, new EventArgs());
                this.ResumeLayout();
            }
        }

        protected virtual void SetStyle(UIStyle style)
        {
            this.SuspendLayout();

            if (!style.IsCustom())
            {
                SetStyleColor(style.Colors());
                Invalidate();
            }

            _style = style == UIStyle.Inherited ? UIStyle.Inherited : UIStyle.Custom;
            UIStyleChanged?.Invoke(this, new EventArgs());
            this.ResumeLayout();
        }

        public event EventHandler UIStyleChanged;

        public virtual void SetStyleColor(UIBaseStyle uiColor)
        {
            controlBoxForeColor = uiColor.FormControlBoxForeColor;
            controlBoxFillHoverColor = uiColor.FormControlBoxFillHoverColor;
            ControlBoxCloseFillHoverColor = uiColor.FormControlBoxCloseFillHoverColor;
            BackColor = uiColor.PageBackColor;
            _rectColor = uiColor.PageRectColor;
            ForeColor = uiColor.PageForeColor;
            titleFillColor = uiColor.PageTitleFillColor;
            titleForeColor = uiColor.PageTitleForeColor;
        }

        private Color controlBoxCloseFillHoverColor;
        /// <summary>
        /// 标题栏颜色
        /// </summary>
        [Description("标题栏关闭按钮移上背景颜色"), Category("SunnyUI"), DefaultValue(typeof(Color), "Red")]
        public Color ControlBoxCloseFillHoverColor
        {
            get => controlBoxCloseFillHoverColor;
            set
            {
                if (controlBoxCloseFillHoverColor != value)
                {
                    controlBoxCloseFillHoverColor = value;
                    Invalidate();
                }
            }
        }

        protected virtual void AfterSetFillColor(Color color)
        {
        }

        protected virtual void AfterSetRectColor(Color color)
        {
        }

        protected virtual void AfterSetForeColor(Color color)
        {
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Width <= 0 || Height <= 0) return;

            if (AllowShowTitle)
            {
                e.Graphics.FillRectangle(TitleFillColor, 0, 0, Width, TitleHeight);
            }

            if (RectSides != ToolStripStatusLabelBorderSides.None)
            {
                if (RectSides.GetValue(ToolStripStatusLabelBorderSides.Left))
                    e.Graphics.DrawLine(RectColor, 0, 0, 0, Height - 1);
                if (RectSides.GetValue(ToolStripStatusLabelBorderSides.Top))
                    e.Graphics.DrawLine(RectColor, 0, 0, Width - 1, 0);
                if (RectSides.GetValue(ToolStripStatusLabelBorderSides.Right))
                    e.Graphics.DrawLine(RectColor, Width - 1, 0, Width - 1, Height - 1);
                if (RectSides.GetValue(ToolStripStatusLabelBorderSides.Bottom))
                    e.Graphics.DrawLine(RectColor, 0, Height - 1, Width - 1, Height - 1);
            }

            if (!AllowShowTitle) return;
            if (Symbol > 0)
            {
                e.Graphics.DrawFontImage(Symbol, SymbolSize, TitleForeColor, new Rectangle(ImageInterval, 0, SymbolSize, TitleHeight), SymbolOffset.X, SymbolOffset.Y, SymbolRotate);
            }

            e.Graphics.DrawString(Text, TitleFont, TitleForeColor, new Rectangle(Symbol > 0 ? ImageInterval * 2 + SymbolSize : ImageInterval, 0, Width, TitleHeight), ContentAlignment.MiddleLeft);

            e.Graphics.SetHighQuality();
            if (ControlBox)
            {
                if (InControlBox)
                {
                    e.Graphics.FillRectangle(ControlBoxCloseFillHoverColor, ControlBoxRect);
                }

                e.Graphics.DrawLine(controlBoxForeColor,
                    ControlBoxRect.Left + ControlBoxRect.Width / 2 - 5,
                    ControlBoxRect.Top + ControlBoxRect.Height / 2 - 5,
                    ControlBoxRect.Left + ControlBoxRect.Width / 2 + 5,
                    ControlBoxRect.Top + ControlBoxRect.Height / 2 + 5);
                e.Graphics.DrawLine(controlBoxForeColor,
                    ControlBoxRect.Left + ControlBoxRect.Width / 2 - 5,
                    ControlBoxRect.Top + ControlBoxRect.Height / 2 + 5,
                    ControlBoxRect.Left + ControlBoxRect.Width / 2 + 5,
                    ControlBoxRect.Top + ControlBoxRect.Height / 2 - 5);
            }

            if (ExtendBox)
            {
                if (InExtendBox)
                {
                    e.Graphics.FillRectangle(ControlBoxFillHoverColor, ExtendBoxRect);
                }

                if (ExtendSymbol == 0)
                {
                    e.Graphics.DrawLine(controlBoxForeColor,
                        ExtendBoxRect.Left + ExtendBoxRect.Width / 2 - 5 - 1,
                        ExtendBoxRect.Top + ExtendBoxRect.Height / 2 - 2,
                        ExtendBoxRect.Left + ExtendBoxRect.Width / 2 - 1,
                        ExtendBoxRect.Top + ExtendBoxRect.Height / 2 + 3);

                    e.Graphics.DrawLine(controlBoxForeColor,
                        ExtendBoxRect.Left + ExtendBoxRect.Width / 2 + 5 - 1,
                        ExtendBoxRect.Top + ExtendBoxRect.Height / 2 - 2,
                        ExtendBoxRect.Left + ExtendBoxRect.Width / 2 - 1,
                        ExtendBoxRect.Top + ExtendBoxRect.Height / 2 + 3);
                }
                else
                {
                    e.Graphics.DrawFontImage(extendSymbol, ExtendSymbolSize, controlBoxForeColor, ExtendBoxRect, ExtendSymbolOffset.X, ExtendSymbolOffset.Y);
                }
            }
        }

        private Color controlBoxForeColor = Color.White;
        /// <summary>
        /// 标题栏颜色
        /// </summary>
        [Description("标题栏按钮颜色"), Category("SunnyUI"), DefaultValue(typeof(Color), "White")]
        public Color ControlBoxForeColor
        {
            get => controlBoxForeColor;
            set
            {
                if (controlBoxForeColor != value)
                {
                    controlBoxForeColor = value;
                    Invalidate();
                }
            }
        }

        private Color controlBoxFillHoverColor;
        /// <summary>
        /// 标题栏颜色
        /// </summary>
        [Description("标题栏按钮移上背景颜色"), Category("SunnyUI"), DefaultValue(typeof(Color), "115, 179, 255")]
        public Color ControlBoxFillHoverColor
        {
            get => controlBoxFillHoverColor;
            set
            {
                if (ControlBoxFillHoverColor != value)
                {
                    controlBoxFillHoverColor = value;
                    Invalidate();
                }
            }
        }

        private Point extendSymbolOffset = new Point(0, 0);

        [DefaultValue(typeof(Point), "0, 0")]
        [Description("扩展按钮字体图标偏移量"), Category("SunnyUI")]
        public Point ExtendSymbolOffset
        {
            get => extendSymbolOffset;
            set
            {
                extendSymbolOffset = value;
                Invalidate();
            }
        }

        private int _extendSymbolSize = 24;

        [DefaultValue(24)]
        [Description("扩展按钮字体图标大小"), Category("SunnyUI")]
        public int ExtendSymbolSize
        {
            get => _extendSymbolSize;
            set
            {
                _extendSymbolSize = Math.Max(value, 16);
                _extendSymbolSize = Math.Min(value, 128);
                Invalidate();
            }
        }

        private int extendSymbol;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor("Sunny.UI.UIImagePropertyEditor, " + AssemblyRefEx.SystemDesign, typeof(UITypeEditor))]
        [DefaultValue(0)]
        [Description("扩展按钮字体图标"), Category("SunnyUI")]
        public int ExtendSymbol
        {
            get => extendSymbol;
            set
            {
                extendSymbol = value;
                Invalidate();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (FormBorderStyle == FormBorderStyle.None && ShowTitle)
            {
                if (InControlBox)
                {
                    InControlBox = false;
                    Close();
                    AfterClose();
                }

                if (InExtendBox)
                {
                    InExtendBox = false;
                    if (ExtendMenu != null)
                    {
                        this.ShowContextMenuStrip(ExtendMenu, ExtendBoxRect.Left, TitleHeight - 1);
                    }
                    else
                    {
                        ExtendBoxClick?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        [DefaultValue(null)]
        [Description("扩展按钮菜单"), Category("SunnyUI")]
        public UIContextMenuStrip ExtendMenu
        {
            get; set;
        }

        public event EventHandler ExtendBoxClick;

        private void AfterClose()
        {
            Console.WriteLine("Close");
        }

        private Color titleFillColor = Color.FromArgb(76, 76, 76);

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("标题颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "76, 76, 76")]
        public Color TitleFillColor
        {
            get => titleFillColor;
            set
            {
                titleFillColor = value;
                Invalidate();
            }
        }

        private Color titleForeColor = Color.White;

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public Color TitleForeColor
        {
            get => titleForeColor;
            set
            {
                titleForeColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 标题字体
        /// </summary>
        private Font titleFont = UIStyles.Font();

        /// <summary>
        /// 标题字体
        /// </summary>
        [Description("标题字体"), Category("SunnyUI")]
        [DefaultValue(typeof(Font), "宋体, 12pt")]
        public Font TitleFont
        {
            get => titleFont;
            set
            {
                titleFont = value;
                Invalidate();
            }
        }

        private int imageInterval = 6;

        public int ImageInterval
        {
            get => imageInterval;
            set
            {
                imageInterval = Math.Max(2, value);
                Invalidate();
            }
        }

        private int titleHeight = 35;

        [Description("面板高度"), Category("SunnyUI")]
        [DefaultValue(35)]
        public int TitleHeight
        {
            get => titleHeight;
            set
            {
                titleHeight = Math.Max(value, 19);
                Padding = new Padding(Padding.Left, ShowTitle ? Math.Max(titleHeight, Padding.Top) : 0, Padding.Right, Padding.Bottom);
                CalcSystemBoxPos();
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
            CalcSystemBoxPos();
        }

        private bool InControlBox;
        private bool InExtendBox;

        /// <summary>
        /// 重载鼠标移动事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (ShowTitle)
            {
                if (ControlBox)
                {
                    bool inControlBox = e.Location.InRect(ControlBoxRect);
                    if (inControlBox != InControlBox)
                    {
                        InControlBox = inControlBox;
                        Invalidate();
                    }
                }

                if (ExtendBox)
                {
                    bool inExtendBox = e.Location.InRect(ExtendBoxRect);
                    if (inExtendBox != InExtendBox)
                    {
                        InExtendBox = inExtendBox;
                        Invalidate();
                    }
                }
            }
            else
            {
                InControlBox = InExtendBox = false;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            InExtendBox = InControlBox = false;
            Invalidate();
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);

            if (AllowShowTitle)
            {
                Padding = new Padding(Padding.Left, Math.Max(titleHeight, Padding.Top), Padding.Right, Padding.Bottom);
            }
        }

        [Description("允许显示标题栏"), Category("SunnyUI"), DefaultValue(false)]
        public bool AllowShowTitle
        {
            get => ShowTitle;
            set => ShowTitle = value;
        }

        /// <summary>
        /// 是否显示窗体的标题栏
        /// </summary>
        private bool showTitle;

        /// <summary>
        /// 是否显示窗体的标题栏
        /// </summary>
        [Description("是否显示窗体的标题栏"), Category("WindowStyle"), DefaultValue(false)]
        public bool ShowTitle
        {
            get => showTitle;
            set
            {
                showTitle = value;
                Padding = new Padding(Padding.Left, value ? Math.Max(titleHeight, Padding.Top) : 0, Padding.Right, Padding.Bottom);
                Invalidate();
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        private Rectangle ExtendBoxRect;

        private void CalcSystemBoxPos()
        {
            if (ControlBox)
            {
                ControlBoxRect = new Rectangle(Width - 6 - 28, titleHeight / 2 - 14, 28, 28);
            }
            else
            {
                ControlBoxRect = new Rectangle(Width + 1, Height + 1, 1, 1);
            }

            if (ExtendBox)
            {
                if (ControlBox)
                {
                    ExtendBoxRect = new Rectangle(ControlBoxRect.Left - 28 - 2, ControlBoxRect.Top, 28, 28);
                }
                else
                {
                    ExtendBoxRect = new Rectangle(Width - 6 - 28, titleHeight / 2 - 14, 28, 28);
                }
            }
            else
            {
                ExtendBoxRect = new Rectangle(Width + 1, Height + 1, 1, 1);
            }
        }

        private Rectangle ControlBoxRect;

        /// <summary>
        /// 是否显示窗体的控制按钮
        /// </summary>
        private bool controlBox;

        /// <summary>
        /// 是否显示窗体的控制按钮
        /// </summary>
        [Description("是否显示窗体的控制按钮"), Category("WindowStyle"), DefaultValue(false)]
        public new bool ControlBox
        {
            get => controlBox;
            set
            {
                controlBox = value;
                CalcSystemBoxPos();
                Invalidate();
            }
        }

        [Browsable(false)]
        public new bool MinimizeBox
        {
            get; set;
        }

        [Browsable(false)]
        public new bool MaximizeBox
        {
            get; set;
        }

        internal event OnReceiveParams OnFrameDealPageParams;

        public bool SendParamToFrame(object value)
        {
            var args = new UIPageParamsArgs(this, null, value);
            OnFrameDealPageParams?.Invoke(this, args);
            return args.Handled;
        }

        public bool SendParamToPage(int pageIndex, object value)
        {
            UIPage page = Frame.GetPage(pageIndex);
            if (page == null)
            {
                throw new NullReferenceException("未能查找到页面的索引为: " + pageIndex);
            }

            var args = new UIPageParamsArgs(this, page, value);
            OnFrameDealPageParams?.Invoke(this, args);
            return args.Handled;
        }

        public bool SendParamToPage(Guid pageGuid, object value)
        {
            UIPage page = Frame.GetPage(pageGuid);
            if (page == null)
            {
                throw new NullReferenceException("未能查找到页面的索引为: " + pageGuid);
            }

            var args = new UIPageParamsArgs(this, page, value);
            OnFrameDealPageParams?.Invoke(this, args);
            return args.Handled;
        }

        internal void DealReceiveParams(UIPageParamsArgs e)
        {
            ReceiveParams?.Invoke(this, e);
        }

        public event OnReceiveParams ReceiveParams;
    }
}