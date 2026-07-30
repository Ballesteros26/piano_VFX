using System;
using System.Web.Services.Description;

namespace System.Web.Services.Protocols
{
	/// <summary>Applying the optional <see cref="T:System.Web.Services.Protocols.SoapDocumentServiceAttribute" /> to an XML Web service sets the default format of SOAP requests and responses sent to and from XML Web service methods within the XML Web service.</summary>
	// Token: 0x02000060 RID: 96
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SoapDocumentServiceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapDocumentServiceAttribute" /> class setting all properties to their default values.</summary>
		// Token: 0x0600024A RID: 586 RVA: 0x000028A3 File Offset: 0x00000AA3
		public SoapDocumentServiceAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapDocumentServiceAttribute" /> class setting the parameter formatting.</summary>
		/// <param name="use">The parameter formatting of the XML Web service. Sets the <see cref="P:System.Web.Services.Protocols.SoapDocumentServiceAttribute.Use" /> property. </param>
		// Token: 0x0600024B RID: 587 RVA: 0x0000B891 File Offset: 0x00009A91
		public SoapDocumentServiceAttribute(SoapBindingUse use)
		{
			this.use = use;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapDocumentServiceAttribute" /> class that sets the parameter formatting and sets whether parameters are encapsulated within a single XML element, under the Body element, in SOAP messages.</summary>
		/// <param name="use">The parameter formatting style. Sets the <see cref="P:System.Web.Services.Protocols.SoapDocumentServiceAttribute.Use" /> property. </param>
		/// <param name="paramStyle">Sets whether parameters are encapsulated within a single XML element, under the Body element, in SOAP messages sent to and from XML Web service methods within the XML Web service. Sets the <see cref="P:System.Web.Services.Protocols.SoapDocumentServiceAttribute.ParameterStyle" /> property. </param>
		// Token: 0x0600024C RID: 588 RVA: 0x0000B8A0 File Offset: 0x00009AA0
		public SoapDocumentServiceAttribute(SoapBindingUse use, SoapParameterStyle paramStyle)
		{
			this.use = use;
			this.paramStyle = paramStyle;
		}

		/// <summary>Gets or sets the default parameter formatting for an XML Web service.</summary>
		/// <returns>The default <see cref="T:System.Web.Services.Description.SoapBindingUse" /> for the XML Web service. If not set, the default is <see cref="F:System.Web.Services.Description.SoapBindingUse.Literal" />.</returns>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000B8B6 File Offset: 0x00009AB6
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000B8BE File Offset: 0x00009ABE
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

		/// <summary>Gets or sets the default setting that controls whether parameters are encapsulated within a single element following the &lt;Body&gt; element in the XML portion of a SOAP message for XML Web service methods of the XML Web service.</summary>
		/// <returns>The default <see cref="T:System.Web.Services.Protocols.SoapParameterStyle" /> for SOAP requests and SOAP responses to and from XML Web service methods within the XML Web service. If not set, the default is <see cref="F:System.Web.Services.Protocols.SoapParameterStyle.Wrapped" />.</returns>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000B8C7 File Offset: 0x00009AC7
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000B8CF File Offset: 0x00009ACF
		public SoapParameterStyle ParameterStyle
		{
			get
			{
				return this.paramStyle;
			}
			set
			{
				this.paramStyle = value;
			}
		}

		/// <summary>Gets or sets how SOAP messages are routed to the XML Web service.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Protocols.SoapServiceRoutingStyle" /> that represents how SOAP messages are routed to the XML Web service. The default value is <see cref="F:System.Web.Services.Protocols.SoapServiceRoutingStyle.SoapAction" />.</returns>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000B8D8 File Offset: 0x00009AD8
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000B8E0 File Offset: 0x00009AE0
		public SoapServiceRoutingStyle RoutingStyle
		{
			get
			{
				return this.routingStyle;
			}
			set
			{
				this.routingStyle = value;
			}
		}

		// Token: 0x04000261 RID: 609
		private SoapBindingUse use;

		// Token: 0x04000262 RID: 610
		private SoapParameterStyle paramStyle;

		// Token: 0x04000263 RID: 611
		private SoapServiceRoutingStyle routingStyle;
	}
}
