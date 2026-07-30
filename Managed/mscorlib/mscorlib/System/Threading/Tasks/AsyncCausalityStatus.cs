using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x020004B7 RID: 1207
	[FriendAccessAllowed]
	internal enum AsyncCausalityStatus
	{
		// Token: 0x04001D95 RID: 7573
		Started,
		// Token: 0x04001D96 RID: 7574
		Completed,
		// Token: 0x04001D97 RID: 7575
		Canceled,
		// Token: 0x04001D98 RID: 7576
		Error
	}
}
