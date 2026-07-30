using System;

namespace System.Xml.Schema
{
	/// <summary>Specifies a restriction on the number of digits that can be entered for the fraction value of a simpleType element. The value of fractionDigits must be a positive integer. Represents the World Wide Web Consortium (W3C) fractionDigits facet.</summary>
	// Token: 0x0200045E RID: 1118
	public class XmlSchemaFractionDigitsFacet : XmlSchemaNumericFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaFractionDigitsFacet" /> class.</summary>
		// Token: 0x06002C37 RID: 11319 RVA: 0x00106ED8 File Offset: 0x001050D8
		public XmlSchemaFractionDigitsFacet()
		{
			base.FacetType = FacetType.FractionDigits;
		}
	}
}
