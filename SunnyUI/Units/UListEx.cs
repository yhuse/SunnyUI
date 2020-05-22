using System;
using System.Collections.Generic;

namespace Sunny.UI
{
    /// <summary>
    /// 支持事件的List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListEx<T> : List<T> where T : new()
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