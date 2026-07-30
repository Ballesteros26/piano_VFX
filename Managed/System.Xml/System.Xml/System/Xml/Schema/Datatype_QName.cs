using System;

namespace System.Xml.Schema
{
	// Token: 0x020003D1 RID: 977
	internal class Datatype_QName : Datatype_anySimpleType
	{
		// Token: 0x06002691 RID: 9873 RVA: 0x000E4458 File Offset: 0x000E2658
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x000E49CA File Offset: 0x000E2BCA
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.qnameFacetsChecker;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06002693 RID: 9875 RVA: 0x000E49D1 File Offset: 0x000E2BD1
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.QName;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x00074F5D File Offset: 0x0007315D
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.QName;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06002695 RID: 9877 RVA: 0x000E3B21 File Offset: 0x000E1D21
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x000E49D5 File Offset: 0x000E2BD5
		public override Type ValueType
		{
			get
			{
				return Datatype_QName.atomicValueType;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06002697 RID: 9879 RVA: 0x000E49DC File Offset: 0x000E2BDC
		internal override Type ListValueType
		{
			get
			{
				return Datatype_QName.listValueType;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06002698 RID: 9880 RVA: 0x000026AE File Offset: 0x000008AE
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000E49E4 File Offset: 0x000E2BE4
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			if (s == null || s.Length == 0)
			{
				return new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
			}
			Exception ex = DatatypeImplementation.qnameFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				XmlQualifiedName xmlQualifiedName = null;
				try
				{
					string text;
					xmlQualifiedName = XmlQualifiedName.Parse(s, nsmgr, out text);
				}
				catch (ArgumentException ex)
				{
					return ex;
				}
				catch (XmlException ex)
				{
					return ex;
				}
				ex = DatatypeImplementation.qnameFacetsChecker.CheckValueFacets(xmlQualifiedName, this);
				if (ex == null)
				{
					typedValue = xmlQualifiedName;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x040019F9 RID: 6649
		private static readonly Type atomicValueType = typeof(XmlQualifiedName);

		// Token: 0x040019FA RID: 6650
		private static readonly Type listValueType = typeof(XmlQualifiedName[]);
	}
}
