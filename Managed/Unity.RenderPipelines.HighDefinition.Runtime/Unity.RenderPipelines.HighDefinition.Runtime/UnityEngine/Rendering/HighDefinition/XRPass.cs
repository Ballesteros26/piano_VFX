using System;
using System.Collections.Generic;
using UnityEngine.XR;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000154 RID: 340
	internal class XRPass
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0004DF34 File Offset: 0x0004C134
		internal bool enabled
		{
			get
			{
				return this.views.Count > 0;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0004DF44 File Offset: 0x0004C144
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x0004DF4C File Offset: 0x0004C14C
		internal bool xrSdkEnabled { get; private set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0004DF55 File Offset: 0x0004C155
		// (set) Token: 0x060009E4 RID: 2532 RVA: 0x0004DF5D File Offset: 0x0004C15D
		internal bool copyDepth { get; private set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0004DF66 File Offset: 0x0004C166
		// (set) Token: 0x060009E6 RID: 2534 RVA: 0x0004DF6E File Offset: 0x0004C16E
		internal int multipassId { get; private set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0004DF77 File Offset: 0x0004C177
		// (set) Token: 0x060009E8 RID: 2536 RVA: 0x0004DF7F File Offset: 0x0004C17F
		internal int cullingPassId { get; private set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x0004DF88 File Offset: 0x0004C188
		// (set) Token: 0x060009EA RID: 2538 RVA: 0x0004DF90 File Offset: 0x0004C190
		internal RenderTargetIdentifier renderTarget { get; private set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0004DF99 File Offset: 0x0004C199
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x0004DFA1 File Offset: 0x0004C1A1
		internal RenderTextureDescriptor renderTargetDesc { get; private set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0004DFAA File Offset: 0x0004C1AA
		internal bool renderTargetValid
		{
			get
			{
				return this.renderTarget != XRPass.invalidRT;
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0004DFBC File Offset: 0x0004C1BC
		internal Matrix4x4 GetProjMatrix(int viewIndex = 0)
		{
			return this.views[viewIndex].projMatrix;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0004DFCF File Offset: 0x0004C1CF
		internal Matrix4x4 GetViewMatrix(int viewIndex = 0)
		{
			return this.views[viewIndex].viewMatrix;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0004DFE2 File Offset: 0x0004C1E2
		internal int GetTextureArraySlice(int viewIndex = 0)
		{
			return this.views[viewIndex].textureArraySlice;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0004DFF5 File Offset: 0x0004C1F5
		internal Rect GetViewport(int viewIndex = 0)
		{
			return this.views[viewIndex].viewport;
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0004E008 File Offset: 0x0004C208
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x0004E010 File Offset: 0x0004C210
		internal ScriptableCullingParameters cullingParams { get; private set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0004E019 File Offset: 0x0004C219
		internal int viewCount
		{
			get
			{
				return this.views.Count;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0004E026 File Offset: 0x0004C226
		internal bool singlePassEnabled
		{
			get
			{
				return this.viewCount > 1;
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0004E031 File Offset: 0x0004C231
		internal void SetCustomMirrorView(XRPass.CustomMirrorView callback)
		{
			this.customMirrorView = callback;
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0004E03A File Offset: 0x0004C23A
		internal int legacyMultipassEye
		{
			get
			{
				return (int)this.views[0].legacyStereoEye;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x0004E04D File Offset: 0x0004C24D
		internal bool legacyMultipassEnabled
		{
			get
			{
				return this.enabled && !this.singlePassEnabled && this.legacyMultipassEye >= 0;
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0004E070 File Offset: 0x0004C270
		internal static XRPass Create(XRPassCreateInfo createInfo)
		{
			XRPass xrpass = GenericPool<XRPass>.Get();
			xrpass.multipassId = createInfo.multipassId;
			xrpass.cullingPassId = createInfo.cullingPassId;
			xrpass.cullingParams = createInfo.cullingParameters;
			xrpass.customMirrorView = createInfo.customMirrorView;
			xrpass.views.Clear();
			if (createInfo.renderTarget != null)
			{
				xrpass.renderTarget = new RenderTargetIdentifier(createInfo.renderTarget);
				xrpass.renderTargetDesc = createInfo.renderTarget.descriptor;
			}
			else
			{
				xrpass.renderTarget = XRPass.invalidRT;
				xrpass.renderTargetDesc = default(RenderTextureDescriptor);
			}
			xrpass.occlusionMeshMaterial = null;
			xrpass.xrSdkEnabled = false;
			xrpass.copyDepth = false;
			return xrpass;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0004E120 File Offset: 0x0004C320
		internal void AddView(Camera camera, Camera.StereoscopicEye eye, int textureArraySlice = -1)
		{
			this.AddViewInternal(new XRView(camera, eye, textureArraySlice));
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0004E130 File Offset: 0x0004C330
		internal void AddView(Matrix4x4 proj, Matrix4x4 view, Rect vp, int textureArraySlice = -1)
		{
			this.AddViewInternal(new XRView(proj, view, vp, textureArraySlice));
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0004E144 File Offset: 0x0004C344
		internal static XRPass Create(XRDisplaySubsystem.XRRenderPass xrRenderPass, int multipassId, ScriptableCullingParameters cullingParameters, Material occlusionMeshMaterial)
		{
			XRPass xrpass = GenericPool<XRPass>.Get();
			xrpass.multipassId = multipassId;
			xrpass.cullingPassId = xrRenderPass.cullingPassIndex;
			xrpass.cullingParams = cullingParameters;
			xrpass.views.Clear();
			xrpass.renderTarget = xrRenderPass.renderTarget;
			xrpass.renderTargetDesc = xrRenderPass.renderTargetDesc;
			xrpass.occlusionMeshMaterial = occlusionMeshMaterial;
			xrpass.xrSdkEnabled = true;
			xrpass.copyDepth = xrRenderPass.shouldFillOutDepth;
			xrpass.customMirrorView = null;
			return xrpass;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0004E1B4 File Offset: 0x0004C3B4
		internal void AddView(XRDisplaySubsystem.XRRenderPass xrSdkRenderPass, XRDisplaySubsystem.XRRenderParameter xrSdkRenderParameter)
		{
			this.AddViewInternal(new XRView(xrSdkRenderPass, xrSdkRenderParameter));
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0004E1C3 File Offset: 0x0004C3C3
		internal static void Release(XRPass xrPass)
		{
			GenericPool<XRPass>.Release(xrPass);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0004E1CC File Offset: 0x0004C3CC
		internal void AddViewInternal(XRView xrView)
		{
			int num = Math.Min(TextureXR.slices, ShaderConfig.s_XrMaxViews);
			if (this.views.Count < num)
			{
				this.views.Add(xrView);
				return;
			}
			throw new NotImplementedException(string.Format("Invalid XR setup for single-pass, trying to add too many views! Max supported: {0}", num));
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0004E21C File Offset: 0x0004C41C
		public void StartSinglePass(CommandBuffer cmd, Camera camera, ScriptableRenderContext renderContext)
		{
			if (this.enabled)
			{
				cmd.SetViewProjectionMatrices(this.GetViewMatrix(0), this.GetProjMatrix(0));
				if (camera.stereoEnabled)
				{
					cmd.DisableScissorRect();
					cmd.SetViewport(camera.pixelRect);
					renderContext.ExecuteCommandBuffer(cmd);
					cmd.Clear();
					if (this.legacyMultipassEnabled)
					{
						renderContext.StartMultiEye(camera, this.legacyMultipassEye);
						return;
					}
					renderContext.StartMultiEye(camera);
					return;
				}
				else if (this.singlePassEnabled)
				{
					if (this.viewCount <= TextureXR.slices)
					{
						cmd.EnableShaderKeyword("STEREO_INSTANCING_ON");
						cmd.SetInstanceMultiplier((uint)this.viewCount);
						return;
					}
					throw new NotImplementedException(string.Format("Invalid XR setup for single-pass, trying to render too many views! Max supported: {0}", TextureXR.slices));
				}
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0004E2D7 File Offset: 0x0004C4D7
		public void StopSinglePass(CommandBuffer cmd, Camera camera, ScriptableRenderContext renderContext)
		{
			if (this.enabled)
			{
				if (camera.stereoEnabled)
				{
					renderContext.ExecuteCommandBuffer(cmd);
					cmd.Clear();
					renderContext.StopMultiEye(camera);
					return;
				}
				cmd.DisableShaderKeyword("STEREO_INSTANCING_ON");
				cmd.SetInstanceMultiplier(1U);
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0004E314 File Offset: 0x0004C514
		internal void EndCamera(CommandBuffer cmd, HDCamera hdCamera, ScriptableRenderContext renderContext)
		{
			if (!this.enabled)
			{
				return;
			}
			this.StopSinglePass(cmd, hdCamera.camera, renderContext);
			if (hdCamera.camera.stereoEnabled)
			{
				if (this.legacyMultipassEnabled)
				{
					renderContext.StereoEndRender(hdCamera.camera, this.legacyMultipassEye, this.legacyMultipassEye == 1);
				}
				else
				{
					renderContext.StereoEndRender(hdCamera.camera);
				}
			}
			if (this.customMirrorView != null)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.XRCustomMirrorView)))
				{
					this.customMirrorView(this, cmd, hdCamera.camera.targetTexture, hdCamera.camera.pixelRect);
				}
			}
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0004E3D4 File Offset: 0x0004C5D4
		internal void RenderOcclusionMeshes(CommandBuffer cmd, RTHandle depthBuffer)
		{
			if (this.enabled && this.xrSdkEnabled && this.occlusionMeshMaterial != null)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.XROcclusionMesh)))
				{
					Matrix4x4 matrix4x = Matrix4x4.Ortho(0f, 1f, 0f, 1f, -1f, 1f);
					for (int i = 0; i < this.viewCount; i++)
					{
						if (this.views[i].occlusionMesh != null)
						{
							CoreUtils.SetRenderTarget(cmd, depthBuffer, ClearFlag.None, 0, CubemapFace.Unknown, i);
							cmd.DrawMesh(this.views[i].occlusionMesh, matrix4x, this.occlusionMeshMaterial);
						}
					}
				}
			}
		}

		// Token: 0x04000F46 RID: 3910
		private readonly List<XRView> views = new List<XRView>(2);

		// Token: 0x04000F4D RID: 3917
		private static RenderTargetIdentifier invalidRT = -1;

		// Token: 0x04000F4F RID: 3919
		private Material occlusionMeshMaterial;

		// Token: 0x04000F50 RID: 3920
		private XRPass.CustomMirrorView customMirrorView;

		// Token: 0x0200028F RID: 655
		// (Invoke) Token: 0x06000CBE RID: 3262
		internal delegate void CustomMirrorView(XRPass pass, CommandBuffer cmd, RenderTexture rt, Rect viewport);
	}
}
