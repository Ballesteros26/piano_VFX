using System;
using System.Runtime.CompilerServices;

namespace System.Web
{
	/// <summary>Encapsulates the HTTP intrinsic object that contains methods for setting cache-specific HTTP headers and for controlling the ASP.NET page output cache.</summary>
	// Token: 0x02000037 RID: 55
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpCachePolicyWrapper : HttpCachePolicyBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCachePolicyWrapper" /> class. </summary>
		/// <param name="httpCachePolicy">The object that this wrapper class provides access to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="httpCachePolicy" /> is null.</exception>
		// Token: 0x06000265 RID: 613 RVA: 0x00006E29 File Offset: 0x00005029
		public HttpCachePolicyWrapper(HttpCachePolicy httpCachePolicy)
		{
			if (httpCachePolicy == null)
			{
				throw new ArgumentNullException("httpCachePolicy");
			}
			this._httpCachePolicy = httpCachePolicy;
		}

		/// <summary>Gets the list of Content-Encoding headers that will be used to vary the output cache.</summary>
		/// <returns>An object that specifies which Content-Encoding headers are used to select the cached response.</returns>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00006E46 File Offset: 0x00005046
		public override HttpCacheVaryByContentEncodings VaryByContentEncodings
		{
			get
			{
				return this._httpCachePolicy.VaryByContentEncodings;
			}
		}

		/// <summary>Gets the list of all HTTP headers that will be used to vary cache output.</summary>
		/// <returns>An object that specifies which HTTP headers are used to select the cached response.</returns>
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00006E53 File Offset: 0x00005053
		public override HttpCacheVaryByHeaders VaryByHeaders
		{
			get
			{
				return this._httpCachePolicy.VaryByHeaders;
			}
		}

		/// <summary>Gets the list of parameters received by an HTTP GET or HTTP POST that affect caching.</summary>
		/// <returns>An object that specifies which cache-control parameters are used to select the cached response.</returns>
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00006E60 File Offset: 0x00005060
		public override HttpCacheVaryByParams VaryByParams
		{
			get
			{
				return this._httpCachePolicy.VaryByParams;
			}
		}

		/// <summary>Registers a validation callback for the current response.</summary>
		/// <param name="handler">The object that will handle the request.</param>
		/// <param name="data">The user-supplied data that is passed to the <see cref="M:System.Web.HttpCachePolicyWrapper.AddValidationCallback(System.Web.HttpCacheValidateHandler,System.Object)" /> delegate.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="handler" /> is null. </exception>
		// Token: 0x06000269 RID: 617 RVA: 0x00006E6D File Offset: 0x0000506D
		public override void AddValidationCallback(HttpCacheValidateHandler handler, object data)
		{
			this._httpCachePolicy.AddValidationCallback(handler, data);
		}

		/// <summary>Appends the specified text to the Cache-Control HTTP header.</summary>
		/// <param name="extension">The text to append to the Cache-Control header.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="extension" /> is null. </exception>
		// Token: 0x0600026A RID: 618 RVA: 0x00006E7C File Offset: 0x0000507C
		public override void AppendCacheExtension(string extension)
		{
			this._httpCachePolicy.AppendCacheExtension(extension);
		}

		/// <summary>Makes the response available in the browser history cache, regardless of the <see cref="T:System.Web.HttpCacheability" /> setting made on the server.</summary>
		/// <param name="allow">true to direct the client browser to store responses in the browser history cache; otherwise false. The default is false.</param>
		// Token: 0x0600026B RID: 619 RVA: 0x00006E8A File Offset: 0x0000508A
		public override void SetAllowResponseInBrowserHistory(bool allow)
		{
			this._httpCachePolicy.SetAllowResponseInBrowserHistory(allow);
		}

