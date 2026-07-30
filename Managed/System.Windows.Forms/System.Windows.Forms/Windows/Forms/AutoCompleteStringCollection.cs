using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Contains a collection of strings to use for the auto-complete feature on certain Windows Forms controls. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000049 RID: 73
	public class AutoCompleteStringCollection : ICollection, IEnumerable, IList
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" /> class. </summary>
		// Token: 0x06000253 RID: 595 RVA: 0x000111CC File Offset: 0x0000F3CC
		public AutoCompleteStringCollection()
		{
			this.list = new ArrayList();
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000254 RID: 596 RVA: 0x000111E0 File Offset: 0x0000F3E0
		// (remove) Token: 0x06000255 RID: 597 RVA: 0x000111FC File Offset: 0x0000F3FC
		public event CollectionChangeEventHandler CollectionChanged;

		/// <summary>Copies the strings of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index. For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the strings copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000256 RID: 598 RVA: 0x00011218 File Offset: 0x0000F418
		void ICollection.CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Adds a string to the collection. For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <returns>The index at which the <paramref name="value" /> has been added. </returns>
		/// <param name="value">The string to be added to the collection</param>
		// Token: 0x06000257 RID: 599 RVA: 0x00011228 File Offset: 0x0000F428
		int IList.Add(object value)
		{
			return this.Add((string)value);
		}

		/// <summary>Determines where the collection contains a specified string. For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <returns>true if <paramref name="value" /> is found in the collection; otherwise, false.</returns>
		/// <param name="value">The string to locate in the collection.</param>
		// Token: 0x06000258 RID: 600 RVA: 0x00011238 File Offset: 0x0000F438
		bool IList.Contains(object value)
		{
			return this.Contains((string)value);
		}

		/// <summary>Determines the index of a specified string in the collection. For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The string to locate in the collection.</param>
		// Token: 0x06000259 RID: 601 RVA: 0x00011248 File Offset: 0x0000F448
		int IList.IndexOf(object value)
		{
			return this.IndexOf((string)value);
		}

		/// <summary>Inserts an item to the collection at the specified index. For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The string to insert into the collection.</param>
		// Token: 0x0600025A RID: 602 RVA: 0x00011258 File Offset: 0x0000F458
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (string)value);
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size. For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00011268 File Offset: 0x0000F468
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only. For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0001126C File Offset: 0x0000F46C
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Removes the first occurrence of a specific string from the collection. For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">The string to remove from the collection.</param>
		// Token: 0x0600025D RID: 605 RVA: 0x00011270 File Offset: 0x0000F470
		void IList.Remove(object value)
		{
			this.Remove((string)value);
		}

		/// <summary>Gets the element at a specified index. For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get.</param>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00011280 File Offset: 0x0000F480
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0001128C File Offset: 0x0000F48C
		object IList.Item
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (string)value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AutoCompleteStringCollection.CollectionChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data.</param>
		// Token: 0x06000260 RID: 608 RVA: 0x0001129C File Offset: 0x0000F49C
		protected void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.CollectionChanged == null)
			{
				return;
			}
			this.CollectionChanged.Invoke(this, e);
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x06000261 RID: 609 RVA: 0x000112B8 File Offset: 0x0000F4B8
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Copies an array of <see cref="T:System.String" /> objects into the collection, starting at the specified position.</summary>
		/// <param name="array">The <see cref="T:System.String" /> objects to add to the collection.</param>
		/// <param name="index">The position within the collection at which to start the insertion. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000262 RID: 610 RVA: 0x000112C8 File Offset: 0x0000F4C8
		public void CopyTo(string[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000263 RID: 611 RVA: 0x000112D8 File Offset: 0x0000F4D8
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <filterpriority>2</filterpriority>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000264 RID: 612 RVA: 0x000112E8 File Offset: 0x0000F4E8
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000265 RID: 613 RVA: 0x000112EC File Offset: 0x0000F4EC
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Inserts a new <see cref="T:System.String" /> into the collection.</summary>
		/// <returns>The position in the collection where the <see cref="T:System.String" /> was added.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to add to the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000266 RID: 614 RVA: 0x000112F0 File Offset: 0x0000F4F0
		public int Add(string value)
		{
			int num = this.list.Add(value);
			this.OnCollectionChanged(new CollectionChangeEventArgs(1, value));
			return num;
		}

		/// <summary>Adds the elements of a <see cref="T:System.String" /> collection to the end. </summary>
		/// <param name="value">The strings to add to the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000267 RID: 615 RVA: 0x00011318 File Offset: 0x0000F518
		public void AddRange(string[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", "Argument cannot be null!");
			}
			this.list.AddRange(value);
			this.OnCollectionChanged(new CollectionChangeEventArgs(3, null));
		}

		/// <summary>Removes all strings from the collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000268 RID: 616 RVA: 0x0001134C File Offset: 0x0000F54C
		public void Clear()
		{
			this.list.Clear();
			this.OnCollectionChanged(new CollectionChangeEventArgs(3, null));
		}

		/// <summary>Indicates whether the <see cref="T:System.String" /> exists within the collection.</summary>
		/// <returns>true if the <see cref="T:System.String" /> exists within the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.String" /> for which to search.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000269 RID: 617 RVA: 0x00011368 File Offset: 0x0000F568
		public bool Contains(string value)
		{
			return this.list.Contains(value);
		}

		/// <summary>Obtains the position of the specified string within the collection.</summary>
		/// <param name="value">The <see cref="T:System.String" /> for which to search.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600026A RID: 618 RVA: 0x00011378 File Offset: 0x0000F578
		public int IndexOf(string value)
		{
			return this.list.IndexOf(value);
		}

		/// <summary>Inserts the string into a specific index in the collection.</summary>
		/// <param name="index">The position at which to insert the string.</param>
		/// <param name="value">The string to insert.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600026B RID: 619 RVA: 0x00011388 File Offset: 0x0000F588
		public void Insert(int index, string value)
		{
			this.list.Insert(index, value);
			this.OnCollectionChanged(new CollectionChangeEventArgs(1, value));
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000113A4 File Offset: 0x0000F5A4
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Removes a string from the collection. </summary>
		/// <param name="value">The <see cref="T:System.String" /> to remove.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600026D RID: 621 RVA: 0x000113A8 File Offset: 0x0000F5A8
		public void Remove(string value)
		{
			this.list.Remove(value);
			this.OnCollectionChanged(new CollectionChangeEventArgs(2, value));
		}

		/// <summary>Removes the string at the specified index.</summary>
		/// <param name="index">The zero-based index of the string to remove.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600026E RID: 622 RVA: 0x000113C4 File Offset: 0x0000F5C4
		public void RemoveAt(int index)
		{
			string text = this[index];
			this.list.RemoveAt(index);
			this.OnCollectionChanged(new CollectionChangeEventArgs(2, text));
		}

		/// <returns>The <see cref="T:System.String" /> at the specified position.</returns>
		/// <param name="index">The index at which to get or set the <see cref="T:System.String" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700009B RID: 155
		public string this[int index]
		{
			get
			{
				return (string)this.list[index];
			}
			set
			{
				this.OnCollectionChanged(new CollectionChangeEventArgs(2, this.list[index]));
				this.list[index] = value;
				this.OnCollectionChanged(new CollectionChangeEventArgs(1, value));
			}
		}

		// Token: 0x040005EF RID: 1519
		private ArrayList list;
	}
}
