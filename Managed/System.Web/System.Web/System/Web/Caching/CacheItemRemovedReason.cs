using System;

namespace System.Web.Caching
{
	/// <summary>Specifies the reason an item was removed from the <see cref="T:System.Web.Caching.Cache" />.</summary>
	// Token: 0x02000687 RID: 1671
	public enum CacheItemRemovedReason
	{
		/// <summary>The item is removed from the cache by a <see cref="M:System.Web.Caching.Cache.Remove(System.String)" /> method call or by an <see cref="M:System.Web.Caching.Cache.Insert(System.String,System.Object)" /> method call that specified the same key.</summary>
		// Token: 0x0400259B RID: 9627
		Removed = 1,
		/// <summary>The item is removed from the cache because it expired.</summary>
		// Token: 0x0400259C RID: 9628
		Expired,
		/// <summary>The item is removed from the cache because the system removed it to free memory.</summary>
		// Token: 0x0400259D RID: 9629
		Underused,
		/// <summary>The item is removed from the cache because the cache dependency associated with it changed.</summary>
		// Token: 0x0400259E RID: 9630
		DependencyChanged
	}
}
