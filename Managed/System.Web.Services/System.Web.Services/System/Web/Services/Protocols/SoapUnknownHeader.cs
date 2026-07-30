using System;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols
{
	/// <summary>Represents the data received from a SOAP header that was not understood by the recipient XML Web service or XML Web service client. This class cannot be inherited.</summary>
	// Token: 0x02000083 RID: 131
	public sealed class SoapUnknownHeader : SoapHeader
	{
		/// <summary>Gets or sets the XML Header element for a SOAP request or response.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> representing the raw XML of the SOAP header.</returns>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000381 RID: 897 RVA: 0x000109E4 File Offset: 0x0000EBE4
		// (set) Token: 0x06000382 RID: 898 RVA: 0x00010B1C File Offset: 0x0000ED1C
		[XmlIgnore]
		public XmlElement Element
		{
			get
			{
				if (this.element == null)
				{
					return null;
				}
				if (this.version == SoapProtocolVersion.Soap12)
				{
					if (this.InternalMustUnderstand)
					{
						this.element.SetAttribute("mustUnderstand", "http://www.w3.org/2003/05/soap-envelope", "1");
					}
					this.element.RemoveAttribute("mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/");
					string internalActor = this.InternalActor;
					if (internalActor != null && internalActor.Length != 0)
					{
						this.element.SetAttribute("role", "http://www.w3.org/2003/05/soap-envelope", internalActor);
					}
					this.element.RemoveAttribute("actor", "http://schemas.xmlsoap.org/soap/envelope/");
				}
				else if (this.version == SoapProtocolVersion.Soap11)
				{
					if (this.InternalMustUnderstand)
					{
						this.element.SetAttribute("mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/", "1");
					}
					this.element.RemoveAttribute("mustUnderstand", "http://www.w3.org/2003/05/soap-envelope");
					string internalActor2 = this.InternalActor;
					if (internalActor2 != null && internalActor2.Length != 0)
					{
						this.element.SetAttribute("actor", "http://schemas.xmlsoap.org/soap/envelope/", internalActor2);
					}
					this.element.RemoveAttribute("role", "http://www.w3.org/2003/05/soap-envelope");
					this.element.RemoveAttribute("relay", "http://www.w3.org/2003/05/soap-envelope");
				}
				return this.element;
			}
			set
			{
				if (value == null && this.element != null)
				{
					base.InternalMustUnderstand = this.InternalMustUnderstand;
					base.InternalActor = this.InternalActor;
				}
				this.element = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00010B48 File Offset: 0x0000ED48
		// (set) Token: 0x06000384 RID: 900 RVA: 0x00010BD8 File Offset: 0x0000EDD8
		internal override bool InternalMustUnderstand
		{
			get
			{
				if (this.element == null)
				{
					return base.InternalMustUnderstand;
				}
				string text = this.GetElementAttribute("mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/", this.element);
				if (text == null)
				{
					text = this.GetElementAttribute("mustUnderstand", "http://www.w3.org/2003/05/soap-envelope", this.element);
					if (text == null)
					{
						return false;
					}
				}
				return !(text == "false") && !(text == "0") && (text == "true" || text == "1");
			}
			set
			{
				base.InternalMustUnderstand = value;
				if (this.element != null)
				{
					if (value)
					{
						this.element.SetAttribute("mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/", "1");
					}
					else
					{
						this.element.RemoveAttribute("mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/");
					}
					this.element.RemoveAttribute("mustUnderstand", "http://www.w3.org/2003/05/soap-envelope");
				}
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00010C40 File Offset: 0x0000EE40
		// (set) Token: 0x06000386 RID: 902 RVA: 0x00010C98 File Offset: 0x0000EE98
		internal override string InternalActor
		{
			get
			{
				if (this.element == null)
				{
					return base.InternalActor;
				}
				string text = this.GetElementAttribute("actor", "http://schemas.xmlsoap.org/soap/envelope/", this.element);
				if (text == null)
				{
					text = this.GetElementAttribute("role", "http://www.w3.org/2003/05/soap-envelope", this.element);
					if (text == null)
					{
						return "";
					}
				}
				return text;
			}
			set
			{
				base.InternalActor = value;
				if (this.element != null)
				{
					if (value == null || value.Length == 0)
					{
						this.element.RemoveAttribute("actor", "http://schemas.xmlsoap.org/soap/envelope/");
					}
					else
					{
						this.element.SetAttribute("actor", "http://schemas.xmlsoap.org/soap/envelope/", value);
					}
					this.element.RemoveAttribute("role", "http://www.w3.org/2003/05/soap-envelope");
				}
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00010D04 File Offset: 0x0000EF04
		// (set) Token: 0x06000388 RID: 904 RVA: 0x00010D78 File Offset: 0x0000EF78
		internal override bool InternalRelay
		{
			get
			{
				if (this.element == null)
				{
					return base.InternalRelay;
				}
				string elementAttribute = this.GetElementAttribute("relay", "http://www.w3.org/2003/05/soap-envelope", this.element);
				return elementAttribute != null && (!(elementAttribute == "false") && !(elementAttribute == "0")) && (elementAttribute == "true" || elementAttribute == "1");
			}
			set
			{
				base.InternalRelay = value;
				if (this.element != null)
				{
					if (value)
					{
						this.element.SetAttribute("relay", "http://www.w3.org/2003/05/soap-envelope", "1");
						return;
					}
					this.element.RemoveAttribute("relay", "http://www.w3.org/2003/05/soap-envelope");
				}
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00010DC8 File Offset: 0x0000EFC8
		private string GetElementAttribute(string name, string ns, XmlElement element)
		{
			if (element == null)
			{
				return null;
			}
			if (element.Prefix.Length == 0 && element.NamespaceURI == ns)
			{
				if (element.HasAttribute(name))
				{
					return element.GetAttribute(name);
				}
				return null;
			}
			else
			{
				if (element.HasAttribute(name, ns))
				{
					return element.GetAttribute(name, ns);
				}
				return null;
			}
		}

		// Token: 0x04000300 RID: 768
		private XmlElement element;
	}
}
