using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000361 RID: 865
	internal sealed class SqlBinaryStorage : DataStorage
	{
		// Token: 0x060028FA RID: 10490 RVA: 0x000B5927 File Offset: 0x000B3B27
		public SqlBinaryStorage(DataColumn column)
			: base(column, typeof(SqlBinary), SqlBinary.Null, SqlBinary.Null, StorageType.SqlBinary)
		{
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x000B5950 File Offset: 0x000B3B50
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
				throw ExprException.Overflow(typeof(SqlBinary));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x000B59E0 File Offset: 0x000B3BE0
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x000B59FF File Offset: 0x000B3BFF
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlBinary)value);
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x000B5A18 File Offset: 0x000B3C18
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlBinary(value);
			}
			return this._nullValue;
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x000B5A2F File Offset: 0x000B3C2F
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x000B5A49 File Offset: 0x000B3C49
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x000B5A5C File Offset: 0x000B3C5C
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x000B5A6F File Offset: 0x000B3C6F
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlBinary(value);
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x000B5A84 File Offset: 0x000B3C84
		public override void SetCapacity(int capacity)
		{
			SqlBinary[] array = new SqlBinary[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x000B5AC4 File Offset: 0x000B3CC4
		public override object ConvertXmlToObject(string s)
		{
			SqlBinary sqlBinary = default(SqlBinary);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlBinary;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlBinary)xmlSerializable;
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x000B5B2C File Offset: 0x000B3D2C
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x000B5B7C File Offset: 0x000B3D7C
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBinary[recordCount];
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x000B5B84 File Offset: 0x000B3D84
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlBinary[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x000B5BAE File Offset: 0x000B3DAE
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlBinary[])store;
		}

		// Token: 0x04001931 RID: 6449
		private SqlBinary[] _values;
	}
}
