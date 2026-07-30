using System;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents the date and time data ranging in value from January 1, 1753 to December 31, 9999 to an accuracy of 3.33 milliseconds to be stored in or retrieved from a database. The <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure has a different underlying data structure from its corresponding .NET Framework type, <see cref="T:System.DateTime" />, which can represent any time between 12:00:00 AM 1/1/0001 and 11:59:59 PM 12/31/9999, to the accuracy of 100 nanoseconds. <see cref="T:System.Data.SqlTypes.SqlDateTime" /> actually stores the relative difference to 00:00:00 AM 1/1/1900. Therefore, a conversion from "00:00:00 AM 1/1/1900" to an integer will return 0.</summary>
	// Token: 0x020002C3 RID: 707
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlDateTime : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06001EC8 RID: 7880 RVA: 0x00094E0B File Offset: 0x0009300B
		private SqlDateTime(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_day = 0;
			this.m_time = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="value">A DateTime structure. </param>
		// Token: 0x06001EC9 RID: 7881 RVA: 0x00094E22 File Offset: 0x00093022
		public SqlDateTime(DateTime value)
		{
			this = SqlDateTime.FromDateTime(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the supplied parameters to initialize the year, month, day.</summary>
		/// <param name="year">An integer representing the year of the of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="month">An integer value representing the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="day">An integer value representing the day number of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06001ECA RID: 7882 RVA: 0x00094E30 File Offset: 0x00093030
		public SqlDateTime(int year, int month, int day)
		{
			this = new SqlDateTime(year, month, day, 0, 0, 0, 0.0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the supplied parameters to initialize the year, month, day, hour, minute, and second of the new structure.</summary>
		/// <param name="year">An integer value representing the year of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="month">An integer value representing the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="day">An integer value representing the day of the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="hour">An integer value representing the hour of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="minute">An integer value representing the minute of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="second">An integer value representing the second of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06001ECB RID: 7883 RVA: 0x00094E47 File Offset: 0x00093047
		public SqlDateTime(int year, int month, int day, int hour, int minute, int second)
		{
			this = new SqlDateTime(year, month, day, hour, minute, second, 0.0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the supplied parameters to initialize the year, month, day, hour, minute, second, and millisecond of the new structure.</summary>
		/// <param name="year">An integer value representing the year of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="month">An integer value representing the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="day">An integer value representing the day of the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="hour">An integer value representing the hour of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="minute">An integer value representing the minute of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="second">An integer value representing the second of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="millisecond">An double value representing the millisecond of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06001ECC RID: 7884 RVA: 0x00094E64 File Offset: 0x00093064
		public SqlDateTime(int year, int month, int day, int hour, int minute, int second, double millisecond)
		{
			if (year >= SqlDateTime.s_minYear && year <= SqlDateTime.s_maxYear && month >= 1 && month <= 12)
			{
				int[] array = (SqlDateTime.IsLeapYear(year) ? SqlDateTime.s_daysToMonth366 : SqlDateTime.s_daysToMonth365);
				if (day >= 1 && day <= array[month] - array[month - 1])
				{
					int num = year - 1;
					int num2 = num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1;
					num2 -= SqlDateTime.s_dayBase;
					if (num2 >= SqlDateTime.s_minDay && num2 <= SqlDateTime.s_maxDay && hour >= 0 && hour < 24 && minute >= 0 && minute < 60 && second >= 0 && second < 60 && millisecond >= 0.0 && millisecond < 1000.0)
					{
						double num3 = millisecond * SqlDateTime.s_SQLTicksPerMillisecond + 0.5;
						int num4 = hour * SqlDateTime.SQLTicksPerHour + minute * SqlDateTime.SQLTicksPerMinute + second * SqlDateTime.SQLTicksPerSecond + (int)num3;
						if (num4 > SqlDateTime.s_maxTime)
						{
							num4 = 0;
							num2++;
						}
						this = new SqlDateTime(num2, num4);
						return;
					}
				}
			}
			throw new SqlTypeException(SQLResource.InvalidDateTimeMessage);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the supplied parameters to initialize the year, month, day, hour, minute, second, and billisecond of the new structure.</summary>
		/// <param name="year">An integer value representing the year of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="month">An integer value representing the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="day">An integer value representing the day of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="hour">An integer value representing the hour of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="minute">An integer value representing the minute of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="second">An integer value representing the second of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="bilisecond">An integer value representing the bilisecond (billionth of a second) of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06001ECD RID: 7885 RVA: 0x00094FA4 File Offset: 0x000931A4
		public SqlDateTime(int year, int month, int day, int hour, int minute, int second, int bilisecond)
		{
			this = new SqlDateTime(year, month, day, hour, minute, second, (double)bilisecond / 1000.0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the supplied parameters.</summary>
		/// <param name="dayTicks">An integer value that represents the date as ticks. </param>
		/// <param name="timeTicks">An integer value that represents the time as ticks. </param>
		// Token: 0x06001ECE RID: 7886 RVA: 0x00094FD0 File Offset: 0x000931D0
		public SqlDateTime(int dayTicks, int timeTicks)
		{
			if (dayTicks < SqlDateTime.s_minDay || dayTicks > SqlDateTime.s_maxDay || timeTicks < SqlDateTime.s_minTime || timeTicks > SqlDateTime.s_maxTime)
			{
				this.m_fNotNull = false;
				throw new OverflowException(SQLResource.DateTimeOverflowMessage);
			}
			this.m_day = dayTicks;
			this.m_time = timeTicks;
			this.m_fNotNull = true;
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x00095024 File Offset: 0x00093224
		internal SqlDateTime(double dblVal)
		{
			if (dblVal < (double)SqlDateTime.s_minDay || dblVal >= (double)(SqlDateTime.s_maxDay + 1))
			{
				throw new OverflowException(SQLResource.DateTimeOverflowMessage);
			}
			int num = (int)dblVal;
			int num2 = (int)((dblVal - (double)num) * (double)SqlDateTime.s_SQLTicksPerDay);
			if (num2 < 0)
			{
				num--;
				num2 += SqlDateTime.s_SQLTicksPerDay;
			}
			else if (num2 >= SqlDateTime.s_SQLTicksPerDay)
			{
				num++;
				num2 -= SqlDateTime.s_SQLTicksPerDay;
			}
			this = new SqlDateTime(num, num2);
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure is null.</summary>
		/// <returns>true if null. Otherwise, false. </returns>
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x00095093 File Offset: 0x00093293
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x000950A0 File Offset: 0x000932A0
		private static TimeSpan ToTimeSpan(SqlDateTime value)
		{
			long num = (long)((double)value.m_time / SqlDateTime.s_SQLTicksPerMillisecond + 0.5);
			return new TimeSpan((long)value.m_day * 864000000000L + num * 10000L);
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x000950E8 File Offset: 0x000932E8
		private static DateTime ToDateTime(SqlDateTime value)
		{
			return SqlDateTime.s_SQLBaseDate.Add(SqlDateTime.ToTimeSpan(value));
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x00095108 File Offset: 0x00093308
		internal static DateTime ToDateTime(int daypart, int timepart)
		{
			if (daypart < SqlDateTime.s_minDay || daypart > SqlDateTime.s_maxDay || timepart < SqlDateTime.s_minTime || timepart > SqlDateTime.s_maxTime)
			{
				throw new OverflowException(SQLResource.DateTimeOverflowMessage);
			}
			long num = (long)daypart * 864000000000L;
			long num2 = (long)((double)timepart / SqlDateTime.s_SQLTicksPerMillisecond + 0.5) * 10000L;
			return new DateTime(SqlDateTime.s_SQLBaseDateTicks + num + num2);
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x00095178 File Offset: 0x00093378
		private static SqlDateTime FromTimeSpan(TimeSpan value)
		{
			if (value < SqlDateTime.s_minTimeSpan || value > SqlDateTime.s_maxTimeSpan)
			{
				throw new SqlTypeException(SQLResource.DateTimeOverflowMessage);
			}
			int num = value.Days;
			long num2 = value.Ticks - (long)num * 864000000000L;
			if (num2 < 0L)
			{
				num--;
				num2 += 864000000000L;
			}
			int num3 = (int)((double)num2 / 10000.0 * SqlDateTime.s_SQLTicksPerMillisecond + 0.5);
			if (num3 > SqlDateTime.s_maxTime)
			{
				num3 = 0;
				num++;
			}
			return new SqlDateTime(num, num3);
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x0009520F File Offset: 0x0009340F
		private static SqlDateTime FromDateTime(DateTime value)
		{
			if (value == DateTime.MaxValue)
			{
				return SqlDateTime.MaxValue;
			}
			return SqlDateTime.FromTimeSpan(value.Subtract(SqlDateTime.s_SQLBaseDate));
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. This property is read-only.</summary>
		/// <returns>The value of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The exception that is thrown when the Value property of a <see cref="N:System.Data.SqlTypes" /> structure is set to null.</exception>
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x00095235 File Offset: 0x00093435
		public DateTime Value
		{
			get
			{
				if (this.m_fNotNull)
				{
					return SqlDateTime.ToDateTime(this);
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Gets the number of ticks representing the date of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <returns>The number of ticks representing the date that is contained in the <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> property of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The exception that is thrown when the Value property of a <see cref="N:System.Data.SqlTypes" /> structure is set to null.</exception>
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x00095250 File Offset: 0x00093450
		public int DayTicks
		{
			get
			{
				if (this.m_fNotNull)
				{
					return this.m_day;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Gets the number of ticks representing the time of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <returns>The number of ticks representing the time of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x00095266 File Offset: 0x00093466
		public int TimeTicks
		{
			get
			{
				if (this.m_fNotNull)
				{
					return this.m_time;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Converts a <see cref="T:System.DateTime" /> structure to a <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> is equal to the combined <see cref="P:System.DateTime.Date" /> and <see cref="P:System.DateTime.TimeOfDay" /> properties of the supplied <see cref="T:System.DateTime" /> structure.</returns>
		/// <param name="value">A DateTime structure. </param>
		// Token: 0x06001ED9 RID: 7897 RVA: 0x0009527C File Offset: 0x0009347C
		public static implicit operator SqlDateTime(DateTime value)
		{
			return new SqlDateTime(value);
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to a <see cref="T:System.DateTime" /> structure.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object whose <see cref="P:System.DateTime.Date" /> and <see cref="P:System.DateTime.TimeOfDay" /> properties contain the same date and time values as the <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> property of the supplied <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		// Token: 0x06001EDA RID: 7898 RVA: 0x00095284 File Offset: 0x00093484
		public static explicit operator DateTime(SqlDateTime x)
		{
			return SqlDateTime.ToDateTime(x);
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to a <see cref="T:System.String" />.</summary>
		/// <returns>A String representing the <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> property of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001EDB RID: 7899 RVA: 0x0009528C File Offset: 0x0009348C
		public override string ToString()
		{
			if (this.IsNull)
			{
				return SQLResource.NullString;
			}
			return SqlDateTime.ToDateTime(this).ToString(null);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> representation of a date and time to its <see cref="T:System.Data.SqlTypes.SqlDateTime" /> equivalent.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure equal to the date and time represented by the specified string.</returns>
		/// <param name="s">The string to be parsed. </param>
		// Token: 0x06001EDC RID: 7900 RVA: 0x000952BC File Offset: 0x000934BC
		public static SqlDateTime Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlDateTime.Null;
			}
			DateTime dateTime;
			try
			{
				dateTime = DateTime.Parse(s, CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)CultureInfo.CurrentCulture.GetFormat(typeof(DateTimeFormatInfo));
				dateTime = DateTime.ParseExact(s, SqlDateTime.s_dateTimeFormats, dateTimeFormatInfo, DateTimeStyles.AllowWhiteSpaces);
			}
			return new SqlDateTime(dateTime);
		}

		/// <summary>Adds the period of time indicated by the supplied <see cref="T:System.TimeSpan" /> parameter, <paramref name="t" />, to the supplied <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDateTime" />. If either argument is <see cref="F:System.Data.SqlTypes.SqlDateTime.Null" />, the new <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> is <see cref="F:System.Data.SqlTypes.SqlDateTime.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="t">A <see cref="T:System.TimeSpan" /> structure. </param>
		// Token: 0x06001EDD RID: 7901 RVA: 0x0009532C File Offset: 0x0009352C
		public static SqlDateTime operator +(SqlDateTime x, TimeSpan t)
		{
			if (!x.IsNull)
			{
				return SqlDateTime.FromDateTime(SqlDateTime.ToDateTime(x) + t);
			}
			return SqlDateTime.Null;
		}

		/// <summary>Subtracts the supplied <see cref="T:System.TimeSpan" /> structure, <paramref name="t" />, from the supplied <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure representing the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="t">A <see cref="T:System.TimeSpan" /> structure. </param>
		// Token: 0x06001EDE RID: 7902 RVA: 0x0009534E File Offset: 0x0009354E
		public static SqlDateTime operator -(SqlDateTime x, TimeSpan t)
		{
			if (!x.IsNull)
			{
				return SqlDateTime.FromDateTime(SqlDateTime.ToDateTime(x) - t);
			}
			return SqlDateTime.Null;
		}

		/// <summary>Adds a <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to the specified TimeSpan.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value.</param>
		/// <param name="t">A Timespan value.</param>
		// Token: 0x06001EDF RID: 7903 RVA: 0x00095370 File Offset: 0x00093570
		public static SqlDateTime Add(SqlDateTime x, TimeSpan t)
		{
			return x + t;
		}

		/// <summary>Subtracts the specified Timespan from this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> instance.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value.</param>
		/// <param name="t">A Timespan value.</param>
		// Token: 0x06001EE0 RID: 7904 RVA: 0x00095379 File Offset: 0x00093579
		public static SqlDateTime Subtract(SqlDateTime x, TimeSpan t)
		{
			return x - t;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> is equal to the date and time represented by the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter. If the <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the Value of the newly created <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure will be null.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />.</param>
		// Token: 0x06001EE1 RID: 7905 RVA: 0x00095382 File Offset: 0x00093582
		public static explicit operator SqlDateTime(SqlString x)
		{
			if (!x.IsNull)
			{
				return SqlDateTime.Parse(x.Value);
			}
			return SqlDateTime.Null;
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x0009539F File Offset: 0x0009359F
		private static bool IsLeapYear(int year)
		{
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structures to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06001EE3 RID: 7907 RVA: 0x000953BA File Offset: 0x000935BA
		public static SqlBoolean operator ==(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day == y.m_day && x.m_time == y.m_time);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06001EE4 RID: 7908 RVA: 0x000953F8 File Offset: 0x000935F8
		public static SqlBoolean operator !=(SqlDateTime x, SqlDateTime y)
		{
			return !(x == y);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06001EE5 RID: 7909 RVA: 0x00095408 File Offset: 0x00093608
		public static SqlBoolean operator <(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day < y.m_day || (x.m_day == y.m_day && x.m_time < y.m_time));
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06001EE6 RID: 7910 RVA: 0x00095464 File Offset: 0x00093664
		public static SqlBoolean operator >(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day > y.m_day || (x.m_day == y.m_day && x.m_time > y.m_time));
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06001EE7 RID: 7911 RVA: 0x000954C0 File Offset: 0x000936C0
		public static SqlBoolean operator <=(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day < y.m_day || (x.m_day == y.m_day && x.m_time <= y.m_time));
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06001EE8 RID: 7912 RVA: 0x00095520 File Offset: 0x00093720
		public static SqlBoolean operator >=(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day > y.m_day || (x.m_day == y.m_day && x.m_time >= y.m_time));
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structures to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06001EE9 RID: 7913 RVA: 0x0009557D File Offset: 0x0009377D
		public static SqlBoolean Equals(SqlDateTime x, SqlDateTime y)
		{
			return x == y;
		}

		/// <summary>Performs a logical comparison of two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06001EEA RID: 7914 RVA: 0x00095586 File Offset: 0x00093786
		public static SqlBoolean NotEquals(SqlDateTime x, SqlDateTime y)
		{
			return x != y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06001EEB RID: 7915 RVA: 0x0009558F File Offset: 0x0009378F
		public static SqlBoolean LessThan(SqlDateTime x, SqlDateTime y)
		{
			return x < y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06001EEC RID: 7916 RVA: 0x00095598 File Offset: 0x00093798
		public static SqlBoolean GreaterThan(SqlDateTime x, SqlDateTime y)
		{
			return x > y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06001EED RID: 7917 RVA: 0x000955A1 File Offset: 0x000937A1
		public static SqlBoolean LessThanOrEqual(SqlDateTime x, SqlDateTime y)
		{
			return x <= y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06001EEE RID: 7918 RVA: 0x000955AA File Offset: 0x000937AA
		public static SqlBoolean GreaterThanOrEqual(SqlDateTime x, SqlDateTime y)
		{
			return x >= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A SqlString structure whose value is a string representing the date and time that is contained in this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001EEF RID: 7919 RVA: 0x000955B3 File Offset: 0x000937B3
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing as Visual Basic). </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001EF0 RID: 7920 RVA: 0x000955C0 File Offset: 0x000937C0
		public int CompareTo(object value)
		{
			if (value is SqlDateTime)
			{
				SqlDateTime sqlDateTime = (SqlDateTime)value;
				return this.CompareTo(sqlDateTime);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlDateTime));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to the supplied <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than <see cref="T:System.Data.SqlTypes.SqlDateTime" />. Zero This instance is the same as <see cref="T:System.Data.SqlTypes.SqlDateTime" />. Greater than zero This instance is greater than <see cref="T:System.Data.SqlTypes.SqlDateTime" />-or- <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to be compared.</param>
		// Token: 0x06001EF1 RID: 7921 RVA: 0x000955FC File Offset: 0x000937FC
		public int CompareTo(SqlDateTime value)
		{
			if (this.IsNull)
			{
				if (!value.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (value.IsNull)
				{
					return 1;
				}
				if (this < value)
				{
					return -1;
				}
				if (this > value)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> object.</summary>
		/// <returns>true if the object is an instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x06001EF2 RID: 7922 RVA: 0x00095654 File Offset: 0x00093854
		public override bool Equals(object value)
		{
			if (!(value is SqlDateTime))
			{
				return false;
			}
			SqlDateTime sqlDateTime = (SqlDateTime)value;
			if (sqlDateTime.IsNull || this.IsNull)
			{
				return sqlDateTime.IsNull && this.IsNull;
			}
			return (this == sqlDateTime).Value;
		}

		/// <summary>Gets the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001EF3 RID: 7923 RVA: 0x000956AC File Offset: 0x000938AC
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XmlSchema.</returns>
		// Token: 0x06001EF4 RID: 7924 RVA: 0x00004526 File Offset: 0x00002726
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x06001EF5 RID: 7925 RVA: 0x000956D4 File Offset: 0x000938D4
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			DateTime dateTime = XmlConvert.ToDateTime(reader.ReadElementString(), XmlDateTimeSerializationMode.RoundtripKind);
			if (dateTime.Kind != DateTimeKind.Unspecified)
			{
				throw new SqlTypeException(SQLResource.TimeZoneSpecifiedMessage);
			}
			SqlDateTime sqlDateTime = SqlDateTime.FromDateTime(dateTime);
			this.m_day = sqlDateTime.DayTicks;
			this.m_time = sqlDateTime.TimeTicks;
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter </param>
		// Token: 0x06001EF6 RID: 7926 RVA: 0x00095755 File Offset: 0x00093955
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.Value, SqlDateTime.s_ISO8601_DateTimeFormat));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06001EF7 RID: 7927 RVA: 0x00095790 File Offset: 0x00093990
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("dateTime", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x040015D0 RID: 5584
		private bool m_fNotNull;

		// Token: 0x040015D1 RID: 5585
		private int m_day;

		// Token: 0x040015D2 RID: 5586
		private int m_time;

		// Token: 0x040015D3 RID: 5587
		private static readonly double s_SQLTicksPerMillisecond = 0.3;

		/// <summary>A constant whose value is the number of ticks equivalent to one second.</summary>
		// Token: 0x040015D4 RID: 5588
		public static readonly int SQLTicksPerSecond = 300;

		/// <summary>A constant whose value is the number of ticks equivalent to one minute.</summary>
		// Token: 0x040015D5 RID: 5589
		public static readonly int SQLTicksPerMinute = SqlDateTime.SQLTicksPerSecond * 60;

		/// <summary>A constant whose value is the number of ticks equivalent to one hour.</summary>
		// Token: 0x040015D6 RID: 5590
		public static readonly int SQLTicksPerHour = SqlDateTime.SQLTicksPerMinute * 60;

		// Token: 0x040015D7 RID: 5591
		private static readonly int s_SQLTicksPerDay = SqlDateTime.SQLTicksPerHour * 24;

		// Token: 0x040015D8 RID: 5592
		private static readonly long s_ticksPerSecond = 10000000L;

		// Token: 0x040015D9 RID: 5593
		private static readonly DateTime s_SQLBaseDate = new DateTime(1900, 1, 1);

		// Token: 0x040015DA RID: 5594
		private static readonly long s_SQLBaseDateTicks = SqlDateTime.s_SQLBaseDate.Ticks;

		// Token: 0x040015DB RID: 5595
		private static readonly int s_minYear = 1753;

		// Token: 0x040015DC RID: 5596
		private static readonly int s_maxYear = 9999;

		// Token: 0x040015DD RID: 5597
		private static readonly int s_minDay = -53690;

		// Token: 0x040015DE RID: 5598
		private static readonly int s_maxDay = 2958463;

		// Token: 0x040015DF RID: 5599
		private static readonly int s_minTime = 0;

		// Token: 0x040015E0 RID: 5600
		private static readonly int s_maxTime = SqlDateTime.s_SQLTicksPerDay - 1;

		// Token: 0x040015E1 RID: 5601
		private static readonly int s_dayBase = 693595;

		// Token: 0x040015E2 RID: 5602
		private static readonly int[] s_daysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334, 365
		};

		// Token: 0x040015E3 RID: 5603
		private static readonly int[] s_daysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335, 366
		};

		// Token: 0x040015E4 RID: 5604
		private static readonly DateTime s_minDateTime = new DateTime(1753, 1, 1);

		// Token: 0x040015E5 RID: 5605
		private static readonly DateTime s_maxDateTime = DateTime.MaxValue;

		// Token: 0x040015E6 RID: 5606
		private static readonly TimeSpan s_minTimeSpan = SqlDateTime.s_minDateTime.Subtract(SqlDateTime.s_SQLBaseDate);

		// Token: 0x040015E7 RID: 5607
		private static readonly TimeSpan s_maxTimeSpan = SqlDateTime.s_maxDateTime.Subtract(SqlDateTime.s_SQLBaseDate);

		// Token: 0x040015E8 RID: 5608
		private static readonly string s_ISO8601_DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		// Token: 0x040015E9 RID: 5609
		private static readonly string[] s_dateTimeFormats = new string[] { "MMM d yyyy hh:mm:ss:ffftt", "MMM d yyyy hh:mm:ss:fff", "d MMM yyyy hh:mm:ss:ffftt", "d MMM yyyy hh:mm:ss:fff", "hh:mm:ss:ffftt", "hh:mm:ss:fff", "yyMMdd", "yyyyMMdd" };

		// Token: 0x040015EA RID: 5610
		private const DateTimeStyles x_DateTimeStyle = DateTimeStyles.AllowWhiteSpaces;

		/// <summary>Represents the minimum valid date value for a <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		// Token: 0x040015EB RID: 5611
		public static readonly SqlDateTime MinValue = new SqlDateTime(SqlDateTime.s_minDay, 0);

		/// <summary>Represents the maximum valid date value for a <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		// Token: 0x040015EC RID: 5612
		public static readonly SqlDateTime MaxValue = new SqlDateTime(SqlDateTime.s_maxDay, SqlDateTime.s_maxTime);

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		// Token: 0x040015ED RID: 5613
		public static readonly SqlDateTime Null = new SqlDateTime(true);
	}
}
