using System;

namespace System.Xml.Schema
{
	// Token: 0x020003DD RID: 989
	internal class Datatype_NOTATION : Datatype_anySimpleType
	{
		// Token: 0x060026BB RID: 9915 RVA: 0x000E4458 File Offset: 0x000E2658
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x000E49CA File Offset: 0x000E2BCA
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.qnameFacetsChecker;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x000E4B19 File Offset: 0x000E2D19
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Notation;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x00072E3D File Offset: 0x0007103D
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.NOTATION;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x000E3B21 File Offset: 0x000E1D21
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x000E4B1D File Offset: 0x000E2D1D
		public override Type ValueType
		{
			get
			{
				return Datatype_NOTATION.atomicValueType;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060026C1 RID: 9921 RVA: 0x000E4B24 File Offset: 0x000E2D24
		internal override Type ListValueType
		{
			get
			{
				return Datatype_NOTATION.listValueType;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x000026AE File Offset: 0x000008AE
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x000E4B2C File Offset: 0x000E2D2C
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

		// Token: 0x060026C4 RID: 9924 RVA: 0x000E4BB0 File Offset: 0x000E2DB0
		internal override void VerifySchemaValid(XmlSchemaObjectTable notations, XmlSchemaObject caller)
		{
			for (Datatype_NOTATION datatype_NOTATION = this; datatype_NOTATION != null; datatype_NOTATION = (Datatype_NOTATION)datatype_NOTATION.Base)
			{
				if (datatype_NOTATION.Restriction != null && (datatype_NOTATION.Restriction.Flags & RestrictionFlags.Enumeration) != (RestrictionFlags)0)
				{
					for (int i = 0; i < datatype_NOTATION.Restriction.Enumeration.Count; i++)
					{
						XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)datatype_NOTATION.Restriction.Enumeration[i];
						if (!notations.Contains(xmlQualifiedName))
						{
							throw new XmlSchemaException("NOTATION cannot be used directly in a schema; only data types derived from NOTATION by specifying an enumeration value can be used in a schema. All enumeration facet values must match the name of a notation declared in the current schema.", caller);
						}
					}
					return;
				}
			}
			throw new XmlSchemaException("NOTATION cannot be used directly in a schema; only data types derived from NOTATION by specifying an enumeration value can be used in a schema. All enumeration facet values must match the name of a notation declared in the current schema.", caller);
		}

		// Token: 0x040019FB RID: 6651
		private static readonly Type atomicValueType = typeof(XmlQualifiedName);

		// Token: 0x040019FC RID: 6652
		private static readonly Type listValueType = typeof(XmlQualifiedName[]);
	}
}
