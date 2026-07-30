using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x020004BA RID: 1210
	[FriendAccessAllowed]
	internal static class AsyncCausalityTracer
	{
		// Token: 0x0600387C RID: 14460 RVA: 0x00002194 File Offset: 0x00000394
		internal static void EnableToETW(bool enabled)
		{
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x0600387D RID: 14461 RVA: 0x00015ED5 File Offset: 0x000140D5
		[FriendAccessAllowed]
		internal static bool LoggingOn
		{
			[FriendAccessAllowed]
			get
			{
				return false;
			}
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x00002194 File Offset: 0x00000394
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceOperationCreation(CausalityTraceLevel traceLevel, int taskId, string operationName, ulong relatedContext)
		{
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x00002194 File Offset: 0x00000394
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceOperationCompletion(CausalityTraceLevel traceLevel, int taskId, AsyncCausalityStatus status)
		{
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x00002194 File Offset: 0x00000394
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceOperationRelation(CausalityTraceLevel traceLevel, int taskId, CausalityRelation relation)
		{
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x00002194 File Offset: 0x00000394
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceSynchronousWorkStart(CausalityTraceLevel traceLevel, int taskId, CausalitySynchronousWork work)
		{
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x00002194 File Offset: 0x00000394
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceSynchronousWorkCompletion(CausalityTraceLevel traceLevel, CausalitySynchronousWork work)
		{
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x000CC7A7 File Offset: 0x000CA9A7
		private static ulong GetOperationId(uint taskId)
		{
			return (ulong)(((long)AppDomain.CurrentDomain.Id << 32) + (long)((ulong)taskId));
		}
	}
}
