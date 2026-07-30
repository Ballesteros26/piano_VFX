using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	/// <summary>The <see cref="T:System.Data.DataTableReader" /> obtains the contents of one or more <see cref="T:System.Data.DataTable" /> objects in the form of one or more read-only, forward-only result sets. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000095 RID: 149
	public sealed class DataTableReader : DbDataReader
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTableReader" /> class by using data from the supplied <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> from which the new <see cref="T:System.Data.DataTableReader" /> obtains its result set. </param>
		// Token: 0x060008D4 RID: 2260 RVA: 0x00028AC4 File Offset: 0x00026CC4
		public DataTableReader(DataTable dataTable)
		{
			if (dataTable == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTable");
			}
			this._tables = new DataTable[] { dataTable };
			this.Init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTableReader" /> class using the supplied array of <see cref="T:System.Data.DataTable" /> objects.</summary>
		/// <param name="dataTables">The array of <see cref="T:System.Data.DataTable" /> objects that supplies the results for the new <see cref="T:System.Data.DataTableReader" /> object. </param>
		// Token: 0x060008D5 RID: 2261 RVA: 0x00028B18 File Offset: 0x00026D18
		public DataTableReader(DataTable[] dataTables)
		{
			if (dataTables == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTable");
			}
			if (dataTables.Length == 0)
			{
				throw ExceptionBuilder.DataTableReaderArgumentIsEmpty();
			}
			this._tables = new DataTable[dataTables.Length];
			for (int i = 0; i < dataTables.Length; i++)
			{
				if (dataTables[i] == null)
				{
					throw ExceptionBuilder.ArgumentNull("DataTable");
				}
				this._tables[i] = dataTables[i];
			}
			this.Init();
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00028B9C File Offset: 0x00026D9C
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x00028BA4 File Offset: 0x00026DA4
		private bool ReaderIsInvalid
		{
			get
			{
				return this._readerIsInvalid;
			}
			set
			{
				if (this._readerIsInvalid == value)
				{
					return;
				}
				this._readerIsInvalid = value;
				if (this._readerIsInvalid && this._listener != null)
				{
					this._listener.CleanUp();
				}
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00028BD2 File Offset: 0x00026DD2
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x00028BDA File Offset: 0x00026DDA
		private bool IsSchemaChanged
		{
			get
			{
				return this._schemaIsChanged;
			}
			set
			{
				if (!value || this._schemaIsChanged == value)
				{
					return;
				}
				this._schemaIsChanged = value;
				if (this._listener != null)
				{
					this._listener.CleanUp();
				}
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00028C03 File Offset: 0x00026E03
		internal DataTable CurrentDataTable
		{
			get
			{
				return this._currentDataTable;
			}
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00028C0C File Offset: 0x00026E0C
		private void Init()
		{
			this._tableCounter = 0;
			this._reachEORows = false;
			this._schemaIsChanged = false;
			this._currentDataTable = this._tables[this._tableCounter];
			this._hasRows = this._currentDataTable.Rows.Count > 0;
			this.ReaderIsInvalid = false;
			this._listener = new DataTableReaderListener(this);
		}

		/// <summary>Closes the current <see cref="T:System.Data.DataTableReader" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008DC RID: 2268 RVA: 0x00028C6D File Offset: 0x00026E6D
		public override void Close()
		{
			if (!this._isOpen)
			{
				return;
			}
			if (this._listener != null)
			{
				this._listener.CleanUp();
			}
			this._listener = null;
			this._schemaTable = null;
			this._isOpen = false;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.DataTableReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.DataTableReader" /> is closed. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060008DD RID: 2269 RVA: 0x00028CA0 File Offset: 0x00026EA0
		public override DataTable GetSchemaTable()
		{
			this.ValidateOpen("GetSchemaTable");
			this.ValidateReader();
			if (this._schemaTable == null)
			{
				this._schemaTable = DataTableReader.GetSchemaTableFromDataTable(this._currentDataTable);
			}
			return this._schemaTable;
		}

		/// <summary>Advances the <see cref="T:System.Data.DataTableReader" /> to the next result set, if any.</summary>
		/// <returns>true if there was another result set; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to navigate within a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008DE RID: 2270 RVA: 0x00028CD4 File Offset: 0x00026ED4
		public override bool NextResult()
		{
			this.ValidateOpen("NextResult");
			if (this._tableCounter == this._tables.Length - 1)
			{
				return false;
			}
			DataTable[] tables = this._tables;
			int num = this._tableCounter + 1;
			this._tableCounter = num;
			this._currentDataTable = tables[num];
			if (this._listener != null)
			{
				this._listener.UpdataTable(this._currentDataTable);
			}
			this._schemaTable = null;
			this._rowCounter = -1;
			this._currentRowRemoved = false;
			this._reachEORows = false;
			this._schemaIsChanged = false;
			this._started = false;
			this.ReaderIsInvalid = false;
			this._tableCleared = false;
			this._hasRows = this._currentDataTable.Rows.Count > 0;
			return true;
		}

		/// <summary>Advances the <see cref="T:System.Data.DataTableReader" /> to the next record.</summary>
		/// <returns>true if there was another row to read; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008DF RID: 2271 RVA: 0x00028D8C File Offset: 0x00026F8C
		public override bool Read()
		{
			if (!this._started)
			{
				this._started = true;
			}
			this.ValidateOpen("Read");
			this.ValidateReader();
			if (this._reachEORows)
			{
				return false;
			}
			if (this._rowCounter >= this._currentDataTable.Rows.Count - 1)
			{
				this._reachEORows = true;
				if (this._listener != null)
				{
					this._listener.CleanUp();
				}
				return false;
			}
			this._rowCounter++;
			this.ValidateRow(this._rowCounter);
			this._currentDataRow = this._currentDataTable.Rows[this._rowCounter];
			while (this._currentDataRow.RowState == DataRowState.Deleted)
			{
				this._rowCounter++;
				if (this._rowCounter == this._currentDataTable.Rows.Count)
				{
					this._reachEORows = true;
					if (this._listener != null)
					{
						this._listener.CleanUp();
					}
					return false;
				}
				this.ValidateRow(this._rowCounter);
				this._currentDataRow = this._currentDataTable.Rows[this._rowCounter];
			}
			if (this._currentRowRemoved)
			{
				this._currentRowRemoved = false;
			}
			return true;
		}

		/// <summary>The depth of nesting for the current row of the <see cref="T:System.Data.DataTableReader" />.</summary>
		/// <returns>The depth of nesting for the current row; always zero.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00028EB9 File Offset: 0x000270B9
		public override int Depth
		{
			get
			{
				this.ValidateOpen("Depth");
				this.ValidateReader();
				return 0;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataTableReader" /> is closed.</summary>
		/// <returns>Returns true if the <see cref="T:System.Data.DataTableReader" /> is closed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00028ECD File Offset: 0x000270CD
		public override bool IsClosed
		{
			get
			{
				return !this._isOpen;
			}
		}

		/// <summary>Gets the number of rows inserted, changed, or deleted by execution of the SQL statement.</summary>
		/// <returns>The <see cref="T:System.Data.DataTableReader" /> does not support this property and always returns 0.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00028ED8 File Offset: 0x000270D8
		public override int RecordsAffected
		{
			get
			{
				this.ValidateReader();
				return 0;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataTableReader" /> contains one or more rows.</summary>
		/// <returns>true if the <see cref="T:System.Data.DataTableReader" /> contains one or more rows; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to retrieve information about a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00028EE1 File Offset: 0x000270E1
		public override bool HasRows
		{
			get
			{
				this.ValidateOpen("HasRows");
				this.ValidateReader();
				return this._hasRows;
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column ordinal.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000195 RID: 405
		public override object this[int ordinal]
		{
			get
			{
				this.ValidateOpen("Item");
				this.ValidateReader();
				if (this._currentDataRow == null || this._currentDataRow.RowState == DataRowState.Deleted)
				{
					this.ReaderIsInvalid = true;
					throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
				}
				object obj;
				try
				{
					obj = this._currentDataRow[ordinal];
				}
				catch (IndexOutOfRangeException ex)
				{
					ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
					throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
				}
				return obj;
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column name.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="name">The name of the column. </param>
		/// <exception cref="T:System.ArgumentException">The name specified is not a valid column name. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000196 RID: 406
		public override object this[string name]
		{
			get
			{
				this.ValidateOpen("Item");
				this.ValidateReader();
				if (this._currentDataRow == null || this._currentDataRow.RowState == DataRowState.Deleted)
				{
					this.ReaderIsInvalid = true;
					throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
				}
				return this._currentDataRow[name];
			}
		}

		/// <summary>Returns the number of columns in the current row.</summary>
		/// <returns>When not positioned in a valid result set, 0; otherwise the number of columns in the current row. </returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to retrieve the field count in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00028FD4 File Offset: 0x000271D4
		public override int FieldCount
		{
			get
			{
				this.ValidateOpen("FieldCount");
				this.ValidateReader();
				return this._currentDataTable.Columns.Count;
			}
		}

		/// <summary>Gets the type of the specified column in provider-specific format.</summary>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008E7 RID: 2279 RVA: 0x00028FF7 File Offset: 0x000271F7
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			this.ValidateOpen("GetProviderSpecificFieldType");
			this.ValidateReader();
			return this.GetFieldType(ordinal);
		}

		/// <summary>Gets the value of the specified column in provider-specific format.</summary>
		/// <returns>The value of the specified column in provider-specific format.</returns>
		/// <param name="ordinal">The zero-based number of the column whose value is retrieved. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /></exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008E8 RID: 2280 RVA: 0x00029011 File Offset: 0x00027211
		public override object GetProviderSpecificValue(int ordinal)
		{
			this.ValidateOpen("GetProviderSpecificValue");
			this.ValidateReader();
			return this.GetValue(ordinal);
		}

		/// <summary>Fills the supplied array with provider-specific type information for all the columns in the <see cref="T:System.Data.DataTableReader" />.</summary>
		/// <returns>The number of column values copied into the array.</returns>
		/// <param name="values">An array of objects to be filled in with type information for the columns in the <see cref="T:System.Data.DataTableReader" />. </param>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008E9 RID: 2281 RVA: 0x0002902B File Offset: 0x0002722B
		public override int GetProviderSpecificValues(object[] values)
		{
			this.ValidateOpen("GetProviderSpecificValues");
			this.ValidateReader();
			return this.GetValues(values);
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a Boolean. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008EA RID: 2282 RVA: 0x00029048 File Offset: 0x00027248
		public override bool GetBoolean(int ordinal)
		{
			this.ValidateState("GetBoolean");
			this.ValidateReader();
			bool flag;
			try
			{
				flag = (bool)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return flag;
		}

		/// <summary>Gets the value of the specified column as a byte.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a byte. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008EB RID: 2283 RVA: 0x0002909C File Offset: 0x0002729C
		public override byte GetByte(int ordinal)
		{
			this.ValidateState("GetByte");
			this.ValidateReader();
			byte b;
			try
			{
				b = (byte)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return b;
		}

		/// <summary>Reads a stream of bytes starting at the specified column offset into the buffer as an array starting at the specified buffer offset.</summary>
		/// <returns>The actual number of bytes read.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the field from which to start the read operation. </param>
		/// <param name="buffer">The buffer into which to read the stream of bytes. </param>
		/// <param name="bufferIndex">The index within the buffer at which to start placing the data. </param>
		/// <param name="length">The maximum length to copy into the buffer. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a byte array. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008EC RID: 2284 RVA: 0x000290F0 File Offset: 0x000272F0
		public override long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			this.ValidateState("GetBytes");
			this.ValidateReader();
			byte[] array;
			try
			{
				array = (byte[])this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = (int)dataIndex;
			int num2 = Math.Min(array.Length - num, length);
			if (num < 0)
			{
				throw ADP.InvalidSourceBufferIndex(array.Length, (long)num, "dataIndex");
			}
			if (bufferIndex < 0 || (bufferIndex > 0 && bufferIndex >= buffer.Length))
			{
				throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
			}
			if (0 < num2)
			{
				Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num2);
			}
			else
			{
				if (length < 0)
				{
					throw ADP.InvalidDataLength((long)length);
				}
				num2 = 0;
			}
			return (long)num2;
		}

		/// <summary>Gets the value of the specified column as a character.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified field does not contain a character. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008ED RID: 2285 RVA: 0x000291B8 File Offset: 0x000273B8
		public override char GetChar(int ordinal)
		{
			this.ValidateState("GetChar");
			this.ValidateReader();
			char c;
			try
			{
				c = (char)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return c;
		}

		/// <summary>Returns the value of the specified column as a character array.</summary>
		/// <returns>The actual number of characters read.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the field from which to start the read operation. </param>
		/// <param name="buffer">The buffer into which to read the stream of chars. </param>
		/// <param name="bufferIndex">The index within the buffer at which to start placing the data. </param>
		/// <param name="length">The maximum length to copy into the buffer. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a character array. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008EE RID: 2286 RVA: 0x0002920C File Offset: 0x0002740C
		public override long GetChars(int ordinal, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			this.ValidateState("GetChars");
			this.ValidateReader();
			char[] array;
			try
			{
				array = (char[])this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = (int)dataIndex;
			int num2 = Math.Min(array.Length - num, length);
			if (num < 0)
			{
				throw ADP.InvalidSourceBufferIndex(array.Length, (long)num, "dataIndex");
			}
			if (bufferIndex < 0 || (bufferIndex > 0 && bufferIndex >= buffer.Length))
			{
				throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
			}
			if (0 < num2)
			{
				Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num2);
			}
			else
			{
				if (length < 0)
				{
					throw ADP.InvalidDataLength((long)length);
				}
				num2 = 0;
			}
			return (long)num2;
		}

		/// <summary>Gets a string representing the data type of the specified column.</summary>
		/// <returns>A string representing the column's data type.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008EF RID: 2287 RVA: 0x000292D4 File Offset: 0x000274D4
		public override string GetDataTypeName(int ordinal)
		{
			this.ValidateOpen("GetDataTypeName");
			this.ValidateReader();
			return this.GetFieldType(ordinal).Name;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a DateTime value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008F0 RID: 2288 RVA: 0x000292F4 File Offset: 0x000274F4
		public override DateTime GetDateTime(int ordinal)
		{
			this.ValidateState("GetDateTime");
			this.ValidateReader();
			DateTime dateTime;
			try
			{
				dateTime = (DateTime)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return dateTime;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a Decimal value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008F1 RID: 2289 RVA: 0x00029348 File Offset: 0x00027548
		public override decimal GetDecimal(int ordinal)
		{
			this.ValidateState("GetDecimal");
			this.ValidateReader();
			decimal num;
			try
			{
				num = (decimal)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return num;
		}

		/// <summary>Gets the value of the column as a double-precision floating point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a double-precision floating point number. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008F2 RID: 2290 RVA: 0x0002939C File Offset: 0x0002759C
		public override double GetDouble(int ordinal)
		{
			this.ValidateState("GetDouble");
			this.ValidateReader();
			double num;
			try
			{
				num = (double)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return num;
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that is the data type of the object.</summary>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008F3 RID: 2291 RVA: 0x000293F0 File Offset: 0x000275F0
		public override Type GetFieldType(int ordinal)
		{
			this.ValidateOpen("GetFieldType");
			this.ValidateReader();
			Type dataType;
			try
			{
				dataType = this._currentDataTable.Columns[ordinal].DataType;
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return dataType;
		}

		/// <summary>Gets the value of the specified column as a single-precision floating point number.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a single-precision floating point number. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008F4 RID: 2292 RVA: 0x0002944C File Offset: 0x0002764C
		public override float GetFloat(int ordinal)
		{
			this.ValidateState("GetFloat");
			this.ValidateReader();
			float num;
			try
			{
				num = (float)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return num;
		}

		/// <summary>Gets the value of the specified column as a globally-unique identifier (GUID).</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a GUID. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008F5 RID: 2293 RVA: 0x000294A0 File Offset: 0x000276A0
		public override Guid GetGuid(int ordinal)
		{
			this.ValidateState("GetGuid");
			this.ValidateReader();
			Guid guid;
			try
			{
				guid = (Guid)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return guid;
		}

		/// <summary>Gets the value of the specified column as a 16-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a 16-bit signed integer. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008F6 RID: 2294 RVA: 0x000294F4 File Offset: 0x000276F4
		public override short GetInt16(int ordinal)
		{
			this.ValidateState("GetInt16");
			this.ValidateReader();
			short num;
			try
			{
				num = (short)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return num;
		}

		/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a 32-bit signed integer value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008F7 RID: 2295 RVA: 0x00029548 File Offset: 0x00027748
		public override int GetInt32(int ordinal)
		{
			this.ValidateState("GetInt32");
			this.ValidateReader();
			int num;
			try
			{
				num = (int)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return num;
		}

		/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a 64-bit signed integer value. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008F8 RID: 2296 RVA: 0x0002959C File Offset: 0x0002779C
		public override long GetInt64(int ordinal)
		{
			this.ValidateState("GetInt64");
			this.ValidateReader();
			long num;
			try
			{
				num = (long)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return num;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.String" />.</summary>
		/// <returns>The name of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008F9 RID: 2297 RVA: 0x000295F0 File Offset: 0x000277F0
		public override string GetName(int ordinal)
		{
			this.ValidateOpen("GetName");
			this.ValidateReader();
			string columnName;
			try
			{
				columnName = this._currentDataTable.Columns[ordinal].ColumnName;
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return columnName;
		}

		/// <summary>Gets the column ordinal, given the name of the column.</summary>
		/// <returns>The zero-based column ordinal.</returns>
		/// <param name="name">The name of the column. </param>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.ArgumentException">The name specified is not a valid column name. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008FA RID: 2298 RVA: 0x0002964C File Offset: 0x0002784C
		public override int GetOrdinal(string name)
		{
			this.ValidateOpen("GetOrdinal");
			this.ValidateReader();
			DataColumn dataColumn = this._currentDataTable.Columns[name];
			if (dataColumn != null)
			{
				return dataColumn.Ordinal;
			}
			throw ExceptionBuilder.ColumnNotInTheTable(name, this._currentDataTable.TableName);
		}

		/// <summary>Gets the value of the specified column as a string.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a string. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008FB RID: 2299 RVA: 0x00029698 File Offset: 0x00027898
		public override string GetString(int ordinal)
		{
			this.ValidateState("GetString");
			this.ValidateReader();
			string text;
			try
			{
				text = (string)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return text;
		}

		/// <summary>Gets the value of the specified column in its native format.</summary>
		/// <returns>The value of the specified column. This method returns DBNull for null columns.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access columns in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008FC RID: 2300 RVA: 0x000296EC File Offset: 0x000278EC
		public override object GetValue(int ordinal)
		{
			this.ValidateState("GetValue");
			this.ValidateReader();
			object obj;
			try
			{
				obj = this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return obj;
		}

		/// <summary>Populates an array of objects with the column values of the current row.</summary>
		/// <returns>The number of column values copied into the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the column values from the <see cref="T:System.Data.DataTableReader" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008FD RID: 2301 RVA: 0x0002973C File Offset: 0x0002793C
		public override int GetValues(object[] values)
		{
			this.ValidateState("GetValues");
			this.ValidateReader();
			if (values == null)
			{
				throw ExceptionBuilder.ArgumentNull("values");
			}
			Array.Copy(this._currentDataRow.ItemArray, values, (this._currentDataRow.ItemArray.Length > values.Length) ? values.Length : this._currentDataRow.ItemArray.Length);
			if (this._currentDataRow.ItemArray.Length <= values.Length)
			{
				return this._currentDataRow.ItemArray.Length;
			}
			return values.Length;
		}

		/// <summary>Gets a value that indicates whether the column contains non-existent or missing values.</summary>
		/// <returns>true if the specified column value is equivalent to <see cref="T:System.DBNull" />; otherwise, false.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008FE RID: 2302 RVA: 0x000297C0 File Offset: 0x000279C0
		public override bool IsDBNull(int ordinal)
		{
			this.ValidateState("IsDBNull");
			this.ValidateReader();
			bool flag;
			try
			{
				flag = this._currentDataRow.IsNull(ordinal);
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return flag;
		}

		/// <summary>Returns an enumerator that can be used to iterate through the item collection. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that represents the item collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008FF RID: 2303 RVA: 0x00029810 File Offset: 0x00027A10
		public override IEnumerator GetEnumerator()
		{
			this.ValidateOpen("GetEnumerator");
			return new DbEnumerator(this);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00029824 File Offset: 0x00027A24
		internal static DataTable GetSchemaTableFromDataTable(DataTable table)
		{
			if (table == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTable");
			}
			DataTable dataTable = new DataTable("SchemaTable");
			dataTable.Locale = CultureInfo.InvariantCulture;
			DataColumn dataColumn = new DataColumn(SchemaTableColumn.ColumnName, typeof(string));
			DataColumn dataColumn2 = new DataColumn(SchemaTableColumn.ColumnOrdinal, typeof(int));
			DataColumn dataColumn3 = new DataColumn(SchemaTableColumn.ColumnSize, typeof(int));
			DataColumn dataColumn4 = new DataColumn(SchemaTableColumn.NumericPrecision, typeof(short));
			DataColumn dataColumn5 = new DataColumn(SchemaTableColumn.NumericScale, typeof(short));
			DataColumn dataColumn6 = new DataColumn(SchemaTableColumn.DataType, typeof(Type));
			DataColumn dataColumn7 = new DataColumn(SchemaTableColumn.ProviderType, typeof(int));
			DataColumn dataColumn8 = new DataColumn(SchemaTableColumn.IsLong, typeof(bool));
			DataColumn dataColumn9 = new DataColumn(SchemaTableColumn.AllowDBNull, typeof(bool));
			DataColumn dataColumn10 = new DataColumn(SchemaTableOptionalColumn.IsReadOnly, typeof(bool));
			DataColumn dataColumn11 = new DataColumn(SchemaTableOptionalColumn.IsRowVersion, typeof(bool));
			DataColumn dataColumn12 = new DataColumn(SchemaTableColumn.IsUnique, typeof(bool));
			DataColumn dataColumn13 = new DataColumn(SchemaTableColumn.IsKey, typeof(bool));
			DataColumn dataColumn14 = new DataColumn(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool));
			DataColumn dataColumn15 = new DataColumn(SchemaTableColumn.BaseSchemaName, typeof(string));
			DataColumn dataColumn16 = new DataColumn(SchemaTableOptionalColumn.BaseCatalogName, typeof(string));
			DataColumn dataColumn17 = new DataColumn(SchemaTableColumn.BaseTableName, typeof(string));
			DataColumn dataColumn18 = new DataColumn(SchemaTableColumn.BaseColumnName, typeof(string));
			DataColumn dataColumn19 = new DataColumn(SchemaTableOptionalColumn.AutoIncrementSeed, typeof(long));
			DataColumn dataColumn20 = new DataColumn(SchemaTableOptionalColumn.AutoIncrementStep, typeof(long));
			DataColumn dataColumn21 = new DataColumn(SchemaTableOptionalColumn.DefaultValue, typeof(object));
			DataColumn dataColumn22 = new DataColumn(SchemaTableOptionalColumn.Expression, typeof(string));
			DataColumn dataColumn23 = new DataColumn(SchemaTableOptionalColumn.ColumnMapping, typeof(MappingType));
			DataColumn dataColumn24 = new DataColumn(SchemaTableOptionalColumn.BaseTableNamespace, typeof(string));
			DataColumn dataColumn25 = new DataColumn(SchemaTableOptionalColumn.BaseColumnNamespace, typeof(string));
			dataColumn3.DefaultValue = -1;
			if (table.DataSet != null)
			{
				dataColumn16.DefaultValue = table.DataSet.DataSetName;
			}
			dataColumn17.DefaultValue = table.TableName;
			dataColumn24.DefaultValue = table.Namespace;
			dataColumn11.DefaultValue = false;
			dataColumn8.DefaultValue = false;
			dataColumn10.DefaultValue = false;
			dataColumn13.DefaultValue = false;
			dataColumn14.DefaultValue = false;
			dataColumn19.DefaultValue = 0;
			dataColumn20.DefaultValue = 1;
			dataTable.Columns.Add(dataColumn);
			dataTable.Columns.Add(dataColumn2);
			dataTable.Columns.Add(dataColumn3);
			dataTable.Columns.Add(dataColumn4);
			dataTable.Columns.Add(dataColumn5);
			dataTable.Columns.Add(dataColumn6);
			dataTable.Columns.Add(dataColumn7);
			dataTable.Columns.Add(dataColumn8);
			dataTable.Columns.Add(dataColumn9);
			dataTable.Columns.Add(dataColumn10);
			dataTable.Columns.Add(dataColumn11);
			dataTable.Columns.Add(dataColumn12);
			dataTable.Columns.Add(dataColumn13);
			dataTable.Columns.Add(dataColumn14);
			dataTable.Columns.Add(dataColumn16);
			dataTable.Columns.Add(dataColumn15);
			dataTable.Columns.Add(dataColumn17);
			dataTable.Columns.Add(dataColumn18);
			dataTable.Columns.Add(dataColumn19);
			dataTable.Columns.Add(dataColumn20);
			dataTable.Columns.Add(dataColumn21);
			dataTable.Columns.Add(dataColumn22);
			dataTable.Columns.Add(dataColumn23);
			dataTable.Columns.Add(dataColumn24);
			dataTable.Columns.Add(dataColumn25);
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn26 = (DataColumn)obj;
				DataRow dataRow = dataTable.NewRow();
				dataRow[dataColumn] = dataColumn26.ColumnName;
				dataRow[dataColumn2] = dataColumn26.Ordinal;
				dataRow[dataColumn6] = dataColumn26.DataType;
				if (dataColumn26.DataType == typeof(string))
				{
					dataRow[dataColumn3] = dataColumn26.MaxLength;
				}
				dataRow[dataColumn9] = dataColumn26.AllowDBNull;
				dataRow[dataColumn10] = dataColumn26.ReadOnly;
				dataRow[dataColumn12] = dataColumn26.Unique;
				if (dataColumn26.AutoIncrement)
				{
					dataRow[dataColumn14] = true;
					dataRow[dataColumn19] = dataColumn26.AutoIncrementSeed;
					dataRow[dataColumn20] = dataColumn26.AutoIncrementStep;
				}
				if (dataColumn26.DefaultValue != DBNull.Value)
				{
					dataRow[dataColumn21] = dataColumn26.DefaultValue;
				}
				if (dataColumn26.Expression.Length != 0)
				{
					bool flag = false;
					DataColumn[] dependency = dataColumn26.DataExpression.GetDependency();
					for (int i = 0; i < dependency.Length; i++)
					{
						if (dependency[i].Table != table)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						dataRow[dataColumn22] = dataColumn26.Expression;
					}
				}
				dataRow[dataColumn23] = dataColumn26.ColumnMapping;
				dataRow[dataColumn18] = dataColumn26.ColumnName;
				dataRow[dataColumn25] = dataColumn26.Namespace;
				dataTable.Rows.Add(dataRow);
			}
			foreach (DataColumn dataColumn27 in table.PrimaryKey)
			{
				dataTable.Rows[dataColumn27.Ordinal][dataColumn13] = true;
			}
			dataTable.AcceptChanges();
			return dataTable;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00029E9C File Offset: 0x0002809C
		private void ValidateOpen(string caller)
		{
			if (!this._isOpen)
			{
				throw ADP.DataReaderClosed(caller);
			}
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00029EAD File Offset: 0x000280AD
		private void ValidateReader()
		{
			if (this.ReaderIsInvalid)
			{
				throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
			}
			if (this.IsSchemaChanged)
			{
				throw ExceptionBuilder.DataTableReaderSchemaIsInvalid(this._currentDataTable.TableName);
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00029EE4 File Offset: 0x000280E4
		private void ValidateState(string caller)
		{
			this.ValidateOpen(caller);
			if (this._tableCleared)
			{
				throw ExceptionBuilder.EmptyDataTableReader(this._currentDataTable.TableName);
			}
			if (this._currentDataRow == null || this._currentDataTable == null)
			{
				this.ReaderIsInvalid = true;
				throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
			}
			if (this._currentDataRow.RowState == DataRowState.Deleted || this._currentDataRow.RowState == DataRowState.Detached || this._currentRowRemoved)
			{
				throw ExceptionBuilder.InvalidCurrentRowInDataTableReader();
			}
			if (0 > this._rowCounter || this._currentDataTable.Rows.Count <= this._rowCounter)
			{
				this.ReaderIsInvalid = true;
				throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
			}
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00029F9C File Offset: 0x0002819C
		private void ValidateRow(int rowPosition)
		{
			if (this.ReaderIsInvalid)
			{
				throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
			}
			if (0 > rowPosition || this._currentDataTable.Rows.Count <= rowPosition)
			{
				this.ReaderIsInvalid = true;
				throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00029FF1 File Offset: 0x000281F1
		internal void SchemaChanged()
		{
			this.IsSchemaChanged = true;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00029FFA File Offset: 0x000281FA
		internal void DataTableCleared()
		{
			if (!this._started)
			{
				return;
			}
			this._rowCounter = -1;
			if (!this._reachEORows)
			{
				this._currentRowRemoved = true;
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0002A01C File Offset: 0x0002821C
		internal void DataChanged(DataRowChangeEventArgs args)
		{
			if (!this._started || (this._rowCounter == -1 && !this._tableCleared))
			{
				return;
			}
			DataRowAction action = args.Action;
			if (action <= DataRowAction.Rollback)
			{
				if (action != DataRowAction.Delete && action != DataRowAction.Rollback)
				{
					return;
				}
			}
			else if (action != DataRowAction.Commit)
			{
				if (action != DataRowAction.Add)
				{
					return;
				}
				this.ValidateRow(this._rowCounter + 1);
				if (this._currentDataRow == this._currentDataTable.Rows[this._rowCounter + 1])
				{
					this._rowCounter++;
					return;
				}
				return;
			}
			if (args.Row.RowState == DataRowState.Detached)
			{
				if (args.Row != this._currentDataRow)
				{
					if (this._rowCounter != 0)
					{
						this.ValidateRow(this._rowCounter - 1);
						if (this._currentDataRow == this._currentDataTable.Rows[this._rowCounter - 1])
						{
							this._rowCounter--;
							return;
						}
					}
				}
				else
				{
					this._currentRowRemoved = true;
					if (this._rowCounter > 0)
					{
						this._rowCounter--;
						this._currentDataRow = this._currentDataTable.Rows[this._rowCounter];
						return;
					}
					this._rowCounter = -1;
					this._currentDataRow = null;
				}
			}
		}

		// Token: 0x04000633 RID: 1587
		private readonly DataTable[] _tables;

		// Token: 0x04000634 RID: 1588
		private bool _isOpen = true;

		// Token: 0x04000635 RID: 1589
		private DataTable _schemaTable;

		// Token: 0x04000636 RID: 1590
		private int _tableCounter = -1;

		// Token: 0x04000637 RID: 1591
		private int _rowCounter = -1;

		// Token: 0x04000638 RID: 1592
		private DataTable _currentDataTable;

		// Token: 0x04000639 RID: 1593
		private DataRow _currentDataRow;

		// Token: 0x0400063A RID: 1594
		private bool _hasRows = true;

		// Token: 0x0400063B RID: 1595
		private bool _reachEORows;

		// Token: 0x0400063C RID: 1596
		private bool _currentRowRemoved;

		// Token: 0x0400063D RID: 1597
		private bool _schemaIsChanged;

		// Token: 0x0400063E RID: 1598
		private bool _started;

		// Token: 0x0400063F RID: 1599
		private bool _readerIsInvalid;

		// Token: 0x04000640 RID: 1600
		private DataTableReaderListener _listener;

		// Token: 0x04000641 RID: 1601
		private bool _tableCleared;
	}
}
