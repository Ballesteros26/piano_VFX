using System;

namespace Mono.Security
{
	// Token: 0x02000041 RID: 65
	internal sealed class BitConverterLE
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00002111 File Offset: 0x00000311
		private BitConverterLE()
		{
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00009D8A File Offset: 0x00007F8A
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

		// Token: 0x06000195 RID: 405 RVA: 0x00009DB8 File Offset: 0x00007FB8
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

		// Token: 0x06000196 RID: 406 RVA: 0x00009E10 File Offset: 0x00008010
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

		// Token: 0x06000197 RID: 407 RVA: 0x00009E9D File Offset: 0x0000809D
		internal static byte[] GetBytes(bool value)
		{
			return new byte[] { value ? 1 : 0 };
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00009EAF File Offset: 0x000080AF
		internal unsafe static byte[] GetBytes(char value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00009EAF File Offset: 0x000080AF
		internal unsafe static byte[] GetBytes(short value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00009EB9 File Offset: 0x000080B9
		internal unsafe static byte[] GetBytes(int value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00009EC3 File Offset: 0x000080C3
		internal unsafe static byte[] GetBytes(long value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00009EAF File Offset: 0x000080AF
		internal unsafe static byte[] GetBytes(ushort value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00009EB9 File Offset: 0x000080B9
		internal unsafe static byte[] GetBytes(uint value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00009EC3 File Offset: 0x000080C3
		internal unsafe static byte[] GetBytes(ulong value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00009EB9 File Offset: 0x000080B9
		internal unsafe static byte[] GetBytes(float value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00009EC3 File Offset: 0x000080C3
		internal unsafe static byte[] GetBytes(double value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00009ECD File Offset: 0x000080CD
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

		// Token: 0x060001A2 RID: 418 RVA: 0x00009EF4 File Offset: 0x000080F4
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

		// Token: 0x060001A3 RID: 419 RVA: 0x00009F4C File Offset: 0x0000814C
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

		// Token: 0x060001A4 RID: 420 RVA: 0x00009F8D File Offset: 0x0000818D
		internal static bool ToBoolean(byte[] value, int startIndex)
		{
			return value[startIndex] > 0;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00009F98 File Offset: 0x00008198
		internal unsafe static char ToChar(byte[] value, int startIndex)
		{
			char c;
			BitConverterLE.UShortFromBytes((byte*)(&c), value, startIndex);
			return c;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00009FB0 File Offset: 0x000081B0
		internal unsafe static short ToInt16(byte[] value, int startIndex)
		{
			short num;
			BitConverterLE.UShortFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00009FC8 File Offset: 0x000081C8
		internal unsafe static int ToInt32(byte[] value, int startIndex)
		{
			int num;
			BitConverterLE.UIntFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00009FE0 File Offset: 0x000081E0
		internal unsafe static long ToInt64(byte[] value, int startIndex)
		{
			long num;
			BitConverterLE.ULongFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00009FF8 File Offset: 0x000081F8
		internal unsafe static ushort ToUInt16(byte[] value, int startIndex)
		{
			ushort num;
			BitConverterLE.UShortFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000A010 File Offset: 0x00008210
		internal unsafe static uint ToUInt32(byte[] value, int startIndex)
		{
			uint num;
			BitConverterLE.UIntFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000A028 File Offset: 0x00008228
		internal unsafe static ulong ToUInt64(byte[] value, int startIndex)
		{
			ulong num;
			BitConverterLE.ULongFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000A040 File Offset: 0x00008240
		internal unsafe static float ToSingle(byte[] value, int startIndex)
		{
			float num;
			BitConverterLE.UIntFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000A058 File Offset: 0x00008258
		internal unsafe static double ToDouble(byte[] value, int startIndex)
		{
			double num;
			BitConverterLE.ULongFromBytes((byte*)(&num), value, startIndex);
			return num;
		}
	}
}
