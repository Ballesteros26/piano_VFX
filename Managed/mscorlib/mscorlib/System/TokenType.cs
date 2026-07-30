using System;

namespace System
{
	// Token: 0x0200017A RID: 378
	internal enum TokenType
	{
		// Token: 0x040009D7 RID: 2519
		NumberToken = 1,
		// Token: 0x040009D8 RID: 2520
		YearNumberToken,
		// Token: 0x040009D9 RID: 2521
		Am,
		// Token: 0x040009DA RID: 2522
		Pm,
		// Token: 0x040009DB RID: 2523
		MonthToken,
		// Token: 0x040009DC RID: 2524
		EndOfString,
		// Token: 0x040009DD RID: 2525
		DayOfWeekToken,
		// Token: 0x040009DE RID: 2526
		TimeZoneToken,
		// Token: 0x040009DF RID: 2527
		EraToken,
		// Token: 0x040009E0 RID: 2528
		DateWordToken,
		// Token: 0x040009E1 RID: 2529
		UnknownToken,
		// Token: 0x040009E2 RID: 2530
		HebrewNumber,
		// Token: 0x040009E3 RID: 2531
		JapaneseEraToken,
		// Token: 0x040009E4 RID: 2532
		TEraToken,
		// Token: 0x040009E5 RID: 2533
		IgnorableSymbol,
		// Token: 0x040009E6 RID: 2534
		SEP_Unk = 256,
		// Token: 0x040009E7 RID: 2535
		SEP_End = 512,
		// Token: 0x040009E8 RID: 2536
		SEP_Space = 768,
		// Token: 0x040009E9 RID: 2537
		SEP_Am = 1024,
		// Token: 0x040009EA RID: 2538
		SEP_Pm = 1280,
		// Token: 0x040009EB RID: 2539
		SEP_Date = 1536,
		// Token: 0x040009EC RID: 2540
		SEP_Time = 1792,
		// Token: 0x040009ED RID: 2541
		SEP_YearSuff = 2048,
		// Token: 0x040009EE RID: 2542
		SEP_MonthSuff = 2304,
		// Token: 0x040009EF RID: 2543
		SEP_DaySuff = 2560,
		// Token: 0x040009F0 RID: 2544
		SEP_HourSuff = 2816,
		// Token: 0x040009F1 RID: 2545
		SEP_MinuteSuff = 3072,
		// Token: 0x040009F2 RID: 2546
		SEP_SecondSuff = 3328,
		// Token: 0x040009F3 RID: 2547
		SEP_LocalTimeMark = 3584,
		// Token: 0x040009F4 RID: 2548
		SEP_DateOrOffset = 3840,
		// Token: 0x040009F5 RID: 2549
		RegularTokenMask = 255,
		// Token: 0x040009F6 RID: 2550
		SeparatorTokenMask = 65280
	}
}
