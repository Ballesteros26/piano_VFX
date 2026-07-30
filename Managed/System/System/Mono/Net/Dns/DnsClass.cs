using System;

namespace Mono.Net.Dns
{
	// Token: 0x0200008D RID: 141
	internal enum DnsClass : ushort
	{
		// Token: 0x04000821 RID: 2081
		Internet = 1,
		// Token: 0x04000822 RID: 2082
		IN = 1,
		// Token: 0x04000823 RID: 2083
		CSNET,
		// Token: 0x04000824 RID: 2084
		CS = 2,
		// Token: 0x04000825 RID: 2085
		CHAOS,
		// Token: 0x04000826 RID: 2086
		CH = 3,
		// Token: 0x04000827 RID: 2087
		Hesiod,
		// Token: 0x04000828 RID: 2088
		HS = 4
	}
}
