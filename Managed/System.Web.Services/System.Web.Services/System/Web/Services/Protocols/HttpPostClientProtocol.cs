using System;
using System.Net;

namespace System.Web.Services.Protocols
{
	/// <summary>The base class for XML Web service client proxies that use the HTTP-POST protocol.</summary>
	// Token: 0x02000037 RID: 55
	public class HttpPostClientProtocol : HttpSimpleClientProtocol
	{
		/// <summary>Creates a <see cref="T:System.Net.WebRequest" /> instance for the specified URI.</summary>
		/// <returns>The <see cref="T:System.Net.WebRequest" /> instance.</returns>
		/// <param name="uri">The <see cref="T:System.Uri" /> to use when creating the <see cref="T:System.Net.WebRequest" />. </param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="uri" /> parameter is null or has a length of zero. </exception>
		// Token: 0x06000124 RID: 292 RVA: 0x000056CE File Offset: 0x000038CE
		protected override WebRequest GetWebRequest(Uri uri)
		{
			WebRequest webRequest = base.GetWebRequest(uri);
			webRequest.Method = "POST";
			return webRequest;
		}
	}
}
