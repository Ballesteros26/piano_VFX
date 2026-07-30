using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000396 RID: 918
	internal interface ITypedSettersV3
	{
		// Token: 0x06002B4D RID: 11085
		void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData);

		// Token: 0x06002B4E RID: 11086
		void SetDBNull(SmiEventSink sink, int ordinal);

		// Token: 0x06002B4F RID: 11087
		void SetBoolean(SmiEventSink sink, int ordinal, bool value);

		// Token: 0x06002B50 RID: 11088
		void SetByte(SmiEventSink sink, int ordinal, byte value);

		// Token: 0x06002B51 RID: 11089
		int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06002B52 RID: 11090
		void SetBytesLength(SmiEventSink sink, int ordinal, long length);

		// Token: 0x06002B53 RID: 11091
		int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06002B54 RID: 11092
		void SetCharsLength(SmiEventSink sink, int ordinal, long length);

		// Token: 0x06002B55 RID: 11093
		void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length);

		// Token: 0x06002B56 RID: 11094
		void SetInt16(SmiEventSink sink, int ordinal, short value);

		// Token: 0x06002B57 RID: 11095
		void SetInt32(SmiEventSink sink, int ordinal, int value);

		// Token: 0x06002B58 RID: 11096
		void SetInt64(SmiEventSink sink, int ordinal, long value);

		// Token: 0x06002B59 RID: 11097
		void SetSingle(SmiEventSink sink, int ordinal, float value);

		// Token: 0x06002B5A RID: 11098
		void SetDouble(SmiEventSink sink, int ordinal, double value);

		// Token: 0x06002B5B RID: 11099
		void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value);

		// Token: 0x06002B5C RID: 11100
		void SetDateTime(SmiEventSink sink, int ordinal, DateTime value);

		// Token: 0x06002B5D RID: 11101
		void SetGuid(SmiEventSink sink, int ordinal, Guid value);
	}
}
