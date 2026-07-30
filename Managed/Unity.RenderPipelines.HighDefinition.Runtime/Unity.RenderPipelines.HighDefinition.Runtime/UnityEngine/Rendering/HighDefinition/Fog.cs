using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200003E RID: 62
	[VolumeComponentMenu("Fog/Fog")]
	[Serializable]
	public class Fog : VolumeComponent
	{
		// Token: 0x0600019C RID: 412 RVA: 0x0000AF38 File Offset: 0x00009138
		internal static bool IsFogEnabled(HDCamera hdCamera)
		{
			return hdCamera.frameSettings.IsEnabled(FrameSettingsField.AtmosphericScattering) && hdCamera.volumeStack.GetComponent<Fog>().enabled.value;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000AF70 File Offset: 0x00009170
		internal static bool IsVolumetricFogEnabled(HDCamera hdCamera)
		{
			bool value = hdCamera.volumeStack.GetComponent<Fog>().enableVolumetricFog.value;
			bool flag = hdCamera.frameSettings.IsEnabled(FrameSettingsField.Volumetrics);
			bool flag2 = CoreUtils.IsSceneViewFogEnabled(hdCamera.camera);
			return value && flag && flag2;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000AFB3 File Offset: 0x000091B3
		internal static bool IsPBRFogEnabled(HDCamera hdCamera)
		{
			hdCamera.volumeStack.GetComponent<VisualEnvironment>();
			return false;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000AFC2 File Offset: 0x000091C2
		private static float ScaleHeightFromLayerDepth(float d)
		{
			return d * 0.144765f;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000AFCC File Offset: 0x000091CC
		internal static void PushNeutralShaderParameters(CommandBuffer cmd)
		{
			cmd.SetGlobalInt(HDShaderIDs._FogEnabled, 0);
			cmd.SetGlobalInt(HDShaderIDs._EnableVolumetricFog, 0);
			cmd.SetGlobalVector(HDShaderIDs._HeightFogBaseScattering, Vector3.zero);
			cmd.SetGlobalFloat(HDShaderIDs._HeightFogBaseExtinction, 0f);
			cmd.SetGlobalVector(HDShaderIDs._HeightFogExponents, Vector2.one);
			cmd.SetGlobalFloat(HDShaderIDs._HeightFogBaseHeight, 0f);
			cmd.SetGlobalFloat(HDShaderIDs._GlobalFogAnisotropy, 0f);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000B04C File Offset: 0x0000924C
		internal static void PushFogShaderParameters(HDCamera hdCamera, CommandBuffer cmd)
		{
			Fog component = hdCamera.volumeStack.GetComponent<Fog>();
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.AtmosphericScattering) || !component.enabled.value)
			{
				Fog.PushNeutralShaderParameters(cmd);
				return;
			}
			component.PushShaderParameters(hdCamera, cmd);
			cmd.SetGlobalInt(HDShaderIDs._PBRFogEnabled, Fog.IsPBRFogEnabled(hdCamera) ? 1 : 0);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000B0AC File Offset: 0x000092AC
		internal virtual void PushShaderParameters(HDCamera hdCamera, CommandBuffer cmd)
		{
			cmd.SetGlobalInt(HDShaderIDs._FogEnabled, 1);
			cmd.SetGlobalFloat(HDShaderIDs._MaxFogDistance, this.maxFogDistance.value);
			Color color = ((this.colorMode.value == FogColorMode.ConstantColor) ? this.color.value : this.tint.value);
			cmd.SetGlobalFloat(Fog.m_ColorModeParam, (float)this.colorMode.value);
			cmd.SetGlobalColor(Fog.m_FogColorParam, new Color(color.r, color.g, color.b, 0f));
			cmd.SetGlobalVector(Fog.m_MipFogParam, new Vector4(this.mipFogNear.value, this.mipFogFar.value, this.mipFogMaxMip.value, 0f));
			DensityVolumeArtistParameters densityVolumeArtistParameters = new DensityVolumeArtistParameters(this.albedo.value, this.meanFreePath.value, this.anisotropy.value);
			DensityVolumeEngineData densityVolumeEngineData = densityVolumeArtistParameters.ConvertToEngineData();
			cmd.SetGlobalVector(HDShaderIDs._HeightFogBaseScattering, densityVolumeEngineData.scattering);
			cmd.SetGlobalFloat(HDShaderIDs._HeightFogBaseExtinction, densityVolumeEngineData.extinction);
			float num = this.baseHeight.value;
			if (ShaderConfig.s_CameraRelativeRendering != 0)
			{
				num -= hdCamera.camera.transform.position.y;
			}
			float num2 = Fog.ScaleHeightFromLayerDepth(Mathf.Max(0.01f, this.maximumHeight.value - this.baseHeight.value));
			cmd.SetGlobalVector(HDShaderIDs._HeightFogExponents, new Vector2(1f / num2, num2));
			cmd.SetGlobalFloat(HDShaderIDs._HeightFogBaseHeight, num);
			bool flag = this.enableVolumetricFog.value && hdCamera.frameSettings.IsEnabled(FrameSettingsField.Volumetrics);
			cmd.SetGlobalFloat(HDShaderIDs._GlobalFogAnisotropy, this.anisotropy.value);
			cmd.SetGlobalInt(HDShaderIDs._EnableVolumetricFog, flag ? 1 : 0);
		}

		// Token: 0x04000193 RID: 403
		private static readonly int m_ColorModeParam = Shader.PropertyToID("_FogColorMode");

		// Token: 0x04000194 RID: 404
		private static readonly int m_FogColorParam = Shader.PropertyToID("_FogColor");

		// Token: 0x04000195 RID: 405
		private static readonly int m_MipFogParam = Shader.PropertyToID("_MipFogParameters");

		// Token: 0x04000196 RID: 406
		[Tooltip("Enables the fog.")]
		public BoolParameter enabled = new BoolParameter(false, false);

		// Token: 0x04000197 RID: 407
		public FogColorParameter colorMode = new FogColorParameter(FogColorMode.SkyColor, false);

		// Token: 0x04000198 RID: 408
		[Tooltip("Specifies the constant color of the fog.")]
		public ColorParameter color = new ColorParameter(Color.grey, true, false, true, false);

		// Token: 0x04000199 RID: 409
		[Tooltip("Specifies the tint of the fog.")]
		public ColorParameter tint = new ColorParameter(Color.white, true, false, true, false);

		// Token: 0x0400019A RID: 410
		[Tooltip("Sets the maximum fog distance HDRP uses when it shades the skybox or the Far Clipping Plane of the Camera.")]
		public MinFloatParameter maxFogDistance = new MinFloatParameter(5000f, 0f, false);

		// Token: 0x0400019B RID: 411
		[Tooltip("Controls the maximum mip map HDRP uses for mip fog (0 is the lowest mip and 1 is the highest mip).")]
		public ClampedFloatParameter mipFogMaxMip = new ClampedFloatParameter(0.5f, 0f, 1f, false);

		// Token: 0x0400019C RID: 412
		[Tooltip("Sets the distance at which HDRP uses the minimum mip image of the blurred sky texture as the fog color.")]
		public MinFloatParameter mipFogNear = new MinFloatParameter(0f, 0f, false);

		// Token: 0x0400019D RID: 413
		[Tooltip("Sets the distance at which HDRP uses the maximum mip image of the blurred sky texture as the fog color.")]
		public MinFloatParameter mipFogFar = new MinFloatParameter(1000f, 0f, false);

		// Token: 0x0400019E RID: 414
		public FloatParameter baseHeight = new FloatParameter(0f, false);

		// Token: 0x0400019F RID: 415
		public FloatParameter maximumHeight = new FloatParameter(50f, false);

		// Token: 0x040001A0 RID: 416
		public ColorParameter albedo = new ColorParameter(Color.white, false);

		// Token: 0x040001A1 RID: 417
		public MinFloatParameter meanFreePath = new MinFloatParameter(400f, 1f, false);

		// Token: 0x040001A2 RID: 418
		public BoolParameter enableVolumetricFog = new BoolParameter(false, false);

		// Token: 0x040001A3 RID: 419
		public ClampedFloatParameter anisotropy = new ClampedFloatParameter(0f, -1f, 1f, false);

		// Token: 0x040001A4 RID: 420
		public ClampedFloatParameter globalLightProbeDimmer = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x040001A5 RID: 421
		[Tooltip("Sets the distance (in meters) from the Camera's Near Clipping Plane to the back of the Camera's volumetric lighting buffer.")]
		public MinFloatParameter depthExtent = new MinFloatParameter(64f, 0.1f, false);

		// Token: 0x040001A6 RID: 422
		[Tooltip("Controls the distribution of slices along the Camera's focal axis. 0 is exponential distribution and 1 is linear distribution.")]
		public ClampedFloatParameter sliceDistributionUniformity = new ClampedFloatParameter(0.75f, 0f, 1f, false);

		// Token: 0x040001A7 RID: 423
		[Tooltip("Applies a blur to smoothen the volumetric lighting output.")]
		public BoolParameter filter = new BoolParameter(false, false);
	}
}
