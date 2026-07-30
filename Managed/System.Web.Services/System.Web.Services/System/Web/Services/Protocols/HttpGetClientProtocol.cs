using System;
using System.Net;

namespace System.Web.Services.Protocols
{
	/// <summary>The base class for XML Web service client proxies that use the HTTP-GET protocol.</summary>
	// Token: 0x02000033 RID: 51
	public class HttpGetClientProtocol : HttpSimpleClientProtocol
	{
		/// <summary>Creates a <see cref="T:System.Net.WebRequest" /> instance for the specified URI.</summary>
		/// <returns>The <see cref="T:System.Net.WebRequest" /> instance.</returns>
		/// <param name="uri">The <see cref="T:System.Uri" /> to use when creating the <see cref="T:System.Net.WebRequest" />. </param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="uri" /> parameter is null or has a length of zero. </exception>
		// Token: 0x06000119 RID: 281 RVA: 0x0000562F File Offset: 0x0000382F
		protected override WebRequest GetWebRequest(Uri uri)
		{
			WebRequest webRequest = base.GetWebRequest(uri);
			webRequest.Method = "GET";
			return webRequest;
		}
	}
}
