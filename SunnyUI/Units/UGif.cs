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

        public bool Loop
        {
            get; set;
        }

        public void Reset()
        {
            ImageCount = 0;
            FrameIndex = 0;
            Active = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            if (Image == null)
            {
                Reset();
                return;
            }

            if (IsGif)
            {
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

                FrameDimension fd = image.GifFrameDimension();
                Image.SelectActiveFrame(fd, FrameIndex);
            }

            ImageChanged?.Invoke(this, Image);
            timer.Enabled = IsGif && active;
        }

        public Color BackColor { get; set; } = Color.White;

        public delegate void OnImageChanged(object sender, Image image);

        public event OnImageChanged ImageChanged;

        private readonly Timer timer;

        private Image image;

        private int ImageCount;
        public bool IsGif => ImageCount > 0;

        public Image Image
        {
            get => image;
            set
            {
                Reset();
                image = value;

                if (Image != null)
                {
                    ImageCount = image.GifFrameCount();

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
        }

        public void Invalidate()
        {
            if (!Active)
            {
                Active = true;
            }
        }

        public void Dispose()
        {
            Reset();
            Image?.Dispose();
            Image = null;
        }

        private int FrameIndex;
        private bool active;
        public bool Active
        {
            get => active;
            set
            {
                ImageCount = 0;
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
