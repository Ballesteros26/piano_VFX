using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents an instant in time, typically expressed as a date and time of day. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000147 RID: 327
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct DateTime : IComparable, IFormattable, IConvertible, ISerializable, IComparable<DateTime>, IEquatable<DateTime>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to a specified number of ticks.</summary>
		/// <param name="ticks">A date and time expressed in the number of 100-nanosecond intervals that have elapsed since January 1, 0001 at 00:00:00.000 in the Gregorian calendar. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ticks" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		// Token: 0x06000CEF RID: 3311 RVA: 0x00037F08 File Offset: 0x00036108
		public DateTime(long ticks)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", Environment.GetResourceString("Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks."));
			}
			this.dateData = (ulong)ticks;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00037F37 File Offset: 0x00036137
		private DateTime(ulong dateData)
		{
			this.dateData = dateData;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to a specified number of ticks and to Coordinated Universal Time (UTC) or local time.</summary>
		/// <param name="ticks">A date and time expressed in the number of 100-nanosecond intervals that have elapsed since January 1, 0001 at 00:00:00.000 in the Gregorian calendar. </param>
		/// <param name="kind">One of the enumeration values that indicates whether <paramref name="ticks" /> specifies a local time, Coordinated Universal Time (UTC), or neither.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ticks" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="kind" /> is not one of the <see cref="T:System.DateTimeKind" /> values.</exception>
		// Token: 0x06000CF1 RID: 3313 RVA: 0x00037F40 File Offset: 0x00036140
		public DateTime(long ticks, DateTimeKind kind)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", Environment.GetResourceString("Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks."));
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid DateTimeKind value."), "kind");
			}
			this.dateData = (ulong)(ticks | ((long)kind << 62));
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00037FA0 File Offset: 0x000361A0
		internal DateTime(long ticks, DateTimeKind kind, bool isAmbiguousDst)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", Environment.GetResourceString("Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks."));
			}
			this.dateData = (ulong)(ticks | (isAmbiguousDst ? (-4611686018427387904L) : long.MinValue));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, and day.</summary>
		/// <param name="year">The year (1 through 9999). </param>
		/// <param name="month">The month (1 through 12). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.-or- <paramref name="month" /> is less than 1 or greater than 12.-or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />. </exception>
		// Token: 0x06000CF3 RID: 3315 RVA: 0x00037FF2 File Offset: 0x000361F2
		public DateTime(int year, int month, int day)
		{
			this.dateData = (ulong)DateTime.DateToTicks(year, month, day);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, and day for the specified calendar.</summary>
		/// <param name="year">The year (1 through the number of years in <paramref name="calendar" />). </param>
		/// <param name="month">The month (1 through the number of months in <paramref name="calendar" />). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is not in the range supported by <paramref name="calendar" />.-or- <paramref name="month" /> is less than 1 or greater than the number of months in <paramref name="calendar" />.-or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />. </exception>
		// Token: 0x06000CF4 RID: 3316 RVA: 0x00038002 File Offset: 0x00036202
		public DateTime(int year, int month, int day, Calendar calendar)
		{
			this = new DateTime(year, month, day, 0, 0, 0, calendar);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, and second.</summary>
		/// <param name="year">The year (1 through 9999). </param>
		/// <param name="month">The month (1 through 12). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <param name="hour">The hours (0 through 23). </param>
		/// <param name="minute">The minutes (0 through 59). </param>
		/// <param name="second">The seconds (0 through 59). </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999. -or- <paramref name="month" /> is less than 1 or greater than 12. -or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.-or- <paramref name="hour" /> is less than 0 or greater than 23. -or- <paramref name="minute" /> is less than 0 or greater than 59. -or- <paramref name="second" /> is less than 0 or greater than 59. </exception>
		// Token: 0x06000CF5 RID: 3317 RVA: 0x00038012 File Offset: 0x00036212
		public DateTime(int year, int month, int day, int hour, int minute, int second)
		{
			this.dateData = (ulong)(DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, and Coordinated Universal Time (UTC) or local time.</summary>
		/// <param name="year">The year (1 through 9999). </param>
		/// <param name="month">The month (1 through 12). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <param name="hour">The hours (0 through 23). </param>
		/// <param name="minute">The minutes (0 through 59). </param>
		/// <param name="second">The seconds (0 through 59). </param>
		/// <param name="kind">One of the enumeration values that indicates whether <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" /> and <paramref name="second" /> specify a local time, Coordinated Universal Time (UTC), or neither.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999. -or- <paramref name="month" /> is less than 1 or greater than 12. -or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.-or- <paramref name="hour" /> is less than 0 or greater than 23. -or- <paramref name="minute" /> is less than 0 or greater than 59. -or- <paramref name="second" /> is less than 0 or greater than 59. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="kind" /> is not one of the <see cref="T:System.DateTimeKind" /> values.</exception>
		// Token: 0x06000CF6 RID: 3318 RVA: 0x00038030 File Offset: 0x00036230
		public DateTime(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
		{
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid DateTimeKind value."), "kind");
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			this.dateData = (ulong)(num | ((long)kind << 62));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, and second for the specified calendar.</summary>
		/// <param name="year">The year (1 through the number of years in <paramref name="calendar" />). </param>
		/// <param name="month">The month (1 through the number of months in <paramref name="calendar" />). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <param name="hour">The hours (0 through 23). </param>
		/// <param name="minute">The minutes (0 through 59). </param>
		/// <param name="second">The seconds (0 through 59). </param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is not in the range supported by <paramref name="calendar" />.-or- <paramref name="month" /> is less than 1 or greater than the number of months in <paramref name="calendar" />.-or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.-or- <paramref name="hour" /> is less than 0 or greater than 23 -or- <paramref name="minute" /> is less than 0 or greater than 59. -or- <paramref name="second" /> is less than 0 or greater than 59. </exception>
		// Token: 0x06000CF7 RID: 3319 RVA: 0x00038080 File Offset: 0x00036280
		public DateTime(int year, int month, int day, int hour, int minute, int second, Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			this.dateData = (ulong)calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, and millisecond.</summary>
		/// <param name="year">The year (1 through 9999). </param>
		/// <param name="month">The month (1 through 12). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <param name="hour">The hours (0 through 23). </param>
		/// <param name="minute">The minutes (0 through 59). </param>
		/// <param name="second">The seconds (0 through 59). </param>
		/// <param name="millisecond">The milliseconds (0 through 999). </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.-or- <paramref name="month" /> is less than 1 or greater than 12.-or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.-or- <paramref name="hour" /> is less than 0 or greater than 23.-or- <paramref name="minute" /> is less than 0 or greater than 59.-or- <paramref name="second" /> is less than 0 or greater than 59.-or- <paramref name="millisecond" /> is less than 0 or greater than 999. </exception>
		// Token: 0x06000CF8 RID: 3320 RVA: 0x000380BC File Offset: 0x000362BC
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 0, 999 }));
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("Combination of arguments to the DateTime constructor is out of the legal range."));
			}
			this.dateData = (ulong)num;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, millisecond, and Coordinated Universal Time (UTC) or local time.</summary>
		/// <param name="year">The year (1 through 9999). </param>
		/// <param name="month">The month (1 through 12). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <param name="hour">The hours (0 through 23). </param>
		/// <param name="minute">The minutes (0 through 59). </param>
		/// <param name="second">The seconds (0 through 59). </param>
		/// <param name="millisecond">The milliseconds (0 through 999). </param>
		/// <param name="kind">One of the enumeration values that indicates whether <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" />, and <paramref name="millisecond" /> specify a local time, Coordinated Universal Time (UTC), or neither.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.-or- <paramref name="month" /> is less than 1 or greater than 12.-or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.-or- <paramref name="hour" /> is less than 0 or greater than 23.-or- <paramref name="minute" /> is less than 0 or greater than 59.-or- <paramref name="second" /> is less than 0 or greater than 59.-or- <paramref name="millisecond" /> is less than 0 or greater than 999. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="kind" /> is not one of the <see cref="T:System.DateTimeKind" /> values.</exception>
		// Token: 0x06000CF9 RID: 3321 RVA: 0x00038154 File Offset: 0x00036354
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, DateTimeKind kind)
		{
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 0, 999 }));
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid DateTimeKind value."), "kind");
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("Combination of arguments to the DateTime constructor is out of the legal range."));
			}
			this.dateData = (ulong)(num | ((long)kind << 62));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, and millisecond for the specified calendar.</summary>
		/// <param name="year">The year (1 through the number of years in <paramref name="calendar" />). </param>
		/// <param name="month">The month (1 through the number of months in <paramref name="calendar" />). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <param name="hour">The hours (0 through 23). </param>
		/// <param name="minute">The minutes (0 through 59). </param>
		/// <param name="second">The seconds (0 through 59). </param>
		/// <param name="millisecond">The milliseconds (0 through 999). </param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is not in the range supported by <paramref name="calendar" />.-or- <paramref name="month" /> is less than 1 or greater than the number of months in <paramref name="calendar" />.-or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.-or- <paramref name="hour" /> is less than 0 or greater than 23.-or- <paramref name="minute" /> is less than 0 or greater than 59.-or- <paramref name="second" /> is less than 0 or greater than 59.-or- <paramref name="millisecond" /> is less than 0 or greater than 999. </exception>
		// Token: 0x06000CFA RID: 3322 RVA: 0x00038210 File Offset: 0x00036410
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 0, 999 }));
			}
			long num = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("Combination of arguments to the DateTime constructor is out of the legal range."));
			}
			this.dateData = (ulong)num;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, millisecond, and Coordinated Universal Time (UTC) or local time for the specified calendar.</summary>
		/// <param name="year">The year (1 through the number of years in <paramref name="calendar" />). </param>
		/// <param name="month">The month (1 through the number of months in <paramref name="calendar" />). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <param name="hour">The hours (0 through 23). </param>
		/// <param name="minute">The minutes (0 through 59). </param>
		/// <param name="second">The seconds (0 through 59). </param>
		/// <param name="millisecond">The milliseconds (0 through 999). </param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />.</param>
		/// <param name="kind">One of the enumeration values that indicates whether <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" />, and <paramref name="millisecond" /> specify a local time, Coordinated Universal Time (UTC), or neither.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is not in the range supported by <paramref name="calendar" />.-or- <paramref name="month" /> is less than 1 or greater than the number of months in <paramref name="calendar" />.-or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.-or- <paramref name="hour" /> is less than 0 or greater than 23.-or- <paramref name="minute" /> is less than 0 or greater than 59.-or- <paramref name="second" /> is less than 0 or greater than 59.-or- <paramref name="millisecond" /> is less than 0 or greater than 999. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="kind" /> is not one of the <see cref="T:System.DateTimeKind" /> values.</exception>
		// Token: 0x06000CFB RID: 3323 RVA: 0x000382BC File Offset: 0x000364BC
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, DateTimeKind kind)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 0, 999 }));
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid DateTimeKind value."), "kind");
			}
			long num = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("Combination of arguments to the DateTime constructor is out of the legal range."));
			}
			this.dateData = (ulong)(num | ((long)kind << 62));
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0003838C File Offset: 0x0003658C
		private DateTime(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			bool flag = false;
			bool flag2 = false;
			long num = 0L;
			ulong num2 = 0UL;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "ticks"))
				{
					if (name == "dateData")
					{
						num2 = Convert.ToUInt64(enumerator.Value, CultureInfo.InvariantCulture);
						flag2 = true;
					}
				}
				else
				{
					num = Convert.ToInt64(enumerator.Value, CultureInfo.InvariantCulture);
					flag = true;
				}
			}
			if (flag2)
			{
				this.dateData = num2;
			}
			else
			{
				if (!flag)
				{
					throw new SerializationException(Environment.GetResourceString("Invalid serialized DateTime data. Unable to find 'ticks' or 'dateData'."));
				}
				this.dateData = (ulong)num;
			}
			long internalTicks = this.InternalTicks;
			if (internalTicks < 0L || internalTicks > 3155378975999999999L)
			{
				throw new SerializationException(Environment.GetResourceString("Invalid serialized DateTime data. Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks."));
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00038468 File Offset: 0x00036668
		internal long InternalTicks
		{
			get
			{
				return (long)(this.dateData & 4611686018427387903UL);
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x0003847A File Offset: 0x0003667A
		private ulong InternalKind
		{
			get
			{
				return this.dateData & 13835058055282163712UL;
			}
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the value of the specified <see cref="T:System.TimeSpan" /> to the value of this instance.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the time interval represented by <paramref name="value" />.</returns>
		/// <param name="value">A positive or negative time interval. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000CFF RID: 3327 RVA: 0x0003848C File Offset: 0x0003668C
		public DateTime Add(TimeSpan value)
		{
			return this.AddTicks(value._ticks);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0003849C File Offset: 0x0003669C
		private DateTime Add(double value, int scale)
		{
			long num;
			checked
			{
				try
				{
					num = (long)(unchecked(value * (double)scale + ((value >= 0.0) ? 0.5 : (-0.5))));
				}
				catch (OverflowException)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Value to add was out of range."));
				}
				if (num <= -315537897600000L || num >= 315537897600000L)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Value to add was out of range."));
				}
			}
			return this.AddTicks(num * 10000L);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of days to the value of this instance.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of days represented by <paramref name="value" />.</returns>
		/// <param name="value">A number of whole and fractional days. The <paramref name="value" /> parameter can be negative or positive. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D01 RID: 3329 RVA: 0x00038534 File Offset: 0x00036734
		public DateTime AddDays(double value)
		{
			return this.Add(value, 86400000);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of hours to the value of this instance.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of hours represented by <paramref name="value" />.</returns>
		/// <param name="value">A number of whole and fractional hours. The <paramref name="value" /> parameter can be negative or positive. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D02 RID: 3330 RVA: 0x00038542 File Offset: 0x00036742
		public DateTime AddHours(double value)
		{
			return this.Add(value, 3600000);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of milliseconds to the value of this instance.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of milliseconds represented by <paramref name="value" />.</returns>
		/// <param name="value">A number of whole and fractional milliseconds. The <paramref name="value" /> parameter can be negative or positive. Note that this value is rounded to the nearest integer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D03 RID: 3331 RVA: 0x00038550 File Offset: 0x00036750
		public DateTime AddMilliseconds(double value)
		{
			return this.Add(value, 1);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of minutes to the value of this instance.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of minutes represented by <paramref name="value" />.</returns>
		/// <param name="value">A number of whole and fractional minutes. The <paramref name="value" /> parameter can be negative or positive. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D04 RID: 3332 RVA: 0x0003855A File Offset: 0x0003675A
		public DateTime AddMinutes(double value)
		{
			return this.Add(value, 60000);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of months to the value of this instance.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and <paramref name="months" />.</returns>
		/// <param name="months">A number of months. The <paramref name="months" /> parameter can be negative or positive. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.-or- <paramref name="months" /> is less than -120,000 or greater than 120,000. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D05 RID: 3333 RVA: 0x00038568 File Offset: 0x00036768
		public DateTime AddMonths(int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", Environment.GetResourceString("Months value must be between +/-120000."));
			}
			int num = this.GetDatePart(0);
			int num2 = this.GetDatePart(2);
			int num3 = this.GetDatePart(3);
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
			if (num < 1 || num > 9999)
			{
				throw new ArgumentOutOfRangeException("months", Environment.GetResourceString("The added or subtracted value results in an un-representable DateTime."));
			}
			int num5 = DateTime.DaysInMonth(num, num2);
			if (num3 > num5)
			{
				num3 = num5;
			}
			return new DateTime((ulong)((DateTime.DateToTicks(num, num2, num3) + this.InternalTicks % 864000000000L) | (long)this.InternalKind));
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of seconds to the value of this instance.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of seconds represented by <paramref name="value" />.</returns>
		/// <param name="value">A number of whole and fractional seconds. The <paramref name="value" /> parameter can be negative or positive. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D06 RID: 3334 RVA: 0x00038637 File Offset: 0x00036837
		public DateTime AddSeconds(double value)
		{
			return this.Add(value, 1000);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of ticks to the value of this instance.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the time represented by <paramref name="value" />.</returns>
		/// <param name="value">A number of 100-nanosecond ticks. The <paramref name="value" /> parameter can be positive or negative. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D07 RID: 3335 RVA: 0x00038648 File Offset: 0x00036848
		public DateTime AddTicks(long value)
		{
			long internalTicks = this.InternalTicks;
			if (value > 3155378975999999999L - internalTicks || value < 0L - internalTicks)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("The added or subtracted value results in an un-representable DateTime."));
			}
			return new DateTime((ulong)((internalTicks + value) | (long)this.InternalKind));
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of years to the value of this instance.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of years represented by <paramref name="value" />.</returns>
		/// <param name="value">A number of years. The <paramref name="value" /> parameter can be negative or positive. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> or the resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D08 RID: 3336 RVA: 0x00038695 File Offset: 0x00036895
		public DateTime AddYears(int value)
		{
			if (value < -10000 || value > 10000)
			{
				throw new ArgumentOutOfRangeException("years", Environment.GetResourceString("Years value must be between +/-10000."));
			}
			return this.AddMonths(value * 12);
		}

		/// <summary>Compares two instances of <see cref="T:System.DateTime" /> and returns an integer that indicates whether the first instance is earlier than, the same as, or later than the second instance.</summary>
		/// <returns>A signed number indicating the relative values of <paramref name="t1" /> and <paramref name="t2" />.Value Type Condition Less than zero <paramref name="t1" /> is earlier than <paramref name="t2" />. Zero <paramref name="t1" /> is the same as <paramref name="t2" />. Greater than zero <paramref name="t1" /> is later than <paramref name="t2" />. </returns>
		/// <param name="t1">The first object to compare. </param>
		/// <param name="t2">The second object to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D09 RID: 3337 RVA: 0x000386C8 File Offset: 0x000368C8
		public static int Compare(DateTime t1, DateTime t2)
		{
			long internalTicks = t1.InternalTicks;
			long internalTicks2 = t2.InternalTicks;
			if (internalTicks > internalTicks2)
			{
				return 1;
			}
			if (internalTicks < internalTicks2)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares the value of this instance to a specified object that contains a specified <see cref="T:System.DateTime" /> value, and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Value Description Less than zero This instance is earlier than <paramref name="value" />. Zero This instance is the same as <paramref name="value" />. Greater than zero This instance is later than <paramref name="value" />, or <paramref name="value" /> is null. </returns>
		/// <param name="value">A boxed object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.DateTime" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D0A RID: 3338 RVA: 0x000386F4 File Offset: 0x000368F4
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is DateTime))
			{
				throw new ArgumentException(Environment.GetResourceString("Object must be of type DateTime."));
			}
			long internalTicks = ((DateTime)value).InternalTicks;
			long internalTicks2 = this.InternalTicks;
			if (internalTicks2 > internalTicks)
			{
				return 1;
			}
			if (internalTicks2 < internalTicks)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares the value of this instance to a specified <see cref="T:System.DateTime" /> value and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <returns>A signed number indicating the relative values of this instance and the <paramref name="value" /> parameter.Value Description Less than zero This instance is earlier than <paramref name="value" />. Zero This instance is the same as <paramref name="value" />. Greater than zero This instance is later than <paramref name="value" />. </returns>
		/// <param name="value">The object to compare to the current instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D0B RID: 3339 RVA: 0x00038744 File Offset: 0x00036944
		public int CompareTo(DateTime value)
		{
			long internalTicks = value.InternalTicks;
			long internalTicks2 = this.InternalTicks;
			if (internalTicks2 > internalTicks)
			{
				return 1;
			}
			if (internalTicks2 < internalTicks)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00038770 File Offset: 0x00036970
		private static long DateToTicks(int year, int month, int day)
		{
			if (year >= 1 && year <= 9999 && month >= 1 && month <= 12)
			{
				int[] array = (DateTime.IsLeapYear(year) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365);
				if (day >= 1 && day <= array[month] - array[month - 1])
				{
					int num = year - 1;
					return (long)(num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1) * 864000000000L;
				}
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Year, Month, and Day parameters describe an un-representable DateTime."));
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000387F9 File Offset: 0x000369F9
		private static long TimeToTicks(int hour, int minute, int second)
		{
			if (hour >= 0 && hour < 24 && minute >= 0 && minute < 60 && second >= 0 && second < 60)
			{
				return TimeSpan.TimeToTicks(hour, minute, second);
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Hour, Minute, and Second parameters describe an un-representable DateTime."));
		}

		/// <summary>Returns the number of days in the specified month and year.</summary>
		/// <returns>The number of days in <paramref name="month" /> for the specified <paramref name="year" />.For example, if <paramref name="month" /> equals 2 for February, the return value is 28 or 29 depending upon whether <paramref name="year" /> is a leap year.</returns>
		/// <param name="year">The year. </param>
		/// <param name="month">The month (a number ranging from 1 to 12). </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="month" /> is less than 1 or greater than 12.-or-<paramref name="year" /> is less than 1 or greater than 9999.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D0E RID: 3342 RVA: 0x00038830 File Offset: 0x00036A30
		public static int DaysInMonth(int year, int month)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Month must be between one and twelve."));
			}
			int[] array = (DateTime.IsLeapYear(year) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365);
			return array[month] - array[month - 1];
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0003887C File Offset: 0x00036A7C
		internal static long DoubleDateToTicks(double value)
		{
			if (value >= 2958466.0 || value <= -657435.0)
			{
				throw new ArgumentException(Environment.GetResourceString("Not a legal OleAut date."));
			}
			long num = (long)(value * 86400000.0 + ((value >= 0.0) ? 0.5 : (-0.5)));
			if (num < 0L)
			{
				num -= num % 86400000L * 2L;
			}
			num += 59926435200000L;
			if (num < 0L || num >= 315537897600000L)
			{
				throw new ArgumentException(Environment.GetResourceString("OleAut date did not convert to a DateTime correctly."));
			}
			return num * 10000L;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="value" /> is an instance of <see cref="T:System.DateTime" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="value">The object to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D10 RID: 3344 RVA: 0x00038928 File Offset: 0x00036B28
		public override bool Equals(object value)
		{
			return value is DateTime && this.InternalTicks == ((DateTime)value).InternalTicks;
		}

		/// <summary>Returns a value indicating whether the value of this instance is equal to the value of the specified <see cref="T:System.DateTime" /> instance.</summary>
		/// <returns>true if the <paramref name="value" /> parameter equals the value of this instance; otherwise, false.</returns>
		/// <param name="value">The object to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D11 RID: 3345 RVA: 0x00038955 File Offset: 0x00036B55
		public bool Equals(DateTime value)
		{
			return this.InternalTicks == value.InternalTicks;
		}

		/// <summary>Returns a value indicating whether two <see cref="T:System.DateTime" /> instances  have the same date and time value.</summary>
		/// <returns>true if the two values are equal; otherwise, false.</returns>
		/// <param name="t1">The first object to compare. </param>
		/// <param name="t2">The second object to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D12 RID: 3346 RVA: 0x00038966 File Offset: 0x00036B66
		public static bool Equals(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks == t2.InternalTicks;
		}

		/// <summary>Deserializes a 64-bit binary value and recreates an original serialized <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>An object that is equivalent to the <see cref="T:System.DateTime" /> object that was serialized by the <see cref="M:System.DateTime.ToBinary" /> method.</returns>
		/// <param name="dateData">A 64-bit signed integer that encodes the <see cref="P:System.DateTime.Kind" /> property in a 2-bit field and the <see cref="P:System.DateTime.Ticks" /> property in a 62-bit field. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateData" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D13 RID: 3347 RVA: 0x00038978 File Offset: 0x00036B78
		public static DateTime FromBinary(long dateData)
		{
			if ((dateData & -9223372036854775808L) == 0L)
			{
				return DateTime.FromBinaryRaw(dateData);
			}
			long num = dateData & 4611686018427387903L;
			if (num > 4611685154427387904L)
			{
				num -= 4611686018427387904L;
			}
			bool flag = false;
			long num2;
			if (num < 0L)
			{
				num2 = TimeZoneInfo.GetLocalUtcOffset(DateTime.MinValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
			}
			else if (num > 3155378975999999999L)
			{
				num2 = TimeZoneInfo.GetLocalUtcOffset(DateTime.MaxValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
			}
			else
			{
				DateTime dateTime = new DateTime(num, DateTimeKind.Utc);
				bool flag2 = false;
				num2 = TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, TimeZoneInfo.Local, out flag2, out flag).Ticks;
			}
			num += num2;
			if (num < 0L)
			{
				num += 864000000000L;
			}
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("The binary data must result in a DateTime with ticks between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks."), "dateData");
			}
			return new DateTime(num, DateTimeKind.Local, flag);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00038A64 File Offset: 0x00036C64
		internal static DateTime FromBinaryRaw(long dateData)
		{
			long num = dateData & 4611686018427387903L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("The binary data must result in a DateTime with ticks between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks."), "dateData");
			}
			return new DateTime((ulong)dateData);
		}

		/// <summary>Converts the specified Windows file time to an equivalent local time.</summary>
		/// <returns>An object that represents the local time equivalent of the date and time represented by the <paramref name="fileTime" /> parameter.</returns>
		/// <param name="fileTime">A Windows file time expressed in ticks. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="fileTime" /> is less than 0 or represents a time greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D15 RID: 3349 RVA: 0x00038AAC File Offset: 0x00036CAC
		public static DateTime FromFileTime(long fileTime)
		{
			return DateTime.FromFileTimeUtc(fileTime).ToLocalTime();
		}

		/// <summary>Converts the specified Windows file time to an equivalent UTC time.</summary>
		/// <returns>An object that represents the UTC time equivalent of the date and time represented by the <paramref name="fileTime" /> parameter.</returns>
		/// <param name="fileTime">A Windows file time expressed in ticks. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="fileTime" /> is less than 0 or represents a time greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D16 RID: 3350 RVA: 0x00038AC7 File Offset: 0x00036CC7
		public static DateTime FromFileTimeUtc(long fileTime)
		{
			if (fileTime < 0L || fileTime > 2650467743999999999L)
			{
				throw new ArgumentOutOfRangeException("fileTime", Environment.GetResourceString("Not a valid Win32 FileTime."));
			}
			return new DateTime(fileTime + 504911232000000000L, DateTimeKind.Utc);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> equivalent to the specified OLE Automation Date.</summary>
		/// <returns>An object that represents the same date and time as <paramref name="d" />.</returns>
		/// <param name="d">An OLE Automation Date value. </param>
		/// <exception cref="T:System.ArgumentException">The date is not a valid OLE Automation Date value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D17 RID: 3351 RVA: 0x00038B00 File Offset: 0x00036D00
		public static DateTime FromOADate(double d)
		{
			return new DateTime(DateTime.DoubleDateToTicks(d), DateTimeKind.Unspecified);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the current <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="info">The object to populate with data. </param>
		/// <param name="context">The destination for this serialization. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06000D18 RID: 3352 RVA: 0x00038B0E File Offset: 0x00036D0E
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ticks", this.InternalTicks);
			info.AddValue("dateData", this.dateData);
		}

		/// <summary>Indicates whether this instance of <see cref="T:System.DateTime" /> is within the daylight saving time range for the current time zone.</summary>
		/// <returns>true if the value of the <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Local" /> or <see cref="F:System.DateTimeKind.Unspecified" /> and the value of this instance of <see cref="T:System.DateTime" /> is within the daylight saving time range for the local time zone; false if <see cref="P:System.DateTime.Kind" /> is <see cref="F:System.DateTimeKind.Utc" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D19 RID: 3353 RVA: 0x00038B40 File Offset: 0x00036D40
		public bool IsDaylightSavingTime()
		{
			return this.Kind != DateTimeKind.Utc && TimeZoneInfo.Local.IsDaylightSavingTime(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		/// <summary>Creates a new <see cref="T:System.DateTime" /> object that has the same number of ticks as the specified <see cref="T:System.DateTime" />, but is designated as either local time, Coordinated Universal Time (UTC), or neither, as indicated by the specified <see cref="T:System.DateTimeKind" /> value.</summary>
		/// <returns>A new object that has the same number of ticks as the object represented by the <paramref name="value" /> parameter and the <see cref="T:System.DateTimeKind" /> value specified by the <paramref name="kind" /> parameter. </returns>
		/// <param name="value">A date and time. </param>
		/// <param name="kind">One of the enumeration values that indicates whether the new object represents local time, UTC, or neither.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D1A RID: 3354 RVA: 0x00038B5E File Offset: 0x00036D5E
		public static DateTime SpecifyKind(DateTime value, DateTimeKind kind)
		{
			return new DateTime(value.InternalTicks, kind);
		}

		/// <summary>Serializes the current <see cref="T:System.DateTime" /> object to a 64-bit binary value that subsequently can be used to recreate the <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>A 64-bit signed integer that encodes the <see cref="P:System.DateTime.Kind" /> and <see cref="P:System.DateTime.Ticks" /> properties. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D1B RID: 3355 RVA: 0x00038B70 File Offset: 0x00036D70
		public long ToBinary()
		{
			if (this.Kind == DateTimeKind.Local)
			{
				TimeSpan localUtcOffset = TimeZoneInfo.GetLocalUtcOffset(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				long num = this.Ticks - localUtcOffset.Ticks;
				if (num < 0L)
				{
					num = 4611686018427387904L + num;
				}
				return num | long.MinValue;
			}
			return (long)this.dateData;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00038BC5 File Offset: 0x00036DC5
		internal long ToBinaryRaw()
		{
			return (long)this.dateData;
		}

		/// <summary>Gets the date component of this instance.</summary>
		/// <returns>A new object with the same date as this instance, and the time value set to 12:00:00 midnight (00:00:00).</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x00038BCD File Offset: 0x00036DCD
		public DateTime Date
		{
			get
			{
				long internalTicks = this.InternalTicks;
				return new DateTime((ulong)((internalTicks - internalTicks % 864000000000L) | (long)this.InternalKind));
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00038BF0 File Offset: 0x00036DF0
		private int GetDatePart(int part)
		{
			int i = (int)(this.InternalTicks / 864000000000L);
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
			int[] array = ((num4 == 3 && (num3 != 24 || num2 == 3)) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365);
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

		/// <summary>Gets the day of the month represented by this instance.</summary>
		/// <returns>The day component, expressed as a value between 1 and 31.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x00038CD3 File Offset: 0x00036ED3
		public int Day
		{
			get
			{
				return this.GetDatePart(3);
			}
		}

		/// <summary>Gets the day of the week represented by this instance.</summary>
		/// <returns>An enumerated constant that indicates the day of the week of this <see cref="T:System.DateTime" /> value. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00038CDC File Offset: 0x00036EDC
		public DayOfWeek DayOfWeek
		{
			get
			{
				return (DayOfWeek)((this.InternalTicks / 864000000000L + 1L) % 7L);
			}
		}

		/// <summary>Gets the day of the year represented by this instance.</summary>
		/// <returns>The day of the year, expressed as a value between 1 and 366.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x00038CF5 File Offset: 0x00036EF5
		public int DayOfYear
		{
			get
			{
				return this.GetDatePart(1);
			}
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D22 RID: 3362 RVA: 0x00038D00 File Offset: 0x00036F00
		public override int GetHashCode()
		{
			long internalTicks = this.InternalTicks;
			return (int)internalTicks ^ (int)(internalTicks >> 32);
		}

		/// <summary>Gets the hour component of the date represented by this instance.</summary>
		/// <returns>The hour component, expressed as a value between 0 and 23.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x00038D1C File Offset: 0x00036F1C
		public int Hour
		{
			get
			{
				return (int)(this.InternalTicks / 36000000000L % 24L);
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00038D33 File Offset: 0x00036F33
		internal bool IsAmbiguousDaylightSavingTime()
		{
			return this.InternalKind == 13835058055282163712UL;
		}

		/// <summary>Gets a value that indicates whether the time represented by this instance is based on local time, Coordinated Universal Time (UTC), or neither.</summary>
		/// <returns>One of the enumeration values that indicates what the current time represents. The default is <see cref="F:System.DateTimeKind.Unspecified" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00038D48 File Offset: 0x00036F48
		public DateTimeKind Kind
		{
			get
			{
				ulong internalKind = this.InternalKind;
				if (internalKind == 0UL)
				{
					return DateTimeKind.Unspecified;
				}
				if (internalKind != 4611686018427387904UL)
				{
					return DateTimeKind.Local;
				}
				return DateTimeKind.Utc;
			}
		}

		/// <summary>Gets the milliseconds component of the date represented by this instance.</summary>
		/// <returns>The milliseconds component, expressed as a value between 0 and 999.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00038D72 File Offset: 0x00036F72
		public int Millisecond
		{
			get
			{
				return (int)(this.InternalTicks / 10000L % 1000L);
			}
		}

		/// <summary>Gets the minute component of the date represented by this instance.</summary>
		/// <returns>The minute component, expressed as a value between 0 and 59.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x00038D89 File Offset: 0x00036F89
		public int Minute
		{
			get
			{
				return (int)(this.InternalTicks / 600000000L % 60L);
			}
		}

		/// <summary>Gets the month component of the date represented by this instance.</summary>
		/// <returns>The month component, expressed as a value between 1 and 12.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00038D9D File Offset: 0x00036F9D
		public int Month
		{
			get
			{
				return this.GetDatePart(2);
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> object that is set to the current date and time on this computer, expressed as the local time.</summary>
		/// <returns>An object whose value is the current local date and time.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x00038DA8 File Offset: 0x00036FA8
		public static DateTime Now
		{
			get
			{
				DateTime utcNow = DateTime.UtcNow;
				bool flag = false;
				long ticks = TimeZoneInfo.GetDateTimeNowUtcOffsetFromUtc(utcNow, out flag).Ticks;
				long num = utcNow.Ticks + ticks;
				if (num > 3155378975999999999L)
				{
					return new DateTime(3155378975999999999L, DateTimeKind.Local);
				}
				if (num < 0L)
				{
					return new DateTime(0L, DateTimeKind.Local);
				}
				return new DateTime(num, DateTimeKind.Local, flag);
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> object that is set to the current date and time on this computer, expressed as the Coordinated Universal Time (UTC).</summary>
		/// <returns>An object whose value is the current UTC date and time.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x00038E0B File Offset: 0x0003700B
		public static DateTime UtcNow
		{
			[SecuritySafeCritical]
			get
			{
				return new DateTime((ulong)((DateTime.GetSystemTimeAsFileTime() + 504911232000000000L) | 4611686018427387904L));
			}
		}

		// Token: 0x06000D2B RID: 3371
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetSystemTimeAsFileTime();

		/// <summary>Gets the seconds component of the date represented by this instance.</summary>
		/// <returns>The seconds component, expressed as a value between 0 and 59.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00038E2B File Offset: 0x0003702B
		public int Second
		{
			get
			{
				return (int)(this.InternalTicks / 10000000L % 60L);
			}
		}

		/// <summary>Gets the number of ticks that represent the date and time of this instance.</summary>
		/// <returns>The number of ticks that represent the date and time of this instance. The value is between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x00038E3F File Offset: 0x0003703F
		public long Ticks
		{
			get
			{
				return this.InternalTicks;
			}
		}

		/// <summary>Gets the time of day for this instance.</summary>
		/// <returns>A time interval that represents the fraction of the day that has elapsed since midnight.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00038E47 File Offset: 0x00037047
		public TimeSpan TimeOfDay
		{
			get
			{
				return new TimeSpan(this.InternalTicks % 864000000000L);
			}
		}

		/// <summary>Gets the current date.</summary>
		/// <returns>An object that is set to today's date, with the time component set to 00:00:00.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x00038E60 File Offset: 0x00037060
		public static DateTime Today
		{
			get
			{
				return DateTime.Now.Date;
			}
		}

		/// <summary>Gets the year component of the date represented by this instance.</summary>
		/// <returns>The year, between 1 and 9999.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x00038E7A File Offset: 0x0003707A
		public int Year
		{
			get
			{
				return this.GetDatePart(0);
			}
		}

		/// <summary>Returns an indication whether the specified year is a leap year.</summary>
		/// <returns>true if <paramref name="year" /> is a leap year; otherwise, false.</returns>
		/// <param name="year">A 4-digit year. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D31 RID: 3377 RVA: 0x00038E83 File Offset: 0x00037083
		public static bool IsLeapYear(int year)
		{
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Year must be between 1 and 9999."));
			}
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		/// <summary>Converts the string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent.</summary>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a date and time to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not contain a valid string representation of a date and time. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D32 RID: 3378 RVA: 0x00038EBF File Offset: 0x000370BF
		public static DateTime Parse(string s)
		{
			return DateTimeParse.Parse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None);
		}

		/// <summary>Converts the string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent by using culture-specific format information.</summary>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" /> as specified by <paramref name="provider" />.</returns>
		/// <param name="s">A string that contains a date and time to convert. </param>
		/// <param name="provider">An object that supplies culture-specific format information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not contain a valid string representation of a date and time. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D33 RID: 3379 RVA: 0x00038ECD File Offset: 0x000370CD
		public static DateTime Parse(string s, IFormatProvider provider)
		{
			return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
		}

		/// <summary>Converts the string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent by using culture-specific format information and formatting style.</summary>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />, as specified by <paramref name="provider" /> and <paramref name="styles" />.</returns>
		/// <param name="s">A string that contains a date and time to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="styles">A bitwise combination of the enumeration values that indicates the style elements that can be present in <paramref name="s" /> for the parse operation to succeed, and that defines how to interpret the parsed date in relation to the current time zone or the current date. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not contain a valid string representation of a date and time. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values. For example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D34 RID: 3380 RVA: 0x00038EDC File Offset: 0x000370DC
		public static DateTime Parse(string s, IFormatProvider provider, DateTimeStyles styles)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), styles);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified format and culture-specific format information. The format of the string representation must match the specified format exactly.</summary>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />, as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="s">A string that contains a date and time to convert. </param>
		/// <param name="format">A format specifier that defines the required format of <paramref name="s" />. For more information, see the Remarks section. </param>
		/// <param name="provider">An object that supplies culture-specific format information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> or <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> or <paramref name="format" /> is an empty string. -or- <paramref name="s" /> does not contain a date and time that corresponds to the pattern specified in <paramref name="format" />. -or-The hour component and the AM/PM designator in <paramref name="s" /> do not agree.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D35 RID: 3381 RVA: 0x00038EF6 File Offset: 0x000370F6
		public static DateTime ParseExact(string s, string format, IFormatProvider provider)
		{
			return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly or an exception is thrown.</summary>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />, as specified by <paramref name="format" />, <paramref name="provider" />, and <paramref name="style" />.</returns>
		/// <param name="s">A string containing a date and time to convert. </param>
		/// <param name="format">A format specifier that defines the required format of <paramref name="s" />. For more information, see the Remarks section. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="style">A bitwise combination of the enumeration values that provides additional information about <paramref name="s" />, about style elements that may be present in <paramref name="s" />, or about the conversion from <paramref name="s" /> to a <see cref="T:System.DateTime" /> value. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> or <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> or <paramref name="format" /> is an empty string. -or- <paramref name="s" /> does not contain a date and time that corresponds to the pattern specified in <paramref name="format" />. -or-The hour component and the AM/PM designator in <paramref name="s" /> do not agree.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values. For example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D36 RID: 3382 RVA: 0x00038F06 File Offset: 0x00037106
		public static DateTime ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified array of formats, culture-specific format information, and style. The format of the string representation must match at least one of the specified formats exactly or an exception is thrown.</summary>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />, as specified by <paramref name="formats" />, <paramref name="provider" />, and <paramref name="style" />.</returns>
		/// <param name="s">A string that contains a date and time to convert. </param>
		/// <param name="formats">An array of allowable formats of <paramref name="s" />. For more information, see the Remarks section. </param>
		/// <param name="provider">An object that supplies culture-specific format information about <paramref name="s" />. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> or <paramref name="formats" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is an empty string. -or- an element of <paramref name="formats" /> is an empty string. -or- <paramref name="s" /> does not contain a date and time that corresponds to any element of <paramref name="formats" />. -or-The hour component and the AM/PM designator in <paramref name="s" /> do not agree.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values. For example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D37 RID: 3383 RVA: 0x00038F21 File Offset: 0x00037121
		public static DateTime ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.ParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style);
		}

		/// <summary>Subtracts the specified date and time from this instance.</summary>
		/// <returns>A time interval that is equal to the date and time represented by this instance minus the date and time represented by <paramref name="value" />.</returns>
		/// <param name="value">The date and time value to subtract. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The result is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D38 RID: 3384 RVA: 0x00038F3C File Offset: 0x0003713C
		public TimeSpan Subtract(DateTime value)
		{
			return new TimeSpan(this.InternalTicks - value.InternalTicks);
		}

		/// <summary>Subtracts the specified duration from this instance.</summary>
		/// <returns>An object that is equal to the date and time represented by this instance minus the time interval represented by <paramref name="value" />.</returns>
		/// <param name="value">The time interval to subtract. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The result is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D39 RID: 3385 RVA: 0x00038F54 File Offset: 0x00037154
		public DateTime Subtract(TimeSpan value)
		{
			long internalTicks = this.InternalTicks;
			long ticks = value._ticks;
			if (internalTicks < ticks || internalTicks - 3155378975999999999L > ticks)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("The added or subtracted value results in an un-representable DateTime."));
			}
			return new DateTime((ulong)((internalTicks - ticks) | (long)this.InternalKind));
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00038FA8 File Offset: 0x000371A8
		private static double TicksToOADate(long value)
		{
			if (value == 0L)
			{
				return 0.0;
			}
			if (value < 864000000000L)
			{
				value += 599264352000000000L;
			}
			if (value < 31241376000000000L)
			{
				throw new OverflowException(Environment.GetResourceString("Not a legal OleAut date."));
			}
			long num = (value - 599264352000000000L) / 10000L;
			if (num < 0L)
			{
				long num2 = num % 86400000L;
				if (num2 != 0L)
				{
					num -= (86400000L + num2) * 2L;
				}
			}
			return (double)num / 86400000.0;
		}

		/// <summary>Converts the value of this instance to the equivalent OLE Automation date.</summary>
		/// <returns>A double-precision floating-point number that contains an OLE Automation date equivalent to the value of this instance.</returns>
		/// <exception cref="T:System.OverflowException">The value of this instance cannot be represented as an OLE Automation Date. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D3B RID: 3387 RVA: 0x00039035 File Offset: 0x00037235
		public double ToOADate()
		{
			return DateTime.TicksToOADate(this.InternalTicks);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to a Windows file time.</summary>
		/// <returns>The value of the current <see cref="T:System.DateTime" /> object expressed as a Windows file time.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting file time would represent a date and time before 12:00 midnight January 1, 1601 C.E. UTC. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D3C RID: 3388 RVA: 0x00039044 File Offset: 0x00037244
		public long ToFileTime()
		{
			return this.ToUniversalTime().ToFileTimeUtc();
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to a Windows file time.</summary>
		/// <returns>The value of the current <see cref="T:System.DateTime" /> object expressed as a Windows file time.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting file time would represent a date and time before 12:00 midnight January 1, 1601 C.E. UTC. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D3D RID: 3389 RVA: 0x00039060 File Offset: 0x00037260
		public long ToFileTimeUtc()
		{
			long num = (((this.InternalKind & 9223372036854775808UL) != 0UL) ? this.ToUniversalTime().InternalTicks : this.InternalTicks) - 504911232000000000L;
			if (num < 0L)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Not a valid Win32 FileTime."));
			}
			return num;
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to local time.</summary>
		/// <returns>An object whose <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Local" />, and whose value is the local time equivalent to the value of the current <see cref="T:System.DateTime" /> object, or <see cref="F:System.DateTime.MaxValue" /> if the converted value is too large to be represented by a <see cref="T:System.DateTime" /> object, or <see cref="F:System.DateTime.MinValue" /> if the converted value is too small to be represented as a <see cref="T:System.DateTime" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D3E RID: 3390 RVA: 0x000390B5 File Offset: 0x000372B5
		public DateTime ToLocalTime()
		{
			return this.ToLocalTime(false);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x000390C0 File Offset: 0x000372C0
		internal DateTime ToLocalTime(bool throwOnOverflow)
		{
			if (this.Kind == DateTimeKind.Local)
			{
				return this;
			}
			bool flag = false;
			bool flag2 = false;
			long ticks = TimeZoneInfo.GetUtcOffsetFromUtc(this, TimeZoneInfo.Local, out flag, out flag2).Ticks;
			long num = this.Ticks + ticks;
			if (num > 3155378975999999999L)
			{
				if (throwOnOverflow)
				{
					throw new ArgumentException(Environment.GetResourceString("Specified argument was out of the range of valid values."));
				}
				return new DateTime(3155378975999999999L, DateTimeKind.Local);
			}
			else
			{
				if (num >= 0L)
				{
					return new DateTime(num, DateTimeKind.Local, flag2);
				}
				if (throwOnOverflow)
				{
					throw new ArgumentException(Environment.GetResourceString("Specified argument was out of the range of valid values."));
				}
				return new DateTime(0L, DateTimeKind.Local);
			}
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent long date string representation.</summary>
		/// <returns>A string that contains the long date string representation of the current <see cref="T:System.DateTime" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D40 RID: 3392 RVA: 0x00039160 File Offset: 0x00037360
		public string ToLongDateString()
		{
			return DateTimeFormat.Format(this, "D", DateTimeFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent long time string representation.</summary>
		/// <returns>A string that contains the long time string representation of the current <see cref="T:System.DateTime" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D41 RID: 3393 RVA: 0x00039177 File Offset: 0x00037377
		public string ToLongTimeString()
		{
			return DateTimeFormat.Format(this, "T", DateTimeFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent short date string representation.</summary>
		/// <returns>A string that contains the short date string representation of the current <see cref="T:System.DateTime" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D42 RID: 3394 RVA: 0x0003918E File Offset: 0x0003738E
		public string ToShortDateString()
		{
			return DateTimeFormat.Format(this, "d", DateTimeFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent short time string representation.</summary>
		/// <returns>A string that contains the short time string representation of the current <see cref="T:System.DateTime" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D43 RID: 3395 RVA: 0x000391A5 File Offset: 0x000373A5
		public string ToShortTimeString()
		{
			return DateTimeFormat.Format(this, "t", DateTimeFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent string representation.</summary>
		/// <returns>A string representation of the value of the current <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by the current culture. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D44 RID: 3396 RVA: 0x000391BC File Offset: 0x000373BC
		public override string ToString()
		{
			return DateTimeFormat.Format(this, null, DateTimeFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent string representation using the specified format.</summary>
		/// <returns>A string representation of value of the current <see cref="T:System.DateTime" /> object as specified by <paramref name="format" />.</returns>
		/// <param name="format">A standard or custom date and time format string (see Remarks). </param>
		/// <exception cref="T:System.FormatException">The length of <paramref name="format" /> is 1, and it is not one of the format specifier characters defined for <see cref="T:System.Globalization.DateTimeFormatInfo" />.-or- <paramref name="format" /> does not contain a valid custom format pattern. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by the current culture.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D45 RID: 3397 RVA: 0x000391CF File Offset: 0x000373CF
		public string ToString(string format)
		{
			return DateTimeFormat.Format(this, format, DateTimeFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <returns>A string representation of value of the current <see cref="T:System.DateTime" /> object as specified by <paramref name="provider" />.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by <paramref name="provider" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D46 RID: 3398 RVA: 0x000391E2 File Offset: 0x000373E2
		public string ToString(IFormatProvider provider)
		{
			return DateTimeFormat.Format(this, null, DateTimeFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <returns>A string representation of value of the current <see cref="T:System.DateTime" /> object as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="format">A standard or custom date and time format string. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The length of <paramref name="format" /> is 1, and it is not one of the format specifier characters defined for <see cref="T:System.Globalization.DateTimeFormatInfo" />.-or- <paramref name="format" /> does not contain a valid custom format pattern. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by <paramref name="provider" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D47 RID: 3399 RVA: 0x000391F6 File Offset: 0x000373F6
		public string ToString(string format, IFormatProvider provider)
		{
			return DateTimeFormat.Format(this, format, DateTimeFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to Coordinated Universal Time (UTC).</summary>
		/// <returns>An object whose <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Utc" />, and whose value is the UTC equivalent to the value of the current <see cref="T:System.DateTime" /> object, or <see cref="F:System.DateTime.MaxValue" /> if the converted value is too large to be represented by a <see cref="T:System.DateTime" /> object, or <see cref="F:System.DateTime.MinValue" /> if the converted value is too small to be represented by a <see cref="T:System.DateTime" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D48 RID: 3400 RVA: 0x0003920A File Offset: 0x0003740A
		public DateTime ToUniversalTime()
		{
			return TimeZoneInfo.ConvertTimeToUtc(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent and returns a value that indicates whether the conversion succeeded.</summary>
		/// <returns>true if the <paramref name="s" /> parameter was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string containing a date and time to convert. </param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.DateTime" /> value equivalent to the date and time contained in <paramref name="s" />, if the conversion succeeded, or <see cref="F:System.DateTime.MinValue" /> if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is an empty string (""), or does not contain a valid string representation of a date and time. This parameter is passed uninitialized. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D49 RID: 3401 RVA: 0x00039218 File Offset: 0x00037418
		public static bool TryParse(string s, out DateTime result)
		{
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified culture-specific format information and formatting style, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <returns>true if the <paramref name="s" /> parameter was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string containing a date and time to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="styles">A bitwise combination of enumeration values that defines how to interpret the parsed date in relation to the current time zone or the current date. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.DateTime" /> value equivalent to the date and time contained in <paramref name="s" />, if the conversion succeeded, or <see cref="F:System.DateTime.MinValue" /> if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is an empty string (""), or does not contain a valid string representation of a date and time. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> is not a valid <see cref="T:System.Globalization.DateTimeStyles" /> value.-or-<paramref name="styles" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values (for example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="provider" /> is a neutral culture and cannot be used in a parsing operation.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D4A RID: 3402 RVA: 0x00039227 File Offset: 0x00037427
		public static bool TryParse(string s, IFormatProvider provider, DateTimeStyles styles, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.GetInstance(provider), styles, out result);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly. The method returns a value that indicates whether the conversion succeeded.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string containing a date and time to convert. </param>
		/// <param name="format">The required format of <paramref name="s" />. See the Remarks section for more information. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="style">A bitwise combination of one or more enumeration values that indicate the permitted format of <paramref name="s" />. </param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.DateTime" /> value equivalent to the date and time contained in <paramref name="s" />, if the conversion succeeded, or <see cref="F:System.DateTime.MinValue" /> if the conversion failed. The conversion fails if either the <paramref name="s" /> or <paramref name="format" /> parameter is null, is an empty string, or does not contain a date and time that correspond to the pattern specified in <paramref name="format" />. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> is not a valid <see cref="T:System.Globalization.DateTimeStyles" /> value.-or-<paramref name="styles" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values (for example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />).</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D4B RID: 3403 RVA: 0x00039242 File Offset: 0x00037442
		public static bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.TryParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified array of formats, culture-specific format information, and style. The format of the string representation must match at least one of the specified formats exactly. The method returns a value that indicates whether the conversion succeeded.</summary>
		/// <returns>true if the <paramref name="s" /> parameter was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string that contains a date and time to convert. </param>
		/// <param name="formats">An array of allowable formats of <paramref name="s" />. See the Remarks section for more information. </param>
		/// <param name="provider">An object that supplies culture-specific format information about <paramref name="s" />. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.DateTime" /> value equivalent to the date and time contained in <paramref name="s" />, if the conversion succeeded, or <see cref="F:System.DateTime.MinValue" /> if the conversion failed. The conversion fails if <paramref name="s" /> or <paramref name="formats" /> is null, <paramref name="s" /> or an element of <paramref name="formats" /> is an empty string, or the format of <paramref name="s" /> is not exactly as specified by at least one of the format patterns in <paramref name="formats" />. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> is not a valid <see cref="T:System.Globalization.DateTimeStyles" /> value.-or-<paramref name="styles" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values (for example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />).</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D4C RID: 3404 RVA: 0x0003925F File Offset: 0x0003745F
		public static bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.TryParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		/// <summary>Adds a specified time interval to a specified date and time, yielding a new date and time.</summary>
		/// <returns>An object that is the sum of the values of <paramref name="d" /> and <paramref name="t" />.</returns>
		/// <param name="d">The date and time value to add. </param>
		/// <param name="t">The time interval to add. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000D4D RID: 3405 RVA: 0x0003927C File Offset: 0x0003747C
		public static DateTime operator +(DateTime d, TimeSpan t)
		{
			long internalTicks = d.InternalTicks;
			long ticks = t._ticks;
			if (ticks > 3155378975999999999L - internalTicks || ticks < 0L - internalTicks)
			{
				throw new ArgumentOutOfRangeException("t", Environment.GetResourceString("The added or subtracted value results in an un-representable DateTime."));
			}
			return new DateTime((ulong)((internalTicks + ticks) | (long)d.InternalKind));
		}

		/// <summary>Subtracts a specified time interval from a specified date and time and returns a new date and time.</summary>
		/// <returns>An object whose value is the value of <paramref name="d" /> minus the value of <paramref name="t" />.</returns>
		/// <param name="d">The date and time value to subtract from. </param>
		/// <param name="t">The time interval to subtract. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000D4E RID: 3406 RVA: 0x000392D4 File Offset: 0x000374D4
		public static DateTime operator -(DateTime d, TimeSpan t)
		{
			long internalTicks = d.InternalTicks;
			long ticks = t._ticks;
			if (internalTicks < ticks || internalTicks - 3155378975999999999L > ticks)
			{
				throw new ArgumentOutOfRangeException("t", Environment.GetResourceString("The added or subtracted value results in an un-representable DateTime."));
			}
			return new DateTime((ulong)((internalTicks - ticks) | (long)d.InternalKind));
		}

		/// <summary>Subtracts a specified date and time from another specified date and time and returns a time interval.</summary>
		/// <returns>The time interval between <paramref name="d1" /> and <paramref name="d2" />; that is, <paramref name="d1" /> minus <paramref name="d2" />.</returns>
		/// <param name="d1">The date and time value to subtract from (the minuend). </param>
		/// <param name="d2">The date and time value to subtract (the subtrahend). </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000D4F RID: 3407 RVA: 0x00039327 File Offset: 0x00037527
		public static TimeSpan operator -(DateTime d1, DateTime d2)
		{
			return new TimeSpan(d1.InternalTicks - d2.InternalTicks);
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.DateTime" /> are equal.</summary>
		/// <returns>true if <paramref name="d1" /> and <paramref name="d2" /> represent the same date and time; otherwise, false.</returns>
		/// <param name="d1">The first object to compare. </param>
		/// <param name="d2">The second object to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000D50 RID: 3408 RVA: 0x00038966 File Offset: 0x00036B66
		public static bool operator ==(DateTime d1, DateTime d2)
		{
			return d1.InternalTicks == d2.InternalTicks;
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.DateTime" /> are not equal.</summary>
		/// <returns>true if <paramref name="d1" /> and <paramref name="d2" /> do not represent the same date and time; otherwise, false.</returns>
		/// <param name="d1">The first object to compare. </param>
		/// <param name="d2">The second object to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000D51 RID: 3409 RVA: 0x0003933D File Offset: 0x0003753D
		public static bool operator !=(DateTime d1, DateTime d2)
		{
			return d1.InternalTicks != d2.InternalTicks;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTime" /> is earlier than another specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>true if <paramref name="t1" /> is less than <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">The first object to compare. </param>
		/// <param name="t2">The second object to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000D52 RID: 3410 RVA: 0x00039352 File Offset: 0x00037552
		public static bool operator <(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks < t2.InternalTicks;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTime" /> represents a date and time that is the same as or earlier than another specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>true if <paramref name="t1" /> is less than or equal to <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">The first object to compare. </param>
		/// <param name="t2">The second object to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000D53 RID: 3411 RVA: 0x00039364 File Offset: 0x00037564
		public static bool operator <=(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks <= t2.InternalTicks;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTime" /> is later than another specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>true if <paramref name="t1" /> is greater than <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">The first object to compare. </param>
		/// <param name="t2">The second object to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000D54 RID: 3412 RVA: 0x00039379 File Offset: 0x00037579
		public static bool operator >(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks > t2.InternalTicks;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTime" /> represents a date and time that is the same as or later than another specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>true if <paramref name="t1" /> is greater than or equal to <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">The first object to compare. </param>
		/// <param name="t2">The second object to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000D55 RID: 3413 RVA: 0x0003938B File Offset: 0x0003758B
		public static bool operator >=(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks >= t2.InternalTicks;
		}

		/// <summary>Converts the value of this instance to all the string representations supported by the standard date and time format specifiers.</summary>
		/// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D56 RID: 3414 RVA: 0x000393A0 File Offset: 0x000375A0
		public string[] GetDateTimeFormats()
		{
			return this.GetDateTimeFormats(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of this instance to all the string representations supported by the standard date and time format specifiers and the specified culture-specific formatting information.</summary>
		/// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information about this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D57 RID: 3415 RVA: 0x000393AD File Offset: 0x000375AD
		public string[] GetDateTimeFormats(IFormatProvider provider)
		{
			return DateTimeFormat.GetAllDateTimes(this, DateTimeFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the value of this instance to all the string representations supported by the specified standard date and time format specifier.</summary>
		/// <returns>A string array where each element is the representation of the value of this instance formatted with the <paramref name="format" /> standard date and time format specifier.</returns>
		/// <param name="format">A standard date and time format string (see Remarks). </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid standard date and time format specifier character.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D58 RID: 3416 RVA: 0x000393C0 File Offset: 0x000375C0
		public string[] GetDateTimeFormats(char format)
		{
			return this.GetDateTimeFormats(format, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of this instance to all the string representations supported by the specified standard date and time format specifier and culture-specific formatting information.</summary>
		/// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.</returns>
		/// <param name="format">A date and time format string (see Remarks). </param>
		/// <param name="provider">An object that supplies culture-specific formatting information about this instance. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid standard date and time format specifier character.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D59 RID: 3417 RVA: 0x000393CE File Offset: 0x000375CE
		public string[] GetDateTimeFormats(char format, IFormatProvider provider)
		{
			return DateTimeFormat.GetAllDateTimes(this, format, DateTimeFormatInfo.GetInstance(provider));
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.DateTime" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.DateTime" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D5A RID: 3418 RVA: 0x000393E2 File Offset: 0x000375E2
		public TypeCode GetTypeCode()
		{
			return TypeCode.DateTime;
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000D5B RID: 3419 RVA: 0x000393E6 File Offset: 0x000375E6
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "DateTime", "Boolean" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000D5C RID: 3420 RVA: 0x0003940D File Offset: 0x0003760D
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "DateTime", "Char" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000D5D RID: 3421 RVA: 0x00039434 File Offset: 0x00037634
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "DateTime", "SByte" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000D5E RID: 3422 RVA: 0x0003945B File Offset: 0x0003765B
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "DateTime", "Byte" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000D5F RID: 3423 RVA: 0x00039482 File Offset: 0x00037682
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "DateTime", "Int16" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000D60 RID: 3424 RVA: 0x000394A9 File Offset: 0x000376A9
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "DateTime", "UInt16" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000D61 RID: 3425 RVA: 0x000394D0 File Offset: 0x000376D0
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "DateTime", "Int32" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000D62 RID: 3426 RVA: 0x000394F7 File Offset: 0x000376F7
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "DateTime", "UInt32" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000D63 RID: 3427 RVA: 0x0003951E File Offset: 0x0003771E
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "DateTime", "Int64" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000D64 RID: 3428 RVA: 0x00039545 File Offset: 0x00037745
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "DateTime", "UInt64" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases. </exception>
		// Token: 0x06000D65 RID: 3429 RVA: 0x0003956C File Offset: 0x0003776C
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "DateTime", "Single" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000D66 RID: 3430 RVA: 0x00039593 File Offset: 0x00037793
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "DateTime", "Double" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000D67 RID: 3431 RVA: 0x000395BA File Offset: 0x000377BA
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "DateTime", "Decimal" }));
		}

		/// <summary>Returns the current <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The current object.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		// Token: 0x06000D68 RID: 3432 RVA: 0x000395E1 File Offset: 0x000377E1
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>Converts the current <see cref="T:System.DateTime" /> object to an object of a specified type.</summary>
		/// <returns>An object of the type specified by the <paramref name="type" /> parameter, with a value equivalent to the current <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="type">The desired type. </param>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DateTime" /> type.</exception>
		// Token: 0x06000D69 RID: 3433 RVA: 0x000395E9 File Offset: 0x000377E9
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00039600 File Offset: 0x00037800
		internal static bool TryCreate(int year, int month, int day, int hour, int minute, int second, int millisecond, out DateTime result)
		{
			result = DateTime.MinValue;
			if (year < 1 || year > 9999 || month < 1 || month > 12)
			{
				return false;
			}
			int[] array = (DateTime.IsLeapYear(year) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365);
			if (day < 1 || day > array[month] - array[month - 1])
			{
				return false;
			}
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
			{
				return false;
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				return false;
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				return false;
			}
			result = new DateTime(num, DateTimeKind.Unspecified);
			return true;
		}

		// Token: 0x0400088B RID: 2187
		private const long TicksPerMillisecond = 10000L;

		// Token: 0x0400088C RID: 2188
		private const long TicksPerSecond = 10000000L;

		// Token: 0x0400088D RID: 2189
		private const long TicksPerMinute = 600000000L;

		// Token: 0x0400088E RID: 2190
		private const long TicksPerHour = 36000000000L;

		// Token: 0x0400088F RID: 2191
		private const long TicksPerDay = 864000000000L;

		// Token: 0x04000890 RID: 2192
		private const int MillisPerSecond = 1000;

		// Token: 0x04000891 RID: 2193
		private const int MillisPerMinute = 60000;

		// Token: 0x04000892 RID: 2194
		private const int MillisPerHour = 3600000;

		// Token: 0x04000893 RID: 2195
		private const int MillisPerDay = 86400000;

		// Token: 0x04000894 RID: 2196
		private const int DaysPerYear = 365;

		// Token: 0x04000895 RID: 2197
		private const int DaysPer4Years = 1461;

		// Token: 0x04000896 RID: 2198
		private const int DaysPer100Years = 36524;

		// Token: 0x04000897 RID: 2199
		private const int DaysPer400Years = 146097;

		// Token: 0x04000898 RID: 2200
		private const int DaysTo1601 = 584388;

		// Token: 0x04000899 RID: 2201
		private const int DaysTo1899 = 693593;

		// Token: 0x0400089A RID: 2202
		internal const int DaysTo1970 = 719162;

		// Token: 0x0400089B RID: 2203
		private const int DaysTo10000 = 3652059;

		// Token: 0x0400089C RID: 2204
		internal const long MinTicks = 0L;

		// Token: 0x0400089D RID: 2205
		internal const long MaxTicks = 3155378975999999999L;

		// Token: 0x0400089E RID: 2206
		private const long MaxMillis = 315537897600000L;

		// Token: 0x0400089F RID: 2207
		private const long FileTimeOffset = 504911232000000000L;

		// Token: 0x040008A0 RID: 2208
		private const long DoubleDateOffset = 599264352000000000L;

		// Token: 0x040008A1 RID: 2209
		private const long OADateMinAsTicks = 31241376000000000L;

		// Token: 0x040008A2 RID: 2210
		private const double OADateMinAsDouble = -657435.0;

		// Token: 0x040008A3 RID: 2211
		private const double OADateMaxAsDouble = 2958466.0;

		// Token: 0x040008A4 RID: 2212
		private const int DatePartYear = 0;

		// Token: 0x040008A5 RID: 2213
		private const int DatePartDayOfYear = 1;

		// Token: 0x040008A6 RID: 2214
		private const int DatePartMonth = 2;

		// Token: 0x040008A7 RID: 2215
		private const int DatePartDay = 3;

		// Token: 0x040008A8 RID: 2216
		private static readonly int[] DaysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334, 365
		};

		// Token: 0x040008A9 RID: 2217
		private static readonly int[] DaysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335, 366
		};

		/// <summary>Represents the smallest possible value of <see cref="T:System.DateTime" />. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008AA RID: 2218
		public static readonly DateTime MinValue = new DateTime(0L, DateTimeKind.Unspecified);

		/// <summary>Represents the largest possible value of <see cref="T:System.DateTime" />. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008AB RID: 2219
		public static readonly DateTime MaxValue = new DateTime(3155378975999999999L, DateTimeKind.Unspecified);

		// Token: 0x040008AC RID: 2220
		private const ulong TicksMask = 4611686018427387903UL;

		// Token: 0x040008AD RID: 2221
		private const ulong FlagsMask = 13835058055282163712UL;

		// Token: 0x040008AE RID: 2222
		private const ulong LocalMask = 9223372036854775808UL;

		// Token: 0x040008AF RID: 2223
		private const long TicksCeiling = 4611686018427387904L;

		// Token: 0x040008B0 RID: 2224
		private const ulong KindUnspecified = 0UL;

		// Token: 0x040008B1 RID: 2225
		private const ulong KindUtc = 4611686018427387904UL;

		// Token: 0x040008B2 RID: 2226
		private const ulong KindLocal = 9223372036854775808UL;

		// Token: 0x040008B3 RID: 2227
		private const ulong KindLocalAmbiguousDst = 13835058055282163712UL;

		// Token: 0x040008B4 RID: 2228
		private const int KindShift = 62;

		// Token: 0x040008B5 RID: 2229
		private const string TicksField = "ticks";

		// Token: 0x040008B6 RID: 2230
		private const string DateDataField = "dateData";

		// Token: 0x040008B7 RID: 2231
		private ulong dateData;
	}
}
