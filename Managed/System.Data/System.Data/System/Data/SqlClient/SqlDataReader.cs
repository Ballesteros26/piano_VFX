using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.SqlServer.Server;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Provides a way of reading a forward-only stream of rows from a SQL Server database. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001A2 RID: 418
	public class SqlDataReader : DbDataReader, IDataReader, IDisposable, IDataRecord, IDbColumnSchemaGenerator
	{
		// Token: 0x0600139B RID: 5019 RVA: 0x000600C8 File Offset: 0x0005E2C8
		internal SqlDataReader(SqlCommand command, CommandBehavior behavior)
		{
			this._sharedState = new SqlDataReader.SharedState();
			this._recordsAffected = -1;
			this.ObjectID = Interlocked.Increment(ref SqlDataReader.s_objectTypeCount);
			base..ctor();
			this._command = command;
			this._commandBehavior = behavior;
			if (this._command != null)
			{
				this._defaultTimeoutMilliseconds = (long)command.CommandTimeout * 1000L;
				this._connection = command.Connection;
				if (this._connection != null)
				{
					this._statistics = this._connection.Statistics;
					this._typeSystem = this._connection.TypeSystem;
				}
			}
			this._sharedState._dataReady = false;
			this._metaDataConsumed = false;
			this._hasRows = false;
			this._browseModeInfoConsumed = false;
			this._currentStream = null;
			this._currentTextReader = null;
			this._cancelAsyncOnCloseTokenSource = new CancellationTokenSource();
			this._cancelAsyncOnCloseToken = this._cancelAsyncOnCloseTokenSource.Token;
			this._columnDataCharsIndex = -1;
		}

		// Token: 0x170003A8 RID: 936
		// (set) Token: 0x0600139C RID: 5020 RVA: 0x000601AF File Offset: 0x0005E3AF
		internal bool BrowseModeInfoConsumed
		{
			set
			{
				this._browseModeInfoConsumed = value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x000601B8 File Offset: 0x0005E3B8
		internal SqlCommand Command
		{
			get
			{
				return this._command;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlConnection" /> associated with the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlConnection" /> associated with the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</returns>
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x000601C0 File Offset: 0x0005E3C0
		protected SqlConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		/// <summary>Gets a value that indicates the depth of nesting for the current row.</summary>
		/// <returns>The depth of nesting for the current row.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x000601C8 File Offset: 0x0005E3C8
		public override int Depth
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("Depth");
				}
				return 0;
			}
		}

		/// <summary>Gets the number of columns in the current row.</summary>
		/// <returns>When not positioned in a valid recordset, 0; otherwise the number of columns in the current row. The default is -1.</returns>
		/// <exception cref="T:System.NotSupportedException">There is no current connection to an instance of SQL Server. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x000601DE File Offset: 0x0005E3DE
		public override int FieldCount
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("FieldCount");
				}
				if (this._currentTask != null)
				{
					throw ADP.AsyncOperationPending();
				}
				if (this.MetaData == null)
				{
					return 0;
				}
				return this._metaData.Length;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.SqlClient.SqlDataReader" /> contains one or more rows.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlClient.SqlDataReader" /> contains one or more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x00060216 File Offset: 0x0005E416
		public override bool HasRows
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("HasRows");
				}
				if (this._currentTask != null)
				{
					throw ADP.AsyncOperationPending();
				}
				return this._hasRows;
			}
		}

		/// <summary>Retrieves a Boolean value that indicates whether the specified <see cref="T:System.Data.SqlClient.SqlDataReader" /> instance has been closed. </summary>
		/// <returns>true if the specified <see cref="T:System.Data.SqlClient.SqlDataReader" /> instance is closed; otherwise false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x0006023F File Offset: 0x0005E43F
		public override bool IsClosed
		{
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x00060247 File Offset: 0x0005E447
		// (set) Token: 0x060013A4 RID: 5028 RVA: 0x0006024F File Offset: 0x0005E44F
		internal bool IsInitialized
		{
			get
			{
				return this._isInitialized;
			}
			set
			{
				this._isInitialized = value;
			}
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00060258 File Offset: 0x0005E458
		internal long ColumnDataBytesRemaining()
		{
			if (-1L == this._sharedState._columnDataBytesRemaining)
			{
				this._sharedState._columnDataBytesRemaining = (long)this._parser.PlpBytesLeft(this._stateObj);
			}
			return this._sharedState._columnDataBytesRemaining;
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x00060290 File Offset: 0x0005E490
		internal _SqlMetaDataSet MetaData
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("MetaData");
				}
				if (this._metaData == null && !this._metaDataConsumed)
				{
					if (this._currentTask != null)
					{
						throw SQL.PendingBeginXXXExists();
					}
					if (!this.TryConsumeMetaData())
					{
						throw SQL.SynchronousCallMayNotPend();
					}
				}
				return this._metaData;
			}
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000602E4 File Offset: 0x0005E4E4
		internal virtual SmiExtendedMetaData[] GetInternalSmiMetaData()
		{
			SmiExtendedMetaData[] array = null;
			_SqlMetaDataSet metaData = this.MetaData;
			if (metaData != null && 0 < metaData.Length)
			{
				array = new SmiExtendedMetaData[metaData.visibleColumns];
				for (int i = 0; i < metaData.Length; i++)
				{
					_SqlMetaData sqlMetaData = metaData[i];
					if (!sqlMetaData.isHidden)
					{
						SqlCollation collation = sqlMetaData.collation;
						string text = null;
						string text2 = null;
						string text3 = null;
						if (SqlDbType.Xml == sqlMetaData.type)
						{
							text = sqlMetaData.xmlSchemaCollectionDatabase;
							text2 = sqlMetaData.xmlSchemaCollectionOwningSchema;
							text3 = sqlMetaData.xmlSchemaCollectionName;
						}
						else if (SqlDbType.Udt == sqlMetaData.type)
						{
							throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
						}
						int num = sqlMetaData.length;
						if (num > 8000)
						{
							num = -1;
						}
						else if (SqlDbType.NChar == sqlMetaData.type || SqlDbType.NVarChar == sqlMetaData.type)
						{
							num /= 2;
						}
						array[i] = new SmiQueryMetaData(sqlMetaData.type, (long)num, sqlMetaData.precision, sqlMetaData.scale, (long)((collation != null) ? collation.LCID : this._defaultLCID), (collation != null) ? collation.SqlCompareOptions : SqlCompareOptions.None, false, null, null, sqlMetaData.column, text, text2, text3, sqlMetaData.isNullable, sqlMetaData.serverName, sqlMetaData.catalogName, sqlMetaData.schemaName, sqlMetaData.tableName, sqlMetaData.baseColumn, sqlMetaData.isKey, sqlMetaData.isIdentity, sqlMetaData.updatability == 0, sqlMetaData.isExpression, sqlMetaData.isDifferentName, sqlMetaData.isHidden);
					}
				}
			}
			return array;
		}

		/// <summary>Gets the number of rows changed, inserted, or deleted by execution of the Transact-SQL statement.</summary>
		/// <returns>The number of rows changed, inserted, or deleted; 0 if no rows were affected or the statement failed; and -1 for SELECT statements.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x00060476 File Offset: 0x0005E676
		public override int RecordsAffected
		{
			get
			{
				if (this._command != null)
				{
					return this._command.InternalRecordsAffected;
				}
				return this._recordsAffected;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x00060492 File Offset: 0x0005E692
		internal string ResetOptionsString
		{
			set
			{
				this._resetOptionsString = value;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x0006049B File Offset: 0x0005E69B
		private SqlStatistics Statistics
		{
			get
			{
				return this._statistics;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x000604A3 File Offset: 0x0005E6A3
		// (set) Token: 0x060013AC RID: 5036 RVA: 0x000604AB File Offset: 0x0005E6AB
		internal MultiPartTableName[] TableNames
		{
			get
			{
				return this._tableNames;
			}
			set
			{
				this._tableNames = value;
			}
		}

		/// <summary>Gets the number of fields in the <see cref="T:System.Data.SqlClient.SqlDataReader" /> that are not hidden. </summary>
		/// <returns>The number of fields that are not hidden.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x000604B4 File Offset: 0x0005E6B4
		public override int VisibleFieldCount
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("VisibleFieldCount");
				}
				_SqlMetaDataSet metaData = this.MetaData;
				if (metaData == null)
				{
					return 0;
				}
				return metaData.visibleColumns;
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column ordinal.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003B6 RID: 950
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column name.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="name">The column name. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">No column with the specified name was found. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003B7 RID: 951
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x000604FE File Offset: 0x0005E6FE
		internal void Bind(TdsParserStateObject stateObj)
		{
			stateObj.Owner = this;
			this._stateObj = stateObj;
			this._parser = stateObj.Parser;
			this._defaultLCID = this._parser.DefaultLCID;
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0006052C File Offset: 0x0005E72C
		internal DataTable BuildSchemaTable()
		{
			_SqlMetaDataSet metaData = this.MetaData;
			DataTable dataTable = new DataTable("SchemaTable");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.MinimumCapacity = metaData.Length;
			DataColumn dataColumn = new DataColumn(SchemaTableColumn.ColumnName, typeof(string));
			DataColumn dataColumn2 = new DataColumn(SchemaTableColumn.ColumnOrdinal, typeof(int));
			DataColumn dataColumn3 = new DataColumn(SchemaTableColumn.ColumnSize, typeof(int));
			DataColumn dataColumn4 = new DataColumn(SchemaTableColumn.NumericPrecision, typeof(short));
			DataColumn dataColumn5 = new DataColumn(SchemaTableColumn.NumericScale, typeof(short));
			DataColumn dataColumn6 = new DataColumn(SchemaTableColumn.DataType, typeof(Type));
			DataColumn dataColumn7 = new DataColumn(SchemaTableOptionalColumn.ProviderSpecificDataType, typeof(Type));
			DataColumn dataColumn8 = new DataColumn(SchemaTableColumn.NonVersionedProviderType, typeof(int));
			DataColumn dataColumn9 = new DataColumn(SchemaTableColumn.ProviderType, typeof(int));
			DataColumn dataColumn10 = new DataColumn(SchemaTableColumn.IsLong, typeof(bool));
			DataColumn dataColumn11 = new DataColumn(SchemaTableColumn.AllowDBNull, typeof(bool));
			DataColumn dataColumn12 = new DataColumn(SchemaTableOptionalColumn.IsReadOnly, typeof(bool));
			DataColumn dataColumn13 = new DataColumn(SchemaTableOptionalColumn.IsRowVersion, typeof(bool));
			DataColumn dataColumn14 = new DataColumn(SchemaTableColumn.IsUnique, typeof(bool));
			DataColumn dataColumn15 = new DataColumn(SchemaTableColumn.IsKey, typeof(bool));
			DataColumn dataColumn16 = new DataColumn(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool));
			DataColumn dataColumn17 = new DataColumn(SchemaTableOptionalColumn.IsHidden, typeof(bool));
			DataColumn dataColumn18 = new DataColumn(SchemaTableOptionalColumn.BaseCatalogName, typeof(string));
			DataColumn dataColumn19 = new DataColumn(SchemaTableColumn.BaseSchemaName, typeof(string));
			DataColumn dataColumn20 = new DataColumn(SchemaTableColumn.BaseTableName, typeof(string));
			DataColumn dataColumn21 = new DataColumn(SchemaTableColumn.BaseColumnName, typeof(string));
			DataColumn dataColumn22 = new DataColumn(SchemaTableOptionalColumn.BaseServerName, typeof(string));
			DataColumn dataColumn23 = new DataColumn(SchemaTableColumn.IsAliased, typeof(bool));
			DataColumn dataColumn24 = new DataColumn(SchemaTableColumn.IsExpression, typeof(bool));
			DataColumn dataColumn25 = new DataColumn("IsIdentity", typeof(bool));
			DataColumn dataColumn26 = new DataColumn("DataTypeName", typeof(string));
			DataColumn dataColumn27 = new DataColumn("UdtAssemblyQualifiedName", typeof(string));
			DataColumn dataColumn28 = new DataColumn("XmlSchemaCollectionDatabase", typeof(string));
			DataColumn dataColumn29 = new DataColumn("XmlSchemaCollectionOwningSchema", typeof(string));
			DataColumn dataColumn30 = new DataColumn("XmlSchemaCollectionName", typeof(string));
			DataColumn dataColumn31 = new DataColumn("IsColumnSet", typeof(bool));
			dataColumn2.DefaultValue = 0;
			dataColumn10.DefaultValue = false;
			DataColumnCollection columns = dataTable.Columns;
			columns.Add(dataColumn);
			columns.Add(dataColumn2);
			columns.Add(dataColumn3);
			columns.Add(dataColumn4);
			columns.Add(dataColumn5);
			columns.Add(dataColumn14);
			columns.Add(dataColumn15);
			columns.Add(dataColumn22);
			columns.Add(dataColumn18);
			columns.Add(dataColumn21);
			columns.Add(dataColumn19);
			columns.Add(dataColumn20);
			columns.Add(dataColumn6);
			columns.Add(dataColumn11);
			columns.Add(dataColumn9);
			columns.Add(dataColumn23);
			columns.Add(dataColumn24);
			columns.Add(dataColumn25);
			columns.Add(dataColumn16);
			columns.Add(dataColumn13);
			columns.Add(dataColumn17);
			columns.Add(dataColumn10);
			columns.Add(dataColumn12);
			columns.Add(dataColumn7);
			columns.Add(dataColumn26);
			columns.Add(dataColumn28);
			columns.Add(dataColumn29);
			columns.Add(dataColumn30);
			columns.Add(dataColumn27);
			columns.Add(dataColumn8);
			columns.Add(dataColumn31);
			for (int i = 0; i < metaData.Length; i++)
			{
				_SqlMetaData sqlMetaData = metaData[i];
				DataRow dataRow = dataTable.NewRow();
				dataRow[dataColumn] = sqlMetaData.column;
				dataRow[dataColumn2] = sqlMetaData.ordinal;
				dataRow[dataColumn3] = ((sqlMetaData.metaType.IsSizeInCharacters && sqlMetaData.length != int.MaxValue) ? (sqlMetaData.length / 2) : sqlMetaData.length);
				dataRow[dataColumn6] = this.GetFieldTypeInternal(sqlMetaData);
				dataRow[dataColumn7] = this.GetProviderSpecificFieldTypeInternal(sqlMetaData);
				dataRow[dataColumn8] = (int)sqlMetaData.type;
				dataRow[dataColumn26] = this.GetDataTypeNameInternal(sqlMetaData);
				if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.IsNewKatmaiDateTimeType)
				{
					dataRow[dataColumn9] = SqlDbType.NVarChar;
					switch (sqlMetaData.type)
					{
					case SqlDbType.Date:
						dataRow[dataColumn3] = 10;
						break;
					case SqlDbType.Time:
						dataRow[dataColumn3] = TdsEnums.WHIDBEY_TIME_LENGTH[(int)((byte.MaxValue != sqlMetaData.scale) ? sqlMetaData.scale : sqlMetaData.metaType.Scale)];
						break;
					case SqlDbType.DateTime2:
						dataRow[dataColumn3] = TdsEnums.WHIDBEY_DATETIME2_LENGTH[(int)((byte.MaxValue != sqlMetaData.scale) ? sqlMetaData.scale : sqlMetaData.metaType.Scale)];
						break;
					case SqlDbType.DateTimeOffset:
						dataRow[dataColumn3] = TdsEnums.WHIDBEY_DATETIMEOFFSET_LENGTH[(int)((byte.MaxValue != sqlMetaData.scale) ? sqlMetaData.scale : sqlMetaData.metaType.Scale)];
						break;
					}
				}
				else if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.IsLargeUdt)
				{
					if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
					{
						dataRow[dataColumn9] = SqlDbType.VarBinary;
					}
					else
					{
						dataRow[dataColumn9] = SqlDbType.Image;
					}
				}
				else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
				{
					dataRow[dataColumn9] = (int)sqlMetaData.type;
					if (sqlMetaData.type == SqlDbType.Udt)
					{
						dataRow[dataColumn27] = sqlMetaData.udtAssemblyQualifiedName;
					}
					else if (sqlMetaData.type == SqlDbType.Xml)
					{
						dataRow[dataColumn28] = sqlMetaData.xmlSchemaCollectionDatabase;
						dataRow[dataColumn29] = sqlMetaData.xmlSchemaCollectionOwningSchema;
						dataRow[dataColumn30] = sqlMetaData.xmlSchemaCollectionName;
					}
				}
				else
				{
					dataRow[dataColumn9] = this.GetVersionedMetaType(sqlMetaData.metaType).SqlDbType;
				}
				if (255 != sqlMetaData.precision)
				{
					dataRow[dataColumn4] = sqlMetaData.precision;
				}
				else
				{
					dataRow[dataColumn4] = sqlMetaData.metaType.Precision;
				}
				if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.IsNewKatmaiDateTimeType)
				{
					dataRow[dataColumn5] = MetaType.MetaNVarChar.Scale;
				}
				else if (255 != sqlMetaData.scale)
				{
					dataRow[dataColumn5] = sqlMetaData.scale;
				}
				else
				{
					dataRow[dataColumn5] = sqlMetaData.metaType.Scale;
				}
				dataRow[dataColumn11] = sqlMetaData.isNullable;
				if (this._browseModeInfoConsumed)
				{
					dataRow[dataColumn23] = sqlMetaData.isDifferentName;
					dataRow[dataColumn15] = sqlMetaData.isKey;
					dataRow[dataColumn17] = sqlMetaData.isHidden;
					dataRow[dataColumn24] = sqlMetaData.isExpression;
				}
				dataRow[dataColumn25] = sqlMetaData.isIdentity;
				dataRow[dataColumn16] = sqlMetaData.isIdentity;
				dataRow[dataColumn10] = sqlMetaData.metaType.IsLong;
				if (SqlDbType.Timestamp == sqlMetaData.type)
				{
					dataRow[dataColumn14] = true;
					dataRow[dataColumn13] = true;
				}
				else
				{
					dataRow[dataColumn14] = false;
					dataRow[dataColumn13] = false;
				}
				dataRow[dataColumn12] = sqlMetaData.updatability == 0;
				dataRow[dataColumn31] = sqlMetaData.isColumnSet;
				if (!string.IsNullOrEmpty(sqlMetaData.serverName))
				{
					dataRow[dataColumn22] = sqlMetaData.serverName;
				}
				if (!string.IsNullOrEmpty(sqlMetaData.catalogName))
				{
					dataRow[dataColumn18] = sqlMetaData.catalogName;
				}
				if (!string.IsNullOrEmpty(sqlMetaData.schemaName))
				{
					dataRow[dataColumn19] = sqlMetaData.schemaName;
				}
				if (!string.IsNullOrEmpty(sqlMetaData.tableName))
				{
					dataRow[dataColumn20] = sqlMetaData.tableName;
				}
				if (!string.IsNullOrEmpty(sqlMetaData.baseColumn))
				{
					dataRow[dataColumn21] = sqlMetaData.baseColumn;
				}
				else if (!string.IsNullOrEmpty(sqlMetaData.column))
				{
					dataRow[dataColumn21] = sqlMetaData.column;
				}
				dataTable.Rows.Add(dataRow);
				dataRow.AcceptChanges();
			}
			foreach (object obj in columns)
			{
				((DataColumn)obj).ReadOnly = true;
			}
			return dataTable;
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00060F0C File Offset: 0x0005F10C
		internal void Cancel(SqlCommand command)
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.Cancel(command);
			}
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00060F2C File Offset: 0x0005F12C
		private bool TryCleanPartialRead()
		{
			if (this._stateObj._partialHeaderBytesRead > 0 && !this._stateObj.TryProcessHeader())
			{
				return false;
			}
			if (-1 != this._lastColumnWithDataChunkRead)
			{
				this.CloseActiveSequentialStreamAndTextReader();
			}
			if (this._sharedState._nextColumnHeaderToRead == 0)
			{
				if (!this._stateObj.Parser.TrySkipRow(this._metaData, this._stateObj))
				{
					return false;
				}
			}
			else
			{
				if (!this.TryResetBlobState())
				{
					return false;
				}
				if (!this._stateObj.Parser.TrySkipRow(this._metaData, this._sharedState._nextColumnHeaderToRead, this._stateObj))
				{
					return false;
				}
			}
			this._sharedState._dataReady = false;
			return true;
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00060FD4 File Offset: 0x0005F1D4
		private void CleanPartialReadReliable()
		{
			this.TryCleanPartialRead();
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00060FDD File Offset: 0x0005F1DD
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
			base.Dispose(disposing);
		}

		/// <summary>Closes the <see cref="T:System.Data.SqlClient.SqlDataReader" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013B6 RID: 5046 RVA: 0x00060FF0 File Offset: 0x0005F1F0
		public override void Close()
		{
			SqlStatistics sqlStatistics = null;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				TdsParserStateObject stateObj = this._stateObj;
				this._cancelAsyncOnCloseTokenSource.Cancel();
				Task currentTask = this._currentTask;
				if (currentTask != null && !currentTask.IsCompleted)
				{
					try
					{
						((IAsyncResult)currentTask).AsyncWaitHandle.WaitOne();
						TaskCompletionSource<object> networkPacketTaskSource = stateObj._networkPacketTaskSource;
						if (networkPacketTaskSource != null)
						{
							((IAsyncResult)networkPacketTaskSource.Task).AsyncWaitHandle.WaitOne();
						}
					}
					catch (Exception)
					{
						this._connection.InnerConnection.DoomThisConnection();
						this._isClosed = true;
						if (stateObj != null)
						{
							TdsParserStateObject tdsParserStateObject = stateObj;
							lock (tdsParserStateObject)
							{
								this._stateObj = null;
								this._command = null;
								this._connection = null;
							}
						}
						throw;
					}
				}
				this.CloseActiveSequentialStreamAndTextReader();
				if (stateObj != null)
				{
					TdsParserStateObject tdsParserStateObject = stateObj;
					lock (tdsParserStateObject)
					{
						if (this._stateObj != null)
						{
							if (this._snapshot != null)
							{
								this.PrepareForAsyncContinuation();
							}
							this.SetTimeout(this._defaultTimeoutMilliseconds);
							stateObj._syncOverAsync = true;
							if (!this.TryCloseInternal(true))
							{
								throw SQL.SynchronousCallMayNotPend();
							}
						}
					}
				}
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00061178 File Offset: 0x0005F378
		private bool TryCloseInternal(bool closeReader)
		{
			TdsParser parser = this._parser;
			TdsParserStateObject stateObj = this._stateObj;
			bool flag = this.IsCommandBehavior(CommandBehavior.CloseConnection);
			bool flag2 = false;
			bool flag3 = false;
			bool flag5;
			try
			{
				if (!this._isClosed && parser != null && stateObj != null && stateObj._pendingData && parser.State == TdsParserState.OpenLoggedIn)
				{
					if (this._altRowStatus == SqlDataReader.ALTROWSTATUS.AltRow)
					{
						this._sharedState._dataReady = true;
					}
					this._stateObj._internalTimeout = false;
					if (this._sharedState._dataReady)
					{
						flag3 = true;
						if (!this.TryCleanPartialRead())
						{
							return false;
						}
						flag3 = false;
					}
					bool flag4;
					if (!parser.TryRun(RunBehavior.Clean, this._command, this, null, stateObj, out flag4))
					{
						return false;
					}
				}
				this.RestoreServerSettings(parser, stateObj);
				flag5 = true;
			}
			finally
			{
				if (flag2)
				{
					this._isClosed = true;
					this._command = null;
					this._connection = null;
					this._statistics = null;
					this._stateObj = null;
					this._parser = null;
				}
				else if (closeReader)
				{
					bool isClosed = this._isClosed;
					this._isClosed = true;
					this._parser = null;
					this._stateObj = null;
					this._data = null;
					if (this._snapshot != null)
					{
						this.CleanupAfterAsyncInvocationInternal(stateObj, true);
					}
					if (this.Connection != null)
					{
						this.Connection.RemoveWeakReference(this);
					}
					if (!isClosed && stateObj != null)
					{
						if (!flag3)
						{
							stateObj.CloseSession();
						}
						else if (parser != null)
						{
							parser.State = TdsParserState.Broken;
							parser.PutSession(stateObj);
							parser.Connection.BreakConnection();
						}
					}
					this.TrySetMetaData(null, false);
					this._fieldNameLookup = null;
					if (flag && this.Connection != null)
					{
						this.Connection.Close();
					}
					if (this._command != null)
					{
						this._recordsAffected = this._command.InternalRecordsAffected;
					}
					this._command = null;
					this._connection = null;
					this._statistics = null;
				}
			}
			return flag5;
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00061350 File Offset: 0x0005F550
		internal virtual void CloseReaderFromConnection()
		{
			TdsParser parser = this._parser;
			if (parser != null && parser.State == TdsParserState.OpenLoggedIn)
			{
				this.Close();
				return;
			}
			TdsParserStateObject stateObj = this._stateObj;
			this._isClosed = true;
			this._cancelAsyncOnCloseTokenSource.Cancel();
			if (stateObj != null)
			{
				TaskCompletionSource<object> networkPacketTaskSource = stateObj._networkPacketTaskSource;
				if (networkPacketTaskSource != null)
				{
					networkPacketTaskSource.TrySetException(ADP.ClosedConnectionError());
				}
				if (this._snapshot != null)
				{
					this.CleanupAfterAsyncInvocationInternal(stateObj, false);
				}
				stateObj._syncOverAsync = true;
				stateObj.RemoveOwner();
			}
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x000613C8 File Offset: 0x0005F5C8
		private bool TryConsumeMetaData()
		{
			while (this._parser != null && this._stateObj != null && this._stateObj._pendingData && !this._metaDataConsumed)
			{
				if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
				{
					if (this._parser.Connection != null)
					{
						this._parser.Connection.DoomThisConnection();
					}
					throw SQL.ConnectionDoomed();
				}
				bool flag;
				if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out flag))
				{
					return false;
				}
			}
			if (this._metaData != null)
			{
				if (this._snapshot != null && this._snapshot._metadata == this._metaData)
				{
					this._metaData = (_SqlMetaDataSet)this._metaData.Clone();
				}
				this._metaData.visibleColumns = 0;
				int[] array = new int[this._metaData.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._metaData.visibleColumns;
					if (!this._metaData[i].isHidden)
					{
						this._metaData.visibleColumns++;
					}
				}
				this._metaData.indexMap = array;
			}
			return true;
		}

		/// <summary>Gets a string representing the data type of the specified column.</summary>
		/// <returns>The string representing the data type of the specified column.</returns>
		/// <param name="i">The zero-based ordinal position of the column to find.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013BA RID: 5050 RVA: 0x00061508 File Offset: 0x0005F708
		public override string GetDataTypeName(int i)
		{
			SqlStatistics sqlStatistics = null;
			string dataTypeNameInternal;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.CheckMetaDataIsReady(i, false);
				dataTypeNameInternal = this.GetDataTypeNameInternal(this._metaData[i]);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return dataTypeNameInternal;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00061558 File Offset: 0x0005F758
		private string GetDataTypeNameInternal(_SqlMetaData metaData)
		{
			string text;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
			{
				text = MetaType.MetaNVarChar.TypeName;
			}
			else if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
			{
				if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
				{
					text = MetaType.MetaMaxVarBinary.TypeName;
				}
				else
				{
					text = MetaType.MetaImage.TypeName;
				}
			}
			else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				if (metaData.type == SqlDbType.Udt)
				{
					text = string.Concat(new string[] { metaData.udtDatabaseName, ".", metaData.udtSchemaName, ".", metaData.udtTypeName });
				}
				else
				{
					text = metaData.metaType.TypeName;
				}
			}
			else
			{
				text = this.GetVersionedMetaType(metaData.metaType).TypeName;
			}
			return text;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00061639 File Offset: 0x0005F839
		internal virtual SqlBuffer.StorageType GetVariantInternalStorageType(int i)
		{
			return this._data[i].VariantInternalStorageType;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</returns>
		// Token: 0x060013BD RID: 5053 RVA: 0x00061648 File Offset: 0x0005F848
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this, this.IsCommandBehavior(CommandBehavior.CloseConnection));
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that is the data type of the object.</summary>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object. If the type does not exist on the client, in the case of a User-Defined Type (UDT) returned from the database, GetFieldType returns null.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013BE RID: 5054 RVA: 0x00061658 File Offset: 0x0005F858
		public override Type GetFieldType(int i)
		{
			SqlStatistics sqlStatistics = null;
			Type fieldTypeInternal;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.CheckMetaDataIsReady(i, false);
				fieldTypeInternal = this.GetFieldTypeInternal(this._metaData[i]);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return fieldTypeInternal;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x000616A8 File Offset: 0x0005F8A8
		private Type GetFieldTypeInternal(_SqlMetaData metaData)
		{
			Type type;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
			{
				type = MetaType.MetaNVarChar.ClassType;
			}
			else if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
			{
				if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
				{
					type = MetaType.MetaMaxVarBinary.ClassType;
				}
				else
				{
					type = MetaType.MetaImage.ClassType;
				}
			}
			else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				if (metaData.type == SqlDbType.Udt)
				{
					type = MetaType.MetaMaxVarBinary.ClassType;
				}
				else
				{
					type = metaData.metaType.ClassType;
				}
			}
			else
			{
				type = this.GetVersionedMetaType(metaData.metaType).ClassType;
			}
			return type;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00061760 File Offset: 0x0005F960
		internal virtual int GetLocaleId(int i)
		{
			_SqlMetaData sqlMetaData = this.MetaData[i];
			int num;
			if (sqlMetaData.collation != null)
			{
				num = sqlMetaData.collation.LCID;
			}
			else
			{
				num = 0;
			}
			return num;
		}

		/// <summary>Gets the name of the specified column.</summary>
		/// <returns>The name of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060013C1 RID: 5057 RVA: 0x00061793 File Offset: 0x0005F993
		public override string GetName(int i)
		{
			this.CheckMetaDataIsReady(i, false);
			return this._metaData[i].column;
		}

		/// <summary>Gets an Object that is a representation of the underlying provider-specific field type.</summary>
		/// <returns>Gets an <see cref="T:System.Object" /> that is a representation of the underlying provider-specific field type.</returns>
		/// <param name="i">An <see cref="T:System.Int32" /> representing the column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013C2 RID: 5058 RVA: 0x000617B0 File Offset: 0x0005F9B0
		public override Type GetProviderSpecificFieldType(int i)
		{
			SqlStatistics sqlStatistics = null;
			Type providerSpecificFieldTypeInternal;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.CheckMetaDataIsReady(i, false);
				providerSpecificFieldTypeInternal = this.GetProviderSpecificFieldTypeInternal(this._metaData[i]);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return providerSpecificFieldTypeInternal;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00061800 File Offset: 0x0005FA00
		private Type GetProviderSpecificFieldTypeInternal(_SqlMetaData metaData)
		{
			Type type;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
			{
				type = MetaType.MetaNVarChar.SqlType;
			}
			else if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
			{
				if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
				{
					type = MetaType.MetaMaxVarBinary.SqlType;
				}
				else
				{
					type = MetaType.MetaImage.SqlType;
				}
			}
			else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				if (metaData.type == SqlDbType.Udt)
				{
					type = MetaType.MetaMaxVarBinary.SqlType;
				}
				else
				{
					type = metaData.metaType.SqlType;
				}
			}
			else
			{
				type = this.GetVersionedMetaType(metaData.metaType).SqlType;
			}
			return type;
		}

		/// <summary>Gets the column ordinal, given the name of the column.</summary>
		/// <returns>The zero-based column ordinal.</returns>
		/// <param name="name">The name of the column. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The name specified is not a valid column name. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060013C4 RID: 5060 RVA: 0x000618B8 File Offset: 0x0005FAB8
		public override int GetOrdinal(string name)
		{
			SqlStatistics sqlStatistics = null;
			int ordinal;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				if (this._fieldNameLookup == null)
				{
					this.CheckMetaDataIsReady();
					this._fieldNameLookup = new FieldNameLookup(this, this._defaultLCID);
				}
				ordinal = this._fieldNameLookup.GetOrdinal(name);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return ordinal;
		}

		/// <summary>Gets an Object that is a representation of the underlying provider specific value.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a representation of the underlying provider specific value.</returns>
		/// <param name="i">An <see cref="T:System.Int32" /> representing the column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013C5 RID: 5061 RVA: 0x0006191C File Offset: 0x0005FB1C
		public override object GetProviderSpecificValue(int i)
		{
			return this.GetSqlValue(i);
		}

		/// <summary>Gets an array of objects that are a representation of the underlying provider specific values.</summary>
		/// <returns>The array of objects that are a representation of the underlying provider specific values.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the column values.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013C6 RID: 5062 RVA: 0x00061925 File Offset: 0x0005FB25
		public override int GetProviderSpecificValues(object[] values)
		{
			return this.GetSqlValues(values);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013C7 RID: 5063 RVA: 0x00061930 File Offset: 0x0005FB30
		public override DataTable GetSchemaTable()
		{
			SqlStatistics sqlStatistics = null;
			DataTable dataTable;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				if ((this._metaData == null || this._metaData.schemaTable == null) && this.MetaData != null)
				{
					this._metaData.schemaTable = this.BuildSchemaTable();
				}
				_SqlMetaDataSet metaData = this._metaData;
				dataTable = ((metaData != null) ? metaData.schemaTable : null);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return dataTable;
		}

		/// <summary>Gets the value of the specified column as a Boolean.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013C8 RID: 5064 RVA: 0x000619A8 File Offset: 0x0005FBA8
		public override bool GetBoolean(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Boolean;
		}

		/// <summary>Retrieves data of type XML as an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>The returned object.</returns>
		/// <param name="i">The value of the specified column.</param>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).Trying to read a previously read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">The returned type was not xml.</exception>
		// Token: 0x060013C9 RID: 5065 RVA: 0x000619C0 File Offset: 0x0005FBC0
		public virtual XmlReader GetXmlReader(int i)
		{
			this.CheckDataIsReady(i, false, false, "GetXmlReader");
			if (this._metaData[i].metaType.SqlDbType != SqlDbType.Xml)
			{
				throw SQL.XmlReaderNotSupportOnColumnType(this._metaData[i].column);
			}
			if (this.IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				this._currentStream = new SqlSequentialStream(this, i);
				this._lastColumnWithDataChunkRead = i;
				return SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(this._currentStream, true, false);
			}
			this.ReadColumn(i, true, false);
			if (this._data[i].IsNull)
			{
				return SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(new MemoryStream(Array.Empty<byte>(), false), true, false);
			}
			return this._data[i].SqlXml.CreateReader();
		}

		/// <summary>Retrieves binary, image, varbinary, UDT, and variant data types as a <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>A stream object.</returns>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).Tried to read a previously-read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">The returned type was not one of the types below:binaryimagevarbinaryudt</exception>
		// Token: 0x060013CA RID: 5066 RVA: 0x00061A78 File Offset: 0x0005FC78
		public override Stream GetStream(int i)
		{
			this.CheckDataIsReady(i, false, false, "GetStream");
			MetaType metaType = this._metaData[i].metaType;
			if ((!metaType.IsBinType || metaType.SqlDbType == SqlDbType.Timestamp) && metaType.SqlDbType != SqlDbType.Variant)
			{
				throw SQL.StreamNotSupportOnColumnType(this._metaData[i].column);
			}
			if (metaType.SqlDbType != SqlDbType.Variant && this.IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				this._currentStream = new SqlSequentialStream(this, i);
				this._lastColumnWithDataChunkRead = i;
				return this._currentStream;
			}
			this.ReadColumn(i, true, false);
			byte[] array;
			if (this._data[i].IsNull)
			{
				array = Array.Empty<byte>();
			}
			else
			{
				array = this._data[i].SqlBinary.Value;
			}
			return new MemoryStream(array, false);
		}

		/// <summary>Gets the value of the specified column as a byte.</summary>
		/// <returns>The value of the specified column as a byte.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013CB RID: 5067 RVA: 0x00061B44 File Offset: 0x0005FD44
		public override byte GetByte(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Byte;
		}

		/// <summary>Reads a stream of bytes from the specified column offset into the buffer an array starting at the given buffer offset.</summary>
		/// <returns>The actual number of bytes read.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the field from which to begin the read operation.</param>
		/// <param name="buffer">The buffer into which to read the stream of bytes. </param>
		/// <param name="bufferIndex">The index within the <paramref name="buffer" /> where the write operation is to start. </param>
		/// <param name="length">The maximum length to copy into the buffer. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013CC RID: 5068 RVA: 0x00061B5C File Offset: 0x0005FD5C
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			SqlStatistics sqlStatistics = null;
			long num = 0L;
			this.CheckDataIsReady(i, true, false, "GetBytes");
			MetaType metaType = this._metaData[i].metaType;
			if ((!metaType.IsLong && !metaType.IsBinType) || SqlDbType.Xml == metaType.SqlDbType)
			{
				throw SQL.NonBlobColumn(this._metaData[i].column);
			}
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				num = this.GetBytesInternal(i, dataIndex, buffer, bufferIndex, length);
				this._lastColumnWithDataChunkRead = i;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return num;
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00061C08 File Offset: 0x0005FE08
		internal virtual long GetBytesInternal(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			long num;
			if (!this.TryGetBytesInternal(i, dataIndex, buffer, bufferIndex, length, out num))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return num;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00061C3C File Offset: 0x0005FE3C
		private bool TryGetBytesInternal(int i, long dataIndex, byte[] buffer, int bufferIndex, int length, out long remaining)
		{
			remaining = 0L;
			int num = 0;
			if (this.IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				if (this._sharedState._nextColumnHeaderToRead <= i && !this.TryReadColumnHeader(i))
				{
					return false;
				}
				if (this._data[i] != null && this._data[i].IsNull)
				{
					throw new SqlNullValueException();
				}
				if (-1L == this._sharedState._columnDataBytesRemaining && this._metaData[i].metaType.IsPlp)
				{
					ulong num2;
					if (!this._parser.TryPlpBytesLeft(this._stateObj, out num2))
					{
						return false;
					}
					this._sharedState._columnDataBytesRemaining = (long)num2;
				}
				if (this._sharedState._columnDataBytesRemaining == 0L)
				{
					return true;
				}
				if (buffer == null)
				{
					if (this._metaData[i].metaType.IsPlp)
					{
						remaining = (long)this._parser.PlpBytesTotalLength(this._stateObj);
						return true;
					}
					remaining = this._sharedState._columnDataBytesRemaining;
					return true;
				}
				else
				{
					if (dataIndex < 0L)
					{
						throw ADP.NegativeParameter("dataIndex");
					}
					if (dataIndex < this._columnDataBytesRead)
					{
						throw ADP.NonSeqByteAccess(dataIndex, this._columnDataBytesRead, "GetBytes");
					}
					long num3 = dataIndex - this._columnDataBytesRead;
					if (num3 > this._sharedState._columnDataBytesRemaining && !this._metaData[i].metaType.IsPlp)
					{
						return true;
					}
					if (bufferIndex < 0 || bufferIndex >= buffer.Length)
					{
						throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
					}
					if (length + bufferIndex > buffer.Length)
					{
						throw ADP.InvalidBufferSizeOrIndex(length, bufferIndex);
					}
					if (length < 0)
					{
						throw ADP.InvalidDataLength((long)length);
					}
					if (num3 > 0L)
					{
						if (this._metaData[i].metaType.IsPlp)
						{
							ulong num4;
							if (!this._parser.TrySkipPlpValue((ulong)num3, this._stateObj, out num4))
							{
								return false;
							}
							this._columnDataBytesRead += (long)num4;
						}
						else
						{
							if (!this._stateObj.TrySkipLongBytes(num3))
							{
								return false;
							}
							this._columnDataBytesRead += num3;
							this._sharedState._columnDataBytesRemaining -= num3;
						}
					}
					int num5;
					bool flag = this.TryGetBytesInternalSequential(i, buffer, bufferIndex, length, out num5);
					remaining = (long)num5;
					return flag;
				}
			}
			else
			{
				if (dataIndex < 0L)
				{
					throw ADP.NegativeParameter("dataIndex");
				}
				if (dataIndex > 2147483647L)
				{
					throw ADP.InvalidSourceBufferIndex(num, dataIndex, "dataIndex");
				}
				int num6 = (int)dataIndex;
				byte[] array;
				if (this._metaData[i].metaType.IsBinType)
				{
					array = this.GetSqlBinary(i).Value;
				}
				else
				{
					SqlString sqlString = this.GetSqlString(i);
					if (this._metaData[i].metaType.IsNCharType)
					{
						array = sqlString.GetUnicodeBytes();
					}
					else
					{
						array = sqlString.GetNonUnicodeBytes();
					}
				}
				num = array.Length;
				if (buffer == null)
				{
					remaining = (long)num;
					return true;
				}
				if (num6 < 0 || num6 >= num)
				{
					return true;
				}
				try
				{
					if (num6 < num)
					{
						if (num6 + length > num)
						{
							num -= num6;
						}
						else
						{
							num = length;
						}
					}
					Buffer.BlockCopy(array, num6, buffer, bufferIndex, num);
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableExceptionType(ex))
					{
						throw;
					}
					num = array.Length;
					if (length < 0)
					{
						throw ADP.InvalidDataLength((long)length);
					}
					if (bufferIndex < 0 || bufferIndex >= buffer.Length)
					{
						throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
					}
					if (num + bufferIndex > buffer.Length)
					{
						throw ADP.InvalidBufferSizeOrIndex(num, bufferIndex);
					}
					throw;
				}
				remaining = (long)num;
				return true;
			}
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00061F78 File Offset: 0x00060178
		internal int GetBytesInternalSequential(int i, byte[] buffer, int index, int length, long? timeoutMilliseconds = null)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			SqlStatistics sqlStatistics = null;
			int num;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(timeoutMilliseconds ?? this._defaultTimeoutMilliseconds);
				if (!this.TryReadColumnHeader(i))
				{
					throw SQL.SynchronousCallMayNotPend();
				}
				if (!this.TryGetBytesInternalSequential(i, buffer, index, length, out num))
				{
					throw SQL.SynchronousCallMayNotPend();
				}
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return num;
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00062000 File Offset: 0x00060200
		internal bool TryGetBytesInternalSequential(int i, byte[] buffer, int index, int length, out int bytesRead)
		{
			bytesRead = 0;
			if (this._sharedState._columnDataBytesRemaining == 0L || length == 0)
			{
				bytesRead = 0;
				return true;
			}
			if (!this._metaData[i].metaType.IsPlp)
			{
				int num = (int)Math.Min((long)length, this._sharedState._columnDataBytesRemaining);
				bool flag = this._stateObj.TryReadByteArray(buffer, index, num, out bytesRead);
				this._columnDataBytesRead += (long)bytesRead;
				this._sharedState._columnDataBytesRemaining -= (long)bytesRead;
				return flag;
			}
			bool flag2 = this._stateObj.TryReadPlpBytes(ref buffer, index, length, out bytesRead);
			this._columnDataBytesRead += (long)bytesRead;
			if (!flag2)
			{
				return false;
			}
			ulong num2;
			if (!this._parser.TryPlpBytesLeft(this._stateObj, out num2))
			{
				this._sharedState._columnDataBytesRemaining = -1L;
				return false;
			}
			this._sharedState._columnDataBytesRemaining = (long)num2;
			return true;
		}

		/// <summary>Retrieves Char, NChar, NText, NVarChar, text, varChar, and Variant data types as a <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>The returned object.</returns>
		/// <param name="i">The column to be retrieved.</param>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).Tried to read a previously-read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">The returned type was not one of the types below:charncharntextnvarchartextvarchar</exception>
		// Token: 0x060013D1 RID: 5073 RVA: 0x000620E8 File Offset: 0x000602E8
		public override TextReader GetTextReader(int i)
		{
			this.CheckDataIsReady(i, false, false, "GetTextReader");
			MetaType metaType = this._metaData[i].metaType;
			if ((!metaType.IsCharType && metaType.SqlDbType != SqlDbType.Variant) || metaType.SqlDbType == SqlDbType.Xml)
			{
				throw SQL.TextReaderNotSupportOnColumnType(this._metaData[i].column);
			}
			if (metaType.SqlDbType != SqlDbType.Variant && this.IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				Encoding encoding;
				if (metaType.IsNCharType)
				{
					encoding = SqlUnicodeEncoding.SqlUnicodeEncodingInstance;
				}
				else
				{
					encoding = this._metaData[i].encoding;
				}
				this._currentTextReader = new SqlSequentialTextReader(this, i, encoding);
				this._lastColumnWithDataChunkRead = i;
				return this._currentTextReader;
			}
			this.ReadColumn(i, true, false);
			string text;
			if (this._data[i].IsNull)
			{
				text = string.Empty;
			}
			else
			{
				text = this._data[i].SqlString.Value;
			}
			return new StringReader(text);
		}

		/// <summary>Gets the value of the specified column as a single character.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060013D2 RID: 5074 RVA: 0x000621D6 File Offset: 0x000603D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override char GetChar(int i)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Reads a stream of characters from the specified column offset into the buffer as an array starting at the given buffer offset.</summary>
		/// <returns>The actual number of characters read.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the field from which to begin the read operation.</param>
		/// <param name="buffer">The buffer into which to read the stream of bytes. </param>
		/// <param name="bufferIndex">The index within the <paramref name="buffer" /> where the write operation is to start. </param>
		/// <param name="length">The maximum length to copy into the buffer. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013D3 RID: 5075 RVA: 0x000621E0 File Offset: 0x000603E0
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			SqlStatistics sqlStatistics = null;
			this.CheckMetaDataIsReady(i, false);
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			long num2;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				if (this._metaData[i].metaType.IsPlp && this.IsCommandBehavior(CommandBehavior.SequentialAccess))
				{
					if (length < 0)
					{
						throw ADP.InvalidDataLength((long)length);
					}
					if (bufferIndex < 0 || (buffer != null && bufferIndex >= buffer.Length))
					{
						throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
					}
					if (buffer != null && length + bufferIndex > buffer.Length)
					{
						throw ADP.InvalidBufferSizeOrIndex(length, bufferIndex);
					}
					long num;
					if (this._metaData[i].type == SqlDbType.Xml)
					{
						try
						{
							this.CheckDataIsReady(i, true, false, "GetChars");
						}
						catch (Exception ex)
						{
							if (ADP.IsCatchableExceptionType(ex))
							{
								throw new TargetInvocationException(ex);
							}
							throw;
						}
						num = this.GetStreamingXmlChars(i, dataIndex, buffer, bufferIndex, length);
					}
					else
					{
						this.CheckDataIsReady(i, true, false, "GetChars");
						num = this.GetCharsFromPlpData(i, dataIndex, buffer, bufferIndex, length);
					}
					this._lastColumnWithDataChunkRead = i;
					num2 = num;
				}
				else
				{
					if (this._sharedState._nextColumnDataToRead == i + 1 && this._sharedState._nextColumnHeaderToRead == i + 1 && this._columnDataChars != null && this.IsCommandBehavior(CommandBehavior.SequentialAccess) && dataIndex < this._columnDataCharsRead)
					{
						throw ADP.NonSeqByteAccess(dataIndex, this._columnDataCharsRead, "GetChars");
					}
					if (this._columnDataCharsIndex != i)
					{
						string value = this.GetSqlString(i).Value;
						this._columnDataChars = value.ToCharArray();
						this._columnDataCharsRead = 0L;
						this._columnDataCharsIndex = i;
					}
					int num3 = this._columnDataChars.Length;
					if (dataIndex > 2147483647L)
					{
						throw ADP.InvalidSourceBufferIndex(num3, dataIndex, "dataIndex");
					}
					int num4 = (int)dataIndex;
					if (buffer == null)
					{
						num2 = (long)num3;
					}
					else if (num4 < 0 || num4 >= num3)
					{
						num2 = 0L;
					}
					else
					{
						try
						{
							if (num4 < num3)
							{
								if (num4 + length > num3)
								{
									num3 -= num4;
								}
								else
								{
									num3 = length;
								}
							}
							Array.Copy(this._columnDataChars, num4, buffer, bufferIndex, num3);
							this._columnDataCharsRead += (long)num3;
						}
						catch (Exception ex2)
						{
							if (!ADP.IsCatchableExceptionType(ex2))
							{
								throw;
							}
							num3 = this._columnDataChars.Length;
							if (length < 0)
							{
								throw ADP.InvalidDataLength((long)length);
							}
							if (bufferIndex < 0 || bufferIndex >= buffer.Length)
							{
								throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
							}
							if (num3 + bufferIndex > buffer.Length)
							{
								throw ADP.InvalidBufferSizeOrIndex(num3, bufferIndex);
							}
							throw;
						}
						num2 = (long)num3;
					}
				}
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return num2;
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x000624A8 File Offset: 0x000606A8
		private long GetCharsFromPlpData(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			if (!this._metaData[i].metaType.IsCharType)
			{
				throw SQL.NonCharColumn(this._metaData[i].column);
			}
			if (this._sharedState._nextColumnHeaderToRead <= i)
			{
				this.ReadColumnHeader(i);
			}
			if (this._data[i] != null && this._data[i].IsNull)
			{
				throw new SqlNullValueException();
			}
			if (dataIndex < this._columnDataCharsRead)
			{
				throw ADP.NonSeqByteAccess(dataIndex, this._columnDataCharsRead, "GetChars");
			}
			if (dataIndex == 0L)
			{
				this._stateObj._plpdecoder = null;
			}
			bool isNCharType = this._metaData[i].metaType.IsNCharType;
			if (-1L == this._sharedState._columnDataBytesRemaining)
			{
				this._sharedState._columnDataBytesRemaining = (long)this._parser.PlpBytesLeft(this._stateObj);
			}
			if (this._sharedState._columnDataBytesRemaining == 0L)
			{
				this._stateObj._plpdecoder = null;
				return 0L;
			}
			long num;
			if (buffer != null)
			{
				if (dataIndex > this._columnDataCharsRead)
				{
					this._stateObj._plpdecoder = null;
					num = dataIndex - this._columnDataCharsRead;
					num = (isNCharType ? (num << 1) : num);
					num = (long)this._parser.SkipPlpValue((ulong)num, this._stateObj);
					this._columnDataBytesRead += num;
					this._columnDataCharsRead += ((isNCharType && num > 0L) ? (num >> 1) : num);
				}
				num = (long)length;
				if (isNCharType)
				{
					num = (long)this._parser.ReadPlpUnicodeChars(ref buffer, bufferIndex, length, this._stateObj);
					this._columnDataBytesRead += num << 1;
				}
				else
				{
					num = (long)this._parser.ReadPlpAnsiChars(ref buffer, bufferIndex, length, this._metaData[i], this._stateObj);
					this._columnDataBytesRead += num << 1;
				}
				this._columnDataCharsRead += num;
				this._sharedState._columnDataBytesRemaining = (long)this._parser.PlpBytesLeft(this._stateObj);
				return num;
			}
			num = (long)this._parser.PlpBytesTotalLength(this._stateObj);
			if (!isNCharType || num <= 0L)
			{
				return num;
			}
			return num >> 1;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x000626BC File Offset: 0x000608BC
		internal long GetStreamingXmlChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			if (this._streamingXml != null && this._streamingXml.ColumnOrdinal != i)
			{
				this._streamingXml.Close();
				this._streamingXml = null;
			}
			SqlStreamingXml sqlStreamingXml;
			if (this._streamingXml == null)
			{
				sqlStreamingXml = new SqlStreamingXml(i, this);
			}
			else
			{
				sqlStreamingXml = this._streamingXml;
			}
			long chars = sqlStreamingXml.GetChars(dataIndex, buffer, bufferIndex, length);
			if (this._streamingXml == null)
			{
				this._streamingXml = sqlStreamingXml;
			}
			return chars;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013D6 RID: 5078 RVA: 0x00062728 File Offset: 0x00060928
		public override DateTime GetDateTime(int i)
		{
			this.ReadColumn(i, true, false);
			DateTime dateTime = this._data[i].DateTime;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsNewKatmaiDateTimeType)
			{
				dateTime = (DateTime)this._data[i].String;
			}
			return dateTime;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Decimal" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013D7 RID: 5079 RVA: 0x00062780 File Offset: 0x00060980
		public override decimal GetDecimal(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Decimal;
		}

		/// <summary>Gets the value of the specified column as a double-precision floating point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013D8 RID: 5080 RVA: 0x00062798 File Offset: 0x00060998
		public override double GetDouble(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Double;
		}

		/// <summary>Gets the value of the specified column as a single-precision floating point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013D9 RID: 5081 RVA: 0x000627B0 File Offset: 0x000609B0
		public override float GetFloat(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Single;
		}

		/// <summary>Gets the value of the specified column as a globally unique identifier (GUID).</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013DA RID: 5082 RVA: 0x000627C8 File Offset: 0x000609C8
		public override Guid GetGuid(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlGuid.Value;
		}

		/// <summary>Gets the value of the specified column as a 16-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013DB RID: 5083 RVA: 0x000627F3 File Offset: 0x000609F3
		public override short GetInt16(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Int16;
		}

		/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013DC RID: 5084 RVA: 0x0006280B File Offset: 0x00060A0B
		public override int GetInt32(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Int32;
		}

		/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013DD RID: 5085 RVA: 0x00062823 File Offset: 0x00060A23
		public override long GetInt64(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Int64;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013DE RID: 5086 RVA: 0x0006283B File Offset: 0x00060A3B
		public virtual SqlBoolean GetSqlBoolean(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlBoolean;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlBinary" />.</summary>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlBinary" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013DF RID: 5087 RVA: 0x00062853 File Offset: 0x00060A53
		public virtual SqlBinary GetSqlBinary(int i)
		{
			this.ReadColumn(i, true, true);
			return this._data[i].SqlBinary;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>The value of the column expressed as a  <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013E0 RID: 5088 RVA: 0x0006286B File Offset: 0x00060A6B
		public virtual SqlByte GetSqlByte(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlByte;
		}

		/// <summary>Gets the value of the specified column as <see cref="T:System.Data.SqlTypes.SqlBytes" />.</summary>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlBytes" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013E1 RID: 5089 RVA: 0x00062883 File Offset: 0x00060A83
		public virtual SqlBytes GetSqlBytes(int i)
		{
			this.ReadColumn(i, true, false);
			return new SqlBytes(this._data[i].SqlBinary);
		}

		/// <summary>Gets the value of the specified column as <see cref="T:System.Data.SqlTypes.SqlChars" />.</summary>
		/// <returns>The value of the column expressed as a  <see cref="T:System.Data.SqlTypes.SqlChars" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013E2 RID: 5090 RVA: 0x000628A0 File Offset: 0x00060AA0
		public virtual SqlChars GetSqlChars(int i)
		{
			this.ReadColumn(i, true, false);
			SqlString sqlString;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsNewKatmaiDateTimeType)
			{
				sqlString = this._data[i].KatmaiDateTimeSqlString;
			}
			else
			{
				sqlString = this._data[i].SqlString;
			}
			return new SqlChars(sqlString);
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</summary>
		/// <returns>The value of the column expressed as a  <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013E3 RID: 5091 RVA: 0x000628FA File Offset: 0x00060AFA
		public virtual SqlDateTime GetSqlDateTime(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlDateTime;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013E4 RID: 5092 RVA: 0x00062912 File Offset: 0x00060B12
		public virtual SqlDecimal GetSqlDecimal(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlDecimal;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
		/// <returns>The value of the column expressed as a  <see cref="T:System.Data.SqlTypes.SqlGuid" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013E5 RID: 5093 RVA: 0x0006292A File Offset: 0x00060B2A
		public virtual SqlGuid GetSqlGuid(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlGuid;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>The value of the column expressed as a  <see cref="T:System.Data.SqlTypes.SqlDouble" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013E6 RID: 5094 RVA: 0x00062942 File Offset: 0x00060B42
		public virtual SqlDouble GetSqlDouble(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlDouble;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013E7 RID: 5095 RVA: 0x0006295A File Offset: 0x00060B5A
		public virtual SqlInt16 GetSqlInt16(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlInt16;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013E8 RID: 5096 RVA: 0x00062972 File Offset: 0x00060B72
		public virtual SqlInt32 GetSqlInt32(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlInt32;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013E9 RID: 5097 RVA: 0x0006298A File Offset: 0x00060B8A
		public virtual SqlInt64 GetSqlInt64(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlInt64;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013EA RID: 5098 RVA: 0x000629A2 File Offset: 0x00060BA2
		public virtual SqlMoney GetSqlMoney(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlMoney;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013EB RID: 5099 RVA: 0x000629BA File Offset: 0x00060BBA
		public virtual SqlSingle GetSqlSingle(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlSingle;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013EC RID: 5100 RVA: 0x000629D4 File Offset: 0x00060BD4
		public virtual SqlString GetSqlString(int i)
		{
			this.ReadColumn(i, true, false);
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsNewKatmaiDateTimeType)
			{
				return this._data[i].KatmaiDateTimeSqlString;
			}
			return this._data[i].SqlString;
		}

		/// <summary>Gets the value of the specified column as an XML value.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlXml" /> value that contains the XML stored within the corresponding field. </returns>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access columns in a closed <see cref="T:System.Data.SqlClient.SqlDataReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The retrieved data is not compatible with the <see cref="T:System.Data.SqlTypes.SqlXml" /> type.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013ED RID: 5101 RVA: 0x00062A28 File Offset: 0x00060C28
		public virtual SqlXml GetSqlXml(int i)
		{
			this.ReadColumn(i, true, false);
			SqlXml sqlXml;
			if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				sqlXml = (this._data[i].IsNull ? SqlXml.Null : this._data[i].SqlCachedBuffer.ToSqlXml());
			}
			else
			{
				SqlXml sqlXml2 = (this._data[i].IsNull ? SqlXml.Null : this._data[i].SqlCachedBuffer.ToSqlXml());
				sqlXml = (SqlXml)this._data[i].String;
			}
			return sqlXml;
		}

		/// <summary>Returns the data value in the specified column as a SQL Server type. </summary>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlDbType" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013EE RID: 5102 RVA: 0x00062AB8 File Offset: 0x00060CB8
		public virtual object GetSqlValue(int i)
		{
			SqlStatistics sqlStatistics = null;
			object sqlValueInternal;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				sqlValueInternal = this.GetSqlValueInternal(i);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return sqlValueInternal;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00062B04 File Offset: 0x00060D04
		private object GetSqlValueInternal(int i)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this.TryReadColumn(i, false, false))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return this.GetSqlValueFromSqlBufferInternal(this._data[i], this._metaData[i]);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00062B40 File Offset: 0x00060D40
		private object GetSqlValueFromSqlBufferInternal(SqlBuffer data, _SqlMetaData metaData)
		{
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
			{
				return data.KatmaiDateTimeSqlString;
			}
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
			{
				return data.SqlValue;
			}
			if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				if (metaData.type != SqlDbType.Udt)
				{
					return data.SqlValue;
				}
				if (this._connection != null)
				{
					throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
				}
				throw ADP.DataReaderClosed("GetSqlValueFromSqlBufferInternal");
			}
			else
			{
				if (metaData.type == SqlDbType.Xml)
				{
					return data.SqlString;
				}
				return data.SqlValue;
			}
		}

		/// <summary>Fills an array of <see cref="T:System.Object" /> that contains the values for all the columns in the record, expressed as SQL Server types.</summary>
		/// <returns>An integer indicating the number of columns copied.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the values. The column values are expressed as SQL Server types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013F1 RID: 5105 RVA: 0x00062BF0 File Offset: 0x00060DF0
		public virtual int GetSqlValues(object[] values)
		{
			SqlStatistics sqlStatistics = null;
			int num2;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.CheckDataIsReady();
				if (values == null)
				{
					throw ADP.ArgumentNull("values");
				}
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				int num = ((values.Length < this._metaData.visibleColumns) ? values.Length : this._metaData.visibleColumns);
				for (int i = 0; i < num; i++)
				{
					values[this._metaData.indexMap[i]] = this.GetSqlValueInternal(i);
				}
				num2 = num;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return num2;
		}

		/// <summary>Gets the value of the specified column as a string.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013F2 RID: 5106 RVA: 0x00062C8C File Offset: 0x00060E8C
		public override string GetString(int i)
		{
			this.ReadColumn(i, true, false);
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsNewKatmaiDateTimeType)
			{
				return this._data[i].KatmaiDateTimeString;
			}
			return this._data[i].String;
		}

		/// <summary>Synchronously gets the value of the specified column as a type. <see cref="M:System.Data.SqlClient.SqlDataReader.GetFieldValueAsync``1(System.Int32,System.Threading.CancellationToken)" /> is the asynchronous version of this method.</summary>
		/// <returns>The returned type object.</returns>
		/// <param name="i">The column to be retrieved.</param>
		/// <typeparam name="T">The type of the value to be returned. See the remarks section for more information.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).Tried to read a previously-read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The value of the column was null (<see cref="M:System.Data.SqlClient.SqlDataReader.IsDBNull(System.Int32)" /> == true), retrieving a non-SQL type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="T" /> doesn’t match the type returned by SQL Server or cannot be cast.</exception>
		// Token: 0x060013F3 RID: 5107 RVA: 0x00062CE0 File Offset: 0x00060EE0
		public override T GetFieldValue<T>(int i)
		{
			SqlStatistics sqlStatistics = null;
			T fieldValueInternal;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				fieldValueInternal = this.GetFieldValueInternal<T>(i);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return fieldValueInternal;
		}

		/// <summary>Gets the value of the specified column in its native format.</summary>
		/// <returns>This method returns <see cref="T:System.DBNull" /> for null database columns.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013F4 RID: 5108 RVA: 0x00062D2C File Offset: 0x00060F2C
		public override object GetValue(int i)
		{
			SqlStatistics sqlStatistics = null;
			object valueInternal;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				valueInternal = this.GetValueInternal(i);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return valueInternal;
		}

		/// <summary>Retrieves the value of the specified column as a <see cref="T:System.TimeSpan" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		// Token: 0x060013F5 RID: 5109 RVA: 0x00062D78 File Offset: 0x00060F78
		public virtual TimeSpan GetTimeSpan(int i)
		{
			this.ReadColumn(i, true, false);
			TimeSpan timeSpan = this._data[i].Time;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005)
			{
				timeSpan = (TimeSpan)this._data[i].String;
			}
			return timeSpan;
		}

		/// <summary>Retrieves the value of the specified column as a <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		// Token: 0x060013F6 RID: 5110 RVA: 0x00062DC0 File Offset: 0x00060FC0
		public virtual DateTimeOffset GetDateTimeOffset(int i)
		{
			this.ReadColumn(i, true, false);
			DateTimeOffset dateTimeOffset = this._data[i].DateTimeOffset;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005)
			{
				dateTimeOffset = (DateTimeOffset)this._data[i].String;
			}
			return dateTimeOffset;
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00062E05 File Offset: 0x00061005
		private object GetValueInternal(int i)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this.TryReadColumn(i, false, false))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return this.GetValueFromSqlBufferInternal(this._data[i], this._metaData[i]);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00062E40 File Offset: 0x00061040
		private object GetValueFromSqlBufferInternal(SqlBuffer data, _SqlMetaData metaData)
		{
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
			{
				if (data.IsNull)
				{
					return DBNull.Value;
				}
				return data.KatmaiDateTimeString;
			}
			else
			{
				if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
				{
					return data.Value;
				}
				if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2000)
				{
					return data.Value;
				}
				if (metaData.type != SqlDbType.Udt)
				{
					return data.Value;
				}
				if (this._connection != null)
				{
					throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
				}
				throw ADP.DataReaderClosed("GetValueFromSqlBufferInternal");
			}
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00062EE0 File Offset: 0x000610E0
		private T GetFieldValueInternal<T>(int i)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this.TryReadColumn(i, false, false))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return this.GetFieldValueFromSqlBufferInternal<T>(this._data[i], this._metaData[i]);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00062F1C File Offset: 0x0006111C
		private T GetFieldValueFromSqlBufferInternal<T>(SqlBuffer data, _SqlMetaData metaData)
		{
			Type typeFromHandle = typeof(T);
			if (SqlDataReader._typeofINullable.IsAssignableFrom(typeFromHandle))
			{
				object obj = this.GetSqlValueFromSqlBufferInternal(data, metaData);
				if (typeFromHandle == SqlDataReader.s_typeofSqlString)
				{
					SqlXml sqlXml = obj as SqlXml;
					if (sqlXml != null)
					{
						if (sqlXml.IsNull)
						{
							obj = SqlString.Null;
						}
						else
						{
							obj = new SqlString(sqlXml.Value);
						}
					}
				}
				return (T)((object)obj);
			}
			T t;
			try
			{
				t = (T)((object)this.GetValueFromSqlBufferInternal(data, metaData));
			}
			catch (InvalidCastException)
			{
				if (data.IsNull)
				{
					throw SQL.SqlNullValue();
				}
				throw;
			}
			return t;
		}

		/// <summary>Populates an array of objects with the column values of the current row.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the attribute columns. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013FB RID: 5115 RVA: 0x00062FC4 File Offset: 0x000611C4
		public override int GetValues(object[] values)
		{
			SqlStatistics sqlStatistics = null;
			bool flag = this.IsCommandBehavior(CommandBehavior.SequentialAccess);
			int num3;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				if (values == null)
				{
					throw ADP.ArgumentNull("values");
				}
				this.CheckMetaDataIsReady();
				int num = ((values.Length < this._metaData.visibleColumns) ? values.Length : this._metaData.visibleColumns);
				int num2 = num - 1;
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				this._commandBehavior &= ~CommandBehavior.SequentialAccess;
				if (!this.TryReadColumn(num2, false, false))
				{
					throw SQL.SynchronousCallMayNotPend();
				}
				for (int i = 0; i < num; i++)
				{
					values[this._metaData.indexMap[i]] = this.GetValueFromSqlBufferInternal(this._data[i], this._metaData[i]);
					if (flag && i < num2)
					{
						this._data[i].Clear();
					}
				}
				num3 = num;
			}
			finally
			{
				if (flag)
				{
					this._commandBehavior |= CommandBehavior.SequentialAccess;
				}
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return num3;
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x000630D0 File Offset: 0x000612D0
		private MetaType GetVersionedMetaType(MetaType actualMetaType)
		{
			MetaType metaType;
			if (actualMetaType == MetaType.MetaUdt)
			{
				metaType = MetaType.MetaVarBinary;
			}
			else if (actualMetaType == MetaType.MetaXml)
			{
				metaType = MetaType.MetaNText;
			}
			else if (actualMetaType == MetaType.MetaMaxVarBinary)
			{
				metaType = MetaType.MetaImage;
			}
			else if (actualMetaType == MetaType.MetaMaxVarChar)
			{
				metaType = MetaType.MetaText;
			}
			else if (actualMetaType == MetaType.MetaMaxNVarChar)
			{
				metaType = MetaType.MetaNText;
			}
			else
			{
				metaType = actualMetaType;
			}
			return metaType;
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00063134 File Offset: 0x00061334
		private bool TryHasMoreResults(out bool moreResults)
		{
			if (this._parser != null)
			{
				bool flag;
				if (!this.TryHasMoreRows(out flag))
				{
					moreResults = false;
					return false;
				}
				if (flag)
				{
					moreResults = false;
					return true;
				}
				while (this._stateObj._pendingData)
				{
					byte b;
					if (!this._stateObj.TryPeekByte(out b))
					{
						moreResults = false;
						return false;
					}
					if (b <= 210)
					{
						if (b == 129)
						{
							moreResults = true;
							return true;
						}
						if (b - 209 <= 1)
						{
							moreResults = true;
							return true;
						}
					}
					else
					{
						if (b == 211)
						{
							if (this._altRowStatus == SqlDataReader.ALTROWSTATUS.Null)
							{
								this._altMetaDataSetCollection.metaDataSet = this._metaData;
								this._metaData = null;
							}
							this._altRowStatus = SqlDataReader.ALTROWSTATUS.AltRow;
							this._hasRows = true;
							moreResults = true;
							return true;
						}
						if (b == 253)
						{
							this._altRowStatus = SqlDataReader.ALTROWSTATUS.Null;
							this._metaData = null;
							this._altMetaDataSetCollection = null;
							moreResults = true;
							return true;
						}
					}
					if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
					{
						throw ADP.ClosedConnectionError();
					}
					bool flag2;
					if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out flag2))
					{
						moreResults = false;
						return false;
					}
				}
			}
			moreResults = false;
			return true;
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00063258 File Offset: 0x00061458
		private bool TryHasMoreRows(out bool moreRows)
		{
			if (this._parser != null)
			{
				if (this._sharedState._dataReady)
				{
					moreRows = true;
					return true;
				}
				SqlDataReader.ALTROWSTATUS altRowStatus = this._altRowStatus;
				if (altRowStatus == SqlDataReader.ALTROWSTATUS.AltRow)
				{
					moreRows = true;
					return true;
				}
				if (altRowStatus == SqlDataReader.ALTROWSTATUS.Done)
				{
					moreRows = false;
					return true;
				}
				if (this._stateObj._pendingData)
				{
					byte b;
					if (!this._stateObj.TryPeekByte(out b))
					{
						moreRows = false;
						return false;
					}
					bool flag = false;
					while (b == 253 || b == 254 || b == 255 || (!flag && (b == 228 || b == 227 || b == 169 || b == 170 || b == 171)))
					{
						if (b == 253 || b == 254 || b == 255)
						{
							flag = true;
						}
						if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
						{
							throw ADP.ClosedConnectionError();
						}
						bool flag2;
						if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out flag2))
						{
							moreRows = false;
							return false;
						}
						if (!this._stateObj._pendingData)
						{
							break;
						}
						if (!this._stateObj.TryPeekByte(out b))
						{
							moreRows = false;
							return false;
						}
					}
					if (this.IsRowToken(b))
					{
						moreRows = true;
						return true;
					}
				}
			}
			moreRows = false;
			return true;
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x000633B1 File Offset: 0x000615B1
		private bool IsRowToken(byte token)
		{
			return 209 == token || 210 == token;
		}

		/// <summary>Gets a value that indicates whether the column contains non-existent or missing values.</summary>
		/// <returns>true if the specified column value is equivalent to <see cref="T:System.DBNull" />; otherwise false.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001400 RID: 5120 RVA: 0x000633C5 File Offset: 0x000615C5
		public override bool IsDBNull(int i)
		{
			this.CheckHeaderIsReady(i, false, "IsDBNull");
			this.SetTimeout(this._defaultTimeoutMilliseconds);
			this.ReadColumnHeader(i);
			return this._data[i].IsNull;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Data.CommandBehavior" /> matches that of the <see cref="T:System.Data.SqlClient.SqlDataReader" /> .</summary>
		/// <returns>true if the specified <see cref="T:System.Data.CommandBehavior" /> is true, false otherwise.</returns>
		/// <param name="condition">A <see cref="T:System.Data.CommandBehavior" /> enumeration.</param>
		// Token: 0x06001401 RID: 5121 RVA: 0x000633F4 File Offset: 0x000615F4
		protected internal bool IsCommandBehavior(CommandBehavior condition)
		{
			return condition == (condition & this._commandBehavior);
		}

		/// <summary>Advances the data reader to the next result, when reading the results of batch Transact-SQL statements.</summary>
		/// <returns>true if there are more result sets; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001402 RID: 5122 RVA: 0x00063404 File Offset: 0x00061604
		public override bool NextResult()
		{
			if (this._currentTask != null)
			{
				throw SQL.PendingBeginXXXExists();
			}
			bool flag;
			if (!this.TryNextResult(out flag))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return flag;
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00063430 File Offset: 0x00061630
		private bool TryNextResult(out bool more)
		{
			SqlStatistics sqlStatistics = null;
			bool flag2;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("NextResult");
				}
				this._fieldNameLookup = null;
				bool flag = false;
				this._hasRows = false;
				if (this.IsCommandBehavior(CommandBehavior.SingleResult))
				{
					if (!this.TryCloseInternal(false))
					{
						more = false;
						flag2 = false;
					}
					else
					{
						this.ClearMetaData();
						more = flag;
						flag2 = true;
					}
				}
				else
				{
					if (this._parser != null)
					{
						bool flag3 = true;
						while (flag3)
						{
							if (!this.TryReadInternal(false, out flag3))
							{
								more = false;
								return false;
							}
						}
					}
					if (this._parser != null)
					{
						bool flag4;
						if (!this.TryHasMoreResults(out flag4))
						{
							more = false;
							return false;
						}
						if (flag4)
						{
							this._metaDataConsumed = false;
							this._browseModeInfoConsumed = false;
							SqlDataReader.ALTROWSTATUS altRowStatus = this._altRowStatus;
							if (altRowStatus != SqlDataReader.ALTROWSTATUS.AltRow)
							{
								if (altRowStatus != SqlDataReader.ALTROWSTATUS.Done)
								{
									if (!this.TryConsumeMetaData())
									{
										more = false;
										return false;
									}
									if (this._metaData == null)
									{
										more = false;
										return true;
									}
								}
								else
								{
									this._metaData = this._altMetaDataSetCollection.metaDataSet;
									this._altRowStatus = SqlDataReader.ALTROWSTATUS.Null;
								}
							}
							else
							{
								int num;
								if (!this._parser.TryGetAltRowId(this._stateObj, out num))
								{
									more = false;
									return false;
								}
								_SqlMetaDataSet altMetaData = this._altMetaDataSetCollection.GetAltMetaData(num);
								if (altMetaData != null)
								{
									this._metaData = altMetaData;
								}
							}
							flag = true;
						}
						else
						{
							if (!this.TryCloseInternal(false))
							{
								more = false;
								return false;
							}
							if (!this.TrySetMetaData(null, false))
							{
								more = false;
								return false;
							}
						}
					}
					else
					{
						this.ClearMetaData();
					}
					more = flag;
					flag2 = true;
				}
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return flag2;
		}

		/// <summary>Advances the <see cref="T:System.Data.SqlClient.SqlDataReader" /> to the next record.</summary>
		/// <returns>true if there are more rows; otherwise false.</returns>
		/// <exception cref="T:System.Data.SqlClient.SqlException">SQL Server returned an error while executing the command text.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001404 RID: 5124 RVA: 0x000635DC File Offset: 0x000617DC
		public override bool Read()
		{
			if (this._currentTask != null)
			{
				throw SQL.PendingBeginXXXExists();
			}
			bool flag;
			if (!this.TryReadInternal(true, out flag))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return flag;
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0006360C File Offset: 0x0006180C
		private bool TryReadInternal(bool setTimeout, out bool more)
		{
			SqlStatistics sqlStatistics = null;
			bool flag3;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				if (this._parser != null)
				{
					if (setTimeout)
					{
						this.SetTimeout(this._defaultTimeoutMilliseconds);
					}
					if (this._sharedState._dataReady && !this.TryCleanPartialRead())
					{
						more = false;
						return false;
					}
					SqlBuffer.Clear(this._data);
					this._sharedState._nextColumnHeaderToRead = 0;
					this._sharedState._nextColumnDataToRead = 0;
					this._sharedState._columnDataBytesRemaining = -1L;
					this._lastColumnWithDataChunkRead = -1;
					if (!this._haltRead)
					{
						bool flag;
						if (!this.TryHasMoreRows(out flag))
						{
							more = false;
							return false;
						}
						if (flag)
						{
							while (this._stateObj._pendingData)
							{
								if (this._altRowStatus == SqlDataReader.ALTROWSTATUS.AltRow)
								{
									this._altRowStatus = SqlDataReader.ALTROWSTATUS.Done;
									this._sharedState._dataReady = true;
									break;
								}
								if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out this._sharedState._dataReady))
								{
									more = false;
									return false;
								}
								if (this._sharedState._dataReady)
								{
									break;
								}
							}
							if (this._sharedState._dataReady)
							{
								this._haltRead = this.IsCommandBehavior(CommandBehavior.SingleRow);
								more = true;
								return true;
							}
						}
						if (!this._stateObj._pendingData && !this.TryCloseInternal(false))
						{
							more = false;
							return false;
						}
					}
					else
					{
						bool flag2;
						if (!this.TryHasMoreRows(out flag2))
						{
							more = false;
							return false;
						}
						while (flag2)
						{
							while (this._stateObj._pendingData && !this._sharedState._dataReady)
							{
								if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out this._sharedState._dataReady))
								{
									more = false;
									return false;
								}
							}
							if (this._sharedState._dataReady && !this.TryCleanPartialRead())
							{
								more = false;
								return false;
							}
							SqlBuffer.Clear(this._data);
							this._sharedState._nextColumnHeaderToRead = 0;
							if (!this.TryHasMoreRows(out flag2))
							{
								more = false;
								return false;
							}
						}
						this._haltRead = false;
					}
				}
				else if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("Read");
				}
				more = false;
				flag3 = true;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return flag3;
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00063864 File Offset: 0x00061A64
		private void ReadColumn(int i, bool setTimeout = true, bool allowPartiallyReadColumn = false)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this.TryReadColumn(i, setTimeout, allowPartiallyReadColumn))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x00063885 File Offset: 0x00061A85
		private bool TryReadColumn(int i, bool setTimeout, bool allowPartiallyReadColumn = false)
		{
			this.CheckDataIsReady(i, allowPartiallyReadColumn, true, null);
			if (setTimeout)
			{
				this.SetTimeout(this._defaultTimeoutMilliseconds);
			}
			return this.TryReadColumnInternal(i, false);
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x000638B0 File Offset: 0x00061AB0
		private bool TryReadColumnData()
		{
			if (!this._data[this._sharedState._nextColumnDataToRead].IsNull)
			{
				_SqlMetaData sqlMetaData = this._metaData[this._sharedState._nextColumnDataToRead];
				if (!this._parser.TryReadSqlValue(this._data[this._sharedState._nextColumnDataToRead], sqlMetaData, (int)this._sharedState._columnDataBytesRemaining, this._stateObj))
				{
					return false;
				}
				this._sharedState._columnDataBytesRemaining = 0L;
			}
			this._sharedState._nextColumnDataToRead++;
			return true;
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00063942 File Offset: 0x00061B42
		private void ReadColumnHeader(int i)
		{
			if (!this.TryReadColumnHeader(i))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x00063953 File Offset: 0x00061B53
		private bool TryReadColumnHeader(int i)
		{
			if (!this._sharedState._dataReady)
			{
				throw SQL.InvalidRead();
			}
			return this.TryReadColumnInternal(i, true);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00063970 File Offset: 0x00061B70
		private bool TryReadColumnInternal(int i, bool readHeaderOnly = false)
		{
			if (i < this._sharedState._nextColumnHeaderToRead)
			{
				return i != this._sharedState._nextColumnDataToRead || readHeaderOnly || this.TryReadColumnData();
			}
			bool flag = this.IsCommandBehavior(CommandBehavior.SequentialAccess);
			if (flag)
			{
				if (0 < this._sharedState._nextColumnDataToRead)
				{
					this._data[this._sharedState._nextColumnDataToRead - 1].Clear();
				}
				if (this._lastColumnWithDataChunkRead > -1 && i > this._lastColumnWithDataChunkRead)
				{
					this.CloseActiveSequentialStreamAndTextReader();
				}
			}
			else if (this._sharedState._nextColumnDataToRead < this._sharedState._nextColumnHeaderToRead && !this.TryReadColumnData())
			{
				return false;
			}
			if (!this.TryResetBlobState())
			{
				return false;
			}
			for (;;)
			{
				_SqlMetaData sqlMetaData = this._metaData[this._sharedState._nextColumnHeaderToRead];
				if (flag && this._sharedState._nextColumnHeaderToRead < i)
				{
					if (!this._parser.TrySkipValue(sqlMetaData, this._sharedState._nextColumnHeaderToRead, this._stateObj))
					{
						break;
					}
					this._sharedState._nextColumnDataToRead = this._sharedState._nextColumnHeaderToRead;
					this._sharedState._nextColumnHeaderToRead++;
				}
				else
				{
					bool flag2;
					ulong num;
					if (!this._parser.TryProcessColumnHeader(sqlMetaData, this._stateObj, this._sharedState._nextColumnHeaderToRead, out flag2, out num))
					{
						return false;
					}
					this._sharedState._nextColumnDataToRead = this._sharedState._nextColumnHeaderToRead;
					this._sharedState._nextColumnHeaderToRead++;
					if (flag2 && sqlMetaData.type != SqlDbType.Timestamp)
					{
						this._parser.GetNullSqlValue(this._data[this._sharedState._nextColumnDataToRead], sqlMetaData);
						if (!readHeaderOnly)
						{
							this._sharedState._nextColumnDataToRead++;
						}
					}
					else if (i > this._sharedState._nextColumnDataToRead || !readHeaderOnly)
					{
						if (!this._parser.TryReadSqlValue(this._data[this._sharedState._nextColumnDataToRead], sqlMetaData, (int)num, this._stateObj))
						{
							return false;
						}
						this._sharedState._nextColumnDataToRead++;
					}
					else
					{
						this._sharedState._columnDataBytesRemaining = (long)num;
					}
				}
				if (this._snapshot != null)
				{
					this._snapshot = null;
					this.PrepareAsyncInvocation(true);
				}
				if (this._sharedState._nextColumnHeaderToRead > i)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00063BAC File Offset: 0x00061DAC
		private bool WillHaveEnoughData(int targetColumn, bool headerOnly = false)
		{
			if (this._lastColumnWithDataChunkRead == this._sharedState._nextColumnDataToRead && this._metaData[this._lastColumnWithDataChunkRead].metaType.IsPlp)
			{
				return false;
			}
			int num = Math.Min(checked(this._stateObj._inBytesRead - this._stateObj._inBytesUsed), this._stateObj._inBytesPacket);
			num--;
			if (targetColumn >= this._sharedState._nextColumnDataToRead && this._sharedState._nextColumnDataToRead < this._sharedState._nextColumnHeaderToRead)
			{
				if (this._sharedState._columnDataBytesRemaining > (long)num)
				{
					return false;
				}
				checked
				{
					num -= (int)this._sharedState._columnDataBytesRemaining;
				}
			}
			int num2 = this._sharedState._nextColumnHeaderToRead;
			while (num >= 0 && num2 <= targetColumn)
			{
				checked
				{
					if (!this._stateObj.IsNullCompressionBitSet(num2))
					{
						MetaType metaType = this._metaData[num2].metaType;
						if (metaType.IsLong || metaType.IsPlp || metaType.SqlDbType == SqlDbType.Udt || metaType.SqlDbType == SqlDbType.Structured)
						{
							return false;
						}
						byte b = this._metaData[num2].tdsType & 48;
						int num3;
						if (b == 32 || b == 0)
						{
							if ((this._metaData[num2].tdsType & 128) != 0)
							{
								num3 = 2;
							}
							else if ((this._metaData[num2].tdsType & 12) == 0)
							{
								num3 = 4;
							}
							else
							{
								num3 = 1;
							}
						}
						else
						{
							num3 = 0;
						}
						num -= num3;
						if (num2 < targetColumn || !headerOnly)
						{
							num -= this._metaData[num2].length;
						}
					}
				}
				num2++;
			}
			return num >= 0;
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x00063D4C File Offset: 0x00061F4C
		private bool TryResetBlobState()
		{
			if (this._sharedState._nextColumnDataToRead < this._sharedState._nextColumnHeaderToRead)
			{
				if (this._sharedState._nextColumnHeaderToRead > 0 && this._metaData[this._sharedState._nextColumnHeaderToRead - 1].metaType.IsPlp)
				{
					ulong num;
					if (this._stateObj._longlen != 0UL && !this._stateObj.Parser.TrySkipPlpValue(18446744073709551615UL, this._stateObj, out num))
					{
						return false;
					}
					if (this._streamingXml != null)
					{
						SqlStreamingXml streamingXml = this._streamingXml;
						this._streamingXml = null;
						streamingXml.Close();
					}
				}
				else if (0L < this._sharedState._columnDataBytesRemaining && !this._stateObj.TrySkipLongBytes(this._sharedState._columnDataBytesRemaining))
				{
					return false;
				}
			}
			this._sharedState._columnDataBytesRemaining = 0L;
			this._columnDataBytesRead = 0L;
			this._columnDataCharsRead = 0L;
			this._columnDataChars = null;
			this._columnDataCharsIndex = -1;
			this._stateObj._plpdecoder = null;
			return true;
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00063E4E File Offset: 0x0006204E
		private void CloseActiveSequentialStreamAndTextReader()
		{
			if (this._currentStream != null)
			{
				this._currentStream.SetClosed();
				this._currentStream = null;
			}
			if (this._currentTextReader != null)
			{
				this._currentTextReader.SetClosed();
				this._currentStream = null;
			}
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00063E84 File Offset: 0x00062084
		private void RestoreServerSettings(TdsParser parser, TdsParserStateObject stateObj)
		{
			if (parser != null && this._resetOptionsString != null)
			{
				if (parser.State == TdsParserState.OpenLoggedIn)
				{
					parser.TdsExecuteSQLBatch(this._resetOptionsString, (this._command != null) ? this._command.CommandTimeout : 0, null, stateObj, true, false);
					parser.Run(RunBehavior.UntilDone, this._command, this, null, stateObj);
				}
				this._resetOptionsString = null;
			}
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x00063EE4 File Offset: 0x000620E4
		internal bool TrySetAltMetaDataSet(_SqlMetaDataSet metaDataSet, bool metaDataConsumed)
		{
			if (this._altMetaDataSetCollection == null)
			{
				this._altMetaDataSetCollection = new _SqlMetaDataSetCollection();
			}
			else if (this._snapshot != null && this._snapshot._altMetaDataSetCollection == this._altMetaDataSetCollection)
			{
				this._altMetaDataSetCollection = (_SqlMetaDataSetCollection)this._altMetaDataSetCollection.Clone();
			}
			this._altMetaDataSetCollection.SetAltMetaData(metaDataSet);
			this._metaDataConsumed = metaDataConsumed;
			if (this._metaDataConsumed && this._parser != null)
			{
				byte b;
				if (!this._stateObj.TryPeekByte(out b))
				{
					return false;
				}
				if (169 == b)
				{
					bool flag;
					if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out flag))
					{
						return false;
					}
					if (!this._stateObj.TryPeekByte(out b))
					{
						return false;
					}
				}
				if (b == 171)
				{
					try
					{
						this._stateObj._accumulateInfoEvents = true;
						bool flag2;
						if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, null, null, this._stateObj, out flag2))
						{
							return false;
						}
					}
					finally
					{
						this._stateObj._accumulateInfoEvents = false;
					}
					if (!this._stateObj.TryPeekByte(out b))
					{
						return false;
					}
				}
				IL_010F:
				this._hasRows = this.IsRowToken(b);
			}
			if (metaDataSet != null && (this._data == null || this._data.Length < metaDataSet.Length))
			{
				this._data = SqlBuffer.CreateBufferArray(metaDataSet.Length);
			}
			return true;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0006404C File Offset: 0x0006224C
		private void ClearMetaData()
		{
			this._metaData = null;
			this._tableNames = null;
			this._fieldNameLookup = null;
			this._metaDataConsumed = false;
			this._browseModeInfoConsumed = false;
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x00064074 File Offset: 0x00062274
		internal bool TrySetMetaData(_SqlMetaDataSet metaData, bool moreInfo)
		{
			this._metaData = metaData;
			this._tableNames = null;
			if (this._metaData != null)
			{
				this._data = SqlBuffer.CreateBufferArray(metaData.Length);
			}
			this._fieldNameLookup = null;
			if (metaData != null)
			{
				if (!moreInfo)
				{
					this._metaDataConsumed = true;
					if (this._parser != null)
					{
						byte b;
						if (!this._stateObj.TryPeekByte(out b))
						{
							return false;
						}
						if (b == 169)
						{
							bool flag;
							if (!this._parser.TryRun(RunBehavior.ReturnImmediately, null, null, null, this._stateObj, out flag))
							{
								return false;
							}
							if (!this._stateObj.TryPeekByte(out b))
							{
								return false;
							}
						}
						if (b == 171)
						{
							try
							{
								this._stateObj._accumulateInfoEvents = true;
								bool flag2;
								if (!this._parser.TryRun(RunBehavior.ReturnImmediately, null, null, null, this._stateObj, out flag2))
								{
									return false;
								}
							}
							finally
							{
								this._stateObj._accumulateInfoEvents = false;
							}
							if (!this._stateObj.TryPeekByte(out b))
							{
								return false;
							}
						}
						IL_00E2:
						this._hasRows = this.IsRowToken(b);
						if (136 == b)
						{
							this._metaDataConsumed = false;
						}
					}
				}
			}
			else
			{
				this._metaDataConsumed = false;
			}
			this._browseModeInfoConsumed = false;
			return true;
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x000641A4 File Offset: 0x000623A4
		private void SetTimeout(long timeoutMilliseconds)
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.SetTimeoutMilliseconds(timeoutMilliseconds);
			}
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x000641C2 File Offset: 0x000623C2
		private bool HasActiveStreamOrTextReaderOnColumn(int columnIndex)
		{
			return false | (this._currentStream != null && this._currentStream.ColumnIndex == columnIndex) | (this._currentTextReader != null && this._currentTextReader.ColumnIndex == columnIndex);
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x000641F9 File Offset: 0x000623F9
		private void CheckMetaDataIsReady()
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (this.MetaData == null)
			{
				throw SQL.InvalidRead();
			}
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x00064217 File Offset: 0x00062417
		private void CheckMetaDataIsReady(int columnIndex, bool permitAsync = false)
		{
			if (!permitAsync && this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (this.MetaData == null)
			{
				throw SQL.InvalidRead();
			}
			if (columnIndex < 0 || columnIndex >= this._metaData.Length)
			{
				throw ADP.IndexOutOfRange();
			}
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x00064250 File Offset: 0x00062450
		private void CheckDataIsReady()
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this._sharedState._dataReady || this._metaData == null)
			{
				throw SQL.InvalidRead();
			}
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0006427C File Offset: 0x0006247C
		private void CheckHeaderIsReady(int columnIndex, bool permitAsync = false, [CallerMemberName] string methodName = null)
		{
			if (this._isClosed)
			{
				throw ADP.DataReaderClosed(methodName ?? "CheckHeaderIsReady");
			}
			if (!permitAsync && this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this._sharedState._dataReady || this._metaData == null)
			{
				throw SQL.InvalidRead();
			}
			if (columnIndex < 0 || columnIndex >= this._metaData.Length)
			{
				throw ADP.IndexOutOfRange();
			}
			if (this.IsCommandBehavior(CommandBehavior.SequentialAccess) && (this._sharedState._nextColumnHeaderToRead > columnIndex + 1 || this._lastColumnWithDataChunkRead > columnIndex))
			{
				throw ADP.NonSequentialColumnAccess(columnIndex, Math.Max(this._sharedState._nextColumnHeaderToRead - 1, this._lastColumnWithDataChunkRead));
			}
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00064328 File Offset: 0x00062528
		private void CheckDataIsReady(int columnIndex, bool allowPartiallyReadColumn = false, bool permitAsync = false, [CallerMemberName] string methodName = null)
		{
			if (this._isClosed)
			{
				throw ADP.DataReaderClosed(methodName ?? "CheckDataIsReady");
			}
			if (!permitAsync && this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this._sharedState._dataReady || this._metaData == null)
			{
				throw SQL.InvalidRead();
			}
			if (columnIndex < 0 || columnIndex >= this._metaData.Length)
			{
				throw ADP.IndexOutOfRange();
			}
			if (this.IsCommandBehavior(CommandBehavior.SequentialAccess) && (this._sharedState._nextColumnDataToRead > columnIndex || this._lastColumnWithDataChunkRead > columnIndex || (!allowPartiallyReadColumn && this._lastColumnWithDataChunkRead == columnIndex) || (allowPartiallyReadColumn && this.HasActiveStreamOrTextReaderOnColumn(columnIndex))))
			{
				throw ADP.NonSequentialColumnAccess(columnIndex, Math.Max(this._sharedState._nextColumnDataToRead, this._lastColumnWithDataChunkRead + 1));
			}
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x000643EA File Offset: 0x000625EA
		[Conditional("DEBUG")]
		private void AssertReaderState(bool requireData, bool permitAsync, int? columnIndex = null, bool enforceSequentialAccess = false)
		{
			bool flag = columnIndex != null;
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlDataReader.NextResult" />, which advances the data reader to the next result, when reading the results of batch Transact-SQL statements.The cancellation token can be used to request that the operation be abandoned before the command timeout elapses.  Exceptions will be reported via the returned Task object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlDataReader.NextResultAsync(System.Threading.CancellationToken)" /> more than once for the same instance before task completion.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">SQL Server returned an error while executing the command text.</exception>
		// Token: 0x0600141B RID: 5147 RVA: 0x000643F4 File Offset: 0x000625F4
		public override Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
			if (this.IsClosed)
			{
				taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("NextResultAsync")));
				return taskCompletionSource.Task;
			}
			IDisposable disposable = null;
			if (cancellationToken.CanBeCanceled)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					taskCompletionSource.SetCanceled();
					return taskCompletionSource.Task;
				}
				disposable = cancellationToken.Register(delegate(object s)
				{
					((SqlCommand)s).CancelIgnoreFailure();
				}, this._command);
			}
			if (Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null) != null)
			{
				taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(SQL.PendingBeginXXXExists()));
				return taskCompletionSource.Task;
			}
			if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
			{
				taskCompletionSource.SetCanceled();
				this._currentTask = null;
				return taskCompletionSource.Task;
			}
			this.PrepareAsyncInvocation(true);
			Func<Task, Task<bool>> moreFunc = null;
			moreFunc = delegate(Task t)
			{
				if (t != null)
				{
					this.PrepareForAsyncContinuation();
				}
				bool flag;
				if (!this.TryNextResult(out flag))
				{
					return this.ContinueRetryable<bool>(moreFunc);
				}
				if (!flag)
				{
					return ADP.FalseTask;
				}
				return ADP.TrueTask;
			};
			return this.InvokeRetryable<bool>(moreFunc, taskCompletionSource, disposable);
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00064508 File Offset: 0x00062708
		internal Task<int> GetBytesAsync(int i, byte[] buffer, int index, int length, int timeout, CancellationToken cancellationToken, out int bytesRead)
		{
			SqlDataReader.<>c__DisplayClass188_0 CS$<>8__locals1 = new SqlDataReader.<>c__DisplayClass188_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.i = i;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			CS$<>8__locals1.buffer = buffer;
			CS$<>8__locals1.index = index;
			CS$<>8__locals1.length = length;
			CS$<>8__locals1.timeout = timeout;
			bytesRead = 0;
			if (this.IsClosed)
			{
				return Task.FromException<int>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("GetBytesAsync")));
			}
			if (this._currentTask != null)
			{
				return Task.FromException<int>(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
			}
			if (CS$<>8__locals1.cancellationToken.CanBeCanceled && CS$<>8__locals1.cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			if (this._sharedState._nextColumnHeaderToRead > this._lastColumnWithDataChunkRead && this._sharedState._nextColumnDataToRead >= this._lastColumnWithDataChunkRead)
			{
				this.PrepareAsyncInvocation(false);
				Task<int> bytesAsyncReadDataStage;
				try
				{
					bytesAsyncReadDataStage = this.GetBytesAsyncReadDataStage(CS$<>8__locals1.i, CS$<>8__locals1.buffer, CS$<>8__locals1.index, CS$<>8__locals1.length, CS$<>8__locals1.timeout, false, CS$<>8__locals1.cancellationToken, CancellationToken.None, out bytesRead);
				}
				catch
				{
					this.CleanupAfterAsyncInvocation(false);
					throw;
				}
				return bytesAsyncReadDataStage;
			}
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
			if (Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null) != null)
			{
				taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
				return taskCompletionSource.Task;
			}
			this.PrepareAsyncInvocation(true);
			Func<Task, Task<int>> moreFunc = null;
			CancellationToken timeoutToken = CancellationToken.None;
			CancellationTokenSource cancellationTokenSource = null;
			if (CS$<>8__locals1.timeout > 0)
			{
				cancellationTokenSource = new CancellationTokenSource();
				cancellationTokenSource.CancelAfter(CS$<>8__locals1.timeout);
				timeoutToken = cancellationTokenSource.Token;
			}
			moreFunc = delegate(Task t)
			{
				if (t != null)
				{
					CS$<>8__locals1.<>4__this.PrepareForAsyncContinuation();
				}
				CS$<>8__locals1.<>4__this.SetTimeout(CS$<>8__locals1.<>4__this._defaultTimeoutMilliseconds);
				if (!CS$<>8__locals1.<>4__this.TryReadColumnHeader(CS$<>8__locals1.i))
				{
					return CS$<>8__locals1.<>4__this.ContinueRetryable<int>(moreFunc);
				}
				if (CS$<>8__locals1.cancellationToken.IsCancellationRequested)
				{
					return Task.FromCanceled<int>(CS$<>8__locals1.cancellationToken);
				}
				if (timeoutToken.IsCancellationRequested)
				{
					return Task.FromException<int>(ADP.ExceptionWithStackTrace(ADP.IO(SQLMessage.Timeout())));
				}
				CS$<>8__locals1.<>4__this.SwitchToAsyncWithoutSnapshot();
				int num;
				Task<int> bytesAsyncReadDataStage2 = CS$<>8__locals1.<>4__this.GetBytesAsyncReadDataStage(CS$<>8__locals1.i, CS$<>8__locals1.buffer, CS$<>8__locals1.index, CS$<>8__locals1.length, CS$<>8__locals1.timeout, true, CS$<>8__locals1.cancellationToken, timeoutToken, out num);
				if (bytesAsyncReadDataStage2 == null)
				{
					return Task.FromResult<int>(num);
				}
				return bytesAsyncReadDataStage2;
			};
			return this.InvokeRetryable<int>(moreFunc, taskCompletionSource, cancellationTokenSource);
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x000646D4 File Offset: 0x000628D4
		private Task<int> GetBytesAsyncReadDataStage(int i, byte[] buffer, int index, int length, int timeout, bool isContinuation, CancellationToken cancellationToken, CancellationToken timeoutToken, out int bytesRead)
		{
			SqlDataReader.<>c__DisplayClass189_0 CS$<>8__locals1 = new SqlDataReader.<>c__DisplayClass189_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			CS$<>8__locals1.timeoutToken = timeoutToken;
			CS$<>8__locals1.i = i;
			CS$<>8__locals1.buffer = buffer;
			CS$<>8__locals1.index = index;
			CS$<>8__locals1.length = length;
			this._lastColumnWithDataChunkRead = CS$<>8__locals1.i;
			CS$<>8__locals1.source = null;
			CS$<>8__locals1.timeoutCancellationSource = null;
			this.SetTimeout(this._defaultTimeoutMilliseconds);
			if (this.TryGetBytesInternalSequential(CS$<>8__locals1.i, CS$<>8__locals1.buffer, CS$<>8__locals1.index, CS$<>8__locals1.length, out bytesRead))
			{
				if (!isContinuation)
				{
					this.CleanupAfterAsyncInvocation(false);
				}
				return null;
			}
			int totalBytesRead = bytesRead;
			if (!isContinuation)
			{
				CS$<>8__locals1.source = new TaskCompletionSource<int>();
				if (Interlocked.CompareExchange<Task>(ref this._currentTask, CS$<>8__locals1.source.Task, null) != null)
				{
					CS$<>8__locals1.source.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
					return CS$<>8__locals1.source.Task;
				}
				if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
				{
					CS$<>8__locals1.source.SetCanceled();
					this._currentTask = null;
					return CS$<>8__locals1.source.Task;
				}
				if (timeout > 0)
				{
					CS$<>8__locals1.timeoutCancellationSource = new CancellationTokenSource();
					CS$<>8__locals1.timeoutCancellationSource.CancelAfter(timeout);
					CS$<>8__locals1.timeoutToken = CS$<>8__locals1.timeoutCancellationSource.Token;
				}
			}
			Func<Task, Task<int>> moreFunc = null;
			moreFunc = delegate(Task _)
			{
				CS$<>8__locals1.<>4__this.PrepareForAsyncContinuation();
				if (CS$<>8__locals1.cancellationToken.IsCancellationRequested)
				{
					return Task.FromCanceled<int>(CS$<>8__locals1.cancellationToken);
				}
				if (CS$<>8__locals1.timeoutToken.IsCancellationRequested)
				{
					return Task.FromException<int>(ADP.ExceptionWithStackTrace(ADP.IO(SQLMessage.Timeout())));
				}
				CS$<>8__locals1.<>4__this.SetTimeout(CS$<>8__locals1.<>4__this._defaultTimeoutMilliseconds);
				int num;
				bool flag = CS$<>8__locals1.<>4__this.TryGetBytesInternalSequential(CS$<>8__locals1.i, CS$<>8__locals1.buffer, CS$<>8__locals1.index + totalBytesRead, CS$<>8__locals1.length - totalBytesRead, out num);
				totalBytesRead += num;
				if (flag)
				{
					return Task.FromResult<int>(totalBytesRead);
				}
				return CS$<>8__locals1.<>4__this.ContinueRetryable<int>(moreFunc);
			};
			Task<int> task = this.ContinueRetryable<int>(moreFunc);
			if (isContinuation)
			{
				return task;
			}
			task.ContinueWith(delegate(Task<int> t)
			{
				CS$<>8__locals1.<>4__this.CompleteRetryable<int>(t, CS$<>8__locals1.source, CS$<>8__locals1.timeoutCancellationSource);
			}, TaskScheduler.Default);
			return CS$<>8__locals1.source.Task;
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlDataReader.Read" />, which advances the <see cref="T:System.Data.SqlClient.SqlDataReader" /> to the next record.The cancellation token can be used to request that the operation be abandoned before the command timeout elapses. Exceptions will be reported via the returned Task object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlDataReader.ReadAsync(System.Threading.CancellationToken)" /> more than once for the same instance before task completion.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">SQL Server returned an error while executing the command text.</exception>
		// Token: 0x0600141E RID: 5150 RVA: 0x000648BC File Offset: 0x00062ABC
		public override Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			if (this.IsClosed)
			{
				return Task.FromException<bool>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("ReadAsync")));
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<bool>(cancellationToken);
			}
			if (this._currentTask != null)
			{
				return Task.FromException<bool>(ADP.ExceptionWithStackTrace(SQL.PendingBeginXXXExists()));
			}
			bool rowTokenRead = false;
			bool more = false;
			try
			{
				if (!this._haltRead && (!this._sharedState._dataReady || this.WillHaveEnoughData(this._metaData.Length - 1, false)))
				{
					if (this._sharedState._dataReady)
					{
						this.CleanPartialReadReliable();
					}
					if (this._stateObj.IsRowTokenReady())
					{
						this.TryReadInternal(true, out more);
						rowTokenRead = true;
						if (!more)
						{
							return ADP.FalseTask;
						}
						if (this.IsCommandBehavior(CommandBehavior.SequentialAccess))
						{
							return ADP.TrueTask;
						}
						if (this.WillHaveEnoughData(this._metaData.Length - 1, false))
						{
							this.TryReadColumn(this._metaData.Length - 1, true, false);
							return ADP.TrueTask;
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				return Task.FromException<bool>(ex);
			}
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
			if (Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null) != null)
			{
				taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(SQL.PendingBeginXXXExists()));
				return taskCompletionSource.Task;
			}
			if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
			{
				taskCompletionSource.SetCanceled();
				this._currentTask = null;
				return taskCompletionSource.Task;
			}
			IDisposable disposable = null;
			if (cancellationToken.CanBeCanceled)
			{
				disposable = cancellationToken.Register(delegate(object s)
				{
					((SqlCommand)s).CancelIgnoreFailure();
				}, this._command);
			}
			this.PrepareAsyncInvocation(true);
			Func<Task, Task<bool>> moreFunc = null;
			moreFunc = delegate(Task t)
			{
				if (t != null)
				{
					this.PrepareForAsyncContinuation();
				}
				if (rowTokenRead || this.TryReadInternal(true, out more))
				{
					if (!more || (this._commandBehavior & CommandBehavior.SequentialAccess) == CommandBehavior.SequentialAccess)
					{
						if (!more)
						{
							return ADP.FalseTask;
						}
						return ADP.TrueTask;
					}
					else
					{
						if (!rowTokenRead)
						{
							rowTokenRead = true;
							this._snapshot = null;
							this.PrepareAsyncInvocation(true);
						}
						if (this.TryReadColumn(this._metaData.Length - 1, true, false))
						{
							return ADP.TrueTask;
						}
					}
				}
				return this.ContinueRetryable<bool>(moreFunc);
			};
			return this.InvokeRetryable<bool>(moreFunc, taskCompletionSource, disposable);
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlDataReader.IsDBNull(System.Int32)" />, which gets a value that indicates whether the column contains non-existent or missing values.The cancellation token can be used to request that the operation be abandoned before the command timeout elapses. Exceptions will be reported via the returned Task object.</summary>
		/// <returns>true if the specified column value is equivalent to DBNull otherwise false.</returns>
		/// <param name="i">The zero-based column to be retrieved.</param>
		/// <param name="cancellationToken">The cancellation instruction, which propagates a notification that operations should be canceled. This does not guarantee the cancellation. A setting of CancellationToken.None makes this method equivalent to <see cref="M:System.Data.SqlClient.SqlDataReader.IsDBNull(System.Int32)" />. The returned task must be marked as cancelled.</param>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).Trying to read a previously read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		// Token: 0x0600141F RID: 5151 RVA: 0x00064AD4 File Offset: 0x00062CD4
		public override Task<bool> IsDBNullAsync(int i, CancellationToken cancellationToken)
		{
			try
			{
				this.CheckHeaderIsReady(i, false, "IsDBNullAsync");
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				return Task.FromException<bool>(ex);
			}
			if (this._sharedState._nextColumnHeaderToRead > i && !cancellationToken.IsCancellationRequested && this._currentTask == null)
			{
				SqlBuffer[] data = this._data;
				if (data == null)
				{
					return Task.FromException<bool>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("IsDBNullAsync")));
				}
				if (!data[i].IsNull)
				{
					return ADP.FalseTask;
				}
				return ADP.TrueTask;
			}
			else
			{
				if (this._currentTask != null)
				{
					return Task.FromException<bool>(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
				}
				if (cancellationToken.IsCancellationRequested)
				{
					return Task.FromCanceled<bool>(cancellationToken);
				}
				try
				{
					if (this.WillHaveEnoughData(i, true))
					{
						this.ReadColumnHeader(i);
						return this._data[i].IsNull ? ADP.TrueTask : ADP.FalseTask;
					}
				}
				catch (Exception ex2)
				{
					if (!ADP.IsCatchableExceptionType(ex2))
					{
						throw;
					}
					return Task.FromException<bool>(ex2);
				}
				TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
				if (Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null) != null)
				{
					taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
					return taskCompletionSource.Task;
				}
				if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
				{
					taskCompletionSource.SetCanceled();
					this._currentTask = null;
					return taskCompletionSource.Task;
				}
				IDisposable disposable = null;
				if (cancellationToken.CanBeCanceled)
				{
					disposable = cancellationToken.Register(delegate(object s)
					{
						((SqlCommand)s).CancelIgnoreFailure();
					}, this._command);
				}
				this.PrepareAsyncInvocation(true);
				Func<Task, Task<bool>> moreFunc = null;
				moreFunc = delegate(Task t)
				{
					if (t != null)
					{
						this.PrepareForAsyncContinuation();
					}
					if (!this.TryReadColumnHeader(i))
					{
						return this.ContinueRetryable<bool>(moreFunc);
					}
					if (!this._data[i].IsNull)
					{
						return ADP.FalseTask;
					}
					return ADP.TrueTask;
				};
				return this.InvokeRetryable<bool>(moreFunc, taskCompletionSource, disposable);
			}
			Task<bool> task;
			return task;
		}

		/// <summary>Asynchronously gets the value of the specified column as a type. <see cref="M:System.Data.SqlClient.SqlDataReader.GetFieldValue``1(System.Int32)" /> is the synchronous version of this method.</summary>
		/// <returns>The returned type object.</returns>
		/// <param name="i">The column to be retrieved.</param>
		/// <param name="cancellationToken">The cancellation instruction, which propagates a notification that operations should be canceled. This does not guarantee the cancellation. A setting of CancellationToken.None makes this method equivalent to <see cref="M:System.Data.SqlClient.SqlDataReader.IsDBNull(System.Int32)" />. The returned task must be marked as cancelled.</param>
		/// <typeparam name="T">The type of the value to be returned. See the remarks section for more information.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).Tried to read a previously-read column in sequential mode.There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The value of the column was null (<see cref="M:System.Data.SqlClient.SqlDataReader.IsDBNull(System.Int32)" /> == true), retrieving a non-SQL type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="T" /> doesn’t match the type returned by SQL Server or cannot be cast.</exception>
		// Token: 0x06001420 RID: 5152 RVA: 0x00064D00 File Offset: 0x00062F00
		public override Task<T> GetFieldValueAsync<T>(int i, CancellationToken cancellationToken)
		{
			try
			{
				this.CheckDataIsReady(i, false, false, "GetFieldValueAsync");
				if (!this.IsCommandBehavior(CommandBehavior.SequentialAccess) && this._sharedState._nextColumnDataToRead > i && !cancellationToken.IsCancellationRequested && this._currentTask == null)
				{
					SqlBuffer[] data = this._data;
					_SqlMetaDataSet metaData = this._metaData;
					if (data != null && metaData != null)
					{
						return Task.FromResult<T>(this.GetFieldValueFromSqlBufferInternal<T>(data[i], metaData[i]));
					}
					return Task.FromException<T>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("GetFieldValueAsync")));
				}
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				return Task.FromException<T>(ex);
			}
			if (this._currentTask != null)
			{
				return Task.FromException<T>(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<T>(cancellationToken);
			}
			try
			{
				if (this.WillHaveEnoughData(i, false))
				{
					return Task.FromResult<T>(this.GetFieldValueInternal<T>(i));
				}
			}
			catch (Exception ex2)
			{
				if (!ADP.IsCatchableExceptionType(ex2))
				{
					throw;
				}
				return Task.FromException<T>(ex2);
			}
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
			if (Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null) != null)
			{
				taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
				return taskCompletionSource.Task;
			}
			if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
			{
				taskCompletionSource.SetCanceled();
				this._currentTask = null;
				return taskCompletionSource.Task;
			}
			IDisposable disposable = null;
			if (cancellationToken.CanBeCanceled)
			{
				disposable = cancellationToken.Register(delegate(object s)
				{
					((SqlCommand)s).CancelIgnoreFailure();
				}, this._command);
			}
			this.PrepareAsyncInvocation(true);
			Func<Task, Task<T>> moreFunc = null;
			moreFunc = delegate(Task t)
			{
				if (t != null)
				{
					this.PrepareForAsyncContinuation();
				}
				if (this.TryReadColumn(i, false, false))
				{
					return Task.FromResult<T>(this.GetFieldValueFromSqlBufferInternal<T>(this._data[i], this._metaData[i]));
				}
				return this.ContinueRetryable<T>(moreFunc);
			};
			return this.InvokeRetryable<T>(moreFunc, taskCompletionSource, disposable);
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x00064F18 File Offset: 0x00063118
		private Task<T> ContinueRetryable<T>(Func<Task, Task<T>> moreFunc)
		{
			TaskCompletionSource<object> networkPacketTaskSource = this._stateObj._networkPacketTaskSource;
			if (this._cancelAsyncOnCloseToken.IsCancellationRequested || networkPacketTaskSource == null)
			{
				return Task.FromException<T>(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
			}
			return networkPacketTaskSource.Task.ContinueWith<Task<T>>(delegate(Task<object> retryTask)
			{
				if (retryTask.IsFaulted)
				{
					return Task.FromException<T>(retryTask.Exception.InnerException);
				}
				if (!this._cancelAsyncOnCloseToken.IsCancellationRequested)
				{
					TdsParserStateObject stateObj = this._stateObj;
					if (stateObj != null)
					{
						TdsParserStateObject tdsParserStateObject = stateObj;
						lock (tdsParserStateObject)
						{
							if (this._stateObj != null)
							{
								if (retryTask.IsCanceled)
								{
									if (this._parser != null)
									{
										this._parser.State = TdsParserState.Broken;
										this._parser.Connection.BreakConnection();
										this._parser.ThrowExceptionAndWarning(this._stateObj, false, false);
									}
								}
								else if (!this.IsClosed)
								{
									try
									{
										return moreFunc(retryTask);
									}
									catch (Exception)
									{
										this.CleanupAfterAsyncInvocation(false);
										throw;
									}
								}
							}
						}
					}
				}
				return Task.FromException<T>(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
			}, TaskScheduler.Default).Unwrap<T>();
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00064F88 File Offset: 0x00063188
		private Task<T> InvokeRetryable<T>(Func<Task, Task<T>> moreFunc, TaskCompletionSource<T> source, IDisposable objectToDispose = null)
		{
			try
			{
				Task<T> task;
				try
				{
					task = moreFunc(null);
				}
				catch (Exception ex)
				{
					task = Task.FromException<T>(ex);
				}
				if (task.IsCompleted)
				{
					this.CompleteRetryable<T>(task, source, objectToDispose);
				}
				else
				{
					task.ContinueWith(delegate(Task<T> t)
					{
						this.CompleteRetryable<T>(t, source, objectToDispose);
					}, TaskScheduler.Default);
				}
			}
			catch (AggregateException ex2)
			{
				source.TrySetException(ex2.InnerException);
			}
			catch (Exception ex3)
			{
				source.TrySetException(ex3);
			}
			return source.Task;
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00065054 File Offset: 0x00063254
		private void CompleteRetryable<T>(Task<T> task, TaskCompletionSource<T> source, IDisposable objectToDispose)
		{
			if (objectToDispose != null)
			{
				objectToDispose.Dispose();
			}
			TdsParserStateObject stateObj = this._stateObj;
			bool flag = stateObj != null && stateObj._syncOverAsync;
			this.CleanupAfterAsyncInvocation(flag);
			Interlocked.CompareExchange<Task>(ref this._currentTask, null, source.Task);
			if (task.IsFaulted)
			{
				Exception innerException = task.Exception.InnerException;
				source.TrySetException(innerException);
				return;
			}
			if (task.IsCanceled)
			{
				source.TrySetCanceled();
				return;
			}
			source.TrySetResult(task.Result);
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x000650D4 File Offset: 0x000632D4
		private void PrepareAsyncInvocation(bool useSnapshot)
		{
			if (useSnapshot)
			{
				if (this._snapshot == null)
				{
					this._snapshot = new SqlDataReader.Snapshot
					{
						_dataReady = this._sharedState._dataReady,
						_haltRead = this._haltRead,
						_metaDataConsumed = this._metaDataConsumed,
						_browseModeInfoConsumed = this._browseModeInfoConsumed,
						_hasRows = this._hasRows,
						_altRowStatus = this._altRowStatus,
						_nextColumnDataToRead = this._sharedState._nextColumnDataToRead,
						_nextColumnHeaderToRead = this._sharedState._nextColumnHeaderToRead,
						_columnDataBytesRead = this._columnDataBytesRead,
						_columnDataBytesRemaining = this._sharedState._columnDataBytesRemaining,
						_metadata = this._metaData,
						_altMetaDataSetCollection = this._altMetaDataSetCollection,
						_tableNames = this._tableNames,
						_currentStream = this._currentStream,
						_currentTextReader = this._currentTextReader
					};
					this._stateObj.SetSnapshot();
				}
			}
			else
			{
				this._stateObj._asyncReadWithoutSnapshot = true;
			}
			this._stateObj._syncOverAsync = false;
			this._stateObj._executionContext = ExecutionContext.Capture();
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x000651FC File Offset: 0x000633FC
		private void CleanupAfterAsyncInvocation(bool ignoreCloseToken = false)
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null && (ignoreCloseToken || !this._cancelAsyncOnCloseToken.IsCancellationRequested || stateObj._asyncReadWithoutSnapshot))
			{
				TdsParserStateObject tdsParserStateObject = stateObj;
				lock (tdsParserStateObject)
				{
					if (this._stateObj != null)
					{
						this.CleanupAfterAsyncInvocationInternal(this._stateObj, true);
					}
				}
			}
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x00065268 File Offset: 0x00063468
		private void CleanupAfterAsyncInvocationInternal(TdsParserStateObject stateObj, bool resetNetworkPacketTaskSource = true)
		{
			if (resetNetworkPacketTaskSource)
			{
				stateObj._networkPacketTaskSource = null;
			}
			stateObj.ResetSnapshot();
			stateObj._syncOverAsync = true;
			stateObj._executionContext = null;
			stateObj._asyncReadWithoutSnapshot = false;
			this._snapshot = null;
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00065298 File Offset: 0x00063498
		private void PrepareForAsyncContinuation()
		{
			if (this._snapshot != null)
			{
				this._sharedState._dataReady = this._snapshot._dataReady;
				this._haltRead = this._snapshot._haltRead;
				this._metaDataConsumed = this._snapshot._metaDataConsumed;
				this._browseModeInfoConsumed = this._snapshot._browseModeInfoConsumed;
				this._hasRows = this._snapshot._hasRows;
				this._altRowStatus = this._snapshot._altRowStatus;
				this._sharedState._nextColumnDataToRead = this._snapshot._nextColumnDataToRead;
				this._sharedState._nextColumnHeaderToRead = this._snapshot._nextColumnHeaderToRead;
				this._columnDataBytesRead = this._snapshot._columnDataBytesRead;
				this._sharedState._columnDataBytesRemaining = this._snapshot._columnDataBytesRemaining;
				this._metaData = this._snapshot._metadata;
				this._altMetaDataSetCollection = this._snapshot._altMetaDataSetCollection;
				this._tableNames = this._snapshot._tableNames;
				this._currentStream = this._snapshot._currentStream;
				this._currentTextReader = this._snapshot._currentTextReader;
				this._stateObj.PrepareReplaySnapshot();
			}
			this._stateObj._executionContext = ExecutionContext.Capture();
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x000653DE File Offset: 0x000635DE
		private void SwitchToAsyncWithoutSnapshot()
		{
			this._snapshot = null;
			this._stateObj.ResetSnapshot();
			this._stateObj._asyncReadWithoutSnapshot = true;
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x00065400 File Offset: 0x00063600
		private Exception UdtNotSupportedException()
		{
			return SQL.UnsupportedFeatureAndToken(this._parser.Connection, SqlDbType.Udt.ToString());
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00065430 File Offset: 0x00063630
		public ReadOnlyCollection<DbColumn> GetColumnSchema()
		{
			SqlStatistics sqlStatistics = null;
			ReadOnlyCollection<DbColumn> dbColumnSchema;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				if ((this._metaData == null || this._metaData.dbColumnSchema == null) && this.MetaData != null)
				{
					this._metaData.dbColumnSchema = this.BuildColumnSchema();
				}
				if (this._metaData != null)
				{
					dbColumnSchema = this._metaData.dbColumnSchema;
				}
				else
				{
					dbColumnSchema = SqlDataReader.s_emptySchema;
				}
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return dbColumnSchema;
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x000654B0 File Offset: 0x000636B0
		private ReadOnlyCollection<DbColumn> BuildColumnSchema()
		{
			_SqlMetaDataSet metaData = this.MetaData;
			DbColumn[] array = new DbColumn[metaData.Length];
			for (int i = 0; i < metaData.Length; i++)
			{
				_SqlMetaData sqlMetaData = metaData[i];
				SqlDbColumn sqlDbColumn = new SqlDbColumn(metaData[i]);
				if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.IsNewKatmaiDateTimeType)
				{
					sqlDbColumn.SqlNumericScale = new int?((int)MetaType.MetaNVarChar.Scale);
				}
				else if (255 != sqlMetaData.scale)
				{
					sqlDbColumn.SqlNumericScale = new int?((int)sqlMetaData.scale);
				}
				else
				{
					sqlDbColumn.SqlNumericScale = new int?((int)sqlMetaData.metaType.Scale);
				}
				if (this._browseModeInfoConsumed)
				{
					sqlDbColumn.SqlIsAliased = new bool?(sqlMetaData.isDifferentName);
					sqlDbColumn.SqlIsKey = new bool?(sqlMetaData.isKey);
					sqlDbColumn.SqlIsHidden = new bool?(sqlMetaData.isHidden);
					sqlDbColumn.SqlIsExpression = new bool?(sqlMetaData.isExpression);
				}
				sqlDbColumn.SqlDataType = this.GetFieldTypeInternal(sqlMetaData);
				sqlDbColumn.SqlDataTypeName = this.GetDataTypeNameInternal(sqlMetaData);
				array[i] = sqlDbColumn;
			}
			return new ReadOnlyCollection<DbColumn>(array);
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x00010468 File Offset: 0x0000E668
		internal SqlDataReader()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000D1F RID: 3359
		internal SqlDataReader.SharedState _sharedState;

		// Token: 0x04000D20 RID: 3360
		private TdsParser _parser;

		// Token: 0x04000D21 RID: 3361
		private TdsParserStateObject _stateObj;

		// Token: 0x04000D22 RID: 3362
		private SqlCommand _command;

		// Token: 0x04000D23 RID: 3363
		private SqlConnection _connection;

		// Token: 0x04000D24 RID: 3364
		private int _defaultLCID;

		// Token: 0x04000D25 RID: 3365
		private bool _haltRead;

		// Token: 0x04000D26 RID: 3366
		private bool _metaDataConsumed;

		// Token: 0x04000D27 RID: 3367
		private bool _browseModeInfoConsumed;

		// Token: 0x04000D28 RID: 3368
		private bool _isClosed;

		// Token: 0x04000D29 RID: 3369
		private bool _isInitialized;

		// Token: 0x04000D2A RID: 3370
		private bool _hasRows;

		// Token: 0x04000D2B RID: 3371
		private SqlDataReader.ALTROWSTATUS _altRowStatus;

		// Token: 0x04000D2C RID: 3372
		private int _recordsAffected;

		// Token: 0x04000D2D RID: 3373
		private long _defaultTimeoutMilliseconds;

		// Token: 0x04000D2E RID: 3374
		private SqlConnectionString.TypeSystem _typeSystem;

		// Token: 0x04000D2F RID: 3375
		private SqlStatistics _statistics;

		// Token: 0x04000D30 RID: 3376
		private SqlBuffer[] _data;

		// Token: 0x04000D31 RID: 3377
		private SqlStreamingXml _streamingXml;

		// Token: 0x04000D32 RID: 3378
		private _SqlMetaDataSet _metaData;

		// Token: 0x04000D33 RID: 3379
		private _SqlMetaDataSetCollection _altMetaDataSetCollection;

		// Token: 0x04000D34 RID: 3380
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04000D35 RID: 3381
		private CommandBehavior _commandBehavior;

		// Token: 0x04000D36 RID: 3382
		private static int s_objectTypeCount;

		// Token: 0x04000D37 RID: 3383
		private static readonly ReadOnlyCollection<DbColumn> s_emptySchema = new ReadOnlyCollection<DbColumn>(Array.Empty<DbColumn>());

		// Token: 0x04000D38 RID: 3384
		internal readonly int ObjectID;

		// Token: 0x04000D39 RID: 3385
		private MultiPartTableName[] _tableNames;

		// Token: 0x04000D3A RID: 3386
		private string _resetOptionsString;

		// Token: 0x04000D3B RID: 3387
		private int _lastColumnWithDataChunkRead;

		// Token: 0x04000D3C RID: 3388
		private long _columnDataBytesRead;

		// Token: 0x04000D3D RID: 3389
		private long _columnDataCharsRead;

		// Token: 0x04000D3E RID: 3390
		private char[] _columnDataChars;

		// Token: 0x04000D3F RID: 3391
		private int _columnDataCharsIndex;

		// Token: 0x04000D40 RID: 3392
		private Task _currentTask;

		// Token: 0x04000D41 RID: 3393
		private SqlDataReader.Snapshot _snapshot;

		// Token: 0x04000D42 RID: 3394
		private CancellationTokenSource _cancelAsyncOnCloseTokenSource;

		// Token: 0x04000D43 RID: 3395
		private CancellationToken _cancelAsyncOnCloseToken;

		// Token: 0x04000D44 RID: 3396
		internal static readonly Type _typeofINullable = typeof(INullable);

		// Token: 0x04000D45 RID: 3397
		private static readonly Type s_typeofSqlString = typeof(SqlString);

		// Token: 0x04000D46 RID: 3398
		private SqlSequentialStream _currentStream;

		// Token: 0x04000D47 RID: 3399
		private SqlSequentialTextReader _currentTextReader;

		// Token: 0x020001A3 RID: 419
		private enum ALTROWSTATUS
		{
			// Token: 0x04000D49 RID: 3401
			Null,
			// Token: 0x04000D4A RID: 3402
			AltRow,
			// Token: 0x04000D4B RID: 3403
			Done
		}

		// Token: 0x020001A4 RID: 420
		internal class SharedState
		{
			// Token: 0x04000D4C RID: 3404
			internal int _nextColumnHeaderToRead;

			// Token: 0x04000D4D RID: 3405
			internal int _nextColumnDataToRead;

			// Token: 0x04000D4E RID: 3406
			internal long _columnDataBytesRemaining;

			// Token: 0x04000D4F RID: 3407
			internal bool _dataReady;
		}

		// Token: 0x020001A5 RID: 421
		private class Snapshot
		{
			// Token: 0x04000D50 RID: 3408
			public bool _dataReady;

			// Token: 0x04000D51 RID: 3409
			public bool _haltRead;

			// Token: 0x04000D52 RID: 3410
			public bool _metaDataConsumed;

			// Token: 0x04000D53 RID: 3411
			public bool _browseModeInfoConsumed;

			// Token: 0x04000D54 RID: 3412
			public bool _hasRows;

			// Token: 0x04000D55 RID: 3413
			public SqlDataReader.ALTROWSTATUS _altRowStatus;

			// Token: 0x04000D56 RID: 3414
			public int _nextColumnDataToRead;

			// Token: 0x04000D57 RID: 3415
			public int _nextColumnHeaderToRead;

			// Token: 0x04000D58 RID: 3416
			public long _columnDataBytesRead;

			// Token: 0x04000D59 RID: 3417
			public long _columnDataBytesRemaining;

			// Token: 0x04000D5A RID: 3418
			public _SqlMetaDataSet _metadata;

			// Token: 0x04000D5B RID: 3419
			public _SqlMetaDataSetCollection _altMetaDataSetCollection;

			// Token: 0x04000D5C RID: 3420
			public MultiPartTableName[] _tableNames;

			// Token: 0x04000D5D RID: 3421
			public SqlSequentialStream _currentStream;

			// Token: 0x04000D5E RID: 3422
			public SqlSequentialTextReader _currentTextReader;
		}
	}
}
