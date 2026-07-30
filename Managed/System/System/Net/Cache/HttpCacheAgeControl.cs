using System;

namespace System.Net.Cache
{
	/// <summary>Specifies the meaning of time values that control caching behavior for resources obtained using <see cref="T:System.Net.HttpWebRequest" /> objects.</summary>
	// Token: 0x020006C1 RID: 1729
	public enum HttpCacheAgeControl
	{
		/// <summary>For internal use only. The Framework will throw an <see cref="T:System.ArgumentException" /> if you try to use this member.</summary>
		// Token: 0x04002AD6 RID: 10966
		None,
		/// <summary>Content can be taken from the cache if the time remaining before expiration is greater than or equal to the time specified with this value.</summary>
		// Token: 0x04002AD7 RID: 10967
		MinFresh,
		/// <summary>Content can be taken from the cache until it is older than the age specified with this value.</summary>
		// Token: 0x04002AD8 RID: 10968
		MaxAge,
		/// <summary>Content can be taken from the cache after it has expired, until the time specified with this value elapses.</summary>
		// Token: 0x04002AD9 RID: 10969
		MaxStale = 4,
		/// <summary>
		///   <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MaxAge" /> and <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MinFresh" />.</summary>
		// Token: 0x04002ADA RID: 10970
		MaxAgeAndMinFresh = 3,
		/// <summary>
		///   <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MaxAge" /> and <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MaxStale" />.</summary>
		// Token: 0x04002ADB RID: 10971
		MaxAgeAndMaxStale = 6
	}
}
