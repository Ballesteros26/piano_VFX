using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200036B RID: 875
	internal sealed class SqlInt64Storage : DataStorage
	{
		// Token: 0x0600298E RID: 10638 RVA: 0x000B8580 File Offset: 0x000B6780
		public SqlInt64Storage(DataColumn column)
			: base(column, typeof(SqlInt64), SqlInt64.Null, SqlInt64.Null, StorageType.SqlInt64)
		{
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x000B85AC File Offset: 0x000B67AC
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					SqlInt64 sqlInt = 0L;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlInt += this._values[num];
							flag = true;
						}
					}
					if (flag)
					{
						return sqlInt;
					}
					return this._nullValue;
				}
				case AggregateType.Mean:
				{
					SqlDecimal sqlDecimal = 0L;
					int num2 = 0;
					foreach (int num3 in records)
					{
						if (!this.IsNull(num3))
						{
							sqlDecimal += this._values[num3].ToSqlDecimal();
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						0L;
						return (sqlDecimal / (long)num2).ToSqlInt64();
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					SqlInt64 sqlInt2 = SqlInt64.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlInt64.LessThan(this._values[num4], sqlInt2).IsTrue)
							{
								sqlInt2 = this._values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlInt2;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlInt64 sqlInt3 = SqlInt64.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlInt64.GreaterThan(this._values[num5], sqlInt3).IsTrue)
							{
								sqlInt3 = this._values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlInt3;
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
				throw ExprException.Overflow(typeof(SqlInt64));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x000B89E4 File Offset: 0x000B6BE4
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x000B8A03 File Offset: 0x000B6C03
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlInt64)value);
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x000B8A1C File Offset: 0x000B6C1C
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlInt64(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000B8A33 File Offset: 0x000B6C33
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000B8A4D File Offset: 0x000B6C4D
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000B8A60 File Offset: 0x000B6C60
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000B8A73 File Offset: 0x000B6C73
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlInt64(value);
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x000B8A88 File Offset: 0x000B6C88
		public override void SetCapacity(int capacity)
		{
			SqlInt64[] array = new SqlInt64[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000B8AC8 File Offset: 0x000B6CC8
		public override object ConvertXmlToObject(string s)
		{
			SqlInt64 sqlInt = default(SqlInt64);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlInt;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlInt64)xmlSerializable;
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x000B8B30 File Offset: 0x000B6D30
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000B8B80 File Offset: 0x000B6D80
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlInt64[recordCount];
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x000B8B88 File Offset: 0x000B6D88
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlInt64[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000B8BB2 File Offset: 0x000B6DB2
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlInt64[])store;
		}

		// Token: 0x0400193B RID: 6459
		private SqlInt64[] _values;
	}
}
