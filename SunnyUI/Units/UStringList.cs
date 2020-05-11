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
 * 文件名称: UStringList.cs
 * 文件说明: 字符串列表
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sunny.UI
{
    /// <summary>
    /// 字符串列表
    /// </summary>
    public class StringList
    {
        private readonly List<string> _strings = new List<string>();

        /// <summary>
        /// 数据个数属性
        /// </summary>
        public int Count => _strings.Count;

        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text => ToString();

        /// <summary>
        /// 读取某行内容
        /// </summary>
        /// <param name="index">序号</param>
        /// <returns>结果</returns>
        public string this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return _strings[index];
            }

            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _strings[index] = value;
            }
        }

        /// <summary>
        /// 字符串数组
        /// </summary>
        public string[] Strings => _strings.ToArray();

        /// <summary>
        /// 追加一行
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>序号</returns>
        public int Add(string value)
        {
            _strings.Add(value);
            return Count;
        }

        /// <summary>
        /// 插入一行
        /// </summary>
        /// <param name="index">序号</param>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public void Insert(int index, string value)
        {
            if (index < 0)
            {
                index = 0;
            }

            _strings.Insert(index, value);
        }

        /// <summary>
        /// 查找数据的位置
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>位置</returns>
        public int IndexOf(string value)
        {
            return _strings.IndexOf(value);
        }

        /// <summary>
        /// 删除一行
        /// </summary>
        /// <param name="index">序号</param>
        public void Delete(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            _strings.RemoveAt(index);
        }

        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <returns>结果</returns>
        public override string ToString()
        {
            return string.Join(Environment.NewLine, _strings.ToArray());
        }

        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <param name="startIndex">开始序号</param>
        /// <param name="count">长度</param>
        /// <returns>字符串</returns>
        public string ToString(int startIndex, int count)
        {
            if (startIndex < 0)
            {
                startIndex = 0;
            }
            else if (startIndex >= Count)
            {
                return "";
            }

            if (count <= 0)
            {
                return "";
            }

            if (count + startIndex > Count)
            {
                count = Count - startIndex;
            }

            var s = new StringBuilder(Count);
            for (int i = startIndex; i < count; i++)
            {
                s.Append(_strings[i] + Environment.NewLine);
            }

            return s.ToString();
        }

        /// <summary>
        /// 清除内容
        /// </summary>
        public void Clear()
        {
            _strings.Clear();
        }

        /// <summary>
        /// 保存为一个文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="encoding">编码</param>
        public void SaveToFile(string fileName, Encoding encoding)
        {
            File.WriteAllLines(fileName, _strings.ToArray(), encoding);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void SaveToFile(string fileName)
        {
            SaveToFile(fileName, Encoding.UTF8);
        }

        /// <summary>
        /// 读入一个文本文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void LoadFromFile(string fileName)
        {
            LoadFromFile(fileName, Encoding.UTF8);
        }

        /// <summary>
        /// 从文件载入
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="encoding">编码</param>
        public void LoadFromFile(string fileName, Encoding encoding)
        {
            Clear();
            _strings.AddRange(File.ReadAllLines(fileName, encoding));
        }
    }
}