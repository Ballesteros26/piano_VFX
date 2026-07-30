using System;
using System.Diagnostics;

namespace System.Linq.Parallel
{
	// Token: 0x02000215 RID: 533
	internal static class TraceHelpers
	{
		// Token: 0x06000D31 RID: 3377 RVA: 0x00003C4C File Offset: 0x00001E4C
		[Conditional("PFXTRACE")]
		internal static void TraceInfo(string msg, params object[] args)
		{
		}
	}
}
