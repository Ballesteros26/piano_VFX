using System;
using System.Collections;

namespace System.Web.UI.Design
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.Design.TemplateGroup" /> objects within a control designer. This class cannot be inherited.</summary>
	// Token: 0x020000A6 RID: 166
	public sealed class TemplateGroupCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateGroupCollection" /> class.</summary>
		// Token: 0x060004E8 RID: 1256 RVA: 0x00002364 File Offset: 0x00000564
		[MonoNotSupported("")]
		public TemplateGroupCollection()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.Design.TemplateGroup" /> objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.Design.TemplateGroup" /> objects in the collection.</returns>
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public int Count
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Web.UI.Design.TemplateGroup" /> object at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Design.TemplateGroup" /> at <paramref name="index" /> in the collection.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.Design.TemplateGroup" /> to get or set in the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is less than zero.- or -<paramref name="value" /> is greater than the <see cref="P:System.Web.UI.Design.TemplateGroupCollection.Count" /> property.</exception>
		// Token: 0x17000137 RID: 311
		[MonoNotSupported("")]
		public TemplateGroup this[int index]
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
			[MonoNotSupported("")]
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.Design.TemplateGroup" /> object to the end of the collection.</summary>
		/// <returns>The index at which the <see cref="T:System.Web.UI.Design.TemplateGroup" /> was added to the collection.</returns>
		/// <param name="group">The <see cref="T:System.Web.UI.Design.TemplateGroup" /> to add to the collection.</param>
		// Token: 0x060004EC RID: 1260 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public int Add(TemplateGroup group)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds the template groups in an existing <see cref="T:System.Web.UI.Design.TemplateGroupCollection" /> object to the current <see cref="T:System.Web.UI.Design.TemplateGroupCollection" /> object.</summary>
		/// <param name="groups">A <see cref="T:System.Web.UI.Design.TemplateGroupCollection" /> that contains the groups to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="groups" /> is null.</exception>
		// Token: 0x060004ED RID: 1261 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public void AddRange(TemplateGroupCollection groups)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all groups from the collection.</summary>
		// Token: 0x060004EE RID: 1262 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public void Clear()
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the specified group is contained within the collection.</summary>
		/// <returns>true if the <paramref name="group" /> is in the collection; otherwise, false.</returns>
		/// <param name="group">The <see cref="T:System.Web.UI.Design.TemplateGroup" /> to locate within the collection.</param>
		// Token: 0x060004EF RID: 1263 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public bool Contains(TemplateGroup group)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies the groups in the collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> that is the destination of the copied groups. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- <paramref name="index" /> is greater than or equal to the length of <paramref name="array" />.-or- The number of elements in the source <see cref="T:System.Web.UI.Design.TemplateGroupCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		// Token: 0x060004F0 RID: 1264 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public void CopyTo(TemplateGroup[] array, int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Web.UI.Design.TemplateGroup" /> object within the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="group" /> within the collection; otherwise, -1, if <paramref name="group" /> is not in the collection.</returns>
		/// <param name="group">The <see cref="T:System.Web.UI.Design.TemplateGroup" /> to locate within the collection.</param>
		// Token: 0x060004F1 RID: 1265 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public int IndexOf(TemplateGroup group)
		{
			throw new NotImplementedException();
		}

		/// <summary>Inserts a <see cref="T:System.Web.UI.Design.TemplateGroup" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index within the collection at which to insert <paramref name="group" />.</param>
		/// <param name="group">The <see cref="T:System.Web.UI.Design.TemplateGroup" /> to insert into the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.- or -<paramref name="index" /> is greater than the <see cref="P:System.Web.UI.Design.TemplateGroupCollection.Count" /> property.</exception>
		// Token: 0x060004F2 RID: 1266 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public void Insert(int index, TemplateGroup group)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.Design.TemplateGroup" /> object from the collection. </summary>
		/// <param name="group">The <see cref="T:System.Web.UI.Design.TemplateGroup" /> to remove from the collection. </param>
		// Token: 0x060004F3 RID: 1267 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public void Remove(TemplateGroup group)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.Design.TemplateGroup" /> object at the specified index within the collection.</summary>
		/// <param name="index">The zero-based index within the collection of the <see cref="T:System.Web.UI.Design.TemplateGroup" /> to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.- or -<paramref name="index" /> is greater than the <see cref="P:System.Web.UI.Design.TemplateGroupCollection.Count" /> property.</exception>
		// Token: 0x060004F4 RID: 1268 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> that is the destination of the copied groups. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060004F5 RID: 1269 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		void ICollection.CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> to use to iterate through the collection.</returns>
		// Token: 0x060004F6 RID: 1270 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <returns>The index at which <paramref name="o" /> was added to the collection.</returns>
		/// <param name="o">The <see cref="T:System.Web.UI.Design.TemplateGroup" /> to add to the collection.</param>
		// Token: 0x060004F7 RID: 1271 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		int IList.Add(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
		// Token: 0x060004F8 RID: 1272 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		void IList.Clear()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <returns>true, if <paramref name="o" /> is in the collection; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Web.UI.Design.TemplateGroup" /> to locate within the collection.</param>
		// Token: 0x060004F9 RID: 1273 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		bool IList.Contains(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="o" /> within the collection; otherwise, -1, if <paramref name="o" /> is not in the collection.</returns>
		/// <param name="o">The <see cref="T:System.Web.UI.Design.TemplateGroup" /> to locate within the collection.</param>
		// Token: 0x060004FA RID: 1274 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		int IList.IndexOf(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">The zero-based index within the collection at which to insert <paramref name="o" />.</param>
		/// <param name="o">The object to insert into the collection.</param>
		// Token: 0x060004FB RID: 1275 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		void IList.Insert(int index, object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="o">The object to remove from the collection.</param>
		// Token: 0x060004FC RID: 1276 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		void IList.Remove(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
		/// <param name="index">The zero-based index within the collection of the object to remove.</param>
		// Token: 0x060004FD RID: 1277 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		void IList.RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Web.UI.Design.TemplateGroupCollection" />.</returns>
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		int ICollection.Count
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>false, if access to the <see cref="T:System.Web.UI.Design.TemplateGroupCollection" /> is not synchronized (thread safe); otherwise, true.</returns>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		bool ICollection.IsSynchronized
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>An object to use to synchronize access to the <see cref="T:System.Web.UI.Design.TemplateGroupCollection" />.</returns>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		object ICollection.SyncRoot
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>false, if the <see cref="T:System.Web.UI.Design.TemplateGroupCollection" /> dynamically increases as new objects are added; otherwise, true.</returns>
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		bool IList.IsFixedSize
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>false, if the <see cref="T:System.Web.UI.Design.TemplateGroupCollection" /> can be added, modified, and removed; otherwise, true.</returns>
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		bool IList.IsReadOnly
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see the <see cref="T:System.Collections.IList" /> class.</summary>
		/// <returns>The object at <paramref name="index" /> in the collection.</returns>
		/// <param name="index">The zero-based index of the object to get in the collection.</param>
		// Token: 0x1700013D RID: 317
		[MonoNotSupported("")]
		object IList.this[int index]
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
			[MonoNotSupported("")]
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
