using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000367 RID: 871
	internal sealed class SqlDoubleStorage : DataStorage
	{
		// Token: 0x06002952 RID: 10578 RVA: 0x000B7028 File Offset: 0x000B5228
		public SqlDoubleStorage(DataColumn column)
			: base(column, typeof(SqlDouble), SqlDouble.Null, SqlDouble.Null, StorageType.SqlDouble)
		{
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000B7054 File Offset: 0x000B5254
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					SqlDouble sqlDouble = 0.0;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlDouble += this._values[num];
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDouble;
					}
					return this._nullValue;
				}
				case AggregateType.Mean:
				{
					SqlDouble sqlDouble2 = 0.0;
					int num2 = 0;
					foreach (int num3 in records)
					{
						if (!this.IsNull(num3))
						{
							sqlDouble2 += this._values[num3];
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						0.0;
						return sqlDouble2 / (double)num2;
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					SqlDouble sqlDouble3 = SqlDouble.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlDouble.LessThan(this._values[num4], sqlDouble3).IsTrue)
							{
								sqlDouble3 = this._values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDouble3;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlDouble sqlDouble4 = SqlDouble.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlDouble.GreaterThan(this._values[num5], sqlDouble4).IsTrue)
							{
								sqlDouble4 = this._values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDouble4;
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
					int num6 = 0;
					for (int l = 0; l < records.Length; l++)
					{
						if (!this.IsNull(records[l]))
						{
							num6++;
						}
					}
					return num6;
				}
				case AggregateType.Var:
				case AggregateType.StDev:
				{
					int num6 = 0;
					SqlDouble sqlDouble5 = 0.0;
					0.0;
					SqlDouble sqlDouble6 = 0.0;
					SqlDouble sqlDouble7 = 0.0;
					foreach (int num7 in records)
					{
						if (!this.IsNull(num7))
						{
							sqlDouble6 += this._values[num7];
							sqlDouble7 += this._values[num7] * this._values[num7];
							num6++;
						}
					}
					if (num6 <= 1)
					{
						return this._nullValue;
					}
					sqlDouble5 = (double)num6 * sqlDouble7 - sqlDouble6 * sqlDouble6;
					SqlBoolean sqlBoolean = sqlDouble5 / (sqlDouble6 * sqlDouble6) < 1E-15;
					if (sqlBoolean ? sqlBoolean : (sqlBoolean | (sqlDouble5 < 0.0)))
					{
						sqlDouble5 = 0.0;
					}
					else
					{
						sqlDouble5 /= (double)(num6 * (num6 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(sqlDouble5.Value);
					}
					return sqlDouble5;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlDouble));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000B7484 File Offset: 0x000B5684
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x000B74A3 File Offset: 0x000B56A3
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlDouble)value);
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x000B74BC File Offset: 0x000B56BC
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlDouble(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000B74D3 File Offset: 0x000B56D3
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x000B74ED File Offset: 0x000B56ED
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x000B7500 File Offset: 0x000B5700
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x000B7513 File Offset: 0x000B5713
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlDouble(value);
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x000B7528 File Offset: 0x000B5728
		public override void SetCapacity(int capacity)
		{
			SqlDouble[] array = new SqlDouble[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x000B7568 File Offset: 0x000B5768
		public override object ConvertXmlToObject(string s)
		{
			SqlDouble sqlDouble = default(SqlDouble);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlDouble;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlDouble)xmlSerializable;
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x000B75D0 File Offset: 0x000B57D0
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x000B7620 File Offset: 0x000B5820
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlDouble[recordCount];
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x000B7628 File Offset: 0x000B5828
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlDouble[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x000B7652 File Offset: 0x000B5852
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlDouble[])store;
		}

		// Token: 0x04001937 RID: 6455
		private SqlDouble[] _values;
	}
}
