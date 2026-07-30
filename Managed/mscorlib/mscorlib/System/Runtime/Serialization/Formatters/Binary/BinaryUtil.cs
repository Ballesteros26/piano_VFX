using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000706 RID: 1798
	internal static class BinaryUtil
	{
		// Token: 0x06004B61 RID: 19297 RVA: 0x0010CF1D File Offset: 0x0010B11D
		[Conditional("_LOGGING")]
		public static void NVTraceI(string name, string value)
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x0010CF1D File Offset: 0x0010B11D
		[Conditional("_LOGGING")]
		public static void NVTraceI(string name, object value)
		{
			BCLDebug.CheckEnabled("BINARY");
		}
	}
}
