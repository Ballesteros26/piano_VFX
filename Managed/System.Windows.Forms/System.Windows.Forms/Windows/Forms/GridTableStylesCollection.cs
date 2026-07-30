using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.DataGridTableStyle" /> objects in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A8 RID: 424
	[ListBindable(false)]
	public class GridTableStylesCollection : BaseCollection, ICollection, IEnumerable, IList
	{
		// Token: 0x06001B9A RID: 7066 RVA: 0x0006B430 File Offset: 0x00069630
		internal GridTableStylesCollection(DataGrid grid)
		{
			this.items = new ArrayList();
			this.owner = grid;
		}

		/// <summary>Occurs when the collection has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001B5 RID: 437
		// (add) Token: 0x06001B9B RID: 7067 RVA: 0x0006B44C File Offset: 0x0006964C
		// (remove) Token: 0x06001B9C RID: 7068 RVA: 0x0006B468 File Offset: 0x00069668
		public event CollectionChangeEventHandler CollectionChanged;

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of items contained in the collection.</returns>
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x0006B484 File Offset: 0x00069684
		int ICollection.Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.GridTableStylesCollection" /> is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x0006B494 File Offset: 0x00069694
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>The <see cref="T:System.Object" /> used to synchronize access to the collection.</returns>
		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x0006B498 File Offset: 0x00069698
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x0006B49C File Offset: 0x0006969C
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x0006B4A0 File Offset: 0x000696A0
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		/// <exception cref="T:System.NotSupportedException">The item property cannot be set.</exception>
		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x0006B4A4 File Offset: 0x000696A4
		// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x0006B4B4 File Offset: 0x000696B4
		object IList.Item
		{
			get
			{
				return this.items[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Copies the collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the collection. The array must have zero-based indexing.  </param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-The number of elements in the <see cref="T:System.Windows.Forms.GridTableStylesCollection" /> is greater than the available space from index to the end of the destination array.</exception>
		/// <exception cref="T:System.InvalidCastException">The type in the collection cannot be cast automatically to the type of the destination array.</exception>
		// Token: 0x06001BA4 RID: 7076 RVA: 0x0006B4BC File Offset: 0x000696BC
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
		// Token: 0x06001BA5 RID: 7077 RVA: 0x0006B4CC File Offset: 0x000696CC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to this collection.</summary>
		/// <returns>The index of the newly added object.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to add to the collection.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to a <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> has already been assigned to a <see cref="T:System.Windows.Forms.GridTableStylesCollection" />.-or-A <see cref="T:System.Windows.Forms.DataGridTableStyle" /> in <see cref="T:System.Windows.Forms.GridTableStylesCollection" /> has the same <see cref="P:System.Windows.Forms.DataGridTableStyle.MappingName" /> property value as <paramref name="value" />.</exception>
		// Token: 0x06001BA6 RID: 7078 RVA: 0x0006B4DC File Offset: 0x000696DC
		int IList.Add(object value)
		{
			return this.Add((DataGridTableStyle)value);
		}

		/// <summary>Clears the collection.</summary>
		// Token: 0x06001BA7 RID: 7079 RVA: 0x0006B4EC File Offset: 0x000696EC
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether an element is in the collection.</summary>
		/// <returns>true if value is found in the collection; otherwise, false.</returns>
		/// <param name="value">The object to locate in the collection. The value can be null.</param>
		// Token: 0x06001BA8 RID: 7080 RVA: 0x0006B4F4 File Offset: 0x000696F4
		bool IList.Contains(object value)
		{
			return this.Contains((DataGridTableStyle)value);
		}

		/// <summary>Returns the zero-based index of the first occurrence of the specified object in the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of value within the entire collection, if found; otherwise, -1.</returns>
		/// <param name="value">The object to locate in the collection. The value can be null.</param>
		// Token: 0x06001BA9 RID: 7081 RVA: 0x0006B504 File Offset: 0x00069704
		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">The object to insert into the collection.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06001BAA RID: 7082 RVA: 0x0006B514 File Offset: 0x00069714
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to remove from the collection.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to a <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</exception>
		// Token: 0x06001BAB RID: 7083 RVA: 0x0006B51C File Offset: 0x0006971C
		void IList.Remove(object value)
		{
			this.Remove((DataGridTableStyle)value);
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified index from the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to remove.</param>
		// Token: 0x06001BAC RID: 7084 RVA: 0x0006B52C File Offset: 0x0006972C
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> with the specified name.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> with the specified <see cref="P:System.Windows.Forms.DataGridTableStyle.MappingName" />.</returns>
		/// <param name="tableName">The <see cref="P:System.Windows.Forms.DataGridTableStyle.MappingName" /> of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to retrieve. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006AF RID: 1711
		public DataGridTableStyle this[string tableName]
		{
			get
			{
				int num = this.FromTableNameToIndex(tableName);
				return (num != -1) ? this[num] : null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> specified by index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to get. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">No item exists at the specified index. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006B0 RID: 1712
		public DataGridTableStyle this[int index]
		{
			get
			{
				return (DataGridTableStyle)this.items[index];
			}
		}

		/// <summary>Gets the underlying list.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains the table data.</returns>
		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x0006B578 File Offset: 0x00069778
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to this collection.</summary>
		/// <returns>The index of the newly added object.</returns>
		/// <param name="table">The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to add to the collection. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BB0 RID: 7088 RVA: 0x0006B580 File Offset: 0x00069780
		public virtual int Add(DataGridTableStyle table)
		{
			int num = this.AddInternal(table);
			this.OnCollectionChanged(new CollectionChangeEventArgs(1, table));
			return num;
		}

		/// <summary>Adds an array of table styles to the collection.</summary>
		/// <param name="tables">An array of <see cref="T:System.Windows.Forms.DataGridTableStyle" /> objects. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BB1 RID: 7089 RVA: 0x0006B5A4 File Offset: 0x000697A4
		public virtual void AddRange(DataGridTableStyle[] tables)
		{
			foreach (DataGridTableStyle dataGridTableStyle in tables)
			{
				this.AddInternal(dataGridTableStyle);
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(3, null));
		}

		/// <summary>Clears the collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BB2 RID: 7090 RVA: 0x0006B5E0 File Offset: 0x000697E0
		public void Clear()
		{
			this.items.Clear();
			this.OnCollectionChanged(new CollectionChangeEventArgs(3, null));
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.GridTableStylesCollection" /> contains the specified <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</summary>
		/// <returns>true if the specified table style exists in the collection; otherwise, false.</returns>
		/// <param name="table">The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to look for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BB3 RID: 7091 RVA: 0x0006B5FC File Offset: 0x000697FC
		public bool Contains(DataGridTableStyle table)
		{
			return this.FromTableNameToIndex(table.MappingName) != -1;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.GridTableStylesCollection" /> contains the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> specified by name.</summary>
		/// <returns>true if the specified table style exists in the collection; otherwise, false.</returns>
		/// <param name="name">The <see cref="P:System.Windows.Forms.DataGridTableStyle.MappingName" /> of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to look for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BB4 RID: 7092 RVA: 0x0006B610 File Offset: 0x00069810
		public bool Contains(string name)
		{
			return this.FromTableNameToIndex(name) != -1;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.GridTableStylesCollection.CollectionChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> containing the event data. </param>
		// Token: 0x06001BB5 RID: 7093 RVA: 0x0006B620 File Offset: 0x00069820
		protected void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged.Invoke(this, e);
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</summary>
		/// <param name="table">The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to remove. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BB6 RID: 7094 RVA: 0x0006B648 File Offset: 0x00069848
		public void Remove(DataGridTableStyle table)
		{
			this.items.Remove(table);
			this.OnCollectionChanged(new CollectionChangeEventArgs(2, table));
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0006B664 File Offset: 0x00069864
		private void MappingNameChanged(object sender, EventArgs args)
		{
			this.OnCollectionChanged(new CollectionChangeEventArgs(3, null));
		}

		/// <summary>Removes a <see cref="T:System.Windows.Forms.DataGridTableStyle" /> at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to remove. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BB8 RID: 7096 RVA: 0x0006B674 File Offset: 0x00069874
		public void RemoveAt(int index)
		{
			DataGridTableStyle dataGridTableStyle = (DataGridTableStyle)this.items[index];
			this.items.RemoveAt(index);
			dataGridTableStyle.MappingNameChanged -= new EventHandler(this.MappingNameChanged);
			this.OnCollectionChanged(new CollectionChangeEventArgs(2, dataGridTableStyle));
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0006B6C0 File Offset: 0x000698C0
		private int AddInternal(DataGridTableStyle table)
		{
			if (this.FromTableNameToIndex(table.MappingName) != -1)
			{
				throw new ArgumentException("The TableStyles collection already has a TableStyle with this mapping name");
			}
			table.MappingNameChanged += new EventHandler(this.MappingNameChanged);
			table.DataGrid = this.owner;
			return this.items.Add(table);
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0006B718 File Offset: 0x00069918
		private int FromTableNameToIndex(string tableName)
		{
			for (int i = 0; i < this.items.Count; i++)
			{
				DataGridTableStyle dataGridTableStyle = (DataGridTableStyle)this.items[i];
				if (string.Compare(dataGridTableStyle.MappingName, tableName, true) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04000F1F RID: 3871
		private ArrayList items;

		// Token: 0x04000F20 RID: 3872
		private DataGrid owner;
	}
}
