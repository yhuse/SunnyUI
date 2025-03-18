/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2025 ShenYongHua(沈永华).
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
 * 文件名称: UIStatusBox.cs
 * 文件说明: 根据状态显示图片控件
 * 当前版本: V3.8.1
 * 创建日期: 2025-01-18
 *
 * 2025-01-18: V3.8.1 增加文件
******************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [Description("根据状态显示图片控件")]
    public class UIStatusBox : PictureBox
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UIStatusBox()
        {
            SetDefaultControlStyles();
            SuspendLayout();
            BorderStyle = BorderStyle.None;
            ResumeLayout(false);
            Width = 36;
            Height = 36;
            Version = UIGlobal.Version;
        }

        private void SetDefaultControlStyles()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            UpdateStyles();
        }

        public string Version { get; }

        [Browsable(false)]
        [DefaultValue(typeof(Image), "null")]
        [Description("初始化图片"), Category("SunnyUI")]
        public new Image InitialImage { get; set; }

        [Browsable(false)]
        [DefaultValue(typeof(Image), "null")]
        [Description("出错图片"), Category("SunnyUI")]
        public new Image ErrorImage { get; set; }

        [DefaultValue(typeof(Image), "null")]
        [Description("图片1"), Category("SunnyUI")]
        public Image Status1 { get; set; }

        [DefaultValue(typeof(Image), "null")]
        [Description("图片2"), Category("SunnyUI")]
        public Image Status2 { get; set; }

        [DefaultValue(typeof(Image), "null")]
        [Description("图片3"), Category("SunnyUI")]
        public Image Status3 { get; set; }

        [DefaultValue(typeof(Image), "null")]
        [Description("图片4"), Category("SunnyUI")]
        public Image Status4 { get; set; }

        [DefaultValue(typeof(Image), "null")]
        [Description("图片5"), Category("SunnyUI")]
        public Image Status5 { get; set; }

        [DefaultValue(typeof(Image), "null")]
        [Description("图片6"), Category("SunnyUI")]
        public Image Status6 { get; set; }

        private int _status;

        [DefaultValue(0)]
        [Description("状态"), Category("SunnyUI")]
        public int Status
        {
            get => _status;
            set
            {
                _status = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 绘制状态图片
        /// </summary>
        /// <param name="pe">pe</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            Image img = Image;

            if (_status == 1 && Status1 != null) img = Status1;
            if (_status == 2 && Status2 != null) img = Status2;
            if (_status == 3 && Status3 != null) img = Status3;
            if (_status == 4 && Status4 != null) img = Status4;
            if (_status == 5 && Status5 != null) img = Status5;
            if (_status == 6 && Status6 != null) img = Status6;

            if (img != null)
            {
                if (SizeMode == PictureBoxSizeMode.Normal)
                    pe.Graphics.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));

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
        }
    }
}
