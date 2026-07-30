using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web.Services.Diagnostics;

namespace System.Web.Services.Protocols
{
	/// <summary>Represents the base class for communicating with an XML Web service using the simple HTTP-GET and HTTP-POST protocols bindings.</summary>
	// Token: 0x02000031 RID: 49
	[ComVisible(true)]
	public abstract class HttpSimpleClientProtocol : HttpWebClientProtocol
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.HttpSimpleClientProtocol" /> class.</summary>
		// Token: 0x0600010B RID: 267 RVA: 0x00005018 File Offset: 0x00003218
		protected HttpSimpleClientProtocol()
		{
			Type type = base.GetType();
			this.clientType = (HttpClientType)WebClientProtocol.GetFromCache(type);
			if (this.clientType == null)
			{
				object internalSyncObject = WebClientProtocol.InternalSyncObject;
				lock (internalSyncObject)
				{
					this.clientType = (HttpClientType)WebClientProtocol.GetFromCache(type);
					if (this.clientType == null)
					{
						this.clientType = new HttpClientType(type);
						WebClientProtocol.AddToCache(type, this.clientType);
					}
				}
			}
		}

		/// <summary>Invokes an XML Web service method using HTTP.</summary>
		/// <returns>An array of objects containing the return value and any by-reference or <paramref name="out" /> parameters of the derived class method.</returns>
		/// <param name="methodName">The name of the XML Web service method in the derived class that is invoking the <see cref="M:System.Web.Services.Protocols.HttpSimpleClientProtocol.Invoke(System.String,System.String,System.Object[])" /> method. </param>
		/// <param name="requestUrl">The URL of the XML Web service method that the client is requesting. </param>
		/// <param name="parameters">An array of objects containing the parameters to pass to the remote XML Web service. The order of the values in the array corresponds to the order of the parameters in the calling method of the derived class. </param>
		/// <exception cref="T:System.Exception">The request reached the server computer, but was not processed successfully. </exception>
		// Token: 0x0600010C RID: 268 RVA: 0x000050A8 File Offset: 0x000032A8
		protected object Invoke(string methodName, string requestUrl, object[] parameters)
		{
			HttpClientMethod clientMethod = this.GetClientMethod(methodName);
			MimeParameterWriter parameterWriter = this.GetParameterWriter(clientMethod);
			Uri uri = new Uri(requestUrl);
			if (parameterWriter != null)
			{
				parameterWriter.RequestEncoding = base.RequestEncoding;
				requestUrl = parameterWriter.GetRequestUrl(uri.AbsoluteUri, parameters);
				uri = new Uri(requestUrl, true);
			}
			WebRequest webRequest = null;
			object obj;
			try
			{
				webRequest = this.GetWebRequest(uri);
				base.NotifyClientCallOut(webRequest);
				base.PendingSyncRequest = webRequest;
				if (parameterWriter != null)
				{
					parameterWriter.InitializeRequest(webRequest, parameters);
					if (parameterWriter.UsesWriteRequest)
					{
						if (parameters.Length == 0)
						{
							webRequest.ContentLength = 0L;
						}
						else
						{
							Stream stream = null;
							try
							{
								stream = webRequest.GetRequestStream();
								parameterWriter.WriteRequest(stream, parameters);
							}
							finally
							{
								if (stream != null)
								{
									stream.Close();
								}
							}
						}
					}
				}
				WebResponse webResponse = this.GetWebResponse(webRequest);
				Stream stream2 = null;
				if (webResponse.ContentLength != 0L)
				{
					stream2 = webResponse.GetResponseStream();
				}
				obj = this.ReadResponse(clientMethod, webResponse, stream2);
			}
			finally
			{
				if (webRequest == base.PendingSyncRequest)
				{
					base.PendingSyncRequest = null;
				}
			}
			return obj;
		}

