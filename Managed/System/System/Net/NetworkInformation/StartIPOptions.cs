using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000608 RID: 1544
	[Flags]
	internal enum StartIPOptions
	{
		// Token: 0x040027E1 RID: 10209
		Both = 3,
		// Token: 0x040027E2 RID: 10210
		None = 0,
		// Token: 0x040027E3 RID: 10211
		StartIPv4 = 1,
		// Token: 0x040027E4 RID: 10212
		StartIPv6 = 2
	}
}
