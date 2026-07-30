using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the simpleContent element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is for simple and complex types with simple content model.</summary>
	// Token: 0x0200047D RID: 1149
	public class XmlSchemaSimpleContent : XmlSchemaContentModel
	{
		/// <summary>Gets one of the <see cref="T:System.Xml.Schema.XmlSchemaSimpleContentRestriction" /> or <see cref="T:System.Xml.Schema.XmlSchemaSimpleContentExtension" />.</summary>
		/// <returns>The content contained within the XmlSchemaSimpleContentRestriction or XmlSchemaSimpleContentExtension class.</returns>
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06002D3B RID: 11579 RVA: 0x00109D41 File Offset: 0x00107F41
		// (set) Token: 0x06002D3C RID: 11580 RVA: 0x00109D49 File Offset: 0x00107F49
		[XmlElement("extension", typeof(XmlSchemaSimpleContentExtension))]
		[XmlElement("restriction", typeof(XmlSchemaSimpleContentRestriction))]
		public override XmlSchemaContent Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		// Token: 0x04001E13 RID: 7699
		private XmlSchemaContent content;
	}
}
