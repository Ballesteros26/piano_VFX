using System;

namespace System.Xml.Schema
{
	// Token: 0x020003BE RID: 958
	internal class Datatype_duration : Datatype_anySimpleType
	{
		// Token: 0x0600263F RID: 9791 RVA: 0x000E4458 File Offset: 0x000E2658
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x000E4460 File Offset: 0x000E2660
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.durationFacetsChecker;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06002641 RID: 9793 RVA: 0x000730BB File Offset: 0x000712BB
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Duration;
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002642 RID: 9794 RVA: 0x000E4467 File Offset: 0x000E2667
		public override Type ValueType
		{
			get
			{
				return Datatype_duration.atomicValueType;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002643 RID: 9795 RVA: 0x000E446E File Offset: 0x000E266E
		internal override Type ListValueType
		{
			get
			{
				return Datatype_duration.listValueType;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06002644 RID: 9796 RVA: 0x000026AE File Offset: 0x000008AE
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002645 RID: 9797 RVA: 0x000E426B File Offset: 0x000E246B
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000E4478 File Offset: 0x000E2678
		internal override int Compare(object value1, object value2)
		{
			return ((TimeSpan)value1).CompareTo(value2);
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x000E4494 File Offset: 0x000E2694
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			if (s == null || s.Length == 0)
			{
				return new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
			}
			Exception ex = DatatypeImplementation.durationFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				TimeSpan timeSpan;
				ex = XmlConvert.TryToTimeSpan(s, out timeSpan);
				if (ex == null)
				{
					ex = DatatypeImplementation.durationFacetsChecker.CheckValueFacets(timeSpan, this);
					if (ex == null)
					{
						typedValue = timeSpan;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x040019EE RID: 6638
		private static readonly Type atomicValueType = typeof(TimeSpan);

		// Token: 0x040019EF RID: 6639
		private static readonly Type listValueType = typeof(TimeSpan[]);
	}
}
