using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200010F RID: 271
	internal class HDRaytracingLightCluster
	{
		// Token: 0x0600087F RID: 2175 RVA: 0x00045E54 File Offset: 0x00044054
		public void Initialize(HDRenderPipeline renderPipeline)
		{
			this.m_RenderPipelineResources = renderPipeline.asset.renderPipelineResources;
			this.m_RenderPipelineRayTracingResources = renderPipeline.asset.renderPipelineRayTracingResources;
			this.m_RenderPipeline = renderPipeline;
			this.m_DebugLightClusterTexture = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "DebugLightClusterTexture");
			this.m_LightCluster = new ComputeBuffer(1, 4);
			this.m_LightDataGPUArray = new ComputeBuffer(1, Marshal.SizeOf(typeof(LightData)));
			this.m_EnvLightDataGPUArray = new ComputeBuffer(1, Marshal.SizeOf(typeof(EnvLightData)));
			this.m_DebugMaterial = CoreUtils.CreateEngineMaterial(this.m_RenderPipelineRayTracingResources.lightClusterDebugS);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00045F18 File Offset: 0x00044118
		public void ReleaseResources()
		{
			this.m_DebugLightClusterTexture.Release();
			if (this.m_LightVolumeGPUArray != null)
			{
				CoreUtils.SafeRelease(this.m_LightVolumeGPUArray);
				this.m_LightVolumeGPUArray = null;
			}
			if (this.m_LightCluster != null)
			{
				CoreUtils.SafeRelease(this.m_LightCluster);
				this.m_LightCluster = null;
			}
			if (this.m_LightCullResult != null)
			{
				CoreUtils.SafeRelease(this.m_LightCullResult);
				this.m_LightCullResult = null;
			}
			if (this.m_LightDataGPUArray != null)
			{
				CoreUtils.SafeRelease(this.m_LightDataGPUArray);
				this.m_LightDataGPUArray = null;
			}
			if (this.m_EnvLightDataGPUArray != null)
			{
				CoreUtils.SafeRelease(this.m_EnvLightDataGPUArray);
				this.m_EnvLightDataGPUArray = null;
			}
			if (this.m_DebugMaterial != null)
			{
				CoreUtils.Destroy(this.m_DebugMaterial);
				this.m_DebugMaterial = null;
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00045FD2 File Offset: 0x000441D2
		private void ResizeClusterBuffer(int bufferSize)
		{
			if (this.m_LightCluster != null)
			{
				if (this.m_LightCluster.count == bufferSize)
				{
					return;
				}
				CoreUtils.SafeRelease(this.m_LightCluster);
				this.m_LightCluster = null;
			}
			if (bufferSize > 0)
			{
				this.m_LightCluster = new ComputeBuffer(bufferSize, 4);
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0004600E File Offset: 0x0004420E
		private void ResizeCullResultBuffer(int numLights)
		{
			if (this.m_LightCullResult != null)
			{
				if (this.m_LightCullResult.count == numLights)
				{
					return;
				}
				CoreUtils.SafeRelease(this.m_LightCullResult);
				this.m_LightCullResult = null;
			}
			if (numLights > 0)
			{
				this.m_LightCullResult = new ComputeBuffer(numLights, 4);
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0004604C File Offset: 0x0004424C
		private void ResizeVolumeBuffer(int numLights)
		{
			if (this.m_LightVolumeGPUArray != null)
			{
				if (this.m_LightVolumeGPUArray.count == numLights)
				{
					return;
				}
				CoreUtils.SafeRelease(this.m_LightVolumeGPUArray);
				this.m_LightVolumeGPUArray = null;
			}
			if (numLights > 0)
			{
				this.m_LightVolumesCPUArray = new LightVolume[numLights];
				this.m_LightVolumeGPUArray = new ComputeBuffer(numLights, Marshal.SizeOf(typeof(LightVolume)));
			}
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000460B0 File Offset: 0x000442B0
		private void ResizeLightDataBuffer(int numLights)
		{
			if (this.m_LightDataGPUArray != null)
			{
				if (this.m_LightDataGPUArray.count == numLights)
				{
					return;
				}
				CoreUtils.SafeRelease(this.m_LightDataGPUArray);
				this.m_LightDataGPUArray = null;
			}
			if (numLights > 0)
			{
				this.m_LightDataGPUArray = new ComputeBuffer(numLights, Marshal.SizeOf(typeof(LightData)));
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00046108 File Offset: 0x00044308
		private void ResizeEnvLightDataBuffer(int numEnvLights)
		{
			if (this.m_EnvLightDataGPUArray != null)
			{
				if (this.m_EnvLightDataGPUArray.count == numEnvLights)
				{
					return;
				}
				CoreUtils.SafeRelease(this.m_EnvLightDataGPUArray);
				this.m_EnvLightDataGPUArray = null;
			}
			if (numEnvLights > 0)
			{
				this.m_EnvLightDataGPUArray = new ComputeBuffer(numEnvLights, Marshal.SizeOf(typeof(EnvLightData)));
			}
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00046160 File Offset: 0x00044360
		private void BuildGPULightVolumes(HDRayTracingLights rayTracingLights)
		{
			int lightCount = rayTracingLights.lightCount;
			if (this.m_LightVolumesCPUArray == null || lightCount != this.m_LightVolumesCPUArray.Length)
			{
				this.ResizeVolumeBuffer(lightCount);
			}
			this.punctualLightCount = 0;
			this.areaLightCount = 0;
			this.envLightCount = 0;
			this.totalLightCount = 0;
			int num = 0;
			for (int i = 0; i < rayTracingLights.hdLightArray.Count; i++)
			{
				HDAdditionalLightData hdadditionalLightData = rayTracingLights.hdLightArray[i];
				if (hdadditionalLightData != null)
				{
					Light component = hdadditionalLightData.gameObject.GetComponent<Light>();
					if (!(component == null) && component.enabled)
					{
						this.m_RenderPipeline.ReserveCookieAtlasTexture(hdadditionalLightData, component);
						float range = component.range;
						this.m_LightVolumesCPUArray[num].range = new Vector3(range, range, range);
						this.m_LightVolumesCPUArray[num].position = hdadditionalLightData.gameObject.transform.position;
						this.m_LightVolumesCPUArray[num].active = (hdadditionalLightData.gameObject.activeInHierarchy ? 1 : 0);
						this.m_LightVolumesCPUArray[num].lightIndex = (uint)i;
						if (hdadditionalLightData.type != HDLightType.Area)
						{
							this.m_LightVolumesCPUArray[num].shape = 0;
							this.m_LightVolumesCPUArray[num].lightType = 0U;
							this.punctualLightCount++;
						}
						else
						{
							this.m_LightVolumesCPUArray[num].shape = 1;
							this.m_LightVolumesCPUArray[num].lightType = 1U;
							this.areaLightCount++;
						}
						num++;
					}
				}
			}
			int num2 = num;
			for (int j = 0; j < rayTracingLights.reflectionProbeArray.Count; j++)
			{
				HDProbe hdprobe = rayTracingLights.reflectionProbeArray[j];
				if (hdprobe != null)
				{
					if (hdprobe.influenceVolume.shape == InfluenceShape.Sphere)
					{
						this.m_LightVolumesCPUArray[j + num2].shape = 0;
						this.m_LightVolumesCPUArray[j + num2].range = new Vector3(hdprobe.influenceVolume.sphereRadius, hdprobe.influenceVolume.sphereRadius, hdprobe.influenceVolume.sphereRadius);
						this.m_LightVolumesCPUArray[j + num2].position = hdprobe.influenceToWorld.GetColumn(3);
					}
					else
					{
						this.m_LightVolumesCPUArray[j + num2].shape = 1;
						this.m_LightVolumesCPUArray[j + num2].range = new Vector3(hdprobe.influenceVolume.boxSize.x / 2f, hdprobe.influenceVolume.boxSize.y / 2f, hdprobe.influenceVolume.boxSize.z / 2f);
						this.m_LightVolumesCPUArray[j + num2].position = hdprobe.influenceToWorld.GetColumn(3);
					}
					this.m_LightVolumesCPUArray[j + num2].active = (hdprobe.gameObject.activeInHierarchy ? 1 : 0);
					this.m_LightVolumesCPUArray[j + num2].lightIndex = (uint)j;
					this.m_LightVolumesCPUArray[j + num2].lightType = 2U;
					this.envLightCount++;
				}
			}
			this.totalLightCount = this.punctualLightCount + this.areaLightCount + this.envLightCount;
			this.m_LightVolumeGPUArray.SetData(this.m_LightVolumesCPUArray);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00046504 File Offset: 0x00044704
		private void EvaluateClusterVolume(HDCamera hdCamera)
		{
			LightCluster component = hdCamera.volumeStack.GetComponent<LightCluster>();
			this.clusterCenter = hdCamera.camera.gameObject.transform.position;
			this.minClusterPos.Set(float.MaxValue, float.MaxValue, float.MaxValue);
			this.maxClusterPos.Set(float.MinValue, float.MinValue, float.MinValue);
			for (int i = 0; i < this.totalLightCount; i++)
			{
				this.minClusterPos.x = Mathf.Min(this.m_LightVolumesCPUArray[i].position.x - this.m_LightVolumesCPUArray[i].range.x, this.minClusterPos.x);
				this.minClusterPos.y = Mathf.Min(this.m_LightVolumesCPUArray[i].position.y - this.m_LightVolumesCPUArray[i].range.y, this.minClusterPos.y);
				this.minClusterPos.z = Mathf.Min(this.m_LightVolumesCPUArray[i].position.z - this.m_LightVolumesCPUArray[i].range.z, this.minClusterPos.z);
				this.maxClusterPos.x = Mathf.Max(this.m_LightVolumesCPUArray[i].position.x + this.m_LightVolumesCPUArray[i].range.x, this.maxClusterPos.x);
				this.maxClusterPos.y = Mathf.Max(this.m_LightVolumesCPUArray[i].position.y + this.m_LightVolumesCPUArray[i].range.y, this.maxClusterPos.y);
				this.maxClusterPos.z = Mathf.Max(this.m_LightVolumesCPUArray[i].position.z + this.m_LightVolumesCPUArray[i].range.z, this.maxClusterPos.z);
			}
			this.minClusterPos.x = ((this.minClusterPos.x < this.clusterCenter.x - component.cameraClusterRange.value) ? (this.clusterCenter.x - component.cameraClusterRange.value) : this.minClusterPos.x);
			this.minClusterPos.y = ((this.minClusterPos.y < this.clusterCenter.y - component.cameraClusterRange.value) ? (this.clusterCenter.y - component.cameraClusterRange.value) : this.minClusterPos.y);
			this.minClusterPos.z = ((this.minClusterPos.z < this.clusterCenter.z - component.cameraClusterRange.value) ? (this.clusterCenter.z - component.cameraClusterRange.value) : this.minClusterPos.z);
			this.maxClusterPos.x = ((this.maxClusterPos.x > this.clusterCenter.x + component.cameraClusterRange.value) ? (this.clusterCenter.x + component.cameraClusterRange.value) : this.maxClusterPos.x);
			this.maxClusterPos.y = ((this.maxClusterPos.y > this.clusterCenter.y + component.cameraClusterRange.value) ? (this.clusterCenter.y + component.cameraClusterRange.value) : this.maxClusterPos.y);
			this.maxClusterPos.z = ((this.maxClusterPos.z > this.clusterCenter.z + component.cameraClusterRange.value) ? (this.clusterCenter.z + component.cameraClusterRange.value) : this.maxClusterPos.z);
			this.clusterCellSize = this.maxClusterPos - this.minClusterPos;
			this.clusterCellSize.x = this.clusterCellSize.x / 64f;
			this.clusterCellSize.y = this.clusterCellSize.y / 64f;
			this.clusterCellSize.z = this.clusterCellSize.z / 32f;
			this.clusterCenter = (this.maxClusterPos + this.minClusterPos) / 2f;
			this.clusterDimension = this.maxClusterPos - this.minClusterPos;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x000469B0 File Offset: 0x00044BB0
		private void CullLights(CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RaytracingCullLights)))
			{
				if (this.m_LightCullResult == null || this.m_LightCullResult.count != this.totalLightCount)
				{
					this.ResizeCullResultBuffer(this.totalLightCount);
				}
				ComputeShader lightClusterBuildCS = this.m_RenderPipelineRayTracingResources.lightClusterBuildCS;
				int num = lightClusterBuildCS.FindKernel("RaytracingLightCull");
				cmd.SetComputeVectorParam(lightClusterBuildCS, HDRaytracingLightCluster._ClusterCenterPosition, this.clusterCenter);
				cmd.SetComputeVectorParam(lightClusterBuildCS, HDRaytracingLightCluster._ClusterDimension, this.clusterDimension);
				cmd.SetComputeFloatParam(lightClusterBuildCS, HDRaytracingLightCluster._LightVolumeCount, HDShadowUtils.Asfloat(this.totalLightCount));
				cmd.SetComputeBufferParam(lightClusterBuildCS, num, HDRaytracingLightCluster._LightVolumes, this.m_LightVolumeGPUArray);
				cmd.SetComputeBufferParam(lightClusterBuildCS, num, HDRaytracingLightCluster._RaytracingLightCullResult, this.m_LightCullResult);
				int num2 = this.totalLightCount / 16 + 1;
				cmd.DispatchCompute(lightClusterBuildCS, num, num2, 1, 1);
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00046AB0 File Offset: 0x00044CB0
		private void BuildLightCluster(HDCamera hdCamera, CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RaytracingBuildCluster)))
			{
				LightCluster component = hdCamera.volumeStack.GetComponent<LightCluster>();
				this.numLightsPerCell = component.maxNumLightsPercell.value;
				int num = 131072 * (this.numLightsPerCell + 4);
				if (this.m_LightCluster.count != num)
				{
					this.ResizeClusterBuffer(num);
				}
				ComputeShader lightClusterBuildCS = this.m_RenderPipelineRayTracingResources.lightClusterBuildCS;
				int num2 = lightClusterBuildCS.FindKernel("RaytracingLightCluster");
				cmd.SetComputeBufferParam(lightClusterBuildCS, num2, HDShaderIDs._RaytracingLightCluster, this.m_LightCluster);
				cmd.SetComputeVectorParam(lightClusterBuildCS, HDShaderIDs._MinClusterPos, this.minClusterPos);
				cmd.SetComputeVectorParam(lightClusterBuildCS, HDShaderIDs._MaxClusterPos, this.maxClusterPos);
				cmd.SetComputeVectorParam(lightClusterBuildCS, HDRaytracingLightCluster._ClusterCellSize, this.clusterCellSize);
				cmd.SetComputeFloatParam(lightClusterBuildCS, HDShaderIDs._LightPerCellCount, HDShadowUtils.Asfloat(this.numLightsPerCell));
				cmd.SetComputeBufferParam(lightClusterBuildCS, num2, HDRaytracingLightCluster._LightVolumes, this.m_LightVolumeGPUArray);
				cmd.SetComputeFloatParam(lightClusterBuildCS, HDRaytracingLightCluster._LightVolumeCount, HDShadowUtils.Asfloat(this.totalLightCount));
				cmd.SetComputeBufferParam(lightClusterBuildCS, num2, HDRaytracingLightCluster._RaytracingLightCullResult, this.m_LightCullResult);
				int num3 = 8;
				int num4 = 8;
				int num5 = 4;
				cmd.DispatchCompute(lightClusterBuildCS, num2, num3, num4, num5);
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00046C1C File Offset: 0x00044E1C
		private void BuildLightData(CommandBuffer cmd, HDCamera hdCamera, HDRayTracingLights rayTracingLights)
		{
			if (rayTracingLights.lightCount == 0)
			{
				this.ResizeLightDataBuffer(1);
				return;
			}
			if (this.m_LightDataGPUArray == null || this.m_LightDataGPUArray.count != rayTracingLights.lightCount)
			{
				this.ResizeLightDataBuffer(rayTracingLights.lightCount);
			}
			this.m_LightDataCPUArray.Clear();
			for (int i = 0; i < rayTracingLights.hdLightArray.Count; i++)
			{
				LightData lightData = default(LightData);
				HDAdditionalLightData hdadditionalLightData = rayTracingLights.hdLightArray[i];
				if (hdadditionalLightData == null)
				{
					this.m_LightDataCPUArray.Add(lightData);
				}
				else
				{
					Light component = hdadditionalLightData.gameObject.GetComponent<Light>();
					float num = HDUtils.ComputeLinearDistanceFade((component.gameObject.transform.position - hdCamera.camera.transform.position).magnitude, hdadditionalLightData.fadeDistance);
					if (((hdadditionalLightData.lightDimmer > 0f && (hdadditionalLightData.affectDiffuse || hdadditionalLightData.affectSpecular)) || hdadditionalLightData.volumetricDimmer > 0f) && num > 0f)
					{
						lightData.lightLayers = hdadditionalLightData.GetLightLayers();
						LightCategory lightCategory = LightCategory.Count;
						GPULightType gpulightType = GPULightType.Point;
						LightVolumeType lightVolumeType = LightVolumeType.Count;
						HDLightType type = hdadditionalLightData.type;
						HDRenderPipeline.EvaluateGPULightType(type, hdadditionalLightData.spotLightShape, hdadditionalLightData.areaLightShape, ref lightCategory, ref gpulightType, ref lightVolumeType);
						lightData.lightType = gpulightType;
						lightData.positionRWS = component.gameObject.transform.position;
						bool flag = hdadditionalLightData.applyRangeAttenuation && gpulightType != GPULightType.ProjectorBox;
						lightData.range = component.range;
						if (flag)
						{
							lightData.rangeAttenuationScale = 1f / (component.range * component.range);
							lightData.rangeAttenuationBias = 1f;
							if (lightData.lightType == GPULightType.Rectangle)
							{
								lightData.rangeAttenuationScale = 1f;
							}
						}
						else
						{
							lightData.rangeAttenuationScale = 4096f / (component.range * component.range);
							lightData.rangeAttenuationBias = 16777216f;
							if (lightData.lightType == GPULightType.Rectangle)
							{
								lightData.rangeAttenuationScale = 4096f;
							}
						}
						Color color = component.color.linear * component.intensity;
						if (hdadditionalLightData.useColorTemperature)
						{
							color *= Mathf.CorrelatedColorTemperatureToRGB(component.colorTemperature);
						}
						lightData.color = new Vector3(color.r, color.g, color.b);
						lightData.forward = component.transform.forward;
						lightData.up = component.transform.up;
						lightData.right = component.transform.right;
						lightData.boxLightSafeExtent = 1f;
						if (lightData.lightType == GPULightType.ProjectorBox)
						{
							lightData.right *= 2f / Mathf.Max(hdadditionalLightData.shapeWidth, 0.001f);
							lightData.up *= 2f / Mathf.Max(hdadditionalLightData.shapeHeight, 0.001f);
						}
						else if (lightData.lightType == GPULightType.ProjectorPyramid)
						{
							float spotAngle = component.spotAngle;
							float num2;
							float num3;
							if (hdadditionalLightData.aspectRatio >= 1f)
							{
								num2 = 2f * Mathf.Tan(spotAngle * 0.5f * 0.017453292f);
								num3 = num2 * hdadditionalLightData.aspectRatio;
							}
							else
							{
								num3 = 2f * Mathf.Tan(spotAngle * 0.5f * 0.017453292f);
								num2 = num3 / hdadditionalLightData.aspectRatio;
							}
							lightData.right *= 2f / num3;
							lightData.up *= 2f / num2;
						}
						if (lightData.lightType == GPULightType.Spot)
						{
							float spotAngle2 = component.spotAngle;
							float innerSpotPercent = hdadditionalLightData.innerSpotPercent01;
							float num4 = Mathf.Clamp(Mathf.Cos(spotAngle2 * 0.5f * 0.017453292f), 0f, 1f);
							float num5 = Mathf.Sqrt(1f - num4 * num4);
							float num6 = Mathf.Clamp(Mathf.Cos(spotAngle2 * 0.5f * innerSpotPercent * 0.017453292f), 0f, 1f);
							float num7 = Mathf.Max(0.0001f, num6 - num4);
							lightData.angleScale = 1f / num7;
							lightData.angleOffset = -num4 * lightData.angleScale;
							float num8 = num4 / num5;
							lightData.up *= num8;
							lightData.right *= num8;
						}
						else
						{
							lightData.angleScale = 0f;
							lightData.angleOffset = 1f;
						}
						if (lightData.lightType != GPULightType.Directional && lightData.lightType != GPULightType.ProjectorBox)
						{
							lightData.size = new Vector2(hdadditionalLightData.shapeRadius * hdadditionalLightData.shapeRadius, 0f);
						}
						if (lightData.lightType == GPULightType.Rectangle || lightData.lightType == GPULightType.Tube)
						{
							lightData.size = new Vector2(hdadditionalLightData.shapeWidth, hdadditionalLightData.shapeHeight);
						}
						lightData.lightDimmer = num * hdadditionalLightData.lightDimmer;
						lightData.diffuseDimmer = num * (hdadditionalLightData.affectDiffuse ? hdadditionalLightData.lightDimmer : 0f);
						lightData.specularDimmer = num * (hdadditionalLightData.affectSpecular ? (hdadditionalLightData.lightDimmer * hdCamera.frameSettings.specularGlobalDimmer) : 0f);
						lightData.volumetricLightDimmer = num * hdadditionalLightData.volumetricDimmer;
						lightData.cookieMode = CookieMode.None;
						lightData.contactShadowMask = 0;
						lightData.cookieIndex = -1;
						lightData.shadowIndex = -1;
						lightData.screenSpaceShadowIndex = -1;
						if (component != null && component.cookie != null)
						{
							if (type != HDLightType.Spot)
							{
								if (type == HDLightType.Point)
								{
									lightData.cookieMode = CookieMode.Clamp;
									lightData.cookieIndex = this.m_RenderPipeline.m_TextureCaches.lightCookieManager.FetchCubeCookie(cmd, component.cookie);
								}
							}
							else
							{
								lightData.cookieMode = ((hdadditionalLightData.legacyLight.cookie.wrapMode == TextureWrapMode.Repeat) ? CookieMode.Repeat : CookieMode.Clamp);
								lightData.cookieScaleOffset = this.m_RenderPipeline.m_TextureCaches.lightCookieManager.Fetch2DCookie(cmd, component.cookie);
							}
						}
						else if (type == HDLightType.Spot && hdadditionalLightData.spotLightShape != SpotLightShape.Cone)
						{
							lightData.cookieMode = CookieMode.Clamp;
							lightData.cookieScaleOffset = this.m_RenderPipeline.m_TextureCaches.lightCookieManager.Fetch2DCookie(cmd, Texture2D.whiteTexture);
						}
						else if (lightData.lightType == GPULightType.Rectangle && hdadditionalLightData.areaLightCookie != null)
						{
							lightData.cookieMode = CookieMode.Clamp;
							lightData.cookieScaleOffset = this.m_RenderPipeline.m_TextureCaches.lightCookieManager.FetchAreaCookie(cmd, hdadditionalLightData.areaLightCookie);
						}
						lightData.shadowDimmer = 1f;
						lightData.volumetricShadowDimmer = 1f;
						lightData.shadowIndex = hdadditionalLightData.shadowIndex;
						lightData.minRoughness = (1f - hdadditionalLightData.maxSmoothness) * (1f - hdadditionalLightData.maxSmoothness);
						lightData.shadowMaskSelector = Vector4.zero;
						lightData.shadowMaskSelector.x = -1f;
						lightData.nonLightMappedOnly = 0;
						if (ShaderConfig.s_CameraRelativeRendering != 0)
						{
							Vector3 worldSpaceCameraPos = hdCamera.mainViewConstants.worldSpaceCameraPos;
							lightData.positionRWS -= worldSpaceCameraPos;
						}
						this.m_LightDataCPUArray.Add(lightData);
					}
				}
			}
			this.m_LightDataGPUArray.SetData<LightData>(this.m_LightDataCPUArray);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0004739C File Offset: 0x0004559C
		private void BuildEnvLightData(CommandBuffer cmd, HDCamera hdCamera, HDRayTracingLights lights)
		{
			int count = lights.reflectionProbeArray.Count;
			if (count == 0)
			{
				this.ResizeEnvLightDataBuffer(1);
				return;
			}
			if (this.m_EnvLightDataCPUArray == null || this.m_EnvLightDataGPUArray == null || this.m_EnvLightDataGPUArray.count != count)
			{
				this.ResizeEnvLightDataBuffer(count);
			}
			this.m_EnvLightDataCPUArray.Clear();
			ProcessedProbeData processedProbeData = default(ProcessedProbeData);
			for (int i = 0; i < lights.reflectionProbeArray.Count; i++)
			{
				HDProbe hdprobe = lights.reflectionProbeArray[i];
				if (hdprobe.HasValidRenderedData())
				{
					HDRenderPipeline.PreprocessProbeData(ref processedProbeData, hdprobe, hdCamera);
					EnvLightData envLightData = default(EnvLightData);
					this.m_RenderPipeline.GetEnvLightData(cmd, hdCamera, in processedProbeData, this.m_RenderPipeline.m_CurrentDebugDisplaySettings, ref envLightData);
					Vector3 worldSpaceCameraPos = hdCamera.mainViewConstants.worldSpaceCameraPos;
					this.m_RenderPipeline.UpdateEnvLighCameraRelativetData(ref envLightData, worldSpaceCameraPos);
					this.m_EnvLightDataCPUArray.Add(envLightData);
				}
			}
			this.m_EnvLightDataGPUArray.SetData<EnvLightData>(this.m_EnvLightDataCPUArray);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0004748C File Offset: 0x0004568C
		public void EvaluateClusterDebugView(CommandBuffer cmd, HDCamera hdCamera)
		{
			ComputeShader lightClusterDebugCS = this.m_RenderPipelineRayTracingResources.lightClusterDebugCS;
			if (lightClusterDebugCS == null)
			{
				return;
			}
			CoreUtils.SetRenderTarget(cmd, this.m_DebugLightClusterTexture, this.m_RenderPipeline.sharedRTManager.GetDepthStencilBuffer(false), ClearFlag.Color, Color.black, 0, CubemapFace.Unknown, -1);
			int num = lightClusterDebugCS.FindKernel("DebugLightCluster");
			cmd.SetComputeBufferParam(lightClusterDebugCS, num, HDShaderIDs._RaytracingLightCluster, this.m_LightCluster);
			cmd.SetComputeVectorParam(lightClusterDebugCS, HDShaderIDs._MinClusterPos, this.minClusterPos);
			cmd.SetComputeVectorParam(lightClusterDebugCS, HDShaderIDs._MaxClusterPos, this.maxClusterPos);
			cmd.SetComputeVectorParam(lightClusterDebugCS, HDRaytracingLightCluster._ClusterCellSize, this.clusterCellSize);
			cmd.SetComputeIntParam(lightClusterDebugCS, HDShaderIDs._LightPerCellCount, this.numLightsPerCell);
			cmd.SetComputeTextureParam(lightClusterDebugCS, num, HDShaderIDs._CameraDepthTexture, this.m_RenderPipeline.sharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetComputeTextureParam(lightClusterDebugCS, num, HDRaytracingLightCluster._DebutLightClusterTexture, this.m_DebugLightClusterTexture);
			int actualWidth = hdCamera.actualWidth;
			int actualHeight = hdCamera.actualHeight;
			int num2 = 8;
			int num3 = (actualWidth + (num2 - 1)) / num2;
			int num4 = (actualHeight + (num2 - 1)) / num2;
			cmd.DispatchCompute(lightClusterDebugCS, num, num3, num4, 1);
			this.m_DebugMaterialProperties.SetBuffer(HDShaderIDs._RaytracingLightCluster, this.m_LightCluster);
			this.m_DebugMaterialProperties.SetVector(HDShaderIDs._MinClusterPos, this.minClusterPos);
			this.m_DebugMaterialProperties.SetVector(HDShaderIDs._MaxClusterPos, this.maxClusterPos);
			this.m_DebugMaterialProperties.SetVector(HDRaytracingLightCluster._ClusterCellSize, this.clusterCellSize);
			this.m_DebugMaterialProperties.SetInt(HDShaderIDs._LightPerCellCount, this.numLightsPerCell);
			this.m_DebugMaterialProperties.SetTexture(HDShaderIDs._CameraDepthTexture, this.m_RenderPipeline.sharedRTManager.GetDepthTexture(false));
			cmd.DrawProcedural(Matrix4x4.identity, this.m_DebugMaterial, 1, MeshTopology.Lines, 48, 131072, this.m_DebugMaterialProperties);
			cmd.DrawProcedural(Matrix4x4.identity, this.m_DebugMaterial, 0, MeshTopology.Triangles, 36, 131072, this.m_DebugMaterialProperties);
			(RenderPipelineManager.currentPipeline as HDRenderPipeline).PushFullScreenDebugTexture(hdCamera, cmd, this.m_DebugLightClusterTexture, FullScreenDebugMode.LightCluster);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000476AF File Offset: 0x000458AF
		public ComputeBuffer GetCluster()
		{
			return this.m_LightCluster;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000476B7 File Offset: 0x000458B7
		public ComputeBuffer GetLightDatas()
		{
			return this.m_LightDataGPUArray;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000476BF File Offset: 0x000458BF
		public ComputeBuffer GetEnvLightDatas()
		{
			return this.m_EnvLightDataGPUArray;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x000476C7 File Offset: 0x000458C7
		public Vector3 GetMinClusterPos()
		{
			return this.minClusterPos;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x000476CF File Offset: 0x000458CF
		public Vector3 GetMaxClusterPos()
		{
			return this.maxClusterPos;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x000476D7 File Offset: 0x000458D7
		public Vector3 GetClusterCellSize()
		{
			return this.clusterCellSize;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x000476DF File Offset: 0x000458DF
		public int GetPunctualLightCount()
		{
			return this.punctualLightCount;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x000476E7 File Offset: 0x000458E7
		public int GetAreaLightCount()
		{
			return this.areaLightCount;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x000476EF File Offset: 0x000458EF
		public int GetEnvLightCount()
		{
			return this.envLightCount;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000476F8 File Offset: 0x000458F8
		private void InvalidateCluster()
		{
			this.minClusterPos.Set(float.MaxValue, float.MaxValue, float.MaxValue);
			this.maxClusterPos.Set(float.MinValue, float.MinValue, float.MinValue);
			this.punctualLightCount = 0;
			this.areaLightCount = 0;
			if (this.m_LightCluster.count != 1)
			{
				this.ResizeClusterBuffer(1);
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0004775C File Offset: 0x0004595C
		public void EvaluateLightClusters(CommandBuffer cmd, HDCamera hdCamera, HDRayTracingLights rayTracingLights)
		{
			if (rayTracingLights.lightCount == 0 || !this.m_RenderPipeline.GetRayTracingState())
			{
				this.InvalidateCluster();
				return;
			}
			this.BuildGPULightVolumes(rayTracingLights);
			if (this.totalLightCount == 0)
			{
				this.InvalidateCluster();
				return;
			}
			this.EvaluateClusterVolume(hdCamera);
			this.CullLights(cmd);
			this.BuildLightCluster(hdCamera, cmd);
			this.BuildLightData(cmd, hdCamera, rayTracingLights);
			this.BuildEnvLightData(cmd, hdCamera, rayTracingLights);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000477C4 File Offset: 0x000459C4
		public void BindLightClusterData(CommandBuffer cmd)
		{
			cmd.SetGlobalBuffer(HDShaderIDs._RaytracingLightCluster, this.GetCluster());
			cmd.SetGlobalBuffer(HDShaderIDs._LightDatasRT, this.GetLightDatas());
			cmd.SetGlobalBuffer(HDShaderIDs._EnvLightDatasRT, this.GetEnvLightDatas());
			cmd.SetGlobalVector(HDShaderIDs._MinClusterPos, this.GetMinClusterPos());
			cmd.SetGlobalVector(HDShaderIDs._MaxClusterPos, this.GetMaxClusterPos());
			cmd.SetGlobalInt(HDShaderIDs._LightPerCellCount, this.numLightsPerCell);
			cmd.SetGlobalInt(HDShaderIDs._PunctualLightCountRT, this.GetPunctualLightCount());
			cmd.SetGlobalInt(HDShaderIDs._AreaLightCountRT, this.GetAreaLightCount());
			cmd.SetGlobalInt(HDShaderIDs._EnvLightCountRT, this.GetEnvLightCount());
		}

		// Token: 0x04000D27 RID: 3367
		private RenderPipelineResources m_RenderPipelineResources;

		// Token: 0x04000D28 RID: 3368
		private HDRenderPipelineRayTracingResources m_RenderPipelineRayTracingResources;

		// Token: 0x04000D29 RID: 3369
		private HDRenderPipeline m_RenderPipeline;

		// Token: 0x04000D2A RID: 3370
		private LightVolume[] m_LightVolumesCPUArray;

		// Token: 0x04000D2B RID: 3371
		private ComputeBuffer m_LightVolumeGPUArray;

		// Token: 0x04000D2C RID: 3372
		private ComputeBuffer m_LightCullResult;

		// Token: 0x04000D2D RID: 3373
		private ComputeBuffer m_LightCluster;

		// Token: 0x04000D2E RID: 3374
		private List<LightData> m_LightDataCPUArray = new List<LightData>();

		// Token: 0x04000D2F RID: 3375
		private ComputeBuffer m_LightDataGPUArray;

		// Token: 0x04000D30 RID: 3376
		private List<EnvLightData> m_EnvLightDataCPUArray = new List<EnvLightData>();

		// Token: 0x04000D31 RID: 3377
		private ComputeBuffer m_EnvLightDataGPUArray;

		// Token: 0x04000D32 RID: 3378
		private RTHandle m_DebugLightClusterTexture;

		// Token: 0x04000D33 RID: 3379
		private Material m_DebugMaterial;

		// Token: 0x04000D34 RID: 3380
		private MaterialPropertyBlock m_DebugMaterialProperties = new MaterialPropertyBlock();

		// Token: 0x04000D35 RID: 3381
		private const string m_LightClusterKernelName = "RaytracingLightCluster";

		// Token: 0x04000D36 RID: 3382
		private const string m_LightCullKernelName = "RaytracingLightCull";

		// Token: 0x04000D37 RID: 3383
		public static readonly int _ClusterCellSize = Shader.PropertyToID("_ClusterCellSize");

		// Token: 0x04000D38 RID: 3384
		public static readonly int _LightVolumes = Shader.PropertyToID("_LightVolumes");

		// Token: 0x04000D39 RID: 3385
		public static readonly int _LightVolumeCount = Shader.PropertyToID("_LightVolumeCount");

		// Token: 0x04000D3A RID: 3386
		public static readonly int _DebugColorGradientTexture = Shader.PropertyToID("_DebugColorGradientTexture");

		// Token: 0x04000D3B RID: 3387
		public static readonly int _DebutLightClusterTexture = Shader.PropertyToID("_DebutLightClusterTexture");

		// Token: 0x04000D3C RID: 3388
		public static readonly int _RaytracingLightCullResult = Shader.PropertyToID("_RaytracingLightCullResult");

		// Token: 0x04000D3D RID: 3389
		public static readonly int _ClusterCenterPosition = Shader.PropertyToID("_ClusterCenterPosition");

		// Token: 0x04000D3E RID: 3390
		public static readonly int _ClusterDimension = Shader.PropertyToID("_ClusterDimension");

		// Token: 0x04000D3F RID: 3391
		private Vector3 minClusterPos = new Vector3(0f, 0f, 0f);

		// Token: 0x04000D40 RID: 3392
		private Vector3 maxClusterPos = new Vector3(0f, 0f, 0f);

		// Token: 0x04000D41 RID: 3393
		private Vector3 clusterCellSize = new Vector3(0f, 0f, 0f);

		// Token: 0x04000D42 RID: 3394
		private Vector3 clusterCenter = new Vector3(0f, 0f, 0f);

		// Token: 0x04000D43 RID: 3395
		private Vector3 clusterDimension = new Vector3(0f, 0f, 0f);

		// Token: 0x04000D44 RID: 3396
		private int punctualLightCount;

		// Token: 0x04000D45 RID: 3397
		private int areaLightCount;

		// Token: 0x04000D46 RID: 3398
		private int envLightCount;

		// Token: 0x04000D47 RID: 3399
		private int totalLightCount;

		// Token: 0x04000D48 RID: 3400
		private int numLightsPerCell;
	}
}
