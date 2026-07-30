using System;
using System.Collections.Generic;

namespace TMPro
{
	// Token: 0x0200002E RID: 46
	internal static class TMP_ListPool<T>
	{
		// Token: 0x06000231 RID: 561 RVA: 0x0000D625 File Offset: 0x0000B825
		public static List<T> Get()
		{
			return TMP_ListPool<T>.s_ListPool.Get();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000D631 File Offset: 0x0000B831
		public static void Release(List<T> toRelease)
		{
			TMP_ListPool<T>.s_ListPool.Release(toRelease);
		}

		// Token: 0x04000177 RID: 375
		private static readonly TMP_ObjectPool<List<T>> s_ListPool = new TMP_ObjectPool<List<T>>(null, delegate(List<T> l)
		{
			l.Clear();
		});
	}
}
