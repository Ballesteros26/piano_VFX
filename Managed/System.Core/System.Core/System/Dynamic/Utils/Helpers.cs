using System;
using System.Collections.Generic;

namespace System.Dynamic.Utils
{
	// Token: 0x02000340 RID: 832
	internal static class Helpers
	{
		// Token: 0x0600192F RID: 6447 RVA: 0x00052E64 File Offset: 0x00051064
		internal static T CommonNode<T>(T first, T second, Func<T, T> parent) where T : class
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			if (@default.Equals(first, second))
			{
				return first;
			}
			HashSet<T> hashSet = new HashSet<T>(@default);
			for (T t = first; t != null; t = parent(t))
			{
				hashSet.Add(t);
			}
			for (T t2 = second; t2 != null; t2 = parent(t2))
			{
				if (hashSet.Contains(t2))
				{
					return t2;
				}
			}
			return default(T);
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x00052ED0 File Offset: 0x000510D0
		internal static void IncrementCount<T>(T key, Dictionary<T, int> dict)
		{
			int num;
			dict.TryGetValue(key, out num);
			dict[key] = num + 1;
		}
	}
}
