using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200010C RID: 268
	[VolumeComponentMenu("Ray Tracing/Path Tracing (Preview)")]
	[Serializable]
	public sealed class PathTracing : VolumeComponent
	{
		// Token: 0x04000D11 RID: 3345
		[Tooltip("Enables path tracing (thus disabling most other passes).")]
		public BoolParameter enable = new BoolParameter(false, false);

		// Token: 0x04000D12 RID: 3346
		[Tooltip("Defines the layers that path tracing should include.")]
		public LayerMaskParameter layerMask = new LayerMaskParameter(-1, false);

		// Token: 0x04000D13 RID: 3347
		[Tooltip("Defines the maximum number of paths cast within each pixel, over time (one per frame).")]
		public ClampedIntParameter maximumSamples = new ClampedIntParameter(256, 1, 4096, false);

		// Token: 0x04000D14 RID: 3348
		[Tooltip("Defines the minimum number of bounces for each path.")]
		public ClampedIntParameter minimumDepth = new ClampedIntParameter(1, 1, 10, false);

		// Token: 0x04000D15 RID: 3349
		[Tooltip("Defines the maximum number of bounces for each path.")]
		public ClampedIntParameter maximumDepth = new ClampedIntParameter(4, 1, 10, false);

		// Token: 0x04000D16 RID: 3350
		[Tooltip("Defines the maximum intensity value computed for a path segment.")]
		public ClampedFloatParameter maximumIntensity = new ClampedFloatParameter(10f, 0f, 100f, false);
	}
}
