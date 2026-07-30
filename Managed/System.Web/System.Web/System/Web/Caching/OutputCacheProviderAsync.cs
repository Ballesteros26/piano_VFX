using System;
using System.Threading.Tasks;
using Unity;

namespace System.Web.Caching
{
	// Token: 0x02000785 RID: 1925
	public abstract class OutputCacheProviderAsync : OutputCacheProvider
	{
		// Token: 0x06004E37 RID: 20023 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected OutputCacheProviderAsync()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004E38 RID: 20024
		public abstract Task<object> AddAsync(string key, object entry, DateTime utcExpiry);

		// Token: 0x06004E39 RID: 20025
		public abstract Task<object> GetAsync(string key);

		// Token: 0x06004E3A RID: 20026
		public abstract Task RemoveAsync(string key);

		// Token: 0x06004E3B RID: 20027
		public abstract Task SetAsync(string key, object entry, DateTime utcExpiry);
	}
}
