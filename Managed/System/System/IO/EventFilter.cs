using System;

namespace System.IO
{
	// Token: 0x020003DC RID: 988
	internal enum EventFilter : short
	{
		// Token: 0x04001A6C RID: 6764
		Read = -1,
		// Token: 0x04001A6D RID: 6765
		Write = -2,
		// Token: 0x04001A6E RID: 6766
		Aio = -3,
		// Token: 0x04001A6F RID: 6767
		Vnode = -4,
		// Token: 0x04001A70 RID: 6768
		Proc = -5,
		// Token: 0x04001A71 RID: 6769
		Signal = -6,
		// Token: 0x04001A72 RID: 6770
		Timer = -7,
		// Token: 0x04001A73 RID: 6771
		MachPort = -8,
		// Token: 0x04001A74 RID: 6772
		FS = -9,
		// Token: 0x04001A75 RID: 6773
		User = -10,
		// Token: 0x04001A76 RID: 6774
		VM = -11
	}
}
