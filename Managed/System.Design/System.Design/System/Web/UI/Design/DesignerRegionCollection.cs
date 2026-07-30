using System;
using System.Collections;

namespace System.Web.UI.Design
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.Design.DesignerRegion" /> objects within a control designer.</summary>
	// Token: 0x02000076 RID: 118
	public class DesignerRegionCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" /> class.</summary>
		// Token: 0x060003BF RID: 959 RVA: 0x00002364 File Offset: 0x00000564
		[MonoNotSupported("")]
		public DesignerRegionCollection()
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" /> class for the specified control designer.</summary>
		/// <param name="owner">The control designer that owns this collection of designer regions.</param>
		// Token: 0x060003C0 RID: 960 RVA: 0x00002364 File Offset: 0x00000564
		[MonoNotSupported("")]
		public DesignerRegionCollection(ControlDesigner owner)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.Design.DesignerRegion" /> object to the end of the collection.</summary>
		/// <returns>The index at which the region was added to the collection.</returns>
		/// <param name="region">The <see cref="T:System.Web.UI.Design.DesignerRegion" /> to add to the collection.</param>
		// Token: 0x060003C1 RID: 961 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public int Add(DesignerRegion region)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all regions from the collection.</summary>
		// Token: 0x060003C2 RID: 962 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public void Clear()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the specified region is contained within the collection.</summary>
		/// <returns>true, if the region is in the collection; otherwise, false.</returns>
		/// <param name="region">The <see cref="T:System.Web.UI.Design.DesignerRegion" /> to locate within the collection.</param>
		// Token: 0x060003C3 RID: 963 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public bool Contains(DesignerRegion region)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" /> object, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> that is the destination of the copied regions. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060003C4 RID: 964 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060003C5 RID: 965 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Web.UI.Design.DesignerRegion" /> object within the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="region" /> within the collection; otherwise, -1, if <paramref name="region" /> is not in the collection.</returns>
		/// <param name="region">The <see cref="T:System.Web.UI.Design.DesignerRegion" /> to locate within the collection.</param>
		// Token: 0x060003C6 RID: 966 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public int IndexOf(DesignerRegion region)
		{
			throw new NotImplementedException();
		}

		/// <summary>Inserts a <see cref="T:System.Web.UI.Design.DesignerRegion" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index within the collection at which to insert the region.</param>
		/// <param name="region">The <see cref="T:System.Web.UI.Design.DesignerRegion" /> to insert into the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.- or -<paramref name="index" /> is greater than the <see cref="P:System.Web.UI.Design.DesignerRegionCollection.Count" /> property.</exception>
		// Token: 0x060003C7 RID: 967 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public void Insert(int index, DesignerRegion region)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.Design.DesignerRegion" /> object from the collection. </summary>
		/// <param name="region">The <see cref="T:System.Web.UI.Design.DesignerRegion" /> to remove from the collection. </param>
		// Token: 0x060003C8 RID: 968 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public void Remove(DesignerRegion region)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.Design.DesignerRegion" /> object at the specified index within the collection.</summary>
		/// <param name="index">The zero-based index within the collection of the <see cref="T:System.Web.UI.Design.DesignerRegion" /> to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.- or -<paramref name="index" /> is greater than the <see cref="P:System.Web.UI.Design.DesignerRegionCollection.Count" /> property.</exception>
		// Token: 0x060003C9 RID: 969 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.Design.DesignerRegion" /> objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.Design.DesignerRegion" /> objects in the collection.</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public int Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" /> object has a fixed size.</summary>
		/// <returns>true, if the size of the collection cannot be changed by adding or removing items; otherwise, false.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public bool IsFixedSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" /> object is read-only.</summary>
		/// <returns>true, if the collection cannot be changed; otherwise, false.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" /> object is synchronized (thread safe).</summary>
		/// <returns>true, if access to the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" /> is synchronized; otherwise, false.</returns>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public bool IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Web.UI.Design.DesignerRegion" /> object at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Design.DesignerRegion" /> at the specified index in the collection.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.Design.DesignerRegion" /> to get or set in the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is less than zero.- or -<paramref name="value" /> is greater than the <see cref="P:System.Web.UI.Design.DesignerRegionCollection.Count" /> property.</exception>
		// Token: 0x170000DA RID: 218
		[MonoNotSupported("")]
		public DesignerRegion this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the control designer that owns the designer region collection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.ControlDesigner" /> that represents the control designer that owns the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" />.</returns>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public ControlDesigner Owner
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" />.</returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> that is the destination of the copied regions. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060003D2 RID: 978 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		void ICollection.CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060003D3 RID: 979 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <returns>The index at which the item was added to the collection.</returns>
		/// <param name="o">The item to add to the collection.</param>
		// Token: 0x060003D4 RID: 980 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		int IList.Add(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
		// Token: 0x060003D5 RID: 981 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		void IList.Clear()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <returns>true, if the region is in the collection; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Web.UI.Design.DesignerRegion" /> to locate within the collection.</param>
		// Token: 0x060003D6 RID: 982 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		bool IList.Contains(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <returns>The zero-based index of the first occurrence of the object within the collection; otherwise, -1, if the object is not in the collection.</returns>
		/// <param name="o">The <see cref="T:System.Web.UI.Design.DesignerRegion" /> to locate within the collection.</param>
		// Token: 0x060003D7 RID: 983 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		int IList.IndexOf(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">The zero-based index within the collection at which to insert the object.</param>
		/// <param name="o">The object to insert into the collection.</param>
		// Token: 0x060003D8 RID: 984 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		void IList.Insert(int index, object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="o">The object to remove from the collection.</param>
		// Token: 0x060003D9 RID: 985 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		void IList.Remove(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
		/// <param name="index">The zero-based index within the collection of the object to remove.</param>
		// Token: 0x060003DA RID: 986 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		void IList.RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		int ICollection.Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>true, if access to the collection is synchronized; otherwise, false.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		bool ICollection.IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>false, if the collection dynamically increases in size as new objects are added; otherwise, true.</returns>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		bool IList.IsFixedSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>The value of the <see cref="P:System.Web.UI.Design.DesignerRegionCollection.IsReadOnly" /> property.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		bool IList.IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <returns>The object at the specified index in the collection.</returns>
		/// <param name="index">The zero-based index of the object to get in the collection.</param>
		// Token: 0x170000E2 RID: 226
		[MonoNotSupported("")]
		object IList.this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
