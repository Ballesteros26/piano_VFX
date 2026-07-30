using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000231 RID: 561
	internal class TdsRecordBufferSetter : SmiRecordBuffer
	{
		// Token: 0x06001955 RID: 6485 RVA: 0x00080FDC File Offset: 0x0007F1DC
		internal TdsRecordBufferSetter(TdsParserStateObject stateObj, SmiMetaData md)
		{
			this._fieldSetters = new TdsValueSetter[md.FieldMetaData.Count];
			for (int i = 0; i < md.FieldMetaData.Count; i++)
			{
				this._fieldSetters[i] = new TdsValueSetter(stateObj, md.FieldMetaData[i]);
			}
			this._stateObj = stateObj;
			this._metaData = md;
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x000061D5 File Offset: 0x000043D5
		internal override bool CanGet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x0000EF2B File Offset: 0x0000D12B
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x00081043 File Offset: 0x0007F243
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._fieldSetters[ordinal].SetDBNull();
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x00081052 File Offset: 0x0007F252
		public override void SetBoolean(SmiEventSink sink, int ordinal, bool value)
		{
			this._fieldSetters[ordinal].SetBoolean(value);
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x00081062 File Offset: 0x0007F262
		public override void SetByte(SmiEventSink sink, int ordinal, byte value)
		{
			this._fieldSetters[ordinal].SetByte(value);
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x00081072 File Offset: 0x0007F272
		public override int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._fieldSetters[ordinal].SetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x00081088 File Offset: 0x0007F288
		public override void SetBytesLength(SmiEventSink sink, int ordinal, long length)
		{
			this._fieldSetters[ordinal].SetBytesLength(length);
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x00081098 File Offset: 0x0007F298
		public override int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._fieldSetters[ordinal].SetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x000810AE File Offset: 0x0007F2AE
		public override void SetCharsLength(SmiEventSink sink, int ordinal, long length)
		{
			this._fieldSetters[ordinal].SetCharsLength(length);
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x000810BE File Offset: 0x0007F2BE
		public override void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length)
		{
			this._fieldSetters[ordinal].SetString(value, offset, length);
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x000810D2 File Offset: 0x0007F2D2
		public override void SetInt16(SmiEventSink sink, int ordinal, short value)
		{
			this._fieldSetters[ordinal].SetInt16(value);
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x000810E2 File Offset: 0x0007F2E2
		public override void SetInt32(SmiEventSink sink, int ordinal, int value)
		{
			this._fieldSetters[ordinal].SetInt32(value);
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x000810F2 File Offset: 0x0007F2F2
		public override void SetInt64(SmiEventSink sink, int ordinal, long value)
		{
			this._fieldSetters[ordinal].SetInt64(value);
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x00081102 File Offset: 0x0007F302
		public override void SetSingle(SmiEventSink sink, int ordinal, float value)
		{
			this._fieldSetters[ordinal].SetSingle(value);
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00081112 File Offset: 0x0007F312
		public override void SetDouble(SmiEventSink sink, int ordinal, double value)
		{
			this._fieldSetters[ordinal].SetDouble(value);
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x00081122 File Offset: 0x0007F322
		public override void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value)
		{
			this._fieldSetters[ordinal].SetSqlDecimal(value);
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00081132 File Offset: 0x0007F332
		public override void SetDateTime(SmiEventSink sink, int ordinal, DateTime value)
		{
			this._fieldSetters[ordinal].SetDateTime(value);
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00081142 File Offset: 0x0007F342
		public override void SetGuid(SmiEventSink sink, int ordinal, Guid value)
		{
			this._fieldSetters[ordinal].SetGuid(value);
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00081152 File Offset: 0x0007F352
		public override void SetTimeSpan(SmiEventSink sink, int ordinal, TimeSpan value)
		{
			this._fieldSetters[ordinal].SetTimeSpan(value);
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x00081162 File Offset: 0x0007F362
		public override void SetDateTimeOffset(SmiEventSink sink, int ordinal, DateTimeOffset value)
		{
			this._fieldSetters[ordinal].SetDateTimeOffset(value);
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00081172 File Offset: 0x0007F372
		public override void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData)
		{
			this._fieldSetters[ordinal].SetVariantType(metaData);
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00081182 File Offset: 0x0007F382
		internal override void NewElement(SmiEventSink sink)
		{
			this._stateObj.WriteByte(1);
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x00081190 File Offset: 0x0007F390
		internal override void EndElements(SmiEventSink sink)
		{
			this._stateObj.WriteByte(0);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00005E03 File Offset: 0x00004003
		[Conditional("DEBUG")]
		private void CheckWritingToColumn(int ordinal)
		{
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x00005E03 File Offset: 0x00004003
		[Conditional("DEBUG")]
		private void SkipPossibleDefaultedColumns(int targetColumn)
		{
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x00005E03 File Offset: 0x00004003
		[Conditional("DEBUG")]
		internal void CheckSettingColumn(int ordinal)
		{
		}

		// Token: 0x0400122A RID: 4650
		private TdsValueSetter[] _fieldSetters;

		// Token: 0x0400122B RID: 4651
		private TdsParserStateObject _stateObj;

		// Token: 0x0400122C RID: 4652
		private SmiMetaData _metaData;
	}
}
