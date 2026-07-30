using System;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x02000411 RID: 1041
	[Serializable]
	internal class GregorianCalendarHelper
	{
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x0600317C RID: 12668 RVA: 0x000B21DD File Offset: 0x000B03DD
		internal int MaxYear
		{
			get
			{
				return this.m_maxYear;
			}
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x000B21E8 File Offset: 0x000B03E8
		internal GregorianCalendarHelper(Calendar cal, EraInfo[] eraInfo)
		{
			this.m_Cal = cal;
			this.m_EraInfo = eraInfo;
			this.m_minDate = this.m_Cal.MinSupportedDateTime;
			this.m_maxYear = this.m_EraInfo[0].maxEraYear;
			this.m_minYear = this.m_EraInfo[0].minEraYear;
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x000B224C File Offset: 0x000B044C
		internal int GetGregorianYear(int year, int era)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Non-negative number required."));
			}
			if (era == 0)
			{
				era = this.m_Cal.CurrentEraValue;
			}
			int i = 0;
			while (i < this.m_EraInfo.Length)
			{
				if (era == this.m_EraInfo[i].era)
				{
					if (year < this.m_EraInfo[i].minEraYear || year > this.m_EraInfo[i].maxEraYear)
					{
						throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), this.m_EraInfo[i].minEraYear, this.m_EraInfo[i].maxEraYear));
					}
					return this.m_EraInfo[i].yearOffset + year;
				}
				else
				{
					i++;
				}
			}
			throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x000B2334 File Offset: 0x000B0534
		internal bool IsValidYear(int year, int era)
		{
			if (year < 0)
			{
				return false;
			}
			if (era == 0)
			{
				era = this.m_Cal.CurrentEraValue;
			}
			for (int i = 0; i < this.m_EraInfo.Length; i++)
			{
				if (era == this.m_EraInfo[i].era)
				{
					return year >= this.m_EraInfo[i].minEraYear && year <= this.m_EraInfo[i].maxEraYear;
				}
			}
			return false;
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x000B23A0 File Offset: 0x000B05A0
		internal virtual int GetDatePart(long ticks, int part)
		{
			this.CheckTicksRange(ticks);
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
			int[] array = ((num4 == 3 && (num3 != 24 || num2 == 3)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365);
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

		// Token: 0x06003181 RID: 12673 RVA: 0x000B2488 File Offset: 0x000B0688
		internal static long GetAbsoluteDate(int year, int month, int day)
		{
			if (year >= 1 && year <= 9999 && month >= 1 && month <= 12)
			{
				int[] array = ((year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365);
				if (day >= 1 && day <= array[month] - array[month - 1])
				{
					int num = year - 1;
					return (long)(num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1);
				}
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Year, Month, and Day parameters describe an un-representable DateTime."));
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x000B2513 File Offset: 0x000B0713
		internal static long DateToTicks(int year, int month, int day)
		{
			return GregorianCalendarHelper.GetAbsoluteDate(year, month, day) * 864000000000L;
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x000B2528 File Offset: 0x000B0728
		internal static long TimeToTicks(int hour, int minute, int second, int millisecond)
		{
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Hour, Minute, and Second parameters describe an un-representable DateTime."));
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 999));
			}
			return TimeSpan.TimeToTicks(hour, minute, second) + (long)millisecond * 10000L;
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x000B25B0 File Offset: 0x000B07B0
		internal void CheckTicksRange(long ticks)
		{
			if (ticks < this.m_Cal.MinSupportedDateTime.Ticks || ticks > this.m_Cal.MaxSupportedDateTime.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Specified time is not supported in this calendar. It should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive."), this.m_Cal.MinSupportedDateTime, this.m_Cal.MaxSupportedDateTime));
			}
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000B2628 File Offset: 0x000B0828
		public DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), -120000, 120000));
			}
			this.CheckTicksRange(time.Ticks);
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
			int[] array = ((num % 4 == 0 && (num % 100 != 0 || num % 400 == 0)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365);
			int num5 = array[num2] - array[num2 - 1];
			if (num3 > num5)
			{
				num3 = num5;
			}
			long num6 = GregorianCalendarHelper.DateToTicks(num, num2, num3) + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(num6, this.m_Cal.MinSupportedDateTime, this.m_Cal.MaxSupportedDateTime);
			return new DateTime(num6);
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x000B2752 File Offset: 0x000B0952
		public DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x000B275F File Offset: 0x000B095F
		public int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x000B276F File Offset: 0x000B096F
		public DayOfWeek GetDayOfWeek(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			return (DayOfWeek)((time.Ticks / 864000000000L + 1L) % 7L);
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x000B2796 File Offset: 0x000B0996
		public int GetDayOfYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 1);
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000B27A8 File Offset: 0x000B09A8
		public int GetDaysInMonth(int year, int month, int era)
		{
			year = this.GetGregorianYear(year, era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Month must be between one and twelve."));
			}
			int[] array = ((year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365);
			return array[month] - array[month - 1];
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000B2807 File Offset: 0x000B0A07
		public int GetDaysInYear(int year, int era)
		{
			year = this.GetGregorianYear(year, era);
			if (year % 4 != 0 || (year % 100 == 0 && year % 400 != 0))
			{
				return 365;
			}
			return 366;
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x000B2834 File Offset: 0x000B0A34
		public int GetEra(DateTime time)
		{
			long ticks = time.Ticks;
			for (int i = 0; i < this.m_EraInfo.Length; i++)
			{
				if (ticks >= this.m_EraInfo[i].ticks)
				{
					return this.m_EraInfo[i].era;
				}
			}
			throw new ArgumentOutOfRangeException(Environment.GetResourceString("Time value was out of era range."));
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600318D RID: 12685 RVA: 0x000B288C File Offset: 0x000B0A8C
		public int[] Eras
		{
			get
			{
				if (this.m_eras == null)
				{
					this.m_eras = new int[this.m_EraInfo.Length];
					for (int i = 0; i < this.m_EraInfo.Length; i++)
					{
						this.m_eras[i] = this.m_EraInfo[i].era;
					}
				}
				return (int[])this.m_eras.Clone();
			}
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x000B28EC File Offset: 0x000B0AEC
		public int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x000B28FC File Offset: 0x000B0AFC
		public int GetMonthsInYear(int year, int era)
		{
			year = this.GetGregorianYear(year, era);
			return 12;
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x000B290C File Offset: 0x000B0B0C
		public int GetYear(DateTime time)
		{
			long ticks = time.Ticks;
			int datePart = this.GetDatePart(ticks, 0);
			for (int i = 0; i < this.m_EraInfo.Length; i++)
			{
				if (ticks >= this.m_EraInfo[i].ticks)
				{
					return datePart - this.m_EraInfo[i].yearOffset;
				}
			}
			throw new ArgumentException(Environment.GetResourceString("No Era was supplied."));
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x000B296C File Offset: 0x000B0B6C
		public int GetYear(int year, DateTime time)
		{
			long ticks = time.Ticks;
			for (int i = 0; i < this.m_EraInfo.Length; i++)
			{
				if (ticks >= this.m_EraInfo[i].ticks)
				{
					return year - this.m_EraInfo[i].yearOffset;
				}
			}
			throw new ArgumentException(Environment.GetResourceString("No Era was supplied."));
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x000B29C4 File Offset: 0x000B0BC4
		public bool IsLeapDay(int year, int month, int day, int era)
		{
			if (day < 1 || day > this.GetDaysInMonth(year, month, era))
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, this.GetDaysInMonth(year, month, era)));
			}
			return this.IsLeapYear(year, era) && (month == 2 && day == 29);
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x000B2A2F File Offset: 0x000B0C2F
		public int GetLeapMonth(int year, int era)
		{
			year = this.GetGregorianYear(year, era);
			return 0;
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x000B2A3C File Offset: 0x000B0C3C
		public bool IsLeapMonth(int year, int month, int era)
		{
			year = this.GetGregorianYear(year, era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 12));
			}
			return false;
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x000B2A89 File Offset: 0x000B0C89
		public bool IsLeapYear(int year, int era)
		{
			year = this.GetGregorianYear(year, era);
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x000B2AB0 File Offset: 0x000B0CB0
		public DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			year = this.GetGregorianYear(year, era);
			long num = GregorianCalendarHelper.DateToTicks(year, month, day) + GregorianCalendarHelper.TimeToTicks(hour, minute, second, millisecond);
			this.CheckTicksRange(num);
			return new DateTime(num);
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x000B2AEC File Offset: 0x000B0CEC
		public virtual int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			this.CheckTicksRange(time.Ticks);
			return GregorianCalendar.GetDefaultInstance().GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x000B2B08 File Offset: 0x000B0D08
		public int ToFourDigitYear(int year, int twoDigitYearMax)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Positive number required."));
			}
			if (year < 100)
			{
				int num = year % 100;
				return (twoDigitYearMax / 100 - ((num > twoDigitYearMax % 100) ? 1 : 0)) * 100 + num;
			}
			if (year < this.m_minYear || year > this.m_maxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), this.m_minYear, this.m_maxYear));
			}
			return year;
		}

		// Token: 0x04001A15 RID: 6677
		internal const long TicksPerMillisecond = 10000L;

		// Token: 0x04001A16 RID: 6678
		internal const long TicksPerSecond = 10000000L;

		// Token: 0x04001A17 RID: 6679
		internal const long TicksPerMinute = 600000000L;

		// Token: 0x04001A18 RID: 6680
		internal const long TicksPerHour = 36000000000L;

		// Token: 0x04001A19 RID: 6681
		internal const long TicksPerDay = 864000000000L;

		// Token: 0x04001A1A RID: 6682
		internal const int MillisPerSecond = 1000;

		// Token: 0x04001A1B RID: 6683
		internal const int MillisPerMinute = 60000;

		// Token: 0x04001A1C RID: 6684
		internal const int MillisPerHour = 3600000;

		// Token: 0x04001A1D RID: 6685
		internal const int MillisPerDay = 86400000;

		// Token: 0x04001A1E RID: 6686
		internal const int DaysPerYear = 365;

		// Token: 0x04001A1F RID: 6687
		internal const int DaysPer4Years = 1461;

		// Token: 0x04001A20 RID: 6688
		internal const int DaysPer100Years = 36524;

		// Token: 0x04001A21 RID: 6689
		internal const int DaysPer400Years = 146097;

		// Token: 0x04001A22 RID: 6690
		internal const int DaysTo10000 = 3652059;

		// Token: 0x04001A23 RID: 6691
		internal const long MaxMillis = 315537897600000L;

		// Token: 0x04001A24 RID: 6692
		internal const int DatePartYear = 0;

		// Token: 0x04001A25 RID: 6693
		internal const int DatePartDayOfYear = 1;

		// Token: 0x04001A26 RID: 6694
		internal const int DatePartMonth = 2;

		// Token: 0x04001A27 RID: 6695
		internal const int DatePartDay = 3;

		// Token: 0x04001A28 RID: 6696
		internal static readonly int[] DaysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334, 365
		};

		// Token: 0x04001A29 RID: 6697
		internal static readonly int[] DaysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335, 366
		};

		// Token: 0x04001A2A RID: 6698
		[OptionalField(VersionAdded = 1)]
		internal int m_maxYear = 9999;

		// Token: 0x04001A2B RID: 6699
		[OptionalField(VersionAdded = 1)]
		internal int m_minYear;

		// Token: 0x04001A2C RID: 6700
		internal Calendar m_Cal;

		// Token: 0x04001A2D RID: 6701
		[OptionalField(VersionAdded = 1)]
		internal EraInfo[] m_EraInfo;

		// Token: 0x04001A2E RID: 6702
		[OptionalField(VersionAdded = 1)]
		internal int[] m_eras;

		// Token: 0x04001A2F RID: 6703
		[OptionalField(VersionAdded = 1)]
		internal DateTime m_minDate;
	}
}
