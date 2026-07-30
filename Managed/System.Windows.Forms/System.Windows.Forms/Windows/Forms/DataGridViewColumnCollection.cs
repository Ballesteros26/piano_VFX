using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.DataGridViewColumn" /> objects in a <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FE RID: 254
	[ListBindable(false)]
	public class DataGridViewColumnCollection : BaseCollection, ICollection, IEnumerable, IList
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" /> class for the given <see cref="T:System.Windows.Forms.DataGridView" />. </summary>
		/// <param name="dataGridView">The <see cref="T:System.Windows.Forms.DataGridView" /> that created this collection.</param>
		// Token: 0x0600133D RID: 4925 RVA: 0x0004A364 File Offset: 0x00048564
		public DataGridViewColumnCollection(DataGridView dataGridView)
		{
			this.dataGridView = dataGridView;
			this.RegenerateSortedList();
		}

		/// <summary>Occurs when the collection changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400016C RID: 364
		// (add) Token: 0x0600133E RID: 4926 RVA: 0x0004A37C File Offset: 0x0004857C
		// (remove) Token: 0x0600133F RID: 4927 RVA: 0x0004A398 File Offset: 0x00048598
		public event CollectionChangeEventHandler CollectionChanged;

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0004A3B4 File Offset: 0x000485B4
		bool IList.IsFixedSize
		{
			get
			{
				return base.List.IsFixedSize;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the column to get.</param>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">When getting this property, <paramref name="index" /> is less than zero or greater than the number of columns in the collection minus one.</exception>
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x0004A3C4 File Offset: 0x000485C4
		// (set) Token: 0x06001342 RID: 4930 RVA: 0x0004A3D0 File Offset: 0x000485D0
		object IList.Item
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Adds an object to the end of the collection.</summary>
		/// <returns>The index at which <paramref name="value" /> has been added.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the end of the collection. The value can be null.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The column indicated by <paramref name="value" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value of the column indicated by <paramref name="value" /> is <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" /> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit" /> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit" /> methods to temporarily set conflicting property values. -or-The <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of the column indicated by <paramref name="value" /> is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> property value is false.-or-The column indicated by <paramref name="value" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value of true.-or-The column indicated by <paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property value that would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> values of all columns in the control to exceed 65535.-or-The column indicated by <paramref name="value" /> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> control contains at least one row and the column indicated by <paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType" /> property value of null.</exception>
		// Token: 0x06001343 RID: 4931 RVA: 0x0004A3D8 File Offset: 0x000485D8
		int IList.Add(object value)
		{
			return this.Add(value as DataGridViewColumn);
		}

		/// <summary>Determines whether an object is in the collection.</summary>
		/// <returns>true if <paramref name="value" /> is found in the <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection. The value can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06001344 RID: 4932 RVA: 0x0004A3E8 File Offset: 0x000485E8
		bool IList.Contains(object value)
		{
			return this.Contains(value as DataGridViewColumn);
		}

		/// <summary>Determines the index of a specific item in the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the collection, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection. The value can be null.</param>
		// Token: 0x06001345 RID: 4933 RVA: 0x0004A3F8 File Offset: 0x000485F8
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as DataGridViewColumn);
		}

		/// <summary>Inserts an element into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert. The value can be null.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The column indicated by <paramref name="value" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value of the column indicated by <paramref name="value" /> is <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" /> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit" /> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit" /> methods to temporarily set conflicting property values. -or-The <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of the column indicated by <paramref name="value" /> is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> property value is false.-or-The column indicated by <paramref name="value" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value of true.-or-The column indicated by <paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property value that would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> values of all columns in the control to exceed 65535.-or-The column indicated by <paramref name="value" /> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> control contains at least one row and the column indicated by <paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType" /> property value of null.</exception>
		// Token: 0x06001346 RID: 4934 RVA: 0x0004A408 File Offset: 0x00048608
		void IList.Insert(int index, object value)
		{
			this.Insert(index, value as DataGridViewColumn);
		}

		/// <summary>Removes the first occurrence of the specified object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the collection. The value can be null.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not in the collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
		// Token: 0x06001347 RID: 4935 RVA: 0x0004A418 File Offset: 0x00048618
		void IList.Remove(object value)
		{
			this.Remove(value as DataGridViewColumn);
		}

		/// <summary>Gets or sets the column at the given index in the collection. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> at the given index.</returns>
		/// <param name="index">The zero-based index of the column to get or set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the number of columns in the collection minus one.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000457 RID: 1111
		public DataGridViewColumn this[int index]
		{
			get
			{
				return (DataGridViewColumn)base.List[index];
			}
		}

		/// <summary>Gets or sets the column of the given name in the collection. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> identified by the <paramref name="columnName" /> parameter.</returns>
		/// <param name="columnName">The name of the column to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="columnName" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000458 RID: 1112
		public DataGridViewColumn this[string columnName]
		{
			get
			{
				foreach (object obj in base.List)
				{
					DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj;
					if (dataGridViewColumn.Name == columnName)
					{
						return dataGridViewColumn;
					}
				}
				return null;
			}
		}

		/// <summary>Adds the given column to the collection.</summary>
		/// <returns>The index of the column.</returns>
		/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewColumn" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-<paramref name="dataGridViewColumn" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The <paramref name="dataGridViewColumn" /><see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" /> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit" /> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit" /> methods to temporarily set conflicting property values. -or-The <paramref name="dataGridViewColumn" /><see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> property value is false.-or-<paramref name="dataGridViewColumn" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value of true.-or-<paramref name="dataGridViewColumn" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property value that would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> values of all columns in the control to exceed 65535.-or-<paramref name="dataGridViewColumn" /> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> control contains at least one row and <paramref name="dataGridViewColumn" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType" /> property value of null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600134A RID: 4938 RVA: 0x0004A4C0 File Offset: 0x000486C0
		public virtual int Add(DataGridViewColumn dataGridViewColumn)
		{
			int num = base.List.Add(dataGridViewColumn);
			dataGridViewColumn.SetIndex(num);
			dataGridViewColumn.SetDataGridView(this.dataGridView);
			this.OnCollectionChanged(new CollectionChangeEventArgs(1, dataGridViewColumn));
			return num;
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.DataGridViewTextBoxColumn" /> with the given column name and column header text to the collection.</summary>
		/// <returns>The index of the column.</returns>
		/// <param name="columnName">The name by which the column will be referred.</param>
		/// <param name="headerText">The text for the column's header.</param>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />, which conflicts with the default column <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" />.-or-The default column <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property value of 100 would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> values of all columns in the control to exceed 65535.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600134B RID: 4939 RVA: 0x0004A4FC File Offset: 0x000486FC
		[DesignerSerializationVisibility(0)]
		public virtual int Add(string columnName, string headerText)
		{
			return this.Add(new DataGridViewTextBoxColumn
			{
				Name = columnName,
				HeaderText = headerText
			});
		}

		/// <summary>Adds a range of columns to the collection. </summary>
		/// <param name="dataGridViewColumns">An array of <see cref="T:System.Windows.Forms.DataGridViewColumn" /> objects to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewColumns" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-At least one of the values in <paramref name="dataGridViewColumns" /> is null.-or-At least one of the columns in <paramref name="dataGridViewColumns" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-At least one of the columns in <paramref name="dataGridViewColumns" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType" /> property value of null and the <see cref="T:System.Windows.Forms.DataGridView" /> control contains at least one row.-or-At least one of the columns in <paramref name="dataGridViewColumns" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" /> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit" /> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit" /> methods to temporarily set conflicting property values. -or-At least one of the columns in <paramref name="dataGridViewColumns" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> property value is false.-or-At least one of the columns in <paramref name="dataGridViewColumns" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value of true.-or-The columns in <paramref name="dataGridViewColumns" /> have <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property values that would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> values of all columns in the control to exceed 65535.-or-At least two of the values in <paramref name="dataGridViewColumns" /> are references to the same <see cref="T:System.Windows.Forms.DataGridViewColumn" />.-or-At least one of the columns in <paramref name="dataGridViewColumns" /> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600134C RID: 4940 RVA: 0x0004A524 File Offset: 0x00048724
		public virtual void AddRange(params DataGridViewColumn[] dataGridViewColumns)
		{
			foreach (DataGridViewColumn dataGridViewColumn in dataGridViewColumns)
			{
				this.Add(dataGridViewColumn);
			}
		}

		/// <summary>Clears the collection. </summary>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600134D RID: 4941 RVA: 0x0004A554 File Offset: 0x00048754
		public virtual void Clear()
		{
			base.List.Clear();
			this.dataGridView.Rows.Clear();
			this.dataGridView.RemoveEditingRow();
			this.RegenerateSortedList();
			this.OnCollectionChanged(new CollectionChangeEventArgs(3, null));
		}

		/// <summary>Determines whether the collection contains the given column.</summary>
		/// <returns>true if the given column is in the collection; otherwise, false.</returns>
		/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to look for.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600134E RID: 4942 RVA: 0x0004A59C File Offset: 0x0004879C
		public virtual bool Contains(DataGridViewColumn dataGridViewColumn)
		{
			return base.List.Contains(dataGridViewColumn);
		}

		/// <summary>Determines whether the collection contains the column referred to by the given name. </summary>
		/// <returns>true if the column is contained in the collection; otherwise, false.</returns>
		/// <param name="columnName">The name of the column to look for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="columnName" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600134F RID: 4943 RVA: 0x0004A5AC File Offset: 0x000487AC
		public virtual bool Contains(string columnName)
		{
			foreach (object obj in base.List)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj;
				if (dataGridViewColumn.Name == columnName)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the items from the collection to the given array.</summary>
		/// <param name="array">The destination <see cref="T:System.Windows.Forms.DataGridViewColumn" /> array.</param>
		/// <param name="index">The index of the destination array at which to start copying.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001350 RID: 4944 RVA: 0x0004A630 File Offset: 0x00048830
		public void CopyTo(DataGridViewColumn[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Returns the number of columns that meet the given filter requirements.</summary>
		/// <returns>The number of columns that meet the filter requirements.</returns>
		/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter for inclusion.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001351 RID: 4945 RVA: 0x0004A640 File Offset: 0x00048840
		public int GetColumnCount(DataGridViewElementStates includeFilter)
		{
			return 0;
		}

		/// <summary>Returns the width, in pixels, required to display all of the columns that meet the given filter requirements. </summary>
		/// <returns>The width, in pixels, that is necessary to display all of the columns that meet the filter requirements.</returns>
		/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter for inclusion.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001352 RID: 4946 RVA: 0x0004A644 File Offset: 0x00048844
		public int GetColumnsWidth(DataGridViewElementStates includeFilter)
		{
			return 0;
		}

		/// <summary>Returns the first column in display order that meets the given inclusion-filter requirements.</summary>
		/// <returns>The first column in display order that meets the given filter requirements, or null if no column is found.</returns>
		/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represents the filter for inclusion.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001353 RID: 4947 RVA: 0x0004A648 File Offset: 0x00048848
		public DataGridViewColumn GetFirstColumn(DataGridViewElementStates includeFilter)
		{
			return null;
		}

		/// <summary>Returns the first column in display order that meets the given inclusion-filter and exclusion-filter requirements. </summary>
		/// <returns>The first column in display order that meets the given filter requirements, or null if no column is found.</returns>
		/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for inclusion.</param>
		/// <param name="excludeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for exclusion.</param>
		/// <exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001354 RID: 4948 RVA: 0x0004A64C File Offset: 0x0004884C
		public DataGridViewColumn GetFirstColumn(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			return null;
		}

		/// <summary>Returns the last column in display order that meets the given filter requirements. </summary>
		/// <returns>The last displayed column in display order that meets the given filter requirements, or null if no column is found.</returns>
		/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for inclusion.</param>
		/// <param name="excludeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for exclusion.</param>
		/// <exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001355 RID: 4949 RVA: 0x0004A650 File Offset: 0x00048850
		public DataGridViewColumn GetLastColumn(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			return null;
		}

		/// <summary>Gets the first column after the given column in display order that meets the given filter requirements. </summary>
		/// <returns>The next column that meets the given filter requirements, or null if no column is found.</returns>
		/// <param name="dataGridViewColumnStart">The column from which to start searching for the next column.</param>
		/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for inclusion.</param>
		/// <param name="excludeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for exclusion.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewColumnStart" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001356 RID: 4950 RVA: 0x0004A654 File Offset: 0x00048854
		public DataGridViewColumn GetNextColumn(DataGridViewColumn dataGridViewColumnStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			return null;
		}

		/// <summary>Gets the last column prior to the given column in display order that meets the given filter requirements. </summary>
		/// <returns>The previous column that meets the given filter requirements, or null if no column is found.</returns>
		/// <param name="dataGridViewColumnStart">The column from which to start searching for the previous column.</param>
		/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for inclusion.</param>
		/// <param name="excludeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for exclusion.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewColumnStart" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001357 RID: 4951 RVA: 0x0004A658 File Offset: 0x00048858
		public DataGridViewColumn GetPreviousColumn(DataGridViewColumn dataGridViewColumnStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			return null;
		}

		/// <summary>Gets the index of the given <see cref="T:System.Windows.Forms.DataGridViewColumn" /> in the collection.</summary>
		/// <returns>The index of the given <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</returns>
		/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to return the index of.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001358 RID: 4952 RVA: 0x0004A65C File Offset: 0x0004885C
		public int IndexOf(DataGridViewColumn dataGridViewColumn)
		{
			return base.List.IndexOf(dataGridViewColumn);
		}

		/// <summary>Inserts a column at the given index in the collection.</summary>
		/// <param name="columnIndex">The zero-based index at which to insert the given column.</param>
		/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to insert.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewColumn" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-<paramref name="dataGridViewColumn" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The <paramref name="dataGridViewColumn" /><see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" /> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit" /> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit" /> methods to temporarily set conflicting property values. -or-The <paramref name="dataGridViewColumn" /><see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> property value is false.-or-<paramref name="dataGridViewColumn" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value of true.-or-<paramref name="dataGridViewColumn" /> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> control contains at least one row and <paramref name="dataGridViewColumn" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType" /> property value of null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001359 RID: 4953 RVA: 0x0004A66C File Offset: 0x0004886C
		public virtual void Insert(int columnIndex, DataGridViewColumn dataGridViewColumn)
		{
			base.List.Insert(columnIndex, dataGridViewColumn);
			dataGridViewColumn.SetIndex(columnIndex);
			dataGridViewColumn.SetDataGridView(this.dataGridView);
			this.OnCollectionChanged(new CollectionChangeEventArgs(1, dataGridViewColumn));
		}

		/// <summary>Removes the specified column from the collection.</summary>
		/// <param name="dataGridViewColumn">The column to delete.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dataGridViewColumn" /> is not in the collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewColumn" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600135A RID: 4954 RVA: 0x0004A6A8 File Offset: 0x000488A8
		public virtual void Remove(DataGridViewColumn dataGridViewColumn)
		{
			this.DataGridView.OnColumnPreRemovedInternal(new DataGridViewColumnEventArgs(dataGridViewColumn));
			base.List.Remove(dataGridViewColumn);
			this.OnCollectionChanged(new CollectionChangeEventArgs(2, dataGridViewColumn));
		}

		/// <summary>Removes the column with the specified name from the collection.</summary>
		/// <param name="columnName">The name of the column to delete.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="columnName" /> does not match the name of any column in the collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="columnName" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600135B RID: 4955 RVA: 0x0004A6E0 File Offset: 0x000488E0
		public virtual void Remove(string columnName)
		{
			foreach (object obj in base.List)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj;
				if (dataGridViewColumn.Name == columnName)
				{
					this.Remove(dataGridViewColumn);
					break;
				}
			}
		}

		/// <summary>Removes the column at the given index in the collection.</summary>
		/// <param name="index">The index of the column to delete.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the number of columns in the control minus one. </exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600135C RID: 4956 RVA: 0x0004A768 File Offset: 0x00048968
		public virtual void RemoveAt(int index)
		{
			DataGridViewColumn dataGridViewColumn = this[index];
			this.Remove(dataGridViewColumn);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridView" /> upon which the collection performs column-related operations.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x0004A784 File Offset: 0x00048984
		protected DataGridView DataGridView
		{
			get
			{
				return this.dataGridView;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridViewColumnCollection.CollectionChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data.</param>
		// Token: 0x0600135E RID: 4958 RVA: 0x0004A78C File Offset: 0x0004898C
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			this.RegenerateIndexes();
			this.RegenerateSortedList();
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged.Invoke(this, e);
			}
		}

		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the elements of the collection. This property returns null unless overridden in a derived class.</returns>
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x0004A7C0 File Offset: 0x000489C0
		protected override ArrayList List
		{
			get
			{
				return base.List;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x0004A7C8 File Offset: 0x000489C8
		internal List<DataGridViewColumn> ColumnDisplayIndexSortedArrayList
		{
			get
			{
				return this.display_index_sorted;
			}
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0004A7D0 File Offset: 0x000489D0
		private void RegenerateIndexes()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].SetIndex(i);
			}
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0004A804 File Offset: 0x00048A04
		internal void RegenerateSortedList()
		{
			DataGridViewColumn[] array = (DataGridViewColumn[])base.List.ToArray(typeof(DataGridViewColumn));
			List<DataGridViewColumn> list = new List<DataGridViewColumn>(array);
			list.Sort(new DataGridViewColumnCollection.ColumnDisplayIndexComparator());
			for (int i = 0; i < list.Count; i++)
			{
				list[i].DisplayIndex = i;
			}
			this.display_index_sorted = list;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0004A86C File Offset: 0x00048A6C
		internal void ClearAutoGeneratedColumns()
		{
			for (int i = this.list.Count - 1; i >= 0; i--)
			{
				if ((this.list[i] as DataGridViewColumn).AutoGenerated)
				{
					this.RemoveAt(i);
				}
			}
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0004A8BC File Offset: 0x00048ABC
		virtual bool System.Collections.IList.get_IsReadOnly()
		{
			return base.IsReadOnly;
		}

		// Token: 0x04000B5B RID: 2907
		private DataGridView dataGridView;

		// Token: 0x04000B5C RID: 2908
		private List<DataGridViewColumn> display_index_sorted;

		// Token: 0x020000FF RID: 255
		private class ColumnDisplayIndexComparator : IComparer<DataGridViewColumn>
		{
			// Token: 0x06001366 RID: 4966 RVA: 0x0004A8CC File Offset: 0x00048ACC
			public int Compare(DataGridViewColumn o1, DataGridViewColumn o2)
			{
				return o1.DisplayIndex - o2.DisplayIndex;
			}
		}
	}
}
