using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of cells in a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E8 RID: 232
	[ListBindable(false)]
	public class DataGridViewCellCollection : BaseCollection, ICollection, IEnumerable, IList
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> class.</summary>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that owns the collection.</param>
		// Token: 0x06001230 RID: 4656 RVA: 0x00047DF4 File Offset: 0x00045FF4
		public DataGridViewCellCollection(DataGridViewRow dataGridViewRow)
		{
			this.dataGridViewRow = dataGridViewRow;
		}

		/// <summary>Occurs when the collection is changed. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000169 RID: 361
		// (add) Token: 0x06001231 RID: 4657 RVA: 0x00047E04 File Offset: 0x00046004
		// (remove) Token: 0x06001232 RID: 4658 RVA: 0x00047E20 File Offset: 0x00046020
		public event CollectionChangeEventHandler CollectionChanged;

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x00047E3C File Offset: 0x0004603C
		bool IList.IsFixedSize
		{
			get
			{
				return base.List.IsFixedSize;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> at the specified index.</returns>
		/// <param name="index">The index of the item to get or set.</param>
		/// <exception cref="T:System.InvalidCastException">The specified value when setting this property is not a <see cref="T:System.Windows.Forms.DataGridViewCell" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is equal to or greater than the number of cells in the collection.</exception>
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x00047E4C File Offset: 0x0004604C
		// (set) Token: 0x06001235 RID: 4661 RVA: 0x00047E58 File Offset: 0x00046058
		object IList.Item
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = value as DataGridViewCell;
			}
		}

		/// <summary>Adds an item to the collection.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to add to the collection.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewCell" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-<paramref name="value" /> represents a cell that already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		// Token: 0x06001236 RID: 4662 RVA: 0x00047E68 File Offset: 0x00046068
		int IList.Add(object value)
		{
			return this.Add(value as DataGridViewCell);
		}

		/// <summary>Determines whether the collection contains the specified value.</summary>
		/// <returns>true if the <paramref name="value" /> is found in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" />; otherwise, false.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" />.</param>
		// Token: 0x06001237 RID: 4663 RVA: 0x00047E78 File Offset: 0x00046078
		bool IList.Contains(object value)
		{
			return this.Contains(value as DataGridViewCell);
		}

		/// <summary>Determines the index of a specific item in a collection.</summary>
		/// <returns>The index of value if found in the list; otherwise, -1.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" />.</param>
		// Token: 0x06001238 RID: 4664 RVA: 0x00047E88 File Offset: 0x00046088
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as DataGridViewCell);
		}

		/// <summary>Inserts an item into the collection at the specified position.</summary>
		/// <param name="index">The zero-based index at which value should be inserted. </param>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to insert into the <see cref="M:System.Windows.Forms.DataGridViewCellCollection.System#Collections#IList#Insert(System.Int32,System.Object)" />.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewCell" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-<paramref name="dataGridViewCell" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		// Token: 0x06001239 RID: 4665 RVA: 0x00047E98 File Offset: 0x00046098
		void IList.Insert(int index, object value)
		{
			this.Insert(index, value as DataGridViewCell);
		}

		/// <summary>Removes the first occurrence of a specific object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to remove from the collection.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewCell" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="cell" /> could not be found in the collection.</exception>
		// Token: 0x0600123A RID: 4666 RVA: 0x00047EA8 File Offset: 0x000460A8
		void IList.Remove(object value)
		{
			this.Remove(value as DataGridViewCell);
		}

		/// <summary>Gets or sets the cell at the provided index location. In C#, this property is the indexer for the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> class.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> stored at the given index.</returns>
		/// <param name="index">The zero-based index of the cell to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is equal to or greater than the number of cells in the collection.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003EF RID: 1007
		public DataGridViewCell this[int index]
		{
			get
			{
				return (DataGridViewCell)base.List[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.Insert(index, value);
			}
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00047EE8 File Offset: 0x000460E8
		internal DataGridViewCell GetCellInternal(int colIndex)
		{
			return (DataGridViewCell)base.List[colIndex];
		}

		/// <summary>Gets or sets the cell in the column with the provided name. In C#, this property is the indexer for the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> class.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> stored in the column with the given name.</returns>
		/// <param name="columnName">The name of the column in which to get or set the cell.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="columnName" /> does not match the name of any columns in the control.</exception>
		/// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003F0 RID: 1008
		public DataGridViewCell this[string columnName]
		{
			get
			{
				if (columnName == null)
				{
					throw new ArgumentNullException("columnName");
				}
				foreach (object obj in base.List)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
					if (string.Compare(dataGridViewCell.OwningColumn.Name, columnName, true) == 0)
					{
						return dataGridViewCell;
					}
				}
				throw new ArgumentException(string.Format("Column name {0} cannot be found.", columnName), "columnName");
			}
			set
			{
				if (columnName == null)
				{
					throw new ArgumentNullException("columnName");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				for (int i = 0; i < base.List.Count; i++)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)base.List[i];
					if (string.Compare(dataGridViewCell.OwningColumn.Name, columnName, true) == 0)
					{
						this.Insert(i, value);
						return;
					}
				}
				this.Add(value);
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00048034 File Offset: 0x00046234
		internal DataGridViewCell GetBoundCell(string dataPropertyName)
		{
			foreach (object obj in base.List)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				if (string.Compare(dataGridViewCell.OwningColumn.DataPropertyName, dataPropertyName, true) == 0)
				{
					return dataGridViewCell;
				}
			}
			return null;
		}

		/// <summary>Adds a cell to the collection.</summary>
		/// <returns>The position in which to insert the new element.</returns>
		/// <param name="dataGridViewCell">A <see cref="T:System.Windows.Forms.DataGridViewCell" /> to add to the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-<paramref name="dataGridViewCell" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001241 RID: 4673 RVA: 0x000480C0 File Offset: 0x000462C0
		public virtual int Add(DataGridViewCell dataGridViewCell)
		{
			int num = base.List.Add(dataGridViewCell);
			dataGridViewCell.SetOwningRow(this.dataGridViewRow);
			dataGridViewCell.SetColumnIndex(num);
			dataGridViewCell.SetDataGridView(this.dataGridViewRow.DataGridView);
			this.OnCollectionChanged(new CollectionChangeEventArgs(1, dataGridViewCell));
			return num;
		}

		/// <summary>Adds an array of cells to the collection.</summary>
		/// <param name="dataGridViewCells">The array of <see cref="T:System.Windows.Forms.DataGridViewCell" /> objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewCells" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-At least one value in <paramref name="dataGridViewCells" /> is null.-or-At least one cell in <paramref name="dataGridViewCells" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.-or-At least two values in <paramref name="dataGridViewCells" /> are references to the same <see cref="T:System.Windows.Forms.DataGridViewCell" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001242 RID: 4674 RVA: 0x0004810C File Offset: 0x0004630C
		[DesignerSerializationVisibility(0)]
		public virtual void AddRange(params DataGridViewCell[] dataGridViewCells)
		{
			foreach (DataGridViewCell dataGridViewCell in dataGridViewCells)
			{
				this.Add(dataGridViewCell);
			}
		}

		/// <summary>Clears all cells from the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001243 RID: 4675 RVA: 0x0004813C File Offset: 0x0004633C
		public virtual void Clear()
		{
			base.List.Clear();
		}

		/// <summary>Determines whether the specified cell is contained in the collection.</summary>
		/// <returns>true if <paramref name="dataGridViewCell" /> is in the collection; otherwise, false.</returns>
		/// <param name="dataGridViewCell">A <see cref="T:System.Windows.Forms.DataGridViewCell" /> to locate in the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001244 RID: 4676 RVA: 0x0004814C File Offset: 0x0004634C
		public virtual bool Contains(DataGridViewCell dataGridViewCell)
		{
			return base.List.Contains(dataGridViewCell);
		}

		/// <summary>Copies the entire collection of cells into an array at a specified location within the array.</summary>
		/// <param name="array">The destination array to which the contents will be copied.</param>
		/// <param name="index">The index of the element in <paramref name="array" /> at which to start copying.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001245 RID: 4677 RVA: 0x0004815C File Offset: 0x0004635C
		public void CopyTo(DataGridViewCell[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Returns the index of the specified cell.</summary>
		/// <returns>The zero-based index of the value of <paramref name="dataGridViewCell" /> parameter, if it is found in the collection; otherwise, -1.</returns>
		/// <param name="dataGridViewCell">The cell to locate in the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001246 RID: 4678 RVA: 0x0004816C File Offset: 0x0004636C
		public int IndexOf(DataGridViewCell dataGridViewCell)
		{
			return base.List.IndexOf(dataGridViewCell);
		}

		/// <summary>Inserts a cell into the collection at the specified index. </summary>
		/// <param name="index">The zero-based index at which to place <paramref name="dataGridViewCell" />.</param>
		/// <param name="dataGridViewCell">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to insert.</param>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-<paramref name="dataGridViewCell" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001247 RID: 4679 RVA: 0x0004817C File Offset: 0x0004637C
		public virtual void Insert(int index, DataGridViewCell dataGridViewCell)
		{
			base.List.Insert(index, dataGridViewCell);
			dataGridViewCell.SetOwningRow(this.dataGridViewRow);
			dataGridViewCell.SetColumnIndex(index);
			dataGridViewCell.SetDataGridView(this.dataGridViewRow.DataGridView);
			this.OnCollectionChanged(new CollectionChangeEventArgs(1, dataGridViewCell));
		}

		/// <summary>Removes the specified cell from the collection.</summary>
		/// <param name="cell">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to remove from the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="cell" /> could not be found in the collection.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001248 RID: 4680 RVA: 0x000481C8 File Offset: 0x000463C8
		public virtual void Remove(DataGridViewCell cell)
		{
			base.List.Remove(cell);
			this.ReIndex();
			this.OnCollectionChanged(new CollectionChangeEventArgs(2, cell));
		}

		/// <summary>Removes the cell at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> to be removed.</param>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001249 RID: 4681 RVA: 0x000481F4 File Offset: 0x000463F4
		public virtual void RemoveAt(int index)
		{
			DataGridViewCell dataGridViewCell = this[index];
			base.List.RemoveAt(index);
			this.ReIndex();
			this.OnCollectionChanged(new CollectionChangeEventArgs(2, dataGridViewCell));
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00048228 File Offset: 0x00046428
		private void ReIndex()
		{
			for (int i = 0; i < base.List.Count; i++)
			{
				this[i].SetColumnIndex(i);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ArrayList" /> containing <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> objects.</summary>
		/// <returns>
		///   <see cref="T:System.Collections.ArrayList" />.</returns>
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x00048260 File Offset: 0x00046460
		protected override ArrayList List
		{
			get
			{
				return base.List;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridViewCellCollection.CollectionChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x0600124C RID: 4684 RVA: 0x00048268 File Offset: 0x00046468
		protected void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged.Invoke(this, e);
			}
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00048284 File Offset: 0x00046484
		virtual bool System.Collections.IList.get_IsReadOnly()
		{
			return base.IsReadOnly;
		}

		// Token: 0x04000AF7 RID: 2807
		private DataGridViewRow dataGridViewRow;
	}
}
