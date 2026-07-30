using System;
using System.Diagnostics;

namespace System.Net
{
	// Token: 0x020004FA RID: 1274
	internal static class Logging
	{
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002620 RID: 9760 RVA: 0x00009E57 File Offset: 0x00008057
		internal static TraceSource Web
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06002621 RID: 9761 RVA: 0x00009E57 File Offset: 0x00008057
		internal static TraceSource HttpListener
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06002622 RID: 9762 RVA: 0x00009E57 File Offset: 0x00008057
		internal static TraceSource Sockets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRACE")]
		internal static void Enter(TraceSource traceSource, object obj, string method, object paramObject)
		{
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRACE")]
		internal static void Enter(TraceSource traceSource, string msg)
		{
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRACE")]
		internal static void Enter(TraceSource traceSource, string msg, string parameters)
		{
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRACE")]
		internal static void Exception(TraceSource traceSource, object obj, string method, Exception e)
		{
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRACE")]
		internal static void Exit(TraceSource traceSource, object obj, string method, object retObject)
		{
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRACE")]
		internal static void Exit(TraceSource traceSource, string msg)
		{
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRACE")]
		internal static void Exit(TraceSource traceSource, string msg, string parameters)
		{
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRACE")]
		internal static void PrintInfo(TraceSource traceSource, object obj, string method, string msg)
		{
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRACE")]
		internal static void PrintInfo(TraceSource traceSource, object obj, string msg)
		{
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRACE")]
		internal static void PrintInfo(TraceSource traceSource, string msg)
		{
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRACE")]
		internal static void PrintWarning(TraceSource traceSource, object obj, string method, string msg)
		{
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRACE")]
		internal static void PrintWarning(TraceSource traceSource, string msg)
		{
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRACE")]
		internal static void PrintError(TraceSource traceSource, string msg)
		{
		}

		// Token: 0x040020EE RID: 8430
		internal static readonly bool On;
	}
}
