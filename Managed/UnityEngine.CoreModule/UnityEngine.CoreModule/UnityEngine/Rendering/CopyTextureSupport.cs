using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000342 RID: 834
	[Flags]
	public enum CopyTextureSupport
	{
		// Token: 0x040009CA RID: 2506
		None = 0,
		// Token: 0x040009CB RID: 2507
		Basic = 1,
		// Token: 0x040009CC RID: 2508
		Copy3D = 2,
		// Token: 0x040009CD RID: 2509
		DifferentTypes = 4,
		// Token: 0x040009CE RID: 2510
		TextureToRT = 8,
		// Token: 0x040009CF RID: 2511
		RTToTexture = 16
	}
}
