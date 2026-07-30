using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Routing;

namespace System.Web
{
	/// <summary>Serves as the base class for classes that provides HTTP-response information from an ASP.NET operation.</summary>
	// Token: 0x0200003C RID: 60
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpResponseBase
	{
		/// <summary>When overridden in a derived class, gets or sets a value that indicates whether to buffer output and send it after the complete response has finished processing.</summary>
		/// <returns>true if the output is buffered; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool Buffer
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets a value that indicates whether to buffer output and send it after the complete page has finished processing.</summary>
		/// <returns>true if the output is buffered; otherwise false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002DA RID: 730 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool BufferOutput
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the caching policy (such as expiration time, privacy settings, and vary clauses) of the current Web page.</summary>
		/// <returns>The caching policy of the current response.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual HttpCachePolicyBase Cache
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the Cache-Control HTTP header that matches one of the <see cref="T:System.Web.HttpCacheability" /> enumeration values.</summary>
		/// <returns>The caching policy of the current response.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002DD RID: 733 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string CacheControl
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the HTTP character set of the current response.</summary>
		/// <returns>The HTTP character set of the current response.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002DF RID: 735 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string Charset
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When implemented in a derived class, gets a <see cref="T:System.Threading.CancellationToken" /> object that is tripped when the client disconnects.</summary>
		/// <returns>The cancellation token.</returns>
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual CancellationToken ClientDisconnectedToken
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the content encoding of the current response.</summary>
		/// <returns>Information about the content encoding of the current response.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Encoding ContentEncoding
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the HTTP MIME type of the current response.</summary>
		/// <returns>The HTTP MIME type of the current response. </returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string ContentType
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the response cookie collection.</summary>
		/// <returns>The response cookie collection.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual HttpCookieCollection Cookies
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the number of minutes before a page that is cached on the client or proxy expires. If the user returns to the same page before it expires, the cached version is displayed. <see cref="P:System.Web.HttpResponseBase.Expires" /> is provided for compatibility with earlier versions of Active Server Pages (ASP).</summary>
		/// <returns>The number of minutes before the page expires.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int Expires
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the absolute date and time at which cached information expires in the cache. <see cref="P:System.Web.HttpResponseBase.ExpiresAbsolute" /> is provided for compatibility with earlier versions of Active Server Pages (ASP).</summary>
		/// <returns>The date and time at which the page expires.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual DateTime ExpiresAbsolute
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets a filter object that is used to modify the HTTP entity body before transmission.</summary>
		/// <returns>An object that acts as the output filter.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Stream Filter
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the collection of response headers.</summary>
		/// <returns>The response headers.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual NameValueCollection Headers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool HeadersWritten
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the encoding for the header of the current response.</summary>
		/// <returns>Information about the encoding for the current header.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002EE RID: 750 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Encoding HeaderEncoding
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the client is connected to the server.</summary>
		/// <returns>true if the client is currently connected; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsClientConnected
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the client is being redirected to a new location.</summary>
		/// <returns>true if the value of the location response header differs from the current location; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsRequestBeingRedirected
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the object that enables text output to the HTTP response stream.</summary>
		/// <returns>An object that enables output to the client.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual TextWriter Output
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, enables binary output to the outgoing HTTP content body.</summary>
		/// <returns>An object that represents the raw contents of the outgoing HTTP content body.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Stream OutputStream
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the value of the HTTP Location header.</summary>
		/// <returns>The absolute URL of the HTTP Location header.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string RedirectLocation
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the Status value that is returned to the client.</summary>
		/// <returns>The status of the HTTP output. For information about valid status codes, see HTTP Status Codes on the MSDN Web site.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string Status
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the HTTP status code of the output that is returned to the client.</summary>
		/// <returns>The status code of the HTTP output that is returned to the client. For information about valid status codes, see HTTP Status Codes on the MSDN Web site.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002FA RID: 762 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int StatusCode
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the HTTP status message of the output that is returned to the client.</summary>
		/// <returns>The status message of the HTTP output that is returned to the client. For information about valid status codes, see HTTP Status Codes on the MSDN Web site.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002FC RID: 764 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string StatusDescription
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets a value that qualifies the status code of the response.</summary>
		/// <returns>The IIS 7.0 substatus code.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002FE RID: 766 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int SubStatusCode
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When implemented in a derived class, gets a value that indicates whether the connection supports asynchronous flush operation.</summary>
		/// <returns>true if the connection supports asynchronous flush operations; otherwise, false.</returns>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SupportsAsyncFlush
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets a value that indicates whether to send HTTP content to the client.</summary>
		/// <returns>true if output is suppressed; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SuppressContent
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SuppressDefaultCacheControlHeader
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When implemented in a derived class, gets or sets a value that specifies whether forms authentication redirection to the login page should be suppressed.</summary>
		/// <returns>true if forms authentication redirection should be suppressed; otherwise, false.</returns>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool SuppressFormsAuthenticationRedirect
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets a value that specifies whether IIS 7.0 custom errors are disabled.</summary>
		/// <returns>true if IIS custom errors are disabled; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool TrySkipIisCustomErrors
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, makes the validity of a cached response dependent on the specified item in the cache.</summary>
		/// <param name="cacheKey">The key of the item that the cached response is dependent on.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000308 RID: 776 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void AddCacheItemDependency(string cacheKey)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, makes the validity of a cached response dependent on the specified items in the cache.</summary>
		/// <param name="cacheKeys">A collection that contains the keys of the items that the cached response is dependent on.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000309 RID: 777 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void AddCacheItemDependencies(ArrayList cacheKeys)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, makes the validity of a cached item dependent on the specified items in the cache.</summary>
		/// <param name="cacheKeys">An array that contains the keys of the items that the cached response is dependent on.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600030A RID: 778 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void AddCacheItemDependencies(string[] cacheKeys)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, associates cache dependencies with the response that enable the response to be invalidated if it is cached and if the specified dependencies change.</summary>
		/// <param name="dependencies">A file, cache key, or <see cref="T:System.Web.Caching.CacheDependency" /> object to add to the list of application dependencies.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600030B RID: 779 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void AddCacheDependency(params CacheDependency[] dependencies)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, adds a single file name to the collection of file names on which the current response is dependent.</summary>
		/// <param name="filename">The name of the file to add.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600030C RID: 780 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void AddFileDependency(string filename)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, adds file names to the collection of file names on which the current response is dependent.</summary>
		/// <param name="filenames">The names of the files to add.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600030D RID: 781 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void AddFileDependencies(ArrayList filenames)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, adds an array of file names to the collection of file names on which the current response is dependent.</summary>
		/// <param name="filenames">An array of file names to add.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600030E RID: 782 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void AddFileDependencies(string[] filenames)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, adds an HTTP header to the current response. This method is provided for compatibility with earlier versions of ASP.</summary>
		/// <param name="name">The name of the HTTP header to add <paramref name="value" /> to.</param>
		/// <param name="value">The string to add to the header.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600030F RID: 783 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void AddHeader(string name, string value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual ISubscriptionToken AddOnSendingHeaders(Action<HttpContextBase> callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, adds an HTTP cookie to the HTTP response cookie collection.</summary>
		/// <param name="cookie">The cookie to add to the response.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000311 RID: 785 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void AppendCookie(HttpCookie cookie)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, adds an HTTP header to the current response.</summary>
		/// <param name="name">The name of the HTTP header to add to the current response.</param>
		/// <param name="value">The value of the header.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000312 RID: 786 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void AppendHeader(string name, string value)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, adds custom log information to the Internet Information Services (IIS) log file.</summary>
		/// <param name="param">The text to add to the log file.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000313 RID: 787 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void AppendToLog(string param)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, adds a session ID to the virtual path if the session is using <see cref="P:System.Web.Configuration.SessionStateSection.Cookieless" /> session state, and returns the combined path. </summary>
		/// <returns>The virtual path, with the session ID inserted.</returns>
		/// <param name="virtualPath">The virtual path of a resource.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000314 RID: 788 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string ApplyAppPathModifier(string virtualPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>When implemented in a derived class, sends the currently buffered response to the client.</summary>
		/// <returns>The asynchronous result object.</returns>
		/// <param name="callback">The callback object.</param>
		/// <param name="state">The response state.</param>
		// Token: 0x06000315 RID: 789 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual IAsyncResult BeginFlush(AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, writes a string of binary characters to the HTTP output stream.</summary>
		/// <param name="buffer">The binary characters to write to the current response.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000316 RID: 790 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void BinaryWrite(byte[] buffer)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, clears all headers and content output from the current response.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000317 RID: 791 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Clear()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, clears all content from the current response.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000318 RID: 792 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void ClearContent()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, clears all headers from the current response.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000319 RID: 793 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void ClearHeaders()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, closes the socket connection to a client.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600031A RID: 794 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Close()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, disables kernel caching for the current response.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600031B RID: 795 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void DisableKernelCache()
		{
			throw new NotImplementedException();
		}

		/// <summary>When implemented in a derived class, disables IIS user-mode caching for this response.</summary>
		// Token: 0x0600031C RID: 796 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void DisableUserCache()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, sends all currently buffered output to the client, stops execution of the requested process, and raises the <see cref="E:System.Web.HttpApplication.EndRequest" /> event.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600031D RID: 797 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void End()
		{
			throw new NotImplementedException();
		}

		/// <summary>When implemented in a derived class, completes an asynchronous flush operation.</summary>
		/// <param name="asyncResult">The asynchronous result object.</param>
		// Token: 0x0600031E RID: 798 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void EndFlush(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, sends all currently buffered output to the client.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600031F RID: 799 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Flush()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Task FlushAsync()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, appends an HTTP PICS-Label header to the current response.</summary>
		/// <param name="value">The string to add to the PICS-Label header.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000321 RID: 801 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Pics(string value)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, redirects a request to the specified URL.</summary>
		/// <param name="url">The target location.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000322 RID: 802 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Redirect(string url)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, redirects a request to the specified URL and specifies whether execution of the current process should terminate.</summary>
		/// <param name="url">The target location. </param>
		/// <param name="endResponse">true to terminate the current process. </param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000323 RID: 803 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Redirect(string url, bool endResponse)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, redirects the request to a new URL by using route parameter values.</summary>
		/// <param name="routeValues">The route parameter values.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000324 RID: 804 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void RedirectToRoute(object routeValues)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, redirects the request to a new URL by using a route name.</summary>
		/// <param name="routeName">The name of the route.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000325 RID: 805 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void RedirectToRoute(string routeName)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, redirects the request to a new URL by using route parameter values.</summary>
		/// <param name="routeValues">The route parameter values.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000326 RID: 806 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void RedirectToRoute(RouteValueDictionary routeValues)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, redirects the request to a new URL by using route parameter values and a route name.</summary>
		/// <param name="routeName">The name of the route.</param>
		/// <param name="routeValues">The route parameter values.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000327 RID: 807 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void RedirectToRoute(string routeName, object routeValues)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, redirects the request to a new URL by using route parameter values and a route name.</summary>
		/// <param name="routeName">The name of the route.</param>
		/// <param name="routeValues">The route parameter values.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000328 RID: 808 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void RedirectToRoute(string routeName, RouteValueDictionary routeValues)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, performs a permanent redirection from the requested URL to a new URL by using route parameter values.</summary>
		/// <param name="routeValues">The route parameter values.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000329 RID: 809 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void RedirectToRoutePermanent(object routeValues)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, performs a permanent redirection from the requested URL to a new URL by using a route name.</summary>
		/// <param name="routeName">The name of the route.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600032A RID: 810 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void RedirectToRoutePermanent(string routeName)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, performs a permanent redirection from the requested URL to a new URL by using route parameter values.</summary>
		/// <param name="routeValues">The route parameter values.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600032B RID: 811 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void RedirectToRoutePermanent(RouteValueDictionary routeValues)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, performs a permanent redirection from the requested URL to a new URL by using the route parameter values and the name of the route that correspond to the new URL.</summary>
		/// <param name="routeName">The name of the route.</param>
		/// <param name="routeValues">The route parameter values.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600032C RID: 812 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void RedirectToRoutePermanent(string routeName, object routeValues)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, performs a permanent redirection from the requested URL to a new URL by using route parameter values and a route name.</summary>
		/// <param name="routeName">The name of the route.</param>
		/// <param name="routeValues">The route parameter values.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600032D RID: 813 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void RedirectToRoutePermanent(string routeName, RouteValueDictionary routeValues)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, performs a permanent redirect from the requested URL to the specified URL.</summary>
		/// <param name="url">The location to which the request is redirected.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600032E RID: 814 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void RedirectPermanent(string url)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, performs a permanent redirect from the requested URL to the specified URL, and provides the option to complete the response.</summary>
		/// <param name="url">The location to which the request is redirected.</param>
		/// <param name="endResponse">true to terminate the response; otherwise false. The default is false.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600032F RID: 815 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void RedirectPermanent(string url, bool endResponse)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, removes from the cache all cached items that are associated with the specified path.</summary>
		/// <param name="path">The virtual absolute path to the items to be removed from the cache.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000330 RID: 816 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void RemoveOutputCacheItem(string path)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, uses the specified output-cache provider to remove all output-cache artifacts that are associated with the specified path.</summary>
		/// <param name="path">The virtual absolute path of the items that are removed from the cache. </param>
		/// <param name="providerName">The provider that is used to remove the output-cache artifacts that are associated with the specified path.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000331 RID: 817 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void RemoveOutputCacheItem(string path, string providerName)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, updates an existing cookie in the cookie collection.</summary>
		/// <param name="cookie">The cookie in the collection to be updated.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000332 RID: 818 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SetCookie(HttpCookie cookie)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, writes the specified file to the HTTP response output stream, without buffering it in memory.</summary>
		/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000333 RID: 819 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void TransmitFile(string filename)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, writes the specified part of a file to the HTTP response output stream, without buffering it in memory.</summary>
		/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
		/// <param name="offset">The position in the file where writing starts.</param>
		/// <param name="length">The number of bytes to write, starting at <paramref name="offset" />.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000334 RID: 820 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void TransmitFile(string filename, long offset, long length)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, writes a character to an HTTP response output stream.</summary>
		/// <param name="ch">The character to write to the HTTP output stream.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000335 RID: 821 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Write(char ch)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, writes the specified array of characters to the HTTP response output stream.</summary>
		/// <param name="buffer">The character array to write.</param>
		/// <param name="index">The position in the character array where writing starts.</param>
		/// <param name="count">The number of characters to write, starting at <paramref name="index" />.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000336 RID: 822 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Write(char[] buffer, int index, int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, writes the specified object to the HTTP response stream.</summary>
		/// <param name="obj">The object to write to the HTTP output stream.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000337 RID: 823 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Write(object obj)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, writes the specified string to the HTTP response output stream.</summary>
		/// <param name="s">The string to write to the HTTP output stream.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000338 RID: 824 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Write(string s)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, writes the contents of the specified file to the HTTP response output stream as a file block.</summary>
		/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000339 RID: 825 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void WriteFile(string filename)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, writes the contents of the specified file to the HTTP response output stream and specifies whether the content is written as a memory block.</summary>
		/// <param name="filename">The name of the file to write to the current response.</param>
		/// <param name="readIntoMemory">true to write the file into a memory block.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600033A RID: 826 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void WriteFile(string filename, bool readIntoMemory)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, writes the specified file to the HTTP response output stream.</summary>
		/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
		/// <param name="offset">The position in the file where writing starts.</param>
		/// <param name="size">The number of bytes to write, starting at <paramref name="offset" />.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600033B RID: 827 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void WriteFile(string filename, long offset, long size)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, writes the specified file to the HTTP response output stream.</summary>
		/// <param name="fileHandle">The file handle of the file to write to the HTTP output stream.</param>
		/// <param name="offset">The position in the file where writing starts.</param>
		/// <param name="size">The number of bytes to write, starting at <paramref name="offset" />.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600033C RID: 828 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void WriteFile(IntPtr fileHandle, long offset, long size)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, inserts substitution blocks into the response, which enables dynamic generation of regions for cached output responses.</summary>
		/// <param name="callback">The method, user control, or object to substitute.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600033D RID: 829 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void WriteSubstitution(HttpResponseSubstitutionCallback callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void PushPromise(string path)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void PushPromise(string path, string method, NameValueCollection headers)
		{
			throw new NotImplementedException();
		}
	}
}
