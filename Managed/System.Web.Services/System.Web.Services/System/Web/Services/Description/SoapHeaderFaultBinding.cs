using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents an extensibility element added to an <see cref="T:System.Web.Services.Description.InputBinding" /> or an <see cref="T:System.Web.Services.Description.OutputBinding" /> within an XML Web service. It specifies the SOAP header types used to transmit error information within the SOAP header.</summary>
	// Token: 0x02000126 RID: 294
	public class SoapHeaderFaultBinding : ServiceDescriptionFormatExtension
	{
		/// <summary>Gets or sets a value specifying the name of the <see cref="T:System.Web.Services.Description.Message" /> within the XML Web service to which the <see cref="T:System.Web.Services.Description.SoapHeaderFaultBinding" /> applies.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> representing the name of the <see cref="T:System.Web.Services.Description.Message" />. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x0003C67D File Offset: 0x0003A87D
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x0003C685 File Offset: 0x0003A885
		[XmlAttribute("message")]
		public XmlQualifiedName Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		/// <summary>Gets or sets a value indicating which <see cref="T:System.Web.Services.Description.MessagePart" /> within the XML Web service the <see cref="T:System.Web.Services.Description.SoapHeaderFaultBinding" /> applies to.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Services.Description.MessagePart" /> to which the <see cref="T:System.Web.Services.Description.SoapHeaderFaultBinding" /> applies.</returns>
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0003C68E File Offset: 0x0003A88E
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x0003C696 File Offset: 0x0003A896
		[XmlAttribute("part")]
		public string Part
		{
			get
			{
				return this.part;
			}
			set
			{
				this.part = value;
			}
		}

		/// <summary>Specifies whether the header is encoded using rules specified by the <see cref="P:System.Web.Services.Description.SoapHeaderBinding.Encoding" /> property, or is encapsulated within a concrete schema.</summary>
		/// <returns>One of the <see cref="T:System.Web.Services.Description.SoapBindingUse" /> values. The default is Default.</returns>
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x0003C69F File Offset: 0x0003A89F
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x0003C6A7 File Offset: 0x0003A8A7
		[DefaultValue(SoapBindingUse.Default)]
		[XmlAttribute("use")]
		public SoapBindingUse Use
		{
			get
			{
				return this.use;
			}
			set
			{
				this.use = value;
			}
		}

		/// <summary>Gets or sets a URI representing the encoding style used to encode the error message for the SOAP header.</summary>
		/// <returns>A string containing a URI. The default value is an empty string ("").</returns>
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x0003C6B0 File Offset: 0x0003A8B0
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x0003C6C6 File Offset: 0x0003A8C6
		[XmlAttribute("encodingStyle")]
		[DefaultValue("")]
		public string Encoding
		{
			get
			{
				if (this.encoding != null)
				{
					return this.encoding;
				}
				return string.Empty;
			}
			set
			{
				this.encoding = value;
			}
		}

		/// <summary>Get or sets a URI representing the location of the specifications for encoding content not specifically defined by the <see cref="P:System.Web.Services.Description.SoapHeaderFaultBinding.Encoding" /> property.</summary>
		/// <returns>Returns a string representing a URI.</returns>
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x0003C6CF File Offset: 0x0003A8CF
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x0003C6E5 File Offset: 0x0003A8E5
		[XmlAttribute("namespace")]
		[DefaultValue("")]
		public string Namespace
		{
			get
			{
				if (this.ns != null)
				{
					return this.ns;
				}
				return string.Empty;
			}
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x04000547 RID: 1351
		private XmlQualifiedName message = XmlQualifiedName.Empty;

		// Token: 0x04000548 RID: 1352
		private string part;

		// Token: 0x04000549 RID: 1353
		private SoapBindingUse use;

		// Token: 0x0400054A RID: 1354
		private string encoding;

		// Token: 0x0400054B RID: 1355
		private string ns;
	}
}
