using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the notation element from XML Schema as specified by the World Wide Web Consortium (W3C). An XML Schema notation declaration is a reconstruction of XML 1.0 NOTATION declarations. The purpose of notations is to describe the format of non-XML data within an XML document.</summary>
	// Token: 0x0200046C RID: 1132
	public class XmlSchemaNotation : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the name of the notation.</summary>
		/// <returns>The name of the notation.</returns>
		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06002C88 RID: 11400 RVA: 0x001072BA File Offset: 0x001054BA
		// (set) Token: 0x06002C89 RID: 11401 RVA: 0x001072C2 File Offset: 0x001054C2
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the public identifier.</summary>
		/// <returns>The public identifier. The value must be a valid Uniform Resource Identifier (URI).</returns>
		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06002C8A RID: 11402 RVA: 0x001072CB File Offset: 0x001054CB
		// (set) Token: 0x06002C8B RID: 11403 RVA: 0x001072D3 File Offset: 0x001054D3
		[XmlAttribute("public")]
		public string Public
		{
			get
			{
				return this.publicId;
			}
			set
			{
				this.publicId = value;
			}
		}

		/// <summary>Gets or sets the system identifier.</summary>
		/// <returns>The system identifier. The value must be a valid URI.</returns>
		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06002C8C RID: 11404 RVA: 0x001072DC File Offset: 0x001054DC
		// (set) Token: 0x06002C8D RID: 11405 RVA: 0x001072E4 File Offset: 0x001054E4
		[XmlAttribute("system")]
		public string System
		{
			get
			{
				return this.systemId;
			}
			set
			{
				this.systemId = value;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002C8E RID: 11406 RVA: 0x001072ED File Offset: 0x001054ED
		// (set) Token: 0x06002C8F RID: 11407 RVA: 0x001072F5 File Offset: 0x001054F5
		[XmlIgnore]
		internal XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
			set
			{
				this.qname = value;
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002C90 RID: 11408 RVA: 0x001072FE File Offset: 0x001054FE
		// (set) Token: 0x06002C91 RID: 11409 RVA: 0x00107306 File Offset: 0x00105506
		[XmlIgnore]
		internal override string NameAttribute
		{
			get
			{
				return this.Name;
			}
			set
			{
				this.Name = value;
			}
		}

		// Token: 0x04001DD4 RID: 7636
		private string name;

		// Token: 0x04001DD5 RID: 7637
		private string publicId;

		// Token: 0x04001DD6 RID: 7638
		private string systemId;

		// Token: 0x04001DD7 RID: 7639
		private XmlQualifiedName qname = XmlQualifiedName.Empty;
	}
}
