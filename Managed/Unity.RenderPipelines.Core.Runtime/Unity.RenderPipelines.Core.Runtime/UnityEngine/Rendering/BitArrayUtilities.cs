using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000056 RID: 86
	public static class BitArrayUtilities
	{
		// Token: 0x06000251 RID: 593 RVA: 0x00009EEE File Offset: 0x000080EE
		public static bool Get8(uint index, byte data)
		{
			return ((int)data & (1 << (int)index)) != 0;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009EEE File Offset: 0x000080EE
		public static bool Get16(uint index, ushort data)
		{
			return ((int)data & (1 << (int)index)) != 0;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00009EEE File Offset: 0x000080EE
		public static bool Get32(uint index, uint data)
		{
			return (data & (1U << (int)index)) > 0U;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009EFB File Offset: 0x000080FB
		public static bool Get64(uint index, ulong data)
		{
			return (data & (1UL << (int)index)) > 0UL;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009F0A File Offset: 0x0000810A
		public static bool Get128(uint index, ulong data1, ulong data2)
		{
			if (index >= 64U)
			{
				return (data2 & (1UL << (int)(index - 64U))) > 0UL;
			}
			return (data1 & (1UL << (int)index)) > 0UL;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009F30 File Offset: 0x00008130
		public static bool Get256(uint index, ulong data1, ulong data2, ulong data3, ulong data4)
		{
			if (index >= 128U)
			{
				if (index >= 192U)
				{
					return (data4 & (1UL << (int)(index - 192U))) > 0UL;
				}
				return (data3 & (1UL << (int)(index - 128U))) > 0UL;
			}
			else
			{
				if (index >= 64U)
				{
					return (data2 & (1UL << (int)(index - 64U))) > 0UL;
				}
				return (data1 & (1UL << (int)index)) > 0UL;
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009F99 File Offset: 0x00008199
		public static void Set8(uint index, ref byte data, bool value)
		{
			data = (byte)(value ? ((int)data | (1 << (int)index)) : ((int)data & ~(1 << (int)index)));
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009FB6 File Offset: 0x000081B6
		public static void Set16(uint index, ref ushort data, bool value)
		{
			data = (ushort)(value ? ((int)data | (1 << (int)index)) : ((int)data & ~(1 << (int)index)));
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009FD3 File Offset: 0x000081D3
		public static void Set32(uint index, ref uint data, bool value)
		{
			data = (value ? (data | (1U << (int)index)) : (data & ~(1U << (int)index)));
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009FEF File Offset: 0x000081EF
		public static void Set64(uint index, ref ulong data, bool value)
		{
			data = (value ? (data | (1UL << (int)index)) : (data & ~(1UL << (int)index)));
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000A010 File Offset: 0x00008210
		public static void Set128(uint index, ref ulong data1, ref ulong data2, bool value)
		{
			if (index < 64U)
			{
				data1 = (value ? (data1 | (1UL << (int)index)) : (data1 & ~(1UL << (int)index)));
				return;
			}
			data2 = (value ? (data2 | (1UL << (int)(index - 64U))) : (data2 & ~(1UL << (int)(index - 64U))));
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000A064 File Offset: 0x00008264
		public static void Set256(uint index, ref ulong data1, ref ulong data2, ref ulong data3, ref ulong data4, bool value)
		{
			if (index < 64U)
			{
				data1 = (value ? (data1 | (1UL << (int)index)) : (data1 & ~(1UL << (int)index)));
				return;
			}
			if (index < 128U)
			{
				data2 = (value ? (data2 | (1UL << (int)(index - 64U))) : (data2 & ~(1UL << (int)(index - 64U))));
				return;
			}
			if (index < 192U)
			{
				data3 = (value ? (data3 | (1UL << (int)(index - 64U))) : (data3 & ~(1UL << (int)(index - 128U))));
				return;
			}
			data4 = (value ? (data4 | (1UL << (int)(index - 64U))) : (data4 & ~(1UL << (int)(index - 192U))));
		}
	}
}
