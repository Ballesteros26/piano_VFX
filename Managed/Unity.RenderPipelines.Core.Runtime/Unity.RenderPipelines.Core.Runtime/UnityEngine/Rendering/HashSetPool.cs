using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000029 RID: 41
	public static class HashSetPool<T>
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x000054A6 File Offset: 0x000036A6
		public static HashSet<T> Get()
		{
			return HashSetPool<T>.s_Pool.Get();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000054B2 File Offset: 0x000036B2
		public static ObjectPool<HashSet<T>>.PooledObject Get(out HashSet<T> value)
		{
			return HashSetPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000054BF File Offset: 0x000036BF
		public static void Release(HashSet<T> toRelease)
		{
			HashSetPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x040000BC RID: 188
		private static readonly ObjectPool<HashSet<T>> s_Pool = new ObjectPool<HashSet<T>>(null, delegate(HashSet<T> l)
		{
			l.Clear();
		}, true);
	}
}
