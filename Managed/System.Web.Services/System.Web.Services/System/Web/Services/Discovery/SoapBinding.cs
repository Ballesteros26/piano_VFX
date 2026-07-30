using System;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery
{
	/// <summary>Represents a SOAP binding in a discovery document. This class cannot be inherited.</summary>
	// Token: 0x020000B9 RID: 185
	[XmlRoot("soap", Namespace = "http://schemas.xmlsoap.org/disco/soap/")]
	public sealed class SoapBinding
	{
		/// <summary>Gets or sets the URL of the XML Web service implementing the SOAP binding.</summary>
		/// <returns>The URL of the XML Web service implementing the SOAP binding. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x000169D4 File Offset: 0x00014BD4
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x000169DC File Offset: 0x00014BDC
		[XmlAttribute("address")]
		public string Address
		{
			get
			{
				return this.address;
			}
			set
			{
				if (value == null)
				{
					this.address = "";
					return;
				}
				this.address = value;
			}
		}

		/// <summary>Gets or sets the XML qualified name of the SOAP binding implemented by the XML Web service.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlQualifiedName" /> of the SOAP binding implemented by the XML Web service.</returns>
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x000169F4 File Offset: 0x00014BF4
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x000169FC File Offset: 0x00014BFC
		[XmlAttribute("binding")]
		public XmlQualifiedName Binding
		{
			get
			{
				return this.binding;
			}
			set
			{
				this.binding = value;
			}
		}

		/// <summary>The XML namespace of the element that specifies a SOAP binding within a discovery document.</summary>
		// Token: 0x04000366 RID: 870
		public const string Namespace = "http://schemas.xmlsoap.org/disco/soap/";

		// Token: 0x04000367 RID: 871
		private XmlQualifiedName binding;

		// Token: 0x04000368 RID: 872
		private string address = "";
	}
}
