using System;
using System.Globalization;
using System.IO;
using System.Text;
using Unity;

namespace System.Net
{
	/// <summary>Represents a response to a request being handled by an <see cref="T:System.Net.HttpListener" /> object.</summary>
	// Token: 0x02000527 RID: 1319
	public sealed class HttpListenerResponse : IDisposable
	{
		// Token: 0x06002840 RID: 10304 RVA: 0x0009B2AC File Offset: 0x000994AC
		internal HttpListenerResponse(HttpListenerContext context)
		{
			this.headers = new WebHeaderCollection();
			this.keep_alive = true;
			this.version = HttpVersion.Version11;
			this.status_code = 200;
			this.status_description = "OK";
			this.headers_lock = new object();
			base..ctor();
			this.context = context;
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002841 RID: 10305 RVA: 0x0009B304 File Offset: 0x00099504
		internal bool ForceCloseChunked
		{
			get
			{
				return this.force_close_chunked;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Text.Encoding" /> for this response's <see cref="P:System.Net.HttpListenerResponse.OutputStream" />.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object suitable for use with the data in the <see cref="P:System.Net.HttpListenerResponse.OutputStream" /> property, or null if no encoding is specified.</returns>
		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x0009B30C File Offset: 0x0009950C
		// (set) Token: 0x06002843 RID: 10307 RVA: 0x0009B327 File Offset: 0x00099527
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
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.content_encoding = value;
			}
		}

		/// <summary>Gets or sets the number of bytes in the body data included in the response.</summary>
		/// <returns>The value of the response's Content-Length header.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The response is already being sent.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06002844 RID: 10308 RVA: 0x0009B35C File Offset: 0x0009955C
		// (set) Token: 0x06002845 RID: 10309 RVA: 0x0009B364 File Offset: 0x00099564
		public long ContentLength64
		{
			get
			{
				return this.content_length;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("Must be >= 0", "value");
				}
				this.cl_set = true;
				this.content_length = value;
			}
		}

		/// <summary>Gets or sets the MIME type of the content returned.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the text of the response's Content-Type header.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is an empty string ("").</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06002846 RID: 10310 RVA: 0x0009B3C0 File Offset: 0x000995C0
		// (set) Token: 0x06002847 RID: 10311 RVA: 0x0009B3C8 File Offset: 0x000995C8
		public string ContentType
		{
			get
			{
				return this.content_type;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.content_type = value;
			}
		}

		/// <summary>Gets or sets the collection of cookies returned with the response.</summary>
		/// <returns>A <see cref="T:System.Net.CookieCollection" /> that contains cookies to accompany the response. The collection is empty if no cookies have been added to the response.</returns>
		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06002848 RID: 10312 RVA: 0x0009B3FD File Offset: 0x000995FD
		// (set) Token: 0x06002849 RID: 10313 RVA: 0x0009B418 File Offset: 0x00099618
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
			set
			{
				this.cookies = value;
			}
		}

