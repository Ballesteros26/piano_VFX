using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the attributeGroup element with the ref attribute from the XML Schema as specified by the World Wide Web Consortium (W3C). AttributesGroupRef is the reference for an attributeGroup, name property contains the attribute group being referenced. </summary>
	// Token: 0x0200043D RID: 1085
	public class XmlSchemaAttributeGroupRef : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the name of the referenced attributeGroup element.</summary>
		/// <returns>The name of the referenced attribute group. The value must be a QName.</returns>
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06002B20 RID: 11040 RVA: 0x00105041 File Offset: 0x00103241
		// (set) Token: 0x06002B21 RID: 11041 RVA: 0x00105049 File Offset: 0x00103249
		[XmlAttribute("ref")]
		public XmlQualifiedName RefName
		{
			get
			{
				return this.refName;
			}
			set
			{
				this.refName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		// Token: 0x04001D40 RID: 7488
		private XmlQualifiedName refName = XmlQualifiedName.Empty;
	}
}
