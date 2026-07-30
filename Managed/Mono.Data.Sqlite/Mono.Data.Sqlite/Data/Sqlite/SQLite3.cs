using System;
using System.Data;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000005 RID: 5
	internal class SQLite3 : SQLiteBase
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00006614 File Offset: 0x00004814
		internal SQLite3(SQLiteDateFormats fmt)
			: base(fmt)
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000661D File Offset: 0x0000481D
		protected override void Dispose(bool bDisposing)
		{
			if (bDisposing)
			{
				this.Close();
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00006628 File Offset: 0x00004828
		internal override void Close()
		{
			if (this._sql != null)
			{
				if (this._usePool)
				{
					SQLiteBase.ResetConnection(this._sql);
					SqliteConnectionPool.Add(this._fileName, this._sql, this._poolVersion);
				}
				else
				{
					this._sql.Dispose();
				}
			}
			this._sql = null;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000667B File Offset: 0x0000487B
		internal override void Cancel()
		{
			UnsafeNativeMethods.sqlite3_interrupt(this._sql);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000668D File Offset: 0x0000488D
		internal override string Version
		{
			get
			{
				return SQLite3.SQLiteVersion;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00006694 File Offset: 0x00004894
		internal static string SQLiteVersion
		{
			get
			{
				return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_libversion(), -1);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000066A1 File Offset: 0x000048A1
		internal override int Changes
		{
			get
			{
				return UnsafeNativeMethods.sqlite3_changes(this._sql);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000066B4 File Offset: 0x000048B4
		internal override void Open(string strFilename, SQLiteOpenFlagsEnum flags, int maxPoolSize, bool usePool)
		{
			if (this._sql != null)
			{
				return;
			}
			this._usePool = usePool;
			if (usePool)
			{
				this._fileName = strFilename;
				this._sql = SqliteConnectionPool.Remove(strFilename, maxPoolSize, out this._poolVersion);
			}
			if (this._sql == null)
			{
				IntPtr intPtr;
				int num;
				if (UnsafeNativeMethods.use_sqlite3_open_v2)
				{
					num = UnsafeNativeMethods.sqlite3_open_v2(SqliteConvert.ToUTF8(strFilename), out intPtr, (int)flags, IntPtr.Zero);
				}
				else
				{
					Console.WriteLine("Your sqlite3 version is old - please upgrade to at least v3.5.0!");
					num = UnsafeNativeMethods.sqlite3_open(SqliteConvert.ToUTF8(strFilename), out intPtr);
				}
				if (num > 0)
				{
					throw new SqliteException(num, null);
				}
				this._sql = intPtr;
			}
			this._functionsArray = SqliteFunction.BindFunctions(this);
			this.SetTimeout(0);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00006758 File Offset: 0x00004958
		internal override void ClearPool()
		{
			SqliteConnectionPool.ClearPool(this._fileName);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00006768 File Offset: 0x00004968
		internal override void SetTimeout(int nTimeoutMS)
		{
			int num = UnsafeNativeMethods.sqlite3_busy_timeout(this._sql, nTimeoutMS);
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00006798 File Offset: 0x00004998
		internal override bool Step(SqliteStatement stmt)
		{
			Random random = null;
			uint tickCount = (uint)Environment.TickCount;
			uint num = (uint)(stmt._command._commandTimeout * 1000);
			int num2;
			int num3;
			for (;;)
			{
				num2 = UnsafeNativeMethods.sqlite3_step(stmt._sqlite_stmt);
				if (num2 == 100)
				{
					break;
				}
				if (num2 == 101)
				{
					return false;
				}
				if (num2 > 0)
				{
					num3 = this.Reset(stmt);
					if (num3 == 0)
					{
						goto Block_4;
					}
					if ((num3 == 6 || num3 == 5) && stmt._command != null)
					{
						if (random == null)
						{
							random = new Random();
						}
						if (Environment.TickCount - (int)tickCount > (int)num)
						{
							goto Block_8;
						}
						Thread.CurrentThread.Join(random.Next(1, 150));
					}
				}
			}
			return true;
			Block_4:
			throw new SqliteException(num2, this.SQLiteLastError());
			Block_8:
			throw new SqliteException(num3, this.SQLiteLastError());
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000684C File Offset: 0x00004A4C
		internal override int Reset(SqliteStatement stmt)
		{
			int num = UnsafeNativeMethods.sqlite3_reset(stmt._sqlite_stmt);
			if (num == 17)
			{
				string text;
				using (SqliteStatement sqliteStatement = this.Prepare(null, stmt._sqlStatement, null, (uint)(stmt._command._commandTimeout * 1000), out text))
				{
					stmt._sqlite_stmt.Dispose();
					stmt._sqlite_stmt = sqliteStatement._sqlite_stmt;
					sqliteStatement._sqlite_stmt = null;
					stmt.BindParameters();
				}
				return -1;
			}
			if (num == 6 || num == 5)
			{
				return num;
			}
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
			return 0;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000068F0 File Offset: 0x00004AF0
		internal override string SQLiteLastError()
		{
			return SQLiteBase.SQLiteLastError(this._sql);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00006900 File Offset: 0x00004B00
		internal override SqliteStatement Prepare(SqliteConnection cnn, string strSql, SqliteStatement previous, uint timeoutMS, out string strRemain)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			int num = 0;
			int num2 = 17;
			int num3 = 0;
			byte[] array = SqliteConvert.ToUTF8(strSql);
			SqliteStatement sqliteStatement = null;
			Random random = null;
			uint tickCount = (uint)Environment.TickCount;
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			SqliteStatement sqliteStatement2;
			try
			{
				while ((num2 == 17 || num2 == 6 || num2 == 5) && num3 < 3)
				{
					num2 = UnsafeNativeMethods.sqlite3_prepare(this._sql, intPtr, array.Length - 1, out zero, out zero2);
					num = -1;
					if (num2 == 17)
					{
						num3++;
					}
					else
					{
						if (num2 == 1)
						{
							if (string.Compare(this.SQLiteLastError(), "near \"TYPES\": syntax error", StringComparison.OrdinalIgnoreCase) == 0)
							{
								int num4 = strSql.IndexOf(';');
								if (num4 == -1)
								{
									num4 = strSql.Length - 1;
								}
								string text = strSql.Substring(0, num4 + 1);
								strSql = strSql.Substring(num4 + 1);
								strRemain = "";
								while (sqliteStatement == null && strSql.Length > 0)
								{
									sqliteStatement = this.Prepare(cnn, strSql, previous, timeoutMS, out strRemain);
									strSql = strRemain;
								}
								if (sqliteStatement != null)
								{
									sqliteStatement.SetTypes(text);
								}
								return sqliteStatement;
							}
							if (this._buildingSchema || string.Compare(this.SQLiteLastError(), 0, "no such table: TEMP.SCHEMA", 0, 26, StringComparison.OrdinalIgnoreCase) != 0)
							{
								continue;
							}
							strRemain = "";
							this._buildingSchema = true;
							try
							{
								ISQLiteSchemaExtensions isqliteSchemaExtensions = ((IServiceProvider)SqliteFactory.Instance).GetService(typeof(ISQLiteSchemaExtensions)) as ISQLiteSchemaExtensions;
								if (isqliteSchemaExtensions != null)
								{
									isqliteSchemaExtensions.BuildTempSchema(cnn);
								}
								while (sqliteStatement == null && strSql.Length > 0)
								{
									sqliteStatement = this.Prepare(cnn, strSql, previous, timeoutMS, out strRemain);
									strSql = strRemain;
								}
								return sqliteStatement;
							}
							finally
							{
								this._buildingSchema = false;
							}
						}
						if (num2 == 6 || num2 == 5)
						{
							if (random == null)
							{
								random = new Random();
							}
							if (Environment.TickCount - (int)tickCount > (int)timeoutMS)
							{
								throw new SqliteException(num2, this.SQLiteLastError());
							}
							Thread.CurrentThread.Join(random.Next(1, 150));
						}
					}
				}
				if (num2 > 0)
				{
					throw new SqliteException(num2, this.SQLiteLastError());
				}
				strRemain = SqliteConvert.UTF8ToString(zero2, num);
				if (zero != IntPtr.Zero)
				{
					sqliteStatement = new SqliteStatement(this, zero, strSql.Substring(0, strSql.Length - strRemain.Length), previous);
				}
				sqliteStatement2 = sqliteStatement;
			}
			finally
			{
				gchandle.Free();
			}
			return sqliteStatement2;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00006B88 File Offset: 0x00004D88
		internal override void Bind_Double(SqliteStatement stmt, int index, double value)
		{
			int num = UnsafeNativeMethods.sqlite3_bind_double(stmt._sqlite_stmt, index, value);
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00006BBC File Offset: 0x00004DBC
		internal override void Bind_Int32(SqliteStatement stmt, int index, int value)
		{
			int num = UnsafeNativeMethods.sqlite3_bind_int(stmt._sqlite_stmt, index, value);
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00006BF0 File Offset: 0x00004DF0
		internal override void Bind_Int64(SqliteStatement stmt, int index, long value)
		{
			int num = UnsafeNativeMethods.sqlite3_bind_int64(stmt._sqlite_stmt, index, value);
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00006C24 File Offset: 0x00004E24
		internal override void Bind_Text(SqliteStatement stmt, int index, string value)
		{
			byte[] array = SqliteConvert.ToUTF8(value);
			int num = UnsafeNativeMethods.sqlite3_bind_text(stmt._sqlite_stmt, index, array, array.Length - 1, (IntPtr)(-1));
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00006C68 File Offset: 0x00004E68
		internal override void Bind_DateTime(SqliteStatement stmt, int index, DateTime dt)
		{
			byte[] array = base.ToUTF8(dt);
			int num = UnsafeNativeMethods.sqlite3_bind_text(stmt._sqlite_stmt, index, array, array.Length - 1, (IntPtr)(-1));
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00006CAC File Offset: 0x00004EAC
		internal override void Bind_Blob(SqliteStatement stmt, int index, byte[] blobData)
		{
			int num = UnsafeNativeMethods.sqlite3_bind_blob(stmt._sqlite_stmt, index, blobData, blobData.Length, (IntPtr)(-1));
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00006CE8 File Offset: 0x00004EE8
		internal override void Bind_Null(SqliteStatement stmt, int index)
		{
			int num = UnsafeNativeMethods.sqlite3_bind_null(stmt._sqlite_stmt, index);
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00006D18 File Offset: 0x00004F18
		internal override int Bind_ParamCount(SqliteStatement stmt)
		{
			return UnsafeNativeMethods.sqlite3_bind_parameter_count(stmt._sqlite_stmt);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00006D2A File Offset: 0x00004F2A
		internal override string Bind_ParamName(SqliteStatement stmt, int index)
		{
			return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_bind_parameter_name(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00006D43 File Offset: 0x00004F43
		internal override int Bind_ParamIndex(SqliteStatement stmt, string paramName)
		{
			return UnsafeNativeMethods.sqlite3_bind_parameter_index(stmt._sqlite_stmt, SqliteConvert.ToUTF8(paramName));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00006D5B File Offset: 0x00004F5B
		internal override int ColumnCount(SqliteStatement stmt)
		{
			return UnsafeNativeMethods.sqlite3_column_count(stmt._sqlite_stmt);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00006D6D File Offset: 0x00004F6D
		internal override string ColumnName(SqliteStatement stmt, int index)
		{
			return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_column_name(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00006D86 File Offset: 0x00004F86
		internal override TypeAffinity ColumnAffinity(SqliteStatement stmt, int index)
		{
			return UnsafeNativeMethods.sqlite3_column_type(stmt._sqlite_stmt, index);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00006D9C File Offset: 0x00004F9C
		internal override string ColumnType(SqliteStatement stmt, int index, out TypeAffinity nAffinity)
		{
			int num = -1;
			IntPtr intPtr = UnsafeNativeMethods.sqlite3_column_decltype(stmt._sqlite_stmt, index);
			nAffinity = this.ColumnAffinity(stmt, index);
			if (intPtr != IntPtr.Zero)
			{
				return SqliteConvert.UTF8ToString(intPtr, num);
			}
			string[] typeDefinitions = stmt.TypeDefinitions;
			if (typeDefinitions != null && index < typeDefinitions.Length && typeDefinitions[index] != null)
			{
				return typeDefinitions[index];
			}
			return string.Empty;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00006DFC File Offset: 0x00004FFC
		internal override int ColumnIndex(SqliteStatement stmt, string columnName)
		{
			int num = this.ColumnCount(stmt);
			for (int i = 0; i < num; i++)
			{
				if (string.Compare(columnName, this.ColumnName(stmt, i), true, CultureInfo.InvariantCulture) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00006E36 File Offset: 0x00005036
		internal override string ColumnOriginalName(SqliteStatement stmt, int index)
		{
			return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_column_origin_name(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00006E4F File Offset: 0x0000504F
		internal override string ColumnDatabaseName(SqliteStatement stmt, int index)
		{
			return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_column_database_name(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00006E68 File Offset: 0x00005068
		internal override string ColumnTableName(SqliteStatement stmt, int index)
		{
			return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_column_table_name(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00006E84 File Offset: 0x00005084
		internal override void ColumnMetaData(string dataBase, string table, string column, out string dataType, out string collateSequence, out bool notNull, out bool primaryKey, out bool autoIncrement)
		{
			int num = -1;
			int num2 = -1;
			IntPtr intPtr;
			IntPtr intPtr2;
			int num4;
			int num5;
			int num6;
			int num3 = UnsafeNativeMethods.sqlite3_table_column_metadata(this._sql, SqliteConvert.ToUTF8(dataBase), SqliteConvert.ToUTF8(table), SqliteConvert.ToUTF8(column), out intPtr, out intPtr2, out num4, out num5, out num6);
			if (num3 > 0)
			{
				throw new SqliteException(num3, this.SQLiteLastError());
			}
			dataType = SqliteConvert.UTF8ToString(intPtr, num);
			collateSequence = SqliteConvert.UTF8ToString(intPtr2, num2);
			notNull = num4 == 1;
			primaryKey = num5 == 1;
			autoIncrement = num6 == 1;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00006F04 File Offset: 0x00005104
		internal override double GetDouble(SqliteStatement stmt, int index)
		{
			return UnsafeNativeMethods.sqlite3_column_double(stmt._sqlite_stmt, index);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00006F17 File Offset: 0x00005117
		internal override int GetInt32(SqliteStatement stmt, int index)
		{
			return UnsafeNativeMethods.sqlite3_column_int(stmt._sqlite_stmt, index);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00006F2A File Offset: 0x0000512A
		internal override long GetInt64(SqliteStatement stmt, int index)
		{
			return UnsafeNativeMethods.sqlite3_column_int64(stmt._sqlite_stmt, index);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00006F3D File Offset: 0x0000513D
		internal override string GetText(SqliteStatement stmt, int index)
		{
			return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_column_text(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00006F56 File Offset: 0x00005156
		internal override DateTime GetDateTime(SqliteStatement stmt, int index)
		{
			return base.ToDateTime(UnsafeNativeMethods.sqlite3_column_text(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00006F70 File Offset: 0x00005170
		internal unsafe override long GetBytes(SqliteStatement stmt, int index, int nDataOffset, byte[] bDest, int nStart, int nLength)
		{
			int num = nLength;
			int num2 = UnsafeNativeMethods.sqlite3_column_bytes(stmt._sqlite_stmt, index);
			IntPtr intPtr = UnsafeNativeMethods.sqlite3_column_blob(stmt._sqlite_stmt, index);
			if (bDest == null)
			{
				return (long)num2;
			}
			if (num + nStart > bDest.Length)
			{
				num = bDest.Length - nStart;
			}
			if (num + nDataOffset > num2)
			{
				num = num2 - nDataOffset;
			}
			if (num > 0)
			{
				Marshal.Copy((IntPtr)((void*)((byte*)(void*)intPtr + nDataOffset)), bDest, nStart, num);
			}
			else
			{
				num = 0;
			}
			return (long)num;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00006FE8 File Offset: 0x000051E8
		internal override long GetChars(SqliteStatement stmt, int index, int nDataOffset, char[] bDest, int nStart, int nLength)
		{
			int num = nLength;
			string text = this.GetText(stmt, index);
			int length = text.Length;
			if (bDest == null)
			{
				return (long)length;
			}
			if (num + nStart > bDest.Length)
			{
				num = bDest.Length - nStart;
			}
			if (num + nDataOffset > length)
			{
				num = length - nDataOffset;
			}
			if (num > 0)
			{
				text.CopyTo(nDataOffset, bDest, nStart, num);
			}
			else
			{
				num = 0;
			}
			return (long)num;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00007041 File Offset: 0x00005241
		internal override bool IsNull(SqliteStatement stmt, int index)
		{
			return this.ColumnAffinity(stmt, index) == TypeAffinity.Null;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000704E File Offset: 0x0000524E
		internal override int AggregateCount(IntPtr context)
		{
			return UnsafeNativeMethods.sqlite3_aggregate_count(context);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00007058 File Offset: 0x00005258
		internal override void CreateFunction(string strFunction, int nArgs, bool needCollSeq, SQLiteCallback func, SQLiteCallback funcstep, SQLiteFinalCallback funcfinal)
		{
			int num = UnsafeNativeMethods.sqlite3_create_function(this._sql, SqliteConvert.ToUTF8(strFunction), nArgs, 4, IntPtr.Zero, func, funcstep, funcfinal);
			if (num == 0)
			{
				num = UnsafeNativeMethods.sqlite3_create_function(this._sql, SqliteConvert.ToUTF8(strFunction), nArgs, 1, IntPtr.Zero, func, funcstep, funcfinal);
			}
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000070C4 File Offset: 0x000052C4
		internal override void CreateCollation(string strCollation, SQLiteCollation func, SQLiteCollation func16, IntPtr user_data)
		{
			int num = UnsafeNativeMethods.sqlite3_create_collation(this._sql, SqliteConvert.ToUTF8(strCollation), 2, user_data, func16);
			if (num == 0)
			{
				UnsafeNativeMethods.sqlite3_create_collation(this._sql, SqliteConvert.ToUTF8(strCollation), 1, user_data, func);
			}
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000711B File Offset: 0x0000531B
		internal override int ContextCollateCompare(CollationEncodingEnum enc, IntPtr context, string s1, string s2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00007122 File Offset: 0x00005322
		internal override int ContextCollateCompare(CollationEncodingEnum enc, IntPtr context, char[] c1, char[] c2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00007129 File Offset: 0x00005329
		internal override CollationSequence GetCollationSequence(SqliteFunction func, IntPtr context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00007130 File Offset: 0x00005330
		internal unsafe override long GetParamValueBytes(IntPtr p, int nDataOffset, byte[] bDest, int nStart, int nLength)
		{
			int num = nLength;
			int num2 = UnsafeNativeMethods.sqlite3_value_bytes(p);
			IntPtr intPtr = UnsafeNativeMethods.sqlite3_value_blob(p);
			if (bDest == null)
			{
				return (long)num2;
			}
			if (num + nStart > bDest.Length)
			{
				num = bDest.Length - nStart;
			}
			if (num + nDataOffset > num2)
			{
				num = num2 - nDataOffset;
			}
			if (num > 0)
			{
				Marshal.Copy((IntPtr)((void*)((byte*)(void*)intPtr + nDataOffset)), bDest, nStart, num);
			}
			else
			{
				num = 0;
			}
			return (long)num;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000718E File Offset: 0x0000538E
		internal override double GetParamValueDouble(IntPtr ptr)
		{
			return UnsafeNativeMethods.sqlite3_value_double(ptr);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00007196 File Offset: 0x00005396
		internal override int GetParamValueInt32(IntPtr ptr)
		{
			return UnsafeNativeMethods.sqlite3_value_int(ptr);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000719E File Offset: 0x0000539E
		internal override long GetParamValueInt64(IntPtr ptr)
		{
			return UnsafeNativeMethods.sqlite3_value_int64(ptr);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000071A6 File Offset: 0x000053A6
		internal override string GetParamValueText(IntPtr ptr)
		{
			return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_value_text(ptr), -1);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000071B4 File Offset: 0x000053B4
		internal override TypeAffinity GetParamValueType(IntPtr ptr)
		{
			return UnsafeNativeMethods.sqlite3_value_type(ptr);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000071BC File Offset: 0x000053BC
		internal override void ReturnBlob(IntPtr context, byte[] value)
		{
			UnsafeNativeMethods.sqlite3_result_blob(context, value, value.Length, (IntPtr)(-1));
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000071CE File Offset: 0x000053CE
		internal override void ReturnDouble(IntPtr context, double value)
		{
			UnsafeNativeMethods.sqlite3_result_double(context, value);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000071D7 File Offset: 0x000053D7
		internal override void ReturnError(IntPtr context, string value)
		{
			UnsafeNativeMethods.sqlite3_result_error(context, SqliteConvert.ToUTF8(value), value.Length);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000071EB File Offset: 0x000053EB
		internal override void ReturnInt32(IntPtr context, int value)
		{
			UnsafeNativeMethods.sqlite3_result_int(context, value);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000071F4 File Offset: 0x000053F4
		internal override void ReturnInt64(IntPtr context, long value)
		{
			UnsafeNativeMethods.sqlite3_result_int64(context, value);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000071FD File Offset: 0x000053FD
		internal override void ReturnNull(IntPtr context)
		{
			UnsafeNativeMethods.sqlite3_result_null(context);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00007208 File Offset: 0x00005408
		internal override void ReturnText(IntPtr context, string value)
		{
			byte[] array = SqliteConvert.ToUTF8(value);
			UnsafeNativeMethods.sqlite3_result_text(context, SqliteConvert.ToUTF8(value), array.Length - 1, (IntPtr)(-1));
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00007233 File Offset: 0x00005433
		internal override IntPtr AggregateContext(IntPtr context)
		{
			return UnsafeNativeMethods.sqlite3_aggregate_context(context, 1);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000723C File Offset: 0x0000543C
		internal override void SetPassword(byte[] passwordBytes)
		{
			int num = UnsafeNativeMethods.sqlite3_key(this._sql, passwordBytes, passwordBytes.Length);
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00007270 File Offset: 0x00005470
		internal override void ChangePassword(byte[] newPasswordBytes)
		{
			int num = UnsafeNativeMethods.sqlite3_rekey(this._sql, newPasswordBytes, (newPasswordBytes == null) ? 0 : newPasswordBytes.Length);
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000072A9 File Offset: 0x000054A9
		internal override void SetUpdateHook(SQLiteUpdateCallback func)
		{
			UnsafeNativeMethods.sqlite3_update_hook(this._sql, func, IntPtr.Zero);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000072C2 File Offset: 0x000054C2
		internal override void SetCommitHook(SQLiteCommitCallback func)
		{
			UnsafeNativeMethods.sqlite3_commit_hook(this._sql, func, IntPtr.Zero);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000072DB File Offset: 0x000054DB
		internal override void SetRollbackHook(SQLiteRollbackCallback func)
		{
			UnsafeNativeMethods.sqlite3_rollback_hook(this._sql, func, IntPtr.Zero);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000072F4 File Offset: 0x000054F4
		internal override object GetValue(SqliteStatement stmt, int index, SQLiteType typ)
		{
			if (this.IsNull(stmt, index))
			{
				return DBNull.Value;
			}
			TypeAffinity typeAffinity = typ.Affinity;
			Type type = null;
			if (typ.Type != DbType.Object)
			{
				type = SqliteConvert.SQLiteTypeToType(typ);
				typeAffinity = SqliteConvert.TypeToAffinity(type);
			}
			switch (typeAffinity)
			{
			case TypeAffinity.Int64:
				if (type == null)
				{
					return this.GetInt64(stmt, index);
				}
				return Convert.ChangeType(this.GetInt64(stmt, index), type, null);
			case TypeAffinity.Double:
				if (type == null)
				{
					return this.GetDouble(stmt, index);
				}
				return Convert.ChangeType(this.GetDouble(stmt, index), type, null);
			case TypeAffinity.Text:
				break;
			case TypeAffinity.Blob:
			{
				if (typ.Type == DbType.Guid && typ.Affinity == TypeAffinity.Text)
				{
					return new Guid(this.GetText(stmt, index));
				}
				int num = (int)this.GetBytes(stmt, index, 0, null, 0, 0);
				byte[] array = new byte[num];
				this.GetBytes(stmt, index, 0, array, 0, num);
				if (typ.Type == DbType.Guid && num == 16)
				{
					return new Guid(array);
				}
				return array;
			}
			default:
				if (typeAffinity == TypeAffinity.DateTime)
				{
					return this.GetDateTime(stmt, index);
				}
				break;
			}
			return this.GetText(stmt, index);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00007427 File Offset: 0x00005627
		internal override int GetCursorForTable(SqliteStatement stmt, int db, int rootPage)
		{
			return -1;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000742A File Offset: 0x0000562A
		internal override long GetRowIdForCursor(SqliteStatement stmt, int cursor)
		{
			return 0L;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000742E File Offset: 0x0000562E
		internal override void GetIndexColumnExtendedInfo(string database, string index, string column, out int sortMode, out int onError, out string collationSequence)
		{
			sortMode = 0;
			onError = 2;
			collationSequence = "BINARY";
		}

		// Token: 0x04000042 RID: 66
		protected SqliteConnectionHandle _sql;

		// Token: 0x04000043 RID: 67
		protected string _fileName;

		// Token: 0x04000044 RID: 68
		protected bool _usePool;

		// Token: 0x04000045 RID: 69
		protected int _poolVersion;

		// Token: 0x04000046 RID: 70
		private bool _buildingSchema;

		// Token: 0x04000047 RID: 71
		protected SqliteFunction[] _functionsArray;
	}
}
