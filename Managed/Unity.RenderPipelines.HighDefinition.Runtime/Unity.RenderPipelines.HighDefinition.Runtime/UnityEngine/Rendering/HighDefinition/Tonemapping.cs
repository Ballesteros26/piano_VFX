using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000E8 RID: 232
	[VolumeComponentMenu("Post-processing/Tonemapping")]
	[Serializable]
	public sealed class Tonemapping : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x00038D9A File Offset: 0x00036F9A
		public bool IsActive()
		{
			if (this.mode.value == TonemappingMode.External)
			{
				return this.ValidateLUT() && this.lutContribution.value > 0f;
			}
			return this.mode.value > TonemappingMode.None;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00038DD8 File Offset: 0x00036FD8
		public bool ValidateLUT()
		{
			HDRenderPipelineAsset currentAsset = HDRenderPipeline.currentAsset;
			if (currentAsset == null || this.lutTexture.value == null)
			{
				return false;
			}
			if (this.lutTexture.value.width != currentAsset.currentPlatformRenderPipelineSettings.postProcessSettings.lutSize)
			{
				return false;
			}
			bool flag = false;
			Texture value = this.lutTexture.value;
			if (value != null)
			{
				Texture3D texture3D;
				if ((texture3D = value as Texture3D) == null)
				{
					RenderTexture renderTexture;
					if ((renderTexture = value as RenderTexture) != null)
					{
						RenderTexture renderTexture2 = renderTexture;
						flag |= renderTexture2.dimension == TextureDimension.Tex3D && renderTexture2.width == renderTexture2.height && renderTexture2.height == renderTexture2.volumeDepth;
					}
				}
				else
				{
					Texture3D texture3D2 = texture3D;
					flag |= texture3D2.width == texture3D2.height && texture3D2.height == texture3D2.depth;
				}
			}
			return flag;
		}

		// Token: 0x040007DF RID: 2015
		[Tooltip("Specifies the tonemapping algorithm to use for the color grading process.")]
		public TonemappingModeParameter mode = new TonemappingModeParameter(TonemappingMode.None, false);

		// Token: 0x040007E0 RID: 2016
		[Tooltip("Controls the transition between the toe and the mid section of the curve. A value of 0 results in no transition and a value of 1 results in a very hard transition.")]
		public ClampedFloatParameter toeStrength = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x040007E1 RID: 2017
		[Tooltip("Controls how much of the dynamic range is in the toe. Higher values result in longer toes and therefore contain more of the dynamic range.")]
		public ClampedFloatParameter toeLength = new ClampedFloatParameter(0.5f, 0f, 1f, false);

		// Token: 0x040007E2 RID: 2018
		[Tooltip("Controls the transition between the midsection and the shoulder of the curve. A value of 0 results in no transition and a value of 1 results in a very hard transition.")]
		public ClampedFloatParameter shoulderStrength = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x040007E3 RID: 2019
		[Tooltip("Sets how many F-stops (EV) to add to the dynamic range of the curve.")]
		public MinFloatParameter shoulderLength = new MinFloatParameter(0.5f, 0f, false);

		// Token: 0x040007E4 RID: 2020
		[Tooltip("Controls how much overshoot to add to the shoulder.")]
		public ClampedFloatParameter shoulderAngle = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x040007E5 RID: 2021
		[Tooltip("Sets a gamma correction value that HDRP applies to the whole curve.")]
		public MinFloatParameter gamma = new MinFloatParameter(1f, 0.001f, false);

		// Token: 0x040007E6 RID: 2022
		[Tooltip("A custom 3D texture lookup table to apply.")]
		public TextureParameter lutTexture = new TextureParameter(null, false);

		// Token: 0x040007E7 RID: 2023
		[Tooltip("How much of the lookup texture will contribute to the color grading effect.")]
		public ClampedFloatParameter lutContribution = new ClampedFloatParameter(1f, 0f, 1f, false);
	}
}
