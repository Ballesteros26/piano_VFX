using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200031E RID: 798
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum CompareFunction
	{
		// Token: 0x040008AC RID: 2220
		Disabled,
		// Token: 0x040008AD RID: 2221
		Never,
		// Token: 0x040008AE RID: 2222
		Less,
		// Token: 0x040008AF RID: 2223
		Equal,
		// Token: 0x040008B0 RID: 2224
		LessEqual,
		// Token: 0x040008B1 RID: 2225
		Greater,
		// Token: 0x040008B2 RID: 2226
		NotEqual,
		// Token: 0x040008B3 RID: 2227
		GreaterEqual,
		// Token: 0x040008B4 RID: 2228
		Always
	}
}
