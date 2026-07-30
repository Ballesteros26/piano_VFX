using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000C7 RID: 199
	[VolumeComponentMenu("Ray Tracing/SubSurface Scattering (Preview)")]
	[Serializable]
	public sealed class SubSurfaceScattering : VolumeComponent
	{
		// Token: 0x04000757 RID: 1879
		[Tooltip("Enable ray traced sub-surface scattering.")]
		public BoolParameter rayTracing = new BoolParameter(false, false);

		// Token: 0x04000758 RID: 1880
		[Tooltip("Number of samples for sub-surface scattering.")]
		public ClampedIntParameter sampleCount = new ClampedIntParameter(1, 1, 32, false);
	}
}
