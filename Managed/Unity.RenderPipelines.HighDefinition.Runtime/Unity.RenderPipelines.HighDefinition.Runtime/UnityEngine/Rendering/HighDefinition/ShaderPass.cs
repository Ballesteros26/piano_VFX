using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200014A RID: 330
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	internal enum ShaderPass
	{
		// Token: 0x04000F00 RID: 3840
		GBuffer,
		// Token: 0x04000F01 RID: 3841
		Forward,
		// Token: 0x04000F02 RID: 3842
		ForwardUnlit,
		// Token: 0x04000F03 RID: 3843
		DeferredLighting,
		// Token: 0x04000F04 RID: 3844
		DepthOnly,
		// Token: 0x04000F05 RID: 3845
		MotionVectors,
		// Token: 0x04000F06 RID: 3846
		Distortion,
		// Token: 0x04000F07 RID: 3847
		LightTransport,
		// Token: 0x04000F08 RID: 3848
		Shadows,
		// Token: 0x04000F09 RID: 3849
		SubsurfaceScattering,
		// Token: 0x04000F0A RID: 3850
		VolumeVoxelization,
		// Token: 0x04000F0B RID: 3851
		VolumetricLighting,
		// Token: 0x04000F0C RID: 3852
		DbufferProjector,
		// Token: 0x04000F0D RID: 3853
		DbufferMesh,
		// Token: 0x04000F0E RID: 3854
		ForwardEmissiveProjector,
		// Token: 0x04000F0F RID: 3855
		ForwardEmissiveMesh,
		// Token: 0x04000F10 RID: 3856
		Raytracing,
		// Token: 0x04000F11 RID: 3857
		RaytracingIndirect,
		// Token: 0x04000F12 RID: 3858
		RaytracingVisibility,
		// Token: 0x04000F13 RID: 3859
		RaytracingForward,
		// Token: 0x04000F14 RID: 3860
		RaytracingGBuffer,
		// Token: 0x04000F15 RID: 3861
		RaytracingSubSurface,
		// Token: 0x04000F16 RID: 3862
		PathTracing
	}
}
