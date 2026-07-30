using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>This class represents the keyref element from XMLSchema as specified by the World Wide Web Consortium (W3C).</summary>
	// Token: 0x02000468 RID: 1128
	public class XmlSchemaKeyref : XmlSchemaIdentityConstraint
	{
		/// <summary>Gets or sets the name of the key that this constraint refers to in another simple or complex type.</summary>
		/// <returns>The QName of the key that this constraint refers to.</returns>
		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06002C65 RID: 11365 RVA: 0x001070BF File Offset: 0x001052BF
		// (set) Token: 0x06002C66 RID: 11366 RVA: 0x001070C7 File Offset: 0x001052C7
		[XmlAttribute("refer")]
		public XmlQualifiedName Refer
		{
			get
			{
				return this.refer;
			}
			set
			{
				this.refer = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		// Token: 0x04001DC8 RID: 7624
		private XmlQualifiedName refer = XmlQualifiedName.Empty;
	}
}
