using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000046 RID: 70
	[VolumeComponentMenu("Ray Tracing/Global Illumination (Preview)")]
	[Serializable]
	public sealed class GlobalIllumination : VolumeComponent
	{
		// Token: 0x040001BC RID: 444
		[Tooltip("Enable ray traced global illumination.")]
		public BoolParameter rayTracing = new BoolParameter(false, false);

		// Token: 0x040001BD RID: 445
		[Tooltip("Defines the layers that GI should include.")]
		public LayerMaskParameter layerMask = new LayerMaskParameter(-1, false);

		// Token: 0x040001BE RID: 446
		[Tooltip("Controls the length of GI rays.")]
		public ClampedFloatParameter rayLength = new ClampedFloatParameter(10f, 0.001f, 50f, false);

		// Token: 0x040001BF RID: 447
		[Tooltip("Controls the clamp of intensity.")]
		public ClampedFloatParameter clampValue = new ClampedFloatParameter(1f, 0.001f, 10f, false);

		// Token: 0x040001C0 RID: 448
		[Tooltip("Controls which version of the effect should be used.")]
		public RayTracingModeParameter mode = new RayTracingModeParameter(RayTracingMode.Quality, false);

		// Token: 0x040001C1 RID: 449
		[Tooltip("Full Resolution")]
		public BoolParameter fullResolution = new BoolParameter(false, false);

		// Token: 0x040001C2 RID: 450
		[Tooltip("Upscale Radius")]
		public ClampedIntParameter upscaleRadius = new ClampedIntParameter(2, 2, 4, false);

		// Token: 0x040001C3 RID: 451
		[Tooltip("Number of samples for GI.")]
		public ClampedIntParameter sampleCount = new ClampedIntParameter(1, 1, 32, false);

		// Token: 0x040001C4 RID: 452
		[Tooltip("Number of bounces for GI.")]
		public ClampedIntParameter bounceCount = new ClampedIntParameter(1, 1, 31, false);

		// Token: 0x040001C5 RID: 453
		[Tooltip("Denoise the ray-traced GI.")]
		public BoolParameter denoise = new BoolParameter(false, false);

		// Token: 0x040001C6 RID: 454
		[Tooltip("Use a half resolution denoiser.")]
		public BoolParameter halfResolutionDenoiser = new BoolParameter(false, false);

		// Token: 0x040001C7 RID: 455
		[Tooltip("Controls the radius of the GI denoiser (First Pass).")]
		public ClampedFloatParameter denoiserRadius = new ClampedFloatParameter(0.6f, 0.001f, 1f, false);

		// Token: 0x040001C8 RID: 456
		[Tooltip("Enable second denoising pass.")]
		public BoolParameter secondDenoiserPass = new BoolParameter(false, false);

		// Token: 0x040001C9 RID: 457
		[Tooltip("Controls the radius of the GI denoiser (Second Pass).")]
		public ClampedFloatParameter secondDenoiserRadius = new ClampedFloatParameter(0.3f, 0.001f, 0.5f, false);
	}
}
