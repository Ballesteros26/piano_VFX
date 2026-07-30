using System;

namespace System.Net
{
	/// <summary>Provides the base interface for implementation of proxy access for the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x020004C2 RID: 1218
	public interface IWebProxy
	{
		/// <summary>Returns the URI of a proxy.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that contains the URI of the proxy used to contact <paramref name="destination" />.</returns>
		/// <param name="destination">A <see cref="T:System.Uri" /> that specifies the requested Internet resource. </param>
		// Token: 0x0600241D RID: 9245
		Uri GetProxy(Uri destination);

		/// <summary>Indicates that the proxy should not be used for the specified host.</summary>
		/// <returns>true if the proxy server should not be used for <paramref name="host" />; otherwise, false.</returns>
		/// <param name="host">The <see cref="T:System.Uri" /> of the host to check for proxy use. </param>
		// Token: 0x0600241E RID: 9246
		bool IsBypassed(Uri host);

		/// <summary>The credentials to submit to the proxy server for authentication.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> instance that contains the credentials that are needed to authenticate a request to the proxy server.</returns>
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600241F RID: 9247
		// (set) Token: 0x06002420 RID: 9248
		ICredentials Credentials { get; set; }
	}
}
