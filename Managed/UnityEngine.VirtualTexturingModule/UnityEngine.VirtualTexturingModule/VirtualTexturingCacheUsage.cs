using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x02000007 RID: 7
	[NativeHeader("Modules/VirtualTexturing/Public/VirtualTexturingSettings.h")]
	[UsedByNativeCode]
	public enum VirtualTexturingCacheUsage
	{
		// Token: 0x0400000B RID: 11
		Any,
		// Token: 0x0400000C RID: 12
		Streaming,
		// Token: 0x0400000D RID: 13
		Procedural
	}
}
