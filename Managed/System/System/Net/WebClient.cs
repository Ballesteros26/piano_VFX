using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net.Cache;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	/// <summary>Provides common methods for sending data to and receiving data from a resource identified by a URI.</summary>
	// Token: 0x020004CC RID: 1228
	[ComVisible(true)]
	public class WebClient : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebClient" /> class.</summary>
		// Token: 0x06002477 RID: 9335 RVA: 0x0008E035 File Offset: 0x0008C235
		public WebClient()
		{
			if (base.GetType() == typeof(WebClient))
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x0008E070 File Offset: 0x0008C270
		private void InitWebClientAsync()
		{
			if (!this.m_InitWebClientAsync)
			{
				this.openReadOperationCompleted = new SendOrPostCallback(this.OpenReadOperationCompleted);
				this.openWriteOperationCompleted = new SendOrPostCallback(this.OpenWriteOperationCompleted);
				this.downloadStringOperationCompleted = new SendOrPostCallback(this.DownloadStringOperationCompleted);
				this.downloadDataOperationCompleted = new SendOrPostCallback(this.DownloadDataOperationCompleted);
				this.downloadFileOperationCompleted = new SendOrPostCallback(this.DownloadFileOperationCompleted);
				this.uploadStringOperationCompleted = new SendOrPostCallback(this.UploadStringOperationCompleted);
				this.uploadDataOperationCompleted = new SendOrPostCallback(this.UploadDataOperationCompleted);
				this.uploadFileOperationCompleted = new SendOrPostCallback(this.UploadFileOperationCompleted);
				this.uploadValuesOperationCompleted = new SendOrPostCallback(this.UploadValuesOperationCompleted);
				this.reportDownloadProgressChanged = new SendOrPostCallback(this.ReportDownloadProgressChanged);
				this.reportUploadProgressChanged = new SendOrPostCallback(this.ReportUploadProgressChanged);
				this.m_Progress = new WebClient.ProgressData();
				this.m_InitWebClientAsync = true;
			}
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x0008E160 File Offset: 0x0008C360
		private void ClearWebClientState()
		{
			if (this.AnotherCallInProgress(Interlocked.Increment(ref this.m_CallNesting)))
			{
				this.CompleteWebClientState();
				throw new NotSupportedException(global::SR.GetString("WebClient does not support concurrent I/O operations."));
			}
			this.m_ContentLength = -1L;
			this.m_WebResponse = null;
			this.m_WebRequest = null;
			this.m_Method = null;
			this.m_Cancelled = false;
			if (this.m_Progress != null)
			{
				this.m_Progress.Reset();
			}
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x0008E1CD File Offset: 0x0008C3CD
		private void CompleteWebClientState()
		{
			Interlocked.Decrement(ref this.m_CallNesting);
		}

		/// <summary>Gets or sets a value that indicates whether to buffer the data read from the Internet resource for a <see cref="T:System.Net.WebClient" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true to enable buffering of the data received from the Internet resource; false to disable buffering. The default is true.</returns>
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x0600247B RID: 9339 RVA: 0x0008E1DB File Offset: 0x0008C3DB
		// (set) Token: 0x0600247C RID: 9340 RVA: 0x0008E1E3 File Offset: 0x0008C3E3
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool AllowReadStreamBuffering { get; set; }

		/// <summary>Gets or sets a value that indicates whether to buffer the data written to the Internet resource for a <see cref="T:System.Net.WebClient" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true to enable buffering of the data written to the Internet resource; false to disable buffering. The default is true.</returns>
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x0008E1EC File Offset: 0x0008C3EC
		// (set) Token: 0x0600247E RID: 9342 RVA: 0x0008E1F4 File Offset: 0x0008C3F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		public bool AllowWriteStreamBuffering { get; set; }

		/// <summary>Occurs when an asynchronous operation to write data to a resource using a write stream is closed.</summary>
		// Token: 0x1400004A RID: 74
		// (add) Token: 0x0600247F RID: 9343 RVA: 0x000027E8 File Offset: 0x000009E8
		// (remove) Token: 0x06002480 RID: 9344 RVA: 0x000027E8 File Offset: 0x000009E8
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event WriteStreamClosedEventHandler WriteStreamClosed
		{
			add
			{
			}
			remove
			{
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.WriteStreamClosed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.WriteStreamClosedEventArgs" />  object containing event data.</param>
		// Token: 0x06002481 RID: 9345 RVA: 0x000027E8 File Offset: 0x000009E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		protected virtual void OnWriteStreamClosed(WriteStreamClosedEventArgs e)
		{
		}

		/// <summary>Gets and sets the <see cref="T:System.Text.Encoding" /> used to upload and download strings.</summary>
		/// <returns>A <see cref="T:System.Text.Encoding" /> that is used to encode strings. The default value of this property is the encoding returned by <see cref="P:System.Text.Encoding.Default" />.</returns>
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x0008E1FD File Offset: 0x0008C3FD
		// (set) Token: 0x06002483 RID: 9347 RVA: 0x0008E205 File Offset: 0x0008C405
		public Encoding Encoding
		{
			get
			{
				return this.m_Encoding;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Encoding");
				}
				this.m_Encoding = value;
			}
		}

		/// <summary>Gets or sets the base URI for requests made by a <see cref="T:System.Net.WebClient" />.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the base URI for requests made by a <see cref="T:System.Net.WebClient" /> or <see cref="F:System.String.Empty" /> if no base address has been specified.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.WebClient.BaseAddress" /> is set to an invalid URI. The inner exception may contain information that will help you locate the error.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x0008E21C File Offset: 0x0008C41C
		// (set) Token: 0x06002485 RID: 9349 RVA: 0x0008E240 File Offset: 0x0008C440
		public string BaseAddress
		{
			get
			{
				if (!(this.m_baseAddress == null))
				{
					return this.m_baseAddress.ToString();
				}
				return string.Empty;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.m_baseAddress = null;
					return;
				}
				try
				{
					this.m_baseAddress = new Uri(value);
				}
				catch (UriFormatException ex)
				{
					throw new ArgumentException(global::SR.GetString("The specified value is not a valid base address."), "value", ex);
				}
			}
		}

		/// <summary>Gets or sets the network credentials that are sent to the host and used to authenticate the request.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> containing the authentication credentials for the request. The default is null.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x0008E298 File Offset: 0x0008C498
		// (set) Token: 0x06002487 RID: 9351 RVA: 0x0008E2A0 File Offset: 0x0008C4A0
		public ICredentials Credentials
		{
			get
			{
				return this.m_credentials;
			}
			set
			{
				this.m_credentials = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> are sent with requests.</summary>
		/// <returns>true if the default credentials are used; otherwise false. The default value is false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="USERNAME" />
		/// </PermissionSet>
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x0008E2A9 File Offset: 0x0008C4A9
		// (set) Token: 0x06002489 RID: 9353 RVA: 0x0008E2BB File Offset: 0x0008C4BB
		public bool UseDefaultCredentials
		{
			get
			{
				return this.m_credentials is SystemNetworkCredential;
			}
			set
			{
				this.m_credentials = (value ? CredentialCache.DefaultCredentials : null);
			}
		}

		/// <summary>Gets or sets a collection of header name/value pairs associated with the request.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> containing header name/value pairs associated with this request.</returns>
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x0008E2CE File Offset: 0x0008C4CE
		// (set) Token: 0x0600248B RID: 9355 RVA: 0x0008E2EA File Offset: 0x0008C4EA
		public WebHeaderCollection Headers
		{
			get
			{
				if (this.m_headers == null)
				{
					this.m_headers = new WebHeaderCollection(WebHeaderCollectionType.WebRequest);
				}
				return this.m_headers;
			}
			set
			{
				this.m_headers = value;
			}
		}

		/// <summary>Gets or sets a collection of query name/value pairs associated with the request.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains query name/value pairs associated with the request. If no pairs are associated with the request, the value is an empty <see cref="T:System.Collections.Specialized.NameValueCollection" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x0600248C RID: 9356 RVA: 0x0008E2F3 File Offset: 0x0008C4F3
		// (set) Token: 0x0600248D RID: 9357 RVA: 0x0008E30E File Offset: 0x0008C50E
		public NameValueCollection QueryString
		{
			get
			{
				if (this.m_requestParameters == null)
				{
					this.m_requestParameters = new NameValueCollection();
				}
				return this.m_requestParameters;
			}
			set
			{
				this.m_requestParameters = value;
			}
		}

		/// <summary>Gets a collection of header name/value pairs associated with the response.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> containing header name/value pairs associated with the response, or null if no response has been received.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x0008E317 File Offset: 0x0008C517
		public WebHeaderCollection ResponseHeaders
		{
			get
			{
				if (this.m_WebResponse != null)
				{
					return this.m_WebResponse.Headers;
				}
				return null;
			}
		}

		/// <summary>Gets or sets the proxy used by this <see cref="T:System.Net.WebClient" /> object.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> instance used to send requests.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Net.WebClient.Proxy" /> is set to null. </exception>
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x0600248F RID: 9359 RVA: 0x0008E32E File Offset: 0x0008C52E
		// (set) Token: 0x06002490 RID: 9360 RVA: 0x0008E344 File Offset: 0x0008C544
		public IWebProxy Proxy
		{
			get
			{
				if (!this.m_ProxySet)
				{
					return WebRequest.InternalDefaultWebProxy;
				}
				return this.m_Proxy;
			}
			set
			{
				this.m_Proxy = value;
				this.m_ProxySet = true;
			}
		}

		/// <summary>Gets or sets the application's cache policy for any resources obtained by this WebClient instance using <see cref="T:System.Net.WebRequest" /> objects.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCachePolicy" /> object that represents the application's caching requirements.</returns>
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002491 RID: 9361 RVA: 0x0008E354 File Offset: 0x0008C554
		// (set) Token: 0x06002492 RID: 9362 RVA: 0x0008E35C File Offset: 0x0008C55C
		public RequestCachePolicy CachePolicy
		{
			get
			{
				return this.m_CachePolicy;
			}
			set
			{
				this.m_CachePolicy = value;
			}
		}

		/// <summary>Gets whether a Web request is in progress.</summary>
		/// <returns>true if the Web request is still in progress; otherwise false.</returns>
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002493 RID: 9363 RVA: 0x0008E365 File Offset: 0x0008C565
		public bool IsBusy
		{
			get
			{
				return this.m_AsyncOp != null;
			}
		}

		/// <summary>Returns a <see cref="T:System.Net.WebRequest" /> object for the specified resource.</summary>
		/// <returns>A new <see cref="T:System.Net.WebRequest" /> object for the specified resource.</returns>
		/// <param name="address">A <see cref="T:System.Uri" /> that identifies the resource to request.</param>
		// Token: 0x06002494 RID: 9364 RVA: 0x0008E370 File Offset: 0x0008C570
		protected virtual WebRequest GetWebRequest(Uri address)
		{
			WebRequest webRequest = WebRequest.Create(address);
			this.CopyHeadersTo(webRequest);
			if (this.Credentials != null)
			{
				webRequest.Credentials = this.Credentials;
			}
			if (this.m_Method != null)
			{
				webRequest.Method = this.m_Method;
			}
			if (this.m_ContentLength != -1L)
			{
				webRequest.ContentLength = this.m_ContentLength;
			}
			if (this.m_ProxySet)
			{
				webRequest.Proxy = this.m_Proxy;
			}
			if (this.m_CachePolicy != null)
			{
				webRequest.CachePolicy = this.m_CachePolicy;
			}
			return webRequest;
		}

		/// <summary>Returns the <see cref="T:System.Net.WebResponse" /> for the specified <see cref="T:System.Net.WebRequest" />.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> containing the response for the specified <see cref="T:System.Net.WebRequest" />.</returns>
		/// <param name="request">A <see cref="T:System.Net.WebRequest" /> that is used to obtain the response. </param>
		// Token: 0x06002495 RID: 9365 RVA: 0x0008E3F4 File Offset: 0x0008C5F4
		protected virtual WebResponse GetWebResponse(WebRequest request)
		{
			WebResponse response = request.GetResponse();
			this.m_WebResponse = response;
			return response;
		}

		/// <summary>Returns the <see cref="T:System.Net.WebResponse" /> for the specified <see cref="T:System.Net.WebRequest" /> using the specified <see cref="T:System.IAsyncResult" />.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> containing the response for the specified <see cref="T:System.Net.WebRequest" />.</returns>
		/// <param name="request">A <see cref="T:System.Net.WebRequest" /> that is used to obtain the response.</param>
		/// <param name="result">An <see cref="T:System.IAsyncResult" /> object obtained from a previous call to <see cref="M:System.Net.WebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> .</param>
		// Token: 0x06002496 RID: 9366 RVA: 0x0008E410 File Offset: 0x0008C610
		protected virtual WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
		{
			WebResponse webResponse = request.EndGetResponse(result);
			this.m_WebResponse = webResponse;
			return webResponse;
		}

		/// <summary>Downloads the resource as a <see cref="T:System.Byte" /> array from the URI specified.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the downloaded resource.</returns>
		/// <param name="address">The URI from which to download data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading data. </exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002497 RID: 9367 RVA: 0x0008E42D File Offset: 0x0008C62D
		public byte[] DownloadData(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.DownloadData(this.GetUri(address));
		}

		/// <summary>Downloads the resource as a <see cref="T:System.Byte" /> array from the URI specified.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the downloaded resource.</returns>
		/// <param name="address">The URI represented by the <see cref="T:System.Uri" />  object, from which to download data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		// Token: 0x06002498 RID: 9368 RVA: 0x0008E44C File Offset: 0x0008C64C
		public byte[] DownloadData(Uri address)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.ClearWebClientState();
			byte[] array2;
			try
			{
				WebRequest webRequest;
				byte[] array = this.DownloadDataInternal(address, out webRequest);
				bool on2 = Logging.On;
				array2 = array;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return array2;
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x0008E4A4 File Offset: 0x0008C6A4
		private byte[] DownloadDataInternal(Uri address, out WebRequest request)
		{
			bool on = Logging.On;
			request = null;
			byte[] array;
			try
			{
				request = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				array = this.DownloadBits(request, null, null, null);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex);
				}
				WebClient.AbortRequest(request);
				throw ex;
			}
			return array;
		}

		/// <summary>Downloads the resource with the specified URI to a local file.</summary>
		/// <param name="address">The URI from which to download data. </param>
		/// <param name="fileName">The name of the local file that is to receive the data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="filename" /> is null or <see cref="F:System.String.Empty" />.-or-The file does not exist.-or- An error occurred while downloading data. </exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600249A RID: 9370 RVA: 0x0008E538 File Offset: 0x0008C738
		public void DownloadFile(string address, string fileName)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.DownloadFile(this.GetUri(address), fileName);
		}

		/// <summary>Downloads the resource with the specified URI to a local file.</summary>
		/// <param name="address">The URI specified as a <see cref="T:System.String" />, from which to download data. </param>
		/// <param name="fileName">The name of the local file that is to receive the data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. -or-The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="filename" /> is null or <see cref="F:System.String.Empty" />.-or- The file does not exist. -or- An error occurred while downloading data. </exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		// Token: 0x0600249B RID: 9371 RVA: 0x0008E558 File Offset: 0x0008C758
		public void DownloadFile(Uri address, string fileName)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			WebRequest webRequest = null;
			FileStream fileStream = null;
			bool flag = false;
			this.ClearWebClientState();
			try
			{
				fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
				webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				this.DownloadBits(webRequest, fileStream, null, null);
				flag = true;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex);
				}
				WebClient.AbortRequest(webRequest);
				throw ex;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
					if (!flag)
					{
						File.Delete(fileName);
					}
					fileStream = null;
				}
				this.CompleteWebClientState();
			}
			bool on2 = Logging.On;
		}

		/// <summary>Opens a readable stream for the data downloaded from a resource with the URI specified as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to read data from a resource.</returns>
		/// <param name="address">The URI specified as a <see cref="T:System.String" /> from which to download data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, <paramref name="address" /> is invalid.-or- An error occurred while downloading data. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600249C RID: 9372 RVA: 0x0008E654 File Offset: 0x0008C854
		public Stream OpenRead(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.OpenRead(this.GetUri(address));
		}

		/// <summary>Opens a readable stream for the data downloaded from a resource with the URI specified as a <see cref="T:System.Uri" /></summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to read data from a resource.</returns>
		/// <param name="address">The URI specified as a <see cref="T:System.Uri" /> from which to download data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, <paramref name="address" /> is invalid.-or- An error occurred while downloading data. </exception>
		// Token: 0x0600249D RID: 9373 RVA: 0x0008E674 File Offset: 0x0008C874
		public Stream OpenRead(Uri address)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			WebRequest webRequest = null;
			this.ClearWebClientState();
			Stream stream;
			try
			{
				webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				Stream responseStream = (this.m_WebResponse = this.GetWebResponse(webRequest)).GetResponseStream();
				bool on2 = Logging.On;
				stream = responseStream;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex);
				}
				WebClient.AbortRequest(webRequest);
				throw ex;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return stream;
		}

		/// <summary>Opens a stream for writing data to the specified resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600249E RID: 9374 RVA: 0x0008E74C File Offset: 0x0008C94C
		public Stream OpenWrite(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.OpenWrite(this.GetUri(address), null);
		}

		/// <summary>Opens a stream for writing data to the specified resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		// Token: 0x0600249F RID: 9375 RVA: 0x0008E76A File Offset: 0x0008C96A
		public Stream OpenWrite(Uri address)
		{
			return this.OpenWrite(address, null);
		}

		/// <summary>Opens a stream for writing data to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024A0 RID: 9376 RVA: 0x0008E774 File Offset: 0x0008C974
		public Stream OpenWrite(string address, string method)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.OpenWrite(this.GetUri(address), method);
		}

		/// <summary>Opens a stream for writing data to the specified resource, by using the specified method.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		// Token: 0x060024A1 RID: 9377 RVA: 0x0008E794 File Offset: 0x0008C994
		public Stream OpenWrite(Uri address, string method)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			WebRequest webRequest = null;
			this.ClearWebClientState();
			Stream stream2;
			try
			{
				this.m_Method = method;
				webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				Stream stream = new WebClient.WebClientWriteStream(webRequest.GetRequestStream(), webRequest, this);
				bool on2 = Logging.On;
				stream2 = stream;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex);
				}
				WebClient.AbortRequest(webRequest);
				throw ex;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return stream2;
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="data">The data buffer to send to the resource. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null. -or-An error occurred while sending the data.-or- There was no response from the server hosting the resource. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024A2 RID: 9378 RVA: 0x0008E86C File Offset: 0x0008CA6C
		public byte[] UploadData(string address, byte[] data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadData(this.GetUri(address), null, data);
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="data">The data buffer to send to the resource. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null. -or-An error occurred while sending the data.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x060024A3 RID: 9379 RVA: 0x0008E88B File Offset: 0x0008CA8B
		public byte[] UploadData(Uri address, byte[] data)
		{
			return this.UploadData(address, null, data);
		}

		/// <summary>Uploads a data buffer to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="method">The HTTP method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- An error occurred while uploading the data.-or- There was no response from the server hosting the resource. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024A4 RID: 9380 RVA: 0x0008E896 File Offset: 0x0008CA96
		public byte[] UploadData(string address, string method, byte[] data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadData(this.GetUri(address), method, data);
		}

		/// <summary>Uploads a data buffer to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="method">The HTTP method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- An error occurred while uploading the data.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x060024A5 RID: 9381 RVA: 0x0008E8B8 File Offset: 0x0008CAB8
		public byte[] UploadData(Uri address, string method, byte[] data)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.ClearWebClientState();
			byte[] array2;
			try
			{
				WebRequest webRequest;
				byte[] array = this.UploadDataInternal(address, method, data, out webRequest);
				bool on2 = Logging.On;
				array2 = array;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return array2;
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x0008E92C File Offset: 0x0008CB2C
		private byte[] UploadDataInternal(Uri address, string method, byte[] data, out WebRequest request)
		{
			request = null;
			byte[] array;
			try
			{
				this.m_Method = method;
				this.m_ContentLength = (long)data.Length;
				request = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				this.UploadBits(request, null, data, 0, null, null, null, null, null);
				array = this.DownloadBits(request, null, null, null);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex);
				}
				WebClient.AbortRequest(request);
				throw ex;
			}
			return array;
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x0008E9E0 File Offset: 0x0008CBE0
		private void OpenFileInternal(bool needsHeaderAndBoundary, string fileName, ref FileStream fs, ref byte[] buffer, ref byte[] formHeaderBytes, ref byte[] boundaryBytes)
		{
			fileName = Path.GetFullPath(fileName);
			if (this.m_headers == null)
			{
				this.m_headers = new WebHeaderCollection(WebHeaderCollectionType.WebRequest);
			}
			string text = this.m_headers["Content-Type"];
			if (text != null)
			{
				if (text.ToLower(CultureInfo.InvariantCulture).StartsWith("multipart/"))
				{
					throw new WebException(global::SR.GetString("The Content-Type header cannot be set to a multipart type for this request."));
				}
			}
			else
			{
				text = "application/octet-stream";
			}
			fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			int num = 8192;
			this.m_ContentLength = -1L;
			if (this.m_Method.ToUpper(CultureInfo.InvariantCulture) == "POST")
			{
				if (needsHeaderAndBoundary)
				{
					string text2 = "---------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
					this.m_headers["Content-Type"] = "multipart/form-data; boundary=" + text2;
					string text3 = string.Concat(new string[]
					{
						"--",
						text2,
						"\r\nContent-Disposition: form-data; name=\"file\"; filename=\"",
						Path.GetFileName(fileName),
						"\"\r\nContent-Type: ",
						text,
						"\r\n\r\n"
					});
					formHeaderBytes = Encoding.UTF8.GetBytes(text3);
					boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + text2 + "--\r\n");
				}
				else
				{
					formHeaderBytes = new byte[0];
					boundaryBytes = new byte[0];
				}
				if (fs.CanSeek)
				{
					this.m_ContentLength = fs.Length + (long)formHeaderBytes.Length + (long)boundaryBytes.Length;
					num = (int)Math.Min(8192L, fs.Length);
				}
			}
			else
			{
				this.m_headers["Content-Type"] = text;
				formHeaderBytes = null;
				boundaryBytes = null;
				if (fs.CanSeek)
				{
					this.m_ContentLength = fs.Length;
					num = (int)Math.Min(8192L, fs.Length);
				}
			}
			buffer = new byte[num];
		}

		/// <summary>Uploads the specified local file to a resource with the specified URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the file. For example, ftp://localhost/samplefile.txt.</param>
		/// <param name="fileName">The file to send to the resource. For example, "samplefile.txt".</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. -or-The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid characters, or does not exist.-or- An error occurred while uploading the file.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024A8 RID: 9384 RVA: 0x0008EBD0 File Offset: 0x0008CDD0
		public byte[] UploadFile(string address, string fileName)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadFile(this.GetUri(address), fileName);
		}

		/// <summary>Uploads the specified local file to a resource with the specified URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the file. For example, ftp://localhost/samplefile.txt.</param>
		/// <param name="fileName">The file to send to the resource. For example, "samplefile.txt".</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. -or-The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid characters, or does not exist.-or- An error occurred while uploading the file.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x060024A9 RID: 9385 RVA: 0x0008EBEE File Offset: 0x0008CDEE
		public byte[] UploadFile(Uri address, string fileName)
		{
			return this.UploadFile(address, null, fileName);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the file.</param>
		/// <param name="method">The method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The file to send to the resource. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. -or-The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid characters, or does not exist.-or- An error occurred while uploading the file.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024AA RID: 9386 RVA: 0x0008EBF9 File Offset: 0x0008CDF9
		public byte[] UploadFile(string address, string method, string fileName)
		{
			return this.UploadFile(this.GetUri(address), method, fileName);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the file.</param>
		/// <param name="method">The method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The file to send to the resource. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. -or-The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid characters, or does not exist.-or- An error occurred while uploading the file.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x060024AB RID: 9387 RVA: 0x0008EC0C File Offset: 0x0008CE0C
		public byte[] UploadFile(Uri address, string method, string fileName)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			FileStream fileStream = null;
			WebRequest webRequest = null;
			this.ClearWebClientState();
			byte[] array5;
			try
			{
				this.m_Method = method;
				byte[] array = null;
				byte[] array2 = null;
				byte[] array3 = null;
				Uri uri = this.GetUri(address);
				bool flag = uri.Scheme != Uri.UriSchemeFile;
				this.OpenFileInternal(flag, fileName, ref fileStream, ref array3, ref array, ref array2);
				webRequest = (this.m_WebRequest = this.GetWebRequest(uri));
				this.UploadBits(webRequest, fileStream, array3, 0, array, array2, null, null, null);
				byte[] array4 = this.DownloadBits(webRequest, null, null, null);
				bool on2 = Logging.On;
				array5 = array4;
			}
			catch (Exception ex)
			{
				if (fileStream != null)
				{
					fileStream.Close();
					fileStream = null;
				}
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex);
				}
				WebClient.AbortRequest(webRequest);
				throw ex;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return array5;
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x0008ED48 File Offset: 0x0008CF48
		private byte[] UploadValuesInternal(NameValueCollection data)
		{
			if (this.m_headers == null)
			{
				this.m_headers = new WebHeaderCollection(WebHeaderCollectionType.WebRequest);
			}
			string text = this.m_headers["Content-Type"];
			if (text != null && string.Compare(text, "application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new WebException(global::SR.GetString("The Content-Type header cannot be changed from its default value for this request."));
			}
			this.m_headers["Content-Type"] = "application/x-www-form-urlencoded";
			string text2 = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text3 in data.AllKeys)
			{
				stringBuilder.Append(text2);
				stringBuilder.Append(WebClient.UrlEncode(text3));
				stringBuilder.Append("=");
				stringBuilder.Append(WebClient.UrlEncode(data[text3]));
				text2 = "&";
			}
			byte[] bytes = Encoding.ASCII.GetBytes(stringBuilder.ToString());
			this.m_ContentLength = (long)bytes.Length;
			return bytes;
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the collection. </param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- There was no response from the server hosting the resource.-or- An error occurred while opening the stream.-or- The Content-type header is not null or "application/x-www-form-urlencoded". </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024AD RID: 9389 RVA: 0x0008EE36 File Offset: 0x0008D036
		public byte[] UploadValues(string address, NameValueCollection data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadValues(this.GetUri(address), null, data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the collection. </param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- There was no response from the server hosting the resource.-or- An error occurred while opening the stream.-or- The Content-type header is not null or "application/x-www-form-urlencoded". </exception>
		// Token: 0x060024AE RID: 9390 RVA: 0x0008EE55 File Offset: 0x0008D055
		public byte[] UploadValues(Uri address, NameValueCollection data)
		{
			return this.UploadValues(address, null, data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the collection. </param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header value is not null and is not application/x-www-form-urlencoded. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024AF RID: 9391 RVA: 0x0008EE60 File Offset: 0x0008D060
		public byte[] UploadValues(string address, string method, NameValueCollection data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadValues(this.GetUri(address), method, data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the collection. </param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header value is not null and is not application/x-www-form-urlencoded. </exception>
		// Token: 0x060024B0 RID: 9392 RVA: 0x0008EE80 File Offset: 0x0008D080
		public byte[] UploadValues(Uri address, string method, NameValueCollection data)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			WebRequest webRequest = null;
			this.ClearWebClientState();
			byte[] array3;
			try
			{
				byte[] array = this.UploadValuesInternal(data);
				this.m_Method = method;
				webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				this.UploadBits(webRequest, null, array, 0, null, null, null, null, null);
				byte[] array2 = this.DownloadBits(webRequest, null, null, null);
				bool on2 = Logging.On;
				array3 = array2;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex);
				}
				WebClient.AbortRequest(webRequest);
				throw ex;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return array3;
		}

		/// <summary>Uploads the specified string to the specified resource, using the POST method.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the string. For Http resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page. </param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024B1 RID: 9393 RVA: 0x0008EF84 File Offset: 0x0008D184
		public string UploadString(string address, string data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadString(this.GetUri(address), null, data);
		}

		/// <summary>Uploads the specified string to the specified resource, using the POST method.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the string. For Http resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page. </param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x060024B2 RID: 9394 RVA: 0x0008EFA3 File Offset: 0x0008D1A3
		public string UploadString(Uri address, string data)
		{
			return this.UploadString(address, null, data);
		}

		/// <summary>Uploads the specified string to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the string. This URI must identify a resource that can accept a request sent with the <paramref name="method" /> method. </param>
		/// <param name="method">The HTTP method used to send the string to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.-or-<paramref name="method" /> cannot be used to send content.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024B3 RID: 9395 RVA: 0x0008EFAE File Offset: 0x0008D1AE
		public string UploadString(string address, string method, string data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadString(this.GetUri(address), method, data);
		}

		/// <summary>Uploads the specified string to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the string. This URI must identify a resource that can accept a request sent with the <paramref name="method" /> method. </param>
		/// <param name="method">The HTTP method used to send the string to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.-or-<paramref name="method" /> cannot be used to send content.</exception>
		// Token: 0x060024B4 RID: 9396 RVA: 0x0008EFD0 File Offset: 0x0008D1D0
		public string UploadString(Uri address, string method, string data)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.ClearWebClientState();
			string text;
			try
			{
				byte[] bytes = this.Encoding.GetBytes(data);
				WebRequest webRequest;
				byte[] array = this.UploadDataInternal(address, method, bytes, out webRequest);
				string stringUsingEncoding = this.GetStringUsingEncoding(webRequest, array);
				bool on2 = Logging.On;
				text = stringUsingEncoding;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return text;
		}

		/// <summary>Downloads the requested resource as a <see cref="T:System.String" />. The resource to download is specified as a <see cref="T:System.String" /> containing the URI.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the requested resource.</returns>
		/// <param name="address">A <see cref="T:System.String" /> containing the URI to download.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024B5 RID: 9397 RVA: 0x0008F05C File Offset: 0x0008D25C
		public string DownloadString(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.DownloadString(this.GetUri(address));
		}

		/// <summary>Downloads the requested resource as a <see cref="T:System.String" />. The resource to download is specified as a <see cref="T:System.Uri" />.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the requested resource.</returns>
		/// <param name="address">A <see cref="T:System.Uri" /> object containing the URI to download.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		// Token: 0x060024B6 RID: 9398 RVA: 0x0008F07C File Offset: 0x0008D27C
		public string DownloadString(Uri address)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.ClearWebClientState();
			string text;
			try
			{
				WebRequest webRequest;
				byte[] array = this.DownloadDataInternal(address, out webRequest);
				string stringUsingEncoding = this.GetStringUsingEncoding(webRequest, array);
				bool on2 = Logging.On;
				text = stringUsingEncoding;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return text;
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x0008F0DC File Offset: 0x0008D2DC
		private static void AbortRequest(WebRequest request)
		{
			try
			{
				if (request != null)
				{
					request.Abort();
				}
			}
			catch (Exception ex)
			{
				if (ex is OutOfMemoryException || ex is StackOverflowException || ex is ThreadAbortException)
				{
					throw;
				}
			}
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x0008F124 File Offset: 0x0008D324
		private void CopyHeadersTo(WebRequest request)
		{
			if (this.m_headers != null && request is HttpWebRequest)
			{
				string text = this.m_headers["Accept"];
				string text2 = this.m_headers["Connection"];
				string text3 = this.m_headers["Content-Type"];
				string text4 = this.m_headers["Expect"];
				string text5 = this.m_headers["Referer"];
				string text6 = this.m_headers["User-Agent"];
				string text7 = this.m_headers["Host"];
				this.m_headers.RemoveInternal("Accept");
				this.m_headers.RemoveInternal("Connection");
				this.m_headers.RemoveInternal("Content-Type");
				this.m_headers.RemoveInternal("Expect");
				this.m_headers.RemoveInternal("Referer");
				this.m_headers.RemoveInternal("User-Agent");
				this.m_headers.RemoveInternal("Host");
				request.Headers = this.m_headers;
				if (text != null && text.Length > 0)
				{
					((HttpWebRequest)request).Accept = text;
				}
				if (text2 != null && text2.Length > 0)
				{
					((HttpWebRequest)request).Connection = text2;
				}
				if (text3 != null && text3.Length > 0)
				{
					((HttpWebRequest)request).ContentType = text3;
				}
				if (text4 != null && text4.Length > 0)
				{
					((HttpWebRequest)request).Expect = text4;
				}
				if (text5 != null && text5.Length > 0)
				{
					((HttpWebRequest)request).Referer = text5;
				}
				if (text6 != null && text6.Length > 0)
				{
					((HttpWebRequest)request).UserAgent = text6;
				}
				if (!string.IsNullOrEmpty(text7))
				{
					((HttpWebRequest)request).Host = text7;
				}
			}
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x0008F2EC File Offset: 0x0008D4EC
		private Uri GetUri(string path)
		{
			Uri uri;
			if (this.m_baseAddress != null)
			{
				if (!Uri.TryCreate(this.m_baseAddress, path, out uri))
				{
					return new Uri(Path.GetFullPath(path));
				}
			}
			else if (!Uri.TryCreate(path, UriKind.Absolute, out uri))
			{
				return new Uri(Path.GetFullPath(path));
			}
			return this.GetUri(uri);
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x0008F344 File Offset: 0x0008D544
		private Uri GetUri(Uri address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			Uri uri = address;
			if (!address.IsAbsoluteUri && this.m_baseAddress != null && !Uri.TryCreate(this.m_baseAddress, address, out uri))
			{
				return address;
			}
			if ((uri.Query == null || uri.Query == string.Empty) && this.m_requestParameters != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string text = string.Empty;
				for (int i = 0; i < this.m_requestParameters.Count; i++)
				{
					stringBuilder.Append(text + this.m_requestParameters.AllKeys[i] + "=" + this.m_requestParameters[i]);
					text = "&";
				}
				uri = new UriBuilder(uri)
				{
					Query = stringBuilder.ToString()
				}.Uri;
			}
			return uri;
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x0008F420 File Offset: 0x0008D620
		private static void DownloadBitsResponseCallback(IAsyncResult result)
		{
			WebClient.DownloadBitsState downloadBitsState = (WebClient.DownloadBitsState)result.AsyncState;
			WebRequest request = downloadBitsState.Request;
			Exception ex = null;
			try
			{
				WebResponse webResponse = downloadBitsState.WebClient.GetWebResponse(request, result);
				downloadBitsState.WebClient.m_WebResponse = webResponse;
				downloadBitsState.SetResponse(webResponse);
			}
			catch (Exception ex2)
			{
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex2;
				if (!(ex2 is WebException) && !(ex2 is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex2);
				}
				WebClient.AbortRequest(request);
				if (downloadBitsState != null && downloadBitsState.WriteStream != null)
				{
					downloadBitsState.WriteStream.Close();
				}
			}
			finally
			{
				if (ex != null)
				{
					downloadBitsState.CompletionDelegate(null, ex, downloadBitsState.AsyncOp);
				}
			}
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x0008F500 File Offset: 0x0008D700
		private static void DownloadBitsReadCallback(IAsyncResult result)
		{
			WebClient.DownloadBitsReadCallbackState((WebClient.DownloadBitsState)result.AsyncState, result);
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x0008F514 File Offset: 0x0008D714
		private static void DownloadBitsReadCallbackState(WebClient.DownloadBitsState state, IAsyncResult result)
		{
			Stream readStream = state.ReadStream;
			Exception ex = null;
			bool flag = false;
			try
			{
				int num = 0;
				if (readStream != null && readStream != Stream.Null)
				{
					num = readStream.EndRead(result);
				}
				flag = state.RetrieveBytes(ref num);
			}
			catch (Exception ex2)
			{
				flag = true;
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex2;
				state.InnerBuffer = null;
				if (!(ex2 is WebException) && !(ex2 is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex2);
				}
				WebClient.AbortRequest(state.Request);
				if (state != null && state.WriteStream != null)
				{
					state.WriteStream.Close();
				}
			}
			finally
			{
				if (flag)
				{
					if (ex == null)
					{
						state.Close();
					}
					state.CompletionDelegate(state.InnerBuffer, ex, state.AsyncOp);
				}
			}
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x0008F604 File Offset: 0x0008D804
		private byte[] DownloadBits(WebRequest request, Stream writeStream, CompletionDelegate completionDelegate, AsyncOperation asyncOp)
		{
			WebClient.DownloadBitsState downloadBitsState = new WebClient.DownloadBitsState(request, writeStream, completionDelegate, asyncOp, this.m_Progress, this);
			if (downloadBitsState.Async)
			{
				request.BeginGetResponse(new AsyncCallback(WebClient.DownloadBitsResponseCallback), downloadBitsState);
				return null;
			}
			WebResponse webResponse = (this.m_WebResponse = this.GetWebResponse(request));
			int num = downloadBitsState.SetResponse(webResponse);
			bool flag;
			do
			{
				flag = downloadBitsState.RetrieveBytes(ref num);
			}
			while (!flag);
			downloadBitsState.Close();
			return downloadBitsState.InnerBuffer;
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x0008F678 File Offset: 0x0008D878
		private static void UploadBitsRequestCallback(IAsyncResult result)
		{
			WebClient.UploadBitsState uploadBitsState = (WebClient.UploadBitsState)result.AsyncState;
			WebRequest request = uploadBitsState.Request;
			Exception ex = null;
			try
			{
				Stream stream = request.EndGetRequestStream(result);
				uploadBitsState.SetRequestStream(stream);
			}
			catch (Exception ex2)
			{
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex2;
				if (!(ex2 is WebException) && !(ex2 is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex2);
				}
				WebClient.AbortRequest(request);
				if (uploadBitsState != null && uploadBitsState.ReadStream != null)
				{
					uploadBitsState.ReadStream.Close();
				}
			}
			finally
			{
				if (ex != null)
				{
					uploadBitsState.UploadCompletionDelegate(null, ex, uploadBitsState);
				}
			}
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x0008F740 File Offset: 0x0008D940
		private static void UploadBitsWriteCallback(IAsyncResult result)
		{
			WebClient.UploadBitsState uploadBitsState = (WebClient.UploadBitsState)result.AsyncState;
			Stream writeStream = uploadBitsState.WriteStream;
			Exception ex = null;
			bool flag = false;
			try
			{
				writeStream.EndWrite(result);
				flag = uploadBitsState.WriteBytes();
			}
			catch (Exception ex2)
			{
				flag = true;
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex2;
				if (!(ex2 is WebException) && !(ex2 is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex2);
				}
				WebClient.AbortRequest(uploadBitsState.Request);
				if (uploadBitsState != null && uploadBitsState.ReadStream != null)
				{
					uploadBitsState.ReadStream.Close();
				}
			}
			finally
			{
				if (flag)
				{
					if (ex == null)
					{
						uploadBitsState.Close();
					}
					uploadBitsState.UploadCompletionDelegate(null, ex, uploadBitsState);
				}
			}
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x0008F81C File Offset: 0x0008DA1C
		private void UploadBits(WebRequest request, Stream readStream, byte[] buffer, int chunkSize, byte[] header, byte[] footer, CompletionDelegate uploadCompletionDelegate, CompletionDelegate downloadCompletionDelegate, AsyncOperation asyncOp)
		{
			if (request.RequestUri.Scheme == Uri.UriSchemeFile)
			{
				footer = (header = null);
			}
			WebClient.UploadBitsState uploadBitsState = new WebClient.UploadBitsState(request, readStream, buffer, chunkSize, header, footer, uploadCompletionDelegate, downloadCompletionDelegate, asyncOp, this.m_Progress, this);
			if (uploadBitsState.Async)
			{
				request.BeginGetRequestStream(new AsyncCallback(WebClient.UploadBitsRequestCallback), uploadBitsState);
				return;
			}
			Stream requestStream = request.GetRequestStream();
			uploadBitsState.SetRequestStream(requestStream);
			while (!uploadBitsState.WriteBytes())
			{
			}
			uploadBitsState.Close();
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x0008F89C File Offset: 0x0008DA9C
		private bool ByteArrayHasPrefix(byte[] prefix, byte[] byteArray)
		{
			if (prefix == null || byteArray == null || prefix.Length > byteArray.Length)
			{
				return false;
			}
			for (int i = 0; i < prefix.Length; i++)
			{
				if (prefix[i] != byteArray[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x0008F8D4 File Offset: 0x0008DAD4
		private string GetStringUsingEncoding(WebRequest request, byte[] data)
		{
			Encoding encoding = null;
			int num = -1;
			string text;
			try
			{
				text = request.ContentType;
			}
			catch (NotImplementedException)
			{
				text = null;
			}
			catch (NotSupportedException)
			{
				text = null;
			}
			if (text != null)
			{
				text = text.ToLower(CultureInfo.InvariantCulture);
				string[] array = text.Split(new char[] { ';', '=', ' ' });
				bool flag = false;
				foreach (string text2 in array)
				{
					if (text2 == "charset")
					{
						flag = true;
					}
					else if (flag)
					{
						try
						{
							encoding = Encoding.GetEncoding(text2);
						}
						catch (ArgumentException)
						{
							break;
						}
					}
				}
			}
			if (encoding == null)
			{
				Encoding[] array3 = new Encoding[]
				{
					Encoding.UTF8,
					Encoding.UTF32,
					Encoding.Unicode,
					Encoding.BigEndianUnicode
				};
				for (int j = 0; j < array3.Length; j++)
				{
					byte[] preamble = array3[j].GetPreamble();
					if (this.ByteArrayHasPrefix(preamble, data))
					{
						encoding = array3[j];
						num = preamble.Length;
						break;
					}
				}
			}
			if (encoding == null)
			{
				encoding = this.Encoding;
			}
			if (num == -1)
			{
				byte[] preamble2 = encoding.GetPreamble();
				if (this.ByteArrayHasPrefix(preamble2, data))
				{
					num = preamble2.Length;
				}
				else
				{
					num = 0;
				}
			}
			return encoding.GetString(data, num, data.Length - num);
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x0008FA20 File Offset: 0x0008DC20
		private string MapToDefaultMethod(Uri address)
		{
			Uri uri;
			if (!address.IsAbsoluteUri && this.m_baseAddress != null)
			{
				uri = new Uri(this.m_baseAddress, address);
			}
			else
			{
				uri = address;
			}
			if (uri.Scheme.ToLower(CultureInfo.InvariantCulture) == "ftp")
			{
				return "STOR";
			}
			return "POST";
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x0008FA7B File Offset: 0x0008DC7B
		private static string UrlEncode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return WebClient.UrlEncode(str, Encoding.UTF8);
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x0008FA8D File Offset: 0x0008DC8D
		private static string UrlEncode(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			return Encoding.ASCII.GetString(WebClient.UrlEncodeToBytes(str, e));
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x0008FAA8 File Offset: 0x0008DCA8
		private static byte[] UrlEncodeToBytes(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			byte[] bytes = e.GetBytes(str);
			return WebClient.UrlEncodeBytesToBytesInternal(bytes, 0, bytes.Length, false);
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x0008FAD0 File Offset: 0x0008DCD0
		private static byte[] UrlEncodeBytesToBytesInternal(byte[] bytes, int offset, int count, bool alwaysCreateReturnValue)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				char c = (char)bytes[offset + i];
				if (c == ' ')
				{
					num++;
				}
				else if (!WebClient.IsSafe(c))
				{
					num2++;
				}
			}
			if (!alwaysCreateReturnValue && num == 0 && num2 == 0)
			{
				return bytes;
			}
			byte[] array = new byte[count + num2 * 2];
			int num3 = 0;
			for (int j = 0; j < count; j++)
			{
				byte b = bytes[offset + j];
				char c2 = (char)b;
				if (WebClient.IsSafe(c2))
				{
					array[num3++] = b;
				}
				else if (c2 == ' ')
				{
					array[num3++] = 43;
				}
				else
				{
					array[num3++] = 37;
					array[num3++] = (byte)WebClient.IntToHex((b >> 4) & 15);
					array[num3++] = (byte)WebClient.IntToHex((int)(b & 15));
				}
			}
			return array;
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x0008FB9B File Offset: 0x0008DD9B
		private static char IntToHex(int n)
		{
			if (n <= 9)
			{
				return (char)(n + 48);
			}
			return (char)(n - 10 + 97);
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x0008FBB0 File Offset: 0x0008DDB0
		private static bool IsSafe(char ch)
		{
			if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
			{
				return true;
			}
			if (ch != '!')
			{
				switch (ch)
				{
				case '\'':
				case '(':
				case ')':
				case '*':
				case '-':
				case '.':
					return true;
				case '+':
				case ',':
					break;
				default:
					if (ch == '_')
					{
						return true;
					}
					break;
				}
				return false;
			}
			return true;
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x0008FC13 File Offset: 0x0008DE13
		private void InvokeOperationCompleted(AsyncOperation asyncOp, SendOrPostCallback callback, AsyncCompletedEventArgs eventArgs)
		{
			if (Interlocked.CompareExchange<AsyncOperation>(ref this.m_AsyncOp, null, asyncOp) == asyncOp)
			{
				this.CompleteWebClientState();
				asyncOp.PostOperationCompleted(callback, eventArgs);
			}
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x0008FC33 File Offset: 0x0008DE33
		private bool AnotherCallInProgress(int callNesting)
		{
			return callNesting > 1;
		}

		/// <summary>Occurs when an asynchronous operation to open a stream containing a resource completes.</summary>
		// Token: 0x1400004B RID: 75
		// (add) Token: 0x060024CD RID: 9421 RVA: 0x0008FC3C File Offset: 0x0008DE3C
		// (remove) Token: 0x060024CE RID: 9422 RVA: 0x0008FC74 File Offset: 0x0008DE74
		public event OpenReadCompletedEventHandler OpenReadCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.OpenReadCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.OpenReadCompletedEventArgs" />  object containing event data.</param>
		// Token: 0x060024CF RID: 9423 RVA: 0x0008FCA9 File Offset: 0x0008DEA9
		protected virtual void OnOpenReadCompleted(OpenReadCompletedEventArgs e)
		{
			if (this.OpenReadCompleted != null)
			{
				this.OpenReadCompleted(this, e);
			}
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x0008FCC0 File Offset: 0x0008DEC0
		private void OpenReadOperationCompleted(object arg)
		{
			this.OnOpenReadCompleted((OpenReadCompletedEventArgs)arg);
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x0008FCD0 File Offset: 0x0008DED0
		private void OpenReadAsyncCallback(IAsyncResult result)
		{
			Tuple<WebRequest, AsyncOperation> tuple = (Tuple<WebRequest, AsyncOperation>)result.AsyncState;
			WebRequest item = tuple.Item1;
			AsyncOperation item2 = tuple.Item2;
			Stream stream = null;
			Exception ex = null;
			try
			{
				stream = (this.m_WebResponse = this.GetWebResponse(item, result)).GetResponseStream();
			}
			catch (Exception ex2)
			{
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex2;
				if (!(ex2 is WebException) && !(ex2 is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex2);
				}
			}
			OpenReadCompletedEventArgs openReadCompletedEventArgs = new OpenReadCompletedEventArgs(stream, ex, this.m_Cancelled, item2.UserSuppliedState);
			this.InvokeOperationCompleted(item2, this.openReadOperationCompleted, openReadCompletedEventArgs);
		}

		/// <summary>Opens a readable stream containing the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and address is invalid.-or- An error occurred while downloading the resource. -or- An error occurred while opening the stream.</exception>
		// Token: 0x060024D2 RID: 9426 RVA: 0x0008FD90 File Offset: 0x0008DF90
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void OpenReadAsync(Uri address)
		{
			this.OpenReadAsync(address, null);
		}

		/// <summary>Opens a readable stream containing the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to retrieve.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and address is invalid.-or- An error occurred while downloading the resource. -or- An error occurred while opening the stream.</exception>
		// Token: 0x060024D3 RID: 9427 RVA: 0x0008FD9C File Offset: 0x0008DF9C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void OpenReadAsync(Uri address, object userToken)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			try
			{
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				webRequest.BeginGetResponse(new AsyncCallback(this.OpenReadAsyncCallback), new Tuple<WebRequest, AsyncOperation>(webRequest, asyncOperation));
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex);
				}
				OpenReadCompletedEventArgs openReadCompletedEventArgs = new OpenReadCompletedEventArgs(null, ex, this.m_Cancelled, asyncOperation.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOperation, this.openReadOperationCompleted, openReadCompletedEventArgs);
			}
			bool on2 = Logging.On;
		}

		/// <summary>Occurs when an asynchronous operation to open a stream to write data to a resource completes.</summary>
		// Token: 0x1400004C RID: 76
		// (add) Token: 0x060024D4 RID: 9428 RVA: 0x0008FE88 File Offset: 0x0008E088
		// (remove) Token: 0x060024D5 RID: 9429 RVA: 0x0008FEC0 File Offset: 0x0008E0C0
		public event OpenWriteCompletedEventHandler OpenWriteCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.OpenWriteCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.OpenWriteCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x060024D6 RID: 9430 RVA: 0x0008FEF5 File Offset: 0x0008E0F5
		protected virtual void OnOpenWriteCompleted(OpenWriteCompletedEventArgs e)
		{
			if (this.OpenWriteCompleted != null)
			{
				this.OpenWriteCompleted(this, e);
			}
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x0008FF0C File Offset: 0x0008E10C
		private void OpenWriteOperationCompleted(object arg)
		{
			this.OnOpenWriteCompleted((OpenWriteCompletedEventArgs)arg);
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x0008FF1C File Offset: 0x0008E11C
		private void OpenWriteAsyncCallback(IAsyncResult result)
		{
			Tuple<WebRequest, AsyncOperation> tuple = (Tuple<WebRequest, AsyncOperation>)result.AsyncState;
			WebRequest item = tuple.Item1;
			AsyncOperation item2 = tuple.Item2;
			WebClient.WebClientWriteStream webClientWriteStream = null;
			Exception ex = null;
			try
			{
				webClientWriteStream = new WebClient.WebClientWriteStream(item.EndGetRequestStream(result), item, this);
			}
			catch (Exception ex2)
			{
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex2;
				if (!(ex2 is WebException) && !(ex2 is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex2);
				}
			}
			OpenWriteCompletedEventArgs openWriteCompletedEventArgs = new OpenWriteCompletedEventArgs(webClientWriteStream, ex, this.m_Cancelled, item2.UserSuppliedState);
			this.InvokeOperationCompleted(item2, this.openWriteOperationCompleted, openWriteCompletedEventArgs);
		}

		/// <summary>Opens a stream for writing data to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		// Token: 0x060024D9 RID: 9433 RVA: 0x0008FFD4 File Offset: 0x0008E1D4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void OpenWriteAsync(Uri address)
		{
			this.OpenWriteAsync(address, null, null);
		}

		/// <summary>Opens a stream for writing data to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		// Token: 0x060024DA RID: 9434 RVA: 0x0008FFDF File Offset: 0x0008E1DF
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void OpenWriteAsync(Uri address, string method)
		{
			this.OpenWriteAsync(address, method, null);
		}

		/// <summary>Opens a stream for writing data to the specified resource, using the specified method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		// Token: 0x060024DB RID: 9435 RVA: 0x0008FFEC File Offset: 0x0008E1EC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void OpenWriteAsync(Uri address, string method, object userToken)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			try
			{
				this.m_Method = method;
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				webRequest.BeginGetRequestStream(new AsyncCallback(this.OpenWriteAsyncCallback), new Tuple<WebRequest, AsyncOperation>(webRequest, asyncOperation));
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex);
				}
				OpenWriteCompletedEventArgs openWriteCompletedEventArgs = new OpenWriteCompletedEventArgs(null, ex, this.m_Cancelled, asyncOperation.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOperation, this.openWriteOperationCompleted, openWriteCompletedEventArgs);
			}
			bool on2 = Logging.On;
		}

		/// <summary>Occurs when an asynchronous resource-download operation completes.</summary>
		// Token: 0x1400004D RID: 77
		// (add) Token: 0x060024DC RID: 9436 RVA: 0x000900EC File Offset: 0x0008E2EC
		// (remove) Token: 0x060024DD RID: 9437 RVA: 0x00090124 File Offset: 0x0008E324
		public event DownloadStringCompletedEventHandler DownloadStringCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.DownloadStringCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.DownloadStringCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x060024DE RID: 9438 RVA: 0x00090159 File Offset: 0x0008E359
		protected virtual void OnDownloadStringCompleted(DownloadStringCompletedEventArgs e)
		{
			if (this.DownloadStringCompleted != null)
			{
				this.DownloadStringCompleted(this, e);
			}
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x00090170 File Offset: 0x0008E370
		private void DownloadStringOperationCompleted(object arg)
		{
			this.OnDownloadStringCompleted((DownloadStringCompletedEventArgs)arg);
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x00090180 File Offset: 0x0008E380
		private void DownloadStringAsyncCallback(byte[] returnBytes, Exception exception, object state)
		{
			AsyncOperation asyncOperation = (AsyncOperation)state;
			string text = null;
			try
			{
				if (returnBytes != null)
				{
					text = this.GetStringUsingEncoding(this.m_WebRequest, returnBytes);
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				exception = ex;
			}
			DownloadStringCompletedEventArgs downloadStringCompletedEventArgs = new DownloadStringCompletedEventArgs(text, exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.downloadStringOperationCompleted, downloadStringCompletedEventArgs);
		}

		/// <summary>Downloads the resource specified as a <see cref="T:System.Uri" />. This method does not block the calling thread.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> containing the URI to download.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		// Token: 0x060024E1 RID: 9441 RVA: 0x000901FC File Offset: 0x0008E3FC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadStringAsync(Uri address)
		{
			this.DownloadStringAsync(address, null);
		}

		/// <summary>Downloads the specified string to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> containing the URI to download.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		// Token: 0x060024E2 RID: 9442 RVA: 0x00090208 File Offset: 0x0008E408
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadStringAsync(Uri address, object userToken)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			try
			{
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				this.DownloadBits(webRequest, null, new CompletionDelegate(this.DownloadStringAsyncCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex);
				}
				this.DownloadStringAsyncCallback(null, ex, asyncOperation);
			}
			bool on2 = Logging.On;
		}

		/// <summary>Occurs when an asynchronous data download operation completes.</summary>
		// Token: 0x1400004E RID: 78
		// (add) Token: 0x060024E3 RID: 9443 RVA: 0x000902D8 File Offset: 0x0008E4D8
		// (remove) Token: 0x060024E4 RID: 9444 RVA: 0x00090310 File Offset: 0x0008E510
		public event DownloadDataCompletedEventHandler DownloadDataCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.DownloadDataCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.DownloadDataCompletedEventArgs" /> object that contains event data.</param>
		// Token: 0x060024E5 RID: 9445 RVA: 0x00090345 File Offset: 0x0008E545
		protected virtual void OnDownloadDataCompleted(DownloadDataCompletedEventArgs e)
		{
			if (this.DownloadDataCompleted != null)
			{
				this.DownloadDataCompleted(this, e);
			}
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x0009035C File Offset: 0x0008E55C
		private void DownloadDataOperationCompleted(object arg)
		{
			this.OnDownloadDataCompleted((DownloadDataCompletedEventArgs)arg);
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x0009036C File Offset: 0x0008E56C
		private void DownloadDataAsyncCallback(byte[] returnBytes, Exception exception, object state)
		{
			AsyncOperation asyncOperation = (AsyncOperation)state;
			DownloadDataCompletedEventArgs downloadDataCompletedEventArgs = new DownloadDataCompletedEventArgs(returnBytes, exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.downloadDataOperationCompleted, downloadDataCompletedEventArgs);
		}

		/// <summary>Downloads the resource as a <see cref="T:System.Byte" /> array from the URI specified as an asynchronous operation. </summary>
		/// <param name="address">A <see cref="T:System.Uri" /> containing the URI to download.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		// Token: 0x060024E8 RID: 9448 RVA: 0x000903A2 File Offset: 0x0008E5A2
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadDataAsync(Uri address)
		{
			this.DownloadDataAsync(address, null);
		}

		/// <summary>Downloads the resource as a <see cref="T:System.Byte" /> array from the URI specified as an asynchronous operation. </summary>
		/// <param name="address">A <see cref="T:System.Uri" /> containing the URI to download.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		// Token: 0x060024E9 RID: 9449 RVA: 0x000903AC File Offset: 0x0008E5AC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadDataAsync(Uri address, object userToken)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			try
			{
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				this.DownloadBits(webRequest, null, new CompletionDelegate(this.DownloadDataAsyncCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex);
				}
				this.DownloadDataAsyncCallback(null, ex, asyncOperation);
			}
			bool on2 = Logging.On;
		}

		/// <summary>Occurs when an asynchronous file download operation completes.</summary>
		// Token: 0x1400004F RID: 79
		// (add) Token: 0x060024EA RID: 9450 RVA: 0x0009047C File Offset: 0x0008E67C
		// (remove) Token: 0x060024EB RID: 9451 RVA: 0x000904B4 File Offset: 0x0008E6B4
		public event AsyncCompletedEventHandler DownloadFileCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.DownloadFileCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x060024EC RID: 9452 RVA: 0x000904E9 File Offset: 0x0008E6E9
		protected virtual void OnDownloadFileCompleted(AsyncCompletedEventArgs e)
		{
			if (this.DownloadFileCompleted != null)
			{
				this.DownloadFileCompleted(this, e);
			}
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x00090500 File Offset: 0x0008E700
		private void DownloadFileOperationCompleted(object arg)
		{
			this.OnDownloadFileCompleted((AsyncCompletedEventArgs)arg);
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x00090510 File Offset: 0x0008E710
		private void DownloadFileAsyncCallback(byte[] returnBytes, Exception exception, object state)
		{
			AsyncOperation asyncOperation = (AsyncOperation)state;
			AsyncCompletedEventArgs asyncCompletedEventArgs = new AsyncCompletedEventArgs(exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.downloadFileOperationCompleted, asyncCompletedEventArgs);
		}

		/// <summary>Downloads, to a local file, the resource with the specified URI. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to download. </param>
		/// <param name="fileName">The name of the file to be placed on the local computer. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. -or-The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		/// <exception cref="T:System.InvalidOperationException">The local file specified by <paramref name="fileName" /> is in use by another thread.</exception>
		// Token: 0x060024EF RID: 9455 RVA: 0x00090545 File Offset: 0x0008E745
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadFileAsync(Uri address, string fileName)
		{
			this.DownloadFileAsync(address, fileName, null);
		}

		/// <summary>Downloads, to a local file, the resource with the specified URI. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to download. </param>
		/// <param name="fileName">The name of the file to be placed on the local computer. </param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. -or-The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		/// <exception cref="T:System.InvalidOperationException">The local file specified by <paramref name="fileName" /> is in use by another thread.</exception>
		// Token: 0x060024F0 RID: 9456 RVA: 0x00090550 File Offset: 0x0008E750
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadFileAsync(Uri address, string fileName, object userToken)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			FileStream fileStream = null;
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			try
			{
				fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				this.DownloadBits(webRequest, fileStream, new CompletionDelegate(this.DownloadFileAsyncCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (fileStream != null)
				{
					fileStream.Close();
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex);
				}
				this.DownloadFileAsyncCallback(null, ex, asyncOperation);
			}
			bool on2 = Logging.On;
		}

		/// <summary>Occurs when an asynchronous string-upload operation completes.</summary>
		// Token: 0x14000050 RID: 80
		// (add) Token: 0x060024F1 RID: 9457 RVA: 0x00090648 File Offset: 0x0008E848
		// (remove) Token: 0x060024F2 RID: 9458 RVA: 0x00090680 File Offset: 0x0008E880
		public event UploadStringCompletedEventHandler UploadStringCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadStringCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Net.UploadStringCompletedEventArgs" />  object containing event data.</param>
		// Token: 0x060024F3 RID: 9459 RVA: 0x000906B5 File Offset: 0x0008E8B5
		protected virtual void OnUploadStringCompleted(UploadStringCompletedEventArgs e)
		{
			if (this.UploadStringCompleted != null)
			{
				this.UploadStringCompleted(this, e);
			}
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x000906CC File Offset: 0x0008E8CC
		private void UploadStringOperationCompleted(object arg)
		{
			this.OnUploadStringCompleted((UploadStringCompletedEventArgs)arg);
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x000906DC File Offset: 0x0008E8DC
		private void StartDownloadAsync(WebClient.UploadBitsState state)
		{
			try
			{
				this.DownloadBits(state.Request, null, state.DownloadCompletionDelegate, state.AsyncOp);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex);
				}
				state.DownloadCompletionDelegate(null, ex, state.AsyncOp);
			}
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x00090768 File Offset: 0x0008E968
		private void UploadStringAsyncWriteCallback(byte[] returnBytes, Exception exception, object state)
		{
			WebClient.UploadBitsState uploadBitsState = (WebClient.UploadBitsState)state;
			if (exception != null)
			{
				UploadStringCompletedEventArgs uploadStringCompletedEventArgs = new UploadStringCompletedEventArgs(null, exception, this.m_Cancelled, uploadBitsState.AsyncOp.UserSuppliedState);
				this.InvokeOperationCompleted(uploadBitsState.AsyncOp, this.uploadStringOperationCompleted, uploadStringCompletedEventArgs);
				return;
			}
			this.StartDownloadAsync(uploadBitsState);
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000907B4 File Offset: 0x0008E9B4
		private void UploadStringAsyncReadCallback(byte[] returnBytes, Exception exception, object state)
		{
			AsyncOperation asyncOperation = (AsyncOperation)state;
			string text = null;
			try
			{
				if (returnBytes != null)
				{
					text = this.GetStringUsingEncoding(this.m_WebRequest, returnBytes);
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				exception = ex;
			}
			UploadStringCompletedEventArgs uploadStringCompletedEventArgs = new UploadStringCompletedEventArgs(text, exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.uploadStringOperationCompleted, uploadStringCompletedEventArgs);
		}

		/// <summary>Uploads the specified string to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the string. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page. </param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x060024F8 RID: 9464 RVA: 0x00090830 File Offset: 0x0008EA30
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadStringAsync(Uri address, string data)
		{
			this.UploadStringAsync(address, null, data, null);
		}

		/// <summary>Uploads the specified string to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the string. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or-<paramref name="method" /> cannot be used to send content.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x060024F9 RID: 9465 RVA: 0x0009083C File Offset: 0x0008EA3C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadStringAsync(Uri address, string method, string data)
		{
			this.UploadStringAsync(address, method, data, null);
		}

		/// <summary>Uploads the specified string to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the string. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or-<paramref name="method" /> cannot be used to send content.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x060024FA RID: 9466 RVA: 0x00090848 File Offset: 0x0008EA48
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadStringAsync(Uri address, string method, string data, object userToken)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			try
			{
				byte[] bytes = this.Encoding.GetBytes(data);
				this.m_Method = method;
				this.m_ContentLength = (long)bytes.Length;
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				this.UploadBits(webRequest, null, bytes, 0, null, null, new CompletionDelegate(this.UploadStringAsyncWriteCallback), new CompletionDelegate(this.UploadStringAsyncReadCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex);
				}
				UploadStringCompletedEventArgs uploadStringCompletedEventArgs = new UploadStringCompletedEventArgs(null, ex, this.m_Cancelled, asyncOperation.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOperation, this.uploadStringOperationCompleted, uploadStringCompletedEventArgs);
			}
			bool on2 = Logging.On;
		}

		/// <summary>Occurs when an asynchronous data-upload operation completes.</summary>
		// Token: 0x14000051 RID: 81
		// (add) Token: 0x060024FB RID: 9467 RVA: 0x00090984 File Offset: 0x0008EB84
		// (remove) Token: 0x060024FC RID: 9468 RVA: 0x000909BC File Offset: 0x0008EBBC
		public event UploadDataCompletedEventHandler UploadDataCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadDataCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.UploadDataCompletedEventArgs" />  object containing event data.</param>
		// Token: 0x060024FD RID: 9469 RVA: 0x000909F1 File Offset: 0x0008EBF1
		protected virtual void OnUploadDataCompleted(UploadDataCompletedEventArgs e)
		{
			if (this.UploadDataCompleted != null)
			{
				this.UploadDataCompleted(this, e);
			}
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x00090A08 File Offset: 0x0008EC08
		private void UploadDataOperationCompleted(object arg)
		{
			this.OnUploadDataCompleted((UploadDataCompletedEventArgs)arg);
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x00090A18 File Offset: 0x0008EC18
		private void UploadDataAsyncWriteCallback(byte[] returnBytes, Exception exception, object state)
		{
			WebClient.UploadBitsState uploadBitsState = (WebClient.UploadBitsState)state;
			if (exception != null)
			{
				UploadDataCompletedEventArgs uploadDataCompletedEventArgs = new UploadDataCompletedEventArgs(returnBytes, exception, this.m_Cancelled, uploadBitsState.AsyncOp.UserSuppliedState);
				this.InvokeOperationCompleted(uploadBitsState.AsyncOp, this.uploadDataOperationCompleted, uploadDataCompletedEventArgs);
				return;
			}
			this.StartDownloadAsync(uploadBitsState);
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x00090A64 File Offset: 0x0008EC64
		private void UploadDataAsyncReadCallback(byte[] returnBytes, Exception exception, object state)
		{
			AsyncOperation asyncOperation = (AsyncOperation)state;
			UploadDataCompletedEventArgs uploadDataCompletedEventArgs = new UploadDataCompletedEventArgs(returnBytes, exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.uploadDataOperationCompleted, uploadDataCompletedEventArgs);
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI, using the POST method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="data">The data buffer to send to the resource. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x06002501 RID: 9473 RVA: 0x00090A9A File Offset: 0x0008EC9A
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadDataAsync(Uri address, byte[] data)
		{
			this.UploadDataAsync(address, null, data, null);
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI, using the specified method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x06002502 RID: 9474 RVA: 0x00090AA6 File Offset: 0x0008ECA6
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadDataAsync(Uri address, string method, byte[] data)
		{
			this.UploadDataAsync(address, method, data, null);
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI, using the specified method and identifying token.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x06002503 RID: 9475 RVA: 0x00090AB4 File Offset: 0x0008ECB4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadDataAsync(Uri address, string method, byte[] data, object userToken)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			int num = 0;
			try
			{
				this.m_Method = method;
				this.m_ContentLength = (long)data.Length;
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				if (this.UploadProgressChanged != null)
				{
					num = (int)Math.Min(8192L, (long)data.Length);
				}
				this.UploadBits(webRequest, null, data, num, null, null, new CompletionDelegate(this.UploadDataAsyncWriteCallback), new CompletionDelegate(this.UploadDataAsyncReadCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex);
				}
				UploadDataCompletedEventArgs uploadDataCompletedEventArgs = new UploadDataCompletedEventArgs(null, ex, this.m_Cancelled, asyncOperation.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOperation, this.uploadDataOperationCompleted, uploadDataCompletedEventArgs);
			}
			bool on2 = Logging.On;
		}

		/// <summary>Occurs when an asynchronous file-upload operation completes.</summary>
		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06002504 RID: 9476 RVA: 0x00090BFC File Offset: 0x0008EDFC
		// (remove) Token: 0x06002505 RID: 9477 RVA: 0x00090C34 File Offset: 0x0008EE34
		public event UploadFileCompletedEventHandler UploadFileCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadFileCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Net.UploadFileCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x06002506 RID: 9478 RVA: 0x00090C69 File Offset: 0x0008EE69
		protected virtual void OnUploadFileCompleted(UploadFileCompletedEventArgs e)
		{
			if (this.UploadFileCompleted != null)
			{
				this.UploadFileCompleted(this, e);
			}
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x00090C80 File Offset: 0x0008EE80
		private void UploadFileOperationCompleted(object arg)
		{
			this.OnUploadFileCompleted((UploadFileCompletedEventArgs)arg);
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x00090C90 File Offset: 0x0008EE90
		private void UploadFileAsyncWriteCallback(byte[] returnBytes, Exception exception, object state)
		{
			WebClient.UploadBitsState uploadBitsState = (WebClient.UploadBitsState)state;
			if (exception != null)
			{
				UploadFileCompletedEventArgs uploadFileCompletedEventArgs = new UploadFileCompletedEventArgs(returnBytes, exception, this.m_Cancelled, uploadBitsState.AsyncOp.UserSuppliedState);
				this.InvokeOperationCompleted(uploadBitsState.AsyncOp, this.uploadFileOperationCompleted, uploadFileCompletedEventArgs);
				return;
			}
			this.StartDownloadAsync(uploadBitsState);
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x00090CDC File Offset: 0x0008EEDC
		private void UploadFileAsyncReadCallback(byte[] returnBytes, Exception exception, object state)
		{
			AsyncOperation asyncOperation = (AsyncOperation)state;
			UploadFileCompletedEventArgs uploadFileCompletedEventArgs = new UploadFileCompletedEventArgs(returnBytes, exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.uploadFileOperationCompleted, uploadFileCompletedEventArgs);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the POST method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page. </param>
		/// <param name="fileName">The file to send to the resource. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. -or-The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x0600250A RID: 9482 RVA: 0x00090D12 File Offset: 0x0008EF12
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadFileAsync(Uri address, string fileName)
		{
			this.UploadFileAsync(address, null, fileName, null);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the POST method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page. </param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The file to send to the resource. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. -or-The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x0600250B RID: 9483 RVA: 0x00090D1E File Offset: 0x0008EF1E
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadFileAsync(Uri address, string method, string fileName)
		{
			this.UploadFileAsync(address, method, fileName, null);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the POST method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The file to send to the resource.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. -or-The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x0600250C RID: 9484 RVA: 0x00090D2C File Offset: 0x0008EF2C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadFileAsync(Uri address, string method, string fileName, object userToken)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			FileStream fileStream = null;
			try
			{
				this.m_Method = method;
				byte[] array = null;
				byte[] array2 = null;
				byte[] array3 = null;
				Uri uri = this.GetUri(address);
				bool flag = uri.Scheme != Uri.UriSchemeFile;
				this.OpenFileInternal(flag, fileName, ref fileStream, ref array3, ref array, ref array2);
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(uri));
				this.UploadBits(webRequest, fileStream, array3, 0, array, array2, new CompletionDelegate(this.UploadFileAsyncWriteCallback), new CompletionDelegate(this.UploadFileAsyncReadCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (fileStream != null)
				{
					fileStream.Close();
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex);
				}
				UploadFileCompletedEventArgs uploadFileCompletedEventArgs = new UploadFileCompletedEventArgs(null, ex, this.m_Cancelled, asyncOperation.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOperation, this.uploadFileOperationCompleted, uploadFileCompletedEventArgs);
			}
			bool on2 = Logging.On;
		}

		/// <summary>Occurs when an asynchronous upload of a name/value collection completes.</summary>
		// Token: 0x14000053 RID: 83
		// (add) Token: 0x0600250D RID: 9485 RVA: 0x00090E90 File Offset: 0x0008F090
		// (remove) Token: 0x0600250E RID: 9486 RVA: 0x00090EC8 File Offset: 0x0008F0C8
		public event UploadValuesCompletedEventHandler UploadValuesCompleted;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadValuesCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.UploadValuesCompletedEventArgs" />  object containing event data.</param>
		// Token: 0x0600250F RID: 9487 RVA: 0x00090EFD File Offset: 0x0008F0FD
		protected virtual void OnUploadValuesCompleted(UploadValuesCompletedEventArgs e)
		{
			if (this.UploadValuesCompleted != null)
			{
				this.UploadValuesCompleted(this, e);
			}
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x00090F14 File Offset: 0x0008F114
		private void UploadValuesOperationCompleted(object arg)
		{
			this.OnUploadValuesCompleted((UploadValuesCompletedEventArgs)arg);
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x00090F24 File Offset: 0x0008F124
		private void UploadValuesAsyncWriteCallback(byte[] returnBytes, Exception exception, object state)
		{
			WebClient.UploadBitsState uploadBitsState = (WebClient.UploadBitsState)state;
			if (exception != null)
			{
				UploadValuesCompletedEventArgs uploadValuesCompletedEventArgs = new UploadValuesCompletedEventArgs(returnBytes, exception, this.m_Cancelled, uploadBitsState.AsyncOp.UserSuppliedState);
				this.InvokeOperationCompleted(uploadBitsState.AsyncOp, this.uploadValuesOperationCompleted, uploadValuesCompletedEventArgs);
				return;
			}
			this.StartDownloadAsync(uploadBitsState);
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x00090F70 File Offset: 0x0008F170
		private void UploadValuesAsyncReadCallback(byte[] returnBytes, Exception exception, object state)
		{
			AsyncOperation asyncOperation = (AsyncOperation)state;
			UploadValuesCompletedEventArgs uploadValuesCompletedEventArgs = new UploadValuesCompletedEventArgs(returnBytes, exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.uploadValuesOperationCompleted, uploadValuesCompletedEventArgs);
		}

		/// <summary>Uploads the data in the specified name/value collection to the resource identified by the specified URI. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the collection. This URI must identify a resource that can accept a request sent with the default method. See remarks.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x06002513 RID: 9491 RVA: 0x00090FA6 File Offset: 0x0008F1A6
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadValuesAsync(Uri address, NameValueCollection data)
		{
			this.UploadValuesAsync(address, null, data, null);
		}

		/// <summary>Uploads the data in the specified name/value collection to the resource identified by the specified URI, using the specified method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the collection. This URI must identify a resource that can accept a request sent with the <paramref name="method" /> method.</param>
		/// <param name="method">The method used to send the string to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.-or-<paramref name="method" /> cannot be used to send content.</exception>
		// Token: 0x06002514 RID: 9492 RVA: 0x00090FB2 File Offset: 0x0008F1B2
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadValuesAsync(Uri address, string method, NameValueCollection data)
		{
			this.UploadValuesAsync(address, method, data, null);
		}

		/// <summary>Uploads the data in the specified name/value collection to the resource identified by the specified URI, using the specified method. This method does not block the calling thread, and allows the caller to pass an object to the method that is invoked when the operation completes.</summary>
		/// <param name="address">The URI of the resource to receive the collection. This URI must identify a resource that can accept a request sent with the <paramref name="method" /> method.</param>
		/// <param name="method">The HTTP method used to send the string to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.-or-<paramref name="method" /> cannot be used to send content.</exception>
		// Token: 0x06002515 RID: 9493 RVA: 0x00090FC0 File Offset: 0x0008F1C0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadValuesAsync(Uri address, string method, NameValueCollection data, object userToken)
		{
			bool on = Logging.On;
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			int num = 0;
			try
			{
				byte[] array = this.UploadValuesInternal(data);
				this.m_Method = method;
				WebRequest webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				if (this.UploadProgressChanged != null)
				{
					num = (int)Math.Min(8192L, (long)array.Length);
				}
				this.UploadBits(webRequest, null, array, num, null, null, new CompletionDelegate(this.UploadValuesAsyncWriteCallback), new CompletionDelegate(this.UploadValuesAsyncReadCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(global::SR.GetString("An exception occurred during a WebClient request."), ex);
				}
				UploadValuesCompletedEventArgs uploadValuesCompletedEventArgs = new UploadValuesCompletedEventArgs(null, ex, this.m_Cancelled, asyncOperation.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOperation, this.uploadValuesOperationCompleted, uploadValuesCompletedEventArgs);
			}
			bool on2 = Logging.On;
		}

		/// <summary>Cancels a pending asynchronous operation.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002516 RID: 9494 RVA: 0x00091108 File Offset: 0x0008F308
		public void CancelAsync()
		{
			WebRequest webRequest = this.m_WebRequest;
			this.m_Cancelled = true;
			WebClient.AbortRequest(webRequest);
		}

		/// <summary>Downloads the resource as a <see cref="T:System.String" /> from the URI specified as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the downloaded resource.</returns>
		/// <param name="address">The URI of the resource to download.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		// Token: 0x06002517 RID: 9495 RVA: 0x0009111C File Offset: 0x0008F31C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<string> DownloadStringTaskAsync(string address)
		{
			return this.DownloadStringTaskAsync(this.GetUri(address));
		}

		/// <summary>Downloads the resource as a <see cref="T:System.String" /> from the URI specified as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the downloaded resource.</returns>
		/// <param name="address">The URI of the resource to download.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		// Token: 0x06002518 RID: 9496 RVA: 0x0009112C File Offset: 0x0008F32C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<string> DownloadStringTaskAsync(Uri address)
		{
			TaskCompletionSource<string> tcs = new TaskCompletionSource<string>(address);
			DownloadStringCompletedEventHandler handler = null;
			handler = delegate(object sender, DownloadStringCompletedEventArgs e)
			{
				this.HandleCompletion<DownloadStringCompletedEventArgs, DownloadStringCompletedEventHandler, string>(tcs, e, (DownloadStringCompletedEventArgs args) => args.Result, handler, delegate(WebClient webClient, DownloadStringCompletedEventHandler completion)
				{
					webClient.DownloadStringCompleted -= completion;
				});
			};
			this.DownloadStringCompleted += handler;
			try
			{
				this.DownloadStringAsync(address, tcs);
			}
			catch
			{
				this.DownloadStringCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		/// <summary>Opens a readable stream containing the specified resource as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.IO.Stream" /> used to read data from a resource.</returns>
		/// <param name="address">The URI of the resource to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and address is invalid.-or- An error occurred while downloading the resource. -or- An error occurred while opening the stream.</exception>
		// Token: 0x06002519 RID: 9497 RVA: 0x000911B0 File Offset: 0x0008F3B0
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<Stream> OpenReadTaskAsync(string address)
		{
			return this.OpenReadTaskAsync(this.GetUri(address));
		}

		/// <summary>Opens a readable stream containing the specified resource as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.IO.Stream" /> used to read data from a resource.</returns>
		/// <param name="address">The URI of the resource to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and address is invalid.-or- An error occurred while downloading the resource. -or- An error occurred while opening the stream.</exception>
		// Token: 0x0600251A RID: 9498 RVA: 0x000911C0 File Offset: 0x0008F3C0
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<Stream> OpenReadTaskAsync(Uri address)
		{
			TaskCompletionSource<Stream> tcs = new TaskCompletionSource<Stream>(address);
			OpenReadCompletedEventHandler handler = null;
			handler = delegate(object sender, OpenReadCompletedEventArgs e)
			{
				this.HandleCompletion<OpenReadCompletedEventArgs, OpenReadCompletedEventHandler, Stream>(tcs, e, (OpenReadCompletedEventArgs args) => args.Result, handler, delegate(WebClient webClient, OpenReadCompletedEventHandler completion)
				{
					webClient.OpenReadCompleted -= completion;
				});
			};
			this.OpenReadCompleted += handler;
			try
			{
				this.OpenReadAsync(address, tcs);
			}
			catch
			{
				this.OpenReadCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		/// <summary>Opens a stream for writing data to the specified resource as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		// Token: 0x0600251B RID: 9499 RVA: 0x00091244 File Offset: 0x0008F444
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<Stream> OpenWriteTaskAsync(string address)
		{
			return this.OpenWriteTaskAsync(this.GetUri(address), null);
		}

		/// <summary>Opens a stream for writing data to the specified resource as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		// Token: 0x0600251C RID: 9500 RVA: 0x00091254 File Offset: 0x0008F454
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<Stream> OpenWriteTaskAsync(Uri address)
		{
			return this.OpenWriteTaskAsync(address, null);
		}

		/// <summary>Opens a stream for writing data to the specified resource as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		// Token: 0x0600251D RID: 9501 RVA: 0x0009125E File Offset: 0x0008F45E
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<Stream> OpenWriteTaskAsync(string address, string method)
		{
			return this.OpenWriteTaskAsync(this.GetUri(address), method);
		}

		/// <summary>Opens a stream for writing data to the specified resource as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		// Token: 0x0600251E RID: 9502 RVA: 0x00091270 File Offset: 0x0008F470
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<Stream> OpenWriteTaskAsync(Uri address, string method)
		{
			TaskCompletionSource<Stream> tcs = new TaskCompletionSource<Stream>(address);
			OpenWriteCompletedEventHandler handler = null;
			handler = delegate(object sender, OpenWriteCompletedEventArgs e)
			{
				this.HandleCompletion<OpenWriteCompletedEventArgs, OpenWriteCompletedEventHandler, Stream>(tcs, e, (OpenWriteCompletedEventArgs args) => args.Result, handler, delegate(WebClient webClient, OpenWriteCompletedEventHandler completion)
				{
					webClient.OpenWriteCompleted -= completion;
				});
			};
			this.OpenWriteCompleted += handler;
			try
			{
				this.OpenWriteAsync(address, method, tcs);
			}
			catch
			{
				this.OpenWriteCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		/// <summary>Uploads the specified string to the specified resource as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the string. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x0600251F RID: 9503 RVA: 0x000912F8 File Offset: 0x0008F4F8
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<string> UploadStringTaskAsync(string address, string data)
		{
			return this.UploadStringTaskAsync(address, null, data);
		}

		/// <summary>Uploads the specified string to the specified resource as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the string. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x06002520 RID: 9504 RVA: 0x00091303 File Offset: 0x0008F503
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<string> UploadStringTaskAsync(Uri address, string data)
		{
			return this.UploadStringTaskAsync(address, null, data);
		}

		/// <summary>Uploads the specified string to the specified resource as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the string. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or-<paramref name="method" /> cannot be used to send content.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x06002521 RID: 9505 RVA: 0x0009130E File Offset: 0x0008F50E
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<string> UploadStringTaskAsync(string address, string method, string data)
		{
			return this.UploadStringTaskAsync(this.GetUri(address), method, data);
		}

		/// <summary>Uploads the specified string to the specified resource as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the string. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or-<paramref name="method" /> cannot be used to send content.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x06002522 RID: 9506 RVA: 0x00091320 File Offset: 0x0008F520
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<string> UploadStringTaskAsync(Uri address, string method, string data)
		{
			TaskCompletionSource<string> tcs = new TaskCompletionSource<string>(address);
			UploadStringCompletedEventHandler handler = null;
			handler = delegate(object sender, UploadStringCompletedEventArgs e)
			{
				this.HandleCompletion<UploadStringCompletedEventArgs, UploadStringCompletedEventHandler, string>(tcs, e, (UploadStringCompletedEventArgs args) => args.Result, handler, delegate(WebClient webClient, UploadStringCompletedEventHandler completion)
				{
					webClient.UploadStringCompleted -= completion;
				});
			};
			this.UploadStringCompleted += handler;
			try
			{
				this.UploadStringAsync(address, method, data, tcs);
			}
			catch
			{
				this.UploadStringCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		/// <summary>Downloads the resource as a <see cref="T:System.Byte" /> array from the URI specified as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the downloaded resource.</returns>
		/// <param name="address">The URI of the resource to download. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		// Token: 0x06002523 RID: 9507 RVA: 0x000913A8 File Offset: 0x0008F5A8
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> DownloadDataTaskAsync(string address)
		{
			return this.DownloadDataTaskAsync(this.GetUri(address));
		}

		/// <summary>Downloads the resource as a <see cref="T:System.Byte" /> array from the URI specified as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the downloaded resource.</returns>
		/// <param name="address">The URI of the resource to download.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		// Token: 0x06002524 RID: 9508 RVA: 0x000913B8 File Offset: 0x0008F5B8
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> DownloadDataTaskAsync(Uri address)
		{
			TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>(address);
			DownloadDataCompletedEventHandler handler = null;
			handler = delegate(object sender, DownloadDataCompletedEventArgs e)
			{
				this.HandleCompletion<DownloadDataCompletedEventArgs, DownloadDataCompletedEventHandler, byte[]>(tcs, e, (DownloadDataCompletedEventArgs args) => args.Result, handler, delegate(WebClient webClient, DownloadDataCompletedEventHandler completion)
				{
					webClient.DownloadDataCompleted -= completion;
				});
			};
			this.DownloadDataCompleted += handler;
			try
			{
				this.DownloadDataAsync(address, tcs);
			}
			catch
			{
				this.DownloadDataCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		/// <summary>Downloads the specified resource to a local file as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="address">The URI of the resource to download.</param>
		/// <param name="fileName">The name of the file to be placed on the local computer.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. -or-The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		/// <exception cref="T:System.InvalidOperationException">The local file specified by <paramref name="fileName" /> is in use by another thread.</exception>
		// Token: 0x06002525 RID: 9509 RVA: 0x0009143C File Offset: 0x0008F63C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task DownloadFileTaskAsync(string address, string fileName)
		{
			return this.DownloadFileTaskAsync(this.GetUri(address), fileName);
		}

		/// <summary>Downloads the specified resource to a local file as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="address">The URI of the resource to download.</param>
		/// <param name="fileName">The name of the file to be placed on the local computer.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. -or-The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		/// <exception cref="T:System.InvalidOperationException">The local file specified by <paramref name="fileName" /> is in use by another thread.</exception>
		// Token: 0x06002526 RID: 9510 RVA: 0x0009144C File Offset: 0x0008F64C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task DownloadFileTaskAsync(Uri address, string fileName)
		{
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>(address);
			AsyncCompletedEventHandler handler = null;
			handler = delegate(object sender, AsyncCompletedEventArgs e)
			{
				this.HandleCompletion<AsyncCompletedEventArgs, AsyncCompletedEventHandler, object>(tcs, e, (AsyncCompletedEventArgs args) => null, handler, delegate(WebClient webClient, AsyncCompletedEventHandler completion)
				{
					webClient.DownloadFileCompleted -= completion;
				});
			};
			this.DownloadFileCompleted += handler;
			try
			{
				this.DownloadFileAsync(address, fileName, tcs);
			}
			catch
			{
				this.DownloadFileCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		/// <summary>Uploads a data buffer that contains a <see cref="T:System.Byte" /> array to the URI specified as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the body of the response received from the resource when the data buffer was uploaded.</returns>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x06002527 RID: 9511 RVA: 0x000914D4 File Offset: 0x0008F6D4
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadDataTaskAsync(string address, byte[] data)
		{
			return this.UploadDataTaskAsync(this.GetUri(address), null, data);
		}

		/// <summary>Uploads a data buffer that contains a <see cref="T:System.Byte" /> array to the URI specified as an asynchronous operation using a task object.  </summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the body of the response received from the resource when the data buffer was uploaded.</returns>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x06002528 RID: 9512 RVA: 0x000914E5 File Offset: 0x0008F6E5
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadDataTaskAsync(Uri address, byte[] data)
		{
			return this.UploadDataTaskAsync(address, null, data);
		}

		/// <summary>Uploads a data buffer that contains a <see cref="T:System.Byte" /> array to the URI specified as an asynchronous operation using a task object.  </summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the body of the response received from the resource when the data buffer was uploaded.</returns>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x06002529 RID: 9513 RVA: 0x000914F0 File Offset: 0x0008F6F0
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadDataTaskAsync(string address, string method, byte[] data)
		{
			return this.UploadDataTaskAsync(this.GetUri(address), method, data);
		}

		/// <summary>Uploads a data buffer that contains a <see cref="T:System.Byte" /> array to the URI specified as an asynchronous operation using a task object.  </summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the body of the response received from the resource when the data buffer was uploaded.</returns>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x0600252A RID: 9514 RVA: 0x00091504 File Offset: 0x0008F704
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadDataTaskAsync(Uri address, string method, byte[] data)
		{
			TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>(address);
			UploadDataCompletedEventHandler handler = null;
			handler = delegate(object sender, UploadDataCompletedEventArgs e)
			{
				this.HandleCompletion<UploadDataCompletedEventArgs, UploadDataCompletedEventHandler, byte[]>(tcs, e, (UploadDataCompletedEventArgs args) => args.Result, handler, delegate(WebClient webClient, UploadDataCompletedEventHandler completion)
				{
					webClient.UploadDataCompleted -= completion;
				});
			};
			this.UploadDataCompleted += handler;
			try
			{
				this.UploadDataAsync(address, method, data, tcs);
			}
			catch
			{
				this.UploadDataCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		/// <summary>Uploads the specified local file to a resource as an asynchronous operation using a task object. </summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the body of the response received from the resource when the file was uploaded.</returns>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="fileName">The local file to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. -or-The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x0600252B RID: 9515 RVA: 0x0009158C File Offset: 0x0008F78C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadFileTaskAsync(string address, string fileName)
		{
			return this.UploadFileTaskAsync(this.GetUri(address), null, fileName);
		}

		/// <summary>Uploads the specified local file to a resource as an asynchronous operation using a task object. </summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the body of the response received from the resource when the file was uploaded.</returns>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="fileName">The local file to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. -or-The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x0600252C RID: 9516 RVA: 0x0009159D File Offset: 0x0008F79D
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadFileTaskAsync(Uri address, string fileName)
		{
			return this.UploadFileTaskAsync(address, null, fileName);
		}

		/// <summary>Uploads the specified local file to a resource as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the body of the response received from the resource when the file was uploaded.</returns>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The local file to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. -or-The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x0600252D RID: 9517 RVA: 0x000915A8 File Offset: 0x0008F7A8
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadFileTaskAsync(string address, string method, string fileName)
		{
			return this.UploadFileTaskAsync(this.GetUri(address), method, fileName);
		}

		/// <summary>Uploads the specified local file to a resource as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the body of the response received from the resource when the file was uploaded.</returns>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The local file to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null. -or-The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x0600252E RID: 9518 RVA: 0x000915BC File Offset: 0x0008F7BC
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadFileTaskAsync(Uri address, string method, string fileName)
		{
			TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>(address);
			UploadFileCompletedEventHandler handler = null;
			handler = delegate(object sender, UploadFileCompletedEventArgs e)
			{
				this.HandleCompletion<UploadFileCompletedEventArgs, UploadFileCompletedEventHandler, byte[]>(tcs, e, (UploadFileCompletedEventArgs args) => args.Result, handler, delegate(WebClient webClient, UploadFileCompletedEventHandler completion)
				{
					webClient.UploadFileCompleted -= completion;
				});
			};
			this.UploadFileCompleted += handler;
			try
			{
				this.UploadFileAsync(address, method, fileName, tcs);
			}
			catch
			{
				this.UploadFileCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the collection.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.-or- An error occurred while opening the stream.-or- The Content-type header is not null or "application/x-www-form-urlencoded". </exception>
		// Token: 0x0600252F RID: 9519 RVA: 0x00091644 File Offset: 0x0008F844
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadValuesTaskAsync(string address, NameValueCollection data)
		{
			return this.UploadValuesTaskAsync(this.GetUri(address), null, data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the collection.</param>
		/// <param name="method">The HTTP method used to send the collection to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or-<paramref name="method" /> cannot be used to send content.-or- There was no response from the server hosting the resource.-or- An error occurred while opening the stream.-or- The Content-type header is not null or "application/x-www-form-urlencoded". </exception>
		// Token: 0x06002530 RID: 9520 RVA: 0x00091655 File Offset: 0x0008F855
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadValuesTaskAsync(string address, string method, NameValueCollection data)
		{
			return this.UploadValuesTaskAsync(this.GetUri(address), method, data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the collection.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header value is not null and is not application/x-www-form-urlencoded. </exception>
		// Token: 0x06002531 RID: 9521 RVA: 0x00091666 File Offset: 0x0008F866
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadValuesTaskAsync(Uri address, NameValueCollection data)
		{
			return this.UploadValuesTaskAsync(address, null, data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI as an asynchronous operation using a task object.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the collection.</param>
		/// <param name="method">The HTTP method used to send the collection to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="address" /> parameter is null.-or-The <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or-<paramref name="method" /> cannot be used to send content.-or- There was no response from the server hosting the resource.-or- An error occurred while opening the stream.-or- The Content-type header is not null or "application/x-www-form-urlencoded". </exception>
		// Token: 0x06002532 RID: 9522 RVA: 0x00091674 File Offset: 0x0008F874
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<byte[]> UploadValuesTaskAsync(Uri address, string method, NameValueCollection data)
		{
			TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>(address);
			UploadValuesCompletedEventHandler handler = null;
			handler = delegate(object sender, UploadValuesCompletedEventArgs e)
			{
				this.HandleCompletion<UploadValuesCompletedEventArgs, UploadValuesCompletedEventHandler, byte[]>(tcs, e, (UploadValuesCompletedEventArgs args) => args.Result, handler, delegate(WebClient webClient, UploadValuesCompletedEventHandler completion)
				{
					webClient.UploadValuesCompleted -= completion;
				});
			};
			this.UploadValuesCompleted += handler;
			try
			{
				this.UploadValuesAsync(address, method, data, tcs);
			}
			catch
			{
				this.UploadValuesCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x000916FC File Offset: 0x0008F8FC
		private void HandleCompletion<TAsyncCompletedEventArgs, TCompletionDelegate, T>(TaskCompletionSource<T> tcs, TAsyncCompletedEventArgs e, Func<TAsyncCompletedEventArgs, T> getResult, TCompletionDelegate handler, Action<WebClient, TCompletionDelegate> unregisterHandler) where TAsyncCompletedEventArgs : AsyncCompletedEventArgs
		{
			if (e.UserState == tcs)
			{
				try
				{
					unregisterHandler(this, handler);
				}
				finally
				{
					if (e.Error != null)
					{
						tcs.TrySetException(e.Error);
					}
					else if (e.Cancelled)
					{
						tcs.TrySetCanceled();
					}
					else
					{
						tcs.TrySetResult(getResult(e));
					}
				}
			}
		}

		/// <summary>Occurs when an asynchronous download operation successfully transfers some or all of the data.</summary>
		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06002534 RID: 9524 RVA: 0x0009177C File Offset: 0x0008F97C
		// (remove) Token: 0x06002535 RID: 9525 RVA: 0x000917B4 File Offset: 0x0008F9B4
		public event DownloadProgressChangedEventHandler DownloadProgressChanged;

		/// <summary>Occurs when an asynchronous upload operation successfully transfers some or all of the data.</summary>
		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06002536 RID: 9526 RVA: 0x000917EC File Offset: 0x0008F9EC
		// (remove) Token: 0x06002537 RID: 9527 RVA: 0x00091824 File Offset: 0x0008FA24
		public event UploadProgressChangedEventHandler UploadProgressChanged;

		/// <summary>Raises the <see cref="E:System.Net.WebClient.DownloadProgressChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.DownloadProgressChangedEventArgs" /> object containing event data.</param>
		// Token: 0x06002538 RID: 9528 RVA: 0x00091859 File Offset: 0x0008FA59
		protected virtual void OnDownloadProgressChanged(DownloadProgressChangedEventArgs e)
		{
			if (this.DownloadProgressChanged != null)
			{
				this.DownloadProgressChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadProgressChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Net.UploadProgressChangedEventArgs" /> object containing event data.</param>
		// Token: 0x06002539 RID: 9529 RVA: 0x00091870 File Offset: 0x0008FA70
		protected virtual void OnUploadProgressChanged(UploadProgressChangedEventArgs e)
		{
			if (this.UploadProgressChanged != null)
			{
				this.UploadProgressChanged(this, e);
			}
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x00091887 File Offset: 0x0008FA87
		private void ReportDownloadProgressChanged(object arg)
		{
			this.OnDownloadProgressChanged((DownloadProgressChangedEventArgs)arg);
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x00091895 File Offset: 0x0008FA95
		private void ReportUploadProgressChanged(object arg)
		{
			this.OnUploadProgressChanged((UploadProgressChangedEventArgs)arg);
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x000918A4 File Offset: 0x0008FAA4
		private void PostProgressChanged(AsyncOperation asyncOp, WebClient.ProgressData progress)
		{
			if (asyncOp != null && progress.BytesSent + progress.BytesReceived > 0L)
			{
				int num;
				if (progress.HasUploadPhase)
				{
					if (progress.TotalBytesToReceive < 0L && progress.BytesReceived == 0L)
					{
						num = ((progress.TotalBytesToSend < 0L) ? 0 : ((progress.TotalBytesToSend == 0L) ? 50 : ((int)(50L * progress.BytesSent / progress.TotalBytesToSend))));
					}
					else
					{
						num = ((progress.TotalBytesToSend < 0L) ? 50 : ((progress.TotalBytesToReceive == 0L) ? 100 : ((int)(50L * progress.BytesReceived / progress.TotalBytesToReceive + 50L))));
					}
					asyncOp.Post(this.reportUploadProgressChanged, new UploadProgressChangedEventArgs(num, asyncOp.UserSuppliedState, progress.BytesSent, progress.TotalBytesToSend, progress.BytesReceived, progress.TotalBytesToReceive));
					return;
				}
				num = ((progress.TotalBytesToReceive < 0L) ? 0 : ((progress.TotalBytesToReceive == 0L) ? 100 : ((int)(100L * progress.BytesReceived / progress.TotalBytesToReceive))));
				asyncOp.Post(this.reportDownloadProgressChanged, new DownloadProgressChangedEventArgs(num, asyncOp.UserSuppliedState, progress.BytesReceived, progress.TotalBytesToReceive));
			}
		}

		// Token: 0x0400202A RID: 8234
		private const int DefaultCopyBufferLength = 8192;

		// Token: 0x0400202B RID: 8235
		private const int DefaultDownloadBufferLength = 65536;

		// Token: 0x0400202C RID: 8236
		private const string DefaultUploadFileContentType = "application/octet-stream";

		// Token: 0x0400202D RID: 8237
		private const string UploadFileContentType = "multipart/form-data";

		// Token: 0x0400202E RID: 8238
		private const string UploadValuesContentType = "application/x-www-form-urlencoded";

		// Token: 0x0400202F RID: 8239
		private Uri m_baseAddress;

		// Token: 0x04002030 RID: 8240
		private ICredentials m_credentials;

		// Token: 0x04002031 RID: 8241
		private WebHeaderCollection m_headers;

		// Token: 0x04002032 RID: 8242
		private NameValueCollection m_requestParameters;

		// Token: 0x04002033 RID: 8243
		private WebResponse m_WebResponse;

		// Token: 0x04002034 RID: 8244
		private WebRequest m_WebRequest;

		// Token: 0x04002035 RID: 8245
		private Encoding m_Encoding = Encoding.Default;

		// Token: 0x04002036 RID: 8246
		private string m_Method;

		// Token: 0x04002037 RID: 8247
		private long m_ContentLength = -1L;

		// Token: 0x04002038 RID: 8248
		private bool m_InitWebClientAsync;

		// Token: 0x04002039 RID: 8249
		private bool m_Cancelled;

		// Token: 0x0400203A RID: 8250
		private WebClient.ProgressData m_Progress;

		// Token: 0x0400203B RID: 8251
		private IWebProxy m_Proxy;

		// Token: 0x0400203C RID: 8252
		private bool m_ProxySet;

		// Token: 0x0400203D RID: 8253
		private RequestCachePolicy m_CachePolicy;

		// Token: 0x04002040 RID: 8256
		private int m_CallNesting;

		// Token: 0x04002041 RID: 8257
		private AsyncOperation m_AsyncOp;

		// Token: 0x04002043 RID: 8259
		private SendOrPostCallback openReadOperationCompleted;

		// Token: 0x04002045 RID: 8261
		private SendOrPostCallback openWriteOperationCompleted;

		// Token: 0x04002047 RID: 8263
		private SendOrPostCallback downloadStringOperationCompleted;

		// Token: 0x04002049 RID: 8265
		private SendOrPostCallback downloadDataOperationCompleted;

		// Token: 0x0400204B RID: 8267
		private SendOrPostCallback downloadFileOperationCompleted;

		// Token: 0x0400204D RID: 8269
		private SendOrPostCallback uploadStringOperationCompleted;

		// Token: 0x0400204F RID: 8271
		private SendOrPostCallback uploadDataOperationCompleted;

		// Token: 0x04002051 RID: 8273
		private SendOrPostCallback uploadFileOperationCompleted;

		// Token: 0x04002053 RID: 8275
		private SendOrPostCallback uploadValuesOperationCompleted;

		// Token: 0x04002056 RID: 8278
		private SendOrPostCallback reportDownloadProgressChanged;

		// Token: 0x04002057 RID: 8279
		private SendOrPostCallback reportUploadProgressChanged;

		// Token: 0x020004CD RID: 1229
		private class ProgressData
		{
			// Token: 0x0600253D RID: 9533 RVA: 0x000919C8 File Offset: 0x0008FBC8
			internal void Reset()
			{
				this.BytesSent = 0L;
				this.TotalBytesToSend = -1L;
				this.BytesReceived = 0L;
				this.TotalBytesToReceive = -1L;
				this.HasUploadPhase = false;
			}

			// Token: 0x04002058 RID: 8280
			internal long BytesSent;

			// Token: 0x04002059 RID: 8281
			internal long TotalBytesToSend = -1L;

			// Token: 0x0400205A RID: 8282
			internal long BytesReceived;

			// Token: 0x0400205B RID: 8283
			internal long TotalBytesToReceive = -1L;

			// Token: 0x0400205C RID: 8284
			internal bool HasUploadPhase;
		}

		// Token: 0x020004CE RID: 1230
		private class DownloadBitsState
		{
			// Token: 0x0600253F RID: 9535 RVA: 0x00091A09 File Offset: 0x0008FC09
			internal DownloadBitsState(WebRequest request, Stream writeStream, CompletionDelegate completionDelegate, AsyncOperation asyncOp, WebClient.ProgressData progress, WebClient webClient)
			{
				this.WriteStream = writeStream;
				this.Request = request;
				this.AsyncOp = asyncOp;
				this.CompletionDelegate = completionDelegate;
				this.WebClient = webClient;
				this.Progress = progress;
			}

			// Token: 0x17000795 RID: 1941
			// (get) Token: 0x06002540 RID: 9536 RVA: 0x00091A3E File Offset: 0x0008FC3E
			internal bool Async
			{
				get
				{
					return this.AsyncOp != null;
				}
			}

			// Token: 0x06002541 RID: 9537 RVA: 0x00091A4C File Offset: 0x0008FC4C
			internal int SetResponse(WebResponse response)
			{
				this.ContentLength = response.ContentLength;
				if (this.ContentLength == -1L || this.ContentLength > 65536L)
				{
					this.Length = 65536L;
				}
				else
				{
					this.Length = this.ContentLength;
				}
				if (this.WriteStream == null)
				{
					if (this.ContentLength > 2147483647L)
					{
						throw new WebException(global::SR.GetString("The message length limit was exceeded"), WebExceptionStatus.MessageLengthLimitExceeded);
					}
					this.SgBuffers = new ScatterGatherBuffers(this.Length);
				}
				this.InnerBuffer = new byte[(int)this.Length];
				this.ReadStream = response.GetResponseStream();
				if (this.Async && response.ContentLength >= 0L)
				{
					this.Progress.TotalBytesToReceive = response.ContentLength;
				}
				if (this.Async)
				{
					if (this.ReadStream == null || this.ReadStream == Stream.Null)
					{
						WebClient.DownloadBitsReadCallbackState(this, null);
					}
					else
					{
						this.ReadStream.BeginRead(this.InnerBuffer, 0, (int)this.Length, new AsyncCallback(WebClient.DownloadBitsReadCallback), this);
					}
					return -1;
				}
				if (this.ReadStream == null || this.ReadStream == Stream.Null)
				{
					return 0;
				}
				return this.ReadStream.Read(this.InnerBuffer, 0, (int)this.Length);
			}

			// Token: 0x06002542 RID: 9538 RVA: 0x00091B94 File Offset: 0x0008FD94
			internal bool RetrieveBytes(ref int bytesRetrieved)
			{
				if (bytesRetrieved > 0)
				{
					if (this.WriteStream != null)
					{
						this.WriteStream.Write(this.InnerBuffer, 0, bytesRetrieved);
					}
					else
					{
						this.SgBuffers.Write(this.InnerBuffer, 0, bytesRetrieved);
					}
					if (this.Async)
					{
						this.Progress.BytesReceived += (long)bytesRetrieved;
					}
					if (this.ContentLength != 0L)
					{
						if (this.Async)
						{
							this.WebClient.PostProgressChanged(this.AsyncOp, this.Progress);
							this.ReadStream.BeginRead(this.InnerBuffer, 0, (int)this.Length, new AsyncCallback(WebClient.DownloadBitsReadCallback), this);
						}
						else
						{
							bytesRetrieved = this.ReadStream.Read(this.InnerBuffer, 0, (int)this.Length);
						}
						return false;
					}
				}
				if (this.Async)
				{
					if (this.Progress.TotalBytesToReceive < 0L)
					{
						this.Progress.TotalBytesToReceive = this.Progress.BytesReceived;
					}
					this.WebClient.PostProgressChanged(this.AsyncOp, this.Progress);
				}
				if (this.ReadStream != null)
				{
					this.ReadStream.Close();
				}
				if (this.WriteStream != null)
				{
					this.WriteStream.Close();
				}
				else if (this.WriteStream == null)
				{
					byte[] array = new byte[this.SgBuffers.Length];
					if (this.SgBuffers.Length > 0)
					{
						BufferOffsetSize[] buffers = this.SgBuffers.GetBuffers();
						int num = 0;
						foreach (BufferOffsetSize bufferOffsetSize in buffers)
						{
							Buffer.BlockCopy(bufferOffsetSize.Buffer, 0, array, num, bufferOffsetSize.Size);
							num += bufferOffsetSize.Size;
						}
					}
					this.InnerBuffer = array;
				}
				return true;
			}

			// Token: 0x06002543 RID: 9539 RVA: 0x00091D41 File Offset: 0x0008FF41
			internal void Close()
			{
				if (this.WriteStream != null)
				{
					this.WriteStream.Close();
				}
				if (this.ReadStream != null)
				{
					this.ReadStream.Close();
				}
			}

			// Token: 0x0400205D RID: 8285
			internal WebClient WebClient;

			// Token: 0x0400205E RID: 8286
			internal Stream WriteStream;

			// Token: 0x0400205F RID: 8287
			internal byte[] InnerBuffer;

			// Token: 0x04002060 RID: 8288
			internal AsyncOperation AsyncOp;

			// Token: 0x04002061 RID: 8289
			internal WebRequest Request;

			// Token: 0x04002062 RID: 8290
			internal CompletionDelegate CompletionDelegate;

			// Token: 0x04002063 RID: 8291
			internal Stream ReadStream;

			// Token: 0x04002064 RID: 8292
			internal ScatterGatherBuffers SgBuffers;

			// Token: 0x04002065 RID: 8293
			internal long ContentLength;

			// Token: 0x04002066 RID: 8294
			internal long Length;

			// Token: 0x04002067 RID: 8295
			private const int Offset = 0;

			// Token: 0x04002068 RID: 8296
			internal WebClient.ProgressData Progress;
		}

		// Token: 0x020004CF RID: 1231
		private class UploadBitsState
		{
			// Token: 0x06002544 RID: 9540 RVA: 0x00091D6C File Offset: 0x0008FF6C
			internal UploadBitsState(WebRequest request, Stream readStream, byte[] buffer, int chunkSize, byte[] header, byte[] footer, CompletionDelegate uploadCompletionDelegate, CompletionDelegate downloadCompletionDelegate, AsyncOperation asyncOp, WebClient.ProgressData progress, WebClient webClient)
			{
				this.InnerBuffer = buffer;
				this.m_ChunkSize = chunkSize;
				this.m_BufferWritePosition = 0;
				this.Header = header;
				this.Footer = footer;
				this.ReadStream = readStream;
				this.Request = request;
				this.AsyncOp = asyncOp;
				this.UploadCompletionDelegate = uploadCompletionDelegate;
				this.DownloadCompletionDelegate = downloadCompletionDelegate;
				if (this.AsyncOp != null)
				{
					this.Progress = progress;
					this.Progress.HasUploadPhase = true;
					this.Progress.TotalBytesToSend = ((request.ContentLength < 0L) ? (-1L) : request.ContentLength);
				}
				this.WebClient = webClient;
			}

			// Token: 0x17000796 RID: 1942
			// (get) Token: 0x06002545 RID: 9541 RVA: 0x00091E0E File Offset: 0x0009000E
			internal bool FileUpload
			{
				get
				{
					return this.ReadStream != null;
				}
			}

			// Token: 0x17000797 RID: 1943
			// (get) Token: 0x06002546 RID: 9542 RVA: 0x00091E19 File Offset: 0x00090019
			internal bool Async
			{
				get
				{
					return this.AsyncOp != null;
				}
			}

			// Token: 0x06002547 RID: 9543 RVA: 0x00091E24 File Offset: 0x00090024
			internal void SetRequestStream(Stream writeStream)
			{
				this.WriteStream = writeStream;
				byte[] array;
				if (this.Header != null)
				{
					array = this.Header;
					this.Header = null;
				}
				else
				{
					array = new byte[0];
				}
				if (this.Async)
				{
					this.Progress.BytesSent += (long)array.Length;
					this.WriteStream.BeginWrite(array, 0, array.Length, new AsyncCallback(WebClient.UploadBitsWriteCallback), this);
					return;
				}
				this.WriteStream.Write(array, 0, array.Length);
			}

			// Token: 0x06002548 RID: 9544 RVA: 0x00091EA8 File Offset: 0x000900A8
			internal bool WriteBytes()
			{
				int num = 0;
				if (this.Async)
				{
					this.WebClient.PostProgressChanged(this.AsyncOp, this.Progress);
				}
				int num3;
				byte[] array;
				if (this.FileUpload)
				{
					int num2 = 0;
					if (this.InnerBuffer != null)
					{
						num2 = this.ReadStream.Read(this.InnerBuffer, 0, this.InnerBuffer.Length);
						if (num2 <= 0)
						{
							this.ReadStream.Close();
							this.InnerBuffer = null;
						}
					}
					if (this.InnerBuffer != null)
					{
						num3 = num2;
						array = this.InnerBuffer;
					}
					else
					{
						if (this.Footer == null)
						{
							return true;
						}
						num3 = this.Footer.Length;
						array = this.Footer;
						this.Footer = null;
					}
				}
				else
				{
					if (this.InnerBuffer == null)
					{
						return true;
					}
					array = this.InnerBuffer;
					if (this.m_ChunkSize != 0)
					{
						num = this.m_BufferWritePosition;
						this.m_BufferWritePosition += this.m_ChunkSize;
						num3 = this.m_ChunkSize;
						if (this.m_BufferWritePosition >= this.InnerBuffer.Length)
						{
							num3 = this.InnerBuffer.Length - num;
							this.InnerBuffer = null;
						}
					}
					else
					{
						num3 = this.InnerBuffer.Length;
						this.InnerBuffer = null;
					}
				}
				if (this.Async)
				{
					this.Progress.BytesSent += (long)num3;
					this.WriteStream.BeginWrite(array, num, num3, new AsyncCallback(WebClient.UploadBitsWriteCallback), this);
				}
				else
				{
					this.WriteStream.Write(array, 0, num3);
				}
				return false;
			}

			// Token: 0x06002549 RID: 9545 RVA: 0x0009200F File Offset: 0x0009020F
			internal void Close()
			{
				if (this.WriteStream != null)
				{
					this.WriteStream.Close();
				}
				if (this.ReadStream != null)
				{
					this.ReadStream.Close();
				}
			}

			// Token: 0x04002069 RID: 8297
			private int m_ChunkSize;

			// Token: 0x0400206A RID: 8298
			private int m_BufferWritePosition;

			// Token: 0x0400206B RID: 8299
			internal WebClient WebClient;

			// Token: 0x0400206C RID: 8300
			internal Stream WriteStream;

			// Token: 0x0400206D RID: 8301
			internal byte[] InnerBuffer;

			// Token: 0x0400206E RID: 8302
			internal byte[] Header;

			// Token: 0x0400206F RID: 8303
			internal byte[] Footer;

			// Token: 0x04002070 RID: 8304
			internal AsyncOperation AsyncOp;

			// Token: 0x04002071 RID: 8305
			internal WebRequest Request;

			// Token: 0x04002072 RID: 8306
			internal CompletionDelegate UploadCompletionDelegate;

			// Token: 0x04002073 RID: 8307
			internal CompletionDelegate DownloadCompletionDelegate;

			// Token: 0x04002074 RID: 8308
			internal Stream ReadStream;

			// Token: 0x04002075 RID: 8309
			internal WebClient.ProgressData Progress;
		}

		// Token: 0x020004D0 RID: 1232
		private class WebClientWriteStream : Stream
		{
			// Token: 0x0600254A RID: 9546 RVA: 0x00092037 File Offset: 0x00090237
			public WebClientWriteStream(Stream stream, WebRequest request, WebClient webClient)
			{
				this.m_request = request;
				this.m_stream = stream;
				this.m_WebClient = webClient;
			}

			// Token: 0x17000798 RID: 1944
			// (get) Token: 0x0600254B RID: 9547 RVA: 0x00092054 File Offset: 0x00090254
			public override bool CanRead
			{
				get
				{
					return this.m_stream.CanRead;
				}
			}

			// Token: 0x17000799 RID: 1945
			// (get) Token: 0x0600254C RID: 9548 RVA: 0x00092061 File Offset: 0x00090261
			public override bool CanSeek
			{
				get
				{
					return this.m_stream.CanSeek;
				}
			}

			// Token: 0x1700079A RID: 1946
			// (get) Token: 0x0600254D RID: 9549 RVA: 0x0009206E File Offset: 0x0009026E
			public override bool CanWrite
			{
				get
				{
					return this.m_stream.CanWrite;
				}
			}

			// Token: 0x1700079B RID: 1947
			// (get) Token: 0x0600254E RID: 9550 RVA: 0x0009207B File Offset: 0x0009027B
			public override bool CanTimeout
			{
				get
				{
					return this.m_stream.CanTimeout;
				}
			}

			// Token: 0x1700079C RID: 1948
			// (get) Token: 0x0600254F RID: 9551 RVA: 0x00092088 File Offset: 0x00090288
			// (set) Token: 0x06002550 RID: 9552 RVA: 0x00092095 File Offset: 0x00090295
			public override int ReadTimeout
			{
				get
				{
					return this.m_stream.ReadTimeout;
				}
				set
				{
					this.m_stream.ReadTimeout = value;
				}
			}

			// Token: 0x1700079D RID: 1949
			// (get) Token: 0x06002551 RID: 9553 RVA: 0x000920A3 File Offset: 0x000902A3
			// (set) Token: 0x06002552 RID: 9554 RVA: 0x000920B0 File Offset: 0x000902B0
			public override int WriteTimeout
			{
				get
				{
					return this.m_stream.WriteTimeout;
				}
				set
				{
					this.m_stream.WriteTimeout = value;
				}
			}

			// Token: 0x1700079E RID: 1950
			// (get) Token: 0x06002553 RID: 9555 RVA: 0x000920BE File Offset: 0x000902BE
			public override long Length
			{
				get
				{
					return this.m_stream.Length;
				}
			}

			// Token: 0x1700079F RID: 1951
			// (get) Token: 0x06002554 RID: 9556 RVA: 0x000920CB File Offset: 0x000902CB
			// (set) Token: 0x06002555 RID: 9557 RVA: 0x000920D8 File Offset: 0x000902D8
			public override long Position
			{
				get
				{
					return this.m_stream.Position;
				}
				set
				{
					this.m_stream.Position = value;
				}
			}

			// Token: 0x06002556 RID: 9558 RVA: 0x000920E6 File Offset: 0x000902E6
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
			{
				return this.m_stream.BeginRead(buffer, offset, size, callback, state);
			}

			// Token: 0x06002557 RID: 9559 RVA: 0x000920FA File Offset: 0x000902FA
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
			{
				return this.m_stream.BeginWrite(buffer, offset, size, callback, state);
			}

			// Token: 0x06002558 RID: 9560 RVA: 0x00092110 File Offset: 0x00090310
			protected override void Dispose(bool disposing)
			{
				try
				{
					if (disposing)
					{
						this.m_stream.Close();
						this.m_WebClient.GetWebResponse(this.m_request).Close();
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			// Token: 0x06002559 RID: 9561 RVA: 0x0009215C File Offset: 0x0009035C
			public override int EndRead(IAsyncResult result)
			{
				return this.m_stream.EndRead(result);
			}

			// Token: 0x0600255A RID: 9562 RVA: 0x0009216A File Offset: 0x0009036A
			public override void EndWrite(IAsyncResult result)
			{
				this.m_stream.EndWrite(result);
			}

			// Token: 0x0600255B RID: 9563 RVA: 0x00092178 File Offset: 0x00090378
			public override void Flush()
			{
				this.m_stream.Flush();
			}

			// Token: 0x0600255C RID: 9564 RVA: 0x00092185 File Offset: 0x00090385
			public override int Read(byte[] buffer, int offset, int count)
			{
				return this.m_stream.Read(buffer, offset, count);
			}

			// Token: 0x0600255D RID: 9565 RVA: 0x00092195 File Offset: 0x00090395
			public override long Seek(long offset, SeekOrigin origin)
			{
				return this.m_stream.Seek(offset, origin);
			}

			// Token: 0x0600255E RID: 9566 RVA: 0x000921A4 File Offset: 0x000903A4
			public override void SetLength(long value)
			{
				this.m_stream.SetLength(value);
			}

			// Token: 0x0600255F RID: 9567 RVA: 0x000921B2 File Offset: 0x000903B2
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.m_stream.Write(buffer, offset, count);
			}

			// Token: 0x04002076 RID: 8310
			private WebRequest m_request;

			// Token: 0x04002077 RID: 8311
			private Stream m_stream;

			// Token: 0x04002078 RID: 8312
			private WebClient m_WebClient;
		}
	}
}
