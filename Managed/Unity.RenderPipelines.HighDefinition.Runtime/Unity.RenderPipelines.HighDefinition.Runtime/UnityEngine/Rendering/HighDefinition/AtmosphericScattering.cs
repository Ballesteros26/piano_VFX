using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200003B RID: 59
	internal abstract class AtmosphericScattering : VolumeComponent
	{
		// Token: 0x06000197 RID: 407
		internal abstract void PushShaderParameters(HDCamera hdCamera, CommandBuffer cmd);

		// Token: 0x04000183 RID: 387
		public FogColorParameter colorMode = new FogColorParameter(FogColorMode.SkyColor, false);

		// Token: 0x04000184 RID: 388
		[Tooltip("Specifies the constant color of the fog.")]
		public ColorParameter color = new ColorParameter(Color.grey, true, false, true, false);

		// Token: 0x04000185 RID: 389
		[Tooltip("Specifies the tint of the fog.")]
		public ColorParameter tint = new ColorParameter(Color.white, true, false, true, false);

		// Token: 0x04000186 RID: 390
		[Tooltip("Controls the overall density of the fog. Acts as a global multiplier.")]
		public ClampedFloatParameter density = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x04000187 RID: 391
		[Tooltip("Sets the maximum fog distance HDRP uses when it shades the skybox or the Far Clipping Plane of the Camera.")]
		public MinFloatParameter maxFogDistance = new MinFloatParameter(5000f, 0f, false);

		// Token: 0x04000188 RID: 392
		[Tooltip("Controls the maximum mip map HDRP uses for mip fog (0 is the lowest mip and 1 is the highest mip).")]
		public ClampedFloatParameter mipFogMaxMip = new ClampedFloatParameter(0.5f, 0f, 1f, false);

		// Token: 0x04000189 RID: 393
		[Tooltip("Sets the distance at which HDRP uses the minimum mip image of the blurred sky texture as the fog color.")]
		public MinFloatParameter mipFogNear = new MinFloatParameter(0f, 0f, false);

		// Token: 0x0400018A RID: 394
		[Tooltip("Sets the distance at which HDRP uses the maximum mip image of the blurred sky texture as the fog color.")]
		public MinFloatParameter mipFogFar = new MinFloatParameter(1000f, 0f, false);
	}
}
