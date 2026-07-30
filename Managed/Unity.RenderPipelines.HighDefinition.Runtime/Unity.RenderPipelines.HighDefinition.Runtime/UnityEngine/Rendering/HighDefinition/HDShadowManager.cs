using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200008F RID: 143
	internal class HDShadowManager : IDisposable
	{
		// Token: 0x060005C4 RID: 1476 RVA: 0x00030F40 File Offset: 0x0002F140
		internal static ShadowResult ReadShadowResult(ShadowResult shadowResult, RenderGraphBuilder builder)
		{
			ShadowResult shadowResult2 = default(ShadowResult);
			if (shadowResult.punctualShadowResult.IsValid())
			{
				shadowResult2.punctualShadowResult = builder.ReadTexture(in shadowResult.punctualShadowResult);
			}
			if (shadowResult.directionalShadowResult.IsValid())
			{
				shadowResult2.directionalShadowResult = builder.ReadTexture(in shadowResult.directionalShadowResult);
			}
			if (shadowResult.areaShadowResult.IsValid())
			{
				shadowResult2.areaShadowResult = builder.ReadTexture(in shadowResult.areaShadowResult);
			}
			return shadowResult2;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00030FC0 File Offset: 0x0002F1C0
		internal ShadowResult RenderShadows(RenderGraph renderGraph, HDCamera hdCamera, CullingResults cullResults)
		{
			ShadowResult shadowResult = default(ShadowResult);
			if (this.m_ShadowRequestCount == 0)
			{
				return shadowResult;
			}
			shadowResult.punctualShadowResult = this.m_Atlas.RenderShadows(renderGraph, cullResults, hdCamera.frameSettings, "Punctual Lights Shadows rendering");
			shadowResult.directionalShadowResult = this.m_CascadeAtlas.RenderShadows(renderGraph, cullResults, hdCamera.frameSettings, "Directional Light Shadows rendering");
			shadowResult.areaShadowResult = this.m_AreaLightShadowAtlas.RenderShadows(renderGraph, cullResults, hdCamera.frameSettings, "Area Light Shadows rendering");
			return shadowResult;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0003103D File Offset: 0x0002F23D
		public static HDShadowManager instance
		{
			get
			{
				return HDShadowManager.s_Instance;
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00031044 File Offset: 0x0002F244
		private HDShadowManager()
		{
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00031058 File Offset: 0x0002F258
		public void InitShadowManager(RenderPipelineResources renderPipelineResources, DepthBits directionalShadowDepthBits, HDShadowInitParameters.HDShadowAtlasInitParams punctualLightAtlasInfo, HDShadowInitParameters.HDShadowAtlasInitParams areaLightAtlasInfo, int maxShadowRequests, Shader clearShader)
		{
			Material material = CoreUtils.CreateEngineMaterial(clearShader);
			this.m_ShadowDatas.Capacity = Math.Max(maxShadowRequests, this.m_ShadowDatas.Capacity);
			this.m_ShadowResolutionRequests = new HDShadowResolutionRequest[maxShadowRequests];
			this.m_ShadowRequests = new HDShadowRequest[maxShadowRequests];
			this.m_CachedDirectionalShadowData = new HDDirectionalShadowData[1];
			for (int i = 0; i < maxShadowRequests; i++)
			{
				this.m_ShadowResolutionRequests[i] = new HDShadowResolutionRequest();
			}
			this.m_Atlas = new HDShadowAtlas(renderPipelineResources, punctualLightAtlasInfo.shadowAtlasResolution, punctualLightAtlasInfo.shadowAtlasResolution, HDShaderIDs._ShadowmapAtlas, HDShaderIDs._ShadowAtlasSize, material, maxShadowRequests, HDShadowAtlas.BlurAlgorithm.None, FilterMode.Bilinear, punctualLightAtlasInfo.shadowAtlasDepthBits, RenderTextureFormat.Shadowmap, "Shadow Map Atlas", 0);
			HDShadowAtlas.BlurAlgorithm blurAlgorithm = ((HDShadowManager.GetDirectionalShadowAlgorithm() == DirectionalShadowAlgorithm.IMS) ? HDShadowAtlas.BlurAlgorithm.IM : HDShadowAtlas.BlurAlgorithm.None);
			this.m_CascadeAtlas = new HDShadowAtlas(renderPipelineResources, 1, 1, HDShaderIDs._ShadowmapCascadeAtlas, HDShaderIDs._CascadeShadowAtlasSize, material, maxShadowRequests, blurAlgorithm, FilterMode.Bilinear, directionalShadowDepthBits, RenderTextureFormat.Shadowmap, "Cascade Shadow Map Atlas", 0);
			if (ShaderConfig.s_AreaLights == 1)
			{
				this.m_AreaLightShadowAtlas = new HDShadowAtlas(renderPipelineResources, areaLightAtlasInfo.shadowAtlasResolution, areaLightAtlasInfo.shadowAtlasResolution, HDShaderIDs._AreaLightShadowmapAtlas, HDShaderIDs._AreaShadowAtlasSize, material, maxShadowRequests, HDShadowAtlas.BlurAlgorithm.EVSM, FilterMode.Bilinear, areaLightAtlasInfo.shadowAtlasDepthBits, RenderTextureFormat.Shadowmap, "Area Light Shadow Map Atlas", HDShaderIDs._AreaShadowmapMomentAtlas);
			}
			this.m_ShadowDataBuffer = new ComputeBuffer(maxShadowRequests, Marshal.SizeOf(typeof(HDShadowData)));
			this.m_DirectionalShadowDataBuffer = new ComputeBuffer(1, Marshal.SizeOf(typeof(HDDirectionalShadowData)));
			this.m_MaxShadowRequests = maxShadowRequests;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000311B4 File Offset: 0x0002F3B4
		public static DirectionalShadowAlgorithm GetDirectionalShadowAlgorithm()
		{
			switch (HDRenderPipeline.currentAsset.currentPlatformRenderPipelineSettings.hdShadowInitParams.shadowFilteringQuality)
			{
			case HDShadowFilteringQuality.Low:
				return DirectionalShadowAlgorithm.PCF5x5;
			case HDShadowFilteringQuality.Medium:
				return DirectionalShadowAlgorithm.PCF7x7;
			case HDShadowFilteringQuality.High:
				return DirectionalShadowAlgorithm.PCSS;
			default:
				return DirectionalShadowAlgorithm.PCF5x5;
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000311F4 File Offset: 0x0002F3F4
		public void UpdateDirectionalShadowResolution(int resolution, int cascadeCount)
		{
			Vector2Int vector2Int = new Vector2Int(resolution, resolution);
			if (cascadeCount > 1)
			{
				vector2Int.x *= 2;
			}
			if (cascadeCount > 2)
			{
				vector2Int.y *= 2;
			}
			this.m_CascadeAtlas.UpdateSize(vector2Int);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0003123C File Offset: 0x0002F43C
		internal int ReserveShadowResolutions(Vector2 resolution, ShadowMapType shadowMapType, int lightID, int index, bool canBeCached, out int cachedRequestIdx)
		{
			cachedRequestIdx = -1;
			if (this.m_ShadowRequestCount >= this.m_MaxShadowRequests)
			{
				Debug.LogWarning("Max shadow requests count reached, dropping all exceeding requests. You can increase this limit by changing the max requests in the HDRP asset");
				return -1;
			}
			int num = -1;
			this.m_ShadowResolutionRequests[this.m_ShadowResolutionRequestCounter].resolution = resolution;
			this.m_ShadowResolutionRequests[this.m_ShadowResolutionRequestCounter].shadowMapType = shadowMapType;
			this.m_ShadowResolutionRequests[this.m_ShadowResolutionRequestCounter].lightID = lightID;
			this.m_ShadowResolutionRequests[this.m_ShadowResolutionRequestCounter].emptyRequest = false;
			this.m_ShadowResolutionRequests[this.m_ShadowResolutionRequestCounter].indexInLight = index;
			this.m_ShadowResolutionRequests[this.m_ShadowResolutionRequestCounter].atlasViewport.width = resolution.x;
			this.m_ShadowResolutionRequests[this.m_ShadowResolutionRequestCounter].atlasViewport.height = resolution.y;
			if (canBeCached)
			{
				this.m_ShadowResolutionRequests[this.m_ShadowResolutionRequestCounter].hasBeenStoredInCachedList = true;
			}
			else
			{
				this.m_ShadowResolutionRequests[this.m_ShadowResolutionRequestCounter].hasBeenStoredInCachedList = false;
			}
			switch (shadowMapType)
			{
			case ShadowMapType.CascadedDirectional:
				this.m_CascadeAtlas.ReserveResolution(this.m_ShadowResolutionRequests[this.m_ShadowResolutionRequestCounter]);
				break;
			case ShadowMapType.PunctualAtlas:
				if (canBeCached)
				{
					num = this.m_Atlas.RegisterCachedLight(this.m_ShadowResolutionRequests[this.m_ShadowResolutionRequestCounter]);
				}
				this.m_Atlas.ReserveResolution(this.m_ShadowResolutionRequests[this.m_ShadowResolutionRequestCounter]);
				break;
			case ShadowMapType.AreaLightAtlas:
				if (canBeCached)
				{
					num = this.m_AreaLightShadowAtlas.RegisterCachedLight(this.m_ShadowResolutionRequests[this.m_ShadowResolutionRequestCounter]);
				}
				this.m_AreaLightShadowAtlas.ReserveResolution(this.m_ShadowResolutionRequests[this.m_ShadowResolutionRequestCounter]);
				break;
			}
			this.m_ShadowResolutionRequestCounter++;
			this.m_ShadowRequestCount = this.m_ShadowResolutionRequestCounter;
			cachedRequestIdx = num;
			return this.m_ShadowResolutionRequestCounter - 1;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000313F2 File Offset: 0x0002F5F2
		internal void MarkCachedShadowSlotsAsEmpty(ShadowMapType shadowMapType, int lightID)
		{
			if (shadowMapType != ShadowMapType.PunctualAtlas)
			{
				if (shadowMapType != ShadowMapType.AreaLightAtlas)
				{
					return;
				}
				if (this.m_AreaLightShadowAtlas != null)
				{
					this.m_AreaLightShadowAtlas.MarkCachedShadowSlotAsEmpty(lightID);
				}
			}
			else if (this.m_Atlas != null)
			{
				this.m_Atlas.MarkCachedShadowSlotAsEmpty(lightID);
				return;
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00031426 File Offset: 0x0002F626
		internal void CheckForCulledCachedShadows()
		{
			this.m_Atlas.MarkCulledShadowMapAsEmptySlots();
			if (ShaderConfig.s_AreaLights == 1)
			{
				this.m_AreaLightShadowAtlas.MarkCulledShadowMapAsEmptySlots();
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00031446 File Offset: 0x0002F646
		internal bool CachedDataIsValid(ShadowMapType type)
		{
			if (type != ShadowMapType.PunctualAtlas)
			{
				return type == ShadowMapType.AreaLightAtlas && this.m_AreaLightShadowAtlas.frameOfCacheValidity > 30;
			}
			return this.m_Atlas.frameOfCacheValidity > 30;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00031473 File Offset: 0x0002F673
		internal void PruneEmptyCachedSlots(ShadowMapType type)
		{
			if (type != ShadowMapType.PunctualAtlas)
			{
				if (type != ShadowMapType.AreaLightAtlas)
				{
					return;
				}
				if (this.m_AreaLightShadowAtlas != null)
				{
					this.m_AreaLightShadowAtlas.PruneDeadCachedLightSlots();
				}
			}
			else if (this.m_Atlas != null)
			{
				this.m_Atlas.PruneDeadCachedLightSlots();
				return;
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000314A5 File Offset: 0x0002F6A5
		internal int GetAtlasShapeID(ShadowMapType type)
		{
			if (type == ShadowMapType.PunctualAtlas)
			{
				return this.m_Atlas.atlasShapeID;
			}
			if (type != ShadowMapType.AreaLightAtlas)
			{
				return -1;
			}
			return this.m_AreaLightShadowAtlas.atlasShapeID;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000314CA File Offset: 0x0002F6CA
		internal bool AtlasHasResized(ShadowMapType type)
		{
			if (type != ShadowMapType.PunctualAtlas)
			{
				return type == ShadowMapType.AreaLightAtlas && this.m_AreaLightShadowAtlas.HasResizedThisFrame();
			}
			return this.m_Atlas.HasResizedThisFrame();
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000314F0 File Offset: 0x0002F6F0
		internal HDShadowResolutionRequest GetResolutionRequest(ShadowMapType type, bool cachedShadow, int index)
		{
			if (cachedShadow)
			{
				if (type == ShadowMapType.PunctualAtlas)
				{
					return this.m_Atlas.GetCachedRequest(index);
				}
				if (type != ShadowMapType.AreaLightAtlas)
				{
					return null;
				}
				return this.m_AreaLightShadowAtlas.GetCachedRequest(index);
			}
			else
			{
				if (index < 0 || index >= this.m_ShadowRequestCount)
				{
					return null;
				}
				return this.m_ShadowResolutionRequests[index];
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0003153D File Offset: 0x0002F73D
		public Vector2 GetReservedResolution(int index)
		{
			if (index < 0 || index >= this.m_ShadowRequestCount)
			{
				return Vector2.zero;
			}
			return this.m_ShadowResolutionRequests[index].resolution;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00031560 File Offset: 0x0002F760
		internal void UpdateShadowRequest(int index, HDShadowRequest shadowRequest)
		{
			if (index >= this.m_ShadowRequestCount)
			{
				return;
			}
			this.m_ShadowRequests[index] = shadowRequest;
			switch (shadowRequest.shadowMapType)
			{
			case ShadowMapType.CascadedDirectional:
				this.m_CascadeAtlas.AddShadowRequest(shadowRequest);
				return;
			case ShadowMapType.PunctualAtlas:
				this.m_Atlas.AddShadowRequest(shadowRequest);
				return;
			case ShadowMapType.AreaLightAtlas:
				this.m_AreaLightShadowAtlas.AddShadowRequest(shadowRequest);
				return;
			default:
				return;
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000315C0 File Offset: 0x0002F7C0
		public unsafe void UpdateCascade(int cascadeIndex, Vector4 cullingSphere, float border)
		{
			if (cullingSphere.w != float.NegativeInfinity)
			{
				cullingSphere.w *= cullingSphere.w;
			}
			this.m_CascadeCount = Mathf.Max(this.m_CascadeCount, cascadeIndex);
			fixed (float* ptr = &this.m_DirectionalShadowData.sphereCascades.FixedElementField)
			{
				((Vector4*)ptr)[cascadeIndex] = cullingSphere;
			}
			fixed (float* ptr = &this.m_DirectionalShadowData.cascadeBorders.FixedElementField)
			{
				ptr[cascadeIndex] = border;
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00031640 File Offset: 0x0002F840
		private HDShadowData CreateShadowData(HDShadowRequest shadowRequest, HDShadowAtlas atlas)
		{
			HDShadowData hdshadowData = default(HDShadowData);
			Matrix4x4 deviceProjection = shadowRequest.deviceProjection;
			Matrix4x4 view = shadowRequest.view;
			hdshadowData.proj = new Vector4(deviceProjection.m00, deviceProjection.m11, deviceProjection.m22, deviceProjection.m23);
			hdshadowData.pos = shadowRequest.position;
			hdshadowData.rot0 = new Vector3(view.m00, view.m01, view.m02);
			hdshadowData.rot1 = new Vector3(view.m10, view.m11, view.m12);
			hdshadowData.rot2 = new Vector3(view.m20, view.m21, view.m22);
			hdshadowData.shadowToWorld = shadowRequest.shadowToWorld;
			hdshadowData.cacheTranslationDelta = new Vector3(0f, 0f, 0f);
			float num = 1f / (float)atlas.width;
			float num2 = 1f / (float)atlas.height;
			hdshadowData.atlasOffset = Vector2.Scale(new Vector2(num, num2), new Vector2(shadowRequest.atlasViewport.x, shadowRequest.atlasViewport.y));
			hdshadowData.shadowMapSize = new Vector4(shadowRequest.atlasViewport.width, shadowRequest.atlasViewport.height, 1f / shadowRequest.atlasViewport.width, 1f / shadowRequest.atlasViewport.height);
			hdshadowData.normalBias = shadowRequest.normalBias;
			hdshadowData.worldTexelSize = shadowRequest.worldTexelSize;
			hdshadowData.shadowFilterParams0.x = shadowRequest.shadowSoftness;
			hdshadowData.shadowFilterParams0.y = HDShadowUtils.Asfloat(shadowRequest.blockerSampleCount);
			hdshadowData.shadowFilterParams0.z = HDShadowUtils.Asfloat(shadowRequest.filterSampleCount);
			hdshadowData.shadowFilterParams0.w = shadowRequest.minFilterSize;
			hdshadowData.zBufferParam = shadowRequest.zBufferParam;
			if (atlas.HasBlurredEVSM())
			{
				hdshadowData.shadowFilterParams0 = shadowRequest.evsmParams;
			}
			return hdshadowData;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00031838 File Offset: 0x0002FA38
		private unsafe Vector4 GetCascadeSphereAtIndex(int index)
		{
			fixed (float* ptr = &this.m_DirectionalShadowData.sphereCascades.FixedElementField)
			{
				return ((Vector4*)ptr)[index];
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00031867 File Offset: 0x0002FA67
		public void UpdateCullingParameters(ref ScriptableCullingParameters cullingParams, float maxShadowDistance)
		{
			cullingParams.shadowDistance = Mathf.Min(maxShadowDistance, cullingParams.shadowDistance);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0003187C File Offset: 0x0002FA7C
		public void LayoutShadowMaps(LightingDebugSettings lightingDebugSettings)
		{
			this.m_Atlas.UpdateDebugSettings(lightingDebugSettings);
			if (this.m_CascadeAtlas != null)
			{
				this.m_CascadeAtlas.UpdateDebugSettings(lightingDebugSettings);
			}
			if (ShaderConfig.s_AreaLights == 1)
			{
				this.m_AreaLightShadowAtlas.UpdateDebugSettings(lightingDebugSettings);
			}
			if (lightingDebugSettings.shadowResolutionScaleFactor != 1f)
			{
				foreach (HDShadowResolutionRequest hdshadowResolutionRequest in this.m_ShadowResolutionRequests)
				{
					if (hdshadowResolutionRequest.shadowMapType != ShadowMapType.CascadedDirectional)
					{
						hdshadowResolutionRequest.resolution *= lightingDebugSettings.shadowResolutionScaleFactor;
					}
				}
			}
			if (this.m_CascadeAtlas != null && !this.m_CascadeAtlas.Layout(false))
			{
				Debug.LogError("Cascade Shadow atlasing has failed, only one directional light can cast shadows at a time");
			}
			this.m_Atlas.Layout(true);
			if (ShaderConfig.s_AreaLights == 1)
			{
				this.m_AreaLightShadowAtlas.Layout(true);
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00031944 File Offset: 0x0002FB44
		public unsafe void PrepareGPUShadowDatas(CullingResults cullResults, HDCamera camera)
		{
			int num = 0;
			this.m_ShadowDatas.Clear();
			for (int i = 0; i < this.m_ShadowRequestCount; i++)
			{
				HDShadowAtlas hdshadowAtlas = this.m_Atlas;
				if (this.m_ShadowRequests[i].shadowMapType == ShadowMapType.CascadedDirectional)
				{
					hdshadowAtlas = this.m_CascadeAtlas;
				}
				else if (this.m_ShadowRequests[i].shadowMapType == ShadowMapType.AreaLightAtlas)
				{
					hdshadowAtlas = this.m_AreaLightShadowAtlas;
				}
				HDShadowData hdshadowData;
				if (this.m_ShadowRequests[i].shouldUseCachedShadow)
				{
					hdshadowData = this.m_ShadowRequests[i].cachedShadowData;
				}
				else
				{
					hdshadowData = this.CreateShadowData(this.m_ShadowRequests[i], hdshadowAtlas);
					this.m_ShadowRequests[i].cachedShadowData = hdshadowData;
				}
				this.m_ShadowDatas.Add(hdshadowData);
				this.m_ShadowRequests[i].shadowIndex = num++;
			}
			int num2 = 4;
			int num3 = 4;
			fixed (float* ptr = &this.m_DirectionalShadowData.sphereCascades.FixedElementField)
			{
				Vector4* ptr2 = (Vector4*)ptr;
				for (int j = 0; j < 4; j++)
				{
					num2 = ((num2 == 4 && ptr2[j].w > 0f) ? j : num2);
					num3 = (((num3 == 4 || num3 == num2) && ptr2[j].w > 0f) ? j : num3);
				}
			}
			if (num3 != 4)
			{
				this.m_DirectionalShadowData.cascadeDirection = (this.GetCascadeSphereAtIndex(num3) - this.GetCascadeSphereAtIndex(num2)).normalized;
			}
			else
			{
				this.m_DirectionalShadowData.cascadeDirection = Vector4.zero;
			}
			this.m_DirectionalShadowData.cascadeDirection.w = (float)camera.volumeStack.GetComponent<HDShadowSettings>().cascadeShadowSplitCount.value;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00031AEC File Offset: 0x0002FCEC
		public void RenderShadows(ScriptableRenderContext renderContext, CommandBuffer cmd, CullingResults cullResults, HDCamera hdCamera)
		{
			if (this.m_ShadowRequestCount == 0)
			{
				return;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderPunctualShadowMaps)))
			{
				this.m_Atlas.RenderShadows(cullResults, hdCamera.frameSettings, renderContext, cmd);
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderDirectionalShadowMaps)))
			{
				this.m_CascadeAtlas.RenderShadows(cullResults, hdCamera.frameSettings, renderContext, cmd);
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderAreaShadowMaps)))
			{
				if (ShaderConfig.s_AreaLights == 1)
				{
					this.m_AreaLightShadowAtlas.RenderShadows(cullResults, hdCamera.frameSettings, renderContext, cmd);
				}
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00031BD0 File Offset: 0x0002FDD0
		public void SyncData()
		{
			if (this.m_ShadowRequestCount == 0)
			{
				return;
			}
			this.m_ShadowDataBuffer.SetData<HDShadowData>(this.m_ShadowDatas);
			this.m_CachedDirectionalShadowData[0] = this.m_DirectionalShadowData;
			this.m_DirectionalShadowDataBuffer.SetData(this.m_CachedDirectionalShadowData);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00031C0F File Offset: 0x0002FE0F
		public void PushGlobalParameters(CommandBuffer cmd)
		{
			cmd.SetGlobalBuffer(HDShaderIDs._HDShadowDatas, this.m_ShadowDataBuffer);
			cmd.SetGlobalBuffer(HDShaderIDs._HDDirectionalShadowData, this.m_DirectionalShadowDataBuffer);
			cmd.SetGlobalInt(HDShaderIDs._CascadeShadowCount, this.m_CascadeCount + 1);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00031C46 File Offset: 0x0002FE46
		public void BindResources(CommandBuffer cmd)
		{
			this.PushGlobalParameters(cmd);
			this.m_Atlas.BindResources(cmd);
			this.m_CascadeAtlas.BindResources(cmd);
			if (ShaderConfig.s_AreaLights == 1)
			{
				this.m_AreaLightShadowAtlas.BindResources(cmd);
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00031C7B File Offset: 0x0002FE7B
		public int GetShadowRequestCount()
		{
			return this.m_ShadowRequestCount;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00031C83 File Offset: 0x0002FE83
		public void Clear()
		{
			this.m_Atlas.Clear();
			this.m_CascadeAtlas.Clear();
			if (ShaderConfig.s_AreaLights == 1)
			{
				this.m_AreaLightShadowAtlas.Clear();
			}
			this.m_ShadowResolutionRequestCounter = 0;
			this.m_ShadowRequestCount = 0;
			this.m_CascadeCount = 0;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00031CC4 File Offset: 0x0002FEC4
		public HDShadowManager.ShadowDebugAtlasTextures GetDebugAtlasTextures()
		{
			HDShadowManager.ShadowDebugAtlasTextures shadowDebugAtlasTextures = default(HDShadowManager.ShadowDebugAtlasTextures);
			if (ShaderConfig.s_AreaLights == 1)
			{
				shadowDebugAtlasTextures.areaShadowAtlas = this.m_AreaLightShadowAtlas.renderTarget;
			}
			shadowDebugAtlasTextures.punctualShadowAtlas = this.m_Atlas.renderTarget;
			shadowDebugAtlasTextures.cascadeShadowAtlas = this.m_CascadeAtlas.renderTarget;
			return shadowDebugAtlasTextures;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00031D18 File Offset: 0x0002FF18
		public void DisplayShadowAtlas(RTHandle atlasTexture, CommandBuffer cmd, Material debugMaterial, float screenX, float screenY, float screenSizeX, float screenSizeY, float minValue, float maxValue, MaterialPropertyBlock mpb)
		{
			this.m_Atlas.DisplayAtlas(atlasTexture, cmd, debugMaterial, new Rect(0f, 0f, (float)this.m_Atlas.width, (float)this.m_Atlas.height), screenX, screenY, screenSizeX, screenSizeY, minValue, maxValue, mpb);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00031D68 File Offset: 0x0002FF68
		public void DisplayShadowCascadeAtlas(RTHandle atlasTexture, CommandBuffer cmd, Material debugMaterial, float screenX, float screenY, float screenSizeX, float screenSizeY, float minValue, float maxValue, MaterialPropertyBlock mpb)
		{
			this.m_CascadeAtlas.DisplayAtlas(atlasTexture, cmd, debugMaterial, new Rect(0f, 0f, (float)this.m_CascadeAtlas.width, (float)this.m_CascadeAtlas.height), screenX, screenY, screenSizeX, screenSizeY, minValue, maxValue, mpb);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00031DB8 File Offset: 0x0002FFB8
		public void DisplayAreaLightShadowAtlas(RTHandle atlasTexture, CommandBuffer cmd, Material debugMaterial, float screenX, float screenY, float screenSizeX, float screenSizeY, float minValue, float maxValue, MaterialPropertyBlock mpb)
		{
			if (ShaderConfig.s_AreaLights == 1)
			{
				this.m_AreaLightShadowAtlas.DisplayAtlas(atlasTexture, cmd, debugMaterial, new Rect(0f, 0f, (float)this.m_AreaLightShadowAtlas.width, (float)this.m_AreaLightShadowAtlas.height), screenX, screenY, screenSizeX, screenSizeY, minValue, maxValue, mpb);
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00031E10 File Offset: 0x00030010
		public void DisplayShadowMap(in HDShadowManager.ShadowDebugAtlasTextures atlasTextures, int shadowIndex, CommandBuffer cmd, Material debugMaterial, float screenX, float screenY, float screenSizeX, float screenSizeY, float minValue, float maxValue, MaterialPropertyBlock mpb)
		{
			if (shadowIndex >= this.m_ShadowRequestCount)
			{
				return;
			}
			HDShadowRequest hdshadowRequest = this.m_ShadowRequests[shadowIndex];
			switch (hdshadowRequest.shadowMapType)
			{
			case ShadowMapType.CascadedDirectional:
				this.m_CascadeAtlas.DisplayAtlas(atlasTextures.cascadeShadowAtlas, cmd, debugMaterial, hdshadowRequest.atlasViewport, screenX, screenY, screenSizeX, screenSizeY, minValue, maxValue, mpb);
				return;
			case ShadowMapType.PunctualAtlas:
				this.m_Atlas.DisplayAtlas(atlasTextures.punctualShadowAtlas, cmd, debugMaterial, hdshadowRequest.atlasViewport, screenX, screenY, screenSizeX, screenSizeY, minValue, maxValue, mpb);
				return;
			case ShadowMapType.AreaLightAtlas:
				if (ShaderConfig.s_AreaLights == 1)
				{
					this.m_AreaLightShadowAtlas.DisplayAtlas(atlasTextures.areaShadowAtlas, cmd, debugMaterial, hdshadowRequest.atlasViewport, screenX, screenY, screenSizeX, screenSizeY, minValue, maxValue, mpb);
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00031ECC File Offset: 0x000300CC
		public void Dispose()
		{
			this.m_ShadowDataBuffer.Dispose();
			this.m_DirectionalShadowDataBuffer.Dispose();
			this.m_Atlas.Release();
			if (ShaderConfig.s_AreaLights == 1)
			{
				this.m_AreaLightShadowAtlas.Release();
			}
			this.m_CascadeAtlas.Release();
		}

		// Token: 0x040005DD RID: 1501
		public const int k_DirectionalShadowCascadeCount = 4;

		// Token: 0x040005DE RID: 1502
		public const int k_MinShadowMapResolution = 16;

		// Token: 0x040005DF RID: 1503
		public const int k_MaxShadowMapResolution = 16384;

		// Token: 0x040005E0 RID: 1504
		private List<HDShadowData> m_ShadowDatas = new List<HDShadowData>();

		// Token: 0x040005E1 RID: 1505
		private HDShadowRequest[] m_ShadowRequests;

		// Token: 0x040005E2 RID: 1506
		private HDShadowResolutionRequest[] m_ShadowResolutionRequests;

		// Token: 0x040005E3 RID: 1507
		private HDDirectionalShadowData[] m_CachedDirectionalShadowData;

		// Token: 0x040005E4 RID: 1508
		private HDDirectionalShadowData m_DirectionalShadowData;

		// Token: 0x040005E5 RID: 1509
		private ComputeBuffer m_ShadowDataBuffer;

		// Token: 0x040005E6 RID: 1510
		private ComputeBuffer m_DirectionalShadowDataBuffer;

		// Token: 0x040005E7 RID: 1511
		private HDShadowAtlas m_CascadeAtlas;

		// Token: 0x040005E8 RID: 1512
		private HDShadowAtlas m_Atlas;

		// Token: 0x040005E9 RID: 1513
		private HDShadowAtlas m_AreaLightShadowAtlas;

		// Token: 0x040005EA RID: 1514
		private int m_MaxShadowRequests;

		// Token: 0x040005EB RID: 1515
		private int m_ShadowRequestCount;

		// Token: 0x040005EC RID: 1516
		private int m_CascadeCount;

		// Token: 0x040005ED RID: 1517
		private int m_ShadowResolutionRequestCounter;

		// Token: 0x040005EE RID: 1518
		private static HDShadowManager s_Instance = new HDShadowManager();

		// Token: 0x0200021B RID: 539
		public struct ShadowDebugAtlasTextures
		{
			// Token: 0x040013E0 RID: 5088
			public RTHandle punctualShadowAtlas;

			// Token: 0x040013E1 RID: 5089
			public RTHandle cascadeShadowAtlas;

			// Token: 0x040013E2 RID: 5090
			public RTHandle areaShadowAtlas;
		}
	}
}
