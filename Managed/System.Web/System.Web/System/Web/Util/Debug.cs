using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Web.Util
{
	// Token: 0x02000112 RID: 274
	internal static class Debug
	{
		// Token: 0x06000DE4 RID: 3556 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message)
		{
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message, bool includePrefix)
		{
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message, Exception e)
		{
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DBG")]
		internal static void Trace(string tagName, Exception e)
		{
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message, Exception e, bool includePrefix)
		{
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DBG")]
		public static void TraceException(string tagName, Exception e)
		{
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DBG")]
		internal static void Assert(bool assertion, string message)
		{
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DBG")]
		internal static void Assert(bool assertion)
		{
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DBG")]
		internal static void Fail(string message)
		{
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00008A69 File Offset: 0x00006C69
		internal static bool IsTagEnabled(string tagName)
		{
			return false;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00008A69 File Offset: 0x00006C69
		internal static bool IsTagPresent(string tagName)
		{
			return false;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x000261DB File Offset: 0x000243DB
		internal static bool IsDebuggerPresent()
		{
			return Debug.NativeMethods.IsDebuggerPresent() || Debugger.IsAttached;
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DBG")]
		internal static void Break()
		{
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DBG")]
		internal static void AlwaysValidate(string tagName)
		{
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DBG")]
		internal static void CheckValid(bool assertion, string message)
		{
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DBG")]
		internal static void Validate(object obj)
		{
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DBG")]
		internal static void ValidateArrayBounds<T>(T[] array, int offset, int count)
		{
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DBG")]
		internal static void Validate(string tagName, object obj)
		{
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DBG")]
		internal static void Dump(string tagName, object obj)
		{
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0000EE9B File Offset: 0x0000D09B
		internal static string FormatUtcDate(DateTime utcTime)
		{
			return string.Empty;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0000EE9B File Offset: 0x0000D09B
		internal static string FormatLocalDate(DateTime localTime)
		{
			return string.Empty;
		}

		// Token: 0x040011AA RID: 4522
		internal const string TAG_INTERNAL = "Internal";

		// Token: 0x040011AB RID: 4523
		internal const string TAG_EXTERNAL = "External";

		// Token: 0x040011AC RID: 4524
		internal const string TAG_ALL = "*";

		// Token: 0x040011AD RID: 4525
		internal const string DATE_FORMAT = "yyyy/MM/dd HH:mm:ss.ffff";

		// Token: 0x040011AE RID: 4526
		internal const string TIME_FORMAT = "HH:mm:ss:ffff";

		// Token: 0x02000113 RID: 275
		[SuppressUnmanagedCodeSecurity]
		private static class NativeMethods
		{
			// Token: 0x06000DF9 RID: 3577
			[DllImport("kernel32.dll")]
			internal static extern bool IsDebuggerPresent();
		}
	}
}
