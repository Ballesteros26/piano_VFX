using System;

namespace System.Xml.Schema
{
	// Token: 0x020003B9 RID: 953
	internal class Datatype_string : Datatype_anySimpleType
	{
		// Token: 0x0600260B RID: 9739 RVA: 0x000E4170 File Offset: 0x000E2370
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlStringConverter.Create(schemaType);
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x0600260C RID: 9740 RVA: 0x0000226C File Offset: 0x0000046C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x000E4178 File Offset: 0x000E2378
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.stringFacetsChecker;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x000163C5 File Offset: 0x000145C5
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.String;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x0000226C File Offset: 0x0000046C
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06002610 RID: 9744 RVA: 0x000E3B21 File Offset: 0x000E1D21
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x000E4180 File Offset: 0x000E2380
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.stringFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ex = DatatypeImplementation.stringFacetsChecker.CheckValueFacets(s, this);
				if (ex == null)
				{
					typedValue = s;
					return null;
				}
			}
			return ex;
		}
	}
}
