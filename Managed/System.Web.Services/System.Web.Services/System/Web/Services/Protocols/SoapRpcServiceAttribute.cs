using System;
using System.Runtime.InteropServices;
using System.Web.Services.Description;

namespace System.Web.Services.Protocols
{
	/// <summary>Sets the default format of SOAP requests and responses sent to and from XML Web service methods within the XML Web service.</summary>
	// Token: 0x0200007A RID: 122
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SoapRpcServiceAttribute : Attribute
	{
		/// <summary>Gets or sets how SOAP messages are routed to the XML Web service.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Protocols.SoapServiceRoutingStyle" /> that represents how SOAP messages are routed to the XML Web service. The default value is <see cref="F:System.Web.Services.Protocols.SoapServiceRoutingStyle.SoapAction" />.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000E508 File Offset: 0x0000C708
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0000E510 File Offset: 0x0000C710
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

		/// <summary>Gets or sets the binding used when invoking the Web service's methods.</summary>
		/// <returns>A member of the <see cref="T:System.Web.Services.Description.SoapBindingUse" /> enumeration specifying the binding used when invoking the Web service's methods.</returns>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000E519 File Offset: 0x0000C719
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000E521 File Offset: 0x0000C721
		[ComVisible(false)]
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

		// Token: 0x040002D3 RID: 723
		private SoapServiceRoutingStyle routingStyle;

		// Token: 0x040002D4 RID: 724
		private SoapBindingUse use = SoapBindingUse.Encoded;
	}
}
