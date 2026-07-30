using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
	/// <summary>Represents the Gregorian calendar.</summary>
	// Token: 0x0200040F RID: 1039
	[ComVisible(true)]
	[Serializable]
	public class GregorianCalendar : Calendar
	{
		// Token: 0x06003157 RID: 12631 RVA: 0x000B17C8 File Offset: 0x000AF9C8
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_type == (GregorianCalendarTypes)0)
			{
				this.m_type = GregorianCalendarTypes.Localized;
			}
			if (this.m_type < GregorianCalendarTypes.Localized || this.m_type > GregorianCalendarTypes.TransliteratedFrench)
			{
				throw new SerializationException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("The deserialized value of the member \"{0}\" in the class \"{1}\" is out of range."), "type", "GregorianCalendar"));
			}
		}

		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.GregorianCalendar" /> type.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.GregorianCalendar" /> type, which is the first moment of January 1, 0001 C.E. and is equivalent to <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x000AB612 File Offset: 0x000A9812
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.GregorianCalendar" /> type.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.GregorianCalendar" /> type, which is the last moment of December 31, 9999 C.E. and is equivalent to <see cref="F:System.DateTime.MaxValue" />.</returns>
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06003159 RID: 12633 RVA: 0x000AB619 File Offset: 0x000A9819
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
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x0600315A RID: 12634 RVA: 0x00003B29 File Offset: 0x00001D29
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x000B181B File Offset: 0x000AFA1B
		internal static Calendar GetDefaultInstance()
		{
			if (GregorianCalendar.s_defaultInstance == null)
			{
				GregorianCalendar.s_defaultInstance = new GregorianCalendar();
			}
			return GregorianCalendar.s_defaultInstance;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.GregorianCalendar" /> class using the default <see cref="T:System.Globalization.GregorianCalendarTypes" /> value.</summary>
		// Token: 0x0600315C RID: 12636 RVA: 0x000B1839 File Offset: 0x000AFA39
		public GregorianCalendar()
			: this(GregorianCalendarTypes.Localized)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.GregorianCalendar" /> class using the specified <see cref="T:System.Globalization.GregorianCalendarTypes" /> value.</summary>
		/// <param name="type">The <see cref="T:System.Globalization.GregorianCalendarTypes" /> value that denotes which language version of the calendar to create. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="type" /> is not a member of the <see cref="T:System.Globalization.GregorianCalendarTypes" /> enumeration.</exception>
		// Token: 0x0600315D RID: 12637 RVA: 0x000B1844 File Offset: 0x000AFA44
		public GregorianCalendar(GregorianCalendarTypes type)
		{
			if (type < GregorianCalendarTypes.Localized || type > GregorianCalendarTypes.TransliteratedFrench)
			{
				throw new ArgumentOutOfRangeException("type", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[]
				{
					GregorianCalendarTypes.Localized,
					GregorianCalendarTypes.TransliteratedFrench
				}));
			}
			this.m_type = type;
		}

		/// <summary>Gets or sets the <see cref="T:System.Globalization.GregorianCalendarTypes" /> value that denotes the language version of the current <see cref="T:System.Globalization.GregorianCalendar" />.</summary>
		/// <returns>A <see cref="T:System.Globalization.GregorianCalendarTypes" /> value that denotes the language version of the current <see cref="T:System.Globalization.GregorianCalendar" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified in a set operation is not a member of the <see cref="T:System.Globalization.GregorianCalendarTypes" /> enumeration.</exception>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the current instance is read-only.</exception>
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x000B1895 File Offset: 0x000AFA95
		// (set) Token: 0x0600315F RID: 12639 RVA: 0x000B189D File Offset: 0x000AFA9D
		public virtual GregorianCalendarTypes CalendarType
		{
			get
			{
				return this.m_type;
			}
			set
			{
				base.VerifyWritable();
				if (value - GregorianCalendarTypes.Localized <= 1 || value - GregorianCalendarTypes.MiddleEastFrench <= 3)
				{
					this.m_type = value;
					return;
				}
				throw new ArgumentOutOfRangeException("m_type", Environment.GetResourceString("Enum value was out of legal range."));
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x000B1895 File Offset: 0x000AFA95
		internal override int ID
		{
			get
			{
				return (int)this.m_type;
			}
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x000B18D0 File Offset: 0x000AFAD0
		internal virtual int GetDatePart(long ticks, int part)
		{
			int i = (int)(ticks / 864000000000L);
			int num = i / 146097;
			i -= num * 146097;
			int num2 = i / 36524;
			if (num2 == 4)
			{
				num2 = 3;
			}
			i -= num2 * 36524;
			int num3 = i / 1461;
			i -= num3 * 1461;
			int num4 = i / 365;
			if (num4 == 4)
			{
				num4 = 3;
			}
			if (part == 0)
			{
				return num * 400 + num2 * 100 + num3 * 4 + num4 + 1;
			}
			i -= num4 * 365;
			if (part == 1)
			{
				return i + 1;
			}
			int[] array = ((num4 == 3 && (num3 != 24 || num2 == 3)) ? GregorianCalendar.DaysToMonth366 : GregorianCalendar.DaysToMonth365);
			int num5 = i >> 6;
			while (i >= array[num5])
			{
				num5++;
			}
			if (part == 2)
			{
				return num5;
			}
			return i - array[num5 - 1] + 1;
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000B19B0 File Offset: 0x000AFBB0
		internal static long GetAbsoluteDate(int year, int month, int day)
		{
			if (year >= 1 && year <= 9999 && month >= 1 && month <= 12)
			{
				int[] array = ((year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? GregorianCalendar.DaysToMonth366 : GregorianCalendar.DaysToMonth365);
				if (day >= 1 && day <= array[month] - array[month - 1])
				{
					int num = year - 1;
					return (long)(num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1);
				}
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Year, Month, and Day parameters describe an un-representable DateTime."));
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000B1A3B File Offset: 0x000AFC3B
		internal virtual long DateToTicks(int year, int month, int day)
		{
			return GregorianCalendar.GetAbsoluteDate(year, month, day) * 864000000000L;
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of months away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of months to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add months. </param>
		/// <param name="months">The number of months to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120000.-or- <paramref name="months" /> is greater than 120000. </exception>
		// Token: 0x06003164 RID: 12644 RVA: 0x000B1A50 File Offset: 0x000AFC50
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
			int[] array = ((num % 4 == 0 && (num % 100 != 0 || num % 400 == 0)) ? GregorianCalendar.DaysToMonth366 : GregorianCalendar.DaysToMonth365);
			int num5 = array[num2] - array[num2 - 1];
			if (num3 > num5)
			{
				num3 = num5;
			}
			long num6 = this.DateToTicks(num, num2, num3) + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(num6, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(num6);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add years. </param>
		/// <param name="years">The number of years to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		// Token: 0x06003165 RID: 12645 RVA: 0x000AB26D File Offset: 0x000A946D
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 31 that represents the day of the month in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003166 RID: 12646 RVA: 0x000B1B64 File Offset: 0x000AFD64
		public override int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003167 RID: 12647 RVA: 0x000AB28A File Offset: 0x000A948A
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 366 that represents the day of the year in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003168 RID: 12648 RVA: 0x000B1B74 File Offset: 0x000AFD74
		public override int GetDayOfYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 1);
		}

		/// <summary>Returns the number of days in the specified month in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar.-or- <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003169 RID: 12649 RVA: 0x000B1B84 File Offset: 0x000AFD84
		public override int GetDaysInMonth(int year, int month, int era)
		{
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 1, 9999 }));
			}
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Month must be between one and twelve."));
			}
			int[] array = ((year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? GregorianCalendar.DaysToMonth366 : GregorianCalendar.DaysToMonth365);
			return array[month] - array[month - 1];
		}

		/// <summary>Returns the number of days in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar.-or- <paramref name="year" /> is outside the range supported by the calendar.</exception>
		// Token: 0x0600316A RID: 12650 RVA: 0x000B1C38 File Offset: 0x000AFE38
		public override int GetDaysInYear(int year, int era)
		{
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 9999));
			}
			if (year % 4 != 0 || (year % 100 == 0 && year % 400 != 0))
			{
				return 365;
			}
			return 366;
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x0600316B RID: 12651 RVA: 0x00003B29 File Offset: 0x00001D29
		public override int GetEra(DateTime time)
		{
			return 1;
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.GregorianCalendar" />.</summary>
		/// <returns>An array of integers that represents the eras in the <see cref="T:System.Globalization.GregorianCalendar" />.</returns>
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600316C RID: 12652 RVA: 0x000AC6E1 File Offset: 0x000AA8E1
		public override int[] Eras
		{
			get
			{
				return new int[] { 1 };
			}
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 12 that represents the month in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x0600316D RID: 12653 RVA: 0x000B1CBB File Offset: 0x000AFEBB
		public override int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		/// <summary>Returns the number of months in the specified year in the specified era.</summary>
		/// <returns>The number of months in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar.-or- <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x0600316E RID: 12654 RVA: 0x000B1CCC File Offset: 0x000AFECC
		public override int GetMonthsInYear(int year, int era)
		{
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
			if (year >= 1 && year <= 9999)
			{
				return 12;
			}
			throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 9999));
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the year in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x0600316F RID: 12655 RVA: 0x000B1D32 File Offset: 0x000AFF32
		public override int GetYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 0);
		}

		/// <summary>Determines whether the specified date in the specified era is a leap day.</summary>
		/// <returns>true if the specified day is a leap day; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="day">An integer from 1 to 31 that represents the day. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar. -or- <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003170 RID: 12656 RVA: 0x000B1D44 File Offset: 0x000AFF44
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 1, 12 }));
			}
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 1, 9999 }));
			}
			if (day < 1 || day > this.GetDaysInMonth(year, month))
			{
				throw new ArgumentOutOfRangeException("day", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[]
				{
					1,
					this.GetDaysInMonth(year, month)
				}));
			}
			return this.IsLeapYear(year) && (month == 2 && day == 29);
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <returns>Always 0 because the Gregorian calendar does not recognize leap months.</returns>
		/// <param name="year">A year.</param>
		/// <param name="era">An era. Specify either <see cref="F:System.Globalization.GregorianCalendar.ADEra" /> or GregorianCalendar.Eras[Calendar.CurrentEra].</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than the Gregorian calendar year 1 or greater than the Gregorian calendar year 9999.-or-<paramref name="era" /> is not <see cref="F:System.Globalization.GregorianCalendar.ADEra" /> or GregorianCalendar.Eras[Calendar.CurrentEra].</exception>
		// Token: 0x06003171 RID: 12657 RVA: 0x000B1E40 File Offset: 0x000B0040
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 9999));
			}
			return 0;
		}

		/// <summary>Determines whether the specified month in the specified year in the specified era is a leap month.</summary>
		/// <returns>This method always returns false, unless overridden by a derived class.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar.-or- <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003172 RID: 12658 RVA: 0x000B1EA8 File Offset: 0x000B00A8
		public override bool IsLeapMonth(int year, int month, int era)
		{
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 9999));
			}
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 1, 12 }));
			}
			return false;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <returns>true if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar.-or- <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003173 RID: 12659 RVA: 0x000B1F44 File Offset: 0x000B0144
		public override bool IsLeapYear(int year, int era)
		{
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
			if (year >= 1 && year <= 9999)
			{
				return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
			}
			throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 9999));
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
		///   <paramref name="era" /> is outside the range supported by the calendar.-or- <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar.-or- <paramref name="hour" /> is less than zero or greater than 23.-or- <paramref name="minute" /> is less than zero or greater than 59.-or- <paramref name="second" /> is less than zero or greater than 59.-or- <paramref name="millisecond" /> is less than zero or greater than 999. </exception>
		// Token: 0x06003174 RID: 12660 RVA: 0x000B1FC1 File Offset: 0x000B01C1
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			if (era == 0 || era == 1)
			{
				return new DateTime(year, month, day, hour, minute, second, millisecond);
			}
			throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x000B1FF1 File Offset: 0x000B01F1
		internal override bool TryToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era, out DateTime result)
		{
			if (era == 0 || era == 1)
			{
				return DateTime.TryCreate(year, month, day, hour, minute, second, millisecond, out result);
			}
			result = DateTime.MinValue;
			return false;
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified in a set operation is less than 99. -or- The value specified in a set operation is greater than MaxSupportedDateTime.Year.</exception>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the current instance is read-only.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06003176 RID: 12662 RVA: 0x000B201C File Offset: 0x000B021C
		// (set) Token: 0x06003177 RID: 12663 RVA: 0x000B2044 File Offset: 0x000B0244
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 2029);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > 9999)
				{
					throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 99, 9999));
				}
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.GregorianCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003178 RID: 12664 RVA: 0x000B209C File Offset: 0x000B029C
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Non-negative number required."));
			}
			if (year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 9999));
			}
			return base.ToFourDigitYear(year);
		}

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x04001A02 RID: 6658
		public const int ADEra = 1;

		// Token: 0x04001A03 RID: 6659
		internal const int DatePartYear = 0;

		// Token: 0x04001A04 RID: 6660
		internal const int DatePartDayOfYear = 1;

		// Token: 0x04001A05 RID: 6661
		internal const int DatePartMonth = 2;

		// Token: 0x04001A06 RID: 6662
		internal const int DatePartDay = 3;

		// Token: 0x04001A07 RID: 6663
		internal const int MaxYear = 9999;

		// Token: 0x04001A08 RID: 6664
		internal GregorianCalendarTypes m_type;

		// Token: 0x04001A09 RID: 6665
		internal static readonly int[] DaysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334, 365
		};

		// Token: 0x04001A0A RID: 6666
		internal static readonly int[] DaysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335, 366
		};

		// Token: 0x04001A0B RID: 6667
		private static volatile Calendar s_defaultInstance;

		// Token: 0x04001A0C RID: 6668
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 2029;
	}
}
