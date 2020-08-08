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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
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

        private UIStatusForm statusForm;

        public UIStatusForm StatusForm => statusForm ?? (statusForm = new UIStatusForm());

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

        public void Render()
        {
            SetStyle(UIStyles.Style);
        }

        private int _symbolSize = 24;

        [DefaultValue(24)]
        public int SymbolSize
        {
            get => _symbolSize;
            set
            {
                _symbolSize = Math.Max(value, 16);
                _symbolSize = Math.Min(value, 64);
                SymbolChange();
                Invalidate();
            }
        }

        private int _symbol;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor(typeof(UIImagePropertyEditor), typeof(UITypeEditor))]
        [DefaultValue(0)]
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

        [DefaultValue(false), Description("在Frame框架中不被关闭")]
        public bool AlwaysOpen { get; set; }

        protected virtual void SymbolChange()
        {

        }

        [Browsable(false)] public Point ParentLocation { get; set; } = new Point(0, 0);

        [Browsable(false)]
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

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

        public string Version { get; }

        /// <summary>
        /// 主题样式
        /// </summary>
        [DefaultValue(UIStyle.Blue), Description("主题样式"), Category("SunnyUI")]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        public event EventHandler Initialize;

        public void ShowStatus(string title, string desc, int max = 100, int value = 0)
        {
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

        [DefaultValue(null)]
        [Browsable(false)]
        public string StatusDescription
        {
            get => StatusForm?.Description;
            set => StatusForm.Description = value;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (e.Control is IStyleInterface ctrl)
            {
                if (!ctrl.StyleCustomMode)
                {
                    ctrl.Style = Style;
                }
            }

            if (e.Control is Panel)
            {
                List<Control> controls = e.Control.GetUIStyleControls("IStyleInterface");
                foreach (var control in controls)
                {
                    if (control is IStyleInterface item)
                    {
                        if (!item.StyleCustomMode)
                        {
                            item.Style = Style;
                        }
                    }
                }
            }
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
            //SetStyle(UIStyles.Style);
        }
    }
}