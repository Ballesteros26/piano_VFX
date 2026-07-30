using System;

namespace System.Xml.Schema
{
	// Token: 0x020003BC RID: 956
	internal class Datatype_double : Datatype_anySimpleType
	{
		// Token: 0x06002629 RID: 9769 RVA: 0x000E424A File Offset: 0x000E244A
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlNumeric2Converter.Create(schemaType);
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x0600262A RID: 9770 RVA: 0x000E4252 File Offset: 0x000E2452
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.numeric2FacetsChecker;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x00005D44 File Offset: 0x00003F44
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Double;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600262C RID: 9772 RVA: 0x000E42FA File Offset: 0x000E24FA
		public override Type ValueType
		{
			get
			{
				return Datatype_double.atomicValueType;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x000E4301 File Offset: 0x000E2501
		internal override Type ListValueType
		{
			get
			{
				return Datatype_double.listValueType;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x000026AE File Offset: 0x000008AE
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600262F RID: 9775 RVA: 0x000E426B File Offset: 0x000E246B
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x000E4308 File Offset: 0x000E2508
		internal override int Compare(object value1, object value2)
		{
			return ((double)value1).CompareTo(value2);
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x000E4324 File Offset: 0x000E2524
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.numeric2FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				double num;
				ex = XmlConvert.TryToDouble(s, out num);
				if (ex == null)
				{
					ex = DatatypeImplementation.numeric2FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x040019E9 RID: 6633
		private static readonly Type atomicValueType = typeof(double);

		// Token: 0x040019EA RID: 6634
		private static readonly Type listValueType = typeof(double[]);
	}
}
