using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001EF RID: 495
	internal static class QueryLifecycle
	{
		// Token: 0x06000C84 RID: 3204 RVA: 0x00029F44 File Offset: 0x00028144
		internal static void LogicalQueryExecutionBegin(int queryID)
		{
			PlinqEtwProvider.Log.ParallelQueryBegin(queryID);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00029F51 File Offset: 0x00028151
		internal static void LogicalQueryExecutionEnd(int queryID)
		{
			PlinqEtwProvider.Log.ParallelQueryEnd(queryID);
		}
	}
}
