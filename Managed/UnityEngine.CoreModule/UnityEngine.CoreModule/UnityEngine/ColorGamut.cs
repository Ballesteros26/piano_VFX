using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000122 RID: 290
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/ColorGamut.h")]
	public enum ColorGamut
	{
		// Token: 0x04000341 RID: 833
		sRGB,
		// Token: 0x04000342 RID: 834
		Rec709,
		// Token: 0x04000343 RID: 835
		Rec2020,
		// Token: 0x04000344 RID: 836
		DisplayP3,
		// Token: 0x04000345 RID: 837
		HDR10,
		// Token: 0x04000346 RID: 838
		DolbyHDR
	}
}
