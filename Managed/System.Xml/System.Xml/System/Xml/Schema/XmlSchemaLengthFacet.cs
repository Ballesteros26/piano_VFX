using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the length facet from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the length of a simpleType element on the data type.</summary>
	// Token: 0x02000454 RID: 1108
	public class XmlSchemaLengthFacet : XmlSchemaNumericFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaLengthFacet" /> class.</summary>
		// Token: 0x06002C2D RID: 11309 RVA: 0x00106E3F File Offset: 0x0010503F
		public XmlSchemaLengthFacet()
		{
			base.FacetType = FacetType.Length;
		}
	}
}
