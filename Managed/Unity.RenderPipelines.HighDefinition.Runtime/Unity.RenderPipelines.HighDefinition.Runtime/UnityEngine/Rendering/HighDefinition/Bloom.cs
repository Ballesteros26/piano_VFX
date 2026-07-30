using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000CA RID: 202
	[VolumeComponentMenu("Post-processing/Bloom")]
	[Serializable]
	public sealed class Bloom : VolumeComponentWithQuality, IPostProcessComponent
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x00037C80 File Offset: 0x00035E80
		// (set) Token: 0x06000739 RID: 1849 RVA: 0x00037CBE File Offset: 0x00035EBE
		public BloomResolution resolution
		{
			get
			{
				if (!base.UsesQualitySettings())
				{
					return this.m_Resolution.value;
				}
				int item = this.quality.levelAndOverride.Item1;
				return VolumeComponentWithQuality.GetPostProcessingQualitySettings().BloomRes[item];
			}
			set
			{
				this.m_Resolution.value = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00037CCC File Offset: 0x00035ECC
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x00037D0A File Offset: 0x00035F0A
		public bool highQualityFiltering
		{
			get
			{
				if (!base.UsesQualitySettings())
				{
					return this.m_HighQualityFiltering.value;
				}
				int item = this.quality.levelAndOverride.Item1;
				return VolumeComponentWithQuality.GetPostProcessingQualitySettings().BloomHighQualityFiltering[item];
			}
			set
			{
				this.m_HighQualityFiltering.value = value;
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00037D18 File Offset: 0x00035F18
		public bool IsActive()
		{
			return this.intensity.value > 0f;
		}

		// Token: 0x0400075C RID: 1884
		[Tooltip("Set the level of brightness to filter out pixels under this level. This value is expressed in gamma-space. A value above 0 will disregard energy conservation rules.")]
		public MinFloatParameter threshold = new MinFloatParameter(0f, 0f, false);

		// Token: 0x0400075D RID: 1885
		[Tooltip("Controls the strength of the bloom filter.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x0400075E RID: 1886
		[Tooltip("Controls the extent of the veiling effect.")]
		public ClampedFloatParameter scatter = new ClampedFloatParameter(0.7f, 0f, 1f, false);

		// Token: 0x0400075F RID: 1887
		[Tooltip("Specifies the tint of the bloom filter.")]
		public ColorParameter tint = new ColorParameter(Color.white, false, false, true, false);

		// Token: 0x04000760 RID: 1888
		[Tooltip("Specifies a Texture to add smudges or dust to the bloom effect.")]
		public TextureParameter dirtTexture = new TextureParameter(null, false);

		// Token: 0x04000761 RID: 1889
		[Tooltip("Controls the strength of the lens dirt.")]
		public MinFloatParameter dirtIntensity = new MinFloatParameter(0f, 0f, false);

		// Token: 0x04000762 RID: 1890
		[Tooltip("When enabled, bloom stretches horizontally depending on the current physical Camera's Anamorphism property value.")]
		public BoolParameter anamorphic = new BoolParameter(true, false);

		// Token: 0x04000763 RID: 1891
		[Tooltip("Specifies the resolution at which HDRP processes the effect. Quarter resolution is less resource intensive but can result in aliasing artifacts.")]
		[SerializeField]
		[FormerlySerializedAs("resolution")]
		private BloomResolutionParameter m_Resolution = new BloomResolutionParameter(BloomResolution.Half, false);

		// Token: 0x04000764 RID: 1892
		[Tooltip("When enabled, bloom uses bicubic sampling instead of bilinear sampling for the upsampling passes.")]
		[SerializeField]
		[FormerlySerializedAs("highQualityFiltering")]
		private BoolParameter m_HighQualityFiltering = new BoolParameter(true, false);
	}
}
