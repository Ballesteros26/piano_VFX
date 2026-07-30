using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000376 RID: 886
	internal sealed class TimeSpanStorage : DataStorage
	{
		// Token: 0x06002A0E RID: 10766 RVA: 0x000BAD5B File Offset: 0x000B8F5B
		public TimeSpanStorage(DataColumn column)
			: base(column, typeof(TimeSpan), TimeSpanStorage.s_defaultValue, StorageType.TimeSpan)
		{
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000BAD7C File Offset: 0x000B8F7C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					decimal num = 0m;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							num += this._values[num2].Ticks;
							flag = true;
						}
					}
					if (flag)
					{
						return TimeSpan.FromTicks((long)Math.Round(num));
					}
					return null;
				}
				case AggregateType.Mean:
				{
					decimal num3 = 0m;
					int num4 = 0;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							num3 += this._values[num5].Ticks;
							num4++;
						}
					}
					if (num4 > 0)
					{
						return TimeSpan.FromTicks((long)Math.Round(num3 / num4));
					}
					return null;
				}
				case AggregateType.Min:
				{
					TimeSpan timeSpan = TimeSpan.MaxValue;
					foreach (int num6 in records)
					{
						if (!this.IsNull(num6))
						{
							timeSpan = ((TimeSpan.Compare(this._values[num6], timeSpan) < 0) ? this._values[num6] : timeSpan);
							flag = true;
						}
					}
					if (flag)
					{
						return timeSpan;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					TimeSpan timeSpan2 = TimeSpan.MinValue;
					foreach (int num7 in records)
					{
						if (!this.IsNull(num7))
						{
							timeSpan2 = ((TimeSpan.Compare(this._values[num7], timeSpan2) >= 0) ? this._values[num7] : timeSpan2);
							flag = true;
						}
					}
					if (flag)
					{
						return timeSpan2;
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
				case AggregateType.StDev:
				{
					int num8 = 0;
					decimal num9 = 0m;
					foreach (int num10 in records)
					{
						if (!this.IsNull(num10))
						{
							num9 += this._values[num10].Ticks;
							num8++;
						}
					}
					if (num8 > 1)
					{
						double num11 = 0.0;
						decimal num12 = num9 / num8;
						foreach (int num13 in records)
						{
							if (!this.IsNull(num13))
							{
								double num14 = (double)(this._values[num13].Ticks - num12);
								num11 += num14 * num14;
							}
						}
						ulong num15 = (ulong)Math.Round(Math.Sqrt(num11 / (double)(num8 - 1)));
						if (num15 > 9223372036854775807UL)
						{
							num15 = 9223372036854775807UL;
						}
						return TimeSpan.FromTicks((long)num15);
					}
					return null;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(TimeSpan));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x000BB120 File Offset: 0x000B9320
		public override int Compare(int recordNo1, int recordNo2)
		{
			TimeSpan timeSpan = this._values[recordNo1];
			TimeSpan timeSpan2 = this._values[recordNo2];
			if (timeSpan == TimeSpanStorage.s_defaultValue || timeSpan2 == TimeSpanStorage.s_defaultValue)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return TimeSpan.Compare(timeSpan, timeSpan2);
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x000BB178 File Offset: 0x000B9378
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
				TimeSpan timeSpan = this._values[recordNo];
				if (TimeSpanStorage.s_defaultValue == timeSpan && this.IsNull(recordNo))
				{
					return -1;
				}
				return timeSpan.CompareTo((TimeSpan)value);
			}
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x000BB1D0 File Offset: 0x000B93D0
		private static TimeSpan ConvertToTimeSpan(object value)
		{
			Type type = value.GetType();
			if (type == typeof(string))
			{
				return TimeSpan.Parse((string)value);
			}
			if (type == typeof(int))
			{
				return new TimeSpan((long)((int)value));
			}
			if (type == typeof(long))
			{
				return new TimeSpan((long)value);
			}
			return (TimeSpan)value;
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x000BB245 File Offset: 0x000B9445
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = TimeSpanStorage.ConvertToTimeSpan(value);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x000BB26B File Offset: 0x000B946B
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000BB290 File Offset: 0x000B9490
		public override object Get(int record)
		{
			TimeSpan timeSpan = this._values[record];
			if (timeSpan != TimeSpanStorage.s_defaultValue)
			{
				return timeSpan;
			}
			return base.GetBits(record);
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x000BB2C5 File Offset: 0x000B94C5
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = TimeSpanStorage.s_defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = TimeSpanStorage.ConvertToTimeSpan(value);
			base.SetNullBit(record, false);
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x000BB304 File Offset: 0x000B9504
		public override void SetCapacity(int capacity)
		{
			TimeSpan[] array = new TimeSpan[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x000BB34A File Offset: 0x000B954A
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToTimeSpan(s);
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x000BB357 File Offset: 0x000B9557
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((TimeSpan)value);
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x000BB364 File Offset: 0x000B9564
		protected override object GetEmptyStorage(int recordCount)
		{
			return new TimeSpan[recordCount];
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x000BB36C File Offset: 0x000B956C
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((TimeSpan[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x000BB396 File Offset: 0x000B9596
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (TimeSpan[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x0400196C RID: 6508
		private static readonly TimeSpan s_defaultValue = TimeSpan.Zero;

		// Token: 0x0400196D RID: 6509
		private TimeSpan[] _values;
	}
}
