using System;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000393 RID: 915
	internal interface ITypedGetters
	{
		// Token: 0x06002AF5 RID: 10997
		bool IsDBNull(int ordinal);

		// Token: 0x06002AF6 RID: 10998
		SqlDbType GetVariantType(int ordinal);

		// Token: 0x06002AF7 RID: 10999
		bool GetBoolean(int ordinal);

		// Token: 0x06002AF8 RID: 11000
		byte GetByte(int ordinal);

		// Token: 0x06002AF9 RID: 11001
		long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06002AFA RID: 11002
		char GetChar(int ordinal);

		// Token: 0x06002AFB RID: 11003
		long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06002AFC RID: 11004
		short GetInt16(int ordinal);

		// Token: 0x06002AFD RID: 11005
		int GetInt32(int ordinal);

		// Token: 0x06002AFE RID: 11006
		long GetInt64(int ordinal);

		// Token: 0x06002AFF RID: 11007
		float GetFloat(int ordinal);

		// Token: 0x06002B00 RID: 11008
		double GetDouble(int ordinal);

		// Token: 0x06002B01 RID: 11009
		string GetString(int ordinal);

		// Token: 0x06002B02 RID: 11010
		decimal GetDecimal(int ordinal);

		// Token: 0x06002B03 RID: 11011
		DateTime GetDateTime(int ordinal);

		// Token: 0x06002B04 RID: 11012
		Guid GetGuid(int ordinal);

		// Token: 0x06002B05 RID: 11013
		SqlBoolean GetSqlBoolean(int ordinal);

		// Token: 0x06002B06 RID: 11014
		SqlByte GetSqlByte(int ordinal);

		// Token: 0x06002B07 RID: 11015
		SqlInt16 GetSqlInt16(int ordinal);

		// Token: 0x06002B08 RID: 11016
		SqlInt32 GetSqlInt32(int ordinal);

		// Token: 0x06002B09 RID: 11017
		SqlInt64 GetSqlInt64(int ordinal);

		// Token: 0x06002B0A RID: 11018
		SqlSingle GetSqlSingle(int ordinal);

		// Token: 0x06002B0B RID: 11019
		SqlDouble GetSqlDouble(int ordinal);

		// Token: 0x06002B0C RID: 11020
		SqlMoney GetSqlMoney(int ordinal);

		// Token: 0x06002B0D RID: 11021
		SqlDateTime GetSqlDateTime(int ordinal);

		// Token: 0x06002B0E RID: 11022
		SqlDecimal GetSqlDecimal(int ordinal);

		// Token: 0x06002B0F RID: 11023
		SqlString GetSqlString(int ordinal);

		// Token: 0x06002B10 RID: 11024
		SqlBinary GetSqlBinary(int ordinal);

		// Token: 0x06002B11 RID: 11025
		SqlGuid GetSqlGuid(int ordinal);

		// Token: 0x06002B12 RID: 11026
		SqlChars GetSqlChars(int ordinal);

		// Token: 0x06002B13 RID: 11027
		SqlBytes GetSqlBytes(int ordinal);

		// Token: 0x06002B14 RID: 11028
		SqlXml GetSqlXml(int ordinal);

		// Token: 0x06002B15 RID: 11029
		SqlBytes GetSqlBytesRef(int ordinal);

		// Token: 0x06002B16 RID: 11030
		SqlChars GetSqlCharsRef(int ordinal);

		// Token: 0x06002B17 RID: 11031
		SqlXml GetSqlXmlRef(int ordinal);
	}
}
