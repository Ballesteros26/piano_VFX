using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000038 RID: 56
	internal class NormalizationTableUtil
	{
		// Token: 0x06000126 RID: 294 RVA: 0x0000606C File Offset: 0x0000426C
		static NormalizationTableUtil()
		{
			int[] array = new int[]
			{
				0, 2320, 6912, 9312, 10624, 11376, 11616, 11920, 42864, 42992,
				63744
			};
			int[] array2 = new int[]
			{
				1760, 4608, 9008, 9728, 10976, 11392, 11632, 13312, 42880, 43008,
				65536
			};
			int[] array3 = new int[] { 144, 2336, 7456, 9312, 9376, 10752, 11616, 11920, 63744 };
			int[] array4 = new int[] { 1760, 4352, 9008, 9376, 9456, 10976, 11632, 13312, 65536 };
			int[] array5 = new int[]
			{
				752, 1152, 1424, 2352, 2480, 2608, 2736, 2864, 3008, 3136,
				3248, 3392, 3520, 3632, 3760, 3840, 4144, 4944, 5904, 6096,
				6304, 6448, 6672, 7616, 8400, 12320, 12432, 43008, 64272, 65056
			};
			int[] array6 = new int[]
			{
				864, 1168, 1872, 2400, 2512, 2640, 2768, 2896, 3024, 3168,
				3280, 3408, 3536, 3664, 3792, 4048, 4160, 4960, 5952, 6112,
				6320, 6464, 6688, 7632, 8432, 12336, 12448, 43024, 64288, 65072
			};
			int[] array7 = new int[] { 1152, 5136, 5744 };
			int[] array8 = new int[] { 4224, 5504, 8624 };
			int[] array9 = new int[] { 0, 2304, 7424, 9472, 12288, 15248, 16400, 19968, 64320 };
			int[] array10 = new int[] { 1792, 4608, 8960, 9728, 12640, 15264, 16432, 40960, 64336 };
			NormalizationTableUtil.Prop = new CodePointIndexer(array, array2, 0, 0);
			NormalizationTableUtil.Map = new CodePointIndexer(array3, array4, 0, 0);
			NormalizationTableUtil.Combining = new CodePointIndexer(array5, array6, 0, 0);
			NormalizationTableUtil.Composite = new CodePointIndexer(array7, array8, 0, 0);
			NormalizationTableUtil.Helper = new CodePointIndexer(array9, array10, 0, 0);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006187 File Offset: 0x00004387
		public static int PropIdx(int cp)
		{
			return NormalizationTableUtil.Prop.ToIndex(cp);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006194 File Offset: 0x00004394
		public static int PropCP(int index)
		{
			return NormalizationTableUtil.Prop.ToCodePoint(index);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000061A1 File Offset: 0x000043A1
		public static int PropCount
		{
			get
			{
				return NormalizationTableUtil.Prop.TotalCount;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000061AD File Offset: 0x000043AD
		public static int MapIdx(int cp)
		{
			return NormalizationTableUtil.Map.ToIndex(cp);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000061BA File Offset: 0x000043BA
		public static int MapCP(int index)
		{
			return NormalizationTableUtil.Map.ToCodePoint(index);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000061C7 File Offset: 0x000043C7
		public static int CbIdx(int cp)
		{
			return NormalizationTableUtil.Combining.ToIndex(cp);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000061D4 File Offset: 0x000043D4
		public static int CbCP(int index)
		{
			return NormalizationTableUtil.Combining.ToCodePoint(index);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000061E1 File Offset: 0x000043E1
		public static int MapCount
		{
			get
			{
				return NormalizationTableUtil.Map.TotalCount;
			}
		}

		// Token: 0x040003FF RID: 1023
		public static readonly CodePointIndexer Prop;

		// Token: 0x04000400 RID: 1024
		public static readonly CodePointIndexer Map;

		// Token: 0x04000401 RID: 1025
		public static readonly CodePointIndexer Combining;

		// Token: 0x04000402 RID: 1026
		public static readonly CodePointIndexer Composite;

		// Token: 0x04000403 RID: 1027
		public static readonly CodePointIndexer Helper;
	}
}
