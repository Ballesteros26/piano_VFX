using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.XR;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000155 RID: 341
	internal class XRSystem
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x0004E4D1 File Offset: 0x0004C6D1
		internal static void SetCustomLayout(XRSystem.CustomLayout cb)
		{
			XRSystem.customLayout = cb;
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0004E4D9 File Offset: 0x0004C6D9
		private static bool testModeEnabledInitialization
		{
			get
			{
				return Array.Exists<string>(Environment.GetCommandLineArgs(), (string arg) => arg == "-xr-tests");
			}
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0004E504 File Offset: 0x0004C704
		internal XRSystem(RenderPipelineResources.ShaderResources shaders)
		{
			this.RefreshXrSdk();
			if (shaders != null)
			{
				this.occlusionMeshMaterial = CoreUtils.CreateEngineMaterial(shaders.xrOcclusionMeshPS);
				this.mirrorViewMaterial = CoreUtils.CreateEngineMaterial(shaders.xrMirrorViewPS);
			}
			TextureXR.maxViews = Math.Max(TextureXR.slices, this.GetMaxViews());
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0004E57C File Offset: 0x0004C77C
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		internal static void XRSystemInit()
		{
			SubsystemManager.GetInstances<XRDisplaySubsystem>(XRSystem.displayList);
			for (int i = 0; i < XRSystem.displayList.Count; i++)
			{
				XRSystem.displayList[i].disableLegacyRenderer = true;
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0004E5BC File Offset: 0x0004C7BC
		internal int GetMaxViews()
		{
			int num = 1;
			if (this.display != null)
			{
				num = 2;
			}
			else if (XRGraphics.stereoRenderingMode == XRGraphics.StereoRenderingMode.SinglePassInstanced)
			{
				num = 2;
			}
			if (XRSystem.testModeEnabled)
			{
				num = Math.Max(num, 2);
			}
			return num;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0004E5F4 File Offset: 0x0004C7F4
		internal List<ValueTuple<Camera, XRPass>> SetupFrame(Camera[] cameras, bool singlePassAllowed, bool singlePassTestModeActive)
		{
			bool flag = this.RefreshXrSdk();
			if (this.framePasses.Count > 0)
			{
				Debug.LogWarning("XRSystem.ReleaseFrame() was not called!");
				this.ReleaseFrame();
			}
			if ((singlePassTestModeActive || XRSystem.automatedTestRunning) && XRSystem.testModeEnabled)
			{
				XRSystem.SetCustomLayout(new XRSystem.CustomLayout(this.LayoutSinglePassTestMode));
			}
			else
			{
				XRSystem.SetCustomLayout(null);
			}
			foreach (Camera camera in cameras)
			{
				if (!(camera == null))
				{
					bool flag2 = flag || (camera.stereoEnabled && XRGraphics.enabled);
					bool flag3 = camera.cameraType == CameraType.Game && camera.targetTexture == null;
					if (XRSystem.customLayout == null || !XRSystem.customLayout(new XRLayout
					{
						camera = camera,
						xrSystem = this
					}))
					{
						if (flag2 && flag3)
						{
							QualitySettings.vSyncCount = 0;
							if (XRGraphics.renderViewportScale != 1f)
							{
								Debug.LogWarning("RenderViewportScale has no effect with this render pipeline. Use dynamic resolution instead.");
							}
							if (flag)
							{
								this.CreateLayoutFromXrSdk(camera, singlePassAllowed);
							}
							else
							{
								this.CreateLayoutLegacyStereo(camera);
							}
						}
						else
						{
							this.AddPassToFrame(camera, this.emptyPass);
						}
					}
				}
			}
			this.CaptureDebugInfo();
			return this.framePasses;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0004E728 File Offset: 0x0004C928
		internal void ReleaseFrame()
		{
			foreach (ValueTuple<Camera, XRPass> valueTuple in this.framePasses)
			{
				XRPass item = valueTuple.Item2;
				if (item != this.emptyPass)
				{
					XRPass.Release(item);
				}
			}
			this.framePasses.Clear();
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0004E794 File Offset: 0x0004C994
		private bool RefreshXrSdk()
		{
			SubsystemManager.GetInstances<XRDisplaySubsystem>(XRSystem.displayList);
			if (XRSystem.displayList.Count <= 0)
			{
				this.display = null;
				return false;
			}
			if (XRSystem.displayList.Count > 1)
			{
				throw new NotImplementedException("Only 1 XR display is supported.");
			}
			this.display = XRSystem.displayList[0];
			this.display.disableLegacyRenderer = true;
			return this.display.running;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0004E804 File Offset: 0x0004CA04
		private void CreateLayoutLegacyStereo(Camera camera)
		{
			ScriptableCullingParameters scriptableCullingParameters;
			if (!camera.TryGetCullingParameters(true, out scriptableCullingParameters))
			{
				Debug.LogError("Unable to get Culling Parameters from camera!");
				return;
			}
			XRPassCreateInfo xrpassCreateInfo = new XRPassCreateInfo
			{
				multipassId = 0,
				cullingPassId = 0,
				cullingParameters = scriptableCullingParameters,
				renderTarget = camera.targetTexture,
				customMirrorView = null
			};
			if (XRGraphics.stereoRenderingMode == XRGraphics.StereoRenderingMode.MultiPass)
			{
				if (camera.stereoTargetEye == StereoTargetEyeMask.Both || camera.stereoTargetEye == StereoTargetEyeMask.Left)
				{
					XRPass xrpass = XRPass.Create(xrpassCreateInfo);
					xrpass.AddView(camera, Camera.StereoscopicEye.Left, 0);
					this.AddPassToFrame(camera, xrpass);
					xrpassCreateInfo.multipassId++;
				}
				if (camera.stereoTargetEye == StereoTargetEyeMask.Both || camera.stereoTargetEye == StereoTargetEyeMask.Right)
				{
					XRPass xrpass2 = XRPass.Create(xrpassCreateInfo);
					xrpass2.AddView(camera, Camera.StereoscopicEye.Right, 1);
					this.AddPassToFrame(camera, xrpass2);
					return;
				}
			}
			else
			{
				XRPass xrpass3 = XRPass.Create(xrpassCreateInfo);
				if (camera.stereoTargetEye == StereoTargetEyeMask.Both || camera.stereoTargetEye == StereoTargetEyeMask.Left)
				{
					xrpass3.AddView(camera, Camera.StereoscopicEye.Left, 0);
				}
				if (camera.stereoTargetEye == StereoTargetEyeMask.Both || camera.stereoTargetEye == StereoTargetEyeMask.Right)
				{
					xrpass3.AddView(camera, Camera.StereoscopicEye.Right, 1);
				}
				this.AddPassToFrame(camera, xrpass3);
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0004E914 File Offset: 0x0004CB14
		private void CreateLayoutFromXrSdk(Camera camera, bool singlePassAllowed)
		{
			XRSystem.<>c__DisplayClass21_0 CS$<>8__locals1;
			CS$<>8__locals1.camera = camera;
			for (int i = 0; i < this.display.GetRenderPassCount(); i++)
			{
				XRDisplaySubsystem.XRRenderPass xrrenderPass;
				this.display.GetRenderPass(i, out xrrenderPass);
				ScriptableCullingParameters scriptableCullingParameters;
				this.display.GetCullingParameters(CS$<>8__locals1.camera, xrrenderPass.cullingPassIndex, out scriptableCullingParameters);
				if (singlePassAllowed && XRSystem.<CreateLayoutFromXrSdk>g__CanUseSinglePass|21_0(xrrenderPass, ref CS$<>8__locals1))
				{
					XRPass xrpass = XRPass.Create(xrrenderPass, this.framePasses.Count, scriptableCullingParameters, this.occlusionMeshMaterial);
					for (int j = 0; j < xrrenderPass.GetRenderParameterCount(); j++)
					{
						XRDisplaySubsystem.XRRenderParameter xrrenderParameter;
						xrrenderPass.GetRenderParameter(CS$<>8__locals1.camera, j, out xrrenderParameter);
						xrpass.AddView(xrrenderPass, xrrenderParameter);
					}
					this.AddPassToFrame(CS$<>8__locals1.camera, xrpass);
				}
				else
				{
					for (int k = 0; k < xrrenderPass.GetRenderParameterCount(); k++)
					{
						XRDisplaySubsystem.XRRenderParameter xrrenderParameter2;
						xrrenderPass.GetRenderParameter(CS$<>8__locals1.camera, k, out xrrenderParameter2);
						XRPass xrpass2 = XRPass.Create(xrrenderPass, this.framePasses.Count, scriptableCullingParameters, this.occlusionMeshMaterial);
						xrpass2.AddView(xrrenderPass, xrrenderParameter2);
						this.AddPassToFrame(CS$<>8__locals1.camera, xrpass2);
					}
				}
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0004EA2D File Offset: 0x0004CC2D
		internal void Cleanup()
		{
			XRSystem.customLayout = null;
			CoreUtils.Destroy(this.occlusionMeshMaterial);
			CoreUtils.Destroy(this.mirrorViewMaterial);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0004EA4B File Offset: 0x0004CC4B
		internal void AddPassToFrame(Camera camera, XRPass xrPass)
		{
			this.framePasses.Add(new ValueTuple<Camera, XRPass>(camera, xrPass));
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0004EA60 File Offset: 0x0004CC60
		internal void RenderMirrorView(CommandBuffer cmd)
		{
			if (this.display == null || !this.display.running)
			{
				return;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.XRMirrorView)))
			{
				cmd.SetRenderTarget(BuiltinRenderTextureType.CameraTarget);
				int preferredMirrorBlitMode = this.display.GetPreferredMirrorBlitMode();
				XRDisplaySubsystem.XRMirrorViewBlitDesc xrmirrorViewBlitDesc;
				if (this.display.GetMirrorViewBlitDesc(null, out xrmirrorViewBlitDesc, preferredMirrorBlitMode))
				{
					if (xrmirrorViewBlitDesc.nativeBlitAvailable)
					{
						this.display.AddGraphicsThreadMirrorViewBlit(cmd, xrmirrorViewBlitDesc.nativeBlitInvalidStates, preferredMirrorBlitMode);
					}
					else
					{
						for (int i = 0; i < xrmirrorViewBlitDesc.blitParamsCount; i++)
						{
							XRDisplaySubsystem.XRBlitParams xrblitParams;
							xrmirrorViewBlitDesc.GetBlitParameter(i, out xrblitParams);
							Vector4 vector = new Vector4(xrblitParams.srcRect.width, xrblitParams.srcRect.height, xrblitParams.srcRect.x, xrblitParams.srcRect.y);
							Vector4 vector2 = new Vector4(xrblitParams.destRect.width, xrblitParams.destRect.height, xrblitParams.destRect.x, xrblitParams.destRect.y);
							this.mirrorViewMaterialProperty.SetTexture(HDShaderIDs._BlitTexture, xrblitParams.srcTex);
							this.mirrorViewMaterialProperty.SetVector(HDShaderIDs._BlitScaleBias, vector);
							this.mirrorViewMaterialProperty.SetVector(HDShaderIDs._BlitScaleBiasRt, vector2);
							this.mirrorViewMaterialProperty.SetInt(HDShaderIDs._BlitTexArraySlice, xrblitParams.srcTexArraySlice);
							int num = ((xrblitParams.srcTex.dimension == TextureDimension.Tex2DArray) ? 1 : 0);
							cmd.DrawProcedural(Matrix4x4.identity, this.mirrorViewMaterial, num, MeshTopology.Quads, 4, 1, this.mirrorViewMaterialProperty);
						}
					}
				}
				else
				{
					cmd.ClearRenderTarget(true, true, Color.black);
				}
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00002646 File Offset: 0x00000846
		private void CaptureDebugInfo()
		{
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0004EC2C File Offset: 0x0004CE2C
		private bool LayoutSinglePassTestMode(XRLayout frameLayout)
		{
			Camera camera = frameLayout.camera;
			ScriptableCullingParameters scriptableCullingParameters;
			if (camera != null && camera.cameraType == CameraType.Game && camera.TryGetCullingParameters(false, out scriptableCullingParameters))
			{
				scriptableCullingParameters.stereoProjectionMatrix = camera.projectionMatrix;
				scriptableCullingParameters.stereoViewMatrix = camera.worldToCameraMatrix;
				XRPassCreateInfo xrpassCreateInfo = new XRPassCreateInfo
				{
					multipassId = 0,
					cullingPassId = 0,
					cullingParameters = scriptableCullingParameters,
					renderTarget = camera.targetTexture,
					customMirrorView = null
				};
				XRViewCreateInfo xrviewCreateInfo = new XRViewCreateInfo
				{
					projMatrix = camera.projectionMatrix,
					viewMatrix = camera.worldToCameraMatrix,
					viewport = new Rect(camera.pixelRect.x, camera.pixelRect.y, (float)camera.pixelWidth, (float)camera.pixelHeight),
					textureArraySlice = -1
				};
				XRPass xrpass = frameLayout.CreatePass(xrpassCreateInfo);
				for (int i = 0; i < TextureXR.slices; i++)
				{
					frameLayout.AddViewToPass(xrviewCreateInfo, xrpass);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0004ED6C File Offset: 0x0004CF6C
		[CompilerGenerated]
		internal static bool <CreateLayoutFromXrSdk>g__CanUseSinglePass|21_0(XRDisplaySubsystem.XRRenderPass renderPass, ref XRSystem.<>c__DisplayClass21_0 A_1)
		{
			if (renderPass.renderTargetDesc.dimension != TextureDimension.Tex2DArray)
			{
				return false;
			}
			if (renderPass.GetRenderParameterCount() != 2 || renderPass.renderTargetDesc.volumeDepth != 2)
			{
				return false;
			}
			XRDisplaySubsystem.XRRenderParameter xrrenderParameter;
			renderPass.GetRenderParameter(A_1.camera, 0, out xrrenderParameter);
			XRDisplaySubsystem.XRRenderParameter xrrenderParameter2;
			renderPass.GetRenderParameter(A_1.camera, 1, out xrrenderParameter2);
			return xrrenderParameter.textureArraySlice == 0 && xrrenderParameter2.textureArraySlice == 1 && !(xrrenderParameter.viewport != xrrenderParameter2.viewport);
		}

		// Token: 0x04000F51 RID: 3921
		internal readonly XRPass emptyPass = new XRPass();

		// Token: 0x04000F52 RID: 3922
		private List<ValueTuple<Camera, XRPass>> framePasses = new List<ValueTuple<Camera, XRPass>>();

		// Token: 0x04000F53 RID: 3923
		private static XRSystem.CustomLayout customLayout = null;

		// Token: 0x04000F54 RID: 3924
		private static List<XRDisplaySubsystem> displayList = new List<XRDisplaySubsystem>();

		// Token: 0x04000F55 RID: 3925
		private XRDisplaySubsystem display;

		// Token: 0x04000F56 RID: 3926
		private Material occlusionMeshMaterial;

		// Token: 0x04000F57 RID: 3927
		private Material mirrorViewMaterial;

		// Token: 0x04000F58 RID: 3928
		private MaterialPropertyBlock mirrorViewMaterialProperty = new MaterialPropertyBlock();

		// Token: 0x04000F59 RID: 3929
		internal static bool automatedTestRunning = false;

		// Token: 0x04000F5A RID: 3930
		internal static bool testModeEnabled = XRSystem.testModeEnabledInitialization;

		// Token: 0x02000290 RID: 656
		// (Invoke) Token: 0x06000CC2 RID: 3266
		internal delegate bool CustomLayout(XRLayout layout);
	}
}
