using System;

namespace Melanchall.DryWetMidi.Common
{
	// Token: 0x020001C0 RID: 448
	internal static class DataTypesUtilities
	{
		// Token: 0x06000B03 RID: 2819 RVA: 0x000242DC File Offset: 0x000224DC
		public static byte Combine(FourBitNumber head, FourBitNumber tail)
		{
			return (byte)(((int)head << 4) | (int)tail);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000242EE File Offset: 0x000224EE
		public static ushort Combine(SevenBitNumber head, SevenBitNumber tail)
		{
			return (ushort)(((int)head << 7) | (int)tail);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00024300 File Offset: 0x00022500
		public static uint Combine(SevenBitNumber head, SevenBitNumber middle, SevenBitNumber tail)
		{
			return (uint)(((int)head << 14) | ((int)middle << 7) | (int)tail);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0002431B File Offset: 0x0002251B
		public static ushort Combine(byte head, byte tail)
		{
			return (ushort)(((int)head << 8) | (int)tail);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00024323 File Offset: 0x00022523
		public static uint Combine(ushort head, ushort tail)
		{
			return (uint)(((int)head << 16) | (int)tail);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002432B File Offset: 0x0002252B
		public static FourBitNumber GetTail(this byte number)
		{
			return (FourBitNumber)(number & FourBitNumber.MaxValue);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002433F File Offset: 0x0002253F
		public static SevenBitNumber GetTail(this ushort number)
		{
			return (SevenBitNumber)((byte)(number & (ushort)SevenBitNumber.MaxValue));
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00024353 File Offset: 0x00022553
		public static byte GetTail(this short number)
		{
			return (byte)(number & 255);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002435D File Offset: 0x0002255D
		public static ushort GetTail(this uint number)
		{
			return (ushort)(number & 65535U);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00024367 File Offset: 0x00022567
		public static FourBitNumber GetHead(this byte number)
		{
			return (FourBitNumber)((byte)(number >> 4));
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00024372 File Offset: 0x00022572
		public static SevenBitNumber GetHead(this ushort number)
		{
			return (SevenBitNumber)((byte)(number >> 7));
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002437D File Offset: 0x0002257D
		public static byte GetHead(this short number)
		{
			return (byte)(number >> 8);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00024383 File Offset: 0x00022583
		public static ushort GetHead(this uint number)
		{
			return (ushort)(number >> 16);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002438C File Offset: 0x0002258C
		public static int GetVlqLength(this int number)
		{
			int num = 1;
			if (number > 127)
			{
				num++;
				if (number > 16383)
				{
					num++;
					if (number > 2097151)
					{
						num++;
						if (number > 268435455)
						{
							num++;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x000243CC File Offset: 0x000225CC
		public static int GetVlqLength(this long number)
		{
			int num = 1;
			if (number > 127L)
			{
				num++;
				if (number > 16383L)
				{
					num++;
					if (number > 2097151L)
					{
						num++;
						if (number > 268435455L)
						{
							num++;
							if (number > 34359738367L)
							{
								num++;
								if (number > 4398046511103L)
								{
									num++;
									if (number > 562949953421311L)
									{
										num++;
										if (number > 72057594037927935L)
										{
											num++;
										}
									}
								}
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002444D File Offset: 0x0002264D
		public static byte[] GetVlqBytes(this int number)
		{
			return ((long)number).GetVlqBytes();
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00024458 File Offset: 0x00022658
		public static byte[] GetVlqBytes(this long number)
		{
			byte[] array = new byte[number.GetVlqLength()];
			int num = array.Length - 1;
			array[num--] = (byte)(number & 127L);
			while ((number >>= 7) > 0L)
			{
				byte[] array2 = array;
				int num2 = num;
				array2[num2] |= 128;
				byte[] array3 = array;
				int num3 = num--;
				array3[num3] += (byte)(number & 127L);
			}
			return array;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x000244B8 File Offset: 0x000226B8
		public static byte GetFirstByte(this int number)
		{
			return (byte)((number >> 24) & 255);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000244C5 File Offset: 0x000226C5
		public static byte GetSecondByte(this int number)
		{
			return (byte)((number >> 16) & 255);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x000244D2 File Offset: 0x000226D2
		public static byte GetThirdByte(this int number)
		{
			return (byte)((number >> 8) & 255);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00024353 File Offset: 0x00022553
		public static byte GetFourthByte(this int number)
		{
			return (byte)(number & 255);
		}
	}
}
