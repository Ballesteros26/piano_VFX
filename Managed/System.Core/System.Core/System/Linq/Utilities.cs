using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000103 RID: 259
	internal static class Utilities
	{
		// Token: 0x06000905 RID: 2309 RVA: 0x0001D080 File Offset: 0x0001B280
		public static bool AreEqualityComparersEqual<TSource>(IEqualityComparer<TSource> left, IEqualityComparer<TSource> right)
		{
			if (left == right)
			{
				return true;
			}
			EqualityComparer<TSource> @default = EqualityComparer<TSource>.Default;
			if (left == null)
			{
				return right == @default || right.Equals(@default);
			}
			if (right == null)
			{
				return left == @default || left.Equals(@default);
			}
			return left.Equals(right);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001D0C2 File Offset: 0x0001B2C2
		public static Func<TSource, bool> CombinePredicates<TSource>(Func<TSource, bool> predicate1, Func<TSource, bool> predicate2)
		{
			return (TSource x) => predicate1(x) && predicate2(x);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0001D0E2 File Offset: 0x0001B2E2
		public static Func<TSource, TResult> CombineSelectors<TSource, TMiddle, TResult>(Func<TSource, TMiddle> selector1, Func<TMiddle, TResult> selector2)
		{
			return (TSource x) => selector2(selector1(x));
		}
	}
}
