using System;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000092 RID: 146
	internal class nsDOMCharacterData
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x0000503D File Offset: 0x0000323D
		public static nsIDOMCharacterData GetProxy(IWebBrowser control, nsIDOMCharacterData obj)
		{
			return Base.GetProxyForObject(control, typeof(nsIDOMCharacterData).GUID, obj) as nsIDOMCharacterData;
		}
	}
}
