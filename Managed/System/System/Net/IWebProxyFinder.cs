using System;
using System.Collections.Generic;

namespace System.Net
{
	// Token: 0x02000434 RID: 1076
	internal interface IWebProxyFinder : IDisposable
	{
		// Token: 0x06002084 RID: 8324
		bool GetProxies(Uri destination, out IList<string> proxyList);

		// Token: 0x06002085 RID: 8325
		void Abort();

		// Token: 0x06002086 RID: 8326
		void Reset();

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06002087 RID: 8327
		bool IsValid { get; }
	}
}
