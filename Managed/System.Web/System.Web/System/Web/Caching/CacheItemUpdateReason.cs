using System;

namespace System.Web.Caching
{
	/// <summary>Specifies the reason that a cached item is being removed from the <see cref="T:System.Web.Caching.Cache" /> object.</summary>
	// Token: 0x02000689 RID: 1673
	public enum CacheItemUpdateReason
	{
		/// <summary>Specifies that the item is being removed from the cache because the absolute or sliding expiration interval expired.</summary>
		// Token: 0x040025A0 RID: 9632
		Expired = 1,
		/// <summary>Specifies that the item is being removed from the cache because the associated <see cref="T:System.Web.Caching.CacheDependency" /> object changed.</summary>
		// Token: 0x040025A1 RID: 9633
		DependencyChanged
	}
}
