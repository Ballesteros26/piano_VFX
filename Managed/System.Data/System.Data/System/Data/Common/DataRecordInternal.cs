using System;
using System.ComponentModel;
using System.Data.ProviderBase;

namespace System.Data.Common
{
	// Token: 0x02000334 RID: 820
	internal sealed class DataRecordInternal : DbDataRecord, ICustomTypeDescriptor
	{
		// Token: 0x060025C2 RID: 9666 RVA: 0x000AB955 File Offset: 0x000A9B55
		internal DataRecordInternal(SchemaInfo[] schemaInfo, object[] values, PropertyDescriptorCollection descriptors, FieldNameLookup fieldNameLookup)
		{
			this._schemaInfo = schemaInfo;
			this._values = values;
			this._propertyDescriptors = descriptors;
			this._fieldNameLookup = fieldNameLookup;
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060025C3 RID: 9667 RVA: 0x000AB97A File Offset: 0x000A9B7A
		public override int FieldCount
		{
			get
			{
				return this._schemaInfo.Length;
			}
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x000AB984 File Offset: 0x000A9B84
		public override int GetValues(object[] values)
		{
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = ((values.Length < this._schemaInfo.Length) ? values.Length : this._schemaInfo.Length);
			for (int i = 0; i < num; i++)
			{
				values[i] = this._values[i];
			}
			return num;
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x000AB9D2 File Offset: 0x000A9BD2
		public override string GetName(int i)
		{
			return this._schemaInfo[i].name;
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x000AB9E5 File Offset: 0x000A9BE5
		public override object GetValue(int i)
		{
			return this._values[i];
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x000AB9EF File Offset: 0x000A9BEF
		public override string GetDataTypeName(int i)
		{
			return this._schemaInfo[i].typeName;
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x000ABA02 File Offset: 0x000A9C02
		public override Type GetFieldType(int i)
		{
			return this._schemaInfo[i].type;
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x000ABA15 File Offset: 0x000A9C15
		public override int GetOrdinal(string name)
		{
			return this._fieldNameLookup.GetOrdinal(name);
		}

		// Token: 0x1700067E RID: 1662
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x1700067F RID: 1663
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x000ABA3B File Offset: 0x000A9C3B
		public override bool GetBoolean(int i)
		{
			return (bool)this._values[i];
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000ABA4A File Offset: 0x000A9C4A
		public override byte GetByte(int i)
		{
			return (byte)this._values[i];
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000ABA5C File Offset: 0x000A9C5C
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			int num = 0;
			byte[] array = (byte[])this._values[i];
			num = array.Length;
			if (dataIndex > 2147483647L)
			{
				throw ADP.InvalidSourceBufferIndex(num, dataIndex, "dataIndex");
			}
			int num2 = (int)dataIndex;
			if (buffer == null)
			{
				return (long)num;
			}
			try
			{
				if (num2 < num)
				{
					if (num2 + length > num)
					{
						num -= num2;
					}
					else
					{
						num = length;
					}
				}
				Array.Copy(array, num2, buffer, bufferIndex, num);
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				num = array.Length;
				if (length < 0)
				{
					throw ADP.InvalidDataLength((long)length);
				}
				if (bufferIndex < 0 || bufferIndex >= buffer.Length)
				{
					throw ADP.InvalidDestinationBufferIndex(length, bufferIndex, "bufferIndex");
				}
				if (dataIndex < 0L || dataIndex >= (long)num)
				{
					throw ADP.InvalidSourceBufferIndex(length, dataIndex, "dataIndex");
				}
				if (num + bufferIndex > buffer.Length)
				{
					throw ADP.InvalidBufferSizeOrIndex(num, bufferIndex);
				}
			}
			return (long)num;
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000ABB40 File Offset: 0x000A9D40
		public override char GetChar(int i)
		{
			return ((string)this._values[i])[0];
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000ABB58 File Offset: 0x000A9D58
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			char[] array = ((string)this._values[i]).ToCharArray();
			int num = array.Length;
			if (dataIndex > 2147483647L)
			{
				throw ADP.InvalidSourceBufferIndex(num, dataIndex, "dataIndex");
			}
			int num2 = (int)dataIndex;
			if (buffer == null)
			{
				return (long)num;
			}
			try
			{
				if (num2 < num)
				{
					if (num2 + length > num)
					{
						num -= num2;
					}
					else
					{
						num = length;
					}
				}
				Array.Copy(array, num2, buffer, bufferIndex, num);
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				num = array.Length;
				if (length < 0)
				{
					throw ADP.InvalidDataLength((long)length);
				}
				if (bufferIndex < 0 || bufferIndex >= buffer.Length)
				{
					throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
				}
				if (num2 < 0 || num2 >= num)
				{
					throw ADP.InvalidSourceBufferIndex(num, dataIndex, "dataIndex");
				}
				if (num + bufferIndex > buffer.Length)
				{
					throw ADP.InvalidBufferSizeOrIndex(num, bufferIndex);
				}
			}
			return (long)num;
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x000ABC40 File Offset: 0x000A9E40
		public override Guid GetGuid(int i)
		{
			return (Guid)this._values[i];
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x000ABC4F File Offset: 0x000A9E4F
		public override short GetInt16(int i)
		{
			return (short)this._values[i];
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x000ABC5E File Offset: 0x000A9E5E
		public override int GetInt32(int i)
		{
			return (int)this._values[i];
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x000ABC6D File Offset: 0x000A9E6D
		public override long GetInt64(int i)
		{
			return (long)this._values[i];
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000ABC7C File Offset: 0x000A9E7C
		public override float GetFloat(int i)
		{
			return (float)this._values[i];
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x000ABC8B File Offset: 0x000A9E8B
		public override double GetDouble(int i)
		{
			return (double)this._values[i];
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000ABC9A File Offset: 0x000A9E9A
		public override string GetString(int i)
		{
			return (string)this._values[i];
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x000ABCA9 File Offset: 0x000A9EA9
		public override decimal GetDecimal(int i)
		{
			return (decimal)this._values[i];
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000ABCB8 File Offset: 0x000A9EB8
		public override DateTime GetDateTime(int i)
		{
			return (DateTime)this._values[i];
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x000ABCC8 File Offset: 0x000A9EC8
		public override bool IsDBNull(int i)
		{
			object obj = this._values[i];
			return obj == null || Convert.IsDBNull(obj);
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x0001A01E File Offset: 0x0001821E
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x00004526 File Offset: 0x00002726
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x00004526 File Offset: 0x00002726
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x00004526 File Offset: 0x00002726
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x00004526 File Offset: 0x00002726
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x00004526 File Offset: 0x00002726
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x00004526 File Offset: 0x00002726
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x0001A026 File Offset: 0x00018226
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x0001A026 File Offset: 0x00018226
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x0001A02E File Offset: 0x0001822E
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x000ABCE9 File Offset: 0x000A9EE9
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (this._propertyDescriptors == null)
			{
				this._propertyDescriptors = new PropertyDescriptorCollection(null);
			}
			return this._propertyDescriptors;
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x00005D82 File Offset: 0x00003F82
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x0400184C RID: 6220
		private SchemaInfo[] _schemaInfo;

		// Token: 0x0400184D RID: 6221
		private object[] _values;

		// Token: 0x0400184E RID: 6222
		private PropertyDescriptorCollection _propertyDescriptors;

		// Token: 0x0400184F RID: 6223
		private FieldNameLookup _fieldNameLookup;
	}
}
