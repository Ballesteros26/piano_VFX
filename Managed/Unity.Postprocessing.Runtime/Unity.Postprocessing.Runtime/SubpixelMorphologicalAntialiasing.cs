using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000032 RID: 50
	[Preserve]
	[Serializable]
	public sealed class SubpixelMorphologicalAntialiasing
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00007E64 File Offset: 0x00006064
		public bool IsSupported()
		{
			return !RuntimeUtilities.isSinglePassStereoEnabled;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00007E70 File Offset: 0x00006070
		internal void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.subpixelMorphologicalAntialiasing);
			propertySheet.properties.SetTexture("_AreaTex", context.resources.smaaLuts.area);
			propertySheet.properties.SetTexture("_SearchTex", context.resources.smaaLuts.search);
			CommandBuffer command = context.command;
			command.BeginSample("SubpixelMorphologicalAntialiasing");
			command.GetTemporaryRT(ShaderIDs.SMAA_Flip, context.width, context.height, 0, FilterMode.Bilinear, context.sourceFormat, RenderTextureReadWrite.Linear, 1, false, RenderTextureMemoryless.None, context.camera.allowDynamicResolution);
			command.GetTemporaryRT(ShaderIDs.SMAA_Flop, context.width, context.height, 0, FilterMode.Bilinear, context.sourceFormat, RenderTextureReadWrite.Linear, 1, false, RenderTextureMemoryless.None, context.camera.allowDynamicResolution);
			command.BlitFullscreenTriangle(context.source, ShaderIDs.SMAA_Flip, propertySheet, (int)this.quality, true, null);
			command.BlitFullscreenTriangle(ShaderIDs.SMAA_Flip, ShaderIDs.SMAA_Flop, propertySheet, (int)(3 + this.quality), false, null);
			command.SetGlobalTexture("_BlendTex", ShaderIDs.SMAA_Flop);
			command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 6, false, null);
			command.ReleaseTemporaryRT(ShaderIDs.SMAA_Flip);
			command.ReleaseTemporaryRT(ShaderIDs.SMAA_Flop);
			command.EndSample("SubpixelMorphologicalAntialiasing");
		}

		// Token: 0x040000C1 RID: 193
		[Tooltip("Lower quality is faster at the expense of visual quality (Low = ~60%, Medium = ~80%).")]
		public SubpixelMorphologicalAntialiasing.Quality quality = SubpixelMorphologicalAntialiasing.Quality.High;

		// Token: 0x02000073 RID: 115
		private enum Pass
		{
			// Token: 0x0400028E RID: 654
			EdgeDetection,
			// Token: 0x0400028F RID: 655
			BlendWeights = 3,
			// Token: 0x04000290 RID: 656
			NeighborhoodBlending = 6
		}

		// Token: 0x02000074 RID: 116
		public enum Quality
		{
			// Token: 0x04000292 RID: 658
			Low,
			// Token: 0x04000293 RID: 659
			Medium,
			// Token: 0x04000294 RID: 660
			High
		}
	}
}