		/// <summary>Gets or sets the collection of header name/value pairs returned by the server.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> instance that contains all the explicitly set HTTP headers to be included in the response.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.WebHeaderCollection" /> instance specified for a set operation is not valid for a response.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x0600284A RID: 10314 RVA: 0x0009B421 File Offset: 0x00099621
		// (set) Token: 0x0600284B RID: 10315 RVA: 0x0009B429 File Offset: 0x00099629
		public WebHeaderCollection Headers
		{
			get
			{
				return this.headers;
			}
			set
			{
				this.headers = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the server requests a persistent connection.</summary>
		/// <returns>true if the server requests a persistent connection; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x0600284C RID: 10316 RVA: 0x0009B432 File Offset: 0x00099632
		// (set) Token: 0x0600284D RID: 10317 RVA: 0x0009B43A File Offset: 0x0009963A
		public bool KeepAlive
		{
			get
			{
				return this.keep_alive;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.keep_alive = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object to which a response can be written.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object to which a response can be written.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x0600284E RID: 10318 RVA: 0x0009B46F File Offset: 0x0009966F
		public Stream OutputStream
		{
			get
			{
				if (this.output_stream == null)
				{
					this.output_stream = this.context.Connection.GetResponseStream();
				}
				return this.output_stream;
			}
		}

		/// <summary>Gets or sets the HTTP version used for the response.</summary>
		/// <returns>A <see cref="T:System.Version" /> object indicating the version of HTTP used when responding to the client. Note that this property is now obsolete.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation does not have its <see cref="P:System.Version.Major" /> property set to 1 or does not have its <see cref="P:System.Version.Minor" /> property set to either 0 or 1.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x0009B495 File Offset: 0x00099695
		// (set) Token: 0x06002850 RID: 10320 RVA: 0x0009B4A0 File Offset: 0x000996A0
		public Version ProtocolVersion
		{
			get
			{
				return this.version;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Major != 1 || (value.Minor != 0 && value.Minor != 1))
				{
					throw new ArgumentException("Must be 1.0 or 1.1", "value");
				}
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				this.version = value;
			}
		}

		/// <summary>Gets or sets the value of the HTTP Location header in this response.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the absolute URL to be sent to the client in the Location header. </returns>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is an empty string ("").</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002851 RID: 10321 RVA: 0x0009B537 File Offset: 0x00099737
		// (set) Token: 0x06002852 RID: 10322 RVA: 0x0009B53F File Offset: 0x0009973F
		public string RedirectLocation
		{
			get
			{
				return this.location;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.location = value;
			}
		}

		/// <summary>Gets or sets whether the response uses chunked transfer encoding.</summary>
		/// <returns>true if the response is set to use chunked transfer encoding; otherwise, false. The default is false.</returns>
		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002853 RID: 10323 RVA: 0x0009B574 File Offset: 0x00099774
		// (set) Token: 0x06002854 RID: 10324 RVA: 0x0009B57C File Offset: 0x0009977C
		public bool SendChunked
		{
			get
			{
				return this.chunked;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				this.chunked = value;
			}
		}

		/// <summary>Gets or sets the HTTP status code to be returned to the client.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that specifies the HTTP status code for the requested resource. The default is <see cref="F:System.Net.HttpStatusCode.OK" />, indicating that the server successfully processed the client's request and included the requested resource in the response body.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">The value specified for a set operation is not valid. Valid values are between 100 and 999 inclusive.</exception>
		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x0009B5B1 File Offset: 0x000997B1
		// (set) Token: 0x06002856 RID: 10326 RVA: 0x0009B5BC File Offset: 0x000997BC
		public int StatusCode
		{
			get
			{
				return this.status_code;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				if (this.HeadersSent)
				{
					throw new InvalidOperationException("Cannot be changed after headers are sent.");
				}
				if (value < 100 || value > 999)
				{
					throw new ProtocolViolationException("StatusCode must be between 100 and 999.");
				}
				this.status_code = value;
				this.status_description = HttpStatusDescription.Get(value);
			}
		}

		/// <summary>Gets or sets a text description of the HTTP status code returned to the client.</summary>
		/// <returns>The text description of the HTTP status code returned to the client. The default is the RFC 2616 description for the <see cref="P:System.Net.HttpListenerResponse.StatusCode" /> property value, or an empty string ("") if an RFC 2616 description does not exist.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation contains non-printable characters.</exception>
		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x0009B620 File Offset: 0x00099820
		// (set) Token: 0x06002858 RID: 10328 RVA: 0x0009B628 File Offset: 0x00099828
		public string StatusDescription
		{
			get
			{
				return this.status_description;
			}
			set
			{
				this.status_description = value;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.HttpListenerResponse" />.</summary>
		// Token: 0x06002859 RID: 10329 RVA: 0x0009B631 File Offset: 0x00099831
		void IDisposable.Dispose()
		{
			this.Close(true);
		}

		/// <summary>Closes the connection to the client without sending a response.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600285A RID: 10330 RVA: 0x0009B63A File Offset: 0x0009983A
		public void Abort()
		{
			if (this.disposed)
			{
				return;
			}
			this.Close(true);
		}

		/// <summary>Adds the specified header and value to the HTTP headers for this response.</summary>
		/// <param name="name">The name of the HTTP header to set.</param>
		/// <param name="value">The value for the <paramref name="name" /> header.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null or an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentException">You are not allowed to specify a value for the specified header.-or-<paramref name="name" /> or <paramref name="value" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65,535 characters.</exception>
		// Token: 0x0600285B RID: 10331 RVA: 0x0009B64C File Offset: 0x0009984C
		public void AddHeader(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == "")
			{
				throw new ArgumentException("'name' cannot be empty", "name");
			}
			if (value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			this.headers.Set(name, value);
		}

		/// <summary>Adds the specified <see cref="T:System.Net.Cookie" /> to the collection of cookies for this response.</summary>
		/// <param name="cookie">The <see cref="T:System.Net.Cookie" /> to add to the collection to be sent with this response</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookie" /> is null.</exception>
		// Token: 0x0600285C RID: 10332 RVA: 0x0009B6A9 File Offset: 0x000998A9
		public void AppendCookie(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			this.Cookies.Add(cookie);
		}

		/// <summary>Appends a value to the specified HTTP header to be sent with this response.</summary>
		/// <param name="name">The name of the HTTP header to append <paramref name="value" /> to.</param>
		/// <param name="value">The value to append to the <paramref name="name" /> header.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is null or an empty string ("").-or-You are not allowed to specify a value for the specified header.-or-<paramref name="name" /> or <paramref name="value" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65,535 characters.</exception>
		// Token: 0x0600285D RID: 10333 RVA: 0x0009B6C8 File Offset: 0x000998C8
		public void AppendHeader(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == "")
			{
				throw new ArgumentException("'name' cannot be empty", "name");
			}
			if (value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			this.headers.Add(name, value);
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x0009B725 File Offset: 0x00099925
		private void Close(bool force)
		{
			this.disposed = true;
			this.context.Connection.Close(force);
		}

		/// <summary>Sends the response to the client and releases the resources held by this <see cref="T:System.Net.HttpListenerResponse" /> instance.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600285F RID: 10335 RVA: 0x0009B73F File Offset: 0x0009993F
		public void Close()
		{
			if (this.disposed)
			{
				return;
			}
			this.Close(false);
		}

		/// <summary>Returns the specified byte array to the client and releases the resources held by this <see cref="T:System.Net.HttpListenerResponse" /> instance.</summary>
		/// <param name="responseEntity">A <see cref="T:System.Byte" /> array that contains the response to send to the client.</param>
		/// <param name="willBlock">true to block execution while flushing the stream to the client; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="responseEntity" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002860 RID: 10336 RVA: 0x0009B751 File Offset: 0x00099951
		public void Close(byte[] responseEntity, bool willBlock)
		{
			if (this.disposed)
			{
				return;
			}
			if (responseEntity == null)
			{
				throw new ArgumentNullException("responseEntity");
			}
			this.ContentLength64 = (long)responseEntity.Length;
			this.OutputStream.Write(responseEntity, 0, (int)this.content_length);
			this.Close(false);
		}

		/// <summary>Copies properties from the specified <see cref="T:System.Net.HttpListenerResponse" /> to this response.</summary>
		/// <param name="templateResponse">The <see cref="T:System.Net.HttpListenerResponse" /> instance to copy.</param>
		// Token: 0x06002861 RID: 10337 RVA: 0x0009B790 File Offset: 0x00099990
		public void CopyFrom(HttpListenerResponse templateResponse)
		{
			this.headers.Clear();
			this.headers.Add(templateResponse.headers);
			this.content_length = templateResponse.content_length;
			this.status_code = templateResponse.status_code;
			this.status_description = templateResponse.status_description;
			this.keep_alive = templateResponse.keep_alive;
			this.version = templateResponse.version;
		}

		/// <summary>Configures the response to redirect the client to the specified URL.</summary>
		/// <param name="url">The URL that the client should use to locate the requested resource.</param>
		// Token: 0x06002862 RID: 10338 RVA: 0x0009B7F5 File Offset: 0x000999F5
		public void Redirect(string url)
		{
			this.StatusCode = 302;
			this.location = url;
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x0009B80C File Offset: 0x00099A0C
		private bool FindCookie(Cookie cookie)
		{
			string name = cookie.Name;
			string domain = cookie.Domain;
			string path = cookie.Path;
			foreach (object obj in this.cookies)
			{
				Cookie cookie2 = (Cookie)obj;
				if (!(name != cookie2.Name) && !(domain != cookie2.Domain) && path == cookie2.Path)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x0009B8B0 File Offset: 0x00099AB0
		internal void SendHeaders(bool closing, MemoryStream ms)
		{
			Encoding @default = this.content_encoding;
			if (@default == null)
			{
				@default = Encoding.Default;
			}
			if (this.content_type != null)
			{
				if (this.content_encoding != null && this.content_type.IndexOf("charset=", StringComparison.Ordinal) == -1)
				{
					string webName = this.content_encoding.WebName;
					this.headers.SetInternal("Content-Type", this.content_type + "; charset=" + webName);
				}
				else
				{
					this.headers.SetInternal("Content-Type", this.content_type);
				}
			}
			if (this.headers["Server"] == null)
			{
				this.headers.SetInternal("Server", "Mono-HTTPAPI/1.0");
			}
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			if (this.headers["Date"] == null)
			{
				this.headers.SetInternal("Date", DateTime.UtcNow.ToString("r", invariantCulture));
			}
			if (!this.chunked)
			{
				if (!this.cl_set && closing)
				{
					this.cl_set = true;
					this.content_length = 0L;
				}
				if (this.cl_set)
				{
					this.headers.SetInternal("Content-Length", this.content_length.ToString(invariantCulture));
				}
			}
			Version protocolVersion = this.context.Request.ProtocolVersion;
			if (!this.cl_set && !this.chunked && protocolVersion >= HttpVersion.Version11)
			{
				this.chunked = true;
			}
			bool flag = this.status_code == 400 || this.status_code == 408 || this.status_code == 411 || this.status_code == 413 || this.status_code == 414 || this.status_code == 500 || this.status_code == 503;
			if (!flag)
			{
				flag = !this.context.Request.KeepAlive;
			}
			if (!this.keep_alive || flag)
			{
				this.headers.SetInternal("Connection", "close");
				flag = true;
			}
			if (this.chunked)
			{
				this.headers.SetInternal("Transfer-Encoding", "chunked");
			}
			int reuses = this.context.Connection.Reuses;
			if (reuses >= 100)
			{
				this.force_close_chunked = true;
				if (!flag)
				{
					this.headers.SetInternal("Connection", "close");
					flag = true;
				}
			}
			if (!flag)
			{
				this.headers.SetInternal("Keep-Alive", string.Format("timeout=15,max={0}", 100 - reuses));
				if (this.context.Request.ProtocolVersion <= HttpVersion.Version10)
				{
					this.headers.SetInternal("Connection", "keep-alive");
				}
			}
			if (this.location != null)
			{
				this.headers.SetInternal("Location", this.location);
			}
			if (this.cookies != null)
			{
				foreach (object obj in this.cookies)
				{
					Cookie cookie = (Cookie)obj;
					this.headers.SetInternal("Set-Cookie", HttpListenerResponse.CookieToClientString(cookie));
				}
			}
			StreamWriter streamWriter = new StreamWriter(ms, @default, 256);
			streamWriter.Write("HTTP/{0} {1} {2}\r\n", this.version, this.status_code, this.status_description);
			string text = HttpListenerResponse.FormatHeaders(this.headers);
			streamWriter.Write(text);
			streamWriter.Flush();
			int num = @default.GetPreamble().Length;
			if (this.output_stream == null)
			{
				this.output_stream = this.context.Connection.GetResponseStream();
			}
			ms.Position = (long)num;
			this.HeadersSent = true;
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x0009BC6C File Offset: 0x00099E6C
		private static string FormatHeaders(WebHeaderCollection headers)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < headers.Count; i++)
			{
				string key = headers.GetKey(i);
				if (WebHeaderCollection.AllowMultiValues(key))
				{
					foreach (string text in headers.GetValues(i))
					{
						stringBuilder.Append(key).Append(": ").Append(text)
							.Append("\r\n");
					}
				}
				else
				{
					stringBuilder.Append(key).Append(": ").Append(headers.Get(i))
						.Append("\r\n");
				}
			}
			return stringBuilder.Append("\r\n").ToString();
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x0009BD24 File Offset: 0x00099F24
		private static string CookieToClientString(Cookie cookie)
		{
			if (cookie.Name.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(64);
			if (cookie.Version > 0)
			{
				stringBuilder.Append("Version=").Append(cookie.Version).Append(";");
			}
			stringBuilder.Append(cookie.Name).Append("=").Append(cookie.Value);
			if (cookie.Path != null && cookie.Path.Length != 0)
			{
				stringBuilder.Append(";Path=").Append(HttpListenerResponse.QuotedString(cookie, cookie.Path));
			}
			if (cookie.Domain != null && cookie.Domain.Length != 0)
			{
				stringBuilder.Append(";Domain=").Append(HttpListenerResponse.QuotedString(cookie, cookie.Domain));
			}
			if (cookie.Port != null && cookie.Port.Length != 0)
			{
				stringBuilder.Append(";Port=").Append(cookie.Port);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x0009BE2E File Offset: 0x0009A02E
		private static string QuotedString(Cookie cookie, string value)
		{
			if (cookie.Version == 0 || HttpListenerResponse.IsToken(value))
			{
				return value;
			}
			return "\"" + value.Replace("\"", "\\\"") + "\"";
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x0009BE64 File Offset: 0x0009A064
		private static bool IsToken(string value)
		{
			int length = value.Length;
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				if (c < ' ' || c >= '\u007f' || HttpListenerResponse.tspecials.IndexOf(c) != -1)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Adds or updates a <see cref="T:System.Net.Cookie" /> in the collection of cookies sent with this response. </summary>
		/// <param name="cookie">A <see cref="T:System.Net.Cookie" /> for this response.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookie" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The cookie already exists in the collection and could not be replaced.</exception>
		// Token: 0x06002869 RID: 10345 RVA: 0x0009BEA8 File Offset: 0x0009A0A8
		public void SetCookie(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			if (this.cookies != null)
			{
				if (this.FindCookie(cookie))
				{
					throw new ArgumentException("The cookie already exists.");
				}
			}
			else
			{
				this.cookies = new CookieCollection();
			}
			this.cookies.Add(cookie);
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal HttpListenerResponse()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040021D5 RID: 8661
		private bool disposed;

		// Token: 0x040021D6 RID: 8662
		private Encoding content_encoding;

		// Token: 0x040021D7 RID: 8663
		private long content_length;

		// Token: 0x040021D8 RID: 8664
		private bool cl_set;

		// Token: 0x040021D9 RID: 8665
		private string content_type;

		// Token: 0x040021DA RID: 8666
		private CookieCollection cookies;

		// Token: 0x040021DB RID: 8667
		private WebHeaderCollection headers;

		// Token: 0x040021DC RID: 8668
		private bool keep_alive;

		// Token: 0x040021DD RID: 8669
		private ResponseStream output_stream;

		// Token: 0x040021DE RID: 8670
		private Version version;

		// Token: 0x040021DF RID: 8671
		private string location;

		// Token: 0x040021E0 RID: 8672
		private int status_code;

		// Token: 0x040021E1 RID: 8673
		private string status_description;

		// Token: 0x040021E2 RID: 8674
		private bool chunked;

		// Token: 0x040021E3 RID: 8675
		private HttpListenerContext context;

		// Token: 0x040021E4 RID: 8676
		internal bool HeadersSent;

		// Token: 0x040021E5 RID: 8677
		internal object headers_lock;

		// Token: 0x040021E6 RID: 8678
		private bool force_close_chunked;

		// Token: 0x040021E7 RID: 8679
		private static string tspecials = "()<>@,;:\\\"/[]?={} \t";
	}
}
