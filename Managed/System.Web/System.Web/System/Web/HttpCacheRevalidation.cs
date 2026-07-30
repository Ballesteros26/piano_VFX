using System;

namespace System.Web
{
	/// <summary>Provides enumerated values that are used to set revalidation-specific Cache-Control HTTP headers.</summary>
	// Token: 0x02000082 RID: 130
	public enum HttpCacheRevalidation
	{
		/// <summary>Sets the HTTP header to Cache-Control: must-revalidate.</summary>
		// Token: 0x04000F1A RID: 3866
		AllCaches = 1,
		/// <summary>Sets the HTTP header to Cache-Control: proxy-revalidate.</summary>
		// Token: 0x04000F1B RID: 3867
		ProxyCaches,
		/// <summary>If this value is set, no cache-revalidation directive is sent. The default value. </summary>
		// Token: 0x04000F1C RID: 3868
		None
	}
}
