using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web.Services.Diagnostics;
using System.Web.Util;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000089 RID: 137
	internal class WebServiceHandler
	{
		// Token: 0x060003A1 RID: 929 RVA: 0x00011159 File Offset: 0x0000F359
		internal WebServiceHandler(ServerProtocol protocol)
		{
			this.protocol = protocol;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000210D File Offset: 0x0000030D
		private static void TraceFlush()
		{
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00011168 File Offset: 0x0000F368
		private void PrepareContext()
		{
			this.exception = null;
			this.wroteException = false;
			this.asyncCallback = null;
			this.asyncBeginComplete = new ManualResetEvent(false);
			this.asyncCallbackCalls = 0;
			if (this.protocol.IsOneWay)
			{
				return;
			}
			HttpContext context = this.protocol.Context;
			if (context == null)
			{
				return;
			}
			int cacheDuration = this.protocol.MethodAttribute.CacheDuration;
			if (cacheDuration > 0)
			{
				context.Response.Cache.SetCacheability(HttpCacheability.Server);
				context.Response.Cache.SetExpires(DateTime.Now.AddSeconds((double)cacheDuration));
				context.Response.Cache.SetSlidingExpiration(false);
				context.Response.Cache.VaryByHeaders["Content-type"] = true;
				context.Response.Cache.VaryByHeaders["SOAPAction"] = true;
				context.Response.Cache.VaryByParams["*"] = true;
			}
			else
			{
				context.Response.Cache.SetNoServerCaching();
				context.Response.Cache.SetMaxAge(TimeSpan.Zero);
			}
			context.Response.BufferOutput = this.protocol.MethodAttribute.BufferResponse;
			context.Response.ContentType = null;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x000112B4 File Offset: 0x0000F4B4
		private void WriteException(Exception e)
		{
			if (this.wroteException)
			{
				return;
			}
			bool traceVerbose = CompModSwitches.Remote.TraceVerbose;
			if (e is TargetInvocationException)
			{
				bool traceVerbose2 = CompModSwitches.Remote.TraceVerbose;
				e = e.InnerException;
			}
			this.wroteException = this.protocol.WriteException(e, this.protocol.Response.OutputStream);
			if (!this.wroteException)
			{
				throw e;
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001131C File Offset: 0x0000F51C
		private void Invoke()
		{
			this.PrepareContext();
			this.protocol.CreateServerInstance();
			try
			{
				TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "Invoke", Array.Empty<object>()) : null);
				TraceMethod traceMethod2 = (Tracing.On ? new TraceMethod(this.protocol.Target, this.protocol.MethodInfo.Name, this.parameters) : null);
				if (Tracing.On)
				{
					Tracing.Enter(this.protocol.MethodInfo.ToString(), traceMethod, traceMethod2);
				}
				object[] array = this.protocol.MethodInfo.Invoke(this.protocol.Target, this.parameters);
				if (Tracing.On)
				{
					Tracing.Exit(this.protocol.MethodInfo.ToString(), traceMethod);
				}
				this.WriteReturns(array);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Error, this, "Invoke", ex);
				}
				if (!this.protocol.IsOneWay)
				{
					this.WriteException(ex);
					throw;
				}
			}
			finally
			{
				this.protocol.DisposeServerInstance();
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001145C File Offset: 0x0000F65C
		private void InvokeTransacted()
		{
			Transactions.InvokeTransacted(new TransactedCallback(this.Invoke), this.protocol.MethodAttribute.TransactionOption);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001147F File Offset: 0x0000F67F
		private void ThrowInitException()
		{
			this.HandleOneWayException(new Exception(Res.GetString("WebConfigExtensionError"), this.protocol.OnewayInitException), "ThrowInitException");
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000114A6 File Offset: 0x0000F6A6
		private void HandleOneWayException(Exception e, string method)
		{
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Error, this, string.IsNullOrEmpty(method) ? "HandleOneWayException" : method, e);
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000114C8 File Offset: 0x0000F6C8
		protected void CoreProcessRequest()
		{
			try
			{
				bool transactionEnabled = this.protocol.MethodAttribute.TransactionEnabled;
				if (this.protocol.IsOneWay)
				{
					WorkItemCallback workItemCallback;
					TraceMethod traceMethod;
					if (this.protocol.OnewayInitException != null)
					{
						workItemCallback = new WorkItemCallback(this.ThrowInitException);
						traceMethod = (Tracing.On ? new TraceMethod(this, "ThrowInitException", Array.Empty<object>()) : null);
					}
					else
					{
						this.parameters = this.protocol.ReadParameters();
						workItemCallback = (transactionEnabled ? new WorkItemCallback(this.OneWayInvokeTransacted) : new WorkItemCallback(this.OneWayInvoke));
						traceMethod = (Tracing.On ? (transactionEnabled ? new TraceMethod(this, "OneWayInvokeTransacted", Array.Empty<object>()) : new TraceMethod(this, "OneWayInvoke", Array.Empty<object>())) : null);
					}
					if (Tracing.On)
					{
						Tracing.Information("TracePostWorkItemIn", new object[] { traceMethod });
					}
					WorkItem.Post(workItemCallback);
					if (Tracing.On)
					{
						Tracing.Information("TracePostWorkItemOut", new object[] { traceMethod });
					}
					this.protocol.WriteOneWayResponse();
				}
				else if (transactionEnabled)
				{
					this.parameters = this.protocol.ReadParameters();
					this.InvokeTransacted();
				}
				else
				{
					this.parameters = this.protocol.ReadParameters();
					this.Invoke();
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Error, this, "CoreProcessRequest", ex);
				}
				if (!this.protocol.IsOneWay)
				{
					this.WriteException(ex);
				}
			}
			WebServiceHandler.TraceFlush();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00011678 File Offset: 0x0000F878
		private HttpContext SwitchContext(HttpContext context)
		{
			PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
			HttpContext httpContext = HttpContext.Current;
			HttpContext.Current = context;
			return httpContext;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001168C File Offset: 0x0000F88C
		private void OneWayInvoke()
		{
			HttpContext httpContext = null;
			if (this.protocol.Context != null)
			{
				httpContext = this.SwitchContext(this.protocol.Context);
			}
			try
			{
				this.Invoke();
			}
			catch (Exception ex)
			{
				this.HandleOneWayException(ex, "OneWayInvoke");
			}
			finally
			{
				if (httpContext != null)
				{
					this.SwitchContext(httpContext);
				}
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000116FC File Offset: 0x0000F8FC
		private void OneWayInvokeTransacted()
		{
			HttpContext httpContext = null;
			if (this.protocol.Context != null)
			{
				httpContext = this.SwitchContext(this.protocol.Context);
			}
			try
			{
				this.InvokeTransacted();
			}
			catch (Exception ex)
			{
				this.HandleOneWayException(ex, "OneWayInvokeTransacted");
			}
			finally
			{
				if (httpContext != null)
				{
					this.SwitchContext(httpContext);
				}
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001176C File Offset: 0x0000F96C
		private void Callback(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				this.asyncBeginComplete.WaitOne();
			}
			this.DoCallback(result);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00011789 File Offset: 0x0000F989
		private void DoCallback(IAsyncResult result)
		{
			if (this.asyncCallback != null && Interlocked.Increment(ref this.asyncCallbackCalls) == 1)
			{
				this.asyncCallback(result);
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000117B0 File Offset: 0x0000F9B0
		protected IAsyncResult BeginCoreProcessRequest(AsyncCallback callback, object asyncState)
		{
			if (this.protocol.MethodAttribute.TransactionEnabled)
			{
				throw new InvalidOperationException(Res.GetString("WebAsyncTransaction"));
			}
			this.parameters = this.protocol.ReadParameters();
			IAsyncResult asyncResult;
			if (this.protocol.IsOneWay)
			{
				TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "OneWayAsyncInvoke", Array.Empty<object>()) : null);
				if (Tracing.On)
				{
					Tracing.Information("TracePostWorkItemIn", new object[] { traceMethod });
				}
				WorkItem.Post(new WorkItemCallback(this.OneWayAsyncInvoke));
				if (Tracing.On)
				{
					Tracing.Information("TracePostWorkItemOut", new object[] { traceMethod });
				}
				asyncResult = new CompletedAsyncResult(asyncState, true);
				if (callback != null)
				{
					callback(asyncResult);
				}
			}
			else
			{
				asyncResult = this.BeginInvoke(callback, asyncState);
			}
			return asyncResult;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00011880 File Offset: 0x0000FA80
		private void OneWayAsyncInvoke()
		{
			if (this.protocol.OnewayInitException != null)
			{
				this.HandleOneWayException(new Exception(Res.GetString("WebConfigExtensionError"), this.protocol.OnewayInitException), "OneWayAsyncInvoke");
				return;
			}
			HttpContext httpContext = null;
			if (this.protocol.Context != null)
			{
				httpContext = this.SwitchContext(this.protocol.Context);
			}
			try
			{
				this.BeginInvoke(new AsyncCallback(this.OneWayCallback), null);
			}
			catch (Exception ex)
			{
				this.HandleOneWayException(ex, "OneWayAsyncInvoke");
			}
			finally
			{
				if (httpContext != null)
				{
					this.SwitchContext(httpContext);
				}
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00011930 File Offset: 0x0000FB30
		private IAsyncResult BeginInvoke(AsyncCallback callback, object asyncState)
		{
			IAsyncResult asyncResult;
			try
			{
				this.PrepareContext();
				this.protocol.CreateServerInstance();
				this.asyncCallback = callback;
				TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "BeginInvoke", Array.Empty<object>()) : null);
				TraceMethod traceMethod2 = (Tracing.On ? new TraceMethod(this.protocol.Target, this.protocol.MethodInfo.Name, this.parameters) : null);
				if (Tracing.On)
				{
					Tracing.Enter(this.protocol.MethodInfo.ToString(), traceMethod, traceMethod2);
				}
				asyncResult = this.protocol.MethodInfo.BeginInvoke(this.protocol.Target, this.parameters, new AsyncCallback(this.Callback), asyncState);
				if (Tracing.On)
				{
					Tracing.Enter(this.protocol.MethodInfo.ToString(), traceMethod);
				}
				if (asyncResult == null)
				{
					throw new InvalidOperationException(Res.GetString("WebNullAsyncResultInBegin"));
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Error, this, "BeginInvoke", ex);
				}
				this.exception = ex;
				asyncResult = new CompletedAsyncResult(asyncState, true);
				this.asyncCallback = callback;
				this.DoCallback(asyncResult);
			}
			this.asyncBeginComplete.Set();
			WebServiceHandler.TraceFlush();
			return asyncResult;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00011A94 File Offset: 0x0000FC94
		private void OneWayCallback(IAsyncResult asyncResult)
		{
			this.EndInvoke(asyncResult);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00011A9D File Offset: 0x0000FC9D
		protected void EndCoreProcessRequest(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				return;
			}
			if (this.protocol.IsOneWay)
			{
				this.protocol.WriteOneWayResponse();
				return;
			}
			this.EndInvoke(asyncResult);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		private void EndInvoke(IAsyncResult asyncResult)
		{
			try
			{
				if (this.exception != null)
				{
					throw this.exception;
				}
				object[] array = this.protocol.MethodInfo.EndInvoke(this.protocol.Target, asyncResult);
				this.WriteReturns(array);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Error, this, "EndInvoke", ex);
				}
				if (!this.protocol.IsOneWay)
				{
					this.WriteException(ex);
				}
			}
			finally
			{
				this.protocol.DisposeServerInstance();
			}
			WebServiceHandler.TraceFlush();
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00011B7C File Offset: 0x0000FD7C
		private void WriteReturns(object[] returnValues)
		{
			if (this.protocol.IsOneWay)
			{
				return;
			}
			bool bufferResponse = this.protocol.MethodAttribute.BufferResponse;
			Stream stream = this.protocol.Response.OutputStream;
			if (!bufferResponse)
			{
				stream = new BufferedResponseStream(stream, 16384);
				((BufferedResponseStream)stream).FlushEnabled = false;
			}
			this.protocol.WriteReturns(returnValues, stream);
			if (!bufferResponse)
			{
				((BufferedResponseStream)stream).FlushEnabled = true;
				stream.Flush();
			}
		}

		// Token: 0x04000306 RID: 774
		private ServerProtocol protocol;

		// Token: 0x04000307 RID: 775
		private Exception exception;

		// Token: 0x04000308 RID: 776
		private AsyncCallback asyncCallback;

		// Token: 0x04000309 RID: 777
		private ManualResetEvent asyncBeginComplete;

		// Token: 0x0400030A RID: 778
		private int asyncCallbackCalls;

		// Token: 0x0400030B RID: 779
		private bool wroteException;

		// Token: 0x0400030C RID: 780
		private object[] parameters;
	}
}
