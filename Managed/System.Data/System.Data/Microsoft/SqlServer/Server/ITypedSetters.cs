using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000395 RID: 917
	internal interface ITypedSetters
	{
		// Token: 0x06002B29 RID: 11049
		void SetDBNull(int ordinal);

		// Token: 0x06002B2A RID: 11050
		void SetBoolean(int ordinal, bool value);

		// Token: 0x06002B2B RID: 11051
		void SetByte(int ordinal, byte value);

		// Token: 0x06002B2C RID: 11052
		void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06002B2D RID: 11053
		void SetChar(int ordinal, char value);

		// Token: 0x06002B2E RID: 11054
		void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06002B2F RID: 11055
		void SetInt16(int ordinal, short value);

		// Token: 0x06002B30 RID: 11056
		void SetInt32(int ordinal, int value);

		// Token: 0x06002B31 RID: 11057
		void SetInt64(int ordinal, long value);

		// Token: 0x06002B32 RID: 11058
		void SetFloat(int ordinal, float value);

		// Token: 0x06002B33 RID: 11059
		void SetDouble(int ordinal, double value);

		// Token: 0x06002B34 RID: 11060
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetString(int ordinal, string value);

		// Token: 0x06002B35 RID: 11061
		void SetString(int ordinal, string value, int offset);

		// Token: 0x06002B36 RID: 11062
		void SetDecimal(int ordinal, decimal value);

		// Token: 0x06002B37 RID: 11063
		void SetDateTime(int ordinal, DateTime value);

		// Token: 0x06002B38 RID: 11064
		void SetGuid(int ordinal, Guid value);

		// Token: 0x06002B39 RID: 11065
		void SetSqlBoolean(int ordinal, SqlBoolean value);

		// Token: 0x06002B3A RID: 11066
		void SetSqlByte(int ordinal, SqlByte value);

		// Token: 0x06002B3B RID: 11067
		void SetSqlInt16(int ordinal, SqlInt16 value);

		// Token: 0x06002B3C RID: 11068
		void SetSqlInt32(int ordinal, SqlInt32 value);

		// Token: 0x06002B3D RID: 11069
		void SetSqlInt64(int ordinal, SqlInt64 value);

		// Token: 0x06002B3E RID: 11070
		void SetSqlSingle(int ordinal, SqlSingle value);

		// Token: 0x06002B3F RID: 11071
		void SetSqlDouble(int ordinal, SqlDouble value);

		// Token: 0x06002B40 RID: 11072
		void SetSqlMoney(int ordinal, SqlMoney value);

		// Token: 0x06002B41 RID: 11073
		void SetSqlDateTime(int ordinal, SqlDateTime value);

		// Token: 0x06002B42 RID: 11074
		void SetSqlDecimal(int ordinal, SqlDecimal value);

		// Token: 0x06002B43 RID: 11075
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlString(int ordinal, SqlString value);

		// Token: 0x06002B44 RID: 11076
		void SetSqlString(int ordinal, SqlString value, int offset);

		// Token: 0x06002B45 RID: 11077
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlBinary(int ordinal, SqlBinary value);

		// Token: 0x06002B46 RID: 11078
		void SetSqlBinary(int ordinal, SqlBinary value, int offset);

		// Token: 0x06002B47 RID: 11079
		void SetSqlGuid(int ordinal, SqlGuid value);

		// Token: 0x06002B48 RID: 11080
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlChars(int ordinal, SqlChars value);

		// Token: 0x06002B49 RID: 11081
		void SetSqlChars(int ordinal, SqlChars value, int offset);

		// Token: 0x06002B4A RID: 11082
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlBytes(int ordinal, SqlBytes value);

		// Token: 0x06002B4B RID: 11083
		void SetSqlBytes(int ordinal, SqlBytes value, int offset);

		// Token: 0x06002B4C RID: 11084
		void SetSqlXml(int ordinal, SqlXml value);
	}
}
