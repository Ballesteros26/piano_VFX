using System;
using System.Text;
using System.Web.Caching;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000385 RID: 901
	internal class DataSourceCacheManager
	{
		// Token: 0x060022C8 RID: 8904 RVA: 0x00059BD4 File Offset: 0x00057DD4
		internal DataSourceCacheManager(int cacheDuration, string cacheKeyDependency, DataSourceCacheExpiry cacheExpirationPolicy, Control owner, HttpContext context)
		{
			this.cacheDuration = cacheDuration;
			this.cacheKeyDependency = cacheKeyDependency;
			this.cacheExpirationPolicy = cacheExpirationPolicy;
			this.controlID = owner.UniqueID;
			this.owner = owner;
			this.context = context;
			if (DataSourceCacheManager.DataCache[this.controlID] == null)
			{
				DataSourceCacheManager.DataCache[this.controlID] = new object();
			}
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x00059C40 File Offset: 0x00057E40
		internal void Expire()
		{
			DataSourceCacheManager.DataCache[this.controlID] = new object();
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x00059C57 File Offset: 0x00057E57
		internal object GetCachedObject(string methodName, ParameterCollection parameters)
		{
			return DataSourceCacheManager.DataCache[this.GetKeyFromParameters(methodName, parameters)];
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x00059C6C File Offset: 0x00057E6C
		internal void SetCachedObject(string methodName, ParameterCollection parameters, object o)
		{
			if (o == null)
			{
				return;
			}
			string keyFromParameters = this.GetKeyFromParameters(methodName, parameters);
			if (DataSourceCacheManager.DataCache[keyFromParameters] != null)
			{
				DataSourceCacheManager.DataCache.Remove(keyFromParameters);
			}
			DateTime dateTime = Cache.NoAbsoluteExpiration;
			TimeSpan noSlidingExpiration = Cache.NoSlidingExpiration;
			if (this.cacheDuration > 0)
			{
				if (this.cacheExpirationPolicy == DataSourceCacheExpiry.Absolute)
				{
					dateTime = DateTime.Now.AddSeconds((double)this.cacheDuration);
				}
				else
				{
					noSlidingExpiration = new TimeSpan(0, 0, this.cacheDuration);
				}
			}
			string[] array;
			if (this.cacheKeyDependency.Length > 0)
			{
				array = new string[] { this.cacheKeyDependency };
			}
			else
			{
				array = new string[0];
			}
			DataSourceCacheManager.DataCache.Add(keyFromParameters, o, new CacheDependency(new string[0], array), dateTime, noSlidingExpiration, CacheItemPriority.Normal, null);
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x00059D27 File Offset: 0x00057F27
		private static Cache DataCache
		{
			get
			{
				if (HttpContext.Current != null)
				{
					return HttpContext.Current.InternalCache;
				}
				throw new InvalidOperationException("HttpContext.Current is null.");
			}
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x00059D48 File Offset: 0x00057F48
		private string GetKeyFromParameters(string methodName, ParameterCollection parameters)
		{
			StringBuilder stringBuilder = new StringBuilder(methodName);
			if (this.owner != null)
			{
				stringBuilder.Append(this.owner.ID);
			}
			for (int i = 0; i < parameters.Count; i++)
			{
				stringBuilder.Append(parameters[i].Name);
				stringBuilder.Append(parameters[i].GetValue(this.context, this.owner));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001937 RID: 6455
		private readonly int cacheDuration;

		// Token: 0x04001938 RID: 6456
		private readonly string cacheKeyDependency;

		// Token: 0x04001939 RID: 6457
		private readonly string controlID;

		// Token: 0x0400193A RID: 6458
		private readonly DataSourceCacheExpiry cacheExpirationPolicy;

		// Token: 0x0400193B RID: 6459
		private readonly Control owner;

		// Token: 0x0400193C RID: 6460
		private readonly HttpContext context;
	}
}
