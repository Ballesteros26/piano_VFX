using System;

namespace System.Xml.Schema
{
	// Token: 0x020003BD RID: 957
	internal class Datatype_decimal : Datatype_anySimpleType
	{
		// Token: 0x06002634 RID: 9780 RVA: 0x000E438E File Offset: 0x000E258E
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlNumeric10Converter.Create(schemaType);
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x000E4396 File Offset: 0x000E2596
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_decimal.numeric10FacetsChecker;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x0007BD32 File Offset: 0x00079F32
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Decimal;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002637 RID: 9783 RVA: 0x000E439D File Offset: 0x000E259D
		public override Type ValueType
		{
			get
			{
				return Datatype_decimal.atomicValueType;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002638 RID: 9784 RVA: 0x000E43A4 File Offset: 0x000E25A4
		internal override Type ListValueType
		{
			get
			{
				return Datatype_decimal.listValueType;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002639 RID: 9785 RVA: 0x000026AE File Offset: 0x000008AE
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x0600263A RID: 9786 RVA: 0x000E43AB File Offset: 0x000E25AB
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive | RestrictionFlags.TotalDigits | RestrictionFlags.FractionDigits;
			}
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x000E43B4 File Offset: 0x000E25B4
		internal override int Compare(object value1, object value2)
		{
			return ((decimal)value1).CompareTo(value2);
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x000E43D0 File Offset: 0x000E25D0
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_decimal.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				decimal num;
				ex = XmlConvert.TryToDecimal(s, out num);
				if (ex == null)
				{
					ex = Datatype_decimal.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x040019EB RID: 6635
		private static readonly Type atomicValueType = typeof(decimal);

		// Token: 0x040019EC RID: 6636
		private static readonly Type listValueType = typeof(decimal[]);

		// Token: 0x040019ED RID: 6637
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(decimal.MinValue, decimal.MaxValue);
	}
}
