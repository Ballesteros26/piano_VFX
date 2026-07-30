using System;
using System.Data.Common;
using System.Runtime.Serialization;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public sealed class SqliteException : DbException
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x0000B788 File Offset: 0x00009988
		private SqliteException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000B792 File Offset: 0x00009992
		public SqliteException(int errorCode, string extendedInformation)
			: base(SqliteException.GetStockErrorMessage(errorCode, extendedInformation))
		{
			this._errorCode = (SQLiteErrorCode)errorCode;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000B7A8 File Offset: 0x000099A8
		public SqliteException(string message)
			: base(message)
		{
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000B7B1 File Offset: 0x000099B1
		public SqliteException()
		{
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000B7B9 File Offset: 0x000099B9
		public SqliteException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000B7C3 File Offset: 0x000099C3
		public new SQLiteErrorCode ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000B7CB File Offset: 0x000099CB
		private static string GetStockErrorMessage(int errorCode, string errorMessage)
		{
			if (errorMessage == null)
			{
				errorMessage = "";
			}
			if (errorMessage.Length > 0)
			{
				errorMessage = "\r\n" + errorMessage;
			}
			if (errorCode < 0 || errorCode >= SqliteException._errorMessages.Length)
			{
				errorCode = 1;
			}
			return SqliteException._errorMessages[errorCode] + errorMessage;
		}

		// Token: 0x040000A6 RID: 166
		private SQLiteErrorCode _errorCode;

		// Token: 0x040000A7 RID: 167
		private static string[] _errorMessages = new string[]
		{
			"SQLite OK", "SQLite error", "An internal logic error in SQLite", "Access permission denied", "Callback routine requested an abort", "The database file is locked", "A table in the database is locked", "malloc() failed", "Attempt to write a read-only database", "Operation terminated by sqlite3_interrupt()",
			"Some kind of disk I/O error occurred", "The database disk image is malformed", "Table or record not found", "Insertion failed because the database is full", "Unable to open the database file", "Database lock protocol error", "Database is empty", "The database schema changed", "Too much data for one row of a table", "Abort due to constraint violation",
			"Data type mismatch", "Library used incorrectly", "Uses OS features not supported on host", "Authorization denied", "Auxiliary database format error", "2nd parameter to sqlite3_bind() out of range", "File opened that is not a database file"
		};
	}
}
