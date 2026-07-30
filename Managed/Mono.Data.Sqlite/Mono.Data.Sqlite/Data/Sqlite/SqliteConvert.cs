using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000018 RID: 24
	public abstract class SqliteConvert
	{
		// Token: 0x06000186 RID: 390 RVA: 0x00008BE0 File Offset: 0x00006DE0
		internal SqliteConvert(SQLiteDateFormats fmt)
		{
			this._datetimeFormat = fmt;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00008BF0 File Offset: 0x00006DF0
		public static byte[] ToUTF8(string sourceText)
		{
			int num = SqliteConvert._utf8.GetByteCount(sourceText) + 1;
			byte[] array = new byte[num];
			num = SqliteConvert._utf8.GetBytes(sourceText, 0, sourceText.Length, array, 0);
			array[num] = 0;
			return array;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00008C2C File Offset: 0x00006E2C
		public byte[] ToUTF8(DateTime dateTimeValue)
		{
			return SqliteConvert.ToUTF8(this.ToString(dateTimeValue));
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00008C3A File Offset: 0x00006E3A
		public virtual string ToString(IntPtr nativestring, int nativestringlen)
		{
			return SqliteConvert.UTF8ToString(nativestring, nativestringlen);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00008C44 File Offset: 0x00006E44
		public static string UTF8ToString(IntPtr nativestring, int nativestringlen)
		{
			if (nativestringlen == 0 || nativestring == IntPtr.Zero)
			{
				return "";
			}
			if (nativestringlen == -1)
			{
				do
				{
					nativestringlen++;
				}
				while (Marshal.ReadByte(nativestring, nativestringlen) != 0);
			}
			byte[] array = new byte[nativestringlen];
			Marshal.Copy(nativestring, array, 0, nativestringlen);
			return SqliteConvert._utf8.GetString(array, 0, nativestringlen);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008C98 File Offset: 0x00006E98
		public DateTime ToDateTime(string dateText)
		{
			switch (this._datetimeFormat)
			{
			case SQLiteDateFormats.Ticks:
				return new DateTime(Convert.ToInt64(dateText, CultureInfo.InvariantCulture));
			case SQLiteDateFormats.JulianDay:
				return this.ToDateTime(Convert.ToDouble(dateText, CultureInfo.InvariantCulture));
			case SQLiteDateFormats.UnixEpoch:
				return SqliteConvert.UnixEpoch.AddSeconds((double)Convert.ToInt32(dateText, CultureInfo.InvariantCulture));
			}
			return DateTime.ParseExact(dateText, SqliteConvert._datetimeFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008D12 File Offset: 0x00006F12
		public DateTime ToDateTime(double julianDay)
		{
			return DateTime.FromOADate(julianDay - 2415018.5);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008D24 File Offset: 0x00006F24
		public double ToJulianDay(DateTime value)
		{
			return value.ToOADate() + 2415018.5;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008D38 File Offset: 0x00006F38
		public string ToString(DateTime dateValue)
		{
			switch (this._datetimeFormat)
			{
			case SQLiteDateFormats.Ticks:
				return dateValue.Ticks.ToString(CultureInfo.InvariantCulture);
			case SQLiteDateFormats.JulianDay:
				return this.ToJulianDay(dateValue).ToString(CultureInfo.InvariantCulture);
			case SQLiteDateFormats.UnixEpoch:
				return (dateValue.Subtract(SqliteConvert.UnixEpoch).Ticks / 10000000L).ToString();
			}
			return dateValue.ToString(SqliteConvert._datetimeFormats[19], CultureInfo.InvariantCulture);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00008DC6 File Offset: 0x00006FC6
		internal DateTime ToDateTime(IntPtr ptr, int len)
		{
			return this.ToDateTime(this.ToString(ptr, len));
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public static string[] Split(string source, char separator)
		{
			char[] array = new char[] { '"', separator };
			char[] array2 = new char[] { '"' };
			int num = 0;
			List<string> list = new List<string>();
			while (source.Length > 0)
			{
				num = source.IndexOfAny(array, num);
				if (num == -1)
				{
					break;
				}
				if (source[num] == array[0])
				{
					num = source.IndexOfAny(array2, num + 1);
					if (num == -1)
					{
						break;
					}
					num++;
				}
				else
				{
					string text = source.Substring(0, num).Trim();
					if (text.Length > 1 && text[0] == array2[0] && text[text.Length - 1] == text[0])
					{
						text = text.Substring(1, text.Length - 2);
					}
					source = source.Substring(num + 1).Trim();
					if (text.Length > 0)
					{
						list.Add(text);
					}
					num = 0;
				}
			}
			if (source.Length > 0)
			{
				string text = source.Trim();
				if (text.Length > 1 && text[0] == array2[0] && text[text.Length - 1] == text[0])
				{
					text = text.Substring(1, text.Length - 2);
				}
				list.Add(text);
			}
			string[] array3 = new string[list.Count];
			list.CopyTo(array3, 0);
			return array3;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008F3A File Offset: 0x0000713A
		public static bool ToBoolean(object source)
		{
			if (source is bool)
			{
				return (bool)source;
			}
			return SqliteConvert.ToBoolean(source.ToString());
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008F58 File Offset: 0x00007158
		public static bool ToBoolean(string source)
		{
			if (string.Compare(source, bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			if (string.Compare(source, bool.FalseString, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return false;
			}
			string text = source.ToLower();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1630810064U)
			{
				if (num <= 890022063U)
				{
					if (num != 873244444U)
					{
						if (num != 890022063U)
						{
							goto IL_0119;
						}
						if (!(text == "0"))
						{
							goto IL_0119;
						}
						return false;
					}
					else if (!(text == "1"))
					{
						goto IL_0119;
					}
				}
				else if (num != 1319056784U)
				{
					if (num != 1630810064U)
					{
						goto IL_0119;
					}
					if (!(text == "on"))
					{
						goto IL_0119;
					}
				}
				else if (!(text == "yes"))
				{
					goto IL_0119;
				}
			}
			else if (num <= 2872740362U)
			{
				if (num != 1647734778U)
				{
					if (num != 2872740362U)
					{
						goto IL_0119;
					}
					if (!(text == "off"))
					{
						goto IL_0119;
					}
					return false;
				}
				else
				{
					if (!(text == "no"))
					{
						goto IL_0119;
					}
					return false;
				}
			}
			else if (num != 3943445553U)
			{
				if (num != 4228665076U)
				{
					goto IL_0119;
				}
				if (!(text == "y"))
				{
					goto IL_0119;
				}
			}
			else
			{
				if (!(text == "n"))
				{
					goto IL_0119;
				}
				return false;
			}
			return true;
			IL_0119:
			throw new ArgumentException("source");
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009088 File Offset: 0x00007288
		internal static void ColumnToType(SqliteStatement stmt, int i, SQLiteType typ)
		{
			typ.Type = SqliteConvert.TypeNameToDbType(stmt._sql.ColumnType(stmt, i, out typ.Affinity));
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000090A8 File Offset: 0x000072A8
		internal static Type SQLiteTypeToType(SQLiteType t)
		{
			if (t.Type == DbType.Object)
			{
				return SqliteConvert._affinitytotype[(int)t.Affinity];
			}
			return SqliteConvert.DbTypeToType(t.Type);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000090CC File Offset: 0x000072CC
		internal static DbType TypeToDbType(Type typ)
		{
			TypeCode typeCode = Type.GetTypeCode(typ);
			if (typeCode != TypeCode.Object)
			{
				return SqliteConvert._typetodbtype[(int)typeCode];
			}
			if (typ == typeof(byte[]))
			{
				return DbType.Binary;
			}
			if (typ == typeof(Guid))
			{
				return DbType.Guid;
			}
			return DbType.String;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009117 File Offset: 0x00007317
		internal static int DbTypeToColumnSize(DbType typ)
		{
			return SqliteConvert._dbtypetocolumnsize[(int)typ];
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00009120 File Offset: 0x00007320
		internal static object DbTypeToNumericPrecision(DbType typ)
		{
			return SqliteConvert._dbtypetonumericprecision[(int)typ];
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00009129 File Offset: 0x00007329
		internal static object DbTypeToNumericScale(DbType typ)
		{
			return SqliteConvert._dbtypetonumericscale[(int)typ];
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00009134 File Offset: 0x00007334
		internal static string DbTypeToTypeName(DbType typ)
		{
			for (int i = 0; i < SqliteConvert._dbtypeNames.Length; i++)
			{
				if (SqliteConvert._dbtypeNames[i].dataType == typ)
				{
					return SqliteConvert._dbtypeNames[i].typeName;
				}
			}
			return string.Empty;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000917C File Offset: 0x0000737C
		internal static Type DbTypeToType(DbType typ)
		{
			return SqliteConvert._dbtypeToType[(int)typ];
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00009188 File Offset: 0x00007388
		internal static TypeAffinity TypeToAffinity(Type typ)
		{
			TypeCode typeCode = Type.GetTypeCode(typ);
			if (typeCode != TypeCode.Object)
			{
				return SqliteConvert._typecodeAffinities[(int)typeCode];
			}
			if (typ == typeof(byte[]) || typ == typeof(Guid))
			{
				return TypeAffinity.Blob;
			}
			return TypeAffinity.Text;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000091D0 File Offset: 0x000073D0
		internal static DbType TypeNameToDbType(string Name)
		{
			if (string.IsNullOrEmpty(Name))
			{
				return DbType.Object;
			}
			string text = Name;
			int num = text.IndexOf('(');
			if (num > 0)
			{
				text = text.Substring(0, num);
			}
			for (int i = 0; i < SqliteConvert._typeNames.Length; i++)
			{
				if (string.Compare(text, SqliteConvert._typeNames[i].typeName, true, CultureInfo.InvariantCulture) == 0)
				{
					return SqliteConvert._typeNames[i].dataType;
				}
			}
			if (Name.IndexOf("INT", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				return DbType.Int64;
			}
			if (Name.IndexOf("CHAR", StringComparison.OrdinalIgnoreCase) >= 0 || Name.IndexOf("CLOB", StringComparison.OrdinalIgnoreCase) >= 0 || Name.IndexOf("TEXT", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				return DbType.String;
			}
			if (Name.IndexOf("BLOB", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				return DbType.Object;
			}
			if (Name.IndexOf("REAL", StringComparison.OrdinalIgnoreCase) >= 0 || Name.IndexOf("FLOA", StringComparison.OrdinalIgnoreCase) >= 0 || Name.IndexOf("DOUB", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				return DbType.Double;
			}
			return DbType.Object;
		}

		// Token: 0x04000072 RID: 114
		protected static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04000073 RID: 115
		private static string[] _datetimeFormats = new string[]
		{
			"THHmmssK", "THHmmK", "HH:mm:ss.FFFFFFFK", "HH:mm:ssK", "HH:mmK", "yyyy-MM-dd HH:mm:ss.FFFFFFFK", "yyyy-MM-dd HH:mm:ssK", "yyyy-MM-dd HH:mmK", "yyyy-MM-ddTHH:mm:ss.FFFFFFFK", "yyyy-MM-ddTHH:mmK",
			"yyyy-MM-ddTHH:mm:ssK", "yyyyMMddHHmmssK", "yyyyMMddHHmmK", "yyyyMMddTHHmmssFFFFFFFK", "THHmmss", "THHmm", "HH:mm:ss.FFFFFFF", "HH:mm:ss", "HH:mm", "yyyy-MM-dd HH:mm:ss.FFFFFFF",
			"yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mm", "yyyy-MM-ddTHH:mm:ss.FFFFFFF", "yyyy-MM-ddTHH:mm", "yyyy-MM-ddTHH:mm:ss", "yyyyMMddHHmmss", "yyyyMMddHHmm", "yyyyMMddTHHmmssFFFFFFF", "yyyy-MM-dd", "yyyyMMdd",
			"yy-MM-dd"
		};

		// Token: 0x04000074 RID: 116
		private static Encoding _utf8 = new UTF8Encoding();

		// Token: 0x04000075 RID: 117
		internal SQLiteDateFormats _datetimeFormat;

		// Token: 0x04000076 RID: 118
		private static Type[] _affinitytotype = new Type[]
		{
			typeof(object),
			typeof(long),
			typeof(double),
			typeof(string),
			typeof(byte[]),
			typeof(object),
			typeof(DateTime),
			typeof(object)
		};

		// Token: 0x04000077 RID: 119
		private static DbType[] _typetodbtype = new DbType[]
		{
			DbType.Object,
			DbType.Binary,
			DbType.Object,
			DbType.Boolean,
			DbType.SByte,
			DbType.SByte,
			DbType.Byte,
			DbType.Int16,
			DbType.UInt16,
			DbType.Int32,
			DbType.UInt32,
			DbType.Int64,
			DbType.UInt64,
			DbType.Single,
			DbType.Double,
			DbType.Decimal,
			DbType.DateTime,
			DbType.Object,
			DbType.String
		};

		// Token: 0x04000078 RID: 120
		private static int[] _dbtypetocolumnsize = new int[]
		{
			int.MaxValue, int.MaxValue, 1, 1, 8, 8, 8, 8, 8, 16,
			2, 4, 8, int.MaxValue, 1, 4, int.MaxValue, 8, 2, 4,
			8, 8, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue
		};

		// Token: 0x04000079 RID: 121
		private static object[] _dbtypetonumericprecision = new object[]
		{
			DBNull.Value,
			DBNull.Value,
			3,
			DBNull.Value,
			19,
			DBNull.Value,
			DBNull.Value,
			53,
			53,
			DBNull.Value,
			5,
			10,
			19,
			DBNull.Value,
			3,
			24,
			DBNull.Value,
			DBNull.Value,
			5,
			10,
			19,
			53,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value
		};

		// Token: 0x0400007A RID: 122
		private static object[] _dbtypetonumericscale = new object[]
		{
			DBNull.Value,
			DBNull.Value,
			0,
			DBNull.Value,
			4,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			0,
			0,
			0,
			DBNull.Value,
			0,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			0,
			0,
			0,
			0,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value
		};

		// Token: 0x0400007B RID: 123
		private static SQLiteTypeNames[] _dbtypeNames = new SQLiteTypeNames[]
		{
			new SQLiteTypeNames("INTEGER", DbType.Int64),
			new SQLiteTypeNames("TINYINT", DbType.Byte),
			new SQLiteTypeNames("INT", DbType.Int32),
			new SQLiteTypeNames("VARCHAR", DbType.AnsiString),
			new SQLiteTypeNames("NVARCHAR", DbType.String),
			new SQLiteTypeNames("CHAR", DbType.AnsiStringFixedLength),
			new SQLiteTypeNames("NCHAR", DbType.StringFixedLength),
			new SQLiteTypeNames("FLOAT", DbType.Double),
			new SQLiteTypeNames("REAL", DbType.Single),
			new SQLiteTypeNames("BIT", DbType.Boolean),
			new SQLiteTypeNames("DECIMAL", DbType.Decimal),
			new SQLiteTypeNames("DATETIME", DbType.DateTime),
			new SQLiteTypeNames("BLOB", DbType.Binary),
			new SQLiteTypeNames("UNIQUEIDENTIFIER", DbType.Guid),
			new SQLiteTypeNames("SMALLINT", DbType.Int16)
		};

		// Token: 0x0400007C RID: 124
		private static Type[] _dbtypeToType = new Type[]
		{
			typeof(string),
			typeof(byte[]),
			typeof(byte),
			typeof(bool),
			typeof(decimal),
			typeof(DateTime),
			typeof(DateTime),
			typeof(decimal),
			typeof(double),
			typeof(Guid),
			typeof(short),
			typeof(int),
			typeof(long),
			typeof(object),
			typeof(sbyte),
			typeof(float),
			typeof(string),
			typeof(DateTime),
			typeof(ushort),
			typeof(uint),
			typeof(ulong),
			typeof(double),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string)
		};

		// Token: 0x0400007D RID: 125
		private static TypeAffinity[] _typecodeAffinities = new TypeAffinity[]
		{
			TypeAffinity.Null,
			TypeAffinity.Blob,
			TypeAffinity.Null,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Double,
			TypeAffinity.Double,
			TypeAffinity.Double,
			TypeAffinity.DateTime,
			TypeAffinity.Null,
			TypeAffinity.Text
		};

		// Token: 0x0400007E RID: 126
		private static SQLiteTypeNames[] _typeNames = new SQLiteTypeNames[]
		{
			new SQLiteTypeNames("COUNTER", DbType.Int64),
			new SQLiteTypeNames("AUTOINCREMENT", DbType.Int64),
			new SQLiteTypeNames("IDENTITY", DbType.Int64),
			new SQLiteTypeNames("LONGTEXT", DbType.String),
			new SQLiteTypeNames("LONGCHAR", DbType.String),
			new SQLiteTypeNames("LONGVARCHAR", DbType.String),
			new SQLiteTypeNames("LONG", DbType.Int64),
			new SQLiteTypeNames("TINYINT", DbType.Byte),
			new SQLiteTypeNames("INTEGER", DbType.Int64),
			new SQLiteTypeNames("INT", DbType.Int32),
			new SQLiteTypeNames("VARCHAR", DbType.String),
			new SQLiteTypeNames("NVARCHAR", DbType.String),
			new SQLiteTypeNames("CHAR", DbType.String),
			new SQLiteTypeNames("NCHAR", DbType.String),
			new SQLiteTypeNames("TEXT", DbType.String),
			new SQLiteTypeNames("NTEXT", DbType.String),
			new SQLiteTypeNames("STRING", DbType.String),
			new SQLiteTypeNames("DOUBLE", DbType.Double),
			new SQLiteTypeNames("FLOAT", DbType.Double),
			new SQLiteTypeNames("REAL", DbType.Single),
			new SQLiteTypeNames("BIT", DbType.Boolean),
			new SQLiteTypeNames("YESNO", DbType.Boolean),
			new SQLiteTypeNames("LOGICAL", DbType.Boolean),
			new SQLiteTypeNames("BOOL", DbType.Boolean),
			new SQLiteTypeNames("BOOLEAN", DbType.Boolean),
			new SQLiteTypeNames("NUMERIC", DbType.Decimal),
			new SQLiteTypeNames("DECIMAL", DbType.Decimal),
			new SQLiteTypeNames("MONEY", DbType.Decimal),
			new SQLiteTypeNames("CURRENCY", DbType.Decimal),
			new SQLiteTypeNames("TIME", DbType.DateTime),
			new SQLiteTypeNames("DATE", DbType.DateTime),
			new SQLiteTypeNames("SMALLDATE", DbType.DateTime),
			new SQLiteTypeNames("BLOB", DbType.Binary),
			new SQLiteTypeNames("BINARY", DbType.Binary),
			new SQLiteTypeNames("VARBINARY", DbType.Binary),
			new SQLiteTypeNames("IMAGE", DbType.Binary),
			new SQLiteTypeNames("GENERAL", DbType.Binary),
			new SQLiteTypeNames("OLEOBJECT", DbType.Binary),
			new SQLiteTypeNames("GUID", DbType.Guid),
			new SQLiteTypeNames("GUIDBLOB", DbType.Guid),
			new SQLiteTypeNames("UNIQUEIDENTIFIER", DbType.Guid),
			new SQLiteTypeNames("MEMO", DbType.String),
			new SQLiteTypeNames("NOTE", DbType.String),
			new SQLiteTypeNames("SMALLINT", DbType.Int16),
			new SQLiteTypeNames("BIGINT", DbType.Int64),
			new SQLiteTypeNames("TIMESTAMP", DbType.DateTime),
			new SQLiteTypeNames("DATETIME", DbType.DateTime)
		};
	}
}
