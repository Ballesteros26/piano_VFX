using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000232 RID: 562
	internal class TdsValueSetter
	{
		// Token: 0x06001970 RID: 6512 RVA: 0x0008119E File Offset: 0x0007F39E
		internal TdsValueSetter(TdsParserStateObject stateObj, SmiMetaData md)
		{
			this._stateObj = stateObj;
			this._metaData = md;
			this._isPlp = MetaDataUtilsSmi.IsPlpFormat(md);
			this._plpUnknownSent = false;
			this._encoder = null;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x000811D0 File Offset: 0x0007F3D0
		internal void SetDBNull()
		{
			if (this._isPlp)
			{
				this._stateObj.Parser.WriteUnsignedLong(ulong.MaxValue, this._stateObj);
				return;
			}
			switch (this._metaData.SqlDbType)
			{
			case SqlDbType.BigInt:
			case SqlDbType.Bit:
			case SqlDbType.DateTime:
			case SqlDbType.Decimal:
			case SqlDbType.Float:
			case SqlDbType.Int:
			case SqlDbType.Money:
			case SqlDbType.Real:
			case SqlDbType.UniqueIdentifier:
			case SqlDbType.SmallDateTime:
			case SqlDbType.SmallInt:
			case SqlDbType.SmallMoney:
			case SqlDbType.TinyInt:
			case SqlDbType.Date:
			case SqlDbType.Time:
			case SqlDbType.DateTime2:
			case SqlDbType.DateTimeOffset:
				this._stateObj.WriteByte(0);
				return;
			case SqlDbType.Binary:
			case SqlDbType.Char:
			case SqlDbType.Image:
			case SqlDbType.NChar:
			case SqlDbType.NText:
			case SqlDbType.NVarChar:
			case SqlDbType.Text:
			case SqlDbType.Timestamp:
			case SqlDbType.VarBinary:
			case SqlDbType.VarChar:
				this._stateObj.Parser.WriteShort(65535, this._stateObj);
				return;
			case SqlDbType.Variant:
				this._stateObj.Parser.WriteInt(0, this._stateObj);
				break;
			case (SqlDbType)24:
			case SqlDbType.Xml:
			case (SqlDbType)26:
			case (SqlDbType)27:
			case (SqlDbType)28:
			case SqlDbType.Udt:
			case SqlDbType.Structured:
				break;
			default:
				return;
			}
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x000812E0 File Offset: 0x0007F4E0
		internal void SetBoolean(bool value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(3, 50, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			if (value)
			{
				this._stateObj.WriteByte(1);
				return;
			}
			this._stateObj.WriteByte(0);
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0008134C File Offset: 0x0007F54C
		internal void SetByte(byte value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(3, 48, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.WriteByte(value);
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x000813A7 File Offset: 0x0007F5A7
		internal int SetBytes(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.SetBytesNoOffsetHandling(fieldOffset, buffer, bufferOffset, length);
			return length;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x000813B8 File Offset: 0x0007F5B8
		private void SetBytesNoOffsetHandling(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (this._isPlp)
			{
				if (!this._plpUnknownSent)
				{
					this._stateObj.Parser.WriteUnsignedLong(18446744073709551614UL, this._stateObj);
					this._plpUnknownSent = true;
				}
				this._stateObj.Parser.WriteInt(length, this._stateObj);
				this._stateObj.WriteByteArray(buffer, length, bufferOffset, true, null);
				return;
			}
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(4 + length, 165, 2, this._stateObj);
			}
			this._stateObj.Parser.WriteShort(length, this._stateObj);
			this._stateObj.WriteByteArray(buffer, length, bufferOffset, true, null);
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0008147C File Offset: 0x0007F67C
		internal void SetBytesLength(long length)
		{
			if (length == 0L)
			{
				if (this._isPlp)
				{
					this._stateObj.Parser.WriteLong(0L, this._stateObj);
					this._plpUnknownSent = true;
				}
				else
				{
					if (SqlDbType.Variant == this._metaData.SqlDbType)
					{
						this._stateObj.Parser.WriteSqlVariantHeader(4, 165, 2, this._stateObj);
					}
					this._stateObj.Parser.WriteShort(0, this._stateObj);
				}
			}
			if (this._plpUnknownSent)
			{
				this._stateObj.Parser.WriteInt(0, this._stateObj);
				this._plpUnknownSent = false;
			}
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00081520 File Offset: 0x0007F720
		internal int SetChars(long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			if (MetaDataUtilsSmi.IsAnsiType(this._metaData.SqlDbType))
			{
				if (this._encoder == null)
				{
					this._encoder = this._stateObj.Parser._defaultEncoding.GetEncoder();
				}
				byte[] array = new byte[this._encoder.GetByteCount(buffer, bufferOffset, length, false)];
				this._encoder.GetBytes(buffer, bufferOffset, length, array, 0, false);
				this.SetBytesNoOffsetHandling(fieldOffset, array, 0, array.Length);
			}
			else if (this._isPlp)
			{
				if (!this._plpUnknownSent)
				{
					this._stateObj.Parser.WriteUnsignedLong(18446744073709551614UL, this._stateObj);
					this._plpUnknownSent = true;
				}
				this._stateObj.Parser.WriteInt(length * 2, this._stateObj);
				this._stateObj.Parser.WriteCharArray(buffer, length, bufferOffset, this._stateObj, true);
			}
			else if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantValue(new string(buffer, bufferOffset, length), length, 0, this._stateObj, true);
			}
			else
			{
				this._stateObj.Parser.WriteShort(length * 2, this._stateObj);
				this._stateObj.Parser.WriteCharArray(buffer, length, bufferOffset, this._stateObj, true);
			}
			return length;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00081674 File Offset: 0x0007F874
		internal void SetCharsLength(long length)
		{
			if (length == 0L)
			{
				if (this._isPlp)
				{
					this._stateObj.Parser.WriteLong(0L, this._stateObj);
					this._plpUnknownSent = true;
				}
				else
				{
					this._stateObj.Parser.WriteShort(0, this._stateObj);
				}
			}
			if (this._plpUnknownSent)
			{
				this._stateObj.Parser.WriteInt(0, this._stateObj);
				this._plpUnknownSent = false;
			}
			this._encoder = null;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x000816F4 File Offset: 0x0007F8F4
		internal void SetString(string value, int offset, int length)
		{
			if (MetaDataUtilsSmi.IsAnsiType(this._metaData.SqlDbType))
			{
				byte[] array;
				if (offset == 0 && value.Length <= length)
				{
					array = this._stateObj.Parser._defaultEncoding.GetBytes(value);
				}
				else
				{
					char[] array2 = value.ToCharArray(offset, length);
					array = this._stateObj.Parser._defaultEncoding.GetBytes(array2);
				}
				this.SetBytes(0L, array, 0, array.Length);
				this.SetBytesLength((long)array.Length);
				return;
			}
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				SqlCollation sqlCollation = new SqlCollation();
				sqlCollation.LCID = checked((int)this._variantType.LocaleId);
				sqlCollation.SqlCompareOptions = this._variantType.CompareOptions;
				if (length * 2 > 8000)
				{
					byte[] array3;
					if (offset == 0 && value.Length <= length)
					{
						array3 = this._stateObj.Parser._defaultEncoding.GetBytes(value);
					}
					else
					{
						array3 = this._stateObj.Parser._defaultEncoding.GetBytes(value.ToCharArray(offset, length));
					}
					this._stateObj.Parser.WriteSqlVariantHeader(9 + array3.Length, 167, 7, this._stateObj);
					this._stateObj.Parser.WriteUnsignedInt(sqlCollation.info, this._stateObj);
					this._stateObj.WriteByte(sqlCollation.sortId);
					this._stateObj.Parser.WriteShort(array3.Length, this._stateObj);
					this._stateObj.WriteByteArray(array3, array3.Length, 0, true, null);
				}
				else
				{
					this._stateObj.Parser.WriteSqlVariantHeader(9 + length * 2, 231, 7, this._stateObj);
					this._stateObj.Parser.WriteUnsignedInt(sqlCollation.info, this._stateObj);
					this._stateObj.WriteByte(sqlCollation.sortId);
					this._stateObj.Parser.WriteShort(length * 2, this._stateObj);
					this._stateObj.Parser.WriteString(value, length, offset, this._stateObj, true);
				}
				this._variantType = null;
				return;
			}
			if (this._isPlp)
			{
				this._stateObj.Parser.WriteLong((long)(length * 2), this._stateObj);
				this._stateObj.Parser.WriteInt(length * 2, this._stateObj);
				this._stateObj.Parser.WriteString(value, length, offset, this._stateObj, true);
				if (length != 0)
				{
					this._stateObj.Parser.WriteInt(0, this._stateObj);
					return;
				}
			}
			else
			{
				this._stateObj.Parser.WriteShort(length * 2, this._stateObj);
				this._stateObj.Parser.WriteString(value, length, offset, this._stateObj, true);
			}
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x000819AC File Offset: 0x0007FBAC
		internal void SetInt16(short value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(4, 52, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.Parser.WriteShort((int)value, this._stateObj);
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00081A14 File Offset: 0x0007FC14
		internal void SetInt32(int value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(6, 56, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.Parser.WriteInt(value, this._stateObj);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00081A7C File Offset: 0x0007FC7C
		internal void SetInt64(long value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				if (this._variantType == null)
				{
					this._stateObj.Parser.WriteSqlVariantHeader(10, 127, 0, this._stateObj);
					this._stateObj.Parser.WriteLong(value, this._stateObj);
					return;
				}
				this._stateObj.Parser.WriteSqlVariantHeader(10, 60, 0, this._stateObj);
				this._stateObj.Parser.WriteInt((int)(value >> 32), this._stateObj);
				this._stateObj.Parser.WriteInt((int)value, this._stateObj);
				this._variantType = null;
				return;
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
				if (SqlDbType.SmallMoney == this._metaData.SqlDbType)
				{
					this._stateObj.Parser.WriteInt((int)value, this._stateObj);
					return;
				}
				if (SqlDbType.Money == this._metaData.SqlDbType)
				{
					this._stateObj.Parser.WriteInt((int)(value >> 32), this._stateObj);
					this._stateObj.Parser.WriteInt((int)value, this._stateObj);
					return;
				}
				this._stateObj.Parser.WriteLong(value, this._stateObj);
				return;
			}
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x00081BC8 File Offset: 0x0007FDC8
		internal void SetSingle(float value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(6, 59, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.Parser.WriteFloat(value, this._stateObj);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00081C30 File Offset: 0x0007FE30
		internal void SetDouble(double value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(10, 62, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.Parser.WriteDouble(value, this._stateObj);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00081C98 File Offset: 0x0007FE98
		internal void SetSqlDecimal(SqlDecimal value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(21, 108, 2, this._stateObj);
				this._stateObj.WriteByte(value.Precision);
				this._stateObj.WriteByte(value.Scale);
				this._stateObj.Parser.WriteSqlDecimal(value, this._stateObj);
				return;
			}
			this._stateObj.WriteByte(checked((byte)MetaType.MetaDecimal.FixedLength));
			this._stateObj.Parser.WriteSqlDecimal(SqlDecimal.ConvertToPrecScale(value, (int)this._metaData.Precision, (int)this._metaData.Scale), this._stateObj);
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00081D54 File Offset: 0x0007FF54
		internal void SetDateTime(DateTime value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				if (this._variantType != null && this._variantType.SqlDbType == SqlDbType.DateTime2)
				{
					this._stateObj.Parser.WriteSqlVariantDateTime2(value, this._stateObj);
				}
				else if (this._variantType != null && this._variantType.SqlDbType == SqlDbType.Date)
				{
					this._stateObj.Parser.WriteSqlVariantDate(value, this._stateObj);
				}
				else
				{
					TdsDateTime tdsDateTime = MetaType.FromDateTime(value, 8);
					this._stateObj.Parser.WriteSqlVariantHeader(10, 61, 0, this._stateObj);
					this._stateObj.Parser.WriteInt(tdsDateTime.days, this._stateObj);
					this._stateObj.Parser.WriteInt(tdsDateTime.time, this._stateObj);
				}
				this._variantType = null;
				return;
			}
			this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			if (SqlDbType.SmallDateTime == this._metaData.SqlDbType)
			{
				TdsDateTime tdsDateTime2 = MetaType.FromDateTime(value, (byte)this._metaData.MaxLength);
				this._stateObj.Parser.WriteShort(tdsDateTime2.days, this._stateObj);
				this._stateObj.Parser.WriteShort(tdsDateTime2.time, this._stateObj);
				return;
			}
			if (SqlDbType.DateTime == this._metaData.SqlDbType)
			{
				TdsDateTime tdsDateTime3 = MetaType.FromDateTime(value, (byte)this._metaData.MaxLength);
				this._stateObj.Parser.WriteInt(tdsDateTime3.days, this._stateObj);
				this._stateObj.Parser.WriteInt(tdsDateTime3.time, this._stateObj);
				return;
			}
			int days = value.Subtract(DateTime.MinValue).Days;
			if (SqlDbType.DateTime2 == this._metaData.SqlDbType)
			{
				long num = value.TimeOfDay.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)this._metaData.Scale];
				this._stateObj.WriteByteArray(BitConverter.GetBytes(num), (int)this._metaData.MaxLength - 3, 0, true, null);
			}
			this._stateObj.WriteByteArray(BitConverter.GetBytes(days), 3, 0, true, null);
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00081F88 File Offset: 0x00080188
		internal void SetGuid(Guid value)
		{
			byte[] array = value.ToByteArray();
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(18, 36, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.WriteByteArray(array, array.Length, 0, true, null);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x00081FF4 File Offset: 0x000801F4
		internal void SetTimeSpan(TimeSpan value)
		{
			byte b;
			byte b2;
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				b = SmiMetaData.DefaultTime.Scale;
				b2 = (byte)SmiMetaData.DefaultTime.MaxLength;
				this._stateObj.Parser.WriteSqlVariantHeader(8, 41, 1, this._stateObj);
				this._stateObj.WriteByte(b);
			}
			else
			{
				b = this._metaData.Scale;
				b2 = (byte)this._metaData.MaxLength;
				this._stateObj.WriteByte(b2);
			}
			long num = value.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)b];
			this._stateObj.WriteByteArray(BitConverter.GetBytes(num), (int)b2, 0, true, null);
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0008209C File Offset: 0x0008029C
		internal void SetDateTimeOffset(DateTimeOffset value)
		{
			byte b;
			byte b2;
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				SmiMetaData defaultDateTimeOffset = SmiMetaData.DefaultDateTimeOffset;
				b = MetaType.MetaDateTimeOffset.Scale;
				b2 = (byte)defaultDateTimeOffset.MaxLength;
				this._stateObj.Parser.WriteSqlVariantHeader(13, 43, 1, this._stateObj);
				this._stateObj.WriteByte(b);
			}
			else
			{
				b = this._metaData.Scale;
				b2 = (byte)this._metaData.MaxLength;
				this._stateObj.WriteByte(b2);
			}
			DateTime utcDateTime = value.UtcDateTime;
			long num = utcDateTime.TimeOfDay.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)b];
			int days = utcDateTime.Subtract(DateTime.MinValue).Days;
			short num2 = (short)value.Offset.TotalMinutes;
			this._stateObj.WriteByteArray(BitConverter.GetBytes(num), (int)(b2 - 5), 0, true, null);
			this._stateObj.WriteByteArray(BitConverter.GetBytes(days), 3, 0, true, null);
			this._stateObj.WriteByte((byte)(num2 & 255));
			this._stateObj.WriteByte((byte)((num2 >> 8) & 255));
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x000821C1 File Offset: 0x000803C1
		internal void SetVariantType(SmiMetaData value)
		{
			this._variantType = value;
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00005E03 File Offset: 0x00004003
		[Conditional("DEBUG")]
		private void CheckSettingOffset(long offset)
		{
		}

		// Token: 0x0400122D RID: 4653
		private TdsParserStateObject _stateObj;

		// Token: 0x0400122E RID: 4654
		private SmiMetaData _metaData;

		// Token: 0x0400122F RID: 4655
		private bool _isPlp;

		// Token: 0x04001230 RID: 4656
		private bool _plpUnknownSent;

		// Token: 0x04001231 RID: 4657
		private Encoder _encoder;

		// Token: 0x04001232 RID: 4658
		private SmiMetaData _variantType;
	}
}
