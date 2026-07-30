using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>the Taiwan calendar.</summary>
	// Token: 0x02000425 RID: 1061
	[ComVisible(true)]
	[Serializable]
	public class TaiwanCalendar : Calendar
	{
		// Token: 0x060032D3 RID: 13011 RVA: 0x000B641F File Offset: 0x000B461F
		internal static Calendar GetDefaultInstance()
		{
			if (TaiwanCalendar.s_defaultInstance == null)
			{
				TaiwanCalendar.s_defaultInstance = new TaiwanCalendar();
			}
			return TaiwanCalendar.s_defaultInstance;
		}

		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.TaiwanCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.TaiwanCalendar" /> class, which is equivalent to the first moment of January 1, 1912 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x060032D4 RID: 13012 RVA: 0x000B643D File Offset: 0x000B463D
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return TaiwanCalendar.calendarMinValue;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.TaiwanCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.TaiwanCalendar" /> class, which is equivalent to the last moment of December 31, 9999 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x060032D5 RID: 13013 RVA: 0x000AB619 File Offset: 0x000A9819
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		/// <summary>Gets a value that indicates whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>Always returns <see cref="F:System.Globalization.CalendarAlgorithmType.SolarCalendar" />.</returns>
		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x060032D6 RID: 13014 RVA: 0x00003B29 File Offset: 0x00001D29
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.TaiwanCalendar" /> class.</summary>
		/// <exception cref="T:System.TypeInitializationException">Unable to initialize a <see cref="T:System.Globalization.TaiwanCalendar" /> object because of missing culture information.</exception>
		// Token: 0x060032D7 RID: 13015 RVA: 0x000B6444 File Offset: 0x000B4644
		public TaiwanCalendar()
		{
			try
			{
				new CultureInfo("zh-TW");
			}
			catch (ArgumentException ex)
			{
				throw new TypeInitializationException(base.GetType().FullName, ex);
			}
			this.helper = new GregorianCalendarHelper(this, TaiwanCalendar.taiwanEraInfo);
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x060032D8 RID: 13016 RVA: 0x00028BDC File Offset: 0x00026DDC
		internal override int ID
		{
			get
			{
				return 4;
			}
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of months away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of months to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add months. </param>
		/// <param name="months">The number of months to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120000.-or- <paramref name="months" /> is greater than 120000. </exception>
		// Token: 0x060032D9 RID: 13017 RVA: 0x000B6498 File Offset: 0x000B4698
		public override DateTime AddMonths(DateTime time, int months)
		{
			return this.helper.AddMonths(time, months);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add years. </param>
		/// <param name="years">The number of years to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		// Token: 0x060032DA RID: 13018 RVA: 0x000B64A7 File Offset: 0x000B46A7
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.helper.AddYears(time, years);
		}

		/// <summary>Returns the number of days in the specified month in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x060032DB RID: 13019 RVA: 0x000B64B6 File Offset: 0x000B46B6
		public override int GetDaysInMonth(int year, int month, int era)
		{
			return this.helper.GetDaysInMonth(year, month, era);
		}

		/// <summary>Returns the number of days in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x060032DC RID: 13020 RVA: 0x000B64C6 File Offset: 0x000B46C6
		public override int GetDaysInYear(int year, int era)
		{
			return this.helper.GetDaysInYear(year, era);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 31 that represents the day of the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x060032DD RID: 13021 RVA: 0x000B64D5 File Offset: 0x000B46D5
		public override int GetDayOfMonth(DateTime time)
		{
			return this.helper.GetDayOfMonth(time);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x060032DE RID: 13022 RVA: 0x000B64E3 File Offset: 0x000B46E3
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return this.helper.GetDayOfWeek(time);
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 366 that represents the day of the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x060032DF RID: 13023 RVA: 0x000B64F1 File Offset: 0x000B46F1
		public override int GetDayOfYear(DateTime time)
		{
			return this.helper.GetDayOfYear(time);
		}

		/// <summary>Returns the number of months in the specified year in the specified era.</summary>
		/// <returns>The number of months in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x060032E0 RID: 13024 RVA: 0x000B64FF File Offset: 0x000B46FF
		public override int GetMonthsInYear(int year, int era)
		{
			return this.helper.GetMonthsInYear(year, era);
		}

		/// <summary>Returns the week of the year that includes the date in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A positive integer that represents the week of the year that includes the date in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <param name="rule">One of the <see cref="T:System.Globalization.CalendarWeekRule" /> values that defines a calendar week. </param>
		/// <param name="firstDayOfWeek">One of the <see cref="T:System.DayOfWeek" /> values that represents the first day of the week. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> or <paramref name="firstDayOfWeek" /> is outside the range supported by the calendar.-or- <paramref name="rule" /> is not a valid <see cref="T:System.Globalization.CalendarWeekRule" /> value. </exception>
		// Token: 0x060032E1 RID: 13025 RVA: 0x000B650E File Offset: 0x000B470E
		[ComVisible(false)]
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return this.helper.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x060032E2 RID: 13026 RVA: 0x000B651E File Offset: 0x000B471E
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 12 that represents the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x060032E3 RID: 13027 RVA: 0x000B652C File Offset: 0x000B472C
		public override int GetMonth(DateTime time)
		{
			return this.helper.GetMonth(time);
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x060032E4 RID: 13028 RVA: 0x000B653A File Offset: 0x000B473A
		public override int GetYear(DateTime time)
		{
			return this.helper.GetYear(time);
		}

		/// <summary>Determines whether the specified date in the specified era is a leap day.</summary>
		/// <returns>true if the specified day is a leap day; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="day">An integer from 1 to 31 that represents the day. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x060032E5 RID: 13029 RVA: 0x000B6548 File Offset: 0x000B4748
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			return this.helper.IsLeapDay(year, month, day, era);
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <returns>true if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x060032E6 RID: 13030 RVA: 0x000B655A File Offset: 0x000B475A
		public override bool IsLeapYear(int year, int era)
		{
			return this.helper.IsLeapYear(year, era);
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <returns>The return value is always 0 because the <see cref="T:System.Globalization.TaiwanCalendar" /> class does not support the notion of a leap month.</returns>
		/// <param name="year">A year.</param>
		/// <param name="era">An era.</param>
		// Token: 0x060032E7 RID: 13031 RVA: 0x000B6569 File Offset: 0x000B4769
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return this.helper.GetLeapMonth(year, era);
		}

		/// <summary>Determines whether the specified month in the specified year in the specified era is a leap month.</summary>
		/// <returns>This method always returns false, unless overridden by a derived class.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x060032E8 RID: 13032 RVA: 0x000B6578 File Offset: 0x000B4778
		public override bool IsLeapMonth(int year, int month, int era)
		{
			return this.helper.IsLeapMonth(year, month, era);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date and time in the specified era.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="day">An integer from 1 to 31 that represents the day. </param>
		/// <param name="hour">An integer from 0 to 23 that represents the hour. </param>
		/// <param name="minute">An integer from 0 to 59 that represents the minute. </param>
		/// <param name="second">An integer from 0 to 59 that represents the second. </param>
		/// <param name="millisecond">An integer from 0 to 999 that represents the millisecond. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar.-or- <paramref name="hour" /> is less than zero or greater than 23.-or- <paramref name="minute" /> is less than zero or greater than 59.-or- <paramref name="second" /> is less than zero or greater than 59.-or- <paramref name="millisecond" /> is less than zero or greater than 999.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x060032E9 RID: 13033 RVA: 0x000B6588 File Offset: 0x000B4788
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this.helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.TaiwanCalendar" />.</summary>
		/// <returns>An array that consists of a single element for which the value is always the current era.</returns>
		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x060032EA RID: 13034 RVA: 0x000B65AD File Offset: 0x000B47AD
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified in a set operation is less than 99. -or- The value specified in a set operation is greater than MaxSupportedDateTime.Year.</exception>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the current instance is read-only.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x060032EB RID: 13035 RVA: 0x000B474F File Offset: 0x000B294F
		// (set) Token: 0x060032EC RID: 13036 RVA: 0x000B65BC File Offset: 0x000B47BC
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 99);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > this.helper.MaxYear)
				{
					throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 99, this.helper.MaxYear));
				}
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.TaiwanCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x060032ED RID: 13037 RVA: 0x000B6620 File Offset: 0x000B4820
		public override int ToFourDigitYear(int year)
		{
			if (year <= 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Positive number required."));
			}
			if (year > this.helper.MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, this.helper.MaxYear));
			}
			return year;
		}

		// Token: 0x04001AF0 RID: 6896
		internal static EraInfo[] taiwanEraInfo = new EraInfo[]
		{
			new EraInfo(1, 1912, 1, 1, 1911, 1, 8088)
		};

		// Token: 0x04001AF1 RID: 6897
		internal static volatile Calendar s_defaultInstance;

		// Token: 0x04001AF2 RID: 6898
		internal GregorianCalendarHelper helper;

		// Token: 0x04001AF3 RID: 6899
		internal static readonly DateTime calendarMinValue = new DateTime(1912, 1, 1);

		// Token: 0x04001AF4 RID: 6900
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 99;
	}
}
