using System;

namespace System.Web.Services.Protocols
{
	/// <summary>Specifies whether the recipient of the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> is the XML Web service, the XML Web service client, or both.</summary>
	// Token: 0x0200006B RID: 107
	[Flags]
	public enum SoapHeaderDirection
	{
		/// <summary>Specifies the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> is sent to the XML Web service.</summary>
		// Token: 0x04000291 RID: 657
		In = 1,
		/// <summary>Specifies the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> is sent to the XML Web service client.</summary>
		// Token: 0x04000292 RID: 658
		Out = 2,
		/// <summary>Specifies the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> is sent to both the XML Web service and the XML Web service client.</summary>
		// Token: 0x04000293 RID: 659
		InOut = 3,
		/// <summary>Specifies the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> is sent to the XML Web service client when an exception is thrown by the XML Web service method.</summary>
		// Token: 0x04000294 RID: 660
		Fault = 4
	}
}
