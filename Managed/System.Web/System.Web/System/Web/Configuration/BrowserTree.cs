using System;
using System.Collections.Specialized;

namespace System.Web.Configuration
{
	// Token: 0x02000560 RID: 1376
	internal class BrowserTree : OrderedDictionary
	{
		// Token: 0x06003B4A RID: 15178 RVA: 0x0009F060 File Offset: 0x0009D260
		internal BrowserTree()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}
	}
}
