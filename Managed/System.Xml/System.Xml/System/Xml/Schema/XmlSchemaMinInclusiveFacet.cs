using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the minInclusive element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the minimum value of a simpleType element. The element value must be greater than or equal to the value of the minInclusive element.</summary>
	// Token: 0x0200045A RID: 1114
	public class XmlSchemaMinInclusiveFacet : XmlSchemaFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaMinInclusiveFacet" /> class.</summary>
		// Token: 0x06002C33 RID: 11315 RVA: 0x00106E99 File Offset: 0x00105099
		public XmlSchemaMinInclusiveFacet()
		{
			base.FacetType = FacetType.MinInclusive;
		}
	}
}
