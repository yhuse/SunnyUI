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
 * 文件名称: UArray.cs
 * 文件说明: 数组扩展类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;

namespace Sunny.UI
{
    /// <summary>
    /// 数组扩展类
    /// </summary>
    public static class ArrayEx
    {
        /// <summary>
        /// BCD码数组
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="length">长度，一个字节两位</param>
        /// <returns>数组</returns>
        public static byte[] BCDData(int value, int length)
        {
            return value.ToString("D" + length * 2).ToHexBytes();
        }

        /// <summary>
        /// BCD码数组
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="length">长度，一个字节两位</param>
        /// <returns>数组</returns>
        public static byte[] BCDData(long value, int length)
        {
            return value.ToString("D" + length * 2).ToHexBytes();
        }

        /// <summary>数据流转为字节数组</summary>
        /// <remarks>
        /// 针对MemoryStream进行优化。内存流的Read实现是一个个字节复制，而ToArray是调用内部内存复制方法
        /// 如果要读完数据，又不支持定位，则采用内存流搬运
        /// 如果指定长度超过数据流长度，就让其报错，因为那是调用者所期望的值
        /// </remarks>
        /// <param name="stream">数据流</param>
        /// <param name="length">长度，0表示读到结束</param>
        /// <returns>字节数组</returns>
        public static byte[] ReadBytes(this Stream stream, long length = -1)
        {
            if (stream == null)
            {
                return null;
            }

            if (length == 0)
            {
                return new byte[0];
            }

            if (stream.CanSeek && stream.Length - stream.Position < length)
            {
                throw new Exception($"无法从长度只有{stream.Length - stream.Position}的数据流里面读取{length}字节的数据");
            }

            var buf = new byte[length];
            stream.Read(buf, 0, buf.Length);
            return buf;
        }

        /// <summary>复制数组</summary>
        /// <param name="src">源数组</param>
        /// <param name="offset">起始位置</param>
        /// <param name="count">复制字节数</param>
        /// <returns>返回复制的总字节数</returns>
        public static byte[] ReadBytes(this byte[] src, int offset = 0, int count = -1)
        {
            if (count == 0)
            {
                return new byte[0];
            }

            // 即使是全部，也要复制一份，而不只是返回原数组，因为可能就是为了复制数组
            if (count < 0)
            {
                count = src.Length - offset;
            }

            var bts = new byte[count];
            Buffer.BlockCopy(src, offset, bts, 0, bts.Length);
            return bts;
        }

        /// <summary>
        /// 字符串转换为Bool数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>Bool数组</returns>
        public static bool[] ToBooleans(this string str)
        {
            bool[] result = new bool[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                result[i] = str.Substring(i, 1) == "1";
            }

            return result;
        }

