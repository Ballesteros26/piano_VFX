using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Xsl
{
	// Token: 0x020004BB RID: 1211
	internal abstract class ListBase<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06003120 RID: 12576
		public abstract int Count { get; }

		// Token: 0x17000A5C RID: 2652
		public abstract T this[int index] { get; set; }

		// Token: 0x06003123 RID: 12579 RVA: 0x0011C61C File Offset: 0x0011A81C
		public virtual bool Contains(T value)
		{
			return this.IndexOf(value) != -1;
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x0011C62C File Offset: 0x0011A82C
		public virtual int IndexOf(T value)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (value.Equals(this[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x0011C668 File Offset: 0x0011A868
		public virtual void CopyTo(T[] array, int index)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array[index + i] = this[i];
			}
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x0011C696 File Offset: 0x0011A896
		public virtual IListEnumerator<T> GetEnumerator()
		{
			return new IListEnumerator<T>(this);
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06003127 RID: 12583 RVA: 0x00003242 File Offset: 0x00001442
		public virtual bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06003128 RID: 12584 RVA: 0x00003242 File Offset: 0x00001442
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x0011C69E File Offset: 0x0011A89E
		public virtual void Add(T value)
		{
			this.Insert(this.Count, value);
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual void Insert(int index, T value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x0011C6B0 File Offset: 0x0011A8B0
		public virtual bool Remove(T value)
		{
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x0011C6D4 File Offset: 0x0011A8D4
		public virtual void Clear()
		{
			for (int i = this.Count - 1; i >= 0; i--)
			{
				this.RemoveAt(i);
			}
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x0011C6FB File Offset: 0x0011A8FB
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new IListEnumerator<T>(this);
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x0011C6FB File Offset: 0x0011A8FB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new IListEnumerator<T>(this);
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06003130 RID: 12592 RVA: 0x0011C708 File Offset: 0x0011A908
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06003131 RID: 12593 RVA: 0x00002068 File Offset: 0x00000268
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x0011C710 File Offset: 0x0011A910
		void ICollection.CopyTo(Array array, int index)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index);
			}
		}

		// Token: 0x17000A61 RID: 2657
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (!ListBase<T>.IsCompatibleType(value.GetType()))
				{
					throw new ArgumentException(Res.GetString("Type is incompatible."), "value");
				}
				this[index] = (T)((object)value);
			}
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x0011C780 File Offset: 0x0011A980
		int IList.Add(object value)
		{
			if (!ListBase<T>.IsCompatibleType(value.GetType()))
			{
				throw new ArgumentException(Res.GetString("Type is incompatible."), "value");
			}
			this.Add((T)((object)value));
			return this.Count - 1;
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x0011C7B8 File Offset: 0x0011A9B8
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x0011C7C0 File Offset: 0x0011A9C0
		bool IList.Contains(object value)
		{
			return ListBase<T>.IsCompatibleType(value.GetType()) && this.Contains((T)((object)value));
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x0011C7DD File Offset: 0x0011A9DD
		int IList.IndexOf(object value)
		{
			if (!ListBase<T>.IsCompatibleType(value.GetType()))
			{
				return -1;
			}
			return this.IndexOf((T)((object)value));
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x0011C7FA File Offset: 0x0011A9FA
		void IList.Insert(int index, object value)
		{
			if (!ListBase<T>.IsCompatibleType(value.GetType()))
			{
				throw new ArgumentException(Res.GetString("Type is incompatible."), "value");
			}
			this.Insert(index, (T)((object)value));
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x0011C82B File Offset: 0x0011AA2B
		void IList.Remove(object value)
		{
			if (ListBase<T>.IsCompatibleType(value.GetType()))
			{
				this.Remove((T)((object)value));
			}
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x0011C847 File Offset: 0x0011AA47
		private static bool IsCompatibleType(object value)
		{
			return (value == null && !typeof(T).IsValueType) || value is T;
		}
	}
}
