using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	/// <summary>Represents a time interval.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001CE RID: 462
	[ComVisible(true)]
	[Serializable]
	public struct TimeSpan : IComparable, IComparable<TimeSpan>, IEquatable<TimeSpan>, IFormattable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TimeSpan" /> structure to the specified number of ticks.</summary>
		/// <param name="ticks">A time period expressed in 100-nanosecond units. </param>
		// Token: 0x06001436 RID: 5174 RVA: 0x00051D34 File Offset: 0x0004FF34
		public TimeSpan(long ticks)
		{
			this._ticks = ticks;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeSpan" /> structure to a specified number of hours, minutes, and seconds.</summary>
		/// <param name="hours">Number of hours. </param>
		/// <param name="minutes">Number of minutes. </param>
		/// <param name="seconds">Number of seconds. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameters specify a <see cref="T:System.TimeSpan" /> value less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		// Token: 0x06001437 RID: 5175 RVA: 0x00051D3D File Offset: 0x0004FF3D
		public TimeSpan(int hours, int minutes, int seconds)
		{
			this._ticks = TimeSpan.TimeToTicks(hours, minutes, seconds);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeSpan" /> structure to a specified number of days, hours, minutes, and seconds.</summary>
		/// <param name="days">Number of days. </param>
		/// <param name="hours">Number of hours. </param>
		/// <param name="minutes">Number of minutes. </param>
		/// <param name="seconds">Number of seconds. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameters specify a <see cref="T:System.TimeSpan" /> value less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		// Token: 0x06001438 RID: 5176 RVA: 0x00051D4D File Offset: 0x0004FF4D
		public TimeSpan(int days, int hours, int minutes, int seconds)
		{
			this = new TimeSpan(days, hours, minutes, seconds, 0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeSpan" /> strrructure to a specified number of days, hours, minutes, seconds, and milliseconds.</summary>
		/// <param name="days">Number of days. </param>
		/// <param name="hours">Number of hours. </param>
		/// <param name="minutes">Number of minutes. </param>
		/// <param name="seconds">Number of seconds. </param>
		/// <param name="milliseconds">Number of milliseconds. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameters specify a <see cref="T:System.TimeSpan" /> value less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		// Token: 0x06001439 RID: 5177 RVA: 0x00051D5C File Offset: 0x0004FF5C
		public TimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
		{
			long num = ((long)days * 3600L * 24L + (long)hours * 3600L + (long)minutes * 60L + (long)seconds) * 1000L + (long)milliseconds;
			if (num > 922337203685477L || num < -922337203685477L)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("TimeSpan overflowed because the duration is too long."));
			}
			this._ticks = num * 10000L;
		}

		/// <summary>Gets the number of ticks that represent the value of the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The number of ticks contained in this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x00051DCE File Offset: 0x0004FFCE
		public long Ticks
		{
			get
			{
				return this._ticks;
			}
		}

		/// <summary>Gets the days component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The day component of this instance. The return value can be positive or negative.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x00051DD6 File Offset: 0x0004FFD6
		public int Days
		{
			get
			{
				return (int)(this._ticks / 864000000000L);
			}
		}

		/// <summary>Gets the hours component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The hour component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -23 through 23.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00051DE9 File Offset: 0x0004FFE9
		public int Hours
		{
			get
			{
				return (int)(this._ticks / 36000000000L % 24L);
			}
		}

		/// <summary>Gets the milliseconds component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The millisecond component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -999 through 999.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x00051E00 File Offset: 0x00050000
		public int Milliseconds
		{
			get
			{
				return (int)(this._ticks / 10000L % 1000L);
			}
		}

		/// <summary>Gets the minutes component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The minute component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -59 through 59.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x00051E17 File Offset: 0x00050017
		public int Minutes
		{
			get
			{
				return (int)(this._ticks / 600000000L % 60L);
			}
		}

		/// <summary>Gets the seconds component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The second component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -59 through 59.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x00051E2B File Offset: 0x0005002B
		public int Seconds
		{
			get
			{
				return (int)(this._ticks / 10000000L % 60L);
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional days.</summary>
		/// <returns>The total number of days represented by this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x00051E3F File Offset: 0x0005003F
		public double TotalDays
		{
			get
			{
				return (double)this._ticks * 1.1574074074074074E-12;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional hours.</summary>
		/// <returns>The total number of hours represented by this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x00051E52 File Offset: 0x00050052
		public double TotalHours
		{
			get
			{
				return (double)this._ticks * 2.7777777777777777E-11;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional milliseconds.</summary>
		/// <returns>The total number of milliseconds represented by this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00051E68 File Offset: 0x00050068
		public double TotalMilliseconds
		{
			get
			{
				double num = (double)this._ticks * 0.0001;
				if (num > 922337203685477.0)
				{
					return 922337203685477.0;
				}
				if (num < -922337203685477.0)
				{
					return -922337203685477.0;
				}
				return num;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional minutes.</summary>
		/// <returns>The total number of minutes represented by this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x00051EB4 File Offset: 0x000500B4
		public double TotalMinutes
		{
			get
			{
				return (double)this._ticks * 1.6666666666666667E-09;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional seconds.</summary>
		/// <returns>The total number of seconds represented by this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00051EC7 File Offset: 0x000500C7
		public double TotalSeconds
		{
			get
			{
				return (double)this._ticks * 1E-07;
			}
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the sum of the specified <see cref="T:System.TimeSpan" /> object and this instance.</summary>
		/// <returns>A new object that represents the value of this instance plus the value of <paramref name="ts" />.</returns>
		/// <param name="ts">The time interval to add.</param>
		/// <exception cref="T:System.OverflowException">The resulting <see cref="T:System.TimeSpan" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001445 RID: 5189 RVA: 0x00051EDC File Offset: 0x000500DC
		public TimeSpan Add(TimeSpan ts)
		{
			long num = this._ticks + ts._ticks;
			if (this._ticks >> 63 == ts._ticks >> 63 && this._ticks >> 63 != num >> 63)
			{
				throw new OverflowException(Environment.GetResourceString("TimeSpan overflowed because the duration is too long."));
			}
			return new TimeSpan(num);
		}

		/// <summary>Compares two <see cref="T:System.TimeSpan" /> values and returns an integer that indicates whether the first value is shorter than, equal to, or longer than the second value.</summary>
		/// <returns>One of the following values.Value Description -1 <paramref name="t1" /> is shorter than <paramref name="t2" />. 0 <paramref name="t1" /> is equal to <paramref name="t2" />. 1 <paramref name="t1" /> is longer than <paramref name="t2" />. </returns>
		/// <param name="t1">The first time interval to compare. </param>
		/// <param name="t2">The second time interval to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001446 RID: 5190 RVA: 0x00051F30 File Offset: 0x00050130
		public static int Compare(TimeSpan t1, TimeSpan t2)
		{
			if (t1._ticks > t2._ticks)
			{
				return 1;
			}
			if (t1._ticks < t2._ticks)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified object and returns an integer that indicates whether this instance is shorter than, equal to, or longer than the specified object.</summary>
		/// <returns>One of the following values.Value Description -1 This instance is shorter than <paramref name="value" />. 0 This instance is equal to <paramref name="value" />. 1 This instance is longer than <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.TimeSpan" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001447 RID: 5191 RVA: 0x00051F54 File Offset: 0x00050154
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is TimeSpan))
			{
				throw new ArgumentException(Environment.GetResourceString("Object must be of type TimeSpan."));
			}
			long ticks = ((TimeSpan)value)._ticks;
			if (this._ticks > ticks)
			{
				return 1;
			}
			if (this._ticks < ticks)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.TimeSpan" /> object and returns an integer that indicates whether this instance is shorter than, equal to, or longer than the <see cref="T:System.TimeSpan" /> object.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Value Description A negative integer This instance is shorter than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. A positive integer This instance is longer than <paramref name="value" />. </returns>
		/// <param name="value">An object to compare to this instance.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001448 RID: 5192 RVA: 0x00051FA4 File Offset: 0x000501A4
		public int CompareTo(TimeSpan value)
		{
			long ticks = value._ticks;
			if (this._ticks > ticks)
			{
				return 1;
			}
			if (this._ticks < ticks)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of days, where the specification is accurate to the nearest millisecond.</summary>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <param name="value">A number of days, accurate to the nearest millisecond. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. -or-<paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.-or-<paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001449 RID: 5193 RVA: 0x00051FCF File Offset: 0x000501CF
		public static TimeSpan FromDays(double value)
		{
			return TimeSpan.Interval(value, 86400000);
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the absolute value of the current <see cref="T:System.TimeSpan" /> object.</summary>
		/// <returns>A new object whose value is the absolute value of the current <see cref="T:System.TimeSpan" /> object.</returns>
		/// <exception cref="T:System.OverflowException">The value of this instance is <see cref="F:System.TimeSpan.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600144A RID: 5194 RVA: 0x00051FDC File Offset: 0x000501DC
		public TimeSpan Duration()
		{
			if (this.Ticks == TimeSpan.MinValue.Ticks)
			{
				throw new OverflowException(Environment.GetResourceString("The duration cannot be returned for TimeSpan.MinValue because the absolute value of TimeSpan.MinValue exceeds the value of TimeSpan.MaxValue."));
			}
			return new TimeSpan((this._ticks >= 0L) ? this._ticks : (-this._ticks));
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.TimeSpan" /> object that represents the same time interval as the current <see cref="T:System.TimeSpan" /> structure; otherwise, false.</returns>
		/// <param name="value">An object to compare with this instance. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600144B RID: 5195 RVA: 0x0005202C File Offset: 0x0005022C
		public override bool Equals(object value)
		{
			return value is TimeSpan && this._ticks == ((TimeSpan)value)._ticks;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.TimeSpan" /> object.</summary>
		/// <returns>true if <paramref name="obj" /> represents the same time interval as this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600144C RID: 5196 RVA: 0x0005204B File Offset: 0x0005024B
		public bool Equals(TimeSpan obj)
		{
			return this._ticks == obj._ticks;
		}

		/// <summary>Returns a value that indicates whether two specified instances of <see cref="T:System.TimeSpan" /> are equal.</summary>
		/// <returns>true if the values of <paramref name="t1" /> and <paramref name="t2" /> are equal; otherwise, false.</returns>
		/// <param name="t1">The first time interval to compare. </param>
		/// <param name="t2">The second time interval to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600144D RID: 5197 RVA: 0x0005204B File Offset: 0x0005024B
		public static bool Equals(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks == t2._ticks;
		}

		/// <summary>Returns a hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600144E RID: 5198 RVA: 0x0005205B File Offset: 0x0005025B
		public override int GetHashCode()
		{
			return (int)this._ticks ^ (int)(this._ticks >> 32);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of hours, where the specification is accurate to the nearest millisecond.</summary>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <param name="value">A number of hours accurate to the nearest millisecond. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. -or-<paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.-or-<paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600144F RID: 5199 RVA: 0x0005206F File Offset: 0x0005026F
		public static TimeSpan FromHours(double value)
		{
			return TimeSpan.Interval(value, 3600000);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0005207C File Offset: 0x0005027C
		private static TimeSpan Interval(double value, int scale)
		{
			if (double.IsNaN(value))
			{
				throw new ArgumentException(Environment.GetResourceString("TimeSpan does not accept floating point Not-a-Number values."));
			}
			double num = value * (double)scale + ((value >= 0.0) ? 0.5 : (-0.5));
			if (num > 922337203685477.0 || num < -922337203685477.0)
			{
				throw new OverflowException(Environment.GetResourceString("TimeSpan overflowed because the duration is too long."));
			}
			return new TimeSpan((long)num * 10000L);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of milliseconds.</summary>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <param name="value">A number of milliseconds. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or-<paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.-or-<paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001451 RID: 5201 RVA: 0x000520FD File Offset: 0x000502FD
		public static TimeSpan FromMilliseconds(double value)
		{
			return TimeSpan.Interval(value, 1);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of minutes, where the specification is accurate to the nearest millisecond.</summary>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <param name="value">A number of minutes, accurate to the nearest millisecond. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or-<paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.-or-<paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001452 RID: 5202 RVA: 0x00052106 File Offset: 0x00050306
		public static TimeSpan FromMinutes(double value)
		{
			return TimeSpan.Interval(value, 60000);
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the negated value of this instance.</summary>
		/// <returns>A new object with the same numeric value as this instance, but with the opposite sign.</returns>
		/// <exception cref="T:System.OverflowException">The negated value of this instance cannot be represented by a <see cref="T:System.TimeSpan" />; that is, the value of this instance is <see cref="F:System.TimeSpan.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001453 RID: 5203 RVA: 0x00052114 File Offset: 0x00050314
		public TimeSpan Negate()
		{
			if (this.Ticks == TimeSpan.MinValue.Ticks)
			{
				throw new OverflowException(Environment.GetResourceString("Negating the minimum value of a twos complement number is invalid."));
			}
			return new TimeSpan(-this._ticks);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of seconds, where the specification is accurate to the nearest millisecond.</summary>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <param name="value">A number of seconds, accurate to the nearest millisecond. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or-<paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.-or-<paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001454 RID: 5204 RVA: 0x00052152 File Offset: 0x00050352
		public static TimeSpan FromSeconds(double value)
		{
			return TimeSpan.Interval(value, 1000);
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the difference between the specified <see cref="T:System.TimeSpan" /> object and this instance.</summary>
		/// <returns>A new time interval whose value is the result of the value of this instance minus the value of <paramref name="ts" />.</returns>
		/// <param name="ts">The time interval to be subtracted. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001455 RID: 5205 RVA: 0x00052160 File Offset: 0x00050360
		public TimeSpan Subtract(TimeSpan ts)
		{
			long num = this._ticks - ts._ticks;
			if (this._ticks >> 63 != ts._ticks >> 63 && this._ticks >> 63 != num >> 63)
			{
				throw new OverflowException(Environment.GetResourceString("TimeSpan overflowed because the duration is too long."));
			}
			return new TimeSpan(num);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified time, where the specification is in units of ticks.</summary>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <param name="value">A number of ticks that represent a time. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001456 RID: 5206 RVA: 0x000521B4 File Offset: 0x000503B4
		public static TimeSpan FromTicks(long value)
		{
			return new TimeSpan(value);
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x000521BC File Offset: 0x000503BC
		internal static long TimeToTicks(int hour, int minute, int second)
		{
			long num = (long)hour * 3600L + (long)minute * 60L + (long)second;
			if (num > 922337203685L || num < -922337203685L)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("TimeSpan overflowed because the duration is too long."));
			}
			return num * 10000000L;
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent. </summary>
		/// <returns>A time interval that corresponds to <paramref name="s" />.</returns>
		/// <param name="s">A string that specifies the time interval to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> has an invalid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or- At least one of the days, hours, minutes, or seconds components is outside its valid range. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001458 RID: 5208 RVA: 0x0005220E File Offset: 0x0005040E
		public static TimeSpan Parse(string s)
		{
			return TimeSpanParse.Parse(s, null);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified culture-specific format information.</summary>
		/// <returns>A time interval that corresponds to <paramref name="input" />, as specified by <paramref name="formatProvider" />.</returns>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> has an invalid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="input" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or- At least one of the days, hours, minutes, or seconds components in <paramref name="input" /> is outside its valid range. </exception>
		// Token: 0x06001459 RID: 5209 RVA: 0x00052217 File Offset: 0x00050417
		public static TimeSpan Parse(string input, IFormatProvider formatProvider)
		{
			return TimeSpanParse.Parse(input, formatProvider);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified format and culture-specific format information. The format of the string representation must match the specified format exactly.</summary>
		/// <returns>A time interval that corresponds to <paramref name="input" />, as specified by <paramref name="format" /> and <paramref name="formatProvider" />.</returns>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="format">A standard or custom format string that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> has an invalid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="input" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or- At least one of the days, hours, minutes, or seconds components in <paramref name="input" /> is outside its valid range. </exception>
		// Token: 0x0600145A RID: 5210 RVA: 0x00052220 File Offset: 0x00050420
		public static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider)
		{
			return TimeSpanParse.ParseExact(input, format, formatProvider, TimeSpanStyles.None);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified array of format strings and culture-specific format information. The format of the string representation must match one of the specified formats exactly.</summary>
		/// <returns>A time interval that corresponds to <paramref name="input" />, as specified by <paramref name="formats" /> and <paramref name="formatProvider" />.</returns>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formats">A array of standard or custom format strings that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> has an invalid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="input" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or- At least one of the days, hours, minutes, or seconds components in <paramref name="input" /> is outside its valid range. </exception>
		// Token: 0x0600145B RID: 5211 RVA: 0x0005222B File Offset: 0x0005042B
		public static TimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider)
		{
			return TimeSpanParse.ParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified format, culture-specific format information, and styles. The format of the string representation must match the specified format exactly.</summary>
		/// <returns>A time interval that corresponds to <paramref name="input" />, as specified by <paramref name="format" />, <paramref name="formatProvider" />, and <paramref name="styles" />.</returns>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="format">A standard or custom format string that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <param name="styles">A bitwise combination of enumeration values that defines the style elements that may be present in <paramref name="input" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> is an invalid <see cref="T:System.Globalization.TimeSpanStyles" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> has an invalid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="input" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or- At least one of the days, hours, minutes, or seconds components in <paramref name="input" /> is outside its valid range. </exception>
		// Token: 0x0600145C RID: 5212 RVA: 0x00052236 File Offset: 0x00050436
		public static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpanParse.ValidateStyles(styles, "styles");
			return TimeSpanParse.ParseExact(input, format, formatProvider, styles);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified formats, culture-specific format information, and styles. The format of the string representation must match one of the specified formats exactly. </summary>
		/// <returns>A time interval that corresponds to <paramref name="input" />, as specified by <paramref name="formats" />, <paramref name="formatProvider" />, and <paramref name="styles" />.</returns>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formats">A array of standard or custom format strings that define the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <param name="styles">A bitwise combination of enumeration values that defines the style elements that may be present in input.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> is an invalid <see cref="T:System.Globalization.TimeSpanStyles" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> has an invalid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="input" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or- At least one of the days, hours, minutes, or seconds components in <paramref name="input" /> is outside its valid range. </exception>
		// Token: 0x0600145D RID: 5213 RVA: 0x0005224C File Offset: 0x0005044C
		public static TimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpanParse.ValidateStyles(styles, "styles");
			return TimeSpanParse.ParseExactMultiple(input, formats, formatProvider, styles);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent and returns a value that indicates whether the conversion succeeded.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false. This operation returns false if the <paramref name="s" /> parameter is null or <see cref="F:System.String.Empty" />, has an invalid format, represents a time interval that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />, or has at least one days, hours, minutes, or seconds component outside its valid range.</returns>
		/// <param name="s">A string that specifies the time interval to convert.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="s" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600145E RID: 5214 RVA: 0x00052262 File Offset: 0x00050462
		public static bool TryParse(string s, out TimeSpan result)
		{
			return TimeSpanParse.TryParse(s, null, out result);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified culture-specific formatting information, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <returns>true if <paramref name="input" /> was converted successfully; otherwise, false. This operation returns false if the <paramref name="input" /> parameter is null or <see cref="F:System.String.Empty" />, has an invalid format, represents a time interval that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />, or has at least one days, hours, minutes, or seconds component outside its valid range.</returns>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		// Token: 0x0600145F RID: 5215 RVA: 0x0005226C File Offset: 0x0005046C
		public static bool TryParse(string input, IFormatProvider formatProvider, out TimeSpan result)
		{
			return TimeSpanParse.TryParse(input, formatProvider, out result);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified format and culture-specific format information, and returns a value that indicates whether the conversion succeeded. The format of the string representation must match the specified format exactly. </summary>
		/// <returns>true if <paramref name="input" /> was converted successfully; otherwise, false.</returns>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="format">A standard or custom format string that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		// Token: 0x06001460 RID: 5216 RVA: 0x00052276 File Offset: 0x00050476
		public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, out TimeSpan result)
		{
			return TimeSpanParse.TryParseExact(input, format, formatProvider, TimeSpanStyles.None, out result);
		}

		/// <summary>Converts the specified string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified formats and culture-specific format information, and returns a value that indicates whether the conversion succeeded. The format of the string representation must match one of the specified formats exactly. </summary>
		/// <returns>true if <paramref name="input" /> was converted successfully; otherwise, false.</returns>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formats">A array of standard or custom format strings that define the acceptable formats of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		// Token: 0x06001461 RID: 5217 RVA: 0x00052282 File Offset: 0x00050482
		public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, out TimeSpan result)
		{
			return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None, out result);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified format, culture-specific format information, and styles, and returns a value that indicates whether the conversion succeeded. The format of the string representation must match the specified format exactly. </summary>
		/// <returns>true if <paramref name="input" /> was converted successfully; otherwise, false.</returns>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="format">A standard or custom format string that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <param name="styles">One or more enumeration values that indicate the style of <paramref name="input" />.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		// Token: 0x06001462 RID: 5218 RVA: 0x0005228E File Offset: 0x0005048E
		public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpanParse.ValidateStyles(styles, "styles");
			return TimeSpanParse.TryParseExact(input, format, formatProvider, styles, out result);
		}

		/// <summary>Converts the specified string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified formats, culture-specific format information, and styles, and returns a value that indicates whether the conversion succeeded. The format of the string representation must match one of the specified formats exactly. </summary>
		/// <returns>true if <paramref name="input" /> was converted successfully; otherwise, false.</returns>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formats">A array of standard or custom format strings that define the acceptable formats of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <param name="styles">One or more enumeration values that indicate the style of <paramref name="input" />.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		// Token: 0x06001463 RID: 5219 RVA: 0x000522A6 File Offset: 0x000504A6
		public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpanParse.ValidateStyles(styles, "styles");
			return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, styles, out result);
		}

		/// <summary>Converts the value of the current <see cref="T:System.TimeSpan" /> object to its equivalent string representation.</summary>
		/// <returns>The string representation of the current <see cref="T:System.TimeSpan" /> value. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001464 RID: 5220 RVA: 0x000522BE File Offset: 0x000504BE
		public override string ToString()
		{
			return TimeSpanFormat.Format(this, null, null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.TimeSpan" /> object to its equivalent string representation by using the specified format.</summary>
		/// <returns>The string representation of the current <see cref="T:System.TimeSpan" /> value in the format specified by the <paramref name="format" /> parameter.</returns>
		/// <param name="format">A standard or custom <see cref="T:System.TimeSpan" /> format string.</param>
		/// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter is not recognized or is not supported.</exception>
		// Token: 0x06001465 RID: 5221 RVA: 0x000522CD File Offset: 0x000504CD
		public string ToString(string format)
		{
			return TimeSpanFormat.Format(this, format, null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.TimeSpan" /> object to its equivalent string representation by using the specified format and culture-specific formatting information.</summary>
		/// <returns>The string representation of the current <see cref="T:System.TimeSpan" /> value, as specified by <paramref name="format" /> and <paramref name="formatProvider" />.</returns>
		/// <param name="format">A standard or custom <see cref="T:System.TimeSpan" /> format string.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter is not recognized or is not supported.</exception>
		// Token: 0x06001466 RID: 5222 RVA: 0x000522DC File Offset: 0x000504DC
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (TimeSpan.LegacyMode)
			{
				return TimeSpanFormat.Format(this, null, null);
			}
			return TimeSpanFormat.Format(this, format, formatProvider);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> whose value is the negated value of the specified instance.</summary>
		/// <returns>An object that has the same numeric value as this instance, but the opposite sign.</returns>
		/// <param name="t">The time interval to be negated. </param>
		/// <exception cref="T:System.OverflowException">The negated value of this instance cannot be represented by a <see cref="T:System.TimeSpan" />; that is, the value of this instance is <see cref="F:System.TimeSpan.MinValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001467 RID: 5223 RVA: 0x00052300 File Offset: 0x00050500
		public static TimeSpan operator -(TimeSpan t)
		{
			if (t._ticks == TimeSpan.MinValue._ticks)
			{
				throw new OverflowException(Environment.GetResourceString("Negating the minimum value of a twos complement number is invalid."));
			}
			return new TimeSpan(-t._ticks);
		}

		/// <summary>Subtracts a specified <see cref="T:System.TimeSpan" /> from another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>An object whose value is the result of the value of <paramref name="t1" /> minus the value of <paramref name="t2" />.</returns>
		/// <param name="t1">The minuend. </param>
		/// <param name="t2">The subtrahend. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001468 RID: 5224 RVA: 0x00052330 File Offset: 0x00050530
		public static TimeSpan operator -(TimeSpan t1, TimeSpan t2)
		{
			return t1.Subtract(t2);
		}

		/// <summary>Returns the specified instance of <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>The time interval specified by <paramref name="t" />.</returns>
		/// <param name="t">The time interval to return. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001469 RID: 5225 RVA: 0x00002119 File Offset: 0x00000319
		public static TimeSpan operator +(TimeSpan t)
		{
			return t;
		}

		/// <summary>Adds two specified <see cref="T:System.TimeSpan" /> instances.</summary>
		/// <returns>An object whose value is the sum of the values of <paramref name="t1" /> and <paramref name="t2" />.</returns>
		/// <param name="t1">The first time interval to add. </param>
		/// <param name="t2">The second time interval to add.</param>
		/// <exception cref="T:System.OverflowException">The resulting <see cref="T:System.TimeSpan" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600146A RID: 5226 RVA: 0x0005233A File Offset: 0x0005053A
		public static TimeSpan operator +(TimeSpan t1, TimeSpan t2)
		{
			return t1.Add(t2);
		}

		/// <summary>Indicates whether two <see cref="T:System.TimeSpan" /> instances are equal.</summary>
		/// <returns>true if the values of <paramref name="t1" /> and <paramref name="t2" /> are equal; otherwise, false.</returns>
		/// <param name="t1">The first time interval to compare. </param>
		/// <param name="t2">The second time interval to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600146B RID: 5227 RVA: 0x0005204B File Offset: 0x0005024B
		public static bool operator ==(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks == t2._ticks;
		}

		/// <summary>Indicates whether two <see cref="T:System.TimeSpan" /> instances are not equal.</summary>
		/// <returns>true if the values of <paramref name="t1" /> and <paramref name="t2" /> are not equal; otherwise, false.</returns>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600146C RID: 5228 RVA: 0x00052344 File Offset: 0x00050544
		public static bool operator !=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks != t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is less than another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>true if the value of <paramref name="t1" /> is less than the value of <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600146D RID: 5229 RVA: 0x00052357 File Offset: 0x00050557
		public static bool operator <(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks < t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is less than or equal to another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>true if the value of <paramref name="t1" /> is less than or equal to the value of <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">The first time interval to compare. </param>
		/// <param name="t2">The second time interval to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600146E RID: 5230 RVA: 0x00052367 File Offset: 0x00050567
		public static bool operator <=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks <= t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is greater than another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>true if the value of <paramref name="t1" /> is greater than the value of <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">The first time interval to compare. </param>
		/// <param name="t2">The second time interval to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600146F RID: 5231 RVA: 0x0005237A File Offset: 0x0005057A
		public static bool operator >(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks > t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is greater than or equal to another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>true if the value of <paramref name="t1" /> is greater than or equal to the value of <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001470 RID: 5232 RVA: 0x0005238A File Offset: 0x0005058A
		public static bool operator >=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks >= t2._ticks;
		}

		// Token: 0x06001471 RID: 5233
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LegacyFormatMode();

		// Token: 0x06001472 RID: 5234 RVA: 0x0005239D File Offset: 0x0005059D
		[SecuritySafeCritical]
		private static bool GetLegacyFormatMode()
		{
			return CompatibilitySwitches.IsAppEarlierThanSilverlight4;
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x000523A4 File Offset: 0x000505A4
		private static bool LegacyMode
		{
			get
			{
				if (!TimeSpan._legacyConfigChecked)
				{
					TimeSpan._legacyMode = TimeSpan.GetLegacyFormatMode();
					TimeSpan._legacyConfigChecked = true;
				}
				return TimeSpan._legacyMode;
			}
		}

		/// <summary>Represents the number of ticks in 1 millisecond. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B3A RID: 2874
		public const long TicksPerMillisecond = 10000L;

		// Token: 0x04000B3B RID: 2875
		private const double MillisecondsPerTick = 0.0001;

		/// <summary>Represents the number of ticks in 1 second.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B3C RID: 2876
		public const long TicksPerSecond = 10000000L;

		// Token: 0x04000B3D RID: 2877
		private const double SecondsPerTick = 1E-07;

		/// <summary>Represents the number of ticks in 1 minute. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B3E RID: 2878
		public const long TicksPerMinute = 600000000L;

		// Token: 0x04000B3F RID: 2879
		private const double MinutesPerTick = 1.6666666666666667E-09;

		/// <summary>Represents the number of ticks in 1 hour. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B40 RID: 2880
		public const long TicksPerHour = 36000000000L;

		// Token: 0x04000B41 RID: 2881
		private const double HoursPerTick = 2.7777777777777777E-11;

		/// <summary>Represents the number of ticks in 1 day. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B42 RID: 2882
		public const long TicksPerDay = 864000000000L;

		// Token: 0x04000B43 RID: 2883
		private const double DaysPerTick = 1.1574074074074074E-12;

		// Token: 0x04000B44 RID: 2884
		private const int MillisPerSecond = 1000;

		// Token: 0x04000B45 RID: 2885
		private const int MillisPerMinute = 60000;

		// Token: 0x04000B46 RID: 2886
		private const int MillisPerHour = 3600000;

		// Token: 0x04000B47 RID: 2887
		private const int MillisPerDay = 86400000;

		// Token: 0x04000B48 RID: 2888
		internal const long MaxSeconds = 922337203685L;

		// Token: 0x04000B49 RID: 2889
		internal const long MinSeconds = -922337203685L;

		// Token: 0x04000B4A RID: 2890
		internal const long MaxMilliSeconds = 922337203685477L;

		// Token: 0x04000B4B RID: 2891
		internal const long MinMilliSeconds = -922337203685477L;

		// Token: 0x04000B4C RID: 2892
		internal const long TicksPerTenthSecond = 1000000L;

		/// <summary>Represents the zero <see cref="T:System.TimeSpan" /> value. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B4D RID: 2893
		public static readonly TimeSpan Zero = new TimeSpan(0L);

		/// <summary>Represents the maximum <see cref="T:System.TimeSpan" /> value. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B4E RID: 2894
		public static readonly TimeSpan MaxValue = new TimeSpan(long.MaxValue);

		/// <summary>Represents the minimum <see cref="T:System.TimeSpan" /> value. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B4F RID: 2895
		public static readonly TimeSpan MinValue = new TimeSpan(long.MinValue);

		// Token: 0x04000B50 RID: 2896
		internal long _ticks;

		// Token: 0x04000B51 RID: 2897
		private static volatile bool _legacyConfigChecked;

		// Token: 0x04000B52 RID: 2898
		private static volatile bool _legacyMode;
	}
}
