using System;
using System.Security;

namespace System.IO
{
	// Token: 0x02000398 RID: 920
	internal abstract class SearchResultHandler<TSource>
	{
		// Token: 0x06002ADC RID: 10972
		[SecurityCritical]
		internal abstract bool IsResultIncluded(SearchResult result);

		// Token: 0x06002ADD RID: 10973
		[SecurityCritical]
		internal abstract TSource CreateObject(SearchResult result);
	}
}
