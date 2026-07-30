using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
	/// <summary>Represents time in divisions, such as weeks, months, and years.</summary>
	// Token: 0x020003F9 RID: 1017
	[ComVisible(true)]
	[Serializable]
	public abstract class Calendar : ICloneable
	{
		/// <summary>Gets the earliest date and time supported by this <see cref="T:System.Globalization.Calendar" /> object.</summary>
		/// <returns>The earliest date and time supported by this calendar. The default is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002FE3 RID: 12259 RVA: 0x000AB612 File Offset: 0x000A9812
		[ComVisible(false)]
		public virtual DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		/// <summary>Gets the latest date and time supported by this <see cref="T:System.Globalization.Calendar" /> object.</summary>
		/// <returns>The latest date and time supported by this calendar. The default is <see cref="F:System.DateTime.MaxValue" />.</returns>
		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06002FE4 RID: 12260 RVA: 0x000AB619 File Offset: 0x000A9819
		[ComVisible(false)]
		public virtual DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06002FE6 RID: 12262 RVA: 0x0009B163 File Offset: 0x00099363
		internal virtual int ID
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06002FE7 RID: 12263 RVA: 0x000AB636 File Offset: 0x000A9836
		internal virtual int BaseCalendarID
		{
			get
			{
				return this.ID;
			}
		}

		/// <summary>Gets a value indicating whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>One of the <see cref="T:System.Globalization.CalendarAlgorithmType" /> values.</returns>
		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x00015ED5 File Offset: 0x000140D5
		[ComVisible(false)]
		public virtual CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.Unknown;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Globalization.Calendar" /> object is read-only.</summary>
		/// <returns>true if this <see cref="T:System.Globalization.Calendar" /> object is read-only; otherwise, false.</returns>
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x000AB63E File Offset: 0x000A983E
		[ComVisible(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Globalization.Calendar" /> object.</summary>
		/// <returns>A new instance of <see cref="T:System.Object" /> that is the memberwise clone of the current <see cref="T:System.Globalization.Calendar" /> object.</returns>
		// Token: 0x06002FEA RID: 12266 RVA: 0x000AB646 File Offset: 0x000A9846
		[ComVisible(false)]
		public virtual object Clone()
		{
			object obj = base.MemberwiseClone();
			((Calendar)obj).SetReadOnlyState(false);
			return obj;
		}

		/// <summary>Returns a read-only version of the specified <see cref="T:System.Globalization.Calendar" /> object.</summary>
		/// <returns>The <see cref="T:System.Globalization.Calendar" /> object specified by the <paramref name="calendar" /> parameter, if <paramref name="calendar" /> is read-only.-or-A read-only memberwise clone of the <see cref="T:System.Globalization.Calendar" /> object specified by <paramref name="calendar" />, if <paramref name="calendar" /> is not read-only.</returns>
		/// <param name="calendar">A <see cref="T:System.Globalization.Calendar" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is null.</exception>
		// Token: 0x06002FEB RID: 12267 RVA: 0x000AB65A File Offset: 0x000A985A
		[ComVisible(false)]
		public static Calendar ReadOnly(Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (calendar.IsReadOnly)
			{
				return calendar;
			}
			Calendar calendar2 = (Calendar)calendar.MemberwiseClone();
			calendar2.SetReadOnlyState(true);
			return calendar2;
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000AB686 File Offset: 0x000A9886
		internal void VerifyWritable()
		{
			if (this.m_isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
			}
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000AB6A0 File Offset: 0x000A98A0
		internal void SetReadOnlyState(bool readOnly)
		{
			this.m_isReadOnly = readOnly;
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002FEE RID: 12270 RVA: 0x000AB6A9 File Offset: 0x000A98A9
		internal virtual int CurrentEraValue
		{
			get
			{
				if (this.m_currentEraValue == -1)
				{
					this.m_currentEraValue = CalendarData.GetCalendarData(this.BaseCalendarID).iCurrentEra;
				}
				return this.m_currentEraValue;
			}
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000AB6D0 File Offset: 0x000A98D0
		internal static void CheckAddResult(long ticks, DateTime minValue, DateTime maxValue)
		{
			if (ticks < minValue.Ticks || ticks > maxValue.Ticks)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("The result is out of the supported range for this calendar. The result should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive."), minValue, maxValue));
			}
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000AB70C File Offset: 0x000A990C
		internal DateTime Add(DateTime time, double value, int scale)
		{
			double num = value * (double)scale + ((value >= 0.0) ? 0.5 : (-0.5));
			if (num <= -315537897600000.0 || num >= 315537897600000.0)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Value to add was out of range."));
			}
			long num2 = (long)num;
			long num3 = time.Ticks + num2 * 10000L;
			Calendar.CheckAddResult(num3, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(num3);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of milliseconds away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of milliseconds to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to add milliseconds to. </param>
		/// <param name="milliseconds">The number of milliseconds to add.</param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="milliseconds" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value. </exception>
		// Token: 0x06002FF1 RID: 12273 RVA: 0x000AB796 File Offset: 0x000A9996
		public virtual DateTime AddMilliseconds(DateTime time, double milliseconds)
		{
			return this.Add(time, milliseconds, 1);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of days away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of days to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add days. </param>
		/// <param name="days">The number of days to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="days" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value. </exception>
		// Token: 0x06002FF2 RID: 12274 RVA: 0x000AB7A1 File Offset: 0x000A99A1
		public virtual DateTime AddDays(DateTime time, int days)
		{
			return this.Add(time, (double)days, 86400000);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of hours away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of hours to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add hours. </param>
		/// <param name="hours">The number of hours to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="hours" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value. </exception>
		// Token: 0x06002FF3 RID: 12275 RVA: 0x000AB7B1 File Offset: 0x000A99B1
		public virtual DateTime AddHours(DateTime time, int hours)
		{
			return this.Add(time, (double)hours, 3600000);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of minutes away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of minutes to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add minutes. </param>
		/// <param name="minutes">The number of minutes to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="minutes" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value. </exception>
		// Token: 0x06002FF4 RID: 12276 RVA: 0x000AB7C1 File Offset: 0x000A99C1
		public virtual DateTime AddMinutes(DateTime time, int minutes)
		{
			return this.Add(time, (double)minutes, 60000);
		}

		/// <summary>When overridden in a derived class, returns a <see cref="T:System.DateTime" /> that is the specified number of months away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of months to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add months. </param>
		/// <param name="months">The number of months to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value. </exception>
		// Token: 0x06002FF5 RID: 12277
		public abstract DateTime AddMonths(DateTime time, int months);

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of seconds away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of seconds to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add seconds. </param>
		/// <param name="seconds">The number of seconds to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="seconds" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value. </exception>
		// Token: 0x06002FF6 RID: 12278 RVA: 0x000AB7D1 File Offset: 0x000A99D1
		public virtual DateTime AddSeconds(DateTime time, int seconds)
		{
			return this.Add(time, (double)seconds, 1000);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of weeks away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of weeks to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add weeks. </param>
		/// <param name="weeks">The number of weeks to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="weeks" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value. </exception>
		// Token: 0x06002FF7 RID: 12279 RVA: 0x000AB7E1 File Offset: 0x000A99E1
		public virtual DateTime AddWeeks(DateTime time, int weeks)
		{
			return this.AddDays(time, weeks * 7);
		}

		/// <summary>When overridden in a derived class, returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add years. </param>
		/// <param name="years">The number of years to add. </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="years" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value. </exception>
		// Token: 0x06002FF8 RID: 12280
		public abstract DateTime AddYears(DateTime time, int years);

		/// <summary>When overridden in a derived class, returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A positive integer that represents the day of the month in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06002FF9 RID: 12281
		public abstract int GetDayOfMonth(DateTime time);

		/// <summary>When overridden in a derived class, returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06002FFA RID: 12282
		public abstract DayOfWeek GetDayOfWeek(DateTime time);

		/// <summary>When overridden in a derived class, returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A positive integer that represents the day of the year in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06002FFB RID: 12283
		public abstract int GetDayOfYear(DateTime time);

		/// <summary>Returns the number of days in the specified month and year of the current era.</summary>
		/// <returns>The number of days in the specified month in the specified year in the current era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">A positive integer that represents the month. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06002FFC RID: 12284 RVA: 0x000AB7ED File Offset: 0x000A99ED
		public virtual int GetDaysInMonth(int year, int month)
		{
			return this.GetDaysInMonth(year, month, 0);
		}

		/// <summary>When overridden in a derived class, returns the number of days in the specified month, year, and era.</summary>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">A positive integer that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06002FFD RID: 12285
		public abstract int GetDaysInMonth(int year, int month, int era);

		/// <summary>Returns the number of days in the specified year of the current era.</summary>
		/// <returns>The number of days in the specified year in the current era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06002FFE RID: 12286 RVA: 0x000AB7F8 File Offset: 0x000A99F8
		public virtual int GetDaysInYear(int year)
		{
			return this.GetDaysInYear(year, 0);
		}

		/// <summary>When overridden in a derived class, returns the number of days in the specified year and era.</summary>
		/// <returns>The number of days in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06002FFF RID: 12287
		public abstract int GetDaysInYear(int year, int era);

		/// <summary>When overridden in a derived class, returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003000 RID: 12288
		public abstract int GetEra(DateTime time);

		/// <summary>When overridden in a derived class, gets the list of eras in the current calendar.</summary>
		/// <returns>An array of integers that represents the eras in the current calendar.</returns>
		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06003001 RID: 12289
		public abstract int[] Eras { get; }

		/// <summary>Returns the hours value in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 0 to 23 that represents the hour in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003002 RID: 12290 RVA: 0x000AB802 File Offset: 0x000A9A02
		public virtual int GetHour(DateTime time)
		{
			return (int)(time.Ticks / 36000000000L % 24L);
		}

		/// <summary>Returns the milliseconds value in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A double-precision floating-point number from 0 to 999 that represents the milliseconds in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003003 RID: 12291 RVA: 0x000AB81A File Offset: 0x000A9A1A
		public virtual double GetMilliseconds(DateTime time)
		{
			return (double)(time.Ticks / 10000L % 1000L);
		}

		/// <summary>Returns the minutes value in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 0 to 59 that represents the minutes in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003004 RID: 12292 RVA: 0x000AB832 File Offset: 0x000A9A32
		public virtual int GetMinute(DateTime time)
		{
			return (int)(time.Ticks / 600000000L % 60L);
		}

		/// <summary>When overridden in a derived class, returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A positive integer that represents the month in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003005 RID: 12293
		public abstract int GetMonth(DateTime time);

		/// <summary>Returns the number of months in the specified year in the current era.</summary>
		/// <returns>The number of months in the specified year in the current era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003006 RID: 12294 RVA: 0x000AB847 File Offset: 0x000A9A47
		public virtual int GetMonthsInYear(int year)
		{
			return this.GetMonthsInYear(year, 0);
		}

		/// <summary>When overridden in a derived class, returns the number of months in the specified year in the specified era.</summary>
		/// <returns>The number of months in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003007 RID: 12295
		public abstract int GetMonthsInYear(int year, int era);

		/// <summary>Returns the seconds value in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 0 to 59 that represents the seconds in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003008 RID: 12296 RVA: 0x000AB851 File Offset: 0x000A9A51
		public virtual int GetSecond(DateTime time)
		{
			return (int)(time.Ticks / 10000000L % 60L);
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x000AB868 File Offset: 0x000A9A68
		internal int GetFirstDayWeekOfYear(DateTime time, int firstDayOfWeek)
		{
			int num = this.GetDayOfYear(time) - 1;
			int num2 = (this.GetDayOfWeek(time) - (DayOfWeek)(num % 7) - firstDayOfWeek + 14) % 7;
			return (num + num2) / 7 + 1;
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x000AB89C File Offset: 0x000A9A9C
		private int GetWeekOfYearFullDays(DateTime time, int firstDayOfWeek, int fullDays)
		{
			int num = this.GetDayOfYear(time) - 1;
			int num2 = this.GetDayOfWeek(time) - (DayOfWeek)(num % 7);
			int num3 = (firstDayOfWeek - num2 + 14) % 7;
			if (num3 != 0 && num3 >= fullDays)
			{
				num3 -= 7;
			}
			int num4 = num - num3;
			if (num4 >= 0)
			{
				return num4 / 7 + 1;
			}
			if (time <= this.MinSupportedDateTime.AddDays((double)num))
			{
				return this.GetWeekOfYearOfMinSupportedDateTime(firstDayOfWeek, fullDays);
			}
			return this.GetWeekOfYearFullDays(time.AddDays((double)(-(double)(num + 1))), firstDayOfWeek, fullDays);
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000AB918 File Offset: 0x000A9B18
		private int GetWeekOfYearOfMinSupportedDateTime(int firstDayOfWeek, int minimumDaysInFirstWeek)
		{
			int num = this.GetDayOfYear(this.MinSupportedDateTime) - 1;
			int num2 = this.GetDayOfWeek(this.MinSupportedDateTime) - (DayOfWeek)(num % 7);
			int num3 = (firstDayOfWeek + 7 - num2) % 7;
			if (num3 == 0 || num3 >= minimumDaysInFirstWeek)
			{
				return 1;
			}
			int num4 = this.DaysInYearBeforeMinSupportedYear - 1;
			int num5 = num2 - 1 - num4 % 7;
			int num6 = (firstDayOfWeek - num5 + 14) % 7;
			int num7 = num4 - num6;
			if (num6 >= minimumDaysInFirstWeek)
			{
				num7 += 7;
			}
			return num7 / 7 + 1;
		}

		/// <summary>Gets the number of days in the year that precedes the year that is specified by the <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> property.</summary>
		/// <returns>The number of days in the year that precedes the year specified by <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" />.</returns>
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x0600300C RID: 12300 RVA: 0x000AB98A File Offset: 0x000A9B8A
		protected virtual int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 365;
			}
		}

		/// <summary>Returns the week of the year that includes the date in the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <returns>A positive integer that represents the week of the year that includes the date in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">A date and time value. </param>
		/// <param name="rule">An enumeration value that defines a calendar week. </param>
		/// <param name="firstDayOfWeek">An enumeration value that represents the first day of the week. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is earlier than <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> or later than <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />.-or-<paramref name="firstDayOfWeek" /> is not a valid <see cref="T:System.DayOfWeek" /> value.-or- <paramref name="rule" /> is not a valid <see cref="T:System.Globalization.CalendarWeekRule" /> value. </exception>
		// Token: 0x0600300D RID: 12301 RVA: 0x000AB994 File Offset: 0x000A9B94
		public virtual int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			if (firstDayOfWeek < DayOfWeek.Sunday || firstDayOfWeek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("firstDayOfWeek", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[]
				{
					DayOfWeek.Sunday,
					DayOfWeek.Saturday
				}));
			}
			switch (rule)
			{
			case CalendarWeekRule.FirstDay:
				return this.GetFirstDayWeekOfYear(time, (int)firstDayOfWeek);
			case CalendarWeekRule.FirstFullWeek:
				return this.GetWeekOfYearFullDays(time, (int)firstDayOfWeek, 7);
			case CalendarWeekRule.FirstFourDayWeek:
				return this.GetWeekOfYearFullDays(time, (int)firstDayOfWeek, 4);
			default:
				throw new ArgumentOutOfRangeException("rule", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[]
				{
					CalendarWeekRule.FirstDay,
					CalendarWeekRule.FirstFourDayWeek
				}));
			}
		}

		/// <summary>When overridden in a derived class, returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the year in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x0600300E RID: 12302
		public abstract int GetYear(DateTime time);

		/// <summary>Determines whether the specified date in the current era is a leap day.</summary>
		/// <returns>true if the specified day is a leap day; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">A positive integer that represents the month. </param>
		/// <param name="day">A positive integer that represents the day. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar. </exception>
		// Token: 0x0600300F RID: 12303 RVA: 0x000ABA33 File Offset: 0x000A9C33
		public virtual bool IsLeapDay(int year, int month, int day)
		{
			return this.IsLeapDay(year, month, day, 0);
		}

		/// <summary>When overridden in a derived class, determines whether the specified date in the specified era is a leap day.</summary>
		/// <returns>true if the specified day is a leap day; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">A positive integer that represents the month. </param>
		/// <param name="day">A positive integer that represents the day. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003010 RID: 12304
		public abstract bool IsLeapDay(int year, int month, int day, int era);

		/// <summary>Determines whether the specified month in the specified year in the current era is a leap month.</summary>
		/// <returns>true if the specified month is a leap month; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">A positive integer that represents the month. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003011 RID: 12305 RVA: 0x000ABA3F File Offset: 0x000A9C3F
		public virtual bool IsLeapMonth(int year, int month)
		{
			return this.IsLeapMonth(year, month, 0);
		}

		/// <summary>When overridden in a derived class, determines whether the specified month in the specified year in the specified era is a leap month.</summary>
		/// <returns>true if the specified month is a leap month; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">A positive integer that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003012 RID: 12306
		public abstract bool IsLeapMonth(int year, int month, int era);

		/// <summary>Calculates the leap month for a specified year.</summary>
		/// <returns>A positive integer that indicates the leap month in the specified year.-or-Zero if this calendar does not support a leap month or if the <paramref name="year" /> parameter does not represent a leap year.</returns>
		/// <param name="year">A year.</param>
		// Token: 0x06003013 RID: 12307 RVA: 0x000ABA4A File Offset: 0x000A9C4A
		[ComVisible(false)]
		public virtual int GetLeapMonth(int year)
		{
			return this.GetLeapMonth(year, 0);
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <returns>A positive integer that indicates the leap month in the specified year and era.-or-Zero if this calendar does not support a leap month or if the <paramref name="year" /> and <paramref name="era" /> parameters do not specify a leap year.</returns>
		/// <param name="year">A year.</param>
		/// <param name="era">An era.</param>
		// Token: 0x06003014 RID: 12308 RVA: 0x000ABA54 File Offset: 0x000A9C54
		[ComVisible(false)]
		public virtual int GetLeapMonth(int year, int era)
		{
			if (!this.IsLeapYear(year, era))
			{
				return 0;
			}
			int monthsInYear = this.GetMonthsInYear(year, era);
			for (int i = 1; i <= monthsInYear; i++)
			{
				if (this.IsLeapMonth(year, i, era))
				{
					return i;
				}
			}
			return 0;
		}

		/// <summary>Determines whether the specified year in the current era is a leap year.</summary>
		/// <returns>true if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003015 RID: 12309 RVA: 0x000ABA90 File Offset: 0x000A9C90
		public virtual bool IsLeapYear(int year)
		{
			return this.IsLeapYear(year, 0);
		}

		/// <summary>When overridden in a derived class, determines whether the specified year in the specified era is a leap year.</summary>
		/// <returns>true if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003016 RID: 12310
		public abstract bool IsLeapYear(int year, int era);

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">A positive integer that represents the month. </param>
		/// <param name="day">A positive integer that represents the day. </param>
		/// <param name="hour">An integer from 0 to 23 that represents the hour. </param>
		/// <param name="minute">An integer from 0 to 59 that represents the minute. </param>
		/// <param name="second">An integer from 0 to 59 that represents the second. </param>
		/// <param name="millisecond">An integer from 0 to 999 that represents the millisecond. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar.-or- <paramref name="hour" /> is less than zero or greater than 23.-or- <paramref name="minute" /> is less than zero or greater than 59.-or- <paramref name="second" /> is less than zero or greater than 59.-or- <paramref name="millisecond" /> is less than zero or greater than 999. </exception>
		// Token: 0x06003017 RID: 12311 RVA: 0x000ABA9C File Offset: 0x000A9C9C
		public virtual DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			return this.ToDateTime(year, month, day, hour, minute, second, millisecond, 0);
		}

		/// <summary>When overridden in a derived class, returns a <see cref="T:System.DateTime" /> that is set to the specified date and time in the specified era.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">A positive integer that represents the month. </param>
		/// <param name="day">A positive integer that represents the day. </param>
		/// <param name="hour">An integer from 0 to 23 that represents the hour. </param>
		/// <param name="minute">An integer from 0 to 59 that represents the minute. </param>
		/// <param name="second">An integer from 0 to 59 that represents the second. </param>
		/// <param name="millisecond">An integer from 0 to 999 that represents the millisecond. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar.-or- <paramref name="hour" /> is less than zero or greater than 23.-or- <paramref name="minute" /> is less than zero or greater than 59.-or- <paramref name="second" /> is less than zero or greater than 59.-or- <paramref name="millisecond" /> is less than zero or greater than 999.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003018 RID: 12312
		public abstract DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era);

		// Token: 0x06003019 RID: 12313 RVA: 0x000ABABC File Offset: 0x000A9CBC
		internal virtual bool TryToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era, out DateTime result)
		{
			result = DateTime.MinValue;
			bool flag;
			try
			{
				result = this.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
				flag = true;
			}
			catch (ArgumentException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000ABB0C File Offset: 0x000A9D0C
		internal virtual bool IsValidYear(int year, int era)
		{
			return year >= this.GetYear(this.MinSupportedDateTime) && year <= this.GetYear(this.MaxSupportedDateTime);
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000ABB31 File Offset: 0x000A9D31
		internal virtual bool IsValidMonth(int year, int month, int era)
		{
			return this.IsValidYear(year, era) && month >= 1 && month <= this.GetMonthsInYear(year, era);
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000ABB51 File Offset: 0x000A9D51
		internal virtual bool IsValidDay(int year, int month, int day, int era)
		{
			return this.IsValidMonth(year, month, era) && day >= 1 && day <= this.GetDaysInMonth(year, month, era);
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.Calendar" /> object is read-only.</exception>
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x0600301D RID: 12317 RVA: 0x000ABB75 File Offset: 0x000A9D75
		// (set) Token: 0x0600301E RID: 12318 RVA: 0x000ABB7D File Offset: 0x000A9D7D
		public virtual int TwoDigitYearMax
		{
			get
			{
				return this.twoDigitYearMax;
			}
			set
			{
				this.VerifyWritable();
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.Calendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x0600301F RID: 12319 RVA: 0x000ABB8C File Offset: 0x000A9D8C
		public virtual int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Non-negative number required."));
			}
			if (year < 100)
			{
				return (this.TwoDigitYearMax / 100 - ((year > this.TwoDigitYearMax % 100) ? 1 : 0)) * 100 + year;
			}
			return year;
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000ABBD8 File Offset: 0x000A9DD8
		internal static long TimeToTicks(int hour, int minute, int second, int millisecond)
		{
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Hour, Minute, and Second parameters describe an un-representable DateTime."));
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 999));
			}
			return TimeSpan.TimeToTicks(hour, minute, second) + (long)millisecond * 10000L;
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000ABC60 File Offset: 0x000A9E60
		[SecuritySafeCritical]
		internal static int GetSystemTwoDigitYearSetting(int CalID, int defaultYearValue)
		{
			int num = CalendarData.nativeGetTwoDigitYearMax(CalID);
			if (num < 0)
			{
				num = defaultYearValue;
			}
			return num;
		}

		// Token: 0x040018C4 RID: 6340
		internal const long TicksPerMillisecond = 10000L;

		// Token: 0x040018C5 RID: 6341
		internal const long TicksPerSecond = 10000000L;

		// Token: 0x040018C6 RID: 6342
		internal const long TicksPerMinute = 600000000L;

		// Token: 0x040018C7 RID: 6343
		internal const long TicksPerHour = 36000000000L;

		// Token: 0x040018C8 RID: 6344
		internal const long TicksPerDay = 864000000000L;

		// Token: 0x040018C9 RID: 6345
		internal const int MillisPerSecond = 1000;

		// Token: 0x040018CA RID: 6346
		internal const int MillisPerMinute = 60000;

		// Token: 0x040018CB RID: 6347
		internal const int MillisPerHour = 3600000;

		// Token: 0x040018CC RID: 6348
		internal const int MillisPerDay = 86400000;

		// Token: 0x040018CD RID: 6349
		internal const int DaysPerYear = 365;

		// Token: 0x040018CE RID: 6350
		internal const int DaysPer4Years = 1461;

		// Token: 0x040018CF RID: 6351
		internal const int DaysPer100Years = 36524;

		// Token: 0x040018D0 RID: 6352
		internal const int DaysPer400Years = 146097;

		// Token: 0x040018D1 RID: 6353
		internal const int DaysTo10000 = 3652059;

		// Token: 0x040018D2 RID: 6354
		internal const long MaxMillis = 315537897600000L;

		// Token: 0x040018D3 RID: 6355
		internal const int CAL_GREGORIAN = 1;

		// Token: 0x040018D4 RID: 6356
		internal const int CAL_GREGORIAN_US = 2;

		// Token: 0x040018D5 RID: 6357
		internal const int CAL_JAPAN = 3;

		// Token: 0x040018D6 RID: 6358
		internal const int CAL_TAIWAN = 4;

		// Token: 0x040018D7 RID: 6359
		internal const int CAL_KOREA = 5;

		// Token: 0x040018D8 RID: 6360
		internal const int CAL_HIJRI = 6;

		// Token: 0x040018D9 RID: 6361
		internal const int CAL_THAI = 7;

		// Token: 0x040018DA RID: 6362
		internal const int CAL_HEBREW = 8;

		// Token: 0x040018DB RID: 6363
		internal const int CAL_GREGORIAN_ME_FRENCH = 9;

		// Token: 0x040018DC RID: 6364
		internal const int CAL_GREGORIAN_ARABIC = 10;

		// Token: 0x040018DD RID: 6365
		internal const int CAL_GREGORIAN_XLIT_ENGLISH = 11;

		// Token: 0x040018DE RID: 6366
		internal const int CAL_GREGORIAN_XLIT_FRENCH = 12;

		// Token: 0x040018DF RID: 6367
		internal const int CAL_JULIAN = 13;

		// Token: 0x040018E0 RID: 6368
		internal const int CAL_JAPANESELUNISOLAR = 14;

		// Token: 0x040018E1 RID: 6369
		internal const int CAL_CHINESELUNISOLAR = 15;

		// Token: 0x040018E2 RID: 6370
		internal const int CAL_SAKA = 16;

		// Token: 0x040018E3 RID: 6371
		internal const int CAL_LUNAR_ETO_CHN = 17;

		// Token: 0x040018E4 RID: 6372
		internal const int CAL_LUNAR_ETO_KOR = 18;

		// Token: 0x040018E5 RID: 6373
		internal const int CAL_LUNAR_ETO_ROKUYOU = 19;

		// Token: 0x040018E6 RID: 6374
		internal const int CAL_KOREANLUNISOLAR = 20;

		// Token: 0x040018E7 RID: 6375
		internal const int CAL_TAIWANLUNISOLAR = 21;

		// Token: 0x040018E8 RID: 6376
		internal const int CAL_PERSIAN = 22;

		// Token: 0x040018E9 RID: 6377
		internal const int CAL_UMALQURA = 23;

		// Token: 0x040018EA RID: 6378
		internal int m_currentEraValue = -1;

		// Token: 0x040018EB RID: 6379
		[OptionalField(VersionAdded = 2)]
		private bool m_isReadOnly;

		/// <summary>Represents the current era of the current calendar. </summary>
		// Token: 0x040018EC RID: 6380
		public const int CurrentEra = 0;

		// Token: 0x040018ED RID: 6381
		internal int twoDigitYearMax = -1;
	}
}
