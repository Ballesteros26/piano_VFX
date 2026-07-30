using System;
using System.Globalization;

namespace System
{
	// Token: 0x02000178 RID: 376
	internal struct DateTimeResult
	{
		// Token: 0x06000FFF RID: 4095 RVA: 0x000455E8 File Offset: 0x000437E8
		internal void Init()
		{
			this.Year = -1;
			this.Month = -1;
			this.Day = -1;
			this.fraction = -1.0;
			this.era = -1;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00045615 File Offset: 0x00043815
		internal void SetDate(int year, int month, int day)
		{
			this.Year = year;
			this.Month = month;
			this.Day = day;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0004562C File Offset: 0x0004382C
		internal void SetFailure(ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
		{
			this.failure = failure;
			this.failureMessageID = failureMessageID;
			this.failureMessageFormatArgument = failureMessageFormatArgument;
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00045643 File Offset: 0x00043843
		internal void SetFailure(ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument, string failureArgumentName)
		{
			this.failure = failure;
			this.failureMessageID = failureMessageID;
			this.failureMessageFormatArgument = failureMessageFormatArgument;
			this.failureArgumentName = failureArgumentName;
		}

		// Token: 0x040009BD RID: 2493
		internal int Year;

		// Token: 0x040009BE RID: 2494
		internal int Month;

		// Token: 0x040009BF RID: 2495
		internal int Day;

		// Token: 0x040009C0 RID: 2496
		internal int Hour;

		// Token: 0x040009C1 RID: 2497
		internal int Minute;

		// Token: 0x040009C2 RID: 2498
		internal int Second;

		// Token: 0x040009C3 RID: 2499
		internal double fraction;

		// Token: 0x040009C4 RID: 2500
		internal int era;

		// Token: 0x040009C5 RID: 2501
		internal ParseFlags flags;

		// Token: 0x040009C6 RID: 2502
		internal TimeSpan timeZoneOffset;

		// Token: 0x040009C7 RID: 2503
		internal Calendar calendar;

		// Token: 0x040009C8 RID: 2504
		internal DateTime parsedDate;

		// Token: 0x040009C9 RID: 2505
		internal ParseFailureKind failure;

		// Token: 0x040009CA RID: 2506
		internal string failureMessageID;

		// Token: 0x040009CB RID: 2507
		internal object failureMessageFormatArgument;

		// Token: 0x040009CC RID: 2508
		internal string failureArgumentName;
	}
}
