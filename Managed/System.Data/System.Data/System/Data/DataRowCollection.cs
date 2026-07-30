using System;
using System.Collections;
using Unity;

namespace System.Data
{
	/// <summary>Represents a collection of rows for a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000080 RID: 128
	public sealed class DataRowCollection : InternalDataCollectionBase
	{
		// Token: 0x06000649 RID: 1609 RVA: 0x0001987C File Offset: 0x00017A7C
		internal DataRowCollection(DataTable table)
		{
			this._list = new DataRowCollection.DataRowTree();
			base..ctor();
			this._table = table;
		}

		/// <summary>Gets the total number of <see cref="T:System.Data.DataRow" /> objects in this collection.</summary>
		/// <returns>The total number of <see cref="T:System.Data.DataRow" /> objects in this collection.</returns>
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00019896 File Offset: 0x00017A96
		public override int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets the row at the specified index.</summary>
		/// <returns>The specified <see cref="T:System.Data.DataRow" />.</returns>
		/// <param name="index">The zero-based index of the row to return. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000132 RID: 306
		public DataRow this[int index]
		{
			get
			{
				return this._list[index];
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.DataRow" /> to the <see cref="T:System.Data.DataRowCollection" /> object.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The row is null. </exception>
		/// <exception cref="T:System.ArgumentException">The row either belongs to another table or already belongs to this table.</exception>
		/// <exception cref="T:System.Data.ConstraintException">The addition invalidates a constraint. </exception>
		/// <exception cref="T:System.Data.NoNullAllowedException">The addition tries to put a null in a <see cref="T:System.Data.DataColumn" /> where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is false.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600064C RID: 1612 RVA: 0x000198B1 File Offset: 0x00017AB1
		public void Add(DataRow row)
		{
			this._table.AddRow(row, -1);
		}

		/// <summary>Inserts a new row into the collection at the specified location.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to add. </param>
		/// <param name="pos">The (zero-based) location in the collection where you want to add the DataRow. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600064D RID: 1613 RVA: 0x000198C0 File Offset: 0x00017AC0
		public void InsertAt(DataRow row, int pos)
		{
			if (pos < 0)
			{
				throw ExceptionBuilder.RowInsertOutOfRange(pos);
			}
			if (pos >= this._list.Count)
			{
				this._table.AddRow(row, -1);
				return;
			}
			this._table.InsertRow(row, -1, pos);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000198F8 File Offset: 0x00017AF8
		internal void DiffInsertAt(DataRow row, int pos)
		{
			if (pos < 0 || pos == this._list.Count)
			{
				this._table.AddRow(row, (pos > -1) ? (pos + 1) : (-1));
				return;
			}
			if (this._table.NestedParentRelations.Length == 0)
			{
				this._table.InsertRow(row, pos + 1, (pos > this._list.Count) ? (-1) : pos);
				return;
			}
			if (pos >= this._list.Count)
			{
				while (pos > this._list.Count)
				{
					this._list.Add(null);
					this._nullInList++;
				}
				this._table.AddRow(row, pos + 1);
				return;
			}
			if (this._list[pos] != null)
			{
				throw ExceptionBuilder.RowInsertTwice(pos, this._table.TableName);
			}
			this._list.RemoveAt(pos);
			this._nullInList--;
			this._table.InsertRow(row, pos + 1, pos);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Data.DataRow" /> object.</summary>
		/// <returns>The zero-based index of the row, or -1 if the row is not found in the collection.</returns>
		/// <param name="row">The DataRow to search for.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600064F RID: 1615 RVA: 0x000199F2 File Offset: 0x00017BF2
		public int IndexOf(DataRow row)
		{
			if (row != null && row.Table == this._table && (row.RBTreeNodeId != 0 || row.RowState != DataRowState.Detached))
			{
				return this._list.IndexOf(row.RBTreeNodeId, row);
			}
			return -1;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00019A2C File Offset: 0x00017C2C
		internal DataRow AddWithColumnEvents(params object[] values)
		{
			DataRow dataRow = this._table.NewRow(-1);
			dataRow.ItemArray = values;
			this._table.AddRow(dataRow, -1);
			return dataRow;
		}

		/// <summary>Creates a row using specified values and adds it to the <see cref="T:System.Data.DataRowCollection" />.</summary>
		/// <returns>None.</returns>
		/// <param name="values">The array of values that are used to create the new row. </param>
		/// <exception cref="T:System.ArgumentException">The array is larger than the number of columns in the table.</exception>
		/// <exception cref="T:System.InvalidCastException">A value does not match its respective column type. </exception>
		/// <exception cref="T:System.Data.ConstraintException">Adding the row invalidates a constraint. </exception>
		/// <exception cref="T:System.Data.NoNullAllowedException">Trying to put a null in a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is false. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000651 RID: 1617 RVA: 0x00019A5C File Offset: 0x00017C5C
		public DataRow Add(params object[] values)
		{
			int num = this._table.NewRecordFromArray(values);
			DataRow dataRow = this._table.NewRow(num);
			this._table.AddRow(dataRow, -1);
			return dataRow;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00019A91 File Offset: 0x00017C91
		internal void ArrayAdd(DataRow row)
		{
			row.RBTreeNodeId = this._list.Add(row);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00019AA5 File Offset: 0x00017CA5
		internal void ArrayInsert(DataRow row, int pos)
		{
			row.RBTreeNodeId = this._list.Insert(pos, row);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00019ABA File Offset: 0x00017CBA
		internal void ArrayClear()
		{
			this._list.Clear();
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00019AC7 File Offset: 0x00017CC7
		internal void ArrayRemove(DataRow row)
		{
			if (row.RBTreeNodeId == 0)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.AttachedNodeWithZerorbTreeNodeId);
			}
			this._list.RBDelete(row.RBTreeNodeId);
			row.RBTreeNodeId = 0;
		}

		/// <summary>Gets the row specified by the primary key value.</summary>
		/// <returns>A <see cref="T:System.Data.DataRow" /> that contains the primary key value specified; otherwise a null value if the primary key value does not exist in the <see cref="T:System.Data.DataRowCollection" />.</returns>
		/// <param name="key">The primary key value of the <see cref="T:System.Data.DataRow" /> to find. </param>
		/// <exception cref="T:System.Data.MissingPrimaryKeyException">The table does not have a primary key. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000656 RID: 1622 RVA: 0x00019AF2 File Offset: 0x00017CF2
		public DataRow Find(object key)
		{
			return this._table.FindByPrimaryKey(key);
		}

		/// <summary>Gets the row that contains the specified primary key values.</summary>
		/// <returns>A <see cref="T:System.Data.DataRow" /> object that contains the primary key values specified; otherwise a null value if the primary key value does not exist in the <see cref="T:System.Data.DataRowCollection" />.</returns>
		/// <param name="keys">An array of primary key values to find. The type of the array is Object. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">No row corresponds to that index value. </exception>
		/// <exception cref="T:System.Data.MissingPrimaryKeyException">The table does not have a primary key. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000657 RID: 1623 RVA: 0x00019B00 File Offset: 0x00017D00
		public DataRow Find(object[] keys)
		{
			return this._table.FindByPrimaryKey(keys);
		}

		/// <summary>Clears the collection of all rows.</summary>
		/// <exception cref="T:System.Data.InvalidConstraintException">A <see cref="T:System.Data.ForeignKeyConstraint" /> is enforced on the <see cref="T:System.Data.DataRowCollection" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000658 RID: 1624 RVA: 0x00019B0E File Offset: 0x00017D0E
		public void Clear()
		{
			this._table.Clear(false);
		}

		/// <summary>Gets a value that indicates whether the primary key of any row in the collection contains the specified value.</summary>
		/// <returns>true if the collection contains a <see cref="T:System.Data.DataRow" /> with the specified primary key value; otherwise, false.</returns>
		/// <param name="key">The value of the primary key to test for. </param>
		/// <exception cref="T:System.Data.MissingPrimaryKeyException">The table does not have a primary key. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000659 RID: 1625 RVA: 0x00019B1C File Offset: 0x00017D1C
		public bool Contains(object key)
		{
			return this._table.FindByPrimaryKey(key) != null;
		}

		/// <summary>Gets a value that indicates whether the primary key columns of any row in the collection contain the values specified in the object array.</summary>
		/// <returns>true if the <see cref="T:System.Data.DataRowCollection" /> contains a <see cref="T:System.Data.DataRow" /> with the specified key values; otherwise, false.</returns>
		/// <param name="keys">An array of primary key values to test for. </param>
		/// <exception cref="T:System.Data.MissingPrimaryKeyException">The table does not have a primary key. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600065A RID: 1626 RVA: 0x00019B2D File Offset: 0x00017D2D
		public bool Contains(object[] keys)
		{
			return this._table.FindByPrimaryKey(keys) != null;
		}

		/// <summary>Copies all the <see cref="T:System.Data.DataRow" /> objects from the collection into the given array, starting at the given destination array index.</summary>
		/// <param name="ar">The one-dimensional array that is the destination of the elements copied from the DataRowCollection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		// Token: 0x0600065B RID: 1627 RVA: 0x00019B3E File Offset: 0x00017D3E
		public override void CopyTo(Array ar, int index)
		{
			this._list.CopyTo(ar, index);
		}

		/// <summary>Copies all the <see cref="T:System.Data.DataRow" /> objects from the collection into the given array, starting at the given destination array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the DataRowCollection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600065C RID: 1628 RVA: 0x00019B4D File Offset: 0x00017D4D
		public void CopyTo(DataRow[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> for this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for this collection.</returns>
		// Token: 0x0600065D RID: 1629 RVA: 0x00019B5C File Offset: 0x00017D5C
		public override IEnumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		/// <summary>Removes the specified <see cref="T:System.Data.DataRow" /> from the collection.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to remove. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600065E RID: 1630 RVA: 0x00019B6C File Offset: 0x00017D6C
		public void Remove(DataRow row)
		{
			if (row == null || row.Table != this._table || -1L == row.rowID)
			{
				throw ExceptionBuilder.RowOutOfRange();
			}
			if (row.RowState != DataRowState.Deleted && row.RowState != DataRowState.Detached)
			{
				row.Delete();
			}
			if (row.RowState != DataRowState.Detached)
			{
				row.AcceptChanges();
			}
		}

		/// <summary>Removes the row at the specified index from the collection.</summary>
		/// <param name="index">The index of the row to remove. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600065F RID: 1631 RVA: 0x00019BC1 File Offset: 0x00017DC1
		public void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00010468 File Offset: 0x0000E668
		internal DataRowCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000597 RID: 1431
		private readonly DataTable _table;

		// Token: 0x04000598 RID: 1432
		private readonly DataRowCollection.DataRowTree _list;

		// Token: 0x04000599 RID: 1433
		internal int _nullInList;

		// Token: 0x02000081 RID: 129
		private sealed class DataRowTree : RBTree<DataRow>
		{
			// Token: 0x06000661 RID: 1633 RVA: 0x00019BD0 File Offset: 0x00017DD0
			internal DataRowTree()
				: base(TreeAccessMethod.INDEX_ONLY)
			{
			}

			// Token: 0x06000662 RID: 1634 RVA: 0x00019BD9 File Offset: 0x00017DD9
			protected override int CompareNode(DataRow record1, DataRow record2)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.CompareNodeInDataRowTree);
			}

			// Token: 0x06000663 RID: 1635 RVA: 0x00019BE2 File Offset: 0x00017DE2
			protected override int CompareSateliteTreeNode(DataRow record1, DataRow record2)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.CompareSateliteTreeNodeInDataRowTree);
			}
		}
	}
}
