using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003A8 RID: 936
	internal abstract class SmiRecordBuffer : SmiTypedGetterSetter, ITypedGettersV3, ITypedSettersV3, ITypedGetters, ITypedSetters, IDisposable
	{
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002BDB RID: 11227 RVA: 0x0000EF2B File Offset: 0x0000D12B
		internal override bool CanGet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002BDC RID: 11228 RVA: 0x0000EF2B File Offset: 0x0000D12B
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void Dispose()
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual bool IsDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlDbType GetVariantType(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual bool GetBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual byte GetByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual char GetChar(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual short GetInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual int GetInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual long GetInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual float GetFloat(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual double GetDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual string GetString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual decimal GetDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual DateTime GetDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual Guid GetGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlBoolean GetSqlBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlByte GetSqlByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlInt16 GetSqlInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlInt32 GetSqlInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlInt64 GetSqlInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlSingle GetSqlSingle(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlDouble GetSqlDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlMoney GetSqlMoney(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlDateTime GetSqlDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlDecimal GetSqlDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlString GetSqlString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlBinary GetSqlBinary(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlGuid GetSqlGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlChars GetSqlChars(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlBytes GetSqlBytes(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlXml GetSqlXml(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlXml GetSqlXmlRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlBytes GetSqlBytesRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual SqlChars GetSqlCharsRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetBoolean(int ordinal, bool value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetByte(int ordinal, byte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetChar(int ordinal, char value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetInt16(int ordinal, short value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetInt32(int ordinal, int value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetInt64(int ordinal, long value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetFloat(int ordinal, float value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetDouble(int ordinal, double value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetString(int ordinal, string value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetString(int ordinal, string value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetDecimal(int ordinal, decimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetDateTime(int ordinal, DateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetGuid(int ordinal, Guid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlBoolean(int ordinal, SqlBoolean value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlByte(int ordinal, SqlByte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlInt16(int ordinal, SqlInt16 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlInt32(int ordinal, SqlInt32 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlInt64(int ordinal, SqlInt64 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlSingle(int ordinal, SqlSingle value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlDouble(int ordinal, SqlDouble value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlMoney(int ordinal, SqlMoney value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlDateTime(int ordinal, SqlDateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlDecimal(int ordinal, SqlDecimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlString(int ordinal, SqlString value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlString(int ordinal, SqlString value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlBinary(int ordinal, SqlBinary value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlBinary(int ordinal, SqlBinary value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlGuid(int ordinal, SqlGuid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlChars(int ordinal, SqlChars value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlChars(int ordinal, SqlChars value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlBytes(int ordinal, SqlBytes value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlBytes(int ordinal, SqlBytes value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public virtual void SetSqlXml(int ordinal, SqlXml value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}
	}
}
