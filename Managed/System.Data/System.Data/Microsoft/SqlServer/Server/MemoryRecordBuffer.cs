using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000397 RID: 919
	internal sealed class MemoryRecordBuffer : SmiRecordBuffer
	{
		// Token: 0x06002B5E RID: 11102 RVA: 0x000BE8F0 File Offset: 0x000BCAF0
		internal MemoryRecordBuffer(SmiMetaData[] metaData)
		{
			this._buffer = new SqlRecordBuffer[metaData.Length];
			for (int i = 0; i < this._buffer.Length; i++)
			{
				this._buffer[i] = new SqlRecordBuffer(metaData[i]);
			}
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x000BE934 File Offset: 0x000BCB34
		public override bool IsDBNull(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].IsNull;
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x000BE943 File Offset: 0x000BCB43
		public override SmiMetaData GetVariantType(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].VariantType;
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x000BE952 File Offset: 0x000BCB52
		public override bool GetBoolean(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Boolean;
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x000BE961 File Offset: 0x000BCB61
		public override byte GetByte(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Byte;
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x000BE970 File Offset: 0x000BCB70
		public override long GetBytesLength(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].BytesLength;
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x000BE97F File Offset: 0x000BCB7F
		public override int GetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].GetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x000BE995 File Offset: 0x000BCB95
		public override long GetCharsLength(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].CharsLength;
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x000BE9A4 File Offset: 0x000BCBA4
		public override int GetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].GetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x000BE9BA File Offset: 0x000BCBBA
		public override string GetString(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].String;
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x000BE9C9 File Offset: 0x000BCBC9
		public override short GetInt16(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int16;
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x000BE9D8 File Offset: 0x000BCBD8
		public override int GetInt32(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int32;
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x000BE9E7 File Offset: 0x000BCBE7
		public override long GetInt64(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int64;
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x000BE9F6 File Offset: 0x000BCBF6
		public override float GetSingle(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Single;
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x000BEA05 File Offset: 0x000BCC05
		public override double GetDouble(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Double;
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x000BEA14 File Offset: 0x000BCC14
		public override SqlDecimal GetSqlDecimal(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].SqlDecimal;
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x000BEA23 File Offset: 0x000BCC23
		public override DateTime GetDateTime(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].DateTime;
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x000BEA32 File Offset: 0x000BCC32
		public override Guid GetGuid(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Guid;
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x000BEA41 File Offset: 0x000BCC41
		public override TimeSpan GetTimeSpan(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].TimeSpan;
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000BEA50 File Offset: 0x000BCC50
		public override DateTimeOffset GetDateTimeOffset(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].DateTimeOffset;
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000BEA5F File Offset: 0x000BCC5F
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._buffer[ordinal].SetNull();
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x000BEA6E File Offset: 0x000BCC6E
		public override void SetBoolean(SmiEventSink sink, int ordinal, bool value)
		{
			this._buffer[ordinal].Boolean = value;
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x000BEA7E File Offset: 0x000BCC7E
		public override void SetByte(SmiEventSink sink, int ordinal, byte value)
		{
			this._buffer[ordinal].Byte = value;
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000BEA8E File Offset: 0x000BCC8E
		public override int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].SetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000BEAA4 File Offset: 0x000BCCA4
		public override void SetBytesLength(SmiEventSink sink, int ordinal, long length)
		{
			this._buffer[ordinal].BytesLength = length;
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x000BEAB4 File Offset: 0x000BCCB4
		public override int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].SetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x000BEACA File Offset: 0x000BCCCA
		public override void SetCharsLength(SmiEventSink sink, int ordinal, long length)
		{
			this._buffer[ordinal].CharsLength = length;
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x000BEADA File Offset: 0x000BCCDA
		public override void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length)
		{
			this._buffer[ordinal].String = value.Substring(offset, length);
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000BEAF3 File Offset: 0x000BCCF3
		public override void SetInt16(SmiEventSink sink, int ordinal, short value)
		{
			this._buffer[ordinal].Int16 = value;
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x000BEB03 File Offset: 0x000BCD03
		public override void SetInt32(SmiEventSink sink, int ordinal, int value)
		{
			this._buffer[ordinal].Int32 = value;
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000BEB13 File Offset: 0x000BCD13
		public override void SetInt64(SmiEventSink sink, int ordinal, long value)
		{
			this._buffer[ordinal].Int64 = value;
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x000BEB23 File Offset: 0x000BCD23
		public override void SetSingle(SmiEventSink sink, int ordinal, float value)
		{
			this._buffer[ordinal].Single = value;
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x000BEB33 File Offset: 0x000BCD33
		public override void SetDouble(SmiEventSink sink, int ordinal, double value)
		{
			this._buffer[ordinal].Double = value;
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x000BEB43 File Offset: 0x000BCD43
		public override void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value)
		{
			this._buffer[ordinal].SqlDecimal = value;
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x000BEB53 File Offset: 0x000BCD53
		public override void SetDateTime(SmiEventSink sink, int ordinal, DateTime value)
		{
			this._buffer[ordinal].DateTime = value;
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000BEB63 File Offset: 0x000BCD63
		public override void SetGuid(SmiEventSink sink, int ordinal, Guid value)
		{
			this._buffer[ordinal].Guid = value;
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000BEB73 File Offset: 0x000BCD73
		public override void SetTimeSpan(SmiEventSink sink, int ordinal, TimeSpan value)
		{
			this._buffer[ordinal].TimeSpan = value;
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x000BEB83 File Offset: 0x000BCD83
		public override void SetDateTimeOffset(SmiEventSink sink, int ordinal, DateTimeOffset value)
		{
			this._buffer[ordinal].DateTimeOffset = value;
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x000BEB93 File Offset: 0x000BCD93
		public override void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData)
		{
			this._buffer[ordinal].VariantType = metaData;
		}

		// Token: 0x04001A54 RID: 6740
		private SqlRecordBuffer[] _buffer;
	}
}
