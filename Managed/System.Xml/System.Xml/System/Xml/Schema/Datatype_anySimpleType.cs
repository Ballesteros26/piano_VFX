using System;

namespace System.Xml.Schema
{
	// Token: 0x020003B6 RID: 950
	internal class Datatype_anySimpleType : DatatypeImplementation
	{
		// Token: 0x060025F7 RID: 9719 RVA: 0x000E40FC File Offset: 0x000E22FC
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlUntypedConverter.Untyped;
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x000E3612 File Offset: 0x000E1812
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.miscFacetsChecker;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060025F9 RID: 9721 RVA: 0x000E4103 File Offset: 0x000E2303
		public override Type ValueType
		{
			get
			{
				return Datatype_anySimpleType.atomicValueType;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060025FA RID: 9722 RVA: 0x00074F5D File Offset: 0x0007315D
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyAtomicType;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x000E410A File Offset: 0x000E230A
		internal override Type ListValueType
		{
			get
			{
				return Datatype_anySimpleType.listValueType;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060025FC RID: 9724 RVA: 0x000163C5 File Offset: 0x000145C5
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.None;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x0000226C File Offset: 0x0000046C
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return (RestrictionFlags)0;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060025FE RID: 9726 RVA: 0x000026AE File Offset: 0x000008AE
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000E4111 File Offset: 0x000E2311
		internal override int Compare(object value1, object value2)
		{
			return string.Compare(value1.ToString(), value2.ToString(), StringComparison.Ordinal);
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x000E4125 File Offset: 0x000E2325
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = XmlComplianceUtil.NonCDataNormalize(s);
			return null;
		}

		// Token: 0x040019E3 RID: 6627
		private static readonly Type atomicValueType = typeof(string);

		// Token: 0x040019E4 RID: 6628
		private static readonly Type listValueType = typeof(string[]);
	}
}
