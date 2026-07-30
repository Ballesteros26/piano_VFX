using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000026 RID: 38
	public static class GenericPool<T> where T : new()
	{
		// Token: 0x060000DB RID: 219 RVA: 0x000053F8 File Offset: 0x000035F8
		public static T Get()
		{
			return GenericPool<T>.s_Pool.Get();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005404 File Offset: 0x00003604
		public static ObjectPool<T>.PooledObject Get(out T value)
		{
			return GenericPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005411 File Offset: 0x00003611
		public static void Release(T toRelease)
		{
			GenericPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x040000B9 RID: 185
		private static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(null, null, true);
	}
}
