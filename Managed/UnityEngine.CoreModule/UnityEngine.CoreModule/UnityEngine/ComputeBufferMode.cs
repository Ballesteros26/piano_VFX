using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000DD RID: 221
	[NativeType("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum ComputeBufferMode
	{
		// Token: 0x04000269 RID: 617
		Immutable,
		// Token: 0x0400026A RID: 618
		Dynamic,
		// Token: 0x0400026B RID: 619
		Circular,
		// Token: 0x0400026C RID: 620
		StreamOut,
		// Token: 0x0400026D RID: 621
		SubUpdates
	}
}
