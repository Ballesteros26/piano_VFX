using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x0200033B RID: 827
	internal sealed class DateTimeOffsetStorage : DataStorage
	{
		// Token: 0x0600265C RID: 9820 RVA: 0x000ACFE9 File Offset: 0x000AB1E9
		internal DateTimeOffsetStorage(DataColumn column)
			: base(column, typeof(DateTimeOffset), DateTimeOffsetStorage.s_defaultValue, StorageType.DateTimeOffset)
		{
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x000AD008 File Offset: 0x000AB208
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					DateTimeOffset dateTimeOffset = DateTimeOffset.MaxValue;
					foreach (int num in records)
					{
						if (base.HasValue(num))
						{
							dateTimeOffset = ((DateTimeOffset.Compare(this._values[num], dateTimeOffset) < 0) ? this._values[num] : dateTimeOffset);
							flag = true;
						}
					}
					if (flag)
					{
						return dateTimeOffset;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					DateTimeOffset dateTimeOffset2 = DateTimeOffset.MinValue;
					foreach (int num2 in records)
					{
						if (base.HasValue(num2))
						{
							dateTimeOffset2 = ((DateTimeOffset.Compare(this._values[num2], dateTimeOffset2) >= 0) ? this._values[num2] : dateTimeOffset2);
							flag = true;
						}
					}
					if (flag)
					{
						return dateTimeOffset2;
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
				{
					int num3 = 0;
					for (int k = 0; k < records.Length; k++)
					{
						if (base.HasValue(records[k]))
						{
							num3++;
						}
					}
					return num3;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(DateTimeOffset));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x000AD198 File Offset: 0x000AB398
		public override int Compare(int recordNo1, int recordNo2)
		{
			DateTimeOffset dateTimeOffset = this._values[recordNo1];
			DateTimeOffset dateTimeOffset2 = this._values[recordNo2];
			if (dateTimeOffset == DateTimeOffsetStorage.s_defaultValue || dateTimeOffset2 == DateTimeOffsetStorage.s_defaultValue)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return DateTimeOffset.Compare(dateTimeOffset, dateTimeOffset2);
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x000AD1F0 File Offset: 0x000AB3F0
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
				DateTimeOffset dateTimeOffset = this._values[recordNo];
				if (DateTimeOffsetStorage.s_defaultValue == dateTimeOffset && !base.HasValue(recordNo))
				{
					return -1;
				}
				return DateTimeOffset.Compare(dateTimeOffset, (DateTimeOffset)value);
			}
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x000AD244 File Offset: 0x000AB444
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = (DateTimeOffset)value;
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x000AD26A File Offset: 0x000AB46A
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000AD28C File Offset: 0x000AB48C
		public override object Get(int record)
		{
			DateTimeOffset dateTimeOffset = this._values[record];
			if (dateTimeOffset != DateTimeOffsetStorage.s_defaultValue || base.HasValue(record))
			{
				return dateTimeOffset;
			}
			return this._nullValue;
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x000AD2C9 File Offset: 0x000AB4C9
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = DateTimeOffsetStorage.s_defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = (DateTimeOffset)value;
			base.SetNullBit(record, false);
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000AD308 File Offset: 0x000AB508
		public override void SetCapacity(int capacity)
		{
			DateTimeOffset[] array = new DateTimeOffset[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x000AD34E File Offset: 0x000AB54E
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToDateTimeOffset(s);
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x000AD35B File Offset: 0x000AB55B
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((DateTimeOffset)value);
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x000AD368 File Offset: 0x000AB568
		protected override object GetEmptyStorage(int recordCount)
		{
			return new DateTimeOffset[recordCount];
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x000AD370 File Offset: 0x000AB570
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((DateTimeOffset[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x000AD39D File Offset: 0x000AB59D
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (DateTimeOffset[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001890 RID: 6288
		private static readonly DateTimeOffset s_defaultValue = DateTimeOffset.MinValue;

		// Token: 0x04001891 RID: 6289
		private DateTimeOffset[] _values;
	}
}
