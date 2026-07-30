using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000368 RID: 872
	internal sealed class SqlGuidStorage : DataStorage
	{
		// Token: 0x06002961 RID: 10593 RVA: 0x000B7660 File Offset: 0x000B5860
		public SqlGuidStorage(DataColumn column)
			: base(column, typeof(SqlGuid), SqlGuid.Null, SqlGuid.Null, StorageType.SqlGuid)
		{
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x000B768C File Offset: 0x000B588C
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
				throw ExprException.Overflow(typeof(SqlGuid));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x000B771C File Offset: 0x000B591C
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this._values[recordNo1].CompareTo(this._values[recordNo2]);
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000B773B File Offset: 0x000B593B
		public override int CompareValueTo(int recordNo, object value)
		{
			return this._values[recordNo].CompareTo((SqlGuid)value);
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x000B7754 File Offset: 0x000B5954
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlGuid(value);
			}
			return this._nullValue;
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x000B776B File Offset: 0x000B596B
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x000B7785 File Offset: 0x000B5985
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x000B7798 File Offset: 0x000B5998
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000B77AB File Offset: 0x000B59AB
		public override void Set(int record, object value)
		{
			this._values[record] = SqlConvert.ConvertToSqlGuid(value);
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x000B77C0 File Offset: 0x000B59C0
		public override void SetCapacity(int capacity)
		{
			SqlGuid[] array = new SqlGuid[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000B7800 File Offset: 0x000B5A00
		public override object ConvertXmlToObject(string s)
		{
			SqlGuid sqlGuid = default(SqlGuid);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlGuid;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlGuid)xmlSerializable;
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x000B7868 File Offset: 0x000B5A68
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x000B78B8 File Offset: 0x000B5AB8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlGuid[recordCount];
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000B78C0 File Offset: 0x000B5AC0
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlGuid[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000B78EA File Offset: 0x000B5AEA
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlGuid[])store;
		}

		// Token: 0x04001938 RID: 6456
		private SqlGuid[] _values;
	}
}
