using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200011A RID: 282
	[VolumeComponentMenu("Ray Tracing/Recursive Rendering (Preview)")]
	[Serializable]
	public sealed class RecursiveRendering : VolumeComponent
	{
		// Token: 0x04000D7C RID: 3452
		[Tooltip("Enable. Enables recursive rendering.")]
		public BoolParameter enable = new BoolParameter(false, false);

		// Token: 0x04000D7D RID: 3453
		[Tooltip("Layer Mask. Layer mask used to include the objects for recursive rendering.")]
		public LayerMaskParameter layerMask = new LayerMaskParameter(-1, false);

		// Token: 0x04000D7E RID: 3454
		[Tooltip("Max Depth. Defines the maximal recursion for rays.")]
		public ClampedIntParameter maxDepth = new ClampedIntParameter(4, 1, 10, false);

		// Token: 0x04000D7F RID: 3455
		[Tooltip("Ray Length. This defines the maximal travel distance of rays.")]
		public ClampedFloatParameter rayLength = new ClampedFloatParameter(10f, 0f, 50f, false);
	}
}
