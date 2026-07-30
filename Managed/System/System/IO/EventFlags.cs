using System;

namespace System.IO
{
	// Token: 0x020003DB RID: 987
	[Flags]
	internal enum EventFlags : ushort
	{
		// Token: 0x04001A5E RID: 6750
		Add = 1,
		// Token: 0x04001A5F RID: 6751
		Delete = 2,
		// Token: 0x04001A60 RID: 6752
		Enable = 4,
		// Token: 0x04001A61 RID: 6753
		Disable = 8,
		// Token: 0x04001A62 RID: 6754
		OneShot = 16,
		// Token: 0x04001A63 RID: 6755
		Clear = 32,
		// Token: 0x04001A64 RID: 6756
		Receipt = 64,
		// Token: 0x04001A65 RID: 6757
		Dispatch = 128,
		// Token: 0x04001A66 RID: 6758
		Flag0 = 4096,
		// Token: 0x04001A67 RID: 6759
		Flag1 = 8192,
		// Token: 0x04001A68 RID: 6760
		SystemFlags = 61440,
		// Token: 0x04001A69 RID: 6761
		EOF = 32768,
		// Token: 0x04001A6A RID: 6762
		Error = 16384
	}
}
