using System;
using System.Diagnostics;

namespace System
{
	// Token: 0x020001F4 RID: 500
	internal static class BCLDebug
	{
		// Token: 0x0600175F RID: 5983 RVA: 0x00002194 File Offset: 0x00000394
		[Conditional("_DEBUG")]
		public static void Assert(bool condition, string message)
		{
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x00002194 File Offset: 0x00000394
		[Conditional("_DEBUG")]
		internal static void Correctness(bool expr, string msg)
		{
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00002194 File Offset: 0x00000394
		[Conditional("_DEBUG")]
		public static void Log(string message)
		{
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00002194 File Offset: 0x00000394
		[Conditional("_DEBUG")]
		public static void Log(string switchName, string message)
		{
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00002194 File Offset: 0x00000394
		[Conditional("_DEBUG")]
		public static void Log(string switchName, LogLevel level, params object[] messages)
		{
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00002194 File Offset: 0x00000394
		[Conditional("_DEBUG")]
		internal static void Perf(bool expr, string msg)
		{
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00002194 File Offset: 0x00000394
		[Conditional("_LOGGING")]
		public static void Trace(string switchName, params object[] messages)
		{
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal static bool CheckEnabled(string switchName)
		{
			return false;
		}
	}
}
