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
 * 文件名称: UCrc32.cs
 * 文件说明: CRC32校验类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System.Collections;
using System.Text;

namespace Sunny.UI
{
    /// <summary>
    /// CRC32校验类
    /// </summary>
    public class Crc32
    {
        private readonly uint _poly;
        private uint[] tab;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Crc32()
            : this(79764919U)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="poly">poly</param>
        public Crc32(uint poly)
        {
            _poly = poly;
        }

        private void Init()
        {
            if (tab != null)
            {
                return;
            }

            tab = new uint[256];
            for (uint index1 = 0U; index1 < 256U; ++index1)
            {
                uint num = index1;
                for (int index2 = 0; index2 < 8; ++index2)
                {
                    if (((int)num & 1) == 0)
                    {
                        num >>= 1;
                    }
                    else
                    {
                        num = num >> 1 ^ _poly;
                    }
                }

                tab[(int)index1] = num;
            }
        }

        /// <summary>
        /// 计算哈希值
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>结果</returns>
        public uint ComputeHash(string text)
        {
            return ComputeHash(Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        /// 计算哈希值
        /// </summary>
        /// <param name="data">数组</param>
        /// <returns>哈希值</returns>
        public uint ComputeHash(byte[] data)
        {
            return ComputeHash(data, 0, data.Length);
        }

        /// <summary>
        /// 计算哈希值
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="start">起始位置</param>
        /// <param name="length">长度</param>
        /// <returns>哈希值</returns>
        public uint ComputeHash(byte[] data, int start, int length)
        {
            return ComputeHash<byte[]>(data, start, length);
        }

        /// <summary>
        /// 计算哈希值
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="start">起始位置</param>
        /// <param name="length">长度</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>哈希值</returns>
        public uint ComputeHash<T>(T data, int start, int length) where T : IList
        {
            Init();
            uint num1 = uint.MaxValue;
            for (int index = 0; index < length; ++index)
            {
                byte num2 = (byte)data[index + start];
                num1 = num1 << 8 ^ tab[num2 ^ num1 >> 24];
            }

            return ~num1;
        }
    }
}