using System;

namespace System.Globalization
{
	// Token: 0x02000416 RID: 1046
	internal enum HebrewNumberParsingState
	{
		// Token: 0x04001A4E RID: 6734
		InvalidHebrewNumber,
		// Token: 0x04001A4F RID: 6735
		NotHebrewDigit,
		// Token: 0x04001A50 RID: 6736
		FoundEndOfHebrewNumber,
		// Token: 0x04001A51 RID: 6737
		ContinueParsing
	}
}
