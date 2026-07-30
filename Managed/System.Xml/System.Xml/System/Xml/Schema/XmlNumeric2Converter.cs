using System;

namespace System.Xml.Schema
{
	// Token: 0x02000495 RID: 1173
	internal class XmlNumeric2Converter : XmlBaseConverter
	{
		// Token: 0x06002F49 RID: 12105 RVA: 0x0010F8F4 File Offset: 0x0010DAF4
		protected XmlNumeric2Converter(XmlSchemaType schemaType)
			: base(schemaType)
		{
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x001104FC File Offset: 0x0010E6FC
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlNumeric2Converter(schemaType);
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x00110504 File Offset: 0x0010E704
		public override double ToDouble(double value)
		{
			return value;
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x00110508 File Offset: 0x0010E708
		public override double ToDouble(float value)
		{
			return (double)value;
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x0011050D File Offset: 0x0010E70D
		public override double ToDouble(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (base.TypeCode == XmlTypeCode.Float)
			{
				return (double)XmlConvert.ToSingle(value);
			}
			return XmlConvert.ToDouble(value);
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x00110538 File Offset: 0x0010E738
		public override double ToDouble(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DoubleType)
			{
				return (double)value;
			}
			if (type == XmlBaseConverter.SingleType)
			{
				return (double)((float)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToDouble((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsDouble;
			}
			return (double)this.ChangeListType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x001105C8 File Offset: 0x0010E7C8
		public override float ToSingle(double value)
		{
			return (float)value;
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x001105CD File Offset: 0x0010E7CD
		public override float ToSingle(float value)
		{
			return value;
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x001105D1 File Offset: 0x0010E7D1
		public override float ToSingle(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (base.TypeCode == XmlTypeCode.Float)
			{
				return XmlConvert.ToSingle(value);
			}
			return (float)XmlConvert.ToDouble(value);
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x001105FC File Offset: 0x0010E7FC
		public override float ToSingle(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DoubleType)
			{
				return (float)((double)value);
			}
			if (type == XmlBaseConverter.SingleType)
			{
				return (float)value;
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToSingle((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (float)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.SingleType);
			}
			return (float)this.ChangeListType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x00110696 File Offset: 0x0010E896
		public override string ToString(double value)
		{
			if (base.TypeCode == XmlTypeCode.Float)
			{
				return XmlConvert.ToString(this.ToSingle(value));
			}
			return XmlConvert.ToString(value);
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x001106B7 File Offset: 0x0010E8B7
		public override string ToString(float value)
		{
			if (base.TypeCode == XmlTypeCode.Float)
			{
				return XmlConvert.ToString(value);
			}
			return XmlConvert.ToString((double)value);
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x0010FBEF File Offset: 0x0010DDEF
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value;
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x001106D4 File Offset: 0x0010E8D4
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DoubleType)
			{
				return this.ToString((double)value);
			}
			if (type == XmlBaseConverter.SingleType)
			{
				return this.ToString((float)value);
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

		// Token: 0x06002F57 RID: 12119 RVA: 0x0011076C File Offset: 0x0010E96C
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
			if (destinationType == XmlBaseConverter.DoubleType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.SingleType)
			{
				return (float)value;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value);
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

		// Token: 0x06002F58 RID: 12120 RVA: 0x00110828 File Offset: 0x0010EA28
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
			if (destinationType == XmlBaseConverter.DoubleType)
			{
				return (double)value;
			}
			if (destinationType == XmlBaseConverter.SingleType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, (double)value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, (double)value);
			}
			return this.ChangeListType(value, destinationType, null);
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x001108E4 File Offset: 0x0010EAE4
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
			if (destinationType == XmlBaseConverter.DoubleType)
			{
				return this.ToDouble(value);
			}
			if (destinationType == XmlBaseConverter.SingleType)
			{
				return this.ToSingle(value);
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

		// Token: 0x06002F5A RID: 12122 RVA: 0x001109A8 File Offset: 0x0010EBA8
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
			if (destinationType == XmlBaseConverter.DoubleType)
			{
				return this.ToDouble(value);
			}
			if (destinationType == XmlBaseConverter.SingleType)
			{
				return this.ToSingle(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.DoubleType)
				{
					return new XmlAtomicValue(base.SchemaType, (double)value);
				}
				if (type == XmlBaseConverter.SingleType)
				{
					return new XmlAtomicValue(base.SchemaType, value);
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
				if (type == XmlBaseConverter.DoubleType)
				{
					return new XmlAtomicValue(base.SchemaType, (double)value);
				}
				if (type == XmlBaseConverter.SingleType)
				{
					return new XmlAtomicValue(base.SchemaType, value);
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
