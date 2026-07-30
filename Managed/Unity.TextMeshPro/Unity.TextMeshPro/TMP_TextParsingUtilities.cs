using System;

namespace TMPro
{
	// Token: 0x02000058 RID: 88
	public class TMP_TextParsingUtilities
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0002128F File Offset: 0x0001F48F
		public static TMP_TextParsingUtilities instance
		{
			get
			{
				return TMP_TextParsingUtilities.s_Instance;
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00021298 File Offset: 0x0001F498
		public static int GetHashCode(string s)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num) ^ (int)TMP_TextParsingUtilities.ToUpperASCIIFast(s[i]);
			}
			return num;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x000212CC File Offset: 0x0001F4CC
		public static int GetHashCodeCaseSensitive(string s)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num) ^ (int)s[i];
			}
			return num;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000212FB File Offset: 0x0001F4FB
		public static char ToLowerASCIIFast(char c)
		{
			if ((int)c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".Length - 1)
			{
				return c;
			}
			return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-"[(int)c];
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00021319 File Offset: 0x0001F519
		public static char ToUpperASCIIFast(char c)
		{
			if ((int)c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1)
			{
				return c;
			}
			return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[(int)c];
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00021337 File Offset: 0x0001F537
		public static uint ToUpperASCIIFast(uint c)
		{
			if ((ulong)c > (ulong)((long)("-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1)))
			{
				return c;
			}
			return (uint)"-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[(int)c];
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00021357 File Offset: 0x0001F557
		public static uint ToLowerASCIIFast(uint c)
		{
			if ((ulong)c > (ulong)((long)("-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".Length - 1)))
			{
				return c;
			}
			return (uint)"-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-"[(int)c];
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00021377 File Offset: 0x0001F577
		public static bool IsHighSurrogate(uint c)
		{
			return c > 55296U && c < 56319U;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0002138B File Offset: 0x0001F58B
		public static bool IsLowSurrogate(uint c)
		{
			return c > 56320U && c < 57343U;
		}

		// Token: 0x04000433 RID: 1075
		private static readonly TMP_TextParsingUtilities s_Instance = new TMP_TextParsingUtilities();

		// Token: 0x04000434 RID: 1076
		private const string k_LookupStringL = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-";

		// Token: 0x04000435 RID: 1077
		private const string k_LookupStringU = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-";
	}
}
