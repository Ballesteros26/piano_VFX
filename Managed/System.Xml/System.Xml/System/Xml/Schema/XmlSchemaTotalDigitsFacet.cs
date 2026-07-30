using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the totalDigits facet from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the number of digits that can be entered for the value of a simpleType element. That value of totalDigits must be a positive integer.</summary>
	// Token: 0x0200045D RID: 1117
	public class XmlSchemaTotalDigitsFacet : XmlSchemaNumericFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaTotalDigitsFacet" /> class.</summary>
		// Token: 0x06002C36 RID: 11318 RVA: 0x00106EC8 File Offset: 0x001050C8
		public XmlSchemaTotalDigitsFacet()
		{
			base.FacetType = FacetType.TotalDigits;
		}
	}
}
