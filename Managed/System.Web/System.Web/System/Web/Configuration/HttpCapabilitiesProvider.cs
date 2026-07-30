using System;

namespace System.Web.Configuration
{
	/// <summary>Enables you to customize browser definitions. You can also customize the algorithm that identifies the browser based on information in the incoming <see cref="T:System.Web.HttpRequest" />.</summary>
	// Token: 0x020005A8 RID: 1448
	public abstract class HttpCapabilitiesProvider
	{
		/// <summary>Gets the <see cref="T:System.Web.HttpBrowserCapabilities" /> object for the current browser.</summary>
		/// <returns>The <see cref="T:System.Web.HttpBrowserCapabilities" /> object for the current browser.</returns>
		/// <param name="request">The current <see cref="T:System.Web.HttpRequest" /> object.</param>
		// Token: 0x06003E09 RID: 15881
		public abstract HttpBrowserCapabilities GetBrowserCapabilities(HttpRequest request);
	}
}
