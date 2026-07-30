using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Xml;

namespace System.Data
{
	/// <summary>Represents a row of data in a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200007B RID: 123
	public class DataRow
	{
		/// <summary>Initializes a new instance of the DataRow. Constructs a row from the builder. Only for internal usage..</summary>
		/// <param name="builder">builder </param>
		// Token: 0x060005E2 RID: 1506 RVA: 0x00018034 File Offset: 0x00016234
		protected internal DataRow(DataRowBuilder builder)
		{
			this._tempRecord = builder._record;
			this._table = builder._table;
			this._columns = this._table.Columns;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00018096 File Offset: 0x00016296
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x000180A3 File Offset: 0x000162A3
		internal XmlBoundElement Element
		{
			get
			{
				return (XmlBoundElement)this._element;
			}
			set
			{
				this._element = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x000180AC File Offset: 0x000162AC
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x000180BF File Offset: 0x000162BF
		internal DataColumn LastChangedColumn
		{
			get
			{
				if (this._countColumnChange == 1)
				{
					return this._lastChangedColumn;
				}
				return null;
			}
			set
			{
				this._countColumnChange++;
				this._lastChangedColumn = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x000180D6 File Offset: 0x000162D6
		internal bool HasPropertyChanged
		{
			get
			{
				return 0 < this._countColumnChange;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x000180E1 File Offset: 0x000162E1
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x000180E9 File Offset: 0x000162E9
		internal int RBTreeNodeId
		{
			get
			{
				return this._rbTreeNodeId;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, int>("<ds.DataRow.set_RBTreeNodeId|INFO> {0}, value={1}", this._objectID, value);
				this._rbTreeNodeId = value;
			}
		}

		/// <summary>Gets or sets the custom error description for a row.</summary>
		/// <returns>The text describing an error.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00018108 File Offset: 0x00016308
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x00018124 File Offset: 0x00016324
		public string RowError
		{
			get
			{
				if (this._error != null)
				{
					return this._error.Text;
				}
				return string.Empty;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataRow.set_RowError|API> {0}, value='{1}'", this._objectID, value);
				if (this._error == null)
				{
					if (!string.IsNullOrEmpty(value))
					{
						this._error = new DataError(value);
					}
					this.RowErrorChanged();
					return;
				}
				if (this._error.Text != value)
				{
					this._error.Text = value;
					this.RowErrorChanged();
				}
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001818F File Offset: 0x0001638F
		private void RowErrorChanged()
		{
			if (this._oldRecord != -1)
			{
				this._table.RecordChanged(this._oldRecord);
			}
			if (this._newRecord != -1)
			{
				this._table.RecordChanged(this._newRecord);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x000181C5 File Offset: 0x000163C5
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x000181CD File Offset: 0x000163CD
		internal long rowID
		{
			get
			{
				return this._rowID;
			}
			set
			{
				this.ResetLastChangedColumn();
				this._rowID = value;
			}
		}

		/// <summary>Gets the current state of the row with regard to its relationship to the <see cref="T:System.Data.DataRowCollection" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowState" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x000181DC File Offset: 0x000163DC
		public DataRowState RowState
		{
			get
			{
				if (this._oldRecord == this._newRecord)
				{
					if (this._oldRecord == -1)
					{
						return DataRowState.Detached;
					}
					if (0 < this._columns.ColumnsImplementingIChangeTrackingCount)
					{
						foreach (DataColumn dataColumn in this._columns.ColumnsImplementingIChangeTracking)
						{
							object obj = this[dataColumn];
							if (DBNull.Value != obj && ((IChangeTracking)obj).IsChanged)
							{
								return DataRowState.Modified;
							}
						}
					}
					return DataRowState.Unchanged;
				}
				else
				{
					if (this._oldRecord == -1)
					{
						return DataRowState.Added;
					}
					if (this._newRecord == -1)
					{
						return DataRowState.Deleted;
					}
					return DataRowState.Modified;
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> for which this row has a schema.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> to which this row belongs.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00018268 File Offset: 0x00016468
		public DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		/// <summary>Gets or sets the data stored in the column specified by index.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
		/// <param name="columnIndex">The zero-based index of the column. </param>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">Occurs when you try to set a value on a deleted row. </exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="columnIndex" /> argument is out of range. </exception>
		/// <exception cref="T:System.InvalidCastException">Occurs when you set the value and the new value's <see cref="T:System.Type" /> does not match <see cref="P:System.Data.DataColumn.DataType" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000127 RID: 295
		public object this[int columnIndex]
		{
			get
			{
				DataColumn dataColumn = this._columns[columnIndex];
				int defaultRecord = this.GetDefaultRecord();
				return dataColumn[defaultRecord];
			}
			set
			{
				DataColumn dataColumn = this._columns[columnIndex];
				this[dataColumn] = value;
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x000182BC File Offset: 0x000164BC
		internal void CheckForLoops(DataRelation rel)
		{
			if (this._table._fInLoadDiffgram || (this._table.DataSet != null && this._table.DataSet._fInLoadDiffgram))
			{
				return;
			}
			int count = this._table.Rows.Count;
			int num = 0;
			for (DataRow dataRow = this.GetParentRow(rel); dataRow != null; dataRow = dataRow.GetParentRow(rel))
			{
				if (dataRow == this || num > count)
				{
					throw ExceptionBuilder.NestedCircular(this._table.TableName);
				}
				num++;
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001833C File Offset: 0x0001653C
		internal int GetNestedParentCount()
		{
			int num = 0;
			foreach (DataRelation dataRelation in this._table.NestedParentRelations)
			{
				if (dataRelation != null)
				{
					if (dataRelation.ParentTable == this._table)
					{
						this.CheckForLoops(dataRelation);
					}
					if (this.GetParentRow(dataRelation) != null)
					{
						num++;
					}
				}
			}
			return num;
		}

		/// <summary>Gets or sets the data stored in the column specified by name.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
		/// <param name="columnName">The name of the column. </param>
		/// <exception cref="T:System.ArgumentException">The column specified by <paramref name="columnName" /> cannot be found. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">Occurs when you try to set a value on a deleted row. </exception>
		/// <exception cref="T:System.InvalidCastException">Occurs when you set a value and its <see cref="T:System.Type" /> does not match <see cref="P:System.Data.DataColumn.DataType" />. </exception>
		/// <exception cref="T:System.Data.NoNullAllowedException">Occurs when you try to insert a null value into a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is set to false.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000128 RID: 296
		public object this[string columnName]
		{
			get
			{
				DataColumn dataColumn = this.GetDataColumn(columnName);
				int defaultRecord = this.GetDefaultRecord();
				return dataColumn[defaultRecord];
			}
			set
			{
				DataColumn dataColumn = this.GetDataColumn(columnName);
				this[dataColumn] = value;
			}
		}

		/// <summary>Gets or sets the data stored in the specified <see cref="T:System.Data.DataColumn" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" /> that contains the data. </param>
		/// <exception cref="T:System.ArgumentException">The column does not belong to this table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> is null. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to set a value on a deleted row. </exception>
		/// <exception cref="T:System.InvalidCastException">The data types of the value and the column do not match. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000129 RID: 297
		public object this[DataColumn column]
		{
			get
			{
				this.CheckColumn(column);
				int defaultRecord = this.GetDefaultRecord();
				return column[defaultRecord];
			}
			set
			{
				this.CheckColumn(column);
				if (this._inChangingEvent)
				{
					throw ExceptionBuilder.EditInRowChanging();
				}
				if (-1L != this.rowID && column.ReadOnly)
				{
					throw ExceptionBuilder.ReadOnly(column.ColumnName);
				}
				DataColumnChangeEventArgs dataColumnChangeEventArgs = null;
				if (this._table.NeedColumnChangeEvents)
				{
					dataColumnChangeEventArgs = new DataColumnChangeEventArgs(this, column, value);
					this._table.OnColumnChanging(dataColumnChangeEventArgs);
				}
				if (column.Table != this._table)
				{
					throw ExceptionBuilder.ColumnNotInTheTable(column.ColumnName, this._table.TableName);
				}
				if (-1L != this.rowID && column.ReadOnly)
				{
					throw ExceptionBuilder.ReadOnly(column.ColumnName);
				}
				object obj = ((dataColumnChangeEventArgs != null) ? dataColumnChangeEventArgs.ProposedValue : value);
				if (obj == null)
				{
					if (column.IsValueType)
					{
						throw ExceptionBuilder.CannotSetToNull(column);
					}
					obj = DBNull.Value;
				}
				bool flag = this.BeginEditInternal();
				try
				{
					int proposedRecordNo = this.GetProposedRecordNo();
					column[proposedRecordNo] = obj;
				}
				catch (Exception ex) when (ADP.IsCatchableOrSecurityExceptionType(ex))
				{
					if (flag)
					{
						this.CancelEdit();
					}
					throw;
				}
				this.LastChangedColumn = column;
				if (dataColumnChangeEventArgs != null)
				{
					this._table.OnColumnChanged(dataColumnChangeEventArgs);
				}
				if (flag)
				{
					this.EndEdit();
				}
			}
		}

		/// <summary>Gets the data stored in the column, specified by index and version of the data to retrieve.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
		/// <param name="columnIndex">The zero-based index of the column. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values that specifies the row version that you want. Possible values are Default, Original, Current, and Proposed. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="columnIndex" /> argument is out of range. </exception>
		/// <exception cref="T:System.InvalidCastException">The data types of the value and the column do not match. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to set a value on a deleted row. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700012A RID: 298
		public object this[int columnIndex, DataRowVersion version]
		{
			get
			{
				DataColumn dataColumn = this._columns[columnIndex];
				int recordFromVersion = this.GetRecordFromVersion(version);
				return dataColumn[recordFromVersion];
			}
		}

		/// <summary>Gets the specified version of data stored in the named column.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
		/// <param name="columnName">The name of the column. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values that specifies the row version that you want. Possible values are Default, Original, Current, and Proposed. </param>
		/// <exception cref="T:System.ArgumentException">The column specified by <paramref name="columnName" /> cannot be found. </exception>
		/// <exception cref="T:System.InvalidCastException">The data types of the value and the column do not match. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">The row was deleted. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700012B RID: 299
		public object this[string columnName, DataRowVersion version]
		{
			get
			{
				DataColumn dataColumn = this.GetDataColumn(columnName);
				int recordFromVersion = this.GetRecordFromVersion(version);
				return dataColumn[recordFromVersion];
			}
		}

		/// <summary>Gets the specified version of data stored in the specified <see cref="T:System.Data.DataColumn" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" /> that contains information about the column. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values that specifies the row version that you want. Possible values are Default, Original, Current, and Proposed. </param>
		/// <exception cref="T:System.ArgumentException">The column does not belong to the table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> argument contains null. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700012C RID: 300
		public object this[DataColumn column, DataRowVersion version]
		{
			get
			{
				this.CheckColumn(column);
				int recordFromVersion = this.GetRecordFromVersion(version);
				return column[recordFromVersion];
			}
		}

		/// <summary>Gets or sets all the values for this row through an array.</summary>
		/// <returns>An array of type <see cref="T:System.Object" />.</returns>
		/// <exception cref="T:System.ArgumentException">The array is larger than the number of columns in the table. </exception>
		/// <exception cref="T:System.InvalidCastException">A value in the array does not match its <see cref="P:System.Data.DataColumn.DataType" /> in its respective <see cref="T:System.Data.DataColumn" />. </exception>
		/// <exception cref="T:System.Data.ConstraintException">An edit broke a constraint. </exception>
		/// <exception cref="T:System.Data.ReadOnlyException">An edit tried to change the value of a read-only column. </exception>
		/// <exception cref="T:System.Data.NoNullAllowedException">An edit tried to put a null value in a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> of the <see cref="T:System.Data.DataColumn" /> object is false. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">The row has been deleted. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x000185A0 File Offset: 0x000167A0
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x000185EC File Offset: 0x000167EC
		public object[] ItemArray
		{
			get
			{
				int defaultRecord = this.GetDefaultRecord();
				object[] array = new object[this._columns.Count];
				for (int i = 0; i < array.Length; i++)
				{
					DataColumn dataColumn = this._columns[i];
					array[i] = dataColumn[defaultRecord];
				}
				return array;
			}
			set
			{
				if (value == null)
				{
					throw ExceptionBuilder.ArgumentNull("ItemArray");
				}
				if (this._columns.Count < value.Length)
				{
					throw ExceptionBuilder.ValueArrayLength();
				}
				DataColumnChangeEventArgs dataColumnChangeEventArgs = null;
				if (this._table.NeedColumnChangeEvents)
				{
					dataColumnChangeEventArgs = new DataColumnChangeEventArgs(this);
				}
				bool flag = this.BeginEditInternal();
				for (int i = 0; i < value.Length; i++)
				{
					if (value[i] != null)
					{
						DataColumn dataColumn = this._columns[i];
						if (-1L != this.rowID && dataColumn.ReadOnly)
						{
							throw ExceptionBuilder.ReadOnly(dataColumn.ColumnName);
						}
						if (dataColumnChangeEventArgs != null)
						{
							dataColumnChangeEventArgs.InitializeColumnChangeEvent(dataColumn, value[i]);
							this._table.OnColumnChanging(dataColumnChangeEventArgs);
						}
						if (dataColumn.Table != this._table)
						{
							throw ExceptionBuilder.ColumnNotInTheTable(dataColumn.ColumnName, this._table.TableName);
						}
						if (-1L != this.rowID && dataColumn.ReadOnly)
						{
							throw ExceptionBuilder.ReadOnly(dataColumn.ColumnName);
						}
						if (this._tempRecord == -1)
						{
							this.BeginEditInternal();
						}
						object obj = ((dataColumnChangeEventArgs != null) ? dataColumnChangeEventArgs.ProposedValue : value[i]);
						if (obj == null)
						{
							if (dataColumn.IsValueType)
							{
								throw ExceptionBuilder.CannotSetToNull(dataColumn);
							}
							obj = DBNull.Value;
						}
						try
						{
							int proposedRecordNo = this.GetProposedRecordNo();
							dataColumn[proposedRecordNo] = obj;
						}
						catch (Exception ex) when (ADP.IsCatchableOrSecurityExceptionType(ex))
						{
							if (flag)
							{
								this.CancelEdit();
							}
							throw;
						}
						this.LastChangedColumn = dataColumn;
						if (dataColumnChangeEventArgs != null)
						{
							this._table.OnColumnChanged(dataColumnChangeEventArgs);
						}
					}
				}
				this.EndEdit();
			}
		}

		/// <summary>Commits all the changes made to this row since the last time <see cref="M:System.Data.DataRow.AcceptChanges" /> was called.</summary>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060005FE RID: 1534 RVA: 0x0001877C File Offset: 0x0001697C
		public void AcceptChanges()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataRow.AcceptChanges|API> {0}", this._objectID);
			try
			{
				this.EndEdit();
				if (this.RowState != DataRowState.Detached && this.RowState != DataRowState.Deleted && this._columns.ColumnsImplementingIChangeTrackingCount > 0)
				{
					foreach (DataColumn dataColumn in this._columns.ColumnsImplementingIChangeTracking)
					{
						object obj = this[dataColumn];
						if (DBNull.Value != obj)
						{
							IChangeTracking changeTracking = (IChangeTracking)obj;
							if (changeTracking.IsChanged)
							{
								changeTracking.AcceptChanges();
							}
						}
					}
				}
				this._table.CommitRow(this);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Starts an edit operation on a <see cref="T:System.Data.DataRow" /> object.</summary>
		/// <exception cref="T:System.Data.InRowChangingEventException">The method was called inside the <see cref="E:System.Data.DataTable.RowChanging" /> event. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">The method was called upon a deleted row. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060005FF RID: 1535 RVA: 0x00018838 File Offset: 0x00016A38
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void BeginEdit()
		{
			this.BeginEditInternal();
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00018844 File Offset: 0x00016A44
		private bool BeginEditInternal()
		{
			if (this._inChangingEvent)
			{
				throw ExceptionBuilder.BeginEditInRowChanging();
			}
			if (this._tempRecord != -1)
			{
				if (this._tempRecord < this._table._recordManager.LastFreeRecord)
				{
					return false;
				}
				this._tempRecord = -1;
			}
			if (this._oldRecord != -1 && this._newRecord == -1)
			{
				throw ExceptionBuilder.DeletedRowInaccessible();
			}
			this.ResetLastChangedColumn();
			this._tempRecord = this._table.NewRecord(this._newRecord);
			return true;
		}

		/// <summary>Cancels the current edit on the row.</summary>
		/// <exception cref="T:System.Data.InRowChangingEventException">The method was called inside the <see cref="E:System.Data.DataTable.RowChanging" /> event. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000601 RID: 1537 RVA: 0x000188BF File Offset: 0x00016ABF
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void CancelEdit()
		{
			if (this._inChangingEvent)
			{
				throw ExceptionBuilder.CancelEditInRowChanging();
			}
			this._table.FreeRecord(ref this._tempRecord);
			this.ResetLastChangedColumn();
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000188E6 File Offset: 0x00016AE6
		private void CheckColumn(DataColumn column)
		{
			if (column == null)
			{
				throw ExceptionBuilder.ArgumentNull("column");
			}
			if (column.Table != this._table)
			{
				throw ExceptionBuilder.ColumnNotInTheTable(column.ColumnName, this._table.TableName);
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001891B File Offset: 0x00016B1B
		internal void CheckInTable()
		{
			if (this.rowID == -1L)
			{
				throw ExceptionBuilder.RowNotInTheTable();
			}
		}

		/// <summary>Deletes the <see cref="T:System.Data.DataRow" />.</summary>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">The <see cref="T:System.Data.DataRow" /> has already been deleted.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000604 RID: 1540 RVA: 0x0001892D File Offset: 0x00016B2D
		public void Delete()
		{
			if (this._inDeletingEvent)
			{
				throw ExceptionBuilder.DeleteInRowDeleting();
			}
			if (this._newRecord == -1)
			{
				return;
			}
			this._table.DeleteRow(this);
		}

		/// <summary>Ends the edit occurring on the row.</summary>
		/// <exception cref="T:System.Data.InRowChangingEventException">The method was called inside the <see cref="E:System.Data.DataTable.RowChanging" /> event. </exception>
		/// <exception cref="T:System.Data.ConstraintException">The edit broke a constraint. </exception>
		/// <exception cref="T:System.Data.ReadOnlyException">The row belongs to the table and the edit tried to change the value of a read-only column. </exception>
		/// <exception cref="T:System.Data.NoNullAllowedException">The edit tried to put a null value into a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is false. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000605 RID: 1541 RVA: 0x00018954 File Offset: 0x00016B54
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void EndEdit()
		{
			if (this._inChangingEvent)
			{
				throw ExceptionBuilder.EndEditInRowChanging();
			}
			if (this._newRecord == -1)
			{
				return;
			}
			if (this._tempRecord != -1)
			{
				try
				{
					this._table.SetNewRecord(this, this._tempRecord, DataRowAction.Change, false, true, true);
				}
				finally
				{
					this.ResetLastChangedColumn();
				}
			}
		}

		/// <summary>Sets the error description for a column specified by index.</summary>
		/// <param name="columnIndex">The zero-based index of the column. </param>
		/// <param name="error">The error description. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="columnIndex" /> argument is out of range </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000606 RID: 1542 RVA: 0x000189B4 File Offset: 0x00016BB4
		public void SetColumnError(int columnIndex, string error)
		{
			DataColumn dataColumn = this._columns[columnIndex];
			if (dataColumn == null)
			{
				throw ExceptionBuilder.ColumnOutOfRange(columnIndex);
			}
			this.SetColumnError(dataColumn, error);
		}

		/// <summary>Sets the error description for a column specified by name.</summary>
		/// <param name="columnName">The name of the column. </param>
		/// <param name="error">The error description. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000607 RID: 1543 RVA: 0x000189E0 File Offset: 0x00016BE0
		public void SetColumnError(string columnName, string error)
		{
			DataColumn dataColumn = this.GetDataColumn(columnName);
			this.SetColumnError(dataColumn, error);
		}

		/// <summary>Sets the error description for a column specified as a <see cref="T:System.Data.DataColumn" />.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to set the error description for. </param>
		/// <param name="error">The error description. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000608 RID: 1544 RVA: 0x00018A00 File Offset: 0x00016C00
		public void SetColumnError(DataColumn column, string error)
		{
			this.CheckColumn(column);
			long num = DataCommonEventSource.Log.EnterScope<int, int, string>("<ds.DataRow.SetColumnError|API> {0}, column={1}, error='{2}'", this._objectID, column.ObjectID, error);
			try
			{
				if (this._error == null)
				{
					this._error = new DataError();
				}
				if (this.GetColumnError(column) != error)
				{
					this._error.SetColumnError(column, error);
					this.RowErrorChanged();
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Gets the error description for the column specified by index.</summary>
		/// <returns>The text of the error description.</returns>
		/// <param name="columnIndex">The zero-based index of the column. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="columnIndex" /> argument is out of range. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000609 RID: 1545 RVA: 0x00018A84 File Offset: 0x00016C84
		public string GetColumnError(int columnIndex)
		{
			return this.GetColumnError(this._columns[columnIndex]);
		}

		/// <summary>Gets the error description for a column, specified by name.</summary>
		/// <returns>The text of the error description.</returns>
		/// <param name="columnName">The name of the column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600060A RID: 1546 RVA: 0x00018A98 File Offset: 0x00016C98
		public string GetColumnError(string columnName)
		{
			return this.GetColumnError(this.GetDataColumn(columnName));
		}

		/// <summary>Gets the error description of the specified <see cref="T:System.Data.DataColumn" />.</summary>
		/// <returns>The text of the error description.</returns>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600060B RID: 1547 RVA: 0x00018AA7 File Offset: 0x00016CA7
		public string GetColumnError(DataColumn column)
		{
			this.CheckColumn(column);
			if (this._error == null)
			{
				this._error = new DataError();
			}
			return this._error.GetColumnError(column);
		}

		/// <summary>Clears the errors for the row. This includes the <see cref="P:System.Data.DataRow.RowError" /> and errors set with <see cref="M:System.Data.DataRow.SetColumnError(System.Int32,System.String)" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600060C RID: 1548 RVA: 0x00018ACF File Offset: 0x00016CCF
		public void ClearErrors()
		{
			if (this._error != null)
			{
				this._error.Clear();
				this.RowErrorChanged();
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00018AEA File Offset: 0x00016CEA
		internal void ClearError(DataColumn column)
		{
			if (this._error != null)
			{
				this._error.Clear(column);
				this.RowErrorChanged();
			}
		}

		/// <summary>Gets a value that indicates whether there are errors in a row.</summary>
		/// <returns>true if the row contains an error; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00018B06 File Offset: 0x00016D06
		public bool HasErrors
		{
			get
			{
				return this._error != null && this._error.HasErrors;
			}
		}

		/// <summary>Gets an array of columns that have errors.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects that contain errors.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600060F RID: 1551 RVA: 0x00018B1D File Offset: 0x00016D1D
		public DataColumn[] GetColumnsInError()
		{
			if (this._error != null)
			{
				return this._error.GetColumnsInError();
			}
			return Array.Empty<DataColumn>();
		}

		/// <summary>Gets the child rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
		/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of the <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000610 RID: 1552 RVA: 0x00018B38 File Offset: 0x00016D38
		public DataRow[] GetChildRows(string relationName)
		{
			return this.GetChildRows(this._table.ChildRelations[relationName], DataRowVersion.Default);
		}

		/// <summary>Gets the child rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
		/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of the <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get. Possible values are Default, Original, Current, and Proposed. </param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> is null. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have the requested <see cref="T:System.Data.DataRowVersion" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000611 RID: 1553 RVA: 0x00018B56 File Offset: 0x00016D56
		public DataRow[] GetChildRows(string relationName, DataRowVersion version)
		{
			return this.GetChildRows(this._table.ChildRelations[relationName], version);
		}

		/// <summary>Gets the child rows of this <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The relation is null. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000612 RID: 1554 RVA: 0x00018B70 File Offset: 0x00016D70
		public DataRow[] GetChildRows(DataRelation relation)
		{
			return this.GetChildRows(relation, DataRowVersion.Default);
		}

		/// <summary>Gets the child rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get. Possible values are Default, Original, Current, and Proposed. </param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> is null. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have the requested <see cref="T:System.Data.DataRowVersion" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000613 RID: 1555 RVA: 0x00018B80 File Offset: 0x00016D80
		public DataRow[] GetChildRows(DataRelation relation, DataRowVersion version)
		{
			if (relation == null)
			{
				return this._table.NewRowArray(0);
			}
			if (relation.DataSet != this._table.DataSet)
			{
				throw ExceptionBuilder.RowNotInTheDataSet();
			}
			if (relation.ParentKey.Table != this._table)
			{
				throw ExceptionBuilder.RelationForeignTable(relation.ParentTable.TableName, this._table.TableName);
			}
			return DataRelation.GetChildRows(relation.ParentKey, relation.ChildKey, this, version);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00018BFC File Offset: 0x00016DFC
		internal DataColumn GetDataColumn(string columnName)
		{
			DataColumn dataColumn = this._columns[columnName];
			if (dataColumn != null)
			{
				return dataColumn;
			}
			throw ExceptionBuilder.ColumnNotInTheTable(columnName, this._table.TableName);
		}

		/// <summary>Gets the parent row of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>The parent <see cref="T:System.Data.DataRow" /> of the current row.</returns>
		/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />. </param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.Data.DataException">A child row has multiple parents.</exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000615 RID: 1557 RVA: 0x00018C2C File Offset: 0x00016E2C
		public DataRow GetParentRow(string relationName)
		{
			return this.GetParentRow(this._table.ParentRelations[relationName], DataRowVersion.Default);
		}

		/// <summary>Gets the parent row of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
		/// <returns>The parent <see cref="T:System.Data.DataRow" /> of the current row.</returns>
		/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> is null.</exception>
		/// <exception cref="T:System.Data.DataException">A child row has multiple parents.</exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have the requested <see cref="T:System.Data.DataRowVersion" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000616 RID: 1558 RVA: 0x00018C4A File Offset: 0x00016E4A
		public DataRow GetParentRow(string relationName, DataRowVersion version)
		{
			return this.GetParentRow(this._table.ParentRelations[relationName], version);
		}

		/// <summary>Gets the parent row of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>The parent <see cref="T:System.Data.DataRow" /> of the current row.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> does not belong to the <see cref="T:System.Data.DataTable" />.The row is null. </exception>
		/// <exception cref="T:System.Data.DataException">A child row has multiple parents.</exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">This row does not belong to the child table of the <see cref="T:System.Data.DataRelation" /> object. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to a table. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000617 RID: 1559 RVA: 0x00018C64 File Offset: 0x00016E64
		public DataRow GetParentRow(DataRelation relation)
		{
			return this.GetParentRow(relation, DataRowVersion.Default);
		}

		/// <summary>Gets the parent row of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
		/// <returns>The parent <see cref="T:System.Data.DataRow" /> of the current row.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get. </param>
		/// <exception cref="T:System.ArgumentNullException">The row is null.The <paramref name="relation" /> does not belong to this table's parent relations. </exception>
		/// <exception cref="T:System.Data.DataException">A child row has multiple parents.</exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation's child table is not the table the row belongs to. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to a table. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000618 RID: 1560 RVA: 0x00018C74 File Offset: 0x00016E74
		public DataRow GetParentRow(DataRelation relation, DataRowVersion version)
		{
			if (relation == null)
			{
				return null;
			}
			if (relation.DataSet != this._table.DataSet)
			{
				throw ExceptionBuilder.RelationForeignRow();
			}
			if (relation.ChildKey.Table != this._table)
			{
				throw ExceptionBuilder.GetParentRowTableMismatch(relation.ChildTable.TableName, this._table.TableName);
			}
			return DataRelation.GetParentRow(relation.ParentKey, relation.ChildKey, this, version);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00018CE4 File Offset: 0x00016EE4
		internal DataRow GetNestedParentRow(DataRowVersion version)
		{
			foreach (DataRelation dataRelation in this._table.NestedParentRelations)
			{
				if (dataRelation != null)
				{
					if (dataRelation.ParentTable == this._table)
					{
						this.CheckForLoops(dataRelation);
					}
					DataRow parentRow = this.GetParentRow(dataRelation, version);
					if (parentRow != null)
					{
						return parentRow;
					}
				}
			}
			return null;
		}

		/// <summary>Gets the parent rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
		/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />. </param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600061A RID: 1562 RVA: 0x00018D36 File Offset: 0x00016F36
		public DataRow[] GetParentRows(string relationName)
		{
			return this.GetParentRows(this._table.ParentRelations[relationName], DataRowVersion.Default);
		}

		/// <summary>Gets the parent rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
		/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get. Possible values are Default, Original, Current, and Proposed. </param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> is null. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have the requested <see cref="T:System.Data.DataRowVersion" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600061B RID: 1563 RVA: 0x00018D54 File Offset: 0x00016F54
		public DataRow[] GetParentRows(string relationName, DataRowVersion version)
		{
			return this.GetParentRows(this._table.ParentRelations[relationName], version);
		}

		/// <summary>Gets the parent rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.DataRelation" /> does not belong to this row's <see cref="T:System.Data.DataSet" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The row is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation's child table is not the table the row belongs to. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to a <see cref="T:System.Data.DataTable" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600061C RID: 1564 RVA: 0x00018D6E File Offset: 0x00016F6E
		public DataRow[] GetParentRows(DataRelation relation)
		{
			return this.GetParentRows(relation, DataRowVersion.Default);
		}

		/// <summary>Gets the parent rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.DataRelation" /> does not belong to this row's <see cref="T:System.Data.DataSet" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The row is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation's child table is not the table the row belongs to. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to a <see cref="T:System.Data.DataTable" />. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have the requested <see cref="T:System.Data.DataRowVersion" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600061D RID: 1565 RVA: 0x00018D7C File Offset: 0x00016F7C
		public DataRow[] GetParentRows(DataRelation relation, DataRowVersion version)
		{
			if (relation == null)
			{
				return this._table.NewRowArray(0);
			}
			if (relation.DataSet != this._table.DataSet)
			{
				throw ExceptionBuilder.RowNotInTheDataSet();
			}
			if (relation.ChildKey.Table != this._table)
			{
				throw ExceptionBuilder.GetParentRowTableMismatch(relation.ChildTable.TableName, this._table.TableName);
			}
			return DataRelation.GetParentRows(relation.ParentKey, relation.ChildKey, this, version);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00018DF7 File Offset: 0x00016FF7
		internal object[] GetColumnValues(DataColumn[] columns)
		{
			return this.GetColumnValues(columns, DataRowVersion.Default);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00018E08 File Offset: 0x00017008
		internal object[] GetColumnValues(DataColumn[] columns, DataRowVersion version)
		{
			DataKey dataKey = new DataKey(columns, false);
			return this.GetKeyValues(dataKey, version);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00018E28 File Offset: 0x00017028
		internal object[] GetKeyValues(DataKey key)
		{
			int defaultRecord = this.GetDefaultRecord();
			return key.GetKeyValues(defaultRecord);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00018E44 File Offset: 0x00017044
		internal object[] GetKeyValues(DataKey key, DataRowVersion version)
		{
			int recordFromVersion = this.GetRecordFromVersion(version);
			return key.GetKeyValues(recordFromVersion);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00018E61 File Offset: 0x00017061
		internal int GetCurrentRecordNo()
		{
			if (this._newRecord == -1)
			{
				throw ExceptionBuilder.NoCurrentData();
			}
			return this._newRecord;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00018E78 File Offset: 0x00017078
		internal int GetDefaultRecord()
		{
			if (this._tempRecord != -1)
			{
				return this._tempRecord;
			}
			if (this._newRecord != -1)
			{
				return this._newRecord;
			}
			throw (this._oldRecord == -1) ? ExceptionBuilder.RowRemovedFromTheTable() : ExceptionBuilder.DeletedRowInaccessible();
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00018EAF File Offset: 0x000170AF
		internal int GetOriginalRecordNo()
		{
			if (this._oldRecord == -1)
			{
				throw ExceptionBuilder.NoOriginalData();
			}
			return this._oldRecord;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00018EC6 File Offset: 0x000170C6
		private int GetProposedRecordNo()
		{
			if (this._tempRecord == -1)
			{
				throw ExceptionBuilder.NoProposedData();
			}
			return this._tempRecord;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00018EE0 File Offset: 0x000170E0
		internal int GetRecordFromVersion(DataRowVersion version)
		{
			if (version <= DataRowVersion.Current)
			{
				if (version == DataRowVersion.Original)
				{
					return this.GetOriginalRecordNo();
				}
				if (version == DataRowVersion.Current)
				{
					return this.GetCurrentRecordNo();
				}
			}
			else
			{
				if (version == DataRowVersion.Proposed)
				{
					return this.GetProposedRecordNo();
				}
				if (version == DataRowVersion.Default)
				{
					return this.GetDefaultRecord();
				}
			}
			throw ExceptionBuilder.InvalidRowVersion();
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00018F3C File Offset: 0x0001713C
		internal DataRowVersion GetDefaultRowVersion(DataViewRowState viewState)
		{
			if (this._oldRecord == this._newRecord)
			{
				int oldRecord = this._oldRecord;
				return DataRowVersion.Default;
			}
			if (this._oldRecord == -1)
			{
				return DataRowVersion.Default;
			}
			if (this._newRecord == -1)
			{
				return DataRowVersion.Original;
			}
			if ((DataViewRowState.ModifiedCurrent & viewState) != DataViewRowState.None)
			{
				return DataRowVersion.Default;
			}
			return DataRowVersion.Original;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00018F98 File Offset: 0x00017198
		internal DataViewRowState GetRecordState(int record)
		{
			if (record == -1)
			{
				return DataViewRowState.None;
			}
			if (record == this._oldRecord && record == this._newRecord)
			{
				return DataViewRowState.Unchanged;
			}
			if (record == this._oldRecord)
			{
				if (this._newRecord == -1)
				{
					return DataViewRowState.Deleted;
				}
				return DataViewRowState.ModifiedOriginal;
			}
			else
			{
				if (record != this._newRecord)
				{
					return DataViewRowState.None;
				}
				if (this._oldRecord == -1)
				{
					return DataViewRowState.Added;
				}
				return DataViewRowState.ModifiedCurrent;
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00018FEE File Offset: 0x000171EE
		internal bool HasKeyChanged(DataKey key)
		{
			return this.HasKeyChanged(key, DataRowVersion.Current, DataRowVersion.Proposed);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00019001 File Offset: 0x00017201
		internal bool HasKeyChanged(DataKey key, DataRowVersion version1, DataRowVersion version2)
		{
			return !this.HasVersion(version1) || !this.HasVersion(version2) || !key.RecordsEqual(this.GetRecordFromVersion(version1), this.GetRecordFromVersion(version2));
		}

		/// <summary>Gets a value that indicates whether a specified version exists.</summary>
		/// <returns>true if the version exists; otherwise, false.</returns>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values that specifies the row version. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600062B RID: 1579 RVA: 0x00019030 File Offset: 0x00017230
		public bool HasVersion(DataRowVersion version)
		{
			if (version <= DataRowVersion.Current)
			{
				if (version == DataRowVersion.Original)
				{
					return this._oldRecord != -1;
				}
				if (version == DataRowVersion.Current)
				{
					return this._newRecord != -1;
				}
			}
			else
			{
				if (version == DataRowVersion.Proposed)
				{
					return this._tempRecord != -1;
				}
				if (version == DataRowVersion.Default)
				{
					return this._tempRecord != -1 || this._newRecord != -1;
				}
			}
			throw ExceptionBuilder.InvalidRowVersion();
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x000190B0 File Offset: 0x000172B0
		internal bool HasChanges()
		{
			if (!this.HasVersion(DataRowVersion.Original) || !this.HasVersion(DataRowVersion.Current))
			{
				return true;
			}
			using (IEnumerator enumerator = this.Table.Columns.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((DataColumn)enumerator.Current).Compare(this._oldRecord, this._newRecord) != 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001913C File Offset: 0x0001733C
		internal bool HaveValuesChanged(DataColumn[] columns)
		{
			return this.HaveValuesChanged(columns, DataRowVersion.Current, DataRowVersion.Proposed);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00019150 File Offset: 0x00017350
		internal bool HaveValuesChanged(DataColumn[] columns, DataRowVersion version1, DataRowVersion version2)
		{
			for (int i = 0; i < columns.Length; i++)
			{
				this.CheckColumn(columns[i]);
			}
			DataKey dataKey = new DataKey(columns, false);
			return this.HasKeyChanged(dataKey, version1, version2);
		}

		/// <summary>Gets a value that indicates whether the column at the specified index contains a null value.</summary>
		/// <returns>true if the column contains a null value; otherwise, false.</returns>
		/// <param name="columnIndex">The zero-based index of the column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600062F RID: 1583 RVA: 0x00019188 File Offset: 0x00017388
		public bool IsNull(int columnIndex)
		{
			DataColumn dataColumn = this._columns[columnIndex];
			int defaultRecord = this.GetDefaultRecord();
			return dataColumn.IsNull(defaultRecord);
		}

		/// <summary>Gets a value that indicates whether the named column contains a null value.</summary>
		/// <returns>true if the column contains a null value; otherwise, false.</returns>
		/// <param name="columnName">The name of the column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000630 RID: 1584 RVA: 0x000191B0 File Offset: 0x000173B0
		public bool IsNull(string columnName)
		{
			DataColumn dataColumn = this.GetDataColumn(columnName);
			int defaultRecord = this.GetDefaultRecord();
			return dataColumn.IsNull(defaultRecord);
		}

		/// <summary>Gets a value that indicates whether the specified <see cref="T:System.Data.DataColumn" /> contains a null value.</summary>
		/// <returns>true if the column contains a null value; otherwise, false.</returns>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000631 RID: 1585 RVA: 0x000191D4 File Offset: 0x000173D4
		public bool IsNull(DataColumn column)
		{
			this.CheckColumn(column);
			int defaultRecord = this.GetDefaultRecord();
			return column.IsNull(defaultRecord);
		}

		/// <summary>Gets a value that indicates whether the specified <see cref="T:System.Data.DataColumn" /> and <see cref="T:System.Data.DataRowVersion" /> contains a null value.</summary>
		/// <returns>true if the column contains a null value; otherwise, false.</returns>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" />. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values that specifies the row version. Possible values are Default, Original, Current, and Proposed. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000632 RID: 1586 RVA: 0x000191F8 File Offset: 0x000173F8
		public bool IsNull(DataColumn column, DataRowVersion version)
		{
			this.CheckColumn(column);
			int recordFromVersion = this.GetRecordFromVersion(version);
			return column.IsNull(recordFromVersion);
		}

		/// <summary>Rejects all changes made to the row since <see cref="M:System.Data.DataRow.AcceptChanges" /> was last called.</summary>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000633 RID: 1587 RVA: 0x0001921C File Offset: 0x0001741C
		public void RejectChanges()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataRow.RejectChanges|API> {0}", this._objectID);
			try
			{
				if (this.RowState != DataRowState.Detached)
				{
					if (this._columns.ColumnsImplementingIChangeTrackingCount != this._columns.ColumnsImplementingIRevertibleChangeTrackingCount)
					{
						foreach (DataColumn dataColumn in this._columns.ColumnsImplementingIChangeTracking)
						{
							if (!dataColumn.ImplementsIRevertibleChangeTracking)
							{
								object obj;
								if (this.RowState != DataRowState.Deleted)
								{
									obj = this[dataColumn];
								}
								else
								{
									obj = this[dataColumn, DataRowVersion.Original];
								}
								if (DBNull.Value != obj && ((IChangeTracking)obj).IsChanged)
								{
									throw ExceptionBuilder.UDTImplementsIChangeTrackingButnotIRevertible(dataColumn.DataType.AssemblyQualifiedName);
								}
							}
						}
					}
					foreach (DataColumn dataColumn2 in this._columns.ColumnsImplementingIChangeTracking)
					{
						object obj2;
						if (this.RowState != DataRowState.Deleted)
						{
							obj2 = this[dataColumn2];
						}
						else
						{
							obj2 = this[dataColumn2, DataRowVersion.Original];
						}
						if (DBNull.Value != obj2 && ((IChangeTracking)obj2).IsChanged)
						{
							((IRevertibleChangeTracking)obj2).RejectChanges();
						}
					}
				}
				this._table.RollbackRow(this);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00019378 File Offset: 0x00017578
		internal void ResetLastChangedColumn()
		{
			this._lastChangedColumn = null;
			this._countColumnChange = 0;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00019388 File Offset: 0x00017588
		internal void SetKeyValues(DataKey key, object[] keyValues)
		{
			bool flag = true;
			bool flag2 = this._tempRecord == -1;
			for (int i = 0; i < keyValues.Length; i++)
			{
				if (!this[key.ColumnsReference[i]].Equals(keyValues[i]))
				{
					if (flag2 && flag)
					{
						flag = false;
						this.BeginEditInternal();
					}
					this[key.ColumnsReference[i]] = keyValues[i];
				}
			}
			if (!flag)
			{
				this.EndEdit();
			}
		}

		/// <summary>Sets the value of the specified <see cref="T:System.Data.DataColumn" /> to a null value.</summary>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" />. </param>
		// Token: 0x06000636 RID: 1590 RVA: 0x000193F1 File Offset: 0x000175F1
		protected void SetNull(DataColumn column)
		{
			this[column] = DBNull.Value;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00019400 File Offset: 0x00017600
		internal void SetNestedParentRow(DataRow parentRow, bool setNonNested)
		{
			if (parentRow == null)
			{
				this.SetParentRowToDBNull();
				return;
			}
			foreach (object obj in this._table.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if ((dataRelation.Nested || setNonNested) && dataRelation.ParentKey.Table == parentRow._table)
				{
					object[] keyValues = parentRow.GetKeyValues(dataRelation.ParentKey);
					this.SetKeyValues(dataRelation.ChildKey, keyValues);
					if (dataRelation.Nested)
					{
						if (parentRow._table == this._table)
						{
							this.CheckForLoops(dataRelation);
						}
						else
						{
							this.GetParentRow(dataRelation);
						}
					}
				}
			}
		}

		/// <summary>Sets the parent row of a <see cref="T:System.Data.DataRow" /> with specified new parent <see cref="T:System.Data.DataRow" />.</summary>
		/// <param name="parentRow">The new parent <see cref="T:System.Data.DataRow" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000638 RID: 1592 RVA: 0x000194C8 File Offset: 0x000176C8
		public void SetParentRow(DataRow parentRow)
		{
			this.SetNestedParentRow(parentRow, true);
		}

		/// <summary>Sets the parent row of a <see cref="T:System.Data.DataRow" /> with specified new parent <see cref="T:System.Data.DataRow" /> and <see cref="T:System.Data.DataRelation" />.</summary>
		/// <param name="parentRow">The new parent <see cref="T:System.Data.DataRow" />. </param>
		/// <param name="relation">The relation <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <exception cref="T:System.Data.RowNotInTableException">One of the rows does not belong to a table </exception>
		/// <exception cref="T:System.ArgumentNullException">One of the rows is null. </exception>
		/// <exception cref="T:System.ArgumentException">The relation does not belong to the <see cref="T:System.Data.DataRelationCollection" /> of the <see cref="T:System.Data.DataSet" /> object. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation's child <see cref="T:System.Data.DataTable" /> is not the table this row belongs to. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000639 RID: 1593 RVA: 0x000194D4 File Offset: 0x000176D4
		public void SetParentRow(DataRow parentRow, DataRelation relation)
		{
			if (relation == null)
			{
				this.SetParentRow(parentRow);
				return;
			}
			if (parentRow == null)
			{
				this.SetParentRowToDBNull(relation);
				return;
			}
			if (this._table.DataSet != parentRow._table.DataSet)
			{
				throw ExceptionBuilder.ParentRowNotInTheDataSet();
			}
			if (relation.ChildKey.Table != this._table)
			{
				throw ExceptionBuilder.SetParentRowTableMismatch(relation.ChildKey.Table.TableName, this._table.TableName);
			}
			if (relation.ParentKey.Table != parentRow._table)
			{
				throw ExceptionBuilder.SetParentRowTableMismatch(relation.ParentKey.Table.TableName, parentRow._table.TableName);
			}
			object[] keyValues = parentRow.GetKeyValues(relation.ParentKey);
			this.SetKeyValues(relation.ChildKey, keyValues);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x000195A4 File Offset: 0x000177A4
		internal void SetParentRowToDBNull()
		{
			foreach (object obj in this._table.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				this.SetParentRowToDBNull(dataRelation);
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00019604 File Offset: 0x00017804
		internal void SetParentRowToDBNull(DataRelation relation)
		{
			if (relation.ChildKey.Table != this._table)
			{
				throw ExceptionBuilder.SetParentRowTableMismatch(relation.ChildKey.Table.TableName, this._table.TableName);
			}
			object[] array = new object[] { DBNull.Value };
			this.SetKeyValues(relation.ChildKey, array);
		}

		/// <summary>Changes the <see cref="P:System.Data.DataRow.Rowstate" /> of a <see cref="T:System.Data.DataRow" /> to Added. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600063C RID: 1596 RVA: 0x00019667 File Offset: 0x00017867
		public void SetAdded()
		{
			if (this.RowState == DataRowState.Unchanged)
			{
				this._table.SetOldRecord(this, -1);
				return;
			}
			throw ExceptionBuilder.SetAddedAndModifiedCalledOnnonUnchanged();
		}

		/// <summary>Changes the <see cref="P:System.Data.DataRow.Rowstate" /> of a <see cref="T:System.Data.DataRow" /> to Modified. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600063D RID: 1597 RVA: 0x00019688 File Offset: 0x00017888
		public void SetModified()
		{
			if (this.RowState != DataRowState.Unchanged)
			{
				throw ExceptionBuilder.SetAddedAndModifiedCalledOnnonUnchanged();
			}
			this._tempRecord = this._table.NewRecord(this._newRecord);
			if (this._tempRecord != -1)
			{
				this._table.SetNewRecord(this, this._tempRecord, DataRowAction.Change, false, true, true);
				return;
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000196DC File Offset: 0x000178DC
		internal int CopyValuesIntoStore(ArrayList storeList, ArrayList nullbitList, int storeIndex)
		{
			int num = 0;
			if (this._oldRecord != -1)
			{
				for (int i = 0; i < this._columns.Count; i++)
				{
					this._columns[i].CopyValueIntoStore(this._oldRecord, storeList[i], (BitArray)nullbitList[i], storeIndex);
				}
				num++;
				storeIndex++;
			}
			DataRowState rowState = this.RowState;
			if (DataRowState.Added == rowState || DataRowState.Modified == rowState)
			{
				for (int j = 0; j < this._columns.Count; j++)
				{
					this._columns[j].CopyValueIntoStore(this._newRecord, storeList[j], (BitArray)nullbitList[j], storeIndex);
				}
				num++;
				storeIndex++;
			}
			if (-1 != this._tempRecord)
			{
				for (int k = 0; k < this._columns.Count; k++)
				{
					this._columns[k].CopyValueIntoStore(this._tempRecord, storeList[k], (BitArray)nullbitList[k], storeIndex);
				}
				num++;
				storeIndex++;
			}
			return num;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000197F4 File Offset: 0x000179F4
		[Conditional("DEBUG")]
		private void VerifyValueFromStorage(DataColumn column, DataRowVersion version, object valueFromStorage)
		{
			if (column.DataExpression != null && !this._inChangingEvent && this._tempRecord == -1 && this._newRecord != -1 && version == DataRowVersion.Original && this._oldRecord == this._newRecord)
			{
				version = DataRowVersion.Current;
			}
		}

		// Token: 0x04000579 RID: 1401
		private readonly DataTable _table;

		// Token: 0x0400057A RID: 1402
		private readonly DataColumnCollection _columns;

		// Token: 0x0400057B RID: 1403
		internal int _oldRecord = -1;

		// Token: 0x0400057C RID: 1404
		internal int _newRecord = -1;

		// Token: 0x0400057D RID: 1405
		internal int _tempRecord;

		// Token: 0x0400057E RID: 1406
		internal long _rowID = -1L;

		// Token: 0x0400057F RID: 1407
		internal DataRowAction _action;

		// Token: 0x04000580 RID: 1408
		internal bool _inChangingEvent;

		// Token: 0x04000581 RID: 1409
		internal bool _inDeletingEvent;

		// Token: 0x04000582 RID: 1410
		internal bool _inCascade;

		// Token: 0x04000583 RID: 1411
		private DataColumn _lastChangedColumn;

		// Token: 0x04000584 RID: 1412
		private int _countColumnChange;

		// Token: 0x04000585 RID: 1413
		private DataError _error;

		// Token: 0x04000586 RID: 1414
		private object _element;

		// Token: 0x04000587 RID: 1415
		private int _rbTreeNodeId;

		// Token: 0x04000588 RID: 1416
		private static int s_objectTypeCount;

		// Token: 0x04000589 RID: 1417
		internal readonly int _objectID = Interlocked.Increment(ref DataRow.s_objectTypeCount);
	}
}
