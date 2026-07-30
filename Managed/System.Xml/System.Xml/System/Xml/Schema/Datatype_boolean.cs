using System;

namespace System.Xml.Schema
{
	// Token: 0x020003BA RID: 954
	internal class Datatype_boolean : Datatype_anySimpleType
	{
		// Token: 0x06002613 RID: 9747 RVA: 0x000E41B9 File Offset: 0x000E23B9
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlBooleanConverter.Create(schemaType);
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x000E3612 File Offset: 0x000E1812
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.miscFacetsChecker;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x0007BFF7 File Offset: 0x0007A1F7
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Boolean;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06002616 RID: 9750 RVA: 0x000E41C1 File Offset: 0x000E23C1
		public override Type ValueType
		{
			get
			{
				return Datatype_boolean.atomicValueType;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x000E41C8 File Offset: 0x000E23C8
		internal override Type ListValueType
		{
			get
			{
				return Datatype_boolean.listValueType;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06002618 RID: 9752 RVA: 0x000026AE File Offset: 0x000008AE
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x000E41CF File Offset: 0x000E23CF
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x000E41D4 File Offset: 0x000E23D4
		internal override int Compare(object value1, object value2)
		{
			return ((bool)value1).CompareTo(value2);
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x000E41F0 File Offset: 0x000E23F0
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.miscFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				bool flag;
				ex = XmlConvert.TryToBoolean(s, out flag);
				if (ex == null)
				{
					typedValue = flag;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x040019E5 RID: 6629
		private static readonly Type atomicValueType = typeof(bool);

		// Token: 0x040019E6 RID: 6630
		private static readonly Type listValueType = typeof(bool[]);
	}
}
