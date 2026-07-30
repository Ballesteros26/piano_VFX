using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Services.Configuration;
using System.Web.Services.Description;
using System.Web.Services.Diagnostics;
using System.Web.Services.Discovery;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols
{
	/// <summary>Specifies the class client that proxies derive from when using SOAP.</summary>
	// Token: 0x0200005D RID: 93
	[ComVisible(true)]
	public class SoapHttpClientProtocol : HttpWebClientProtocol
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapHttpClientProtocol" /> class.</summary>
		// Token: 0x0600021B RID: 539 RVA: 0x0000A330 File Offset: 0x00008530
		public SoapHttpClientProtocol()
		{
			Type type = base.GetType();
			this.clientType = (SoapClientType)WebClientProtocol.GetFromCache(type);
			if (this.clientType == null)
			{
				object internalSyncObject = WebClientProtocol.InternalSyncObject;
				lock (internalSyncObject)
				{
					this.clientType = (SoapClientType)WebClientProtocol.GetFromCache(type);
					if (this.clientType == null)
					{
						this.clientType = new SoapClientType(type);
						WebClientProtocol.AddToCache(type, this.clientType);
					}
				}
			}
		}

		/// <summary>Dynamically binds to an XML Web service described in the discovery document at <see cref="P:System.Web.Services.Protocols.WebClientProtocol.Url" />.</summary>
		/// <exception cref="T:System.Exception">The binding defined in the proxy class could not be found in the discovery document at <see cref="P:System.Web.Services.Protocols.WebClientProtocol.Url" />. </exception>
		/// <exception cref="T:System.Exception">The proxy class does not have a binding defined. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600021C RID: 540 RVA: 0x0000A3C0 File Offset: 0x000085C0
		public void Discover()
		{
			if (this.clientType.Binding == null)
			{
				throw new InvalidOperationException(Res.GetString("DiscoveryIsNotPossibleBecauseTypeIsMissing1", new object[] { base.GetType().FullName }));
			}
			foreach (object obj in new DiscoveryClientProtocol(this).Discover(base.Url).References)
			{
				global::System.Web.Services.Discovery.SoapBinding soapBinding = obj as global::System.Web.Services.Discovery.SoapBinding;
				if (soapBinding != null && this.clientType.Binding.Name == soapBinding.Binding.Name && this.clientType.Binding.Namespace == soapBinding.Binding.Namespace)
				{
					base.Url = soapBinding.Address;
					return;
				}
			}
			throw new InvalidOperationException(Res.GetString("TheBindingNamedFromNamespaceWasNotFoundIn3", new object[]
			{
				this.clientType.Binding.Name,
				this.clientType.Binding.Namespace,
				base.Url
			}));
		}

		/// <summary>Creates a <see cref="T:System.Net.WebRequest" /> for the specified <paramref name="uri" />.</summary>
		/// <returns>The <see cref="T:System.Net.WebRequest" /> for the specified URI.</returns>
		/// <param name="uri">The <see cref="T:System.Uri" /> to use when creating the <see cref="T:System.Net.WebRequest" />. </param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="uri" /> parameter is null. </exception>
		// Token: 0x0600021D RID: 541 RVA: 0x0000A4F0 File Offset: 0x000086F0
		protected override WebRequest GetWebRequest(Uri uri)
		{
			return base.GetWebRequest(uri);
		}

		/// <summary>Gets or sets the version of the SOAP protocol used to make the SOAP request to the XML Web service.</summary>
		/// <returns>One of the <see cref="T:System.Web.Services.Protocols.SoapProtocolVersion" /> values. The default is <see cref="F:System.Web.Services.Protocols.SoapProtocolVersion.Soap11" />.</returns>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000A4F9 File Offset: 0x000086F9
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000A501 File Offset: 0x00008701
		[WebServicesDescription("ClientProtocolSoapVersion")]
		[DefaultValue(SoapProtocolVersion.Default)]
		[ComVisible(false)]
		public SoapProtocolVersion SoapVersion
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		/// <summary>Returns a <see cref="T:System.Xml.XmlWriter" /> initialized with the <see cref="P:System.Web.Services.Protocols.SoapMessage.Stream" /> property of the <see cref="T:System.Web.Services.Protocols.SoapClientMessage" /> parameter.</summary>
		/// <returns>A <see cref="T:System.Xml.XmlWriter" /> initialized with the <see cref="P:System.Web.Services.Protocols.SoapMessage.Stream" /> property of the <paramref name="message" /> parameter.</returns>
		/// <param name="message">A <see cref="T:System.Web.Services.Protocols.SoapClientMessage" /> that provides the <see cref="P:System.Web.Services.Protocols.SoapMessage.Stream" /> to initialize the <see cref="T:System.Xml.XmlWriter" />.</param>
		/// <param name="bufferSize">The initial buffer size of the <see cref="T:System.IO.StreamWriter" /> used by the <see cref="T:System.Xml.XmlWriter" />.</param>
		// Token: 0x06000220 RID: 544 RVA: 0x0000A50A File Offset: 0x0000870A
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		protected virtual XmlWriter GetWriterForMessage(SoapClientMessage message, int bufferSize)
		{
			if (bufferSize < 512)
			{
				bufferSize = 512;
			}
			return new XmlTextWriter(new StreamWriter(message.Stream, (base.RequestEncoding != null) ? base.RequestEncoding : new UTF8Encoding(false), bufferSize));
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlReader" /> initialized with the <see cref="P:System.Web.Services.Protocols.SoapMessage.Stream" /> property of the <see cref="T:System.Web.Services.Protocols.SoapClientMessage" /> parameter.</summary>
		/// <returns>A <see cref="T:System.Xml.XmlReader" /> initialized with the <see cref="P:System.Web.Services.Protocols.SoapMessage.Stream" /> property of the <paramref name="message" /> parameter.</returns>
		/// <param name="message">A <see cref="T:System.Web.Services.Protocols.SoapClientMessage" /> that provides the <see cref="P:System.Web.Services.Protocols.SoapMessage.Stream" /> to initialize the <see cref="T:System.Xml.XmlReader" />.</param>
		/// <param name="bufferSize">The initial buffer size of the <see cref="T:System.IO.StreamReader" /> used by the <see cref="T:System.Xml.XmlReader" />.</param>
		// Token: 0x06000221 RID: 545 RVA: 0x0000A544 File Offset: 0x00008744
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		protected virtual XmlReader GetReaderForMessage(SoapClientMessage message, int bufferSize)
		{
			Encoding encoding = ((message.SoapVersion == SoapProtocolVersion.Soap12) ? RequestResponseUtils.GetEncoding2(message.ContentType) : RequestResponseUtils.GetEncoding(message.ContentType));
			if (bufferSize < 512)
			{
				bufferSize = 512;
			}
			XmlTextReader xmlTextReader;
			if (encoding != null)
			{
				xmlTextReader = new XmlTextReader(new StreamReader(message.Stream, encoding, true, bufferSize));
			}
			else
			{
				xmlTextReader = new XmlTextReader(message.Stream);
			}
			xmlTextReader.DtdProcessing = DtdProcessing.Prohibit;
			xmlTextReader.Normalization = true;
			xmlTextReader.XmlResolver = null;
			return xmlTextReader;
		}

		/// <summary>Invokes an XML Web service method synchronously using SOAP.</summary>
		/// <returns>An array of objects that contains the return value and any reference or out parameters of the derived class method.</returns>
		/// <param name="methodName">The name of the XML Web service method. </param>
		/// <param name="parameters">An array of objects that contains the parameters to pass to the XML Web service. The order of the values in the array corresponds to the order of the parameters in the calling method of the derived class. </param>
		/// <exception cref="T:System.Web.Services.Protocols.SoapException">The request reached the server computer, but was not processed successfully. </exception>
		/// <exception cref="T:System.InvalidOperationException">The request was not valid for the object's current state.</exception>
		/// <exception cref="T:System.Net.WebException">An error occurred while accessing the network.</exception>
		// Token: 0x06000222 RID: 546 RVA: 0x0000A5C0 File Offset: 0x000087C0
		protected object[] Invoke(string methodName, object[] parameters)
		{
			WebRequest webRequest = null;
			object[] array;
			try
			{
				webRequest = this.GetWebRequest(base.Uri);
				base.NotifyClientCallOut(webRequest);
				base.PendingSyncRequest = webRequest;
				SoapClientMessage soapClientMessage = this.BeforeSerialize(webRequest, methodName, parameters);
				Stream requestStream = webRequest.GetRequestStream();
				try
				{
					soapClientMessage.SetStream(requestStream);
					this.Serialize(soapClientMessage);
				}
				finally
				{
					requestStream.Close();
				}
				WebResponse webResponse = this.GetWebResponse(webRequest);
				Stream stream = null;
				try
				{
					stream = webResponse.GetResponseStream();
					array = this.ReadResponse(soapClientMessage, webResponse, stream, false);
				}
				catch (XmlException ex)
				{
					throw new InvalidOperationException(Res.GetString("WebResponseBadXml"), ex);
				}
				finally
				{
					if (stream != null)
					{
						stream.Close();
					}
				}
			}
			finally
			{
				if (webRequest == base.PendingSyncRequest)
				{
					base.PendingSyncRequest = null;
				}
			}
			return array;
		}

		/// <summary>Starts an asynchronous invocation of an XML Web service method using SOAP.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that is passed to the <see cref="M:System.Web.Services.Protocols.SoapHttpClientProtocol.EndInvoke(System.IAsyncResult)" /> method to obtain the return values from the remote method call.</returns>
		/// <param name="methodName">The name of the XML Web service method in the derived class that is invoking the <see cref="M:System.Web.Services.Protocols.SoapHttpClientProtocol.BeginInvoke(System.String,System.Object[],System.AsyncCallback,System.Object)" /> method. </param>
		/// <param name="parameters">An array of objects containing the parameters to pass to the XML Web service. The order of the values in the array correspond to the order of the parameters in the calling method of the derived class. </param>
		/// <param name="callback">The delegate to call when the asynchronous invoke is complete. If <paramref name="callback" /> is null, the delegate is not called. </param>
		/// <param name="asyncState">Extra information supplied by the caller. </param>
		/// <exception cref="T:System.Web.Services.Protocols.SoapException">The request reached the server computer, but was not processed successfully. </exception>
		/// <exception cref="T:System.InvalidOperationException">The request was not valid for the object's current state.</exception>
		/// <exception cref="T:System.Net.WebException">An error occurred while accessing the network.</exception>
		// Token: 0x06000223 RID: 547 RVA: 0x0000A6A0 File Offset: 0x000088A0
		protected IAsyncResult BeginInvoke(string methodName, object[] parameters, AsyncCallback callback, object asyncState)
		{
			SoapHttpClientProtocol.InvokeAsyncState invokeAsyncState = new SoapHttpClientProtocol.InvokeAsyncState(methodName, parameters);
			WebClientAsyncResult webClientAsyncResult = new WebClientAsyncResult(this, invokeAsyncState, null, callback, asyncState);
			return base.BeginSend(base.Uri, webClientAsyncResult, true);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A6D0 File Offset: 0x000088D0
		internal override void InitializeAsyncRequest(WebRequest request, object internalAsyncState)
		{
			SoapHttpClientProtocol.InvokeAsyncState invokeAsyncState = (SoapHttpClientProtocol.InvokeAsyncState)internalAsyncState;
			invokeAsyncState.Message = this.BeforeSerialize(request, invokeAsyncState.MethodName, invokeAsyncState.Parameters);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A700 File Offset: 0x00008900
		internal override void AsyncBufferedSerialize(WebRequest request, Stream requestStream, object internalAsyncState)
		{
			SoapHttpClientProtocol.InvokeAsyncState invokeAsyncState = (SoapHttpClientProtocol.InvokeAsyncState)internalAsyncState;
			invokeAsyncState.Message.SetStream(requestStream);
			this.Serialize(invokeAsyncState.Message);
		}

		/// <summary>Ends an asynchronous invocation of an XML Web service method using SOAP.</summary>
		/// <returns>An array of objects that contains the return value and any by-reference or out parameters of the derived class method.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> returned from the <see cref="M:System.Web.Services.Protocols.SoapHttpClientProtocol.BeginInvoke(System.String,System.Object[],System.AsyncCallback,System.Object)" /> method. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> is not the return value from the <see cref="M:System.Web.Services.Protocols.SoapHttpClientProtocol.BeginInvoke(System.String,System.Object[],System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.Web.Services.Protocols.SoapException">The request reached the server computer, but was not processed successfully. </exception>
		/// <exception cref="T:System.InvalidOperationException">The request was not valid for the object's current state.</exception>
		/// <exception cref="T:System.Net.WebException">An error occurred while accessing the network.</exception>
		// Token: 0x06000226 RID: 550 RVA: 0x0000A72C File Offset: 0x0000892C
		protected object[] EndInvoke(IAsyncResult asyncResult)
		{
			object obj = null;
			Stream stream = null;
			object[] array;
			try
			{
				WebResponse webResponse = base.EndSend(asyncResult, ref obj, ref stream);
				SoapHttpClientProtocol.InvokeAsyncState invokeAsyncState = (SoapHttpClientProtocol.InvokeAsyncState)obj;
				array = this.ReadResponse(invokeAsyncState.Message, webResponse, stream, true);
			}
			catch (XmlException ex)
			{
				throw new InvalidOperationException(Res.GetString("WebResponseBadXml"), ex);
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
			return array;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000A7A0 File Offset: 0x000089A0
		private void InvokeAsyncCallback(IAsyncResult result)
		{
			object[] array = null;
			Exception ex = null;
			WebClientAsyncResult webClientAsyncResult = (WebClientAsyncResult)result;
			if (webClientAsyncResult.Request != null)
			{
				object obj = null;
				Stream stream = null;
				try
				{
					WebResponse webResponse = base.EndSend(webClientAsyncResult, ref obj, ref stream);
					SoapHttpClientProtocol.InvokeAsyncState invokeAsyncState = (SoapHttpClientProtocol.InvokeAsyncState)obj;
					array = this.ReadResponse(invokeAsyncState.Message, webResponse, stream, true);
				}
				catch (XmlException ex2)
				{
					if (Tracing.On)
					{
						Tracing.ExceptionCatch(TraceEventType.Warning, this, "InvokeAsyncCallback", ex2);
					}
					ex = new InvalidOperationException(Res.GetString("WebResponseBadXml"), ex2);
				}
				catch (Exception ex3)
				{
					if (ex3 is ThreadAbortException || ex3 is StackOverflowException || ex3 is OutOfMemoryException)
					{
						throw;
					}
					if (Tracing.On)
					{
						Tracing.ExceptionCatch(TraceEventType.Warning, this, "InvokeAsyncCallback", ex3);
					}
					ex = ex3;
				}
				finally
				{
					if (stream != null)
					{
						stream.Close();
					}
				}
			}
			UserToken userToken = (UserToken)((AsyncOperation)result.AsyncState).UserSuppliedState;
			base.OperationCompleted(userToken.UserState, array, ex, false);
		}

		/// <summary>Invokes the specified method asynchronously.</summary>
		/// <param name="methodName">The name of the method to invoke.</param>
		/// <param name="parameters">The parameters to pass to the method.</param>
		/// <param name="callback">The delegate called when the method invocation has completed.</param>
		// Token: 0x06000228 RID: 552 RVA: 0x0000A8B4 File Offset: 0x00008AB4
		protected void InvokeAsync(string methodName, object[] parameters, SendOrPostCallback callback)
		{
			this.InvokeAsync(methodName, parameters, callback, null);
		}

		/// <summary>Invokes the specified method asynchronously.</summary>
		/// <param name="methodName">The name of the method to invoke.</param>
		/// <param name="parameters">The parameters to pass to the method.</param>
		/// <param name="callback">The delegate called when the method invocation has completed.</param>
		/// <param name="userState">An object used to pass state information into the <paramref name="callback" /> delegate.</param>
		// Token: 0x06000229 RID: 553 RVA: 0x0000A8C0 File Offset: 0x00008AC0
		protected void InvokeAsync(string methodName, object[] parameters, SendOrPostCallback callback, object userState)
		{
			if (userState == null)
			{
				userState = base.NullToken;
			}
			SoapHttpClientProtocol.InvokeAsyncState invokeAsyncState = new SoapHttpClientProtocol.InvokeAsyncState(methodName, parameters);
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(new UserToken(callback, userState));
			WebClientAsyncResult webClientAsyncResult = new WebClientAsyncResult(this, invokeAsyncState, null, new AsyncCallback(this.InvokeAsyncCallback), asyncOperation);
			try
			{
				base.AsyncInvokes.Add(userState, webClientAsyncResult);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Warning, this, "InvokeAsync", ex);
				}
				Exception ex2 = new ArgumentException(Res.GetString("AsyncDuplicateUserState"), ex);
				InvokeCompletedEventArgs invokeCompletedEventArgs = new InvokeCompletedEventArgs(new object[1], ex2, false, userState);
				asyncOperation.PostOperationCompleted(callback, invokeCompletedEventArgs);
				return;
			}
			try
			{
				base.BeginSend(base.Uri, webClientAsyncResult, true);
			}
			catch (Exception ex3)
			{
				if (ex3 is ThreadAbortException || ex3 is StackOverflowException || ex3 is OutOfMemoryException)
				{
					throw;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Warning, this, "InvokeAsync", ex3);
				}
				base.OperationCompleted(userState, new object[1], ex3, false);
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000A9EC File Offset: 0x00008BEC
		private static Array CombineExtensionsHelper(Array array1, Array array2, Array array3, Type elementType)
		{
			int num = array1.Length + array2.Length + array3.Length;
			if (num == 0)
			{
				return null;
			}
			Array array4;
			if (elementType == typeof(SoapReflectedExtension))
			{
				array4 = new SoapReflectedExtension[num];
			}
			else
			{
				if (!(elementType == typeof(object)))
				{
					throw new ArgumentException(Res.GetString("ElementTypeMustBeObjectOrSoapReflectedException"), "elementType");
				}
				array4 = new object[num];
			}
			int num2 = 0;
			Array.Copy(array1, 0, array4, num2, array1.Length);
			num2 += array1.Length;
			Array.Copy(array2, 0, array4, num2, array2.Length);
			num2 += array2.Length;
			Array.Copy(array3, 0, array4, num2, array3.Length);
			return array4;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000AAA2 File Offset: 0x00008CA2
		private string EnvelopeNs
		{
			get
			{
				if (this.version != SoapProtocolVersion.Soap12)
				{
					return "http://schemas.xmlsoap.org/soap/envelope/";
				}
				return "http://www.w3.org/2003/05/soap-envelope";
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000AAB8 File Offset: 0x00008CB8
		private string EncodingNs
		{
			get
			{
				if (this.version != SoapProtocolVersion.Soap12)
				{
					return "http://schemas.xmlsoap.org/soap/encoding/";
				}
				return "http://www.w3.org/2003/05/soap-encoding";
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000AACE File Offset: 0x00008CCE
		private string HttpContentType
		{
			get
			{
				if (this.version != SoapProtocolVersion.Soap12)
				{
					return "text/xml";
				}
				return "application/soap+xml";
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000AAE4 File Offset: 0x00008CE4
		private SoapClientMessage BeforeSerialize(WebRequest request, string methodName, object[] parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			SoapClientMethod method = this.clientType.GetMethod(methodName);
			if (method == null)
			{
				throw new ArgumentException(Res.GetString("WebInvalidMethodName", new object[] { methodName }));
			}
			SoapReflectedExtension[] array = (SoapReflectedExtension[])SoapHttpClientProtocol.CombineExtensionsHelper(this.clientType.HighPriExtensions, method.extensions, this.clientType.LowPriExtensions, typeof(SoapReflectedExtension));
			object[] array2 = (object[])SoapHttpClientProtocol.CombineExtensionsHelper(this.clientType.HighPriExtensionInitializers, method.extensionInitializers, this.clientType.LowPriExtensionInitializers, typeof(object));
			SoapExtension[] array3 = SoapMessage.InitializeExtensions(array, array2);
			SoapClientMessage soapClientMessage = new SoapClientMessage(this, method, base.Url);
			soapClientMessage.initializedExtensions = array3;
			if (array3 != null)
			{
				soapClientMessage.SetExtensionStream(new SoapExtensionStream());
			}
			soapClientMessage.InitExtensionStreamChain(soapClientMessage.initializedExtensions);
			string text = UrlEncoder.EscapeString(method.action, Encoding.UTF8);
			soapClientMessage.SetStage(SoapMessageStage.BeforeSerialize);
			if (this.version == SoapProtocolVersion.Soap12)
			{
				soapClientMessage.ContentType = ContentType.Compose("application/soap+xml", (base.RequestEncoding != null) ? base.RequestEncoding : Encoding.UTF8, text);
			}
			else
			{
				soapClientMessage.ContentType = ContentType.Compose("text/xml", (base.RequestEncoding != null) ? base.RequestEncoding : Encoding.UTF8);
			}
			soapClientMessage.SetParameterValues(parameters);
			SoapHeaderHandling.GetHeaderMembers(soapClientMessage.Headers, this, method.inHeaderMappings, SoapHeaderDirection.In, true);
			soapClientMessage.RunExtensions(soapClientMessage.initializedExtensions, true);
			request.ContentType = soapClientMessage.ContentType;
			if (soapClientMessage.ContentEncoding != null && soapClientMessage.ContentEncoding.Length > 0)
			{
				request.Headers["Content-Encoding"] = soapClientMessage.ContentEncoding;
			}
			request.Method = "POST";
			if (this.version != SoapProtocolVersion.Soap12 && request.Headers["SOAPAction"] == null)
			{
				StringBuilder stringBuilder = new StringBuilder(text.Length + 2);
				stringBuilder.Append('"');
				stringBuilder.Append(text);
				stringBuilder.Append('"');
				request.Headers.Add("SOAPAction", stringBuilder.ToString());
			}
			return soapClientMessage;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000ACFC File Offset: 0x00008EFC
		private void Serialize(SoapClientMessage message)
		{
			Stream stream = message.Stream;
			SoapClientMethod method = message.Method;
			bool flag = method.use == SoapBindingUse.Encoded;
			string envelopeNs = this.EnvelopeNs;
			string encodingNs = this.EncodingNs;
			XmlWriter writerForMessage = this.GetWriterForMessage(message, 1024);
			if (writerForMessage == null)
			{
				throw new InvalidOperationException(Res.GetString("WebNullWriterForMessage"));
			}
			writerForMessage.WriteStartDocument();
			writerForMessage.WriteStartElement("soap", "Envelope", envelopeNs);
			writerForMessage.WriteAttributeString("xmlns", "soap", null, envelopeNs);
			if (flag)
			{
				writerForMessage.WriteAttributeString("xmlns", "soapenc", null, encodingNs);
				writerForMessage.WriteAttributeString("xmlns", "tns", null, this.clientType.serviceNamespace);
				writerForMessage.WriteAttributeString("xmlns", "types", null, SoapReflector.GetEncodedNamespace(this.clientType.serviceNamespace, this.clientType.serviceDefaultIsEncoded));
			}
			writerForMessage.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
			writerForMessage.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
			SoapHeaderHandling.WriteHeaders(writerForMessage, method.inHeaderSerializer, message.Headers, method.inHeaderMappings, SoapHeaderDirection.In, flag, this.clientType.serviceNamespace, this.clientType.serviceDefaultIsEncoded, envelopeNs);
			writerForMessage.WriteStartElement("Body", envelopeNs);
			if (flag && this.version != SoapProtocolVersion.Soap12)
			{
				writerForMessage.WriteAttributeString("soap", "encodingStyle", null, encodingNs);
			}
			object[] parameterValues = message.GetParameterValues();
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "Serialize", Array.Empty<object>()) : null);
			if (Tracing.On)
			{
				Tracing.Enter(Tracing.TraceId("TraceWriteRequest"), traceMethod, new TraceMethod(method.parameterSerializer, "Serialize", new object[]
				{
					writerForMessage,
					parameterValues,
					null,
					flag ? encodingNs : null
				}));
			}
			method.parameterSerializer.Serialize(writerForMessage, parameterValues, null, flag ? encodingNs : null);
			if (Tracing.On)
			{
				Tracing.Exit(Tracing.TraceId("TraceWriteRequest"), traceMethod);
			}
			writerForMessage.WriteEndElement();
			writerForMessage.WriteEndElement();
			writerForMessage.Flush();
			message.SetStage(SoapMessageStage.AfterSerialize);
			message.RunExtensions(message.initializedExtensions, true);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000AF28 File Offset: 0x00009128
		private object[] ReadResponse(SoapClientMessage message, WebResponse response, Stream responseStream, bool asyncCall)
		{
			SoapClientMethod method = message.Method;
			HttpWebResponse httpWebResponse = response as HttpWebResponse;
			int num = (int)((httpWebResponse != null) ? httpWebResponse.StatusCode : ((HttpStatusCode)(-1)));
			if (num >= 300 && num != 500 && num != 400)
			{
				throw new WebException(RequestResponseUtils.CreateResponseExceptionString(httpWebResponse, responseStream), null, WebExceptionStatus.ProtocolError, httpWebResponse);
			}
			message.Headers.Clear();
			message.SetStream(responseStream);
			message.InitExtensionStreamChain(message.initializedExtensions);
			message.SetStage(SoapMessageStage.BeforeDeserialize);
			message.ContentType = response.ContentType;
			message.ContentEncoding = response.Headers["Content-Encoding"];
			message.RunExtensions(message.initializedExtensions, false);
			if (method.oneWay && (httpWebResponse == null || httpWebResponse.StatusCode != HttpStatusCode.InternalServerError))
			{
				return new object[0];
			}
			bool flag = ContentType.IsSoap(message.ContentType);
			if (!flag || (flag && httpWebResponse != null && httpWebResponse.ContentLength == 0L))
			{
				if (num == 400)
				{
					throw new WebException(RequestResponseUtils.CreateResponseExceptionString(httpWebResponse, responseStream), null, WebExceptionStatus.ProtocolError, httpWebResponse);
				}
				throw new InvalidOperationException(Res.GetString("WebResponseContent", new object[] { message.ContentType, this.HttpContentType }) + Environment.NewLine + RequestResponseUtils.CreateResponseExceptionString(response, responseStream));
			}
			else
			{
				if (message.Exception != null)
				{
					throw message.Exception;
				}
				int num2;
				if (asyncCall || httpWebResponse == null)
				{
					num2 = 512;
				}
				else
				{
					num2 = RequestResponseUtils.GetBufferSize((int)httpWebResponse.ContentLength);
				}
				XmlReader readerForMessage = this.GetReaderForMessage(message, num2);
				if (readerForMessage == null)
				{
					throw new InvalidOperationException(Res.GetString("WebNullReaderForMessage"));
				}
				readerForMessage.MoveToContent();
				int depth = readerForMessage.Depth;
				string encodingNs = this.EncodingNs;
				string namespaceURI = readerForMessage.NamespaceURI;
				if (namespaceURI == null || namespaceURI.Length == 0)
				{
					readerForMessage.ReadStartElement("Envelope");
				}
				else if (readerForMessage.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/")
				{
					readerForMessage.ReadStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
				}
				else
				{
					if (!(readerForMessage.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope"))
					{
						throw new SoapException(Res.GetString("WebInvalidEnvelopeNamespace", new object[] { namespaceURI, this.EnvelopeNs }), SoapException.VersionMismatchFaultCode);
					}
					readerForMessage.ReadStartElement("Envelope", "http://www.w3.org/2003/05/soap-envelope");
				}
				readerForMessage.MoveToContent();
				new SoapHeaderHandling().ReadHeaders(readerForMessage, method.outHeaderSerializer, message.Headers, method.outHeaderMappings, SoapHeaderDirection.Out | SoapHeaderDirection.Fault, namespaceURI, (method.use == SoapBindingUse.Encoded) ? encodingNs : null, false);
				readerForMessage.MoveToContent();
				readerForMessage.ReadStartElement("Body", namespaceURI);
				readerForMessage.MoveToContent();
				if (readerForMessage.IsStartElement("Fault", namespaceURI))
				{
					message.Exception = this.ReadSoapException(readerForMessage);
				}
				else if (method.oneWay)
				{
					readerForMessage.Skip();
					message.SetParameterValues(new object[0]);
				}
				else
				{
					TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "ReadResponse", Array.Empty<object>()) : null);
					bool flag2 = method.use == SoapBindingUse.Encoded;
					if (Tracing.On)
					{
						Tracing.Enter(Tracing.TraceId("TraceReadResponse"), traceMethod, new TraceMethod(method.returnSerializer, "Deserialize", new object[]
						{
							readerForMessage,
							flag2 ? encodingNs : null
						}));
					}
					if (!flag2 && (WebServicesSection.Current.SoapEnvelopeProcessing.IsStrict || Tracing.On))
					{
						XmlDeserializationEvents xmlDeserializationEvents = (Tracing.On ? Tracing.GetDeserializationEvents() : RuntimeUtils.GetDeserializationEvents());
						message.SetParameterValues((object[])method.returnSerializer.Deserialize(readerForMessage, null, xmlDeserializationEvents));
					}
					else
					{
						message.SetParameterValues((object[])method.returnSerializer.Deserialize(readerForMessage, flag2 ? encodingNs : null));
					}
					if (Tracing.On)
					{
						Tracing.Exit(Tracing.TraceId("TraceReadResponse"), traceMethod);
					}
				}
				while (depth < readerForMessage.Depth && readerForMessage.Read())
				{
				}
				if (readerForMessage.NodeType == XmlNodeType.EndElement)
				{
					readerForMessage.Read();
				}
				message.SetStage(SoapMessageStage.AfterDeserialize);
				message.RunExtensions(message.initializedExtensions, false);
				SoapHeaderHandling.SetHeaderMembers(message.Headers, this, method.outHeaderMappings, SoapHeaderDirection.Out | SoapHeaderDirection.Fault, true);
				if (message.Exception != null)
				{
					throw message.Exception;
				}
				return message.GetParameterValues();
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000B348 File Offset: 0x00009548
		private SoapException ReadSoapException(XmlReader reader)
		{
			XmlQualifiedName xmlQualifiedName = XmlQualifiedName.Empty;
			string text = null;
			string text2 = null;
			string text3 = null;
			XmlNode xmlNode = null;
			SoapFaultSubCode soapFaultSubCode = null;
			string text4 = null;
			bool flag = reader.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope";
			if (reader.IsEmptyElement)
			{
				reader.Skip();
			}
			else
			{
				reader.ReadStartElement();
				reader.MoveToContent();
				int depth = reader.Depth;
				while (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.None)
				{
					if (reader.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/" || reader.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope" || reader.NamespaceURI == null || reader.NamespaceURI.Length == 0)
					{
						if (reader.LocalName == "faultcode" || reader.LocalName == "Code")
						{
							if (flag)
							{
								xmlQualifiedName = this.ReadSoap12FaultCode(reader, out soapFaultSubCode);
							}
							else
							{
								xmlQualifiedName = this.ReadFaultCode(reader);
							}
						}
						else if (reader.LocalName == "faultstring")
						{
							text4 = reader.GetAttribute("lang", "http://www.w3.org/XML/1998/namespace");
							reader.MoveToElement();
							text = reader.ReadElementString();
						}
						else if (reader.LocalName == "Reason")
						{
							if (reader.IsEmptyElement)
							{
								reader.Skip();
							}
							else
							{
								reader.ReadStartElement();
								reader.MoveToContent();
								while (reader.NodeType != XmlNodeType.EndElement)
								{
									if (reader.NodeType == XmlNodeType.None)
									{
										break;
									}
									if (reader.LocalName == "Text" && reader.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope")
									{
										text = reader.ReadElementString();
									}
									else
									{
										reader.Skip();
									}
									reader.MoveToContent();
								}
								while (reader.NodeType == XmlNodeType.Whitespace)
								{
									reader.Skip();
								}
								if (reader.NodeType == XmlNodeType.None)
								{
									reader.Skip();
								}
								else
								{
									reader.ReadEndElement();
								}
							}
						}
						else if (reader.LocalName == "faultactor" || reader.LocalName == "Node")
						{
							text2 = reader.ReadElementString();
						}
						else if (reader.LocalName == "detail" || reader.LocalName == "Detail")
						{
							xmlNode = new XmlDocument().ReadNode(reader);
						}
						else if (reader.LocalName == "Role")
						{
							text3 = reader.ReadElementString();
						}
						else
						{
							reader.Skip();
						}
					}
					else
					{
						reader.Skip();
					}
					reader.MoveToContent();
				}
				while (reader.Read() && depth < reader.Depth)
				{
				}
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					reader.Read();
				}
			}
			if (xmlNode != null || flag)
			{
				return new SoapException(text, xmlQualifiedName, text2, text3, text4, xmlNode, soapFaultSubCode, null);
			}
			return new SoapHeaderException(text, xmlQualifiedName, text2, text3, text4, soapFaultSubCode, null);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000B604 File Offset: 0x00009804
		private XmlQualifiedName ReadSoap12FaultCode(XmlReader reader, out SoapFaultSubCode subcode)
		{
			SoapFaultSubCode soapFaultSubCode = this.ReadSoap12FaultCodesRecursive(reader, 0);
			if (soapFaultSubCode == null)
			{
				subcode = null;
				return null;
			}
			subcode = soapFaultSubCode.SubCode;
			return soapFaultSubCode.Code;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000B630 File Offset: 0x00009830
		private SoapFaultSubCode ReadSoap12FaultCodesRecursive(XmlReader reader, int depth)
		{
			if (depth > 100)
			{
				return null;
			}
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return null;
			}
			XmlQualifiedName xmlQualifiedName = null;
			SoapFaultSubCode soapFaultSubCode = null;
			int depth2 = reader.Depth;
			reader.ReadStartElement();
			reader.MoveToContent();
			while (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.None)
			{
				if (reader.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope" || reader.NamespaceURI == null || reader.NamespaceURI.Length == 0)
				{
					if (reader.LocalName == "Value")
					{
						xmlQualifiedName = this.ReadFaultCode(reader);
					}
					else if (reader.LocalName == "Subcode")
					{
						soapFaultSubCode = this.ReadSoap12FaultCodesRecursive(reader, depth + 1);
					}
					else
					{
						reader.Skip();
					}
				}
				else
				{
					reader.Skip();
				}
				reader.MoveToContent();
			}
			while (depth2 < reader.Depth && reader.Read())
			{
			}
			if (reader.NodeType == XmlNodeType.EndElement)
			{
				reader.Read();
			}
			return new SoapFaultSubCode(xmlQualifiedName, soapFaultSubCode);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000B724 File Offset: 0x00009924
		private XmlQualifiedName ReadFaultCode(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return null;
			}
			reader.ReadStartElement();
			string text = reader.ReadString();
			int num = text.IndexOf(":", StringComparison.Ordinal);
			string text2 = reader.NamespaceURI;
			if (num >= 0)
			{
				string text3 = text.Substring(0, num);
				text2 = reader.LookupNamespace(text3);
				if (text2 == null)
				{
					throw new InvalidOperationException(Res.GetString("WebQNamePrefixUndefined", new object[] { text3 }));
				}
			}
			reader.ReadEndElement();
			return new XmlQualifiedName(text.Substring(num + 1), text2);
		}

		// Token: 0x04000253 RID: 595
		private SoapClientType clientType;

		// Token: 0x04000254 RID: 596
		private SoapProtocolVersion version;

		// Token: 0x0200005E RID: 94
		private class InvokeAsyncState
		{
			// Token: 0x06000235 RID: 565 RVA: 0x0000B7A9 File Offset: 0x000099A9
			public InvokeAsyncState(string methodName, object[] parameters)
			{
				this.MethodName = methodName;
				this.Parameters = parameters;
			}

			// Token: 0x04000255 RID: 597
			public string MethodName;

			// Token: 0x04000256 RID: 598
			public object[] Parameters;

			// Token: 0x04000257 RID: 599
			public SoapClientMessage Message;
		}
	}
}
