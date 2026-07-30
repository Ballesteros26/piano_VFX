using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200001F RID: 31
	public sealed class SqliteDataReader : DbDataReader
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x00009EF0 File Offset: 0x000080F0
		internal SqliteDataReader(SqliteCommand cmd, CommandBehavior behave)
		{
			this._command = cmd;
			this._version = this._command.Connection._version;
			this._commandBehavior = behave;
			this._activeStatementIndex = -1;
			this._activeStatement = null;
			this._rowsAffected = -1;
			this._fieldCount = 0;
			if (this._command != null)
			{
				this.NextResult();
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00009F52 File Offset: 0x00008152
		internal void Cancel()
		{
			this._version = 0L;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00009F5C File Offset: 0x0000815C
		public override void Close()
		{
			try
			{
				if (this._command != null)
				{
					try
					{
						try
						{
							if (this._version != 0L)
							{
								try
								{
									while (this.NextResult())
									{
									}
								}
								catch
								{
								}
							}
							this._command.ClearDataReader();
						}
						finally
						{
							if ((this._commandBehavior & CommandBehavior.CloseConnection) != CommandBehavior.Default && this._command.Connection != null)
							{
								DbConnection connection = this._command.Connection;
								this._command.Dispose();
								connection.Close();
								this._disposeCommand = false;
							}
						}
					}
					finally
					{
						if (this._disposeCommand)
						{
							this._command.Dispose();
						}
					}
				}
				this._command = null;
				this._activeStatement = null;
				this._fieldTypeArray = null;
			}
			finally
			{
				if (this._keyInfo != null)
				{
					this._keyInfo.Dispose();
					this._keyInfo = null;
				}
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000A048 File Offset: 0x00008248
		private void CheckClosed()
		{
			if (this._command == null)
			{
				throw new InvalidOperationException("DataReader has been closed");
			}
			if (this._version == 0L)
			{
				throw new SqliteException(4, "Execution was aborted by the user");
			}
			if (this._command.Connection.State != ConnectionState.Open || this._command.Connection._version != this._version)
			{
				throw new InvalidOperationException("Connection was closed, statement was terminated");
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000A0B2 File Offset: 0x000082B2
		private void CheckValidRow()
		{
			if (this._readingState != 0)
			{
				throw new InvalidOperationException("No current row");
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000A0C7 File Offset: 0x000082C7
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this, (this._commandBehavior & CommandBehavior.CloseConnection) == CommandBehavior.CloseConnection);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000A0DC File Offset: 0x000082DC
		public override int Depth
		{
			get
			{
				this.CheckClosed();
				return 0;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000A0E5 File Offset: 0x000082E5
		public override int FieldCount
		{
			get
			{
				this.CheckClosed();
				if (this._keyInfo == null)
				{
					return this._fieldCount;
				}
				return this._fieldCount + this._keyInfo.Count;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000A10E File Offset: 0x0000830E
		public override int VisibleFieldCount
		{
			get
			{
				this.CheckClosed();
				return this._fieldCount;
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000A11C File Offset: 0x0000831C
		private TypeAffinity VerifyType(int i, DbType typ)
		{
			this.CheckClosed();
			this.CheckValidRow();
			TypeAffinity affinity = this.GetSQLiteType(i).Affinity;
			switch (affinity)
			{
			case TypeAffinity.Int64:
				if (typ == DbType.Int16)
				{
					return affinity;
				}
				if (typ == DbType.Int32)
				{
					return affinity;
				}
				if (typ == DbType.Int64)
				{
					return affinity;
				}
				if (typ == DbType.Boolean)
				{
					return affinity;
				}
				if (typ == DbType.Byte)
				{
					return affinity;
				}
				if (typ == DbType.DateTime)
				{
					return affinity;
				}
				if (typ == DbType.Single)
				{
					return affinity;
				}
				if (typ == DbType.Double)
				{
					return affinity;
				}
				if (typ == DbType.Decimal)
				{
					return affinity;
				}
				break;
			case TypeAffinity.Double:
				if (typ == DbType.Single)
				{
					return affinity;
				}
				if (typ == DbType.Double)
				{
					return affinity;
				}
				if (typ == DbType.Decimal)
				{
					return affinity;
				}
				if (typ == DbType.DateTime)
				{
					return affinity;
				}
				break;
			case TypeAffinity.Text:
				if (typ == DbType.SByte)
				{
					return affinity;
				}
				if (typ == DbType.String)
				{
					return affinity;
				}
				if (typ == DbType.SByte)
				{
					return affinity;
				}
				if (typ == DbType.Guid)
				{
					return affinity;
				}
				if (typ == DbType.DateTime)
				{
					return affinity;
				}
				if (typ == DbType.Decimal)
				{
					return affinity;
				}
				break;
			case TypeAffinity.Blob:
				if (typ == DbType.Guid)
				{
					return affinity;
				}
				if (typ == DbType.String)
				{
					return affinity;
				}
				if (typ == DbType.Binary)
				{
					return affinity;
				}
				break;
			}
			throw new InvalidCastException();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000A1F4 File Offset: 0x000083F4
		public override bool GetBoolean(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetBoolean(i - this.VisibleFieldCount);
			}
			this.VerifyType(i, DbType.Boolean);
			return Convert.ToBoolean(this.GetValue(i), CultureInfo.CurrentCulture);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000A240 File Offset: 0x00008440
		public override byte GetByte(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetByte(i - this.VisibleFieldCount);
			}
			this.VerifyType(i, DbType.Byte);
			return Convert.ToByte(this._activeStatement._sql.GetInt32(this._activeStatement, i));
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000A298 File Offset: 0x00008498
		public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetBytes(i - this.VisibleFieldCount, fieldOffset, buffer, bufferoffset, length);
			}
			this.VerifyType(i, DbType.Binary);
			return this._activeStatement._sql.GetBytes(this._activeStatement, i, (int)fieldOffset, buffer, bufferoffset, length);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000A2F8 File Offset: 0x000084F8
		public override char GetChar(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetChar(i - this.VisibleFieldCount);
			}
			this.VerifyType(i, DbType.SByte);
			return Convert.ToChar(this._activeStatement._sql.GetInt32(this._activeStatement, i));
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000A350 File Offset: 0x00008550
		public override long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetChars(i - this.VisibleFieldCount, fieldoffset, buffer, bufferoffset, length);
			}
			this.VerifyType(i, DbType.String);
			return this._activeStatement._sql.GetChars(this._activeStatement, i, (int)fieldoffset, buffer, bufferoffset, length);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000A3B0 File Offset: 0x000085B0
		public override string GetDataTypeName(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetDataTypeName(i - this.VisibleFieldCount);
			}
			SQLiteType sqliteType = this.GetSQLiteType(i);
			return this._activeStatement._sql.ColumnType(this._activeStatement, i, out sqliteType.Affinity);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000A408 File Offset: 0x00008608
		public override DateTime GetDateTime(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetDateTime(i - this.VisibleFieldCount);
			}
			this.VerifyType(i, DbType.DateTime);
			return this._activeStatement._sql.GetDateTime(this._activeStatement, i);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000A45C File Offset: 0x0000865C
		public override decimal GetDecimal(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetDecimal(i - this.VisibleFieldCount);
			}
			this.VerifyType(i, DbType.Decimal);
			return decimal.Parse(this._activeStatement._sql.GetText(this._activeStatement, i), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, CultureInfo.InvariantCulture);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000A4C0 File Offset: 0x000086C0
		public override double GetDouble(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetDouble(i - this.VisibleFieldCount);
			}
			this.VerifyType(i, DbType.Double);
			return this._activeStatement._sql.GetDouble(this._activeStatement, i);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000A512 File Offset: 0x00008712
		public override Type GetFieldType(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetFieldType(i - this.VisibleFieldCount);
			}
			return SqliteConvert.SQLiteTypeToType(this.GetSQLiteType(i));
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000A548 File Offset: 0x00008748
		public override float GetFloat(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetFloat(i - this.VisibleFieldCount);
			}
			this.VerifyType(i, DbType.Single);
			return Convert.ToSingle(this._activeStatement._sql.GetDouble(this._activeStatement, i));
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000A5A0 File Offset: 0x000087A0
		public override Guid GetGuid(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetGuid(i - this.VisibleFieldCount);
			}
			if (this.VerifyType(i, DbType.Guid) == TypeAffinity.Blob)
			{
				byte[] array = new byte[16];
				this._activeStatement._sql.GetBytes(this._activeStatement, i, 0, array, 0, 16);
				return new Guid(array);
			}
			return new Guid(this._activeStatement._sql.GetText(this._activeStatement, i));
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000A628 File Offset: 0x00008828
		public override short GetInt16(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetInt16(i - this.VisibleFieldCount);
			}
			this.VerifyType(i, DbType.Int16);
			return Convert.ToInt16(this._activeStatement._sql.GetInt32(this._activeStatement, i));
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000A680 File Offset: 0x00008880
		public override int GetInt32(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetInt32(i - this.VisibleFieldCount);
			}
			this.VerifyType(i, DbType.Int32);
			return this._activeStatement._sql.GetInt32(this._activeStatement, i);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000A6D4 File Offset: 0x000088D4
		public override long GetInt64(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetInt64(i - this.VisibleFieldCount);
			}
			this.VerifyType(i, DbType.Int64);
			return this._activeStatement._sql.GetInt64(this._activeStatement, i);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000A727 File Offset: 0x00008927
		public override string GetName(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetName(i - this.VisibleFieldCount);
			}
			return this._activeStatement._sql.ColumnName(this._activeStatement, i);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000A768 File Offset: 0x00008968
		public override int GetOrdinal(string name)
		{
			this.CheckClosed();
			int num = this._activeStatement._sql.ColumnIndex(this._activeStatement, name);
			if (num == -1 && this._keyInfo != null)
			{
				num = this._keyInfo.GetOrdinal(name);
				if (num > -1)
				{
					num += this.VisibleFieldCount;
				}
			}
			return num;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000A7BA File Offset: 0x000089BA
		public override DataTable GetSchemaTable()
		{
			return this.GetSchemaTable(true, false);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000A7C4 File Offset: 0x000089C4
		internal DataTable GetSchemaTable(bool wantUniqueInfo, bool wantDefaultValue)
		{
			this.CheckClosed();
			DataTable dataTable = new DataTable("SchemaTable");
			DataTable dataTable2 = null;
			string text = "";
			string text2 = "";
			string text3 = "";
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add(SchemaTableColumn.ColumnName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.ColumnOrdinal, typeof(int));
			dataTable.Columns.Add(SchemaTableColumn.ColumnSize, typeof(int));
			dataTable.Columns.Add(SchemaTableColumn.NumericPrecision, typeof(short));
			dataTable.Columns.Add(SchemaTableColumn.NumericScale, typeof(short));
			dataTable.Columns.Add(SchemaTableColumn.IsUnique, typeof(bool));
			dataTable.Columns.Add(SchemaTableColumn.IsKey, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.BaseServerName, typeof(string));
			dataTable.Columns.Add(SchemaTableOptionalColumn.BaseCatalogName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.BaseColumnName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.BaseSchemaName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.BaseTableName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.DataType, typeof(Type));
			dataTable.Columns.Add(SchemaTableColumn.AllowDBNull, typeof(bool));
			dataTable.Columns.Add(SchemaTableColumn.ProviderType, typeof(int));
			dataTable.Columns.Add(SchemaTableColumn.IsAliased, typeof(bool));
			dataTable.Columns.Add(SchemaTableColumn.IsExpression, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.IsRowVersion, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.IsHidden, typeof(bool));
			dataTable.Columns.Add(SchemaTableColumn.IsLong, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.IsReadOnly, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.ProviderSpecificDataType, typeof(Type));
			dataTable.Columns.Add(SchemaTableOptionalColumn.DefaultValue, typeof(object));
			dataTable.Columns.Add("DataTypeName", typeof(string));
			dataTable.Columns.Add("CollationType", typeof(string));
			dataTable.BeginLoadData();
			for (int i = 0; i < this._fieldCount; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				DbType type = this.GetSQLiteType(i).Type;
				dataRow[SchemaTableColumn.ColumnName] = this.GetName(i);
				dataRow[SchemaTableColumn.ColumnOrdinal] = i;
				dataRow[SchemaTableColumn.ColumnSize] = SqliteConvert.DbTypeToColumnSize(type);
				dataRow[SchemaTableColumn.NumericPrecision] = SqliteConvert.DbTypeToNumericPrecision(type);
				dataRow[SchemaTableColumn.NumericScale] = SqliteConvert.DbTypeToNumericScale(type);
				dataRow[SchemaTableColumn.ProviderType] = this.GetSQLiteType(i).Type;
				dataRow[SchemaTableColumn.IsLong] = false;
				dataRow[SchemaTableColumn.AllowDBNull] = true;
				dataRow[SchemaTableOptionalColumn.IsReadOnly] = false;
				dataRow[SchemaTableOptionalColumn.IsRowVersion] = false;
				dataRow[SchemaTableColumn.IsUnique] = false;
				dataRow[SchemaTableColumn.IsKey] = false;
				dataRow[SchemaTableOptionalColumn.IsAutoIncrement] = false;
				dataRow[SchemaTableColumn.DataType] = this.GetFieldType(i);
				dataRow[SchemaTableOptionalColumn.IsHidden] = false;
				if (SqliteDataReader.hasColumnMetadataSupport)
				{
					try
					{
						text3 = this._command.Connection._sql.ColumnOriginalName(this._activeStatement, i);
						if (!string.IsNullOrEmpty(text3))
						{
							dataRow[SchemaTableColumn.BaseColumnName] = text3;
						}
						dataRow[SchemaTableColumn.IsExpression] = string.IsNullOrEmpty(text3);
						dataRow[SchemaTableColumn.IsAliased] = string.Compare(this.GetName(i), text3, true, CultureInfo.InvariantCulture) != 0;
						string text4 = this._command.Connection._sql.ColumnTableName(this._activeStatement, i);
						if (!string.IsNullOrEmpty(text4))
						{
							dataRow[SchemaTableColumn.BaseTableName] = text4;
						}
						text4 = this._command.Connection._sql.ColumnDatabaseName(this._activeStatement, i);
						if (!string.IsNullOrEmpty(text4))
						{
							dataRow[SchemaTableOptionalColumn.BaseCatalogName] = text4;
						}
					}
					catch (EntryPointNotFoundException)
					{
						SqliteDataReader.hasColumnMetadataSupport = false;
					}
				}
				string text5 = null;
				if (!string.IsNullOrEmpty(text3))
				{
					string text6;
					bool flag;
					bool flag2;
					bool flag3;
					this._command.Connection._sql.ColumnMetaData((string)dataRow[SchemaTableOptionalColumn.BaseCatalogName], (string)dataRow[SchemaTableColumn.BaseTableName], text3, out text5, out text6, out flag, out flag2, out flag3);
					if (flag || flag2)
					{
						dataRow[SchemaTableColumn.AllowDBNull] = false;
					}
					dataRow[SchemaTableColumn.IsKey] = flag2;
					dataRow[SchemaTableOptionalColumn.IsAutoIncrement] = flag3;
					dataRow["CollationType"] = text6;
					string[] array = text5.Split(new char[] { '(' });
					if (array.Length > 1)
					{
						text5 = array[0];
						array = array[1].Split(new char[] { ')' });
						if (array.Length > 1)
						{
							array = array[0].Split(new char[] { ',', '.' });
							if (this.GetSQLiteType(i).Type == DbType.String || this.GetSQLiteType(i).Type == DbType.Binary)
							{
								dataRow[SchemaTableColumn.ColumnSize] = Convert.ToInt32(array[0], CultureInfo.InvariantCulture);
							}
							else
							{
								dataRow[SchemaTableColumn.NumericPrecision] = Convert.ToInt32(array[0], CultureInfo.InvariantCulture);
								if (array.Length > 1)
								{
									dataRow[SchemaTableColumn.NumericScale] = Convert.ToInt32(array[1], CultureInfo.InvariantCulture);
								}
							}
						}
					}
					if (wantDefaultValue)
					{
						using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].TABLE_INFO([{1}])", dataRow[SchemaTableOptionalColumn.BaseCatalogName], dataRow[SchemaTableColumn.BaseTableName]), this._command.Connection))
						{
							using (DbDataReader dbDataReader = sqliteCommand.ExecuteReader())
							{
								while (dbDataReader.Read())
								{
									if (string.Compare((string)dataRow[SchemaTableColumn.BaseColumnName], dbDataReader.GetString(1), true, CultureInfo.InvariantCulture) == 0)
									{
										if (!dbDataReader.IsDBNull(4))
										{
											dataRow[SchemaTableOptionalColumn.DefaultValue] = dbDataReader[4];
											break;
										}
										break;
									}
								}
							}
						}
					}
					if (wantUniqueInfo)
					{
						if ((string)dataRow[SchemaTableOptionalColumn.BaseCatalogName] != text || (string)dataRow[SchemaTableColumn.BaseTableName] != text2)
						{
							text = (string)dataRow[SchemaTableOptionalColumn.BaseCatalogName];
							text2 = (string)dataRow[SchemaTableColumn.BaseTableName];
							DbConnection connection = this._command.Connection;
							string text7 = "Indexes";
							string[] array2 = new string[4];
							array2[0] = (string)dataRow[SchemaTableOptionalColumn.BaseCatalogName];
							array2[2] = (string)dataRow[SchemaTableColumn.BaseTableName];
							dataTable2 = connection.GetSchema(text7, array2);
						}
						foreach (object obj in dataTable2.Rows)
						{
							DataRow dataRow2 = (DataRow)obj;
							DbConnection connection2 = this._command.Connection;
							string text8 = "IndexColumns";
							string[] array3 = new string[5];
							array3[0] = (string)dataRow[SchemaTableOptionalColumn.BaseCatalogName];
							array3[2] = (string)dataRow[SchemaTableColumn.BaseTableName];
							array3[3] = (string)dataRow2["INDEX_NAME"];
							DataTable schema = connection2.GetSchema(text8, array3);
							using (IEnumerator enumerator2 = schema.Rows.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									if (string.Compare((string)((DataRow)enumerator2.Current)["COLUMN_NAME"], text3, true, CultureInfo.InvariantCulture) == 0)
									{
										if (schema.Rows.Count == 1 && !(bool)dataRow[SchemaTableColumn.AllowDBNull])
										{
											dataRow[SchemaTableColumn.IsUnique] = dataRow2["UNIQUE"];
										}
										if (schema.Rows.Count == 1 && (bool)dataRow2["PRIMARY_KEY"] && !string.IsNullOrEmpty(text5) && string.Compare(text5, "integer", true, CultureInfo.InvariantCulture) == 0)
										{
											break;
										}
										break;
									}
								}
							}
						}
					}
					if (string.IsNullOrEmpty(text5))
					{
						TypeAffinity typeAffinity;
						text5 = this._activeStatement._sql.ColumnType(this._activeStatement, i, out typeAffinity);
					}
					if (!string.IsNullOrEmpty(text5))
					{
						dataRow["DataTypeName"] = text5;
					}
				}
				dataTable.Rows.Add(dataRow);
			}
			if (this._keyInfo != null)
			{
				this._keyInfo.AppendSchemaTable(dataTable);
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000B220 File Offset: 0x00009420
		public override string GetString(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetString(i - this.VisibleFieldCount);
			}
			this.VerifyType(i, DbType.String);
			return this._activeStatement._sql.GetText(this._activeStatement, i);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000B274 File Offset: 0x00009474
		public override object GetValue(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetValue(i - this.VisibleFieldCount);
			}
			SQLiteType sqliteType = this.GetSQLiteType(i);
			return this._activeStatement._sql.GetValue(this._activeStatement, i, sqliteType);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000B2C8 File Offset: 0x000094C8
		public override int GetValues(object[] values)
		{
			int num = this.FieldCount;
			if (values.Length < num)
			{
				num = values.Length;
			}
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000B2FD File Offset: 0x000094FD
		public override bool HasRows
		{
			get
			{
				this.CheckClosed();
				return this._readingState != 1;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000B311 File Offset: 0x00009511
		public override bool IsClosed
		{
			get
			{
				return this._command == null;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000B31C File Offset: 0x0000951C
		public override bool IsDBNull(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.IsDBNull(i - this.VisibleFieldCount);
			}
			return this._activeStatement._sql.IsNull(this._activeStatement, i);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000B35C File Offset: 0x0000955C
		public override bool NextResult()
		{
			this.CheckClosed();
			SqliteStatement sqliteStatement = null;
			int num;
			for (;;)
			{
				if (this._activeStatement != null && sqliteStatement == null)
				{
					this._activeStatement._sql.Reset(this._activeStatement);
					if ((this._commandBehavior & CommandBehavior.SingleResult) != CommandBehavior.Default)
					{
						break;
					}
				}
				sqliteStatement = this._command.GetStatement(this._activeStatementIndex + 1);
				if (sqliteStatement == null)
				{
					return false;
				}
				if (this._readingState < 1)
				{
					this._readingState = 1;
				}
				this._activeStatementIndex++;
				num = sqliteStatement._sql.ColumnCount(sqliteStatement);
				if ((this._commandBehavior & CommandBehavior.SchemaOnly) != CommandBehavior.Default && num != 0)
				{
					goto IL_015F;
				}
				if (sqliteStatement._sql.Step(sqliteStatement))
				{
					goto Block_9;
				}
				if (num != 0)
				{
					goto IL_0158;
				}
				if (this._rowsAffected == -1)
				{
					this._rowsAffected = 0;
				}
				this._rowsAffected += sqliteStatement._sql.Changes;
				sqliteStatement._sql.Reset(sqliteStatement);
			}
			for (;;)
			{
				sqliteStatement = this._command.GetStatement(this._activeStatementIndex + 1);
				if (sqliteStatement == null)
				{
					break;
				}
				this._activeStatementIndex++;
				sqliteStatement._sql.Step(sqliteStatement);
				if (sqliteStatement._sql.ColumnCount(sqliteStatement) == 0)
				{
					if (this._rowsAffected == -1)
					{
						this._rowsAffected = 0;
					}
					this._rowsAffected += sqliteStatement._sql.Changes;
				}
				sqliteStatement._sql.Reset(sqliteStatement);
			}
			return false;
			Block_9:
			this._readingState = -1;
			goto IL_015F;
			IL_0158:
			this._readingState = 1;
			IL_015F:
			this._activeStatement = sqliteStatement;
			this._fieldCount = num;
			this._fieldTypeArray = null;
			if ((this._commandBehavior & CommandBehavior.KeyInfo) != CommandBehavior.Default)
			{
				this.LoadKeyInfo();
			}
			return true;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000B4F0 File Offset: 0x000096F0
		private SQLiteType GetSQLiteType(int i)
		{
			if (this._fieldTypeArray == null)
			{
				this._fieldTypeArray = new SQLiteType[this.VisibleFieldCount];
			}
			if (this._fieldTypeArray[i] == null)
			{
				this._fieldTypeArray[i] = new SQLiteType();
			}
			SQLiteType sqliteType = this._fieldTypeArray[i];
			if (sqliteType.Affinity == TypeAffinity.Uninitialized)
			{
				sqliteType.Type = SqliteConvert.TypeNameToDbType(this._activeStatement._sql.ColumnType(this._activeStatement, i, out sqliteType.Affinity));
			}
			else
			{
				sqliteType.Affinity = this._activeStatement._sql.ColumnAffinity(this._activeStatement, i);
			}
			return sqliteType;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000B588 File Offset: 0x00009788
		public override bool Read()
		{
			this.CheckClosed();
			if (this._readingState == -1)
			{
				this._readingState = 0;
				return true;
			}
			if (this._readingState == 0)
			{
				if ((this._commandBehavior & CommandBehavior.SingleRow) == CommandBehavior.Default && this._activeStatement._sql.Step(this._activeStatement))
				{
					if (this._keyInfo != null)
					{
						this._keyInfo.Reset();
					}
					return true;
				}
				this._readingState = 1;
			}
			return false;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000B5F4 File Offset: 0x000097F4
		public override int RecordsAffected
		{
			get
			{
				if (this._rowsAffected >= 0)
				{
					return this._rowsAffected;
				}
				return 0;
			}
		}

		// Token: 0x1700003C RID: 60
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x1700003D RID: 61
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000B61F File Offset: 0x0000981F
		private void LoadKeyInfo()
		{
			if (this._keyInfo != null)
			{
				this._keyInfo.Dispose();
			}
			this._keyInfo = new SqliteKeyReader(this._command.Connection, this, this._activeStatement);
		}

		// Token: 0x04000097 RID: 151
		private SqliteCommand _command;

		// Token: 0x04000098 RID: 152
		private int _activeStatementIndex;

		// Token: 0x04000099 RID: 153
		private SqliteStatement _activeStatement;

		// Token: 0x0400009A RID: 154
		private int _readingState;

		// Token: 0x0400009B RID: 155
		private int _rowsAffected;

		// Token: 0x0400009C RID: 156
		private int _fieldCount;

		// Token: 0x0400009D RID: 157
		private SQLiteType[] _fieldTypeArray;

		// Token: 0x0400009E RID: 158
		private CommandBehavior _commandBehavior;

		// Token: 0x0400009F RID: 159
		internal bool _disposeCommand;

		// Token: 0x040000A0 RID: 160
		private SqliteKeyReader _keyInfo;

		// Token: 0x040000A1 RID: 161
		internal long _version;

		// Token: 0x040000A2 RID: 162
		private static bool hasColumnMetadataSupport = true;
	}
}
