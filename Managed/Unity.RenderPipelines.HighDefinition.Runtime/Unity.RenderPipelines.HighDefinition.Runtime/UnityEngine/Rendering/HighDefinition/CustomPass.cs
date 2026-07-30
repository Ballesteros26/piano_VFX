using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000125 RID: 293
	[Serializable]
	public abstract class CustomPass : IVersionable<CustomPass.Version>
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x000487E9 File Offset: 0x000469E9
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x000487F1 File Offset: 0x000469F1
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
				this.m_ProfilingSampler = new ProfilingSampler(this.m_Name);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0004880B File Offset: 0x00046A0B
		internal ProfilingSampler profilingSampler
		{
			get
			{
				if (this.m_ProfilingSampler == null)
				{
					this.m_ProfilingSampler = new ProfilingSampler(this.m_Name ?? "Custom Pass");
				}
				return this.m_ProfilingSampler;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00048835 File Offset: 0x00046A35
		protected float fadeValue
		{
			get
			{
				return this.owner.fadeValue;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00048842 File Offset: 0x00046A42
		protected CustomPassInjectionPoint injectionPoint
		{
			get
			{
				return this.owner.injectionPoint;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00003AC0 File Offset: 0x00001CC0
		protected virtual bool executeInSceneView
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0004884F File Offset: 0x00046A4F
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x00048857 File Offset: 0x00046A57
		CustomPass.Version IVersionable<CustomPass.Version>.version
		{
			get
			{
				return this.m_Version;
			}
			set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00048860 File Offset: 0x00046A60
		internal bool WillBeExecuted(HDCamera hdCamera)
		{
			return this.enabled && (hdCamera.camera.cameraType != CameraType.SceneView || this.executeInSceneView);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00048888 File Offset: 0x00046A88
		internal void ExecuteInternal(ScriptableRenderContext renderContext, CommandBuffer cmd, HDCamera hdCamera, CullingResults cullingResult, SharedRTManager rtManager, CustomPass.RenderTargets targets, CustomPassVolume owner)
		{
			this.owner = owner;
			this.currentRTManager = rtManager;
			this.currentRenderTarget = targets;
			this.currentHDCamera = hdCamera;
			using (new ProfilingScope(cmd, this.profilingSampler))
			{
				if (!this.isSetup)
				{
					this.Setup(renderContext, cmd);
					this.isSetup = true;
				}
				this.SetCustomPassTarget(cmd);
				this.isExecuting = true;
				this.Execute(renderContext, cmd, hdCamera, cullingResult);
				this.isExecuting = false;
				if (this.targetDepthBuffer != CustomPass.TargetBuffer.Camera)
				{
					CoreUtils.SetRenderTarget(cmd, targets.cameraColorBuffer, ClearFlag.None, 0, CubemapFace.Unknown, -1);
				}
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00048934 File Offset: 0x00046B34
		internal void InternalAggregateCullingParameters(ref ScriptableCullingParameters cullingParameters, HDCamera hdCamera)
		{
			this.AggregateCullingParameters(ref cullingParameters, hdCamera);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00048940 File Offset: 0x00046B40
		~CustomPass()
		{
			this.CleanupPassInternal();
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0004896C File Offset: 0x00046B6C
		internal void CleanupPassInternal()
		{
			if (this.isSetup)
			{
				this.Cleanup();
				this.isSetup = false;
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00048984 File Offset: 0x00046B84
		private bool IsMSAAEnabled(HDCamera hdCamera)
		{
			return hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA) & (this.injectionPoint == CustomPassInjectionPoint.BeforeTransparent || this.injectionPoint == CustomPassInjectionPoint.AfterOpaqueDepthAndNormal);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x000489B8 File Offset: 0x00046BB8
		private void SetCustomPassTarget(CommandBuffer cmd)
		{
			if (this.targetColorBuffer == CustomPass.TargetBuffer.None && this.targetDepthBuffer == CustomPass.TargetBuffer.None)
			{
				return;
			}
			bool flag = this.IsMSAAEnabled(this.currentHDCamera);
			RTHandle rthandle = (flag ? this.currentRenderTarget.cameraColorMSAABuffer : this.currentRenderTarget.cameraColorBuffer);
			RTHandle depthStencilBuffer = this.currentRTManager.GetDepthStencilBuffer(flag);
			RTHandle rthandle2 = ((this.targetColorBuffer == CustomPass.TargetBuffer.Custom) ? this.currentRenderTarget.customColorBuffer.Value : rthandle);
			RTHandle rthandle3 = ((this.targetDepthBuffer == CustomPass.TargetBuffer.Custom) ? this.currentRenderTarget.customDepthBuffer.Value : depthStencilBuffer);
			if (this.targetColorBuffer == CustomPass.TargetBuffer.None && this.targetDepthBuffer != CustomPass.TargetBuffer.None)
			{
				CoreUtils.SetRenderTarget(cmd, rthandle3, this.clearFlags, 0, CubemapFace.Unknown, -1);
				return;
			}
			if (this.targetColorBuffer != CustomPass.TargetBuffer.None && this.targetDepthBuffer == CustomPass.TargetBuffer.None)
			{
				CoreUtils.SetRenderTarget(cmd, rthandle2, this.clearFlags, 0, CubemapFace.Unknown, -1);
				return;
			}
			CoreUtils.SetRenderTarget(cmd, rthandle2, rthandle3, this.clearFlags, 0, CubemapFace.Unknown, -1);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00002646 File Offset: 0x00000846
		protected virtual void AggregateCullingParameters(ref ScriptableCullingParameters cullingParameters, HDCamera hdCamera)
		{
		}

		// Token: 0x060008DD RID: 2269
		protected abstract void Execute(ScriptableRenderContext renderContext, CommandBuffer cmd, HDCamera hdCamera, CullingResults cullingResult);

		// Token: 0x060008DE RID: 2270 RVA: 0x00002646 File Offset: 0x00000846
		protected virtual void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00002646 File Offset: 0x00000846
		protected virtual void Cleanup()
		{
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00048AA4 File Offset: 0x00046CA4
		protected void SetCameraRenderTarget(CommandBuffer cmd, bool bindDepth = true, ClearFlag clearFlags = ClearFlag.None)
		{
			if (!this.isExecuting)
			{
				throw new Exception("SetCameraRenderTarget can only be called inside the CustomPass.Execute function");
			}
			if (bindDepth)
			{
				CoreUtils.SetRenderTarget(cmd, this.currentRenderTarget.cameraColorBuffer, this.currentRTManager.GetDepthStencilBuffer(this.IsMSAAEnabled(this.currentHDCamera)), clearFlags, 0, CubemapFace.Unknown, -1);
				return;
			}
			CoreUtils.SetRenderTarget(cmd, this.currentRenderTarget.cameraColorBuffer, clearFlags, 0, CubemapFace.Unknown, -1);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00048B0C File Offset: 0x00046D0C
		protected void SetCustomRenderTarget(CommandBuffer cmd, bool bindDepth = true, ClearFlag clearFlags = ClearFlag.None)
		{
			if (!this.isExecuting)
			{
				throw new Exception("SetCameraRenderTarget can only be called inside the CustomPass.Execute function");
			}
			if (bindDepth)
			{
				CoreUtils.SetRenderTarget(cmd, this.currentRenderTarget.customColorBuffer.Value, this.currentRenderTarget.customDepthBuffer.Value, clearFlags, 0, CubemapFace.Unknown, -1);
				return;
			}
			CoreUtils.SetRenderTarget(cmd, this.currentRenderTarget.customColorBuffer.Value, clearFlags, 0, CubemapFace.Unknown, -1);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00048B74 File Offset: 0x00046D74
		protected void SetRenderTargetAuto(CommandBuffer cmd)
		{
			this.SetCustomPassTarget(cmd);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00048B80 File Offset: 0x00046D80
		protected void ResolveMSAAColorBuffer(CommandBuffer cmd, HDCamera hdCamera)
		{
			if (!this.isExecuting)
			{
				throw new Exception("ResolveMSAAColorBuffer can only be called inside the CustomPass.Execute function");
			}
			if (this.IsMSAAEnabled(hdCamera))
			{
				this.currentRTManager.ResolveMSAAColor(cmd, hdCamera, this.currentRenderTarget.cameraColorMSAABuffer, this.currentRenderTarget.cameraColorBuffer);
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00048BCC File Offset: 0x00046DCC
		protected void GetCameraBuffers(out RTHandle colorBuffer, out RTHandle depthBuffer)
		{
			if (!this.isExecuting)
			{
				throw new Exception("GetCameraBuffers can only be called inside the CustomPass.Execute function");
			}
			bool flag = this.IsMSAAEnabled(this.currentHDCamera);
			colorBuffer = (flag ? this.currentRenderTarget.cameraColorMSAABuffer : this.currentRenderTarget.cameraColorBuffer);
			depthBuffer = this.currentRTManager.GetDepthStencilBuffer(flag);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00048C24 File Offset: 0x00046E24
		protected void GetCustomBuffers(out RTHandle colorBuffer, out RTHandle depthBuffer)
		{
			if (!this.isExecuting)
			{
				throw new Exception("GetCustomBuffers can only be called inside the CustomPass.Execute function");
			}
			colorBuffer = this.currentRenderTarget.customColorBuffer.Value;
			depthBuffer = this.currentRenderTarget.customDepthBuffer.Value;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00048C5D File Offset: 0x00046E5D
		protected RTHandle GetNormalBuffer()
		{
			if (!this.isExecuting)
			{
				throw new Exception("GetNormalBuffer can only be called inside the CustomPass.Execute function");
			}
			return this.currentRTManager.GetNormalBuffer(this.IsMSAAEnabled(this.currentHDCamera));
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00048C89 File Offset: 0x00046E89
		public virtual IEnumerable<Material> RegisterMaterialForInspector()
		{
			yield break;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00048C94 File Offset: 0x00046E94
		protected RenderQueueRange GetRenderQueueRange(CustomPass.RenderQueueType type)
		{
			switch (type)
			{
			case CustomPass.RenderQueueType.OpaqueNoAlphaTest:
				return HDRenderQueue.k_RenderQueue_OpaqueNoAlphaTest;
			case CustomPass.RenderQueueType.OpaqueAlphaTest:
				return HDRenderQueue.k_RenderQueue_OpaqueAlphaTest;
			case CustomPass.RenderQueueType.AllOpaque:
				return HDRenderQueue.k_RenderQueue_AllOpaque;
			case CustomPass.RenderQueueType.AfterPostProcessOpaque:
				return HDRenderQueue.k_RenderQueue_AfterPostProcessOpaque;
			case CustomPass.RenderQueueType.PreRefraction:
				return HDRenderQueue.k_RenderQueue_PreRefraction;
			case CustomPass.RenderQueueType.Transparent:
				return HDRenderQueue.k_RenderQueue_Transparent;
			case CustomPass.RenderQueueType.LowTransparent:
				return HDRenderQueue.k_RenderQueue_LowTransparent;
			case CustomPass.RenderQueueType.AllTransparent:
				return HDRenderQueue.k_RenderQueue_AllTransparent;
			case CustomPass.RenderQueueType.AllTransparentWithLowRes:
				return HDRenderQueue.k_RenderQueue_AllTransparentWithLowRes;
			case CustomPass.RenderQueueType.AfterPostProcessTransparent:
				return HDRenderQueue.k_RenderQueue_AfterPostProcessTransparent;
			}
			return HDRenderQueue.k_RenderQueue_All;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00048D16 File Offset: 0x00046F16
		public static CustomPass CreateFullScreenPass(Material fullScreenMaterial, CustomPass.TargetBuffer targetColorBuffer = CustomPass.TargetBuffer.Camera, CustomPass.TargetBuffer targetDepthBuffer = CustomPass.TargetBuffer.Camera)
		{
			return new FullScreenCustomPass
			{
				name = "FullScreen Pass",
				targetColorBuffer = targetColorBuffer,
				targetDepthBuffer = targetDepthBuffer,
				fullscreenPassMaterial = fullScreenMaterial
			};
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00048D40 File Offset: 0x00046F40
		public static CustomPass CreateDrawRenderersPass(CustomPass.RenderQueueType queue, LayerMask mask, Material overrideMaterial, string overrideMaterialPassName = "Forward", SortingCriteria sorting = SortingCriteria.CommonOpaque, ClearFlag clearFlags = ClearFlag.None, CustomPass.TargetBuffer targetColorBuffer = CustomPass.TargetBuffer.Camera, CustomPass.TargetBuffer targetDepthBuffer = CustomPass.TargetBuffer.Camera)
		{
			return new DrawRenderersCustomPass
			{
				name = "DrawRenderers Pass",
				renderQueueType = queue,
				layerMask = mask,
				overrideMaterial = overrideMaterial,
				overrideMaterialPassName = overrideMaterialPassName,
				sortingCriteria = sorting,
				clearFlags = clearFlags,
				targetColorBuffer = targetColorBuffer,
				targetDepthBuffer = targetDepthBuffer
			};
		}

		// Token: 0x04000DA0 RID: 3488
		[SerializeField]
		[FormerlySerializedAs("name")]
		private string m_Name = "Custom Pass";

		// Token: 0x04000DA1 RID: 3489
		private ProfilingSampler m_ProfilingSampler;

		// Token: 0x04000DA2 RID: 3490
		public bool enabled = true;

		// Token: 0x04000DA3 RID: 3491
		public CustomPass.TargetBuffer targetColorBuffer;

		// Token: 0x04000DA4 RID: 3492
		public CustomPass.TargetBuffer targetDepthBuffer;

		// Token: 0x04000DA5 RID: 3493
		public ClearFlag clearFlags;

		// Token: 0x04000DA6 RID: 3494
		[SerializeField]
		private bool passFoldout;

		// Token: 0x04000DA7 RID: 3495
		[NonSerialized]
		private bool isSetup;

		// Token: 0x04000DA8 RID: 3496
		private bool isExecuting;

		// Token: 0x04000DA9 RID: 3497
		private CustomPass.RenderTargets currentRenderTarget;

		// Token: 0x04000DAA RID: 3498
		private CustomPassVolume owner;

		// Token: 0x04000DAB RID: 3499
		private SharedRTManager currentRTManager;

		// Token: 0x04000DAC RID: 3500
		private HDCamera currentHDCamera;

		// Token: 0x04000DAD RID: 3501
		[SerializeField]
		private CustomPass.Version m_Version = MigrationDescription.LastVersion<CustomPass.Version>();

		// Token: 0x02000271 RID: 625
		public enum TargetBuffer
		{
			// Token: 0x04001618 RID: 5656
			Camera,
			// Token: 0x04001619 RID: 5657
			Custom,
			// Token: 0x0400161A RID: 5658
			None
		}

		// Token: 0x02000272 RID: 626
		public enum RenderQueueType
		{
			// Token: 0x0400161C RID: 5660
			OpaqueNoAlphaTest,
			// Token: 0x0400161D RID: 5661
			OpaqueAlphaTest,
			// Token: 0x0400161E RID: 5662
			AllOpaque,
			// Token: 0x0400161F RID: 5663
			AfterPostProcessOpaque,
			// Token: 0x04001620 RID: 5664
			PreRefraction,
			// Token: 0x04001621 RID: 5665
			Transparent,
			// Token: 0x04001622 RID: 5666
			LowTransparent,
			// Token: 0x04001623 RID: 5667
			AllTransparent,
			// Token: 0x04001624 RID: 5668
			AllTransparentWithLowRes,
			// Token: 0x04001625 RID: 5669
			AfterPostProcessTransparent,
			// Token: 0x04001626 RID: 5670
			All
		}

		// Token: 0x02000273 RID: 627
		internal struct RenderTargets
		{
			// Token: 0x04001627 RID: 5671
			public RTHandle cameraColorMSAABuffer;

			// Token: 0x04001628 RID: 5672
			public RTHandle cameraColorBuffer;

			// Token: 0x04001629 RID: 5673
			public Lazy<RTHandle> customColorBuffer;

			// Token: 0x0400162A RID: 5674
			public Lazy<RTHandle> customDepthBuffer;
		}

		// Token: 0x02000274 RID: 628
		private enum Version
		{
			// Token: 0x0400162C RID: 5676
			Initial
		}
	}
}
