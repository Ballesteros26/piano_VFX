using System;
using System.Globalization;

namespace System.Net.Cache
{
	/// <summary>Defines an application's caching requirements for resources obtained by using <see cref="T:System.Net.HttpWebRequest" /> objects.</summary>
	// Token: 0x020006C2 RID: 1730
	public class HttpRequestCachePolicy : RequestCachePolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class. </summary>
		// Token: 0x0600361C RID: 13852 RVA: 0x000C6388 File Offset: 0x000C4588
		public HttpRequestCachePolicy()
			: this(HttpRequestCacheLevel.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified cache policy.</summary>
		/// <param name="level">An <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value. </param>
		// Token: 0x0600361D RID: 13853 RVA: 0x000C6394 File Offset: 0x000C4594
		public HttpRequestCachePolicy(HttpRequestCacheLevel level)
			: base(HttpRequestCachePolicy.MapLevel(level))
		{
			this.m_Level = level;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified age control and time values. </summary>
		/// <param name="cacheAgeControl">One of the following <see cref="T:System.Net.Cache.HttpCacheAgeControl" /> enumeration values: <see cref="F:System.Net.Cache.HttpCacheAgeControl.MaxAge" />, <see cref="F:System.Net.Cache.HttpCacheAgeControl.MaxStale" />, or <see cref="F:System.Net.Cache.HttpCacheAgeControl.MinFresh" />.</param>
		/// <param name="ageOrFreshOrStale">A <see cref="T:System.TimeSpan" /> value that specifies an amount of time. See the Remarks section for more information. </param>
		/// <exception cref="T:System.ArgumentException">The value specified for the <paramref name="cacheAgeControl" /> parameter cannot be used with this constructor.</exception>
		// Token: 0x0600361E RID: 13854 RVA: 0x000C63E0 File Offset: 0x000C45E0
		public HttpRequestCachePolicy(HttpCacheAgeControl cacheAgeControl, TimeSpan ageOrFreshOrStale)
			: this(HttpRequestCacheLevel.Default)
		{
			switch (cacheAgeControl)
			{
			case HttpCacheAgeControl.MinFresh:
				this.m_MinFresh = ageOrFreshOrStale;
				return;
			case HttpCacheAgeControl.MaxAge:
				this.m_MaxAge = ageOrFreshOrStale;
				return;
			case HttpCacheAgeControl.MaxStale:
				this.m_MaxStale = ageOrFreshOrStale;
				return;
			}
			throw new ArgumentException(global::SR.GetString("The specified value is not valid in the '{0}' enumeration.", new object[] { "HttpCacheAgeControl" }), "cacheAgeControl");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified maximum age, age control value, and time value.</summary>
		/// <param name="cacheAgeControl">An <see cref="T:System.Net.Cache.HttpCacheAgeControl" /> value. </param>
		/// <param name="maxAge">A <see cref="T:System.TimeSpan" /> value that specifies the maximum age for resources.</param>
		/// <param name="freshOrStale">A <see cref="T:System.TimeSpan" /> value that specifies an amount of time. See the Remarks section for more information.  </param>
		/// <exception cref="T:System.ArgumentException">The value specified for the <paramref name="cacheAgeControl" /> parameter is not valid.</exception>
		// Token: 0x0600361F RID: 13855 RVA: 0x000C6448 File Offset: 0x000C4648
		public HttpRequestCachePolicy(HttpCacheAgeControl cacheAgeControl, TimeSpan maxAge, TimeSpan freshOrStale)
			: this(HttpRequestCacheLevel.Default)
		{
			switch (cacheAgeControl)
			{
			case HttpCacheAgeControl.MinFresh:
				this.m_MinFresh = freshOrStale;
				return;
			case HttpCacheAgeControl.MaxAge:
				this.m_MaxAge = maxAge;
				return;
			case HttpCacheAgeControl.MaxAgeAndMinFresh:
				this.m_MaxAge = maxAge;
				this.m_MinFresh = freshOrStale;
				return;
			case HttpCacheAgeControl.MaxStale:
				this.m_MaxStale = freshOrStale;
				return;
			case HttpCacheAgeControl.MaxAgeAndMaxStale:
				this.m_MaxAge = maxAge;
				this.m_MaxStale = freshOrStale;
				return;
			}
			throw new ArgumentException(global::SR.GetString("The specified value is not valid in the '{0}' enumeration.", new object[] { "HttpCacheAgeControl" }), "cacheAgeControl");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified cache synchronization date.</summary>
		/// <param name="cacheSyncDate">A <see cref="T:System.DateTime" /> object that specifies the time when resources stored in the cache must be revalidated.</param>
		// Token: 0x06003620 RID: 13856 RVA: 0x000C64D6 File Offset: 0x000C46D6
		public HttpRequestCachePolicy(DateTime cacheSyncDate)
			: this(HttpRequestCacheLevel.Default)
		{
			this.m_LastSyncDateUtc = cacheSyncDate.ToUniversalTime();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified maximum age, age control value, time value, and cache synchronization date.</summary>
		/// <param name="cacheAgeControl">An <see cref="T:System.Net.Cache.HttpCacheAgeControl" /> value. </param>
		/// <param name="maxAge">A <see cref="T:System.TimeSpan" /> value that specifies the maximum age for resources.</param>
		/// <param name="freshOrStale">A <see cref="T:System.TimeSpan" /> value that specifies an amount of time. See the Remarks section for more information.  </param>
		/// <param name="cacheSyncDate">A <see cref="T:System.DateTime" /> object that specifies the time when resources stored in the cache must be revalidated.</param>
		// Token: 0x06003621 RID: 13857 RVA: 0x000C64EC File Offset: 0x000C46EC
		public HttpRequestCachePolicy(HttpCacheAgeControl cacheAgeControl, TimeSpan maxAge, TimeSpan freshOrStale, DateTime cacheSyncDate)
			: this(cacheAgeControl, maxAge, freshOrStale)
		{
			this.m_LastSyncDateUtc = cacheSyncDate.ToUniversalTime();
		}

		/// <summary>Gets the <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value that was specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value that specifies the cache behavior for resources that were obtained using <see cref="T:System.Net.HttpWebRequest" /> objects.</returns>
		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06003622 RID: 13858 RVA: 0x000C6504 File Offset: 0x000C4704
		public new HttpRequestCacheLevel Level
		{
			get
			{
				return this.m_Level;
			}
		}

		/// <summary>Gets the cache synchronization date for this instance.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value set to the date specified when this instance was created. If no date was specified, this property's value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06003623 RID: 13859 RVA: 0x000C650C File Offset: 0x000C470C
		public DateTime CacheSyncDate
		{
			get
			{
				if (this.m_LastSyncDateUtc == DateTime.MinValue || this.m_LastSyncDateUtc == DateTime.MaxValue)
				{
					return this.m_LastSyncDateUtc;
				}
				return this.m_LastSyncDateUtc.ToLocalTime();
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06003624 RID: 13860 RVA: 0x000C6544 File Offset: 0x000C4744
		internal DateTime InternalCacheSyncDateUtc
		{
			get
			{
				return this.m_LastSyncDateUtc;
			}
		}

		/// <summary>Gets the maximum age permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that is set to the maximum age value specified when this instance was created. If no date was specified, this property's value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06003625 RID: 13861 RVA: 0x000C654C File Offset: 0x000C474C
		public TimeSpan MaxAge
		{
			get
			{
				return this.m_MaxAge;
			}
		}

		/// <summary>Gets the minimum freshness that is permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the minimum freshness specified when this instance was created. If no date was specified, this property's value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x000C6554 File Offset: 0x000C4754
		public TimeSpan MinFresh
		{
			get
			{
				return this.m_MinFresh;
			}
		}

		/// <summary>Gets the maximum staleness value that is permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that is set to the maximum staleness value specified when this instance was created. If no date was specified, this property's value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06003627 RID: 13863 RVA: 0x000C655C File Offset: 0x000C475C
		public TimeSpan MaxStale
		{
			get
			{
				return this.m_MaxStale;
			}
		}

		/// <summary>Returns a string representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the property values for this instance.</returns>
		// Token: 0x06003628 RID: 13864 RVA: 0x000C6564 File Offset: 0x000C4764
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Level:",
				this.m_Level.ToString(),
				(this.m_MaxAge == TimeSpan.MaxValue) ? string.Empty : (" MaxAge:" + this.m_MaxAge.ToString()),
				(this.m_MinFresh == TimeSpan.MinValue) ? string.Empty : (" MinFresh:" + this.m_MinFresh.ToString()),
				(this.m_MaxStale == TimeSpan.MinValue) ? string.Empty : (" MaxStale:" + this.m_MaxStale.ToString()),
				(this.CacheSyncDate == DateTime.MinValue) ? string.Empty : (" CacheSyncDate:" + this.CacheSyncDate.ToString(CultureInfo.CurrentCulture))
			});
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x000C6676 File Offset: 0x000C4876
		private static RequestCacheLevel MapLevel(HttpRequestCacheLevel level)
		{
			if (level <= HttpRequestCacheLevel.NoCacheNoStore)
			{
				return (RequestCacheLevel)level;
			}
			if (level == HttpRequestCacheLevel.CacheOrNextCacheOnly)
			{
				return RequestCacheLevel.CacheOnly;
			}
			if (level == HttpRequestCacheLevel.Refresh)
			{
				return RequestCacheLevel.Reload;
			}
			throw new ArgumentOutOfRangeException("level");
		}

		// Token: 0x04002ADC RID: 10972
		internal static readonly HttpRequestCachePolicy BypassCache = new HttpRequestCachePolicy(HttpRequestCacheLevel.BypassCache);

		// Token: 0x04002ADD RID: 10973
		private HttpRequestCacheLevel m_Level;

		// Token: 0x04002ADE RID: 10974
		private DateTime m_LastSyncDateUtc = DateTime.MinValue;

		// Token: 0x04002ADF RID: 10975
		private TimeSpan m_MaxAge = TimeSpan.MaxValue;

		// Token: 0x04002AE0 RID: 10976
		private TimeSpan m_MinFresh = TimeSpan.MinValue;

		// Token: 0x04002AE1 RID: 10977
		private TimeSpan m_MaxStale = TimeSpan.MinValue;
	}
}
