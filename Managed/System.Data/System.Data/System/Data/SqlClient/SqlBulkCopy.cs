using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace System.Data.SqlClient
{
	/// <summary>Lets you efficiently bulk load a SQL Server table with data from another source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000152 RID: 338
	public sealed class SqlBulkCopy : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> class using the specified open instance of <see cref="T:System.Data.SqlClient.SqlConnection" />. </summary>
		/// <param name="connection">The already open <see cref="T:System.Data.SqlClient.SqlConnection" /> instance that will be used to perform the bulk copy operation. If your connection string does not use Integrated Security = true, you can use <see cref="T:System.Data.SqlClient.SqlCredential" /> to pass the user ID and password more securely than by specifying the user ID and password as text in the connection string.</param>
		// Token: 0x0600107A RID: 4218 RVA: 0x000529F5 File Offset: 0x00050BF5
		public SqlBulkCopy(SqlConnection connection)
		{
			if (connection == null)
			{
				throw ADP.ArgumentNull("connection");
			}
			this._connection = connection;
			this._columnMappings = new SqlBulkCopyColumnMappingCollection();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> class using the supplied existing open instance of <see cref="T:System.Data.SqlClient.SqlConnection" />. The <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> instance behaves according to options supplied in the <paramref name="copyOptions" /> parameter. If a non-null <see cref="T:System.Data.SqlClient.SqlTransaction" /> is supplied, the copy operations will be performed within that transaction.</summary>
		/// <param name="connection">The already open <see cref="T:System.Data.SqlClient.SqlConnection" /> instance that will be used to perform the bulk copy. If your connection string does not use Integrated Security = true, you can use <see cref="T:System.Data.SqlClient.SqlCredential" /> to pass the user ID and password more securely than by specifying the user ID and password as text in the connection string.</param>
		/// <param name="copyOptions">A combination of values from the <see cref="T:System.Data.SqlClient.SqlBulkCopyOptions" />  enumeration that determines which data source rows are copied to the destination table.</param>
		/// <param name="externalTransaction">An existing <see cref="T:System.Data.SqlClient.SqlTransaction" /> instance under which the bulk copy will occur.</param>
		// Token: 0x0600107B RID: 4219 RVA: 0x00052A25 File Offset: 0x00050C25
		public SqlBulkCopy(SqlConnection connection, SqlBulkCopyOptions copyOptions, SqlTransaction externalTransaction)
			: this(connection)
		{
			this._copyOptions = copyOptions;
			if (externalTransaction != null && this.IsCopyOption(SqlBulkCopyOptions.UseInternalTransaction))
			{
				throw SQL.BulkLoadConflictingTransactionOption();
			}
			if (!this.IsCopyOption(SqlBulkCopyOptions.UseInternalTransaction))
			{
				this._externalTransaction = externalTransaction;
			}
		}

		/// <summary>Initializes and opens a new instance of <see cref="T:System.Data.SqlClient.SqlConnection" /> based on the supplied <paramref name="connectionString" />. The constructor uses the <see cref="T:System.Data.SqlClient.SqlConnection" /> to initialize a new instance of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> class.</summary>
		/// <param name="connectionString">The string defining the connection that will be opened for use by the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> instance. If your connection string does not use Integrated Security = true, you can use <see cref="M:System.Data.SqlClient.SqlBulkCopy.#ctor(System.Data.SqlClient.SqlConnection)" /> or <see cref="M:System.Data.SqlClient.SqlBulkCopy.#ctor(System.Data.SqlClient.SqlConnection,System.Data.SqlClient.SqlBulkCopyOptions,System.Data.SqlClient.SqlTransaction)" /> and <see cref="T:System.Data.SqlClient.SqlCredential" /> to pass the user ID and password more securely than by specifying the user ID and password as text in the connection string.</param>
		// Token: 0x0600107C RID: 4220 RVA: 0x00052A59 File Offset: 0x00050C59
		public SqlBulkCopy(string connectionString)
			: this(new SqlConnection(connectionString))
		{
			if (connectionString == null)
			{
				throw ADP.ArgumentNull("connectionString");
			}
			this._connection = new SqlConnection(connectionString);
			this._columnMappings = new SqlBulkCopyColumnMappingCollection();
			this._ownConnection = true;
		}

		/// <summary>Initializes and opens a new instance of <see cref="T:System.Data.SqlClient.SqlConnection" /> based on the supplied <paramref name="connectionString" />. The constructor uses that <see cref="T:System.Data.SqlClient.SqlConnection" /> to initialize a new instance of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> class. The <see cref="T:System.Data.SqlClient.SqlConnection" /> instance behaves according to options supplied in the <paramref name="copyOptions" /> parameter.</summary>
		/// <param name="connectionString">The string defining the connection that will be opened for use by the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> instance. If your connection string does not use Integrated Security = true, you can use <see cref="M:System.Data.SqlClient.SqlBulkCopy.#ctor(System.Data.SqlClient.SqlConnection)" /> or <see cref="M:System.Data.SqlClient.SqlBulkCopy.#ctor(System.Data.SqlClient.SqlConnection,System.Data.SqlClient.SqlBulkCopyOptions,System.Data.SqlClient.SqlTransaction)" /> and <see cref="T:System.Data.SqlClient.SqlCredential" /> to pass the user ID and password more securely than by specifying the user ID and password as text in the connection string.</param>
		/// <param name="copyOptions">A combination of values from the <see cref="T:System.Data.SqlClient.SqlBulkCopyOptions" />  enumeration that determines which data source rows are copied to the destination table.</param>
		// Token: 0x0600107D RID: 4221 RVA: 0x00052A93 File Offset: 0x00050C93
		public SqlBulkCopy(string connectionString, SqlBulkCopyOptions copyOptions)
			: this(connectionString)
		{
			this._copyOptions = copyOptions;
		}

		/// <summary>Number of rows in each batch. At the end of each batch, the rows in the batch are sent to the server.</summary>
		/// <returns>The integer value of the <see cref="P:System.Data.SqlClient.SqlBulkCopy.BatchSize" /> property, or zero if no value has been set.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00052AA3 File Offset: 0x00050CA3
		// (set) Token: 0x0600107F RID: 4223 RVA: 0x00052AAB File Offset: 0x00050CAB
		public int BatchSize
		{
			get
			{
				return this._batchSize;
			}
			set
			{
				if (value >= 0)
				{
					this._batchSize = value;
					return;
				}
				throw ADP.ArgumentOutOfRange("BatchSize");
			}
		}

		/// <summary>Number of seconds for the operation to complete before it times out.</summary>
		/// <returns>The integer value of the <see cref="P:System.Data.SqlClient.SqlBulkCopy.BulkCopyTimeout" /> property. The default is 30 seconds. A value of 0 indicates no limit; the bulk copy will wait indefinitely.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x00052AC3 File Offset: 0x00050CC3
		// (set) Token: 0x06001081 RID: 4225 RVA: 0x00052ACB File Offset: 0x00050CCB
		public int BulkCopyTimeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				if (value < 0)
				{
					throw SQL.BulkLoadInvalidTimeout(value);
				}
				this._timeout = value;
			}
		}

		/// <summary>Enables or disables a <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object to stream data from an <see cref="T:System.Data.IDataReader" /> object</summary>
		/// <returns>true if a <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object can stream data from an <see cref="T:System.Data.IDataReader" /> object; otherwise, false. The default is false.</returns>
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x00052ADF File Offset: 0x00050CDF
		// (set) Token: 0x06001083 RID: 4227 RVA: 0x00052AE7 File Offset: 0x00050CE7
		public bool EnableStreaming
		{
			get
			{
				return this._enableStreaming;
			}
			set
			{
				this._enableStreaming = value;
			}
		}

		/// <summary>Returns a collection of <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> items. Column mappings define the relationships between columns in the data source and columns in the destination.</summary>
		/// <returns>A collection of column mappings. By default, it is an empty collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x00052AF0 File Offset: 0x00050CF0
		public SqlBulkCopyColumnMappingCollection ColumnMappings
		{
			get
			{
				return this._columnMappings;
			}
		}

		/// <summary>Name of the destination table on the server. </summary>
		/// <returns>The string value of the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property, or null if none as been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00052AF8 File Offset: 0x00050CF8
		// (set) Token: 0x06001086 RID: 4230 RVA: 0x00052B00 File Offset: 0x00050D00
		public string DestinationTableName
		{
			get
			{
				return this._destinationTableName;
			}
			set
			{
				if (value == null)
				{
					throw ADP.ArgumentNull("DestinationTableName");
				}
				if (value.Length == 0)
				{
					throw ADP.ArgumentOutOfRange("DestinationTableName");
				}
				this._destinationTableName = value;
			}
		}

		/// <summary>Defines the number of rows to be processed before generating a notification event.</summary>
		/// <returns>The integer value of the <see cref="P:System.Data.SqlClient.SqlBulkCopy.NotifyAfter" /> property, or zero if the property has not been set.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x00052B2A File Offset: 0x00050D2A
		// (set) Token: 0x06001088 RID: 4232 RVA: 0x00052B32 File Offset: 0x00050D32
		public int NotifyAfter
		{
			get
			{
				return this._notifyAfter;
			}
			set
			{
				if (value >= 0)
				{
					this._notifyAfter = value;
					return;
				}
				throw ADP.ArgumentOutOfRange("NotifyAfter");
			}
		}

		/// <summary>Occurs every time that the number of rows specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.NotifyAfter" /> property have been processed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06001089 RID: 4233 RVA: 0x00052B4A File Offset: 0x00050D4A
		// (remove) Token: 0x0600108A RID: 4234 RVA: 0x00052B63 File Offset: 0x00050D63
		public event SqlRowsCopiedEventHandler SqlRowsCopied
		{
			add
			{
				this._rowsCopiedEventHandler = (SqlRowsCopiedEventHandler)Delegate.Combine(this._rowsCopiedEventHandler, value);
			}
			remove
			{
				this._rowsCopiedEventHandler = (SqlRowsCopiedEventHandler)Delegate.Remove(this._rowsCopiedEventHandler, value);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x00052B7C File Offset: 0x00050D7C
		internal SqlStatistics Statistics
		{
			get
			{
				if (this._connection != null && this._connection.StatisticsEnabled)
				{
					return this._connection.Statistics;
				}
				return null;
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> class.</summary>
		// Token: 0x0600108C RID: 4236 RVA: 0x00052BA0 File Offset: 0x00050DA0
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00052BAF File Offset: 0x00050DAF
		private bool IsCopyOption(SqlBulkCopyOptions copyOption)
		{
			return (this._copyOptions & copyOption) == copyOption;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00052BBC File Offset: 0x00050DBC
		private string CreateInitialQuery()
		{
			string[] array;
			try
			{
				array = MultipartIdentifier.ParseMultipartIdentifier(this.DestinationTableName, "[\"", "]\"", "SqlBulkCopy.WriteToServer failed because the SqlBulkCopy.DestinationTableName is an invalid multipart name", true);
			}
			catch (Exception ex)
			{
				throw SQL.BulkLoadInvalidDestinationTable(this.DestinationTableName, ex);
			}
			if (string.IsNullOrEmpty(array[3]))
			{
				throw SQL.BulkLoadInvalidDestinationTable(this.DestinationTableName, null);
			}
			string text = "select @@trancount; SET FMTONLY ON select * from " + this.DestinationTableName + " SET FMTONLY OFF ";
			string text2;
			if (this._connection.IsKatmaiOrNewer)
			{
				text2 = "sp_tablecollations_100";
			}
			else
			{
				text2 = "sp_tablecollations_90";
			}
			string text3 = array[3];
			bool flag = text3.Length > 0 && '#' == text3[0];
			if (!string.IsNullOrEmpty(text3))
			{
				text3 = SqlServerEscapeHelper.EscapeStringAsLiteral(text3);
				text3 = SqlServerEscapeHelper.EscapeIdentifier(text3);
			}
			string text4 = array[2];
			if (!string.IsNullOrEmpty(text4))
			{
				text4 = SqlServerEscapeHelper.EscapeStringAsLiteral(text4);
				text4 = SqlServerEscapeHelper.EscapeIdentifier(text4);
			}
			string text5 = array[1];
			if (flag && string.IsNullOrEmpty(text5))
			{
				text += string.Format(null, "exec tempdb..{0} N'{1}.{2}'", text2, text4, text3);
			}
			else
			{
				if (!string.IsNullOrEmpty(text5))
				{
					text5 = SqlServerEscapeHelper.EscapeIdentifier(text5);
				}
				text += string.Format(null, "exec {0}..{1} N'{2}.{3}'", new object[] { text5, text2, text4, text3 });
			}
			return text;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00052D08 File Offset: 0x00050F08
		private Task<BulkCopySimpleResultSet> CreateAndExecuteInitialQueryAsync(out BulkCopySimpleResultSet result)
		{
			string text = this.CreateInitialQuery();
			Task task = this._parser.TdsExecuteSQLBatch(text, this.BulkCopyTimeout, null, this._stateObj, !this._isAsyncBulkCopy, true);
			if (task == null)
			{
				result = new BulkCopySimpleResultSet();
				this.RunParser(result);
				return null;
			}
			result = null;
			return task.ContinueWith<BulkCopySimpleResultSet>(delegate(Task t)
			{
				if (t.IsFaulted)
				{
					throw t.Exception.InnerException;
				}
				BulkCopySimpleResultSet bulkCopySimpleResultSet = new BulkCopySimpleResultSet();
				this.RunParserReliably(bulkCopySimpleResultSet);
				return bulkCopySimpleResultSet;
			}, TaskScheduler.Default);
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00052D70 File Offset: 0x00050F70
		private string AnalyzeTargetAndCreateUpdateBulkCommand(BulkCopySimpleResultSet internalResults)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (internalResults[2].Count == 0)
			{
				throw SQL.BulkLoadNoCollation();
			}
			stringBuilder.AppendFormat("insert bulk {0} (", this.DestinationTableName);
			int num = 0;
			int num2 = 0;
			if (this._connection.HasLocalTransaction && this._externalTransaction == null && this._internalTransaction == null && this._connection.Parser != null && this._connection.Parser.CurrentTransaction != null && this._connection.Parser.CurrentTransaction.IsLocal)
			{
				throw SQL.BulkLoadExistingTransaction();
			}
			_SqlMetaDataSet metaData = internalResults[1].MetaData;
			this._sortedColumnMappings = new List<_ColumnMapping>(metaData.Length);
			for (int i = 0; i < metaData.Length; i++)
			{
				_SqlMetaData sqlMetaData = metaData[i];
				bool flag = false;
				if (sqlMetaData.type == SqlDbType.Timestamp || (sqlMetaData.isIdentity && !this.IsCopyOption(SqlBulkCopyOptions.KeepIdentity)))
				{
					metaData[i] = null;
					flag = true;
				}
				int j = 0;
				while (j < this._localColumnMappings.Count)
				{
					if (this._localColumnMappings[j]._destinationColumnOrdinal == sqlMetaData.ordinal || this.UnquotedName(this._localColumnMappings[j]._destinationColumnName) == sqlMetaData.column)
					{
						if (flag)
						{
							num2++;
							break;
						}
						this._sortedColumnMappings.Add(new _ColumnMapping(this._localColumnMappings[j]._internalSourceColumnOrdinal, sqlMetaData));
						num++;
						if (num > 1)
						{
							stringBuilder.Append(", ");
						}
						if (sqlMetaData.type == SqlDbType.Variant)
						{
							this.AppendColumnNameAndTypeName(stringBuilder, sqlMetaData.column, "sql_variant");
						}
						else
						{
							if (sqlMetaData.type == SqlDbType.Udt)
							{
								throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
							}
							this.AppendColumnNameAndTypeName(stringBuilder, sqlMetaData.column, sqlMetaData.type.ToString());
						}
						byte b = sqlMetaData.metaType.NullableType;
						if (b <= 106)
						{
							if (b - 41 > 2)
							{
								if (b != 106)
								{
									goto IL_029B;
								}
								goto IL_0217;
							}
							else
							{
								stringBuilder.AppendFormat(null, "({0})", sqlMetaData.scale);
							}
						}
						else
						{
							if (b == 108)
							{
								goto IL_0217;
							}
							if (b != 240)
							{
								goto IL_029B;
							}
							if (sqlMetaData.IsLargeUdt)
							{
								stringBuilder.Append("(max)");
							}
							else
							{
								int length = sqlMetaData.length;
								stringBuilder.AppendFormat(null, "({0})", length);
							}
						}
						IL_032C:
						object obj = internalResults[2][i][3];
						SqlDbType type = sqlMetaData.type;
						if (type <= SqlDbType.NVarChar)
						{
							if (type != SqlDbType.Char && type - SqlDbType.NChar > 2)
							{
								goto IL_0371;
							}
							goto IL_036C;
						}
						else
						{
							if (type == SqlDbType.Text || type == SqlDbType.VarChar)
							{
								goto IL_036C;
							}
							goto IL_0371;
						}
						IL_0374:
						bool flag2;
						if (obj == null || !flag2)
						{
							break;
						}
						SqlString sqlString = (SqlString)obj;
						if (sqlString.IsNull)
						{
							break;
						}
						stringBuilder.Append(" COLLATE " + sqlString.Value);
						if (this._SqlDataReaderRowSource == null || sqlMetaData.collation == null)
						{
							break;
						}
						int internalSourceColumnOrdinal = this._localColumnMappings[j]._internalSourceColumnOrdinal;
						int lcid = sqlMetaData.collation.LCID;
						int localeId = this._SqlDataReaderRowSource.GetLocaleId(internalSourceColumnOrdinal);
						if (localeId != lcid)
						{
							throw SQL.BulkLoadLcidMismatch(localeId, this._SqlDataReaderRowSource.GetName(internalSourceColumnOrdinal), lcid, sqlMetaData.column);
						}
						break;
						IL_0371:
						flag2 = false;
						goto IL_0374;
						IL_036C:
						flag2 = true;
						goto IL_0374;
						IL_0217:
						stringBuilder.AppendFormat(null, "({0},{1})", sqlMetaData.precision, sqlMetaData.scale);
						goto IL_032C;
						IL_029B:
						if (!sqlMetaData.metaType.IsFixed && !sqlMetaData.metaType.IsLong)
						{
							int num3 = sqlMetaData.length;
							b = sqlMetaData.metaType.NullableType;
							if (b == 99 || b == 231 || b == 239)
							{
								num3 /= 2;
							}
							stringBuilder.AppendFormat(null, "({0})", num3);
							goto IL_032C;
						}
						if (sqlMetaData.metaType.IsPlp && sqlMetaData.metaType.SqlDbType != SqlDbType.Xml)
						{
							stringBuilder.Append("(max)");
							goto IL_032C;
						}
						goto IL_032C;
					}
					else
					{
						j++;
					}
				}
				if (j == this._localColumnMappings.Count)
				{
					metaData[i] = null;
				}
			}
			if (num + num2 != this._localColumnMappings.Count)
			{
				throw SQL.BulkLoadNonMatchingColumnMapping();
			}
			stringBuilder.Append(")");
			if ((this._copyOptions & (SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.KeepNulls | SqlBulkCopyOptions.FireTriggers)) != SqlBulkCopyOptions.Default)
			{
				bool flag3 = false;
				stringBuilder.Append(" with (");
				if (this.IsCopyOption(SqlBulkCopyOptions.KeepNulls))
				{
					stringBuilder.Append("KEEP_NULLS");
					flag3 = true;
				}
				if (this.IsCopyOption(SqlBulkCopyOptions.TableLock))
				{
					stringBuilder.Append((flag3 ? ", " : "") + "TABLOCK");
					flag3 = true;
				}
				if (this.IsCopyOption(SqlBulkCopyOptions.CheckConstraints))
				{
					stringBuilder.Append((flag3 ? ", " : "") + "CHECK_CONSTRAINTS");
					flag3 = true;
				}
				if (this.IsCopyOption(SqlBulkCopyOptions.FireTriggers))
				{
					stringBuilder.Append((flag3 ? ", " : "") + "FIRE_TRIGGERS");
				}
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x000532C8 File Offset: 0x000514C8
		private Task SubmitUpdateBulkCommand(string TDSCommand)
		{
			Task task = this._parser.TdsExecuteSQLBatch(TDSCommand, this.BulkCopyTimeout, null, this._stateObj, !this._isAsyncBulkCopy, true);
			if (task == null)
			{
				this.RunParser(null);
				return null;
			}
			return task.ContinueWith(delegate(Task t)
			{
				if (t.IsFaulted)
				{
					throw t.Exception.InnerException;
				}
				this.RunParserReliably(null);
			}, TaskScheduler.Default);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0005331C File Offset: 0x0005151C
		private void WriteMetaData(BulkCopySimpleResultSet internalResults)
		{
			this._stateObj.SetTimeoutSeconds(this.BulkCopyTimeout);
			_SqlMetaDataSet metaData = internalResults[1].MetaData;
			this._stateObj._outputMessageType = 7;
			this._parser.WriteBulkCopyMetaData(metaData, this._sortedColumnMappings.Count, this._stateObj);
		}

		/// <summary>Closes the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> instance.</summary>
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
		// Token: 0x06001093 RID: 4243 RVA: 0x00053370 File Offset: 0x00051570
		public void Close()
		{
			if (this._insideRowsCopiedEvent)
			{
				throw SQL.InvalidOperationInsideEvent();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00053390 File Offset: 0x00051590
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._columnMappings = null;
				this._parser = null;
				try
				{
					if (this._internalTransaction != null)
					{
						this._internalTransaction.Rollback();
						this._internalTransaction.Dispose();
						this._internalTransaction = null;
					}
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableExceptionType(ex))
					{
						throw;
					}
				}
				finally
				{
					if (this._connection != null)
					{
						if (this._ownConnection)
						{
							this._connection.Dispose();
						}
						this._connection = null;
					}
				}
			}
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00053420 File Offset: 0x00051620
		private object GetValueFromSourceRow(int destRowIndex, out bool isSqlType, out bool isDataFeed, out bool isNull)
		{
			_SqlMetaData metadata = this._sortedColumnMappings[destRowIndex]._metadata;
			int sourceColumnOrdinal = this._sortedColumnMappings[destRowIndex]._sourceColumnOrdinal;
			switch (this._rowSourceType)
			{
			case SqlBulkCopy.ValueSourceType.IDataReader:
			case SqlBulkCopy.ValueSourceType.DbDataReader:
				if (this._currentRowMetadata[destRowIndex].IsDataFeed)
				{
					if (this._DbDataReaderRowSource.IsDBNull(sourceColumnOrdinal))
					{
						isSqlType = false;
						isDataFeed = false;
						isNull = true;
						return DBNull.Value;
					}
					isSqlType = false;
					isDataFeed = true;
					isNull = false;
					switch (this._currentRowMetadata[destRowIndex].Method)
					{
					case SqlBulkCopy.ValueMethod.DataFeedStream:
						return new StreamDataFeed(this._DbDataReaderRowSource.GetStream(sourceColumnOrdinal));
					case SqlBulkCopy.ValueMethod.DataFeedText:
						return new TextDataFeed(this._DbDataReaderRowSource.GetTextReader(sourceColumnOrdinal));
					case SqlBulkCopy.ValueMethod.DataFeedXml:
						return new XmlDataFeed(this._SqlDataReaderRowSource.GetXmlReader(sourceColumnOrdinal));
					default:
					{
						isDataFeed = false;
						object value = this._DbDataReaderRowSource.GetValue(sourceColumnOrdinal);
						ADP.IsNullOrSqlType(value, out isNull, out isSqlType);
						return value;
					}
					}
				}
				else if (this._SqlDataReaderRowSource != null)
				{
					if (this._currentRowMetadata[destRowIndex].IsSqlType)
					{
						isSqlType = true;
						isDataFeed = false;
						INullable nullable;
						switch (this._currentRowMetadata[destRowIndex].Method)
						{
						case SqlBulkCopy.ValueMethod.SqlTypeSqlDecimal:
							nullable = this._SqlDataReaderRowSource.GetSqlDecimal(sourceColumnOrdinal);
							break;
						case SqlBulkCopy.ValueMethod.SqlTypeSqlDouble:
							nullable = new SqlDecimal(this._SqlDataReaderRowSource.GetSqlDouble(sourceColumnOrdinal).Value);
							break;
						case SqlBulkCopy.ValueMethod.SqlTypeSqlSingle:
							nullable = new SqlDecimal((double)this._SqlDataReaderRowSource.GetSqlSingle(sourceColumnOrdinal).Value);
							break;
						default:
							nullable = (INullable)this._SqlDataReaderRowSource.GetSqlValue(sourceColumnOrdinal);
							break;
						}
						isNull = nullable.IsNull;
						return nullable;
					}
					isSqlType = false;
					isDataFeed = false;
					object value2 = this._SqlDataReaderRowSource.GetValue(sourceColumnOrdinal);
					isNull = value2 == null || value2 == DBNull.Value;
					if (!isNull && metadata.type == SqlDbType.Udt)
					{
						INullable nullable2 = value2 as INullable;
						isNull = nullable2 != null && nullable2.IsNull;
					}
					return value2;
				}
				else
				{
					isDataFeed = false;
					IDataReader dataReader = (IDataReader)this._rowSource;
					if (this._enableStreaming && this._SqlDataReaderRowSource == null && dataReader.IsDBNull(sourceColumnOrdinal))
					{
						isSqlType = false;
						isNull = true;
						return DBNull.Value;
					}
					object value3 = dataReader.GetValue(sourceColumnOrdinal);
					ADP.IsNullOrSqlType(value3, out isNull, out isSqlType);
					return value3;
				}
				break;
			case SqlBulkCopy.ValueSourceType.DataTable:
			case SqlBulkCopy.ValueSourceType.RowArray:
			{
				isDataFeed = false;
				object obj = this._currentRow[sourceColumnOrdinal];
				ADP.IsNullOrSqlType(obj, out isNull, out isSqlType);
				if (!isNull && this._currentRowMetadata[destRowIndex].IsSqlType)
				{
					switch (this._currentRowMetadata[destRowIndex].Method)
					{
					case SqlBulkCopy.ValueMethod.SqlTypeSqlDecimal:
						if (isSqlType)
						{
							return (SqlDecimal)obj;
						}
						isSqlType = true;
						return new SqlDecimal((decimal)obj);
					case SqlBulkCopy.ValueMethod.SqlTypeSqlDouble:
					{
						if (isSqlType)
						{
							return new SqlDecimal(((SqlDouble)obj).Value);
						}
						double num = (double)obj;
						if (!double.IsNaN(num))
						{
							isSqlType = true;
							return new SqlDecimal(num);
						}
						break;
					}
					case SqlBulkCopy.ValueMethod.SqlTypeSqlSingle:
					{
						if (isSqlType)
						{
							return new SqlDecimal((double)((SqlSingle)obj).Value);
						}
						float num2 = (float)obj;
						if (!float.IsNaN(num2))
						{
							isSqlType = true;
							return new SqlDecimal((double)num2);
						}
						break;
					}
					}
				}
				return obj;
			}
			default:
				throw ADP.NotSupported();
			}
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x000537A8 File Offset: 0x000519A8
		private Task ReadFromRowSourceAsync(CancellationToken cts)
		{
			if (this._isAsyncBulkCopy && this._DbDataReaderRowSource != null)
			{
				return this._DbDataReaderRowSource.ReadAsync(cts).ContinueWith<Task<bool>>(delegate(Task<bool> t)
				{
					if (t.Status == TaskStatus.RanToCompletion)
					{
						this._hasMoreRowToCopy = t.Result;
					}
					return t;
				}, TaskScheduler.Default).Unwrap<bool>();
			}
			this._hasMoreRowToCopy = false;
			try
			{
				this._hasMoreRowToCopy = this.ReadFromRowSource();
			}
			catch (Exception ex)
			{
				if (this._isAsyncBulkCopy)
				{
					return Task.FromException<bool>(ex);
				}
				throw;
			}
			return null;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0005382C File Offset: 0x00051A2C
		private bool ReadFromRowSource()
		{
			switch (this._rowSourceType)
			{
			case SqlBulkCopy.ValueSourceType.IDataReader:
			case SqlBulkCopy.ValueSourceType.DbDataReader:
				return ((IDataReader)this._rowSource).Read();
			case SqlBulkCopy.ValueSourceType.DataTable:
			case SqlBulkCopy.ValueSourceType.RowArray:
				while (this._rowEnumerator.MoveNext())
				{
					this._currentRow = (DataRow)this._rowEnumerator.Current;
					if ((this._currentRow.RowState & this._rowStateToSkip) == (DataRowState)0)
					{
						this._currentRowLength = this._currentRow.ItemArray.Length;
						return true;
					}
				}
				return false;
			default:
				throw ADP.NotSupported();
			}
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000538C0 File Offset: 0x00051AC0
		private SqlBulkCopy.SourceColumnMetadata GetColumnMetadata(int ordinal)
		{
			int sourceColumnOrdinal = this._sortedColumnMappings[ordinal]._sourceColumnOrdinal;
			_SqlMetaData metadata = this._sortedColumnMappings[ordinal]._metadata;
			bool flag;
			bool flag2;
			SqlBulkCopy.ValueMethod valueMethod;
			if ((this._SqlDataReaderRowSource != null || this._dataTableSource != null) && (metadata.metaType.NullableType == 106 || metadata.metaType.NullableType == 108))
			{
				flag = false;
				Type type;
				switch (this._rowSourceType)
				{
				case SqlBulkCopy.ValueSourceType.IDataReader:
				case SqlBulkCopy.ValueSourceType.DbDataReader:
					type = this._SqlDataReaderRowSource.GetFieldType(sourceColumnOrdinal);
					break;
				case SqlBulkCopy.ValueSourceType.DataTable:
				case SqlBulkCopy.ValueSourceType.RowArray:
					type = this._dataTableSource.Columns[sourceColumnOrdinal].DataType;
					break;
				default:
					type = null;
					break;
				}
				if (typeof(SqlDecimal) == type || typeof(decimal) == type)
				{
					flag2 = true;
					valueMethod = SqlBulkCopy.ValueMethod.SqlTypeSqlDecimal;
				}
				else if (typeof(SqlDouble) == type || typeof(double) == type)
				{
					flag2 = true;
					valueMethod = SqlBulkCopy.ValueMethod.SqlTypeSqlDouble;
				}
				else if (typeof(SqlSingle) == type || typeof(float) == type)
				{
					flag2 = true;
					valueMethod = SqlBulkCopy.ValueMethod.SqlTypeSqlSingle;
				}
				else
				{
					flag2 = false;
					valueMethod = SqlBulkCopy.ValueMethod.GetValue;
				}
			}
			else if (this._enableStreaming && metadata.length == 2147483647)
			{
				flag2 = false;
				if (this._SqlDataReaderRowSource != null)
				{
					MetaType metaType = this._SqlDataReaderRowSource.MetaData[sourceColumnOrdinal].metaType;
					if (metadata.type == SqlDbType.VarBinary && metaType.IsBinType && metaType.SqlDbType != SqlDbType.Timestamp && this._SqlDataReaderRowSource.IsCommandBehavior(CommandBehavior.SequentialAccess))
					{
						flag = true;
						valueMethod = SqlBulkCopy.ValueMethod.DataFeedStream;
					}
					else if ((metadata.type == SqlDbType.VarChar || metadata.type == SqlDbType.NVarChar) && metaType.IsCharType && metaType.SqlDbType != SqlDbType.Xml)
					{
						flag = true;
						valueMethod = SqlBulkCopy.ValueMethod.DataFeedText;
					}
					else if (metadata.type == SqlDbType.Xml && metaType.SqlDbType == SqlDbType.Xml)
					{
						flag = true;
						valueMethod = SqlBulkCopy.ValueMethod.DataFeedXml;
					}
					else
					{
						flag = false;
						valueMethod = SqlBulkCopy.ValueMethod.GetValue;
					}
				}
				else if (this._DbDataReaderRowSource != null)
				{
					if (metadata.type == SqlDbType.VarBinary)
					{
						flag = true;
						valueMethod = SqlBulkCopy.ValueMethod.DataFeedStream;
					}
					else if (metadata.type == SqlDbType.VarChar || metadata.type == SqlDbType.NVarChar)
					{
						flag = true;
						valueMethod = SqlBulkCopy.ValueMethod.DataFeedText;
					}
					else
					{
						flag = false;
						valueMethod = SqlBulkCopy.ValueMethod.GetValue;
					}
				}
				else
				{
					flag = false;
					valueMethod = SqlBulkCopy.ValueMethod.GetValue;
				}
			}
			else
			{
				flag2 = false;
				flag = false;
				valueMethod = SqlBulkCopy.ValueMethod.GetValue;
			}
			return new SqlBulkCopy.SourceColumnMetadata(valueMethod, flag2, flag);
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00053B2C File Offset: 0x00051D2C
		private void CreateOrValidateConnection(string method)
		{
			if (this._connection == null)
			{
				throw ADP.ConnectionRequired(method);
			}
			if (this._ownConnection && this._connection.State != ConnectionState.Open)
			{
				this._connection.Open();
			}
			this._connection.ValidateConnectionForExecute(method, null);
			if (this._externalTransaction != null && this._connection != this._externalTransaction.Connection)
			{
				throw ADP.TransactionConnectionMismatch();
			}
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00053B98 File Offset: 0x00051D98
		private void RunParser(BulkCopySimpleResultSet bulkCopyHandler = null)
		{
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			openTdsConnection.ThreadHasParserLockForClose = true;
			try
			{
				this._parser.Run(RunBehavior.UntilDone, null, null, bulkCopyHandler, this._stateObj);
			}
			finally
			{
				openTdsConnection.ThreadHasParserLockForClose = false;
			}
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00053BE8 File Offset: 0x00051DE8
		private void RunParserReliably(BulkCopySimpleResultSet bulkCopyHandler = null)
		{
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			openTdsConnection.ThreadHasParserLockForClose = true;
			try
			{
				this._parser.Run(RunBehavior.UntilDone, null, null, bulkCopyHandler, this._stateObj);
			}
			finally
			{
				openTdsConnection.ThreadHasParserLockForClose = false;
			}
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00053C38 File Offset: 0x00051E38
		private void CommitTransaction()
		{
			if (this._internalTransaction != null)
			{
				SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
				openTdsConnection.ThreadHasParserLockForClose = true;
				try
				{
					this._internalTransaction.Commit();
					this._internalTransaction.Dispose();
					this._internalTransaction = null;
				}
				finally
				{
					openTdsConnection.ThreadHasParserLockForClose = false;
				}
			}
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00053C98 File Offset: 0x00051E98
		private void AbortTransaction()
		{
			if (this._internalTransaction != null)
			{
				if (!this._internalTransaction.IsZombied)
				{
					SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
					openTdsConnection.ThreadHasParserLockForClose = true;
					try
					{
						this._internalTransaction.Rollback();
					}
					finally
					{
						openTdsConnection.ThreadHasParserLockForClose = false;
					}
				}
				this._internalTransaction.Dispose();
				this._internalTransaction = null;
			}
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00053D04 File Offset: 0x00051F04
		private void AppendColumnNameAndTypeName(StringBuilder query, string columnName, string typeName)
		{
			SqlServerEscapeHelper.EscapeIdentifier(query, columnName);
			query.Append(" ");
			query.Append(typeName);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00053D24 File Offset: 0x00051F24
		private string UnquotedName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			if (name[0] == '[')
			{
				int length = name.Length;
				name = name.Substring(1, length - 2);
			}
			return name;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00053D5C File Offset: 0x00051F5C
		private object ValidateBulkCopyVariant(object value)
		{
			byte tdstype = MetaType.GetMetaTypeFromValue(value, true, true).TDSType;
			if (tdstype <= 108)
			{
				if (tdstype <= 43)
				{
					if (tdstype != 36 && tdstype - 40 > 3)
					{
						goto IL_00AD;
					}
				}
				else
				{
					switch (tdstype)
					{
					case 48:
					case 50:
					case 52:
					case 56:
					case 59:
					case 60:
					case 61:
					case 62:
						break;
					case 49:
					case 51:
					case 53:
					case 54:
					case 55:
					case 57:
					case 58:
						goto IL_00AD;
					default:
						if (tdstype != 108)
						{
							goto IL_00AD;
						}
						break;
					}
				}
			}
			else if (tdstype <= 165)
			{
				if (tdstype != 127 && tdstype != 165)
				{
					goto IL_00AD;
				}
			}
			else if (tdstype != 167 && tdstype != 231)
			{
				goto IL_00AD;
			}
			if (value is INullable)
			{
				return MetaType.GetComValueFromSqlVariant(value);
			}
			return value;
			IL_00AD:
			throw SQL.BulkLoadInvalidVariantValue();
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00053E1C File Offset: 0x0005201C
		private object ConvertValue(object value, _SqlMetaData metadata, bool isNull, ref bool isSqlType, out bool coercedToDataFeed)
		{
			coercedToDataFeed = false;
			if (!isNull)
			{
				MetaType metaType = metadata.metaType;
				bool flag = false;
				object obj;
				try
				{
					byte nullableType = metaType.NullableType;
					MetaType metaType2;
					if (nullableType <= 165)
					{
						if (nullableType <= 59)
						{
							switch (nullableType)
							{
							case 34:
							case 35:
							case 36:
							case 38:
							case 40:
							case 41:
							case 42:
							case 43:
							case 50:
								break;
							case 37:
							case 39:
							case 44:
							case 45:
							case 46:
							case 47:
							case 48:
							case 49:
								goto IL_02A3;
							default:
								if (nullableType - 58 > 1)
								{
									goto IL_02A3;
								}
								break;
							}
						}
						else if (nullableType - 61 > 1)
						{
							switch (nullableType)
							{
							case 98:
								value = this.ValidateBulkCopyVariant(value);
								flag = true;
								goto IL_02B6;
							case 99:
								goto IL_0212;
							case 100:
							case 101:
							case 102:
							case 103:
							case 105:
							case 107:
								goto IL_02A3;
							case 104:
							case 109:
							case 110:
							case 111:
								break;
							case 106:
							case 108:
							{
								metaType2 = MetaType.GetMetaTypeFromSqlDbType(metaType.SqlDbType, false);
								value = SqlParameter.CoerceValue(value, metaType2, out coercedToDataFeed, out flag, false);
								SqlDecimal sqlDecimal;
								if (isSqlType && !flag)
								{
									sqlDecimal = (SqlDecimal)value;
								}
								else
								{
									sqlDecimal = new SqlDecimal((decimal)value);
								}
								if (sqlDecimal.Scale != metadata.scale)
								{
									sqlDecimal = TdsParser.AdjustSqlDecimalScale(sqlDecimal, (int)metadata.scale);
								}
								if (sqlDecimal.Precision > metadata.precision)
								{
									try
									{
										sqlDecimal = SqlDecimal.ConvertToPrecScale(sqlDecimal, (int)metadata.precision, (int)sqlDecimal.Scale);
									}
									catch (SqlTruncateException)
									{
										throw SQL.BulkLoadCannotConvertValue(value.GetType(), metaType2, ADP.ParameterValueOutOfRange(sqlDecimal));
									}
								}
								value = sqlDecimal;
								isSqlType = true;
								flag = false;
								goto IL_02B6;
							}
							default:
								if (nullableType != 165)
								{
									goto IL_02A3;
								}
								break;
							}
						}
					}
					else if (nullableType <= 173)
					{
						if (nullableType != 167 && nullableType != 173)
						{
							goto IL_02A3;
						}
					}
					else if (nullableType != 175)
					{
						if (nullableType == 231)
						{
							goto IL_0212;
						}
						switch (nullableType)
						{
						case 239:
							goto IL_0212;
						case 240:
							throw ADP.DbTypeNotSupported("UDT");
						case 241:
							if (value is XmlReader)
							{
								value = new XmlDataFeed((XmlReader)value);
								flag = true;
								coercedToDataFeed = true;
								goto IL_02B6;
							}
							goto IL_02B6;
						default:
							goto IL_02A3;
						}
					}
					metaType2 = MetaType.GetMetaTypeFromSqlDbType(metaType.SqlDbType, false);
					value = SqlParameter.CoerceValue(value, metaType2, out coercedToDataFeed, out flag, false);
					goto IL_02B6;
					IL_0212:
					metaType2 = MetaType.GetMetaTypeFromSqlDbType(metaType.SqlDbType, false);
					value = SqlParameter.CoerceValue(value, metaType2, out coercedToDataFeed, out flag, false);
					if (!coercedToDataFeed && ((isSqlType && !flag) ? ((SqlString)value).Value.Length : ((string)value).Length) > metadata.length / 2)
					{
						throw SQL.BulkLoadStringTooLong();
					}
					goto IL_02B6;
					IL_02A3:
					throw SQL.BulkLoadCannotConvertValue(value.GetType(), metadata.metaType, null);
					IL_02B6:
					if (flag)
					{
						isSqlType = false;
					}
					obj = value;
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableExceptionType(ex))
					{
						throw;
					}
					throw SQL.BulkLoadCannotConvertValue(value.GetType(), metadata.metaType, ex);
				}
				return obj;
			}
			if (!metadata.isNullable)
			{
				throw SQL.BulkLoadBulkLoadNotAllowDBNull(metadata.column);
			}
			return value;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00054144 File Offset: 0x00052344
		public void WriteToServer(DbDataReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics sqlStatistics = this.Statistics;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowSource = reader;
				this._DbDataReaderRowSource = reader;
				this._SqlDataReaderRowSource = reader as SqlDataReader;
				this._dataTableSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.DbDataReader;
				this._isAsyncBulkCopy = false;
				this.WriteRowSourceToServerAsync(reader.FieldCount, CancellationToken.None);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		/// <summary>Copies all rows in the supplied <see cref="T:System.Data.IDataReader" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="reader">A <see cref="T:System.Data.IDataReader" /> whose rows will be copied to the destination table.</param>
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
		// Token: 0x060010A3 RID: 4259 RVA: 0x000541DC File Offset: 0x000523DC
		public void WriteToServer(IDataReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics sqlStatistics = this.Statistics;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowSource = reader;
				this._SqlDataReaderRowSource = this._rowSource as SqlDataReader;
				this._DbDataReaderRowSource = this._rowSource as DbDataReader;
				this._dataTableSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.IDataReader;
				this._isAsyncBulkCopy = false;
				this.WriteRowSourceToServerAsync(reader.FieldCount, CancellationToken.None);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		/// <summary>Copies all rows in the supplied <see cref="T:System.Data.DataTable" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> whose rows will be copied to the destination table.</param>
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
		// Token: 0x060010A4 RID: 4260 RVA: 0x00054284 File Offset: 0x00052484
		public void WriteToServer(DataTable table)
		{
			this.WriteToServer(table, (DataRowState)0);
		}

		/// <summary>Copies only rows that match the supplied row state in the supplied <see cref="T:System.Data.DataTable" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> whose rows will be copied to the destination table.</param>
		/// <param name="rowState">A value from the <see cref="T:System.Data.DataRowState" /> enumeration. Only rows matching the row state are copied to the destination.</param>
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
		// Token: 0x060010A5 RID: 4261 RVA: 0x00054290 File Offset: 0x00052490
		public void WriteToServer(DataTable table, DataRowState rowState)
		{
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics sqlStatistics = this.Statistics;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowStateToSkip = ((rowState == (DataRowState)0 || rowState == DataRowState.Deleted) ? DataRowState.Deleted : (~rowState | DataRowState.Deleted));
				this._rowSource = table;
				this._dataTableSource = table;
				this._SqlDataReaderRowSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.DataTable;
				this._rowEnumerator = table.Rows.GetEnumerator();
				this._isAsyncBulkCopy = false;
				this.WriteRowSourceToServerAsync(table.Columns.Count, CancellationToken.None);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		/// <summary>Copies all rows from the supplied <see cref="T:System.Data.DataRow" /> array to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="rows">An array of <see cref="T:System.Data.DataRow" /> objects that will be copied to the destination table.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010A6 RID: 4262 RVA: 0x00054344 File Offset: 0x00052544
		public void WriteToServer(DataRow[] rows)
		{
			SqlStatistics sqlStatistics = this.Statistics;
			if (rows == null)
			{
				throw new ArgumentNullException("rows");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			if (rows.Length == 0)
			{
				return;
			}
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				DataTable table = rows[0].Table;
				this._rowStateToSkip = DataRowState.Deleted;
				this._rowSource = rows;
				this._dataTableSource = table;
				this._SqlDataReaderRowSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.RowArray;
				this._rowEnumerator = rows.GetEnumerator();
				this._isAsyncBulkCopy = false;
				this.WriteRowSourceToServerAsync(table.Columns.Count, CancellationToken.None);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataRow[])" />, which copies all rows from the supplied <see cref="T:System.Data.DataRow" /> array to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="rows">An array of <see cref="T:System.Data.DataRow" /> objects that will be copied to the destination table.</param>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataRow[])" /> multiple times for the same instance before task completion.Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataRow[])" /> and <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataRow[])" /> for the same instance before task completion.The connection drops or is closed during <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataRow[])" /> execution.Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object was closed during the method execution.Returned in the task object, there was a connection pool timeout.Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlConnection" /> object is closed before method execution.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Returned in the task object, any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x060010A7 RID: 4263 RVA: 0x000543F8 File Offset: 0x000525F8
		public Task WriteToServerAsync(DataRow[] rows)
		{
			return this.WriteToServerAsync(rows, CancellationToken.None);
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataRow[])" />, which copies all rows from the supplied <see cref="T:System.Data.DataRow" /> array to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.The cancellation token can be used to request that the operation be abandoned before the command timeout elapses.  Exceptions will be reported via the returned Task object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="rows">An array of <see cref="T:System.Data.DataRow" /> objects that will be copied to the destination table.</param>
		/// <param name="cancellationToken">The cancellation instruction. A <see cref="P:System.Threading.CancellationToken.None" /> value in this parameter makes this method equivalent to <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" />.</param>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataRow[])" /> multiple times for the same instance before task completion.Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataRow[])" /> and <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataRow[])" /> for the same instance before task completion.The connection drops or is closed during <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataRow[])" /> execution.Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object was closed during the method execution.Returned in the task object, there was a connection pool timeout.Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlConnection" /> object is closed before method execution.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Returned in the task object, any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x060010A8 RID: 4264 RVA: 0x00054408 File Offset: 0x00052608
		public Task WriteToServerAsync(DataRow[] rows, CancellationToken cancellationToken)
		{
			Task task = null;
			if (rows == null)
			{
				throw new ArgumentNullException("rows");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics sqlStatistics = this.Statistics;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				if (rows.Length == 0)
				{
					return cancellationToken.IsCancellationRequested ? Task.FromCanceled(cancellationToken) : Task.CompletedTask;
				}
				DataTable table = rows[0].Table;
				this._rowStateToSkip = DataRowState.Deleted;
				this._rowSource = rows;
				this._dataTableSource = table;
				this._SqlDataReaderRowSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.RowArray;
				this._rowEnumerator = rows.GetEnumerator();
				this._isAsyncBulkCopy = true;
				task = this.WriteRowSourceToServerAsync(table.Columns.Count, cancellationToken);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return task;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x000544D4 File Offset: 0x000526D4
		public Task WriteToServerAsync(DbDataReader reader)
		{
			return this.WriteToServerAsync(reader, CancellationToken.None);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x000544E4 File Offset: 0x000526E4
		public Task WriteToServerAsync(DbDataReader reader, CancellationToken cancellationToken)
		{
			Task task = null;
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics sqlStatistics = this.Statistics;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowSource = reader;
				this._SqlDataReaderRowSource = reader as SqlDataReader;
				this._DbDataReaderRowSource = reader;
				this._dataTableSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.DbDataReader;
				this._isAsyncBulkCopy = true;
				task = this.WriteRowSourceToServerAsync(reader.FieldCount, cancellationToken);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return task;
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.IDataReader)" />, which copies all rows in the supplied <see cref="T:System.Data.IDataReader" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="reader">A <see cref="T:System.Data.IDataReader" /> whose rows will be copied to the destination table.</param>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.IDataReader)" /> multiple times for the same instance before task completion.Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.IDataReader)" /> and <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.IDataReader)" /> for the same instance before task completion.The connection drops or is closed during <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.IDataReader)" /> execution.Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object was closed during the method execution.Returned in the task object, there was a connection pool timeout.Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlConnection" /> object is closed before method execution.The <see cref="T:System.Data.IDataReader" /> was closed before the completed <see cref="T:System.Threading.Tasks.Task" /> returned.The <see cref="T:System.Data.IDataReader" />'s associated connection was closed before the completed <see cref="T:System.Threading.Tasks.Task" /> returned.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Returned in the task object, any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x060010AB RID: 4267 RVA: 0x0005457C File Offset: 0x0005277C
		public Task WriteToServerAsync(IDataReader reader)
		{
			return this.WriteToServerAsync(reader, CancellationToken.None);
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.IDataReader)" />, which copies all rows in the supplied <see cref="T:System.Data.IDataReader" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.The cancellation token can be used to request that the operation be abandoned before the command timeout elapses.  Exceptions will be reported via the returned Task object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="reader">A <see cref="T:System.Data.IDataReader" /> whose rows will be copied to the destination table.</param>
		/// <param name="cancellationToken">The cancellation instruction. A <see cref="P:System.Threading.CancellationToken.None" /> value in this parameter makes this method equivalent to <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" />.</param>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.IDataReader)" /> multiple times for the same instance before task completion.Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.IDataReader)" /> and <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.IDataReader)" /> for the same instance before task completion.The connection drops or is closed during <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.IDataReader)" /> execution.Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object was closed during the method execution.Returned in the task object, there was a connection pool timeout.Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlConnection" /> object is closed before method execution.The <see cref="T:System.Data.IDataReader" /> was closed before the completed <see cref="T:System.Threading.Tasks.Task" /> returned.The <see cref="T:System.Data.IDataReader" />'s associated connection was closed before the completed <see cref="T:System.Threading.Tasks.Task" /> returned.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Returned in the task object, any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x060010AC RID: 4268 RVA: 0x0005458C File Offset: 0x0005278C
		public Task WriteToServerAsync(IDataReader reader, CancellationToken cancellationToken)
		{
			Task task = null;
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics sqlStatistics = this.Statistics;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowSource = reader;
				this._SqlDataReaderRowSource = this._rowSource as SqlDataReader;
				this._DbDataReaderRowSource = this._rowSource as DbDataReader;
				this._dataTableSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.IDataReader;
				this._isAsyncBulkCopy = true;
				task = this.WriteRowSourceToServerAsync(reader.FieldCount, cancellationToken);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return task;
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataTable)" />, which copies all rows in the supplied <see cref="T:System.Data.DataTable" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> whose rows will be copied to the destination table.</param>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" /> multiple times for the same instance before task completion.Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" /> and <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataTable)" /> for the same instance before task completion.The connection drops or is closed during <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" /> execution.Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object was closed during the method execution.Returned in the task object, there was a connection pool timeout.Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlConnection" /> object is closed before method execution.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Returned in the task object, any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x060010AD RID: 4269 RVA: 0x00054630 File Offset: 0x00052830
		public Task WriteToServerAsync(DataTable table)
		{
			return this.WriteToServerAsync(table, (DataRowState)0, CancellationToken.None);
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataTable)" />, which copies all rows in the supplied <see cref="T:System.Data.DataTable" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.The cancellation token can be used to request that the operation be abandoned before the command timeout elapses.  Exceptions will be reported via the returned Task object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> whose rows will be copied to the destination table.</param>
		/// <param name="cancellationToken">The cancellation instruction. A <see cref="P:System.Threading.CancellationToken.None" /> value in this parameter makes this method equivalent to <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" />.</param>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" /> multiple times for the same instance before task completion.Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" /> and <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataTable)" /> for the same instance before task completion.The connection drops or is closed during <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" /> execution.Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object was closed during the method execution.Returned in the task object, there was a connection pool timeout.Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlConnection" /> object is closed before method execution.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Returned in the task object, any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x060010AE RID: 4270 RVA: 0x0005463F File Offset: 0x0005283F
		public Task WriteToServerAsync(DataTable table, CancellationToken cancellationToken)
		{
			return this.WriteToServerAsync(table, (DataRowState)0, cancellationToken);
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataTable,System.Data.DataRowState)" />, which copies only rows that match the supplied row state in the supplied <see cref="T:System.Data.DataTable" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> whose rows will be copied to the destination table.</param>
		/// <param name="rowState">A value from the <see cref="T:System.Data.DataRowState" /> enumeration. Only rows matching the row state are copied to the destination.</param>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable,System.Data.DataRowState)" /> multiple times for the same instance before task completion.Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable,System.Data.DataRowState)" /> and <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataTable,System.Data.DataRowState)" /> for the same instance before task completion.The connection drops or is closed during <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable,System.Data.DataRowState)" /> execution.Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object was closed during the method execution.Returned in the task object, there was a connection pool timeout.Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlConnection" /> object is closed before method execution.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Returned in the task object, any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x060010AF RID: 4271 RVA: 0x0005464A File Offset: 0x0005284A
		public Task WriteToServerAsync(DataTable table, DataRowState rowState)
		{
			return this.WriteToServerAsync(table, rowState, CancellationToken.None);
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataTable,System.Data.DataRowState)" />, which copies only rows that match the supplied row state in the supplied <see cref="T:System.Data.DataTable" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.The cancellation token can be used to request that the operation be abandoned before the command timeout elapses.  Exceptions will be reported via the returned Task object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> whose rows will be copied to the destination table.</param>
		/// <param name="rowState">A value from the <see cref="T:System.Data.DataRowState" /> enumeration. Only rows matching the row state are copied to the destination.</param>
		/// <param name="cancellationToken">The cancellation instruction. A <see cref="P:System.Threading.CancellationToken.None" /> value in this parameter makes this method equivalent to <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" />.</param>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable,System.Data.DataRowState)" /> multiple times for the same instance before task completion.Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable,System.Data.DataRowState)" /> and <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataTable,System.Data.DataRowState)" /> for the same instance before task completion.The connection drops or is closed during <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable,System.Data.DataRowState)" /> execution.Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object was closed during the method execution.Returned in the task object, there was a connection pool timeout.Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlConnection" /> object is closed before method execution.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Returned in the task object, any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x060010B0 RID: 4272 RVA: 0x0005465C File Offset: 0x0005285C
		public Task WriteToServerAsync(DataTable table, DataRowState rowState, CancellationToken cancellationToken)
		{
			Task task = null;
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics sqlStatistics = this.Statistics;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowStateToSkip = ((rowState == (DataRowState)0 || rowState == DataRowState.Deleted) ? DataRowState.Deleted : (~rowState | DataRowState.Deleted));
				this._rowSource = table;
				this._SqlDataReaderRowSource = null;
				this._dataTableSource = table;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.DataTable;
				this._rowEnumerator = table.Rows.GetEnumerator();
				this._isAsyncBulkCopy = true;
				task = this.WriteRowSourceToServerAsync(table.Columns.Count, cancellationToken);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return task;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00054710 File Offset: 0x00052910
		private Task WriteRowSourceToServerAsync(int columnCount, CancellationToken ctoken)
		{
			Task currentReconnectionTask = this._connection._currentReconnectionTask;
			if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
			{
				if (this._isAsyncBulkCopy)
				{
					TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
					Action <>9__2;
					currentReconnectionTask.ContinueWith(delegate(Task t)
					{
						Task task3 = this.WriteRowSourceToServerAsync(columnCount, ctoken);
						if (task3 == null)
						{
							tcs.SetResult(null);
							return;
						}
						Task task4 = task3;
						TaskCompletionSource<object> tcs2 = tcs;
						Action action;
						if ((action = <>9__2) == null)
						{
							action = (<>9__2 = delegate
							{
								tcs.SetResult(null);
							});
						}
						AsyncHelper.ContinueTask(task4, tcs2, action, null, null, null, null, null);
					}, ctoken);
					return tcs.Task;
				}
				AsyncHelper.WaitForCompletion(currentReconnectionTask, this.BulkCopyTimeout, delegate
				{
					throw SQL.CR_ReconnectTimeout();
				}, false);
			}
			bool flag = true;
			this._isBulkCopyingInProgress = true;
			this.CreateOrValidateConnection("WriteToServer");
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			this._parserLock = openTdsConnection._parserLock;
			this._parserLock.Wait(this._isAsyncBulkCopy);
			Task task2;
			try
			{
				this.WriteRowSourceToServerCommon(columnCount);
				Task task = this.WriteToServerInternalAsync(ctoken);
				if (task != null)
				{
					flag = false;
					task2 = task.ContinueWith<Task>(delegate(Task t)
					{
						this.AbortTransaction();
						this._isBulkCopyingInProgress = false;
						if (this._parser != null)
						{
							this._parser._asyncWrite = false;
						}
						if (this._parserLock != null)
						{
							this._parserLock.Release();
							this._parserLock = null;
						}
						return t;
					}, TaskScheduler.Default).Unwrap();
				}
				else
				{
					task2 = null;
				}
			}
			catch (OutOfMemoryException ex)
			{
				this._connection.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this._connection.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this._connection.Abort(ex3);
				throw;
			}
			finally
			{
				this._columnMappings.ReadOnly = false;
				if (flag)
				{
					this.AbortTransaction();
					this._isBulkCopyingInProgress = false;
					if (this._parser != null)
					{
						this._parser._asyncWrite = false;
					}
					if (this._parserLock != null)
					{
						this._parserLock.Release();
						this._parserLock = null;
					}
				}
			}
			return task2;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00054908 File Offset: 0x00052B08
		private void WriteRowSourceToServerCommon(int columnCount)
		{
			bool flag = false;
			this._columnMappings.ReadOnly = true;
			this._localColumnMappings = this._columnMappings;
			if (this._localColumnMappings.Count > 0)
			{
				this._localColumnMappings.ValidateCollection();
				using (IEnumerator enumerator = this._localColumnMappings.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((SqlBulkCopyColumnMapping)enumerator.Current)._internalSourceColumnOrdinal == -1)
						{
							flag = true;
							break;
						}
					}
					goto IL_008A;
				}
			}
			this._localColumnMappings = new SqlBulkCopyColumnMappingCollection();
			this._localColumnMappings.CreateDefaultMapping(columnCount);
			IL_008A:
			if (flag)
			{
				int num = -1;
				flag = false;
				if (this._localColumnMappings.Count > 0)
				{
					foreach (object obj in this._localColumnMappings)
					{
						SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping = (SqlBulkCopyColumnMapping)obj;
						if (sqlBulkCopyColumnMapping._internalSourceColumnOrdinal == -1)
						{
							string text = this.UnquotedName(sqlBulkCopyColumnMapping.SourceColumn);
							switch (this._rowSourceType)
							{
							case SqlBulkCopy.ValueSourceType.IDataReader:
							case SqlBulkCopy.ValueSourceType.DbDataReader:
								try
								{
									num = this._DbDataReaderRowSource.GetOrdinal(text);
								}
								catch (IndexOutOfRangeException ex)
								{
									throw SQL.BulkLoadNonMatchingColumnName(text, ex);
								}
								break;
							case SqlBulkCopy.ValueSourceType.DataTable:
								num = ((DataTable)this._rowSource).Columns.IndexOf(text);
								break;
							case SqlBulkCopy.ValueSourceType.RowArray:
								num = ((DataRow[])this._rowSource)[0].Table.Columns.IndexOf(text);
								break;
							}
							if (num == -1)
							{
								throw SQL.BulkLoadNonMatchingColumnName(text);
							}
							sqlBulkCopyColumnMapping._internalSourceColumnOrdinal = num;
						}
					}
				}
			}
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00054ACC File Offset: 0x00052CCC
		internal void OnConnectionClosed()
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.OnConnectionClosed();
			}
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00054AEC File Offset: 0x00052CEC
		private void OnRowsCopied(SqlRowsCopiedEventArgs value)
		{
			SqlRowsCopiedEventHandler rowsCopiedEventHandler = this._rowsCopiedEventHandler;
			if (rowsCopiedEventHandler != null)
			{
				rowsCopiedEventHandler(this, value);
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00054B0C File Offset: 0x00052D0C
		private bool FireRowsCopiedEvent(long rowsCopied)
		{
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			bool canBeReleasedFromAnyThread = openTdsConnection._parserLock.CanBeReleasedFromAnyThread;
			openTdsConnection._parserLock.Release();
			SqlRowsCopiedEventArgs sqlRowsCopiedEventArgs = new SqlRowsCopiedEventArgs(rowsCopied);
			try
			{
				this._insideRowsCopiedEvent = true;
				this.OnRowsCopied(sqlRowsCopiedEventArgs);
			}
			finally
			{
				this._insideRowsCopiedEvent = false;
				openTdsConnection._parserLock.Wait(canBeReleasedFromAnyThread);
			}
			return sqlRowsCopiedEventArgs.Abort;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00054B80 File Offset: 0x00052D80
		private Task ReadWriteColumnValueAsync(int col)
		{
			bool flag;
			bool flag2;
			bool flag3;
			object obj = this.GetValueFromSourceRow(col, out flag, out flag2, out flag3);
			_SqlMetaData metadata = this._sortedColumnMappings[col]._metadata;
			if (!flag2)
			{
				obj = this.ConvertValue(obj, metadata, flag3, ref flag, out flag2);
			}
			Task task = null;
			if (metadata.type != SqlDbType.Variant)
			{
				task = this._parser.WriteBulkCopyValue(obj, metadata, this._stateObj, flag, flag2, flag3);
			}
			else
			{
				SqlBuffer.StorageType storageType = SqlBuffer.StorageType.Empty;
				if (this._SqlDataReaderRowSource != null && this._connection.IsKatmaiOrNewer)
				{
					storageType = this._SqlDataReaderRowSource.GetVariantInternalStorageType(this._sortedColumnMappings[col]._sourceColumnOrdinal);
				}
				if (storageType == SqlBuffer.StorageType.DateTime2)
				{
					this._parser.WriteSqlVariantDateTime2((DateTime)obj, this._stateObj);
				}
				else if (storageType == SqlBuffer.StorageType.Date)
				{
					this._parser.WriteSqlVariantDate((DateTime)obj, this._stateObj);
				}
				else
				{
					task = this._parser.WriteSqlVariantDataRowValue(obj, this._stateObj, true);
				}
			}
			return task;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00054C77 File Offset: 0x00052E77
		private void RegisterForConnectionCloseNotification<T>(ref Task<T> outerTask)
		{
			SqlConnection connection = this._connection;
			if (connection == null)
			{
				throw ADP.ClosedConnectionError();
			}
			connection.RegisterForConnectionCloseNotification<T>(ref outerTask, this, 3);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00054C90 File Offset: 0x00052E90
		private Task CopyColumnsAsync(int col, TaskCompletionSource<object> source = null)
		{
			Task task = null;
			Task task2 = null;
			try
			{
				int i;
				for (i = col; i < this._sortedColumnMappings.Count; i++)
				{
					task2 = this.ReadWriteColumnValueAsync(i);
					if (task2 != null)
					{
						break;
					}
				}
				if (task2 != null)
				{
					if (source == null)
					{
						source = new TaskCompletionSource<object>();
						task = source.Task;
					}
					this.CopyColumnsAsyncSetupContinuation(source, task2, i);
					return task;
				}
				if (source != null)
				{
					source.SetResult(null);
				}
			}
			catch (Exception ex)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(ex);
			}
			return task;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00054D18 File Offset: 0x00052F18
		private void CopyColumnsAsyncSetupContinuation(TaskCompletionSource<object> source, Task task, int i)
		{
			AsyncHelper.ContinueTask(task, source, delegate
			{
				if (i + 1 < this._sortedColumnMappings.Count)
				{
					this.CopyColumnsAsync(i + 1, source);
					return;
				}
				source.SetResult(null);
			}, this._connection.GetOpenTdsConnection(), null, null, null, null);
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00054D68 File Offset: 0x00052F68
		private void CheckAndRaiseNotification()
		{
			bool flag = false;
			Exception ex = null;
			this._rowsCopied++;
			if (this._notifyAfter > 0 && this._rowsUntilNotification > 0)
			{
				int num = this._rowsUntilNotification - 1;
				this._rowsUntilNotification = num;
				if (num == 0)
				{
					try
					{
						this._stateObj.BcpLock = true;
						flag = this.FireRowsCopiedEvent((long)this._rowsCopied);
						if (ConnectionState.Open != this._connection.State)
						{
							ex = ADP.OpenConnectionRequired("CheckAndRaiseNotification", this._connection.State);
						}
					}
					catch (Exception ex2)
					{
						if (!ADP.IsCatchableExceptionType(ex2))
						{
							ex = ex2;
						}
						else
						{
							ex = OperationAbortedException.Aborted(ex2);
						}
					}
					finally
					{
						this._stateObj.BcpLock = false;
					}
					if (!flag)
					{
						this._rowsUntilNotification = this._notifyAfter;
					}
				}
			}
			if (!flag && this._rowsUntilNotification > this._notifyAfter)
			{
				this._rowsUntilNotification = this._notifyAfter;
			}
			if (ex == null && flag)
			{
				ex = OperationAbortedException.Aborted(null);
			}
			if (this._connection.State != ConnectionState.Open)
			{
				throw ADP.OpenConnectionRequired("WriteToServer", this._connection.State);
			}
			if (ex != null)
			{
				this._parser._asyncWrite = false;
				this._parser.WriteBulkCopyDone(this._stateObj);
				this.RunParser(null);
				this.AbortTransaction();
				throw ex;
			}
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00054EC0 File Offset: 0x000530C0
		private Task CheckForCancellation(CancellationToken cts, TaskCompletionSource<object> tcs)
		{
			if (cts.IsCancellationRequested)
			{
				if (tcs == null)
				{
					tcs = new TaskCompletionSource<object>();
				}
				tcs.SetCanceled();
				return tcs.Task;
			}
			return null;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00054EE4 File Offset: 0x000530E4
		private TaskCompletionSource<object> ContinueTaskPend(Task task, TaskCompletionSource<object> source, Func<TaskCompletionSource<object>> action)
		{
			if (task == null)
			{
				return action();
			}
			AsyncHelper.ContinueTask(task, source, delegate
			{
				action();
			}, null, null, null, null, null);
			return null;
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00054F28 File Offset: 0x00053128
		private Task CopyRowsAsync(int rowsSoFar, int totalRows, CancellationToken cts, TaskCompletionSource<object> source = null)
		{
			Task task = null;
			try
			{
				int i = rowsSoFar;
				Action <>9__1;
				Action <>9__2;
				while ((totalRows <= 0 || i < totalRows) && this._hasMoreRowToCopy)
				{
					if (this._isAsyncBulkCopy)
					{
						task = this.CheckForCancellation(cts, source);
						if (task != null)
						{
							return task;
						}
					}
					this._stateObj.WriteByte(209);
					Task task2 = this.CopyColumnsAsync(0, null);
					if (task2 != null)
					{
						source = source ?? new TaskCompletionSource<object>();
						task = source.Task;
						AsyncHelper.ContinueTask(task2, source, delegate
						{
							this.CheckAndRaiseNotification();
							Task task5 = this.ReadFromRowSourceAsync(cts);
							if (task5 == null)
							{
								this.CopyRowsAsync(i + 1, totalRows, cts, source);
								return;
							}
							Task task6 = task5;
							TaskCompletionSource<object> source3 = source;
							Action action2;
							if ((action2 = <>9__2) == null)
							{
								action2 = (<>9__2 = delegate
								{
									this.CopyRowsAsync(i + 1, totalRows, cts, source);
								});
							}
							AsyncHelper.ContinueTask(task6, source3, action2, this._connection.GetOpenTdsConnection(), null, null, null, null);
						}, this._connection.GetOpenTdsConnection(), null, null, null, null);
						return task;
					}
					this.CheckAndRaiseNotification();
					Task task3 = this.ReadFromRowSourceAsync(cts);
					if (task3 != null)
					{
						if (source == null)
						{
							source = new TaskCompletionSource<object>();
						}
						task = source.Task;
						Task task4 = task3;
						TaskCompletionSource<object> source2 = source;
						Action action;
						if ((action = <>9__1) == null)
						{
							action = (<>9__1 = delegate
							{
								this.CopyRowsAsync(i + 1, totalRows, cts, source);
							});
						}
						AsyncHelper.ContinueTask(task4, source2, action, this._connection.GetOpenTdsConnection(), null, null, null, null);
						return task;
					}
					int j = i;
					i = j + 1;
				}
				if (source != null)
				{
					source.TrySetResult(null);
				}
			}
			catch (Exception ex)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(ex);
			}
			return task;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x000550FC File Offset: 0x000532FC
		private Task CopyBatchesAsync(BulkCopySimpleResultSet internalResults, string updateBulkCommandText, CancellationToken cts, TaskCompletionSource<object> source = null)
		{
			try
			{
				Action <>9__0;
				while (this._hasMoreRowToCopy)
				{
					SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
					if (this.IsCopyOption(SqlBulkCopyOptions.UseInternalTransaction))
					{
						openTdsConnection.ThreadHasParserLockForClose = true;
						try
						{
							this._internalTransaction = this._connection.BeginTransaction();
						}
						finally
						{
							openTdsConnection.ThreadHasParserLockForClose = false;
						}
					}
					Task task = this.SubmitUpdateBulkCommand(updateBulkCommandText);
					if (task != null)
					{
						if (source == null)
						{
							source = new TaskCompletionSource<object>();
						}
						Task task2 = task;
						TaskCompletionSource<object> source2 = source;
						Action action;
						if ((action = <>9__0) == null)
						{
							action = (<>9__0 = delegate
							{
								if (this.CopyBatchesAsyncContinued(internalResults, updateBulkCommandText, cts, source) == null)
								{
									this.CopyBatchesAsync(internalResults, updateBulkCommandText, cts, source);
								}
							});
						}
						AsyncHelper.ContinueTask(task2, source2, action, this._connection.GetOpenTdsConnection(), null, null, null, null);
						return source.Task;
					}
					Task task3 = this.CopyBatchesAsyncContinued(internalResults, updateBulkCommandText, cts, source);
					if (task3 != null)
					{
						return task3;
					}
				}
			}
			catch (Exception ex)
			{
				if (source != null)
				{
					source.TrySetException(ex);
					return source.Task;
				}
				throw;
			}
			if (source != null)
			{
				source.SetResult(null);
				return source.Task;
			}
			return null;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00055280 File Offset: 0x00053480
		private Task CopyBatchesAsyncContinued(BulkCopySimpleResultSet internalResults, string updateBulkCommandText, CancellationToken cts, TaskCompletionSource<object> source)
		{
			Task task2;
			try
			{
				this.WriteMetaData(internalResults);
				Task task = this.CopyRowsAsync(0, this._savedBatchSize, cts, null);
				if (task != null)
				{
					if (source == null)
					{
						source = new TaskCompletionSource<object>();
					}
					AsyncHelper.ContinueTask(task, source, delegate
					{
						if (this.CopyBatchesAsyncContinuedOnSuccess(internalResults, updateBulkCommandText, cts, source) == null)
						{
							this.CopyBatchesAsync(internalResults, updateBulkCommandText, cts, source);
						}
					}, this._connection.GetOpenTdsConnection(), delegate(Exception _)
					{
						this.CopyBatchesAsyncContinuedOnError(false);
					}, delegate
					{
						this.CopyBatchesAsyncContinuedOnError(true);
					}, null, null);
					task2 = source.Task;
				}
				else
				{
					task2 = this.CopyBatchesAsyncContinuedOnSuccess(internalResults, updateBulkCommandText, cts, source);
				}
			}
			catch (Exception ex)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(ex);
				task2 = source.Task;
			}
			return task2;
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00055394 File Offset: 0x00053594
		private Task CopyBatchesAsyncContinuedOnSuccess(BulkCopySimpleResultSet internalResults, string updateBulkCommandText, CancellationToken cts, TaskCompletionSource<object> source)
		{
			Task task2;
			try
			{
				Task task = this._parser.WriteBulkCopyDone(this._stateObj);
				if (task == null)
				{
					this.RunParser(null);
					this.CommitTransaction();
					task2 = null;
				}
				else
				{
					if (source == null)
					{
						source = new TaskCompletionSource<object>();
					}
					AsyncHelper.ContinueTask(task, source, delegate
					{
						try
						{
							this.RunParser(null);
							this.CommitTransaction();
						}
						catch (Exception)
						{
							this.CopyBatchesAsyncContinuedOnError(false);
							throw;
						}
						this.CopyBatchesAsync(internalResults, updateBulkCommandText, cts, source);
					}, this._connection.GetOpenTdsConnection(), delegate(Exception _)
					{
						this.CopyBatchesAsyncContinuedOnError(false);
					}, null, null, null);
					task2 = source.Task;
				}
			}
			catch (Exception ex)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(ex);
				task2 = source.Task;
			}
			return task2;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0005547C File Offset: 0x0005367C
		private void CopyBatchesAsyncContinuedOnError(bool cleanupParser)
		{
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			try
			{
				if (cleanupParser && this._parser != null && this._stateObj != null)
				{
					this._parser._asyncWrite = false;
					this._parser.WriteBulkCopyDone(this._stateObj);
					this.RunParser(null);
				}
				if (this._stateObj != null)
				{
					this.CleanUpStateObjectOnError();
				}
			}
			catch (OutOfMemoryException)
			{
				openTdsConnection.DoomThisConnection();
				throw;
			}
			catch (StackOverflowException)
			{
				openTdsConnection.DoomThisConnection();
				throw;
			}
			catch (ThreadAbortException)
			{
				openTdsConnection.DoomThisConnection();
				throw;
			}
			this.AbortTransaction();
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00055528 File Offset: 0x00053728
		private void CleanUpStateObjectOnError()
		{
			if (this._stateObj != null)
			{
				this._parser.Connection.ThreadHasParserLockForClose = true;
				try
				{
					this._stateObj.ResetBuffer();
					this._stateObj._outputPacketNumber = 1;
					if (this._parser.State == TdsParserState.OpenNotLoggedIn || this._parser.State == TdsParserState.OpenLoggedIn)
					{
						this._stateObj.CancelRequest();
					}
					this._stateObj._internalTimeout = false;
					this._stateObj.CloseSession();
					this._stateObj._bulkCopyOpperationInProgress = false;
					this._stateObj._bulkCopyWriteTimeout = false;
					this._stateObj = null;
				}
				finally
				{
					this._parser.Connection.ThreadHasParserLockForClose = false;
				}
			}
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x000555EC File Offset: 0x000537EC
		private void WriteToServerInternalRestContinuedAsync(BulkCopySimpleResultSet internalResults, CancellationToken cts, TaskCompletionSource<object> source)
		{
			Task task = null;
			try
			{
				string text = this.AnalyzeTargetAndCreateUpdateBulkCommand(internalResults);
				if (this._sortedColumnMappings.Count != 0)
				{
					this._stateObj.SniContext = SniContext.Snix_SendRows;
					this._savedBatchSize = this._batchSize;
					this._rowsUntilNotification = this._notifyAfter;
					this._rowsCopied = 0;
					this._currentRowMetadata = new SqlBulkCopy.SourceColumnMetadata[this._sortedColumnMappings.Count];
					for (int i = 0; i < this._currentRowMetadata.Length; i++)
					{
						this._currentRowMetadata[i] = this.GetColumnMetadata(i);
					}
					task = this.CopyBatchesAsync(internalResults, text, cts, null);
				}
				if (task != null)
				{
					if (source == null)
					{
						source = new TaskCompletionSource<object>();
					}
					AsyncHelper.ContinueTask(task, source, delegate
					{
						if (task.IsCanceled)
						{
							this._localColumnMappings = null;
							try
							{
								this.CleanUpStateObjectOnError();
								return;
							}
							finally
							{
								source.SetCanceled();
							}
						}
						if (task.Exception != null)
						{
							source.SetException(task.Exception.InnerException);
							return;
						}
						this._localColumnMappings = null;
						try
						{
							this.CleanUpStateObjectOnError();
						}
						finally
						{
							if (source != null)
							{
								if (cts.IsCancellationRequested)
								{
									source.SetCanceled();
								}
								else
								{
									source.SetResult(null);
								}
							}
						}
					}, this._connection.GetOpenTdsConnection(), null, null, null, null);
				}
				else
				{
					this._localColumnMappings = null;
					try
					{
						this.CleanUpStateObjectOnError();
					}
					catch (Exception)
					{
					}
					if (source != null)
					{
						source.SetResult(null);
					}
				}
			}
			catch (Exception ex)
			{
				this._localColumnMappings = null;
				try
				{
					this.CleanUpStateObjectOnError();
				}
				catch (Exception)
				{
				}
				if (source == null)
				{
					throw;
				}
				source.TrySetException(ex);
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0005579C File Offset: 0x0005399C
		private void WriteToServerInternalRestAsync(CancellationToken cts, TaskCompletionSource<object> source)
		{
			this._hasMoreRowToCopy = true;
			Task<BulkCopySimpleResultSet> internalResultsTask = null;
			BulkCopySimpleResultSet bulkCopySimpleResultSet = new BulkCopySimpleResultSet();
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			try
			{
				this._parser = this._connection.Parser;
				this._parser._asyncWrite = this._isAsyncBulkCopy;
				Task task;
				try
				{
					task = this._connection.ValidateAndReconnect(delegate
					{
						if (this._parserLock != null)
						{
							this._parserLock.Release();
							this._parserLock = null;
						}
					}, this.BulkCopyTimeout);
				}
				catch (SqlException ex)
				{
					SqlException ex5;
					throw SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, ex5);
				}
				if (task != null)
				{
					if (this._isAsyncBulkCopy)
					{
						CancellationTokenRegistration regReconnectCancel = default(CancellationTokenRegistration);
						TaskCompletionSource<object> cancellableReconnectTS = new TaskCompletionSource<object>();
						if (cts.CanBeCanceled)
						{
							regReconnectCancel = cts.Register(delegate(object s)
							{
								((TaskCompletionSource<object>)s).TrySetCanceled();
							}, cancellableReconnectTS);
						}
						AsyncHelper.ContinueTask(task, cancellableReconnectTS, delegate
						{
							cancellableReconnectTS.SetResult(null);
						}, null, null, null, null, null);
						AsyncHelper.SetTimeoutException(cancellableReconnectTS, this.BulkCopyTimeout, () => SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, SQL.CR_ReconnectTimeout()), CancellationToken.None);
						AsyncHelper.ContinueTask(cancellableReconnectTS.Task, source, delegate
						{
							regReconnectCancel.Dispose();
							if (this._parserLock != null)
							{
								this._parserLock.Release();
								this._parserLock = null;
							}
							this._parserLock = this._connection.GetOpenTdsConnection()._parserLock;
							this._parserLock.Wait(true);
							this.WriteToServerInternalRestAsync(cts, source);
						}, null, delegate(Exception e)
						{
							regReconnectCancel.Dispose();
						}, delegate
						{
							regReconnectCancel.Dispose();
						}, (Exception ex) => SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, ex), this._connection);
					}
					else
					{
						try
						{
							AsyncHelper.WaitForCompletion(task, this.BulkCopyTimeout, delegate
							{
								throw SQL.CR_ReconnectTimeout();
							}, true);
						}
						catch (SqlException ex2)
						{
							throw SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, ex2);
						}
						this._parserLock = this._connection.GetOpenTdsConnection()._parserLock;
						this._parserLock.Wait(false);
						this.WriteToServerInternalRestAsync(cts, source);
					}
				}
				else
				{
					if (this._isAsyncBulkCopy)
					{
						this._connection.AddWeakReference(this, 3);
					}
					openTdsConnection.ThreadHasParserLockForClose = true;
					try
					{
						this._stateObj = this._parser.GetSession(this);
						this._stateObj._bulkCopyOpperationInProgress = true;
						this._stateObj.StartSession(this);
					}
					finally
					{
						openTdsConnection.ThreadHasParserLockForClose = false;
					}
					try
					{
						internalResultsTask = this.CreateAndExecuteInitialQueryAsync(out bulkCopySimpleResultSet);
					}
					catch (SqlException ex3)
					{
						throw SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, ex3);
					}
					if (internalResultsTask != null)
					{
						AsyncHelper.ContinueTask(internalResultsTask, source, delegate
						{
							this.WriteToServerInternalRestContinuedAsync(internalResultsTask.Result, cts, source);
						}, this._connection.GetOpenTdsConnection(), null, null, null, null);
					}
					else
					{
						this.WriteToServerInternalRestContinuedAsync(bulkCopySimpleResultSet, cts, source);
					}
				}
			}
			catch (Exception ex4)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(ex4);
			}
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00055B38 File Offset: 0x00053D38
		private Task WriteToServerInternalAsync(CancellationToken ctoken)
		{
			TaskCompletionSource<object> source = null;
			Task<object> task = null;
			if (this._isAsyncBulkCopy)
			{
				source = new TaskCompletionSource<object>();
				task = source.Task;
				this.RegisterForConnectionCloseNotification<object>(ref task);
			}
			if (this._destinationTableName != null)
			{
				try
				{
					Task task2 = this.ReadFromRowSourceAsync(ctoken);
					if (task2 != null)
					{
						AsyncHelper.ContinueTask(task2, source, delegate
						{
							if (!this._hasMoreRowToCopy)
							{
								source.SetResult(null);
								return;
							}
							this.WriteToServerInternalRestAsync(ctoken, source);
						}, this._connection.GetOpenTdsConnection(), null, null, null, null);
						return task;
					}
					if (!this._hasMoreRowToCopy)
					{
						if (source != null)
						{
							source.SetResult(null);
						}
						return task;
					}
					this.WriteToServerInternalRestAsync(ctoken, source);
					return task;
				}
				catch (Exception ex)
				{
					if (source == null)
					{
						throw;
					}
					source.TrySetException(ex);
				}
				return task;
			}
			if (source != null)
			{
				source.SetException(SQL.BulkLoadMissingDestinationTable());
				return task;
			}
			throw SQL.BulkLoadMissingDestinationTable();
		}

		// Token: 0x04000ADC RID: 2780
		private const int MetaDataResultId = 1;

		// Token: 0x04000ADD RID: 2781
		private const int CollationResultId = 2;

		// Token: 0x04000ADE RID: 2782
		private const int CollationId = 3;

		// Token: 0x04000ADF RID: 2783
		private const int MAX_LENGTH = 2147483647;

		// Token: 0x04000AE0 RID: 2784
		private const int DefaultCommandTimeout = 30;

		// Token: 0x04000AE1 RID: 2785
		private bool _enableStreaming;

		// Token: 0x04000AE2 RID: 2786
		private int _batchSize;

		// Token: 0x04000AE3 RID: 2787
		private bool _ownConnection;

		// Token: 0x04000AE4 RID: 2788
		private SqlBulkCopyOptions _copyOptions;

		// Token: 0x04000AE5 RID: 2789
		private int _timeout = 30;

		// Token: 0x04000AE6 RID: 2790
		private string _destinationTableName;

		// Token: 0x04000AE7 RID: 2791
		private int _rowsCopied;

		// Token: 0x04000AE8 RID: 2792
		private int _notifyAfter;

		// Token: 0x04000AE9 RID: 2793
		private int _rowsUntilNotification;

		// Token: 0x04000AEA RID: 2794
		private bool _insideRowsCopiedEvent;

		// Token: 0x04000AEB RID: 2795
		private object _rowSource;

		// Token: 0x04000AEC RID: 2796
		private SqlDataReader _SqlDataReaderRowSource;

		// Token: 0x04000AED RID: 2797
		private DbDataReader _DbDataReaderRowSource;

		// Token: 0x04000AEE RID: 2798
		private DataTable _dataTableSource;

		// Token: 0x04000AEF RID: 2799
		private SqlBulkCopyColumnMappingCollection _columnMappings;

		// Token: 0x04000AF0 RID: 2800
		private SqlBulkCopyColumnMappingCollection _localColumnMappings;

		// Token: 0x04000AF1 RID: 2801
		private SqlConnection _connection;

		// Token: 0x04000AF2 RID: 2802
		private SqlTransaction _internalTransaction;

		// Token: 0x04000AF3 RID: 2803
		private SqlTransaction _externalTransaction;

		// Token: 0x04000AF4 RID: 2804
		private SqlBulkCopy.ValueSourceType _rowSourceType;

		// Token: 0x04000AF5 RID: 2805
		private DataRow _currentRow;

		// Token: 0x04000AF6 RID: 2806
		private int _currentRowLength;

		// Token: 0x04000AF7 RID: 2807
		private DataRowState _rowStateToSkip;

		// Token: 0x04000AF8 RID: 2808
		private IEnumerator _rowEnumerator;

		// Token: 0x04000AF9 RID: 2809
		private TdsParser _parser;

		// Token: 0x04000AFA RID: 2810
		private TdsParserStateObject _stateObj;

		// Token: 0x04000AFB RID: 2811
		private List<_ColumnMapping> _sortedColumnMappings;

		// Token: 0x04000AFC RID: 2812
		private SqlRowsCopiedEventHandler _rowsCopiedEventHandler;

		// Token: 0x04000AFD RID: 2813
		private int _savedBatchSize;

		// Token: 0x04000AFE RID: 2814
		private bool _hasMoreRowToCopy;

		// Token: 0x04000AFF RID: 2815
		private bool _isAsyncBulkCopy;

		// Token: 0x04000B00 RID: 2816
		private bool _isBulkCopyingInProgress;

		// Token: 0x04000B01 RID: 2817
		private SqlInternalConnectionTds.SyncAsyncLock _parserLock;

		// Token: 0x04000B02 RID: 2818
		private SqlBulkCopy.SourceColumnMetadata[] _currentRowMetadata;

		// Token: 0x02000153 RID: 339
		private enum ValueSourceType
		{
			// Token: 0x04000B04 RID: 2820
			Unspecified,
			// Token: 0x04000B05 RID: 2821
			IDataReader,
			// Token: 0x04000B06 RID: 2822
			DataTable,
			// Token: 0x04000B07 RID: 2823
			RowArray,
			// Token: 0x04000B08 RID: 2824
			DbDataReader
		}

		// Token: 0x02000154 RID: 340
		private enum ValueMethod : byte
		{
			// Token: 0x04000B0A RID: 2826
			GetValue,
			// Token: 0x04000B0B RID: 2827
			SqlTypeSqlDecimal,
			// Token: 0x04000B0C RID: 2828
			SqlTypeSqlDouble,
			// Token: 0x04000B0D RID: 2829
			SqlTypeSqlSingle,
			// Token: 0x04000B0E RID: 2830
			DataFeedStream,
			// Token: 0x04000B0F RID: 2831
			DataFeedText,
			// Token: 0x04000B10 RID: 2832
			DataFeedXml
		}

		// Token: 0x02000155 RID: 341
		private struct SourceColumnMetadata
		{
			// Token: 0x060010C9 RID: 4297 RVA: 0x00055CBC File Offset: 0x00053EBC
			public SourceColumnMetadata(SqlBulkCopy.ValueMethod method, bool isSqlType, bool isDataFeed)
			{
				this.Method = method;
				this.IsSqlType = isSqlType;
				this.IsDataFeed = isDataFeed;
			}

			// Token: 0x04000B11 RID: 2833
			public readonly SqlBulkCopy.ValueMethod Method;

			// Token: 0x04000B12 RID: 2834
			public readonly bool IsSqlType;

			// Token: 0x04000B13 RID: 2835
			public readonly bool IsDataFeed;
		}
	}
}
