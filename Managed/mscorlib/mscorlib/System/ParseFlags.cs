using System;

namespace System
{
	// Token: 0x02000177 RID: 375
	[Flags]
	internal enum ParseFlags
	{
		// Token: 0x040009AE RID: 2478
		HaveYear = 1,
		// Token: 0x040009AF RID: 2479
		HaveMonth = 2,
		// Token: 0x040009B0 RID: 2480
		HaveDay = 4,
		// Token: 0x040009B1 RID: 2481
		HaveHour = 8,
		// Token: 0x040009B2 RID: 2482
		HaveMinute = 16,
		// Token: 0x040009B3 RID: 2483
		HaveSecond = 32,
		// Token: 0x040009B4 RID: 2484
		HaveTime = 64,
		// Token: 0x040009B5 RID: 2485
		HaveDate = 128,
		// Token: 0x040009B6 RID: 2486
		TimeZoneUsed = 256,
		// Token: 0x040009B7 RID: 2487
		TimeZoneUtc = 512,
		// Token: 0x040009B8 RID: 2488
		ParsedMonthName = 1024,
		// Token: 0x040009B9 RID: 2489
		CaptureOffset = 2048,
		// Token: 0x040009BA RID: 2490
		YearDefault = 4096,
		// Token: 0x040009BB RID: 2491
		Rfc1123Pattern = 8192,
		// Token: 0x040009BC RID: 2492
		UtcSortPattern = 16384
	}
}
