using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000354 RID: 852
	internal sealed class DoubleStorage : DataStorage
	{
		// Token: 0x06002869 RID: 10345 RVA: 0x000B1976 File Offset: 0x000AFB76
		internal DoubleStorage(DataColumn column)
			: base(column, typeof(double), 0.0, StorageType.Double)
		{
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x000B199C File Offset: 0x000AFB9C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					double num = 0.0;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							num += this._values[num2];
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
					double num3 = 0.0;
					int num4 = 0;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							num3 += this._values[num5];
							num4++;
							flag = true;
						}
					}
					if (flag)
					{
						return num3 / (double)num4;
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					double num6 = double.MaxValue;
					foreach (int num7 in records)
					{
						if (!this.IsNull(num7))
						{
							num6 = Math.Min(this._values[num7], num6);
							flag = true;
						}
					}
					if (flag)
					{
						return num6;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					double num8 = double.MinValue;
					foreach (int num9 in records)
					{
						if (!this.IsNull(num9))
						{
							num8 = Math.Max(this._values[num9], num8);
							flag = true;
						}
					}
					if (flag)
					{
						return num8;
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
					int num10 = 0;
					double num11 = 0.0;
					double num12 = 0.0;
					foreach (int num13 in records)
					{
						if (!this.IsNull(num13))
						{
							num11 += this._values[num13];
							num12 += this._values[num13] * this._values[num13];
							num10++;
						}
					}
					if (num10 <= 1)
					{
						return this._nullValue;
					}
					double num14 = (double)num10 * num12 - num11 * num11;
					if (num14 / (num11 * num11) < 1E-15 || num14 < 0.0)
					{
						num14 = 0.0;
					}
					else
					{
						num14 /= (double)(num10 * (num10 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(num14);
					}
					return num14;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(double));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x000B1CB0 File Offset: 0x000AFEB0
		public override int Compare(int recordNo1, int recordNo2)
		{
			double num = this._values[recordNo1];
			double num2 = this._values[recordNo2];
			if (num == 0.0 || num2 == 0.0)
			{
				int num3 = base.CompareBits(recordNo1, recordNo2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			return num.CompareTo(num2);
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x000B1D00 File Offset: 0x000AFF00
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
				double num = this._values[recordNo];
				if (0.0 == num && this.IsNull(recordNo))
				{
					return -1;
				}
				return num.CompareTo((double)value);
			}
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x000B1D50 File Offset: 0x000AFF50
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToDouble(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x000B1D81 File Offset: 0x000AFF81
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x000B1D9C File Offset: 0x000AFF9C
		public override object Get(int record)
		{
			double num = this._values[record];
			if (num != 0.0)
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x000B1DCC File Offset: 0x000AFFCC
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = 0.0;
				base.SetNullBit(record, true);
				return;
			}
			this._values[record] = ((IConvertible)value).ToDouble(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x000B1E20 File Offset: 0x000B0020
		public override void SetCapacity(int capacity)
		{
			double[] array = new double[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x000B1E66 File Offset: 0x000B0066
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToDouble(s);
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000B1E73 File Offset: 0x000B0073
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((double)value);
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000B1E80 File Offset: 0x000B0080
		protected override object GetEmptyStorage(int recordCount)
		{
			return new double[recordCount];
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000B1E88 File Offset: 0x000B0088
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((double[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x000B1EAA File Offset: 0x000B00AA
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (double[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001906 RID: 6406
		private const double defaultValue = 0.0;

		// Token: 0x04001907 RID: 6407
		private double[] _values;
	}
}
