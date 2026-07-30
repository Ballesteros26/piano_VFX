using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the maxLength element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the maximum length of the data value of a simpleType element. The length must be less than the value of the maxLength element.</summary>
	// Token: 0x02000456 RID: 1110
	public class XmlSchemaMaxLengthFacet : XmlSchemaNumericFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaMaxLengthFacet" /> class.</summary>
		// Token: 0x06002C2F RID: 11311 RVA: 0x00106E5D File Offset: 0x0010505D
		public XmlSchemaMaxLengthFacet()
		{
			base.FacetType = FacetType.MaxLength;
		}
	}
}
