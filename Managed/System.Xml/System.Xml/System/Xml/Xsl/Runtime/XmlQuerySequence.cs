using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000610 RID: 1552
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class XmlQuerySequence<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		// Token: 0x06003CCA RID: 15562 RVA: 0x00152647 File Offset: 0x00150847
		public static XmlQuerySequence<T> CreateOrReuse(XmlQuerySequence<T> seq)
		{
			if (seq != null)
			{
				seq.Clear();
				return seq;
			}
			return new XmlQuerySequence<T>();
		}

		// Token: 0x06003CCB RID: 15563 RVA: 0x00152659 File Offset: 0x00150859
		public static XmlQuerySequence<T> CreateOrReuse(XmlQuerySequence<T> seq, T item)
		{
			if (seq != null)
			{
				seq.Clear();
				seq.Add(item);
				return seq;
			}
			return new XmlQuerySequence<T>(item);
		}

		// Token: 0x06003CCC RID: 15564 RVA: 0x00152673 File Offset: 0x00150873
		public XmlQuerySequence()
		{
			this.items = new T[16];
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x00152688 File Offset: 0x00150888
		public XmlQuerySequence(int capacity)
		{
			this.items = new T[capacity];
		}

		// Token: 0x06003CCE RID: 15566 RVA: 0x0015269C File Offset: 0x0015089C
		public XmlQuerySequence(T[] array, int size)
		{
			this.items = array;
			this.size = size;
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x001526B2 File Offset: 0x001508B2
		public XmlQuerySequence(T value)
		{
			this.items = new T[1];
			this.items[0] = value;
			this.size = 1;
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x0011C6FB File Offset: 0x0011A8FB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new IListEnumerator<T>(this);
		}

		// Token: 0x06003CD1 RID: 15569 RVA: 0x0011C6FB File Offset: 0x0011A8FB
		public IEnumerator<T> GetEnumerator()
		{
			return new IListEnumerator<T>(this);
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06003CD2 RID: 15570 RVA: 0x001526DA File Offset: 0x001508DA
		public int Count
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06003CD3 RID: 15571 RVA: 0x0000226C File Offset: 0x0000046C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06003CD4 RID: 15572 RVA: 0x00002068 File Offset: 0x00000268
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x001526E2 File Offset: 0x001508E2
		void ICollection.CopyTo(Array array, int index)
		{
			if (this.size == 0)
			{
				return;
			}
			Array.Copy(this.items, 0, array, index, this.size);
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06003CD6 RID: 15574 RVA: 0x00003242 File Offset: 0x00001442
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x00010C4A File Offset: 0x0000EE4A
		void ICollection<T>.Add(T value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x00010C4A File Offset: 0x0000EE4A
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x00152701 File Offset: 0x00150901
		public bool Contains(T value)
		{
			return this.IndexOf(value) != -1;
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x00152710 File Offset: 0x00150910
		public void CopyTo(T[] array, int index)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array[index + i] = this[i];
			}
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x00010C4A File Offset: 0x0000EE4A
		bool ICollection<T>.Remove(T value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06003CDC RID: 15580 RVA: 0x00003242 File Offset: 0x00001442
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06003CDD RID: 15581 RVA: 0x00003242 File Offset: 0x00001442
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C59 RID: 3161
		object IList.this[int index]
		{
			get
			{
				if (index >= this.size)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.items[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x00010C4A File Offset: 0x0000EE4A
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x00010C4A File Offset: 0x0000EE4A
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x00152765 File Offset: 0x00150965
		bool IList.Contains(object value)
		{
			return this.Contains((T)((object)value));
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x00152773 File Offset: 0x00150973
		int IList.IndexOf(object value)
		{
			return this.IndexOf((T)((object)value));
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x00010C4A File Offset: 0x0000EE4A
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003CE5 RID: 15589 RVA: 0x00010C4A File Offset: 0x0000EE4A
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x00010C4A File Offset: 0x0000EE4A
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C5A RID: 3162
		public T this[int index]
		{
			get
			{
				if (index >= this.size)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.items[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x001527A4 File Offset: 0x001509A4
		public int IndexOf(T value)
		{
			int num = Array.IndexOf<T>(this.items, value);
			if (num >= this.size)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x00010C4A File Offset: 0x0000EE4A
		void IList<T>.Insert(int index, T value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x00010C4A File Offset: 0x0000EE4A
		void IList<T>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x001527CA File Offset: 0x001509CA
		public void Clear()
		{
			this.size = 0;
			this.OnItemsChanged();
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x001527DC File Offset: 0x001509DC
		public void Add(T value)
		{
			this.EnsureCache();
			T[] array = this.items;
			int num = this.size;
			this.size = num + 1;
			array[num] = value;
			this.OnItemsChanged();
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x00152812 File Offset: 0x00150A12
		public void SortByKeys(Array keys)
		{
			if (this.size <= 1)
			{
				return;
			}
			Array.Sort(keys, this.items, 0, this.size);
			this.OnItemsChanged();
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x00152838 File Offset: 0x00150A38
		private void EnsureCache()
		{
			if (this.size >= this.items.Length)
			{
				T[] array = new T[this.size * 2];
				this.CopyTo(array, 0);
				this.items = array;
			}
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x00002F50 File Offset: 0x00001150
		protected virtual void OnItemsChanged()
		{
		}

		// Token: 0x040027B1 RID: 10161
		public static readonly XmlQuerySequence<T> Empty = new XmlQuerySequence<T>();

		// Token: 0x040027B2 RID: 10162
		private static readonly Type XPathItemType = typeof(XPathItem);

		// Token: 0x040027B3 RID: 10163
		private T[] items;

		// Token: 0x040027B4 RID: 10164
		private int size;

		// Token: 0x040027B5 RID: 10165
		private const int DefaultCacheSize = 16;
	}
}
