using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.ListSortDescription" /> objects.</summary>
	// Token: 0x020002AA RID: 682
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ListSortDescriptionCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> class. </summary>
		// Token: 0x06001510 RID: 5392 RVA: 0x00053B50 File Offset: 0x00051D50
		public ListSortDescriptionCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> class with the specified array of <see cref="T:System.ComponentModel.ListSortDescription" /> objects.</summary>
		/// <param name="sorts">The array of <see cref="T:System.ComponentModel.ListSortDescription" /> objects to be contained in the collection.</param>
		// Token: 0x06001511 RID: 5393 RVA: 0x00053B64 File Offset: 0x00051D64
		public ListSortDescriptionCollection(ListSortDescription[] sorts)
		{
			if (sorts != null)
			{
				for (int i = 0; i < sorts.Length; i++)
				{
					this.sorts.Add(sorts[i]);
				}
			}
		}

		/// <summary>Gets or sets the specified <see cref="T:System.ComponentModel.ListSortDescription" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ListSortDescription" /> with the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" />  to get or set in the collection. </param>
		/// <exception cref="T:System.InvalidOperationException">An item is set in the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" />, which is read-only.</exception>
		// Token: 0x17000463 RID: 1123
		public ListSortDescription this[int index]
		{
			get
			{
				return (ListSortDescription)this.sorts[index];
			}
			set
			{
				throw new InvalidOperationException(global::SR.GetString("Once a ListSortDescriptionCollection has been created it can't be modified."));
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x000027E2 File Offset: 0x000009E2
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x000027E2 File Offset: 0x000009E2
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the specified <see cref="T:System.ComponentModel.ListSortDescription" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ListSortDescription" /> with the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" />  to get in the collection </param>
		// Token: 0x17000466 RID: 1126
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new InvalidOperationException(global::SR.GetString("Once a ListSortDescriptionCollection has been created it can't be modified."));
			}
		}

		/// <summary>Adds an item to the collection.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The item to add to the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06001518 RID: 5400 RVA: 0x00053BB5 File Offset: 0x00051DB5
		int IList.Add(object value)
		{
			throw new InvalidOperationException(global::SR.GetString("Once a ListSortDescriptionCollection has been created it can't be modified."));
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06001519 RID: 5401 RVA: 0x00053BB5 File Offset: 0x00051DB5
		void IList.Clear()
		{
			throw new InvalidOperationException(global::SR.GetString("Once a ListSortDescriptionCollection has been created it can't be modified."));
		}

		/// <summary>Determines if the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the collection; otherwise, false. </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		// Token: 0x0600151A RID: 5402 RVA: 0x00053BCF File Offset: 0x00051DCF
		public bool Contains(object value)
		{
			return ((IList)this.sorts).Contains(value);
		}

		/// <summary>Returns the index of the specified item in the collection.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		// Token: 0x0600151B RID: 5403 RVA: 0x00053BDD File Offset: 0x00051DDD
		public int IndexOf(object value)
		{
			return ((IList)this.sorts).IndexOf(value);
		}

		/// <summary>Inserts an item into the collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" />  to get or set in the collection</param>
		/// <param name="value">The item to insert into the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x0600151C RID: 5404 RVA: 0x00053BB5 File Offset: 0x00051DB5
		void IList.Insert(int index, object value)
		{
			throw new InvalidOperationException(global::SR.GetString("Once a ListSortDescriptionCollection has been created it can't be modified."));
		}

		/// <summary>Removes the first occurrence of an item from the collection.</summary>
		/// <param name="value">The item to remove from the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x0600151D RID: 5405 RVA: 0x00053BB5 File Offset: 0x00051DB5
		void IList.Remove(object value)
		{
			throw new InvalidOperationException(global::SR.GetString("Once a ListSortDescriptionCollection has been created it can't be modified."));
		}

		/// <summary>Removes an item from the collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" />  to remove from the collection</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x0600151E RID: 5406 RVA: 0x00053BB5 File Offset: 0x00051DB5
		void IList.RemoveAt(int index)
		{
			throw new InvalidOperationException(global::SR.GetString("Once a ListSortDescriptionCollection has been created it can't be modified."));
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x00053BEB File Offset: 0x00051DEB
		public int Count
		{
			get
			{
				return this.sorts.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is thread safe.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x000027E2 File Offset: 0x000009E2
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the current instance that can be used to synchronize access to the collection.</summary>
		/// <returns>The current instance of the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" />.</returns>
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x00002068 File Offset: 0x00000268
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the contents of the collection to the specified array, starting at the specified destination array index.</summary>
		/// <param name="array">The destination array for the items copied from the collection.</param>
		/// <param name="index">The index of the destination array at which copying begins.</param>
		// Token: 0x06001522 RID: 5410 RVA: 0x00053BF8 File Offset: 0x00051DF8
		public void CopyTo(Array array, int index)
		{
			this.sorts.CopyTo(array, index);
		}

		/// <summary>Gets a <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06001523 RID: 5411 RVA: 0x00053C07 File Offset: 0x00051E07
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.sorts.GetEnumerator();
		}

		// Token: 0x0400131E RID: 4894
		private ArrayList sorts = new ArrayList();
	}
}
