using System;

namespace Mono.Net.Dns
{
	// Token: 0x0200008F RID: 143
	internal enum DnsOpCode : byte
	{
		// Token: 0x0400082C RID: 2092
		Query,
		// Token: 0x0400082D RID: 2093
		[Obsolete]
		IQuery,
		// Token: 0x0400082E RID: 2094
		Status,
		// Token: 0x0400082F RID: 2095
		Notify = 4,
		// Token: 0x04000830 RID: 2096
		Update
	}
}
