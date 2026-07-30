using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the minLength element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the minimum length of the data value of a simpleType element. The length must be greater than the value of the minLength element.</summary>
	// Token: 0x02000455 RID: 1109
	public class XmlSchemaMinLengthFacet : XmlSchemaNumericFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaMinLengthFacet" /> class.</summary>
		// Token: 0x06002C2E RID: 11310 RVA: 0x00106E4E File Offset: 0x0010504E
		public XmlSchemaMinLengthFacet()
		{
			base.FacetType = FacetType.MinLength;
		}
	}
}
