using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000158 RID: 344
	internal class GradientSkyRenderer : SkyRenderer
	{
		// Token: 0x06000A1D RID: 2589 RVA: 0x0004EF48 File Offset: 0x0004D148
		public override void Build()
		{
			HDRenderPipelineAsset defaultAsset = HDRenderPipeline.defaultAsset;
			this.m_GradientSkyMaterial = CoreUtils.CreateEngineMaterial(defaultAsset.renderPipelineResources.shaders.gradientSkyPS);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0004EF76 File Offset: 0x0004D176
		public override void Cleanup()
		{
			CoreUtils.Destroy(this.m_GradientSkyMaterial);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0004EF84 File Offset: 0x0004D184
		public override void RenderSky(BuiltinSkyParameters builtinParams, bool renderForCubemap, bool renderSunDisk)
		{
			GradientSky gradientSky = builtinParams.skySettings as GradientSky;
			this.m_GradientSkyMaterial.SetColor(this._GradientBottom, gradientSky.bottom.value);
			this.m_GradientSkyMaterial.SetColor(this._GradientMiddle, gradientSky.middle.value);
			this.m_GradientSkyMaterial.SetColor(this._GradientTop, gradientSky.top.value);
			this.m_GradientSkyMaterial.SetFloat(this._GradientDiffusion, gradientSky.gradientDiffusion.value);
			this.m_GradientSkyMaterial.SetFloat(HDShaderIDs._SkyIntensity, SkyRenderer.GetSkyIntensity(gradientSky, builtinParams.debugSettings));
			this.m_PropertyBlock.SetMatrix(HDShaderIDs._PixelCoordToViewDirWS, builtinParams.pixelCoordToViewDirMatrix);
			CoreUtils.DrawFullScreen(builtinParams.commandBuffer, this.m_GradientSkyMaterial, this.m_PropertyBlock, renderForCubemap ? 0 : 1);
		}

		// Token: 0x04000F5F RID: 3935
		private Material m_GradientSkyMaterial;

		// Token: 0x04000F60 RID: 3936
		private MaterialPropertyBlock m_PropertyBlock = new MaterialPropertyBlock();

		// Token: 0x04000F61 RID: 3937
		private readonly int _GradientBottom = Shader.PropertyToID("_GradientBottom");

		// Token: 0x04000F62 RID: 3938
		private readonly int _GradientMiddle = Shader.PropertyToID("_GradientMiddle");

		// Token: 0x04000F63 RID: 3939
		private readonly int _GradientTop = Shader.PropertyToID("_GradientTop");

		// Token: 0x04000F64 RID: 3940
		private readonly int _GradientDiffusion = Shader.PropertyToID("_GradientDiffusion");
	}
}