        /// <summary>
        /// Bool数组转换为字符串
        /// </summary>
        /// <param name="b">Bool数组</param>
        /// <returns>字符串</returns>
        public static string ToBooleansString(this bool[] b)
        {
            StringBuilder sb = new StringBuilder();
            foreach (bool b1 in b)
            {
                sb.Append(b1 ? "1" : "0");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Bool数组转换为Byte数组
        /// </summary>
        /// <param name="b">Bool数组</param>
        /// <param name="needCompress">需要压缩</param>
        /// <returns>Byte数组</returns>
        public static byte[] ToBytes(this bool[] b, bool needCompress = false)
        {
            if (b.IsNullOrEmpty())
            {
                return null;
            }

            if (!needCompress)
            {
                byte[] bts = new byte[b.Length];
                for (int i = 0; i < b.Length; i++)
                {
                    bts[i] = b[i].ToByte();
                }

                return bts;
            }
            else
            {
                int len = b.Length / 8;
                if (b.Length % 8 != 0)
                {
                    len++;
                }

                byte[] all = b.ToBytes();
                StringBuilder sb = new StringBuilder();
                foreach (byte t in all)
                {
                    sb.Append(t.ToString());
                }

                string str = sb.ToString().PadRight(len * 8, '0');
                byte[] bts = new byte[len];
                for (int i = 0; i < len; i++)
                {
                    bts[i] = (byte)str.Left(8).ToNumberLong(MathEx.Characters.BINARY);
                    str = str.RemoveLeft(8);
                }

                return bts;
            }
        }

        /// <summary>
        /// Byte数组转换为Bool数组
        /// </summary>
        /// <param name="b">Byte数组</param>
        /// <param name="compressed">是否压缩</param>
        /// <returns>Bool数组</returns>
        public static bool[] ToBooleans(this byte[] b, bool compressed = false)
        {
            if (b.IsNullOrEmpty())
            {
                return null;
            }

            if (!compressed)
            {
                bool[] bts = new bool[b.Length];
                for (int i = 0; i < b.Length; i++)
                {
                    bts[i] = b[i].ToBool();
                }

                return bts;
            }
            else
            {
                int len = b.Length * 8;
                bool[] bts = new bool[len];
                for (int i = 0; i < b.Length; i++)
                {
                    string str = b[i].ToNumberString(MathEx.Characters.BINARY).PadLeft(8, '0');
                    for (int j = 0; j < 8; j++)
                    {
                        bts[i * 8 + j] = str[j] == '1';
                    }
                }

                return bts;
            }
        }

        /// <summary>
        /// 浮点数组转为字节数组
        /// </summary>
        /// <param name="f">浮点数组</param>
        /// <returns>字节数组</returns>
        public static byte[] ToBytes(this float[] f)
        {
            byte[] b = new byte[f.Length * 4];
            for (int i = 0; i < f.Length; i++)
            {
                Array.Copy(BitConverter.GetBytes(f[i]), 0, b, i * 4, 4);
            }

            return b;
        }

        /// <summary>
        /// 字节数组转为浮点数组
        /// </summary>
        /// <param name="b">字节数组</param>
        /// <returns>浮点数组</returns>
        public static float[] ToFloats(this byte[] b)
        {
            float[] f = new float[b.Length / 4];
            for (int i = 0; i < f.Length; i++)
            {
                f[i] = BitConverter.ToSingle(b, i * 4);
            }

            return f;
        }

        /// <summary>
        /// 整形数组转为字节数组
        /// </summary>
        /// <param name="f">整形数组</param>
        /// <returns>字节数组</returns>
        public static byte[] ToBytes(this int[] f)
        {
            byte[] b = new byte[f.Length * 4];
            for (int i = 0; i < f.Length; i++)
            {
                Array.Copy(BitConverter.GetBytes(f[i]), 0, b, i * 4, 4);
            }

            return b;
        }

        /// <summary>
        /// 字节数组转为整形数组
        /// </summary>
        /// <param name="b">字节数组</param>
        /// <returns>整形数组</returns>
        public static int[] ToIntegers(this byte[] b)
        {
            int[] f = new int[b.Length / 4];
            for (int i = 0; i < f.Length; i++)
            {
                f[i] = BitConverter.ToInt32(b, i * 4);
            }

            return f;
        }

        /// <summary>
        /// 字节数组转为Base64编码字符串
        /// </summary>
        /// <param name="b">字节数组</param>
        /// <returns>整形数组</returns>
        public static string ToBase64String(this byte[] b)
        {
            return b.IsNullOrEmpty() ? string.Empty : Convert.ToBase64String(b);
        }

        /// <summary>
        /// 字节数组转为整形数组
        /// </summary>
        /// <param name="b">字节数组</param>
        /// <returns>整形数组</returns>
        public static byte[] ToBase64Bytes(this string b)
        {
            return b.IsNullOrEmpty() ? null : Convert.FromBase64String(b);
        }

        /// <summary>
        /// 字节数组转为值类型的对象
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="bytes">字节数组</param>
        /// <returns>对象</returns>
        public static T ToStruct<T>(this byte[] bytes) where T : struct
        {
            if (bytes == null)
            {
                throw new NullReferenceException();
            }

            //            Type type = typeof(T);
            //            if (type.GetCustomAttribute<StructLayoutAttribute>() == null)
            //            {
            //                throw new Exception(type.Name + "未设置 StructLayout 属性。");
            //            }

            //得到结构体的大小
            int size;
            try
            {
                size = Marshal.SizeOf(typeof(T));
            }
            catch (Exception)
            {
                throw new NullReferenceException();
            }

            if (size > bytes.Length)
            {
                throw new NullReferenceException();
            }

            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将byte数组拷到分配好的内存空间
            Marshal.Copy(bytes, 0, structPtr, size);
            //将内存空间转换为目标结构体
            var obj = Marshal.PtrToStructure(structPtr, typeof(T));
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            //返回结构体
            return (T)obj;
        }

        /// <summary>
        /// 值类型对象长度
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="obj">obj</param>
        /// <returns>长度</returns>
        public static int Size<T>(this T obj) where T : struct
        {
            return Marshal.SizeOf(typeof(T));
        }

        /// <summary>
        /// 值类型的对象转换为字节数组
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>字节数组</returns>
        public static byte[] StructToBytes<T>(this T obj) where T : struct
        {
            //            Type type = typeof(T);
            //            if (type.GetCustomAttribute<StructLayoutAttribute>() == null)
            //            {
            //                throw new ApplicationException(type.Name + "未设置 StructLayout 属性。");
            //            }

            //得到结构体的大小
            int size = obj.Size();
            //创建byte数组
            var bytes = new byte[size];
            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将结构体拷到分配好的内存空间
            Marshal.StructureToPtr(obj, structPtr, false);
            //从内存空间拷到byte数组
            Marshal.Copy(structPtr, bytes, 0, size);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            //返回byte数组
            return bytes;
        }

        /// <summary>
        /// 从二维数组获取某一维的数组
        /// </summary>
        /// <param name="two">二维数组</param>
        /// <param name="index">维数</param>
        /// <returns>一维数组</returns>
        public static byte[] One(this byte[,] two, int index)
        {
            if (index < 0 || index >= two.GetLength(0))
            {
                return null;
            }

            int len = two.GetLength(1);
            var result = new byte[len];
            IntPtr arrHandler = Marshal.UnsafeAddrOfPinnedArrayElement(two, index * len);
            Marshal.Copy(arrHandler, result, 0, len);
            return result;
        }

        /// <summary>
        /// 从二维数组获取某一维的数组
        /// </summary>
        /// <param name="two">二维数组</param>
        /// <param name="index">维数</param>
        /// <returns>一维数组</returns>
        public static float[] One(this float[,] two, int index)
        {
            if (index < 0 || index >= two.GetLength(0))
            {
                return null;
            }

            int len = two.GetLength(1);
            var result = new float[len];
            IntPtr arrHandler = Marshal.UnsafeAddrOfPinnedArrayElement(two, index * len);
            Marshal.Copy(arrHandler, result, 0, len);
            return result;
        }

        /// <summary>
        /// 从二维数组获取某一维的数组
        /// </summary>
        /// <param name="two">二维数组</param>
        /// <param name="index">维数</param>
        /// <returns>一维数组</returns>
        public static int[] One(this int[,] two, int index)
        {
            if (index < 0 || index >= two.GetLength(0))
            {
                return null;
            }

            int len = two.GetLength(1);
            var result = new int[len];
            IntPtr arrHandler = Marshal.UnsafeAddrOfPinnedArrayElement(two, index * len);
            Marshal.Copy(arrHandler, result, 0, len);
            return result;
        }

        /// <summary>
        /// 从二维数组获取某一维的数组
        /// </summary>
        /// <param name="two">二维数组</param>
        /// <param name="index">维数</param>
        /// <returns>一维数组</returns>
        public static double[] One(this double[,] two, int index)
        {
            if (index < 0 || index >= two.GetLength(0))
            {
                return null;
            }

            int len = two.GetLength(1);
            var result = new double[len];
            IntPtr arrHandler = Marshal.UnsafeAddrOfPinnedArrayElement(two, index * len);
            Marshal.Copy(arrHandler, result, 0, len);
            return result;
        }

        /// <summary>
        /// 从二维数组获取某一维的数组
        /// </summary>
        /// <param name="two">二维数组</param>
        /// <param name="index">维数</param>
        /// <returns>一维数组</returns>
        public static char[] One(this char[,] two, int index)
        {
            if (index < 0 || index >= two.GetLength(0))
            {
                return null;
            }

            int len = two.GetLength(1);
            var result = new char[len];
            IntPtr arrHandler = Marshal.UnsafeAddrOfPinnedArrayElement(two, index * len);
            Marshal.Copy(arrHandler, result, 0, len);
            return result;
        }

        /// <summary>
        /// 从二维数组获取某一维的数组
        /// </summary>
        /// <param name="two">二维数组</param>
        /// <param name="index">维数</param>
        /// <returns>一维数组</returns>
        public static long[] One(this long[,] two, int index)
        {
            if (index < 0 || index >= two.GetLength(0))
            {
                return null;
            }

            int len = two.GetLength(1);
            var result = new long[len];
            IntPtr arrHandler = Marshal.UnsafeAddrOfPinnedArrayElement(two, index * len);
            Marshal.Copy(arrHandler, result, 0, len);
            return result;
        }

        /// <summary>
        /// 从二维数组获取某一维的数组
        /// </summary>
        /// <param name="two">二维数组</param>
        /// <param name="index">维数</param>
        /// <returns>一维数组</returns>
        public static short[] One(this short[,] two, int index)
        {
            if (index < 0 || index >= two.GetLength(0))
            {
                return null;
            }

            int len = two.GetLength(1);
            var result = new short[len];
            IntPtr arrHandler = Marshal.UnsafeAddrOfPinnedArrayElement(two, index * len);
            Marshal.Copy(arrHandler, result, 0, len);
            return result;
        }

        /// <summary>
        /// 取中值
        /// </summary>
        /// <param name="a">数组</param>
        /// <param name="avg">当数组个数为偶数时，是否需要将中间两数平均</param>
        /// <returns>中值</returns>
        public static double MedianNum(this double[] a, bool avg = false)
        {
            if (a.IsNullOrEmpty())
            {
                throw new NullReferenceException();
            }

            Array.Sort(a);
            return (a.Length & 1) > 0 ? a[a.Length / 2] : (avg ? a[a.Length / 2 - 1] : a[a.Length / 2]);
        }

        /// <summary>
        /// 取中值
        /// </summary>
        /// <param name="a">数组</param>
        /// <param name="avg">当数组个数为偶数时，是否需要将中间两数平均</param>
        /// <returns>中值</returns>
        public static int MedianNum(this int[] a, bool avg = false)
        {
            if (a.IsNullOrEmpty())
            {
                throw new NullReferenceException();
            }

            Array.Sort(a);
            return (a.Length & 1) > 0 ? a[a.Length / 2] : (avg ? a[a.Length / 2 - 1] : a[a.Length / 2]);
        }

        /// <summary>
        /// 取中值
        /// </summary>
        /// <param name="a">数组</param>
        /// <param name="avg">当数组个数为偶数时，是否需要将中间两数平均</param>
        /// <returns>中值</returns>
        public static byte MedianNum(this byte[] a, bool avg = false)
        {
            if (a.IsNullOrEmpty())
            {
                throw new NullReferenceException();
            }

            Array.Sort(a);
            return (a.Length & 1) > 0 ? a[a.Length / 2] : (avg ? a[a.Length / 2 - 1] : a[a.Length / 2]);
        }

        /// <summary>
        /// 取中值
        /// </summary>
        /// <param name="a">数组</param>
        /// <param name="avg">当数组个数为偶数时，是否需要将中间两数平均</param>
        /// <returns>中值</returns>
        public static float MedianNum(this float[] a, bool avg = false)
        {
            if (a.IsNullOrEmpty())
            {
                throw new NullReferenceException();
            }

            Array.Sort(a);
            return (a.Length & 1) > 0 ? a[a.Length / 2] : (avg ? a[a.Length / 2 - 1] : a[a.Length / 2]);
        }

        /// <summary>
        /// 取中值
        /// </summary>
        /// <param name="a">数组</param>
        /// <param name="avg">当数组个数为偶数时，是否需要将中间两数平均</param>
        /// <returns>中值</returns>
        public static short MedianNum(this short[] a, bool avg = false)
        {
            if (a.IsNullOrEmpty())
            {
                throw new NullReferenceException();
            }

            Array.Sort(a);
            return (a.Length & 1) > 0 ? a[a.Length / 2] : (avg ? a[a.Length / 2 - 1] : a[a.Length / 2]);
        }

        /// <summary>
        /// 检查索引是否存在
        /// </summary>
        /// <param name="source">数组</param>
        /// <param name="index">索引</param>
        /// <returns>索引是否存在</returns>
        public static bool WithinIndex(this IList source, int index)
        {
            return source.IsValid() && index >= 0 && index < source.Count;
        }

        /// <summary>
        ///	检查数组是否为空
        /// </summary>
        /// <param name = "source">数组</param>
        /// <returns>结果</returns>
        public static bool IsNullOrEmpty(this IList source)
        {
            return source == null || source.Count == 0;
        }

        /// <summary>
        ///	检查数组是否为空
        /// </summary>
        /// <param name = "source">数组</param>
        /// <returns>结果</returns>
        public static bool IsValid(this IList source)
        {
            return !source.IsNullOrEmpty();
        }

        /// <summary>
        /// 附加数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="combineWith">原数组</param>
        /// <param name="arrayToCombine">待附加数组</param>
        /// <returns>结果</returns>
        public static T[] CombineArray<T>(this T[] combineWith, T[] arrayToCombine)
        {
            if (combineWith == default(T[]) || arrayToCombine == default(T[]))
            {
                return combineWith;
            }

            int initialSize = combineWith.Length;
            Array.Resize(ref combineWith, initialSize + arrayToCombine.Length);
            Array.Copy(arrayToCombine, arrayToCombine.GetLowerBound(0), combineWith, initialSize, arrayToCombine.Length);
            return combineWith;
        }

        /// <summary>
        /// 复制数组的一块
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="index">起始索引</param>
        /// <param name="length">长度</param>
        /// <param name="padToLength">长度不够是否补齐</param>
        /// <returns>数组</returns>
        public static T[] BlockCopy<T>(this T[] array, int index, int length, bool padToLength = false)
        {
            if (array == null)
            {
                throw new NullReferenceException();
            }

            int n = length;
            T[] b = null;

            if (array.Length < index + length)
            {
                n = array.Length - index;
                if (padToLength)
                {
                    b = new T[length];
                }
            }

            if (b == null)
            {
                b = new T[n];
            }

            Array.Copy(array, index, b, 0, n);
            return b;
        }

        /// <summary>
        /// Find the first occurence of an byte[] in another byte[]
        /// </summary>
        /// <param name = "buf1">the byte[] to search in</param>
        /// <param name = "buf2">the byte[] to find</param>
        /// <returns>the first position of the found byte[] or -1 if not found</returns>
        /// <remarks>
        /// http://www.codeplex.com/site/users/view/blaumeiser
        /// </remarks>
        public static int FindArrayInArray(this byte[] buf1, byte[] buf2)
        {
            if (buf2 == null || buf1 == null)
            {
                return -1;
            }

            if (buf2.Length == 0)
            {
                return 0; // by definition empty sets match immediately
            }

            int j = -1;
            int end = buf1.Length - buf2.Length;
            while ((j = Array.IndexOf(buf1, buf2[0], j + 1)) <= end && j != -1)
            {
                int i = 1;
                while (buf1[j + i] == buf2[i])
                {
                    if (++i == buf2.Length)
                    {
                        return j;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// 字节数组转十六进制字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="span">分隔符</param>
        /// <returns>结果</returns>
        public static string ToHexString(this byte[] bytes, string span = "")
        {
            if (bytes.IsNullOrEmpty())
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("X").PadLeft(2, '0'));
                sb.Append(span);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 十六进制字符串转字节数组
        /// </summary>
        /// <param name="str">十六进制字符串</param>
        /// <param name="span">分隔符</param>
        /// <returns>结果</returns>
        public static byte[] ToHexBytes(this string str, string span = "")
        {
            if (!span.IsNullOrEmpty())
            {
                str = str.Replace(span, "");
            }

            if (str.IsNullOrEmpty())
            {
                return new byte[0];
            }

            var bytes = new byte[str.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = byte.Parse(str.Substring(2 * i, 2), NumberStyles.AllowHexSpecifier);
            }

            return bytes;
        }

        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="inputBytes">数组</param>
        /// <returns>结果</returns>
        public static byte[] GZipCompress(this byte[] inputBytes)
        {
            if (inputBytes.IsNullOrEmpty())
            {
                return null;
            }

            using (MemoryStream outStream = new MemoryStream())
            using (GZipStream zipStream = new GZipStream(outStream, CompressionMode.Compress, true))
            {
                zipStream.Write(inputBytes, 0, inputBytes.Length);
                zipStream.Close(); //很重要，必须关闭，否则无法正确解压
                return outStream.ToArray();
            }
        }

        /// <summary>
        /// 解压缩字节数组
        /// </summary>
        /// <param name="inputBytes">数组</param>
        /// <returns>结果</returns>
        public static byte[] GZipDecompress(this byte[] inputBytes)
        {
            if (inputBytes.IsNullOrEmpty())
            {
                return null;
            }

            using (MemoryStream inputStream = new MemoryStream(inputBytes))
            using (MemoryStream outStream = new MemoryStream())
            using (GZipStream zipStream = new GZipStream(inputStream, CompressionMode.Decompress))
            {
                zipStream.CopyTo(outStream);
                zipStream.Close();
                return outStream.ToArray();
            }
        }

        /// <summary>
        /// Deflate算法压缩字节数组
        /// </summary>
        /// <param name="inputBytes">数组</param>
        /// <returns>结果</returns>
        public static byte[] DeflateCompress(this byte[] inputBytes)
        {
            if (inputBytes.IsNullOrEmpty())
            {
                return null;
            }

            using (MemoryStream outStream = new MemoryStream())
            using (DeflateStream zipStream = new DeflateStream(outStream, CompressionMode.Compress, true))
            {
                zipStream.Write(inputBytes, 0, inputBytes.Length);
                zipStream.Close(); //很重要，必须关闭，否则无法正确解压
                return outStream.ToArray();
            }
        }

        /// <summary>
        /// Deflate算法解压缩字节数组
        /// </summary>
        /// <param name="inputBytes">数组</param>
        /// <returns>结果</returns>
        public static byte[] DeflateDecompress(this byte[] inputBytes)
        {
            if (inputBytes.IsNullOrEmpty())
            {
                return null;
            }

            using (MemoryStream inputStream = new MemoryStream(inputBytes))
            using (MemoryStream outStream = new MemoryStream())
            using (DeflateStream zipStream = new DeflateStream(inputStream, CompressionMode.Decompress))
            {
                zipStream.CopyTo(outStream);
                zipStream.Close();
                return outStream.ToArray();
            }
        }

        /// <summary>
        /// 冒泡排序法
        /// </summary>
        /// <param name="list">列表</param>
        public static void BubbleSort(this int[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                for (int j = i; j < list.Length; j++)
                {
                    if (list[i] >= list[j])
                    {
                        continue;
                    }

                    int temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }
        }

        /// <summary>
        /// 插入排序法
        /// </summary>
        /// <param name="list">列表</param>
        public static void InsertionSort(int[] list)
        {
            for (int i = 1; i < list.Length; i++)
            {
                int t = list[i];
                int j = i;
                while ((j > 0) && (list[j - 1] > t))
                {
                    list[j] = list[j - 1];
                    --j;
                }

                list[j] = t;
            }
        }

        /// <summary>
        /// 选择排序法
        /// </summary>
        /// <param name="list">列表</param>
        public static void SelectionSort(int[] list)
        {
            for (int i = 0; i < list.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < list.Length; j++)
                {
                    if (list[j] < list[min])
                    {
                        min = j;
                    }
                }

                int t = list[min];
                list[min] = list[i];
                list[i] = t;
            }
        }

        /// <summary>
        /// 希尔排序法
        /// </summary>
        /// <param name="list">列表</param>
        public static void ShellSort(int[] list)
        {
            int inc;
            for (inc = 1; inc <= list.Length / 9; inc = 3 * inc + 1)
            {
                for (; inc > 0; inc /= 3)
                {
                    for (int i = inc + 1; i <= list.Length; i += inc)
                    {
                        int t = list[i - 1];
                        int j = i;
                        while ((j > inc) && (list[j - inc - 1] > t))
                        {
                            list[j - 1] = list[j - inc - 1];
                            j -= inc;
                        }

                        list[j - 1] = t;
                    }
                }
            }
        }

        /// <summary>
        /// 赋值交换
        /// </summary>
        /// <param name="l">l</param>
        /// <param name="r">r</param>
        /// <typeparam name="T">T</typeparam>
        public static void Swap<T>(ref T l, ref T r) where T : struct
        {
            T s = l;
            l = r;
            r = s;
        }

        /// <summary>
        /// 快速排序法
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="low">起始序号</param>
        /// <param name="high">结束序号</param>
        public static void QuickSort(int[] list, int low, int high)
        {
            if (high <= low)
            {
                return;
            }

            if (high == low + 1)
            {
                if (list[low] > list[high])
                {
                    Swap(ref list[low], ref list[high]);
                }

                return;
            }

            int mid = (low + high) >> 1;
            int pivot = list[mid];
            Swap(ref list[low], ref list[mid]);
            int l = low + 1;
            int r = high;
            do
            {
                while (l <= r && list[l] < pivot)
                {
                    l++;
                }

                while (list[r] >= pivot)
                {
                    r--;
                }

                if (l < r)
                {
                    Swap(ref list[l], ref list[r]);
                }
            }
            while (l < r);

            list[low] = list[r];
            list[r] = pivot;
            if (low + 1 < r)
            {
                QuickSort(list, low, r - 1);
            }

            if (r + 1 < high)
            {
                QuickSort(list, r + 1, high);
            }
        }
    }
}