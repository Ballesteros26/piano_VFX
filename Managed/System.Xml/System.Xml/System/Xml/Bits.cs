using System;

namespace System.Xml
{
	// Token: 0x0200008A RID: 138
	internal static class Bits
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x00016148 File Offset: 0x00014348
		public static int Count(uint num)
		{
			num = (num & Bits.MASK_0101010101010101) + ((num >> 1) & Bits.MASK_0101010101010101);
			num = (num & Bits.MASK_0011001100110011) + ((num >> 2) & Bits.MASK_0011001100110011);
			num = (num & Bits.MASK_0000111100001111) + ((num >> 4) & Bits.MASK_0000111100001111);
			num = (num & Bits.MASK_0000000011111111) + ((num >> 8) & Bits.MASK_0000000011111111);
			num = (num & Bits.MASK_1111111111111111) + (num >> 16);
			return (int)num;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000161B0 File Offset: 0x000143B0
		public static bool ExactlyOne(uint num)
		{
			return num != 0U && (num & (num - 1U)) == 0U;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000161BF File Offset: 0x000143BF
		public static bool MoreThanOne(uint num)
		{
			return (num & (num - 1U)) > 0U;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000161C9 File Offset: 0x000143C9
		public static uint ClearLeast(uint num)
		{
			return num & (num - 1U);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000161D0 File Offset: 0x000143D0
		public static int LeastPosition(uint num)
		{
			if (num == 0U)
			{
				return 0;
			}
			return Bits.Count(num ^ (num - 1U));
		}

		// Token: 0x040002FB RID: 763
		private static readonly uint MASK_0101010101010101 = 1431655765U;

		// Token: 0x040002FC RID: 764
		private static readonly uint MASK_0011001100110011 = 858993459U;

		// Token: 0x040002FD RID: 765
		private static readonly uint MASK_0000111100001111 = 252645135U;

		// Token: 0x040002FE RID: 766
		private static readonly uint MASK_0000000011111111 = 16711935U;

		// Token: 0x040002FF RID: 767
		private static readonly uint MASK_1111111111111111 = 65535U;
	}
}
