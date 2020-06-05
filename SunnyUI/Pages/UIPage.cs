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
 * 文件名称: UIPage.cs
 * 文件说明: 页面基类，从Form继承，可放置于容器内
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("Initialize")]
    public partial class UIPage : Form, IStyleInterface
    {
        public readonly Guid Guid = Guid.NewGuid();
        private Color _rectColor = UIColor.Blue;

        private ToolStripStatusLabelBorderSides _rectSides = ToolStripStatusLabelBorderSides.None;

        protected UIStyle _style = UIStyle.Blue;

        public UIStatusForm StatusForm;

        public UIPage()
        {
            InitializeComponent();

            base.BackColor = UIColor.LightBlue;
            TopLevel = false;
            if (this.Register()) SetStyle(UIStyles.Style);

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            UpdateStyles();

            if (!IsDesignMode) base.Dock = DockStyle.Fill;

            Version = UIGlobal.Version;
        }

        [Browsable(false)] public Point ParentLocation { get; set; } = new Point(0, 0);

        public int PageIndex { get; set; } = -1;

        /// <summary>
        ///     边框颜色
        /// </summary>
        /// <value>The color of the border style.</value>
        [Description("边框颜色")]
        public Color RectColor
        {
            get => _rectColor;
            set
            {
                _rectColor = value;
                AfterSetRectColor(value);
                _style = UIStyle.Custom;
                Invalidate();
            }
        }

        protected bool IsDesignMode
        {
            get
            {
                var ReturnFlag = false;
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                    ReturnFlag = true;
                else if (Process.GetCurrentProcess().ProcessName == "devenv")
                    ReturnFlag = true;

                return ReturnFlag;
            }
        }

        [DefaultValue(ToolStripStatusLabelBorderSides.None)]
        [Description("边框显示位置")]
        public ToolStripStatusLabelBorderSides RectSides
        {
            get => _rectSides;
            set
            {
                _rectSides = value;
                Invalidate();
            }
        }

        [DefaultValue(null)] public string TagString { get; set; }

        public string Version { get; }

        [DefaultValue(UIStyle.Blue)]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        [DefaultValue(false)] public bool StyleCustomMode { get; set; }

        public event EventHandler Initialize;

        public void ShowStatus(string title, string desc, int max = 100, int value = 0)
        {
            if (StatusForm == null) StatusForm = new UIStatusForm();

            StatusForm.Style = Style;
            StatusForm.Show(title, desc, max, value);
        }

        public void HideStatus()
        {
            StatusForm.Hide();
        }

        public void StatusStepIt()
        {
            StatusForm.StepIt();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (e.Control is IStyleInterface ctrl)
                if (!ctrl.StyleCustomMode)
                    ctrl.Style = Style;
        }

        public virtual void Init()
        {
            Initialize?.Invoke(this, null);
        }

        public virtual void Final()
        {
        }

        public void SetStyle(UIStyle style)
        {
            this.SetChildUIStyle(style);

            SetStyleColor(UIStyles.GetStyleColor(style));
            _style = style;
        }

        public virtual void SetStyleColor(UIBaseStyle uiColor)
        {
            if (uiColor.IsCustom()) return;

            BackColor = uiColor.PlainColor;
            RectColor = uiColor.RectColor;
            ForeColor = UIFontColor.Primary;
            Invalidate();
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Width <= 0 || Height <= 0) return;

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
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            AfterSetFillColor(BackColor);
            _style = UIStyle.Custom;
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            AfterSetForeColor(ForeColor);
            _style = UIStyle.Custom;
        }

        private void UIPage_Shown(object sender, EventArgs e)
        {
            SetStyle(UIStyles.Style);
        }
    }
}