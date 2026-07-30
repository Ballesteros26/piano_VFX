using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000625 RID: 1573
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class XsltConvert
	{
		// Token: 0x06003D82 RID: 15746 RVA: 0x00154220 File Offset: 0x00152420
		public static bool ToBoolean(XPathItem item)
		{
			if (item.IsNode)
			{
				return true;
			}
			Type valueType = item.ValueType;
			if (valueType == XsltConvert.StringType)
			{
				return item.Value.Length != 0;
			}
			if (valueType == XsltConvert.DoubleType)
			{
				double valueAsDouble = item.ValueAsDouble;
				return valueAsDouble < 0.0 || 0.0 < valueAsDouble;
			}
			return item.ValueAsBoolean;
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x0015428F File Offset: 0x0015248F
		public static bool ToBoolean(IList<XPathItem> listItems)
		{
			return listItems.Count != 0 && XsltConvert.ToBoolean(listItems[0]);
		}

		// Token: 0x06003D84 RID: 15748 RVA: 0x001542A7 File Offset: 0x001524A7
		public static double ToDouble(string value)
		{
			return XPathConvert.StringToDouble(value);
		}

		// Token: 0x06003D85 RID: 15749 RVA: 0x001542B0 File Offset: 0x001524B0
		public static double ToDouble(XPathItem item)
		{
			if (item.IsNode)
			{
				return XPathConvert.StringToDouble(item.Value);
			}
			Type valueType = item.ValueType;
			if (valueType == XsltConvert.StringType)
			{
				return XPathConvert.StringToDouble(item.Value);
			}
			if (valueType == XsltConvert.DoubleType)
			{
				return item.ValueAsDouble;
			}
			if (!item.ValueAsBoolean)
			{
				return 0.0;
			}
			return 1.0;
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x00154320 File Offset: 0x00152520
		public static double ToDouble(IList<XPathItem> listItems)
		{
			if (listItems.Count == 0)
			{
				return double.NaN;
			}
			return XsltConvert.ToDouble(listItems[0]);
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x00154340 File Offset: 0x00152540
		public static XPathNavigator ToNode(XPathItem item)
		{
			if (!item.IsNode)
			{
				XPathDocument xpathDocument = new XPathDocument();
				XmlRawWriter xmlRawWriter = xpathDocument.LoadFromWriter(XPathDocument.LoadFlags.AtomizeNames, string.Empty);
				xmlRawWriter.WriteString(XsltConvert.ToString(item));
				xmlRawWriter.Close();
				return xpathDocument.CreateNavigator();
			}
			RtfNavigator rtfNavigator = item as RtfNavigator;
			if (rtfNavigator != null)
			{
				return rtfNavigator.ToNavigator();
			}
			return (XPathNavigator)item;
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x00154394 File Offset: 0x00152594
		public static XPathNavigator ToNode(IList<XPathItem> listItems)
		{
			if (listItems.Count == 1)
			{
				return XsltConvert.ToNode(listItems[0]);
			}
			throw new XslTransformException("Cannot convert a node-set which contains zero nodes or more than one node to a single node.", new string[] { string.Empty });
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x001543C4 File Offset: 0x001525C4
		public static IList<XPathNavigator> ToNodeSet(XPathItem item)
		{
			return new XmlQueryNodeSequence(XsltConvert.ToNode(item));
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x001543D1 File Offset: 0x001525D1
		public static IList<XPathNavigator> ToNodeSet(IList<XPathItem> listItems)
		{
			if (listItems.Count == 1)
			{
				return new XmlQueryNodeSequence(XsltConvert.ToNode(listItems[0]));
			}
			return XmlILStorageConverter.ItemsToNavigators(listItems);
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x001543F4 File Offset: 0x001525F4
		public static string ToString(double value)
		{
			return XPathConvert.DoubleToString(value);
		}

		// Token: 0x06003D8C RID: 15756 RVA: 0x001543FC File Offset: 0x001525FC
		public static string ToString(XPathItem item)
		{
			if (!item.IsNode && item.ValueType == XsltConvert.DoubleType)
			{
				return XPathConvert.DoubleToString(item.ValueAsDouble);
			}
			return item.Value;
		}

		// Token: 0x06003D8D RID: 15757 RVA: 0x0015442A File Offset: 0x0015262A
		public static string ToString(IList<XPathItem> listItems)
		{
			if (listItems.Count == 0)
			{
				return string.Empty;
			}
			return XsltConvert.ToString(listItems[0]);
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x00154448 File Offset: 0x00152648
		public static string ToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime).ToString();
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x0015446A File Offset: 0x0015266A
		public static double ToDouble(decimal value)
		{
			return (double)value;
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x00154473 File Offset: 0x00152673
		public static double ToDouble(int value)
		{
			return (double)value;
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x00154473 File Offset: 0x00152673
		public static double ToDouble(long value)
		{
			return (double)value;
		}

		// Token: 0x06003D92 RID: 15762 RVA: 0x00154477 File Offset: 0x00152677
		public static decimal ToDecimal(double value)
		{
			return (decimal)value;
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x0015447F File Offset: 0x0015267F
		public static int ToInt(double value)
		{
			return checked((int)value);
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x00154483 File Offset: 0x00152683
		public static long ToLong(double value)
		{
			return checked((long)value);
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x0010F70E File Offset: 0x0010D90E
		public static DateTime ToDateTime(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.AllXsd);
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x00154488 File Offset: 0x00152688
		internal static XmlAtomicValue ConvertToType(XmlAtomicValue value, XmlQueryType destinationType)
		{
			XmlTypeCode xmlTypeCode = destinationType.TypeCode;
			switch (xmlTypeCode)
			{
			case XmlTypeCode.String:
				switch (value.XmlType.TypeCode)
				{
				case XmlTypeCode.String:
				case XmlTypeCode.Boolean:
				case XmlTypeCode.Double:
					return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToString(value));
				case XmlTypeCode.DateTime:
					return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToString(value.ValueAsDateTime));
				}
				break;
			case XmlTypeCode.Boolean:
				xmlTypeCode = value.XmlType.TypeCode;
				if (xmlTypeCode - XmlTypeCode.String <= 1 || xmlTypeCode == XmlTypeCode.Double)
				{
					return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToBoolean(value));
				}
				break;
			case XmlTypeCode.Decimal:
				if (value.XmlType.TypeCode == XmlTypeCode.Double)
				{
					return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToDecimal(value.ValueAsDouble));
				}
				break;
			case XmlTypeCode.Float:
			case XmlTypeCode.Duration:
				break;
			case XmlTypeCode.Double:
				xmlTypeCode = value.XmlType.TypeCode;
				switch (xmlTypeCode)
				{
				case XmlTypeCode.String:
				case XmlTypeCode.Boolean:
				case XmlTypeCode.Double:
					return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToDouble(value));
				case XmlTypeCode.Decimal:
					return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToDouble((decimal)value.ValueAs(XsltConvert.DecimalType, null)));
				case XmlTypeCode.Float:
					break;
				default:
					if (xmlTypeCode - XmlTypeCode.Long <= 1)
					{
						return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToDouble(value.ValueAsLong));
					}
					break;
				}
				break;
			case XmlTypeCode.DateTime:
				if (value.XmlType.TypeCode == XmlTypeCode.String)
				{
					return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToDateTime(value.Value));
				}
				break;
			default:
				if (xmlTypeCode - XmlTypeCode.Long <= 1)
				{
					if (value.XmlType.TypeCode == XmlTypeCode.Double)
					{
						return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToLong(value.ValueAsDouble));
					}
				}
				break;
			}
			return value;
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x00154658 File Offset: 0x00152858
		public static IList<XPathNavigator> EnsureNodeSet(IList<XPathItem> listItems)
		{
			if (listItems.Count == 1)
			{
				XPathItem xpathItem = listItems[0];
				if (!xpathItem.IsNode)
				{
					throw new XslTransformException("Expression must evaluate to a node-set.", new string[] { string.Empty });
				}
				if (xpathItem is RtfNavigator)
				{
					throw new XslTransformException("To use a result tree fragment in a path expression, first convert it to a node-set using the msxsl:node-set() function.", new string[] { string.Empty });
				}
			}
			return XmlILStorageConverter.ItemsToNavigators(listItems);
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x001546C0 File Offset: 0x001528C0
		internal static XmlQueryType InferXsltType(Type clrType)
		{
			if (clrType == XsltConvert.BooleanType)
			{
				return XmlQueryTypeFactory.BooleanX;
			}
			if (clrType == XsltConvert.ByteType)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.DecimalType)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.DateTimeType)
			{
				return XmlQueryTypeFactory.StringX;
			}
			if (clrType == XsltConvert.DoubleType)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.Int16Type)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.Int32Type)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.Int64Type)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.IXPathNavigableType)
			{
				return XmlQueryTypeFactory.NodeNotRtf;
			}
			if (clrType == XsltConvert.SByteType)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.SingleType)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.StringType)
			{
				return XmlQueryTypeFactory.StringX;
			}
			if (clrType == XsltConvert.UInt16Type)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.UInt32Type)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.UInt64Type)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.XPathNavigatorArrayType)
			{
				return XmlQueryTypeFactory.NodeSDod;
			}
			if (clrType == XsltConvert.XPathNavigatorType)
			{
				return XmlQueryTypeFactory.NodeNotRtf;
			}
			if (clrType == XsltConvert.XPathNodeIteratorType)
			{
				return XmlQueryTypeFactory.NodeSDod;
			}
			if (clrType.IsEnum)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.VoidType)
			{
				return XmlQueryTypeFactory.Empty;
			}
			return XmlQueryTypeFactory.ItemS;
		}

		// Token: 0x040027E6 RID: 10214
		internal static readonly Type BooleanType = typeof(bool);

		// Token: 0x040027E7 RID: 10215
		internal static readonly Type ByteArrayType = typeof(byte[]);

		// Token: 0x040027E8 RID: 10216
		internal static readonly Type ByteType = typeof(byte);

		// Token: 0x040027E9 RID: 10217
		internal static readonly Type DateTimeType = typeof(DateTime);

		// Token: 0x040027EA RID: 10218
		internal static readonly Type DecimalType = typeof(decimal);

		// Token: 0x040027EB RID: 10219
		internal static readonly Type DoubleType = typeof(double);

		// Token: 0x040027EC RID: 10220
		internal static readonly Type ICollectionType = typeof(ICollection);

		// Token: 0x040027ED RID: 10221
		internal static readonly Type IEnumerableType = typeof(IEnumerable);

		// Token: 0x040027EE RID: 10222
		internal static readonly Type IListType = typeof(IList);

		// Token: 0x040027EF RID: 10223
		internal static readonly Type Int16Type = typeof(short);

		// Token: 0x040027F0 RID: 10224
		internal static readonly Type Int32Type = typeof(int);

		// Token: 0x040027F1 RID: 10225
		internal static readonly Type Int64Type = typeof(long);

		// Token: 0x040027F2 RID: 10226
		internal static readonly Type IXPathNavigableType = typeof(IXPathNavigable);

		// Token: 0x040027F3 RID: 10227
		internal static readonly Type ObjectType = typeof(object);

		// Token: 0x040027F4 RID: 10228
		internal static readonly Type SByteType = typeof(sbyte);

		// Token: 0x040027F5 RID: 10229
		internal static readonly Type SingleType = typeof(float);

		// Token: 0x040027F6 RID: 10230
		internal static readonly Type StringType = typeof(string);

		// Token: 0x040027F7 RID: 10231
		internal static readonly Type TimeSpanType = typeof(TimeSpan);

		// Token: 0x040027F8 RID: 10232
		internal static readonly Type UInt16Type = typeof(ushort);

		// Token: 0x040027F9 RID: 10233
		internal static readonly Type UInt32Type = typeof(uint);

		// Token: 0x040027FA RID: 10234
		internal static readonly Type UInt64Type = typeof(ulong);

		// Token: 0x040027FB RID: 10235
		internal static readonly Type UriType = typeof(Uri);

		// Token: 0x040027FC RID: 10236
		internal static readonly Type VoidType = typeof(void);

		// Token: 0x040027FD RID: 10237
		internal static readonly Type XmlAtomicValueType = typeof(XmlAtomicValue);

		// Token: 0x040027FE RID: 10238
		internal static readonly Type XmlQualifiedNameType = typeof(XmlQualifiedName);

		// Token: 0x040027FF RID: 10239
		internal static readonly Type XPathItemType = typeof(XPathItem);

		// Token: 0x04002800 RID: 10240
		internal static readonly Type XPathNavigatorArrayType = typeof(XPathNavigator[]);

		// Token: 0x04002801 RID: 10241
		internal static readonly Type XPathNavigatorType = typeof(XPathNavigator);

		// Token: 0x04002802 RID: 10242
		internal static readonly Type XPathNodeIteratorType = typeof(XPathNodeIterator);
	}
}
