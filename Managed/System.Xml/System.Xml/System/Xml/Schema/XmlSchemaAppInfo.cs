using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) appinfo element.</summary>
	// Token: 0x0200043A RID: 1082
	public class XmlSchemaAppInfo : XmlSchemaObject
	{
		/// <summary>Gets or sets the source of the application information.</summary>
		/// <returns>A Uniform Resource Identifier (URI) reference. The default is String.Empty.Optional.</returns>
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06002AEA RID: 10986 RVA: 0x00104D3D File Offset: 0x00102F3D
		// (set) Token: 0x06002AEB RID: 10987 RVA: 0x00104D45 File Offset: 0x00102F45
		[XmlAttribute("source", DataType = "anyURI")]
		public string Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		/// <summary>Gets or sets an array of <see cref="T:System.Xml.XmlNode" /> objects that represents the appinfo child nodes.</summary>
		/// <returns>An array of <see cref="T:System.Xml.XmlNode" /> objects that represents the appinfo child nodes.</returns>
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x00104D4E File Offset: 0x00102F4E
		// (set) Token: 0x06002AED RID: 10989 RVA: 0x00104D56 File Offset: 0x00102F56
		[XmlAnyElement]
		[XmlText]
		public XmlNode[] Markup
		{
			get
			{
				return this.markup;
			}
			set
			{
				this.markup = value;
			}
		}

		// Token: 0x04001D2B RID: 7467
		private string source;

		// Token: 0x04001D2C RID: 7468
		private XmlNode[] markup;
	}
}
