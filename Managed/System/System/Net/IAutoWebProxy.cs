using System;

namespace System.Net
{
	// Token: 0x02000493 RID: 1171
	internal interface IAutoWebProxy : IWebProxy
	{
		// Token: 0x060022B8 RID: 8888
		ProxyChain GetProxies(Uri destination);
	}
}
