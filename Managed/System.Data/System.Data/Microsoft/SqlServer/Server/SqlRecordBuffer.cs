using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003AE RID: 942
	internal sealed class SqlRecordBuffer
	{
		// Token: 0x06002CBC RID: 11452 RVA: 0x000C18A3 File Offset: 0x000BFAA3
		internal SqlRecordBuffer(SmiMetaData metaData)
		{
			this._isNull = true;
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002CBD RID: 11453 RVA: 0x000C18B2 File Offset: 0x000BFAB2
		internal bool IsNull
		{
			get
			{
				return this._isNull;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x000C18BA File Offset: 0x000BFABA
		// (set) Token: 0x06002CBF RID: 11455 RVA: 0x000C18C7 File Offset: 0x000BFAC7
		internal bool Boolean
		{
			get
			{
				return this._value._boolean;
			}
			set
			{
				this._value._boolean = value;
				this._type = SqlRecordBuffer.StorageType.Boolean;
				this._isNull = false;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x000C18E3 File Offset: 0x000BFAE3
		// (set) Token: 0x06002CC1 RID: 11457 RVA: 0x000C18F0 File Offset: 0x000BFAF0
		internal byte Byte
		{
			get
			{
				return this._value._byte;
			}
			set
			{
				this._value._byte = value;
				this._type = SqlRecordBuffer.StorageType.Byte;
				this._isNull = false;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002CC2 RID: 11458 RVA: 0x000C190C File Offset: 0x000BFB0C
		// (set) Token: 0x06002CC3 RID: 11459 RVA: 0x000C1919 File Offset: 0x000BFB19
		internal DateTime DateTime
		{
			get
			{
				return this._value._dateTime;
			}
			set
			{
				this._value._dateTime = value;
				this._type = SqlRecordBuffer.StorageType.DateTime;
				this._isNull = false;
				if (this._isMetaSet)
				{
					this._isMetaSet = false;
					return;
				}
				this._metadata = null;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x000C194C File Offset: 0x000BFB4C
		// (set) Token: 0x06002CC5 RID: 11461 RVA: 0x000C1959 File Offset: 0x000BFB59
		internal DateTimeOffset DateTimeOffset
		{
			get
			{
				return this._value._dateTimeOffset;
			}
			set
			{
				this._value._dateTimeOffset = value;
				this._type = SqlRecordBuffer.StorageType.DateTimeOffset;
				this._isNull = false;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002CC6 RID: 11462 RVA: 0x000C1975 File Offset: 0x000BFB75
		// (set) Token: 0x06002CC7 RID: 11463 RVA: 0x000C1982 File Offset: 0x000BFB82
		internal double Double
		{
			get
			{
				return this._value._double;
			}
			set
			{
				this._value._double = value;
				this._type = SqlRecordBuffer.StorageType.Double;
				this._isNull = false;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002CC8 RID: 11464 RVA: 0x000C199E File Offset: 0x000BFB9E
		// (set) Token: 0x06002CC9 RID: 11465 RVA: 0x000C19AB File Offset: 0x000BFBAB
		internal Guid Guid
		{
			get
			{
				return this._value._guid;
			}
			set
			{
				this._value._guid = value;
				this._type = SqlRecordBuffer.StorageType.Guid;
				this._isNull = false;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x000C19C7 File Offset: 0x000BFBC7
		// (set) Token: 0x06002CCB RID: 11467 RVA: 0x000C19D4 File Offset: 0x000BFBD4
		internal short Int16
		{
			get
			{
				return this._value._int16;
			}
			set
			{
				this._value._int16 = value;
				this._type = SqlRecordBuffer.StorageType.Int16;
				this._isNull = false;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002CCC RID: 11468 RVA: 0x000C19F0 File Offset: 0x000BFBF0
		// (set) Token: 0x06002CCD RID: 11469 RVA: 0x000C19FD File Offset: 0x000BFBFD
		internal int Int32
		{
			get
			{
				return this._value._int32;
			}
			set
			{
				this._value._int32 = value;
				this._type = SqlRecordBuffer.StorageType.Int32;
				this._isNull = false;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06002CCE RID: 11470 RVA: 0x000C1A1A File Offset: 0x000BFC1A
		// (set) Token: 0x06002CCF RID: 11471 RVA: 0x000C1A27 File Offset: 0x000BFC27
		internal long Int64
		{
			get
			{
				return this._value._int64;
			}
			set
			{
				this._value._int64 = value;
				this._type = SqlRecordBuffer.StorageType.Int64;
				this._isNull = false;
				if (this._isMetaSet)
				{
					this._isMetaSet = false;
					return;
				}
				this._metadata = null;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002CD0 RID: 11472 RVA: 0x000C1A5B File Offset: 0x000BFC5B
		// (set) Token: 0x06002CD1 RID: 11473 RVA: 0x000C1A68 File Offset: 0x000BFC68
		internal float Single
		{
			get
			{
				return this._value._single;
			}
			set
			{
				this._value._single = value;
				this._type = SqlRecordBuffer.StorageType.Single;
				this._isNull = false;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x000C1A88 File Offset: 0x000BFC88
		// (set) Token: 0x06002CD3 RID: 11475 RVA: 0x000C1AE8 File Offset: 0x000BFCE8
		internal string String
		{
			get
			{
				if (SqlRecordBuffer.StorageType.String == this._type)
				{
					return (string)this._object;
				}
				if (SqlRecordBuffer.StorageType.CharArray == this._type)
				{
					return new string((char[])this._object, 0, (int)this.CharsLength);
				}
				return new SqlXml(new MemoryStream((byte[])this._object, false)).Value;
			}
			set
			{
				this._object = value;
				this._value._int64 = (long)value.Length;
				this._type = SqlRecordBuffer.StorageType.String;
				this._isNull = false;
				if (this._isMetaSet)
				{
					this._isMetaSet = false;
					return;
				}
				this._metadata = null;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002CD4 RID: 11476 RVA: 0x000C1B34 File Offset: 0x000BFD34
		// (set) Token: 0x06002CD5 RID: 11477 RVA: 0x000C1B41 File Offset: 0x000BFD41
		internal SqlDecimal SqlDecimal
		{
			get
			{
				return (SqlDecimal)this._object;
			}
			set
			{
				this._object = value;
				this._type = SqlRecordBuffer.StorageType.SqlDecimal;
				this._isNull = false;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x000C1B5E File Offset: 0x000BFD5E
		// (set) Token: 0x06002CD7 RID: 11479 RVA: 0x000C1B6B File Offset: 0x000BFD6B
		internal TimeSpan TimeSpan
		{
			get
			{
				return this._value._timeSpan;
			}
			set
			{
				this._value._timeSpan = value;
				this._type = SqlRecordBuffer.StorageType.TimeSpan;
				this._isNull = false;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x000C1B88 File Offset: 0x000BFD88
		// (set) Token: 0x06002CD9 RID: 11481 RVA: 0x000C1BA5 File Offset: 0x000BFDA5
		internal long BytesLength
		{
			get
			{
				if (SqlRecordBuffer.StorageType.String == this._type)
				{
					this.ConvertXmlStringToByteArray();
				}
				return this._value._int64;
			}
			set
			{
				if (value == 0L)
				{
					this._value._int64 = value;
					this._object = Array.Empty<byte>();
					this._type = SqlRecordBuffer.StorageType.ByteArray;
					this._isNull = false;
					return;
				}
				this._value._int64 = value;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002CDA RID: 11482 RVA: 0x000C1A1A File Offset: 0x000BFC1A
		// (set) Token: 0x06002CDB RID: 11483 RVA: 0x000C1BDC File Offset: 0x000BFDDC
		internal long CharsLength
		{
			get
			{
				return this._value._int64;
			}
			set
			{
				if (value == 0L)
				{
					this._value._int64 = value;
					this._object = Array.Empty<char>();
					this._type = SqlRecordBuffer.StorageType.CharArray;
					this._isNull = false;
					return;
				}
				this._value._int64 = value;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002CDC RID: 11484 RVA: 0x000C1C14 File Offset: 0x000BFE14
		// (set) Token: 0x06002CDD RID: 11485 RVA: 0x000C1D15 File Offset: 0x000BFF15
		internal SmiMetaData VariantType
		{
			get
			{
				switch (this._type)
				{
				case SqlRecordBuffer.StorageType.Boolean:
					return SmiMetaData.DefaultBit;
				case SqlRecordBuffer.StorageType.Byte:
					return SmiMetaData.DefaultTinyInt;
				case SqlRecordBuffer.StorageType.ByteArray:
					return SmiMetaData.DefaultVarBinary;
				case SqlRecordBuffer.StorageType.CharArray:
					return SmiMetaData.DefaultNVarChar;
				case SqlRecordBuffer.StorageType.DateTime:
					return this._metadata ?? SmiMetaData.DefaultDateTime;
				case SqlRecordBuffer.StorageType.DateTimeOffset:
					return SmiMetaData.DefaultDateTimeOffset;
				case SqlRecordBuffer.StorageType.Double:
					return SmiMetaData.DefaultFloat;
				case SqlRecordBuffer.StorageType.Guid:
					return SmiMetaData.DefaultUniqueIdentifier;
				case SqlRecordBuffer.StorageType.Int16:
					return SmiMetaData.DefaultSmallInt;
				case SqlRecordBuffer.StorageType.Int32:
					return SmiMetaData.DefaultInt;
				case SqlRecordBuffer.StorageType.Int64:
					return this._metadata ?? SmiMetaData.DefaultBigInt;
				case SqlRecordBuffer.StorageType.Single:
					return SmiMetaData.DefaultReal;
				case SqlRecordBuffer.StorageType.String:
					return this._metadata ?? SmiMetaData.DefaultNVarChar;
				case SqlRecordBuffer.StorageType.SqlDecimal:
					return new SmiMetaData(SqlDbType.Decimal, 17L, ((SqlDecimal)this._object).Precision, ((SqlDecimal)this._object).Scale, 0L, SqlCompareOptions.None);
				case SqlRecordBuffer.StorageType.TimeSpan:
					return SmiMetaData.DefaultTime;
				default:
					return null;
				}
			}
			set
			{
				this._metadata = value;
				this._isMetaSet = true;
			}
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000C1D28 File Offset: 0x000BFF28
		internal int GetBytes(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int num = (int)fieldOffset;
			if (SqlRecordBuffer.StorageType.String == this._type)
			{
				this.ConvertXmlStringToByteArray();
			}
			Buffer.BlockCopy((byte[])this._object, num, buffer, bufferOffset, length);
			return length;
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x000C1D60 File Offset: 0x000BFF60
		internal int GetChars(long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int num = (int)fieldOffset;
			if (SqlRecordBuffer.StorageType.CharArray == this._type)
			{
				Array.Copy((char[])this._object, num, buffer, bufferOffset, length);
			}
			else
			{
				((string)this._object).CopyTo(num, buffer, bufferOffset, length);
			}
			return length;
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x000C1DA8 File Offset: 0x000BFFA8
		internal int SetBytes(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int num = (int)fieldOffset;
			if (this.IsNull || SqlRecordBuffer.StorageType.ByteArray != this._type)
			{
				if (num != 0)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				this._object = new byte[length];
				this._type = SqlRecordBuffer.StorageType.ByteArray;
				this._isNull = false;
				this.BytesLength = (long)length;
			}
			else
			{
				if ((long)num > this.BytesLength)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				if ((long)(num + length) > this.BytesLength)
				{
					int num2 = ((byte[])this._object).Length;
					if (num + length > num2)
					{
						byte[] array = new byte[Math.Max(num + length, 2 * num2)];
						Buffer.BlockCopy((byte[])this._object, 0, array, 0, (int)this.BytesLength);
						this._object = array;
					}
					this.BytesLength = (long)(num + length);
				}
			}
			Buffer.BlockCopy(buffer, bufferOffset, (byte[])this._object, num, length);
			return length;
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x000C1E8C File Offset: 0x000C008C
		internal int SetChars(long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int num = (int)fieldOffset;
			if (this.IsNull || (SqlRecordBuffer.StorageType.CharArray != this._type && SqlRecordBuffer.StorageType.String != this._type))
			{
				if (num != 0)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				this._object = new char[length];
				this._type = SqlRecordBuffer.StorageType.CharArray;
				this._isNull = false;
				this.CharsLength = (long)length;
			}
			else
			{
				if ((long)num > this.CharsLength)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				if (SqlRecordBuffer.StorageType.String == this._type)
				{
					this._object = ((string)this._object).ToCharArray();
					this._type = SqlRecordBuffer.StorageType.CharArray;
				}
				if ((long)(num + length) > this.CharsLength)
				{
					int num2 = ((char[])this._object).Length;
					if (num + length > num2)
					{
						char[] array = new char[Math.Max(num + length, 2 * num2)];
						Array.Copy((char[])this._object, 0, array, 0, (int)this.CharsLength);
						this._object = array;
					}
					this.CharsLength = (long)(num + length);
				}
			}
			Array.Copy(buffer, bufferOffset, (char[])this._object, num, length);
			return length;
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x000C1FA1 File Offset: 0x000C01A1
		internal void SetNull()
		{
			this._isNull = true;
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x000C1FAC File Offset: 0x000C01AC
		private void ConvertXmlStringToByteArray()
		{
			string text = (string)this._object;
			byte[] array = new byte[2 + Encoding.Unicode.GetByteCount(text)];
			array[0] = byte.MaxValue;
			array[1] = 254;
			Encoding.Unicode.GetBytes(text, 0, text.Length, array, 2);
			this._object = array;
			this._value._int64 = (long)array.Length;
			this._type = SqlRecordBuffer.StorageType.ByteArray;
		}

		// Token: 0x04001ADC RID: 6876
		private bool _isNull;

		// Token: 0x04001ADD RID: 6877
		private SqlRecordBuffer.StorageType _type;

		// Token: 0x04001ADE RID: 6878
		private SqlRecordBuffer.Storage _value;

		// Token: 0x04001ADF RID: 6879
		private object _object;

		// Token: 0x04001AE0 RID: 6880
		private SmiMetaData _metadata;

		// Token: 0x04001AE1 RID: 6881
		private bool _isMetaSet;

		// Token: 0x020003AF RID: 943
		internal enum StorageType
		{
			// Token: 0x04001AE3 RID: 6883
			Boolean,
			// Token: 0x04001AE4 RID: 6884
			Byte,
			// Token: 0x04001AE5 RID: 6885
			ByteArray,
			// Token: 0x04001AE6 RID: 6886
			CharArray,
			// Token: 0x04001AE7 RID: 6887
			DateTime,
			// Token: 0x04001AE8 RID: 6888
			DateTimeOffset,
			// Token: 0x04001AE9 RID: 6889
			Double,
			// Token: 0x04001AEA RID: 6890
			Guid,
			// Token: 0x04001AEB RID: 6891
			Int16,
			// Token: 0x04001AEC RID: 6892
			Int32,
			// Token: 0x04001AED RID: 6893
			Int64,
			// Token: 0x04001AEE RID: 6894
			Single,
			// Token: 0x04001AEF RID: 6895
			String,
			// Token: 0x04001AF0 RID: 6896
			SqlDecimal,
			// Token: 0x04001AF1 RID: 6897
			TimeSpan
		}

		// Token: 0x020003B0 RID: 944
		[StructLayout(LayoutKind.Explicit)]
		internal struct Storage
		{
			// Token: 0x04001AF2 RID: 6898
			[FieldOffset(0)]
			internal bool _boolean;

			// Token: 0x04001AF3 RID: 6899
			[FieldOffset(0)]
			internal byte _byte;

			// Token: 0x04001AF4 RID: 6900
			[FieldOffset(0)]
			internal DateTime _dateTime;

			// Token: 0x04001AF5 RID: 6901
			[FieldOffset(0)]
			internal DateTimeOffset _dateTimeOffset;

			// Token: 0x04001AF6 RID: 6902
			[FieldOffset(0)]
			internal double _double;

			// Token: 0x04001AF7 RID: 6903
			[FieldOffset(0)]
			internal Guid _guid;

			// Token: 0x04001AF8 RID: 6904
			[FieldOffset(0)]
			internal short _int16;

			// Token: 0x04001AF9 RID: 6905
			[FieldOffset(0)]
			internal int _int32;

			// Token: 0x04001AFA RID: 6906
			[FieldOffset(0)]
			internal long _int64;

			// Token: 0x04001AFB RID: 6907
			[FieldOffset(0)]
			internal float _single;

			// Token: 0x04001AFC RID: 6908
			[FieldOffset(0)]
			internal TimeSpan _timeSpan;
		}
	}
}
