using System;

namespace System.Web.UI
{
	/// <summary>Describes the way data cached using ASP.NET caching mechanisms expires when a time-out is set. </summary>
	// Token: 0x0200015B RID: 347
	public enum DataSourceCacheExpiry
	{
		/// <summary>Cached data expires when the amount of time specified by the CacheDuration property has passed since the data was first cached.</summary>
		// Token: 0x04001235 RID: 4661
		Absolute,
		/// <summary>Cached data expires only when the cache entry has not been used for the amount of time specified by the CacheDuration property.</summary>
		// Token: 0x04001236 RID: 4662
		Sliding
	}
}
