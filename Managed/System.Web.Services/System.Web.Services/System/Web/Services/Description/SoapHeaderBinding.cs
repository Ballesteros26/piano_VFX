using System;
using System.ComponentModel;
using System.Web.Services.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents an extensibility element added to an <see cref="T:System.Web.Services.Description.InputBinding" /> or an <see cref="T:System.Web.Services.Description.OutputBinding" /> within an XML Web service.</summary>
	// Token: 0x02000125 RID: 293
	[XmlFormatExtension("header", "http://schemas.xmlsoap.org/wsdl/soap/", typeof(InputBinding), typeof(OutputBinding))]
	public class SoapHeaderBinding : ServiceDescriptionFormatExtension
	{
		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.Services.Description.SoapHeaderBinding" /> instance is mapped to a specific property in generated proxy classes.</summary>
		/// <returns>true if the <see cref="T:System.Web.Services.Description.SoapHeaderBinding" /> maps to a specific property; otherwise false.</returns>
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0003C5D7 File Offset: 0x0003A7D7
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x0003C5DF File Offset: 0x0003A7DF
		[XmlIgnore]
		public bool MapToProperty
		{
			get
			{
				return this.mapToProperty;
			}
			set
			{
				this.mapToProperty = value;
			}
		}

		/// <summary>Gets or sets a value specifying the name of the <see cref="T:System.Web.Services.Description.Message" /> within the XML Web service to which the <see cref="T:System.Web.Services.Description.SoapHeaderBinding" /> applies.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> representing the name of the <see cref="T:System.Web.Services.Description.Message" />.</returns>
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0003C5E8 File Offset: 0x0003A7E8
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x0003C5F0 File Offset: 0x0003A7F0
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

		/// <summary>Gets or sets a value indicating to which <see cref="T:System.Web.Services.Description.MessagePart" /> within the XML Web service the <see cref="T:System.Web.Services.Description.SoapHeaderBinding" /> applies.</summary>
		/// <returns>A string representing the name of the <see cref="T:System.Web.Services.Description.MessagePart" /> to which the <see cref="T:System.Web.Services.Description.SoapHeaderBinding" /> applies.</returns>
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0003C5F9 File Offset: 0x0003A7F9
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x0003C601 File Offset: 0x0003A801
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

		/// <summary>Specifies whether the header is encoded using rules specified by the <see cref="P:System.Web.Services.Description.SoapHeaderBinding.Encoding" /> property, or is encapsulated within a concrete XML schema.</summary>
		/// <returns>One of the <see cref="T:System.Web.Services.Description.SoapBindingUse" /> values. The default is Default.</returns>
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0003C60A File Offset: 0x0003A80A
		// (set) Token: 0x060008CA RID: 2250 RVA: 0x0003C612 File Offset: 0x0003A812
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

		/// <summary>Gets or sets a URI representing the encoding style used to encode the SOAP header.</summary>
		/// <returns>A string containing a URI. The default value is an empty string ("").</returns>
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x0003C61B File Offset: 0x0003A81B
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x0003C631 File Offset: 0x0003A831
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

		/// <summary>Get or sets the URI representing the location of the specification for encoding of content not specifically defined by the <see cref="P:System.Web.Services.Description.SoapHeaderBinding.Encoding" /> property.</summary>
		/// <returns>A string representing a URI.</returns>
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0003C63A File Offset: 0x0003A83A
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x0003C650 File Offset: 0x0003A850
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

		/// <summary>Gets or sets the extension type controlling the output in a WSDL document for the headerfault XML element of a SOAP header.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.SoapHeaderFaultBinding" /> representing the SOAP header types used to transmit error information.</returns>
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0003C659 File Offset: 0x0003A859
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x0003C661 File Offset: 0x0003A861
		[XmlElement("headerfault")]
		public SoapHeaderFaultBinding Fault
		{
			get
			{
				return this.fault;
			}
			set
			{
				this.fault = value;
			}
		}

		// Token: 0x04000540 RID: 1344
		private XmlQualifiedName message = XmlQualifiedName.Empty;

		// Token: 0x04000541 RID: 1345
		private string part;

		// Token: 0x04000542 RID: 1346
		private SoapBindingUse use;

		// Token: 0x04000543 RID: 1347
		private string encoding;

		// Token: 0x04000544 RID: 1348
		private string ns;

		// Token: 0x04000545 RID: 1349
		private bool mapToProperty;

		// Token: 0x04000546 RID: 1350
		private SoapHeaderFaultBinding fault;
	}
}
