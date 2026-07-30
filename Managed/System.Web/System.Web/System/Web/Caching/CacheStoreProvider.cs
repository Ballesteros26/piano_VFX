using System;
using System.Collections;
using System.Configuration.Provider;
using Unity;

namespace System.Web.Caching
{
	// Token: 0x02000784 RID: 1924
	public abstract class CacheStoreProvider : ProviderBase, IDisposable
	{
		// Token: 0x06004E2A RID: 20010 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected CacheStoreProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x170017CB RID: 6091
		// (get) Token: 0x06004E2B RID: 20011
		public abstract long ItemCount { get; }

		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x06004E2C RID: 20012
		public abstract long SizeInBytes { get; }

		// Token: 0x06004E2D RID: 20013
		public abstract object Add(string key, object item, CacheInsertOptions options);

		// Token: 0x06004E2E RID: 20014
		public abstract bool AddDependent(string key, CacheDependency dependency, out DateTime utcLastUpdated);

		// Token: 0x06004E2F RID: 20015
		public abstract void Dispose();

		// Token: 0x06004E30 RID: 20016
		public abstract object Get(string key);

		// Token: 0x06004E31 RID: 20017
		public abstract IDictionaryEnumerator GetEnumerator();

		// Token: 0x06004E32 RID: 20018
		public abstract void Insert(string key, object item, CacheInsertOptions options);

		// Token: 0x06004E33 RID: 20019
		public abstract object Remove(string key);

		// Token: 0x06004E34 RID: 20020
		public abstract object Remove(string key, CacheItemRemovedReason reason);

		// Token: 0x06004E35 RID: 20021
		public abstract void RemoveDependent(string key, CacheDependency dependency);

		// Token: 0x06004E36 RID: 20022
		public abstract long Trim(int percent);
	}
}
