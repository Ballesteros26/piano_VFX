using System;

namespace UnityEngine.UI
{
	// Token: 0x0200002D RID: 45
	internal static class Misc
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x0000F4F2 File Offset: 0x0000D6F2
		public static void Destroy(Object obj)
		{
			if (obj != null)
			{
				if (Application.isPlaying)
				{
					if (obj is GameObject)
					{
						(obj as GameObject).transform.parent = null;
					}
					Object.Destroy(obj);
					return;
				}
				Object.DestroyImmediate(obj);
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000F52A File Offset: 0x0000D72A
		public static void DestroyImmediate(Object obj)
		{
			if (obj != null)
			{
				if (Application.isEditor)
				{
					Object.DestroyImmediate(obj);
					return;
				}
				Object.Destroy(obj);
			}
		}
	}
}
