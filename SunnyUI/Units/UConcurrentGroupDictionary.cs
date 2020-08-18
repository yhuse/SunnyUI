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
 * 文件名称: UConcurrentGroupDictionary.cs
 * 文件说明: 分组线程安全字典
 * 当前版本: V2.2
 * 创建日期: 2020-08-18
 *
 * 2020-08-18: V2.2.7 增加文件说明
******************************************************************************/

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Sunny.UI
{
    public class ConcurrentGroupDictionary<TGroup, TKey, TValue>
    {
        private readonly ConcurrentDictionary<TGroup, ConcurrentDictionary<TKey, TValue>> GroupDictionary
            = new ConcurrentDictionary<TGroup, ConcurrentDictionary<TKey, TValue>>();
        private readonly ConcurrentDictionary<TKey, TValue> ObjectDictionary
            = new ConcurrentDictionary<TKey, TValue>();

        public ConcurrentDictionary<TKey, TValue> this[TGroup group]
        {
            get => ContainsGroup(group) ? GroupDictionary[group] : new ConcurrentDictionary<TKey, TValue>();
            set
            {
                if (!ContainsGroup(group)) TryAdd(group);
                GroupDictionary[group] = value;
            }
        }

        public bool ContainsGroup(TGroup group)
        {
            return GroupDictionary.ContainsKey(group);
        }

        public bool ContainsKey(TKey key)
        {
            return ObjectDictionary.ContainsKey(key);
        }

        public bool ContainsKey(TGroup group, TKey key)
        {
            return ContainsGroup(group) && GroupDictionary[group].ContainsKey(key);
        }

        public void TryAdd(TGroup group)
        {
            if (!GroupDictionary.ContainsKey(group))
                GroupDictionary.TryAdd(group, new ConcurrentDictionary<TKey, TValue>());
        }

        public void TryAdd(TGroup group, TKey key, TValue value)
        {
            TryAdd(group);

            if (GroupDictionary[group].ContainsKey(key))
                GroupDictionary[group][key] = value;
            else
                GroupDictionary[group].TryAdd(key, value);

            if (ObjectDictionary.ContainsKey(key))
                ObjectDictionary[key] = value;
            else
                ObjectDictionary.TryAdd(key, value);
        }

        public void TryRemove(TGroup group, TKey key, TValue value)
        {
            if (GroupDictionary.ContainsKey(group))
            {
                if (GroupDictionary[group].ContainsKey(key)) GroupDictionary[group].TryRemove(key, out _);
                if (GroupDictionary[group].Count == 0) GroupDictionary.TryRemove(group, out _);
            }

            if (ObjectDictionary.ContainsKey(key))
            {
                ObjectDictionary.TryRemove(key, out _);
            }
        }

        public void ClearAll()
        {
            List<TGroup> groups = GroupDictionary.Keys.ToList();
            foreach (var group in groups)
            {
                ClearGroup(group);
            }

            GroupDictionary.Clear();
            ObjectDictionary.Clear();
        }

        public void ClearGroup(TGroup group)
        {
            if (GroupDictionary.ContainsKey(group))
            {
                List<TKey> keys = GroupDictionary[group].Keys.ToList();
                foreach (var key in keys)
                {
                    if (ObjectDictionary.ContainsKey(key))
                        ObjectDictionary.TryRemove(key, out _);
                }

                GroupDictionary[group].Clear();
            }
        }

        public List<TValue> GetValues(TGroup group)
        {
            if (!this.GroupDictionary.ContainsKey(group))
            {
                return new List<TValue>();
            }

            return GroupDictionary[group].Values.ToList();
        }

        public List<TGroup> Groups()
        {
            return GroupDictionary.Keys.ToList();
        }

        public List<TValue> AllValues()
        {
            return ObjectDictionary.Values.ToList();
        }

        public List<TKey> AllKeys()
        {
            return ObjectDictionary.Keys.ToList();
        }

        public List<TKey> GroupKeys(TGroup group)
        {
            return ContainsGroup(group) ? GroupDictionary[group].Keys.ToList() : new List<TKey>();
        }

        public List<TValue> GroupValues(TGroup group)
        {
            return ContainsGroup(group) ? GroupDictionary[group].Values.ToList() : new List<TValue>();
        }

        public TValue GetValue(TKey key)
        {
            return ObjectDictionary.ContainsKey(key) ? ObjectDictionary[key] : default;
        }

        public int Count() => ObjectDictionary.Count;

        public int Count(TGroup group)
        {
            return ContainsGroup(group) ? GroupDictionary[group].Count : 0;
        }
    }
}
