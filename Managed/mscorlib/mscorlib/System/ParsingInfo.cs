using System;
using System.Globalization;

namespace System
{
	// Token: 0x02000179 RID: 377
	internal struct ParsingInfo
	{
		// Token: 0x06001003 RID: 4099 RVA: 0x00045662 File Offset: 0x00043862
		internal void Init()
		{
			this.dayOfWeek = -1;
			this.timeMark = DateTimeParse.TM.NotSet;
		}

		// Token: 0x040009CD RID: 2509
		internal Calendar calendar;

		// Token: 0x040009CE RID: 2510
		internal int dayOfWeek;

		// Token: 0x040009CF RID: 2511
		internal DateTimeParse.TM timeMark;

		// Token: 0x040009D0 RID: 2512
		internal bool fUseHour12;

		// Token: 0x040009D1 RID: 2513
		internal bool fUseTwoDigitYear;

		// Token: 0x040009D2 RID: 2514
		internal bool fAllowInnerWhite;

		// Token: 0x040009D3 RID: 2515
		internal bool fAllowTrailingWhite;

		// Token: 0x040009D4 RID: 2516
		internal bool fCustomNumberParser;

		// Token: 0x040009D5 RID: 2517
		internal DateTimeParse.MatchNumberDelegate parseNumberDelegate;
	}
}
