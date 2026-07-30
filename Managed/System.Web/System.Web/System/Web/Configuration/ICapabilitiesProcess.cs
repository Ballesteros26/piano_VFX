using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.Configuration
{
	// Token: 0x020005B3 RID: 1459
	internal interface ICapabilitiesProcess
	{
		// Token: 0x06003E9E RID: 16030
		CapabilitiesResult Process(string userAgent, IDictionary initialCapabilities);

		// Token: 0x06003E9F RID: 16031
		CapabilitiesResult Process(HttpRequest request, IDictionary initialCapabilities);

		// Token: 0x06003EA0 RID: 16032
		CapabilitiesResult Process(NameValueCollection header, IDictionary initialCapabilities);
	}
}
