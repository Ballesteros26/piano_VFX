using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000119 RID: 281
	[VolumeComponentMenu("Ray Tracing/Settings (Preview)")]
	[Serializable]
	public sealed class RayTracingSettings : VolumeComponent
	{
		// Token: 0x04000D7B RID: 3451
		[Tooltip("Controls the bias for all real-time ray tracing effects.")]
		public ClampedFloatParameter rayBias = new ClampedFloatParameter(0.001f, 0f, 0.1f, false);
	}
}
