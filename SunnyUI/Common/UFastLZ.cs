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
 * 文件名称: FastLZ.cs
 * 文件说明: FastLZ压缩解压类
 * 当前版本: V3.1
 * 创建日期: 2022-03-31
 * 引用地址: https://ariya.github.io/FastLZ/
 * 
 * FastLZ (MIT license) is an ANSI C/C90 implementation of Lempel-Ziv 77 
 * algorithm (LZ77) of lossless data compression. It is suitable to compress 
 * series of text/paragraphs, sequences of raw pixel data, or any other blocks 
 * of data with lots of repetition. It is not intended to be used on images, 
 * videos, and other formats of data typically already in an optimal 
 * compressed form.
 *
 * 2022-03-31: V3.1.2 增加文件说明
 ******************************************************************************
 * 压缩函数
 * int fastlz_compress_level(int level, const void* input, int length, void* output);
 * 
 * level：	压缩级别，目前，仅支持级别1和级别2。
 * 级别1是最快的压缩，通常对短数据有用。
 * 级别2稍微慢一点，但它提供了更好的压缩比。
 * 无论级别如何，压缩数据都可以使用下面的函数fastlz_decompress进行解压缩。
 * 
 * input：	输入缓冲区，用于存放要压缩的数据。
 * length：	输入缓冲区的大小，最小输入缓冲区大小为16。
 * output：	输出缓冲区，用于存放压缩后的数据。输出缓冲区必须至少比输入缓冲区大5%，并且不能小于66字节。
 * 
 * 返回值是压缩后的数据大小，如果输入不可压缩，则返回值可能大于长度。注意，输入缓冲区和输出缓冲区不能重叠。
 ******************************************************************************
 * 解压函数
 * int fastlz_decompress(const void* input, int length, void* output, int maxout);
 * 
 * input：   输入缓冲区，用于存放要解压的数据。
 * length：  输入缓冲区的长度。
 * output：  输出缓冲区，用于存放解压后的数据。
 * maxout：  输出缓冲区的所能容纳的最大长度。解压时会保证输出缓冲区的写入量不会超过maxout中指定的值。
 * 
 * 返回值是解压后的数据大小。如果发生错误，例如压缩数据损坏或输出缓冲区不够大，则将返回0。
 * 注意，输入缓冲区和输出缓冲区不能重叠。
 ******************************************************************************
 * 扩展CompressEx，DecompressEx
 * 扩展压缩结果增加16个字节头部和8个字节尾部，以!开头，\r\n结尾
 * 16个字节头部:8字节标识(!FastLZ )+4字节(输出缓冲区的所能容纳的最大长度maxout)+4字节(当前数据大小)
 * 8个字节尾部:4字节(保留)+1字节(*)+1字节(CRC,累加和,0不判断)+2字节(\r\n)
