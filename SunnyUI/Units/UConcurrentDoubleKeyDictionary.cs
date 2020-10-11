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
 * 文件名称: UConcurrentDoubleKeyDictionary.cs
 * 文件说明: 双主键线程安全字典，适用场景主键和键值都唯一，可双向查找
 * 当前版本: V2.2
 * 创建日期: 2020-08-17
 *
 * 2020-08-17: V2.2.7 增加文件说明
******************************************************************************/

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Sunny.UI
{
    public class ConcurrentDoubleKeyDictionary<TKey1, TKey2>
    {
        private readonly ConcurrentDictionary<TKey1, TKey2> Key1 = new ConcurrentDictionary<TKey1, TKey2>();
        private readonly ConcurrentDictionary<TKey2, TKey1> Key2 = new ConcurrentDictionary<TKey2, TKey1>();

        public void TryAdd(TKey1 key1, TKey2 key2)
        {
            Key1.TryAddOrUpdate(key1, key2);
            Key2.TryAddOrUpdate(key2, key1);
        }

        public TKey2 this[TKey1 key1]
        {
            get => GetKey2(key1);
            set => TryAdd(key1, value);
        }

        public int Count => Key1.Count;

        public void RemoveByKey1(TKey1 key1)
        {
            if (ContainsKey1(key1))
            {
                TKey2 key2 = GetKey2(key1);
                Key1.TryRemove(key1, out _);
                if (ContainsKey2(key2)) Key2.TryRemove(key2, out _);
            }
        }

        public void RemoveByKey2(TKey2 key2)
        {
            if (ContainsKey2(key2))
            {
                TKey1 key1 = GetKey1(key2);
                Key2.TryRemove(key2, out _);
                if (ContainsKey1(key1)) Key1.TryRemove(key1, out _);
            }
        }

        public TKey2 GetKey2(TKey1 key1)
        {
            return Key1.ContainsKey(key1) ? Key1[key1] : default(TKey2);
        }

        public TKey1 GetKey1(TKey2 key2)
        {
            return Key2.ContainsKey(key2) ? Key2[key2] : default(TKey1);
        }

        public bool ContainsKey1(TKey1 key1)
        {
            return Key1.ContainsKey(key1);
        }

        public bool ContainsKey2(TKey2 key2)
        {
            return Key2.ContainsKey(key2);
        }

        public IList<TKey1> Key1List()
        {
            return Key1.Keys.ToList();
        }

        public IList<TKey2> Key2List()
        {
            return Key2.Keys.ToList();
        }

        public void Clear()
        {
            Key1.Clear();
            Key2.Clear();
        }
    }
}