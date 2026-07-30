using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the pattern element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the value entered for a simpleType element.</summary>
	// Token: 0x02000457 RID: 1111
	public class XmlSchemaPatternFacet : XmlSchemaFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaPatternFacet" /> class.</summary>
		// Token: 0x06002C30 RID: 11312 RVA: 0x00106E6C File Offset: 0x0010506C
		public XmlSchemaPatternFacet()
		{
			base.FacetType = FacetType.Pattern;
		}
	}
}
