using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000700 RID: 1792
	internal static class SerTrace
	{
		// Token: 0x06004B33 RID: 19251 RVA: 0x00002194 File Offset: 0x00000394
		[Conditional("_LOGGING")]
		internal static void InfoLog(params object[] messages)
		{
		}

		// Token: 0x06004B34 RID: 19252 RVA: 0x0010C7CD File Offset: 0x0010A9CD
		[Conditional("SER_LOGGING")]
		internal static void Log(params object[] messages)
		{
			if (!(messages[0] is string))
			{
				messages[0] = messages[0].GetType().Name + " ";
				return;
			}
			messages[0] = messages[0] + " ";
		}
	}
}
