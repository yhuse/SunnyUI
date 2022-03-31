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
        public static bool IsSys64bit()
        {
            return IntPtr.Size == 8;
        }

        public static byte[] Compress(byte[] input, int begin, int len)
        {
            byte[] output = new byte[input.Length];
            fixed (void* pSrc1 = &input[begin])
            fixed (void* pSrc2 = output)
            {
                int outlen = IsSys64bit() ? FastLZx64.FastLZ_Compress(pSrc1, len, pSrc2) : FastLZx86.FastLZ_Compress(pSrc1, len, pSrc2);
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
                int outlen = IsSys64bit() ? FastLZx64.FastLZ_Compress_level(level, pSrc1, len, pSrc2) : FastLZx86.FastLZ_Compress_level(level, pSrc1, len, pSrc2);
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
                int outlen = IsSys64bit() ? FastLZx64.FastLZ_Decompress(pSrc1, length, pSrc2, maxout) : FastLZx86.FastLZ_Decompress(pSrc1, length, pSrc2, maxout);
                byte[] result = new byte[outlen];
                Array.Copy(output, 0, result, 0, outlen);
                return result;
            }
        }
    }
}