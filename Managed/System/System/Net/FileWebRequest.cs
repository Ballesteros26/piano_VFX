using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Net
{
	/// <summary>Provides a file system implementation of the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x020004BE RID: 1214
	[Serializable]
	public class FileWebRequest : WebRequest, ISerializable
	{
		// Token: 0x060023DE RID: 9182 RVA: 0x0008C07C File Offset: 0x0008A27C
		internal FileWebRequest(Uri uri)
		{
			if (uri.Scheme != Uri.UriSchemeFile)
			{
				throw new ArgumentOutOfRangeException("uri");
			}
			this.m_uri = uri;
			this.m_fileAccess = FileAccess.Read;
			this.m_headers = new WebHeaderCollection(WebHeaderCollectionType.FileWebRequest);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.FileWebRequest" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the new <see cref="T:System.Net.FileWebRequest" /> object. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.FileWebRequest" /> object. </param>
		// Token: 0x060023DF RID: 9183 RVA: 0x0008C0D8 File Offset: 0x0008A2D8
		[Obsolete("Serialization is obsoleted for this type. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected FileWebRequest(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
			this.m_headers = (WebHeaderCollection)serializationInfo.GetValue("headers", typeof(WebHeaderCollection));
			this.m_proxy = (IWebProxy)serializationInfo.GetValue("proxy", typeof(IWebProxy));
			this.m_uri = (Uri)serializationInfo.GetValue("uri", typeof(Uri));
			this.m_connectionGroupName = serializationInfo.GetString("connectionGroupName");
			this.m_method = serializationInfo.GetString("method");
			this.m_contentLength = serializationInfo.GetInt64("contentLength");
			this.m_timeout = serializationInfo.GetInt32("timeout");
			this.m_fileAccess = (FileAccess)serializationInfo.GetInt32("fileAccess");
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the required data to serialize the <see cref="T:System.Net.FileWebRequest" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized data for the <see cref="T:System.Net.FileWebRequest" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream that is associated with the new <see cref="T:System.Net.FileWebRequest" />. </param>
		// Token: 0x060023E0 RID: 9184 RVA: 0x00083ECE File Offset: 0x000820CE
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" />  that specifies the destination for this serialization. </param>
		// Token: 0x060023E1 RID: 9185 RVA: 0x0008C1B8 File Offset: 0x0008A3B8
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("headers", this.m_headers, typeof(WebHeaderCollection));
			serializationInfo.AddValue("proxy", this.m_proxy, typeof(IWebProxy));
			serializationInfo.AddValue("uri", this.m_uri, typeof(Uri));
			serializationInfo.AddValue("connectionGroupName", this.m_connectionGroupName);
			serializationInfo.AddValue("method", this.m_method);
			serializationInfo.AddValue("contentLength", this.m_contentLength);
			serializationInfo.AddValue("timeout", this.m_timeout);
			serializationInfo.AddValue("fileAccess", this.m_fileAccess);
			serializationInfo.AddValue("preauthenticate", false);
			base.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x060023E2 RID: 9186 RVA: 0x0008C284 File Offset: 0x0008A484
		internal bool Aborted
		{
			get
			{
				return this.m_Aborted != 0;
			}
		}

		/// <summary>Gets or sets the name of the connection group for the request. This property is reserved for future use.</summary>
		/// <returns>The name of the connection group for the request.</returns>
		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060023E3 RID: 9187 RVA: 0x0008C28F File Offset: 0x0008A48F
		// (set) Token: 0x060023E4 RID: 9188 RVA: 0x0008C297 File Offset: 0x0008A497
		public override string ConnectionGroupName
		{
			get
			{
				return this.m_connectionGroupName;
			}
			set
			{
				this.m_connectionGroupName = value;
			}
		}

		/// <summary>Gets or sets the content length of the data being sent.</summary>
		/// <returns>The number of bytes of request data being sent.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.FileWebRequest.ContentLength" /> is less than 0. </exception>
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x0008C2A0 File Offset: 0x0008A4A0
		// (set) Token: 0x060023E6 RID: 9190 RVA: 0x0008C2A8 File Offset: 0x0008A4A8
		public override long ContentLength
		{
			get
			{
				return this.m_contentLength;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentException(global::SR.GetString("The Content-Length value must be greater than or equal to zero."), "value");
				}
				this.m_contentLength = value;
			}
		}

		/// <summary>Gets or sets the content type of the data being sent. This property is reserved for future use.</summary>
		/// <returns>The content type of the data being sent.</returns>
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060023E7 RID: 9191 RVA: 0x0008C2CB File Offset: 0x0008A4CB
		// (set) Token: 0x060023E8 RID: 9192 RVA: 0x0008C2DD File Offset: 0x0008A4DD
		public override string ContentType
		{
			get
			{
				return this.m_headers["Content-Type"];
			}
			set
			{
				this.m_headers["Content-Type"] = value;
			}
		}

		/// <summary>Gets or sets the credentials that are associated with this request. This property is reserved for future use.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> that contains the authentication credentials that are associated with this request. The default is null.</returns>
		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x0008C2F0 File Offset: 0x0008A4F0
		// (set) Token: 0x060023EA RID: 9194 RVA: 0x0008C2F8 File Offset: 0x0008A4F8
		public override ICredentials Credentials
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

		/// <summary>Gets a collection of the name/value pairs that are associated with the request. This property is reserved for future use.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains header name/value pairs associated with this request.</returns>
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060023EB RID: 9195 RVA: 0x0008C301 File Offset: 0x0008A501
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.m_headers;
			}
		}

		/// <summary>Gets or sets the protocol method used for the request. This property is reserved for future use.</summary>
		/// <returns>The protocol method to use in this request.</returns>
		/// <exception cref="T:System.ArgumentException">The method is invalid.- or -The method is not supported.- or -Multiple methods were specified.</exception>
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060023EC RID: 9196 RVA: 0x0008C309 File Offset: 0x0008A509
		// (set) Token: 0x060023ED RID: 9197 RVA: 0x0008C311 File Offset: 0x0008A511
		public override string Method
		{
			get
			{
				return this.m_method;
			}
			set
			{
				if (ValidationHelper.IsBlankString(value))
				{
					throw new ArgumentException(global::SR.GetString("Cannot set null or blank methods on request."), "value");
				}
				this.m_method = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to preauthenticate a request. This property is reserved for future use.</summary>
		/// <returns>true to preauthenticate; otherwise, false.</returns>
		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x0008C337 File Offset: 0x0008A537
		// (set) Token: 0x060023EF RID: 9199 RVA: 0x0008C33F File Offset: 0x0008A53F
		public override bool PreAuthenticate
		{
			get
			{
				return this.m_preauthenticate;
			}
			set
			{
				this.m_preauthenticate = true;
			}
		}

		/// <summary>Gets or sets the network proxy to use for this request. This property is reserved for future use.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> that indicates the network proxy to use for this request.</returns>
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x0008C348 File Offset: 0x0008A548
		// (set) Token: 0x060023F1 RID: 9201 RVA: 0x0008C350 File Offset: 0x0008A550
		public override IWebProxy Proxy
		{
			get
			{
				return this.m_proxy;
			}
			set
			{
				this.m_proxy = value;
			}
		}

		/// <summary>Gets or sets the length of time until the request times out.</summary>
		/// <returns>The time, in milliseconds, until the request times out, or the value <see cref="F:System.Threading.Timeout.Infinite" /> to indicate that the request does not time out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than or equal to zero and is not <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x0008C359 File Offset: 0x0008A559
		// (set) Token: 0x060023F3 RID: 9203 RVA: 0x0008C361 File Offset: 0x0008A561
		public override int Timeout
		{
			get
			{
				return this.m_timeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", global::SR.GetString("Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value >= 0."));
				}
				this.m_timeout = value;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI of the request.</returns>
		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060023F4 RID: 9204 RVA: 0x0008C387 File Offset: 0x0008A587
		public override Uri RequestUri
		{
			get
			{
				return this.m_uri;
			}
		}

		/// <summary>Begins an asynchronous request for a <see cref="T:System.IO.Stream" /> object to use to write data.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.FileWebRequest.Method" /> property is GET and the application writes to the stream. </exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is being used by a previous call to <see cref="M:System.Net.FileWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.ApplicationException">No write stream is available. </exception>
		/// <exception cref="T:System.Net.WebException">The <see cref="T:System.Net.FileWebRequest" /> was aborted. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060023F5 RID: 9205 RVA: 0x0008C390 File Offset: 0x0008A590
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			try
			{
				if (this.Aborted)
				{
					throw ExceptionHelper.RequestAbortedException;
				}
				if (!this.CanGetRequestStream())
				{
					throw new ProtocolViolationException(global::SR.GetString("Cannot send a content-body with this verb-type."));
				}
				if (this.m_response != null)
				{
					throw new InvalidOperationException(global::SR.GetString("This operation cannot be performed after the request has been submitted."));
				}
				lock (this)
				{
					if (this.m_writePending)
					{
						throw new InvalidOperationException(global::SR.GetString("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress."));
					}
					this.m_writePending = true;
				}
				this.m_ReadAResult = new LazyAsyncResult(this, state, callback);
				ThreadPool.QueueUserWorkItem(FileWebRequest.s_GetRequestStreamCallback, this.m_ReadAResult);
			}
			catch (Exception)
			{
				bool on = Logging.On;
				throw;
			}
			finally
			{
			}
			return this.m_ReadAResult;
		}

		/// <summary>Begins an asynchronous request for a file system resource.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.InvalidOperationException">The stream is already in use by a previous call to <see cref="M:System.Net.FileWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.Net.WebException">The <see cref="T:System.Net.FileWebRequest" /> was aborted. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060023F6 RID: 9206 RVA: 0x0008C46C File Offset: 0x0008A66C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			try
			{
				if (this.Aborted)
				{
					throw ExceptionHelper.RequestAbortedException;
				}
				lock (this)
				{
					if (this.m_readPending)
					{
						throw new InvalidOperationException(global::SR.GetString("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress."));
					}
					this.m_readPending = true;
				}
				this.m_WriteAResult = new LazyAsyncResult(this, state, callback);
				ThreadPool.QueueUserWorkItem(FileWebRequest.s_GetResponseCallback, this.m_WriteAResult);
			}
			catch (Exception)
			{
				bool on = Logging.On;
				throw;
			}
			finally
			{
			}
			return this.m_WriteAResult;
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x0008C518 File Offset: 0x0008A718
		private bool CanGetRequestStream()
		{
			return !KnownHttpVerb.Parse(this.m_method).ContentBodyNotAllowed;
		}

		/// <summary>Ends an asynchronous request for a <see cref="T:System.IO.Stream" /> instance that the application uses to write data.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object that the application uses to write data.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that references the pending request for a stream. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		// Token: 0x060023F8 RID: 9208 RVA: 0x0008C530 File Offset: 0x0008A730
		public override Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			Stream stream;
			try
			{
				LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
				if (asyncResult == null || lazyAsyncResult == null)
				{
					throw (asyncResult == null) ? new ArgumentNullException("asyncResult") : new ArgumentException(global::SR.GetString("The AsyncResult is not valid."), "asyncResult");
				}
				object obj = lazyAsyncResult.InternalWaitForCompletion();
				if (obj is Exception)
				{
					throw (Exception)obj;
				}
				stream = (Stream)obj;
				this.m_writePending = false;
			}
			catch (Exception)
			{
				bool on = Logging.On;
				throw;
			}
			finally
			{
			}
			return stream;
		}

		/// <summary>Ends an asynchronous request for a file system resource.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains the response from the file system resource.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that references the pending request for a response. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		// Token: 0x060023F9 RID: 9209 RVA: 0x0008C5BC File Offset: 0x0008A7BC
		public override WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			WebResponse webResponse;
			try
			{
				LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
				if (asyncResult == null || lazyAsyncResult == null)
				{
					throw (asyncResult == null) ? new ArgumentNullException("asyncResult") : new ArgumentException(global::SR.GetString("The AsyncResult is not valid."), "asyncResult");
				}
				object obj = lazyAsyncResult.InternalWaitForCompletion();
				if (obj is Exception)
				{
					throw (Exception)obj;
				}
				webResponse = (WebResponse)obj;
				this.m_readPending = false;
			}
			catch (Exception)
			{
				bool on = Logging.On;
				throw;
			}
			finally
			{
			}
			return webResponse;
		}

		/// <summary>Returns a <see cref="T:System.IO.Stream" /> object for writing data to the file system resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> for writing data to the file system resource.</returns>
		/// <exception cref="T:System.Net.WebException">The request times out. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060023FA RID: 9210 RVA: 0x0008C648 File Offset: 0x0008A848
		public override Stream GetRequestStream()
		{
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this.BeginGetRequestStream(null, null);
				if (this.Timeout != -1 && !asyncResult.IsCompleted && (!asyncResult.AsyncWaitHandle.WaitOne(this.Timeout, false) || !asyncResult.IsCompleted))
				{
					if (this.m_stream != null)
					{
						this.m_stream.Close();
					}
					throw new WebException(NetRes.GetWebStatusString(WebExceptionStatus.Timeout), WebExceptionStatus.Timeout);
				}
			}
			catch (Exception)
			{
				bool on = Logging.On;
				throw;
			}
			finally
			{
			}
			return this.EndGetRequestStream(asyncResult);
		}

		/// <summary>Returns a response to a file system request.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains the response from the file system resource.</returns>
		/// <exception cref="T:System.Net.WebException">The request timed out. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060023FB RID: 9211 RVA: 0x0008C6DC File Offset: 0x0008A8DC
		public override WebResponse GetResponse()
		{
			this.m_syncHint = true;
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this.BeginGetResponse(null, null);
				if (this.Timeout != -1 && !asyncResult.IsCompleted && (!asyncResult.AsyncWaitHandle.WaitOne(this.Timeout, false) || !asyncResult.IsCompleted))
				{
					if (this.m_response != null)
					{
						this.m_response.Close();
					}
					throw new WebException(NetRes.GetWebStatusString(WebExceptionStatus.Timeout), WebExceptionStatus.Timeout);
				}
			}
			catch (Exception)
			{
				bool on = Logging.On;
				throw;
			}
			finally
			{
			}
			return this.EndGetResponse(asyncResult);
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x0008C778 File Offset: 0x0008A978
		private static void GetRequestStreamCallback(object state)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)state;
			FileWebRequest fileWebRequest = (FileWebRequest)lazyAsyncResult.AsyncObject;
			try
			{
				if (fileWebRequest.m_stream == null)
				{
					fileWebRequest.m_stream = new FileWebStream(fileWebRequest, fileWebRequest.m_uri.LocalPath, FileMode.Create, FileAccess.Write, FileShare.Read);
					fileWebRequest.m_fileAccess = FileAccess.Write;
					fileWebRequest.m_writing = true;
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = new WebException(ex.Message, ex);
				lazyAsyncResult.InvokeCallback(ex2);
				return;
			}
			lazyAsyncResult.InvokeCallback(fileWebRequest.m_stream);
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x0008C800 File Offset: 0x0008AA00
		private static void GetResponseCallback(object state)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)state;
			FileWebRequest fileWebRequest = (FileWebRequest)lazyAsyncResult.AsyncObject;
			if (fileWebRequest.m_writePending || fileWebRequest.m_writing)
			{
				FileWebRequest fileWebRequest2 = fileWebRequest;
				lock (fileWebRequest2)
				{
					if (fileWebRequest.m_writePending || fileWebRequest.m_writing)
					{
						fileWebRequest.m_readerEvent = new ManualResetEvent(false);
					}
				}
			}
			if (fileWebRequest.m_readerEvent != null)
			{
				fileWebRequest.m_readerEvent.WaitOne();
			}
			try
			{
				if (fileWebRequest.m_response == null)
				{
					fileWebRequest.m_response = new FileWebResponse(fileWebRequest, fileWebRequest.m_uri, fileWebRequest.m_fileAccess, !fileWebRequest.m_syncHint);
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = new WebException(ex.Message, ex);
				lazyAsyncResult.InvokeCallback(ex2);
				return;
			}
			lazyAsyncResult.InvokeCallback(fileWebRequest.m_response);
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x0008C8E8 File Offset: 0x0008AAE8
		internal void UnblockReader()
		{
			lock (this)
			{
				if (this.m_readerEvent != null)
				{
					this.m_readerEvent.Set();
				}
			}
			this.m_writing = false;
		}

		/// <summary>Always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Default credentials are not supported for file Uniform Resource Identifiers (URIs).</exception>
		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060023FF RID: 9215 RVA: 0x0008C938 File Offset: 0x0008AB38
		// (set) Token: 0x06002400 RID: 9216 RVA: 0x0008C938 File Offset: 0x0008AB38
		public override bool UseDefaultCredentials
		{
			get
			{
				throw ExceptionHelper.PropertyNotSupportedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotSupportedException;
			}
		}

		/// <summary>Cancels a request to an Internet resource.</summary>
		// Token: 0x06002401 RID: 9217 RVA: 0x0008C940 File Offset: 0x0008AB40
		public override void Abort()
		{
			bool on = Logging.On;
			try
			{
				if (Interlocked.Increment(ref this.m_Aborted) == 1)
				{
					LazyAsyncResult readAResult = this.m_ReadAResult;
					LazyAsyncResult writeAResult = this.m_WriteAResult;
					WebException ex = new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
					Stream stream = this.m_stream;
					if (readAResult != null && !readAResult.IsCompleted)
					{
						readAResult.InvokeCallback(ex);
					}
					if (writeAResult != null && !writeAResult.IsCompleted)
					{
						writeAResult.InvokeCallback(ex);
					}
					if (stream != null)
					{
						if (stream is ICloseEx)
						{
							((ICloseEx)stream).CloseEx(CloseExState.Abort);
						}
						else
						{
							stream.Close();
						}
					}
					if (this.m_response != null)
					{
						((ICloseEx)this.m_response).CloseEx(CloseExState.Abort);
					}
				}
			}
			catch (Exception)
			{
				bool on2 = Logging.On;
				throw;
			}
			finally
			{
			}
		}

		// Token: 0x04001FED RID: 8173
		private static WaitCallback s_GetRequestStreamCallback = new WaitCallback(FileWebRequest.GetRequestStreamCallback);

		// Token: 0x04001FEE RID: 8174
		private static WaitCallback s_GetResponseCallback = new WaitCallback(FileWebRequest.GetResponseCallback);

		// Token: 0x04001FEF RID: 8175
		private string m_connectionGroupName;

		// Token: 0x04001FF0 RID: 8176
		private long m_contentLength;

		// Token: 0x04001FF1 RID: 8177
		private ICredentials m_credentials;

		// Token: 0x04001FF2 RID: 8178
		private FileAccess m_fileAccess;

		// Token: 0x04001FF3 RID: 8179
		private WebHeaderCollection m_headers;

		// Token: 0x04001FF4 RID: 8180
		private string m_method = "GET";

		// Token: 0x04001FF5 RID: 8181
		private bool m_preauthenticate;

		// Token: 0x04001FF6 RID: 8182
		private IWebProxy m_proxy;

		// Token: 0x04001FF7 RID: 8183
		private ManualResetEvent m_readerEvent;

		// Token: 0x04001FF8 RID: 8184
		private bool m_readPending;

		// Token: 0x04001FF9 RID: 8185
		private WebResponse m_response;

		// Token: 0x04001FFA RID: 8186
		private Stream m_stream;

		// Token: 0x04001FFB RID: 8187
		private bool m_syncHint;

		// Token: 0x04001FFC RID: 8188
		private int m_timeout = 100000;

		// Token: 0x04001FFD RID: 8189
		private Uri m_uri;

		// Token: 0x04001FFE RID: 8190
		private bool m_writePending;

		// Token: 0x04001FFF RID: 8191
		private bool m_writing;

		// Token: 0x04002000 RID: 8192
		private LazyAsyncResult m_WriteAResult;

		// Token: 0x04002001 RID: 8193
		private LazyAsyncResult m_ReadAResult;

		// Token: 0x04002002 RID: 8194
		private int m_Aborted;
	}
}
