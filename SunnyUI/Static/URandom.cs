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
 * 文件名称: URandom.cs
 * 文件说明: 随机数扩展类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Text;

namespace Sunny.UI
{
    /// <summary>
    /// 随机数扩展类
    /// </summary>
    public static class RandomEx
    {
        /// <summary>
        /// 随机长整形
        /// </summary>
        /// <returns>结果</returns>
        public static long RandomLong()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// 生成随机纯字母随机字符串
        /// </summary>
        /// <param name="length">生成长度</param>
        /// <returns>结果</returns>
        public static string RandomPureChar(int length = 10)
        {
            return RandomBase(MathEx.CHARS_PUREUPPERCHAR.ToCharArray(), length);
        }

        /// <summary>
        /// 生成字母和数字的随机字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>结果</returns>
        public static string RandomChars(int length = 10)
        {
            return RandomBase(MathEx.CHARS_62.ToCharArray(), length);
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="length">生成长度</param>
        /// <param name="firstcanbezero">首字母可以为0</param>
        /// <returns>结果</returns>
        public static string RandomNumber(int length = 18, bool firstcanbezero = true)
        {
            string str = RandomBase(MathEx.CHARS_DECIMAL.ToCharArray(), length);
            if (firstcanbezero)
            {
                return str;
            }

            while (str.Left(1) == "0")
            {
                str = RandomBase(MathEx.CHARS_DECIMAL.ToCharArray(), length);
            }

            return str;
        }

        private static string RandomBase(char[] pattern, int length)
        {
            if (pattern.IsNullOrEmpty())
            {
                throw new ArgumentNullException();
            }

            string result = "";
            for (int i = 0; i < length; i++)
            {
                Random random = new Random(RandomSeed());
                result += pattern[random.Next(0, pattern.Length)];
            }

            return result;
        }

        /// <summary>
        /// 随机数种子
        /// </summary>
        /// <returns>结果</returns>
        public static int RandomSeed()
        {
            return Guid.NewGuid().GetHashCode();
        }

        /// <summary>
        /// 可以随机生成一个长度为2的十六进制字节数组，
        /// 使用GetString ()方法对其进行解码就可以得到汉字字符了。
        /// 不过对于生成中文汉字验证码来说，因为第15区也就是AF区以前都没有汉字，
        /// 只有少量符号，汉字都从第16区B0开始，并且从区位D7开始以后的汉字都是和很难见到的繁杂汉字，
        /// 所以这些都要排出掉。所以随机生成的汉字十六进制区位码第1位范围在B、C、D之间，
        /// 如果第1位是D的话，第2位区位码就不能是7以后的十六进制数。
        /// 在来看看区位码表发现每区的第一个位置和最后一个位置都是空的，没有汉字，
        /// 因此随机生成的区位码第3位如果是A的话，第4位就不能是0；第3位如果是F的话，
        /// 第4位就不能是F。知道了原理，随机生成中文汉字的程序也就出来了，
        /// 以下就是生成长度为N的随机汉字C#台代码：
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>字符串</returns>
        public static string RandomChinese(int length = 6)
        {
            // 获取GB2312编码页（表）
            Encoding gb = Encoding.GetEncoding("gb2312");
            object[] bytes = CreateRegionCode(length);
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                string temp = gb.GetString((byte[])Convert.ChangeType(bytes[i], typeof(byte[])));
                sb.Append(temp);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 此函数在汉字编码范围内随机创建含两个元素的十六进制字节数组，每个字节数组代表一个汉字，并将
        /// 四个字节数组存储在object数组中。
        /// </summary>
        /// <param name="length">代表需要产生的汉字个数</param>
        /// <returns>结果</returns>
        private static object[] CreateRegionCode(int length)
        {
            //定义一个字符串数组储存汉字编码的组成元素
            var rBase = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
            //定义一个object数组用来
            var bytes = new object[length];
            //每循环一次产生一个含两个元素的十六进制字节数组，并将其放入bytes数组中
            //每个汉字有四个区位码组成
            //区位码第1位和区位码第2位作为字节数组第一个元素
            //区位码第3位和区位码第4位作为字节数组第二个元素
            for (int i = 0; i < length; i++)
            {
                //区位码第1位
                var rnd = new Random(RandomSeed());
                int r1 = rnd.Next(11, 14);
                string strR1 = rBase[r1].Trim();
                //区位码第2位
                rnd = new Random(RandomSeed()); // 更换随机数发生器的 种子避免产生重复值
                int r2 = rnd.Next(0, r1 == 13 ? 7 : 16);
                string strR2 = rBase[r2].Trim();
                //区位码第3位
                rnd = new Random(RandomSeed());
                int r3 = rnd.Next(10, 16);
                string strR3 = rBase[r3].Trim();
                //区位码第4位
                rnd = new Random(RandomSeed());
                int r4;
                switch (r3)
                {
                    case 10:
                        r4 = rnd.Next(1, 16);
                        break;

                    case 15:
                        r4 = rnd.Next(0, 15);
                        break;

                    default:
                        r4 = rnd.Next(0, 16);
                        break;
                }

                string strR4 = rBase[r4].Trim();
                // 定义两个字节变量存储产生的随机汉字区位码
                byte byte1 = Convert.ToByte(strR1 + strR2, 16);
                byte byte2 = Convert.ToByte(strR3 + strR4, 16);
                // 将两个字节变量存储在字节数组中
                byte[] strR = { byte1, byte2 };
                // 将产生的一个汉字的字节数组放入object数组中
                bytes.SetValue(strR, i);
            }

            return bytes;
        }

        /// <summary>
        /// 此函数为生成指定数目的汉字
        /// </summary>
        /// <param name="length">汉字数目</param>
        /// <returns>所有汉字</returns>
        public static string RandomChineseEx(int length = 6)
        {
            var charArrary = new string[length];
            var rand = new Random();
            for (int i = 0; i < length; i++)
            {
                int area = rand.Next(16, 88); //汉字由区位和码位组成(都为0-94,其中区位16-55为一级汉字区,56-87为二级汉字区,1-9为特殊字符区)
                int code = rand.Next(1, area == 55 ? 90 : 94);
                charArrary[i] = Encoding.GetEncoding("GB2312")
                    .GetString(new[] { Convert.ToByte(area + 160), Convert.ToByte(code + 160) });
            }

            var sb = new StringBuilder();
            foreach (string t in charArrary)
            {
                sb.Append(t);
            }

            return sb.ToString();
        }
    }
}