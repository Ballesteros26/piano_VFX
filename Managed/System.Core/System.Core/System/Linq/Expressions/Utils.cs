using System;

namespace System.Linq.Expressions
{
	// Token: 0x020002BA RID: 698
	internal static class Utils
	{
		// Token: 0x060014DA RID: 5338 RVA: 0x0003E11C File Offset: 0x0003C31C
		public static ConstantExpression Constant(bool value)
		{
			if (!value)
			{
				return Utils.s_false;
			}
			return Utils.s_true;
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0003E12C File Offset: 0x0003C32C
		public static ConstantExpression Constant(int value)
		{
			switch (value)
			{
			case -1:
				return Utils.s_m1;
			case 0:
				return Utils.s_0;
			case 1:
				return Utils.s_1;
			case 2:
				return Utils.s_2;
			case 3:
				return Utils.s_3;
			default:
				return Expression.Constant(value);
			}
		}

		// Token: 0x040009DC RID: 2524
		public static readonly object BoxedFalse = false;

		// Token: 0x040009DD RID: 2525
		public static readonly object BoxedTrue = true;

		// Token: 0x040009DE RID: 2526
		public static readonly object BoxedIntM1 = -1;

		// Token: 0x040009DF RID: 2527
		public static readonly object BoxedInt0 = 0;

		// Token: 0x040009E0 RID: 2528
		public static readonly object BoxedInt1 = 1;

		// Token: 0x040009E1 RID: 2529
		public static readonly object BoxedInt2 = 2;

		// Token: 0x040009E2 RID: 2530
		public static readonly object BoxedInt3 = 3;

		// Token: 0x040009E3 RID: 2531
		public static readonly object BoxedDefaultSByte = 0;

		// Token: 0x040009E4 RID: 2532
		public static readonly object BoxedDefaultChar = '\0';

		// Token: 0x040009E5 RID: 2533
		public static readonly object BoxedDefaultInt16 = 0;

		// Token: 0x040009E6 RID: 2534
		public static readonly object BoxedDefaultInt64 = 0L;

		// Token: 0x040009E7 RID: 2535
		public static readonly object BoxedDefaultByte = 0;

		// Token: 0x040009E8 RID: 2536
		public static readonly object BoxedDefaultUInt16 = 0;

		// Token: 0x040009E9 RID: 2537
		public static readonly object BoxedDefaultUInt32 = 0U;

		// Token: 0x040009EA RID: 2538
		public static readonly object BoxedDefaultUInt64 = 0UL;

		// Token: 0x040009EB RID: 2539
		public static readonly object BoxedDefaultSingle = 0f;

		// Token: 0x040009EC RID: 2540
		public static readonly object BoxedDefaultDouble = 0.0;

		// Token: 0x040009ED RID: 2541
		public static readonly object BoxedDefaultDecimal = 0m;

		// Token: 0x040009EE RID: 2542
		public static readonly object BoxedDefaultDateTime = default(DateTime);

		// Token: 0x040009EF RID: 2543
		private static readonly ConstantExpression s_true = Expression.Constant(Utils.BoxedTrue);

		// Token: 0x040009F0 RID: 2544
		private static readonly ConstantExpression s_false = Expression.Constant(Utils.BoxedFalse);

		// Token: 0x040009F1 RID: 2545
		private static readonly ConstantExpression s_m1 = Expression.Constant(Utils.BoxedIntM1);

		// Token: 0x040009F2 RID: 2546
		private static readonly ConstantExpression s_0 = Expression.Constant(Utils.BoxedInt0);

		// Token: 0x040009F3 RID: 2547
		private static readonly ConstantExpression s_1 = Expression.Constant(Utils.BoxedInt1);

		// Token: 0x040009F4 RID: 2548
		private static readonly ConstantExpression s_2 = Expression.Constant(Utils.BoxedInt2);

		// Token: 0x040009F5 RID: 2549
		private static readonly ConstantExpression s_3 = Expression.Constant(Utils.BoxedInt3);

		// Token: 0x040009F6 RID: 2550
		public static readonly DefaultExpression Empty = Expression.Empty();

		// Token: 0x040009F7 RID: 2551
		public static readonly ConstantExpression Null = Expression.Constant(null);
	}
}
