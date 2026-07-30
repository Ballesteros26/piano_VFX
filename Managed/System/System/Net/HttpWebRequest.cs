using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net.Cache;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mono.Security.Interface;
using Unity;

namespace System.Net
{
	/// <summary>Provides an HTTP-specific implementation of the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x0200052B RID: 1323
	[Serializable]
	public class HttpWebRequest : WebRequest, ISerializable
	{
		// Token: 0x06002882 RID: 10370 RVA: 0x0009C060 File Offset: 0x0009A260
		static HttpWebRequest()
		{
			NetConfig netConfig = ConfigurationSettings.GetConfig("system.net/settings") as NetConfig;
			if (netConfig != null)
			{
				int num = netConfig.MaxResponseHeadersLength;
				if (num != -1)
				{
					num *= 64;
				}
				HttpWebRequest.defaultMaxResponseHeadersLength = num;
			}
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x0009C0A0 File Offset: 0x0009A2A0
		internal HttpWebRequest(Uri uri)
		{
			this.allowAutoRedirect = true;
			this.allowBuffering = true;
			this.contentLength = -1L;
			this.keepAlive = true;
			this.maxAutoRedirect = 50;
			this.mediaType = string.Empty;
			this.method = "GET";
			this.initialMethod = "GET";
			this.pipelined = true;
			this.version = HttpVersion.Version11;
			this.timeout = 100000;
			this.locker = new object();
			this.readWriteTimeout = 300000;
			base..ctor();
			this.requestUri = uri;
			this.actualUri = uri;
			this.proxy = WebRequest.InternalDefaultWebProxy;
			this.webHeaders = new WebHeaderCollection(WebHeaderCollectionType.HttpWebRequest);
			this.ThrowOnError = true;
			this.ResetAuthorization();
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x0009C15E File Offset: 0x0009A35E
		internal HttpWebRequest(Uri uri, MonoTlsProvider tlsProvider, MonoTlsSettings settings = null)
			: this(uri)
		{
			this.tlsProvider = tlsProvider;
			this.tlsSettings = settings;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebRequest" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the new <see cref="T:System.Net.HttpWebRequest" /> object. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the new <see cref="T:System.Net.HttpWebRequest" /> object. </param>
		// Token: 0x06002885 RID: 10373 RVA: 0x0009C178 File Offset: 0x0009A378
		[Obsolete("Serialization is obsoleted for this type", false)]
		protected HttpWebRequest(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.allowAutoRedirect = true;
			this.allowBuffering = true;
			this.contentLength = -1L;
			this.keepAlive = true;
			this.maxAutoRedirect = 50;
			this.mediaType = string.Empty;
			this.method = "GET";
			this.initialMethod = "GET";
			this.pipelined = true;
			this.version = HttpVersion.Version11;
			this.timeout = 100000;
			this.locker = new object();
			this.readWriteTimeout = 300000;
			base..ctor();
			this.requestUri = (Uri)serializationInfo.GetValue("requestUri", typeof(Uri));
			this.actualUri = (Uri)serializationInfo.GetValue("actualUri", typeof(Uri));
			this.allowAutoRedirect = serializationInfo.GetBoolean("allowAutoRedirect");
			this.allowBuffering = serializationInfo.GetBoolean("allowBuffering");
			this.certificates = (X509CertificateCollection)serializationInfo.GetValue("certificates", typeof(X509CertificateCollection));
			this.connectionGroup = serializationInfo.GetString("connectionGroup");
			this.contentLength = serializationInfo.GetInt64("contentLength");
			this.webHeaders = (WebHeaderCollection)serializationInfo.GetValue("webHeaders", typeof(WebHeaderCollection));
			this.keepAlive = serializationInfo.GetBoolean("keepAlive");
			this.maxAutoRedirect = serializationInfo.GetInt32("maxAutoRedirect");
			this.mediaType = serializationInfo.GetString("mediaType");
			this.method = serializationInfo.GetString("method");
			this.initialMethod = serializationInfo.GetString("initialMethod");
			this.pipelined = serializationInfo.GetBoolean("pipelined");
			this.version = (Version)serializationInfo.GetValue("version", typeof(Version));
			this.proxy = (IWebProxy)serializationInfo.GetValue("proxy", typeof(IWebProxy));
			this.sendChunked = serializationInfo.GetBoolean("sendChunked");
			this.timeout = serializationInfo.GetInt32("timeout");
			this.redirects = serializationInfo.GetInt32("redirects");
			this.host = serializationInfo.GetString("host");
			this.ResetAuthorization();
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x0009C3BA File Offset: 0x0009A5BA
		private void ResetAuthorization()
		{
			this.auth_state = new HttpWebRequest.AuthorizationState(this, false);
			this.proxy_auth_state = new HttpWebRequest.AuthorizationState(this, true);
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x0009C3D6 File Offset: 0x0009A5D6
		private void SetSpecialHeaders(string HeaderName, string value)
		{
			value = WebHeaderCollection.CheckBadChars(value, true);
			this.webHeaders.RemoveInternal(HeaderName);
			if (value.Length != 0)
			{
				this.webHeaders.AddInternal(HeaderName, value);
			}
		}

		/// <summary>Gets or sets the value of the Accept HTTP header.</summary>
		/// <returns>The value of the Accept HTTP header. The default value is null.</returns>
		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002888 RID: 10376 RVA: 0x0009C402 File Offset: 0x0009A602
		// (set) Token: 0x06002889 RID: 10377 RVA: 0x0009C414 File Offset: 0x0009A614
		public string Accept
		{
			get
			{
				return this.webHeaders["Accept"];
			}
			set
			{
				this.CheckRequestStarted();
				this.SetSpecialHeaders("Accept", value);
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the Internet resource that actually responds to the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that identifies the Internet resource that actually responds to the request. The default is the URI used by the <see cref="M:System.Net.WebRequest.Create(System.String)" /> method to initialize the request.</returns>
		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x0600288A RID: 10378 RVA: 0x0009C428 File Offset: 0x0009A628
		// (set) Token: 0x0600288B RID: 10379 RVA: 0x0009C430 File Offset: 0x0009A630
		public Uri Address
		{
			get
			{
				return this.actualUri;
			}
			internal set
			{
				this.actualUri = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the request should follow redirection responses.</summary>
		/// <returns>true if the request should automatically follow redirection responses from the Internet resource; otherwise, false. The default value is true.</returns>
		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x0600288C RID: 10380 RVA: 0x0009C439 File Offset: 0x0009A639
		// (set) Token: 0x0600288D RID: 10381 RVA: 0x0009C441 File Offset: 0x0009A641
		public virtual bool AllowAutoRedirect
		{
			get
			{
				return this.allowAutoRedirect;
			}
			set
			{
				this.allowAutoRedirect = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to buffer the data sent to the Internet resource.</summary>
		/// <returns>true to enable buffering of the data sent to the Internet resource; false to disable buffering. The default is true.</returns>
		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x0600288E RID: 10382 RVA: 0x0009C44A File Offset: 0x0009A64A
		// (set) Token: 0x0600288F RID: 10383 RVA: 0x0009C452 File Offset: 0x0009A652
		public virtual bool AllowWriteStreamBuffering
		{
			get
			{
				return this.allowBuffering;
			}
			set
			{
				this.allowBuffering = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to buffer the received from the  Internet resource.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true to enable buffering of the data received from the Internet resource; false to disable buffering. The default is true.</returns>
		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002890 RID: 10384 RVA: 0x00004240 File Offset: 0x00002440
		// (set) Token: 0x06002891 RID: 10385 RVA: 0x0009C45B File Offset: 0x0009A65B
		public virtual bool AllowReadStreamBuffering
		{
			get
			{
				return false;
			}
			set
			{
				if (value)
				{
					throw new InvalidOperationException();
				}
			}
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x00093A0B File Offset: 0x00091C0B
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets or sets the type of decompression that is used.</summary>
		/// <returns>A T:System.Net.DecompressionMethods object that indicates the type of decompression that is used. </returns>
		/// <exception cref="T:System.InvalidOperationException">The object's current state does not allow this property to be set.</exception>
		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002893 RID: 10387 RVA: 0x0009C466 File Offset: 0x0009A666
		// (set) Token: 0x06002894 RID: 10388 RVA: 0x0009C46E File Offset: 0x0009A66E
		public DecompressionMethods AutomaticDecompression
		{
			get
			{
				return this.auto_decomp;
			}
			set
			{
				this.CheckRequestStarted();
				this.auto_decomp = value;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002895 RID: 10389 RVA: 0x0009C47D File Offset: 0x0009A67D
		internal bool InternalAllowBuffering
		{
			get
			{
				return this.allowBuffering && this.MethodWithBuffer;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x0009C490 File Offset: 0x0009A690
		private bool MethodWithBuffer
		{
			get
			{
				return this.method != "HEAD" && this.method != "GET" && this.method != "MKCOL" && this.method != "CONNECT" && this.method != "TRACE";
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06002897 RID: 10391 RVA: 0x0009C4F7 File Offset: 0x0009A6F7
		internal MonoTlsProvider TlsProvider
		{
			get
			{
				return this.tlsProvider;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x0009C4FF File Offset: 0x0009A6FF
		internal MonoTlsSettings TlsSettings
		{
			get
			{
				return this.tlsSettings;
			}
		}

		/// <summary>Gets or sets the collection of security certificates that are associated with this request.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> that contains the security certificates associated with this request.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null. </exception>
		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002899 RID: 10393 RVA: 0x0009C507 File Offset: 0x0009A707
		// (set) Token: 0x0600289A RID: 10394 RVA: 0x0009C522 File Offset: 0x0009A722
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this.certificates == null)
				{
					this.certificates = new X509CertificateCollection();
				}
				return this.certificates;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.certificates = value;
			}
		}

		/// <summary>Gets or sets the value of the Connection HTTP header.</summary>
		/// <returns>The value of the Connection HTTP header. The default value is null.</returns>
		/// <exception cref="T:System.ArgumentException">The value of <see cref="P:System.Net.HttpWebRequest.Connection" /> is set to Keep-alive or Close. </exception>
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x0600289B RID: 10395 RVA: 0x0009C539 File Offset: 0x0009A739
		// (set) Token: 0x0600289C RID: 10396 RVA: 0x0009C54C File Offset: 0x0009A74C
		public string Connection
		{
			get
			{
				return this.webHeaders["Connection"];
			}
			set
			{
				this.CheckRequestStarted();
				if (string.IsNullOrEmpty(value))
				{
					this.webHeaders.RemoveInternal("Connection");
					return;
				}
				string text = value.ToLowerInvariant();
				if (text.Contains("keep-alive") || text.Contains("close"))
				{
					throw new ArgumentException("Keep-Alive and Close may not be set with this property");
				}
				if (this.keepAlive)
				{
					value += ", Keep-Alive";
				}
				this.webHeaders.CheckUpdate("Connection", value);
			}
		}

		/// <summary>Gets or sets the name of the connection group for the request.</summary>
		/// <returns>The name of the connection group for this request. The default value is null.</returns>
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x0600289D RID: 10397 RVA: 0x0009C5CA File Offset: 0x0009A7CA
		// (set) Token: 0x0600289E RID: 10398 RVA: 0x0009C5D2 File Offset: 0x0009A7D2
		public override string ConnectionGroupName
		{
			get
			{
				return this.connectionGroup;
			}
			set
			{
				this.connectionGroup = value;
			}
		}

		/// <summary>Gets or sets the Content-length HTTP header.</summary>
		/// <returns>The number of bytes of data to send to the Internet resource. The default is -1, which indicates the property has not been set and that there is no request data to send.</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has been started by calling the <see cref="M:System.Net.HttpWebRequest.GetRequestStream" />, <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />, <see cref="M:System.Net.HttpWebRequest.GetResponse" />, or <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The new <see cref="P:System.Net.HttpWebRequest.ContentLength" /> value is less than 0. </exception>
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x0600289F RID: 10399 RVA: 0x0009C5DB File Offset: 0x0009A7DB
		// (set) Token: 0x060028A0 RID: 10400 RVA: 0x0009C5E3 File Offset: 0x0009A7E3
		public override long ContentLength
		{
			get
			{
				return this.contentLength;
			}
			set
			{
				this.CheckRequestStarted();
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Content-Length must be >= 0");
				}
				this.contentLength = value;
				this.haveContentLength = true;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (set) Token: 0x060028A1 RID: 10401 RVA: 0x0009C60E File Offset: 0x0009A80E
		internal long InternalContentLength
		{
			set
			{
				this.contentLength = value;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060028A2 RID: 10402 RVA: 0x0009C617 File Offset: 0x0009A817
		// (set) Token: 0x060028A3 RID: 10403 RVA: 0x0009C61F File Offset: 0x0009A81F
		internal bool ThrowOnError { get; set; }

		/// <summary>Gets or sets the value of the Content-type HTTP header.</summary>
		/// <returns>The value of the Content-type HTTP header. The default value is null.</returns>
		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060028A4 RID: 10404 RVA: 0x0009C628 File Offset: 0x0009A828
		// (set) Token: 0x060028A5 RID: 10405 RVA: 0x0009C63A File Offset: 0x0009A83A
		public override string ContentType
		{
			get
			{
				return this.webHeaders["Content-Type"];
			}
			set
			{
				this.SetSpecialHeaders("Content-Type", value);
			}
		}

		/// <summary>Gets or sets the delegate method called when an HTTP 100-continue response is received from the Internet resource.</summary>
		/// <returns>A delegate that implements the callback method that executes when an HTTP Continue response is returned from the Internet resource. The default value is null.</returns>
		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060028A6 RID: 10406 RVA: 0x0009C648 File Offset: 0x0009A848
		// (set) Token: 0x060028A7 RID: 10407 RVA: 0x0009C650 File Offset: 0x0009A850
		public HttpContinueDelegate ContinueDelegate
		{
			get
			{
				return this.continueDelegate;
			}
			set
			{
				this.continueDelegate = value;
			}
		}

		/// <summary>Gets or sets the cookies associated with the request.</summary>
		/// <returns>A <see cref="T:System.Net.CookieContainer" /> that contains the cookies associated with this request.</returns>
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060028A8 RID: 10408 RVA: 0x0009C659 File Offset: 0x0009A859
		// (set) Token: 0x060028A9 RID: 10409 RVA: 0x0009C661 File Offset: 0x0009A861
		public virtual CookieContainer CookieContainer
		{
			get
			{
				return this.cookieContainer;
			}
			set
			{
				this.cookieContainer = value;
			}
		}

		/// <summary>Gets or sets authentication information for the request.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> that contains the authentication credentials associated with the request. The default is null.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x0009C66A File Offset: 0x0009A86A
		// (set) Token: 0x060028AB RID: 10411 RVA: 0x0009C672 File Offset: 0x0009A872
		public override ICredentials Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.credentials = value;
			}
		}

		/// <summary>Get or set the Date HTTP header value to use in an HTTP request.</summary>
		/// <returns>The Date header value in the HTTP request.</returns>
		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060028AC RID: 10412 RVA: 0x0009C67C File Offset: 0x0009A87C
		// (set) Token: 0x060028AD RID: 10413 RVA: 0x0009C6BB File Offset: 0x0009A8BB
		public DateTime Date
		{
			get
			{
				string text = this.webHeaders["Date"];
				if (text == null)
				{
					return DateTime.MinValue;
				}
				return DateTime.ParseExact(text, "r", CultureInfo.InvariantCulture).ToLocalTime();
			}
			set
			{
				this.SetDateHeaderHelper("Date", value);
			}
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x0009C6C9 File Offset: 0x0009A8C9
		private void SetDateHeaderHelper(string headerName, DateTime dateTime)
		{
			if (dateTime == DateTime.MinValue)
			{
				this.SetSpecialHeaders(headerName, null);
				return;
			}
			this.SetSpecialHeaders(headerName, HttpProtocolUtils.date2string(dateTime));
		}

		/// <summary>Gets or sets the default cache policy for this request.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> that specifies the cache policy in effect for this request when no other policy is applicable.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x060028AF RID: 10415 RVA: 0x0009C6EE File Offset: 0x0009A8EE
		// (set) Token: 0x060028B0 RID: 10416 RVA: 0x0009C6EE File Offset: 0x0009A8EE
		[MonoTODO]
		public new static RequestCachePolicy DefaultCachePolicy
		{
			get
			{
				throw HttpWebRequest.GetMustImplement();
			}
			set
			{
				throw HttpWebRequest.GetMustImplement();
			}
		}

		/// <summary>Gets or sets the default maximum length of an HTTP error response.</summary>
		/// <returns>An integer that represents the default maximum length of an HTTP error response.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0 and is not equal to -1. </exception>
		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x0009C6EE File Offset: 0x0009A8EE
		// (set) Token: 0x060028B2 RID: 10418 RVA: 0x0009C6EE File Offset: 0x0009A8EE
		[MonoTODO]
		public static int DefaultMaximumErrorResponseLength
		{
			get
			{
				throw HttpWebRequest.GetMustImplement();
			}
			set
			{
				throw HttpWebRequest.GetMustImplement();
			}
		}

		/// <summary>Gets or sets the value of the Expect HTTP header.</summary>
		/// <returns>The contents of the Expect HTTP header. The default value is null.NoteThe value for this property is stored in <see cref="T:System.Net.WebHeaderCollection" />. If WebHeaderCollection is set, the property value is lost.</returns>
		/// <exception cref="T:System.ArgumentException">Expect is set to a string that contains "100-continue" as a substring. </exception>
		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x060028B3 RID: 10419 RVA: 0x0009C6F5 File Offset: 0x0009A8F5
		// (set) Token: 0x060028B4 RID: 10420 RVA: 0x0009C708 File Offset: 0x0009A908
		public string Expect
		{
			get
			{
				return this.webHeaders["Expect"];
			}
			set
			{
				this.CheckRequestStarted();
				string text = value;
				if (text != null)
				{
					text = text.Trim().ToLower();
				}
				if (text == null || text.Length == 0)
				{
					this.webHeaders.RemoveInternal("Expect");
					return;
				}
				if (text == "100-continue")
				{
					throw new ArgumentException("100-Continue cannot be set with this property.", "value");
				}
				this.webHeaders.CheckUpdate("Expect", value);
			}
		}

		/// <summary>Gets a value that indicates whether a response has been received from an Internet resource.</summary>
		/// <returns>true if a response has been received; otherwise, false.</returns>
		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x060028B5 RID: 10421 RVA: 0x0009C776 File Offset: 0x0009A976
		public virtual bool HaveResponse
		{
			get
			{
				return this.haveResponse;
			}
		}

		/// <summary>Specifies a collection of the name/value pairs that make up the HTTP headers.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the name/value pairs that make up the headers for the HTTP request.</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has been started by calling the <see cref="M:System.Net.HttpWebRequest.GetRequestStream" />, <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />, <see cref="M:System.Net.HttpWebRequest.GetResponse" />, or <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> method. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x060028B6 RID: 10422 RVA: 0x0009C77E File Offset: 0x0009A97E
		// (set) Token: 0x060028B7 RID: 10423 RVA: 0x0009C788 File Offset: 0x0009A988
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.webHeaders;
			}
			set
			{
				this.CheckRequestStarted();
				WebHeaderCollection webHeaderCollection = new WebHeaderCollection(WebHeaderCollectionType.HttpWebRequest);
				foreach (string text in value.AllKeys)
				{
					webHeaderCollection.Add(text, value[text]);
				}
				this.webHeaders = webHeaderCollection;
			}
		}

		/// <summary>Get or set the Host header value to use in an HTTP request independent from the request URI.</summary>
		/// <returns>The Host header value in the HTTP request.</returns>
		/// <exception cref="T:System.ArgumentNullException">The Host header cannot be set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The Host header cannot be set to an invalid value. </exception>
		/// <exception cref="T:System.InvalidOperationException">The Host header cannot be set after the <see cref="T:System.Net.HttpWebRequest" /> has already started to be sent. </exception>
		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x0009C7D5 File Offset: 0x0009A9D5
		// (set) Token: 0x060028B9 RID: 10425 RVA: 0x0009C7F1 File Offset: 0x0009A9F1
		public string Host
		{
			get
			{
				if (this.host == null)
				{
					return this.actualUri.Authority;
				}
				return this.host;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!HttpWebRequest.CheckValidHost(this.actualUri.Scheme, value))
				{
					throw new ArgumentException("Invalid host: " + value);
				}
				this.host = value;
			}
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x0009C82C File Offset: 0x0009AA2C
		private static bool CheckValidHost(string scheme, string val)
		{
			IPAddress ipaddress;
			return val.Length != 0 && val[0] != '.' && val.IndexOf('/') < 0 && (IPAddress.TryParse(val, out ipaddress) || Uri.IsWellFormedUriString(scheme + "://" + val + "/", UriKind.Absolute));
		}

		/// <summary>Gets or sets the value of the If-Modified-Since HTTP header.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the contents of the If-Modified-Since HTTP header. The default value is the current date and time.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x0009C880 File Offset: 0x0009AA80
		// (set) Token: 0x060028BC RID: 10428 RVA: 0x0009C8CC File Offset: 0x0009AACC
		public DateTime IfModifiedSince
		{
			get
			{
				string text = this.webHeaders["If-Modified-Since"];
				if (text == null)
				{
					return DateTime.Now;
				}
				DateTime dateTime;
				try
				{
					dateTime = MonoHttpDate.Parse(text);
				}
				catch (Exception)
				{
					dateTime = DateTime.Now;
				}
				return dateTime;
			}
			set
			{
				this.CheckRequestStarted();
				this.webHeaders.SetInternal("If-Modified-Since", value.ToUniversalTime().ToString("r", null));
			}
		}

		/// <summary>Gets or sets a value that indicates whether to make a persistent connection to the Internet resource.</summary>
		/// <returns>true if the request to the Internet resource should contain a Connection HTTP header with the value Keep-alive; otherwise, false. The default is true.</returns>
		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060028BD RID: 10429 RVA: 0x0009C904 File Offset: 0x0009AB04
		// (set) Token: 0x060028BE RID: 10430 RVA: 0x0009C90C File Offset: 0x0009AB0C
		public bool KeepAlive
		{
			get
			{
				return this.keepAlive;
			}
			set
			{
				this.keepAlive = value;
			}
		}

		/// <summary>Gets or sets the maximum number of redirects that the request follows.</summary>
		/// <returns>The maximum number of redirection responses that the request follows. The default value is 50.</returns>
		/// <exception cref="T:System.ArgumentException">The value is set to 0 or less. </exception>
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060028BF RID: 10431 RVA: 0x0009C915 File Offset: 0x0009AB15
		// (set) Token: 0x060028C0 RID: 10432 RVA: 0x0009C91D File Offset: 0x0009AB1D
		public int MaximumAutomaticRedirections
		{
			get
			{
				return this.maxAutoRedirect;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("Must be > 0", "value");
				}
				this.maxAutoRedirect = value;
			}
		}

		/// <summary>Gets or sets the maximum allowed length of the response headers.</summary>
		/// <returns>The length, in kilobytes (1024 bytes), of the response headers.</returns>
		/// <exception cref="T:System.InvalidOperationException">The property is set after the request has already been submitted. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0 and is not equal to -1. </exception>
		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060028C1 RID: 10433 RVA: 0x0009C93A File Offset: 0x0009AB3A
		// (set) Token: 0x060028C2 RID: 10434 RVA: 0x0009C942 File Offset: 0x0009AB42
		[MonoTODO("Use this")]
		public int MaximumResponseHeadersLength
		{
			get
			{
				return this.maxResponseHeadersLength;
			}
			set
			{
				this.maxResponseHeadersLength = value;
			}
		}

		/// <summary>Gets or sets the default for the <see cref="P:System.Net.HttpWebRequest.MaximumResponseHeadersLength" /> property.</summary>
		/// <returns>The length, in kilobytes (1024 bytes), of the default maximum for response headers received. The default configuration file sets this value to 64 kilobytes.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is not equal to -1 and is less than zero. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060028C3 RID: 10435 RVA: 0x0009C94B File Offset: 0x0009AB4B
		// (set) Token: 0x060028C4 RID: 10436 RVA: 0x0009C952 File Offset: 0x0009AB52
		[MonoTODO("Use this")]
		public static int DefaultMaximumResponseHeadersLength
		{
			get
			{
				return HttpWebRequest.defaultMaxResponseHeadersLength;
			}
			set
			{
				HttpWebRequest.defaultMaxResponseHeadersLength = value;
			}
		}

		/// <summary>Gets or sets a time-out in milliseconds when writing to or reading from a stream.</summary>
		/// <returns>The number of milliseconds before the writing or reading times out. The default value is 300,000 milliseconds (5 minutes).</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has already been sent. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /></exception>
		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x060028C5 RID: 10437 RVA: 0x0009C95A File Offset: 0x0009AB5A
		// (set) Token: 0x060028C6 RID: 10438 RVA: 0x0009C962 File Offset: 0x0009AB62
		public int ReadWriteTimeout
		{
			get
			{
				return this.readWriteTimeout;
			}
			set
			{
				if (this.requestSent)
				{
					throw new InvalidOperationException("The request has already been sent.");
				}
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value", "Must be >= -1");
				}
				this.readWriteTimeout = value;
			}
		}

		/// <summary>Gets or sets a timeout, in seconds, to wait for the server status after 100-continue is received.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.The timeout, in seconds, to wait for the server status after 100-continue is received.</returns>
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x060028C7 RID: 10439 RVA: 0x00004239 File Offset: 0x00002439
		// (set) Token: 0x060028C8 RID: 10440 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		public int ContinueTimeout
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

		/// <summary>Gets or sets the media type of the request.</summary>
		/// <returns>The media type of the request. The default value is null.</returns>
		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x060028C9 RID: 10441 RVA: 0x0009C992 File Offset: 0x0009AB92
		// (set) Token: 0x060028CA RID: 10442 RVA: 0x0009C99A File Offset: 0x0009AB9A
		public string MediaType
		{
			get
			{
				return this.mediaType;
			}
			set
			{
				this.mediaType = value;
			}
		}

		/// <summary>Gets or sets the method for the request.</summary>
		/// <returns>The request method to use to contact the Internet resource. The default value is GET.</returns>
		/// <exception cref="T:System.ArgumentException">No method is supplied.-or- The method string contains invalid characters. </exception>
		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x060028CB RID: 10443 RVA: 0x0009C9A3 File Offset: 0x0009ABA3
		// (set) Token: 0x060028CC RID: 10444 RVA: 0x0009C9AC File Offset: 0x0009ABAC
		public override string Method
		{
			get
			{
				return this.method;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					throw new ArgumentException("not a valid method");
				}
				this.method = value.ToUpperInvariant();
				if (this.method != "HEAD" && this.method != "GET" && this.method != "POST" && this.method != "PUT" && this.method != "DELETE" && this.method != "CONNECT" && this.method != "TRACE" && this.method != "MKCOL")
				{
					this.method = value;
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether to pipeline the request to the Internet resource.</summary>
		/// <returns>true if the request should be pipelined; otherwise, false. The default is true.</returns>
		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x060028CD RID: 10445 RVA: 0x0009CA7F File Offset: 0x0009AC7F
		// (set) Token: 0x060028CE RID: 10446 RVA: 0x0009CA87 File Offset: 0x0009AC87
		public bool Pipelined
		{
			get
			{
				return this.pipelined;
			}
			set
			{
				this.pipelined = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to send an Authorization header with the request.</summary>
		/// <returns>true to send an  HTTP Authorization header with requests after authentication has taken place; otherwise, false. The default is false.</returns>
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x060028CF RID: 10447 RVA: 0x0009CA90 File Offset: 0x0009AC90
		// (set) Token: 0x060028D0 RID: 10448 RVA: 0x0009CA98 File Offset: 0x0009AC98
		public override bool PreAuthenticate
		{
			get
			{
				return this.preAuthenticate;
			}
			set
			{
				this.preAuthenticate = value;
			}
		}

		/// <summary>Gets or sets the version of HTTP to use for the request.</summary>
		/// <returns>The HTTP version to use for the request. The default is <see cref="F:System.Net.HttpVersion.Version11" />.</returns>
		/// <exception cref="T:System.ArgumentException">The HTTP version is set to a value other than 1.0 or 1.1. </exception>
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x060028D1 RID: 10449 RVA: 0x0009CAA1 File Offset: 0x0009ACA1
		// (set) Token: 0x060028D2 RID: 10450 RVA: 0x0009CAA9 File Offset: 0x0009ACA9
		public Version ProtocolVersion
		{
			get
			{
				return this.version;
			}
			set
			{
				if (value != HttpVersion.Version10 && value != HttpVersion.Version11)
				{
					throw new ArgumentException("value");
				}
				this.force_version = true;
				this.version = value;
			}
		}

		/// <summary>Gets or sets proxy information for the request.</summary>
		/// <returns>The <see cref="T:System.Net.IWebProxy" /> object to use to proxy the request. The default value is set by calling the <see cref="P:System.Net.GlobalProxySelection.Select" /> property.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Net.HttpWebRequest.Proxy" /> is set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The request has been started by calling <see cref="M:System.Net.HttpWebRequest.GetRequestStream" />, <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />, <see cref="M:System.Net.HttpWebRequest.GetResponse" />, or <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have permission for the requested operation. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x060028D3 RID: 10451 RVA: 0x0009CADE File Offset: 0x0009ACDE
		// (set) Token: 0x060028D4 RID: 10452 RVA: 0x0009CAE6 File Offset: 0x0009ACE6
		public override IWebProxy Proxy
		{
			get
			{
				return this.proxy;
			}
			set
			{
				this.CheckRequestStarted();
				this.proxy = value;
				this.servicePoint = null;
				this.GetServicePoint();
			}
		}

		/// <summary>Gets or sets the value of the Referer HTTP header.</summary>
		/// <returns>The value of the Referer HTTP header. The default value is null.</returns>
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x060028D5 RID: 10453 RVA: 0x0009CB03 File Offset: 0x0009AD03
		// (set) Token: 0x060028D6 RID: 10454 RVA: 0x0009CB15 File Offset: 0x0009AD15
		public string Referer
		{
			get
			{
				return this.webHeaders["Referer"];
			}
			set
			{
				this.CheckRequestStarted();
				if (value == null || value.Trim().Length == 0)
				{
					this.webHeaders.RemoveInternal("Referer");
					return;
				}
				this.webHeaders.SetInternal("Referer", value);
			}
		}

		/// <summary>Gets the original Uniform Resource Identifier (URI) of the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI of the Internet resource passed to the <see cref="M:System.Net.WebRequest.Create(System.String)" /> method.</returns>
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x060028D7 RID: 10455 RVA: 0x0009CB4F File Offset: 0x0009AD4F
		public override Uri RequestUri
		{
			get
			{
				return this.requestUri;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to send data in segments to the Internet resource.</summary>
		/// <returns>true to send data to the Internet resource in segments; otherwise, false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has been started by calling the <see cref="M:System.Net.HttpWebRequest.GetRequestStream" />, <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />, <see cref="M:System.Net.HttpWebRequest.GetResponse" />, or <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> method. </exception>
		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x060028D8 RID: 10456 RVA: 0x0009CB57 File Offset: 0x0009AD57
		// (set) Token: 0x060028D9 RID: 10457 RVA: 0x0009CB5F File Offset: 0x0009AD5F
		public bool SendChunked
		{
			get
			{
				return this.sendChunked;
			}
			set
			{
				this.CheckRequestStarted();
				this.sendChunked = value;
			}
		}

		/// <summary>Gets the service point to use for the request.</summary>
		/// <returns>A <see cref="T:System.Net.ServicePoint" /> that represents the network connection to the Internet resource.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x060028DA RID: 10458 RVA: 0x0009CB6E File Offset: 0x0009AD6E
		public ServicePoint ServicePoint
		{
			get
			{
				return this.GetServicePoint();
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x060028DB RID: 10459 RVA: 0x0009CB76 File Offset: 0x0009AD76
		internal ServicePoint ServicePointNoLock
		{
			get
			{
				return this.servicePoint;
			}
		}

		/// <summary>Gets a value that indicates whether the request provides support for a <see cref="T:System.Net.CookieContainer" />.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if a <see cref="T:System.Net.CookieContainer" /> is supported; otherwise, false. </returns>
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x060028DC RID: 10460 RVA: 0x000027E2 File Offset: 0x000009E2
		public virtual bool SupportsCookieContainer
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the time-out value in milliseconds for the <see cref="M:System.Net.HttpWebRequest.GetResponse" /> and <see cref="M:System.Net.HttpWebRequest.GetRequestStream" /> methods.</summary>
		/// <returns>The number of milliseconds to wait before the request times out. The default value is 100,000 milliseconds (100 seconds).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than zero and is not <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x060028DD RID: 10461 RVA: 0x0009CB7E File Offset: 0x0009AD7E
		// (set) Token: 0x060028DE RID: 10462 RVA: 0x0009CB86 File Offset: 0x0009AD86
		public override int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.timeout = value;
			}
		}

		/// <summary>Gets or sets the value of the Transfer-encoding HTTP header.</summary>
		/// <returns>The value of the Transfer-encoding HTTP header. The default value is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set when <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to the value "Chunked". </exception>
		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x060028DF RID: 10463 RVA: 0x0009CB9E File Offset: 0x0009AD9E
		// (set) Token: 0x060028E0 RID: 10464 RVA: 0x0009CBB0 File Offset: 0x0009ADB0
		public string TransferEncoding
		{
			get
			{
				return this.webHeaders["Transfer-Encoding"];
			}
			set
			{
				this.CheckRequestStarted();
				string text = value;
				if (text != null)
				{
					text = text.Trim().ToLower();
				}
				if (text == null || text.Length == 0)
				{
					this.webHeaders.RemoveInternal("Transfer-Encoding");
					return;
				}
				if (text == "chunked")
				{
					throw new ArgumentException("Chunked encoding must be set with the SendChunked property");
				}
				if (!this.sendChunked)
				{
					throw new ArgumentException("SendChunked must be True", "value");
				}
				this.webHeaders.CheckUpdate("Transfer-Encoding", value);
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether default credentials are sent with requests.</summary>
		/// <returns>true if the default credentials are used; otherwise false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">You attempted to set this property after the request was sent.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="USERNAME" />
		/// </PermissionSet>
		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x060028E1 RID: 10465 RVA: 0x0009CC31 File Offset: 0x0009AE31
		// (set) Token: 0x060028E2 RID: 10466 RVA: 0x0009CC40 File Offset: 0x0009AE40
		public override bool UseDefaultCredentials
		{
			get
			{
				return CredentialCache.DefaultCredentials == this.Credentials;
			}
			set
			{
				this.Credentials = (value ? CredentialCache.DefaultCredentials : null);
			}
		}

		/// <summary>Gets or sets the value of the User-agent HTTP header.</summary>
		/// <returns>The value of the User-agent HTTP header. The default value is null.NoteThe value for this property is stored in <see cref="T:System.Net.WebHeaderCollection" />. If WebHeaderCollection is set, the property value is lost.</returns>
		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x060028E3 RID: 10467 RVA: 0x0009CC53 File Offset: 0x0009AE53
		// (set) Token: 0x060028E4 RID: 10468 RVA: 0x0009CC65 File Offset: 0x0009AE65
		public string UserAgent
		{
			get
			{
				return this.webHeaders["User-Agent"];
			}
			set
			{
				this.webHeaders.SetInternal("User-Agent", value);
			}
		}

		/// <summary>Gets or sets a value that indicates whether to allow high-speed NTLM-authenticated connection sharing.</summary>
		/// <returns>true to keep the authenticated connection open; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x0009CC78 File Offset: 0x0009AE78
		// (set) Token: 0x060028E6 RID: 10470 RVA: 0x0009CC80 File Offset: 0x0009AE80
		public bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.unsafe_auth_blah;
			}
			set
			{
				this.unsafe_auth_blah = value;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x060028E7 RID: 10471 RVA: 0x0009CC89 File Offset: 0x0009AE89
		internal bool GotRequestStream
		{
			get
			{
				return this.gotRequestStream;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x060028E8 RID: 10472 RVA: 0x0009CC91 File Offset: 0x0009AE91
		// (set) Token: 0x060028E9 RID: 10473 RVA: 0x0009CC99 File Offset: 0x0009AE99
		internal bool ExpectContinue
		{
			get
			{
				return this.expectContinue;
			}
			set
			{
				this.expectContinue = value;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x060028EA RID: 10474 RVA: 0x0009C428 File Offset: 0x0009A628
		internal Uri AuthUri
		{
			get
			{
				return this.actualUri;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x0009CCA2 File Offset: 0x0009AEA2
		internal bool ProxyQuery
		{
			get
			{
				return this.servicePoint.UsesProxy && !this.servicePoint.UseConnect;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x060028EC RID: 10476 RVA: 0x0009CCC1 File Offset: 0x0009AEC1
		internal ServerCertValidationCallback ServerCertValidationCallback
		{
			get
			{
				return this.certValidationCallback;
			}
		}

		/// <summary>Gets or sets a callback function to validate the server certificate.</summary>
		/// <returns>Returns <see cref="T:System.Net.Security.RemoteCertificateValidationCallback" />.A callback function to validate the server certificate.</returns>
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x060028ED RID: 10477 RVA: 0x0009CCC9 File Offset: 0x0009AEC9
		// (set) Token: 0x060028EE RID: 10478 RVA: 0x0009CCE0 File Offset: 0x0009AEE0
		public RemoteCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				if (this.certValidationCallback == null)
				{
					return null;
				}
				return this.certValidationCallback.ValidationCallback;
			}
			set
			{
				if (value == null)
				{
					this.certValidationCallback = null;
					return;
				}
				this.certValidationCallback = new ServerCertValidationCallback(value);
			}
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x0009CCFC File Offset: 0x0009AEFC
		internal ServicePoint GetServicePoint()
		{
			object obj = this.locker;
			lock (obj)
			{
				if (this.hostChanged || this.servicePoint == null)
				{
					this.servicePoint = ServicePointManager.FindServicePoint(this.actualUri, this.proxy);
					this.hostChanged = false;
				}
			}
			return this.servicePoint;
		}

		/// <summary>Adds a byte range header to a request for a specific range from the beginning or end of the requested data.</summary>
		/// <param name="range">The starting or ending point of the range. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added. </exception>
		// Token: 0x060028F0 RID: 10480 RVA: 0x0009CD6C File Offset: 0x0009AF6C
		public void AddRange(int range)
		{
			this.AddRange("bytes", (long)range);
		}

		/// <summary>Adds a byte range header to the request for a specified range.</summary>
		/// <param name="from">The position at which to start sending data. </param>
		/// <param name="to">The position at which to stop sending data. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="from" /> is greater than <paramref name="to" />-or- <paramref name="from" /> or <paramref name="to" /> is less than 0. </exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added. </exception>
		// Token: 0x060028F1 RID: 10481 RVA: 0x0009CD7B File Offset: 0x0009AF7B
		public void AddRange(int from, int to)
		{
			this.AddRange("bytes", (long)from, (long)to);
		}

		/// <summary>Adds a Range header to a request for a specific range from the beginning or end of the requested data.</summary>
		/// <param name="rangeSpecifier">The description of the range. </param>
		/// <param name="range">The starting or ending point of the range. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rangeSpecifier" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added. </exception>
		// Token: 0x060028F2 RID: 10482 RVA: 0x0009CD8C File Offset: 0x0009AF8C
		public void AddRange(string rangeSpecifier, int range)
		{
			this.AddRange(rangeSpecifier, (long)range);
		}

		/// <summary>Adds a range header to a request for a specified range.</summary>
		/// <param name="rangeSpecifier">The description of the range. </param>
		/// <param name="from">The position at which to start sending data. </param>
		/// <param name="to">The position at which to stop sending data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rangeSpecifier" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="from" /> is greater than <paramref name="to" />-or- <paramref name="from" /> or <paramref name="to" /> is less than 0. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added. </exception>
		// Token: 0x060028F3 RID: 10483 RVA: 0x0009CD97 File Offset: 0x0009AF97
		public void AddRange(string rangeSpecifier, int from, int to)
		{
			this.AddRange(rangeSpecifier, (long)from, (long)to);
		}

		/// <summary>Adds a byte range header to a request for a specific range from the beginning or end of the requested data.</summary>
		/// <param name="range">The starting or ending point of the range.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added. </exception>
		// Token: 0x060028F4 RID: 10484 RVA: 0x0009CDA4 File Offset: 0x0009AFA4
		public void AddRange(long range)
		{
			this.AddRange("bytes", range);
		}

		/// <summary>Adds a byte range header to the request for a specified range.</summary>
		/// <param name="from">The position at which to start sending data.</param>
		/// <param name="to">The position at which to stop sending data.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="from" /> is greater than <paramref name="to" />-or- <paramref name="from" /> or <paramref name="to" /> is less than 0. </exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added. </exception>
		// Token: 0x060028F5 RID: 10485 RVA: 0x0009CDB2 File Offset: 0x0009AFB2
		public void AddRange(long from, long to)
		{
			this.AddRange("bytes", from, to);
		}

		/// <summary>Adds a Range header to a request for a specific range from the beginning or end of the requested data.</summary>
		/// <param name="rangeSpecifier">The description of the range.</param>
		/// <param name="range">The starting or ending point of the range.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rangeSpecifier" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added. </exception>
		// Token: 0x060028F6 RID: 10486 RVA: 0x0009CDC4 File Offset: 0x0009AFC4
		public void AddRange(string rangeSpecifier, long range)
		{
			if (rangeSpecifier == null)
			{
				throw new ArgumentNullException("rangeSpecifier");
			}
			if (!WebHeaderCollection.IsValidToken(rangeSpecifier))
			{
				throw new ArgumentException("Invalid range specifier", "rangeSpecifier");
			}
			string text = this.webHeaders["Range"];
			if (text == null)
			{
				text = rangeSpecifier + "=";
			}
			else
			{
				if (string.Compare(text.Substring(0, text.IndexOf('=')), rangeSpecifier, StringComparison.OrdinalIgnoreCase) != 0)
				{
					throw new InvalidOperationException("A different range specifier is already in use");
				}
				text += ",";
			}
			string text2 = range.ToString(CultureInfo.InvariantCulture);
			if (range < 0L)
			{
				text = text + "0" + text2;
			}
			else
			{
				text = text + text2 + "-";
			}
			this.webHeaders.ChangeInternal("Range", text);
		}

		/// <summary>Adds a range header to a request for a specified range.</summary>
		/// <param name="rangeSpecifier">The description of the range.</param>
		/// <param name="from">The position at which to start sending data.</param>
		/// <param name="to">The position at which to stop sending data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rangeSpecifier" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="from" /> is greater than <paramref name="to" />-or- <paramref name="from" /> or <paramref name="to" /> is less than 0. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rangeSpecifier" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The range header could not be added. </exception>
		// Token: 0x060028F7 RID: 10487 RVA: 0x0009CE88 File Offset: 0x0009B088
		public void AddRange(string rangeSpecifier, long from, long to)
		{
			if (rangeSpecifier == null)
			{
				throw new ArgumentNullException("rangeSpecifier");
			}
			if (!WebHeaderCollection.IsValidToken(rangeSpecifier))
			{
				throw new ArgumentException("Invalid range specifier", "rangeSpecifier");
			}
			if (from > to || from < 0L)
			{
				throw new ArgumentOutOfRangeException("from");
			}
			if (to < 0L)
			{
				throw new ArgumentOutOfRangeException("to");
			}
			string text = this.webHeaders["Range"];
			if (text == null)
			{
				text = rangeSpecifier + "=";
			}
			else
			{
				text += ",";
			}
			text = string.Format("{0}{1}-{2}", text, from, to);
			this.webHeaders.ChangeInternal("Range", text);
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x0009CF38 File Offset: 0x0009B138
		private WebOperation SendRequest(bool redirecting, BufferOffsetSize writeBuffer, CancellationToken cancellationToken)
		{
			object obj = this.locker;
			WebOperation webOperation2;
			lock (obj)
			{
				if (!redirecting && this.requestSent)
				{
					WebOperation webOperation = this.currentOperation;
					if (webOperation == null)
					{
						throw new InvalidOperationException("Should never happen!");
					}
					webOperation2 = webOperation;
				}
				else
				{
					WebOperation webOperation = new WebOperation(this, writeBuffer, false, cancellationToken);
					if (Interlocked.CompareExchange<WebOperation>(ref this.currentOperation, webOperation, null) != null)
					{
						throw new InvalidOperationException("Invalid nested call.");
					}
					this.requestSent = true;
					if (!redirecting)
					{
						this.redirects = 0;
					}
					this.servicePoint = this.GetServicePoint();
					this.servicePoint.SendRequest(webOperation, this.connectionGroup);
					webOperation2 = webOperation;
				}
			}
			return webOperation2;
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x0009CFEC File Offset: 0x0009B1EC
		private async Task<Stream> MyGetRequestStreamAsync(CancellationToken cancellationToken)
		{
			if (this.Aborted)
			{
				throw HttpWebRequest.CreateRequestAbortedException();
			}
			bool flag = !(this.method == "GET") && !(this.method == "CONNECT") && !(this.method == "HEAD") && !(this.method == "TRACE");
			if (this.method == null || !flag)
			{
				throw new ProtocolViolationException("Cannot send data when method is: " + this.method);
			}
			if (this.contentLength == -1L && !this.sendChunked && !this.allowBuffering && this.KeepAlive)
			{
				throw new ProtocolViolationException("Content-Length not set");
			}
			string transferEncoding = this.TransferEncoding;
			if (!this.sendChunked && transferEncoding != null && transferEncoding.Trim() != "")
			{
				throw new ProtocolViolationException("SendChunked should be true.");
			}
			object obj = this.locker;
			WebOperation webOperation;
			lock (obj)
			{
				if (this.getResponseCalled)
				{
					throw new InvalidOperationException("The operation cannot be performed once the request has been submitted.");
				}
				webOperation = this.currentOperation;
				if (webOperation == null)
				{
					this.initialMethod = this.method;
					this.gotRequestStream = true;
					webOperation = this.SendRequest(false, null, cancellationToken);
				}
			}
			return await webOperation.GetRequestStream().ConfigureAwait(false);
		}

		/// <summary>Begins an asynchronous request for a <see cref="T:System.IO.Stream" /> object to use to write data.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">The state object for this request. </param>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.HttpWebRequest.Method" /> property is GET or HEAD.-or- <see cref="P:System.Net.HttpWebRequest.KeepAlive" /> is true, <see cref="P:System.Net.HttpWebRequest.AllowWriteStreamBuffering" /> is false, <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is -1, <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false, and <see cref="P:System.Net.HttpWebRequest.Method" /> is POST or PUT. </exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is being used by a previous call to <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />-or- <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to a value and <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false.-or- The thread pool is running out of threads. </exception>
		/// <exception cref="T:System.NotSupportedException">The request cache validator indicated that the response for this request can be served from the cache; however, requests that write data must not use the cache. This exception can occur if you are using a custom cache validator that is incorrectly implemented. </exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called. </exception>
		/// <exception cref="T:System.ObjectDisposedException">In a .NET Compact Framework application, a request stream with zero content length was not obtained and closed correctly. For more information about handling zero content length requests, see Network Programming in the .NET Compact Framework.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060028FA RID: 10490 RVA: 0x0009D039 File Offset: 0x0009B239
		public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			if (this.Aborted)
			{
				throw HttpWebRequest.CreateRequestAbortedException();
			}
			return TaskToApm.Begin(this.RunWithTimeout<Stream>(new Func<CancellationToken, Task<Stream>>(this.MyGetRequestStreamAsync)), callback, state);
		}

		/// <summary>Ends an asynchronous request for a <see cref="T:System.IO.Stream" /> object to use to write data.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> to use to write request data.</returns>
		/// <param name="asyncResult">The pending request for a stream. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.IO.IOException">The request did not complete, and no stream is available. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by the current instance from a call to <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method was called previously using <paramref name="asyncResult" />. </exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.-or- An error occurred while processing the request. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060028FB RID: 10491 RVA: 0x0009D064 File Offset: 0x0009B264
		public override Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Stream stream;
			try
			{
				stream = TaskToApm.End<Stream>(asyncResult);
			}
			catch (Exception ex)
			{
				throw HttpWebRequest.FlattenException(ex);
			}
			return stream;
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object to use to write request data.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> to use to write request data.</returns>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.HttpWebRequest.Method" /> property is GET or HEAD.-or- <see cref="P:System.Net.HttpWebRequest.KeepAlive" /> is true, <see cref="P:System.Net.HttpWebRequest.AllowWriteStreamBuffering" /> is false, <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is -1, <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false, and <see cref="P:System.Net.HttpWebRequest.Method" /> is POST or PUT. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.HttpWebRequest.GetRequestStream" /> method is called more than once.-or- <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to a value and <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false. </exception>
		/// <exception cref="T:System.NotSupportedException">The request cache validator indicated that the response for this request can be served from the cache; however, requests that write data must not use the cache. This exception can occur if you are using a custom cache validator that is incorrectly implemented. </exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.-or- The time-out period for the request expired.-or- An error occurred while processing the request. </exception>
		/// <exception cref="T:System.ObjectDisposedException">In a .NET Compact Framework application, a request stream with zero content length was not obtained and closed correctly. For more information about handling zero content length requests, see Network Programming in the .NET Compact Framework.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060028FC RID: 10492 RVA: 0x0009D0A0 File Offset: 0x0009B2A0
		public override Stream GetRequestStream()
		{
			Stream result;
			try
			{
				result = this.GetRequestStreamAsync().Result;
			}
			catch (Exception ex)
			{
				throw HttpWebRequest.FlattenException(ex);
			}
			return result;
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object to use to write request data and outputs the <see cref="T:System.Net.TransportContext" /> associated with the stream.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> to use to write request data.</returns>
		/// <param name="context">The <see cref="T:System.Net.TransportContext" /> for the <see cref="T:System.IO.Stream" />.</param>
		/// <exception cref="T:System.Exception">The <see cref="M:System.Net.HttpWebRequest.GetRequestStream" /> method was unable to obtain the <see cref="T:System.IO.Stream" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.HttpWebRequest.GetRequestStream" /> method is called more than once.-or- <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to a value and <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false. </exception>
		/// <exception cref="T:System.NotSupportedException">The request cache validator indicated that the response for this request can be served from the cache; however, requests that write data must not use the cache. This exception can occur if you are using a custom cache validator that is incorrectly implemented. </exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.HttpWebRequest.Method" /> property is GET or HEAD.-or- <see cref="P:System.Net.HttpWebRequest.KeepAlive" /> is true, <see cref="P:System.Net.HttpWebRequest.AllowWriteStreamBuffering" /> is false, <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is -1, <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false, and <see cref="P:System.Net.HttpWebRequest.Method" /> is POST or PUT. </exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.-or- The time-out period for the request expired.-or- An error occurred while processing the request. </exception>
		// Token: 0x060028FD RID: 10493 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		public Stream GetRequestStream(out TransportContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x0009D0D4 File Offset: 0x0009B2D4
		internal static async Task<T> RunWithTimeout<T>(Func<CancellationToken, Task<T>> func, int timeout, Action abort)
		{
			T result;
			using (CancellationTokenSource cts = new CancellationTokenSource())
			{
				Task timeoutTask = Task.Delay(timeout);
				Task<T> workerTask = func(cts.Token);
				ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter = Task.WhenAny(new Task[] { workerTask, timeoutTask }).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult() == timeoutTask)
				{
					try
					{
						cts.Cancel();
						abort();
					}
					catch
					{
					}
					workerTask.ContinueWith<int?>(delegate(Task<T> t)
					{
						AggregateException exception = t.Exception;
						if (exception == null)
						{
							return null;
						}
						return new int?(exception.GetHashCode());
					}, TaskContinuationOptions.OnlyOnFaulted);
					throw new WebException("The operation has timed out.", WebExceptionStatus.Timeout);
				}
				result = workerTask.Result;
			}
			return result;
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x0009D129 File Offset: 0x0009B329
		private Task<T> RunWithTimeout<T>(Func<CancellationToken, Task<T>> func)
		{
			return HttpWebRequest.RunWithTimeout<T>(func, this.timeout, new Action(this.Abort));
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x0009D144 File Offset: 0x0009B344
		private async Task<HttpWebResponse> MyGetResponseAsync(CancellationToken cancellationToken)
		{
			if (this.Aborted)
			{
				throw HttpWebRequest.CreateRequestAbortedException();
			}
			if (this.method == null)
			{
				throw new ProtocolViolationException("Method is null.");
			}
			string transferEncoding = this.TransferEncoding;
			if (!this.sendChunked && transferEncoding != null && transferEncoding.Trim() != "")
			{
				throw new ProtocolViolationException("SendChunked should be true.");
			}
			WebCompletionSource completion = new WebCompletionSource();
			object obj = this.locker;
			WebOperation operation;
			lock (obj)
			{
				this.getResponseCalled = true;
				WebCompletionSource webCompletionSource = Interlocked.CompareExchange<WebCompletionSource>(ref this.responseTask, completion, null);
				if (webCompletionSource != null)
				{
					webCompletionSource.ThrowOnError();
					if (this.haveResponse && webCompletionSource.IsCompleted)
					{
						return this.webResponse;
					}
					throw new InvalidOperationException("Cannot re-call start of asynchronous method while a previous call is still in progress.");
				}
				else
				{
					operation = this.currentOperation;
					if (this.currentOperation != null)
					{
						this.writeStream = this.currentOperation.WriteStream;
					}
					this.initialMethod = this.method;
					operation = this.SendRequest(false, null, cancellationToken);
				}
			}
			HttpWebResponse httpWebResponse;
			for (;;)
			{
				WebException throwMe = null;
				HttpWebResponse response = null;
				WebResponseStream stream = null;
				bool redirect = false;
				bool mustReadAll = false;
				WebOperation ntlm = null;
				BufferOffsetSize writeBuffer = null;
				try
				{
					cancellationToken.ThrowIfCancellationRequested();
					WebRequestStream webRequestStream = await operation.GetRequestStream();
					this.writeStream = webRequestStream;
					await this.writeStream.WriteRequestAsync(cancellationToken).ConfigureAwait(false);
					stream = await operation.GetResponseStream();
					ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation> valueTuple = await this.GetResponseFromData(stream, cancellationToken).ConfigureAwait(false);
					response = valueTuple.Item1;
					redirect = valueTuple.Item2;
					mustReadAll = valueTuple.Item3;
					writeBuffer = valueTuple.Item4;
					ntlm = valueTuple.Item5;
				}
				catch (Exception ex)
				{
					throwMe = this.GetWebException(ex);
				}
				obj = this.locker;
				lock (obj)
				{
					if (throwMe != null)
					{
						this.haveResponse = true;
						completion.TrySetException(throwMe);
						throw throwMe;
					}
					if (!redirect)
					{
						this.haveResponse = true;
						this.webResponse = response;
						completion.TrySetCompleted();
						httpWebResponse = response;
						break;
					}
					this.finished_reading = false;
					this.haveResponse = false;
					this.webResponse = null;
					this.currentOperation = ntlm;
				}
				try
				{
					if (mustReadAll)
					{
						await stream.ReadAllAsync(redirect || ntlm != null, cancellationToken).ConfigureAwait(false);
					}
					operation.CompleteResponseRead(true, null);
					response.Close();
				}
				catch (Exception ex2)
				{
					throwMe = this.GetWebException(ex2);
				}
				obj = this.locker;
				lock (obj)
				{
					if (throwMe != null)
					{
						this.haveResponse = true;
						WebResponseStream webResponseStream = stream;
						if (webResponseStream != null)
						{
							webResponseStream.Close();
						}
						completion.TrySetException(throwMe);
						throw throwMe;
					}
					if (ntlm == null)
					{
						operation = this.SendRequest(true, writeBuffer, cancellationToken);
					}
					else
					{
						operation = ntlm;
					}
				}
				throwMe = null;
				response = null;
				stream = null;
				ntlm = null;
				writeBuffer = null;
			}
			return httpWebResponse;
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x0009D194 File Offset: 0x0009B394
		[return: TupleElementNames(new string[] { "response", "redirect", "mustReadAll", "writeBuffer", "ntlm" })]
		private async Task<ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>> GetResponseFromData(WebResponseStream stream, CancellationToken cancellationToken)
		{
			HttpWebResponse response = new HttpWebResponse(this.actualUri, this.method, stream, this.cookieContainer);
			WebException throwMe = null;
			bool redirect = false;
			bool mustReadAll = false;
			WebOperation webOperation = null;
			Task<BufferOffsetSize> rewriteHandler = null;
			BufferOffsetSize writeBuffer = null;
			object obj = this.locker;
			lock (obj)
			{
				ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException> valueTuple = this.CheckFinalStatus(response);
				redirect = valueTuple.Item1;
				mustReadAll = valueTuple.Item2;
				rewriteHandler = valueTuple.Item3;
				throwMe = valueTuple.Item4;
			}
			if (throwMe != null)
			{
				if (mustReadAll)
				{
					await stream.ReadAllAsync(false, cancellationToken).ConfigureAwait(false);
				}
				throw throwMe;
			}
			if (rewriteHandler != null)
			{
				writeBuffer = await rewriteHandler.ConfigureAwait(false);
			}
			obj = this.locker;
			lock (obj)
			{
				bool flag2 = this.ProxyQuery && this.proxy != null && !this.proxy.IsBypassed(this.actualUri);
				if (!redirect)
				{
					if ((flag2 ? this.proxy_auth_state : this.auth_state).IsNtlmAuthenticated && response.StatusCode < HttpStatusCode.BadRequest)
					{
						stream.Connection.NtlmAuthenticated = true;
					}
					if (this.writeStream != null)
					{
						this.writeStream.KillBuffer();
					}
					return new ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>(response, false, false, writeBuffer, null);
				}
				if (this.sendChunked)
				{
					this.sendChunked = false;
					this.webHeaders.RemoveInternal("Transfer-Encoding");
				}
				webOperation = this.HandleNtlmAuth(stream, response, writeBuffer, cancellationToken).Item1;
			}
			return new ValueTuple<HttpWebResponse, bool, bool, BufferOffsetSize, WebOperation>(response, true, mustReadAll, writeBuffer, webOperation);
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x0009D1EC File Offset: 0x0009B3EC
		internal static Exception FlattenException(Exception e)
		{
			AggregateException ex;
			if ((ex = e as AggregateException) != null)
			{
				ex = ex.Flatten();
				if (ex.InnerExceptions.Count == 1)
				{
					return ex.InnerException;
				}
			}
			return e;
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x0009D220 File Offset: 0x0009B420
		private WebException GetWebException(Exception e)
		{
			e = HttpWebRequest.FlattenException(e);
			WebException ex;
			if ((ex = e as WebException) != null && (!this.Aborted || ex.Status == WebExceptionStatus.RequestCanceled || ex.Status == WebExceptionStatus.Timeout))
			{
				return ex;
			}
			if (this.Aborted || e is OperationCanceledException || e is ObjectDisposedException)
			{
				return HttpWebRequest.CreateRequestAbortedException();
			}
			return new WebException(e.Message, e, WebExceptionStatus.UnknownError, null);
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x0009D289 File Offset: 0x0009B489
		internal static WebException CreateRequestAbortedException()
		{
			return new WebException(global::SR.Format("The request was aborted: The request was canceled.", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
		}

		/// <summary>Begins an asynchronous request to an Internet resource.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request for a response.</returns>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate </param>
		/// <param name="state">The state object for this request. </param>
		/// <exception cref="T:System.InvalidOperationException">The stream is already in use by a previous call to <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />-or- <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to a value and <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false.-or- The thread pool is running out of threads. </exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">
		///   <see cref="P:System.Net.HttpWebRequest.Method" /> is GET or HEAD, and either <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is greater than zero or <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is true.-or- <see cref="P:System.Net.HttpWebRequest.KeepAlive" /> is true, <see cref="P:System.Net.HttpWebRequest.AllowWriteStreamBuffering" /> is false, and either <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is -1, <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false and <see cref="P:System.Net.HttpWebRequest.Method" /> is POST or PUT.-or- The <see cref="T:System.Net.HttpWebRequest" /> has an entity body but the <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> method is called without calling the <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" /> method. -or- The <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is greater than zero, but the application does not write all of the promised data.</exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002905 RID: 10501 RVA: 0x0009D2A1 File Offset: 0x0009B4A1
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			if (this.Aborted)
			{
				throw HttpWebRequest.CreateRequestAbortedException();
			}
			return TaskToApm.Begin(this.RunWithTimeout<HttpWebResponse>(new Func<CancellationToken, Task<HttpWebResponse>>(this.MyGetResponseAsync)), callback, state);
		}

		/// <summary>Ends an asynchronous request to an Internet resource.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains the response from the Internet resource.</returns>
		/// <param name="asyncResult">The pending request for a response. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method was called previously using <paramref name="asyncResult." />-or- The <see cref="P:System.Net.HttpWebRequest.ContentLength" /> property is greater than 0 but the data has not been written to the request stream. </exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.-or- An error occurred while processing the request. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by the current instance from a call to <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002906 RID: 10502 RVA: 0x0009D2CC File Offset: 0x0009B4CC
		public override WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			WebResponse webResponse;
			try
			{
				webResponse = TaskToApm.End<HttpWebResponse>(asyncResult);
			}
			catch (Exception ex)
			{
				throw HttpWebRequest.FlattenException(ex);
			}
			return webResponse;
		}

		/// <summary>Ends an asynchronous request for a <see cref="T:System.IO.Stream" /> object to use to write data and outputs the <see cref="T:System.Net.TransportContext" /> associated with the stream.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> to use to write request data.</returns>
		/// <param name="asyncResult">The pending request for a stream.</param>
		/// <param name="context">The <see cref="T:System.Net.TransportContext" /> for the <see cref="T:System.IO.Stream" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by the current instance from a call to <see cref="M:System.Net.HttpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method was called previously using <paramref name="asyncResult" />. </exception>
		/// <exception cref="T:System.IO.IOException">The request did not complete, and no stream is available. </exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.-or- An error occurred while processing the request. </exception>
		// Token: 0x06002907 RID: 10503 RVA: 0x0009D308 File Offset: 0x0009B508
		public Stream EndGetRequestStream(IAsyncResult asyncResult, out TransportContext context)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			context = null;
			return this.EndGetRequestStream(asyncResult);
		}

		/// <summary>Returns a response from an Internet resource.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains the response from the Internet resource.</returns>
		/// <exception cref="T:System.InvalidOperationException">The stream is already in use by a previous call to <see cref="M:System.Net.HttpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />.-or- <see cref="P:System.Net.HttpWebRequest.TransferEncoding" /> is set to a value and <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false. </exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">
		///   <see cref="P:System.Net.HttpWebRequest.Method" /> is GET or HEAD, and either <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is greater or equal to zero or <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is true.-or- <see cref="P:System.Net.HttpWebRequest.KeepAlive" /> is true, <see cref="P:System.Net.HttpWebRequest.AllowWriteStreamBuffering" /> is false, <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is -1, <see cref="P:System.Net.HttpWebRequest.SendChunked" /> is false, and <see cref="P:System.Net.HttpWebRequest.Method" /> is POST or PUT. -or- The <see cref="T:System.Net.HttpWebRequest" /> has an entity body but the <see cref="M:System.Net.HttpWebRequest.GetResponse" /> method is called without calling the <see cref="M:System.Net.HttpWebRequest.GetRequestStream" /> method. -or- The <see cref="P:System.Net.HttpWebRequest.ContentLength" /> is greater than zero, but the application does not write all of the promised data.</exception>
		/// <exception cref="T:System.NotSupportedException">The request cache validator indicated that the response for this request can be served from the cache; however, this request includes data to be sent to the server. Requests that send data must not use the cache. This exception can occur if you are using a custom cache validator that is incorrectly implemented. </exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="M:System.Net.HttpWebRequest.Abort" /> was previously called.-or- The time-out period for the request expired.-or- An error occurred while processing the request. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002908 RID: 10504 RVA: 0x0009D324 File Offset: 0x0009B524
		public override WebResponse GetResponse()
		{
			WebResponse result;
			try
			{
				result = this.GetResponseAsync().Result;
			}
			catch (Exception ex)
			{
				throw HttpWebRequest.FlattenException(ex);
			}
			return result;
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002909 RID: 10505 RVA: 0x0009D358 File Offset: 0x0009B558
		// (set) Token: 0x0600290A RID: 10506 RVA: 0x0009D360 File Offset: 0x0009B560
		internal bool FinishedReading
		{
			get
			{
				return this.finished_reading;
			}
			set
			{
				this.finished_reading = value;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x0600290B RID: 10507 RVA: 0x0009D369 File Offset: 0x0009B569
		internal bool Aborted
		{
			get
			{
				return Interlocked.CompareExchange(ref this.aborted, 0, 0) == 1;
			}
		}

		/// <summary>Cancels a request to an Internet resource.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600290C RID: 10508 RVA: 0x0009D37C File Offset: 0x0009B57C
		public override void Abort()
		{
			if (Interlocked.CompareExchange(ref this.aborted, 1, 0) == 1)
			{
				return;
			}
			this.haveResponse = true;
			WebOperation webOperation = this.currentOperation;
			if (webOperation != null)
			{
				webOperation.Abort();
			}
			WebCompletionSource webCompletionSource = this.responseTask;
			if (webCompletionSource != null)
			{
				webCompletionSource.TrySetCanceled();
			}
			if (this.webResponse != null)
			{
				try
				{
					this.webResponse.Close();
					this.webResponse = null;
				}
				catch
				{
				}
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x0600290D RID: 10509 RVA: 0x00083ECE File Offset: 0x000820CE
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data required to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x0600290E RID: 10510 RVA: 0x0009D3F4 File Offset: 0x0009B5F4
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("requestUri", this.requestUri, typeof(Uri));
			serializationInfo.AddValue("actualUri", this.actualUri, typeof(Uri));
			serializationInfo.AddValue("allowAutoRedirect", this.allowAutoRedirect);
			serializationInfo.AddValue("allowBuffering", this.allowBuffering);
			serializationInfo.AddValue("certificates", this.certificates, typeof(X509CertificateCollection));
			serializationInfo.AddValue("connectionGroup", this.connectionGroup);
			serializationInfo.AddValue("contentLength", this.contentLength);
			serializationInfo.AddValue("webHeaders", this.webHeaders, typeof(WebHeaderCollection));
			serializationInfo.AddValue("keepAlive", this.keepAlive);
			serializationInfo.AddValue("maxAutoRedirect", this.maxAutoRedirect);
			serializationInfo.AddValue("mediaType", this.mediaType);
			serializationInfo.AddValue("method", this.method);
			serializationInfo.AddValue("initialMethod", this.initialMethod);
			serializationInfo.AddValue("pipelined", this.pipelined);
			serializationInfo.AddValue("version", this.version, typeof(Version));
			serializationInfo.AddValue("proxy", this.proxy, typeof(IWebProxy));
			serializationInfo.AddValue("sendChunked", this.sendChunked);
			serializationInfo.AddValue("timeout", this.timeout);
			serializationInfo.AddValue("redirects", this.redirects);
			serializationInfo.AddValue("host", this.host);
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x0009D591 File Offset: 0x0009B791
		private void CheckRequestStarted()
		{
			if (this.requestSent)
			{
				throw new InvalidOperationException("request started");
			}
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x0009D5A6 File Offset: 0x0009B7A6
		internal void DoContinueDelegate(int statusCode, WebHeaderCollection headers)
		{
			if (this.continueDelegate != null)
			{
				this.continueDelegate(statusCode, headers);
			}
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x0009D5BD File Offset: 0x0009B7BD
		private void RewriteRedirectToGet()
		{
			this.method = "GET";
			this.webHeaders.RemoveInternal("Transfer-Encoding");
			this.sendChunked = false;
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x0009D5E4 File Offset: 0x0009B7E4
		private bool Redirect(HttpStatusCode code, WebResponse response)
		{
			this.redirects++;
			Exception ex = null;
			string text = null;
			switch (code)
			{
			case HttpStatusCode.MultipleChoices:
				ex = new WebException("Ambiguous redirect.");
				goto IL_0094;
			case HttpStatusCode.MovedPermanently:
			case HttpStatusCode.Found:
				if (this.method == "POST")
				{
					this.RewriteRedirectToGet();
					goto IL_0094;
				}
				goto IL_0094;
			case HttpStatusCode.SeeOther:
				this.RewriteRedirectToGet();
				goto IL_0094;
			case HttpStatusCode.NotModified:
				return false;
			case HttpStatusCode.UseProxy:
				ex = new NotImplementedException("Proxy support not available.");
				goto IL_0094;
			case HttpStatusCode.TemporaryRedirect:
				goto IL_0094;
			}
			ex = new ProtocolViolationException("Invalid status code: " + (int)code);
			IL_0094:
			if (this.method != "GET" && !this.InternalAllowBuffering && this.ResendContentFactory == null && (this.writeStream.WriteBufferLength > 0 || this.contentLength > 0L))
			{
				ex = new WebException("The request requires buffering data to succeed.", null, WebExceptionStatus.ProtocolError, response);
			}
			if (ex != null)
			{
				throw ex;
			}
			if (this.AllowWriteStreamBuffering || this.method == "GET")
			{
				this.contentLength = -1L;
			}
			text = response.Headers["Location"];
			if (text == null)
			{
				throw new WebException(string.Format("No Location header found for {0}", (int)code), null, WebExceptionStatus.ProtocolError, response);
			}
			Uri uri = this.actualUri;
			try
			{
				this.actualUri = new Uri(this.actualUri, text);
			}
			catch (Exception)
			{
				throw new WebException(string.Format("Invalid URL ({0}) for {1}", text, (int)code), null, WebExceptionStatus.ProtocolError, response);
			}
			this.hostChanged = this.actualUri.Scheme != uri.Scheme || this.Host != uri.Authority;
			return true;
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x0009D79C File Offset: 0x0009B99C
		private string GetHeaders()
		{
			bool flag = false;
			if (this.sendChunked)
			{
				flag = true;
				this.webHeaders.ChangeInternal("Transfer-Encoding", "chunked");
				this.webHeaders.RemoveInternal("Content-Length");
			}
			else if (this.contentLength != -1L)
			{
				if (this.auth_state.NtlmAuthState == HttpWebRequest.NtlmAuthState.Challenge || this.proxy_auth_state.NtlmAuthState == HttpWebRequest.NtlmAuthState.Challenge)
				{
					if (this.haveContentLength || this.gotRequestStream || this.contentLength > 0L)
					{
						this.webHeaders.SetInternal("Content-Length", "0");
					}
					else
					{
						this.webHeaders.RemoveInternal("Content-Length");
					}
				}
				else
				{
					if (this.contentLength > 0L)
					{
						flag = true;
					}
					if (this.haveContentLength || this.gotRequestStream || this.contentLength > 0L)
					{
						this.webHeaders.SetInternal("Content-Length", this.contentLength.ToString());
					}
				}
				this.webHeaders.RemoveInternal("Transfer-Encoding");
			}
			else
			{
				this.webHeaders.RemoveInternal("Content-Length");
			}
			if (this.actualVersion == HttpVersion.Version11 && flag && this.servicePoint.SendContinue)
			{
				this.webHeaders.ChangeInternal("Expect", "100-continue");
				this.expectContinue = true;
			}
			else
			{
				this.webHeaders.RemoveInternal("Expect");
				this.expectContinue = false;
			}
			bool proxyQuery = this.ProxyQuery;
			string text = (proxyQuery ? "Proxy-Connection" : "Connection");
			this.webHeaders.RemoveInternal((!proxyQuery) ? "Proxy-Connection" : "Connection");
			Version protocolVersion = this.servicePoint.ProtocolVersion;
			bool flag2 = protocolVersion == null || protocolVersion == HttpVersion.Version10;
			if (this.keepAlive && (this.version == HttpVersion.Version10 || flag2))
			{
				if (this.webHeaders[text] == null || this.webHeaders[text].IndexOf("keep-alive", StringComparison.OrdinalIgnoreCase) == -1)
				{
					this.webHeaders.ChangeInternal(text, "keep-alive");
				}
			}
			else if (!this.keepAlive && this.version == HttpVersion.Version11)
			{
				this.webHeaders.ChangeInternal(text, "close");
			}
			this.webHeaders.SetInternal("Host", this.Host);
			if (this.cookieContainer != null)
			{
				string cookieHeader = this.cookieContainer.GetCookieHeader(this.actualUri);
				if (cookieHeader != "")
				{
					this.webHeaders.ChangeInternal("Cookie", cookieHeader);
				}
				else
				{
					this.webHeaders.RemoveInternal("Cookie");
				}
			}
			string text2 = null;
			if ((this.auto_decomp & DecompressionMethods.GZip) != DecompressionMethods.None)
			{
				text2 = "gzip";
			}
			if ((this.auto_decomp & DecompressionMethods.Deflate) != DecompressionMethods.None)
			{
				text2 = ((text2 != null) ? "gzip, deflate" : "deflate");
			}
			if (text2 != null)
			{
				this.webHeaders.ChangeInternal("Accept-Encoding", text2);
			}
			if (!this.usedPreAuth && this.preAuthenticate)
			{
				this.DoPreAuthenticate();
			}
			return this.webHeaders.ToString();
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x0009DAA8 File Offset: 0x0009BCA8
		private void DoPreAuthenticate()
		{
			bool flag = this.proxy != null && !this.proxy.IsBypassed(this.actualUri);
			ICredentials credentials = ((!flag || this.credentials != null) ? this.credentials : this.proxy.Credentials);
			Authorization authorization = AuthenticationManager.PreAuthenticate(this, credentials);
			if (authorization == null)
			{
				return;
			}
			this.webHeaders.RemoveInternal("Proxy-Authorization");
			this.webHeaders.RemoveInternal("Authorization");
			string text = ((flag && this.credentials == null) ? "Proxy-Authorization" : "Authorization");
			this.webHeaders[text] = authorization.Message;
			this.usedPreAuth = true;
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x0009DB54 File Offset: 0x0009BD54
		internal byte[] GetRequestHeaders()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text;
			if (!this.ProxyQuery)
			{
				text = this.actualUri.PathAndQuery;
			}
			else
			{
				text = string.Format("{0}://{1}{2}", this.actualUri.Scheme, this.Host, this.actualUri.PathAndQuery);
			}
			if (!this.force_version && this.servicePoint.ProtocolVersion != null && this.servicePoint.ProtocolVersion < this.version)
			{
				this.actualVersion = this.servicePoint.ProtocolVersion;
			}
			else
			{
				this.actualVersion = this.version;
			}
			stringBuilder.AppendFormat("{0} {1} HTTP/{2}.{3}\r\n", new object[]
			{
				this.method,
				text,
				this.actualVersion.Major,
				this.actualVersion.Minor
			});
			stringBuilder.Append(this.GetHeaders());
			string text2 = stringBuilder.ToString();
			return Encoding.UTF8.GetBytes(text2);
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x0009DC5C File Offset: 0x0009BE5C
		private ValueTuple<WebOperation, bool> HandleNtlmAuth(WebResponseStream stream, HttpWebResponse response, BufferOffsetSize writeBuffer, CancellationToken cancellationToken)
		{
			bool flag = response.StatusCode == HttpStatusCode.ProxyAuthenticationRequired;
			if ((flag ? this.proxy_auth_state : this.auth_state).NtlmAuthState == HttpWebRequest.NtlmAuthState.None)
			{
				return new ValueTuple<WebOperation, bool>(null, false);
			}
			bool flag2 = this.auth_state.NtlmAuthState == HttpWebRequest.NtlmAuthState.Challenge || this.proxy_auth_state.NtlmAuthState == HttpWebRequest.NtlmAuthState.Challenge;
			WebOperation webOperation = new WebOperation(this, writeBuffer, flag2, cancellationToken);
			stream.Operation.SetPriorityRequest(webOperation);
			ICredentials credentials = ((!flag || this.proxy == null) ? this.credentials : this.proxy.Credentials);
			if (credentials != null)
			{
				stream.Connection.NtlmCredential = credentials.GetCredential(this.requestUri, "NTLM");
				stream.Connection.UnsafeAuthenticatedConnectionSharing = this.unsafe_auth_blah;
			}
			return new ValueTuple<WebOperation, bool>(webOperation, flag2);
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x0009DD28 File Offset: 0x0009BF28
		private bool CheckAuthorization(WebResponse response, HttpStatusCode code)
		{
			if (code != HttpStatusCode.ProxyAuthenticationRequired)
			{
				return this.auth_state.CheckAuthorization(response, code);
			}
			return this.proxy_auth_state.CheckAuthorization(response, code);
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x0009DD50 File Offset: 0x0009BF50
		[return: TupleElementNames(new string[] { "task", "throwMe" })]
		private ValueTuple<Task<BufferOffsetSize>, WebException> GetRewriteHandler(HttpWebResponse response, bool redirect)
		{
			if (redirect)
			{
				if (!this.MethodWithBuffer)
				{
					return new ValueTuple<Task<BufferOffsetSize>, WebException>(null, null);
				}
				if (this.writeStream.WriteBufferLength == 0 || this.contentLength == 0L)
				{
					return new ValueTuple<Task<BufferOffsetSize>, WebException>(null, null);
				}
			}
			if (this.AllowWriteStreamBuffering)
			{
				return new ValueTuple<Task<BufferOffsetSize>, WebException>(Task.FromResult<BufferOffsetSize>(this.writeStream.GetWriteBuffer()), null);
			}
			if (this.ResendContentFactory == null)
			{
				return new ValueTuple<Task<BufferOffsetSize>, WebException>(null, new WebException("The request requires buffering data to succeed.", null, WebExceptionStatus.ProtocolError, response));
			}
			return new ValueTuple<Task<BufferOffsetSize>, WebException>(async delegate
			{
				BufferOffsetSize bufferOffsetSize;
				using (MemoryStream ms = new MemoryStream())
				{
					await this.ResendContentFactory(ms).ConfigureAwait(false);
					byte[] array = ms.ToArray();
					bufferOffsetSize = new BufferOffsetSize(array, 0, array.Length, false);
				}
				return bufferOffsetSize;
			}(), null);
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x0009DDE0 File Offset: 0x0009BFE0
		[return: TupleElementNames(new string[] { "redirect", "mustReadAll", "writeBuffer", "throwMe" })]
		private ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException> CheckFinalStatus(HttpWebResponse response)
		{
			WebException ex = null;
			bool flag = false;
			Task<BufferOffsetSize> task = null;
			HttpStatusCode statusCode = response.StatusCode;
			if (((!this.auth_state.IsCompleted && statusCode == HttpStatusCode.Unauthorized && this.credentials != null) || (this.ProxyQuery && !this.proxy_auth_state.IsCompleted && statusCode == HttpStatusCode.ProxyAuthenticationRequired)) && !this.usedPreAuth && this.CheckAuthorization(response, statusCode))
			{
				flag = true;
				if (!this.MethodWithBuffer)
				{
					return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(true, flag, null, null);
				}
				ValueTuple<Task<BufferOffsetSize>, WebException> rewriteHandler = this.GetRewriteHandler(response, false);
				task = rewriteHandler.Item1;
				ex = rewriteHandler.Item2;
				if (ex == null)
				{
					return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(true, flag, task, null);
				}
				if (!this.ThrowOnError)
				{
					return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(false, flag, null, null);
				}
				this.writeStream.InternalClose();
				this.writeStream = null;
				response.Close();
				return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(false, flag, null, ex);
			}
			else
			{
				if (statusCode >= HttpStatusCode.BadRequest)
				{
					ex = new WebException(string.Format("The remote server returned an error: ({0}) {1}.", (int)statusCode, response.StatusDescription), null, WebExceptionStatus.ProtocolError, response);
					flag = true;
				}
				else if (statusCode == HttpStatusCode.NotModified && this.allowAutoRedirect)
				{
					ex = new WebException(string.Format("The remote server returned an error: ({0}) {1}.", (int)statusCode, response.StatusDescription), null, WebExceptionStatus.ProtocolError, response);
				}
				else if (statusCode >= HttpStatusCode.MultipleChoices && this.allowAutoRedirect && this.redirects >= this.maxAutoRedirect)
				{
					ex = new WebException("Max. redirections exceeded.", null, WebExceptionStatus.ProtocolError, response);
					flag = true;
				}
				if (ex == null)
				{
					int num = (int)statusCode;
					bool flag2 = false;
					if (this.allowAutoRedirect && num >= 300)
					{
						flag2 = this.Redirect(statusCode, response);
						ValueTuple<Task<BufferOffsetSize>, WebException> rewriteHandler2 = this.GetRewriteHandler(response, true);
						task = rewriteHandler2.Item1;
						ex = rewriteHandler2.Item2;
						if (flag2 && !this.unsafe_auth_blah)
						{
							this.auth_state.Reset();
							this.proxy_auth_state.Reset();
						}
					}
					if (num >= 300 && num != 304)
					{
						flag = true;
					}
					if (ex == null)
					{
						return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(flag2, flag, task, null);
					}
				}
				if (!this.ThrowOnError)
				{
					return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(false, flag, null, null);
				}
				if (this.writeStream != null)
				{
					this.writeStream.InternalClose();
					this.writeStream = null;
				}
				return new ValueTuple<bool, bool, Task<BufferOffsetSize>, WebException>(false, flag, null, ex);
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x0600291A RID: 10522 RVA: 0x0009DFF4 File Offset: 0x0009C1F4
		// (set) Token: 0x0600291B RID: 10523 RVA: 0x0009DFFC File Offset: 0x0009C1FC
		internal bool ReuseConnection { get; set; }

		// Token: 0x0600291C RID: 10524 RVA: 0x0009E008 File Offset: 0x0009C208
		internal static StringBuilder GenerateConnectionGroup(string connectionGroupName, bool unsafeConnectionGroup, bool isInternalGroup)
		{
			StringBuilder stringBuilder = new StringBuilder(connectionGroupName);
			stringBuilder.Append(unsafeConnectionGroup ? "U>" : "S>");
			if (isInternalGroup)
			{
				stringBuilder.Append("I>");
			}
			return stringBuilder;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebRequest" /> class.</summary>
		// Token: 0x0600291E RID: 10526 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		public HttpWebRequest()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040021F2 RID: 8690
		private Uri requestUri;

		// Token: 0x040021F3 RID: 8691
		private Uri actualUri;

		// Token: 0x040021F4 RID: 8692
		private bool hostChanged;

		// Token: 0x040021F5 RID: 8693
		private bool allowAutoRedirect;

		// Token: 0x040021F6 RID: 8694
		private bool allowBuffering;

		// Token: 0x040021F7 RID: 8695
		private X509CertificateCollection certificates;

		// Token: 0x040021F8 RID: 8696
		private string connectionGroup;

		// Token: 0x040021F9 RID: 8697
		private bool haveContentLength;

		// Token: 0x040021FA RID: 8698
		private long contentLength;

		// Token: 0x040021FB RID: 8699
		private HttpContinueDelegate continueDelegate;

		// Token: 0x040021FC RID: 8700
		private CookieContainer cookieContainer;

		// Token: 0x040021FD RID: 8701
		private ICredentials credentials;

		// Token: 0x040021FE RID: 8702
		private bool haveResponse;

		// Token: 0x040021FF RID: 8703
		private bool requestSent;

		// Token: 0x04002200 RID: 8704
		private WebHeaderCollection webHeaders;

		// Token: 0x04002201 RID: 8705
		private bool keepAlive;

		// Token: 0x04002202 RID: 8706
		private int maxAutoRedirect;

		// Token: 0x04002203 RID: 8707
		private string mediaType;

		// Token: 0x04002204 RID: 8708
		private string method;

		// Token: 0x04002205 RID: 8709
		private string initialMethod;

		// Token: 0x04002206 RID: 8710
		private bool pipelined;

		// Token: 0x04002207 RID: 8711
		private bool preAuthenticate;

		// Token: 0x04002208 RID: 8712
		private bool usedPreAuth;

		// Token: 0x04002209 RID: 8713
		private Version version;

		// Token: 0x0400220A RID: 8714
		private bool force_version;

		// Token: 0x0400220B RID: 8715
		private Version actualVersion;

		// Token: 0x0400220C RID: 8716
		private IWebProxy proxy;

		// Token: 0x0400220D RID: 8717
		private bool sendChunked;

		// Token: 0x0400220E RID: 8718
		private ServicePoint servicePoint;

		// Token: 0x0400220F RID: 8719
		private int timeout;

		// Token: 0x04002210 RID: 8720
		private WebRequestStream writeStream;

		// Token: 0x04002211 RID: 8721
		private HttpWebResponse webResponse;

		// Token: 0x04002212 RID: 8722
		private WebCompletionSource responseTask;

		// Token: 0x04002213 RID: 8723
		private WebOperation currentOperation;

		// Token: 0x04002214 RID: 8724
		private int aborted;

		// Token: 0x04002215 RID: 8725
		private bool gotRequestStream;

		// Token: 0x04002216 RID: 8726
		private int redirects;

		// Token: 0x04002217 RID: 8727
		private bool expectContinue;

		// Token: 0x04002218 RID: 8728
		private bool getResponseCalled;

		// Token: 0x04002219 RID: 8729
		private object locker;

		// Token: 0x0400221A RID: 8730
		private bool finished_reading;

		// Token: 0x0400221B RID: 8731
		private DecompressionMethods auto_decomp;

		// Token: 0x0400221C RID: 8732
		private int maxResponseHeadersLength;

		// Token: 0x0400221D RID: 8733
		private static int defaultMaxResponseHeadersLength = 65536;

		// Token: 0x0400221E RID: 8734
		private int readWriteTimeout;

		// Token: 0x0400221F RID: 8735
		private MonoTlsProvider tlsProvider;

		// Token: 0x04002220 RID: 8736
		private MonoTlsSettings tlsSettings;

		// Token: 0x04002221 RID: 8737
		private ServerCertValidationCallback certValidationCallback;

		// Token: 0x04002222 RID: 8738
		private HttpWebRequest.AuthorizationState auth_state;

		// Token: 0x04002223 RID: 8739
		private HttpWebRequest.AuthorizationState proxy_auth_state;

		// Token: 0x04002224 RID: 8740
		private string host;

		// Token: 0x04002225 RID: 8741
		[NonSerialized]
		internal Func<Stream, Task> ResendContentFactory;

		// Token: 0x04002226 RID: 8742
		internal readonly int ID;

		// Token: 0x04002228 RID: 8744
		private bool unsafe_auth_blah;

		// Token: 0x0200052C RID: 1324
		private enum NtlmAuthState
		{
			// Token: 0x0400222B RID: 8747
			None,
			// Token: 0x0400222C RID: 8748
			Challenge,
			// Token: 0x0400222D RID: 8749
			Response
		}

		// Token: 0x0200052D RID: 1325
		private struct AuthorizationState
		{
			// Token: 0x170008A0 RID: 2208
			// (get) Token: 0x0600291F RID: 10527 RVA: 0x0009E089 File Offset: 0x0009C289
			public bool IsCompleted
			{
				get
				{
					return this.isCompleted;
				}
			}

			// Token: 0x170008A1 RID: 2209
			// (get) Token: 0x06002920 RID: 10528 RVA: 0x0009E091 File Offset: 0x0009C291
			public HttpWebRequest.NtlmAuthState NtlmAuthState
			{
				get
				{
					return this.ntlm_auth_state;
				}
			}

			// Token: 0x170008A2 RID: 2210
			// (get) Token: 0x06002921 RID: 10529 RVA: 0x0009E099 File Offset: 0x0009C299
			public bool IsNtlmAuthenticated
			{
				get
				{
					return this.isCompleted && this.ntlm_auth_state > HttpWebRequest.NtlmAuthState.None;
				}
			}

			// Token: 0x06002922 RID: 10530 RVA: 0x0009E0AE File Offset: 0x0009C2AE
			public AuthorizationState(HttpWebRequest request, bool isProxy)
			{
				this.request = request;
				this.isProxy = isProxy;
				this.isCompleted = false;
				this.ntlm_auth_state = HttpWebRequest.NtlmAuthState.None;
			}

			// Token: 0x06002923 RID: 10531 RVA: 0x0009E0CC File Offset: 0x0009C2CC
			public bool CheckAuthorization(WebResponse response, HttpStatusCode code)
			{
				this.isCompleted = false;
				if (code == HttpStatusCode.Unauthorized && this.request.credentials == null)
				{
					return false;
				}
				if (this.isProxy != (code == HttpStatusCode.ProxyAuthenticationRequired))
				{
					return false;
				}
				if (this.isProxy && (this.request.proxy == null || this.request.proxy.Credentials == null))
				{
					return false;
				}
				string[] values = response.Headers.GetValues(this.isProxy ? "Proxy-Authenticate" : "WWW-Authenticate");
				if (values == null || values.Length == 0)
				{
					return false;
				}
				ICredentials credentials = ((!this.isProxy) ? this.request.credentials : this.request.proxy.Credentials);
				Authorization authorization = null;
				string[] array = values;
				for (int i = 0; i < array.Length; i++)
				{
					authorization = AuthenticationManager.Authenticate(array[i], this.request, credentials);
					if (authorization != null)
					{
						break;
					}
				}
				if (authorization == null)
				{
					return false;
				}
				this.request.webHeaders[this.isProxy ? "Proxy-Authorization" : "Authorization"] = authorization.Message;
				this.isCompleted = authorization.Complete;
				if (authorization.ModuleAuthenticationType == "NTLM")
				{
					this.ntlm_auth_state++;
				}
				return true;
			}

			// Token: 0x06002924 RID: 10532 RVA: 0x0009E207 File Offset: 0x0009C407
			public void Reset()
			{
				this.isCompleted = false;
				this.ntlm_auth_state = HttpWebRequest.NtlmAuthState.None;
				this.request.webHeaders.RemoveInternal(this.isProxy ? "Proxy-Authorization" : "Authorization");
			}

			// Token: 0x06002925 RID: 10533 RVA: 0x0009E23B File Offset: 0x0009C43B
			public override string ToString()
			{
				return string.Format("{0}AuthState [{1}:{2}]", this.isProxy ? "Proxy" : "", this.isCompleted, this.ntlm_auth_state);
			}

			// Token: 0x0400222E RID: 8750
			private readonly HttpWebRequest request;

			// Token: 0x0400222F RID: 8751
			private readonly bool isProxy;

			// Token: 0x04002230 RID: 8752
			private bool isCompleted;

			// Token: 0x04002231 RID: 8753
			private HttpWebRequest.NtlmAuthState ntlm_auth_state;
		}
	}
}
