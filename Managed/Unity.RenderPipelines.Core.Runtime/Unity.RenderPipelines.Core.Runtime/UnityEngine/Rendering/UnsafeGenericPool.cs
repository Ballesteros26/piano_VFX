using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000027 RID: 39
	public static class UnsafeGenericPool<T> where T : new()
	{
		// Token: 0x060000DF RID: 223 RVA: 0x0000542D File Offset: 0x0000362D
		public static T Get()
		{
			return UnsafeGenericPool<T>.s_Pool.Get();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005439 File Offset: 0x00003639
		public static ObjectPool<T>.PooledObject Get(out T value)
		{
			return UnsafeGenericPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005446 File Offset: 0x00003646
		public static void Release(T toRelease)
		{
			UnsafeGenericPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x040000BA RID: 186
		private static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(null, null, false);
	}
}
