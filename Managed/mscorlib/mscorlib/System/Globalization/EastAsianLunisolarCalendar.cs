using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents a calendar that divides time into months, days, years, and eras, and has dates that are based on cycles of the sun and the moon.</summary>
	// Token: 0x0200040D RID: 1037
	[ComVisible(true)]
	[Serializable]
	public abstract class EastAsianLunisolarCalendar : Calendar
	{
		/// <summary>Gets a value indicating whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>Always returns <see cref="F:System.Globalization.CalendarAlgorithmType.LunisolarCalendar" />.</returns>
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06003128 RID: 12584 RVA: 0x00028538 File Offset: 0x00026738
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunisolarCalendar;
			}
		}

		/// <summary>Calculates the year in the sexagenary (60-year) cycle that corresponds to the specified date.</summary>
		/// <returns>A number from 1 through 60 in the sexagenary cycle that corresponds to the <paramref name="date" /> parameter.</returns>
		/// <param name="time">A <see cref="T:System.DateTime" /> to read.</param>
		// Token: 0x06003129 RID: 12585 RVA: 0x000B0C04 File Offset: 0x000AEE04
		public virtual int GetSexagenaryYear(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.TimeToLunar(time, ref num, ref num2, ref num3);
			return (num - 4) % 60 + 1;
		}

		/// <summary>Calculates the celestial stem of the specified year in the sexagenary (60-year) cycle.</summary>
		/// <returns>A number from 1 through 10.</returns>
		/// <param name="sexagenaryYear">An integer from 1 through 60 that represents a year in the sexagenary cycle. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sexagenaryYear" /> is less than 1 or greater than 60.</exception>
		// Token: 0x0600312A RID: 12586 RVA: 0x000B0C3C File Offset: 0x000AEE3C
		public int GetCelestialStem(int sexagenaryYear)
		{
			if (sexagenaryYear < 1 || sexagenaryYear > 60)
			{
				throw new ArgumentOutOfRangeException("sexagenaryYear", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 1, 60 }));
			}
			return (sexagenaryYear - 1) % 10 + 1;
		}

		/// <summary>Calculates the terrestrial branch of the specified year in the sexagenary (60-year) cycle.</summary>
		/// <returns>An integer from 1 through 12.</returns>
		/// <param name="sexagenaryYear">An integer from 1 through 60 that represents a year in the sexagenary cycle.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sexagenaryYear" /> is less than 1 or greater than 60.</exception>
		// Token: 0x0600312B RID: 12587 RVA: 0x000B0C88 File Offset: 0x000AEE88
		public int GetTerrestrialBranch(int sexagenaryYear)
		{
			if (sexagenaryYear < 1 || sexagenaryYear > 60)
			{
				throw new ArgumentOutOfRangeException("sexagenaryYear", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 1, 60 }));
			}
			return (sexagenaryYear - 1) % 12 + 1;
		}

		// Token: 0x0600312C RID: 12588
		internal abstract int GetYearInfo(int LunarYear, int Index);

		// Token: 0x0600312D RID: 12589
		internal abstract int GetYear(int year, DateTime time);

		// Token: 0x0600312E RID: 12590
		internal abstract int GetGregorianYear(int year, int era);

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x0600312F RID: 12591
		internal abstract int MinCalendarYear { get; }

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06003130 RID: 12592
		internal abstract int MaxCalendarYear { get; }

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06003131 RID: 12593
		internal abstract EraInfo[] CalEraInfo { get; }

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06003132 RID: 12594
		internal abstract DateTime MinDate { get; }

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06003133 RID: 12595
		internal abstract DateTime MaxDate { get; }

		// Token: 0x06003134 RID: 12596 RVA: 0x000B0CD4 File Offset: 0x000AEED4
		internal int MinEraCalendarYear(int era)
		{
			EraInfo[] calEraInfo = this.CalEraInfo;
			if (calEraInfo == null)
			{
				return this.MinCalendarYear;
			}
			if (era == 0)
			{
				era = this.CurrentEraValue;
			}
			if (era == this.GetEra(this.MinDate))
			{
				return this.GetYear(this.MinCalendarYear, this.MinDate);
			}
			for (int i = 0; i < calEraInfo.Length; i++)
			{
				if (era == calEraInfo[i].era)
				{
					return calEraInfo[i].minEraYear;
				}
			}
			throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x000B0D58 File Offset: 0x000AEF58
		internal int MaxEraCalendarYear(int era)
		{
			EraInfo[] calEraInfo = this.CalEraInfo;
			if (calEraInfo == null)
			{
				return this.MaxCalendarYear;
			}
			if (era == 0)
			{
				era = this.CurrentEraValue;
			}
			if (era == this.GetEra(this.MaxDate))
			{
				return this.GetYear(this.MaxCalendarYear, this.MaxDate);
			}
			for (int i = 0; i < calEraInfo.Length; i++)
			{
				if (era == calEraInfo[i].era)
				{
					return calEraInfo[i].maxEraYear;
				}
			}
			throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x000AAEFC File Offset: 0x000A90FC
		internal EastAsianLunisolarCalendar()
		{
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x000B0DDC File Offset: 0x000AEFDC
		internal void CheckTicksRange(long ticks)
		{
			if (ticks < this.MinSupportedDateTime.Ticks || ticks > this.MaxSupportedDateTime.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Specified time is not supported in this calendar. It should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive."), this.MinSupportedDateTime, this.MaxSupportedDateTime));
			}
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000B0E40 File Offset: 0x000AF040
		internal void CheckEraRange(int era)
		{
			if (era == 0)
			{
				era = this.CurrentEraValue;
			}
			if (era < this.GetEra(this.MinDate) || era > this.GetEra(this.MaxDate))
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000B0E80 File Offset: 0x000AF080
		internal int CheckYearRange(int year, int era)
		{
			this.CheckEraRange(era);
			year = this.GetGregorianYear(year, era);
			if (year < this.MinCalendarYear || year > this.MaxCalendarYear)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[]
				{
					this.MinEraCalendarYear(era),
					this.MaxEraCalendarYear(era)
				}));
			}
			return year;
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x000B0EEC File Offset: 0x000AF0EC
		internal int CheckYearMonthRange(int year, int month, int era)
		{
			year = this.CheckYearRange(year, era);
			if (month == 13 && this.GetYearInfo(year, 0) == 0)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Month must be between one and twelve."));
			}
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Month must be between one and twelve."));
			}
			return year;
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x000B0F48 File Offset: 0x000AF148
		internal int InternalGetDaysInMonth(int year, int month)
		{
			int num = 32768;
			num >>= month - 1;
			int num2;
			if ((this.GetYearInfo(year, 3) & num) == 0)
			{
				num2 = 29;
			}
			else
			{
				num2 = 30;
			}
			return num2;
		}

		/// <summary>Calculates the number of days in the specified month of the specified year and era.</summary>
		/// <returns>The number of days in the specified month of the specified year and era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 through 12 in a common year, or 1 through 13 in a leap year, that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x0600313C RID: 12604 RVA: 0x000B0F79 File Offset: 0x000AF179
		public override int GetDaysInMonth(int year, int month, int era)
		{
			year = this.CheckYearMonthRange(year, month, era);
			return this.InternalGetDaysInMonth(year, month);
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x000B0F8E File Offset: 0x000AF18E
		private static int GregorianIsLeapYear(int y)
		{
			if (y % 4 != 0)
			{
				return 0;
			}
			if (y % 100 != 0)
			{
				return 1;
			}
			if (y % 400 == 0)
			{
				return 1;
			}
			return 0;
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date, time, and era.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that is set to the specified date, time, and era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 through 13 that represents the month. </param>
		/// <param name="day">An integer from 1 through 31 that represents the day. </param>
		/// <param name="hour">An integer from 0 through 23 that represents the hour. </param>
		/// <param name="minute">An integer from 0 through 59 that represents the minute. </param>
		/// <param name="second">An integer from 0 through 59 that represents the second. </param>
		/// <param name="millisecond">An integer from 0 through 999 that represents the millisecond. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" />, <paramref name="millisecond" />, or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x0600313E RID: 12606 RVA: 0x000B0FAC File Offset: 0x000AF1AC
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			year = this.CheckYearMonthRange(year, month, era);
			int num = this.InternalGetDaysInMonth(year, month);
			if (day < 1 || day > num)
			{
				throw new ArgumentOutOfRangeException("day", Environment.GetResourceString("Day must be between 1 and {0} for month {1}.", new object[] { num, month }));
			}
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			if (this.LunarToGregorian(year, month, day, ref num2, ref num3, ref num4))
			{
				return new DateTime(num2, num3, num4, hour, minute, second, millisecond);
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Year, Month, and Day parameters describe an un-representable DateTime."));
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x000B103C File Offset: 0x000AF23C
		internal void GregorianToLunar(int nSYear, int nSMonth, int nSDate, ref int nLYear, ref int nLMonth, ref int nLDate)
		{
			int i = ((EastAsianLunisolarCalendar.GregorianIsLeapYear(nSYear) == 1) ? EastAsianLunisolarCalendar.DaysToMonth366[nSMonth - 1] : EastAsianLunisolarCalendar.DaysToMonth365[nSMonth - 1]) + nSDate;
			nLYear = nSYear;
			int num;
			int num2;
			if (nLYear == this.MaxCalendarYear + 1)
			{
				nLYear--;
				i += ((EastAsianLunisolarCalendar.GregorianIsLeapYear(nLYear) == 1) ? 366 : 365);
				num = this.GetYearInfo(nLYear, 1);
				num2 = this.GetYearInfo(nLYear, 2);
			}
			else
			{
				num = this.GetYearInfo(nLYear, 1);
				num2 = this.GetYearInfo(nLYear, 2);
				if (nSMonth < num || (nSMonth == num && nSDate < num2))
				{
					nLYear--;
					i += ((EastAsianLunisolarCalendar.GregorianIsLeapYear(nLYear) == 1) ? 366 : 365);
					num = this.GetYearInfo(nLYear, 1);
					num2 = this.GetYearInfo(nLYear, 2);
				}
			}
			i -= EastAsianLunisolarCalendar.DaysToMonth365[num - 1];
			i -= num2 - 1;
			int num3 = 32768;
			int yearInfo = this.GetYearInfo(nLYear, 3);
			int num4 = (((yearInfo & num3) != 0) ? 30 : 29);
			nLMonth = 1;
			while (i > num4)
			{
				i -= num4;
				nLMonth++;
				num3 >>= 1;
				num4 = (((yearInfo & num3) != 0) ? 30 : 29);
			}
			nLDate = i;
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x000B1174 File Offset: 0x000AF374
		internal bool LunarToGregorian(int nLYear, int nLMonth, int nLDate, ref int nSolarYear, ref int nSolarMonth, ref int nSolarDay)
		{
			if (nLDate < 1 || nLDate > 30)
			{
				return false;
			}
			int num = nLDate - 1;
			for (int i = 1; i < nLMonth; i++)
			{
				num += this.InternalGetDaysInMonth(nLYear, i);
			}
			int yearInfo = this.GetYearInfo(nLYear, 1);
			int yearInfo2 = this.GetYearInfo(nLYear, 2);
			int num2 = EastAsianLunisolarCalendar.GregorianIsLeapYear(nLYear);
			int[] array = ((num2 == 1) ? EastAsianLunisolarCalendar.DaysToMonth366 : EastAsianLunisolarCalendar.DaysToMonth365);
			nSolarDay = yearInfo2;
			if (yearInfo > 1)
			{
				nSolarDay += array[yearInfo - 1];
			}
			nSolarDay += num;
			if (nSolarDay > num2 + 365)
			{
				nSolarYear = nLYear + 1;
				nSolarDay -= num2 + 365;
			}
			else
			{
				nSolarYear = nLYear;
			}
			nSolarMonth = 1;
			while (nSolarMonth < 12 && array[nSolarMonth] < nSolarDay)
			{
				nSolarMonth++;
			}
			nSolarDay -= array[nSolarMonth - 1];
			return true;
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x000B124C File Offset: 0x000AF44C
		internal DateTime LunarToTime(DateTime time, int year, int month, int day)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.LunarToGregorian(year, month, day, ref num, ref num2, ref num3);
			return GregorianCalendar.GetDefaultInstance().ToDateTime(num, num2, num3, time.Hour, time.Minute, time.Second, time.Millisecond);
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x000B129C File Offset: 0x000AF49C
		internal void TimeToLunar(DateTime time, ref int year, ref int month, ref int day)
		{
			Calendar defaultInstance = GregorianCalendar.GetDefaultInstance();
			int year2 = defaultInstance.GetYear(time);
			int month2 = defaultInstance.GetMonth(time);
			int dayOfMonth = defaultInstance.GetDayOfMonth(time);
			this.GregorianToLunar(year2, month2, dayOfMonth, ref year, ref month, ref day);
		}

		/// <summary>Calculates the date that is the specified number of months away from the specified date.</summary>
		/// <returns>A new <see cref="T:System.DateTime" /> that results from adding the specified number of months to the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add <paramref name="months" />. </param>
		/// <param name="months">The number of months to add. </param>
		/// <exception cref="T:System.ArgumentException">The result is outside the supported range of a <see cref="T:System.DateTime" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120000 or greater than 120000. -or-<paramref name="time" /> is less than <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x06003143 RID: 12611 RVA: 0x000B12D8 File Offset: 0x000AF4D8
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { -120000, 120000 }));
			}
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.TimeToLunar(time, ref num, ref num2, ref num3);
			int i = num2 + months;
			if (i > 0)
			{
				int num4 = (this.InternalIsLeapYear(num) ? 13 : 12);
				while (i - num4 > 0)
				{
					i -= num4;
					num++;
					num4 = (this.InternalIsLeapYear(num) ? 13 : 12);
				}
				num2 = i;
			}
			else
			{
				while (i <= 0)
				{
					int num5 = (this.InternalIsLeapYear(num - 1) ? 13 : 12);
					i += num5;
					num--;
				}
				num2 = i;
			}
			int num6 = this.InternalGetDaysInMonth(num, num2);
			if (num3 > num6)
			{
				num3 = num6;
			}
			DateTime dateTime = this.LunarToTime(time, num, num2, num3);
			Calendar.CheckAddResult(dateTime.Ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return dateTime;
		}

		/// <summary>Calculates the date that is the specified number of years away from the specified date.</summary>
		/// <returns>A new <see cref="T:System.DateTime" /> that results from adding the specified number of years to the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add <paramref name="years" />. </param>
		/// <param name="years">The number of years to add. </param>
		/// <exception cref="T:System.ArgumentException">The result is outside the supported range of a <see cref="T:System.DateTime" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is less than <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x06003144 RID: 12612 RVA: 0x000B13E4 File Offset: 0x000AF5E4
		public override DateTime AddYears(DateTime time, int years)
		{
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.TimeToLunar(time, ref num, ref num2, ref num3);
			num += years;
			if (num2 == 13 && !this.InternalIsLeapYear(num))
			{
				num2 = 12;
				num3 = this.InternalGetDaysInMonth(num, num2);
			}
			int num4 = this.InternalGetDaysInMonth(num, num2);
			if (num3 > num4)
			{
				num3 = num4;
			}
			DateTime dateTime = this.LunarToTime(time, num, num2, num3);
			Calendar.CheckAddResult(dateTime.Ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return dateTime;
		}

		/// <summary>Calculates the day of the year in the specified date.</summary>
		/// <returns>An integer from 1 through 354 in a common year, or 1 through 384 in a leap year, that represents the day of the year specified in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003145 RID: 12613 RVA: 0x000B1464 File Offset: 0x000AF664
		public override int GetDayOfYear(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.TimeToLunar(time, ref num, ref num2, ref num3);
			for (int i = 1; i < num2; i++)
			{
				num3 += this.InternalGetDaysInMonth(num, i);
			}
			return num3;
		}

		/// <summary>Calculates the day of the month in the specified date.</summary>
		/// <returns>An integer from 1 through 31 that represents the day of the month specified in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003146 RID: 12614 RVA: 0x000B14AC File Offset: 0x000AF6AC
		public override int GetDayOfMonth(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.TimeToLunar(time, ref num, ref num2, ref num3);
			return num3;
		}

		/// <summary>Calculates the number of days in the specified year and era.</summary>
		/// <returns>The number of days in the specified year and era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06003147 RID: 12615 RVA: 0x000B14DC File Offset: 0x000AF6DC
		public override int GetDaysInYear(int year, int era)
		{
			year = this.CheckYearRange(year, era);
			int num = 0;
			int num2 = (this.InternalIsLeapYear(year) ? 13 : 12);
			while (num2 != 0)
			{
				num += this.InternalGetDaysInMonth(year, num2--);
			}
			return num;
		}

		/// <summary>Returns the month in the specified date.</summary>
		/// <returns>An integer from 1 to 13 that represents the month specified in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003148 RID: 12616 RVA: 0x000B151C File Offset: 0x000AF71C
		public override int GetMonth(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.TimeToLunar(time, ref num, ref num2, ref num3);
			return num2;
		}

		/// <summary>Returns the year in the specified date.</summary>
		/// <returns>An integer that represents the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003149 RID: 12617 RVA: 0x000B154C File Offset: 0x000AF74C
		public override int GetYear(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.TimeToLunar(time, ref num, ref num2, ref num3);
			return this.GetYear(num, time);
		}

		/// <summary>Calculates the day of the week in the specified date.</summary>
		/// <returns>One of the <see cref="T:System.DayOfWeek" /> values that represents the day of the week specified in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is less than <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x0600314A RID: 12618 RVA: 0x000B1581 File Offset: 0x000AF781
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		/// <summary>Calculates the number of months in the specified year and era.</summary>
		/// <returns>The number of months in the specified year in the specified era. The return value is 12 months in a common year or 13 months in a leap year.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x0600314B RID: 12619 RVA: 0x000B15A7 File Offset: 0x000AF7A7
		public override int GetMonthsInYear(int year, int era)
		{
			year = this.CheckYearRange(year, era);
			if (!this.InternalIsLeapYear(year))
			{
				return 12;
			}
			return 13;
		}

		/// <summary>Determines whether the specified date in the specified era is a leap day.</summary>
		/// <returns>true if the specified day is a leap day; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 through 13 that represents the month. </param>
		/// <param name="day">An integer from 1 through 31 that represents the day. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x0600314C RID: 12620 RVA: 0x000B15C4 File Offset: 0x000AF7C4
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			year = this.CheckYearMonthRange(year, month, era);
			int num = this.InternalGetDaysInMonth(year, month);
			if (day < 1 || day > num)
			{
				throw new ArgumentOutOfRangeException("day", Environment.GetResourceString("Day must be between 1 and {0} for month {1}.", new object[] { num, month }));
			}
			int yearInfo = this.GetYearInfo(year, 0);
			return yearInfo != 0 && month == yearInfo + 1;
		}

		/// <summary>Determines whether the specified month in the specified year and era is a leap month.</summary>
		/// <returns>true if the <paramref name="month" /> parameter is a leap month; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 through 13 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x0600314D RID: 12621 RVA: 0x000B1630 File Offset: 0x000AF830
		public override bool IsLeapMonth(int year, int month, int era)
		{
			year = this.CheckYearMonthRange(year, month, era);
			int yearInfo = this.GetYearInfo(year, 0);
			return yearInfo != 0 && month == yearInfo + 1;
		}

		/// <summary>Calculates the leap month for the specified year and era.</summary>
		/// <returns>A positive integer from 1 through 13 that indicates the leap month in the specified year and era. -or-Zero if this calendar does not support a leap month, or if the <paramref name="year" /> and <paramref name="era" /> parameters do not specify a leap year.</returns>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		// Token: 0x0600314E RID: 12622 RVA: 0x000B165C File Offset: 0x000AF85C
		public override int GetLeapMonth(int year, int era)
		{
			year = this.CheckYearRange(year, era);
			int yearInfo = this.GetYearInfo(year, 0);
			if (yearInfo > 0)
			{
				return yearInfo + 1;
			}
			return 0;
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000B1685 File Offset: 0x000AF885
		internal bool InternalIsLeapYear(int year)
		{
			return this.GetYearInfo(year, 0) != 0;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <returns>true if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06003150 RID: 12624 RVA: 0x000B1692 File Offset: 0x000AF892
		public override bool IsLeapYear(int year, int era)
		{
			year = this.CheckYearRange(year, era);
			return this.InternalIsLeapYear(year);
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.EastAsianLunisolarCalendar" />  is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than 99 or greater than the maximum supported year in the current calendar.</exception>
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x000B16A5 File Offset: 0x000AF8A5
		// (set) Token: 0x06003152 RID: 12626 RVA: 0x000B16DC File Offset: 0x000AF8DC
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.BaseCalendarID, this.GetYear(new DateTime(2029, 1, 1)));
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > this.MaxCalendarYear)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 99, this.MaxCalendarYear }));
				}
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a four-digit year.</summary>
		/// <returns>An integer that contains the four-digit representation of the <paramref name="year" /> parameter.</returns>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by this calendar. </exception>
		// Token: 0x06003153 RID: 12627 RVA: 0x000B1737 File Offset: 0x000AF937
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Non-negative number required."));
			}
			year = base.ToFourDigitYear(year);
			this.CheckYearRange(year, 0);
			return year;
		}

		// Token: 0x040019F5 RID: 6645
		internal const int LeapMonth = 0;

		// Token: 0x040019F6 RID: 6646
		internal const int Jan1Month = 1;

		// Token: 0x040019F7 RID: 6647
		internal const int Jan1Date = 2;

		// Token: 0x040019F8 RID: 6648
		internal const int nDaysPerMonth = 3;

		// Token: 0x040019F9 RID: 6649
		internal static readonly int[] DaysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334
		};

		// Token: 0x040019FA RID: 6650
		internal static readonly int[] DaysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335
		};

		// Token: 0x040019FB RID: 6651
		internal const int DatePartYear = 0;

		// Token: 0x040019FC RID: 6652
		internal const int DatePartDayOfYear = 1;

		// Token: 0x040019FD RID: 6653
		internal const int DatePartMonth = 2;

		// Token: 0x040019FE RID: 6654
		internal const int DatePartDay = 3;

		// Token: 0x040019FF RID: 6655
		internal const int MaxCalendarMonth = 13;

		// Token: 0x04001A00 RID: 6656
		internal const int MaxCalendarDay = 30;

		// Token: 0x04001A01 RID: 6657
		private const int DEFAULT_GREGORIAN_TWO_DIGIT_YEAR_MAX = 2029;
	}
}
