using System;

namespace System.Web.Services.Configuration
{
	/// <summary>Specifies the transmission protocols that are used to decrypt data sent from a client browser in the HTTP request.</summary>
	// Token: 0x02000141 RID: 321
	[Flags]
	public enum WebServiceProtocols
	{
		/// <summary>Unknown protocol.</summary>
		// Token: 0x040005A0 RID: 1440
		Unknown = 0,
		/// <summary>The HTTP SOAP protocol.</summary>
		// Token: 0x040005A1 RID: 1441
		HttpSoap = 1,
		/// <summary>The HTTP GET protocol.</summary>
		// Token: 0x040005A2 RID: 1442
		HttpGet = 2,
		/// <summary>The HTTP POST protocol.</summary>
		// Token: 0x040005A3 RID: 1443
		HttpPost = 4,
		/// <summary>The Web Services Documentation protocol.</summary>
		// Token: 0x040005A4 RID: 1444
		Documentation = 8,
		/// <summary>The HTTP POST LOCALHOST protocol.</summary>
		// Token: 0x040005A5 RID: 1445
		HttpPostLocalhost = 16,
		/// <summary>The HTTP SOAP version 1.2 protocol.</summary>
		// Token: 0x040005A6 RID: 1446
		HttpSoap12 = 32,
		/// <summary>Any version of the HTTP SOAP protocol.</summary>
		// Token: 0x040005A7 RID: 1447
		AnyHttpSoap = 33
	}
}
