using System;

namespace System.Web.Caching
{
	/// <summary>Specifies the relative priority of items stored in the <see cref="T:System.Web.Caching.Cache" /> object.</summary>
	// Token: 0x02000683 RID: 1667
	public enum CacheItemPriority
	{
		/// <summary>Cache items with this priority level are the most likely to be deleted from the cache as the server frees system memory.</summary>
		// Token: 0x04002587 RID: 9607
		Low = 1,
		/// <summary>Cache items with this priority level are more likely to be deleted from the cache as the server frees system memory than items assigned a <see cref="F:System.Web.Caching.CacheItemPriority.Normal" /> priority.</summary>
		// Token: 0x04002588 RID: 9608
		BelowNormal,
		/// <summary>Cache items with this priority level are likely to be deleted from the cache as the server frees system memory only after those items with <see cref="F:System.Web.Caching.CacheItemPriority.Low" /> or <see cref="F:System.Web.Caching.CacheItemPriority.BelowNormal" /> priority. This is the default.</summary>
		// Token: 0x04002589 RID: 9609
		Normal,
		/// <summary>The default value for a cached item's priority is <see cref="F:System.Web.Caching.CacheItemPriority.Normal" />.</summary>
		// Token: 0x0400258A RID: 9610
		Default = 3,
		/// <summary>Cache items with this priority level are less likely to be deleted as the server frees system memory than those assigned a <see cref="F:System.Web.Caching.CacheItemPriority.Normal" /> priority.</summary>
		// Token: 0x0400258B RID: 9611
		AboveNormal,
		/// <summary>Cache items with this priority level are the least likely to be deleted from the cache as the server frees system memory.</summary>
		// Token: 0x0400258C RID: 9612
		High,
		/// <summary>The cache items with this priority level will not be automatically deleted from the cache as the server frees system memory. However, items with this priority level are removed along with other items according to the item's absolute or sliding expiration time. </summary>
		// Token: 0x0400258D RID: 9613
		NotRemovable
	}
}