		/// <summary>Sets the Cache-Control header to the specified <see cref="T:System.Web.HttpCacheability" /> value.</summary>
		/// <param name="cacheability">The <see cref="T:System.Web.HttpCacheability" /> enumeration value to set the header to.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="cacheability" /> is not one of the enumeration values. </exception>
		// Token: 0x0600026C RID: 620 RVA: 0x00006E98 File Offset: 0x00005098
		public override void SetCacheability(HttpCacheability cacheability)
		{
			this._httpCachePolicy.SetCacheability(cacheability);
		}

		/// <summary>Sets the Cache-Control header to the specified <see cref="T:System.Web.HttpCacheability" /> value and appends an extension to the directive.</summary>
		/// <param name="cacheability">The <see cref="T:System.Web.HttpCacheability" /> enumeration value to set the header to.</param>
		/// <param name="field">The cache-control extension to add to the header.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="field" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="cacheability" /> is not <see cref="F:System.Web.HttpCacheability.Private" /> or <see cref="F:System.Web.HttpCacheability.NoCache" />. </exception>
		// Token: 0x0600026D RID: 621 RVA: 0x00006EA6 File Offset: 0x000050A6
		public override void SetCacheability(HttpCacheability cacheability, string field)
		{
			this._httpCachePolicy.SetCacheability(cacheability, field);
		}

		/// <summary>Sets the ETag HTTP header to the specified string.</summary>
		/// <param name="etag">The text to use for the ETag header.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="etag" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The ETag header has already been set. - or -The <see cref="M:System.Web.HttpCachePolicy.SetETagFromFileDependencies" /> method has already been called.</exception>
		// Token: 0x0600026E RID: 622 RVA: 0x00006EB5 File Offset: 0x000050B5
		public override void SetETag(string etag)
		{
			this._httpCachePolicy.SetETag(etag);
		}

		/// <summary>Sets the ETag HTTP header based on the time stamps of the handler's file dependencies.</summary>
		/// <exception cref="T:System.InvalidOperationException">The ETag header has already been set. </exception>
		// Token: 0x0600026F RID: 623 RVA: 0x00006EC3 File Offset: 0x000050C3
		public override void SetETagFromFileDependencies()
		{
			this._httpCachePolicy.SetETagFromFileDependencies();
		}

		/// <summary>Sets the Expires HTTP header to an absolute date and time.</summary>
		/// <param name="date">The absolute expiration time.</param>
		// Token: 0x06000270 RID: 624 RVA: 0x00006ED0 File Offset: 0x000050D0
		public override void SetExpires(DateTime date)
		{
			this._httpCachePolicy.SetExpires(date);
		}

		/// <summary>Sets the Last-Modified HTTP header to the specified date and time.</summary>
		/// <param name="date">The date-time value to set the Last-Modified header to.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="date" /> is later than the current DateTime. </exception>
		// Token: 0x06000271 RID: 625 RVA: 0x00006EDE File Offset: 0x000050DE
		public override void SetLastModified(DateTime date)
		{
			this._httpCachePolicy.SetLastModified(date);
		}

		/// <summary>Sets the Last-Modified HTTP header based on the time stamps of the handler's file dependencies.</summary>
		// Token: 0x06000272 RID: 626 RVA: 0x00006EEC File Offset: 0x000050EC
		public override void SetLastModifiedFromFileDependencies()
		{
			this._httpCachePolicy.SetLastModifiedFromFileDependencies();
		}

		/// <summary>Sets the Cache-Control: max-age HTTP header to the specified time span.</summary>
		/// <param name="delta">The time span to set the Cache-Control: max-age header to.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="delta" /> is less than 0 or greater than one year. </exception>
		// Token: 0x06000273 RID: 627 RVA: 0x00006EF9 File Offset: 0x000050F9
		public override void SetMaxAge(TimeSpan delta)
		{
			this._httpCachePolicy.SetMaxAge(delta);
		}

		/// <summary>Stops all origin-server caching for the current response.</summary>
		// Token: 0x06000274 RID: 628 RVA: 0x00006F07 File Offset: 0x00005107
		public override void SetNoServerCaching()
		{
			this._httpCachePolicy.SetNoServerCaching();
		}

