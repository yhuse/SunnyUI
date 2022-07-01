/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2022 ShenYongHua(沈永华).
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
 * 文件名称: UIGifAvatar.cs
 * 文件说明: Gif动态头像
 * 当前版本: V3.1
 * 创建日期: 2022-07-01
 *
 * 2022-07-01: V3.2.0 增加文件说明
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public class UIGifAvatar : UIControl, IZoomScale
    {
        private Image image;
        private System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        public UIGifAvatar()
        {
            SetStyleFlags(false, false, false);
            timer1.Tick += timer1_Tick;
            fillColor = UIStyles.Blue.PanelFillColor;
            rectColor = UIStyles.Blue.PanelFillColor;
            Width = Height = 128;
        }

        ~UIGifAvatar()
        {
            Active = false;
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色，当值为背景色或透明色或空值则不填充"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "243, 249, 255")]
        public Color FillColor
        {
            get
            {
                return fillColor;
            }
            set
            {
                if (fillColor != value)
                {
                    fillColor = value;
                    SetStyleCustom();
                }
            }
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "243, 249, 255")]
        public Color RectColor
        {
            get
            {
                return rectColor;
            }
            set
            {
                if (rectColor != value)
                {
                    rectColor = value;
                    SetStyleCustom();
                }
            }
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            fillColor = uiColor.PanelFillColor;
            rectColor = uiColor.PanelFillColor;
        }

        [DefaultValue(null)]
        public Image Image
        {
            get => image;
            set
            {
                image = value;
                bool atv = Active;
                Active = false;
                if (atv)
                {
                    while (timer1.Enabled)
                        Thread.Sleep(timer1.Interval);
                }

                if (Image != null)
                {
                    ImageCount = GifHelper.GifFrameCount(Image);
                    if (IsGif)
                    {
                        int delay = GifHelper.GifFrameInterval(image);
                        if (delay > 0)
                        {
                            timer1.Interval = delay;
                        }
                    }

                    ShowImage();
                    Invalidate();
                }

                Active = atv;
            }
        }

        private int avatarSize = 120;

        [DefaultValue(120), Description("头像大小"), Category("SunnyUI")]
        public int AvatarSize
        {
            get => avatarSize;
            set
            {
                avatarSize = value;
                ShowImage();
                Invalidate();
            }
        }


        private int ImageCount;
        public bool IsGif => ImageCount > 0;

        private bool active;

        [DefaultValue(false)]
        public bool Active
        {
            get => active;
            set
            {
                active = value;
                if (active)
                {
                    timer1.Start();
                }
                else
                {
                    FrameIndex = 0;
                    timer1.Stop();
                }
            }
        }

        private int FrameIndex;

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ShowImage();
            Invalidate();
        }

        private Image RoundImage;

        public void ShowImage()
        {
            int size = avatarSize;
            if (Image == null)
            {
                return;
            }

            if (RoundImage != null)
            {
                if (RoundImage.Width != Width || RoundImage.Height != Height)
                {
                    RoundImage.Dispose();
                    RoundImage = null;
                }
            }

            if (RoundImage == null)
            {
                RoundImage = new Bitmap(Width, Height);
            }

            Graphics g = Graphics.FromImage(RoundImage);
            g.Clear(FillColor);

            float sc1 = Image.Width * 1.0f / size;
            float sc2 = Image.Height * 1.0f / size;
            int drawSize = Math.Min(Width, Height);
            Image bmp = ScaleImage(Math.Min(sc1, sc2));
            if (bmp != null)
            {
                g.DrawImage(bmp, (drawSize - avatarSize) / 2, (drawSize - avatarSize) / 2);
                bmp.Dispose();
            }

            g.DrawEllipse(rectColor, new Rectangle((drawSize - AvatarSize) / 2, (drawSize - AvatarSize) / 2,
                AvatarSize, AvatarSize), true, RectSize);
        }

        public bool ShowScore { get; set; } = true;

        private Image ScaleImage(float size)
        {
            if (Image == null)
            {
                return null;
            }

            if (!IsGif)
            {
                Bitmap scaleImage = ((Bitmap)Image).ResizeImage((int)(Image.Width * 1.0 / size + 0.5),
               (int)(Image.Height * 1.0 / size + 0.5));
                Bitmap result = scaleImage.Split(avatarSize, UIShape.Circle);
                scaleImage.Dispose();
                return result;
            }
            else
            {
                FrameDimension fd = new FrameDimension(Image.FrameDimensionsList[0]);
                Image.SelectActiveFrame(fd, FrameIndex);
                Bitmap imageEx = new Bitmap(Image.Width, Image.Height);
                Graphics g = Graphics.FromImage(imageEx);
                g.DrawImage(Image, new Point(0, 0));
                Bitmap scaleImage = imageEx.ResizeImage((int)(Image.Width * 1.0 / size + 0.5), (int)(Image.Height * 1.0 / size + 0.5));
                Bitmap result = scaleImage.Split(avatarSize, UIShape.Circle);
                scaleImage.Dispose();
                imageEx.Dispose();
                return result;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (RoundImage != null)
            {
                e.Graphics.DrawImage(RoundImage, 0, 0);
            }
            else
            {
                int drawSize = Math.Min(Width, Height);
                e.Graphics.FillEllipse(Color.Silver, new Rectangle((drawSize - AvatarSize) / 2, (drawSize - AvatarSize) / 2,
                    AvatarSize, AvatarSize), true);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            if (IsGif)
            {
                if (FrameIndex < ImageCount - 1)
                {
                    FrameIndex++;
                }
                else
                {
                    FrameIndex = 0;
                }

                ShowImage();
                Invalidate();
            }

            timer1.Enabled = active;
        }
    }
}
