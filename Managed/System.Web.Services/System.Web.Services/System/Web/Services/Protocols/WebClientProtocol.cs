using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Services.Diagnostics;

namespace System.Web.Services.Protocols
{
	/// <summary>Specifies the base class for all XML Web service client proxies created using ASP.NET.</summary>
	// Token: 0x0200001C RID: 28
	[ComVisible(true)]
	public abstract class WebClientProtocol : Component
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002DBC File Offset: 0x00000FBC
		internal static object InternalSyncObject
		{
			get
			{
				if (WebClientProtocol.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref WebClientProtocol.s_InternalSyncObject, obj, null);
				}
				return WebClientProtocol.s_InternalSyncObject;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.WebClientProtocol" /> class.</summary>
		// Token: 0x06000074 RID: 116 RVA: 0x00002DF4 File Offset: 0x00000FF4
		protected WebClientProtocol()
		{
			this.timeout = 100000;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002E24 File Offset: 0x00001024
		internal WebClientProtocol(WebClientProtocol protocol)
		{
			this.credentials = protocol.credentials;
			this.uri = protocol.uri;
			this.timeout = protocol.timeout;
			this.connectionGroupName = protocol.connectionGroupName;
			this.requestEncoding = protocol.requestEncoding;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002E8E File Offset: 0x0000108E
		internal static RequestCachePolicy BypassCache
		{
			get
			{
				if (WebClientProtocol.bypassCache == null)
				{
					WebClientProtocol.bypassCache = new RequestCachePolicy(RequestCacheLevel.BypassCache);
				}
				return WebClientProtocol.bypassCache;
			}
		}

		/// <summary>Gets or sets security credentials for XML Web service client authentication.</summary>
		/// <returns>The <see cref="T:System.Net.ICredentials" /> for the XML Web service client.</returns>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002EA7 File Offset: 0x000010A7
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002EAF File Offset: 0x000010AF
		public ICredentials Credentials
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

		/// <summary>Gets or sets a value that indicates whether to set the <see cref="P:System.Web.Services.Protocols.WebClientProtocol.Credentials" /> property to the value of the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> property.</summary>
		/// <returns>true if the Credentials property is set to the value of the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> property; otherwise, false.</returns>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002EB8 File Offset: 0x000010B8
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002ECA File Offset: 0x000010CA
		public bool UseDefaultCredentials
		{
			get
			{
				return this.credentials == CredentialCache.DefaultCredentials;
			}
			set
			{
				this.credentials = (value ? CredentialCache.DefaultCredentials : null);
			}
		}

		/// <summary>Gets or sets the name of the connection group for the request.</summary>
		/// <returns>The name of the connection group. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002EDD File Offset: 0x000010DD
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002EF3 File Offset: 0x000010F3
		[DefaultValue("")]
		public string ConnectionGroupName
		{
			get
			{
				if (this.connectionGroupName != null)
				{
					return this.connectionGroupName;
				}
				return string.Empty;
			}
			set
			{
				this.connectionGroupName = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002EFC File Offset: 0x000010FC
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002F04 File Offset: 0x00001104
		internal WebRequest PendingSyncRequest
		{
			get
			{
				return this.pendingSyncRequest;
			}
			set
			{
				this.pendingSyncRequest = value;
			}
		}

		/// <summary>Gets or sets whether pre-authentication is enabled.</summary>
		/// <returns>true to pre-authenticate the request; otherwise, false. The default is false.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002F0D File Offset: 0x0000110D
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002F15 File Offset: 0x00001115
		[WebServicesDescription("ClientProtocolPreAuthenticate")]
		[DefaultValue(false)]
		public bool PreAuthenticate
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

		/// <summary>Gets or sets the base URL of the XML Web service the client is requesting.</summary>
		/// <returns>The base URL of the XML Web service the client is requesting. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002F1E File Offset: 0x0000111E
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002F3F File Offset: 0x0000113F
		[WebServicesDescription("ClientProtocolUrl")]
		[SettingsBindable(true)]
		[DefaultValue("")]
		public string Url
		{
			get
			{
				if (!(this.uri == null))
				{
					return this.uri.ToString();
				}
				return string.Empty;
			}
			set
			{
				this.uri = new Uri(value);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002F4D File Offset: 0x0000114D
		internal Hashtable AsyncInvokes
		{
			get
			{
				return this.asyncInvokes;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002F55 File Offset: 0x00001155
		internal object NullToken
		{
			get
			{
				return this.nullToken;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002F5D File Offset: 0x0000115D
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002F65 File Offset: 0x00001165
		internal Uri Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		/// <summary>The <see cref="T:System.Text.Encoding" /> used to make the client request to the XML Web service.</summary>
		/// <returns>The character encoding for the client request. The default is null, which uses the default encoding for the underlying transport and protocol.</returns>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002F6E File Offset: 0x0000116E
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00002F76 File Offset: 0x00001176
		[DefaultValue(null)]
		[SettingsBindable(true)]
		[WebServicesDescription("ClientProtocolEncoding")]
		public Encoding RequestEncoding
		{
			get
			{
				return this.requestEncoding;
			}
			set
			{
				this.requestEncoding = value;
			}
		}

		/// <summary>Indicates the time an XML Web service client waits for the reply to a synchronous XML Web service request to arrive (in milliseconds).</summary>
		/// <returns>The time out, in milliseconds, for synchronous calls to the XML Web service. The default is 100000 milliseconds.</returns>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002F7F File Offset: 0x0000117F
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00002F87 File Offset: 0x00001187
		[WebServicesDescription("ClientProtocolTimeout")]
		[DefaultValue(100000)]
		[SettingsBindable(true)]
		public int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.timeout = ((value < -1) ? (-1) : value);
			}
		}

		/// <summary>Cancels a request to an XML Web service method.</summary>
		// Token: 0x0600008B RID: 139 RVA: 0x00002F98 File Offset: 0x00001198
		public virtual void Abort()
		{
			WebRequest webRequest = this.PendingSyncRequest;
			if (webRequest != null)
			{
				webRequest.Abort();
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002FB8 File Offset: 0x000011B8
		internal IAsyncResult BeginSend(Uri requestUri, WebClientAsyncResult asyncResult, bool callWriteAsyncRequest)
		{
			if (WebClientProtocol.readResponseAsyncCallback == null)
			{
				object internalSyncObject = WebClientProtocol.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (WebClientProtocol.readResponseAsyncCallback == null)
					{
						WebClientProtocol.getRequestStreamAsyncCallback = new AsyncCallback(WebClientProtocol.GetRequestStreamAsyncCallback);
						WebClientProtocol.getResponseAsyncCallback = new AsyncCallback(WebClientProtocol.GetResponseAsyncCallback);
						WebClientProtocol.readResponseAsyncCallback = new AsyncCallback(WebClientProtocol.ReadResponseAsyncCallback);
					}
				}
			}
			WebRequest webRequest = this.GetWebRequest(requestUri);
			asyncResult.Request = webRequest;
			this.InitializeAsyncRequest(webRequest, asyncResult.InternalAsyncState);
			if (callWriteAsyncRequest)
			{
				webRequest.BeginGetRequestStream(WebClientProtocol.getRequestStreamAsyncCallback, asyncResult);
			}
			else
			{
				webRequest.BeginGetResponse(WebClientProtocol.getResponseAsyncCallback, asyncResult);
			}
			if (!asyncResult.IsCompleted)
			{
				asyncResult.CombineCompletedSynchronously(false);
			}
			return asyncResult;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003084 File Offset: 0x00001284
		private static void ProcessAsyncException(WebClientAsyncResult client, Exception e, string method)
		{
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Error, typeof(WebClientProtocol), method, e);
			}
			WebException ex = e as WebException;
			if (ex != null && ex.Response != null)
			{
				client.Response = ex.Response;
				return;
			}
			if (client.IsCompleted)
			{
				throw new InvalidOperationException(Res.GetString("ThereWasAnErrorDuringAsyncProcessing"), e);
			}
			client.Complete(e);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000030EC File Offset: 0x000012EC
		private static void GetRequestStreamAsyncCallback(IAsyncResult asyncResult)
		{
			WebClientAsyncResult webClientAsyncResult = (WebClientAsyncResult)asyncResult.AsyncState;
			webClientAsyncResult.CombineCompletedSynchronously(asyncResult.CompletedSynchronously);
			bool flag = true;
			try
			{
				Stream stream = webClientAsyncResult.Request.EndGetRequestStream(asyncResult);
				flag = false;
				try
				{
					webClientAsyncResult.ClientProtocol.AsyncBufferedSerialize(webClientAsyncResult.Request, stream, webClientAsyncResult.InternalAsyncState);
				}
				finally
				{
					stream.Close();
				}
				webClientAsyncResult.Request.BeginGetResponse(WebClientProtocol.getResponseAsyncCallback, webClientAsyncResult);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				WebClientProtocol.ProcessAsyncException(webClientAsyncResult, ex, "GetRequestStreamAsyncCallback");
				if (flag)
				{
					WebException ex2 = ex as WebException;
					if (ex2 != null && ex2.Response != null)
					{
						webClientAsyncResult.Complete(ex);
					}
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000031BC File Offset: 0x000013BC
		private static void GetResponseAsyncCallback(IAsyncResult asyncResult)
		{
			WebClientAsyncResult webClientAsyncResult = (WebClientAsyncResult)asyncResult.AsyncState;
			webClientAsyncResult.CombineCompletedSynchronously(asyncResult.CompletedSynchronously);
			try
			{
				webClientAsyncResult.Response = webClientAsyncResult.ClientProtocol.GetWebResponse(webClientAsyncResult.Request, asyncResult);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				WebClientProtocol.ProcessAsyncException(webClientAsyncResult, ex, "GetResponseAsyncCallback");
				if (webClientAsyncResult.Response == null)
				{
					return;
				}
			}
			WebClientProtocol.ReadAsyncResponse(webClientAsyncResult);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003244 File Offset: 0x00001444
		private static void ReadAsyncResponse(WebClientAsyncResult client)
		{
			if (client.Response.ContentLength == 0L)
			{
				client.Complete();
				return;
			}
			try
			{
				client.ResponseStream = client.Response.GetResponseStream();
				WebClientProtocol.ReadAsyncResponseStream(client);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				WebClientProtocol.ProcessAsyncException(client, ex, "ReadAsyncResponse");
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000032B8 File Offset: 0x000014B8
		private static void ReadAsyncResponseStream(WebClientAsyncResult client)
		{
			for (;;)
			{
				byte[] array = client.Buffer;
				long contentLength = client.Response.ContentLength;
				if (array == null)
				{
					array = (client.Buffer = new byte[(contentLength == -1L) ? 1024L : contentLength]);
				}
				else if (contentLength != -1L && contentLength > (long)array.Length)
				{
					array = (client.Buffer = new byte[contentLength]);
				}
				IAsyncResult asyncResult = client.ResponseStream.BeginRead(array, 0, array.Length, WebClientProtocol.readResponseAsyncCallback, client);
				if (!asyncResult.CompletedSynchronously)
				{
					break;
				}
				if (WebClientProtocol.ProcessAsyncResponseStreamResult(client, asyncResult))
				{
					return;
				}
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003344 File Offset: 0x00001544
		private static bool ProcessAsyncResponseStreamResult(WebClientAsyncResult client, IAsyncResult asyncResult)
		{
			int num = client.ResponseStream.EndRead(asyncResult);
			long contentLength = client.Response.ContentLength;
			bool flag;
			if (contentLength > 0L && (long)num == contentLength)
			{
				client.ResponseBufferedStream = new MemoryStream(client.Buffer);
				flag = true;
			}
			else if (num > 0)
			{
				if (client.ResponseBufferedStream == null)
				{
					int num2 = (int)((contentLength == -1L) ? ((long)client.Buffer.Length) : contentLength);
					client.ResponseBufferedStream = new MemoryStream(num2);
				}
				client.ResponseBufferedStream.Write(client.Buffer, 0, num);
				flag = false;
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				client.Complete();
			}
			return flag;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000033D8 File Offset: 0x000015D8
		private static void ReadResponseAsyncCallback(IAsyncResult asyncResult)
		{
			WebClientAsyncResult webClientAsyncResult = (WebClientAsyncResult)asyncResult.AsyncState;
			webClientAsyncResult.CombineCompletedSynchronously(asyncResult.CompletedSynchronously);
			if (asyncResult.CompletedSynchronously)
			{
				return;
			}
			try
			{
				if (!WebClientProtocol.ProcessAsyncResponseStreamResult(webClientAsyncResult, asyncResult))
				{
					WebClientProtocol.ReadAsyncResponseStream(webClientAsyncResult);
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				WebClientProtocol.ProcessAsyncException(webClientAsyncResult, ex, "ReadResponseAsyncCallback");
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000210D File Offset: 0x0000030D
		internal void NotifyClientCallOut(WebRequest request)
		{
		}

		/// <summary>Creates a <see cref="T:System.Net.WebRequest" /> instance for the specified <paramref name="uri" />. This protected method is called by the XML Web service client infrastructure to get a new <see cref="T:System.Net.WebRequest" /> transport object to transmit the XML Web service request.</summary>
		/// <returns>The <see cref="T:System.Net.WebRequest" /> instance.</returns>
		/// <param name="uri">The <see cref="T:System.Uri" /> to use when creating the <see cref="T:System.Net.WebRequest" />. </param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="uri" /> parameter is null. </exception>
		// Token: 0x06000095 RID: 149 RVA: 0x00003454 File Offset: 0x00001654
		protected virtual WebRequest GetWebRequest(Uri uri)
		{
			if (uri == null)
			{
				throw new InvalidOperationException(Res.GetString("WebMissingPath"));
			}
			WebRequest webRequest = WebRequest.Create(uri);
			this.PendingSyncRequest = webRequest;
			webRequest.Timeout = this.timeout;
			webRequest.ConnectionGroupName = this.connectionGroupName;
			webRequest.Credentials = this.Credentials;
			webRequest.PreAuthenticate = this.PreAuthenticate;
			webRequest.CachePolicy = WebClientProtocol.BypassCache;
			return webRequest;
		}

		/// <summary>Returns a response from a synchronous request to an XML Web service method.</summary>
		/// <returns>A response from a synchronous request to an XML Web service method.</returns>
		/// <param name="request">The <see cref="T:System.Net.WebRequest" /> to get the response from. </param>
		/// <exception cref="T:System.Net.WebException">If <see cref="M:System.Web.Services.Protocols.WebClientProtocol.Abort" /> is invoked prior to calling <see cref="M:System.Web.Services.Protocols.WebClientProtocol.GetWebResponse(System.Net.WebRequest)" />. </exception>
		// Token: 0x06000096 RID: 150 RVA: 0x000034C4 File Offset: 0x000016C4
		protected virtual WebResponse GetWebResponse(WebRequest request)
		{
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "GetWebResponse", Array.Empty<object>()) : null);
			WebResponse webResponse = null;
			try
			{
				if (Tracing.On)
				{
					Tracing.Enter("WebRequest.GetResponse", traceMethod, new TraceMethod(request, "GetResponse", Array.Empty<object>()));
				}
				webResponse = request.GetResponse();
				if (Tracing.On)
				{
					Tracing.Exit("WebRequest.GetResponse", traceMethod);
				}
			}
			catch (WebException ex)
			{
				if (ex.Response == null)
				{
					throw ex;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Error, this, "GetWebResponse", ex);
				}
				webResponse = ex.Response;
			}
			return webResponse;
		}

		/// <summary>Returns a response from an asynchronous request to an XML Web service method. This protected method is called by the XML Web service client infrastructure to get the response from an asynchronous XML Web service request.</summary>
		/// <returns>A response from an asynchronous request to an XML Web service method.</returns>
		/// <param name="request">The <see cref="T:System.Net.WebRequest" /> to get the response from. </param>
		/// <param name="result">The <see cref="T:System.IAsyncResult" /> to pass to <see cref="M:System.Net.HttpWebRequest.EndGetResponse(System.IAsyncResult)" /> when the response has completed. </param>
		/// <exception cref="T:System.Net.WebException">If <see cref="M:System.Web.Services.Protocols.WebClientProtocol.Abort" /> is invoked prior to calling <see cref="M:System.Web.Services.Protocols.WebClientProtocol.GetWebResponse(System.Net.WebRequest)" />. </exception>
		// Token: 0x06000097 RID: 151 RVA: 0x00003568 File Offset: 0x00001768
		protected virtual WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
		{
			return request.EndGetResponse(result);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000210D File Offset: 0x0000030D
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		internal virtual void InitializeAsyncRequest(WebRequest request, object internalAsyncState)
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003571 File Offset: 0x00001771
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		internal virtual void AsyncBufferedSerialize(WebRequest request, Stream requestStream, object internalAsyncState)
		{
			throw new NotSupportedException(Res.GetString("ProtocolDoesNotAsyncSerialize"));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003584 File Offset: 0x00001784
		internal WebResponse EndSend(IAsyncResult asyncResult, ref object internalAsyncState, ref Stream responseStream)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException(Res.GetString("WebNullAsyncResultInEnd"));
			}
			WebClientAsyncResult webClientAsyncResult = (WebClientAsyncResult)asyncResult;
			if (webClientAsyncResult.EndSendCalled)
			{
				throw new InvalidOperationException(Res.GetString("CanTCallTheEndMethodOfAnAsyncCallMoreThan"));
			}
			webClientAsyncResult.EndSendCalled = true;
			WebResponse webResponse = webClientAsyncResult.WaitForResponse();
			internalAsyncState = webClientAsyncResult.InternalAsyncState;
			responseStream = webClientAsyncResult.ResponseBufferedStream;
			return webResponse;
		}

		/// <summary>Gets an instance of a client protocol handler from the cache.</summary>
		/// <returns>An instance of a client protocol handler from the cache.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the client protocol handler to be returned from the cache.</param>
		// Token: 0x0600009B RID: 155 RVA: 0x000035E0 File Offset: 0x000017E0
		protected static object GetFromCache(Type type)
		{
			return WebClientProtocol.cache[type];
		}

		/// <summary>Add an instance of the client protocol handler to the cache.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the client protocol handler..</param>
		/// <param name="value">The client protocol handler to be added to the cache.</param>
		// Token: 0x0600009C RID: 156 RVA: 0x000035ED File Offset: 0x000017ED
		protected static void AddToCache(Type type, object value)
		{
			WebClientProtocol.cache.Add(type, value);
		}

		// Token: 0x040001A7 RID: 423
		private static AsyncCallback getRequestStreamAsyncCallback;

		// Token: 0x040001A8 RID: 424
		private static AsyncCallback getResponseAsyncCallback;

		// Token: 0x040001A9 RID: 425
		private static volatile AsyncCallback readResponseAsyncCallback;

		// Token: 0x040001AA RID: 426
		private static ClientTypeCache cache = new ClientTypeCache();

		// Token: 0x040001AB RID: 427
		private static RequestCachePolicy bypassCache;

		// Token: 0x040001AC RID: 428
		private ICredentials credentials;

		// Token: 0x040001AD RID: 429
		private bool preAuthenticate;

		// Token: 0x040001AE RID: 430
		private Uri uri;

		// Token: 0x040001AF RID: 431
		private int timeout;

		// Token: 0x040001B0 RID: 432
		private string connectionGroupName;

		// Token: 0x040001B1 RID: 433
		private Encoding requestEncoding;

		// Token: 0x040001B2 RID: 434
		private WebRequest pendingSyncRequest;

		// Token: 0x040001B3 RID: 435
		private object nullToken = new object();

		// Token: 0x040001B4 RID: 436
		private Hashtable asyncInvokes = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040001B5 RID: 437
		private static object s_InternalSyncObject;
	}
}
