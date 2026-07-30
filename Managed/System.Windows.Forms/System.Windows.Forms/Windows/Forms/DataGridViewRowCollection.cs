using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace System.Windows.Forms
{
	/// <summary>A collection of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000123 RID: 291
	[DesignerSerializer("System.Windows.Forms.Design.DataGridViewRowCollectionCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ListBindable(false)]
	public class DataGridViewRowCollection : ICollection, IEnumerable, IList
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> class. </summary>
		/// <param name="dataGridView">The <see cref="T:System.Windows.Forms.DataGridView" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		// Token: 0x060014EF RID: 5359 RVA: 0x0004EFA0 File Offset: 0x0004D1A0
		public DataGridViewRowCollection(DataGridView dataGridView)
		{
			this.dataGridView = dataGridView;
			this.list = new ArrayList();
		}

		/// <summary>Occurs when the contents of the collection change.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400016D RID: 365
		// (add) Token: 0x060014F0 RID: 5360 RVA: 0x0004EFC4 File Offset: 0x0004D1C4
		// (remove) Token: 0x060014F1 RID: 5361 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public event CollectionChangeEventHandler CollectionChanged;

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</returns>
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x0004EFFC File Offset: 0x0004D1FC
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x0004F004 File Offset: 0x0004D204
		bool IList.IsFixedSize
		{
			get
			{
				return this.list.IsFixedSize;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x0004F014 File Offset: 0x0004D214
		bool IList.IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x0004F024 File Offset: 0x0004D224
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.NotSupportedException">The user tried to set this property.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.- or -<paramref name="index" /> is equal to or greater than <see cref="P:System.Windows.Forms.DataGridViewRowCollection.Count" />.</exception>
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0004F034 File Offset: 0x0004D234
		// (set) Token: 0x060014F7 RID: 5367 RVA: 0x0004F040 File Offset: 0x0004D240
		object IList.Item
		{
			get
			{
				return this[index];
			}
			set
			{
				this.list[index] = value as DataGridViewRow;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</returns>
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x0004F054 File Offset: 0x0004D254
		object ICollection.SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.DataGridViewRow" /> to the collection.</summary>
		/// <returns>The index of the new <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the <paramref name="value" /> is not null.-or-<paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of true.-or-This operation would add a frozen row after unfrozen rows. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> has more cells than there are columns in the control.</exception>
		// Token: 0x060014F9 RID: 5369 RVA: 0x0004F064 File Offset: 0x0004D264
		int IList.Add(object value)
		{
			return this.Add(value as DataGridViewRow);
		}

		/// <summary>Determines whether the collection contains the specified item.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.Windows.Forms.DataGridViewRow" /> found in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />; otherwise, false.</returns>
		/// <param name="value">The item to locate in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		// Token: 0x060014FA RID: 5370 RVA: 0x0004F074 File Offset: 0x0004D274
		bool IList.Contains(object value)
		{
			return this.Contains(value as DataGridViewRow);
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> cannot be cast automatically to the type of <paramref name="array" />. </exception>
		// Token: 0x060014FB RID: 5371 RVA: 0x0004F084 File Offset: 0x0004D284
		void ICollection.CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060014FC RID: 5372 RVA: 0x0004F094 File Offset: 0x0004D294
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Returns the index of a specified item in the collection.</summary>
		/// <returns>The index of <paramref name="value" /> if it is a <see cref="T:System.Windows.Forms.DataGridViewRow" /> found in the list; otherwise, -1.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		// Token: 0x060014FD RID: 5373 RVA: 0x0004F0A4 File Offset: 0x0004D2A4
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as DataGridViewRow);
		}

		/// <summary>Inserts a <see cref="T:System.Windows.Forms.DataGridViewRow" /> into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to insert into the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the number of rows in the collection. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-<paramref name="index" /> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to true.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the <paramref name="value" /> is not null.-or-<paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of true.-or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> has more cells than there are columns in the control.</exception>
		// Token: 0x060014FE RID: 5374 RVA: 0x0004F0B4 File Offset: 0x0004D2B4
		void IList.Insert(int index, object value)
		{
			this.Insert(index, value as DataGridViewRow);
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to remove from the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not contained in this collection.-or-<paramref name="value" /> is a shared row.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-<paramref name="value" /> is the row for new records.-or-The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is bound to an <see cref="T:System.ComponentModel.IBindingList" /> implementation with <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> and <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" /> property values that are not both true. </exception>
		// Token: 0x060014FF RID: 5375 RVA: 0x0004F0C4 File Offset: 0x0004D2C4
		void IList.Remove(object value)
		{
			this.Remove(value as DataGridViewRow);
		}

		/// <summary>Gets the number of rows in the collection.</summary>
		/// <returns>The number of rows in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0004F0D4 File Offset: 0x0004D2D4
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewRow" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> at the specified index. Accessing a <see cref="T:System.Windows.Forms.DataGridViewRow" /> with this indexer causes the row to become unshared. To keep the row shared, use the <see cref="M:System.Windows.Forms.DataGridViewRowCollection.SharedRow(System.Int32)" /> method. For more information, see Best Practices for Scaling the Windows Forms DataGridView Control.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.- or -<paramref name="index" /> is equal to or greater than <see cref="P:System.Windows.Forms.DataGridViewRowCollection.Count" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004E7 RID: 1255
		public DataGridViewRow this[int index]
		{
			get
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)this.list[index];
				if (dataGridViewRow.Index == -1)
				{
					dataGridViewRow = (DataGridViewRow)dataGridViewRow.Clone();
					dataGridViewRow.SetIndex(index);
					this.list[index] = dataGridViewRow;
				}
				return dataGridViewRow;
			}
		}

		/// <summary>Adds a new row to the collection.</summary>
		/// <returns>The index of the new row.</returns>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-This operation would add a frozen row after unfrozen rows.</exception>
		/// <exception cref="T:System.ArgumentException">The row returned by the <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property has more cells than there are columns in the control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001502 RID: 5378 RVA: 0x0004F130 File Offset: 0x0004D330
		[DesignerSerializationVisibility(0)]
		public virtual int Add()
		{
			return this.Add(this.dataGridView.RowTemplateFull);
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0004F144 File Offset: 0x0004D344
		private int AddCore(DataGridViewRow dataGridViewRow, bool sharable)
		{
			if (this.dataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException("DataGridView has no columns.");
			}
			dataGridViewRow.SetDataGridView(this.dataGridView);
			int num = -1;
			if (this.DataGridView != null && this.DataGridView.EditingRow != null && this.DataGridView.EditingRow != dataGridViewRow)
			{
				num = this.list.Count - 1;
				this.DataGridView.EditingRow.SetIndex(this.list.Count);
			}
			int num2;
			if (num >= 0)
			{
				this.list.Insert(num, dataGridViewRow);
				num2 = num;
			}
			else
			{
				num2 = this.list.Add(dataGridViewRow);
			}
			if (sharable && this.CanBeShared(dataGridViewRow))
			{
				dataGridViewRow.SetIndex(-1);
			}
			else
			{
				dataGridViewRow.SetIndex(num2);
			}
			this.CompleteRowCells(dataGridViewRow);
			for (int i = 0; i < dataGridViewRow.Cells.Count; i++)
			{
				dataGridViewRow.Cells[i].SetOwningColumn(this.dataGridView.Columns[i]);
			}
			if (this.raiseEvent)
			{
				this.OnCollectionChanged(new CollectionChangeEventArgs(1, dataGridViewRow));
				this.DataGridView.OnRowsAddedInternal(new DataGridViewRowsAddedEventArgs(num2, 1));
			}
			return num2;
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0004F294 File Offset: 0x0004D494
		private void CompleteRowCells(DataGridViewRow row)
		{
			if (row == null || this.DataGridView == null)
			{
				return;
			}
			if (row.Cells.Count < this.DataGridView.ColumnCount)
			{
				for (int i = row.Cells.Count; i < this.DataGridView.ColumnCount; i++)
				{
					row.Cells.Add((DataGridViewCell)this.DataGridView.Columns[i].CellTemplate.Clone());
				}
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> to the collection.</summary>
		/// <returns>The index of the new <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the <paramref name="dataGridViewRow" /> is not null.-or-<paramref name="dataGridViewRow" /> has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of true. -or-This operation would add a frozen row after unfrozen rows.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewRow" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dataGridViewRow" /> has more cells than there are columns in the control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001505 RID: 5381 RVA: 0x0004F320 File Offset: 0x0004D520
		public virtual int Add(DataGridViewRow dataGridViewRow)
		{
			if (this.dataGridView.DataSource != null)
			{
				throw new InvalidOperationException("DataSource of DataGridView is not null.");
			}
			return this.AddCore(dataGridViewRow, true);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0004F348 File Offset: 0x0004D548
		private bool CanBeShared(DataGridViewRow row)
		{
			return false;
		}

		/// <summary>Adds the specified number of new rows to the collection.</summary>
		/// <returns>The index of the last row that was added.</returns>
		/// <param name="count">The number of rows to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-The row returned by the <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property has more cells than there are columns in the control. -or-This operation would add frozen rows after unfrozen rows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001507 RID: 5383 RVA: 0x0004F34C File Offset: 0x0004D54C
		[DesignerSerializationVisibility(0)]
		public virtual int Add(int count)
		{
			if (count <= 0)
			{
				throw new ArgumentOutOfRangeException("Count is less than or equeal to 0.");
			}
			if (this.dataGridView.DataSource != null)
			{
				throw new InvalidOperationException("DataSource of DataGridView is not null.");
			}
			if (this.dataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException("DataGridView has no columns.");
			}
			this.raiseEvent = false;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				num = this.Add(this.dataGridView.RowTemplateFull);
			}
			this.DataGridView.OnRowsAddedInternal(new DataGridViewRowsAddedEventArgs(num - count + 1, count));
			this.raiseEvent = true;
			return num;
		}

		/// <summary>Adds a new row to the collection, and populates the cells with the specified objects.</summary>
		/// <returns>The index of the new row.</returns>
		/// <param name="values">A variable number of objects that populate the cells of the new <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.VirtualMode" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to true.- or -The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns. -or-The row returned by the <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property has more cells than there are columns in the control.-or-This operation would add a frozen row after unfrozen rows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001508 RID: 5384 RVA: 0x0004F3F4 File Offset: 0x0004D5F4
		[DesignerSerializationVisibility(0)]
		public virtual int Add(params object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values is null.");
			}
			if (this.dataGridView.VirtualMode)
			{
				throw new InvalidOperationException("DataGridView is in virtual mode.");
			}
			DataGridViewRow rowTemplateFull = this.dataGridView.RowTemplateFull;
			int num = this.AddCore(rowTemplateFull, false);
			rowTemplateFull.SetValues(values);
			return num;
		}

		/// <summary>Adds the specified number of rows to the collection based on the row at the specified index.</summary>
		/// <returns>The index of the last row that was added.</returns>
		/// <param name="indexSource">The index of the row on which to base the new rows.</param>
		/// <param name="count">The number of rows to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexSource" /> is less than zero or greater than or equal to the number of rows in the control.-or-<paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-This operation would add a frozen row after unfrozen rows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001509 RID: 5385 RVA: 0x0004F44C File Offset: 0x0004D64C
		public virtual int AddCopies(int indexSource, int count)
		{
			this.raiseEvent = false;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				num = this.AddCopy(indexSource);
			}
			this.DataGridView.OnRowsAddedInternal(new DataGridViewRowsAddedEventArgs(num - count + 1, count));
			this.raiseEvent = true;
			return num;
		}

		/// <summary>Adds a new row based on the row at the specified index.</summary>
		/// <returns>The index of the new row.</returns>
		/// <param name="indexSource">The index of the row on which to base the new row.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexSource" /> is less than zero or greater than or equal to the number of rows in the collection.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-This operation would add a frozen row after unfrozen rows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600150A RID: 5386 RVA: 0x0004F49C File Offset: 0x0004D69C
		public virtual int AddCopy(int indexSource)
		{
			return this.Add((this.list[indexSource] as DataGridViewRow).Clone() as DataGridViewRow);
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects to the collection.</summary>
		/// <param name="dataGridViewRows">An array of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects to be added to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewRows" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dataGridViewRows" /> contains only one row, and the row it contains has more cells than there are columns in the control.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-At least one entry in the <paramref name="dataGridViewRows" /> array is null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-At least one row in the <paramref name="dataGridViewRows" /> array has a <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property value that is not null.-or-At least one row in the <paramref name="dataGridViewRows" /> array has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of true.-or-Two or more rows in the <paramref name="dataGridViewRows" /> array are identical.-or-At least one row in the <paramref name="dataGridViewRows" /> array contains one or more cells of a type that is incompatible with the type of the corresponding column in the control.-or-At least one row in the <paramref name="dataGridViewRows" /> array contains more cells than there are columns in the control.-or-This operation would add frozen rows after unfrozen rows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600150B RID: 5387 RVA: 0x0004F4C0 File Offset: 0x0004D6C0
		[DesignerSerializationVisibility(0)]
		public virtual void AddRange(params DataGridViewRow[] dataGridViewRows)
		{
			if (this.dataGridView.DataSource != null)
			{
				throw new InvalidOperationException("DataSource of DataGridView is not null.");
			}
			int num = 0;
			int num2 = -1;
			this.raiseEvent = false;
			foreach (DataGridViewRow dataGridViewRow in dataGridViewRows)
			{
				num2 = this.Add(dataGridViewRow);
				num++;
			}
			this.raiseEvent = true;
			this.DataGridView.OnRowsAddedInternal(new DataGridViewRowsAddedEventArgs(num2 - num + 1, num));
			this.OnCollectionChanged(new CollectionChangeEventArgs(1, dataGridViewRows));
		}

		/// <summary>Clears the collection. </summary>
		/// <exception cref="T:System.InvalidOperationException">The collection is data bound and the underlying data source does not support clearing the row data.-or-The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents the row collection from being modified:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600150C RID: 5388 RVA: 0x0004F548 File Offset: 0x0004D748
		public virtual void Clear()
		{
			int count = this.list.Count;
			this.DataGridView.OnRowsPreRemovedInternal(new DataGridViewRowsRemovedEventArgs(0, count));
			for (int i = 0; i < count; i++)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)this.list[0];
				if (dataGridViewRow.IsNewRow)
				{
					break;
				}
				this.list.Remove(dataGridViewRow);
				this.ReIndex();
			}
			this.DataGridView.OnRowsPostRemovedInternal(new DataGridViewRowsRemovedEventArgs(0, count));
			this.OnCollectionChanged(new CollectionChangeEventArgs(3, null));
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0004F5D8 File Offset: 0x0004D7D8
		internal void ClearInternal()
		{
			this.list.Clear();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> is in the collection.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.DataGridViewRow" /> is in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />; otherwise, false.</returns>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to locate in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600150E RID: 5390 RVA: 0x0004F5E8 File Offset: 0x0004D7E8
		public virtual bool Contains(DataGridViewRow dataGridViewRow)
		{
			return this.list.Contains(dataGridViewRow);
		}

		/// <summary>Copies the items from the collection into the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> array, starting at the specified index.</summary>
		/// <param name="array">A <see cref="T:System.Windows.Forms.DataGridViewRow" /> array that is the destination of the items copied from the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600150F RID: 5391 RVA: 0x0004F5F8 File Offset: 0x0004D7F8
		public void CopyTo(DataGridViewRow[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Returns the index of the first <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified criteria.</summary>
		/// <returns>The index of the first <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />; -1 if no row is found.</returns>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001510 RID: 5392 RVA: 0x0004F608 File Offset: 0x0004D808
		public int GetFirstRow(DataGridViewElementStates includeFilter)
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)this.list[i];
				if ((dataGridViewRow.State & includeFilter) != DataGridViewElementStates.None)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Returns the index of the first <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified inclusion and exclusion criteria.</summary>
		/// <returns>The index of the first <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />, and does not have the attributes specified by <paramref name="excludeFilter" />; -1 if no row is found.</returns>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <param name="excludeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <exception cref="T:System.ArgumentException">One or both of the specified filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001511 RID: 5393 RVA: 0x0004F654 File Offset: 0x0004D854
		public int GetFirstRow(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)this.list[i];
				if ((dataGridViewRow.State & includeFilter) != DataGridViewElementStates.None && (dataGridViewRow.State & excludeFilter) == DataGridViewElementStates.None)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Returns the index of the last <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified criteria.</summary>
		/// <returns>The index of the last <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />; -1 if no row is found.</returns>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001512 RID: 5394 RVA: 0x0004F6AC File Offset: 0x0004D8AC
		public int GetLastRow(DataGridViewElementStates includeFilter)
		{
			for (int i = this.list.Count - 1; i >= 0; i--)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)this.list[i];
				if ((dataGridViewRow.State & includeFilter) != DataGridViewElementStates.None)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Returns the index of the next <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified criteria.</summary>
		/// <returns>The index of the first <see cref="T:System.Windows.Forms.DataGridViewRow" /> after <paramref name="indexStart" /> that has the attributes specified by <paramref name="includeFilter" />, or -1 if no row is found.</returns>
		/// <param name="indexStart">The index of the row where the method should begin to look for the next <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexStart" /> is less than -1.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001513 RID: 5395 RVA: 0x0004F6FC File Offset: 0x0004D8FC
		public int GetNextRow(int indexStart, DataGridViewElementStates includeFilter)
		{
			for (int i = indexStart + 1; i < this.list.Count; i++)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)this.list[i];
				if ((dataGridViewRow.State & includeFilter) != DataGridViewElementStates.None)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Returns the index of the next <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified inclusion and exclusion criteria.</summary>
		/// <returns>The index of the next <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />, and does not have the attributes specified by <paramref name="excludeFilter" />; -1 if no row is found.</returns>
		/// <param name="indexStart">The index of the row where the method should begin to look for the next <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <param name="excludeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexStart" /> is less than -1.</exception>
		/// <exception cref="T:System.ArgumentException">One or both of the specified filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001514 RID: 5396 RVA: 0x0004F74C File Offset: 0x0004D94C
		public int GetNextRow(int indexStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			for (int i = indexStart + 1; i < this.list.Count; i++)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)this.list[i];
				if ((dataGridViewRow.State & includeFilter) != DataGridViewElementStates.None && (dataGridViewRow.State & excludeFilter) == DataGridViewElementStates.None)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Returns the index of the previous <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified criteria.</summary>
		/// <returns>The index of the previous <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />; -1 if no row is found.</returns>
		/// <param name="indexStart">The index of the row where the method should begin to look for the previous <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexStart" /> is greater than the number of rows in the collection.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001515 RID: 5397 RVA: 0x0004F7A8 File Offset: 0x0004D9A8
		public int GetPreviousRow(int indexStart, DataGridViewElementStates includeFilter)
		{
			for (int i = indexStart - 1; i >= 0; i--)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)this.list[i];
				if ((dataGridViewRow.State & includeFilter) != DataGridViewElementStates.None)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Returns the index of the previous <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified inclusion and exclusion criteria.</summary>
		/// <returns>The index of the previous <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />, and does not have the attributes specified by <paramref name="excludeFilter" />; -1 if no row is found.</returns>
		/// <param name="indexStart">The index of the row where the method should begin to look for the previous <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <param name="excludeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexStart" /> is greater than the number of rows in the collection.</exception>
		/// <exception cref="T:System.ArgumentException">One or both of the specified filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001516 RID: 5398 RVA: 0x0004F7EC File Offset: 0x0004D9EC
		public int GetPreviousRow(int indexStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			for (int i = indexStart - 1; i >= 0; i--)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)this.list[i];
				if ((dataGridViewRow.State & includeFilter) != DataGridViewElementStates.None && (dataGridViewRow.State & excludeFilter) == DataGridViewElementStates.None)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Returns the number of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects in the collection that meet the specified criteria.</summary>
		/// <returns>The number of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> that have the attributes specified by <paramref name="includeFilter" />.</returns>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001517 RID: 5399 RVA: 0x0004F83C File Offset: 0x0004DA3C
		public int GetRowCount(DataGridViewElementStates includeFilter)
		{
			int num = 0;
			foreach (object obj in this.list)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				if ((dataGridViewRow.State & includeFilter) != DataGridViewElementStates.None)
				{
					num++;
				}
			}
			return num;
		}

		/// <summary>Returns the cumulative height of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects that meet the specified criteria.</summary>
		/// <returns>The cumulative height of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> that have the attributes specified by <paramref name="includeFilter" />.</returns>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001518 RID: 5400 RVA: 0x0004F8B8 File Offset: 0x0004DAB8
		public int GetRowsHeight(DataGridViewElementStates includeFilter)
		{
			int num = 0;
			foreach (object obj in this.list)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				if ((dataGridViewRow.State & includeFilter) != DataGridViewElementStates.None)
				{
					num += dataGridViewRow.Height;
				}
			}
			return num;
		}

		/// <summary>Gets the state of the row with the specified index.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the state of the specified row.</returns>
		/// <param name="rowIndex">The index of the row.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than zero and greater than the number of rows in the collection minus one.</exception>
		// Token: 0x06001519 RID: 5401 RVA: 0x0004F93C File Offset: 0x0004DB3C
		public virtual DataGridViewElementStates GetRowState(int rowIndex)
		{
			return (this.list[rowIndex] as DataGridViewRow).State;
		}

		/// <summary>Returns the index of a specified item in the collection.</summary>
		/// <returns>The index of <paramref name="value" /> if it is a <see cref="T:System.Windows.Forms.DataGridViewRow" /> found in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />; otherwise, -1.</returns>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to locate in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600151A RID: 5402 RVA: 0x0004F954 File Offset: 0x0004DB54
		public int IndexOf(DataGridViewRow dataGridViewRow)
		{
			return this.list.IndexOf(dataGridViewRow);
		}

		/// <summary>Inserts the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> into the collection.</summary>
		/// <param name="rowIndex">The position at which to insert the row.</param>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to insert into the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the collection. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewRow" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-<paramref name="rowIndex" /> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to true.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of <paramref name="dataGridViewRow" /> is not null.-or-<paramref name="dataGridViewRow" /> has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of true. -or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dataGridViewRow" /> has more cells than there are columns in the control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600151B RID: 5403 RVA: 0x0004F964 File Offset: 0x0004DB64
		public virtual void Insert(int rowIndex, DataGridViewRow dataGridViewRow)
		{
			dataGridViewRow.SetIndex(rowIndex);
			dataGridViewRow.SetDataGridView(this.dataGridView);
			this.CompleteRowCells(dataGridViewRow);
			this.list.Insert(rowIndex, dataGridViewRow);
			this.ReIndex();
			this.OnCollectionChanged(new CollectionChangeEventArgs(1, dataGridViewRow));
			if (this.raiseEvent)
			{
				this.DataGridView.OnRowsAddedInternal(new DataGridViewRowsAddedEventArgs(rowIndex, 1));
			}
		}

		/// <summary>Inserts the specified number of rows into the collection at the specified location.</summary>
		/// <param name="rowIndex">The position at which to insert the rows.</param>
		/// <param name="count">The number of rows to insert into the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the collection. -or-<paramref name="count" /> is less than 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-<paramref name="rowIndex" /> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to true.-or-The row returned by the <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property has more cells than there are columns in the control. -or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600151C RID: 5404 RVA: 0x0004F9C8 File Offset: 0x0004DBC8
		public virtual void Insert(int rowIndex, int count)
		{
			int num = rowIndex;
			this.raiseEvent = false;
			for (int i = 0; i < count; i++)
			{
				this.Insert(num++, this.dataGridView.RowTemplateFull);
			}
			this.DataGridView.OnRowsAddedInternal(new DataGridViewRowsAddedEventArgs(rowIndex, count));
			this.raiseEvent = true;
		}

		/// <summary>Inserts a row into the collection at the specified position, and populates the cells with the specified objects.</summary>
		/// <param name="rowIndex">The position at which to insert the row.</param>
		/// <param name="values">A variable number of objects that populate the cells of the new row.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the collection. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.VirtualMode" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to true.-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-<paramref name="rowIndex" /> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to true.-or-The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the row returned by the control's <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property is not null. -or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
		/// <exception cref="T:System.ArgumentException">The row returned by the control's <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property has more cells than there are columns in the control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600151D RID: 5405 RVA: 0x0004FA20 File Offset: 0x0004DC20
		public virtual void Insert(int rowIndex, params object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("Values is null.");
			}
			if (this.dataGridView.VirtualMode || this.dataGridView.DataSource != null)
			{
				throw new InvalidOperationException();
			}
			DataGridViewRow dataGridViewRow = new DataGridViewRow();
			dataGridViewRow.SetValues(values);
			this.Insert(rowIndex, dataGridViewRow);
		}

		/// <summary>Inserts rows into the collection at the specified position.</summary>
		/// <param name="indexSource">The index of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> on which to base the new rows.</param>
		/// <param name="indexDestination">The position at which to insert the rows.</param>
		/// <param name="count">The number of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexSource" /> is less than zero or greater than the number of rows in the collection minus one.-or-<paramref name="indexDestination" /> is less than zero or greater than the number of rows in the collection.-or-<paramref name="count" /> is less than 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-<paramref name="indexDestination" /> is equal to the number of rows in the collection and <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> is true.-or-This operation would insert frozen rows after unfrozen rows or unfrozen rows before frozen rows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600151E RID: 5406 RVA: 0x0004FA7C File Offset: 0x0004DC7C
		public virtual void InsertCopies(int indexSource, int indexDestination, int count)
		{
			this.raiseEvent = false;
			int num = indexDestination;
			for (int i = 0; i < count; i++)
			{
				this.InsertCopy(indexSource, num++);
			}
			this.DataGridView.OnRowsAddedInternal(new DataGridViewRowsAddedEventArgs(indexDestination, count));
			this.raiseEvent = true;
		}

		/// <summary>Inserts a row into the collection at the specified position, based on the row at specified position.</summary>
		/// <param name="indexSource">The index of the row on which to base the new row.</param>
		/// <param name="indexDestination">The position at which to insert the row.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexSource" /> is less than zero or greater than the number of rows in the collection minus one.-or-<paramref name="indexDestination" /> is less than zero or greater than the number of rows in the collection.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-<paramref name="indexDestination" /> is equal to the number of rows in the collection and <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> is true. -or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600151F RID: 5407 RVA: 0x0004FACC File Offset: 0x0004DCCC
		public virtual void InsertCopy(int indexSource, int indexDestination)
		{
			this.Insert(indexDestination, new object[] { (this.list[indexSource] as DataGridViewRow).Clone() });
		}

		/// <summary>Inserts the <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects into the collection at the specified position.</summary>
		/// <param name="rowIndex">The position at which to insert the rows.</param>
		/// <param name="dataGridViewRows">An array of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewRows" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the collection.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dataGridViewRows" /> contains only one row, and the row it contains has more cells than there are columns in the control.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-<paramref name="rowIndex" /> is equal to the number of rows in the collection and <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> is true.-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-At least one entry in the <paramref name="dataGridViewRows" /> array is null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-At least one row in the <paramref name="dataGridViewRows" /> array has a <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property value that is not null.-or-At least one row in the <paramref name="dataGridViewRows" /> array has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of true.-or-Two or more rows in the <paramref name="dataGridViewRows" /> array are identical.-or-At least one row in the <paramref name="dataGridViewRows" /> array contains one or more cells of a type that is incompatible with the type of the corresponding column in the control.-or-At least one row in the <paramref name="dataGridViewRows" /> array contains more cells than there are columns in the control. -or-This operation would insert frozen rows after unfrozen rows or unfrozen rows before frozen rows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001520 RID: 5408 RVA: 0x0004FB00 File Offset: 0x0004DD00
		public virtual void InsertRange(int rowIndex, params DataGridViewRow[] dataGridViewRows)
		{
			this.raiseEvent = false;
			int num = rowIndex;
			int num2 = 0;
			foreach (DataGridViewRow dataGridViewRow in dataGridViewRows)
			{
				this.Insert(num++, dataGridViewRow);
				num2++;
			}
			this.DataGridView.OnRowsAddedInternal(new DataGridViewRowsAddedEventArgs(rowIndex, num2));
			this.raiseEvent = true;
		}

		/// <summary>Removes the row from the collection.</summary>
		/// <param name="dataGridViewRow">The row to remove from the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewRow" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dataGridViewRow" /> is not contained in this collection.-or-<paramref name="dataGridViewRow" /> is a shared row.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-<paramref name="dataGridViewRow" /> is the row for new records.-or-The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is bound to an <see cref="T:System.ComponentModel.IBindingList" /> implementation with <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> and <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" /> property values that are not both true. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001521 RID: 5409 RVA: 0x0004FB60 File Offset: 0x0004DD60
		public virtual void Remove(DataGridViewRow dataGridViewRow)
		{
			if (dataGridViewRow.IsNewRow)
			{
				throw new InvalidOperationException("Cannot delete the new row");
			}
			this.DataGridView.OnRowsPreRemovedInternal(new DataGridViewRowsRemovedEventArgs(dataGridViewRow.Index, 1));
			this.list.Remove(dataGridViewRow);
			this.ReIndex();
			this.OnCollectionChanged(new CollectionChangeEventArgs(2, dataGridViewRow));
			this.DataGridView.OnRowsPostRemovedInternal(new DataGridViewRowsRemovedEventArgs(dataGridViewRow.Index, 1));
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0004FBD0 File Offset: 0x0004DDD0
		internal virtual void RemoveInternal(DataGridViewRow dataGridViewRow)
		{
			this.DataGridView.OnRowsPreRemovedInternal(new DataGridViewRowsRemovedEventArgs(dataGridViewRow.Index, 1));
			this.list.Remove(dataGridViewRow);
			this.ReIndex();
			this.OnCollectionChanged(new CollectionChangeEventArgs(2, dataGridViewRow));
			this.DataGridView.OnRowsPostRemovedInternal(new DataGridViewRowsRemovedEventArgs(dataGridViewRow.Index, 1));
		}

		/// <summary>Removes the row at the specified position from the collection.</summary>
		/// <param name="index">The position of the row to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero and greater than the number of rows in the collection minus one. </exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-<paramref name="index" /> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to true.-or-The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is bound to an <see cref="T:System.ComponentModel.IBindingList" /> implementation with <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> and <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" /> property values that are not both true.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001523 RID: 5411 RVA: 0x0004FC2C File Offset: 0x0004DE2C
		public virtual void RemoveAt(int index)
		{
			DataGridViewRow dataGridViewRow = this[index];
			this.Remove(dataGridViewRow);
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0004FC48 File Offset: 0x0004DE48
		internal void RemoveAtInternal(int index)
		{
			DataGridViewRow dataGridViewRow = this[index];
			this.RemoveInternal(dataGridViewRow);
		}

		/// <summary>Returns the <see cref="T:System.Windows.Forms.DataGridViewRow" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> positioned at the specified index.</returns>
		/// <param name="rowIndex">The index of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> to get.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001525 RID: 5413 RVA: 0x0004FC64 File Offset: 0x0004DE64
		public DataGridViewRow SharedRow(int rowIndex)
		{
			return (DataGridViewRow)this.list[rowIndex];
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0004FC78 File Offset: 0x0004DE78
		internal int SharedRowIndexOf(DataGridViewRow row)
		{
			return this.list.IndexOf(row);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridView" /> that owns the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridView" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</returns>
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x0004FC88 File Offset: 0x0004DE88
		protected DataGridView DataGridView
		{
			get
			{
				return this.dataGridView;
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects.</summary>
		/// <returns>An array of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects.</returns>
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x0004FC90 File Offset: 0x0004DE90
		protected ArrayList List
		{
			get
			{
				return this.list;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridViewRowCollection.CollectionChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x06001529 RID: 5417 RVA: 0x0004FC98 File Offset: 0x0004DE98
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged.Invoke(this, e);
			}
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0004FCB4 File Offset: 0x0004DEB4
		internal void AddInternal(DataGridViewRow dataGridViewRow, bool sharable)
		{
			this.raiseEvent = false;
			this.AddCore(dataGridViewRow, sharable);
			this.raiseEvent = true;
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x0004FCD0 File Offset: 0x0004DED0
		internal ArrayList RowIndexSortedArrayList
		{
			get
			{
				ArrayList arrayList = (ArrayList)this.list.Clone();
				arrayList.Sort(new DataGridViewRowCollection.RowIndexComparator());
				return arrayList;
			}
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0004FCFC File Offset: 0x0004DEFC
		internal void ReIndex()
		{
			for (int i = 0; i < this.Count; i++)
			{
				(this.list[i] as DataGridViewRow).SetIndex(i);
			}
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0004FD38 File Offset: 0x0004DF38
		internal void Sort(IComparer comparer)
		{
			if (this.DataGridView != null && this.DataGridView.EditingRow != null)
			{
				this.list.Sort(0, this.Count - 1, comparer);
			}
			else
			{
				this.list.Sort(comparer);
			}
			for (int i = 0; i < this.list.Count; i++)
			{
				(this.list[i] as DataGridViewRow).SetIndex(i);
			}
		}

		// Token: 0x04000BF2 RID: 3058
		private ArrayList list;

		// Token: 0x04000BF3 RID: 3059
		private DataGridView dataGridView;

		// Token: 0x04000BF4 RID: 3060
		private bool raiseEvent = true;

		// Token: 0x02000124 RID: 292
		private class RowIndexComparator : IComparer
		{
			// Token: 0x0600152F RID: 5423 RVA: 0x0004FDC4 File Offset: 0x0004DFC4
			public int Compare(object o1, object o2)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)o1;
				DataGridViewRow dataGridViewRow2 = (DataGridViewRow)o2;
				if (dataGridViewRow.Index < dataGridViewRow2.Index)
				{
					return -1;
				}
				if (dataGridViewRow.Index > dataGridViewRow2.Index)
				{
					return 1;
				}
				return 0;
			}
		}
	}
}
