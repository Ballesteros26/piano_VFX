using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Configuration;
using System.Web.Routing;
using System.Web.Util;
using Unity;

namespace System.Web
{
	/// <summary>Enables ASP.NET to read the HTTP values sent by a client during a Web request. </summary>
	// Token: 0x020000A0 RID: 160
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpRequest
	{
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x000118FB File Offset: 0x0000FAFB
		internal static bool ValidateRequestNewMode
		{
			get
			{
				return HttpRequest.validateRequestNewMode;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00011902 File Offset: 0x0000FB02
		internal bool InputValidationEnabled
		{
			get
			{
				return this.inputValidationEnabled;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0001190A File Offset: 0x0000FB0A
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x00011911 File Offset: 0x0000FB11
		private static char[] RequestPathInvalidCharacters { get; set; }

		// Token: 0x06000798 RID: 1944 RVA: 0x0001191C File Offset: 0x0000FB1C
		private static char[] CharsFromList(string list)
		{
			string[] array = list.Split(new char[] { ',' });
			char[] array2 = new char[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array[i].Trim();
				if (text.Length != 1)
				{
					throw new ConfigurationErrorsException();
				}
				array2[i] = text[0];
			}
			return array2;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00011978 File Offset: 0x0000FB78
		static HttpRequest()
		{
			try
			{
				UrlMappingsSection urlMappingsSection = WebConfigurationManager.GetWebApplicationSection("system.web/urlMappings") as UrlMappingsSection;
				if (urlMappingsSection != null && urlMappingsSection.IsEnabled)
				{
					HttpRequest.urlMappings = urlMappingsSection.UrlMappings;
					if (HttpRequest.urlMappings.Count == 0)
					{
						HttpRequest.urlMappings = null;
					}
				}
				if (HttpRuntime.Section.RequestValidationMode >= new Version(4, 0))
				{
					HttpRequest.validateRequestNewMode = true;
					string requestPathInvalidCharacters = HttpRuntime.Section.RequestPathInvalidCharacters;
					if (!string.IsNullOrEmpty(requestPathInvalidCharacters))
					{
						HttpRequest.RequestPathInvalidCharacters = HttpRequest.CharsFromList(requestPathInvalidCharacters);
					}
				}
			}
			catch
			{
			}
			HttpRequest.host_addresses = HttpRequest.GetLocalHostAddresses();
		}

		/// <summary>Initializes an <see cref="T:System.Web.HttpRequest" /> object.</summary>
		/// <param name="filename">The name of the file associated with the request. </param>
		/// <param name="url">Information regarding the URL of the current request. </param>
		/// <param name="queryString">The entire query string sent with the request (everything after the'?'). </param>
		// Token: 0x0600079A RID: 1946 RVA: 0x00011A28 File Offset: 0x0000FC28
		public HttpRequest(string filename, string url, string queryString)
		{
			this.orig_url = url;
			this.url_components = new UriBuilder(url);
			this.url_components.Query = queryString;
			this.query_string_nvc = new WebROCollection();
			if (queryString != null)
			{
				HttpUtility.ParseQueryString(queryString, Encoding.Default, this.query_string_nvc);
			}
			this.query_string_nvc.Protect();
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00011A8B File Offset: 0x0000FC8B
		internal HttpRequest(HttpWorkerRequest worker_request, HttpContext context)
		{
			this.worker_request = worker_request;
			this.context = context;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		internal UriBuilder UrlComponents
		{
			get
			{
				if (this.url_components == null)
				{
					byte[] queryStringRawBytes = this.worker_request.GetQueryStringRawBytes();
					string text;
					if (queryStringRawBytes != null)
					{
						text = this.ContentEncoding.GetString(queryStringRawBytes);
					}
					else
					{
						text = this.worker_request.GetQueryString();
					}
					this.BuildUrlComponents(this.ApplyUrlMapping(this.worker_request.GetUriPath()), text);
				}
				return this.url_components;
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00011B08 File Offset: 0x0000FD08
		private void BuildUrlComponents(string path, string query)
		{
			if (this.url_components != null)
			{
				return;
			}
			this.url_components = new UriBuilder();
			this.url_components.Scheme = this.worker_request.GetProtocol();
			this.url_components.Host = this.worker_request.GetServerName();
			this.url_components.Port = this.worker_request.GetLocalPort();
			this.url_components.Path = path;
			if (query != null && query.Length > 0)
			{
				this.url_components.Query = query.TrimStart(HttpRequest.queryTrimChars);
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00011B9C File Offset: 0x0000FD9C
		internal string ApplyUrlMapping(string url)
		{
			if (HttpRequest.urlMappings == null)
			{
				return url;
			}
			string text = VirtualPathUtility.ToAppRelative(url);
			UrlMapping urlMapping = null;
			foreach (object obj in HttpRequest.urlMappings)
			{
				UrlMapping urlMapping2 = (UrlMapping)obj;
				if (urlMapping2 != null && string.Compare(text, urlMapping2.Url, StringComparison.Ordinal) == 0)
				{
					urlMapping = urlMapping2;
					break;
				}
			}
			if (urlMapping == null)
			{
				return url;
			}
			string text2 = VirtualPathUtility.ToAbsolute(urlMapping.MappedUrl.Trim());
			Uri uri = new Uri("http://host.com" + text2);
			if (this.url_components != null)
			{
				this.url_components.Path = uri.AbsolutePath;
				this.url_components.Query = uri.Query.TrimStart(HttpRequest.queryTrimChars);
				this.query_string_nvc = new WebROCollection();
				HttpUtility.ParseQueryString(uri.Query, Encoding.Default, this.query_string_nvc);
				this.query_string_nvc.Protect();
			}
			else
			{
				this.BuildUrlComponents(uri.AbsolutePath, uri.Query);
			}
			return this.url_components.Path;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00011CC8 File Offset: 0x0000FEC8
		private string[] SplitHeader(int header_index)
		{
			string[] array = null;
			string knownRequestHeader = this.worker_request.GetKnownRequestHeader(header_index);
			if (knownRequestHeader != null && knownRequestHeader != "" && knownRequestHeader.Trim() != "")
			{
				array = knownRequestHeader.Split(new char[] { ',' });
				for (int i = array.Length - 1; i >= 0; i--)
				{
					array[i] = array[i].Trim();
				}
			}
			return array;
		}

		/// <summary>Gets a string array of client-supported MIME accept types.</summary>
		/// <returns>A string array of client-supported MIME accept types.</returns>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00011D34 File Offset: 0x0000FF34
		public string[] AcceptTypes
		{
			get
			{
				if (this.worker_request == null)
				{
					return null;
				}
				if (this.accept_types == null)
				{
					this.accept_types = this.SplitHeader(20);
				}
				return this.accept_types;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Principal.WindowsIdentity" /> type for the current user.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.WindowsIdentity" /> object for the current Microsoft Internet Information Services (IIS) authentication settings.</returns>
		/// <exception cref="T:System.InvalidOperationException">The Web application is running in IIS 7 integrated mode and the <see cref="E:System.Web.HttpApplication.PostAuthenticateRequest" /> event has not yet been raised.</exception>
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00003A1F File Offset: 0x00001C1F
		public WindowsIdentity LogonUserIdentity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the anonymous identifier for the user, if present.</summary>
		/// <returns>A string representing the current anonymous user identifier.</returns>
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00011D5C File Offset: 0x0000FF5C
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x00011D64 File Offset: 0x0000FF64
		public string AnonymousID
		{
			get
			{
				return this.anonymous_id;
			}
			internal set
			{
				this.anonymous_id = value;
			}
		}

		/// <summary>Gets the ASP.NET application's virtual application root path on the server.</summary>
		/// <returns>The virtual path of the current application.</returns>
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00011D6D File Offset: 0x0000FF6D
		public string ApplicationPath
		{
			get
			{
				if (this.worker_request == null)
				{
					return null;
				}
				return this.worker_request.GetAppPath();
			}
		}

		/// <summary>Gets or sets information about the requesting client's browser capabilities.</summary>
		/// <returns>An <see cref="T:System.Web.HttpBrowserCapabilities" /> object listing the capabilities of the client's browser.</returns>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00011D84 File Offset: 0x0000FF84
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x00011DA5 File Offset: 0x0000FFA5
		public HttpBrowserCapabilities Browser
		{
			get
			{
				if (this.browser_capabilities == null)
				{
					this.browser_capabilities = HttpCapabilitiesBase.BrowserCapabilitiesProvider.GetBrowserCapabilities(this);
				}
				return this.browser_capabilities;
			}
			set
			{
				this.browser_capabilities = value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00011DAE File Offset: 0x0000FFAE
		internal bool BrowserMightHaveSpecialWriter
		{
			get
			{
				return this.browser_capabilities != null || HttpApplicationFactory.AppBrowsersFiles.Length != 0;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00011DAE File Offset: 0x0000FFAE
		internal bool BrowserMightHaveAdapters
		{
			get
			{
				return this.browser_capabilities != null || HttpApplicationFactory.AppBrowsersFiles.Length != 0;
			}
		}

		/// <summary>Gets the current request's client security certificate.</summary>
		/// <returns>An <see cref="T:System.Web.HttpClientCertificate" /> object containing information about the client's security certificate settings.</returns>
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00011DC3 File Offset: 0x0000FFC3
		public HttpClientCertificate ClientCertificate
		{
			get
			{
				if (this.client_cert == null)
				{
					this.client_cert = new HttpClientCertificate(this.worker_request);
				}
				return this.client_cert;
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00011DE4 File Offset: 0x0000FFE4
		internal static string GetParameter(string header, string attr)
		{
			int num = header.IndexOf(attr);
			if (num == -1)
			{
				return null;
			}
			num += attr.Length;
			if (num >= header.Length)
			{
				return null;
			}
			char c = header[num];
			if (c != '"')
			{
				c = ' ';
			}
			int num2 = header.IndexOf(c, num + 1);
			if (num2 != -1)
			{
				return header.Substring(num + 1, num2 - num - 1);
			}
			if (c != '"')
			{
				return header.Substring(num);
			}
			return null;
		}

		/// <summary>Gets or sets the character set of the entity-body.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object representing the client's character set.</returns>
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00011E50 File Offset: 0x00010050
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x00011ED4 File Offset: 0x000100D4
		public Encoding ContentEncoding
		{
			get
			{
				if (this.encoding == null)
				{
					if (this.worker_request == null)
					{
						throw HttpException.NewWithCode("No HttpWorkerRequest", 3001);
					}
					string parameter = HttpRequest.GetParameter(this.ContentType, "; charset=");
					if (parameter == null)
					{
						this.encoding = WebEncoding.RequestEncoding;
					}
					else
					{
						try
						{
							this.encoding = Encoding.GetEncoding(parameter);
						}
						catch
						{
							this.encoding = WebEncoding.RequestEncoding;
						}
					}
				}
				return this.encoding;
			}
			set
			{
				this.encoding = value;
			}
		}

		/// <summary>Specifies the length, in bytes, of content sent by the client.</summary>
		/// <returns>The length, in bytes, of content sent by the client.</returns>
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00011EE0 File Offset: 0x000100E0
		public int ContentLength
		{
			get
			{
				if (this.content_length == -1)
				{
					if (this.worker_request == null)
					{
						return 0;
					}
					string knownRequestHeader = this.worker_request.GetKnownRequestHeader(11);
					if (knownRequestHeader != null)
					{
						try
						{
							this.content_length = int.Parse(knownRequestHeader);
						}
						catch
						{
						}
					}
				}
				if (this.content_length < 0)
				{
					return 0;
				}
				return this.content_length;
			}
		}

		/// <summary>Gets or sets the MIME content type of the incoming request.</summary>
		/// <returns>A string representing the MIME content type of the incoming request, for example, "text/html". Additional common MIME types include "audio.wav", "image/gif", and "application/pdf".</returns>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00011F44 File Offset: 0x00010144
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x00011F82 File Offset: 0x00010182
		public string ContentType
		{
			get
			{
				if (this.content_type == null)
				{
					if (this.worker_request != null)
					{
						this.content_type = this.worker_request.GetKnownRequestHeader(12);
					}
					if (this.content_type == null)
					{
						this.content_type = string.Empty;
					}
				}
				return this.content_type;
			}
			set
			{
				this.content_type = value;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00011F8C File Offset: 0x0001018C
		internal HttpCookieCollection CookiesNoValidation
		{
			get
			{
				if (this.cookies_unvalidated == null)
				{
					if (this.worker_request == null)
					{
						this.cookies_unvalidated = new HttpCookieCollection();
					}
					else
					{
						string knownRequestHeader = this.worker_request.GetKnownRequestHeader(25);
						this.cookies_unvalidated = new HttpCookieCollection(knownRequestHeader);
					}
				}
				return this.cookies_unvalidated;
			}
		}

		/// <summary>Gets a collection of cookies sent by the client.</summary>
		/// <returns>An <see cref="T:System.Web.HttpCookieCollection" /> object representing the client's cookie variables.</returns>
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00011FD8 File Offset: 0x000101D8
		public HttpCookieCollection Cookies
		{
			get
			{
				if (this.cookies == null)
				{
					this.cookies = this.CookiesNoValidation;
				}
				if ((this.validate_cookies | HttpRequest.validateRequestNewMode) && !this.checked_cookies)
				{
					this.checked_cookies = true;
					HttpRequest.ValidateCookieCollection(this.cookies);
				}
				return this.cookies;
			}
		}

		/// <summary>Gets the virtual path of the current request.</summary>
		/// <returns>The virtual path of the current request.</returns>
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00012027 File Offset: 0x00010227
		public string CurrentExecutionFilePath
		{
			get
			{
				if (this.current_exe_path != null)
				{
					return this.current_exe_path;
				}
				return this.FilePath;
			}
		}

		/// <summary>Gets the extension of the file name that is specified in the <see cref="P:System.Web.HttpRequest.CurrentExecutionFilePath" /> property.</summary>
		/// <returns>The extension of the file name that is specified in the <see cref="P:System.Web.HttpRequest.CurrentExecutionFilePath" /> property.</returns>
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0001203E File Offset: 0x0001023E
		public string CurrentExecutionFilePathExtension
		{
			get
			{
				return global::System.IO.Path.GetExtension(this.CurrentExecutionFilePath);
			}
		}

		/// <summary>Gets the virtual path of the application root and makes it relative by using the tilde (~) notation for the application root (as in "~/page.aspx").</summary>
		/// <returns>The virtual path of the application root for the current request.</returns>
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0001204B File Offset: 0x0001024B
		public string AppRelativeCurrentExecutionFilePath
		{
			get
			{
				return VirtualPathUtility.ToAppRelative(this.CurrentExecutionFilePath);
			}
		}

		/// <summary>Gets the virtual path of the current request.</summary>
		/// <returns>The virtual path of the current request.</returns>
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00012058 File Offset: 0x00010258
		public string FilePath
		{
			get
			{
				if (this.worker_request == null)
				{
					return "/";
				}
				if (this.file_path == null)
				{
					this.file_path = UrlUtils.Canonic(this.ApplyUrlMapping(this.worker_request.GetFilePath()));
				}
				return this.file_path;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00012092 File Offset: 0x00010292
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x000120C7 File Offset: 0x000102C7
		internal string ClientFilePath
		{
			get
			{
				if (this.client_file_path != null)
				{
					return this.client_file_path;
				}
				if (this.worker_request == null)
				{
					return "/";
				}
				return UrlUtils.Canonic(this.ApplyUrlMapping(this.worker_request.GetFilePath()));
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.client_file_path = null;
					return;
				}
				this.client_file_path = value;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x000120E4 File Offset: 0x000102E4
		internal string BaseVirtualDir
		{
			get
			{
				if (this.base_virtual_dir == null)
				{
					this.base_virtual_dir = this.FilePath;
					if (UrlUtils.HasSessionId(this.base_virtual_dir))
					{
						this.base_virtual_dir = UrlUtils.RemoveSessionId(VirtualPathUtility.GetDirectory(this.base_virtual_dir), this.base_virtual_dir);
					}
					int num = this.base_virtual_dir.LastIndexOf('/');
					if (num != -1)
					{
						if (num == 0)
						{
							num = 1;
						}
						this.base_virtual_dir = this.base_virtual_dir.Substring(0, num);
					}
					else
					{
						this.base_virtual_dir = "/";
					}
				}
				return this.base_virtual_dir;
			}
		}

		/// <summary>Gets the collection of files uploaded by the client, in multipart MIME format.</summary>
		/// <returns>An <see cref="T:System.Web.HttpFileCollection" /> object representing a collection of files uploaded by the client. The items of the <see cref="T:System.Web.HttpFileCollection" /> object are of type <see cref="T:System.Web.HttpPostedFile" />.</returns>
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0001216C File Offset: 0x0001036C
		public HttpFileCollection Files
		{
			get
			{
				if (this.files == null)
				{
					this.files = new HttpFileCollection();
					if (this.worker_request != null && this.IsContentType("multipart/form-data", true))
					{
						this.form = new WebROCollection();
						this.LoadMultiPart();
						this.form.Protect();
					}
				}
				return this.files;
			}
		}

		/// <summary>Gets or sets the filter to use when reading the current input stream.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object to be used as the filter.</returns>
		/// <exception cref="T:System.Web.HttpException">The specified <see cref="T:System.IO.Stream" /> is invalid.</exception>
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x000121C4 File Offset: 0x000103C4
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x000121EE File Offset: 0x000103EE
		public Stream Filter
		{
			get
			{
				if (this.filter != null)
				{
					return this.filter;
				}
				if (this.input_filter == null)
				{
					this.input_filter = new InputFilterStream();
				}
				return this.input_filter;
			}
			set
			{
				if (this.input_filter == null)
				{
					throw new HttpException("Invalid filter");
				}
				this.filter = value;
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001220C File Offset: 0x0001040C
		private static Stream GetSubStream(Stream stream)
		{
			if (stream is IntPtrStream)
			{
				return new IntPtrStream(stream);
			}
			if (stream is MemoryStream)
			{
				MemoryStream memoryStream = (MemoryStream)stream;
				return new MemoryStream(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, false, true);
			}
			if (stream is TempFileStream)
			{
				((TempFileStream)stream).SavePosition();
				return stream;
			}
			throw new NotSupportedException("The stream is " + stream.GetType());
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00012277 File Offset: 0x00010477
		private static void EndSubStream(Stream stream)
		{
			if (stream is TempFileStream)
			{
				((TempFileStream)stream).RestorePosition();
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001228C File Offset: 0x0001048C
		private void LoadMultiPart()
		{
			string parameter = HttpRequest.GetParameter(this.ContentType, "; boundary=");
			if (parameter == null)
			{
				return;
			}
			Stream subStream = HttpRequest.GetSubStream(this.InputStream);
			HttpMultipart httpMultipart = new HttpMultipart(subStream, parameter, this.ContentEncoding);
			HttpMultipart.Element element;
			while ((element = httpMultipart.ReadNextElement()) != null)
			{
				if (element.Filename == null)
				{
					byte[] array = new byte[element.Length];
					subStream.Position = element.Start;
					subStream.Read(array, 0, (int)element.Length);
					this.form.Add(element.Name, this.ContentEncoding.GetString(array));
				}
				else
				{
					HttpPostedFile httpPostedFile = new HttpPostedFile(element.Filename, element.ContentType, subStream, element.Start, element.Length);
					this.files.AddFile(element.Name, httpPostedFile);
				}
			}
			HttpRequest.EndSubStream(subStream);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00012368 File Offset: 0x00010568
		private void AddRawKeyValue(StringBuilder key, StringBuilder value)
		{
			string text = HttpUtility.UrlDecode(key.ToString(), this.ContentEncoding);
			this.form.Add(text, HttpUtility.UrlDecode(value.ToString(), this.ContentEncoding));
			key.Length = 0;
			value.Length = 0;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x000123B4 File Offset: 0x000105B4
		private void LoadWwwForm()
		{
			using (Stream subStream = HttpRequest.GetSubStream(this.InputStream))
			{
				using (StreamReader streamReader = new StreamReader(subStream, this.ContentEncoding))
				{
					StringBuilder stringBuilder = new StringBuilder();
					StringBuilder stringBuilder2 = new StringBuilder();
					int num;
					while ((num = streamReader.Read()) != -1)
					{
						if (num == 61)
						{
							stringBuilder2.Length = 0;
							while ((num = streamReader.Read()) != -1)
							{
								if (num == 38)
								{
									this.AddRawKeyValue(stringBuilder, stringBuilder2);
									break;
								}
								stringBuilder2.Append((char)num);
							}
							if (num == -1)
							{
								this.AddRawKeyValue(stringBuilder, stringBuilder2);
								return;
							}
						}
						else if (num == 38)
						{
							this.AddRawKeyValue(stringBuilder, stringBuilder2);
						}
						else
						{
							stringBuilder.Append((char)num);
						}
					}
					if (num == -1)
					{
						this.AddRawKeyValue(stringBuilder, stringBuilder2);
					}
					HttpRequest.EndSubStream(subStream);
				}
			}
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00012498 File Offset: 0x00010698
		private bool IsContentType(string ct, bool starts_with)
		{
			if (starts_with)
			{
				return StrUtils.StartsWith(this.ContentType, ct, true);
			}
			return string.Compare(this.ContentType, ct, true, Helpers.InvariantCulture) == 0;
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x000124C0 File Offset: 0x000106C0
		internal WebROCollection FormUnvalidated
		{
			get
			{
				if (this.form == null)
				{
					this.form = new WebROCollection();
					this.files = new HttpFileCollection();
					if (this.IsContentType("multipart/form-data", true))
					{
						this.LoadMultiPart();
					}
					else if (this.IsContentType("application/x-www-form-urlencoded", true))
					{
						this.LoadWwwForm();
					}
					this.form.Protect();
				}
				return this.form;
			}
		}

		/// <summary>Gets a collection of form variables.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> representing a collection of form variables.</returns>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00012528 File Offset: 0x00010728
		public NameValueCollection Form
		{
			get
			{
				NameValueCollection formUnvalidated = this.FormUnvalidated;
				if (HttpRequest.validateRequestNewMode && !this.checked_form)
				{
					if (!this.lazyFormValidation)
					{
						this.checked_form = true;
						HttpRequest.ValidateNameValueCollection("Form", formUnvalidated, RequestValidationSource.Form);
					}
				}
				else if (this.validate_form && !this.checked_form)
				{
					this.checked_form = true;
					HttpRequest.ValidateNameValueCollection("Form", formUnvalidated);
				}
				return formUnvalidated;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0001258B File Offset: 0x0001078B
		internal NameValueCollection HeadersNoValidation
		{
			get
			{
				if (this.headers_unvalidated == null)
				{
					this.headers_unvalidated = new HeadersCollection(this);
				}
				return this.headers_unvalidated;
			}
		}

		/// <summary>Gets a collection of HTTP headers.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of headers.</returns>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x000125A8 File Offset: 0x000107A8
		public NameValueCollection Headers
		{
			get
			{
				if (this.headers == null)
				{
					this.headers = this.HeadersNoValidation;
					if (HttpRequest.validateRequestNewMode)
					{
						RequestValidator requestValidator = RequestValidator.Current;
						foreach (string text in this.headers.AllKeys)
						{
							string text2 = this.headers[text];
							int num;
							if (!requestValidator.IsValidRequestString(HttpContext.Current, text2, RequestValidationSource.Headers, text, out num))
							{
								HttpRequest.ThrowValidationException("Headers", text, text2);
							}
						}
					}
				}
				return this.headers;
			}
		}

		/// <summary>Gets the HTTP data transfer method (such as GET, POST, or HEAD) used by the client.</summary>
		/// <returns>The HTTP data transfer method used by the client.</returns>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0001262C File Offset: 0x0001082C
		public string HttpMethod
		{
			get
			{
				if (this.http_method == null)
				{
					if (this.worker_request != null)
					{
						this.http_method = this.worker_request.GetHttpVerbName();
					}
					else
					{
						this.http_method = "GET";
					}
				}
				return this.http_method;
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00012664 File Offset: 0x00010864
		private void DoFilter(byte[] buffer)
		{
			if (this.input_filter == null || this.filter == null)
			{
				return;
			}
			if (buffer.Length < 1024)
			{
				buffer = new byte[1024];
			}
			this.input_filter.BaseStream = this.input_stream;
			MemoryStream memoryStream = new MemoryStream();
			for (;;)
			{
				int num = this.filter.Read(buffer, 0, buffer.Length);
				if (num <= 0)
				{
					break;
				}
				memoryStream.Write(buffer, 0, num);
			}
			this.input_stream = new MemoryStream(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, false, true);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x000126EC File Offset: 0x000108EC
		private TempFileStream GetTempStream()
		{
			string dynamicBase = AppDomain.CurrentDomain.SetupInformation.DynamicBase;
			TempFileStream tempFileStream = null;
			Random random = new Random();
			do
			{
				int num = random.Next();
				string text = global::System.IO.Path.Combine(dynamicBase, "tmp" + (num + 1).ToString("x") + ".req");
				try
				{
					tempFileStream = new TempFileStream(text);
				}
				catch (SecurityException)
				{
					throw;
				}
				catch
				{
				}
			}
			while (tempFileStream == null);
			return tempFileStream;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00012770 File Offset: 0x00010970
		private void MakeInputStream()
		{
			if (this.input_stream != null)
			{
				return;
			}
			if (this.worker_request == null)
			{
				this.input_stream = new MemoryStream(new byte[0], 0, 0, false, true);
				this.DoFilter(new byte[1024]);
				return;
			}
			int contentLength = this.ContentLength;
			int num = contentLength / 1024;
			HttpRuntimeSection section = HttpRuntime.Section;
			if (num > section.MaxRequestLength)
			{
				throw HttpException.NewWithCode(400, "Upload size exceeds httpRuntime limit.", 3004);
			}
			int num2 = 0;
			byte[] array = this.worker_request.GetPreloadedEntityBody();
			if (this.content_length <= 0 || this.worker_request.IsEntireEntityBodyIsPreloaded())
			{
				if (array == null || contentLength == 0)
				{
					this.input_stream = new MemoryStream(new byte[0], 0, 0, false, true);
				}
				else
				{
					this.input_stream = new MemoryStream(array, 0, array.Length, false, true);
				}
				this.DoFilter(new byte[1024]);
				return;
			}
			if (array != null)
			{
				num2 = array.Length;
			}
			if (contentLength > 0 && num >= section.RequestLengthDiskThreshold)
			{
				num2 = Math.Min(contentLength, num2);
				this.request_file = this.GetTempStream();
				Stream stream = this.request_file;
				if (num2 > 0)
				{
					stream.Write(array, 0, num2);
				}
				if (num2 < contentLength)
				{
					array = new byte[Math.Min(contentLength, 32768)];
					do
					{
						int num3 = Math.Min(contentLength - num2, 32768);
						int num4 = this.worker_request.ReadEntityBody(array, num3);
						if (num4 <= 0)
						{
							break;
						}
						stream.Write(array, 0, num4);
						num2 += num4;
					}
					while (num2 < contentLength);
				}
				this.request_file.SetReadOnly();
				this.input_stream = this.request_file;
			}
			else if (contentLength > 0)
			{
				num2 = Math.Min(contentLength, num2);
				IntPtr intPtr = Marshal.AllocHGlobal(contentLength);
				if (intPtr == (IntPtr)0)
				{
					throw HttpException.NewWithCode(string.Format("Not enough memory to allocate {0} bytes.", contentLength), 3009);
				}
				if (num2 > 0)
				{
					Marshal.Copy(array, 0, intPtr, num2);
				}
				if (num2 < contentLength)
				{
					array = new byte[Math.Min(contentLength, 32768)];
					do
					{
						int num5 = Math.Min(contentLength - num2, 32768);
						int num6 = this.worker_request.ReadEntityBody(array, num5);
						if (num6 <= 0)
						{
							break;
						}
						Marshal.Copy(array, 0, (IntPtr)((long)intPtr + (long)num2), num6);
						num2 += num6;
					}
					while (num2 < contentLength);
				}
				this.input_stream = new IntPtrStream(intPtr, num2);
			}
			else
			{
				MemoryStream memoryStream = new MemoryStream();
				Stream stream2 = memoryStream;
				if (num2 > 0)
				{
					memoryStream.Write(array, 0, num2);
				}
				array = new byte[32768];
				long num7 = (long)section.MaxRequestLength * 1024L;
				long num8 = (long)section.RequestLengthDiskThreshold * 1024L;
				for (;;)
				{
					int num9 = this.worker_request.ReadEntityBody(array, 32768);
					if (num9 <= 0)
					{
						goto IL_0304;
					}
					num2 += num9;
					if (num2 < 0 || (long)num2 > num7)
					{
						break;
					}
					if (memoryStream != null && (long)num2 > num8)
					{
						this.request_file = this.GetTempStream();
						memoryStream.WriteTo(this.request_file);
						memoryStream = null;
						stream2 = this.request_file;
					}
					stream2.Write(array, 0, num9);
				}
				throw HttpException.NewWithCode(400, "Upload size exceeds httpRuntime limit.", 3004);
				IL_0304:
				if (memoryStream != null)
				{
					this.input_stream = new MemoryStream(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, false, true);
				}
				else
				{
					this.request_file.SetReadOnly();
					this.input_stream = this.request_file;
				}
			}
			this.DoFilter(array);
			if (num2 < contentLength)
			{
				throw HttpException.NewWithCode(411, "The request body is incomplete.", 3009);
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00012ADC File Offset: 0x00010CDC
		internal void ReleaseResources()
		{
			if (this.input_stream != null)
			{
				Stream stream = this.input_stream;
				this.input_stream = null;
				try
				{
					stream.Close();
				}
				catch
				{
				}
			}
			if (this.request_file != null)
			{
				Stream stream = this.request_file;
				this.request_file = null;
				try
				{
					stream.Close();
				}
				catch
				{
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Routing.RequestContext" /> instance of the current request.</summary>
		/// <returns>The <see cref="T:System.Web.Routing.RequestContext" /> instance of the current request. For non-routed requests, the <see cref="T:System.Web.Routing.RequestContext" /> object that is returned is empty.</returns>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00012B48 File Offset: 0x00010D48
		// (set) Token: 0x060007CC RID: 1996 RVA: 0x00012B7C File Offset: 0x00010D7C
		public RequestContext RequestContext
		{
			get
			{
				if (this.requestContext == null)
				{
					this.requestContext = new RequestContext(new HttpContextWrapper(this.context ?? HttpContext.Current), new RouteData());
				}
				return this.requestContext;
			}
			internal set
			{
				this.requestContext = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> object of the current <see cref="T:System.Web.HttpWorkerRequest" /> instance.</summary>
		/// <returns>The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> object of the current <see cref="T:System.Web.HttpWorkerRequest" /> instance.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The current <see cref="T:System.Web.HttpWorkerRequest" /> object is not a System.Web.Hosting.IIS7WorkerRequest object or a System.Web.Hosting.ISAPIWorkerRequestInProc object.</exception>
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00012B85 File Offset: 0x00010D85
		public ChannelBinding HttpChannelBinding
		{
			get
			{
				throw new PlatformNotSupportedException("This property is not supported.");
			}
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</returns>
		/// <exception cref="T:System.Web.HttpException">The request's entity body has already been loaded and parsed. Examples of properties that cause the entity body to be loaded and parsed include the following: The <see cref="P:System.Web.HttpRequest.Form" /> property.The <see cref="P:System.Web.HttpRequest.Files" /> property.The <see cref="P:System.Web.HttpRequest.InputStream" /> property.The <see cref="M:System.Web.HttpRequest.GetBufferlessInputStream" /> method.To avoid this exception, call the <see cref="P:System.Web.HttpRequest.ReadEntityBodyMode" /> method first. This exception is also thrown if the client disconnects while the entity body is being read.</exception>
		// Token: 0x060007CE RID: 1998 RVA: 0x00012B91 File Offset: 0x00010D91
		public Stream GetBufferedInputStream()
		{
			return this.input_stream;
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</returns>
		/// <exception cref="T:System.Web.HttpException">The request's entity body has already been loaded and parsed. Examples of properties that cause the entity body to be loaded and parsed include the following:<see cref="P:System.Web.HttpRequest.Form" /><see cref="P:System.Web.HttpRequest.InputStream" /><see cref="P:System.Web.HttpRequest.Files" /><see cref="M:System.Web.HttpRequest.GetBufferedInputStream" />To avoid this exception, call the <see cref="P:System.Web.HttpRequest.ReadEntityBodyMode" /> method first. This exception is also thrown if the client disconnects while the entity body is being read.</exception>
		// Token: 0x060007CF RID: 1999 RVA: 0x00012B99 File Offset: 0x00010D99
		public Stream GetBufferlessInputStream()
		{
			if (this.bufferlessInputStream == null)
			{
				if (this.input_stream != null)
				{
					throw new HttpException("Input stream has already been created");
				}
				this.bufferlessInputStream = new HttpRequest.BufferlessInputStream(this);
			}
			return this.bufferlessInputStream;
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body, optionally disabling the request-length limit that is set in the <see cref="P:System.Web.Configuration.HttpRuntimeSection.MaxRequestLength" /> property.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</returns>
		/// <param name="disableMaxRequestLength">true to disable the request-length limit; otherwise, false.</param>
		/// <exception cref="T:System.Web.HttpException">The request's entity body has already been loaded and parsed. Examples of properties that cause the entity body to be loaded and parsed include the following: The <see cref="P:System.Web.HttpRequest.Form" /> property.The <see cref="P:System.Web.HttpRequest.Files" /> property.The <see cref="P:System.Web.HttpRequest.InputStream" /> property.The <see cref="M:System.Web.HttpRequest.GetBufferedInputStream" /> method.To avoid this exception, call the <see cref="P:System.Web.HttpRequest.ReadEntityBodyMode" /> method first. This exception is also thrown if the client disconnects while the entity body is being read.</exception>
		// Token: 0x060007D0 RID: 2000 RVA: 0x00012BC8 File Offset: 0x00010DC8
		public Stream GetBufferlessInputStream(bool disableMaxRequestLength)
		{
			return this.GetBufferlessInputStream();
		}

		/// <summary>Gets the contents of the incoming HTTP entity body.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object representing the contents of the incoming HTTP content body.</returns>
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00012BD0 File Offset: 0x00010DD0
		public Stream InputStream
		{
			get
			{
				if (this.input_stream == null)
				{
					this.MakeInputStream();
				}
				return this.input_stream;
			}
		}

		/// <summary>Gets a value indicating whether the request has been authenticated.</summary>
		/// <returns>true if the request is authenticated; otherwise, false.</returns>
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00012BE6 File Offset: 0x00010DE6
		public bool IsAuthenticated
		{
			get
			{
				return this.context.User != null && this.context.User.Identity != null && this.context.User.Identity.IsAuthenticated;
			}
		}

		/// <summary>Gets a value indicating whether the HTTP connection uses secure sockets (that is, HTTPS).</summary>
		/// <returns>true if the connection is an SSL connection; otherwise, false.</returns>
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00012C1E File Offset: 0x00010E1E
		public bool IsSecureConnection
		{
			get
			{
				return this.worker_request != null && this.worker_request.IsSecure();
			}
		}

		/// <summary>Gets the specified object from the <see cref="P:System.Web.HttpRequest.QueryString" />, <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.Cookies" />, or <see cref="P:System.Web.HttpRequest.ServerVariables" /> collections.</summary>
		/// <returns>The <see cref="P:System.Web.HttpRequest.QueryString" />, <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.Cookies" />, or <see cref="P:System.Web.HttpRequest.ServerVariables" /> collection member specified in the <paramref name="key" /> parameter. If the specified <paramref name="key" /> is not found, then null is returned.</returns>
		/// <param name="key">The name of the collection member to get. </param>
		// Token: 0x17000302 RID: 770
		public string this[string key]
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Low)]
			get
			{
				string text = this.QueryString[key];
				if (text == null)
				{
					text = this.Form[key];
				}
				if (text == null)
				{
					HttpCookie httpCookie = this.Cookies[key];
					if (httpCookie != null)
					{
						text = httpCookie.Value;
					}
				}
				if (text == null)
				{
					text = this.ServerVariables[key];
				}
				return text;
			}
		}

		/// <summary>Gets a combined collection of <see cref="P:System.Web.HttpRequest.QueryString" />, <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.Cookies" />, and <see cref="P:System.Web.HttpRequest.ServerVariables" /> items.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object. </returns>
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00012C8D File Offset: 0x00010E8D
		public NameValueCollection Params
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Low)]
			get
			{
				if (this.all_params == null)
				{
					this.all_params = new HttpParamsCollection(this.QueryString, this.Form, this.ServerVariables, this.Cookies);
				}
				return this.all_params;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00012CC0 File Offset: 0x00010EC0
		internal string PathNoValidation
		{
			get
			{
				if (this.original_path == null)
				{
					if (this.url_components != null)
					{
						this.original_path = this.UrlComponents.Path;
					}
					else
					{
						this.original_path = this.ApplyUrlMapping(this.worker_request.GetUriPath());
					}
				}
				return this.original_path;
			}
		}

		/// <summary>Gets the virtual path of the current request.</summary>
		/// <returns>The virtual path of the current request.</returns>
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00012D10 File Offset: 0x00010F10
		public string Path
		{
			get
			{
				if (this.unescaped_path == null)
				{
					this.unescaped_path = this.PathNoValidation;
					int num;
					if (HttpRequest.validateRequestNewMode && !RequestValidator.Current.IsValidRequestString(HttpContext.Current, this.unescaped_path, RequestValidationSource.Path, null, out num))
					{
						HttpRequest.ThrowValidationException("Path", "Path", this.unescaped_path);
					}
				}
				return this.unescaped_path;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00012D6E File Offset: 0x00010F6E
		internal string PathInfoNoValidation
		{
			get
			{
				if (this.path_info_unvalidated == null)
				{
					if (this.worker_request == null)
					{
						return string.Empty;
					}
					this.path_info_unvalidated = this.worker_request.GetPathInfo() ?? string.Empty;
				}
				return this.path_info_unvalidated;
			}
		}

		/// <summary>Gets additional path information for a resource with a URL extension.</summary>
		/// <returns>Additional path information for a resource.</returns>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00012DA8 File Offset: 0x00010FA8
		public string PathInfo
		{
			get
			{
				if (this.path_info == null)
				{
					this.path_info = this.PathInfoNoValidation;
					int num;
					if (HttpRequest.validateRequestNewMode && !RequestValidator.Current.IsValidRequestString(HttpContext.Current, this.path_info, RequestValidationSource.PathInfo, null, out num))
					{
						HttpRequest.ThrowValidationException("PathInfo", "PathInfo", this.path_info);
					}
				}
				return this.path_info;
			}
		}

		/// <summary>Gets the physical file system path of the currently executing server application's root directory.</summary>
		/// <returns>The file system path of the current application's root directory.</returns>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00012E08 File Offset: 0x00011008
		public string PhysicalApplicationPath
		{
			get
			{
				if (this.worker_request == null)
				{
					throw new ArgumentNullException();
				}
				string appDomainAppPath = HttpRuntime.AppDomainAppPath;
				if (SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, appDomainAppPath).Demand();
				}
				return appDomainAppPath;
			}
		}

		/// <summary>Gets the physical file system path corresponding to the requested URL.</summary>
		/// <returns>The file system path of the current request.</returns>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x00012E40 File Offset: 0x00011040
		public string PhysicalPath
		{
			get
			{
				if (this.worker_request == null)
				{
					return string.Empty;
				}
				if (this.physical_path == null)
				{
					this.physical_path = this.worker_request.MapPath(this.FilePath);
				}
				if (SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.physical_path).Demand();
				}
				return this.physical_path;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x00012E98 File Offset: 0x00011098
		internal string RootVirtualDir
		{
			get
			{
				if (this.root_virtual_dir == null)
				{
					string filePath = this.FilePath;
					int num = filePath.LastIndexOf('/');
					if (num < 1)
					{
						this.root_virtual_dir = "/";
					}
					else
					{
						this.root_virtual_dir = filePath.Substring(0, num);
					}
				}
				return this.root_virtual_dir;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00012EE4 File Offset: 0x000110E4
		internal WebROCollection QueryStringUnvalidated
		{
			get
			{
				if (this.query_string_nvc == null)
				{
					this.query_string_nvc = new WebROCollection();
					string text = this.UrlComponents.Query;
					if (text != null)
					{
						if (text.Length != 0)
						{
							text = text.Remove(0, 1);
						}
						HttpUtility.ParseQueryString(text, this.ContentEncoding, this.query_string_nvc);
					}
					this.query_string_nvc.Protect();
				}
				return this.query_string_nvc;
			}
		}

		/// <summary>Gets the collection of HTTP query string variables.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> containing the collection of query string variables sent by the client. For example, If the request URL is <paramref name="http://www.contoso.com/default.aspx?id=44" /> then the value of <see cref="P:System.Web.HttpRequest.QueryString" /> is "<paramref name="id=44" />".</returns>
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x00012F48 File Offset: 0x00011148
		public NameValueCollection QueryString
		{
			get
			{
				NameValueCollection queryStringUnvalidated = this.QueryStringUnvalidated;
				if (HttpRequest.validateRequestNewMode && !this.checked_query_string)
				{
					if (!this.lazyQueryStringValidation)
					{
						this.checked_query_string = true;
						HttpRequest.ValidateNameValueCollection("QueryString", queryStringUnvalidated, RequestValidationSource.QueryString);
					}
				}
				else if (this.validate_query_string && !this.checked_query_string)
				{
					this.checked_query_string = true;
					HttpRequest.ValidateNameValueCollection("QueryString", queryStringUnvalidated);
				}
				return queryStringUnvalidated;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x00012FAC File Offset: 0x000111AC
		internal string RawUrlUnvalidated
		{
			get
			{
				if (this.raw_url_unvalidated == null)
				{
					if (this.worker_request != null)
					{
						this.raw_url_unvalidated = this.worker_request.GetRawUrl();
					}
					else
					{
						this.raw_url_unvalidated = this.UrlComponents.Path + this.UrlComponents.Query;
					}
					if (this.raw_url_unvalidated == null)
					{
						this.raw_url_unvalidated = string.Empty;
					}
				}
				return this.raw_url_unvalidated;
			}
		}

		/// <summary>Gets the raw URL of the current request.</summary>
		/// <returns>The raw URL of the current request.</returns>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00013018 File Offset: 0x00011218
		public string RawUrl
		{
			get
			{
				if (this.raw_url == null)
				{
					this.raw_url = this.RawUrlUnvalidated;
					int num;
					if (HttpRequest.validateRequestNewMode && !RequestValidator.Current.IsValidRequestString(HttpContext.Current, this.raw_url, RequestValidationSource.RawUrl, null, out num))
					{
						HttpRequest.ThrowValidationException("RawUrl", "RawUrl", this.raw_url);
					}
				}
				return this.raw_url;
			}
		}

		/// <summary>Gets or sets the HTTP data transfer method (GET or POST) used by the client.</summary>
		/// <returns>A string representing the HTTP invocation type sent by the client.</returns>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00013078 File Offset: 0x00011278
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x000130C5 File Offset: 0x000112C5
		public string RequestType
		{
			get
			{
				if (this.request_type == null)
				{
					if (this.worker_request != null)
					{
						this.request_type = this.worker_request.GetHttpVerbName();
						this.http_method = this.request_type;
					}
					else
					{
						this.request_type = "GET";
					}
				}
				return this.request_type;
			}
			set
			{
				this.request_type = value;
			}
		}

		/// <summary>Gets a collection of Web server variables.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of server variables.</returns>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x000130CE File Offset: 0x000112CE
		public NameValueCollection ServerVariables
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Low)]
			get
			{
				if (this.server_variables == null)
				{
					this.server_variables = new ServerVariablesCollection(this);
				}
				return this.server_variables;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.CancellationToken" /> object that is tripped when a request times out.</summary>
		/// <returns>The cancellation token.</returns>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00003A1F File Offset: 0x00001C1F
		public CancellationToken TimedOutToken
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the number of bytes in the current input stream.</summary>
		/// <returns>The number of bytes in the input stream.</returns>
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x000130EA File Offset: 0x000112EA
		public int TotalBytes
		{
			get
			{
				return (int)this.InputStream.Length;
			}
		}

		/// <summary>Provides access to HTTP request values without triggering request validation.</summary>
		/// <returns>Request values that have not been checked using request validation.</returns>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x000130F8 File Offset: 0x000112F8
		public UnvalidatedRequestValues Unvalidated
		{
			get
			{
				return new UnvalidatedRequestValues
				{
					Cookies = this.CookiesNoValidation,
					Files = this.Files,
					Form = this.FormUnvalidated,
					Headers = this.HeadersNoValidation,
					Path = this.PathNoValidation,
					PathInfo = this.PathInfoNoValidation,
					QueryString = this.QueryStringUnvalidated,
					RawUrl = this.RawUrlUnvalidated,
					Url = this.Url
				};
			}
		}

		/// <summary>Gets information about the URL of the current request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> object containing information regarding the URL of the current request.</returns>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00013178 File Offset: 0x00011378
		public Uri Url
		{
			get
			{
				if (this.cached_url == null)
				{
					if (this.orig_url == null)
					{
						this.cached_url = this.UrlComponents.Uri;
					}
					else
					{
						this.cached_url = new Uri(this.orig_url);
					}
				}
				return this.cached_url;
			}
		}

		/// <summary>Gets information about the URL of the client's previous request that linked to the current URL.</summary>
		/// <returns>A <see cref="T:System.Uri" /> object.</returns>
		/// <exception cref="T:System.UriFormatException">The HTTP Referer request header is malformed and cannot be converted to a <see cref="T:System.Uri" /> object. </exception>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x000131C8 File Offset: 0x000113C8
		public Uri UrlReferrer
		{
			get
			{
				if (this.worker_request == null)
				{
					return null;
				}
				string knownRequestHeader = this.worker_request.GetKnownRequestHeader(36);
				if (knownRequestHeader == null)
				{
					return null;
				}
				Uri uri = null;
				try
				{
					uri = new Uri(knownRequestHeader);
				}
				catch (UriFormatException)
				{
				}
				return uri;
			}
		}

		/// <summary>Gets the raw user agent string of the client browser.</summary>
		/// <returns>The raw user agent string of the client browser.</returns>
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00013214 File Offset: 0x00011414
		public string UserAgent
		{
			get
			{
				if (this.worker_request == null)
				{
					return null;
				}
				return this.worker_request.GetKnownRequestHeader(39);
			}
		}

		/// <summary>Gets the IP host address of the remote client.</summary>
		/// <returns>The IP address of the remote client.</returns>
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0001322D File Offset: 0x0001142D
		public string UserHostAddress
		{
			get
			{
				if (this.worker_request == null)
				{
					return null;
				}
				return this.worker_request.GetRemoteAddress();
			}
		}

		/// <summary>Gets the DNS name of the remote client.</summary>
		/// <returns>The DNS name of the remote client.</returns>
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x00013244 File Offset: 0x00011444
		public string UserHostName
		{
			get
			{
				if (this.worker_request == null)
				{
					return null;
				}
				return this.worker_request.GetRemoteName();
			}
		}

		/// <summary>Gets a sorted string array of client language preferences.</summary>
		/// <returns>A sorted string array of client language preferences, or null if empty.</returns>
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0001325B File Offset: 0x0001145B
		public string[] UserLanguages
		{
			get
			{
				if (this.worker_request == null)
				{
					return null;
				}
				if (this.user_languages == null)
				{
					this.user_languages = this.SplitHeader(23);
				}
				return this.user_languages;
			}
		}

		/// <summary>Performs a binary read of a specified number of bytes from the current input stream.</summary>
		/// <returns>A byte array.</returns>
		/// <param name="count">The number of bytes to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is 0.- or -<paramref name="count" /> is greater than the number of bytes available. </exception>
		// Token: 0x060007ED RID: 2029 RVA: 0x00013284 File Offset: 0x00011484
		public byte[] BinaryRead(int count)
		{
			if (count < 0)
			{
				throw new ArgumentException("count is < 0");
			}
			Stream inputStream = this.InputStream;
			byte[] array = new byte[count];
			if (inputStream.Read(array, 0, count) != count)
			{
				throw new ArgumentException(string.Format("count {0} exceeds length of available input {1}", count, inputStream.Length - inputStream.Position));
			}
			return array;
		}

		/// <summary>Maps an incoming image-field form parameter to appropriate x-coordinate and y-coordinate values.</summary>
		/// <returns>A two-dimensional array of integers.</returns>
		/// <param name="imageFieldName">The name of the form image map. </param>
		// Token: 0x060007EE RID: 2030 RVA: 0x000132E4 File Offset: 0x000114E4
		public int[] MapImageCoordinates(string imageFieldName)
		{
			string[] imageCoordinatesParameters = this.GetImageCoordinatesParameters(imageFieldName);
			if (imageCoordinatesParameters == null)
			{
				return null;
			}
			int[] array = new int[2];
			try
			{
				array[0] = int.Parse(imageCoordinatesParameters[0]);
				array[1] = int.Parse(imageCoordinatesParameters[1]);
			}
			catch
			{
				return null;
			}
			return array;
		}

		/// <summary>Maps an incoming image field form parameter into appropriate x and y coordinate values.</summary>
		/// <returns>The x and y coordinate values.</returns>
		/// <param name="imageFieldName">The name of the image field.</param>
		// Token: 0x060007EF RID: 2031 RVA: 0x00013338 File Offset: 0x00011538
		public double[] MapRawImageCoordinates(string imageFieldName)
		{
			string[] imageCoordinatesParameters = this.GetImageCoordinatesParameters(imageFieldName);
			if (imageCoordinatesParameters == null)
			{
				return null;
			}
			double[] array = new double[2];
			try
			{
				array[0] = double.Parse(imageCoordinatesParameters[0]);
				array[1] = double.Parse(imageCoordinatesParameters[1]);
			}
			catch
			{
				return null;
			}
			return array;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001338C File Offset: 0x0001158C
		private string[] GetImageCoordinatesParameters(string imageFieldName)
		{
			string httpMethod = this.HttpMethod;
			NameValueCollection nameValueCollection = null;
			if (httpMethod == "HEAD" || httpMethod == "GET")
			{
				nameValueCollection = this.QueryString;
			}
			else if (httpMethod == "POST")
			{
				nameValueCollection = this.Form;
			}
			if (nameValueCollection == null)
			{
				return null;
			}
			string text = nameValueCollection[imageFieldName + ".x"];
			if (text == null || text == "")
			{
				return null;
			}
			string text2 = nameValueCollection[imageFieldName + ".y"];
			if (text2 == null || text2 == "")
			{
				return null;
			}
			return new string[] { text, text2 };
		}

		/// <summary>Maps the specified virtual path to a physical path.</summary>
		/// <returns>The physical path on the server specified by <paramref name="virtualPath" />.</returns>
		/// <param name="virtualPath">The virtual path (absolute or relative) for the current request. </param>
		/// <exception cref="T:System.Web.HttpException">No <see cref="T:System.Web.HttpContext" /> object is defined for the request. </exception>
		// Token: 0x060007F1 RID: 2033 RVA: 0x00013434 File Offset: 0x00011634
		public string MapPath(string virtualPath)
		{
			if (this.worker_request == null)
			{
				return null;
			}
			return this.MapPath(virtualPath, this.BaseVirtualDir, true);
		}

		/// <summary>Maps the specified virtual path to a physical path.</summary>
		/// <returns>The physical path on the server.</returns>
		/// <param name="virtualPath">The virtual path (absolute or relative) for the current request. </param>
		/// <param name="baseVirtualDir">The virtual base directory path used for relative resolution. </param>
		/// <param name="allowCrossAppMapping">true to indicate that <paramref name="virtualPath" /> may belong to another application; otherwise, false. </param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="allowCrossMapping" /> is false and <paramref name="virtualPath" /> belongs to another application. </exception>
		/// <exception cref="T:System.Web.HttpException">No <see cref="T:System.Web.HttpContext" /> object is defined for the request. </exception>
		// Token: 0x060007F2 RID: 2034 RVA: 0x00013450 File Offset: 0x00011650
		public string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping)
		{
			if (this.worker_request == null)
			{
				throw HttpException.NewWithCode("No HttpWorkerRequest", 3001);
			}
			if (virtualPath == null)
			{
				virtualPath = "~";
			}
			else
			{
				virtualPath = virtualPath.Trim();
				if (virtualPath.Length == 0)
				{
					virtualPath = "~";
				}
			}
			if (!VirtualPathUtility.IsValidVirtualPath(virtualPath))
			{
				throw HttpException.NewWithCode(string.Format("'{0}' is not a valid virtual path.", virtualPath), 3001);
			}
			string text = HttpRuntime.AppDomainAppVirtualPath;
			if (!VirtualPathUtility.IsRooted(virtualPath))
			{
				if (StrUtils.IsNullOrEmpty(baseVirtualDir))
				{
					baseVirtualDir = text;
				}
				virtualPath = VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(baseVirtualDir), virtualPath);
				if (!VirtualPathUtility.IsAbsolute(virtualPath))
				{
					virtualPath = VirtualPathUtility.ToAbsolute(virtualPath, false);
				}
			}
			else if (!VirtualPathUtility.IsAbsolute(virtualPath))
			{
				virtualPath = VirtualPathUtility.ToAbsolute(virtualPath, false);
			}
			bool flag = string.Compare(virtualPath, text, RuntimeHelpers.StringComparison) == 0;
			text = VirtualPathUtility.AppendTrailingSlash(text);
			if (!allowCrossAppMapping)
			{
				if (!StrUtils.StartsWith(virtualPath, text, true))
				{
					throw new ArgumentException("MapPath: Mapping across applications not allowed");
				}
				if (text.Length > 1 && virtualPath.Length > 1 && virtualPath[0] != '/')
				{
					throw HttpException.NewWithCode("MapPath: Mapping across applications not allowed", 3001);
				}
			}
			if (!flag && !virtualPath.StartsWith(text, RuntimeHelpers.StringComparison))
			{
				throw new InvalidOperationException(string.Format("Failed to map path '{0}'", virtualPath));
			}
			string text2 = this.worker_request.MapPath(virtualPath);
			if (virtualPath[virtualPath.Length - 1] != '/' && text2[text2.Length - 1] == global::System.IO.Path.DirectorySeparatorChar)
			{
				text2 = text2.TrimEnd(new char[] { global::System.IO.Path.DirectorySeparatorChar });
			}
			return text2;
		}

		/// <summary>Saves an HTTP request to disk.</summary>
		/// <param name="filename">The physical drive path. </param>
		/// <param name="includeHeaders">A Boolean value specifying whether an HTTP header should be saved to disk. </param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.Configuration.HttpRuntimeSection.RequireRootedSaveAsPath" /> property of the <see cref="T:System.Web.Configuration.HttpRuntimeSection" /> is set to true but <paramref name="filename" /> is not an absolute path.</exception>
		// Token: 0x060007F3 RID: 2035 RVA: 0x000135C8 File Offset: 0x000117C8
		public void SaveAs(string filename, bool includeHeaders)
		{
			Stream stream = new FileStream(filename, FileMode.Create);
			if (includeHeaders)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string text = string.Empty;
				string text2 = "/";
				if (this.worker_request != null)
				{
					text = this.worker_request.GetHttpVersion();
					text2 = this.UrlComponents.Path;
				}
				string query = this.UrlComponents.Query;
				stringBuilder.AppendFormat("{0} {1}{2} {3}\r\n", new object[] { this.HttpMethod, text2, query, text });
				NameValueCollection nameValueCollection = this.Headers;
				foreach (string text3 in nameValueCollection.AllKeys)
				{
					stringBuilder.Append(text3);
					stringBuilder.Append(':');
					stringBuilder.Append(nameValueCollection[text3]);
					stringBuilder.Append("\r\n");
				}
				stringBuilder.Append("\r\n");
				byte[] bytes = Encoding.GetEncoding(28591).GetBytes(stringBuilder.ToString());
				stream.Write(bytes, 0, bytes.Length);
			}
			Stream subStream = HttpRequest.GetSubStream(this.InputStream);
			try
			{
				long num = subStream.Length;
				int num2 = (int)Math.Min((num < 0L) ? 0L : num, 8192L);
				byte[] array = new byte[num2];
				int num3;
				while (num > 0L && (num3 = subStream.Read(array, 0, num2)) > 0)
				{
					stream.Write(array, 0, num3);
					num -= (long)num3;
				}
			}
			finally
			{
				stream.Flush();
				stream.Close();
				HttpRequest.EndSubStream(subStream);
			}
		}

		/// <summary>Causes validation to occur for the collections accessed through the <see cref="P:System.Web.HttpRequest.Cookies" />, <see cref="P:System.Web.HttpRequest.Form" />, and <see cref="P:System.Web.HttpRequest.QueryString" /> properties.</summary>
		/// <exception cref="T:System.Web.HttpRequestValidationException">Potentially dangerous data was received from the client. </exception>
		// Token: 0x060007F4 RID: 2036 RVA: 0x00013764 File Offset: 0x00011964
		public void ValidateInput()
		{
			this.validate_cookies = true;
			this.validate_query_string = true;
			this.validate_form = true;
			this.inputValidationEnabled = true;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00013784 File Offset: 0x00011984
		internal void Validate()
		{
			HttpRuntimeSection section = HttpRuntime.Section;
			string query = this.UrlComponents.Query;
			if (query != null && query.Length > section.MaxQueryStringLength)
			{
				throw new HttpException(400, "The length of the query string for this request exceeds the configured maxQueryStringLength value.");
			}
			string pathNoValidation = this.PathNoValidation;
			if (pathNoValidation != null)
			{
				if (pathNoValidation.Length > section.MaxUrlLength)
				{
					throw new HttpException(400, "The length of the URL for this request exceeds the configured maxUrlLength value.");
				}
				char[] requestPathInvalidCharacters = HttpRequest.RequestPathInvalidCharacters;
				if (requestPathInvalidCharacters != null)
				{
					int num = pathNoValidation.IndexOfAny(requestPathInvalidCharacters);
					if (num != -1)
					{
						throw HttpException.NewWithCode(string.Format("A potentially dangerous Request.Path value was detected from the client ({0}).", pathNoValidation[num]), 3003);
					}
				}
			}
			if (HttpRequest.validateRequestNewMode)
			{
				this.ValidateInput();
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00013832 File Offset: 0x00011A32
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x0001383A File Offset: 0x00011A3A
		internal string ClientTarget
		{
			get
			{
				return this.client_target;
			}
			set
			{
				this.client_target = value;
			}
		}

		/// <summary>Gets a value indicating whether the request is from the local computer.</summary>
		/// <returns>true if the request is from the local computer; otherwise, false.</returns>
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00013844 File Offset: 0x00011A44
		public bool IsLocal
		{
			get
			{
				string remoteAddress = this.worker_request.GetRemoteAddress();
				if (StrUtils.IsNullOrEmpty(remoteAddress))
				{
					return false;
				}
				if (remoteAddress == "127.0.0.1")
				{
					return true;
				}
				IPAddress ipaddress = IPAddress.Parse(remoteAddress);
				if (IPAddress.IsLoopback(ipaddress))
				{
					return true;
				}
				for (int i = 0; i < HttpRequest.host_addresses.Length; i++)
				{
					if (ipaddress.Equals(HttpRequest.host_addresses[i]))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x000138AB File Offset: 0x00011AAB
		internal void SetFilePath(string path)
		{
			this.file_path = path;
			this.physical_path = null;
			this.original_path = null;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x000138C4 File Offset: 0x00011AC4
		internal void SetCurrentExePath(string path)
		{
			this.cached_url = null;
			this.current_exe_path = path;
			this.UrlComponents.Path = path + this.PathInfo;
			this.root_virtual_dir = null;
			this.base_virtual_dir = null;
			this.physical_path = null;
			this.unescaped_path = null;
			this.original_path = null;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001391C File Offset: 0x00011B1C
		internal void SetPathInfo(string pi)
		{
			this.cached_url = null;
			this.path_info = pi;
			this.original_path = null;
			string path = this.UrlComponents.Path;
			this.UrlComponents.Path = path + this.PathInfo;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00013961 File Offset: 0x00011B61
		internal void SetFormCollection(WebROCollection coll, bool lazyValidation)
		{
			if (coll == null)
			{
				return;
			}
			this.form = coll;
			this.lazyFormValidation = lazyValidation;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00013975 File Offset: 0x00011B75
		internal void SetQueryStringCollection(WebROCollection coll, bool lazyValidation)
		{
			if (coll == null)
			{
				return;
			}
			this.query_string_nvc = coll;
			this.lazyQueryStringValidation = lazyValidation;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00013989 File Offset: 0x00011B89
		internal void SetHeader(string name, string value)
		{
			WebROCollection webROCollection = (WebROCollection)this.Headers;
			webROCollection.Unprotect();
			webROCollection[name] = value;
			webROCollection.Protect();
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x000139AC File Offset: 0x00011BAC
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x00013A02 File Offset: 0x00011C02
		internal string QueryStringRaw
		{
			get
			{
				if (this.UrlComponents != null)
				{
					return this.UrlComponents.Query;
				}
				string queryString = this.worker_request.GetQueryString();
				if (queryString == null || queryString.Length == 0)
				{
					return string.Empty;
				}
				if (queryString[0] == '?')
				{
					return queryString;
				}
				return "?" + queryString;
			}
			set
			{
				this.UrlComponents.Query = value;
				this.cached_url = null;
				this.query_string_nvc = null;
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00013A1E File Offset: 0x00011C1E
		internal void SetForm(WebROCollection coll)
		{
			this.form = coll;
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00013A27 File Offset: 0x00011C27
		internal HttpWorkerRequest WorkerRequest
		{
			get
			{
				return this.worker_request;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x00013A2F File Offset: 0x00011C2F
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x00013A37 File Offset: 0x00011C37
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

		// Token: 0x06000805 RID: 2053 RVA: 0x00013A40 File Offset: 0x00011C40
		private static void ValidateNameValueCollection(string name, NameValueCollection coll)
		{
			if (coll == null)
			{
				return;
			}
			foreach (object obj in coll.Keys)
			{
				string text = (string)obj;
				string text2 = coll[text];
				if (text2 != null && text2.Length > 0 && HttpRequest.IsInvalidString(text2))
				{
					HttpRequest.ThrowValidationException(name, text, text2);
				}
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00013ABC File Offset: 0x00011CBC
		private static void ValidateNameValueCollection(string name, NameValueCollection coll, RequestValidationSource source)
		{
			if (coll == null)
			{
				return;
			}
			RequestValidator requestValidator = RequestValidator.Current;
			HttpContext httpContext = HttpContext.Current;
			foreach (object obj in coll.Keys)
			{
				string text = (string)obj;
				string text2 = coll[text];
				int num;
				if (text2 != null && text2.Length > 0 && !requestValidator.IsValidRequestString(httpContext, text2, source, text, out num))
				{
					HttpRequest.ThrowValidationException(name, text, text2);
				}
			}
		}

		/// <summary>Provides IIS with a copy of the HTTP request entity body.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">The method was invoked on a version of IIS earlier than IIS 7.0. </exception>
		// Token: 0x06000807 RID: 2055 RVA: 0x00013B54 File Offset: 0x00011D54
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		public void InsertEntityBody()
		{
			throw new PlatformNotSupportedException("This method is not supported.");
		}

		/// <summary>Provides IIS with a copy of the HTTP request entity body and with information about the request entity object.</summary>
		/// <param name="buffer">An array that contains the request entity data.</param>
		/// <param name="offset">The zero-based position in <paramref name="buffer" /> at which to begin storing the request entity data.</param>
		/// <param name="count">The number of bytes to read into the <paramref name="buffer" /> array.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">The method was invoked on a version of IIS earlier than IIS 7.0. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is a negative value. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is a negative value.</exception>
		/// <exception cref="T:System.ArgumentException">The number of items in <paramref name="count" /> is larger than the available space in <paramref name="buffer" />, given the <paramref name="offset" /> value.</exception>
		// Token: 0x06000808 RID: 2056 RVA: 0x00013B54 File Offset: 0x00011D54
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		public void InsertEntityBody(byte[] buffer, int offset, int count)
		{
			throw new PlatformNotSupportedException("This method is not supported.");
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00013B60 File Offset: 0x00011D60
		private static void ValidateCookieCollection(HttpCookieCollection cookies)
		{
			if (cookies == null)
			{
				return;
			}
			int count = cookies.Count;
			RequestValidator requestValidator = RequestValidator.Current;
			HttpContext httpContext = HttpContext.Current;
			for (int i = 0; i < count; i++)
			{
				HttpCookie httpCookie = cookies[i];
				if (httpCookie != null)
				{
					string value = httpCookie.Value;
					string name = httpCookie.Name;
					if (!string.IsNullOrEmpty(value))
					{
						bool flag;
						if (HttpRequest.validateRequestNewMode)
						{
							int num;
							flag = !requestValidator.IsValidRequestString(httpContext, value, RequestValidationSource.Cookies, name, out num);
						}
						else
						{
							flag = HttpRequest.IsInvalidString(value);
						}
						if (flag)
						{
							HttpRequest.ThrowValidationException("Cookies", name, value);
						}
					}
				}
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00013BF4 File Offset: 0x00011DF4
		private static void ThrowValidationException(string name, string key, string value)
		{
			string text = "\"" + value + "\"";
			if (text.Length > 20)
			{
				text = text.Substring(0, 16) + "...\"";
			}
			throw new HttpRequestValidationException(string.Format("A potentially dangerous Request.{0} value was detected from the client ({1}={2}).", name, key, text));
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00013C44 File Offset: 0x00011E44
		internal static void ValidateString(string key, string value, RequestValidationSource source)
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			int num;
			if (HttpRequest.IsInvalidString(value, out num))
			{
				HttpRequest.ThrowValidationException(source.ToString(), key, value);
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00013C78 File Offset: 0x00011E78
		internal static bool IsInvalidString(string val)
		{
			int num;
			return HttpRequest.IsInvalidString(val, out num);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00013C90 File Offset: 0x00011E90
		internal static bool IsInvalidString(string val, out int validationFailureIndex)
		{
			validationFailureIndex = 0;
			int length = val.Length;
			if (length < 2)
			{
				return false;
			}
			char c = val[0];
			for (int i = 1; i < length; i++)
			{
				char c2 = val[i];
				if (c == '<' || c == '＜')
				{
					if (c2 == '!' || c2 < ' ' || (c2 >= 'a' && c2 <= 'z') || (c2 >= 'A' && c2 <= 'Z'))
					{
						validationFailureIndex = i - 1;
						return true;
					}
				}
				else if (c == '&' && c2 == '#')
				{
					validationFailureIndex = i - 1;
					return true;
				}
				c = c2;
			}
			return false;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00013D10 File Offset: 0x00011F10
		private static IPAddress[] GetLocalHostAddresses()
		{
			IPAddress[] array;
			try
			{
				array = Dns.GetHostAddresses(Dns.GetHostName());
			}
			catch
			{
				array = new IPAddress[0];
			}
			return array;
		}

		/// <summary>Gets a value that indicates whether the request entity body has been read, and if so, how it was read.</summary>
		/// <returns>The value that indicates how the request entity body was read, or that it has not been read.</returns>
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00013D48 File Offset: 0x00011F48
		public ReadEntityBodyMode ReadEntityBodyMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ReadEntityBodyMode.None;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ITlsTokenBindingInfo TlsTokenBindingInfo
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Forcibly terminates the underlying TCP connection, causing any outstanding I/O to fail.</summary>
		// Token: 0x06000811 RID: 2065 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Abort()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000F77 RID: 3959
		private HttpWorkerRequest worker_request;

		// Token: 0x04000F78 RID: 3960
		private HttpContext context;

		// Token: 0x04000F79 RID: 3961
		private WebROCollection query_string_nvc;

		// Token: 0x04000F7A RID: 3962
		private string orig_url;

		// Token: 0x04000F7B RID: 3963
		private UriBuilder url_components;

		// Token: 0x04000F7C RID: 3964
		private string client_target;

		// Token: 0x04000F7D RID: 3965
		private HttpBrowserCapabilities browser_capabilities;

		// Token: 0x04000F7E RID: 3966
		private string file_path;

		// Token: 0x04000F7F RID: 3967
		private string base_virtual_dir;

		// Token: 0x04000F80 RID: 3968
		private string root_virtual_dir;

		// Token: 0x04000F81 RID: 3969
		private string client_file_path;

		// Token: 0x04000F82 RID: 3970
		private string content_type;

		// Token: 0x04000F83 RID: 3971
		private int content_length = -1;

		// Token: 0x04000F84 RID: 3972
		private Encoding encoding;

		// Token: 0x04000F85 RID: 3973
		private string current_exe_path;

		// Token: 0x04000F86 RID: 3974
		private string physical_path;

		// Token: 0x04000F87 RID: 3975
		private string unescaped_path;

		// Token: 0x04000F88 RID: 3976
		private string original_path;

		// Token: 0x04000F89 RID: 3977
		private string path_info;

		// Token: 0x04000F8A RID: 3978
		private string path_info_unvalidated;

		// Token: 0x04000F8B RID: 3979
		private string raw_url;

		// Token: 0x04000F8C RID: 3980
		private string raw_url_unvalidated;

		// Token: 0x04000F8D RID: 3981
		private WebROCollection all_params;

		// Token: 0x04000F8E RID: 3982
		private NameValueCollection headers;

		// Token: 0x04000F8F RID: 3983
		private WebROCollection headers_unvalidated;

		// Token: 0x04000F90 RID: 3984
		private Stream input_stream;

		// Token: 0x04000F91 RID: 3985
		private InputFilterStream input_filter;

		// Token: 0x04000F92 RID: 3986
		private Stream filter;

		// Token: 0x04000F93 RID: 3987
		private HttpCookieCollection cookies;

		// Token: 0x04000F94 RID: 3988
		private HttpCookieCollection cookies_unvalidated;

		// Token: 0x04000F95 RID: 3989
		private string http_method;

		// Token: 0x04000F96 RID: 3990
		private WebROCollection form;

		// Token: 0x04000F97 RID: 3991
		private HttpFileCollection files;

		// Token: 0x04000F98 RID: 3992
		private ServerVariablesCollection server_variables;

		// Token: 0x04000F99 RID: 3993
		private HttpClientCertificate client_cert;

		// Token: 0x04000F9A RID: 3994
		private string request_type;

		// Token: 0x04000F9B RID: 3995
		private string[] accept_types;

		// Token: 0x04000F9C RID: 3996
		private string[] user_languages;

		// Token: 0x04000F9D RID: 3997
		private Uri cached_url;

		// Token: 0x04000F9E RID: 3998
		private TempFileStream request_file;

		// Token: 0x04000F9F RID: 3999
		private static readonly IPAddress[] host_addresses;

		// Token: 0x04000FA0 RID: 4000
		private bool validate_cookies;

		// Token: 0x04000FA1 RID: 4001
		private bool validate_query_string;

		// Token: 0x04000FA2 RID: 4002
		private bool validate_form;

		// Token: 0x04000FA3 RID: 4003
		private bool checked_cookies;

		// Token: 0x04000FA4 RID: 4004
		private bool checked_query_string;

		// Token: 0x04000FA5 RID: 4005
		private bool checked_form;

		// Token: 0x04000FA6 RID: 4006
		private static readonly UrlMappingCollection urlMappings;

		// Token: 0x04000FA7 RID: 4007
		private static readonly char[] queryTrimChars = new char[] { '?' };

		// Token: 0x04000FA8 RID: 4008
		private bool lazyFormValidation;

		// Token: 0x04000FA9 RID: 4009
		private bool lazyQueryStringValidation;

		// Token: 0x04000FAA RID: 4010
		private bool inputValidationEnabled;

		// Token: 0x04000FAB RID: 4011
		private RequestContext requestContext;

		// Token: 0x04000FAC RID: 4012
		private HttpRequest.BufferlessInputStream bufferlessInputStream;

		// Token: 0x04000FAD RID: 4013
		private static bool validateRequestNewMode;

		// Token: 0x04000FAF RID: 4015
		private string anonymous_id;

		// Token: 0x04000FB0 RID: 4016
		private const int INPUT_BUFFER_SIZE = 32768;

		// Token: 0x020000A1 RID: 161
		private class BufferlessInputStream : Stream
		{
			// Token: 0x06000812 RID: 2066 RVA: 0x00013D63 File Offset: 0x00011F63
			public BufferlessInputStream(HttpRequest request)
			{
				this.request = request;
				this.content_length = request.ContentLength;
			}

			// Token: 0x17000321 RID: 801
			// (get) Token: 0x06000813 RID: 2067 RVA: 0x00008B66 File Offset: 0x00006D66
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000322 RID: 802
			// (get) Token: 0x06000814 RID: 2068 RVA: 0x00008A69 File Offset: 0x00006C69
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000323 RID: 803
			// (get) Token: 0x06000815 RID: 2069 RVA: 0x00008A69 File Offset: 0x00006C69
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000324 RID: 804
			// (get) Token: 0x06000816 RID: 2070 RVA: 0x00013D7E File Offset: 0x00011F7E
			public override long Length
			{
				get
				{
					return (long)this.content_length;
				}
			}

			// Token: 0x17000325 RID: 805
			// (get) Token: 0x06000817 RID: 2071 RVA: 0x00013D87 File Offset: 0x00011F87
			// (set) Token: 0x06000818 RID: 2072 RVA: 0x00013D8F File Offset: 0x00011F8F
			public override long Position
			{
				get
				{
					return this.position;
				}
				set
				{
					throw new NotSupportedException("This is a readonly stream");
				}
			}

			// Token: 0x06000819 RID: 2073 RVA: 0x0000393A File Offset: 0x00001B3A
			public override void Flush()
			{
			}

			// Token: 0x0600081A RID: 2074 RVA: 0x00013D9C File Offset: 0x00011F9C
			public override int Read(byte[] buffer, int offset, int count)
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (offset < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException("offset or count less than zero.");
				}
				if (buffer.Length - offset < count)
				{
					throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
				}
				if (count == 0 || this.request.worker_request == null)
				{
					return 0;
				}
				if (!this.checked_maxRequestLength)
				{
					int num = this.content_length / 1024;
					HttpRuntimeSection section = HttpRuntime.Section;
					if (num > section.MaxRequestLength)
					{
						throw HttpException.NewWithCode(400, "Upload size exceeds httpRuntime limit.", 3004);
					}
					this.checked_maxRequestLength = true;
				}
				if (!this.preloaded_served)
				{
					if (this.preloadedBuffer == null)
					{
						this.preloadedBuffer = this.request.worker_request.GetPreloadedEntityBody();
					}
					if (this.preloadedBuffer != null)
					{
						long num2 = (long)this.preloadedBuffer.Length - this.position;
						int num3 = (int)Math.Min((long)count, num2);
						Array.Copy(this.preloadedBuffer, this.position, buffer, (long)offset, (long)num3);
						this.position += (long)num3;
						if ((long)num3 == num2)
						{
							this.preloaded_served = true;
						}
						return num3;
					}
					this.preloaded_served = true;
				}
				if (this.position < (long)this.content_length)
				{
					long num4 = (long)this.content_length - this.position;
					int num5 = count;
					if (num4 < (long)count)
					{
						num5 = (int)num4;
					}
					int num6 = this.request.worker_request.ReadEntityBody(buffer, offset, num5);
					this.position += (long)num6;
					return num6;
				}
				return 0;
			}

			// Token: 0x0600081B RID: 2075 RVA: 0x00013F09 File Offset: 0x00012109
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException("Can not seek on the HttpRequest.BufferlessInputStream");
			}

			// Token: 0x0600081C RID: 2076 RVA: 0x00013F15 File Offset: 0x00012115
			public override void SetLength(long value)
			{
				throw new NotSupportedException("Can not set length on the HttpRequest.BufferlessInputStream");
			}

			// Token: 0x0600081D RID: 2077 RVA: 0x00013F21 File Offset: 0x00012121
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException("Can not write on the HttpRequest.BufferlessInputStream");
			}

			// Token: 0x04000FB1 RID: 4017
			private HttpRequest request;

			// Token: 0x04000FB2 RID: 4018
			private int content_length;

			// Token: 0x04000FB3 RID: 4019
			private byte[] preloadedBuffer;

			// Token: 0x04000FB4 RID: 4020
			private bool preloaded_served;

			// Token: 0x04000FB5 RID: 4021
			private bool checked_maxRequestLength;

			// Token: 0x04000FB6 RID: 4022
			private long position;
		}
	}
}
