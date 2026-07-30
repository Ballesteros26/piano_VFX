using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Unity;

namespace System.Data.Odbc
{
	/// <summary>Provides a way of reading a forward-only stream of data rows from a data source. This class cannot be inherited. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000297 RID: 663
	public sealed class OdbcDataReader : DbDataReader
	{
		// Token: 0x06001C15 RID: 7189 RVA: 0x0008A904 File Offset: 0x00088B04
		internal OdbcDataReader(OdbcCommand command, CMDWrapper cmdWrapper, CommandBehavior commandbehavior)
		{
			this._recordAffected = -1;
			this._row = -1;
			this._column = -1;
			this.ObjectID = Interlocked.Increment(ref OdbcDataReader.s_objectTypeCount);
			base..ctor();
			this._command = command;
			this._commandBehavior = commandbehavior;
			this._cmdText = command.CommandText;
			this._cmdWrapper = cmdWrapper;
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x0008A960 File Offset: 0x00088B60
		private CNativeBuffer Buffer
		{
			get
			{
				CNativeBuffer dataReaderBuf = this._cmdWrapper._dataReaderBuf;
				if (dataReaderBuf == null)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return dataReaderBuf;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x0008A98E File Offset: 0x00088B8E
		private OdbcConnection Connection
		{
			get
			{
				if (this._cmdWrapper != null)
				{
					return this._cmdWrapper.Connection;
				}
				return null;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001C18 RID: 7192 RVA: 0x0008A9A5 File Offset: 0x00088BA5
		// (set) Token: 0x06001C19 RID: 7193 RVA: 0x0008A9AD File Offset: 0x00088BAD
		internal OdbcCommand Command
		{
			get
			{
				return this._command;
			}
			set
			{
				this._command = value;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x0008A9B6 File Offset: 0x00088BB6
		private OdbcStatementHandle StatementHandle
		{
			get
			{
				return this._cmdWrapper.StatementHandle;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x0008A9C3 File Offset: 0x00088BC3
		private OdbcStatementHandle KeyInfoStatementHandle
		{
			get
			{
				return this._cmdWrapper.KeyInfoStatement;
			}
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x0008A9D0 File Offset: 0x00088BD0
		internal bool IsBehavior(CommandBehavior behavior)
		{
			return this.IsCommandBehavior(behavior);
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x0008A9D9 File Offset: 0x00088BD9
		internal bool IsCancelingCommand
		{
			get
			{
				return this._command != null && this._command.Canceling;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001C1E RID: 7198 RVA: 0x0008A9F0 File Offset: 0x00088BF0
		internal bool IsNonCancelingCommand
		{
			get
			{
				return this._command != null && !this._command.Canceling;
			}
		}

		/// <summary>Gets a value that indicates the depth of nesting for the current row.</summary>
		/// <returns>The depth of nesting for the current row.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x000601C8 File Offset: 0x0005E3C8
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
		/// <returns>When not positioned in a valid record set, 0; otherwise the number of columns in the current record. The default is -1.</returns>
		/// <exception cref="T:System.NotSupportedException">There is no current connection to a data source. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x0008AA0C File Offset: 0x00088C0C
		public override int FieldCount
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("FieldCount");
				}
				if (this._noMoreResults)
				{
					return 0;
				}
				if (this._dataCache == null)
				{
					short num;
					ODBC32.RetCode retCode = this.FieldCountNoThrow(out num);
					if (retCode != ODBC32.RetCode.SUCCESS)
					{
						this.Connection.HandleError(this.StatementHandle, retCode);
					}
				}
				if (this._dataCache == null)
				{
					return 0;
				}
				return this._dataCache._count;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.Odbc.OdbcDataReader" /> contains one or more rows.</summary>
		/// <returns>true if the <see cref="T:System.Data.Odbc.OdbcDataReader" /> contains one or more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x0008AA71 File Offset: 0x00088C71
		public override bool HasRows
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("HasRows");
				}
				if (this._hasRows == OdbcDataReader.HasRowsStatus.DontKnow)
				{
					this.Read();
					this._skipReadOnce = true;
				}
				return this._hasRows == OdbcDataReader.HasRowsStatus.HasRows;
			}
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x0008AAA8 File Offset: 0x00088CA8
		internal ODBC32.RetCode FieldCountNoThrow(out short cColsAffected)
		{
			if (this.IsCancelingCommand)
			{
				cColsAffected = 0;
				return ODBC32.RetCode.ERROR;
			}
			ODBC32.RetCode retCode = this.StatementHandle.NumberOfResultColumns(out cColsAffected);
			if (retCode == ODBC32.RetCode.SUCCESS)
			{
				this._hiddenColumns = 0;
				if (this.IsCommandBehavior(CommandBehavior.KeyInfo) && !this.Connection.ProviderInfo.NoSqlSoptSSNoBrowseTable && !this.Connection.ProviderInfo.NoSqlSoptSSHiddenColumns)
				{
					for (int i = 0; i < (int)cColsAffected; i++)
					{
						if (this.GetColAttribute(i, (ODBC32.SQL_DESC)1211, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.IGNORE).ToInt64() == 1L)
						{
							this._hiddenColumns = (int)cColsAffected - i;
							cColsAffected = (short)i;
							break;
						}
					}
				}
				this._dataCache = new DbCache(this, (int)cColsAffected);
			}
			else
			{
				cColsAffected = 0;
			}
			return retCode;
		}

		/// <summary>Indicates whether the <see cref="T:System.Data.Odbc.OdbcDataReader" /> is closed.</summary>
		/// <returns>true if the <see cref="T:System.Data.Odbc.OdbcDataReader" /> is closed; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x0008AB51 File Offset: 0x00088D51
		public override bool IsClosed
		{
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x0008AB5C File Offset: 0x00088D5C
		private SQLLEN GetRowCount()
		{
			if (!this.IsClosed)
			{
				SQLLEN sqllen;
				ODBC32.RetCode retCode = this.StatementHandle.RowCount(out sqllen);
				if (retCode == ODBC32.RetCode.SUCCESS || ODBC32.RetCode.SUCCESS_WITH_INFO == retCode)
				{
					return sqllen;
				}
			}
			return -1;
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x0008AB8E File Offset: 0x00088D8E
		internal int CalculateRecordsAffected(int cRowsAffected)
		{
			if (0 <= cRowsAffected)
			{
				if (-1 == this._recordAffected)
				{
					this._recordAffected = cRowsAffected;
				}
				else
				{
					this._recordAffected += cRowsAffected;
				}
			}
			return this._recordAffected;
		}

		/// <summary>Gets the number of rows changed, inserted, or deleted by execution of the SQL statement.</summary>
		/// <returns>The number of rows changed, inserted, or deleted. -1 for SELECT statements; 0 if no rows were affected, or the statement failed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x0008ABBA File Offset: 0x00088DBA
		public override int RecordsAffected
		{
			get
			{
				return this._recordAffected;
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column ordinal.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000535 RID: 1333
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column name.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="value">The column name. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">No column with the specified name was found. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000536 RID: 1334
		public override object this[string value]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(value));
			}
		}

		/// <summary>Closes the <see cref="T:System.Data.Odbc.OdbcDataReader" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001C29 RID: 7209 RVA: 0x0008ABC2 File Offset: 0x00088DC2
		public override void Close()
		{
			this.Close(false);
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x0008ABCC File Offset: 0x00088DCC
		private void Close(bool disposing)
		{
			Exception ex = null;
			CMDWrapper cmdWrapper = this._cmdWrapper;
			if (cmdWrapper != null && cmdWrapper.StatementHandle != null)
			{
				if (this.IsNonCancelingCommand)
				{
					this.NextResult(disposing, !disposing);
					if (this._command != null)
					{
						if (this._command.HasParameters)
						{
							this._command.Parameters.GetOutputValues(this._cmdWrapper);
						}
						cmdWrapper.FreeStatementHandle(ODBC32.STMT.CLOSE);
						this._command.CloseFromDataReader();
					}
				}
				cmdWrapper.FreeKeyInfoStatementHandle(ODBC32.STMT.CLOSE);
			}
			if (this._command != null)
			{
				this._command.CloseFromDataReader();
				if (this.IsCommandBehavior(CommandBehavior.CloseConnection))
				{
					this._command.Parameters.RebindCollection = true;
					this.Connection.Close();
				}
			}
			else if (cmdWrapper != null)
			{
				cmdWrapper.Dispose();
			}
			this._command = null;
			this._isClosed = true;
			this._dataCache = null;
			this._metadata = null;
			this._schemaTable = null;
			this._isRead = false;
			this._hasRows = OdbcDataReader.HasRowsStatus.DontKnow;
			this._isValidResult = false;
			this._noMoreResults = true;
			this._noMoreRows = true;
			this._fieldNameLookup = null;
			this.SetCurrentRowColumnInfo(-1, 0);
			if (ex != null && !disposing)
			{
				throw ex;
			}
			this._cmdWrapper = null;
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0008ACED File Offset: 0x00088EED
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close(true);
			}
		}

		/// <summary>Gets the name of the source data type.</summary>
		/// <returns>The name of the source data type.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C2C RID: 7212 RVA: 0x0008ACFC File Offset: 0x00088EFC
		public override string GetDataTypeName(int i)
		{
			if (this._dataCache != null)
			{
				DbSchemaInfo schema = this._dataCache.GetSchema(i);
				if (schema._typename == null)
				{
					schema._typename = this.GetColAttributeStr(i, ODBC32.SQL_DESC.TYPE_NAME, ODBC32.SQL_COLUMN.TYPE_NAME, ODBC32.HANDLER.THROW);
				}
				return schema._typename;
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</returns>
		// Token: 0x06001C2D RID: 7213 RVA: 0x0008AD44 File Offset: 0x00088F44
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this, this.IsCommandBehavior(CommandBehavior.CloseConnection));
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that is the data type of the object.</summary>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C2E RID: 7214 RVA: 0x0008AD54 File Offset: 0x00088F54
		public override Type GetFieldType(int i)
		{
			if (this._dataCache != null)
			{
				DbSchemaInfo schema = this._dataCache.GetSchema(i);
				if (schema._type == null)
				{
					schema._type = this.GetSqlType(i)._type;
				}
				return schema._type;
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the name of the specified column.</summary>
		/// <returns>A string that is the name of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C2F RID: 7215 RVA: 0x0008ADA4 File Offset: 0x00088FA4
		public override string GetName(int i)
		{
			if (this._dataCache != null)
			{
				DbSchemaInfo schema = this._dataCache.GetSchema(i);
				if (schema._name == null)
				{
					schema._name = this.GetColAttributeStr(i, ODBC32.SQL_DESC.NAME, ODBC32.SQL_COLUMN.NAME, ODBC32.HANDLER.THROW);
					if (schema._name == null)
					{
						schema._name = "";
					}
				}
				return schema._name;
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the column ordinal, given the name of the column.</summary>
		/// <returns>The zero-based column ordinal.</returns>
		/// <param name="value">The name of the column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C30 RID: 7216 RVA: 0x0008AE01 File Offset: 0x00089001
		public override int GetOrdinal(string value)
		{
			if (this._fieldNameLookup == null)
			{
				if (this._dataCache == null)
				{
					throw ADP.DataReaderNoData();
				}
				this._fieldNameLookup = new FieldNameLookup(this, -1);
			}
			return this._fieldNameLookup.GetOrdinal(value);
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x0008AE32 File Offset: 0x00089032
		private int IndexOf(string value)
		{
			if (this._fieldNameLookup == null)
			{
				if (this._dataCache == null)
				{
					throw ADP.DataReaderNoData();
				}
				this._fieldNameLookup = new FieldNameLookup(this, -1);
			}
			return this._fieldNameLookup.IndexOf(value);
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x0008AE63 File Offset: 0x00089063
		private bool IsCommandBehavior(CommandBehavior condition)
		{
			return condition == (condition & this._commandBehavior);
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x0008AE70 File Offset: 0x00089070
		internal object GetValue(int i, TypeMap typemap)
		{
			ODBC32.SQL_TYPE sql_type = typemap._sql_type;
			if (sql_type != ODBC32.SQL_TYPE.SS_VARIANT)
			{
				switch (sql_type)
				{
				case ODBC32.SQL_TYPE.GUID:
					return this.internalGetGuid(i);
				case ODBC32.SQL_TYPE.WLONGVARCHAR:
				case ODBC32.SQL_TYPE.WVARCHAR:
				case ODBC32.SQL_TYPE.WCHAR:
				case ODBC32.SQL_TYPE.LONGVARCHAR:
				case ODBC32.SQL_TYPE.CHAR:
				case ODBC32.SQL_TYPE.VARCHAR:
					return this.internalGetString(i);
				case ODBC32.SQL_TYPE.BIT:
					return this.internalGetBoolean(i);
				case ODBC32.SQL_TYPE.TINYINT:
					return this.internalGetByte(i);
				case ODBC32.SQL_TYPE.BIGINT:
					return this.internalGetInt64(i);
				case ODBC32.SQL_TYPE.LONGVARBINARY:
				case ODBC32.SQL_TYPE.VARBINARY:
				case ODBC32.SQL_TYPE.BINARY:
					return this.internalGetBytes(i);
				case (ODBC32.SQL_TYPE)0:
				case (ODBC32.SQL_TYPE)9:
				case (ODBC32.SQL_TYPE)10:
				case ODBC32.SQL_TYPE.TIMESTAMP:
					break;
				case ODBC32.SQL_TYPE.NUMERIC:
				case ODBC32.SQL_TYPE.DECIMAL:
					return this.internalGetDecimal(i);
				case ODBC32.SQL_TYPE.INTEGER:
					return this.internalGetInt32(i);
				case ODBC32.SQL_TYPE.SMALLINT:
					return this.internalGetInt16(i);
				case ODBC32.SQL_TYPE.FLOAT:
				case ODBC32.SQL_TYPE.DOUBLE:
					return this.internalGetDouble(i);
				case ODBC32.SQL_TYPE.REAL:
					return this.internalGetFloat(i);
				default:
					switch (sql_type)
					{
					case ODBC32.SQL_TYPE.TYPE_DATE:
						return this.internalGetDate(i);
					case ODBC32.SQL_TYPE.TYPE_TIME:
						return this.internalGetTime(i);
					case ODBC32.SQL_TYPE.TYPE_TIMESTAMP:
						return this.internalGetDateTime(i);
					}
					break;
				}
				return this.internalGetBytes(i);
			}
			if (!this._isRead)
			{
				throw ADP.DataReaderNoData();
			}
			int num;
			if (this._dataCache.AccessIndex(i) == null && this.QueryFieldInfo(i, ODBC32.SQL_C.BINARY, out num))
			{
				ODBC32.SQL_TYPE sql_TYPE = (ODBC32.SQL_TYPE)this.GetColAttribute(i, (ODBC32.SQL_DESC)1216, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.THROW);
				return this.GetValue(i, TypeMap.FromSqlType(sql_TYPE));
			}
			return this._dataCache[i];
		}

		/// <summary>Gets the value of the column at the specified ordinal in its native format.</summary>
		/// <returns>The value to return.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C34 RID: 7220 RVA: 0x0008AFE4 File Offset: 0x000891E4
		public override object GetValue(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null)
				{
					this._dataCache[i] = this.GetValue(i, this.GetSqlType(i));
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Populates an array of objects with the column values of the current row.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of type <see cref="T:System.Object" /> into which to copy the attribute columns. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C35 RID: 7221 RVA: 0x0008B034 File Offset: 0x00089234
		public override int GetValues(object[] values)
		{
			if (this._isRead)
			{
				int num = Math.Min(values.Length, this.FieldCount);
				for (int i = 0; i < num; i++)
				{
					values[i] = this.GetValue(i);
				}
				return num;
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x0008B078 File Offset: 0x00089278
		private TypeMap GetSqlType(int i)
		{
			DbSchemaInfo schema = this._dataCache.GetSchema(i);
			TypeMap typeMap;
			if (schema._dbtype == null)
			{
				schema._dbtype = new ODBC32.SQL_TYPE?((ODBC32.SQL_TYPE)this.GetColAttribute(i, ODBC32.SQL_DESC.CONCISE_TYPE, ODBC32.SQL_COLUMN.TYPE, ODBC32.HANDLER.THROW));
				typeMap = TypeMap.FromSqlType(schema._dbtype.Value);
				if (typeMap._signType)
				{
					bool flag = this.GetColAttribute(i, ODBC32.SQL_DESC.UNSIGNED, ODBC32.SQL_COLUMN.UNSIGNED, ODBC32.HANDLER.THROW).ToInt64() != 0L;
					typeMap = TypeMap.UpgradeSignedType(typeMap, flag);
					schema._dbtype = new ODBC32.SQL_TYPE?(typeMap._sql_type);
				}
			}
			else
			{
				typeMap = TypeMap.FromSqlType(schema._dbtype.Value);
			}
			this.Connection.SetSupportedType(schema._dbtype.Value);
			return typeMap;
		}

		/// <summary>Gets a value that indicates whether the column contains nonexistent or missing values.</summary>
		/// <returns>true if the specified column value is equivalent to <see cref="T:System.DBNull" />; otherwise false.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C37 RID: 7223 RVA: 0x0008B130 File Offset: 0x00089330
		public override bool IsDBNull(int i)
		{
			if (!this.IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				return Convert.IsDBNull(this.GetValue(i));
			}
			object obj = this._dataCache[i];
			if (obj != null)
			{
				return Convert.IsDBNull(obj);
			}
			TypeMap sqlType = this.GetSqlType(i);
			if (sqlType._bufferSize > 0)
			{
				return Convert.IsDBNull(this.GetValue(i));
			}
			int num;
			return !this.QueryFieldInfo(i, sqlType._sql_c, out num);
		}

		/// <summary>Gets the value of the specified column as a byte.</summary>
		/// <returns>The value of the specified column as a byte.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C38 RID: 7224 RVA: 0x0008B19B File Offset: 0x0008939B
		public override byte GetByte(int i)
		{
			return (byte)this.internalGetByte(i);
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0008B1AC File Offset: 0x000893AC
		private object internalGetByte(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.UTINYINT))
				{
					this._dataCache[i] = this.Buffer.ReadByte(0);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a character.</summary>
		/// <returns>The value of the specified column as a character.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C3A RID: 7226 RVA: 0x0008B209 File Offset: 0x00089409
		public override char GetChar(int i)
		{
			return (char)this.internalGetChar(i);
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x0008B218 File Offset: 0x00089418
		private object internalGetChar(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.WCHAR))
				{
					this._dataCache[i] = this.Buffer.ReadChar(0);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a 16-bit signed integer.</summary>
		/// <returns>The value of the specified column as a 16-bit signed integer.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C3C RID: 7228 RVA: 0x0008B275 File Offset: 0x00089475
		public override short GetInt16(int i)
		{
			return (short)this.internalGetInt16(i);
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x0008B284 File Offset: 0x00089484
		private object internalGetInt16(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.SSHORT))
				{
					this._dataCache[i] = this.Buffer.ReadInt16(0);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
		/// <returns>The value of the specified column as a 32-bit signed integer.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C3E RID: 7230 RVA: 0x0008B2E1 File Offset: 0x000894E1
		public override int GetInt32(int i)
		{
			return (int)this.internalGetInt32(i);
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x0008B2F0 File Offset: 0x000894F0
		private object internalGetInt32(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.SLONG))
				{
					this._dataCache[i] = this.Buffer.ReadInt32(0);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
		/// <returns>The value of the specified column as a 64-bit signed integer.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C40 RID: 7232 RVA: 0x0008B34D File Offset: 0x0008954D
		public override long GetInt64(int i)
		{
			return (long)this.internalGetInt64(i);
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x0008B35C File Offset: 0x0008955C
		private object internalGetInt64(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.WCHAR))
				{
					string text = (string)this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.WCHAR, -3);
					this._dataCache[i] = long.Parse(text, CultureInfo.InvariantCulture);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a Boolean.</summary>
		/// <returns>A Boolean that is the value of the column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C42 RID: 7234 RVA: 0x0008B3CE File Offset: 0x000895CE
		public override bool GetBoolean(int i)
		{
			return (bool)this.internalGetBoolean(i);
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x0008B3DC File Offset: 0x000895DC
		private object internalGetBoolean(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.BIT))
				{
					this._dataCache[i] = this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.BIT, -1);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a single-precision floating-point number.</summary>
		/// <returns>The value of the specified column as a single-precision floating-point number.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C44 RID: 7236 RVA: 0x0008B437 File Offset: 0x00089637
		public override float GetFloat(int i)
		{
			return (float)this.internalGetFloat(i);
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x0008B448 File Offset: 0x00089648
		private object internalGetFloat(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.REAL))
				{
					this._dataCache[i] = this.Buffer.ReadSingle(0);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C46 RID: 7238 RVA: 0x0008B4A4 File Offset: 0x000896A4
		public DateTime GetDate(int i)
		{
			return (DateTime)this.internalGetDate(i);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x0008B4B4 File Offset: 0x000896B4
		private object internalGetDate(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.TYPE_DATE))
				{
					this._dataCache[i] = this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.TYPE_DATE, -1);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C48 RID: 7240 RVA: 0x0008B50F File Offset: 0x0008970F
		public override DateTime GetDateTime(int i)
		{
			return (DateTime)this.internalGetDateTime(i);
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x0008B520 File Offset: 0x00089720
		private object internalGetDateTime(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.TYPE_TIMESTAMP))
				{
					this._dataCache[i] = this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.TYPE_TIMESTAMP, -1);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Decimal" /> object.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.Decimal" /> object.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C4A RID: 7242 RVA: 0x0008B57B File Offset: 0x0008977B
		public override decimal GetDecimal(int i)
		{
			return (decimal)this.internalGetDecimal(i);
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0008B58C File Offset: 0x0008978C
		private object internalGetDecimal(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.WCHAR))
				{
					string text = null;
					try
					{
						text = (string)this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.WCHAR, -3);
						this._dataCache[i] = decimal.Parse(text, CultureInfo.InvariantCulture);
					}
					catch (OverflowException ex)
					{
						this._dataCache[i] = text;
						throw ex;
					}
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a double-precision floating-point number.</summary>
		/// <returns>The value of the specified column as a double-precision floating-point number.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C4C RID: 7244 RVA: 0x0008B620 File Offset: 0x00089820
		public override double GetDouble(int i)
		{
			return (double)this.internalGetDouble(i);
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x0008B630 File Offset: 0x00089830
		private object internalGetDouble(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.DOUBLE))
				{
					this._dataCache[i] = this.Buffer.ReadDouble(0);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a globally unique identifier (GUID).</summary>
		/// <returns>The value of the specified column as a GUID.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C4E RID: 7246 RVA: 0x0008B68C File Offset: 0x0008988C
		public override Guid GetGuid(int i)
		{
			return (Guid)this.internalGetGuid(i);
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x0008B69C File Offset: 0x0008989C
		private object internalGetGuid(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.GUID))
				{
					this._dataCache[i] = this.Buffer.ReadGuid(0);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.String" />.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.String" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C50 RID: 7248 RVA: 0x0008B6F9 File Offset: 0x000898F9
		public override string GetString(int i)
		{
			return (string)this.internalGetString(i);
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x0008B708 File Offset: 0x00089908
		private object internalGetString(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null)
				{
					CNativeBuffer buffer = this.Buffer;
					int num = buffer.Length - 4;
					int num2;
					if (this.GetData(i, ODBC32.SQL_C.WCHAR, buffer.Length - 2, out num2))
					{
						if (num2 <= num && -4 != num2)
						{
							string text = buffer.PtrToStringUni(0, Math.Min(num2, num) / 2);
							this._dataCache[i] = text;
							return text;
						}
						char[] array = new char[num / 2];
						StringBuilder stringBuilder = new StringBuilder(((num2 == -4) ? num : num2) / 2);
						int num3 = num;
						int num4 = ((-4 == num2) ? (-1) : (num2 - num3));
						bool data;
						do
						{
							int num5 = num3 / 2;
							buffer.ReadChars(0, array, 0, num5);
							stringBuilder.Append(array, 0, num5);
							if (num4 == 0)
							{
								break;
							}
							data = this.GetData(i, ODBC32.SQL_C.WCHAR, buffer.Length - 2, out num2);
							if (-4 != num2)
							{
								num3 = Math.Min(num2, num);
								if (0 < num4)
								{
									num4 -= num3;
								}
								else
								{
									num4 = 0;
								}
							}
						}
						while (data);
						this._dataCache[i] = stringBuilder.ToString();
					}
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.TimeSpan" /> object.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.TimeSpan" /> object.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C52 RID: 7250 RVA: 0x0008B830 File Offset: 0x00089A30
		public TimeSpan GetTime(int i)
		{
			return (TimeSpan)this.internalGetTime(i);
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x0008B840 File Offset: 0x00089A40
		private object internalGetTime(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.TYPE_TIME))
				{
					this._dataCache[i] = this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.TYPE_TIME, -1);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x0008B89B File Offset: 0x00089A9B
		private void SetCurrentRowColumnInfo(int row, int column)
		{
			if (this._row != row || this._column != column)
			{
				this._row = row;
				this._column = column;
				this._sequentialBytesRead = 0L;
			}
		}

		/// <summary>Reads a stream of bytes from the specified column offset into the buffer as an array, starting at the particular buffer offset.</summary>
		/// <returns>The actual number of bytes read.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the field where the read operation is to start. </param>
		/// <param name="buffer">The buffer into which to read the stream of bytes. </param>
		/// <param name="bufferIndex">The index within the <paramref name="buffer" /> where the write operation is to start. </param>
		/// <param name="length">The number of bytes to read. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C55 RID: 7253 RVA: 0x0008B8C5 File Offset: 0x00089AC5
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			return this.GetBytesOrChars(i, dataIndex, buffer, false, bufferIndex, length);
		}

		/// <summary>Reads a stream of characters from the specified column offset into the buffer as an array, starting at the particular buffer offset.</summary>
		/// <returns>The actual number of characters read.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the row where the read operation is to start. </param>
		/// <param name="buffer">The buffer into which to copy data. </param>
		/// <param name="bufferIndex">The index within the <paramref name="buffer" /> where the write operation is to start. </param>
		/// <param name="length">The number of characters to read. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C56 RID: 7254 RVA: 0x0008B8D5 File Offset: 0x00089AD5
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			return this.GetBytesOrChars(i, dataIndex, buffer, true, bufferIndex, length);
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x0008B8E8 File Offset: 0x00089AE8
		private long GetBytesOrChars(int i, long dataIndex, Array buffer, bool isCharsBuffer, int bufferIndex, int length)
		{
			if (this.IsClosed)
			{
				throw ADP.DataReaderNoData();
			}
			if (!this._isRead)
			{
				throw ADP.DataReaderNoData();
			}
			if (dataIndex < 0L)
			{
				throw ADP.ArgumentOutOfRange("dataIndex");
			}
			if (bufferIndex < 0)
			{
				throw ADP.ArgumentOutOfRange("bufferIndex");
			}
			if (length < 0)
			{
				throw ADP.ArgumentOutOfRange("length");
			}
			string text = (isCharsBuffer ? "GetChars" : "GetBytes");
			this.SetCurrentRowColumnInfo(this._row, i);
			object obj;
			if (isCharsBuffer)
			{
				obj = (string)this._dataCache[i];
			}
			else
			{
				obj = (byte[])this._dataCache[i];
			}
			if (!this.IsCommandBehavior(CommandBehavior.SequentialAccess) || obj != null)
			{
				if (2147483647L < dataIndex)
				{
					throw ADP.ArgumentOutOfRange("dataIndex");
				}
				if (obj == null)
				{
					if (isCharsBuffer)
					{
						obj = (string)this.internalGetString(i);
					}
					else
					{
						obj = (byte[])this.internalGetBytes(i);
					}
				}
				int num = (isCharsBuffer ? ((string)obj).Length : ((byte[])obj).Length);
				if (buffer == null)
				{
					return (long)num;
				}
				if (length == 0)
				{
					return 0L;
				}
				if (dataIndex >= (long)num)
				{
					return 0L;
				}
				int num2 = Math.Min(num - (int)dataIndex, length);
				num2 = Math.Min(num2, buffer.Length - bufferIndex);
				if (num2 <= 0)
				{
					return 0L;
				}
				if (isCharsBuffer)
				{
					((string)obj).CopyTo((int)dataIndex, (char[])buffer, bufferIndex, num2);
				}
				else
				{
					Array.Copy((byte[])obj, (int)dataIndex, (byte[])buffer, bufferIndex, num2);
				}
				return (long)num2;
			}
			else if (buffer == null)
			{
				ODBC32.SQL_C sql_C = (isCharsBuffer ? ODBC32.SQL_C.WCHAR : ODBC32.SQL_C.BINARY);
				int num3;
				if (!this.QueryFieldInfo(i, sql_C, out num3))
				{
					if (isCharsBuffer)
					{
						throw ADP.InvalidCast();
					}
					return -1L;
				}
				else
				{
					if (isCharsBuffer)
					{
						return (long)(num3 / 2);
					}
					return (long)num3;
				}
			}
			else
			{
				if ((isCharsBuffer && dataIndex < this._sequentialBytesRead / 2L) || (!isCharsBuffer && dataIndex < this._sequentialBytesRead))
				{
					throw ADP.NonSeqByteAccess(dataIndex, this._sequentialBytesRead, text);
				}
				if (isCharsBuffer)
				{
					dataIndex -= this._sequentialBytesRead / 2L;
				}
				else
				{
					dataIndex -= this._sequentialBytesRead;
				}
				if (dataIndex > 0L && (long)this.readBytesOrCharsSequentialAccess(i, null, isCharsBuffer, 0, dataIndex) < dataIndex)
				{
					return 0L;
				}
				length = Math.Min(length, buffer.Length - bufferIndex);
				if (length > 0)
				{
					return (long)this.readBytesOrCharsSequentialAccess(i, buffer, isCharsBuffer, bufferIndex, (long)length);
				}
				int num4;
				if (isCharsBuffer && !this.QueryFieldInfo(i, ODBC32.SQL_C.WCHAR, out num4))
				{
					throw ADP.InvalidCast();
				}
				return 0L;
			}
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x0008BB3C File Offset: 0x00089D3C
		private int readBytesOrCharsSequentialAccess(int i, Array buffer, bool isCharsBuffer, int bufferIndex, long bytesOrCharsLength)
		{
			int num = 0;
			long num2 = (isCharsBuffer ? checked(bytesOrCharsLength * 2L) : bytesOrCharsLength);
			CNativeBuffer buffer2 = this.Buffer;
			while (num2 > 0L)
			{
				int num3;
				int num4;
				bool flag;
				if (isCharsBuffer)
				{
					num3 = (int)Math.Min(num2, (long)(buffer2.Length - 4));
					flag = this.GetData(i, ODBC32.SQL_C.WCHAR, num3 + 2, out num4);
				}
				else
				{
					num3 = (int)Math.Min(num2, (long)(buffer2.Length - 2));
					flag = this.GetData(i, ODBC32.SQL_C.BINARY, num3, out num4);
				}
				if (!flag)
				{
					throw ADP.InvalidCast();
				}
				bool flag2 = false;
				if (num4 == 0)
				{
					break;
				}
				int num5;
				if (-4 == num4)
				{
					num5 = num3;
				}
				else if (num4 > num3)
				{
					num5 = num3;
				}
				else
				{
					num5 = num4;
					flag2 = true;
				}
				this._sequentialBytesRead += (long)num5;
				if (isCharsBuffer)
				{
					int num6 = num5 / 2;
					if (buffer != null)
					{
						buffer2.ReadChars(0, (char[])buffer, bufferIndex, num6);
						bufferIndex += num6;
					}
					num += num6;
				}
				else
				{
					if (buffer != null)
					{
						buffer2.ReadBytes(0, (byte[])buffer, bufferIndex, num5);
						bufferIndex += num5;
					}
					num += num5;
				}
				num2 -= (long)num5;
				if (flag2)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x0008BC4C File Offset: 0x00089E4C
		private object internalGetBytes(int i)
		{
			if (this._dataCache.AccessIndex(i) == null)
			{
				int num = this.Buffer.Length - 4;
				int num2 = 0;
				int j;
				if (this.GetData(i, ODBC32.SQL_C.BINARY, num, out j))
				{
					CNativeBuffer buffer = this.Buffer;
					byte[] array;
					if (-4 != j)
					{
						array = new byte[j];
						this.Buffer.ReadBytes(0, array, num2, Math.Min(j, num));
						while (j > num)
						{
							this.GetData(i, ODBC32.SQL_C.BINARY, num, out j);
							num2 += num;
							buffer.ReadBytes(0, array, num2, Math.Min(j, num));
						}
					}
					else
					{
						List<byte[]> list = new List<byte[]>();
						int num3 = 0;
						do
						{
							int num4 = ((-4 != j) ? j : num);
							array = new byte[num4];
							num3 += num4;
							buffer.ReadBytes(0, array, 0, num4);
							list.Add(array);
						}
						while (-4 == j && this.GetData(i, ODBC32.SQL_C.BINARY, num, out j));
						array = new byte[num3];
						foreach (byte[] array2 in list)
						{
							array2.CopyTo(array, num2);
							num2 += array2.Length;
						}
					}
					this._dataCache[i] = array;
				}
			}
			return this._dataCache[i];
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x0008BDA0 File Offset: 0x00089FA0
		private SQLLEN GetColAttribute(int iColumn, ODBC32.SQL_DESC v3FieldId, ODBC32.SQL_COLUMN v2FieldId, ODBC32.HANDLER handler)
		{
			short num = 0;
			if (this.Connection == null || this._cmdWrapper.Canceling)
			{
				return -1;
			}
			OdbcStatementHandle statementHandle = this.StatementHandle;
			SQLLEN sqllen;
			ODBC32.RetCode retCode;
			if (this.Connection.IsV3Driver)
			{
				retCode = statementHandle.ColumnAttribute(iColumn + 1, (short)v3FieldId, this.Buffer, out num, out sqllen);
			}
			else
			{
				if (v2FieldId == (ODBC32.SQL_COLUMN)(-1))
				{
					return 0;
				}
				retCode = statementHandle.ColumnAttribute(iColumn + 1, (short)v2FieldId, this.Buffer, out num, out sqllen);
			}
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				if (retCode == ODBC32.RetCode.ERROR && "HY091" == this.Command.GetDiagSqlState())
				{
					this.Connection.FlagUnsupportedColAttr(v3FieldId, v2FieldId);
				}
				if (handler == ODBC32.HANDLER.THROW)
				{
					this.Connection.HandleError(statementHandle, retCode);
				}
				return -1;
			}
			return sqllen;
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x0008BE60 File Offset: 0x0008A060
		private string GetColAttributeStr(int i, ODBC32.SQL_DESC v3FieldId, ODBC32.SQL_COLUMN v2FieldId, ODBC32.HANDLER handler)
		{
			short num = 0;
			CNativeBuffer buffer = this.Buffer;
			buffer.WriteInt16(0, 0);
			OdbcStatementHandle statementHandle = this.StatementHandle;
			if (this.Connection == null || this._cmdWrapper.Canceling || statementHandle == null)
			{
				return "";
			}
			ODBC32.RetCode retCode;
			if (this.Connection.IsV3Driver)
			{
				SQLLEN sqllen;
				retCode = statementHandle.ColumnAttribute(i + 1, (short)v3FieldId, buffer, out num, out sqllen);
			}
			else
			{
				if (v2FieldId == (ODBC32.SQL_COLUMN)(-1))
				{
					return null;
				}
				SQLLEN sqllen;
				retCode = statementHandle.ColumnAttribute(i + 1, (short)v2FieldId, buffer, out num, out sqllen);
			}
			if (retCode != ODBC32.RetCode.SUCCESS || num == 0)
			{
				if (retCode == ODBC32.RetCode.ERROR && "HY091" == this.Command.GetDiagSqlState())
				{
					this.Connection.FlagUnsupportedColAttr(v3FieldId, v2FieldId);
				}
				if (handler == ODBC32.HANDLER.THROW)
				{
					this.Connection.HandleError(statementHandle, retCode);
				}
				return null;
			}
			return buffer.PtrToStringUni(0, (int)(num / 2));
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x0008BF2C File Offset: 0x0008A12C
		private string GetDescFieldStr(int i, ODBC32.SQL_DESC attribute, ODBC32.HANDLER handler)
		{
			int num = 0;
			if (this.Connection == null || this._cmdWrapper.Canceling)
			{
				return "";
			}
			if (!this.Connection.IsV3Driver)
			{
				return null;
			}
			CNativeBuffer buffer = this.Buffer;
			using (OdbcDescriptorHandle odbcDescriptorHandle = new OdbcDescriptorHandle(this.StatementHandle, ODBC32.SQL_ATTR.APP_PARAM_DESC))
			{
				ODBC32.RetCode descriptionField = odbcDescriptorHandle.GetDescriptionField(i + 1, attribute, buffer, out num);
				if (descriptionField != ODBC32.RetCode.SUCCESS || num == 0)
				{
					if (descriptionField == ODBC32.RetCode.ERROR && "HY091" == this.Command.GetDiagSqlState())
					{
						this.Connection.FlagUnsupportedColAttr(attribute, ODBC32.SQL_COLUMN.COUNT);
					}
					if (handler == ODBC32.HANDLER.THROW)
					{
						this.Connection.HandleError(this.StatementHandle, descriptionField);
					}
					return null;
				}
			}
			return buffer.PtrToStringUni(0, num / 2);
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x0008C000 File Offset: 0x0008A200
		private bool QueryFieldInfo(int i, ODBC32.SQL_C sqlctype, out int cbLengthOrIndicator)
		{
			int num = 0;
			if (sqlctype == ODBC32.SQL_C.WCHAR)
			{
				num = 2;
			}
			return this.GetData(i, sqlctype, num, out cbLengthOrIndicator);
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0008C020 File Offset: 0x0008A220
		private bool GetData(int i, ODBC32.SQL_C sqlctype)
		{
			int num;
			return this.GetData(i, sqlctype, this.Buffer.Length - 4, out num);
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x0008C044 File Offset: 0x0008A244
		private bool GetData(int i, ODBC32.SQL_C sqlctype, int cb, out int cbLengthOrIndicator)
		{
			IntPtr intPtr = IntPtr.Zero;
			if (this.IsCancelingCommand)
			{
				throw ADP.DataReaderNoData();
			}
			CNativeBuffer buffer = this.Buffer;
			ODBC32.RetCode data = this.StatementHandle.GetData(i + 1, sqlctype, buffer, cb, out intPtr);
			if (data != ODBC32.RetCode.SUCCESS)
			{
				if (data != ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					if (data != ODBC32.RetCode.NO_DATA)
					{
						this.Connection.HandleError(this.StatementHandle, data);
					}
					else
					{
						if (sqlctype != ODBC32.SQL_C.WCHAR && sqlctype != ODBC32.SQL_C.BINARY)
						{
							this.Connection.HandleError(this.StatementHandle, data);
						}
						if (intPtr == (IntPtr)(-4))
						{
							intPtr = (IntPtr)0;
						}
					}
				}
				else if ((int)intPtr == -4)
				{
				}
			}
			this.SetCurrentRowColumnInfo(this._row, i);
			if (intPtr == (IntPtr)(-1))
			{
				this._dataCache[i] = DBNull.Value;
				cbLengthOrIndicator = 0;
				return false;
			}
			cbLengthOrIndicator = (int)intPtr;
			return true;
		}

		/// <summary>Advances the <see cref="T:System.Data.Odbc.OdbcDataReader" /> to the next record.</summary>
		/// <returns>true if there are more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001C60 RID: 7264 RVA: 0x0008C11C File Offset: 0x0008A31C
		public override bool Read()
		{
			if (this.IsClosed)
			{
				throw ADP.DataReaderClosed("Read");
			}
			if (this.IsCancelingCommand)
			{
				this._isRead = false;
				return false;
			}
			if (this._skipReadOnce)
			{
				this._skipReadOnce = false;
				return this._isRead;
			}
			if (this._noMoreRows || this._noMoreResults || this.IsCommandBehavior(CommandBehavior.SchemaOnly))
			{
				return false;
			}
			if (!this._isValidResult)
			{
				return false;
			}
			ODBC32.RetCode retCode = this.StatementHandle.Fetch();
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				if (retCode != ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					if (retCode != ODBC32.RetCode.NO_DATA)
					{
						this.Connection.HandleError(this.StatementHandle, retCode);
					}
					else
					{
						this._isRead = false;
						if (this._hasRows == OdbcDataReader.HasRowsStatus.DontKnow)
						{
							this._hasRows = OdbcDataReader.HasRowsStatus.HasNoRows;
						}
					}
				}
				else
				{
					this.Connection.HandleErrorNoThrow(this.StatementHandle, retCode);
					this._hasRows = OdbcDataReader.HasRowsStatus.HasRows;
					this._isRead = true;
				}
			}
			else
			{
				this._hasRows = OdbcDataReader.HasRowsStatus.HasRows;
				this._isRead = true;
			}
			this._dataCache.FlushValues();
			if (this.IsCommandBehavior(CommandBehavior.SingleRow))
			{
				this._noMoreRows = true;
				this.SetCurrentRowColumnInfo(-1, 0);
			}
			else
			{
				this.SetCurrentRowColumnInfo(this._row + 1, 0);
			}
			return this._isRead;
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x0008C23C File Offset: 0x0008A43C
		internal void FirstResult()
		{
			SQLLEN rowCount = this.GetRowCount();
			this.CalculateRecordsAffected(rowCount);
			short num;
			if (this.FieldCountNoThrow(out num) == ODBC32.RetCode.SUCCESS && num == 0)
			{
				this.NextResult();
				return;
			}
			this._isValidResult = true;
		}

		/// <summary>Advances the <see cref="T:System.Data.Odbc.OdbcDataReader" /> to the next result when reading the results of batch SQL statements.</summary>
		/// <returns>true if there are more result sets; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001C62 RID: 7266 RVA: 0x0008C279 File Offset: 0x0008A479
		public override bool NextResult()
		{
			return this.NextResult(false, false);
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x0008C284 File Offset: 0x0008A484
		private bool NextResult(bool disposing, bool allresults)
		{
			ODBC32.RetCode retCode = ODBC32.RetCode.SUCCESS;
			bool flag = false;
			bool flag2 = this.IsCommandBehavior(CommandBehavior.SingleResult);
			if (this.IsClosed)
			{
				throw ADP.DataReaderClosed("NextResult");
			}
			this._fieldNameLookup = null;
			if (this.IsCancelingCommand || this._noMoreResults)
			{
				return false;
			}
			this._isRead = false;
			this._hasRows = OdbcDataReader.HasRowsStatus.DontKnow;
			this._fieldNameLookup = null;
			this._metadata = null;
			this._schemaTable = null;
			int num = 0;
			OdbcErrorCollection odbcErrorCollection = null;
			ODBC32.RetCode retCode2;
			bool flag3;
			do
			{
				this._isValidResult = false;
				retCode2 = this.StatementHandle.MoreResults();
				flag3 = retCode2 == ODBC32.RetCode.SUCCESS || retCode2 == ODBC32.RetCode.SUCCESS_WITH_INFO;
				if (retCode2 == ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					this.Connection.HandleErrorNoThrow(this.StatementHandle, retCode2);
				}
				else if (!disposing && retCode2 != ODBC32.RetCode.NO_DATA && retCode2 != ODBC32.RetCode.SUCCESS)
				{
					if (odbcErrorCollection == null)
					{
						retCode = retCode2;
						odbcErrorCollection = new OdbcErrorCollection();
					}
					ODBC32.GetDiagErrors(odbcErrorCollection, null, this.StatementHandle, retCode2);
					num++;
				}
				if (!disposing && flag3)
				{
					num = 0;
					SQLLEN rowCount = this.GetRowCount();
					this.CalculateRecordsAffected(rowCount);
					if (!flag2)
					{
						short num2;
						this.FieldCountNoThrow(out num2);
						flag = num2 != 0;
						this._isValidResult = flag;
					}
				}
			}
			while ((!flag2 && flag3 && !flag) || (ODBC32.RetCode.NO_DATA != retCode2 && allresults && num < 2000) || (flag2 && flag3));
			if (retCode2 == ODBC32.RetCode.NO_DATA)
			{
				this._dataCache = null;
				this._noMoreResults = true;
			}
			if (odbcErrorCollection != null)
			{
				odbcErrorCollection.SetSource(this.Connection.Driver);
				OdbcException ex = OdbcException.CreateException(odbcErrorCollection, retCode);
				this.Connection.ConnectionIsAlive(ex);
				throw ex;
			}
			return flag3;
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x0008C410 File Offset: 0x0008A610
		private void BuildMetaDataInfo()
		{
			int fieldCount = this.FieldCount;
			OdbcDataReader.MetaData[] array = new OdbcDataReader.MetaData[fieldCount];
			bool flag = this.IsCommandBehavior(CommandBehavior.KeyInfo);
			List<string> list;
			if (flag)
			{
				list = new List<string>();
			}
			else
			{
				list = null;
			}
			for (int i = 0; i < fieldCount; i++)
			{
				array[i] = new OdbcDataReader.MetaData();
				array[i].ordinal = i;
				TypeMap typeMap = TypeMap.FromSqlType((ODBC32.SQL_TYPE)this.GetColAttribute(i, ODBC32.SQL_DESC.CONCISE_TYPE, ODBC32.SQL_COLUMN.TYPE, ODBC32.HANDLER.THROW));
				if (typeMap._signType)
				{
					bool flag2 = this.GetColAttribute(i, ODBC32.SQL_DESC.UNSIGNED, ODBC32.SQL_COLUMN.UNSIGNED, ODBC32.HANDLER.THROW).ToInt64() != 0L;
					typeMap = TypeMap.UpgradeSignedType(typeMap, flag2);
				}
				array[i].typemap = typeMap;
				array[i].size = this.GetColAttribute(i, ODBC32.SQL_DESC.OCTET_LENGTH, ODBC32.SQL_COLUMN.LENGTH, ODBC32.HANDLER.IGNORE);
				ODBC32.SQL_TYPE sql_TYPE = array[i].typemap._sql_type;
				if (sql_TYPE - ODBC32.SQL_TYPE.WLONGVARCHAR <= 2)
				{
					OdbcDataReader.MetaData metaData = array[i];
					metaData.size /= 2;
				}
				array[i].precision = (byte)this.GetColAttribute(i, (ODBC32.SQL_DESC)4, ODBC32.SQL_COLUMN.PRECISION, ODBC32.HANDLER.IGNORE);
				array[i].scale = (byte)this.GetColAttribute(i, (ODBC32.SQL_DESC)5, ODBC32.SQL_COLUMN.SCALE, ODBC32.HANDLER.IGNORE);
				array[i].isAutoIncrement = this.GetColAttribute(i, ODBC32.SQL_DESC.AUTO_UNIQUE_VALUE, ODBC32.SQL_COLUMN.AUTO_INCREMENT, ODBC32.HANDLER.IGNORE) == 1;
				array[i].isReadOnly = this.GetColAttribute(i, ODBC32.SQL_DESC.UPDATABLE, ODBC32.SQL_COLUMN.UPDATABLE, ODBC32.HANDLER.IGNORE) == 0;
				ODBC32.SQL_NULLABILITY sql_NULLABILITY = (ODBC32.SQL_NULLABILITY)this.GetColAttribute(i, ODBC32.SQL_DESC.NULLABLE, ODBC32.SQL_COLUMN.NULLABLE, ODBC32.HANDLER.IGNORE);
				array[i].isNullable = sql_NULLABILITY == ODBC32.SQL_NULLABILITY.NULLABLE;
				sql_TYPE = array[i].typemap._sql_type;
				if (sql_TYPE == ODBC32.SQL_TYPE.WLONGVARCHAR || sql_TYPE == ODBC32.SQL_TYPE.LONGVARBINARY || sql_TYPE == ODBC32.SQL_TYPE.LONGVARCHAR)
				{
					array[i].isLong = true;
				}
				else
				{
					array[i].isLong = false;
				}
				if (this.IsCommandBehavior(CommandBehavior.KeyInfo))
				{
					if (!this.Connection.ProviderInfo.NoSqlCASSColumnKey)
					{
						bool flag3 = this.GetColAttribute(i, (ODBC32.SQL_DESC)1212, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.IGNORE) == 1;
						if (flag3)
						{
							array[i].isKeyColumn = flag3;
							array[i].isUnique = true;
							flag = false;
						}
					}
					array[i].baseSchemaName = this.GetColAttributeStr(i, ODBC32.SQL_DESC.SCHEMA_NAME, ODBC32.SQL_COLUMN.OWNER_NAME, ODBC32.HANDLER.IGNORE);
					array[i].baseCatalogName = this.GetColAttributeStr(i, ODBC32.SQL_DESC.CATALOG_NAME, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.IGNORE);
					array[i].baseTableName = this.GetColAttributeStr(i, ODBC32.SQL_DESC.BASE_TABLE_NAME, ODBC32.SQL_COLUMN.TABLE_NAME, ODBC32.HANDLER.IGNORE);
					array[i].baseColumnName = this.GetColAttributeStr(i, ODBC32.SQL_DESC.BASE_COLUMN_NAME, ODBC32.SQL_COLUMN.NAME, ODBC32.HANDLER.IGNORE);
					if (this.Connection.IsV3Driver)
					{
						if (array[i].baseTableName == null || array[i].baseTableName.Length == 0)
						{
							array[i].baseTableName = this.GetDescFieldStr(i, ODBC32.SQL_DESC.BASE_TABLE_NAME, ODBC32.HANDLER.IGNORE);
						}
						if (array[i].baseColumnName == null || array[i].baseColumnName.Length == 0)
						{
							array[i].baseColumnName = this.GetDescFieldStr(i, ODBC32.SQL_DESC.BASE_COLUMN_NAME, ODBC32.HANDLER.IGNORE);
						}
					}
					if (array[i].baseTableName != null && !list.Contains(array[i].baseTableName))
					{
						list.Add(array[i].baseTableName);
					}
				}
				if ((array[i].isKeyColumn || array[i].isAutoIncrement) && sql_NULLABILITY == ODBC32.SQL_NULLABILITY.UNKNOWN)
				{
					array[i].isNullable = false;
				}
			}
			if (!this.Connection.ProviderInfo.NoSqlCASSColumnKey)
			{
				for (int j = fieldCount; j < fieldCount + this._hiddenColumns; j++)
				{
					bool flag3 = this.GetColAttribute(j, (ODBC32.SQL_DESC)1212, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.IGNORE) == 1;
					if (flag3 && this.GetColAttribute(j, (ODBC32.SQL_DESC)1211, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.IGNORE) == 1)
					{
						for (int k = 0; k < fieldCount; k++)
						{
							array[k].isKeyColumn = false;
							array[k].isUnique = false;
						}
					}
				}
			}
			this._metadata = array;
			if (this.IsCommandBehavior(CommandBehavior.KeyInfo))
			{
				if (list != null && list.Count > 0)
				{
					List<string>.Enumerator enumerator = list.GetEnumerator();
					OdbcDataReader.QualifiedTableName qualifiedTableName = new OdbcDataReader.QualifiedTableName(this.Connection.QuoteChar("GetSchemaTable"));
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						qualifiedTableName.Table = text;
						if (this.RetrieveKeyInfo(flag, qualifiedTableName, false) <= 0)
						{
							this.RetrieveKeyInfo(flag, qualifiedTableName, true);
						}
					}
					return;
				}
				OdbcDataReader.QualifiedTableName qualifiedTableName2 = new OdbcDataReader.QualifiedTableName(this.Connection.QuoteChar("GetSchemaTable"), this.GetTableNameFromCommandText());
				if (!string.IsNullOrEmpty(qualifiedTableName2.Table))
				{
					this.SetBaseTableNames(qualifiedTableName2);
					if (this.RetrieveKeyInfo(flag, qualifiedTableName2, false) <= 0)
					{
						this.RetrieveKeyInfo(flag, qualifiedTableName2, true);
					}
				}
			}
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x0008C874 File Offset: 0x0008AA74
		private DataTable NewSchemaTable()
		{
			DataTable dataTable = new DataTable("SchemaTable");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.MinimumCapacity = this.FieldCount;
			DataColumnCollection columns = dataTable.Columns;
			columns.Add(new DataColumn("ColumnName", typeof(string)));
			columns.Add(new DataColumn("ColumnOrdinal", typeof(int)));
			columns.Add(new DataColumn("ColumnSize", typeof(int)));
			columns.Add(new DataColumn("NumericPrecision", typeof(short)));
			columns.Add(new DataColumn("NumericScale", typeof(short)));
			columns.Add(new DataColumn("DataType", typeof(object)));
			columns.Add(new DataColumn("ProviderType", typeof(int)));
			columns.Add(new DataColumn("IsLong", typeof(bool)));
			columns.Add(new DataColumn("AllowDBNull", typeof(bool)));
			columns.Add(new DataColumn("IsReadOnly", typeof(bool)));
			columns.Add(new DataColumn("IsRowVersion", typeof(bool)));
			columns.Add(new DataColumn("IsUnique", typeof(bool)));
			columns.Add(new DataColumn("IsKey", typeof(bool)));
			columns.Add(new DataColumn("IsAutoIncrement", typeof(bool)));
			columns.Add(new DataColumn("BaseSchemaName", typeof(string)));
			columns.Add(new DataColumn("BaseCatalogName", typeof(string)));
			columns.Add(new DataColumn("BaseTableName", typeof(string)));
			columns.Add(new DataColumn("BaseColumnName", typeof(string)));
			foreach (object obj in columns)
			{
				((DataColumn)obj).ReadOnly = true;
			}
			return dataTable;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.Odbc.OdbcDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.Odbc.OdbcDataReader" /> is closed. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001C66 RID: 7270 RVA: 0x0008CAC4 File Offset: 0x0008ACC4
		public override DataTable GetSchemaTable()
		{
			if (this.IsClosed)
			{
				throw ADP.DataReaderClosed("GetSchemaTable");
			}
			if (this._noMoreResults)
			{
				return null;
			}
			if (this._schemaTable != null)
			{
				return this._schemaTable;
			}
			DataTable dataTable = this.NewSchemaTable();
			if (this.FieldCount == 0)
			{
				return dataTable;
			}
			if (this._metadata == null)
			{
				this.BuildMetaDataInfo();
			}
			DataColumn dataColumn = dataTable.Columns["ColumnName"];
			DataColumn dataColumn2 = dataTable.Columns["ColumnOrdinal"];
			DataColumn dataColumn3 = dataTable.Columns["ColumnSize"];
			DataColumn dataColumn4 = dataTable.Columns["NumericPrecision"];
			DataColumn dataColumn5 = dataTable.Columns["NumericScale"];
			DataColumn dataColumn6 = dataTable.Columns["DataType"];
			DataColumn dataColumn7 = dataTable.Columns["ProviderType"];
			DataColumn dataColumn8 = dataTable.Columns["IsLong"];
			DataColumn dataColumn9 = dataTable.Columns["AllowDBNull"];
			DataColumn dataColumn10 = dataTable.Columns["IsReadOnly"];
			DataColumn dataColumn11 = dataTable.Columns["IsRowVersion"];
			DataColumn dataColumn12 = dataTable.Columns["IsUnique"];
			DataColumn dataColumn13 = dataTable.Columns["IsKey"];
			DataColumn dataColumn14 = dataTable.Columns["IsAutoIncrement"];
			DataColumn dataColumn15 = dataTable.Columns["BaseSchemaName"];
			DataColumn dataColumn16 = dataTable.Columns["BaseCatalogName"];
			DataColumn dataColumn17 = dataTable.Columns["BaseTableName"];
			DataColumn dataColumn18 = dataTable.Columns["BaseColumnName"];
			int fieldCount = this.FieldCount;
			for (int i = 0; i < fieldCount; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow[dataColumn] = this.GetName(i);
				dataRow[dataColumn2] = i;
				dataRow[dataColumn3] = (int)Math.Min(Math.Max(-2147483648L, this._metadata[i].size.ToInt64()), 2147483647L);
				dataRow[dataColumn4] = (short)this._metadata[i].precision;
				dataRow[dataColumn5] = (short)this._metadata[i].scale;
				dataRow[dataColumn6] = this._metadata[i].typemap._type;
				dataRow[dataColumn7] = this._metadata[i].typemap._odbcType;
				dataRow[dataColumn8] = this._metadata[i].isLong;
				dataRow[dataColumn9] = this._metadata[i].isNullable;
				dataRow[dataColumn10] = this._metadata[i].isReadOnly;
				dataRow[dataColumn11] = this._metadata[i].isRowVersion;
				dataRow[dataColumn12] = this._metadata[i].isUnique;
				dataRow[dataColumn13] = this._metadata[i].isKeyColumn;
				dataRow[dataColumn14] = this._metadata[i].isAutoIncrement;
				dataRow[dataColumn15] = this._metadata[i].baseSchemaName;
				dataRow[dataColumn16] = this._metadata[i].baseCatalogName;
				dataRow[dataColumn17] = this._metadata[i].baseTableName;
				dataRow[dataColumn18] = this._metadata[i].baseColumnName;
				dataTable.Rows.Add(dataRow);
				dataRow.AcceptChanges();
			}
			this._schemaTable = dataTable;
			return dataTable;
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x0008CE8C File Offset: 0x0008B08C
		internal int RetrieveKeyInfo(bool needkeyinfo, OdbcDataReader.QualifiedTableName qualifiedTableName, bool quoted)
		{
			int num = 0;
			IntPtr intPtr = IntPtr.Zero;
			if (this.IsClosed || this._cmdWrapper == null)
			{
				return 0;
			}
			this._cmdWrapper.CreateKeyInfoStatementHandle();
			CNativeBuffer buffer = this.Buffer;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				buffer.DangerousAddRef(ref flag);
				ODBC32.RetCode retCode;
				if (needkeyinfo)
				{
					if (!this.Connection.ProviderInfo.NoSqlPrimaryKeys)
					{
						retCode = this.KeyInfoStatementHandle.PrimaryKeys(qualifiedTableName.Catalog, qualifiedTableName.Schema, qualifiedTableName.GetTable(quoted));
						if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
						{
							bool flag2 = false;
							buffer.WriteInt16(0, 0);
							retCode = this.KeyInfoStatementHandle.BindColumn2(4, ODBC32.SQL_C.WCHAR, buffer.PtrOffset(0, 256), (IntPtr)256, buffer.PtrOffset(256, IntPtr.Size).Handle);
							while (this.KeyInfoStatementHandle.Fetch() == ODBC32.RetCode.SUCCESS)
							{
								intPtr = buffer.ReadIntPtr(256);
								string text = buffer.PtrToStringUni(0, (int)intPtr / 2);
								int num2 = this.GetOrdinalFromBaseColName(text);
								if (num2 == -1)
								{
									flag2 = true;
									break;
								}
								num++;
								this._metadata[num2].isKeyColumn = true;
								this._metadata[num2].isUnique = true;
								this._metadata[num2].isNullable = false;
								this._metadata[num2].baseTableName = qualifiedTableName.Table;
								if (this._metadata[num2].baseColumnName == null)
								{
									this._metadata[num2].baseColumnName = text;
								}
							}
							if (flag2)
							{
								OdbcDataReader.MetaData[] metadata = this._metadata;
								for (int i = 0; i < metadata.Length; i++)
								{
									metadata[i].isKeyColumn = false;
								}
							}
							retCode = this.KeyInfoStatementHandle.BindColumn3(4, ODBC32.SQL_C.WCHAR, buffer.DangerousGetHandle());
						}
						else if ("IM001" == this.Command.GetDiagSqlState())
						{
							this.Connection.ProviderInfo.NoSqlPrimaryKeys = true;
						}
					}
					if (num == 0)
					{
						this.KeyInfoStatementHandle.MoreResults();
						num += this.RetrieveKeyInfoFromStatistics(qualifiedTableName, quoted);
					}
					this.KeyInfoStatementHandle.MoreResults();
				}
				retCode = this.KeyInfoStatementHandle.SpecialColumns(qualifiedTableName.GetTable(quoted));
				if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					intPtr = IntPtr.Zero;
					buffer.WriteInt16(0, 0);
					retCode = this.KeyInfoStatementHandle.BindColumn2(2, ODBC32.SQL_C.WCHAR, buffer.PtrOffset(0, 256), (IntPtr)256, buffer.PtrOffset(256, IntPtr.Size).Handle);
					while (this.KeyInfoStatementHandle.Fetch() == ODBC32.RetCode.SUCCESS)
					{
						intPtr = buffer.ReadIntPtr(256);
						string text = buffer.PtrToStringUni(0, (int)intPtr / 2);
						int num2 = this.GetOrdinalFromBaseColName(text);
						if (num2 != -1)
						{
							this._metadata[num2].isRowVersion = true;
							if (this._metadata[num2].baseColumnName == null)
							{
								this._metadata[num2].baseColumnName = text;
							}
						}
					}
					retCode = this.KeyInfoStatementHandle.BindColumn3(2, ODBC32.SQL_C.WCHAR, buffer.DangerousGetHandle());
					retCode = this.KeyInfoStatementHandle.MoreResults();
				}
			}
			finally
			{
				if (flag)
				{
					buffer.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x0008D1C4 File Offset: 0x0008B3C4
		private int RetrieveKeyInfoFromStatistics(OdbcDataReader.QualifiedTableName qualifiedTableName, bool quoted)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			int[] array = new int[16];
			int[] array2 = new int[16];
			int num = 0;
			int num2 = 0;
			bool flag = false;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			int num3 = 0;
			string text4 = string.Copy(qualifiedTableName.GetTable(quoted));
			ODBC32.RetCode retCode = this.KeyInfoStatementHandle.Statistics(text4);
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				return 0;
			}
			CNativeBuffer buffer = this.Buffer;
			bool flag2 = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				buffer.DangerousAddRef(ref flag2);
				HandleRef handleRef = buffer.PtrOffset(0, 256);
				HandleRef handleRef2 = buffer.PtrOffset(256, 256);
				HandleRef handleRef3 = buffer.PtrOffset(512, 4);
				IntPtr handle = buffer.PtrOffset(520, IntPtr.Size).Handle;
				IntPtr handle2 = buffer.PtrOffset(528, IntPtr.Size).Handle;
				IntPtr handle3 = buffer.PtrOffset(536, IntPtr.Size).Handle;
				buffer.WriteInt16(256, 0);
				retCode = this.KeyInfoStatementHandle.BindColumn2(6, ODBC32.SQL_C.WCHAR, handleRef2, (IntPtr)256, handle2);
				retCode = this.KeyInfoStatementHandle.BindColumn2(8, ODBC32.SQL_C.SSHORT, handleRef3, (IntPtr)4, handle3);
				buffer.WriteInt16(512, 0);
				retCode = this.KeyInfoStatementHandle.BindColumn2(9, ODBC32.SQL_C.WCHAR, handleRef, (IntPtr)256, handle);
				while (this.KeyInfoStatementHandle.Fetch() == ODBC32.RetCode.SUCCESS)
				{
					intPtr2 = buffer.ReadIntPtr(520);
					intPtr = buffer.ReadIntPtr(528);
					if (buffer.ReadInt16(256) != 0)
					{
						text = buffer.PtrToStringUni(0, (int)intPtr2 / 2);
						text2 = buffer.PtrToStringUni(256, (int)intPtr / 2);
						int num4 = (int)buffer.ReadInt16(512);
						if (this.SameIndexColumn(text3, text2, num4, num2))
						{
							if (!flag)
							{
								num4 = this.GetOrdinalFromBaseColName(text, qualifiedTableName.Table);
								if (num4 == -1)
								{
									flag = true;
								}
								else if (num2 < 16)
								{
									array[num2++] = num4;
								}
								else
								{
									flag = true;
								}
							}
						}
						else
						{
							if (!flag && num2 != 0 && (num == 0 || num > num2))
							{
								num = num2;
								for (int i = 0; i < num2; i++)
								{
									array2[i] = array[i];
								}
							}
							num2 = 0;
							text3 = text2;
							flag = false;
							num4 = this.GetOrdinalFromBaseColName(text, qualifiedTableName.Table);
							if (num4 == -1)
							{
								flag = true;
							}
							else
							{
								array[num2++] = num4;
							}
						}
					}
				}
				if (!flag && num2 != 0 && (num == 0 || num > num2))
				{
					num = num2;
					for (int j = 0; j < num2; j++)
					{
						array2[j] = array[j];
					}
				}
				if (num != 0)
				{
					for (int k = 0; k < num; k++)
					{
						int num5 = array2[k];
						num3++;
						this._metadata[num5].isKeyColumn = true;
						this._metadata[num5].isNullable = false;
						this._metadata[num5].isUnique = true;
						if (this._metadata[num5].baseTableName == null)
						{
							this._metadata[num5].baseTableName = qualifiedTableName.Table;
						}
						if (this._metadata[num5].baseColumnName == null)
						{
							this._metadata[num5].baseColumnName = text;
						}
					}
				}
				this._cmdWrapper.FreeKeyInfoStatementHandle(ODBC32.STMT.UNBIND);
			}
			finally
			{
				if (flag2)
				{
					buffer.DangerousRelease();
				}
			}
			return num3;
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x0008D560 File Offset: 0x0008B760
		internal bool SameIndexColumn(string currentindexname, string indexname, int ordinal, int ncols)
		{
			return !string.IsNullOrEmpty(currentindexname) && (currentindexname == indexname && ordinal == ncols + 1);
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x0008D57F File Offset: 0x0008B77F
		internal int GetOrdinalFromBaseColName(string columnname)
		{
			return this.GetOrdinalFromBaseColName(columnname, null);
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x0008D58C File Offset: 0x0008B78C
		internal int GetOrdinalFromBaseColName(string columnname, string tablename)
		{
			if (string.IsNullOrEmpty(columnname))
			{
				return -1;
			}
			if (this._metadata != null)
			{
				int fieldCount = this.FieldCount;
				for (int i = 0; i < fieldCount; i++)
				{
					if (this._metadata[i].baseColumnName != null && columnname == this._metadata[i].baseColumnName)
					{
						if (string.IsNullOrEmpty(tablename))
						{
							return i;
						}
						if (tablename == this._metadata[i].baseTableName)
						{
							return i;
						}
					}
				}
			}
			return this.IndexOf(columnname);
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0008D60C File Offset: 0x0008B80C
		internal string GetTableNameFromCommandText()
		{
			if (this._command == null)
			{
				return null;
			}
			string text = this._cmdText;
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			CStringTokenizer cstringTokenizer = new CStringTokenizer(text, this.Connection.QuoteChar("GetSchemaTable")[0], this.Connection.EscapeChar("GetSchemaTable"));
			int num;
			if (cstringTokenizer.StartsWith("select"))
			{
				num = cstringTokenizer.FindTokenIndex("from");
			}
			else if (cstringTokenizer.StartsWith("insert") || cstringTokenizer.StartsWith("update") || cstringTokenizer.StartsWith("delete"))
			{
				num = cstringTokenizer.CurrentPosition;
			}
			else
			{
				num = -1;
			}
			if (num == -1)
			{
				return null;
			}
			string text2 = cstringTokenizer.NextToken();
			text = cstringTokenizer.NextToken();
			if (text.Length > 0 && text[0] == ',')
			{
				return null;
			}
			if (text.Length == 2 && (text[0] == 'a' || text[0] == 'A') && (text[1] == 's' || text[1] == 'S'))
			{
				text = cstringTokenizer.NextToken();
				text = cstringTokenizer.NextToken();
				if (text.Length > 0 && text[0] == ',')
				{
					return null;
				}
			}
			return text2;
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0008D734 File Offset: 0x0008B934
		internal void SetBaseTableNames(OdbcDataReader.QualifiedTableName qualifiedTableName)
		{
			int fieldCount = this.FieldCount;
			for (int i = 0; i < fieldCount; i++)
			{
				if (this._metadata[i].baseTableName == null)
				{
					this._metadata[i].baseTableName = qualifiedTableName.Table;
					this._metadata[i].baseSchemaName = qualifiedTableName.Schema;
					this._metadata[i].baseCatalogName = qualifiedTableName.Catalog;
				}
			}
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x00010468 File Offset: 0x0000E668
		internal OdbcDataReader()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400150B RID: 5387
		private OdbcCommand _command;

		// Token: 0x0400150C RID: 5388
		private int _recordAffected;

		// Token: 0x0400150D RID: 5389
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x0400150E RID: 5390
		private DbCache _dataCache;

		// Token: 0x0400150F RID: 5391
		private OdbcDataReader.HasRowsStatus _hasRows;

		// Token: 0x04001510 RID: 5392
		private bool _isClosed;

		// Token: 0x04001511 RID: 5393
		private bool _isRead;

		// Token: 0x04001512 RID: 5394
		private bool _isValidResult;

		// Token: 0x04001513 RID: 5395
		private bool _noMoreResults;

		// Token: 0x04001514 RID: 5396
		private bool _noMoreRows;

		// Token: 0x04001515 RID: 5397
		private bool _skipReadOnce;

		// Token: 0x04001516 RID: 5398
		private int _hiddenColumns;

		// Token: 0x04001517 RID: 5399
		private CommandBehavior _commandBehavior;

		// Token: 0x04001518 RID: 5400
		private int _row;

		// Token: 0x04001519 RID: 5401
		private int _column;

		// Token: 0x0400151A RID: 5402
		private long _sequentialBytesRead;

		// Token: 0x0400151B RID: 5403
		private static int s_objectTypeCount;

		// Token: 0x0400151C RID: 5404
		internal readonly int ObjectID;

		// Token: 0x0400151D RID: 5405
		private OdbcDataReader.MetaData[] _metadata;

		// Token: 0x0400151E RID: 5406
		private DataTable _schemaTable;

		// Token: 0x0400151F RID: 5407
		private string _cmdText;

		// Token: 0x04001520 RID: 5408
		private CMDWrapper _cmdWrapper;

		// Token: 0x02000298 RID: 664
		private enum HasRowsStatus
		{
			// Token: 0x04001522 RID: 5410
			DontKnow,
			// Token: 0x04001523 RID: 5411
			HasRows,
			// Token: 0x04001524 RID: 5412
			HasNoRows
		}

		// Token: 0x02000299 RID: 665
		internal sealed class QualifiedTableName
		{
			// Token: 0x17000537 RID: 1335
			// (get) Token: 0x06001C6F RID: 7279 RVA: 0x0008D79C File Offset: 0x0008B99C
			internal string Catalog
			{
				get
				{
					return this._catalogName;
				}
			}

			// Token: 0x17000538 RID: 1336
			// (get) Token: 0x06001C70 RID: 7280 RVA: 0x0008D7A4 File Offset: 0x0008B9A4
			internal string Schema
			{
				get
				{
					return this._schemaName;
				}
			}

			// Token: 0x17000539 RID: 1337
			// (get) Token: 0x06001C71 RID: 7281 RVA: 0x0008D7AC File Offset: 0x0008B9AC
			// (set) Token: 0x06001C72 RID: 7282 RVA: 0x0008D7B4 File Offset: 0x0008B9B4
			internal string Table
			{
				get
				{
					return this._tableName;
				}
				set
				{
					this._quotedTableName = value;
					this._tableName = this.UnQuote(value);
				}
			}

			// Token: 0x1700053A RID: 1338
			// (get) Token: 0x06001C73 RID: 7283 RVA: 0x0008D7CA File Offset: 0x0008B9CA
			internal string QuotedTable
			{
				get
				{
					return this._quotedTableName;
				}
			}

			// Token: 0x06001C74 RID: 7284 RVA: 0x0008D7D2 File Offset: 0x0008B9D2
			internal string GetTable(bool flag)
			{
				if (!flag)
				{
					return this.Table;
				}
				return this.QuotedTable;
			}

			// Token: 0x06001C75 RID: 7285 RVA: 0x0008D7E4 File Offset: 0x0008B9E4
			internal QualifiedTableName(string quoteChar)
			{
				this._quoteChar = quoteChar;
			}

			// Token: 0x06001C76 RID: 7286 RVA: 0x0008D7F4 File Offset: 0x0008B9F4
			internal QualifiedTableName(string quoteChar, string qualifiedname)
			{
				this._quoteChar = quoteChar;
				string[] array = OdbcDataReader.QualifiedTableName.ParseProcedureName(qualifiedname, quoteChar, quoteChar);
				this._catalogName = this.UnQuote(array[1]);
				this._schemaName = this.UnQuote(array[2]);
				this._quotedTableName = array[3];
				this._tableName = this.UnQuote(array[3]);
			}

			// Token: 0x06001C77 RID: 7287 RVA: 0x0008D850 File Offset: 0x0008BA50
			private string UnQuote(string str)
			{
				if (str != null && str.Length > 0)
				{
					char c = this._quoteChar[0];
					if (str[0] == c && str.Length > 1 && str[str.Length - 1] == c)
					{
						str = str.Substring(1, str.Length - 2);
					}
				}
				return str;
			}

			// Token: 0x06001C78 RID: 7288 RVA: 0x0008D8AC File Offset: 0x0008BAAC
			internal static string[] ParseProcedureName(string name, string quotePrefix, string quoteSuffix)
			{
				string[] array = new string[4];
				if (!string.IsNullOrEmpty(name))
				{
					bool flag = !string.IsNullOrEmpty(quotePrefix) && !string.IsNullOrEmpty(quoteSuffix);
					int i = 0;
					int num = 0;
					while (num < array.Length && i < name.Length)
					{
						int num2 = i;
						if (flag && name.IndexOf(quotePrefix, i, quotePrefix.Length, StringComparison.Ordinal) == i)
						{
							for (i += quotePrefix.Length; i < name.Length; i += quoteSuffix.Length)
							{
								i = name.IndexOf(quoteSuffix, i, StringComparison.Ordinal);
								if (i < 0)
								{
									i = name.Length;
									break;
								}
								i += quoteSuffix.Length;
								if (i >= name.Length || name.IndexOf(quoteSuffix, i, quoteSuffix.Length, StringComparison.Ordinal) != i)
								{
									break;
								}
							}
						}
						if (i < name.Length)
						{
							i = name.IndexOf(".", i, StringComparison.Ordinal);
							if (i < 0 || num == array.Length - 1)
							{
								i = name.Length;
							}
						}
						array[num] = name.Substring(num2, i - num2);
						i += ".".Length;
						num++;
					}
					int num3 = array.Length - 1;
					while (0 <= num3)
					{
						array[num3] = ((0 < num) ? array[--num] : null);
						num3--;
					}
				}
				return array;
			}

			// Token: 0x04001525 RID: 5413
			private string _catalogName;

			// Token: 0x04001526 RID: 5414
			private string _schemaName;

			// Token: 0x04001527 RID: 5415
			private string _tableName;

			// Token: 0x04001528 RID: 5416
			private string _quotedTableName;

			// Token: 0x04001529 RID: 5417
			private string _quoteChar;
		}

		// Token: 0x0200029A RID: 666
		private sealed class MetaData
		{
			// Token: 0x0400152A RID: 5418
			internal int ordinal;

			// Token: 0x0400152B RID: 5419
			internal TypeMap typemap;

			// Token: 0x0400152C RID: 5420
			internal SQLLEN size;

			// Token: 0x0400152D RID: 5421
			internal byte precision;

			// Token: 0x0400152E RID: 5422
			internal byte scale;

			// Token: 0x0400152F RID: 5423
			internal bool isAutoIncrement;

			// Token: 0x04001530 RID: 5424
			internal bool isUnique;

			// Token: 0x04001531 RID: 5425
			internal bool isReadOnly;

			// Token: 0x04001532 RID: 5426
			internal bool isNullable;

			// Token: 0x04001533 RID: 5427
			internal bool isRowVersion;

			// Token: 0x04001534 RID: 5428
			internal bool isLong;

			// Token: 0x04001535 RID: 5429
			internal bool isKeyColumn;

			// Token: 0x04001536 RID: 5430
			internal string baseSchemaName;

			// Token: 0x04001537 RID: 5431
			internal string baseCatalogName;

			// Token: 0x04001538 RID: 5432
			internal string baseTableName;

			// Token: 0x04001539 RID: 5433
			internal string baseColumnName;
		}
	}
}
