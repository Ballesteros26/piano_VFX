using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200012D RID: 301
	[Flags]
	[Obsolete]
	internal enum ObsoleteCaptureSettingsOverrides
	{
		// Token: 0x04000DE8 RID: 3560
		ClearColorMode = 4,
		// Token: 0x04000DE9 RID: 3561
		BackgroundColorHDR = 8,
		// Token: 0x04000DEA RID: 3562
		ClearDepth = 16,
		// Token: 0x04000DEB RID: 3563
		CullingMask = 32,
		// Token: 0x04000DEC RID: 3564
		UseOcclusionCulling = 64,
		// Token: 0x04000DED RID: 3565
		VolumeLayerMask = 128,
		// Token: 0x04000DEE RID: 3566
		VolumeAnchorOverride = 256,
		// Token: 0x04000DEF RID: 3567
		Projection = 512,
		// Token: 0x04000DF0 RID: 3568
		NearClip = 1024,
		// Token: 0x04000DF1 RID: 3569
		FarClip = 2048,
		// Token: 0x04000DF2 RID: 3570
		FieldOfview = 4096,
		// Token: 0x04000DF3 RID: 3571
		OrphographicSize = 8192,
		// Token: 0x04000DF4 RID: 3572
		RenderingPath = 16384,
		// Token: 0x04000DF5 RID: 3573
		ShadowDistance = 262144
	}
}
