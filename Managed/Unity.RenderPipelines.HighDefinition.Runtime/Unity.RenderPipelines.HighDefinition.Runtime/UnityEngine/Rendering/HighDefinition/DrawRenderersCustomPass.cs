using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000128 RID: 296
	[Serializable]
	internal class DrawRenderersCustomPass : CustomPass
	{
		// Token: 0x06000900 RID: 2304 RVA: 0x0004948C File Offset: 0x0004768C
		protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			this.fadeValueId = Shader.PropertyToID("_FadeValue");
			if (string.IsNullOrEmpty(this.overrideMaterialPassName) && this.overrideMaterial != null)
			{
				this.overrideMaterialPassName = this.overrideMaterial.GetPassName(this.overrideMaterialPassIndex);
			}
			DrawRenderersCustomPass.forwardShaderTags = new ShaderTagId[]
			{
				HDShaderPassNames.s_ForwardName,
				HDShaderPassNames.s_ForwardOnlyName,
				HDShaderPassNames.s_SRPDefaultUnlitName,
				HDShaderPassNames.s_EmptyName
			};
			DrawRenderersCustomPass.depthShaderTags = new ShaderTagId[]
			{
				HDShaderPassNames.s_DepthForwardOnlyName,
				HDShaderPassNames.s_DepthOnlyName,
				HDShaderPassNames.s_EmptyName
			};
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00049545 File Offset: 0x00047745
		protected override void AggregateCullingParameters(ref ScriptableCullingParameters cullingParameters, HDCamera hdCamera)
		{
			cullingParameters.cullingMask |= (uint)this.layerMask;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0004955F File Offset: 0x0004775F
		protected ShaderTagId[] GetShaderTagIds()
		{
			if (this.shaderPass == DrawRenderersCustomPass.ShaderPass.DepthPrepass)
			{
				return DrawRenderersCustomPass.depthShaderTags;
			}
			return DrawRenderersCustomPass.forwardShaderTags;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00049578 File Offset: 0x00047778
		protected override void Execute(ScriptableRenderContext renderContext, CommandBuffer cmd, HDCamera hdCamera, CullingResults cullingResult)
		{
			ShaderTagId[] shaderTagIds = this.GetShaderTagIds();
			if (this.overrideMaterial != null)
			{
				shaderTagIds[DrawRenderersCustomPass.forwardShaderTags.Length - 1] = new ShaderTagId(this.overrideMaterialPassName);
				this.overrideMaterial.SetFloat(this.fadeValueId, base.fadeValue);
			}
			if (shaderTagIds.Length == 0)
			{
				Debug.LogWarning("Attempt to call DrawRenderers with an empty shader passes. Skipping the call to avoid errors");
				return;
			}
			RenderStateMask renderStateMask = (this.overrideDepthState ? RenderStateMask.Depth : RenderStateMask.Nothing);
			renderStateMask |= ((this.overrideDepthState && !this.depthWrite) ? RenderStateMask.Stencil : RenderStateMask.Nothing);
			RenderStateBlock renderStateBlock = new RenderStateBlock(renderStateMask)
			{
				depthState = new DepthState(this.depthWrite, this.depthCompareFunction),
				stencilState = new StencilState(false, byte.MaxValue, byte.MaxValue, CompareFunction.Always, StencilOp.Keep, StencilOp.Keep, StencilOp.Keep)
			};
			PerObjectData perObjectData = (hdCamera.frameSettings.IsEnabled(FrameSettingsField.Shadowmask) ? (PerObjectData.LightProbe | PerObjectData.LightProbeProxyVolume | PerObjectData.Lightmaps | PerObjectData.OcclusionProbe | PerObjectData.OcclusionProbeProxyVolume | PerObjectData.ShadowMask) : (PerObjectData.LightProbe | PerObjectData.LightProbeProxyVolume | PerObjectData.Lightmaps));
			RendererListDesc rendererListDesc = new RendererListDesc(shaderTagIds, cullingResult, hdCamera.camera)
			{
				rendererConfiguration = perObjectData,
				renderQueueRange = base.GetRenderQueueRange(this.renderQueueType),
				sortingCriteria = this.sortingCriteria,
				excludeObjectMotionVectors = false,
				overrideMaterial = this.overrideMaterial,
				overrideMaterialPassIndex = ((this.overrideMaterial != null) ? this.overrideMaterial.FindPass(this.overrideMaterialPassName) : 0),
				stateBlock = new RenderStateBlock?(renderStateBlock),
				layerMask = this.layerMask
			};
			HDUtils.DrawRendererList(renderContext, cmd, RendererList.Create(in rendererListDesc));
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00049700 File Offset: 0x00047900
		public override IEnumerable<Material> RegisterMaterialForInspector()
		{
			yield return this.overrideMaterial;
			yield break;
		}

		// Token: 0x04000DBF RID: 3519
		public bool filterFoldout;

		// Token: 0x04000DC0 RID: 3520
		public bool rendererFoldout;

		// Token: 0x04000DC1 RID: 3521
		public CustomPass.RenderQueueType renderQueueType = CustomPass.RenderQueueType.AllOpaque;

		// Token: 0x04000DC2 RID: 3522
		public string[] passNames = new string[] { "Forward" };

		// Token: 0x04000DC3 RID: 3523
		public LayerMask layerMask = 1;

		// Token: 0x04000DC4 RID: 3524
		public SortingCriteria sortingCriteria = SortingCriteria.CommonOpaque;

		// Token: 0x04000DC5 RID: 3525
		public Material overrideMaterial;

		// Token: 0x04000DC6 RID: 3526
		[SerializeField]
		private int overrideMaterialPassIndex;

		// Token: 0x04000DC7 RID: 3527
		public string overrideMaterialPassName = "Forward";

		// Token: 0x04000DC8 RID: 3528
		public bool overrideDepthState;

		// Token: 0x04000DC9 RID: 3529
		public CompareFunction depthCompareFunction = CompareFunction.LessEqual;

		// Token: 0x04000DCA RID: 3530
		public bool depthWrite = true;

		// Token: 0x04000DCB RID: 3531
		public DrawRenderersCustomPass.ShaderPass shaderPass;

		// Token: 0x04000DCC RID: 3532
		private int fadeValueId;

		// Token: 0x04000DCD RID: 3533
		private static ShaderTagId[] forwardShaderTags;

		// Token: 0x04000DCE RID: 3534
		private static ShaderTagId[] depthShaderTags;

		// Token: 0x04000DCF RID: 3535
		private ShaderTagId[] cachedShaderTagIDs;

		// Token: 0x02000277 RID: 631
		public enum ShaderPass
		{
			// Token: 0x04001634 RID: 5684
			DepthPrepass = 1,
			// Token: 0x04001635 RID: 5685
			Forward = 0
		}
	}
}
