using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Xml
{
	/// <summary>Resolves external XML resources named by a Uniform Resource Identifier (URI).</summary>
	// Token: 0x020002A9 RID: 681
	public class XmlUrlResolver : XmlResolver
	{
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x0009019C File Offset: 0x0008E39C
		private static XmlDownloadManager DownloadManager
		{
			get
			{
				if (XmlUrlResolver.s_DownloadManager == null)
				{
					object obj = new XmlDownloadManager();
					Interlocked.CompareExchange<object>(ref XmlUrlResolver.s_DownloadManager, obj, null);
				}
				return (XmlDownloadManager)XmlUrlResolver.s_DownloadManager;
			}
		}

		/// <summary>Sets credentials used to authenticate Web requests.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> object. If this property is not set, the value defaults to null; that is, the XmlUrlResolver has no user credentials.</returns>
		// Token: 0x170004B4 RID: 1204
		// (set) Token: 0x0600191B RID: 6427 RVA: 0x000901CD File Offset: 0x0008E3CD
		public override ICredentials Credentials
		{
			set
			{
				this._credentials = value;
			}
		}

		/// <summary>Gets or sets the network proxy for the underlying <see cref="T:System.Net.WebRequest" /> object.</summary>
		/// <returns>The <see cref="T:System.Net.IwebProxy" /> to use to access the Internet resource.</returns>
		// Token: 0x170004B5 RID: 1205
		// (set) Token: 0x0600191C RID: 6428 RVA: 0x000901D6 File Offset: 0x0008E3D6
		public IWebProxy Proxy
		{
			set
			{
				this._proxy = value;
			}
		}

		/// <summary>Gets or sets the cache policy for the underlying <see cref="T:System.Net.WebRequest" /> object.</summary>
		/// <returns>The <see cref="T:System.Net.Cache.RequestCachePolicy" /> object.</returns>
		// Token: 0x170004B6 RID: 1206
		// (set) Token: 0x0600191D RID: 6429 RVA: 0x000901DF File Offset: 0x0008E3DF
		public RequestCachePolicy CachePolicy
		{
			set
			{
				this._cachePolicy = value;
			}
		}

		/// <summary>Maps a URI to an object containing the actual resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object or null if a type other than stream is specified.</returns>
		/// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)" />.</param>
		/// <param name="role">The current implementation does not use this parameter when resolving URIs. This is provided for future extensibility purposes. For example, this can be mapped to the xlink: role and used as an implementation specific argument in other scenarios.</param>
		/// <param name="ofObjectToReturn">The type of object to return. The current implementation only returns <see cref="T:System.IO.Stream" /> objects.</param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="ofObjectToReturn" /> is neither null nor a Stream type.</exception>
		/// <exception cref="T:System.UriFormatException">The specified URI is not an absolute URI.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="absoluteUri" /> is null.</exception>
		/// <exception cref="T:System.Exception">There is a runtime error (for example, an interrupted server connection).</exception>
		// Token: 0x0600191E RID: 6430 RVA: 0x000901E8 File Offset: 0x0008E3E8
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			if (ofObjectToReturn == null || ofObjectToReturn == typeof(Stream) || ofObjectToReturn == typeof(object))
			{
				return XmlUrlResolver.DownloadManager.GetStream(absoluteUri, this._credentials, this._proxy, this._cachePolicy);
			}
			throw new XmlException("Object type is not supported.", string.Empty);
		}

		/// <summary>Resolves the absolute URI from the base and relative URIs.</summary>
		/// <returns>A <see cref="T:System.Uri" /> representing the absolute URI, or null if the relative URI cannot be resolved.</returns>
		/// <param name="baseUri">The base URI used to resolve the relative URI.</param>
		/// <param name="relativeUri">The URI to resolve. The URI can be absolute or relative. If absolute, this value effectively replaces the <paramref name="baseUri" /> value. If relative, it combines with the <paramref name="baseUri" /> to make an absolute URI.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="baseUri" /> is null or <paramref name="relativeUri" /> is null.</exception>
		// Token: 0x0600191F RID: 6431 RVA: 0x0009024F File Offset: 0x0008E44F
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		public override Uri ResolveUri(Uri baseUri, string relativeUri)
		{
			return base.ResolveUri(baseUri, relativeUri);
		}

		/// <summary>Asynchronously maps a URI to an object containing the actual resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object or null if a type other than stream is specified.</returns>
		/// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)" />.</param>
		/// <param name="role">The current implementation does not use this parameter when resolving URIs. This is provided for future extensibility purposes. For example, this can be mapped to the xlink: role and used as an implementation specific argument in other scenarios.</param>
		/// <param name="ofObjectToReturn">The type of object to return. The current implementation only returns <see cref="T:System.IO.Stream" /> objects.</param>
		// Token: 0x06001920 RID: 6432 RVA: 0x0009025C File Offset: 0x0008E45C
		public override async Task<object> GetEntityAsync(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			if (ofObjectToReturn == null || ofObjectToReturn == typeof(Stream) || ofObjectToReturn == typeof(object))
			{
				return await XmlUrlResolver.DownloadManager.GetStreamAsync(absoluteUri, this._credentials, this._proxy, this._cachePolicy).ConfigureAwait(false);
			}
			throw new XmlException("Object type is not supported.", string.Empty);
		}

		// Token: 0x0400106D RID: 4205
		private static object s_DownloadManager;

		// Token: 0x0400106E RID: 4206
		private ICredentials _credentials;

		// Token: 0x0400106F RID: 4207
		private IWebProxy _proxy;

		// Token: 0x04001070 RID: 4208
		private RequestCachePolicy _cachePolicy;
	}
}
