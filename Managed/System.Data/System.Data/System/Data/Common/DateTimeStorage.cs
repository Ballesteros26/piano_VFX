using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x0200033C RID: 828
	internal sealed class DateTimeStorage : DataStorage
	{
		// Token: 0x0600266B RID: 9835 RVA: 0x000AD3BE File Offset: 0x000AB5BE
		internal DateTimeStorage(DataColumn column)
			: base(column, typeof(DateTime), DateTimeStorage.s_defaultValue, StorageType.DateTime)
		{
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x000AD3E0 File Offset: 0x000AB5E0
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					DateTime dateTime = DateTime.MaxValue;
					foreach (int num in records)
					{
						if (base.HasValue(num))
						{
							dateTime = ((DateTime.Compare(this._values[num], dateTime) < 0) ? this._values[num] : dateTime);
							flag = true;
						}
					}
					if (flag)
					{
						return dateTime;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					DateTime dateTime2 = DateTime.MinValue;
					foreach (int num2 in records)
					{
						if (base.HasValue(num2))
						{
							dateTime2 = ((DateTime.Compare(this._values[num2], dateTime2) >= 0) ? this._values[num2] : dateTime2);
							flag = true;
						}
					}
					if (flag)
					{
						return dateTime2;
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
				throw ExprException.Overflow(typeof(DateTime));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000AD570 File Offset: 0x000AB770
		public override int Compare(int recordNo1, int recordNo2)
		{
			DateTime dateTime = this._values[recordNo1];
			DateTime dateTime2 = this._values[recordNo2];
			if (dateTime == DateTimeStorage.s_defaultValue || dateTime2 == DateTimeStorage.s_defaultValue)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return DateTime.Compare(dateTime, dateTime2);
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000AD5C8 File Offset: 0x000AB7C8
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
				DateTime dateTime = this._values[recordNo];
				if (DateTimeStorage.s_defaultValue == dateTime && !base.HasValue(recordNo))
				{
					return -1;
				}
				return DateTime.Compare(dateTime, (DateTime)value);
			}
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x000AD61C File Offset: 0x000AB81C
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToDateTime(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x000AD64D File Offset: 0x000AB84D
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x000AD670 File Offset: 0x000AB870
		public override object Get(int record)
		{
			DateTime dateTime = this._values[record];
			if (dateTime != DateTimeStorage.s_defaultValue || base.HasValue(record))
			{
				return dateTime;
			}
			return this._nullValue;
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x000AD6B0 File Offset: 0x000AB8B0
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = DateTimeStorage.s_defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			DateTime dateTime = ((IConvertible)value).ToDateTime(base.FormatProvider);
			DateTime dateTime2;
			switch (base.DateTimeMode)
			{
			case DataSetDateTime.Local:
				if (dateTime.Kind == DateTimeKind.Local)
				{
					dateTime2 = dateTime;
				}
				else if (dateTime.Kind == DateTimeKind.Utc)
				{
					dateTime2 = dateTime.ToLocalTime();
				}
				else
				{
					dateTime2 = DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
				}
				break;
			case DataSetDateTime.Unspecified:
			case DataSetDateTime.UnspecifiedLocal:
				dateTime2 = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
				break;
			case DataSetDateTime.Utc:
				if (dateTime.Kind == DateTimeKind.Utc)
				{
					dateTime2 = dateTime;
				}
				else if (dateTime.Kind == DateTimeKind.Local)
				{
					dateTime2 = dateTime.ToUniversalTime();
				}
				else
				{
					dateTime2 = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
				}
				break;
			default:
				throw ExceptionBuilder.InvalidDateTimeMode(base.DateTimeMode);
			}
			this._values[record] = dateTime2;
			base.SetNullBit(record, false);
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x000AD798 File Offset: 0x000AB998
		public override void SetCapacity(int capacity)
		{
			DateTime[] array = new DateTime[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x000AD7E0 File Offset: 0x000AB9E0
		public override object ConvertXmlToObject(string s)
		{
			object obj;
			if (base.DateTimeMode == DataSetDateTime.UnspecifiedLocal)
			{
				obj = XmlConvert.ToDateTime(s, XmlDateTimeSerializationMode.Unspecified);
			}
			else
			{
				obj = XmlConvert.ToDateTime(s, XmlDateTimeSerializationMode.RoundtripKind);
			}
			return obj;
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x000AD814 File Offset: 0x000ABA14
		public override string ConvertObjectToXml(object value)
		{
			string text;
			if (base.DateTimeMode == DataSetDateTime.UnspecifiedLocal)
			{
				text = XmlConvert.ToString((DateTime)value, XmlDateTimeSerializationMode.Local);
			}
			else
			{
				text = XmlConvert.ToString((DateTime)value, XmlDateTimeSerializationMode.RoundtripKind);
			}
			return text;
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x000AD847 File Offset: 0x000ABA47
		protected override object GetEmptyStorage(int recordCount)
		{
			return new DateTime[recordCount];
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x000AD850 File Offset: 0x000ABA50
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			DateTime[] array = (DateTime[])store;
			bool flag = !base.HasValue(record);
			if (flag || (base.DateTimeMode & DataSetDateTime.Local) == (DataSetDateTime)0)
			{
				array[storeIndex] = this._values[record];
			}
			else
			{
				array[storeIndex] = this._values[record].ToUniversalTime();
			}
			nullbits.Set(storeIndex, flag);
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x000AD8B4 File Offset: 0x000ABAB4
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (DateTime[])store;
			base.SetNullStorage(nullbits);
			if (base.DateTimeMode == DataSetDateTime.UnspecifiedLocal)
			{
				for (int i = 0; i < this._values.Length; i++)
				{
					if (base.HasValue(i))
					{
						this._values[i] = DateTime.SpecifyKind(this._values[i].ToLocalTime(), DateTimeKind.Unspecified);
					}
				}
				return;
			}
			if (base.DateTimeMode == DataSetDateTime.Local)
			{
				for (int j = 0; j < this._values.Length; j++)
				{
					if (base.HasValue(j))
					{
						this._values[j] = this._values[j].ToLocalTime();
					}
				}
			}
		}

		// Token: 0x04001892 RID: 6290
		private static readonly DateTime s_defaultValue = DateTime.MinValue;

		// Token: 0x04001893 RID: 6291
		private DateTime[] _values;
	}
}
