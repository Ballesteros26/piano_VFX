using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Util;
using Unity;

namespace System.Web
{
	/// <summary>Encapsulates HTTP-response information from an ASP.NET operation.</summary>
	// Token: 0x020000A6 RID: 166
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpResponse
	{
		// Token: 0x0600086B RID: 2155 RVA: 0x00014840 File Offset: 0x00012A40
		internal HttpResponse()
		{
			this.output_stream = new HttpResponseStream(this);
			this.writer = new HttpWriter(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpResponse" /> class.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> object that enables custom HTTP output.</param>
		// Token: 0x0600086C RID: 2156 RVA: 0x000148A6 File Offset: 0x00012AA6
		public HttpResponse(TextWriter writer)
			: this()
		{
			this.writer = writer;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000148B8 File Offset: 0x00012AB8
		internal HttpResponse(HttpWorkerRequest worker_request, HttpContext context)
			: this()
		{
			this.WorkerRequest = worker_request;
			this.context = context;
			if (worker_request != null && worker_request.GetHttpVersion() == "HTTP/1.1")
			{
				string serverVariable = worker_request.GetServerVariable("GATEWAY_INTERFACE");
				this.use_chunked = string.IsNullOrEmpty(serverVariable) || !serverVariable.StartsWith("cgi", StringComparison.OrdinalIgnoreCase);
			}
			else
			{
				this.use_chunked = false;
			}
			this.writer = new HttpWriter(this);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001492F File Offset: 0x00012B2F
		internal TextWriter SetTextWriter(TextWriter writer)
		{
			TextWriter textWriter = this.writer;
			this.writer = writer;
			return textWriter;
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00014940 File Offset: 0x00012B40
		internal string VersionHeader
		{
			get
			{
				if (!this.version_header_checked && this.version_header == null)
				{
					this.version_header_checked = true;
					HttpRuntimeSection section = HttpRuntime.Section;
					if (section != null && section.EnableVersionHeader)
					{
						this.version_header = Environment.Version.ToString(3);
					}
				}
				return this.version_header;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x0001498C File Offset: 0x00012B8C
		// (set) Token: 0x06000871 RID: 2161 RVA: 0x00014994 File Offset: 0x00012B94
		internal HttpContext Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0001499D File Offset: 0x00012B9D
		internal string[] FileDependencies
		{
			get
			{
				if (this.fileDependencies == null || this.fileDependencies.Count == 0)
				{
					return new string[0];
				}
				return (string[])this.fileDependencies.ToArray(typeof(string));
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x000149D5 File Offset: 0x00012BD5
		private ArrayList FileDependenciesArray
		{
			get
			{
				if (this.fileDependencies == null)
				{
					this.fileDependencies = new ArrayList();
				}
				return this.fileDependencies;
			}
		}

		/// <summary>Gets or sets a value indicating whether to buffer output and send it after the complete response is finished processing.</summary>
		/// <returns>true if the output to client is buffered; otherwise, false.</returns>
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x000149F0 File Offset: 0x00012BF0
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x000149F8 File Offset: 0x00012BF8
		public bool Buffer
		{
			get
			{
				return this.buffer;
			}
			set
			{
				this.buffer = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to buffer output and send it after the complete page is finished processing.</summary>
		/// <returns>true if the output to client is buffered; otherwise false. The default is true.</returns>
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x000149F0 File Offset: 0x00012BF0
		// (set) Token: 0x06000877 RID: 2167 RVA: 0x000149F8 File Offset: 0x00012BF8
		public bool BufferOutput
		{
			get
			{
				return this.buffer;
			}
			set
			{
				this.buffer = value;
			}
		}

		/// <summary>Gets or sets the HTTP character set of the output stream.</summary>
		/// <returns>A <see cref="T:System.Text.Encoding" /> object that contains information about the character set of the current response.</returns>
		/// <exception cref="T:System.ArgumentNullException">Attempted to set <see cref="P:System.Web.HttpResponse.ContentEncoding" /> to null.</exception>
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x00014A04 File Offset: 0x00012C04
		// (set) Token: 0x06000879 RID: 2169 RVA: 0x00014A7C File Offset: 0x00012C7C
		public Encoding ContentEncoding
		{
			get
			{
				if (this.encoding == null)
				{
					if (this.context != null)
					{
						string parameter = HttpRequest.GetParameter(this.context.Request.ContentType, "; charset=");
						if (parameter != null)
						{
							try
							{
								this.encoding = Encoding.GetEncoding(parameter);
							}
							catch
							{
							}
						}
					}
					if (this.encoding == null)
					{
						this.encoding = WebEncoding.ResponseEncoding;
					}
				}
				return this.encoding;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentException("ContentEncoding can not be null");
				}
				this.encoding = value;
				HttpWriter httpWriter = this.writer as HttpWriter;
				if (httpWriter != null)
				{
					httpWriter.SetEncoding(this.encoding);
				}
			}
		}

		/// <summary>Gets or sets the HTTP MIME type of the output stream.</summary>
		/// <returns>The HTTP MIME type of the output stream. The default value is "text/html".</returns>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.HttpResponse.ContentType" /> property is set to null.</exception>
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x00014AB9 File Offset: 0x00012CB9
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x00014AC1 File Offset: 0x00012CC1
		public string ContentType
		{
			get
			{
				return this.content_type;
			}
			set
			{
				this.content_type = value;
			}
		}

		/// <summary>Gets or sets the HTTP character set of the output stream.</summary>
		/// <returns>The HTTP character set of the output stream.</returns>
		/// <exception cref="T:System.Web.HttpException">The Charset property was set after headers were sent.</exception>
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x00014ACA File Offset: 0x00012CCA
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x00014AEB File Offset: 0x00012CEB
		public string Charset
		{
			get
			{
				if (this.charset == null)
				{
					this.charset = this.ContentEncoding.WebName;
				}
				return this.charset;
			}
			set
			{
				this.charset_set = true;
				this.charset = value;
			}
		}

		/// <summary>Gets the response cookie collection.</summary>
		/// <returns>The response cookie collection.</returns>
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x00014AFB File Offset: 0x00012CFB
		public HttpCookieCollection Cookies
		{
			get
			{
				if (this.cookies == null)
				{
					this.cookies = new HttpCookieCollection(true, false);
				}
				return this.cookies;
			}
		}

		/// <summary>Gets or sets the number of minutes before a page cached on a browser expires. If the user returns to the same page before it expires, the cached version is displayed. <see cref="P:System.Web.HttpResponse.Expires" /> is provided for compatibility with earlier versions of ASP.</summary>
		/// <returns>The number of minutes before the page expires.</returns>
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00014B18 File Offset: 0x00012D18
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x00014B2F File Offset: 0x00012D2F
		public int Expires
		{
			get
			{
				if (this.cache_policy == null)
				{
					return 0;
				}
				return this.cache_policy.ExpireMinutes();
			}
			set
			{
				this.Cache.SetExpires(DateTime.Now + new TimeSpan(0, value, 0));
			}
		}

		/// <summary>Gets or sets the absolute date and time at which to remove cached information from the cache. <see cref="P:System.Web.HttpResponse.ExpiresAbsolute" /> is provided for compatibility with earlier versions of ASP.</summary>
		/// <returns>The date and time at which the page expires.</returns>
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00014B4E File Offset: 0x00012D4E
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x00014B5B File Offset: 0x00012D5B
		public DateTime ExpiresAbsolute
		{
			get
			{
				return this.Cache.Expires;
			}
			set
			{
				this.Cache.SetExpires(value);
			}
		}

		/// <summary>Gets or sets a wrapping filter object that is used to modify the HTTP entity body before transmission.</summary>
		/// <returns>The <see cref="T:System.IO.Stream" /> object that acts as the output filter.</returns>
		/// <exception cref="T:System.Web.HttpException">Filtering is not allowed with the entity.</exception>
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00014B69 File Offset: 0x00012D69
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x00014B80 File Offset: 0x00012D80
		public Stream Filter
		{
			get
			{
				if (this.WorkerRequest == null)
				{
					return null;
				}
				return this.output_stream.Filter;
			}
			set
			{
				this.output_stream.Filter = value;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Text.Encoding" /> object that represents the encoding for the current header output stream.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> that contains information about the character set for the current header.</returns>
		/// <exception cref="T:System.ArgumentNullException">The encoding value is null.</exception>
		/// <exception cref="T:System.Web.HttpException">The encoding value is <see cref="P:System.Text.Encoding.Unicode" />.- or -The headers have already been sent.</exception>
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00014B90 File Offset: 0x00012D90
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x00014BF9 File Offset: 0x00012DF9
		public Encoding HeaderEncoding
		{
			get
			{
				if (this.headerEncoding == null)
				{
					GlobalizationSection globalizationSection = WebConfigurationManager.SafeGetSection("system.web/globalization", typeof(GlobalizationSection)) as GlobalizationSection;
					if (globalizationSection == null)
					{
						this.headerEncoding = Encoding.UTF8;
					}
					else
					{
						this.headerEncoding = globalizationSection.ResponseHeaderEncoding;
						if (this.headerEncoding == Encoding.Unicode)
						{
							throw new HttpException("HeaderEncoding must not be Unicode");
						}
					}
				}
				return this.headerEncoding;
			}
			set
			{
				if (this.headers_sent)
				{
					throw new HttpException("headers have already been sent");
				}
				if (value == null)
				{
					throw new ArgumentNullException("HeaderEncoding");
				}
				if (value == Encoding.Unicode)
				{
					throw new HttpException("HeaderEncoding must not be Unicode");
				}
				this.headerEncoding = value;
			}
		}

		/// <summary>Gets the collection of response headers.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of response headers.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The operation requires the integrated pipeline mode in IIS 7.0 and at least the .NET Framework version 3.0.</exception>
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00014C36 File Offset: 0x00012E36
		public NameValueCollection Headers
		{
			get
			{
				if (this.headers == null)
				{
					this.headers = new HttpHeaderCollection();
				}
				return this.headers;
			}
		}

		/// <summary>Gets a value indicating whether the client is still connected to the server.</summary>
		/// <returns>true if the client is currently connected; otherwise, false.</returns>
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x00014C51 File Offset: 0x00012E51
		public bool IsClientConnected
		{
			get
			{
				return this.WorkerRequest == null || this.WorkerRequest.IsClientConnected();
			}
		}

		/// <summary>Gets a Boolean value indicating whether the client is being transferred to a new location.</summary>
		/// <returns>true if the value of the location response header is different than the current location; otherwise, false.</returns>
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x00014C68 File Offset: 0x00012E68
		public bool IsRequestBeingRedirected
		{
			get
			{
				return this.is_request_being_redirected;
			}
		}

		/// <summary>Enables output of text to the outgoing HTTP response stream.</summary>
		/// <returns>A <see cref="T:System.IO.TextWriter" /> object that enables custom output to the client.</returns>
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00014C70 File Offset: 0x00012E70
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x00014C78 File Offset: 0x00012E78
		public TextWriter Output
		{
			get
			{
				return this.writer;
			}
			set
			{
				this.writer = value;
			}
		}

		/// <summary>Enables binary output to the outgoing HTTP content body.</summary>
		/// <returns>An IO <see cref="T:System.IO.Stream" /> representing the raw contents of the outgoing HTTP content body.</returns>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="OutputStream" /> is not available.</exception>
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00014C81 File Offset: 0x00012E81
		public Stream OutputStream
		{
			get
			{
				return this.output_stream;
			}
		}

		/// <summary>Gets or sets the value of the Http Location header.</summary>
		/// <returns>The absolute URI that is transmitted to the client in the HTTP Location header.</returns>
		/// <exception cref="T:System.Web.HttpException">The HTTP headers have already been written.</exception>
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00014C89 File Offset: 0x00012E89
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x00014C91 File Offset: 0x00012E91
		public string RedirectLocation
		{
			get
			{
				return this.redirect_location;
			}
			set
			{
				this.redirect_location = value;
			}
		}

		/// <summary>Sets the Status line that is returned to the client.</summary>
		/// <returns>Setting the status code causes a string describing the status of the HTTP output to be returned to the client. The default value is 200 (OK).</returns>
		/// <exception cref="T:System.Web.HttpException">Status is set to an invalid status code.</exception>
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x00014C9A File Offset: 0x00012E9A
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x00014CB8 File Offset: 0x00012EB8
		public string Status
		{
			get
			{
				return this.status_code.ToString() + " " + this.StatusDescription;
			}
			set
			{
				int num = value.IndexOf(' ');
				if (num == -1)
				{
					throw new HttpException("Invalid format for the Status property");
				}
				if (!int.TryParse(value.Substring(0, num), out this.status_code))
				{
					throw new HttpException("Invalid format for the Status property");
				}
				this.status_description = value.Substring(num + 1);
			}
		}

		/// <summary>Gets or sets a value qualifying the status code of the response.</summary>
		/// <returns>An integer value that represents the IIS 7.0 substatus code.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The operation requires the integrated pipeline mode in IIS 7.0 and at least the .NET Framework version 3.0.</exception>
		/// <exception cref="T:System.Web.HttpException">The status code is set after all HTTP headers have been sent.</exception>
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00014D0C File Offset: 0x00012F0C
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x00014D14 File Offset: 0x00012F14
		public int SubStatusCode { get; set; }

		/// <summary>Gets or sets a value that specifies whether forms authentication redirection to the login page should be suppressed.</summary>
		/// <returns>true if forms authentication redirection should be suppressed; otherwise, false.</returns>
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00014D1D File Offset: 0x00012F1D
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x00014D25 File Offset: 0x00012F25
		public bool SuppressFormsAuthenticationRedirect { get; set; }

		/// <summary>Gets or sets a value that specifies whether IIS 7.0 custom errors are disabled.</summary>
		/// <returns>true to disable IIS custom errors; otherwise, false.</returns>
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00014D2E File Offset: 0x00012F2E
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x00014D36 File Offset: 0x00012F36
		public bool TrySkipIisCustomErrors { get; set; }

		/// <summary>Gets or sets the HTTP status code of the output returned to the client.</summary>
		/// <returns>An Integer representing the status of the HTTP output returned to the client. The default value is 200 (OK). For a listing of valid status codes, see Http Status Codes.</returns>
		/// <exception cref="T:System.Web.HttpException">
		///   <see cref="P:System.Web.HttpResponse.StatusCode" /> is set after the HTTP headers have been sent.</exception>
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00014D3F File Offset: 0x00012F3F
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x00014D47 File Offset: 0x00012F47
		public int StatusCode
		{
			get
			{
				return this.status_code;
			}
			set
			{
				if (this.headers_sent)
				{
					throw new HttpException("headers have already been sent");
				}
				this.status_code = value;
				this.status_description = null;
			}
		}

		/// <summary>Gets or sets the HTTP status string of the output returned to the client.</summary>
		/// <returns>A string that describes the status of the HTTP output returned to the client. The default value is "OK". For a listing of valid status codes, see Http Status Codes.</returns>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="StatusDescription" /> is set after the HTTP headers have been sent.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value has a length greater than 512.</exception>
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00014D6A File Offset: 0x00012F6A
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x00014D8B File Offset: 0x00012F8B
		public string StatusDescription
		{
			get
			{
				if (this.status_description == null)
				{
					this.status_description = HttpWorkerRequest.GetStatusDescription(this.status_code);
				}
				return this.status_description;
			}
			set
			{
				if (this.headers_sent)
				{
					throw new HttpException("headers have already been sent");
				}
				this.status_description = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to send HTTP content to the client.</summary>
		/// <returns>true to suppress output; otherwise, false.</returns>
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00014DA7 File Offset: 0x00012FA7
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x00014DAF File Offset: 0x00012FAF
		public bool SuppressContent
		{
			get
			{
				return this.suppress_content;
			}
			set
			{
				this.suppress_content = value;
			}
		}

		/// <summary>Associates a set of cache dependencies with the response to facilitate invalidation of the response if it is stored in the output cache and the specified dependencies change.</summary>
		/// <param name="dependencies">A file, cache key, or <see cref="T:System.Web.Caching.CacheDependency" /> to add to the list of application dependencies.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dependencies" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">This method was called too late in the cache processing pipeline, after the cached response was already created.</exception>
		// Token: 0x0600089D RID: 2205 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public void AddCacheDependency(params CacheDependency[] dependencies)
		{
			throw new NotImplementedException();
		}

		/// <summary>Makes the validity of a cached item dependent on another item in the cache.</summary>
		/// <param name="cacheKeys">An array of item keys that the cached response is dependent upon.</param>
		// Token: 0x0600089E RID: 2206 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public void AddCacheItemDependencies(string[] cacheKeys)
		{
			throw new NotImplementedException();
		}

		/// <summary>Makes the validity of a cached response dependent on other items in the cache.</summary>
		/// <param name="cacheKeys">The <see cref="T:System.Collections.ArrayList" /> that contains the keys of the items that the current cached response is dependent upon.</param>
		// Token: 0x0600089F RID: 2207 RVA: 0x0000393A File Offset: 0x00001B3A
		[global::System.MonoTODO("Currently does nothing")]
		public void AddCacheItemDependencies(ArrayList cacheKeys)
		{
		}

		/// <summary>Makes the validity of a cached response dependent on another item in the cache.</summary>
		/// <param name="cacheKey">The key of the item that the cached response is dependent upon.</param>
		// Token: 0x060008A0 RID: 2208 RVA: 0x0000393A File Offset: 0x00001B3A
		[global::System.MonoTODO("Currently does nothing")]
		public void AddCacheItemDependency(string cacheKey)
		{
		}

		/// <summary>Adds a group of file names to the collection of file names on which the current response is dependent.</summary>
		/// <param name="filenames">The collection of files to add.</param>
		// Token: 0x060008A1 RID: 2209 RVA: 0x00014DB8 File Offset: 0x00012FB8
		public void AddFileDependencies(ArrayList filenames)
		{
			if (filenames == null || filenames.Count == 0)
			{
				return;
			}
			this.FileDependenciesArray.AddRange(filenames);
		}

		/// <summary>Adds an array of file names to the collection of file names on which the current response is dependent.</summary>
		/// <param name="filenames">An array of files to add.</param>
		// Token: 0x060008A2 RID: 2210 RVA: 0x00014DD2 File Offset: 0x00012FD2
		public void AddFileDependencies(string[] filenames)
		{
			if (filenames == null || filenames.Length == 0)
			{
				return;
			}
			this.FileDependenciesArray.AddRange(filenames);
		}

		/// <summary>Adds a single file name to the collection of file names on which the current response is dependent.</summary>
		/// <param name="filename">The name of the file to add.</param>
		// Token: 0x060008A3 RID: 2211 RVA: 0x00014DE8 File Offset: 0x00012FE8
		public void AddFileDependency(string filename)
		{
			if (filename == null || filename == string.Empty)
			{
				return;
			}
			this.FileDependenciesArray.Add(filename);
		}

		/// <summary>Adds an HTTP header to the output stream. <see cref="M:System.Web.HttpResponse.AddHeader(System.String,System.String)" /> is provided for compatibility with earlier versions of ASP.</summary>
		/// <param name="name">The name of the HTTP header to add <paramref name="value" /> to.</param>
		/// <param name="value">The string to add to the header.</param>
		// Token: 0x060008A4 RID: 2212 RVA: 0x00014E08 File Offset: 0x00013008
		public void AddHeader(string name, string value)
		{
			this.AppendHeader(name, value);
		}

		/// <summary>Adds an HTTP cookie to the intrinsic cookie collection.</summary>
		/// <param name="cookie">The <see cref="T:System.Web.HttpCookie" /> to add to the output stream.</param>
		/// <exception cref="T:System.Web.HttpException">A cookie is appended after the HTTP headers have been sent.</exception>
		// Token: 0x060008A5 RID: 2213 RVA: 0x00014E12 File Offset: 0x00013012
		public void AppendCookie(HttpCookie cookie)
		{
			this.Cookies.Add(cookie);
		}

		/// <summary>Adds an HTTP header to the output stream.</summary>
		/// <param name="name">The name of the HTTP header to add to the output stream.</param>
		/// <param name="value">The string to append to the header.</param>
		/// <exception cref="T:System.Web.HttpException">The header is appended after the HTTP headers have been sent.</exception>
		// Token: 0x060008A6 RID: 2214 RVA: 0x00014E20 File Offset: 0x00013020
		public void AppendHeader(string name, string value)
		{
			if (this.headers_sent)
			{
				throw new HttpException("Headers have been already sent");
			}
			if (string.Compare(name, "content-length", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.content_length = (long)ulong.Parse(value);
				this.use_chunked = false;
				return;
			}
			if (string.Compare(name, "content-type", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.ContentType = value;
				return;
			}
			if (string.Compare(name, "transfer-encoding", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.transfer_encoding = value;
				this.use_chunked = false;
				return;
			}
			if (string.Compare(name, "cache-control", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.user_cache_control = value;
				return;
			}
			this.Headers.Add(name, value);
		}

		/// <summary>Adds custom log information to the Internet Information Services (IIS) log file.</summary>
		/// <param name="param">The text to add to the log file.</param>
		// Token: 0x060008A7 RID: 2215 RVA: 0x00014EB8 File Offset: 0x000130B8
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		public void AppendToLog(string param)
		{
			Console.Write("System.Web: ");
			Console.WriteLine(param);
		}

		/// <summary>Adds a session ID to the virtual path if the session is using <see cref="P:System.Web.Configuration.SessionStateSection.Cookieless" /> session state and returns the combined path. If <see cref="P:System.Web.Configuration.SessionStateSection.Cookieless" /> session state is not used, <see cref="M:System.Web.HttpResponse.ApplyAppPathModifier(System.String)" /> returns the original virtual path.</summary>
		/// <returns>The <paramref name="virtualPath" /> with the session ID inserted.</returns>
		/// <param name="virtualPath">The virtual path to a resource. </param>
		// Token: 0x060008A8 RID: 2216 RVA: 0x00014ECC File Offset: 0x000130CC
		public string ApplyAppPathModifier(string virtualPath)
		{
			if (virtualPath == null || this.context == null)
			{
				return null;
			}
			if (virtualPath.Length == 0)
			{
				return this.context.Request.RootVirtualDir;
			}
			if (UrlUtils.IsRelativeUrl(virtualPath))
			{
				virtualPath = UrlUtils.Combine(this.context.Request.RootVirtualDir, virtualPath);
			}
			else if (UrlUtils.IsRooted(virtualPath))
			{
				virtualPath = UrlUtils.Canonic(virtualPath);
			}
			SessionStateSection sessionStateSection = WebConfigurationManager.GetWebApplicationSection("system.web/sessionState") as SessionStateSection;
			if (!SessionStateModule.IsCookieLess(this.context, sessionStateSection))
			{
				return virtualPath;
			}
			if (this.app_path_mod != null && virtualPath.IndexOf(this.app_path_mod) < 0)
			{
				if (UrlUtils.HasSessionId(virtualPath))
				{
					virtualPath = UrlUtils.RemoveSessionId(VirtualPathUtility.GetDirectory(virtualPath), virtualPath);
				}
				return UrlUtils.InsertSessionId(this.app_path_mod, virtualPath);
			}
			return virtualPath;
		}

		/// <summary>Writes a string of binary characters to the HTTP output stream.</summary>
		/// <param name="buffer">The bytes to write to the output stream.</param>
		// Token: 0x060008A9 RID: 2217 RVA: 0x00014F8C File Offset: 0x0001318C
		public void BinaryWrite(byte[] buffer)
		{
			this.output_stream.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00014F9E File Offset: 0x0001319E
		internal void BinaryWrite(byte[] buffer, int start, int len)
		{
			this.output_stream.Write(buffer, start, len);
		}

		/// <summary>Clears all content output from the buffer stream.</summary>
		// Token: 0x060008AB RID: 2219 RVA: 0x00014FAE File Offset: 0x000131AE
		public void Clear()
		{
			this.ClearContent();
		}

		/// <summary>Clears all content output from the buffer stream.</summary>
		// Token: 0x060008AC RID: 2220 RVA: 0x00014FB6 File Offset: 0x000131B6
		public void ClearContent()
		{
			this.output_stream.Clear();
			this.content_length = -1L;
		}

		/// <summary>Clears all headers from the buffer stream.</summary>
		/// <exception cref="T:System.Web.HttpException">Headers are cleared after the HTTP headers have been sent.</exception>
		// Token: 0x060008AD RID: 2221 RVA: 0x00014FCC File Offset: 0x000131CC
		public void ClearHeaders()
		{
			if (this.headers_sent)
			{
				throw new HttpException("headers have been already sent");
			}
			this.content_length = -1L;
			this.content_type = "text/html";
			this.transfer_encoding = null;
			this.user_cache_control = "private";
			if (this.cache_policy != null)
			{
				this.cache_policy.Cacheability = HttpCacheability.Private;
			}
			if (this.headers != null)
			{
				this.headers.Clear();
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00015038 File Offset: 0x00013238
		internal bool HeadersSent
		{
			get
			{
				return this.headers_sent;
			}
		}

		/// <summary>Closes the socket connection to a client.</summary>
		// Token: 0x060008AF RID: 2223 RVA: 0x00015040 File Offset: 0x00013240
		public void Close()
		{
			if (this.closed)
			{
				return;
			}
			if (this.WorkerRequest != null)
			{
				this.WorkerRequest.CloseConnection();
			}
			this.closed = true;
		}

		/// <summary>Disables kernel caching for the current response.</summary>
		// Token: 0x060008B0 RID: 2224 RVA: 0x0000393A File Offset: 0x00001B3A
		public void DisableKernelCache()
		{
		}

		/// <summary>Sends all currently buffered output to the client, stops execution of the page, and raises the <see cref="E:System.Web.HttpApplication.EndRequest" /> event.</summary>
		/// <exception cref="T:System.Threading.ThreadAbortException">The call to <see cref="M:System.Web.HttpResponse.End" /> has terminated the current request.</exception>
		// Token: 0x060008B1 RID: 2225 RVA: 0x00015068 File Offset: 0x00013268
		public void End()
		{
			if (this.context == null)
			{
				return;
			}
			if (this.context.TimeoutPossible)
			{
				Thread.CurrentThread.Abort(FlagEnd.Value);
				return;
			}
			HttpApplication applicationInstance = this.context.ApplicationInstance;
			if (applicationInstance != null)
			{
				applicationInstance.CompleteRequest();
			}
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x000150B0 File Offset: 0x000132B0
		private void AddHeadersNoCache(NameValueCollection write_headers, bool final_flush)
		{
			if (this.use_chunked)
			{
				write_headers.Add("Transfer-Encoding", "chunked");
			}
			else if (this.transfer_encoding != null)
			{
				write_headers.Add("Transfer-Encoding", this.transfer_encoding);
			}
			if (this.redirect_location != null)
			{
				write_headers.Add("Location", this.redirect_location);
			}
			string versionHeader = this.VersionHeader;
			if (versionHeader != null)
			{
				write_headers.Add("X-AspNet-Version", versionHeader);
			}
			if (this.content_length >= 0L)
			{
				write_headers.Add(HttpWorkerRequest.GetKnownResponseHeaderName(11), this.content_length.ToString(Helpers.InvariantCulture));
			}
			else if (this.BufferOutput)
			{
				if (final_flush)
				{
					this.content_length = this.output_stream.total;
					write_headers.Add(HttpWorkerRequest.GetKnownResponseHeaderName(11), this.content_length.ToString(Helpers.InvariantCulture));
				}
				else if (this.use_chunked)
				{
					write_headers.Add(HttpWorkerRequest.GetKnownResponseHeaderName(1), "close");
				}
			}
			else if (this.use_chunked)
			{
				write_headers.Add(HttpWorkerRequest.GetKnownResponseHeaderName(1), "close");
			}
			if (this.cache_policy != null)
			{
				this.cache_policy.SetHeaders(this, this.headers);
			}
			else
			{
				write_headers.Add("Cache-Control", this.CacheControl);
			}
			if (this.content_type != null)
			{
				string text = this.content_type;
				if ((this.charset_set || text == "text/plain" || text == "text/html") && text.IndexOf("charset=") == -1 && !string.IsNullOrEmpty(this.charset))
				{
					text = text + "; charset=" + this.charset;
				}
				write_headers.Add("Content-Type", text);
			}
			if (this.cookies != null && this.cookies.Count != 0)
			{
				int count = this.cookies.Count;
				for (int i = 0; i < count; i++)
				{
					write_headers.Add("Set-Cookie", this.cookies.Get(i).GetCookieHeaderValue());
				}
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00015298 File Offset: 0x00013498
		internal void WriteHeaders(bool final_flush)
		{
			if (this.headers_sent)
			{
				return;
			}
			if (this.context != null)
			{
				HttpApplication applicationInstance = this.context.ApplicationInstance;
				if (applicationInstance != null)
				{
					applicationInstance.TriggerPreSendRequestHeaders();
				}
			}
			this.headers_sent = true;
			if (this.cached_response != null)
			{
				this.cached_response.SetHeaders(this.headers);
			}
			NameValueCollection nameValueCollection;
			if (this.cached_headers != null)
			{
				nameValueCollection = this.cached_headers;
			}
			else
			{
				nameValueCollection = this.Headers;
				this.AddHeadersNoCache(nameValueCollection, final_flush);
			}
			if (this.WorkerRequest != null)
			{
				this.WorkerRequest.SendStatus(this.status_code, this.StatusDescription);
			}
			if (this.WorkerRequest != null)
			{
				for (int i = 0; i < nameValueCollection.Count; i++)
				{
					string key = nameValueCollection.GetKey(i);
					int knownResponseHeaderIndex = HttpWorkerRequest.GetKnownResponseHeaderIndex(key);
					string[] values = nameValueCollection.GetValues(i);
					if (values != null)
					{
						foreach (string text in values)
						{
							if (knownResponseHeaderIndex > -1)
							{
								this.WorkerRequest.SendKnownResponseHeader(knownResponseHeaderIndex, text);
							}
							else
							{
								this.WorkerRequest.SendUnknownResponseHeader(key, text);
							}
						}
					}
				}
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000153A5 File Offset: 0x000135A5
		internal void DoFilter(bool close)
		{
			if (this.output_stream.HaveFilter && this.context != null && this.context.Error == null)
			{
				this.output_stream.ApplyFilter(close);
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x000153D8 File Offset: 0x000135D8
		internal void Flush(bool final_flush)
		{
			if (this.completed)
			{
				throw new HttpException("Server cannot flush a completed response");
			}
			this.DoFilter(final_flush);
			if (!this.headers_sent && (final_flush || this.status_code != 200))
			{
				this.use_chunked = false;
			}
			bool flag = this.context != null && this.context.Request.HttpMethod == "HEAD";
			if (this.suppress_content || flag)
			{
				if (!this.headers_sent)
				{
					this.WriteHeaders(true);
				}
				this.output_stream.Clear();
				if (this.WorkerRequest != null)
				{
					this.output_stream.Flush(this.WorkerRequest, true);
				}
				this.completed = true;
				return;
			}
			this.completed = final_flush;
			if (!this.headers_sent)
			{
				this.WriteHeaders(final_flush);
			}
			if (this.context != null)
			{
				HttpApplication applicationInstance = this.context.ApplicationInstance;
				if (applicationInstance != null)
				{
					applicationInstance.TriggerPreSendRequestContent();
				}
			}
			if (this.IsCached)
			{
				this.cached_response.SetData(this.output_stream.GetData());
			}
			if (this.WorkerRequest != null)
			{
				this.output_stream.Flush(this.WorkerRequest, final_flush);
			}
		}

		/// <summary>Sends all currently buffered output to the client.</summary>
		/// <exception cref="T:System.Web.HttpException">The cache is flushed after the response has been sent.</exception>
		// Token: 0x060008B6 RID: 2230 RVA: 0x000154F5 File Offset: 0x000136F5
		public void Flush()
		{
			this.Flush(false);
		}

		/// <summary>Appends a HTTP PICS-Label header to the output stream.</summary>
		/// <param name="value">The string to add to the PICS-Label header.</param>
		// Token: 0x060008B7 RID: 2231 RVA: 0x000154FE File Offset: 0x000136FE
		public void Pics(string value)
		{
			this.AppendHeader("PICS-Label", value);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001550C File Offset: 0x0001370C
		private void Redirect(string url, bool endResponse, int code)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			if (this.headers_sent)
			{
				throw new HttpException("Headers have already been sent");
			}
			if (url.IndexOf('\n') != -1)
			{
				throw new ArgumentException("Redirect URI cannot contain newline characters.", "url");
			}
			this.is_request_being_redirected = true;
			this.ClearHeaders();
			this.ClearContent();
			this.StatusCode = code;
			url = this.ApplyAppPathModifier(url);
			if (!StrUtils.StartsWith(url, "http:", true) && !StrUtils.StartsWith(url, "https:", true) && !StrUtils.StartsWith(url, "file:", true) && !StrUtils.StartsWith(url, "ftp:", true))
			{
				HttpRuntimeSection section = HttpRuntime.Section;
				if (section != null && section.UseFullyQualifiedRedirectUrl)
				{
					UriBuilder uriBuilder = new UriBuilder(this.context.Request.Url);
					int num = url.IndexOf('?');
					if (num == -1)
					{
						uriBuilder.Path = url;
						uriBuilder.Query = null;
					}
					else
					{
						uriBuilder.Path = url.Substring(0, num);
						uriBuilder.Query = url.Substring(num + 1);
					}
					uriBuilder.Fragment = null;
					uriBuilder.Password = null;
					uriBuilder.UserName = null;
					url = uriBuilder.Uri.ToString();
				}
			}
			this.redirect_location = url;
			this.Write("<html><head><title>Object moved</title></head><body>\r\n");
			this.Write("<h2>Object moved to <a href=\"" + url + "\">here</a></h2>\r\n");
			this.Write("</body><html>\r\n");
			if (endResponse)
			{
				this.End();
			}
			this.is_request_being_redirected = false;
		}

		/// <summary>Redirects a request to a new URL and specifies the new URL.</summary>
		/// <param name="url">The target location. </param>
		/// <exception cref="T:System.Web.HttpException">A redirection is attempted after the HTTP headers have been sent.</exception>
		// Token: 0x060008B9 RID: 2233 RVA: 0x00015682 File Offset: 0x00013882
		public void Redirect(string url)
		{
			this.Redirect(url, true);
		}

		/// <summary>Redirects a client to a new URL. Specifies the new URL and whether execution of the current page should terminate.</summary>
		/// <param name="url">The location of the target. </param>
		/// <param name="endResponse">Indicates whether execution of the current page should terminate. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="url" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="url" /> contains a newline character.</exception>
		/// <exception cref="T:System.Web.HttpException">A redirection is attempted after the HTTP headers have been sent.</exception>
		/// <exception cref="T:System.ApplicationException">The page request is the result of a callback.</exception>
		// Token: 0x060008BA RID: 2234 RVA: 0x0001568C File Offset: 0x0001388C
		public void Redirect(string url, bool endResponse)
		{
			this.Redirect(url, endResponse, 302);
		}

		/// <summary>Performs a permanent redirection from the requested URL to the specified URL.</summary>
		/// <param name="url">The location to redirect the request to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="url" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="url" /> includes a newline character (\n).</exception>
		// Token: 0x060008BB RID: 2235 RVA: 0x0001569B File Offset: 0x0001389B
		public void RedirectPermanent(string url)
		{
			this.RedirectPermanent(url, true);
		}

		/// <summary>Performs a permanent redirection from the requested URL to the specified URL, and provides the option to complete the response. </summary>
		/// <param name="url">The location to redirect the request to. </param>
		/// <param name="endResponse">true to terminate the response; otherwise false. The default is false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="url" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="url" /> includes a newline character (\n).</exception>
		// Token: 0x060008BC RID: 2236 RVA: 0x000156A5 File Offset: 0x000138A5
		public void RedirectPermanent(string url, bool endResponse)
		{
			this.Redirect(url, endResponse, 301);
		}

		/// <summary>Redirects a request to a new URL by using route parameter values.</summary>
		/// <param name="routeValues">The route parameter values.</param>
		/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
		/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
		// Token: 0x060008BD RID: 2237 RVA: 0x000156B4 File Offset: 0x000138B4
		public void RedirectToRoute(object routeValues)
		{
			this.RedirectToRoute("RedirectToRoute", null, new RouteValueDictionary(routeValues), 302, true);
		}

		/// <summary>Redirects a request to a new URL by using route parameter values.</summary>
		/// <param name="routeValues">The route parameter values.</param>
		/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
		/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
		// Token: 0x060008BE RID: 2238 RVA: 0x000156CE File Offset: 0x000138CE
		public void RedirectToRoute(RouteValueDictionary routeValues)
		{
			this.RedirectToRoute("RedirectToRoute", null, routeValues, 302, true);
		}

		/// <summary>Redirects a request to a new URL by using a route name.</summary>
		/// <param name="routeName">The name of the route.</param>
		/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
		/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
		// Token: 0x060008BF RID: 2239 RVA: 0x000156E3 File Offset: 0x000138E3
		public void RedirectToRoute(string routeName)
		{
			this.RedirectToRoute("RedirectToRoute", routeName, null, 302, true);
		}

		/// <summary>Redirects a request to a new URL by using route parameter values and a route name.</summary>
		/// <param name="routeName">The name of the route.</param>
		/// <param name="routeValues">The route parameter values.</param>
		/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
		/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
		// Token: 0x060008C0 RID: 2240 RVA: 0x000156F8 File Offset: 0x000138F8
		public void RedirectToRoute(string routeName, object routeValues)
		{
			this.RedirectToRoute("RedirectToRoute", routeName, new RouteValueDictionary(routeValues), 302, true);
		}

		/// <summary>Redirects a request to a new URL by using route parameter values and a route name.</summary>
		/// <param name="routeName">The name of the route.</param>
		/// <param name="routeValues">The route parameter values.</param>
		/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
		/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
		// Token: 0x060008C1 RID: 2241 RVA: 0x00015712 File Offset: 0x00013912
		public void RedirectToRoute(string routeName, RouteValueDictionary routeValues)
		{
			this.RedirectToRoute("RedirectToRoute", routeName, routeValues, 302, true);
		}

		/// <summary>Performs a permanent redirection from a requested URL to a new URL by using route parameter values.</summary>
		/// <param name="routeValues">The route parameter values.</param>
		/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
		/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
		// Token: 0x060008C2 RID: 2242 RVA: 0x00015727 File Offset: 0x00013927
		public void RedirectToRoutePermanent(object routeValues)
		{
			this.RedirectToRoute("RedirectToRoutePermanent", null, new RouteValueDictionary(routeValues), 301, false);
		}

		/// <summary>Performs a permanent redirection from a requested URL to a new URL by using route parameter values.</summary>
		/// <param name="routeValues">The route parameter values.</param>
		/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
		/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
		// Token: 0x060008C3 RID: 2243 RVA: 0x00015741 File Offset: 0x00013941
		public void RedirectToRoutePermanent(RouteValueDictionary routeValues)
		{
			this.RedirectToRoute("RedirectToRoutePermanent", null, routeValues, 301, false);
		}

		/// <summary>Performs a permanent redirection from a requested URL to a new URL by using a route name.</summary>
		/// <param name="routeName">The name of the route.</param>
		/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
		/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
		// Token: 0x060008C4 RID: 2244 RVA: 0x00015756 File Offset: 0x00013956
		public void RedirectToRoutePermanent(string routeName)
		{
			this.RedirectToRoute("RedirectToRoutePermanent", routeName, null, 301, false);
		}

		/// <summary>Performs a permanent redirection from a requested URL to a new URL by using the route parameter values and the name of the route that correspond to the new URL.</summary>
		/// <param name="routeName">The name of the route.</param>
		/// <param name="routeValues">The route parameter values.</param>
		/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
		/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
		// Token: 0x060008C5 RID: 2245 RVA: 0x0001576B File Offset: 0x0001396B
		public void RedirectToRoutePermanent(string routeName, object routeValues)
		{
			this.RedirectToRoute("RedirectToRoutePermanent", routeName, new RouteValueDictionary(routeValues), 301, false);
		}

		/// <summary>Performs a permanent redirection from a requested URL to a new URL by using route parameter values and a route name.</summary>
		/// <param name="routeName">The name of the route.</param>
		/// <param name="routeValues">The route parameter values.</param>
		/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
		/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
		// Token: 0x060008C6 RID: 2246 RVA: 0x00015785 File Offset: 0x00013985
		public void RedirectToRoutePermanent(string routeName, RouteValueDictionary routeValues)
		{
			this.RedirectToRoute("RedirectToRoutePermanent", routeName, routeValues, 301, false);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001579C File Offset: 0x0001399C
		private void RedirectToRoute(string callerName, string routeName, RouteValueDictionary routeValues, int redirectCode, bool endResponse)
		{
			HttpContext httpContext = this.context ?? HttpContext.Current;
			HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
			if (httpRequest == null)
			{
				throw new NullReferenceException();
			}
			VirtualPathData virtualPath = RouteTable.Routes.GetVirtualPath(httpRequest.RequestContext, routeName, routeValues);
			string text = ((virtualPath != null) ? virtualPath.VirtualPath : null);
			if (string.IsNullOrEmpty(text))
			{
				throw new InvalidOperationException("No matching route found for RedirectToRoute");
			}
			this.Redirect(text, true, redirectCode);
		}

		/// <summary>Uses the specified output-cache provider to remove all output-cache items that are associated with the specified path. </summary>
		/// <param name="path">The virtual absolute path of the items that are removed from the cache. </param>
		/// <param name="providerName">The provider that is used to remove the output-cache artifacts that are associated with the specified path.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an invalid path.</exception>
		// Token: 0x060008C8 RID: 2248 RVA: 0x0001580C File Offset: 0x00013A0C
		public static void RemoveOutputCacheItem(string path, string providerName)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length > 0 && path[0] != '/')
			{
				throw new ArgumentException("Invalid path for HttpResponse.RemoveOutputCacheItem: '" + path + "'. An absolute virtual path is expected");
			}
			OutputCache.RemoveFromProvider(path, providerName);
		}

		/// <summary>Removes from the cache all cached items that are associated with the default output-cache provider. This method is static.</summary>
		/// <param name="path">The virtual absolute path to the items that are removed from the cache.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is not an absolute virtual path.</exception>
		// Token: 0x060008C9 RID: 2249 RVA: 0x00015858 File Offset: 0x00013A58
		public static void RemoveOutputCacheItem(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				return;
			}
			if (path[0] != '/')
			{
				throw new ArgumentException("'" + path + "' is not an absolute virtual path.");
			}
			HttpResponse.RemoveOutputCacheItem(path, OutputCache.DefaultProviderName);
		}

		/// <summary>Updates an existing cookie in the cookie collection.</summary>
		/// <param name="cookie">The cookie in the collection to be updated.</param>
		/// <exception cref="T:System.Web.HttpException">The cookie is set after the HTTP headers have been sent.</exception>
		/// <exception cref="T:System.Web.HttpException">Attempted to set the cookie after the HTTP headers were sent.</exception>
		/// <exception cref="T:System.Web.HttpException">The cookie is set after the HTTP headers have been sent.</exception>
		/// <exception cref="T:System.Web.HttpException">Attempted to set the cookie after the HTTP headers were sent.</exception>
		// Token: 0x060008CA RID: 2250 RVA: 0x000158A8 File Offset: 0x00013AA8
		public void SetCookie(HttpCookie cookie)
		{
			this.AppendCookie(cookie);
		}

		/// <summary>Writes a character to an HTTP response output stream.</summary>
		/// <param name="ch">The character to write to the HTTP output stream.</param>
		// Token: 0x060008CB RID: 2251 RVA: 0x000158B1 File Offset: 0x00013AB1
		public void Write(char ch)
		{
			TextWriter output = this.Output;
			if (output == null)
			{
				throw new NullReferenceException(".NET 4.0 emulation. A null value was found where an object was required.");
			}
			output.Write(ch);
		}

		/// <summary>Writes an <see cref="T:System.Object" /> to an HTTP response stream.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to write to the HTTP output stream.</param>
		// Token: 0x060008CC RID: 2252 RVA: 0x000158D0 File Offset: 0x00013AD0
		public void Write(object obj)
		{
			TextWriter output = this.Output;
			if (output == null)
			{
				throw new NullReferenceException(".NET 4.0 emulation. A null value was found where an object was required.");
			}
			if (obj == null)
			{
				return;
			}
			output.Write(obj.ToString());
		}

		/// <summary>Writes a string to an HTTP response output stream.</summary>
		/// <param name="s">The string to write to the HTTP output stream.</param>
		// Token: 0x060008CD RID: 2253 RVA: 0x00015902 File Offset: 0x00013B02
		public void Write(string s)
		{
			TextWriter output = this.Output;
			if (output == null)
			{
				throw new NullReferenceException(".NET 4.0 emulation. A null value was found where an object was required.");
			}
			output.Write(s);
		}

		/// <summary>Writes an array of characters to an HTTP response output stream.</summary>
		/// <param name="buffer">The character array to write.</param>
		/// <param name="index">The position in the character array where writing starts.</param>
		/// <param name="count">The number of characters to write, beginning at <paramref name="index" />.</param>
		// Token: 0x060008CE RID: 2254 RVA: 0x0001591E File Offset: 0x00013B1E
		public void Write(char[] buffer, int index, int count)
		{
			TextWriter output = this.Output;
			if (output == null)
			{
				throw new NullReferenceException(".NET 4.0 emulation. A null value was found where an object was required.");
			}
			output.Write(buffer, index, count);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001593C File Offset: 0x00013B3C
		private bool IsFileSystemDirSeparator(char ch)
		{
			return ch == '\\' || ch == '/';
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001594C File Offset: 0x00013B4C
		private string GetNormalizedFileName(string fn)
		{
			if (string.IsNullOrEmpty(fn))
			{
				return fn;
			}
			int length = fn.Length;
			if (length >= 3 && fn[1] == ':' && this.IsFileSystemDirSeparator(fn[2]))
			{
				return Path.GetFullPath(fn);
			}
			bool flag = this.IsFileSystemDirSeparator(fn[0]);
			if (length >= 2 && flag && this.IsFileSystemDirSeparator(fn[1]))
			{
				return Path.GetFullPath(fn);
			}
			if (!flag)
			{
				HttpContext httpContext = this.context ?? HttpContext.Current;
				HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
				if (httpRequest != null)
				{
					return httpRequest.MapPath(fn);
				}
			}
			return fn;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x000159EC File Offset: 0x00013BEC
		internal void WriteFile(FileStream fs, long offset, long size)
		{
			byte[] array = new byte[32768];
			if (offset != 0L)
			{
				fs.Position = offset;
			}
			long num = size;
			int num2;
			while (num > 0L && (num2 = fs.Read(array, 0, (int)Math.Min(num, 32768L))) != 0)
			{
				num -= (long)num2;
				this.output_stream.Write(array, 0, num2);
			}
		}

		/// <summary>Writes the contents of the specified file directly to an HTTP response output stream as a file block.</summary>
		/// <param name="filename">The name of the file to write to the HTTP output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filename" /> parameter is null.</exception>
		// Token: 0x060008D2 RID: 2258 RVA: 0x00015A43 File Offset: 0x00013C43
		public void WriteFile(string filename)
		{
			this.WriteFile(filename, false);
		}

		/// <summary>Writes the contents of the specified file directly to an HTTP response output stream as a memory block.</summary>
		/// <param name="filename">The name of the file to write into a memory block.</param>
		/// <param name="readIntoMemory">Indicates whether the file will be written into a memory block.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filename" /> parameter is null.</exception>
		// Token: 0x060008D3 RID: 2259 RVA: 0x00015A50 File Offset: 0x00013C50
		public void WriteFile(string filename, bool readIntoMemory)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			string normalizedFileName = this.GetNormalizedFileName(filename);
			if (readIntoMemory)
			{
				using (FileStream fileStream = File.OpenRead(normalizedFileName))
				{
					this.WriteFile(fileStream, 0L, fileStream.Length);
					goto IL_0056;
				}
			}
			FileInfo fileInfo = new FileInfo(normalizedFileName);
			this.output_stream.WriteFile(normalizedFileName, 0L, fileInfo.Length);
			IL_0056:
			if (this.buffer)
			{
				return;
			}
			this.output_stream.ApplyFilter(false);
			this.Flush();
		}

		/// <summary>Writes the specified file directly to an HTTP response output stream.</summary>
		/// <param name="fileHandle">The file handle of the file to write to the HTTP output stream.</param>
		/// <param name="offset">The byte position in the file where writing will start.</param>
		/// <param name="size">The number of bytes to write to the output stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileHandler" /> is null.</exception>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="offset" /> is less than 0.- or -<paramref name="size" /> is greater than the file size minus <paramref name="offset" />.</exception>
		// Token: 0x060008D4 RID: 2260 RVA: 0x00015AE0 File Offset: 0x00013CE0
		public void WriteFile(IntPtr fileHandle, long offset, long size)
		{
			if (offset < 0L)
			{
				throw new ArgumentNullException("offset can not be negative");
			}
			if (size < 0L)
			{
				throw new ArgumentNullException("size can not be negative");
			}
			if (size == 0L)
			{
				return;
			}
			using (FileStream fileStream = new FileStream(fileHandle, FileAccess.Read))
			{
				this.WriteFile(fileStream, offset, size);
			}
			if (this.buffer)
			{
				return;
			}
			this.output_stream.ApplyFilter(false);
			this.Flush();
		}

		/// <summary>Writes the specified file directly to an HTTP response output stream.</summary>
		/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
		/// <param name="offset">The byte position in the file where writing will start.</param>
		/// <param name="size">The number of bytes to write to the output stream.</param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="offset" /> is less than 0.- or -<paramref name="size" /> is greater than the file size minus <paramref name="offset" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filename" /> parameter is null.</exception>
		// Token: 0x060008D5 RID: 2261 RVA: 0x00015B5C File Offset: 0x00013D5C
		public void WriteFile(string filename, long offset, long size)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			if (offset < 0L)
			{
				throw new ArgumentNullException("offset can not be negative");
			}
			if (size < 0L)
			{
				throw new ArgumentNullException("size can not be negative");
			}
			if (size == 0L)
			{
				return;
			}
			FileStream fileStream = File.OpenRead(filename);
			this.WriteFile(fileStream, offset, size);
			if (this.buffer)
			{
				return;
			}
			this.output_stream.ApplyFilter(false);
			this.Flush();
		}

		/// <summary>Allows insertion of response substitution blocks into the response, which allows dynamic generation of specified response regions for output cached responses.</summary>
		/// <param name="callback">The method, user control, or object to substitute.</param>
		/// <exception cref="T:System.ArgumentException">The target of the <paramref name="callback" /> parameter is of type <see cref="T:System.Web.UI.Control" />.</exception>
		// Token: 0x060008D6 RID: 2262 RVA: 0x00015BC8 File Offset: 0x00013DC8
		public void WriteSubstitution(HttpResponseSubstitutionCallback callback)
		{
			if (callback == null)
			{
				throw new NullReferenceException();
			}
			object target = callback.Target;
			if (target != null && target.GetType() == typeof(Control))
			{
				throw new ArgumentException("callback");
			}
			string text = callback(this.context);
			if (!this.IsCached)
			{
				this.Write(text);
				return;
			}
			this.Cache.Cacheability = HttpCacheability.Server;
			this.Flush();
			if (this.WorkerRequest == null)
			{
				this.Write(text);
			}
			else
			{
				byte[] bytes = WebEncoding.ResponseEncoding.GetBytes(text);
				this.WorkerRequest.SendResponseFromMemory(bytes, bytes.Length);
			}
			this.cached_response.SetData(callback);
		}

		/// <summary>Writes the specified file directly to an HTTP response output stream, without buffering it in memory.</summary>
		/// <param name="filename">The name of the file to write to the HTTP output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filename" /> parameter is null</exception>
		// Token: 0x060008D7 RID: 2263 RVA: 0x00015C71 File Offset: 0x00013E71
		public void TransmitFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			this.TransmitFile(filename, false);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00015C8C File Offset: 0x00013E8C
		internal void TransmitFile(string filename, bool final_flush)
		{
			FileInfo fileInfo = new FileInfo(filename);
			using (fileInfo.OpenRead())
			{
			}
			this.output_stream.WriteFile(filename, 0L, fileInfo.Length);
			this.output_stream.ApplyFilter(final_flush);
			this.Flush(final_flush);
		}

		/// <summary>Writes the specified part of a file directly to an HTTP response output stream without buffering it in memory.</summary>
		/// <param name="filename">The name of the file to write to the HTTP output.</param>
		/// <param name="offset">The position in the file to begin to write to the HTTP output.</param>
		/// <param name="length">The number of bytes to be transmitted.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="offset" /> parameter is less than zero.- or -The <paramref name="length" /> parameter is less than -1.- or - The <paramref name="length" /> parameter specifies a number of bytes that is greater than the number of bytes the file contains minus the offset.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The out-of-process worker request is not supported.- or -The response is not using an <see cref="T:System.Web.HttpWriter" /> object.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="offset" /> parameter is less than zero or greater than the file size.- or -The <paramref name="length" /> parameter is less than -1 or greater than the value of the <paramref name="offset" /> parameter plus the file size.</exception>
		// Token: 0x060008D9 RID: 2265 RVA: 0x00015CEC File Offset: 0x00013EEC
		public void TransmitFile(string filename, long offset, long length)
		{
			this.output_stream.WriteFile(filename, offset, length);
			this.output_stream.ApplyFilter(false);
			this.Flush(false);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00015D0F File Offset: 0x00013F0F
		internal void TransmitFile(VirtualFile vf)
		{
			this.TransmitFile(vf, false);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00015D1C File Offset: 0x00013F1C
		internal void TransmitFile(VirtualFile vf, bool final_flush)
		{
			if (vf == null)
			{
				throw new ArgumentNullException("vf");
			}
			if (vf is DefaultVirtualFile)
			{
				this.TransmitFile(HostingEnvironment.MapPath(vf.VirtualPath), final_flush);
				return;
			}
			byte[] array = new byte[65535];
			using (Stream stream = vf.Open())
			{
				int num;
				while ((num = stream.Read(array, 0, 65535)) > 0)
				{
					this.output_stream.Write(array, 0, num);
					this.output_stream.ApplyFilter(final_flush);
					this.Flush(false);
				}
				if (final_flush)
				{
					this.Flush(true);
				}
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00015DC0 File Offset: 0x00013FC0
		internal void SetAppPathModifier(string app_modifier)
		{
			this.app_path_mod = app_modifier;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00015DC9 File Offset: 0x00013FC9
		internal void SetCachedHeaders(NameValueCollection headers)
		{
			this.cached_headers = headers;
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x00015DD2 File Offset: 0x00013FD2
		// (set) Token: 0x060008DF RID: 2271 RVA: 0x00015DDD File Offset: 0x00013FDD
		internal bool IsCached
		{
			get
			{
				return this.cached_response != null;
			}
			set
			{
				if (value)
				{
					this.cached_response = new CachedRawResponse(this.cache_policy);
					return;
				}
				this.cached_response = null;
			}
		}

		/// <summary>Gets the caching policy (such as expiration time, privacy settings, and vary clauses) of a Web page.</summary>
		/// <returns>An <see cref="T:System.Web.HttpCachePolicy" /> object that contains information about the caching policy of the current response.</returns>
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00015DFB File Offset: 0x00013FFB
		public HttpCachePolicy Cache
		{
			get
			{
				if (this.cache_policy == null)
				{
					this.cache_policy = new HttpCachePolicy();
				}
				return this.cache_policy;
			}
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00015E16 File Offset: 0x00014016
		internal CachedRawResponse GetCachedResponse()
		{
			if (this.cached_response != null)
			{
				this.cached_response.StatusCode = this.StatusCode;
				this.cached_response.StatusDescription = this.StatusDescription;
			}
			return this.cached_response;
		}

		/// <summary>Gets or sets the Cache-Control HTTP header that matches one of the <see cref="T:System.Web.HttpCacheability" /> enumeration values.</summary>
		/// <returns>A string representation of the <see cref="T:System.Web.HttpCacheability" /> enumeration value.</returns>
		/// <exception cref="T:System.ArgumentException">The string value set does not match one of the <see cref="T:System.Web.HttpCacheability" /> enumeration values.</exception>
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00015F04 File Offset: 0x00014104
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x00015E48 File Offset: 0x00014048
		public string CacheControl
		{
			get
			{
				if (this.user_cache_control == null)
				{
					return "private";
				}
				return this.user_cache_control;
			}
			set
			{
				if (value == null || value == "")
				{
					this.Cache.SetCacheability(HttpCacheability.NoCache);
					this.user_cache_control = null;
					return;
				}
				if (string.Compare(value, "public", true, Helpers.InvariantCulture) == 0)
				{
					this.Cache.SetCacheability(HttpCacheability.Public);
					this.user_cache_control = "public";
					return;
				}
				if (string.Compare(value, "private", true, Helpers.InvariantCulture) == 0)
				{
					this.Cache.SetCacheability(HttpCacheability.Private);
					this.user_cache_control = "private";
					return;
				}
				if (string.Compare(value, "no-cache", true, Helpers.InvariantCulture) == 0)
				{
					this.Cache.SetCacheability(HttpCacheability.NoCache);
					this.user_cache_control = "no-cache";
					return;
				}
				throw new ArgumentException("CacheControl property only allows `public', `private' or no-cache, for different uses, use Response.AppendHeader");
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00015F1A File Offset: 0x0001411A
		internal int GetOutputByteCount()
		{
			return this.output_stream.GetTotalLength();
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00015F27 File Offset: 0x00014127
		internal void ReleaseResources()
		{
			if (this.output_stream != null)
			{
				this.output_stream.ReleaseResources(true);
			}
			if (this.completed)
			{
				return;
			}
			this.Close();
			this.completed = true;
		}

		/// <summary>Gets a <see cref="T:System.Threading.CancellationToken" /> object that is tripped when the client disconnects.</summary>
		/// <returns>The cancellation token.</returns>
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00015F54 File Offset: 0x00014154
		public CancellationToken ClientDisconnectedToken
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(CancellationToken);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00015F70 File Offset: 0x00014170
		public bool HeadersWritten
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that indicates whether the connection supports asynchronous flush operations.</summary>
		/// <returns>true if the connection supports asynchronous flush operations; otherwise, false.</returns>
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x00015F8C File Offset: 0x0001418C
		public bool SupportsAsyncFlush
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00015FA8 File Offset: 0x000141A8
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool SuppressDefaultCacheControlHeader
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ISubscriptionToken AddOnSendingHeaders(Action<HttpContext> callback)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Sends the currently buffered response to the client.</summary>
		/// <returns>The asynchronous result object.</returns>
		/// <param name="callback">The callback object.</param>
		/// <param name="state">The response state.</param>
		/// <exception cref="T:System.Web.HttpException">The response is already completed.</exception>
		// Token: 0x060008EC RID: 2284 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public IAsyncResult BeginFlush(AsyncCallback callback, object state)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Disables IIS user-mode caching for this response.</summary>
		// Token: 0x060008ED RID: 2285 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void DisableUserCache()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Completes an asynchronous flush operation.</summary>
		/// <param name="asyncResult">The asynchronous result object.</param>
		/// <exception cref="T:System.ArgumentNullException">Asynchronous flush is not supported and the <paramref name="asyncResult" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">Asynchronous flush is not supported and the <paramref name="asyncResult" /> parameter cannot be cast to a FlushAsyncResult object.</exception>
		// Token: 0x060008EE RID: 2286 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void EndFlush(IAsyncResult asyncResult)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Task FlushAsync()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void PushPromise(string path)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void PushPromise(string path, string method, NameValueCollection headers)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000FC7 RID: 4039
		internal HttpWorkerRequest WorkerRequest;

		// Token: 0x04000FC8 RID: 4040
		internal HttpResponseStream output_stream;

		// Token: 0x04000FC9 RID: 4041
		internal bool buffer = true;

		// Token: 0x04000FCA RID: 4042
		private ArrayList fileDependencies;

		// Token: 0x04000FCB RID: 4043
		private HttpContext context;

		// Token: 0x04000FCC RID: 4044
		private TextWriter writer;

		// Token: 0x04000FCD RID: 4045
		private HttpCachePolicy cache_policy;

		// Token: 0x04000FCE RID: 4046
		private Encoding encoding;

		// Token: 0x04000FCF RID: 4047
		private HttpCookieCollection cookies;

		// Token: 0x04000FD0 RID: 4048
		private int status_code = 200;

		// Token: 0x04000FD1 RID: 4049
		private string status_description = "OK";

		// Token: 0x04000FD2 RID: 4050
		private string content_type = "text/html";

		// Token: 0x04000FD3 RID: 4051
		private string charset;

		// Token: 0x04000FD4 RID: 4052
		private bool charset_set;

		// Token: 0x04000FD5 RID: 4053
		private CachedRawResponse cached_response;

		// Token: 0x04000FD6 RID: 4054
		private string user_cache_control = "private";

		// Token: 0x04000FD7 RID: 4055
		private string redirect_location;

		// Token: 0x04000FD8 RID: 4056
		private string version_header;

		// Token: 0x04000FD9 RID: 4057
		private bool version_header_checked;

		// Token: 0x04000FDA RID: 4058
		private long content_length = -1L;

		// Token: 0x04000FDB RID: 4059
		private HttpHeaderCollection headers;

		// Token: 0x04000FDC RID: 4060
		private bool headers_sent;

		// Token: 0x04000FDD RID: 4061
		private NameValueCollection cached_headers;

		// Token: 0x04000FDE RID: 4062
		private string transfer_encoding;

		// Token: 0x04000FDF RID: 4063
		internal bool use_chunked;

		// Token: 0x04000FE0 RID: 4064
		private bool closed;

		// Token: 0x04000FE1 RID: 4065
		private bool completed;

		// Token: 0x04000FE2 RID: 4066
		internal bool suppress_content;

		// Token: 0x04000FE3 RID: 4067
		private string app_path_mod;

		// Token: 0x04000FE4 RID: 4068
		private bool is_request_being_redirected;

		// Token: 0x04000FE5 RID: 4069
		private Encoding headerEncoding;

		// Token: 0x04000FE9 RID: 4073
		private const int bufLen = 65535;
	}
}
