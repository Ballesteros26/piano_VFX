using System;

namespace System.Net.Cache
{
	// Token: 0x020006BD RID: 1725
	internal class RequestCacheBinding
	{
		// Token: 0x06003614 RID: 13844 RVA: 0x000C6303 File Offset: 0x000C4503
		internal RequestCacheBinding(RequestCache requestCache, RequestCacheValidator cacheValidator, RequestCachePolicy policy)
		{
			this.m_RequestCache = requestCache;
			this.m_CacheValidator = cacheValidator;
			this.m_Policy = policy;
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06003615 RID: 13845 RVA: 0x000C6320 File Offset: 0x000C4520
		internal RequestCache Cache
		{
			get
			{
				return this.m_RequestCache;
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06003616 RID: 13846 RVA: 0x000C6328 File Offset: 0x000C4528
		internal RequestCacheValidator Validator
		{
			get
			{
				return this.m_CacheValidator;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06003617 RID: 13847 RVA: 0x000C6330 File Offset: 0x000C4530
		internal RequestCachePolicy Policy
		{
			get
			{
				return this.m_Policy;
			}
		}

		// Token: 0x04002ABF RID: 10943
		private RequestCache m_RequestCache;

		// Token: 0x04002AC0 RID: 10944
		private RequestCacheValidator m_CacheValidator;

		// Token: 0x04002AC1 RID: 10945
		private RequestCachePolicy m_Policy;
	}
}
