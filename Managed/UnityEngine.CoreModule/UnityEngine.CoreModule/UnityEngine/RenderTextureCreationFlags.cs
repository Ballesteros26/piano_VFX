using System;

namespace UnityEngine
{
	// Token: 0x0200012B RID: 299
	[Flags]
	public enum RenderTextureCreationFlags
	{
		// Token: 0x040003CB RID: 971
		MipMap = 1,
		// Token: 0x040003CC RID: 972
		AutoGenerateMips = 2,
		// Token: 0x040003CD RID: 973
		SRGB = 4,
		// Token: 0x040003CE RID: 974
		EyeTexture = 8,
		// Token: 0x040003CF RID: 975
		EnableRandomWrite = 16,
		// Token: 0x040003D0 RID: 976
		CreatedFromScript = 32,
		// Token: 0x040003D1 RID: 977
		AllowVerticalFlip = 128,
		// Token: 0x040003D2 RID: 978
		NoResolvedColorSurface = 256,
		// Token: 0x040003D3 RID: 979
		DynamicallyScalable = 1024,
		// Token: 0x040003D4 RID: 980
		BindMS = 2048
	}
}
