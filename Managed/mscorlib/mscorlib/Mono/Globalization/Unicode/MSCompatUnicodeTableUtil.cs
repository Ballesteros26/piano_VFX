using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000037 RID: 55
	internal class MSCompatUnicodeTableUtil
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00005EE8 File Offset: 0x000040E8
		static MSCompatUnicodeTableUtil()
		{
			int[] array = new int[] { 0, 40960, 63744 };
			int[] array2 = new int[] { 13312, 42240, 65536 };
			int[] array3 = new int[] { 0, 7680, 12288, 19968, 44032, 63744 };
			int[] array4 = new int[] { 4608, 10240, 13312, 40960, 55216, 65536 };
			int[] array5 = new int[] { 0, 7680, 12288, 19968, 44032, 63744 };
			int[] array6 = new int[] { 4608, 10240, 13312, 40960, 55216, 65536 };
			int[] array7 = new int[] { 0, 7680, 12288, 64256 };
			int[] array8 = new int[] { 3840, 10240, 13312, 65536 };
			int[] array9 = new int[] { 0, 7680, 12288, 64256 };
			int[] array10 = new int[] { 4608, 10240, 13312, 65536 };
			int[] array11 = new int[] { 12544, 19968, 59392 };
			int[] array12 = new int[] { 13312, 40960, 65536 };
			int[] array13 = new int[] { 12544, 19968, 63744 };
			int[] array14 = new int[] { 13312, 40960, 64256 };
			MSCompatUnicodeTableUtil.Ignorable = new CodePointIndexer(array, array2, -1, -1);
			MSCompatUnicodeTableUtil.Category = new CodePointIndexer(array3, array4, 0, 0);
			MSCompatUnicodeTableUtil.Level1 = new CodePointIndexer(array5, array6, 0, 0);
			MSCompatUnicodeTableUtil.Level2 = new CodePointIndexer(array7, array8, 0, 0);
			MSCompatUnicodeTableUtil.Level3 = new CodePointIndexer(array9, array10, 0, 0);
			MSCompatUnicodeTableUtil.CjkCHS = new CodePointIndexer(array11, array12, -1, -1);
			MSCompatUnicodeTableUtil.Cjk = new CodePointIndexer(array13, array14, -1, -1);
		}

		// Token: 0x040003F7 RID: 1015
		public const byte ResourceVersion = 3;

		// Token: 0x040003F8 RID: 1016
		public static readonly CodePointIndexer Ignorable;

		// Token: 0x040003F9 RID: 1017
		public static readonly CodePointIndexer Category;

		// Token: 0x040003FA RID: 1018
		public static readonly CodePointIndexer Level1;

		// Token: 0x040003FB RID: 1019
		public static readonly CodePointIndexer Level2;

		// Token: 0x040003FC RID: 1020
		public static readonly CodePointIndexer Level3;

		// Token: 0x040003FD RID: 1021
		public static readonly CodePointIndexer CjkCHS;

		// Token: 0x040003FE RID: 1022
		public static readonly CodePointIndexer Cjk;
	}
}
