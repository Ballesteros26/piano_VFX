using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x0200002A RID: 42
	public static class DictionaryPool<TKey, TValue>
	{
		// Token: 0x060000EB RID: 235 RVA: 0x000054EA File Offset: 0x000036EA
		public static Dictionary<TKey, TValue> Get()
		{
			return DictionaryPool<TKey, TValue>.s_Pool.Get();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000054F6 File Offset: 0x000036F6
		public static ObjectPool<Dictionary<TKey, TValue>>.PooledObject Get(out Dictionary<TKey, TValue> value)
		{
			return DictionaryPool<TKey, TValue>.s_Pool.Get(out value);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005503 File Offset: 0x00003703
		public static void Release(Dictionary<TKey, TValue> toRelease)
		{
			DictionaryPool<TKey, TValue>.s_Pool.Release(toRelease);
		}

		// Token: 0x040000BD RID: 189
		private static readonly ObjectPool<Dictionary<TKey, TValue>> s_Pool = new ObjectPool<Dictionary<TKey, TValue>>(null, delegate(Dictionary<TKey, TValue> l)
		{
			l.Clear();
		}, true);
	}
}
