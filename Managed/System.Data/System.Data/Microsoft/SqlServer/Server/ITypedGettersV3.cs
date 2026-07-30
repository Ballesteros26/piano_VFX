using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000394 RID: 916
	internal interface ITypedGettersV3
	{
		// Token: 0x06002B18 RID: 11032
		bool IsDBNull(SmiEventSink sink, int ordinal);

		// Token: 0x06002B19 RID: 11033
		SmiMetaData GetVariantType(SmiEventSink sink, int ordinal);

		// Token: 0x06002B1A RID: 11034
		bool GetBoolean(SmiEventSink sink, int ordinal);

		// Token: 0x06002B1B RID: 11035
		byte GetByte(SmiEventSink sink, int ordinal);

		// Token: 0x06002B1C RID: 11036
		long GetBytesLength(SmiEventSink sink, int ordinal);

		// Token: 0x06002B1D RID: 11037
		int GetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06002B1E RID: 11038
		long GetCharsLength(SmiEventSink sink, int ordinal);

		// Token: 0x06002B1F RID: 11039
		int GetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06002B20 RID: 11040
		string GetString(SmiEventSink sink, int ordinal);

		// Token: 0x06002B21 RID: 11041
		short GetInt16(SmiEventSink sink, int ordinal);

		// Token: 0x06002B22 RID: 11042
		int GetInt32(SmiEventSink sink, int ordinal);

		// Token: 0x06002B23 RID: 11043
		long GetInt64(SmiEventSink sink, int ordinal);

		// Token: 0x06002B24 RID: 11044
		float GetSingle(SmiEventSink sink, int ordinal);

		// Token: 0x06002B25 RID: 11045
		double GetDouble(SmiEventSink sink, int ordinal);

		// Token: 0x06002B26 RID: 11046
		SqlDecimal GetSqlDecimal(SmiEventSink sink, int ordinal);

		// Token: 0x06002B27 RID: 11047
		DateTime GetDateTime(SmiEventSink sink, int ordinal);

		// Token: 0x06002B28 RID: 11048
		Guid GetGuid(SmiEventSink sink, int ordinal);
	}
}
