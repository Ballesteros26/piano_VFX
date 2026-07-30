using System;

namespace System.Net.Cache
{
	// Token: 0x020006BA RID: 1722
	internal sealed class RequestCacheManager
	{
		// Token: 0x0600360B RID: 13835 RVA: 0x000020EB File Offset: 0x000002EB
		private RequestCacheManager()
		{
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x000C6170 File Offset: 0x000C4370
		internal static RequestCacheBinding GetBinding(string internedScheme)
		{
			if (internedScheme == null)
			{
				throw new ArgumentNullException("uriScheme");
			}
			if (RequestCacheManager.s_CacheConfigSettings == null)
			{
				RequestCacheManager.LoadConfigSettings();
			}
			if (RequestCacheManager.s_CacheConfigSettings.DisableAllCaching)
			{
				return RequestCacheManager.s_BypassCacheBinding;
			}
			if (internedScheme.Length == 0)
			{
				return RequestCacheManager.s_DefaultGlobalBinding;
			}
			if (internedScheme == Uri.UriSchemeHttp || internedScheme == Uri.UriSchemeHttps)
			{
				return RequestCacheManager.s_DefaultHttpBinding;
			}
			if (internedScheme == Uri.UriSchemeFtp)
			{
				return RequestCacheManager.s_DefaultFtpBinding;
			}
			return RequestCacheManager.s_BypassCacheBinding;
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x000C61EA File Offset: 0x000C43EA
		internal static bool IsCachingEnabled
		{
			get
			{
				if (RequestCacheManager.s_CacheConfigSettings == null)
				{
					RequestCacheManager.LoadConfigSettings();
				}
				return !RequestCacheManager.s_CacheConfigSettings.DisableAllCaching;
			}
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000C620C File Offset: 0x000C440C
		internal static void SetBinding(string uriScheme, RequestCacheBinding binding)
		{
			if (uriScheme == null)
			{
				throw new ArgumentNullException("uriScheme");
			}
			if (RequestCacheManager.s_CacheConfigSettings == null)
			{
				RequestCacheManager.LoadConfigSettings();
			}
			if (RequestCacheManager.s_CacheConfigSettings.DisableAllCaching)
			{
				return;
			}
			if (uriScheme.Length == 0)
			{
				RequestCacheManager.s_DefaultGlobalBinding = binding;
				return;
			}
			if (uriScheme == Uri.UriSchemeHttp || uriScheme == Uri.UriSchemeHttps)
			{
				RequestCacheManager.s_DefaultHttpBinding = binding;
				return;
			}
			if (uriScheme == Uri.UriSchemeFtp)
			{
				RequestCacheManager.s_DefaultFtpBinding = binding;
			}
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x000C6290 File Offset: 0x000C4490
		private static void LoadConfigSettings()
		{
			RequestCacheBinding requestCacheBinding = RequestCacheManager.s_BypassCacheBinding;
			lock (requestCacheBinding)
			{
				if (RequestCacheManager.s_CacheConfigSettings == null)
				{
					RequestCacheManager.s_CacheConfigSettings = new RequestCachingSectionInternal();
				}
			}
		}

		// Token: 0x04002AB9 RID: 10937
		private static volatile RequestCachingSectionInternal s_CacheConfigSettings;

		// Token: 0x04002ABA RID: 10938
		private static readonly RequestCacheBinding s_BypassCacheBinding = new RequestCacheBinding(null, null, new RequestCachePolicy(RequestCacheLevel.BypassCache));

		// Token: 0x04002ABB RID: 10939
		private static volatile RequestCacheBinding s_DefaultGlobalBinding;

		// Token: 0x04002ABC RID: 10940
		private static volatile RequestCacheBinding s_DefaultHttpBinding;

		// Token: 0x04002ABD RID: 10941
		private static volatile RequestCacheBinding s_DefaultFtpBinding;
	}
}
