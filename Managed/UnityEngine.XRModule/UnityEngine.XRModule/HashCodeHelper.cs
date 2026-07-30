using System;

namespace UnityEngine.XR
{
	// Token: 0x02000027 RID: 39
	internal static class HashCodeHelper
	{
		// Token: 0x0600012E RID: 302 RVA: 0x000046C8 File Offset: 0x000028C8
		public static int Combine(int hash1, int hash2)
		{
			return hash1 * 486187739 + hash2;
		}

		// Token: 0x040000E6 RID: 230
		private const int k_HashCodeMultiplier = 486187739;
	}
}
