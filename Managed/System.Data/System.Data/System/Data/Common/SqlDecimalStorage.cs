using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000366 RID: 870
	internal sealed class SqlDecimalStorage : DataStorage
	{
		// Token: 0x06002943 RID: 10563 RVA: 0x000B69F7 File Offset: 0x000B4BF7
		public SqlDecimalStorage(DataColumn column)
			: base(column, typeof(SqlDecimal), SqlDecimal.Null, SqlDecimal.Null, StorageType.SqlDecimal)
		{
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x000B6A20 File Offset: 0x000B4C20
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					SqlDecimal sqlDecimal = 0L;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlDecimal += this._values[num];
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDecimal;
					}
					return this._nullValue;
				}
				case AggregateType.Mean:
				{
					SqlDecimal sqlDecimal2 = 0L;
					int num2 = 0;
					foreach (int num3 in records)
					{
						if (!this.IsNull(num3))
						{
							sqlDecimal2 += this._values[num3];
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						0L;
						return sqlDecimal2 / (long)num2;
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					SqlDecimal sqlDecimal3 = SqlDecimal.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlDecimal.LessThan(this._values[num4], sqlDecimal3).IsTrue)
							{
								sqlDecimal3 = this._values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDecimal3;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlDecimal sqlDecimal4 = SqlDecimal.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlDecimal.GreaterThan(this._values[num5], sqlDecimal4).IsTrue)
							{
								sqlDecimal4 = this._values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDecimal4;
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
					SqlDouble sqlDouble = 0.0;
					0.0;
					SqlDouble sqlDouble2 = 0.0;
					SqlDouble sqlDouble3 = 0.0;
					foreach (int num7 in records)
					{
						if (!this.IsNull(num7))
						{
							sqlDouble2 += this._values[num7].ToSqlDouble();
							sqlDouble3 += this._values[num7].ToSqlDouble() * this._values[num7].ToSqlDouble();
							num6++;
						}
					}
					if (num6 <= 1)
					{
						return this._nullValue;
					}
					sqlDouble = (double)num6 * sqlDouble3 - sqlDouble2 * sqlDouble2;
					SqlBoolean sqlBoolean = sqlDouble / (sqlDouble2 * sqlDouble2) < 1E-15;
					if (sqlBoolean ? sqlBoolean : (sqlBoolean | (sqlDouble < 0.0)))
					{
						sqlDouble = 0.0;
					}
					else
					{
						sqlDouble /= (double)(num6 * (num6 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(sqlDouble.Value);
					}
					return sqlDouble;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlDecimal));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x000B6E4C File Offset: 0x000B504C
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x000B6E6B File Offset: 0x000B506B
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlDecimal)value);
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x000B6E84 File Offset: 0x000B5084
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlDecimal(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x000B6E9B File Offset: 0x000B509B
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000B6EB5 File Offset: 0x000B50B5
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000B6EC8 File Offset: 0x000B50C8
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000B6EDB File Offset: 0x000B50DB
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlDecimal(value);
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x000B6EF0 File Offset: 0x000B50F0
		public override void SetCapacity(int capacity)
		{
			SqlDecimal[] array = new SqlDecimal[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x000B6F30 File Offset: 0x000B5130
		public override object ConvertXmlToObject(string s)
		{
			SqlDecimal sqlDecimal = default(SqlDecimal);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlDecimal;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlDecimal)xmlSerializable;
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x000B6F98 File Offset: 0x000B5198
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x000B6FE8 File Offset: 0x000B51E8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlDecimal[recordCount];
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000B6FF0 File Offset: 0x000B51F0
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlDecimal[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x000B701A File Offset: 0x000B521A
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlDecimal[])store;
		}

		// Token: 0x04001936 RID: 6454
		private SqlDecimal[] _values;
	}
}
