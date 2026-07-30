using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000086 RID: 134
	internal static class VisualElementListPool
	{
		// Token: 0x06000342 RID: 834 RVA: 0x0000BEEC File Offset: 0x0000A0EC
		public static List<VisualElement> Copy(List<VisualElement> elements)
		{
			List<VisualElement> list = VisualElementListPool.pool.Get();
			list.AddRange(elements);
			return list;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000BF14 File Offset: 0x0000A114
		public static List<VisualElement> Get(int initialCapacity = 0)
		{
			List<VisualElement> list = VisualElementListPool.pool.Get();
			bool flag = initialCapacity > 0 && list.Capacity < initialCapacity;
			if (flag)
			{
				list.Capacity = initialCapacity;
			}
			return list;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000BF50 File Offset: 0x0000A150
		public static void Release(List<VisualElement> elements)
		{
			elements.Clear();
			VisualElementListPool.pool.Release(elements);
		}

		// Token: 0x0400018D RID: 397
		private static ObjectPool<List<VisualElement>> pool = new ObjectPool<List<VisualElement>>(20);
	}
}
