using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents the Julian calendar.</summary>
	// Token: 0x0200041E RID: 1054
	[ComVisible(true)]
	[Serializable]
	public class JulianCalendar : Calendar
	{
		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.JulianCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.JulianCalendar" /> class, which is equivalent to the first moment of January 1, 0001 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x000AB612 File Offset: 0x000A9812
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.JulianCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.JulianCalendar" /> class, which is equivalent to the last moment of December 31, 9999 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x000AB619 File Offset: 0x000A9819
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
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x00003B29 File Offset: 0x00001D29
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.JulianCalendar" /> class.</summary>
		// Token: 0x06003225 RID: 12837 RVA: 0x000B49A0 File Offset: 0x000B2BA0
		public JulianCalendar()
		{
			this.twoDigitYearMax = 2029;
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x0004DB4B File Offset: 0x0004BD4B
		internal override int ID
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x000B49BE File Offset: 0x000B2BBE
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != JulianCalendar.JulianEra)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x000B49E0 File Offset: 0x000B2BE0
		internal void CheckYearEraRange(int year, int era)
		{
			JulianCalendar.CheckEraRange(era);
			if (year <= 0 || year > this.MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, this.MaxYear));
			}
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x000B4A30 File Offset: 0x000B2C30
		internal static void CheckMonthRange(int month)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Month must be between one and twelve."));
			}
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x000B4A50 File Offset: 0x000B2C50
		internal static void CheckDayRange(int year, int month, int day)
		{
			if (year == 1 && month == 1 && day < 3)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Year, Month, and Day parameters describe an un-representable DateTime."));
			}
			int[] array = ((year % 4 == 0) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			int num = array[month] - array[month - 1];
			if (day < 1 || day > num)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, num));
			}
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000B4ACC File Offset: 0x000B2CCC
		internal static int GetDatePart(long ticks, int part)
		{
			int i = (int)((ticks + 1728000000000L) / 864000000000L);
			int num = i / 1461;
			i -= num * 1461;
			int num2 = i / 365;
			if (num2 == 4)
			{
				num2 = 3;
			}
			if (part == 0)
			{
				return num * 4 + num2 + 1;
			}
			i -= num2 * 365;
			if (part == 1)
			{
				return i + 1;
			}
			int[] array = ((num2 == 3) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			int num3 = i >> 6;
			while (i >= array[num3])
			{
				num3++;
			}
			if (part == 2)
			{
				return num3;
			}
			return i - array[num3 - 1] + 1;
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000B4B68 File Offset: 0x000B2D68
		internal static long DateToTicks(int year, int month, int day)
		{
			int[] array = ((year % 4 == 0) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			int num = year - 1;
			return (long)(num * 365 + num / 4 + array[month - 1] + day - 1 - 2) * 864000000000L;
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of months away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of months to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add months. </param>
		/// <param name="months">The number of months to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120000.-or- <paramref name="months" /> is greater than 120000. </exception>
		// Token: 0x0600322D RID: 12845 RVA: 0x000B4BB0 File Offset: 0x000B2DB0
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), -120000, 120000));
			}
			int num = JulianCalendar.GetDatePart(time.Ticks, 0);
			int num2 = JulianCalendar.GetDatePart(time.Ticks, 2);
			int num3 = JulianCalendar.GetDatePart(time.Ticks, 3);
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
			int[] array = ((num % 4 == 0 && (num % 100 != 0 || num % 400 == 0)) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			int num5 = array[num2] - array[num2 - 1];
			if (num3 > num5)
			{
				num3 = num5;
			}
			long num6 = JulianCalendar.DateToTicks(num, num2, num3) + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(num6, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(num6);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add years. </param>
		/// <param name="years">The number of years to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		// Token: 0x0600322E RID: 12846 RVA: 0x000AB26D File Offset: 0x000A946D
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 31 that represents the day of the month in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x0600322F RID: 12847 RVA: 0x000B4CC0 File Offset: 0x000B2EC0
		public override int GetDayOfMonth(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 3);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003230 RID: 12848 RVA: 0x000AB28A File Offset: 0x000A948A
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 366 that represents the day of the year in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003231 RID: 12849 RVA: 0x000B4CCF File Offset: 0x000B2ECF
		public override int GetDayOfYear(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 1);
		}

		/// <summary>Returns the number of days in the specified month in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar. -or- <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003232 RID: 12850 RVA: 0x000B4CE0 File Offset: 0x000B2EE0
		public override int GetDaysInMonth(int year, int month, int era)
		{
			this.CheckYearEraRange(year, era);
			JulianCalendar.CheckMonthRange(month);
			int[] array = ((year % 4 == 0) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			return array[month] - array[month - 1];
		}

		/// <summary>Returns the number of days in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar. -or- <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003233 RID: 12851 RVA: 0x000B4D16 File Offset: 0x000B2F16
		public override int GetDaysInYear(int year, int era)
		{
			if (!this.IsLeapYear(year, era))
			{
				return 365;
			}
			return 366;
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003234 RID: 12852 RVA: 0x000B4D2D File Offset: 0x000B2F2D
		public override int GetEra(DateTime time)
		{
			return JulianCalendar.JulianEra;
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 12 that represents the month in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003235 RID: 12853 RVA: 0x000B4D34 File Offset: 0x000B2F34
		public override int GetMonth(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 2);
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.JulianCalendar" />.</summary>
		/// <returns>An array of integers that represents the eras in the <see cref="T:System.Globalization.JulianCalendar" />.</returns>
		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x000B4D43 File Offset: 0x000B2F43
		public override int[] Eras
		{
			get
			{
				return new int[] { JulianCalendar.JulianEra };
			}
		}

		/// <summary>Returns the number of months in the specified year in the specified era.</summary>
		/// <returns>The number of months in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar. -or- <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003237 RID: 12855 RVA: 0x000B4D53 File Offset: 0x000B2F53
		public override int GetMonthsInYear(int year, int era)
		{
			this.CheckYearEraRange(year, era);
			return 12;
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the year in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003238 RID: 12856 RVA: 0x000B4D5F File Offset: 0x000B2F5F
		public override int GetYear(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 0);
		}

		/// <summary>Determines whether the specified date in the specified era is a leap day.</summary>
		/// <returns>true if the specified day is a leap day; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="day">An integer from 1 to 31 that represents the day. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar. -or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003239 RID: 12857 RVA: 0x000B4D6E File Offset: 0x000B2F6E
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			JulianCalendar.CheckMonthRange(month);
			if (this.IsLeapYear(year, era))
			{
				JulianCalendar.CheckDayRange(year, month, day);
				return month == 2 && day == 29;
			}
			JulianCalendar.CheckDayRange(year, month, day);
			return false;
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <returns>A positive integer that indicates the leap month in the specified year and era. Alternatively, this method returns zero if the calendar does not support a leap month, or if <paramref name="year" /> and <paramref name="era" /> do not specify a leap year.</returns>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		// Token: 0x0600323A RID: 12858 RVA: 0x000B4D9E File Offset: 0x000B2F9E
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			this.CheckYearEraRange(year, era);
			return 0;
		}

		/// <summary>Determines whether the specified month in the specified year in the specified era is a leap month.</summary>
		/// <returns>This method always returns false, unless overridden by a derived class.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar. -or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x0600323B RID: 12859 RVA: 0x000B4DA9 File Offset: 0x000B2FA9
		public override bool IsLeapMonth(int year, int month, int era)
		{
			this.CheckYearEraRange(year, era);
			JulianCalendar.CheckMonthRange(month);
			return false;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <returns>true if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. -or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x0600323C RID: 12860 RVA: 0x000B4DBA File Offset: 0x000B2FBA
		public override bool IsLeapYear(int year, int era)
		{
			this.CheckYearEraRange(year, era);
			return year % 4 == 0;
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
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar.-or- <paramref name="hour" /> is less than zero or greater than 23.-or- <paramref name="minute" /> is less than zero or greater than 59.-or- <paramref name="second" /> is less than zero or greater than 59.-or- <paramref name="millisecond" /> is less than zero or greater than 999. -or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x0600323D RID: 12861 RVA: 0x000B4DCC File Offset: 0x000B2FCC
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			this.CheckYearEraRange(year, era);
			JulianCalendar.CheckMonthRange(month);
			JulianCalendar.CheckDayRange(year, month, day);
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 999));
			}
			if (hour >= 0 && hour < 24 && minute >= 0 && minute < 60 && second >= 0 && second < 60)
			{
				return new DateTime(JulianCalendar.DateToTicks(year, month, day) + new TimeSpan(0, hour, minute, second, millisecond).Ticks);
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Hour, Minute, and Second parameters describe an un-representable DateTime."));
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified in a set operation is less than 99. -or- The value specified in a set operation is greater than MaxSupportedDateTime.Year.</exception>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the current instance is read-only.</exception>
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x0600323E RID: 12862 RVA: 0x000ABB75 File Offset: 0x000A9D75
		// (set) Token: 0x0600323F RID: 12863 RVA: 0x000B4E84 File Offset: 0x000B3084
		public override int TwoDigitYearMax
		{
			get
			{
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > this.MaxYear)
				{
					throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 99, this.MaxYear));
				}
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.JulianCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003240 RID: 12864 RVA: 0x000B4EE0 File Offset: 0x000B30E0
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Non-negative number required."));
			}
			if (year > this.MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument must be between {0} and {1}."), 1, this.MaxYear));
			}
			return base.ToFourDigitYear(year);
		}

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x04001A9C RID: 6812
		public static readonly int JulianEra = 1;

		// Token: 0x04001A9D RID: 6813
		private const int DatePartYear = 0;

		// Token: 0x04001A9E RID: 6814
		private const int DatePartDayOfYear = 1;

		// Token: 0x04001A9F RID: 6815
		private const int DatePartMonth = 2;

		// Token: 0x04001AA0 RID: 6816
		private const int DatePartDay = 3;

		// Token: 0x04001AA1 RID: 6817
		private const int JulianDaysPerYear = 365;

		// Token: 0x04001AA2 RID: 6818
		private const int JulianDaysPer4Years = 1461;

		// Token: 0x04001AA3 RID: 6819
		private static readonly int[] DaysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334, 365
		};

		// Token: 0x04001AA4 RID: 6820
		private static readonly int[] DaysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335, 366
		};

		// Token: 0x04001AA5 RID: 6821
		internal int MaxYear = 9999;
	}
}
