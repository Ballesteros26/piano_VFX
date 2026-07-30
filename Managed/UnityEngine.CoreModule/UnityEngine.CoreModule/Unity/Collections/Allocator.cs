using System;
using UnityEngine.Scripting;

namespace Unity.Collections
{
	// Token: 0x02000058 RID: 88
	[UsedByNativeCode]
	public enum Allocator
	{
		// Token: 0x04000105 RID: 261
		Invalid,
		// Token: 0x04000106 RID: 262
		None,
		// Token: 0x04000107 RID: 263
		Temp,
		// Token: 0x04000108 RID: 264
		TempJob,
		// Token: 0x04000109 RID: 265
		Persistent,
		// Token: 0x0400010A RID: 266
		AudioKernel
	}
}
