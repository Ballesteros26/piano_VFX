using System;
using System.Runtime.CompilerServices;

namespace System.Web
{
	/// <summary>Serves as the base class for classes that contain methods for setting cache-specific HTTP headers and for controlling the ASP.NET page output cache.</summary>
	// Token: 0x02000036 RID: 54
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpCachePolicyBase
	{
		/// <summary>When overridden in a derived class, gets the list of Content-Encoding headers that are used to vary the output cache.</summary>
		/// <returns>The Content-Encoding headers that are used to select the cached response.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual HttpCacheVaryByContentEncodings VaryByContentEncodings
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the list of all HTTP headers that are used to vary cache output.</summary>
		/// <returns>The HTTP headers that are used to select the cached response.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual HttpCacheVaryByHeaders VaryByHeaders
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the list of parameters that are received by an HTTP GET or POST verb that affect caching.</summary>
		/// <returns>The cache-control parameters that are used to select the cached response.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual HttpCacheVaryByParams VaryByParams
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, registers a validation callback for the current response.</summary>
		/// <param name="handler">The object that will handle the request.</param>
		/// <param name="data">The user-supplied data that is passed to the <see cref="M:System.Web.HttpCachePolicyWrapper.AddValidationCallback(System.Web.HttpCacheValidateHandler,System.Object)" /> delegate.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000250 RID: 592 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void AddValidationCallback(HttpCacheValidateHandler handler, object data)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, appends the specified text to the Cache-Control HTTP header.</summary>
		/// <param name="extension">The text to append to the Cache-Control header.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000251 RID: 593 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void AppendCacheExtension(string extension)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, makes the response available in the browser history cache, regardless of the <see cref="T:System.Web.HttpCacheability" /> setting made on the server.</summary>
		/// <param name="allow">true to direct the client browser to store responses in the browser history cache; otherwise false.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000252 RID: 594 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetAllowResponseInBrowserHistory(bool allow)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, sets the Cache-Control header to the specified <see cref="T:System.Web.HttpCacheability" /> value.</summary>
		/// <param name="cacheability">The <see cref="T:System.Web.HttpCacheability" /> enumeration value to set the header to.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000253 RID: 595 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetCacheability(HttpCacheability cacheability)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, sets the Cache-Control header to the specified <see cref="T:System.Web.HttpCacheability" /> value and appends an extension to the directive.</summary>
		/// <param name="cacheability">The <see cref="T:System.Web.HttpCacheability" /> enumeration value to set the header to.</param>
		/// <param name="field">The cache-control extension to add to the header.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000254 RID: 596 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetCacheability(HttpCacheability cacheability, string field)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, sets the ETag HTTP header to the specified string.</summary>
		/// <param name="etag">The text to use for the ETag header.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000255 RID: 597 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetETag(string etag)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, sets the ETag HTTP header based on the time stamps of the handler's file dependencies.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000256 RID: 598 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetETagFromFileDependencies()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, sets the Expires HTTP header to an absolute date and time.</summary>
		/// <param name="date">The absolute expiration time.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000257 RID: 599 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetExpires(DateTime date)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, sets the Last-Modified HTTP header to the specified date and time.</summary>
		/// <param name="date">The date-time value to set the Last-Modified header to.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000258 RID: 600 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetLastModified(DateTime date)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, sets the Last-Modified HTTP header based on the time stamps of the handler's file dependencies.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000259 RID: 601 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetLastModifiedFromFileDependencies()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, sets the Cache-Control: max-age HTTP header to the specified time span.</summary>
		/// <param name="delta">The time span to set the Cache-Control: max-age header to.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600025A RID: 602 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetMaxAge(TimeSpan delta)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, stops all origin-server caching for the current response.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600025B RID: 603 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetNoServerCaching()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, sets the Cache-Control: no-store HTTP header.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600025C RID: 604 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetNoStore()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, sets the Cache-Control: no-transform HTTP header.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600025D RID: 605 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetNoTransforms()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, specifies whether the response contains the vary:* header when caching varies by parameters.</summary>
		/// <param name="omit">true to direct the <see cref="T:System.Web.HttpCachePolicy" /> object not to use the * value for its <see cref="P:System.Web.HttpCachePolicy.VaryByHeaders" /> property; otherwise, false.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600025E RID: 606 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetOmitVaryStar(bool omit)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, sets the Cache-Control: s-maxage HTTP header to the specified time span.</summary>
		/// <param name="delta">The time span to set the Cache-Control: s-maxage header to.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600025F RID: 607 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetProxyMaxAge(TimeSpan delta)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, sets the Cache-Control HTTP header to either the must-revalidate or the proxy-revalidate directives, based on the specified enumeration value.</summary>
		/// <param name="revalidation">The <see cref="T:System.Web.HttpCacheRevalidation" /> enumeration value to set the Cache-Control header to.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000260 RID: 608 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetRevalidation(HttpCacheRevalidation revalidation)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, sets cache expiration to absolute or sliding.</summary>
		/// <param name="slide">true to set a sliding cache expiration, or false to set an absolute cache expiration.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000261 RID: 609 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetSlidingExpiration(bool slide)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, specifies whether the ASP.NET cache should ignore HTTP Cache-Control headers that are sent by the client that invalidate the cache.</summary>
		/// <param name="validUntilExpires">true to specify that ASP.NET should ignore Cache-Control invalidation headers; otherwise, false.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000262 RID: 610 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetValidUntilExpires(bool validUntilExpires)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, specifies a text string to vary cached output responses by.</summary>
		/// <param name="custom">The text string to vary cached output by.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000263 RID: 611 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetVaryByCustom(string custom)
		{
			throw new NotImplementedException();
		}
	}
}
