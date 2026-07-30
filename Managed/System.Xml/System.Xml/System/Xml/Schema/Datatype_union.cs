using System;

namespace System.Xml.Schema
{
	// Token: 0x020003B5 RID: 949
	internal class Datatype_union : Datatype_anySimpleType
	{
		// Token: 0x060025E9 RID: 9705 RVA: 0x000E3E1D File Offset: 0x000E201D
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlUnionConverter.Create(schemaType);
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x000E3E25 File Offset: 0x000E2025
		internal Datatype_union(XmlSchemaSimpleType[] types)
		{
			this.types = types;
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000E3E34 File Offset: 0x000E2034
		internal override int Compare(object value1, object value2)
		{
			XsdSimpleValue xsdSimpleValue = value1 as XsdSimpleValue;
			XsdSimpleValue xsdSimpleValue2 = value2 as XsdSimpleValue;
			if (xsdSimpleValue == null || xsdSimpleValue2 == null)
			{
				return -1;
			}
			XmlSchemaType xmlType = xsdSimpleValue.XmlType;
			XmlSchemaType xmlType2 = xsdSimpleValue2.XmlType;
			if (xmlType == xmlType2)
			{
				return xmlType.Datatype.Compare(xsdSimpleValue.TypedValue, xsdSimpleValue2.TypedValue);
			}
			return -1;
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060025EC RID: 9708 RVA: 0x000E3E82 File Offset: 0x000E2082
		public override Type ValueType
		{
			get
			{
				return Datatype_union.atomicValueType;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x00074F5D File Offset: 0x0007315D
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyAtomicType;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x060025EE RID: 9710 RVA: 0x000E3E89 File Offset: 0x000E2089
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.unionFacetsChecker;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060025EF RID: 9711 RVA: 0x000E3E90 File Offset: 0x000E2090
		internal override Type ListValueType
		{
			get
			{
				return Datatype_union.listValueType;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x000E3E97 File Offset: 0x000E2097
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x000E3E9B File Offset: 0x000E209B
		internal XmlSchemaSimpleType[] BaseMemberTypes
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x000E3EA4 File Offset: 0x000E20A4
		internal bool HasAtomicMembers()
		{
			for (int i = 0; i < this.types.Length; i++)
			{
				if (this.types[i].Datatype.Variety == XmlSchemaDatatypeVariety.List)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x000E3EDC File Offset: 0x000E20DC
		internal bool IsUnionBaseOf(DatatypeImplementation derivedType)
		{
			for (int i = 0; i < this.types.Length; i++)
			{
				if (derivedType.IsDerivedFrom(this.types[i].Datatype))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x000E3F14 File Offset: 0x000E2114
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = null;
			typedValue = null;
			Exception ex = DatatypeImplementation.unionFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				for (int i = 0; i < this.types.Length; i++)
				{
					if (this.types[i].Datatype.TryParseValue(s, nameTable, nsmgr, out typedValue) == null)
					{
						xmlSchemaSimpleType = this.types[i];
						break;
					}
				}
				if (xmlSchemaSimpleType == null)
				{
					ex = new XmlSchemaException("The value '{0}' is not valid according to any of the memberTypes of the union.", s);
				}
				else
				{
					typedValue = new XsdSimpleValue(xmlSchemaSimpleType, typedValue);
					ex = DatatypeImplementation.unionFacetsChecker.CheckValueFacets(typedValue, this);
					if (ex == null)
					{
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x000E3FA4 File Offset: 0x000E21A4
		internal override Exception TryParseValue(object value, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			typedValue = null;
			string text = value as string;
			if (text != null)
			{
				return this.TryParseValue(text, nameTable, nsmgr, out typedValue);
			}
			object obj = null;
			XmlSchemaSimpleType xmlSchemaSimpleType = null;
			for (int i = 0; i < this.types.Length; i++)
			{
				if (this.types[i].Datatype.TryParseValue(value, nameTable, nsmgr, out obj) == null)
				{
					xmlSchemaSimpleType = this.types[i];
					break;
				}
			}
			Exception ex;
			if (obj != null)
			{
				try
				{
					if (this.HasLexicalFacets)
					{
						string text2 = (string)this.ValueConverter.ChangeType(obj, typeof(string), nsmgr);
						ex = DatatypeImplementation.unionFacetsChecker.CheckLexicalFacets(ref text2, this);
						if (ex != null)
						{
							return ex;
						}
					}
					typedValue = new XsdSimpleValue(xmlSchemaSimpleType, obj);
					if (this.HasValueFacets)
					{
						ex = DatatypeImplementation.unionFacetsChecker.CheckValueFacets(typedValue, this);
						if (ex != null)
						{
							return ex;
						}
					}
					return null;
				}
				catch (FormatException ex)
				{
				}
				catch (InvalidCastException ex)
				{
				}
				catch (OverflowException ex)
				{
				}
				catch (ArgumentException ex)
				{
				}
				return ex;
			}
			ex = new XmlSchemaException("The value '{0}' is not valid according to any of the memberTypes of the union.", value.ToString());
			return ex;
		}

		// Token: 0x040019E0 RID: 6624
		private static readonly Type atomicValueType = typeof(object);

		// Token: 0x040019E1 RID: 6625
		private static readonly Type listValueType = typeof(object[]);

		// Token: 0x040019E2 RID: 6626
		private XmlSchemaSimpleType[] types;
	}
}
