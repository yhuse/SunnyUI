using System;
using System.Collections;

namespace Sunny.UI
{
    [Serializable]
    public class UIObjectCollection : IList
    {
        private ArrayList data = new ArrayList();

        public event EventHandler CountChange;

        public object this[int index]
        {
            get => data[index];
            set => data[index] = value;
        }

        public int Count => data.Count;

        bool IList.IsReadOnly => false;

        bool IList.IsFixedSize => false;

        public int Add(object value)
        {
            int count = data.Add(value);
            CountChange?.Invoke(this, new EventArgs());
            return count;
        }

        public void AddRange(object[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value is null");
            }

            data.AddRange(value);
            CountChange?.Invoke(this, new EventArgs());
        }

        public void Clear()
        {
            data.Clear();
            CountChange?.Invoke(this, new EventArgs());
        }

        public bool Contains(object value)
        {
            return data.Contains(value);
        }

        public void CopyTo(object[] array, int index)
        {
            data.CopyTo(array, index);
        }

        public UIObjectEnumerator GetEnumerator()
        {
            return new UIObjectEnumerator(this);
        }

        public int IndexOf(object value)
        {
            return data.IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            data.Insert(index, value);
            CountChange?.Invoke(this, new EventArgs());
        }

        public bool IsReadOnly => false;

        public bool IsSynchronized => false;

        public void Remove(object value)
        {
            data.Remove(value);
            CountChange?.Invoke(this, new EventArgs());
        }

        public void RemoveAt(int index)
        {
            data.RemoveAt(index);
            CountChange?.Invoke(this, new EventArgs());
        }

        public object SyncRoot => data.SyncRoot;

        object IList.this[int index]
        {
            get => this[index];
            set => this[index] = value;
        }

        int IList.Add(object value)
        {
            int count = Add(value);
            CountChange?.Invoke(this, new EventArgs());
            return count;
        }

        bool IList.Contains(object value)
        {
            return Contains(value);
        }


        int IList.IndexOf(object value)
        {
            return IndexOf(value);
        }

        void IList.Insert(int index, object value)
        {
            Insert(index, value);
            CountChange?.Invoke(this, new EventArgs());
        }

        void IList.Remove(object value)
        {
            Remove(value);
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
    public class UIObjectEnumerator
    {
        private readonly IEnumerator baseEnumerator;
        private readonly IEnumerable temp;

        internal UIObjectEnumerator(UIObjectCollection mappings)
        {
            this.temp = (IEnumerable)(mappings);
            this.baseEnumerator = temp.GetEnumerator();
        }

        public object Current => baseEnumerator.Current;

        public bool MoveNext()
        {
            return baseEnumerator.MoveNext();
        }

        public void Reset()
        {
            baseEnumerator.Reset();
        }

    }
}
