using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x020004F0 RID: 1264
	internal class WebProxyData
	{
		// Token: 0x040020B5 RID: 8373
		internal bool bypassOnLocal;

		// Token: 0x040020B6 RID: 8374
		internal bool automaticallyDetectSettings;

		// Token: 0x040020B7 RID: 8375
		internal Uri proxyAddress;

		// Token: 0x040020B8 RID: 8376
		internal Hashtable proxyHostAddresses;

		// Token: 0x040020B9 RID: 8377
		internal Uri scriptLocation;

		// Token: 0x040020BA RID: 8378
		internal ArrayList bypassList;
	}
}
