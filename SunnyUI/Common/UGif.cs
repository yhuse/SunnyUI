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
 * 文件名称: UGif.cs
 * 文件说明: GIF图片帮助类，可控制单帧显示图片
 * 当前版本: V3.1
 * 创建日期: 2021-07-17
 *
 * 2021-07-17: V3.0.5 增加文件说明
******************************************************************************/

/*  用法
        private void button1_Click(object sender, System.EventArgs e)
        {
            Gif gif = new Gif("C:\\aa.gif");
            //gif.Loop = true;//是否循环播放
            gif.ImageChanged += Gif_ImageChanged;
            gif.Active = gif.IsGif;//打开播放
        }

        private void Gif_ImageChanged(object sender, System.Drawing.Image image)
        {
            pictureBox1.Image = image;
        } 
*/

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class Gif : IDisposable
    {
        public Gif(string fileName)
        {
            timer = new Timer();
            timer.Tick += Timer_Tick;

            if (FileEx.Exists(fileName))
            {
                Image = Image.FromFile(fileName);
            }
        }

        public Gif(Image img)
        {
            timer = new Timer();
            timer.Tick += Timer_Tick;

            if (img != null)
            {
                Image = img;
            }
        }

        public void Dispose()
        {
            Active = false;
            timer?.Stop();
            timer?.Dispose();
            Image?.Dispose();
            Image = null;
            ShowImage?.Dispose();
            ShowImage = null;
        }

        public bool Loop
        {
            get; set;
        }

        public void JumpToFrame(int frameIndex)
        {
            if (ImageCount == 0) return;
            if (frameIndex >= 0 && frameIndex < ImageCount)
            {
                if (ShowImage == null || ShowImage.Width != Image.Width || ShowImage.Height != Image.Height)
                {
                    ShowImage?.Dispose();
                    ShowImage = new Bitmap(Image.Width, Image.Height);
                }

                FrameDimension fd = Image.GifFrameDimension();
                Image.SelectActiveFrame(fd, frameIndex);
                ShowImage.Graphics().DrawImage(Image, 0, 0, Image.Width, Image.Height);
                ImageChanged?.Invoke(this, ShowImage);
            }
        }

        private Image ShowImage;

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            if (Image == null)
            {
                Active = false;
                return;
            }

            if (IsGif)
            {
                JumpToFrame(FrameIndex);

                if (FrameIndex < ImageCount - 1)
                {
                    FrameIndex++;
                }
                else
                {
                    if (Loop)
                    {
                        FrameIndex = 0;
                    }
                    else
                    {
                        active = false;
                    }
                }
            }
            else
            {
                ImageChanged?.Invoke(this, Image);
            }

            timer.Enabled = IsGif && active;
        }

        public delegate void OnImageChanged(object sender, Image image);

        public event OnImageChanged ImageChanged;

        private readonly Timer timer;

        private Image image;

        private int ImageCount => image == null ? 0 : image.GifFrameCount();
        public bool IsGif => ImageCount > 0;

        public Image Image
        {
            get => image;
            set
            {
                Active = false;
                image = value;
                if (IsGif)
                {
                    int delay = image.GifFrameInterval();
                    if (delay > 0)
                    {
                        timer.Interval = delay;
                    }
                }
            }
        }

        public void Invalidate()
        {
            if (!Active)
            {
                Active = true;
            }
        }

        private int FrameIndex;
        private bool active;
        public bool Active
        {
            get => active;
            set
            {
                FrameIndex = 0;
                active = value;
                timer.Enabled = value;
            }
        }
    }

    public static class GifHelper
    {
        public static FrameDimension GifFrameDimension(this Image img)
        {
            return new FrameDimension(img.FrameDimensionsList[0]);
        }

        public static int GifFrameCount(this Image img)
        {
            if (img == null) return 0;
            FrameDimension fd = new FrameDimension(img.FrameDimensionsList[0]);
            return img.GetFrameCount(fd);
        }

        public static int GifFrameInterval(this Image img)
        {
            FrameDimension dim = new FrameDimension(img.FrameDimensionsList[0]);
            int frameCount = img.GetFrameCount(dim);
            if (frameCount <= 1)
                return 0;

            int delay = 0;
            bool stop = false;
            for (int i = 0; i < frameCount; i++)//遍历图像帧 
            {
                if (stop) break;

                img.SelectActiveFrame(dim, i);//激活当前帧 
                for (int j = 0; j < img.PropertyIdList.Length; j++)//遍历帧属性 
                {
                    if ((int)img.PropertyIdList.GetValue(j) == 0x5100)//如果是延迟时间 
                    {
                        PropertyItem pItem = (PropertyItem)img.PropertyItems.GetValue(j);//获取延迟时间属性 
                        byte[] delayByte = new byte[4];//延迟时间，以1/100秒为单位 
                        delayByte[0] = pItem.Value[i * 4];
                        delayByte[1] = pItem.Value[1 + i * 4];
                        delayByte[2] = pItem.Value[2 + i * 4];
                        delayByte[3] = pItem.Value[3 + i * 4];
                        delay = BitConverter.ToInt32(delayByte, 0) * 10; //乘以10，获取到毫秒 
                        stop = true;
                        break;
                    }
                }
            }

            return delay;
        }
    }
}
