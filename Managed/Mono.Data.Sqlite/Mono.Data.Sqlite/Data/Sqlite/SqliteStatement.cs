using System;
using System.Data;
using System.Globalization;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000031 RID: 49
	internal sealed class SqliteStatement : IDisposable
	{
		// Token: 0x0600027C RID: 636 RVA: 0x0000DBFC File Offset: 0x0000BDFC
		internal SqliteStatement(SQLiteBase sqlbase, SqliteStatementHandle stmt, string strCommand, SqliteStatement previous)
		{
			this._sql = sqlbase;
			this._sqlite_stmt = stmt;
			this._sqlStatement = strCommand;
			int num = 0;
			int num2 = this._sql.Bind_ParamCount(this);
			if (num2 > 0)
			{
				if (previous != null)
				{
					num = previous._unnamedParameters;
				}
				this._paramNames = new string[num2];
				this._paramValues = new SqliteParameter[num2];
				for (int i = 0; i < num2; i++)
				{
					string text = this._sql.Bind_ParamName(this, i + 1);
					if (string.IsNullOrEmpty(text))
					{
						text = string.Format(CultureInfo.InvariantCulture, ";{0}", num);
						num++;
						this._unnamedParameters++;
					}
					this._paramNames[i] = text;
					this._paramValues[i] = null;
				}
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000DCBC File Offset: 0x0000BEBC
		internal bool MapParameter(string s, SqliteParameter p)
		{
			if (this._paramNames == null)
			{
				return false;
			}
			int num = 0;
			if (s.Length > 0 && ":$@;".IndexOf(s[0]) == -1)
			{
				num = 1;
			}
			int num2 = this._paramNames.Length;
			for (int i = 0; i < num2; i++)
			{
				if (string.Compare(this._paramNames[i], num, s, 0, Math.Max(this._paramNames[i].Length - num, s.Length), true, CultureInfo.InvariantCulture) == 0)
				{
					this._paramValues[i] = p;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000DD47 File Offset: 0x0000BF47
		public void Dispose()
		{
			if (this._sqlite_stmt != null)
			{
				this._sqlite_stmt.Dispose();
			}
			this._sqlite_stmt = null;
			this._paramNames = null;
			this._paramValues = null;
			this._sql = null;
			this._sqlStatement = null;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000DD80 File Offset: 0x0000BF80
		internal void BindParameters()
		{
			if (this._paramNames == null)
			{
				return;
			}
			int num = this._paramNames.Length;
			for (int i = 0; i < num; i++)
			{
				this.BindParameter(i + 1, this._paramValues[i]);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		private void BindParameter(int index, SqliteParameter param)
		{
			if (param == null)
			{
				throw new SqliteException(1, "Insufficient parameters supplied to the command");
			}
			object value = param.Value;
			DbType dbType = param.DbType;
			if (Convert.IsDBNull(value) || value == null)
			{
				this._sql.Bind_Null(this, index);
				return;
			}
			if (dbType == DbType.Object)
			{
				dbType = SqliteConvert.TypeToDbType(value.GetType());
			}
			switch (dbType)
			{
			case DbType.Binary:
				this._sql.Bind_Blob(this, index, (byte[])value);
				return;
			case DbType.Byte:
			case DbType.Boolean:
			case DbType.Int16:
			case DbType.Int32:
			case DbType.SByte:
			case DbType.UInt16:
				this._sql.Bind_Int32(this, index, Convert.ToInt32(value, CultureInfo.CurrentCulture));
				return;
			case DbType.Currency:
			case DbType.Double:
			case DbType.Single:
				this._sql.Bind_Double(this, index, Convert.ToDouble(value, CultureInfo.CurrentCulture));
				return;
			case DbType.Date:
			case DbType.DateTime:
			case DbType.Time:
				this._sql.Bind_DateTime(this, index, Convert.ToDateTime(value, CultureInfo.CurrentCulture));
				return;
			case DbType.Decimal:
				this._sql.Bind_Text(this, index, Convert.ToDecimal(value, CultureInfo.CurrentCulture).ToString(CultureInfo.InvariantCulture));
				return;
			case DbType.Guid:
				if (this._command.Connection._binaryGuid)
				{
					this._sql.Bind_Blob(this, index, ((Guid)value).ToByteArray());
					return;
				}
				this._sql.Bind_Text(this, index, value.ToString());
				return;
			case DbType.Int64:
			case DbType.UInt32:
			case DbType.UInt64:
				this._sql.Bind_Int64(this, index, Convert.ToInt64(value, CultureInfo.CurrentCulture));
				return;
			}
			this._sql.Bind_Text(this, index, value.ToString());
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000DF60 File Offset: 0x0000C160
		internal string[] TypeDefinitions
		{
			get
			{
				return this._types;
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000DF68 File Offset: 0x0000C168
		internal void SetTypes(string typedefs)
		{
			int num = typedefs.IndexOf("TYPES", 0, StringComparison.OrdinalIgnoreCase);
			if (num == -1)
			{
				throw new ArgumentOutOfRangeException();
			}
			string[] array = typedefs.Substring(num + 6).Replace(" ", "").Replace(";", "")
				.Replace("\"", "")
				.Replace("[", "")
				.Replace("]", "")
				.Replace("`", "")
				.Split(new char[] { ',', '\r', '\n', '\t' });
			for (int i = 0; i < array.Length; i++)
			{
				if (string.IsNullOrEmpty(array[i]))
				{
					array[i] = null;
				}
			}
			this._types = array;
		}

		// Token: 0x040000FB RID: 251
		internal SQLiteBase _sql;

		// Token: 0x040000FC RID: 252
		internal string _sqlStatement;

		// Token: 0x040000FD RID: 253
		internal SqliteStatementHandle _sqlite_stmt;

		// Token: 0x040000FE RID: 254
		internal int _unnamedParameters;

		// Token: 0x040000FF RID: 255
		internal string[] _paramNames;

		// Token: 0x04000100 RID: 256
		internal SqliteParameter[] _paramValues;

		// Token: 0x04000101 RID: 257
		internal SqliteCommand _command;

		// Token: 0x04000102 RID: 258
		private string[] _types;
	}
}
