using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000049 RID: 73
	internal static class HashUtility
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x00009BA8 File Offset: 0x00007DA8
		public static int CombineHash(this int h1, int h2)
		{
			return h1 ^ (int)((long)h2 + (long)((ulong)(-1640531527)) + (long)((long)h1 << 6) + (long)(h1 >> 2));
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00009BC0 File Offset: 0x00007DC0
		public static int CombineHash(int h1, int h2, int h3)
		{
			return h1.CombineHash(h2).CombineHash(h3);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00009BCF File Offset: 0x00007DCF
		public static int CombineHash(int h1, int h2, int h3, int h4)
		{
			return HashUtility.CombineHash(h1, h2, h3).CombineHash(h4);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00009BDF File Offset: 0x00007DDF
		public static int CombineHash(int h1, int h2, int h3, int h4, int h5)
		{
			return HashUtility.CombineHash(h1, h2, h3, h4).CombineHash(h5);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00009BF1 File Offset: 0x00007DF1
		public static int CombineHash(int h1, int h2, int h3, int h4, int h5, int h6)
		{
			return HashUtility.CombineHash(h1, h2, h3, h4, h5).CombineHash(h6);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00009C05 File Offset: 0x00007E05
		public static int CombineHash(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
		{
			return HashUtility.CombineHash(h1, h2, h3, h4, h5, h6).CombineHash(h7);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00009C1C File Offset: 0x00007E1C
		public static int CombineHash(int[] hashes)
		{
			if (hashes == null || hashes.Length == 0)
			{
				return 0;
			}
			int num = hashes[0];
			for (int i = 1; i < hashes.Length; i++)
			{
				num = num.CombineHash(hashes[i]);
			}
			return num;
		}
	}
}
