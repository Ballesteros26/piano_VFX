using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200015C RID: 348
	internal class PhysicallyBasedSkyRenderer : SkyRenderer
	{
		// Token: 0x06000A3E RID: 2622 RVA: 0x0004FF24 File Offset: 0x0004E124
		private RTHandle AllocateGroundIrradianceTable(int index)
		{
			return RTHandles.Alloc(256, 1, 1, DepthBits.None, PhysicallyBasedSkyRenderer.s_ColorFormat, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex2D, true, false, true, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, string.Format("GroundIrradianceTable{0}", index));
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0004FF64 File Offset: 0x0004E164
		private RTHandle AllocateInScatteredRadianceTable(int index)
		{
			return RTHandles.Alloc(128, 32, 1024, DepthBits.None, PhysicallyBasedSkyRenderer.s_ColorFormat, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex3D, true, false, true, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, string.Format("InScatteredRadianceTable{0}", index));
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0004FFB4 File Offset: 0x0004E1B4
		public override void Build()
		{
			HDRenderPipelineAsset currentAsset = HDRenderPipeline.currentAsset;
			RenderPipelineResources renderPipelineResources = HDRenderPipeline.defaultAsset.renderPipelineResources;
			PhysicallyBasedSkyRenderer.s_GroundIrradiancePrecomputationCS = renderPipelineResources.shaders.groundIrradiancePrecomputationCS;
			PhysicallyBasedSkyRenderer.s_InScatteredRadiancePrecomputationCS = renderPipelineResources.shaders.inScatteredRadiancePrecomputationCS;
			PhysicallyBasedSkyRenderer.s_PbrSkyMaterial = CoreUtils.CreateEngineMaterial(renderPipelineResources.shaders.physicallyBasedSkyPS);
			PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties = new MaterialPropertyBlock();
			this.m_GroundIrradianceTables = new RTHandle[2];
			this.m_GroundIrradianceTables[0] = this.AllocateGroundIrradianceTable(0);
			this.m_InScatteredRadianceTables = new RTHandle[5];
			this.m_InScatteredRadianceTables[0] = this.AllocateInScatteredRadianceTable(0);
			this.m_InScatteredRadianceTables[1] = this.AllocateInScatteredRadianceTable(1);
			this.m_InScatteredRadianceTables[2] = this.AllocateInScatteredRadianceTable(2);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00050064 File Offset: 0x0004E264
		public override void SetGlobalSkyData(CommandBuffer cmd, BuiltinSkyParameters builtinParams)
		{
			this.UpdateGlobalConstantBuffer(cmd, builtinParams);
			if (this.m_LastPrecomputedBounce > 0)
			{
				cmd.SetGlobalTexture(HDShaderIDs._AirSingleScatteringTexture, this.m_InScatteredRadianceTables[0]);
				cmd.SetGlobalTexture(HDShaderIDs._AerosolSingleScatteringTexture, this.m_InScatteredRadianceTables[1]);
				cmd.SetGlobalTexture(HDShaderIDs._MultipleScatteringTexture, this.m_InScatteredRadianceTables[2]);
				return;
			}
			cmd.SetGlobalTexture(HDShaderIDs._AirSingleScatteringTexture, CoreUtils.blackVolumeTexture);
			cmd.SetGlobalTexture(HDShaderIDs._AerosolSingleScatteringTexture, CoreUtils.blackVolumeTexture);
			cmd.SetGlobalTexture(HDShaderIDs._MultipleScatteringTexture, CoreUtils.blackVolumeTexture);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0005010C File Offset: 0x0004E30C
		public override void Cleanup()
		{
			RTHandles.Release(this.m_GroundIrradianceTables[0]);
			this.m_GroundIrradianceTables[0] = null;
			RTHandles.Release(this.m_GroundIrradianceTables[1]);
			this.m_GroundIrradianceTables[1] = null;
			RTHandles.Release(this.m_InScatteredRadianceTables[0]);
			this.m_InScatteredRadianceTables[0] = null;
			RTHandles.Release(this.m_InScatteredRadianceTables[1]);
			this.m_InScatteredRadianceTables[1] = null;
			RTHandles.Release(this.m_InScatteredRadianceTables[2]);
			this.m_InScatteredRadianceTables[2] = null;
			RTHandles.Release(this.m_InScatteredRadianceTables[3]);
			this.m_InScatteredRadianceTables[3] = null;
			RTHandles.Release(this.m_InScatteredRadianceTables[4]);
			this.m_InScatteredRadianceTables[4] = null;
			this.m_LastPrecomputedBounce = 0;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000501BC File Offset: 0x0004E3BC
		private static float CornetteShanksPhasePartConstant(float anisotropy)
		{
			return 0.119366206f * (1f - anisotropy * anisotropy) / (2f + anisotropy * anisotropy);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000501E4 File Offset: 0x0004E3E4
		private static Vector2 ComputeExponentialInterpolationParams(float k)
		{
			if (k == 0f)
			{
				k = 1E-06f;
			}
			float num = 10f * k;
			float num2 = 1f / (Mathf.Exp(num) - 1f);
			return new Vector2(num, num2);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00050224 File Offset: 0x0004E424
		private void UpdateGlobalConstantBuffer(CommandBuffer cmd, BuiltinSkyParameters builtinParams)
		{
			PhysicallyBasedSky physicallyBasedSky = builtinParams.skySettings as PhysicallyBasedSky;
			float planetaryRadius = physicallyBasedSky.GetPlanetaryRadius();
			float num = Mathf.Max(physicallyBasedSky.airMaximumAltitude.value, physicallyBasedSky.aerosolMaximumAltitude.value);
			float airScaleHeight = physicallyBasedSky.GetAirScaleHeight();
			float aerosolScaleHeight = physicallyBasedSky.GetAerosolScaleHeight();
			float skyIntensity = SkyRenderer.GetSkyIntensity(physicallyBasedSky, builtinParams.debugSettings);
			Vector2 vector = PhysicallyBasedSkyRenderer.ComputeExponentialInterpolationParams(physicallyBasedSky.horizonZenithShift.value);
			cmd.SetGlobalFloat(HDShaderIDs._PlanetaryRadius, planetaryRadius);
			cmd.SetGlobalFloat(HDShaderIDs._RcpPlanetaryRadius, 1f / planetaryRadius);
			cmd.SetGlobalFloat(HDShaderIDs._AtmosphericDepth, num);
			cmd.SetGlobalFloat(HDShaderIDs._RcpAtmosphericDepth, 1f / num);
			cmd.SetGlobalFloat(HDShaderIDs._AtmosphericRadius, planetaryRadius + num);
			cmd.SetGlobalFloat(HDShaderIDs._AerosolAnisotropy, physicallyBasedSky.aerosolAnisotropy.value);
			cmd.SetGlobalFloat(HDShaderIDs._AerosolPhasePartConstant, PhysicallyBasedSkyRenderer.CornetteShanksPhasePartConstant(physicallyBasedSky.aerosolAnisotropy.value));
			cmd.SetGlobalFloat(HDShaderIDs._AirDensityFalloff, 1f / airScaleHeight);
			cmd.SetGlobalFloat(HDShaderIDs._AirScaleHeight, airScaleHeight);
			cmd.SetGlobalFloat(HDShaderIDs._AerosolDensityFalloff, 1f / aerosolScaleHeight);
			cmd.SetGlobalFloat(HDShaderIDs._AerosolScaleHeight, aerosolScaleHeight);
			cmd.SetGlobalVector(HDShaderIDs._AirSeaLevelExtinction, physicallyBasedSky.GetAirExtinctionCoefficient());
			cmd.SetGlobalFloat(HDShaderIDs._AerosolSeaLevelExtinction, physicallyBasedSky.GetAerosolExtinctionCoefficient());
			cmd.SetGlobalVector(HDShaderIDs._AirSeaLevelScattering, physicallyBasedSky.GetAirScatteringCoefficient());
			cmd.SetGlobalFloat(HDShaderIDs._IntensityMultiplier, skyIntensity);
			cmd.SetGlobalVector(HDShaderIDs._AerosolSeaLevelScattering, physicallyBasedSky.GetAerosolScatteringCoefficient());
			cmd.SetGlobalFloat(HDShaderIDs._ColorSaturation, physicallyBasedSky.colorSaturation.value);
			cmd.SetGlobalVector(HDShaderIDs._GroundAlbedo, physicallyBasedSky.groundTint.value);
			cmd.SetGlobalFloat(HDShaderIDs._AlphaSaturation, physicallyBasedSky.alphaSaturation.value);
			cmd.SetGlobalVector(HDShaderIDs._PlanetCenterPosition, physicallyBasedSky.GetPlanetCenterPosition(builtinParams.worldSpaceCameraPos));
			cmd.SetGlobalFloat(HDShaderIDs._AlphaMultiplier, physicallyBasedSky.alphaMultiplier.value);
			cmd.SetGlobalVector(HDShaderIDs._HorizonTint, physicallyBasedSky.horizonTint.value);
			cmd.SetGlobalFloat(HDShaderIDs._HorizonZenithShiftPower, vector.x);
			cmd.SetGlobalVector(HDShaderIDs._ZenithTint, physicallyBasedSky.zenithTint.value);
			cmd.SetGlobalFloat(HDShaderIDs._HorizonZenithShiftScale, vector.y);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0005047C File Offset: 0x0004E67C
		private void PrecomputeTables(CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.InScatteredRadiancePrecomputation)))
			{
				int num = this.m_LastPrecomputedBounce + 1;
				int num2 = Math.Min(num - 1, 2);
				int num3 = 3;
				int num4 = Math.Min(num, 2);
				for (int i = 0; i < num4; i++)
				{
					int num5 = ((i == 0) ? num2 : num3);
					switch (num5)
					{
					case 0:
						cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_InScatteredRadiancePrecomputationCS, num5, HDShaderIDs._AirSingleScatteringTable, this.m_InScatteredRadianceTables[0]);
						cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_InScatteredRadiancePrecomputationCS, num5, HDShaderIDs._AerosolSingleScatteringTable, this.m_InScatteredRadianceTables[1]);
						cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_InScatteredRadiancePrecomputationCS, num5, HDShaderIDs._MultipleScatteringTable, this.m_InScatteredRadianceTables[2]);
						cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_InScatteredRadiancePrecomputationCS, num5, HDShaderIDs._MultipleScatteringTableOrder, this.m_InScatteredRadianceTables[3]);
						break;
					case 1:
						cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_InScatteredRadiancePrecomputationCS, num5, HDShaderIDs._AirSingleScatteringTexture, this.m_InScatteredRadianceTables[0]);
						cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_InScatteredRadiancePrecomputationCS, num5, HDShaderIDs._AerosolSingleScatteringTexture, this.m_InScatteredRadianceTables[1]);
						cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_InScatteredRadiancePrecomputationCS, num5, HDShaderIDs._GroundIrradianceTexture, this.m_GroundIrradianceTables[1]);
						cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_InScatteredRadiancePrecomputationCS, num5, HDShaderIDs._MultipleScatteringTable, this.m_InScatteredRadianceTables[4]);
						break;
					case 2:
						cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_InScatteredRadiancePrecomputationCS, num5, HDShaderIDs._MultipleScatteringTexture, this.m_InScatteredRadianceTables[3]);
						cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_InScatteredRadiancePrecomputationCS, num5, HDShaderIDs._GroundIrradianceTexture, this.m_GroundIrradianceTables[1]);
						cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_InScatteredRadiancePrecomputationCS, num5, HDShaderIDs._MultipleScatteringTable, this.m_InScatteredRadianceTables[4]);
						break;
					case 3:
						cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_InScatteredRadiancePrecomputationCS, num5, HDShaderIDs._MultipleScatteringTexture, this.m_InScatteredRadianceTables[4]);
						cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_InScatteredRadiancePrecomputationCS, num5, HDShaderIDs._MultipleScatteringTableOrder, this.m_InScatteredRadianceTables[3]);
						cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_InScatteredRadiancePrecomputationCS, num5, HDShaderIDs._MultipleScatteringTable, this.m_InScatteredRadianceTables[2]);
						break;
					}
					cmd.DispatchCompute(PhysicallyBasedSkyRenderer.s_InScatteredRadiancePrecomputationCS, num5, 32, 8, 256);
				}
				cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_GroundIrradiancePrecomputationCS, num2, HDShaderIDs._GroundIrradianceTable, this.m_GroundIrradianceTables[0]);
				cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_GroundIrradiancePrecomputationCS, num2, HDShaderIDs._GroundIrradianceTableOrder, this.m_GroundIrradianceTables[1]);
				switch (num2)
				{
				case 1:
					cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_GroundIrradiancePrecomputationCS, num2, HDShaderIDs._AirSingleScatteringTexture, this.m_InScatteredRadianceTables[0]);
					cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_GroundIrradiancePrecomputationCS, num2, HDShaderIDs._AerosolSingleScatteringTexture, this.m_InScatteredRadianceTables[1]);
					break;
				case 2:
					cmd.SetComputeTextureParam(PhysicallyBasedSkyRenderer.s_GroundIrradiancePrecomputationCS, num2, HDShaderIDs._MultipleScatteringTexture, this.m_InScatteredRadianceTables[3]);
					break;
				}
				cmd.DispatchCompute(PhysicallyBasedSkyRenderer.s_GroundIrradiancePrecomputationCS, num2, 4, 1, 1);
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x000507AC File Offset: 0x0004E9AC
		protected override bool Update(BuiltinSkyParameters builtinParams)
		{
			this.UpdateGlobalConstantBuffer(builtinParams.commandBuffer, builtinParams);
			PhysicallyBasedSky physicallyBasedSky = builtinParams.skySettings as PhysicallyBasedSky;
			int precomputationHashCode = physicallyBasedSky.GetPrecomputationHashCode();
			if (precomputationHashCode != this.m_LastPrecomputationParamHash)
			{
				this.m_LastPrecomputedBounce = 0;
			}
			if (this.m_LastPrecomputedBounce == 0)
			{
				if (this.m_GroundIrradianceTables[1] == null)
				{
					this.m_GroundIrradianceTables[1] = this.AllocateGroundIrradianceTable(1);
				}
				if (this.m_InScatteredRadianceTables[3] == null)
				{
					this.m_InScatteredRadianceTables[3] = this.AllocateInScatteredRadianceTable(3);
				}
				if (this.m_InScatteredRadianceTables[4] == null)
				{
					this.m_InScatteredRadianceTables[4] = this.AllocateInScatteredRadianceTable(4);
				}
			}
			if (this.m_LastPrecomputedBounce == physicallyBasedSky.numberOfBounces.value)
			{
				RTHandles.Release(this.m_GroundIrradianceTables[1]);
				RTHandles.Release(this.m_InScatteredRadianceTables[3]);
				RTHandles.Release(this.m_InScatteredRadianceTables[4]);
				this.m_GroundIrradianceTables[1] = null;
				this.m_InScatteredRadianceTables[3] = null;
				this.m_InScatteredRadianceTables[4] = null;
			}
			if (this.m_LastPrecomputedBounce < physicallyBasedSky.numberOfBounces.value)
			{
				this.PrecomputeTables(builtinParams.commandBuffer);
				this.m_LastPrecomputedBounce++;
				this.m_LastPrecomputationParamHash = precomputationHashCode;
				return builtinParams.skySettings.updateMode != EnvironmentUpdateMode.Realtime;
			}
			return false;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000508D8 File Offset: 0x0004EAD8
		public override void RenderSky(BuiltinSkyParameters builtinParams, bool renderForCubemap, bool renderSunDisk)
		{
			PhysicallyBasedSky physicallyBasedSky = builtinParams.skySettings as PhysicallyBasedSky;
			Vector3 worldSpaceCameraPos = builtinParams.worldSpaceCameraPos;
			float num = Vector3.Distance(worldSpaceCameraPos, physicallyBasedSky.GetPlanetCenterPosition(worldSpaceCameraPos));
			float planetaryRadius = physicallyBasedSky.GetPlanetaryRadius();
			bool flag = num > planetaryRadius;
			CommandBuffer commandBuffer = builtinParams.commandBuffer;
			Quaternion quaternion = Quaternion.Euler(physicallyBasedSky.planetRotation.value.x, physicallyBasedSky.planetRotation.value.y, physicallyBasedSky.planetRotation.value.z);
			Quaternion quaternion2 = Quaternion.Euler(physicallyBasedSky.spaceRotation.value.x, physicallyBasedSky.spaceRotation.value.y, physicallyBasedSky.spaceRotation.value.z);
			PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetMatrix(HDShaderIDs._PixelCoordToViewDirWS, builtinParams.pixelCoordToViewDirMatrix);
			PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetVector(HDShaderIDs._WorldSpaceCameraPos1, builtinParams.worldSpaceCameraPos);
			PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetMatrix(HDShaderIDs._ViewMatrix1, builtinParams.viewMatrix);
			PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetMatrix(HDShaderIDs._PlanetRotation, Matrix4x4.Rotate(quaternion));
			PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetMatrix(HDShaderIDs._SpaceRotation, Matrix4x4.Rotate(quaternion2));
			if (this.m_LastPrecomputedBounce != 0)
			{
				PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetTexture(HDShaderIDs._GroundIrradianceTexture, this.m_GroundIrradianceTables[0]);
				PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetTexture(HDShaderIDs._AirSingleScatteringTexture, this.m_InScatteredRadianceTables[0]);
				PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetTexture(HDShaderIDs._AerosolSingleScatteringTexture, this.m_InScatteredRadianceTables[1]);
				PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetTexture(HDShaderIDs._MultipleScatteringTexture, this.m_InScatteredRadianceTables[2]);
			}
			else
			{
				PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetTexture(HDShaderIDs._GroundIrradianceTexture, Texture2D.blackTexture);
				PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetTexture(HDShaderIDs._AirSingleScatteringTexture, CoreUtils.blackVolumeTexture);
				PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetTexture(HDShaderIDs._AerosolSingleScatteringTexture, CoreUtils.blackVolumeTexture);
				PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetTexture(HDShaderIDs._MultipleScatteringTexture, CoreUtils.blackVolumeTexture);
			}
			int num2 = 0;
			if (physicallyBasedSky.groundColorTexture.value != null)
			{
				num2 = 1;
				PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetTexture(HDShaderIDs._GroundAlbedoTexture, physicallyBasedSky.groundColorTexture.value);
			}
			PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetInt(HDShaderIDs._HasGroundAlbedoTexture, num2);
			int num3 = 0;
			if (physicallyBasedSky.groundEmissionTexture.value != null)
			{
				num3 = 1;
				PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetTexture(HDShaderIDs._GroundEmissionTexture, physicallyBasedSky.groundEmissionTexture.value);
				PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetFloat(HDShaderIDs._GroundEmissionMultiplier, physicallyBasedSky.groundEmissionMultiplier.value);
			}
			PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetInt(HDShaderIDs._HasGroundEmissionTexture, num3);
			int num4 = 0;
			if (physicallyBasedSky.spaceEmissionTexture.value != null)
			{
				num4 = 1;
				PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetTexture(HDShaderIDs._SpaceEmissionTexture, physicallyBasedSky.spaceEmissionTexture.value);
				PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetFloat(HDShaderIDs._SpaceEmissionMultiplier, physicallyBasedSky.spaceEmissionMultiplier.value);
			}
			PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetInt(HDShaderIDs._HasSpaceEmissionTexture, num4);
			PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties.SetInt(HDShaderIDs._RenderSunDisk, renderSunDisk ? 1 : 0);
			int num5 = (renderForCubemap ? 0 : 2) + (flag ? 0 : 1);
			CoreUtils.DrawFullScreen(builtinParams.commandBuffer, PhysicallyBasedSkyRenderer.s_PbrSkyMaterial, PhysicallyBasedSkyRenderer.s_PbrSkyMaterialProperties, num5);
		}

		// Token: 0x04000FA0 RID: 4000
		private int m_LastPrecomputationParamHash;

		// Token: 0x04000FA1 RID: 4001
		private int m_LastPrecomputedBounce;

		// Token: 0x04000FA2 RID: 4002
		private RTHandle[] m_GroundIrradianceTables;

		// Token: 0x04000FA3 RID: 4003
		private RTHandle[] m_InScatteredRadianceTables;

		// Token: 0x04000FA4 RID: 4004
		private static ComputeShader s_GroundIrradiancePrecomputationCS;

		// Token: 0x04000FA5 RID: 4005
		private static ComputeShader s_InScatteredRadiancePrecomputationCS;

		// Token: 0x04000FA6 RID: 4006
		private static Material s_PbrSkyMaterial;

		// Token: 0x04000FA7 RID: 4007
		private static MaterialPropertyBlock s_PbrSkyMaterialProperties;

		// Token: 0x04000FA8 RID: 4008
		private static GraphicsFormat s_ColorFormat = GraphicsFormat.R16G16B16A16_SFloat;

		// Token: 0x02000293 RID: 659
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
		public enum PbrSkyConfig
		{
			// Token: 0x040016E9 RID: 5865
			GroundIrradianceTableSize = 256,
			// Token: 0x040016EA RID: 5866
			InScatteredRadianceTableSizeX = 128,
			// Token: 0x040016EB RID: 5867
			InScatteredRadianceTableSizeY = 32,
			// Token: 0x040016EC RID: 5868
			InScatteredRadianceTableSizeZ = 16,
			// Token: 0x040016ED RID: 5869
			InScatteredRadianceTableSizeW = 64
		}
	}
}
