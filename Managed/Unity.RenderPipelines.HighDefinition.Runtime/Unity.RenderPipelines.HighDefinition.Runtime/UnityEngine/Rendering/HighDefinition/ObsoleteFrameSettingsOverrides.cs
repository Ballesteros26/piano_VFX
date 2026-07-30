using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000131 RID: 305
	[Flags]
	[Obsolete("For data migration")]
	internal enum ObsoleteFrameSettingsOverrides
	{
		// Token: 0x04000E11 RID: 3601
		Shadow = 1,
		// Token: 0x04000E12 RID: 3602
		ContactShadow = 2,
		// Token: 0x04000E13 RID: 3603
		ShadowMask = 4,
		// Token: 0x04000E14 RID: 3604
		SSR = 8,
		// Token: 0x04000E15 RID: 3605
		SSAO = 16,
		// Token: 0x04000E16 RID: 3606
		SubsurfaceScattering = 32,
		// Token: 0x04000E17 RID: 3607
		Transmission = 64,
		// Token: 0x04000E18 RID: 3608
		AtmosphericScaterring = 128,
		// Token: 0x04000E19 RID: 3609
		Volumetrics = 256,
		// Token: 0x04000E1A RID: 3610
		ReprojectionForVolumetrics = 512,
		// Token: 0x04000E1B RID: 3611
		LightLayers = 1024,
		// Token: 0x04000E1C RID: 3612
		MSAA = 2048,
		// Token: 0x04000E1D RID: 3613
		ExposureControl = 4096,
		// Token: 0x04000E1E RID: 3614
		TransparentPrepass = 8192,
		// Token: 0x04000E1F RID: 3615
		TransparentPostpass = 16384,
		// Token: 0x04000E20 RID: 3616
		MotionVectors = 32768,
		// Token: 0x04000E21 RID: 3617
		ObjectMotionVectors = 65536,
		// Token: 0x04000E22 RID: 3618
		Decals = 131072,
		// Token: 0x04000E23 RID: 3619
		RoughRefraction = 262144,
		// Token: 0x04000E24 RID: 3620
		Distortion = 524288,
		// Token: 0x04000E25 RID: 3621
		Postprocess = 1048576,
		// Token: 0x04000E26 RID: 3622
		ShaderLitMode = 2097152,
		// Token: 0x04000E27 RID: 3623
		DepthPrepassWithDeferredRendering = 4194304,
		// Token: 0x04000E28 RID: 3624
		OpaqueObjects = 16777216,
		// Token: 0x04000E29 RID: 3625
		TransparentObjects = 33554432,
		// Token: 0x04000E2A RID: 3626
		AsyncCompute = 8388608,
		// Token: 0x04000E2B RID: 3627
		LightListAsync = 134217728,
		// Token: 0x04000E2C RID: 3628
		SSRAsync = 268435456,
		// Token: 0x04000E2D RID: 3629
		SSAOAsync = 536870912,
		// Token: 0x04000E2E RID: 3630
		ContactShadowsAsync = 1073741824,
		// Token: 0x04000E2F RID: 3631
		VolumeVoxelizationsAsync = -2147483648
	}
}
