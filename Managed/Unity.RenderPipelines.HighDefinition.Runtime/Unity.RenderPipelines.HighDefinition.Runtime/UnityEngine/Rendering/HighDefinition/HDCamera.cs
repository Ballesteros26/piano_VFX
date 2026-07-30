using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using Utilities;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000F8 RID: 248
	public class HDCamera
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0003F943 File Offset: 0x0003DB43
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x0003F94B File Offset: 0x0003DB4B
		public int actualWidth { get; private set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0003F954 File Offset: 0x0003DB54
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x0003F95C File Offset: 0x0003DB5C
		public int actualHeight { get; private set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0003F965 File Offset: 0x0003DB65
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x0003F96D File Offset: 0x0003DB6D
		public MSAASamples msaaSamples { get; private set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0003F976 File Offset: 0x0003DB76
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x0003F97E File Offset: 0x0003DB7E
		public FrameSettings frameSettings { get; private set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0003F987 File Offset: 0x0003DB87
		public RTHandleProperties historyRTHandleProperties
		{
			get
			{
				return this.m_HistoryRTSystem.rtHandleProperties;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0003F994 File Offset: 0x0003DB94
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0003F99C File Offset: 0x0003DB9C
		public VolumeStack volumeStack { get; private set; }

		// Token: 0x060007F2 RID: 2034 RVA: 0x0003F9A8 File Offset: 0x0003DBA8
		public static HDCamera GetOrCreate(Camera camera, int xrMultipassId = 0)
		{
			HDCamera hdcamera;
			if (!HDCamera.s_Cameras.TryGetValue(new ValueTuple<Camera, int>(camera, xrMultipassId), out hdcamera))
			{
				hdcamera = new HDCamera(camera);
				HDCamera.s_Cameras.Add(new ValueTuple<Camera, int>(camera, xrMultipassId), hdcamera);
			}
			return hdcamera;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0003F9E4 File Offset: 0x0003DBE4
		public void Reset()
		{
			this.isFirstFrame = true;
			this.cameraFrameCount = 0U;
			this.resetPostProcessingHistory = true;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0003F9FC File Offset: 0x0003DBFC
		public RTHandle AllocHistoryFrameRT(int id, Func<string, int, RTHandleSystem, RTHandle> allocator, int bufferCount)
		{
			this.m_HistoryRTSystem.AllocBuffer(id, (RTHandleSystem rts, int i) => allocator(this.camera.name, i, rts), bufferCount);
			return this.m_HistoryRTSystem.GetFrameRT(id, 0);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0003FA43 File Offset: 0x0003DC43
		public RTHandle GetPreviousFrameRT(int id)
		{
			return this.m_HistoryRTSystem.GetFrameRT(id, 1);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0003FA52 File Offset: 0x0003DC52
		public RTHandle GetCurrentFrameRT(int id)
		{
			return this.m_HistoryRTSystem.GetFrameRT(id, 0);
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0003FA61 File Offset: 0x0003DC61
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0003FA69 File Offset: 0x0003DC69
		internal SkyUpdateContext visualSky { get; private set; } = new SkyUpdateContext();

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0003FA72 File Offset: 0x0003DC72
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x0003FA7A File Offset: 0x0003DC7A
		internal SkyUpdateContext lightingSky { get; private set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0003FA83 File Offset: 0x0003DC83
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x0003FA8B File Offset: 0x0003DC8B
		internal SkyAmbientMode skyAmbientMode { get; private set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0003FA94 File Offset: 0x0003DC94
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x0003FA9C File Offset: 0x0003DC9C
		internal XRPass xr { get; private set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x0003FAA5 File Offset: 0x0003DCA5
		internal Matrix4x4 nonObliqueProjMatrix
		{
			get
			{
				if (!(this.m_AdditionalCameraData != null))
				{
					return GeometryUtils.CalculateProjectionMatrix(this.camera);
				}
				return this.m_AdditionalCameraData.GetNonObliqueProjection(this.camera);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0003FAD2 File Offset: 0x0003DCD2
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x0003FADA File Offset: 0x0003DCDA
		internal bool isFirstFrame { get; private set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0003FAE3 File Offset: 0x0003DCE3
		internal bool isMainGameView
		{
			get
			{
				return this.camera.cameraType == CameraType.Game && this.camera.targetTexture == null;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0003FB06 File Offset: 0x0003DD06
		internal int viewCount
		{
			get
			{
				return Math.Max(1, this.xr.viewCount);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0003FB19 File Offset: 0x0003DD19
		internal bool clearDepth
		{
			get
			{
				if (!(this.m_AdditionalCameraData != null))
				{
					return this.camera.clearFlags != CameraClearFlags.Nothing;
				}
				return this.m_AdditionalCameraData.clearDepth;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x0003FB46 File Offset: 0x0003DD46
		internal HDAdditionalCameraData.ClearColorMode clearColorMode
		{
			get
			{
				if (this.m_AdditionalCameraData != null)
				{
					return this.m_AdditionalCameraData.clearColorMode;
				}
				if (this.camera.clearFlags == CameraClearFlags.Skybox)
				{
					return HDAdditionalCameraData.ClearColorMode.Sky;
				}
				if (this.camera.clearFlags == CameraClearFlags.Color)
				{
					return HDAdditionalCameraData.ClearColorMode.Color;
				}
				return HDAdditionalCameraData.ClearColorMode.None;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0003FB84 File Offset: 0x0003DD84
		internal Color backgroundColorHDR
		{
			get
			{
				if (this.m_AdditionalCameraData != null)
				{
					return this.m_AdditionalCameraData.backgroundColorHDR;
				}
				return this.camera.backgroundColor.linear;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x0003FBBE File Offset: 0x0003DDBE
		internal HDAdditionalCameraData.FlipYMode flipYMode
		{
			get
			{
				if (this.m_AdditionalCameraData != null)
				{
					return this.m_AdditionalCameraData.flipYMode;
				}
				return HDAdditionalCameraData.FlipYMode.Automatic;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x0003FBDB File Offset: 0x0003DDDB
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x0003FBE3 File Offset: 0x0003DDE3
		internal HDAdditionalCameraData.AntialiasingMode antialiasing { get; private set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0003FBEC File Offset: 0x0003DDEC
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x0003FBF4 File Offset: 0x0003DDF4
		internal HDAdditionalCameraData.SMAAQualityLevel SMAAQuality { get; private set; } = HDAdditionalCameraData.SMAAQualityLevel.Medium;

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0003FBFD File Offset: 0x0003DDFD
		internal bool dithering
		{
			get
			{
				return this.m_AdditionalCameraData != null && this.m_AdditionalCameraData.dithering;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0003FC1A File Offset: 0x0003DE1A
		internal bool stopNaNs
		{
			get
			{
				return this.m_AdditionalCameraData != null && this.m_AdditionalCameraData.stopNaNs;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0003FC37 File Offset: 0x0003DE37
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x0003FC3F File Offset: 0x0003DE3F
		internal HDPhysicalCamera physicalParameters { get; private set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0003FC48 File Offset: 0x0003DE48
		internal IEnumerable<AOVRequestData> aovRequests
		{
			get
			{
				if (!(this.m_AdditionalCameraData != null) || this.m_AdditionalCameraData.Equals(null))
				{
					return Enumerable.Empty<AOVRequestData>();
				}
				return this.m_AdditionalCameraData.aovRequests;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0003FC77 File Offset: 0x0003DE77
		internal LayerMask probeLayerMask
		{
			get
			{
				if (!(this.m_AdditionalCameraData != null))
				{
					return -1;
				}
				return this.m_AdditionalCameraData.probeLayerMask;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0003FC99 File Offset: 0x0003DE99
		internal float probeRangeCompressionFactor
		{
			get
			{
				if (!(this.m_AdditionalCameraData != null))
				{
					return 1f;
				}
				return this.m_AdditionalCameraData.probeCustomFixedExposure;
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0003FCBC File Offset: 0x0003DEBC
		internal bool ValidShadowHistory(HDAdditionalLightData lightData, int screenSpaceShadowIndex, GPULightType lightType)
		{
			return this.shadowHistoryUsage[screenSpaceShadowIndex].lightInstanceID == lightData.GetInstanceID() && this.shadowHistoryUsage[screenSpaceShadowIndex].frameCount == this.cameraFrameCount - 1U && this.shadowHistoryUsage[screenSpaceShadowIndex].lightType == lightType;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0003FD14 File Offset: 0x0003DF14
		internal void PropagateShadowHistory(HDAdditionalLightData lightData, int screenSpaceShadowIndex, GPULightType lightType)
		{
			this.shadowHistoryUsage[screenSpaceShadowIndex].lightInstanceID = lightData.GetInstanceID();
			this.shadowHistoryUsage[screenSpaceShadowIndex].frameCount = this.cameraFrameCount;
			this.shadowHistoryUsage[screenSpaceShadowIndex].lightType = lightType;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0003FD61 File Offset: 0x0003DF61
		internal ProfilingSampler profilingSampler
		{
			get
			{
				HDAdditionalCameraData additionalCameraData = this.m_AdditionalCameraData;
				return ((additionalCameraData != null) ? additionalCameraData.profilingSampler : null) ?? ProfilingSampler.Get<HDProfileId>(HDProfileId.HDRenderPipelineRenderCamera);
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0003FD80 File Offset: 0x0003DF80
		internal HDCamera(Camera cam)
		{
			this.camera = cam;
			this.frustum = default(Frustum);
			this.frustum.planes = new Plane[6];
			this.frustum.corners = new Vector3[8];
			this.frustumPlaneEquations = new Vector4[6];
			this.volumeStack = VolumeManager.instance.CreateStack();
			this.Reset();
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0003FF14 File Offset: 0x0003E114
		internal bool IsTAAEnabled()
		{
			return this.antialiasing == HDAdditionalCameraData.AntialiasingMode.TemporalAntialiasing;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0003FF20 File Offset: 0x0003E120
		internal bool IsVolumetricReprojectionEnabled()
		{
			bool flag = Fog.IsVolumetricFogEnabled(this);
			bool flag2 = this.frameSettings.IsEnabled(FrameSettingsField.ReprojectionForVolumetrics);
			bool flag3 = this.camera.cameraType == CameraType.Game;
			bool isPlaying = Application.isPlaying;
			return flag && flag2 && flag3 && isPlaying;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0003FF60 File Offset: 0x0003E160
		internal void Update(FrameSettings currentFrameSettings, HDRenderPipeline hdrp, MSAASamples newMSAASamples, XRPass xrPass)
		{
			Camera camera = ((this.parentCamera != null) ? this.parentCamera : this.camera);
			this.animateMaterials = CoreUtils.AreAnimatedMaterialsEnabled(camera);
			this.time = (this.animateMaterials ? hdrp.GetTime() : 0f);
			this.lastTime = (this.animateMaterials ? hdrp.GetLastTime() : 0f);
			if (this.shadowHistoryUsage == null || this.shadowHistoryUsage.Length != hdrp.currentPlatformRenderPipelineSettings.hdShadowInitParams.maxScreenSpaceShadowSlots)
			{
				this.shadowHistoryUsage = new HDCamera.ShadowHistoryUsage[hdrp.currentPlatformRenderPipelineSettings.hdShadowInitParams.maxScreenSpaceShadowSlots];
			}
			this.camera.TryGetComponent<HDAdditionalCameraData>(out this.m_AdditionalCameraData);
			this.UpdateVolumeAndPhysicalParameters();
			this.xr = xrPass;
			this.frameSettings = currentFrameSettings;
			this.UpdateAntialiasing();
			hdrp.ReinitializeVolumetricBufferParams(this);
			bool flag = this.frameSettings.IsEnabled(FrameSettingsField.Refraction) || this.frameSettings.IsEnabled(FrameSettingsField.Distortion);
			bool flag2 = this.frameSettings.IsEnabled(FrameSettingsField.SSR) || this.antialiasing == HDAdditionalCameraData.AntialiasingMode.TemporalAntialiasing;
			bool flag3 = this.IsVolumetricReprojectionEnabled();
			int num = 0;
			if (flag)
			{
				num = 1;
			}
			if (flag2)
			{
				num = 2;
			}
			int num2 = (flag3 ? 2 : 0);
			if (this.m_NumColorPyramidBuffersAllocated != num || this.m_NumVolumetricBuffersAllocated != num2)
			{
				this.colorPyramidHistoryIsValid = false;
				this.volumetricHistoryIsValid = false;
				this.m_HistoryRTSystem.Dispose();
				this.m_HistoryRTSystem = new BufferedRTHandleSystem();
				if (num != 0)
				{
					this.AllocHistoryFrameRT(0, new Func<string, int, RTHandleSystem, RTHandle>(HDCamera.HistoryBufferAllocatorFunction), num);
				}
				if (num2 != 0)
				{
					hdrp.AllocateVolumetricHistoryBuffers(this, num2);
				}
				this.m_NumColorPyramidBuffersAllocated = num;
				this.m_NumVolumetricBuffersAllocated = num2;
			}
			if (this.xr.enabled)
			{
				this.finalViewport = this.xr.GetViewport(0);
			}
			else
			{
				this.finalViewport = new Rect(this.camera.pixelRect.x, this.camera.pixelRect.y, (float)this.camera.pixelWidth, (float)this.camera.pixelHeight);
			}
			this.actualWidth = Math.Max((int)this.finalViewport.size.x, 1);
			this.actualHeight = Math.Max((int)this.finalViewport.size.y, 1);
			Vector2Int vector2Int = new Vector2Int(this.actualWidth, this.actualHeight);
			if (this.isMainGameView)
			{
				Vector2Int scaledSize = DynamicResolutionHandler.instance.GetScaledSize(new Vector2Int(this.actualWidth, this.actualHeight));
				this.actualWidth = scaledSize.x;
				this.actualHeight = scaledSize.y;
			}
			int actualWidth = this.actualWidth;
			int actualHeight = this.actualHeight;
			this.msaaSamples = newMSAASamples;
			this.screenSize = new Vector4((float)actualWidth, (float)actualHeight, 1f / (float)actualWidth, 1f / (float)actualHeight);
			this.screenParams = new Vector4(this.screenSize.x, this.screenSize.y, 1f + this.screenSize.z, 1f + this.screenSize.w);
			this.UpdateAllViewConstants();
			this.isFirstFrame = false;
			this.cameraFrameCount += 1U;
			hdrp.UpdateVolumetricBufferParams(this);
			RTHandles.SetReferenceSize(vector2Int.x, vector2Int.y, this.msaaSamples);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x000402C0 File Offset: 0x0003E4C0
		internal void BeginRender(CommandBuffer cmd)
		{
			RTHandles.SetReferenceSize(this.actualWidth, this.actualHeight, this.msaaSamples);
			this.m_HistoryRTSystem.SwapAndSetReferenceSize(this.actualWidth, this.actualHeight, this.msaaSamples);
			this.m_RecorderCaptureActions = CameraCaptureBridge.GetCaptureActions(this.camera);
			this.SetupCurrentMaterialQuality(cmd);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00040319 File Offset: 0x0003E519
		internal void UpdateAllViewConstants(bool jitterProjectionMatrix)
		{
			this.UpdateAllViewConstants(jitterProjectionMatrix, false);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00040324 File Offset: 0x0003E524
		internal void GetPixelCoordToViewDirWS(Vector4 resolution, float aspect, ref Matrix4x4[] transforms)
		{
			if (this.xr.singlePassEnabled)
			{
				for (int i = 0; i < this.viewCount; i++)
				{
					transforms[i] = this.ComputePixelCoordToWorldSpaceViewDirectionMatrix(this.m_XRViewConstants[i], resolution, aspect);
				}
				return;
			}
			transforms[0] = this.ComputePixelCoordToWorldSpaceViewDirectionMatrix(this.mainViewConstants, resolution, aspect);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00040384 File Offset: 0x0003E584
		internal static void ClearAll()
		{
			foreach (KeyValuePair<ValueTuple<Camera, int>, HDCamera> keyValuePair in HDCamera.s_Cameras)
			{
				keyValuePair.Value.ReleaseHistoryBuffer();
				keyValuePair.Value.Dispose();
			}
			HDCamera.s_Cameras.Clear();
			HDCamera.s_Cleanup.Clear();
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x000403FC File Offset: 0x0003E5FC
		internal static void CleanUnused()
		{
			foreach (ValueTuple<Camera, int> valueTuple in HDCamera.s_Cameras.Keys)
			{
				HDCamera hdcamera = HDCamera.s_Cameras[valueTuple];
				if (!(hdcamera.camera != null) || hdcamera.camera.cameraType != CameraType.SceneView)
				{
					bool flag = hdcamera.m_AdditionalCameraData != null && hdcamera.m_AdditionalCameraData.hasPersistentHistory;
					if (hdcamera.camera == null || (!hdcamera.camera.isActiveAndEnabled && hdcamera.camera.cameraType != CameraType.Preview && !flag))
					{
						HDCamera.s_Cleanup.Add(valueTuple);
					}
				}
			}
			foreach (ValueTuple<Camera, int> valueTuple2 in HDCamera.s_Cleanup)
			{
				HDCamera.s_Cameras[valueTuple2].Dispose();
				HDCamera.s_Cameras.Remove(valueTuple2);
			}
			HDCamera.s_Cleanup.Clear();
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00040534 File Offset: 0x0003E734
		internal void SetupGlobalParams(CommandBuffer cmd, int frameCount)
		{
			bool flag = this.frameSettings.IsEnabled(FrameSettingsField.Postprocess) && this.antialiasing == HDAdditionalCameraData.AntialiasingMode.TemporalAntialiasing && this.camera.cameraType == CameraType.Game;
			cmd.SetGlobalMatrix(HDShaderIDs._ViewMatrix, this.mainViewConstants.viewMatrix);
			cmd.SetGlobalMatrix(HDShaderIDs._InvViewMatrix, this.mainViewConstants.invViewMatrix);
			cmd.SetGlobalMatrix(HDShaderIDs._ProjMatrix, this.mainViewConstants.projMatrix);
			cmd.SetGlobalMatrix(HDShaderIDs._InvProjMatrix, this.mainViewConstants.invProjMatrix);
			cmd.SetGlobalMatrix(HDShaderIDs._ViewProjMatrix, this.mainViewConstants.viewProjMatrix);
			cmd.SetGlobalMatrix(HDShaderIDs._InvViewProjMatrix, this.mainViewConstants.invViewProjMatrix);
			cmd.SetGlobalMatrix(HDShaderIDs._NonJitteredViewProjMatrix, this.mainViewConstants.nonJitteredViewProjMatrix);
			cmd.SetGlobalMatrix(HDShaderIDs._PrevViewProjMatrix, this.mainViewConstants.prevViewProjMatrix);
			cmd.SetGlobalMatrix(HDShaderIDs._PrevInvViewProjMatrix, this.mainViewConstants.prevInvViewProjMatrix);
			cmd.SetGlobalMatrix(HDShaderIDs._CameraViewProjMatrix, this.mainViewConstants.viewProjMatrix);
			cmd.SetGlobalVector(HDShaderIDs._WorldSpaceCameraPos, this.mainViewConstants.worldSpaceCameraPos);
			cmd.SetGlobalVector(HDShaderIDs._PrevCamPosRWS, this.mainViewConstants.prevWorldSpaceCameraPos);
			cmd.SetGlobalVector(HDShaderIDs._ScreenSize, this.screenSize);
			cmd.SetGlobalVector(HDShaderIDs._RTHandleScale, RTHandles.rtHandleProperties.rtHandleScale);
			cmd.SetGlobalVector(HDShaderIDs._RTHandleScaleHistory, this.m_HistoryRTSystem.rtHandleProperties.rtHandleScale);
			cmd.SetGlobalVector(HDShaderIDs._ZBufferParams, this.zBufferParams);
			cmd.SetGlobalVector(HDShaderIDs._ProjectionParams, this.projectionParams);
			cmd.SetGlobalVector(HDShaderIDs.unity_OrthoParams, this.unity_OrthoParams);
			cmd.SetGlobalVector(HDShaderIDs._ScreenParams, this.screenParams);
			cmd.SetGlobalVector(HDShaderIDs._TaaFrameInfo, new Vector4(this.taaSharpenStrength, 0f, (float)this.taaFrameIndex, (float)(flag ? 1 : 0)));
			cmd.SetGlobalVector(HDShaderIDs._TaaJitterStrength, this.taaJitter);
			cmd.SetGlobalInt(HDShaderIDs._FrameCount, frameCount);
			cmd.SetGlobalVectorArray(HDShaderIDs._FrustumPlanes, this.frustumPlaneEquations);
			float num = this.time;
			float num2 = this.lastTime;
			float deltaTime = Time.deltaTime;
			float smoothDeltaTime = Time.smoothDeltaTime;
			cmd.SetGlobalVector(HDShaderIDs._Time, new Vector4(num * 0.05f, num, num * 2f, num * 3f));
			cmd.SetGlobalVector(HDShaderIDs._SinTime, new Vector4(Mathf.Sin(num * 0.125f), Mathf.Sin(num * 0.25f), Mathf.Sin(num * 0.5f), Mathf.Sin(num)));
			cmd.SetGlobalVector(HDShaderIDs._CosTime, new Vector4(Mathf.Cos(num * 0.125f), Mathf.Cos(num * 0.25f), Mathf.Cos(num * 0.5f), Mathf.Cos(num)));
			cmd.SetGlobalVector(HDShaderIDs.unity_DeltaTime, new Vector4(deltaTime, 1f / deltaTime, smoothDeltaTime, 1f / smoothDeltaTime));
			cmd.SetGlobalVector(HDShaderIDs._TimeParameters, new Vector4(num, Mathf.Sin(num), Mathf.Cos(num), 0f));
			cmd.SetGlobalVector(HDShaderIDs._LastTimeParameters, new Vector4(num2, Mathf.Sin(num2), Mathf.Cos(num2), 0f));
			float num3 = 1f / Mathf.Max(this.probeRangeCompressionFactor, 1E-06f);
			cmd.SetGlobalFloat(HDShaderIDs._ProbeExposureScale, num3);
			cmd.SetGlobalInt(HDShaderIDs._XRViewCount, this.viewCount);
			for (int i = 0; i < this.viewCount; i++)
			{
				this.m_XRViewMatrix[i] = this.m_XRViewConstants[i].viewMatrix;
				this.m_XRInvViewMatrix[i] = this.m_XRViewConstants[i].invViewMatrix;
				this.m_XRProjMatrix[i] = this.m_XRViewConstants[i].projMatrix;
				this.m_XRInvProjMatrix[i] = this.m_XRViewConstants[i].invProjMatrix;
				this.m_XRViewProjMatrix[i] = this.m_XRViewConstants[i].viewProjMatrix;
				this.m_XRInvViewProjMatrix[i] = this.m_XRViewConstants[i].invViewProjMatrix;
				this.m_XRNonJitteredViewProjMatrix[i] = this.m_XRViewConstants[i].nonJitteredViewProjMatrix;
				this.m_XRPrevViewProjMatrix[i] = this.m_XRViewConstants[i].prevViewProjMatrix;
				this.m_XRPrevInvViewProjMatrix[i] = this.m_XRViewConstants[i].prevInvViewProjMatrix;
				this.m_XRPrevViewProjMatrixNoCameraTrans[i] = this.m_XRViewConstants[i].prevViewProjMatrixNoCameraTrans;
				this.m_XRPixelCoordToViewDirWS[i] = this.m_XRViewConstants[i].pixelCoordToViewDirWS;
				this.m_XRWorldSpaceCameraPos[i] = this.m_XRViewConstants[i].worldSpaceCameraPos;
				this.m_XRWorldSpaceCameraPosViewOffset[i] = this.m_XRViewConstants[i].worldSpaceCameraPosViewOffset;
				this.m_XRPrevWorldSpaceCameraPos[i] = this.m_XRViewConstants[i].prevWorldSpaceCameraPos;
			}
			cmd.SetGlobalMatrixArray(HDShaderIDs._XRViewMatrix, this.m_XRViewMatrix);
			cmd.SetGlobalMatrixArray(HDShaderIDs._XRInvViewMatrix, this.m_XRInvViewMatrix);
			cmd.SetGlobalMatrixArray(HDShaderIDs._XRProjMatrix, this.m_XRProjMatrix);
			cmd.SetGlobalMatrixArray(HDShaderIDs._XRInvProjMatrix, this.m_XRInvProjMatrix);
			cmd.SetGlobalMatrixArray(HDShaderIDs._XRViewProjMatrix, this.m_XRViewProjMatrix);
			cmd.SetGlobalMatrixArray(HDShaderIDs._XRInvViewProjMatrix, this.m_XRInvViewProjMatrix);
			cmd.SetGlobalMatrixArray(HDShaderIDs._XRNonJitteredViewProjMatrix, this.m_XRNonJitteredViewProjMatrix);
			cmd.SetGlobalMatrixArray(HDShaderIDs._XRPrevViewProjMatrix, this.m_XRPrevViewProjMatrix);
			cmd.SetGlobalMatrixArray(HDShaderIDs._XRPrevInvViewProjMatrix, this.m_XRPrevInvViewProjMatrix);
			cmd.SetGlobalMatrixArray(HDShaderIDs._XRPrevViewProjMatrixNoCameraTrans, this.m_XRPrevViewProjMatrixNoCameraTrans);
			cmd.SetGlobalMatrixArray(HDShaderIDs._XRPixelCoordToViewDirWS, this.m_XRPixelCoordToViewDirWS);
			cmd.SetGlobalVectorArray(HDShaderIDs._XRWorldSpaceCameraPos, this.m_XRWorldSpaceCameraPos);
			cmd.SetGlobalVectorArray(HDShaderIDs._XRWorldSpaceCameraPosViewOffset, this.m_XRWorldSpaceCameraPosViewOffset);
			cmd.SetGlobalVectorArray(HDShaderIDs._XRPrevWorldSpaceCameraPos, this.m_XRPrevWorldSpaceCameraPos);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00040B7C File Offset: 0x0003ED7C
		internal void AllocateAmbientOcclusionHistoryBuffer(float scaleFactor)
		{
			if (scaleFactor != this.m_AmbientOcclusionResolutionScale || this.GetCurrentFrameRT(7) == null)
			{
				this.ReleaseHistoryFrameRT(7);
				HDCamera.AmbientOcclusionAllocator ambientOcclusionAllocator = new HDCamera.AmbientOcclusionAllocator(scaleFactor);
				this.AllocHistoryFrameRT(7, new Func<string, int, RTHandleSystem, RTHandle>(ambientOcclusionAllocator.Allocator), 2);
				this.m_AmbientOcclusionResolutionScale = scaleFactor;
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00040BCB File Offset: 0x0003EDCB
		internal void ReleaseHistoryFrameRT(int id)
		{
			this.m_HistoryRTSystem.ReleaseBuffer(id);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00040BDC File Offset: 0x0003EDDC
		internal void ExecuteCaptureActions(RTHandle input, CommandBuffer cmd)
		{
			if (this.m_RecorderCaptureActions == null || !this.m_RecorderCaptureActions.MoveNext())
			{
				return;
			}
			cmd.GetTemporaryRT(this.m_RecorderTempRT, this.actualWidth, this.actualHeight, 0, FilterMode.Point, input.rt.graphicsFormat);
			Material blitMaterial = HDUtils.GetBlitMaterial(input.rt.dimension, false);
			Vector4 rtHandleScale = RTHandles.rtHandleProperties.rtHandleScale;
			Vector2 vector = new Vector2(rtHandleScale.x, rtHandleScale.y);
			this.m_RecorderPropertyBlock.SetTexture(HDShaderIDs._BlitTexture, input);
			this.m_RecorderPropertyBlock.SetVector(HDShaderIDs._BlitScaleBias, vector);
			this.m_RecorderPropertyBlock.SetFloat(HDShaderIDs._BlitMipLevel, 0f);
			cmd.SetRenderTarget(this.m_RecorderTempRT);
			cmd.DrawProcedural(Matrix4x4.identity, blitMaterial, 0, MeshTopology.Triangles, 3, 1, this.m_RecorderPropertyBlock);
			this.m_RecorderCaptureActions.Reset();
			while (this.m_RecorderCaptureActions.MoveNext())
			{
				this.m_RecorderCaptureActions.Current(this.m_RecorderTempRT, cmd);
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00040CF4 File Offset: 0x0003EEF4
		internal void ExecuteCaptureActions(RenderGraph renderGraph, RenderGraphResource input)
		{
			if (this.m_RecorderCaptureActions == null || !this.m_RecorderCaptureActions.MoveNext())
			{
				return;
			}
			HDCamera.ExecuteCaptureActionsPassData executeCaptureActionsPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDCamera.ExecuteCaptureActionsPassData>("Execute Capture Actions", out executeCaptureActionsPassData, null))
			{
				TextureDesc textureDesc = renderGraph.GetTextureDesc(in input);
				Vector4 rtHandleScale = renderGraph.rtHandleProperties.rtHandleScale;
				executeCaptureActionsPassData.viewportScale = new Vector2(rtHandleScale.x, rtHandleScale.y);
				executeCaptureActionsPassData.blitMaterial = HDUtils.GetBlitMaterial(textureDesc.dimension, false);
				executeCaptureActionsPassData.recorderCaptureActions = this.m_RecorderCaptureActions;
				executeCaptureActionsPassData.input = renderGraphBuilder.ReadTexture(in input);
				HDCamera.ExecuteCaptureActionsPassData executeCaptureActionsPassData2 = executeCaptureActionsPassData;
				RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(this.actualWidth, this.actualHeight, false, false)
				{
					colorFormat = textureDesc.colorFormat,
					name = "TempCaptureActions"
				}, 0);
				executeCaptureActionsPassData2.tempTexture = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				renderGraphBuilder.SetRenderFunc<HDCamera.ExecuteCaptureActionsPassData>(delegate(HDCamera.ExecuteCaptureActionsPassData data, RenderGraphContext ctx)
				{
					RenderGraphResourceRegistry resources = ctx.resources;
					RenderGraphResource renderGraphResource = data.tempTexture;
					RTHandle texture = resources.GetTexture(in renderGraphResource);
					MaterialPropertyBlock tempMaterialPropertyBlock = ctx.renderGraphPool.GetTempMaterialPropertyBlock();
					tempMaterialPropertyBlock.SetTexture(HDShaderIDs._BlitTexture, ctx.resources.GetTexture(in data.input));
					tempMaterialPropertyBlock.SetVector(HDShaderIDs._BlitScaleBias, data.viewportScale);
					tempMaterialPropertyBlock.SetFloat(HDShaderIDs._BlitMipLevel, 0f);
					ctx.cmd.SetRenderTarget(texture);
					ctx.cmd.DrawProcedural(Matrix4x4.identity, data.blitMaterial, 0, MeshTopology.Triangles, 3, 1, tempMaterialPropertyBlock);
					data.recorderCaptureActions.Reset();
					while (data.recorderCaptureActions.MoveNext())
					{
						data.recorderCaptureActions.Current(texture, ctx.cmd);
					}
				});
			}
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00040E10 File Offset: 0x0003F010
		internal void UpdateCurrentSky(SkyManager skyManager)
		{
			this.skyAmbientMode = this.volumeStack.GetComponent<VisualEnvironment>().skyAmbientMode.value;
			this.visualSky.skySettings = SkyManager.GetSkySetting(this.volumeStack);
			VolumeManager.instance.Update(skyManager.lightingOverrideVolumeStack, this.volumeAnchor, skyManager.lightingOverrideLayerMask);
			if (VolumeManager.instance.IsComponentActiveInMask<VisualEnvironment>(skyManager.lightingOverrideLayerMask))
			{
				SkySettings skySetting = SkyManager.GetSkySetting(skyManager.lightingOverrideVolumeStack);
				if (this.m_LightingOverrideSky.skySettings != null && skySetting == null)
				{
					this.visualSky.skyParametersHash = -1;
				}
				this.m_LightingOverrideSky.skySettings = skySetting;
				this.lightingSky = this.m_LightingOverrideSky;
				return;
			}
			this.lightingSky = this.visualSky;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00040ED8 File Offset: 0x0003F0D8
		private void SetupCurrentMaterialQuality(CommandBuffer cmd)
		{
			HDRenderPipelineAsset currentAsset = HDRenderPipeline.currentAsset;
			MaterialQuality availableMaterialQualityLevels = currentAsset.availableMaterialQualityLevels;
			MaterialQuality materialQuality = ((this.frameSettings.materialQuality == (MaterialQuality)0) ? currentAsset.defaultMaterialQualityLevel : this.frameSettings.materialQuality);
			availableMaterialQualityLevels.GetClosestQuality(materialQuality).SetGlobalShaderKeywords(cmd);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00040F20 File Offset: 0x0003F120
		private void UpdateAntialiasing()
		{
			HDAdditionalCameraData.AntialiasingMode antialiasing = this.antialiasing;
			if (!this.frameSettings.IsEnabled(FrameSettingsField.Postprocess) || !CoreUtils.ArePostProcessesEnabled(this.camera))
			{
				this.antialiasing = HDAdditionalCameraData.AntialiasingMode.None;
			}
			else if (this.m_AdditionalCameraData != null)
			{
				this.antialiasing = this.m_AdditionalCameraData.antialiasing;
				this.SMAAQuality = this.m_AdditionalCameraData.SMAAQuality;
				this.taaSharpenStrength = this.m_AdditionalCameraData.taaSharpenStrength;
			}
			else
			{
				this.antialiasing = HDAdditionalCameraData.AntialiasingMode.None;
			}
			if (this.antialiasing != HDAdditionalCameraData.AntialiasingMode.TemporalAntialiasing)
			{
				this.taaFrameIndex = 0;
				this.taaJitter = Vector4.zero;
			}
			if (antialiasing != this.antialiasing && this.antialiasing == HDAdditionalCameraData.AntialiasingMode.TemporalAntialiasing)
			{
				this.resetPostProcessingHistory = true;
			}
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00040FD8 File Offset: 0x0003F1D8
		private void GetXrViewParameters(int xrViewIndex, out Matrix4x4 proj, out Matrix4x4 view, out Vector3 cameraPosition)
		{
			proj = this.xr.GetProjMatrix(xrViewIndex);
			view = this.xr.GetViewMatrix(xrViewIndex);
			cameraPosition = view.inverse.GetColumn(3);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00041024 File Offset: 0x0003F224
		private void UpdateAllViewConstants()
		{
			if (this.m_XRViewConstants == null || this.m_XRViewConstants.Length != this.viewCount)
			{
				this.m_XRViewConstants = new HDCamera.ViewConstants[this.viewCount];
			}
			this.UpdateAllViewConstants(this.IsTAAEnabled(), true);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0004105C File Offset: 0x0003F25C
		private void UpdateAllViewConstants(bool jitterProjectionMatrix, bool updatePreviousFrameConstants)
		{
			Matrix4x4 projectionMatrix = this.camera.projectionMatrix;
			Matrix4x4 worldToCameraMatrix = this.camera.worldToCameraMatrix;
			Vector3 position = this.camera.transform.position;
			if (this.xr.enabled && this.viewCount == 1)
			{
				this.GetXrViewParameters(0, out projectionMatrix, out worldToCameraMatrix, out position);
			}
			this.UpdateViewConstants(ref this.mainViewConstants, projectionMatrix, worldToCameraMatrix, position, jitterProjectionMatrix, updatePreviousFrameConstants);
			if (this.xr.singlePassEnabled)
			{
				for (int i = 0; i < this.viewCount; i++)
				{
					this.GetXrViewParameters(i, out projectionMatrix, out worldToCameraMatrix, out position);
					this.UpdateViewConstants(ref this.m_XRViewConstants[i], projectionMatrix, worldToCameraMatrix, position, jitterProjectionMatrix, updatePreviousFrameConstants);
					this.m_XRViewConstants[i].worldSpaceCameraPosViewOffset = this.m_XRViewConstants[i].worldSpaceCameraPos - this.mainViewConstants.worldSpaceCameraPos;
				}
			}
			else
			{
				this.m_XRViewConstants[0] = this.mainViewConstants;
			}
			this.UpdateFrustum(in this.mainViewConstants);
			this.m_RecorderCaptureActions = CameraCaptureBridge.GetCaptureActions(this.camera);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0004116C File Offset: 0x0003F36C
		private void UpdateViewConstants(ref HDCamera.ViewConstants viewConstants, Matrix4x4 projMatrix, Matrix4x4 viewMatrix, Vector3 cameraPosition, bool jitterProjectionMatrix, bool updatePreviousFrameConstants)
		{
			Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(jitterProjectionMatrix ? this.GetJitteredProjectionMatrix(projMatrix) : projMatrix, true);
			Matrix4x4 matrix4x = viewMatrix;
			Matrix4x4 gpuprojectionMatrix2 = GL.GetGPUProjectionMatrix(projMatrix, true);
			if (ShaderConfig.s_CameraRelativeRendering != 0)
			{
				matrix4x.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
			}
			Matrix4x4 matrix4x2 = gpuprojectionMatrix2 * matrix4x;
			if (updatePreviousFrameConstants)
			{
				if (this.isFirstFrame)
				{
					viewConstants.prevWorldSpaceCameraPos = cameraPosition;
					viewConstants.prevViewProjMatrix = matrix4x2;
					viewConstants.prevInvViewProjMatrix = viewConstants.prevViewProjMatrix.inverse;
				}
				else
				{
					viewConstants.prevWorldSpaceCameraPos = viewConstants.worldSpaceCameraPos;
					viewConstants.prevViewProjMatrix = viewConstants.nonJitteredViewProjMatrix;
					viewConstants.prevViewProjMatrixNoCameraTrans = viewConstants.prevViewProjMatrix;
				}
			}
			viewConstants.viewMatrix = matrix4x;
			viewConstants.invViewMatrix = matrix4x.inverse;
			viewConstants.projMatrix = gpuprojectionMatrix;
			viewConstants.invProjMatrix = gpuprojectionMatrix.inverse;
			viewConstants.viewProjMatrix = gpuprojectionMatrix * matrix4x;
			viewConstants.invViewProjMatrix = viewConstants.viewProjMatrix.inverse;
			viewConstants.nonJitteredViewProjMatrix = gpuprojectionMatrix2 * matrix4x;
			viewConstants.worldSpaceCameraPos = cameraPosition;
			viewConstants.worldSpaceCameraPosViewOffset = Vector3.zero;
			float num = HDUtils.ProjectionMatrixAspect(in gpuprojectionMatrix);
			viewConstants.pixelCoordToViewDirWS = this.ComputePixelCoordToWorldSpaceViewDirectionMatrix(viewConstants, this.screenSize, num);
			if (updatePreviousFrameConstants)
			{
				Vector3 vector = viewConstants.worldSpaceCameraPos - viewConstants.prevWorldSpaceCameraPos;
				viewConstants.prevWorldSpaceCameraPos -= viewConstants.worldSpaceCameraPos;
				viewConstants.prevViewProjMatrix *= Matrix4x4.Translate(vector);
				viewConstants.prevInvViewProjMatrix = viewConstants.prevViewProjMatrix.inverse;
				return;
			}
			Matrix4x4 matrix4x3 = viewMatrix;
			matrix4x3.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
			viewConstants.prevViewProjMatrixNoCameraTrans = gpuprojectionMatrix2 * matrix4x3;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0004133C File Offset: 0x0003F53C
		private void UpdateFrustum(in HDCamera.ViewConstants viewConstants)
		{
			Matrix4x4 matrix4x = this.mainViewConstants.projMatrix;
			Matrix4x4 matrix4x2 = this.mainViewConstants.invProjMatrix;
			Matrix4x4 matrix4x3 = this.mainViewConstants.viewProjMatrix;
			if (this.xr.enabled)
			{
				Matrix4x4 stereoProjectionMatrix = this.xr.cullingParams.stereoProjectionMatrix;
				Matrix4x4 stereoViewMatrix = this.xr.cullingParams.stereoViewMatrix;
				if (ShaderConfig.s_CameraRelativeRendering != 0)
				{
					Vector4 vector = stereoViewMatrix.inverse.GetColumn(3) - this.camera.transform.position;
					stereoViewMatrix.SetColumn(3, vector);
				}
				matrix4x = GL.GetGPUProjectionMatrix(stereoProjectionMatrix, true);
				matrix4x2 = matrix4x.inverse;
				matrix4x3 = matrix4x * stereoViewMatrix;
			}
			float nearClipPlane = this.camera.nearClipPlane;
			float farClipPlane = this.camera.farClipPlane;
			float num = matrix4x[2, 3] / (farClipPlane * nearClipPlane) * (farClipPlane - nearClipPlane);
			Mathf.Abs(num);
			bool flag = num > 0f;
			bool flag2 = matrix4x2.MultiplyPoint(new Vector3(0f, 1f, 0f)).y < 0f;
			if (flag)
			{
				this.zBufferParams = new Vector4(-1f + farClipPlane / nearClipPlane, 1f, -1f / farClipPlane + 1f / nearClipPlane, 1f / farClipPlane);
			}
			else
			{
				this.zBufferParams = new Vector4(1f - farClipPlane / nearClipPlane, farClipPlane / nearClipPlane, 1f / farClipPlane - 1f / nearClipPlane, 1f / nearClipPlane);
			}
			this.projectionParams = new Vector4((float)(flag2 ? (-1) : 1), nearClipPlane, farClipPlane, 1f / farClipPlane);
			float num2 = (this.camera.orthographic ? (2f * this.camera.orthographicSize) : 0f);
			float num3 = num2 * this.camera.aspect;
			this.unity_OrthoParams = new Vector4(num3, num2, 0f, (float)(this.camera.orthographic ? 1 : 0));
			Matrix4x4 matrix4x4 = viewConstants.invViewMatrix;
			Vector3 vector2 = -matrix4x4.GetColumn(2);
			vector2.Normalize();
			Matrix4x4 matrix4x5 = matrix4x3;
			matrix4x4 = viewConstants.invViewMatrix;
			Frustum.Create(ref this.frustum, matrix4x5, matrix4x4.GetColumn(3), vector2, nearClipPlane, farClipPlane);
			for (int i = 0; i < 6; i++)
			{
				this.frustumPlaneEquations[i] = new Vector4(this.frustum.planes[i].normal.x, this.frustum.planes[i].normal.y, this.frustum.planes[i].normal.z, this.frustum.planes[i].distance);
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00041620 File Offset: 0x0003F820
		private void UpdateVolumeAndPhysicalParameters()
		{
			this.volumeAnchor = null;
			this.volumeLayerMask = -1;
			this.physicalParameters = null;
			if (this.m_AdditionalCameraData != null)
			{
				this.volumeLayerMask = this.m_AdditionalCameraData.volumeLayerMask;
				this.volumeAnchor = this.m_AdditionalCameraData.volumeAnchorOverride;
				this.physicalParameters = this.m_AdditionalCameraData.physicalParameters;
			}
			else if (this.camera.cameraType == CameraType.SceneView)
			{
				Camera main = Camera.main;
				bool flag = true;
				HDAdditionalCameraData hdadditionalCameraData;
				if (main != null && main.TryGetComponent<HDAdditionalCameraData>(out hdadditionalCameraData))
				{
					this.volumeLayerMask = hdadditionalCameraData.volumeLayerMask;
					this.volumeAnchor = hdadditionalCameraData.volumeAnchorOverride;
					this.physicalParameters = hdadditionalCameraData.physicalParameters;
					flag = false;
				}
				if (flag)
				{
					HDRenderPipeline hdrenderPipeline = RenderPipelineManager.currentPipeline as HDRenderPipeline;
					if (hdrenderPipeline.asset.currentPlatformRenderPipelineSettings.lightLoopSettings.skyLightingOverrideLayerMask == -1)
					{
						this.volumeLayerMask = -1;
					}
					else
					{
						this.volumeLayerMask = -1 & ~(hdrenderPipeline.asset.currentPlatformRenderPipelineSettings.lightLoopSettings.skyLightingOverrideLayerMask | int.MinValue);
					}
				}
			}
			if (this.volumeAnchor == null)
			{
				this.volumeAnchor = this.camera.transform;
			}
			using (new ProfilingScope(null, ProfilingSampler.Get<HDProfileId>(HDProfileId.VolumeUpdate)))
			{
				VolumeManager.instance.Update(this.volumeStack, this.volumeAnchor, this.volumeLayerMask);
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x000417B0 File Offset: 0x0003F9B0
		private Matrix4x4 GetJitteredProjectionMatrix(Matrix4x4 origProj)
		{
			if (this.xr.enabled)
			{
				this.taaJitter = Vector4.zero;
				return origProj;
			}
			float num = HaltonSequence.Get((this.taaFrameIndex & 1023) + 1, 2) - 0.5f;
			float num2 = HaltonSequence.Get((this.taaFrameIndex & 1023) + 1, 3) - 0.5f;
			this.taaJitter = new Vector4(num, num2, num / (float)this.actualWidth, num2 / (float)this.actualHeight);
			int num3 = this.taaFrameIndex + 1;
			this.taaFrameIndex = num3;
			if (num3 >= 8)
			{
				this.taaFrameIndex = 0;
			}
			Matrix4x4 matrix4x;
			if (this.camera.orthographic)
			{
				float orthographicSize = this.camera.orthographicSize;
				float num4 = orthographicSize * this.camera.aspect;
				Vector4 vector = this.taaJitter;
				vector.x *= num4 / (0.5f * (float)this.actualWidth);
				vector.y *= orthographicSize / (0.5f * (float)this.actualHeight);
				float num5 = vector.x - num4;
				float num6 = vector.x + num4;
				float num7 = vector.y + orthographicSize;
				float num8 = vector.y - orthographicSize;
				matrix4x = Matrix4x4.Ortho(num5, num6, num8, num7, this.camera.nearClipPlane, this.camera.farClipPlane);
			}
			else
			{
				FrustumPlanes decomposeProjection = origProj.decomposeProjection;
				float num9 = Math.Abs(decomposeProjection.top) + Math.Abs(decomposeProjection.bottom);
				float num10 = Math.Abs(decomposeProjection.left) + Math.Abs(decomposeProjection.right);
				Vector2 vector2 = new Vector2(num * num10 / (float)this.actualWidth, num2 * num9 / (float)this.actualHeight);
				decomposeProjection.left += vector2.x;
				decomposeProjection.right += vector2.x;
				decomposeProjection.top += vector2.y;
				decomposeProjection.bottom += vector2.y;
				matrix4x = Matrix4x4.Frustum(decomposeProjection);
			}
			return matrix4x;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x000419B8 File Offset: 0x0003FBB8
		private Matrix4x4 ComputePixelCoordToWorldSpaceViewDirectionMatrix(HDCamera.ViewConstants viewConstants, Vector4 resolution, float aspect = -1f)
		{
			if (this.xr.enabled)
			{
				Matrix4x4 matrix4x = Matrix4x4.Scale(new Vector3(-1f, -1f, -1f)) * viewConstants.invViewProjMatrix;
				matrix4x *= Matrix4x4.Scale(new Vector3(1f, -1f, 1f));
				matrix4x *= Matrix4x4.Translate(new Vector3(-1f, -1f, 0f));
				return (matrix4x * Matrix4x4.Scale(new Vector3(2f * resolution.z, 2f * resolution.w, 1f))).transpose;
			}
			float num = this.camera.GetGateFittedFieldOfView() * 0.017453292f;
			Vector2 gateFittedLensShift = this.camera.GetGateFittedLensShift();
			return HDUtils.ComputePixelCoordToWorldSpaceViewDirectionMatrix(num, gateFittedLensShift, resolution, viewConstants.viewMatrix, false, aspect);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00041A9C File Offset: 0x0003FC9C
		private void Dispose()
		{
			VolumeManager.instance.DestroyStack(this.volumeStack);
			if (this.m_HistoryRTSystem != null)
			{
				this.m_HistoryRTSystem.Dispose();
				this.m_HistoryRTSystem = null;
			}
			if (this.lightingSky != null && this.lightingSky != this.visualSky)
			{
				this.lightingSky.Cleanup();
			}
			if (this.visualSky != null)
			{
				this.visualSky.Cleanup();
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00041B08 File Offset: 0x0003FD08
		private static RTHandle HistoryBufferAllocatorFunction(string viewName, int frameIndex, RTHandleSystem rtHandleSystem)
		{
			frameIndex &= 1;
			HDRenderPipeline hdrenderPipeline = (HDRenderPipeline)RenderPipelineManager.currentPipeline;
			return rtHandleSystem.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, (GraphicsFormat)hdrenderPipeline.currentPlatformRenderPipelineSettings.colorBufferFormat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, true, false, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, string.Format("CameraColorBufferMipChain{0}", frameIndex));
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00041B66 File Offset: 0x0003FD66
		private void ReleaseHistoryBuffer()
		{
			this.m_HistoryRTSystem.ReleaseAll();
		}

		// Token: 0x04000889 RID: 2185
		public Vector4 screenSize;

		// Token: 0x0400088A RID: 2186
		public Frustum frustum;

		// Token: 0x0400088B RID: 2187
		public Camera camera;

		// Token: 0x0400088C RID: 2188
		public Vector4 taaJitter;

		// Token: 0x0400088D RID: 2189
		public HDCamera.ViewConstants mainViewConstants;

		// Token: 0x0400088E RID: 2190
		public bool colorPyramidHistoryIsValid;

		// Token: 0x0400088F RID: 2191
		public bool volumetricHistoryIsValid;

		// Token: 0x04000895 RID: 2197
		public float time;

		// Token: 0x04000896 RID: 2198
		internal Vector4[] frustumPlaneEquations;

		// Token: 0x04000897 RID: 2199
		internal int taaFrameIndex;

		// Token: 0x04000898 RID: 2200
		internal float taaSharpenStrength;

		// Token: 0x04000899 RID: 2201
		internal Vector4 zBufferParams;

		// Token: 0x0400089A RID: 2202
		internal Vector4 unity_OrthoParams;

		// Token: 0x0400089B RID: 2203
		internal Vector4 projectionParams;

		// Token: 0x0400089C RID: 2204
		internal Vector4 screenParams;

		// Token: 0x0400089D RID: 2205
		internal int volumeLayerMask;

		// Token: 0x0400089E RID: 2206
		internal Transform volumeAnchor;

		// Token: 0x0400089F RID: 2207
		internal Rect finalViewport;

		// Token: 0x040008A0 RID: 2208
		internal int colorPyramidHistoryMipCount;

		// Token: 0x040008A1 RID: 2209
		internal VBufferParameters[] vBufferParams;

		// Token: 0x040008A2 RID: 2210
		internal uint cameraFrameCount;

		// Token: 0x040008A3 RID: 2211
		internal bool animateMaterials;

		// Token: 0x040008A4 RID: 2212
		internal float lastTime;

		// Token: 0x040008A5 RID: 2213
		internal Camera parentCamera;

		// Token: 0x040008A6 RID: 2214
		internal HDCamera.ShadowHistoryUsage[] shadowHistoryUsage;

		// Token: 0x040008A7 RID: 2215
		internal SkyUpdateContext m_LightingOverrideSky = new SkyUpdateContext();

		// Token: 0x040008AF RID: 2223
		internal bool resetPostProcessingHistory = true;

		// Token: 0x040008B1 RID: 2225
		private static Dictionary<ValueTuple<Camera, int>, HDCamera> s_Cameras = new Dictionary<ValueTuple<Camera, int>, HDCamera>();

		// Token: 0x040008B2 RID: 2226
		private static List<ValueTuple<Camera, int>> s_Cleanup = new List<ValueTuple<Camera, int>>();

		// Token: 0x040008B3 RID: 2227
		private HDAdditionalCameraData m_AdditionalCameraData;

		// Token: 0x040008B4 RID: 2228
		private BufferedRTHandleSystem m_HistoryRTSystem = new BufferedRTHandleSystem();

		// Token: 0x040008B5 RID: 2229
		private int m_NumColorPyramidBuffersAllocated;

		// Token: 0x040008B6 RID: 2230
		private int m_NumVolumetricBuffersAllocated;

		// Token: 0x040008B7 RID: 2231
		private float m_AmbientOcclusionResolutionScale;

		// Token: 0x040008B8 RID: 2232
		private HDCamera.ViewConstants[] m_XRViewConstants;

		// Token: 0x040008B9 RID: 2233
		private Matrix4x4[] m_XRViewMatrix = new Matrix4x4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040008BA RID: 2234
		private Matrix4x4[] m_XRInvViewMatrix = new Matrix4x4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040008BB RID: 2235
		private Matrix4x4[] m_XRProjMatrix = new Matrix4x4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040008BC RID: 2236
		private Matrix4x4[] m_XRInvProjMatrix = new Matrix4x4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040008BD RID: 2237
		private Matrix4x4[] m_XRViewProjMatrix = new Matrix4x4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040008BE RID: 2238
		private Matrix4x4[] m_XRInvViewProjMatrix = new Matrix4x4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040008BF RID: 2239
		private Matrix4x4[] m_XRNonJitteredViewProjMatrix = new Matrix4x4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040008C0 RID: 2240
		private Matrix4x4[] m_XRPrevViewProjMatrix = new Matrix4x4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040008C1 RID: 2241
		private Matrix4x4[] m_XRPrevInvViewProjMatrix = new Matrix4x4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040008C2 RID: 2242
		private Matrix4x4[] m_XRPrevViewProjMatrixNoCameraTrans = new Matrix4x4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040008C3 RID: 2243
		private Matrix4x4[] m_XRPixelCoordToViewDirWS = new Matrix4x4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040008C4 RID: 2244
		private Vector4[] m_XRWorldSpaceCameraPos = new Vector4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040008C5 RID: 2245
		private Vector4[] m_XRWorldSpaceCameraPosViewOffset = new Vector4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040008C6 RID: 2246
		private Vector4[] m_XRPrevWorldSpaceCameraPos = new Vector4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040008C7 RID: 2247
		private IEnumerator<Action<RenderTargetIdentifier, CommandBuffer>> m_RecorderCaptureActions;

		// Token: 0x040008C8 RID: 2248
		private int m_RecorderTempRT = Shader.PropertyToID("TempRecorder");

		// Token: 0x040008C9 RID: 2249
		private MaterialPropertyBlock m_RecorderPropertyBlock = new MaterialPropertyBlock();

		// Token: 0x02000262 RID: 610
		public struct ViewConstants
		{
			// Token: 0x040015B4 RID: 5556
			public Matrix4x4 viewMatrix;

			// Token: 0x040015B5 RID: 5557
			public Matrix4x4 invViewMatrix;

			// Token: 0x040015B6 RID: 5558
			public Matrix4x4 projMatrix;

			// Token: 0x040015B7 RID: 5559
			public Matrix4x4 invProjMatrix;

			// Token: 0x040015B8 RID: 5560
			public Matrix4x4 viewProjMatrix;

			// Token: 0x040015B9 RID: 5561
			public Matrix4x4 invViewProjMatrix;

			// Token: 0x040015BA RID: 5562
			public Matrix4x4 nonJitteredViewProjMatrix;

			// Token: 0x040015BB RID: 5563
			public Matrix4x4 prevViewProjMatrix;

			// Token: 0x040015BC RID: 5564
			public Matrix4x4 prevInvViewProjMatrix;

			// Token: 0x040015BD RID: 5565
			public Matrix4x4 prevViewProjMatrixNoCameraTrans;

			// Token: 0x040015BE RID: 5566
			public Matrix4x4 pixelCoordToViewDirWS;

			// Token: 0x040015BF RID: 5567
			public Vector3 worldSpaceCameraPos;

			// Token: 0x040015C0 RID: 5568
			internal float pad0;

			// Token: 0x040015C1 RID: 5569
			public Vector3 worldSpaceCameraPosViewOffset;

			// Token: 0x040015C2 RID: 5570
			internal float pad1;

			// Token: 0x040015C3 RID: 5571
			public Vector3 prevWorldSpaceCameraPos;

			// Token: 0x040015C4 RID: 5572
			internal float pad2;
		}

		// Token: 0x02000263 RID: 611
		internal struct ShadowHistoryUsage
		{
			// Token: 0x040015C5 RID: 5573
			public int lightInstanceID;

			// Token: 0x040015C6 RID: 5574
			public uint frameCount;

			// Token: 0x040015C7 RID: 5575
			public GPULightType lightType;
		}

		// Token: 0x02000264 RID: 612
		private class ExecuteCaptureActionsPassData
		{
			// Token: 0x040015C8 RID: 5576
			public RenderGraphResource input;

			// Token: 0x040015C9 RID: 5577
			public RenderGraphMutableResource tempTexture;

			// Token: 0x040015CA RID: 5578
			public IEnumerator<Action<RenderTargetIdentifier, CommandBuffer>> recorderCaptureActions;

			// Token: 0x040015CB RID: 5579
			public Vector2 viewportScale;

			// Token: 0x040015CC RID: 5580
			public Material blitMaterial;
		}

		// Token: 0x02000265 RID: 613
		private struct AmbientOcclusionAllocator
		{
			// Token: 0x06000C5E RID: 3166 RVA: 0x0005931B File Offset: 0x0005751B
			public AmbientOcclusionAllocator(float scaleFactor)
			{
				this.scaleFactor = scaleFactor;
			}

			// Token: 0x06000C5F RID: 3167 RVA: 0x00059324 File Offset: 0x00057524
			public RTHandle Allocator(string id, int frameIndex, RTHandleSystem rtHandleSystem)
			{
				return rtHandleSystem.Alloc(Vector2.one * this.scaleFactor, TextureXR.slices, DepthBits.None, GraphicsFormat.R32_UInt, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, string.Format("AO Packed history_{0}", frameIndex));
			}

			// Token: 0x040015CD RID: 5581
			private float scaleFactor;
		}
	}
}
