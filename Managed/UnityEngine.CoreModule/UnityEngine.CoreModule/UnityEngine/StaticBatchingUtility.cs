using System;

namespace UnityEngine
{
	// Token: 0x020001DD RID: 477
	public sealed class StaticBatchingUtility
	{
		// Token: 0x06001501 RID: 5377 RVA: 0x00022B08 File Offset: 0x00020D08
		public static void Combine(GameObject staticBatchRoot)
		{
			InternalStaticBatchingUtility.CombineRoot(staticBatchRoot, null);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00022B13 File Offset: 0x00020D13
		public static void Combine(GameObject[] gos, GameObject staticBatchRoot)
		{
			InternalStaticBatchingUtility.CombineGameObjects(gos, staticBatchRoot, false, null);
		}
	}
}
