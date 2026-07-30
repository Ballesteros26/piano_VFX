using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) whiteSpace facet.</summary>
	// Token: 0x0200045F RID: 1119
	public class XmlSchemaWhiteSpaceFacet : XmlSchemaFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaWhiteSpaceFacet" /> class.</summary>
		// Token: 0x06002C38 RID: 11320 RVA: 0x00106EE8 File Offset: 0x001050E8
		public XmlSchemaWhiteSpaceFacet()
		{
			base.FacetType = FacetType.Whitespace;
		}
	}
}
