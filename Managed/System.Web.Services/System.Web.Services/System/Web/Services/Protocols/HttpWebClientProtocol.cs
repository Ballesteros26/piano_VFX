using System;
using System.Collections;
using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Web.Services.Protocols
{
	/// <summary>Represents the base class for all XML Web service client proxies that use the HTTP transport protocol.</summary>
	// Token: 0x02000021 RID: 33
	[ComVisible(true)]
	public abstract class HttpWebClientProtocol : WebClientProtocol
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.HttpWebClientProtocol" /> class.</summary>
		// Token: 0x060000B2 RID: 178 RVA: 0x0000388E File Offset: 0x00001A8E
		protected HttpWebClientProtocol()
		{
			this.allowAutoRedirect = false;
			this.userAgent = HttpWebClientProtocol.UserAgentDefault;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000038A8 File Offset: 0x00001AA8
		internal HttpWebClientProtocol(HttpWebClientProtocol protocol)
			: base(protocol)
		{
			this.allowAutoRedirect = protocol.allowAutoRedirect;
			this.enableDecompression = protocol.enableDecompression;
			this.cookieJar = protocol.cookieJar;
			this.clientCertificates = protocol.clientCertificates;
			this.proxy = protocol.proxy;
			this.userAgent = protocol.userAgent;
		}

		/// <summary>Gets or sets whether the client automatically follows server redirects.</summary>
		/// <returns>true to automatically redirect the client to follow server redirects; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.Net.WebException">The <see cref="P:System.Web.Services.Protocols.HttpWebClientProtocol.AllowAutoRedirect" /> property is false and the Web server attempts to redirect the request. </exception>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003904 File Offset: 0x00001B04
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000390C File Offset: 0x00001B0C
		[DefaultValue(false)]
		[WebServicesDescription("ClientProtocolAllowAutoRedirect")]
		public bool AllowAutoRedirect
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

		/// <summary>Gets or sets the collection of cookies.</summary>
		/// <returns>A <see cref="T:System.Net.CookieContainer" /> that represents the cookies for a Web Services client.</returns>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003915 File Offset: 0x00001B15
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000391D File Offset: 0x00001B1D
		[WebServicesDescription("ClientProtocolCookieContainer")]
		[DefaultValue(null)]
		public CookieContainer CookieContainer
		{
			get
			{
				return this.cookieJar;
			}
			set
			{
				this.cookieJar = value;
			}
		}

		/// <summary>Gets the collection of client certificates.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> that represents the client certificates.</returns>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003926 File Offset: 0x00001B26
		[WebServicesDescription("ClientProtocolClientCertificates")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this.clientCertificates == null)
				{
					this.clientCertificates = new X509CertificateCollection();
				}
				return this.clientCertificates;
			}
		}

		/// <summary>Gets or sets a value that indicates whether decompression is enabled for this <see cref="T:System.Web.Services.Protocols.HttpWebClientProtocol" />. </summary>
		/// <returns>true if decompression is enabled for this <see cref="T:System.Web.Services.Protocols.HttpWebClientProtocol" />; otherwise, false. The default is false.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003941 File Offset: 0x00001B41
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003949 File Offset: 0x00001B49
		[DefaultValue(false)]
		[WebServicesDescription("ClientProtocolEnableDecompression")]
		public bool EnableDecompression
		{
			get
			{
				return this.enableDecompression;
			}
			set
			{
				this.enableDecompression = value;
			}
		}

		/// <summary>Gets or sets the value for the user agent header that is sent with each request.</summary>
		/// <returns>The value of the HTTP protocol user agent header. The default is "MS Web Services Client Protocol <paramref name="number" /> ", where <paramref name="number" /> is the version of the common language runtime (for example, 1.0.3705.0).</returns>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003952 File Offset: 0x00001B52
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00003968 File Offset: 0x00001B68
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebServicesDescription("ClientProtocolUserAgent")]
		[Browsable(false)]
		public string UserAgent
		{
			get
			{
				if (this.userAgent != null)
				{
					return this.userAgent;
				}
				return string.Empty;
			}
			set
			{
				this.userAgent = value;
			}
		}

		/// <summary>Gets or sets proxy information for making an XML Web service request through a firewall.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> that contains the proxy information for making requests through a firewall. The default value is the operating system proxy settings.</returns>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003971 File Offset: 0x00001B71
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00003979 File Offset: 0x00001B79
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IWebProxy Proxy
		{
			get
			{
				return this.proxy;
			}
			set
			{
				this.proxy = value;
			}
		}

		/// <summary>Creates a <see cref="T:System.Net.WebRequest" /> for the specified URI.</summary>
		/// <returns>The created <see cref="T:System.Net.WebRequest" />.</returns>
		/// <param name="uri">The <see cref="T:System.Uri" /> for creating the <see cref="T:System.Net.WebRequest" />. </param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="uri" /> parameter is null. </exception>
		// Token: 0x060000BF RID: 191 RVA: 0x00003984 File Offset: 0x00001B84
		protected override WebRequest GetWebRequest(Uri uri)
		{
			WebRequest webRequest = base.GetWebRequest(uri);
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest != null)
			{
				httpWebRequest.UserAgent = this.UserAgent;
				httpWebRequest.AllowAutoRedirect = this.allowAutoRedirect;
				httpWebRequest.AutomaticDecompression = (this.enableDecompression ? DecompressionMethods.GZip : DecompressionMethods.None);
				httpWebRequest.AllowWriteStreamBuffering = true;
				httpWebRequest.SendChunked = false;
				if (this.unsafeAuthenticatedConnectionSharing != httpWebRequest.UnsafeAuthenticatedConnectionSharing)
				{
					httpWebRequest.UnsafeAuthenticatedConnectionSharing = this.unsafeAuthenticatedConnectionSharing;
				}
				if (this.proxy != null)
				{
					httpWebRequest.Proxy = this.proxy;
				}
				if (this.clientCertificates != null && this.clientCertificates.Count > 0)
				{
					httpWebRequest.ClientCertificates.AddRange(this.clientCertificates);
				}
				httpWebRequest.CookieContainer = this.cookieJar;
			}
			return webRequest;
		}

		/// <summary>Returns a response from a synchronous request to an XML Web service method.</summary>
		/// <returns>A response from a synchronous request to an XML Web service method.</returns>
		/// <param name="request">The <see cref="T:System.Net.WebRequest" /> from which to get the response. </param>
		// Token: 0x060000C0 RID: 192 RVA: 0x00003A3E File Offset: 0x00001C3E
		protected override WebResponse GetWebResponse(WebRequest request)
		{
			return base.GetWebResponse(request);
		}

		/// <summary>Returns a response from an asynchronous request to an XML Web service method.</summary>
		/// <returns>A response from an asynchronous request to an XML Web service method.</returns>
		/// <param name="request">The <see cref="T:System.Net.WebRequest" /> from which to get the response. </param>
		/// <param name="result">The <see cref="T:System.IAsyncResult" /> to pass to <see cref="M:System.Net.HttpWebRequest.EndGetResponse(System.IAsyncResult)" /> when the response has completed. </param>
		// Token: 0x060000C1 RID: 193 RVA: 0x00003A47 File Offset: 0x00001C47
		protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
		{
			return base.GetWebResponse(request, result);
		}

		/// <summary>Gets or sets a value that indicates whether connection sharing is enabled when the client uses NTLM authentication to connect to the Web server that hosts the XML Web service.</summary>
		/// <returns>true if connection sharing is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003A51 File Offset: 0x00001C51
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003A59 File Offset: 0x00001C59
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.unsafeAuthenticatedConnectionSharing;
			}
			set
			{
				this.unsafeAuthenticatedConnectionSharing = value;
			}
		}

		/// <summary>Cancels an asynchronous call to an XML Web service method, unless the call has already completed.</summary>
		/// <param name="userState">The object provided in the last parameter to the asynchronous call of the Begin method.</param>
		// Token: 0x060000C4 RID: 196 RVA: 0x00003A64 File Offset: 0x00001C64
		protected void CancelAsync(object userState)
		{
			if (userState == null)
			{
				userState = base.NullToken;
			}
			WebClientAsyncResult webClientAsyncResult = this.OperationCompleted(userState, new object[1], null, true);
			if (webClientAsyncResult != null)
			{
				webClientAsyncResult.Abort();
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003A98 File Offset: 0x00001C98
		internal WebClientAsyncResult OperationCompleted(object userState, object[] parameters, Exception e, bool canceled)
		{
			WebClientAsyncResult webClientAsyncResult = (WebClientAsyncResult)base.AsyncInvokes[userState];
			if (webClientAsyncResult != null)
			{
				AsyncOperation asyncOperation = (AsyncOperation)webClientAsyncResult.AsyncState;
				UserToken userToken = (UserToken)asyncOperation.UserSuppliedState;
				InvokeCompletedEventArgs invokeCompletedEventArgs = new InvokeCompletedEventArgs(parameters, e, canceled, userState);
				base.AsyncInvokes.Remove(userState);
				asyncOperation.PostOperationCompleted(userToken.Callback, invokeCompletedEventArgs);
			}
			return webClientAsyncResult;
		}

		/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlMembersMapping" /> for each XML Web service method exposed by the specified type, and stores the mappings in the specified <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>true if <paramref name="type" /> can be assigned to a <see cref="T:System.Web.Services.Protocols.SoapHttpClientProtocol" />; otherwise, false.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> that exposes the XML Web service methods.</param>
		/// <param name="mappings">A <see cref="T:System.Collections.ArrayList" /> that is used to store the mappings.</param>
		// Token: 0x060000C6 RID: 198 RVA: 0x00003AF8 File Offset: 0x00001CF8
		public static bool GenerateXmlMappings(Type type, ArrayList mappings)
		{
			if (!typeof(SoapHttpClientProtocol).IsAssignableFrom(type))
			{
				return false;
			}
			WebServiceBindingAttribute attribute = WebServiceBindingReflector.GetAttribute(type);
			if (attribute == null)
			{
				throw new InvalidOperationException(Res.GetString("WebClientBindingAttributeRequired"));
			}
			string @namespace = attribute.Namespace;
			bool flag = SoapReflector.ServiceDefaultIsEncoded(type);
			ArrayList arrayList = new ArrayList();
			SoapClientType.GenerateXmlMappings(type, arrayList, @namespace, flag, mappings);
			return true;
		}

		/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlMembersMapping" /> for each XML Web service method exposed by the specified types, and stores the mappings in the specified <see cref="T:System.Collections.ArrayList" />, as well as in a <see cref="T:System.Collections.Hashtable" /> that this method returns.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> that contains the <see cref="T:System.Xml.Serialization.XmlMembersMapping" /> for each XML Web service method exposed by the specified types. The types contained in <paramref name="types" /> are used as keys.</returns>
		/// <param name="types">An array of type <see cref="T:System.Type" /> that contains the types that expose the XML Web service methods.</param>
		/// <param name="mappings">A <see cref="T:System.Collections.ArrayList" /> that is used to store the mappings.</param>
		// Token: 0x060000C7 RID: 199 RVA: 0x00003B50 File Offset: 0x00001D50
		public static Hashtable GenerateXmlMappings(Type[] types, ArrayList mappings)
		{
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			Hashtable hashtable = new Hashtable();
			foreach (Type type in types)
			{
				ArrayList arrayList = new ArrayList();
				if (HttpWebClientProtocol.GenerateXmlMappings(type, mappings))
				{
					hashtable.Add(type, arrayList);
					mappings.Add(arrayList);
				}
			}
			return hashtable;
		}

		// Token: 0x040001C7 RID: 455
		private bool allowAutoRedirect;

		// Token: 0x040001C8 RID: 456
		private bool enableDecompression;

		// Token: 0x040001C9 RID: 457
		private CookieContainer cookieJar;

		// Token: 0x040001CA RID: 458
		private X509CertificateCollection clientCertificates;

		// Token: 0x040001CB RID: 459
		private IWebProxy proxy;

		// Token: 0x040001CC RID: 460
		private static string UserAgentDefault = "Mozilla/4.0 (compatible; MSIE 6.0; MS Web Services Client Protocol " + Environment.Version.ToString() + ")";

		// Token: 0x040001CD RID: 461
		private string userAgent;

		// Token: 0x040001CE RID: 462
		private bool unsafeAuthenticatedConnectionSharing;
	}
}