		/// <summary>Starts an asynchronous invocation of a method of an XML Web service.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that can be passed to the <see cref="M:System.Web.Services.Protocols.HttpSimpleClientProtocol.EndInvoke(System.IAsyncResult)" /> method to obtain the return values from the XML Web service method.</returns>
		/// <param name="methodName">The name of the XML Web service method. </param>
		/// <param name="requestUrl">The URL to use when creating the <see cref="T:System.Net.WebRequest" />. </param>
		/// <param name="parameters">An array of objects containing the parameters to pass to the XML Web service method. The order of the values in the array corresponds to the order of the parameters in the calling method of the derived class. </param>
		/// <param name="callback">The delegate to call when the asynchronous method call is complete. If <paramref name="callback" /> is null, the delegate is not called. </param>
		/// <param name="asyncState">The additional information supplied by a client. </param>
		/// <exception cref="T:System.Exception">The request reached the server computer, but was not processed successfully. </exception>
		// Token: 0x0600010D RID: 269 RVA: 0x000051B0 File Offset: 0x000033B0
		protected IAsyncResult BeginInvoke(string methodName, string requestUrl, object[] parameters, AsyncCallback callback, object asyncState)
		{
			HttpClientMethod clientMethod = this.GetClientMethod(methodName);
			MimeParameterWriter parameterWriter = this.GetParameterWriter(clientMethod);
			Uri uri = new Uri(requestUrl);
			if (parameterWriter != null)
			{
				parameterWriter.RequestEncoding = base.RequestEncoding;
				requestUrl = parameterWriter.GetRequestUrl(uri.AbsoluteUri, parameters);
				uri = new Uri(requestUrl, true);
			}
			HttpSimpleClientProtocol.InvokeAsyncState invokeAsyncState = new HttpSimpleClientProtocol.InvokeAsyncState(clientMethod, parameterWriter, parameters);
			WebClientAsyncResult webClientAsyncResult = new WebClientAsyncResult(this, invokeAsyncState, null, callback, asyncState);
			return base.BeginSend(uri, webClientAsyncResult, parameterWriter.UsesWriteRequest);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005220 File Offset: 0x00003420
		internal override void InitializeAsyncRequest(WebRequest request, object internalAsyncState)
		{
			HttpSimpleClientProtocol.InvokeAsyncState invokeAsyncState = (HttpSimpleClientProtocol.InvokeAsyncState)internalAsyncState;
			if (invokeAsyncState.ParamWriter.UsesWriteRequest && invokeAsyncState.Parameters.Length == 0)
			{
				request.ContentLength = 0L;
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005254 File Offset: 0x00003454
		internal override void AsyncBufferedSerialize(WebRequest request, Stream requestStream, object internalAsyncState)
		{
			HttpSimpleClientProtocol.InvokeAsyncState invokeAsyncState = (HttpSimpleClientProtocol.InvokeAsyncState)internalAsyncState;
			if (invokeAsyncState.ParamWriter != null)
			{
				invokeAsyncState.ParamWriter.InitializeRequest(request, invokeAsyncState.Parameters);
				if (invokeAsyncState.ParamWriter.UsesWriteRequest && invokeAsyncState.Parameters.Length != 0)
				{
					invokeAsyncState.ParamWriter.WriteRequest(requestStream, invokeAsyncState.Parameters);
				}
			}
		}

		/// <summary>Completes asynchronous invocation of an XML Web service method using HTTP.</summary>
		/// <returns>An array of objects containing the return value and any by reference or <paramref name="out" /> parameters for the XML Web service method.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> returned from the <see cref="M:System.Web.Services.Protocols.HttpSimpleClientProtocol.BeginInvoke(System.String,System.String,System.Object[],System.AsyncCallback,System.Object)" /> method. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> is not the return value from the <see cref="M:System.Web.Services.Protocols.HttpSimpleClientProtocol.BeginInvoke(System.String,System.String,System.Object[],System.AsyncCallback,System.Object)" /> method. </exception>
		// Token: 0x06000110 RID: 272 RVA: 0x000052AC File Offset: 0x000034AC
		protected object EndInvoke(IAsyncResult asyncResult)
		{
			object obj = null;
			Stream stream = null;
			WebResponse webResponse = base.EndSend(asyncResult, ref obj, ref stream);
			HttpSimpleClientProtocol.InvokeAsyncState invokeAsyncState = (HttpSimpleClientProtocol.InvokeAsyncState)obj;
			return this.ReadResponse(invokeAsyncState.Method, webResponse, stream);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000052E0 File Offset: 0x000034E0
		private void InvokeAsyncCallback(IAsyncResult result)
		{
			object obj = null;
			Exception ex = null;
			WebClientAsyncResult webClientAsyncResult = (WebClientAsyncResult)result;
			if (webClientAsyncResult.Request != null)
			{
				try
				{
					object obj2 = null;
					Stream stream = null;
					WebResponse webResponse = base.EndSend(webClientAsyncResult, ref obj2, ref stream);
					HttpSimpleClientProtocol.InvokeAsyncState invokeAsyncState = (HttpSimpleClientProtocol.InvokeAsyncState)obj2;
					obj = this.ReadResponse(invokeAsyncState.Method, webResponse, stream);
				}
				catch (Exception ex2)
				{
					if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
					{
						throw;
					}
					ex = ex2;
					if (Tracing.On)
					{
						Tracing.ExceptionCatch(TraceEventType.Error, this, "InvokeAsyncCallback", ex2);
					}
				}
			}
			UserToken userToken = (UserToken)((AsyncOperation)result.AsyncState).UserSuppliedState;
			base.OperationCompleted(userToken.UserState, new object[] { obj }, ex, false);
		}

		/// <summary>Invokes the specified method asynchronously.</summary>
		/// <param name="methodName">The name of the method to invoke.</param>
		/// <param name="requestUrl">The request URL of the invoked web service.</param>
		/// <param name="parameters">The parameters to pass to the method.</param>
		/// <param name="callback">The delegate called when the method invocation has completed.</param>
		// Token: 0x06000112 RID: 274 RVA: 0x000053AC File Offset: 0x000035AC
		protected void InvokeAsync(string methodName, string requestUrl, object[] parameters, SendOrPostCallback callback)
		{
			this.InvokeAsync(methodName, requestUrl, parameters, callback, null);
		}

		/// <summary>Invokes the specified method asynchronously while maintaining an associated state.</summary>
		/// <param name="methodName">The name of the method to invoke.</param>
		/// <param name="requestUrl">The request URL of the invoked web service.</param>
		/// <param name="parameters">The parameters to pass to the method.</param>
		/// <param name="callback">The delegate called when the method invocation has completed.</param>
		/// <param name="userState">An object containing associated state information that is passed to the <paramref name="callback" /> delegate when the method has completed.</param>
		// Token: 0x06000113 RID: 275 RVA: 0x000053BC File Offset: 0x000035BC
		protected void InvokeAsync(string methodName, string requestUrl, object[] parameters, SendOrPostCallback callback, object userState)
		{
			if (userState == null)
			{
				userState = base.NullToken;
			}
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(new UserToken(callback, userState));
			WebClientAsyncResult webClientAsyncResult = new WebClientAsyncResult(this, null, null, new AsyncCallback(this.InvokeAsyncCallback), asyncOperation);
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
					Tracing.ExceptionCatch(TraceEventType.Error, this, "InvokeAsync", ex);
				}
				Exception ex2 = new ArgumentException(Res.GetString("AsyncDuplicateUserState"), ex);
				InvokeCompletedEventArgs invokeCompletedEventArgs = new InvokeCompletedEventArgs(new object[1], ex2, false, userState);
				asyncOperation.PostOperationCompleted(callback, invokeCompletedEventArgs);
				return;
			}
			try
			{
				HttpClientMethod clientMethod = this.GetClientMethod(methodName);
				MimeParameterWriter parameterWriter = this.GetParameterWriter(clientMethod);
				Uri uri = new Uri(requestUrl);
				if (parameterWriter != null)
				{
					parameterWriter.RequestEncoding = base.RequestEncoding;
					requestUrl = parameterWriter.GetRequestUrl(uri.AbsoluteUri, parameters);
					uri = new Uri(requestUrl, true);
				}
				webClientAsyncResult.InternalAsyncState = new HttpSimpleClientProtocol.InvokeAsyncState(clientMethod, parameterWriter, parameters);
				base.BeginSend(uri, webClientAsyncResult, parameterWriter.UsesWriteRequest);
			}
			catch (Exception ex3)
			{
				if (ex3 is ThreadAbortException || ex3 is StackOverflowException || ex3 is OutOfMemoryException)
				{
					throw;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Error, this, "InvokeAsync", ex3);
				}
				base.OperationCompleted(userState, new object[1], ex3, false);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000553C File Offset: 0x0000373C
		private MimeParameterWriter GetParameterWriter(HttpClientMethod method)
		{
			if (method.writerType == null)
			{
				return null;
			}
			return (MimeParameterWriter)MimeFormatter.CreateInstance(method.writerType, method.writerInitializer);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005564 File Offset: 0x00003764
		private HttpClientMethod GetClientMethod(string methodName)
		{
			HttpClientMethod method = this.clientType.GetMethod(methodName);
			if (method == null)
			{
				throw new ArgumentException(Res.GetString("WebInvalidMethodName", new object[] { methodName }), "methodName");
			}
			return method;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000055A4 File Offset: 0x000037A4
		private object ReadResponse(HttpClientMethod method, WebResponse response, Stream responseStream)
		{
			HttpWebResponse httpWebResponse = response as HttpWebResponse;
			if (httpWebResponse != null && httpWebResponse.StatusCode >= HttpStatusCode.MultipleChoices)
			{
				throw new WebException(RequestResponseUtils.CreateResponseExceptionString(httpWebResponse, responseStream), null, WebExceptionStatus.ProtocolError, httpWebResponse);
			}
			if (method.readerType == null)
			{
				return null;
			}
			if (responseStream != null)
			{
				return ((MimeReturnReader)MimeFormatter.CreateInstance(method.readerType, method.readerInitializer)).Read(response, responseStream);
			}
			return null;
		}

		// Token: 0x040001ED RID: 493
		private HttpClientType clientType;

		// Token: 0x02000032 RID: 50
		private class InvokeAsyncState
		{
			// Token: 0x06000117 RID: 279 RVA: 0x0000560A File Offset: 0x0000380A
			internal InvokeAsyncState(HttpClientMethod method, MimeParameterWriter paramWriter, object[] parameters)
			{
				this.Method = method;
				this.ParamWriter = paramWriter;
				this.Parameters = parameters;
			}

			// Token: 0x040001EE RID: 494
			internal object[] Parameters;

			// Token: 0x040001EF RID: 495
			internal MimeParameterWriter ParamWriter;

			// Token: 0x040001F0 RID: 496
			internal HttpClientMethod Method;
		}
	}
}
