using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000006 RID: 6
	internal class SQLite3_UTF16 : SQLite3
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00007440 File Offset: 0x00005640
		internal SQLite3_UTF16(SQLiteDateFormats fmt)
			: base(fmt)
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00007449 File Offset: 0x00005649
		public override string ToString(IntPtr b, int nbytelen)
		{
			return SQLite3_UTF16.UTF16ToString(b, nbytelen);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00007452 File Offset: 0x00005652
		public static string UTF16ToString(IntPtr b, int nbytelen)
		{
			if (nbytelen == 0 || b == IntPtr.Zero)
			{
				return "";
			}
			if (nbytelen == -1)
			{
				return Marshal.PtrToStringUni(b);
			}
			return Marshal.PtrToStringUni(b, nbytelen / 2);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00007480 File Offset: 0x00005680
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
				if ((flags & SQLiteOpenFlagsEnum.Create) == SQLiteOpenFlagsEnum.None && !File.Exists(strFilename))
				{
					throw new SqliteException(14, strFilename);
				}
				IntPtr intPtr;
				int num = UnsafeNativeMethods.sqlite3_open16(strFilename, out intPtr);
				if (num > 0)
				{
					throw new SqliteException(num, null);
				}
				this._sql = intPtr;
			}
			this._functionsArray = SqliteFunction.BindFunctions(this);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00007507 File Offset: 0x00005707
		internal override void Bind_DateTime(SqliteStatement stmt, int index, DateTime dt)
		{
			this.Bind_Text(stmt, index, base.ToString(dt));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00007518 File Offset: 0x00005718
		internal override void Bind_Text(SqliteStatement stmt, int index, string value)
		{
			int num = UnsafeNativeMethods.sqlite3_bind_text16(stmt._sqlite_stmt, index, value, value.Length * 2, (IntPtr)(-1));
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00007557 File Offset: 0x00005757
		internal override DateTime GetDateTime(SqliteStatement stmt, int index)
		{
			return base.ToDateTime(this.GetText(stmt, index));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00007567 File Offset: 0x00005767
		internal override string ColumnName(SqliteStatement stmt, int index)
		{
			return SQLite3_UTF16.UTF16ToString(UnsafeNativeMethods.sqlite3_column_name16(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00007580 File Offset: 0x00005780
		internal override string GetText(SqliteStatement stmt, int index)
		{
			return SQLite3_UTF16.UTF16ToString(UnsafeNativeMethods.sqlite3_column_text16(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00007599 File Offset: 0x00005799
		internal override string ColumnOriginalName(SqliteStatement stmt, int index)
		{
			return SQLite3_UTF16.UTF16ToString(UnsafeNativeMethods.sqlite3_column_origin_name16(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000075B2 File Offset: 0x000057B2
		internal override string ColumnDatabaseName(SqliteStatement stmt, int index)
		{
			return SQLite3_UTF16.UTF16ToString(UnsafeNativeMethods.sqlite3_column_database_name16(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000075CB File Offset: 0x000057CB
		internal override string ColumnTableName(SqliteStatement stmt, int index)
		{
			return SQLite3_UTF16.UTF16ToString(UnsafeNativeMethods.sqlite3_column_table_name16(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000075E4 File Offset: 0x000057E4
		internal override string GetParamValueText(IntPtr ptr)
		{
			return SQLite3_UTF16.UTF16ToString(UnsafeNativeMethods.sqlite3_value_text16(ptr), -1);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000075F2 File Offset: 0x000057F2
		internal override void ReturnError(IntPtr context, string value)
		{
			UnsafeNativeMethods.sqlite3_result_error16(context, value, value.Length * 2);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00007603 File Offset: 0x00005803
		internal override void ReturnText(IntPtr context, string value)
		{
			UnsafeNativeMethods.sqlite3_result_text16(context, value, value.Length * 2, (IntPtr)(-1));
		}
	}
}
