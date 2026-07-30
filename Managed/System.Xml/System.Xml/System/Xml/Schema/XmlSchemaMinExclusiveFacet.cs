using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the minExclusive element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the minimum value of a simpleType element. The element value must be greater than the value of the minExclusive element.</summary>
	// Token: 0x02000459 RID: 1113
	public class XmlSchemaMinExclusiveFacet : XmlSchemaFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaMinExclusiveFacet" /> class.</summary>
		// Token: 0x06002C32 RID: 11314 RVA: 0x00106E8A File Offset: 0x0010508A
		public XmlSchemaMinExclusiveFacet()
		{
			base.FacetType = FacetType.MinExclusive;
		}
	}
}