		/// <summary>Sets the Cache-Control: no-store HTTP header.</summary>
		// Token: 0x06000275 RID: 629 RVA: 0x00006F14 File Offset: 0x00005114
		public override void SetNoStore()
		{
			this._httpCachePolicy.SetNoStore();
		}

		/// <summary>Sets the Cache-Control: no-transform HTTP header.</summary>
		// Token: 0x06000276 RID: 630 RVA: 0x00006F21 File Offset: 0x00005121
		public override void SetNoTransforms()
		{
			this._httpCachePolicy.SetNoTransforms();
		}

		/// <summary>Specifies whether the response contains the vary:* header when varying by parameters.</summary>
		/// <param name="omit">true to direct the <see cref="T:System.Web.HttpCachePolicy" /> object to not use the * value for its <see cref="P:System.Web.HttpCachePolicy.VaryByHeaders" /> property; otherwise, false.</param>
		// Token: 0x06000277 RID: 631 RVA: 0x00006F2E File Offset: 0x0000512E
		public override void SetOmitVaryStar(bool omit)
		{
			this._httpCachePolicy.SetOmitVaryStar(omit);
		}

		/// <summary>Sets the Cache-Control: s-maxage HTTP header to the specified time span.</summary>
		/// <param name="delta">The time span to set the Cache-Control: s-maxage header to.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="delta" /> is less than 0. </exception>
		// Token: 0x06000278 RID: 632 RVA: 0x00006F3C File Offset: 0x0000513C
		public override void SetProxyMaxAge(TimeSpan delta)
		{
			this._httpCachePolicy.SetProxyMaxAge(delta);
		}

		/// <summary>Sets the Cache-Control HTTP header to either the must-revalidate or the proxy-revalidate directives, based on the specified enumeration value.</summary>
		/// <param name="revalidation">The <see cref="T:System.Web.HttpCacheRevalidation" /> enumeration value to set the Cache-Control header to.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="revalidation" /> is not one of the enumeration values. </exception>
		// Token: 0x06000279 RID: 633 RVA: 0x00006F4A File Offset: 0x0000514A
		public override void SetRevalidation(HttpCacheRevalidation revalidation)
		{
			this._httpCachePolicy.SetRevalidation(revalidation);
		}

		/// <summary>Sets cache expiration to absolute or sliding.</summary>
		/// <param name="slide">true to set a sliding cache expiration, and false to set an absolute cache expiration.</param>
		// Token: 0x0600027A RID: 634 RVA: 0x00006F58 File Offset: 0x00005158
		public override void SetSlidingExpiration(bool slide)
		{
			this._httpCachePolicy.SetSlidingExpiration(slide);
		}

		/// <summary>Specifies whether the ASP.NET cache should ignore HTTP Cache-Control headers sent by the client that invalidate the cache.</summary>
		/// <param name="validUntilExpires">true to specify that ASP.NET should ignore Cache-Control invalidation headers; otherwise, false.</param>
		// Token: 0x0600027B RID: 635 RVA: 0x00006F66 File Offset: 0x00005166
		public override void SetValidUntilExpires(bool validUntilExpires)
		{
			this._httpCachePolicy.SetValidUntilExpires(validUntilExpires);
		}

		/// <summary>Specifies a text string to vary cached output responses by.</summary>
		/// <param name="custom">The text string to vary cached output by.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="custom" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.HttpCachePolicy.SetVaryByCustom(System.String)" /> method has already been called. </exception>
		// Token: 0x0600027C RID: 636 RVA: 0x00006F74 File Offset: 0x00005174
		public override void SetVaryByCustom(string custom)
		{
			this._httpCachePolicy.SetVaryByCustom(custom);
		}

		// Token: 0x04000D9C RID: 3484
		private HttpCachePolicy _httpCachePolicy;
	}
}
