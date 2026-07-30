using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003B1 RID: 945
	internal static class ValueUtilsSmi
	{
		// Token: 0x06002CE4 RID: 11492 RVA: 0x000C201A File Offset: 0x000C021A
		internal static bool IsDBNull(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			return ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal);
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x000C2024 File Offset: 0x000C0224
		internal static bool GetBoolean(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Boolean))
			{
				return ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (bool)value;
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x000C2064 File Offset: 0x000C0264
		internal static byte GetByte(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Byte))
			{
				return ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (byte)value;
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x000C20A4 File Offset: 0x000C02A4
		private static long GetBytesConversion(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, long fieldOffset, byte[] buffer, int bufferOffset, int length, bool throwOnNull)
		{
			object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
			if (sqlValue == null)
			{
				throw ADP.InvalidCast();
			}
			SqlBinary sqlBinary = (SqlBinary)sqlValue;
			if (sqlBinary.IsNull)
			{
				if (throwOnNull)
				{
					throw SQL.SqlNullValue();
				}
				return 0L;
			}
			else
			{
				if (buffer == null)
				{
					return (long)sqlBinary.Length;
				}
				length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength * 2L, (long)sqlBinary.Length, fieldOffset, buffer.Length, bufferOffset, length);
				Array.Copy(sqlBinary.Value, checked((int)fieldOffset), buffer, bufferOffset, length);
				return (long)length;
			}
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x000C2130 File Offset: 0x000C0330
		internal static long GetBytes(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiExtendedMetaData metaData, long fieldOffset, byte[] buffer, int bufferOffset, int length, bool throwOnNull)
		{
			if ((-1L != metaData.MaxLength && (SqlDbType.VarChar == metaData.SqlDbType || SqlDbType.NVarChar == metaData.SqlDbType || SqlDbType.Char == metaData.SqlDbType || SqlDbType.NChar == metaData.SqlDbType)) || SqlDbType.Xml == metaData.SqlDbType)
			{
				throw SQL.NonBlobColumn(metaData.Name);
			}
			return ValueUtilsSmi.GetBytesInternal(sink, getters, ordinal, metaData, fieldOffset, buffer, bufferOffset, length, throwOnNull);
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x000C2198 File Offset: 0x000C0398
		internal static long GetBytesInternal(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, long fieldOffset, byte[] buffer, int bufferOffset, int length, bool throwOnNull)
		{
			if (!ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.ByteArray))
			{
				return ValueUtilsSmi.GetBytesConversion(sink, getters, ordinal, metaData, fieldOffset, buffer, bufferOffset, length, throwOnNull);
			}
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				if (throwOnNull)
				{
					throw SQL.SqlNullValue();
				}
				ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, 0L, fieldOffset, buffer.Length, bufferOffset, length);
				return 0L;
			}
			else
			{
				long bytesLength_Unchecked = ValueUtilsSmi.GetBytesLength_Unchecked(sink, getters, ordinal);
				if (buffer == null)
				{
					return bytesLength_Unchecked;
				}
				if (MetaDataUtilsSmi.IsCharOrXmlType(metaData.SqlDbType))
				{
					length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength * 2L, bytesLength_Unchecked, fieldOffset, buffer.Length, bufferOffset, length);
				}
				else
				{
					length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, bytesLength_Unchecked, fieldOffset, buffer.Length, bufferOffset, length);
				}
				if (length > 0)
				{
					length = ValueUtilsSmi.GetBytes_Unchecked(sink, getters, ordinal, fieldOffset, buffer, bufferOffset, length);
				}
				return (long)length;
			}
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x000C2274 File Offset: 0x000C0474
		internal static long GetChars(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.CharArray))
			{
				long charsLength_Unchecked = ValueUtilsSmi.GetCharsLength_Unchecked(sink, getters, ordinal);
				if (buffer == null)
				{
					return charsLength_Unchecked;
				}
				length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, charsLength_Unchecked, fieldOffset, buffer.Length, bufferOffset, length);
				if (length > 0)
				{
					length = ValueUtilsSmi.GetChars_Unchecked(sink, getters, ordinal, fieldOffset, buffer, bufferOffset, length);
				}
				return (long)length;
			}
			else
			{
				string text = (string)ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
				if (text == null)
				{
					throw ADP.InvalidCast();
				}
				if (buffer == null)
				{
					return (long)text.Length;
				}
				length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength * 2L, (long)text.Length, fieldOffset, buffer.Length, bufferOffset, length);
				text.CopyTo(checked((int)fieldOffset), buffer, bufferOffset, length);
				return (long)length;
			}
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x000C2338 File Offset: 0x000C0538
		internal static DateTime GetDateTime(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.DateTime))
			{
				return ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (DateTime)value;
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x000C2378 File Offset: 0x000C0578
		internal static DateTimeOffset GetDateTimeOffset(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, bool gettersSupportKatmaiDateTime)
		{
			if (gettersSupportKatmaiDateTime)
			{
				return ValueUtilsSmi.GetDateTimeOffset(sink, (SmiTypedGetterSetter)getters, ordinal, metaData);
			}
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (DateTimeOffset)value;
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x000C23B9 File Offset: 0x000C05B9
		internal static DateTimeOffset GetDateTimeOffset(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.DateTimeOffset))
			{
				return ValueUtilsSmi.GetDateTimeOffset_Unchecked(sink, getters, ordinal);
			}
			return (DateTimeOffset)ValueUtilsSmi.GetValue200(sink, getters, ordinal, metaData);
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000C23E4 File Offset: 0x000C05E4
		internal static decimal GetDecimal(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Decimal))
			{
				return ValueUtilsSmi.GetDecimal_PossiblyMoney(sink, getters, ordinal, metaData);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (decimal)value;
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x000C2428 File Offset: 0x000C0628
		internal static double GetDouble(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Double))
			{
				return ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (double)value;
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000C2468 File Offset: 0x000C0668
		internal static Guid GetGuid(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Guid))
			{
				return ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (Guid)value;
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000C24AC File Offset: 0x000C06AC
		internal static short GetInt16(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Int16))
			{
				return ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (short)value;
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000C24EC File Offset: 0x000C06EC
		internal static int GetInt32(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Int32))
			{
				return ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (int)value;
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000C2530 File Offset: 0x000C0730
		internal static long GetInt64(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Int64))
			{
				return ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (long)value;
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000C2574 File Offset: 0x000C0774
		internal static float GetSingle(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Single))
			{
				return ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (float)value;
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000C25B8 File Offset: 0x000C07B8
		internal static SqlBinary GetSqlBinary(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlBinary))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					return SqlBinary.Null;
				}
				return ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				return (SqlBinary)sqlValue;
			}
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000C2604 File Offset: 0x000C0804
		internal static SqlBoolean GetSqlBoolean(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlBoolean))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					return SqlBoolean.Null;
				}
				return new SqlBoolean(ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal));
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				return (SqlBoolean)sqlValue;
			}
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000C2654 File Offset: 0x000C0854
		internal static SqlByte GetSqlByte(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlByte))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					return SqlByte.Null;
				}
				return new SqlByte(ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal));
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				return (SqlByte)sqlValue;
			}
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000C26A4 File Offset: 0x000C08A4
		internal static SqlBytes GetSqlBytes(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlBytes sqlBytes;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlBytes))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlBytes = SqlBytes.Null;
				}
				else
				{
					long bytesLength_Unchecked = ValueUtilsSmi.GetBytesLength_Unchecked(sink, getters, ordinal);
					if (0L <= bytesLength_Unchecked && bytesLength_Unchecked < 8000L)
					{
						sqlBytes = new SqlBytes(ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal));
					}
					else
					{
						sqlBytes = new SqlBytes(ValueUtilsSmi.CopyIntoNewSmiScratchStream(new SmiGettersStream(sink, getters, ordinal, metaData), sink));
					}
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				SqlBinary sqlBinary = (SqlBinary)sqlValue;
				if (sqlBinary.IsNull)
				{
					sqlBytes = SqlBytes.Null;
				}
				else
				{
					sqlBytes = new SqlBytes(sqlBinary.Value);
				}
			}
			return sqlBytes;
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000C2744 File Offset: 0x000C0944
		internal static SqlChars GetSqlChars(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlChars sqlChars;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlChars))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlChars = SqlChars.Null;
				}
				else
				{
					sqlChars = new SqlChars(ValueUtilsSmi.GetCharArray_Unchecked(sink, getters, ordinal));
				}
			}
			else if (SqlDbType.Xml == metaData.SqlDbType)
			{
				SqlXml sqlXml_Unchecked = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal);
				if (sqlXml_Unchecked.IsNull)
				{
					sqlChars = SqlChars.Null;
				}
				else
				{
					sqlChars = new SqlChars(sqlXml_Unchecked.Value.ToCharArray());
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				SqlString sqlString = (SqlString)sqlValue;
				if (sqlString.IsNull)
				{
					sqlChars = SqlChars.Null;
				}
				else
				{
					sqlChars = new SqlChars(sqlString.Value.ToCharArray());
				}
			}
			return sqlChars;
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000C27F4 File Offset: 0x000C09F4
		internal static SqlDateTime GetSqlDateTime(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlDateTime sqlDateTime;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlDateTime))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlDateTime = SqlDateTime.Null;
				}
				else
				{
					DateTime dateTime_Unchecked = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
					sqlDateTime = new SqlDateTime(dateTime_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlDateTime = (SqlDateTime)sqlValue;
			}
			return sqlDateTime;
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000C284C File Offset: 0x000C0A4C
		internal static SqlDecimal GetSqlDecimal(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlDecimal sqlDecimal;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlDecimal))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlDecimal = SqlDecimal.Null;
				}
				else
				{
					sqlDecimal = ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlDecimal = (SqlDecimal)sqlValue;
			}
			return sqlDecimal;
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000C289C File Offset: 0x000C0A9C
		internal static SqlDouble GetSqlDouble(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlDouble sqlDouble;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlDouble))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlDouble = SqlDouble.Null;
				}
				else
				{
					double double_Unchecked = ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal);
					sqlDouble = new SqlDouble(double_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlDouble = (SqlDouble)sqlValue;
			}
			return sqlDouble;
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000C28F4 File Offset: 0x000C0AF4
		internal static SqlGuid GetSqlGuid(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlGuid sqlGuid;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlGuid))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlGuid = SqlGuid.Null;
				}
				else
				{
					Guid guid_Unchecked = ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal);
					sqlGuid = new SqlGuid(guid_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlGuid = (SqlGuid)sqlValue;
			}
			return sqlGuid;
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x000C294C File Offset: 0x000C0B4C
		internal static SqlInt16 GetSqlInt16(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlInt16 sqlInt;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlInt16))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlInt = SqlInt16.Null;
				}
				else
				{
					short int16_Unchecked = ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal);
					sqlInt = new SqlInt16(int16_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlInt = (SqlInt16)sqlValue;
			}
			return sqlInt;
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x000C29A4 File Offset: 0x000C0BA4
		internal static SqlInt32 GetSqlInt32(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlInt32 sqlInt;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlInt32))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlInt = SqlInt32.Null;
				}
				else
				{
					int int32_Unchecked = ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal);
					sqlInt = new SqlInt32(int32_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlInt = (SqlInt32)sqlValue;
			}
			return sqlInt;
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x000C29FC File Offset: 0x000C0BFC
		internal static SqlInt64 GetSqlInt64(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlInt64 sqlInt;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlInt64))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlInt = SqlInt64.Null;
				}
				else
				{
					long int64_Unchecked = ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal);
					sqlInt = new SqlInt64(int64_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlInt = (SqlInt64)sqlValue;
			}
			return sqlInt;
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x000C2A54 File Offset: 0x000C0C54
		internal static SqlMoney GetSqlMoney(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlMoney sqlMoney;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlMoney))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlMoney = SqlMoney.Null;
				}
				else
				{
					sqlMoney = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlMoney = (SqlMoney)sqlValue;
			}
			return sqlMoney;
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000C2AA4 File Offset: 0x000C0CA4
		internal static SqlSingle GetSqlSingle(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlSingle sqlSingle;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlSingle))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlSingle = SqlSingle.Null;
				}
				else
				{
					float single_Unchecked = ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal);
					sqlSingle = new SqlSingle(single_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlSingle = (SqlSingle)sqlValue;
			}
			return sqlSingle;
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000C2AFC File Offset: 0x000C0CFC
		internal static SqlString GetSqlString(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlString sqlString;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlString))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlString = SqlString.Null;
				}
				else
				{
					string string_Unchecked = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					sqlString = new SqlString(string_Unchecked);
				}
			}
			else if (SqlDbType.Xml == metaData.SqlDbType)
			{
				SqlXml sqlXml_Unchecked = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal);
				if (sqlXml_Unchecked.IsNull)
				{
					sqlString = SqlString.Null;
				}
				else
				{
					sqlString = new SqlString(sqlXml_Unchecked.Value);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlString = (SqlString)sqlValue;
			}
			return sqlString;
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000C2B88 File Offset: 0x000C0D88
		internal static SqlXml GetSqlXml(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlXml sqlXml;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlXml))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlXml = SqlXml.Null;
				}
				else
				{
					sqlXml = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlXml = (SqlXml)sqlValue;
			}
			return sqlXml;
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000C2BD8 File Offset: 0x000C0DD8
		internal static string GetString(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.String))
			{
				return ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (string)value;
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000C2C19 File Offset: 0x000C0E19
		internal static TimeSpan GetTimeSpan(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.TimeSpan))
			{
				return ValueUtilsSmi.GetTimeSpan_Unchecked(sink, getters, ordinal);
			}
			return (TimeSpan)ValueUtilsSmi.GetValue200(sink, getters, ordinal, metaData);
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000C2C44 File Offset: 0x000C0E44
		internal static object GetValue200(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData)
		{
			object obj;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				obj = DBNull.Value;
			}
			else
			{
				SqlDbType sqlDbType = metaData.SqlDbType;
				if (sqlDbType != SqlDbType.Variant)
				{
					switch (sqlDbType)
					{
					case SqlDbType.Date:
					case SqlDbType.DateTime2:
						obj = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
						break;
					case SqlDbType.Time:
						obj = ValueUtilsSmi.GetTimeSpan_Unchecked(sink, getters, ordinal);
						break;
					case SqlDbType.DateTimeOffset:
						obj = ValueUtilsSmi.GetDateTimeOffset_Unchecked(sink, getters, ordinal);
						break;
					default:
						obj = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
						break;
					}
				}
				else
				{
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					obj = ValueUtilsSmi.GetValue200(sink, getters, ordinal, metaData);
				}
			}
			return obj;
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000C2CE4 File Offset: 0x000C0EE4
		internal static object GetValue(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			object obj = null;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				obj = DBNull.Value;
			}
			else
			{
				switch (metaData.SqlDbType)
				{
				case SqlDbType.BigInt:
					obj = ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Binary:
					obj = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Bit:
					obj = ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Char:
					obj = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.DateTime:
					obj = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Decimal:
					obj = ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal).Value;
					break;
				case SqlDbType.Float:
					obj = ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Image:
					obj = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Int:
					obj = ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Money:
					obj = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal).Value;
					break;
				case SqlDbType.NChar:
					obj = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.NText:
					obj = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.NVarChar:
					obj = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Real:
					obj = ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.UniqueIdentifier:
					obj = ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.SmallDateTime:
					obj = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.SmallInt:
					obj = ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.SmallMoney:
					obj = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal).Value;
					break;
				case SqlDbType.Text:
					obj = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Timestamp:
					obj = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.TinyInt:
					obj = ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.VarBinary:
					obj = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.VarChar:
					obj = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Variant:
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					obj = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData);
					break;
				case SqlDbType.Xml:
					obj = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal).Value;
					break;
				case SqlDbType.Udt:
					throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
				}
			}
			return obj;
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000C2F60 File Offset: 0x000C1160
		internal static object GetSqlValue200(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData)
		{
			object obj;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				if (SqlDbType.Udt == metaData.SqlDbType)
				{
					throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
				}
				obj = ValueUtilsSmi.s_typeSpecificNullForSqlValue[(int)metaData.SqlDbType];
			}
			else
			{
				SqlDbType sqlDbType = metaData.SqlDbType;
				if (sqlDbType != SqlDbType.Variant)
				{
					switch (sqlDbType)
					{
					case SqlDbType.Date:
					case SqlDbType.DateTime2:
						obj = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
						break;
					case SqlDbType.Time:
						obj = ValueUtilsSmi.GetTimeSpan_Unchecked(sink, getters, ordinal);
						break;
					case SqlDbType.DateTimeOffset:
						obj = ValueUtilsSmi.GetDateTimeOffset_Unchecked(sink, getters, ordinal);
						break;
					default:
						obj = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
						break;
					}
				}
				else
				{
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					obj = ValueUtilsSmi.GetSqlValue200(sink, getters, ordinal, metaData);
				}
			}
			return obj;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000C3028 File Offset: 0x000C1228
		internal static object GetSqlValue(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			object obj = null;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				if (SqlDbType.Udt == metaData.SqlDbType)
				{
					throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
				}
				obj = ValueUtilsSmi.s_typeSpecificNullForSqlValue[(int)metaData.SqlDbType];
			}
			else
			{
				switch (metaData.SqlDbType)
				{
				case SqlDbType.BigInt:
					obj = new SqlInt64(ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Binary:
					obj = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Bit:
					obj = new SqlBoolean(ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Char:
					obj = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.DateTime:
					obj = new SqlDateTime(ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Decimal:
					obj = ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Float:
					obj = new SqlDouble(ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Image:
					obj = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Int:
					obj = new SqlInt32(ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Money:
					obj = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.NChar:
					obj = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.NText:
					obj = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.NVarChar:
					obj = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Real:
					obj = new SqlSingle(ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.UniqueIdentifier:
					obj = new SqlGuid(ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.SmallDateTime:
					obj = new SqlDateTime(ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.SmallInt:
					obj = new SqlInt16(ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.SmallMoney:
					obj = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Text:
					obj = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Timestamp:
					obj = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.TinyInt:
					obj = new SqlByte(ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.VarBinary:
					obj = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.VarChar:
					obj = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Variant:
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					obj = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData);
					break;
				case SqlDbType.Xml:
					obj = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Udt:
					throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
				}
			}
			return obj;
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000C3334 File Offset: 0x000C1534
		internal static void SetDBNull(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, bool value)
		{
			ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, ordinal);
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000C333E File Offset: 0x000C153E
		internal static void SetBoolean(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, bool value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Boolean);
			ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x000C3351 File Offset: 0x000C1551
		internal static void SetByte(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, byte value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Byte);
			ValueUtilsSmi.SetByte_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000C3364 File Offset: 0x000C1564
		internal static long SetBytes(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.ByteArray);
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, fieldOffset, buffer.Length, bufferOffset, length);
			if (length == 0)
			{
				fieldOffset = 0L;
				bufferOffset = 0;
			}
			return (long)ValueUtilsSmi.SetBytes_Unchecked(sink, setters, ordinal, fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000C33C4 File Offset: 0x000C15C4
		internal static long SetBytesLength(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, long length)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.ByteArray);
			if (length < 0L)
			{
				throw ADP.InvalidDataLength(length);
			}
			if (metaData.MaxLength >= 0L && length > metaData.MaxLength)
			{
				length = metaData.MaxLength;
			}
			setters.SetBytesLength(sink, ordinal, length);
			sink.ProcessMessagesAndThrow();
			return length;
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000C3418 File Offset: 0x000C1618
		internal static long SetChars(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.CharArray);
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, fieldOffset, buffer.Length, bufferOffset, length);
			if (length == 0)
			{
				fieldOffset = 0L;
				bufferOffset = 0;
			}
			return (long)ValueUtilsSmi.SetChars_Unchecked(sink, setters, ordinal, fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000C3477 File Offset: 0x000C1677
		internal static void SetDateTime(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.DateTime);
			ValueUtilsSmi.SetDateTime_Checked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000C348B File Offset: 0x000C168B
		internal static void SetDateTimeOffset(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTimeOffset value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.DateTimeOffset);
			ValueUtilsSmi.SetDateTimeOffset_Unchecked(sink, (SmiTypedGetterSetter)setters, ordinal, value);
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x000C34A4 File Offset: 0x000C16A4
		internal static void SetDecimal(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, decimal value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Decimal);
			ValueUtilsSmi.SetDecimal_PossiblyMoney(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000C34B8 File Offset: 0x000C16B8
		internal static void SetDouble(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, double value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Double);
			ValueUtilsSmi.SetDouble_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000C34CB File Offset: 0x000C16CB
		internal static void SetGuid(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, Guid value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Guid);
			ValueUtilsSmi.SetGuid_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000C34DF File Offset: 0x000C16DF
		internal static void SetInt16(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, short value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Int16);
			ValueUtilsSmi.SetInt16_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000C34F2 File Offset: 0x000C16F2
		internal static void SetInt32(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, int value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Int32);
			ValueUtilsSmi.SetInt32_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000C3506 File Offset: 0x000C1706
		internal static void SetInt64(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, long value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Int64);
			ValueUtilsSmi.SetInt64_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x000C351A File Offset: 0x000C171A
		internal static void SetSingle(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, float value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Single);
			ValueUtilsSmi.SetSingle_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000C352E File Offset: 0x000C172E
		internal static void SetSqlBinary(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBinary value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlBinary);
			ValueUtilsSmi.SetSqlBinary_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000C3544 File Offset: 0x000C1744
		internal static void SetSqlBoolean(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBoolean value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlBoolean);
			ValueUtilsSmi.SetSqlBoolean_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x000C3558 File Offset: 0x000C1758
		internal static void SetSqlByte(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlByte value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlByte);
			ValueUtilsSmi.SetSqlByte_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000C356C File Offset: 0x000C176C
		internal static void SetSqlBytes(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBytes value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlBytes);
			ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000C3582 File Offset: 0x000C1782
		internal static void SetSqlChars(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlChars value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlChars);
			ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x000C3598 File Offset: 0x000C1798
		internal static void SetSqlDateTime(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDateTime value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlDateTime);
			ValueUtilsSmi.SetSqlDateTime_Checked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000C35AD File Offset: 0x000C17AD
		internal static void SetSqlDecimal(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDecimal value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlDecimal);
			ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000C35C1 File Offset: 0x000C17C1
		internal static void SetSqlDouble(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDouble value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlDouble);
			ValueUtilsSmi.SetSqlDouble_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000C35D5 File Offset: 0x000C17D5
		internal static void SetSqlGuid(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlGuid value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlGuid);
			ValueUtilsSmi.SetSqlGuid_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x000C35E9 File Offset: 0x000C17E9
		internal static void SetSqlInt16(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlInt16 value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlInt16);
			ValueUtilsSmi.SetSqlInt16_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x000C35FD File Offset: 0x000C17FD
		internal static void SetSqlInt32(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlInt32 value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlInt32);
			ValueUtilsSmi.SetSqlInt32_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x000C3611 File Offset: 0x000C1811
		internal static void SetSqlInt64(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlInt64 value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlInt64);
			ValueUtilsSmi.SetSqlInt64_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x000C3625 File Offset: 0x000C1825
		internal static void SetSqlMoney(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlMoney value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlMoney);
			ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000C363A File Offset: 0x000C183A
		internal static void SetSqlSingle(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlSingle value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlSingle);
			ValueUtilsSmi.SetSqlSingle_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x000C364E File Offset: 0x000C184E
		internal static void SetSqlString(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlString value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlString);
			ValueUtilsSmi.SetSqlString_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x000C3664 File Offset: 0x000C1864
		internal static void SetSqlXml(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlXml value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlXml);
			ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x000C3678 File Offset: 0x000C1878
		internal static void SetString(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, string value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.String);
			ValueUtilsSmi.SetString_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x000C368E File Offset: 0x000C188E
		internal static void SetTimeSpan(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, TimeSpan value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.TimeSpan);
			ValueUtilsSmi.SetTimeSpan_Checked(sink, (SmiTypedGetterSetter)setters, ordinal, metaData, value);
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x000C36A8 File Offset: 0x000C18A8
		internal static void SetCompatibleValue(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, object value, ExtendedClrTypeCode typeCode, int offset)
		{
			switch (typeCode)
			{
			case ExtendedClrTypeCode.Invalid:
				throw ADP.UnknownDataType(value.GetType());
			case ExtendedClrTypeCode.Boolean:
				ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, ordinal, (bool)value);
				return;
			case ExtendedClrTypeCode.Byte:
				ValueUtilsSmi.SetByte_Unchecked(sink, setters, ordinal, (byte)value);
				return;
			case ExtendedClrTypeCode.Char:
			{
				char[] array = new char[] { (char)value };
				ValueUtilsSmi.SetCompatibleValue(sink, setters, ordinal, metaData, array, ExtendedClrTypeCode.CharArray, 0);
				return;
			}
			case ExtendedClrTypeCode.DateTime:
				ValueUtilsSmi.SetDateTime_Checked(sink, setters, ordinal, metaData, (DateTime)value);
				return;
			case ExtendedClrTypeCode.DBNull:
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, ordinal);
				return;
			case ExtendedClrTypeCode.Decimal:
				ValueUtilsSmi.SetDecimal_PossiblyMoney(sink, setters, ordinal, metaData, (decimal)value);
				return;
			case ExtendedClrTypeCode.Double:
				ValueUtilsSmi.SetDouble_Unchecked(sink, setters, ordinal, (double)value);
				return;
			case ExtendedClrTypeCode.Empty:
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, ordinal);
				return;
			case ExtendedClrTypeCode.Int16:
				ValueUtilsSmi.SetInt16_Unchecked(sink, setters, ordinal, (short)value);
				return;
			case ExtendedClrTypeCode.Int32:
				ValueUtilsSmi.SetInt32_Unchecked(sink, setters, ordinal, (int)value);
				return;
			case ExtendedClrTypeCode.Int64:
				ValueUtilsSmi.SetInt64_Unchecked(sink, setters, ordinal, (long)value);
				return;
			case ExtendedClrTypeCode.SByte:
				throw ADP.InvalidCast();
			case ExtendedClrTypeCode.Single:
				ValueUtilsSmi.SetSingle_Unchecked(sink, setters, ordinal, (float)value);
				return;
			case ExtendedClrTypeCode.String:
				ValueUtilsSmi.SetString_LengthChecked(sink, setters, ordinal, metaData, (string)value, offset);
				return;
			case ExtendedClrTypeCode.UInt16:
				throw ADP.InvalidCast();
			case ExtendedClrTypeCode.UInt32:
				throw ADP.InvalidCast();
			case ExtendedClrTypeCode.UInt64:
				throw ADP.InvalidCast();
			case ExtendedClrTypeCode.Object:
				throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
			case ExtendedClrTypeCode.ByteArray:
				ValueUtilsSmi.SetByteArray_LengthChecked(sink, setters, ordinal, metaData, (byte[])value, offset);
				return;
			case ExtendedClrTypeCode.CharArray:
				ValueUtilsSmi.SetCharArray_LengthChecked(sink, setters, ordinal, metaData, (char[])value, offset);
				return;
			case ExtendedClrTypeCode.Guid:
				ValueUtilsSmi.SetGuid_Unchecked(sink, setters, ordinal, (Guid)value);
				return;
			case ExtendedClrTypeCode.SqlBinary:
				ValueUtilsSmi.SetSqlBinary_LengthChecked(sink, setters, ordinal, metaData, (SqlBinary)value, offset);
				return;
			case ExtendedClrTypeCode.SqlBoolean:
				ValueUtilsSmi.SetSqlBoolean_Unchecked(sink, setters, ordinal, (SqlBoolean)value);
				return;
			case ExtendedClrTypeCode.SqlByte:
				ValueUtilsSmi.SetSqlByte_Unchecked(sink, setters, ordinal, (SqlByte)value);
				return;
			case ExtendedClrTypeCode.SqlDateTime:
				ValueUtilsSmi.SetSqlDateTime_Checked(sink, setters, ordinal, metaData, (SqlDateTime)value);
				return;
			case ExtendedClrTypeCode.SqlDouble:
				ValueUtilsSmi.SetSqlDouble_Unchecked(sink, setters, ordinal, (SqlDouble)value);
				return;
			case ExtendedClrTypeCode.SqlGuid:
				ValueUtilsSmi.SetSqlGuid_Unchecked(sink, setters, ordinal, (SqlGuid)value);
				return;
			case ExtendedClrTypeCode.SqlInt16:
				ValueUtilsSmi.SetSqlInt16_Unchecked(sink, setters, ordinal, (SqlInt16)value);
				return;
			case ExtendedClrTypeCode.SqlInt32:
				ValueUtilsSmi.SetSqlInt32_Unchecked(sink, setters, ordinal, (SqlInt32)value);
				return;
			case ExtendedClrTypeCode.SqlInt64:
				ValueUtilsSmi.SetSqlInt64_Unchecked(sink, setters, ordinal, (SqlInt64)value);
				return;
			case ExtendedClrTypeCode.SqlMoney:
				ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, ordinal, metaData, (SqlMoney)value);
				return;
			case ExtendedClrTypeCode.SqlDecimal:
				ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, ordinal, (SqlDecimal)value);
				return;
			case ExtendedClrTypeCode.SqlSingle:
				ValueUtilsSmi.SetSqlSingle_Unchecked(sink, setters, ordinal, (SqlSingle)value);
				return;
			case ExtendedClrTypeCode.SqlString:
				ValueUtilsSmi.SetSqlString_LengthChecked(sink, setters, ordinal, metaData, (SqlString)value, offset);
				return;
			case ExtendedClrTypeCode.SqlChars:
				ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, ordinal, metaData, (SqlChars)value, offset);
				return;
			case ExtendedClrTypeCode.SqlBytes:
				ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, ordinal, metaData, (SqlBytes)value, offset);
				return;
			case ExtendedClrTypeCode.SqlXml:
				ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, ordinal, (SqlXml)value);
				return;
			case ExtendedClrTypeCode.DataTable:
			case ExtendedClrTypeCode.DbDataReader:
			case ExtendedClrTypeCode.IEnumerableOfSqlDataRecord:
			case ExtendedClrTypeCode.TimeSpan:
			case ExtendedClrTypeCode.DateTimeOffset:
				break;
			case ExtendedClrTypeCode.Stream:
				ValueUtilsSmi.SetStream_Unchecked(sink, setters, ordinal, metaData, (StreamDataFeed)value);
				return;
			case ExtendedClrTypeCode.TextReader:
				ValueUtilsSmi.SetTextReader_Unchecked(sink, setters, ordinal, metaData, (TextDataFeed)value);
				return;
			case ExtendedClrTypeCode.XmlReader:
				ValueUtilsSmi.SetXmlReader_Unchecked(sink, setters, ordinal, ((XmlDataFeed)value)._source);
				break;
			default:
				return;
			}
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x000C3A04 File Offset: 0x000C1C04
		internal static void SetCompatibleValueV200(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, object value, ExtendedClrTypeCode typeCode, int offset, int length, ParameterPeekAheadValue peekAhead, SqlBuffer.StorageType storageType)
		{
			if (typeCode != ExtendedClrTypeCode.DateTime)
			{
				ValueUtilsSmi.SetCompatibleValueV200(sink, setters, ordinal, metaData, value, typeCode, offset, length, peekAhead);
				return;
			}
			if (storageType == SqlBuffer.StorageType.DateTime2)
			{
				ValueUtilsSmi.SetDateTime2_Checked(sink, setters, ordinal, metaData, (DateTime)value);
				return;
			}
			if (storageType == SqlBuffer.StorageType.Date)
			{
				ValueUtilsSmi.SetDate_Checked(sink, setters, ordinal, metaData, (DateTime)value);
				return;
			}
			ValueUtilsSmi.SetDateTime_Checked(sink, setters, ordinal, metaData, (DateTime)value);
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x000C3A68 File Offset: 0x000C1C68
		internal static void SetCompatibleValueV200(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, object value, ExtendedClrTypeCode typeCode, int offset, int length, ParameterPeekAheadValue peekAhead)
		{
			switch (typeCode)
			{
			case ExtendedClrTypeCode.DataTable:
				ValueUtilsSmi.SetDataTable_Unchecked(sink, setters, ordinal, metaData, (DataTable)value);
				return;
			case ExtendedClrTypeCode.DbDataReader:
				ValueUtilsSmi.SetDbDataReader_Unchecked(sink, setters, ordinal, metaData, (DbDataReader)value);
				return;
			case ExtendedClrTypeCode.IEnumerableOfSqlDataRecord:
				ValueUtilsSmi.SetIEnumerableOfSqlDataRecord_Unchecked(sink, setters, ordinal, metaData, (IEnumerable<SqlDataRecord>)value, peekAhead);
				return;
			case ExtendedClrTypeCode.TimeSpan:
				ValueUtilsSmi.SetTimeSpan_Checked(sink, setters, ordinal, metaData, (TimeSpan)value);
				return;
			case ExtendedClrTypeCode.DateTimeOffset:
				ValueUtilsSmi.SetDateTimeOffset_Unchecked(sink, setters, ordinal, (DateTimeOffset)value);
				return;
			default:
				ValueUtilsSmi.SetCompatibleValue(sink, setters, ordinal, metaData, value, typeCode, offset);
				return;
			}
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x000C3AFC File Offset: 0x000C1CFC
		private static void SetDataTable_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DataTable value)
		{
			setters = setters.GetTypedGetterSetter(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			ExtendedClrTypeCode[] array = new ExtendedClrTypeCode[metaData.FieldMetaData.Count];
			for (int i = 0; i < metaData.FieldMetaData.Count; i++)
			{
				array[i] = ExtendedClrTypeCode.Invalid;
			}
			foreach (object obj in value.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				setters.NewElement(sink);
				sink.ProcessMessagesAndThrow();
				for (int j = 0; j < metaData.FieldMetaData.Count; j++)
				{
					SmiMetaData smiMetaData = metaData.FieldMetaData[j];
					if (dataRow.IsNull(j))
					{
						ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, j);
					}
					else
					{
						object obj2 = dataRow[j];
						if (ExtendedClrTypeCode.Invalid == array[j])
						{
							array[j] = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(smiMetaData.SqlDbType, smiMetaData.IsMultiValued, obj2);
						}
						ValueUtilsSmi.SetCompatibleValueV200(sink, setters, j, smiMetaData, obj2, array[j], 0, -1, null);
					}
				}
			}
			setters.EndElements(sink);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x000C3C2C File Offset: 0x000C1E2C
		internal static void FillCompatibleITypedSettersFromReader(SmiEventSink_Default sink, ITypedSettersV3 setters, SmiMetaData[] metaData, SqlDataReader reader)
		{
			for (int i = 0; i < metaData.Length; i++)
			{
				if (!reader.IsDBNull(i))
				{
					switch (metaData[i].SqlDbType)
					{
					case SqlDbType.BigInt:
						ValueUtilsSmi.SetInt64_Unchecked(sink, setters, i, reader.GetInt64(i));
						goto IL_02BC;
					case SqlDbType.Binary:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_02BC;
					case SqlDbType.Bit:
						ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, i, reader.GetBoolean(i));
						goto IL_02BC;
					case SqlDbType.Char:
						ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlChars(i), 0);
						goto IL_02BC;
					case SqlDbType.DateTime:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_02BC;
					case SqlDbType.Decimal:
						ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, reader.GetSqlDecimal(i));
						goto IL_02BC;
					case SqlDbType.Float:
						ValueUtilsSmi.SetDouble_Unchecked(sink, setters, i, reader.GetDouble(i));
						goto IL_02BC;
					case SqlDbType.Image:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_02BC;
					case SqlDbType.Int:
						ValueUtilsSmi.SetInt32_Unchecked(sink, setters, i, reader.GetInt32(i));
						goto IL_02BC;
					case SqlDbType.Money:
						ValueUtilsSmi.SetSqlMoney_Unchecked(sink, setters, i, metaData[i], reader.GetSqlMoney(i));
						goto IL_02BC;
					case SqlDbType.NChar:
					case SqlDbType.NText:
					case SqlDbType.NVarChar:
						ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlChars(i), 0);
						goto IL_02BC;
					case SqlDbType.Real:
						ValueUtilsSmi.SetSingle_Unchecked(sink, setters, i, reader.GetFloat(i));
						goto IL_02BC;
					case SqlDbType.UniqueIdentifier:
						ValueUtilsSmi.SetGuid_Unchecked(sink, setters, i, reader.GetGuid(i));
						goto IL_02BC;
					case SqlDbType.SmallDateTime:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_02BC;
					case SqlDbType.SmallInt:
						ValueUtilsSmi.SetInt16_Unchecked(sink, setters, i, reader.GetInt16(i));
						goto IL_02BC;
					case SqlDbType.SmallMoney:
						ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], reader.GetSqlMoney(i));
						goto IL_02BC;
					case SqlDbType.Text:
						ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlChars(i), 0);
						goto IL_02BC;
					case SqlDbType.Timestamp:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_02BC;
					case SqlDbType.TinyInt:
						ValueUtilsSmi.SetByte_Unchecked(sink, setters, i, reader.GetByte(i));
						goto IL_02BC;
					case SqlDbType.VarBinary:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_02BC;
					case SqlDbType.VarChar:
						ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlChars(i), 0);
						goto IL_02BC;
					case SqlDbType.Variant:
					{
						object sqlValue = reader.GetSqlValue(i);
						ExtendedClrTypeCode extendedClrTypeCode = MetaDataUtilsSmi.DetermineExtendedTypeCode(sqlValue);
						ValueUtilsSmi.SetCompatibleValue(sink, setters, i, metaData[i], sqlValue, extendedClrTypeCode, 0);
						goto IL_02BC;
					}
					case SqlDbType.Xml:
						ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, i, reader.GetSqlXml(i));
						goto IL_02BC;
					case SqlDbType.Udt:
						throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
					}
					throw ADP.NotSupported();
				}
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, i);
				IL_02BC:;
			}
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x000C3F04 File Offset: 0x000C2104
		internal static void FillCompatibleSettersFromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, IList<SmiExtendedMetaData> metaData, DbDataReader reader)
		{
			for (int i = 0; i < metaData.Count; i++)
			{
				if (!reader.IsDBNull(i))
				{
					switch (metaData[i].SqlDbType)
					{
					case SqlDbType.BigInt:
						ValueUtilsSmi.SetInt64_Unchecked(sink, setters, i, reader.GetInt64(i));
						goto IL_0424;
					case SqlDbType.Binary:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0424;
					case SqlDbType.Bit:
						ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, i, reader.GetBoolean(i));
						goto IL_0424;
					case SqlDbType.Char:
						ValueUtilsSmi.SetCharsOrString_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0424;
					case SqlDbType.DateTime:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_0424;
					case SqlDbType.Decimal:
					{
						SqlDataReader sqlDataReader = reader as SqlDataReader;
						if (sqlDataReader != null)
						{
							ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, sqlDataReader.GetSqlDecimal(i));
							goto IL_0424;
						}
						ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, new SqlDecimal(reader.GetDecimal(i)));
						goto IL_0424;
					}
					case SqlDbType.Float:
						ValueUtilsSmi.SetDouble_Unchecked(sink, setters, i, reader.GetDouble(i));
						goto IL_0424;
					case SqlDbType.Image:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0424;
					case SqlDbType.Int:
						ValueUtilsSmi.SetInt32_Unchecked(sink, setters, i, reader.GetInt32(i));
						goto IL_0424;
					case SqlDbType.Money:
						ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], new SqlMoney(reader.GetDecimal(i)));
						goto IL_0424;
					case SqlDbType.NChar:
					case SqlDbType.NText:
					case SqlDbType.NVarChar:
						ValueUtilsSmi.SetCharsOrString_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0424;
					case SqlDbType.Real:
						ValueUtilsSmi.SetSingle_Unchecked(sink, setters, i, reader.GetFloat(i));
						goto IL_0424;
					case SqlDbType.UniqueIdentifier:
						ValueUtilsSmi.SetGuid_Unchecked(sink, setters, i, reader.GetGuid(i));
						goto IL_0424;
					case SqlDbType.SmallDateTime:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_0424;
					case SqlDbType.SmallInt:
						ValueUtilsSmi.SetInt16_Unchecked(sink, setters, i, reader.GetInt16(i));
						goto IL_0424;
					case SqlDbType.SmallMoney:
						ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], new SqlMoney(reader.GetDecimal(i)));
						goto IL_0424;
					case SqlDbType.Text:
						ValueUtilsSmi.SetCharsOrString_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0424;
					case SqlDbType.Timestamp:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0424;
					case SqlDbType.TinyInt:
						ValueUtilsSmi.SetByte_Unchecked(sink, setters, i, reader.GetByte(i));
						goto IL_0424;
					case SqlDbType.VarBinary:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0424;
					case SqlDbType.VarChar:
						ValueUtilsSmi.SetCharsOrString_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0424;
					case SqlDbType.Variant:
					{
						SqlDataReader sqlDataReader2 = reader as SqlDataReader;
						SqlBuffer.StorageType storageType = SqlBuffer.StorageType.Empty;
						object obj;
						if (sqlDataReader2 != null)
						{
							obj = sqlDataReader2.GetSqlValue(i);
							storageType = sqlDataReader2.GetVariantInternalStorageType(i);
						}
						else
						{
							obj = reader.GetValue(i);
						}
						ExtendedClrTypeCode extendedClrTypeCode = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(metaData[i].SqlDbType, metaData[i].IsMultiValued, obj);
						if (storageType == SqlBuffer.StorageType.DateTime2 || storageType == SqlBuffer.StorageType.Date)
						{
							ValueUtilsSmi.SetCompatibleValueV200(sink, setters, i, metaData[i], obj, extendedClrTypeCode, 0, 0, null, storageType);
							goto IL_0424;
						}
						ValueUtilsSmi.SetCompatibleValueV200(sink, setters, i, metaData[i], obj, extendedClrTypeCode, 0, 0, null);
						goto IL_0424;
					}
					case SqlDbType.Xml:
					{
						SqlDataReader sqlDataReader3 = reader as SqlDataReader;
						if (sqlDataReader3 != null)
						{
							ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, i, sqlDataReader3.GetSqlXml(i));
							goto IL_0424;
						}
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0424;
					}
					case SqlDbType.Udt:
						throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
					case SqlDbType.Date:
					case SqlDbType.DateTime2:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_0424;
					case SqlDbType.Time:
					{
						SqlDataReader sqlDataReader4 = reader as SqlDataReader;
						TimeSpan timeSpan;
						if (sqlDataReader4 != null)
						{
							timeSpan = sqlDataReader4.GetTimeSpan(i);
						}
						else
						{
							timeSpan = (TimeSpan)reader.GetValue(i);
						}
						ValueUtilsSmi.SetTimeSpan_Checked(sink, setters, i, metaData[i], timeSpan);
						goto IL_0424;
					}
					case SqlDbType.DateTimeOffset:
					{
						SqlDataReader sqlDataReader5 = reader as SqlDataReader;
						DateTimeOffset dateTimeOffset;
						if (sqlDataReader5 != null)
						{
							dateTimeOffset = sqlDataReader5.GetDateTimeOffset(i);
						}
						else
						{
							dateTimeOffset = (DateTimeOffset)reader.GetValue(i);
						}
						ValueUtilsSmi.SetDateTimeOffset_Unchecked(sink, setters, i, dateTimeOffset);
						goto IL_0424;
					}
					}
					throw ADP.NotSupported();
				}
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, i);
				IL_0424:;
			}
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x000C4348 File Offset: 0x000C2548
		internal static void FillCompatibleSettersFromRecord(SmiEventSink_Default sink, SmiTypedGetterSetter setters, SmiMetaData[] metaData, SqlDataRecord record, SmiDefaultFieldsProperty useDefaultValues)
		{
			for (int i = 0; i < metaData.Length; i++)
			{
				if (useDefaultValues == null || !useDefaultValues[i])
				{
					if (!record.IsDBNull(i))
					{
						switch (metaData[i].SqlDbType)
						{
						case SqlDbType.BigInt:
							ValueUtilsSmi.SetInt64_Unchecked(sink, setters, i, record.GetInt64(i));
							goto IL_0333;
						case SqlDbType.Binary:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0333;
						case SqlDbType.Bit:
							ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, i, record.GetBoolean(i));
							goto IL_0333;
						case SqlDbType.Char:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0333;
						case SqlDbType.DateTime:
							ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], record.GetDateTime(i));
							goto IL_0333;
						case SqlDbType.Decimal:
							ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, record.GetSqlDecimal(i));
							goto IL_0333;
						case SqlDbType.Float:
							ValueUtilsSmi.SetDouble_Unchecked(sink, setters, i, record.GetDouble(i));
							goto IL_0333;
						case SqlDbType.Image:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0333;
						case SqlDbType.Int:
							ValueUtilsSmi.SetInt32_Unchecked(sink, setters, i, record.GetInt32(i));
							goto IL_0333;
						case SqlDbType.Money:
							ValueUtilsSmi.SetSqlMoney_Unchecked(sink, setters, i, metaData[i], record.GetSqlMoney(i));
							goto IL_0333;
						case SqlDbType.NChar:
						case SqlDbType.NText:
						case SqlDbType.NVarChar:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0333;
						case SqlDbType.Real:
							ValueUtilsSmi.SetSingle_Unchecked(sink, setters, i, record.GetFloat(i));
							goto IL_0333;
						case SqlDbType.UniqueIdentifier:
							ValueUtilsSmi.SetGuid_Unchecked(sink, setters, i, record.GetGuid(i));
							goto IL_0333;
						case SqlDbType.SmallDateTime:
							ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], record.GetDateTime(i));
							goto IL_0333;
						case SqlDbType.SmallInt:
							ValueUtilsSmi.SetInt16_Unchecked(sink, setters, i, record.GetInt16(i));
							goto IL_0333;
						case SqlDbType.SmallMoney:
							ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], record.GetSqlMoney(i));
							goto IL_0333;
						case SqlDbType.Text:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0333;
						case SqlDbType.Timestamp:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0333;
						case SqlDbType.TinyInt:
							ValueUtilsSmi.SetByte_Unchecked(sink, setters, i, record.GetByte(i));
							goto IL_0333;
						case SqlDbType.VarBinary:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0333;
						case SqlDbType.VarChar:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0333;
						case SqlDbType.Variant:
						{
							object sqlValue = record.GetSqlValue(i);
							ExtendedClrTypeCode extendedClrTypeCode = MetaDataUtilsSmi.DetermineExtendedTypeCode(sqlValue);
							ValueUtilsSmi.SetCompatibleValueV200(sink, setters, i, metaData[i], sqlValue, extendedClrTypeCode, 0, -1, null);
							goto IL_0333;
						}
						case SqlDbType.Xml:
							ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, i, record.GetSqlXml(i));
							goto IL_0333;
						case SqlDbType.Udt:
							throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
						case SqlDbType.Date:
						case SqlDbType.DateTime2:
							ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], record.GetDateTime(i));
							goto IL_0333;
						case SqlDbType.Time:
						{
							TimeSpan timeSpan;
							if (record != null)
							{
								timeSpan = record.GetTimeSpan(i);
							}
							else
							{
								timeSpan = (TimeSpan)record.GetValue(i);
							}
							ValueUtilsSmi.SetTimeSpan_Checked(sink, setters, i, metaData[i], timeSpan);
							goto IL_0333;
						}
						case SqlDbType.DateTimeOffset:
						{
							DateTimeOffset dateTimeOffset;
							if (record != null)
							{
								dateTimeOffset = record.GetDateTimeOffset(i);
							}
							else
							{
								dateTimeOffset = (DateTimeOffset)record.GetValue(i);
							}
							ValueUtilsSmi.SetDateTimeOffset_Unchecked(sink, setters, i, dateTimeOffset);
							goto IL_0333;
						}
						}
						throw ADP.NotSupported();
					}
					ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, i);
				}
				IL_0333:;
			}
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x000C4698 File Offset: 0x000C2898
		internal static Stream CopyIntoNewSmiScratchStream(Stream source, SmiEventSink_Default sink)
		{
			Stream stream = new MemoryStream();
			int num;
			if (source.CanSeek && 8000L > source.Length)
			{
				num = (int)source.Length;
			}
			else
			{
				num = 8000;
			}
			byte[] array = new byte[num];
			int num2;
			while ((num2 = source.Read(array, 0, num)) != 0)
			{
				stream.Write(array, 0, num2);
			}
			stream.Flush();
			stream.Seek(0L, SeekOrigin.Begin);
			return stream;
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000C4704 File Offset: 0x000C2904
		private static decimal GetDecimal_PossiblyMoney(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			if (SqlDbType.Decimal == metaData.SqlDbType)
			{
				return ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal).Value;
			}
			return ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal).Value;
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000C473B File Offset: 0x000C293B
		private static void SetDecimal_PossiblyMoney(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, decimal value)
		{
			if (SqlDbType.Decimal == metaData.SqlDbType || SqlDbType.Variant == metaData.SqlDbType)
			{
				ValueUtilsSmi.SetDecimal_Unchecked(sink, setters, ordinal, value);
				return;
			}
			ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, ordinal, metaData, new SqlMoney(value));
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000C476B File Offset: 0x000C296B
		private static void VerifyDateTimeRange(SqlDbType dbType, DateTime value)
		{
			if (SqlDbType.SmallDateTime == dbType && (ValueUtilsSmi.s_dtSmallMax < value || ValueUtilsSmi.s_dtSmallMin > value))
			{
				throw ADP.InvalidMetaDataValue();
			}
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x000C4792 File Offset: 0x000C2992
		private static void VerifyTimeRange(SqlDbType dbType, TimeSpan value)
		{
			if (SqlDbType.Time == dbType && (ValueUtilsSmi.s_timeMin > value || value > ValueUtilsSmi.s_timeMax))
			{
				throw ADP.InvalidMetaDataValue();
			}
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x000C47B9 File Offset: 0x000C29B9
		private static void SetDateTime_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			ValueUtilsSmi.VerifyDateTimeRange(metaData.SqlDbType, value);
			ValueUtilsSmi.SetDateTime_Unchecked(sink, setters, ordinal, (SqlDbType.Date == metaData.SqlDbType) ? value.Date : value);
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x000C47E5 File Offset: 0x000C29E5
		private static void SetTimeSpan_Checked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, TimeSpan value)
		{
			ValueUtilsSmi.VerifyTimeRange(metaData.SqlDbType, value);
			ValueUtilsSmi.SetTimeSpan_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x000C47FE File Offset: 0x000C29FE
		private static void SetSqlDateTime_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDateTime value)
		{
			if (!value.IsNull)
			{
				ValueUtilsSmi.VerifyDateTimeRange(metaData.SqlDbType, value.Value);
			}
			ValueUtilsSmi.SetSqlDateTime_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x000C4825 File Offset: 0x000C2A25
		private static void SetDateTime2_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			ValueUtilsSmi.VerifyDateTimeRange(metaData.SqlDbType, value);
			ValueUtilsSmi.SetDateTime2_Unchecked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x000C483F File Offset: 0x000C2A3F
		private static void SetDate_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			ValueUtilsSmi.VerifyDateTimeRange(metaData.SqlDbType, value);
			ValueUtilsSmi.SetDate_Unchecked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x000C485C File Offset: 0x000C2A5C
		private static void SetSqlMoney_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlMoney value)
		{
			if (!value.IsNull && SqlDbType.SmallMoney == metaData.SqlDbType)
			{
				decimal value2 = value.Value;
				if (TdsEnums.SQL_SMALL_MONEY_MIN > value2 || TdsEnums.SQL_SMALL_MONEY_MAX < value2)
				{
					throw SQL.MoneyOverflow(value2.ToString(CultureInfo.InvariantCulture));
				}
			}
			ValueUtilsSmi.SetSqlMoney_Unchecked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000C48BC File Offset: 0x000C2ABC
		private static void SetByteArray_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, byte[] buffer, int offset)
		{
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, buffer.Length, offset, buffer.Length - offset);
			ValueUtilsSmi.SetByteArray_Unchecked(sink, setters, ordinal, buffer, offset, num);
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x000C48FC File Offset: 0x000C2AFC
		private static void SetCharArray_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, char[] buffer, int offset)
		{
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, buffer.Length, offset, buffer.Length - offset);
			ValueUtilsSmi.SetCharArray_Unchecked(sink, setters, ordinal, buffer, offset, num);
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x000C493C File Offset: 0x000C2B3C
		private static void SetSqlBinary_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBinary value, int offset)
		{
			int num = 0;
			if (!value.IsNull)
			{
				num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, value.Length, offset, value.Length - offset);
			}
			ValueUtilsSmi.SetSqlBinary_Unchecked(sink, setters, ordinal, value, offset, num);
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x000C498C File Offset: 0x000C2B8C
		private static void SetBytes_FromRecord(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDataRecord record, int offset)
		{
			long num = record.GetBytes(ordinal, 0L, null, 0, 0);
			if (num > 2147483647L)
			{
				num = -1L;
			}
			int num2 = checked(ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, (int)num, offset, (int)num));
			int num3;
			if (num2 > 8000 || num2 < 0)
			{
				num3 = 8000;
			}
			else
			{
				num3 = num2;
			}
			byte[] array = new byte[num3];
			long num4 = 1L;
			long num5 = (long)offset;
			long num6 = 0L;
			long bytes;
			while ((num2 < 0 || num6 < (long)num2) && (bytes = record.GetBytes(ordinal, num5, array, 0, num3)) != 0L && num4 != 0L)
			{
				num4 = (long)setters.SetBytes(sink, ordinal, num5, array, 0, checked((int)bytes));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num5 += num4;
					num6 += num4;
				}
			}
			setters.SetBytesLength(sink, ordinal, num5);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x000C4A54 File Offset: 0x000C2C54
		private static void SetBytes_FromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader reader, int offset)
		{
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, -1, offset, -1);
			int num2 = 8000;
			byte[] array = new byte[num2];
			long num3 = 1L;
			long num4 = (long)offset;
			long num5 = 0L;
			long bytes;
			while ((num < 0 || num5 < (long)num) && (bytes = reader.GetBytes(ordinal, num4, array, 0, num2)) != 0L && num3 != 0L)
			{
				num3 = (long)setters.SetBytes(sink, ordinal, num4, array, 0, checked((int)bytes));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num4 += num3;
					num5 += num3;
				}
			}
			setters.SetBytesLength(sink, ordinal, num4);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x000C4AF0 File Offset: 0x000C2CF0
		private static void SetSqlBytes_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBytes value, int offset)
		{
			int num = 0;
			if (!value.IsNull)
			{
				long num2 = value.Length;
				if (num2 > 2147483647L)
				{
					num2 = -1L;
				}
				num = checked(ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, (int)num2, offset, (int)num2));
			}
			ValueUtilsSmi.SetSqlBytes_Unchecked(sink, setters, ordinal, value, 0, (long)num);
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000C4B48 File Offset: 0x000C2D48
		private static void SetChars_FromRecord(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDataRecord record, int offset)
		{
			long num = record.GetChars(ordinal, 0L, null, 0, 0);
			if (num > 2147483647L)
			{
				num = -1L;
			}
			int num2 = checked(ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, (int)num, offset, (int)num - offset));
			int num3;
			if (num2 > 4000 || num2 < 0)
			{
				if (MetaDataUtilsSmi.IsAnsiType(metaData.SqlDbType))
				{
					num3 = 8000;
				}
				else
				{
					num3 = 4000;
				}
			}
			else
			{
				num3 = num2;
			}
			char[] array = new char[num3];
			long num4 = 1L;
			long num5 = (long)offset;
			long num6 = 0L;
			long chars;
			while ((num2 < 0 || num6 < (long)num2) && (chars = record.GetChars(ordinal, num5, array, 0, num3)) != 0L && num4 != 0L)
			{
				num4 = (long)setters.SetChars(sink, ordinal, num5, array, 0, checked((int)chars));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num5 += num4;
					num6 += num4;
				}
			}
			setters.SetCharsLength(sink, ordinal, num5);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000C4C28 File Offset: 0x000C2E28
		private static void SetCharsOrString_FromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader reader, int offset)
		{
			bool flag = false;
			try
			{
				ValueUtilsSmi.SetChars_FromReader(sink, setters, ordinal, metaData, reader, offset);
				flag = true;
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
			}
			if (!flag)
			{
				ValueUtilsSmi.SetString_FromReader(sink, setters, ordinal, metaData, reader, offset);
			}
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x000C4C74 File Offset: 0x000C2E74
		private static void SetChars_FromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader reader, int offset)
		{
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, -1, offset, -1);
			int num2;
			if (MetaDataUtilsSmi.IsAnsiType(metaData.SqlDbType))
			{
				num2 = 8000;
			}
			else
			{
				num2 = 4000;
			}
			char[] array = new char[num2];
			long num3 = 1L;
			long num4 = (long)offset;
			long num5 = 0L;
			long chars;
			while ((num < 0 || num5 < (long)num) && (chars = reader.GetChars(ordinal, num4, array, 0, num2)) != 0L && num3 != 0L)
			{
				num3 = (long)setters.SetChars(sink, ordinal, num4, array, 0, checked((int)chars));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num4 += num3;
					num5 += num3;
				}
			}
			setters.SetCharsLength(sink, ordinal, num4);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x000C4D24 File Offset: 0x000C2F24
		private static void SetString_FromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader reader, int offset)
		{
			string @string = reader.GetString(ordinal);
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, (long)@string.Length, 0L, -1, offset, -1);
			setters.SetString(sink, ordinal, @string, offset, num);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000C4D6C File Offset: 0x000C2F6C
		private static void SetSqlChars_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlChars value, int offset)
		{
			int num = 0;
			if (!value.IsNull)
			{
				long num2 = value.Length;
				if (num2 > 2147483647L)
				{
					num2 = -1L;
				}
				num = checked(ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, (int)num2, offset, (int)num2 - offset));
			}
			ValueUtilsSmi.SetSqlChars_Unchecked(sink, setters, ordinal, value, 0, num);
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x000C4DC4 File Offset: 0x000C2FC4
		private static void SetSqlString_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlString value, int offset)
		{
			if (value.IsNull)
			{
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, ordinal);
				return;
			}
			string value2 = value.Value;
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, value2.Length, offset, value2.Length - offset);
			ValueUtilsSmi.SetSqlString_Unchecked(sink, setters, ordinal, metaData, value, offset, num);
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000C4E20 File Offset: 0x000C3020
		private static void SetString_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, string value, int offset)
		{
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, value.Length, offset, checked(value.Length - offset));
			ValueUtilsSmi.SetString_Unchecked(sink, setters, ordinal, value, offset, num);
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x000C4E63 File Offset: 0x000C3063
		private static void ThrowIfInvalidSetterAccess(SmiMetaData metaData, ExtendedClrTypeCode setterTypeCode)
		{
			if (!ValueUtilsSmi.CanAccessSetterDirectly(metaData, setterTypeCode))
			{
				throw ADP.InvalidCast();
			}
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000C4E74 File Offset: 0x000C3074
		private static void ThrowIfITypedGettersIsNull(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				throw SQL.SqlNullValue();
			}
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000C4E88 File Offset: 0x000C3088
		private static bool CanAccessGetterDirectly(SmiMetaData metaData, ExtendedClrTypeCode setterTypeCode)
		{
			bool flag = ValueUtilsSmi.s_canAccessGetterDirectly[(int)setterTypeCode, (int)metaData.SqlDbType];
			if (flag && (ExtendedClrTypeCode.DataTable == setterTypeCode || ExtendedClrTypeCode.DbDataReader == setterTypeCode || ExtendedClrTypeCode.IEnumerableOfSqlDataRecord == setterTypeCode))
			{
				flag = metaData.IsMultiValued;
			}
			return flag;
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000C4EC4 File Offset: 0x000C30C4
		private static bool CanAccessSetterDirectly(SmiMetaData metaData, ExtendedClrTypeCode setterTypeCode)
		{
			bool flag = ValueUtilsSmi.s_canAccessSetterDirectly[(int)setterTypeCode, (int)metaData.SqlDbType];
			if (flag && (ExtendedClrTypeCode.DataTable == setterTypeCode || ExtendedClrTypeCode.DbDataReader == setterTypeCode || ExtendedClrTypeCode.IEnumerableOfSqlDataRecord == setterTypeCode))
			{
				flag = metaData.IsMultiValued;
			}
			return flag;
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000C4EFD File Offset: 0x000C30FD
		private static long PositiveMin(long first, long second)
		{
			if (first < 0L)
			{
				return second;
			}
			if (second < 0L)
			{
				return first;
			}
			return Math.Min(first, second);
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000C4F14 File Offset: 0x000C3114
		private static int CheckXetParameters(SqlDbType dbType, long maxLength, long actualLength, long fieldOffset, int bufferLength, int bufferOffset, int length)
		{
			if (0L > fieldOffset)
			{
				throw ADP.NegativeParameter("fieldOffset");
			}
			if (bufferOffset < 0)
			{
				throw ADP.InvalidDestinationBufferIndex(bufferLength, bufferOffset, "bufferOffset");
			}
			checked
			{
				if (bufferLength < 0)
				{
					length = (int)ValueUtilsSmi.PositiveMin(unchecked((long)length), ValueUtilsSmi.PositiveMin(maxLength, actualLength));
					if (length < -1)
					{
						length = -1;
					}
					return length;
				}
				if (bufferOffset > bufferLength)
				{
					throw ADP.InvalidDestinationBufferIndex(bufferLength, bufferOffset, "bufferOffset");
				}
				if (length + bufferOffset > bufferLength)
				{
					throw ADP.InvalidBufferSizeOrIndex(length, bufferOffset);
				}
			}
			if (length < 0)
			{
				throw ADP.InvalidDataLength((long)length);
			}
			if (0L <= actualLength && actualLength <= fieldOffset)
			{
				return 0;
			}
			length = Math.Min(length, bufferLength - bufferOffset);
			if (SqlDbType.Variant == dbType)
			{
				length = Math.Min(length, 8000);
			}
			if (0L <= actualLength)
			{
				length = (int)Math.Min((long)length, actualLength - fieldOffset);
			}
			else if (SqlDbType.Udt != dbType && 0L <= maxLength)
			{
				length = (int)Math.Min((long)length, maxLength - fieldOffset);
			}
			if (length < 0)
			{
				return 0;
			}
			return length;
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000C4FFF File Offset: 0x000C31FF
		private static bool IsDBNull_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			bool flag = getters.IsDBNull(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return flag;
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x000C500F File Offset: 0x000C320F
		private static bool GetBoolean_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			bool boolean = getters.GetBoolean(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return boolean;
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000C501F File Offset: 0x000C321F
		private static byte GetByte_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			byte @byte = getters.GetByte(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @byte;
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000C5030 File Offset: 0x000C3230
		private static byte[] GetByteArray_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			int bytesLength = (int)getters.GetBytesLength(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			int num = bytesLength;
			byte[] array = new byte[num];
			getters.GetBytes(sink, ordinal, 0L, array, 0, num);
			sink.ProcessMessagesAndThrow();
			return array;
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000C5069 File Offset: 0x000C3269
		internal static int GetBytes_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int bytes = getters.GetBytes(sink, ordinal, fieldOffset, buffer, bufferOffset, length);
			sink.ProcessMessagesAndThrow();
			return bytes;
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000C5080 File Offset: 0x000C3280
		private static long GetBytesLength_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long bytesLength = getters.GetBytesLength(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return bytesLength;
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000C5090 File Offset: 0x000C3290
		private static char[] GetCharArray_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			int charsLength = (int)getters.GetCharsLength(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			int num = charsLength;
			char[] array = new char[num];
			getters.GetChars(sink, ordinal, 0L, array, 0, num);
			sink.ProcessMessagesAndThrow();
			return array;
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000C50C9 File Offset: 0x000C32C9
		internal static int GetChars_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int chars = getters.GetChars(sink, ordinal, fieldOffset, buffer, bufferOffset, length);
			sink.ProcessMessagesAndThrow();
			return chars;
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x000C50E0 File Offset: 0x000C32E0
		private static long GetCharsLength_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long charsLength = getters.GetCharsLength(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return charsLength;
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x000C50F0 File Offset: 0x000C32F0
		private static DateTime GetDateTime_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			DateTime dateTime = getters.GetDateTime(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return dateTime;
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000C5100 File Offset: 0x000C3300
		private static DateTimeOffset GetDateTimeOffset_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal)
		{
			DateTimeOffset dateTimeOffset = getters.GetDateTimeOffset(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return dateTimeOffset;
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x000C5110 File Offset: 0x000C3310
		private static double GetDouble_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			double @double = getters.GetDouble(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @double;
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x000C5120 File Offset: 0x000C3320
		private static Guid GetGuid_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			Guid guid = getters.GetGuid(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return guid;
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000C5130 File Offset: 0x000C3330
		private static short GetInt16_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			short @int = getters.GetInt16(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @int;
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000C5140 File Offset: 0x000C3340
		private static int GetInt32_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			int @int = getters.GetInt32(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @int;
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x000C5150 File Offset: 0x000C3350
		private static long GetInt64_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long @int = getters.GetInt64(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @int;
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x000C5160 File Offset: 0x000C3360
		private static float GetSingle_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			float single = getters.GetSingle(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return single;
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x000C5170 File Offset: 0x000C3370
		private static SqlBinary GetSqlBinary_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			return new SqlBinary(ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal));
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000C517F File Offset: 0x000C337F
		private static SqlDecimal GetSqlDecimal_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			SqlDecimal sqlDecimal = getters.GetSqlDecimal(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return sqlDecimal;
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000C518F File Offset: 0x000C338F
		private static SqlMoney GetSqlMoney_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long @int = getters.GetInt64(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return SqlTypeWorkarounds.SqlMoneyCtor(@int, 1);
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000C51A5 File Offset: 0x000C33A5
		private static SqlXml GetSqlXml_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			return new SqlXml(ValueUtilsSmi.CopyIntoNewSmiScratchStream(new SmiGettersStream(sink, getters, ordinal, SmiMetaData.DefaultXml), sink));
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000C51BF File Offset: 0x000C33BF
		private static string GetString_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			string @string = getters.GetString(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @string;
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000C51CF File Offset: 0x000C33CF
		private static TimeSpan GetTimeSpan_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal)
		{
			TimeSpan timeSpan = getters.GetTimeSpan(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return timeSpan;
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000C51DF File Offset: 0x000C33DF
		private static void SetBoolean_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, bool value)
		{
			setters.SetBoolean(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000C51F0 File Offset: 0x000C33F0
		private static void SetByteArray_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, byte[] buffer, int bufferOffset, int length)
		{
			if (length > 0)
			{
				setters.SetBytes(sink, ordinal, 0L, buffer, bufferOffset, length);
				sink.ProcessMessagesAndThrow();
			}
			setters.SetBytesLength(sink, ordinal, (long)length);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000C5220 File Offset: 0x000C3420
		private static void SetStream_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metadata, StreamDataFeed feed)
		{
			long maxLength = metadata.MaxLength;
			byte[] array = new byte[4096];
			int num = 0;
			do
			{
				int num2 = 4096;
				if (maxLength > 0L && (long)(num + num2) > maxLength)
				{
					num2 = (int)(maxLength - (long)num);
				}
				int num3 = feed._source.Read(array, 0, num2);
				if (num3 == 0)
				{
					break;
				}
				setters.SetBytes(sink, ordinal, (long)num, array, 0, num3);
				sink.ProcessMessagesAndThrow();
				num += num3;
			}
			while (maxLength <= 0L || (long)num < maxLength);
			setters.SetBytesLength(sink, ordinal, (long)num);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000C52A4 File Offset: 0x000C34A4
		private static void SetTextReader_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metadata, TextDataFeed feed)
		{
			long maxLength = metadata.MaxLength;
			char[] array = new char[4096];
			int num = 0;
			do
			{
				int num2 = 4096;
				if (maxLength > 0L && (long)(num + num2) > maxLength)
				{
					num2 = (int)(maxLength - (long)num);
				}
				int num3 = feed._source.Read(array, 0, num2);
				if (num3 == 0)
				{
					break;
				}
				setters.SetChars(sink, ordinal, (long)num, array, 0, num3);
				sink.ProcessMessagesAndThrow();
				num += num3;
			}
			while (maxLength <= 0L || (long)num < maxLength);
			setters.SetCharsLength(sink, ordinal, (long)num);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000C5328 File Offset: 0x000C3528
		private static void SetByte_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, byte value)
		{
			setters.SetByte(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000C5339 File Offset: 0x000C3539
		private static int SetBytes_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int num = setters.SetBytes(sink, ordinal, fieldOffset, buffer, bufferOffset, length);
			sink.ProcessMessagesAndThrow();
			return num;
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000C5350 File Offset: 0x000C3550
		private static void SetCharArray_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, char[] buffer, int bufferOffset, int length)
		{
			if (length > 0)
			{
				setters.SetChars(sink, ordinal, 0L, buffer, bufferOffset, length);
				sink.ProcessMessagesAndThrow();
			}
			setters.SetCharsLength(sink, ordinal, (long)length);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x000C537E File Offset: 0x000C357E
		private static int SetChars_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int num = setters.SetChars(sink, ordinal, fieldOffset, buffer, bufferOffset, length);
			sink.ProcessMessagesAndThrow();
			return num;
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x000C5395 File Offset: 0x000C3595
		private static void SetDBNull_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal)
		{
			setters.SetDBNull(sink, ordinal);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x000C53A5 File Offset: 0x000C35A5
		private static void SetDecimal_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, decimal value)
		{
			setters.SetSqlDecimal(sink, ordinal, new SqlDecimal(value));
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x000C53BB File Offset: 0x000C35BB
		private static void SetDateTime_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, DateTime value)
		{
			setters.SetDateTime(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000C53CC File Offset: 0x000C35CC
		private static void SetDateTime2_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			setters.SetVariantMetaData(sink, ordinal, SmiMetaData.DefaultDateTime2);
			setters.SetDateTime(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000C53EB File Offset: 0x000C35EB
		private static void SetDate_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			setters.SetVariantMetaData(sink, ordinal, SmiMetaData.DefaultDate);
			setters.SetDateTime(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x000C540A File Offset: 0x000C360A
		private static void SetTimeSpan_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, TimeSpan value)
		{
			setters.SetTimeSpan(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x000C541B File Offset: 0x000C361B
		private static void SetDateTimeOffset_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, DateTimeOffset value)
		{
			setters.SetDateTimeOffset(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x000C542C File Offset: 0x000C362C
		private static void SetDouble_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, double value)
		{
			setters.SetDouble(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000C543D File Offset: 0x000C363D
		private static void SetGuid_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, Guid value)
		{
			setters.SetGuid(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x000C544E File Offset: 0x000C364E
		private static void SetInt16_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, short value)
		{
			setters.SetInt16(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x000C545F File Offset: 0x000C365F
		private static void SetInt32_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, int value)
		{
			setters.SetInt32(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x000C5470 File Offset: 0x000C3670
		private static void SetInt64_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, long value)
		{
			setters.SetInt64(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x000C5481 File Offset: 0x000C3681
		private static void SetSingle_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, float value)
		{
			setters.SetSingle(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x000C5492 File Offset: 0x000C3692
		private static void SetSqlBinary_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlBinary value, int offset, int length)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				ValueUtilsSmi.SetByteArray_Unchecked(sink, setters, ordinal, value.Value, offset, length);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x000C54C0 File Offset: 0x000C36C0
		private static void SetSqlBoolean_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlBoolean value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetBoolean(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000C54EA File Offset: 0x000C36EA
		private static void SetSqlByte_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlByte value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetByte(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x000C5514 File Offset: 0x000C3714
		private static void SetSqlBytes_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlBytes value, int offset, long length)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			int num;
			if (length > 8000L || length < 0L)
			{
				num = 8000;
			}
			else
			{
				num = checked((int)length);
			}
			byte[] array = new byte[num];
			long num2 = 1L;
			long num3 = (long)offset;
			long num4 = 0L;
			long num5;
			while ((length < 0L || num4 < length) && (num5 = value.Read(num3, array, 0, num)) != 0L && num2 != 0L)
			{
				num2 = (long)setters.SetBytes(sink, ordinal, num3, array, 0, checked((int)num5));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num3 += num2;
					num4 += num2;
				}
			}
			setters.SetBytesLength(sink, ordinal, num3);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x000C55BC File Offset: 0x000C37BC
		private static void SetSqlChars_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlChars value, int offset, int length)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			int num;
			if (length > 4000 || length < 0)
			{
				num = 4000;
			}
			else
			{
				num = length;
			}
			char[] array = new char[num];
			long num2 = 1L;
			long num3 = (long)offset;
			long num4 = 0L;
			long num5;
			while ((length < 0 || num4 < (long)length) && (num5 = value.Read(num3, array, 0, num)) != 0L && num2 != 0L)
			{
				num2 = (long)setters.SetChars(sink, ordinal, num3, array, 0, checked((int)num5));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num3 += num2;
					num4 += num2;
				}
			}
			setters.SetCharsLength(sink, ordinal, num3);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x000C565E File Offset: 0x000C385E
		private static void SetSqlDateTime_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlDateTime value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetDateTime(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x000C5688 File Offset: 0x000C3888
		private static void SetSqlDecimal_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlDecimal value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetSqlDecimal(sink, ordinal, value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000C56AC File Offset: 0x000C38AC
		private static void SetSqlDouble_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlDouble value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetDouble(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000C56D6 File Offset: 0x000C38D6
		private static void SetSqlGuid_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlGuid value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetGuid(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x000C5700 File Offset: 0x000C3900
		private static void SetSqlInt16_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlInt16 value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetInt16(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x000C572A File Offset: 0x000C392A
		private static void SetSqlInt32_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlInt32 value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetInt32(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000C5754 File Offset: 0x000C3954
		private static void SetSqlInt64_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlInt64 value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetInt64(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x000C5780 File Offset: 0x000C3980
		private static void SetSqlMoney_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlMoney value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				if (SqlDbType.Variant == metaData.SqlDbType)
				{
					setters.SetVariantMetaData(sink, ordinal, SmiMetaData.DefaultMoney);
					sink.ProcessMessagesAndThrow();
				}
				setters.SetInt64(sink, ordinal, SqlTypeWorkarounds.SqlMoneyToSqlInternalRepresentation(value));
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x000C57D2 File Offset: 0x000C39D2
		private static void SetSqlSingle_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlSingle value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetSingle(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x000C57FC File Offset: 0x000C39FC
		private static void SetSqlString_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlString value, int offset, int length)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			if (SqlDbType.Variant == metaData.SqlDbType)
			{
				metaData = new SmiMetaData(SqlDbType.NVarChar, 4000L, 0, 0, (long)value.LCID, value.SqlCompareOptions);
				setters.SetVariantMetaData(sink, ordinal, metaData);
				sink.ProcessMessagesAndThrow();
			}
			ValueUtilsSmi.SetString_Unchecked(sink, setters, ordinal, value.Value, offset, length);
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000C586D File Offset: 0x000C3A6D
		private static void SetSqlXml_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlXml value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			ValueUtilsSmi.SetXmlReader_Unchecked(sink, setters, ordinal, value.CreateReader());
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x000C5894 File Offset: 0x000C3A94
		private static void SetXmlReader_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, XmlReader xmlReader)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.CloseOutput = false;
			xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
			xmlWriterSettings.Encoding = Encoding.Unicode;
			xmlWriterSettings.OmitXmlDeclaration = true;
			XmlWriter xmlWriter = XmlWriter.Create(new SmiSettersStream(sink, setters, ordinal, SmiMetaData.DefaultXml), xmlWriterSettings);
			xmlReader.Read();
			while (!xmlReader.EOF)
			{
				xmlWriter.WriteNode(xmlReader, true);
			}
			xmlWriter.Flush();
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x000C5900 File Offset: 0x000C3B00
		private static void SetString_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, string value, int offset, int length)
		{
			setters.SetString(sink, ordinal, value, offset, length);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x000C5918 File Offset: 0x000C3B18
		private static void SetDbDataReader_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader value)
		{
			setters = setters.GetTypedGetterSetter(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			while (value.Read())
			{
				setters.NewElement(sink);
				sink.ProcessMessagesAndThrow();
				ValueUtilsSmi.FillCompatibleSettersFromReader(sink, setters, metaData.FieldMetaData, value);
			}
			setters.EndElements(sink);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x000C596C File Offset: 0x000C3B6C
		private static void SetIEnumerableOfSqlDataRecord_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, IEnumerable<SqlDataRecord> value, ParameterPeekAheadValue peekAhead)
		{
			setters = setters.GetTypedGetterSetter(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			IEnumerator<SqlDataRecord> enumerator = null;
			try
			{
				SmiExtendedMetaData[] array = new SmiExtendedMetaData[metaData.FieldMetaData.Count];
				metaData.FieldMetaData.CopyTo(array, 0);
				SmiDefaultFieldsProperty smiDefaultFieldsProperty = (SmiDefaultFieldsProperty)metaData.ExtendedProperties[SmiPropertySelector.DefaultFields];
				int num = 1;
				if (peekAhead != null && peekAhead.FirstRecord != null)
				{
					enumerator = peekAhead.Enumerator;
					setters.NewElement(sink);
					sink.ProcessMessagesAndThrow();
					ValueUtilsSmi.FillCompatibleSettersFromRecord(sink, setters, array, peekAhead.FirstRecord, smiDefaultFieldsProperty);
					num++;
				}
				else
				{
					enumerator = value.GetEnumerator();
				}
				using (enumerator)
				{
					while (enumerator.MoveNext())
					{
						setters.NewElement(sink);
						sink.ProcessMessagesAndThrow();
						SqlDataRecord sqlDataRecord = enumerator.Current;
						if (sqlDataRecord.FieldCount != array.Length)
						{
							throw SQL.EnumeratedRecordFieldCountChanged(num);
						}
						for (int i = 0; i < sqlDataRecord.FieldCount; i++)
						{
							if (!MetaDataUtilsSmi.IsCompatible(metaData.FieldMetaData[i], sqlDataRecord.GetSqlMetaData(i)))
							{
								throw SQL.EnumeratedRecordMetaDataChanged(sqlDataRecord.GetName(i), num);
							}
						}
						ValueUtilsSmi.FillCompatibleSettersFromRecord(sink, setters, array, sqlDataRecord, smiDefaultFieldsProperty);
						num++;
					}
					setters.EndElements(sink);
					sink.ProcessMessagesAndThrow();
				}
			}
			finally
			{
				IDisposable disposable = enumerator;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x04001AFD RID: 6909
		private const int __maxByteChunkSize = 8000;

		// Token: 0x04001AFE RID: 6910
		private const int __maxCharChunkSize = 4000;

		// Token: 0x04001AFF RID: 6911
		private const int NoLengthLimit = -1;

		// Token: 0x04001B00 RID: 6912
		private const int constBinBufferSize = 4096;

		// Token: 0x04001B01 RID: 6913
		private const int constTextBufferSize = 4096;

		// Token: 0x04001B02 RID: 6914
		private static object[] s_typeSpecificNullForSqlValue = new object[]
		{
			SqlInt64.Null,
			SqlBinary.Null,
			SqlBoolean.Null,
			SqlString.Null,
			SqlDateTime.Null,
			SqlDecimal.Null,
			SqlDouble.Null,
			SqlBinary.Null,
			SqlInt32.Null,
			SqlMoney.Null,
			SqlString.Null,
			SqlString.Null,
			SqlString.Null,
			SqlSingle.Null,
			SqlGuid.Null,
			SqlDateTime.Null,
			SqlInt16.Null,
			SqlMoney.Null,
			SqlString.Null,
			SqlBinary.Null,
			SqlByte.Null,
			SqlBinary.Null,
			SqlString.Null,
			DBNull.Value,
			null,
			SqlXml.Null,
			null,
			null,
			null,
			null,
			null,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value
		};

		// Token: 0x04001B03 RID: 6915
		private static readonly DateTime s_dtSmallMax = new DateTime(2079, 6, 6, 23, 59, 29, 998);

		// Token: 0x04001B04 RID: 6916
		private static readonly DateTime s_dtSmallMin = new DateTime(1899, 12, 31, 23, 59, 29, 999);

		// Token: 0x04001B05 RID: 6917
		private static readonly TimeSpan s_timeMin = TimeSpan.Zero;

		// Token: 0x04001B06 RID: 6918
		private static readonly TimeSpan s_timeMax = new TimeSpan(863999999999L);

		// Token: 0x04001B07 RID: 6919
		private const bool X = true;

		// Token: 0x04001B08 RID: 6920
		private const bool _ = false;

		// Token: 0x04001B09 RID: 6921
		private static bool[,] s_canAccessGetterDirectly = new bool[,]
		{
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, true, false, false, false, false, false,
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, true, false, false, false, true,
				false, false, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, true, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, true, false, true, false, false, false, true, false, false,
				true, true, true, false, false, false, false, false, true, true,
				false, true, true, false, false, true, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, true, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, true, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false, false, false, true,
				false, true, false, false, false, false, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, true, false, false, false, false, false,
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false
			},
			{
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, true, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, true, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, true,
				false, false, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, true, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false, false, false, true,
				false, true, false, false, false, false, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, true, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, true
			},
			{
				false, true, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			}
		};

		// Token: 0x04001B0A RID: 6922
		private static bool[,] s_canAccessSetterDirectly = new bool[,]
		{
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, true, false, false, false, false, false,
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, true, false, true, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, true, false, false, false, true,
				false, false, false, false, false, false, false, true, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, true, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, true, false, true, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, true, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false, false, false, true,
				false, true, false, true, false, true, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, true, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, true, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false, false, false, true,
				false, true, false, true, false, false, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, true, false, false, false, false, false,
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, true, false, true, false
			},
			{
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, true, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, true, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, true,
				false, false, false, false, false, false, false, true, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, true, false, true, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, true, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false, false, false, true,
				false, true, false, true, false, false, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, true, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, true
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			}
		};
	}
}
