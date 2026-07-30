using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the enumeration facet from XML Schema as specified by the World Wide Web Consortium (W3C). This class specifies a list of valid values for a simpleType element. Declaration is contained within a restriction declaration.</summary>
	// Token: 0x02000458 RID: 1112
	public class XmlSchemaEnumerationFacet : XmlSchemaFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaEnumerationFacet" /> class.</summary>
		// Token: 0x06002C31 RID: 11313 RVA: 0x00106E7B File Offset: 0x0010507B
		public XmlSchemaEnumerationFacet()
		{
			base.FacetType = FacetType.Enumeration;
		}
	}
}
