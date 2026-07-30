using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000091 RID: 145
	internal enum DnsQClass : ushort
	{
		// Token: 0x04000835 RID: 2101
		Internet = 1,
		// Token: 0x04000836 RID: 2102
		IN = 1,
		// Token: 0x04000837 RID: 2103
		CSNET,
		// Token: 0x04000838 RID: 2104
		CS = 2,
		// Token: 0x04000839 RID: 2105
		CHAOS,
		// Token: 0x0400083A RID: 2106
		CH = 3,
		// Token: 0x0400083B RID: 2107
		Hesiod,
		// Token: 0x0400083C RID: 2108
		HS = 4,
		// Token: 0x0400083D RID: 2109
		None = 254,
		// Token: 0x0400083E RID: 2110
		Any
	}
}
