using System;

namespace System.Xml.Schema
{
	// Token: 0x020003BB RID: 955
	internal class Datatype_float : Datatype_anySimpleType
	{
		// Token: 0x0600261E RID: 9758 RVA: 0x000E424A File Offset: 0x000E244A
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlNumeric2Converter.Create(schemaType);
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x0600261F RID: 9759 RVA: 0x000E4252 File Offset: 0x000E2452
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.numeric2FacetsChecker;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002620 RID: 9760 RVA: 0x000E4259 File Offset: 0x000E2459
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Float;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002621 RID: 9761 RVA: 0x000E425D File Offset: 0x000E245D
		public override Type ValueType
		{
			get
			{
				return Datatype_float.atomicValueType;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002622 RID: 9762 RVA: 0x000E4264 File Offset: 0x000E2464
		internal override Type ListValueType
		{
			get
			{
				return Datatype_float.listValueType;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002623 RID: 9763 RVA: 0x000026AE File Offset: 0x000008AE
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002624 RID: 9764 RVA: 0x000E426B File Offset: 0x000E246B
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000E4274 File Offset: 0x000E2474
		internal override int Compare(object value1, object value2)
		{
			return ((float)value1).CompareTo(value2);
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x000E4290 File Offset: 0x000E2490
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.numeric2FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				float num;
				ex = XmlConvert.TryToSingle(s, out num);
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

		// Token: 0x040019E7 RID: 6631
		private static readonly Type atomicValueType = typeof(float);

		// Token: 0x040019E8 RID: 6632
		private static readonly Type listValueType = typeof(float[]);
	}
}
