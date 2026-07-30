using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> objects in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A2 RID: 418
	[ListBindable(false)]
	[Editor("System.Windows.Forms.Design.DataGridColumnCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public class GridColumnStylesCollection : BaseCollection, ICollection, IEnumerable, IList
	{
		// Token: 0x06001B11 RID: 6929 RVA: 0x000697A8 File Offset: 0x000679A8
		internal GridColumnStylesCollection(DataGridTableStyle tablestyle)
		{
			this.items = new ArrayList();
			this.owner = tablestyle;
			this.fire_event = true;
		}

		/// <summary>Occurs when a change is made to the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001B4 RID: 436
		// (add) Token: 0x06001B12 RID: 6930 RVA: 0x000697CC File Offset: 0x000679CC
		// (remove) Token: 0x06001B13 RID: 6931 RVA: 0x000697E8 File Offset: 0x000679E8
		public event CollectionChangeEventHandler CollectionChanged;

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x00069804 File Offset: 0x00067A04
		int ICollection.Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x00069814 File Offset: 0x00067A14
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
		/// <returns>The object used to synchronize access to the collection.</returns>
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x00069818 File Offset: 0x00067A18
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001B17 RID: 6935 RVA: 0x0006981C File Offset: 0x00067A1C
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x00069820 File Offset: 0x00067A20
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <exception cref="T:System.NotSupportedException">An operation attempts to set this property.</exception>
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x00069824 File Offset: 0x00067A24
		// (set) Token: 0x06001B1A RID: 6938 RVA: 0x00069834 File Offset: 0x00067A34
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
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The array must have zero-based indexing.  </param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-The number of elements in the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> cannot be cast automatically to the type of <paramref name="array" />.</exception>
		// Token: 0x06001B1B RID: 6939 RVA: 0x0006983C File Offset: 0x00067A3C
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
		// Token: 0x06001B1C RID: 6940 RVA: 0x0006984C File Offset: 0x00067A4C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		/// <summary>Adds an object to the collection.</summary>
		/// <returns>The index at which the value has been added.</returns>
		/// <param name="value">The object to be added to the collection. The value can be null.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to a <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</exception>
		// Token: 0x06001B1D RID: 6941 RVA: 0x0006985C File Offset: 0x00067A5C
		int IList.Add(object value)
		{
			return this.Add((DataGridColumnStyle)value);
		}

		/// <summary>Clears the collection of <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> objects.</summary>
		// Token: 0x06001B1E RID: 6942 RVA: 0x0006986C File Offset: 0x00067A6C
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether an element is in the collection.</summary>
		/// <returns>true if the element is in the collection; otherwise, false.</returns>
		/// <param name="value">The object to locate in the collection. The value can be null.</param>
		// Token: 0x06001B1F RID: 6943 RVA: 0x00069874 File Offset: 0x00067A74
		bool IList.Contains(object value)
		{
			return this.Contains((DataGridColumnStyle)value);
		}

		/// <summary>Returns the zero-based index of the first occurrence of the specified object in the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of the <paramref name="value" /> parameter within the collection, if found; otherwise, -1.</returns>
		/// <param name="value">The object to locate in the collection. The value can be null.</param>
		// Token: 0x06001B20 RID: 6944 RVA: 0x00069884 File Offset: 0x00067A84
		int IList.IndexOf(object value)
		{
			return this.IndexOf((DataGridColumnStyle)value);
		}

		/// <summary>This method is not supported by this control.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The object to insert into the collection.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06001B21 RID: 6945 RVA: 0x00069894 File Offset: 0x00067A94
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> from the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to remove from the collection.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to a <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</exception>
		// Token: 0x06001B22 RID: 6946 RVA: 0x0006989C File Offset: 0x00067A9C
		void IList.Remove(object value)
		{
			this.Remove((DataGridColumnStyle)value);
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> at the specified index from the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to remove.</param>
		// Token: 0x06001B23 RID: 6947 RVA: 0x000698AC File Offset: 0x00067AAC
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified name.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified column header.</returns>
		/// <param name="columnName">The <see cref="P:System.Windows.Forms.DataGridColumnStyle.MappingName" /> of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to retrieve. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000675 RID: 1653
		public DataGridColumnStyle this[string columnName]
		{
			get
			{
				int num = this.FromColumnNameToIndex(columnName);
				return (num != -1) ? this[num] : null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> at a specified index.</summary>
		/// <returns>The specified <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000676 RID: 1654
		public DataGridColumnStyle this[int index]
		{
			get
			{
				return (DataGridColumnStyle)this.items[index];
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> associated with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> associated the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		/// <param name="propertyDesciptor">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000677 RID: 1655
		public DataGridColumnStyle this[PropertyDescriptor propertyDesciptor]
		{
			get
			{
				for (int i = 0; i < this.items.Count; i++)
				{
					DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)this.items[i];
					if (dataGridColumnStyle.PropertyDescriptor.Equals(propertyDesciptor))
					{
						return dataGridColumnStyle;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the list of items in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the collection items.</returns>
		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x0006994C File Offset: 0x00067B4C
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x00069954 File Offset: 0x00067B54
		// (set) Token: 0x06001B29 RID: 6953 RVA: 0x0006995C File Offset: 0x00067B5C
		internal bool FireEvents
		{
			get
			{
				return this.fire_event;
			}
			set
			{
				this.fire_event = value;
			}
		}

		/// <summary>Adds a column style to the collection.</summary>
		/// <returns>The index of the new <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
		/// <param name="column">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to add. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B2A RID: 6954 RVA: 0x00069968 File Offset: 0x00067B68
		public virtual int Add(DataGridColumnStyle column)
		{
			if (this.FromColumnNameToIndex(column.MappingName) != -1)
			{
				throw new ArgumentException("The ColumnStyles collection already has a column with this mapping name");
			}
			column.TableStyle = this.owner;
			column.SetDataGridInternal(this.owner.DataGrid);
			this.ConnectColumnEvents(column);
			int num = this.items.Add(column);
			this.OnCollectionChanged(new CollectionChangeEventArgs(1, column));
			return num;
		}

		/// <summary>Adds an array of column style objects to the collection.</summary>
		/// <param name="columns">An array of <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> objects to add to the collection. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B2B RID: 6955 RVA: 0x000699D4 File Offset: 0x00067BD4
		public void AddRange(DataGridColumnStyle[] columns)
		{
			foreach (DataGridColumnStyle dataGridColumnStyle in columns)
			{
				this.Add(dataGridColumnStyle);
			}
		}

		/// <summary>Clears the collection of <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> objects.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B2C RID: 6956 RVA: 0x00069A04 File Offset: 0x00067C04
		public void Clear()
		{
			this.items.Clear();
			this.OnCollectionChanged(new CollectionChangeEventArgs(3, null));
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> contains the specified <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
		/// <returns>true if the collection contains the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />; otherwise, false.</returns>
		/// <param name="column">The desired <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B2D RID: 6957 RVA: 0x00069A20 File Offset: 0x00067C20
		public bool Contains(DataGridColumnStyle column)
		{
			return this.FromColumnNameToIndex(column.MappingName) != -1;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> contains a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> associated with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <returns>true if the collection contains the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />; otherwise, false.</returns>
		/// <param name="propertyDescriptor">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with the desired <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B2E RID: 6958 RVA: 0x00069A34 File Offset: 0x00067C34
		public bool Contains(PropertyDescriptor propertyDescriptor)
		{
			return this[propertyDescriptor] != null;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> contains the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified name.</summary>
		/// <returns>true if the collection contains the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />; otherwise, false.</returns>
		/// <param name="name">The <see cref="P:System.Windows.Forms.DataGridColumnStyle.MappingName" /> of the desired <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B2F RID: 6959 RVA: 0x00069A50 File Offset: 0x00067C50
		public bool Contains(string name)
		{
			return this.FromColumnNameToIndex(name) != -1;
		}

		/// <summary>Gets the index of a specified <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
		/// <returns>The zero-based index of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> within the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> or -1 if no corresponding <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> exists.</returns>
		/// <param name="element">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to find. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B30 RID: 6960 RVA: 0x00069A60 File Offset: 0x00067C60
		public int IndexOf(DataGridColumnStyle element)
		{
			return this.items.IndexOf(element);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.GridColumnStylesCollection.CollectionChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data event. </param>
		// Token: 0x06001B31 RID: 6961 RVA: 0x00069A70 File Offset: 0x00067C70
		protected void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.fire_event && this.CollectionChanged != null)
			{
				this.CollectionChanged.Invoke(this, e);
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> from the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
		/// <param name="column">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to remove from the collection. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B32 RID: 6962 RVA: 0x00069AA4 File Offset: 0x00067CA4
		public void Remove(DataGridColumnStyle column)
		{
			this.items.Remove(column);
			this.DisconnectColumnEvents(column);
			this.OnCollectionChanged(new CollectionChangeEventArgs(2, column));
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified index from the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to remove. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B33 RID: 6963 RVA: 0x00069AD4 File Offset: 0x00067CD4
		public void RemoveAt(int index)
		{
			DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)this.items[index];
			this.items.RemoveAt(index);
			this.DisconnectColumnEvents(dataGridColumnStyle);
			this.OnCollectionChanged(new CollectionChangeEventArgs(2, dataGridColumnStyle));
		}

		/// <summary>Sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> for each column style in the collection to null.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B34 RID: 6964 RVA: 0x00069B14 File Offset: 0x00067D14
		public void ResetPropertyDescriptors()
		{
			for (int i = 0; i < this.items.Count; i++)
			{
				DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)this.items[i];
				if (dataGridColumnStyle.PropertyDescriptor != null)
				{
					dataGridColumnStyle.PropertyDescriptor = null;
				}
			}
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x00069B64 File Offset: 0x00067D64
		private void ConnectColumnEvents(DataGridColumnStyle col)
		{
			col.AlignmentChanged += new EventHandler(this.ColumnAlignmentChangedEvent);
			col.FontChanged += new EventHandler(this.ColumnFontChangedEvent);
			col.HeaderTextChanged += new EventHandler(this.ColumnHeaderTextChanged);
			col.MappingNameChanged += new EventHandler(this.ColumnMappingNameChangedEvent);
			col.NullTextChanged += new EventHandler(this.ColumnNullTextChangedEvent);
			col.PropertyDescriptorChanged += new EventHandler(this.ColumnPropertyDescriptorChanged);
			col.ReadOnlyChanged += new EventHandler(this.ColumnReadOnlyChangedEvent);
			col.WidthChanged += new EventHandler(this.ColumnWidthChangedEvent);
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x00069C04 File Offset: 0x00067E04
		private void DisconnectColumnEvents(DataGridColumnStyle col)
		{
			col.AlignmentChanged -= new EventHandler(this.ColumnAlignmentChangedEvent);
			col.FontChanged -= new EventHandler(this.ColumnFontChangedEvent);
			col.HeaderTextChanged -= new EventHandler(this.ColumnHeaderTextChanged);
			col.MappingNameChanged -= new EventHandler(this.ColumnMappingNameChangedEvent);
			col.NullTextChanged -= new EventHandler(this.ColumnNullTextChangedEvent);
			col.PropertyDescriptorChanged -= new EventHandler(this.ColumnPropertyDescriptorChanged);
			col.ReadOnlyChanged -= new EventHandler(this.ColumnReadOnlyChangedEvent);
			col.WidthChanged -= new EventHandler(this.ColumnWidthChangedEvent);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x00069CA4 File Offset: 0x00067EA4
		private void ColumnAlignmentChangedEvent(object sender, EventArgs e)
		{
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x00069CA8 File Offset: 0x00067EA8
		private void ColumnFontChangedEvent(object sender, EventArgs e)
		{
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x00069CAC File Offset: 0x00067EAC
		private void ColumnHeaderTextChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00069CB0 File Offset: 0x00067EB0
		private void ColumnMappingNameChangedEvent(object sender, EventArgs e)
		{
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00069CB4 File Offset: 0x00067EB4
		private void ColumnNullTextChangedEvent(object sender, EventArgs e)
		{
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00069CB8 File Offset: 0x00067EB8
		private void ColumnPropertyDescriptorChanged(object sender, EventArgs e)
		{
			this.OnCollectionChanged(new CollectionChangeEventArgs(3, sender));
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x00069CC8 File Offset: 0x00067EC8
		private void ColumnReadOnlyChangedEvent(object sender, EventArgs e)
		{
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x00069CCC File Offset: 0x00067ECC
		private void ColumnWidthChangedEvent(object sender, EventArgs e)
		{
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x00069CD0 File Offset: 0x00067ED0
		private int FromColumnNameToIndex(string columnName)
		{
			for (int i = 0; i < this.items.Count; i++)
			{
				DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)this.items[i];
				if (dataGridColumnStyle.MappingName != null && !(dataGridColumnStyle.MappingName == string.Empty))
				{
					if (string.Compare(dataGridColumnStyle.MappingName, columnName, true) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x04000F08 RID: 3848
		private ArrayList items;

		// Token: 0x04000F09 RID: 3849
		private DataGridTableStyle owner;

		// Token: 0x04000F0A RID: 3850
		private bool fire_event;
	}
}
