using System;

namespace System.Xml.Schema
{
	// Token: 0x020003D0 RID: 976
	internal class Datatype_anyURI : Datatype_anySimpleType
	{
		// Token: 0x06002685 RID: 9861 RVA: 0x000E4458 File Offset: 0x000E2658
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x000E4178 File Offset: 0x000E2378
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.stringFacetsChecker;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06002687 RID: 9863 RVA: 0x000E492C File Offset: 0x000E2B2C
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyUri;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002688 RID: 9864 RVA: 0x000E4930 File Offset: 0x000E2B30
		public override Type ValueType
		{
			get
			{
				return Datatype_anyURI.atomicValueType;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06002689 RID: 9865 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x000E4937 File Offset: 0x000E2B37
		internal override Type ListValueType
		{
			get
			{
				return Datatype_anyURI.listValueType;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x0600268B RID: 9867 RVA: 0x000026AE File Offset: 0x000008AE
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x000E3B21 File Offset: 0x000E1D21
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x000E493E File Offset: 0x000E2B3E
		internal override int Compare(object value1, object value2)
		{
			if (!((Uri)value1).Equals((Uri)value2))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x000E4958 File Offset: 0x000E2B58
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.stringFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				Uri uri;
				ex = XmlConvert.TryToUri(s, out uri);
				if (ex == null)
				{
					string originalString = uri.OriginalString;
					ex = ((StringFacetsChecker)DatatypeImplementation.stringFacetsChecker).CheckValueFacets(originalString, this, false);
					if (ex == null)
					{
						typedValue = uri;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x040019F7 RID: 6647
		private static readonly Type atomicValueType = typeof(Uri);

		// Token: 0x040019F8 RID: 6648
		private static readonly Type listValueType = typeof(Uri[]);
	}
}
