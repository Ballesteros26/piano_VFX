using System;

namespace System.Diagnostics
{
	// Token: 0x020001DF RID: 479
	internal class AssertWrapper
	{
		// Token: 0x06000F32 RID: 3890 RVA: 0x00046992 File Offset: 0x00044B92
		public static void ShowAssert(string stackTrace, StackFrame frame, string message, string detailMessage)
		{
			new DefaultTraceListener().Fail(message, detailMessage);
		}
	}
}
