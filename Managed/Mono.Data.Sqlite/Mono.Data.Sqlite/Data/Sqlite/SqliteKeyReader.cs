using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200002D RID: 45
	internal sealed class SqliteKeyReader : IDisposable
	{
		// Token: 0x06000216 RID: 534 RVA: 0x0000C14C File Offset: 0x0000A34C
		internal SqliteKeyReader(SqliteConnection cnn, SqliteDataReader reader, SqliteStatement stmt)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			Dictionary<string, List<string>> dictionary2 = new Dictionary<string, List<string>>();
			List<SqliteKeyReader.KeyInfo> list = new List<SqliteKeyReader.KeyInfo>();
			this._stmt = stmt;
			using (DataTable schema = cnn.GetSchema("Catalogs"))
			{
				foreach (object obj in schema.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dictionary.Add((string)dataRow["CATALOG_NAME"], Convert.ToInt32(dataRow["ID"]));
				}
			}
			using (DataTable schemaTable = reader.GetSchemaTable(false, false))
			{
				foreach (object obj2 in schemaTable.Rows)
				{
					DataRow dataRow2 = (DataRow)obj2;
					if (dataRow2[SchemaTableOptionalColumn.BaseCatalogName] != DBNull.Value)
					{
						string text = (string)dataRow2[SchemaTableOptionalColumn.BaseCatalogName];
						string text2 = (string)dataRow2[SchemaTableColumn.BaseTableName];
						List<string> list2;
						if (!dictionary2.ContainsKey(text))
						{
							list2 = new List<string>();
							dictionary2.Add(text, list2);
						}
						else
						{
							list2 = dictionary2[text];
						}
						if (!list2.Contains(text2))
						{
							list2.Add(text2);
						}
					}
				}
				foreach (KeyValuePair<string, List<string>> keyValuePair in dictionary2)
				{
					for (int i = 0; i < keyValuePair.Value.Count; i++)
					{
						string text3 = keyValuePair.Value[i];
						DataRow dataRow3 = null;
						using (DataTable schema2 = cnn.GetSchema("Indexes", new string[] { keyValuePair.Key, null, text3 }))
						{
							int num = 0;
							while (num < 2 && dataRow3 == null)
							{
								foreach (object obj3 in schema2.Rows)
								{
									DataRow dataRow4 = (DataRow)obj3;
									if (num == 0 && (bool)dataRow4["PRIMARY_KEY"])
									{
										dataRow3 = dataRow4;
										break;
									}
									if (num == 1 && (bool)dataRow4["UNIQUE"])
									{
										dataRow3 = dataRow4;
										break;
									}
								}
								num++;
							}
							if (dataRow3 == null)
							{
								keyValuePair.Value.RemoveAt(i);
								i--;
							}
							else
							{
								using (DataTable schema3 = cnn.GetSchema("Tables", new string[] { keyValuePair.Key, null, text3 }))
								{
									int num2 = dictionary[keyValuePair.Key];
									int num3 = Convert.ToInt32(schema3.Rows[0]["TABLE_ROOTPAGE"]);
									int cursorForTable = stmt._sql.GetCursorForTable(stmt, num2, num3);
									using (DataTable schema4 = cnn.GetSchema("IndexColumns", new string[]
									{
										keyValuePair.Key,
										null,
										text3,
										(string)dataRow3["INDEX_NAME"]
									}))
									{
										SqliteKeyReader.KeyQuery keyQuery = null;
										List<string> list3 = new List<string>();
										for (int j = 0; j < schema4.Rows.Count; j++)
										{
											bool flag = true;
											foreach (object obj4 in schemaTable.Rows)
											{
												DataRow dataRow5 = (DataRow)obj4;
												if (!dataRow5.IsNull(SchemaTableColumn.BaseColumnName) && (string)dataRow5[SchemaTableColumn.BaseColumnName] == (string)schema4.Rows[j]["COLUMN_NAME"] && (string)dataRow5[SchemaTableColumn.BaseTableName] == text3 && (string)dataRow5[SchemaTableOptionalColumn.BaseCatalogName] == keyValuePair.Key)
												{
													schema4.Rows.RemoveAt(j);
													j--;
													flag = false;
													break;
												}
											}
											if (flag)
											{
												list3.Add((string)schema4.Rows[j]["COLUMN_NAME"]);
											}
										}
										if ((string)dataRow3["INDEX_NAME"] != "sqlite_master_PK_" + text3 && list3.Count > 0)
										{
											string[] array = new string[list3.Count];
											list3.CopyTo(array);
											keyQuery = new SqliteKeyReader.KeyQuery(cnn, keyValuePair.Key, text3, array);
										}
										for (int k = 0; k < schema4.Rows.Count; k++)
										{
											string text4 = (string)schema4.Rows[k]["COLUMN_NAME"];
											list.Add(new SqliteKeyReader.KeyInfo
											{
												rootPage = num3,
												cursor = cursorForTable,
												database = num2,
												databaseName = keyValuePair.Key,
												tableName = text3,
												columnName = text4,
												query = keyQuery,
												column = k
											});
										}
									}
								}
							}
						}
					}
				}
			}
			this._keyInfo = new SqliteKeyReader.KeyInfo[list.Count];
			list.CopyTo(this._keyInfo);
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		internal int Count
		{
			get
			{
				if (this._keyInfo != null)
				{
					return this._keyInfo.Length;
				}
				return 0;
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000C7FC File Offset: 0x0000A9FC
		internal void Sync(int i)
		{
			this.Sync();
			if (this._keyInfo[i].cursor == -1)
			{
				throw new InvalidCastException();
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000C820 File Offset: 0x0000AA20
		internal void Sync()
		{
			if (this._isValid)
			{
				return;
			}
			SqliteKeyReader.KeyQuery keyQuery = null;
			for (int i = 0; i < this._keyInfo.Length; i++)
			{
				if (this._keyInfo[i].query == null || this._keyInfo[i].query != keyQuery)
				{
					keyQuery = this._keyInfo[i].query;
					if (keyQuery != null)
					{
						keyQuery.Sync(this._stmt._sql.GetRowIdForCursor(this._stmt, this._keyInfo[i].cursor));
					}
				}
			}
			this._isValid = true;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000C8BC File Offset: 0x0000AABC
		internal void Reset()
		{
			this._isValid = false;
			if (this._keyInfo == null)
			{
				return;
			}
			for (int i = 0; i < this._keyInfo.Length; i++)
			{
				if (this._keyInfo[i].query != null)
				{
					this._keyInfo[i].query.IsValid = false;
				}
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000C918 File Offset: 0x0000AB18
		public void Dispose()
		{
			this._stmt = null;
			if (this._keyInfo == null)
			{
				return;
			}
			for (int i = 0; i < this._keyInfo.Length; i++)
			{
				if (this._keyInfo[i].query != null)
				{
					this._keyInfo[i].query.Dispose();
				}
			}
			this._keyInfo = null;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000C978 File Offset: 0x0000AB78
		internal string GetDataTypeName(int i)
		{
			this.Sync();
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetDataTypeName(this._keyInfo[i].column);
			}
			return "integer";
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
		internal Type GetFieldType(int i)
		{
			this.Sync();
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetFieldType(this._keyInfo[i].column);
			}
			return typeof(long);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000CA2D File Offset: 0x0000AC2D
		internal string GetName(int i)
		{
			return this._keyInfo[i].columnName;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000CA40 File Offset: 0x0000AC40
		internal int GetOrdinal(string name)
		{
			for (int i = 0; i < this._keyInfo.Length; i++)
			{
				if (string.Compare(name, this._keyInfo[i].columnName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000CA80 File Offset: 0x0000AC80
		internal bool GetBoolean(int i)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetBoolean(this._keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000CADC File Offset: 0x0000ACDC
		internal byte GetByte(int i)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetByte(this._keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000CB38 File Offset: 0x0000AD38
		internal long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetBytes(this._keyInfo[i].column, fieldOffset, buffer, bufferoffset, length);
			}
			throw new InvalidCastException();
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000CB98 File Offset: 0x0000AD98
		internal char GetChar(int i)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetChar(this._keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000CBF4 File Offset: 0x0000ADF4
		internal long GetChars(int i, long fieldOffset, char[] buffer, int bufferoffset, int length)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetChars(this._keyInfo[i].column, fieldOffset, buffer, bufferoffset, length);
			}
			throw new InvalidCastException();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000CC54 File Offset: 0x0000AE54
		internal DateTime GetDateTime(int i)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetDateTime(this._keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000CCB0 File Offset: 0x0000AEB0
		internal decimal GetDecimal(int i)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetDecimal(this._keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000CD0C File Offset: 0x0000AF0C
		internal double GetDouble(int i)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetDouble(this._keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000CD68 File Offset: 0x0000AF68
		internal float GetFloat(int i)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetFloat(this._keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
		internal Guid GetGuid(int i)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetGuid(this._keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000CE20 File Offset: 0x0000B020
		internal short GetInt16(int i)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetInt16(this._keyInfo[i].column);
			}
			long rowIdForCursor = this._stmt._sql.GetRowIdForCursor(this._stmt, this._keyInfo[i].cursor);
			if (rowIdForCursor == 0L)
			{
				throw new InvalidCastException();
			}
			return Convert.ToInt16(rowIdForCursor);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000CEAC File Offset: 0x0000B0AC
		internal int GetInt32(int i)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetInt32(this._keyInfo[i].column);
			}
			long rowIdForCursor = this._stmt._sql.GetRowIdForCursor(this._stmt, this._keyInfo[i].cursor);
			if (rowIdForCursor == 0L)
			{
				throw new InvalidCastException();
			}
			return Convert.ToInt32(rowIdForCursor);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000CF38 File Offset: 0x0000B138
		internal long GetInt64(int i)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetInt64(this._keyInfo[i].column);
			}
			long rowIdForCursor = this._stmt._sql.GetRowIdForCursor(this._stmt, this._keyInfo[i].cursor);
			if (rowIdForCursor == 0L)
			{
				throw new InvalidCastException();
			}
			return Convert.ToInt64(rowIdForCursor);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000CFC4 File Offset: 0x0000B1C4
		internal string GetString(int i)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetString(this._keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000D020 File Offset: 0x0000B220
		internal object GetValue(int i)
		{
			if (this._keyInfo[i].cursor == -1)
			{
				return DBNull.Value;
			}
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetValue(this._keyInfo[i].column);
			}
			if (this.IsDBNull(i))
			{
				return DBNull.Value;
			}
			return this.GetInt64(i);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000D0AC File Offset: 0x0000B2AC
		internal bool IsDBNull(int i)
		{
			if (this._keyInfo[i].cursor == -1)
			{
				return true;
			}
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.IsDBNull(this._keyInfo[i].column);
			}
			return this._stmt._sql.GetRowIdForCursor(this._stmt, this._keyInfo[i].cursor) == 0L;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000D144 File Offset: 0x0000B344
		internal void AppendSchemaTable(DataTable tbl)
		{
			SqliteKeyReader.KeyQuery keyQuery = null;
			for (int i = 0; i < this._keyInfo.Length; i++)
			{
				if (this._keyInfo[i].query == null || this._keyInfo[i].query != keyQuery)
				{
					keyQuery = this._keyInfo[i].query;
					if (keyQuery == null)
					{
						DataRow dataRow = tbl.NewRow();
						dataRow[SchemaTableColumn.ColumnName] = this._keyInfo[i].columnName;
						dataRow[SchemaTableColumn.ColumnOrdinal] = tbl.Rows.Count;
						dataRow[SchemaTableColumn.ColumnSize] = 8;
						dataRow[SchemaTableColumn.NumericPrecision] = 255;
						dataRow[SchemaTableColumn.NumericScale] = 255;
						dataRow[SchemaTableColumn.ProviderType] = DbType.Int64;
						dataRow[SchemaTableColumn.IsLong] = false;
						dataRow[SchemaTableColumn.AllowDBNull] = false;
						dataRow[SchemaTableOptionalColumn.IsReadOnly] = false;
						dataRow[SchemaTableOptionalColumn.IsRowVersion] = false;
						dataRow[SchemaTableColumn.IsUnique] = false;
						dataRow[SchemaTableColumn.IsKey] = true;
						dataRow[SchemaTableColumn.DataType] = typeof(long);
						dataRow[SchemaTableOptionalColumn.IsHidden] = true;
						dataRow[SchemaTableColumn.BaseColumnName] = this._keyInfo[i].columnName;
						dataRow[SchemaTableColumn.IsExpression] = false;
						dataRow[SchemaTableColumn.IsAliased] = false;
						dataRow[SchemaTableColumn.BaseTableName] = this._keyInfo[i].tableName;
						dataRow[SchemaTableOptionalColumn.BaseCatalogName] = this._keyInfo[i].databaseName;
						dataRow[SchemaTableOptionalColumn.IsAutoIncrement] = true;
						dataRow["DataTypeName"] = "integer";
						tbl.Rows.Add(dataRow);
					}
					else
					{
						keyQuery.Sync(0L);
						using (DataTable schemaTable = keyQuery._reader.GetSchemaTable())
						{
							foreach (object obj in schemaTable.Rows)
							{
								object[] itemArray = ((DataRow)obj).ItemArray;
								DataRow dataRow2 = tbl.Rows.Add(itemArray);
								dataRow2[SchemaTableOptionalColumn.IsHidden] = true;
								dataRow2[SchemaTableColumn.ColumnOrdinal] = tbl.Rows.Count - 1;
							}
						}
					}
				}
			}
		}

		// Token: 0x040000E4 RID: 228
		private SqliteKeyReader.KeyInfo[] _keyInfo;

		// Token: 0x040000E5 RID: 229
		private SqliteStatement _stmt;

		// Token: 0x040000E6 RID: 230
		private bool _isValid;

		// Token: 0x0200003C RID: 60
		private struct KeyInfo
		{
			// Token: 0x04000115 RID: 277
			internal string databaseName;

			// Token: 0x04000116 RID: 278
			internal string tableName;

			// Token: 0x04000117 RID: 279
			internal string columnName;

			// Token: 0x04000118 RID: 280
			internal int database;

			// Token: 0x04000119 RID: 281
			internal int rootPage;

			// Token: 0x0400011A RID: 282
			internal int cursor;

			// Token: 0x0400011B RID: 283
			internal SqliteKeyReader.KeyQuery query;

			// Token: 0x0400011C RID: 284
			internal int column;
		}

		// Token: 0x0200003D RID: 61
		private sealed class KeyQuery : IDisposable
		{
			// Token: 0x060002F4 RID: 756 RVA: 0x0000E628 File Offset: 0x0000C828
			internal KeyQuery(SqliteConnection cnn, string database, string table, params string[] columns)
			{
				using (SqliteCommandBuilder sqliteCommandBuilder = new SqliteCommandBuilder())
				{
					this._command = cnn.CreateCommand();
					for (int i = 0; i < columns.Length; i++)
					{
						columns[i] = sqliteCommandBuilder.QuoteIdentifier(columns[i]);
					}
				}
				this._command.CommandText = string.Format("SELECT {0} FROM [{1}].[{2}] WHERE ROWID = ?", string.Join(",", columns), database, table);
				this._command.Parameters.AddWithValue(null, 0L);
			}

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000E6C4 File Offset: 0x0000C8C4
			// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000E6CF File Offset: 0x0000C8CF
			internal bool IsValid
			{
				get
				{
					return this._reader != null;
				}
				set
				{
					if (value)
					{
						throw new ArgumentException();
					}
					if (this._reader != null)
					{
						this._reader.Dispose();
						this._reader = null;
					}
				}
			}

			// Token: 0x060002F7 RID: 759 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
			internal void Sync(long rowid)
			{
				this.IsValid = false;
				this._command.Parameters[0].Value = rowid;
				this._reader = this._command.ExecuteReader();
				this._reader.Read();
			}

			// Token: 0x060002F8 RID: 760 RVA: 0x0000E741 File Offset: 0x0000C941
			public void Dispose()
			{
				this.IsValid = false;
				if (this._command != null)
				{
					this._command.Dispose();
				}
				this._command = null;
			}

			// Token: 0x0400011D RID: 285
			private SqliteCommand _command;

			// Token: 0x0400011E RID: 286
			internal SqliteDataReader _reader;
		}
	}
}
