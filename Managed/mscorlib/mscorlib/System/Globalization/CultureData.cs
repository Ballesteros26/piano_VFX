using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Globalization
{
	// Token: 0x0200043C RID: 1084
	[StructLayout(LayoutKind.Sequential)]
	internal class CultureData
	{
		// Token: 0x060033D3 RID: 13267 RVA: 0x000BB211 File Offset: 0x000B9411
		private CultureData(string name)
		{
			this.sRealName = name;
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060033D4 RID: 13268 RVA: 0x000BB220 File Offset: 0x000B9420
		public static CultureData Invariant
		{
			get
			{
				if (CultureData.s_Invariant == null)
				{
					CultureData cultureData = new CultureData("");
					cultureData.sISO639Language = "iv";
					cultureData.sAM1159 = "AM";
					cultureData.sPM2359 = "PM";
					cultureData.sTimeSeparator = ":";
					cultureData.saLongTimes = new string[] { "HH:mm:ss" };
					cultureData.saShortTimes = new string[] { "HH:mm", "hh:mm tt", "H:mm", "h:mm tt" };
					cultureData.iFirstDayOfWeek = 0;
					cultureData.iFirstWeekOfYear = 0;
					cultureData.waCalendars = new int[] { 1 };
					cultureData.calendars = new CalendarData[23];
					cultureData.calendars[0] = CalendarData.Invariant;
					cultureData.iDefaultAnsiCodePage = 1252;
					cultureData.iDefaultOemCodePage = 437;
					cultureData.iDefaultMacCodePage = 10000;
					cultureData.iDefaultEbcdicCodePage = 37;
					cultureData.sListSeparator = ",";
					Interlocked.CompareExchange<CultureData>(ref CultureData.s_Invariant, cultureData, null);
				}
				return CultureData.s_Invariant;
			}
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x000BB334 File Offset: 0x000B9534
		public static CultureData GetCultureData(string cultureName, bool useUserOverride)
		{
			CultureData cultureData;
			try
			{
				cultureData = new CultureInfo(cultureName, useUserOverride).m_cultureData;
			}
			catch
			{
				cultureData = null;
			}
			return cultureData;
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x000BB368 File Offset: 0x000B9568
		public static CultureData GetCultureData(string cultureName, bool useUserOverride, int datetimeIndex, int calendarId, int numberIndex, string iso2lang, int ansiCodePage, int oemCodePage, int macCodePage, int ebcdicCodePage, bool rightToLeft, string listSeparator)
		{
			if (string.IsNullOrEmpty(cultureName))
			{
				return CultureData.Invariant;
			}
			CultureData cultureData = new CultureData(cultureName);
			cultureData.fill_culture_data(datetimeIndex);
			cultureData.bUseOverrides = useUserOverride;
			cultureData.calendarId = calendarId;
			cultureData.numberIndex = numberIndex;
			cultureData.sISO639Language = iso2lang;
			cultureData.iDefaultAnsiCodePage = ansiCodePage;
			cultureData.iDefaultOemCodePage = oemCodePage;
			cultureData.iDefaultMacCodePage = macCodePage;
			cultureData.iDefaultEbcdicCodePage = ebcdicCodePage;
			cultureData.isRightToLeft = rightToLeft;
			cultureData.sListSeparator = listSeparator;
			return cultureData;
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x0000A42E File Offset: 0x0000862E
		internal static CultureData GetCultureData(int culture, bool bUseUserOverride)
		{
			return null;
		}

		// Token: 0x060033D8 RID: 13272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void fill_culture_data(int datetimeIndex);

		// Token: 0x060033D9 RID: 13273 RVA: 0x000BB3E0 File Offset: 0x000B95E0
		public CalendarData GetCalendar(int calendarId)
		{
			int num = calendarId - 1;
			if (this.calendars == null)
			{
				this.calendars = new CalendarData[23];
			}
			CalendarData calendarData = this.calendars[num];
			if (calendarData == null)
			{
				calendarData = new CalendarData(this.sRealName, calendarId, this.bUseOverrides);
				this.calendars[num] = calendarData;
			}
			return calendarData;
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060033DA RID: 13274 RVA: 0x000BB42F File Offset: 0x000B962F
		internal string[] LongTimes
		{
			get
			{
				return this.saLongTimes;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060033DB RID: 13275 RVA: 0x000BB439 File Offset: 0x000B9639
		internal string[] ShortTimes
		{
			get
			{
				return this.saShortTimes;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x060033DC RID: 13276 RVA: 0x000BB443 File Offset: 0x000B9643
		internal string SISO639LANGNAME
		{
			get
			{
				return this.sISO639Language;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x060033DD RID: 13277 RVA: 0x000BB44B File Offset: 0x000B964B
		internal int IFIRSTDAYOFWEEK
		{
			get
			{
				return this.iFirstDayOfWeek;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x060033DE RID: 13278 RVA: 0x000BB453 File Offset: 0x000B9653
		internal int IFIRSTWEEKOFYEAR
		{
			get
			{
				return this.iFirstWeekOfYear;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x060033DF RID: 13279 RVA: 0x000BB45B File Offset: 0x000B965B
		internal string SAM1159
		{
			get
			{
				return this.sAM1159;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x060033E0 RID: 13280 RVA: 0x000BB463 File Offset: 0x000B9663
		internal string SPM2359
		{
			get
			{
				return this.sPM2359;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x000BB46B File Offset: 0x000B966B
		internal string TimeSeparator
		{
			get
			{
				return this.sTimeSeparator;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x060033E2 RID: 13282 RVA: 0x000BB474 File Offset: 0x000B9674
		internal int[] CalendarIds
		{
			get
			{
				if (this.waCalendars == null)
				{
					string text = this.sISO639Language;
					if (!(text == "ja"))
					{
						if (!(text == "zh"))
						{
							this.waCalendars = new int[] { this.calendarId };
						}
						else
						{
							this.waCalendars = new int[] { this.calendarId, 4 };
						}
					}
					else
					{
						this.waCalendars = new int[] { this.calendarId, 3 };
					}
				}
				return this.waCalendars;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x000BB507 File Offset: 0x000B9707
		internal bool IsInvariantCulture
		{
			get
			{
				return string.IsNullOrEmpty(this.sRealName);
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x060033E4 RID: 13284 RVA: 0x000BB514 File Offset: 0x000B9714
		internal string CultureName
		{
			get
			{
				return this.sRealName;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x060033E5 RID: 13285 RVA: 0x000604AD File Offset: 0x0005E6AD
		internal string SCOMPAREINFO
		{
			get
			{
				return "";
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x060033E6 RID: 13286 RVA: 0x000BB514 File Offset: 0x000B9714
		internal string STEXTINFO
		{
			get
			{
				return this.sRealName;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x060033E7 RID: 13287 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal int ILANGUAGE
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x060033E8 RID: 13288 RVA: 0x000BB51C File Offset: 0x000B971C
		internal int IDEFAULTANSICODEPAGE
		{
			get
			{
				return this.iDefaultAnsiCodePage;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x060033E9 RID: 13289 RVA: 0x000BB524 File Offset: 0x000B9724
		internal int IDEFAULTOEMCODEPAGE
		{
			get
			{
				return this.iDefaultOemCodePage;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x060033EA RID: 13290 RVA: 0x000BB52C File Offset: 0x000B972C
		internal int IDEFAULTMACCODEPAGE
		{
			get
			{
				return this.iDefaultMacCodePage;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x060033EB RID: 13291 RVA: 0x000BB534 File Offset: 0x000B9734
		internal int IDEFAULTEBCDICCODEPAGE
		{
			get
			{
				return this.iDefaultEbcdicCodePage;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x060033EC RID: 13292 RVA: 0x000BB53C File Offset: 0x000B973C
		internal bool IsRightToLeft
		{
			get
			{
				return this.isRightToLeft;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x060033ED RID: 13293 RVA: 0x000BB544 File Offset: 0x000B9744
		internal string SLIST
		{
			get
			{
				return this.sListSeparator;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x060033EE RID: 13294 RVA: 0x000BB54C File Offset: 0x000B974C
		internal bool UseUserOverride
		{
			get
			{
				return this.bUseOverrides;
			}
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x000BB554 File Offset: 0x000B9754
		internal string CalendarName(int calendarId)
		{
			return this.GetCalendar(calendarId).sNativeName;
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x000BB562 File Offset: 0x000B9762
		internal string[] EraNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saEraNames;
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x000BB570 File Offset: 0x000B9770
		internal string[] AbbrevEraNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevEraNames;
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000BB57E File Offset: 0x000B977E
		internal string[] AbbreviatedEnglishEraNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevEnglishEraNames;
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000BB58C File Offset: 0x000B978C
		internal string[] ShortDates(int calendarId)
		{
			return this.GetCalendar(calendarId).saShortDates;
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000BB59A File Offset: 0x000B979A
		internal string[] LongDates(int calendarId)
		{
			return this.GetCalendar(calendarId).saLongDates;
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000BB5A8 File Offset: 0x000B97A8
		internal string[] YearMonths(int calendarId)
		{
			return this.GetCalendar(calendarId).saYearMonths;
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000BB5B6 File Offset: 0x000B97B6
		internal string[] DayNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saDayNames;
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000BB5C4 File Offset: 0x000B97C4
		internal string[] AbbreviatedDayNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevDayNames;
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000BB5D2 File Offset: 0x000B97D2
		internal string[] SuperShortDayNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saSuperShortDayNames;
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000BB5E0 File Offset: 0x000B97E0
		internal string[] MonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saMonthNames;
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000BB5EE File Offset: 0x000B97EE
		internal string[] GenitiveMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saMonthGenitiveNames;
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000BB5FC File Offset: 0x000B97FC
		internal string[] AbbreviatedMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevMonthNames;
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x000BB60A File Offset: 0x000B980A
		internal string[] AbbreviatedGenitiveMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevMonthGenitiveNames;
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000BB618 File Offset: 0x000B9818
		internal string[] LeapYearMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saLeapYearMonthNames;
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x000BB626 File Offset: 0x000B9826
		internal string MonthDay(int calendarId)
		{
			return this.GetCalendar(calendarId).sMonthDay;
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x000BB634 File Offset: 0x000B9834
		internal string DateSeparator(int calendarId)
		{
			return CultureData.GetDateSeparator(this.ShortDates(calendarId)[0]);
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x000BB644 File Offset: 0x000B9844
		private static string GetDateSeparator(string format)
		{
			return CultureData.GetSeparator(format, "dyM");
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x000BB654 File Offset: 0x000B9854
		private static string GetSeparator(string format, string timeParts)
		{
			int num = CultureData.IndexOfTimePart(format, 0, timeParts);
			if (num != -1)
			{
				char c = format[num];
				do
				{
					num++;
				}
				while (num < format.Length && format[num] == c);
				int num2 = num;
				if (num2 < format.Length)
				{
					int num3 = CultureData.IndexOfTimePart(format, num2, timeParts);
					if (num3 != -1)
					{
						return CultureData.UnescapeNlsString(format, num2, num3 - 1);
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x000BB6B8 File Offset: 0x000B98B8
		private static int IndexOfTimePart(string format, int startIndex, string timeParts)
		{
			bool flag = false;
			for (int i = startIndex; i < format.Length; i++)
			{
				if (!flag && timeParts.IndexOf(format[i]) != -1)
				{
					return i;
				}
				char c = format[i];
				if (c != '\'')
				{
					if (c == '\\' && i + 1 < format.Length)
					{
						i++;
						c = format[i];
						if (c != '\'' && c != '\\')
						{
							i--;
						}
					}
				}
				else
				{
					flag = !flag;
				}
			}
			return -1;
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000BB72C File Offset: 0x000B992C
		private static string UnescapeNlsString(string str, int start, int end)
		{
			StringBuilder stringBuilder = null;
			int num = start;
			while (num < str.Length && num <= end)
			{
				char c = str[num];
				if (c != '\'')
				{
					if (c != '\\')
					{
						if (stringBuilder != null)
						{
							stringBuilder.Append(str[num]);
						}
					}
					else
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(str, start, num - start, str.Length);
						}
						num++;
						if (num < str.Length)
						{
							stringBuilder.Append(str[num]);
						}
					}
				}
				else if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(str, start, num - start, str.Length);
				}
				num++;
			}
			if (stringBuilder == null)
			{
				return str.Substring(start, end - start + 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x00002119 File Offset: 0x00000319
		internal static string[] ReescapeWin32Strings(string[] array)
		{
			return array;
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x00002119 File Offset: 0x00000319
		internal static string ReescapeWin32String(string str)
		{
			return str;
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal static bool IsCustomCultureId(int cultureId)
		{
			return false;
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x000BB7D4 File Offset: 0x000B99D4
		internal void GetNFIValues(NumberFormatInfo nfi)
		{
			if (!this.IsInvariantCulture)
			{
				CultureData.fill_number_data(nfi, this.numberIndex);
			}
			nfi.percentDecimalDigits = nfi.numberDecimalDigits;
			nfi.percentDecimalSeparator = nfi.numberDecimalSeparator;
			nfi.percentGroupSizes = nfi.numberGroupSizes;
			nfi.percentGroupSeparator = nfi.numberGroupSeparator;
		}

		// Token: 0x06003408 RID: 13320
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void fill_number_data(NumberFormatInfo nfi, int numberIndex);

		// Token: 0x04001B98 RID: 7064
		private string sAM1159;

		// Token: 0x04001B99 RID: 7065
		private string sPM2359;

		// Token: 0x04001B9A RID: 7066
		private string sTimeSeparator;

		// Token: 0x04001B9B RID: 7067
		private volatile string[] saLongTimes;

		// Token: 0x04001B9C RID: 7068
		private volatile string[] saShortTimes;

		// Token: 0x04001B9D RID: 7069
		private int iFirstDayOfWeek;

		// Token: 0x04001B9E RID: 7070
		private int iFirstWeekOfYear;

		// Token: 0x04001B9F RID: 7071
		private volatile int[] waCalendars;

		// Token: 0x04001BA0 RID: 7072
		private CalendarData[] calendars;

		// Token: 0x04001BA1 RID: 7073
		private string sISO639Language;

		// Token: 0x04001BA2 RID: 7074
		private readonly string sRealName;

		// Token: 0x04001BA3 RID: 7075
		private bool bUseOverrides;

		// Token: 0x04001BA4 RID: 7076
		private int calendarId;

		// Token: 0x04001BA5 RID: 7077
		private int numberIndex;

		// Token: 0x04001BA6 RID: 7078
		private int iDefaultAnsiCodePage;

		// Token: 0x04001BA7 RID: 7079
		private int iDefaultOemCodePage;

		// Token: 0x04001BA8 RID: 7080
		private int iDefaultMacCodePage;

		// Token: 0x04001BA9 RID: 7081
		private int iDefaultEbcdicCodePage;

		// Token: 0x04001BAA RID: 7082
		private bool isRightToLeft;

		// Token: 0x04001BAB RID: 7083
		private string sListSeparator;

		// Token: 0x04001BAC RID: 7084
		private static CultureData s_Invariant;
	}
}
