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
using System.IO;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// GIF图片帮助类
    /// </summary>
    public class Gif : IDisposable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileName">文件名</param>
        public Gif(string fileName)
        {
            timer = new Timer();
            timer.Tick += Timer_Tick;

            if (File.Exists(fileName))
            {
                Image = Image.FromFile(fileName);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="img">图片</param>
        public Gif(Image img)
        {
            timer = new Timer();
            timer.Tick += Timer_Tick;

            if (img != null)
            {
                Image = img;
            }
        }

        /// <summary>
        /// 析构函数
        /// </summary>
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

        /// <summary>
        /// 是否循环
        /// </summary>
        public bool Loop
        {
            get; set;
        }

        /// <summary>
        /// 跳帧
        /// </summary>
        /// <param name="frameIndex">帧号</param>
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
                using var g = ShowImage.Graphics();
                g.DrawImage(Image, 0, 0, Image.Width, Image.Height);
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

        /// <summary>
        /// 图片切换事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="image">图片</param>
        public delegate void OnImageChanged(object sender, Image image);

        /// <summary>
        /// 图片切换事件
        /// </summary>
        public event OnImageChanged ImageChanged;

        private readonly Timer timer;

        private Image image;

        private int ImageCount => image == null ? 0 : image.GifFrameCount();

        /// <summary>
        /// 图片是否是GIF图片
        /// </summary>
        public bool IsGif => ImageCount > 0;

        /// <summary>
        /// 图片
        /// </summary>
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

        /// <summary>
        /// 刷新
        /// </summary>
        public void Invalidate()
        {
            if (!Active)
            {
                Active = true;
            }
        }

        private int FrameIndex;
        private bool active;

        /// <summary>
        /// 图片动画
        /// </summary>
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

    /// <summary>
    /// GIF图片帮助类
    /// </summary>
    public static class GifHelper
    {
        /// <summary>
        /// 获取图像框架的维度
        /// </summary>
        /// <param name="img">图片</param>
        /// <returns>图像框架的维度</returns>
        public static FrameDimension GifFrameDimension(this Image img)
        {
            return new FrameDimension(img.FrameDimensionsList[0]);
        }

        /// <summary>
        /// 获取图像的帧数
        /// </summary>
        /// <param name="img">图像</param>
        /// <returns>帧数</returns>
        public static int GifFrameCount(this Image img)
        {
            if (img == null) return 0;
            FrameDimension fd = new FrameDimension(img.FrameDimensionsList[0]);
            return img.GetFrameCount(fd);
        }

        /// <summary>
        /// 获取图像的帧间隔
        /// </summary>
        /// <param name="img">图像</param>
        /// <returns>帧间隔</returns>
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
