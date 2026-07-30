using System;
using System.Web.Services.Diagnostics;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200008A RID: 138
	internal class SyncSessionlessHandler : WebServiceHandler, IHttpHandler
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x00011BF4 File Offset: 0x0000FDF4
		internal SyncSessionlessHandler(ServerProtocol protocol)
			: base(protocol)
		{
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00002B51 File Offset: 0x00000D51
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00011C00 File Offset: 0x0000FE00
		public void ProcessRequest(HttpContext context)
		{
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "ProcessRequest", Array.Empty<object>()) : null);
			if (Tracing.On)
			{
				Tracing.Enter("IHttpHandler.ProcessRequest", traceMethod, Tracing.Details(context.Request));
			}
			base.CoreProcessRequest();
			if (Tracing.On)
			{
				Tracing.Exit("IHttpHandler.ProcessRequest", traceMethod);
			}
		}
	}
}
