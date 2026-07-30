using System;
using System.Collections.Specialized;
using System.IO;

namespace System.Net.Cache
{
	// Token: 0x020006B8 RID: 1720
	internal abstract class RequestCache
	{
		// Token: 0x060035E4 RID: 13796 RVA: 0x000C5D43 File Offset: 0x000C3F43
		protected RequestCache(bool isPrivateCache, bool canWrite)
		{
			this._IsPrivateCache = isPrivateCache;
			this._CanWrite = canWrite;
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x060035E5 RID: 13797 RVA: 0x000C5D59 File Offset: 0x000C3F59
		internal bool IsPrivateCache
		{
			get
			{
				return this._IsPrivateCache;
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x060035E6 RID: 13798 RVA: 0x000C5D61 File Offset: 0x000C3F61
		internal bool CanWrite
		{
			get
			{
				return this._CanWrite;
			}
		}

		// Token: 0x060035E7 RID: 13799
		internal abstract Stream Retrieve(string key, out RequestCacheEntry cacheEntry);

		// Token: 0x060035E8 RID: 13800
		internal abstract Stream Store(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x060035E9 RID: 13801
		internal abstract void Remove(string key);

		// Token: 0x060035EA RID: 13802
		internal abstract void Update(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x060035EB RID: 13803
		internal abstract bool TryRetrieve(string key, out RequestCacheEntry cacheEntry, out Stream readStream);

		// Token: 0x060035EC RID: 13804
		internal abstract bool TryStore(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata, out Stream writeStream);

		// Token: 0x060035ED RID: 13805
		internal abstract bool TryRemove(string key);

		// Token: 0x060035EE RID: 13806
		internal abstract bool TryUpdate(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x060035EF RID: 13807
		internal abstract void UnlockEntry(Stream retrieveStream);

		// Token: 0x04002AAA RID: 10922
		internal static readonly char[] LineSplits = new char[] { '\r', '\n' };

		// Token: 0x04002AAB RID: 10923
		private bool _IsPrivateCache;

		// Token: 0x04002AAC RID: 10924
		private bool _CanWrite;
	}
}
