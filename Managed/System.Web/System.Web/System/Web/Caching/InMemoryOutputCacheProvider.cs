using System;

namespace System.Web.Caching
{
	// Token: 0x02000690 RID: 1680
	internal sealed class InMemoryOutputCacheProvider : OutputCacheProvider
	{
		// Token: 0x06004783 RID: 18307 RVA: 0x000C90C8 File Offset: 0x000C72C8
		public override object Add(string key, object entry, DateTime utcExpiry)
		{
			return HttpRuntime.InternalCache.Add("@InMemoryOCP_" + key, entry, null, utcExpiry.ToLocalTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
		}

		// Token: 0x06004784 RID: 18308 RVA: 0x000C90EF File Offset: 0x000C72EF
		public override object Get(string key)
		{
			return HttpRuntime.InternalCache.Get("@InMemoryOCP_" + key);
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x000C9106 File Offset: 0x000C7306
		public override void Remove(string key)
		{
			HttpRuntime.InternalCache.Remove("@InMemoryOCP_" + key);
		}

		// Token: 0x06004786 RID: 18310 RVA: 0x000C9120 File Offset: 0x000C7320
		public override void Set(string key, object entry, DateTime utcExpiry)
		{
			Cache internalCache = HttpRuntime.InternalCache;
			string text = "@InMemoryOCP_" + key;
			if (internalCache.Get(text) != null)
			{
				internalCache.Remove(text);
			}
			internalCache.Add(text, entry, null, utcExpiry.ToLocalTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
		}

		// Token: 0x040025B6 RID: 9654
		private const string CACHE_PREFIX = "@InMemoryOCP_";
	}
}
