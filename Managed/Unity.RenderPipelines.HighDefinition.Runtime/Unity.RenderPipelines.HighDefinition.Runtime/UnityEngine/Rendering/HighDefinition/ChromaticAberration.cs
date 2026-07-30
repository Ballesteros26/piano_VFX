using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000CD RID: 205
	[VolumeComponentMenu("Post-processing/Chromatic Aberration")]
	[Serializable]
	public sealed class ChromaticAberration : VolumeComponentWithQuality, IPostProcessComponent
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x00037FB8 File Offset: 0x000361B8
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x00037FF6 File Offset: 0x000361F6
		public int maxSamples
		{
			get
			{
				if (!base.UsesQualitySettings())
				{
					return this.m_MaxSamples.value;
				}
				int item = this.quality.levelAndOverride.Item1;
				return VolumeComponentWithQuality.GetPostProcessingQualitySettings().ChromaticAberrationMaxSamples[item];
			}
			set
			{
				this.m_MaxSamples.value = value;
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00038004 File Offset: 0x00036204
		public bool IsActive()
		{
			return this.intensity.value > 0f;
		}

		// Token: 0x0400076F RID: 1903
		[Tooltip("Specifies a Texture which HDRP uses to shift the hue of chromatic aberrations.")]
		public TextureParameter spectralLut = new TextureParameter(null, false);

		// Token: 0x04000770 RID: 1904
		[Tooltip("Controls the strength of the chromatic aberration effect.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x04000771 RID: 1905
		[Tooltip("Controls the maximum number of samples HDRP uses to render the effect. A lower sample number results in better performance.")]
		[SerializeField]
		[FormerlySerializedAs("maxSamples")]
		private ClampedIntParameter m_MaxSamples = new ClampedIntParameter(6, 3, 24, false);
	}
}
