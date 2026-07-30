using System;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000007 RID: 7
	internal abstract class SQLiteBase : SqliteConvert, IDisposable
	{
		// Token: 0x060000AA RID: 170 RVA: 0x0000761A File Offset: 0x0000581A
		internal SQLiteBase(SQLiteDateFormats fmt)
			: base(fmt)
		{
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000AB RID: 171
		internal abstract string Version { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000AC RID: 172
		internal abstract int Changes { get; }

		// Token: 0x060000AD RID: 173
		internal abstract void Open(string strFilename, SQLiteOpenFlagsEnum flags, int maxPoolSize, bool usePool);

		// Token: 0x060000AE RID: 174
		internal abstract void Close();

		// Token: 0x060000AF RID: 175
		internal abstract void SetTimeout(int nTimeoutMS);

		// Token: 0x060000B0 RID: 176
		internal abstract string SQLiteLastError();

		// Token: 0x060000B1 RID: 177
		internal abstract void ClearPool();

		// Token: 0x060000B2 RID: 178
		internal abstract SqliteStatement Prepare(SqliteConnection cnn, string strSql, SqliteStatement previous, uint timeoutMS, out string strRemain);

		// Token: 0x060000B3 RID: 179
		internal abstract bool Step(SqliteStatement stmt);

		// Token: 0x060000B4 RID: 180
		internal abstract int Reset(SqliteStatement stmt);

		// Token: 0x060000B5 RID: 181
		internal abstract void Cancel();

		// Token: 0x060000B6 RID: 182
		internal abstract void Bind_Double(SqliteStatement stmt, int index, double value);

		// Token: 0x060000B7 RID: 183
		internal abstract void Bind_Int32(SqliteStatement stmt, int index, int value);

		// Token: 0x060000B8 RID: 184
		internal abstract void Bind_Int64(SqliteStatement stmt, int index, long value);

		// Token: 0x060000B9 RID: 185
		internal abstract void Bind_Text(SqliteStatement stmt, int index, string value);

		// Token: 0x060000BA RID: 186
		internal abstract void Bind_Blob(SqliteStatement stmt, int index, byte[] blobData);

		// Token: 0x060000BB RID: 187
		internal abstract void Bind_DateTime(SqliteStatement stmt, int index, DateTime dt);

		// Token: 0x060000BC RID: 188
		internal abstract void Bind_Null(SqliteStatement stmt, int index);

		// Token: 0x060000BD RID: 189
		internal abstract int Bind_ParamCount(SqliteStatement stmt);

		// Token: 0x060000BE RID: 190
		internal abstract string Bind_ParamName(SqliteStatement stmt, int index);

		// Token: 0x060000BF RID: 191
		internal abstract int Bind_ParamIndex(SqliteStatement stmt, string paramName);

		// Token: 0x060000C0 RID: 192
		internal abstract int ColumnCount(SqliteStatement stmt);

		// Token: 0x060000C1 RID: 193
		internal abstract string ColumnName(SqliteStatement stmt, int index);

		// Token: 0x060000C2 RID: 194
		internal abstract TypeAffinity ColumnAffinity(SqliteStatement stmt, int index);

		// Token: 0x060000C3 RID: 195
		internal abstract string ColumnType(SqliteStatement stmt, int index, out TypeAffinity nAffinity);

		// Token: 0x060000C4 RID: 196
		internal abstract int ColumnIndex(SqliteStatement stmt, string columnName);

		// Token: 0x060000C5 RID: 197
		internal abstract string ColumnOriginalName(SqliteStatement stmt, int index);

		// Token: 0x060000C6 RID: 198
		internal abstract string ColumnDatabaseName(SqliteStatement stmt, int index);

		// Token: 0x060000C7 RID: 199
		internal abstract string ColumnTableName(SqliteStatement stmt, int index);

		// Token: 0x060000C8 RID: 200
		internal abstract void ColumnMetaData(string dataBase, string table, string column, out string dataType, out string collateSequence, out bool notNull, out bool primaryKey, out bool autoIncrement);

		// Token: 0x060000C9 RID: 201
		internal abstract void GetIndexColumnExtendedInfo(string database, string index, string column, out int sortMode, out int onError, out string collationSequence);

		// Token: 0x060000CA RID: 202
		internal abstract double GetDouble(SqliteStatement stmt, int index);

		// Token: 0x060000CB RID: 203
		internal abstract int GetInt32(SqliteStatement stmt, int index);

		// Token: 0x060000CC RID: 204
		internal abstract long GetInt64(SqliteStatement stmt, int index);

		// Token: 0x060000CD RID: 205
		internal abstract string GetText(SqliteStatement stmt, int index);

		// Token: 0x060000CE RID: 206
		internal abstract long GetBytes(SqliteStatement stmt, int index, int nDataoffset, byte[] bDest, int nStart, int nLength);

		// Token: 0x060000CF RID: 207
		internal abstract long GetChars(SqliteStatement stmt, int index, int nDataoffset, char[] bDest, int nStart, int nLength);

		// Token: 0x060000D0 RID: 208
		internal abstract DateTime GetDateTime(SqliteStatement stmt, int index);

		// Token: 0x060000D1 RID: 209
		internal abstract bool IsNull(SqliteStatement stmt, int index);

		// Token: 0x060000D2 RID: 210
		internal abstract void CreateCollation(string strCollation, SQLiteCollation func, SQLiteCollation func16, IntPtr user_data);

		// Token: 0x060000D3 RID: 211
		internal abstract void CreateFunction(string strFunction, int nArgs, bool needCollSeq, SQLiteCallback func, SQLiteCallback funcstep, SQLiteFinalCallback funcfinal);

		// Token: 0x060000D4 RID: 212
		internal abstract CollationSequence GetCollationSequence(SqliteFunction func, IntPtr context);

		// Token: 0x060000D5 RID: 213
		internal abstract int ContextCollateCompare(CollationEncodingEnum enc, IntPtr context, string s1, string s2);

		// Token: 0x060000D6 RID: 214
		internal abstract int ContextCollateCompare(CollationEncodingEnum enc, IntPtr context, char[] c1, char[] c2);

		// Token: 0x060000D7 RID: 215
		internal abstract int AggregateCount(IntPtr context);

		// Token: 0x060000D8 RID: 216
		internal abstract IntPtr AggregateContext(IntPtr context);

		// Token: 0x060000D9 RID: 217
		internal abstract long GetParamValueBytes(IntPtr ptr, int nDataOffset, byte[] bDest, int nStart, int nLength);

		// Token: 0x060000DA RID: 218
		internal abstract double GetParamValueDouble(IntPtr ptr);

		// Token: 0x060000DB RID: 219
		internal abstract int GetParamValueInt32(IntPtr ptr);

		// Token: 0x060000DC RID: 220
		internal abstract long GetParamValueInt64(IntPtr ptr);

		// Token: 0x060000DD RID: 221
		internal abstract string GetParamValueText(IntPtr ptr);

		// Token: 0x060000DE RID: 222
		internal abstract TypeAffinity GetParamValueType(IntPtr ptr);

		// Token: 0x060000DF RID: 223
		internal abstract void ReturnBlob(IntPtr context, byte[] value);

		// Token: 0x060000E0 RID: 224
		internal abstract void ReturnDouble(IntPtr context, double value);

		// Token: 0x060000E1 RID: 225
		internal abstract void ReturnError(IntPtr context, string value);

		// Token: 0x060000E2 RID: 226
		internal abstract void ReturnInt32(IntPtr context, int value);

		// Token: 0x060000E3 RID: 227
		internal abstract void ReturnInt64(IntPtr context, long value);

		// Token: 0x060000E4 RID: 228
		internal abstract void ReturnNull(IntPtr context);

		// Token: 0x060000E5 RID: 229
		internal abstract void ReturnText(IntPtr context, string value);

		// Token: 0x060000E6 RID: 230
		internal abstract void SetPassword(byte[] passwordBytes);

		// Token: 0x060000E7 RID: 231
		internal abstract void ChangePassword(byte[] newPasswordBytes);

		// Token: 0x060000E8 RID: 232
		internal abstract void SetUpdateHook(SQLiteUpdateCallback func);

		// Token: 0x060000E9 RID: 233
		internal abstract void SetCommitHook(SQLiteCommitCallback func);

		// Token: 0x060000EA RID: 234
		internal abstract void SetRollbackHook(SQLiteRollbackCallback func);

		// Token: 0x060000EB RID: 235
		internal abstract int GetCursorForTable(SqliteStatement stmt, int database, int rootPage);

		// Token: 0x060000EC RID: 236
		internal abstract long GetRowIdForCursor(SqliteStatement stmt, int cursor);

		// Token: 0x060000ED RID: 237
		internal abstract object GetValue(SqliteStatement stmt, int index, SQLiteType typ);

		// Token: 0x060000EE RID: 238 RVA: 0x00007623 File Offset: 0x00005823
		protected virtual void Dispose(bool bDisposing)
		{
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00007625 File Offset: 0x00005825
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000762E File Offset: 0x0000582E
		internal static string SQLiteLastError(SqliteConnectionHandle db)
		{
			return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_errmsg(db), -1);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00007644 File Offset: 0x00005844
		internal static void FinalizeStatement(SqliteStatementHandle stmt)
		{
			object @lock = SQLiteBase._lock;
			lock (@lock)
			{
				int num = UnsafeNativeMethods.sqlite3_finalize(stmt);
				if (num > 0)
				{
					throw new SqliteException(num, null);
				}
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00007698 File Offset: 0x00005898
		internal static void CloseConnection(SqliteConnectionHandle db)
		{
			object @lock = SQLiteBase._lock;
			lock (@lock)
			{
				SQLiteBase.ResetConnection(db);
				int num;
				if (UnsafeNativeMethods.use_sqlite3_close_v2)
				{
					num = UnsafeNativeMethods.sqlite3_close_v2(db);
				}
				else
				{
					num = UnsafeNativeMethods.sqlite3_close(db);
				}
				if (num > 0)
				{
					throw new SqliteException(num, SQLiteBase.SQLiteLastError(db));
				}
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000770C File Offset: 0x0000590C
		internal static void ResetConnection(SqliteConnectionHandle db)
		{
			object @lock = SQLiteBase._lock;
			lock (@lock)
			{
				IntPtr intPtr = IntPtr.Zero;
				do
				{
					intPtr = UnsafeNativeMethods.sqlite3_next_stmt(db, intPtr);
					if (intPtr != IntPtr.Zero)
					{
						UnsafeNativeMethods.sqlite3_reset(intPtr);
					}
				}
				while (intPtr != IntPtr.Zero);
				UnsafeNativeMethods.sqlite3_exec(db, SqliteConvert.ToUTF8("ROLLBACK"), IntPtr.Zero, IntPtr.Zero, out intPtr);
				if (intPtr != IntPtr.Zero)
				{
					UnsafeNativeMethods.sqlite3_free(intPtr);
				}
			}
		}

		// Token: 0x04000048 RID: 72
		internal static object _lock = new object();
	}
}
