using System;

namespace System.Web.Util
{
	// Token: 0x02000111 RID: 273
	internal sealed class DateTimeUtil
	{
		// Token: 0x06000DDE RID: 3550 RVA: 0x00002050 File Offset: 0x00000250
		private DateTimeUtil()
		{
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x000260A8 File Offset: 0x000242A8
		internal static DateTime FromFileTimeToUtc(long filetime)
		{
			return new DateTime(filetime + 504911232000000000L, DateTimeKind.Utc);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x000260BB File Offset: 0x000242BB
		internal static DateTime ConvertToUniversalTime(DateTime localTime)
		{
			if (localTime < DateTimeUtil.MinValuePlusOneDay)
			{
				return DateTime.MinValue;
			}
			if (localTime > DateTimeUtil.MaxValueMinusOneDay)
			{
				return DateTime.MaxValue;
			}
			return localTime.ToUniversalTime();
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x000260EA File Offset: 0x000242EA
		internal static DateTime ConvertToLocalTime(DateTime utcTime)
		{
			if (utcTime < DateTimeUtil.MinValuePlusOneDay)
			{
				return DateTime.MinValue;
			}
			if (utcTime > DateTimeUtil.MaxValueMinusOneDay)
			{
				return DateTime.MaxValue;
			}
			return utcTime.ToLocalTime();
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0002611C File Offset: 0x0002431C
		internal static TimeSpan GetTimeoutFromTimeUnit(int timeoutValue, TimeUnit timeoutUnit)
		{
			switch (timeoutUnit)
			{
			case TimeUnit.Days:
				return new TimeSpan(timeoutValue, 0, 0, 0);
			case TimeUnit.Hours:
				return new TimeSpan(timeoutValue, 0, 0);
			case TimeUnit.Minutes:
				return new TimeSpan(0, timeoutValue, 0);
			case TimeUnit.Seconds:
				return new TimeSpan(0, 0, timeoutValue);
			case TimeUnit.Milliseconds:
				return new TimeSpan(0, 0, 0, 0, timeoutValue);
			}
			throw new ArgumentException(global::SR.GetString("Invalid value for '{0}' parameter.", new object[] { "timeoutUnit" }));
		}

		// Token: 0x040011A7 RID: 4519
		private const long FileTimeOffset = 504911232000000000L;

		// Token: 0x040011A8 RID: 4520
		private static readonly DateTime MinValuePlusOneDay = DateTime.MinValue.AddDays(1.0);

		// Token: 0x040011A9 RID: 4521
		private static readonly DateTime MaxValueMinusOneDay = DateTime.MaxValue.AddDays(-1.0);
	}
}
