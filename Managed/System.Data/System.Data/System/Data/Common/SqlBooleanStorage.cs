using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200036F RID: 879
	internal sealed class SqlBooleanStorage : DataStorage
	{
		// Token: 0x060029CC RID: 10700 RVA: 0x000B9C3C File Offset: 0x000B7E3C
		public SqlBooleanStorage(DataColumn column)
			: base(column, typeof(SqlBoolean), SqlBoolean.Null, SqlBoolean.Null, StorageType.SqlBoolean)
		{
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x000B9C68 File Offset: 0x000B7E68
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					SqlBoolean sqlBoolean = true;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlBoolean = SqlBoolean.And(this._values[num], sqlBoolean);
							flag = true;
						}
					}
					if (flag)
					{
						return sqlBoolean;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlBoolean sqlBoolean2 = false;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							sqlBoolean2 = SqlBoolean.Or(this._values[num2], sqlBoolean2);
							flag = true;
						}
					}
					if (flag)
					{
						return sqlBoolean2;
					}
					return this._nullValue;
				}
				case AggregateType.First:
					if (records.Length != 0)
					{
						return this._values[records[0]];
					}
					return this._nullValue;
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
				throw ExprException.Overflow(typeof(SqlBoolean));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x000B9DD8 File Offset: 0x000B7FD8
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x000B9DF7 File Offset: 0x000B7FF7
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlBoolean)value);
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x000B9E10 File Offset: 0x000B8010
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlBoolean(value);
			}
			return this._nullValue;
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x000B9E27 File Offset: 0x000B8027
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x000B9E41 File Offset: 0x000B8041
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x000B9E54 File Offset: 0x000B8054
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x000B9E67 File Offset: 0x000B8067
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlBoolean(value);
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x000B9E7C File Offset: 0x000B807C
		public override void SetCapacity(int capacity)
		{
			SqlBoolean[] array = new SqlBoolean[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x000B9EBC File Offset: 0x000B80BC
		public override object ConvertXmlToObject(string s)
		{
			SqlBoolean sqlBoolean = default(SqlBoolean);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlBoolean;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlBoolean)xmlSerializable;
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x000B9F24 File Offset: 0x000B8124
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x000B9F74 File Offset: 0x000B8174
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBoolean[recordCount];
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x000B9F7C File Offset: 0x000B817C
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlBoolean[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x000B9FA6 File Offset: 0x000B81A6
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlBoolean[])store;
		}

		// Token: 0x0400193F RID: 6463
		private SqlBoolean[] _values;
	}
}
