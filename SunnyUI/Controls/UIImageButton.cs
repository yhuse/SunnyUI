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
 * 文件名称: UIImageButton.cs
 * 文件说明: 图像按钮
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2022-03-19: V3.1.1 重构主题配色
 * 2023-05-04: V3.3.6 增加调用点击事件PerformClick
 * 2023-05-13: V3.3.6 重构DrawString函数
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 图像按钮
    /// </summary>
    public sealed class UIImageButton : PictureBox, IStyleInterface, IZoomScale
    {
        private bool IsPress;
        private bool IsHover;

        private Image imageDisabled;
        private Image imagePress;
        private Image imageHover;
        private Image imageSelected;
        private bool selected;
        private string text;
        private ContentAlignment textAlign = ContentAlignment.MiddleCenter;
        private Color foreColor = UIFontColor.Primary;

        private bool isClick;

        /// <summary>
        /// 调用点击事件
        /// </summary>
        public void PerformClick()
        {
            if (isClick) return;
            if (Enabled)
            {
                isClick = true;
                OnClick(EventArgs.Empty);
                isClick = false;
            }
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
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

        public void SetStyleColor(UIBaseStyle uiColor)
        {
            foreColor = uiColor.ImageButtonForeColor;
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

        private UIStyle _style = UIStyle.Inherited;
        private float DefaultFontSize = -1;

        public void SetDPIScale()
        {
            if (!UIDPIScale.NeedSetDPIFont()) return;
            if (DefaultFontSize < 0) DefaultFontSize = this.Font.Size;
            this.SetDPIScaleFont(DefaultFontSize);
        }

        [Category("SunnyUI")]
        [Description("按钮文字")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => text;
            set
            {
                if (text != value)
                {
                    text = value;
                    Invalidate();
                }
            }
        }

        [Description("文字对齐方式"), Category("SunnyUI")]
        [DefaultValue(ContentAlignment.MiddleCenter)]
        public ContentAlignment TextAlign
        {
            get => textAlign;
            set
            {
                textAlign = value;
                Invalidate();
            }
        }

        [Category("SunnyUI")]
        [Description("文字字体")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                Invalidate();
            }
        }

        [Category("SunnyUI")]
        [Description("文字颜色")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(typeof(Color), "48, 48, 48")]
        public override Color ForeColor
        {
            get => foreColor;
            set
            {
                foreColor = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        [DefaultValue(typeof(Image), "null")]
        [Description("初始化图片"), Category("SunnyUI")]
        public new Image InitialImage { get; set; }

        [Browsable(false)]
        [DefaultValue(typeof(Image), "null")]
        [Description("出错图片"), Category("SunnyUI")]
        public new Image ErrorImage { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public UIImageButton()
        {
            SetDefaultControlStyles();
            SuspendLayout();
            BorderStyle = BorderStyle.None;
            ResumeLayout(false);
            Width = 100;
            Height = 35;
            Version = UIGlobal.Version;
            Cursor = Cursors.Hand;
            base.Font = UIStyles.Font();
        }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false), Browsable(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        public string Version { get; }

        /// <summary>
        /// 鼠标移上图片
        /// </summary>
        [DefaultValue(typeof(Image), "null")]
        [Description("鼠标移上图片"), Category("SunnyUI")]
        public Image ImageHover
        {
            get => imageHover;

            set
            {
                imageHover = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 鼠标按下图片
        /// </summary>
        [DefaultValue(typeof(Image), "null")]
        [Description("鼠标按下图片"), Category("SunnyUI")]
        public Image ImagePress
        {
            get => imagePress;

            set
            {
                imagePress = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 不可用时图片
        /// </summary>
        [DefaultValue(typeof(Image), "null")]
        [Description("不可用时图片"), Category("SunnyUI")]
        public Image ImageDisabled
        {
            get => imageDisabled;
            set
            {
                imageDisabled = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 选中时图片
        /// </summary>
        [DefaultValue(typeof(Image), "null")]
        [Description("选中时图片"), Category("SunnyUI")]
        public Image ImageSelected
        {
            get => imageSelected;
            set
            {
                imageSelected = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        [DefaultValue(typeof(bool), "false")]
        [Description("是否选中"), Category("SunnyUI")]
        public bool Selected
        {
            get => selected;

            set
            {
                if (selected != value)
                {
                    selected = value;
                    Invalidate();
                }
            }
        }

        private void SetDefaultControlStyles()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            UpdateStyles();
        }

        /// <summary>
        /// 重载鼠标按下事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IsPress = true;
            Invalidate();
        }

        /// <summary>
        /// 重载鼠标抬起事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            IsPress = false;
            Invalidate();
        }

        /// <summary>
        /// 重载鼠标进入事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            if (!DesignMode)
            {
                Cursor = Cursors.Hand;
            }

            IsHover = true;
            Invalidate();
        }

        /// <summary>
        /// 重载鼠标离开事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            IsHover = false;
            IsPress = false;
            Invalidate();
        }

        private Point imageOffset;

        [DefaultValue(typeof(Point), "0, 0")]
        [Description("图片偏移位置"), Category("SunnyUI")]
        public Point ImageOffset
        {
            get => imageOffset;
            set
            {
                imageOffset = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 绘制按钮
        /// </summary>
        /// <param name="pe">pe</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            Image img = Image;

            if (!Enabled)
            {
                img = imageDisabled;
            }
            else
            {
                if (IsPress)
                {
                    img = imagePress;
                }
                else if (IsHover)
                {
                    img = imageHover;
                }

                if (Selected)
                {
                    img = imageSelected;
                }
            }

            if (img == null)
            {
                img = Image;
            }

            if (img != null)
            {
                if (SizeMode == PictureBoxSizeMode.Normal)
                    pe.Graphics.DrawImage(img, new Rectangle(ImageOffset.X, ImageOffset.Y, img.Width, img.Height));

                if (SizeMode == PictureBoxSizeMode.StretchImage)
                    pe.Graphics.DrawImage(img, new Rectangle(0, 0, Width, Height));

                if (SizeMode == PictureBoxSizeMode.AutoSize)
                {
                    Width = img.Width;
                    Height = img.Height;
                    pe.Graphics.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));
                }

                if (SizeMode == PictureBoxSizeMode.Zoom)
                    pe.Graphics.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));

                if (SizeMode == PictureBoxSizeMode.CenterImage)
                    pe.Graphics.DrawImage(img, new Rectangle((Width - img.Width) / 2, (Height - img.Height) / 2, img.Width, img.Height));
            }
            else
            {
                base.OnPaint(pe);
            }

            pe.Graphics.DrawString(text, Font, ForeColor, new Rectangle(Padding.Left, Padding.Top, Width - Padding.Left - Padding.Right, Height - Padding.Top - Padding.Bottom), TextAlign);
        }
    }
}