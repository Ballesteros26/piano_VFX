using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;
using Unity;

namespace System.Net
{
	/// <summary>Provides an HTTP-specific implementation of the <see cref="T:System.Net.WebResponse" /> class.</summary>
	// Token: 0x02000534 RID: 1332
	[Serializable]
	public class HttpWebResponse : WebResponse, ISerializable, IDisposable
	{
		// Token: 0x06002933 RID: 10547 RVA: 0x0009F21C File Offset: 0x0009D41C
		internal HttpWebResponse(Uri uri, string method, HttpStatusCode status, WebHeaderCollection headers)
		{
			this.uri = uri;
			this.method = method;
			this.statusCode = status;
			this.statusDescription = HttpStatusDescription.Get(status);
			this.webHeaders = headers;
			this.version = HttpVersion.Version10;
			this.contentLength = -1L;
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x0009F26C File Offset: 0x0009D46C
		internal HttpWebResponse(Uri uri, string method, WebResponseStream stream, CookieContainer container)
		{
			this.uri = uri;
			this.method = method;
			this.stream = stream;
			this.webHeaders = stream.Headers ?? new WebHeaderCollection();
			this.version = stream.Version;
			this.statusCode = stream.StatusCode;
			this.statusDescription = stream.StatusDescription ?? HttpStatusDescription.Get(this.statusCode);
			this.contentLength = -1L;
			try
			{
				string text = this.webHeaders["Content-Length"];
				if (string.IsNullOrEmpty(text) || !long.TryParse(text, out this.contentLength))
				{
					this.contentLength = -1L;
				}
			}
			catch (Exception)
			{
				this.contentLength = -1L;
			}
			if (container != null)
			{
				this.cookie_container = container;
				this.FillCookies();
			}
			string text2 = this.webHeaders["Content-Encoding"];
			if (text2 == "gzip" && (stream.Request.AutomaticDecompression & DecompressionMethods.GZip) != DecompressionMethods.None)
			{
				this.stream = new GZipStream(stream, CompressionMode.Decompress);
				this.webHeaders.Remove(HttpRequestHeader.ContentEncoding);
				return;
			}
			if (text2 == "deflate" && (stream.Request.AutomaticDecompression & DecompressionMethods.Deflate) != DecompressionMethods.None)
			{
				this.stream = new DeflateStream(stream, CompressionMode.Decompress);
				this.webHeaders.Remove(HttpRequestHeader.ContentEncoding);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebResponse" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.HttpWebRequest" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.HttpWebRequest" />. </param>
		// Token: 0x06002935 RID: 10549 RVA: 0x0009F3C0 File Offset: 0x0009D5C0
		[Obsolete("Serialization is obsoleted for this type", false)]
		protected HttpWebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.uri = (Uri)serializationInfo.GetValue("uri", typeof(Uri));
			this.contentLength = serializationInfo.GetInt64("contentLength");
			this.contentType = serializationInfo.GetString("contentType");
			this.method = serializationInfo.GetString("method");
			this.statusDescription = serializationInfo.GetString("statusDescription");
			this.cookieCollection = (CookieCollection)serializationInfo.GetValue("cookieCollection", typeof(CookieCollection));
			this.version = (Version)serializationInfo.GetValue("version", typeof(Version));
			this.statusCode = (HttpStatusCode)serializationInfo.GetValue("statusCode", typeof(HttpStatusCode));
		}

		/// <summary>Gets the character set of the response.</summary>
		/// <returns>A string that contains the character set of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x0009F49C File Offset: 0x0009D69C
		public string CharacterSet
		{
			get
			{
				string text = this.ContentType;
				if (text == null)
				{
					return "ISO-8859-1";
				}
				string text2 = text.ToLower();
				int num = text2.IndexOf("charset=", StringComparison.Ordinal);
				if (num == -1)
				{
					return "ISO-8859-1";
				}
				num += 8;
				int num2 = text2.IndexOf(';', num);
				if (num2 != -1)
				{
					return text.Substring(num, num2 - num);
				}
				return text.Substring(num);
			}
		}

		/// <summary>Gets the method that is used to encode the body of the response.</summary>
		/// <returns>A string that describes the method that is used to encode the body of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002937 RID: 10551 RVA: 0x0009F4FC File Offset: 0x0009D6FC
		public string ContentEncoding
		{
			get
			{
				this.CheckDisposed();
				string text = this.webHeaders["Content-Encoding"];
				if (text == null)
				{
					return "";
				}
				return text;
			}
		}

		/// <summary>Gets the length of the content returned by the request.</summary>
		/// <returns>The number of bytes returned by the request. Content length does not include header information.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x0009F52A File Offset: 0x0009D72A
		public override long ContentLength
		{
			get
			{
				return this.contentLength;
			}
		}

		/// <summary>Gets the content type of the response.</summary>
		/// <returns>A string that contains the content type of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002939 RID: 10553 RVA: 0x0009F532 File Offset: 0x0009D732
		public override string ContentType
		{
			get
			{
				this.CheckDisposed();
				if (this.contentType == null)
				{
					this.contentType = this.webHeaders["Content-Type"];
				}
				return this.contentType;
			}
		}

		/// <summary>Gets or sets the cookies that are associated with this response.</summary>
		/// <returns>A <see cref="T:System.Net.CookieCollection" /> that contains the cookies that are associated with this response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x0600293A RID: 10554 RVA: 0x0009F55E File Offset: 0x0009D75E
		// (set) Token: 0x0600293B RID: 10555 RVA: 0x0009F57F File Offset: 0x0009D77F
		public virtual CookieCollection Cookies
		{
			get
			{
				this.CheckDisposed();
				if (this.cookieCollection == null)
				{
					this.cookieCollection = new CookieCollection();
				}
				return this.cookieCollection;
			}
			set
			{
				this.CheckDisposed();
				this.cookieCollection = value;
			}
		}

		/// <summary>Gets the headers that are associated with this response from the server.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the header information returned with the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x0600293C RID: 10556 RVA: 0x0009F58E File Offset: 0x0009D78E
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.webHeaders;
			}
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x00093A0B File Offset: 0x00091C0B
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether both client and server were authenticated.</summary>
		/// <returns>true if mutual authentication occurred; otherwise, false.</returns>
		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x0600293E RID: 10558 RVA: 0x0009F596 File Offset: 0x0009D796
		[MonoTODO]
		public override bool IsMutuallyAuthenticated
		{
			get
			{
				throw HttpWebResponse.GetMustImplement();
			}
		}

		/// <summary>Gets the last date and time that the contents of the response were modified.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the date and time that the contents of the response were modified.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x0600293F RID: 10559 RVA: 0x0009F5A0 File Offset: 0x0009D7A0
		public DateTime LastModified
		{
			get
			{
				this.CheckDisposed();
				DateTime dateTime;
				try
				{
					dateTime = MonoHttpDate.Parse(this.webHeaders["Last-Modified"]);
				}
				catch (Exception)
				{
					dateTime = DateTime.Now;
				}
				return dateTime;
			}
		}

		/// <summary>Gets the method that is used to return the response.</summary>
		/// <returns>A string that contains the HTTP method that is used to return the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x0009F5E8 File Offset: 0x0009D7E8
		public virtual string Method
		{
			get
			{
				this.CheckDisposed();
				return this.method;
			}
		}

		/// <summary>Gets the version of the HTTP protocol that is used in the response.</summary>
		/// <returns>A <see cref="T:System.Version" /> that contains the HTTP protocol version of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002941 RID: 10561 RVA: 0x0009F5F6 File Offset: 0x0009D7F6
		public Version ProtocolVersion
		{
			get
			{
				this.CheckDisposed();
				return this.version;
			}
		}

		/// <summary>Gets the URI of the Internet resource that responded to the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI of the Internet resource that responded to the request.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002942 RID: 10562 RVA: 0x0009F604 File Offset: 0x0009D804
		public override Uri ResponseUri
		{
			get
			{
				this.CheckDisposed();
				return this.uri;
			}
		}

		/// <summary>Gets the name of the server that sent the response.</summary>
		/// <returns>A string that contains the name of the server that sent the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002943 RID: 10563 RVA: 0x0009F612 File Offset: 0x0009D812
		public string Server
		{
			get
			{
				this.CheckDisposed();
				return this.webHeaders["Server"] ?? "";
			}
		}

		/// <summary>Gets the status of the response.</summary>
		/// <returns>One of the <see cref="T:System.Net.HttpStatusCode" /> values.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06002944 RID: 10564 RVA: 0x0009F633 File Offset: 0x0009D833
		public virtual HttpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		/// <summary>Gets the status description returned with the response.</summary>
		/// <returns>A string that describes the status of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002945 RID: 10565 RVA: 0x0009F63B File Offset: 0x0009D83B
		public virtual string StatusDescription
		{
			get
			{
				this.CheckDisposed();
				return this.statusDescription;
			}
		}

		/// <summary>Gets a value that indicates if headers are supported.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if headers are supported; otherwise, false.</returns>
		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool SupportsHeaders
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the contents of a header that was returned with the response.</summary>
		/// <returns>The contents of the specified header.</returns>
		/// <param name="headerName">The header value to return. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x06002947 RID: 10567 RVA: 0x0009F64C File Offset: 0x0009D84C
		public string GetResponseHeader(string headerName)
		{
			this.CheckDisposed();
			string text = this.webHeaders[headerName];
			if (text == null)
			{
				return "";
			}
			return text;
		}

		/// <summary>Gets the stream that is used to read the body of the response from the server.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> containing the body of the response.</returns>
		/// <exception cref="T:System.Net.ProtocolViolationException">There is no response stream. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002948 RID: 10568 RVA: 0x0009F676 File Offset: 0x0009D876
		public override Stream GetResponseStream()
		{
			this.CheckDisposed();
			if (this.stream == null)
			{
				return Stream.Null;
			}
			if (string.Equals(this.method, "HEAD", StringComparison.OrdinalIgnoreCase))
			{
				return Stream.Null;
			}
			return this.stream;
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.HttpWebResponse" /> will be serialized. </param>
		/// <param name="streamingContext">The destination of the serialization. </param>
		// Token: 0x06002949 RID: 10569 RVA: 0x000843F0 File Offset: 0x000825F0
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x0600294A RID: 10570 RVA: 0x0009F6AC File Offset: 0x0009D8AC
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("uri", this.uri);
			serializationInfo.AddValue("contentLength", this.contentLength);
			serializationInfo.AddValue("contentType", this.contentType);
			serializationInfo.AddValue("method", this.method);
			serializationInfo.AddValue("statusDescription", this.statusDescription);
			serializationInfo.AddValue("cookieCollection", this.cookieCollection);
			serializationInfo.AddValue("version", this.version);
			serializationInfo.AddValue("statusCode", this.statusCode);
		}

		/// <summary>Closes the response stream.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600294B RID: 10571 RVA: 0x0009F748 File Offset: 0x0009D948
		public override void Close()
		{
			Stream stream = Interlocked.Exchange<Stream>(ref this.stream, null);
			if (stream != null)
			{
				stream.Close();
			}
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x0009F76B File Offset: 0x0009D96B
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.HttpWebResponse" />, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources. </param>
		// Token: 0x0600294D RID: 10573 RVA: 0x0009F774 File Offset: 0x0009D974
		protected override void Dispose(bool disposing)
		{
			this.disposed = true;
			base.Dispose(true);
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x0009F784 File Offset: 0x0009D984
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x0009F7A0 File Offset: 0x0009D9A0
		private void FillCookies()
		{
			if (this.webHeaders == null)
			{
				return;
			}
			CookieCollection cookieCollection = null;
			try
			{
				string text = this.webHeaders.Get("Set-Cookie");
				if (text != null)
				{
					cookieCollection = this.cookie_container.CookieCutter(this.uri, "Set-Cookie", text, false);
				}
			}
			catch
			{
			}
			try
			{
				string text = this.webHeaders.Get("Set-Cookie2");
				if (text != null)
				{
					CookieCollection cookieCollection2 = this.cookie_container.CookieCutter(this.uri, "Set-Cookie2", text, false);
					if (cookieCollection != null && cookieCollection.Count != 0)
					{
						cookieCollection.Add(cookieCollection2);
					}
					else
					{
						cookieCollection = cookieCollection2;
					}
				}
			}
			catch
			{
			}
			this.cookieCollection = cookieCollection;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebResponse" /> class.</summary>
		// Token: 0x06002950 RID: 10576 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		public HttpWebResponse()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002265 RID: 8805
		private Uri uri;

		// Token: 0x04002266 RID: 8806
		private WebHeaderCollection webHeaders;

		// Token: 0x04002267 RID: 8807
		private CookieCollection cookieCollection;

		// Token: 0x04002268 RID: 8808
		private string method;

		// Token: 0x04002269 RID: 8809
		private Version version;

		// Token: 0x0400226A RID: 8810
		private HttpStatusCode statusCode;

		// Token: 0x0400226B RID: 8811
		private string statusDescription;

		// Token: 0x0400226C RID: 8812
		private long contentLength;

		// Token: 0x0400226D RID: 8813
		private string contentType;

		// Token: 0x0400226E RID: 8814
		private CookieContainer cookie_container;

		// Token: 0x0400226F RID: 8815
		private bool disposed;

		// Token: 0x04002270 RID: 8816
		private Stream stream;
	}
}
