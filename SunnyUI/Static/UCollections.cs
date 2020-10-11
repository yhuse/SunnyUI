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
 * 文件名称: UCollections.cs
 * 文件说明: 集合扩展类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Sunny.UI
{
    /// <summary>
    /// Collections 扩展类
    /// </summary>
    public static class CollectionsEx
    {
        /// <summary>
        /// 键排序列表
        /// </summary>
        /// <typeparam name="Key">键</typeparam>
        /// <typeparam name="Value">值</typeparam>
        /// <param name="dictionary">字典</param>
        /// <returns>键排序列表</returns>
        public static List<Key> SortedKeys<Key, Value>(this ConcurrentDictionary<Key, Value> dictionary)
        {
            if (dictionary == null) return null;

            List<Key> keys = dictionary.Keys.ToList();
            keys.Sort();
            return keys;
        }

        /// <summary>
        /// 键排序列表
        /// </summary>
        /// <typeparam name="Key">键</typeparam>
        /// <typeparam name="Value">值</typeparam>
        /// <param name="dictionary">字典</param>
        /// <returns>键排序列表</returns>
        public static List<Key> SortedKeys<Key, Value>(this Dictionary<Key, Value> dictionary)
        {
            if (dictionary == null) return null;

            List<Key> keys = dictionary.Keys.ToList();
            keys.Sort();
            return keys;
        }

        /// <summary>
        /// 键排序后，取值排序列表
        /// </summary>
        /// <typeparam name="Key">键</typeparam>
        /// <typeparam name="Value">值</typeparam>
        /// <param name="dictionary">字典</param>
        /// <returns>值排序列表</returns>
        public static List<Value> SortedValues<Key, Value>(this ConcurrentDictionary<Key, Value> dictionary)
        {
            if (dictionary == null) return null;

            List<Key> keys = dictionary.SortedKeys();
            List<Value> values = new List<Value>();
            foreach (var key in keys)
            {
                values.Add(dictionary[key]);
            }

            return values;
        }

        /// <summary>
        /// 键排序后，取值排序列表
        /// </summary>
        /// <typeparam name="Key">键</typeparam>
        /// <typeparam name="Value">值</typeparam>
        /// <param name="dictionary">字典</param>
        /// <returns>值排序列表</returns>
        public static List<Value> SortedValues<Key, Value>(this Dictionary<Key, Value> dictionary)
        {
            if (dictionary == null) return null;

            List<Key> keys = dictionary.SortedKeys();
            List<Value> values = new List<Value>();
            foreach (var key in keys)
            {
                values.Add(dictionary[key]);
            }

            return values;
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="queue">queue</param>
        public static void Clear<T>(this ConcurrentQueue<T> queue)
        {
            while (queue.Count > 0)
            {
                queue.TryDequeue(out T _);
            }
        }

        /// <summary>
        /// 增加或更新
        /// </summary>
        /// <param name="dictionary">字典</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <typeparam name="Key">键类型</typeparam>
        /// <typeparam name="Value">值类型</typeparam>
        public static void TryAddOrUpdate<Key, Value>(this ConcurrentDictionary<Key, Value> dictionary, Key key, Value value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.TryAdd(key, value);
            }
        }

        /// <summary>
        /// 增加或更新
        /// </summary>
        /// <param name="dictionary">字典</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <typeparam name="Key">键类型</typeparam>
        /// <typeparam name="Value">值类型</typeparam>
        public static void TryAddOrUpdate<Key, Value>(this Dictionary<Key, Value> dictionary, Key key, Value value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        /// <summary>
        /// GetOneByValue 从字典中找出第一个值与value相等的记录的key
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dic">字典</param>
        /// <param name="value">值</param>
        /// <returns>Key</returns>
        public static TKey FindFirstKeyByValue<TKey, TValue>(this IDictionary<TKey, TValue> dic, TValue value) where TKey : class where TValue : IEquatable<TValue>
        {
            foreach (var pair in dic)
            {
                if (pair.Value.Equals(value))
                {
                    return pair.Key;
                }
            }

            return default;
        }

        /// <summary>
        /// RemoveOneByValue 从字典中删除第一个值与value相等的记录
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dic">字典</param>
        /// <param name="value">值</param>
        public static void RemoveFirstValue<TKey, TValue>(this IDictionary<TKey, TValue> dic, TValue value) where TKey : class where TValue : IEquatable<TValue>
        {
            TKey dest = dic.FindFirstKeyByValue(value);
            if (dic.ContainsKey(dest))
            {
                dic.Remove(dest);
            }
        }

        /// <summary>
        /// Search 返回的是目标值所在的索引，如果不存在则返回-1
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="value">值</param>
        /// <returns>索引</returns>
        public static int Search<T>(IList<T> list, T value) where T : IComparable
        {
            int left = 0;
            int right = list.Count;
            while (right >= left)
            {
                int middle = (left + right) / 2;
                if (list[middle].CompareTo(value) == 0)
                {
                    return middle;
                }

                if (list[middle].CompareTo(value) > 0)
                {
                    right = middle - 1;
                }
                else
                {
                    left = middle + 1;
                }
            }

            return -1;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="list">List</param>
        /// <param name="index">序号</param>
        /// <returns>值</returns>
        public static T Get<T>(this List<T> list, int index)
        {
            return list.Exist(index) ? list[index] : default;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="list">List</param>
        /// <param name="index">序号</param>
        /// <param name="value">值</param>
        public static void Update<T>(this List<T> list, int index, T value)
        {
            if (!list.Exist(index))
            {
                return;
            }

            list[index] = value;
        }

        /// <summary>
        /// 索引是否存在
        /// </summary>
        /// <param name="list">List</param>
        /// <param name="index">序号</param>
        /// <returns>结果</returns>
        public static bool Exist<T>(this List<T> list, int index)
        {
            return (index >= 0 && index < list.Count);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        public static bool IsEmpty<T>(this List<T> list)
        {
            return list.Count == 0;
        }

        /// <summary>
        /// 第一个
        /// </summary>
        public static T First<T>(this List<T> list)
        {
            return list.Get(0);
        }

        /// <summary>
        /// 最后一个
        /// </summary>
        public static T Last<T>(this List<T> list)
        {
            return list.Get(list.Count - 1);
        }
    }
}