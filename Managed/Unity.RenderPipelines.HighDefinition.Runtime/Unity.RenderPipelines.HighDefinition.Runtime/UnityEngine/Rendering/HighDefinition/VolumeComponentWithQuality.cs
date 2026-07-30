using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000182 RID: 386
	public abstract class VolumeComponentWithQuality : VolumeComponent
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x00054778 File Offset: 0x00052978
		internal static GlobalPostProcessingQualitySettings GetPostProcessingQualitySettings()
		{
			HDRenderPipeline hdrenderPipeline = (HDRenderPipeline)RenderPipelineManager.currentPipeline;
			if (hdrenderPipeline != null)
			{
				return hdrenderPipeline.currentPlatformRenderPipelineSettings.postProcessQualitySettings;
			}
			return null;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x000547A0 File Offset: 0x000529A0
		internal static GlobalLightingQualitySettings GetLightingQualitySettings()
		{
			HDRenderPipeline hdrenderPipeline = (HDRenderPipeline)RenderPipelineManager.currentPipeline;
			if (hdrenderPipeline != null)
			{
				return hdrenderPipeline.currentPlatformRenderPipelineSettings.lightingQualitySettings;
			}
			return null;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x000547C8 File Offset: 0x000529C8
		protected bool UsesQualitySettings()
		{
			return !this.quality.levelAndOverride.Item2 && (HDRenderPipeline)RenderPipelineManager.currentPipeline != null;
		}

		// Token: 0x04001068 RID: 4200
		[Tooltip("Specifies the quality level to be used for performance relevant parameters.")]
		public ScalableSettingLevelParameter quality = new ScalableSettingLevelParameter(1, false, false);
	}
}
