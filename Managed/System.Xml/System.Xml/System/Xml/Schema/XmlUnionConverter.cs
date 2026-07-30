using System;

namespace System.Xml.Schema
{
	// Token: 0x0200049F RID: 1183
	internal class XmlUnionConverter : XmlBaseConverter
	{
		// Token: 0x06002FDA RID: 12250 RVA: 0x0011420C File Offset: 0x0011240C
		protected XmlUnionConverter(XmlSchemaType schemaType)
			: base(schemaType)
		{
			while (schemaType.DerivedBy == XmlSchemaDerivationMethod.Restriction)
			{
				schemaType = schemaType.BaseXmlSchemaType;
			}
			XmlSchemaSimpleType[] baseMemberTypes = ((XmlSchemaSimpleTypeUnion)((XmlSchemaSimpleType)schemaType).Content).BaseMemberTypes;
			this.converters = new XmlValueConverter[baseMemberTypes.Length];
			for (int i = 0; i < baseMemberTypes.Length; i++)
			{
				this.converters[i] = baseMemberTypes[i].ValueConverter;
				if (baseMemberTypes[i].Datatype.Variety == XmlSchemaDatatypeVariety.List)
				{
					this.hasListMember = true;
				}
				else if (baseMemberTypes[i].Datatype.Variety == XmlSchemaDatatypeVariety.Atomic)
				{
					this.hasAtomicMember = true;
				}
			}
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x001142A4 File Offset: 0x001124A4
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlUnionConverter(schemaType);
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x001142AC File Offset: 0x001124AC
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
			if (type == XmlBaseConverter.XmlAtomicValueType && this.hasAtomicMember)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			if (type == XmlBaseConverter.XmlAtomicValueArrayType && this.hasListMember)
			{
				return XmlAnyListConverter.ItemList.ChangeType(value, destinationType, nsResolver);
			}
			if (!(type == XmlBaseConverter.StringType))
			{
				throw base.CreateInvalidClrMappingException(type, destinationType);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return value;
			}
			return ((XsdSimpleValue)base.SchemaType.Datatype.ParseValue((string)value, new NameTable(), nsResolver, true)).XmlType.ValueConverter.ChangeType((string)value, destinationType, nsResolver);
		}

		// Token: 0x04001EED RID: 7917
		private XmlValueConverter[] converters;

		// Token: 0x04001EEE RID: 7918
		private bool hasAtomicMember;

		// Token: 0x04001EEF RID: 7919
		private bool hasListMember;
	}
}
