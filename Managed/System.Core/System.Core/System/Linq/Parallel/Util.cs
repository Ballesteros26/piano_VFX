using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000216 RID: 534
	internal static class Util
	{
		// Token: 0x06000D32 RID: 3378 RVA: 0x0002C259 File Offset: 0x0002A459
		internal static int Sign(int x)
		{
			if (x < 0)
			{
				return -1;
			}
			if (x != 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0002C268 File Offset: 0x0002A468
		internal static Comparer<TKey> GetDefaultComparer<TKey>()
		{
			if (typeof(TKey) == typeof(int))
			{
				return (Comparer<TKey>)Util.s_fastIntComparer;
			}
			if (typeof(TKey) == typeof(long))
			{
				return (Comparer<TKey>)Util.s_fastLongComparer;
			}
			if (typeof(TKey) == typeof(float))
			{
				return (Comparer<TKey>)Util.s_fastFloatComparer;
			}
			if (typeof(TKey) == typeof(double))
			{
				return (Comparer<TKey>)Util.s_fastDoubleComparer;
			}
			if (typeof(TKey) == typeof(DateTime))
			{
				return (Comparer<TKey>)Util.s_fastDateTimeComparer;
			}
			return Comparer<TKey>.Default;
		}

		// Token: 0x0400083B RID: 2107
		private static Util.FastIntComparer s_fastIntComparer = new Util.FastIntComparer();

		// Token: 0x0400083C RID: 2108
		private static Util.FastLongComparer s_fastLongComparer = new Util.FastLongComparer();

		// Token: 0x0400083D RID: 2109
		private static Util.FastFloatComparer s_fastFloatComparer = new Util.FastFloatComparer();

		// Token: 0x0400083E RID: 2110
		private static Util.FastDoubleComparer s_fastDoubleComparer = new Util.FastDoubleComparer();

		// Token: 0x0400083F RID: 2111
		private static Util.FastDateTimeComparer s_fastDateTimeComparer = new Util.FastDateTimeComparer();

		// Token: 0x02000217 RID: 535
		private class FastIntComparer : Comparer<int>
		{
			// Token: 0x06000D35 RID: 3381 RVA: 0x0002C36C File Offset: 0x0002A56C
			public override int Compare(int x, int y)
			{
				return x.CompareTo(y);
			}
		}

		// Token: 0x02000218 RID: 536
		private class FastLongComparer : Comparer<long>
		{
			// Token: 0x06000D37 RID: 3383 RVA: 0x0002C37E File Offset: 0x0002A57E
			public override int Compare(long x, long y)
			{
				return x.CompareTo(y);
			}
		}

		// Token: 0x02000219 RID: 537
		private class FastFloatComparer : Comparer<float>
		{
			// Token: 0x06000D39 RID: 3385 RVA: 0x0002C390 File Offset: 0x0002A590
			public override int Compare(float x, float y)
			{
				return x.CompareTo(y);
			}
		}

		// Token: 0x0200021A RID: 538
		private class FastDoubleComparer : Comparer<double>
		{
			// Token: 0x06000D3B RID: 3387 RVA: 0x0002C3A2 File Offset: 0x0002A5A2
			public override int Compare(double x, double y)
			{
				return x.CompareTo(y);
			}
		}

		// Token: 0x0200021B RID: 539
		private class FastDateTimeComparer : Comparer<DateTime>
		{
			// Token: 0x06000D3D RID: 3389 RVA: 0x0002C3B4 File Offset: 0x0002A5B4
			public override int Compare(DateTime x, DateTime y)
			{
				return x.CompareTo(y);
			}
		}
	}
}
