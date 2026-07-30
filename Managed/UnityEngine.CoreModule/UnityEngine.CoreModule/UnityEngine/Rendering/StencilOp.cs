using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x02000321 RID: 801
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum StencilOp
	{
		// Token: 0x040008C0 RID: 2240
		Keep,
		// Token: 0x040008C1 RID: 2241
		Zero,
		// Token: 0x040008C2 RID: 2242
		Replace,
		// Token: 0x040008C3 RID: 2243
		IncrementSaturate,
		// Token: 0x040008C4 RID: 2244
		DecrementSaturate,
		// Token: 0x040008C5 RID: 2245
		Invert,
		// Token: 0x040008C6 RID: 2246
		IncrementWrap,
		// Token: 0x040008C7 RID: 2247
		DecrementWrap
	}
}
