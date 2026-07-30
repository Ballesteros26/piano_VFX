using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200038E RID: 910
	[Flags]
	public enum ShaderPropertyFlags
	{
		// Token: 0x04000B6B RID: 2923
		None = 0,
		// Token: 0x04000B6C RID: 2924
		HideInInspector = 1,
		// Token: 0x04000B6D RID: 2925
		PerRendererData = 2,
		// Token: 0x04000B6E RID: 2926
		NoScaleOffset = 4,
		// Token: 0x04000B6F RID: 2927
		Normal = 8,
		// Token: 0x04000B70 RID: 2928
		HDR = 16,
		// Token: 0x04000B71 RID: 2929
		Gamma = 32,
		// Token: 0x04000B72 RID: 2930
		NonModifiableTextureData = 64,
		// Token: 0x04000B73 RID: 2931
		MainTexture = 128,
		// Token: 0x04000B74 RID: 2932
		MainColor = 256
	}
}
