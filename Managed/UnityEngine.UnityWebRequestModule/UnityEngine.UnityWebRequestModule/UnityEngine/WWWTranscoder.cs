using System;
using System.IO;
using System.Text;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[VisibleToOtherModules(new string[] { "UnityEngine.UnityWebRequestWWWModule" })]
	internal class WWWTranscoder
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002ACC File Offset: 0x00000CCC
		private static byte Hex2Byte(byte[] b, int offset)
		{
			byte b2 = 0;
			for (int i = offset; i < offset + 2; i++)
			{
				b2 *= 16;
				int num = (int)b[i];
				bool flag = num >= 48 && num <= 57;
				if (flag)
				{
					num -= 48;
				}
				else
				{
					bool flag2 = num >= 65 && num <= 75;
					if (flag2)
					{
						num -= 55;
					}
					else
					{
						bool flag3 = num >= 97 && num <= 102;
						if (flag3)
						{
							num -= 87;
						}
					}
				}
				bool flag4 = num > 15;
				if (flag4)
				{
					return 63;
				}
				b2 += (byte)num;
			}
			return b2;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002B74 File Offset: 0x00000D74
		private static byte[] Byte2Hex(byte b, byte[] hexChars)
		{
			return new byte[]
			{
				hexChars[b >> 4],
				hexChars[(int)(b & 15)]
			};
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public static string URLEncode(string toEncode)
		{
			return WWWTranscoder.URLEncode(toEncode, Encoding.UTF8);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public static string URLEncode(string toEncode, Encoding e)
		{
			byte[] array = WWWTranscoder.Encode(e.GetBytes(toEncode), WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace, WWWTranscoder.urlForbidden, false);
			return WWWForm.DefaultEncoding.GetString(array, 0, array.Length);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002C00 File Offset: 0x00000E00
		public static byte[] URLEncode(byte[] toEncode)
		{
			return WWWTranscoder.Encode(toEncode, WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace, WWWTranscoder.urlForbidden, false);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002C28 File Offset: 0x00000E28
		public static string DataEncode(string toEncode)
		{
			return WWWTranscoder.DataEncode(toEncode, Encoding.UTF8);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002C48 File Offset: 0x00000E48
		public static string DataEncode(string toEncode, Encoding e)
		{
			byte[] array = WWWTranscoder.Encode(e.GetBytes(toEncode), WWWTranscoder.urlEscapeChar, WWWTranscoder.dataSpace, WWWTranscoder.urlForbidden, false);
			return WWWForm.DefaultEncoding.GetString(array, 0, array.Length);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002C88 File Offset: 0x00000E88
		public static byte[] DataEncode(byte[] toEncode)
		{
			return WWWTranscoder.Encode(toEncode, WWWTranscoder.urlEscapeChar, WWWTranscoder.dataSpace, WWWTranscoder.urlForbidden, false);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public static string QPEncode(string toEncode)
		{
			return WWWTranscoder.QPEncode(toEncode, Encoding.UTF8);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public static string QPEncode(string toEncode, Encoding e)
		{
			byte[] array = WWWTranscoder.Encode(e.GetBytes(toEncode), WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace, WWWTranscoder.qpForbidden, true);
			return WWWForm.DefaultEncoding.GetString(array, 0, array.Length);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002D10 File Offset: 0x00000F10
		public static byte[] QPEncode(byte[] toEncode)
		{
			return WWWTranscoder.Encode(toEncode, WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace, WWWTranscoder.qpForbidden, true);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002D38 File Offset: 0x00000F38
		public static byte[] Encode(byte[] input, byte escapeChar, byte[] space, byte[] forbidden, bool uppercase)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(input.Length * 2))
			{
				for (int i = 0; i < input.Length; i++)
				{
					bool flag = input[i] == 32;
					if (flag)
					{
						memoryStream.Write(space, 0, space.Length);
					}
					else
					{
						bool flag2 = input[i] < 32 || input[i] > 126 || WWWTranscoder.ByteArrayContains(forbidden, input[i]);
						if (flag2)
						{
							memoryStream.WriteByte(escapeChar);
							memoryStream.Write(WWWTranscoder.Byte2Hex(input[i], uppercase ? WWWTranscoder.ucHexChars : WWWTranscoder.lcHexChars), 0, 2);
						}
						else
						{
							memoryStream.WriteByte(input[i]);
						}
					}
				}
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002E04 File Offset: 0x00001004
		private static bool ByteArrayContains(byte[] array, byte b)
		{
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = array[i] == b;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002E40 File Offset: 0x00001040
		public static string URLDecode(string toEncode)
		{
			return WWWTranscoder.URLDecode(toEncode, Encoding.UTF8);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002E60 File Offset: 0x00001060
		public static string URLDecode(string toEncode, Encoding e)
		{
			byte[] array = WWWTranscoder.Decode(WWWForm.DefaultEncoding.GetBytes(toEncode), WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace);
			return e.GetString(array, 0, array.Length);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002E98 File Offset: 0x00001098
		public static byte[] URLDecode(byte[] toEncode)
		{
			return WWWTranscoder.Decode(toEncode, WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002EBC File Offset: 0x000010BC
		public static string DataDecode(string toDecode)
		{
			return WWWTranscoder.DataDecode(toDecode, Encoding.UTF8);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002EDC File Offset: 0x000010DC
		public static string DataDecode(string toDecode, Encoding e)
		{
			byte[] array = WWWTranscoder.Decode(WWWForm.DefaultEncoding.GetBytes(toDecode), WWWTranscoder.urlEscapeChar, WWWTranscoder.dataSpace);
			return e.GetString(array, 0, array.Length);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002F14 File Offset: 0x00001114
		public static byte[] DataDecode(byte[] toDecode)
		{
			return WWWTranscoder.Decode(toDecode, WWWTranscoder.urlEscapeChar, WWWTranscoder.dataSpace);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002F38 File Offset: 0x00001138
		public static string QPDecode(string toEncode)
		{
			return WWWTranscoder.QPDecode(toEncode, Encoding.UTF8);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002F58 File Offset: 0x00001158
		public static string QPDecode(string toEncode, Encoding e)
		{
			byte[] array = WWWTranscoder.Decode(WWWForm.DefaultEncoding.GetBytes(toEncode), WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace);
			return e.GetString(array, 0, array.Length);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002F90 File Offset: 0x00001190
		public static byte[] QPDecode(byte[] toEncode)
		{
			return WWWTranscoder.Decode(toEncode, WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002FB4 File Offset: 0x000011B4
		private static bool ByteSubArrayEquals(byte[] array, int index, byte[] comperand)
		{
			bool flag = array.Length - index < comperand.Length;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < comperand.Length; i++)
				{
					bool flag3 = array[index + i] != comperand[i];
					if (flag3)
					{
						return false;
					}
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003004 File Offset: 0x00001204
		public static byte[] Decode(byte[] input, byte escapeChar, byte[] space)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(input.Length))
			{
				for (int i = 0; i < input.Length; i++)
				{
					bool flag = WWWTranscoder.ByteSubArrayEquals(input, i, space);
					if (flag)
					{
						i += space.Length - 1;
						memoryStream.WriteByte(32);
					}
					else
					{
						bool flag2 = input[i] == escapeChar && i + 2 < input.Length;
						if (flag2)
						{
							i++;
							memoryStream.WriteByte(WWWTranscoder.Hex2Byte(input, i++));
						}
						else
						{
							memoryStream.WriteByte(input[i]);
						}
					}
				}
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000030B8 File Offset: 0x000012B8
		public static bool SevenBitClean(string s)
		{
			return WWWTranscoder.SevenBitClean(s, Encoding.UTF8);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000030D8 File Offset: 0x000012D8
		public static bool SevenBitClean(string s, Encoding e)
		{
			return WWWTranscoder.SevenBitClean(e.GetBytes(s));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000030F8 File Offset: 0x000012F8
		public static bool SevenBitClean(byte[] input)
		{
			for (int i = 0; i < input.Length; i++)
			{
				bool flag = input[i] < 32 || input[i] > 126;
				if (flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000008 RID: 8
		private static byte[] ucHexChars = WWWForm.DefaultEncoding.GetBytes("0123456789ABCDEF");

		// Token: 0x04000009 RID: 9
		private static byte[] lcHexChars = WWWForm.DefaultEncoding.GetBytes("0123456789abcdef");

		// Token: 0x0400000A RID: 10
		private static byte urlEscapeChar = 37;

		// Token: 0x0400000B RID: 11
		private static byte[] urlSpace = new byte[] { 43 };

		// Token: 0x0400000C RID: 12
		private static byte[] dataSpace = WWWForm.DefaultEncoding.GetBytes("%20");

		// Token: 0x0400000D RID: 13
		private static byte[] urlForbidden = WWWForm.DefaultEncoding.GetBytes("@&;:<>=?\"'/\\!#%+$,{}|^[]`");

		// Token: 0x0400000E RID: 14
		private static byte qpEscapeChar = 61;

		// Token: 0x0400000F RID: 15
		private static byte[] qpSpace = new byte[] { 95 };

		// Token: 0x04000010 RID: 16
		private static byte[] qpForbidden = WWWForm.DefaultEncoding.GetBytes("&;=?\"'%+_");
	}
}
