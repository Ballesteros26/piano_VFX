using System;
using System.Collections;
using System.Globalization;
using System.Numerics;

namespace System.Data.Common
{
	// Token: 0x02000326 RID: 806
	internal sealed class BigIntegerStorage : DataStorage
	{
		// Token: 0x06002490 RID: 9360 RVA: 0x000A726D File Offset: 0x000A546D
		internal BigIntegerStorage(DataColumn column)
			: base(column, typeof(BigInteger), BigInteger.Zero, StorageType.BigInteger)
		{
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000A728C File Offset: 0x000A548C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000A729C File Offset: 0x000A549C
		public override int Compare(int recordNo1, int recordNo2)
		{
			BigInteger bigInteger = this._values[recordNo1];
			BigInteger bigInteger2 = this._values[recordNo2];
			if (bigInteger.IsZero || bigInteger2.IsZero)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return bigInteger.CompareTo(bigInteger2);
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x000A72EC File Offset: 0x000A54EC
		public override int CompareValueTo(int recordNo, object value)
		{
			if (this._nullValue == value)
			{
				if (!base.HasValue(recordNo))
				{
					return 0;
				}
				return 1;
			}
			else
			{
				BigInteger bigInteger = this._values[recordNo];
				if (bigInteger.IsZero && !base.HasValue(recordNo))
				{
					return -1;
				}
				return bigInteger.CompareTo((BigInteger)value);
			}
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x000A7340 File Offset: 0x000A5540
		internal static BigInteger ConvertToBigInteger(object value, IFormatProvider formatProvider)
		{
			if (value.GetType() == typeof(BigInteger))
			{
				return (BigInteger)value;
			}
			if (value.GetType() == typeof(string))
			{
				return BigInteger.Parse((string)value, formatProvider);
			}
			if (value.GetType() == typeof(long))
			{
				return (long)value;
			}
			if (value.GetType() == typeof(int))
			{
				return (int)value;
			}
			if (value.GetType() == typeof(short))
			{
				return (short)value;
			}
			if (value.GetType() == typeof(sbyte))
			{
				return (sbyte)value;
			}
			if (value.GetType() == typeof(ulong))
			{
				return (ulong)value;
			}
			if (value.GetType() == typeof(uint))
			{
				return (uint)value;
			}
			if (value.GetType() == typeof(ushort))
			{
				return (ushort)value;
			}
			if (value.GetType() == typeof(byte))
			{
				return (byte)value;
			}
			throw ExceptionBuilder.ConvertFailed(value.GetType(), typeof(BigInteger));
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x000A74BC File Offset: 0x000A56BC
		internal static object ConvertFromBigInteger(BigInteger value, Type type, IFormatProvider formatProvider)
		{
			if (type == typeof(string))
			{
				return value.ToString("D", formatProvider);
			}
			if (type == typeof(sbyte))
			{
				return (sbyte)value;
			}
			if (type == typeof(short))
			{
				return (short)value;
			}
			if (type == typeof(int))
			{
				return (int)value;
			}
			if (type == typeof(long))
			{
				return (long)value;
			}
			if (type == typeof(byte))
			{
				return (byte)value;
			}
			if (type == typeof(ushort))
			{
				return (ushort)value;
			}
			if (type == typeof(uint))
			{
				return (uint)value;
			}
			if (type == typeof(ulong))
			{
				return (ulong)value;
			}
			if (type == typeof(float))
			{
				return (float)value;
			}
			if (type == typeof(double))
			{
				return (double)value;
			}
			if (type == typeof(decimal))
			{
				return (decimal)value;
			}
			if (type == typeof(BigInteger))
			{
				return value;
			}
			throw ExceptionBuilder.ConvertFailed(typeof(BigInteger), type);
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000A765E File Offset: 0x000A585E
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = BigIntegerStorage.ConvertToBigInteger(value, base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000A768A File Offset: 0x000A588A
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000A76AC File Offset: 0x000A58AC
		public override object Get(int record)
		{
			BigInteger bigInteger = this._values[record];
			if (!bigInteger.IsZero)
			{
				return bigInteger;
			}
			return base.GetBits(record);
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000A76E0 File Offset: 0x000A58E0
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = BigInteger.Zero;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = BigIntegerStorage.ConvertToBigInteger(value, base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000A7730 File Offset: 0x000A5930
		public override void SetCapacity(int capacity)
		{
			BigInteger[] array = new BigInteger[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000A7776 File Offset: 0x000A5976
		public override object ConvertXmlToObject(string s)
		{
			return BigInteger.Parse(s, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000A7788 File Offset: 0x000A5988
		public override string ConvertObjectToXml(object value)
		{
			return ((BigInteger)value).ToString("D", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000A77AD File Offset: 0x000A59AD
		protected override object GetEmptyStorage(int recordCount)
		{
			return new BigInteger[recordCount];
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000A77B5 File Offset: 0x000A59B5
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((BigInteger[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000A77E2 File Offset: 0x000A59E2
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (BigInteger[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x040017DF RID: 6111
		private BigInteger[] _values;
	}
}
