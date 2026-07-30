using System;
using System.Web.Services.Description;

namespace System.Web.Services.Protocols
{
	/// <summary>Applying the <see cref="T:System.Web.Services.Protocols.SoapDocumentMethodAttribute" /> to a method specifies that SOAP messages to and from the method use Document formatting.</summary>
	// Token: 0x0200005F RID: 95
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class SoapDocumentMethodAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapDocumentMethodAttribute" /> class.</summary>
		// Token: 0x06000236 RID: 566 RVA: 0x000028A3 File Offset: 0x00000AA3
		public SoapDocumentMethodAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapDocumentMethodAttribute" /> class, setting the <see cref="P:System.Web.Services.Protocols.SoapDocumentMethodAttribute.Action" /> property to the value of the <paramref name="action" /> parameter.</summary>
		/// <param name="action">The SOAPAction HTTP header field of the SOAP request. Sets the <see cref="P:System.Web.Services.Protocols.SoapDocumentMethodAttribute.Action" /> property. </param>
		// Token: 0x06000237 RID: 567 RVA: 0x0000B7BF File Offset: 0x000099BF
		public SoapDocumentMethodAttribute(string action)
		{
			this.action = action;
		}

		/// <summary>Gets or sets the SOAPAction HTTP header field of the SOAP request.</summary>
		/// <returns>The SOAPAction HTTP header field of the SOAP request. The default is http://tempuri.org/MethodName, where MethodName is the name of the XML Web service method.</returns>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000B7CE File Offset: 0x000099CE
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000B7D6 File Offset: 0x000099D6
		public string Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}

		/// <summary>Gets or sets whether an XML Web service client waits for the Web server to finish processing an XML Web service method.</summary>
		/// <returns>true if the XML Web service client does not wait for the Web server to completely process an XML Web service method. The default value is false.</returns>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000B7DF File Offset: 0x000099DF
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000B7E7 File Offset: 0x000099E7
		public bool OneWay
		{
			get
			{
				return this.oneWay;
			}
			set
			{
				this.oneWay = value;
			}
		}

		/// <summary>Gets or sets the namespace associated with the SOAP request for an XML Web service method.</summary>
		/// <returns>The XML namespace associated with the SOAP request for an XML Web service method. The default is http://tempuri.org/.</returns>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000B7F0 File Offset: 0x000099F0
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000B7F8 File Offset: 0x000099F8
		public string RequestNamespace
		{
			get
			{
				return this.requestNamespace;
			}
			set
			{
				this.requestNamespace = value;
			}
		}

		/// <summary>Gets or sets the XML namespace associated with the SOAP response for an XML Web service method.</summary>
		/// <returns>The XML namespace associated with the SOAP response for an XML Web service method. The default is http://tempuri.org/.</returns>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000B801 File Offset: 0x00009A01
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000B809 File Offset: 0x00009A09
		public string ResponseNamespace
		{
			get
			{
				return this.responseNamespace;
			}
			set
			{
				this.responseNamespace = value;
			}
		}

		/// <summary>Gets or sets the XML element associated with the SOAP request for an XML Web service method, which is defined in a service description as an operation.</summary>
		/// <returns>The XML element associated with the SOAP request for an XML Web service method, which is defined in an service description as an operation. The default value is the name of the XML Web service method.</returns>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000B812 File Offset: 0x00009A12
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000B828 File Offset: 0x00009A28
		public string RequestElementName
		{
			get
			{
				if (this.requestName != null)
				{
					return this.requestName;
				}
				return string.Empty;
			}
			set
			{
				this.requestName = value;
			}
		}

		/// <summary>Gets or sets the XML element associated with the SOAP response for an XML Web service method.</summary>
		/// <returns>The XML element associated with the SOAP request for an XML Web service method. The default value is WebServiceNameResult, where WebServiceName is the name of the XML Web service method.</returns>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000B831 File Offset: 0x00009A31
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000B847 File Offset: 0x00009A47
		public string ResponseElementName
		{
			get
			{
				if (this.responseName != null)
				{
					return this.responseName;
				}
				return string.Empty;
			}
			set
			{
				this.responseName = value;
			}
		}

		/// <summary>Gets or sets the parameter formatting for an XML Web service method within the XML portion of a SOAP message.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Description.SoapBindingUse" /> for the XML Web service method. The default is <see cref="F:System.Web.Services.Description.SoapBindingUse.Literal" />.</returns>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000B850 File Offset: 0x00009A50
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000B858 File Offset: 0x00009A58
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

		/// <summary>Gets or sets whether parameters are encapsulated within a single XML element beneath the Body element in the XML portion of a SOAP message.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Protocols.SoapParameterStyle" /> for SOAP messages sent to and from an XML Web service method. The default value is <see cref="F:System.Web.Services.Protocols.SoapParameterStyle.Wrapped" />.</returns>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000B861 File Offset: 0x00009A61
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000B869 File Offset: 0x00009A69
		public SoapParameterStyle ParameterStyle
		{
			get
			{
				return this.style;
			}
			set
			{
				this.style = value;
			}
		}

		/// <summary>Gets or sets the binding an XML Web service method is implementing an operation for.</summary>
		/// <returns>The binding an XML Web service method is implementing an operation for. The default is the name of the XML Web service with "Soap" appended.</returns>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000B872 File Offset: 0x00009A72
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000B888 File Offset: 0x00009A88
		public string Binding
		{
			get
			{
				if (this.binding != null)
				{
					return this.binding;
				}
				return string.Empty;
			}
			set
			{
				this.binding = value;
			}
		}

		// Token: 0x04000258 RID: 600
		private string action;

		// Token: 0x04000259 RID: 601
		private string requestName;

		// Token: 0x0400025A RID: 602
		private string responseName;

		// Token: 0x0400025B RID: 603
		private string requestNamespace;

		// Token: 0x0400025C RID: 604
		private string responseNamespace;

		// Token: 0x0400025D RID: 605
		private bool oneWay;

		// Token: 0x0400025E RID: 606
		private SoapBindingUse use;

		// Token: 0x0400025F RID: 607
		private SoapParameterStyle style;

		// Token: 0x04000260 RID: 608
		private string binding;
	}
}
