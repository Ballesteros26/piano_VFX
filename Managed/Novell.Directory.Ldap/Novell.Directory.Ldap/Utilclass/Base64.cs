using System;
using System.IO;
using System.Text;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000042 RID: 66
	public class Base64
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x0000C79C File Offset: 0x0000A99C
		private Base64()
		{
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000C7A4 File Offset: 0x0000A9A4
		public static string encode(string inputString)
		{
			string text;
			try
			{
				text = Base64.encode(SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(inputString)));
			}
			catch (IOException)
			{
				throw new SystemException("US-ASCII String encoding not supported by JVM");
			}
			return text;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000C7EC File Offset: 0x0000A9EC
		[CLSCompliant(false)]
		public static string encode(sbyte[] inputBytes)
		{
			bool flag = false;
			bool flag2 = false;
			int num = inputBytes.Length;
			if (num == 0)
			{
				return new StringBuilder("").ToString();
			}
			int num2;
			if (num % 3 == 0)
			{
				num2 = num / 3;
			}
			else
			{
				num2 = num / 3 + 1;
			}
			if (num % 3 == 1)
			{
				flag2 = true;
			}
			else if (num % 3 == 2)
			{
				flag = true;
			}
			char[] array = new char[num2 * 4];
			int i = 0;
			int num3 = 0;
			int num4 = 1;
			while (i < num)
			{
				int num5 = 255 & (int)inputBytes[i];
				array[num3] = Base64.emap[num5 >> 2];
				if (num4 == num2 && flag2)
				{
					array[num3 + 1] = Base64.emap[(num5 & 3) << 4];
					array[num3 + 2] = '=';
					array[num3 + 3] = '=';
					break;
				}
				int num6 = 255 & (int)inputBytes[i + 1];
				array[num3 + 1] = Base64.emap[((num5 & 3) << 4) + ((num6 & 240) >> 4)];
				if (num4 == num2 && flag)
				{
					array[num3 + 2] = Base64.emap[(num6 & 15) << 2];
					array[num3 + 3] = '=';
					break;
				}
				int num7 = 255 & (int)inputBytes[i + 2];
				array[num3 + 2] = Base64.emap[((num6 & 15) << 2) | ((num7 & 192) >> 6)];
				array[num3 + 3] = Base64.emap[num7 & 63];
				i += 3;
				num3 += 4;
				num4++;
			}
			return new string(array);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000C948 File Offset: 0x0000AB48
		[CLSCompliant(false)]
		public static sbyte[] decode(string encodedString)
		{
			char[] array = new char[encodedString.Length];
			SupportClass.GetCharsFromString(encodedString, 0, encodedString.Length, ref array, 0);
			return Base64.decode(array);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000C978 File Offset: 0x0000AB78
		[CLSCompliant(false)]
		public static sbyte[] decode(char[] encodedChars)
		{
			int num = encodedChars.Length;
			int num2 = num / 4;
			bool flag = false;
			bool flag2 = false;
			if (encodedChars.Length == 0)
			{
				return new sbyte[0];
			}
			if (num % 4 != 0)
			{
				throw new SystemException("Novell.Directory.Ldap.ldif_dsml.Base64Decoder: decode: mal-formatted encode value");
			}
			sbyte[] array;
			if (encodedChars[num - 1] == '=' && encodedChars[num - 2] == '=')
			{
				flag2 = true;
				array = new sbyte[num2 * 3 - 2];
			}
			else if (encodedChars[num - 1] == '=')
			{
				flag = true;
				array = new sbyte[num2 * 3 - 1];
			}
			else
			{
				array = new sbyte[num2 * 3];
			}
			int i = 0;
			int num3 = 0;
			int num4 = 1;
			while (i < num)
			{
				array[num3] = (sbyte)(((int)Base64.dmap[(int)encodedChars[i]] << 2) | ((Base64.dmap[(int)encodedChars[i + 1]] & 48) >> 4));
				if (num4 == num2 && flag2)
				{
					break;
				}
				array[num3 + 1] = (sbyte)(((int)(Base64.dmap[(int)encodedChars[i + 1]] & 15) << 4) | ((Base64.dmap[(int)encodedChars[i + 2]] & 60) >> 2));
				if (num4 == num2 && flag)
				{
					break;
				}
				array[num3 + 2] = (sbyte)(((int)(Base64.dmap[(int)encodedChars[i + 2]] & 3) << 6) | (int)(Base64.dmap[(int)encodedChars[i + 3]] & 63));
				i += 4;
				num3 += 3;
				num4++;
			}
			return array;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000CA9C File Offset: 0x0000AC9C
		[CLSCompliant(false)]
		public static sbyte[] decode(StringBuilder encodedSBuf, int start, int end)
		{
			int num = end - start;
			int num2 = num / 4;
			bool flag = false;
			bool flag2 = false;
			if (encodedSBuf.Length == 0)
			{
				return new sbyte[0];
			}
			if (num % 4 != 0)
			{
				throw new SystemException("Novell.Directory.Ldap.ldif_dsml.Base64Decoder: decode error: mal-formatted encode value");
			}
			sbyte[] array;
			if (encodedSBuf[end - 1] == '=' && encodedSBuf[end - 2] == '=')
			{
				flag2 = true;
				array = new sbyte[num2 * 3 - 2];
			}
			else if (encodedSBuf[end - 1] == '=')
			{
				flag = true;
				array = new sbyte[num2 * 3 - 1];
			}
			else
			{
				array = new sbyte[num2 * 3];
			}
			int i = 0;
			int num3 = 0;
			int num4 = 1;
			while (i < num)
			{
				array[num3] = (sbyte)(((int)Base64.dmap[(int)encodedSBuf[start + i]] << 2) | ((Base64.dmap[(int)encodedSBuf[start + i + 1]] & 48) >> 4));
				if (num4 == num2 && flag2)
				{
					break;
				}
				array[num3 + 1] = (sbyte)(((int)(Base64.dmap[(int)encodedSBuf[start + i + 1]] & 15) << 4) | ((Base64.dmap[(int)encodedSBuf[start + i + 2]] & 60) >> 2));
				if (num4 == num2 && flag)
				{
					break;
				}
				array[num3 + 2] = (sbyte)(((int)(Base64.dmap[(int)encodedSBuf[start + i + 2]] & 3) << 6) | (int)(Base64.dmap[(int)encodedSBuf[start + i + 3]] & 63));
				i += 4;
				num3 += 3;
				num4++;
			}
			return array;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000CBF8 File Offset: 0x0000ADF8
		[CLSCompliant(false)]
		public static bool isLDIFSafe(sbyte[] bytes)
		{
			int num = bytes.Length;
			if (num > 0)
			{
				int num2 = (int)bytes[0];
				if (num2 == 0 || num2 == 10 || num2 == 13 || num2 == 32 || num2 == 58 || num2 == 60 || num2 < 0)
				{
					return false;
				}
				if (bytes[num - 1] == 32)
				{
					return false;
				}
				if (num > 1)
				{
					for (int i = 1; i < bytes.Length; i++)
					{
						num2 = (int)bytes[i];
						if (num2 == 0 || num2 == 10 || num2 == 13 || num2 < 0)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000CC68 File Offset: 0x0000AE68
		public static bool isLDIFSafe(string str)
		{
			bool flag;
			try
			{
				flag = Base64.isLDIFSafe(SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(str)));
			}
			catch (IOException)
			{
				throw new SystemException("UTF-8 String encoding not supported by JVM");
			}
			return flag;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000CCB0 File Offset: 0x0000AEB0
		private static int getByteCount(sbyte b)
		{
			if (b > 0)
			{
				return 0;
			}
			if (((int)b & 224) == 192)
			{
				return 1;
			}
			if (((int)b & 240) == 224)
			{
				return 2;
			}
			if (((int)b & 248) == 240)
			{
				return 3;
			}
			if (((int)b & 252) == 248)
			{
				return 4;
			}
			if (((int)b & 255) == 252)
			{
				return 5;
			}
			return -1;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000CD14 File Offset: 0x0000AF14
		[CLSCompliant(false)]
		public static bool isValidUTF8(sbyte[] array, bool isUCS2Only)
		{
			int i = 0;
			while (i < array.Length)
			{
				int byteCount = Base64.getByteCount(array[i]);
				if (byteCount == 0)
				{
					i++;
				}
				else
				{
					if (byteCount == -1 || i + byteCount >= array.Length || (isUCS2Only && byteCount >= 3))
					{
						return false;
					}
					if ((Base64.lowerBoundMask[byteCount][0] & array[i]) == 0 && (Base64.lowerBoundMask[byteCount][1] & array[i + 1]) == 0)
					{
						return false;
					}
					for (int j = 1; j <= byteCount; j++)
					{
						if ((array[i + j] & Base64.continuationMask) != Base64.continuationResult)
						{
							return false;
						}
					}
					i += byteCount + 1;
				}
			}
			return true;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000CD9C File Offset: 0x0000AF9C
		// Note: this type is marked as 'beforefieldinit'.
		static Base64()
		{
			sbyte[][] array = new sbyte[6][];
			array[0] = new sbyte[2];
			int num = 1;
			sbyte[] array2 = new sbyte[2];
			array2[0] = 30;
			array[num] = array2;
			array[2] = new sbyte[] { 15, 32 };
			array[3] = new sbyte[] { 7, 48 };
			array[4] = new sbyte[] { 2, 56 };
			array[5] = new sbyte[] { 1, 60 };
			Base64.lowerBoundMask = array;
			Base64.continuationMask = (sbyte)SupportClass.Identity(192L);
			Base64.continuationResult = (sbyte)SupportClass.Identity(128L);
		}

		// Token: 0x04000192 RID: 402
		private static readonly char[] emap = new char[]
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
			'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
			'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
			'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
			'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
			'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7',
			'8', '9', '+', '/'
		};

		// Token: 0x04000193 RID: 403
		private static readonly sbyte[] dmap = new sbyte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 62, 0, 0, 0, 63, 52, 53,
			54, 55, 56, 57, 58, 59, 60, 61, 0, 0,
			0, 0, 0, 0, 0, 0, 1, 2, 3, 4,
			5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
			15, 16, 17, 18, 19, 20, 21, 22, 23, 24,
			25, 0, 0, 0, 0, 0, 0, 26, 27, 28,
			29, 30, 31, 32, 33, 34, 35, 36, 37, 38,
			39, 40, 41, 42, 43, 44, 45, 46, 47, 48,
			49, 50, 51, 0, 0, 0, 0, 0
		};

		// Token: 0x04000194 RID: 404
		private static readonly sbyte[][] lowerBoundMask;

		// Token: 0x04000195 RID: 405
		private static sbyte continuationMask;

		// Token: 0x04000196 RID: 406
		private static sbyte continuationResult;
	}
}
