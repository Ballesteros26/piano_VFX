using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000369 RID: 873
	internal sealed class SqlInt16Storage : DataStorage
	{
		// Token: 0x06002970 RID: 10608 RVA: 0x000B78F8 File Offset: 0x000B5AF8
		public SqlInt16Storage(DataColumn column)
			: base(column, typeof(SqlInt16), SqlInt16.Null, SqlInt16.Null, StorageType.SqlInt16)
		{
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000B7924 File Offset: 0x000B5B24
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
					SqlInt64 sqlInt2 = 0L;
					int num2 = 0;
					foreach (int num3 in records)
					{
						if (!this.IsNull(num3))
						{
							sqlInt2 += this._values[num3].ToSqlInt64();
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						0;
						return (sqlInt2 / (long)num2).ToSqlInt16();
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					SqlInt16 sqlInt3 = SqlInt16.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlInt16.LessThan(this._values[num4], sqlInt3).IsTrue)
							{
								sqlInt3 = this._values[num4];
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
				case AggregateType.Max:
				{
					SqlInt16 sqlInt4 = SqlInt16.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlInt16.GreaterThan(this._values[num5], sqlInt4).IsTrue)
							{
								sqlInt4 = this._values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlInt4;
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
				throw ExprException.Overflow(typeof(SqlInt16));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000B7D60 File Offset: 0x000B5F60
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x000B7D7F File Offset: 0x000B5F7F
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlInt16)value);
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000B7D98 File Offset: 0x000B5F98
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlInt16(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x000B7DAF File Offset: 0x000B5FAF
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x000B7DC9 File Offset: 0x000B5FC9
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000B7DDC File Offset: 0x000B5FDC
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000B7DEF File Offset: 0x000B5FEF
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlInt16(value);
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000B7E04 File Offset: 0x000B6004
		public override void SetCapacity(int capacity)
		{
			SqlInt16[] array = new SqlInt16[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x000B7E44 File Offset: 0x000B6044
		public override object ConvertXmlToObject(string s)
		{
			SqlInt16 sqlInt = default(SqlInt16);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlInt;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlInt16)xmlSerializable;
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000B7EAC File Offset: 0x000B60AC
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x000B7EFC File Offset: 0x000B60FC
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlInt16[recordCount];
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x000B7F04 File Offset: 0x000B6104
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlInt16[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x000B7F2E File Offset: 0x000B612E
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlInt16[])store;
		}

		// Token: 0x04001939 RID: 6457
		private SqlInt16[] _values;
	}
}
