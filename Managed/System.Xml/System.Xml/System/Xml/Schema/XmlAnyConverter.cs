using System;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x0200049C RID: 1180
	internal class XmlAnyConverter : XmlBaseConverter
	{
		// Token: 0x06002FB4 RID: 12212 RVA: 0x001130AA File Offset: 0x001112AA
		protected XmlAnyConverter(XmlTypeCode typeCode)
			: base(typeCode)
		{
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x001130B3 File Offset: 0x001112B3
		public override bool ToBoolean(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsBoolean;
			}
			return (bool)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x001130F3 File Offset: 0x001112F3
		public override DateTime ToDateTime(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsDateTime;
			}
			return (DateTime)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x00113134 File Offset: 0x00111334
		public override DateTimeOffset ToDateTimeOffset(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.XmlAtomicValueType)
			{
				return (DateTimeOffset)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.DateTimeOffsetType);
			}
			return (DateTimeOffset)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x0011318C File Offset: 0x0011138C
		public override decimal ToDecimal(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.XmlAtomicValueType)
			{
				return (decimal)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.DecimalType);
			}
			return (decimal)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x001131E1 File Offset: 0x001113E1
		public override double ToDouble(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsDouble;
			}
			return (double)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x00113221 File Offset: 0x00111421
		public override int ToInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsInt;
			}
			return (int)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x00113261 File Offset: 0x00111461
		public override long ToInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsLong;
			}
			return (long)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x001132A4 File Offset: 0x001114A4
		public override float ToSingle(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.XmlAtomicValueType)
			{
				return (float)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.SingleType);
			}
			return (float)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x001132FC File Offset: 0x001114FC
		public override object ChangeType(bool value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean), value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x0011335C File Offset: 0x0011155C
		public override object ChangeType(DateTime value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.DateTime), value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x001133BC File Offset: 0x001115BC
		public override object ChangeType(DateTimeOffset value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.DateTime), value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x00113420 File Offset: 0x00111620
		public override object ChangeType(decimal value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Decimal), value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x00113484 File Offset: 0x00111684
		public override object ChangeType(double value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Double), value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x001134E4 File Offset: 0x001116E4
		public override object ChangeType(int value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Int), value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x00113544 File Offset: 0x00111744
		public override object ChangeType(long value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Long), value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x001135A4 File Offset: 0x001117A4
		public override object ChangeType(float value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Float), (double)value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x00113604 File Offset: 0x00111804
		public override object ChangeType(string value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String), value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, nsResolver);
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x0011366C File Offset: 0x0011186C
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
			Type type = value.GetType();
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.BooleanType && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsBoolean;
			}
			if (destinationType == XmlBaseConverter.DateTimeType && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsDateTime;
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(XmlBaseConverter.DateTimeOffsetType);
			}
			if (destinationType == XmlBaseConverter.DecimalType && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (decimal)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.DecimalType);
			}
			if (destinationType == XmlBaseConverter.DoubleType && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsDouble;
			}
			if (destinationType == XmlBaseConverter.Int32Type && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsInt;
			}
			if (destinationType == XmlBaseConverter.Int64Type && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsLong;
			}
			if (destinationType == XmlBaseConverter.SingleType && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (float)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.SingleType);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
				if (type == XmlBaseConverter.BooleanType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean), (bool)value);
				}
				if (type == XmlBaseConverter.ByteType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.UnsignedByte), value);
				}
				if (type == XmlBaseConverter.ByteArrayType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Base64Binary), value);
				}
				if (type == XmlBaseConverter.DateTimeType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.DateTime), (DateTime)value);
				}
				if (type == XmlBaseConverter.DateTimeOffsetType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.DateTime), (DateTimeOffset)value);
				}
				if (type == XmlBaseConverter.DecimalType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Decimal), value);
				}
				if (type == XmlBaseConverter.DoubleType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Double), (double)value);
				}
				if (type == XmlBaseConverter.Int16Type)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Short), value);
				}
				if (type == XmlBaseConverter.Int32Type)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Int), (int)value);
				}
				if (type == XmlBaseConverter.Int64Type)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Long), (long)value);
				}
				if (type == XmlBaseConverter.SByteType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Byte), value);
				}
				if (type == XmlBaseConverter.SingleType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Float), value);
				}
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String), (string)value);
				}
				if (type == XmlBaseConverter.TimeSpanType)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Duration), value);
				}
				if (type == XmlBaseConverter.UInt16Type)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.UnsignedShort), value);
				}
				if (type == XmlBaseConverter.UInt32Type)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.UnsignedInt), value);
				}
				if (type == XmlBaseConverter.UInt64Type)
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.UnsignedLong), value);
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.UriType))
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.AnyUri), value);
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XmlQualifiedNameType))
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.QName), value, nsResolver);
				}
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XPathNavigatorType))
				{
					return (XPathNavigator)value;
				}
			}
			if (destinationType == XmlBaseConverter.XPathNavigatorType && XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XPathNavigatorType))
			{
				return this.ToNavigator((XPathNavigator)value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return (XPathItem)this.ChangeType(value, XmlBaseConverter.XmlAtomicValueType, nsResolver);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x00111CC5 File Offset: 0x0010FEC5
		private object ChangeTypeWildcardDestination(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value.GetType() == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x00111CF0 File Offset: 0x0010FEF0
		private object ChangeTypeWildcardSource(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return (XPathItem)this.ChangeType(value, XmlBaseConverter.XmlAtomicValueType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x00113B11 File Offset: 0x00111D11
		private XPathNavigator ToNavigator(XPathNavigator nav)
		{
			if (base.TypeCode != XmlTypeCode.Item)
			{
				throw base.CreateInvalidClrMappingException(XmlBaseConverter.XPathNavigatorType, XmlBaseConverter.XPathNavigatorType);
			}
			return nav;
		}

		// Token: 0x04001EE8 RID: 7912
		public static readonly XmlValueConverter Item = new XmlAnyConverter(XmlTypeCode.Item);

		// Token: 0x04001EE9 RID: 7913
		public static readonly XmlValueConverter AnyAtomic = new XmlAnyConverter(XmlTypeCode.AnyAtomicType);
	}
}
