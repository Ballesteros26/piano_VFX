using System;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Indicates whether ASP.NET routing is processing a URL from a client or generating a URL.</summary>
	// Token: 0x020004F4 RID: 1268
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public enum RouteDirection
	{
		/// <summary>A URL from a client is being processed.</summary>
		// Token: 0x04001F01 RID: 7937
		IncomingRequest,
		/// <summary>A URL is being created based on the route definition.</summary>
		// Token: 0x04001F02 RID: 7938
		UrlGeneration
	}
}
