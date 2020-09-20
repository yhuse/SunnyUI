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
        private readonly ConcurrentDictionary<TGroup, ConcurrentDictionary<TKey, TValue>> Dictionary
            = new ConcurrentDictionary<TGroup, ConcurrentDictionary<TKey, TValue>>();

        public ConcurrentDictionary<TKey, TValue> this[TGroup group]
        {
            get => ContainsGroup(group) ? Dictionary[group] : new ConcurrentDictionary<TKey, TValue>();
            set
            {
                if (!ContainsGroup(group)) TryAdd(group);
                Dictionary[group] = value;
            }
        }

        public bool ContainsGroup(TGroup group)
        {
            return Dictionary.ContainsKey(group);
        }

        public bool ContainsKey(TKey key)
        {
            return Groups.Any(group => ContainsKey(group, key));
        }

        public bool ContainsKey(TGroup group, TKey key)
        {
            return ContainsGroup(group) && Dictionary[group].ContainsKey(key);
        }

        public void TryAdd(TGroup group)
        {
            if (!Dictionary.ContainsKey(group))
                Dictionary.TryAdd(group, new ConcurrentDictionary<TKey, TValue>());
        }

        public void TryAdd(TGroup group, TKey key, TValue value)
        {
            TryAdd(group);

            if (ContainsKey(group, key))
                Dictionary[group][key] = value;
            else
                Dictionary[group].TryAdd(key, value);
        }

        public bool TryUpdate(TGroup group, TKey key, TValue value)
        {
            if (!ContainsGroup(group)) return false;
            TryAdd(group, key, value);
            return true;
        }

        public void TryRemove(TGroup group, TKey key)
        {
            if (ContainsGroup(group))
            {
                if (ContainsKey(group, key)) Dictionary[group].TryRemove(key, out _);
                if (GroupItemsCount(group) == 0) Dictionary.TryRemove(group, out _);
            }
        }

        public void TryRemove(TGroup group)
        {
            ClearGroup(group);
            Dictionary.TryRemove(group, out _);
        }

        public void Clear()
        {
            foreach (var group in Groups)
            {
                ClearGroup(group);
            }

            Dictionary.Clear();
        }

        public void ClearGroup(TGroup group)
        {
            if (ContainsGroup(group))
            {
                Dictionary[group].Clear();
            }
        }

        public List<TValue> Values(TGroup group)
        {
            return ContainsGroup(group) ? Dictionary[group].Values.ToList() : new List<TValue>();
        }

        public List<TGroup> Groups
        {
            get => Dictionary.Keys.ToList();
        }

        public List<TGroup> SortedGroups
        {
            get
            {
                List<TGroup> groups = Groups;
                groups.Sort();
                return groups;
            }
        }

        public List<TKey> Keys(TGroup group)
        {
            return ContainsGroup(group) ? Dictionary[group].Keys.ToList() : new List<TKey>();
        }

        public List<TKey> SortedKeys(TGroup group)
        {
            List<TKey> keys = Keys(group);
            keys.Sort();
            return keys;
        }

        public List<TValue> SortedValues(TGroup group)
        {
            List<TValue> values = new List<TValue>();
            if (ContainsGroup(group))
            {
                var keys = SortedKeys(group);
                foreach (var key in keys)
                {
                    values.Add(Dictionary[group][key]);
                }
            }

            return values;
        }

        public TValue GetValue(TGroup group, TKey key)
        {
            return ContainsKey(group, key) ? Dictionary[group][key] : default;
        }

        public int AllCount
        {
            get => Groups.Sum(GroupItemsCount);
        }

        public int GroupCount
        {
            get => Dictionary.Count;
        }

        public int GroupItemsCount(TGroup group)
        {
            return ContainsGroup(group) ? Dictionary[group].Count : 0;
        }
    }
}
