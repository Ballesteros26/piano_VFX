using System;

namespace System.Globalization
{
	/// <summary>Represents the Persian calendar.</summary>
	// Token: 0x020003F7 RID: 1015
	[Serializable]
	public class PersianCalendar : Calendar
	{
		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.PersianCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.PersianCalendar" /> class, which is equivalent to the first moment of March 21, 622 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x000AAEEE File Offset: 0x000A90EE
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return PersianCalendar.minDate;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.PersianCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.PersianCalendar" /> class, which is equivalent to the last moment of December 31, 9999 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x000AAEF5 File Offset: 0x000A90F5
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return PersianCalendar.maxDate;
			}
		}

		/// <summary>Gets a value indicating whether the current calendar is solar-based, lunar-based, or lunisolar-based.</summary>
		/// <returns>Always returns <see cref="F:System.Globalization.CalendarAlgorithmType.SolarCalendar" />.</returns>
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002FC2 RID: 12226 RVA: 0x00003B29 File Offset: 0x00001D29
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002FC4 RID: 12228 RVA: 0x00003B29 File Offset: 0x00001D29
		internal override int BaseCalendarID
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06002FC5 RID: 12229 RVA: 0x000AAF04 File Offset: 0x000A9104
		internal override int ID
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x000AAF08 File Offset: 0x000A9108
		private long GetAbsoluteDatePersian(int year, int month, int day)
		{
			if (year >= 1 && year <= 9378 && month >= 1 && month <= 12)
			{
				int num = PersianCalendar.DaysInPreviousMonths(month) + day - 1;
				int num2 = (int)(365.242189 * (double)(year - 1));
				return CalendricalCalculationsHelper.PersianNewYearOnOrBefore(PersianCalendar.PersianEpoch + (long)num2 + 180L) + (long)num;
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Year, Month, and Day parameters describe an un-representable DateTime."));
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x000AAF70 File Offset: 0x000A9170
		internal static void CheckTicksRange(long ticks)
		{
			if (ticks < PersianCalendar.minDate.Ticks || ticks > PersianCalendar.maxDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Specified time is not supported in this calendar. It should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive."), PersianCalendar.minDate, PersianCalendar.maxDate));
			}
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000AAFCA File Offset: 0x000A91CA
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != PersianCalendar.PersianEra)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000AAFEC File Offset: 0x000A91EC
		internal static void CheckYearRange(int year, int era)
		{
			PersianCalendar.CheckEraRange(era);
			if (year < 1 || year > 9378)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 9378));
			}
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000AB03C File Offset: 0x000A923C
		internal static void CheckYearMonthRange(int year, int month, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378 && month > 10)
			{
				throw new ArgumentOutOfRangeException("month", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 10));
			}
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Month must be between one and twelve."));
			}
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x000AB0A8 File Offset: 0x000A92A8
		private static int MonthFromOrdinalDay(int ordinalDay)
		{
			int num = 0;
			while (ordinalDay > PersianCalendar.DaysToMonth[num])
			{
				num++;
			}
			return num;
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x000AB0C8 File Offset: 0x000A92C8
		private static int DaysInPreviousMonths(int month)
		{
			month--;
			return PersianCalendar.DaysToMonth[month];
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x000AB0D8 File Offset: 0x000A92D8
		internal int GetDatePart(long ticks, int part)
		{
			PersianCalendar.CheckTicksRange(ticks);
			long num = ticks / 864000000000L + 1L;
			int num2 = (int)Math.Floor((double)(CalendricalCalculationsHelper.PersianNewYearOnOrBefore(num) - PersianCalendar.PersianEpoch) / 365.242189 + 0.5) + 1;
			if (part == 0)
			{
				return num2;
			}
			int num3 = (int)(num - CalendricalCalculationsHelper.GetNumberOfDays(this.ToDateTime(num2, 1, 1, 0, 0, 0, 0, 1)));
			if (part == 1)
			{
				return num3;
			}
			int num4 = PersianCalendar.MonthFromOrdinalDay(num3);
			if (part == 2)
			{
				return num4;
			}
			int num5 = num3 - PersianCalendar.DaysInPreviousMonths(num4);
			if (part == 3)
			{
				return num5;
			}
			throw new InvalidOperationException(Environment.GetResourceString("Internal Error in DateTime and Calendar operations."));
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> object that is offset the specified number of months from the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that represents the date yielded by adding the number of months specified by the <paramref name="months" /> parameter to the date specified by the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add months. </param>
		/// <param name="months">The positive or negative number of months to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120,000 or greater than 120,000. </exception>
		// Token: 0x06002FCE RID: 12238 RVA: 0x000AB174 File Offset: 0x000A9374
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), -120000, 120000));
			}
			int num = this.GetDatePart(time.Ticks, 0);
			int num2 = this.GetDatePart(time.Ticks, 2);
			int num3 = this.GetDatePart(time.Ticks, 3);
			int num4 = num2 - 1 + months;
			if (num4 >= 0)
			{
				num2 = num4 % 12 + 1;
				num += num4 / 12;
			}
			else
			{
				num2 = 12 + (num4 + 1) % 12;
				num += (num4 - 11) / 12;
			}
			int daysInMonth = this.GetDaysInMonth(num, num2);
			if (num3 > daysInMonth)
			{
				num3 = daysInMonth;
			}
			long num5 = this.GetAbsoluteDatePersian(num, num2, num3) * 864000000000L + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(num5, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(num5);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> object that is offset the specified number of years from the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> object that results from adding the specified number of years to the specified <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add years. </param>
		/// <param name="years">The positive or negative number of years to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="years" /> is less than -10,000 or greater than 10,000. </exception>
		// Token: 0x06002FCF RID: 12239 RVA: 0x000AB26D File Offset: 0x000A946D
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>An integer from 1 through 31 that represents the day of the month in the specified <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="time" /> parameter represents a date less than <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x06002FD0 RID: 12240 RVA: 0x000AB27A File Offset: 0x000A947A
		public override int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the specified <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06002FD1 RID: 12241 RVA: 0x000AB28A File Offset: 0x000A948A
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>An integer from 1 through 366 that represents the day of the year in the specified <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="time" /> parameter represents a date less than <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x06002FD2 RID: 12242 RVA: 0x000AB2A3 File Offset: 0x000A94A3
		public override int GetDayOfYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 1);
		}

		/// <summary>Returns the number of days in the specified month of the specified year and era.</summary>
		/// <returns>The number of days in the specified month of the specified year and era.</returns>
		/// <param name="year">An integer from 1 through 9378 that represents the year. </param>
		/// <param name="month">An integer that represents the month, and ranges from 1 through 12 if <paramref name="year" /> is not 9378, or 1 through 10 if <paramref name="year" /> is 9378.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06002FD3 RID: 12243 RVA: 0x000AB2B4 File Offset: 0x000A94B4
		public override int GetDaysInMonth(int year, int month, int era)
		{
			PersianCalendar.CheckYearMonthRange(year, month, era);
			if (month == 10 && year == 9378)
			{
				return 13;
			}
			int num = PersianCalendar.DaysToMonth[month] - PersianCalendar.DaysToMonth[month - 1];
			if (month == 12 && !this.IsLeapYear(year))
			{
				num--;
			}
			return num;
		}

		/// <summary>Returns the number of days in the specified year of the specified era.</summary>
		/// <returns>The number of days in the specified year and era. The number of days is 365 in a common year or 366 in a leap year.</returns>
		/// <param name="year">An integer from 1 through 9378 that represents the year. </param>
		/// <param name="era">An integer from 0 through 1 that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06002FD4 RID: 12244 RVA: 0x000AB2FE File Offset: 0x000A94FE
		public override int GetDaysInYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378)
			{
				return PersianCalendar.DaysToMonth[9] + 13;
			}
			if (!this.IsLeapYear(year, 0))
			{
				return 365;
			}
			return 366;
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>Always returns <see cref="F:System.Globalization.PersianCalendar.PersianEra" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="time" /> parameter represents a date less than <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x06002FD5 RID: 12245 RVA: 0x000AB330 File Offset: 0x000A9530
		public override int GetEra(DateTime time)
		{
			PersianCalendar.CheckTicksRange(time.Ticks);
			return PersianCalendar.PersianEra;
		}

		/// <summary>Gets the list of eras in a <see cref="T:System.Globalization.PersianCalendar" /> object.</summary>
		/// <returns>An array of integers that represents the eras in a <see cref="T:System.Globalization.PersianCalendar" /> object. The array consists of a single element having a value of <see cref="F:System.Globalization.PersianCalendar.PersianEra" />.</returns>
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06002FD6 RID: 12246 RVA: 0x000AB343 File Offset: 0x000A9543
		public override int[] Eras
		{
			get
			{
				return new int[] { PersianCalendar.PersianEra };
			}
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>An integer from 1 through 12 that represents the month in the specified <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="time" /> parameter represents a date less than <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x06002FD7 RID: 12247 RVA: 0x000AB353 File Offset: 0x000A9553
		public override int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		/// <summary>Returns the number of months in the specified year of the specified era.</summary>
		/// <returns>Returns 10 if the <paramref name="year" /> parameter is 9378; otherwise, always returns 12.</returns>
		/// <param name="year">An integer from 1 through 9378 that represents the year. </param>
		/// <param name="era">An integer from 0 through 1 that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06002FD8 RID: 12248 RVA: 0x000AB363 File Offset: 0x000A9563
		public override int GetMonthsInYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378)
			{
				return 10;
			}
			return 12;
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>An integer from 1 through 9378 that represents the year in the specified <see cref="T:System.DateTime" />. </returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="time" /> parameter represents a date less than <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x06002FD9 RID: 12249 RVA: 0x000AB379 File Offset: 0x000A9579
		public override int GetYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 0);
		}

		/// <summary>Determines whether the specified date is a leap day.</summary>
		/// <returns>true if the specified day is a leap day; otherwise, false.</returns>
		/// <param name="year">An integer from 1 through 9378 that represents the year. </param>
		/// <param name="month">An integer that represents the month and ranges from 1 through 12 if <paramref name="year" /> is not 9378, or 1 through 10 if <paramref name="year" /> is 9378.</param>
		/// <param name="day">An integer from 1 through 31 that represents the day. </param>
		/// <param name="era">An integer from 0 through 1 that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06002FDA RID: 12250 RVA: 0x000AB38C File Offset: 0x000A958C
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Day must be between 1 and {0} for month {1}."), daysInMonth, month));
			}
			return this.IsLeapYear(year, era) && month == 12 && day == 30;
		}

		/// <summary>Returns the leap month for a specified year and era.</summary>
		/// <returns>The return value is always 0.</returns>
		/// <param name="year">An integer from 1 through 9378 that represents the year to convert. </param>
		/// <param name="era">An integer from 0 through 1 that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06002FDB RID: 12251 RVA: 0x000AB3EE File Offset: 0x000A95EE
		public override int GetLeapMonth(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			return 0;
		}

		/// <summary>Determines whether the specified month in the specified year and era is a leap month.</summary>
		/// <returns>Always returns false because the <see cref="T:System.Globalization.PersianCalendar" /> class does not support the notion of a leap month.</returns>
		/// <param name="year">An integer from 1 through 9378 that represents the year. </param>
		/// <param name="month">An integer that represents the month and ranges from 1 through 12 if <paramref name="year" /> is not 9378, or 1 through 10 if <paramref name="year" /> is 9378.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06002FDC RID: 12252 RVA: 0x000AB3F8 File Offset: 0x000A95F8
		public override bool IsLeapMonth(int year, int month, int era)
		{
			PersianCalendar.CheckYearMonthRange(year, month, era);
			return false;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <returns>true if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">An integer from 1 through 9378 that represents the year. </param>
		/// <param name="era">An integer from 0 through 1 that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06002FDD RID: 12253 RVA: 0x000AB403 File Offset: 0x000A9603
		public override bool IsLeapYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			return year != 9378 && this.GetAbsoluteDatePersian(year + 1, 1, 1) - this.GetAbsoluteDatePersian(year, 1, 1) == 366L;
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> object that is set to the specified date, time, and era.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that is set to the specified date and time in the current era.</returns>
		/// <param name="year">An integer from 1 through 9378 that represents the year. </param>
		/// <param name="month">An integer from 1 through 12 that represents the month. </param>
		/// <param name="day">An integer from 1 through 31 that represents the day. </param>
		/// <param name="hour">An integer from 0 through 23 that represents the hour. </param>
		/// <param name="minute">An integer from 0 through 59 that represents the minute. </param>
		/// <param name="second">An integer from 0 through 59 that represents the second. </param>
		/// <param name="millisecond">An integer from 0 through 999 that represents the millisecond. </param>
		/// <param name="era">An integer from 0 through 1 that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" />, <paramref name="millisecond" />, or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06002FDE RID: 12254 RVA: 0x000AB434 File Offset: 0x000A9634
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Day must be between 1 and {0} for month {1}."), daysInMonth, month));
			}
			long absoluteDatePersian = this.GetAbsoluteDatePersian(year, month, day);
			if (absoluteDatePersian >= 0L)
			{
				return new DateTime(absoluteDatePersian * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Year, Month, and Day parameters describe an un-representable DateTime."));
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.InvalidOperationException">This calendar is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than 100 or greater than 9378.</exception>
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002FDF RID: 12255 RVA: 0x000AB4BD File Offset: 0x000A96BD
		// (set) Token: 0x06002FE0 RID: 12256 RVA: 0x000AB4E4 File Offset: 0x000A96E4
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 1410);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > 9378)
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 99, 9378));
				}
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a four-digit year representation.</summary>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <param name="year">An integer from 1 through 9378 that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 0 or greater than 9378. </exception>
		// Token: 0x06002FE1 RID: 12257 RVA: 0x000AB53C File Offset: 0x000A973C
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Non-negative number required."));
			}
			if (year < 100)
			{
				return base.ToFourDigitYear(year);
			}
			if (year > 9378)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 9378));
			}
			return year;
		}

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x0400189D RID: 6301
		public static readonly int PersianEra = 1;

		// Token: 0x0400189E RID: 6302
		internal static long PersianEpoch = new DateTime(622, 3, 22).Ticks / 864000000000L;

		// Token: 0x0400189F RID: 6303
		private const int ApproximateHalfYear = 180;

		// Token: 0x040018A0 RID: 6304
		internal const int DatePartYear = 0;

		// Token: 0x040018A1 RID: 6305
		internal const int DatePartDayOfYear = 1;

		// Token: 0x040018A2 RID: 6306
		internal const int DatePartMonth = 2;

		// Token: 0x040018A3 RID: 6307
		internal const int DatePartDay = 3;

		// Token: 0x040018A4 RID: 6308
		internal const int MonthsPerYear = 12;

		// Token: 0x040018A5 RID: 6309
		internal static int[] DaysToMonth = new int[]
		{
			0, 31, 62, 93, 124, 155, 186, 216, 246, 276,
			306, 336, 366
		};

		// Token: 0x040018A6 RID: 6310
		internal const int MaxCalendarYear = 9378;

		// Token: 0x040018A7 RID: 6311
		internal const int MaxCalendarMonth = 10;

		// Token: 0x040018A8 RID: 6312
		internal const int MaxCalendarDay = 13;

		// Token: 0x040018A9 RID: 6313
		internal static DateTime minDate = new DateTime(622, 3, 22);

		// Token: 0x040018AA RID: 6314
		internal static DateTime maxDate = DateTime.MaxValue;

		// Token: 0x040018AB RID: 6315
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 1410;
	}
}
