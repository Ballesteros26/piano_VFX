using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.UI
{
	// Token: 0x0200003C RID: 60
	internal static class ListPool<T>
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x00014EC3 File Offset: 0x000130C3
		private static void Clear(List<T> l)
		{
			l.Clear();
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00014ECB File Offset: 0x000130CB
		public static List<T> Get()
		{
			return ListPool<T>.s_ListPool.Get();
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00014ED7 File Offset: 0x000130D7
		public static void Release(List<T> toRelease)
		{
			ListPool<T>.s_ListPool.Release(toRelease);
		}

		// Token: 0x0400016F RID: 367
		private static readonly ObjectPool<List<T>> s_ListPool = new ObjectPool<List<T>>(null, new UnityAction<List<T>>(ListPool<T>.Clear));
	}
}
