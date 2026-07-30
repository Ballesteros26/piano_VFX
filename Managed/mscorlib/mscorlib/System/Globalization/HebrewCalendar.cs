using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents the Hebrew calendar.</summary>
	// Token: 0x02000413 RID: 1043
	[ComVisible(true)]
	[Serializable]
	public class HebrewCalendar : Calendar
	{
		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.HebrewCalendar" /> type.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.HebrewCalendar" /> type, which is equivalent to the first moment of January, 1, 1583 C.E. in the Gregorian calendar.</returns>
		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x0600319A RID: 12698 RVA: 0x000B2BC6 File Offset: 0x000B0DC6
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return HebrewCalendar.calendarMinValue;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.HebrewCalendar" /> type.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.HebrewCalendar" /> type, which is equivalent to the last moment of September, 29, 2239 C.E. in the Gregorian calendar.</returns>
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x000B2BCD File Offset: 0x000B0DCD
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return HebrewCalendar.calendarMaxValue;
			}
		}

		/// <summary>Gets a value that indicates whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>Always returns <see cref="F:System.Globalization.CalendarAlgorithmType.LunisolarCalendar" />.</returns>
		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x00028538 File Offset: 0x00026738
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunisolarCalendar;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x0600319E RID: 12702 RVA: 0x00058C99 File Offset: 0x00056E99
		internal override int ID
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x000B2BD4 File Offset: 0x000B0DD4
		private static void CheckHebrewYearValue(int y, int era, string varName)
		{
			HebrewCalendar.CheckEraRange(era);
			if (y > 5999 || y < 5343)
			{
				throw new ArgumentOutOfRangeException(varName, string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 5343, 5999));
			}
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x000B2C28 File Offset: 0x000B0E28
		private void CheckHebrewMonthValue(int year, int month, int era)
		{
			int monthsInYear = this.GetMonthsInYear(year, era);
			if (month < 1 || month > monthsInYear)
			{
				throw new ArgumentOutOfRangeException("month", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, monthsInYear));
			}
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x000B2C74 File Offset: 0x000B0E74
		private void CheckHebrewDayValue(int year, int month, int day, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, daysInMonth));
			}
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x000B2CBF File Offset: 0x000B0EBF
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != HebrewCalendar.HebrewEra)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x000B2CE4 File Offset: 0x000B0EE4
		private static void CheckTicksRange(long ticks)
		{
			if (ticks < HebrewCalendar.calendarMinValue.Ticks || ticks > HebrewCalendar.calendarMaxValue.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Specified time is not supported in this calendar. It should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive."), HebrewCalendar.calendarMinValue, HebrewCalendar.calendarMaxValue));
			}
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x000B2D44 File Offset: 0x000B0F44
		internal static int GetResult(HebrewCalendar.__DateBuffer result, int part)
		{
			switch (part)
			{
			case 0:
				return result.year;
			case 2:
				return result.month;
			case 3:
				return result.day;
			}
			throw new InvalidOperationException(Environment.GetResourceString("Internal Error in DateTime and Calendar operations."));
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000B2D84 File Offset: 0x000B0F84
		internal static int GetLunarMonthDay(int gregorianYear, HebrewCalendar.__DateBuffer lunarDate)
		{
			int num = gregorianYear - 1583;
			if (num < 0 || num > 656)
			{
				throw new ArgumentOutOfRangeException("gregorianYear");
			}
			num *= 2;
			lunarDate.day = HebrewCalendar.HebrewTable[num];
			int num2 = HebrewCalendar.HebrewTable[num + 1];
			int day = lunarDate.day;
			if (day != 0)
			{
				switch (day)
				{
				case 30:
					lunarDate.month = 3;
					break;
				case 31:
					lunarDate.month = 5;
					lunarDate.day = 2;
					break;
				case 32:
					lunarDate.month = 5;
					lunarDate.day = 3;
					break;
				case 33:
					lunarDate.month = 3;
					lunarDate.day = 29;
					break;
				default:
					lunarDate.month = 4;
					break;
				}
			}
			else
			{
				lunarDate.month = 5;
				lunarDate.day = 1;
			}
			return num2;
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000B2E44 File Offset: 0x000B1044
		internal virtual int GetDatePart(long ticks, int part)
		{
			HebrewCalendar.CheckTicksRange(ticks);
			DateTime dateTime = new DateTime(ticks);
			int year = dateTime.Year;
			int month = dateTime.Month;
			int day = dateTime.Day;
			HebrewCalendar.__DateBuffer _DateBuffer = new HebrewCalendar.__DateBuffer();
			_DateBuffer.year = year + 3760;
			int num = HebrewCalendar.GetLunarMonthDay(year, _DateBuffer);
			HebrewCalendar.__DateBuffer _DateBuffer2 = new HebrewCalendar.__DateBuffer();
			_DateBuffer2.year = _DateBuffer.year;
			_DateBuffer2.month = _DateBuffer.month;
			_DateBuffer2.day = _DateBuffer.day;
			long absoluteDate = GregorianCalendar.GetAbsoluteDate(year, month, day);
			if (month == 1 && day == 1)
			{
				return HebrewCalendar.GetResult(_DateBuffer2, part);
			}
			long num2 = absoluteDate - GregorianCalendar.GetAbsoluteDate(year, 1, 1);
			if (num2 + (long)_DateBuffer.day <= (long)HebrewCalendar.LunarMonthLen[num, _DateBuffer.month])
			{
				_DateBuffer2.day += (int)num2;
				return HebrewCalendar.GetResult(_DateBuffer2, part);
			}
			_DateBuffer2.month++;
			_DateBuffer2.day = 1;
			num2 -= (long)(HebrewCalendar.LunarMonthLen[num, _DateBuffer.month] - _DateBuffer.day);
			if (num2 > 1L)
			{
				while (num2 > (long)HebrewCalendar.LunarMonthLen[num, _DateBuffer2.month])
				{
					long num3 = num2;
					int[,] lunarMonthLen = HebrewCalendar.LunarMonthLen;
					int num4 = num;
					HebrewCalendar.__DateBuffer _DateBuffer3 = _DateBuffer2;
					int month2 = _DateBuffer3.month;
					_DateBuffer3.month = month2 + 1;
					num2 = num3 - (long)lunarMonthLen[num4, month2];
					if (_DateBuffer2.month > 13 || HebrewCalendar.LunarMonthLen[num, _DateBuffer2.month] == 0)
					{
						_DateBuffer2.year++;
						num = HebrewCalendar.HebrewTable[(year + 1 - 1583) * 2 + 1];
						_DateBuffer2.month = 1;
					}
				}
				_DateBuffer2.day += (int)(num2 - 1L);
			}
			return HebrewCalendar.GetResult(_DateBuffer2, part);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of months away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of months to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add <paramref name="months" />. </param>
		/// <param name="months">The number of months to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120,000 or greater than 120,000. </exception>
		// Token: 0x060031A7 RID: 12711 RVA: 0x000B3010 File Offset: 0x000B1210
		public override DateTime AddMonths(DateTime time, int months)
		{
			DateTime dateTime;
			try
			{
				int num = this.GetDatePart(time.Ticks, 0);
				int datePart = this.GetDatePart(time.Ticks, 2);
				int num2 = this.GetDatePart(time.Ticks, 3);
				int i;
				if (months >= 0)
				{
					int num3;
					for (i = datePart + months; i > (num3 = this.GetMonthsInYear(num, 0)); i -= num3)
					{
						num++;
					}
				}
				else if ((i = datePart + months) <= 0)
				{
					months = -months;
					months -= datePart;
					num--;
					int num3;
					while (months > (num3 = this.GetMonthsInYear(num, 0)))
					{
						num--;
						months -= num3;
					}
					num3 = this.GetMonthsInYear(num, 0);
					i = num3 - months;
				}
				int daysInMonth = this.GetDaysInMonth(num, i);
				if (num2 > daysInMonth)
				{
					num2 = daysInMonth;
				}
				dateTime = this.ToDateTime(num, i, num2, 0, 0, 0, 0);
				dateTime = new DateTime(dateTime.Ticks + time.Ticks % 864000000000L);
			}
			catch (ArgumentException)
			{
				throw new ArgumentOutOfRangeException("months", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Value to add was out of range."), Array.Empty<object>()));
			}
			return dateTime;
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add <paramref name="years" />. </param>
		/// <param name="years">The number of years to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		// Token: 0x060031A8 RID: 12712 RVA: 0x000B3128 File Offset: 0x000B1328
		public override DateTime AddYears(DateTime time, int years)
		{
			int num = this.GetDatePart(time.Ticks, 0);
			int num2 = this.GetDatePart(time.Ticks, 2);
			int num3 = this.GetDatePart(time.Ticks, 3);
			num += years;
			HebrewCalendar.CheckHebrewYearValue(num, 0, "years");
			int monthsInYear = this.GetMonthsInYear(num, 0);
			if (num2 > monthsInYear)
			{
				num2 = monthsInYear;
			}
			int daysInMonth = this.GetDaysInMonth(num, num2);
			if (num3 > daysInMonth)
			{
				num3 = daysInMonth;
			}
			long num4 = this.ToDateTime(num, num2, num3, 0, 0, 0, 0).Ticks + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(num4, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(num4);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 30 that represents the day of the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x060031A9 RID: 12713 RVA: 0x000B31D2 File Offset: 0x000B13D2
		public override int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x060031AA RID: 12714 RVA: 0x000AB28A File Offset: 0x000A948A
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000B31E2 File Offset: 0x000B13E2
		internal static int GetHebrewYearType(int year, int era)
		{
			HebrewCalendar.CheckHebrewYearValue(year, era, "year");
			return HebrewCalendar.HebrewTable[(year - 3760 - 1583) * 2 + 1];
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 385 that represents the day of the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is earlier than September 17, 1583 in the Gregorian calendar, or greater than <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />. </exception>
		// Token: 0x060031AC RID: 12716 RVA: 0x000B3208 File Offset: 0x000B1408
		public override int GetDayOfYear(DateTime time)
		{
			int year = this.GetYear(time);
			DateTime dateTime;
			if (year == 5343)
			{
				dateTime = new DateTime(1582, 9, 27);
			}
			else
			{
				dateTime = this.ToDateTime(year, 1, 1, 0, 0, 0, 0, 0);
			}
			return (int)((time.Ticks - dateTime.Ticks) / 864000000000L) + 1;
		}

		/// <summary>Returns the number of days in the specified month in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 13 that represents the month. </param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or Calendar.Eras[Calendar.CurrentEra].</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object. </exception>
		// Token: 0x060031AD RID: 12717 RVA: 0x000B3264 File Offset: 0x000B1464
		public override int GetDaysInMonth(int year, int month, int era)
		{
			HebrewCalendar.CheckEraRange(era);
			int hebrewYearType = HebrewCalendar.GetHebrewYearType(year, era);
			this.CheckHebrewMonthValue(year, month, era);
			int num = HebrewCalendar.LunarMonthLen[hebrewYearType, month];
			if (num == 0)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Month must be between one and twelve."));
			}
			return num;
		}

		/// <summary>Returns the number of days in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or HebrewCalendar.Eras[Calendar.CurrentEra].</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object. </exception>
		// Token: 0x060031AE RID: 12718 RVA: 0x000B32AC File Offset: 0x000B14AC
		public override int GetDaysInYear(int year, int era)
		{
			HebrewCalendar.CheckEraRange(era);
			int hebrewYearType = HebrewCalendar.GetHebrewYearType(year, era);
			if (hebrewYearType < 4)
			{
				return 352 + hebrewYearType;
			}
			return 382 + (hebrewYearType - 3);
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era in the specified <see cref="T:System.DateTime" />. The return value is always <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x060031AF RID: 12719 RVA: 0x000B32DC File Offset: 0x000B14DC
		public override int GetEra(DateTime time)
		{
			return HebrewCalendar.HebrewEra;
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.HebrewCalendar" />.</summary>
		/// <returns>An array of integers that represents the eras in the <see cref="T:System.Globalization.HebrewCalendar" /> type. The return value is always an array containing one element equal to <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" />. </returns>
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060031B0 RID: 12720 RVA: 0x000B32E3 File Offset: 0x000B14E3
		public override int[] Eras
		{
			get
			{
				return new int[] { HebrewCalendar.HebrewEra };
			}
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 13 that represents the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is less than <see cref="P:System.Globalization.HebrewCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x060031B1 RID: 12721 RVA: 0x000B32F3 File Offset: 0x000B14F3
		public override int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		/// <summary>Returns the number of months in the specified year in the specified era.</summary>
		/// <returns>The number of months in the specified year in the specified era. The return value is either 12 in a common year, or 13 in a leap year.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or HebrewCalendar.Eras[Calendar.CurrentEra].</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object. </exception>
		// Token: 0x060031B2 RID: 12722 RVA: 0x000B3303 File Offset: 0x000B1503
		public override int GetMonthsInYear(int year, int era)
		{
			if (!this.IsLeapYear(year, era))
			{
				return 12;
			}
			return 13;
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <returns>An integer that represents the year in the specified <see cref="T:System.DateTime" /> value.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object. </exception>
		// Token: 0x060031B3 RID: 12723 RVA: 0x000B3314 File Offset: 0x000B1514
		public override int GetYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 0);
		}

		/// <summary>Determines whether the specified date in the specified era is a leap day.</summary>
		/// <returns>true if the specified day is a leap day; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 13 that represents the month. </param>
		/// <param name="day">An integer from 1 to 30 that represents the day. </param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or HebrewCalendar.Eras[Calendar.CurrentEra]..</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x060031B4 RID: 12724 RVA: 0x000B3324 File Offset: 0x000B1524
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			if (this.IsLeapMonth(year, month, era))
			{
				this.CheckHebrewDayValue(year, month, day, era);
				return true;
			}
			if (this.IsLeapYear(year, 0) && month == 6 && day == 30)
			{
				return true;
			}
			this.CheckHebrewDayValue(year, month, day, era);
			return false;
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <returns>A positive integer that indicates the leap month in the specified year and era. The return value is 7 if the <paramref name="year" /> and <paramref name="era" /> parameters specify a leap year, or 0 if the year is not a leap year.</returns>
		/// <param name="year">A year.</param>
		/// <param name="era">An era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or HebrewCalendar.Eras[Calendar.CurrentEra].</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is not <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or HebrewCalendar.Eras[Calendar.CurrentEra].-or-<paramref name="year" /> is less than the Hebrew calendar year 5343 or greater than the Hebrew calendar year 5999.</exception>
		// Token: 0x060031B5 RID: 12725 RVA: 0x000B3360 File Offset: 0x000B1560
		public override int GetLeapMonth(int year, int era)
		{
			if (this.IsLeapYear(year, era))
			{
				return 7;
			}
			return 0;
		}

		/// <summary>Determines whether the specified month in the specified year in the specified era is a leap month.</summary>
		/// <returns>true if the specified month is a leap month; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 13 that represents the month. </param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or HebrewCalendar.Eras[Calendar.CurrentEra].</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x060031B6 RID: 12726 RVA: 0x000B336F File Offset: 0x000B156F
		public override bool IsLeapMonth(int year, int month, int era)
		{
			bool flag = this.IsLeapYear(year, era);
			this.CheckHebrewMonthValue(year, month, era);
			return flag && month == 7;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <returns>true if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or HebrewCalendar.Eras[Calendar.CurrentEra].</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar. </exception>
		// Token: 0x060031B7 RID: 12727 RVA: 0x000B338B File Offset: 0x000B158B
		public override bool IsLeapYear(int year, int era)
		{
			HebrewCalendar.CheckHebrewYearValue(year, era, "year");
			return (7L * (long)year + 1L) % 19L < 7L;
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x000B33AC File Offset: 0x000B15AC
		private static int GetDayDifference(int lunarYearType, int month1, int day1, int month2, int day2)
		{
			if (month1 == month2)
			{
				return day1 - day2;
			}
			bool flag = month1 > month2;
			if (flag)
			{
				int num = month1;
				int num2 = day1;
				month1 = month2;
				day1 = day2;
				month2 = num;
				day2 = num2;
			}
			int num3 = HebrewCalendar.LunarMonthLen[lunarYearType, month1] - day1;
			month1++;
			while (month1 < month2)
			{
				num3 += HebrewCalendar.LunarMonthLen[lunarYearType, month1++];
			}
			num3 += day2;
			if (!flag)
			{
				return -num3;
			}
			return num3;
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000B3414 File Offset: 0x000B1614
		private static DateTime HebrewToGregorian(int hebrewYear, int hebrewMonth, int hebrewDay, int hour, int minute, int second, int millisecond)
		{
			int num = hebrewYear - 3760;
			HebrewCalendar.__DateBuffer _DateBuffer = new HebrewCalendar.__DateBuffer();
			int lunarMonthDay = HebrewCalendar.GetLunarMonthDay(num, _DateBuffer);
			if (hebrewMonth == _DateBuffer.month && hebrewDay == _DateBuffer.day)
			{
				return new DateTime(num, 1, 1, hour, minute, second, millisecond);
			}
			int dayDifference = HebrewCalendar.GetDayDifference(lunarMonthDay, hebrewMonth, hebrewDay, _DateBuffer.month, _DateBuffer.day);
			DateTime dateTime = new DateTime(num, 1, 1);
			return new DateTime(dateTime.Ticks + (long)dayDifference * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date and time in the specified era.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 13 that represents the month. </param>
		/// <param name="day">An integer from 1 to 30 that represents the day. </param>
		/// <param name="hour">An integer from 0 to 23 that represents the hour. </param>
		/// <param name="minute">An integer from 0 to 59 that represents the minute. </param>
		/// <param name="second">An integer from 0 to 59 that represents the second. </param>
		/// <param name="millisecond">An integer from 0 to 999 that represents the millisecond. </param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or HebrewCalendar.Eras[Calendar.CurrentEra].</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" /> or <paramref name="era" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object.-or- <paramref name="hour" /> is less than 0 or greater than 23.-or- <paramref name="minute" /> is less than 0 or greater than 59.-or- <paramref name="second" /> is less than 0 or greater than 59.-or- <paramref name="millisecond" /> is less than 0 or greater than 999. </exception>
		// Token: 0x060031BA RID: 12730 RVA: 0x000B34A0 File Offset: 0x000B16A0
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			HebrewCalendar.CheckHebrewYearValue(year, era, "year");
			this.CheckHebrewMonthValue(year, month, era);
			this.CheckHebrewDayValue(year, month, day, era);
			DateTime dateTime = HebrewCalendar.HebrewToGregorian(year, month, day, hour, minute, second, millisecond);
			HebrewCalendar.CheckTicksRange(dateTime.Ticks);
			return dateTime;
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.HebrewCalendar" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">In a set operation, the Hebrew calendar year value is less than 5343 but is not 99, or the year value is greater than 5999. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060031BB RID: 12731 RVA: 0x000B34ED File Offset: 0x000B16ED
		// (set) Token: 0x060031BC RID: 12732 RVA: 0x000B3514 File Offset: 0x000B1714
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 5790);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value != 99)
				{
					HebrewCalendar.CheckHebrewYearValue(value, HebrewCalendar.HebrewEra, "value");
				}
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a 4-digit year by using the <see cref="P:System.Globalization.HebrewCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>If the <paramref name="year" /> parameter is a 2-digit year, the return value is the corresponding 4-digit year. If the <paramref name="year" /> parameter is a 4-digit year, the return value is the unchanged <paramref name="year" /> parameter.</returns>
		/// <param name="year">A 2-digit year from 0 through 99, or a 4-digit Hebrew calendar year from 5343 through 5999.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 0.-or-<paramref name="year" /> is less than <see cref="P:System.Globalization.HebrewCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />. </exception>
		// Token: 0x060031BD RID: 12733 RVA: 0x000B3538 File Offset: 0x000B1738
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
			if (year > 5999 || year < 5343)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 5343, 5999));
			}
			return year;
		}

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x04001A37 RID: 6711
		public static readonly int HebrewEra = 1;

		// Token: 0x04001A38 RID: 6712
		internal const int DatePartYear = 0;

		// Token: 0x04001A39 RID: 6713
		internal const int DatePartDayOfYear = 1;

		// Token: 0x04001A3A RID: 6714
		internal const int DatePartMonth = 2;

		// Token: 0x04001A3B RID: 6715
		internal const int DatePartDay = 3;

		// Token: 0x04001A3C RID: 6716
		internal const int DatePartDayOfWeek = 4;

		// Token: 0x04001A3D RID: 6717
		private const int HebrewYearOf1AD = 3760;

		// Token: 0x04001A3E RID: 6718
		private const int FirstGregorianTableYear = 1583;

		// Token: 0x04001A3F RID: 6719
		private const int LastGregorianTableYear = 2239;

		// Token: 0x04001A40 RID: 6720
		private const int TABLESIZE = 656;

		// Token: 0x04001A41 RID: 6721
		private const int MinHebrewYear = 5343;

		// Token: 0x04001A42 RID: 6722
		private const int MaxHebrewYear = 5999;

		// Token: 0x04001A43 RID: 6723
		private static readonly int[] HebrewTable = new int[]
		{
			7, 3, 17, 3, 0, 4, 11, 2, 21, 6,
			1, 3, 13, 2, 25, 4, 5, 3, 16, 2,
			27, 6, 9, 1, 20, 2, 0, 6, 11, 3,
			23, 4, 4, 2, 14, 3, 27, 4, 8, 2,
			18, 3, 28, 6, 11, 1, 22, 5, 2, 3,
			12, 3, 25, 4, 6, 2, 16, 3, 26, 6,
			8, 2, 20, 1, 0, 6, 11, 2, 24, 4,
			4, 3, 15, 2, 25, 6, 8, 1, 19, 2,
			29, 6, 9, 3, 22, 4, 3, 2, 13, 3,
			25, 4, 6, 3, 17, 2, 27, 6, 7, 3,
			19, 2, 31, 4, 11, 3, 23, 4, 5, 2,
			15, 3, 25, 6, 6, 2, 19, 1, 29, 6,
			10, 2, 22, 4, 3, 3, 14, 2, 24, 6,
			6, 1, 17, 3, 28, 5, 8, 3, 20, 1,
			32, 5, 12, 3, 22, 6, 4, 1, 16, 2,
			26, 6, 6, 3, 17, 2, 0, 4, 10, 3,
			22, 4, 3, 2, 14, 3, 24, 6, 5, 2,
			17, 1, 28, 6, 9, 2, 19, 3, 31, 4,
			13, 2, 23, 6, 3, 3, 15, 1, 27, 5,
			7, 3, 17, 3, 29, 4, 11, 2, 21, 6,
			3, 1, 14, 2, 25, 6, 5, 3, 16, 2,
			28, 4, 9, 3, 20, 2, 0, 6, 12, 1,
			23, 6, 4, 2, 14, 3, 26, 4, 8, 2,
			18, 3, 0, 4, 10, 3, 21, 5, 1, 3,
			13, 1, 24, 5, 5, 3, 15, 3, 27, 4,
			8, 2, 19, 3, 29, 6, 10, 2, 22, 4,
			3, 3, 14, 2, 26, 4, 6, 3, 18, 2,
			28, 6, 10, 1, 20, 6, 2, 2, 12, 3,
			24, 4, 5, 2, 16, 3, 28, 4, 8, 3,
			19, 2, 0, 6, 12, 1, 23, 5, 3, 3,
			14, 3, 26, 4, 7, 2, 17, 3, 28, 6,
			9, 2, 21, 4, 1, 3, 13, 2, 25, 4,
			5, 3, 16, 2, 27, 6, 9, 1, 19, 3,
			0, 5, 11, 3, 23, 4, 4, 2, 14, 3,
			25, 6, 7, 1, 18, 2, 28, 6, 9, 3,
			21, 4, 2, 2, 12, 3, 25, 4, 6, 2,
			16, 3, 26, 6, 8, 2, 20, 1, 0, 6,
			11, 2, 22, 6, 4, 1, 15, 2, 25, 6,
			6, 3, 18, 1, 29, 5, 9, 3, 22, 4,
			2, 3, 13, 2, 23, 6, 4, 3, 15, 2,
			27, 4, 7, 3, 19, 2, 31, 4, 11, 3,
			21, 6, 3, 2, 15, 1, 25, 6, 6, 2,
			17, 3, 29, 4, 10, 2, 20, 6, 3, 1,
			13, 3, 24, 5, 4, 3, 16, 1, 27, 5,
			7, 3, 17, 3, 0, 4, 11, 2, 21, 6,
			1, 3, 13, 2, 25, 4, 5, 3, 16, 2,
			29, 4, 9, 3, 19, 6, 30, 2, 13, 1,
			23, 6, 4, 2, 14, 3, 27, 4, 8, 2,
			18, 3, 0, 4, 11, 3, 22, 5, 2, 3,
			14, 1, 26, 5, 6, 3, 16, 3, 28, 4,
			10, 2, 20, 6, 30, 3, 11, 2, 24, 4,
			4, 3, 15, 2, 25, 6, 8, 1, 19, 2,
			29, 6, 9, 3, 22, 4, 3, 2, 13, 3,
			25, 4, 7, 2, 17, 3, 27, 6, 9, 1,
			21, 5, 1, 3, 11, 3, 23, 4, 5, 2,
			15, 3, 25, 6, 6, 2, 19, 1, 29, 6,
			10, 2, 22, 4, 3, 3, 14, 2, 24, 6,
			6, 1, 18, 2, 28, 6, 8, 3, 20, 4,
			2, 2, 12, 3, 24, 4, 4, 3, 16, 2,
			26, 6, 6, 3, 17, 2, 0, 4, 10, 3,
			22, 4, 3, 2, 14, 3, 24, 6, 5, 2,
			17, 1, 28, 6, 9, 2, 21, 4, 1, 3,
			13, 2, 23, 6, 5, 1, 15, 3, 27, 5,
			7, 3, 19, 1, 0, 5, 10, 3, 22, 4,
			2, 3, 13, 2, 24, 6, 4, 3, 15, 2,
			27, 4, 8, 3, 20, 4, 1, 2, 11, 3,
			22, 6, 3, 2, 15, 1, 25, 6, 7, 2,
			17, 3, 29, 4, 10, 2, 21, 6, 1, 3,
			13, 1, 24, 5, 5, 3, 15, 3, 27, 4,
			8, 2, 19, 6, 1, 1, 12, 2, 22, 6,
			3, 3, 14, 2, 26, 4, 6, 3, 18, 2,
			28, 6, 10, 1, 20, 6, 2, 2, 12, 3,
			24, 4, 5, 2, 16, 3, 28, 4, 9, 2,
			19, 6, 30, 3, 12, 1, 23, 5, 3, 3,
			14, 3, 26, 4, 7, 2, 17, 3, 28, 6,
			9, 2, 21, 4, 1, 3, 13, 2, 25, 4,
			5, 3, 16, 2, 27, 6, 9, 1, 19, 6,
			30, 2, 11, 3, 23, 4, 4, 2, 14, 3,
			27, 4, 7, 3, 18, 2, 28, 6, 11, 1,
			22, 5, 2, 3, 12, 3, 25, 4, 6, 2,
			16, 3, 26, 6, 8, 2, 20, 4, 30, 3,
			11, 2, 24, 4, 4, 3, 15, 2, 25, 6,
			8, 1, 18, 3, 29, 5, 9, 3, 22, 4,
			3, 2, 13, 3, 23, 6, 6, 1, 17, 2,
			27, 6, 7, 3, 20, 4, 1, 2, 11, 3,
			23, 4, 5, 2, 15, 3, 25, 6, 6, 2,
			19, 1, 29, 6, 10, 2, 20, 6, 3, 1,
			14, 2, 24, 6, 4, 3, 17, 1, 28, 5,
			8, 3, 20, 4, 1, 3, 12, 2, 22, 6,
			2, 3, 14, 2, 26, 4, 6, 3, 17, 2,
			0, 4, 10, 3, 20, 6, 1, 2, 14, 1,
			24, 6, 5, 2, 15, 3, 28, 4, 9, 2,
			19, 6, 1, 1, 12, 3, 23, 5, 3, 3,
			15, 1, 27, 5, 7, 3, 17, 3, 29, 4,
			11, 2, 21, 6, 1, 3, 12, 2, 25, 4,
			5, 3, 16, 2, 28, 4, 9, 3, 19, 6,
			30, 2, 12, 1, 23, 6, 4, 2, 14, 3,
			26, 4, 8, 2, 18, 3, 0, 4, 10, 3,
			22, 5, 2, 3, 14, 1, 25, 5, 6, 3,
			16, 3, 28, 4, 9, 2, 20, 6, 30, 3,
			11, 2, 23, 4, 4, 3, 15, 2, 27, 4,
			7, 3, 19, 2, 29, 6, 11, 1, 21, 6,
			3, 2, 13, 3, 25, 4, 6, 2, 17, 3,
			27, 6, 9, 1, 20, 5, 30, 3, 10, 3,
			22, 4, 3, 2, 14, 3, 24, 6, 5, 2,
			17, 1, 28, 6, 9, 2, 21, 4, 1, 3,
			13, 2, 23, 6, 5, 1, 16, 2, 27, 6,
			7, 3, 19, 4, 30, 2, 11, 3, 23, 4,
			3, 3, 14, 2, 25, 6, 5, 3, 16, 2,
			28, 4, 9, 3, 21, 4, 2, 2, 12, 3,
			23, 6, 4, 2, 16, 1, 26, 6, 8, 2,
			20, 4, 30, 3, 11, 2, 22, 6, 4, 1,
			14, 3, 25, 5, 6, 3, 18, 1, 29, 5,
			9, 3, 22, 4, 2, 3, 13, 2, 23, 6,
			4, 3, 15, 2, 27, 4, 7, 3, 20, 4,
			1, 2, 11, 3, 21, 6, 3, 2, 15, 1,
			25, 6, 6, 2, 17, 3, 29, 4, 10, 2,
			20, 6, 3, 1, 13, 3, 24, 5, 4, 3,
			17, 1, 28, 5, 8, 3, 18, 6, 1, 1,
			12, 2, 22, 6, 2, 3, 14, 2, 26, 4,
			6, 3, 17, 2, 28, 6, 10, 1, 20, 6,
			1, 2, 12, 3, 24, 4, 5, 2, 15, 3,
			28, 4, 9, 2, 19, 6, 33, 3, 12, 1,
			23, 5, 3, 3, 13, 3, 25, 4, 6, 2,
			16, 3, 26, 6, 8, 2, 20, 4, 30, 3,
			11, 2, 24, 4, 4, 3, 15, 2, 25, 6,
			8, 1, 18, 6, 33, 2, 9, 3, 22, 4,
			3, 2, 13, 3, 25, 4, 6, 3, 17, 2,
			27, 6, 9, 1, 21, 5, 1, 3, 11, 3,
			23, 4, 5, 2, 15, 3, 25, 6, 6, 2,
			19, 4, 33, 3, 10, 2, 22, 4, 3, 3,
			14, 2, 24, 6, 6, 1
		};

		// Token: 0x04001A44 RID: 6724
		private static readonly int[,] LunarMonthLen = new int[,]
		{
			{
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0
			},
			{
				0, 30, 29, 29, 29, 30, 29, 30, 29, 30,
				29, 30, 29, 0
			},
			{
				0, 30, 29, 30, 29, 30, 29, 30, 29, 30,
				29, 30, 29, 0
			},
			{
				0, 30, 30, 30, 29, 30, 29, 30, 29, 30,
				29, 30, 29, 0
			},
			{
				0, 30, 29, 29, 29, 30, 30, 29, 30, 29,
				30, 29, 30, 29
			},
			{
				0, 30, 29, 30, 29, 30, 30, 29, 30, 29,
				30, 29, 30, 29
			},
			{
				0, 30, 30, 30, 29, 30, 30, 29, 30, 29,
				30, 29, 30, 29
			}
		};

		// Token: 0x04001A45 RID: 6725
		internal static readonly DateTime calendarMinValue = new DateTime(1583, 1, 1);

		// Token: 0x04001A46 RID: 6726
		internal static readonly DateTime calendarMaxValue = new DateTime(new DateTime(2239, 9, 29, 23, 59, 59, 999).Ticks + 9999L);

		// Token: 0x04001A47 RID: 6727
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 5790;

		// Token: 0x02000414 RID: 1044
		internal class __DateBuffer
		{
			// Token: 0x04001A48 RID: 6728
			internal int year;

			// Token: 0x04001A49 RID: 6729
			internal int month;

			// Token: 0x04001A4A RID: 6730
			internal int day;
		}
	}
}
