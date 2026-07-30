using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000ED RID: 237
	[VolumeComponentMenu("Post-processing/White Balance")]
	[Serializable]
	public sealed class WhiteBalance : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x06000779 RID: 1913 RVA: 0x000390EB File Offset: 0x000372EB
		public bool IsActive()
		{
			return !Mathf.Approximately(this.temperature.value, 0f) || !Mathf.Approximately(this.tint.value, 0f);
		}

		// Token: 0x040007F4 RID: 2036
		[Tooltip("Controls the color temperature HDRP uses for white balancing.")]
		public ClampedFloatParameter temperature = new ClampedFloatParameter(0f, -100f, 100f, false);

		// Token: 0x040007F5 RID: 2037
		[Tooltip("Controls the white balance color to compensate for a green or magenta tint.")]
		public ClampedFloatParameter tint = new ClampedFloatParameter(0f, -100f, 100f, false);
	}
}
