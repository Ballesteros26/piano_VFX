using System;

namespace System.Web
{
	/// <summary>Provides enumerated values that are used to set the Cache-Control HTTP header.</summary>
	// Token: 0x02000087 RID: 135
	public enum HttpCacheability
	{
		/// <summary>Sets the Cache-Control: no-cache header. Without a field name, the directive applies to the entire request and a shared (proxy server) cache must force a successful revalidation with the origin Web server before satisfying the request. With a field name, the directive applies only to the named field; the rest of the response may be supplied from a shared cache. </summary>
		// Token: 0x04000F27 RID: 3879
		NoCache = 1,
		/// <summary>Default value. Sets Cache-Control: private to specify that the response is cacheable only on the client and not by shared (proxy server) caches. </summary>
		// Token: 0x04000F28 RID: 3880
		Private,
		/// <summary>Specifies that the response is cached only at the origin server. Similar to the <see cref="F:System.Web.HttpCacheability.NoCache" /> option. Clients receive a Cache-Control: no-cache directive but the document is cached on the origin server. Equivalent to <see cref="F:System.Web.HttpCacheability.ServerAndNoCache" />.</summary>
		// Token: 0x04000F29 RID: 3881
		Server,
		/// <summary>Sets Cache-Control: public to specify that the response is cacheable by clients and shared (proxy) caches. </summary>
		// Token: 0x04000F2A RID: 3882
		Public,
		/// <summary>Indicates that the response is cached at the server and at the client but nowhere else. Proxy servers are not allowed to cache the response. </summary>
		// Token: 0x04000F2B RID: 3883
		ServerAndPrivate,
		/// <summary>Applies the settings of both <see cref="F:System.Web.HttpCacheability.Server" /> and <see cref="F:System.Web.HttpCacheability.NoCache" /> to indicate that the content is cached at the server but all others are explicitly denied the ability to cache the response. </summary>
		// Token: 0x04000F2C RID: 3884
		ServerAndNoCache = 3
	}
}
