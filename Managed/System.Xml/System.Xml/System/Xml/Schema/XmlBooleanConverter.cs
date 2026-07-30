using System;

namespace System.Xml.Schema
{
	// Token: 0x02000497 RID: 1175
	internal class XmlBooleanConverter : XmlBaseConverter
	{
		// Token: 0x06002F6D RID: 12141 RVA: 0x0010F8F4 File Offset: 0x0010DAF4
		protected XmlBooleanConverter(XmlSchemaType schemaType)
			: base(schemaType)
		{
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x001112F0 File Offset: 0x0010F4F0
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlBooleanConverter(schemaType);
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x0000206B File Offset: 0x0000026B
		public override bool ToBoolean(bool value)
		{
			return value;
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x001112F8 File Offset: 0x0010F4F8
		public override bool ToBoolean(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToBoolean(value);
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x00111310 File Offset: 0x0010F510
		public override bool ToBoolean(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.BooleanType)
			{
				return (bool)value;
			}
			if (type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToBoolean((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsBoolean;
			}
			return (bool)this.ChangeListType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x0011138A File Offset: 0x0010F58A
		public override string ToString(bool value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x0010FBEF File Offset: 0x0010DDEF
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value;
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x00111394 File Offset: 0x0010F594
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.BooleanType)
			{
				return XmlConvert.ToString((bool)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return (string)value;
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).Value;
			}
			return (string)this.ChangeListType(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x00111410 File Offset: 0x0010F610
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
			if (destinationType == XmlBaseConverter.BooleanType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			return this.ChangeListType(value, destinationType, null);
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x001114B0 File Offset: 0x0010F6B0
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
			if (destinationType == XmlBaseConverter.BooleanType)
			{
				return XmlConvert.ToBoolean(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x0011155C File Offset: 0x0010F75C
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
			if (destinationType == XmlBaseConverter.BooleanType)
			{
				return this.ToBoolean(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.BooleanType)
				{
					return new XmlAtomicValue(base.SchemaType, (bool)value);
				}
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				if (type == XmlBaseConverter.BooleanType)
				{
					return new XmlAtomicValue(base.SchemaType, (bool)value);
				}
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}
	}
}
