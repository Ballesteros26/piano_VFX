using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace System.Net
{
	/// <summary>Describes an incoming HTTP request to an <see cref="T:System.Net.HttpListener" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000524 RID: 1316
	public sealed class HttpListenerRequest
	{
		// Token: 0x0600280E RID: 10254 RVA: 0x0009A582 File Offset: 0x00098782
		internal HttpListenerRequest(HttpListenerContext context)
		{
			this.context = context;
			this.headers = new WebHeaderCollection();
			this.version = HttpVersion.Version10;
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x0009A5A8 File Offset: 0x000987A8
		internal void SetRequestLine(string req)
		{
			string[] array = req.Split(HttpListenerRequest.separators, 3);
			if (array.Length != 3)
			{
				this.context.ErrorMessage = "Invalid request line (parts).";
				return;
			}
			this.method = array[0];
			foreach (char c in this.method)
			{
				int num = (int)c;
				if ((num < 65 || num > 90) && (num <= 32 || c >= '\u007f' || c == '(' || c == ')' || c == '<' || c == '<' || c == '>' || c == '@' || c == ',' || c == ';' || c == ':' || c == '\\' || c == '"' || c == '/' || c == '[' || c == ']' || c == '?' || c == '=' || c == '{' || c == '}'))
				{
					this.context.ErrorMessage = "(Invalid verb)";
					return;
				}
			}
			this.raw_url = array[1];
			if (array[2].Length != 8 || !array[2].StartsWith("HTTP/"))
			{
				this.context.ErrorMessage = "Invalid request line (version).";
				return;
			}
			try
			{
				this.version = new Version(array[2].Substring(5));
				if (this.version.Major < 1)
				{
					throw new Exception();
				}
			}
			catch
			{
				this.context.ErrorMessage = "Invalid request line (version).";
			}
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x0009A710 File Offset: 0x00098910
		private void CreateQueryString(string query)
		{
			if (query == null || query.Length == 0)
			{
				this.query_string = new NameValueCollection(1);
				return;
			}
			this.query_string = new NameValueCollection();
			if (query[0] == '?')
			{
				query = query.Substring(1);
			}
			foreach (string text in query.Split(new char[] { '&' }))
			{
				int num = text.IndexOf('=');
				if (num == -1)
				{
					this.query_string.Add(null, WebUtility.UrlDecode(text));
				}
				else
				{
					string text2 = WebUtility.UrlDecode(text.Substring(0, num));
					string text3 = WebUtility.UrlDecode(text.Substring(num + 1));
					this.query_string.Add(text2, text3);
				}
			}
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x0009A7C8 File Offset: 0x000989C8
		private static bool MaybeUri(string s)
		{
			int num = s.IndexOf(':');
			return num != -1 && num < 10 && HttpListenerRequest.IsPredefinedScheme(s.Substring(0, num));
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x0009A7F8 File Offset: 0x000989F8
		private static bool IsPredefinedScheme(string scheme)
		{
			if (scheme == null || scheme.Length < 3)
			{
				return false;
			}
			char c = scheme[0];
			if (c == 'h')
			{
				return scheme == "http" || scheme == "https";
			}
			if (c == 'f')
			{
				return scheme == "file" || scheme == "ftp";
			}
			if (c != 'n')
			{
				return (c == 'g' && scheme == "gopher") || (c == 'm' && scheme == "mailto");
			}
			c = scheme[1];
			if (c == 'e')
			{
				return scheme == "news" || scheme == "net.pipe" || scheme == "net.tcp";
			}
			return scheme == "nntp";
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x0009A8D0 File Offset: 0x00098AD0
		internal void FinishInitialization()
		{
			string text = this.UserHostName;
			if (this.version > HttpVersion.Version10 && (text == null || text.Length == 0))
			{
				this.context.ErrorMessage = "Invalid host name";
				return;
			}
			Uri uri = null;
			string pathAndQuery;
			if (HttpListenerRequest.MaybeUri(this.raw_url.ToLowerInvariant()) && Uri.TryCreate(this.raw_url, UriKind.Absolute, out uri))
			{
				pathAndQuery = uri.PathAndQuery;
			}
			else
			{
				pathAndQuery = this.raw_url;
			}
			if (text == null || text.Length == 0)
			{
				text = this.UserHostAddress;
			}
			if (uri != null)
			{
				text = uri.Host;
			}
			int num = text.IndexOf(':');
			if (num >= 0)
			{
				text = text.Substring(0, num);
			}
			string text2 = string.Format("{0}://{1}:{2}", this.IsSecureConnection ? "https" : "http", text, this.LocalEndPoint.Port);
			if (!Uri.TryCreate(text2 + pathAndQuery, UriKind.Absolute, out this.url))
			{
				this.context.ErrorMessage = WebUtility.HtmlEncode("Invalid url: " + text2 + pathAndQuery);
				return;
			}
			this.CreateQueryString(this.url.Query);
			this.url = HttpListenerRequestUriBuilder.GetRequestUri(this.raw_url, this.url.Scheme, this.url.Authority, this.url.LocalPath, this.url.Query);
			if (this.version >= HttpVersion.Version11)
			{
				string text3 = this.Headers["Transfer-Encoding"];
				this.is_chunked = text3 != null && string.Compare(text3, "chunked", StringComparison.OrdinalIgnoreCase) == 0;
				if (text3 != null && !this.is_chunked)
				{
					this.context.Connection.SendError(null, 501);
					return;
				}
			}
			if (!this.is_chunked && !this.cl_set && (string.Compare(this.method, "POST", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.method, "PUT", StringComparison.OrdinalIgnoreCase) == 0))
			{
				this.context.Connection.SendError(null, 411);
				return;
			}
			if (string.Compare(this.Headers["Expect"], "100-continue", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.context.Connection.GetResponseStream().InternalWrite(HttpListenerRequest._100continue, 0, HttpListenerRequest._100continue.Length);
			}
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x0009AB24 File Offset: 0x00098D24
		internal static string Unquote(string str)
		{
			int num = str.IndexOf('"');
			int num2 = str.LastIndexOf('"');
			if (num >= 0 && num2 >= 0)
			{
				str = str.Substring(num + 1, num2 - 1);
			}
			return str.Trim();
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x0009AB60 File Offset: 0x00098D60
		internal void AddHeader(string header)
		{
			int num = header.IndexOf(':');
			if (num == -1 || num == 0)
			{
				this.context.ErrorMessage = "Bad Request";
				this.context.ErrorStatus = 400;
				return;
			}
			string text = header.Substring(0, num).Trim();
			string text2 = header.Substring(num + 1).Trim();
			string text3 = text.ToLower(CultureInfo.InvariantCulture);
			this.headers.SetInternal(text, text2);
			if (text3 == "accept-language")
			{
				this.user_languages = text2.Split(new char[] { ',' });
				return;
			}
			if (!(text3 == "accept"))
			{
				if (!(text3 == "content-length"))
				{
					if (!(text3 == "referer"))
					{
						if (!(text3 == "cookie"))
						{
							return;
						}
						goto IL_0155;
					}
				}
				else
				{
					try
					{
						this.content_length = long.Parse(text2.Trim());
						if (this.content_length < 0L)
						{
							this.context.ErrorMessage = "Invalid Content-Length.";
						}
						this.cl_set = true;
						return;
					}
					catch
					{
						this.context.ErrorMessage = "Invalid Content-Length.";
						return;
					}
				}
				try
				{
					this.referrer = new Uri(text2);
					return;
				}
				catch
				{
					this.referrer = new Uri("http://someone.is.screwing.with.the.headers.com/");
					return;
				}
				IL_0155:
				if (this.cookies == null)
				{
					this.cookies = new CookieCollection();
				}
				string[] array = text2.Split(new char[] { ',', ';' });
				Cookie cookie = null;
				int num2 = 0;
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text4 = array2[i].Trim();
					if (text4.Length != 0)
					{
						if (text4.StartsWith("$Version"))
						{
							num2 = int.Parse(HttpListenerRequest.Unquote(text4.Substring(text4.IndexOf('=') + 1)));
						}
						else if (text4.StartsWith("$Path"))
						{
							if (cookie != null)
							{
								cookie.Path = text4.Substring(text4.IndexOf('=') + 1).Trim();
							}
						}
						else if (text4.StartsWith("$Domain"))
						{
							if (cookie != null)
							{
								cookie.Domain = text4.Substring(text4.IndexOf('=') + 1).Trim();
							}
						}
						else if (text4.StartsWith("$Port"))
						{
							if (cookie != null)
							{
								cookie.Port = text4.Substring(text4.IndexOf('=') + 1).Trim();
							}
						}
						else
						{
							if (cookie != null)
							{
								this.cookies.Add(cookie);
							}
							try
							{
								cookie = new Cookie();
								int num3 = text4.IndexOf('=');
								if (num3 > 0)
								{
									cookie.Name = text4.Substring(0, num3).Trim();
									cookie.Value = text4.Substring(num3 + 1).Trim();
								}
								else
								{
									cookie.Name = text4.Trim();
									cookie.Value = string.Empty;
								}
								cookie.Version = num2;
							}
							catch (CookieException)
							{
								cookie = null;
							}
						}
					}
				}
				if (cookie != null)
				{
					this.cookies.Add(cookie);
				}
				return;
			}
			this.accept_types = text2.Split(new char[] { ',' });
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x0009AEBC File Offset: 0x000990BC
		internal bool FlushInput()
		{
			if (!this.HasEntityBody)
			{
				return true;
			}
			int num = 2048;
			if (this.content_length > 0L)
			{
				num = (int)Math.Min(this.content_length, (long)num);
			}
			byte[] array = new byte[num];
			bool flag;
			for (;;)
			{
				try
				{
					IAsyncResult asyncResult = this.InputStream.BeginRead(array, 0, num, null, null);
					if (!asyncResult.IsCompleted && !asyncResult.AsyncWaitHandle.WaitOne(1000))
					{
						flag = false;
					}
					else
					{
						if (this.InputStream.EndRead(asyncResult) > 0)
						{
							continue;
						}
						flag = true;
					}
				}
				catch (ObjectDisposedException)
				{
					this.input_stream = null;
					flag = true;
				}
				catch
				{
					flag = false;
				}
				break;
			}
			return flag;
		}

		/// <summary>Gets the MIME types accepted by the client. </summary>
		/// <returns>A <see cref="T:System.String" /> array that contains the type names specified in the request's Accept header or null if the client request did not include an Accept header.</returns>
		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002817 RID: 10263 RVA: 0x0009AF6C File Offset: 0x0009916C
		public string[] AcceptTypes
		{
			get
			{
				return this.accept_types;
			}
		}

		/// <summary>Gets an error code that identifies a problem with the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> provided by the client.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains a Windows error code.</returns>
		/// <exception cref="T:System.InvalidOperationException">The client certificate has not been initialized yet by a call to the <see cref="M:System.Net.HttpListenerRequest.BeginGetClientCertificate(System.AsyncCallback,System.Object)" /> or <see cref="M:System.Net.HttpListenerRequest.GetClientCertificate" /> methods-or - The operation is still in progress.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06002818 RID: 10264 RVA: 0x0009AF74 File Offset: 0x00099174
		public int ClientCertificateError
		{
			get
			{
				HttpConnection connection = this.context.Connection;
				if (connection.ClientCertificate == null)
				{
					throw new InvalidOperationException("No client certificate");
				}
				int[] clientCertificateErrors = connection.ClientCertificateErrors;
				if (clientCertificateErrors != null && clientCertificateErrors.Length != 0)
				{
					return clientCertificateErrors[0];
				}
				return 0;
			}
		}

		/// <summary>Gets the content encoding that can be used with data sent with the request</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object suitable for use with the data in the <see cref="P:System.Net.HttpListenerRequest.InputStream" /> property.</returns>
		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002819 RID: 10265 RVA: 0x0009AFB1 File Offset: 0x000991B1
		public Encoding ContentEncoding
		{
			get
			{
				if (this.content_encoding == null)
				{
					this.content_encoding = Encoding.Default;
				}
				return this.content_encoding;
			}
		}

		/// <summary>Gets the length of the body data included in the request.</summary>
		/// <returns>The value from the request's Content-Length header. This value is -1 if the content length is not known.</returns>
		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x0600281A RID: 10266 RVA: 0x0009AFCC File Offset: 0x000991CC
		public long ContentLength64
		{
			get
			{
				if (!this.is_chunked)
				{
					return this.content_length;
				}
				return -1L;
			}
		}

		/// <summary>Gets the MIME type of the body data included in the request.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the text of the request's Content-Type header.</returns>
		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x0600281B RID: 10267 RVA: 0x0009AFDF File Offset: 0x000991DF
		public string ContentType
		{
			get
			{
				return this.headers["content-type"];
			}
		}

		/// <summary>Gets the cookies sent with the request.</summary>
		/// <returns>A <see cref="T:System.Net.CookieCollection" /> that contains cookies that accompany the request. This property returns an empty collection if the request does not contain cookies.</returns>
		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x0600281C RID: 10268 RVA: 0x0009AFF1 File Offset: 0x000991F1
		public CookieCollection Cookies
		{
			get
			{
				if (this.cookies == null)
				{
					this.cookies = new CookieCollection();
				}
				return this.cookies;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the request has associated body data.</summary>
		/// <returns>true if the request has associated body data; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x0600281D RID: 10269 RVA: 0x0009B00C File Offset: 0x0009920C
		public bool HasEntityBody
		{
			get
			{
				return this.content_length > 0L || this.is_chunked;
			}
		}

		/// <summary>Gets the collection of header name/value pairs sent in the request.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the HTTP headers included in the request.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x0600281E RID: 10270 RVA: 0x0009B020 File Offset: 0x00099220
		public NameValueCollection Headers
		{
			get
			{
				return this.headers;
			}
		}

		/// <summary>Gets the HTTP method specified by the client. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the method used in the request.</returns>
		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x0600281F RID: 10271 RVA: 0x0009B028 File Offset: 0x00099228
		public string HttpMethod
		{
			get
			{
				return this.method;
			}
		}

		/// <summary>Gets a stream that contains the body data sent by the client.</summary>
		/// <returns>A readable <see cref="T:System.IO.Stream" /> object that contains the bytes sent by the client in the body of the request. This property returns <see cref="F:System.IO.Stream.Null" /> if no data is sent with the request.</returns>
		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002820 RID: 10272 RVA: 0x0009B030 File Offset: 0x00099230
		public Stream InputStream
		{
			get
			{
				if (this.input_stream == null)
				{
					if (this.is_chunked || this.content_length > 0L)
					{
						this.input_stream = this.context.Connection.GetRequestStream(this.is_chunked, this.content_length);
					}
					else
					{
						this.input_stream = Stream.Null;
					}
				}
				return this.input_stream;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the client sending this request is authenticated.</summary>
		/// <returns>true if the client was authenticated; otherwise, false.</returns>
		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06002821 RID: 10273 RVA: 0x00004240 File Offset: 0x00002440
		[MonoTODO("Always returns false")]
		public bool IsAuthenticated
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the request is sent from the local computer.</summary>
		/// <returns>true if the request originated on the same computer as the <see cref="T:System.Net.HttpListener" /> object that provided the request; otherwise, false.</returns>
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002822 RID: 10274 RVA: 0x0009B08C File Offset: 0x0009928C
		public bool IsLocal
		{
			get
			{
				return this.LocalEndPoint.Address.Equals(this.RemoteEndPoint.Address);
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the TCP connection used to send the request is using the Secure Sockets Layer (SSL) protocol.</summary>
		/// <returns>true if the TCP connection is using SSL; otherwise, false.</returns>
		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06002823 RID: 10275 RVA: 0x0009B0A9 File Offset: 0x000992A9
		public bool IsSecureConnection
		{
			get
			{
				return this.context.Connection.IsSecure;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the client requests a persistent connection.</summary>
		/// <returns>true if the connection should be kept open; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06002824 RID: 10276 RVA: 0x0009B0BC File Offset: 0x000992BC
		public bool KeepAlive
		{
			get
			{
				if (this.ka_set)
				{
					return this.keep_alive;
				}
				this.ka_set = true;
				string text = this.headers["Connection"];
				if (!string.IsNullOrEmpty(text))
				{
					this.keep_alive = string.Compare(text, "keep-alive", StringComparison.OrdinalIgnoreCase) == 0;
				}
				else if (this.version == HttpVersion.Version11)
				{
					this.keep_alive = true;
				}
				else
				{
					text = this.headers["keep-alive"];
					if (!string.IsNullOrEmpty(text))
					{
						this.keep_alive = string.Compare(text, "closed", StringComparison.OrdinalIgnoreCase) != 0;
					}
				}
				return this.keep_alive;
			}
		}

		/// <summary>Get the server IP address and port number to which the request is directed.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> that represents the IP address that the request is sent to.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06002825 RID: 10277 RVA: 0x0009B15E File Offset: 0x0009935E
		public IPEndPoint LocalEndPoint
		{
			get
			{
				return this.context.Connection.LocalEndPoint;
			}
		}

		/// <summary>Gets the HTTP version used by the requesting client.</summary>
		/// <returns>A <see cref="T:System.Version" /> that identifies the client's version of HTTP.</returns>
		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002826 RID: 10278 RVA: 0x0009B170 File Offset: 0x00099370
		public Version ProtocolVersion
		{
			get
			{
				return this.version;
			}
		}

		/// <summary>Gets the query string included in the request.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains the query data included in the request <see cref="P:System.Net.HttpListenerRequest.Url" />.</returns>
		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002827 RID: 10279 RVA: 0x0009B178 File Offset: 0x00099378
		public NameValueCollection QueryString
		{
			get
			{
				return this.query_string;
			}
		}

		/// <summary>Gets the URL information (without the host and port) requested by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the raw URL for this request.</returns>
		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002828 RID: 10280 RVA: 0x0009B180 File Offset: 0x00099380
		public string RawUrl
		{
			get
			{
				return this.raw_url;
			}
		}

		/// <summary>Gets the client IP address and port number from which the request originated.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> that represents the IP address and port number from which the request originated.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06002829 RID: 10281 RVA: 0x0009B188 File Offset: 0x00099388
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.context.Connection.RemoteEndPoint;
			}
		}

		/// <summary>Gets the request identifier of the incoming HTTP request.</summary>
		/// <returns>A <see cref="T:System.Guid" /> object that contains the identifier of the HTTP request.</returns>
		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x0009B19A File Offset: 0x0009939A
		[MonoTODO("Always returns Guid.Empty")]
		public Guid RequestTraceIdentifier
		{
			get
			{
				return Guid.Empty;
			}
		}

		/// <summary>Gets the <see cref="T:System.Uri" /> object requested by the client.</summary>
		/// <returns>A <see cref="T:System.Uri" /> object that identifies the resource requested by the client.</returns>
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x0600282B RID: 10283 RVA: 0x0009B1A1 File Offset: 0x000993A1
		public Uri Url
		{
			get
			{
				return this.url;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the resource that referred the client to the server.</summary>
		/// <returns>A <see cref="T:System.Uri" /> object that contains the text of the request's <see cref="F:System.Net.HttpRequestHeader.Referer" /> header, or null if the header was not included in the request.</returns>
		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x0600282C RID: 10284 RVA: 0x0009B1A9 File Offset: 0x000993A9
		public Uri UrlReferrer
		{
			get
			{
				return this.referrer;
			}
		}

		/// <summary>Gets the user agent presented by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains the text of the request's User-Agent header.</returns>
		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x0600282D RID: 10285 RVA: 0x0009B1B1 File Offset: 0x000993B1
		public string UserAgent
		{
			get
			{
				return this.headers["user-agent"];
			}
		}

		/// <summary>Gets the server IP address and port number to which the request is directed.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the host address information.</returns>
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x0009B1C3 File Offset: 0x000993C3
		public string UserHostAddress
		{
			get
			{
				return this.LocalEndPoint.ToString();
			}
		}

		/// <summary>Gets the DNS name and, if provided, the port number specified by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the text of the request's Host header.</returns>
		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x0600282F RID: 10287 RVA: 0x0009B1D0 File Offset: 0x000993D0
		public string UserHostName
		{
			get
			{
				return this.headers["host"];
			}
		}

		/// <summary>Gets the natural languages that are preferred for the response.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains the languages specified in the request's <see cref="F:System.Net.HttpRequestHeader.AcceptLanguage" /> header or null if the client request did not include an <see cref="F:System.Net.HttpRequestHeader.AcceptLanguage" /> header.</returns>
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x0009B1E2 File Offset: 0x000993E2
		public string[] UserLanguages
		{
			get
			{
				return this.user_languages;
			}
		}

		/// <summary>Begins an asynchronous request for the client's X.509 v.3 certificate.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that indicates the status of the operation.</returns>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the callback delegate when the operation completes.</param>
		// Token: 0x06002831 RID: 10289 RVA: 0x0009B1EA File Offset: 0x000993EA
		public IAsyncResult BeginGetClientCertificate(AsyncCallback requestCallback, object state)
		{
			if (this.gcc_delegate == null)
			{
				this.gcc_delegate = new HttpListenerRequest.GCCDelegate(this.GetClientCertificate);
			}
			return this.gcc_delegate.BeginInvoke(requestCallback, state);
		}

		/// <summary>Ends an asynchronous request for the client's X.509 v.3 certificate.</summary>
		/// <returns>The <see cref="T:System.IAsyncResult" /> object that is returned when the operation started.</returns>
		/// <param name="asyncResult">The pending request for the certificate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling <see cref="M:System.Net.HttpListenerRequest.BeginGetClientCertificate(System.AsyncCallback,System.Object)" /><paramref name="e." /></exception>
		/// <exception cref="T:System.InvalidOperationException">This method was already called for the operation identified by <paramref name="asyncResult" />. </exception>
		// Token: 0x06002832 RID: 10290 RVA: 0x0009B213 File Offset: 0x00099413
		public X509Certificate2 EndGetClientCertificate(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (this.gcc_delegate == null)
			{
				throw new InvalidOperationException();
			}
			return this.gcc_delegate.EndInvoke(asyncResult);
		}

		/// <summary>Retrieves the client's X.509 v.3 certificate.</summary>
		/// <returns>A <see cref="N:System.Security.Cryptography.X509Certificates" /> object that contains the client's X.509 v.3 certificate.</returns>
		/// <exception cref="T:System.InvalidOperationException">A call to this method to retrieve the client's X.509 v.3 certificate is in progress and therefore another call to this method cannot be made.</exception>
		// Token: 0x06002833 RID: 10291 RVA: 0x0009B23D File Offset: 0x0009943D
		public X509Certificate2 GetClientCertificate()
		{
			return this.context.Connection.ClientCertificate;
		}

		/// <summary>Gets the Service Provider Name (SPN) that the client sent on the request.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the SPN the client sent on the request. </returns>
		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002834 RID: 10292 RVA: 0x00009E57 File Offset: 0x00008057
		[MonoTODO]
		public string ServiceName
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.TransportContext" /> for the client request.</summary>
		/// <returns>A <see cref="T:System.Net.TransportContext" /> object for the client request.</returns>
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x0009B24F File Offset: 0x0009944F
		public TransportContext TransportContext
		{
			get
			{
				return new HttpListenerRequest.Context();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the TCP connection was  a WebSocket request.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the TCP connection is a WebSocket request; otherwise, false.</returns>
		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002836 RID: 10294 RVA: 0x00004240 File Offset: 0x00002440
		[MonoTODO]
		public bool IsWebSocketRequest
		{
			get
			{
				return false;
			}
		}

		/// <summary>Retrieves the client's X.509 v.3 certificate as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="N:System.Security.Cryptography.X509Certificates" /> object that contains the client's X.509 v.3 certificate.</returns>
		// Token: 0x06002837 RID: 10295 RVA: 0x0009B256 File Offset: 0x00099456
		public Task<X509Certificate2> GetClientCertificateAsync()
		{
			return Task<X509Certificate2>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetClientCertificate), new Func<IAsyncResult, X509Certificate2>(this.EndGetClientCertificate), null);
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal HttpListenerRequest()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040021C0 RID: 8640
		private string[] accept_types;

		// Token: 0x040021C1 RID: 8641
		private Encoding content_encoding;

		// Token: 0x040021C2 RID: 8642
		private long content_length;

		// Token: 0x040021C3 RID: 8643
		private bool cl_set;

		// Token: 0x040021C4 RID: 8644
		private CookieCollection cookies;

		// Token: 0x040021C5 RID: 8645
		private WebHeaderCollection headers;

		// Token: 0x040021C6 RID: 8646
		private string method;

		// Token: 0x040021C7 RID: 8647
		private Stream input_stream;

		// Token: 0x040021C8 RID: 8648
		private Version version;

		// Token: 0x040021C9 RID: 8649
		private NameValueCollection query_string;

		// Token: 0x040021CA RID: 8650
		private string raw_url;

		// Token: 0x040021CB RID: 8651
		private Uri url;

		// Token: 0x040021CC RID: 8652
		private Uri referrer;

		// Token: 0x040021CD RID: 8653
		private string[] user_languages;

		// Token: 0x040021CE RID: 8654
		private HttpListenerContext context;

		// Token: 0x040021CF RID: 8655
		private bool is_chunked;

		// Token: 0x040021D0 RID: 8656
		private bool ka_set;

		// Token: 0x040021D1 RID: 8657
		private bool keep_alive;

		// Token: 0x040021D2 RID: 8658
		private HttpListenerRequest.GCCDelegate gcc_delegate;

		// Token: 0x040021D3 RID: 8659
		private static byte[] _100continue = Encoding.ASCII.GetBytes("HTTP/1.1 100 Continue\r\n\r\n");

		// Token: 0x040021D4 RID: 8660
		private static char[] separators = new char[] { ' ' };

		// Token: 0x02000525 RID: 1317
		private class Context : TransportContext
		{
			// Token: 0x0600283A RID: 10298 RVA: 0x00004239 File Offset: 0x00002439
			public override ChannelBinding GetChannelBinding(ChannelBindingKind kind)
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x02000526 RID: 1318
		// (Invoke) Token: 0x0600283D RID: 10301
		private delegate X509Certificate2 GCCDelegate();
	}
}
