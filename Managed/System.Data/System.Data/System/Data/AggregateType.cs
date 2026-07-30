using System;

namespace System.Data
{
	// Token: 0x0200004F RID: 79
	internal enum AggregateType
	{
		// Token: 0x040004DD RID: 1245
		None,
		// Token: 0x040004DE RID: 1246
		Sum = 4,
		// Token: 0x040004DF RID: 1247
		Mean,
		// Token: 0x040004E0 RID: 1248
		Min,
		// Token: 0x040004E1 RID: 1249
		Max,
		// Token: 0x040004E2 RID: 1250
		First,
		// Token: 0x040004E3 RID: 1251
		Count,
		// Token: 0x040004E4 RID: 1252
		Var,
		// Token: 0x040004E5 RID: 1253
		StDev
	}
}
