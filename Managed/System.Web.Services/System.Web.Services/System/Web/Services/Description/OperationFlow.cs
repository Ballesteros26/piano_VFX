using System;

namespace System.Web.Services.Description
{
	/// <summary>Specifies the type of transmission an endpoint of the XML Web service can support.</summary>
	// Token: 0x020000FE RID: 254
	public enum OperationFlow
	{
		/// <summary>Indicates that the endpoint of the XML Web service receives no transmissions.</summary>
		// Token: 0x04000417 RID: 1047
		None,
		/// <summary>Indicates that the endpoint of the XML Web service receives a message.</summary>
		// Token: 0x04000418 RID: 1048
		OneWay,
		/// <summary>Indicates that the endpoint of the XML Web service sends a message.</summary>
		// Token: 0x04000419 RID: 1049
		Notification,
		/// <summary>Indicates that the endpoint of the XML Web service receives a message, then sends a correlated message.</summary>
		// Token: 0x0400041A RID: 1050
		RequestResponse,
		/// <summary>Indicates that the endpoint of the XML Web service sends a message, then receives a correlated message.</summary>
		// Token: 0x0400041B RID: 1051
		SolicitResponse
	}
}
