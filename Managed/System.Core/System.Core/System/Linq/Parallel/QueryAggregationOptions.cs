using System;

namespace System.Linq.Parallel
{
	// Token: 0x0200010F RID: 271
	[Flags]
	internal enum QueryAggregationOptions
	{
		// Token: 0x0400054D RID: 1357
		None = 0,
		// Token: 0x0400054E RID: 1358
		Associative = 1,
		// Token: 0x0400054F RID: 1359
		Commutative = 2,
		// Token: 0x04000550 RID: 1360
		AssociativeCommutative = 3
	}
}
