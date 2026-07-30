using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000364 RID: 868
	internal sealed class SqlCharsStorage : DataStorage
	{
		// Token: 0x06002926 RID: 10534 RVA: 0x000B642C File Offset: 0x000B462C
		public SqlCharsStorage(DataColumn column)
			: base(column, typeof(SqlChars), SqlChars.Null, SqlChars.Null, StorageType.SqlChars)
		{
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x000B644C File Offset: 0x000B464C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			try
			{
				if (kind != AggregateType.First)
				{
					if (kind == AggregateType.Count)
					{
						int num = 0;
						for (int i = 0; i < records.Length; i++)
						{
							if (!this.IsNull(records[i]))
							{
								num++;
							}
						}
						return num;
					}
				}
				else
				{
					if (records.Length != 0)
					{
						return this._values[records[0]];
					}
					return null;
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlChars));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x000061D5 File Offset: 0x000043D5
		public override int Compare(int recordNo1, int recordNo2)
		{
			return 0;
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x000061D5 File Offset: 0x000043D5
		public override int CompareValueTo(int recordNo, object value)
		{
			return 0;
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000B64D4 File Offset: 0x000B46D4
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x000B64E6 File Offset: 0x000B46E6
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x000B64F0 File Offset: 0x000B46F0
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x000B64FF File Offset: 0x000B46FF
		public override void Set(int record, object value)
		{
			if (value == DBNull.Value || value == null)
			{
				this._values[record] = SqlChars.Null;
				return;
			}
			this._values[record] = (SqlChars)value;
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x000B6528 File Offset: 0x000B4728
		public override void SetCapacity(int capacity)
		{
			SqlChars[] array = new SqlChars[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x000B6568 File Offset: 0x000B4768
		public override object ConvertXmlToObject(string s)
		{
			SqlString sqlString = default(SqlString);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlString;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return new SqlChars((SqlString)xmlSerializable);
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x000B65D0 File Offset: 0x000B47D0
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000B6620 File Offset: 0x000B4820
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlChars[recordCount];
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x000B6628 File Offset: 0x000B4828
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlChars[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x000B664A File Offset: 0x000B484A
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlChars[])store;
		}

		// Token: 0x04001934 RID: 6452
		private SqlChars[] _values;
	}
}
