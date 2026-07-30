using System;

namespace System.Globalization
{
	// Token: 0x02000403 RID: 1027
	[Flags]
	internal enum DateTimeFormatFlags
	{
		// Token: 0x0400194B RID: 6475
		None = 0,
		// Token: 0x0400194C RID: 6476
		UseGenitiveMonth = 1,
		// Token: 0x0400194D RID: 6477
		UseLeapYearMonth = 2,
		// Token: 0x0400194E RID: 6478
		UseSpacesInMonthNames = 4,
		// Token: 0x0400194F RID: 6479
		UseHebrewRule = 8,
		// Token: 0x04001950 RID: 6480
		UseSpacesInDayNames = 16,
		// Token: 0x04001951 RID: 6481
		UseDigitPrefixInTokens = 32,
		// Token: 0x04001952 RID: 6482
		NotInitialized = -1
	}
}
