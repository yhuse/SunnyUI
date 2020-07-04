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
 * 文件名称: UListEx.cs
 * 文件说明: 带增加、删除事件的List列表
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;

namespace Sunny.UI
{
    /// <summary>
    /// 支持事件的List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListEx<T> : List<T>
    {
        public delegate void OnListChange(object sender, T item, int index);

        /// <summary>
        /// 删除事件
        /// </summary>
        public event OnListChange OnDelete;

        /// <summary>
        /// 添加事件
        /// </summary>
        public event OnListChange OnAppend;

        public new void Add(T item)
        {
            base.Add(item);
            OnAppend?.Invoke(this, item, this.Count);
        }

        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            OnAppend?.Invoke(this, item, index);
        }

        public new void Remove(T item)
        {
            int index = IndexOf(item);
            base.Remove(item);
            OnDelete?.Invoke(this, item, index);
        }

        public new void RemoveAt(int index)
        {
            T item = base[index];
            base.RemoveAt(index);
            OnDelete?.Invoke(this, item, index);
        }

        public new void RemoveRange(int index, int count)
        {
            for (int i = index; i < index + count; i++)
            {
                this.RemoveAt(i);
            }
        }

        public new void AddRange(IEnumerable<T> collection)
        {
            int Index = Count;
            base.AddRange(collection);
            foreach (var item in collection)
            {
                OnAppend?.Invoke(this, item, Index);
                Index++;
            }
        }

        public new void InsertRange(int index, IEnumerable<T> collection)
        {
            base.InsertRange(index, collection);
            foreach (var item in collection)
            {
                OnAppend?.Invoke(this, item, index);
                index++;
            }
        }
    }

    public class NListEventArgs<T> : EventArgs
    {
        public NListEventArgs(T item, int index)
        {
            Item = item;
            Index = index;
        }

        public T Item { get; set; }
        public int Index { get; set; }
    }
}