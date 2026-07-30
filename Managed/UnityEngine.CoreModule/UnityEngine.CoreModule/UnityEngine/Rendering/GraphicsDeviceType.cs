using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200032D RID: 813
	[UsedByNativeCode]
	public enum GraphicsDeviceType
	{
		// Token: 0x04000939 RID: 2361
		[Obsolete("OpenGL2 is no longer supported in Unity 5.5+")]
		OpenGL2,
		// Token: 0x0400093A RID: 2362
		[Obsolete("Direct3D 9 is no longer supported in Unity 2017.2+")]
		Direct3D9,
		// Token: 0x0400093B RID: 2363
		Direct3D11,
		// Token: 0x0400093C RID: 2364
		[Obsolete("PS3 is no longer supported in Unity 5.5+")]
		PlayStation3,
		// Token: 0x0400093D RID: 2365
		Null,
		// Token: 0x0400093E RID: 2366
		[Obsolete("Xbox360 is no longer supported in Unity 5.5+")]
		Xbox360 = 6,
		// Token: 0x0400093F RID: 2367
		OpenGLES2 = 8,
		// Token: 0x04000940 RID: 2368
		OpenGLES3 = 11,
		// Token: 0x04000941 RID: 2369
		[Obsolete("PVita is no longer supported as of Unity 2018")]
		PlayStationVita,
		// Token: 0x04000942 RID: 2370
		PlayStation4,
		// Token: 0x04000943 RID: 2371
		XboxOne,
		// Token: 0x04000944 RID: 2372
		[Obsolete("PlayStationMobile is no longer supported in Unity 5.3+")]
		PlayStationMobile,
		// Token: 0x04000945 RID: 2373
		Metal,
		// Token: 0x04000946 RID: 2374
		OpenGLCore,
		// Token: 0x04000947 RID: 2375
		Direct3D12,
		// Token: 0x04000948 RID: 2376
		[Obsolete("Nintendo 3DS support is unavailable since 2018.1")]
		N3DS,
		// Token: 0x04000949 RID: 2377
		Vulkan = 21,
		// Token: 0x0400094A RID: 2378
		Switch,
		// Token: 0x0400094B RID: 2379
		XboxOneD3D12
	}
}