******************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace Sunny.UI
{
    public static unsafe class FastLZx86
    {
        [DllImport("FastLZx86.dll", EntryPoint = "FastLZ_Compress", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FastLZ_Compress(void* input, int length, void* output);

        [DllImport("FastLZx86.dll", EntryPoint = "FastLZ_Compress_level", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FastLZ_Compress_level(int level, void* input, int length, void* output);

        [DllImport("FastLZx86.dll", EntryPoint = "FastLZ_Decompress", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FastLZ_Decompress(void* input, int length, void* output, int maxout);
    }

    public static unsafe class FastLZx64
    {
        [DllImport("FastLZx64.dll", EntryPoint = "FastLZ_Compress", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FastLZ_Compress(void* input, int length, void* output);

        [DllImport("FastLZx64.dll", EntryPoint = "FastLZ_Compress_level", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FastLZ_Compress_level(int level, void* input, int length, void* output);

        [DllImport("FastLZx64.dll", EntryPoint = "FastLZ_Decompress", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FastLZ_Decompress(void* input, int length, void* output, int maxout);
    }

    public enum FastLZCompressionLevel
    {
        Level1 = 1,
        Level2 = 2
    }

    /// <summary>
    /// FastLZ压缩解压类
    /// </summary>
    public static unsafe class FastLZ
    {
        /// <summary>
        /// 是否64位
        /// </summary>
        /// <returns></returns>
        private static bool Is64bitApp()
        {
            return IntPtr.Size == 8;
        }

        /// <summary>
        /// 压缩(原生）
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="begin">起始位置</param>
        /// <param name="length">长度</param>
        /// <returns>压缩结果</returns>
        public static byte[] Compress(byte[] input, int begin, int length)
        {
            byte[] output = new byte[Math.Max(length * 2, 66)];
            fixed (void* pSrc1 = &input[begin])
            fixed (void* pSrc2 = output)
            {
                int outlen = Is64bitApp() ? FastLZx64.FastLZ_Compress(pSrc1, length, pSrc2) : FastLZx86.FastLZ_Compress(pSrc1, length, pSrc2);
                byte[] result = new byte[outlen];
                Array.Copy(output, 0, result, 0, outlen);
                return result;
            }
        }

        /// <summary>
        /// 压缩(原生）
        /// </summary>
        /// <param name="level">压缩级别</param>
        /// <param name="input">输入</param>
        /// <param name="begin">起始位置</param>
        /// <param name="length">长度</param>
        /// <returns>压缩结果</returns>
        public static byte[] Compress(FastLZCompressionLevel level, byte[] input, int begin, int length)
        {
            byte[] output = new byte[Math.Max(length * 2, 66)];
            fixed (void* pSrc1 = &input[begin])
            fixed (void* pSrc2 = output)
            {
                int outlen = Is64bitApp() ? FastLZx64.FastLZ_Compress_level((int)level, pSrc1, length, pSrc2) : FastLZx86.FastLZ_Compress_level((int)level, pSrc1, length, pSrc2);
                byte[] result = new byte[outlen];
                Array.Copy(output, 0, result, 0, outlen);
                return result;
            }
        }

        /// <summary>
        /// 解压缩(原生）
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="begin">起始位置</param>
        /// <param name="length">长度</param>
        /// <param name="maxout">解压结果最大长度</param>
        /// <returns>解压缩结果</returns>
        public static byte[] Decompress(byte[] input, int begin, int length, int maxout)
        {
            byte[] output = new byte[maxout + 66];
            fixed (byte* pSrc1 = &input[begin])
            fixed (byte* pSrc2 = output)
            {
                int outlen = Is64bitApp() ? FastLZx64.FastLZ_Decompress(pSrc1, length, pSrc2, output.Length) : FastLZx86.FastLZ_Decompress(pSrc1, length, pSrc2, output.Length);
                byte[] result = new byte[outlen];
                Array.Copy(output, 0, result, 0, outlen);
                return result;
            }
        }

        private static byte[] ExHead = "!FastLZ".ToEnBytes(8);
        private const int ExHeadAllLength = 16;
        private const int ExTailAllLength = 8;

        /// <summary>
        /// 压缩(扩展）
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="begin">起始位置</param>
        /// <param name="length">长度</param>
        /// <returns>压缩结果</returns>
        public static byte[] CompressEx(byte[] input, int begin, int length)
        {
            byte[] result = new byte[0];
            if (begin + length > input.Length) return result;
            byte[] output = new byte[Math.Max(length * 2, 66)];
            fixed (void* pSrc1 = &input[begin])
            fixed (void* pSrc2 = output)
            {
                int outlen = Is64bitApp() ? FastLZx64.FastLZ_Compress(pSrc1, length, pSrc2) : FastLZx86.FastLZ_Compress(pSrc1, length, pSrc2);
                result = new byte[outlen + ExHeadAllLength + ExTailAllLength];
                Array.Copy(ExHead, 0, result, 0, ExHead.Length);
                Array.Copy(BitConverter.GetBytes((int)output.Length), 0, result, 8, 4);
                Array.Copy(BitConverter.GetBytes((int)outlen), 0, result, 12, 4);
                Array.Copy(output, 0, result, 16, outlen);
                result[result.Length - 1 - 7] = 0;  //保留
                result[result.Length - 1 - 6] = 0;  //保留
                result[result.Length - 1 - 5] = 0;  //保留
                result[result.Length - 1 - 4] = 0;  //保留
                result[result.Length - 1 - 3] = 42; //*
                result[result.Length - 1 - 2] = 0;  //CRC
                result[result.Length - 1 - 1] = 13; //\r
                result[result.Length - 1 - 0] = 10; //\n
                return result;
            }
        }

        /// <summary>
        /// 解压缩(扩展）
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="begin">起始位置</param>
        /// <param name="length">长度</param>
        /// <returns>解压缩结果</returns>
        public static byte[] DecompressEx(byte[] input, int begin, int length)
        {
            byte[] result = new byte[0];
            if (input.Length <= 2 + ExHeadAllLength + ExTailAllLength) return result;
            if (begin + length > input.Length) return result;
            if (input[begin] != 33) return result;
            if (input[begin + length - 4] != 42) return result;
            if (length != BitConverter.ToInt32(input, begin + 12) + ExHeadAllLength + ExTailAllLength) return result;

            byte[] output = new byte[BitConverter.ToInt32(input, begin + ExHead.Length)];
            fixed (byte* pSrc1 = &input[begin + ExHeadAllLength])
            fixed (byte* pSrc2 = output)
            {
                length = length - ExHeadAllLength - ExTailAllLength;
                int outlen = Is64bitApp() ? FastLZx64.FastLZ_Decompress(pSrc1, length, pSrc2, output.Length) : FastLZx86.FastLZ_Decompress(pSrc1, length, pSrc2, output.Length);
                result = new byte[outlen];
                Array.Copy(output, 0, result, 0, outlen);
                return result;
            }
        }
    }
}