using System;

namespace System.Net.Cache
{
	/// <summary>Defines an application's caching requirements for resources obtained by using <see cref="T:System.Net.WebRequest" /> objects.</summary>
	// Token: 0x020006BF RID: 1727
	public class RequestCachePolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.RequestCachePolicy" /> class. </summary>
		// Token: 0x06003618 RID: 13848 RVA: 0x000C6338 File Offset: 0x000C4538
		public RequestCachePolicy()
			: this(RequestCacheLevel.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.RequestCachePolicy" /> class. using the specified cache policy.</summary>
		/// <param name="level">A <see cref="T:System.Net.Cache.RequestCacheLevel" /> that specifies the cache behavior for resources obtained using <see cref="T:System.Net.WebRequest" /> objects. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">level is not a valid <see cref="T:System.Net.Cache.RequestCacheLevel" />.value.</exception>
		// Token: 0x06003619 RID: 13849 RVA: 0x000C6341 File Offset: 0x000C4541
		public RequestCachePolicy(RequestCacheLevel level)
		{
			if (level < RequestCacheLevel.Default || level > RequestCacheLevel.NoCacheNoStore)
			{
				throw new ArgumentOutOfRangeException("level");
			}
			this.m_Level = level;
		}

		/// <summary>Gets the <see cref="T:System.Net.Cache.RequestCacheLevel" /> value specified when this instance was constructed.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCacheLevel" /> value that specifies the cache behavior for resources obtained using <see cref="T:System.Net.WebRequest" /> objects.</returns>
		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x0600361A RID: 13850 RVA: 0x000C6363 File Offset: 0x000C4563
		public RequestCacheLevel Level
		{
			get
			{
				return this.m_Level;
			}
		}

		/// <summary>Returns a string representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the <see cref="P:System.Net.Cache.RequestCachePolicy.Level" /> for this instance.</returns>
		// Token: 0x0600361B RID: 13851 RVA: 0x000C636B File Offset: 0x000C456B
		public override string ToString()
		{
			return "Level:" + this.m_Level.ToString();
		}

		// Token: 0x04002ACA RID: 10954
		private RequestCacheLevel m_Level;
	}
}
