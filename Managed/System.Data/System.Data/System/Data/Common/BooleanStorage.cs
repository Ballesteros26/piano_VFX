using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000327 RID: 807
	internal sealed class BooleanStorage : DataStorage
	{
		// Token: 0x060024A0 RID: 9376 RVA: 0x000A77F7 File Offset: 0x000A59F7
		internal BooleanStorage(DataColumn column)
			: base(column, typeof(bool), false, StorageType.Boolean)
		{
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000A7814 File Offset: 0x000A5A14
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					bool flag2 = true;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							flag2 = this._values[num] && flag2;
							flag = true;
						}
					}
					if (flag)
					{
						return flag2;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					bool flag3 = false;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							flag3 = this._values[num2] || flag3;
							flag = true;
						}
					}
					if (flag)
					{
						return flag3;
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
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(bool));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000A7930 File Offset: 0x000A5B30
		public override int Compare(int recordNo1, int recordNo2)
		{
			bool flag = this._values[recordNo1];
			bool flag2 = this._values[recordNo2];
			if (!flag || !flag2)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return flag.CompareTo(flag2);
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000A796C File Offset: 0x000A5B6C
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
				bool flag = this._values[recordNo];
				if (!flag && this.IsNull(recordNo))
				{
					return -1;
				}
				return flag.CompareTo((bool)value);
			}
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000A79B3 File Offset: 0x000A5BB3
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToBoolean(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000A79E4 File Offset: 0x000A5BE4
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000A7A00 File Offset: 0x000A5C00
		public override object Get(int record)
		{
			bool flag = this._values[record];
			if (flag)
			{
				return flag;
			}
			return base.GetBits(record);
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000A7A27 File Offset: 0x000A5C27
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = false;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToBoolean(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000A7A68 File Offset: 0x000A5C68
		public override void SetCapacity(int capacity)
		{
			bool[] array = new bool[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000A7AAE File Offset: 0x000A5CAE
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToBoolean(s);
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000A7ABB File Offset: 0x000A5CBB
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((bool)value);
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000A7AC8 File Offset: 0x000A5CC8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new bool[recordCount];
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000A7AD0 File Offset: 0x000A5CD0
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((bool[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000A7AF2 File Offset: 0x000A5CF2
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (bool[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x040017E0 RID: 6112
		private const bool defaultValue = false;

		// Token: 0x040017E1 RID: 6113
		private bool[] _values;
	}
}
