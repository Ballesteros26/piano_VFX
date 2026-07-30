using System;

namespace Mono.Security
{
	// Token: 0x02000009 RID: 9
	internal sealed class BitConverterLE
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000034D2 File Offset: 0x000016D2
		private BitConverterLE()
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000034DA File Offset: 0x000016DA
		private unsafe static byte[] GetUShortBytes(byte* bytes)
		{
			if (BitConverter.IsLittleEndian)
			{
				return new byte[]
				{
					*bytes,
					bytes[1]
				};
			}
			return new byte[]
			{
				bytes[1],
				*bytes
			};
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003508 File Offset: 0x00001708
		private unsafe static byte[] GetUIntBytes(byte* bytes)
		{
			if (BitConverter.IsLittleEndian)
			{
				return new byte[]
				{
					*bytes,
					bytes[1],
					bytes[2],
					bytes[3]
				};
			}
			return new byte[]
			{
				bytes[3],
				bytes[2],
				bytes[1],
				*bytes
			};
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003560 File Offset: 0x00001760
		private unsafe static byte[] GetULongBytes(byte* bytes)
		{
			if (BitConverter.IsLittleEndian)
			{
				return new byte[]
				{
					*bytes,
					bytes[1],
					bytes[2],
					bytes[3],
					bytes[4],
					bytes[5],
					bytes[6],
					bytes[7]
				};
			}
			return new byte[]
			{
				bytes[7],
				bytes[6],
				bytes[5],
				bytes[4],
				bytes[3],
				bytes[2],
				bytes[1],
				*bytes
			};
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000035ED File Offset: 0x000017ED
		internal static byte[] GetBytes(bool value)
		{
			return new byte[] { value ? 1 : 0 };
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000035FF File Offset: 0x000017FF
		internal unsafe static byte[] GetBytes(char value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003609 File Offset: 0x00001809
		internal unsafe static byte[] GetBytes(short value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003613 File Offset: 0x00001813
		internal unsafe static byte[] GetBytes(int value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000361D File Offset: 0x0000181D
		internal unsafe static byte[] GetBytes(long value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003627 File Offset: 0x00001827
		internal unsafe static byte[] GetBytes(ushort value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003631 File Offset: 0x00001831
		internal unsafe static byte[] GetBytes(uint value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000363B File Offset: 0x0000183B
		internal unsafe static byte[] GetBytes(ulong value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003645 File Offset: 0x00001845
		internal unsafe static byte[] GetBytes(float value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000364F File Offset: 0x0000184F
		internal unsafe static byte[] GetBytes(double value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003659 File Offset: 0x00001859
		private unsafe static void UShortFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				*dst = src[startIndex];
				dst[1] = src[startIndex + 1];
				return;
			}
			*dst = src[startIndex + 1];
			dst[1] = src[startIndex];
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003680 File Offset: 0x00001880
		private unsafe static void UIntFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				*dst = src[startIndex];
				dst[1] = src[startIndex + 1];
				dst[2] = src[startIndex + 2];
				dst[3] = src[startIndex + 3];
				return;
			}
			*dst = src[startIndex + 3];
			dst[1] = src[startIndex + 2];
			dst[2] = src[startIndex + 1];
			dst[3] = src[startIndex];
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000036D8 File Offset: 0x000018D8
		private unsafe static void ULongFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				for (int i = 0; i < 8; i++)
				{
					dst[i] = src[startIndex + i];
				}
				return;
			}
			for (int j = 0; j < 8; j++)
			{
				dst[j] = src[startIndex + (7 - j)];
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003719 File Offset: 0x00001919
		internal static bool ToBoolean(byte[] value, int startIndex)
		{
			return value[startIndex] > 0;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003724 File Offset: 0x00001924
		internal unsafe static char ToChar(byte[] value, int startIndex)
		{
			char c;
			BitConverterLE.UShortFromBytes((byte*)(&c), value, startIndex);
			return c;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000373C File Offset: 0x0000193C
		internal unsafe static short ToInt16(byte[] value, int startIndex)
		{
			short num;
			BitConverterLE.UShortFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003754 File Offset: 0x00001954
		internal unsafe static int ToInt32(byte[] value, int startIndex)
		{
			int num;
			BitConverterLE.UIntFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000376C File Offset: 0x0000196C
		internal unsafe static long ToInt64(byte[] value, int startIndex)
		{
			long num;
			BitConverterLE.ULongFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003784 File Offset: 0x00001984
		internal unsafe static ushort ToUInt16(byte[] value, int startIndex)
		{
			ushort num;
			BitConverterLE.UShortFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000379C File Offset: 0x0000199C
		internal unsafe static uint ToUInt32(byte[] value, int startIndex)
		{
			uint num;
			BitConverterLE.UIntFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000037B4 File Offset: 0x000019B4
		internal unsafe static ulong ToUInt64(byte[] value, int startIndex)
		{
			ulong num;
			BitConverterLE.ULongFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000037CC File Offset: 0x000019CC
		internal unsafe static float ToSingle(byte[] value, int startIndex)
		{
			float num;
			BitConverterLE.UIntFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000037E4 File Offset: 0x000019E4
		internal unsafe static double ToDouble(byte[] value, int startIndex)
		{
			double num;
			BitConverterLE.ULongFromBytes((byte*)(&num), value, startIndex);
			return num;
		}
	}
}
