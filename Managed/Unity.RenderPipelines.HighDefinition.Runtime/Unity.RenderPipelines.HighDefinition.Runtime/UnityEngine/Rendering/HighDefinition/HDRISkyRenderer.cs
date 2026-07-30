using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200015A RID: 346
	internal class HDRISkyRenderer : SkyRenderer
	{
		// Token: 0x06000A24 RID: 2596 RVA: 0x0004F2B4 File Offset: 0x0004D4B4
		public override void Build()
		{
			HDRenderPipelineAsset defaultAsset = HDRenderPipeline.defaultAsset;
			this.m_SkyHDRIMaterial = CoreUtils.CreateEngineMaterial(defaultAsset.renderPipelineResources.shaders.hdriSkyPS);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0004F2E2 File Offset: 0x0004D4E2
		public override void Cleanup()
		{
			CoreUtils.Destroy(this.m_SkyHDRIMaterial);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0004F2EF File Offset: 0x0004D4EF
		private void GetParameters(out float intensity, out float phi, out float backplatePhi, BuiltinSkyParameters builtinParams, HDRISky hdriSky)
		{
			intensity = SkyRenderer.GetSkyIntensity(hdriSky, builtinParams.debugSettings);
			phi = -0.017453292f * hdriSky.rotation.value;
			backplatePhi = phi - 0.017453292f * hdriSky.plateRotation.value;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0004F32C File Offset: 0x0004D52C
		private Vector4 GetBackplateParameters0(HDRISky hdriSky)
		{
			float num = Mathf.Abs(hdriSky.scale.value.x);
			float num2 = Mathf.Abs(hdriSky.scale.value.y);
			if (hdriSky.backplateType.value == BackplateType.Disc)
			{
				num2 = num;
			}
			return new Vector4(num, num2, hdriSky.groundLevel.value, hdriSky.projectionDistance.value);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0004F394 File Offset: 0x0004D594
		private Vector4 GetBackplateParameters1(float backplatePhi, HDRISky hdriSky)
		{
			float num = 3f;
			float num2 = hdriSky.blendAmount.value / 100f;
			switch (hdriSky.backplateType.value)
			{
			case BackplateType.Disc:
				num = 0f;
				break;
			case BackplateType.Rectangle:
				num = 1f;
				break;
			case BackplateType.Ellipse:
				num = 2f;
				break;
			case BackplateType.Infinite:
				num = 3f;
				num2 = 0f;
				break;
			}
			return new Vector4(num, num2, Mathf.Cos(backplatePhi), Mathf.Sin(backplatePhi));
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0004F414 File Offset: 0x0004D614
		private Vector4 GetBackplateParameters2(HDRISky hdriSky)
		{
			float num = -0.017453292f * hdriSky.plateTexRotation.value;
			return new Vector4(Mathf.Cos(num), Mathf.Sin(num), hdriSky.plateTexOffset.value.x, hdriSky.plateTexOffset.value.y);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0004F464 File Offset: 0x0004D664
		public override void PreRenderSky(BuiltinSkyParameters builtinParams, bool renderForCubemap, bool renderSunDisk)
		{
			HDRISky hdrisky = builtinParams.skySettings as HDRISky;
			if (!hdrisky.enableBackplate.value)
			{
				return;
			}
			int num;
			if (renderForCubemap)
			{
				num = HDRISkyRenderer.m_RenderDepthOnlyCubemapWithBackplateID;
			}
			else
			{
				num = HDRISkyRenderer.m_RenderDepthOnlyFullscreenSkyWithBackplateID;
			}
			float num2;
			float num3;
			float num4;
			this.GetParameters(out num2, out num3, out num4, builtinParams, hdrisky);
			using (new ProfilingScope(builtinParams.commandBuffer, ProfilingSampler.Get<HDProfileId>(HDProfileId.PreRenderSky)))
			{
				this.m_SkyHDRIMaterial.SetTexture(HDShaderIDs._Cubemap, hdrisky.hdriSky.value);
				this.m_SkyHDRIMaterial.SetVector(HDShaderIDs._SkyParam, new Vector4(num2, 0f, Mathf.Cos(num3), Mathf.Sin(num3)));
				this.m_SkyHDRIMaterial.SetVector(HDShaderIDs._BackplateParameters0, this.GetBackplateParameters0(hdrisky));
				this.m_PropertyBlock.SetMatrix(HDShaderIDs._PixelCoordToViewDirWS, builtinParams.pixelCoordToViewDirMatrix);
				CoreUtils.DrawFullScreen(builtinParams.commandBuffer, this.m_SkyHDRIMaterial, this.m_PropertyBlock, num);
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0004F568 File Offset: 0x0004D768
		public override void RenderSky(BuiltinSkyParameters builtinParams, bool renderForCubemap, bool renderSunDisk)
		{
			HDRISky hdrisky = builtinParams.skySettings as HDRISky;
			float num;
			float num2;
			float num3;
			this.GetParameters(out num, out num2, out num3, builtinParams, hdrisky);
			int num4;
			if (!hdrisky.enableBackplate.value)
			{
				if (renderForCubemap)
				{
					num4 = HDRISkyRenderer.m_RenderCubemapID;
				}
				else
				{
					num4 = HDRISkyRenderer.m_RenderFullscreenSkyID;
				}
			}
			else if (renderForCubemap)
			{
				num4 = HDRISkyRenderer.m_RenderCubemapWithBackplateID;
			}
			else
			{
				num4 = HDRISkyRenderer.m_RenderFullscreenSkyWithBackplateID;
			}
			this.m_SkyHDRIMaterial.SetTexture(HDShaderIDs._Cubemap, hdrisky.hdriSky.value);
			this.m_SkyHDRIMaterial.SetVector(HDShaderIDs._SkyParam, new Vector4(num, 0f, Mathf.Cos(num2), Mathf.Sin(num2)));
			this.m_SkyHDRIMaterial.SetVector(HDShaderIDs._BackplateParameters0, this.GetBackplateParameters0(hdrisky));
			this.m_SkyHDRIMaterial.SetVector(HDShaderIDs._BackplateParameters1, this.GetBackplateParameters1(num3, hdrisky));
			this.m_SkyHDRIMaterial.SetVector(HDShaderIDs._BackplateParameters2, this.GetBackplateParameters2(hdrisky));
			this.m_SkyHDRIMaterial.SetColor(HDShaderIDs._BackplateShadowTint, hdrisky.shadowTint.value);
			uint num5 = 0U;
			if (hdrisky.pointLightShadow.value)
			{
				num5 |= 4096U;
			}
			if (hdrisky.dirLightShadow.value)
			{
				num5 |= 16384U;
			}
			if (hdrisky.rectLightShadow.value)
			{
				num5 |= 8192U;
			}
			this.m_SkyHDRIMaterial.SetInt(HDShaderIDs._BackplateShadowFilter, (int)num5);
			this.m_PropertyBlock.SetMatrix(HDShaderIDs._PixelCoordToViewDirWS, builtinParams.pixelCoordToViewDirMatrix);
			CoreUtils.DrawFullScreen(builtinParams.commandBuffer, this.m_SkyHDRIMaterial, this.m_PropertyBlock, num4);
		}

		// Token: 0x04000F73 RID: 3955
		private Material m_SkyHDRIMaterial;

		// Token: 0x04000F74 RID: 3956
		private MaterialPropertyBlock m_PropertyBlock = new MaterialPropertyBlock();

		// Token: 0x04000F75 RID: 3957
		private static int m_RenderCubemapID = 0;

		// Token: 0x04000F76 RID: 3958
		private static int m_RenderFullscreenSkyID = 1;

		// Token: 0x04000F77 RID: 3959
		private static int m_RenderCubemapWithBackplateID = 2;

		// Token: 0x04000F78 RID: 3960
		private static int m_RenderFullscreenSkyWithBackplateID = 3;

		// Token: 0x04000F79 RID: 3961
		private static int m_RenderDepthOnlyCubemapWithBackplateID = 4;

		// Token: 0x04000F7A RID: 3962
		private static int m_RenderDepthOnlyFullscreenSkyWithBackplateID = 5;
	}
}
