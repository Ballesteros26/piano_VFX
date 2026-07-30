using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000363 RID: 867
	internal sealed class SqlBytesStorage : DataStorage
	{
		// Token: 0x06002918 RID: 10520 RVA: 0x000B61FF File Offset: 0x000B43FF
		public SqlBytesStorage(DataColumn column)
			: base(column, typeof(SqlBytes), SqlBytes.Null, SqlBytes.Null, StorageType.SqlBytes)
		{
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000B6220 File Offset: 0x000B4420
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
				throw ExprException.Overflow(typeof(SqlBytes));
			}
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000061D5 File Offset: 0x000043D5
		public override int Compare(int recordNo1, int recordNo2)
		{
			return 0;
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x000061D5 File Offset: 0x000043D5
		public override int CompareValueTo(int recordNo, object value)
		{
			return 0;
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x000B62A8 File Offset: 0x000B44A8
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x000B62BA File Offset: 0x000B44BA
		public override object Get(int record)
		{
			return this._values[record];
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x000B62C4 File Offset: 0x000B44C4
		public override bool IsNull(int record)
		{
			return this._values[record].IsNull;
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x000B62D3 File Offset: 0x000B44D3
		public override void Set(int record, object value)
		{
			if (value == DBNull.Value || value == null)
			{
				this._values[record] = SqlBytes.Null;
				return;
			}
			this._values[record] = (SqlBytes)value;
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x000B62FC File Offset: 0x000B44FC
		public override void SetCapacity(int capacity)
		{
			SqlBytes[] array = new SqlBytes[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000B633C File Offset: 0x000B453C
		public override object ConvertXmlToObject(string s)
		{
			SqlBinary sqlBinary = default(SqlBinary);
			TextReader textReader = new StringReader("<col>" + s + "</col>");
			IXmlSerializable xmlSerializable = sqlBinary;
			using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return new SqlBytes((SqlBinary)xmlSerializable);
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x000B63A4 File Offset: 0x000B45A4
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000B63F4 File Offset: 0x000B45F4
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBytes[recordCount];
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x000B63FC File Offset: 0x000B45FC
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((SqlBytes[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x000B641E File Offset: 0x000B461E
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (SqlBytes[])store;
		}

		// Token: 0x04001933 RID: 6451
		private SqlBytes[] _values;
	}
}
