using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000328 RID: 808
	internal sealed class ByteStorage : DataStorage
	{
		// Token: 0x060024AE RID: 9390 RVA: 0x000A7B07 File Offset: 0x000A5D07
		internal ByteStorage(DataColumn column)
			: base(column, typeof(byte), 0, StorageType.Byte)
		{
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000A7B24 File Offset: 0x000A5D24
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			checked
			{
				try
				{
					switch (kind)
					{
					case AggregateType.Sum:
					{
						ulong num = 0UL;
						foreach (int num2 in records)
						{
							if (!this.IsNull(num2))
							{
								num += unchecked((ulong)this._values[num2]);
								flag = true;
							}
						}
						if (flag)
						{
							return num;
						}
						return this._nullValue;
					}
					case AggregateType.Mean:
					{
						long num3 = 0L;
						int num4 = 0;
						foreach (int num5 in records)
						{
							if (!this.IsNull(num5))
							{
								num3 += (long)(unchecked((ulong)this._values[num5]));
								unchecked
								{
									num4++;
									flag = true;
								}
							}
						}
						if (flag)
						{
							return (byte)(num3 / unchecked((long)num4));
						}
						return this._nullValue;
					}
					case AggregateType.Min:
					{
						byte b = byte.MaxValue;
						foreach (int num6 in records)
						{
							if (!this.IsNull(num6))
							{
								b = Math.Min(this._values[num6], b);
								flag = true;
							}
						}
						if (flag)
						{
							return b;
						}
						return this._nullValue;
					}
					case AggregateType.Max:
					{
						byte b2 = 0;
						foreach (int num7 in records)
						{
							if (!this.IsNull(num7))
							{
								b2 = Math.Max(this._values[num7], b2);
								flag = true;
							}
						}
						if (flag)
						{
							return b2;
						}
						return this._nullValue;
					}
					case AggregateType.First:
						if (records.Length != 0)
						{
							return this._values[records[0]];
						}
						return null;
					case AggregateType.Count:
						return base.Aggregate(records, kind);
					case AggregateType.Var:
					case AggregateType.StDev:
					{
						int num8 = 0;
						double num9 = 0.0;
						double num10 = 0.0;
						unchecked
						{
							foreach (int num11 in records)
							{
								if (!this.IsNull(num11))
								{
									num9 += (double)this._values[num11];
									num10 += (double)this._values[num11] * (double)this._values[num11];
									num8++;
								}
							}
							if (num8 <= 1)
							{
								return this._nullValue;
							}
							double num12 = (double)num8 * num10 - num9 * num9;
							if (num12 / (num9 * num9) < 1E-15 || num12 < 0.0)
							{
								num12 = 0.0;
							}
							else
							{
								num12 /= (double)(num8 * (num8 - 1));
							}
							if (kind == AggregateType.StDev)
							{
								return Math.Sqrt(num12);
							}
							return num12;
						}
					}
					}
				}
				catch (OverflowException)
				{
					throw ExprException.Overflow(typeof(byte));
				}
				throw ExceptionBuilder.AggregateException(kind, this._dataType);
			}
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000A7E24 File Offset: 0x000A6024
		public override int Compare(int recordNo1, int recordNo2)
		{
			byte b = this._values[recordNo1];
			byte b2 = this._values[recordNo2];
			if (b == 0 || b2 == 0)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return b.CompareTo(b2);
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000A7E60 File Offset: 0x000A6060
		public override int CompareValueTo(int recordNo, object value)
		{
			if (this._nullValue == value)
			{
				if (this.IsNull(recordNo))
				{
					return 0;
				}
				return 1;
			}
			else
			{
				byte b = this._values[recordNo];
				if (b == 0 && this.IsNull(recordNo))
				{
					return -1;
				}
				return b.CompareTo((byte)value);
			}
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000A7EA7 File Offset: 0x000A60A7
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToByte(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000A7ED8 File Offset: 0x000A60D8
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000A7EF4 File Offset: 0x000A60F4
		public override object Get(int record)
		{
			byte b = this._values[record];
			if (b != 0)
			{
				return b;
			}
			return base.GetBits(record);
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000A7F1B File Offset: 0x000A611B
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = 0;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToByte(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000A7F5C File Offset: 0x000A615C
		public override void SetCapacity(int capacity)
		{
			byte[] array = new byte[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000A7FA2 File Offset: 0x000A61A2
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToByte(s);
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000A7FAF File Offset: 0x000A61AF
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((byte)value);
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000A7FBC File Offset: 0x000A61BC
		protected override object GetEmptyStorage(int recordCount)
		{
			return new byte[recordCount];
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000A7FC4 File Offset: 0x000A61C4
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((byte[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000A7FE6 File Offset: 0x000A61E6
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (byte[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x040017E2 RID: 6114
		private const byte defaultValue = 0;

		// Token: 0x040017E3 RID: 6115
		private byte[] _values;
	}
}
