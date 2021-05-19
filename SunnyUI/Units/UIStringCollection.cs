/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2021 ShenYongHua(沈永华).
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
 * 文件名称: UIStringCollection.cs
 * 文件说明: 带集合个数改变事件的字符串集合类
 * 当前版本: V3.0
 * 创建日期: 2021-05-19
 *
 * 2021-05-19: V3.0.3 增加文件说明
******************************************************************************/

using System;
using System.Collections;

namespace Sunny.UI
{
    /// <devdoc>
    ///    <para>Represents a collection of strings.</para>
    /// </devdoc>
    [Serializable]
    public class UIStringCollection : IList
    {
        private ArrayList data = new ArrayList();

        public event EventHandler CountChange;

        /// <devdoc>
        /// <para>Represents the entry at the specified index of the <see cref='System.Collections.Specialized.StringCollection'/>.</para>
        /// </devdoc>
        public string this[int index]
        {
            get => (string)data[index];
            set => data[index] = value;
        }

        /// <devdoc>
        ///    <para>Gets the number of strings in the 
        ///    <see cref='System.Collections.Specialized.StringCollection'/> .</para>
        /// </devdoc>
        public int Count => data.Count;

        bool IList.IsReadOnly => false;

        bool IList.IsFixedSize => false;


        /// <devdoc>
        ///    <para>Adds a string with the specified value to the 
        ///    <see cref='System.Collections.Specialized.StringCollection'/> .</para>
        /// </devdoc>
        public int Add(string value)
        {
            int count = data.Add(value);
            CountChange?.Invoke(this, new EventArgs());
            return count;
        }

        /// <devdoc>
        /// <para>Copies the elements of a string array to the end of the <see cref='System.Collections.Specialized.StringCollection'/>.</para>
        /// </devdoc>
        public void AddRange(string[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            data.AddRange(value);
            CountChange?.Invoke(this, new EventArgs());
        }

        /// <devdoc>
        ///    <para>Removes all the strings from the 
        ///    <see cref='System.Collections.Specialized.StringCollection'/> .</para>
        /// </devdoc>
        public void Clear()
        {
            data.Clear();
            CountChange?.Invoke(this, new EventArgs());
        }

        /// <devdoc>
        ///    <para>Gets a value indicating whether the 
        ///    <see cref='System.Collections.Specialized.StringCollection'/> contains a string with the specified 
        ///       value.</para>
        /// </devdoc>
        public bool Contains(string value)
        {
            return data.Contains(value);
        }

        /// <devdoc>
        /// <para>Copies the <see cref='System.Collections.Specialized.StringCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </devdoc>
        public void CopyTo(string[] array, int index)
        {
            data.CopyTo(array, index);
        }

        /// <devdoc>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='System.Collections.Specialized.StringCollection'/> .</para>
        /// </devdoc>
        public UIStringEnumerator GetEnumerator()
        {
            return new UIStringEnumerator(this);
        }

        /// <devdoc>
        ///    <para>Returns the index of the first occurrence of a string in 
        ///       the <see cref='System.Collections.Specialized.StringCollection'/> .</para>
        /// </devdoc>
        public int IndexOf(string value)
        {
            return data.IndexOf(value);
        }

        /// <devdoc>
        /// <para>Inserts a string into the <see cref='System.Collections.Specialized.StringCollection'/> at the specified 
        ///    index.</para>
        /// </devdoc>
        public void Insert(int index, string value)
        {
            data.Insert(index, value);
            CountChange?.Invoke(this, new EventArgs());
        }

        /// <devdoc>
        /// <para>Gets a value indicating whether the <see cref='System.Collections.Specialized.StringCollection'/> is read-only.</para>
        /// </devdoc>
        public bool IsReadOnly => false;

        /// <devdoc>
        ///    <para>Gets a value indicating whether access to the 
        ///    <see cref='System.Collections.Specialized.StringCollection'/> 
        ///    is synchronized (thread-safe).</para>
        /// </devdoc>
        public bool IsSynchronized => false;

        /// <devdoc>
        ///    <para> Removes a specific string from the 
        ///    <see cref='System.Collections.Specialized.StringCollection'/> .</para>
        /// </devdoc>
        public void Remove(string value)
        {
            data.Remove(value);
            CountChange?.Invoke(this, new EventArgs());
        }

        /// <devdoc>
        /// <para>Removes the string at the specified index of the <see cref='System.Collections.Specialized.StringCollection'/>.</para>
        /// </devdoc>
        public void RemoveAt(int index)
        {
            data.RemoveAt(index);
            CountChange?.Invoke(this, new EventArgs());
        }

        /// <devdoc>
        /// <para>Gets an object that can be used to synchronize access to the <see cref='System.Collections.Specialized.StringCollection'/>.</para>
        /// </devdoc>
        public object SyncRoot => data.SyncRoot;

        object IList.this[int index]
        {
            get => this[index];
            set => this[index] = (string)value;
        }

        int IList.Add(object value)
        {
            int count = Add((string)value);
            CountChange?.Invoke(this, new EventArgs());
            return count;
        }

        bool IList.Contains(object value)
        {
            return Contains((string)value);
        }


        int IList.IndexOf(object value)
        {
            return IndexOf((string)value);
        }

        void IList.Insert(int index, object value)
        {
            Insert(index, (string)value);
            CountChange?.Invoke(this, new EventArgs());
        }

        void IList.Remove(object value)
        {
            Remove((string)value);
            CountChange?.Invoke(this, new EventArgs());
        }

        void ICollection.CopyTo(Array array, int index)
        {
            data.CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }
    }

    /// <devdoc>
    ///    <para>[To be supplied.]</para>
    /// </devdoc>
    public class UIStringEnumerator
    {
        private readonly IEnumerator baseEnumerator;
        private readonly IEnumerable temp;

        internal UIStringEnumerator(UIStringCollection mappings)
        {
            this.temp = (IEnumerable)(mappings);
            this.baseEnumerator = temp.GetEnumerator();
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public string Current => (string)(baseEnumerator.Current);

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public bool MoveNext()
        {
            return baseEnumerator.MoveNext();
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public void Reset()
        {
            baseEnumerator.Reset();
        }

    }
}