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
 * 文件名称: UCompress.cs
 * 文件说明: 数据帧压缩类
 * 当前版本: V3.4
 * 创建日期: 2023-07-17
 *
 * 2023-07-17: V3.4.0 增加文件说明
******************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace Sunny.UI
{
    public enum DataFrameCompressType
    {
        [DisplayText("None")]
        None = 0,

        [DisplayText("FastLZ")]
        FastLZ = 1,

        [DisplayText("ZLib")]
        ZLib = 2,

        [DisplayText("GZip")]
        GZip = 3,

        [DisplayText("Deflate")]
        Deflate = 4
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct DataFrame
    {
        public DateTime DateTime;
        public byte[] Data;
        public int Index;
        public int CRC;

        public DataFrame(DateTime dateTime, byte[] data, int index, int crc)
        {
            DateTime = dateTime;
            Data = data;
            Index = index;
            CRC = crc;
        }

        public static DataFrame Empty()
        {
#if NET40
            return new DataFrame(DateTime.MinValue, new byte[0], -1, 0);
#else
            return new DataFrame(DateTime.MinValue, Array.Empty<byte>(), -1, 0);
#endif
        }
    }

    public static class DataFrameCompressHelper
    {
        public static byte[] Compress(DataFrame frame, DataFrameCompressType compressType, out int compressedLength, out int decompressLength)
        {
            byte[] compressed;
            decompressLength = frame.Data.Length;
            switch (compressType)
            {
                case DataFrameCompressType.None:
                    compressed = frame.Data;
                    break;
                case DataFrameCompressType.FastLZ:
                    if (FastLZ.CheckFastLZDll())
                    {
                        compressed = FastLZ.Compress(frame.Data, 0, frame.Data.Length);
                        decompressLength = Math.Max(frame.Data.Length * 2, 66);
                    }
                    else
                    {
                        throw new NullReferenceException("FastLZx86.dll and FastLZx64.dll not exists.");
                    }
                    break;
                case DataFrameCompressType.ZLib:
                    compressed = frame.Data.ZLibCompress();
                    break;
                case DataFrameCompressType.GZip:
                    compressed = frame.Data.GZipCompress();
                    break;
                case DataFrameCompressType.Deflate:
                    compressed = frame.Data.DeflateCompress();
                    break;
                default:
                    compressed = frame.Data;
                    break;
            }

            compressedLength = compressed.Length;
            return compressed;
        }

        public static byte[] Decompress(byte[] package, DataFrameCompressType compressType, int decompressLength)
        {
            byte[] data;
            switch (compressType)
            {
                case DataFrameCompressType.None:
                    data = package;
                    break;
                case DataFrameCompressType.FastLZ:
                    if (FastLZ.CheckFastLZDll())
                    {
                        data = FastLZ.Decompress(package, 0, package.Length, decompressLength);
                    }
                    else
                    {
                        throw new NullReferenceException("FastLZx86.dll and FastLZx64.dll not exists.");
                    }
                    break;
                case DataFrameCompressType.ZLib:
                    data = package.ZLibDecompress();
                    break;
                case DataFrameCompressType.GZip:
                    data = package.GZipDecompress();
                    break;
                case DataFrameCompressType.Deflate:
                    data = package.DeflateDecompress();
                    break;
                default:
                    data = package;
                    break;
            }

            return data;
        }
    }
}
