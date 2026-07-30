using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000373 RID: 883
	internal sealed class SqlUdtStorage : DataStorage
	{
		// Token: 0x060029EB RID: 10731 RVA: 0x000BA62B File Offset: 0x000B882B
		public SqlUdtStorage(DataColumn column, Type type)
			: this(column, type, SqlUdtStorage.GetStaticNullForUdtType(type))
		{
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x000BA63C File Offset: 0x000B883C
		private SqlUdtStorage(DataColumn column, Type type, object nullValue)
			: base(column, type, nullValue, nullValue, typeof(ICloneable).IsAssignableFrom(type), DataStorage.GetStorageType(type))
		{
			this._implementsIXmlSerializable = typeof(IXmlSerializable).IsAssignableFrom(type);
			this._implementsIComparable = typeof(IComparable).IsAssignableFrom(type);
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000BA698 File Offset: 0x000B8898
		internal static object GetStaticNullForUdtType(Type type)
		{
			object obj;
			if (!SqlUdtStorage.s_typeToNull.TryGetValue(type, out obj))
			{
				PropertyInfo property = type.GetProperty("Null", BindingFlags.Static | BindingFlags.Public);
				if (property != null)
				{
					obj = property.GetValue(null, null);
				}
				else
				{
					FieldInfo field = type.GetField("Null", BindingFlags.Static | BindingFlags.Public);
					if (!(field != null))
					{
						throw ExceptionBuilder.INullableUDTwithoutStaticNull(type.AssemblyQualifiedName);
					}
					obj = field.GetValue(null);
				}
				Dictionary<Type, object> dictionary = SqlUdtStorage.s_typeToNull;
				lock (dictionary)
				{
					SqlUdtStorage.s_typeToNull[type] = obj;
				}
			}
			return obj;
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000BA740 File Offset: 0x000B8940
		public override bool IsNull(int record)
		{
			return ((INullable)this._values[record]).IsNull;
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x000A728C File Offset: 0x000A548C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x000BA754 File Offset: 0x000B8954
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.CompareValueTo(recordNo1, this._values[recordNo2]);
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000BA768 File Offset: 0x000B8968
		public override int CompareValueTo(int recordNo1, object value)
		{
			if (DBNull.Value == value)
			{
				value = this._nullValue;
			}
			if (this._implementsIComparable)
			{
				return ((IComparable)this._values[recordNo1]).CompareTo(value);
			}
			if (this._nullValue != value)
			{
				throw ExceptionBuilder.IComparableNotImplemented(this._dataType.AssemblyQualifiedName);
			}
			if (!((INullable)this._values[recordNo1]).IsNull)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x000BA7D2 File Offset: 0x000B89D2
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x000BA7EC File Offset: 0x000B89EC
		public override object Get(int recordNo)
		{
			return this._values[recordNo];
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x000BA7F8 File Offset: 0x000B89F8
		public override void Set(int recordNo, object value)
		{
			if (DBNull.Value == value)
			{
				this._values[recordNo] = this._nullValue;
				base.SetNullBit(recordNo, true);
				return;
			}
			if (value == null)
			{
				if (this._isValueType)
				{
					throw ExceptionBuilder.StorageSetFailed();
				}
				this._values[recordNo] = this._nullValue;
				base.SetNullBit(recordNo, true);
				return;
			}
			else
			{
				if (!this._dataType.IsInstanceOfType(value))
				{
					throw ExceptionBuilder.StorageSetFailed();
				}
				this._values[recordNo] = value;
				base.SetNullBit(recordNo, false);
				return;
			}
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x000BA874 File Offset: 0x000B8A74
		public override void SetCapacity(int capacity)
		{
			object[] array = new object[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x000BA8BC File Offset: 0x000B8ABC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object ConvertXmlToObject(string s)
		{
			if (this._implementsIXmlSerializable)
			{
				object obj = Activator.CreateInstance(this._dataType, true);
				using (XmlTextReader xmlTextReader = new XmlTextReader(new StringReader("<col>" + s + "</col>")))
				{
					((IXmlSerializable)obj).ReadXml(xmlTextReader);
				}
				return obj;
			}
			StringReader stringReader = new StringReader(s);
			return ObjectStorage.GetXmlSerializer(this._dataType).Deserialize(stringReader);
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x000BA93C File Offset: 0x000B8B3C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
		{
			if (xmlAttrib == null)
			{
				string text = xmlReader.GetAttribute("InstanceType", "urn:schemas-microsoft-com:xml-msdata");
				if (text == null)
				{
					string attribute = xmlReader.GetAttribute("InstanceType", "http://www.w3.org/2001/XMLSchema-instance");
					if (attribute != null)
					{
						text = XSDSchema.XsdtoClr(attribute).FullName;
					}
				}
				object obj = Activator.CreateInstance((text == null) ? this._dataType : Type.GetType(text), true);
				((IXmlSerializable)obj).ReadXml(xmlReader);
				return obj;
			}
			return ObjectStorage.GetXmlSerializer(this._dataType, xmlAttrib).Deserialize(xmlReader);
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x000BA9B8 File Offset: 0x000B8BB8
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			if (this._implementsIXmlSerializable)
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
				{
					((IXmlSerializable)value).WriteXml(xmlTextWriter);
					goto IL_0045;
				}
			}
			ObjectStorage.GetXmlSerializer(value.GetType()).Serialize(stringWriter, value);
			IL_0045:
			return stringWriter.ToString();
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x000BAA20 File Offset: 0x000B8C20
		public override void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
		{
			if (xmlAttrib == null)
			{
				((IXmlSerializable)value).WriteXml(xmlWriter);
				return;
			}
			ObjectStorage.GetXmlSerializer(this._dataType, xmlAttrib).Serialize(xmlWriter, value);
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x000B3855 File Offset: 0x000B1A55
		protected override object GetEmptyStorage(int recordCount)
		{
			return new object[recordCount];
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x000BAA45 File Offset: 0x000B8C45
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((object[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x000BAA67 File Offset: 0x000B8C67
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (object[])store;
		}

		// Token: 0x04001961 RID: 6497
		private object[] _values;

		// Token: 0x04001962 RID: 6498
		private readonly bool _implementsIXmlSerializable;

		// Token: 0x04001963 RID: 6499
		private readonly bool _implementsIComparable;

		// Token: 0x04001964 RID: 6500
		private static readonly Dictionary<Type, object> s_typeToNull = new Dictionary<Type, object>();
	}
}
