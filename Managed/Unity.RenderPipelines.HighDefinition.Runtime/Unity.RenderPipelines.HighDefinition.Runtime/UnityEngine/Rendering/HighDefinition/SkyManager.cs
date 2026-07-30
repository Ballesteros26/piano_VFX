using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000161 RID: 353
	internal class SkyManager
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00050C58 File Offset: 0x0004EE58
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x00050C60 File Offset: 0x0004EE60
		public VolumeStack lightingOverrideVolumeStack { get; private set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00050C69 File Offset: 0x0004EE69
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x00050C71 File Offset: 0x0004EE71
		public LayerMask lightingOverrideLayerMask { get; private set; } = -1;

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00050C7A File Offset: 0x0004EE7A
		public static Dictionary<int, Type> skyTypesDict
		{
			get
			{
				if (SkyManager.m_SkyTypesDict == null)
				{
					SkyManager.UpdateSkyTypes();
				}
				return SkyManager.m_SkyTypesDict;
			}
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00050D10 File Offset: 0x0004EF10
		~SkyManager()
		{
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00050D38 File Offset: 0x0004EF38
		internal static SkySettings GetSkySetting(VolumeStack stack)
		{
			int value = stack.GetComponent<VisualEnvironment>().skyType.value;
			Type type;
			if (SkyManager.skyTypesDict.TryGetValue(value, out type))
			{
				return (SkySettings)stack.GetComponent(type);
			}
			if (value == 2 && SkyManager.logOnce)
			{
				Debug.LogError("You are using the deprecated Procedural Sky in your Scene. You can still use it but, to do so, you must install it separately. To do this, open the Package Manager window and import the 'Procedural Sky' sample from the HDRP package page, then close and re-open your project without saving.");
				SkyManager.logOnce = false;
			}
			return null;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00050D90 File Offset: 0x0004EF90
		private static void UpdateSkyTypes()
		{
			if (SkyManager.m_SkyTypesDict == null)
			{
				SkyManager.m_SkyTypesDict = new Dictionary<int, Type>();
				foreach (Type type in from t in CoreUtils.GetAllTypesDerivedFrom<SkySettings>()
					where !t.IsAbstract
					select t)
				{
					object[] customAttributes = type.GetCustomAttributes(typeof(SkyUniqueID), false);
					if (customAttributes.Length == 0)
					{
						Debug.LogWarningFormat("Missing attribute SkyUniqueID on class {0}. Class won't be registered as an available sky.", new object[] { type });
					}
					else
					{
						int uniqueID = ((SkyUniqueID)customAttributes[0]).uniqueID;
						Type type2;
						if (uniqueID == 0)
						{
							Debug.LogWarningFormat("0 is a reserved SkyUniqueID and is used in class {0}. Class won't be registered as an available sky.", new object[] { type });
						}
						else if (SkyManager.m_SkyTypesDict.TryGetValue(uniqueID, out type2))
						{
							Debug.LogWarningFormat("SkyUniqueID {0} used in class {1} is already used in class {2}. Class won't be registered as an available sky.", new object[] { uniqueID, type, type2 });
						}
						else
						{
							SkyManager.m_SkyTypesDict.Add(uniqueID, type);
						}
					}
				}
			}
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00050EA8 File Offset: 0x0004F0A8
		public void UpdateCurrentSkySettings(HDCamera hdCamera)
		{
			hdCamera.UpdateCurrentSky(this);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00050EB4 File Offset: 0x0004F0B4
		public void SetGlobalSkyData(CommandBuffer cmd, HDCamera hdCamera)
		{
			if (this.IsCachedContextValid(hdCamera.lightingSky))
			{
				SkyRenderer skyRenderer = hdCamera.lightingSky.skyRenderer;
				if (skyRenderer != null)
				{
					this.m_BuiltinParameters.skySettings = hdCamera.lightingSky.skySettings;
					skyRenderer.SetGlobalSkyData(cmd, this.m_BuiltinParameters);
				}
			}
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00050F04 File Offset: 0x0004F104
		public void Build(HDRenderPipelineAsset hdAsset, RenderPipelineResources defaultResources, IBLFilterBSDF[] iblFilterBSDFArray)
		{
			HDRenderPipelineAsset defaultAsset = HDRenderPipeline.defaultAsset;
			this.m_Resolution = (int)hdAsset.currentPlatformRenderPipelineSettings.lightLoopSettings.skyReflectionSize;
			this.m_IBLFilterArray = iblFilterBSDFArray;
			this.m_StandardSkyboxMaterial = CoreUtils.CreateEngineMaterial(defaultResources.shaders.skyboxCubemapPS);
			this.m_BlitCubemapMaterial = CoreUtils.CreateEngineMaterial(defaultResources.shaders.blitCubemapPS);
			this.m_OpaqueAtmScatteringMaterial = CoreUtils.CreateEngineMaterial(defaultResources.shaders.opaqueAtmosphericScatteringPS);
			this.m_OpaqueAtmScatteringBlock = new MaterialPropertyBlock();
			this.m_ComputeAmbientProbeCS = defaultAsset.renderPipelineResources.shaders.ambientProbeConvolutionCS;
			this.m_ComputeAmbientProbeKernel = this.m_ComputeAmbientProbeCS.FindKernel("AmbientProbeConvolution");
			this.lightingOverrideVolumeStack = VolumeManager.instance.CreateStack();
			this.lightingOverrideLayerMask = hdAsset.currentPlatformRenderPipelineSettings.lightLoopSettings.skyLightingOverrideLayerMask;
			int skyReflectionSize = (int)hdAsset.currentPlatformRenderPipelineSettings.lightLoopSettings.skyReflectionSize;
			this.m_SkyboxBSDFCubemapIntermediate = RTHandles.Alloc(skyReflectionSize, skyReflectionSize, 1, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Trilinear, TextureWrapMode.Repeat, TextureDimension.Cube, false, true, false, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, "SkyboxBSDFIntermediate");
			this.m_CubemapScreenSize = new Vector4((float)skyReflectionSize, (float)skyReflectionSize, 1f / (float)skyReflectionSize, 1f / (float)skyReflectionSize);
			Matrix4x4.Perspective(90f, 1f, 0.01f, 1f);
			for (int i = 0; i < 6; i++)
			{
				Matrix4x4 matrix4x = Matrix4x4.LookAt(Vector3.zero, CoreUtils.lookAtList[i], CoreUtils.upVectorList[i]) * Matrix4x4.Scale(new Vector3(1f, 1f, -1f));
				this.m_facePixelCoordToViewDirMatrices[i] = HDUtils.ComputePixelCoordToWorldSpaceViewDirectionMatrix(1.5707964f, Vector2.zero, this.m_CubemapScreenSize, matrix4x, true, -1f);
				this.m_CameraRelativeViewMatrices[i] = matrix4x;
			}
			this.InitializeBlackCubemapArray();
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x000510CC File Offset: 0x0004F2CC
		private void InitializeBlackCubemapArray()
		{
			if (this.m_BlackCubemapArray == null)
			{
				this.m_BlackCubemapArray = new CubemapArray(1, this.m_IBLFilterArray.Length, TextureFormat.RGBA32, false)
				{
					hideFlags = HideFlags.HideAndDontSave,
					wrapMode = TextureWrapMode.Repeat,
					wrapModeV = TextureWrapMode.Clamp,
					filterMode = FilterMode.Trilinear,
					anisoLevel = 0,
					name = "BlackCubemapArray"
				};
				Color32[] array = new Color32[]
				{
					new Color32(0, 0, 0, 0)
				};
				for (int i = 0; i < this.m_IBLFilterArray.Length; i++)
				{
					for (int j = 0; j < 6; j++)
					{
						this.m_BlackCubemapArray.SetPixels32(array, (CubemapFace)j, i);
					}
				}
				this.m_BlackCubemapArray.Apply();
			}
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00051180 File Offset: 0x0004F380
		public void Cleanup()
		{
			CoreUtils.Destroy(this.m_StandardSkyboxMaterial);
			CoreUtils.Destroy(this.m_BlitCubemapMaterial);
			CoreUtils.Destroy(this.m_OpaqueAtmScatteringMaterial);
			RTHandles.Release(this.m_SkyboxBSDFCubemapIntermediate);
			CoreUtils.Destroy(this.m_BlackCubemapArray);
			for (int i = 0; i < this.m_CachedSkyContexts.size; i++)
			{
				this.m_CachedSkyContexts[i].Cleanup();
			}
			this.m_StaticLightingSky.Cleanup();
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000511F6 File Offset: 0x0004F3F6
		public bool IsLightingSkyValid(HDCamera hdCamera)
		{
			return hdCamera.lightingSky.IsValid();
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00051203 File Offset: 0x0004F403
		public bool IsVisualSkyValid(HDCamera hdCamera)
		{
			return hdCamera.visualSky.IsValid();
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00051210 File Offset: 0x0004F410
		private SphericalHarmonicsL2 GetAmbientProbe(SkyUpdateContext skyContext)
		{
			if (skyContext.IsValid() && this.IsCachedContextValid(skyContext))
			{
				return this.m_CachedSkyContexts[skyContext.cachedSkyRenderingContextId].renderingContext.ambientProbe;
			}
			return this.m_BlackAmbientProbe;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00051245 File Offset: 0x0004F445
		private Texture GetSkyCubemap(SkyUpdateContext skyContext)
		{
			if (skyContext.IsValid() && this.IsCachedContextValid(skyContext))
			{
				return this.m_CachedSkyContexts[skyContext.cachedSkyRenderingContextId].renderingContext.skyboxCubemapRT;
			}
			return CoreUtils.blackCubeTexture;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0005127E File Offset: 0x0004F47E
		private Texture GetReflectionTexture(SkyUpdateContext skyContext)
		{
			if (skyContext.IsValid() && this.IsCachedContextValid(skyContext))
			{
				return this.m_CachedSkyContexts[skyContext.cachedSkyRenderingContextId].renderingContext.skyboxBSDFCubemapArray;
			}
			return this.m_BlackCubemapArray;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000512B3 File Offset: 0x0004F4B3
		public Texture GetSkyReflection(HDCamera hdCamera)
		{
			return this.GetReflectionTexture(hdCamera.lightingSky);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x000512C1 File Offset: 0x0004F4C1
		internal SphericalHarmonicsL2 GetAmbientProbe(HDCamera hdCamera)
		{
			if (hdCamera.lightingSky == null && hdCamera.skyAmbientMode == SkyAmbientMode.Dynamic)
			{
				return this.m_BlackAmbientProbe;
			}
			if (hdCamera.skyAmbientMode == SkyAmbientMode.Static)
			{
				return this.GetAmbientProbe(this.m_StaticLightingSky);
			}
			return this.GetAmbientProbe(hdCamera.lightingSky);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x000512FC File Offset: 0x0004F4FC
		internal void SetupAmbientProbe(HDCamera hdCamera)
		{
			RenderSettings.ambientMode = AmbientMode.Custom;
			RenderSettings.ambientProbe = this.GetAmbientProbe(hdCamera);
			if (hdCamera.lightingSky == null && hdCamera.skyAmbientMode == SkyAmbientMode.Dynamic)
			{
				return;
			}
			bool flag = true;
			this.m_StandardSkyboxMaterial.SetTexture("_Tex", this.GetSkyCubemap((hdCamera.skyAmbientMode > SkyAmbientMode.Static && flag) ? hdCamera.lightingSky : this.m_StaticLightingSky));
			RenderSettings.skybox = this.m_StandardSkyboxMaterial;
			RenderSettings.ambientIntensity = 1f;
			RenderSettings.ambientMode = AmbientMode.Skybox;
			RenderSettings.reflectionIntensity = 1f;
			RenderSettings.customReflection = null;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0005138C File Offset: 0x0004F58C
		private void BlitCubemap(CommandBuffer cmd, Cubemap source, RenderTexture dest)
		{
			MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
			for (int i = 0; i < 6; i++)
			{
				CoreUtils.SetRenderTarget(cmd, dest, ClearFlag.None, 0, (CubemapFace)i, -1);
				materialPropertyBlock.SetTexture("_MainTex", source);
				materialPropertyBlock.SetFloat("_faceIndex", (float)i);
				cmd.DrawProcedural(Matrix4x4.identity, this.m_BlitCubemapMaterial, 0, MeshTopology.Triangles, 3, 1, materialPropertyBlock);
			}
			cmd.GenerateMips(dest);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x000513F4 File Offset: 0x0004F5F4
		private void RenderSkyToCubemap(SkyUpdateContext skyContext)
		{
			using (new ProfilingScope(this.m_BuiltinParameters.commandBuffer, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderSkyToCubemap)))
			{
				SkyRenderingContext renderingContext = this.m_CachedSkyContexts[skyContext.cachedSkyRenderingContextId].renderingContext;
				SkyRenderer skyRenderer = skyContext.skyRenderer;
				for (int i = 0; i < 6; i++)
				{
					this.m_BuiltinParameters.pixelCoordToViewDirMatrix = this.m_facePixelCoordToViewDirMatrices[i];
					this.m_BuiltinParameters.viewMatrix = this.m_CameraRelativeViewMatrices[i];
					this.m_BuiltinParameters.colorBuffer = renderingContext.skyboxCubemapRT;
					this.m_BuiltinParameters.depthBuffer = null;
					CoreUtils.SetRenderTarget(this.m_BuiltinParameters.commandBuffer, renderingContext.skyboxCubemapRT, ClearFlag.None, 0, (CubemapFace)i, -1);
					skyRenderer.RenderSky(this.m_BuiltinParameters, true, skyContext.skySettings.includeSunInBaking.value);
				}
				this.m_BuiltinParameters.commandBuffer.GenerateMips(renderingContext.skyboxCubemapRT);
			}
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00051508 File Offset: 0x0004F708
		private void RenderCubemapGGXConvolution(SkyUpdateContext skyContext)
		{
			using (new ProfilingScope(this.m_BuiltinParameters.commandBuffer, ProfilingSampler.Get<HDProfileId>(HDProfileId.UpdateSkyEnvironmentConvolution)))
			{
				SkyRenderingContext renderingContext = this.m_CachedSkyContexts[skyContext.cachedSkyRenderingContextId].renderingContext;
				SkyRenderer skyRenderer = skyContext.skyRenderer;
				for (int i = 0; i < this.m_IBLFilterArray.Length; i++)
				{
					this.m_IBLFilterArray[i].FilterCubemap(this.m_BuiltinParameters.commandBuffer, renderingContext.skyboxCubemapRT, this.m_SkyboxBSDFCubemapIntermediate);
					for (int j = 0; j < 6; j++)
					{
						this.m_BuiltinParameters.commandBuffer.CopyTexture(this.m_SkyboxBSDFCubemapIntermediate, j, renderingContext.skyboxBSDFCubemapArray, 6 * i + j);
					}
				}
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000515E8 File Offset: 0x0004F7E8
		private int GetSunLightHashCode(Light light)
		{
			HDAdditionalLightData component = light.GetComponent<HDAdditionalLightData>();
			int num = 13;
			num = num * 23 + light.transform.position.GetHashCode();
			num = num * 23 + light.transform.rotation.GetHashCode();
			num = num * 23 + light.color.GetHashCode();
			num = num * 23 + light.colorTemperature.GetHashCode();
			num = num * 23 + light.intensity.GetHashCode();
			if (component != null)
			{
				num = num * 23 + component.lightDimmer.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x000516A4 File Offset: 0x0004F8A4
		private void AllocateNewRenderingContext(SkyUpdateContext skyContext, int slot, int newHash, bool supportConvolution, in SphericalHarmonicsL2 previousAmbientProbe)
		{
			ref CachedSkyContext ptr = ref this.m_CachedSkyContexts[slot];
			ptr.hash = newHash;
			ptr.refCount = 1;
			ptr.type = skyContext.skySettings.GetSkyRendererType();
			if (ptr.renderingContext != null && ptr.renderingContext.supportsConvolution != supportConvolution)
			{
				ptr.renderingContext.Cleanup();
				ptr.renderingContext = null;
			}
			if (ptr.renderingContext == null)
			{
				ptr.renderingContext = new SkyRenderingContext(this.m_Resolution, this.m_IBLFilterArray.Length, supportConvolution, previousAmbientProbe);
			}
			else
			{
				ptr.renderingContext.UpdateAmbientProbe(in previousAmbientProbe);
			}
			skyContext.cachedSkyRenderingContextId = slot;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00051748 File Offset: 0x0004F948
		private bool AcquireSkyRenderingContext(SkyUpdateContext updateContext, int newHash, bool supportConvolution = true)
		{
			SphericalHarmonicsL2 sphericalHarmonicsL = default(SphericalHarmonicsL2);
			if (this.IsCachedContextValid(updateContext))
			{
				ref CachedSkyContext ptr = ref this.m_CachedSkyContexts[updateContext.cachedSkyRenderingContextId];
				if (newHash == ptr.hash && !(updateContext.skySettings.GetSkyRendererType() != ptr.type))
				{
					return false;
				}
				if (updateContext.skySettings.GetSkyRendererType() == ptr.type)
				{
					sphericalHarmonicsL = ptr.renderingContext.ambientProbe;
				}
				this.ReleaseCachedContext(updateContext.cachedSkyRenderingContextId);
			}
			int num = -1;
			for (int i = 0; i < this.m_CachedSkyContexts.size; i++)
			{
				if (this.m_CachedSkyContexts[i].hash == newHash)
				{
					this.m_CachedSkyContexts[i].refCount++;
					updateContext.cachedSkyRenderingContextId = i;
					updateContext.skyParametersHash = newHash;
					return false;
				}
				if (num == -1 && this.m_CachedSkyContexts[i].hash == 0)
				{
					num = i;
				}
			}
			if (num != -1)
			{
				this.AllocateNewRenderingContext(updateContext, num, newHash, supportConvolution, in sphericalHarmonicsL);
			}
			else
			{
				DynamicArray<CachedSkyContext> cachedSkyContexts = this.m_CachedSkyContexts;
				CachedSkyContext cachedSkyContext = default(CachedSkyContext);
				int num2 = cachedSkyContexts.Add(in cachedSkyContext);
				this.AllocateNewRenderingContext(updateContext, num2, newHash, supportConvolution, in sphericalHarmonicsL);
			}
			return true;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00051870 File Offset: 0x0004FA70
		private void ReleaseCachedContext(int id)
		{
			ref CachedSkyContext ptr = ref this.m_CachedSkyContexts[id];
			ptr.refCount--;
			if (ptr.refCount == 0)
			{
				ptr.Cleanup();
			}
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000518A4 File Offset: 0x0004FAA4
		private bool IsCachedContextValid(SkyUpdateContext skyContext)
		{
			if (skyContext.skySettings == null)
			{
				return false;
			}
			int cachedSkyRenderingContextId = skyContext.cachedSkyRenderingContextId;
			return cachedSkyRenderingContextId != -1 && skyContext.skySettings.GetSkyRendererType() == this.m_CachedSkyContexts[cachedSkyRenderingContextId].type && this.m_CachedSkyContexts[cachedSkyRenderingContextId].hash != 0;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00051908 File Offset: 0x0004FB08
		private int ComputeSkyHash(SkyUpdateContext skyContext, Light sunLight, SkyAmbientMode ambientMode, bool staticSky = false)
		{
			int num = 0;
			if (sunLight != null)
			{
				num = this.GetSunLightHashCode(sunLight);
			}
			return ((num * 23 + skyContext.skySettings.GetHashCode()) * 23 + (staticSky ? 1 : 0)) * 23 + ((ambientMode == SkyAmbientMode.Static) ? 1 : 0);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0005194F File Offset: 0x0004FB4F
		public void RequestEnvironmentUpdate()
		{
			this.m_UpdateRequired = true;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00051958 File Offset: 0x0004FB58
		public void UpdateEnvironment(HDCamera hdCamera, ScriptableRenderContext renderContext, SkyUpdateContext skyContext, Light sunLight, bool updateRequired, bool updateAmbientProbe, bool staticSky, SkyAmbientMode ambientMode, int frameIndex, CommandBuffer cmd)
		{
			if (skyContext.IsValid())
			{
				skyContext.currentUpdateTime += Time.deltaTime;
				this.m_BuiltinParameters.hdCamera = hdCamera;
				this.m_BuiltinParameters.commandBuffer = cmd;
				this.m_BuiltinParameters.sunLight = sunLight;
				this.m_BuiltinParameters.pixelCoordToViewDirMatrix = hdCamera.mainViewConstants.pixelCoordToViewDirWS;
				this.m_BuiltinParameters.worldSpaceCameraPos = hdCamera.mainViewConstants.worldSpaceCameraPos;
				this.m_BuiltinParameters.viewMatrix = hdCamera.mainViewConstants.viewMatrix;
				this.m_BuiltinParameters.screenSize = this.m_CubemapScreenSize;
				this.m_BuiltinParameters.debugSettings = null;
				this.m_BuiltinParameters.frameIndex = frameIndex;
				this.m_BuiltinParameters.skySettings = skyContext.skySettings;
				int num = this.ComputeSkyHash(skyContext, sunLight, ambientMode, staticSky);
				bool flag = updateRequired | this.AcquireSkyRenderingContext(skyContext, num, !staticSky);
				SkyRenderingContext renderingContext = this.m_CachedSkyContexts[skyContext.cachedSkyRenderingContextId].renderingContext;
				if (this.IsCachedContextValid(skyContext))
				{
					flag |= skyContext.skyRenderer.DoUpdate(this.m_BuiltinParameters);
				}
				if (!flag && (skyContext.skySettings.updateMode.value != EnvironmentUpdateMode.OnChanged || num == skyContext.skyParametersHash) && (skyContext.skySettings.updateMode.value != EnvironmentUpdateMode.Realtime || skyContext.currentUpdateTime <= skyContext.skySettings.updatePeriod.value))
				{
					return;
				}
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.UpdateSkyEnvironment)))
				{
					this.RenderSkyToCubemap(skyContext);
					if (updateAmbientProbe)
					{
						using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.UpdateSkyAmbientProbe)))
						{
							cmd.SetComputeBufferParam(this.m_ComputeAmbientProbeCS, this.m_ComputeAmbientProbeKernel, this.m_AmbientProbeOutputBufferParam, renderingContext.ambientProbeResult);
							cmd.SetComputeTextureParam(this.m_ComputeAmbientProbeCS, this.m_ComputeAmbientProbeKernel, this.m_AmbientProbeInputCubemap, renderingContext.skyboxCubemapRT);
							cmd.DispatchCompute(this.m_ComputeAmbientProbeCS, this.m_ComputeAmbientProbeKernel, 1, 1, 1);
							cmd.RequestAsyncReadback(renderingContext.ambientProbeResult, new Action<AsyncGPUReadbackRequest>(renderingContext.OnComputeAmbientProbeDone));
							if (!Profiler.enabled && this.m_requireWaitForAsyncReadBackRequest)
							{
								cmd.WaitAllAsyncReadbackRequests();
								renderContext.ExecuteCommandBuffer(cmd);
								CommandBufferPool.Release(cmd);
								renderContext.Submit();
								cmd = CommandBufferPool.Get();
								this.m_requireWaitForAsyncReadBackRequest = false;
							}
						}
					}
					if (renderingContext.supportsConvolution)
					{
						this.RenderCubemapGGXConvolution(skyContext);
					}
					skyContext.skyParametersHash = num;
					skyContext.currentUpdateTime = 0f;
					return;
				}
			}
			if (skyContext.cachedSkyRenderingContextId != -1)
			{
				this.ReleaseCachedContext(skyContext.cachedSkyRenderingContextId);
				skyContext.cachedSkyRenderingContextId = -1;
			}
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00051C20 File Offset: 0x0004FE20
		public void UpdateEnvironment(HDCamera hdCamera, ScriptableRenderContext renderContext, Light sunLight, int frameIndex, CommandBuffer cmd)
		{
			SkyAmbientMode value = hdCamera.volumeStack.GetComponent<VisualEnvironment>().skyAmbientMode.value;
			this.UpdateEnvironment(hdCamera, renderContext, hdCamera.lightingSky, sunLight, this.m_UpdateRequired, value == SkyAmbientMode.Dynamic, false, value, frameIndex, cmd);
			if (value == SkyAmbientMode.Static && hdCamera.camera.cameraType != CameraType.Preview)
			{
				StaticLightingSky staticLightingSky = SkyManager.GetStaticLightingSky();
				if (staticLightingSky != null)
				{
					this.m_StaticLightingSky.skySettings = staticLightingSky.skySettings;
					this.UpdateEnvironment(hdCamera, renderContext, this.m_StaticLightingSky, sunLight, false, true, true, value, frameIndex, cmd);
				}
			}
			this.m_UpdateRequired = false;
			Texture reflectionTexture = this.GetReflectionTexture(hdCamera.lightingSky);
			cmd.SetGlobalTexture(HDShaderIDs._SkyTexture, reflectionTexture);
			float num = Mathf.Clamp(Mathf.Log((float)reflectionTexture.width, 2f) + 1f, 0f, 6f);
			cmd.SetGlobalFloat(HDShaderIDs._SkyTextureMipCount, num);
			if (this.IsLightingSkyValid(hdCamera))
			{
				cmd.SetGlobalInt(HDShaderIDs._EnvLightSkyEnabled, 1);
				return;
			}
			cmd.SetGlobalInt(HDShaderIDs._EnvLightSkyEnabled, 0);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00051D28 File Offset: 0x0004FF28
		internal void UpdateBuiltinParameters(SkyUpdateContext skyContext, HDCamera hdCamera, Light sunLight, RTHandle colorBuffer, RTHandle depthBuffer, DebugDisplaySettings debugSettings, int frameIndex, CommandBuffer cmd)
		{
			this.m_BuiltinParameters.hdCamera = hdCamera;
			this.m_BuiltinParameters.commandBuffer = cmd;
			this.m_BuiltinParameters.sunLight = sunLight;
			this.m_BuiltinParameters.pixelCoordToViewDirMatrix = hdCamera.mainViewConstants.pixelCoordToViewDirWS;
			this.m_BuiltinParameters.worldSpaceCameraPos = hdCamera.mainViewConstants.worldSpaceCameraPos;
			this.m_BuiltinParameters.viewMatrix = hdCamera.mainViewConstants.viewMatrix;
			this.m_BuiltinParameters.screenSize = hdCamera.screenSize;
			this.m_BuiltinParameters.colorBuffer = colorBuffer;
			this.m_BuiltinParameters.depthBuffer = depthBuffer;
			this.m_BuiltinParameters.debugSettings = debugSettings;
			this.m_BuiltinParameters.frameIndex = frameIndex;
			this.m_BuiltinParameters.skySettings = skyContext.skySettings;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00051DF4 File Offset: 0x0004FFF4
		public void PreRenderSky(HDCamera hdCamera, Light sunLight, RTHandle colorBuffer, RTHandle normalBuffer, RTHandle depthBuffer, DebugDisplaySettings debugSettings, int frameIndex, CommandBuffer cmd)
		{
			SkyUpdateContext visualSky = hdCamera.visualSky;
			if (visualSky.IsValid())
			{
				this.UpdateBuiltinParameters(visualSky, hdCamera, sunLight, colorBuffer, depthBuffer, debugSettings, frameIndex, cmd);
				SkyAmbientMode value = hdCamera.volumeStack.GetComponent<VisualEnvironment>().skyAmbientMode.value;
				int num = this.ComputeSkyHash(visualSky, sunLight, value, false);
				this.AcquireSkyRenderingContext(visualSky, num, true);
				visualSky.skyRenderer.DoUpdate(this.m_BuiltinParameters);
				if (depthBuffer != BuiltinSkyParameters.nullRT && normalBuffer != BuiltinSkyParameters.nullRT)
				{
					CoreUtils.SetRenderTarget(cmd, normalBuffer, depthBuffer, 0, CubemapFace.Unknown, -1);
				}
				else if (depthBuffer != BuiltinSkyParameters.nullRT)
				{
					CoreUtils.SetRenderTarget(cmd, depthBuffer, ClearFlag.None, 0, CubemapFace.Unknown, -1);
				}
				visualSky.skyRenderer.PreRenderSky(this.m_BuiltinParameters, false, hdCamera.camera.cameraType != CameraType.Reflection || visualSky.skySettings.includeSunInBaking.value);
			}
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00051EEC File Offset: 0x000500EC
		public void RenderSky(HDCamera hdCamera, Light sunLight, RTHandle colorBuffer, RTHandle depthBuffer, DebugDisplaySettings debugSettings, int frameIndex, CommandBuffer cmd)
		{
			SkyUpdateContext visualSky = hdCamera.visualSky;
			if (visualSky.IsValid() && hdCamera.clearColorMode == HDAdditionalCameraData.ClearColorMode.Sky)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderSky)))
				{
					this.UpdateBuiltinParameters(visualSky, hdCamera, sunLight, colorBuffer, depthBuffer, debugSettings, frameIndex, cmd);
					SkyAmbientMode value = hdCamera.volumeStack.GetComponent<VisualEnvironment>().skyAmbientMode.value;
					int num = this.ComputeSkyHash(visualSky, sunLight, value, false);
					this.AcquireSkyRenderingContext(visualSky, num, true);
					visualSky.skyRenderer.DoUpdate(this.m_BuiltinParameters);
					if (depthBuffer == BuiltinSkyParameters.nullRT)
					{
						CoreUtils.SetRenderTarget(cmd, colorBuffer, ClearFlag.None, 0, CubemapFace.Unknown, -1);
					}
					else
					{
						CoreUtils.SetRenderTarget(cmd, colorBuffer, depthBuffer, 0, CubemapFace.Unknown, -1);
					}
					if (debugSettings.data.lightingDebugSettings.debugLightingMode != DebugLightingMode.LuxMeter)
					{
						visualSky.skyRenderer.RenderSky(this.m_BuiltinParameters, false, hdCamera.camera.cameraType != CameraType.Reflection || visualSky.skySettings.includeSunInBaking.value);
					}
				}
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0005200C File Offset: 0x0005020C
		public void RenderOpaqueAtmosphericScattering(CommandBuffer cmd, HDCamera hdCamera, RTHandle colorBuffer, RTHandle volumetricLighting, RTHandle intermediateBuffer, RTHandle depthBuffer, Matrix4x4 pixelCoordToViewDirWS, bool isMSAA)
		{
			using (new ProfilingScope(this.m_BuiltinParameters.commandBuffer, ProfilingSampler.Get<HDProfileId>(HDProfileId.OpaqueAtmosphericScattering)))
			{
				this.m_OpaqueAtmScatteringBlock.SetMatrix(HDShaderIDs._PixelCoordToViewDirWS, pixelCoordToViewDirWS);
				if (isMSAA)
				{
					this.m_OpaqueAtmScatteringBlock.SetTexture(HDShaderIDs._ColorTextureMS, colorBuffer);
				}
				else
				{
					this.m_OpaqueAtmScatteringBlock.SetTexture(HDShaderIDs._ColorTexture, colorBuffer);
				}
				if (volumetricLighting != null)
				{
					this.m_OpaqueAtmScatteringBlock.SetTexture(HDShaderIDs._VBufferLighting, volumetricLighting);
				}
				if (Fog.IsPBRFogEnabled(hdCamera))
				{
					HDUtils.DrawFullScreen(cmd, this.m_OpaqueAtmScatteringMaterial, intermediateBuffer, depthBuffer, this.m_OpaqueAtmScatteringBlock, isMSAA ? 3 : 2);
					cmd.CopyTexture(intermediateBuffer, colorBuffer);
				}
				else
				{
					HDUtils.DrawFullScreen(cmd, this.m_OpaqueAtmScatteringMaterial, colorBuffer, depthBuffer, this.m_OpaqueAtmScatteringBlock, isMSAA ? 1 : 0);
				}
			}
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0005210C File Offset: 0x0005030C
		public static StaticLightingSky GetStaticLightingSky()
		{
			if (SkyManager.m_StaticLightingSkies.Count == 0)
			{
				return null;
			}
			return SkyManager.m_StaticLightingSkies[SkyManager.m_StaticLightingSkies.Count - 1];
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00052134 File Offset: 0x00050334
		public static void RegisterStaticLightingSky(StaticLightingSky staticLightingSky)
		{
			if (!SkyManager.m_StaticLightingSkies.Contains(staticLightingSky))
			{
				if (SkyManager.m_StaticLightingSkies.Count != 0)
				{
					Debug.LogWarning("One Static Lighting Sky component was already set for baking, only the latest one will be used.");
				}
				Type type;
				if (staticLightingSky.staticLightingSkyUniqueID == 2 && !SkyManager.skyTypesDict.TryGetValue(2, out type))
				{
					Debug.LogError("You are using the deprecated Procedural Sky for static lighting in your Scene. You can still use it but, to do so, you must install it separately. To do this, open the Package Manager window and import the 'Procedural Sky' sample from the HDRP package page, then close and re-open your project without saving.");
					return;
				}
				SkyManager.m_StaticLightingSkies.Add(staticLightingSky);
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00052192 File Offset: 0x00050392
		public static void UnRegisterStaticLightingSky(StaticLightingSky staticLightingSky)
		{
			SkyManager.m_StaticLightingSkies.Remove(staticLightingSky);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x000521A0 File Offset: 0x000503A0
		public Texture2D ExportSkyToTexture(Camera camera)
		{
			HDCamera orCreate = HDCamera.GetOrCreate(camera, 0);
			if (!orCreate.visualSky.IsValid() || !this.IsCachedContextValid(orCreate.visualSky))
			{
				Debug.LogError("Cannot export sky to a texture, no valid Sky is setup (Also make sure the game view has been rendered at least once).");
				return null;
			}
			RenderTexture renderTexture = this.m_CachedSkyContexts[orCreate.visualSky.cachedSkyRenderingContextId].renderingContext.skyboxCubemapRT;
			int width = renderTexture.width;
			RenderTexture renderTexture2 = new RenderTexture(width * 6, width, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear)
			{
				dimension = TextureDimension.Tex2D,
				useMipMap = false,
				autoGenerateMips = false,
				filterMode = FilterMode.Trilinear
			};
			renderTexture2.Create();
			Texture2D texture2D = new Texture2D(width * 6, width, TextureFormat.RGBAFloat, false);
			Texture2D texture2D2 = new Texture2D(width * 6, width, TextureFormat.RGBAFloat, false);
			int num = 0;
			for (int i = 0; i < 6; i++)
			{
				Graphics.SetRenderTarget(renderTexture, 0, (CubemapFace)i);
				texture2D.ReadPixels(new Rect(0f, 0f, (float)width, (float)width), num, 0);
				texture2D.Apply();
				num += width;
			}
			Graphics.Blit(texture2D, renderTexture2, new Vector2(1f, -1f), new Vector2(0f, 0f));
			texture2D2.ReadPixels(new Rect(0f, 0f, (float)(width * 6), (float)width), 0, 0);
			texture2D2.Apply();
			Graphics.SetRenderTarget(null);
			CoreUtils.Destroy(texture2D);
			CoreUtils.Destroy(renderTexture2);
			return texture2D2;
		}

		// Token: 0x04000FC5 RID: 4037
		private Material m_StandardSkyboxMaterial;

		// Token: 0x04000FC6 RID: 4038
		private Material m_BlitCubemapMaterial;

		// Token: 0x04000FC7 RID: 4039
		private Material m_OpaqueAtmScatteringMaterial;

		// Token: 0x04000FC8 RID: 4040
		private SphericalHarmonicsL2 m_BlackAmbientProbe;

		// Token: 0x04000FC9 RID: 4041
		private bool m_UpdateRequired;

		// Token: 0x04000FCA RID: 4042
		private int m_Resolution;

		// Token: 0x04000FCB RID: 4043
		private SkyUpdateContext m_StaticLightingSky = new SkyUpdateContext();

		// Token: 0x04000FCE RID: 4046
		private static Dictionary<int, Type> m_SkyTypesDict = null;

		// Token: 0x04000FCF RID: 4047
		private static List<StaticLightingSky> m_StaticLightingSkies = new List<StaticLightingSky>();

		// Token: 0x04000FD0 RID: 4048
		private static bool logOnce = true;

		// Token: 0x04000FD1 RID: 4049
		private bool m_requireWaitForAsyncReadBackRequest = true;

		// Token: 0x04000FD2 RID: 4050
		private MaterialPropertyBlock m_OpaqueAtmScatteringBlock;

		// Token: 0x04000FD3 RID: 4051
		private IBLFilterBSDF[] m_IBLFilterArray;

		// Token: 0x04000FD4 RID: 4052
		private RTHandle m_SkyboxBSDFCubemapIntermediate;

		// Token: 0x04000FD5 RID: 4053
		private Vector4 m_CubemapScreenSize;

		// Token: 0x04000FD6 RID: 4054
		private Matrix4x4[] m_facePixelCoordToViewDirMatrices = new Matrix4x4[6];

		// Token: 0x04000FD7 RID: 4055
		private Matrix4x4[] m_CameraRelativeViewMatrices = new Matrix4x4[6];

		// Token: 0x04000FD8 RID: 4056
		private BuiltinSkyParameters m_BuiltinParameters = new BuiltinSkyParameters();

		// Token: 0x04000FD9 RID: 4057
		private ComputeShader m_ComputeAmbientProbeCS;

		// Token: 0x04000FDA RID: 4058
		private readonly int m_AmbientProbeOutputBufferParam = Shader.PropertyToID("_AmbientProbeOutputBuffer");

		// Token: 0x04000FDB RID: 4059
		private readonly int m_AmbientProbeInputCubemap = Shader.PropertyToID("_AmbientProbeInputCubemap");

		// Token: 0x04000FDC RID: 4060
		private int m_ComputeAmbientProbeKernel;

		// Token: 0x04000FDD RID: 4061
		private CubemapArray m_BlackCubemapArray;

		// Token: 0x04000FDE RID: 4062
		private DynamicArray<CachedSkyContext> m_CachedSkyContexts = new DynamicArray<CachedSkyContext>(2);
	}
}
