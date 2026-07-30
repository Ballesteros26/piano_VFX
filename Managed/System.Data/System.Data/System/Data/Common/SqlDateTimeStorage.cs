using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000365 RID: 869
	internal sealed class SqlDateTimeStorage : DataStorage
	{
		// Token: 0x06002934 RID: 10548 RVA: 0x000B6658 File Offset: 0x000B4858
		public SqlDateTimeStorage(DataColumn column)
			: base(column, typeof(SqlDateTime), SqlDateTime.Null, SqlDateTime.Null, StorageType.SqlDateTime)
		{
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x000B6684 File Offset: 0x000B4884
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					SqlDateTime sqlDateTime = SqlDateTime.MaxValue;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							if (SqlDateTime.LessThan(this._values[num], sqlDateTime).IsTrue)
							{
								sqlDateTime = this._values[num];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDateTime;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlDateTime sqlDateTime2 = SqlDateTime.MinValue;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							if (SqlDateTime.GreaterThan(this._values[num2], sqlDateTime2).IsTrue)
							{
								sqlDateTime2 = this._values[num2];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDateTime2;
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
						if (!this.IsNull(records[k]))
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
				throw ExprException.Overflow(typeof(SqlDateTime));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000B681C File Offset: 0x000B4A1C
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x000B683B File Offset: 0x000B4A3B
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlDateTime)value);
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x000B6854 File Offset: 0x000B4A54
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlDateTime(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000B686B File Offset: 0x000B4A6B
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000B6885 File Offset: 0x000B4A85
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000B6898 File Offset: 0x000B4A98
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000B68AB File Offset: 0x000B4AAB
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlDateTime(value);
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000B68C0 File Offset: 0x000B4AC0
		public override void SetCapacity(int capacity)
		{
			SqlDateTime[] array = new SqlDateTime[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x000B6900 File Offset: 0x000B4B00
		public override object ConvertXmlToObject(string s)
		{
			SqlDateTime sqlDateTime = default(SqlDateTime);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlDateTime;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlDateTime)xmlSerializable;
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x000B6968 File Offset: 0x000B4B68
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x000B69B8 File Offset: 0x000B4BB8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlDateTime[recordCount];
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x000B69C0 File Offset: 0x000B4BC0
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlDateTime[])store)[storeIndex] = this._values[record];
			nullbits.Set(record, this.IsNull(record));
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x000B69E9 File Offset: 0x000B4BE9
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlDateTime[])store;
		}

		// Token: 0x04001935 RID: 6453
		private SqlDateTime[] _values;
	}
}
