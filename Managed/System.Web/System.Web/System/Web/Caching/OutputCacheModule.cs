using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Text;
using System.Web.Compilation;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Caching
{
	// Token: 0x02000693 RID: 1683
	internal sealed class OutputCacheModule : IHttpModule
	{
		// Token: 0x06004798 RID: 18328 RVA: 0x000C94B8 File Offset: 0x000C76B8
		private OutputCacheProvider FindCacheProvider(HttpApplication app)
		{
			HttpContext httpContext = HttpContext.Current;
			if (app == null)
			{
				app = ((httpContext != null) ? httpContext.ApplicationInstance : null);
				if (app == null)
				{
					throw new InvalidOperationException("Unable to find output cache provider.");
				}
			}
			string outputCacheProviderName = app.GetOutputCacheProviderName(httpContext);
			if (string.IsNullOrEmpty(outputCacheProviderName))
			{
				throw new ProviderException("Invalid OutputCacheProvider name. Name must not be null or an empty string.");
			}
			OutputCacheProvider provider = OutputCache.GetProvider(outputCacheProviderName);
			if (provider == null)
			{
				throw new ProviderException(string.Format("OutputCacheProvider named '{0}' cannot be found.", outputCacheProviderName));
			}
			return provider;
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x0000393A File Offset: 0x00001B3A
		public void Dispose()
		{
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x000C951F File Offset: 0x000C771F
		public void Init(HttpApplication context)
		{
			context.ResolveRequestCache += this.OnResolveRequestCache;
			context.UpdateRequestCache += this.OnUpdateRequestCache;
			this.response_removed = new CacheItemRemovedCallback(this.OnRawResponseRemoved);
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x000C9558 File Offset: 0x000C7758
		private void OnBuildManagerRemoveEntry(BuildManagerRemoveEntryEventArgs args)
		{
			string entryName = args.EntryName;
			HttpContext context = args.Context;
			object obj = OutputCacheModule.keysCacheLock;
			string text;
			lock (obj)
			{
				if (!this.keysCache.TryGetValue(entryName, out text))
				{
					return;
				}
				this.keysCache.Remove(entryName);
				if (context == null)
				{
					if (this.entriesToInvalidate == null)
					{
						this.entriesToInvalidate = new Dictionary<string, string>(StringComparer.Ordinal);
						this.entriesToInvalidate.Add(entryName, text);
						return;
					}
					if (!this.entriesToInvalidate.ContainsKey(entryName))
					{
						this.entriesToInvalidate.Add(entryName, text);
						return;
					}
				}
			}
			OutputCacheProvider outputCacheProvider = this.FindCacheProvider((context != null) ? context.ApplicationInstance : null);
			outputCacheProvider.Remove(entryName);
			if (!string.IsNullOrEmpty(text))
			{
				outputCacheProvider.Remove(text);
			}
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x000C9638 File Offset: 0x000C7838
		private void OnResolveRequestCache(object o, EventArgs args)
		{
			HttpApplication httpApplication = o as HttpApplication;
			HttpContext httpContext = ((httpApplication != null) ? httpApplication.Context : null);
			if (httpContext == null)
			{
				return;
			}
			OutputCacheProvider outputCacheProvider = this.FindCacheProvider(httpApplication);
			string filePath = httpContext.Request.FilePath;
			CachedVaryBy cachedVaryBy = outputCacheProvider.Get(filePath) as CachedVaryBy;
			if (cachedVaryBy == null)
			{
				return;
			}
			string text = cachedVaryBy.CreateKey(filePath, httpContext);
			CachedRawResponse cachedRawResponse = outputCacheProvider.Get(text) as CachedRawResponse;
			if (cachedRawResponse == null)
			{
				return;
			}
			object obj = OutputCacheModule.keysCacheLock;
			lock (obj)
			{
				string text2;
				if (this.entriesToInvalidate != null && this.entriesToInvalidate.TryGetValue(filePath, out text2) && string.Compare(text2, text, StringComparison.Ordinal) == 0)
				{
					outputCacheProvider.Remove(filePath);
					outputCacheProvider.Remove(text);
					this.entriesToInvalidate.Remove(filePath);
					return;
				}
			}
			ArrayList validationCallbacks = cachedRawResponse.Policy.ValidationCallbacks;
			if (validationCallbacks != null && validationCallbacks.Count > 0)
			{
				bool flag2 = true;
				bool flag3 = false;
				foreach (object obj2 in validationCallbacks)
				{
					Pair pair = (Pair)obj2;
					HttpCacheValidateHandler httpCacheValidateHandler = (HttpCacheValidateHandler)pair.First;
					object second = pair.Second;
					HttpValidationStatus httpValidationStatus = HttpValidationStatus.Valid;
					try
					{
						httpCacheValidateHandler(httpContext, second, ref httpValidationStatus);
					}
					catch
					{
						flag2 = false;
						break;
					}
					if (httpValidationStatus == HttpValidationStatus.Invalid)
					{
						flag2 = false;
						break;
					}
					if (httpValidationStatus == HttpValidationStatus.IgnoreThisRequest)
					{
						flag3 = true;
					}
				}
				if (!flag2)
				{
					this.OnRawResponseRemoved(text, cachedRawResponse, CacheItemRemovedReason.Removed);
					return;
				}
				if (flag3)
				{
					return;
				}
			}
			HttpResponse response = httpContext.Response;
			response.ClearContent();
			IList data = cachedRawResponse.GetData();
			if (data != null)
			{
				Encoding responseEncoding = WebEncoding.ResponseEncoding;
				foreach (object obj3 in data)
				{
					CachedRawResponse.DataItem dataItem = (CachedRawResponse.DataItem)obj3;
					if (dataItem.Length > 0L)
					{
						response.BinaryWrite(dataItem.Buffer, 0, (int)dataItem.Length);
					}
					else if (dataItem.Callback != null)
					{
						string text3 = dataItem.Callback(httpContext);
						if (text3 != null && text3.Length != 0)
						{
							byte[] bytes = responseEncoding.GetBytes(text3);
							response.BinaryWrite(bytes, 0, bytes.Length);
						}
					}
				}
			}
			response.ClearHeaders();
			response.SetCachedHeaders(cachedRawResponse.Headers);
			response.StatusCode = cachedRawResponse.StatusCode;
			response.StatusDescription = cachedRawResponse.StatusDescription;
			httpApplication.CompleteRequest();
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x000C98E8 File Offset: 0x000C7AE8
		private void OnUpdateRequestCache(object o, EventArgs args)
		{
			HttpApplication httpApplication = o as HttpApplication;
			HttpContext httpContext = ((httpApplication != null) ? httpApplication.Context : null);
			HttpResponse httpResponse = ((httpContext != null) ? httpContext.Response : null);
			if (httpResponse != null && httpResponse.IsCached && httpResponse.StatusCode == 200 && !httpContext.Trace.IsEnabled)
			{
				this.DoCacheInsert(httpContext, httpApplication, httpResponse);
			}
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x000C9944 File Offset: 0x000C7B44
		private void DoCacheInsert(HttpContext context, HttpApplication app, HttpResponse response)
		{
			string filePath = context.Request.FilePath;
			OutputCacheProvider outputCacheProvider = this.FindCacheProvider(app);
			CachedVaryBy cachedVaryBy = outputCacheProvider.Get(filePath) as CachedVaryBy;
			CachedRawResponse cachedRawResponse = null;
			bool flag = true;
			string text = null;
			string text2 = null;
			HttpCachePolicy cache = response.Cache;
			if (cachedVaryBy == null)
			{
				cachedVaryBy = new CachedVaryBy(cache, filePath);
				outputCacheProvider.Add(filePath, cachedVaryBy, Cache.NoAbsoluteExpiration);
				flag = false;
				text = filePath;
			}
			string text3 = cachedVaryBy.CreateKey(filePath, context);
			if (flag)
			{
				cachedRawResponse = outputCacheProvider.Get(text3) as CachedRawResponse;
			}
			if (cachedRawResponse == null)
			{
				CachedRawResponse cachedResponse = response.GetCachedResponse();
				if (cachedResponse != null)
				{
					string[] array = new string[] { filePath };
					cachedResponse.VaryBy = cachedVaryBy;
					cachedVaryBy.ItemList.Add(text3);
					TimeSpan timeSpan;
					DateTime dateTime;
					DateTime dateTime2;
					if (cache.Sliding)
					{
						timeSpan = TimeSpan.FromSeconds((double)cache.Duration);
						dateTime = Cache.NoAbsoluteExpiration;
						dateTime2 = DateTime.UtcNow + timeSpan;
					}
					else
					{
						timeSpan = Cache.NoSlidingExpiration;
						dateTime = cache.Expires;
						dateTime2 = dateTime.ToUniversalTime();
					}
					outputCacheProvider.Set(text3, cachedResponse, dateTime2);
					HttpRuntime.InternalCache.Insert(text3, cachedResponse, new CacheDependency(null, array), dateTime, timeSpan, CacheItemPriority.Normal, this.response_removed);
					text2 = text3;
				}
			}
			if (text != null)
			{
				object obj = OutputCacheModule.keysCacheLock;
				lock (obj)
				{
					if (this.keysCache == null)
					{
						BuildManager.RemoveEntry += this.OnBuildManagerRemoveEntry;
						this.keysCache = new Dictionary<string, string>(StringComparer.Ordinal);
						this.keysCache.Add(text, text2);
					}
					else if (!this.keysCache.ContainsKey(text))
					{
						this.keysCache.Add(text, text2);
					}
				}
			}
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x000C9AFC File Offset: 0x000C7CFC
		private void OnRawResponseRemoved(string key, object value, CacheItemRemovedReason reason)
		{
			CachedRawResponse cachedRawResponse = value as CachedRawResponse;
			CachedVaryBy cachedVaryBy = ((cachedRawResponse != null) ? cachedRawResponse.VaryBy : null);
			if (cachedVaryBy == null)
			{
				return;
			}
			List<string> itemList = cachedVaryBy.ItemList;
			OutputCacheProvider outputCacheProvider = this.FindCacheProvider(null);
			itemList.Remove(key);
			outputCacheProvider.Remove(key);
			if (itemList.Count != 0)
			{
				return;
			}
			outputCacheProvider.Remove(cachedVaryBy.Key);
		}

		// Token: 0x040025C0 RID: 9664
		private CacheItemRemovedCallback response_removed;

		// Token: 0x040025C1 RID: 9665
		private static object keysCacheLock = new object();

		// Token: 0x040025C2 RID: 9666
		private Dictionary<string, string> keysCache;

		// Token: 0x040025C3 RID: 9667
		private Dictionary<string, string> entriesToInvalidate;
	}
}
