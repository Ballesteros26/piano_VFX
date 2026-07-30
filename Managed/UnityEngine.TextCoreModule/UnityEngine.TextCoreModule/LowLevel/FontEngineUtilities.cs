using System;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200004F RID: 79
	internal struct FontEngineUtilities
	{
		// Token: 0x0600021A RID: 538 RVA: 0x0001B7F4 File Offset: 0x000199F4
		internal static bool Approximately(float a, float b)
		{
			return Mathf.Abs(a - b) < 0.001f;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0001B818 File Offset: 0x00019A18
		internal static int MaxValue(int a, int b, int c)
		{
			return (a < b) ? ((b < c) ? c : b) : ((a < c) ? c : a);
		}
	}
}
