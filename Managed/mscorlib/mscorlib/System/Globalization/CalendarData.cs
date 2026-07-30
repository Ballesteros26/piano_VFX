using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020003FB RID: 1019
	[StructLayout(LayoutKind.Sequential)]
	internal class CalendarData
	{
		// Token: 0x06003022 RID: 12322 RVA: 0x000ABC7B File Offset: 0x000A9E7B
		private CalendarData()
		{
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000ABC90 File Offset: 0x000A9E90
		static CalendarData()
		{
			CalendarData calendarData = new CalendarData();
			calendarData.sNativeName = "Gregorian Calendar";
			calendarData.iTwoDigitYearMax = 2029;
			calendarData.iCurrentEra = 1;
			calendarData.saShortDates = new string[] { "MM/dd/yyyy", "yyyy-MM-dd" };
			calendarData.saLongDates = new string[] { "dddd, dd MMMM yyyy" };
			calendarData.saYearMonths = new string[] { "yyyy MMMM" };
			calendarData.sMonthDay = "MMMM dd";
			calendarData.saEraNames = new string[] { "A.D." };
			calendarData.saAbbrevEraNames = new string[] { "AD" };
			calendarData.saAbbrevEnglishEraNames = new string[] { "AD" };
			calendarData.saDayNames = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
			calendarData.saAbbrevDayNames = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
			calendarData.saSuperShortDayNames = new string[] { "Su", "Mo", "Tu", "We", "Th", "Fr", "Sa" };
			calendarData.saMonthNames = new string[]
			{
				"January",
				"February",
				"March",
				"April",
				"May",
				"June",
				"July",
				"August",
				"September",
				"October",
				"November",
				"December",
				string.Empty
			};
			calendarData.saAbbrevMonthNames = new string[]
			{
				"Jan",
				"Feb",
				"Mar",
				"Apr",
				"May",
				"Jun",
				"Jul",
				"Aug",
				"Sep",
				"Oct",
				"Nov",
				"Dec",
				string.Empty
			};
			calendarData.saMonthGenitiveNames = calendarData.saMonthNames;
			calendarData.saAbbrevMonthGenitiveNames = calendarData.saAbbrevMonthNames;
			calendarData.saLeapYearMonthNames = calendarData.saMonthNames;
			calendarData.bUseUserOverrides = false;
			CalendarData.Invariant = calendarData;
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000ABF3C File Offset: 0x000AA13C
		internal CalendarData(string localeName, int calendarId, bool bUseUserOverrides)
		{
			this.bUseUserOverrides = bUseUserOverrides;
			if (!CalendarData.nativeGetCalendarData(this, localeName, calendarId))
			{
				if (this.sNativeName == null)
				{
					this.sNativeName = string.Empty;
				}
				if (this.saShortDates == null)
				{
					this.saShortDates = CalendarData.Invariant.saShortDates;
				}
				if (this.saYearMonths == null)
				{
					this.saYearMonths = CalendarData.Invariant.saYearMonths;
				}
				if (this.saLongDates == null)
				{
					this.saLongDates = CalendarData.Invariant.saLongDates;
				}
				if (this.sMonthDay == null)
				{
					this.sMonthDay = CalendarData.Invariant.sMonthDay;
				}
				if (this.saEraNames == null)
				{
					this.saEraNames = CalendarData.Invariant.saEraNames;
				}
				if (this.saAbbrevEraNames == null)
				{
					this.saAbbrevEraNames = CalendarData.Invariant.saAbbrevEraNames;
				}
				if (this.saAbbrevEnglishEraNames == null)
				{
					this.saAbbrevEnglishEraNames = CalendarData.Invariant.saAbbrevEnglishEraNames;
				}
				if (this.saDayNames == null)
				{
					this.saDayNames = CalendarData.Invariant.saDayNames;
				}
				if (this.saAbbrevDayNames == null)
				{
					this.saAbbrevDayNames = CalendarData.Invariant.saAbbrevDayNames;
				}
				if (this.saSuperShortDayNames == null)
				{
					this.saSuperShortDayNames = CalendarData.Invariant.saSuperShortDayNames;
				}
				if (this.saMonthNames == null)
				{
					this.saMonthNames = CalendarData.Invariant.saMonthNames;
				}
				if (this.saAbbrevMonthNames == null)
				{
					this.saAbbrevMonthNames = CalendarData.Invariant.saAbbrevMonthNames;
				}
			}
			this.saShortDates = CultureData.ReescapeWin32Strings(this.saShortDates);
			this.saLongDates = CultureData.ReescapeWin32Strings(this.saLongDates);
			this.saYearMonths = CultureData.ReescapeWin32Strings(this.saYearMonths);
			this.sMonthDay = CultureData.ReescapeWin32String(this.sMonthDay);
			if ((ushort)calendarId == 4)
			{
				if (CultureInfo.IsTaiwanSku)
				{
					this.sNativeName = "中華民國曆";
				}
				else
				{
					this.sNativeName = string.Empty;
				}
			}
			if (this.saMonthGenitiveNames == null || string.IsNullOrEmpty(this.saMonthGenitiveNames[0]))
			{
				this.saMonthGenitiveNames = this.saMonthNames;
			}
			if (this.saAbbrevMonthGenitiveNames == null || string.IsNullOrEmpty(this.saAbbrevMonthGenitiveNames[0]))
			{
				this.saAbbrevMonthGenitiveNames = this.saAbbrevMonthNames;
			}
			if (this.saLeapYearMonthNames == null || string.IsNullOrEmpty(this.saLeapYearMonthNames[0]))
			{
				this.saLeapYearMonthNames = this.saMonthNames;
			}
			this.InitializeEraNames(localeName, calendarId);
			this.InitializeAbbreviatedEraNames(localeName, calendarId);
			if (calendarId == 3)
			{
				this.saAbbrevEnglishEraNames = JapaneseCalendar.EnglishEraNames();
			}
			else
			{
				this.saAbbrevEnglishEraNames = new string[] { "" };
			}
			this.iCurrentEra = this.saEraNames.Length;
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000AC1B8 File Offset: 0x000AA3B8
		private void InitializeEraNames(string localeName, int calendarId)
		{
			switch ((ushort)calendarId)
			{
			case 1:
				if (this.saEraNames == null || this.saEraNames.Length == 0 || string.IsNullOrEmpty(this.saEraNames[0]))
				{
					this.saEraNames = new string[] { "A.D." };
					return;
				}
				return;
			case 2:
			case 13:
				this.saEraNames = new string[] { "A.D." };
				return;
			case 3:
			case 14:
				this.saEraNames = JapaneseCalendar.EraNames();
				return;
			case 4:
				if (CultureInfo.IsTaiwanSku)
				{
					this.saEraNames = new string[] { "中華民國" };
					return;
				}
				this.saEraNames = new string[] { string.Empty };
				return;
			case 5:
				this.saEraNames = new string[] { "단기" };
				return;
			case 6:
			case 23:
				if (localeName == "dv-MV")
				{
					this.saEraNames = new string[] { "ހ\u07a8ޖ\u07b0ރ\u07a9" };
					return;
				}
				this.saEraNames = new string[] { "بعد الهجرة" };
				return;
			case 7:
				this.saEraNames = new string[] { "พ.ศ." };
				return;
			case 8:
				this.saEraNames = new string[] { "C.E." };
				return;
			case 9:
				this.saEraNames = new string[] { "ap. J.-C." };
				return;
			case 10:
			case 11:
			case 12:
				this.saEraNames = new string[] { "م" };
				return;
			case 22:
				if (this.saEraNames == null || this.saEraNames.Length == 0 || string.IsNullOrEmpty(this.saEraNames[0]))
				{
					this.saEraNames = new string[] { "ه.ش" };
					return;
				}
				return;
			}
			this.saEraNames = CalendarData.Invariant.saEraNames;
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000AC3A0 File Offset: 0x000AA5A0
		private void InitializeAbbreviatedEraNames(string localeName, int calendarId)
		{
			CalendarId calendarId2 = (CalendarId)calendarId;
			if (calendarId2 <= CalendarId.JULIAN)
			{
				switch (calendarId2)
				{
				case CalendarId.GREGORIAN:
					if (this.saAbbrevEraNames == null || this.saAbbrevEraNames.Length == 0 || string.IsNullOrEmpty(this.saAbbrevEraNames[0]))
					{
						this.saAbbrevEraNames = new string[] { "AD" };
						return;
					}
					return;
				case CalendarId.GREGORIAN_US:
					break;
				case CalendarId.JAPAN:
					goto IL_0096;
				case CalendarId.TAIWAN:
					this.saAbbrevEraNames = new string[1];
					if (this.saEraNames[0].Length == 4)
					{
						this.saAbbrevEraNames[0] = this.saEraNames[0].Substring(2, 2);
						return;
					}
					this.saAbbrevEraNames[0] = this.saEraNames[0];
					return;
				case CalendarId.KOREA:
					goto IL_014B;
				case CalendarId.HIJRI:
					goto IL_00A2;
				default:
					if (calendarId2 != CalendarId.JULIAN)
					{
						goto IL_014B;
					}
					break;
				}
				this.saAbbrevEraNames = new string[] { "AD" };
				return;
			}
			if (calendarId2 != CalendarId.JAPANESELUNISOLAR)
			{
				if (calendarId2 != CalendarId.PERSIAN)
				{
					if (calendarId2 != CalendarId.UMALQURA)
					{
						goto IL_014B;
					}
					goto IL_00A2;
				}
				else
				{
					if (this.saAbbrevEraNames == null || this.saAbbrevEraNames.Length == 0 || string.IsNullOrEmpty(this.saAbbrevEraNames[0]))
					{
						this.saAbbrevEraNames = this.saEraNames;
						return;
					}
					return;
				}
			}
			IL_0096:
			this.saAbbrevEraNames = JapaneseCalendar.AbbrevEraNames();
			return;
			IL_00A2:
			if (localeName == "dv-MV")
			{
				this.saAbbrevEraNames = new string[] { "ހ." };
				return;
			}
			this.saAbbrevEraNames = new string[] { "هـ" };
			return;
			IL_014B:
			this.saAbbrevEraNames = this.saEraNames;
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000AC504 File Offset: 0x000AA704
		internal static CalendarData GetCalendarData(int calendarId)
		{
			return CultureInfo.GetCultureInfo(CalendarData.CalendarIdToCultureName(calendarId)).m_cultureData.GetCalendar(calendarId);
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000AC51C File Offset: 0x000AA71C
		private static string CalendarIdToCultureName(int calendarId)
		{
			switch (calendarId)
			{
			case 2:
				return "fa-IR";
			case 3:
				return "ja-JP";
			case 4:
				return "zh-TW";
			case 5:
				return "ko-KR";
			case 6:
			case 10:
			case 23:
				return "ar-SA";
			case 7:
				return "th-TH";
			case 8:
				return "he-IL";
			case 9:
				return "ar-DZ";
			case 11:
			case 12:
				return "ar-IQ";
			}
			return "en-US";
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x0009B163 File Offset: 0x00099363
		public static int nativeGetTwoDigitYearMax(int calID)
		{
			return -1;
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x000AC5C6 File Offset: 0x000AA7C6
		private static bool nativeGetCalendarData(CalendarData data, string localeName, int calendarId)
		{
			return data.fill_calendar_data(localeName.ToLowerInvariant(), calendarId);
		}

		// Token: 0x0600302B RID: 12331
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool fill_calendar_data(string localeName, int datetimeIndex);

		// Token: 0x040018F3 RID: 6387
		internal const int MAX_CALENDARS = 23;

		// Token: 0x040018F4 RID: 6388
		internal string sNativeName;

		// Token: 0x040018F5 RID: 6389
		internal string[] saShortDates;

		// Token: 0x040018F6 RID: 6390
		internal string[] saYearMonths;

		// Token: 0x040018F7 RID: 6391
		internal string[] saLongDates;

		// Token: 0x040018F8 RID: 6392
		internal string sMonthDay;

		// Token: 0x040018F9 RID: 6393
		internal string[] saEraNames;

		// Token: 0x040018FA RID: 6394
		internal string[] saAbbrevEraNames;

		// Token: 0x040018FB RID: 6395
		internal string[] saAbbrevEnglishEraNames;

		// Token: 0x040018FC RID: 6396
		internal string[] saDayNames;

		// Token: 0x040018FD RID: 6397
		internal string[] saAbbrevDayNames;

		// Token: 0x040018FE RID: 6398
		internal string[] saSuperShortDayNames;

		// Token: 0x040018FF RID: 6399
		internal string[] saMonthNames;

		// Token: 0x04001900 RID: 6400
		internal string[] saAbbrevMonthNames;

		// Token: 0x04001901 RID: 6401
		internal string[] saMonthGenitiveNames;

		// Token: 0x04001902 RID: 6402
		internal string[] saAbbrevMonthGenitiveNames;

		// Token: 0x04001903 RID: 6403
		internal string[] saLeapYearMonthNames;

		// Token: 0x04001904 RID: 6404
		internal int iTwoDigitYearMax = 2029;

		// Token: 0x04001905 RID: 6405
		internal int iCurrentEra;

		// Token: 0x04001906 RID: 6406
		internal bool bUseUserOverrides;

		// Token: 0x04001907 RID: 6407
		internal static CalendarData Invariant;
	}
}
