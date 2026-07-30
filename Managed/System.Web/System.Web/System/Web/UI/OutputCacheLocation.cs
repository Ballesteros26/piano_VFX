using System;

namespace System.Web.UI
{
	/// <summary>Specifies the valid values for controlling the location of the output-cached HTTP response for a resource.</summary>
	// Token: 0x0200020A RID: 522
	public enum OutputCacheLocation
	{
		/// <summary>The output cache can be located on the browser client (where the request originated), on a proxy server (or any other server) participating in the request, or on the server where the request was processed. This value corresponds to the <see cref="F:System.Web.HttpCacheability.Public" /> enumeration value.</summary>
		// Token: 0x0400149A RID: 5274
		Any,
		/// <summary>The output cache is located on the browser client where the request originated. This value corresponds to the <see cref="F:System.Web.HttpCacheability.Private" /> enumeration value.</summary>
		// Token: 0x0400149B RID: 5275
		Client,
		/// <summary>The output cache can be stored in any HTTP 1.1 cache-capable devices other than the origin server. This includes proxy servers and the client that made the request.</summary>
		// Token: 0x0400149C RID: 5276
		Downstream,
		/// <summary>The output cache is located on the Web server where the request was processed. This value corresponds to the <see cref="F:System.Web.HttpCacheability.Server" /> enumeration value.</summary>
		// Token: 0x0400149D RID: 5277
		Server,
		/// <summary>The output cache is disabled for the requested page. This value corresponds to the <see cref="F:System.Web.HttpCacheability.NoCache" /> enumeration value.</summary>
		// Token: 0x0400149E RID: 5278
		None,
		/// <summary>The output cache can be stored only at the origin server or at the requesting client. Proxy servers are not allowed to cache the response. This value corresponds to the combination of the <see cref="F:System.Web.HttpCacheability.Private" /> and <see cref="F:System.Web.HttpCacheability.Server" /> enumeration values.</summary>
		// Token: 0x0400149F RID: 5279
		ServerAndClient
	}
}
