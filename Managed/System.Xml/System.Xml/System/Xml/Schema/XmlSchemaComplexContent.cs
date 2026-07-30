using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the complexContent element from XML Schema as specified by the World Wide Web Consortium (W3C). This class represents the complex content model for complex types. It contains extensions or restrictions on a complex type that has either only elements or mixed content.</summary>
	// Token: 0x02000443 RID: 1091
	public class XmlSchemaComplexContent : XmlSchemaContentModel
	{
		/// <summary>Gets or sets information that determines if the type has a mixed content model.</summary>
		/// <returns>If this property is true, character data is allowed to appear between the child elements of the complex type (mixed content model). The default is false.Optional.</returns>
		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06002B58 RID: 11096 RVA: 0x001056F3 File Offset: 0x001038F3
		// (set) Token: 0x06002B59 RID: 11097 RVA: 0x001056FB File Offset: 0x001038FB
		[XmlAttribute("mixed")]
		public bool IsMixed
		{
			get
			{
				return this.isMixed;
			}
			set
			{
				this.isMixed = value;
				this.hasMixedAttribute = true;
			}
		}

		/// <summary>Gets or sets the content.</summary>
		/// <returns>One of either the <see cref="T:System.Xml.Schema.XmlSchemaComplexContentRestriction" /> or <see cref="T:System.Xml.Schema.XmlSchemaComplexContentExtension" /> classes.</returns>
		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002B5A RID: 11098 RVA: 0x0010570B File Offset: 0x0010390B
		// (set) Token: 0x06002B5B RID: 11099 RVA: 0x00105713 File Offset: 0x00103913
		[XmlElement("extension", typeof(XmlSchemaComplexContentExtension))]
		[XmlElement("restriction", typeof(XmlSchemaComplexContentRestriction))]
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

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002B5C RID: 11100 RVA: 0x0010571C File Offset: 0x0010391C
		[XmlIgnore]
		internal bool HasMixedAttribute
		{
			get
			{
				return this.hasMixedAttribute;
			}
		}

		// Token: 0x04001D4F RID: 7503
		private XmlSchemaContent content;

		// Token: 0x04001D50 RID: 7504
		private bool isMixed;

		// Token: 0x04001D51 RID: 7505
		private bool hasMixedAttribute;
	}
}
