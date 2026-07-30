using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200036E RID: 878
	internal sealed class SqlStringStorage : DataStorage
	{
		// Token: 0x060029BB RID: 10683 RVA: 0x000B9858 File Offset: 0x000B7A58
		public SqlStringStorage(DataColumn column)
			: base(column, typeof(SqlString), SqlString.Null, SqlString.Null, StorageType.SqlString)
		{
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x000B9884 File Offset: 0x000B7A84
		public override object Aggregate(int[] recordNos, AggregateType kind)
		{
			try
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
						if (!this.IsNull(recordNos[i]))
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
				throw ExprException.Overflow(typeof(SqlString));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x000B99DC File Offset: 0x000B7BDC
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.Compare(this._values[recordNo1], this._values[recordNo2]);
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000B99FC File Offset: 0x000B7BFC
		public int Compare(SqlString valueNo1, SqlString valueNo2)
		{
			if (valueNo1.IsNull && valueNo2.IsNull)
			{
				return 0;
			}
			if (valueNo1.IsNull)
			{
				return -1;
			}
			if (valueNo2.IsNull)
			{
				return 1;
			}
			return this._table.Compare(valueNo1.Value, valueNo2.Value);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x000B9A4C File Offset: 0x000B7C4C
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.Compare(this._values[recordNo], (SqlString)value);
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x000B9A66 File Offset: 0x000B7C66
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlString(value);
			}
			return this._nullValue;
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x000B9A7D File Offset: 0x000B7C7D
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000B9A97 File Offset: 0x000B7C97
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x000B9AAC File Offset: 0x000B7CAC
		public override int GetStringLength(int record)
		{
			SqlString sqlString = this._values[record];
			if (!sqlString.IsNull)
			{
				return sqlString.Value.Length;
			}
			return 0;
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x000B9ADD File Offset: 0x000B7CDD
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x000B9AF0 File Offset: 0x000B7CF0
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlString(value);
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x000B9B04 File Offset: 0x000B7D04
		public override void SetCapacity(int capacity)
		{
			SqlString[] array = new SqlString[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x000B9B44 File Offset: 0x000B7D44
		public override object ConvertXmlToObject(string s)
		{
			SqlString sqlString = default(SqlString);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlString;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlString)xmlSerializable;
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x000B9BAC File Offset: 0x000B7DAC
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x000B9BFC File Offset: 0x000B7DFC
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlString[recordCount];
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x000B9C04 File Offset: 0x000B7E04
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlString[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x000B9C2E File Offset: 0x000B7E2E
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlString[])store;
		}

		// Token: 0x0400193E RID: 6462
		private SqlString[] _values;
	}
}
