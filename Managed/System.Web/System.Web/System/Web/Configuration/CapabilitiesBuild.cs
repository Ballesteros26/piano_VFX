using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace System.Web.Configuration
{
	// Token: 0x0200058C RID: 1420
	internal abstract class CapabilitiesBuild : ICapabilitiesProcess
	{
		// Token: 0x06003C0F RID: 15375
		protected abstract Collection<string> HeaderNames(Collection<string> list);

		// Token: 0x06003C10 RID: 15376 RVA: 0x000A06CC File Offset: 0x0009E8CC
		public CapabilitiesResult Process(string userAgent, IDictionary initialCapabilities)
		{
			return this.Process(new NameValueCollection(1) { { "User-Agent", userAgent } }, initialCapabilities);
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x000A06F4 File Offset: 0x0009E8F4
		public CapabilitiesResult Process(HttpRequest request, IDictionary initialCapabilities)
		{
			if (request != null)
			{
				return this.Process(request.Headers, initialCapabilities);
			}
			return this.Process("", initialCapabilities);
		}

		// Token: 0x06003C12 RID: 15378
		public abstract CapabilitiesResult Process(NameValueCollection header, IDictionary initialCapabilities);
	}
}
