using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000072 RID: 114
	internal class nsAccessibleRelation
	{
		// Token: 0x06000365 RID: 869 RVA: 0x00004DED File Offset: 0x00002FED
		public static nsIAccessibleRelation GetProxy(IWebBrowser control, nsIAccessibleRelation obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIAccessibleRelation).GUID, obj) as nsIAccessibleRelation;
		}
	}
}
