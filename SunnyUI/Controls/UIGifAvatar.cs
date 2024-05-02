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
 * 文件名称: UIGifAvatar.cs
 * 文件说明: Gif动态头像
 * 当前版本: V3.1
 * 创建日期: 2022-07-01
 *
 * 2022-07-01: V3.2.0 增加文件说明
 * 2022-07-25: V3.2.2 重写图片刷新流程，减少内存及GC
 * 2023-11-12: V3.5.2 重构主题
******************************************************************************/

using System;
using System.Collections.Concurrent;
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
            Clear();
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color RectColor
        {
            get => rectColor;
            set => SetRectColor(value);
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            fillColor = uiColor.PanelFillColor;
            rectColor = uiColor.PanelFillColor;
            CalcImages();
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

                    CalcImages();
                    Invalidate();
                }

                Active = atv;
            }
        }

        private void Clear()
        {
            foreach (var item in Images.Values)
            {
                item.Dispose();
            }
        }

        private void CalcImages()
        {
            FrameIndex = -1;
            Clear();

            if (Image == null) return;

            Images.Clear();
            if (!IsGif)
            {
                Images.TryAdd(0, ScaleImage(0));
            }
            else
            {
                for (int i = 0; i < ImageCount; i++)
                {
                    Images.TryAdd(i, ScaleImage(i));
                }
            }

            FrameIndex = 0;
        }

        private ConcurrentDictionary<int, Image> Images = new ConcurrentDictionary<int, Image>();

        private int avatarSize = 120;

        [DefaultValue(120), Description("头像大小"), Category("SunnyUI")]
        public int AvatarSize
        {
            get => avatarSize;
            set
            {
                avatarSize = value;
                CalcImages();
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
            CalcImages();
            Invalidate();
        }

        private Image ScaleImage(int frameIndex)
        {
            if (Image == null)
            {
                return null;
            }

            var img = new Bitmap(Width, Height);
            using Graphics g = img.Graphics();
            g.Clear(FillColor);

            float size = avatarSize;
            float sc1 = Image.Width * 1.0f / size;
            float sc2 = Image.Height * 1.0f / size;
            size = Math.Min(sc1, sc2);

            Bitmap scaleImage;
            Bitmap result;
            if (!IsGif)
            {
                scaleImage = ((Bitmap)Image).ResizeImage((int)(Image.Width * 1.0 / size + 0.5), (int)(Image.Height * 1.0 / size + 0.5));
                result = scaleImage.Split(avatarSize, UIShape.Circle);
            }
            else
            {
                FrameDimension fd = new FrameDimension(Image.FrameDimensionsList[0]);
                Image.SelectActiveFrame(fd, frameIndex);
                using Bitmap imageEx = new Bitmap(Image.Width, Image.Height);
                Graphics gx = Graphics.FromImage(imageEx);
                gx.DrawImage(Image, new Point(0, 0));
                scaleImage = imageEx.ResizeImage((int)(Image.Width * 1.0 / size + 0.5), (int)(Image.Height * 1.0 / size + 0.5));
                result = scaleImage.Split(avatarSize, UIShape.Circle);
            }

            int drawSize = Math.Min(Width, Height);
            g.DrawImage(result, (drawSize - avatarSize) / 2, (drawSize - avatarSize) / 2);
            g.DrawEllipse(rectColor, new Rectangle((drawSize - AvatarSize) / 2, (drawSize - AvatarSize) / 2, AvatarSize, AvatarSize), true, RectSize);
            scaleImage.Dispose();
            result.Dispose();

            return img;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Image == null || FrameIndex < 0 || FrameIndex >= Images.Count)
            {
                int drawSize = Math.Min(Width, Height);
                e.Graphics.FillEllipse(Color.Silver, new Rectangle((drawSize - AvatarSize) / 2, (drawSize - AvatarSize) / 2,
                    AvatarSize, AvatarSize), true);
            }
            else
            {
                e.Graphics.DrawImage(Images[FrameIndex], 0, 0);
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

                Invalidate();
            }

            timer1.Enabled = active;
        }
    }
}
