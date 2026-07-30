using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000B1C RID: 2844
	[FriendAccessAllowed]
	public enum EventChannel : byte
	{
		// Token: 0x040032EE RID: 13038
		None,
		// Token: 0x040032EF RID: 13039
		Admin = 16,
		// Token: 0x040032F0 RID: 13040
		Operational,
		// Token: 0x040032F1 RID: 13041
		Analytic,
		// Token: 0x040032F2 RID: 13042
		Debug
	}
}
