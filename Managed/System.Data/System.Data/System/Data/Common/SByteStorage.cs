using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x0200035F RID: 863
	internal sealed class SByteStorage : DataStorage
	{
		// Token: 0x060028D9 RID: 10457 RVA: 0x000B3DE1 File Offset: 0x000B1FE1
		public SByteStorage(DataColumn column)
			: base(column, typeof(sbyte), 0, StorageType.SByte)
		{
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000B3DFC File Offset: 0x000B1FFC
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
						long num = 0L;
						foreach (int num2 in records)
						{
							if (!this.IsNull(num2))
							{
								num += unchecked((long)this._values[num2]);
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
								num3 += unchecked((long)this._values[num5]);
								unchecked
								{
									num4++;
									flag = true;
								}
							}
						}
						if (flag)
						{
							return (sbyte)(num3 / unchecked((long)num4));
						}
						return this._nullValue;
					}
					case AggregateType.Min:
					{
						sbyte b = sbyte.MaxValue;
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
						sbyte b2 = sbyte.MinValue;
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
					throw ExprException.Overflow(typeof(sbyte));
				}
				throw ExceptionBuilder.AggregateException(kind, this._dataType);
			}
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x000B40F8 File Offset: 0x000B22F8
		public override int Compare(int recordNo1, int recordNo2)
		{
			sbyte b = this._values[recordNo1];
			sbyte b2 = this._values[recordNo2];
			if (b.Equals(0) || b2.Equals(0))
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return b.CompareTo(b2);
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000B4144 File Offset: 0x000B2344
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
				sbyte b = this._values[recordNo];
				if (b == 0 && this.IsNull(recordNo))
				{
					return -1;
				}
				return b.CompareTo((sbyte)value);
			}
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x000B418B File Offset: 0x000B238B
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToSByte(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x000B41BC File Offset: 0x000B23BC
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000B41D8 File Offset: 0x000B23D8
		public override object Get(int record)
		{
			sbyte b = this._values[record];
			if (!b.Equals(0))
			{
				return b;
			}
			return base.GetBits(record);
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x000B4206 File Offset: 0x000B2406
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = 0;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToSByte(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000B4244 File Offset: 0x000B2444
		public override void SetCapacity(int capacity)
		{
			sbyte[] array = new sbyte[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000B428A File Offset: 0x000B248A
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToSByte(s);
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000B4297 File Offset: 0x000B2497
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((sbyte)value);
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000B42A4 File Offset: 0x000B24A4
		protected override object GetEmptyStorage(int recordCount)
		{
			return new sbyte[recordCount];
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000B42AC File Offset: 0x000B24AC
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((sbyte[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000B42CE File Offset: 0x000B24CE
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (sbyte[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x0400192F RID: 6447
		private const sbyte defaultValue = 0;

		// Token: 0x04001930 RID: 6448
		private sbyte[] _values;
	}
}
