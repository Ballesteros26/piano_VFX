using System;

namespace System.Text
{
	// Token: 0x02000289 RID: 649
	internal static class StringBuilderCache
	{
		// Token: 0x06001E10 RID: 7696 RVA: 0x000713BC File Offset: 0x0006F5BC
		public static StringBuilder Acquire(int capacity = 16)
		{
			if (capacity <= 360)
			{
				StringBuilder cachedInstance = StringBuilderCache.CachedInstance;
				if (cachedInstance != null && capacity <= cachedInstance.Capacity)
				{
					StringBuilderCache.CachedInstance = null;
					cachedInstance.Clear();
					return cachedInstance;
				}
			}
			return new StringBuilder(capacity);
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x000713F8 File Offset: 0x0006F5F8
		public static void Release(StringBuilder sb)
		{
			if (sb.Capacity <= 360)
			{
				StringBuilderCache.CachedInstance = sb;
			}
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x0007140D File Offset: 0x0006F60D
		public static string GetStringAndRelease(StringBuilder sb)
		{
			string text = sb.ToString();
			StringBuilderCache.Release(sb);
			return text;
		}

		// Token: 0x04001069 RID: 4201
		private const int MAX_BUILDER_SIZE = 360;

		// Token: 0x0400106A RID: 4202
		[ThreadStatic]
		private static StringBuilder CachedInstance;
	}
}
