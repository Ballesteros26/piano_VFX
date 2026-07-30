using System;
using System.Data.Common;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003AA RID: 938
	internal abstract class SmiTypedGetterSetter : ITypedGettersV3, ITypedSettersV3
	{
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002C32 RID: 11314
		internal abstract bool CanGet { get; }

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002C33 RID: 11315
		internal abstract bool CanSet { get; }

		// Token: 0x06002C34 RID: 11316 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual bool IsDBNull(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual SmiMetaData GetVariantType(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual bool GetBoolean(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual byte GetByte(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual long GetBytesLength(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual int GetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual long GetCharsLength(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual int GetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual string GetString(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual short GetInt16(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual int GetInt32(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual long GetInt64(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual float GetSingle(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual double GetDouble(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual SqlDecimal GetSqlDecimal(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual DateTime GetDateTime(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual Guid GetGuid(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual TimeSpan GetTimeSpan(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x000C0A94 File Offset: 0x000BEC94
		public virtual DateTimeOffset GetDateTimeOffset(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x000C09AB File Offset: 0x000BEBAB
		internal virtual SmiTypedGetterSetter GetTypedGetterSetter(SmiEventSink sink, int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual void SetDBNull(SmiEventSink sink, int ordinal)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual void SetBoolean(SmiEventSink sink, int ordinal, bool value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual void SetByte(SmiEventSink sink, int ordinal, byte value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual void SetBytesLength(SmiEventSink sink, int ordinal, long length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual void SetCharsLength(SmiEventSink sink, int ordinal, long length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual void SetInt16(SmiEventSink sink, int ordinal, short value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual void SetInt32(SmiEventSink sink, int ordinal, int value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual void SetInt64(SmiEventSink sink, int ordinal, long value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual void SetSingle(SmiEventSink sink, int ordinal, float value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual void SetDouble(SmiEventSink sink, int ordinal, double value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual void SetDateTime(SmiEventSink sink, int ordinal, DateTime value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual void SetGuid(SmiEventSink sink, int ordinal, Guid value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual void SetTimeSpan(SmiEventSink sink, int ordinal, TimeSpan value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x000C0AAD File Offset: 0x000BECAD
		public virtual void SetDateTimeOffset(SmiEventSink sink, int ordinal, DateTimeOffset value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x000C0AAD File Offset: 0x000BECAD
		internal virtual void NewElement(SmiEventSink sink)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000C0AAD File Offset: 0x000BECAD
		internal virtual void EndElements(SmiEventSink sink)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}
	}
}
