using System;
using System.Collections.Generic;
using System.Text;

namespace System.Globalization
{
	// Token: 0x02000408 RID: 1032
	internal class DateTimeFormatInfoScanner
	{
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06003113 RID: 12563 RVA: 0x000B03D8 File Offset: 0x000AE5D8
		private static Dictionary<string, string> KnownWords
		{
			get
			{
				if (DateTimeFormatInfoScanner.s_knownWords == null)
				{
					DateTimeFormatInfoScanner.s_knownWords = new Dictionary<string, string>
					{
						{
							"/",
							string.Empty
						},
						{
							"-",
							string.Empty
						},
						{
							".",
							string.Empty
						},
						{
							"年",
							string.Empty
						},
						{
							"月",
							string.Empty
						},
						{
							"日",
							string.Empty
						},
						{
							"년",
							string.Empty
						},
						{
							"월",
							string.Empty
						},
						{
							"일",
							string.Empty
						},
						{
							"시",
							string.Empty
						},
						{
							"분",
							string.Empty
						},
						{
							"초",
							string.Empty
						},
						{
							"時",
							string.Empty
						},
						{
							"时",
							string.Empty
						},
						{
							"分",
							string.Empty
						},
						{
							"秒",
							string.Empty
						}
					};
				}
				return DateTimeFormatInfoScanner.s_knownWords;
			}
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000B0504 File Offset: 0x000AE704
		internal static int SkipWhiteSpacesAndNonLetter(string pattern, int currentIndex)
		{
			while (currentIndex < pattern.Length)
			{
				char c = pattern[currentIndex];
				if (c == '\\')
				{
					currentIndex++;
					if (currentIndex >= pattern.Length)
					{
						break;
					}
					c = pattern[currentIndex];
					if (c == '\'')
					{
						continue;
					}
				}
				if (char.IsLetter(c) || c == '\'' || c == '.')
				{
					break;
				}
				currentIndex++;
			}
			return currentIndex;
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000B055C File Offset: 0x000AE75C
		internal void AddDateWordOrPostfix(string formatPostfix, string str)
		{
			if (str.Length > 0)
			{
				if (str.Equals("."))
				{
					this.AddIgnorableSymbols(".");
					return;
				}
				string text;
				if (!DateTimeFormatInfoScanner.KnownWords.TryGetValue(str, out text))
				{
					if (this.m_dateWords == null)
					{
						this.m_dateWords = new List<string>();
					}
					if (formatPostfix == "MMMM")
					{
						string text2 = "\ue000" + str;
						if (!this.m_dateWords.Contains(text2))
						{
							this.m_dateWords.Add(text2);
							return;
						}
					}
					else
					{
						if (!this.m_dateWords.Contains(str))
						{
							this.m_dateWords.Add(str);
						}
						if (str[str.Length - 1] == '.')
						{
							string text3 = str.Substring(0, str.Length - 1);
							if (!this.m_dateWords.Contains(text3))
							{
								this.m_dateWords.Add(text3);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000B0640 File Offset: 0x000AE840
		internal int AddDateWords(string pattern, int index, string formatPostfix)
		{
			int num = DateTimeFormatInfoScanner.SkipWhiteSpacesAndNonLetter(pattern, index);
			if (num != index && formatPostfix != null)
			{
				formatPostfix = null;
			}
			index = num;
			StringBuilder stringBuilder = new StringBuilder();
			while (index < pattern.Length)
			{
				char c = pattern[index];
				if (c == '\'')
				{
					this.AddDateWordOrPostfix(formatPostfix, stringBuilder.ToString());
					index++;
					break;
				}
				if (c == '\\')
				{
					index++;
					if (index < pattern.Length)
					{
						stringBuilder.Append(pattern[index]);
						index++;
					}
				}
				else if (char.IsWhiteSpace(c))
				{
					this.AddDateWordOrPostfix(formatPostfix, stringBuilder.ToString());
					if (formatPostfix != null)
					{
						formatPostfix = null;
					}
					stringBuilder.Length = 0;
					index++;
				}
				else
				{
					stringBuilder.Append(c);
					index++;
				}
			}
			return index;
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000B06F4 File Offset: 0x000AE8F4
		internal static int ScanRepeatChar(string pattern, char ch, int index, out int count)
		{
			count = 1;
			while (++index < pattern.Length && pattern[index] == ch)
			{
				count++;
			}
			return index;
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000B071C File Offset: 0x000AE91C
		internal void AddIgnorableSymbols(string text)
		{
			if (this.m_dateWords == null)
			{
				this.m_dateWords = new List<string>();
			}
			string text2 = "\ue001" + text;
			if (!this.m_dateWords.Contains(text2))
			{
				this.m_dateWords.Add(text2);
			}
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000B0764 File Offset: 0x000AE964
		internal void ScanDateWord(string pattern)
		{
			this.m_ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
			for (int i = 0; i < pattern.Length; i++)
			{
				char c = pattern[i];
				if (c <= 'M')
				{
					if (c == '\'')
					{
						i = this.AddDateWords(pattern, i + 1, null);
						continue;
					}
					if (c == '.')
					{
						if (this.m_ymdFlags == DateTimeFormatInfoScanner.FoundDatePattern.FoundYMDPatternFlag)
						{
							this.AddIgnorableSymbols(".");
							this.m_ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
						}
						i++;
						continue;
					}
					if (c == 'M')
					{
						int num;
						i = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'M', i, out num);
						if (num >= 4 && i < pattern.Length && pattern[i] == '\'')
						{
							i = this.AddDateWords(pattern, i + 1, "MMMM");
						}
						this.m_ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundMonthPatternFlag;
						continue;
					}
				}
				else
				{
					if (c == '\\')
					{
						i += 2;
						continue;
					}
					if (c != 'd')
					{
						if (c == 'y')
						{
							int num;
							i = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'y', i, out num);
							this.m_ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundYearPatternFlag;
							continue;
						}
					}
					else
					{
						int num;
						i = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'd', i, out num);
						if (num <= 2)
						{
							this.m_ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundDayPatternFlag;
							continue;
						}
						continue;
					}
				}
				if (this.m_ymdFlags == DateTimeFormatInfoScanner.FoundDatePattern.FoundYMDPatternFlag && !char.IsWhiteSpace(c))
				{
					this.m_ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
				}
			}
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000B089C File Offset: 0x000AEA9C
		internal string[] GetDateWordsOfDTFI(DateTimeFormatInfo dtfi)
		{
			string[] array = dtfi.GetAllDateTimePatterns('D');
			for (int i = 0; i < array.Length; i++)
			{
				this.ScanDateWord(array[i]);
			}
			array = dtfi.GetAllDateTimePatterns('d');
			for (int i = 0; i < array.Length; i++)
			{
				this.ScanDateWord(array[i]);
			}
			array = dtfi.GetAllDateTimePatterns('y');
			for (int i = 0; i < array.Length; i++)
			{
				this.ScanDateWord(array[i]);
			}
			this.ScanDateWord(dtfi.MonthDayPattern);
			array = dtfi.GetAllDateTimePatterns('T');
			for (int i = 0; i < array.Length; i++)
			{
				this.ScanDateWord(array[i]);
			}
			array = dtfi.GetAllDateTimePatterns('t');
			for (int i = 0; i < array.Length; i++)
			{
				this.ScanDateWord(array[i]);
			}
			string[] array2 = null;
			if (this.m_dateWords != null && this.m_dateWords.Count > 0)
			{
				array2 = new string[this.m_dateWords.Count];
				for (int i = 0; i < this.m_dateWords.Count; i++)
				{
					array2[i] = this.m_dateWords[i];
				}
			}
			return array2;
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000B09A4 File Offset: 0x000AEBA4
		internal static FORMATFLAGS GetFormatFlagGenitiveMonth(string[] monthNames, string[] genitveMonthNames, string[] abbrevMonthNames, string[] genetiveAbbrevMonthNames)
		{
			if (DateTimeFormatInfoScanner.EqualStringArrays(monthNames, genitveMonthNames) && DateTimeFormatInfoScanner.EqualStringArrays(abbrevMonthNames, genetiveAbbrevMonthNames))
			{
				return FORMATFLAGS.None;
			}
			return FORMATFLAGS.UseGenitiveMonth;
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000B09BC File Offset: 0x000AEBBC
		internal static FORMATFLAGS GetFormatFlagUseSpaceInMonthNames(string[] monthNames, string[] genitveMonthNames, string[] abbrevMonthNames, string[] genetiveAbbrevMonthNames)
		{
			return FORMATFLAGS.None | ((DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(monthNames) || DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(genitveMonthNames) || DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(abbrevMonthNames) || DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(genetiveAbbrevMonthNames)) ? FORMATFLAGS.UseDigitPrefixInTokens : FORMATFLAGS.None) | ((DateTimeFormatInfoScanner.ArrayElementsHaveSpace(monthNames) || DateTimeFormatInfoScanner.ArrayElementsHaveSpace(genitveMonthNames) || DateTimeFormatInfoScanner.ArrayElementsHaveSpace(abbrevMonthNames) || DateTimeFormatInfoScanner.ArrayElementsHaveSpace(genetiveAbbrevMonthNames)) ? FORMATFLAGS.UseSpacesInMonthNames : FORMATFLAGS.None);
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000B0A15 File Offset: 0x000AEC15
		internal static FORMATFLAGS GetFormatFlagUseSpaceInDayNames(string[] dayNames, string[] abbrevDayNames)
		{
			if (!DateTimeFormatInfoScanner.ArrayElementsHaveSpace(dayNames) && !DateTimeFormatInfoScanner.ArrayElementsHaveSpace(abbrevDayNames))
			{
				return FORMATFLAGS.None;
			}
			return FORMATFLAGS.UseSpacesInDayNames;
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000B0A2B File Offset: 0x000AEC2B
		internal static FORMATFLAGS GetFormatFlagUseHebrewCalendar(int calID)
		{
			if (calID != 8)
			{
				return FORMATFLAGS.None;
			}
			return (FORMATFLAGS)10;
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000B0A38 File Offset: 0x000AEC38
		private static bool EqualStringArrays(string[] array1, string[] array2)
		{
			if (array1 == array2)
			{
				return true;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (!array1[i].Equals(array2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x000B0A74 File Offset: 0x000AEC74
		private static bool ArrayElementsHaveSpace(string[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < array[i].Length; j++)
				{
					if (char.IsWhiteSpace(array[i][j]))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000B0AB8 File Offset: 0x000AECB8
		private static bool ArrayElementsBeginWithDigit(string[] array)
		{
			int i = 0;
			while (i < array.Length)
			{
				if (array[i].Length > 0 && array[i][0] >= '0' && array[i][0] <= '9')
				{
					int num = 1;
					while (num < array[i].Length && array[i][num] >= '0' && array[i][num] <= '9')
					{
						num++;
					}
					if (num == array[i].Length)
					{
						return false;
					}
					if (num == array[i].Length - 1)
					{
						char c = array[i][num];
						if (c == '月' || c == '월')
						{
							return false;
						}
					}
					return num != array[i].Length - 4 || array[i][num] != '\'' || array[i][num + 1] != ' ' || array[i][num + 2] != '月' || array[i][num + 3] != '\'';
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		// Token: 0x040019CB RID: 6603
		internal const char MonthPostfixChar = '\ue000';

		// Token: 0x040019CC RID: 6604
		internal const char IgnorableSymbolChar = '\ue001';

		// Token: 0x040019CD RID: 6605
		internal const string CJKYearSuff = "年";

		// Token: 0x040019CE RID: 6606
		internal const string CJKMonthSuff = "月";

		// Token: 0x040019CF RID: 6607
		internal const string CJKDaySuff = "日";

		// Token: 0x040019D0 RID: 6608
		internal const string KoreanYearSuff = "년";

		// Token: 0x040019D1 RID: 6609
		internal const string KoreanMonthSuff = "월";

		// Token: 0x040019D2 RID: 6610
		internal const string KoreanDaySuff = "일";

		// Token: 0x040019D3 RID: 6611
		internal const string KoreanHourSuff = "시";

		// Token: 0x040019D4 RID: 6612
		internal const string KoreanMinuteSuff = "분";

		// Token: 0x040019D5 RID: 6613
		internal const string KoreanSecondSuff = "초";

		// Token: 0x040019D6 RID: 6614
		internal const string CJKHourSuff = "時";

		// Token: 0x040019D7 RID: 6615
		internal const string ChineseHourSuff = "时";

		// Token: 0x040019D8 RID: 6616
		internal const string CJKMinuteSuff = "分";

		// Token: 0x040019D9 RID: 6617
		internal const string CJKSecondSuff = "秒";

		// Token: 0x040019DA RID: 6618
		internal List<string> m_dateWords = new List<string>();

		// Token: 0x040019DB RID: 6619
		private static volatile Dictionary<string, string> s_knownWords;

		// Token: 0x040019DC RID: 6620
		private DateTimeFormatInfoScanner.FoundDatePattern m_ymdFlags;

		// Token: 0x02000409 RID: 1033
		private enum FoundDatePattern
		{
			// Token: 0x040019DE RID: 6622
			None,
			// Token: 0x040019DF RID: 6623
			FoundYearPatternFlag,
			// Token: 0x040019E0 RID: 6624
			FoundMonthPatternFlag,
			// Token: 0x040019E1 RID: 6625
			FoundDayPatternFlag = 4,
			// Token: 0x040019E2 RID: 6626
			FoundYMDPatternFlag = 7
		}
	}
}
