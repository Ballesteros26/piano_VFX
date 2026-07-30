using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200031C RID: 796
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum BlendMode
	{
		// Token: 0x0400087B RID: 2171
		Zero,
		// Token: 0x0400087C RID: 2172
		One,
		// Token: 0x0400087D RID: 2173
		DstColor,
		// Token: 0x0400087E RID: 2174
		SrcColor,
		// Token: 0x0400087F RID: 2175
		OneMinusDstColor,
		// Token: 0x04000880 RID: 2176
		SrcAlpha,
		// Token: 0x04000881 RID: 2177
		OneMinusSrcColor,
		// Token: 0x04000882 RID: 2178
		DstAlpha,
		// Token: 0x04000883 RID: 2179
		OneMinusDstAlpha,
		// Token: 0x04000884 RID: 2180
		SrcAlphaSaturate,
		// Token: 0x04000885 RID: 2181
		OneMinusSrcAlpha
	}
}
