using System;

namespace System.Net
{
	// Token: 0x02000436 RID: 1078
	internal static class IntPtrHelper
	{
		// Token: 0x06002089 RID: 8329 RVA: 0x0007EBF1 File Offset: 0x0007CDF1
		internal static IntPtr Add(IntPtr a, int b)
		{
			return (IntPtr)((long)a + (long)b);
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x0007EC01 File Offset: 0x0007CE01
		internal static long Subtract(IntPtr a, IntPtr b)
		{
			return (long)a - (long)b;
		}
	}
}
