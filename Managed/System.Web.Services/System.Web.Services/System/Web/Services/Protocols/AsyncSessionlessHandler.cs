using System;
using System.Web.Services.Diagnostics;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200008C RID: 140
	internal class AsyncSessionlessHandler : SyncSessionlessHandler, IHttpAsyncHandler, IHttpHandler
	{
		// Token: 0x060003BA RID: 954 RVA: 0x00011C5D File Offset: 0x0000FE5D
		internal AsyncSessionlessHandler(ServerProtocol protocol)
			: base(protocol)
		{
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00011C68 File Offset: 0x0000FE68
		public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback callback, object asyncState)
		{
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "BeginProcessRequest", Array.Empty<object>()) : null);
			if (Tracing.On)
			{
				Tracing.Enter("IHttpAsyncHandler.BeginProcessRequest", traceMethod, Tracing.Details(context.Request));
			}
			IAsyncResult asyncResult = base.BeginCoreProcessRequest(callback, asyncState);
			if (Tracing.On)
			{
				Tracing.Exit("IHttpAsyncHandler.BeginProcessRequest", traceMethod);
			}
			return asyncResult;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00011CC8 File Offset: 0x0000FEC8
		public void EndProcessRequest(IAsyncResult asyncResult)
		{
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "EndProcessRequest", Array.Empty<object>()) : null);
			if (Tracing.On)
			{
				Tracing.Enter("IHttpAsyncHandler.EndProcessRequest", traceMethod);
			}
			base.EndCoreProcessRequest(asyncResult);
			if (Tracing.On)
			{
				Tracing.Exit("IHttpAsyncHandler.EndProcessRequest", traceMethod);
			}
		}
	}
}
