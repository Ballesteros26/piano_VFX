using System;

namespace System.Xml.Schema
{
	// Token: 0x020003AF RID: 943
	[Flags]
	internal enum RestrictionFlags
	{
		// Token: 0x04001961 RID: 6497
		Length = 1,
		// Token: 0x04001962 RID: 6498
		MinLength = 2,
		// Token: 0x04001963 RID: 6499
		MaxLength = 4,
		// Token: 0x04001964 RID: 6500
		Pattern = 8,
		// Token: 0x04001965 RID: 6501
		Enumeration = 16,
		// Token: 0x04001966 RID: 6502
		WhiteSpace = 32,
		// Token: 0x04001967 RID: 6503
		MaxInclusive = 64,
		// Token: 0x04001968 RID: 6504
		MaxExclusive = 128,
		// Token: 0x04001969 RID: 6505
		MinInclusive = 256,
		// Token: 0x0400196A RID: 6506
		MinExclusive = 512,
		// Token: 0x0400196B RID: 6507
		TotalDigits = 1024,
		// Token: 0x0400196C RID: 6508
		FractionDigits = 2048
	}
}
