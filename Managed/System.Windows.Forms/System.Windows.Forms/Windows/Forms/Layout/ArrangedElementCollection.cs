using System;
using System.Collections;

namespace System.Windows.Forms.Layout
{
	/// <summary>Represents a collection of objects.</summary>
	// Token: 0x0200049C RID: 1180
	public class ArrangedElementCollection : ICollection, IEnumerable, IList
	{
		// Token: 0x06004B59 RID: 19289 RVA: 0x00128538 File Offset: 0x00126738
		internal ArrangedElementCollection()
		{
			this.list = new ArrayList();
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Add(System.Object)" /> method.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06004B5A RID: 19290 RVA: 0x0012854C File Offset: 0x0012674C
		int IList.Add(object value)
		{
			return this.Add(value);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Clear" /> method.</summary>
		// Token: 0x06004B5B RID: 19291 RVA: 0x00128558 File Offset: 0x00126758
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Contains(System.Object)" /> method.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06004B5C RID: 19292 RVA: 0x00128560 File Offset: 0x00126760
		bool IList.Contains(object value)
		{
			return this.Contains(value);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.IndexOf(System.Object)" /> method.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06004B5D RID: 19293 RVA: 0x0012856C File Offset: 0x0012676C
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06004B5E RID: 19294 RVA: 0x00128578 File Offset: 0x00126778
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Collections.IList.IsFixedSize" /> property.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x06004B5F RID: 19295 RVA: 0x00128580 File Offset: 0x00126780
		bool IList.IsFixedSize
		{
			get
			{
				return this.IsFixedSize;
			}
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Remove(System.Object)" /> method.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06004B60 RID: 19296 RVA: 0x00128588 File Offset: 0x00126788
		void IList.Remove(object value)
		{
			this.Remove(value);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" /> method.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		// Token: 0x06004B61 RID: 19297 RVA: 0x00128594 File Offset: 0x00126794
		void IList.RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Collections.IList.Item(System.Int32)" /> property.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get.</param>
		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x06004B62 RID: 19298 RVA: 0x001285A4 File Offset: 0x001267A4
		// (set) Token: 0x06004B63 RID: 19299 RVA: 0x001285B0 File Offset: 0x001267B0
		object IList.Item
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = value;
			}
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Collections.ICollection.IsSynchronized" /> property.</summary>
		/// <returns>true if access to the <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x06004B64 RID: 19300 RVA: 0x001285BC File Offset: 0x001267BC
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Collections.ICollection.SyncRoot" /> property.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" />.</returns>
		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x06004B65 RID: 19301 RVA: 0x001285CC File Offset: 0x001267CC
		object ICollection.SyncRoot
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements currently contained in the collection.</returns>
		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x06004B66 RID: 19302 RVA: 0x001285E0 File Offset: 0x001267E0
		public virtual int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x06004B67 RID: 19303 RVA: 0x001285F0 File Offset: 0x001267F0
		public virtual bool IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		/// <summary>Copies the entire contents of this collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source collection is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source element cannot be cast automatically to the type of <paramref name="array" />.</exception>
		// Token: 0x06004B68 RID: 19304 RVA: 0x00128600 File Offset: 0x00126800
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Determines whether two <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" /> instances are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" /> is equal to the current <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" /> to compare with the current <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" />.</param>
		// Token: 0x06004B69 RID: 19305 RVA: 0x00128610 File Offset: 0x00126810
		public override bool Equals(object obj)
		{
			return obj is ArrangedElementCollection && this == obj;
		}

		/// <summary>Returns an enumerator for the entire collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the entire collection.</returns>
		// Token: 0x06004B6A RID: 19306 RVA: 0x00128628 File Offset: 0x00126828
		public virtual IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" />.</returns>
		// Token: 0x06004B6B RID: 19307 RVA: 0x00128638 File Offset: 0x00126838
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x00128640 File Offset: 0x00126840
		internal int Add(object value)
		{
			return this.list.Add(value);
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x00128650 File Offset: 0x00126850
		internal void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x00128660 File Offset: 0x00126860
		internal bool Contains(object value)
		{
			return this.list.Contains(value);
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x00128670 File Offset: 0x00126870
		internal int IndexOf(object value)
		{
			return this.list.IndexOf(value);
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x00128680 File Offset: 0x00126880
		internal void Insert(int index, object value)
		{
			this.list.Insert(index, value);
		}

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x06004B71 RID: 19313 RVA: 0x00128690 File Offset: 0x00126890
		internal bool IsFixedSize
		{
			get
			{
				return this.list.IsFixedSize;
			}
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x001286A0 File Offset: 0x001268A0
		internal void Remove(object value)
		{
			this.list.Remove(value);
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x001286B0 File Offset: 0x001268B0
		internal void InternalRemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x17001310 RID: 4880
		internal object this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x04002858 RID: 10328
		internal ArrayList list;
	}
}
