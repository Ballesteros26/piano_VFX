using System;
using System.Collections;

namespace System.Data.Common
{
	// Token: 0x02000374 RID: 884
	internal sealed class StringStorage : DataStorage
	{
		// Token: 0x060029FE RID: 10750 RVA: 0x000BAA81 File Offset: 0x000B8C81
		public StringStorage(DataColumn column)
			: base(column, typeof(string), string.Empty, StorageType.String)
		{
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x000BAA9C File Offset: 0x000B8C9C
		public override object Aggregate(int[] recordNos, AggregateType kind)
		{
			switch (kind)
			{
			case AggregateType.Min:
			{
				int num = -1;
				int i;
				for (i = 0; i < recordNos.Length; i++)
				{
					if (!this.IsNull(recordNos[i]))
					{
						num = recordNos[i];
						break;
					}
				}
				if (num >= 0)
				{
					for (i++; i < recordNos.Length; i++)
					{
						if (!this.IsNull(recordNos[i]) && this.Compare(num, recordNos[i]) > 0)
						{
							num = recordNos[i];
						}
					}
					return this.Get(num);
				}
				return this._nullValue;
			}
			case AggregateType.Max:
			{
				int num2 = -1;
				int i;
				for (i = 0; i < recordNos.Length; i++)
				{
					if (!this.IsNull(recordNos[i]))
					{
						num2 = recordNos[i];
						break;
					}
				}
				if (num2 >= 0)
				{
					for (i++; i < recordNos.Length; i++)
					{
						if (this.Compare(num2, recordNos[i]) < 0)
						{
							num2 = recordNos[i];
						}
					}
					return this.Get(num2);
				}
				return this._nullValue;
			}
			case AggregateType.Count:
			{
				int num3 = 0;
				for (int i = 0; i < recordNos.Length; i++)
				{
					if (this._values[recordNos[i]] != null)
					{
						num3++;
					}
				}
				return num3;
			}
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x000BABB0 File Offset: 0x000B8DB0
		public override int Compare(int recordNo1, int recordNo2)
		{
			string text = this._values[recordNo1];
			string text2 = this._values[recordNo2];
			if (text == text2)
			{
				return 0;
			}
			if (text == null)
			{
				return -1;
			}
			if (text2 == null)
			{
				return 1;
			}
			return this._table.Compare(text, text2);
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000BABEC File Offset: 0x000B8DEC
		public override int CompareValueTo(int recordNo, object value)
		{
			string text = this._values[recordNo];
			if (text == null)
			{
				if (this._nullValue == value)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (this._nullValue == value)
				{
					return 1;
				}
				return this._table.Compare(text, (string)value);
			}
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x000BAC2F File Offset: 0x000B8E2F
		public override object ConvertValue(object value)
		{
			if (this._nullValue != value)
			{
				if (value != null)
				{
					value = value.ToString();
				}
				else
				{
					value = this._nullValue;
				}
			}
			return value;
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x000BAC50 File Offset: 0x000B8E50
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x000BAC64 File Offset: 0x000B8E64
		public override object Get(int recordNo)
		{
			string text = this._values[recordNo];
			if (text != null)
			{
				return text;
			}
			return this._nullValue;
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x000BAC88 File Offset: 0x000B8E88
		public override int GetStringLength(int record)
		{
			string text = this._values[record];
			if (text == null)
			{
				return 0;
			}
			return text.Length;
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x000BACA9 File Offset: 0x000B8EA9
		public override bool IsNull(int record)
		{
			return this._values[record] == null;
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x000BACB6 File Offset: 0x000B8EB6
		public override void Set(int record, object value)
		{
			if (this._nullValue == value)
			{
				this._values[record] = null;
				return;
			}
			this._values[record] = value.ToString();
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x000BACDC File Offset: 0x000B8EDC
		public override void SetCapacity(int capacity)
		{
			string[] array = new string[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x00005DA6 File Offset: 0x00003FA6
		public override object ConvertXmlToObject(string s)
		{
			return s;
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x000BAD1B File Offset: 0x000B8F1B
		public override string ConvertObjectToXml(object value)
		{
			return (string)value;
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x000BAD23 File Offset: 0x000B8F23
		protected override object GetEmptyStorage(int recordCount)
		{
			return new string[recordCount];
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x000BAD2B File Offset: 0x000B8F2B
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((string[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x000BAD4D File Offset: 0x000B8F4D
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (string[])store;
		}

		// Token: 0x04001965 RID: 6501
		private string[] _values;
	}
}
