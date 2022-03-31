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
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sunny.UI
{
    internal static unsafe class FastLZx86
    {
        [DllImport("FastLZx86.dll", EntryPoint = "FastLZ_Compress", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FastLZ_Compress(void* input, int length, void* output);

        [DllImport("FastLZx86.dll", EntryPoint = "FastLZ_Compress_level", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FastLZ_Compress_level(int level, void* input, int length, void* output);

        [DllImport("FastLZx86.dll", EntryPoint = "FastLZ_Decompress", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FastLZ_Decompress(void* input, int length, void* output, int maxout);
    }

    internal static unsafe class FastLZx64
    {
        [DllImport("FastLZx64.dll", EntryPoint = "FastLZ_Compress", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FastLZ_Compress(void* input, int length, void* output);

        [DllImport("FastLZx64.dll", EntryPoint = "FastLZ_Compress_level", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FastLZ_Compress_level(int level, void* input, int length, void* output);

        [DllImport("FastLZx64.dll", EntryPoint = "FastLZ_Decompress", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FastLZ_Decompress(void* input, int length, void* output, int maxout);
    }

    public static unsafe class FastLZ
    {
        /// <summary>
        /// 是否64位
        /// </summary>
        /// <returns></returns>
        public static bool Is64bitApp()
        {
            return IntPtr.Size == 8;
        }

        public static byte[] Compress(byte[] input, int begin, int len)
        {
            byte[] output = new byte[input.Length];
            fixed (void* pSrc1 = &input[begin])
            fixed (void* pSrc2 = output)
            {
                int outlen = Is64bitApp() ? FastLZx64.FastLZ_Compress(pSrc1, len, pSrc2) : FastLZx86.FastLZ_Compress(pSrc1, len, pSrc2);
                byte[] result = new byte[outlen];
                Array.Copy(output, 0, result, 0, outlen);
                return result;
            }
        }

        public static byte[] Compress(int level, byte[] input, int begin, int len)
        {
            byte[] output = new byte[input.Length];
            fixed (void* pSrc1 = &input[begin])
            fixed (void* pSrc2 = output)
            {
                int outlen = Is64bitApp() ? FastLZx64.FastLZ_Compress_level(level, pSrc1, len, pSrc2) : FastLZx86.FastLZ_Compress_level(level, pSrc1, len, pSrc2);
                byte[] result = new byte[outlen];
                Array.Copy(output, 0, result, 0, outlen);
                return result;
            }
        }

        public static byte[] Decompress(byte[] input, int begin, int length, int maxout)
        {
            byte[] output = new byte[maxout];
            fixed (byte* pSrc1 = &input[begin])
            fixed (byte* pSrc2 = output)
            {
                int outlen = Is64bitApp() ? FastLZx64.FastLZ_Decompress(pSrc1, length, pSrc2, maxout) : FastLZx86.FastLZ_Decompress(pSrc1, length, pSrc2, maxout);
                byte[] result = new byte[outlen];
                Array.Copy(output, 0, result, 0, outlen);
                return result;
            }
        }
    }
}