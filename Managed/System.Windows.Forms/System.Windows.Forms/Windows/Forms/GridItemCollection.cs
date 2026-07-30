using System;
using System.Collections;

namespace System.Windows.Forms
{
	/// <summary>Contains a collection of <see cref="T:System.Windows.Forms.GridItem" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A5 RID: 421
	public class GridItemCollection : ICollection, IEnumerable
	{
		// Token: 0x06001B89 RID: 7049 RVA: 0x0006B258 File Offset: 0x00069458
		internal GridItemCollection()
		{
			this.list = new SortedList();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x0006B278 File Offset: 0x00069478
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="dest">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		// Token: 0x06001B8C RID: 7052 RVA: 0x0006B288 File Offset: 0x00069488
		void ICollection.CopyTo(Array dest, int index)
		{
			this.list.CopyTo(dest, index);
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.GridItemCollection" />.</returns>
		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x0006B298 File Offset: 0x00069498
		object ICollection.SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x0006B2A8 File Offset: 0x000694A8
		internal void Add(GridItem grid_item)
		{
			string text = grid_item.Label;
			while (this.list.ContainsKey(text))
			{
				text += "_";
			}
			this.list.Add(text, grid_item);
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x0006B2EC File Offset: 0x000694EC
		internal void AddRange(GridItemCollection items)
		{
			foreach (object obj in items)
			{
				GridItem gridItem = (GridItem)obj;
				this.Add(gridItem);
			}
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x0006B358 File Offset: 0x00069558
		internal int IndexOf(GridItem grid_item)
		{
			return this.list.IndexOfValue(grid_item);
		}

		/// <summary>Gets the number of grid items in the collection.</summary>
		/// <returns>The number of grid items in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x0006B368 File Offset: 0x00069568
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.GridItem" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.GridItem" /> at the specified index.</returns>
		/// <param name="index">The index of the grid item to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006A6 RID: 1702
		public GridItem this[int index]
		{
			get
			{
				if (index >= this.list.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return (GridItem)this.list.GetByIndex(index);
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.GridItem" /> with the matching label.</summary>
		/// <returns>The grid item whose label matches the <paramref name="label" /> parameter.</returns>
		/// <param name="label">A string value to match to a grid item label </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006A7 RID: 1703
		public GridItem this[string label]
		{
			get
			{
				return (GridItem)this.list[label];
			}
		}

		/// <summary>Returns an enumeration of all the grid items in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Windows.Forms.GridItemCollection" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B94 RID: 7060 RVA: 0x0006B3BC File Offset: 0x000695BC
		public IEnumerator GetEnumerator()
		{
			return new GridItemCollection.GridItemEnumerator(this);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x0006B3C4 File Offset: 0x000695C4
		internal void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x04000F16 RID: 3862
		private SortedList list;

		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.GridItemCollection" /> has no entries. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000F17 RID: 3863
		public static GridItemCollection Empty = new GridItemCollection();

		// Token: 0x020001A6 RID: 422
		internal class GridItemEnumerator : IEnumerator
		{
			// Token: 0x06001B96 RID: 7062 RVA: 0x0006B3D4 File Offset: 0x000695D4
			public GridItemEnumerator(GridItemCollection coll)
			{
				this.collection = coll;
				this.nIndex = -1;
			}

			// Token: 0x170006A8 RID: 1704
			// (get) Token: 0x06001B97 RID: 7063 RVA: 0x0006B3EC File Offset: 0x000695EC
			object IEnumerator.Current
			{
				get
				{
					return this.collection[this.nIndex];
				}
			}

			// Token: 0x06001B98 RID: 7064 RVA: 0x0006B400 File Offset: 0x00069600
			public bool MoveNext()
			{
				this.nIndex++;
				return this.nIndex < this.collection.Count;
			}

			// Token: 0x06001B99 RID: 7065 RVA: 0x0006B424 File Offset: 0x00069624
			public void Reset()
			{
				this.nIndex = -1;
			}

			// Token: 0x04000F18 RID: 3864
			private int nIndex;

			// Token: 0x04000F19 RID: 3865
			private GridItemCollection collection;
		}
	}
}
