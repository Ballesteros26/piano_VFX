using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000023 RID: 35
	[Preserve]
	[Serializable]
	public sealed class Fog
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00004DDA File Offset: 0x00002FDA
		internal DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000053D4 File Offset: 0x000035D4
		internal bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled && RenderSettings.fog && !RuntimeUtilities.scriptableRenderPipelineActive && context.resources.shaders.deferredFog && context.resources.shaders.deferredFog.isSupported && context.camera.actualRenderingPath == RenderingPath.DeferredShading;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00005438 File Offset: 0x00003638
		internal void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.deferredFog);
			propertySheet.ClearKeywords();
			Color color = (RuntimeUtilities.isLinearColorSpace ? RenderSettings.fogColor.linear : RenderSettings.fogColor);
			propertySheet.properties.SetVector(ShaderIDs.FogColor, color);
			propertySheet.properties.SetVector(ShaderIDs.FogParams, new Vector3(RenderSettings.fogDensity, RenderSettings.fogStartDistance, RenderSettings.fogEndDistance));
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, this.excludeSkybox ? 1 : 0, false, null);
		}

		// Token: 0x04000089 RID: 137
		[Tooltip("Enables the internal deferred fog pass. Actual fog settings should be set in the Lighting panel.")]
		public bool enabled = true;

		// Token: 0x0400008A RID: 138
		[Tooltip("Mark true for the fog to ignore the skybox")]
		public bool excludeSkybox = true;
	}
}
