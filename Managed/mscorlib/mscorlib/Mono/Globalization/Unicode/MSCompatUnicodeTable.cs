using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000035 RID: 53
	internal class MSCompatUnicodeTable
	{
		// Token: 0x06000106 RID: 262 RVA: 0x0000527C File Offset: 0x0000347C
		public static TailoringInfo GetTailoringInfo(int lcid)
		{
			for (int i = 0; i < MSCompatUnicodeTable.tailoringInfos.Length; i++)
			{
				if (MSCompatUnicodeTable.tailoringInfos[i].LCID == lcid)
				{
					return MSCompatUnicodeTable.tailoringInfos[i];
				}
			}
			return null;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000052B4 File Offset: 0x000034B4
		public unsafe static void BuildTailoringTables(CultureInfo culture, TailoringInfo t, ref Contraction[] contractions, ref Level2Map[] diacriticals)
		{
			List<Contraction> list = new List<Contraction>();
			List<Level2Map> list2 = new List<Level2Map>();
			int num = 0;
			char[] array;
			char* ptr;
			if ((array = MSCompatUnicodeTable.tailoringArr) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			int i = t.TailoringIndex;
			int num2 = i + t.TailoringCount;
			while (i < num2)
			{
				int num3 = i + 1;
				switch (ptr[i])
				{
				case '\u0001':
				{
					i++;
					while (ptr[num3] != '\0')
					{
						num3++;
					}
					char[] array2 = new char[num3 - i];
					Marshal.Copy((IntPtr)((void*)(ptr + i)), array2, 0, num3 - i);
					byte[] array3 = new byte[4];
					for (int j = 0; j < 4; j++)
					{
						array3[j] = (byte)ptr[num3 + 1 + j];
					}
					list.Add(new Contraction(num, array2, null, array3));
					i = num3 + 6;
					num++;
					break;
				}
				case '\u0002':
					list2.Add(new Level2Map((byte)ptr[i + 1], (byte)ptr[i + 2]));
					i += 3;
					break;
				case '\u0003':
				{
					i++;
					while (ptr[num3] != '\0')
					{
						num3++;
					}
					char[] array2 = new char[num3 - i];
					Marshal.Copy((IntPtr)((void*)(ptr + i)), array2, 0, num3 - i);
					num3++;
					int num4 = num3;
					while (ptr[num4] != '\0')
					{
						num4++;
					}
					string text = new string(ptr, num3, num4 - num3);
					list.Add(new Contraction(num, array2, text, null));
					i = num4 + 1;
					num++;
					break;
				}
				default:
					throw new NotImplementedException(string.Format("Mono INTERNAL ERROR (Should not happen): Collation tailoring table is broken for culture {0} ({1}) at 0x{2:X}", culture.LCID, culture.Name, i));
				}
			}
			array = null;
			list.Sort(ContractionComparer.Instance);
			list2.Sort((Level2Map a, Level2Map b) => (int)(a.Source - b.Source));
			contractions = list.ToArray();
			diacriticals = list2.ToArray();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000054E0 File Offset: 0x000036E0
		private unsafe static void SetCJKReferences(string name, ref CodePointIndexer cjkIndexer, ref byte* catTable, ref byte* lv1Table, ref CodePointIndexer lv2Indexer, ref byte* lv2Table)
		{
			if (name == "zh-CHS")
			{
				catTable = MSCompatUnicodeTable.cjkCHScategory;
				lv1Table = MSCompatUnicodeTable.cjkCHSlv1;
				cjkIndexer = MSCompatUnicodeTableUtil.CjkCHS;
				return;
			}
			if (name == "zh-CHT")
			{
				catTable = MSCompatUnicodeTable.cjkCHTcategory;
				lv1Table = MSCompatUnicodeTable.cjkCHTlv1;
				cjkIndexer = MSCompatUnicodeTableUtil.Cjk;
				return;
			}
			if (name == "ja")
			{
				catTable = MSCompatUnicodeTable.cjkJAcategory;
				lv1Table = MSCompatUnicodeTable.cjkJAlv1;
				cjkIndexer = MSCompatUnicodeTableUtil.Cjk;
				return;
			}
			if (!(name == "ko"))
			{
				return;
			}
			catTable = MSCompatUnicodeTable.cjkKOcategory;
			lv1Table = MSCompatUnicodeTable.cjkKOlv1;
			lv2Table = MSCompatUnicodeTable.cjkKOlv2;
			cjkIndexer = MSCompatUnicodeTableUtil.Cjk;
			lv2Indexer = MSCompatUnicodeTableUtil.Cjk;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005589 File Offset: 0x00003789
		public unsafe static byte Category(int cp)
		{
			return MSCompatUnicodeTable.categories[MSCompatUnicodeTableUtil.Category.ToIndex(cp)];
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000559D File Offset: 0x0000379D
		public unsafe static byte Level1(int cp)
		{
			return MSCompatUnicodeTable.level1[MSCompatUnicodeTableUtil.Level1.ToIndex(cp)];
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000055B1 File Offset: 0x000037B1
		public unsafe static byte Level2(int cp)
		{
			return MSCompatUnicodeTable.level2[MSCompatUnicodeTableUtil.Level2.ToIndex(cp)];
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000055C5 File Offset: 0x000037C5
		public unsafe static byte Level3(int cp)
		{
			return MSCompatUnicodeTable.level3[MSCompatUnicodeTableUtil.Level3.ToIndex(cp)];
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000055DC File Offset: 0x000037DC
		public static bool IsSortable(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (!MSCompatUnicodeTable.IsSortable((int)s[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005610 File Offset: 0x00003810
		public static bool IsSortable(int cp)
		{
			return !MSCompatUnicodeTable.IsIgnorable(cp) || (cp == 0 || cp == 1600 || cp == 65279) || (6155 <= cp && cp <= 6158) || (8204 <= cp && cp <= 8207) || (8234 <= cp && cp <= 8238) || (8298 <= cp && cp <= 8303) || (8204 <= cp && cp <= 8207) || (65529 <= cp && cp <= 65533);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000056A3 File Offset: 0x000038A3
		public static bool IsIgnorable(int cp)
		{
			return MSCompatUnicodeTable.IsIgnorable(cp, 1);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000056AC File Offset: 0x000038AC
		public unsafe static bool IsIgnorable(int cp, byte flag)
		{
			if (cp == 0)
			{
				return true;
			}
			if ((flag & 1) != 0)
			{
				if (char.GetUnicodeCategory((char)cp) == UnicodeCategory.OtherNotAssigned)
				{
					return true;
				}
				if (55424 <= cp && cp < 56192)
				{
					return true;
				}
			}
			int num = MSCompatUnicodeTableUtil.Ignorable.ToIndex(cp);
			return num >= 0 && (MSCompatUnicodeTable.ignorableFlags[num] & flag) > 0;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005701 File Offset: 0x00003901
		public static bool IsIgnorableSymbol(int cp)
		{
			return MSCompatUnicodeTable.IsIgnorable(cp, 2);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000570A File Offset: 0x0000390A
		public static bool IsIgnorableNonSpacing(int cp)
		{
			return MSCompatUnicodeTable.IsIgnorable(cp, 4);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005713 File Offset: 0x00003913
		public static int ToKanaTypeInsensitive(int i)
		{
			if (12353 > i || i > 12436)
			{
				return i;
			}
			return i + 96;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000572C File Offset: 0x0000392C
		public static int ToWidthCompat(int i)
		{
			if (i < 8592)
			{
				return i;
			}
			if (i > 65280)
			{
				if (i <= 65374)
				{
					return i - 65280 + 32;
				}
				switch (i)
				{
				case 65504:
					return 162;
				case 65505:
					return 163;
				case 65506:
					return 172;
				case 65507:
					return 175;
				case 65508:
					return 166;
				case 65509:
					return 165;
				case 65510:
					return 8361;
				}
			}
			if (i > 13054)
			{
				return i;
			}
			if (i <= 8595)
			{
				return 56921 + i;
			}
			if (i < 9474)
			{
				return i;
			}
			if (i <= 9675)
			{
				if (i == 9474)
				{
					return 65512;
				}
				if (i == 9632)
				{
					return 65517;
				}
				if (i != 9675)
				{
					return i;
				}
				return 65518;
			}
			else
			{
				if (i < 12288)
				{
					return i;
				}
				if (i < 12593)
				{
					if (i <= 12300)
					{
						switch (i)
						{
						case 12288:
							return 32;
						case 12289:
							return 65380;
						case 12290:
							return 65377;
						default:
							if (i == 12300)
							{
								return 65378;
							}
							break;
						}
					}
					else
					{
						if (i == 12301)
						{
							return 65379;
						}
						if (i == 12539)
						{
							return 65381;
						}
					}
					return i;
				}
				if (i < 12644)
				{
					return i - 12592 + 65440;
				}
				if (i == 12644)
				{
					return 65440;
				}
				return i;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000058A4 File Offset: 0x00003AA4
		public static bool HasSpecialWeight(char c)
		{
			if (c < 'ぁ')
			{
				return false;
			}
			if ('ｦ' <= c && c < 'ﾞ')
			{
				return true;
			}
			if ('㌀' <= c)
			{
				return false;
			}
			if (c < 'ゝ')
			{
				return c < '\u3099';
			}
			if (c < '\u3100')
			{
				return c != '・';
			}
			return c >= '㋐' && c < '㋿';
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005914 File Offset: 0x00003B14
		public static byte GetJapaneseDashType(char c)
		{
			if (c <= 'ゞ')
			{
				if (c != 'ゝ' && c != 'ゞ')
				{
					return 3;
				}
			}
			else
			{
				switch (c)
				{
				case 'ー':
					return 5;
				case 'ヽ':
				case 'ヾ':
					break;
				default:
					if (c != 'ｰ')
					{
						return 3;
					}
					break;
				}
			}
			return 4;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005960 File Offset: 0x00003B60
		public static bool IsHalfWidthKana(char c)
		{
			return 'ｦ' <= c && c <= 'ﾝ';
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005977 File Offset: 0x00003B77
		public static bool IsHiragana(char c)
		{
			return 'ぁ' <= c && c <= 'ゔ';
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005990 File Offset: 0x00003B90
		public static bool IsJapaneseSmallLetter(char c)
		{
			if ('ｧ' <= c && c <= 'ｯ')
			{
				return true;
			}
			if ('\u3040' < c && c < 'ヺ')
			{
				if (c <= 'ォ')
				{
					if (c <= 'っ')
					{
						switch (c)
						{
						case 'ぁ':
						case 'ぃ':
						case 'ぅ':
						case 'ぇ':
						case 'ぉ':
							break;
						case 'あ':
						case 'い':
						case 'う':
						case 'え':
							return false;
						default:
							if (c != 'っ')
							{
								return false;
							}
							break;
						}
					}
					else
					{
						switch (c)
						{
						case 'ゃ':
						case 'ゅ':
						case 'ょ':
							break;
						case 'や':
						case 'ゆ':
							return false;
						default:
							if (c != 'ゎ')
							{
								switch (c)
								{
								case 'ァ':
								case 'ィ':
								case 'ゥ':
								case 'ェ':
								case 'ォ':
									break;
								case 'ア':
								case 'イ':
								case 'ウ':
								case 'エ':
									return false;
								default:
									return false;
								}
							}
							break;
						}
					}
				}
				else if (c <= 'ョ')
				{
					if (c != 'ッ')
					{
						switch (c)
						{
						case 'ャ':
						case 'ュ':
						case 'ョ':
							break;
						case 'ヤ':
						case 'ユ':
							return false;
						default:
							return false;
						}
					}
				}
				else if (c != 'ヮ' && c != 'ヵ' && c != 'ヶ')
				{
					return false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005ABF File Offset: 0x00003CBF
		public static bool IsReady
		{
			get
			{
				return MSCompatUnicodeTable.isReady;
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005AC8 File Offset: 0x00003CC8
		private static IntPtr GetResource(string name)
		{
			int num;
			Module module;
			return Assembly.GetExecutingAssembly().GetManifestResourceInternal(name, out num, out module);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005AE4 File Offset: 0x00003CE4
		private unsafe static uint UInt32FromBytePtr(byte* raw, uint idx)
		{
			return (uint)((int)raw[idx] + ((int)raw[idx + 1U] << 8) + ((int)raw[idx + 2U] << 16) + ((int)raw[idx + 3U] << 24));
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005B0C File Offset: 0x00003D0C
		unsafe static MSCompatUnicodeTable()
		{
			IntPtr intPtr = MSCompatUnicodeTable.GetResource("collation.core.bin");
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			byte* ptr = (byte*)(void*)intPtr;
			intPtr = MSCompatUnicodeTable.GetResource("collation.tailoring.bin");
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			byte* ptr2 = (byte*)(void*)intPtr;
			if (ptr == null || ptr2 == null)
			{
				return;
			}
			if (*ptr != 3 || *ptr2 != 3)
			{
				return;
			}
			uint num = 1U;
			uint num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.ignorableFlags = ptr + num;
			num += num2;
			num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.categories = ptr + num;
			num += num2;
			num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.level1 = ptr + num;
			num += num2;
			num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.level2 = ptr + num;
			num += num2;
			num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.level3 = ptr + num;
			num += num2;
			num = 1U;
			uint num3 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
			num += 4U;
			MSCompatUnicodeTable.tailoringInfos = new TailoringInfo[num3];
			int num4 = 0;
			while ((long)num4 < (long)((ulong)num3))
			{
				int num5 = (int)MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
				num += 4U;
				int num6 = (int)MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
				num += 4U;
				int num7 = (int)MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
				num += 4U;
				TailoringInfo tailoringInfo = new TailoringInfo(num5, num6, num7, ptr2[(UIntPtr)(num++)] > 0);
				MSCompatUnicodeTable.tailoringInfos[num4] = tailoringInfo;
				num4++;
			}
			num += 2U;
			num3 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
			num += 4U;
			MSCompatUnicodeTable.tailoringArr = new char[num3];
			int num8 = 0;
			while ((long)num8 < (long)((ulong)num3))
			{
				MSCompatUnicodeTable.tailoringArr[num8] = (char)((int)ptr2[num] + ((int)ptr2[num + 1U] << 8));
				num8++;
				num += 2U;
			}
			MSCompatUnicodeTable.isReady = true;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005CCC File Offset: 0x00003ECC
		public unsafe static void FillCJK(string culture, ref CodePointIndexer cjkIndexer, ref byte* catTable, ref byte* lv1Table, ref CodePointIndexer lv2Indexer, ref byte* lv2Table)
		{
			object obj = MSCompatUnicodeTable.forLock;
			lock (obj)
			{
				MSCompatUnicodeTable.FillCJKCore(culture, ref cjkIndexer, ref catTable, ref lv1Table, ref lv2Indexer, ref lv2Table);
				MSCompatUnicodeTable.SetCJKReferences(culture, ref cjkIndexer, ref catTable, ref lv1Table, ref lv2Indexer, ref lv2Table);
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005D20 File Offset: 0x00003F20
		private unsafe static void FillCJKCore(string culture, ref CodePointIndexer cjkIndexer, ref byte* catTable, ref byte* lv1Table, ref CodePointIndexer cjkLv2Indexer, ref byte* lv2Table)
		{
			if (!MSCompatUnicodeTable.IsReady)
			{
				return;
			}
			string text = null;
			if (!(culture == "zh-CHS"))
			{
				if (!(culture == "zh-CHT"))
				{
					if (!(culture == "ja"))
					{
						if (culture == "ko")
						{
							text = "cjkKO";
							catTable = MSCompatUnicodeTable.cjkKOcategory;
							lv1Table = MSCompatUnicodeTable.cjkKOlv1;
						}
					}
					else
					{
						text = "cjkJA";
						catTable = MSCompatUnicodeTable.cjkJAcategory;
						lv1Table = MSCompatUnicodeTable.cjkJAlv1;
					}
				}
				else
				{
					text = "cjkCHT";
					catTable = MSCompatUnicodeTable.cjkCHTcategory;
					lv1Table = MSCompatUnicodeTable.cjkCHTlv1;
				}
			}
			else
			{
				text = "cjkCHS";
				catTable = MSCompatUnicodeTable.cjkCHScategory;
				lv1Table = MSCompatUnicodeTable.cjkCHSlv1;
			}
			if (text == null || lv1Table != (IntPtr)((UIntPtr)0))
			{
				return;
			}
			uint num = 0U;
			IntPtr intPtr = MSCompatUnicodeTable.GetResource(string.Format("collation.{0}.bin", text));
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			byte* ptr = (byte*)(void*)intPtr;
			num += 1U;
			uint num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			catTable = ptr + num;
			lv1Table = ptr + num + num2;
			if (!(culture == "zh-CHS"))
			{
				if (!(culture == "zh-CHT"))
				{
					if (!(culture == "ja"))
					{
						if (culture == "ko")
						{
							MSCompatUnicodeTable.cjkKOcategory = catTable;
							MSCompatUnicodeTable.cjkKOlv1 = lv1Table;
						}
					}
					else
					{
						MSCompatUnicodeTable.cjkJAcategory = catTable;
						MSCompatUnicodeTable.cjkJAlv1 = lv1Table;
					}
				}
				else
				{
					MSCompatUnicodeTable.cjkCHTcategory = catTable;
					MSCompatUnicodeTable.cjkCHTlv1 = lv1Table;
				}
			}
			else
			{
				MSCompatUnicodeTable.cjkCHScategory = catTable;
				MSCompatUnicodeTable.cjkCHSlv1 = lv1Table;
			}
			if (text != "cjkKO")
			{
				return;
			}
			intPtr = MSCompatUnicodeTable.GetResource("collation.cjkKOlv2.bin");
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			ptr = (byte*)(void*)intPtr;
			num = 5U;
			MSCompatUnicodeTable.cjkKOlv2 = ptr + num;
			lv2Table = MSCompatUnicodeTable.cjkKOlv2;
		}

		// Token: 0x040003E1 RID: 993
		public static int MaxExpansionLength = 3;

		// Token: 0x040003E2 RID: 994
		private unsafe static readonly byte* ignorableFlags;

		// Token: 0x040003E3 RID: 995
		private unsafe static readonly byte* categories;

		// Token: 0x040003E4 RID: 996
		private unsafe static readonly byte* level1;

		// Token: 0x040003E5 RID: 997
		private unsafe static readonly byte* level2;

		// Token: 0x040003E6 RID: 998
		private unsafe static readonly byte* level3;

		// Token: 0x040003E7 RID: 999
		private unsafe static byte* cjkCHScategory;

		// Token: 0x040003E8 RID: 1000
		private unsafe static byte* cjkCHTcategory;

		// Token: 0x040003E9 RID: 1001
		private unsafe static byte* cjkJAcategory;

		// Token: 0x040003EA RID: 1002
		private unsafe static byte* cjkKOcategory;

		// Token: 0x040003EB RID: 1003
		private unsafe static byte* cjkCHSlv1;

		// Token: 0x040003EC RID: 1004
		private unsafe static byte* cjkCHTlv1;

		// Token: 0x040003ED RID: 1005
		private unsafe static byte* cjkJAlv1;

		// Token: 0x040003EE RID: 1006
		private unsafe static byte* cjkKOlv1;

		// Token: 0x040003EF RID: 1007
		private unsafe static byte* cjkKOlv2;

		// Token: 0x040003F0 RID: 1008
		private const int ResourceVersionSize = 1;

		// Token: 0x040003F1 RID: 1009
		private static readonly char[] tailoringArr;

		// Token: 0x040003F2 RID: 1010
		private static readonly TailoringInfo[] tailoringInfos;

		// Token: 0x040003F3 RID: 1011
		private static object forLock = new object();

		// Token: 0x040003F4 RID: 1012
		public static readonly bool isReady;
	}
}
