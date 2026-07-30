using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000028 RID: 40
	public static class ListPool<T>
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00005462 File Offset: 0x00003662
		public static List<T> Get()
		{
			return ListPool<T>.s_Pool.Get();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000546E File Offset: 0x0000366E
		public static ObjectPool<List<T>>.PooledObject Get(out List<T> value)
		{
			return ListPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000547B File Offset: 0x0000367B
		public static void Release(List<T> toRelease)
		{
			ListPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x040000BB RID: 187
		private static readonly ObjectPool<List<T>> s_Pool = new ObjectPool<List<T>>(null, delegate(List<T> l)
		{
			l.Clear();
		}, true);
	}
}
