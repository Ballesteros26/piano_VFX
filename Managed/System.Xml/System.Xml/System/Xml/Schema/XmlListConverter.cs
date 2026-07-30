using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x0200049E RID: 1182
	internal class XmlListConverter : XmlBaseConverter
	{
		// Token: 0x06002FCE RID: 12238 RVA: 0x00113BEA File Offset: 0x00111DEA
		protected XmlListConverter(XmlBaseConverter atomicConverter)
			: base(atomicConverter)
		{
			this.atomicConverter = atomicConverter;
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x00113BFA File Offset: 0x00111DFA
		protected XmlListConverter(XmlBaseConverter atomicConverter, Type clrTypeDefault)
			: base(atomicConverter, clrTypeDefault)
		{
			this.atomicConverter = atomicConverter;
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x0010F8F4 File Offset: 0x0010DAF4
		protected XmlListConverter(XmlSchemaType schemaType)
			: base(schemaType)
		{
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x00113C0B File Offset: 0x00111E0B
		public static XmlValueConverter Create(XmlValueConverter atomicConverter)
		{
			if (atomicConverter == XmlUntypedConverter.Untyped)
			{
				return XmlUntypedConverter.UntypedList;
			}
			if (atomicConverter == XmlAnyConverter.Item)
			{
				return XmlAnyListConverter.ItemList;
			}
			if (atomicConverter == XmlAnyConverter.AnyAtomic)
			{
				return XmlAnyListConverter.AnyAtomicList;
			}
			return new XmlListConverter((XmlBaseConverter)atomicConverter);
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x00113C42 File Offset: 0x00111E42
		public override object ChangeType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x00113C70 File Offset: 0x00111E70
		protected override object ChangeListType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			Type type = value.GetType();
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (!(value is IEnumerable) || !this.IsListType(destinationType))
			{
				throw this.CreateInvalidClrMappingException(type, destinationType);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				if (type == XmlBaseConverter.StringType)
				{
					return value;
				}
				return this.ListAsString((IEnumerable)value, nsResolver);
			}
			else
			{
				if (type == XmlBaseConverter.StringType)
				{
					value = this.StringAsList((string)value);
				}
				if (destinationType.IsArray)
				{
					Type elementType = destinationType.GetElementType();
					if (elementType == XmlBaseConverter.ObjectType)
					{
						return this.ToArray<object>(value, nsResolver);
					}
					if (type == destinationType)
					{
						return value;
					}
					if (elementType == XmlBaseConverter.BooleanType)
					{
						return this.ToArray<bool>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.ByteType)
					{
						return this.ToArray<byte>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.ByteArrayType)
					{
						return this.ToArray<byte[]>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.DateTimeType)
					{
						return this.ToArray<DateTime>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.DateTimeOffsetType)
					{
						return this.ToArray<DateTimeOffset>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.DecimalType)
					{
						return this.ToArray<decimal>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.DoubleType)
					{
						return this.ToArray<double>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.Int16Type)
					{
						return this.ToArray<short>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.Int32Type)
					{
						return this.ToArray<int>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.Int64Type)
					{
						return this.ToArray<long>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.SByteType)
					{
						return this.ToArray<sbyte>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.SingleType)
					{
						return this.ToArray<float>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.StringType)
					{
						return this.ToArray<string>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.TimeSpanType)
					{
						return this.ToArray<TimeSpan>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.UInt16Type)
					{
						return this.ToArray<ushort>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.UInt32Type)
					{
						return this.ToArray<uint>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.UInt64Type)
					{
						return this.ToArray<ulong>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.UriType)
					{
						return this.ToArray<Uri>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.XmlAtomicValueType)
					{
						return this.ToArray<XmlAtomicValue>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.XmlQualifiedNameType)
					{
						return this.ToArray<XmlQualifiedName>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.XPathItemType)
					{
						return this.ToArray<XPathItem>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.XPathNavigatorType)
					{
						return this.ToArray<XPathNavigator>(value, nsResolver);
					}
					throw this.CreateInvalidClrMappingException(type, destinationType);
				}
				else
				{
					if (type == base.DefaultClrType && type != XmlBaseConverter.ObjectArrayType)
					{
						return value;
					}
					return this.ToList(value, nsResolver);
				}
			}
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x00113F3D File Offset: 0x0011213D
		private bool IsListType(Type type)
		{
			return type == XmlBaseConverter.IListType || type == XmlBaseConverter.ICollectionType || type == XmlBaseConverter.IEnumerableType || type == XmlBaseConverter.StringType || type.IsArray;
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x00113F7C File Offset: 0x0011217C
		private T[] ToArray<T>(object list, IXmlNamespaceResolver nsResolver)
		{
			IList list2 = list as IList;
			if (list2 != null)
			{
				T[] array = new T[list2.Count];
				for (int i = 0; i < list2.Count; i++)
				{
					array[i] = (T)((object)this.atomicConverter.ChangeType(list2[i], typeof(T), nsResolver));
				}
				return array;
			}
			IEnumerable enumerable = list as IEnumerable;
			List<T> list3 = new List<T>();
			foreach (object obj in enumerable)
			{
				list3.Add((T)((object)this.atomicConverter.ChangeType(obj, typeof(T), nsResolver)));
			}
			return list3.ToArray();
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x00114054 File Offset: 0x00112254
		private IList ToList(object list, IXmlNamespaceResolver nsResolver)
		{
			IList list2 = list as IList;
			if (list2 != null)
			{
				object[] array = new object[list2.Count];
				for (int i = 0; i < list2.Count; i++)
				{
					array[i] = this.atomicConverter.ChangeType(list2[i], XmlBaseConverter.ObjectType, nsResolver);
				}
				return array;
			}
			IEnumerable enumerable = list as IEnumerable;
			List<object> list3 = new List<object>();
			foreach (object obj in enumerable)
			{
				list3.Add(this.atomicConverter.ChangeType(obj, XmlBaseConverter.ObjectType, nsResolver));
			}
			return list3;
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x00114110 File Offset: 0x00112310
		private List<string> StringAsList(string value)
		{
			return new List<string>(XmlConvert.SplitString(value));
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x00114120 File Offset: 0x00112320
		private string ListAsString(IEnumerable list, IXmlNamespaceResolver nsResolver)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in list)
			{
				if (obj != null)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(' ');
					}
					stringBuilder.Append(this.atomicConverter.ToString(obj, nsResolver));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x0011419C File Offset: 0x0011239C
		private new Exception CreateInvalidClrMappingException(Type sourceType, Type destinationType)
		{
			if (sourceType == destinationType)
			{
				return new InvalidCastException(Res.GetString("Xml type 'List of {0}' does not support Clr type '{1}'.", new object[] { base.XmlTypeName, sourceType.Name }));
			}
			return new InvalidCastException(Res.GetString("Xml type 'List of {0}' does not support a conversion from Clr type '{1}' to Clr type '{2}'.", new object[] { base.XmlTypeName, sourceType.Name, destinationType.Name }));
		}

		// Token: 0x04001EEC RID: 7916
		protected XmlValueConverter atomicConverter;
	}
}
