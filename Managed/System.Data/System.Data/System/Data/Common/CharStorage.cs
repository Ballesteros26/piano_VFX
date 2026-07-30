using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000329 RID: 809
	internal sealed class CharStorage : DataStorage
	{
		// Token: 0x060024BC RID: 9404 RVA: 0x000A7FFB File Offset: 0x000A61FB
		internal CharStorage(DataColumn column)
			: base(column, typeof(char), '\0', StorageType.Char)
		{
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000A8018 File Offset: 0x000A6218
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					char c = char.MaxValue;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							c = ((this._values[num] < c) ? this._values[num] : c);
							flag = true;
						}
					}
					if (flag)
					{
						return c;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					char c2 = '\0';
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							c2 = ((this._values[num2] > c2) ? this._values[num2] : c2);
							flag = true;
						}
					}
					if (flag)
					{
						return c2;
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
				throw ExprException.Overflow(typeof(char));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000A8150 File Offset: 0x000A6350
		public override int Compare(int recordNo1, int recordNo2)
		{
			char c = this._values[recordNo1];
			char c2 = this._values[recordNo2];
			if (c == '\0' || c2 == '\0')
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return c.CompareTo(c2);
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000A818C File Offset: 0x000A638C
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
				char c = this._values[recordNo];
				if (c == '\0' && this.IsNull(recordNo))
				{
					return -1;
				}
				return c.CompareTo((char)value);
			}
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000A81D3 File Offset: 0x000A63D3
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToChar(base.FormatProvider);
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000A8204 File Offset: 0x000A6404
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000A8220 File Offset: 0x000A6420
		public override object Get(int record)
		{
			char c = this._values[record];
			if (c != '\0')
			{
				return c;
			}
			return base.GetBits(record);
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000A8248 File Offset: 0x000A6448
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = '\0';
				base.SetNullBit(record, true);
				return;
			}
			char c = ((IConvertible)value).ToChar(base.FormatProvider);
			if ((c >= '\ud800' && c <= '\udfff') || (c < '!' && (c == '\t' || c == '\n' || c == '\r')))
			{
				throw ExceptionBuilder.ProblematicChars(c);
			}
			this._values[record] = c;
			base.SetNullBit(record, false);
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000A82C0 File Offset: 0x000A64C0
		public override void SetCapacity(int capacity)
		{
			char[] array = new char[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000A8306 File Offset: 0x000A6506
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToChar(s);
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000A8313 File Offset: 0x000A6513
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((char)value);
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000A8320 File Offset: 0x000A6520
		protected override object GetEmptyStorage(int recordCount)
		{
			return new char[recordCount];
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000A8328 File Offset: 0x000A6528
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((char[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000A834A File Offset: 0x000A654A
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (char[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x040017E4 RID: 6116
		private const char defaultValue = '\0';

		// Token: 0x040017E5 RID: 6117
		private char[] _values;
	}
}
