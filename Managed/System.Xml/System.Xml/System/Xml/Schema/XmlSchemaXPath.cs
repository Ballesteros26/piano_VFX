using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) selector element.</summary>
	// Token: 0x02000465 RID: 1125
	public class XmlSchemaXPath : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the attribute for the XPath expression.</summary>
		/// <returns>The string attribute value for the XPath expression.</returns>
		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06002C60 RID: 11360 RVA: 0x001070A6 File Offset: 0x001052A6
		// (set) Token: 0x06002C61 RID: 11361 RVA: 0x001070AE File Offset: 0x001052AE
		[XmlAttribute("xpath")]
		[DefaultValue("")]
		public string XPath
		{
			get
			{
				return this.xpath;
			}
			set
			{
				this.xpath = value;
			}
		}

		// Token: 0x04001DC7 RID: 7623
		private string xpath;
	}
}
