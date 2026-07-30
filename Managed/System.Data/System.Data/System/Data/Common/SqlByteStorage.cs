using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000362 RID: 866
	internal sealed class SqlByteStorage : DataStorage
	{
		// Token: 0x06002909 RID: 10505 RVA: 0x000B5BBC File Offset: 0x000B3DBC
		public SqlByteStorage(DataColumn column)
			: base(column, typeof(SqlByte), SqlByte.Null, SqlByte.Null, StorageType.SqlByte)
		{
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x000B5BE8 File Offset: 0x000B3DE8
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
						return (sqlInt2 / (long)num2).ToSqlByte();
					}
					return this._nullValue;
				}
				case AggregateType.Min:
				{
					SqlByte sqlByte = SqlByte.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlByte.LessThan(this._values[num4], sqlByte).IsTrue)
							{
								sqlByte = this._values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlByte;
					}
					return this._nullValue;
				}
				case AggregateType.Max:
				{
					SqlByte sqlByte2 = SqlByte.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlByte.GreaterThan(this._values[num5], sqlByte2).IsTrue)
							{
								sqlByte2 = this._values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlByte2;
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
				throw ExprException.Overflow(typeof(SqlByte));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x000B6024 File Offset: 0x000B4224
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x000B6043 File Offset: 0x000B4243
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlByte)value);
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x000B605C File Offset: 0x000B425C
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlByte(value);
			}
			return this._nullValue;
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x000B6073 File Offset: 0x000B4273
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x000B608D File Offset: 0x000B428D
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x000B60A0 File Offset: 0x000B42A0
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x000B60B3 File Offset: 0x000B42B3
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlByte(value);
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x000B60C8 File Offset: 0x000B42C8
		public override void SetCapacity(int capacity)
		{
			SqlByte[] array = new SqlByte[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x000B6108 File Offset: 0x000B4308
		public override object ConvertXmlToObject(string s)
		{
			SqlByte sqlByte = default(SqlByte);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlByte;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlByte)xmlSerializable;
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x000B6170 File Offset: 0x000B4370
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x000B61C0 File Offset: 0x000B43C0
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlByte[recordCount];
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x000B61C8 File Offset: 0x000B43C8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlByte[])store)[storeIndex] = this._values[record];
			nullbits.Set(record, this.IsNull(record));
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000B61F1 File Offset: 0x000B43F1
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlByte[])store;
		}

		// Token: 0x04001932 RID: 6450
		private SqlByte[] _values;
	}
}
