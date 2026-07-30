using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents a point in time, typically expressed as a date and time of day, relative to Coordinated Universal Time (UTC).</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000149 RID: 329
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct DateTimeOffset : IComparable, IFormattable, ISerializable, IDeserializationCallback, IComparable<DateTimeOffset>, IEquatable<DateTimeOffset>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified number of ticks and offset.</summary>
		/// <param name="ticks">A date and time expressed as the number of 100-nanosecond intervals that have elapsed since 12:00:00 midnight on January 1, 0001.</param>
		/// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> is not specified in whole minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.DateTimeOffset.UtcDateTime" /> property is earlier than <see cref="F:System.DateTimeOffset.MinValue" /> or later than <see cref="F:System.DateTimeOffset.MaxValue" />.-or-<paramref name="ticks" /> is less than DateTimeOffset.MinValue.Ticks or greater than DateTimeOffset.MaxValue.Ticks.-or-<paramref name="Offset" /> s less than -14 hours or greater than 14 hours.</exception>
		// Token: 0x06000D6C RID: 3436 RVA: 0x00039728 File Offset: 0x00037928
		public DateTimeOffset(long ticks, TimeSpan offset)
		{
			this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			DateTime dateTime = new DateTime(ticks);
			this.m_dateTime = DateTimeOffset.ValidateDate(dateTime, offset);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="dateTime">A date and time.   </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The Coordinated Universal Time (UTC) date and time that results from applying the offset is earlier than <see cref="F:System.DateTimeOffset.MinValue" />.-or-The UTC date and time that results from applying the offset is later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000D6D RID: 3437 RVA: 0x00039758 File Offset: 0x00037958
		public DateTimeOffset(DateTime dateTime)
		{
			TimeSpan localUtcOffset;
			if (dateTime.Kind != DateTimeKind.Utc)
			{
				localUtcOffset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
			}
			else
			{
				localUtcOffset = new TimeSpan(0L);
			}
			this.m_offsetMinutes = DateTimeOffset.ValidateOffset(localUtcOffset);
			this.m_dateTime = DateTimeOffset.ValidateDate(dateTime, localUtcOffset);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified <see cref="T:System.DateTime" /> value and offset.</summary>
		/// <param name="dateTime">A date and time.   </param>
		/// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateTime.Kind" /> equals <see cref="F:System.DateTimeKind.Utc" /> and <paramref name="offset" /> does not equal zero.-or-<paramref name="dateTime.Kind" /> equals <see cref="F:System.DateTimeKind.Local" /> and <paramref name="offset" /> does not equal the offset of the system's local time zone.-or-<paramref name="offset" /> is not specified in whole minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than -14 hours or greater than 14 hours.-or-<see cref="P:System.DateTimeOffset.UtcDateTime" /> is less than <see cref="F:System.DateTimeOffset.MinValue" /> or greater than <see cref="F:System.DateTimeOffset.MaxValue" />. </exception>
		// Token: 0x06000D6E RID: 3438 RVA: 0x0003979C File Offset: 0x0003799C
		public DateTimeOffset(DateTime dateTime, TimeSpan offset)
		{
			if (dateTime.Kind == DateTimeKind.Local)
			{
				if (offset != TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime))
				{
					throw new ArgumentException(Environment.GetResourceString("The UTC Offset of the local dateTime parameter does not match the offset argument."), "offset");
				}
			}
			else if (dateTime.Kind == DateTimeKind.Utc && offset != TimeSpan.Zero)
			{
				throw new ArgumentException(Environment.GetResourceString("The UTC Offset for Utc DateTime instances must be 0."), "offset");
			}
			this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			this.m_dateTime = DateTimeOffset.ValidateDate(dateTime, offset);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified year, month, day, hour, minute, second, and offset.</summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).   </param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> does not represent whole minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than one or greater than 9999.-or-<paramref name="month" /> is less than one or greater than 12.-or-<paramref name="day" /> is less than one or greater than the number of days in <paramref name="month" />.-or-<paramref name="hour" /> is less than zero or greater than 23.-or-<paramref name="minute" /> is less than 0 or greater than 59.-or-<paramref name="second" /> is less than 0 or greater than 59.-or-<paramref name="offset" /> is less than -14 hours or greater than 14 hours.-or-The <see cref="P:System.DateTimeOffset.UtcDateTime" /> property is earlier than <see cref="F:System.DateTimeOffset.MinValue" /> or later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000D6F RID: 3439 RVA: 0x0003981C File Offset: 0x00037A1C
		public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, TimeSpan offset)
		{
			this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			this.m_dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second), offset);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified year, month, day, hour, minute, second, millisecond, and offset.</summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).   </param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="millisecond">The milliseconds (0 through 999).</param>
		/// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> does not represent whole minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than one or greater than 9999.-or-<paramref name="month" /> is less than one or greater than 12.-or-<paramref name="day" /> is less than one or greater than the number of days in <paramref name="month" />.-or-<paramref name="hour" /> is less than zero or greater than 23.-or-<paramref name="minute" /> is less than 0 or greater than 59.-or-<paramref name="second" /> is less than 0 or greater than 59.-or-<paramref name="millisecond" /> is less than 0 or greater than 999.-or-<paramref name="offset" /> is less than -14 or greater than 14.-or-The <see cref="P:System.DateTimeOffset.UtcDateTime" /> property is earlier than <see cref="F:System.DateTimeOffset.MinValue" /> or later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000D70 RID: 3440 RVA: 0x00039846 File Offset: 0x00037A46
		public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, TimeSpan offset)
		{
			this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			this.m_dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second, millisecond), offset);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified year, month, day, hour, minute, second, millisecond, and offset of a specified calendar.</summary>
		/// <param name="year">The year.</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).   </param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="millisecond">The milliseconds (0 through 999).</param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />.</param>
		/// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> does not represent whole minutes.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> cannot be null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than the <paramref name="calendar" /> parameter's MinSupportedDateTime.Year or greater than MaxSupportedDateTime.Year.-or-<paramref name="month" /> is either less than or greater than the number of months in <paramref name="year" /> in the <paramref name="calendar" />. -or-<paramref name="day" /> is less than one or greater than the number of days in <paramref name="month" />.-or-<paramref name="hour" /> is less than zero or greater than 23.-or-<paramref name="minute" /> is less than 0 or greater than 59.-or-<paramref name="second" /> is less than 0 or greater than 59.-or-<paramref name="millisecond" /> is less than 0 or greater than 999.-or-<paramref name="offset" /> is less than -14 hours or greater than 14 hours.-or-The <paramref name="year" />, <paramref name="month" />, and <paramref name="day" /> parameters cannot be represented as a date and time value.-or-The <see cref="P:System.DateTimeOffset.UtcDateTime" /> property is earlier than <see cref="F:System.DateTimeOffset.MinValue" /> or later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000D71 RID: 3441 RVA: 0x00039874 File Offset: 0x00037A74
		public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, TimeSpan offset)
		{
			this.m_offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			this.m_dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second, millisecond, calendar), offset);
		}

		/// <summary>Gets a <see cref="T:System.DateTimeOffset" /> object that is set to the current date and time on the current computer, with the offset set to the local time's offset from Coordinated Universal Time (UTC).</summary>
		/// <returns>A <see cref="T:System.DateTimeOffset" /> object whose date and time is the current local time and whose offset is the local time zone's offset from Coordinated Universal Time (UTC).</returns>
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x000398AD File Offset: 0x00037AAD
		public static DateTimeOffset Now
		{
			get
			{
				return new DateTimeOffset(DateTime.Now);
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTimeOffset" /> object whose date and time are set to the current Coordinated Universal Time (UTC) date and time and whose offset is <see cref="F:System.TimeSpan.Zero" />.</summary>
		/// <returns>An object whose date and time is the current Coordinated Universal Time (UTC) and whose offset is <see cref="F:System.TimeSpan.Zero" />.</returns>
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x000398B9 File Offset: 0x00037AB9
		public static DateTimeOffset UtcNow
		{
			get
			{
				return new DateTimeOffset(DateTime.UtcNow);
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> value that represents the date and time of the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The date and time of the current <see cref="T:System.DateTimeOffset" /> object.</returns>
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x000398C5 File Offset: 0x00037AC5
		public DateTime DateTime
		{
			get
			{
				return this.ClockDateTime;
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> value that represents the Coordinated Universal Time (UTC) date and time of the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The Coordinated Universal Time (UTC) date and time of the current <see cref="T:System.DateTimeOffset" /> object.</returns>
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x000398CD File Offset: 0x00037ACD
		public DateTime UtcDateTime
		{
			get
			{
				return DateTime.SpecifyKind(this.m_dateTime, DateTimeKind.Utc);
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> value that represents the local date and time of the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The local date and time of the current <see cref="T:System.DateTimeOffset" /> object.</returns>
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x000398DC File Offset: 0x00037ADC
		public DateTime LocalDateTime
		{
			get
			{
				return this.UtcDateTime.ToLocalTime();
			}
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTimeOffset" /> object to the date and time specified by an offset value.</summary>
		/// <returns>An object that is equal to the original <see cref="T:System.DateTimeOffset" /> object (that is, their <see cref="M:System.DateTimeOffset.ToUniversalTime" /> methods return identical points in time) but whose <see cref="P:System.DateTimeOffset.Offset" /> property is set to <paramref name="offset" />.</returns>
		/// <param name="offset">The offset to convert the <see cref="T:System.DateTimeOffset" /> value to.   </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTimeOffset" /> object has a <see cref="P:System.DateTimeOffset.DateTime" /> value earlier than <see cref="F:System.DateTimeOffset.MinValue" />.-or-The resulting <see cref="T:System.DateTimeOffset" /> object has a <see cref="P:System.DateTimeOffset.DateTime" /> value later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than -14 hours.-or-<paramref name="offset" /> is greater than 14 hours.</exception>
		// Token: 0x06000D77 RID: 3447 RVA: 0x000398F8 File Offset: 0x00037AF8
		public DateTimeOffset ToOffset(TimeSpan offset)
		{
			return new DateTimeOffset((this.m_dateTime + offset).Ticks, offset);
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x00039920 File Offset: 0x00037B20
		private DateTime ClockDateTime
		{
			get
			{
				return new DateTime((this.m_dateTime + this.Offset).Ticks, DateTimeKind.Unspecified);
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> value that represents the date component of the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value that represents the date component of the current <see cref="T:System.DateTimeOffset" /> object.</returns>
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x0003994C File Offset: 0x00037B4C
		public DateTime Date
		{
			get
			{
				return this.ClockDateTime.Date;
			}
		}

		/// <summary>Gets the day of the month represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The day component of the current <see cref="T:System.DateTimeOffset" /> object, expressed as a value between 1 and 31.</returns>
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00039968 File Offset: 0x00037B68
		public int Day
		{
			get
			{
				return this.ClockDateTime.Day;
			}
		}

		/// <summary>Gets the day of the week represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>One of the enumeration values that indicates the day of the week of the current <see cref="T:System.DateTimeOffset" /> object.</returns>
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00039984 File Offset: 0x00037B84
		public DayOfWeek DayOfWeek
		{
			get
			{
				return this.ClockDateTime.DayOfWeek;
			}
		}

		/// <summary>Gets the day of the year represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The day of the year of the current <see cref="T:System.DateTimeOffset" /> object, expressed as a value between 1 and 366.</returns>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x000399A0 File Offset: 0x00037BA0
		public int DayOfYear
		{
			get
			{
				return this.ClockDateTime.DayOfYear;
			}
		}

		/// <summary>Gets the hour component of the time represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The hour component of the current <see cref="T:System.DateTimeOffset" /> object. This property uses a 24-hour clock; the value ranges from 0 to 23.</returns>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x000399BC File Offset: 0x00037BBC
		public int Hour
		{
			get
			{
				return this.ClockDateTime.Hour;
			}
		}

		/// <summary>Gets the millisecond component of the time represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The millisecond component of the current <see cref="T:System.DateTimeOffset" /> object, expressed as an integer between 0 and 999.</returns>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x000399D8 File Offset: 0x00037BD8
		public int Millisecond
		{
			get
			{
				return this.ClockDateTime.Millisecond;
			}
		}

		/// <summary>Gets the minute component of the time represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The minute component of the current <see cref="T:System.DateTimeOffset" /> object, expressed as an integer between 0 and 59.</returns>
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x000399F4 File Offset: 0x00037BF4
		public int Minute
		{
			get
			{
				return this.ClockDateTime.Minute;
			}
		}

		/// <summary>Gets the month component of the date represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The month component of the current <see cref="T:System.DateTimeOffset" /> object, expressed as an integer between 1 and 12.</returns>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x00039A10 File Offset: 0x00037C10
		public int Month
		{
			get
			{
				return this.ClockDateTime.Month;
			}
		}

		/// <summary>Gets the time's offset from Coordinated Universal Time (UTC). </summary>
		/// <returns>The difference between the current <see cref="T:System.DateTimeOffset" /> object's time value and Coordinated Universal Time (UTC).</returns>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x00039A2B File Offset: 0x00037C2B
		public TimeSpan Offset
		{
			get
			{
				return new TimeSpan(0, (int)this.m_offsetMinutes, 0);
			}
		}

		/// <summary>Gets the second component of the clock time represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The second component of the <see cref="T:System.DateTimeOffset" /> object, expressed as an integer value between 0 and 59.</returns>
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00039A3C File Offset: 0x00037C3C
		public int Second
		{
			get
			{
				return this.ClockDateTime.Second;
			}
		}

		/// <summary>Gets the number of ticks that represents the date and time of the current <see cref="T:System.DateTimeOffset" /> object in clock time.</summary>
		/// <returns>The number of ticks in the <see cref="T:System.DateTimeOffset" /> object's clock time.</returns>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x00039A58 File Offset: 0x00037C58
		public long Ticks
		{
			get
			{
				return this.ClockDateTime.Ticks;
			}
		}

		/// <summary>Gets the number of ticks that represents the date and time of the current <see cref="T:System.DateTimeOffset" /> object in Coordinated Universal Time (UTC).</summary>
		/// <returns>The number of ticks in the <see cref="T:System.DateTimeOffset" /> object's Coordinated Universal Time (UTC).</returns>
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00039A74 File Offset: 0x00037C74
		public long UtcTicks
		{
			get
			{
				return this.UtcDateTime.Ticks;
			}
		}

		/// <summary>Gets the time of day for the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The time interval of the current date that has elapsed since midnight.</returns>
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00039A90 File Offset: 0x00037C90
		public TimeSpan TimeOfDay
		{
			get
			{
				return this.ClockDateTime.TimeOfDay;
			}
		}

		/// <summary>Gets the year component of the date represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The year component of the current <see cref="T:System.DateTimeOffset" /> object, expressed as an integer value between 0 and 9999.</returns>
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x00039AAC File Offset: 0x00037CAC
		public int Year
		{
			get
			{
				return this.ClockDateTime.Year;
			}
		}

		/// <summary>Adds a specified time interval to a <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the time interval represented by <paramref name="timeSpan" />.</returns>
		/// <param name="timeSpan">A <see cref="T:System.TimeSpan" /> object that represents a positive or a negative time interval.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.-or- The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000D87 RID: 3463 RVA: 0x00039AC8 File Offset: 0x00037CC8
		public DateTimeOffset Add(TimeSpan timeSpan)
		{
			return new DateTimeOffset(this.ClockDateTime.Add(timeSpan), this.Offset);
		}

		/// <summary>Adds a specified number of whole and fractional days to the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the number of days represented by <paramref name="days" />.</returns>
		/// <param name="days">A number of whole and fractional days. The number can be negative or positive.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.-or- The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000D88 RID: 3464 RVA: 0x00039AF0 File Offset: 0x00037CF0
		public DateTimeOffset AddDays(double days)
		{
			return new DateTimeOffset(this.ClockDateTime.AddDays(days), this.Offset);
		}

		/// <summary>Adds a specified number of whole and fractional hours to the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the number of hours represented by <paramref name="hours" />.</returns>
		/// <param name="hours">A number of whole and fractional hours. The number can be negative or positive.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.-or- The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000D89 RID: 3465 RVA: 0x00039B18 File Offset: 0x00037D18
		public DateTimeOffset AddHours(double hours)
		{
			return new DateTimeOffset(this.ClockDateTime.AddHours(hours), this.Offset);
		}

		/// <summary>Adds a specified number of milliseconds to the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the number of whole milliseconds represented by <paramref name="milliseconds" />.</returns>
		/// <param name="milliseconds">A number of whole and fractional milliseconds. The number can be negative or positive.   </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.-or- The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000D8A RID: 3466 RVA: 0x00039B40 File Offset: 0x00037D40
		public DateTimeOffset AddMilliseconds(double milliseconds)
		{
			return new DateTimeOffset(this.ClockDateTime.AddMilliseconds(milliseconds), this.Offset);
		}

		/// <summary>Adds a specified number of whole and fractional minutes to the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the number of minutes represented by <paramref name="minutes" />.</returns>
		/// <param name="minutes">A number of whole and fractional minutes. The number can be negative or positive.   </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.-or- The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000D8B RID: 3467 RVA: 0x00039B68 File Offset: 0x00037D68
		public DateTimeOffset AddMinutes(double minutes)
		{
			return new DateTimeOffset(this.ClockDateTime.AddMinutes(minutes), this.Offset);
		}

		/// <summary>Adds a specified number of months to the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the number of months represented by <paramref name="months" />.</returns>
		/// <param name="months">A number of whole months. The number can be negative or positive.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.-or- The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000D8C RID: 3468 RVA: 0x00039B90 File Offset: 0x00037D90
		public DateTimeOffset AddMonths(int months)
		{
			return new DateTimeOffset(this.ClockDateTime.AddMonths(months), this.Offset);
		}

		/// <summary>Adds a specified number of whole and fractional seconds to the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the number of seconds represented by <paramref name="seconds" />.</returns>
		/// <param name="seconds">A number of whole and fractional seconds. The number can be negative or positive.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.-or- The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000D8D RID: 3469 RVA: 0x00039BB8 File Offset: 0x00037DB8
		public DateTimeOffset AddSeconds(double seconds)
		{
			return new DateTimeOffset(this.ClockDateTime.AddSeconds(seconds), this.Offset);
		}

		/// <summary>Adds a specified number of ticks to the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the number of ticks represented by <paramref name="ticks" />.</returns>
		/// <param name="ticks">A number of 100-nanosecond ticks. The number can be negative or positive.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.-or- The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000D8E RID: 3470 RVA: 0x00039BE0 File Offset: 0x00037DE0
		public DateTimeOffset AddTicks(long ticks)
		{
			return new DateTimeOffset(this.ClockDateTime.AddTicks(ticks), this.Offset);
		}

		/// <summary>Adds a specified number of years to the <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the number of years represented by <paramref name="years" />.</returns>
		/// <param name="years">A number of years. The number can be negative or positive.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.-or- The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000D8F RID: 3471 RVA: 0x00039C08 File Offset: 0x00037E08
		public DateTimeOffset AddYears(int years)
		{
			return new DateTimeOffset(this.ClockDateTime.AddYears(years), this.Offset);
		}

		/// <summary>Compares two <see cref="T:System.DateTimeOffset" /> objects and indicates whether the first is earlier than the second, equal to the second, or later than the second.</summary>
		/// <returns>A signed integer that indicates whether the value of the <paramref name="first" /> parameter is earlier than, later than, or the same time as the value of the <paramref name="second" /> parameter, as the following table shows.Return valueMeaningLess than zero<paramref name="first" /> is earlier than <paramref name="second" />.Zero<paramref name="first" /> is equal to <paramref name="second" />.Greater than zero<paramref name="first" /> is later than <paramref name="second" />.</returns>
		/// <param name="first">The first object to compare.</param>
		/// <param name="second">The second object to compare.</param>
		// Token: 0x06000D90 RID: 3472 RVA: 0x00039C2F File Offset: 0x00037E2F
		public static int Compare(DateTimeOffset first, DateTimeOffset second)
		{
			return DateTime.Compare(first.UtcDateTime, second.UtcDateTime);
		}

		/// <summary>Compares the value of the current <see cref="T:System.DateTimeOffset" /> object with another object of the same type.</summary>
		/// <returns>A 32-bit signed integer that indicates whether the current <see cref="T:System.DateTimeOffset" /> object is less than, equal to, or greater than <paramref name="obj" />. The return values of the method are interpreted as follows:Return ValueDescriptionLess than zeroThe current <see cref="T:System.DateTimeOffset" /> object is less than (earlier than) <paramref name="obj" />.ZeroThe current <see cref="T:System.DateTimeOffset" /> object is equal to (the same point in time as) <paramref name="obj" />.Greater than zeroThe current <see cref="T:System.DateTimeOffset" /> object is greater than (later than) <paramref name="obj" />.</returns>
		/// <param name="obj">The object to compare with the current <see cref="T:System.DateTimeOffset" /> object.</param>
		// Token: 0x06000D91 RID: 3473 RVA: 0x00039C44 File Offset: 0x00037E44
		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is DateTimeOffset))
			{
				throw new ArgumentException(Environment.GetResourceString("Object must be of type DateTimeOffset."));
			}
			DateTime utcDateTime = ((DateTimeOffset)obj).UtcDateTime;
			DateTime utcDateTime2 = this.UtcDateTime;
			if (utcDateTime2 > utcDateTime)
			{
				return 1;
			}
			if (utcDateTime2 < utcDateTime)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares the current <see cref="T:System.DateTimeOffset" /> object to a specified <see cref="T:System.DateTimeOffset" /> object and indicates whether the current object is earlier than, the same as, or later than the second <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>A signed integer that indicates the relationship between the current <see cref="T:System.DateTimeOffset" /> object and <paramref name="other" />, as the following table shows.Return ValueDescriptionLess than zeroThe current <see cref="T:System.DateTimeOffset" /> object is earlier than <paramref name="other" />.ZeroThe current <see cref="T:System.DateTimeOffset" /> object is the same as <paramref name="other" />.Greater than zero.The current <see cref="T:System.DateTimeOffset" /> object is later than <paramref name="other" />.</returns>
		/// <param name="other">An object to compare with the current <see cref="T:System.DateTimeOffset" /> object.</param>
		// Token: 0x06000D92 RID: 3474 RVA: 0x00039C9C File Offset: 0x00037E9C
		public int CompareTo(DateTimeOffset other)
		{
			DateTime utcDateTime = other.UtcDateTime;
			DateTime utcDateTime2 = this.UtcDateTime;
			if (utcDateTime2 > utcDateTime)
			{
				return 1;
			}
			if (utcDateTime2 < utcDateTime)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Determines whether a <see cref="T:System.DateTimeOffset" /> object represents the same point in time as a specified object.</summary>
		/// <returns>true if the <paramref name="obj" /> parameter is a <see cref="T:System.DateTimeOffset" /> object and represents the same point in time as the current <see cref="T:System.DateTimeOffset" /> object; otherwise, false.</returns>
		/// <param name="obj">The object to compare to the current <see cref="T:System.DateTimeOffset" /> object.</param>
		// Token: 0x06000D93 RID: 3475 RVA: 0x00039CD0 File Offset: 0x00037ED0
		public override bool Equals(object obj)
		{
			return obj is DateTimeOffset && this.UtcDateTime.Equals(((DateTimeOffset)obj).UtcDateTime);
		}

		/// <summary>Determines whether the current <see cref="T:System.DateTimeOffset" /> object represents the same point in time as a specified <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>true if both <see cref="T:System.DateTimeOffset" /> objects have the same <see cref="P:System.DateTimeOffset.UtcDateTime" /> value; otherwise, false.</returns>
		/// <param name="other">An object to compare to the current <see cref="T:System.DateTimeOffset" /> object.   </param>
		// Token: 0x06000D94 RID: 3476 RVA: 0x00039D04 File Offset: 0x00037F04
		public bool Equals(DateTimeOffset other)
		{
			return this.UtcDateTime.Equals(other.UtcDateTime);
		}

		/// <summary>Determines whether the current <see cref="T:System.DateTimeOffset" /> object represents the same time and has the same offset as a specified <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>true if the current <see cref="T:System.DateTimeOffset" /> object and <paramref name="other" /> have the same date and time value and the same <see cref="P:System.DateTimeOffset.Offset" /> value; otherwise, false.</returns>
		/// <param name="other">The object to compare to the current <see cref="T:System.DateTimeOffset" /> object. </param>
		// Token: 0x06000D95 RID: 3477 RVA: 0x00039D28 File Offset: 0x00037F28
		public bool EqualsExact(DateTimeOffset other)
		{
			return this.ClockDateTime == other.ClockDateTime && this.Offset == other.Offset && this.ClockDateTime.Kind == other.ClockDateTime.Kind;
		}

		/// <summary>Determines whether two specified <see cref="T:System.DateTimeOffset" /> objects represent the same point in time.</summary>
		/// <returns>true if the two <see cref="T:System.DateTimeOffset" /> objects have the same <see cref="P:System.DateTimeOffset.UtcDateTime" /> value; otherwise, false.</returns>
		/// <param name="first">The first object to compare.</param>
		/// <param name="second">The second object to compare.</param>
		// Token: 0x06000D96 RID: 3478 RVA: 0x00039D7E File Offset: 0x00037F7E
		public static bool Equals(DateTimeOffset first, DateTimeOffset second)
		{
			return DateTime.Equals(first.UtcDateTime, second.UtcDateTime);
		}

		/// <summary>Converts the specified Windows file time to an equivalent local time.</summary>
		/// <returns>An object that represents the date and time of <paramref name="fileTime" /> with the offset set to the local time offset.</returns>
		/// <param name="fileTime">A Windows file time, expressed in ticks.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="filetime" /> is less than zero.-or-<paramref name="filetime" /> is greater than DateTimeOffset.MaxValue.Ticks.</exception>
		// Token: 0x06000D97 RID: 3479 RVA: 0x00039D93 File Offset: 0x00037F93
		public static DateTimeOffset FromFileTime(long fileTime)
		{
			return new DateTimeOffset(DateTime.FromFileTime(fileTime));
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00039DA0 File Offset: 0x00037FA0
		public static DateTimeOffset FromUnixTimeSeconds(long seconds)
		{
			if (seconds < -62135596800L || seconds > 253402300799L)
			{
				throw new ArgumentOutOfRangeException("seconds", string.Format(Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), -62135596800L, 253402300799L));
			}
			return new DateTimeOffset(seconds * 10000000L + 621355968000000000L, TimeSpan.Zero);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00039E18 File Offset: 0x00038018
		public static DateTimeOffset FromUnixTimeMilliseconds(long milliseconds)
		{
			if (milliseconds < -62135596800000L || milliseconds > 253402300799999L)
			{
				throw new ArgumentOutOfRangeException("milliseconds", string.Format(Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), -62135596800000L, 253402300799999L));
			}
			return new DateTimeOffset(milliseconds * 10000L + 621355968000000000L, TimeSpan.Zero);
		}

		/// <summary>Runs when the deserialization of an object has been completed.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		// Token: 0x06000D9A RID: 3482 RVA: 0x00039E90 File Offset: 0x00038090
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			try
			{
				this.m_offsetMinutes = DateTimeOffset.ValidateOffset(this.Offset);
				this.m_dateTime = DateTimeOffset.ValidateDate(this.ClockDateTime, this.Offset);
			}
			catch (ArgumentException ex)
			{
				throw new SerializationException(Environment.GetResourceString("An error occurred while deserializing the object.  The serialized data is corrupt."), ex);
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data required to serialize the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <param name="info">The object to populate with data.</param>
		/// <param name="context">The destination for this serialization (see <see cref="T:System.Runtime.Serialization.StreamingContext" />).</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
		// Token: 0x06000D9B RID: 3483 RVA: 0x00039EEC File Offset: 0x000380EC
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("DateTime", this.m_dateTime);
			info.AddValue("OffsetMinutes", this.m_offsetMinutes);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00039F20 File Offset: 0x00038120
		private DateTimeOffset(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_dateTime = (DateTime)info.GetValue("DateTime", typeof(DateTime));
			this.m_offsetMinutes = (short)info.GetValue("OffsetMinutes", typeof(short));
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000D9D RID: 3485 RVA: 0x00039F7C File Offset: 0x0003817C
		public override int GetHashCode()
		{
			return this.UtcDateTime.GetHashCode();
		}

		/// <summary>Converts the specified string representation of a date, time, and offset to its <see cref="T:System.DateTimeOffset" /> equivalent.</summary>
		/// <returns>An object that is equivalent to the date and time that is contained in <paramref name="input" />.</returns>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <exception cref="T:System.ArgumentException">The offset is greater than 14 hours or less than -14 hours.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> does not contain a valid string representation of a date and time.-or-<paramref name="input" /> contains the string representation of an offset value without a date or time.</exception>
		// Token: 0x06000D9E RID: 3486 RVA: 0x00039F98 File Offset: 0x00038198
		public static DateTimeOffset Parse(string input)
		{
			TimeSpan timeSpan;
			return new DateTimeOffset(DateTimeParse.Parse(input, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out timeSpan).Ticks, timeSpan);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified culture-specific format information.</summary>
		/// <returns>An object that is equivalent to the date and time that is contained in <paramref name="input" />, as specified by <paramref name="formatProvider" />.</returns>
		/// <param name="input">A string that contains a date and time to convert.   </param>
		/// <param name="formatProvider">An object that provides culture-specific format information about <paramref name="input" />.</param>
		/// <exception cref="T:System.ArgumentException">The offset is greater than 14 hours or less than -14 hours.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> does not contain a valid string representation of a date and time.-or-<paramref name="input" /> contains the string representation of an offset value without a date or time.</exception>
		// Token: 0x06000D9F RID: 3487 RVA: 0x00039FC1 File Offset: 0x000381C1
		public static DateTimeOffset Parse(string input, IFormatProvider formatProvider)
		{
			return DateTimeOffset.Parse(input, formatProvider, DateTimeStyles.None);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified culture-specific format information and formatting style.</summary>
		/// <returns>An object that is equivalent to the date and time that is contained in <paramref name="input" /> as specified by <paramref name="formatProvider" /> and <paramref name="styles" />.</returns>
		/// <param name="input">A string that contains a date and time to convert.   </param>
		/// <param name="formatProvider">An object that provides culture-specific format information about <paramref name="input" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="input" />. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.   </param>
		/// <exception cref="T:System.ArgumentException">The offset is greater than 14 hours or less than -14 hours.-or-<paramref name="styles" /> is not a valid <see cref="T:System.Globalization.DateTimeStyles" /> value.-or-<paramref name="styles" /> includes an unsupported <see cref="T:System.Globalization.DateTimeStyles" /> value.-or-<paramref name="styles" /> includes <see cref="T:System.Globalization.DateTimeStyles" /> values that cannot be used together.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> does not contain a valid string representation of a date and time.-or-<paramref name="input" /> contains the string representation of an offset value without a date or time.</exception>
		// Token: 0x06000DA0 RID: 3488 RVA: 0x00039FCC File Offset: 0x000381CC
		public static DateTimeOffset Parse(string input, IFormatProvider formatProvider, DateTimeStyles styles)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			TimeSpan timeSpan;
			return new DateTimeOffset(DateTimeParse.Parse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out timeSpan).Ticks, timeSpan);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified format and culture-specific format information. The format of the string representation must match the specified format exactly.</summary>
		/// <returns>An object that is equivalent to the date and time that is contained in <paramref name="input" /> as specified by <paramref name="format" /> and <paramref name="formatProvider" />.</returns>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="format">A format specifier that defines the expected format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information about <paramref name="input" />.</param>
		/// <exception cref="T:System.ArgumentException">The offset is greater than 14 hours or less than -14 hours.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="format" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is an empty string ("").-or-<paramref name="input" /> does not contain a valid string representation of a date and time.-or-<paramref name="format" /> is an empty string.-or-The hour component and the AM/PM designator in <paramref name="input" /> do not agree.</exception>
		// Token: 0x06000DA1 RID: 3489 RVA: 0x0003A003 File Offset: 0x00038203
		public static DateTimeOffset ParseExact(string input, string format, IFormatProvider formatProvider)
		{
			return DateTimeOffset.ParseExact(input, format, formatProvider, DateTimeStyles.None);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly.</summary>
		/// <returns>An object that is equivalent to the date and time that is contained in the <paramref name="input" /> parameter, as specified by the <paramref name="format" />, <paramref name="formatProvider" />, and <paramref name="styles" /> parameters.</returns>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="format">A format specifier that defines the expected format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information about <paramref name="input" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="input" />.</param>
		/// <exception cref="T:System.ArgumentException">The offset is greater than 14 hours or less than -14 hours.-or-The <paramref name="styles" /> parameter includes an unsupported value.-or-The <paramref name="styles" /> parameter contains <see cref="T:System.Globalization.DateTimeStyles" /> values that cannot be used together.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="format" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is an empty string ("").-or-<paramref name="input" /> does not contain a valid string representation of a date and time.-or-<paramref name="format" /> is an empty string.-or-The hour component and the AM/PM designator in <paramref name="input" /> do not agree. </exception>
		// Token: 0x06000DA2 RID: 3490 RVA: 0x0003A010 File Offset: 0x00038210
		public static DateTimeOffset ParseExact(string input, string format, IFormatProvider formatProvider, DateTimeStyles styles)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			TimeSpan timeSpan;
			return new DateTimeOffset(DateTimeParse.ParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out timeSpan).Ticks, timeSpan);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified formats, culture-specific format information, and style. The format of the string representation must match one of the specified formats exactly.</summary>
		/// <returns>An object that is equivalent to the date and time that is contained in the <paramref name="input" /> parameter, as specified by the <paramref name="formats" />, <paramref name="formatProvider" />, and <paramref name="styles" /> parameters.</returns>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="formats">An array of format specifiers that define the expected formats of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information about <paramref name="input" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="input" />.</param>
		/// <exception cref="T:System.ArgumentException">The offset is greater than 14 hours or less than -14 hours.-or-<paramref name="styles" /> includes an unsupported value.-or-The <paramref name="styles" /> parameter contains <see cref="T:System.Globalization.DateTimeStyles" /> values that cannot be used together.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is an empty string ("").-or-<paramref name="input" /> does not contain a valid string representation of a date and time.-or-No element of <paramref name="formats" /> contains a valid format specifier.-or-The hour component and the AM/PM designator in <paramref name="input" /> do not agree. </exception>
		// Token: 0x06000DA3 RID: 3491 RVA: 0x0003A048 File Offset: 0x00038248
		public static DateTimeOffset ParseExact(string input, string[] formats, IFormatProvider formatProvider, DateTimeStyles styles)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			TimeSpan timeSpan;
			return new DateTimeOffset(DateTimeParse.ParseExactMultiple(input, formats, DateTimeFormatInfo.GetInstance(formatProvider), styles, out timeSpan).Ticks, timeSpan);
		}

		/// <summary>Subtracts a <see cref="T:System.DateTimeOffset" /> value that represents a specific date and time from the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>An object that specifies the interval between the two <see cref="T:System.DateTimeOffset" /> objects.</returns>
		/// <param name="value">An object that represents the value to subtract. </param>
		// Token: 0x06000DA4 RID: 3492 RVA: 0x0003A080 File Offset: 0x00038280
		public TimeSpan Subtract(DateTimeOffset value)
		{
			return this.UtcDateTime.Subtract(value.UtcDateTime);
		}

		/// <summary>Subtracts a specified time interval from the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>An object that is equal to the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object, minus the time interval represented by <paramref name="value" />.</returns>
		/// <param name="value">The time interval to subtract.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.-or- The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000DA5 RID: 3493 RVA: 0x0003A0A4 File Offset: 0x000382A4
		public DateTimeOffset Subtract(TimeSpan value)
		{
			return new DateTimeOffset(this.ClockDateTime.Subtract(value), this.Offset);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTimeOffset" /> object to a Windows file time.</summary>
		/// <returns>The value of the current <see cref="T:System.DateTimeOffset" /> object, expressed as a Windows file time.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting file time would represent a date and time before midnight on January 1, 1601 C.E. Coordinated Universal Time (UTC).</exception>
		// Token: 0x06000DA6 RID: 3494 RVA: 0x0003A0CC File Offset: 0x000382CC
		public long ToFileTime()
		{
			return this.UtcDateTime.ToFileTime();
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0003A0E8 File Offset: 0x000382E8
		public long ToUnixTimeSeconds()
		{
			return this.UtcDateTime.Ticks / 10000000L - 62135596800L;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0003A114 File Offset: 0x00038314
		public long ToUnixTimeMilliseconds()
		{
			return this.UtcDateTime.Ticks / 10000L - 62135596800000L;
		}

		/// <summary>Converts the current <see cref="T:System.DateTimeOffset" /> object to a <see cref="T:System.DateTimeOffset" /> object that represents the local time.</summary>
		/// <returns>An object that represents the date and time of the current <see cref="T:System.DateTimeOffset" /> object converted to local time.</returns>
		// Token: 0x06000DA9 RID: 3497 RVA: 0x0003A140 File Offset: 0x00038340
		public DateTimeOffset ToLocalTime()
		{
			return this.ToLocalTime(false);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0003A14C File Offset: 0x0003834C
		internal DateTimeOffset ToLocalTime(bool throwOnOverflow)
		{
			return new DateTimeOffset(this.UtcDateTime.ToLocalTime(throwOnOverflow));
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTimeOffset" /> object to its equivalent string representation.</summary>
		/// <returns>A string representation of a <see cref="T:System.DateTimeOffset" /> object that includes the offset appended at the end of the string.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by the current culture. </exception>
		// Token: 0x06000DAB RID: 3499 RVA: 0x0003A16D File Offset: 0x0003836D
		public override string ToString()
		{
			return DateTimeFormat.Format(this.ClockDateTime, null, DateTimeFormatInfo.CurrentInfo, this.Offset);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTimeOffset" /> object to its equivalent string representation using the specified format.</summary>
		/// <returns>A string representation of the value of the current <see cref="T:System.DateTimeOffset" /> object, as specified by <paramref name="format" />.</returns>
		/// <param name="format">A format string.</param>
		/// <exception cref="T:System.FormatException">The length of <paramref name="format" /> is one, and it is not one of the standard format specifier characters defined for <see cref="T:System.Globalization.DateTimeFormatInfo" />. -or-<paramref name="format" /> does not contain a valid custom format pattern.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by the current culture. </exception>
		// Token: 0x06000DAC RID: 3500 RVA: 0x0003A186 File Offset: 0x00038386
		public string ToString(string format)
		{
			return DateTimeFormat.Format(this.ClockDateTime, format, DateTimeFormatInfo.CurrentInfo, this.Offset);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTimeOffset" /> object to its equivalent string representation using the specified culture-specific formatting information.</summary>
		/// <returns>A string representation of the value of the current <see cref="T:System.DateTimeOffset" /> object, as specified by <paramref name="formatProvider" />.</returns>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by <paramref name="formatProvider" />. </exception>
		// Token: 0x06000DAD RID: 3501 RVA: 0x0003A19F File Offset: 0x0003839F
		public string ToString(IFormatProvider formatProvider)
		{
			return DateTimeFormat.Format(this.ClockDateTime, null, DateTimeFormatInfo.GetInstance(formatProvider), this.Offset);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTimeOffset" /> object to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <returns>A string representation of the value of the current <see cref="T:System.DateTimeOffset" /> object, as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="format">A format string.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.FormatException">The length of <paramref name="format" /> is one, and it is not one of the standard format specifier characters defined for <see cref="T:System.Globalization.DateTimeFormatInfo" />.-or-<paramref name="format" /> does not contain a valid custom format pattern. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by <paramref name="formatProvider" />. </exception>
		// Token: 0x06000DAE RID: 3502 RVA: 0x0003A1B9 File Offset: 0x000383B9
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return DateTimeFormat.Format(this.ClockDateTime, format, DateTimeFormatInfo.GetInstance(formatProvider), this.Offset);
		}

		/// <summary>Converts the current <see cref="T:System.DateTimeOffset" /> object to a <see cref="T:System.DateTimeOffset" /> value that represents the Coordinated Universal Time (UTC).</summary>
		/// <returns>An object that represents the date and time of the current <see cref="T:System.DateTimeOffset" /> object converted to Coordinated Universal Time (UTC).</returns>
		// Token: 0x06000DAF RID: 3503 RVA: 0x0003A1D3 File Offset: 0x000383D3
		public DateTimeOffset ToUniversalTime()
		{
			return new DateTimeOffset(this.UtcDateTime);
		}

		/// <summary>Tries to converts a specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <returns>true if the <paramref name="input" /> parameter is successfully converted; otherwise, false.</returns>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="result">When the method returns, contains the <see cref="T:System.DateTimeOffset" /> equivalent to the date and time of <paramref name="input" />, if the conversion succeeded, or <see cref="F:System.DateTimeOffset.MinValue" />, if the conversion failed. The conversion fails if the <paramref name="input" /> parameter is null or does not contain a valid string representation of a date and time. This parameter is passed uninitialized.</param>
		// Token: 0x06000DB0 RID: 3504 RVA: 0x0003A1E0 File Offset: 0x000383E0
		public static bool TryParse(string input, out DateTimeOffset result)
		{
			DateTime dateTime;
			TimeSpan timeSpan;
			bool flag = DateTimeParse.TryParse(input, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out dateTime, out timeSpan);
			result = new DateTimeOffset(dateTime.Ticks, timeSpan);
			return flag;
		}

		/// <summary>Tries to convert a specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <returns>true if the <paramref name="input" /> parameter is successfully converted; otherwise, false.</returns>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information about <paramref name="input" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="input" />. </param>
		/// <param name="result">When the method returns, contains the <see cref="T:System.DateTimeOffset" /> value equivalent to the date and time of <paramref name="input" />, if the conversion succeeded, or <see cref="F:System.DateTimeOffset.MinValue" />, if the conversion failed. The conversion fails if the <paramref name="input" /> parameter is null or does not contain a valid string representation of a date and time. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> includes an undefined <see cref="T:System.Globalization.DateTimeStyles" /> value.-or-<see cref="F:System.Globalization.DateTimeStyles.NoCurrentDateDefault" />  is not supported.-or-<paramref name="styles" /> includes mutually exclusive <see cref="T:System.Globalization.DateTimeStyles" /> values.</exception>
		// Token: 0x06000DB1 RID: 3505 RVA: 0x0003A210 File Offset: 0x00038410
		public static bool TryParse(string input, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			DateTime dateTime;
			TimeSpan timeSpan;
			bool flag = DateTimeParse.TryParse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out timeSpan);
			result = new DateTimeOffset(dateTime.Ticks, timeSpan);
			return flag;
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly.</summary>
		/// <returns>true if the <paramref name="input" /> parameter is successfully converted; otherwise, false.</returns>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="format">A format specifier that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information about <paramref name="input" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of input. A typical value to specify is None.</param>
		/// <param name="result">When the method returns, contains the <see cref="T:System.DateTimeOffset" /> equivalent to the date and time of <paramref name="input" />, if the conversion succeeded, or <see cref="F:System.DateTimeOffset.MinValue" />, if the conversion failed. The conversion fails if the <paramref name="input" /> parameter is null, or does not contain a valid string representation of a date and time in the expected format defined by <paramref name="format" /> and <paramref name="provider" />. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> includes an undefined <see cref="T:System.Globalization.DateTimeStyles" /> value.-or-<see cref="F:System.Globalization.DateTimeStyles.NoCurrentDateDefault" />  is not supported.-or-<paramref name="styles" /> includes mutually exclusive <see cref="T:System.Globalization.DateTimeStyles" /> values.</exception>
		// Token: 0x06000DB2 RID: 3506 RVA: 0x0003A250 File Offset: 0x00038450
		public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			DateTime dateTime;
			TimeSpan timeSpan;
			bool flag = DateTimeParse.TryParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out timeSpan);
			result = new DateTimeOffset(dateTime.Ticks, timeSpan);
			return flag;
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified array of formats, culture-specific format information, and style. The format of the string representation must match one of the specified formats exactly.</summary>
		/// <returns>true if the <paramref name="input" /> parameter is successfully converted; otherwise, false.</returns>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="formats">An array that defines the expected formats of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information about <paramref name="input" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of input. A typical value to specify is None.</param>
		/// <param name="result">When the method returns, contains the <see cref="T:System.DateTimeOffset" /> equivalent to the date and time of <paramref name="input" />, if the conversion succeeded, or <see cref="F:System.DateTimeOffset.MinValue" />, if the conversion failed. The conversion fails if the <paramref name="input" /> does not contain a valid string representation of a date and time, or does not contain the date and time in the expected format defined by <paramref name="format" />, or if <paramref name="formats" /> is null. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> includes an undefined <see cref="T:System.Globalization.DateTimeStyles" /> value.-or-<see cref="F:System.Globalization.DateTimeStyles.NoCurrentDateDefault" />  is not supported.-or-<paramref name="styles" /> includes mutually exclusive <see cref="T:System.Globalization.DateTimeStyles" /> values.</exception>
		// Token: 0x06000DB3 RID: 3507 RVA: 0x0003A290 File Offset: 0x00038490
		public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			DateTime dateTime;
			TimeSpan timeSpan;
			bool flag = DateTimeParse.TryParseExactMultiple(input, formats, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out timeSpan);
			result = new DateTimeOffset(dateTime.Ticks, timeSpan);
			return flag;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0003A2D0 File Offset: 0x000384D0
		private static short ValidateOffset(TimeSpan offset)
		{
			long ticks = offset.Ticks;
			if (ticks % 600000000L != 0L)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset must be specified in whole minutes."), "offset");
			}
			if (ticks < -504000000000L || ticks > 504000000000L)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Offset must be within plus or minus 14 hours."));
			}
			return (short)(offset.Ticks / 600000000L);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0003A340 File Offset: 0x00038540
		private static DateTime ValidateDate(DateTime dateTime, TimeSpan offset)
		{
			long num = dateTime.Ticks - offset.Ticks;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("The UTC time represented when the offset is applied must be between year 0 and 10,000."));
			}
			return new DateTime(num, DateTimeKind.Unspecified);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0003A38C File Offset: 0x0003858C
		private static DateTimeStyles ValidateStyles(DateTimeStyles style, string parameterName)
		{
			if ((style & ~(DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind)) != DateTimeStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("An undefined DateTimeStyles value is being used."), parameterName);
			}
			if ((style & DateTimeStyles.AssumeLocal) != DateTimeStyles.None && (style & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("The DateTimeStyles values AssumeLocal and AssumeUniversal cannot be used together."), parameterName);
			}
			if ((style & DateTimeStyles.NoCurrentDateDefault) != DateTimeStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("The DateTimeStyles value 'NoCurrentDateDefault' is not allowed when parsing DateTimeOffset."), parameterName);
			}
			style &= ~DateTimeStyles.RoundtripKind;
			style &= ~DateTimeStyles.AssumeLocal;
			return style;
		}

		/// <summary>Defines an implicit conversion of a <see cref="T:System.DateTime" /> object to a <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The converted object.</returns>
		/// <param name="dateTime">The object to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The Coordinated Universal Time (UTC) date and time that results from applying the offset is earlier than <see cref="F:System.DateTimeOffset.MinValue" />.-or-The UTC date and time that results from applying the offset is later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000DB7 RID: 3511 RVA: 0x0003A3F6 File Offset: 0x000385F6
		public static implicit operator DateTimeOffset(DateTime dateTime)
		{
			return new DateTimeOffset(dateTime);
		}

		/// <summary>Adds a specified time interval to a <see cref="T:System.DateTimeOffset" /> object that has a specified date and time, and yields a <see cref="T:System.DateTimeOffset" /> object that has new a date and time.</summary>
		/// <returns>An object whose value is the sum of the values of <paramref name="dateTimeTz" /> and <paramref name="timeSpan" />.</returns>
		/// <param name="dateTimeOffset">The object to add the time interval to.</param>
		/// <param name="timeSpan">The time interval to add.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.-or- The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000DB8 RID: 3512 RVA: 0x0003A3FE File Offset: 0x000385FE
		public static DateTimeOffset operator +(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
		{
			return new DateTimeOffset(dateTimeOffset.ClockDateTime + timeSpan, dateTimeOffset.Offset);
		}

		/// <summary>Subtracts a specified time interval from a specified date and time, and yields a new date and time.</summary>
		/// <returns>An object that is equal to the value of <paramref name="dateTimeOffset" /> minus <paramref name="timeSpan" />.</returns>
		/// <param name="dateTimeOffset">The date and time object to subtract from.</param>
		/// <param name="timeSpan">The time interval to subtract.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" /> or greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000DB9 RID: 3513 RVA: 0x0003A419 File Offset: 0x00038619
		public static DateTimeOffset operator -(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
		{
			return new DateTimeOffset(dateTimeOffset.ClockDateTime - timeSpan, dateTimeOffset.Offset);
		}

		/// <summary>Subtracts one <see cref="T:System.DateTimeOffset" /> object from another and yields a time interval.</summary>
		/// <returns>An object that represents the difference between <paramref name="left" /> and <paramref name="right" />.</returns>
		/// <param name="left">The minuend.   </param>
		/// <param name="right">The subtrahend.</param>
		// Token: 0x06000DBA RID: 3514 RVA: 0x0003A434 File Offset: 0x00038634
		public static TimeSpan operator -(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime - right.UtcDateTime;
		}

		/// <summary>Determines whether two specified <see cref="T:System.DateTimeOffset" /> objects represent the same point in time.</summary>
		/// <returns>true if both <see cref="T:System.DateTimeOffset" /> objects have the same <see cref="P:System.DateTimeOffset.UtcDateTime" /> value; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06000DBB RID: 3515 RVA: 0x0003A449 File Offset: 0x00038649
		public static bool operator ==(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime == right.UtcDateTime;
		}

		/// <summary>Determines whether two specified <see cref="T:System.DateTimeOffset" /> objects refer to different points in time.</summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> do not have the same <see cref="P:System.DateTimeOffset.UtcDateTime" /> value; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06000DBC RID: 3516 RVA: 0x0003A45E File Offset: 0x0003865E
		public static bool operator !=(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime != right.UtcDateTime;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTimeOffset" /> object is less than a second specified <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>true if the <see cref="P:System.DateTimeOffset.UtcDateTime" /> value of <paramref name="left" /> is earlier than the <see cref="P:System.DateTimeOffset.UtcDateTime" /> value of <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06000DBD RID: 3517 RVA: 0x0003A473 File Offset: 0x00038673
		public static bool operator <(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime < right.UtcDateTime;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTimeOffset" /> object is less than a second specified <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>true if the <see cref="P:System.DateTimeOffset.UtcDateTime" /> value of <paramref name="left" /> is earlier than the <see cref="P:System.DateTimeOffset.UtcDateTime" /> value of <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06000DBE RID: 3518 RVA: 0x0003A488 File Offset: 0x00038688
		public static bool operator <=(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime <= right.UtcDateTime;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTimeOffset" /> object is greater than (or later than) a second specified <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>true if the <see cref="P:System.DateTimeOffset.UtcDateTime" /> value of <paramref name="left" /> is later than the <see cref="P:System.DateTimeOffset.UtcDateTime" /> value of <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06000DBF RID: 3519 RVA: 0x0003A49D File Offset: 0x0003869D
		public static bool operator >(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime > right.UtcDateTime;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTimeOffset" /> object is greater than or equal to a second specified <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>true if the <see cref="P:System.DateTimeOffset.UtcDateTime" /> value of <paramref name="left" /> is the same as or later than the <see cref="P:System.DateTimeOffset.UtcDateTime" /> value of <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06000DC0 RID: 3520 RVA: 0x0003A4B2 File Offset: 0x000386B2
		public static bool operator >=(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime >= right.UtcDateTime;
		}

		// Token: 0x040008BC RID: 2236
		internal const long MaxOffset = 504000000000L;

		// Token: 0x040008BD RID: 2237
		internal const long MinOffset = -504000000000L;

		// Token: 0x040008BE RID: 2238
		private const long UnixEpochTicks = 621355968000000000L;

		// Token: 0x040008BF RID: 2239
		private const long UnixEpochSeconds = 62135596800L;

		// Token: 0x040008C0 RID: 2240
		private const long UnixEpochMilliseconds = 62135596800000L;

		/// <summary>Represents the earliest possible <see cref="T:System.DateTimeOffset" /> value. This field is read-only.</summary>
		// Token: 0x040008C1 RID: 2241
		public static readonly DateTimeOffset MinValue = new DateTimeOffset(0L, TimeSpan.Zero);

		/// <summary>Represents the greatest possible value of <see cref="T:System.DateTimeOffset" />. This field is read-only.</summary>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="F:System.DateTime.MaxValue" /> is outside the range of the current or specified culture's default calendar.</exception>
		// Token: 0x040008C2 RID: 2242
		public static readonly DateTimeOffset MaxValue = new DateTimeOffset(3155378975999999999L, TimeSpan.Zero);

		// Token: 0x040008C3 RID: 2243
		private DateTime m_dateTime;

		// Token: 0x040008C4 RID: 2244
		private short m_offsetMinutes;
	}
}
